function allowBackSpace(val, event) { //vinay Swift Changes
    if (val == 'Rmburse747') {
        var key = window.event ? event.keyCode : event.which;
        //alert(key);
        if (event.keyCode == 8) {
            $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_DateofogauthriztnRmburse").val('');
            $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_DateofogauthriztnRmburse").focus();
            return true;
        }
    }
    if (val == 'NewDateExp747') {
        var key = window.event ? event.keyCode : event.which;
        //alert(key);
        if (event.keyCode == 8) {
            $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_NewDateofExpiry").val('');
            $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_NewDateofExpiry").focus();
            return true;
        }
    }
    if (val == 'DateandplaceofExpiry') {
        var key = window.event ? event.keyCode : event.which;
        //alert(key);
        if (event.keyCode == 8) {
            $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_740_Date").val('');
            $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_740_Date").focus();
            return true;
        }
    }
}

function SubmitValidation() {
    SaveUpdateData();
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_740").is(':checked')) {

        var _Beneficiary = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Beneficiary").val();
        var _Beneficiary2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Beneficiary2").val();
        var _Beneficiary3 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Beneficiary3").val();
        var _Beneficiary4 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Beneficiary4").val();
        var _Beneficiary5 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Beneficiary5").val();

        var _txtNegoPartyIdentifier = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtNegoPartyIdentifier").val();
        var _txtNegoAccountNumber = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtNegoAccountNumber").val();

        var _txtPercentageCreditAmountTolerance = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtPercentageCreditAmountTolerance").val();
        var _txtPercentageCreditAmountTolerance1 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtPercentageCreditAmountTolerance1").val();
        var _txt_Acceptance_Max_Credit_Amt = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Max_Credit_Amt").val();

        var _txtMixedPaymentDetail = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtMixedPaymentDetails").val();
        var _txtMixedPaymentDetail2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtMixedPaymentDetails2").val();
        var _txtMixedPaymentDetail3 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtMixedPaymentDetails3").val();
        var _txtMixedPaymentDetail4 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtMixedPaymentDetails4").val();
        var _txtDeferredPaymentDetail = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtDeferredPaymentDetails").val();
        var _txtDeferredPaymentDetail2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtDeferredPaymentDetails2").val();
        var _txtDeferredPaymentDetail3 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtDeferredPaymentDetails3").val();
        var _txtDeferredPaymentDetail4 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtDeferredPaymentDetails4").val();
        var _txt_740_Draftsat1 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_740_Draftsat1").val();
        var _txt_740_Draftsat2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_740_Draftsat2").val();
        var _txt_740_Draftsat3 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_740_Draftsat3").val();
        var _ddlDrawee_740 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_ddlDrawee_740").val();
        var _txtDraweeAccountNumber = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtDraweeAccountNumber").val();
        if (_ddlDrawee_740 == 'A') {
            var _txtDraweeSwiftCode = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtDraweeSwiftCode").val();
            if ((_txt_740_Draftsat1 + _txt_740_Draftsat2 + _txt_740_Draftsat3 != "" && _txtDraweeAccountNumber + _txtDraweeSwiftCode == "")) {
                alert('If field 42C is present then 42A must also be present.');
                return false;
            }
            if ((_txtDraweeAccountNumber + _txtDraweeSwiftCode != "" && _txt_740_Draftsat1 + _txt_740_Draftsat2 + _txt_740_Draftsat3 == "")) {
                alert('If field 42A is present then 42C must also be present.');
                return false;
            }
            if ((( _txtDraweeAccountNumber != "" || _txtDraweeSwiftCode != "")) && 
		        ((_txtMixedPaymentDetail != "" || _txtMixedPaymentDetail2 != "" || _txtMixedPaymentDetail3 != "" || _txtMixedPaymentDetail4 != "")) 
		        && ((_txtDeferredPaymentDetail != "" || _txtDeferredPaymentDetail2 != "" || _txtDeferredPaymentDetail3 != "" || _txtDeferredPaymentDetail4 != ""))) {
                alert('Either fields 42C and 42a together or field 42M alone, or field 42P alone may be present. No other combination of these fields is allowed.');
                return false;
             }

            if (((_txt_740_Draftsat1 != "" || _txt_740_Draftsat2 != "" || _txt_740_Draftsat3 != "")) &&
                ((_txtDraweeAccountNumber != "" || _txtDraweeSwiftCode != "" )) &&
		        ((_txtMixedPaymentDetail != "" || _txtMixedPaymentDetail2 != "" || _txtMixedPaymentDetail3 != "" || _txtMixedPaymentDetail4 != "")) &&
		        ((_txtDeferredPaymentDetail != "" || _txtDeferredPaymentDetail2 != "" || _txtDeferredPaymentDetail3 != "" || _txtDeferredPaymentDetail4 != ""))) {
                alert('Either fields 42C and 42a together or field 42M alone, or field 42P alone may be present. No other combination of these fields is allowed.');
                return false;
             }

            if (((_txt_740_Draftsat1 != "" || _txt_740_Draftsat2 != "" || _txt_740_Draftsat3 != "")) &&
                ((_txtDraweeAccountNumber != "" || _txtDraweeSwiftCode != "" )) &&
		        ((_txtMixedPaymentDetail != "" || _txtMixedPaymentDetail2 != "" || _txtMixedPaymentDetail3 != "" || _txtMixedPaymentDetail4 != ""))) {
                alert('Either fields 42C and 42a together or field 42M alone, or field 42P alone may be present. No other combination of these fields is allowed.');
                return false;
             }

            if (((_txt_740_Draftsat1 != "" || _txt_740_Draftsat2 != "" || _txt_740_Draftsat3 != "")) &&
                ((_txtDraweeAccountNumber != "" || _txtDraweeSwiftCode != "" )) &&
		        ((_txtDeferredPaymentDetail != "" || _txtDeferredPaymentDetail2 != "" || _txtDeferredPaymentDetail3 != "" || _txtDeferredPaymentDetail4 != ""))) {
                alert('Either fields 42C and 42a together or field 42M alone, or field 42P alone may be present. No other combination of these fields is allowed.');
                return false;
             }
          }
            
           if (_ddlDrawee_740 == 'D') {
             var _txtDraweeName = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtDraweeName").val();
             var _txtDraweeAddress1 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtDraweeAddress1").val();
             var _txtDraweeAddress2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtDraweeAddress2").val();
             var _txtDraweeAddress3 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtDraweeAddress3").val();

            if ((_txt_740_Draftsat1 + _txt_740_Draftsat2 + _txt_740_Draftsat3 != "" && _txtDraweeAccountNumber + _txtDraweeName
	            + _txtDraweeAddress1 + _txtDraweeAddress2 + _txtDraweeAddress3 == "")) {
                alert('If field 42C is present then 42A must also be present.');
                return false;
            }

            if ((_txt_740_Draftsat1 + _txt_740_Draftsat2 + _txt_740_Draftsat3 == "") && (_txtDraweeAccountNumber + _txtDraweeName
	            + _txtDraweeAddress1 + _txtDraweeAddress2 + _txtDraweeAddress3 != "")) {
                alert('If field 42A is present then 42C must also be present.');
                return false;
            }

            if (((_txtDraweeAccountNumber != "" || _txtDraweeName != "" || _txtDraweeAddress1 != "" || _txtDraweeAddress2 != "" || _txtDraweeAddress3 != "")) &&
		        ((_txtMixedPaymentDetail != "" || _txtMixedPaymentDetail2 != "" || _txtMixedPaymentDetail3 != "" || _txtMixedPaymentDetail4 != "")) && 
		        ((_txtDeferredPaymentDetail != "" || _txtDeferredPaymentDetail2 != "" || _txtDeferredPaymentDetail3 != "" || _txtDeferredPaymentDetail4 != ""))) {
                alert('Either fields 42C and 42a together or field 42M alone, or field 42P alone may be present. No other combination of these fields is allowed.');
                return false;
            }

            if (((_txt_740_Draftsat1 != "" || _txt_740_Draftsat2 != "" || _txt_740_Draftsat3 != "")) && 
                ((_txtDraweeAccountNumber != "" || _txtDraweeName != "" || _txtDraweeAddress1 != "" || _txtDraweeAddress2 != "" || _txtDraweeAddress3 != "")) &&
		        ((_txtMixedPaymentDetail != "" || _txtMixedPaymentDetail2 != "" || _txtMixedPaymentDetail3 != "" || _txtMixedPaymentDetail4 != "")) &&
		        ((_txtDeferredPaymentDetail != "" || _txtDeferredPaymentDetail2 != "" || _txtDeferredPaymentDetail3 != "" || _txtDeferredPaymentDetail4 != ""))) {
                alert('Either fields 42C and 42a together or field 42M alone, or field 42P alone may be present. No other combination of these fields is allowed.');
                return false;
            }

            if (((_txt_740_Draftsat1 != "" || _txt_740_Draftsat2 != "" || _txt_740_Draftsat3 != "")) &&
                ((_txtDraweeAccountNumber != "" || _txtDraweeName != "" || _txtDraweeAddress1 != "" || _txtDraweeAddress2 != "" || _txtDraweeAddress3 != "")) &&
		        ((_txtMixedPaymentDetail != "" || _txtMixedPaymentDetail2 != "" || _txtMixedPaymentDetail3 != "" || _txtMixedPaymentDetail4 != ""))) {
                alert('Either fields 42C and 42a together or field 42M alone, or field 42P alone may be present. No other combination of these fields is allowed.');
                return false;
            }

            if (((_txt_740_Draftsat1 != "" || _txt_740_Draftsat2 != "" || _txt_740_Draftsat3 != "")) &&
                ((_txtDraweeAccountNumber != "" || _txtDraweeName != "" || _txtDraweeAddress1 != "" || _txtDraweeAddress2 != "" || _txtDraweeAddress3 != "")) &&
		        ((_txtDeferredPaymentDetail != "" || _txtDeferredPaymentDetail2 != "" || _txtDeferredPaymentDetail3 != "" || _txtDeferredPaymentDetail4 != ""))) {
                alert('Either fields 42C and 42a together or field 42M alone, or field 42P alone may be present. No other combination of these fields is allowed.');
                return false;
            }
        }

        if ((_txt_Acceptance_Max_Credit_Amt != "") && (_txtPercentageCreditAmountTolerance1 != "" || _txtPercentageCreditAmountTolerance != "")) {
            alert('Either field 39A or 39B, but not both, may be present.');
            return false;
        }

        if (((_txt_740_Draftsat1 != "" || _txt_740_Draftsat2 != "" || _txt_740_Draftsat3 != "")) &&
		((_txtMixedPaymentDetail != "" || _txtMixedPaymentDetail2 != "" || _txtMixedPaymentDetail3 != "" || _txtMixedPaymentDetail4 != ""))
		&& ((_txtDeferredPaymentDetail != "" || _txtDeferredPaymentDetail2 != "" || _txtDeferredPaymentDetail3 != "" || _txtDeferredPaymentDetail4 != ""))) {
            alert('Either fields 42C and 42a together or field 42M alone, or field 42P alone may be present. No other combination of these fields is allowed.');
            return false;
        }

        if (((_txtMixedPaymentDetail != "" || _txtMixedPaymentDetail2 != "" || _txtMixedPaymentDetail3 != "" || _txtMixedPaymentDetail4 != "")) && ((_txtDeferredPaymentDetail != "" || _txtDeferredPaymentDetail2 != "" || _txtDeferredPaymentDetail3 != "" || _txtDeferredPaymentDetail4 != ""))) {
            alert('Either fields 42C and 42a together or field 42M alone, or field 42P alone may be present. No other combination of these fields is allowed.');
            return false;
        }

        if ($("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_ddlNegotiatingBankSwift").val() == "A") {
            var _txtNegoSwiftCode = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtNegoSwiftCode").val();

            if ((_Beneficiary + _Beneficiary2 + _Beneficiary3 + _Beneficiary4 + _Beneficiary5 != "") && (_txtNegoAccountNumber + _txtNegoSwiftCode != "")) {
                alert('[59] Beneficiary and [58A] Negotiating Bank both can not be present for MT740.');
                return false;
            }
        }
        else if ($("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_ddlNegotiatingBankSwift").val() == "D") {

            var NegoName = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtNegoName").val();
            var NegoAddress1 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtNegoAddress1").val();
            var NegoAddress2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtNegoAddress2").val();
            var NegoAddress3 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtNegoAddress3").val();

            if ((_Beneficiary + _Beneficiary2 + _Beneficiary3 + _Beneficiary4 + _Beneficiary5 != "") && (_txtNegoAccountNumber + NegoName + NegoAddress1 + NegoAddress2 + NegoAddress3 != "")) {
                alert('[59] Beneficiary and [58D] Negotiating Bank both can not be present for MT740.');
                return false;
            }
        }

        var bill_Curr = $("#lblDoc_Curr").text().toUpperCase();
        var CreditCurrency = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtCreditCurrency").val().toUpperCase();
        if (CreditCurrency == "" || CreditCurrency != bill_Curr) {
            alert('[32B] Credit currency not matching bill currency'); //vinay swift Changes
            return false;
        }

        var CreditAmount = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtCreditAmount").val();
        if (CreditAmount == "" || CreditAmount == 0) {
            alert('[32B] Credit Amount can not be blank or zero.');
            return false;
        }

    }


    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_756").is(':checked') || $("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_999").is(':checked')) {
        var _Discrepancy_Charges_Swift = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Discrepancy_Charges_Swift").val();
        /*        
        if (_Discrepancy_Charges_Swift == "" || _Discrepancy_Charges_Swift == 0) {
        alert('Discrepancy Charges Swift can not be blank or zero.');
        return false;
        }
        */
        var _DraftAmt = $("#TabContainerMain_tbDocumentDetails_txtDraftAmt").val();
        if (_DraftAmt == "" || _DraftAmt == 0) {
            alert('Draft Amt can not be blank or zero.');
            return false;
        }

    }

    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_799").is(':checked')) {
        if (
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_1") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_2") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_3") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_4") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_5") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_6") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_7") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_9") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_10") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_11") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_12") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_13") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_14") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_15") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_16") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_17") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_19") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_20") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_21") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_22") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_23") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_24") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_25") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_26") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_27") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_29") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_30") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_31") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_32") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_33") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_34") == "" &&
        $().val("TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_35") == ""
        ) {
            alert('Please Enter Narrative for MT799 Swift.');
            return false;
        }
    }
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_747").is(':checked')) {

        var _Docu_Credit_no = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_documentaryCredtno").val().trim();
        if (_Docu_Credit_no == "") {
            alert("Please enter Documentary Credit no.");
            return false;
        }

        if (_Docu_Credit_no.match("^/")) {
            alert("Documentary Credit no start with /.");
            return false;
        }
        if (_Docu_Credit_no.match("/$")) {
            alert("Documentary Credit no ends with /.");
            return false;
        }

        if (_Docu_Credit_no.match("//")) {
            alert("Documentary Credit no content //.");
            return false;
        }

        var _Reimburs_Bank_Ref = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_reimbursingbankRef").val().trim();
        if (_Reimburs_Bank_Ref.match("^/")) {
            alert("Reimbursing Bank Ref start with /.");
            return false;
        }
        if (_Reimburs_Bank_Ref.match("/$")) {
            alert("Reimbursing Bank Ref ends with /.");
            return false;
        }

        if (_Reimburs_Bank_Ref.match("//")) {
            alert("Reimbursing Bank Ref content //.");
            return false;
        }

        var _DateAuthReimburse = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_DateofogauthriztnRmburse").val().trim();
        if (_DateAuthReimburse == "") {
            alert("Please enter Date of the Original Authorisation to Reimburse.");
            return false;
        }

        var _NewDate_ofexpiry = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_NewDateofExpiry").val().trim();
        var _PercentageTol1 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_PercentageCreditAmtTolerance1").val().trim();
        var _PercentageTol2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_PercentageCreditAmtTolerance2").val().trim();
        var _AddAmt1 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_AdditionalAmtsCovered1").val().trim();
        var _AddAmt2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_AdditionalAmtsCovered2").val().trim();
        var _AddAmt3 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_AdditionalAmtsCovered3").val().trim();
        var _AddAmt4 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_AdditionalAmtsCovered4").val().trim();
        var _SenRec1 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_SentoReceivInfo1").val().trim();
        var _SenRec2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_SentoReceivInfo2").val().trim();
        var _SenRec3 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_SentoReceivInfo3").val().trim();
        var _SenRec4 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_SentoReceivInfo4").val().trim();
        var _SenRec5 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_SentoReceivInfo5").val().trim();
        var _SenRec6 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_SentoReceivInfo6").val().trim();
        var _Narr1 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_1").val().trim();
        var _Narr2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_2").val().trim();
        var _Narr3 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_3").val().trim();
        var _Narr4 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_4").val().trim();
        var _Narr5 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_5").val().trim();
        var _Narr6 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_6").val().trim();
        var _Narr7 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_7").val().trim();
        var _Narr8 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_8").val().trim();
        var _Narr9 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_9").val().trim();
        var _Narr10 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_10").val().trim();
        var _Narr11 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_11").val().trim();
        var _Narr12 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_12").val().trim();
        var _Narr13 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_13").val().trim();
        var _Narr14 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_14").val().trim();
        var _Narr15 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_15").val().trim();
        var _Narr16 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_16").val().trim();
        var _Narr17 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_17").val().trim();
        var _Narr18 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_18").val().trim();
        var _Narr19 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_19").val().trim();
        var _Narr20 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_20").val().trim();

        var Currency32 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_IncreaseofDocumentaryCreditCurr").val().toUpperCase().trim();
        var Amt32 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_IncreaseofDocumentaryCreditAmt").val().trim();
        if (Amt32 == 0) { Amt32 = ''; }

        var Currency33 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_DecreaseofDocumentaryCreditCurr").val().toUpperCase().trim();
        var Amt33 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_DecreaseofDocumentaryCreditAmt").val().trim();
        if (Amt33 == 0) { Amt33 = ''; }

        var Currency34 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_NewDocumentaryCreditAmtAfterAmendmentCurr").val().toUpperCase().trim();
        var Amt34 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_NewDocumentaryCreditAmtAfterAmendment").val().trim();
        if (Amt34 == 0) { Amt34 = ''; }

        if (Currency32 + Amt32 + Currency33 + Amt33 + Currency34 + Amt34 + _AddAmt1 + _AddAmt2 + _AddAmt3 + _AddAmt4 + _SenRec1 + _SenRec2 + _SenRec3 + _SenRec4 + _SenRec5 + _SenRec6 +
            _NewDate_ofexpiry + _PercentageTol1 + _PercentageTol2 + _Narr1 + _Narr2 + _Narr3 + _Narr4 + _Narr5 + _Narr6 + _Narr7 + _Narr8 + _Narr9
            + _Narr10 + _Narr11 + _Narr12 + _Narr13 + _Narr14 + _Narr15 + _Narr16 + _Narr17 + _Narr18 + _Narr19 + _Narr20 == "") {
            alert('At least one of the fields 31E,32B,33B,34B,39A,39C,72Z or 77 must be present.');
            return false;
        }

        if ((Currency32 + Amt32 != "" && Currency34 + Amt34 == "") || (Currency33 + Amt33 != "" && Currency34 + Amt34 == "")) {
            alert('If either [32B] or [33B] is present, then [34B] must also be present.');
            return false;
        }

        if ((Currency34 + Amt34 != "") && (Currency32 + Amt32 + Currency33 + Amt33 == "")) {
            alert(' If [34B] is present, either [32B] or [33B] must also be present.');
            return false;
        }

        if ((Currency32 != "" && Currency34 != "") && (Currency32 != Currency34)) {
            alert('[32B] Currency code of 32B 34B not matching');
            return false;
        }

        if ((Currency33 != "" && Currency34 != "") && (Currency33 != Currency34)) {
            alert('[33B] Currency code of 33B 34B not matching');
            return false;
        }

        if ((Currency32 != "" && Amt32 == "") || (Currency32 == "" && Amt32 != "")) {
            alert('Please Fill complete [32B] or keep both Currency and amt Feild blank.');
            return false;
        }
        if ((Currency33 != "" && Amt33 == "") || (Currency33 == "" && Amt33 != "")) {
            alert('Please Fill complete [33B] or keep both Currency and amt Feild blank.');
            return false;
        }
        if ((Currency34 != "" && Amt34 == "") || (Currency34 == "" && Amt34 != "")) {
            alert('Please Fill complete [34B] or keep both Currency and amt Feild blank.');
            return false;
        }

        if ((Currency32 != "" && Currency33 != "" && Currency34 != "")) {
            if ((Currency32 != Currency34) || (Currency33 != Currency34) || (Currency32 != Currency33)) {
                alert('[32B] Currency code of 32B 33B 34B not matching');
                return false;
            }
        }
    }
    return true;
}

