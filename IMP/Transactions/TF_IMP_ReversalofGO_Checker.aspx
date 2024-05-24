﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_ReversalofGO_Checker.aspx.cs" Inherits="IMP_Transactions_TF_IMP_ReversalofGO_Checker" %>

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
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="../../Style/Style_V2.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="../../Style/TAB.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../../Scripts/jquery-1.8.3.min.js" type="text/javascript""></script>
    <script src="../../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <link href="../../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../../Help_Plugins/MyJquery1.js" type="text/javascript"></script>
    <script src="../Scripts/TF_IMP_ReversalofGO_Checker.js" type="text/javascript"></script>
    
    </head>
   <body onload="EndRequest();closeWindows();" onunload="closeWindows();">
    <form id="form1" runat="server" autocomplete="off">
      <div style="width: 100%">
            <div id="Div1" class="AlertJqueryHide">
                <p id="P1">
                </p>
            </div>
        </div>
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../../Scripts/Enable_Disable_Opener.js"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/InitEndRequest.js"></script>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnBack" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><strong>&nbsp; BRO Margin Reversal - Checker</strong></span>
                            </td>
                            <td align="right" style="width: 50%">
                                 <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                    TabIndex="46"  OnClientClick="return OnBackClick();" />
                                </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="middle" colspan="2">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%;" valign="top" colspan="2">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                                <%-----------------Hidden Fields--------------------------%>
                                 <input type="hidden" id="hdnRejectReason" runat="server" />
                            </td>
                        </tr>
                        </table>
                        <table id="tbl_Acceptance" cellspacing="0" border="0" width="100%">
                        <tr>
                        <td align="left">
                        <span class="elementLabel">BRO NO : </span>
                        <asp:TextBox ID="txtDocNo" Enabled="false" runat="server" CssClass="textBox" Width="110px" TabIndex="1"></asp:TextBox>
                        </td>
                          <td align="left">
                        <span class="elementLabel">BRO Date : </span>
                        <asp:TextBox ID="txtBrodate1" Enabled="false"  runat="server" CssClass="textBox" Width="90px" TabIndex="2"></asp:TextBox>
                        </td>
                         <td align="left">
                        <span class="elementLabel">Applicant Name : </span>
                        <asp:TextBox ID="txtAppl_Name" Enabled="false" runat="server" CssClass="textBox" Width="220px" TabIndex="3"></asp:TextBox>
                        </td>
                        <td align="left">
                        <span class="elementLabel"> CCY : </span>
                        <asp:TextBox ID="txtccy" Enabled="false" runat="server" CssClass="textBox"  Width="50px" TabIndex="4"></asp:TextBox>
                        </td>
                         <td align="left">
                        <span class="elementLabel">Bill Amt : </span>
                        <asp:TextBox ID="txtBill_Amt" Enabled="false" runat="server" CssClass="textBox"  Width="110px" TabIndex="5"></asp:TextBox>
                        </td>
                        </tr>
                        <tr>
                         <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="5">
                                <ajaxToolkit:TabContainer ID="TabContainerMain" runat="server" ActiveTabIndex="0"
                                    CssClass="ajax__tab_xp-theme">
                                     <ajaxToolkit:TabPanel ID="tbBROGO1" runat="server" colspan="3" HeaderText="Reversal Of Gen Operation"
                                      Font-Bold="true" ForeColor="White">
                                        <ContentTemplate>
                                            <table width="80%">
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">COMMENT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Comment" runat="server" CssClass="textBox" TabIndex="6"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="20%">
                                                            <span class="elementLabel">Section No : </span>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="txt_GO1_SectionNo" runat="server" CssClass="textBox" TabIndex="7"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right" width="15%">
                                                            <span class="elementLabel">Remarks : </span>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="txt_GO1_Remarks"  Width="300px" runat="server" CssClass="textBox" TabIndex="8"></asp:TextBox>
                                                        </td>
                                                        <td align="right" width="15%">
                                                            <span class="elementLabel">MEMO : </span>
                                                        </td>
                                                        <td width="10%">
                                                            <asp:TextBox ID="txt_GO1_Memo" runat="server" CssClass="textBox" Width="50px" TabIndex="9"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="15%">
                                                            <span class="elementLabel">SCHEME No : </span>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="txt_GO1_Scheme_no" runat="server" CssClass="textBox" TabIndex="10"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">DEBIT / CREDIT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Code" runat="server" CssClass="textBox" TabIndex="11"
                                                                Width="20px"  MaxLength="1"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Curr" runat="server" CssClass="textBox" TabIndex="12"
                                                                Width="25px"  MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Amt" runat="server" CssClass="textBox" Width="90px" onkeydown="return validate_Number(event);"
                                                                TabIndex="13" Style="text-align: right" MaxLength="16" ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">CUSTOMER : </span>
                                                        </td>
                                                        <td colspan="4">
                                                            <asp:TextBox ID="txt_GO1_Debit_Cust" runat="server" CssClass="textBox" TabIndex="14"  MaxLength="12"
                                                                 Width="180px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C CODE : </span>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txt_GO1_Debit_Cust_AcCode" runat="server" CssClass="textBox" TabIndex="15"  MaxLength="5"
                                                                Width="90px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C No : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Cust_AccNo" runat="server" CssClass="textBox" TabIndex="16"  MaxLength="12"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="right">
                                                            <span class="elementLabel">EXCH RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Exch_Rate" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                TabIndex="17" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">EXCH CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Exch_CCY" runat="server" CssClass="textBox" MaxLength="3"
                                                                TabIndex="18" Width="25px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">FUND : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_FUND" runat="server" CssClass="textBox" TabIndex="19"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CHECK No. : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Check_No" runat="server" CssClass="textBox" TabIndex="20"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AVAILABLE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Available" runat="server" CssClass="textBox" TabIndex="21"
                                                                Width="90px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">ADVICE PRINT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_AdPrint" runat="server" CssClass="textBox" TabIndex="22"
                                                                Width="20px" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">DETAILS : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Details"  Width="300px" runat="server" CssClass="textBox" TabIndex="23"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">ENTITY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                TabIndex="24" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">DIVISION : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Division" runat="server" CssClass="textBox" TabIndex="25"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Inter_Amount" onkeydown="return validate_Number(event);" runat="server" CssClass="textBox" TabIndex="26"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Inter_Rate"  runat="server" CssClass="textBox" TabIndex="27"
                                                                Width="90px" Style="text-align: right" onkeydown="return validate_Number(event);"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">DEBIT / CREDIT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Code" runat="server" CssClass="textBox" TabIndex="28"  MaxLength="1"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Curr" runat="server" CssClass="textBox" TabIndex="29"  MaxLength="3"
                                                                Width="25px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                                <asp:TextBox ID="txt_GO1_Credit_Amt" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                TabIndex="30" Width="90px" Style="text-align: right" MaxLength="16" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">CUSTOMER : </span>
                                                        </td>
                                                        <td colspan="4">
                                                            <asp:TextBox ID="txt_GO1_Credit_Cust" runat="server" CssClass="textBox" TabIndex="31"  MaxLength="12"
                                                                Width="180px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C CODE : </span>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txt_GO1_Credit_Cust_AcCode" runat="server" CssClass="textBox" TabIndex="32"  MaxLength="5"
                                                                Width="90px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C No : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Cust_AccNo" runat="server" CssClass="textBox" TabIndex="33"  MaxLength="20"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="right">
                                                            <span class="elementLabel">EXCH RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Exch_Rate" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                TabIndex="34" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">EXCH CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Exch_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                TabIndex="35" Width="25px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">FUND : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_FUND" runat="server" CssClass="textBox" TabIndex="36"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CHECK No. : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Check_No" runat="server" CssClass="textBox" TabIndex="37"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AVAILABLE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Available" runat="server" CssClass="textBox" TabIndex="38"
                                                                Width="90px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">ADVICE PRINT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_AdPrint" runat="server" CssClass="textBox" TabIndex="39"
                                                                Width="20px" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">DETAILS : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Details"  Width="300px" runat="server" CssClass="textBox" TabIndex="40"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">ENTITY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                TabIndex="41" Width="90px" Style="text-align: right" MaxLength="3" ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">DIVISION : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Division" runat="server" CssClass="textBox" TabIndex="42"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Inter_Amount" onkeydown="return validate_Number(event);" runat="server" CssClass="textBox" TabIndex="43"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Inter_Rate" onkeydown="return validate_Number(event);" runat="server" CssClass="textBox" TabIndex="44"
                                                                Width="90px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                <tr>
                                                   <td align="left"> 
                                                   <td >
                                                  <asp:Button ID="btnSave" Style="visibility: hidden" runat="server" Text="Save" CssClass="buttonDefault"
                                                   ToolTip="Save" OnClick="btnSave_Click" TabIndex="107" />
                                                   </td>
                                                   </td>
                                               </tr>
                                                <tr style="height: 35px">
                                                    <td align="right">
                                                        <span class="elementLabel">Approve / Reject :</span>
                                                    </td>
                                                    <td align="left" colspan="3" width="90%">
                                                        <asp:DropDownList ID="ddlApproveReject" runat="server" CssClass="dropdownList" TabIndex="45">
                                                            <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Approve"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Reject"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                </ajaxToolkit:TabContainer>
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
