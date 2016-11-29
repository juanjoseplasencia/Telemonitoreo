
function setUpGrid(sortcolumn, sortorder, rowsperpage) {
    $("#gridView").jqGrid({
        height: 'auto',
        autowidth: true,
        styleUI: 'Bootstrap',
        scrollrows: false,
        hidegrid: false,
        jsonReader: { repeatitems: false, id: "UsuarioKey" },
        colNames: ['ID del Registro', 'DNI', 'Nombres', 'Apellido Paterno', 'Apellido Materno', 'Est. de Salud', 'Rol Asignado', 'Estado'],
        colModel: [
            { name: 'UsuarioKey', index: 'UsuarioKey', key: true },
            { name: 'UserName', index: 'UserName', formatter: 'showlink', formatoptions: { baseLinkUrl: 'javascript:', showAction: "LinkEdit('", addParam: "');" } },
            { name: 'Nombres', index: 'Nombres' },
            { name: 'APaterno', index: 'APaterno' },
            { name: 'AMaterno', index: 'AMaterno' },
            { name: 'Establecimiento', index: 'Establecimiento' },
            { name: 'RoleName', index: 'RoleName' },
            { name: 'Estado', index: 'Estado' }
        ],
        url: getControllerURL('Account') + "/BuscarUsuarios",
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
            "estado": function () {
                return $('#Estado').val();
            },
            "establecimiento": function () {
                return $('#EstablecimientoId').val();
            },
            "rol": function () {
                return $('#RoleName').val();
            }
        },
        rowNum: rowsperpage,
        rowList: [30, 60, 1000000],
        page: 1,
        pager: jQuery("#pager"),
        pagerpos: 'center',
        sortname: sortcolumn,
        sortorder: sortorder,
        viewrecords: true,
        multiselect: true,
        emptyrecords: 'No se encontraron usuarios'
    });
    jQuery("#gridView").jqGrid('navGrid', '#pager', { edit: false, add: false, del: false, search: false, refresh: false });
}

function buscar() {
    $("#gridView").trigger("reloadGrid", [{ page: 1 }]);
}

function exportarExcel() {
    $("#hdSortColumn").val($("#gridView").jqGrid('getGridParam', 'sortname'));
    $("#hdSortDirection").val($("#gridView").jqGrid('getGridParam', 'sortorder'));
    $("#ListadoUsuarios").submit();
}

$(function () {
    setUpGrid("UsuarioKey", "asc", 30);
    var x = $('select.ui-pg-selbox option').eq(2);
    x.text("Show All");
    $("#btnBuscar").on("click", buscar);
    $("#btnEliminar").on("click", eliminar);
    $("#btnExportarExcel").on("click", exportarExcel);
});

function LinkEdit(id) {
    var row = id.split("=");
    var rowId = row[1];
    window.location.href = getControllerURL('Account') + "/Edit/" + rowId;
}

function eliminar() {
    var selectedIds = $("#gridView").jqGrid('getGridParam', 'selarrrow');
    if (selectedIds.length > 0) {
        confirmDialog("Esta seguro que desea eliminar las filas seleccionadas?",
        function () {
            $.ajax({
                type: "POST",
                url: getControllerURL("Account") + "/Eliminar",
                data: { "ids": selectedIds },
                dataType: "json",
                success: function (data) {
                    if (data == true)
                        buscar();
                    else
                        alertDialog(data, 310);
                },
                error: function () {
                    alertDialog("Ocurrió un error al intentar eliminar las filas seleccionadas");
                },
                traditional: true
            });
        },
        function () {
            return false;
        });
    }
    else {
        alertDialog("Debe seleccionar al menos una fila para eliminar", 220);
        return false;
    }
}
