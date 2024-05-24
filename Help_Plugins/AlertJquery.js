
function VAlert(Result) {
    $("#Paragraph").text(Result);
    $("#dialog").dialog({
        title: "Message From LMCC",
        width: 680,
        modal: true,
        closeOnEscape: true,
        dialogClass: "AlertJqueryDisplay",
        hide: { effect: "close", duration: 400 },
        buttons: [
                    {
                        text: "Ok",
                        icon: "ui-icon-heart",
                        click: function () {
                            $(this).dialog("close");
                            $("").focus();
                        }
                    }
                  ]
    });
    $('.ui-dialog :button').blur();
    return false;
}
