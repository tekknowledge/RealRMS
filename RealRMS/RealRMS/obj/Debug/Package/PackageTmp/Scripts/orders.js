var orders = (function () {
    $(document).ready(function () {
        var seconds = 60;
        // This is a usability issue but i'm probably not going to have time to fix it. The page should reload via ajax and not interrupt work.
        window.setTimeout(
            function () {
                if (!isRefreshing()) // can this be changed to a while?
                    window.location.reload();
            }, (1000 * seconds * 5)
        );
    });
    function getNextRow(element) {
        if (element.tagName === "TR")
            return element;
        return getNextRow(element.nextSibling);
    }

    var refreshing = false;

    function isRefreshing() {
        return refreshing;
    }

    function showLoadingSpan($spanToShow, $spanToHide) {
        $spanToShow.show();
        if ($spanToHide)
            $spanToHide.hide();
    }

    function fadeLoadingSpan($spanToFade) {
        window.setTimeout(
            function () {
                $spanToFade.fadeOut();
            }, (2000)
        );    
    }

    return {
        expand: function(button) {
            var nextTableRow = getNextRow(button.parentNode.parentNode.nextSibling);
            nextTableRow.style.display = nextTableRow.style.display !== "none" ? "none" : "";
            button.value = button.value === "+" ? "-" : "+";
        },
        setOrderDelivered: function(ctrl, orderId) {
            var updateUrl = "/Order/SetDelivered?value=" + ctrl.checked.toString().toLowerCase() + "&orderId=" + orderId.toString();
            var $savingSpan = $("#deliveringPrompt_" + orderId.toString());
            var $savedSpan = $("#deliveredPrompt_" + orderId.toString());

            showLoadingSpan($savingSpan);

            $.ajax({
                url: updateUrl,
                type: "POST"
            }).done(function (data) {
                refreshing = false;
                showLoadingSpan($savedSpan, $savingSpan);
                fadeLoadingSpan($savedSpan);
            }).error(function (data) {
                window.alert("There was an error processing your request. Try again. If the problem persists, please contact the site administrator");
            });
        },
        updateOrder : function(orderId) {
            refreshing = true;
            var $savingPromptSpan = $("#savingPrompt_" + orderId.toString());
            var $savedPromptSpan = $("#savedPrompt_" + orderId.toString());

            showLoadingSpan($savingPromptSpan);

            var checkboxes = $(".orderItem" + orderId.toString());
            var orderItems = [];
            var allChecked = true;
            checkboxes.each(function (index, chk) {
                var orderItem = JSON.parse(chk.value);
                orderItem.Ready = chk.checked;
                allChecked = allChecked && chk.checked;
                orderItems.push(orderItem);
            });
            $("#orderDone_" + orderId.toString()).prop("checked", allChecked);
            if (allChecked)
                $("#orderDelivered_" + orderId.toString()).removeAttr("disabled");
            else
                $("#orderDelivered_" + orderId.toString()).prop("disabled", !allChecked);
            var CreateUrl = "/Order/AddItems";

            $.ajax({
                url: CreateUrl,
                type: "POST",
                data: JSON.stringify(orderItems),
                contentType: "application/json"
            }).done(function (data) {
                refreshing = false;
                showLoadingSpan($savedPromptSpan, $savingPromptSpan);
                fadeLoadingSpan($savedPromptSpan);
            }).error(function (data) {
                window.alert("There was an error processing your request. Try again. If the problem persists, please contact the site administrator");
            });
        }
    }
}());