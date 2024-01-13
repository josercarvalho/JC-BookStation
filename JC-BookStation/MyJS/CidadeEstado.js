$(document).ready(function() {

    $("#ddlEstados").change(function() {
        var estadoId = $(this).val();
        //var url = '@Url.Action("LoadCidadeId", "Cliente")';
        var url = '/Cliente/LoadCidadeId/';
        $.getJSON(url, { estadoId: estadoId },
            function(estadosData) {
                $("#ddlCidades :gt(0)").remove();
                var select = $("#ddlCidades");
                select.attr('disabled', false);
                select.empty();
                select.append($('<option/>', {
                    value: 0,
                    text: "Selecione a Cidade... "
                }));
                $.each(estadosData, function(index, itemData) {
                    //alert(estadosData);
                    //alert(itemData);
                    select.append($('<option/>', {
                        value: itemData.Value,
                        text: itemData.Text
                    }));
                });
            });
    });
})