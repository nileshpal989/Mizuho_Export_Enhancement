function MyConfirm(Message) {
    $("#Paragraph").text(Message);
    $("#dialog").dialog({
        title: "Message From LMCC",
        width: 430,
        modal: true,
        closeOnEscape: true,
        dialogClass: "AlertJqueryDisplay",
        hide: { effect: "close", duration: 400 },
        buttons: [
                    {
                        text: "Yes", //"✔"
                        icon: "ui-icon-heart",
                        click: function () {
                            $(this).dialog("close");
                            return true;
                            //$("#btnDeleteConfirm").click();
                        }
                    },
                    {
                        text: "No", //"✖"
                        icon: "ui-icon-heart",
                        click: function () {
                            $(this).dialog("close");
                            return false;
                        }
                    }
                  ]
    }); //.prev(".ui-dialog-titlebar").css("background", "navy");
    $('.ui-dialog :button').blur();
}

