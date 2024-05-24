function allowBackSpace(ID, event) { //vinay Swift Changes
    if (ID == 734) {
        var key = window.event ? event.keyCode : event.which;
        //alert(key);
        if (event.keyCode == 8) {
            $("#TabContainerMain_tbSwift_txtDate734").val('');
            $("#TabContainerMain_tbSwift_btncalendar_Date734").focus(); ;
            return true;
        }
    }
}

function SwiftsAmountvalidate(ID) {//vinay Swift Changes
    Math.trunc = Math.trunc || function (x) {
        if (isNaN(x)) {
            return NaN;
        }
        if (x > 0) {
            return Math.floor(x);
        }
        return Math.ceil(x);
    };

    if (ID == 734) {
        var Swift_txtCurrency = $("#TabContainerMain_tbSwift_txtCurrency734").val().toUpperCase();
        var Swift_txtAmount = $("#TabContainerMain_tbSwift_txtAmount734").val();
        if (Swift_txtAmount == '' || Swift_txtAmount == '0') {
            $("#TabContainerMain_tbSwift_txtAmount734").val('');
        }
        else {
            if (Swift_txtCurrency == 'JPY') {
                var amt = Math.trunc(Swift_txtAmount);
            }
            else {
                var amt = parseFloat(Swift_txtAmount).toFixed(2);
            }
            $("#TabContainerMain_tbSwift_txtAmount734").val(amt);
            $("#TabContainerMain_tbSwift_txtAmount734").focus();
            
        }
    }
}

function Swift734validations() {
    var tabContainer = $get('TabContainerMain');
    if ($("#TabContainerMain_tbDocumentDetails_rdb_MT734").is(':checked')) {
        var _ddlTotalAmountClaimed734 = $("#TabContainerMain_tbSwift_ddlTotalAmountClaimed734").val();
        var _txtDate734 = $("#TabContainerMain_tbSwift_txtDate734").val();
        var _txtCurrency734 = $("#TabContainerMain_tbSwift_txtCurrency734").val();
        var _txtAmount734 = $("#TabContainerMain_tbSwift_txtAmount734").val();
        var _txtChargesClaimed7341 = $("#TabContainerMain_tbSwift_txtChargesClaimed7341").val();
        var _txtChargesClaimed7342 = $("#TabContainerMain_tbSwift_txtChargesClaimed7342").val();
        var _txtChargesClaimed7343 = $("#TabContainerMain_tbSwift_txtChargesClaimed7343").val();
        var _txtChargesClaimed7344 = $("#TabContainerMain_tbSwift_txtChargesClaimed7344").val();
        var _txtChargesClaimed7345 = $("#TabContainerMain_tbSwift_txtChargesClaimed7345").val();
        var _txtChargesClaimed7346 = $("#TabContainerMain_tbSwift_txtChargesClaimed7346").val();
        var _txtCurrency734 = $("#TabContainerMain_tbSwift_txtCurrency734").val().toUpperCase();   // vinay Swift Changes
        var _txt_Currency = $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency").val();
        var _Doctype = $("#hdnDocType").val();

        var _ddl_DisposalOfDoc = $("#TabContainerMain_tbSwift_ddl_DisposalOfDoc").val();

        if (_Doctype == "IBA" || _Doctype == "ACC") {

            if ((_txtCurrency734 != _txt_Currency) && (_txtDate734!="")) {
                alert('33A Total Amount Claimed Currency is Not valid.');

                tabContainer.control.set_activeTabIndex(1);
                $("#TabContainerMain_tbSwift_txtCurrency734").focus();
                return false;
            }

            if (_txtChargesClaimed7341 != "" || _txtChargesClaimed7342 != "" || _txtChargesClaimed7343 != "" || _txtChargesClaimed7344 != "" || _txtChargesClaimed7345 != "" || _txtChargesClaimed7346 != "") {
                if (_ddlTotalAmountClaimed734 == "A") {
                    if (_txtDate734 == "") {
                        alert('Please Fill 33A Total amount Date.');
                        tabContainer.control.set_activeTabIndex(1);
                        $("#TabContainerMain_tbSwift_txtDate734").focus();
                        return false;
                    }
                    else if (_txtCurrency734 == "") {
                        alert('Please Fill 33A Total amount Currency.');
                        tabContainer.control.set_activeTabIndex(1);
                        $("#TabContainerMain_tbSwift_txtCurrency734").focus();
                        return false;
                    }
                    else if (_txtAmount734 == "" || _txtAmount734 == "0") {
                        alert('Please Fill 33A Total amount .');
                        tabContainer.control.set_activeTabIndex(1);
                        $("#TabContainerMain_tbSwift_txtAmount734").focus();
                        return false;
                    }
                }

                if (_txtChargesClaimed7341 != "" || _txtChargesClaimed7342 != "" || _txtChargesClaimed7343 != "" || _txtChargesClaimed7344 != "" || _txtChargesClaimed7345 != "" || _txtChargesClaimed7346 != "") {
                    if (_txtCurrency734 != _txt_Currency) {
                        alert('33A Total Amount Claimed Currency is Not valid.');
                        tabContainer.control.set_activeTabIndex(1);
                        $("#TabContainerMain_tbSwift_txtCurrency734").focus();
                        return false;
                    }
                }

                if (_ddlTotalAmountClaimed734 == "B") {
                    if (_txtCurrency734 == "") {
                        alert('Please Fill Total amount Currency.');
                        tabContainer.control.set_activeTabIndex(1);
                        $("#TabContainerMain_tbSwift_txtCurrency734").focus();
                        return false;
                    }
                    else if (_txtAmount734 == "" || _txtAmount734 == "0") {
                        alert('Please Fill Total amount .');
                        tabContainer.control.set_activeTabIndex(1);
                        $("#TabContainerMain_tbSwift_txtAmount734").focus();
                        return false;
                    }
                }
            }
            if (_ddl_DisposalOfDoc == "") {
                alert('Please Select Disposal Of Document.');
                tabContainer.control.set_activeTabIndex(1);
                $("#TabContainerMain_tbSwift_ddl_DisposalOfDoc").focus();
                return false;
            }
        }
    }
    return true;
}

