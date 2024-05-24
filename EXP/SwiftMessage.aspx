<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SwiftMessage.aspx.cs" Inherits="EXP_SwiftMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=11" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
    <script language="javascript" type="text/javascript">
        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }
        function OpenLogded_DocNoList(e, Swift) {
            var keycode;
            var DocRefNo;
            var BranchCode = $get("ddlBranch").value;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                if (Swift == 'Swift742') {
                    DocRefNo = $get("TabContainerMain_TabSwifts_txt_742_ClaimBankRef");
                }
                else if (Swift == 'Swift754') {
                    DocRefNo = $get("TabContainerMain_TabSwifts_txt_754_SenRef");
                }
                else if (Swift == 'Swift799') {
                    DocRefNo = $get("TabContainerMain_TabSwifts_txt_799_transRefNo");
                }
                else if (Swift == 'Swift499') {
                    DocRefNo = $get("TabContainerMain_TabSwifts_txt_499_transRefNo");
                }
                else if (Swift == 'Swift420') {
                    DocRefNo = $get("TabContainerMain_TabSwifts_txt_420_SendingBankTRN");
                }
                open_popup('Expswift_DocNoHelp.aspx?hNo=1&Swift=' + Swift + '&BranchCode=' + BranchCode + '&DocNo=' + DocRefNo.value, 450, 650, 'OverseasBankList');
                return false;
            }
        }
        function selectLogded_DocNo(selectedID, Swift) {
            var DocRefNo;

            if (Swift == 'Swift742') {
                DocRefNo = $get("TabContainerMain_TabSwifts_txt_742_ClaimBankRef");
                DocRefNo.value = selectedID;
                $get("TabContainerMain_TabSwifts_txt_742_ClaimBankRef").onchange();
            }
            else if (Swift == 'Swift754') {
                DocRefNo = $get("TabContainerMain_TabSwifts_txt_754_SenRef");
                DocRefNo.value = selectedID;
                $get("TabContainerMain_TabSwifts_txt_754_SenRef").onchange();
            }
            else if (Swift == 'Swift799') {
                DocRefNo = $get("TabContainerMain_TabSwifts_txt_799_transRefNo");
                DocRefNo.value = selectedID;
                $get("TabContainerMain_TabSwifts_txt_799_transRefNo").onchange();
            }
            else if (Swift == 'Swift499') {
                DocRefNo = $get("TabContainerMain_TabSwifts_txt_499_transRefNo");
                DocRefNo.value = selectedID;
                $get("TabContainerMain_TabSwifts_txt_499_transRefNo").onchange();
            }
            else if (Swift == 'Swift420') {
                DocRefNo = $get("TabContainerMain_TabSwifts_txt_420_SendingBankTRN");
                DocRefNo.value = selectedID;
                $get("TabContainerMain_TabSwifts_txt_420_SendingBankTRN").onchange();
            }
        }
        function OpenOverseasBankList(e, Swift) {
            var keycode;
            var txtOverseasBankID;

            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                if (Swift == 'Swift742') {
                    txtOverseasBankID = $get("TabContainerMain_TabSwifts_txtReceiver742");
                }
                else if (Swift == 'Swift754') {
                    txtOverseasBankID = $get("TabContainerMain_TabSwifts_txtReceiver754");
                }
                else if (Swift == 'Swift799') {
                    txtOverseasBankID = $get("TabContainerMain_TabSwifts_txtReceiver799");
                }
                else if (Swift == 'Swift499') {
                    txtOverseasBankID = $get("TabContainerMain_TabSwifts_txtReceiver499");
                }
                else if (Swift == 'Swift199') {
                    txtOverseasBankID = $get("TabContainerMain_TabSwifts_txtReceiver199");
                }
                else if (Swift == 'Swift299') {
                    txtOverseasBankID = $get("TabContainerMain_TabSwifts_txtReceiver299");
                }
                else if (Swift == 'Swift999') {
                    txtOverseasBankID = $get("TabContainerMain_TabSwifts_txtReceiver999");
                }
                else if (Swift == 'Swift420') {
                    txtOverseasBankID = $get("TabContainerMain_TabSwifts_txtReceiver420");
                }
                open_popup('EXP_OverseasBankLookup.aspx?hNo=5&Swift="' + Swift + '"&bankID=' + txtOverseasBankID.value, 450, 650, 'OverseasBankList');
                return false;
            }
        }
        function selectSwiftOverseasBank(selectedID, Swift) {
            var SwiftCode;
            if (Swift == 'Swift742') {
                SwiftCode = $get("TabContainerMain_TabSwifts_txtReceiver742");
                SwiftCode.value = selectedID;
                $get("TabContainerMain_TabSwifts_txtReceiver742").onchange();
            }
            else if (Swift == 'Swift754') {
                SwiftCode = $get("TabContainerMain_TabSwifts_txtReceiver754");
                SwiftCode.value = selectedID;
                $get("TabContainerMain_TabSwifts_txtReceiver754").onchange();
            }
            else if (Swift == 'Swift799') {
                SwiftCode = $get("TabContainerMain_TabSwifts_txtReceiver799");
                SwiftCode.value = selectedID;
                $get("TabContainerMain_TabSwifts_txtReceiver799").onchange();
            }
            else if (Swift == 'Swift499') {
                SwiftCode = $get("TabContainerMain_TabSwifts_txtReceiver499");
                SwiftCode.value = selectedID;
                $get("TabContainerMain_TabSwifts_txtReceiver499").onchange();
            }
            else if (Swift == 'Swift199') {
                SwiftCode = $get("TabContainerMain_TabSwifts_txtReceiver199");
                SwiftCode.value = selectedID;
                $get("TabContainerMain_TabSwifts_txtReceiver199").onchange();
            }
            else if (Swift == 'Swift299') {
                SwiftCode = $get("TabContainerMain_TabSwifts_txtReceiver299");
                SwiftCode.value = selectedID;
                $get("TabContainerMain_TabSwifts_txtReceiver299").onchange();
            }
            else if (Swift == 'Swift999') {
                SwiftCode = $get("TabContainerMain_TabSwifts_txtReceiver999");
                SwiftCode.value = selectedID;
                $get("TabContainerMain_TabSwifts_txtReceiver999").onchange();
            }
            else if (Swift == 'Swift420') {
                SwiftCode = $get("TabContainerMain_TabSwifts_txtReceiver420");
                SwiftCode.value = selectedID;
                $get("TabContainerMain_TabSwifts_txtReceiver420").onchange();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <uc1:Menu ID="Menu1" runat="server" />
        <br />
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" valign="bottom">
                            <span class="pageLabel"><strong>Swift Message </strong></span>
                        </td>
                        <td align="right" style="width: 50%">
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                TabIndex="34" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left">
                            <span class="elementLabel" style="font-size: small">Branch :</span>
                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true" Width="100px"
                                runat="server" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <div>
                        <tr align="left">
                            <td align="left">
                                <span class="elementLabel" style="font-size: medium">Swift Types :</span>
                                <asp:DropDownList ID="ddlSwiftTypes" Height="22px" Width="100px" CssClass="dropdownList"
                                    AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlSwiftTypes_SelectedIndexChanged">
                                    <asp:ListItem Text="----Select----" Value="" />
                                    <asp:ListItem Text="MT 742" Value="8" />
                                    <asp:ListItem Text="MT 754" Value="11" />
                                    <asp:ListItem Text="MT 799" Value="9" />
                                    <asp:ListItem Text="MT 420" Value="5" />
                                    <asp:ListItem Text="MT 499" Value="6" />
                                    <asp:ListItem Text="MT 199" Value="3" />
                                    <asp:ListItem Text="MT 299" Value="2" />
                                    <asp:ListItem Text="MT 999" Value="10" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%;" valign="top">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                                <input type="hidden" id="hdnUserName" runat="server" />
                                <br />
                            </td>
                        </tr>
                    </div>
                    <table>
                        <tr>
                            <td align="left" style="width: 0%; border: 1px solid #49A3FF">
                                <ajaxToolkit:TabContainer ID="TabContainerMain" Visible="false" runat="server" ActiveTabIndex="0"
                                    CssClass="ajax__tab_xp-theme">
                                    <ajaxToolkit:TabPanel ID="TabSwifts" runat="server" Visible="false" HeaderText=""
                                        Font-Bold="true" ForeColor="White">
                                        <ContentTemplate>
                                            <table>
                                                <asp:Panel ID="Panel_103" HorizontalAlign="Left" runat="server" Visible="false">
                                                    <tr>
                                                        <td align="left">
                                                            <span style="font-size: large">MT : 103 - Normal </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[20] Sender's Reference : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_103_Sendr_Ref" runat="server" CssClass="textBox" MaxLength="16"
                                                                TabIndex="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[13C]Time Indication : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_103_TimeIndictn" runat="server" CssClass="textBox" TabIndex="2"
                                                                Width="130px" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">[23B] Bank Operation Code : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_103_BankOprtnCode" runat="server" CssClass="textBox" Width="50px"
                                                                MaxLength="4" TabIndex="2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[23E] Instruction Code : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtInstructionCode" runat="server" CssClass="textBox" TabIndex="2"
                                                                Width="40px" MaxLength="4"></asp:TextBox>
                                                            <asp:TextBox ID="txtInstructionAddInfo" runat="server" CssClass="textBox" TabIndex="2"
                                                                Width="250px" MaxLength="30"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">[26T] Transaction Type Code : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtTransactionTypeCode" runat="server" CssClass="textBox" TabIndex="2"
                                                                Width="30px" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[32A] Value Date/Currency/Interbank Settled Amount :
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_103_ValueDate" runat="server" CssClass="textBox" Width="70px"
                                                                MaxLength="10" TabIndex="3"></asp:TextBox>
                                                            <asp:TextBox ID="txt_103_32A_CCy" runat="server" CssClass="textBox" Width="35px"
                                                                MaxLength="3" TabIndex="4"></asp:TextBox>
                                                            <asp:TextBox ID="txt_103_IntrBankAttldAmt" runat="server" CssClass="textBox" MaxLength="15"
                                                                TabIndex="5"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[33B] Currency/Instructed Amount : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtCurrency" runat="server" CssClass="textBox" TabIndex="6" Width="30px"
                                                                MaxLength="3"></asp:TextBox>
                                                            <asp:TextBox ID="txtInstructedAmount" runat="server" CssClass="textBox" TabIndex="6"
                                                                Width="150px" MaxLength="15"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">[36] Exchange Rate : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtExchangeRate" runat="server" CssClass="textBox" TabIndex="6"
                                                                Width="120px" MaxLength="12"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[50K] Ordering Customer : </span>
                                                            <asp:DropDownList ID="ddlOrderingcustomer" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlOrderingcustomer_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="50A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="50F" Value="F"></asp:ListItem>
                                                                <asp:ListItem Text="50K" Value="K"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">A / C :</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtOrderingcustomerAccountNumber" runat="server" CssClass="textBox"
                                                                TabIndex="6" Width="300px" MaxLength="35"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblOrderingcustomerSwiftcode" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="TxtOrderingcustomerSwiftcode" CssClass="textBox"
                                                                MaxLength="11" TabIndex="6" Width="100px"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="TxtOrderingcustomerName" CssClass="textBox" Visible="false"
                                                                TabIndex="6" MaxLength="35" Width="250px"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="TxtOrderingcustomerNameK" CssClass="textBox" Visible="false"
                                                                TabIndex="6" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblOrderingcustomerAddress1" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtOrderingcustomerAddress1" CssClass="textBox" TabIndex="6"
                                                                Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblOrderingcustomerAddress2" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtOrderingcustomerAddress2" CssClass="textBox" TabIndex="6"
                                                                Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblOrderingcustomerAddress3" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtOrderingcustomerAddress3" CssClass="textBox" TabIndex="6"
                                                                Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[51A] Sending Institution : </span>
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:TextBox ID="txtSendingInstitutionAccountNumber" runat="server" CssClass="textBox"
                                                                TabIndex="7" Width="20px" MaxLength="1"></asp:TextBox>
                                                            <asp:TextBox ID="txtSendingInstitutionAccountNumber1" runat="server" CssClass="textBox"
                                                                TabIndex="7" Width="300px" MaxLength="34"></asp:TextBox>
                                                            <asp:TextBox ID="txtSendingInstitutionSwiftCode" runat="server" CssClass="textBox"
                                                                TabIndex="7" Width="300px" MaxLength="35"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[52A]Ordering Institution:</span>
                                                            <asp:DropDownList ID="ddlOrderingInstitution" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlOrderingInstitution_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="52A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="52D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">PartyIdentifier:</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtOrderingInstitutionAccountNumber" runat="server" CssClass="textBox"
                                                                TabIndex="7" Width="20px" MaxLength="1"></asp:TextBox>
                                                            <asp:TextBox ID="txtOrderingInstitutionAccountNumber1" runat="server" CssClass="textBox"
                                                                TabIndex="7" Width="250px" MaxLength="34"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">[53A]Sender Correspondent:</span>
                                                            <asp:DropDownList ID="ddlSendersCorrespondent" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlSendersCorrespondent_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="53A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="53B" Value="B"></asp:ListItem>
                                                                <asp:ListItem Text="53D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">PartyIdentifier:</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtSendersCorrespondentAccountNumber" runat="server" CssClass="textBox"
                                                                TabIndex="8" Width="20px" MaxLength="1"></asp:TextBox>
                                                            <asp:TextBox ID="txtSendersCorrespondentAccountNumber1" runat="server" CssClass="textBox"
                                                                TabIndex="8" Width="250px" MaxLength="34"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblOrderingInstitutionSwiftCode" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtOrderingInstitutionSwiftCode" CssClass="textBox"
                                                                MaxLength="11" TabIndex="7" Width="100px"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtOrderingInstitutionName" CssClass="textBox" Visible="false"
                                                                TabIndex="7" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSendersCorrespondentSwiftCode" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtSendersCorrespondentSwiftCode" CssClass="textBox"
                                                                TabIndex="8" MaxLength="11" Width="100px"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtSendersCorrespondentName" CssClass="textBox" TabIndex="8"
                                                                Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtSendersCorrespondentLocation" CssClass="textBox"
                                                                TabIndex="8" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblOrderingInstitutionAddress1" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtOrderingInstitutionAddress1" CssClass="textBox"
                                                                TabIndex="7" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSendersCorrespondentAddress1" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtSendersCorrespondentAddress1" CssClass="textBox"
                                                                TabIndex="8" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblOrderingInstitutionAddress2" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtOrderingInstitutionAddress2" CssClass="textBox"
                                                                TabIndex="7" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSendersCorrespondentAddress2" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtSendersCorrespondentAddress2" CssClass="textBox"
                                                                TabIndex="8" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblOrderingInstitutionAddress3" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtOrderingInstitutionAddress3" CssClass="textBox"
                                                                TabIndex="7" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSendersCorrespondentAddress3" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtSendersCorrespondentAddress3" CssClass="textBox"
                                                                TabIndex="8" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[54A]Receiver Correspondent:</span>
                                                            <asp:DropDownList ID="ddlReceiversCorrespondent" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlReceiversCorrespondent_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="54A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="54B" Value="B"></asp:ListItem>
                                                                <asp:ListItem Text="54D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">PartyIdentifier:</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtReceiversCorrespondentAccountNumber" runat="server" CssClass="textBox"
                                                                TabIndex="9" Width="20px" MaxLength="1"></asp:TextBox>
                                                            <asp:TextBox ID="txtReceiversCorrespondentAccountNumber1" runat="server" CssClass="textBox"
                                                                TabIndex="9" Width="250px" MaxLength="34"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">[55A]Third ReimbursementInstitution:</span>
                                                            <asp:DropDownList ID="ddlThirdReimbursementInstitution" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlThirdReimbursementInstitution_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="55A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="55B" Value="B"></asp:ListItem>
                                                                <asp:ListItem Text="55D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">PartyIdentifier:</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtThirdReimbursementInstitutionAccountNumber" runat="server" CssClass="textBox"
                                                                TabIndex="10" Width="20px" MaxLength="1"></asp:TextBox>
                                                            <asp:TextBox ID="txtThirdReimbursementInstitutionAccountNumber1" runat="server" CssClass="textBox"
                                                                TabIndex="10" Width="250px" MaxLength="34"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblReceiversCorrespondentSwiftCode" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtReceiversCorrespondentSwiftCode" CssClass="textBox"
                                                                MaxLength="11" Width="100px" TabIndex="9"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtReceiversCorrespondentName" CssClass="textBox"
                                                                Visible="false" MaxLength="35" Width="250px" TabIndex="9"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtReceiversCorrespondentLocation" CssClass="textBox"
                                                                Visible="false" MaxLength="35" Width="250px" TabIndex="9"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblThirdReimbursementInstitutionSwiftCode" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtThirdReimbursementInstitutionSwiftCode" CssClass="textBox"
                                                                MaxLength="11" Width="100px" TabIndex="10"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtThirdReimbursementInstitutionName" CssClass="textBox"
                                                                Visible="false" MaxLength="35" Width="250px" TabIndex="10"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtThirdReimbursementInstitutionLocation" CssClass="textBox"
                                                                Visible="false" MaxLength="35" Width="250px" TabIndex="10"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblReceiversCorrespondentAddress1" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtReceiversCorrespondentAddress1" CssClass="textBox"
                                                                Visible="false" TabIndex="9" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblThirdReimbursementInstitutionAddress1" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtThirdReimbursementInstitutionAddress1" CssClass="textBox"
                                                                Visible="false" TabIndex="10" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblReceiversCorrespondentAddress2" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtReceiversCorrespondentAddress2" CssClass="textBox"
                                                                Visible="false" TabIndex="9" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblThirdReimbursementInstitutionAddress2" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtThirdReimbursementInstitutionAddress2" CssClass="textBox"
                                                                TabIndex="10" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblReceiversCorrespondentAddress3" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtReceiversCorrespondentAddress3" CssClass="textBox"
                                                                Visible="false" TabIndex="9" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblThirdReimbursementInstitutionAddress3" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtThirdReimbursementInstitutionAddress3" CssClass="textBox"
                                                                Visible="false" TabIndex="10" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[56A]Intermediary Institution:</span>
                                                            <asp:DropDownList ID="ddlintermediaryinstitution" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlintermediaryinstitution_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="56A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="56C" Value="C"></asp:ListItem>
                                                                <asp:ListItem Text="56D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">PartyIdentifier:</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtintermediaryinstitutionidentifiercode" runat="server" CssClass="textBox"
                                                                TabIndex="11" Width="20px" MaxLength="1"></asp:TextBox>
                                                            <asp:TextBox ID="txtintermediaryinstitutionaccountnumber" runat="server" CssClass="textBox"
                                                                TabIndex="11" Width="250px" MaxLength="35"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">[57A]Account WithInstitution:</span>
                                                            <asp:DropDownList ID="ddlAccountwithinstitution" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlAccountwithinstitution_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="57A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="57B" Value="B"></asp:ListItem>
                                                                <asp:ListItem Text="57C" Value="C"></asp:ListItem>
                                                                <asp:ListItem Text="57D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">PartyIdentifier:</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtAccountwithinstitutionidentifiercode" runat="server" CssClass="textBox"
                                                                TabIndex="12" Width="18px" MaxLength="1"></asp:TextBox>
                                                            <asp:TextBox ID="txtAccountwithinstitutionAccountnumber" runat="server" CssClass="textBox"
                                                                TabIndex="12" Width="250px" MaxLength="35"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblintermediaryinstitutionidenfier" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtintermediaryinstitutioncode" CssClass="textBox"
                                                                MaxLength="11" Width="100px" TabIndex="11"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtintermediaryinstitutionname" CssClass="textBox"
                                                                Visible="false" MaxLength="35" Width="250px" TabIndex="11"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithinstitutionidenfier" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithinstitutioncode" CssClass="textBox"
                                                                MaxLength="11" TabIndex="12" Width="100px"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtAccountwithinstitutionlocation" CssClass="textBox"
                                                                Visible="false" MaxLength="35" Width="250px" TabIndex="12"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtAccountwithinstitutionName" CssClass="textBox"
                                                                Visible="false" MaxLength="35" Width="250px" TabIndex="12"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblintermediaryinstitutionAddress1" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtintermediaryinstitutionAddress1" CssClass="textBox"
                                                                Visible="false" TabIndex="11" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithinstitutionAddress1" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithinstitutionAddress1" CssClass="textBox"
                                                                Visible="false" TabIndex="12" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblintermediaryinstitutionAddress2" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtintermediaryinstitutionAddress2" CssClass="textBox"
                                                                Visible="false" TabIndex="11" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithinstitutionAddress2" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithinstitutionAddress2" CssClass="textBox"
                                                                Visible="false" TabIndex="12" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblintermediaryinstitutionAddress3" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtintermediaryinstitutionAddress3" CssClass="textBox"
                                                                Visible="false" TabIndex="11" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithinstitutionAddress3" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithinstitutionAddress3" CssClass="textBox"
                                                                Visible="false" TabIndex="12" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[70] Remittance Information : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_103_Remittanceinfo1" runat="server" CssClass="textBox" Width="370px"
                                                                MaxLength="35" TabIndex="13"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">[59A] Beneficiary Customer A/C : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtBeneficiaryCustomerAccountNumber" runat="server" CssClass="textBox"
                                                                Width="200px" TabIndex="14" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel"></span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_103_Remittanceinfo2" runat="server" CssClass="textBox" Width="370px"
                                                                MaxLength="35" TabIndex="13"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">Address 1 : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryCustomerAddress1" CssClass="textBox"
                                                                MaxLength="35" TabIndex="14" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel"></span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_103_Remittanceinfo3" runat="server" CssClass="textBox" Width="370px"
                                                                MaxLength="35" TabIndex="13"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">Address 2 : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryCustomerAddress2" CssClass="textBox"
                                                                MaxLength="35" Width="250px" TabIndex="14"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel"></span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_103_Remittanceinfo4" runat="server" CssClass="textBox" Width="370px"
                                                                MaxLength="35" TabIndex="13"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">Address 3 : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryCustomerAddress3" CssClass="textBox"
                                                                MaxLength="35" Width="250px" TabIndex="14"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[71A] Details Of Charges : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtDetailsOfCharges" runat="server" CssClass="textBox" TabIndex="15"
                                                                Width="30px" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[71F] Sender's Charges : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtSenderCharges" runat="server" CssClass="textBox" TabIndex="16"
                                                                Width="30px" MaxLength="3"></asp:TextBox>
                                                            <asp:TextBox ID="txtSenderCharges2" runat="server" CssClass="textBox" TabIndex="16"
                                                                Width="150px" MaxLength="15"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">[71G] Receiver's Charges : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtReceiverCharges" runat="server" CssClass="textBox" TabIndex="17"
                                                                Width="30px" MaxLength="3"></asp:TextBox>
                                                            <asp:TextBox ID="txtReceiverCharges2" runat="server" CssClass="textBox" TabIndex="17"
                                                                Width="150px" MaxLength="15"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[72] Sender to Receiver Information : </span>
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:TextBox ID="txtSendertoReceiverInformation" runat="server" CssClass="textBox"
                                                                TabIndex="18" Width="300px" MaxLength="35"></asp:TextBox>
                                                            <asp:TextBox ID="txtSendertoReceiverInformation2" runat="server" CssClass="textBox"
                                                                TabIndex="18" Width="300px" MaxLength="35"></asp:TextBox>
                                                            <asp:TextBox ID="txtSendertoReceiverInformation3" runat="server" CssClass="textBox"
                                                                TabIndex="18" Width="300px" MaxLength="35"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:TextBox ID="txtSendertoReceiverInformation4" runat="server" CssClass="textBox"
                                                                TabIndex="18" Width="300px" MaxLength="35"></asp:TextBox>
                                                            <asp:TextBox ID="txtSendertoReceiverInformation5" runat="server" CssClass="textBox"
                                                                TabIndex="18" Width="300px" MaxLength="35"></asp:TextBox>
                                                            <asp:TextBox ID="txtSendertoReceiverInformation6" runat="server" CssClass="textBox"
                                                                TabIndex="18" Width="300px" MaxLength="35"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[77B] Regulatory Reporting : </span>
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:TextBox ID="txtRegulatoryReporting" runat="server" CssClass="textBox" TabIndex="19"
                                                                Width="300px" MaxLength="35"></asp:TextBox>
                                                            <asp:TextBox ID="txtRegulatoryReporting2" runat="server" CssClass="textBox" TabIndex="19"
                                                                Width="300px" MaxLength="35"></asp:TextBox>
                                                            <asp:TextBox ID="txtRegulatoryReporting3" runat="server" CssClass="textBox" TabIndex="19"
                                                                Width="300px" MaxLength="35"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                            <table>
                                                <asp:Panel ID="Panel_191" runat="server" Visible="false">
                                                    <tr>
                                                        <td align="left">
                                                            <span style="font-size: large">MT : 191 - Normal </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[20] Transaction Reference Number :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_191_transRefNo" runat="server" MaxLength="16" CssClass="textBox"
                                                                TabIndex="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[21] Related Reference : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_191_RelRef" runat="server" CssClass="textBox" MaxLength="16"
                                                                TabIndex="2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[32B] Currency Code And Amount : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_191_Curr" runat="server" CssClass="textBox" Width="35px" MaxLength="3"
                                                                TabIndex="3"></asp:TextBox>
                                                            <asp:TextBox ID="txt_191_Amt" runat="server" CssClass="textBox" Width="95px" MaxLength="15"
                                                                TabIndex="4"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[52A] Ordering Institution : </span>
                                                            <asp:DropDownList ID="ddlOrderingInstitution_191" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlOrderingInstitution_191_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="52A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="52D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">PartyIdentifier:</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtOrderingInstitutionAccountNumber_191" runat="server" CssClass="textBox"
                                                                Width="20px" MaxLength="1" TabIndex="5"></asp:TextBox>
                                                            <asp:TextBox ID="txtOrderingInstitutionAccountNumber1_191" runat="server" CssClass="textBox"
                                                                Width="260px" MaxLength="34" TabIndex="6"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblOrderingInstitutionSwiftCode_191" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtOrderingInstitutionSwiftCode_191" CssClass="textBox"
                                                                MaxLength="11" Width="100px" TabIndex="7"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtOrderingInstitutionName_191" CssClass="textBox"
                                                                Visible="false" MaxLength="35" Width="250px" TabIndex="7"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblOrderingInstitutionAddress1_191" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtOrderingInstitutionAddress1_191" CssClass="textBox"
                                                                Visible="false" MaxLength="35" TabIndex="8" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblOrderingInstitutionAddress2_191" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtOrderingInstitutionAddress2_191" CssClass="textBox"
                                                                Visible="false" MaxLength="35" TabIndex="9" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblOrderingInstitutionAddress3_191" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtOrderingInstitutionAddress3_191" CssClass="textBox"
                                                                Visible="false" TabIndex="10" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[57A]Account WithInstitution:</span>
                                                            <asp:DropDownList ID="ddlAccountwithinstitution_191" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlAccountwithinstitution_191_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="57A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="57B" Value="B"></asp:ListItem>
                                                                <asp:ListItem Text="57D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">PartyIdentifier:</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtAccountwithinstitutionidentifiercode_191" runat="server" CssClass="textBox"
                                                                Width="18px" MaxLength="1" TabIndex="11"></asp:TextBox>
                                                            <asp:TextBox ID="txtAccountwithinstitutionAccountnumber_191" runat="server" CssClass="textBox"
                                                                Width="260px" MaxLength="35" TabIndex="12"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithinstitutionidenfier_191" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithinstitutioncode_191" CssClass="textBox"
                                                                MaxLength="11" Width="100px" TabIndex="13"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtAccountwithinstitutionlocation_191" CssClass="textBox"
                                                                Visible="false" MaxLength="35" Width="250px" TabIndex="13"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtAccountwithinstitutionName_191" CssClass="textBox"
                                                                Visible="false" MaxLength="35" Width="250px" TabIndex="13"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithinstitutionAddress1_191" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithinstitutionAddress1_191" CssClass="textBox"
                                                                Visible="false" TabIndex="14" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithinstitutionAddress2_191" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithinstitutionAddress2_191" CssClass="textBox"
                                                                Visible="false" MaxLength="35" TabIndex="15" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithinstitutionAddress3_191" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithinstitutionAddress3_191" CssClass="textBox"
                                                                Visible="false" MaxLength="35" TabIndex="16" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[71B] Details Of Charges : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_191_DetailsOfChrgs1" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="330px" TabIndex="17"></asp:TextBox>
                                                            <asp:TextBox ID="txt_191_DetailsOfChrgs2" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="18"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_191_DetailsOfChrgs3" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="19"></asp:TextBox>
                                                            <asp:TextBox ID="txt_191_DetailsOfChrgs4" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="20"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_191_DetailsOfChrgs5" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="21"></asp:TextBox>
                                                            <asp:TextBox ID="txt_191_DetailsOfChrgs6" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="22"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[72] Sender to Receiver Information : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_191_SenToRecinfo1" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="23"></asp:TextBox>
                                                            <asp:TextBox ID="txt_191_SenToRecinfo2" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="24"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_191_SenToRecinfo3" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="25"></asp:TextBox>
                                                            <asp:TextBox ID="txt_191_SenToRecinfo4" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="26"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_191_SenToRecinfo5" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="27"></asp:TextBox>
                                                            <asp:TextBox ID="txt_191_SenToRecinfo6" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="28"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                            <table>
                                                <asp:Panel ID="Panel_199" runat="server" Visible="false">
                                                    <tr>
                                                        <td align="left">
                                                            <span style="font-size: large">MT : 199 - Free format message </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
													<tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;"> Receiver </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReceiver199" runat="server" MaxLength="11" CssClass="textBox"
                                                                TabIndex="1" AutoPostBack="true"
                                                                OnTextChanged="txtReceiver199_TextChanged"></asp:TextBox>
                                                                <asp:Button ID="btnReceiver199BankList" runat="server" CssClass="btnHelp_enabled"
                                                                 TabIndex="1" OnClientClick="return OpenOverseasBankList('mouseClick','Swift199');" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[20] Transaction Reference Number :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_transRefNo" Width="" runat="server" MaxLength="16" CssClass="textBox"
                                                                AutoPostBack="true" OnTextChanged="txt_199_transRefNo_TextChanged" TabIndex="1"></asp:TextBox>
                                                                <%--<asp:Button ID="btnDocNoHelp_199" runat="server" CssClass="btnHelp_enabled" TabIndex="1"
                                                                OnClientClick="return OpenLogded_DocNoList('mouseClick','Swift199');" />--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[21] Related Reference : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_RelRef" runat="server" CssClass="textBox" MaxLength="16"
                                                                AutoPostBack="true"
                                                                OnTextChanged="txt_199_RelRef_TextChanged" TabIndex="2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[79] Narrative : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr1" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="3" AutoPostBack="true" OnTextChanged="txt_199_Narr1_TextChanged" ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr2" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="4" AutoPostBack="true" OnTextChanged="txt_199_Narr2_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr3" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="5" AutoPostBack="true" OnTextChanged="txt_199_Narr3_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr4" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="6" AutoPostBack="true" OnTextChanged="txt_199_Narr4_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr5" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="7" AutoPostBack="true" OnTextChanged="txt_199_Narr5_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr6" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="8" AutoPostBack="true" OnTextChanged="txt_199_Narr6_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr7" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="9" AutoPostBack="true" OnTextChanged="txt_199_Narr7_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr8" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="10" AutoPostBack="true" OnTextChanged="txt_199_Narr8_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr9" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="11" AutoPostBack="true" OnTextChanged="txt_199_Narr9_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr10" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="12" AutoPostBack="true" OnTextChanged="txt_199_Narr10_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr11" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="13" AutoPostBack="true" OnTextChanged="txt_199_Narr11_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr12" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="14" AutoPostBack="true" OnTextChanged="txt_199_Narr12_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr13" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="15" AutoPostBack="true" OnTextChanged="txt_199_Narr13_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr14" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="16" AutoPostBack="true" OnTextChanged="txt_199_Narr14_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr15" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="17" AutoPostBack="true" OnTextChanged="txt_199_Narr15_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr16" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="18" AutoPostBack="true" OnTextChanged="txt_199_Narr16_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr17" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="19" AutoPostBack="true" OnTextChanged="txt_199_Narr17_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr18" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="20" AutoPostBack="true" OnTextChanged="txt_199_Narr18_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr19" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="21" AutoPostBack="true" OnTextChanged="txt_199_Narr19_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr20" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="22" AutoPostBack="true" OnTextChanged="txt_199_Narr20_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr21" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="23" AutoPostBack="true" OnTextChanged="txt_199_Narr21_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr22" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="24" AutoPostBack="true" OnTextChanged="txt_199_Narr22_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr23" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="25" AutoPostBack="true" OnTextChanged="txt_199_Narr23_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr24" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="26" AutoPostBack="true" OnTextChanged="txt_199_Narr24_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr25" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="27" AutoPostBack="true" OnTextChanged="txt_199_Narr25_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr26" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="28" AutoPostBack="true" OnTextChanged="txt_199_Narr26_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr27" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="29" AutoPostBack="true" OnTextChanged="txt_199_Narr27_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr28" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="30" AutoPostBack="true" OnTextChanged="txt_199_Narr28_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr29" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="31" AutoPostBack="true" OnTextChanged="txt_199_Narr29_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr30" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="32" AutoPostBack="true" OnTextChanged="txt_199_Narr30_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr31" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="33" AutoPostBack="true" OnTextChanged="txt_199_Narr31_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr32" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="34" AutoPostBack="true" OnTextChanged="txt_199_Narr32_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr33" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="35" AutoPostBack="true" OnTextChanged="txt_199_Narr33_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr34" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="36" AutoPostBack="true" OnTextChanged="txt_199_Narr34_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr35" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="37" AutoPostBack="true" OnTextChanged="txt_199_Narr35_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                            <table>
                                                <asp:Panel ID="Panel_202" HorizontalAlign="Left" runat="server" Visible="false">
                                                    <tr>
                                                        <td align="left">
                                                            <span style="font-size: large">MT : 202- Normal </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[20] Transaction Reference Number : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_202_Trans_RefNo" runat="server" CssClass="textBox" MaxLength="16"
                                                                TabIndex="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[21] Related Reference : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_202_Related_Ref" runat="server" CssClass="textBox" MaxLength="16"
                                                                TabIndex="2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[13C] Time Indication : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_202_TimeIndicatn" runat="server" CssClass="textBox" TabIndex="3"
                                                                Width="130px" MaxLength="17"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[32A] Value Date/Currency/Amount : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_202_ValueDate" runat="server" CssClass="textBox" Width="60px"
                                                                MaxLength="6" TabIndex="4"></asp:TextBox>
                                                            <asp:TextBox ID="txt_202_Currency" runat="server" CssClass="textBox" Width="35px"
                                                                MaxLength="3" TabIndex="5"></asp:TextBox>
                                                            <asp:TextBox ID="txt_202_Amt" runat="server" CssClass="textBox" MaxLength="15" TabIndex="6"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[52A] Ordering Institution:</span>
                                                            <asp:DropDownList ID="ddlOrderingInstitution_202" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlOrderingInstitution_202_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="52A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="52D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Party Identifier:</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtOrderingInstitutionAccountNumber_202" runat="server" CssClass="textBox"
                                                                TabIndex="7" Width="20px" MaxLength="1"></asp:TextBox>
                                                            <asp:TextBox ID="txtOrderingInstitutionAccountNumber1_202" runat="server" CssClass="textBox"
                                                                TabIndex="8" Width="260px" MaxLength="34"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">[53A]Sender Correspondent:</span>
                                                            <asp:DropDownList ID="ddlSendersCorrespondent_202" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlSendersCorrespondent_202_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="53A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="53B" Value="B"></asp:ListItem>
                                                                <asp:ListItem Text="53D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">PartyIdentifier:</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtSendersCorrespondentAccountNumber_202" runat="server" CssClass="textBox"
                                                                TabIndex="10" Width="20px" MaxLength="1"></asp:TextBox>
                                                            <asp:TextBox ID="txtSendersCorrespondentAccountNumber1_202" runat="server" CssClass="textBox"
                                                                TabIndex="11" Width="300px" MaxLength="34"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblOrderingInstitutionidentier_202" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtOrderingInstitutionidentifiercode_202" CssClass="textBox"
                                                                MaxLength="11" TabIndex="9" Width="100px"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtOrderingInstitutionName_202" CssClass="textBox"
                                                                Visible="false" MaxLength="35" TabIndex="9" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSendersCorrespondentIdentifier_202" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtSendersCorrespondentidentifiercode_202" CssClass="textBox"
                                                                MaxLength="11" Width="100px" TabIndex="12"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtSendersCorrespondentName_202" CssClass="textBox"
                                                                Visible="false" MaxLength="35" Width="250px" TabIndex="12"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtSendersCorrespondentLocation_202" CssClass="textBox"
                                                                Visible="false" MaxLength="35" Width="250px" TabIndex="12"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblOrderingInstitutionAddress1_202" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtOrderingInstitutionAddress1_202" CssClass="textBox"
                                                                Visible="false" TabIndex="9" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSendersCorrespondentAddress1_202" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtSendersCorrespondentAddress1_202" CssClass="textBox"
                                                                Visible="false" TabIndex="12" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblOrderingInstitutionAddress2_202" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtOrderingInstitutionAddress2_202" CssClass="textBox"
                                                                Visible="false" TabIndex="9" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSendersCorrespondentAddress2_202" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtSendersCorrespondentAddress2_202" CssClass="textBox"
                                                                Visible="false" TabIndex="12" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblOrderingInstitutionAddress3_202" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtOrderingInstitutionAddress3_202" CssClass="textBox"
                                                                Visible="false" TabIndex="9" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblSendersCorrespondentAddress3_202" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtSendersCorrespondentAddress3_202" CssClass="textBox"
                                                                Visible="false" TabIndex="12" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[54A] Receiver Correspondent : </span>
                                                            <asp:DropDownList ID="ddlReceiversCorrespondent_202" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlReceiversCorrespondent_202_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="54A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="54B" Value="B"></asp:ListItem>
                                                                <asp:ListItem Text="54D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">PartyIdentifier:</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtReceiversCorrespondentAccountNumber_202" runat="server" CssClass="textBox"
                                                                TabIndex="13" Width="20px" MaxLength="1"></asp:TextBox>
                                                            <asp:TextBox ID="txtReceiversCorrespondentAccountNumber1_202" runat="server" CssClass="textBox"
                                                                TabIndex="13" Width="250px" MaxLength="34"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">[56A] Intermediary :</span>
                                                            <asp:DropDownList ID="ddlIntermediary_202" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlIntermediary_202_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="56A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="56D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Party Identifier:</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtIntermediaryAccountno_202" runat="server" CssClass="textBox"
                                                                TabIndex="14" Width="20px" MaxLength="1"></asp:TextBox>
                                                            <asp:TextBox ID="txtIntermediaryAccountno1_202" runat="server" CssClass="textBox"
                                                                TabIndex="14" Width="250px" MaxLength="34"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblReceiversCorrespondentIdentier_202" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtReceiversCorrespondentIdentifiercode_202" CssClass="textBox"
                                                                MaxLength="11" TabIndex="13" Width="100px"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtReceiversCorrespondentLocation_202" CssClass="textBox"
                                                                Visible="false" MaxLength="35" TabIndex="13" Width="250px"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtReceiversCorrespondentName_202" CssClass="textBox"
                                                                Visible="false" MaxLength="35" TabIndex="13" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblIntermediaryIdentifier" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtIntermediaryIdentifiercode_202" CssClass="textBox"
                                                                MaxLength="11" TabIndex="14" Width="100px"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtIntermediaryname_202" CssClass="textBox" Visible="false"
                                                                MaxLength="35" TabIndex="14" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblReceiversCorrespondentAddress1_202" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtReceiversCorrespondentAddress1_202" CssClass="textBox"
                                                                Visible="false" TabIndex="13" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblIntermediaryAddress1_202" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtIntermediaryAddress1_202" CssClass="textBox" Visible="false"
                                                                MaxLength="35" TabIndex="14" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblReceiversCorrespondentAddress2_202" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtReceiversCorrespondentAddress2_202" CssClass="textBox"
                                                                Visible="false" TabIndex="13" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblIntermediaryAddress2_202" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtIntermediaryAddress2_202" CssClass="textBox" Visible="false"
                                                                TabIndex="14" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblReceiversCorrespondentAddress3_202" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtReceiversCorrespondentAddress3_202" CssClass="textBox"
                                                                Visible="false" TabIndex="13" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblIntermediaryAddress3_202" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtIntermediaryAddress3_202" CssClass="textBox" Visible="false"
                                                                TabIndex="14" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[57A] Account With Institution:</span>
                                                            <asp:DropDownList ID="ddlAccountwithinstitution_202" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlAccountwithinstitution_202_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="57A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="57B" Value="B"></asp:ListItem>
                                                                <asp:ListItem Text="57D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Party Identifier:</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtAccountwithinstitutionAccountno_202" runat="server" CssClass="textBox"
                                                                TabIndex="15" Width="18px" MaxLength="1"></asp:TextBox>
                                                            <asp:TextBox ID="txtAccountwithinstitutionAccountno1_202" runat="server" CssClass="textBox"
                                                                TabIndex="15" Width="250px" MaxLength="35"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">[58A] Beneficiary Institution : </span>
                                                            <asp:DropDownList ID="ddlBeneficiaryInstitution_202" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlBeneficiaryInstitution_202_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="58A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="58D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Party Identifier:</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtBeneficiaryInstitutionAccountNumber_202" runat="server" CssClass="textBox"
                                                                TabIndex="16" Width="20px" MaxLength="1"></asp:TextBox>
                                                            <asp:TextBox ID="txtBeneficiaryInstitutionAccountNumber1_202" runat="server" CssClass="textBox"
                                                                TabIndex="16" Width="260px" MaxLength="34"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithinstitutionidenfier_202" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithinstitutionIdentifiercode_202" CssClass="textBox"
                                                                MaxLength="11" TabIndex="15" Width="100px"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtAccountwithinstitutionlocation_202" CssClass="textBox"
                                                                Visible="false" MaxLength="35" TabIndex="15" Width="250px"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtAccountwithinstitutionName_202" CssClass="textBox"
                                                                Visible="false" MaxLength="35" TabIndex="15" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblBeneficiaryInstitutionIdentifier_202" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryInstitutionidentifiercode_202" CssClass="textBox"
                                                                MaxLength="11" TabIndex="16" Width="100px"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryInstitutionName_202" CssClass="textBox"
                                                                Visible="false" MaxLength="35" TabIndex="16" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithinstitutionAddress1_202" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithinstitutionAddress1_202" CssClass="textBox"
                                                                Visible="false" TabIndex="15" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblBeneficiaryInstitutionAddress1_202" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryInstitutionAddress1_202" CssClass="textBox"
                                                                Visible="false" TabIndex="16" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithinstitutionAddress2_202" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithinstitutionAddress2_202" CssClass="textBox"
                                                                Visible="false" TabIndex="15" MaxLength="35" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblBeneficiaryInstitutionAddress2_202" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryInstitutionAddress2_202" CssClass="textBox"
                                                                Visible="false" MaxLength="35" TabIndex="16" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithinstitutionAddress3_202" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithinstitutionAddress3_202" CssClass="textBox"
                                                                Visible="false" MaxLength="35" TabIndex="15" Width="250px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblBeneficiaryInstitutionAddress3_202" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryInstitutionAddress3_202" CssClass="textBox"
                                                                Visible="false" MaxLength="35" TabIndex="16" Width="250px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[72] Sender to Receiver Information : </span>
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:TextBox ID="txt_202_SenRecInfo1" runat="server" CssClass="textBox" TabIndex="17"
                                                                Width="300px" MaxLength="35"></asp:TextBox>
                                                            <asp:TextBox ID="txt_202_SenRecInfo2" runat="server" CssClass="textBox" TabIndex="17"
                                                                Width="300px" MaxLength="35"></asp:TextBox>
                                                            <asp:TextBox ID="txt_202_SenRecInfo3" runat="server" CssClass="textBox" TabIndex="17"
                                                                Width="300px" MaxLength="35"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:TextBox ID="txt_202_SenRecInfo4" runat="server" CssClass="textBox" TabIndex="17"
                                                                Width="300px" MaxLength="35"></asp:TextBox>
                                                            <asp:TextBox ID="txt_202_SenRecInfo5" runat="server" CssClass="textBox" TabIndex="17"
                                                                Width="300px" MaxLength="35"></asp:TextBox>
                                                            <asp:TextBox ID="txt_202_SenRecInfo6" runat="server" CssClass="textBox" TabIndex="17"
                                                                Width="300px" MaxLength="35"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                            <table>
                                               <asp:Panel ID="Panel_299" runat="server" Visible="false">
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <span style="font-size: large">MT : 299 - Free Format Message </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;"> Receiver </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReceiver299" runat="server" MaxLength="11" CssClass="textBox"
                                                                TabIndex="1" AutoPostBack="true"
                                                                OnTextChanged="txtReceiver299_TextChanged"></asp:TextBox>
                                                                <asp:Button ID="btnReceiver299BankList" runat="server" CssClass="btnHelp_enabled"
                                                                 TabIndex="1" OnClientClick="return OpenOverseasBankList('mouseClick','Swift299');" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[20] Transaction Reference Number :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_transRefNo" Width="" runat="server" MaxLength="16" CssClass="textBox"
                                                                AutoPostBack="true" OnTextChanged="txt_299_transRefNo_TextChanged" TabIndex="1"></asp:TextBox>
                                                                <%--<asp:Button ID="btnDocNoHelp_299" runat="server" CssClass="btnHelp_enabled" TabIndex="1"
                                                                OnClientClick="return OpenLogded_DocNoList('mouseClick','Swift299');" />--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[21] Related Reference : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_RelRef" runat="server" CssClass="textBox" MaxLength="16"
                                                                AutoPostBack="true"
                                                                OnTextChanged="txt_299_RelRef_TextChanged" TabIndex="2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[79] Narrative : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr1" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="3" AutoPostBack="true" OnTextChanged="txt_299_Narr1_TextChanged" ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr2" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="4" AutoPostBack="true" OnTextChanged="txt_299_Narr2_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr3" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="5" AutoPostBack="true" OnTextChanged="txt_299_Narr3_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr4" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="6" AutoPostBack="true" OnTextChanged="txt_299_Narr4_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr5" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="7" AutoPostBack="true" OnTextChanged="txt_299_Narr5_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr6" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="8" AutoPostBack="true" OnTextChanged="txt_299_Narr6_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr7" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="9" AutoPostBack="true" OnTextChanged="txt_299_Narr7_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr8" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="10" AutoPostBack="true" OnTextChanged="txt_299_Narr8_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr9" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="11" AutoPostBack="true" OnTextChanged="txt_299_Narr9_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr10" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="12" AutoPostBack="true" OnTextChanged="txt_299_Narr10_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr11" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="13" AutoPostBack="true" OnTextChanged="txt_299_Narr11_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr12" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="14" AutoPostBack="true" OnTextChanged="txt_299_Narr12_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr13" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="15" AutoPostBack="true" OnTextChanged="txt_299_Narr13_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr14" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="16" AutoPostBack="true" OnTextChanged="txt_299_Narr14_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr15" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="17" AutoPostBack="true" OnTextChanged="txt_299_Narr15_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr16" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="18" AutoPostBack="true" OnTextChanged="txt_299_Narr16_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr17" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="19" AutoPostBack="true" OnTextChanged="txt_299_Narr17_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr18" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="20" AutoPostBack="true" OnTextChanged="txt_299_Narr18_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr19" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="21" AutoPostBack="true" OnTextChanged="txt_299_Narr19_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr20" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="22" AutoPostBack="true" OnTextChanged="txt_299_Narr20_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr21" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="23" AutoPostBack="true" OnTextChanged="txt_299_Narr21_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr22" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="24" AutoPostBack="true" OnTextChanged="txt_299_Narr22_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr23" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="25" AutoPostBack="true" OnTextChanged="txt_299_Narr23_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr24" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="26" AutoPostBack="true" OnTextChanged="txt_299_Narr24_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr25" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="27" AutoPostBack="true" OnTextChanged="txt_299_Narr25_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr26" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="28" AutoPostBack="true" OnTextChanged="txt_299_Narr26_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr27" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="29" AutoPostBack="true" OnTextChanged="txt_299_Narr27_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr28" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="30" AutoPostBack="true" OnTextChanged="txt_299_Narr28_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr29" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="31" AutoPostBack="true" OnTextChanged="txt_299_Narr29_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr30" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="32" AutoPostBack="true" OnTextChanged="txt_299_Narr30_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr31" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="33" AutoPostBack="true" OnTextChanged="txt_299_Narr31_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr32" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="34" AutoPostBack="true" OnTextChanged="txt_299_Narr32_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr33" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="35" AutoPostBack="true" OnTextChanged="txt_299_Narr33_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr34" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="36" AutoPostBack="true" OnTextChanged="txt_299_Narr34_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr35" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="37" AutoPostBack="true" OnTextChanged="txt_299_Narr35_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                            <table>
                                                <asp:Panel ID="Panel_420" runat="server" Visible="false">
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <span style="font-size: large">MT : 420 - Tracer (Enquires about documents sent for collection) </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;"> Receiver </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReceiver420" runat="server" MaxLength="11" CssClass="textBox"
                                                                AutoPostBack="true" OnTextChanged="txtReceiver420_TextChanged" TabIndex="1"></asp:TextBox>
                                                                <asp:Button ID="btnReceiver420BankList" runat="server" CssClass="btnHelp_enabled" TabIndex="1"
                                                                 OnClientClick="return OpenOverseasBankList('mouseClick','Swift420');" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[20] Sending Bank's
                                                                TRN :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_420_SendingBankTRN" runat="server" MaxLength="16" CssClass="textBox"
                                                                AutoPostBack="true" OnTextChanged="txt_420_SendingBankTRN_TextChanged"  TabIndex="1"></asp:TextBox>
                                                            <%--<asp:Button ID="btnDocNoHelp_420" runat="server" CssClass="btnHelp_enabled" TabIndex="1"
                                                                OnClientClick="return OpenLogded_DocNoList('mouseClick','Swift420');" />--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[21] Related Reference
                                                                : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_420_RelRef" runat="server" CssClass="textBox" MaxLength="16"
                                                                AutoPostBack="true" OnTextChanged="txt_420_RelRef_TextChanged" TabIndex="2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[32A] Amount Traced
                                                                : </span>
                                                            <asp:DropDownList ID="ddlAmountTraced_420" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlAmountTraced_420_TextChanged" AutoPostBack="true" TabIndex="3">
                                                                <asp:ListItem Text="32A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="32B" Value="B"></asp:ListItem>
                                                                <asp:ListItem Text="32K" Value="K"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblAmountTracedDayMonth_420" Text="(Day/Month) :" runat="server" class="elementLabel"
                                                                Visible="false"></asp:Label>
                                                                <%--<asp:TextBox ID="txtAmountTracedDayMonth_420" runat="server" CssClass="textBox" TabIndex="5"
                                                                Width="18px" Visible="false" MaxLength="1" AutoPostBack="true" 
                                                                OnTextChanged="txtAmountTracedDayMonth_420_TextChanged"></asp:TextBox>
                                                                <span class="elementLabel">(Day/Month) :</span>--%>
                                                                <asp:DropDownList ID="ddlAmountTracedDayMonth_420" Height="22px" Width="35px" 
                                                                   CssClass="dropdownList" AutoPostBack="true" runat="server" Visible="false" TabIndex="3">
                                                                    <asp:ListItem Text="-Select-" Value="" />
                                                                    <asp:ListItem Text="D" Value="D" />
                                                                    <asp:ListItem Text="M" Value="M" /> 
                                                                </asp:DropDownList>
                                                            
                                                            <asp:Label ID="lblAmountTracedNoofDaysMonth_420" Text="(No of Days/Months) :" runat="server"
                                                                class="elementLabel" Visible="false"></asp:Label>
                                                            <asp:TextBox ID="txtAmountTracedNoofDaysMonth_420" runat="server" CssClass="textBox"
                                                                TabIndex="3" Width="40px" Visible="false" MaxLength="3" AutoPostBack="true" 
                                                                OnTextChanged="txtAmountTracedNoofDaysMonth_420_TextChanged"></asp:TextBox>

                                                            <asp:Label ID="lblAmountTracedDate_420" Text="Date :" runat="server" class="elementLabel"></asp:Label>
                                                            <asp:TextBox ID="txtAmountTracedDate_420" runat="server" CssClass="textBox" TabIndex="3"
                                                                Width="70px"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="AmountTracedDate420" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txtAmountTracedDate_420" ErrorTooltipEnabled="True"
                                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="btnCal420_Date" runat="server" CssClass="btncalendar_enabled" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtAmountTracedDate_420" PopupButtonID="btnCal420_Date" Enabled="True">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="AmountTracedDate420"
                                                                ValidationGroup="dtVal" ControlToValidate="txtAmountTracedDate_420" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                                Enabled="False"></ajaxToolkit:MaskedEditValidator>

                                                            <asp:Label ID="lblAmountTracedCode_420" Text="Code :" runat="server" class="elementLabel"
                                                                Visible="false"></asp:Label>
                                                            <%--<asp:TextBox ID="txtAmountTracedCode_420" runat="server" CssClass="textBox" TabIndex="5"
                                                                Width="35px" Visible="false" MaxLength="2"
                                                                AutoPostBack="true" OnTextChanged="txtAmountTracedCode_420_TextChanged"></asp:TextBox>--%>
                                                                <%--<span class="elementLabel">Code : </span>--%>
                                                                <asp:DropDownList ID="ddlAmountTracedCode_420" Height="22px" Width="300px" 
                                                                   CssClass="dropdownList" AutoPostBack="true" runat="server" Visible="false" TabIndex="3">
                                                                    <asp:ListItem Text="--Select--" Value="" />
                                                                    <asp:ListItem Text="BE-After the date of the bill of exchange" Value="BE" />
                                                                    <asp:ListItem Text="CC-After customs clearance of goods" Value="CC" /> 
                                                                    <asp:ListItem Text="FD-After goods pass food and drug administration" Value="FD" />
                                                                    <asp:ListItem Text="FP-First presentation" Value="FP" />
                                                                    <asp:ListItem Text="GA-After arrival of goods" Value="GA" />
                                                                    <asp:ListItem Text="ID-After invoice date" Value="ID" />
                                                                    <asp:ListItem Text="ST-After sight" Value="ST" />
                                                                    <asp:ListItem Text="TD-After date of transport documents" Value="TD" />
                                                                    <asp:ListItem Text="XX-See field 72 for specification" Value="XX" />
                                                                </asp:DropDownList>

                                                            <span class="elementLabel">Currency : </span>
                                                            <asp:DropDownList ID="ddl_AmountTracedCurrency_420" runat="server" CssClass="dropdownList"
                                                                TabIndex="3" Width="70px" OnSelectedIndexChanged="ddl_AmountTracedCurrency_420_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Amount : </span>
                                                            <asp:TextBox ID="txtAmountTracedAmount_420" runat="server" CssClass="textBox" TabIndex="3"
                                                                Width="110px" MaxLength="15"
                                                                AutoPostBack="true" OnTextChanged="txtAmountTracedAmount_420_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[30] Date of Collection Instruction : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_420_DateofCollnInstruction" runat="server" CssClass="textBox"
                                                                Width="80px" TabIndex="6"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="DateofCollnInstruction420" Mask="99/99/9999"
                                                                MaskType="Date" runat="server" TargetControlID="txt_420_DateofCollnInstruction"
                                                                ErrorTooltipEnabled="True" CultureName="en-GB" CultureAMPMPlaceholder="AM;PM"
                                                                CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY" CultureDatePlaceholder="/"
                                                                Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                CultureTimePlaceholder=":">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="btnCal_Date" runat="server" CssClass="btncalendar_enabled" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txt_420_DateofCollnInstruction" PopupButtonID="btnCal_Date"
                                                                Enabled="True">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="DateofCollnInstruction420"
                                                                ValidationGroup="dtVal" ControlToValidate="txt_420_DateofCollnInstruction" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                                Enabled="False"></ajaxToolkit:MaskedEditValidator>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[59] Drawee </span><span class="elementLabel">Account :
                                                            </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_420_DraweeAccount" runat="server" CssClass="textBox" MaxLength="34"
                                                                Width="330px" TabIndex="18" AutoPostBack="true" 
                                                                OnTextChanged="txt_420_DraweeAccount_TextChanged" ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Name : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_420_DraweeName" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="19" AutoPostBack="true" 
                                                                OnTextChanged="txt_420_DraweeName_TextChanged" ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Address 1 : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_420_DraweeAdd1" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="20" AutoPostBack="true" 
                                                                OnTextChanged="txt_420_DraweeAdd1_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Address 2 : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_420_DraweeAdd2" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="21" AutoPostBack="true" 
                                                                OnTextChanged="txt_420_DraweeAdd2_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Address 3 : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_420_DraweeAdd3" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="22" AutoPostBack="true" 
                                                                OnTextChanged="txt_420_DraweeAdd3_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[72] Sender to Receiver Information : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_420_SenToRecinfo1" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="23" AutoPostBack="true" 
                                                                OnTextChanged="txt_420_SenToRecinfo1_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_420_SenToRecinfo2" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="24" AutoPostBack="true" 
                                                                OnTextChanged="txt_420_SenToRecinfo2_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_420_SenToRecinfo3" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="25" AutoPostBack="true" 
                                                                OnTextChanged="txt_420_SenToRecinfo3_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_420_SenToRecinfo4" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="26" AutoPostBack="true" 
                                                                OnTextChanged="txt_420_SenToRecinfo4_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_420_SenToRecinfo5" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="27" AutoPostBack="true" 
                                                                OnTextChanged="txt_420_SenToRecinfo5_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_420_SenToRecinfo6" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="28" AutoPostBack="true" 
                                                                OnTextChanged="txt_420_SenToRecinfo6_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                            <table>
                                               <asp:Panel ID="Panel_499" runat="server" Visible="false">
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <span style="font-size: large">MT : 499 - Free Format Message (For non LC documents) </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;"> Receiver </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReceiver499" runat="server" MaxLength="11" CssClass="textBox"
                                                                TabIndex="1" AutoPostBack="true"
                                                                OnTextChanged="txtReceiver499_TextChanged"></asp:TextBox>
                                                                <asp:Button ID="btnReceiver499BankList" runat="server" CssClass="btnHelp_enabled"
                                                                 TabIndex="1" OnClientClick="return OpenOverseasBankList('mouseClick','Swift499');" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[20] Transaction Reference Number :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_transRefNo" Width="" runat="server" MaxLength="16" CssClass="textBox"
                                                                AutoPostBack="true" OnTextChanged="txt_499_transRefNo_TextChanged" TabIndex="1"></asp:TextBox>
                                                                <%--<asp:Button ID="btnDocNoHelp_499" runat="server" CssClass="btnHelp_enabled" TabIndex="1"
                                                                OnClientClick="return OpenLogded_DocNoList('mouseClick','Swift499');" />--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[21] Related Reference : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_RelRef" runat="server" CssClass="textBox" MaxLength="16"
                                                                AutoPostBack="true"
                                                                OnTextChanged="txt_499_RelRef_TextChanged" TabIndex="2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[79] Narrative : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr1" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="3" AutoPostBack="true" OnTextChanged="txt_499_Narr1_TextChanged" ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr2" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="4" AutoPostBack="true" OnTextChanged="txt_499_Narr2_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr3" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="5" AutoPostBack="true" OnTextChanged="txt_499_Narr3_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr4" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="6" AutoPostBack="true" OnTextChanged="txt_499_Narr4_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr5" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="7" AutoPostBack="true" OnTextChanged="txt_499_Narr5_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr6" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="8" AutoPostBack="true" OnTextChanged="txt_499_Narr6_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr7" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="9" AutoPostBack="true" OnTextChanged="txt_499_Narr7_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr8" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="10" AutoPostBack="true" OnTextChanged="txt_499_Narr8_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr9" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="11" AutoPostBack="true" OnTextChanged="txt_499_Narr9_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr10" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="12" AutoPostBack="true" OnTextChanged="txt_499_Narr10_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr11" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="13" AutoPostBack="true" OnTextChanged="txt_499_Narr11_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr12" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="14" AutoPostBack="true" OnTextChanged="txt_499_Narr12_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr13" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="15" AutoPostBack="true" OnTextChanged="txt_499_Narr13_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr14" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="16" AutoPostBack="true" OnTextChanged="txt_499_Narr14_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr15" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="17" AutoPostBack="true" OnTextChanged="txt_499_Narr15_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr16" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="18" AutoPostBack="true" OnTextChanged="txt_499_Narr16_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr17" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="19" AutoPostBack="true" OnTextChanged="txt_499_Narr17_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr18" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="20" AutoPostBack="true" OnTextChanged="txt_499_Narr18_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr19" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="21" AutoPostBack="true" OnTextChanged="txt_499_Narr19_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr20" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="22" AutoPostBack="true" OnTextChanged="txt_499_Narr20_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr21" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="23" AutoPostBack="true" OnTextChanged="txt_499_Narr21_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr22" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="24" AutoPostBack="true" OnTextChanged="txt_499_Narr22_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr23" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="25" AutoPostBack="true" OnTextChanged="txt_499_Narr23_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr24" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="26" AutoPostBack="true" OnTextChanged="txt_499_Narr24_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr25" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="27" AutoPostBack="true" OnTextChanged="txt_499_Narr25_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr26" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="28" AutoPostBack="true" OnTextChanged="txt_499_Narr26_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr27" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="29" AutoPostBack="true" OnTextChanged="txt_499_Narr27_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr28" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="30" AutoPostBack="true" OnTextChanged="txt_499_Narr28_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr29" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="31" AutoPostBack="true" OnTextChanged="txt_499_Narr29_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr30" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="32" AutoPostBack="true" OnTextChanged="txt_499_Narr30_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr31" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="33" AutoPostBack="true" OnTextChanged="txt_499_Narr31_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr32" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="34" AutoPostBack="true" OnTextChanged="txt_499_Narr32_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr33" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="35" AutoPostBack="true" OnTextChanged="txt_499_Narr33_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr34" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="36" AutoPostBack="true" OnTextChanged="txt_499_Narr34_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr35" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="37" AutoPostBack="true" OnTextChanged="txt_499_Narr35_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                            <table>
                                                <asp:Panel ID="Panel_730" runat="server" Visible="false">
                                                    <tr>
                                                        <td align="left">
                                                            <span style="font-size: large">MT : 730 - Normal </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[20] Sender's Reference : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_730_SenderRef" runat="server" MaxLength="16" CssClass="textBox"
                                                                TabIndex="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[21] Receiver's Reference : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_730_RecRef" runat="server" CssClass="textBox" MaxLength="16"
                                                                TabIndex="2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[25] Account Identification : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_730_Account_Identification" runat="server" CssClass="textBox"
                                                                Width="250px" MaxLength="35" TabIndex="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[30] Date Of Message Being Acknowledge : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_730_DateOfMsg" runat="server" CssClass="textBox" Width="75px"
                                                                MaxLength="10" TabIndex="4"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[32A] Amount of Charges :</span>
                                                            <asp:DropDownList ID="ddlAmountofCharges_730" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlAmountofCharges_730_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="32B" Value="B"></asp:ListItem>
                                                                <asp:ListItem Text="32D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtAmountofChargesDate_730" runat="server" CssClass="textBox" TabIndex="5"
                                                                Width="70px" Visible="false" MaxLength="6"></asp:TextBox>
                                                            <asp:TextBox ID="txtAmountofChargesCurrency_730" runat="server" CssClass="textBox"
                                                                TabIndex="5" Width="35px" MaxLength="3"></asp:TextBox>
                                                            <asp:TextBox ID="txtAmountofChargesAmount_730" runat="server" CssClass="textBox"
                                                                TabIndex="5" Width="150px" MaxLength="15"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[57A] Account With Bank : </span>
                                                            <asp:DropDownList ID="ddlAccountWithBank_730" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlAccountWithBank_730_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="57A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="57D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Party Identifier : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtAccountWithBankAccountnumber_730" runat="server" CssClass="textBox"
                                                                TabIndex="6" Width="20px" Visible="false" MaxLength="1"></asp:TextBox>
                                                            <asp:TextBox ID="txtAccountWithBankAccountnumber1_730" runat="server" CssClass="textBox"
                                                                TabIndex="6" Width="250px" MaxLength="34"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountWithBankSwiftcode_730" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountWithBankIdentifiercode_730" CssClass="textBox"
                                                                MaxLength="11" TabIndex="6" Width="100px"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtAccountWithBankName_730" CssClass="textBox" Visible="false"
                                                                TabIndex="6" MaxLength="35" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountWithBankAddress1_730" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountWithBankAddress1_730" CssClass="textBox"
                                                                Visible="false" TabIndex="6" MaxLength="35" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountWithBankAddress2_730" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountWithBankAddress2_730" CssClass="textBox"
                                                                Visible="false" TabIndex="6" MaxLength="35" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountWithBankAddress3_730" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountWithBankAddress3_730" CssClass="textBox"
                                                                Visible="false" TabIndex="6" MaxLength="35" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[71B] Charges : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_730_Charges1" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="7"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_730_Charges2" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="7"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_730_Charges3" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="7"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_730_Charges4" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="7"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_730_Charges5" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="7"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_730_Charges6" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="7"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[72] Sender To Receiver Information : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_730_SenToRecInfo1" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="8"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_730_SenToRecInfo2" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="8"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_730_SenToRecInfo3" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="8"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_730_SenToRecInfo4" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="8"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_730_SenToRecInfo5" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="8"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_730_SenToRecInfo6" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="330px" TabIndex="8"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                            <table>
                                                <asp:Panel ID="Panel_742" runat="server" Visible="false">
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <span style="font-size: large">MT : 742 - Reimbursement Claim </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">Receiver : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReceiver742" runat="server" MaxLength="11" CssClass="textBox"
                                                                TabIndex="1" AutoPostBack="true" OnTextChanged="txtReceiver742_TextChanged"></asp:TextBox>
                                                            <asp:Button ID="btnReceiver742BankList" runat="server" CssClass="btnHelp_enabled"
                                                                TabIndex="1" OnClientClick="return OpenOverseasBankList('mouseClick','Swift742');" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[20] Claiming Bank's
                                                                Reference : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_ClaimBankRef" runat="server" MaxLength="16" CssClass="textBox"
                                                                TabIndex="1" AutoPostBack="true" OnTextChanged="txt_742_ClaimBankRef_TextChanged"></asp:TextBox>
                                                            <%--<asp:Button ID="btnDocNoHelp_742" runat="server" CssClass="btnHelp_enabled" TabIndex="1"
                                                                OnClientClick="return OpenLogded_DocNoList('mouseClick','Swift742');" />--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[21] Documentary Credit
                                                                Number : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_DocumCreditNo" runat="server" CssClass="textBox" MaxLength="16"
                                                                TabIndex="2" AutoPostBack="true" OnTextChanged="txt_742_DocumCreditNo_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[31C] Date Of Issue : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_Dateofissue" runat="server" CssClass="textBox" Width="75px"
                                                                MaxLength="6" TabIndex="3"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txt_742_Dateofissue" ErrorTooltipEnabled="True"
                                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="Dateofissue742" runat="server" CssClass="btncalendar_enabled" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txt_742_Dateofissue" PopupButtonID="Dateofissue742" Enabled="True">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender1"
                                                                ValidationGroup="dtVal" ControlToValidate="txt_742_Dateofissue" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                                Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[52A] Issuing Bank
                                                                : </span>
                                                            <asp:DropDownList ID="ddl_Issuingbank_742" runat="server" CssClass="dropdownList"
                                                                TabIndex="4" OnSelectedIndexChanged="ddl_Issuingbank_742_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="52A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="52D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Party Identifier : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtIssuingBankAccountnumber_742" runat="server" CssClass="textBox"
                                                                TabIndex="4" Width="20px" MaxLength="1" AutoPostBack="true" OnTextChanged="txtIssuingBankAccountnumber_742_TextChanged"></asp:TextBox>
                                                            <asp:TextBox ID="txtIssuingBankAccountnumber1_742" runat="server" CssClass="textBox"
                                                                TabIndex="4" Width="330px" MaxLength="34" AutoPostBack="true" OnTextChanged="txtIssuingBankAccountnumber1_742_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblIssuingBankIdentifier_742" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtIssuingBankIdentifiercode_742" CssClass="textBox"
                                                                MaxLength="11" TabIndex="4" Width="100px" AutoPostBack="true" OnTextChanged="txtIssuingBankIdentifiercode_742_TextChanged"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtIssuingBankName_742" CssClass="textBox" Visible="false"
                                                                MaxLength="35" TabIndex="4" Width="330px" AutoPostBack="true" OnTextChanged="txtIssuingBankName_742_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblIssuingBankAddress1_742" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtIssuingBankAddress1_742" CssClass="textBox" Visible="false"
                                                                TabIndex="4" MaxLength="35" Width="330px" AutoPostBack="true" OnTextChanged="txtIssuingBankAddress1_742_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblIssuingBankAddress2_742" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtIssuingBankAddress2_742" CssClass="textBox" Visible="false"
                                                                TabIndex="4" MaxLength="35" Width="330px" AutoPostBack="true" OnTextChanged="txtIssuingBankAddress2_742_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblIssuingBankAddress3_742" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtIssuingBankAddress3_742" CssClass="textBox" Visible="false"
                                                                TabIndex="4" MaxLength="35" Width="330px" AutoPostBack="true" OnTextChanged="txtIssuingBankAddress3_742_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[32B] Principal Amount
                                                                Claimed : </span>
                                                        </td>
                                                        <td>
                                                            <span class="elementLabel">Currency : </span>
                                                            <%--<asp:TextBox ID="txt_742_PrinAmtClmd_Ccy" runat="server" CssClass="textBox"  Width="35px" MaxLength="3" TabIndex="5"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddl_742_PrinAmtClmd_Ccy" runat="server" CssClass="dropdownList"
                                                                TabIndex="5" Width="70px" OnSelectedIndexChanged="ddl_742_PrinAmtClmd_Ccy_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Amount : </span>
                                                            <asp:TextBox ID="txt_742_PrinAmtClmd_Amt" runat="server" CssClass="textBox" MaxLength="15"
                                                                Width="95px" TabIndex="5"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[33B] Additional Amount Claimed as Allowed for in Excess
                                                                of Principal Amount : </span>
                                                        </td>
                                                        <td>
                                                            <span class="elementLabel">Currency : </span>
                                                            <%--<asp:TextBox ID="txt_742_AddAmtClamd_Ccy" runat="server" CssClass="textBox"  Width="35px" MaxLength="3" TabIndex="6"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddl_742_AddAmtClamd_Ccy" runat="server" CssClass="dropdownList"
                                                                TabIndex="6" Width="70px" OnSelectedIndexChanged="ddl_742_AddAmtClamd_Ccy_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Amount : </span>
                                                            <asp:TextBox ID="txt_742_AddAmtClamd_Amt" runat="server" CssClass="textBox" MaxLength="15"
                                                                Width="95px" TabIndex="6"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[71D] Charges : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_Charges1" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="7" AutoPostBack="true" OnTextChanged="txt_742_Charges1_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_Charges2" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="7" AutoPostBack="true" OnTextChanged="txt_742_Charges2_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_Charges3" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="7" AutoPostBack="true" OnTextChanged="txt_742_Charges3_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_Charges4" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="7" AutoPostBack="true" OnTextChanged="txt_742_Charges4_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_Charges5" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="7" AutoPostBack="true" OnTextChanged="txt_742_Charges5_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_Charges6" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="7" AutoPostBack="true" OnTextChanged="txt_742_Charges6_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[34A] Total Amount
                                                                Claimed : </span>
                                                            <asp:DropDownList ID="ddlTotalAmtclamd_742" runat="server" CssClass="dropdownList"
                                                                TabIndex="8" OnSelectedIndexChanged="ddlTotalAmtclamd_742_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="34A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="34B" Value="B"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lbl_742_TotalAmtClmd_Date" Text="Date :" runat="server" class="elementLabel"></asp:Label>
                                                            <asp:TextBox ID="txt_742_TotalAmtClmd_Date" runat="server" CssClass="textBox" Width="60px"
                                                                MaxLength="6" TabIndex="8"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txt_742_TotalAmtClmd_Date" ErrorTooltipEnabled="True"
                                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="TotalAmtClmd742_Date" runat="server" CssClass="btncalendar_enabled" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txt_742_TotalAmtClmd_Date" PopupButtonID="TotalAmtClmd742_Date"
                                                                Enabled="True">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender2"
                                                                ValidationGroup="dtVal" ControlToValidate="txt_742_TotalAmtClmd_Date" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                                Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                            <span class="elementLabel">Currency : </span>
                                                            <%--<asp:TextBox ID="txt_742_TotalAmtClmd_Ccy" runat="server" CssClass="textBox" MaxLength="3" Width="35px" TabIndex="8"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddl_742_TotalAmtClmd_Ccy" runat="server" CssClass="dropdownList"
                                                                TabIndex="8" Width="70px" OnSelectedIndexChanged="ddl_742_TotalAmtClmd_Ccy_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Amount : </span>
                                                            <asp:TextBox ID="txt_742_TotalAmtClmd_Amt" runat="server" CssClass="textBox" MaxLength="15"
                                                                Width="115px" TabIndex="8"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[57A] Account With Bank : </span>
                                                            <asp:DropDownList ID="ddlAccountwithbank_742" runat="server" CssClass="dropdownList"
                                                                TabIndex="9" OnSelectedIndexChanged="ddlAccountwithbank_742_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="57A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="57B" Value="B"></asp:ListItem>
                                                                <asp:ListItem Text="57D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Party Identifier : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtAccountwithBankAccountnumber_742" runat="server" CssClass="textBox"
                                                                TabIndex="9" Width="20px" MaxLength="1" AutoPostBack="true" OnTextChanged="txtAccountwithBankAccountnumber_742_TextChanged"></asp:TextBox>
                                                            <asp:TextBox ID="txtAccountwithBankAccountnumber1_742" runat="server" CssClass="textBox"
                                                                TabIndex="9" Width="330px" MaxLength="34" AutoPostBack="true" OnTextChanged="txtAccountwithBankAccountnumber1_742_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithBankIdentifier_742" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithBankIdentifiercode_742" CssClass="textBox"
                                                                MaxLength="11" TabIndex="9" Width="100px" AutoPostBack="true" OnTextChanged="txtAccountwithBankIdentifiercode_742_TextChanged"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtAccountwithBankLocation_742" CssClass="textBox"
                                                                Visible="false" MaxLength="35" TabIndex="9" Width="330px" AutoPostBack="true"
                                                                OnTextChanged="txtAccountwithBankLocation_742_TextChanged"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtAccountwithBankName_742" CssClass="textBox" Visible="false"
                                                                MaxLength="35" TabIndex="9" Width="330px" AutoPostBack="true" OnTextChanged="txtAccountwithBankName_742_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithBankAddress1_742" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithBankAddress1_742" CssClass="textBox"
                                                                Visible="false" TabIndex="9" MaxLength="35" Width="330px" AutoPostBack="true"
                                                                OnTextChanged="txtAccountwithBankAddress1_742_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithBankAddress2_742" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithBankAddress2_742" CssClass="textBox"
                                                                Visible="false" TabIndex="9" MaxLength="35" Width="330px" AutoPostBack="true"
                                                                OnTextChanged="txtAccountwithBankAddress2_742_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithBankAddress3_742" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithBankAddress3_742" CssClass="textBox"
                                                                Visible="false" TabIndex="9" MaxLength="35" Width="330px" AutoPostBack="true"
                                                                OnTextChanged="txtAccountwithBankAddress3_742_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[58A] Beneficiary Bank : </span>
                                                            <asp:DropDownList ID="ddlBeneficiarybank_742" runat="server" CssClass="dropdownList"
                                                                TabIndex="10" OnSelectedIndexChanged="ddlBeneficiarybank_742_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="58A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="58D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Party Identifier : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtBeneficiaryBankAccountnumber_742" runat="server" CssClass="textBox"
                                                                TabIndex="10" Width="20px" MaxLength="1" AutoPostBack="true" OnTextChanged="txtBeneficiaryBankAccountnumber_742_TextChanged"></asp:TextBox>
                                                            <asp:TextBox ID="txtBeneficiaryBankAccountnumber1_742" runat="server" CssClass="textBox"
                                                                TabIndex="10" Width="330px" MaxLength="34" AutoPostBack="true" OnTextChanged="txtBeneficiaryBankAccountnumber1_742_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblBeneficiaryBankIdentifier_742" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryBankIdentifiercode_742" CssClass="textBox"
                                                                MaxLength="11" TabIndex="10" Width="100px" AutoPostBack="true" OnTextChanged="txtBeneficiaryBankIdentifiercode_742_TextChanged"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryBankName_742" CssClass="textBox" Visible="false"
                                                                MaxLength="35" TabIndex="10" Width="330px" AutoPostBack="true" OnTextChanged="txtBeneficiaryBankName_742_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblBeneficiaryBankAddress1_742" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryBankAddress1_742" CssClass="textBox"
                                                                Visible="false" TabIndex="10" MaxLength="35" Width="330px" AutoPostBack="true"
                                                                OnTextChanged="txtBeneficiaryBankAddress1_742_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblBeneficiaryBankAddress2_742" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryBankAddress2_742" CssClass="textBox"
                                                                Visible="false" TabIndex="10" MaxLength="35" Width="330px" AutoPostBack="true"
                                                                OnTextChanged="txtBeneficiaryBankAddress2_742_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblBeneficiaryBankAddress3_742" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryBankAddress3_742" CssClass="textBox"
                                                                Visible="false" TabIndex="10" MaxLength="35" Width="330px" AutoPostBack="true"
                                                                OnTextChanged="txtBeneficiaryBankAddress3_742_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[72Z] Sender to Receiver Information : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_SenRecInfo1" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="11" AutoPostBack="true" OnTextChanged="txt_742_SenRecInfo1_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_SenRecInfo2" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="11" AutoPostBack="true" OnTextChanged="txt_742_SenRecInfo2_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_SenRecInfo3" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="11" AutoPostBack="true" OnTextChanged="txt_742_SenRecInfo3_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_SenRecInfo4" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="11" AutoPostBack="true" OnTextChanged="txt_742_SenRecInfo4_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_SenRecInfo5" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="11" AutoPostBack="true" OnTextChanged="txt_742_SenRecInfo5_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_SenRecInfo6" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="11" AutoPostBack="true" OnTextChanged="txt_742_SenRecInfo6_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                            <table>
                                                <asp:Panel ID="Panel_754" runat="server" Visible="false">
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <span style="font-size: large">MT : 754 - Advice of Payment/Acceptance/Negotiation </span>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">Receiver : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReceiver754" runat="server" MaxLength="11" CssClass="textBox"
                                                                TabIndex="1" AutoPostBack="true" OnTextChanged="txtReceiver754_TextChanged"></asp:TextBox>
                                                            <asp:Button ID="btnReceiver754BankList" runat="server" CssClass="btnHelp_enabled"
                                                                TabIndex="1" OnClientClick="return OpenOverseasBankList('mouseClick','Swift754');" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[20] Sender's Reference
                                                                : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_SenRef" runat="server" MaxLength="16" CssClass="textBox"
                                                                TabIndex="1" AutoPostBack="true" OnTextChanged="txt_754_SenRef_TextChanged"></asp:TextBox>
                                                            <%--<asp:Button ID="btnDocNoHelp_754" runat="server" CssClass="btnHelp_enabled" TabIndex="1"
                                                                OnClientClick="return OpenLogded_DocNoList('mouseClick','Swift754');" />--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[21] Related Reference
                                                                : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_RelRef" runat="server" CssClass="textBox" MaxLength="16"
                                                                TabIndex="2" AutoPostBack="true" OnTextChanged="txt_754_RelRef_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[32A] Principal Amount
                                                                Paid/Accepted/Negotiated : </span>
                                                            <asp:DropDownList ID="ddlPrinAmtPaidAccNego_754" runat="server" CssClass="dropdownList"
                                                                TabIndex="2" OnSelectedIndexChanged="ddlPrinAmtPaidAccNego_754_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="32A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="32B" Value="B"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblPrinAmtPaidAccNegoDate_754" Text="Date :" runat="server" class="elementLabel"></asp:Label>
                                                            <asp:TextBox ID="txtPrinAmtPaidAccNegoDate_754" runat="server" CssClass="textBox"
                                                                Width="60px" MaxLength="6" TabIndex="2"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txtPrinAmtPaidAccNegoDate_754" ErrorTooltipEnabled="True"
                                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="BtnPrinAmtPaidAccNegoDate_754" runat="server" CssClass="btncalendar_enabled" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtPrinAmtPaidAccNegoDate_754" PopupButtonID="BtnPrinAmtPaidAccNegoDate_754"
                                                                Enabled="True">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="MaskedEditExtender3"
                                                                ValidationGroup="dtVal" ControlToValidate="txtPrinAmtPaidAccNegoDate_754" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                                Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                            <span class="elementLabel">Currency : </span>
                                                            <%--<asp:TextBox ID="txtPrinAmtPaidAccNegoCurr_754" runat="server" CssClass="textBox" MaxLength="3" Width="35px" TabIndex="2"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddl_PrinAmtPaidAccNegoCurr_754" runat="server" CssClass="dropdownList"
                                                                TabIndex="2" Width="70px" OnSelectedIndexChanged="ddl_PrinAmtPaidAccNegoCurr_754_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Amount : </span>
                                                            <asp:TextBox ID="txtPrinAmtPaidAccNegoAmt_754" runat="server" CssClass="textBox"
                                                                MaxLength="15" Width="115px" TabIndex="2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[33B] Additional Amounts : </span>
                                                        </td>
                                                        <td>
                                                            <span class="elementLabel">Currency : </span>
                                                            <%--<asp:TextBox ID="txt_754_AddAmtClamd_Ccy" runat="server" CssClass="textBox"  Width="35px" MaxLength="3" TabIndex="2"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddl_754_AddAmtClamd_Ccy" runat="server" CssClass="dropdownList"
                                                                TabIndex="2" Width="70px" OnSelectedIndexChanged="ddl_754_AddAmtClamd_Ccy_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Amount : </span>
                                                            <asp:TextBox ID="txt_754_AddAmtClamd_Amt" runat="server" CssClass="textBox" MaxLength="15"
                                                                Width="95px" TabIndex="2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[71D] Charges Deducted : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_ChargesDeduct1" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="3" AutoPostBack="true" OnTextChanged="txt_754_ChargesDeduct1_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_ChargesDeduct2" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="3" AutoPostBack="true" OnTextChanged="txt_754_ChargesDeduct2_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_ChargesDeduct3" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="3" AutoPostBack="true" OnTextChanged="txt_754_ChargesDeduct3_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_ChargesDeduct4" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="3" AutoPostBack="true" OnTextChanged="txt_754_ChargesDeduct4_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_ChargesDeduct5" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="3" AutoPostBack="true" OnTextChanged="txt_754_ChargesDeduct5_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_ChargesDeduct6" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="3" AutoPostBack="true" OnTextChanged="txt_754_ChargesDeduct6_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[73A] Charges Added: </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_ChargesAdded1" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="3" AutoPostBack="true" OnTextChanged="txt_754_ChargesAdded1_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_ChargesAdded2" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="3" AutoPostBack="true" OnTextChanged="txt_754_ChargesAdded2_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_ChargesAdded3" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="3" AutoPostBack="true" OnTextChanged="txt_754_ChargesAdded3_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_ChargesAdded4" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="3" AutoPostBack="true" OnTextChanged="txt_754_ChargesAdded4_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_ChargesAdded5" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="3" AutoPostBack="true" OnTextChanged="txt_754_ChargesAdded5_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_ChargesAdded6" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="3" AutoPostBack="true" OnTextChanged="txt_754_ChargesAdded6_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[34A] Total Amount Claimed : </span>
                                                            <asp:DropDownList ID="ddlTotalAmtclamd_754" runat="server" CssClass="dropdownList"
                                                                TabIndex="4" OnSelectedIndexChanged="ddlTotalAmtclamd_754_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="34A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="34B" Value="B"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lbl_754_TotalAmtClmd_Date" Text="Date :" runat="server" class="elementLabel"></asp:Label>
                                                            <asp:TextBox ID="txt_754_TotalAmtClmd_Date" runat="server" CssClass="textBox" Width="60px"
                                                                MaxLength="6" TabIndex="4"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txt_754_TotalAmtClmd_Date" ErrorTooltipEnabled="True"
                                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="TotalAmtClmd754_Date" runat="server" CssClass="btncalendar_enabled" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txt_754_TotalAmtClmd_Date" PopupButtonID="TotalAmtClmd754_Date"
                                                                Enabled="True">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator6" runat="server" ControlExtender="MaskedEditExtender4"
                                                                ValidationGroup="dtVal" ControlToValidate="txt_754_TotalAmtClmd_Date" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                                Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                            <span class="elementLabel">Currency : </span>
                                                            <%--<asp:TextBox ID="txt_754_TotalAmtClmd_Ccy" runat="server" CssClass="textBox" MaxLength="3" Width="35px" TabIndex="8"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddl_754_TotalAmtClmd_Ccy" runat="server" CssClass="dropdownList"
                                                                TabIndex="4" Width="70px" OnSelectedIndexChanged="ddl_754_TotalAmtClmd_Ccy_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Amount : </span>
                                                            <asp:TextBox ID="txt_754_TotalAmtClmd_Amt" runat="server" CssClass="textBox" MaxLength="15"
                                                                Width="115px" TabIndex="4"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[53A] Reimbursing Bank : </span>
                                                            <asp:DropDownList ID="ddlReimbursingbank_754" runat="server" CssClass="dropdownList"
                                                                TabIndex="9" OnSelectedIndexChanged="ddlReimbursingbank_754_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="53A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="53B" Value="B"></asp:ListItem>
                                                                <asp:ListItem Text="53D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Party Identifier : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtReimbursingBankAccountnumber_754" runat="server" CssClass="textBox"
                                                                TabIndex="9" Width="20px" MaxLength="1" AutoPostBack="true" OnTextChanged="txtReimbursingBankAccountnumber_754_TextChanged"></asp:TextBox>
                                                            <asp:TextBox ID="txtReimbursingBankAccountnumber1_754" runat="server" CssClass="textBox"
                                                                TabIndex="9" Width="330px" MaxLength="34" AutoPostBack="true" OnTextChanged="txtReimbursingBankAccountnumber1_754_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblReimbursingBankIdentifier_754" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtReimbursingBankIdentifiercode_754" CssClass="textBox"
                                                                MaxLength="11" TabIndex="9" Width="100px" AutoPostBack="true" OnTextChanged="txtReimbursingBankIdentifiercode_754_TextChanged"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtReimbursingBankLocation_754" CssClass="textBox"
                                                                Visible="false" MaxLength="35" TabIndex="9" Width="330px" AutoPostBack="true"
                                                                OnTextChanged="txtReimbursingBankLocation_754_TextChanged"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtReimbursingBankName_754" CssClass="textBox" Visible="false"
                                                                MaxLength="35" TabIndex="9" Width="330px" AutoPostBack="true" OnTextChanged="txtReimbursingBankName_754_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblReimbursingBankAddress1_754" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtReimbursingBankAddress1_754" CssClass="textBox"
                                                                Visible="false" TabIndex="9" MaxLength="35" Width="330px" AutoPostBack="true"
                                                                OnTextChanged="txtReimbursingBankAddress1_754_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblReimbursingBankAddress2_754" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtReimbursingBankAddress2_754" CssClass="textBox"
                                                                Visible="false" TabIndex="9" MaxLength="35" Width="330px" AutoPostBack="true"
                                                                OnTextChanged="txtReimbursingBankAddress2_754_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblReimbursingBankAddress3_754" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtReimbursingBankAddress3_754" CssClass="textBox"
                                                                Visible="false" TabIndex="9" MaxLength="35" Width="330px" AutoPostBack="true"
                                                                OnTextChanged="txtReimbursingBankAddress3_754_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[57A] Account With Bank : </span>
                                                            <asp:DropDownList ID="ddlAccountwithbank_754" runat="server" CssClass="dropdownList"
                                                                TabIndex="9" OnSelectedIndexChanged="ddlAccountwithbank_754_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="57A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="57B" Value="B"></asp:ListItem>
                                                                <asp:ListItem Text="57D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Party Identifier : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtAccountwithBankAccountnumber_754" runat="server" CssClass="textBox"
                                                                TabIndex="9" Width="20px" MaxLength="1" AutoPostBack="true" OnTextChanged="txtAccountwithBankAccountnumber_754_TextChanged"></asp:TextBox>
                                                            <asp:TextBox ID="txtAccountwithBankAccountnumber1_754" runat="server" CssClass="textBox"
                                                                TabIndex="9" Width="330px" MaxLength="34" AutoPostBack="true" OnTextChanged="txtAccountwithBankAccountnumber1_754_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithBankIdentifier_754" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithBankIdentifiercode_754" CssClass="textBox"
                                                                MaxLength="11" TabIndex="9" Width="100px" AutoPostBack="true" OnTextChanged="txtAccountwithBankIdentifiercode_754_TextChanged"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtAccountwithBankLocation_754" CssClass="textBox"
                                                                Visible="false" MaxLength="35" TabIndex="9" Width="330px" AutoPostBack="true"
                                                                OnTextChanged="txtAccountwithBankLocation_754_TextChanged"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtAccountwithBankName_754" CssClass="textBox" Visible="false"
                                                                MaxLength="35" TabIndex="9" Width="330px" AutoPostBack="true" OnTextChanged="txtAccountwithBankName_754_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithBankAddress1_754" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithBankAddress1_754" CssClass="textBox"
                                                                Visible="false" TabIndex="9" MaxLength="35" Width="330px" AutoPostBack="true"
                                                                OnTextChanged="txtAccountwithBankAddress1_754_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithBankAddress2_754" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithBankAddress2_754" CssClass="textBox"
                                                                Visible="false" TabIndex="9" MaxLength="35" Width="330px" AutoPostBack="true"
                                                                OnTextChanged="txtAccountwithBankAddress2_754_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithBankAddress3_754" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithBankAddress3_754" CssClass="textBox"
                                                                Visible="false" TabIndex="9" MaxLength="35" Width="330px" AutoPostBack="true"
                                                                OnTextChanged="txtAccountwithBankAddress3_754_TextChanged"></asp:TextBox></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[58A] Beneficiary Bank : </span>
                                                            <asp:DropDownList ID="ddlBeneficiarybank_754" runat="server" CssClass="dropdownList"
                                                                TabIndex="10" OnSelectedIndexChanged="ddlBeneficiarybank_754_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="58A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="58D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Party Identifier : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtBeneficiaryBankAccountnumber_754" runat="server" CssClass="textBox"
                                                                TabIndex="10" Width="20px" MaxLength="1" AutoPostBack="true" OnTextChanged="txtBeneficiaryBankAccountnumber_754_TextChanged"></asp:TextBox>
                                                            <asp:TextBox ID="txtBeneficiaryBankAccountnumber1_754" runat="server" CssClass="textBox"
                                                                TabIndex="10" Width="330px" MaxLength="34" AutoPostBack="true" OnTextChanged="txtBeneficiaryBankAccountnumber1_754_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblBeneficiaryBankIdentifier_754" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryBankIdentifiercode_754" CssClass="textBox"
                                                                MaxLength="11" TabIndex="10" Width="100px" AutoPostBack="true" OnTextChanged="txtBeneficiaryBankIdentifiercode_754_TextChanged"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryBankName_754" CssClass="textBox" Visible="false"
                                                                MaxLength="35" TabIndex="10" Width="330px" AutoPostBack="true" OnTextChanged="txtBeneficiaryBankName_754_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblBeneficiaryBankAddress1_754" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryBankAddress1_754" CssClass="textBox"
                                                                Visible="false" TabIndex="10" MaxLength="35" Width="330px" AutoPostBack="true"
                                                                OnTextChanged="txtBeneficiaryBankAddress1_754_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblBeneficiaryBankAddress2_754" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryBankAddress2_754" CssClass="textBox"
                                                                Visible="false" TabIndex="10" MaxLength="35" Width="330px" AutoPostBack="true"
                                                                OnTextChanged="txtBeneficiaryBankAddress2_754_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblBeneficiaryBankAddress3_754" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryBankAddress3_754" CssClass="textBox"
                                                                Visible="false" TabIndex="10" MaxLength="35" Width="330px" AutoPostBack="true"
                                                                OnTextChanged="txtBeneficiaryBankAddress3_754_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[72Z] Sender to Receiver Information : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_SenRecInfo1" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="11" AutoPostBack="true" OnTextChanged="txt_754_SenRecInfo1_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_SenRecInfo2" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="11" AutoPostBack="true" OnTextChanged="txt_754_SenRecInfo2_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_SenRecInfo3" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="11" AutoPostBack="true" OnTextChanged="txt_754_SenRecInfo3_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_SenRecInfo4" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="11" AutoPostBack="true" OnTextChanged="txt_754_SenRecInfo4_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_SenRecInfo5" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="11" AutoPostBack="true" OnTextChanged="txt_754_SenRecInfo5_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_SenRecInfo6" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="11" AutoPostBack="true" OnTextChanged="txt_754_SenRecInfo6_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[77] Narrative : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr1" runat="server" CssClass="textBox" Width="420px" MaxLength="35"
                                                                TabIndex="11" AutoPostBack="true" OnTextChanged="txt_754_Narr1_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr2" runat="server" CssClass="textBox" Width="420px" MaxLength="35"
                                                                TabIndex="11" AutoPostBack="true" OnTextChanged="txt_754_Narr2_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr3" runat="server" CssClass="textBox" Width="420px" MaxLength="35"
                                                                TabIndex="11" AutoPostBack="true" OnTextChanged="txt_754_Narr3_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr4" runat="server" CssClass="textBox" Width="420px" MaxLength="35"
                                                                TabIndex="11" AutoPostBack="true" OnTextChanged="txt_754_Narr4_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr5" runat="server" CssClass="textBox" Width="420px" MaxLength="35"
                                                                TabIndex="11" AutoPostBack="true" OnTextChanged="txt_754_Narr5_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr6" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="11" AutoPostBack="true" OnTextChanged="txt_754_Narr6_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr7" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="11" AutoPostBack="true" OnTextChanged="txt_754_Narr7_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr8" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="11" AutoPostBack="true" OnTextChanged="txt_754_Narr8_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr9" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="11" AutoPostBack="true" OnTextChanged="txt_754_Narr9_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr10" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="12" AutoPostBack="true" OnTextChanged="txt_754_Narr10_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr11" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="13" AutoPostBack="true" OnTextChanged="txt_754_Narr11_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr12" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="14" AutoPostBack="true" OnTextChanged="txt_754_Narr12_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr13" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="15" AutoPostBack="true" OnTextChanged="txt_754_Narr13_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr14" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="16" AutoPostBack="true" OnTextChanged="txt_754_Narr14_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr15" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="17" AutoPostBack="true" OnTextChanged="txt_754_Narr15_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr16" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="18" AutoPostBack="true" OnTextChanged="txt_754_Narr16_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr17" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="19" AutoPostBack="true" OnTextChanged="txt_754_Narr17_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr18" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="20" AutoPostBack="true" OnTextChanged="txt_754_Narr18_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr19" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="21" AutoPostBack="true" OnTextChanged="txt_754_Narr19_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr20" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="22" AutoPostBack="true" OnTextChanged="txt_754_Narr20_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                            <table>
                                                <asp:Panel ID="Panel_799" runat="server" Visible="false">
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <span style="font-size: large">MT : 799 - Free Format Message  (For LC documents) </span>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">Receiver : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReceiver799" runat="server" MaxLength="11" AutoPostBack="true"
                                                                OnTextChanged="txtReceiver799_TextChanged" CssClass="textBox" TabIndex="1"></asp:TextBox>
                                                            <asp:Button ID="btnReceiver799BankList" runat="server" CssClass="btnHelp_enabled"
                                                                TabIndex="1" OnClientClick="return OpenOverseasBankList('mouseClick','Swift799');" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[20] Transaction Reference
                                                                Number : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_transRefNo" Width="" runat="server" MaxLength="16" CssClass="textBox"
                                                                AutoPostBack="true" OnTextChanged="txt_799_transRefNo_TextChanged" TabIndex="1"></asp:TextBox>
                                                            <%--<asp:Button ID="btnDocNoHelp_799" runat="server" CssClass="btnHelp_enabled" TabIndex="1"
                                                                OnClientClick="return OpenLogded_DocNoList('mouseClick','Swift799');" />--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[21] Related Reference : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_RelRef" runat="server" CssClass="textBox" AutoPostBack="true"
                                                                OnTextChanged="txt_799_RelRef_TextChanged" MaxLength="16" TabIndex="2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[79] Narrative :
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr1" runat="server" CssClass="textBox" Width="420px" AutoPostBack="true"
                                                                OnTextChanged="txt_799_Narr1_TextChanged" MaxLength="35" TabIndex="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr2" runat="server" CssClass="textBox" Width="420px" AutoPostBack="true"
                                                                OnTextChanged="txt_799_Narr2_TextChanged" MaxLength="35" TabIndex="4"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr3" runat="server" CssClass="textBox" Width="420px" AutoPostBack="true"
                                                                OnTextChanged="txt_799_Narr3_TextChanged" MaxLength="35" TabIndex="5"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr4" runat="server" CssClass="textBox" Width="420px" AutoPostBack="true"
                                                                OnTextChanged="txt_799_Narr4_TextChanged" MaxLength="35" TabIndex="6"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr5" runat="server" CssClass="textBox" Width="420px" AutoPostBack="true"
                                                                OnTextChanged="txt_799_Narr5_TextChanged" MaxLength="35" TabIndex="7"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr6" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr6_TextChanged" TabIndex="8"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr7" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr7_TextChanged" TabIndex="9"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr8" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr8_TextChanged" TabIndex="10"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr9" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr9_TextChanged" TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr10" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr10_TextChanged"
                                                                TabIndex="12"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr11" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr11_TextChanged"
                                                                TabIndex="13"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr12" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr12_TextChanged"
                                                                TabIndex="14"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr13" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr13_TextChanged"
                                                                TabIndex="15"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr14" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr14_TextChanged"
                                                                TabIndex="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr15" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr15_TextChanged"
                                                                TabIndex="17"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr16" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr16_TextChanged"
                                                                TabIndex="18"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr17" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr17_TextChanged"
                                                                TabIndex="19"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr18" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr18_TextChanged"
                                                                TabIndex="20"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr19" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr19_TextChanged"
                                                                TabIndex="21"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr20" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr20_TextChanged"
                                                                TabIndex="22"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr21" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr21_TextChanged"
                                                                TabIndex="23"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr22" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr22_TextChanged"
                                                                TabIndex="24"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr23" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr23_TextChanged"
                                                                TabIndex="25"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr24" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr24_TextChanged"
                                                                TabIndex="26"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr25" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr25_TextChanged"
                                                                TabIndex="27"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr26" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr26_TextChanged"
                                                                TabIndex="28"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr27" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr27_TextChanged"
                                                                TabIndex="29"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr28" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr28_TextChanged"
                                                                TabIndex="30"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr29" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr29_TextChanged"
                                                                TabIndex="31"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr30" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr30_TextChanged"
                                                                TabIndex="32"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr31" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr31_TextChanged"
                                                                TabIndex="33"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr32" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr32_TextChanged"
                                                                TabIndex="34"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr33" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr33_TextChanged"
                                                                TabIndex="35"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr34" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr34_TextChanged"
                                                                TabIndex="36"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr35" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" AutoPostBack="true" OnTextChanged="txt_799_Narr35_TextChanged"
                                                                TabIndex="37"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                            <table>
                                               <asp:Panel ID="Panel_999" runat="server" Visible="false">
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <span style="font-size: large">MT : 999 - Free Format Message </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;"> Receiver </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReceiver999" runat="server" MaxLength="11" CssClass="textBox"
                                                                TabIndex="1" AutoPostBack="true"
                                                                OnTextChanged="txtReceiver999_TextChanged"></asp:TextBox>
                                                                <asp:Button ID="btnReceiver999BankList" runat="server" CssClass="btnHelp_enabled"
                                                                 TabIndex="1" OnClientClick="return OpenOverseasBankList('mouseClick','Swift999');" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[20] Transaction Reference Number :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_transRefNo" Width="" runat="server" MaxLength="16" CssClass="textBox"
                                                                AutoPostBack="true" OnTextChanged="txt_999_transRefNo_TextChanged" TabIndex="1"></asp:TextBox>
                                                                <%--<asp:Button ID="btnDocNoHelp_999" runat="server" CssClass="btnHelp_enabled" TabIndex="1"
                                                                OnClientClick="return OpenLogded_DocNoList('mouseClick','Swift999');" />--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[21] Related Reference : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_RelRef" runat="server" CssClass="textBox" MaxLength="16"
                                                                AutoPostBack="true"
                                                                OnTextChanged="txt_999_RelRef_TextChanged" TabIndex="2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[79] Narrative : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr1" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="3" AutoPostBack="true" OnTextChanged="txt_999_Narr1_TextChanged" ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr2" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="4" AutoPostBack="true" OnTextChanged="txt_999_Narr2_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr3" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="5" AutoPostBack="true" OnTextChanged="txt_999_Narr3_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr4" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="6" AutoPostBack="true" OnTextChanged="txt_999_Narr4_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr5" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="7" AutoPostBack="true" OnTextChanged="txt_999_Narr5_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr6" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="8" AutoPostBack="true" OnTextChanged="txt_999_Narr6_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr7" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="9" AutoPostBack="true" OnTextChanged="txt_999_Narr7_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr8" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="10" AutoPostBack="true" OnTextChanged="txt_999_Narr8_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr9" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="11" AutoPostBack="true" OnTextChanged="txt_999_Narr9_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr10" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="12" AutoPostBack="true" OnTextChanged="txt_999_Narr10_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr11" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="13" AutoPostBack="true" OnTextChanged="txt_999_Narr11_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr12" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="14" AutoPostBack="true" OnTextChanged="txt_999_Narr12_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr13" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="15" AutoPostBack="true" OnTextChanged="txt_999_Narr13_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr14" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="16" AutoPostBack="true" OnTextChanged="txt_999_Narr14_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr15" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="17" AutoPostBack="true" OnTextChanged="txt_999_Narr15_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr16" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="18" AutoPostBack="true" OnTextChanged="txt_999_Narr16_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr17" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="19" AutoPostBack="true" OnTextChanged="txt_999_Narr17_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr18" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="20" AutoPostBack="true" OnTextChanged="txt_999_Narr18_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr19" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="21" AutoPostBack="true" OnTextChanged="txt_999_Narr19_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr20" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="22" AutoPostBack="true" OnTextChanged="txt_999_Narr20_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr21" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="23" AutoPostBack="true" OnTextChanged="txt_999_Narr21_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr22" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="24" AutoPostBack="true" OnTextChanged="txt_999_Narr22_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr23" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="25" AutoPostBack="true" OnTextChanged="txt_999_Narr23_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr24" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="26" AutoPostBack="true" OnTextChanged="txt_999_Narr24_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr25" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="27" AutoPostBack="true" OnTextChanged="txt_999_Narr25_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr26" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="28" AutoPostBack="true" OnTextChanged="txt_999_Narr26_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr27" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="29" AutoPostBack="true" OnTextChanged="txt_999_Narr27_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr28" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="30" AutoPostBack="true" OnTextChanged="txt_999_Narr28_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr29" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="31" AutoPostBack="true" OnTextChanged="txt_999_Narr29_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr30" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="32" AutoPostBack="true" OnTextChanged="txt_999_Narr30_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr31" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="33" AutoPostBack="true" OnTextChanged="txt_999_Narr31_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr32" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="34" AutoPostBack="true" OnTextChanged="txt_999_Narr32_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr33" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="35" AutoPostBack="true" OnTextChanged="txt_999_Narr33_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr34" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="36" AutoPostBack="true" OnTextChanged="txt_999_Narr34_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr35" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="37" AutoPostBack="true" OnTextChanged="txt_999_Narr35_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                </ajaxToolkit:TabContainer>
                            </td>
                        </tr>
                    </table>
                </table>
                <tr>
                    <td align="left" colspan="3">
                        <asp:Button ID="btn_SaveSwift" runat="server" Text="Save Draft" CssClass="buttonDefault"
                            Width="150px" ToolTip="Save SWIFT Message" Height="32" TabIndex="256" OnClick="btn_SaveSwift_Click" />
                    </td>
                    <td align="left" colspan="3">
                        <asp:Button ID="btn_View_Swift" runat="server" Text="Save and View Swift"
                            CssClass="buttonDefault" Width="150px" ToolTip="Save and View SWIFT Message"
                            Height="32" TabIndex="256" OnClick="btn_View_Swift_Click" />
                    </td>
                    
                    <td align="left" colspan="3">
                        <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" TabIndex="256" Text="Send To Checker"
                            Width="150px" ToolTip="Send To Checker" OnClick="btnSave_Click" Height="32" />
                    </td>
                    <td align="center" colspan="4">
                        <asp:Label ID="lblChecker_Remark" runat="server" CssClass="mandatoryField" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
