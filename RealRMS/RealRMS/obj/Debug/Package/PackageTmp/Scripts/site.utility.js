var site = site || {};
(function (site) {
    site.utility = {};
    var queryStringKeyValuePairs = null;

    function parseKeyValuePairs(params) {
        var keyValuePairs = [];
        for (var i = 0; i < params.length; i++) {
            var param = params[i];
            var hasValue = param.indexOf('=') > -1;
            var keyValue = {};
            if (hasValue) {
                var keyValAry = param.split('=');
                keyValue = { key: keyValAry[0], value: keyValAry[1] };
            } else {
                keyValue = { key: param, value: "" };
            }

            keyValuePairs.push( keyValue );
        }  
        return keyValuePairs;

    }

    site.utility.getQueryStringValue = function(key) {
        var kvps = [];
        if (queryStringKeyValuePairs != null) {
            kvps = queryStringKeyValuePairs;
        } else {
            var hasQuery = window.location.href.indexOf('?') > -1;
            if (!hasQuery)
                return "";

            var query = window.location.href.split('?')[1];
            var hasMultipleParams = query.indexOf('&') > -1;
            kvps = parseKeyValuePairs(hasMultipleParams ? query.split('&') : [query]);
            queryStringKeyValuePairs = kvps;
        }

        for (var k = 0; k < kvps.length; k++) {
            var pair = kvps[k];
            if (key.toLowerCase() == pair.key.toLowerCase()) {
                return pair.value;
            }
        }

        return "";
    }
}(site));