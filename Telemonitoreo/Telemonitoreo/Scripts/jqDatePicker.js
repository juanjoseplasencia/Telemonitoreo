$(function () {
    $(".datepicker").datepicker({
        showButtonPanel: true,
        appendText: "(dd/mm/yyyy)"
    });
    var _gotoToday = jQuery.datepicker._gotoToday;
    jQuery.datepicker._gotoToday = function (a) {
        var target = jQuery(a);
        var inst = this._getInst(target[0]);
        _gotoToday.call(this, a);
        jQuery.datepicker._selectDate(a, jQuery.datepicker._formatDate(inst, inst.selectedDay, inst.selectedMonth, inst.selectedYear));
    };
    if (jQuery.validator)
    {
        jQuery.validator.methods.date = function (value, element) {
            if (value) {
                try {
                    $.datepicker.parseDate('dd/mm/yy', value);
                } catch (ex) {
                    return false;
                }
            }
            return true;
        };
    }
});