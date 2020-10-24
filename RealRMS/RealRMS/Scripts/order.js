var order = (function () {
    $(document).ready(function () {

    });
    var orderApp = angular.module('orderApp', []);

    orderApp.controller('OrderController', function ($scope) {
        $scope.menu = {};
        $scope.orderedItems = [];
        $scope.seats = [1];
        $scope.orderNumber = 0;
        $scope.order = {};
        $scope.menuDictionary = [];
        $scope.annotating = false;
        $scope.currentOrderItem = null;
        $scope.isAdmin = false;
        $scope.splittingChecks = false;
        var newItemCounter = 0; // This is a hack. Angular dropdowns inside of ng-repeat want to bind to each other. It wants to assign all of one item to one person. Ugh.

        // Loads the current order being viewed.
        $scope.init = function (model) {
            $scope.orderNumber = model.order.Id;
            $scope.order = model.order;
            $scope.order.EmployeeId = model.EmployeeId;
            $scope.menu = getCategorizedMenu(model.fullMenu);
            $scope.menuDictionary = model.fullMenu;
            $scope.orderedItems = model.order.OrderItems;
            $scope.isAdmin = model.IsAdmin;
            if ($scope.orderNumber > 0) {
                initializeSeats();
                $scope.orderedItems = getOrderItemObjectForClient();
            }
        }

        // Creates a new order.
        $scope.createOrder = function () {
            if (isNaN($scope.order.NumberOfSeats) || $scope.order.NumberOfSeats <= 0) {
                window.alert("Number of Seats must be a positive integer");
                return;
            }

            if ($scope.order.TableId <= 0) {
                window.alert("Table Id must be a postive integer");
                return;
            }
            
            initializeSeats();
            var CreateUrl = "/Order/Create";
            $.ajax({
                url: CreateUrl,
                type: "POST",
                data: $scope.order
            }).done(function (data) {
                if (data != null && data.Id > 0) {
                    $scope.order = data;
                    $scope.orderNumber = data.Id;
                    $scope.$digest();
                } else {
                    window.alert("This table is currently taken. Please enter an unoccupied table number.");
                }
            }).error(function (data) {
                window.alert("There was an error processing your request. Try again. If the problem persists, please contact the site administrator");
            });
        }

        // Adds an item to an order. There is a hack here so that the Assigned Seat drop down doesn't duplicate for doubles of items.
        $scope.addItemToOrder = function (item) {
            var orderedItm = {};
            orderedItm.menuId = item.menuId;
            orderedItm.name = item.name;
            orderedItm.description = item.description;
            orderedItm.price = item.price;
            orderedItm.id = newItemCounter;
            $scope.orderedItems.push(orderedItm);
            newItemCounter--;
        }

        // Removes an item from an order and deletes from the database.
        $scope.removeItemFromOrder = function (index) {
            var orderItemId = $scope.orderedItems[index].id;
            $scope.orderedItems.splice(index, 1);
            if (orderItemId <= 0)
                return;

            var DeleteUrl = "/Order/DeleteItem/";
            $.ajax({
                url: DeleteUrl + orderItemId,
                type: "Delete",
            }).done(function (data) {
                if (data === true) {
                    $scope.$digest();
                }
            }).error(function (data) {
                window.alert("There was an error processing your request. Try again. If the problem persists, please contact the site administrator");
            });
        }

        // Commits the added order items to the database
        $scope.sendOrder = function () {
            $(".informationalText").text("Your order has been sent to the kitchen!");
            var dataToSave = getOrderItemObjectForServer();
            var CreateUrl = "/Order/AddItems";

            $.ajax({
                url: CreateUrl,
                type: "POST",
                data: JSON.stringify(dataToSave),
                contentType: "application/json"
            }).done(function (data) {
                if (data != null && data.Id > 0) {
                    $scope.order = data;
                    $scope.orderNumber = data.Id;
                    initializeSeats();
                    $scope.orderedItems = getOrderItemObjectForClient();
                    $scope.$digest();
                }
            }).error(function (data) {
                window.alert("There was an error processing your request. Try again. If the problem persists, please contact the site administrator");
            });

        }

        $scope.voidOrder = function () {
            if (window.confirm("Void this order?")) {
                var UpdateUrl = "/Order/Update";
                $scope.order.Voided = true;
                $scope.order.InProgress = false;
                $scope.order.Delivered = false;

                $.ajax({
                    url: UpdateUrl,
                    type: "POST",
                    data: $scope.order
                }).done(function (data) {
                    if (data === true) {
                        window.location.href = "/Order/Index?message=Order has successfully been voided.";
                    } else {
                        window.alert("There order was not voided. Try again. If the problem persists, please contact the site administrator");
                    }
                }).error(function (data) {
                    window.alert("There was an error processing your request. Try again. If the problem persists, please contact the site administrator");
                });
            }
        }

        // Opens the comment screen for an order item.
        $scope.initializeComment = function (index, orderItem) {
            $scope.currentOrderItem = orderItem;
            $scope.orderItemCommentIndex = index;
            $scope.annotating = true;
        }

        // Adds a comment to an order item.
        $scope.addComment = function () {
            $scope.orderedItems[$scope.orderItemCommentIndex].comment = $scope.currentOrderItem.comment;
            $scope.annotating = false;
        }

        // Cancels adding a comment for an order item.
        $scope.cancelComment = function () {
            $scope.annotating = false;
        }

        $scope.splitChecks = function() {
            $scope.splittingChecks = true;
        }

        $scope.cancelCheckout = function() {
            $scope.splittingChecks = false;
        }

        // Converts the angular js order item object to an object that can be digested by the server.
        function getOrderItemObjectForServer() {
            var dataToSave = [];
            for (var i = 0; i < $scope.orderedItems.length; i++) {
                var orderedItem = $scope.orderedItems[i];
                var itemToSave = {};
                itemToSave.Id = orderedItem.id <= 0 ? 0 : orderedItem.id;
                itemToSave.MenuId = orderedItem.menuId;
                itemToSave.SeatNumber = orderedItem.seatNumber === undefined ? 1 : orderedItem.seatNumber;
                itemToSave.OrderId = $scope.order.Id;
                itemToSave.Ready = null;
                itemToSave.Comment = orderedItem.comment === undefined ? null : orderedItem.comment;
                dataToSave.push(itemToSave);
            }
            return dataToSave;
        }

        // Gets the readable order item object to be digested by angular js
        function getOrderItemObjectForClient() {
            var itemsForClient = [];
            for (var i = 0; i < $scope.order.OrderItems.length; i++) {
                var item = {};
                var itemFromServer = $scope.order.OrderItems[i];
                item.id = itemFromServer.Id;
                item.menuId = itemFromServer.MenuId;
                item.seatNumber = itemFromServer.SeatNumber;
                item.comment = itemFromServer.Comment;
                for (var j = 0; j < $scope.menuDictionary.length; j++) {
                    var menuItem = $scope.menuDictionary[j];
                    if (menuItem.Id == item.menuId) {
                        item.name = menuItem.Name;
                        break;
                    }
                }
                itemsForClient.push(item);
            }
            return itemsForClient;
        }

        // Initializes the seat dropdowns
        function initializeSeats() {
            $scope.seats = [];
            for (var i = 1; i <= $scope.order.NumberOfSeats; i++) {
                $scope.seats.push(i);
            }
        }

        // Gets the menu for display sorted by category
        function getCategorizedMenu(menu) {
            var items = getParsedMenu(menu);
            var categories = [];
            var menuToDisplay = {};
            var i;
            for (i = 0; i < items.length; i++) {
                var item = items[i];
                if (categories.indexOf(item.category) == -1)
                    categories.push(item.category);
            }
            categories.sort();
            for (i = 0; i < categories.length; i++) {
                var category = categories[i];
                menuToDisplay[category] = [];
                for (var j = 0; j < items.length; j++) {
                    var item = items[j];
                    if (item.category == category) {
                        menuToDisplay[category].push(
							{
							    menuId: item.id,
							    name: item.name,
							    description: item.description,
							    price: item.cost
							}
						);
                    }
                }
            }
            return menuToDisplay;
        }

        // Gets an angular js object that displays the menu
        function getParsedMenu(menu) {
            var parsedMenuItems = [];
            for (var i = 0; i < menu.length; i++) {
                var menuItemToParse = menu[i];
                var parsedMenuItem = {};
                parsedMenuItem.id = menuItemToParse.Id;
                parsedMenuItem.name = menuItemToParse.Name;
                parsedMenuItem.category = menuItemToParse.MenuCategory.Name;
                parsedMenuItem.description = menuItemToParse.Description;
                parsedMenuItem.cost = menuItemToParse.Price;
                parsedMenuItems.push(parsedMenuItem);
            }
            return parsedMenuItems;
        }
    });
    return {
        checks: {},

        allowDrop: function (ev) {
            ev.preventDefault();
        },

        drag: function (ev) {
            ev.dataTransfer.setData("text", ev.target.id);
        },

        drop: function (ev) {

            ev.preventDefault();
            var data = ev.dataTransfer.getData("text");
            ev.target.appendChild(document.getElementById(data));
        },

        prepareSingleOrder: function(numberOfSeats, orderId) {
            order.checks = {};
            var list = [];
            var check = {};
            check.Partition = 1;
            check.Seats = [];
            check.OrderId = orderId;
            numberOfSeats = JSON.parse(numberOfSeats);
            for (var i = 0; i < numberOfSeats; i++) {
                check.Seats.push(i);
            }
            list.push(check);
            order.checks.list = list;
            order.sendOrder(orderId);

        },

        prepareMultipleOrder: function (orderId) {
            order.checks = {};
            var list = [];
            $(".droppableSection").each(function (idx, ctrl) {
                var $div = $(ctrl);
                var check = {};
                check.Partition = $div.prop("id").replace("check", 0);
                check.Seats = [];
                check.OrderId = orderId;
                $div.find(".draggableSeatNumber").each(function (j, btn) {
                    check.Seats.push(btn.value);
                    //list.push(check);
                });
                if (check.Seats.length > 0)
                    list.push(check);

            });
            order.checks.list = list;
            $("#splitChecks").val(JSON.stringify(order.checks));

            var seatsAccountedFor = 0;
            for (var i = 0; i < order.checks.list.length; i++) {
                seatsAccountedFor += order.checks.list[i].Seats.length;
            }
            if (seatsAccountedFor !== $(".droppableSection").length) {
                window.alert("All checks must belong to an order.");
                return;
            }
            order.sendOrder(orderId);
        },

        sendOrder: function(orderId) {
            var CreateTransaction = "/Transaction/Create";

            order.checks.OrderId = orderId;
            $.ajax({
                url: CreateTransaction,
                type: "POST",
                data: JSON.stringify(order.checks.list),
                contentType: "application/json"
            }).done(function (data) {
                if (data.length > 0) {
                    window.location.href = "/Transaction/ViewTransaction?orderIds=" + data;
                } else {
                    window.alert("There was an error processing your request. Try again. If the problem persists, please contact the site administrator");
                }
            }).error(function (data) {
                window.alert("There was an error processing your request. Try again. If the problem persists, please contact the site administrator");
            });
        }
    }
}());