$(document).ready(function () {
    function decodeHTMLEntities(text) {
        var txt = document.createElement("textarea");
        txt.innerHTML = text;
        return txt.value;
    }
    var ufSelecionada = $("#UF").data("uf");
    var municipioSelecionado = decodeHTMLEntities($("#Municipio").data("municipio"));

    if (ufSelecionada) {
        $("#UF").val(ufSelecionada);
        $.getJSON("https://servicodados.ibge.gov.br/api/v1/localidades/estados/" + ufSelecionada + "/municipios", function (data) {
            var items = '<option value="">Selecione o Município</option>';
            $.each(data, function (i, municipio) {
                items += '<option value="' + municipio.nome + '" data-codigo="' + municipio.id + '">' + municipio.nome + '</option>';
            });
            $("#Municipio").html(items);

            municipioSelecionado = $.trim(municipioSelecionado);
            var municipioEncontrado = $("#Municipio option").filter(function () {
                return $(this).text().trim() === municipioSelecionado;
            });

            if (municipioEncontrado.length > 0) {
                municipioEncontrado.prop('selected', true);
                console.log("Município encontrado e selecionado: " + municipioSelecionado);
            } else {
                console.log("Município não encontrado: " + municipioSelecionado);
            }
        });
    }

    $("#UF").change(function () {
        var uf = $(this).val();
        if (uf) {
            $.getJSON("https://servicodados.ibge.gov.br/api/v1/localidades/estados/" + uf + "/municipios", function (data) {
                var items = '<option value="">Selecione o Município</option>';
                $.each(data, function (i, municipio) {
                    items += '<option value="' + municipio.nome + '" data-codigo="' + municipio.id + '">' + municipio.nome + '</option>';
                });
                $("#Municipio").html(items);
            });
        } else {
            $("#Municipio").html('<option value="">Selecione o Município</option>');
        }
    });

    $("#Municipio").change(function () {
        var codigo = $('option:selected', this).data('codigo');
        $("#CodigoMunicipio").val(codigo);
    });
});
