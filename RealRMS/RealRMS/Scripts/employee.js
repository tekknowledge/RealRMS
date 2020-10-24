var manageEmployee = (function () {
    $(document).ready(function () {

    });

    function swap(dd1, dd2) {
        for (var i = dd1.options.length - 1; i >= 0; i--) {
            if (dd1.options[i].selected) {
                var option = document.createElement("option");
                option.text = dd1.options[i].text;
                option.value = dd1.options[i].value;
                dd2.options.add(option);
                dd1.options.remove(i);
            }
        }
    }

    return {
        assign: function () {
            var assigned = document.getElementById("assignedRoles");
            var available = document.getElementById("avRoles");
            swap(available, assigned);
        },

        revoke: function () {
            var assigned = document.getElementById("assignedRoles");
            var available = document.getElementById("avRoles");
            swap(assigned, available);
        },

        gatherRoles: function () {
            var assignedRoles = document.getElementById("assignedRoles");
            var roles = document.getElementById("rolestring");
            var allRoles = [];
            for (var i = 0; i < assignedRoles.options.length; i++) {
                var data = {};
                data.id = assignedRoles.options[i].value;
                data.name = assignedRoles.options[i].text;
                allRoles.push(data);
            }
            roles.value = JSON.stringify(allRoles);
        }
    }
}());