function Toggel_GO_SWIFT_Remark() {
    if ($("#TabContainerMain_tbDocumentGO_chkGO_Swift_SFMS").is(':checked')) {
        var _txt_GO_Swift_SFMS_Remarks = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Remarks").val();

        $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Details").val(_txt_GO_Swift_SFMS_Remarks);
        $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Details").val(_txt_GO_Swift_SFMS_Remarks);
    }
}
function Toggel_GO_LC_Remark() {
    if ($("#TabContainerMain_tbDocumentGO_chkGO_LC_Commitement").is(':checked')) {
        var _txt_GO_LC_Commitement_Remarks = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Remarks").val();

        $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Debit_Details").val(_txt_GO_LC_Commitement_Remarks);
        $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Credit_Details").val(_txt_GO_LC_Commitement_Remarks);
    }
}

function Toggel_Ledger_Amt() {

    if ($("#TabContainerMain_tbDocumentLedger_chk_Ledger_Modify").is(':checked')) {
        var _Ledger_Amt = $("#TabContainerMain_tbDocumentLedger_txtLedger_Modify_amt").val();
        var _Bill_Amt = $("#lblBillAmt").text();
        var flag = 0;
        if (_Ledger_Amt == '') {
            _Ledger_Amt = 0;
        }
        if (_Bill_Amt == '') {
            _Bill_Amt = 0;
        }
        _Ledger_Amt = parseFloat(_Ledger_Amt);
        _Bill_Amt = parseFloat(_Bill_Amt);

        if (_Ledger_Amt == 0) {
            flag = 1;
            alert('Ledger amt can not be blank or Zero.');
            $("#TabContainerMain_tbDocumentLedger_txtLedgerBalanceAmt").focus();
        }
        if (flag == 0) {
            var _txtLedger_Modify_amt = $("#TabContainerMain_tbDocumentLedger_txtLedger_Modify_amt").val();
            $("#TabContainerMain_tbDocumentLedger_txtLedgerBalanceAmt").val(_txtLedger_Modify_amt);
            $("#TabContainerMain_tbDocumentDetails_txtDraftAmt").val(_txtLedger_Modify_amt);
            $("#TabContainerMain_tbDocumentAccounting_txt_DiscAmt").val(_txtLedger_Modify_amt);
            $("#TabContainerMain_tbDocumentAccounting_txt_CR_Acceptance_amt").val(_txtLedger_Modify_amt);

            Commission_Toggel();
        }
    }
    else {
        $("#TabContainerMain_tbDocumentDetails_txtDraftAmt").val(_Bill_Amt);
        $("#TabContainerMain_tbDocumentAccounting_txt_DiscAmt").val(_Bill_Amt);
        $("#TabContainerMain_tbDocumentAccounting_txt_CR_Acceptance_amt").val(_Bill_Amt);
        Commission_Toggel();
    }

}
function OpenSettl_For_Bank_help(e) {
    var keycode;
    var _BranchName = $("#hdnBranchName").val();
    var _DocScrutiny = $("#hdnDocScrutiny").val();
    var _Document_Curr = $("#lblDoc_Curr").text();
    var LocalForeign = $("#lblForeign_Local").text();
    if (window.event) keycode = window.event.keyCode;
    if (keycode == 113 || e == 'mouseClick') {
        if (LocalForeign == 'Foreign') {
            open_popup('../HelpForms/TF_IMP_Settl_for_Bank_ForeignHelp.aspx?BranchName=' + _BranchName + '&Document_Curr=' + _Document_Curr, 500, 500, 'SundryCodeList');
        }
        if (LocalForeign == 'Local') {
            open_popup('../HelpForms/TF_IMP_Settl_for_Bank_Help.aspx?BranchName=' + _BranchName + '&DocScrutiny=' + _DocScrutiny, 500, 500, 'SundryCodeList');
        }
        return false;
    }
}

function selectSettl_For_Bank_Local(ABBR, ACcode, Acno) {
    $("#TabContainerMain_tbDocumentDetails_txtSettl_ForBank_Abbr").val(ABBR);
    $("#TabContainerMain_tbDocumentDetails_txtSettl_ForBank_AccCode").val(ACcode);
    $("#TabContainerMain_tbDocumentDetails_txtSettl_ForBank_AccNo").val(Acno);
}

function Toggel_DR_Code() {
    var _txt_DR_Code = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Code").val();
    if ($("#TabContainerMain_tbDocumentGO_chkGO_Swift_SFMS").is(':checked')) {
        $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Cust_AcCode").val(_txt_DR_Code);
    }
    if ($("#TabContainerMain_tbDocumentGO_chkGO_LC_Commitement").is(':checked')) {
        $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Debit_Cust_AcCode").val(_txt_DR_Code);
    }
}

function Toggel_IMP_ACC_Cust_abbr() {
    var _txt_DR_Cust_abbr = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cust_abbr").val();
    if ($("#TabContainerMain_tbDocumentGO_chkGO_Swift_SFMS").is(':checked')) {
        $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Cust").val(_txt_DR_Cust_abbr);
        $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Cust").val(_txt_DR_Cust_abbr);
    }
    if ($("#TabContainerMain_tbDocumentGO_chkGO_LC_Commitement").is(':checked')) {
        $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Debit_Cust").val(_txt_DR_Cust_abbr);
        $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Credit_Cust").val(_txt_DR_Cust_abbr);
    }
}
function Toggel_IMP_ACC_Cust_No() {
    var _txt_DR_Cust_Acc = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cust_Acc").val();
    if ($("#TabContainerMain_tbDocumentGO_chkGO_Swift_SFMS").is(':checked')) {
        $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Cust_AccNo").val(_txt_DR_Cust_Acc);
    }
    if ($("#TabContainerMain_tbDocumentGO_chkGO_LC_Commitement").is(':checked')) {
        $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Debit_Cust_AccNo").val(_txt_DR_Cust_Acc);
    }
}

function Toggel_IMP_ACC_Coll_Comm() {
    var _txt_CR_Accept_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Accept_Commission_amt").val();
    $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_amt").val(_txt_CR_Accept_Commission_amt);
}
function Toggel_Amt(id) {
    id = '#' + id;
    Math.trunc = Math.trunc || function (x) {
        if (isNaN(x)) {
            return NaN;
        }
        if (x > 0) {
            return Math.floor(x);
        }
        return Math.ceil(x);
    };


    if ($(id).val() == '' || $(id).val() == 0) {
        $(id).val(0);
    }

    if ($("#lblDoc_Curr").text() == 'JPY') {
        $(id).val(Math.trunc($(id).val()));
    }
    else {
        $(id).val(parseFloat($(id).val()).toFixed(2));
    }
}
function Toggel_GO_LC_Print_Advice() {

    var _txt_GO_LC_Commitement_Debit_Amt = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Debit_Amt").val();
    if (_txt_GO_LC_Commitement_Debit_Amt > 0) {
        $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Debit_Advice_Print").val('Y');
        //$("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Credit_Advice_Print").val('Y');
    }
    else {
        $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Debit_Advice_Print").val('');
        $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Credit_Advice_Print").val('');
    }

}
function Toggel_Swift_Type(Type) {

    if (Type == 'None') {

        if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_None").is(':checked')) {
            $("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_740").prop('checked', false);
            $("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_756").prop('checked', false);
            $("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_999").prop('checked', false);
            $("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_747").prop('checked', false);
        }

    }
    if (Type == 'MT740') {

        if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_740").is(':checked')) {
            $("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_None").prop('checked', false);
            //$("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_999").prop('checked', false);
        }
    }
    if (Type == 'MT756') {

        if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_756").is(':checked')) {
            $("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_None").prop('checked', false);
            //$("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_999").prop('checked', false);

        }
    }
    // ============= Bhupen ==================//
    if (Type == 'MT747') {

        if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_747").is(':checked')) {
            $("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_None").prop('checked', false);
            //$("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_999").prop('checked', false);

        }
    }

    if (Type == 'MT999') {

        if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_999").is(':checked')) {
            $("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_None").prop('checked', false);
            //$("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_740").prop('checked', false);
            //$("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_756").prop('checked', false);
        }
    }
    if (Type == 'MT799') {
        if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_799").is(':checked')) {
            $("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_None").prop('checked', false);
            //$("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_740").prop('checked', false);
            //$("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_756").prop('checked', false);
        }
    }
}
function Ledger_Amt_validation() {
    var _Ledger_Amt = $("#TabContainerMain_tbDocumentLedger_txtLedger_Modify_amt").val();
    var _Bill_Amt = $("#TabContainerMain_tbDocumentDetails_txtDraftAmt").val();

    if (_Ledger_Amt == '') {
        _Ledger_Amt = 0;
    }
    _Ledger_Amt = parseFloat(_Ledger_Amt);

    if (_Bill_Amt == '') {
        _Bill_Amt = 0;
    }
    _Bill_Amt = parseFloat(_Bill_Amt);

    if (_Ledger_Amt == 0) {
        alert('Ledger amt can not be blank or Zero.');
        $("#TabContainerMain_tbDocumentLedger_txtLedger_Modify_amt").focus();
    }
    if (_Ledger_Amt > _Bill_Amt) {
        alert('Ledger amt can not be gerater than Bill Logdment amt.');
        $("#TabContainerMain_tbDocumentLedger_txtLedger_Modify_amt").focus();
    }
}
function DebitCredit_Amt(Type) {
    if (Type == 'Swift_SFMS') {
        var _txt_GO_Swift_SFMS_Debit_Amt = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Amt").val();
        $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Amt").val(_txt_GO_Swift_SFMS_Debit_Amt);
    }
    else if (Type == 'LC_Commitement') {
        var _txt_GO_LC_Commitement_Debit_Amt = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Debit_Amt").val();
        $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Credit_Amt").val(_txt_GO_LC_Commitement_Debit_Amt);

        Toggel_GO_LC_Print_Advice();
    }
}
function validate_Commission_MATU_Code(evnt) {
    var charCode = (evnt.which) ? evnt.which : event.keyCode;
    if (charCode != 8 && charCode != 9 && charCode != 46 && charCode != 49 && charCode != 50 && charCode != 97 && charCode != 98 && charCode != 116)
        return false;
    else
        return true;
}
function validate_Commission_LUMP_Code(evnt) {

    var charCode = (evnt.which) ? evnt.which : event.keyCode;
    if (charCode != 8 && charCode != 9 && charCode != 46 && charCode != 89 && charCode != 97 && charCode != 98 && charCode != 116)
        return false;
    else
        return true;
}
function validate_Commission_PAYER_Code(evnt) {

    var charCode = (evnt.which) ? evnt.which : event.keyCode;
    if (charCode != 8 && charCode != 9 && charCode != 46 && charCode != 66 && charCode != 83 && charCode != 97 && charCode != 98 && charCode != 116)
        return false;
    else
        return true;
}
function Uppercase(id) {
    $('#' + id).val($('#' + id).val().toUpperCase());
}
function round(values, decimals) {
    return Number(Math.round(values + 'e' + decimals) + 'e-' + decimals);
}
function Commission_Toggel() {

    var _lblDoc_Curr = $("#lblDoc_Curr").text();
    var working_Days = 0; var Usance_Comm_Charges = 0;
    var _Bill_Amt = $("#TabContainerMain_tbDocumentAccounting_txt_DiscAmt").val();
    var _txt_IMP_ACC_ExchRate = $("#TabContainerMain_tbDocumentAccounting_txt_IMP_ACC_ExchRate").val();
    var _txtTenor = $("#TabContainerMain_tbDocumentInstructions_txtTenor").val();
    var _Usance_Comm_Rate = 1.2;

    if ($("#TabContainerMain_tbDocumentAccounting_chk_IMP_ACC_Commission").is(':checked')) {
        if (_lblDoc_Curr == 'INR') {
            working_Days = 365;
            _txt_IMP_ACC_ExchRate = 1;
        }
        else {
            working_Days = 360;
        }

        _Bill_Amt = parseFloat(_Bill_Amt).toFixed(2);
        _txt_IMP_ACC_ExchRate = parseFloat(_txt_IMP_ACC_ExchRate).toFixed(2);
        _txtTenor = parseFloat(_txtTenor).toFixed(2);
        _Usance_Comm_Rate = parseFloat(_Usance_Comm_Rate).toFixed(2);
        working_Days = parseFloat(working_Days).toFixed(2);

        Usance_Comm_Charges = parseFloat((((_Bill_Amt * _txt_IMP_ACC_ExchRate) * (_Usance_Comm_Rate / 100)) * (_txtTenor)) / (working_Days)).toFixed(2);

        if (Usance_Comm_Charges <= 1000) {
            Usance_Comm_Charges = parseFloat(1000).toFixed(2);
        }
        else {
            Usance_Comm_Charges = parseFloat(round(parseFloat(Usance_Comm_Charges), 0)).toFixed(2);
        }
    }
    var _txtCommission_matu = $("#TabContainerMain_tbDocumentAccounting_txtCommission_matu");
    var _txt_CR_Accept_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Accept_Commission_Curr");
    var _txt_CR_Others_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Others_Curr");
    var _txt_CR_Their_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Their_Commission_Curr");

    var _txt_CR_Accept_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Accept_Commission_amt");
    var _txt_CR_Others_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Others_amt");
    var _txt_CR_Their_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Their_Commission_amt");

    var _txt_CR_Accept_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Accept_Commission_Payer");
    var _txt_CR_Pay_Handle_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Pay_Handle_Commission_Payer");
    var _txt_CR_Others_Payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Others_Payer");
    var _txt_CR_Their_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Their_Commission_Payer");



    if (_txtCommission_matu.val() == 1) {

        _txt_CR_Accept_Commission_Curr.prop("disabled", false);
        _txt_CR_Accept_Commission_Curr.val('INR');
        //        _txt_CR_Others_Curr.prop("disabled", false);
        //        _txt_CR_Their_Commission_Curr.prop("disabled", false);

        _txt_CR_Accept_Commission_amt.val(Usance_Comm_Charges);
        $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_amt").val(Usance_Comm_Charges);
        _txt_CR_Accept_Commission_amt.prop("disabled", false);
        //        _txt_CR_Others_amt.prop("disabled", false);
        //        _txt_CR_Their_Commission_amt.prop("disabled", false);

        _txt_CR_Accept_Commission_Payer.val('B');
        _txt_CR_Accept_Commission_Payer.prop("disabled", false);
        //        _txt_CR_Others_Payer.prop("disabled", false);
        //        _txt_CR_Their_Commission_Payer.prop("disabled", false);

    }
    else {
        _txt_CR_Accept_Commission_Curr.prop("disabled", true);
        _txt_CR_Accept_Commission_Curr.val(_lblDoc_Curr);
        //        _txt_CR_Others_Curr.prop("disabled", true);
        //        _txt_CR_Others_Curr.val(_lblDoc_Curr);
        //        _txt_CR_Their_Commission_Curr.prop("disabled", true);
        //        _txt_CR_Their_Commission_Curr.val(_lblDoc_Curr);

        _txt_CR_Accept_Commission_amt.prop("disabled", true);
        _txt_CR_Accept_Commission_amt.val('');
        $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_amt").val('');
        //        _txt_CR_Others_amt.prop("disabled", true);
        //        _txt_CR_Others_amt.val('');
        //        _txt_CR_Their_Commission_amt.prop("disabled", true);
        //        _txt_CR_Their_Commission_amt.val('');


        _txt_CR_Accept_Commission_Payer.prop("disabled", true);
        _txt_CR_Accept_Commission_Payer.val('');
        //        _txt_CR_Others_Payer.prop("disabled", true);
        //        _txt_CR_Others_Payer.val('');
        //        _txt_CR_Their_Commission_Payer.prop("disabled", true);
        //        _txt_CR_Their_Commission_Payer.val('');

    }
}
//function toggle_LEDGER_modify() {
//    if ($("#TabContainerMain_tbDocumentDetails_chk_Ledger_Modify").is(':checked')) {
//        //$("#TabContainerMain_tbDocumentDetails_txtLedger_Modify_amt").css('display', 'block');
//        $("#TabContainerMain_tbDocumentDetails_txtLedger_Modify_amt").prop("disabled", false);
//        return true;
//    }
//    else {
//        //$("#TabContainerMain_tbDocumentDetails_txtLedger_Modify_amt").css('display', 'none');
//        $("#TabContainerMain_tbDocumentDetails_txtLedger_Modify_amt").val('');
//        $("#TabContainerMain_tbDocumentDetails_txtLedger_Modify_amt").prop("disabled", true);
//        return true;
//    }
//}
//function SubmitCheck() {
//    if (confirm('Are you sure you want to Submit this record to checker?')) {
//        return true;
//    }
//    else
//        return false;
//}
function SubmitCheck() {

    if (SubmitValidation() == true) {
        if (confirm('Are you sure you want to Submit this record to checker?')) {
            return true;
        }
        else
            return false;
    }
    else
        return false;
}



