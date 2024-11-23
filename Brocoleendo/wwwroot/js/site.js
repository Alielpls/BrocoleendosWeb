$(document).ready(function () {

    if ((localStorage.getItem("FuncionarioID") == null || localStorage.getItem("FuncionarioID") == 0) && (window.location.pathname != "/Home/Login")) {
        //alert("Sem login");
        window.location.replace("../Home/Login");
    }

    $("#sairLogin").click(function () {
        localStorage.clear();
        window.location.replace("../Home/Login");
    });

});