function OnDocNextClickNew() {
    var DocType = $("#hdnDocType").val();
    if (DocType == "ICA" || DocType == "ICU") {
        OnDocNextClick(1);
    }
    if (DocType == "IBA" || DocType == "ACC") {
        OnDocNextClick(1);
    }
    return false;
}
function OnSwiftPrevClickNew() {
    var DocType = $("#hdnDocType").val();
    if (DocType == "ICA" || DocType == "ICU") {
        OnDocNextClick(0);
    }
    if (DocType == "IBA" || DocType == "ACC") {
        OnDocNextClick(0);
    }
    return false;
}
function OnSwiftNextClickNew() {
    var DocType = $("#hdnDocType").val();
    if (DocType == "ICA" || DocType == "ICU") {
        OnDocNextClick(2);
    }
    if (DocType == "IBA" || DocType == "ACC") {
        OnDocNextClick(2);
    }
    return false;
}
function OnInstPrevClickNew() {

    var DocType = $("#hdnDocType").val();
    if (DocType == "ICA" || DocType == "ICU") {
        OnDocNextClick(0);
    }
    if (DocType == "IBA" || DocType == "ACC") {
        OnDocNextClick(1);
    }
    return false;
}
function OnInstNextClickNew() {

    var DocType = $("#hdnDocType").val();
    if (DocType == "ICA" || DocType == "ICU") {
        OnDocNextClick(2);
    }
    if (DocType == "IBA" || DocType == "ACC") {
        OnDocNextClick(3);
    }
    return false;
}
function OnAMLPrevClickNew() {
    var DocType = $("#hdnDocType").val();
    if (DocType == "ICA" || DocType == "ICU") {
        OnDocNextClick(1);
    }
    if (DocType == "IBA" || DocType == "ACC") {
        OnDocNextClick(2);
    }
    return false;
}
function TotalAmountClaimedChange() {
    var TotalAmountClaimed = $("#TabContainerMain_tbSwift_ddlTotalAmountClaimed734").val();
    if (TotalAmountClaimed == 'A') {
        $("#TabContainerMain_tbSwift_txtDate734").css('display', 'inline-block');
        $("#TabContainerMain_tbSwift_btncalendar_Date734").css('display', 'inline-block'); // vinay Swift Changes
        $("#TabContainerMain_tbSwift_lblDate734").css('display', 'inline-block');
    }
    if (TotalAmountClaimed == 'B') {
        $("#TabContainerMain_tbSwift_txtDate734").css('display', 'none');
        $("#TabContainerMain_tbSwift_btncalendar_Date734").css('display', 'none');  // vinay Swift Changes
        $("#TabContainerMain_tbSwift_lblDate734").css('display', 'none');
    }
};
function AccountWithBankChange() {

    var AccountWithBank = $("#TabContainerMain_tbSwift_ddlAccountWithBank734").val();
    if (AccountWithBank == 'A') {

        $("#TabContainerMain_tbSwift_txtAccountWithBank734SwiftCode").css('display', 'block');
        $("#TabContainerMain_tbSwift_lblAccountWithBank734SwiftCode").text('Swift/IFSC Code :');
        $("#TabContainerMain_tbSwift_txtAccountWithBank734Location").css('display', 'none');
        $("#TabContainerMain_tbSwift_txtAccountWithBank734Name").css('display', 'none');
        $("#TabContainerMain_tbSwift_lblAccountWithBank734Address1").css('display', 'none');
        $("#TabContainerMain_tbSwift_lblAccountWithBank734Address2").css('display', 'none');
        $("#TabContainerMain_tbSwift_lblAccountWithBank734Address3").css('display', 'none');
        $("#TabContainerMain_tbSwift_txtAccountWithBank734Address1").css('display', 'none');
        $("#TabContainerMain_tbSwift_txtAccountWithBank734Address2").css('display', 'none');
        $("#TabContainerMain_tbSwift_txtAccountWithBank734Address3").css('display', 'none');
    }
    if (AccountWithBank == 'B') {
        $("#TabContainerMain_tbSwift_txtAccountWithBank734SwiftCode").css('display', 'none');
        $("#TabContainerMain_tbSwift_lblAccountWithBank734SwiftCode").text('Location :');
        $("#TabContainerMain_tbSwift_txtAccountWithBank734Location").css('display', 'block');
        $("#TabContainerMain_tbSwift_txtAccountWithBank734Name").css('display', 'none');
        $("#TabContainerMain_tbSwift_lblAccountWithBank734Address1").css('display', 'none');
        $("#TabContainerMain_tbSwift_lblAccountWithBank734Address2").css('display', 'none');
        $("#TabContainerMain_tbSwift_lblAccountWithBank734Address3").css('display', 'none');
        $("#TabContainerMain_tbSwift_txtAccountWithBank734Address1").css('display', 'none');
        $("#TabContainerMain_tbSwift_txtAccountWithBank734Address2").css('display', 'none');
        $("#TabContainerMain_tbSwift_txtAccountWithBank734Address3").css('display', 'none');
    }
    if (AccountWithBank == 'D') {
        $("#TabContainerMain_tbSwift_txtAccountWithBank734SwiftCode").css('display', 'none');
        $("#TabContainerMain_tbSwift_lblAccountWithBank734SwiftCode").text('Name :');
        $("#TabContainerMain_tbSwift_txtAccountWithBank734Location").css('display', 'none');
        $("#TabContainerMain_tbSwift_txtAccountWithBank734Name").css('display', 'block');
        $("#TabContainerMain_tbSwift_lblAccountWithBank734Address1").css('display', 'block');
        $("#TabContainerMain_tbSwift_lblAccountWithBank734Address2").css('display', 'block');
        $("#TabContainerMain_tbSwift_lblAccountWithBank734Address3").css('display', 'block');
        $("#TabContainerMain_tbSwift_txtAccountWithBank734Address1").css('display', 'block');
        $("#TabContainerMain_tbSwift_txtAccountWithBank734Address2").css('display', 'block');
        $("#TabContainerMain_tbSwift_txtAccountWithBank734Address3").css('display', 'block');
    }
};
function PageLoadFunctions() {
    OnTheirRefNoChange();
    OnInvoiceNoChange();
}
function OnTheirRefNoChange() {
    var DocNo = $("#txtDocNo");
    var TheirRefNo = $("#TabContainerMain_tbDocumentDetails_txt_Their_Ref_no");
    if (TheirRefNo.val() != '') {
        $.ajax({
            type: "POST",
            url: "TF_IMP_BOE_Maker.aspx/TheirRefNoChange",
            data: '{TheirRefNo:"' + TheirRefNo.val() + '",DocNo:"' + DocNo.val() + '"}',
            dataType: "JSON",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.d.TheirRefNoStatus == 'Exist') {
                    alert('This Bank Ref No [' + TheirRefNo.val() + '] already exists.');
                    TheirRefNo.focus();
                    //TheirRefNo.val('');
                    //return false;
                }
                else {

                }
            },
            failure: function () { },
            error: function () { }
        });
    }
}
function OnInvoiceNoChange() {
    var DocNo = $("#txtDocNo");
    var InvoiceNo = $("#TabContainerMain_tbDocumentDetails_txt_Inv_No");
    var InvoiceNoArray = InvoiceNo.val().split(",");
    if (InvoiceNo.val() != '') {
        $.each(InvoiceNoArray, function (index, value) {
            $.ajax({
                type: "POST",
                url: "TF_IMP_BOE_Maker.aspx/InvoiceNoChange",
                data: "{InvoiceNo:'" + value + "',DocNo:'" + DocNo.val() + "'}",
                dataType: "JSON",
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data.d.InvoiceNoStatus == 'Exist') {
                        alert('This Invoice No [' + value + '] already exists.');
                    }
                    else {

                    }
                },
                failure: function () { },
                error: function () { }
            });
        });
    }
}
function OnInputKeyPress() {
    $("input:not(#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions2,#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions1)").keypress(function (e) {
        var k;
        document.all ? k = e.keyCode : k = e.which;
        return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || k == 47 || k == 45 || k == 46 || k == 44 || (k >= 48 && k <= 57));
    });
    $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions2, #TabContainerMain_tbDocumentInstructions_txt_SP_Instructions1").keypress(function (e) {
        var k;
        document.all ? k = e.keyCode : k = e.which;
        return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || k == 47 || k == 45 || k == 46 || k == 44 || (k >= 48 && k <= 57));
    });
}
function OnDocumentScrutinyChange() {
    var DocType = $("#hdnDocType").val();
    var LocalForeign = $("#lblForeign_Local").text();
    var DocScrutiny = $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Scrutiny").val();
    var AcceptanceDate = $("#TabContainerMain_tbDocumentDetails_txt_AcceptanceDate").val();
    $("#hdnDocumentScrutiny").val(DocScrutiny);
    if (DocType == 'IBA') {
        if (LocalForeign == "FOREIGN" && DocScrutiny == "1") {
            //Foreign IBA Without DISCREPANCY
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions1").text("AS PER THE RBI GUIDELINES BILL OF ENTRIES TO BE SUBMITTED");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions2").text("WITHIN 90 DAYS FROM THE DATE OF REMITTANCE. WE DELIVER");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions3").text("THE DOCUMENTS AGAINST PAYMENT. KINDLY PROVIDE YOUR");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions4").text("SETTLEMENT INSTRUCTIONS AS PER UCP 600");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions5").text("BY (5 BANKING DAYS) " + AcceptanceDate);

        }
        else if (LocalForeign == "FOREIGN" && DocScrutiny == "2") {
            //Foreign IBA With DISCREPANCY
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions1").text("AS PER THE RBI GUIDELINES BILL OF ENTRIES TO BE SUBMITTED");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions2").text("WITHIN 90 DAYS FROM THE DATE OF REMITTANCE WE DELIVER");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions3").text("THE DOCUMENTS AGAINST PAYMENT. IF THE DISCREPANCIES ARE");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions4").text("ACCEPTABLE, KINDLY PROVIDE YOUR SETTLEMENT INSTRUCTIONS AS");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions5").text("PER ARTICLE 16(D) OF UCP 600 BY(5 BANKING DAYS) " + AcceptanceDate);

            $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_Annexure").prop('checked', true);
            OnAsPerAnnexureChange();
        }
        else if (LocalForeign == "LOCAL" && DocScrutiny == "1") {
            //Local IBA Without DISCREPANCY
            Exp_AcceptanceDateCal();
            DuedateCal();
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions1").text("WE DELIVER THE DOCUMENTS AGAINST PAYMENT.");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions2").text("KINDLY PROVIDE YOUR SETTLEMENT INSTRUCTIONS ");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions3").text("AS PER UCP 600 BY (5 BANKING DAYS) " + AcceptanceDate);
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions4").text("");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions5").text("");

        }
        else if (LocalForeign == "LOCAL" && DocScrutiny == "2") {
            //Local IBA With DISCREPANCY
            Exp_AcceptanceDateCal();
            DuedateCal();
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions1").text("WE DELIVER THE DOCUMENTS AGAINST PAYMENT. IF THE");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions2").text("DISCREPANCIES ARE ACCEPTABLE,KINDLY PROVIDE YOUR");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions3").text("SETTLEMENT INSTRUCTIONS AS PER ARTICLE 16(D) OF UCP 600.");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions4").text("BY (5 BANKING DAYS) " + AcceptanceDate);
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions5").text("");

            $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_Annexure").prop('checked', true);
            OnAsPerAnnexureChange();
        }
    }
    if (DocType = 'ACC') {
        if (LocalForeign == "FOREIGN" && DocScrutiny == "1") {
            //Foreign ACC Without DISCREPANCY
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions1").text("AS PER THE RBI GUIDELINES BILL OF ENTRIES TO BE SUBMITTED");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions2").text("WITHIN 90 DAYS FROM THE DATE OF REMITTANCE. WE DELIVER");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions3").text("THE DOCUMENTS AGAINST ACCEPTANCE. KINDLY PROVIDE");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions4").text("YOUR ACCEPTANCE AS PER UCP 600 BY(5 BANKING DAYS) " + AcceptanceDate);
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions5").text("");
        }
        else if (LocalForeign == "FOREIGN" && DocScrutiny == "2") {
            //Foreign ACC With DISCREPANCY
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions1").text("AS PER THE RBI GUIDELINES BILL OF ENTRIES TO BE SUBMITTED");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions2").text("WITHIN 90 DAYS FROM THE DATE OF REMITTANCE. WE DELIVER");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions3").text("THE DOCUMENTS AGAINST ACCEPTANCE. IF THE DISCREPANCIES");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions4").text("ARE ACCEPTED BY YOU KINDLY PROVIDE YOUR ACCEPTANCE");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions5").text("AS PER ARTICLE 16(D) OF UCP 600, BY " + AcceptanceDate);

            $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_Annexure").prop('checked', true);
            OnAsPerAnnexureChange();
        }
        else if (LocalForeign == "LOCAL" && DocScrutiny == "1") {
            //Local ACC Without DISCREPANCY
            Exp_AcceptanceDateCal();
            DuedateCal();
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions1").text("KINDLY PROVIDE YOUR ACCEPTANCE AS PER UCP 600");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions2").text("BY (5 BANKING DAYS) " + AcceptanceDate);
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions3").text("WE DELIVER THE DOCUMENTS AGAINST ACCEPTANCE.");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions4").text("KINDLY MAKE YOUR PAYMENT ON DUE DATE.");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions5").text("");

        }
        else if (LocalForeign == "LOCAL" && DocScrutiny == "2") {
            //Local ACC With DISCREPANCY
            Exp_AcceptanceDateCal();
            DuedateCal();
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions1").text("IF THE DISCREPANCIES ARE ACCEPTABLE,");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions2").text("KINDLY PROVIDE YOUR ACCEPTANCE AS PER");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions3").text("ARTICLE 16(D) OF UCP 600 BY (5 BANKING DAYS) " + AcceptanceDate);
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions4").text("WE DELIVER THE DOCUMENTS AGAINST ACCEPTANCE.");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions5").text("KINDLY MAKE YOUR PAYMENT ON DUE DATE.");

            $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_Annexure").prop('checked', true);
            OnAsPerAnnexureChange();
        }
    }
    if (DocScrutiny == '2') {

        $("#TabContainerMain_tbDocumentDetails_divDisc").css('display', 'block');
    }
    else {
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_1").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_2").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_3").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_4").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_5").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_6").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_7").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_8").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_9").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_10").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_11").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_12").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_13").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_14").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_15").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_16").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_17").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_18").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_19").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_20").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_21").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_22").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_23").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_24").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_25").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_26").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_27").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_28").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_29").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_30").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_31").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_32").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_33").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_34").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_35").val('');
        $("#TabContainerMain_tbDocumentDetails_divDisc").css('display', 'none');
    }
    InstractionChangeForBRO();
}
function OnCountryOfOriginChange() {
    var CountryID = $("#TabContainerMain_tbDocumentDetails_ddlCountryOfOrigin");
    $.ajax({
        type: "POST",
        url: "TF_IMP_BOE_Maker.aspx/fill_Country_Description",
        data: '{CountryID:"' + CountryID.val() + '"}',
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.d.CountryDesc != "") {
                $("#TabContainerMain_tbDocumentDetails_lblCountryOfOriginDesc").text(data.d.CountryDesc);
                $("#TabContainerMain_tbAmlDetails_txtCountryOfOrigin").val(data.d.CountryDesc);
                if (data.d.CountrySStatus == "false") {
                    alert('Selected Country Of Origin is in Sanctioned Country list.');
                }
                CountryID.focus();
            }
            else {
                $("#TabContainerMain_tbAmlDetails_txtCountryOfOrigin").val('');
                $("#TabContainerMain_tbDocumentDetails_lblCountryOfOriginDesc").text('');
                CountryID.focus();
            }
        },
        failure: function (data) { alert(data.d); CountryID.focus(); },
        error: function (data) { alert(data.d); CountryID.focus(); }
    });
}
function OnSpecialCasesChange() {
    if ($("#TabContainerMain_tbDocumentDetails_chkSpecialCase").is(":checked")) {
        $(".riskcustmercss").css('visibility', 'visible');
        $("#TabContainerMain_tbDocumentDetails_txtRiskCust").val($('#hdnLCRiskCust').val());
        $("#TabContainerMain_tbDocumentDetails_txtSettelementAcNo").val($('#hdnLCSettlementAccNo').val());
        $("#TabContainerMain_tbDocumentDetails_txtRiskCustAbbr").val($('#hdnLCSpCustAbbr').val());
    }
    else {
        $(".riskcustmercss").css('visibility', 'hidden');
        $("#TabContainerMain_tbDocumentDetails_txtRiskCust").val('');
        $("#TabContainerMain_tbDocumentDetails_txtSettelementAcNo").val('');
        $("#TabContainerMain_tbDocumentDetails_txtRiskCustAbbr").val('');
    }
}
function DownloadAmlClick() {
    var DocNo = $("#txtDocNo").val();
    var UserName = $("#hdnUserName").val();
    var Path = "E:-Ashu-Mizuho-Import_Automation-13082019-Mizuho_TF-TF_GeneratedFiles-IMPORT-AML";
    $.ajax({
        type: "POST",
        url: "TF_IMP_BOE_Maker.aspx/AMLTextFileCreation",
        data: '{DocNo:"' + DocNo + '",Path:"' + Path + '",UserName:"' + UserName + '"}',
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.d.AMLFileName != "") {
                //var FilePath = 'E:/Ashu/Mizuho/Import_Automation/13082019/Mizuho_TF/TF_GeneratedFiles/IMPORT/AML/';
                open_popup('../HelpForms/TF_IMPWH_PaymentAuthExcelFileCreation.aspx?FileName=' + data.d.AMLFileName, 450, 650, 'Nego_Remit_BankList');
                return false;
            }
            else {
            }
        },
        failure: function (data) { alert(data.d); TenorDays.focus(); },
        error: function (data) { alert(data.d); TenorDays.focus(); }
    });
    return false;
}
function SubmitValidation() {
    SaveUpdateData();
    var _ddl_Doc_Currency = $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency").val();
    var _txtBillAmount = $("#TabContainerMain_tbDocumentDetails_txtBillAmount").val();
    var _txtCustomer_ID = $("#TabContainerMain_tbDocumentDetails_txtCustomer_ID").val();
    var _txtDocNo = $("#txtDocNo").val();
    var tabContainer = $get('TabContainerMain');
    if ($("#txtDateReceived").val() == '') {
        alert('Received Date can not be blank.');
        tabContainer.control.set_activeTabIndex(0);
        $("#txtDateReceived").focus();
        return false;
    }
    if (_txtCustomer_ID == '') {
        alert('Customer can not be blank.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_txtCustomer_ID").focus();
        return false;
    }
    if (_txtBillAmount == '' || _txtBillAmount == 0) {
        alert('Bill Amount can not be blank.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_txtBillAmount").focus();
        return false;
    }
    if (_ddl_Doc_Currency == '') {
        alert('Please select Currency.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency").focus();
        return false;
    }
    if ($("#TabContainerMain_tbDocumentDetails_txtDueDate").val() == '') {
        alert('Please Enter Due Date.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_txtDueDate").focus();
        return false;
    }
    else {
        var DueDate = $("#TabContainerMain_tbDocumentDetails_txtDueDate").val();
        var DueDateDay = DueDate.split('/')[0];
        var DueDateMonth = DueDate.split('/')[1] - 1;
        var DueDateYear = DueDate.split('/')[2];

        var LodgDate = $("#txtLogdmentDate").val();
        var LodgDateDay = LodgDate.split('/')[0];
        var LodgDateMonth = LodgDate.split('/')[1] - 1;
        var LodgDateYear = LodgDate.split('/')[2];

        var LodgDateNew = new Date(LodgDateYear, LodgDateMonth, LodgDateDay);
        var DueDateNew = new Date(DueDateYear, DueDateMonth, DueDateDay);
        if (DueDateNew < LodgDateNew) {
            alert('Due date can not be Less than Lodgement date.');
            $("#TabContainerMain_tbDocumentDetails_txtDueDate").focus();
            return false;
        }
    }

    if ($("#TabContainerMain_tbDocumentDetails_ddlDrawer").val() == '') {
        alert('Please select Drawer.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_ddlDrawer").focus();
        return false;
    }
    if ($("#TabContainerMain_tbDocumentDetails_ddlCountryCode").val() == '') {
        alert('Select Country Code.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_ddlCountryCode").focus();
        return false;
    }
    if ($("#TabContainerMain_tbDocumentDetails_txtCommodityDesc").val() == '') {
        alert('Enter Goods Description.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_txtCommodityDesc").focus();
        return false;
    }
    //    if ($("#TabContainerMain_tbDocumentDetails_txt_Inv_No").val() == '') {
    //        alert('Enter Invoice No.');
    //        tabContainer.control.set_activeTabIndex(0);
    //        $("#TabContainerMain_tbDocumentDetails_txt_Inv_No").focus();
    //        return false;
    //    }
    if ($("#TabContainerMain_tbDocumentDetails_ddlTenor_Days_From").val() == 'BOEXCHANGE DATE' && $("#TabContainerMain_tbDocumentDetails_txtBOExchange").val() == '') {
        alert('Enter BOExchange Date.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_txtBOExchange").focus();
        return false;
    }
    if ($("#TabContainerMain_tbDocumentDetails_ddlTenor_Days_From").val() == 'INVOICE DATE' && $("#TabContainerMain_tbDocumentDetails_txt_Inv_Date").val() == '') {
        alert('Enter Invoice Date.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_txt_Inv_Date").focus();
        return false;
    }

    if ($("#TabContainerMain_tbDocumentDetails_ddlTenor_Days_From").val() == 'SHIPMENT DATE' && $("#TabContainerMain_tbDocumentDetails_txtShippingDate").val() == '') {
        alert('Enter Shipping Date.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_txtShippingDate").focus();
        return false;
    }
    if ($("#TabContainerMain_tbDocumentDetails_txtShippingDate").val() != '' && $("#TabContainerMain_tbDocumentDetails_txtVesselName").val() == '') {
        alert('Enter Vessel Name.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_txtVesselName").focus();
        return false;
    }
    if ($("#TabContainerMain_tbDocumentDetails_txtShippingDate").val() != '' && $("#TabContainerMain_tbDocumentDetails_txtFromPort").val() == '') {
        alert('Enter From port.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_txtFromPort").focus();
        return false;
    }
    if ($("#TabContainerMain_tbDocumentDetails_txtShippingDate").val() != '' && $("#TabContainerMain_tbDocumentDetails_txtToPort").val() == '') {
        alert('Enter To port.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_txtToPort").focus();
        return false;
    }
    if ($("#lblCollection_Lodgment_UnderLC").text() == "Lodgment_Under_LC") {
        if ($("#TabContainerMain_tbDocumentDetails_txt_LC_No").val() == '') {
            alert('LC No can not be blank.');
            tabContainer.control.set_activeTabIndex(0);
            $("#TabContainerMain_tbDocumentDetails_txt_LC_No").focus();
            return false;
        }
        if ($("#TabContainerMain_tbDocumentDetails_txtNego_Date").val() == '') {
            alert('Please Enter Negotiation Date.');
            tabContainer.control.set_activeTabIndex(0);
            $("#TabContainerMain_tbDocumentDetails_txtNego_Date").focus();
            return false;
        }
        if ($("#TabContainerMain_tbDocumentDetails_ddlDrawer").val() == '') {
            alert('Select Drawer.');
            tabContainer.control.set_activeTabIndex(0);
            $("#TabContainerMain_tbDocumentDetails_ddlDrawer").focus();
            return false;
        }
        if ($("#TabContainerMain_tbDocumentDetails_ddl_Nego_Remit_Bank").val() == '') {
            alert('Select Nego / Remit Bank Type.');
            tabContainer.control.set_activeTabIndex(0);
            $("#TabContainerMain_tbDocumentDetails_ddl_Nego_Remit_Bank").focus();
            return false;
        }
        if ($("#TabContainerMain_tbDocumentDetails_txtNego_Remit_Bank").val() == '') {
            alert('Swift/IFSC Code can not be blank.');
            tabContainer.control.set_activeTabIndex(0);
            $("#TabContainerMain_tbDocumentDetails_txtNego_Remit_Bank").focus();
            return false;
        }
        if ($("#TabContainerMain_tbDocumentDetails_ddl_Doc_Scrutiny").val() == '2' && $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_1").val() == '') {
            alert('Discrepancy can not be blank for Discrepant Document.');
            tabContainer.control.set_activeTabIndex(0);
            $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_1").focus();
            return false;
        }
    }
    if ($("#lblCollection_Lodgment_UnderLC").text() == "Collection") {
        if ($("#TabContainerMain_tbDocumentDetails_ddlDrawer").val() == '') {
            alert('Select Drawer.');
            tabContainer.control.set_activeTabIndex(0);
            $("#TabContainerMain_tbDocumentDetails_ddlDrawer").focus();
            return false;
        }
        if ($("#TabContainerMain_tbDocumentDetails_ddl_Nego_Remit_Bank").val() == '') {
            alert('Select Nego / Remit Bank Type.');
            tabContainer.control.set_activeTabIndex(0);
            $("#TabContainerMain_tbDocumentDetails_ddl_Nego_Remit_Bank").focus();
            return false;
        }
        if ($("#TabContainerMain_tbDocumentDetails_txtNego_Remit_Bank").val() == '') {
            alert('Swift/IFSC Code can not be blank.');
            tabContainer.control.set_activeTabIndex(0);
            $("#TabContainerMain_tbDocumentDetails_txtNego_Remit_Bank").focus();
            return false;
        }
        if ($("#TabContainerMain_tbDocumentDetails_ddl_Commodity").val() == '') {
            alert('Select Commodity Code.');
            tabContainer.control.set_activeTabIndex(0);
            $("#TabContainerMain_tbDocumentDetails_ddl_Commodity").focus();
            return false;
        }
        if ($("#TabContainerMain_tbDocumentDetails_ddlCountryCode").val() == '') {
            alert('Select Country Code.');
            tabContainer.control.set_activeTabIndex(0);
            $("#TabContainerMain_tbDocumentDetails_ddlCountryCode").focus();
            return false;
        }
    }

    if ($("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions1").val().length > 60) {
        alert('Max lenght is 60 Character for Special Instructions1.');
        tabContainer.control.set_activeTabIndex(2);
        $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions1").focus();
        return false;
    }
    if ($("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions2").val().length > 60) {
        alert('Max lenght is 60 Character for Special Instructions2.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions2").focus();
        return false;
    }
    if ($("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions3").val().length > 60) {
        alert('Max lenght is 60 Character for Special Instructions3.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions3").focus();
        return false;
    }
    if ($("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions4").val().length > 60) {
        alert('Max lenght is 60 Character for Special Instructions4.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions4").focus();
        return false;
    }
    if ($("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions5").val().length > 60) {
        alert('Max lenght is 60 Character for Special Instructions5.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions5").focus();
        return false;
    }

    var _txt_Their_Ref_no = $("#TabContainerMain_tbDocumentDetails_txt_Their_Ref_no").val();
    if (_txt_Their_Ref_no.match("^/")) {
        alert("Their_Ref_no start with /");
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_txt_Their_Ref_no").focus();
        return false;
    }
    if (_txt_Their_Ref_no.match("/$")) {
        alert("Their_Ref_no ends with /");
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_txt_Their_Ref_no").focus();
        return false;
    }

    if (_txt_Their_Ref_no.match("//")) {
        alert("Their_Ref_no content //");
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_txt_Their_Ref_no").focus();
        return false;
    }

    if (!$("#TabContainerMain_tbDocumentDetails_cb_Protest").is(':checked') && ($("#TabContainerMain_tbDocumentDetails_rdb_MT499").is(':checked') || $("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(':checked'))) {
        if (
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_1") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_2") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_3") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_4") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_5") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_6") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_7") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_9") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_10") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_11") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_12") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_13") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_14") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_15") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_16") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_17") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_19") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_20") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_21") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_22") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_23") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_24") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_25") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_26") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_27") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_29") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_30") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_31") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_32") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_33") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_34") == "" &&
        $().val("TabContainerMain_tbDocumentDetails_txt_Narrative_35") == ""
        ) {
            alert('Please Enter Narrative.');
            return false;
        }
    }
    /*
    var _Swift_IFSC = $("#TabContainerMain_tbDocumentDetails_lbl_Nego_RemitSwift_IFSC").attr('title');
    if (_Swift_IFSC.match("^/")) {
    alert("Swift / IFSC start with /");
    tabContainer.control.set_activeTabIndex(0);
    $("#TabContainerMain_tbDocumentDetails_txtNego_Remit_Bank").focus();
    return false;
    }
    if (_Swift_IFSC.match("/$")) {
    alert("Swift / IFSC ends with /");
    tabContainer.control.set_activeTabIndex(0);
    $("#TabContainerMain_tbDocumentDetails_txtNego_Remit_Bank").focus();
    return false;
    }

    if (_Swift_IFSC.match("//")) {
    alert("Swift / IFSC content //");
    tabContainer.control.set_activeTabIndex(0);
    $("#TabContainerMain_tbDocumentDetails_txtNego_Remit_Bank").focus();
    return false;
    }
    */
    return true;
}

function LeiVerify() {
    SaveUpdateData();
}


function SubmitConfirm() {
    if (SubmitValidation() == true && ValidateSwiftCreate() == true && ValidateSFMSCreate() == true) {
        if (Swift734validations() == true) {

            if (confirm('Are you sure you want to Submit this record to checker?')) {
                $("#TabContainerMain_tbDocumentDetails_txtCustomer_ID").val('');
                SubmitToChecker();
            }
            else {
                return false;
            }
        }
        else {
            return false;
        }
    }
    else {
        return false;
    }
}
function OnPaymentSwift56Change() {
    PaymentSwiftselection();
    $("#TabContainerMain_tbDocumentInstructions_ddlPaymentSwift56").focus();
}
function OnPaymentSwift57Change() {
    PaymentSwiftselection();
    $("#TabContainerMain_tbDocumentInstructions_ddlPaymentSwift57").focus();
}
function OnPaymentSwift58Change() {
    PaymentSwiftselection();
    $("#TabContainerMain_tbDocumentInstructions_ddlPaymentSwift58").focus();
}
function OnBackClick() {
    SaveUpdateData();
    /*
    var DocNo = $("#txtDocNo").val();
    var UserName = $("#hdnUserName").val();
    $.ajax({
        type: "POST",
        url: "TF_IMP_BOE_Maker.aspx/CreateUserLog",
        data: '{UserName:"' + UserName + '",DocNo:"' + DocNo + '"}',
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {

        },
        failure: function (data) { alert(data.d); CommodityID.focus(); },
        error: function (data) { alert(data.d); CommodityID.focus(); }
    });
    */
    window.location.href = "TF_IMP_BOE_Maker_View.aspx";
    return false;
}
function PaymentSwiftselection() {
    if ($("#TabContainerMain_tbDocumentInstructions_ddlPaymentSwift56").val() == "56A") {
        $("#TabContainerMain_tbDocumentInstructions_panel56ACC_No").css('display', 'block');
        $("#TabContainerMain_tbDocumentInstructions_panel56A").css('display', 'block');
        $("#TabContainerMain_tbDocumentInstructions_panel56DName").css('display', 'none');
        $("#TabContainerMain_tbDocumentInstructions_panel56DAddress").css('display', 'none');
        $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift56D_name").val('');
        $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift56D_Address").val('');
    }
    else if ($("#TabContainerMain_tbDocumentInstructions_ddlPaymentSwift56").val() == "56D") {
        $("#TabContainerMain_tbDocumentInstructions_panel56ACC_No").css('display', 'block');
        $("#TabContainerMain_tbDocumentInstructions_panel56DName").css('display', 'block');
        $("#TabContainerMain_tbDocumentInstructions_panel56DAddress").css('display', 'block');
        $("#TabContainerMain_tbDocumentInstructions_panel56A").css('display', 'none');
        $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift56A").val('');
    }
    else {
        $("#TabContainerMain_tbDocumentInstructions_panel56ACC_No").css('display', 'none');
        $("#TabContainerMain_tbDocumentInstructions_panel56DName").css('display', 'none');
        $("#TabContainerMain_tbDocumentInstructions_panel56DAddress").css('display', 'none');
        $("#TabContainerMain_tbDocumentInstructions_panel56A").css('display', 'none');
    }
    if ($("#TabContainerMain_tbDocumentInstructions_ddlPaymentSwift57").val() == "57A") {
        $("#TabContainerMain_tbDocumentInstructions_panel57ACC_No").css('display', 'block');
        $("#TabContainerMain_tbDocumentInstructions_panel57A").css('display', 'block');
        $("#TabContainerMain_tbDocumentInstructions_panel57DName").css('display', 'none');
        $("#TabContainerMain_tbDocumentInstructions_panel57DAddress").css('display', 'none');
        $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift57D_name").val('');
        $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift57D_Address").val('');
    }
    else if ($("#TabContainerMain_tbDocumentInstructions_ddlPaymentSwift57").val() == "57D") {
        $("#TabContainerMain_tbDocumentInstructions_panel57ACC_No").css('display', 'block');
        $("#TabContainerMain_tbDocumentInstructions_panel57DName").css('display', 'block');
        $("#TabContainerMain_tbDocumentInstructions_panel57DAddress").css('display', 'block');
        $("#TabContainerMain_tbDocumentInstructions_panel57A").css('display', 'none');
        $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift57A").val('');
    }
    else {
        $("#TabContainerMain_tbDocumentInstructions_panel57ACC_No").css('display', 'none');
        $("#TabContainerMain_tbDocumentInstructions_panel57A").css('display', 'none');
        $("#TabContainerMain_tbDocumentInstructions_panel57DName").css('display', 'none');
        $("#TabContainerMain_tbDocumentInstructions_panel57DAddress").css('display', 'none');
    }
    if ($("#TabContainerMain_tbDocumentInstructions_ddlPaymentSwift58").val() == "58A") {
        $("#TabContainerMain_tbDocumentInstructions_panel58ACC_No").css('display', 'block');
        $("#TabContainerMain_tbDocumentInstructions_panel58A").css('display', 'block');
        $("#TabContainerMain_tbDocumentInstructions_panel58DName").css('display', 'none');
        $("#TabContainerMain_tbDocumentInstructions_panel58Address").css('display', 'none');
        $("#TabContainerMain_tbDocumentInstructions_panel58Address2").css('display', 'none');
        $("#TabContainerMain_tbDocumentInstructions_panel58Address3").css('display', 'none');
        $("#TabContainerMain_tbDocumentInstructions_panel58Address4").css('display', 'none');
        $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift58D_name").val('');
        $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift58D_Address").val('');
        $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift58D_Address2").val('');
        $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift58D_Address3").val('');
        $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift58D_Address4").val('');
    }
    else if ($("#TabContainerMain_tbDocumentInstructions_ddlPaymentSwift58").val() == "58D") {
        $("#TabContainerMain_tbDocumentInstructions_panel58ACC_No").css('display', 'block');
        $("#TabContainerMain_tbDocumentInstructions_panel58DName").css('display', 'block');
        $("#TabContainerMain_tbDocumentInstructions_panel58Address").css('display', 'block');
        $("#TabContainerMain_tbDocumentInstructions_panel58Address2").css('display', 'block');
        $("#TabContainerMain_tbDocumentInstructions_panel58Address3").css('display', 'block');
        $("#TabContainerMain_tbDocumentInstructions_panel58Address4").css('display', 'block');
        $("#TabContainerMain_tbDocumentInstructions_panel58A").css('display', 'none');
        $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift58A").val('');
    }
    else {
        $("#TabContainerMain_tbDocumentInstructions_panel58ACC_No").css('display', 'none');
        $("#TabContainerMain_tbDocumentInstructions_panel58A").css('display', 'none');
        $("#TabContainerMain_tbDocumentInstructions_panel58DName").css('display', 'none');
        $("#TabContainerMain_tbDocumentInstructions_panel58Address").css('display', 'none');
        $("#TabContainerMain_tbDocumentInstructions_panel58Address2").css('display', 'none');
        $("#TabContainerMain_tbDocumentInstructions_panel58Address3").css('display', 'none');
        $("#TabContainerMain_tbDocumentInstructions_panel58Address4").css('display', 'none');
    }
}
function Instrutionselection() {
    if ($("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_None").is(':checked')) {
        $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_Other").prop('checked', false);
        $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_Annexure").prop('checked', false);
        $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_On_Sight").prop('checked', false);
        $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_On_Date").prop('checked', false);
        $("#TabContainerMain_tbDocumentInstructions_rdb_ownLCDiscount_No").prop('checked', true);
    }
    if ($("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_Other").is(':checked')) {
        $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_None").prop('checked', false);
        $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_Annexure").prop('checked', false);
        $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_On_Sight").prop('checked', false);
        $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_On_Date").prop('checked', false);
        $("#TabContainerMain_tbDocumentInstructions_rdb_ownLCDiscount_No").prop('checked', true);
    }
    if ($("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_On_Sight").is(':checked')) {
        $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_None").prop('checked', false);
        $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_Other").prop('checked', false);
        $("#TabContainerMain_tbDocumentInstructions_rdb_ownLCDiscount_Yes").prop('checked', true);
    }
    if ($("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_On_Date").is(':checked')) {
        $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_None").prop('checked', false);
        $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_Other").prop('checked', false);
        $("#TabContainerMain_tbDocumentInstructions_rdb_ownLCDiscount_Yes").prop('checked', true);
    }
}
function OnNoneChange() {
    Instrutionselection();
    $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions1").val('');
    $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions2").val('');
    $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions3").val('');
    $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions4").val('');
    $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions5").val('');
    $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions1").prop('disabled', false);
    $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions2").prop('disabled', false);
    $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions3").prop('disabled', false);
    $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions4").prop('disabled', false);
    $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions5").prop('disabled', false);
}
function OnOthersChange() {
    OnNoneChange();
}
function OnAsPerAnnexureChange() {
    OnNoneChange();
    if ($("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_Annexure").is(':checked')) {
        $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions1").val('As Per Annexure');
    }
    if ($("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_Annexure").is(':checked') && $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_On_Sight").is(':checked')) {
        $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions1").val('As Per Annexure');
        $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions2").val('Beneficiary to be paid at sight as per LC terms.');
    }
    else if ($("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_Annexure").is(':checked') && $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_On_Date").is(':checked')) {
        $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions1").val('As Per Annexure');
        $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions2").val('Beneficiary to be paid on dated');
    }
}
function OnBenetobepaidonsightChange() {
    $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_None").prop('checked', false);
    $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_Other").prop('checked', false);

    OnNoneChange();

    if ($("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_Annexure").is(':checked') == false && $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_On_Sight").is(':checked')) {
        $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions1").val('Beneficiary to be paid at sight as per LC terms.');
    }
    else if ($("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_Annexure").is(':checked') && $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_On_Sight").is(':checked')) {
        $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions1").val('As Per Annexure');
        $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions2").val('Beneficiary to be paid at sight as per LC terms.');
    }
}
function OnBenetobepaidonDatedChange() {
    $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_None").prop('checked', false);
    $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_Other").prop('checked', false);

    OnNoneChange();
    if ($("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_Annexure").is(':checked') == false && $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_On_Date").is(':checked')) {
        $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions1").val('Beneficiary to be paid on dated');
    }
    else if ($("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_Annexure").is(':checked') && $("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_On_Date").is(':checked')) {
        $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions1").val('As Per Annexure');
        $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions2").val('Beneficiary to be paid on dated');
    }
}
function OnDocNextClick(index) {
    SaveUpdateData();
    var _ddl_Doc_Currency = $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency").val();
    var _txtBillAmount = $("#TabContainerMain_tbDocumentDetails_txtBillAmount").val();
    var _txtCustomer_ID = $("#TabContainerMain_tbDocumentDetails_txtCustomer_ID").val();
    var _txtDocNo = $("#txtDocNo").val();
    var tabContainer = $get('TabContainerMain');
    if ($("#txtDateReceived").val() == '') {
        alert('Received Date can not be blank.');
        tabContainer.control.set_activeTabIndex(0);
        $("#txtDateReceived").focus();
        return false;
    }
    if (_txtCustomer_ID == '') {
        alert('Customer can not be blank.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_txtCustomer_ID").focus();
        return false;
    }
    if (_txtBillAmount == '' || _txtBillAmount == 0) {
        alert('Bill Amount can not be blank.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_txtBillAmount").focus();
        return false;
    }
    if (_ddl_Doc_Currency == '') {
        alert('Please select Currency.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency").focus();
        return false;
    }
    if ($("#TabContainerMain_tbDocumentDetails_txtDueDate").val() == '') {
        alert('Please Enter Due Date.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_txtDueDate").focus();
        return false;
    }
    else {
        var DueDate = $("#TabContainerMain_tbDocumentDetails_txtDueDate").val();
        var DueDateDay = DueDate.split('/')[0];
        var DueDateMonth = DueDate.split('/')[1] - 1;
        var DueDateYear = DueDate.split('/')[2];

        var LodgDate = $("#txtLogdmentDate").val();
        var LodgDateDay = LodgDate.split('/')[0];
        var LodgDateMonth = LodgDate.split('/')[1] - 1;
        var LodgDateYear = LodgDate.split('/')[2];

        var LodgDateNew = new Date(LodgDateYear, LodgDateMonth, LodgDateDay);
        var DueDateNew = new Date(DueDateYear, DueDateMonth, DueDateDay);
        if (DueDateNew < LodgDateNew) {
            alert('Due date can not be Less than Lodgement date.');
            $("#TabContainerMain_tbDocumentDetails_txtDueDate").focus();
            return false;
        }
    }
    if ($("#TabContainerMain_tbDocumentDetails_ddlDrawer").val() == '') {
        alert('Please select Drawer.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_ddlDrawer").focus();
        return false;
    }
    if ($("#TabContainerMain_tbDocumentDetails_ddlCountryCode").val() == '') {
        alert('Select Country Code.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_ddlCountryCode").focus();
        return false;
    }
    if ($("#TabContainerMain_tbDocumentDetails_txtCommodityDesc").val() == '') {
        alert('Enter Goods Description.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_txtCommodityDesc").focus();
        return false;
    }
    //    if ($("#TabContainerMain_tbDocumentDetails_txt_Inv_No").val() == '') {
    //        alert('Enter Invoice No.');
    //        tabContainer.control.set_activeTabIndex(0);
    //        $("#TabContainerMain_tbDocumentDetails_txt_Inv_No").focus();
    //        return false;
    //    }
    if ($("#TabContainerMain_tbDocumentDetails_ddlTenor_Days_From").val() == 'BOEXCHANGE DATE' && $("#TabContainerMain_tbDocumentDetails_txtBOExchange").val() == '') {
        alert('Enter BOExchange Date.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_txtBOExchange").focus();
        return false;
    }
    if ($("#TabContainerMain_tbDocumentDetails_ddlTenor_Days_From").val() == 'INVOICE DATE' && $("#TabContainerMain_tbDocumentDetails_txt_Inv_Date").val() == '') {
        alert('Enter Invoice Date.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_txt_Inv_Date").focus();
        return false;
    }

    if ($("#TabContainerMain_tbDocumentDetails_ddlTenor_Days_From").val() == 'SHIPMENT DATE' && $("#TabContainerMain_tbDocumentDetails_txtShippingDate").val() == '') {
        alert('Enter Shipping Date.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_txtShippingDate").focus();
        return false;
    }
    if ($("#TabContainerMain_tbDocumentDetails_txtShippingDate").val() != '' && $("#TabContainerMain_tbDocumentDetails_txtVesselName").val() == '') {
        alert('Enter Vessel Name.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_txtVesselName").focus();
        return false;
    }
    if ($("#TabContainerMain_tbDocumentDetails_txtShippingDate").val() != '' && $("#TabContainerMain_tbDocumentDetails_txtFromPort").val() == '') {
        alert('Enter From port.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_txtFromPort").focus();
        return false;
    }
    if ($("#TabContainerMain_tbDocumentDetails_txtShippingDate").val() != '' && $("#TabContainerMain_tbDocumentDetails_txtToPort").val() == '') {
        alert('Enter To port.');
        tabContainer.control.set_activeTabIndex(0);
        $("#TabContainerMain_tbDocumentDetails_txtToPort").focus();
        return false;
    }
    if ($("#lblCollection_Lodgment_UnderLC").text() == "Lodgment_Under_LC") {
        if ($("#TabContainerMain_tbDocumentDetails_txt_LC_No").val() == '') {
            alert('LC No can not be blank.');
            tabContainer.control.set_activeTabIndex(0);
            $("#TabContainerMain_tbDocumentDetails_txt_LC_No").focus();
            return false;
        }
        if ($("#TabContainerMain_tbDocumentDetails_txtNego_Date").val() == '') {
            alert('Please Enter Negotiation Date.');
            tabContainer.control.set_activeTabIndex(0);
            $("#TabContainerMain_tbDocumentDetails_txtNego_Date").focus();
            return false;
        }
        if ($("#TabContainerMain_tbDocumentDetails_ddlDrawer").val() == '') {
            alert('Select Drawer.');
            tabContainer.control.set_activeTabIndex(0);
            $("#TabContainerMain_tbDocumentDetails_ddlDrawer").focus();
            return false;
        }
        if ($("#TabContainerMain_tbDocumentDetails_ddl_Nego_Remit_Bank").val() == '') {
            alert('Select Nego / Remit Bank Type.');
            tabContainer.control.set_activeTabIndex(0);
            $("#TabContainerMain_tbDocumentDetails_ddl_Nego_Remit_Bank").focus();
            return false;
        }
        if ($("#TabContainerMain_tbDocumentDetails_txtNego_Remit_Bank").val() == '') {
            alert('Swift/IFSC Code can not be blank.');
            tabContainer.control.set_activeTabIndex(0);
            $("#TabContainerMain_tbDocumentDetails_txtNego_Remit_Bank").focus();
            return false;
        }
        if ($("#TabContainerMain_tbDocumentDetails_ddl_Doc_Scrutiny").val() == '2' && $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_1").val() == '') {
            alert('Discrepancy can not be blank for Discrepant Document.');
            tabContainer.control.set_activeTabIndex(0);
            $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_1").focus();
            return false;
        }
    }
    if ($("#lblCollection_Lodgment_UnderLC").text() == "Collection") {
        if ($("#TabContainerMain_tbDocumentDetails_ddlDrawer").val() == '') {
            alert('Select Drawer.');
            tabContainer.control.set_activeTabIndex(0);
            $("#TabContainerMain_tbDocumentDetails_ddlDrawer").focus();
            return false;
        }
        if ($("#TabContainerMain_tbDocumentDetails_ddl_Nego_Remit_Bank").val() == '') {
            alert('Select Nego / Remit Bank Type.');
            tabContainer.control.set_activeTabIndex(0);
            $("#TabContainerMain_tbDocumentDetails_ddl_Nego_Remit_Bank").focus();
            return false;
        }
        if ($("#TabContainerMain_tbDocumentDetails_txtNego_Remit_Bank").val() == '') {
            alert('Swift/IFSC Code can not be blank.');
            tabContainer.control.set_activeTabIndex(0);
            $("#TabContainerMain_tbDocumentDetails_txtNego_Remit_Bank").focus();
            return false;
        }
        if ($("#TabContainerMain_tbDocumentDetails_ddl_Commodity").val() == '') {
            alert('Select Commodity Code.');
            tabContainer.control.set_activeTabIndex(0);
            $("#TabContainerMain_tbDocumentDetails_ddl_Commodity").focus();
            return false;
        }
        if ($("#TabContainerMain_tbDocumentDetails_ddlCountryCode").val() == '') {
            alert('Select Country Code.');
            tabContainer.control.set_activeTabIndex(0);
            $("#TabContainerMain_tbDocumentDetails_ddlCountryCode").focus();
            return false;
        }
    }

    if ($("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions1").val().length > 60) {
        alert('Max lenght is 60 Character for Special Instructions1.');
        //tabContainer.control.set_activeTabIndex(2);
        $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions1").focus();
        return false;
    }
    if ($("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions2").val().length > 60) {
        alert('Max lenght is 60 Character for Special Instructions2.');
        tabContainer.control.set_activeTabIndex(2);
        $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions2").focus();
        return false;
    }
    if ($("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions3").val().length > 60) {
        alert('Max lenght is 60 Character for Special Instructions3.');
        tabContainer.control.set_activeTabIndex(2);
        $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions3").focus();
        return false;
    }
    if ($("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions4").val().length > 60) {
        alert('Max lenght is 60 Character for Special Instructions4.');
        tabContainer.control.set_activeTabIndex(2);
        $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions4").focus();
        return false;
    }
    if ($("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions5").val().length > 60) {
        alert('Max lenght is 60 Character for Special Instructions5.');
        tabContainer.control.set_activeTabIndex(2);
        $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions5").focus();
        return false;
    }
    var tabContainer = $get('TabContainerMain');
    tabContainer.control.set_activeTabIndex(index);
    return false;
}
function OnDaysFromChange() {
    DuedateCal();
    if ($("#lblSight_Usance").text() == 'Usance') {
        if ($("#TabContainerMain_tbDocumentDetails_ddlTenor_Days_From").val() == 'OTHERS/BLANK') {
            $("#TabContainerMain_tbDocumentDetails_txtTenor_Description").prop('disabled', false);
        }
        else {
            $("#TabContainerMain_tbDocumentDetails_txtTenor_Description").prop('disabled', true);
        }
    }
    $("#TabContainerMain_tbDocumentDetails_ddlTenor_Days_From").focus();
}
function OnTenorTextChange() {
    var TenorDays = $("#TabContainerMain_tbDocumentDetails_txtTenor");
    $.ajax({
        type: "POST",
        url: "TF_IMP_BOE_Maker.aspx/GetStampDutyCharges1",
        data: '{TenorDays:"' + TenorDays.val() + '"}',
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.d.StampDutyPerThousand != "") {
                $("#hdnStamp_Duty_Per_Thousand").val(data.d.StampDutyPerThousand);
                Total_Bill_Amount('a');
            }
            else {
                $("#hdnStamp_Duty_Per_Thousand").val('');
            }
        },
        failure: function (data) { alert(data.d); TenorDays.focus(); },
        error: function (data) { alert(data.d); TenorDays.focus(); }
    });
    DuedateCal();
}
function OnTenorChange() {
    var _lblSight_Usance = $("#lblSight_Usance").text();
    var _ddlTenor = $("#TabContainerMain_tbDocumentDetails_ddlTenor");
    if (_lblSight_Usance == "Usance" && _ddlTenor.val() == "1") {
        alert('For Usance type Tenor cannot be Sight.');
        _ddlTenor.val('-Select-');
        _ddlTenor.focus();
    }
    DuedateCal();
    _ddlTenor.focus();
}
function OnCountryCodeChange() {
    var CountryID = $("#TabContainerMain_tbDocumentDetails_ddlCountryCode");
    $.ajax({
        type: "POST",
        url: "TF_IMP_BOE_Maker.aspx/fill_Country_Description",
        data: '{CountryID:"' + CountryID.val() + '"}',
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.d.CountryDesc != "") {
                $("#TabContainerMain_tbDocumentDetails_lblCountryDesc").text(data.d.CountryDesc);
                $("#TabContainerMain_tbAmlDetails_txtAMLCountry").val(data.d.CountryDesc);
                if (data.d.CountrySStatus == "false") {
                    alert('Selected Country is in Sanctioned Country list.')
                }
                CountryID.focus();
            }
            else {
                $("#TabContainerMain_tbAmlDetails_txtAMLCountry").val('');
                $("#TabContainerMain_tbDocumentDetails_lblCountryDesc").text('');
                CountryID.focus();
            }
        },
        failure: function (data) { alert(data.d); CountryID.focus(); },
        error: function (data) { alert(data.d); CountryID.focus(); }
    });
}
function OnCommodityDescChange() {
    $("#TabContainerMain_tbAmlDetails_txtAMLCommodity").val($("#TabContainerMain_tbDocumentDetails_txtCommodityDesc").val());
}
function OnCommodityCodeChange() {
    var CommodityID = $("#TabContainerMain_tbDocumentDetails_ddl_Commodity");
    $.ajax({
        type: "POST",
        url: "TF_IMP_BOE_Maker.aspx/fill_GBaseCommodity_Description",
        data: '{CommodityID:"' + CommodityID.val() + '"}',
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.d.CommodityDesc != "") {
                if (data.d.CommodityDesc.length > 25) {
                    $("#TabContainerMain_tbDocumentDetails_lblCommodityDesc").text(data.d.CommodityDesc.substring(0, 25) + '...');
                    $("#TabContainerMain_tbDocumentDetails_lblCommodityDesc").attr('title', data.d.CommodityDesc);
                }
                else {
                    $("#TabContainerMain_tbDocumentDetails_lblCommodityDesc").text(data.d.CommodityDesc);
                }
                CommodityID.focus();
            }
            else {
                $("#TabContainerMain_tbDocumentDetails_lblCommodityDesc").text('');
                CommodityID.focus();
            }
        },
        failure: function (data) { alert(data.d); CommodityID.focus(); },
        error: function (data) { alert(data.d); CommodityID.focus(); }
    });
}
function OnReceivedChange() {
    var TodayDate = new Date();
    var RecivedDate = $("#txtDateReceived").val();
    var RecivedDateDay = RecivedDate.split('/')[0];
    var RecivedDateMonth = RecivedDate.split('/')[1] - 1;
    var RecivedDateYear = RecivedDate.split('/')[2];

    var LodgDate = $("#txtLogdmentDate").val();
    var LodgDateDay = LodgDate.split('/')[0];
    var LodgDateMonth = LodgDate.split('/')[1] - 1;
    var LodgDateYear = LodgDate.split('/')[2];

    var LodgDateNew = new Date(LodgDateYear, LodgDateMonth, LodgDateDay);
    var RecivedDateNew = new Date(RecivedDateYear, RecivedDateMonth, RecivedDateDay);
    if (RecivedDateNew > LodgDateNew) {
        alert('Recived date can not be greater than lodgement date.');
        $("#txtDateReceived").val('');
        $("#txtDateReceived").focus();
        return false;
    }
    else {
        DuedateCal();
        Exp_AcceptanceDateCal();
        $("#txtDateReceived").focus();
    }
}
function OnBoeExchangeDateChange() {
    var SightOrUsance = $("#lblSight_Usance").text();

    var BOExchangeDate = $("#TabContainerMain_tbDocumentDetails_txtBOExchange").val();
    var BOExchangeDateDay = BOExchangeDate.split('/')[0];
    var BOExchangeDateMonth = BOExchangeDate.split('/')[1] - 1;
    var BOExchangeDateYear = BOExchangeDate.split('/')[2];

    var LodgDate = $("#txtLogdmentDate").val();
    var LodgDateDay = LodgDate.split('/')[0];
    var LodgDateMonth = LodgDate.split('/')[1] - 1;
    var LodgDateYear = LodgDate.split('/')[2];

    var LodgDateNew = new Date(LodgDateYear, LodgDateMonth, LodgDateDay);
    var BOExchangeDateNew = new Date(BOExchangeDateYear, BOExchangeDateMonth, BOExchangeDateDay);
    if (BOExchangeDateNew > LodgDateNew) {
        alert('BOExchange date can not be greater than lodgement date.');
        $("#TabContainerMain_tbDocumentDetails_txtBOExchange").val('');
        $("#TabContainerMain_tbDocumentDetails_txtBOExchange").focus();
        return false;
    }
    else {
        if (SightOrUsance != "Sight") {
            DuedateCal();
        }
        $("#TabContainerMain_tbDocumentDetails_txtBOExchange").focus();
    }
}
function OnNegoRemiTypeChange() {
    /////////////////////////////////////////////////////////////////////////////
    $("#TabContainerMain_tbDocumentDetails_txt_Their_Ref_no").val('');
    ////////////////////////////////////////////////////////////////
    $("#TabContainerMain_tbDocumentDetails_txtNego_Remit_Bank").val('');
    $("#TabContainerMain_tbDocumentDetails_lbl_Nego_Remit_Bank").text('');
    $("#TabContainerMain_tbDocumentDetails_lbl_Nego_Remit_Bank_Addr").text('');
    $("#TabContainerMain_tbDocumentDetails_lbl_Nego_RemitSwift_IFSC").text('');
    $("#TabContainerMain_tbDocumentDetails_ddl_Nego_Remit_Bank").focus();

    if ($("#TabContainerMain_tbDocumentDetails_ddl_Nego_Remit_Bank").val() == 'LOCAL') {
        $("#TabContainerMain_tbDocumentInstructions_ddlPaymentSwift56").prop('disabled', true);
        $("#TabContainerMain_tbDocumentInstructions_ddlPaymentSwift57").prop('disabled', true);
        $("#TabContainerMain_tbDocumentInstructions_ddlPaymentSwift58").prop('disabled', true);

        $("#TabContainerMain_tbDocumentInstructions_ddlPaymentSwift56").val('');
        $("#TabContainerMain_tbDocumentInstructions_ddlPaymentSwift57").val('');
        $("#TabContainerMain_tbDocumentInstructions_ddlPaymentSwift58").val('');
        PaymentSwiftselection();
    }
    else {
        $("#TabContainerMain_tbDocumentInstructions_ddlPaymentSwift56").prop('disabled', false);
        $("#TabContainerMain_tbDocumentInstructions_ddlPaymentSwift57").prop('disabled', false);
        $("#TabContainerMain_tbDocumentInstructions_ddlPaymentSwift58").prop('disabled', false);
    }
}
function OnDueDateChange() {
    $("#TabContainerMain_tbDocumentDetails_txtDueDate").focus();
}
function OnNegoDateChange() {
    var NegoDate = $("#TabContainerMain_tbDocumentDetails_txtNego_Date").val();
    var NegoDateDay = NegoDate.split('/')[0];
    var NegoDateMonth = NegoDate.split('/')[1] - 1;
    var NegoDateYear = NegoDate.split('/')[2];

    var LodgDate = $("#txtLogdmentDate").val();
    var LodgDateDay = LodgDate.split('/')[0];
    var LodgDateMonth = LodgDate.split('/')[1] - 1;
    var LodgDateYear = LodgDate.split('/')[2];

    var LodgDateNew = new Date(LodgDateYear, LodgDateMonth, LodgDateDay);
    var NegoDateNew = new Date(NegoDateYear, NegoDateMonth, NegoDateDay);
    if (NegoDateNew > LodgDateNew) {
        alert('Nego date can not be greater than lodgement date.');
        $("#TabContainerMain_tbDocumentDetails_txtNego_Date").val('');
        $("#TabContainerMain_tbDocumentDetails_txtNego_Date").focus();
        return false;
    }
    else {
        $("#TabContainerMain_tbDocumentDetails_txtNego_Date").focus();
    }
}
function OnInvoiceDateChange() {
    var InvoiceDate = $("#TabContainerMain_tbDocumentDetails_txt_Inv_Date").val();
    var InvoiceDateDay = InvoiceDate.split('/')[0];
    var InvoiceDateMonth = InvoiceDate.split('/')[1] - 1;
    var InvoiceDateYear = InvoiceDate.split('/')[2];

    var LodgDate = $("#txtLogdmentDate").val();
    var LodgDateDay = LodgDate.split('/')[0];
    var LodgDateMonth = LodgDate.split('/')[1] - 1;
    var LodgDateYear = LodgDate.split('/')[2];

    var LodgDateNew = new Date(LodgDateYear, LodgDateMonth, LodgDateDay);
    var InvoiceDateNew = new Date(InvoiceDateYear, InvoiceDateMonth, InvoiceDateDay);
    if (InvoiceDateNew > LodgDateNew) {
        alert('Invoice date can not be greater than lodgement date.');
        $("#TabContainerMain_tbDocumentDetails_txt_Inv_Date").val('');
        $("#TabContainerMain_tbDocumentDetails_txt_Inv_Date").focus();
        return false;
    }
    else {
        DuedateCal();
        $("#TabContainerMain_tbDocumentDetails_txt_Inv_Date").focus();
    }
}
function OnShippmentDateChange() {
    var ShipmentDate = $("#TabContainerMain_tbDocumentDetails_txtShippingDate").val();
    var ShipmentDateDay = ShipmentDate.split('/')[0];
    var ShipmentDateMonth = ShipmentDate.split('/')[1] - 1;
    var ShipmentDateYear = ShipmentDate.split('/')[2];

    var LodgDate = $("#txtLogdmentDate").val();
    var LodgDateDay = LodgDate.split('/')[0];
    var LodgDateMonth = LodgDate.split('/')[1] - 1;
    var LodgDateYear = LodgDate.split('/')[2];

    var LodgDateNew = new Date(LodgDateYear, LodgDateMonth, LodgDateDay);
    var ShipmentDateNew = new Date(ShipmentDateYear, ShipmentDateMonth, ShipmentDateDay);
    if (ShipmentDateNew > LodgDateNew) {
        alert('Shipment date can not be greater than lodgement date.');
        $("#TabContainerMain_tbDocumentDetails_txtShippingDate").val('');
        $("#TabContainerMain_tbDocumentDetails_txtShippingDate").focus();
        return false;
    }
    else {
        DuedateCal();
        $("#TabContainerMain_tbDocumentDetails_txtShippingDate").focus();
    }
}
function Exp_AcceptanceDateCal() {
    var CurrID = $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency");
    var dateRecieved = $("#txtDateReceived").val();
    var branchcode = $("#hdnBranchCode").val();
    var DocType = $("#hdnDocType").val();
    $.ajax({
        type: "POST",
        url: "TF_IMP_BOE_Maker.aspx/Exp_AcceptanceDate",
        data: '{CurrID:"' + CurrID.val() + '",dateRecieved:"' + dateRecieved + '",branchcode:"' + branchcode + '",DocType:"' + DocType + '"}',
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.d.ExDate != "") {
                $("#TabContainerMain_tbDocumentDetails_txt_AcceptanceDate").val(data.d.ExDate);
                Toggel_Instruction_Date();
            }
            else {
                //$("#TabContainerMain_tbDocumentDetails_txtDueDate").val('');
            }
        },
        failure: function (data) { alert(data.d); },
        error: function (data) { alert(data.d); }
    });
}
function DuedateCal() {
    var CurrID = $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency");
    var dateRecieved = $("#txtDateReceived").val();
    var branchcode = $("#hdnBranchCode").val();
    var SightOrUsance = $("#lblSight_Usance").text();
    var Tenor = $("#TabContainerMain_tbDocumentDetails_ddlTenor").val();
    var TenorDaysFrom = $("#TabContainerMain_tbDocumentDetails_ddlTenor_Days_From").val();
    var ShippingDate = $("#TabContainerMain_tbDocumentDetails_txtShippingDate").val();
    var InvDate = $("#TabContainerMain_tbDocumentDetails_txt_Inv_Date").val();
    var BoeExchangeDate = $("#TabContainerMain_tbDocumentDetails_txtBOExchange").val();
    var TenorDays = $("#TabContainerMain_tbDocumentDetails_txtTenor").val();
    $.ajax({
        type: "POST",
        url: "TF_IMP_BOE_Maker.aspx/Duedate",
        data: '{CurrID:"' + CurrID.val() + '",dateRecieved:"' + dateRecieved + '",branchcode:"' + branchcode + '",SightOrUsance:"' + SightOrUsance + '",Tenor:"' + Tenor + '",TenorDaysFrom:"' + TenorDaysFrom + '",ShippingDate:"' + ShippingDate + '",InvDate:"' + InvDate + '",BoeExchangeDate:"' + BoeExchangeDate + '",TenorDays:"' + TenorDays + '"}',
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.d.DueDate != "") {
                $("#TabContainerMain_tbDocumentDetails_txtDueDate").val(data.d.DueDate);
            }
            else {
                //$("#TabContainerMain_tbDocumentDetails_txtDueDate").val('');
            }
        },
        failure: function (data) { alert(data.d); },
        error: function (data) { alert(data.d); }
    });
}
function OnCurrencyChange() {
    var CurrID = $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency");
    var _flcIlcType = $("#lblForeign_Local").text();
    $.ajax({
        type: "POST",
        url: "TF_IMP_BOE_Maker.aspx/fillCurrencyDetails",
        data: '{CurrID:"' + CurrID.val() + '"}',
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.d.CurrDesc != "false") {
                if (_flcIlcType == 'FOREIGN' && CurrID.val() == 'INR') {
                    alert('For Foreign bill currency can not be INR.');
                    $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency").val('Select');
                    $("#TabContainerMain_tbDocumentDetails_lbl_Doc_Currency").text('');
                    $("#TabContainerMain_tbDocumentDetails_ddl_Comission_Currency").val('Select');
                    $("#TabContainerMain_tbDocumentDetails_ddl_Interest_Currency").val('Select');
                    $("#TabContainerMain_tbDocumentDetails_ddl_Other_Currency").val('Select');
                    $("#TabContainerMain_tbDocumentDetails_ddl_Their_Commission_Currency").val('Select');
                    _ddl_Doc_Currency.focus();
                    return false;
                }
                $("#TabContainerMain_tbDocumentDetails_lbl_Doc_Currency").text(data.d.CurrDesc);
                $("#TabContainerMain_tbDocumentDetails_ddl_Comission_Currency").val(CurrID.val());
                $("#TabContainerMain_tbDocumentDetails_ddl_Interest_Currency").val(CurrID.val());
                $("#TabContainerMain_tbDocumentDetails_ddl_Other_Currency").val(CurrID.val());
                $("#TabContainerMain_tbDocumentDetails_ddl_Their_Commission_Currency").val(CurrID.val());
                if (CurrID.val() == 'INR') {
                    $("#TabContainerMain_tbDocumentDetails_ddlCountryOfOrigin").val('IN');
                }
                else {
                    $("#TabContainerMain_tbDocumentDetails_ddlCountryOfOrigin").val('');
                }
                DuedateCal();
                Exp_AcceptanceDateCal();
                CurrID.focus();
            }
            else {
                $("#TabContainerMain_tbDocumentDetails_lbl_Doc_Currency").text('');
                CurrID.focus();
            }
        },
        failure: function (data) { alert(data.d); txtCustomerID.focus(); },
        error: function (data) { alert(data.d); txtCustomerID.focus(); }
    });
    Total_Bill_Amount('a');
}
function ViewAMLReport() {
    var _SWIFT_File_Type = "AML";
    var _ddl_Nego_Remit_Bank_Type = "AML";
    var _txtDocNo = $("#txtDocNo").val();
    var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
    winame.focus();
    return false;
}
function OnDrawerChange() {
    var drawerName = $("#TabContainerMain_tbDocumentDetails_ddlDrawer option:selected").text();
    $("#TabContainerMain_tbAmlDetails_txtAMLDrawer").val(drawerName);
    $("#TabContainerMain_tbDocumentDetails_ddlDrawer").focus();
}
function ViewSwiftMessage() {
    SaveUpdateData();

    var ddl = $("#TabContainerMain_tbDocumentDetails_ddl_Nego_Remit_Bank");
    var Lodg = $("#lblCollection_Lodgment_UnderLC").text();
    if (ddl.val() != '') {
        if (ddl.val() == 'LOCAL') {
            alert("Swift File can not create for Local bank type.");
            return false;
        }
        else {
            if ($("#TabContainerMain_tbDocumentDetails_rdb_MT734").is(':checked') ||
                $("#TabContainerMain_tbDocumentDetails_rdb_MT799").is(':checked') ||
                $("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(':checked') ||
                $("#TabContainerMain_tbDocumentDetails_rdb_MT499").is(':checked')) {

                if (ValidateSwiftCreate() == true) {
                    var _txtDocNo = $("#txtDocNo").val();
                    var _SWIFT_File_Type = '';

                    if ($("#TabContainerMain_tbDocumentDetails_rdb_MT499").is(':checked')) {
                        _SWIFT_File_Type = 'MT499';
                    }
                    if ($("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(':checked')) {
                        _SWIFT_File_Type = 'MT999';
                    }
                    if ($("#TabContainerMain_tbDocumentDetails_cb_Protest").is(':checked') && $("#TabContainerMain_tbDocumentDetails_rdb_MT499").is(':checked')) {
                        _SWIFT_File_Type = 'Prot499';
                    }
                    if ($("#TabContainerMain_tbDocumentDetails_cb_Protest").is(':checked') && $("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(':checked')) {
                        _SWIFT_File_Type = 'Prot999';
                    }
                    if ($("#TabContainerMain_tbDocumentDetails_rdb_MT734").is(':checked')) {
                        _SWIFT_File_Type = 'MT734';
                    }
                    if ($("#TabContainerMain_tbDocumentDetails_rdb_MT799").is(':checked')) {
                        _SWIFT_File_Type = 'MT799';
                    }
                    if (Lodg == "Lodgment_Under_LC") {
                        if ($("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(':checked') && $("#hdnDocumentScrutiny").val() == '2') {
                            _SWIFT_File_Type = 'MT999LC';
                        }
                    }

                    var _ddl_Nego_Remit_Bank_Type = $("#TabContainerMain_tbDocumentDetails_ddl_Nego_Remit_Bank").val();
                    if (_ddl_Nego_Remit_Bank_Type == '0') {
                        _ddl_Nego_Remit_Bank_Type = '';
                    }

                    if (Lodg == "Lodgment_Under_LC") {
                        var ddlscrutiny = $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Scrutiny").val();
                        if (ddlscrutiny == 2) {
                            var _txt_Discrepancy1 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_1").val();
                            if (_txt_Discrepancy1 == "") {
                                alert('Discrepancy can not be blank for Discrepant Document.');
                                $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_1").focus();
                                return false;
                            }
                            else {
                                var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
                                winame.focus();
                                return false;
                            }
                        }
                        else {
                            var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
                            winame.focus();
                            return false;
                        }
                    }
                    else {
                        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
                        winame.focus();
                        return false;
                    }
                }
                else {
                    return false;
                }
            }
            else {
                alert("Please Select File Type.");
                return false;
            }
        }
    }
    else {
        alert("Please select bank type.");
        return false;
    }

}
function ViewSFMSMessage() {
    SaveUpdateData();
    var ddl = $("#TabContainerMain_tbDocumentDetails_ddl_Nego_Remit_Bank");
    var Lodg = $("#lblCollection_Lodgment_UnderLC").text();
    if (ddl.val() != '') {
        if (ddl.val() == 'FOREIGN') {
            alert("SFMS File can not create for Foreign bank type.");
            return false;
        }
        else {
            if ($("#TabContainerMain_tbDocumentDetails_rdb_MT734").is(':checked') ||
                 $("#TabContainerMain_tbDocumentDetails_rdb_MT799").is(':checked') ||
                 $("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(':checked') ||
                 $("#TabContainerMain_tbDocumentDetails_rdb_MT499").is(':checked')) {

                if (ValidateSFMSCreate() == true) {
                    var _txtDocNo = $("#txtDocNo").val();
                    if ($("#TabContainerMain_tbDocumentDetails_rdb_MT499").is(':checked')) {
                        _SWIFT_File_Type = 'MT499';
                    }
                    if ($("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(':checked')) {
                        _SWIFT_File_Type = 'MT999';
                    }
                    if ($("#TabContainerMain_tbDocumentDetails_cb_Protest").is(':checked') && $("#TabContainerMain_tbDocumentDetails_rdb_MT499").is(':checked')) {
                        _SWIFT_File_Type = 'Prot499';
                    }
                    if ($("#TabContainerMain_tbDocumentDetails_cb_Protest").is(':checked') && $("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(':checked')) {
                        _SWIFT_File_Type = 'Prot999';
                    }
                    if ($("#TabContainerMain_tbDocumentDetails_rdb_MT734").is(':checked')) {
                        _SWIFT_File_Type = 'MT734';
                    }
                    if ($("#TabContainerMain_tbDocumentDetails_rdb_MT799").is(':checked')) {
                        _SWIFT_File_Type = 'MT799';
                    }
                    if (Lodg == "Lodgment_Under_LC") {
                        if ($("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(':checked') && $("#hdnDocumentScrutiny").val() == '2') {
                            _SWIFT_File_Type = 'MT999LC';
                        }
                    }

                    var _ddl_Nego_Remit_Bank_Type = $("#TabContainerMain_tbDocumentDetails_ddl_Nego_Remit_Bank").val();
                    if (_ddl_Nego_Remit_Bank_Type == '0') {
                        _ddl_Nego_Remit_Bank_Type = '';
                    }
                    if (Lodg == "Lodgment_Under_LC") {
                        var ddlscrutiny = $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Scrutiny").val();
                        if (ddlscrutiny == 2) {
                            var _txt_Discrepancy1 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_1").val();
                            if (_txt_Discrepancy1 == "") {
                                alert('Discrepancy can not be blank for Discrepant Document.');
                                $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_1").focus();
                                return false;
                            }
                            else {
                                var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
                                winame.focus();
                                return false;
                            }
                        }
                        else {
                            var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
                            winame.focus();
                            return false;
                        }
                    }
                    else {
                        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
                        winame.focus();
                        return false;
                    }
                }
                else {
                    return false;
                }
            }
            else {
                alert("Please Select File Type.");
                return false;
            }
        }
    }
    else {
        alert("Please select bank type.");
        return false;
    }
}
function SaveUpdateLCData() {
    var hdnCustAbbr = document.getElementById('hdnCustAbbr').value;
    var hdnLCREF1 = document.getElementById('hdnLCREF1').value;
    var hdnLCREF2 = document.getElementById('hdnLCREF2').value;
    var hdnLCREF3 = document.getElementById('hdnLCREF3').value;
    var hdnLCCurrency = document.getElementById('hdnLCCurrency').value;
    var hdnLCAppNo = document.getElementById('hdnLCAppNo').value;
    var hdnLCCountry = document.getElementById('hdnLCCountry').value;
    var hdnLCCommCode = document.getElementById('hdnLCCommCode').value;
    var hdnUserName = document.getElementById('hdnUserName').value;
    var _Collection_Lodgment = $("#lblCollection_Lodgment_UnderLC").text();
    var _txtDocNo = $("#txtDocNo").val();
    var _txtCustomer_ID = $("#TabContainerMain_tbDocumentDetails_txtCustomer_ID").val();
    var LCNO = $("#TabContainerMain_tbDocumentDetails_txt_LC_No").val();
    var txtBillAmount = $("#TabContainerMain_tbDocumentDetails_txtBillAmount").val();
    var _ddl_Doc_Currency = $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency").val();
    if (_Collection_Lodgment != 'Collection') {
        if (_txtCustomer_ID != '' && _ddl_Doc_Currency != '' && txtBillAmount != '' && txtBillAmount != '0' && LCNO != '') {
            $.ajax({
                type: "POST",
                url: "TF_IMP_BOE_Maker.aspx/AddUpdateLCData",
                data: '{ hdnCustAbbr:"' + hdnCustAbbr + '",hdnLCREF1:"' + hdnLCREF1 + '",hdnLCREF2:"' + hdnLCREF2 + '",hdnLCCurrency:"' + hdnLCCurrency +
            '",hdnLCAppNo:"' + hdnLCAppNo + '",hdnLCCountry:"' + hdnLCCountry + '",hdnLCREF3:"' + hdnLCREF3 + '",txtCustomer_ID:"' + _txtCustomer_ID +
            '",txtDocNo:"' + _txtDocNo + '",hdnUserName:"' + hdnUserName + '",txtBillAmount:"' + txtBillAmount +
            '",hdnLCCommCode:"' + hdnLCCommCode + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
        }
    }
}

function SaveUpdateData() {
    var LodgCumAcc = 'N';
    if ($("#chkLodgCumAcc").is(":checked")) {
        LodgCumAcc = 'Y';
    }
    var hdnUserName = document.getElementById('hdnUserName').value;
    var _BranchCode = $("#hdnBranchCode").val();
    var _DocType = $("#hdnDocType").val();
    var _flcIlcType = $("#lblForeign_Local").text();
    var _Collection_Lodgment = $("#lblCollection_Lodgment_UnderLC").text();
    var _Sight_Usance = $("#lblSight_Usance").text();
    var _txtDocNo = $("#txtDocNo").val();
    var _txtDateReceived = $("#txtDateReceived").val();
    var _txtLogdmentDate = $("#txtLogdmentDate").val();
    var _txtCustomer_ID = $("#TabContainerMain_tbDocumentDetails_txtCustomer_ID").val();
    var _txt_AcceptanceDate = $("#TabContainerMain_tbDocumentDetails_txt_AcceptanceDate").val();
    var _txtBillAmount = $("#TabContainerMain_tbDocumentDetails_txtBillAmount").val();
    var _ddl_Doc_Currency = $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency").val();

    var _txt_LC_No = '', _txt_BRO_Ref_No = '', _SGDocNo='';
    if (_DocType == 'IBA' || _DocType == 'ACC') {
        _txt_LC_No = $("#TabContainerMain_tbDocumentDetails_txt_LC_No").val();
        _txt_BRO_Ref_No = $("#TabContainerMain_tbDocumentDetails_lblBRONo").text();
        _SGDocNo = $("#hdnSGDocNo").val();
    }

    var _DP_DA = '';
    if (_Collection_Lodgment == 'Collection') {
        if ($("#TabContainerMain_tbDocumentDetails_rbtDP").is(':checked')) {
            _DP_DA = 'Y';
        }
        else {
            _DP_DA = 'N';
        }
    }
    var _ddlTenor = $("#TabContainerMain_tbDocumentDetails_ddlTenor").val();

    var _txtTenor = $("#TabContainerMain_tbDocumentDetails_txtTenor").val();
    var _ddlTenor_Days_From = $("#TabContainerMain_tbDocumentDetails_ddlTenor_Days_From").val();
    var _txtTenor_Description = $("#TabContainerMain_tbDocumentDetails_txtTenor_Description").val();

    var _txtBOExchange = $("#TabContainerMain_tbDocumentDetails_txtBOExchange").val();
    var _txtDueDate = $("#TabContainerMain_tbDocumentDetails_txtDueDate").val();
    var _ddl_Nego_Remit_Bank_Type = $("#TabContainerMain_tbDocumentDetails_ddl_Nego_Remit_Bank").val();

    var _txtNego_Remit_Bank = $("#TabContainerMain_tbDocumentDetails_txtNego_Remit_Bank").val();
    var _txt_Their_Ref_no = $("#TabContainerMain_tbDocumentDetails_txt_Their_Ref_no").val();
    var _txtNego_Date = $("#TabContainerMain_tbDocumentDetails_txtNego_Date").val();
    var _txtAcwithInstitution = $("#TabContainerMain_tbDocumentDetails_txtAcwithInstitution").val();
    var _txt_Swift_Code = $("#TabContainerMain_tbDocumentDetails_txt_Swift_Code").val();
    var _txtReimbursingbank = $("#TabContainerMain_tbDocumentDetails_txtReimbursingbank").val();
    var _txt_Inv_No = $("#TabContainerMain_tbDocumentDetails_txt_Inv_No").val();
    var _ddlDrawer = $("#TabContainerMain_tbDocumentDetails_ddlDrawer").val();
    var _txt_Inv_Date = $("#TabContainerMain_tbDocumentDetails_txt_Inv_Date").val();
    var _ddl_Commodity = $("#TabContainerMain_tbDocumentDetails_ddl_Commodity").val();

    var _txtCommodityDesc = $("#TabContainerMain_tbDocumentDetails_txtCommodityDesc").val();
    var _ddlCountryCode = $("#TabContainerMain_tbDocumentDetails_ddlCountryCode").val();

    var _txt_CountryOfOrigin = $("#TabContainerMain_tbDocumentDetails_ddlCountryOfOrigin").val();
    var _txtShippingDate = $("#TabContainerMain_tbDocumentDetails_txtShippingDate").val();
    var _txtDocFirst1 = $("#TabContainerMain_tbDocumentDetails_txtDocFirst1").val();
    var _txtDocFirst2 = $("#TabContainerMain_tbDocumentDetails_txtDocFirst2").val();
    var _txtDocFirst3 = $("#TabContainerMain_tbDocumentDetails_txtDocFirst3").val();
    var _txtVesselName = $("#TabContainerMain_tbDocumentDetails_txtVesselName").val();
    var _txtDocSecond1 = $("#TabContainerMain_tbDocumentDetails_txtDocSecond1").val();
    var _txtDocSecond2 = $("#TabContainerMain_tbDocumentDetails_txtDocSecond2").val();
    var _txtDocSecond3 = $("#TabContainerMain_tbDocumentDetails_txtDocSecond3").val();
    var _txtFromPort = $("#TabContainerMain_tbDocumentDetails_txtFromPort").val();
    var _txtToPort = $("#TabContainerMain_tbDocumentDetails_txtToPort").val();
    var _ddl_Interest_Currency = $("#TabContainerMain_tbDocumentDetails_ddl_Interest_Currency").val();

    var _txt_Interest_Amount = $("#TabContainerMain_tbDocumentDetails_txt_Interest_Amount").val();
    var _ddl_Comission_Currency = $("#TabContainerMain_tbDocumentDetails_ddl_Comission_Currency").val();

    var _txtComissionAmount = $("#TabContainerMain_tbDocumentDetails_txtComissionAmount").val();
    var _ddl_Other_Currency = $("#TabContainerMain_tbDocumentDetails_ddl_Other_Currency").val();

    var _txtOther_amount = $("#TabContainerMain_tbDocumentDetails_txtOther_amount").val();
    var _ddl_Their_Commission_Currency = $("#TabContainerMain_tbDocumentDetails_ddl_Their_Commission_Currency").val();

    var _txtTheirCommission_Amount = $("#TabContainerMain_tbDocumentDetails_txtTheirCommission_Amount").val();
    var _txt_Total_Bill_Amt = $("#TabContainerMain_tbDocumentDetails_txt_Total_Bill_Amt").val();

    var _ddl_Doc_Scrutiny = '';
    if (_Collection_Lodgment == 'Lodgment_Under_LC' && $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Scrutiny").val() != '') {
        _ddl_Doc_Scrutiny = $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Scrutiny").val();
    }

    var _txt_Discrepancy1 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_1").val();
    var _txt_Discrepancy2 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_2").val();
    var _txt_Discrepancy3 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_3").val();
    var _txt_Discrepancy4 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_4").val();
    var _txt_Discrepancy5 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_5").val();
    var _txt_Discrepancy6 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_6").val();
    var _txt_Discrepancy7 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_7").val();
    var _txt_Discrepancy8 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_8").val();
    var _txt_Discrepancy9 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_9").val();
    var _txt_Discrepancy10 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_10").val();
    var _txt_Discrepancy11 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_11").val();
    var _txt_Discrepancy12 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_12").val();
    var _txt_Discrepancy13 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_13").val();
    var _txt_Discrepancy14 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_14").val();
    var _txt_Discrepancy15 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_15").val();
    var _txt_Discrepancy16 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_16").val();
    var _txt_Discrepancy17 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_17").val();
    var _txt_Discrepancy18 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_18").val();
    var _txt_Discrepancy19 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_19").val();
    var _txt_Discrepancy20 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_20").val();
    var _txt_Discrepancy21 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_21").val();
    var _txt_Discrepancy22 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_22").val();
    var _txt_Discrepancy23 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_23").val();
    var _txt_Discrepancy24 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_24").val();
    var _txt_Discrepancy25 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_25").val();
    var _txt_Discrepancy26 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_26").val();
    var _txt_Discrepancy27 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_27").val();
    var _txt_Discrepancy28 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_28").val();
    var _txt_Discrepancy29 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_29").val();
    var _txt_Discrepancy30 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_30").val();
    var _txt_Discrepancy31 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_31").val();
    var _txt_Discrepancy32 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_32").val();
    var _txt_Discrepancy33 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_33").val();
    var _txt_Discrepancy34 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_34").val();
    var _txt_Discrepancy35 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_35").val();

    var _SWIFT_File_Type = '';
    var _ProtestFlag = '';
    if ($("#TabContainerMain_tbDocumentDetails_rbd_None").is(':checked')) {
        _SWIFT_File_Type = '';
    }
    else if (_DocType == 'ICA' || _DocType == 'ICU') {
        if ($("#TabContainerMain_tbDocumentDetails_rdb_MT499").is(':checked')) {
            _SWIFT_File_Type = 'MT499';

            if ($("#TabContainerMain_tbDocumentDetails_cb_Protest").is(':checked')) {
                _ProtestFlag = 'Y';
            }
        }
        else if ($("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(':checked')) {
            _SWIFT_File_Type = 'MT999';

            if ($("#TabContainerMain_tbDocumentDetails_cb_Protest").is(':checked')) {
                _ProtestFlag = 'Y';
            }
        }

    }
    else if (_DocType == 'IBA' || _DocType == 'ACC') {
        if ($("#TabContainerMain_tbDocumentDetails_rdb_MT734").is(':checked')) {
            _SWIFT_File_Type = 'MT734';
        }
        else if ($("#TabContainerMain_tbDocumentDetails_rdb_MT799").is(':checked')) {
            _SWIFT_File_Type = 'MT799';
        }
        else if ($("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(':checked')) {
            _SWIFT_File_Type = 'MT999';

            if ($("#TabContainerMain_tbDocumentDetails_cb_Protest").is(':checked')) {
                _ProtestFlag = 'Y';
            }
        }
    }

    var _txt_Narrative1 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_1").val();
    var _txt_Narrative2 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_2").val();
    var _txt_Narrative3 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_3").val();
    var _txt_Narrative4 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_4").val();
    var _txt_Narrative5 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_5").val();
    var _txt_Narrative6 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_6").val();
    var _txt_Narrative7 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_7").val();
    var _txt_Narrative8 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_8").val();
    var _txt_Narrative9 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_9").val();
    var _txt_Narrative10 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_10").val();
    var _txt_Narrative11 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_11").val();
    var _txt_Narrative12 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_12").val();
    var _txt_Narrative13 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_13").val();
    var _txt_Narrative14 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_14").val();
    var _txt_Narrative15 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_15").val();
    var _txt_Narrative16 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_16").val();
    var _txt_Narrative17 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_17").val();
    var _txt_Narrative18 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_18").val();
    var _txt_Narrative19 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_19").val();
    var _txt_Narrative20 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_20").val();
    var _txt_Narrative21 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_21").val();
    var _txt_Narrative22 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_22").val();
    var _txt_Narrative23 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_23").val();
    var _txt_Narrative24 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_24").val();
    var _txt_Narrative25 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_25").val();
    var _txt_Narrative26 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_26").val();
    var _txt_Narrative27 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_27").val();
    var _txt_Narrative28 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_28").val();
    var _txt_Narrative29 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_29").val();
    var _txt_Narrative30 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_30").val();
    var _txt_Narrative31 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_31").val();
    var _txt_Narrative32 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_32").val();
    var _txt_Narrative33 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_33").val();
    var _txt_Narrative34 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_34").val();
    var _txt_Narrative35 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_35").val();

    var _SP_Instructions_Type = '';
    if ($("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_None").is(':checked')) {
        _SP_Instructions_Type = 'None';
    }
    else if ($("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_Other").is(':checked')) {
        _SP_Instructions_Type = 'Others';
    }
    else if ($("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_Annexure").is(':checked')) {
        _SP_Instructions_Type = 'Annexure';
    }
    var _SP_Instructions_Type1 = '';
    if ($("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_On_Sight").is(':checked')) {
        _SP_Instructions_Type1 = 'On_Sight';
    }
    else if ($("#TabContainerMain_tbDocumentInstructions_rdb_SP_Instr_On_Date").is(':checked')) {
        _SP_Instructions_Type1 = 'On_Date';
    }
    var _txt_SP_Instructions1 = $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions1").val();
    var _txt_SP_Instructions2 = $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions2").val();
    var _txt_SP_Instructions3 = $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions3").val();
    var _txt_SP_Instructions4 = $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions4").val();
    var _txt_SP_Instructions5 = $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions5").val();

    // _txt_Instructions are labels not text boxes 
    var _txt_Instructions1 = $("#TabContainerMain_tbDocumentInstructions_txt_Instructions1").text();
    var _txt_Instructions2 = $("#TabContainerMain_tbDocumentInstructions_txt_Instructions2").text();
    var _txt_Instructions3 = $("#TabContainerMain_tbDocumentInstructions_txt_Instructions3").text();
    var _txt_Instructions4 = $("#TabContainerMain_tbDocumentInstructions_txt_Instructions4").text();
    var _txt_Instructions5 = $("#TabContainerMain_tbDocumentInstructions_txt_Instructions5").text();

    var Instructiondate = '';

    //payment swift details
    var _Pay_swift_Type_56 = '';
    var _txt_PaymentSwift56A = '';
    var _txt_PaymentSwift56ACC_No = '';
    var _txt_PaymentSwift56D_name = '';
    var _txt_PaymentSwift56D_Address = '';

    var _Pay_swift_Type_57 = '';
    var _txt_PaymentSwift57A = '';
    var _txt_PaymentSwift57ACC_No = '';
    var _txt_PaymentSwift57D_name = '';
    var _txt_PaymentSwift57D_Address = '';

    var _Pay_swift_Type_58 = '';
    var _txt_PaymentSwift58A = '';
    var _txt_PaymentSwift58ACC_No = '';
    var _txt_PaymentSwift58D_name = '';
    var _txt_PaymentSwift58D_Address = '';
    var _txt_PaymentSwift58D_Address2 = '';
    var _txt_PaymentSwift58D_Address3 = '';
    var _txt_PaymentSwift58D_Address4 = '';

    //if (_Collection_Lodgment == 'Collection') {
    _Pay_swift_Type_56 = $("#TabContainerMain_tbDocumentInstructions_ddlPaymentSwift56").val();
    if (_Pay_swift_Type_56 == '56A') {
        _txt_PaymentSwift56ACC_No = $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift56ACC_No").val();
        _txt_PaymentSwift56A = $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift56A").val();
    }
    else if (_Pay_swift_Type_56 == '56D') {
        _txt_PaymentSwift56ACC_No = $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift56ACC_No").val();
        _txt_PaymentSwift56D_name = $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift56D_name").val();
        _txt_PaymentSwift56D_Address = $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift56D_Address").val();
    }

    _Pay_swift_Type_57 = $("#TabContainerMain_tbDocumentInstructions_ddlPaymentSwift57").val();
    if (_Pay_swift_Type_57 == '57A') {
        _txt_PaymentSwift57ACC_No = $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift57ACC_No").val();
        _txt_PaymentSwift57A = $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift57A").val();
    }
    else if (_Pay_swift_Type_57 == '57D') {
        _txt_PaymentSwift57ACC_No = $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift57ACC_No").val();
        _txt_PaymentSwift57D_name = $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift57D_name").val();
        _txt_PaymentSwift57D_Address = $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift57D_Address").val();
    }

    _Pay_swift_Type_58 = $("#TabContainerMain_tbDocumentInstructions_ddlPaymentSwift58").val();
    if (_Pay_swift_Type_58 == '58A') {
        _txt_PaymentSwift58ACC_No = $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift58ACC_No").val();
        _txt_PaymentSwift58A = $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift58A").val();
    }
    else if (_Pay_swift_Type_58 == '58D') {
        _txt_PaymentSwift58ACC_No = $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift58ACC_No").val();
        _txt_PaymentSwift58D_name = $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift58D_name").val();
        _txt_PaymentSwift58D_Address = $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift58D_Address").val();
        _txt_PaymentSwift58D_Address2 = $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift58D_Address2").val();
        _txt_PaymentSwift58D_Address3 = $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift58D_Address3").val();
        _txt_PaymentSwift58D_Address4 = $("#TabContainerMain_tbDocumentInstructions_txt_PaymentSwift58D_Address4").val();
    }

    //}
    var _OwnLCDiscount_Type = '';
    if ($("#TabContainerMain_tbDocumentInstructions_rdb_ownLCDiscount_Yes").is(':checked')) {
        _OwnLCDiscount_Type = 'Y';
    }
    else if ($("#TabContainerMain_tbDocumentInstructions_rdb_ownLCDiscount_No").is(':checked')) {
        _OwnLCDiscount_Type = 'N';
    }
    var Stamp_Duty_Charges_Curr = "";
    var Stamp_Duty_Charges_ExRate = "";
    var Stamp_Duty_Charges_Amount = "";
    if (_Collection_Lodgment == 'Collection') {
        Stamp_Duty_Charges_Curr = $("#TabContainerMain_tbDocumentDetails_ddl_Stamp_Duty_Charges_Curr").val();
        Stamp_Duty_Charges_ExRate = $("#TabContainerMain_tbDocumentDetails_txt_Stamp_Duty_Charges_ExRate").val();
        Stamp_Duty_Charges_Amount = $("#TabContainerMain_tbDocumentDetails_txt_Stamp_Duty_Charges_Amount").val();
    }

    var _txtAMLDrawee = $("#TabContainerMain_tbAmlDetails_txtAMLDrawee").val();
    var _txtAMLDrawer = $("#TabContainerMain_tbAmlDetails_txtAMLDrawer").val();
    var _txtAMLNagoRemiBank = $("#TabContainerMain_tbAmlDetails_txtAMLNagoRemiBank").val();
    //var _txtAMLSwiftCode = $("#TabContainerMain_tbAmlDetails_txtAMLSwiftCode").val();
    var _txtAMLCommodity = $("#TabContainerMain_tbAmlDetails_txtAMLCommodity").val();
    var _txtAMLVessel = $("#TabContainerMain_tbAmlDetails_txtAMLVessel").val();
    var _txtAMLFromPort = $("#TabContainerMain_tbAmlDetails_txtAMLFromPort").val();
    var _txtAMLCountry = $("#TabContainerMain_tbAmlDetails_txtAMLCountry").val();
    var _txtCountryOfOrigin = $("#TabContainerMain_tbAmlDetails_txtCountryOfOrigin").val();
    var _txtAML1 = $("#TabContainerMain_tbAmlDetails_txtAML1").val();
    var _txtAML2 = $("#TabContainerMain_tbAmlDetails_txtAML2").val();
    var _txtAML3 = $("#TabContainerMain_tbAmlDetails_txtAML3").val();
    var _txtAML4 = $("#TabContainerMain_tbAmlDetails_txtAML4").val();
    var _txtAML5 = $("#TabContainerMain_tbAmlDetails_txtAML5").val();
    var _txtAML6 = $("#TabContainerMain_tbAmlDetails_txtAML6").val();
    var _txtAML7 = $("#TabContainerMain_tbAmlDetails_txtAML7").val();
    var _txtAML8 = $("#TabContainerMain_tbAmlDetails_txtAML8").val();
    var _txtAML9 = $("#TabContainerMain_tbAmlDetails_txtAML9").val();
    var _txtAML10 = $("#TabContainerMain_tbAmlDetails_txtAML10").val();
    var _txtAML11 = $("#TabContainerMain_tbAmlDetails_txtAML11").val();
    var _txtAML12 = $("#TabContainerMain_tbAmlDetails_txtAML12").val();
    var _txtAML13 = $("#TabContainerMain_tbAmlDetails_txtAML13").val();
    var _txtAML14 = $("#TabContainerMain_tbAmlDetails_txtAML14").val();
    var _txtAML15 = $("#TabContainerMain_tbAmlDetails_txtAML15").val();
    var _txtAML16 = $("#TabContainerMain_tbAmlDetails_txtAML16").val();
    var _txtAML17 = $("#TabContainerMain_tbAmlDetails_txtAML17").val();
    var _txtAML18 = $("#TabContainerMain_tbAmlDetails_txtAML18").val();
    var _txtAML19 = $("#TabContainerMain_tbAmlDetails_txtAML19").val();
    var _txtAML20 = $("#TabContainerMain_tbAmlDetails_txtAML20").val();
    var _txtAML21 = $("#TabContainerMain_tbAmlDetails_txtAML21").val();
    var _txtAML22 = $("#TabContainerMain_tbAmlDetails_txtAML22").val();
    var _txtAML23 = $("#TabContainerMain_tbAmlDetails_txtAML23").val();
    var _txtAML24 = $("#TabContainerMain_tbAmlDetails_txtAML24").val();
    var _txtAML25 = $("#TabContainerMain_tbAmlDetails_txtAML25").val();
    var _txtAML26 = $("#TabContainerMain_tbAmlDetails_txtAML26").val();
    var _txtAML27 = $("#TabContainerMain_tbAmlDetails_txtAML27").val();
    var _txtAML28 = $("#TabContainerMain_tbAmlDetails_txtAML28").val();
    var _txtAML29 = $("#TabContainerMain_tbAmlDetails_txtAML29").val();
    var _txtAML30 = $("#TabContainerMain_tbAmlDetails_txtAML30").val();

    var _chkSpecialCase = "N";
    var _txt_txtRiskCust = "";
    var _txt_txtSettelementAcNo = "";
    var _txt_txtRiskCustAbbr = "";
    if ($("#TabContainerMain_tbDocumentDetails_chkSpecialCase").is(":checked")) {
        _chkSpecialCase = "Y";
        var _txt_txtRiskCust = $("#TabContainerMain_tbDocumentDetails_txtRiskCust").val();
        var _txt_txtSettelementAcNo = $("#TabContainerMain_tbDocumentDetails_txtSettelementAcNo").val();
        var _txt_txtRiskCustAbbr = $("#TabContainerMain_tbDocumentDetails_txtRiskCustAbbr").val();
    }

    // Swift Details
    var _txtChargesClaimed7341 = '', _txtChargesClaimed7342 = '', _txtChargesClaimed7343 = '', _txtChargesClaimed7344 = '', _txtChargesClaimed7345 = '', _txtChargesClaimed7346 = '',

        _ddlTotalAmountClaimed734 = '', _txtDate734 = '', _txtCurrency734 = '', _txtAmount734 = '',

        _txtAccountWithBank734PartyIdentifier = '', _ddlAccountWithBank734 = '', _txtAccountWithBank734AccountNo = '', _txtAccountWithBank734SwiftCode = '', _txtAccountWithBank734Location = '', _txtAccountWithBank734Name = '',
        _txtAccountWithBank734Address1 = '', _txtAccountWithBank734Address2 = '', _txtAccountWithBank734Address3 = '',

        _txtSendertoReceiverInformation7341 = '', _txtSendertoReceiverInformation7342 = '', _txtSendertoReceiverInformation7343 = '',
        _txtSendertoReceiverInformation7344 = '', _txtSendertoReceiverInformation7345 = '', _txtSendertoReceiverInformation7346 = '',
        _ddl_DisposalOfDoc = '';
    if (_DocType == 'IBA' || _DocType == 'ACC') {
        _txtChargesClaimed7341 = $("#TabContainerMain_tbSwift_txtChargesClaimed7341").val();
        _txtChargesClaimed7342 = $("#TabContainerMain_tbSwift_txtChargesClaimed7342").val();
        _txtChargesClaimed7343 = $("#TabContainerMain_tbSwift_txtChargesClaimed7343").val();
        _txtChargesClaimed7344 = $("#TabContainerMain_tbSwift_txtChargesClaimed7344").val();
        _txtChargesClaimed7345 = $("#TabContainerMain_tbSwift_txtChargesClaimed7345").val();
        _txtChargesClaimed7346 = $("#TabContainerMain_tbSwift_txtChargesClaimed7346").val();

        _ddlTotalAmountClaimed734 = $("#TabContainerMain_tbSwift_ddlTotalAmountClaimed734").val();
        if (_ddlTotalAmountClaimed734 == 'A') {
            _txtDate734 = $("#TabContainerMain_tbSwift_txtDate734").val();
            _txtCurrency734 = $("#TabContainerMain_tbSwift_txtCurrency734").val();
            _txtAmount734 = $("#TabContainerMain_tbSwift_txtAmount734").val();
        }
        if (_ddlTotalAmountClaimed734 == 'B') {
            _txtCurrency734 = $("#TabContainerMain_tbSwift_txtCurrency734").val();
            _txtAmount734 = $("#TabContainerMain_tbSwift_txtAmount734").val();
        }

        _ddlAccountWithBank734 = $("#TabContainerMain_tbSwift_ddlAccountWithBank734").val();
        _txtAccountWithBank734PartyIdentifier = $("#TabContainerMain_tbSwift_txtAccountWithBank734PartyIdentifier").val();
        if (_ddlAccountWithBank734 == 'A') {
            _txtAccountWithBank734AccountNo = $("#TabContainerMain_tbSwift_txtAccountWithBank734AccountNo").val();
            _txtAccountWithBank734SwiftCode = $("#TabContainerMain_tbSwift_txtAccountWithBank734SwiftCode").val();
        }
        if (_ddlAccountWithBank734 == 'B') {
            _txtAccountWithBank734AccountNo = $("#TabContainerMain_tbSwift_txtAccountWithBank734AccountNo").val();
            _txtAccountWithBank734Location = $("#TabContainerMain_tbSwift_txtAccountWithBank734Location").val();
        }
        if (_ddlAccountWithBank734 == 'D') {
            _txtAccountWithBank734AccountNo = $("#TabContainerMain_tbSwift_txtAccountWithBank734AccountNo").val();
            _txtAccountWithBank734Name = $("#TabContainerMain_tbSwift_txtAccountWithBank734Name").val();
            _txtAccountWithBank734Address1 = $("#TabContainerMain_tbSwift_txtAccountWithBank734Address1").val();
            _txtAccountWithBank734Address2 = $("#TabContainerMain_tbSwift_txtAccountWithBank734Address2").val();
            _txtAccountWithBank734Address3 = $("#TabContainerMain_tbSwift_txtAccountWithBank734Address3").val();
        }

        _txtSendertoReceiverInformation7341 = $("#TabContainerMain_tbSwift_txtSendertoReceiverInformation7341").val();
        _txtSendertoReceiverInformation7342 = $("#TabContainerMain_tbSwift_txtSendertoReceiverInformation7342").val();
        _txtSendertoReceiverInformation7343 = $("#TabContainerMain_tbSwift_txtSendertoReceiverInformation7343").val();
        _txtSendertoReceiverInformation7344 = $("#TabContainerMain_tbSwift_txtSendertoReceiverInformation7344").val();
        _txtSendertoReceiverInformation7345 = $("#TabContainerMain_tbSwift_txtSendertoReceiverInformation7345").val();
        _txtSendertoReceiverInformation7346 = $("#TabContainerMain_tbSwift_txtSendertoReceiverInformation7346").val();

        _ddl_DisposalOfDoc = $("#TabContainerMain_tbSwift_ddl_DisposalOfDoc").val();
    }


    if (_txtCustomer_ID != '' && _ddl_Doc_Currency != '' && _txtBillAmount != '' && _txtBillAmount != 0) {
        $.ajax({
            type: "POST",
            url: "TF_IMP_BOE_Maker.aspx/AddUpdateBoe",
            data: '{ hdnUserName:"' + hdnUserName + '",_BranchCode:"' + _BranchCode + '",_DocType:"' + _DocType + '",_flcIlcType:"' + _flcIlcType + '",_Sight_Usance:"' + _Sight_Usance +
            '", _txtDocNo: "' + _txtDocNo + '", _txtDateReceived: "' + _txtDateReceived + '", _txtLogdmentDate: "' + _txtLogdmentDate + '", _txtCustomer_ID: "' + _txtCustomer_ID +
            '", _txt_AcceptanceDate: "' + _txt_AcceptanceDate + '", _txtBillAmount: "' + _txtBillAmount + '", _ddl_Doc_Currency: "' + _ddl_Doc_Currency + '", _txt_LC_No: "' + _txt_LC_No + '", _txt_BRO_Ref_No: "' + _txt_BRO_Ref_No + '", _SGDocNo: "' + _SGDocNo + '", _DP_DA: "' + _DP_DA +
            '", _ddlTenor: "' + _ddlTenor + '", _txtTenor: "' + _txtTenor + '",_ddlTenor_Days_From: "' + _ddlTenor_Days_From + '", _txtTenor_Description: "' + _txtTenor_Description + '", _txtBOExchange: "' + _txtBOExchange + '", _txtDueDate: "' + _txtDueDate +
            '", _ddl_Nego_Remit_Bank_Type: "' + _ddl_Nego_Remit_Bank_Type + '",_txtNego_Remit_Bank: "' + _txtNego_Remit_Bank + '", _txt_Their_Ref_no: "' + _txt_Their_Ref_no + '", _txtNego_Date: "' + _txtNego_Date +
            '", _txtAcwithInstitution: "' + _txtAcwithInstitution + '", _txt_Swift_Code: "' + _txt_Swift_Code + '", _txtReimbursingbank: "' + _txtReimbursingbank + '", _txt_Inv_No: "' + _txt_Inv_No +
            '", _ddlDrawer: "' + _ddlDrawer + '", _txt_Inv_Date: "' + _txt_Inv_Date +
            '", _ddl_Commodity: "' + _ddl_Commodity + '", _txtCommodityDesc: "' + _txtCommodityDesc + '", _ddlCountryCode: "' + _ddlCountryCode + '", _txt_CountryOfOrigin: "' + _txt_CountryOfOrigin +
            '", _txtShippingDate: "' + _txtShippingDate + '", _txtDocFirst1: "' + _txtDocFirst1 + '", _txtDocFirst2: "' + _txtDocFirst2 + '", _txtDocFirst3: "' + _txtDocFirst3 +
            '", _txtVesselName: "' + _txtVesselName + '", _txtDocSecond1: "' + _txtDocSecond1 + '", _txtDocSecond2: "' + _txtDocSecond2 + '", _txtDocSecond3: "' + _txtDocSecond3 +
            '", _txtFromPort: "' + _txtFromPort + '", _txtToPort: "' + _txtToPort +
            '", _ddl_Interest_Currency: "' + _ddl_Interest_Currency + '", _txt_Interest_Amount: "' + _txt_Interest_Amount + '", _ddl_Comission_Currency: "' + _ddl_Comission_Currency + '", _txtComissionAmount: "' + _txtComissionAmount +
            '", _ddl_Other_Currency: "' + _ddl_Other_Currency + '", _txtOther_amount: "' + _txtOther_amount + '", _ddl_Their_Commission_Currency: "' + _ddl_Their_Commission_Currency + '", _txtTheirCommission_Amount: "' + _txtTheirCommission_Amount +
            '", _txt_Total_Bill_Amt: "' + _txt_Total_Bill_Amt + '", _ddl_Doc_Scrutiny: "' + _ddl_Doc_Scrutiny +
            '", _txt_Discrepancy1: "' + _txt_Discrepancy1 + '", _txt_Discrepancy2: "' + _txt_Discrepancy2 + '", _txt_Discrepancy3: "' + _txt_Discrepancy3 + '", _txt_Discrepancy4: "' + _txt_Discrepancy4 + '", _txt_Discrepancy5: "' + _txt_Discrepancy5 +
            '", _txt_Discrepancy6: "' + _txt_Discrepancy6 + '", _txt_Discrepancy7: "' + _txt_Discrepancy7 + '", _txt_Discrepancy8: "' + _txt_Discrepancy8 + '", _txt_Discrepancy9: "' + _txt_Discrepancy9 + '", _txt_Discrepancy10: "' + _txt_Discrepancy10 +
            '", _txt_Discrepancy11: "' + _txt_Discrepancy11 + '", _txt_Discrepancy12: "' + _txt_Discrepancy12 + '", _txt_Discrepancy13: "' + _txt_Discrepancy13 + '", _txt_Discrepancy14: "' + _txt_Discrepancy14 + '", _txt_Discrepancy15: "' + _txt_Discrepancy15 +
            '", _txt_Discrepancy16: "' + _txt_Discrepancy16 + '", _txt_Discrepancy17: "' + _txt_Discrepancy17 + '", _txt_Discrepancy18: "' + _txt_Discrepancy18 + '", _txt_Discrepancy19: "' + _txt_Discrepancy19 + '", _txt_Discrepancy20: "' + _txt_Discrepancy20 +
            '", _txt_Discrepancy21: "' + _txt_Discrepancy21 + '", _txt_Discrepancy22: "' + _txt_Discrepancy22 + '", _txt_Discrepancy23: "' + _txt_Discrepancy23 + '", _txt_Discrepancy24: "' + _txt_Discrepancy24 + '", _txt_Discrepancy25: "' + _txt_Discrepancy25 +
            '", _txt_Discrepancy26: "' + _txt_Discrepancy26 + '", _txt_Discrepancy27: "' + _txt_Discrepancy27 + '", _txt_Discrepancy28: "' + _txt_Discrepancy28 + '", _txt_Discrepancy29: "' + _txt_Discrepancy29 + '", _txt_Discrepancy30: "' + _txt_Discrepancy30 +
            '", _txt_Discrepancy31: "' + _txt_Discrepancy31 + '", _txt_Discrepancy32: "' + _txt_Discrepancy32 + '", _txt_Discrepancy33: "' + _txt_Discrepancy33 + '", _txt_Discrepancy34: "' + _txt_Discrepancy34 + '", _txt_Discrepancy35: "' + _txt_Discrepancy35 +
            '", _SWIFT_File_Type:"' + _SWIFT_File_Type + '", _ProtestFlag:"' + _ProtestFlag +
            '", _txt_Narrative1: "' + _txt_Narrative1 + '", _txt_Narrative2: "' + _txt_Narrative2 + '", _txt_Narrative3: "' + _txt_Narrative3 + '", _txt_Narrative4: "' + _txt_Narrative4 + '", _txt_Narrative5: "' + _txt_Narrative5 +
            '", _txt_Narrative6: "' + _txt_Narrative6 + '", _txt_Narrative7: "' + _txt_Narrative7 + '", _txt_Narrative8: "' + _txt_Narrative8 + '", _txt_Narrative9: "' + _txt_Narrative9 + '", _txt_Narrative10: "' + _txt_Narrative10 +
            '", _txt_Narrative11: "' + _txt_Narrative11 + '", _txt_Narrative12: "' + _txt_Narrative12 + '", _txt_Narrative13: "' + _txt_Narrative13 + '", _txt_Narrative14: "' + _txt_Narrative14 + '", _txt_Narrative15: "' + _txt_Narrative15 +
            '", _txt_Narrative16: "' + _txt_Narrative16 + '", _txt_Narrative17: "' + _txt_Narrative17 + '", _txt_Narrative18: "' + _txt_Narrative18 + '", _txt_Narrative19: "' + _txt_Narrative19 + '", _txt_Narrative20: "' + _txt_Narrative20 +
            '", _txt_Narrative21: "' + _txt_Narrative21 + '", _txt_Narrative22: "' + _txt_Narrative22 + '", _txt_Narrative23: "' + _txt_Narrative23 + '", _txt_Narrative24: "' + _txt_Narrative24 + '", _txt_Narrative25: "' + _txt_Narrative25 +
            '", _txt_Narrative26: "' + _txt_Narrative26 + '", _txt_Narrative27: "' + _txt_Narrative27 + '", _txt_Narrative28: "' + _txt_Narrative28 + '", _txt_Narrative29: "' + _txt_Narrative29 + '", _txt_Narrative30: "' + _txt_Narrative30 +
            '", _txt_Narrative31: "' + _txt_Narrative31 + '", _txt_Narrative32: "' + _txt_Narrative32 + '", _txt_Narrative33: "' + _txt_Narrative33 + '", _txt_Narrative34: "' + _txt_Narrative34 + '", _txt_Narrative35: "' + _txt_Narrative35 +
            '",_SP_Instructions_Type:"' + _SP_Instructions_Type + '",_SP_Instructions_Type1:"' + _SP_Instructions_Type1 + '", _txt_SP_Instructions1: "' + _txt_SP_Instructions1 + '", _txt_SP_Instructions2: "' + _txt_SP_Instructions2 + '", _txt_SP_Instructions3: "' + _txt_SP_Instructions3 + '", _txt_SP_Instructions4: "' + _txt_SP_Instructions4 + '", _txt_SP_Instructions5: "' + _txt_SP_Instructions5 +
            '",_txt_Instructions1:"' + _txt_Instructions1 + '",_txt_Instructions2:"' + _txt_Instructions2 + '",_txt_Instructions3:"' + _txt_Instructions3 + '",_txt_Instructions4:"' + _txt_Instructions4 + '",_txt_Instructions5:"' + _txt_Instructions5 +
            '",_Instructiondate:"' + Instructiondate +
            '",_Pay_swift_Type_56:"' + _Pay_swift_Type_56 + '",_txt_PaymentSwift56ACC_No:"' + _txt_PaymentSwift56ACC_No + '",_txt_PaymentSwift56A:"' + _txt_PaymentSwift56A + '",_txt_PaymentSwift56D_name:"' + _txt_PaymentSwift56D_name + '",_txt_PaymentSwift56D_Address:"' + _txt_PaymentSwift56D_Address +
            '",_Pay_swift_Type_57:"' + _Pay_swift_Type_57 + '",_txt_PaymentSwift57ACC_No:"' + _txt_PaymentSwift57ACC_No + '",_txt_PaymentSwift57A:"' + _txt_PaymentSwift57A + '",_txt_PaymentSwift57D_name:"' + _txt_PaymentSwift57D_name + '",_txt_PaymentSwift57D_Address:"' + _txt_PaymentSwift57D_Address +
            '",_Pay_swift_Type_58:"' + _Pay_swift_Type_58 + '",_txt_PaymentSwift58ACC_No:"' + _txt_PaymentSwift58ACC_No + '",_txt_PaymentSwift58A:"' + _txt_PaymentSwift58A + '",_txt_PaymentSwift58D_name:"' + _txt_PaymentSwift58D_name + '",_txt_PaymentSwift58D_Address:"' + _txt_PaymentSwift58D_Address +
            '",_txt_PaymentSwift58D_Address2:"' + _txt_PaymentSwift58D_Address2 + '",_txt_PaymentSwift58D_Address3:"' + _txt_PaymentSwift58D_Address3 + '",_txt_PaymentSwift58D_Address4:"' + _txt_PaymentSwift58D_Address4 +
            '",Stamp_Duty_Charges_Curr:"' + Stamp_Duty_Charges_Curr + '",Stamp_Duty_Charges_ExRate:"' + Stamp_Duty_Charges_ExRate + '",Stamp_Duty_Charges_Amount:"' + Stamp_Duty_Charges_Amount + '",_OwnLCDiscount_Type:"' + _OwnLCDiscount_Type +
            '",_txtAMLDrawee:"' + _txtAMLDrawee + '",_txtAMLDrawer:"' + _txtAMLDrawer + '",_txtAMLNagoRemiBank:"' + _txtAMLNagoRemiBank + '",_txtAMLCommodity:"' + _txtAMLCommodity + '",_txtAMLVessel:"' + _txtAMLVessel + '",_txtAMLFromPort:"' + _txtAMLFromPort + '",_txtAMLCountry:"' + _txtAMLCountry + '",_txtCountryOfOrigin:"' + _txtCountryOfOrigin +
            '",_txtAML1:"' + _txtAML1 + '",_txtAML2:"' + _txtAML2 + '",_txtAML3:"' + _txtAML3 + '",_txtAML4:"' + _txtAML4 + '",_txtAML5:"' + _txtAML5 + '",_txtAML6:"' + _txtAML6 + '",_txtAML7:"' + _txtAML7 + '",_txtAML8:"' + _txtAML8 +
            '",_txtAML9:"' + _txtAML9 + '",_txtAML10:"' + _txtAML10 + '",_txtAML11:"' + _txtAML11 + '",_txtAML12:"' + _txtAML12 + '",_txtAML13:"' + _txtAML13 + '",_txtAML14:"' + _txtAML14 + '",_txtAML15:"' + _txtAML15 + '",_txtAML16:"' + _txtAML16 + '",_txtAML17:"' + _txtAML17 +
            '",_txtAML18:"' + _txtAML18 + '",_txtAML19:"' + _txtAML19 + '",_txtAML20:"' + _txtAML20 + '",_txtAML21:"' + _txtAML21 + '",_txtAML22:"' + _txtAML22 + '",_txtAML23:"' + _txtAML23 + '",_txtAML24:"' + _txtAML24 + '",_txtAML25:"' + _txtAML25 + '",_txtAML26:"' + _txtAML26 +
            '",_txtAML27:"' + _txtAML27 + '",_txtAML28:"' + _txtAML28 + '",_txtAML29:"' + _txtAML29 + '",_txtAML30:"' + _txtAML30 + '",_chkSpecialCase:"' + _chkSpecialCase + '",_txt_txtRiskCust:"' + _txt_txtRiskCust + '",_txt_txtSettelementAcNo:"' + _txt_txtSettelementAcNo + '",_txt_txtRiskCustAbbr:"' + _txt_txtRiskCustAbbr + '",LodgCumAcc:"' + LodgCumAcc +

            '",_txtChargesClaimed7341:"' + _txtChargesClaimed7341 + '",_txtChargesClaimed7342:"' + _txtChargesClaimed7342 + '",_txtChargesClaimed7343:"' + _txtChargesClaimed7343 +
            '",_txtChargesClaimed7344:"' + _txtChargesClaimed7344 + '",_txtChargesClaimed7345:"' + _txtChargesClaimed7345 +
            '",_txtChargesClaimed7346:"' + _txtChargesClaimed7346 + '",_ddlTotalAmountClaimed734:"' + _ddlTotalAmountClaimed734 +
            '",_txtDate734:"' + _txtDate734 + '",_txtCurrency734:"' + _txtCurrency734 +
            '",_txtAmount734:"' + _txtAmount734 + '",_ddlAccountWithBank734:"' + _ddlAccountWithBank734 +
            '",_txtAccountWithBank734AccountNo:"' + _txtAccountWithBank734AccountNo + '",_txtAccountWithBank734SwiftCode:"' + _txtAccountWithBank734SwiftCode +
            '",_txtAccountWithBank734Location:"' + _txtAccountWithBank734Location + '",_txtAccountWithBank734Name:"' + _txtAccountWithBank734Name +
            '",_txtAccountWithBank734Address1:"' + _txtAccountWithBank734Address1 + '",_txtAccountWithBank734Address2:"' + _txtAccountWithBank734Address2 +
            '",_txtAccountWithBank734Address3:"' + _txtAccountWithBank734Address3 + '",_txtSendertoReceiverInformation7341:"' + _txtSendertoReceiverInformation7341 +
            '",_txtSendertoReceiverInformation7342:"' + _txtSendertoReceiverInformation7342 + '",_txtSendertoReceiverInformation7343:"' + _txtSendertoReceiverInformation7343 +
            '",_txtSendertoReceiverInformation7344:"' + _txtSendertoReceiverInformation7344 + '",_txtSendertoReceiverInformation7345:"' + _txtSendertoReceiverInformation7345 +
            '",_txtSendertoReceiverInformation7346:"' + _txtSendertoReceiverInformation7346 +
            '",_txtAccountWithBank734PartyIdentifier:"' + _txtAccountWithBank734PartyIdentifier +

            '",_ddl_DisposalOfDoc:"' + _ddl_DisposalOfDoc +

            '"}',

            contentType: "application/json; charset=utf-8",
            dataType: "json",
            failure: function (response) {
                alert(response.d);
            },
            error: function (response) {
                alert(response.d);
            }
        });
    }
}
function OpenReimbursingBankList(e) {
    var keycode;
    if (window.event) keycode = window.event.keyCode;
    if (keycode == 113 || e == 'mouseClick') {
        open_popup('../HelpForms/TF_ReimbursingBankLookUp.aspx?hNo=1', 450, 650, 'ReimbursingBankList');
        return false;
    }
}
function selectReimbursingBank(selectedID) {
    var txtReimbursingbank = $get("TabContainerMain_tbDocumentDetails_txtReimbursingbank");
    txtReimbursingbank.value = selectedID;
    FillReimbursingBank();
}
function FillReimbursingBank() {
    var ReimbBankID = $("#TabContainerMain_tbDocumentDetails_txtReimbursingbank");
    $.ajax({
        type: "POST",
        url: "TF_IMP_BOE_Maker.aspx/fillReimbursingbankDetails",
        data: '{ReimbBankID:"' + ReimbBankID.val() + '"}',
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.d.ReimbBankDesc != "") {
                $("#TabContainerMain_tbDocumentDetails_lbl_Reimbursingbank").text(data.d.ReimbBankDesc);
                ReimbBankID.focus();
            }
            else {
                alert('Invalid Reimbursing Bank ID.');
                $("#TabContainerMain_tbDocumentDetails_lbl_Reimbursingbank").text('');
                ReimbBankID.val('');
                ReimbBankID.focus();
            }
        },
        failure: function (data) { alert(data.d); txtCustomerID.focus(); },
        error: function (data) { alert(data.d); txtCustomerID.focus(); }
    });
}
function Open_Acwith_Institution_List(e) {
    var keycode;
    var txtAcwithInstitution = "";
    if (window.event) keycode = window.event.keyCode;
    if (keycode == 113 || e == 'mouseClick') {
        open_popup('../../EXP/EXP_OverseasBankLookUp.aspx?hNo=1&bankID=' + txtAcwithInstitution, 450, 650, 'AcwithInstitutionList');
        return false;
    }
}
function selectOverseasBank(selectedID) {
    var txtAcwithInstitution = $get("TabContainerMain_tbDocumentDetails_txtAcwithInstitution");
    txtAcwithInstitution.value = selectedID;
    FillAccountWithInstitution();
}
function FillAccountWithInstitution() {
    var AccWithID = $("#TabContainerMain_tbDocumentDetails_txtAcwithInstitution");
    $.ajax({
        type: "POST",
        url: "TF_IMP_BOE_Maker.aspx/fillAcwithInstitutionDetails",
        data: '{AccWithID:"' + AccWithID.val() + '"}',
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.d.AccWithInstiDesc != "") {
                $("#TabContainerMain_tbDocumentDetails_lblAcWithInstiBankDesc").text(data.d.AccWithInstiDesc);
                AccWithID.focus();
            }
            else {
                alert('Invalid Bank ID.');
                $("#TabContainerMain_tbDocumentDetails_lblAcWithInstiBankDesc").text('');
                AccWithID.val('');
                AccWithID.focus();
            }
        },
        failure: function (data) { alert(data.d); txtCustomerID.focus(); },
        error: function (data) { alert(data.d); txtCustomerID.focus(); }
    });
}
function OpenNego_Remit_BankList(e) {
    var keycode;
    var lblForeign_Local = document.getElementById('lblForeign_Local').innerHTML;
    var ddl_Nego_Remit_Bank = $("#TabContainerMain_tbDocumentDetails_ddl_Nego_Remit_Bank").val();
    if (window.event) keycode = window.event.keyCode;
    if (ddl_Nego_Remit_Bank == '') {
        alert('Select Bank type First.');
        $("#TabContainerMain_tbDocumentDetails_ddl_Nego_Remit_Bank").focus();
        return false;
    }
    else if (keycode == 113 || e == 'mouseClick') {

        var txtNego_Remit_Bank = $get("TabContainerMain_tbDocumentDetails_txtNego_Remit_Bank").value;
        if (ddl_Nego_Remit_Bank == 'FOREIGN') {
            open_popup('../HelpForms/TF_OverseasBankLookUp.aspx?hNo=IMP', 450, 650, 'Nego_Remit_BankList');
        }
        else if (ddl_Nego_Remit_Bank == 'LOCAL') {
            open_popup('../HelpForms/TF_LocalBankLookUp.aspx?hNo=IMP', 450, 650, 'Nego_Remit_BankList');
        }
        return false;
    }
}
function selectNego_Remit_Bank_IMP(SwiftCode, BankCode, BankName, BankAddress) {
    var txtNego_Remit_Bank = $get("TabContainerMain_tbDocumentDetails_txtNego_Remit_Bank");
    txtNego_Remit_Bank.value = BankCode;
    $("#hdnNegoRemiSwiftCode").val(SwiftCode);
    $("[id$=hdnnego_remittype]").val(BankCode);
    FillNegoRemiBank();
    return true;
}
function FillNegoRemiBank() {
    var NegoRemiID = $("#TabContainerMain_tbDocumentDetails_txtNego_Remit_Bank").val();
    var NegoRemiType = $("#TabContainerMain_tbDocumentDetails_ddl_Nego_Remit_Bank").val();
    var SwiftCode = $("#hdnNegoRemiSwiftCode").val();

    $.ajax({
        type: "POST",
        url: "TF_IMP_BOE_Maker.aspx/fillNegoRemitBankDetails",
        data: '{NegoRemiID:"' + NegoRemiID + '",SwiftCode:"' + SwiftCode + '",NegoRemiType:"' + NegoRemiType + '"}',
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.d.SStatus == "true") {

                //$("#TabContainerMain_tbDocumentDetails_lbl_Nego_Remit_Bank").text(data.d.shortName);
                $("#TabContainerMain_tbDocumentDetails_lbl_Nego_Remit_Bank").text('NAME');
                $("#TabContainerMain_tbDocumentDetails_lbl_Nego_Remit_Bank").prop('title', data.d.NegoRemiDesc);

                $("#TabContainerMain_tbDocumentDetails_lbl_Nego_Remit_Bank_Addr").text('ADDRESS');
                $("#TabContainerMain_tbDocumentDetails_lbl_Nego_Remit_Bank_Addr").prop('title', data.d.BankAddress);


                $("#hdnNegoRemiBankCode").val(data.d.NegoRemiBankCode);
                if (NegoRemiType == "FOREIGN") {
                    $("#TabContainerMain_tbAmlDetails_txtAMLNagoRemiBank").val(data.d.NegoRemiDesc);
                    $("#TabContainerMain_tbDocumentDetails_lbl_Nego_RemitSwift_IFSC").text('SWIFT');
                    $("#TabContainerMain_tbDocumentDetails_lbl_Nego_RemitSwift_IFSC").prop('title', data.d.SwiftCode);

                }
                else if (NegoRemiType == "LOCAL") {
                    $("#TabContainerMain_tbAmlDetails_txtAMLNagoRemiBank").val(data.d.NegoRemiDesc);
                    $("#TabContainerMain_tbDocumentDetails_lbl_Nego_RemitSwift_IFSC").text('IFSC');
                    $("#TabContainerMain_tbDocumentDetails_lbl_Nego_RemitSwift_IFSC").prop('title', data.d.IFSC_Code);
                }
            }
            else {
                $("#TabContainerMain_tbDocumentDetails_lbl_Nego_Remit_Bank").text('');
                $("#TabContainerMain_tbDocumentDetails_lbl_Nego_Remit_Bank").prop('title', '');
                $("#TabContainerMain_tbDocumentDetails_lbl_Nego_Remit_Bank_Addr").text('');
                $("#TabContainerMain_tbDocumentDetails_lbl_Nego_Remit_Bank_Addr").prop('title', '');
                $("#TabContainerMain_tbDocumentDetails_lbl_Nego_RemitSwift_IFSC").text('');
                $("#TabContainerMain_tbDocumentDetails_lbl_Nego_RemitSwift_IFSC").prop('title', '');

                alert('This swift/IFSC code is not part of Overseas/Local Bank Master.');
                $("#TabContainerMain_tbAmlDetails_txtAMLNagoRemiBank").val('');
                $("#TabContainerMain_tbDocumentDetails_txtNego_Remit_Bank").focus();
            }
            if (data.d.RMAExistStatus == "true") {
                if (data.d.RMAStatus == "true") {
                    $("#hdnMT999LC").val('MT999');
                }
                else {
                    $("#hdnMT999LC").val('MT999LC');
                    alert('This swift code [' + SwiftCode + '] is not Enabled.');
                }
            }
            else {
                $("#hdnMT999LC").val('MT999LC');
                alert('This swift code [' + SwiftCode + '] does not exist in RMA Master.');
            }
            $("#TabContainerMain_tbDocumentDetails_txtNego_Remit_Bank").focus();
        },
        failure: function (data) { alert(data.d); $("#TabContainerMain_tbDocumentDetails_txtNego_Remit_Bank").focus(); },
        error: function (data) { alert(data.d); $("#TabContainerMain_tbDocumentDetails_txtNego_Remit_Bank").focus(); }
    });
    return true;
}
function OpenLCList(e) {
    var txtCustomer_ID = $("#TabContainerMain_tbDocumentDetails_txtCustomer_ID");
    var txtDocNo = $("#txtDocNo").val();
    var LocalForeign = $("#lblForeign_Local").text();
    if (txtCustomer_ID.val() != '') {
        var keycode;
        var hdnCustAbbr = document.getElementById('hdnCustAbbr');
        if (window.event) keycode = window.event.keyCode;
        if (keycode == 113 || e == 'mouseClick') {
            open_popup('../HelpForms/TF_IMP_LCHelp.aspx?RefNo=' + txtDocNo + '&CustAbbr=' + hdnCustAbbr.value + '&LocalForeign=' + LocalForeign, 400, 700, 'LCList');
            return false;
        }
    }
    else {
        alert('Please select Customer.');
        txtCustomer_ID.focus();
        return false;
    }
}
function selectLC(LCNo, CustAbbr, Amount, Currency, Country, Commodity) {
    var txt_LC_No = $get("TabContainerMain_tbDocumentDetails_txt_LC_No");
    txt_LC_No.value = LCNo;
    $get("TabContainerMain_tbDocumentDetails_hdnCustLCNo").value = LCNo;
    FillLCDetails();
    CheckLCinBRO();
    CheckLCinSG();
    return true;
}
function FillLCDetails() {
    try {
        var DocNo = $("#txtDocNo").val();
        var CustAbbr = $("#hdnCustAbbr").val();
        var CollectionORLC = $("#lblCollection_Lodgment_UnderLC").val();
        var LCNo = $("#TabContainerMain_tbDocumentDetails_txt_LC_No").val();
        var ForeignLocal = $("#lblForeign_Local").text(); ;
        $.ajax({
            type: "POST",
            url: "TF_IMP_BOE_Maker.aspx/fillLCDetails",
            data: '{DocNo:"' + DocNo + '",CustAbbr:"' + CustAbbr + '",LCNo:"' + LCNo + '",CollectionORLC:"' + CollectionORLC + '",ForeignLocal:"' + ForeignLocal + '"}',
            datatype: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.d.LCStatus == "true") {
                    $("#TabContainerMain_tbDocumentDetails_lblLCDesc").text(data.d.LCAmount);
                    $("#hdnLCREF1").val(data.d.LCRef1);
                    $("#hdnLCREF2").val(data.d.LCRef2);
                    $("#hdnLCREF3").val(data.d.LCRef3);
                    $("#hdnLCCurrency").val(data.d.LCCurrency);
                    $("#hdnLCAppNo").val(data.d.LCAppNo);
                    $("#hdnLCCountry").val(data.d.LCCountry);
                    $("#hdnLCCommCode").val(data.d.LCCommodityCode);
                    $("#hdnExpiryDate").val(data.d.LCExpiryDate);
                    $("#hdnLCRiskCust").val(data.d.LCRiskCustomer);
                    $("#hdnLCSettlementAccNo").val(data.d.LCSettlementAcNo);
                    $("#hdnLCSpCustAbbr").val(data.d.LCCustomerABBR1);

                    $("#TabContainerMain_tbDocumentDetails_txtRiskCust").val(data.d.LCRiskCustomer);
                    $("#TabContainerMain_tbDocumentDetails_txtSettelementAcNo").val(data.d.LCSettlementAcNo);
                    $("#TabContainerMain_tbDocumentDetails_txtRiskCustAbbr").val(data.d.LCCustomerABBR1);

                    if (data.d.LCExpiryDate == "Yes") {
                        alert('This LC has expired.');
                    }
                    if (data.d.Currency == "false") {
                        $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency").prop('disabled', false);
                        alert('LC Currency Code does not exist in LMCC Master Please update.');
                    }
                    else {
                        $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency").val(data.d.Currency);
                        $("#TabContainerMain_tbDocumentDetails_lbl_Doc_Currency").text(data.d.CurrencyName);
                        $("#TabContainerMain_tbDocumentDetails_ddl_Comission_Currency").val(data.d.Currency);
                        $("#TabContainerMain_tbDocumentDetails_ddl_Interest_Currency").val(data.d.Currency);
                        $("#TabContainerMain_tbDocumentDetails_ddl_Other_Currency").val(data.d.Currency);
                        $("#TabContainerMain_tbDocumentDetails_ddl_Their_Commission_Currency").val(data.d.Currency);
                        $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency").prop('disabled', true);
                        DuedateCal();
                        Exp_AcceptanceDateCal();
                    }

                    if (data.d.Commodity == "false") {
                        $("#TabContainerMain_tbDocumentDetails_ddl_Commodity").prop('disabled', false);
                        alert('LC Commodity Code does not exist in LMCC Master Please update.');
                    }
                    else {
                        $("#TabContainerMain_tbDocumentDetails_ddl_Commodity").val(data.d.Commodity);
                        $("#TabContainerMain_tbDocumentDetails_lblCommodityDesc").text(data.d.CommodityName);
                        $("#TabContainerMain_tbDocumentDetails_ddl_Commodity").prop('disabled', true);
                    }
                    if (data.d.Country == "false") {
                        $("#TabContainerMain_tbDocumentDetails_ddlCountryCode").prop('disabled', false);
                        alert('LC Country Code does not exist in LMCC Master Please update.');
                    }
                    else {
                        $("#TabContainerMain_tbDocumentDetails_ddlCountryCode").val(data.d.Country);
                        $("#TabContainerMain_tbAmlDetails_txtAMLCountry").val(data.d.CountryName);
                        $("#TabContainerMain_tbDocumentDetails_lblCountryDesc").text(data.d.CountryName);
                        $("#TabContainerMain_tbDocumentDetails_ddlCountryCode").prop('disabled', true);
                    }

                }
                else {
                    $("#TabContainerMain_tbDocumentDetails_txtRiskCust").val('');
                    $("#TabContainerMain_tbDocumentDetails_txtSettelementAcNo").val('');
                    $("#TabContainerMain_tbDocumentDetails_txtRiskCustAbbr").val('');
                    $("#TabContainerMain_tbDocumentDetails_lblLCDesc").text('');
                    $("#TabContainerMain_tbDocumentDetails_txt_LC_No").val('');
                    $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency").val('Select');
                    $("#TabContainerMain_tbDocumentDetails_ddl_Comission_Currency").val('Select');
                    $("#TabContainerMain_tbDocumentDetails_ddl_Interest_Currency").val('Select');
                    $("#TabContainerMain_tbDocumentDetails_ddl_Other_Currency").val('Select');
                    $("#TabContainerMain_tbDocumentDetails_ddl_Their_Commission_Currency").val('Select');
                    $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency").prop('disabled', false);
                    $("#TabContainerMain_tbDocumentDetails_ddl_Commodity").val('Select');
                    $("#TabContainerMain_tbDocumentDetails_ddl_Commodity").prop('disabled', false);
                    $("#TabContainerMain_tbDocumentDetails_ddlCountryCode").val('Select');
                    $("#TabContainerMain_tbDocumentDetails_ddlCountryCode").prop('disabled', false);
                    alert('Invalid LC No for this customer.');
                }
                $("#TabContainerMain_tbDocumentDetails_txt_LC_No").focus();
            },
            failure: function (data) { alert(data.d); $("#TabContainerMain_tbDocumentDetails_txt_LC_No").focus(); },
            error: function (data) { alert(data.d); $("#TabContainerMain_tbDocumentDetails_txt_LC_No").focus(); }
        });
    }
    catch (err) {
        alert(err.message);
    }
    return true;
}
function CheckLCinBRO() {
    var LCNo = $("#TabContainerMain_tbDocumentDetails_txt_LC_No").val();
    var DocNo = $("#txtDocNo").val();
    $.ajax({
        type: "POST",
        url: "TF_IMP_BOE_Maker.aspx/CheckLCinBRO",
        data: '{LCNo:"' + LCNo + '",DocNo:"' + DocNo + '"}',
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.d.LCInBROStatus == "Exists") {
                OnDocumentScrutinyChange();
                open_popup('../HelpForms/TF_IMP_BROHelp.aspx?LCNo=' + LCNo + '&DocNo=' + DocNo, 400, 400, 'LCList');
                return false;
            }
            else {
                OnDocumentScrutinyChange();
                $("#TabContainerMain_tbDocumentDetails_lblBRONo").text('');
                $("#TabContainerMain_tbDocumentDetails_lblBRODate").text('');
                $("#TabContainerMain_tbDocumentDetails_lblBROAmount").text('');
            }
        },
        failure: function (data) { alert(data.d); },
        error: function (data) { alert(data.d); }
    });
}
function selectBRO(BRORefNo, BRODate, BROAmount) {
    if (BRORefNo != '') {
        $("#TabContainerMain_tbDocumentDetails_lblBRONo").text('BRO No- ' + BRORefNo);
    }
    if (BRODate != '') {
        $("#TabContainerMain_tbDocumentDetails_lblBRODate").text('Date- ' + BRODate);
    }
    if (BROAmount != '') {
        $("#TabContainerMain_tbDocumentDetails_lblBROAmount").text('Amount- ' + BROAmount);
    }
    InstractionChangeForBRO();
}
function InstractionChangeForBRO() {
    if ($("#TabContainerMain_tbDocumentDetails_lblBRONo").text() != '' || $("#TabContainerMain_tbDocumentDetails_lblSGDocNo").text()!='') {
        var DocType = $("#hdnDocType").val();
        if (DocType == 'ACC') {
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions1").text("AS PER RBI GUIDELINES BILL OF ENTRIES TO BE");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions2").text("SUBMITTED WITHIN 90 DAYS FROM THE DATE OF REMITTANCE.");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions3").text("KINDLY MAKE YOUR PAYMENT ON DUE DATE.");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions4").text("");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions5").text("");
        }
        if (DocType == 'IBA') {
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions1").text("AS PER RBI GUIDELINES BILL OF ENTRIES TO BE");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions2").text("SUBMITTED WITHIN 90 DAYS FROM THE DATE OF REMITTANCE.");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions3").text("WE DELIVER DOCUMENTS AGAINST PAYMENT.");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions4").text("");
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions5").text("");
        } 
    }
}

function CheckLCinSG() {
    var LCNo = $("#TabContainerMain_tbDocumentDetails_txt_LC_No").val();
    var DocNo = $("#txtDocNo").val();
    $.ajax({
        type: "POST",
        url: "TF_IMP_BOE_Maker.aspx/CheckLCinSG",
        data: '{LCNo:"' + LCNo + '",DocNo:"' + DocNo + '"}',
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.d.LCInSGStatus == "Exists") {
                OnDocumentScrutinyChange();
                open_popup('../HelpForms/TF_IMP_SG_Help.aspx?LCNo=' + LCNo + '&DocNo=' + DocNo, 400, 400, 'LCList');
                return false;
            }
            else {
                OnDocumentScrutinyChange();
                $("#TabContainerMain_tbDocumentDetails_lblSGDocNo").text('');
                $("#hdnSGDocNo").val('');
                $("#TabContainerMain_tbDocumentDetails_lblSGValDate").text('');
                $("#TabContainerMain_tbDocumentDetails_lblSGAmount").text('');
            }
        },
        failure: function (data) { alert(data.d); },
        error: function (data) { alert(data.d); }
    });
}


