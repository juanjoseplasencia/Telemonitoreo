//Añadir mensajes educacionales
function AddContenido() {
    if ($("#Dia").val() === "" || $("#Contenido").val() === "") {
        alert("Debe seleccionar todos los datos para contenido del mensaje.");
        return false;
    }

    //Validar longitud del mensaje
    var contenido = $("#Contenido").val().trim();
    if (contenido.length > 140) {
        alert("El mensaje no debe tener mas de 140 caracteres.");
        return false;
    }

    $(".tbl").dataTable().fnAddData([$("#Dia option:selected").text(), $("#Contenido").val()]);

    $("#Dia").val("");
    $("#Contenido").val("");
    return false;
}

//Añadir mensajes educacionales - modo edicion
function AddContenidoEdit() {
    if ($("#Dia").val() === "" || $("#nContenido").val() === "") {
        alert("Debe seleccionar todos los datos para contenido del mensaje.");
        return false;
    }

    //Validar longitud del mensaje
    var contenido = $("#nContenido").val().trim();
    if (contenido.length > 140) {
        alert("El mensaje no debe tener mas de 140 caracteres.");
        return false;
    }

    // Adding item to table
    $(".tbl").dataTable().fnAddData(["0", $("#Dia option:selected").text(), $("#nContenido").val()]);

    $("#Dia").val("");
    $("#nContenido").val("");
    return false;
}

//Eliminar mensaje educacional de la lista
function DeleteContenido() {
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
function GuardarContenido() {
    //Crear la parte principal del formulario
    var mensaje = { "IdMensajeEducacional": "", "SemanaEmbarazo": "", "Contenido": [] };
    //Crear la parte secundaria
    var mensajeContenido = { "IdMensajeEducacional": "", "DiaSemana": "", "Contenido": "" };
    //Asignar el contenido principal
    var ul;
    if ($("#SemanaEmbarazo").val() === "") {
        ul = $(".validation-summary-valid ul");
        ul.html("");
        ul.append("<li>" + "Debe ingresar una semana de embarazo para los mensajes." + "</li>");
        return false;
    }
    mensaje.IdMensajeEducacional = "0";
    mensaje.SemanaEmbarazo = $("#SemanaEmbarazo").val();
    //Obtener los datos de la tabla conteniendo los registros secundarios
    var oTable = $(".tbl").dataTable().fnGetData();

    if (oTable.length < 3) {
        ul = $(".validation-summary-valid ul");
        ul.html("");
        ul.append("<li>" + "Debe registrar al menos 3 mensajes para la semana." + "</li>");
        return false;
    }

    for (var i = 0; i < oTable.length; i++) {
        //Asignar valores de mensajes educacionales
        mensajeContenido.IdMensajeEducacional = 0;
        mensajeContenido.DiaSemana = ObtenerDia(oTable[i][0]);
        mensajeContenido.Contenido = oTable[i][1];
        //Añadir elementos a la lista
        mensaje.Contenido.push(mensajeContenido);
        mensajeContenido = { "IdMensajeEducacional": "", "DiaSemana": "", "Contenido": "" };
    }

    //Enviar la informacion al servidor para salvar
    $.ajax({
        url: "Crear",
        data: JSON.stringify(mensaje),
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
function GuardarContenidoEdit() {
    //Crear la parte principal del formulario
    var mensaje = { "IdMensajeEducacional": "", "SemanaEmbarazo": "", "Contenido": [] };
    //Crear la parte secundaria
    var mensajeContenido = { "IdMensajeEducacional": "", "IdContenidoMensajeEducacional": "", "DiaSemana": "", "Contenido": "" };
    //Asignar el contenido principal
    var ul;
    if ($("#SemanaEmbarazo").val() === "") {
        ul = $(".validation-summary-valid ul");
        ul.html("");
        ul.append("<li>" + "Debe ingresar una semana de embarazo para los mensajes." + "</li>");
        return false;
    }
    mensaje.IdMensajeEducacional = $("#IdMensajeEducacional").val();
    mensaje.SemanaEmbarazo = $("#SemanaEmbarazo").val();

    //Obtener los datos de la tabla conteniendo los registros secundarios
    var oTable = $(".tbl").dataTable().fnGetData();

    if (oTable.length < 3) {
        ul = $(".validation-summary-valid ul");
        ul.html("");
        ul.append("<li>" + "Debe registrar al menos 3 mensajes para la semana." + "</li>");
        return false;
    }

    for (var i = 0; i < oTable.length; i++) {
        //Asignar valores de mensajes educacionales
        mensajeContenido.IdMensajeEducacional = $("#IdMensajeEducacional").val();
        mensajeContenido.IdContenidoMensajeEducacional = oTable[i][0];
        mensajeContenido.DiaSemana = ObtenerDia(oTable[i][1]);
        mensajeContenido.Contenido = oTable[i][2];
        //Añadir elementos a la lista
        mensaje.Contenido.push(mensajeContenido);
        mensajeContenido = { "IdMensajeEducacional": "", "IdContenidoMensajeEducacional": "", "DiaSemana": "", "Contenido": "" };
    }

    //Enviar la informacion al servidor para salvar
    $.ajax({
        url: getControllerURL("MensajeEducacional") + "/Edit",
        data: JSON.stringify(mensaje),
        type: "POST",
        contentType: "application/json;",
        dataType: "json",
        success: function (result) {

            if (result.Success == "1") {
                window.location.href = getControllerURL("MensajeEducacional") + "/index";
            } else {
                alert(result.ex);
            }
        }
    });
    return false;
}

function ObtenerDia(textodia) {
    //textodia = textodia.toLowerCase();
    switch (textodia) {
        case DiaUno:
            return 1;
        case DiaDos:
            return 2;
        case DiaTres:
            return 3;
        case DiaCuatro:
            return 4;
        case DiaCinco:
            return 5;
        case DiaSeis:
            return 6;
        default:
            return 7;
    }
}

$(document).ready(function () {
    //Validacion de solamente digitos
    $("#SemanaEmbarazo").keypress(function (e) {
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
});