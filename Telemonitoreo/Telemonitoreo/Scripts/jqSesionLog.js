
function setUpGrid(sortcolumn, sortorder, rowsperpage) {
    $("#gridView").jqGrid({
        height: 'auto',
        autowidth: true,
        styleUI: 'Bootstrap',
        scrollrows: false,
        hidegrid: false,
        jsonReader: { repeatitems: false, id: "RegistroEventoKey" },
        colNames: ['ID Sesión', 'DNI', 'Nombres', 'A. Paterno', 'A. Materno', 'Acción', 'Objeto/Menú', 'ID de Registro', 'Fecha', 'IP'],
        colModel: [
            { name: 'RegistroEventoKey', index: 'RegistroEventoKey', key: true, width: 100 },
            { name: 'Dni', index: 'Dni', width: 80 },
            { name: 'Nombre', index: 'Nombre', width: 200, sortable: false },
            { name: 'APaterno', index: 'APaterno', width: 150 },
            { name: 'AMaterno', index: 'AMaterno', width: 150 },
            { name: 'Accion', index: 'Accion', width: 200 },
            { name: 'Menu', index: 'Menu', width: 250 },
            { name: 'IdRegistro', index: 'IdRegistro', sortable: false,  width: 110 },
            { name: 'EventoFecha', index: 'EventoFecha', formatter: 'date', formatoptions: { srcformat: 'd/m/Y H:i:s', newformat: 'd/m/Y H:i:s' }, sortable: false, width: 140 },
            { name: 'Origen', index: 'Origen', width: 75, sortable: false }
        ],
        url: getControllerURL('SesionLog') + "/BuscarSesiones",
        datatype: 'json',
        postData: {
            "numDni": function () {
                return $('#NroDni').val();
            },
            "aPaterno": function () {
                return $('#APaterno').val();
            },
            "aMaterno": function () {
                return $('#AMaterno').val();
            },
            "fechaIni": function () {
                return $('#FechaI').val();
            },
            "fechaFin": function () {
                return $('#FechaF').val();
            },
            "menu": function () {
                return $('#Menu').val();
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
        emptyrecords: 'No se encontraron sesiones'
    });
    jQuery("#gridView").jqGrid('navGrid', '#pager', { edit: false, add: false, del: false, search: false, refresh: false });
}

function buscar() {
    $("#gridView").trigger("reloadGrid", [{ page: 1 }]);
}

function exportarExcel() {
    $("#hdSortColumn").val($("#gridView").jqGrid('getGridParam', 'sortname'));
    $("#hdSortDirection").val($("#gridView").jqGrid('getGridParam', 'sortorder'));
    $("#ListadoSesiones").submit();
}

$(function () {
    setUpGrid("RegistroEventoKey", "asc", 30);
    var x = $('select.ui-pg-selbox option').eq(2);
    x.text("Show All");
    $("#btnBuscar").on("click", buscar);
    $("#btnExportarExcel").on("click", exportarExcel);
});