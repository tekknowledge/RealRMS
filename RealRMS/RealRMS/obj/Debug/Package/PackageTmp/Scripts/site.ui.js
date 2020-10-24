var site = site || {};
(function (site) {
    site.ui = {};

    site.ui.populateTable = function(htmlId, data, bindingFunctions, styles) {
        var tbl = document.getElementById("grid");
        for (var i = 0; i < data.length; i++) {
            var item = data[i];


            var propertyCount = 0;

            var row = tbl.insertRow(i + 1);
            if (i == 0 || i % 2 == 0)
                row.className = "alternatingrow";
            else
                row.className = "row";
            for (var property in item) {
                if (item.hasOwnProperty(property)) {
                    var predicate = null;
					var style = null;
                    if (bindingFunctions !== undefined)
                        predicate = bindingFunctions[property];
                    if (styles !== undefined)
                        style = styles[property];
                    var cell = row.insertCell(propertyCount);
                    cell.innerHTML = predicate == null ? item[property] : predicate(item[property], row.className == "row");
					cell.className = style == null ? "" : style;
                    propertyCount++;
                }
            }
        }
    }

    site.ui.populateOptionByText = function(ddlId, optionText) {
        var ddl = document.getElementById(ddlId);
        for (var i = 0; i < ddl.options.length; i++) {
            var opt = ddl.options[i];
            if (opt.text == optionText) {
                ddl.selectedIndex = i;
                return;
            }
        }
    }

    site.ui.focusOnValidationError = function(id) {
        var $target = $('input, select, textarea').filter(function () {
            return $(this).attr('name').toLowerCase() == id.toLowerCase();
        });
        $target.focus();
    }

    site.ui.addDashes = function (input, place1, place2) {
        try {
            var value = input.value.replace(/-/g, "");
            var firstPart = value.substring(0, place1);
            var secondPart = value.substring(place1, place2);
            var theRest = value.substring(place2, value.length);
            input.value = firstPart + "-" + secondPart + "-" + theRest;
        }catch (ex){}
    }
}(site));