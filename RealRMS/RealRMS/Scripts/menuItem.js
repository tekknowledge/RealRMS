        var menuItem = function () {

        $(document).ready(function() {
            var id = site.utility.getQueryStringValue("id"); //window.location.href.split('=')[1];
            /*for (var i = 0; i < site.data.menu.length; i++) {
                var item = site.data.menu[i];
                if (item.id == parseInt(id)) {
                    document.getElementById("id").value = item.id;
                    document.getElementById("name").value = item.name;
                    document.getElementById("description").value = item.description;
                    document.getElementById("cost").value = item.cost;
                    site.ui.populateOptionByText("category", item.category);
                }
            }*/
        });

		return {
		    setCategory: function (ddl) {
                if (ddl.selectedIndex == 0) {
                    return;
                }

			    var selectedOption = ddl.options[ddl.selectedIndex];
			    var data = {};
			    data.id = selectedOption.value;
			    data.name = selectedOption.text;
			    document.getElementById("menucategory.id").value = data.id;
			    document.getElementById("menucategory.name").value = data.name;
			}
		}
	}();