function validate_Number(evnt) {
    var charCode = (evnt.which) ? evnt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
        return false;
    else
        return true;
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
        else if (CName == 'Received Date') {
            var txtDateReceived = $("#txtDateReceived").val();
            var txtLogdmentDate = $("#txtLogdmentDate").val();
            var _txtDateReceived = new Date(txtDateReceived);
            var _txtLogdmentDate = new Date(txtLogdmentDate);
            if (_txtDateReceived > _txtLogdmentDate) {
                alert("Received Date Should not be greater than Lodgment Date.");
                $("#txtDateReceived").focus();
                return false;
            }
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
function SaveUpdateData() {

    // Document Details
    var hdnUserName = document.getElementById('hdnUserName').value;
    var _BranchName = $("#hdnBranchName").val();
    var _txtDocNo = $("#txtDocNo").val();
    var _txtValueDate = $("#txtValueDate").val();
    var _Document_Curr = $("#lblDoc_Curr").text();

    var _txtHO_Apl = $("#TabContainerMain_tbDocumentDetails_txtHO_Apl").val();
    var _txtIBD_ACC_kind = $("#TabContainerMain_tbDocumentDetails_txtIBD_ACC_kind").val();
    var _txtCommentCode = $("#TabContainerMain_tbDocumentDetails_txtCommentCode").val();
    var _txtAutoSettlement = $("#TabContainerMain_tbDocumentDetails_txtAutoSettlement").val();

    var _txtDraftAmt = $("#TabContainerMain_tbDocumentDetails_txtDraftAmt").val();

    var _txtContractNo = $("#TabContainerMain_tbDocumentDetails_txtContractNo").val();
    var _txtExchRate = $("#TabContainerMain_tbDocumentDetails_txtExchRate").val();

    var _txtCountryRisk = $("#TabContainerMain_tbDocumentDetails_txtCountryRisk").val();
    var _txtRiskCust = $("#TabContainerMain_tbDocumentDetails_txtRiskCust").val();
    var _txtGradeCode = $("#TabContainerMain_tbDocumentDetails_txtGradeCode").val();

    var _txtApplNo = $("#TabContainerMain_tbDocumentDetails_txtApplNo").val();
    var _txtApplBR = $("#TabContainerMain_tbDocumentDetails_txtApplBR").val();
    var _txtPurpose = $("#TabContainerMain_tbDocumentDetails_txtPurpose").val();
    var _ddl_PurposeCode = $("#TabContainerMain_tbDocumentDetails_ddl_PurposeCode").val();

    var _txtsettlCodeForCust = $("#TabContainerMain_tbDocumentDetails_txtsettlCodeForCust").val();
    var _txtsettlforCust_Abbr = $("#TabContainerMain_tbDocumentDetails_txtsettlforCust_Abbr").val();
    var _txtsettlforCust_AccCode = $("#TabContainerMain_tbDocumentDetails_txtsettlforCust_AccCode").val();
    var _txtsettlforCust_AccNo = $("#TabContainerMain_tbDocumentDetails_txtsettlforCust_AccNo").val();

    var _txtInterest_From = $("#TabContainerMain_tbDocumentDetails_txtInterest_From").val();
    var _txtInterest_To = $("#TabContainerMain_tbDocumentDetails_txtInterest_To").val();
    var _txt_No_Of_Days = $("#TabContainerMain_tbDocumentDetails_txt_No_Of_Days").val();

    var _txt_INT_Rate = $("#TabContainerMain_tbDocumentDetails_txt_INT_Rate").val();
    var _txtBaseRate = $("#TabContainerMain_tbDocumentDetails_txtBaseRate").val();
    var _txtSpread = $("#TabContainerMain_tbDocumentDetails_txtSpread").val();
    var _txtInterestAmt = $("#TabContainerMain_tbDocumentDetails_txtInterestAmt").val();

    var _txtFundType = $("#TabContainerMain_tbDocumentDetails_txtFundType").val();
    var _txtInternalRate = $("#TabContainerMain_tbDocumentDetails_txtInternalRate").val();

    var _txtSettl_CodeForBank = $("#TabContainerMain_tbDocumentDetails_txtSettl_CodeForBank").val();
    var _ddl_Settl_ForBank_Abbr = $("#TabContainerMain_tbDocumentDetails_txtSettl_ForBank_Abbr").val();
    var _txtSettl_ForBank_AccCode = $("#TabContainerMain_tbDocumentDetails_txtSettl_ForBank_AccCode").val();
    var _txtSettl_ForBank_AccNo = $("#TabContainerMain_tbDocumentDetails_txtSettl_ForBank_AccNo").val();

    var _txtAttn = $("#TabContainerMain_tbDocumentDetails_txtAttn").val();
    var _txtREM_EUC = $("#TabContainerMain_tbDocumentDetails_txtREM_EUC").val();

    // Instruction
    var _txt_INST_Code = $("#TabContainerMain_tbDocumentInstructions_txt_INST_Code").val();

    // Import Accounting
    var _chk_IMP_ACC_Commission = 'N';
    if ($("#TabContainerMain_tbDocumentAccounting_chk_IMP_ACC_Commission").is(':checked')) {
        _chk_IMP_ACC_Commission = 'Y';
    }
    var _txt_IMP_ACC_ExchRate = $("#TabContainerMain_tbDocumentAccounting_txt_IMP_ACC_ExchRate").val();
    var _txtPrinc_matu = $("#TabContainerMain_tbDocumentAccounting_txtPrinc_matu").val();
    var _txtInterest_matu = $("#TabContainerMain_tbDocumentAccounting_txtInterest_matu").val();
    var _txtCommission_matu = $("#TabContainerMain_tbDocumentAccounting_txtCommission_matu").val();
    var _txtTheir_Commission_matu = $("#TabContainerMain_tbDocumentAccounting_txtTheir_Commission_matu").val();

    var _txtPrinc_lump = $("#TabContainerMain_tbDocumentAccounting_txtPrinc_lump").val();
    var _txtInterest_lump = $("#TabContainerMain_tbDocumentAccounting_txtInterest_lump").val();
    var _txtCommission_lump = $("#TabContainerMain_tbDocumentAccounting_txtCommission_lump").val();
    var _txtTheir_Commission_lump = $("#TabContainerMain_tbDocumentAccounting_txtTheir_Commission_lump").val();

    var _txtprinc_Contract_no = $("#TabContainerMain_tbDocumentAccounting_txtprinc_Contract_no").val();
    var _txtInterest_Contract_no = $("#TabContainerMain_tbDocumentAccounting_txtInterest_Contract_no").val();
    var _txtCommission_Contract_no = $("#TabContainerMain_tbDocumentAccounting_txtCommission_Contract_no").val();
    var _txtTheir_Commission_Contract_no = $("#TabContainerMain_tbDocumentAccounting_txtTheir_Commission_Contract_no").val();

    var _txtprinc_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtprinc_Ex_rate").val();
    var _txtInterest_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtInterest_Ex_rate").val();
    var _txtCommission_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtCommission_Ex_rate").val();
    var _txtTheir_Commission_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtTheir_Commission_Ex_rate").val();

    var _txtprinc_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtprinc_Intnl_Ex_rate").val();
    var _txtInterest_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtInterest_Intnl_Ex_rate").val();
    var _txtCommission_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtCommission_Intnl_Ex_rate").val();
    var _txtTheir_Commission_Intnl_Ex_rate = $("#TabContainerMain_tbDocumentAccounting_txtTheir_Commission_Intnl_Ex_rate").val();

    var _txt_CR_Code = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Code").val();
    var _txt_CR_Cust_abbr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Cust_abbr").val();
    var _txt_CR_Cust_Acc = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Cust_Acc").val();

    var _txt_CR_Acceptance_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Acceptance_amt").val();
    var _txt_CR_Interest_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Interest_amt").val();
    var _txt_CR_Accept_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Accept_Commission_amt").val();
    var _txt_CR_Pay_Handle_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Pay_Handle_Commission_amt").val();
    var _txt_CR_Others_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Others_amt").val();
    var _txt_CR_Their_Commission_amt = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Their_Commission_amt").val();

    var _txt_Princ_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_Princ_Ex_Curr").val();
    var _txt_interest_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_interest_Ex_Curr").val();
    var _txt_Commission_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_Commission_Ex_Curr").val();
    var _txt_Their_Commission_Ex_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_Their_Commission_Ex_Curr").val();

    var _txt_CR_Acceptance_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Acceptance_Curr").val();
    var _txt_CR_Interest_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Interest_Curr").val();
    var _txt_CR_Accept_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Accept_Commission_Curr").val();
    var _txt_CR_Pay_Handle_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Pay_Handle_Commission_Curr").val();
    var _txt_CR_Others_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Others_Curr").val();
    var _txt_CR_Their_Commission_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Their_Commission_Curr").val();
    var _txt_DR_Cur_Acc_Curr = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_Curr").val();
    var _txt_DR_Cur_Acc_Curr2 = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_Curr2").val();
    var _txt_DR_Cur_Acc_Curr3 = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_Curr3").val();


    var _txt_CR_Acceptance_payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Acceptance_payer").val();
    var _txt_CR_Interest_payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Interest_payer").val();
    var _txt_CR_Accept_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Accept_Commission_Payer").val();
    var _txt_CR_Pay_Handle_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Pay_Handle_Commission_Payer").val();
    var _txt_CR_Others_Payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Others_Payer").val();
    var _txt_CR_Their_Commission_Payer = $("#TabContainerMain_tbDocumentAccounting_txt_CR_Their_Commission_Payer").val();


    var _txt_DR_Code = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Code").val();
    var _txt_DR_Cust_abbr = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cust_abbr").val();
    var _txt_DR_Cust_Acc = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cust_Acc").val();
    var _txt_DR_Cur_Acc_amt = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_amt").val();
    var _txt_DR_Cur_Acc_amt2 = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_amt2").val();
    var _txt_DR_Cur_Acc_amt3 = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_amt3").val();

    var _txt_DR_Cur_Acc_payer = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_payer").val();
    var _txt_DR_Cur_Acc_payer2 = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_payer2").val();
    var _txt_DR_Cur_Acc_payer3 = $("#TabContainerMain_tbDocumentAccounting_txt_DR_Cur_Acc_payer3").val();

    //  General_Operations Swift_SFMS_CHRG
    var _chkGO_Swift_SFMS = 'N', _txt_GO_Swift_SFMS_Comment = '', _txt_GO_Swift_SFMS_Ref_No = '',
                _txt_GO_Swift_SFMS_SectionNo = '', _txt_GO_Swift_SFMS_Remarks = '', _txt_GO_Swift_SFMS_Memo = '',

                _txt_GO_Swift_SFMS_Debit_Code = '', _txt_GO_Swift_SFMS_Debit_Curr = '', _txt_GO_Swift_SFMS_Debit_Amt = 0,
                _txt_GO_Swift_SFMS_Debit_Cust = '', _txt_GO_Swift_SFMS_Debit_Cust_AcCode = '', _txt_GO_Swift_SFMS_Debit_Cust_AccNo = '',
                _txt_GO_Swift_SFMS_Debit_Exch_Curr = '', _txt_GO_Swift_SFMS_Debit_Exch_Rate = 0,
                _txt_GO_Swift_SFMS_Debit_AdPrint = '', _txt_GO_Swift_SFMS_Debit_Details = '', _txt_GO_Swift_SFMS_Debit_Entity = '',

                _txt_GO_Swift_SFMS_Credit_Code = '', _txt_GO_Swift_SFMS_Credit_Curr = '', _txt_GO_Swift_SFMS_Credit_Amt = 0,
                _txt_GO_Swift_SFMS_Credit_Cust = '', _txt_GO_Swift_SFMS_Credit_Cust_AcCode = '', _txt_GO_Swift_SFMS_Credit_Cust_AccNo = '',
                _txt_GO_Swift_SFMS_Credit_Exch_Curr = '', _txt_GO_Swift_SFMS_Credit_Exch_Rate = 0,
                _txt_GO_Swift_SFMS_Credit_AdPrint = '', _txt_GO_Swift_SFMS_Credit_Details = '', _txt_GO_Swift_SFMS_Credit_Entity = '',

                _txt_GO_Swift_SFMS_Scheme_no = '',
                _txt_GO_Swift_SFMS_Debit_FUND = '', _txt_GO_Swift_SFMS_Debit_Check_No = '', _txt_GO_Swift_SFMS_Debit_Available = '',
                _txt_GO_Swift_SFMS_Debit_Division = '', _txt_GO_Swift_SFMS_Debit_Inter_Amount = '', _txt_GO_Swift_SFMS_Debit_Inter_Rate = '',

                _txt_GO_Swift_SFMS_Credit_FUND = '', _txt_GO_Swift_SFMS_Credit_Check_No = '', _txt_GO_Swift_SFMS_Credit_Available = '',
                _txt_GO_Swift_SFMS_Credit_Division = '', _txt_GO_Swift_SFMS_Credit_Inter_Amount = '', _txt_GO_Swift_SFMS_Credit_Inter_Rate = '';


    if ($("#TabContainerMain_tbDocumentGO_chkGO_Swift_SFMS").is(':checked')) {
        _chkGO_Swift_SFMS = 'Y';

        _txt_GO_Swift_SFMS_Comment = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Comment").val();
        _txt_GO_Swift_SFMS_Ref_No = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Ref_No").val();

        _txt_GO_Swift_SFMS_SectionNo = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_SectionNo").val();
        _txt_GO_Swift_SFMS_Remarks = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Remarks").val();
        _txt_GO_Swift_SFMS_Memo = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Memo").val();

        // Debit

        _txt_GO_Swift_SFMS_Debit_Code = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Code").val();
        _txt_GO_Swift_SFMS_Debit_Curr = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Curr").val();
        _txt_GO_Swift_SFMS_Debit_Amt = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Amt").val();

        _txt_GO_Swift_SFMS_Debit_Cust = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Cust").val();
        _txt_GO_Swift_SFMS_Debit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Cust_AcCode").val();
        _txt_GO_Swift_SFMS_Debit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Cust_AccNo").val();

        _txt_GO_Swift_SFMS_Debit_Exch_Curr = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Exch_Curr").val();
        _txt_GO_Swift_SFMS_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Exch_Rate").val();

        _txt_GO_Swift_SFMS_Debit_AdPrint = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_AdPrint").val();
        _txt_GO_Swift_SFMS_Debit_Details = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Details").val();
        _txt_GO_Swift_SFMS_Debit_Entity = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Entity").val();

        // Credit

        _txt_GO_Swift_SFMS_Credit_Code = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Code").val();
        _txt_GO_Swift_SFMS_Credit_Curr = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Curr").val();
        _txt_GO_Swift_SFMS_Credit_Amt = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Amt").val();

        _txt_GO_Swift_SFMS_Credit_Cust = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Cust").val();
        _txt_GO_Swift_SFMS_Credit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Cust_AcCode").val();
        _txt_GO_Swift_SFMS_Credit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Cust_AccNo").val();

        _txt_GO_Swift_SFMS_Credit_Exch_Curr = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Exch_Curr").val();
        _txt_GO_Swift_SFMS_Credit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Exch_Rate").val();

        _txt_GO_Swift_SFMS_Credit_AdPrint = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_AdPrint").val();
        _txt_GO_Swift_SFMS_Credit_Details = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Details").val();
        _txt_GO_Swift_SFMS_Credit_Entity = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Entity").val();

        ////////////////////////
        _txt_GO_Swift_SFMS_Scheme_no = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Scheme_no").val();
        _txt_GO_Swift_SFMS_Debit_FUND = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_FUND").val();
        _txt_GO_Swift_SFMS_Debit_Check_No = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Check_No").val();
        _txt_GO_Swift_SFMS_Debit_Available = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Available").val();
        _txt_GO_Swift_SFMS_Debit_Division = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Division").val();
        _txt_GO_Swift_SFMS_Debit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Inter_Amount").val();
        _txt_GO_Swift_SFMS_Debit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Debit_Inter_Rate").val();
        _txt_GO_Swift_SFMS_Credit_FUND = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_FUND").val();
        _txt_GO_Swift_SFMS_Credit_Check_No = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Check_No").val();
        _txt_GO_Swift_SFMS_Credit_Available = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Available").val();
        _txt_GO_Swift_SFMS_Credit_Division = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Division").val();
        _txt_GO_Swift_SFMS_Credit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Inter_Amount").val();
        _txt_GO_Swift_SFMS_Credit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_txt_GO_Swift_SFMS_Credit_Inter_Rate").val();

    }

    //  General_Operations LC_Commitment_CHRG
    var _chkGO_LC_Commitement = 'N', _txt_GO_LC_Commitement_Comment = '', _txt_GO_LC_Commitement_Ref_No = '',
                _txt_GO_LC_Commitement_SectionNo = '', _txt_GO_LC_Commitement_Remarks = '', _txt_GO_LC_Commitement_MEMO = '',

                _txt_GO_LC_Commitement_Debit_Code = '', _txt_GO_LC_Commitement_Debit_Curr = '', _txt_GO_LC_Commitement_Debit_Amt = 0,
                _txt_GO_LC_Commitement_Debit_Cust = '', _txt_GO_LC_Commitement_Debit_Cust_AcCode = '', _txt_GO_LC_Commitement_Debit_Cust_AccNo = '',
                _txt_GO_LC_Commitement_Debit_Exch_Curr = '', _txt_GO_LC_Commitement_Debit_Exch_Rate = 0,
                _txt_GO_LC_Commitement_Debit_Advice_Print = '', _txt_GO_LC_Commitement_Debit_Details = '', _txt_GO_LC_Commitement_Debit_Entity = '',

                _txt_GO_LC_Commitement_Credit_Code = '', _txt_GO_LC_Commitement_Credit_Curr = '', _txt_GO_LC_Commitement_Credit_Amt = 0,
                _txt_GO_LC_Commitement_Credit_Cust = '', _txt_GO_LC_Commitement_Credit_Cust_AcCode = '', _txt_GO_LC_Commitement_Credit_Cust_AccNo = '',
                _txt_GO_LC_Commitement_Credit_Exch_Curr = '', _txt_GO_LC_Commitement_Credit_Exch_Rate = 0,
                _txt_GO_LC_Commitement_Credit_Advice_Print = '', _txt_GO_LC_Commitement_Credit_Details = '', _txt_GO_LC_Commitement_Credit_Entity = '',

                _txt_GO_LC_Commitement_Scheme_no = '',
                _txt_GO_LC_Commitement_Debit_FUND = '', _txt_GO_LC_Commitement_Debit_Check_No = '', _txt_GO_LC_Commitement_Debit_Available = '',
                _txt_GO_LC_Commitement_Debit_Division = '', _txt_GO_LC_Commitement_Debit_Inter_Amount = 0, _txt_GO_LC_Commitement_Debit_Inter_Rate = 0,

                _txt_GO_LC_Commitement_Credit_FUND = '', _txt_GO_LC_Commitement_Credit_Check_No = '', _txt_GO_LC_Commitement_Credit_Available = '',
                _txt_GO_LC_Commitement_Credit_Division = '', _txt_GO_LC_Commitement_Credit_Inter_Amount = 0, _txt_GO_LC_Commitement_Credit_Inter_Rate = 0;

    if ($("#TabContainerMain_tbDocumentGO_chkGO_LC_Commitement").is(':checked')) {
        _chkGO_LC_Commitement = 'Y';
        _txt_GO_LC_Commitement_Ref_No = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Ref_No").val();
        _txt_GO_LC_Commitement_Comment = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Comment").val();

        _txt_GO_LC_Commitement_SectionNo = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_SectionNo").val();
        _txt_GO_LC_Commitement_Remarks = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Remarks").val();
        _txt_GO_LC_Commitement_MEMO = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_MEMO").val();

        //Debit

        _txt_GO_LC_Commitement_Debit_Code = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Debit_Code").val();
        _txt_GO_LC_Commitement_Debit_Curr = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Debit_Curr").val();
        _txt_GO_LC_Commitement_Debit_Amt = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Debit_Amt").val();

        _txt_GO_LC_Commitement_Debit_Cust = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Debit_Cust").val();
        _txt_GO_LC_Commitement_Debit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Debit_Cust_AcCode").val();
        _txt_GO_LC_Commitement_Debit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Debit_Cust_AccNo").val();

        _txt_GO_LC_Commitement_Debit_Exch_Curr = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Debit_Exch_Curr").val();
        _txt_GO_LC_Commitement_Debit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Debit_Exch_Rate").val();

        _txt_GO_LC_Commitement_Debit_Advice_Print = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Debit_Advice_Print").val();
        _txt_GO_LC_Commitement_Debit_Details = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Debit_Details").val();
        _txt_GO_LC_Commitement_Debit_Entity = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Debit_Entity").val();

        //Credit

        _txt_GO_LC_Commitement_Credit_Code = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Credit_Code").val();
        _txt_GO_LC_Commitement_Credit_Curr = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Credit_Curr").val();
        _txt_GO_LC_Commitement_Credit_Amt = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Credit_Amt").val();

        _txt_GO_LC_Commitement_Credit_Cust = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Credit_Cust").val();
        _txt_GO_LC_Commitement_Credit_Cust_AcCode = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Credit_Cust_AcCode").val();
        _txt_GO_LC_Commitement_Credit_Cust_AccNo = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Credit_Cust_AccNo").val();

        _txt_GO_LC_Commitement_Credit_Exch_Curr = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Credit_Exch_Curr").val();
        _txt_GO_LC_Commitement_Credit_Exch_Rate = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Credit_Exch_Rate").val();

        _txt_GO_LC_Commitement_Credit_Advice_Print = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Credit_Advice_Print").val();
        _txt_GO_LC_Commitement_Credit_Details = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Credit_Details").val();
        _txt_GO_LC_Commitement_Credit_Entity = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Credit_Entity").val();

        ///////////////////////////
        _txt_GO_LC_Commitement_Scheme_no = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Scheme_no").val();
        _txt_GO_LC_Commitement_Debit_FUND = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Debit_FUND").val();
        _txt_GO_LC_Commitement_Debit_Check_No = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Debit_Check_No").val();
        _txt_GO_LC_Commitement_Debit_Available = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Debit_Available").val();
        _txt_GO_LC_Commitement_Debit_Division = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Debit_Division").val();
        _txt_GO_LC_Commitement_Debit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Debit_Inter_Amount").val();
        _txt_GO_LC_Commitement_Debit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Debit_Inter_Rate").val();
        _txt_GO_LC_Commitement_Credit_FUND = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Credit_FUND").val();
        _txt_GO_LC_Commitement_Credit_Check_No = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Credit_Check_No").val();
        _txt_GO_LC_Commitement_Credit_Available = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Credit_Available").val();
        _txt_GO_LC_Commitement_Credit_Division = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Credit_Division").val();
        _txt_GO_LC_Commitement_Credit_Inter_Amount = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Credit_Inter_Amount").val();
        _txt_GO_LC_Commitement_Credit_Inter_Rate = $("#TabContainerMain_tbDocumentGO_txt_GO_LC_Commitement_Credit_Inter_Rate").val();

    }

    //// Swift Files
    var None_Flag = '', MT740_Flag = '', MT756_Flag = '', MT999_Flag = '', MT799_Flag = '', MT754_Flag = '', MT747_Flag = '';

    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_None").is(':checked')) {
        None_Flag = 'Y';
    }
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_740").is(':checked')) {
        MT740_Flag = 'Y';
    }

    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_756").is(':checked')) {
        MT756_Flag = 'Y';
    }
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_999").is(':checked')) {
        MT999_Flag = 'Y';
    }
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_799").is(':checked')) {
        MT799_Flag = 'Y';
    }
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_754").is(':checked')) {
        MT754_Flag = 'Y';
    }
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_747").is(':checked')) {
        MT747_Flag = 'Y';
    }


    var _txt_Narrative1 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_1").val();
    var _txt_Narrative2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_2").val();
    var _txt_Narrative3 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_3").val();
    var _txt_Narrative4 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_4").val();
    var _txt_Narrative5 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_5").val();
    var _txt_Narrative6 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_6").val();
    var _txt_Narrative7 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_7").val();
    var _txt_Narrative8 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_8").val();
    var _txt_Narrative9 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_9").val();
    var _txt_Narrative10 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_10").val();
    var _txt_Narrative11 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_11").val();
    var _txt_Narrative12 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_12").val();
    var _txt_Narrative13 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_13").val();
    var _txt_Narrative14 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_14").val();
    var _txt_Narrative15 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_15").val();
    var _txt_Narrative16 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_16").val();
    var _txt_Narrative17 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_17").val();
    var _txt_Narrative18 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_18").val();
    var _txt_Narrative19 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_19").val();
    var _txt_Narrative20 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_20").val();
    var _txt_Narrative21 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_21").val();
    var _txt_Narrative22 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_22").val();
    var _txt_Narrative23 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_23").val();
    var _txt_Narrative24 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_24").val();
    var _txt_Narrative25 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_25").val();
    var _txt_Narrative26 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_26").val();
    var _txt_Narrative27 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_27").val();
    var _txt_Narrative28 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_28").val();
    var _txt_Narrative29 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_29").val();
    var _txt_Narrative30 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_30").val();
    var _txt_Narrative31 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_31").val();
    var _txt_Narrative32 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_32").val();
    var _txt_Narrative33 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_33").val();
    var _txt_Narrative34 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_34").val();
    var _txt_Narrative35 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift799_txt_Narrative_35").val();

    var _ddlNegotiatingBankSwift = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_ddlNegotiatingBankSwift").val();
    var _txtNegoAccountNumber = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtNegoAccountNumber").val();
    var _txtNegoPartyIdentifier = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtNegoPartyIdentifier").val();
    var _txtNegoSwiftCode = '', _txtNegoName = '', _txtNegoAddress1 = '', _txtNegoAddress2 = '', _txtNegoAddress3 = '';
    if (_ddlNegotiatingBankSwift == 'A') {
        _txtNegoSwiftCode = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtNegoSwiftCode").val();
    }
    if (_ddlNegotiatingBankSwift == 'D') {
        _txtNegoName = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtNegoName").val();
        _txtNegoAddress1 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtNegoAddress1").val();
        _txtNegoAddress2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtNegoAddress2").val();
        _txtNegoAddress3 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtNegoAddress3").val();
    }

    // Swift 740
    //New fields added by bhupen on 23082022
    var _txt_740_documentaryCreditno = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_740_documentaryCreditno").val();
    var _txt_740_AccountIdentification = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_740_AccountIdentification").val();
    var _txt_740_Applicablerules = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_740_Applicablerules").val();
    var _txt_740_Date = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_740_Date").val();
    var _txt_740_Placeofexpiry = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_740_Placeofexpiry").val();
    var _txt_740_Draftsat1 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_740_Draftsat1").val();
    var _txt_740_Draftsat2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_740_Draftsat2").val();
    var _txt_740_Draftsat3 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_740_Draftsat3").val();
    var _ddlAvailablewithby_740 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_ddlAvailablewithby_740").val();
    var _txtAvailablewithbyCode = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtAvailablewithbyCode").val();
    var _txtAvailablewithbySwiftCode = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtAvailablewithbySwiftCode").val(); 
    var _txtAvailablewithbySwiftCode = '', _txtAvailablewithbyName = '', _txtAvailablewithbyAddress1 = '', _txtAvailablewithbyAddress2 = '', _txtAvailablewithbyAddress3 = '';
    if (_ddlAvailablewithby_740 == 'A') {
        _txtAvailablewithbySwiftCode = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtAvailablewithbySwiftCode").val();
    }
    if (_ddlAvailablewithby_740 == 'D') {
        _txtAvailablewithbyName = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtAvailablewithbyName").val();
        _txtAvailablewithbyAddress1 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtAvailablewithbyAddress1").val();
        _txtAvailablewithbyAddress2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtAvailablewithbyAddress2").val();
        _txtAvailablewithbyAddress3 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtAvailablewithbyAddress3").val();
    }

    var _ddlDrawee_740 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_ddlDrawee_740").val();
    var _txtDraweeAccountNumber = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtDraweeAccountNumber").val();
    var _txtDraweeSwiftCode = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtDraweeSwiftCode").val();
    var _txtDraweeSwiftCode = '', _txtDraweeName = '', _txtDraweeAddress1 = '', _txtDraweeAddress2 = '', _txtDraweeAddress3 = '';
    if (_ddlDrawee_740 == 'A') {
        _txtDraweeSwiftCode = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtDraweeSwiftCode").val();
    }
    if (_ddlDrawee_740 == 'D') {
        _txtDraweeName = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtDraweeName").val();
        _txtDraweeAddress1 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtDraweeAddress1").val();
        _txtDraweeAddress2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtDraweeAddress2").val();
        _txtDraweeAddress3 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtDraweeAddress3").val();
    }
    var _txt_Acceptance_Beneficiary5 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Beneficiary5").val();
    //----------------------------------------------------------end--------------------------------------------------------------------------------//
    var _txt_Acceptance_Beneficiary = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Beneficiary").val();
    var _txt_Acceptance_Beneficiary2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Beneficiary2").val();
    var _txt_Acceptance_Beneficiary3 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Beneficiary3").val();
    var _txt_Acceptance_Beneficiary4 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Beneficiary4").val();
    var _txtCreditCurrency = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtCreditCurrency").val();
    var _txtCreditAmount = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtCreditAmount").val();
    var _txtPercentageCreditAmountTolerance = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtPercentageCreditAmountTolerance").val();
    var _txtPercentageCreditAmountTolerance1 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtPercentageCreditAmountTolerance1").val();
    var _txt_Acceptance_Max_Credit_Amt = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Max_Credit_Amt").val();
    var _txt_Acceptance_Additional_Amt_Covered = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Additional_Amt_Covered").val();
    var _txt_Acceptance_Additional_Amt_Covered2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Additional_Amt_Covered2").val();
    var _txt_Acceptance_Additional_Amt_Covered3 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Additional_Amt_Covered3").val();
    var _txt_Acceptance_Additional_Amt_Covered4 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Additional_Amt_Covered4").val();
    var _txtMixedPaymentDetails = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtMixedPaymentDetails").val();
    var _txtMixedPaymentDetails2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtMixedPaymentDetails2").val();
    var _txtMixedPaymentDetails3 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtMixedPaymentDetails3").val();
    var _txtMixedPaymentDetails4 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtMixedPaymentDetails4").val();
    var _txtDeferredPaymentDetails = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtDeferredPaymentDetails").val();
    var _txtDeferredPaymentDetails2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtDeferredPaymentDetails2").val();
    var _txtDeferredPaymentDetails3 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtDeferredPaymentDetails3").val();
    var _txtDeferredPaymentDetails4 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txtDeferredPaymentDetails4").val();
    var _txt_Acceptance_Reimbur_Bank_Charges = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Reimbur_Bank_Charges").val();
    var _txt_Acceptance_Other_Charges = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Other_Charges").val();
    var _txt_Acceptance_Other_Charges2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Other_Charges2").val();
    var _txt_Acceptance_Other_Charges3 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Other_Charges3").val();
    var _txt_Acceptance_Other_Charges4 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Other_Charges4").val();
    var _txt_Acceptance_Other_Charges5 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Other_Charges5").val();
    var _txt_Acceptance_Other_Charges6 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Other_Charges6").val();
    var _txt_Acceptance_Sender_to_Receiver_Information = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Sender_to_Receiver_Information").val();
    var _txt_Acceptance_Sender_to_Receiver_Information2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Sender_to_Receiver_Information2").val();
    var _txt_Acceptance_Sender_to_Receiver_Information3 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Sender_to_Receiver_Information3").val();
    var _txt_Acceptance_Sender_to_Receiver_Information4 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Sender_to_Receiver_Information4").val();
    var _txt_Acceptance_Sender_to_Receiver_Information5 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Sender_to_Receiver_Information5").val();
    var _txt_Acceptance_Sender_to_Receiver_Information6 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwift740_txt_Acceptance_Sender_to_Receiver_Information6").val();

    // Swift 756 MT
    var _ddlReceiverCorrespondentMT = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_ddlReceiverCorrespondentMT").val();
    var _txtReceiverAccountNumberMT = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txtReceiverAccountNumberMT").val();
    var _txtReceiverPartyIdentifier = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txtReceiverPartyIdentifier").val();
    var _txtReceiverSwiftCodeMT = '', _txtReceiverLocationMT = '', _txtReceiverNameMT = '', _txtReceiverAddress1MT = '', _txtReceiverAddress2MT = '', _txtReceiverAddress3MT = '';
    if (_ddlReceiverCorrespondentMT == 'A') {
        _txtReceiverSwiftCodeMT = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txtReceiverSwiftCodeMT").val();
    }
    if (_ddlReceiverCorrespondentMT == 'B') {
        _txtReceiverLocationMT = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txtReceiverLocationMT").val();
    }
    if (_ddlReceiverCorrespondentMT == 'D') {
        _txtReceiverNameMT = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txtReceiverNameMT").val();
        _txtReceiverAddress1MT = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txtReceiverAddress1MT").val();
        _txtReceiverAddress2MT = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txtReceiverAddress2MT").val();
        _txtReceiverAddress3MT = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txtReceiverAddress3MT").val();
    }

    var _txt_Narrative_756_1 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_1").val();
    var _txt_Narrative_756_2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_2").val();
    var _txt_Narrative_756_3 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_3").val();
    var _txt_Narrative_756_4 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_4").val();
    var _txt_Narrative_756_5 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_5").val();
    var _txt_Narrative_756_6 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_6").val();
    var _txt_Narrative_756_7 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_7").val();
    var _txt_Narrative_756_8 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_8").val();
    var _txt_Narrative_756_9 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_9").val();
    var _txt_Narrative_756_10 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_10").val();
    var _txt_Narrative_756_11 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_11").val();
    var _txt_Narrative_756_12 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_12").val();
    var _txt_Narrative_756_13 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_13").val();
    var _txt_Narrative_756_14 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_14").val();
    var _txt_Narrative_756_15 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_15").val();
    var _txt_Narrative_756_16 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_16").val();
    var _txt_Narrative_756_17 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_17").val();
    var _txt_Narrative_756_18 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_18").val();
    var _txt_Narrative_756_19 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_19").val();
    var _txt_Narrative_756_20 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_20").val();
    var _txt_Narrative_756_21 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_21").val();
    var _txt_Narrative_756_22 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_22").val();
    var _txt_Narrative_756_23 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_23").val();
    var _txt_Narrative_756_24 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_24").val();
    var _txt_Narrative_756_25 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_25").val();
    var _txt_Narrative_756_26 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_26").val();
    var _txt_Narrative_756_27 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_27").val();
    var _txt_Narrative_756_28 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_28").val();
    var _txt_Narrative_756_29 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_29").val();
    var _txt_Narrative_756_30 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_30").val();
    var _txt_Narrative_756_31 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_31").val();
    var _txt_Narrative_756_32 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_32").val();
    var _txt_Narrative_756_33 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_33").val();
    var _txt_Narrative_756_34 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_34").val();
    var _txt_Narrative_756_35 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Narrative_756_35").val();

    var _txt_Discrepancy_Charges_Swift = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT756_txt_Discrepancy_Charges_Swift").val();

    // Swift 756 SFMS
    var _ddlSenderCorrespondentSFMS = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftSFMS756_ddlSenderCorrespondentSFMS").val();
    var _txtSenderAccountNumberSFMS = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftSFMS756_txtSenderAccountNumberSFMS").val();
    var _txtSenderPartyIdentifierSFMS = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftSFMS756_txtSenderPartyIdentifierSFMS").val();
    var _txtSenderSwiftCodeSFMS = "", _txtSenderLocationSFMS = "", _txtSenderNameSFMS = "", _txtSenderAddress1SFMS = "", _txtSenderAddress2SFMS = "", _txtSenderAddress3SFMS = "";
    if (_ddlSenderCorrespondentSFMS == 'A') {
        _txtSenderSwiftCodeSFMS = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftSFMS756_txtSenderSwiftCodeSFMS").val();
    }
    if (_ddlSenderCorrespondentSFMS == 'B') {
        _txtSenderLocationSFMS = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftSFMS756_txtSenderLocationSFMS").val();
    }
    if (_ddlSenderCorrespondentSFMS == 'D') {
        _txtSenderNameSFMS = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftSFMS756_txtSenderNameSFMS").val();
        _txtSenderAddress1SFMS = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftSFMS756_txtSenderAddress1SFMS").val();
        _txtSenderAddress2SFMS = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftSFMS756_txtSenderAddress2SFMS").val();
        _txtSenderAddress3SFMS = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftSFMS756_txtSenderAddress3SFMS").val();
    }

    var _ddlReceiverCorrespondentSFMS = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftSFMS756_ddlReceiverCorrespondentSFMS").val();
    var _txtReceiverAccountNumberSFMS = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftSFMS756_txtReceiverAccountNumberSFMS").val();
    var _txtReceiverPartyIdentifierSFMS = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftSFMS756_txtReceiverPartyIdentifierSFMS").val();
    var _txtReceiverSwiftCodeSFMS = '', _txtReceiverLocationSFMS = '', _txtReceiverNameSFMS = '', _txtReceiverAddress1SFMS = '', _txtReceiverAddress2SFMS = '', _txtReceiverAddress3SFMS = '';
    if (_ddlReceiverCorrespondentSFMS == 'A') {
        _txtReceiverSwiftCodeSFMS = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftSFMS756_txtReceiverSwiftCodeSFMS").val();
    }
    if (_ddlReceiverCorrespondentSFMS == 'B') {
        _txtReceiverLocationSFMS = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftSFMS756_txtReceiverLocationSFMS").val();
    }
    if (_ddlReceiverCorrespondentSFMS == 'D') {
        _txtReceiverNameSFMS = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftSFMS756_txtReceiverNameSFMS").val();
        _txtReceiverAddress1SFMS = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftSFMS756_txtReceiverAddress1SFMS").val();
        _txtReceiverAddress2SFMS = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftSFMS756_txtReceiverAddress2SFMS").val();
        _txtReceiverAddress3SFMS = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftSFMS756_txtReceiverAddress3SFMS").val();
    }

    var _txt_Discrepancy_Charges_SFMS = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftSFMS756_txt_Discrepancy_Charges_SFMS").val();


    //////////////////    MT 747    ///////////////////////////
    var _txtdoccreditNo_747 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_documentaryCredtno").val();
    var _txtReimbursingbanRef_747 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_reimbursingbankRef").val();
    var _txtDateoforiginalAutoOfReimburse_747 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_DateofogauthriztnRmburse").val();
    var _txtNewdateofExpiry_747 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_NewDateofExpiry").val();
    var _txtIncreaseofDocumentryCreditAmt_Curr_747 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_IncreaseofDocumentaryCreditCurr").val();
    var _txtIncreaseofDocumentryCreditAmt_747 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_IncreaseofDocumentaryCreditAmt").val();
    var _txtdecreaseofDocumentryCreditAmt_Curr_747 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_DecreaseofDocumentaryCreditCurr").val();
    var _txtdecreaseofDocumentryCreditAmt_747 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_DecreaseofDocumentaryCreditAmt").val();
    var _NewDocumentryCreditAmtAfterAmendment_Curr_747 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_NewDocumentaryCreditAmtAfterAmendmentCurr").val();
    var _NewDocumentryCreditAmtAfterAmendment_747 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_NewDocumentaryCreditAmtAfterAmendment").val();
    var _txtPercentageCreditAmtTolerance_747_1 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_PercentageCreditAmtTolerance1").val();
    var _txtPercentageCreditAmtTolerance_747_2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_PercentageCreditAmtTolerance2").val();
    var _txtAddAmtCovered_747_1 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_AdditionalAmtsCovered1").val();
    var _txtAddAmtCovered_747_2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_AdditionalAmtsCovered2").val();
    var _txtAddAmtCovered_747_3 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_AdditionalAmtsCovered3").val();
    var _txtAddAmtCovered_747_4 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_AdditionalAmtsCovered4").val();
    var _txtSenToRecInfo_747_1 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_SentoReceivInfo1").val();
    var _txtSenToRecInfo_747_2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_SentoReceivInfo2").val();
    var _txtSenToRecInfo_747_3 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_SentoReceivInfo3").val();
    var _txtSenToRecInfo_747_4 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_SentoReceivInfo4").val();
    var _txtSenToRecInfo_747_5 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_SentoReceivInfo5").val();
    var _txtSenToRecInfo_747_6 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_747_SentoReceivInfo6").val();

    var _txt_Narrative_747_1 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_1").val();
    var _txt_Narrative_747_2 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_2").val();
    var _txt_Narrative_747_3 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_3").val();
    var _txt_Narrative_747_4 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_4").val();
    var _txt_Narrative_747_5 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_5").val();
    var _txt_Narrative_747_6 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_6").val();
    var _txt_Narrative_747_7 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_7").val();
    var _txt_Narrative_747_8 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_8").val();
    var _txt_Narrative_747_9 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_9").val();
    var _txt_Narrative_747_10 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_10").val();
    var _txt_Narrative_747_11 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_11").val();
    var _txt_Narrative_747_12 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_12").val();
    var _txt_Narrative_747_13 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_13").val();
    var _txt_Narrative_747_14 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_14").val();
    var _txt_Narrative_747_15 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_15").val();
    var _txt_Narrative_747_16 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_16").val();
    var _txt_Narrative_747_17 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_17").val();
    var _txt_Narrative_747_18 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_18").val();
    var _txt_Narrative_747_19 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_19").val();
    var _txt_Narrative_747_20 = $("#TabContainerMain_tbDocumentSwiftFile_TabContainerSwift_tbSwiftMT747_txt_Narrative_747_20").val();
    //////////////////////////////////////////////////  END  ///////////////////////////////////////////////////////////////////////////

    ////LEDGER MODIFICATION
    var _chk_Ledger_Modify = 'N',
    _txtLedgerRemark = '',
    _txtLedgerCustName = '', _txtLedgerAccode = '', _txtLedgerCURR = '',
    _txtLedgerRefNo = '', _txtLedger_Modify_amt = '', _txtLedgerOperationDate = '', _txtLedgerBalanceAmt = '',
    _txtLedgerAcceptDate = '', _txtLedgerMaturity = '', _txtLedgerSettlememtDate = '', _txtLedgerSettlValue = '',
    _txtLedgerLastModDate = '', _txtLedgerREM_EUC = '', _txtLedgerLastOPEDate = '', _txtLedgerTransNo = '',
    _txtLedgerContraCountry = '', _txtLedgerStatusCode = '', _txtLedgerCollectOfComm = '', _txtLedgerCommodity = '',
    _txtLedgerhandlingCommRate = '', _txtLedgerhandlingCommCurr = '', _txtLedgerhandlingCommAmt = '', _txtLedgerhandlingCommPayer = '',
    _txtLedgerPostageRate = '', _txtLedgerPostageCurr = '', _txtLedgerPostageAmt = '', _txtLedgerPostagePayer = '',
    _txtLedgerOthersRate = '', _txtLedgerOthersCurr = '', _txtLedgerOthersAmt = '', _txtLedgerOthersPayer = '',
    _txtLedgerTheirCommRate = '', _txtLedgerTheirCommCurr = '', _txtLedgerTheirCommAmt = '', _txtLedgerTheirCommPayer = '',
    _txtLedgerNegoBank = '',
    _txtLedgerReimbursingBank = '',
    _txtLedgerDrawer = '', _txtLedgerTenor = '', _txtLedgerAttn = '';

    if ($("#TabContainerMain_tbDocumentLedger_chk_Ledger_Modify").is(':checked')) {
        _chk_Ledger_Modify = 'Y';
        var _txtLedgerRemark = $("#TabContainerMain_tbDocumentLedger_txtLedgerRemark").val();
        var _txtLedgerCustName = $("#TabContainerMain_tbDocumentLedger_txtLedgerCustName").val();
        var _txtLedgerAccode = $("#TabContainerMain_tbDocumentLedger_txtLedgerAccode").val();
        var _txtLedgerCURR = $("#TabContainerMain_tbDocumentLedger_txtLedgerCURR").val();
        var _txtLedgerRefNo = $("#TabContainerMain_tbDocumentLedger_txtLedgerRefNo").val();
        var _txtLedger_Modify_amt = $("#TabContainerMain_tbDocumentLedger_txtLedger_Modify_amt").val();
        var _txtLedgerOperationDate = $("#TabContainerMain_tbDocumentLedger_txtLedgerOperationDate").val();
        var _txtLedgerBalanceAmt = $("#TabContainerMain_tbDocumentLedger_txtLedgerBalanceAmt").val();
        var _txtLedgerAcceptDate = $("#TabContainerMain_tbDocumentLedger_txtLedgerAcceptDate").val();
        var _txtLedgerMaturity = $("#TabContainerMain_tbDocumentLedger_txtLedgerMaturity").val();
        var _txtLedgerSettlememtDate = $("#TabContainerMain_tbDocumentLedger_txtLedgerSettlememtDate").val();
        var _txtLedgerSettlValue = $("#TabContainerMain_tbDocumentLedger_txtLedgerSettlValue").val();
        var _txtLedgerLastModDate = $("#TabContainerMain_tbDocumentLedger_txtLedgerLastModDate").val();
        var _txtLedgerREM_EUC = $("#TabContainerMain_tbDocumentLedger_txtLedgerREM_EUC").val();
        var _txtLedgerLastOPEDate = $("#TabContainerMain_tbDocumentLedger_txtLedgerLastOPEDate").val();
        var _txtLedgerTransNo = $("#TabContainerMain_tbDocumentLedger_txtLedgerTransNo").val();
        var _txtLedgerContraCountry = $("#TabContainerMain_tbDocumentLedger_txtLedgerContraCountry").val();
        var _txtLedgerStatusCode = $("#TabContainerMain_tbDocumentLedger_txtLedgerStatusCode").val();
        var _txtLedgerCollectOfComm = $("#TabContainerMain_tbDocumentLedger_txtLedgerCollectOfComm").val();
        var _txtLedgerCommodity = $("#TabContainerMain_tbDocumentLedger_txtLedgerCommodity").val();
        var _txtLedgerhandlingCommRate = $("#TabContainerMain_tbDocumentLedger_txtLedgerhandlingCommRate").val();
        var _txtLedgerhandlingCommCurr = $("#TabContainerMain_tbDocumentLedger_txtLedgerhandlingCommCurr").val();
        var _txtLedgerhandlingCommPayer = $("#TabContainerMain_tbDocumentLedger_txtLedgerhandlingCommPayer").val();
        var _txtLedgerhandlingCommAmt = $("#TabContainerMain_tbDocumentLedger_txtLedgerhandlingCommAmt").val();
        var _txtLedgerPostageRate = $("#TabContainerMain_tbDocumentLedger_txtLedgerPostageRate").val();
        var _txtLedgerPostageCurr = $("#TabContainerMain_tbDocumentLedger_txtLedgerPostageCurr").val();
        var _txtLedgerPostageAmt = $("#TabContainerMain_tbDocumentLedger_txtLedgerPostageAmt").val();
        var _txtLedgerPostagePayer = $("#TabContainerMain_tbDocumentLedger_txtLedgerPostagePayer").val();
        var _txtLedgerOthersRate = $("#TabContainerMain_tbDocumentLedger_txtLedgerOthersRate").val();
        var _txtLedgerOthersCurr = $("#TabContainerMain_tbDocumentLedger_txtLedgerOthersCurr").val();
        var _txtLedgerOthersAmt = $("#TabContainerMain_tbDocumentLedger_txtLedgerOthersAmt").val();
        var _txtLedgerOthersPayer = $("#TabContainerMain_tbDocumentLedger_txtLedgerOthersPayer").val();
        var _txtLedgerTheirCommRate = $("#TabContainerMain_tbDocumentLedger_txtLedgerTheirCommRate").val();
        var _txtLedgerTheirCommCurr = $("#TabContainerMain_tbDocumentLedger_txtLedgerTheirCommCurr").val();
        var _txtLedgerTheirCommAmt = $("#TabContainerMain_tbDocumentLedger_txtLedgerTheirCommAmt").val();
        var _txtLedgerTheirCommPayer = $("#TabContainerMain_tbDocumentLedger_txtLedgerTheirCommPayer").val();
        var _txtLedgerNegoBank = $("#TabContainerMain_tbDocumentLedger_txtLedgerNegoBank").val();

        var _txtLedgerReimbursingBank = $("#TabContainerMain_tbDocumentLedger_txtLedgerReimbursingBank").val();
        var _txtLedgerDrawer = $("#TabContainerMain_tbDocumentLedger_txtLedgerDrawer").val();
        var _txtLedgerTenor = $("#TabContainerMain_tbDocumentLedger_txtLedgerTenor").val();
        var _txtLedgerAttn = $("#TabContainerMain_tbDocumentLedger_txtLedgerAttn").val();
    }



    $.ajax({
        type: "POST",
        url: "TF_IMP_BOOKING_OF_IBD_AND_ACC_Maker.aspx/AddUpdateAceptance",
        data: '{ hdnUserName: "' + hdnUserName + '", _BranchName:"' + _BranchName + '", _txtDocNo: "' + _txtDocNo + '", _Document_Curr: "' + _Document_Curr + '", _txtHO_Apl: "' + _txtHO_Apl + '", _txtIBD_ACC_kind: "' + _txtIBD_ACC_kind + '", _txtValueDate: "' + _txtValueDate +
                    '", _txtCommentCode: "' + _txtCommentCode + '", _txtAutoSettlement: "' + _txtAutoSettlement +
                    '", _txtDraftAmt: "' + _txtDraftAmt + '", _txtContractNo: "' + _txtContractNo + '", _txtExchRate: "' + _txtExchRate +
                    '", _txtCountryRisk: "' + _txtCountryRisk + '", _txtRiskCust: "' + _txtRiskCust +
                    '", _txtGradeCode: "' + _txtGradeCode + '", _txtApplNo: "' + _txtApplNo + '", _txtApplBR: "' + _txtApplBR + '", _txtPurpose: "' + _txtPurpose + '", _ddl_PurposeCode: "' + _ddl_PurposeCode +
                    '", _txtsettlCodeForCust: "' + _txtsettlCodeForCust + '", _txtsettlforCust_Abbr: "' + _txtsettlforCust_Abbr + '", _txtsettlforCust_AccCode: "' + _txtsettlforCust_AccCode + '", _txtsettlforCust_AccNo: "' + _txtsettlforCust_AccNo +
                    '", _txtInterest_From: "' + _txtInterest_From + '", _txtInterest_To: "' + _txtInterest_To + '", _txt_No_Of_Days: "' + _txt_No_Of_Days +
                    '",_txt_INT_Rate: "' + _txt_INT_Rate +
                    '", _txtBaseRate: "' + _txtBaseRate + '", _txtSpread: "' + _txtSpread + '", _txtInterestAmt: "' + _txtInterestAmt + '", _txtFundType: "' + _txtFundType +
                    '", _txtInternalRate: "' + _txtInternalRate + '", _txtSettl_CodeForBank: "' + _txtSettl_CodeForBank + '", _ddl_Settl_ForBank_Abbr: "' + _ddl_Settl_ForBank_Abbr +
                    '", _txtSettl_ForBank_AccCode: "' + _txtSettl_ForBank_AccCode + '", _txtSettl_ForBank_AccNo: "' + _txtSettl_ForBank_AccNo +
                    '", _txtAttn: "' + _txtAttn + '", _txtREM_EUC: "' + _txtREM_EUC +

        //// Instruction
                    '", _txt_INST_Code:"' + _txt_INST_Code +

        //// Import Accounting
                    '", _chk_IMP_ACC_Commission:"' + _chk_IMP_ACC_Commission +
                    '", _txt_IMP_ACC_ExchRate:"' + _txt_IMP_ACC_ExchRate +
                    '", _txtPrinc_matu: "' + _txtPrinc_matu + '", _txtInterest_matu: "' + _txtInterest_matu + '", _txtCommission_matu: "' + _txtCommission_matu + '", _txtTheir_Commission_matu: "' + _txtTheir_Commission_matu +
                    '", _txtPrinc_lump: "' + _txtPrinc_lump + '", _txtInterest_lump: "' + _txtInterest_lump + '", _txtCommission_lump: "' + _txtCommission_lump + '", _txtTheir_Commission_lump: "' + _txtTheir_Commission_lump +
                    '", _txtprinc_Contract_no: "' + _txtprinc_Contract_no + '", _txtInterest_Contract_no: "' + _txtInterest_Contract_no + '", _txtCommission_Contract_no: "' + _txtCommission_Contract_no + '", _txtTheir_Commission_Contract_no: "' + _txtTheir_Commission_Contract_no +
                    '", _txtprinc_Ex_rate: "' + _txtprinc_Ex_rate + '", _txtInterest_Ex_rate: "' + _txtInterest_Ex_rate + '", _txtCommission_Ex_rate: "' + _txtCommission_Ex_rate + '", _txtTheir_Commission_Ex_rate: "' + _txtTheir_Commission_Ex_rate +
                    '", _txtprinc_Intnl_Ex_rate: "' + _txtprinc_Intnl_Ex_rate + '", _txtInterest_Intnl_Ex_rate: "' + _txtInterest_Intnl_Ex_rate + '", _txtCommission_Intnl_Ex_rate: "' + _txtCommission_Intnl_Ex_rate + '", _txtTheir_Commission_Intnl_Ex_rate: "' + _txtTheir_Commission_Intnl_Ex_rate +
                    '", _txt_CR_Code: "' + _txt_CR_Code + '", _txt_CR_Cust_abbr: "' + _txt_CR_Cust_abbr + '", _txt_CR_Cust_Acc: "' + _txt_CR_Cust_Acc +
                    '", _txt_CR_Acceptance_amt: "' + _txt_CR_Acceptance_amt + '", _txt_CR_Interest_amt: "' + _txt_CR_Interest_amt + '", _txt_CR_Accept_Commission_amt: "' + _txt_CR_Accept_Commission_amt +
                    '", _txt_CR_Pay_Handle_Commission_amt: "' + _txt_CR_Pay_Handle_Commission_amt + '", _txt_CR_Others_amt: "' + _txt_CR_Others_amt + '", _txt_CR_Their_Commission_amt: "' + _txt_CR_Their_Commission_amt +
                    '", _txt_CR_Acceptance_payer: "' + _txt_CR_Acceptance_payer + '", _txt_CR_Interest_payer: "' + _txt_CR_Interest_payer + '", _txt_CR_Accept_Commission_Payer: "' + _txt_CR_Accept_Commission_Payer +
                    '", _txt_CR_Pay_Handle_Commission_Payer: "' + _txt_CR_Pay_Handle_Commission_Payer + '", _txt_CR_Others_Payer: "' + _txt_CR_Others_Payer + '", _txt_CR_Their_Commission_Payer: "' + _txt_CR_Their_Commission_Payer +
                    '", _txt_DR_Code: "' + _txt_DR_Code + '", _txt_DR_Cust_abbr: "' + _txt_DR_Cust_abbr + '", _txt_DR_Cust_Acc: "' + _txt_DR_Cust_Acc +
                    '", _txt_DR_Cur_Acc_amt: "' + _txt_DR_Cur_Acc_amt + '", _txt_DR_Cur_Acc_amt2: "' + _txt_DR_Cur_Acc_amt2 + '", _txt_DR_Cur_Acc_amt3: "' + _txt_DR_Cur_Acc_amt3 +
                    '", _txt_DR_Cur_Acc_payer: "' + _txt_DR_Cur_Acc_payer + '", _txt_DR_Cur_Acc_payer2: "' + _txt_DR_Cur_Acc_payer2 + '", _txt_DR_Cur_Acc_payer3: "' + _txt_DR_Cur_Acc_payer3 +

                    '", _txt_Princ_Ex_Curr: "' + _txt_Princ_Ex_Curr + '", _txt_interest_Ex_Curr: "' + _txt_interest_Ex_Curr + '", _txt_Commission_Ex_Curr: "' + _txt_Commission_Ex_Curr + '", _txt_Their_Commission_Ex_Curr: "' + _txt_Their_Commission_Ex_Curr +
                    '", _txt_CR_Acceptance_Curr: "' + _txt_CR_Acceptance_Curr + '", _txt_CR_Interest_Curr: "' + _txt_CR_Interest_Curr + '", _txt_CR_Accept_Commission_Curr: "' + _txt_CR_Accept_Commission_Curr +
                    '", _txt_CR_Pay_Handle_Commission_Curr: "' + _txt_CR_Pay_Handle_Commission_Curr + '", _txt_CR_Others_Curr: "' + _txt_CR_Others_Curr + '", _txt_CR_Their_Commission_Curr: "' + _txt_CR_Their_Commission_Curr +
                    '", _txt_DR_Cur_Acc_Curr: "' + _txt_DR_Cur_Acc_Curr + '", _txt_DR_Cur_Acc_Curr2: "' + _txt_DR_Cur_Acc_Curr2 + '", _txt_DR_Cur_Acc_Curr3: "' + _txt_DR_Cur_Acc_Curr3 +

        ////  General_Operations Swift_SFMS_CHRG
                    '", _chkGO_Swift_SFMS: "' + _chkGO_Swift_SFMS + '", _txt_GO_Swift_SFMS_Comment: "' + _txt_GO_Swift_SFMS_Comment + '", _txt_GO_Swift_SFMS_Ref_No: "' + _txt_GO_Swift_SFMS_Ref_No +
                    '", _txt_GO_Swift_SFMS_SectionNo: "' + _txt_GO_Swift_SFMS_SectionNo + '", _txt_GO_Swift_SFMS_Remarks: "' + _txt_GO_Swift_SFMS_Remarks + '", _txt_GO_Swift_SFMS_Memo: "' + _txt_GO_Swift_SFMS_Memo +

                    '", _txt_GO_Swift_SFMS_Debit_Code: "' + _txt_GO_Swift_SFMS_Debit_Code + '", _txt_GO_Swift_SFMS_Debit_Curr: "' + _txt_GO_Swift_SFMS_Debit_Curr + '", _txt_GO_Swift_SFMS_Debit_Amt: "' + _txt_GO_Swift_SFMS_Debit_Amt +
                    '", _txt_GO_Swift_SFMS_Debit_Cust: "' + _txt_GO_Swift_SFMS_Debit_Cust + '", _txt_GO_Swift_SFMS_Debit_Cust_AcCode: "' + _txt_GO_Swift_SFMS_Debit_Cust_AcCode + '", _txt_GO_Swift_SFMS_Debit_Cust_AccNo: "' + _txt_GO_Swift_SFMS_Debit_Cust_AccNo +
                    '", _txt_GO_Swift_SFMS_Debit_Exch_Curr: "' + _txt_GO_Swift_SFMS_Debit_Exch_Curr + '", _txt_GO_Swift_SFMS_Debit_Exch_Rate: "' + _txt_GO_Swift_SFMS_Debit_Exch_Rate +
                    '", _txt_GO_Swift_SFMS_Debit_AdPrint: "' + _txt_GO_Swift_SFMS_Debit_AdPrint + '", _txt_GO_Swift_SFMS_Debit_Details: "' + _txt_GO_Swift_SFMS_Debit_Details + '", _txt_GO_Swift_SFMS_Debit_Entity: "' + _txt_GO_Swift_SFMS_Debit_Entity +

                    '", _txt_GO_Swift_SFMS_Credit_Code: "' + _txt_GO_Swift_SFMS_Credit_Code + '", _txt_GO_Swift_SFMS_Credit_Curr: "' + _txt_GO_Swift_SFMS_Credit_Curr + '", _txt_GO_Swift_SFMS_Credit_Amt: "' + _txt_GO_Swift_SFMS_Credit_Amt +
                    '", _txt_GO_Swift_SFMS_Credit_Cust: "' + _txt_GO_Swift_SFMS_Credit_Cust + '", _txt_GO_Swift_SFMS_Credit_Cust_AcCode: "' + _txt_GO_Swift_SFMS_Credit_Cust_AcCode + '", _txt_GO_Swift_SFMS_Credit_Cust_AccNo: "' + _txt_GO_Swift_SFMS_Credit_Cust_AccNo +
                    '", _txt_GO_Swift_SFMS_Credit_Exch_Curr: "' + _txt_GO_Swift_SFMS_Credit_Exch_Curr + '", _txt_GO_Swift_SFMS_Credit_Exch_Rate: "' + _txt_GO_Swift_SFMS_Credit_Exch_Rate +
                    '", _txt_GO_Swift_SFMS_Credit_AdPrint: "' + _txt_GO_Swift_SFMS_Credit_AdPrint + '", _txt_GO_Swift_SFMS_Credit_Details: "' + _txt_GO_Swift_SFMS_Credit_Details + '", _txt_GO_Swift_SFMS_Credit_Entity: "' + _txt_GO_Swift_SFMS_Credit_Entity +

                    '", _txt_GO_Swift_SFMS_Scheme_no:"' + _txt_GO_Swift_SFMS_Scheme_no +
                    '", _txt_GO_Swift_SFMS_Debit_FUND:"' + _txt_GO_Swift_SFMS_Debit_FUND + '", _txt_GO_Swift_SFMS_Debit_Check_No:"' + _txt_GO_Swift_SFMS_Debit_Check_No + '", _txt_GO_Swift_SFMS_Debit_Available:"' + _txt_GO_Swift_SFMS_Debit_Available +
                    '", _txt_GO_Swift_SFMS_Debit_Division:"' + _txt_GO_Swift_SFMS_Debit_Division + '", _txt_GO_Swift_SFMS_Debit_Inter_Amount:"' + _txt_GO_Swift_SFMS_Debit_Inter_Amount + '", _txt_GO_Swift_SFMS_Debit_Inter_Rate:"' + _txt_GO_Swift_SFMS_Debit_Inter_Rate +
                    '", _txt_GO_Swift_SFMS_Credit_FUND:"' + _txt_GO_Swift_SFMS_Credit_FUND + '", _txt_GO_Swift_SFMS_Credit_Check_No:"' + _txt_GO_Swift_SFMS_Credit_Check_No + '", _txt_GO_Swift_SFMS_Credit_Available:"' + _txt_GO_Swift_SFMS_Credit_Available +
                    '", _txt_GO_Swift_SFMS_Credit_Division:"' + _txt_GO_Swift_SFMS_Credit_Division + '", _txt_GO_Swift_SFMS_Credit_Inter_Amount:"' + _txt_GO_Swift_SFMS_Credit_Inter_Amount + '", _txt_GO_Swift_SFMS_Credit_Inter_Rate:"' + _txt_GO_Swift_SFMS_Credit_Inter_Rate +
        ////  General_Operations LC_Commitment_CHRG
                    '", _chkGO_LC_Commitement: "' + _chkGO_LC_Commitement + '", _txt_GO_LC_Commitement_Comment: "' + _txt_GO_LC_Commitement_Comment + '", _txt_GO_LC_Commitement_Ref_No: "' + _txt_GO_LC_Commitement_Ref_No +
                    '", _txt_GO_LC_Commitement_SectionNo: "' + _txt_GO_LC_Commitement_SectionNo + '", _txt_GO_LC_Commitement_Remarks: "' + _txt_GO_LC_Commitement_Remarks + '", _txt_GO_LC_Commitement_MEMO: "' + _txt_GO_LC_Commitement_MEMO +

                    '", _txt_GO_LC_Commitement_Debit_Code: "' + _txt_GO_LC_Commitement_Debit_Code + '", _txt_GO_LC_Commitement_Debit_Curr: "' + _txt_GO_LC_Commitement_Debit_Curr + '", _txt_GO_LC_Commitement_Debit_Amt: "' + _txt_GO_LC_Commitement_Debit_Amt +
                    '", _txt_GO_LC_Commitement_Debit_Cust: "' + _txt_GO_LC_Commitement_Debit_Cust + '", _txt_GO_LC_Commitement_Debit_Cust_AcCode: "' + _txt_GO_LC_Commitement_Debit_Cust_AcCode + '", _txt_GO_LC_Commitement_Debit_Cust_AccNo: "' + _txt_GO_LC_Commitement_Debit_Cust_AccNo +
                    '", _txt_GO_LC_Commitement_Debit_Exch_Curr: "' + _txt_GO_LC_Commitement_Debit_Exch_Curr + '", _txt_GO_LC_Commitement_Debit_Exch_Rate: "' + _txt_GO_LC_Commitement_Debit_Exch_Rate +
                    '", _txt_GO_LC_Commitement_Debit_Advice_Print: "' + _txt_GO_LC_Commitement_Debit_Advice_Print + '", _txt_GO_LC_Commitement_Debit_Details: "' + _txt_GO_LC_Commitement_Debit_Details + '", _txt_GO_LC_Commitement_Debit_Entity: "' + _txt_GO_LC_Commitement_Debit_Entity +

                    '", _txt_GO_LC_Commitement_Credit_Code: "' + _txt_GO_LC_Commitement_Credit_Code + '", _txt_GO_LC_Commitement_Credit_Curr: "' + _txt_GO_LC_Commitement_Credit_Curr + '", _txt_GO_LC_Commitement_Credit_Amt: "' + _txt_GO_LC_Commitement_Credit_Amt +
                    '", _txt_GO_LC_Commitement_Credit_Cust: "' + _txt_GO_LC_Commitement_Credit_Cust + '", _txt_GO_LC_Commitement_Credit_Cust_AcCode: "' + _txt_GO_LC_Commitement_Credit_Cust_AcCode + '", _txt_GO_LC_Commitement_Credit_Cust_AccNo: "' + _txt_GO_LC_Commitement_Credit_Cust_AccNo +
                    '", _txt_GO_LC_Commitement_Credit_Exch_Curr: "' + _txt_GO_LC_Commitement_Credit_Exch_Curr + '", _txt_GO_LC_Commitement_Credit_Exch_Rate: "' + _txt_GO_LC_Commitement_Credit_Exch_Rate +
                    '", _txt_GO_LC_Commitement_Credit_Advice_Print: "' + _txt_GO_LC_Commitement_Credit_Advice_Print + '", _txt_GO_LC_Commitement_Credit_Details: "' + _txt_GO_LC_Commitement_Credit_Details + '", _txt_GO_LC_Commitement_Credit_Entity: "' + _txt_GO_LC_Commitement_Credit_Entity +

                    '", _txt_GO_LC_Commitement_Scheme_no:"' + _txt_GO_LC_Commitement_Scheme_no +
                    '", _txt_GO_LC_Commitement_Debit_FUND:"' + _txt_GO_LC_Commitement_Debit_FUND + '", _txt_GO_LC_Commitement_Debit_Check_No:"' + _txt_GO_LC_Commitement_Debit_Check_No + '", _txt_GO_LC_Commitement_Debit_Available:"' + _txt_GO_LC_Commitement_Debit_Available +
                    '", _txt_GO_LC_Commitement_Debit_Division:"' + _txt_GO_LC_Commitement_Debit_Division + '", _txt_GO_LC_Commitement_Debit_Inter_Amount:"' + _txt_GO_LC_Commitement_Debit_Inter_Amount + '", _txt_GO_LC_Commitement_Debit_Inter_Rate:"' + _txt_GO_LC_Commitement_Debit_Inter_Rate +
                    '", _txt_GO_LC_Commitement_Credit_FUND:"' + _txt_GO_LC_Commitement_Credit_FUND + '", _txt_GO_LC_Commitement_Credit_Check_No:"' + _txt_GO_LC_Commitement_Credit_Check_No + '", _txt_GO_LC_Commitement_Credit_Available:"' + _txt_GO_LC_Commitement_Credit_Available +
                    '", _txt_GO_LC_Commitement_Credit_Division:"' + _txt_GO_LC_Commitement_Credit_Division + '", _txt_GO_LC_Commitement_Credit_Inter_Amount:"' + _txt_GO_LC_Commitement_Credit_Inter_Amount + '", _txt_GO_LC_Commitement_Credit_Inter_Rate:"' + _txt_GO_LC_Commitement_Credit_Inter_Rate +

        ////  Swift Files
                    '", None_Flag: "' + None_Flag + '", MT740_Flag: "' + MT740_Flag + '", MT756_Flag: "' + MT756_Flag + '", MT999_Flag: "' + MT999_Flag + '", MT799_Flag: "' + MT799_Flag + '", MT754_Flag: "' + MT754_Flag +

                    '", _txt_Narrative1: "' + _txt_Narrative1 + '", _txt_Narrative2: "' + _txt_Narrative2 + '", _txt_Narrative3: "' + _txt_Narrative3 + '", _txt_Narrative4: "' + _txt_Narrative4 + '", _txt_Narrative5: "' + _txt_Narrative5 +
                    '", _txt_Narrative6: "' + _txt_Narrative6 + '", _txt_Narrative7: "' + _txt_Narrative7 + '", _txt_Narrative8: "' + _txt_Narrative8 + '", _txt_Narrative9: "' + _txt_Narrative9 + '", _txt_Narrative10: "' + _txt_Narrative10 +
                    '", _txt_Narrative11: "' + _txt_Narrative11 + '", _txt_Narrative12: "' + _txt_Narrative12 + '", _txt_Narrative13: "' + _txt_Narrative13 + '", _txt_Narrative14: "' + _txt_Narrative14 + '", _txt_Narrative15: "' + _txt_Narrative15 +
                    '", _txt_Narrative16: "' + _txt_Narrative16 + '", _txt_Narrative17: "' + _txt_Narrative17 + '", _txt_Narrative18: "' + _txt_Narrative18 + '", _txt_Narrative19: "' + _txt_Narrative19 + '", _txt_Narrative20: "' + _txt_Narrative20 +
                    '", _txt_Narrative21: "' + _txt_Narrative21 + '", _txt_Narrative22: "' + _txt_Narrative22 + '", _txt_Narrative23: "' + _txt_Narrative23 + '", _txt_Narrative24: "' + _txt_Narrative24 + '", _txt_Narrative25: "' + _txt_Narrative25 +
                    '", _txt_Narrative26: "' + _txt_Narrative26 + '", _txt_Narrative27: "' + _txt_Narrative27 + '", _txt_Narrative28: "' + _txt_Narrative28 + '", _txt_Narrative29: "' + _txt_Narrative29 + '", _txt_Narrative30: "' + _txt_Narrative30 +
                    '", _txt_Narrative31: "' + _txt_Narrative31 + '", _txt_Narrative32: "' + _txt_Narrative32 + '", _txt_Narrative33: "' + _txt_Narrative33 + '", _txt_Narrative34: "' + _txt_Narrative34 + '", _txt_Narrative35: "' + _txt_Narrative35 +

        ///// Swift 740
                    '",_ddlNegotiatingBankSwift: "' + _ddlNegotiatingBankSwift + '",_txtNegoPartyIdentifier: "' + _txtNegoPartyIdentifier +
                    '",_txtNegoAccountNumber: "' + _txtNegoAccountNumber + '",_txtNegoSwiftCode: "' + _txtNegoSwiftCode + '",_txtNegoName: "' + _txtNegoName +
                    '",_txtNegoAddress1: "' + _txtNegoAddress1 + '",_txtNegoAddress2: "' + _txtNegoAddress2 + '",_txtNegoAddress3: "' + _txtNegoAddress3 +
                    //Added by bhupen on 23082022
                    '",_txt_740_documentaryCreditno: "' + _txt_740_documentaryCreditno + '",_txt_740_AccountIdentification: "' + _txt_740_AccountIdentification +
                    '",_txt_740_Applicablerules: "' + _txt_740_Applicablerules + '",_txt_740_Date: "' + _txt_740_Date + '",_txt_740_Placeofexpiry: "' + _txt_740_Placeofexpiry +
                    '",_txt_740_Draftsat1: "' + _txt_740_Draftsat1 + '",_txt_740_Draftsat2: "' + _txt_740_Draftsat2 + '",_txt_740_Draftsat3: "' + _txt_740_Draftsat3 +
                    '",_ddlAvailablewithby_740: "' + _ddlAvailablewithby_740 + '",_txtAvailablewithbyCode: "' + _txtAvailablewithbyCode + '",_txtAvailablewithbySwiftCode: "' + _txtAvailablewithbySwiftCode +
                    '",_txtAvailablewithbyName: "' + _txtAvailablewithbyName + '",_txtAvailablewithbyAddress1: "' + _txtAvailablewithbyAddress1 +
                     '",_txtAvailablewithbyAddress2: "' + _txtAvailablewithbyAddress2 + '",_txtAvailablewithbyAddress3: "' + _txtAvailablewithbyAddress3 +
                    '",_ddlDrawee_740: "' + _ddlDrawee_740 + '",_txtDraweeAccountNumber: "' + _txtDraweeAccountNumber + '",_txtDraweeSwiftCode: "' + _txtDraweeSwiftCode +
                    '",_txtDraweeName: "' + _txtDraweeName + '",_txtDraweeAddress1: "' + _txtDraweeAddress1 + '",_txtDraweeAddress2: "' + _txtDraweeAddress2 +
                    '",_txtDraweeAddress3: "' + _txtDraweeAddress3 + '",_txt_Acceptance_Beneficiary5: "' + _txt_Acceptance_Beneficiary5 +

                    '",_txt_Acceptance_Beneficiary: "' + _txt_Acceptance_Beneficiary +
                    '",_txt_Acceptance_Beneficiary2: "' + _txt_Acceptance_Beneficiary2 + '",_txt_Acceptance_Beneficiary3: "' + _txt_Acceptance_Beneficiary3 +
                    '",_txt_Acceptance_Beneficiary4: "' + _txt_Acceptance_Beneficiary4 + '",_txtCreditCurrency: "' + _txtCreditCurrency + '",_txtCreditAmount: "' + _txtCreditAmount +
                    '",_txtPercentageCreditAmountTolerance: "' + _txtPercentageCreditAmountTolerance + '",_txtPercentageCreditAmountTolerance1: "' + _txtPercentageCreditAmountTolerance1 + '",_txt_Acceptance_Max_Credit_Amt: "' + _txt_Acceptance_Max_Credit_Amt +
                    '",_txt_Acceptance_Additional_Amt_Covered: "' + _txt_Acceptance_Additional_Amt_Covered + '",_txt_Acceptance_Additional_Amt_Covered2: "' + _txt_Acceptance_Additional_Amt_Covered2 +
                    '",_txt_Acceptance_Additional_Amt_Covered3: "' + _txt_Acceptance_Additional_Amt_Covered3 + '",_txt_Acceptance_Additional_Amt_Covered4: "' + _txt_Acceptance_Additional_Amt_Covered4 +
                    '",_txtMixedPaymentDetails: "' + _txtMixedPaymentDetails + '",_txtMixedPaymentDetails2: "' + _txtMixedPaymentDetails2 + '",_txtMixedPaymentDetails3: "' + _txtMixedPaymentDetails3 +
                    '",_txtMixedPaymentDetails4: "' + _txtMixedPaymentDetails4 + '",_txtDeferredPaymentDetails: "' + _txtDeferredPaymentDetails +
                    '",_txtDeferredPaymentDetails2: "' + _txtDeferredPaymentDetails2 + '",_txtDeferredPaymentDetails3: "' + _txtDeferredPaymentDetails3 +
                    '",_txtDeferredPaymentDetails4: "' + _txtDeferredPaymentDetails4 + '",_txt_Acceptance_Reimbur_Bank_Charges: "' + _txt_Acceptance_Reimbur_Bank_Charges +
                    '",_txt_Acceptance_Other_Charges: "' + _txt_Acceptance_Other_Charges + '",_txt_Acceptance_Other_Charges2: "' + _txt_Acceptance_Other_Charges2 +
                    '",_txt_Acceptance_Other_Charges3: "' + _txt_Acceptance_Other_Charges3 + '",_txt_Acceptance_Other_Charges4: "' + _txt_Acceptance_Other_Charges4 +
                    '",_txt_Acceptance_Other_Charges5: "' + _txt_Acceptance_Other_Charges5 + '",_txt_Acceptance_Other_Charges6: "' + _txt_Acceptance_Other_Charges6 +
                    '",_txt_Acceptance_Sender_to_Receiver_Information: "' + _txt_Acceptance_Sender_to_Receiver_Information + '",_txt_Acceptance_Sender_to_Receiver_Information2: "' + _txt_Acceptance_Sender_to_Receiver_Information2 +
                    '",_txt_Acceptance_Sender_to_Receiver_Information3: "' + _txt_Acceptance_Sender_to_Receiver_Information3 + '",_txt_Acceptance_Sender_to_Receiver_Information4: "' + _txt_Acceptance_Sender_to_Receiver_Information4 +
                    '",_txt_Acceptance_Sender_to_Receiver_Information5: "' + _txt_Acceptance_Sender_to_Receiver_Information5 + '",_txt_Acceptance_Sender_to_Receiver_Information6: "' + _txt_Acceptance_Sender_to_Receiver_Information6 +

        ///// Swift 756 MT
                    '",_ddlReceiverCorrespondentMT: "' + _ddlReceiverCorrespondentMT + '",_txtReceiverPartyIdentifier: "' + _txtReceiverPartyIdentifier + '",_txtReceiverAccountNumberMT: "' + _txtReceiverAccountNumberMT +
                    '",_txtReceiverSwiftCodeMT: "' + _txtReceiverSwiftCodeMT + '",_txtReceiverNameMT: "' + _txtReceiverNameMT +
                    '",_txtReceiverLocationMT: "' + _txtReceiverLocationMT + '",_txtReceiverAddress1MT: "' + _txtReceiverAddress1MT +
                    '",_txtReceiverAddress2MT: "' + _txtReceiverAddress2MT + '",_txtReceiverAddress3MT: "' + _txtReceiverAddress3MT +

                    '", _txt_Narrative_756_1: "' + _txt_Narrative_756_1 + '", _txt_Narrative_756_2: "' + _txt_Narrative_756_2 + '", _txt_Narrative_756_3: "' + _txt_Narrative_756_3 + '", _txt_Narrative_756_4: "' + _txt_Narrative_756_4 + '", _txt_Narrative_756_5: "' + _txt_Narrative_756_5 +
                    '", _txt_Narrative_756_6: "' + _txt_Narrative_756_6 + '", _txt_Narrative_756_7: "' + _txt_Narrative_756_7 + '", _txt_Narrative_756_8: "' + _txt_Narrative_756_8 + '", _txt_Narrative_756_9: "' + _txt_Narrative_756_9 + '", _txt_Narrative_756_10: "' + _txt_Narrative_756_10 +
                    '", _txt_Narrative_756_11: "' + _txt_Narrative_756_11 + '", _txt_Narrative_756_12: "' + _txt_Narrative_756_12 + '", _txt_Narrative_756_13: "' + _txt_Narrative_756_13 + '", _txt_Narrative_756_14: "' + _txt_Narrative_756_14 + '", _txt_Narrative_756_15: "' + _txt_Narrative_756_15 +
                    '", _txt_Narrative_756_16: "' + _txt_Narrative_756_16 + '", _txt_Narrative_756_17: "' + _txt_Narrative_756_17 + '", _txt_Narrative_756_18: "' + _txt_Narrative_756_18 + '", _txt_Narrative_756_19: "' + _txt_Narrative_756_19 + '", _txt_Narrative_756_20: "' + _txt_Narrative_756_20 +
                    '", _txt_Narrative_756_21: "' + _txt_Narrative_756_21 + '", _txt_Narrative_756_22: "' + _txt_Narrative_756_22 + '", _txt_Narrative_756_23: "' + _txt_Narrative_756_23 + '", _txt_Narrative_756_24: "' + _txt_Narrative_756_24 + '", _txt_Narrative_756_25: "' + _txt_Narrative_756_25 +
                    '", _txt_Narrative_756_26: "' + _txt_Narrative_756_26 + '", _txt_Narrative_756_27: "' + _txt_Narrative_756_27 + '", _txt_Narrative_756_28: "' + _txt_Narrative_756_28 + '", _txt_Narrative_756_29: "' + _txt_Narrative_756_29 + '", _txt_Narrative_756_30: "' + _txt_Narrative_756_30 +
                    '", _txt_Narrative_756_31: "' + _txt_Narrative_756_31 + '", _txt_Narrative_756_32: "' + _txt_Narrative_756_32 + '", _txt_Narrative_756_33: "' + _txt_Narrative_756_33 + '", _txt_Narrative_756_34: "' + _txt_Narrative_756_34 + '", _txt_Narrative_756_35: "' + _txt_Narrative_756_35 +
                    '", _txt_Discrepancy_Charges_Swift: "' + _txt_Discrepancy_Charges_Swift +
        ///// Swift 756 SFMS
                    '",_ddlReceiverCorrespondentSFMS: "' + _ddlReceiverCorrespondentSFMS + '",_txtReceiverAccountNumberSFMS: "' + _txtReceiverAccountNumberSFMS +
                    '",_txtSenderPartyIdentifierSFMS: "' + _txtSenderPartyIdentifierSFMS + '",_txtReceiverPartyIdentifierSFMS: "' + _txtReceiverPartyIdentifierSFMS +
                    '",_txtReceiverSwiftCodeSFMS: "' + _txtReceiverSwiftCodeSFMS + '",_txtReceiverNameSFMS: "' + _txtReceiverNameSFMS +
                    '",_txtReceiverLocationSFMS: "' + _txtReceiverLocationSFMS + '",_txtReceiverAddress1SFMS: "' + _txtReceiverAddress1SFMS +
                    '",_txtReceiverAddress2SFMS: "' + _txtReceiverAddress2SFMS + '",_txtReceiverAddress3SFMS: "' + _txtReceiverAddress3SFMS +

                    '",_ddlSenderCorrespondentSFMS: "' + _ddlSenderCorrespondentSFMS + '",_txtSenderAccountNumberSFMS: "' + _txtSenderAccountNumberSFMS +
                    '",_txtSenderSwiftCodeSFMS: "' + _txtSenderSwiftCodeSFMS + '",_txtSenderNameSFMS: "' + _txtSenderNameSFMS +
                    '",_txtSenderLocationSFMS: "' + _txtSenderLocationSFMS + '",_txtSenderAddress1SFMS: "' + _txtSenderAddress1SFMS +
                    '",_txtSenderAddress2SFMS: "' + _txtSenderAddress2SFMS + '",_txtSenderAddress3SFMS: "' + _txtSenderAddress3SFMS +
                    '", _txt_Discrepancy_Charges_SFMS: "' + _txt_Discrepancy_Charges_SFMS +

        //////////////////////////////////   Bhupen   ////////////////////////
        // MT 747

                          '", MT747_Flag: "' + MT747_Flag + '", _txtdoccreditNo_747: "' + _txtdoccreditNo_747 + '", _txtReimbursingbanRef_747: "' + _txtReimbursingbanRef_747 +
                       '", _txtDateoforiginalAutoOfReimburse_747: "' + _txtDateoforiginalAutoOfReimburse_747 + '", _txtNewdateofExpiry_747: "' + _txtNewdateofExpiry_747 +
                     '", _txtIncreaseofDocumentryCreditAmt_Curr_747 : "' + _txtIncreaseofDocumentryCreditAmt_Curr_747 + '", _txtIncreaseofDocumentryCreditAmt_747: "' + _txtIncreaseofDocumentryCreditAmt_747 +
                   '", _txtdecreaseofDocumentryCreditAmt_Curr_747: "' + _txtdecreaseofDocumentryCreditAmt_Curr_747 + '", _txtdecreaseofDocumentryCreditAmt_747: "' + _txtdecreaseofDocumentryCreditAmt_747 +
                    '", _NewDocumentryCreditAmtAfterAmendment_Curr_747: "' + _NewDocumentryCreditAmtAfterAmendment_Curr_747 + '", _NewDocumentryCreditAmtAfterAmendment_747: "' + _NewDocumentryCreditAmtAfterAmendment_747 +
                       '", _txtPercentageCreditAmtTolerance_747_1: "' + _txtPercentageCreditAmtTolerance_747_1 + '", _txtPercentageCreditAmtTolerance_747_2: "' + _txtPercentageCreditAmtTolerance_747_2 +
                     '", _txtAddAmtCovered_747_1: "' + _txtAddAmtCovered_747_1 + '", _txtAddAmtCovered_747_2: "' + _txtAddAmtCovered_747_2 +
                    '", _txtAddAmtCovered_747_3: "' + _txtAddAmtCovered_747_3 + '", _txtAddAmtCovered_747_4: "' + _txtAddAmtCovered_747_4 +
                     '", _txtSenToRecInfo_747_1: "' + _txtSenToRecInfo_747_1 + '", _txtSenToRecInfo_747_2: "' + _txtSenToRecInfo_747_2 +
                    '", _txtSenToRecInfo_747_3: "' + _txtSenToRecInfo_747_3 + '", _txtSenToRecInfo_747_4: "' + _txtSenToRecInfo_747_4 +
                    '", _txtSenToRecInfo_747_5: "' + _txtSenToRecInfo_747_5 + '", _txtSenToRecInfo_747_6: "' + _txtSenToRecInfo_747_6 +

                    '", _txt_Narrative_747_1: "' + _txt_Narrative_747_1 + '", _txt_Narrative_747_2: "' + _txt_Narrative_747_2 + '", _txt_Narrative_747_3: "' + _txt_Narrative_747_3 + '", _txt_Narrative_747_4: "' + _txt_Narrative_747_4 + '", _txt_Narrative_747_5: "' + _txt_Narrative_747_5 +
                    '", _txt_Narrative_747_6: "' + _txt_Narrative_747_6 + '", _txt_Narrative_747_7: "' + _txt_Narrative_747_7 + '", _txt_Narrative_747_8: "' + _txt_Narrative_747_8 + '", _txt_Narrative_747_9: "' + _txt_Narrative_747_9 + '", _txt_Narrative_747_10: "' + _txt_Narrative_747_10 +
                    '", _txt_Narrative_747_11: "' + _txt_Narrative_747_11 + '", _txt_Narrative_747_12: "' + _txt_Narrative_747_12 + '", _txt_Narrative_747_13: "' + _txt_Narrative_747_13 + '", _txt_Narrative_747_14: "' + _txt_Narrative_747_14 + '", _txt_Narrative_747_15: "' + _txt_Narrative_747_15 +
                    '", _txt_Narrative_747_16: "' + _txt_Narrative_747_16 + '", _txt_Narrative_747_17: "' + _txt_Narrative_747_17 + '", _txt_Narrative_747_18: "' + _txt_Narrative_747_18 + '", _txt_Narrative_747_19: "' + _txt_Narrative_747_19 + '", _txt_Narrative_747_20: "' + _txt_Narrative_747_20 +

        ///////////////////////////////////////  End /////////////////////////////////////

        ////LEDGER MODIFICATION _txtLedgerRemark
                    '", _chk_Ledger_Modify:"' + _chk_Ledger_Modify +
                    '", _txtLedgerRemark:"' + _txtLedgerRemark +
                    '", _txtLedgerCustName:"' + _txtLedgerCustName + '", _txtLedgerAccode:"' + _txtLedgerAccode + '", _txtLedgerCURR:"' + _txtLedgerCURR +
                    '", _txtLedgerRefNo:"' + _txtLedgerRefNo + '", _txtLedger_Modify_amt:"' + _txtLedger_Modify_amt +
                    '", _txtLedgerOperationDate:"' + _txtLedgerOperationDate + '", _txtLedgerBalanceAmt:"' + _txtLedgerBalanceAmt +
                    '", _txtLedgerAcceptDate:"' + _txtLedgerAcceptDate + '", _txtLedgerMaturity:"' + _txtLedgerMaturity +
                    '", _txtLedgerSettlememtDate:"' + _txtLedgerSettlememtDate + '", _txtLedgerSettlValue:"' + _txtLedgerSettlValue +
                    '", _txtLedgerLastModDate:"' + _txtLedgerLastModDate + '", _txtLedgerREM_EUC:"' + _txtLedgerREM_EUC +
                    '", _txtLedgerLastOPEDate:"' + _txtLedgerLastOPEDate + '", _txtLedgerTransNo:"' + _txtLedgerTransNo +
                    '", _txtLedgerContraCountry:"' + _txtLedgerContraCountry + '", _txtLedgerStatusCode:"' + _txtLedgerStatusCode +
                    '", _txtLedgerCollectOfComm:"' + _txtLedgerCollectOfComm + '", _txtLedgerCommodity:"' + _txtLedgerCommodity +
                    '", _txtLedgerhandlingCommRate:"' + _txtLedgerhandlingCommRate + '", _txtLedgerhandlingCommCurr:"' + _txtLedgerhandlingCommCurr +
                    '", _txtLedgerhandlingCommAmt:"' + _txtLedgerhandlingCommAmt + '", _txtLedgerhandlingCommPayer:"' + _txtLedgerhandlingCommPayer +
                    '", _txtLedgerPostageRate:"' + _txtLedgerPostageRate + '", _txtLedgerPostageCurr:"' + _txtLedgerPostageCurr +
                    '", _txtLedgerPostageAmt:"' + _txtLedgerPostageAmt + '", _txtLedgerPostagePayer:"' + _txtLedgerPostagePayer +
                    '", _txtLedgerOthersRate:"' + _txtLedgerOthersRate + '", _txtLedgerOthersCurr:"' + _txtLedgerOthersCurr +
                    '", _txtLedgerOthersAmt:"' + _txtLedgerOthersAmt + '", _txtLedgerOthersPayer:"' + _txtLedgerOthersPayer +
                    '", _txtLedgerTheirCommRate:"' + _txtLedgerTheirCommRate + '", _txtLedgerTheirCommCurr:"' + _txtLedgerTheirCommCurr +
                    '", _txtLedgerTheirCommAmt:"' + _txtLedgerTheirCommAmt + '", _txtLedgerTheirCommPayer:"' + _txtLedgerTheirCommPayer +
                    '", _txtLedgerNegoBank:"' + _txtLedgerNegoBank +
                    '", _txtLedgerReimbursingBank:"' + _txtLedgerReimbursingBank +
                    '", _txtLedgerDrawer:"' + _txtLedgerDrawer + '", _txtLedgerTenor:"' + _txtLedgerTenor + '", _txtLedgerAttn:"' + _txtLedgerAttn +
                    '"}',

        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function (response) {
            alert(response.d);
        },
        error: function (response) {
            alert(response.d);
        }
    });
}

