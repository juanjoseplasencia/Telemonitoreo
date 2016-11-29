function calcularFechaProbableParto(year, month, day) {
    var date = new Date(new Date(year, month, day) - 85 * 24 * 60 * 60 * 1000);
    return new Date(date.getFullYear() + 1, date.getMonth(), date.getDate());
}
function calcularEdad(year, month, day) {
    var now = new Date();
    var today = new Date(now.getFullYear(), now.getMonth(), now.getDate());
    var miliseconds = today - new Date(year, month, day);
    var edad = Math.floor(miliseconds / 1000 / 60 / 60 / 24 / 365);
    return edad >= 0 ? edad : 0;
}
function getinfogestante() {
    if ($("#GestanteNroDocumento").val().length != 8) {
        alertDialog("Debe ingresar el DNI (8 digitos) para obtener datos de la gestante a registrar.", 200);
    }
    else {
        $.ajax({
            url: "GetReniecData?dni=" + $("#GestanteNroDocumento").val(),
            type: 'GET',
            cache: false,
            success: function (result) {
                $("#ajaxpartialtarget").html(result);
                setinfogestante();
                return false;
            }
        });
    }
}
function clearinfogestante() {
    document.getElementById("Nombres").value = '';
    document.getElementById("APaterno").value = '';
    document.getElementById("AMaterno").value = '';
    document.getElementById("FechaNacimiento").value = '';
    document.getElementById("Edad").value = '';
    document.getElementById("GestanteDireccion").value = '';
}
function setinfogestante() {
    if (document.getElementById('ReniecError')) {
        $("#GestanteNroDocumento").parent().next().html('<span for="GestanteNroDocumento" class="">' + document.getElementById('ReniecError').value + '</span>');
        clearinfogestante();
    }
    else if (document.getElementById('ReniecGeneroId').value == '1') {
        $("#GestanteNroDocumento").parent().next().html('<span for="GestanteNroDocumento" class="">El DNI ingresado corresponde a un paciente de genero masculino.</span>');
        clearinfogestante();
        }
        else {
            if (document.getElementById('ReniecNombres')) {
                document.getElementById('Nombres').value = document.getElementById('ReniecNombres').value;
            }

            if (document.getElementById('ReniecAPaterno')) {
                document.getElementById('APaterno').value = document.getElementById('ReniecAPaterno').value;
            }

            if (document.getElementById('ReniecAMaterno')) {
                document.getElementById('AMaterno').value = document.getElementById('ReniecAMaterno').value;
            }

            if (document.getElementById('ReniecFNacimiento')) {
                document.getElementById('FechaNacimiento').value = document.getElementById('ReniecFNacimiento').value;
                document.getElementById('Edad').focus();
            }

            if (document.getElementById('ReniecDireccion')) {
                document.getElementById('GestanteDireccion').value = document.getElementById('ReniecDireccion').value;
            }
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

    $("#GestanteTelefono").keypress(function (e) {
        //if the letter is not digit don't type anything
        if (e.which !== 8 && e.which !== 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });

    $("#FechaNacimiento").datepicker("option", "maxDate", "0");
    $("#FechaUltimaRegla").datepicker("option", { maxDate: "0", minDate: "-1y" });
    if ($("#GestanteKey").length == 0) {
        $("#FechaUltimaRegla").datepicker("option", "onSelect", function (dateText, inst) {
            $("#FechaProbableParto").datepicker("setDate",
                calcularFechaProbableParto(inst.selectedYear, inst.selectedMonth, inst.selectedDay));
        });
        $("#FechaProbableParto").datepicker("option", "beforeShow", function (input, inst) {
            var date = $("#FechaUltimaRegla").datepicker("getDate");
            if (date !== null) {
                $("#FechaProbableParto").datepicker("setDate",
                    calcularFechaProbableParto(date.getFullYear(), date.getMonth(), date.getDate()));
                $("#FechaProbableParto").datepicker("hide");
            }
        });
    }
    $("#FechaNacimiento").datepicker("option", "onSelect", function (dateText, inst) {
        $("#Edad").val(calcularEdad(inst.selectedYear, inst.selectedMonth, inst.selectedDay));
    });
    $("#Edad").focus(function () {
        var dateTyped = $("#FechaNacimiento").val();
        if (dateTyped !== null) {
            var date = new Date(parseInt(dateTyped.substring(6), 10),
                              parseInt(dateTyped.substring(3, 5), 10) - 1,
                              parseInt(dateTyped.substring(0, 2), 10));
            $("#Edad").val(calcularEdad(date.getFullYear(), date.getMonth(), date.getDate()));
        }
    });

    if ($("#FechaNacimiento").length > 0)
    {
        var dateValue = $("#FechaNacimiento").datepicker("getDate");
        if (dateValue !== null) {
            $("#Edad").val(calcularEdad(dateValue.getFullYear(), dateValue.getMonth(), dateValue.getDate()));
        }
    }
    else if ($("#ddFechaNacimiento").length > 0)
        {
        var dateValue = $.trim($("#ddFechaNacimiento")[0].innerText);
        if (dateValue !== "") {
            var date = new Date(parseInt(dateValue.substring(6), 10),
                               parseInt(dateValue.substring(3, 5), 10) - 1,
                               parseInt(dateValue.substring(0, 2), 10));
            $("#ddEdad").html(calcularEdad(date.getFullYear(), date.getMonth(), date.getDate()));
        }
        }

    //Autocompletar region
    $("#Region").autocomplete({
        delay: 700,
        height: 100,
        minLength: 3,
        source: function (request, response) {
            var params = {
                region: request.term
            };
            $.ajax({
                type: "GET",
                url: getControllerURL("Gestante") + "/GetRegiones",
                data: {
                    region: request.term
                },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.Text,
                            value: item.Value
                        }
                    }));
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert("No se pudo encontrar la region.");
                }
            });
        },
        focus: function (event, ui) {
            $("#Region").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#Region").val(ui.item.label);
            return false;
        }
    });

    //Autocompletar establecimiento
    $("#Establecimiento").autocomplete({
        delay: 700,
        height: 100,
        minLength: 5,
        source: function (request, response) {
            var params = {
                establecimiento: request.term
            };
            $.ajax({
                type: "GET",
                url: getControllerURL("Gestante") + "/GetEstablecimientosPorRegion",
                data: {
                    establecimiento: request.term,
                    region: $("#Region").val()
                },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.Descripcion,
                            value: item.EstablecimientoId
                        }
                    }));
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert("No se pudo encontrar el establecimiento.");
                }
            });
        },
        focus: function (event, ui) {
            $("#Establecimiento").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            //$("#Region").val(ui.item.label2);
            $("#Establecimiento").val(ui.item.label);
            $("#EstablecimientoId").val(ui.item.value);
            return false;
        }
    });

    //Autocompletar diagnostico ingreso
    $("#DiagIngreso").autocomplete({
        delay: 700,
        height: 100,
        minLength: 5,
        source: function (request, response) {
            var params = {
                diagnostico: request.term
            };
            $.ajax({
                type: "GET",
                url: getControllerURL("Gestante") + "/GetDiagnosticosPorNombre",
                data: { diagnostico: request.term },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    $("#DiagIngreso").tooltip("hide");
                    response($.map(data, function (item) {
                        return {
                            label: item.Descripcion,
                            value: item.DiagnosticoId,
                            label2: item.Cie10
                        }
                    }));
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert("No se pudo encontrar el diagnóstico.");
                }
            });
        },
        focus: function (event, ui) {
            $("#DiagIngreso").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#DiagIngreso").tooltip("hide");
            $("#DiagIngresoCie10").val(ui.item.label2);
            $("#DiagIngreso").val(ui.item.label);
            $("#DiagnosticoIngreso").val(ui.item.value);
            return false;
        }
    });

    //Autocompletar diagnostico ingreso CIE10
    $("#DiagIngresoCie10").autocomplete({
        delay: 700,
        height: 100,
        minLength: 2,
        source: function (request, response) {
            var params = {
                diagnosticoCie10: request.term
            };
            $.ajax({
                type: "GET",
                url: getControllerURL("Gestante") + "/GetDiagnosticosPorCie10",
                data: { diagnosticoCie10: request.term },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    $("#DiagIngresoCie10").tooltip("hide");
                    response($.map(data, function (item) {
                        return {
                            label: item.Cie10,
                            value: item.DiagnosticoId,
                            label2: item.Descripcion
                        }
                    }));
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert("No se pudo encontrar el diagnóstico.");
                }
            });
        },
        focus: function (event, ui) {
            $("#DiagIngresoCie10").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#DiagIngresoCie10").tooltip("hide");
            $("#DiagIngresoCie10").val(ui.item.label);
            $("#DiagIngreso").val(ui.item.label2);
            $("#DiagnosticoIngreso").val(ui.item.value);
            return false;
        }
    });

    //Autocompletar diagnostico intermedio 1
    $("#DiagIntermedio1").autocomplete({
        delay: 700,
        height: 100,
        minLength: 5,
        source: function (request, response) {
            var params = {
                diagnostico: request.term
            };
            $.ajax({
                type: "GET",
                url: getControllerURL("Gestante") + "/GetDiagnosticosPorNombre",
                data: { diagnostico: request.term },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    $("#DiagIntermedio1").tooltip("hide");
                    response($.map(data, function (item) {
                        return {
                            label: item.Descripcion,
                            value: item.DiagnosticoId,
                            label2: item.Cie10
                        }
                    }));
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert("No se pudo encontrar el diagnóstico.");
                }
            });
        },
        focus: function (event, ui) {
            $("#DiagIntermedio1").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#DiagIntermedio1").tooltip("hide");
            $("#DiagIntermedio1Cie10").val(ui.item.label2);
            $("#DiagIntermedio1").val(ui.item.label);
            $("#DiagnosticoIntermedio1").val(ui.item.value);
            return false;
        }
    });

    //Autocompletar diagnostico intermedio 1 CIE10
    $("#DiagIntermedio1Cie10").autocomplete({
        delay: 700,
        height: 100,
        minLength: 2,
        source: function (request, response) {
            var params = {
                diagnosticoCie10: request.term
            };
            $.ajax({
                type: "GET",
                url: getControllerURL("Gestante") + "/GetDiagnosticosPorCie10",
                data: { diagnosticoCie10: request.term },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    $("#DiagIntermedio1Cie10").tooltip("hide");
                    response($.map(data, function (item) {
                        return {
                            label: item.Cie10,
                            value: item.DiagnosticoId,
                            label2: item.Descripcion
                        }
                    }));
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert("No se pudo encontrar el diagnóstico.");
                }
            });
        },
        focus: function (event, ui) {
            $("#DiagIntermedio1Cie10").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#DiagIntermedio1Cie10").tooltip("hide");
            $("#DiagIntermedio1Cie10").val(ui.item.label);
            $("#DiagIntermedio1").val(ui.item.label2);
            $("#DiagnosticoIntermedio1").val(ui.item.value);
            return false;
        }
    });

    //Autocompletar diagnostico intermedio 2
    $("#DiagIntermedio2").autocomplete({
        delay: 700,
        height: 100,
        minLength: 5,
        source: function (request, response) {
            var params = {
                diagnostico: request.term
            };
            $.ajax({
                type: "GET",
                url: getControllerURL("Gestante") + "/GetDiagnosticosPorNombre",
                data: { diagnostico: request.term },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    $("#DiagIntermedio2").tooltip("hide");
                    response($.map(data, function (item) {
                        return {
                            label: item.Descripcion,
                            value: item.DiagnosticoId,
                            label2: item.Cie10
                        }
                    }));
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert("No se pudo encontrar el diagnóstico.");
                }
            });
        },
        focus: function (event, ui) {
            $("#DiagIntermedio2").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#DiagIntermedio2").tooltip("hide");
            $("#DiagIntermedio2Cie10").val(ui.item.label2);
            $("#DiagIntermedio2").val(ui.item.label);
            $("#DiagnosticoIntermedio2").val(ui.item.value);
            return false;
        }
    });

    //Autocompletar diagnostico intermedio 2 CIE10
    $("#DiagIntermedio2Cie10").autocomplete({
        delay: 700,
        height: 100,
        minLength: 2,
        source: function (request, response) {
            var params = {
                diagnosticoCie10: request.term
            };
            $.ajax({
                type: "GET",
                url: getControllerURL("Gestante") + "/GetDiagnosticosPorCie10",
                data: { diagnosticoCie10: request.term },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    $("#DiagIntermedio2Cie10").tooltip("hide");
                    response($.map(data, function (item) {
                        return {
                            label: item.Cie10,
                            value: item.DiagnosticoId,
                            label2: item.Descripcion
                        }
                    }));
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert("No se pudo encontrar el diagnóstico.");
                }
            });
        },
        focus: function (event, ui) {
            $("#DiagIntermedio2Cie10").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#DiagIntermedio2Cie10").tooltip("hide");
            $("#DiagIntermedio2Cie10").val(ui.item.label);
            $("#DiagIntermedio2").val(ui.item.label2);
            $("#DiagnosticoIntermedio2").val(ui.item.value);
            return false;
        }
    });

    //Autocompletar diagnostico egreso
    $("#DiagEgreso").autocomplete({
        delay: 700,
        height: 100,
        minLength: 5,
        source: function (request, response) {
            var params = {
                diagnostico: request.term
            };
            $.ajax({
                type: "GET",
                url: getControllerURL("Gestante") + "/GetDiagnosticosPorNombre",
                data: { diagnostico: request.term },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    $("#DiagEgreso").tooltip("hide");
                    response($.map(data, function (item) {
                        return {
                            label: item.Descripcion,
                            value: item.DiagnosticoId,
                            label2: item.Cie10
                        }
                    }));
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert("No se pudo encontrar el diagnóstico.");
                }
            });
        },
        focus: function (event, ui) {
            $("#DiagEgreso").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#DiagEgreso").tooltip("hide");
            $("#DiagEgresoCie10").val(ui.item.label2);
            $("#DiagEgreso").val(ui.item.label);
            $("#DiagnosticoEgreso").val(ui.item.value);
            return false;
        }
    });

    //Autocompletar diagnostico egreso CIE10
    $("#DiagEgresoCie10").autocomplete({
        delay: 700,
        height: 100,
        minLength: 2,
        source: function (request, response) {
            var params = {
                diagnosticoCie10: request.term
            };
            $.ajax({
                type: "GET",
                url: getControllerURL("Gestante") + "/GetDiagnosticosPorCie10",
                data: { diagnosticoCie10: request.term },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    $("#DiagEgresoCie10").tooltip("hide");
                    response($.map(data, function (item) {
                        return {
                            label: item.Cie10,
                            value: item.DiagnosticoId,
                            label2: item.Descripcion
                        }
                    }));
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert("No se pudo encontrar el diagnóstico.");
                }
            });
        },
        focus: function (event, ui) {
            $("#DiagEgresoCie10").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            $("#DiagEgresoCie10").tooltip("hide");
            $("#DiagEgresoCie10").val(ui.item.label);
            $("#DiagEgreso").val(ui.item.label2);
            $("#DiagnosticoEgreso").val(ui.item.value);
            return false;
        }
    });

    $("#DiagIngresoCie10").tooltip({ title: "Digite codigo CIE10 para buscar...", placement: "bottom" });
    $("#DiagIngreso").tooltip({ title: "Digite nombre de Dx para buscar...", placement: "bottom" });
    $("#DiagIntermedio1Cie10").tooltip({ title: "Digite codigo CIE10 para buscar...", placement: "bottom" });
    $("#DiagIntermedio1").tooltip({ title: "Digite nombre de Dx para buscar...", placement: "bottom" });
    $("#DiagIntermedio2Cie10").tooltip({ title: "Digite codigo CIE10 para buscar...", placement: "bottom" });
    $("#DiagIntermedio2").tooltip({ title: "Digite nombre de Dx para buscar...", placement: "bottom" });
    $("#DiagEgresoCie10").tooltip({ title: "Digite codigo CIE10 para buscar...", placement: "bottom" });
    $("#DiagEgreso").tooltip({ title: "Digite nombre de Dx para buscar...", placement: "bottom" });
});
