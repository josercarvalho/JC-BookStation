
$(document).ready(function () {
    contInputForm = 0;
    $('input,textarea,select').each(function () {
        contInputForm++;
        if (contInputForm == 1) {
            $(this).focus();
        }
        $(this).attr('onKeypress', 'setFocusInput(event, ' + contInputForm + ')');
    });

    setFocusInput = function (event, campo) {
        var contInputFormEvent = 0;
        if (event.keyCode == 13) {
            if (campo != null && campo != 0) {
                var campoFocus = null;
                $('input,textarea,select').each(function () {
                    contInputFormEvent++;
                    if (contInputFormEvent == campo + 1) {
                        campoFocus = this;
                    }
                });
                if (typeof (campoFocus) != "undefined") {
                    if (campoFocus.type == "submit")
                        checkParent(campoFocus);
                    $(campoFocus).focus();
                    event.preventDefault ? event.preventDefault() : event.returnValue = false;
                } else {
                    $(campoFocus).parent();
                }
            }
        }
    };

    checkParent = function (campo) {
        if ($(campo).parent().length > 0) {
            if ($($(campo).parent()).is("form")) {
                $($(campo).parent()).submit();
            } else {
                checkParent($(campo).parent());
            }
        } else {
            console.log("Element Form not found!");
        }
    };
});