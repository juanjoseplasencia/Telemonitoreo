function UsarResultados() {
    if (document.getElementById("GesKey").value > 0) {
        document.getElementById("KeyGestante").value = document.getElementById("GesKey").value;
        if (document.getElementById("GesNombres")) {
            document.getElementById("NombresGestante").value = document.getElementById("GesNombres").value;
        }
        if (document.getElementById("GesAPaterno")) {
            document.getElementById("APaternoGestante").value = document.getElementById("GesAPaterno").value;
        }
        if (document.getElementById("GesAMaterno")) {
            document.getElementById("AMaternoGestante").value = document.getElementById("GesAMaterno").value;
        }
        $("#KeyGestante").parent().next().empty();
    }
    else {
        $("#KeyGestante").parent().next().html('<span for="KeyGestante" class="">No existe gestante registrada para el DNI ingresado.</span>');
        document.getElementById("NombresGestante").value = '';
        document.getElementById("APaternoGestante").value = '';
        document.getElementById("AMaternoGestante").value = '';
        document.getElementById("KeyGestante").value = '';
    }
}

function BuscarGestante() {
    var strDni = $("#GestanteDni").val();
    if (strDni.length !== 8) {
        alertDialog("Debe ingresar el DNI (8 digitos) para obtener datos de la gestante a registrar.", 200);
        return false;
    }
    $.ajax({
        url: "GetGestanteData?dni=" + strDni,
        type: this.method,
        success: function (result) {
            $("#ajaxpartialtarget").html(result);
            UsarResultados();
            return false;
        }
    });
    return false;
}

//Añadir medicamentos
function AddMedicina() {
    if ($("#MedicamentoId").val() === "" || $("#Descripcion").val() === "" || $("#Dosis").val() === "" || $("#Dias").val() === ""
        || $("#Cantidad").val() === "" || $("#Instrucciones").val() === "") {
        alert("Debe seleccionar todos los datos para el medicamento a agregar.");
        return false;
    }

    if ($("#Cantidad").val() === "0" || $("#Dias").val() === "0") {
        alert("La frecuencia y número de días no puede ser menor a 1.");
        return false;
    }

    //validar existencia de medicamento
    var oTable = $(".tbl").dataTable().fnGetData();

    if (oTable.length > 0) {
        var idMed = $("#MedicamentoId").val();
        for (var i = 0; i < oTable.length; i++) {
            if (idMed === oTable[i][0]) {
                alert("El medicamento ya se encuentra en la lista.");
                return false;
            }
        }
    }

    $(".tbl").dataTable().fnAddData([$("#MedicamentoId").val(), $("#Descripcion").val(), $("#Dosis").val(), $("#Dias").val(),
        $("#Cantidad").val(), $("#Instrucciones").val()]);

    $("#MedicamentoId").val("");
    $("#Descripcion").val("");
    $("#Dosis").val("");
    $("#Dias").val("");
    $("#Cantidad").val("");
    $("#Instrucciones").val("");
    return false;
}

//Añadir medicamentos - modo edicion
function AddMedicinaEdit() {
    if ($("#MedicamentoId").val() === "" || $("#Descripcion").val() === "" || $("#Dosis").val() === "" || $("#Dias").val() === ""
        || $("#Cantidad").val() === "" || $("#Instrucciones").val() === "") {
        alert("Debe seleccionar todos los datos para el medicamento a agregar.");
        return false;
    }

    if ($("#Cantidad").val() === "0" || $("#Dias").val() === "0") {
        alert("La frecuencia y número de días no puede ser menor a 1.");
        return false;
    }

    //validar existencia de medicamento
    var oTable = $(".tbl").dataTable().fnGetData();

    if (oTable.length > 0) {
        var idMed = $("#MedicamentoId").val();
        for (var i = 0; i < oTable.length; i++) {
            if (idMed === oTable[i][1]) {
                alert("El medicamento ya se encuentra en la lista.");
                return false;
            }
        }
    }

    $(".tbl").dataTable().fnAddData(["0", $("#MedicamentoId").val(), $("#Descripcion").val(), $("#Dosis").val(), $("#Dias").val(),
        $("#Cantidad").val(), $("#Instrucciones").val()]);

    $("#MedicamentoId").val("");
    $("#Descripcion").val("");
    $("#Dosis").val("");
    $("#Dias").val("");
    $("#Cantidad").val("");
    $("#Instrucciones").val("");
    return false;
}