function selectSG(SGDocNo, SGValDat, SGAmount) {
    if (SGDocNo != '') {
        $("#TabContainerMain_tbDocumentDetails_lblSGDocNo").text('SG DocNo- ' + SGDocNo);
        $("#hdnSGDocNo").val(SGDocNo);
    }
    if (SGValDat != '') {
        $("#TabContainerMain_tbDocumentDetails_lblSGValDate").text('Date- ' + SGValDat);
    }
    if (SGAmount != '') {
        $("#TabContainerMain_tbDocumentDetails_lblSGAmount").text('Amount- ' + SGAmount);
    }
    InstractionChangeForBRO();
}
function OpenCustomerCodeList(e) {
    var keycode;
    var hdnBranchName = document.getElementById('hdnBranchName');
    if (window.event) keycode = window.event.keyCode;
    if (keycode == 113 || e == 'mouseClick') {
        var txtCustomer_ID = $get("TabContainerMain_tbDocumentDetails_txtCustomer_ID");
        open_popup('../HelpForms/TF_IMP_CustomerHelp.aspx?BranchName=' + hdnBranchName.value + '&CustID=' + txtCustomer_ID.value, 400, 500, 'CustomerCodeList');
        return false;
    }
}
function selectCustomer(selectedID) {
    $("#TabContainerMain_tbDocumentDetails_txtCustomer_ID").val(selectedID);
    $("[id$=hdncustnamelei]").val(selectedID);
    FillCustomerDetails();
    return true;
}
function FillCustomerDetails() {
    var txtCustomerID = $("#TabContainerMain_tbDocumentDetails_txtCustomer_ID");
    var brachname = $("#hdnBranchName").val();
    $.ajax({
        type: "POST",
        url: "TF_IMP_BOE_Maker.aspx/fillCustomerMasterDetails",
        data: '{CustomerID:"' + txtCustomerID.val() + '",BranchName:"' + brachname + '"}',
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.d.CustStatus == "No") {
                $("#hdnCustAbbr").val(data.d.CustAbbr);
                $("#TabContainerMain_tbDocumentDetails_lblCustomerDesc").text(data.d.CustName);
                $("#TabContainerMain_tbAmlDetails_txtAMLDrawee").val(data.d.CustName);
                $("#TabContainerMain_tbDocumentDetails_ddlDrawer").empty().append('<option selected="selected" value="">select</option>');
                $.each(data.d.DrawerList, function () {
                    $("#TabContainerMain_tbDocumentDetails_ddlDrawer").append($("<option></option>").val(this['Value']).html(this['Text']));
                })
                $("#TabContainerMain_tbDocumentDetails_lblLCDesc").text('');
                $("#TabContainerMain_tbDocumentDetails_txt_LC_No").val('');
                $("#TabContainerMain_tbDocumentDetails_lblBRONo").text('');
                $("#TabContainerMain_tbDocumentDetails_lblBRODate").text('');
                $("#TabContainerMain_tbDocumentDetails_lblBROAmount").text('');
                OnDocumentScrutinyChange();
                txtCustomerID.focus();
            }
            else {
                alert(data.d.CustStatus);
                $("#TabContainerMain_tbDocumentDetails_lblCustomerDesc").text('');
                $("#TabContainerMain_tbAmlDetails_txtAMLDrawee").val('');
                $("#TabContainerMain_tbDocumentDetails_ddlDrawer").empty();
                $("#TabContainerMain_tbDocumentDetails_lblBRONo").text('');
                $("#TabContainerMain_tbDocumentDetails_lblBRODate").text('');
                $("#TabContainerMain_tbDocumentDetails_lblBROAmount").text('');
                txtCustomerID.val('');
                txtCustomerID.focus();
                OnDocumentScrutinyChange();
            }
        },
        failure: function (data) { alert(data.d); txtCustomerID.focus(); },
        error: function (data) { alert(data.d); txtCustomerID.focus(); }
    });
}
function Total_Bill_Amount(check) {
    Math.trunc = Math.trunc || function (x) {
        if (isNaN(x)) {
            return NaN;
        }
        if (x > 0) {
            return Math.floor(x);
        }
        return Math.ceil(x);
    };

    var _txt_Stamp_Duty_Charges_ExRate = 0, Stamp_Duty_Charges_INR = 0;
    var _Collection_Lodgment = $("#lblCollection_Lodgment_UnderLC").text();
    var lblSight_Usance = $("#lblSight_Usance").text();
    var _ddl_Doc_Currency = $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency");
    var Stamp_Duty_Charges_INR = $get("TabContainerMain_tbDocumentDetails_txt_Stamp_Duty_Charges_Amount");
    var txtBillAmount = $get("TabContainerMain_tbDocumentDetails_txtBillAmount");
    var txt_Interest_Amount = $get("TabContainerMain_tbDocumentDetails_txt_Interest_Amount");
    var txtComissionAmount = $get("TabContainerMain_tbDocumentDetails_txtComissionAmount");
    var txtOther_amount = $get("TabContainerMain_tbDocumentDetails_txtOther_amount");
    var txtTheirCommission_Amount = $get("TabContainerMain_tbDocumentDetails_txtTheirCommission_Amount");
    var txt_Total_Bill_Amt = $get("TabContainerMain_tbDocumentDetails_txt_Total_Bill_Amt");
    var hdnStamp_Duty_Per_Thousand = $get("hdnStamp_Duty_Per_Thousand").value;
    var _txtTenor = $get("TabContainerMain_tbDocumentDetails_txtTenor");
    if (_ddl_Doc_Currency.val() == 'JPY') {
        if (hdnStamp_Duty_Per_Thousand == '') {
            hdnStamp_Duty_Per_Thousand = 0;
        }
        hdnStamp_Duty_Per_Thousand = parseFloat(hdnStamp_Duty_Per_Thousand);
        if (_Collection_Lodgment == 'Collection') {
            _txt_Stamp_Duty_Charges_ExRate = $get("TabContainerMain_tbDocumentDetails_txt_Stamp_Duty_Charges_ExRate").value;

            if (_txt_Stamp_Duty_Charges_ExRate == '') {
                _txt_Stamp_Duty_Charges_ExRate = 0;
            }
        }
        else {
            if (check == 'b') {
                var lblLCDesc = $get("TabContainerMain_tbDocumentDetails_lblLCDesc").innerHTML;
                if (parseFloat(txtBillAmount.value) > parseFloat(lblLCDesc)) {
                    alert('Lodgement Amount is greater than LC Balance Amount.');
                    //txtBillAmount.focus();
                    setTimeout(function () {
                        txtBillAmount.focus();
                    }, 0);
                }
            }
        }
        _txt_Stamp_Duty_Charges_ExRate = parseFloat(_txt_Stamp_Duty_Charges_ExRate);


        if (txtBillAmount.value == '') {
            txtBillAmount.value = 0;
        }

        if (_ddl_Doc_Currency.value == 'JPY') {
            txtBillAmount.value = Math.trunc(txtBillAmount.value);
        }
        else {
            txtBillAmount.value = Math.trunc(txtBillAmount.value);
        }
        if (txt_Interest_Amount.value == '') {
            txt_Interest_Amount.value = 0;
        }
        txt_Interest_Amount.value = Math.trunc(txt_Interest_Amount.value);
        if (txtComissionAmount.value == '') {
            txtComissionAmount.value = 0;
        }
        txtComissionAmount.value = Math.trunc(txtComissionAmount.value);
        if (txtOther_amount.value == '') {
            txtOther_amount.value = 0;
        }
        txtOther_amount.value = Math.trunc(txtOther_amount.value);
        if (txtTheirCommission_Amount.value == '') {
            txtTheirCommission_Amount.value = 0;
        }
        txtTheirCommission_Amount.value = Math.trunc(txtTheirCommission_Amount.value);
        var _TTTotalAmt = 0;
        _TTTotalAmt = Math.trunc(Math.trunc(txtBillAmount.value) +
                  Math.trunc(txt_Interest_Amount.value) +
                  Math.trunc(txtComissionAmount.value) +
                  Math.trunc(txtOther_amount.value) +
                  Math.trunc(txtTheirCommission_Amount.value));
        txt_Total_Bill_Amt.value = _TTTotalAmt;

        // Stamp_Duty_Charges in INR calculation
        var _Stamp_Duty_Charges_INR = 0;
        var SP_Instruction1 = $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions1");
        var SP_Instruction2 = $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions2");

        Stamp_Duty_Charges_INR.value = round(Math.trunc(((hdnStamp_Duty_Per_Thousand) * (txtBillAmount.value / 1000)) * _txt_Stamp_Duty_Charges_ExRate), 0);

        if (lblSight_Usance == 'Usance' && _Collection_Lodgment == 'Collection' && Stamp_Duty_Charges_INR.value > 0) {
            SP_Instruction1.val('KINDLY AFFIX STAMP OF RS. ' + round(Stamp_Duty_Charges_INR.value, 0) + '/- ON THE REVERSE OF BILL OF EXCHANGE.');
            var Check_SP_Instruction1 = SP_Instruction1.val();
            if (Check_SP_Instruction1.length > 60) {
                SP_Instruction1.val('KINDLY AFFIX STAMP OF RS. ' + round(Stamp_Duty_Charges_INR.value, 0) + '/-');
                SP_Instruction2.val('ON THE REVERSE OF BILL OF EXCHANGE.');
            }
        }
        else {
            SP_Instruction1.val('');
            SP_Instruction2.val('');
        }
    }
    else {
        if (hdnStamp_Duty_Per_Thousand == '') {
            hdnStamp_Duty_Per_Thousand = 0;
        }
        hdnStamp_Duty_Per_Thousand = parseFloat(hdnStamp_Duty_Per_Thousand);
        if (_Collection_Lodgment == 'Collection') {
            _txt_Stamp_Duty_Charges_ExRate = $get("TabContainerMain_tbDocumentDetails_txt_Stamp_Duty_Charges_ExRate").value;

            if (_txt_Stamp_Duty_Charges_ExRate == '') {
                _txt_Stamp_Duty_Charges_ExRate = 0;
            }
        }
        else {
            if (check == 'b') {
                var lblLCDesc = $get("TabContainerMain_tbDocumentDetails_lblLCDesc").innerHTML;
                if (parseFloat(txtBillAmount.value) > parseFloat(lblLCDesc)) {
                    alert('Lodgement Amount is greater than LC Balance Amount.');
                    setTimeout(function () {
                        txtBillAmount.focus();
                    }, 0);
                }
            }
        }
        _txt_Stamp_Duty_Charges_ExRate = parseFloat(_txt_Stamp_Duty_Charges_ExRate);


        if (txtBillAmount.value == '') {
            txtBillAmount.value = 0;
        }

        if (_ddl_Doc_Currency.value == 'JPY') {
            txtBillAmount.value = parseFloat(txtBillAmount.value);
        }
        else {
            txtBillAmount.value = parseFloat(txtBillAmount.value).toFixed(2);
        }
        if (txt_Interest_Amount.value == '') {
            txt_Interest_Amount.value = 0;
        }
        txt_Interest_Amount.value = parseFloat(txt_Interest_Amount.value).toFixed(2);
        if (txtComissionAmount.value == '') {
            txtComissionAmount.value = 0;
        }
        txtComissionAmount.value = parseFloat(txtComissionAmount.value).toFixed(2);
        if (txtOther_amount.value == '') {
            txtOther_amount.value = 0;
        }
        txtOther_amount.value = parseFloat(txtOther_amount.value).toFixed(2);
        if (txtTheirCommission_Amount.value == '') {
            txtTheirCommission_Amount.value = 0;
        }
        txtTheirCommission_Amount.value = parseFloat(txtTheirCommission_Amount.value).toFixed(2);
        var _TTTotalAmt = 0;
        _TTTotalAmt = parseFloat(parseFloat(txtBillAmount.value) +
                  parseFloat(txt_Interest_Amount.value) +
                  parseFloat(txtComissionAmount.value) +
                  parseFloat(txtOther_amount.value) +
                  parseFloat(txtTheirCommission_Amount.value)).toFixed(2);
        txt_Total_Bill_Amt.value = _TTTotalAmt;
        // Stamp_Duty_Charges in INR calculation
        var _Stamp_Duty_Charges_INR = 0;
        var SP_Instruction1 = $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions1");
        var SP_Instruction2 = $("#TabContainerMain_tbDocumentInstructions_txt_SP_Instructions2");
        Stamp_Duty_Charges_INR.value = round(parseFloat(((hdnStamp_Duty_Per_Thousand) * (txtBillAmount.value / 1000)) * _txt_Stamp_Duty_Charges_ExRate), 0);

        //Stamp_Duty_Charges_INR.value = parseFloat(round((((hdnStamp_Duty_Per_Thousand) * (txtBillAmount.value / 1000)) * _txt_Stamp_Duty_Charges_ExRate), 0)).toFixed(2);

        if (lblSight_Usance == 'Usance' && _Collection_Lodgment == 'Collection' && Stamp_Duty_Charges_INR.value > 0) {

            SP_Instruction1.val('KINDLY AFFIX STAMP OF RS. ' + round(Stamp_Duty_Charges_INR.value, 0) + '/- ON THE REVERSE OF BILL OF EXCHANGE.');
            var Check_SP_Instruction1 = SP_Instruction1.val();
            if (Check_SP_Instruction1.length > 60) {
                SP_Instruction1.val('KINDLY AFFIX STAMP OF RS. ' + round(Stamp_Duty_Charges_INR.value, 0) + '/-');
                SP_Instruction2.val('ON THE REVERSE OF BILL OF EXCHANGE.');
            }
        }
        else {
            SP_Instruction1.val('');
            SP_Instruction2.val('');
        }
    }
}
function round(values, decimals) {
    return Number(Math.round(values + 'e' + decimals) + 'e-' + decimals);
}
function Toggel_Instruction_Date() {
    var _DocType = $("#hdnDocType").val();
    var _flcIlcType = $("#lblForeign_Local").text();
    var _Collection_Lodgment = $("#lblCollection_Lodgment_UnderLC").text();
    var _txt_AcceptanceDate = $("#TabContainerMain_tbDocumentDetails_txt_AcceptanceDate").val();
    // collection
    if (_DocType == 'ICA') {
        if (_flcIlcType == 'FOREIGN') {
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions5").text('OR BEFORE BY ' + _txt_AcceptanceDate);
        }
        if (_flcIlcType == 'LOCAL') {
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions3").text('BY (5 BANKING DAYS) ' + _txt_AcceptanceDate);
        }
    }
    if (_DocType == 'ICU') {
        if (_flcIlcType == 'FOREIGN') {
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions4").text('KINDLY PROVIDE YOUR ACCEPTANCE BY ' + _txt_AcceptanceDate);
        }
        if (_flcIlcType == 'LOCAL') {
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions2").text('KINDLY PROVIDE YOUR ACCEPTANCE BY ' + _txt_AcceptanceDate);
        }
    }
    // Lodgment Under LC
    var _ddl_Doc_Scrutiny = '';
    if (_Collection_Lodgment == 'Lodgment_Under_LC') {
        _ddl_Doc_Scrutiny = $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Scrutiny").val();
    }

    if (_DocType == 'IBA') {
        if (_flcIlcType == 'FOREIGN' && _ddl_Doc_Scrutiny == '1') {
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions5").text('BY (5 BANKING DAYS) ' + _txt_AcceptanceDate);
        }
        if (_flcIlcType == 'FOREIGN' && _ddl_Doc_Scrutiny == '2') {
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions5").text('PER ARTICLE 16(D) OF UCP 600 BY(5 BANKING DAYS) ' + _txt_AcceptanceDate);
        }
        if (_flcIlcType == 'LOCAL' && _ddl_Doc_Scrutiny == '1') {
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions3").text('AS PER UCP 600 BY (5 BANKING DAYS) ' + _txt_AcceptanceDate);
        }
        if (_flcIlcType == 'LOCAL' && _ddl_Doc_Scrutiny == '2') {
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions4").text('BY (5 BANKING DAYS) ' + _txt_AcceptanceDate);
        }
    }
    if (_DocType == 'ACC') {
        if (_flcIlcType == 'FOREIGN' && _ddl_Doc_Scrutiny == '1') {
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions4").text('YOUR ACCEPTANCE AS PER UCP 600 BY(5 BANKING DAYS) ' + _txt_AcceptanceDate);
        }
        if (_flcIlcType == 'FOREIGN' && _ddl_Doc_Scrutiny == '2') {
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions5").text('AS PER ARTICLE 16(D) OF UCP 600, BY ' + _txt_AcceptanceDate);
        }
        if (_flcIlcType == 'LOCAL' && _ddl_Doc_Scrutiny == '1') {
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions2").text('BY (5 BANKING DAYS) ' + _txt_AcceptanceDate);
        }
        if (_flcIlcType == 'LOCAL' && _ddl_Doc_Scrutiny == '2') {
            $("#TabContainerMain_tbDocumentInstructions_txt_Instructions3").text('ARTICLE 16(D) OF UCP 600 BY (5 BANKING DAYS) ' + _txt_AcceptanceDate);
        }
    }
}
function FillAMLVesselName() {
    var _txtVesselName = $("#TabContainerMain_tbDocumentDetails_txtVesselName").val();
    $("#TabContainerMain_tbAmlDetails_txtAMLVessel").val(_txtVesselName);
}
function FillAMLFromPort() {
    var _txtFromPort = $("#TabContainerMain_tbDocumentDetails_txtFromPort").val();
    $("#TabContainerMain_tbAmlDetails_txtAMLFromPort").val(_txtFromPort);
}
function ValidateSwiftCreate() {
    var tabContainer = $get('TabContainerMain');
    var ddl = $("#TabContainerMain_tbDocumentDetails_ddl_Nego_Remit_Bank");
    if (ddl.val() == 'FOREIGN') {
        if ($("#TabContainerMain_tbDocumentDetails_rdb_MT734").is(':checked') || $("#TabContainerMain_tbDocumentDetails_rdb_MT799").is(':checked') || $("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(':checked') || $("#TabContainerMain_tbDocumentDetails_rdb_MT499").is(':checked')) {

            var _txtNego_Remit_Bank = $("#TabContainerMain_tbDocumentDetails_txtNego_Remit_Bank").val();
            var _txt_Swift_Code = $("#TabContainerMain_tbDocumentDetails_lbl_Nego_RemitSwift_IFSC").attr('title');
            var txtBillAmount = $get("TabContainerMain_tbDocumentDetails_txtBillAmount");
            var _ddl_Doc_Currency = $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency").val();
            var txt_Their_Ref_no = $get("TabContainerMain_tbDocumentDetails_txt_Their_Ref_no");
            var txtLogdmentDate = $("#txtLogdmentDate").val();

            var _txt_Discrepancy1 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_1").val();
            var _txt_Discrepancy2 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_2").val();
            var _txt_Discrepancy3 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_3").val();
            var _txt_Discrepancy4 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_4").val();
            var _txt_Discrepancy5 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_5").val();
            var _txt_Discrepancy6 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_6").val();
            var _txt_Discrepancy7 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_7").val();
            var _txt_Discrepancy8 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_8").val();
            var _txt_Discrepancy9 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_9").val();
            var _txt_Discrepancy10 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_10").val();
            var _txt_Discrepancy11 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_11").val();
            var _txt_Discrepancy12 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_12").val();
            var _txt_Discrepancy13 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_13").val();
            var _txt_Discrepancy14 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_14").val();
            var _txt_Discrepancy15 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_15").val();
            var _txt_Discrepancy16 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_16").val();
            var _txt_Discrepancy17 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_17").val();
            var _txt_Discrepancy18 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_18").val();
            var _txt_Discrepancy19 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_19").val();
            var _txt_Discrepancy20 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_20").val();
            var _txt_Discrepancy21 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_21").val();
            var _txt_Discrepancy22 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_22").val();
            var _txt_Discrepancy23 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_23").val();
            var _txt_Discrepancy24 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_24").val();
            var _txt_Discrepancy25 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_25").val();
            var _txt_Discrepancy26 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_26").val();
            var _txt_Discrepancy27 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_27").val();
            var _txt_Discrepancy28 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_28").val();
            var _txt_Discrepancy29 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_29").val();
            var _txt_Discrepancy30 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_30").val();
            var _txt_Discrepancy31 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_31").val();
            var _txt_Discrepancy32 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_32").val();
            var _txt_Discrepancy33 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_33").val();
            var _txt_Discrepancy34 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_34").val();
            var _txt_Discrepancy35 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_35").val();

            var _txt_Narrative1 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_1").val();
            var _txt_Narrative2 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_2").val();
            var _txt_Narrative3 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_3").val();
            var _txt_Narrative4 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_4").val();
            var _txt_Narrative5 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_5").val();
            var _txt_Narrative6 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_6").val();
            var _txt_Narrative7 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_7").val();
            var _txt_Narrative8 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_8").val();
            var _txt_Narrative9 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_9").val();
            var _txt_Narrative10 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_10").val();
            var _txt_Narrative11 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_11").val();
            var _txt_Narrative12 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_12").val();
            var _txt_Narrative13 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_13").val();
            var _txt_Narrative14 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_14").val();
            var _txt_Narrative15 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_15").val();
            var _txt_Narrative16 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_16").val();
            var _txt_Narrative17 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_17").val();
            var _txt_Narrative18 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_18").val();
            var _txt_Narrative19 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_19").val();
            var _txt_Narrative20 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_20").val();
            var _txt_Narrative21 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_21").val();
            var _txt_Narrative22 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_22").val();
            var _txt_Narrative23 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_23").val();
            var _txt_Narrative24 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_24").val();
            var _txt_Narrative25 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_25").val();
            var _txt_Narrative26 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_26").val();
            var _txt_Narrative27 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_27").val();
            var _txt_Narrative28 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_28").val();
            var _txt_Narrative29 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_29").val();
            var _txt_Narrative30 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_30").val();
            var _txt_Narrative31 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_31").val();
            var _txt_Narrative32 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_32").val();
            var _txt_Narrative33 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_33").val();
            var _txt_Narrative34 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_34").val();
            var _txt_Narrative35 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_35").val();

			var _ddl_DisposalOfDoc = $("#TabContainerMain_tbSwift_ddl_DisposalOfDoc").val();
			var _DocumentScrutiny = $("#hdnDocumentScrutiny").val()
			
            var _DocType = $("#hdnDocType").val();
            var _SWIFT_File_Type = '';
            var _ProtestFlag = '';

            if (_DocType == 'ICA' || _DocType == 'ICU') {
                if ($("#TabContainerMain_tbDocumentDetails_cb_Protest").is(':checked')) {
                    _ProtestFlag = 'Y';
                }
                if ($("#TabContainerMain_tbDocumentDetails_rdb_MT499").is(':checked')) {
                    _SWIFT_File_Type = 'MT499';
                }
                else if ($("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(':checked')) {
                    _SWIFT_File_Type = 'MT999';
                }

            }
            else if (_DocType == 'IBA' || _DocType == 'ACC') {
                if ($("#TabContainerMain_tbDocumentDetails_rdb_MT734").is(':checked')) {
                    _SWIFT_File_Type = 'MT734';
                }
                else if ($("#TabContainerMain_tbDocumentDetails_rdb_MT799").is(':checked')) {
                    _SWIFT_File_Type = 'MT799';
                }
                else if ($("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(':checked')) {
                    _SWIFT_File_Type = 'MT999';
                }
            }
            if (_SWIFT_File_Type == 'MT799') {
                if (_txt_Swift_Code == '' || _txtNego_Remit_Bank == '') {
                    tabContainer.control.set_activeTabIndex(0);
                    alert('Swift Code can not be blank. Please Enter Swift Code.');
                    return false;
                }
                else if (_txt_Narrative1 == '') {
                    alert('Please enter Narrative');
                    tabContainer.control.set_activeTabIndex(0);
                    $("#TabContainerMain_tbDocumentDetails_txt_Narrative_1").focus();
                    return false;
                }

            }
            if (_SWIFT_File_Type == 'MT499') {
                if (_txt_Swift_Code == '' || _txtNego_Remit_Bank == '') {
                    alert('Swift Code can not be blank. Please Enter Swift Code.');
                    tabContainer.control.set_activeTabIndex(0);
                    return false;
                }
                else if (_txt_Narrative1 == '' && _ProtestFlag == '') {
                    alert('Please enter Narrative');
                    tabContainer.control.set_activeTabIndex(0);
                    $("#TabContainerMain_tbDocumentDetails_txt_Narrative_1").focus();
                    return false;
                }
            }
            var Lodg = $("#lblCollection_Lodgment_UnderLC").text();
            if (_SWIFT_File_Type == 'MT999') {
                if (_txt_Swift_Code == '' || _txtNego_Remit_Bank == '') {
                    alert('Swift Code can not be blank. Please Enter Swift Code.')
                    tabContainer.control.set_activeTabIndex(0);
                    return false;
                }
                else if (_txt_Narrative1 == '' && _ProtestFlag == '' && Lodg != "Lodgment_Under_LC" && $("#hdnDocumentScrutiny").val() != '2') {
                    alert('Please enter Narrative');
                    tabContainer.control.set_activeTabIndex(0);
                    $("#TabContainerMain_tbDocumentDetails_txt_Narrative_1").focus();
                    return false;
                }
				else if (_ddl_DisposalOfDoc == '' && Lodg == "Lodgment_Under_LC" && _DocumentScrutiny == '2') {
				    alert('Please Select Disposal Of Document.');
				    tabContainer.control.set_activeTabIndex(1);
				    $("#TabContainerMain_tbSwift_ddl_DisposalOfDoc").focus();
					return false;
				}
            }
            if (_SWIFT_File_Type == 'MT734') {
                if (_txt_Swift_Code == '' || _txtNego_Remit_Bank == '') {
                    alert('Swift Code can not be blank. Please Enter Swift Code.');
                    tabContainer.control.set_activeTabIndex(0);
                    return false;
                }
                else if (txtLogdmentDate == '') {
                    alert('please Enter Logdment Date.');
                    tabContainer.control.set_activeTabIndex(0);
                    $("#txtLogdmentDate").focus();
                    return false;
                }
                else if (txtBillAmount.value == '' || txtBillAmount.value == '0.00' || txtBillAmount.value == 0) {
                    alert(' Please Enter Amount.');
                    tabContainer.control.set_activeTabIndex(0);
                    txtBillAmount.focus();
                    return false;
                }
                else if (txt_Their_Ref_no.value.length == '') {
                    alert('Their Ref.No should not Blank.');
                    tabContainer.control.set_activeTabIndex(0);
                    txt_Their_Ref_no.focus();
                    return false;
                }
                else if (txt_Their_Ref_no.value.length > 16) {
                    //alert('Their Ref.No should not greater than 16 character.');
                    //txt_Their_Ref_no.focus();
                    //return false;
                }
//                else if (_txt_Discrepancy1 == '') {
//                    alert('Please enter Discrepancy')
//                    tabContainer.control.set_activeTabIndex(0);
//                    return false;
//                }
                else if (_ddl_DisposalOfDoc == '') {
				    alert('Please Select Disposal Of Document.');
				    tabContainer.control.set_activeTabIndex(1);
				    $("#TabContainerMain_tbSwift_ddl_DisposalOfDoc").focus();
					return false;
				}
            }
        }
    }
    return true;
}
function ValidateSFMSCreate() {
    var tabContainer = $get('TabContainerMain');
    var ddl = $("#TabContainerMain_tbDocumentDetails_ddl_Nego_Remit_Bank");
    if (ddl.val() == 'LOCAL') {
        if ($("#TabContainerMain_tbDocumentDetails_rdb_MT734").is(':checked') || $("#TabContainerMain_tbDocumentDetails_rdb_MT799").is(':checked') || $("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(':checked') || $("#TabContainerMain_tbDocumentDetails_rdb_MT499").is(':checked')) {
            //return true;
            var _txtNego_Remit_Bank = $("#TabContainerMain_tbDocumentDetails_txtNego_Remit_Bank").val();
            var _txt_IFSC_Code = $("#TabContainerMain_tbDocumentDetails_lbl_Nego_RemitSwift_IFSC").attr('title');
            var txtBillAmount = $get("TabContainerMain_tbDocumentDetails_txtBillAmount");
            var _ddl_Doc_Currency = $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency").val();
            var txt_Their_Ref_no = $get("TabContainerMain_tbDocumentDetails_txt_Their_Ref_no");
            var txtLogdmentDate = $("#txtLogdmentDate").val();

            var _txt_Discrepancy1 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_1").val();
            var _txt_Discrepancy2 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_2").val();
            var _txt_Discrepancy3 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_3").val();
            var _txt_Discrepancy4 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_4").val();
            var _txt_Discrepancy5 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_5").val();
            var _txt_Discrepancy6 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_6").val();
            var _txt_Discrepancy7 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_7").val();
            var _txt_Discrepancy8 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_8").val();
            var _txt_Discrepancy9 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_9").val();
            var _txt_Discrepancy10 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_10").val();
            var _txt_Discrepancy11 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_11").val();
            var _txt_Discrepancy12 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_12").val();
            var _txt_Discrepancy13 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_13").val();
            var _txt_Discrepancy14 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_14").val();
            var _txt_Discrepancy15 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_15").val();
            var _txt_Discrepancy16 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_16").val();
            var _txt_Discrepancy17 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_17").val();
            var _txt_Discrepancy18 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_18").val();
            var _txt_Discrepancy19 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_19").val();
            var _txt_Discrepancy20 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_20").val();
            var _txt_Discrepancy21 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_21").val();
            var _txt_Discrepancy22 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_22").val();
            var _txt_Discrepancy23 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_23").val();
            var _txt_Discrepancy24 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_24").val();
            var _txt_Discrepancy25 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_25").val();
            var _txt_Discrepancy26 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_26").val();
            var _txt_Discrepancy27 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_27").val();
            var _txt_Discrepancy28 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_28").val();
            var _txt_Discrepancy29 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_29").val();
            var _txt_Discrepancy30 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_30").val();
            var _txt_Discrepancy31 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_31").val();
            var _txt_Discrepancy32 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_32").val();
            var _txt_Discrepancy33 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_33").val();
            var _txt_Discrepancy34 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_34").val();
            var _txt_Discrepancy35 = $("#TabContainerMain_tbDocumentDetails_txt_Discrepancy_35").val();

            var _txt_Narrative1 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_1").val();
            var _txt_Narrative2 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_2").val();
            var _txt_Narrative3 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_3").val();
            var _txt_Narrative4 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_4").val();
            var _txt_Narrative5 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_5").val();
            var _txt_Narrative6 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_6").val();
            var _txt_Narrative7 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_7").val();
            var _txt_Narrative8 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_8").val();
            var _txt_Narrative9 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_9").val();
            var _txt_Narrative10 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_10").val();
            var _txt_Narrative11 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_11").val();
            var _txt_Narrative12 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_12").val();
            var _txt_Narrative13 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_13").val();
            var _txt_Narrative14 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_14").val();
            var _txt_Narrative15 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_15").val();
            var _txt_Narrative16 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_16").val();
            var _txt_Narrative17 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_17").val();
            var _txt_Narrative18 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_18").val();
            var _txt_Narrative19 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_19").val();
            var _txt_Narrative20 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_20").val();
            var _txt_Narrative21 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_21").val();
            var _txt_Narrative22 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_22").val();
            var _txt_Narrative23 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_23").val();
            var _txt_Narrative24 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_24").val();
            var _txt_Narrative25 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_25").val();
            var _txt_Narrative26 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_26").val();
            var _txt_Narrative27 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_27").val();
            var _txt_Narrative28 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_28").val();
            var _txt_Narrative29 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_29").val();
            var _txt_Narrative30 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_30").val();
            var _txt_Narrative31 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_31").val();
            var _txt_Narrative32 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_32").val();
            var _txt_Narrative33 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_33").val();
            var _txt_Narrative34 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_34").val();
            var _txt_Narrative35 = $("#TabContainerMain_tbDocumentDetails_txt_Narrative_35").val();

			var _ddl_DisposalOfDoc = $("#TabContainerMain_tbSwift_ddl_DisposalOfDoc").val();
			var _DocumentScrutiny = $("#hdnDocumentScrutiny").val()
			
            var _DocType = $("#hdnDocType").val();
            var _IFSC_File_Type = '';
            var _ProtestFlag = '';

            if (_DocType == 'ICA' || _DocType == 'ICU') {
                if ($("#TabContainerMain_tbDocumentDetails_cb_Protest").is(':checked')) {
                    _ProtestFlag = 'Y';
                }
                if ($("#TabContainerMain_tbDocumentDetails_rdb_MT499").is(':checked')) {
                    _IFSC_File_Type = 'MT499';
                }
                else if ($("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(':checked')) {
                    _IFSC_File_Type = 'MT999';
                }

            }
            else if (_DocType == 'IBA' || _DocType == 'ACC') {
                if ($("#TabContainerMain_tbDocumentDetails_rdb_MT734").is(':checked')) {
                    _IFSC_File_Type = 'MT734';
                }
                else if ($("#TabContainerMain_tbDocumentDetails_rdb_MT799").is(':checked')) {
                    _IFSC_File_Type = 'MT799';
                }
                else if ($("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(':checked')) {
                    _IFSC_File_Type = 'MT999';
                }
            }
            if (_IFSC_File_Type == 'MT799') {
                if (_txt_IFSC_Code == '' || _txtNego_Remit_Bank == '') {
                    alert('IFSC Code can not be blank. Please Enter IFSC Code.');
                    tabContainer.control.set_activeTabIndex(0);
                    return false;
                }
                else if (_txt_Narrative1 == '') {
                    alert('Please enter Narrative');
                    tabContainer.control.set_activeTabIndex(0);
                    $("#TabContainerMain_tbDocumentDetails_txt_Narrative_1").focus();
                    return false;
                }
            }
            var Lodg = $("#lblCollection_Lodgment_UnderLC").text();
            if (_IFSC_File_Type == 'MT999') {
                if (_txt_IFSC_Code == '' || _txtNego_Remit_Bank == '') {
                    alert('IFSC Code can not be blank. Please Enter IFSC Code.')
                    tabContainer.control.set_activeTabIndex(0);
                    return false;
                }
                else if (_txt_Narrative1 == '' && _ProtestFlag == '' && Lodg != "Lodgment_Under_LC" && $("#hdnDocumentScrutiny").val() != '2') {
                    alert('Please enter Narrative');
                    tabContainer.control.set_activeTabIndex(0);
                    $("#TabContainerMain_tbDocumentDetails_txt_Narrative_1").focus();
                    return false;
                }
				else if (_ddl_DisposalOfDoc == '' && Lodg == "Lodgment_Under_LC" && _DocumentScrutiny == '2') {
				    alert('Please Select Disposal Of Document.');
				    tabContainer.control.set_activeTabIndex(0);
					return false;
				}
            }
            if (_IFSC_File_Type == 'MT734') {
                if (_txt_IFSC_Code == '' || _txtNego_Remit_Bank == '') {
                    alert('IFSC Code can not be blank. Please Enter IFSC Code.');
                    tabContainer.control.set_activeTabIndex(0);
                    return false;
                }
                else if (txtLogdmentDate == '') {
                    alert('please Enter Logdment Date.');
                    tabContainer.control.set_activeTabIndex(0);
                    $("#txtLogdmentDate").focus();
                    return false;
                }
                else if (txtBillAmount.value == '' || txtBillAmount.value == '0.00' || txtBillAmount.value == 0) {
                    alert(' Please Enter Amount.');
                    tabContainer.control.set_activeTabIndex(0);
                    txtBillAmount.focus();
                    return false;
                }
                else if (txt_Their_Ref_no.value.length == '') {
                    alert('Their Ref.No should not Blank.');
                    tabContainer.control.set_activeTabIndex(0);
                    txt_Their_Ref_no.focus();
                    return false;
                }
                else if (txt_Their_Ref_no.value.length > 16) {
                    //alert('Their Ref.No should not greater than 16 character.');
                    //tabContainer.control.set_activeTabIndex(0);
                    //txt_Their_Ref_no.focus();
                    //return false;
                }
//                else if (_txt_Discrepancy1 == '') {
//                    alert('Please enter Discrepancy');
//                    tabContainer.control.set_activeTabIndex(0);
//                    return false;
//                }
                else if (_ddl_DisposalOfDoc == '') {
				    alert('Please Select Disposal Of Document.');
				    tabContainer.control.set_activeTabIndex(1);
				    $("#TabContainerMain_tbSwift_ddl_DisposalOfDoc").focus();
					return false;
				}

            }
            if (_IFSC_File_Type == 'MT499') {
                if (_txt_IFSC_Code == '' || _txtNego_Remit_Bank == '') {
                    alert('IFSC Code can not be blank. Please Enter IFSC Code.');
                    tabContainer.control.set_activeTabIndex(0);
                    return false;
                }
                else if (_txt_Narrative1 == '' && _ProtestFlag == '') {
                    alert('Please enter Narrative');
                    tabContainer.control.set_activeTabIndex(0);
                    $("#TabContainerMain_tbDocumentDetails_txt_Narrative_1").focus();
                    return false;
                }
            }
        }
    }
    return true;
}
///////////////////////////////////////////////////////////Modified By NIlesh/////////////////////////////////////////////////////////////
function validate_Their_Ref_No() {

//    var txt_Their_Ref_no = $get("TabContainerMain_tbDocumentDetails_txt_Their_Ref_no");
//    var negobank = $("#TabContainerMain_tbDocumentDetails_ddl_Nego_Remit_Bank")
//    if (txt_Their_Ref_no.value.length > 16 && negobank.val() == 'FOREIGN') {
//        var retval = confirm('Their Ref.No should not greater than 16 character,Do You Want to proceed!.');
//        if (retval == true) {
//            return true;
//        }
//        else {
//            txt_Their_Ref_no.focus();
//            return false;
//        }

//    }
}
//////////////////////////////////////////////////////////////END//////////////////////////////////////////////////////////////////////////////
function validate_Discrepancy(controlID, CName, Clenght) {
    var _controlID = $get(controlID);
    if (_controlID.value.length > Clenght) {
        alert(CName + ' should not greater than ' + Clenght + ' character.');
        _controlID.focus();
        return false;
    }
}
function validate_Number(evnt) {
    var charCode = (evnt.which) ? evnt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
        return false;
    else
        return true;
}
function validate_ProtestFlag(Control_ID) {
    if ($("#lblCollection_Lodgment_UnderLC").text() == "Lodgment_Under_LC") {
        if (Control_ID == 'Protest') {
            if ($("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(":not(':checked')")) {
                alert('Please Select Swift type MT999.');
                return false;
            }
        }
        else {
            if ($("#TabContainerMain_tbDocumentDetails_cb_Protest").is(':checked')) {
                $("#TabContainerMain_tbDocumentDetails_cb_Protest").prop("checked", false);
            }
        }
    }
    else {
        if (Control_ID == 'Protest') {
            if ($("#TabContainerMain_tbDocumentDetails_rdb_MT499").is(":not(':checked')") && $("#TabContainerMain_tbDocumentDetails_rdb_MT999").is(":not(':checked')")) {
                alert('Please Select Swift type MT499 / MT999.');
                return false;
            }
        }
        else {
            if ($("#TabContainerMain_tbDocumentDetails_cb_Protest").is(':checked')) {
                $("#TabContainerMain_tbDocumentDetails_cb_Protest").prop("checked", false);
            }
        }
    }
}
function Block_Backpsace(evnt) {
    var charCode = (evnt.which) ? evnt.which : event.keyCode;
    if (charCode == 8)
        return false;
    else
        return true;
}
function SubmitToChecker() {
    var DocNo = $("#txtDocNo").val();
    var UserName = $("#hdnUserName").val();
    $.ajax({
        type: "POST",
        url: "TF_IMP_BOE_Maker.aspx/SubmitToChecker",
        data: '{UserName:"' + UserName + '",DocNo:"' + DocNo + '"}',
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.d.CheckerStatus == "Submit") {
                window.location.href = "TF_IMP_BOE_Maker_View.aspx?result=Submit";
            }
            else {
                alert(data.d.CheckerStatus);
            }
        },
        failure: function (data) { alert(data.d); },
        error: function (data) { alert(data.d); }
    });
}
function Check_Tenor_Type() {
    var lblSight_Usance = $("#lblSight_Usance").text();
    var ddlTenor = $("#TabContainerMain_tbDocumentDetails_ddlTenor");
    if (lblSight_Usance == 'Usance' && ddlTenor.val() == '1') {
        alert('For Usance type Tenor cannot be Sight.');
        $("#TabContainerMain_tbDocumentDetails_ddlTenor").val('-Select-');
        ddlTenor.focus();
        return false;
    }
}
function validate_Currency() {
    var _flcIlcType = $("#lblForeign_Local").text();
    var _ddl_Doc_Currency = $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency");
    if (_flcIlcType == 'FOREIGN' && _ddl_Doc_Currency.val() == 'INR') {
        alert('For Foreign bill currency can not be INR.');
        $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency").val('Select');
        _ddl_Doc_Currency.focus();
        return false;
    }
}
function isValidDate(controlID, CName) {
    var obj = controlID;
    if (controlID.value != "__/__/____") {
        var day = obj.value.split("/")[0];
        var month = obj.value.split("/")[1];
        var year = obj.value.split("/")[2];
        var today = new Date();
        if (day == "__") {
            day = today.getDay();
        }
        if (month == "__") {
            month = today.getMonth() + 1;
        }
        if (year == "____") {
            year = today.getFullYear();
        }
        var dt = new Date(year, month - 1, day);
        if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year)) {
            alert('Invalid ' + CName);
            controlID.focus();
            return false;
        }
    }
}
function checkSystemDate(controlID) {
    var obj = controlID;
    if (controlID.value != "__/__/____") {
        var day = obj.value.split("/")[0];
        var month = obj.value.split("/")[1];
        var year = obj.value.split("/")[2];
        var dt = new Date(year, month - 1, day);
        var today = new Date();
        if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {
            alert("Invalid Date");
            controlID.focus();
            return false;
        }
    }
}
function currchrck() {
    var _txtChargesClaimed7341 = $("#TabContainerMain_tbSwift_txtChargesClaimed7341").val();
    var _txtChargesClaimed7342 = $("#TabContainerMain_tbSwift_txtChargesClaimed7342").val();
    var _txtChargesClaimed7343 = $("#TabContainerMain_tbSwift_txtChargesClaimed7343").val();
    var _txtChargesClaimed7344 = $("#TabContainerMain_tbSwift_txtChargesClaimed7344").val();
    var _txtChargesClaimed7345 = $("#TabContainerMain_tbSwift_txtChargesClaimed7345").val();
    var _txtChargesClaimed7346 = $("#TabContainerMain_tbSwift_txtChargesClaimed7346").val();
    var _txtCurrency734 = $("#TabContainerMain_tbSwift_txtCurrency734").val();
    var _txt_Currency = $("#TabContainerMain_tbDocumentDetails_ddl_Doc_Currency").val();
    if (_txtChargesClaimed7341 != "" || _txtChargesClaimed7342 != "" || _txtChargesClaimed7343 != "" || _txtChargesClaimed7344 != "" || _txtChargesClaimed7345 != "" || _txtChargesClaimed7346 != "") {
        if (_txtCurrency734 != _txt_Currency) {
            alert('Total Amount Claimed Currency is Not valid.');
            return false;
            _txtCurrency734.Focus();
        }
    }

}
