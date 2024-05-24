function SubmitValidation() {
    if (confirm('Are you sure you want to Submit this record to checker?')) {
        SubmitToChecker();
    }
    else {
        return false;
    }
}

function Toggel_GO1_Remarks() {
    var _txt_GO1_Remarks = $("#TabContainerMain_tbBROGO1_txt_GO1_Remarks").val();
    $("#TabContainerMain_tbBROGO1_txt_GO1_Debit_Details").val(_txt_GO1_Remarks);
    $("#TabContainerMain_tbBROGO1_txt_GO1_Credit_Details").val(_txt_GO1_Remarks);
}

function validate_Number(evnt) {
    var charCode = (evnt.which) ? evnt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
        return false;
    else
        return true;
}
function SubmitToChecker() {
    var DocNo1 = $("#txtDocNo").val();
    $.ajax({
        type: "POST",
        url: "TF_IMP_ReversalofGO_Maker.aspx/SubmitToChecker",
        data: '{DocNo:"' + DocNo + '"}',
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.d.CheckerStatus == "Submit") {
                window.location.href = "TF_IMP_ReversalofGO_Maker_View.aspx?result=Submit";
            }
            else {
                alert(data.CheckerStatus);
            }
        },
        failure: function (data) { alert(data.d); },
        error: function (data) { alert(data.d); }
    });
}