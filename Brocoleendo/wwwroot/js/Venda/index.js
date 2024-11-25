var eventAttached = false;

$(document).ready(function () {

    $('#ProdutoSelecionadoID').change(function () {
        var selectedOption = $(this).find('option:selected');
        var saldo = selectedOption.data('saldo');


        if (saldo !== undefined) {
            $("#saldoProduto").val(saldo);
        } else {
            $("#saldoProduto").val(0.0);
        }
    });

    $("#plusVenda").click(function () {
        CriarVenda();
    });

    $("#plusProduto").click(function () {
        AddProduto();
    });

});

function CriarVenda() {
    if ($("#venda_ID_VENDA").val() == '' || $("#venda_ID_VENDA").val() == 0 || $("#venda_ID_VENDA").val() == null) {
        var jsonData = {
            ID_VENDA: 0,
            FORMA_PGTO: $("#ddlPagamentos").val(),
            DT_VENDA: "2024-11-23T15:30:00",
            ID_FUNC: localStorage.getItem("FuncionarioID"),
            NOME_FUNC: localStorage.getItem("FuncionarioNome"),
            PRECO_TOT: 0,
            ST_ATIVO: true
        };
        // console.log(jsonData);

        $.ajax({
            type: "POST",
            url: "newVenda",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(jsonData),
            success: function (data) {
                var json = $.parseJSON(data);
                if (json == 0) {
                    toasterWarning('<h4><i class=\"fa fa-exclamation-triangle\">&nbsp;</i><b> Erro ao criar venda</b></h4>');
                }
                else {
                    toasterSuccess('<h4><i class=\"fa fa-check\">&nbsp;</i><b> Venda criada com sucesso</b></h4>');
                    $("#venda_ID_VENDA").val(data);
                    $("#ddlPagamentos").attr('disabled', true);
                    $("#plusProduto").show();
                    $("#plusVenda").hide();
                    $("#plusProduto").click();
                }
            },
            error: function () { alert('Error') }
        });
    }
}

function AddProduto() {

    if ($("#ProdutoSelecionadoID").val() == '') {
        toasterWarning('<h4><i class=\"fa fa-exclamation-triangle\">&nbsp;</i><b>Selecione um produto</b></h4>');
        $("#ProdutoSelecionadoID").focus();
        return;
    }

    if ($("#kgQuantidade").val() == '' || $("#kgQuantidade").val() == 0 || parseFloat($("#kgQuantidade").val()) > parseFloat($("#saldoProduto").val())) {
        toasterWarning('<h4><i class=\"fa fa-exclamation-triangle\">&nbsp;</i><b> Saldo inválido</b></h4>');
        $("#kgQuantidade").focus();
        return;
    }

    var jsonData2 = {
        ID_VENDA_PRODUTO: 0,
        ID_VENDA: $("#venda_ID_VENDA").val(),
        ID_PRODUTO: $("#ProdutoSelecionadoID").val(),
        ID_Estoque_Prod: 0,
        FORMA_PGTO: $("#ddlPagamentos").val(),
        DT_VENDA: "2024-11-23T15:30:00",
        NOME_FUNC: localStorage.getItem("FuncionarioNome"),
        DS_PRODUTO: "-",
        Url_Img: "-",
        KG_PRODUTO: parseFloat($("#kgQuantidade").val()),
        PRECO_UNI: 0,
        PRECO_TOT: 0
    };

    // console.log(jsonData2);

    $.ajax({
        type: "POST",
        url: "newProdutoVenda",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(jsonData2),
        success: function (data2) {
            // console.log(data2);
            if (data2 == null) {
                toasterDanger('<h4><i class=\"fa fa-exclamation-triangle\">&nbsp;</i><b> Erro ao adicionar produto</b></h4>');
            }
            else if (data2 == $('#produtosDiv').html()) {
                toasterWarning('<h4><i class=\"fa fa-exclamation-triangle\">&nbsp;</i><b> Saldo insuficiente</b></h4>');
                $("#kgQuantidade").focus();
            }
            else {

                var selectedOption = $("#ProdutoSelecionadoID").find('option:selected');
                var saldo = selectedOption.data('saldo');
                var newSaldo = (parseFloat(saldo) - parseFloat($("#kgQuantidade").val()));

                console.log(parseFloat(saldo) + " - " + parseFloat($("#kgQuantidade").val()) + " = " + newSaldo);

                selectedOption.attr('data-saldo', newSaldo);
                selectedOption.data('saldo', newSaldo);
                $("#saldoProduto").val(newSaldo);

                $("#kgQuantidade").val('0');
                $("#ProdutoSelecionadoID").val('').change();

                toasterSuccess('<h4><i class=\"fa fa-check\">&nbsp;</i><b> Produto adicionado com sucesso</b></h4>');
                $('#produtosDiv').html(data2);

                attachEventHandlers();
            }
        },
        error: function (data2) {
            console.log(data2);
            alert('Error')
        }
    });

}


function attachEventHandlers() {


    if (eventAttached == false) {
        $(document).on('click', '#deletItem', (function () {
            var _idVendaProduto = $(this).data('iv_venda');
            var _idProduto = $(this).data('id');
            var _kgProduto = parseFloat($(this).data('kg'));

            console.log(_idVendaProduto);
            var jsonData2 = {
                ID_VENDA_PRODUTO: _idVendaProduto,
                ID_VENDA: $("#venda_ID_VENDA").val(),
                ID_PRODUTO: _idProduto,
                ID_Estoque_Prod: 0,
                FORMA_PGTO: $("#ddlPagamentos").val(),
                DT_VENDA: "2024-11-23T15:30:00",
                NOME_FUNC: localStorage.getItem("FuncionarioNome"),
                DS_PRODUTO: "-",
                Url_Img: "-",
                KG_PRODUTO: _kgProduto,
                PRECO_UNI: 0,
                PRECO_TOT: 0
            };

            //console.log(jsonData2);

            $.ajax({
                type: "POST",
                url: "dltProdutoVenda",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(jsonData2),
                success: function (data2) {
                    if (data2 == null) {
                        toasterDanger('<h4><i class=\"fa fa-exclamation-triangle\">&nbsp;</i><b> Erro ao remover produto</b></h4>');
                    }
                    else if (data2 == $('#produtosDiv').html()) {
                        toasterWarning('<h4><i class=\"fa fa-exclamation-triangle\">&nbsp;</i><b> Erro ao remover produto</b></h4>');
                    }
                    else {


                        var selectedOption = $("#ProdutoSelecionadoID").find(`option[value="${_idProduto}"]`);
                        var saldo = selectedOption.data('saldo');
                        var newSaldo = (parseFloat(saldo) + _kgProduto);

                        var print = (parseFloat(saldo) + " + " + _kgProduto + " = " + newSaldo);
                        console.log(print);

                        selectedOption.attr('data-saldo', newSaldo);
                        selectedOption.data('saldo', newSaldo);

                        $("#kgQuantidade").val('0');
                        $("#ProdutoSelecionadoID").val('').change();

                        $('#produtosDiv').html(data2);
                        toasterSuccess('<h4><i class=\"fa fa-check\">&nbsp;</i><b> Produto removido com sucesso</b></h4>');
                        $("#deletItem").data("event-attached", true);


                    }
                },
                error: function (data2) {
                    console.log(data2);
                    alert('Error')
                }
            });


        }));
    }
    eventAttached = true;



}

function deleteProduto() {

}
