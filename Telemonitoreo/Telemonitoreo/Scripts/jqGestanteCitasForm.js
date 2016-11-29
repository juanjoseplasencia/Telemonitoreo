function CallbackResult() {
    if (document.getElementById("GesKey").value > 0) {
        document.getElementById("GestanteKey").value = document.getElementById("GesKey").value;
        if (document.getElementById("GesNombres")) {
            document.getElementById("NombresGestante").value = document.getElementById("GesNombres").value;
        }
        if (document.getElementById("GesAPaterno")) {
            document.getElementById("APaternoGestante").value = document.getElementById("GesAPaterno").value;
        }
        if (document.getElementById("GesAMaterno")) {
            document.getElementById("AMaternoGestante").value = document.getElementById("GesAMaterno").value;
        }
    }
    else {
        $("#GestanteKey").parent().next().html('<span for="GestanteNroDocumento" class="">No existe gestante registrada para el DNI ingresado.</span>');
        document.getElementById("NombresGestante").value = '';
        document.getElementById("APaternoGestante").value = '';
        document.getElementById("AMaternoGestante").value = '';
        document.getElementById("GestanteKey").value = 0;
    }
}

function RunAction() {
    if ($("#GestanteNroDocumento").val().length != 8) {
        alertDialog("Debe ingresar el DNI (8 digitos) para obtener datos de la gestante a registrar.", 200);
    }
    else {
        $.ajax({
            url: getControllerURL("GestanteCita") + "/GetGestanteData?dni=" + $("#GestanteNroDocumento").val(),
            type: this.method,
            success: function (result) {
                $("#ajaxpartialtarget").html(result);
                CallbackResult();
                return false;
            }
        });
    }
}

$(function () {
    //Validacion de solamente digitos
    $("#GestanteNroDocumento").keypress(function (e) {
        //if the letter is not digit don't type anything
        if (e.which !== 8 && e.which !== 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });

    $("#FechaCita").datepicker("option", "minDate", "0");
    if ($("#btnConsultar").length == 0)
        RunAction();
});
