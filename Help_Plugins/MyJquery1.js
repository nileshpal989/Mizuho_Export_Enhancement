﻿function MyConfirm(Message,ID) {
    $("#Paragraph").text(Message);
    $("#dialog").dialog({
        title: "Confirm",
        width: 400,
        modal: true,
        closeOnEscape: true,
        dialogClass: "AlertJqueryDisplay",
        hide: { effect: "explode", duration: 400 },
        buttons: [
                    {
                        text: "Yes", //"✔"
                        icon: "ui-icon-heart",
                        click: function () {
                            $(this).dialog("close");
                            $(ID).click();
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

function MyAlert(Result) {
    var x = document.getElementById("snackbar");
    x.innerHTML = Result;
    x.className = "show";
    setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
}

function VAlert(Result, ID) {
    $("#Paragraph").text(Result);
    $("#dialog").dialog({
        title: "Alert",
        width: 400,
        modal: true,
        closeOnEscape: true,
        dialogClass: "AlertJqueryDisplay",
        hide: { effect: "explode", duration: 400 },
        buttons: [
                    {
                        text: "Ok",
                        icon: "ui-icon-heart",
                        click: function () {
                            $(this).dialog("close");
                            $(ID).focus();
                        }
                    }
                  ]
    });
    $('.ui-dialog :button').blur();
    return false;
}