function confirmapprove() {
    var btnConfirm = document.getElementById("btnapproveall");
    var conf = confirm('Are You Sure Want To Approve All Transactions?');
    if (conf) {
        btnConfirm.click();
    }
    else {
        return false;
    }
}

// For Progress Bar
function ShowProgress() {
    setTimeout(function () {
        var modal = $('<div />');
        modal.addClass("modal");
        $('body').append(modal);
        var loading = $(".loading");
        loading.show();
        var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
        var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
        loading.css({ top: top, left: left });
    }, 200);
}
function ALART() {
    var btnConfirm = document.getElementById("btnapproveall");
    var ALART = alert('Selected Records Approved Successfully.');
    return false;
    }
