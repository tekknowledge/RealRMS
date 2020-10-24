var login = (function () {
    return {
        submit: function (evt) {
            if (document.getElementById("pwd").value != "correct") {
                $(".errorText").html("Incorrect credentials. Please check your id and password and try again.");
                evt.preventDefault();
                return;
            }
            localStorage.idWebuser = document.getElementById("empid").text;
            window.location.href = 'mock_menu.htm';
        }
    }
}());
