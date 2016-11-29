
function getControllerURL(controllerName) {
    var pathName = $(location).attr('pathname');
    var pos = pathName.indexOf(controllerName, 0);
    var returnValue = controllerName;
    if (pos > 0) {
        returnValue = pathName.substring(0, pos) + returnValue;
    }
    return returnValue;
}

function alertDialog(message, height, width) {
    if (height == null)
        height = 180
    if (width == null)
        width = 400
    var alertWidth = width - 110;
    var sHTML = '<div style="display:inline-block;width:' + alertWidth + 'px;margin:10px;">' + message + '</div>';
    var $thedialog = $("#GenericDialog");
    $thedialog.attr("title", "Atención");
    $thedialog.html(sHTML);
    $thedialog.dialog({
        autoOpen: true,
        dialogClass: 'customdialog-no-closebutton',
        resizable: false,
        height: height,
        width: width,
        modal: true,
        buttons: {
            Ok: function () {
                $thedialog.dialog("close");
                return true;
            }
        },
        create: function () {
            $(this).closest(".ui-dialog")
                .find("button")
                .addClass("btn btn-primary");
        }
    });
    $thedialog.width(width - 30);
    var div = $thedialog.parent("div").find("div.ui-dialog-titlebar");
    div.height(20);
    div.find("span.ui-dialog-title").width("100%");
    div.find("span.ui-dialog-title").css("text-align", "center");
}

function confirmDialog(message, OkFunction, CancelFunction, height, width) {
    if (height == null)
        height = 200
    if (width == null)
        width = 400
    var confirmWidth = width - 100;
    var sHTML = '<div style="display:inline-block;width:' + confirmWidth + 'px;margin:10px;">' + message + '</div>';
    var $thedialog = $("#GenericDialog");
    $thedialog.attr("title", "Confirmar");
    $thedialog.html(sHTML);
    $thedialog.dialog({
        autoOpen: true,
        dialogClass: 'customdialog-no-closebutton',
        resizable: false,
        height: height,
        width: width,
        modal: true,
        buttons: {
            Ok: function () {
                $thedialog.dialog("close");
                OkFunction();
            },
            Cancel: function () {
                $thedialog.dialog("close");
                CancelFunction();
            }
        },
        create: function () {
            $(this).closest(".ui-dialog")
                .find("button")
                .addClass("btn btn-primary");
        }
    });
    $thedialog.width(width - 25);
    var div = $thedialog.parent("div").find("div.ui-dialog-titlebar");
    div.height(20);
    div.find("span.ui-dialog-title").width("100%");
    div.find("span.ui-dialog-title").css("text-align", "center");
}

function formatDateAsString(date)
{
    return date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear();
}

