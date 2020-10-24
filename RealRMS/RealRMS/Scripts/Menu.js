var menu = (function () {
    $(document).ready(function () {

    });

    return {
        showClockOutButton: function () {
            $("#clockLink").html($("#clockLink").html().replace("In", "Out"));
            $("#clockLink").prop("href", "javascript:menu.clockOut();");
        },

        showClockInButton: function () {
            $("#clockLink").html($("#clockLink").html().replace("Out", "In"));
            $("#clockLink").prop("href", "javascript:menu.clockIn();");
        },

        clockIn: function () {
            var empId = prompt("Please enter your employee id.", "Employee Id #");
            if (empId == null || empId == "")
                return;
            if (isNaN(empId) || empId % 1 !== 0) {
                window.alert("Please Enter a valid Employee Id.");
                return;
            }
            if (empId != document.getElementById("EmployeeId").value) {
                window.alert("Invalid Employee Id. Please enter YOUR employee id and try again.");
                return;
            }

            $.ajax({
                url: "/home/clockIn?EmployeeId=" + empId,
                type: "POST",
                dataType: "json"
            }).done(function (data) {
                if (data != null && data.Id > 0) {
                    window.alert("You have been clocked in at " + (new Date()).toString());
                    $("#TimeCardId").val(data.Id);
                    menu.showClockOutButton();
                }
            }).error(function(data) {
                window.alert("There was an error processing your request. Try again. If the problem persists, please contact the site administrator");
            });

        },

        clockOut: function () {
            var empId = prompt("Please enter your employee id.", "Employee Id #");
            if (empId == null)
                return;
            if (isNaN(empId) || empId % 1 !== 0) {
                window.alert("Please Enter a valid Employee Id.");
                return;
            }

            if (empId != document.getElementById("EmployeeId").value) {
                window.alert("Invalid Employee Id. Please enter YOUR employee id and try again.");
                return;
            }

            $.ajax({
                url: "/home/clockOut?EmployeeId=" + empId,
                type: "POST",
                dataType: "json"
            }).done(function (data) {
                if (data != null && data.Id > 0) {
                    window.alert("You have been clocked out at " + (new Date()).toString());
                    menu.showClockInButton();

                }
            }).error(function (data) {
                window.alert("There was an error processing your request. Try again. If the problem persists, please contact the site administrator");
            });
        }
    }
}());