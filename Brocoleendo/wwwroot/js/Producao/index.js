$(document).ready(function () {
    $('.details').click(function () {
        var buttonClicked = $(this);
        var DS_id = buttonClicked.attr('data-id');

        var jsonQuadrante = {
            ID_Localizacao_Lote: DS_id,
            DS_Quadrante: "AA",
            ST_USO: true,
            ID_Producao: $(this).data('producao'),
            DS_Produto: $(this).data('produto'),
            ST_Ativo: true
        };

        console.log(jsonQuadrante);

        $.ajax({
            url: 'Producao/Details',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(jsonQuadrante),
            success: function (partialView) {
                $('#detailsPlace').html(partialView);
                $('#detailsPlace').show();
                attachEventHandlers();
            }
        });
    });
});

function attachEventHandlers() {
    console.log("aa");

    $("#plusProducao").on('click', function () {

        var jsonData = {
            ID_LOCALIZACAO_LOTE: parseInt($(this).data("idquadrante")), // obligated
            ID_Produto: parseInt($("#ProdutoSelecionadoID").val()) // obligated
        };

        console.log(jsonData);

        $.ajax({
            type: "POST",
            url: "Producao/InsProducao",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(jsonData),
            success: function (data) {
                var json = $.parseJSON(data);
                if (json == 0 || json == null) {
                    toasterWarning('<h4><i class=\"fa fa-exclamation-triangle\">&nbsp;</i><b> Erro ao criar produção</b></h4>');
                }
                else {
                    toasterSuccess('<h4><i class=\"fa fa-check\">&nbsp;</i><b> Produção criada com sucesso</b></h4>');
                    $('#detailsPlace').html('');
                    window.location.href = "";
                }
            },
            error: function () { alert('Error') }
        });

    });



    $("#finishProducao").click(function () {

        if ($("#txtKgRendido").val() == '') {
            toasterWarning('<h4><i class=\"fa fa-exclamation-triangle\">&nbsp;</i><b> Digite a quantidade de Kg rendido</b></h4>');
            $("#txtKgRendido").focus();
            return;
        }

        jsonDataFinish = {
            ID_Producao: parseInt($(this).data("idproducao")),
            KG_Rendido: parseFloat($("#txtKgRendido").val())
        };


        $.ajax({
            type: "POST",
            url: "Producao/dltProducao",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(jsonDataFinish),
            success: function (data) {
                var json = $.parseJSON(data);
                if (json == false || json == null) {
                    toasterWarning('<h4><i class=\"fa fa-exclamation-triangle\">&nbsp;</i><b> Erro ao finalizar produção</b></h4>');
                }
                else {
                    toasterSuccess('<h4><i class=\"fa fa-check\">&nbsp;</i><b> Produção finalizada com sucesso</b></h4>');
                    $('#detailsPlace').html('');
                    window.location.href = "";
                }
            },
            error: function () { alert('Error') }
        });

    });

    $("#plusAcao").on('click', function () {

        if ($("#txtAcao").val().trim() == "") {
            toasterWarning('<h4><i class=\"fa fa-exclamation-triangle\">&nbsp;</i><b> Digite a ação realizada</b></h4>');
            $("#txtAcao").focus();
            return;
        }

        var jsonData = {
            ID_PRODUCAO: parseInt($(this).data("idproducao")), // obligated
            ID_FUNC: localStorage.getItem("FuncionarioID"), // obligated
            DS_ACAO: $("#txtAcao").val(),
            ID_QUADRANTE: parseInt($(this).data("idquadrante"))
        };
        console.clear();
        console.log(jsonData);

        $.ajax({
            type: "POST",
            url: "Producao/InsAcaoProducao",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(jsonData),
            success: function (partialView) {
                toasterSuccess('<h4><i class=\"fa fa-check\">&nbsp;</i><b> Ação adicionada com sucesso</b></h4>');
                $('#detailsPlace').html('');
                $('#detailsPlace').show();
                $('#detailsPlace').html(partialView);
                $("#txtAcao").val('');
                attachEventHandlers();
            },
            error: function (partialView) {
                toasterWarning('<h4><i class=\"fa fa-exclamation-triangle\">&nbsp;</i><b> Erro ao adicionar ação</b></h4>');
                console.log(partialView)
            }
        });
    });




}