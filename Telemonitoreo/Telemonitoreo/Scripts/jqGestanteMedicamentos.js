function setUpGrid(sortcolumn, sortorder, rowsperpage) {
    jQuery("#sg1").jqGrid({
        url: getControllerURL('GestanteMedicamento') + "/BuscarGestanteMedicamentos",
        height: 'auto',
        autowidth: true,
        styleUI: 'Bootstrap',
        datatype: 'json',
        postData: {
            "numDni": function () {
                return $('#NroDocumento').val();
            },
            "aPaterno": function () {
                return $('#APaterno').val();
            },
            "aMaterno": function () {
                return $('#AMaterno').val();
            },
            "establecimiento": function () {
                return $('#EstablecimientoId').val();
            },
            "fechaIni": function () {
                return $('#FechaI').val();
            },
            "fechaFin": function () {
                return $('#FechaF').val();
            }
        },
        colNames: ['ID de Registro', 'DNI de Gestante', 'Nombre', 'Ape. Paterno', 'Ape. Materno', 'Establecimiento', 'Fecha de Asignación'],
        colModel: [
   		    { name: 'GestanteMedicamentoId', index: 'GestanteMedicamentoId', key: true},
   		    { name: 'GestanteDni', index: 'GestanteDni', formatter: 'showlink', formatoptions: { baseLinkUrl: 'javascript:', showAction: "LinkEdit('", addParam: "');" } },
   		    { name: 'GestanteNombres', index: 'GestanteNombres' },
   		    { name: 'GestanteAPaterno', index: 'GestanteAPaterno', align: "right" },
   		    { name: 'GestanteAMaterno', index: 'GestanteAMaterno', align: "right" },
   		    { name: 'Establecimiento', index: 'Establecimiento', align: "right" },
   		    { name: 'Fecha', index: 'Fecha', formatter: 'date', formatoptions: { srcformat: 'd/m/Y', newformat: 'd/m/Y' }, sortable: false, width: 140 }
        ],
        rowNum: rowsperpage,
        rowList: [8, 10, 20, 30],
        pager: '#psg1',
        sortname: sortcolumn,
        sortorder: sortorder,
        viewrecords: true,
        multiselect: true,
        subGrid: true,
        //caption: "Custom Icons in Subgrid",
        // define the icons in subgrid
        //subGridOptions: {
        //    "plusicon": "ui-icon-triangle-1-e",
        //    "minusicon": "ui-icon-triangle-1-s",
        //    "openicon": "ui-icon-arrowreturn-1-e"
        //},
        subGridRowExpanded: function (subgrid_id, row_id) {
            var subgridTableId = subgrid_id + "_t";
            var pagerId = "p_" + subgridTableId;
            $("#" + subgrid_id).html("<table id='" + subgridTableId + "' class='scroll'></table><div id='" + pagerId + "' class='scroll'></div>");
            jQuery("#" + subgridTableId).jqGrid({
                url: getControllerURL('GestanteMedicamento') + "/BuscarGestanteMedDetalle?idGMed=" + row_id,
                datatype: 'json',
                colNames: ['ID de Detalle', 'Medicamento', 'Dosis'],
                colModel: [
				    { name: "GestanteMedicamentoDetalleId", index: "GestanteMedicamentoDetalleId", key: true },
				    { name: "Medicamento", index: "Medicamento" },
				    { name: "Dosis", index: "Dosis", align: "right" }
                ],
                rowNum: 20,
                pager: pagerId,
                sortname: 'GestanteMedicamentoDetalleId',
                sortorder: "asc",
                height: '100%',
                rowList: [],
                pgbuttons: false,
                pgtext: null,
                viewrecords: false
            });
            jQuery("#" + subgridTableId).jqGrid('navGrid', "#" + pagerId, { edit: false, add: false, del: false, search: false, refresh: false });
        }
    });
    jQuery("#sg1").jqGrid('navGrid', '#psg1', { edit: false, add: false, del: false, search: false, refresh: false });
}

function buscar() {
    $("#sg1").trigger("reloadGrid", [{ page: 1 }]);
}

$(function () {
    setUpGrid("GestanteMedicamentoId", "asc", 30);
    var x = $('select.ui-pg-selbox option').eq(2);
    x.text("Show All");
    $("#btnBuscar").on("click", buscar);
    $("#btnEliminar").on("click", deshabilitarAsignacion);
});

function LinkEdit(id) {
    var row = id.split("=");
    var rowId = row[1];
    window.location.href = getControllerURL('GestanteMedicamento') + "/Edit/" + rowId;
}

function deshabilitarAsignacion() {
    var selectedIds = $("#sg1").jqGrid('getGridParam', 'selarrrow');
    if (selectedIds.length > 0) {
        confirmDialog("Esta seguro que desea deshabilitar las filas seleccionadas?",
        function () {
            $.ajax({
                type: "POST",
                url: getControllerURL("GestanteMedicamento") + "/Eliminar",
                data: { "ids": selectedIds },
                dataType: "json",
                success: function (data) {
                    if (data == true)
                        buscar();
                    else
                        alertDialog(data, 310);
                },
                error: function () {
                    alertDialog("Ocurrió un error al intentar deshabilitar las filas seleccionadas");
                },
                traditional: true
            });
        },
        function () {
            return false;
        });
    }
    else {
        alertDialog("Debe seleccionar al menos una fila para deshabilitar", 220);
        return false;
    }
}