function OnSuccess(response) {
}
function ViewSwiftMessage() {
    SaveUpdateData();
    var _txtDocNo = $("#txtDocNo").val();
    var _SWIFT_File_Type = '';
    var _ddl_Nego_Remit_Bank_Type = $("#hdnNegoRemiBankType").val();
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_740").is(':checked')) {
        _SWIFT_File_Type = 'MT740';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_756").is(':checked')) {
        _SWIFT_File_Type = 'MT756';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_999").is(':checked')) {
        _SWIFT_File_Type = 'MT999756';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_799").is(':checked')) {
        _SWIFT_File_Type = 'MT799ACC';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_747").is(':checked')) {
        _SWIFT_File_Type = 'MT747';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    return false;
}
function ViewSFMSMessage() {
    SaveUpdateData();
    var _txtDocNo = $("#txtDocNo").val();
    var _SWIFT_File_Type = '';
    var _ddl_Nego_Remit_Bank_Type = $("#hdnNegoRemiBankType").val();
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_740").is(':checked')) {
        _SWIFT_File_Type = 'MT740';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_756").is(':checked')) {
        _SWIFT_File_Type = 'MT756';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_999").is(':checked')) {
        _SWIFT_File_Type = 'MT999756';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_799").is(':checked')) {
        _SWIFT_File_Type = 'MT799ACC';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    if ($("#TabContainerMain_tbDocumentSwiftFile_rdb_swift_747").is(':checked')) {
        _SWIFT_File_Type = 'MT747';
        var winame = window.open('../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=' + _txtDocNo + '&FileType=' + _SWIFT_File_Type + '&Type=' + _ddl_Nego_Remit_Bank_Type, '', 'scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400');
        winame.focus();
    }
    return false;
}