//Eliminar medicamento de la lista
function DeleteMedicina() {

    //Usado para seleccionar la fila
    var oTt = TableTools.fnGetInstance("tbl"); // Get Table instance 
    var sRow = oTt.fnGetSelected(); // Get Selected Item From Table 

    /*
    //Colocar los valores para ser editados en caso sea necesario
    $('#ItemName').val($.trim(sRow[0].cells[0].innerHTML.toString()));
    $('#Qty').val(jQuery.trim(sRow[0].cells[1].innerHTML.toString()));
    $('#UnitPrice').val($.trim(sRow[0].cells[2].innerHTML.toString()));
    */
    if (sRow && sRow[0]) {
        $(".tbl").dataTable().fnDeleteRow(sRow[0]);
    }
}

//Enviar al servidor en formato JSON
function GuardarMed() {
    //Crear la parte principal del formulario
    var medicamentos = {
        "GestanteMedicamentoId": "", "MedicamentoId": "", "Descripcion": "", "Dosis": "", "Dias": "", "Cantidad": "",
        "Instrucciones": ""
    };
    //Crear la parte secundaria
    var asignacion = {
        "GestanteMedicamentoId": "", "GestanteKey": "", "Fecha": "", "NombreMedico": "", "EstablecimientoId": "",
        "Medicamentos": []
    };
    //Asignar el contenido principal
    var ul = $(".validation-summary-valid ul");
    ul.html("");
    var error = false;
    if ($("#KeyGestante").val() === "") {
        ul.append("<li>" + "Debe seleccionar la gestante." + "</li>");
        error = true;
    }
    if ($("#Fecha").val() === "") {
        ul.append("<li>" + "Seleccione una fecha para los medicamentos." + "</li>");
        error = true;
    }
    if ($("#NombreMedico").val() === "") {
        ul.append("<li>" + "Ingrese un nombre para le médico que receta." + "</li>");
        error = true;
    }
    if ($("#EstablecimientoId").val() === "") {
        ul.append("<li>" + "Seleccione un establecimiento." + "</li>");
        error = true;
    }

    if (error) {
        //$(window).scrollTop($(".validation-summary-valid").offset().top);
        $("html, body").animate({ scrollTop: $(".validation-summary-valid").offset().top }, "slow");
        return false;
    }

    asignacion.GestanteMedicamentoId = "0";
    asignacion.GestanteKey = $("#KeyGestante").val();
    asignacion.Fecha = $("#Fecha").val();
    asignacion.NombreMedico = $("#NombreMedico").val();
    asignacion.EstablecimientoId = $("#EstablecimientoId").val();
    //Obtener los datos de la tabla conteniendo los registros secundarios
    var oTable = $(".tbl").dataTable().fnGetData();

    if (oTable.length < 1) {
        ul.append("<li>" + "No hay contenido de medicamentos." + "</li>");
        $("html, body").animate({ scrollTop: $(".validation-summary-valid").offset().top }, "slow");
        return false;
    }

    for (var i = 0; i < oTable.length; i++) {
        //Asignar valores de medicamentos
        medicamentos.GestanteMedicamentoId = 0;
        medicamentos.MedicamentoId = oTable[i][0];
        medicamentos.Descripcion = "";
        medicamentos.Dosis = oTable[i][2];
        medicamentos.Dias = oTable[i][3];
        medicamentos.Cantidad = oTable[i][4];
        medicamentos.Instrucciones = oTable[i][5];
        //Añadir elementos a la lista
        asignacion.Medicamentos.push(medicamentos);
        medicamentos = {
            "GestanteMedicamentoId": "", "MedicamentoId": "", "Descripcion": "", "Dosis": "", "Dias": "", "Cantidad": "",
            "Instrucciones": ""
        };
    }

    //Enviar la informacion al servidor para salvar
    $.ajax({
        url: "Crear",
        data: JSON.stringify(asignacion),
        type: "POST",
        contentType: "application/json;",
        dataType: "json",
        success: function (result) {

            if (result.Success == "1") {
                window.location.href = "index";
            }
            else {
                alert(result.ex);
            }
        }
    });
    return false;
}

