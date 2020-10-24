var site = site || {};
(function (site) {
    $(document).ready(function() {
        var $messageSpan = $(".informationalText");
        if ($messageSpan.length > 0) {
            var message = site.utility.getQueryStringValue("message");
            if (message.length > 0)
                $messageSpan.text(decodeURIComponent(message));
        }
        var $errorSpan = $(".errorText");
        if ($messageSpan.length > 0) {
            var error = site.utility.getQueryStringValue("errorMessage");
            if (error.length > 0)
                $errorSpan.text(decodeURIComponent(error));
        }
    });

}(site));