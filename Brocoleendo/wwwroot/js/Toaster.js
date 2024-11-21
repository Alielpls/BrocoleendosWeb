var toasterDestroy;
var idToaster = "#snackbar";


function gerenciarToaster(tipoToaster, fecharModal) {
    destruirToaster();
    fecharModal = (typeof fecharModal !== 'undefined') ? fecharModal : false;

    // $(idToaster).addClass("toaster");
    if (fecharModal) {
        $(".modal").modal('hide');
    }

    visibilidadeToaster = $(idToaster).css("visibility");
    $(idToaster).addClass(tipoToaster).addClass("show");
    if (visibilidadeToaster == 'hidden') {
        toasterDestroy = setTimeout(function () {
            $(idToaster).removeClass().addClass("toaster");
        }, 4900);
    }
}

function salvoSucesso(mensagem, fecharModal) {
    destruirToaster();
    //$(".modal").modal('hide');
    var tipoToaster = "toaster-success";
    gerenciarToaster(tipoToaster, fecharModal);
    $('<div>' + mensagem + '</div>').appendTo($(idToaster));

    //$("#snackbar").addClass("toaster-success").addClass("show");
    //toasterDestroy = setTimeout(function () {
    //    $(idToaster).empty(); //LIMPAR TEXTO ANTERIOR
    //    $("#snackbar").removeClass("toaster-success").removeClass("show");
    //}, 5000);//6sec
}

function toasterSuccess(mensagem, fecharModal) {
    destruirToaster();
    //$(".modal").modal('hide');
    var tipoToaster = "toaster-success";
    gerenciarToaster(tipoToaster, fecharModal);
    $('<div>' + mensagem + '</div>').appendTo($(idToaster));

    //$("#snackbar").addClass("toaster-success").addClass("show");
    //toasterDestroy = setTimeout(function () {
    //    $(idToaster).empty(); //LIMPAR TEXTO ANTERIOR
    //    $("#snackbar").removeClass("toaster-success").removeClass("show");
    //}, 5000);//6sec
}

function toasterInfo(mensagem, fecharModal) {
    destruirToaster();
    //$(mensagem).appendTo($(idToaster));
    //$("#snackbar").addClass("toaster-info").addClass("show");
    //toasterDestroy = setTimeout(function () {
    //    $(idToaster).empty(); //LIMPAR TEXTO ANTERIOR
    //    $("#snackbar").removeClass("toaster-info").removeClass("show");
    //}, 5000);//6sec

    var tipoToaster = "toaster-info";
    gerenciarToaster(tipoToaster, fecharModal);
    $('<div>' + mensagem + '</div>').appendTo($(idToaster));
}

function toasterWarning(mensagem, fecharModal) {
    destruirToaster();
    //$(mensagem).appendTo($(idToaster));
    //$("#snackbar").addClass("toaster-warning").addClass("show");
    //toasterDestroy = setTimeout(function () {
    //    $(idToaster).empty(); //LIMPAR TEXTO ANTERIOR
    //    $("#snackbar").removeClass("toaster-warning").removeClass("show");
    //}, 5000);//6sec

    var tipoToaster = "toaster-warning";
    gerenciarToaster(tipoToaster, fecharModal);
    $('<div>' + mensagem + '</div>').appendTo($(idToaster));
}

function toasterDanger(mensagem, fecharModal) {
    destruirToaster();

    var tipoToaster = "toaster-danger";
    gerenciarToaster(tipoToaster, fecharModal);
    $('<div>' + mensagem + '</div>').appendTo($(idToaster));

}

function destruirToaster() {
    clearTimeout(toasterDestroy);
    $(idToaster).empty().removeClass().addClass("toaster");

    criarToaster = (typeof $(idToaster).css("visibility") !== 'undefined') ? false : true;
    if (criarToaster) {
        $('<div id="snackbar" class="toaster"><label id="lblToaster"></label></div>').appendTo($(".container"));
    }
}