//Enviar al servidor en formato JSON - modo edicion
function GuardarMedEdit() {
    //Crear la parte principal del formulario
    var medicamentos = {
        "GestanteMedicamentoId": "", "GestanteMedicamentoDetalleId": "", "MedicamentoId": "", "Descripcion": "",
        "Dosis": "", "Dias": "", "Cantidad": "", "Instrucciones": ""
    };
    //Crear la parte secundaria
    var asignacion = {
        "GestanteMedicamentoId": "", "GestanteKey": "", "Fecha": "", "NombreMedico": "", "EstablecimientoId": "",
        "Medicamentos": []
    };
    //Asignar el contenido principal
    var ul = $(".validation-summary-valid ul");
    ul.html("");
    var error = false;
    if ($("#KeyGestante").val() === "") {
        ul.append("<li>" + "Debe seleccionar la gestante." + "</li>");
        error = true;
    }
    if ($("#Fecha").val() === "") {
        ul.append("<li>" + "Seleccione una fecha para los medicamentos." + "</li>");
        error = true;
    }
    if ($("#NombreMedico").val() === "") {
        ul.append("<li>" + "Ingrese un nombre para le médico que receta." + "</li>");
        error = true;
    }
    if ($("#EstablecimientoId").val() === "") {
        ul.append("<li>" + "Seleccione un establecimiento." + "</li>");
        error = true;
    }

    if (error) {
        //$(window).scrollTop($(".validation-summary-valid").offset().top);
        $("html, body").animate({ scrollTop: $(".validation-summary-valid").offset().top }, "slow");
        return false;
    }
    asignacion.GestanteMedicamentoId = $("#GestanteMedicamentoId").val();
    asignacion.GestanteKey = $("#KeyGestante").val();
    asignacion.Fecha = $("#Fecha").val();
    asignacion.NombreMedico = $("#NombreMedico").val();
    asignacion.EstablecimientoId = $("#EstablecimientoId").val();

    //Obtener los datos de la tabla conteniendo los registros secundarios
    var oTable = $(".tbl").dataTable().fnGetData();

    if (oTable.length < 1) {
        ul.append("<li>" + "No hay contenido de medicamentos." + "</li>");
        $("html, body").animate({ scrollTop: $(".validation-summary-valid").offset().top }, "slow");
        return false;
    }

    for (var i = 0; i < oTable.length; i++) {
        //Asignar valores de medicamentos
        medicamentos.GestanteMedicamentoId = $("#GestanteMedicamentoId").val();
        medicamentos.GestanteMedicamentoDetalleId = oTable[i][0];
        medicamentos.MedicamentoId = oTable[i][1];
        medicamentos.Descripcion = "";
        medicamentos.Dosis = oTable[i][3];
        medicamentos.Dias = oTable[i][4];
        medicamentos.Cantidad = oTable[i][5];
        medicamentos.Instrucciones = oTable[i][6];
        //Añadir elementos a la lista
        asignacion.Medicamentos.push(medicamentos);
        medicamentos = {
            "GestanteMedicamentoId": "", "GestanteMedicamentoDetalleId": "", "MedicamentoId": "", "Descripcion": "",
            "Dosis": "", "Dias": "", "Cantidad": "", "Instrucciones": ""
        };
    }
    //Enviar la informacion al servidor para salvar
    $.ajax({
        url: getControllerURL("GestanteMedicamento") + "/Edit",
        data: JSON.stringify(asignacion),
        type: "POST",
        contentType: "application/json;",
        dataType: "json",
        success: function (result) {

            if (result.Success == "1") {
                window.location.href = getControllerURL("GestanteMedicamento") + "/index";
            }
            else {
                alert(result.ex);
            }
        }
    });

}

$(document).ready(function () {
    //Validacion de solamente digitos
    $("#GestanteDni").keypress(function (e) {
        //if the letter is not digit don't type anything
        if (e.which !== 8 && e.which !== 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });

    $("#Cantidad").keypress(function (e) {
        //if the letter is not digit don't type anything
        if (e.which !== 8 && e.which !== 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });

    $("#Dias").keypress(function (e) {
        //if the letter is not digit don't type anything
        if (e.which !== 8 && e.which !== 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });

    //Usando datatables.js (jQuery Data Table)
    $(".tbl").dataTable({
        "sDom": 'T<"clear">lfrtip',
        "oTableTools": {
            "aButtons": [],
            "sRowSelect": "single"
        },
        "bLengthChange": false,
        "bFilter": false,
        "bSort": false,
        "bInfo": false
    });
    var oTable = $(".tbl").dataTable();

    //Autocompletar medicinas
    $("#Descripcion").autocomplete({
        delay: 700,
        height: 100,
        minLength: 5,
        source: function (request, response) {
            var params = {
                medicamento: request.term
            };
            $.ajax({
                type: "GET",
                url: getControllerURL("GestanteMedicamento") + "/GetMedicamentosPorNombre",
                data: { medicamento: request.term },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.Descripcion + "-" + item.Concentracion,
                            value: item.MedicamentoId,
                            nombre: item.Descripcion,
                            dosis: item.Concentracion
                        }
                    }));
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert("No se pudo encontrar el medicamento.");
                }
            });
        },
        focus: function (event, ui) {
            $("#Descripcion").val(ui.item.nombre);
            return false;
        },
        select: function (event, ui) {
            $("#Descripcion").val(ui.item.nombre);
            $("#MedicamentoId").val(ui.item.value);
            $("#Dosis").val(ui.item.dosis);
            return false;
        }
    });
});