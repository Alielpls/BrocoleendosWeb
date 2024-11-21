$(document).ready(function () {

    if (localStorage.getItem("FuncionarioID") == null) {
        alert("Sem login");
    } else {
       // alert("Bem vindo, " + localStorage.getItem("FuncionarioNome"));
    }

});