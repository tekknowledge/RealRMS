var menuList = (function () {

    $(document).ready(function () {
        //site.ui.populateTable("grid", site.data.menuList);
    });
    return {
        filter: function (sel) {
            var idx = sel.selectedIndex;
            var category = sel[idx].text;
            var grid = document.getElementById("grid");
            var previousRowStyle = "row";
            for (var i = 1; i < grid.rows.length; i++) {
                var row = grid.rows[i];
                if (category == "--Any--" || row.cells[2].innerHTML.indexOf(category) > -1) {
                    grid.rows[i].style.display = "";
                    grid.rows[i].className = previousRowStyle === "row" ? "alternatingRow" : "row";

                } else {
                    grid.rows[i].style.display = "none";
                }

                if (grid.rows[i].style.display !== "none")
                    previousRowStyle = grid.rows[i].className;
            }
        },
        deleteRow: function (id, rowIndex) {
            if (confirm('Are you sure you want to remove this item?')) {
                $.ajax({
                    url: "/menu/delete?id=" + id,
                    type: "DELETE",
                    dataType: "json"
                }).done(function (data) {
                    if (data == true) {
                        document.getElementById("grid").deleteRow(rowIndex);
                        $(".informationalText").html("Your item has successfully been deleted.");
                    }
                }).error(function (data) {
                    window.alert("There was an error processing your request. Try again. If the problem persists, please contact the site administrator");
                });
            }
        }
    }
}());
