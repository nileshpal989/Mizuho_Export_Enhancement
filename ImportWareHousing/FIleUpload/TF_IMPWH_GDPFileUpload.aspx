<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMPWH_GDPFileUpload.aspx.cs"
    Inherits="ImportWareHousing_FIleUpload_TF_IMPWH_GDPFileUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/Style_new.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/jquery.min.js"></script>
    <script type="text/javascript" language="javascript">
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
        function Confirm(a, b, c) {
            try {
                $("#hdnFilePath").val(a);
                $("#hdnFileExtension").val(b);
                $("#hdnFileName").val(c);

                if (confirm("Do you want to save data?")) {
                    document.getElementById('btnConfirm').click();
                    $('#btnSubmit').trigger('click');
                } else {
                    return false;
                }
            }
            catch (err) {
                alert(err.Message);
            }
        }     
    </script>
    <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: transparent;
            z-index: 99;
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid navy;
            width: 400px;
            height: 40px;
            display: none;
            position: absolute;
            background-color: white;
            z-index: 999;
        }
        
        hr
        {
            display: block;
            margin-top: 0.5em;
            margin-bottom: 0.5em;
            margin-left: auto;
            margin-right: auto;
            border-style: inset;
            border-width: 2px;
        }
        
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=40);
            opacity: 0.4;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            width: 400px;
            border: 3px solid navy;
        }
        .modalPopup .header
        {
            background-color: navy;
            height: 30px;
            color: White;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }
        .modalPopup .body
        {
            padding-top:5px;
            min-height: 100px;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }
        .modalPopup .footerAjaxModel
        {
            padding-bottom: 5px;
        }
        .modalPopup .Yes, .modalPopup .No
        {
            margin: 0 1px 0 0;
            padding: 4px 10px;
            width: 100px;
            background: #007DFB;
            color: #FFF;
            text-align: center;
            text-decoration: none;
            border: 0;
            cursor: pointer;
            font-size: 11px;
        }
        .modalPopup .Yes
        {
            background-color: #2FBDF1;
            border: 1px solid #0DA9D0;
        }
        .modalPopup .No
        {
            background-color: #9F9F9F;
            border: 1px solid #5C5C5C;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
        PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground"
        CancelControlID="btnNo">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
        <div class="header">
            Confirmation
        </div>
        <div class="body">
            This File Already Uploaded Do you want to Upload It Again?
        </div>
        <div class="footerAjaxModel" align="center">
            <asp:Button ID="btnYes" runat="server" CssClass="Yes" Text="Yes" OnClick="btnYes_Click" />
            <asp:Button ID="btnNo" runat="server" Text="No" CssClass="No" />
        </div>
    </asp:Panel>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="loading" align="center">
        Uploading file. Please wait.
        <br />
        <img src="../../Images/ProgressBar1.gif" alt="" />
    </div>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <Triggers>
                    <%--<asp:PostBackTrigger ControlID="btnUpldCSV" />--%>
                    <asp:PostBackTrigger ControlID="btnUpload" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <input type="hidden" id="hdnCustId" runat="server" />
                            <input type="hidden" id="hdnRecordCount" runat="server" />
                            <input type="hidden" id="hdnStatus" runat="server" />
                            <input type="hidden" id="hdnYesNo" runat="server" />
                            <input type="hidden" id="hdnFilePath" runat="server" />
                            <input type="hidden" id="hdnFileName" runat="server" />
                            <input type="hidden" id="hdnFileExtension" runat="server" />
                            <td align="left" style="width: 50%; font-weight: bold" valign="bottom">
                                &nbsp;<span class="pageLabel" style="font-weight: bold">Good To Pay Authorization File Upload</span>
                            </td>
                            <td align="right" style="width: 50%">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%;" valign="top" colspan="2">
                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                    <tr>
                                        <td colspan="2" nowrap="nowrap" align="center">
                                            <%--<asp:Label ID="labelMessage1" runat="server" CssClass="mandatoryField"></asp:Label>--%>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td align="right" style="width: 150px">
                                            <span class="mandatoryField">*</span>&nbsp;<span class="elementLabel">Select File :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:FileUpload ID="fileinhouse" runat="server" ViewStateMode="Enabled" TabIndex="3"
                                                Width="500px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Input File :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:TextBox ID="txtInputFile" runat="server" CssClass="textBox" MaxLength="10"
                                                Width="413px" TabIndex="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <table border="0" style="border-color: red" cellpadding="2">
                                                <tr>
                                                    <td width="130px" nowrap="nowrap">
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Button ID="btnUpload" runat="server" Text="Upload Excel File" CssClass="buttonDefault"
                                                                    ToolTip="Upload Excel File" TabIndex="4" OnClick="btnUpload_Click" />
                                                                <asp:Button ID="btnValidate" CssClass="buttonDefault" Text="Validate" runat="server"
                                                                    OnClick="btnValidate_Click" />
                                                                <asp:Button ID="btnProcess" CssClass="buttonDefault" Text="Process" runat="server"
                                                                    OnClick="btnProcess_Click" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="130px" nowrap="nowrap">
                                                        <div id="divUpload" style="display: none">
                                                            <div style="width: 300pt; text-align: center;">
                                                                Uploading...
                                                            </div>
                                                            <div style="width: 300pt; height: 20px; border: solid 1pt gray">
                                                                <div id="divProgress" runat="server" style="width: 1pt; height: 20px; background-color: orange;
                                                                    display: none">
                                                                </div>
                                                            </div>
                                                            <div style="width: 300pt; text-align: center;">
                                                                <asp:Label ID="lblPercentage" runat="server" Text="Label"></asp:Label>
                                                            </div>
                                                            <br />
                                                            <asp:Label ID="Label9" runat="server" ForeColor="Red" Text=""></asp:Label>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblHint" CssClass="mandatoryField" Font-Size="Small" Font-Bold="true"
                                                            runat="server" />&nbsp;
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                        <td>
                                                            <span class="mandatoryField">*</span>
                                                            <asp:Label ID="lbldateformathint" Font-Bold="true" Font-Size="Medium" Text="Excel file all date column should be formated in UK 'dd/mm/yyyy' format."
                                                                CssClass="mandatoryField" runat="server" />&nbsp;
                                                        </td>
                                                    </tr>--%>
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <span class="mandatoryField">*</span>
                                                        <asp:Label ID="Label1" Font-Bold="true" Font-Size="Medium" Text=" 1." CssClass="mandatoryField"
                                                            runat="server" />&nbsp;
                                                        <asp:Label ID="Label6" Font-Bold="true" Font-Size="Medium" Text=" First Upload Excel File."
                                                            CssClass="mandatoryField" runat="server" />
                                                        &nbsp; &nbsp;<asp:Label ID="Label4" Text=" 2." Font-Size="Medium" Font-Bold="true"
                                                            CssClass="mandatoryField" runat="server" />&nbsp;
                                                        <asp:Label ID="Label7" Font-Bold="true" Font-Size="Medium" Text=" Validate For Error Records. "
                                                            CssClass="mandatoryField" runat="server" />
                                                        &nbsp; &nbsp;<asp:Label ID="Label5" Font-Bold="true" Font-Size="Medium" Text=" 3. "
                                                            CssClass="mandatoryField" runat="server" />&nbsp;
                                                        <asp:Label ID="Label8" Text=" Process. " Font-Bold="true" Font-Size="Medium" CssClass="mandatoryField"
                                                            runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Label runat="server" CssClass="elementLabel" ID="labelMessage" Style="font-weight: bold;"></asp:Label>
                                                                &nbsp;<br />
                                                                <asp:Label runat="server" CssClass="elementLabel" ID="label2" Style="font-weight: bold;"></asp:Label>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                                                                <asp:Label runat="server" CssClass="elementLabel" ID="label3" Style="font-weight: bold;"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>
