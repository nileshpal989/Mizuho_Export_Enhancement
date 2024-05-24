using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

public partial class EXP_SwiftMessage : System.Web.UI.Page
{
    string _result = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            hdnUserName.Value = Session["userName"].ToString();
            if (!IsPostBack)
            {
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch_SelectedIndexChanged(null, null);
                ddlBranch.Enabled = false;
                ddlSwiftTypes_SelectedIndexChanged(null, null);
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("Expswift_MakerView.aspx", true);
                }
                else
                {
                    fillCurrency();

                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        string no = Request.QueryString["Trans_RefNo"].Trim();
                        if (no != "")
                        {
                            ///////    trans ref not editable   added by shailesh//////
                            txt_999_transRefNo.Enabled = false; txt_742_ClaimBankRef.Enabled = false;txt_754_SenRef.Enabled = false; txt_799_transRefNo.Enabled = false;
                            txt_420_SendingBankTRN.Enabled = false; txt_499_transRefNo.Enabled = false; txt_199_transRefNo.Enabled = false; txt_299_transRefNo.Enabled = false;
                            fillDetails();
                        }
                    }
                }
            }
            txtPrinAmtPaidAccNegoAmt_754.Attributes.Add("onkeydown", "return validate_Number(event);");
            txt_754_AddAmtClamd_Amt.Attributes.Add("onkeydown", "return validate_Number(event);");
            txt_754_TotalAmtClmd_Amt.Attributes.Add("onkeydown", "return validate_Number(event);");
            txt_742_PrinAmtClmd_Amt.Attributes.Add("onkeydown", "return validate_Number(event);");
            txt_742_AddAmtClamd_Amt.Attributes.Add("onkeydown", "return validate_Number(event);");
            txt_742_TotalAmtClmd_Amt.Attributes.Add("onkeydown", "return validate_Number(event);");
            txtAmountTracedAmount_420.Attributes.Add("onkeydown", "return validate_Number(event);");
            txtAmountTracedNoofDaysMonth_420.Attributes.Add("onkeydown", "return validate_Number(event);");
        }
    }
    protected void btn_SaveSwift_Click(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedItem.Text == "----Select----")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select Branch');", true);
            ddlBranch.Focus();

        }
        else if (ddlSwiftTypes.SelectedItem.Text == "----Select----")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select Swift Types');", true);
            ddlSwiftTypes.Focus();
        }
        else
        {
            if (ddlSwiftTypes.SelectedItem.Text == "MT 799")
            {
                SaveView799();
            }

            else if (ddlSwiftTypes.SelectedItem.Text == "MT 742")
            {
                SaveView742();
            }

            else if (ddlSwiftTypes.SelectedItem.Text == "MT 499")
            {
                SaveView499();
            }
            else if (ddlSwiftTypes.SelectedItem.Text == "MT 199")
            {
                SaveView199();
            }
            else if (ddlSwiftTypes.SelectedItem.Text == "MT 299")
            {
                SaveView299();
            }
            else if (ddlSwiftTypes.SelectedItem.Text == "MT 999")
            {
                SaveView999();
            }

            else if (ddlSwiftTypes.SelectedItem.Text == "MT 420")
            {
                SaveView420();
            }

            else if (ddlSwiftTypes.SelectedItem.Text == "MT 754")
            {
                SaveView754();
            }
        }
    }
    protected void btn_View_Swift_Click(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedItem.Text == "----Select----")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select Branch');", true);
            ddlBranch.Focus();

        }
        else if (ddlSwiftTypes.SelectedItem.Text == "----Select----")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select Swift Types');", true);
            ddlSwiftTypes.Focus();
        }
        else
        {
            string SwiftType = ddlSwiftTypes.SelectedItem.Text;

            if (ddlSwiftTypes.SelectedItem.Text == "MT 742")
            {
                Sendtochecker742();
                if (txtReceiver742.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Receiver');", true);
                }

                else if (txt_742_ClaimBankRef.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Claiming Bank Reference');", true);
                }
                else if (txt_742_ClaimBankRef.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Claiming bank Ref contains two consecutive slashes //');", true);
                }
                else if (txt_742_ClaimBankRef.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Claiming bank Ref start with slash(/)');", true);
                }
                else if (txt_742_ClaimBankRef.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Claiming bank Ref end with slash(/)');", true);
                }

                else if (txt_742_DocumCreditNo.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Documentary Credit Number');", true);
                }
                else if (txt_742_DocumCreditNo.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Documentary credit No contains consecutive slashes //');", true);
                }
                else if (txt_742_DocumCreditNo.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Documentary credit No start with slash(/)');", true);
                }
                else if (txt_742_DocumCreditNo.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Documentary credit No end with slash(/)');", true);
                }

                else if (txtIssuingBankIdentifiercode_742.Text == "" && ddl_Issuingbank_742.SelectedValue == "A")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Issuing Bank identifier code');", true);
                    txtIssuingBankIdentifiercode_742.Focus();
                }
                else if (txtIssuingBankName_742.Text == "" && ddl_Issuingbank_742.SelectedValue == "D")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Issuing Bank Name');", true);
                    txtIssuingBankName_742.Focus();
                }

                else if (ddl_742_PrinAmtClmd_Ccy.SelectedItem.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Principle Amount Claimed Currency');", true);
                    ddl_742_PrinAmtClmd_Ccy.Focus();
                }
                else if (txt_742_PrinAmtClmd_Amt.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Principle Amount Claimed Amount');", true);
                    txt_742_PrinAmtClmd_Amt.Focus();
                }

                else if (ddl_742_PrinAmtClmd_Ccy.SelectedItem.Text == "JPY" && txt_742_PrinAmtClmd_Amt.Text.Contains("."))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('If currency is JPY then Decimal is not allowed in amount.');", true);
                    txt_742_PrinAmtClmd_Amt.Focus();
                }
                else if (Regex.IsMatch(txt_742_PrinAmtClmd_Amt.Text, @"\.\d\d\d"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal in Principal Amount Claimed field');", true);
                    txt_742_PrinAmtClmd_Amt.Focus();
                }
                else if (txt_742_PrinAmtClmd_Amt.Text.Contains(','))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please do not enter (,) in Principal amount Claimed field');", true);
                    txt_742_PrinAmtClmd_Amt.Focus();
                }

                else if (ddl_742_AddAmtClamd_Ccy.SelectedItem.Text == "JPY" && txt_742_AddAmtClamd_Amt.Text.Contains("."))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('If currency is JPY then Decimal is not allowed in amount.');", true);
                    txt_742_AddAmtClamd_Amt.Focus();
                }
                else if (Regex.IsMatch(txt_742_AddAmtClamd_Amt.Text, @"\.\d\d\d"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal in additional amount field');", true);
                    txt_742_AddAmtClamd_Amt.Focus();
                }
                else if (txt_742_AddAmtClamd_Amt.Text.Contains(','))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please do not enter (,) in additional amount field');", true);
                    txt_742_AddAmtClamd_Amt.Focus();
                }

                else if (txt_742_TotalAmtClmd_Date.Text == "" && ddlTotalAmtclamd_742.SelectedValue == "A")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Total Amount claimed Date should not be blank');", true);
                    txt_742_TotalAmtClmd_Date.Focus();
                }
                else if (ddl_742_TotalAmtClmd_Ccy.SelectedItem.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Total Amount claimed Currency should not be blank');", true);
                    ddl_742_TotalAmtClmd_Ccy.Focus();
                }
                else if (txt_742_TotalAmtClmd_Amt.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Total Amount claimed Amount should not be blank');", true);
                    txt_742_TotalAmtClmd_Amt.Focus();
                }
                else if (ddl_742_TotalAmtClmd_Ccy.SelectedItem.Text == "JPY" && txt_742_TotalAmtClmd_Amt.Text.Contains("."))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('If currency is JPY then Decimal is not allowed in amount.');", true);
                    txt_742_TotalAmtClmd_Amt.Focus();
                }
                else if (Regex.IsMatch(txt_742_TotalAmtClmd_Amt.Text, @"\.\d\d\d"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal in total amount claimed field');", true);
                    txt_742_TotalAmtClmd_Amt.Focus();
                }

                else if (txt_742_TotalAmtClmd_Amt.Text.Contains(','))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please do not enter (,) in total amount claimed field');", true);
                    txt_742_TotalAmtClmd_Amt.Focus();
                }
                else
                {
                    audittrail("M", txt_742_ClaimBankRef.Text.Trim());
                    string Docno = txt_742_ClaimBankRef.Text.Trim();
                    string url = "TF_EXP_SwiftFileView.aspx?SwiftType=" + SwiftType + "&Docno=" + Docno;
                    string script = "window.open('" + url + "','_blank','height=600,width=1100,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
                }
            }

            if (ddlSwiftTypes.SelectedItem.Text == "MT 754")
            {
                Sendtochecker754();
                if (txtReceiver754.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Receiver');", true);
                }

                else if (txt_754_SenRef.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Sender Reference');", true);
                }
                else if (txt_754_SenRef.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Sender Reference contains two consecutive slashes //');", true);
                }
                else if (txt_754_SenRef.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Sender Reference start with slash(/)');", true);
                }
                else if (txt_754_SenRef.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Sender Reference end with slash(/)');", true);
                }

                else if (txt_754_RelRef.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Related Reference');", true);
                }
                else if (txt_754_RelRef.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference contains two consecutive slashes //');", true);
                }
                else if (txt_754_RelRef.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference start with slash(/)');", true);
                }
                else if (txt_754_RelRef.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference end with slash(/)');", true);
                }

                else if (txtPrinAmtPaidAccNegoDate_754.Text == "" && ddlPrinAmtPaidAccNego_754.SelectedValue == "A")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Principal Amount Date should not be blank');", true);
                }
                else if (ddl_PrinAmtPaidAccNegoCurr_754.SelectedItem.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Principal Amount Currency');", true);
                }
                else if (txtPrinAmtPaidAccNegoAmt_754.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter amount in principle amount field');", true);
                    txtPrinAmtPaidAccNegoAmt_754.Focus();
                }
                else if (ddl_PrinAmtPaidAccNegoCurr_754.SelectedItem.Text == "JPY" && txtPrinAmtPaidAccNegoAmt_754.Text.Contains("."))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('If currency is JPY then Decimal is not allowed in amount.');", true);
                    txtPrinAmtPaidAccNegoAmt_754.Focus();
                }
                else if (Regex.IsMatch(txtPrinAmtPaidAccNegoAmt_754.Text, @"\.\d\d\d"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal in principle amt field');", true);
                    txtPrinAmtPaidAccNegoAmt_754.Focus();
                }
                else if (txtPrinAmtPaidAccNegoAmt_754.Text.Contains(','))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please do not enter (,) in principle amount field');", true);
                    txtPrinAmtPaidAccNegoAmt_754.Focus();
                }
                else if (Regex.IsMatch(txt_754_TotalAmtClmd_Amt.Text, @"\.\d\d\d"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal in Total amount claimed field');", true);
                    txt_754_TotalAmtClmd_Amt.Focus();
                }

                else if (txt_754_TotalAmtClmd_Amt.Text.Contains(','))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please do not enter (,) in Total amount claimed field');", true);
                    txt_754_TotalAmtClmd_Amt.Focus();
                }
                else if (Regex.IsMatch(txt_754_AddAmtClamd_Amt.Text, @"\.\d\d\d"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal in Total amount claimed field');", true);
                    txt_754_AddAmtClamd_Amt.Focus();
                }
                else if (ddl_754_TotalAmtClmd_Ccy.Text != "" && ddl_754_TotalAmtClmd_Ccy.Text != ddl_PrinAmtPaidAccNegoCurr_754.Text)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('The currency code in the fields 32a and 34a must be the same ');", true);
                }
                else if (ddl_754_AddAmtClamd_Ccy.SelectedItem.Text == "JPY" && txt_754_AddAmtClamd_Amt.Text.Contains("."))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('If currency is JPY then Decimal is not allowed in amount.');", true);
                    txt_754_AddAmtClamd_Amt.Focus();
                }
                else if (ddl_754_TotalAmtClmd_Ccy.SelectedItem.Text == "JPY" && txt_754_TotalAmtClmd_Amt.Text.Contains("."))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('If currency is JPY then Decimal is not allowed in amount.');", true);
                    txt_754_TotalAmtClmd_Amt.Focus();
                }

                else if ((txt_754_SenRecInfo1.Text + txt_754_SenRecInfo2.Text + txt_754_SenRecInfo3.Text +
                txt_754_SenRecInfo4.Text + txt_754_SenRecInfo5.Text + txt_754_SenRecInfo6.Text != "") &&
                (txt_754_Narr1.Text + txt_754_Narr2.Text + txt_754_Narr3.Text + txt_754_Narr4.Text + txt_754_Narr5.Text +
                txt_754_Narr6.Text + txt_754_Narr7.Text + txt_754_Narr8.Text + txt_754_Narr9.Text + txt_754_Narr10.Text +
                txt_754_Narr11.Text + txt_754_Narr12.Text + txt_754_Narr13.Text + txt_754_Narr14.Text + txt_754_Narr15.Text +
                txt_754_Narr16.Text + txt_754_Narr17.Text + txt_754_Narr18.Text + txt_754_Narr19.Text + txt_754_Narr20.Text != ""))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 72Z or 77 may be present, but not both');", true);
                }

                else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankIdentifiercode_754.Text != "") &&
               (txtAccountwithBankAccountnumber_754.Text + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankIdentifiercode_754.Text != ""))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
                }

                else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankLocation_754.Text != "") &&
                   (txtAccountwithBankAccountnumber_754.Text + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankLocation_754.Text != ""))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
                }

                else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankName_754.Text +
                    txtReimbursingBankAddress1_754.Text + txtReimbursingBankAddress2_754.Text + txtReimbursingBankAddress3_754.Text != "") &&
                  (txtAccountwithBankAccountnumber_754.Text + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankName_754.Text +
                  txtAccountwithBankAddress1_754.Text + txtAccountwithBankAddress2_754.Text + txtAccountwithBankAddress3_754.Text != ""))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
                }

                else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankIdentifiercode_754.Text != "") &&
                   (txtAccountwithBankAccountnumber_754.Text + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankLocation_754.Text != ""))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
                }

                else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankIdentifiercode_754.Text != "") &&
                   (txtAccountwithBankAccountnumber_754.Text + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankName_754.Text +
                   txtAccountwithBankAddress1_754.Text + txtAccountwithBankAddress2_754.Text + txtAccountwithBankAddress3_754.Text != ""))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
                }

                else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankLocation_754.Text != "") &&
                   (txtAccountwithBankAccountnumber_754.Text + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankIdentifiercode_754.Text != ""))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
                }

                else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankLocation_754.Text != "") &&
                   (txtAccountwithBankAccountnumber_754.Text + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankName_754.Text +
                   txtAccountwithBankAddress1_754.Text + txtAccountwithBankAddress2_754.Text + txtAccountwithBankAddress3_754.Text != ""))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
                }

                else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankName_754.Text +
                    txtReimbursingBankAddress1_754.Text + txtReimbursingBankAddress2_754.Text + txtReimbursingBankAddress3_754.Text != "") &&
                   (txtAccountwithBankAccountnumber_754.Text + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankIdentifiercode_754.Text != ""))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
                }

                else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankName_754.Text +
                    txtReimbursingBankAddress1_754.Text + txtReimbursingBankAddress2_754.Text + txtReimbursingBankAddress3_754.Text != "") && (txtAccountwithBankAccountnumber_754.Text
                    + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankLocation_754.Text != ""))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
                }
                else
                {
                    audittrail("M", txt_754_SenRef.Text.Trim());
                    string Docno = txt_754_SenRef.Text.Trim();
                    string url = "TF_EXP_SwiftFileView.aspx?SwiftType=" + SwiftType + "&Docno=" + Docno;
                    string script = "window.open('" + url + "','_blank','height=600,width=1100,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
                }
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 799")
            {
                Sendtochecker799();
                if (txtReceiver799.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter receiver');", true);
                    txtReceiver799.Focus();
                }
                else if (txt_799_transRefNo.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Transaction Reference Number');", true);
                    txt_799_transRefNo.Focus();
                }
                else if (txt_799_transRefNo.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number contains two consecutive slashes //');", true);
                    txt_799_transRefNo.Focus();
                }
                else if (txt_799_transRefNo.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number start with slash(/)');", true);
                    txt_799_transRefNo.Focus();
                }
                else if (txt_799_transRefNo.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number end with slash(/)');", true);
                    txt_799_transRefNo.Focus();
                }

                else if (txt_799_RelRef.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference contains two consecutive slashes //');", true);
                    txt_799_RelRef.Focus();
                }
                else if (txt_799_RelRef.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference start with slash(/)');", true);
                    txt_799_RelRef.Focus();
                }
                else if (txt_799_RelRef.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference end with slash(/)');", true);
                    txt_799_RelRef.Focus();
                }

                else if (txt_799_Narr1.Text == "" && txt_799_Narr2.Text == "" && txt_799_Narr3.Text == "" && txt_799_Narr4.Text == "" && txt_799_Narr5.Text == "" && txt_799_Narr6.Text == "" && txt_799_Narr7.Text == "" && txt_799_Narr8.Text == "" && txt_799_Narr9.Text == "" && txt_799_Narr10.Text == ""
                && txt_799_Narr11.Text == "" && txt_799_Narr12.Text == "" && txt_799_Narr13.Text == "" && txt_799_Narr14.Text == "" && txt_799_Narr15.Text == "" && txt_799_Narr16.Text == "" && txt_799_Narr17.Text == "" && txt_799_Narr18.Text == "" && txt_799_Narr19.Text == "" && txt_799_Narr20.Text == ""
                && txt_799_Narr21.Text == "" && txt_799_Narr22.Text == "" && txt_799_Narr23.Text == "" && txt_799_Narr24.Text == "" && txt_799_Narr25.Text == "" && txt_799_Narr26.Text == "" && txt_799_Narr27.Text == "" && txt_799_Narr28.Text == "" && txt_799_Narr29.Text == "" && txt_799_Narr30.Text == ""
                && txt_799_Narr31.Text == "" && txt_799_Narr32.Text == "" && txt_799_Narr33.Text == "" && txt_799_Narr34.Text == "" && txt_799_Narr35.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Narrative');", true);
                    txt_799_Narr1.Focus();
                }
                else
                {
                    audittrail("M", txt_799_transRefNo.Text.Trim());
                    string Docno = txt_799_transRefNo.Text.Trim();
                    //string SwiftType = ddlSwiftTypes.SelectedItem.Text;
                    string url = "TF_EXP_SwiftFileView.aspx?SwiftType=" + SwiftType + "&Docno=" + Docno;
                    string script = "window.open('" + url + "','_blank','height=600,width=1100,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
                }
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 420")
            {
                Sendtochecker420();
                if (txtReceiver420.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Receiver');", true);
                    txtReceiver420.Focus();
                }
                else if (txt_420_SendingBankTRN.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Sending Bank TRN');", true);
                }
                else if (txt_420_SendingBankTRN.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Sending Bank TRN contains two consecutive slashes //');", true);
                }
                else if (txt_420_SendingBankTRN.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Sending Bank TRN start with slash(/)');", true);
                }
                else if (txt_420_SendingBankTRN.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Sending Bank TRN end with slash(/)');", true);
                }

                else if (txt_420_RelRef.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Related Reference');", true);
                }
                else if (txt_420_RelRef.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference contains two consecutive slashes //');", true);
                }
                else if (txt_420_RelRef.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference start with slash(/)');", true);
                }
                else if (txt_420_RelRef.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference end with slash(/)');", true);
                }
                else if (Regex.IsMatch(txtAmountTracedAmount_420.Text, @"\.\d\d\d"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal');", true);
                    txtAmountTracedAmount_420.Focus();
                }
                else if (txtAmountTracedAmount_420.Text.Contains(","))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please do not enter (,) in amount field');", true);
                    txtAmountTracedAmount_420.Focus();
                }

                else if (ddl_AmountTracedCurrency_420.SelectedItem.Text == "JPY" && txtAmountTracedAmount_420.Text.Contains("."))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('If currency is JPY then Decimal is not allowed in amount field.');", true);
                    ddl_AmountTracedCurrency_420.Focus();
                }

                else if (ddl_AmountTracedCurrency_420.SelectedItem.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Amount traced currency');", true);
                    ddl_AmountTracedCurrency_420.Focus();
                }
                else if (txtAmountTracedAmount_420.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Amount traced amount');", true);
                    txtAmountTracedAmount_420.Focus();
                }

                else if (ddlAmountTraced_420.SelectedValue == "A" && txtAmountTracedDate_420.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Amount traced date');", true);
                    txtAmountTracedDate_420.Focus();
                }
                else if (ddlAmountTraced_420.SelectedValue == "K" && ddlAmountTracedDayMonth_420.SelectedValue == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Amount traced day month');", true);
                    ddlAmountTracedDayMonth_420.Focus();
                }
                else if (ddlAmountTraced_420.SelectedValue == "K" && txtAmountTracedNoofDaysMonth_420.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Amount traced no of days month');", true);
                    txtAmountTracedNoofDaysMonth_420.Focus();
                }

                else if (ddlAmountTraced_420.SelectedValue == "K" && ddlAmountTracedCode_420.SelectedValue == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Amount traced code');", true);
                    ddlAmountTracedCode_420.Focus();
                }

                else
                {
                    audittrail("M", txt_420_SendingBankTRN.Text.Trim());
                    string Docno = txt_420_SendingBankTRN.Text.Trim();
                    //string SwiftType = ddlSwiftTypes.SelectedItem.Text;
                    string url = "TF_EXP_SwiftFileView.aspx?SwiftType=" + SwiftType + "&Docno=" + Docno;
                    string script = "window.open('" + url + "','_blank','height=600,width=1100,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
                }
            }

            if (ddlSwiftTypes.SelectedItem.Text == "MT 499")
            {
                Sendtochecker499();
                if (txtReceiver499.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter receiver');", true);
                    txtReceiver499.Focus();
                }
                else if (txt_499_transRefNo.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Transaction Reference Number');", true);
                    txt_499_transRefNo.Focus();
                }
                else if (txt_499_transRefNo.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number contains two consecutive slashes //');", true);
                    txt_499_transRefNo.Focus();
                }
                else if (txt_499_transRefNo.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number start with slash(/)');", true);
                    txt_499_transRefNo.Focus();
                }
                else if (txt_499_transRefNo.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number end with slash(/)');", true);
                    txt_499_transRefNo.Focus();
                }

                else if (txt_499_RelRef.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference contains two consecutive slashes //');", true);
                    txt_499_RelRef.Focus();
                }
                else if (txt_499_RelRef.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference start with slash(/)');", true);
                    txt_499_RelRef.Focus();
                }
                else if (txt_499_RelRef.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference end with slash(/)');", true);
                    txt_499_RelRef.Focus();
                }

                else if (txt_499_Narr1.Text == "" && txt_499_Narr2.Text == "" && txt_499_Narr3.Text == "" && txt_499_Narr4.Text == "" && txt_499_Narr5.Text == "" && txt_499_Narr6.Text == "" && txt_499_Narr7.Text == "" && txt_499_Narr8.Text == "" && txt_499_Narr9.Text == "" && txt_499_Narr10.Text == ""
                && txt_499_Narr11.Text == "" && txt_499_Narr12.Text == "" && txt_499_Narr13.Text == "" && txt_499_Narr14.Text == "" && txt_499_Narr15.Text == "" && txt_499_Narr16.Text == "" && txt_499_Narr17.Text == "" && txt_499_Narr18.Text == "" && txt_499_Narr19.Text == "" && txt_499_Narr20.Text == ""
                && txt_499_Narr21.Text == "" && txt_499_Narr22.Text == "" && txt_499_Narr23.Text == "" && txt_499_Narr24.Text == "" && txt_499_Narr25.Text == "" && txt_499_Narr26.Text == "" && txt_499_Narr27.Text == "" && txt_499_Narr28.Text == "" && txt_499_Narr29.Text == "" && txt_499_Narr30.Text == ""
                && txt_499_Narr31.Text == "" && txt_499_Narr32.Text == "" && txt_499_Narr33.Text == "" && txt_499_Narr34.Text == "" && txt_499_Narr35.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Narrative');", true);
                    txt_499_Narr1.Focus();
                }
                else
                {
                    audittrail("M", txt_499_transRefNo.Text.Trim());
                    string Docno = txt_499_transRefNo.Text.Trim();
                    //string SwiftType = ddlSwiftTypes.SelectedItem.Text;
                    string url = "TF_EXP_SwiftFileView.aspx?SwiftType=" + SwiftType + "&Docno=" + Docno;
                    string script = "window.open('" + url + "','_blank','height=600,width=1100,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
                }
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 199")
            {
                Sendtochecker199();
                if (txtReceiver199.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter receiver');", true);
                    txtReceiver199.Focus();
                }
                else if (txt_199_transRefNo.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Transaction Reference Number');", true);
                    txt_199_transRefNo.Focus();
                }
                else if (txt_199_transRefNo.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number contains two consecutive slashes //');", true);
                    txt_199_transRefNo.Focus();
                }
                else if (txt_199_transRefNo.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number start with slash(/)');", true);
                    txt_199_transRefNo.Focus();
                }
                else if (txt_199_transRefNo.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number end with slash(/)');", true);
                    txt_199_transRefNo.Focus();
                }

                else if (txt_199_RelRef.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference contains two consecutive slashes //');", true);
                    txt_199_RelRef.Focus();
                }
                else if (txt_199_RelRef.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference start with slash(/)');", true);
                    txt_199_RelRef.Focus();
                }
                else if (txt_199_RelRef.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference end with slash(/)');", true);
                    txt_199_RelRef.Focus();
                }

                else if (txt_199_Narr1.Text == "" && txt_199_Narr2.Text == "" && txt_199_Narr3.Text == "" && txt_199_Narr4.Text == "" && txt_199_Narr5.Text == "" && txt_199_Narr6.Text == "" && txt_199_Narr7.Text == "" && txt_199_Narr8.Text == "" && txt_199_Narr9.Text == "" && txt_199_Narr10.Text == ""
                && txt_199_Narr11.Text == "" && txt_199_Narr12.Text == "" && txt_199_Narr13.Text == "" && txt_199_Narr14.Text == "" && txt_199_Narr15.Text == "" && txt_199_Narr16.Text == "" && txt_199_Narr17.Text == "" && txt_199_Narr18.Text == "" && txt_199_Narr19.Text == "" && txt_199_Narr20.Text == ""
                && txt_199_Narr21.Text == "" && txt_199_Narr22.Text == "" && txt_199_Narr23.Text == "" && txt_199_Narr24.Text == "" && txt_199_Narr25.Text == "" && txt_199_Narr26.Text == "" && txt_199_Narr27.Text == "" && txt_199_Narr28.Text == "" && txt_199_Narr29.Text == "" && txt_199_Narr30.Text == ""
                && txt_199_Narr31.Text == "" && txt_199_Narr32.Text == "" && txt_199_Narr33.Text == "" && txt_199_Narr34.Text == "" && txt_199_Narr35.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Narrative');", true);
                    txt_199_Narr1.Focus();
                }
                else
                {
                    audittrail("M", txt_199_transRefNo.Text.Trim());
                    string Docno = txt_199_transRefNo.Text.Trim();
                    //string SwiftType = ddlSwiftTypes.SelectedItem.Text;
                    string url = "TF_EXP_SwiftFileView.aspx?SwiftType=" + SwiftType + "&Docno=" + Docno;
                    string script = "window.open('" + url + "','_blank','height=600,width=1100,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
                }
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 299")
            {
                Sendtochecker299();
                if (txtReceiver299.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter receiver');", true);
                    txtReceiver299.Focus();
                }
                else if (txt_299_transRefNo.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Transaction Reference Number');", true);
                    txt_299_transRefNo.Focus();
                }
                else if (txt_299_transRefNo.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number contains two consecutive slashes //');", true);
                    txt_299_transRefNo.Focus();
                }
                else if (txt_299_transRefNo.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number start with slash(/)');", true);
                    txt_299_transRefNo.Focus();
                }
                else if (txt_299_transRefNo.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number end with slash(/)');", true);
                    txt_299_transRefNo.Focus();
                }

                else if (txt_299_RelRef.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference contains two consecutive slashes //');", true);
                    txt_299_RelRef.Focus();
                }
                else if (txt_299_RelRef.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference start with slash(/)');", true);
                    txt_299_RelRef.Focus();
                }
                else if (txt_299_RelRef.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference end with slash(/)');", true);
                    txt_299_RelRef.Focus();
                }

                else if (txt_299_Narr1.Text == "" && txt_299_Narr2.Text == "" && txt_299_Narr3.Text == "" && txt_299_Narr4.Text == "" && txt_299_Narr5.Text == "" && txt_299_Narr6.Text == "" && txt_299_Narr7.Text == "" && txt_299_Narr8.Text == "" && txt_299_Narr9.Text == "" && txt_299_Narr10.Text == ""
                && txt_299_Narr11.Text == "" && txt_299_Narr12.Text == "" && txt_299_Narr13.Text == "" && txt_299_Narr14.Text == "" && txt_299_Narr15.Text == "" && txt_299_Narr16.Text == "" && txt_299_Narr17.Text == "" && txt_299_Narr18.Text == "" && txt_299_Narr19.Text == "" && txt_299_Narr20.Text == ""
                && txt_299_Narr21.Text == "" && txt_299_Narr22.Text == "" && txt_299_Narr23.Text == "" && txt_299_Narr24.Text == "" && txt_299_Narr25.Text == "" && txt_299_Narr26.Text == "" && txt_299_Narr27.Text == "" && txt_299_Narr28.Text == "" && txt_299_Narr29.Text == "" && txt_299_Narr30.Text == ""
                && txt_299_Narr31.Text == "" && txt_299_Narr32.Text == "" && txt_299_Narr33.Text == "" && txt_299_Narr34.Text == "" && txt_299_Narr35.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Narrative');", true);
                    txt_299_Narr1.Focus();
                }
                else
                {
                    audittrail("M", txt_299_transRefNo.Text.Trim());
                    string Docno = txt_299_transRefNo.Text.Trim();
                    //string SwiftType = ddlSwiftTypes.SelectedItem.Text;
                    string url = "TF_EXP_SwiftFileView.aspx?SwiftType=" + SwiftType + "&Docno=" + Docno;
                    string script = "window.open('" + url + "','_blank','height=600,width=1100,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
                }
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 999")
            {
                Sendtochecker999();
                if (txtReceiver999.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter receiver');", true);
                    txtReceiver999.Focus();
                }
                else if (txt_999_transRefNo.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Transaction Reference Number');", true);
                    txt_999_transRefNo.Focus();
                }
                else if (txt_999_transRefNo.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number contains two consecutive slashes //');", true);
                    txt_999_transRefNo.Focus();
                }
                else if (txt_999_transRefNo.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number start with slash(/)');", true);
                    txt_999_transRefNo.Focus();
                }
                else if (txt_999_transRefNo.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number end with slash(/)');", true);
                    txt_999_transRefNo.Focus();
                }

                else if (txt_999_RelRef.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference contains two consecutive slashes //');", true);
                    txt_999_RelRef.Focus();
                }
                else if (txt_999_RelRef.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference start with slash(/)');", true);
                    txt_999_RelRef.Focus();
                }
                else if (txt_999_RelRef.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference end with slash(/)');", true);
                    txt_999_RelRef.Focus();
                }

                else if (txt_999_Narr1.Text == "" && txt_999_Narr2.Text == "" && txt_999_Narr3.Text == "" && txt_999_Narr4.Text == "" && txt_999_Narr5.Text == "" && txt_999_Narr6.Text == "" && txt_999_Narr7.Text == "" && txt_999_Narr8.Text == "" && txt_999_Narr9.Text == "" && txt_999_Narr10.Text == ""
                && txt_999_Narr11.Text == "" && txt_999_Narr12.Text == "" && txt_999_Narr13.Text == "" && txt_999_Narr14.Text == "" && txt_999_Narr15.Text == "" && txt_999_Narr16.Text == "" && txt_999_Narr17.Text == "" && txt_999_Narr18.Text == "" && txt_999_Narr19.Text == "" && txt_999_Narr20.Text == ""
                && txt_999_Narr21.Text == "" && txt_999_Narr22.Text == "" && txt_999_Narr23.Text == "" && txt_999_Narr24.Text == "" && txt_999_Narr25.Text == "" && txt_999_Narr26.Text == "" && txt_999_Narr27.Text == "" && txt_999_Narr28.Text == "" && txt_999_Narr29.Text == "" && txt_999_Narr30.Text == ""
                && txt_999_Narr31.Text == "" && txt_999_Narr32.Text == "" && txt_999_Narr33.Text == "" && txt_999_Narr34.Text == "" && txt_999_Narr35.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Narrative');", true);
                    txt_999_Narr1.Focus();
                }
                else
                {
                    audittrail("M", txt_999_transRefNo.Text.Trim());
                    string Docno = txt_999_transRefNo.Text.Trim();
                    //string SwiftType = ddlSwiftTypes.SelectedItem.Text;
                    string url = "TF_EXP_SwiftFileView.aspx?SwiftType=" + SwiftType + "&Docno=" + Docno;
                    string script = "window.open('" + url + "','_blank','height=600,width=1100,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
                }
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedItem.Text == "----Select----")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select Branch');", true);
            ddlBranch.Focus();

        }
        else if (ddlSwiftTypes.SelectedItem.Text == "----Select----")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select Swift Types');", true);
            ddlSwiftTypes.Focus();
        }
        else
        {
            string Result = "";
            TF_DATA obj = new TF_DATA();
            if (ddlSwiftTypes.SelectedItem.Text == "MT 742")
            {
                Sendtochecker742();
                if (txtReceiver742.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Receiver');", true);
                }

                else if (txt_742_ClaimBankRef.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Claiming Bank Reference');", true);
                }
                else if (txt_742_ClaimBankRef.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Claiming bank Ref contains two consecutive slashes //');", true);
                }
                else if (txt_742_ClaimBankRef.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Claiming bank Ref start with slash(/)');", true);
                }
                else if (txt_742_ClaimBankRef.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Claiming bank Ref end with slash(/)');", true);
                }

                else if (txt_742_DocumCreditNo.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Documentary Credit Number');", true);
                }
                else if (txt_742_DocumCreditNo.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Documentary credit No contains consecutive slashes //');", true);
                }
                else if (txt_742_DocumCreditNo.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Documentary credit No start with slash(/)');", true);
                }
                else if (txt_742_DocumCreditNo.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Documentary credit No end with slash(/)');", true);
                }

                else if (txtIssuingBankIdentifiercode_742.Text == "" && ddl_Issuingbank_742.SelectedValue == "A")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Issuing Bank identifier code');", true);
                    txtIssuingBankIdentifiercode_742.Focus();
                }
                else if (txtIssuingBankName_742.Text == "" && ddl_Issuingbank_742.SelectedValue == "D")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Issuing Bank Name');", true);
                    txtIssuingBankName_742.Focus();
                }

                else if (ddl_742_PrinAmtClmd_Ccy.SelectedItem.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Principle Amount Claimed Currency');", true);
                    ddl_742_PrinAmtClmd_Ccy.Focus();
                }
                else if (txt_742_PrinAmtClmd_Amt.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Principle Amount Claimed Amount');", true);
                    txt_742_PrinAmtClmd_Amt.Focus();
                }

                else if (ddl_742_PrinAmtClmd_Ccy.SelectedItem.Text == "JPY" && txt_742_PrinAmtClmd_Amt.Text.Contains("."))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('If currency is JPY then Decimal is not allowed in amount.');", true);
                    txt_742_PrinAmtClmd_Amt.Focus();
                }
                else if (Regex.IsMatch(txt_742_PrinAmtClmd_Amt.Text, @"\.\d\d\d"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal in Principal Amount Claimed field');", true);
                    txt_742_PrinAmtClmd_Amt.Focus();
                }
                else if (txt_742_PrinAmtClmd_Amt.Text.Contains(','))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please do not enter (,) in Principal amount Claimed field');", true);
                    txt_742_PrinAmtClmd_Amt.Focus();
                }

                else if (ddl_742_AddAmtClamd_Ccy.SelectedItem.Text == "JPY" && txt_742_AddAmtClamd_Amt.Text.Contains("."))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('If currency is JPY then Decimal is not allowed in amount.');", true);
                    txt_742_AddAmtClamd_Amt.Focus();
                }
                else if (Regex.IsMatch(txt_742_AddAmtClamd_Amt.Text, @"\.\d\d\d"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal in additional amount field');", true);
                    txt_742_AddAmtClamd_Amt.Focus();
                }
                else if (txt_742_AddAmtClamd_Amt.Text.Contains(','))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please do not enter (,) in additional amount field');", true);
                    txt_742_AddAmtClamd_Amt.Focus();
                }

                else if (txt_742_TotalAmtClmd_Date.Text == "" && ddlTotalAmtclamd_742.SelectedValue == "A")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Total Amount claimed Date should not be blank');", true);
                    txt_742_TotalAmtClmd_Date.Focus();
                }
                else if (ddl_742_TotalAmtClmd_Ccy.SelectedItem.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Total Amount claimed Currency should not be blank');", true);
                    ddl_742_TotalAmtClmd_Ccy.Focus();
                }
                else if (txt_742_TotalAmtClmd_Amt.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Total Amount claimed Amount should not be blank');", true);
                    txt_742_TotalAmtClmd_Amt.Focus();
                }
                else if (ddl_742_TotalAmtClmd_Ccy.SelectedItem.Text == "JPY" && txt_742_TotalAmtClmd_Amt.Text.Contains("."))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('If currency is JPY then Decimal is not allowed in amount.');", true);
                    txt_742_TotalAmtClmd_Amt.Focus();
                }
                else if (Regex.IsMatch(txt_742_TotalAmtClmd_Amt.Text, @"\.\d\d\d"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal in total amount claimed field');", true);
                    txt_742_TotalAmtClmd_Amt.Focus();
                }

                else if (txt_742_TotalAmtClmd_Amt.Text.Contains(','))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please do not enter (,) in total amount claimed field');", true);
                    txt_742_TotalAmtClmd_Amt.Focus();
                }

                else
                {
                    SqlParameter P1 = new SqlParameter("@Trans_RefNo", txt_742_ClaimBankRef.Text.Trim());
                    SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
                    p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;
                    Result = obj.SaveDeleteData("TF_EXP_ExportSubmitForChecker", P1, p_SwiftType);
                    if (Result == "Submit")
                    {
                        audittrail("C" , txt_742_ClaimBankRef.Text.Trim());
                        string _script = "";
                        _script = "window.location='Expswift_Makerview.aspx?result=Submit'";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                    }
                }
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 754")
            {
                Sendtochecker754();
                if (txtReceiver754.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Receiver');", true);
                }

                else if (txt_754_SenRef.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Sender Reference');", true);
                }
                else if (txt_754_SenRef.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Sender Reference contains two consecutive slashes //');", true);
                }
                else if (txt_754_SenRef.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Sender Reference start with slash(/)');", true);
                }
                else if (txt_754_SenRef.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Sender Reference end with slash(/)');", true);
                }

                else if (txt_754_RelRef.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Related Reference');", true);
                }
                else if (txt_754_RelRef.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference contains two consecutive slashes //');", true);
                }
                else if (txt_754_RelRef.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference start with slash(/)');", true);
                }
                else if (txt_754_RelRef.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference end with slash(/)');", true);
                }

                else if (txtPrinAmtPaidAccNegoDate_754.Text == "" && ddlPrinAmtPaidAccNego_754.SelectedValue == "A")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Principal Amount Date should not be blank');", true);
                }
                else if (ddl_PrinAmtPaidAccNegoCurr_754.SelectedItem.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Principal Amount Currency');", true);
                }
                else if (txtPrinAmtPaidAccNegoAmt_754.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter amount in principle amount field');", true);
                    txtPrinAmtPaidAccNegoAmt_754.Focus();
                }
                else if (ddl_PrinAmtPaidAccNegoCurr_754.SelectedItem.Text == "JPY" && txtPrinAmtPaidAccNegoAmt_754.Text.Contains("."))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('If currency is JPY then Decimal is not allowed in amount.');", true);
                    txtPrinAmtPaidAccNegoAmt_754.Focus();
                }
                else if (Regex.IsMatch(txtPrinAmtPaidAccNegoAmt_754.Text, @"\.\d\d\d"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal in principle amt field');", true);
                    txtPrinAmtPaidAccNegoAmt_754.Focus();
                }
                else if (txtPrinAmtPaidAccNegoAmt_754.Text.Contains(','))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please do not enter (,) in principle amount field');", true);
                    txtPrinAmtPaidAccNegoAmt_754.Focus();
                }

                else if (ddl_754_TotalAmtClmd_Ccy.Text != "" && ddl_754_TotalAmtClmd_Ccy.Text != ddl_PrinAmtPaidAccNegoCurr_754.Text)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('The currency code in the fields 32a and 34a must be the same ');", true);
                }
                else if (Regex.IsMatch(txt_754_TotalAmtClmd_Amt.Text, @"\.\d\d\d"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal in Total amount claimed field');", true);
                    txt_754_TotalAmtClmd_Amt.Focus();
                }

                else if (txt_754_TotalAmtClmd_Amt.Text.Contains(','))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please do not enter (,) in Total amount claimed field');", true);
                    txt_754_TotalAmtClmd_Amt.Focus();
                }
                else if (Regex.IsMatch(txt_754_AddAmtClamd_Amt.Text, @"\.\d\d\d"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal in Total amount claimed field');", true);
                    txt_754_AddAmtClamd_Amt.Focus();
                }

                else if (ddl_754_AddAmtClamd_Ccy.SelectedItem.Text == "JPY" && txt_754_AddAmtClamd_Amt.Text.Contains("."))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('If currency is JPY then Decimal is not allowed in amount.');", true);
                    txt_754_AddAmtClamd_Amt.Focus();
                }
                else if (ddl_754_TotalAmtClmd_Ccy.SelectedItem.Text == "JPY" && txt_754_TotalAmtClmd_Amt.Text.Contains("."))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('If currency is JPY then Decimal is not allowed in amount.');", true);
                    txt_754_TotalAmtClmd_Amt.Focus();
                }

                else if ((txt_754_SenRecInfo1.Text + txt_754_SenRecInfo2.Text + txt_754_SenRecInfo3.Text +
                txt_754_SenRecInfo4.Text + txt_754_SenRecInfo5.Text + txt_754_SenRecInfo6.Text != "") &&
                (txt_754_Narr1.Text + txt_754_Narr2.Text + txt_754_Narr3.Text + txt_754_Narr4.Text + txt_754_Narr5.Text +
                txt_754_Narr6.Text + txt_754_Narr7.Text + txt_754_Narr8.Text + txt_754_Narr9.Text + txt_754_Narr10.Text +
                txt_754_Narr11.Text + txt_754_Narr12.Text + txt_754_Narr13.Text + txt_754_Narr14.Text + txt_754_Narr15.Text +
                txt_754_Narr16.Text + txt_754_Narr17.Text + txt_754_Narr18.Text + txt_754_Narr19.Text + txt_754_Narr20.Text != ""))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 72Z or 77 may be present, but not both');", true);
                }

                else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankIdentifiercode_754.Text != "") &&
               (txtAccountwithBankAccountnumber_754.Text + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankIdentifiercode_754.Text != ""))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
                }

                else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankLocation_754.Text != "") &&
                   (txtAccountwithBankAccountnumber_754.Text + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankLocation_754.Text != ""))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
                }

                else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankName_754.Text +
                    txtReimbursingBankAddress1_754.Text + txtReimbursingBankAddress2_754.Text + txtReimbursingBankAddress3_754.Text != "") &&
                  (txtAccountwithBankAccountnumber_754.Text + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankName_754.Text +
                  txtAccountwithBankAddress1_754.Text + txtAccountwithBankAddress2_754.Text + txtAccountwithBankAddress3_754.Text != ""))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
                }

                else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankIdentifiercode_754.Text != "") &&
                   (txtAccountwithBankAccountnumber_754.Text + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankLocation_754.Text != ""))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
                }

                else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankIdentifiercode_754.Text != "") &&
                   (txtAccountwithBankAccountnumber_754.Text + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankName_754.Text +
                   txtAccountwithBankAddress1_754.Text + txtAccountwithBankAddress2_754.Text + txtAccountwithBankAddress3_754.Text != ""))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
                }

                else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankLocation_754.Text != "") &&
                   (txtAccountwithBankAccountnumber_754.Text + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankIdentifiercode_754.Text != ""))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
                }

                else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankLocation_754.Text != "") &&
                   (txtAccountwithBankAccountnumber_754.Text + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankName_754.Text +
                   txtAccountwithBankAddress1_754.Text + txtAccountwithBankAddress2_754.Text + txtAccountwithBankAddress3_754.Text != ""))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
                }

                else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankName_754.Text +
                    txtReimbursingBankAddress1_754.Text + txtReimbursingBankAddress2_754.Text + txtReimbursingBankAddress3_754.Text != "") &&
                   (txtAccountwithBankAccountnumber_754.Text + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankIdentifiercode_754.Text != ""))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
                }

                else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankName_754.Text +
                    txtReimbursingBankAddress1_754.Text + txtReimbursingBankAddress2_754.Text + txtReimbursingBankAddress3_754.Text != "") && (txtAccountwithBankAccountnumber_754.Text
                    + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankLocation_754.Text != ""))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
                }
                else
                {
                    SqlParameter P1 = new SqlParameter("@Trans_RefNo", txt_754_SenRef.Text.Trim());
                    SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
                    p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;
                    Result = obj.SaveDeleteData("TF_EXP_ExportSubmitForChecker", P1, p_SwiftType);
                    if (Result == "Submit")
                    {
                        audittrail("C",txt_754_SenRef.Text.Trim());
                        string _script = "";
                        _script = "window.location='Expswift_Makerview.aspx?result=Submit'";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                    }
                }
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 799")
            {
                Sendtochecker799();
                if (txtReceiver799.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter receiver');", true);
                    txtReceiver799.Focus();
                }
                else if (txt_799_transRefNo.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Transaction Reference Number');", true);
                    txt_799_transRefNo.Focus();
                }
                else if (txt_799_transRefNo.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number contains two consecutive slashes //');", true);
                    txt_799_transRefNo.Focus();
                }
                else if (txt_799_transRefNo.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number start with slash(/)');", true);
                    txt_799_transRefNo.Focus();
                }
                else if (txt_799_transRefNo.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number end with slash(/)');", true);
                    txt_799_transRefNo.Focus();
                }

                else if (txt_799_RelRef.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference contains two consecutive slashes //');", true);
                    txt_799_RelRef.Focus();
                }
                else if (txt_799_RelRef.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference start with slash(/)');", true);
                    txt_799_RelRef.Focus();
                }
                else if (txt_799_RelRef.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference end with slash(/)');", true);
                    txt_799_RelRef.Focus();
                }

                else if (txt_799_Narr1.Text == "" && txt_799_Narr2.Text == "" && txt_799_Narr3.Text == "" && txt_799_Narr4.Text == "" && txt_799_Narr5.Text == "" && txt_799_Narr6.Text == "" && txt_799_Narr7.Text == "" && txt_799_Narr8.Text == "" && txt_799_Narr9.Text == "" && txt_799_Narr10.Text == ""
                && txt_799_Narr11.Text == "" && txt_799_Narr12.Text == "" && txt_799_Narr13.Text == "" && txt_799_Narr14.Text == "" && txt_799_Narr15.Text == "" && txt_799_Narr16.Text == "" && txt_799_Narr17.Text == "" && txt_799_Narr18.Text == "" && txt_799_Narr19.Text == "" && txt_799_Narr20.Text == ""
                && txt_799_Narr21.Text == "" && txt_799_Narr22.Text == "" && txt_799_Narr23.Text == "" && txt_799_Narr24.Text == "" && txt_799_Narr25.Text == "" && txt_799_Narr26.Text == "" && txt_799_Narr27.Text == "" && txt_799_Narr28.Text == "" && txt_799_Narr29.Text == "" && txt_799_Narr30.Text == ""
                && txt_799_Narr31.Text == "" && txt_799_Narr32.Text == "" && txt_799_Narr33.Text == "" && txt_799_Narr34.Text == "" && txt_799_Narr35.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Narrative');", true);
                    txt_799_Narr1.Focus();
                }
                else
                {
                    SqlParameter P1 = new SqlParameter("@Trans_RefNo", txt_799_transRefNo.Text.Trim());
                    SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
                    p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;
                    Result = obj.SaveDeleteData("TF_EXP_ExportSubmitForChecker", P1, p_SwiftType);
                    if (Result == "Submit")
                    {
                        audittrail("C",txt_799_transRefNo.Text.Trim());
                        string _script = "";
                        _script = "window.location='Expswift_Makerview.aspx?result=Submit'";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                    }
                }
            }

            if (ddlSwiftTypes.SelectedItem.Text == "MT 499")
            {
                Sendtochecker499();
                if (txtReceiver499.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter receiver');", true);
                    txtReceiver499.Focus();
                }
                else if (txt_499_transRefNo.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Transaction Reference Number');", true);
                    txt_499_transRefNo.Focus();
                }
                else if (txt_499_transRefNo.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number contains two consecutive slashes //');", true);
                    txt_499_transRefNo.Focus();
                }
                else if (txt_499_transRefNo.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number start with slash(/)');", true);
                    txt_499_transRefNo.Focus();
                }
                else if (txt_499_transRefNo.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number end with slash(/)');", true);
                    txt_499_transRefNo.Focus();
                }

                else if (txt_499_RelRef.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference contains two consecutive slashes //');", true);
                    txt_499_RelRef.Focus();
                }
                else if (txt_499_RelRef.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference start with slash(/)');", true);
                    txt_499_RelRef.Focus();
                }
                else if (txt_499_RelRef.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference end with slash(/)');", true);
                    txt_499_RelRef.Focus();
                }

                else if (txt_499_Narr1.Text == "" && txt_499_Narr2.Text == "" && txt_499_Narr3.Text == "" && txt_499_Narr4.Text == "" && txt_499_Narr5.Text == "" && txt_499_Narr6.Text == "" && txt_499_Narr7.Text == "" && txt_499_Narr8.Text == "" && txt_499_Narr9.Text == "" && txt_499_Narr10.Text == ""
                && txt_499_Narr11.Text == "" && txt_499_Narr12.Text == "" && txt_499_Narr13.Text == "" && txt_499_Narr14.Text == "" && txt_499_Narr15.Text == "" && txt_499_Narr16.Text == "" && txt_499_Narr17.Text == "" && txt_499_Narr18.Text == "" && txt_499_Narr19.Text == "" && txt_499_Narr20.Text == ""
                && txt_499_Narr21.Text == "" && txt_499_Narr22.Text == "" && txt_499_Narr23.Text == "" && txt_499_Narr24.Text == "" && txt_499_Narr25.Text == "" && txt_499_Narr26.Text == "" && txt_499_Narr27.Text == "" && txt_499_Narr28.Text == "" && txt_499_Narr29.Text == "" && txt_499_Narr30.Text == ""
                && txt_499_Narr31.Text == "" && txt_499_Narr32.Text == "" && txt_499_Narr33.Text == "" && txt_499_Narr34.Text == "" && txt_499_Narr35.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Narrative');", true);
                    txt_499_Narr1.Focus();
                }
                else
                {
                    SqlParameter P1 = new SqlParameter("@Trans_RefNo", txt_499_transRefNo.Text.Trim());
                    SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
                    p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;
                    Result = obj.SaveDeleteData("TF_EXP_ExportSubmitForChecker", P1, p_SwiftType);
                    if (Result == "Submit")
                    {
                        audittrail("C",txt_499_transRefNo.Text.Trim());
                        string _script = "";
                        _script = "window.location='Expswift_Makerview.aspx?result=Submit'";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                    }
                }
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 420")
            {
                Sendtochecker420();
                if (txtReceiver420.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Receiver');", true);
                    txtReceiver420.Focus();
                }
                else if (txt_420_SendingBankTRN.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Sending Bank TRN');", true);
                }
                else if (txt_420_SendingBankTRN.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Sending Bank TRN contains two consecutive slashes //');", true);
                }
                else if (txt_420_SendingBankTRN.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Sending Bank TRN start with slash(/)');", true);
                }
                else if (txt_420_SendingBankTRN.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Sending Bank TRN end with slash(/)');", true);
                }

                else if (txt_420_RelRef.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Related Reference');", true);
                }
                else if (txt_420_RelRef.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference contains two consecutive slashes //');", true);
                }
                else if (txt_420_RelRef.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference start with slash(/)');", true);
                }
                else if (txt_420_RelRef.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference end with slash(/)');", true);
                }

                else if (Regex.IsMatch(txtAmountTracedAmount_420.Text, @"\.\d\d\d"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal');", true);
                    txtAmountTracedAmount_420.Focus();
                }
                else if (txtAmountTracedAmount_420.Text.Contains(","))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please do not enter (,) in amount field');", true);
                    txtAmountTracedAmount_420.Focus();
                }

                else if (ddl_AmountTracedCurrency_420.SelectedItem.Text == "JPY" && txtAmountTracedAmount_420.Text.Contains("."))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('If currency is JPY then Decimal is not allowed in amount field.');", true);
                    ddl_AmountTracedCurrency_420.Focus();
                }

                else if (ddl_AmountTracedCurrency_420.SelectedItem.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Amount traced currency');", true);
                    ddl_AmountTracedCurrency_420.Focus();
                }
                else if (txtAmountTracedAmount_420.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Amount traced amount');", true);
                    txtAmountTracedAmount_420.Focus();
                }

                else if (ddlAmountTraced_420.SelectedValue == "A" && txtAmountTracedDate_420.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Amount traced date');", true);
                    txtAmountTracedDate_420.Focus();
                }
                else if (ddlAmountTraced_420.SelectedValue == "K" && ddlAmountTracedDayMonth_420.SelectedValue == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Amount traced day month');", true);
                    ddlAmountTracedDayMonth_420.Focus();
                }
                else if (ddlAmountTraced_420.SelectedValue == "K" && txtAmountTracedNoofDaysMonth_420.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Amount traced no of days month');", true);
                    txtAmountTracedNoofDaysMonth_420.Focus();
                }

                else if (ddlAmountTraced_420.SelectedValue == "K" && ddlAmountTracedCode_420.SelectedValue == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Amount traced code');", true);
                    ddlAmountTracedCode_420.Focus();
                }

                else
                {
                    SqlParameter P1 = new SqlParameter("@Trans_RefNo", txt_420_SendingBankTRN.Text.Trim());
                    SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
                    p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;
                    Result = obj.SaveDeleteData("TF_EXP_ExportSubmitForChecker", P1, p_SwiftType);
                    if (Result == "Submit")
                    {
                        audittrail("C", txt_420_SendingBankTRN.Text.Trim());
                        string _script = "";
                        _script = "window.location='Expswift_Makerview.aspx?result=Submit'";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                    }
                }
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 199")
            {
                Sendtochecker199();
                if (txtReceiver199.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter receiver');", true);
                    txtReceiver199.Focus();
                }
                else if (txt_199_transRefNo.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Transaction Reference Number');", true);
                    txt_199_transRefNo.Focus();
                }
                else if (txt_199_transRefNo.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number contains two consecutive slashes //');", true);
                    txt_199_transRefNo.Focus();
                }
                else if (txt_199_transRefNo.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number start with slash(/)');", true);
                    txt_199_transRefNo.Focus();
                }
                else if (txt_199_transRefNo.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number end with slash(/)');", true);
                    txt_199_transRefNo.Focus();
                }

                else if (txt_199_RelRef.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference contains two consecutive slashes //');", true);
                    txt_199_RelRef.Focus();
                }
                else if (txt_199_RelRef.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference start with slash(/)');", true);
                    txt_199_RelRef.Focus();
                }
                else if (txt_199_RelRef.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference end with slash(/)');", true);
                    txt_199_RelRef.Focus();
                }

                else if (txt_199_Narr1.Text == "" && txt_199_Narr2.Text == "" && txt_199_Narr3.Text == "" && txt_199_Narr4.Text == "" && txt_199_Narr5.Text == "" && txt_199_Narr6.Text == "" && txt_199_Narr7.Text == "" && txt_199_Narr8.Text == "" && txt_199_Narr9.Text == "" && txt_199_Narr10.Text == ""
                && txt_199_Narr11.Text == "" && txt_199_Narr12.Text == "" && txt_199_Narr13.Text == "" && txt_199_Narr14.Text == "" && txt_199_Narr15.Text == "" && txt_199_Narr16.Text == "" && txt_199_Narr17.Text == "" && txt_199_Narr18.Text == "" && txt_199_Narr19.Text == "" && txt_199_Narr20.Text == ""
                && txt_199_Narr21.Text == "" && txt_199_Narr22.Text == "" && txt_199_Narr23.Text == "" && txt_199_Narr24.Text == "" && txt_199_Narr25.Text == "" && txt_199_Narr26.Text == "" && txt_199_Narr27.Text == "" && txt_199_Narr28.Text == "" && txt_199_Narr29.Text == "" && txt_199_Narr30.Text == ""
                && txt_199_Narr31.Text == "" && txt_199_Narr32.Text == "" && txt_199_Narr33.Text == "" && txt_199_Narr34.Text == "" && txt_199_Narr35.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Narrative');", true);
                    txt_199_Narr1.Focus();
                }
                else
                {
                    SqlParameter P1 = new SqlParameter("@Trans_RefNo", txt_199_transRefNo.Text.Trim());
                    SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
                    p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;
                    Result = obj.SaveDeleteData("TF_EXP_ExportSubmitForChecker", P1, p_SwiftType);
                    if (Result == "Submit")
                    {
                        audittrail("C", txt_199_transRefNo.Text.Trim());
                        string _script = "";
                        _script = "window.location='Expswift_Makerview.aspx?result=Submit'";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                    }
                }
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 299")
            {
                Sendtochecker299();
                if (txtReceiver299.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter receiver');", true);
                    txtReceiver299.Focus();
                }
                else if (txt_299_transRefNo.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Transaction Reference Number');", true);
                    txt_299_transRefNo.Focus();
                }
                else if (txt_299_transRefNo.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number contains two consecutive slashes //');", true);
                    txt_299_transRefNo.Focus();
                }
                else if (txt_299_transRefNo.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number start with slash(/)');", true);
                    txt_299_transRefNo.Focus();
                }
                else if (txt_299_transRefNo.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number end with slash(/)');", true);
                    txt_299_transRefNo.Focus();
                }

                else if (txt_299_RelRef.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference contains two consecutive slashes //');", true);
                    txt_299_RelRef.Focus();
                }
                else if (txt_299_RelRef.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference start with slash(/)');", true);
                    txt_299_RelRef.Focus();
                }
                else if (txt_299_RelRef.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference end with slash(/)');", true);
                    txt_299_RelRef.Focus();
                }

                else if (txt_299_Narr1.Text == "" && txt_299_Narr2.Text == "" && txt_299_Narr3.Text == "" && txt_299_Narr4.Text == "" && txt_299_Narr5.Text == "" && txt_299_Narr6.Text == "" && txt_299_Narr7.Text == "" && txt_299_Narr8.Text == "" && txt_299_Narr9.Text == "" && txt_299_Narr10.Text == ""
                && txt_299_Narr11.Text == "" && txt_299_Narr12.Text == "" && txt_299_Narr13.Text == "" && txt_299_Narr14.Text == "" && txt_299_Narr15.Text == "" && txt_299_Narr16.Text == "" && txt_299_Narr17.Text == "" && txt_299_Narr18.Text == "" && txt_299_Narr19.Text == "" && txt_299_Narr20.Text == ""
                && txt_299_Narr21.Text == "" && txt_299_Narr22.Text == "" && txt_299_Narr23.Text == "" && txt_299_Narr24.Text == "" && txt_299_Narr25.Text == "" && txt_299_Narr26.Text == "" && txt_299_Narr27.Text == "" && txt_299_Narr28.Text == "" && txt_299_Narr29.Text == "" && txt_299_Narr30.Text == ""
                && txt_299_Narr31.Text == "" && txt_299_Narr32.Text == "" && txt_299_Narr33.Text == "" && txt_299_Narr34.Text == "" && txt_299_Narr35.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Narrative');", true);
                    txt_299_Narr1.Focus();
                }
                else
                {
                    SqlParameter P1 = new SqlParameter("@Trans_RefNo", txt_299_transRefNo.Text.Trim());
                    SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
                    p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;
                    Result = obj.SaveDeleteData("TF_EXP_ExportSubmitForChecker", P1, p_SwiftType);
                    if (Result == "Submit")
                    {
                        audittrail("C", txt_299_transRefNo.Text.Trim());
                        string _script = "";
                        _script = "window.location='Expswift_Makerview.aspx?result=Submit'";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                    }
                }
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 999")
            {
                Sendtochecker999();
                if (txtReceiver999.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter receiver');", true);
                    txtReceiver999.Focus();
                }
                else if (txt_999_transRefNo.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Transaction Reference Number');", true);
                    txt_999_transRefNo.Focus();
                }
                else if (txt_999_transRefNo.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number contains two consecutive slashes //');", true);
                    txt_999_transRefNo.Focus();
                }
                else if (txt_999_transRefNo.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number start with slash(/)');", true);
                    txt_999_transRefNo.Focus();
                }
                else if (txt_999_transRefNo.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number end with slash(/)');", true);
                    txt_999_transRefNo.Focus();
                }

                else if (txt_999_RelRef.Text.Contains("//"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference contains two consecutive slashes //');", true);
                    txt_999_RelRef.Focus();
                }
                else if (txt_999_RelRef.Text.StartsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference start with slash(/)');", true);
                    txt_999_RelRef.Focus();
                }
                else if (txt_999_RelRef.Text.EndsWith("/"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference end with slash(/)');", true);
                    txt_999_RelRef.Focus();
                }

                else if (txt_999_Narr1.Text == "" && txt_999_Narr2.Text == "" && txt_999_Narr3.Text == "" && txt_999_Narr4.Text == "" && txt_999_Narr5.Text == "" && txt_999_Narr6.Text == "" && txt_999_Narr7.Text == "" && txt_999_Narr8.Text == "" && txt_999_Narr9.Text == "" && txt_999_Narr10.Text == ""
                && txt_999_Narr11.Text == "" && txt_999_Narr12.Text == "" && txt_999_Narr13.Text == "" && txt_999_Narr14.Text == "" && txt_999_Narr15.Text == "" && txt_999_Narr16.Text == "" && txt_999_Narr17.Text == "" && txt_999_Narr18.Text == "" && txt_999_Narr19.Text == "" && txt_999_Narr20.Text == ""
                && txt_999_Narr21.Text == "" && txt_999_Narr22.Text == "" && txt_999_Narr23.Text == "" && txt_999_Narr24.Text == "" && txt_999_Narr25.Text == "" && txt_999_Narr26.Text == "" && txt_999_Narr27.Text == "" && txt_999_Narr28.Text == "" && txt_999_Narr29.Text == "" && txt_999_Narr30.Text == ""
                && txt_999_Narr31.Text == "" && txt_999_Narr32.Text == "" && txt_999_Narr33.Text == "" && txt_999_Narr34.Text == "" && txt_999_Narr35.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Narrative');", true);
                    txt_999_Narr1.Focus();
                }
                else
                {
                    SqlParameter P1 = new SqlParameter("@Trans_RefNo", txt_999_transRefNo.Text.Trim());
                    SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
                    p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;
                    Result = obj.SaveDeleteData("TF_EXP_ExportSubmitForChecker", P1, p_SwiftType);
                    if (Result == "Submit")
                    {
                        audittrail("C", txt_999_transRefNo.Text.Trim());
                        string _script = "";
                        _script = "window.location='Expswift_Makerview.aspx?result=Submit'";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                    }
                }
            }
        }
    }
    public void fillDetails()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@Trans_RefNo", Request.QueryString["Trans_RefNo"].ToString());
        SqlParameter PSwifttype = new SqlParameter("@Swift_Type", Request.QueryString["Swift_Type"].ToString());

        DataTable dt = new DataTable();
        dt = obj.getData("TF_EXP_SwiftViewDetails", PDocNo, PSwifttype);

        if (dt.Rows.Count > 0)
        {
            TabContainerMain.Visible = true;
            TabSwifts.Visible = true;
            ddlSwiftTypes.SelectedItem.Text = dt.Rows[0]["Swift_Type"].ToString();
            ddlBranch.SelectedItem.Text = dt.Rows[0]["Branch"].ToString();
            ddlSwiftTypes.Enabled = false;
            //=========================742 ===================//
            if (ddlSwiftTypes.SelectedItem.Text == "MT 742")
            {
                Panel_742.Visible = true;
                txtReceiver742.Text = dt.Rows[0]["Receiver"].ToString();
                txt_742_ClaimBankRef.Text = dt.Rows[0]["Trans_RefNo"].ToString();
                txt_742_DocumCreditNo.Text = dt.Rows[0]["Related_Ref"].ToString();
                txt_742_Dateofissue.Text = dt.Rows[0]["Dateofissue"].ToString();

                ddl_Issuingbank_742.SelectedValue = dt.Rows[0]["ddlIssuingBank"].ToString();
                txtIssuingBankAccountnumber_742.Text = dt.Rows[0]["IssuingBank_PartyIdent"].ToString();
                txtIssuingBankAccountnumber1_742.Text = dt.Rows[0]["IssuingBank_PartyIdent1"].ToString();
                txtIssuingBankIdentifiercode_742.Text = dt.Rows[0]["IssuingBank_Identcode"].ToString();
                txtIssuingBankName_742.Text = dt.Rows[0]["IssuingBank_Name"].ToString();
                txtIssuingBankAddress1_742.Text = dt.Rows[0]["IssuingBank_Add1"].ToString();
                txtIssuingBankAddress2_742.Text = dt.Rows[0]["IssuingBank_Add2"].ToString();
                txtIssuingBankAddress3_742.Text = dt.Rows[0]["IssuingBank_Add3"].ToString();
                ddl_Issuingbank_742_TextChanged(null, null);

                ddl_742_PrinAmtClmd_Ccy.ClearSelection();
                ddl_742_PrinAmtClmd_Ccy.SelectedValue = dt.Rows[0]["PrincipalAmtClaimed_Curr"].ToString();
                txt_742_PrinAmtClmd_Amt.Text = dt.Rows[0]["PrincipalAmtClaimed_Amt"].ToString();

                ddl_742_AddAmtClamd_Ccy.ClearSelection();
                ddl_742_AddAmtClamd_Ccy.SelectedValue = dt.Rows[0]["AddnlAmt_Curr"].ToString();

                txt_742_AddAmtClamd_Amt.Text = dt.Rows[0]["AddnlAmt_Amt"].ToString();

                txt_742_Charges1.Text = dt.Rows[0]["Charges1"].ToString();
                txt_742_Charges2.Text = dt.Rows[0]["Charges2"].ToString();
                txt_742_Charges3.Text = dt.Rows[0]["Charges3"].ToString();
                txt_742_Charges4.Text = dt.Rows[0]["Charges4"].ToString();
                txt_742_Charges5.Text = dt.Rows[0]["Charges5"].ToString();
                txt_742_Charges6.Text = dt.Rows[0]["Charges6"].ToString();

                ddlTotalAmtclamd_742.ClearSelection();
                ddlTotalAmtclamd_742.SelectedValue = dt.Rows[0]["ddlTotalAmtClaimed"].ToString();

                txt_742_TotalAmtClmd_Date.Text = dt.Rows[0]["TotalAmtClaimed_Date"].ToString();

                ddl_742_TotalAmtClmd_Ccy.ClearSelection();
                ddl_742_TotalAmtClmd_Ccy.Text = dt.Rows[0]["TotalAmtClaimed_Curr"].ToString();
                txt_742_TotalAmtClmd_Amt.Text = dt.Rows[0]["TotalAmtClaimed_Amt"].ToString();
                ddlTotalAmtclamd_742_TextChanged(null, null);

                ddlAccountwithbank_742.ClearSelection();
                ddlAccountwithbank_742.SelectedValue = dt.Rows[0]["ddlAccWithBank"].ToString();
                txtAccountwithBankAccountnumber_742.Text = dt.Rows[0]["AccWithBank_PartyIdent"].ToString();
                txtAccountwithBankAccountnumber1_742.Text = dt.Rows[0]["AccWithBank_PartyIdent1"].ToString();
                txtAccountwithBankIdentifiercode_742.Text = dt.Rows[0]["AccWithBank_Identcode"].ToString();
                txtAccountwithBankLocation_742.Text = dt.Rows[0]["AccWithBank_Location"].ToString();
                txtAccountwithBankName_742.Text = dt.Rows[0]["AccWithBank_Name"].ToString();
                txtAccountwithBankAddress1_742.Text = dt.Rows[0]["AccWithBank_Add1"].ToString();
                txtAccountwithBankAddress2_742.Text = dt.Rows[0]["AccWithBank_Add2"].ToString();
                txtAccountwithBankAddress3_742.Text = dt.Rows[0]["AccWithBank_Add3"].ToString();
                ddlAccountwithbank_742_TextChanged(null, null);

                ddlBeneficiarybank_742.ClearSelection();
                ddlBeneficiarybank_742.SelectedValue = dt.Rows[0]["ddlBeneficiaryBank"].ToString();
                txtBeneficiaryBankAccountnumber_742.Text = dt.Rows[0]["BeneficiaryBank_PartyIdent"].ToString();
                txtBeneficiaryBankAccountnumber1_742.Text = dt.Rows[0]["BeneficiaryBank_PartyIdent1"].ToString();
                txtBeneficiaryBankIdentifiercode_742.Text = dt.Rows[0]["BeneficiaryBank_Identcode"].ToString();
                txtBeneficiaryBankName_742.Text = dt.Rows[0]["BeneficiaryBank_Name"].ToString();
                txtBeneficiaryBankAddress1_742.Text = dt.Rows[0]["BeneficiaryBank_Add1"].ToString();
                txtBeneficiaryBankAddress2_742.Text = dt.Rows[0]["BeneficiaryBank_Add2"].ToString();
                txtBeneficiaryBankAddress3_742.Text = dt.Rows[0]["BeneficiaryBank_Add3"].ToString();
                ddlBeneficiarybank_742_TextChanged(null, null);

                txt_742_SenRecInfo1.Text = dt.Rows[0]["SenToRecinfo1"].ToString();
                txt_742_SenRecInfo2.Text = dt.Rows[0]["SenToRecinfo2"].ToString();
                txt_742_SenRecInfo3.Text = dt.Rows[0]["SenToRecinfo3"].ToString();
                txt_742_SenRecInfo4.Text = dt.Rows[0]["SenToRecinfo4"].ToString();
                txt_742_SenRecInfo5.Text = dt.Rows[0]["SenToRecinfo5"].ToString();
                txt_742_SenRecInfo6.Text = dt.Rows[0]["SenToRecinfo6"].ToString();

                if (dt.Rows[0]["Reject_Remarks"].ToString() != "")
                {
                    lblChecker_Remark.Text = "Checker Remark :- " + dt.Rows[0]["Reject_Remarks"].ToString();
                }
            }
            ////=========================742 end===================//
            ////=========================420 start===================//

            if (ddlSwiftTypes.SelectedItem.Text == "MT 420")
            {
                Panel_420.Visible = true;
                txtReceiver420.Text = dt.Rows[0]["Receiver"].ToString();
                txt_420_SendingBankTRN.Text = dt.Rows[0]["Trans_RefNo"].ToString();
                txt_420_RelRef.Text = dt.Rows[0]["Related_Ref"].ToString();

                ddlAmountTraced_420.ClearSelection();
                ddlAmountTraced_420.SelectedValue = dt.Rows[0]["ddlAmtTraced"].ToString();
                txtAmountTracedAmount_420.Text = dt.Rows[0]["TracedAmt"].ToString();
                ddlAmountTracedCode_420.SelectedValue = dt.Rows[0]["Tracedcode"].ToString();
                ddl_AmountTracedCurrency_420.SelectedValue = dt.Rows[0]["Tracedcurr"].ToString();
                txtAmountTracedDate_420.Text = dt.Rows[0]["TracedDate"].ToString();
                ddlAmountTracedDayMonth_420.SelectedValue = dt.Rows[0]["TracedDayMonth"].ToString();
                txtAmountTracedNoofDaysMonth_420.Text = dt.Rows[0]["TracedNoofDayMonth"].ToString();
                ddlAmountTraced_420_TextChanged(null, null);

                txt_420_DateofCollnInstruction.Text = dt.Rows[0]["DateofCollnInstruction"].ToString();

                txt_420_DraweeAccount.Text = dt.Rows[0]["DraweeAccount"].ToString();
                txt_420_DraweeName.Text = dt.Rows[0]["DraweeName"].ToString();
                txt_420_DraweeAdd1.Text = dt.Rows[0]["DraweeAdd1"].ToString();
                txt_420_DraweeAdd2.Text = dt.Rows[0]["DraweeAdd2"].ToString();
                txt_420_DraweeAdd3.Text = dt.Rows[0]["DraweeAdd3"].ToString();

                txt_420_SenToRecinfo1.Text = dt.Rows[0]["SenToRecinfo1"].ToString();
                txt_420_SenToRecinfo2.Text = dt.Rows[0]["SenToRecinfo2"].ToString();
                txt_420_SenToRecinfo3.Text = dt.Rows[0]["SenToRecinfo3"].ToString();
                txt_420_SenToRecinfo4.Text = dt.Rows[0]["SenToRecinfo4"].ToString();
                txt_420_SenToRecinfo5.Text = dt.Rows[0]["SenToRecinfo5"].ToString();
                txt_420_SenToRecinfo6.Text = dt.Rows[0]["SenToRecinfo6"].ToString();

                if (dt.Rows[0]["Reject_Remarks"].ToString() != "")
                {
                    lblChecker_Remark.Text = "Checker Remark :- " + dt.Rows[0]["Reject_Remarks"].ToString();
                }
            }
            ////=========================================420 end================================================//
            ////========================================754 ===================================================//
            if (ddlSwiftTypes.SelectedItem.Text == "MT 754")
            {
                Panel_754.Visible = true;
                txtReceiver754.Text = dt.Rows[0]["Receiver"].ToString();
                txt_754_SenRef.Text = dt.Rows[0]["Trans_RefNo"].ToString();
                txt_754_RelRef.Text = dt.Rows[0]["Related_Ref"].ToString();

                ddlPrinAmtPaidAccNego_754.ClearSelection();
                ddlPrinAmtPaidAccNego_754.SelectedValue = dt.Rows[0]["ddlPrincipalAmtPaidAccptdNego"].ToString();
                txtPrinAmtPaidAccNegoDate_754.Text = dt.Rows[0]["PrincipalAmtPaidAccptdNego_Date"].ToString();

                ddl_PrinAmtPaidAccNegoCurr_754.ClearSelection();
                ddl_PrinAmtPaidAccNegoCurr_754.Text = dt.Rows[0]["PrincipalAmtPaidAccptdNego_Curr"].ToString();
                txtPrinAmtPaidAccNegoAmt_754.Text = dt.Rows[0]["PrincipalAmtPaidAccptdNego_Amt"].ToString();
                ddlPrinAmtPaidAccNego_754_TextChanged(null, null);

                ddl_754_AddAmtClamd_Ccy.ClearSelection();
                ddl_754_AddAmtClamd_Ccy.Text = dt.Rows[0]["AddnlAmt_Curr"].ToString();
                txt_754_AddAmtClamd_Amt.Text = dt.Rows[0]["AddnlAmt_Amt"].ToString();

                txt_754_ChargesDeduct1.Text = dt.Rows[0]["Charges1"].ToString();
                txt_754_ChargesDeduct2.Text = dt.Rows[0]["Charges2"].ToString();
                txt_754_ChargesDeduct3.Text = dt.Rows[0]["Charges3"].ToString();
                txt_754_ChargesDeduct4.Text = dt.Rows[0]["Charges4"].ToString();
                txt_754_ChargesDeduct5.Text = dt.Rows[0]["Charges5"].ToString();
                txt_754_ChargesDeduct6.Text = dt.Rows[0]["Charges6"].ToString();

                txt_754_ChargesAdded1.Text = dt.Rows[0]["ChargesAdded1"].ToString();
                txt_754_ChargesAdded2.Text = dt.Rows[0]["ChargesAdded2"].ToString();
                txt_754_ChargesAdded3.Text = dt.Rows[0]["ChargesAdded3"].ToString();
                txt_754_ChargesAdded4.Text = dt.Rows[0]["ChargesAdded4"].ToString();
                txt_754_ChargesAdded5.Text = dt.Rows[0]["ChargesAdded5"].ToString();
                txt_754_ChargesAdded6.Text = dt.Rows[0]["ChargesAdded6"].ToString();

                ddlTotalAmtclamd_754.ClearSelection();
                ddlTotalAmtclamd_754.SelectedValue = dt.Rows[0]["ddlTotalAmtClaimed"].ToString(); ;
                txt_754_TotalAmtClmd_Date.Text = dt.Rows[0]["TotalAmtClaimed_Date"].ToString();

                ddl_754_TotalAmtClmd_Ccy.ClearSelection();
                ddl_754_TotalAmtClmd_Ccy.Text = dt.Rows[0]["TotalAmtClaimed_Curr"].ToString();
                txt_754_TotalAmtClmd_Amt.Text = dt.Rows[0]["TotalAmtClaimed_Amt"].ToString();
                ddlTotalAmtclamd_754_TextChanged(null, null);

                ddlReimbursingbank_754.ClearSelection();
                ddlReimbursingbank_754.SelectedValue = dt.Rows[0]["ddlReimbursingBank"].ToString();
                txtReimbursingBankAccountnumber_754.Text = dt.Rows[0]["ReimbursingBank_PartyIdent"].ToString();
                txtReimbursingBankAccountnumber1_754.Text = dt.Rows[0]["ReimbursingBank_PartyIdent1"].ToString();
                txtReimbursingBankIdentifiercode_754.Text = dt.Rows[0]["ReimbursingBank_Identcode"].ToString();
                txtReimbursingBankLocation_754.Text = dt.Rows[0]["ReimbursingBank_Location"].ToString();
                txtReimbursingBankName_754.Text = dt.Rows[0]["ReimbursingBank_Name"].ToString();
                txtReimbursingBankAddress1_754.Text = dt.Rows[0]["ReimbursingBank_Add1"].ToString();
                txtReimbursingBankAddress2_754.Text = dt.Rows[0]["ReimbursingBank_Add2"].ToString();
                txtReimbursingBankAddress3_754.Text = dt.Rows[0]["ReimbursingBank_Add3"].ToString();
                ddlReimbursingbank_754_TextChanged(null, null);

                ddlAccountwithbank_754.ClearSelection();
                ddlAccountwithbank_754.SelectedValue = dt.Rows[0]["ddlAccWithBank"].ToString();
                txtAccountwithBankAccountnumber_754.Text = dt.Rows[0]["AccWithBank_PartyIdent"].ToString();
                txtAccountwithBankAccountnumber1_754.Text = dt.Rows[0]["AccWithBank_PartyIdent1"].ToString();
                txtAccountwithBankIdentifiercode_754.Text = dt.Rows[0]["AccWithBank_Identcode"].ToString();
                txtAccountwithBankLocation_754.Text = dt.Rows[0]["AccWithBank_Location"].ToString();
                txtAccountwithBankName_754.Text = dt.Rows[0]["AccWithBank_Name"].ToString();
                txtAccountwithBankAddress1_754.Text = dt.Rows[0]["AccWithBank_Add1"].ToString();
                txtAccountwithBankAddress2_754.Text = dt.Rows[0]["AccWithBank_Add2"].ToString();
                txtAccountwithBankAddress3_754.Text = dt.Rows[0]["AccWithBank_Add3"].ToString();
                ddlAccountwithbank_754_TextChanged(null, null);

                ddlBeneficiarybank_754.ClearSelection();
                ddlBeneficiarybank_754.SelectedValue = dt.Rows[0]["ddlBeneficiaryBank"].ToString();
                txtBeneficiaryBankAccountnumber_754.Text = dt.Rows[0]["BeneficiaryBank_PartyIdent"].ToString();
                txtBeneficiaryBankAccountnumber1_754.Text = dt.Rows[0]["BeneficiaryBank_PartyIdent1"].ToString();
                txtBeneficiaryBankIdentifiercode_754.Text = dt.Rows[0]["BeneficiaryBank_Identcode"].ToString();
                txtBeneficiaryBankName_754.Text = dt.Rows[0]["BeneficiaryBank_Name"].ToString();
                txtBeneficiaryBankAddress1_754.Text = dt.Rows[0]["BeneficiaryBank_Add1"].ToString();
                txtBeneficiaryBankAddress2_754.Text = dt.Rows[0]["BeneficiaryBank_Add2"].ToString();
                txtBeneficiaryBankAddress3_754.Text = dt.Rows[0]["BeneficiaryBank_Add3"].ToString();
                ddlBeneficiarybank_754_TextChanged(null, null);

                txt_754_SenRecInfo1.Text = dt.Rows[0]["SenToRecinfo1"].ToString();
                txt_754_SenRecInfo2.Text = dt.Rows[0]["SenToRecinfo2"].ToString();
                txt_754_SenRecInfo3.Text = dt.Rows[0]["SenToRecinfo3"].ToString();
                txt_754_SenRecInfo4.Text = dt.Rows[0]["SenToRecinfo4"].ToString();
                txt_754_SenRecInfo5.Text = dt.Rows[0]["SenToRecinfo5"].ToString();
                txt_754_SenRecInfo6.Text = dt.Rows[0]["SenToRecinfo6"].ToString();

                txt_754_Narr1.Text = dt.Rows[0]["Narrative1"].ToString();
                txt_754_Narr2.Text = dt.Rows[0]["Narrative2"].ToString();
                txt_754_Narr3.Text = dt.Rows[0]["Narrative3"].ToString();
                txt_754_Narr4.Text = dt.Rows[0]["Narrative4"].ToString();
                txt_754_Narr5.Text = dt.Rows[0]["Narrative5"].ToString();
                txt_754_Narr6.Text = dt.Rows[0]["Narrative6"].ToString();
                txt_754_Narr7.Text = dt.Rows[0]["Narrative7"].ToString();
                txt_754_Narr8.Text = dt.Rows[0]["Narrative8"].ToString();
                txt_754_Narr9.Text = dt.Rows[0]["Narrative9"].ToString();
                txt_754_Narr10.Text = dt.Rows[0]["Narrative10"].ToString();
                txt_754_Narr11.Text = dt.Rows[0]["Narrative11"].ToString();
                txt_754_Narr12.Text = dt.Rows[0]["Narrative12"].ToString();
                txt_754_Narr13.Text = dt.Rows[0]["Narrative13"].ToString();
                txt_754_Narr14.Text = dt.Rows[0]["Narrative14"].ToString();
                txt_754_Narr15.Text = dt.Rows[0]["Narrative15"].ToString();
                txt_754_Narr16.Text = dt.Rows[0]["Narrative16"].ToString();
                txt_754_Narr17.Text = dt.Rows[0]["Narrative17"].ToString();
                txt_754_Narr18.Text = dt.Rows[0]["Narrative18"].ToString();
                txt_754_Narr19.Text = dt.Rows[0]["Narrative19"].ToString();
                txt_754_Narr20.Text = dt.Rows[0]["Narrative20"].ToString();

                if (dt.Rows[0]["Reject_Remarks"].ToString() != "")
                {
                    lblChecker_Remark.Text = "Checker Remark :- " + dt.Rows[0]["Reject_Remarks"].ToString();
                }
            }
            ////=========================754 end===================//
            if (ddlSwiftTypes.SelectedItem.Text == "MT 499")
            {
                Panel_499.Visible = true;
                txtReceiver499.Text = dt.Rows[0]["Receiver"].ToString();
                txt_499_transRefNo.Text = dt.Rows[0]["Trans_RefNo"].ToString();
                txt_499_RelRef.Text = dt.Rows[0]["Related_Ref"].ToString();
                txt_499_Narr1.Text = dt.Rows[0]["Narrative1"].ToString();
                txt_499_Narr2.Text = dt.Rows[0]["Narrative2"].ToString();
                txt_499_Narr3.Text = dt.Rows[0]["Narrative3"].ToString();
                txt_499_Narr4.Text = dt.Rows[0]["Narrative4"].ToString();
                txt_499_Narr5.Text = dt.Rows[0]["Narrative5"].ToString();
                txt_499_Narr6.Text = dt.Rows[0]["Narrative6"].ToString();
                txt_499_Narr7.Text = dt.Rows[0]["Narrative7"].ToString();
                txt_499_Narr8.Text = dt.Rows[0]["Narrative8"].ToString();
                txt_499_Narr9.Text = dt.Rows[0]["Narrative9"].ToString();
                txt_499_Narr10.Text = dt.Rows[0]["Narrative10"].ToString();
                txt_499_Narr11.Text = dt.Rows[0]["Narrative11"].ToString();
                txt_499_Narr12.Text = dt.Rows[0]["Narrative12"].ToString();
                txt_499_Narr13.Text = dt.Rows[0]["Narrative13"].ToString();
                txt_499_Narr14.Text = dt.Rows[0]["Narrative14"].ToString();
                txt_499_Narr15.Text = dt.Rows[0]["Narrative15"].ToString();
                txt_499_Narr16.Text = dt.Rows[0]["Narrative16"].ToString();
                txt_499_Narr17.Text = dt.Rows[0]["Narrative17"].ToString();
                txt_499_Narr18.Text = dt.Rows[0]["Narrative18"].ToString();
                txt_499_Narr19.Text = dt.Rows[0]["Narrative19"].ToString();
                txt_499_Narr20.Text = dt.Rows[0]["Narrative20"].ToString();
                txt_499_Narr21.Text = dt.Rows[0]["Narrative21"].ToString();
                txt_499_Narr22.Text = dt.Rows[0]["Narrative22"].ToString();
                txt_499_Narr23.Text = dt.Rows[0]["Narrative23"].ToString();
                txt_499_Narr24.Text = dt.Rows[0]["Narrative24"].ToString();
                txt_499_Narr25.Text = dt.Rows[0]["Narrative25"].ToString();
                txt_499_Narr26.Text = dt.Rows[0]["Narrative26"].ToString();
                txt_499_Narr27.Text = dt.Rows[0]["Narrative27"].ToString();
                txt_499_Narr28.Text = dt.Rows[0]["Narrative28"].ToString();
                txt_499_Narr29.Text = dt.Rows[0]["Narrative29"].ToString();
                txt_499_Narr30.Text = dt.Rows[0]["Narrative30"].ToString();
                txt_499_Narr31.Text = dt.Rows[0]["Narrative31"].ToString();
                txt_499_Narr32.Text = dt.Rows[0]["Narrative32"].ToString();
                txt_499_Narr33.Text = dt.Rows[0]["Narrative33"].ToString();
                txt_499_Narr34.Text = dt.Rows[0]["Narrative34"].ToString();
                txt_499_Narr35.Text = dt.Rows[0]["Narrative35"].ToString();

                if (dt.Rows[0]["Reject_Remarks"].ToString() != "")
                {
                    lblChecker_Remark.Text = "Checker Remark :- " + dt.Rows[0]["Reject_Remarks"].ToString();
                }
            }

            if (ddlSwiftTypes.SelectedItem.Text == "MT 199")
            {
                Panel_199.Visible = true;
                txtReceiver199.Text = dt.Rows[0]["Receiver"].ToString();
                txt_199_transRefNo.Text = dt.Rows[0]["Trans_RefNo"].ToString();
                txt_199_RelRef.Text = dt.Rows[0]["Related_Ref"].ToString();
                txt_199_Narr1.Text = dt.Rows[0]["Narrative1"].ToString();
                txt_199_Narr2.Text = dt.Rows[0]["Narrative2"].ToString();
                txt_199_Narr3.Text = dt.Rows[0]["Narrative3"].ToString();
                txt_199_Narr4.Text = dt.Rows[0]["Narrative4"].ToString();
                txt_199_Narr5.Text = dt.Rows[0]["Narrative5"].ToString();
                txt_199_Narr6.Text = dt.Rows[0]["Narrative6"].ToString();
                txt_199_Narr7.Text = dt.Rows[0]["Narrative7"].ToString();
                txt_199_Narr8.Text = dt.Rows[0]["Narrative8"].ToString();
                txt_199_Narr9.Text = dt.Rows[0]["Narrative9"].ToString();
                txt_199_Narr10.Text = dt.Rows[0]["Narrative10"].ToString();
                txt_199_Narr11.Text = dt.Rows[0]["Narrative11"].ToString();
                txt_199_Narr12.Text = dt.Rows[0]["Narrative12"].ToString();
                txt_199_Narr13.Text = dt.Rows[0]["Narrative13"].ToString();
                txt_199_Narr14.Text = dt.Rows[0]["Narrative14"].ToString();
                txt_199_Narr15.Text = dt.Rows[0]["Narrative15"].ToString();
                txt_199_Narr16.Text = dt.Rows[0]["Narrative16"].ToString();
                txt_199_Narr17.Text = dt.Rows[0]["Narrative17"].ToString();
                txt_199_Narr18.Text = dt.Rows[0]["Narrative18"].ToString();
                txt_199_Narr19.Text = dt.Rows[0]["Narrative19"].ToString();
                txt_199_Narr20.Text = dt.Rows[0]["Narrative20"].ToString();
                txt_199_Narr21.Text = dt.Rows[0]["Narrative21"].ToString();
                txt_199_Narr22.Text = dt.Rows[0]["Narrative22"].ToString();
                txt_199_Narr23.Text = dt.Rows[0]["Narrative23"].ToString();
                txt_199_Narr24.Text = dt.Rows[0]["Narrative24"].ToString();
                txt_199_Narr25.Text = dt.Rows[0]["Narrative25"].ToString();
                txt_199_Narr26.Text = dt.Rows[0]["Narrative26"].ToString();
                txt_199_Narr27.Text = dt.Rows[0]["Narrative27"].ToString();
                txt_199_Narr28.Text = dt.Rows[0]["Narrative28"].ToString();
                txt_199_Narr29.Text = dt.Rows[0]["Narrative29"].ToString();
                txt_199_Narr30.Text = dt.Rows[0]["Narrative30"].ToString();
                txt_199_Narr31.Text = dt.Rows[0]["Narrative31"].ToString();
                txt_199_Narr32.Text = dt.Rows[0]["Narrative32"].ToString();
                txt_199_Narr33.Text = dt.Rows[0]["Narrative33"].ToString();
                txt_199_Narr34.Text = dt.Rows[0]["Narrative34"].ToString();
                txt_199_Narr35.Text = dt.Rows[0]["Narrative35"].ToString();

                if (dt.Rows[0]["Reject_Remarks"].ToString() != "")
                {
                    lblChecker_Remark.Text = "Checker Remark :- " + dt.Rows[0]["Reject_Remarks"].ToString();
                }
            }

            if (ddlSwiftTypes.SelectedItem.Text == "MT 299")
            {
                Panel_299.Visible = true;
                txtReceiver299.Text = dt.Rows[0]["Receiver"].ToString();
                txt_299_transRefNo.Text = dt.Rows[0]["Trans_RefNo"].ToString();
                txt_299_RelRef.Text = dt.Rows[0]["Related_Ref"].ToString();
                txt_299_Narr1.Text = dt.Rows[0]["Narrative1"].ToString();
                txt_299_Narr2.Text = dt.Rows[0]["Narrative2"].ToString();
                txt_299_Narr3.Text = dt.Rows[0]["Narrative3"].ToString();
                txt_299_Narr4.Text = dt.Rows[0]["Narrative4"].ToString();
                txt_299_Narr5.Text = dt.Rows[0]["Narrative5"].ToString();
                txt_299_Narr6.Text = dt.Rows[0]["Narrative6"].ToString();
                txt_299_Narr7.Text = dt.Rows[0]["Narrative7"].ToString();
                txt_299_Narr8.Text = dt.Rows[0]["Narrative8"].ToString();
                txt_299_Narr9.Text = dt.Rows[0]["Narrative9"].ToString();
                txt_299_Narr10.Text = dt.Rows[0]["Narrative10"].ToString();
                txt_299_Narr11.Text = dt.Rows[0]["Narrative11"].ToString();
                txt_299_Narr12.Text = dt.Rows[0]["Narrative12"].ToString();
                txt_299_Narr13.Text = dt.Rows[0]["Narrative13"].ToString();
                txt_299_Narr14.Text = dt.Rows[0]["Narrative14"].ToString();
                txt_299_Narr15.Text = dt.Rows[0]["Narrative15"].ToString();
                txt_299_Narr16.Text = dt.Rows[0]["Narrative16"].ToString();
                txt_299_Narr17.Text = dt.Rows[0]["Narrative17"].ToString();
                txt_299_Narr18.Text = dt.Rows[0]["Narrative18"].ToString();
                txt_299_Narr19.Text = dt.Rows[0]["Narrative19"].ToString();
                txt_299_Narr20.Text = dt.Rows[0]["Narrative20"].ToString();
                txt_299_Narr21.Text = dt.Rows[0]["Narrative21"].ToString();
                txt_299_Narr22.Text = dt.Rows[0]["Narrative22"].ToString();
                txt_299_Narr23.Text = dt.Rows[0]["Narrative23"].ToString();
                txt_299_Narr24.Text = dt.Rows[0]["Narrative24"].ToString();
                txt_299_Narr25.Text = dt.Rows[0]["Narrative25"].ToString();
                txt_299_Narr26.Text = dt.Rows[0]["Narrative26"].ToString();
                txt_299_Narr27.Text = dt.Rows[0]["Narrative27"].ToString();
                txt_299_Narr28.Text = dt.Rows[0]["Narrative28"].ToString();
                txt_299_Narr29.Text = dt.Rows[0]["Narrative29"].ToString();
                txt_299_Narr30.Text = dt.Rows[0]["Narrative30"].ToString();
                txt_299_Narr31.Text = dt.Rows[0]["Narrative31"].ToString();
                txt_299_Narr32.Text = dt.Rows[0]["Narrative32"].ToString();
                txt_299_Narr33.Text = dt.Rows[0]["Narrative33"].ToString();
                txt_299_Narr34.Text = dt.Rows[0]["Narrative34"].ToString();
                txt_299_Narr35.Text = dt.Rows[0]["Narrative35"].ToString();

                if (dt.Rows[0]["Reject_Remarks"].ToString() != "")
                {
                    lblChecker_Remark.Text = "Checker Remark :- " + dt.Rows[0]["Reject_Remarks"].ToString();
                }
            }

            if (ddlSwiftTypes.SelectedItem.Text == "MT 999")
            {
                Panel_999.Visible = true;
                txtReceiver999.Text = dt.Rows[0]["Receiver"].ToString();
                txt_999_transRefNo.Text = dt.Rows[0]["Trans_RefNo"].ToString();
                txt_999_RelRef.Text = dt.Rows[0]["Related_Ref"].ToString();
                txt_999_Narr1.Text = dt.Rows[0]["Narrative1"].ToString();
                txt_999_Narr2.Text = dt.Rows[0]["Narrative2"].ToString();
                txt_999_Narr3.Text = dt.Rows[0]["Narrative3"].ToString();
                txt_999_Narr4.Text = dt.Rows[0]["Narrative4"].ToString();
                txt_999_Narr5.Text = dt.Rows[0]["Narrative5"].ToString();
                txt_999_Narr6.Text = dt.Rows[0]["Narrative6"].ToString();
                txt_999_Narr7.Text = dt.Rows[0]["Narrative7"].ToString();
                txt_999_Narr8.Text = dt.Rows[0]["Narrative8"].ToString();
                txt_999_Narr9.Text = dt.Rows[0]["Narrative9"].ToString();
                txt_999_Narr10.Text = dt.Rows[0]["Narrative10"].ToString();
                txt_999_Narr11.Text = dt.Rows[0]["Narrative11"].ToString();
                txt_999_Narr12.Text = dt.Rows[0]["Narrative12"].ToString();
                txt_999_Narr13.Text = dt.Rows[0]["Narrative13"].ToString();
                txt_999_Narr14.Text = dt.Rows[0]["Narrative14"].ToString();
                txt_999_Narr15.Text = dt.Rows[0]["Narrative15"].ToString();
                txt_999_Narr16.Text = dt.Rows[0]["Narrative16"].ToString();
                txt_999_Narr17.Text = dt.Rows[0]["Narrative17"].ToString();
                txt_999_Narr18.Text = dt.Rows[0]["Narrative18"].ToString();
                txt_999_Narr19.Text = dt.Rows[0]["Narrative19"].ToString();
                txt_999_Narr20.Text = dt.Rows[0]["Narrative20"].ToString();
                txt_999_Narr21.Text = dt.Rows[0]["Narrative21"].ToString();
                txt_999_Narr22.Text = dt.Rows[0]["Narrative22"].ToString();
                txt_999_Narr23.Text = dt.Rows[0]["Narrative23"].ToString();
                txt_999_Narr24.Text = dt.Rows[0]["Narrative24"].ToString();
                txt_999_Narr25.Text = dt.Rows[0]["Narrative25"].ToString();
                txt_999_Narr26.Text = dt.Rows[0]["Narrative26"].ToString();
                txt_999_Narr27.Text = dt.Rows[0]["Narrative27"].ToString();
                txt_999_Narr28.Text = dt.Rows[0]["Narrative28"].ToString();
                txt_999_Narr29.Text = dt.Rows[0]["Narrative29"].ToString();
                txt_999_Narr30.Text = dt.Rows[0]["Narrative30"].ToString();
                txt_999_Narr31.Text = dt.Rows[0]["Narrative31"].ToString();
                txt_999_Narr32.Text = dt.Rows[0]["Narrative32"].ToString();
                txt_999_Narr33.Text = dt.Rows[0]["Narrative33"].ToString();
                txt_999_Narr34.Text = dt.Rows[0]["Narrative34"].ToString();
                txt_999_Narr35.Text = dt.Rows[0]["Narrative35"].ToString();

                if (dt.Rows[0]["Reject_Remarks"].ToString() != "")
                {
                    lblChecker_Remark.Text = "Checker Remark :- " + dt.Rows[0]["Reject_Remarks"].ToString();
                }
            }

            ////================799================//
            if (ddlSwiftTypes.SelectedItem.Text == "MT 799")
            {
                    Panel_799.Visible = true;
                    txtReceiver799.Text = dt.Rows[0]["Receiver"].ToString();
                    txt_799_transRefNo.Text = dt.Rows[0]["Trans_RefNo"].ToString();
                    txt_799_RelRef.Text = dt.Rows[0]["Related_Ref"].ToString();
                    txt_799_Narr1.Text = dt.Rows[0]["Narrative1"].ToString();
                    txt_799_Narr2.Text = dt.Rows[0]["Narrative2"].ToString();
                    txt_799_Narr3.Text = dt.Rows[0]["Narrative3"].ToString();
                    txt_799_Narr4.Text = dt.Rows[0]["Narrative4"].ToString();
                    txt_799_Narr5.Text = dt.Rows[0]["Narrative5"].ToString();
                    txt_799_Narr6.Text = dt.Rows[0]["Narrative6"].ToString();
                    txt_799_Narr7.Text = dt.Rows[0]["Narrative7"].ToString();
                    txt_799_Narr8.Text = dt.Rows[0]["Narrative8"].ToString();
                    txt_799_Narr9.Text = dt.Rows[0]["Narrative9"].ToString();
                    txt_799_Narr10.Text = dt.Rows[0]["Narrative10"].ToString();
                    txt_799_Narr11.Text = dt.Rows[0]["Narrative11"].ToString();
                    txt_799_Narr12.Text = dt.Rows[0]["Narrative12"].ToString();
                    txt_799_Narr13.Text = dt.Rows[0]["Narrative13"].ToString();
                    txt_799_Narr14.Text = dt.Rows[0]["Narrative14"].ToString();
                    txt_799_Narr15.Text = dt.Rows[0]["Narrative15"].ToString();
                    txt_799_Narr16.Text = dt.Rows[0]["Narrative16"].ToString();
                    txt_799_Narr17.Text = dt.Rows[0]["Narrative17"].ToString();
                    txt_799_Narr18.Text = dt.Rows[0]["Narrative18"].ToString();
                    txt_799_Narr19.Text = dt.Rows[0]["Narrative19"].ToString();
                    txt_799_Narr20.Text = dt.Rows[0]["Narrative20"].ToString();
                    txt_799_Narr21.Text = dt.Rows[0]["Narrative21"].ToString();
                    txt_799_Narr22.Text = dt.Rows[0]["Narrative22"].ToString();
                    txt_799_Narr23.Text = dt.Rows[0]["Narrative23"].ToString();
                    txt_799_Narr24.Text = dt.Rows[0]["Narrative24"].ToString();
                    txt_799_Narr25.Text = dt.Rows[0]["Narrative25"].ToString();
                    txt_799_Narr26.Text = dt.Rows[0]["Narrative26"].ToString();
                    txt_799_Narr27.Text = dt.Rows[0]["Narrative27"].ToString();
                    txt_799_Narr28.Text = dt.Rows[0]["Narrative28"].ToString();
                    txt_799_Narr29.Text = dt.Rows[0]["Narrative29"].ToString();
                    txt_799_Narr30.Text = dt.Rows[0]["Narrative30"].ToString();
                    txt_799_Narr31.Text = dt.Rows[0]["Narrative31"].ToString();
                    txt_799_Narr32.Text = dt.Rows[0]["Narrative32"].ToString();
                    txt_799_Narr33.Text = dt.Rows[0]["Narrative33"].ToString();
                    txt_799_Narr34.Text = dt.Rows[0]["Narrative34"].ToString();
                    txt_799_Narr35.Text = dt.Rows[0]["Narrative35"].ToString();

                    if (dt.Rows[0]["Reject_Remarks"].ToString() != "")
                    {
                        lblChecker_Remark.Text = "Checker Remark :- " + dt.Rows[0]["Reject_Remarks"].ToString();
                    }
                   //==================799 end===============//
            }
        }
    }
    /////   check Transaction ref no already in Use  ////////////////
    public void CheckTransRef_SwiftType()
    {
        string TranRefno = "";

        if (ddlBranch.SelectedItem.Text == "----Select----")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select Branch');", true);
            ddlBranch.Focus();

        }
        else if (ddlSwiftTypes.SelectedItem.Text == "----Select----")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select Swift Types');", true);
            ddlSwiftTypes.Focus();
        }
        else
        {
            if (ddlSwiftTypes.SelectedItem.Text == "MT 799")
            {
                TranRefno = txt_799_transRefNo.Text.Trim();
            }

            else if (ddlSwiftTypes.SelectedItem.Text == "MT 742")
            {
                TranRefno = txt_742_ClaimBankRef.Text.Trim();
            }

            else if (ddlSwiftTypes.SelectedItem.Text == "MT 499")
            {
                TranRefno = txt_499_transRefNo.Text.Trim();
            }
            else if (ddlSwiftTypes.SelectedItem.Text == "MT 199")
            {
                TranRefno = txt_199_transRefNo.Text.Trim();
            }
            else if (ddlSwiftTypes.SelectedItem.Text == "MT 299")
            {
                TranRefno = txt_299_transRefNo.Text.Trim();
            }
            else if (ddlSwiftTypes.SelectedItem.Text == "MT 999")
            {
                TranRefno = txt_999_transRefNo.Text.Trim();
            }
            else if (ddlSwiftTypes.SelectedItem.Text == "MT 420")
            {
                TranRefno = txt_420_SendingBankTRN.Text.Trim();
            }

            else if (ddlSwiftTypes.SelectedItem.Text == "MT 754")
            {
                TranRefno = txt_754_SenRef.Text.Trim();
            }
        }

        if (TranRefno.Contains("/") == true)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction reference number contains slash.');", true);
            txt_999_transRefNo.Text = txt_742_ClaimBankRef.Text = txt_754_SenRef.Text = txt_799_transRefNo.Text = "";
            txt_420_SendingBankTRN.Text = txt_499_transRefNo.Text = txt_199_transRefNo.Text = txt_299_transRefNo.Text = "";
        }
        else
        {
            SqlParameter p_Branch = new SqlParameter("@Branch", SqlDbType.VarChar);
            p_Branch.Value = ddlBranch.SelectedItem.Text;
            SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
            p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;
            SqlParameter p_TranRefno = new SqlParameter("@Trans_Ref_No", SqlDbType.VarChar);
            p_TranRefno.Value = TranRefno;

            TF_DATA objSave = new TF_DATA();
            DataTable dt = new DataTable();
            dt = objSave.getData("TF_SWIFT_STP_GetExportTransData", p_Branch, p_SwiftType, p_TranRefno);

            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction reference number is in use already.');", true);
                txt_999_transRefNo.Text = txt_742_ClaimBankRef.Text = txt_754_SenRef.Text = txt_799_transRefNo.Text = "";
                txt_420_SendingBankTRN.Text = txt_499_transRefNo.Text = txt_199_transRefNo.Text = txt_299_transRefNo.Text = "";
            }
        }
    }
    protected void txtReceiver799_TextChanged(object sender, EventArgs e)
    {
        if (Check_AlphaNumric(txtReceiver799.Text) == false)
        {
            txtReceiver799.Text = "";
            txtReceiver799.Focus();
        }
        else if ((txtReceiver799.Text.Length < 8) || (txtReceiver799.Text.Length > 8 && txtReceiver799.Text.Length != 11))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Receiver should be 8 or 11 alphanumeric characters!');", true);
            txtReceiver799.Text = "";
            txtReceiver799.Focus();
        }
        else
        {
            txt_799_transRefNo.Focus();
        }
    }
    protected void txt_799_transRefNo_TextChanged(object sender, EventArgs e)
    {
        //CheckRefno();
        CheckTransRef_SwiftType();
        if (Check_X_CharSet(txt_799_transRefNo.Text) == false)
        {
            txt_799_transRefNo.Text = "";
            txt_799_transRefNo.Focus();
        }
        else
        {
            txt_799_RelRef.Focus();
        }
    }
    protected void txt_799_RelRef_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_RelRef.Text) == false)
        {
            txt_799_RelRef.Text = "";
            txt_799_RelRef.Focus();
        }
        else
        {
            txt_799_Narr1.Focus();
        }
    }
    protected void txt_799_Narr1_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr1.Text) == false)
        {
            txt_799_Narr1.Text = "";
            txt_799_Narr1.Focus();
        }
        else
        {
            txt_799_Narr2.Focus();
        }
    }
    protected void txt_799_Narr2_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr2.Text) == false)
        {
            txt_799_Narr2.Text = "";
            txt_799_Narr2.Focus();
        }
        else
        {
            txt_799_Narr3.Focus();
        }
    }
    protected void txt_799_Narr3_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr3.Text) == false)
        {
            txt_799_Narr3.Text = "";
            txt_799_Narr3.Focus();
        }
        else
        {
            txt_799_Narr4.Focus();
        }
    }
    protected void txt_799_Narr4_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr4.Text) == false)
        {
            txt_799_Narr4.Text = "";
            txt_799_Narr4.Focus();
        }
        else
        {
            txt_799_Narr5.Focus();
        }
    }
    protected void txt_799_Narr5_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr5.Text) == false)
        {
            txt_799_Narr5.Text = "";
            txt_799_Narr5.Focus();
        }
        else
        {
            txt_799_Narr6.Focus();
        }
    }
    protected void txt_799_Narr6_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr6.Text) == false)
        {
            txt_799_Narr6.Text = "";
            txt_799_Narr6.Focus();
        }
        else
        {
            txt_799_Narr7.Focus();
        }
    }
    protected void txt_799_Narr7_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr7.Text) == false)
        {
            txt_799_Narr7.Text = "";
            txt_799_Narr7.Focus();
        }
        else
        {
            txt_799_Narr8.Focus();
        }
    }
    protected void txt_799_Narr8_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr8.Text) == false)
        {
            txt_799_Narr8.Text = "";
            txt_799_Narr8.Focus();
        }
        else
        {
            txt_799_Narr9.Focus();
        }
    }
    protected void txt_799_Narr9_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr9.Text) == false)
        {
            txt_799_Narr9.Text = "";
            txt_799_Narr9.Focus();
        }
        else
        {
            txt_799_Narr10.Focus();
        }
    }
    protected void txt_799_Narr10_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr10.Text) == false)
        {
            txt_799_Narr10.Text = "";
            txt_799_Narr10.Focus();
        }
        else
        {
            txt_799_Narr11.Focus();
        }
    }
    protected void txt_799_Narr11_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr11.Text) == false)
        {
            txt_799_Narr11.Text = "";
            txt_799_Narr11.Focus();
        }
        else
        {
            txt_799_Narr12.Focus();
        }
    }
    protected void txt_799_Narr12_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr12.Text) == false)
        {
            txt_799_Narr12.Text = "";
            txt_799_Narr12.Focus();
        }
        else
        {
            txt_799_Narr13.Focus();
        }
    }
    protected void txt_799_Narr13_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr13.Text) == false)
        {
            txt_799_Narr13.Text = "";
            txt_799_Narr13.Focus();
        }
        else
        {
            txt_799_Narr14.Focus();
        }
    }
    protected void txt_799_Narr14_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr14.Text) == false)
        {
            txt_799_Narr14.Text = "";
            txt_799_Narr14.Focus();
        }
        else
        {
            txt_799_Narr15.Focus();
        }
    }
    protected void txt_799_Narr15_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr15.Text) == false)
        {
            txt_799_Narr15.Text = "";
            txt_799_Narr15.Focus();
        }
        else
        {
            txt_799_Narr16.Focus();
        }
    }
    protected void txt_799_Narr16_TextChanged(object sender, EventArgs e)
    {
        Regex r = new Regex(@"[~`!@#$[%^&*=|\{};_<>]");
        if (r.IsMatch(txt_799_Narr16.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('special characters are not allowed !!');", true);
            txt_799_Narr16.Text = "";
            txt_799_Narr16.Focus();
        }
        if (Check_X_CharSet(txt_799_Narr16.Text) == false)
        {
            txt_799_Narr16.Text = "";
            txt_799_Narr16.Focus();
        }
        else
        {
            txt_799_Narr17.Focus();
        }
    }
    protected void txt_799_Narr17_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr17.Text) == false)
        {
            txt_799_Narr17.Text = "";
            txt_799_Narr17.Focus();
        }
        else
        {
            txt_799_Narr18.Focus();
        }
    }
    protected void txt_799_Narr18_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr18.Text) == false)
        {
            txt_799_Narr18.Text = "";
            txt_799_Narr18.Focus();
        }
        else
        {
            txt_799_Narr19.Focus();
        }
    }
    protected void txt_799_Narr19_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr19.Text) == false)
        {
            txt_799_Narr19.Text = "";
            txt_799_Narr19.Focus();
        }
        else
        {
            txt_799_Narr20.Focus();
        }
    }
    protected void txt_799_Narr20_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr20.Text) == false)
        {
            txt_799_Narr20.Text = "";
            txt_799_Narr20.Focus();
        }
        else
        {
            txt_799_Narr21.Focus();
        }
    }
    protected void txt_799_Narr21_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr21.Text) == false)
        {
            txt_799_Narr21.Text = "";
            txt_799_Narr21.Focus();
        }
        else
        {
            txt_799_Narr22.Focus();
        }
    }
    protected void txt_799_Narr22_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr22.Text) == false)
        {
            txt_799_Narr22.Text = "";
            txt_799_Narr22.Focus();
        }
        else
        {
            txt_799_Narr23.Focus();
        }
    }
    protected void txt_799_Narr23_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr23.Text) == false)
        {
            txt_799_Narr23.Text = "";
            txt_799_Narr23.Focus();
        }
        else
        {
            txt_799_Narr24.Focus();
        }
    }
    protected void txt_799_Narr24_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr24.Text) == false)
        {
            txt_799_Narr24.Text = "";
            txt_799_Narr24.Focus();
        }
        else
        {
            txt_799_Narr25.Focus();
        }
    }
    protected void txt_799_Narr25_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr25.Text) == false)
        {
            txt_799_Narr25.Text = "";
            txt_799_Narr25.Focus();
        }
        else
        {
            txt_799_Narr26.Focus();
        }
    }
    protected void txt_799_Narr26_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr26.Text) == false)
        {
            txt_799_Narr26.Text = "";
            txt_799_Narr26.Focus();
        }
        else
        {
            txt_799_Narr27.Focus();
        }
    }
    protected void txt_799_Narr27_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr27.Text) == false)
        {
            txt_799_Narr27.Text = "";
            txt_799_Narr27.Focus();
        }
        else
        {
            txt_799_Narr28.Focus();
        }
    }
    protected void txt_799_Narr28_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr28.Text) == false)
        {
            txt_799_Narr28.Text = "";
            txt_799_Narr28.Focus();
        }
        else
        {
            txt_799_Narr29.Focus();
        }
    }
    protected void txt_799_Narr29_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr29.Text) == false)
        {
            txt_799_Narr29.Text = "";
            txt_799_Narr29.Focus();
        }
        else
        {
            txt_799_Narr30.Focus();
        }
    }
    protected void txt_799_Narr30_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr30.Text) == false)
        {
            txt_799_Narr30.Text = "";
            txt_799_Narr30.Focus();
        }
        else
        {
            txt_799_Narr31.Focus();
        }
    }
    protected void txt_799_Narr31_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr31.Text) == false)
        {
            txt_799_Narr31.Text = "";
            txt_799_Narr31.Focus();
        }
        else
        {
            txt_799_Narr32.Focus();
        }
    }
    protected void txt_799_Narr32_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr32.Text) == false)
        {
            txt_799_Narr32.Text = "";
            txt_799_Narr32.Focus();
        }
        else
        {
            txt_799_Narr33.Focus();
        }
    }
    protected void txt_799_Narr33_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr33.Text) == false)
        {
            txt_799_Narr33.Text = "";
            txt_799_Narr33.Focus();
        }
        else
        {
            txt_799_Narr34.Focus();
        }
    }
    protected void txt_799_Narr34_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr34.Text) == false)
        {
            txt_799_Narr34.Text = "";
            txt_799_Narr34.Focus();
        }
        else
        {
            txt_799_Narr35.Focus();
        }
    }
    protected void txt_799_Narr35_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_799_Narr35.Text) == false)
        {
            txt_799_Narr35.Text = "";
            txt_799_Narr35.Focus();
        }
    }
    //public void CheckRefno()
    //{
    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@Trans_RefNo", SqlDbType.VarChar);
    //    p1.Value = txt_799_transRefNo.Text.Trim();
    //    SqlParameter p2 = new SqlParameter("@Swift_type", ddlSwiftTypes.SelectedItem.Text);
    //    SqlParameter p3 = new SqlParameter("@ADate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
    //    string _query = "TF_EXP_CheckREfno799";
    //    DataTable dt = objData.getData(_query, p1, p2, p3);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('This transaction reference number is already approved on the same day.');", true);
    //        txt_799_transRefNo.Text = "";
    //    }
    //}

    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
        //ddlBranch.SelectedIndex = 1;
        //ddlBranch_SelectedIndexChanged(null, null);
        //fillGrid();
    }
    protected void fillBranchbyuser()
    {
        string hdnUserName = Session["userName"].ToString();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@Username", SqlDbType.VarChar);
        p1.Value = hdnUserName;
        string _query = "TF_EXP_Branchselection";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            ddlBranch.SelectedItem.Text = dt.Rows[0]["BranchName"].ToString();
            ddlBranch.Enabled = false;
        }
        else
        { }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Expswift_MakerView.aspx", true);
    }
    public void Clearall()
    {

        txtReceiver742.Text = "";
        txtReceiver754.Text = "";
        txtReceiver799.Text = "";
        txtReceiver499.Text = "";
        txtReceiver420.Text = "";
        //================499================//
        txt_499_transRefNo.Text = "";
        txt_499_RelRef.Text = "";
        txt_499_Narr1.Text = "";
        txt_499_Narr2.Text = "";
        txt_499_Narr3.Text = "";
        txt_499_Narr4.Text = "";
        txt_499_Narr5.Text = "";
        txt_499_Narr6.Text = "";
        txt_499_Narr7.Text = "";
        txt_499_Narr8.Text = "";
        txt_499_Narr9.Text = "";
        txt_499_Narr10.Text = "";
        txt_499_Narr11.Text = "";
        txt_499_Narr12.Text = "";
        txt_499_Narr13.Text = "";
        txt_499_Narr14.Text = "";
        txt_499_Narr15.Text = "";
        txt_499_Narr16.Text = "";
        txt_499_Narr17.Text = "";
        txt_499_Narr18.Text = "";
        txt_499_Narr19.Text = "";
        txt_499_Narr20.Text = "";
        txt_499_Narr21.Text = "";
        txt_499_Narr22.Text = "";
        txt_499_Narr23.Text = "";
        txt_499_Narr24.Text = "";
        txt_499_Narr25.Text = "";
        txt_499_Narr26.Text = "";
        txt_499_Narr27.Text = "";
        txt_499_Narr28.Text = "";
        txt_499_Narr29.Text = "";
        txt_499_Narr30.Text = "";
        txt_499_Narr31.Text = "";
        txt_499_Narr32.Text = "";
        txt_499_Narr33.Text = "";
        txt_499_Narr34.Text = "";
        txt_499_Narr35.Text = "";
        //==================499 end===============//

        //================799================//
        txt_799_transRefNo.Text = "";
        txt_799_RelRef.Text = "";
        txt_799_Narr1.Text = "";
        txt_799_Narr2.Text = "";
        txt_799_Narr3.Text = "";
        txt_799_Narr4.Text = "";
        txt_799_Narr5.Text = "";
        txt_799_Narr6.Text = "";
        txt_799_Narr7.Text = "";
        txt_799_Narr8.Text = "";
        txt_799_Narr9.Text = "";
        txt_799_Narr10.Text = "";
        txt_799_Narr11.Text = "";
        txt_799_Narr12.Text = "";
        txt_799_Narr13.Text = "";
        txt_799_Narr14.Text = "";
        txt_799_Narr15.Text = "";
        txt_799_Narr16.Text = "";
        txt_799_Narr17.Text = "";
        txt_799_Narr18.Text = "";
        txt_799_Narr19.Text = "";
        txt_799_Narr20.Text = "";
        txt_799_Narr21.Text = "";
        txt_799_Narr22.Text = "";
        txt_799_Narr23.Text = "";
        txt_799_Narr24.Text = "";
        txt_799_Narr25.Text = "";
        txt_799_Narr26.Text = "";
        txt_799_Narr27.Text = "";
        txt_799_Narr28.Text = "";
        txt_799_Narr29.Text = "";
        txt_799_Narr30.Text = "";
        txt_799_Narr31.Text = "";
        txt_799_Narr32.Text = "";
        txt_799_Narr33.Text = "";
        txt_799_Narr34.Text = "";
        txt_799_Narr35.Text = "";
        //==================799 end===============//
        txt_420_SendingBankTRN.Text = "";
        txt_420_RelRef.Text = "";
        ddlAmountTraced_420.SelectedValue="";
        txtAmountTracedAmount_420.Text = "";
        ddlAmountTracedCode_420.SelectedValue = "";
        ddl_AmountTracedCurrency_420.Text = "";
        txtAmountTracedDate_420.Text = "";
        ddlAmountTracedDayMonth_420.SelectedValue = "";
        txtAmountTracedNoofDaysMonth_420.Text = "";
        txt_420_DateofCollnInstruction.Text = "";
        txt_420_DraweeAccount.Text = "";
        txt_420_DraweeName.Text = "";
        txt_420_DraweeAdd1.Text = "";
        txt_420_DraweeAdd2.Text = "";
        txt_420_DraweeAdd3.Text = "";
        txt_420_SenToRecinfo1.Text = "";
        txt_420_SenToRecinfo2.Text = "";
        txt_420_SenToRecinfo3.Text = "";
        txt_420_SenToRecinfo4.Text = "";
        txt_420_SenToRecinfo5.Text = "";
        txt_420_SenToRecinfo6.Text = "";
        //=========================420 end===================//
        //=========================742 ===================//
        txt_742_ClaimBankRef.Text = "";
        txt_742_DocumCreditNo.Text = "";
        txt_742_Dateofissue.Text = "";
        txtIssuingBankAccountnumber_742.Text = "";
        txtIssuingBankAccountnumber1_742.Text = "";
        txtIssuingBankIdentifiercode_742.Text = "";
        txtIssuingBankName_742.Text = "";
        txtIssuingBankAddress1_742.Text = "";
        txtIssuingBankAddress2_742.Text = "";
        txtIssuingBankAddress3_742.Text = "";
        ddl_742_PrinAmtClmd_Ccy.Text = "";
        txt_742_PrinAmtClmd_Amt.Text = "";
        ddl_742_AddAmtClamd_Ccy.Text = "";
        txt_742_AddAmtClamd_Amt.Text = "";
        txt_742_Charges1.Text = "";
        txt_742_Charges2.Text = "";
        txt_742_Charges3.Text = "";
        txt_742_Charges4.Text = "";
        txt_742_Charges5.Text = "";
        txt_742_Charges6.Text = "";
        txt_742_TotalAmtClmd_Date.Text = "";
        ddl_742_TotalAmtClmd_Ccy.Text = "";
        txt_742_TotalAmtClmd_Amt.Text = "";
        txtAccountwithBankAccountnumber_742.Text = "";
        txtAccountwithBankAccountnumber1_742.Text = "";
        txtAccountwithBankIdentifiercode_742.Text = "";
        txtAccountwithBankLocation_742.Text = "";
        txtAccountwithBankName_742.Text = "";
        txtAccountwithBankAddress1_742.Text = "";
        txtAccountwithBankAddress2_742.Text = "";
        txtAccountwithBankAddress3_742.Text = "";
        txtBeneficiaryBankAccountnumber_742.Text = "";
        txtBeneficiaryBankAccountnumber1_742.Text = "";
        txtBeneficiaryBankIdentifiercode_742.Text = "";
        txtBeneficiaryBankName_742.Text = "";
        txtBeneficiaryBankAddress1_742.Text = "";
        txtBeneficiaryBankAddress2_742.Text = "";
        txtBeneficiaryBankAddress3_742.Text = "";
        txt_742_SenRecInfo1.Text = "";
        txt_742_SenRecInfo2.Text = "";
        txt_742_SenRecInfo3.Text = "";
        txt_742_SenRecInfo4.Text = "";
        txt_742_SenRecInfo5.Text = "";
        txt_742_SenRecInfo6.Text = "";
        //=========================742 end===================//
        //=========================754 ===================//
        txt_754_SenRef.Text = "";
        txt_754_RelRef.Text = "";
        txtPrinAmtPaidAccNegoDate_754.Text = "";
        ddl_PrinAmtPaidAccNegoCurr_754.Text = "";
        txtPrinAmtPaidAccNegoAmt_754.Text = "";
        ddl_754_AddAmtClamd_Ccy.Text = "";
        txtPrinAmtPaidAccNegoAmt_754.Text = "";
        txt_754_ChargesDeduct1.Text = "";
        txt_754_ChargesDeduct2.Text = "";
        txt_754_ChargesDeduct3.Text = "";
        txt_754_ChargesDeduct4.Text = "";
        txt_754_ChargesDeduct5.Text = "";
        txt_754_ChargesDeduct6.Text = "";
        txt_754_ChargesAdded1.Text = "";
        txt_754_ChargesAdded2.Text = "";
        txt_754_ChargesAdded3.Text = "";
        txt_754_ChargesAdded4.Text = "";
        txt_754_ChargesAdded5.Text = "";
        txt_754_ChargesAdded6.Text = "";
        txt_754_TotalAmtClmd_Date.Text = "";
        ddl_754_TotalAmtClmd_Ccy.Text = "";
        txt_754_TotalAmtClmd_Amt.Text = "";
        txtReimbursingBankAccountnumber_754.Text = "";
        txtReimbursingBankAccountnumber1_754.Text = "";
        txtReimbursingBankIdentifiercode_754.Text = "";
        txtReimbursingBankLocation_754.Text = "";
        txtReimbursingBankName_754.Text = "";
        txtReimbursingBankAddress1_754.Text = "";
        txtReimbursingBankAddress2_754.Text = "";
        txtReimbursingBankAddress3_754.Text = "";
        txtAccountwithBankAccountnumber_754.Text = "";
        txtAccountwithBankAccountnumber1_754.Text = "";
        txtAccountwithBankIdentifiercode_754.Text = "";
        txtAccountwithBankLocation_754.Text = "";
        txtAccountwithBankName_754.Text = "";
        txtAccountwithBankAddress1_754.Text = "";
        txtAccountwithBankAddress2_754.Text = "";
        txtAccountwithBankAddress3_754.Text = "";
        txtBeneficiaryBankAccountnumber_754.Text = "";
        txtBeneficiaryBankAccountnumber1_754.Text = "";
        txtBeneficiaryBankIdentifiercode_754.Text = "";
        txtBeneficiaryBankName_754.Text = "";
        txtBeneficiaryBankAddress1_754.Text = "";
        txtBeneficiaryBankAddress2_754.Text = "";
        txtBeneficiaryBankAddress3_754.Text = "";
        txt_754_SenRecInfo1.Text = "";
        txt_754_SenRecInfo2.Text = "";
        txt_754_SenRecInfo3.Text = "";
        txt_754_SenRecInfo4.Text = "";
        txt_754_SenRecInfo5.Text = "";
        txt_754_SenRecInfo6.Text = "";
        txt_754_Narr1.Text = "";
        txt_754_Narr2.Text = "";
        txt_754_Narr3.Text = "";
        txt_754_Narr4.Text = "";
        txt_754_Narr5.Text = "";
        txt_754_Narr6.Text = "";
        txt_754_Narr7.Text = "";
        txt_754_Narr8.Text = "";
        txt_754_Narr9.Text = "";
        txt_754_Narr10.Text = "";
        txt_754_Narr11.Text = "";
        txt_754_Narr12.Text = "";
        txt_754_Narr13.Text = "";
        txt_754_Narr14.Text = "";
        txt_754_Narr15.Text = "";
        txt_754_Narr16.Text = "";
        txt_754_Narr17.Text = "";
        txt_754_Narr18.Text = "";
        txt_754_Narr19.Text = "";
        txt_754_Narr20.Text = "";
        //=========================754 end===================//
    }
    public void Save742()
    {
        SqlParameter p_Branch = new SqlParameter("@Branch", SqlDbType.VarChar);
        p_Branch.Value = ddlBranch.SelectedItem.Text;
        SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
        p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;
        if (txtReceiver742.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter receiver');", true);
            txtReceiver742.Focus();
        }
        SqlParameter p_txtReceiver742 = new SqlParameter("@Receiver", SqlDbType.VarChar);
        p_txtReceiver742.Value = txtReceiver742.Text.Trim();

        if (txt_742_ClaimBankRef.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Claiming Bank Reference');", true);
        }
        if (txt_742_ClaimBankRef.Text.Contains("//"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Claiming bank Ref contains two consecutive slashes //');", true);
        }
        else if (txt_742_ClaimBankRef.Text.StartsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Claiming bank Ref start with slash(/)');", true);
        }
        else if (txt_742_ClaimBankRef.Text.EndsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Claiming bank Ref end with slash(/)');", true);
        }
        SqlParameter p_txt_742_ClaimBankRef = new SqlParameter("@ClaimBankRef", SqlDbType.VarChar);
        p_txt_742_ClaimBankRef.Value = txt_742_ClaimBankRef.Text.Trim();

        if (txt_742_DocumCreditNo.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Documentary Credit Number');", true);
        }
        if (txt_742_DocumCreditNo.Text.Contains("//"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Documentary credit No contains consecutive slashes //');", true);
        }
        else if (txt_742_DocumCreditNo.Text.StartsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Documentary credit No start with slash(/)');", true);
        }
        else if (txt_742_DocumCreditNo.Text.EndsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Documentary credit No end with slash(/)');", true);
        }
        SqlParameter p_txt_742_DocumCreditNo = new SqlParameter("@DocumCreditNo", SqlDbType.VarChar);
        p_txt_742_DocumCreditNo.Value = txt_742_DocumCreditNo.Text.Trim();

        SqlParameter p_txt_742_Dateofissue = new SqlParameter("@Dateofissue", SqlDbType.VarChar);
        p_txt_742_Dateofissue.Value = txt_742_Dateofissue.Text.Trim();

        SqlParameter p_ddl_Issuingbank_742 = new SqlParameter("@ddlIssuingBank", SqlDbType.VarChar);
        p_ddl_Issuingbank_742.Value = ddl_Issuingbank_742.SelectedValue;

        if (txtIssuingBankIdentifiercode_742.Text == "" && txtIssuingBankName_742.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Enter Issuing Bank[52A]');", true);
        }

        //if (txtIssuingBankAccountnumber_742.Text == "")
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Issuing Bank Partyidentifier');", true);
        //    txtIssuingBankAccountnumber_742.Focus();
        //}
        SqlParameter p_txtIssuingBankAccountnumber_742 = new SqlParameter("@IssuingBank_PartyIdent", SqlDbType.VarChar);
        p_txtIssuingBankAccountnumber_742.Value = txtIssuingBankAccountnumber_742.Text.Trim();

        //if (txtIssuingBankAccountnumber1_742.Text == "")
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Issuing Bank Partyidentifier');", true);
        //    txtIssuingBankAccountnumber1_742.Focus();
        //}
        SqlParameter p_txtIssuingBankAccountnumber1_742 = new SqlParameter("@IssuingBank_PartyIdent1", SqlDbType.VarChar);
        p_txtIssuingBankAccountnumber1_742.Value = txtIssuingBankAccountnumber1_742.Text.Trim();

        if (txtIssuingBankIdentifiercode_742.Text == "" && ddl_Issuingbank_742.SelectedValue == "A")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Issuing Bank identifier code');", true);
            txtIssuingBankIdentifiercode_742.Focus();
        }
        SqlParameter p_txtIssuingBankIdentifiercode_742 = new SqlParameter("@IssuingBank_Identcode", SqlDbType.VarChar);
        p_txtIssuingBankIdentifiercode_742.Value = txtIssuingBankIdentifiercode_742.Text.Trim();

        if (txtIssuingBankName_742.Text == "" && ddl_Issuingbank_742.SelectedValue == "D")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Issuing Bank Name');", true);
            txtIssuingBankName_742.Focus();
        }
        SqlParameter p_txtIssuingBankName_742 = new SqlParameter("@IssuingBank_Name", SqlDbType.VarChar);
        p_txtIssuingBankName_742.Value = txtIssuingBankName_742.Text.Trim();
        SqlParameter p_txtIssuingBankAddress1_742 = new SqlParameter("@IssuingBank_Add1", SqlDbType.VarChar);
        p_txtIssuingBankAddress1_742.Value = txtIssuingBankAddress1_742.Text.Trim();
        SqlParameter p_txtIssuingBankAddress2_742 = new SqlParameter("@IssuingBank_Add2", SqlDbType.VarChar);
        p_txtIssuingBankAddress2_742.Value = txtIssuingBankAddress2_742.Text.Trim();
        SqlParameter p_txtIssuingBankAddress3_742 = new SqlParameter("@IssuingBank_Add3", SqlDbType.VarChar);
        p_txtIssuingBankAddress3_742.Value = txtIssuingBankAddress3_742.Text.Trim();


        if (ddl_742_PrinAmtClmd_Ccy.SelectedItem.Text == "" && txt_742_PrinAmtClmd_Amt.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Principle Amount Claimed[32B]');", true);
        }
        if (ddl_742_PrinAmtClmd_Ccy.SelectedItem.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Principle Amount Claimed Currency');", true);
            ddl_742_PrinAmtClmd_Ccy.Focus();
        }
        SqlParameter p_ddl_742_PrinAmtClmd_Ccy = new SqlParameter("@PrincipalAmtClaimed_Curr", SqlDbType.VarChar);
        p_ddl_742_PrinAmtClmd_Ccy.Value = ddl_742_PrinAmtClmd_Ccy.SelectedItem.Text.Trim();

        if (txt_742_PrinAmtClmd_Amt.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Principle Amount Claimed Amount');", true);
            txt_742_PrinAmtClmd_Amt.Focus();
        }
        if (Regex.IsMatch(txt_742_PrinAmtClmd_Amt.Text, @"\.\d\d\d"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal in Principal Amount Claimed field');", true);
            txt_742_PrinAmtClmd_Amt.Focus();
        }

        if (txt_742_PrinAmtClmd_Amt.Text.Contains(','))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please do not enter (,) in Principal amount Claimed field');", true);
            txt_742_PrinAmtClmd_Amt.Focus();
        }
        SqlParameter p_txt_742_PrinAmtClmd_Amt = new SqlParameter("@PrincipalAmtClaimed_Amt", SqlDbType.VarChar);
        p_txt_742_PrinAmtClmd_Amt.Value = txt_742_PrinAmtClmd_Amt.Text.Trim();

        SqlParameter p_ddl_742_AddAmtClamd_Ccy = new SqlParameter("@AddnlAmt_Curr", SqlDbType.VarChar);
        p_ddl_742_AddAmtClamd_Ccy.Value = ddl_742_AddAmtClamd_Ccy.SelectedItem.Text.Trim();
        if (Regex.IsMatch(txt_742_AddAmtClamd_Amt.Text, @"\.\d\d\d"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal in additional amount field');", true);
            txt_742_AddAmtClamd_Amt.Focus();
        }

        if (txt_742_AddAmtClamd_Amt.Text.Contains(','))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please do not enter (,) in additional amount field');", true);
            txt_742_AddAmtClamd_Amt.Focus();
        }
        SqlParameter p_txt_742_AddAmtClamd_Amt = new SqlParameter("@AddnlAmt_Amt", SqlDbType.VarChar);
        p_txt_742_AddAmtClamd_Amt.Value = txt_742_AddAmtClamd_Amt.Text.Trim();

        SqlParameter p_txt_742_Charges1 = new SqlParameter("@Charges1", SqlDbType.VarChar);
        p_txt_742_Charges1.Value = txt_742_Charges1.Text.Trim();
        SqlParameter p_txt_742_Charges2 = new SqlParameter("@Charges2", SqlDbType.VarChar);
        p_txt_742_Charges2.Value = txt_742_Charges2.Text.Trim();
        SqlParameter p_txt_742_Charges3 = new SqlParameter("@Charges3", SqlDbType.VarChar);
        p_txt_742_Charges3.Value = txt_742_Charges3.Text.Trim();
        SqlParameter p_txt_742_Charges4 = new SqlParameter("@Charges4", SqlDbType.VarChar);
        p_txt_742_Charges4.Value = txt_742_Charges4.Text.Trim();
        SqlParameter p_txt_742_Charges5 = new SqlParameter("@Charges5", SqlDbType.VarChar);
        p_txt_742_Charges5.Value = txt_742_Charges5.Text.Trim();
        SqlParameter p_txt_742_Charges6 = new SqlParameter("@Charges6", SqlDbType.VarChar);
        p_txt_742_Charges6.Value = txt_742_Charges6.Text.Trim();

        SqlParameter p_ddlTotalAmtclamd_742 = new SqlParameter("@ddlTotalAmtClaimed", SqlDbType.VarChar);
        p_ddlTotalAmtclamd_742.Value = ddlTotalAmtclamd_742.SelectedValue;

        if (txt_742_TotalAmtClmd_Date.Text == "" && ddl_742_TotalAmtClmd_Ccy.SelectedItem.Text == "" && txt_742_TotalAmtClmd_Amt.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Enter Total Amount claimed[34A]');", true);
        }

        if (txt_742_TotalAmtClmd_Date.Text == "" && ddlTotalAmtclamd_742.SelectedValue == "A")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Total Amount claimed Date should not be blank');", true);
        }
        SqlParameter p_txt_742_TotalAmtClmd_Date = new SqlParameter("@TotalAmtClaimed_Date", SqlDbType.VarChar);
        p_txt_742_TotalAmtClmd_Date.Value = txt_742_TotalAmtClmd_Date.Text.Trim();
        if (ddl_742_TotalAmtClmd_Ccy.SelectedItem.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Total Amount claimed Currency should not be blank');", true);
        }
        SqlParameter p_ddl_742_TotalAmtClmd_Ccy = new SqlParameter("@TotalAmtClaimed_Curr", SqlDbType.VarChar);
        p_ddl_742_TotalAmtClmd_Ccy.Value = ddl_742_TotalAmtClmd_Ccy.SelectedItem.Text.Trim();
        if (txt_742_TotalAmtClmd_Amt.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Total Amount claimed Amount should not be blank');", true);
        }
        if (Regex.IsMatch(txt_742_TotalAmtClmd_Amt.Text, @"\.\d\d\d"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal in total amount claimed field');", true);
            txt_742_TotalAmtClmd_Amt.Focus();
        }

        if (txt_742_TotalAmtClmd_Amt.Text.Contains(','))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please do not enter (,) in total amount claimed field');", true);
            txt_742_TotalAmtClmd_Amt.Focus();
        }
        SqlParameter p_txt_742_TotalAmtClmd_Amt = new SqlParameter("@TotalAmtClaimed_Amt", SqlDbType.VarChar);
        p_txt_742_TotalAmtClmd_Amt.Value = txt_742_TotalAmtClmd_Amt.Text.Trim();

        SqlParameter p_ddlAccountwithbank_742 = new SqlParameter("@ddlAccWithBank", SqlDbType.VarChar);
        p_ddlAccountwithbank_742.Value = ddlAccountwithbank_742.SelectedValue;
        SqlParameter p_txtAccountwithBankAccountnumber_742 = new SqlParameter("@AccWithBank_PartyIdent", SqlDbType.VarChar);
        p_txtAccountwithBankAccountnumber_742.Value = txtAccountwithBankAccountnumber_742.Text.Trim();
        SqlParameter p_txtAccountwithBankAccountnumber1_742 = new SqlParameter("@AccWithBank_PartyIdent1", SqlDbType.VarChar);
        p_txtAccountwithBankAccountnumber1_742.Value = txtAccountwithBankAccountnumber1_742.Text.Trim();
        SqlParameter p_txtAccountwithBankIdentifiercode_742 = new SqlParameter("@AccWithBank_Identcode", SqlDbType.VarChar);
        p_txtAccountwithBankIdentifiercode_742.Value = txtAccountwithBankIdentifiercode_742.Text.Trim();
        SqlParameter p_txtAccountwithBankLocation_742 = new SqlParameter("@AccWithBank_Location", SqlDbType.VarChar);
        p_txtAccountwithBankLocation_742.Value = txtAccountwithBankLocation_742.Text.Trim();
        SqlParameter p_txtAccountwithBankName_742 = new SqlParameter("@AccWithBank_Name", SqlDbType.VarChar);
        p_txtAccountwithBankName_742.Value = txtAccountwithBankName_742.Text.Trim();
        SqlParameter p_txtAccountwithBankAddress1_742 = new SqlParameter("@AccWithBank_Add1", SqlDbType.VarChar);
        p_txtAccountwithBankAddress1_742.Value = txtAccountwithBankAddress1_742.Text.Trim();
        SqlParameter p_txtAccountwithBankAddress2_742 = new SqlParameter("@AccWithBank_Add2", SqlDbType.VarChar);
        p_txtAccountwithBankAddress2_742.Value = txtAccountwithBankAddress2_742.Text.Trim();
        SqlParameter p_txtAccountwithBankAddress3_742 = new SqlParameter("@AccWithBank_Add3", SqlDbType.VarChar);
        p_txtAccountwithBankAddress3_742.Value = txtAccountwithBankAddress3_742.Text.Trim();

        SqlParameter p_ddlBeneficiarybank_742 = new SqlParameter("@ddlBeneficiaryBank", SqlDbType.VarChar);
        p_ddlBeneficiarybank_742.Value = ddlBeneficiarybank_742.SelectedValue;
        SqlParameter p_txtBeneficiaryBankAccountnumber_742 = new SqlParameter("@BeneficiaryBank_PartyIdent", SqlDbType.VarChar);
        p_txtBeneficiaryBankAccountnumber_742.Value = txtBeneficiaryBankAccountnumber_742.Text.Trim();
        SqlParameter p_txtBeneficiaryBankAccountnumber1_742 = new SqlParameter("@BeneficiaryBank_PartyIdent1", SqlDbType.VarChar);
        p_txtBeneficiaryBankAccountnumber1_742.Value = txtBeneficiaryBankAccountnumber1_742.Text.Trim();
        SqlParameter p_txtBeneficiaryBankIdentifiercode_742 = new SqlParameter("@BeneficiaryBank_Identcode", SqlDbType.VarChar);
        p_txtBeneficiaryBankIdentifiercode_742.Value = txtBeneficiaryBankIdentifiercode_742.Text.Trim();
        SqlParameter p_txtBeneficiaryBankName_742 = new SqlParameter("@BeneficiaryBank_Name", SqlDbType.VarChar);
        p_txtBeneficiaryBankName_742.Value = txtBeneficiaryBankName_742.Text.Trim();
        SqlParameter p_txtBeneficiaryBankAddress1_742 = new SqlParameter("@BeneficiaryBank_Add1", SqlDbType.VarChar);
        p_txtBeneficiaryBankAddress1_742.Value = txtBeneficiaryBankAddress1_742.Text.Trim();
        SqlParameter p_txtBeneficiaryBankAddress2_742 = new SqlParameter("@BeneficiaryBank_Add2", SqlDbType.VarChar);
        p_txtBeneficiaryBankAddress2_742.Value = txtBeneficiaryBankAddress2_742.Text.Trim();
        SqlParameter p_txtBeneficiaryBankAddress3_742 = new SqlParameter("@BeneficiaryBank_Add3", SqlDbType.VarChar);
        p_txtBeneficiaryBankAddress3_742.Value = txtBeneficiaryBankAddress3_742.Text.Trim();

        SqlParameter p_742_SenToRecinfo1 = new SqlParameter("@SenToRecinfo1", SqlDbType.VarChar);
        p_742_SenToRecinfo1.Value = txt_742_SenRecInfo1.Text.Trim();
        SqlParameter p_742_SenToRecinfo2 = new SqlParameter("@SenToRecinfo2", SqlDbType.VarChar);
        p_742_SenToRecinfo2.Value = txt_742_SenRecInfo2.Text.Trim();
        SqlParameter p_742_SenToRecinfo3 = new SqlParameter("@SenToRecinfo3", SqlDbType.VarChar);
        p_742_SenToRecinfo3.Value = txt_742_SenRecInfo3.Text.Trim();
        SqlParameter p_742_SenToRecinfo4 = new SqlParameter("@SenToRecinfo4", SqlDbType.VarChar);
        p_742_SenToRecinfo4.Value = txt_742_SenRecInfo4.Text.Trim();
        SqlParameter p_742_SenToRecinfo5 = new SqlParameter("@SenToRecinfo5", SqlDbType.VarChar);
        p_742_SenToRecinfo5.Value = txt_742_SenRecInfo5.Text.Trim();
        SqlParameter p_742_SenToRecinfo6 = new SqlParameter("@SenToRecinfo6", SqlDbType.VarChar);
        p_742_SenToRecinfo6.Value = txt_742_SenRecInfo6.Text.Trim();

        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData("TF_Exp_AddUpdateSwift742", p_Branch, p_SwiftType, p_txtReceiver742, p_txt_742_ClaimBankRef, p_txt_742_DocumCreditNo, p_txt_742_Dateofissue, p_ddl_Issuingbank_742,
            p_txtIssuingBankAccountnumber_742, p_txtIssuingBankAccountnumber1_742, p_txtIssuingBankIdentifiercode_742, p_txtIssuingBankName_742, p_txtIssuingBankAddress1_742,
            p_txtIssuingBankAddress2_742, p_txtIssuingBankAddress3_742, p_ddl_742_PrinAmtClmd_Ccy, p_txt_742_PrinAmtClmd_Amt, p_ddl_742_AddAmtClamd_Ccy, p_txt_742_AddAmtClamd_Amt,
            p_txt_742_Charges1, p_txt_742_Charges2, p_txt_742_Charges3, p_txt_742_Charges4, p_txt_742_Charges5, p_txt_742_Charges6, p_ddlTotalAmtclamd_742, p_txt_742_TotalAmtClmd_Date,
            p_ddl_742_TotalAmtClmd_Ccy, p_txt_742_TotalAmtClmd_Amt, p_ddlAccountwithbank_742, p_txtAccountwithBankAccountnumber_742, p_txtAccountwithBankAccountnumber1_742,
            p_txtAccountwithBankIdentifiercode_742, p_txtAccountwithBankLocation_742, p_txtAccountwithBankName_742, p_txtAccountwithBankAddress1_742, p_txtAccountwithBankAddress2_742,
            p_txtAccountwithBankAddress3_742, p_ddlBeneficiarybank_742, p_txtBeneficiaryBankAccountnumber_742, p_txtBeneficiaryBankAccountnumber1_742, p_txtBeneficiaryBankIdentifiercode_742,
            p_txtBeneficiaryBankName_742, p_txtBeneficiaryBankAddress1_742, p_txtBeneficiaryBankAddress2_742, p_txtBeneficiaryBankAddress3_742, p_742_SenToRecinfo1,
            p_742_SenToRecinfo2, p_742_SenToRecinfo3, p_742_SenToRecinfo4, p_742_SenToRecinfo5, p_742_SenToRecinfo6);

        if (_result == "added")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added as Draft .');", true);
        }
        else if (_result == "Updated")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Error');", true);
        }
    }
    public void Save754()
    {
        SqlParameter p_Branch = new SqlParameter("@Branch", SqlDbType.VarChar);
        p_Branch.Value = ddlBranch.SelectedItem.Text;
        SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
        p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;
        if (txtReceiver754.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter receiver');", true);
            txtReceiver754.Focus();
        }
        SqlParameter p_txtReceiver754 = new SqlParameter("@Receiver", SqlDbType.VarChar);
        p_txtReceiver754.Value = txtReceiver754.Text.Trim();

        if (txt_754_SenRef.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Sender Reference');", true);
        }
        if (txt_754_SenRef.Text.Contains("//"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Sender Reference contains two consecutive slashes //');", true);
        }
        else if (txt_754_SenRef.Text.StartsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Sender Reference start with slash(/)');", true);
        }
        else if (txt_754_SenRef.Text.EndsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Sender Reference end with slash(/)');", true);
        }
        SqlParameter p_754SenRefno = new SqlParameter("@Send_Ref", SqlDbType.VarChar);
        p_754SenRefno.Value = txt_754_SenRef.Text.Trim();

        if (txt_754_RelRef.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Related Reference');", true);
        }
        if (txt_754_RelRef.Text.Contains("//"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference contains two consecutive slashes //');", true);
        }
        else if (txt_754_RelRef.Text.StartsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference start with slash(/)');", true);
        }
        else if (txt_754_RelRef.Text.EndsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference end with slash(/)');", true);
        }
        SqlParameter p_754RelRef = new SqlParameter("@Related_Ref", SqlDbType.VarChar);
        p_754RelRef.Value = txt_754_RelRef.Text.Trim();

        SqlParameter p_ddlPrinAmtPaidAccNegoAmt_754 = new SqlParameter("@ddlPrincipalAmtPaidAccptdNego", SqlDbType.VarChar);
        p_ddlPrinAmtPaidAccNegoAmt_754.Value = ddlPrinAmtPaidAccNego_754.SelectedValue;

        if (txtPrinAmtPaidAccNegoDate_754.Text == "" && ddl_PrinAmtPaidAccNegoCurr_754.Text == "" && txtPrinAmtPaidAccNegoAmt_754.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert(' Principal Amount Paid/Accepted/Negotiated cannot be blank');", true);
        }

        SqlParameter p_txtPrinAmtPaidAccNegoDate_754 = new SqlParameter("@PrincipalAmtPaidAccptdNego_Date", SqlDbType.VarChar);
        p_txtPrinAmtPaidAccNegoDate_754.Value = txtPrinAmtPaidAccNegoDate_754.Text.Trim();
        SqlParameter p_ddl_PrinAmtPaidAccNegoCurr_754 = new SqlParameter("@PrincipalAmtPaidAccptdNego_Curr", SqlDbType.VarChar);
        p_ddl_PrinAmtPaidAccNegoCurr_754.Value = ddl_PrinAmtPaidAccNegoCurr_754.Text.Trim();
        if (Regex.IsMatch(txtPrinAmtPaidAccNegoAmt_754.Text, @"\.\d\d\d"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal in principle amt field');", true);
            txtPrinAmtPaidAccNegoAmt_754.Focus();
        }

        if (txtPrinAmtPaidAccNegoAmt_754.Text.Contains(','))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please do not enter (,) in principle amount field');", true);
            txtPrinAmtPaidAccNegoAmt_754.Focus();
        }
        SqlParameter p_txtPrinAmtPaidAccNegoAmt_754 = new SqlParameter("@PrincipalAmtPaidAccptdNego_Amt", SqlDbType.VarChar);
        p_txtPrinAmtPaidAccNegoAmt_754.Value = txtPrinAmtPaidAccNegoAmt_754.Text.Trim();

        SqlParameter p_ddl_754_AddAmtClamd_Ccy = new SqlParameter("@AddnlAmt_Curr", SqlDbType.VarChar);
        p_ddl_754_AddAmtClamd_Ccy.Value = ddl_754_AddAmtClamd_Ccy.Text.Trim();
        if (Regex.IsMatch(txt_754_AddAmtClamd_Amt.Text, @"\.\d\d\d"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal in Additional amounts field');", true);
            txt_754_AddAmtClamd_Amt.Focus();
        }

        if (txt_754_AddAmtClamd_Amt.Text.Contains(','))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please do not enter (,) in Additional amounts field');", true);
            txt_754_AddAmtClamd_Amt.Focus();
        }
        SqlParameter p_txt_754_AddAmtClamd_Amt = new SqlParameter("@AddnlAmt_Amt", SqlDbType.VarChar);
        p_txt_754_AddAmtClamd_Amt.Value = txt_754_AddAmtClamd_Amt.Text.Trim();

        SqlParameter p_txt_754_ChargesDeduct1 = new SqlParameter("@Charges1", SqlDbType.VarChar);
        p_txt_754_ChargesDeduct1.Value = txt_754_ChargesDeduct1.Text.Trim();
        SqlParameter p_txt_754_ChargesDeduct2 = new SqlParameter("@Charges2", SqlDbType.VarChar);
        p_txt_754_ChargesDeduct2.Value = txt_754_ChargesDeduct2.Text.Trim();
        SqlParameter p_txt_754_ChargesDeduct3 = new SqlParameter("@Charges3", SqlDbType.VarChar);
        p_txt_754_ChargesDeduct3.Value = txt_754_ChargesDeduct3.Text.Trim();
        SqlParameter p_txt_754_ChargesDeduct4 = new SqlParameter("@Charges4", SqlDbType.VarChar);
        p_txt_754_ChargesDeduct4.Value = txt_754_ChargesDeduct4.Text.Trim();
        SqlParameter p_txt_754_ChargesDeduct5 = new SqlParameter("@Charges5", SqlDbType.VarChar);
        p_txt_754_ChargesDeduct5.Value = txt_754_ChargesDeduct5.Text.Trim();
        SqlParameter p_txt_754_ChargesDeduct6 = new SqlParameter("@Charges6", SqlDbType.VarChar);
        p_txt_754_ChargesDeduct6.Value = txt_754_ChargesDeduct6.Text.Trim();

        SqlParameter p_txt_754_ChargesAdded1 = new SqlParameter("@ChargesAdded1", SqlDbType.VarChar);
        p_txt_754_ChargesAdded1.Value = txt_754_ChargesAdded1.Text.Trim();
        SqlParameter p_txt_754_ChargesAdded2 = new SqlParameter("@ChargesAdded2", SqlDbType.VarChar);
        p_txt_754_ChargesAdded2.Value = txt_754_ChargesAdded2.Text.Trim();
        SqlParameter p_txt_754_ChargesAdded3 = new SqlParameter("@ChargesAdded3", SqlDbType.VarChar);
        p_txt_754_ChargesAdded3.Value = txt_754_ChargesAdded3.Text.Trim();
        SqlParameter p_txt_754_ChargesAdded4 = new SqlParameter("@ChargesAdded4", SqlDbType.VarChar);
        p_txt_754_ChargesAdded4.Value = txt_754_ChargesAdded4.Text.Trim();
        SqlParameter p_txt_754_ChargesAdded5 = new SqlParameter("@ChargesAdded5", SqlDbType.VarChar);
        p_txt_754_ChargesAdded5.Value = txt_754_ChargesAdded5.Text.Trim();
        SqlParameter p_txt_754_ChargesAdded6 = new SqlParameter("@ChargesAdded6", SqlDbType.VarChar);
        p_txt_754_ChargesAdded6.Value = txt_754_ChargesAdded6.Text.Trim();

        SqlParameter p_ddlTotalAmtclamd_754 = new SqlParameter("@ddlTotalAmtClaimed", SqlDbType.VarChar);
        p_ddlTotalAmtclamd_754.Value = ddlTotalAmtclamd_754.SelectedValue;
        SqlParameter p_txt_754_TotalAmtClmd_Date = new SqlParameter("@TotalAmtClaimed_Date", SqlDbType.VarChar);
        p_txt_754_TotalAmtClmd_Date.Value = txt_754_TotalAmtClmd_Date.Text.Trim();
        SqlParameter p_ddl_754_TotalAmtClmd_Ccy = new SqlParameter("@TotalAmtClaimed_Curr", SqlDbType.VarChar);
        p_ddl_754_TotalAmtClmd_Ccy.Value = ddl_754_TotalAmtClmd_Ccy.Text.Trim();
        if (Regex.IsMatch(txt_754_TotalAmtClmd_Amt.Text, @"\.\d\d\d"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal in Total amount claimed field');", true);
            txt_754_TotalAmtClmd_Amt.Focus();
        }

        if (txt_754_TotalAmtClmd_Amt.Text.Contains(','))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please do not enter (,) in Total amount claimed field');", true);
            txt_754_TotalAmtClmd_Amt.Focus();
        }
        SqlParameter p_txt_754_TotalAmtClmd_Amt = new SqlParameter("@TotalAmtClaimed_Amt", SqlDbType.VarChar);
        p_txt_754_TotalAmtClmd_Amt.Value = txt_754_TotalAmtClmd_Amt.Text.Trim();
        if (ddl_754_TotalAmtClmd_Ccy.Text != "")
        {
            if (ddl_754_TotalAmtClmd_Ccy.Text != ddl_PrinAmtPaidAccNegoCurr_754.Text)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('The currency code in the fields 32a and 34a must be the same ');", true);
            }
        }
        SqlParameter p_ddlReimbursingbank_754 = new SqlParameter("@ddlReimbursingBank", SqlDbType.VarChar);
        p_ddlReimbursingbank_754.Value = ddlReimbursingbank_754.SelectedValue;
        SqlParameter p_txtReimbursingBankAccountnumber_754 = new SqlParameter("@ReimbursingBank_PartyIdent", SqlDbType.VarChar);
        p_txtReimbursingBankAccountnumber_754.Value = txtReimbursingBankAccountnumber_754.Text.Trim();
        SqlParameter p_txtReimbursingBankAccountnumber1_754 = new SqlParameter("@ReimbursingBank_PartyIdent1", SqlDbType.VarChar);
        p_txtReimbursingBankAccountnumber1_754.Value = txtReimbursingBankAccountnumber1_754.Text.Trim();
        SqlParameter p_txtReimbursingBankIdentifiercode_754 = new SqlParameter("@ReimbursingBank_Identcode", SqlDbType.VarChar);
        p_txtReimbursingBankIdentifiercode_754.Value = txtReimbursingBankIdentifiercode_754.Text.Trim();
        SqlParameter p_txtReimbursingBankLocation_754 = new SqlParameter("@ReimbursingBank_Location", SqlDbType.VarChar);
        p_txtReimbursingBankLocation_754.Value = txtReimbursingBankLocation_754.Text.Trim();
        SqlParameter p_txtReimbursingBankName_754 = new SqlParameter("@ReimbursingBank_Name", SqlDbType.VarChar);
        p_txtReimbursingBankName_754.Value = txtReimbursingBankName_754.Text.Trim();
        SqlParameter p_txtReimbursingBankAddress1_754 = new SqlParameter("@ReimbursingBank_Add1", SqlDbType.VarChar);
        p_txtReimbursingBankAddress1_754.Value = txtReimbursingBankAddress1_754.Text.Trim();
        SqlParameter p_txtReimbursingBankAddress2_754 = new SqlParameter("@ReimbursingBank_Add2", SqlDbType.VarChar);
        p_txtReimbursingBankAddress2_754.Value = txtReimbursingBankAddress2_754.Text.Trim();
        SqlParameter p_txtReimbursingBankAddress3_754 = new SqlParameter("@ReimbursingBank_Add3", SqlDbType.VarChar);
        p_txtReimbursingBankAddress3_754.Value = txtReimbursingBankAddress3_754.Text.Trim();

        SqlParameter p_ddlAccountwithbank_754 = new SqlParameter("@ddlAccWithBank", SqlDbType.VarChar);
        p_ddlAccountwithbank_754.Value = ddlAccountwithbank_754.SelectedValue;
        SqlParameter p_txtAccountwithBankAccountnumber_754 = new SqlParameter("@AccWithBank_PartyIdent", SqlDbType.VarChar);
        p_txtAccountwithBankAccountnumber_754.Value = txtAccountwithBankAccountnumber_754.Text.Trim();
        SqlParameter p_txtAccountwithBankAccountnumber1_754 = new SqlParameter("@AccWithBank_PartyIdent1", SqlDbType.VarChar);
        p_txtAccountwithBankAccountnumber1_754.Value = txtAccountwithBankAccountnumber1_754.Text.Trim();
        SqlParameter p_txtAccountwithBankIdentifiercode_754 = new SqlParameter("@AccWithBank_Identcode", SqlDbType.VarChar);
        p_txtAccountwithBankIdentifiercode_754.Value = txtAccountwithBankIdentifiercode_754.Text.Trim();
        SqlParameter p_txtAccountwithBankLocation_754 = new SqlParameter("@AccWithBank_Location", SqlDbType.VarChar);
        p_txtAccountwithBankLocation_754.Value = txtAccountwithBankLocation_754.Text.Trim();
        SqlParameter p_txtAccountwithBankName_754 = new SqlParameter("@AccWithBank_Name", SqlDbType.VarChar);
        p_txtAccountwithBankName_754.Value = txtAccountwithBankName_754.Text.Trim();
        SqlParameter p_txtAccountwithBankAddress1_754 = new SqlParameter("@AccWithBank_Add1", SqlDbType.VarChar);
        p_txtAccountwithBankAddress1_754.Value = txtAccountwithBankAddress1_754.Text.Trim();
        SqlParameter p_txtAccountwithBankAddress2_754 = new SqlParameter("@AccWithBank_Add2", SqlDbType.VarChar);
        p_txtAccountwithBankAddress2_754.Value = txtAccountwithBankAddress2_754.Text.Trim();
        SqlParameter p_txtAccountwithBankAddress3_754 = new SqlParameter("@AccWithBank_Add3", SqlDbType.VarChar);
        p_txtAccountwithBankAddress3_754.Value = txtAccountwithBankAddress3_754.Text.Trim();

        SqlParameter p_ddlBeneficiarybank_754 = new SqlParameter("@ddlBeneficiaryBank", SqlDbType.VarChar);
        p_ddlBeneficiarybank_754.Value = ddlBeneficiarybank_754.SelectedValue;
        SqlParameter p_txtBeneficiaryBankAccountnumber_754 = new SqlParameter("@BeneficiaryBank_PartyIdent", SqlDbType.VarChar);
        p_txtBeneficiaryBankAccountnumber_754.Value = txtBeneficiaryBankAccountnumber_754.Text.Trim();
        SqlParameter p_txtBeneficiaryBankAccountnumber1_754 = new SqlParameter("@BeneficiaryBank_PartyIdent1", SqlDbType.VarChar);
        p_txtBeneficiaryBankAccountnumber1_754.Value = txtBeneficiaryBankAccountnumber1_754.Text.Trim();
        SqlParameter p_txtBeneficiaryBankIdentifiercode_754 = new SqlParameter("@BeneficiaryBank_Identcode", SqlDbType.VarChar);
        p_txtBeneficiaryBankIdentifiercode_754.Value = txtBeneficiaryBankIdentifiercode_754.Text.Trim();
        SqlParameter p_txtBeneficiaryBankName_754 = new SqlParameter("@BeneficiaryBank_Name", SqlDbType.VarChar);
        p_txtBeneficiaryBankName_754.Value = txtBeneficiaryBankName_754.Text.Trim();
        SqlParameter p_txtBeneficiaryBankAddress1_754 = new SqlParameter("@BeneficiaryBank_Add1", SqlDbType.VarChar);
        p_txtBeneficiaryBankAddress1_754.Value = txtBeneficiaryBankAddress1_754.Text.Trim();
        SqlParameter p_txtBeneficiaryBankAddress2_754 = new SqlParameter("@BeneficiaryBank_Add2", SqlDbType.VarChar);
        p_txtBeneficiaryBankAddress2_754.Value = txtBeneficiaryBankAddress2_754.Text.Trim();
        SqlParameter p_txtBeneficiaryBankAddress3_754 = new SqlParameter("@BeneficiaryBank_Add3", SqlDbType.VarChar);
        p_txtBeneficiaryBankAddress3_754.Value = txtBeneficiaryBankAddress3_754.Text.Trim();

        SqlParameter p_754_SenToRecinfo1 = new SqlParameter("@SenToRecinfo1", SqlDbType.VarChar);
        p_754_SenToRecinfo1.Value = txt_754_SenRecInfo1.Text.Trim();
        SqlParameter p_754_SenToRecinfo2 = new SqlParameter("@SenToRecinfo2", SqlDbType.VarChar);
        p_754_SenToRecinfo2.Value = txt_754_SenRecInfo2.Text.Trim();
        SqlParameter p_754_SenToRecinfo3 = new SqlParameter("@SenToRecinfo3", SqlDbType.VarChar);
        p_754_SenToRecinfo3.Value = txt_754_SenRecInfo3.Text.Trim();
        SqlParameter p_754_SenToRecinfo4 = new SqlParameter("@SenToRecinfo4", SqlDbType.VarChar);
        p_754_SenToRecinfo4.Value = txt_754_SenRecInfo4.Text.Trim();
        SqlParameter p_754_SenToRecinfo5 = new SqlParameter("@SenToRecinfo5", SqlDbType.VarChar);
        p_754_SenToRecinfo5.Value = txt_754_SenRecInfo5.Text.Trim();
        SqlParameter p_754_SenToRecinfo6 = new SqlParameter("@SenToRecinfo6", SqlDbType.VarChar);
        p_754_SenToRecinfo6.Value = txt_754_SenRecInfo6.Text.Trim();

        SqlParameter p_txt_754_Narr1 = new SqlParameter("@Narrative1", SqlDbType.VarChar);
        p_txt_754_Narr1.Value = txt_754_Narr1.Text.Trim();
        SqlParameter p_txt_754_Narr2 = new SqlParameter("@Narrative2", SqlDbType.VarChar);
        p_txt_754_Narr2.Value = txt_754_Narr2.Text.Trim();
        SqlParameter p_txt_754_Narr3 = new SqlParameter("@Narrative3", SqlDbType.VarChar);
        p_txt_754_Narr3.Value = txt_754_Narr3.Text.Trim();
        SqlParameter p_txt_754_Narr4 = new SqlParameter("@Narrative4", SqlDbType.VarChar);
        p_txt_754_Narr4.Value = txt_754_Narr4.Text.Trim();
        SqlParameter p_txt_754_Narr5 = new SqlParameter("@Narrative5", SqlDbType.VarChar);
        p_txt_754_Narr5.Value = txt_754_Narr5.Text.Trim();
        SqlParameter p_txt_754_Narr6 = new SqlParameter("@Narrative6", SqlDbType.VarChar);
        p_txt_754_Narr6.Value = txt_754_Narr6.Text.Trim();
        SqlParameter p_txt_754_Narr7 = new SqlParameter("@Narrative7", SqlDbType.VarChar);
        p_txt_754_Narr7.Value = txt_754_Narr7.Text.Trim();
        SqlParameter p_txt_754_Narr8 = new SqlParameter("@Narrative8", SqlDbType.VarChar);
        p_txt_754_Narr8.Value = txt_754_Narr8.Text.Trim();
        SqlParameter p_txt_754_Narr9 = new SqlParameter("@Narrative9", SqlDbType.VarChar);
        p_txt_754_Narr9.Value = txt_754_Narr9.Text.Trim();
        SqlParameter p_txt_754_Narr10 = new SqlParameter("@Narrative10", SqlDbType.VarChar);
        p_txt_754_Narr10.Value = txt_754_Narr10.Text.Trim();
        SqlParameter p_txt_754_Narr11 = new SqlParameter("@Narrative11", SqlDbType.VarChar);
        p_txt_754_Narr11.Value = txt_754_Narr11.Text.Trim();
        SqlParameter p_txt_754_Narr12 = new SqlParameter("@Narrative12", SqlDbType.VarChar);
        p_txt_754_Narr12.Value = txt_754_Narr12.Text.Trim();
        SqlParameter p_txt_754_Narr13 = new SqlParameter("@Narrative13", SqlDbType.VarChar);
        p_txt_754_Narr13.Value = txt_754_Narr13.Text.Trim();
        SqlParameter p_txt_754_Narr14 = new SqlParameter("@Narrative14", SqlDbType.VarChar);
        p_txt_754_Narr14.Value = txt_754_Narr14.Text.Trim();
        SqlParameter p_txt_754_Narr15 = new SqlParameter("@Narrative15", SqlDbType.VarChar);
        p_txt_754_Narr15.Value = txt_754_Narr15.Text.Trim();
        SqlParameter p_txt_754_Narr16 = new SqlParameter("@Narrative16", SqlDbType.VarChar);
        p_txt_754_Narr16.Value = txt_754_Narr16.Text.Trim();
        SqlParameter p_txt_754_Narr17 = new SqlParameter("@Narrative17", SqlDbType.VarChar);
        p_txt_754_Narr17.Value = txt_754_Narr17.Text.Trim();
        SqlParameter p_txt_754_Narr18 = new SqlParameter("@Narrative18", SqlDbType.VarChar);
        p_txt_754_Narr18.Value = txt_754_Narr18.Text.Trim();
        SqlParameter p_txt_754_Narr19 = new SqlParameter("@Narrative19", SqlDbType.VarChar);
        p_txt_754_Narr19.Value = txt_754_Narr19.Text.Trim();
        SqlParameter p_txt_754_Narr20 = new SqlParameter("@Narrative20", SqlDbType.VarChar);
        p_txt_754_Narr20.Value = txt_754_Narr20.Text.Trim();

        //if (txt_754_SenRecInfo1.Text != "" || txt_754_SenRecInfo2.Text != "" || txt_754_SenRecInfo3.Text != "" ||
        //txt_754_SenRecInfo4.Text != "" || txt_754_SenRecInfo5.Text != "" || txt_754_SenRecInfo6.Text != "" &&
        //txt_754_Narr1.Text != "" || txt_754_Narr2.Text != "" || txt_754_Narr3.Text != "" || txt_754_Narr4.Text != "" || txt_754_Narr5.Text != "" ||
        //txt_754_Narr6.Text != "" || txt_754_Narr7.Text != "" || txt_754_Narr8.Text != "" || txt_754_Narr9.Text != "" || txt_754_Narr10.Text != "" ||
        //txt_754_Narr11.Text != "" || txt_754_Narr12.Text != "" || txt_754_Narr13.Text != "" || txt_754_Narr14.Text != "" || txt_754_Narr15.Text != "" ||
        //txt_754_Narr16.Text != "" || txt_754_Narr17.Text != "" || txt_754_Narr18.Text != "" || txt_754_Narr19.Text != "" || txt_754_Narr20.Text != "")
        if (txt_754_SenRecInfo1.Text != "" && txt_754_Narr1.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 72Z or 77 may be present, but not both');", true);
        }


        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData("TF_Exp_AddUpdateSwift754", p_Branch, p_SwiftType, p_txtReceiver754, p_754SenRefno, p_754RelRef, p_ddlPrinAmtPaidAccNegoAmt_754, p_txtPrinAmtPaidAccNegoDate_754,
        p_ddl_PrinAmtPaidAccNegoCurr_754, p_txtPrinAmtPaidAccNegoAmt_754, p_ddl_754_AddAmtClamd_Ccy, p_txt_754_AddAmtClamd_Amt, p_txt_754_ChargesDeduct1, p_txt_754_ChargesDeduct2,
        p_txt_754_ChargesDeduct3, p_txt_754_ChargesDeduct4, p_txt_754_ChargesDeduct5, p_txt_754_ChargesDeduct6, p_txt_754_ChargesAdded1, p_txt_754_ChargesAdded2, p_txt_754_ChargesAdded3,
        p_txt_754_ChargesAdded4, p_txt_754_ChargesAdded5, p_txt_754_ChargesAdded6, p_ddlTotalAmtclamd_754, p_txt_754_TotalAmtClmd_Date, p_ddl_754_TotalAmtClmd_Ccy, p_txt_754_TotalAmtClmd_Amt,
        p_ddlReimbursingbank_754, p_txtReimbursingBankAccountnumber_754, p_txtReimbursingBankAccountnumber1_754, p_txtReimbursingBankIdentifiercode_754, p_txtReimbursingBankLocation_754,
        p_txtReimbursingBankName_754, p_txtReimbursingBankAddress1_754, p_txtReimbursingBankAddress2_754, p_txtReimbursingBankAddress3_754, p_ddlAccountwithbank_754,
        p_txtAccountwithBankAccountnumber_754, p_txtAccountwithBankAccountnumber1_754, p_txtAccountwithBankIdentifiercode_754, p_txtAccountwithBankLocation_754, p_txtAccountwithBankName_754,
        p_txtAccountwithBankAddress1_754, p_txtAccountwithBankAddress2_754, p_txtAccountwithBankAddress3_754, p_ddlBeneficiarybank_754, p_txtBeneficiaryBankAccountnumber_754,
        p_txtBeneficiaryBankAccountnumber1_754, p_txtBeneficiaryBankIdentifiercode_754, p_txtBeneficiaryBankName_754, p_txtBeneficiaryBankAddress1_754, p_txtBeneficiaryBankAddress2_754,
        p_txtBeneficiaryBankAddress3_754, p_754_SenToRecinfo1, p_754_SenToRecinfo2, p_754_SenToRecinfo3, p_754_SenToRecinfo4, p_754_SenToRecinfo5, p_754_SenToRecinfo6, p_txt_754_Narr1,
        p_txt_754_Narr2, p_txt_754_Narr3, p_txt_754_Narr4, p_txt_754_Narr5, p_txt_754_Narr6, p_txt_754_Narr7, p_txt_754_Narr8, p_txt_754_Narr9, p_txt_754_Narr10, p_txt_754_Narr11,
        p_txt_754_Narr12, p_txt_754_Narr13, p_txt_754_Narr14, p_txt_754_Narr15, p_txt_754_Narr16, p_txt_754_Narr17, p_txt_754_Narr18, p_txt_754_Narr19, p_txt_754_Narr20);

        if (_result == "added")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added as Draft.');", true);
        }
        else if (_result == "Updated")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Error');", true);
        }
    }
    public void Save799()
    {
        SqlParameter p_Branch = new SqlParameter("@Branch", SqlDbType.VarChar);
        p_Branch.Value = ddlBranch.SelectedItem.Text;
        SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
        p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;
        if (txtReceiver799.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter receiver');", true);
            txtReceiver799.Focus();
        }
        SqlParameter p_txtReceiver799 = new SqlParameter("@Receiver", SqlDbType.VarChar);
        p_txtReceiver799.Value = txtReceiver799.Text.Trim();

        if (txt_799_transRefNo.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Transaction Reference Number');", true);
        }
        if (txt_799_transRefNo.Text.Contains("//"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number contains two consecutive slashes //');", true);
        }
        else if (txt_799_transRefNo.Text.StartsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number start with slash(/)');", true);
        }
        else if (txt_799_transRefNo.Text.EndsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number end with slash(/)');", true);
        }
        SqlParameter p_799TranRefno = new SqlParameter("@Trans_Ref_No", SqlDbType.VarChar);
        p_799TranRefno.Value = txt_799_transRefNo.Text.Trim();

        if (txt_799_RelRef.Text.Contains("//"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference contains two consecutive slashes //');", true);
        }
        else if (txt_799_RelRef.Text.StartsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference start with slash(/)');", true);
        }
        else if (txt_799_RelRef.Text.EndsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Relateds Reference end with slash(/)');", true);
        }
        SqlParameter p_799RelatedRef = new SqlParameter("@Related_Ref", SqlDbType.VarChar);
        p_799RelatedRef.Value = txt_799_RelRef.Text.Trim();
        SqlParameter p_799Narr1 = new SqlParameter("@Narrative1", SqlDbType.VarChar);
        p_799Narr1.Value = txt_799_Narr1.Text.Trim();
        SqlParameter p_799Narr2 = new SqlParameter("@Narrative2", SqlDbType.VarChar);
        p_799Narr2.Value = txt_799_Narr2.Text.Trim();
        SqlParameter p_799Narr3 = new SqlParameter("@Narrative3", SqlDbType.VarChar);
        p_799Narr3.Value = txt_799_Narr3.Text.Trim();
        SqlParameter p_799Narr4 = new SqlParameter("@Narrative4", SqlDbType.VarChar);
        p_799Narr4.Value = txt_799_Narr4.Text.Trim();
        SqlParameter p_799Narr5 = new SqlParameter("@Narrative5", SqlDbType.VarChar);
        p_799Narr5.Value = txt_799_Narr5.Text.Trim();
        SqlParameter p_799Narr6 = new SqlParameter("@Narrative6", SqlDbType.VarChar);
        p_799Narr6.Value = txt_799_Narr6.Text.Trim();
        SqlParameter p_799Narr7 = new SqlParameter("@Narrative7", SqlDbType.VarChar);
        p_799Narr7.Value = txt_799_Narr7.Text.Trim();
        SqlParameter p_799Narr8 = new SqlParameter("@Narrative8", SqlDbType.VarChar);
        p_799Narr8.Value = txt_799_Narr8.Text.Trim();
        SqlParameter p_799Narr9 = new SqlParameter("@Narrative9", SqlDbType.VarChar);
        p_799Narr9.Value = txt_799_Narr9.Text.Trim();
        SqlParameter p_799Narr10 = new SqlParameter("@Narrative10", SqlDbType.VarChar);
        p_799Narr10.Value = txt_799_Narr10.Text.Trim();
        SqlParameter p_799Narr11 = new SqlParameter("@Narrative11", SqlDbType.VarChar);
        p_799Narr11.Value = txt_799_Narr11.Text.Trim();
        SqlParameter p_799Narr12 = new SqlParameter("@Narrative12", SqlDbType.VarChar);
        p_799Narr12.Value = txt_799_Narr12.Text.Trim();
        SqlParameter p_799Narr13 = new SqlParameter("@Narrative13", SqlDbType.VarChar);
        p_799Narr13.Value = txt_799_Narr13.Text.Trim();
        SqlParameter p_799Narr14 = new SqlParameter("@Narrative14", SqlDbType.VarChar);
        p_799Narr14.Value = txt_799_Narr14.Text.Trim();
        SqlParameter p_799Narr15 = new SqlParameter("@Narrative15", SqlDbType.VarChar);
        p_799Narr15.Value = txt_799_Narr15.Text.Trim();
        SqlParameter p_799Narr16 = new SqlParameter("@Narrative16", SqlDbType.VarChar);
        p_799Narr16.Value = txt_799_Narr16.Text.Trim();
        SqlParameter p_799Narr17 = new SqlParameter("@Narrative17", SqlDbType.VarChar);
        p_799Narr17.Value = txt_799_Narr17.Text.Trim();
        SqlParameter p_799Narr18 = new SqlParameter("@Narrative18", SqlDbType.VarChar);
        p_799Narr18.Value = txt_799_Narr18.Text.Trim();
        SqlParameter p_799Narr19 = new SqlParameter("@Narrative19", SqlDbType.VarChar);
        p_799Narr19.Value = txt_799_Narr19.Text.Trim();
        SqlParameter p_799Narr20 = new SqlParameter("@Narrative20", SqlDbType.VarChar);
        p_799Narr20.Value = txt_799_Narr20.Text.Trim();
        SqlParameter p_799Narr21 = new SqlParameter("@Narrative21", SqlDbType.VarChar);
        p_799Narr21.Value = txt_799_Narr21.Text.Trim();
        SqlParameter p_799Narr22 = new SqlParameter("@Narrative22", SqlDbType.VarChar);
        p_799Narr22.Value = txt_799_Narr22.Text.Trim();
        SqlParameter p_799Narr23 = new SqlParameter("@Narrative23", SqlDbType.VarChar);
        p_799Narr23.Value = txt_799_Narr23.Text.Trim();
        SqlParameter p_799Narr24 = new SqlParameter("@Narrative24", SqlDbType.VarChar);
        p_799Narr24.Value = txt_799_Narr24.Text.Trim();
        SqlParameter p_799Narr25 = new SqlParameter("@Narrative25", SqlDbType.VarChar);
        p_799Narr25.Value = txt_799_Narr25.Text.Trim();
        SqlParameter p_799Narr26 = new SqlParameter("@Narrative26", SqlDbType.VarChar);
        p_799Narr26.Value = txt_799_Narr26.Text.Trim();
        SqlParameter p_799Narr27 = new SqlParameter("@Narrative27", SqlDbType.VarChar);
        p_799Narr27.Value = txt_799_Narr27.Text.Trim();
        SqlParameter p_799Narr28 = new SqlParameter("@Narrative28", SqlDbType.VarChar);
        p_799Narr28.Value = txt_799_Narr28.Text.Trim();
        SqlParameter p_799Narr29 = new SqlParameter("@Narrative29", SqlDbType.VarChar);
        p_799Narr29.Value = txt_799_Narr29.Text.Trim();
        SqlParameter p_799Narr30 = new SqlParameter("@Narrative30", SqlDbType.VarChar);
        p_799Narr30.Value = txt_799_Narr30.Text.Trim();
        SqlParameter p_799Narr31 = new SqlParameter("@Narrative31", SqlDbType.VarChar);
        p_799Narr31.Value = txt_799_Narr31.Text.Trim();
        SqlParameter p_799Narr32 = new SqlParameter("@Narrative32", SqlDbType.VarChar);
        p_799Narr32.Value = txt_799_Narr32.Text.Trim();
        SqlParameter p_799Narr33 = new SqlParameter("@Narrative33", SqlDbType.VarChar);
        p_799Narr33.Value = txt_799_Narr33.Text.Trim();
        SqlParameter p_799Narr34 = new SqlParameter("@Narrative34", SqlDbType.VarChar);
        p_799Narr34.Value = txt_799_Narr34.Text.Trim();
        SqlParameter p_799Narr35 = new SqlParameter("@Narrative35", SqlDbType.VarChar);
        p_799Narr35.Value = txt_799_Narr35.Text.Trim();
        if (txt_799_Narr1.Text == "" && txt_799_Narr2.Text == "" && txt_799_Narr3.Text == "" && txt_799_Narr4.Text == "" && txt_799_Narr5.Text == "" && txt_799_Narr6.Text == "" && txt_799_Narr7.Text == "" && txt_799_Narr8.Text == "" && txt_799_Narr9.Text == "" && txt_799_Narr10.Text == ""
            && txt_799_Narr11.Text == "" && txt_799_Narr12.Text == "" && txt_799_Narr13.Text == "" && txt_799_Narr14.Text == "" && txt_799_Narr15.Text == "" && txt_799_Narr16.Text == "" && txt_799_Narr17.Text == "" && txt_799_Narr18.Text == "" && txt_799_Narr19.Text == "" && txt_799_Narr20.Text == ""
            && txt_799_Narr21.Text == "" && txt_799_Narr22.Text == "" && txt_799_Narr23.Text == "" && txt_799_Narr24.Text == "" && txt_799_Narr25.Text == "" && txt_799_Narr26.Text == "" && txt_799_Narr27.Text == "" && txt_799_Narr28.Text == "" && txt_799_Narr29.Text == "" && txt_799_Narr30.Text == ""
            && txt_799_Narr31.Text == "" && txt_799_Narr32.Text == "" && txt_799_Narr33.Text == "" && txt_799_Narr34.Text == "" && txt_799_Narr35.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Narrative');", true);
        }


        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData("TF_Exp_AddUpdateSwift799", p_Branch, p_SwiftType, p_txtReceiver799, p_799TranRefno, p_799RelatedRef, p_799Narr1, p_799Narr2, p_799Narr3, p_799Narr4, p_799Narr5,
            p_799Narr6, p_799Narr7, p_799Narr8, p_799Narr9, p_799Narr10, p_799Narr11, p_799Narr12, p_799Narr13, p_799Narr14, p_799Narr15, p_799Narr16, p_799Narr17, p_799Narr18,
            p_799Narr19, p_799Narr20, p_799Narr21, p_799Narr22, p_799Narr23, p_799Narr24, p_799Narr25, p_799Narr26, p_799Narr27, p_799Narr28, p_799Narr29, p_799Narr30,
           p_799Narr31, p_799Narr32, p_799Narr33, p_799Narr34, p_799Narr35);

        if (_result == "added")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added as Draft.');", true);
        }
        else if (_result == "Updated")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Error');", true);
        }

    }
    public void Sendtochecker742()
    {
        string hdnUserName = Session["userName"].ToString();
        SqlParameter p_Branch = new SqlParameter("@Branch", SqlDbType.VarChar);
        p_Branch.Value = ddlBranch.SelectedItem.Text;
        SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
        p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;
        SqlParameter p_txtReceiver742 = new SqlParameter("@Receiver", SqlDbType.VarChar);
        p_txtReceiver742.Value = txtReceiver742.Text.Trim();
        SqlParameter p_txt_742_ClaimBankRef = new SqlParameter("@ClaimBankRef", SqlDbType.VarChar);
        p_txt_742_ClaimBankRef.Value = txt_742_ClaimBankRef.Text.Trim();

        SqlParameter p_txt_742_DocumCreditNo = new SqlParameter("@DocumCreditNo", SqlDbType.VarChar);
        p_txt_742_DocumCreditNo.Value = txt_742_DocumCreditNo.Text.Trim();

        SqlParameter p_txt_742_Dateofissue = new SqlParameter("@Dateofissue", SqlDbType.VarChar);
        p_txt_742_Dateofissue.Value = txt_742_Dateofissue.Text.Trim();

        SqlParameter p_ddl_Issuingbank_742 = new SqlParameter("@ddlIssuingBank", SqlDbType.VarChar);
        p_ddl_Issuingbank_742.Value = ddl_Issuingbank_742.SelectedValue;

        SqlParameter p_txtIssuingBankAccountnumber_742 = new SqlParameter("@IssuingBank_PartyIdent", SqlDbType.VarChar);
        p_txtIssuingBankAccountnumber_742.Value = txtIssuingBankAccountnumber_742.Text.Trim();

        SqlParameter p_txtIssuingBankAccountnumber1_742 = new SqlParameter("@IssuingBank_PartyIdent1", SqlDbType.VarChar);
        p_txtIssuingBankAccountnumber1_742.Value = txtIssuingBankAccountnumber1_742.Text.Trim();

        SqlParameter p_txtIssuingBankIdentifiercode_742 = new SqlParameter("@IssuingBank_Identcode", SqlDbType.VarChar);
        p_txtIssuingBankIdentifiercode_742.Value = txtIssuingBankIdentifiercode_742.Text.Trim();

        SqlParameter p_txtIssuingBankName_742 = new SqlParameter("@IssuingBank_Name", SqlDbType.VarChar);
        p_txtIssuingBankName_742.Value = txtIssuingBankName_742.Text.Trim();
        SqlParameter p_txtIssuingBankAddress1_742 = new SqlParameter("@IssuingBank_Add1", SqlDbType.VarChar);
        p_txtIssuingBankAddress1_742.Value = txtIssuingBankAddress1_742.Text.Trim();
        SqlParameter p_txtIssuingBankAddress2_742 = new SqlParameter("@IssuingBank_Add2", SqlDbType.VarChar);
        p_txtIssuingBankAddress2_742.Value = txtIssuingBankAddress2_742.Text.Trim();
        SqlParameter p_txtIssuingBankAddress3_742 = new SqlParameter("@IssuingBank_Add3", SqlDbType.VarChar);
        p_txtIssuingBankAddress3_742.Value = txtIssuingBankAddress3_742.Text.Trim();
        SqlParameter p_ddl_742_PrinAmtClmd_Ccy = new SqlParameter("@PrincipalAmtClaimed_Curr", SqlDbType.VarChar);
        p_ddl_742_PrinAmtClmd_Ccy.Value = ddl_742_PrinAmtClmd_Ccy.SelectedItem.Text.Trim();

        SqlParameter p_txt_742_PrinAmtClmd_Amt = new SqlParameter("@PrincipalAmtClaimed_Amt", SqlDbType.VarChar);
        p_txt_742_PrinAmtClmd_Amt.Value = txt_742_PrinAmtClmd_Amt.Text.Trim();

        SqlParameter p_ddl_742_AddAmtClamd_Ccy = new SqlParameter("@AddnlAmt_Curr", SqlDbType.VarChar);
        p_ddl_742_AddAmtClamd_Ccy.Value = ddl_742_AddAmtClamd_Ccy.SelectedItem.Text.Trim();

        SqlParameter p_txt_742_AddAmtClamd_Amt = new SqlParameter("@AddnlAmt_Amt", SqlDbType.VarChar);
        p_txt_742_AddAmtClamd_Amt.Value = txt_742_AddAmtClamd_Amt.Text.Trim();

        SqlParameter p_txt_742_Charges1 = new SqlParameter("@Charges1", SqlDbType.VarChar);
        p_txt_742_Charges1.Value = txt_742_Charges1.Text.Trim();
        SqlParameter p_txt_742_Charges2 = new SqlParameter("@Charges2", SqlDbType.VarChar);
        p_txt_742_Charges2.Value = txt_742_Charges2.Text.Trim();
        SqlParameter p_txt_742_Charges3 = new SqlParameter("@Charges3", SqlDbType.VarChar);
        p_txt_742_Charges3.Value = txt_742_Charges3.Text.Trim();
        SqlParameter p_txt_742_Charges4 = new SqlParameter("@Charges4", SqlDbType.VarChar);
        p_txt_742_Charges4.Value = txt_742_Charges4.Text.Trim();
        SqlParameter p_txt_742_Charges5 = new SqlParameter("@Charges5", SqlDbType.VarChar);
        p_txt_742_Charges5.Value = txt_742_Charges5.Text.Trim();
        SqlParameter p_txt_742_Charges6 = new SqlParameter("@Charges6", SqlDbType.VarChar);
        p_txt_742_Charges6.Value = txt_742_Charges6.Text.Trim();

        SqlParameter p_ddlTotalAmtclamd_742 = new SqlParameter("@ddlTotalAmtClaimed", SqlDbType.VarChar);
        p_ddlTotalAmtclamd_742.Value = ddlTotalAmtclamd_742.SelectedValue;

        SqlParameter p_txt_742_TotalAmtClmd_Date = new SqlParameter("@TotalAmtClaimed_Date", SqlDbType.VarChar);
        p_txt_742_TotalAmtClmd_Date.Value = txt_742_TotalAmtClmd_Date.Text.Trim();

        SqlParameter p_ddl_742_TotalAmtClmd_Ccy = new SqlParameter("@TotalAmtClaimed_Curr", SqlDbType.VarChar);
        p_ddl_742_TotalAmtClmd_Ccy.Value = ddl_742_TotalAmtClmd_Ccy.SelectedItem.Text.Trim();

        SqlParameter p_txt_742_TotalAmtClmd_Amt = new SqlParameter("@TotalAmtClaimed_Amt", SqlDbType.VarChar);
        p_txt_742_TotalAmtClmd_Amt.Value = txt_742_TotalAmtClmd_Amt.Text.Trim();

        SqlParameter p_ddlAccountwithbank_742 = new SqlParameter("@ddlAccWithBank", SqlDbType.VarChar);
        p_ddlAccountwithbank_742.Value = ddlAccountwithbank_742.SelectedValue;
        SqlParameter p_txtAccountwithBankAccountnumber_742 = new SqlParameter("@AccWithBank_PartyIdent", SqlDbType.VarChar);
        p_txtAccountwithBankAccountnumber_742.Value = txtAccountwithBankAccountnumber_742.Text.Trim();
        SqlParameter p_txtAccountwithBankAccountnumber1_742 = new SqlParameter("@AccWithBank_PartyIdent1", SqlDbType.VarChar);
        p_txtAccountwithBankAccountnumber1_742.Value = txtAccountwithBankAccountnumber1_742.Text.Trim();
        SqlParameter p_txtAccountwithBankIdentifiercode_742 = new SqlParameter("@AccWithBank_Identcode", SqlDbType.VarChar);
        p_txtAccountwithBankIdentifiercode_742.Value = txtAccountwithBankIdentifiercode_742.Text.Trim();
        SqlParameter p_txtAccountwithBankLocation_742 = new SqlParameter("@AccWithBank_Location", SqlDbType.VarChar);
        p_txtAccountwithBankLocation_742.Value = txtAccountwithBankLocation_742.Text.Trim();
        SqlParameter p_txtAccountwithBankName_742 = new SqlParameter("@AccWithBank_Name", SqlDbType.VarChar);
        p_txtAccountwithBankName_742.Value = txtAccountwithBankName_742.Text.Trim();
        SqlParameter p_txtAccountwithBankAddress1_742 = new SqlParameter("@AccWithBank_Add1", SqlDbType.VarChar);
        p_txtAccountwithBankAddress1_742.Value = txtAccountwithBankAddress1_742.Text.Trim();
        SqlParameter p_txtAccountwithBankAddress2_742 = new SqlParameter("@AccWithBank_Add2", SqlDbType.VarChar);
        p_txtAccountwithBankAddress2_742.Value = txtAccountwithBankAddress2_742.Text.Trim();
        SqlParameter p_txtAccountwithBankAddress3_742 = new SqlParameter("@AccWithBank_Add3", SqlDbType.VarChar);
        p_txtAccountwithBankAddress3_742.Value = txtAccountwithBankAddress3_742.Text.Trim();

        SqlParameter p_ddlBeneficiarybank_742 = new SqlParameter("@ddlBeneficiaryBank", SqlDbType.VarChar);
        p_ddlBeneficiarybank_742.Value = ddlBeneficiarybank_742.SelectedValue;
        SqlParameter p_txtBeneficiaryBankAccountnumber_742 = new SqlParameter("@BeneficiaryBank_PartyIdent", SqlDbType.VarChar);
        p_txtBeneficiaryBankAccountnumber_742.Value = txtBeneficiaryBankAccountnumber_742.Text.Trim();
        SqlParameter p_txtBeneficiaryBankAccountnumber1_742 = new SqlParameter("@BeneficiaryBank_PartyIdent1", SqlDbType.VarChar);
        p_txtBeneficiaryBankAccountnumber1_742.Value = txtBeneficiaryBankAccountnumber1_742.Text.Trim();
        SqlParameter p_txtBeneficiaryBankIdentifiercode_742 = new SqlParameter("@BeneficiaryBank_Identcode", SqlDbType.VarChar);
        p_txtBeneficiaryBankIdentifiercode_742.Value = txtBeneficiaryBankIdentifiercode_742.Text.Trim();
        SqlParameter p_txtBeneficiaryBankName_742 = new SqlParameter("@BeneficiaryBank_Name", SqlDbType.VarChar);
        p_txtBeneficiaryBankName_742.Value = txtBeneficiaryBankName_742.Text.Trim();
        SqlParameter p_txtBeneficiaryBankAddress1_742 = new SqlParameter("@BeneficiaryBank_Add1", SqlDbType.VarChar);
        p_txtBeneficiaryBankAddress1_742.Value = txtBeneficiaryBankAddress1_742.Text.Trim();
        SqlParameter p_txtBeneficiaryBankAddress2_742 = new SqlParameter("@BeneficiaryBank_Add2", SqlDbType.VarChar);
        p_txtBeneficiaryBankAddress2_742.Value = txtBeneficiaryBankAddress2_742.Text.Trim();
        SqlParameter p_txtBeneficiaryBankAddress3_742 = new SqlParameter("@BeneficiaryBank_Add3", SqlDbType.VarChar);
        p_txtBeneficiaryBankAddress3_742.Value = txtBeneficiaryBankAddress3_742.Text.Trim();

        SqlParameter p_742_SenToRecinfo1 = new SqlParameter("@SenToRecinfo1", SqlDbType.VarChar);
        p_742_SenToRecinfo1.Value = txt_742_SenRecInfo1.Text.Trim();
        SqlParameter p_742_SenToRecinfo2 = new SqlParameter("@SenToRecinfo2", SqlDbType.VarChar);
        p_742_SenToRecinfo2.Value = txt_742_SenRecInfo2.Text.Trim();
        SqlParameter p_742_SenToRecinfo3 = new SqlParameter("@SenToRecinfo3", SqlDbType.VarChar);
        p_742_SenToRecinfo3.Value = txt_742_SenRecInfo3.Text.Trim();
        SqlParameter p_742_SenToRecinfo4 = new SqlParameter("@SenToRecinfo4", SqlDbType.VarChar);
        p_742_SenToRecinfo4.Value = txt_742_SenRecInfo4.Text.Trim();
        SqlParameter p_742_SenToRecinfo5 = new SqlParameter("@SenToRecinfo5", SqlDbType.VarChar);
        p_742_SenToRecinfo5.Value = txt_742_SenRecInfo5.Text.Trim();
        SqlParameter p_742_SenToRecinfo6 = new SqlParameter("@SenToRecinfo6", SqlDbType.VarChar);
        p_742_SenToRecinfo6.Value = txt_742_SenRecInfo6.Text.Trim();

        SqlParameter P_AddedBy = new SqlParameter("@AddedBy", hdnUserName);
        SqlParameter P_AddedDate = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        SqlParameter P_UpdatedBy = new SqlParameter("@UpdatedBy", hdnUserName);
        SqlParameter P_UpdatedDate = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData("TF_Exp_AddUpdateSwift742", p_Branch, p_SwiftType, p_txtReceiver742, p_txt_742_ClaimBankRef, p_txt_742_DocumCreditNo, p_txt_742_Dateofissue, p_ddl_Issuingbank_742,
            p_txtIssuingBankAccountnumber_742, p_txtIssuingBankAccountnumber1_742, p_txtIssuingBankIdentifiercode_742, p_txtIssuingBankName_742, p_txtIssuingBankAddress1_742,
            p_txtIssuingBankAddress2_742, p_txtIssuingBankAddress3_742, p_ddl_742_PrinAmtClmd_Ccy, p_txt_742_PrinAmtClmd_Amt, p_ddl_742_AddAmtClamd_Ccy, p_txt_742_AddAmtClamd_Amt,
            p_txt_742_Charges1, p_txt_742_Charges2, p_txt_742_Charges3, p_txt_742_Charges4, p_txt_742_Charges5, p_txt_742_Charges6, p_ddlTotalAmtclamd_742, p_txt_742_TotalAmtClmd_Date,
            p_ddl_742_TotalAmtClmd_Ccy, p_txt_742_TotalAmtClmd_Amt, p_ddlAccountwithbank_742, p_txtAccountwithBankAccountnumber_742, p_txtAccountwithBankAccountnumber1_742,
            p_txtAccountwithBankIdentifiercode_742, p_txtAccountwithBankLocation_742, p_txtAccountwithBankName_742, p_txtAccountwithBankAddress1_742, p_txtAccountwithBankAddress2_742,
            p_txtAccountwithBankAddress3_742, p_ddlBeneficiarybank_742, p_txtBeneficiaryBankAccountnumber_742, p_txtBeneficiaryBankAccountnumber1_742, p_txtBeneficiaryBankIdentifiercode_742,
            p_txtBeneficiaryBankName_742, p_txtBeneficiaryBankAddress1_742, p_txtBeneficiaryBankAddress2_742, p_txtBeneficiaryBankAddress3_742, p_742_SenToRecinfo1,
            p_742_SenToRecinfo2, p_742_SenToRecinfo3, p_742_SenToRecinfo4, p_742_SenToRecinfo5, p_742_SenToRecinfo6, P_AddedBy, P_AddedDate, P_UpdatedBy, P_UpdatedDate);

        if (_result == "added")
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added as Draft .');", true);
        }
        else if (_result == "Updated")
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated successfully.');", true);
        }
        else
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Error while saving the Record');", true);
        }
    }
    public void Sendtochecker754()
    {
        string hdnUserName = Session["userName"].ToString();
        SqlParameter p_Branch = new SqlParameter("@Branch", SqlDbType.VarChar);
        p_Branch.Value = ddlBranch.SelectedItem.Text;
        SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
        p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;

        SqlParameter p_txtReceiver754 = new SqlParameter("@Receiver", SqlDbType.VarChar);
        p_txtReceiver754.Value = txtReceiver754.Text.Trim();


        SqlParameter p_754SenRefno = new SqlParameter("@Send_Ref", SqlDbType.VarChar);
        p_754SenRefno.Value = txt_754_SenRef.Text.Trim();


        SqlParameter p_754RelRef = new SqlParameter("@Related_Ref", SqlDbType.VarChar);
        p_754RelRef.Value = txt_754_RelRef.Text.Trim();

        SqlParameter p_ddlPrinAmtPaidAccNegoAmt_754 = new SqlParameter("@ddlPrincipalAmtPaidAccptdNego", SqlDbType.VarChar);
        p_ddlPrinAmtPaidAccNegoAmt_754.Value = ddlPrinAmtPaidAccNego_754.SelectedValue;



        SqlParameter p_txtPrinAmtPaidAccNegoDate_754 = new SqlParameter("@PrincipalAmtPaidAccptdNego_Date", SqlDbType.VarChar);
        p_txtPrinAmtPaidAccNegoDate_754.Value = txtPrinAmtPaidAccNegoDate_754.Text.Trim();
        SqlParameter p_ddl_PrinAmtPaidAccNegoCurr_754 = new SqlParameter("@PrincipalAmtPaidAccptdNego_Curr", SqlDbType.VarChar);
        p_ddl_PrinAmtPaidAccNegoCurr_754.Value = ddl_PrinAmtPaidAccNegoCurr_754.Text.Trim();

        SqlParameter p_txtPrinAmtPaidAccNegoAmt_754 = new SqlParameter("@PrincipalAmtPaidAccptdNego_Amt", SqlDbType.VarChar);
        p_txtPrinAmtPaidAccNegoAmt_754.Value = txtPrinAmtPaidAccNegoAmt_754.Text.Trim();

        SqlParameter p_ddl_754_AddAmtClamd_Ccy = new SqlParameter("@AddnlAmt_Curr", SqlDbType.VarChar);
        p_ddl_754_AddAmtClamd_Ccy.Value = ddl_754_AddAmtClamd_Ccy.Text.Trim();


        SqlParameter p_txt_754_AddAmtClamd_Amt = new SqlParameter("@AddnlAmt_Amt", SqlDbType.VarChar);
        p_txt_754_AddAmtClamd_Amt.Value = txt_754_AddAmtClamd_Amt.Text.Trim();

        SqlParameter p_txt_754_ChargesDeduct1 = new SqlParameter("@Charges1", SqlDbType.VarChar);
        p_txt_754_ChargesDeduct1.Value = txt_754_ChargesDeduct1.Text.Trim();
        SqlParameter p_txt_754_ChargesDeduct2 = new SqlParameter("@Charges2", SqlDbType.VarChar);
        p_txt_754_ChargesDeduct2.Value = txt_754_ChargesDeduct2.Text.Trim();
        SqlParameter p_txt_754_ChargesDeduct3 = new SqlParameter("@Charges3", SqlDbType.VarChar);
        p_txt_754_ChargesDeduct3.Value = txt_754_ChargesDeduct3.Text.Trim();
        SqlParameter p_txt_754_ChargesDeduct4 = new SqlParameter("@Charges4", SqlDbType.VarChar);
        p_txt_754_ChargesDeduct4.Value = txt_754_ChargesDeduct4.Text.Trim();
        SqlParameter p_txt_754_ChargesDeduct5 = new SqlParameter("@Charges5", SqlDbType.VarChar);
        p_txt_754_ChargesDeduct5.Value = txt_754_ChargesDeduct5.Text.Trim();
        SqlParameter p_txt_754_ChargesDeduct6 = new SqlParameter("@Charges6", SqlDbType.VarChar);
        p_txt_754_ChargesDeduct6.Value = txt_754_ChargesDeduct6.Text.Trim();

        SqlParameter p_txt_754_ChargesAdded1 = new SqlParameter("@ChargesAdded1", SqlDbType.VarChar);
        p_txt_754_ChargesAdded1.Value = txt_754_ChargesAdded1.Text.Trim();
        SqlParameter p_txt_754_ChargesAdded2 = new SqlParameter("@ChargesAdded2", SqlDbType.VarChar);
        p_txt_754_ChargesAdded2.Value = txt_754_ChargesAdded2.Text.Trim();
        SqlParameter p_txt_754_ChargesAdded3 = new SqlParameter("@ChargesAdded3", SqlDbType.VarChar);
        p_txt_754_ChargesAdded3.Value = txt_754_ChargesAdded3.Text.Trim();
        SqlParameter p_txt_754_ChargesAdded4 = new SqlParameter("@ChargesAdded4", SqlDbType.VarChar);
        p_txt_754_ChargesAdded4.Value = txt_754_ChargesAdded4.Text.Trim();
        SqlParameter p_txt_754_ChargesAdded5 = new SqlParameter("@ChargesAdded5", SqlDbType.VarChar);
        p_txt_754_ChargesAdded5.Value = txt_754_ChargesAdded5.Text.Trim();
        SqlParameter p_txt_754_ChargesAdded6 = new SqlParameter("@ChargesAdded6", SqlDbType.VarChar);
        p_txt_754_ChargesAdded6.Value = txt_754_ChargesAdded6.Text.Trim();

        SqlParameter p_ddlTotalAmtclamd_754 = new SqlParameter("@ddlTotalAmtClaimed", SqlDbType.VarChar);
        p_ddlTotalAmtclamd_754.Value = ddlTotalAmtclamd_754.SelectedValue;
        SqlParameter p_txt_754_TotalAmtClmd_Date = new SqlParameter("@TotalAmtClaimed_Date", SqlDbType.VarChar);
        p_txt_754_TotalAmtClmd_Date.Value = txt_754_TotalAmtClmd_Date.Text.Trim();
        SqlParameter p_ddl_754_TotalAmtClmd_Ccy = new SqlParameter("@TotalAmtClaimed_Curr", SqlDbType.VarChar);
        p_ddl_754_TotalAmtClmd_Ccy.Value = ddl_754_TotalAmtClmd_Ccy.Text.Trim();

        SqlParameter p_txt_754_TotalAmtClmd_Amt = new SqlParameter("@TotalAmtClaimed_Amt", SqlDbType.VarChar);
        p_txt_754_TotalAmtClmd_Amt.Value = txt_754_TotalAmtClmd_Amt.Text.Trim();

        SqlParameter p_ddlReimbursingbank_754 = new SqlParameter("@ddlReimbursingBank", SqlDbType.VarChar);
        p_ddlReimbursingbank_754.Value = ddlReimbursingbank_754.SelectedValue;
        SqlParameter p_txtReimbursingBankAccountnumber_754 = new SqlParameter("@ReimbursingBank_PartyIdent", SqlDbType.VarChar);
        p_txtReimbursingBankAccountnumber_754.Value = txtReimbursingBankAccountnumber_754.Text.Trim();
        SqlParameter p_txtReimbursingBankAccountnumber1_754 = new SqlParameter("@ReimbursingBank_PartyIdent1", SqlDbType.VarChar);
        p_txtReimbursingBankAccountnumber1_754.Value = txtReimbursingBankAccountnumber1_754.Text.Trim();
        SqlParameter p_txtReimbursingBankIdentifiercode_754 = new SqlParameter("@ReimbursingBank_Identcode", SqlDbType.VarChar);
        p_txtReimbursingBankIdentifiercode_754.Value = txtReimbursingBankIdentifiercode_754.Text.Trim();
        SqlParameter p_txtReimbursingBankLocation_754 = new SqlParameter("@ReimbursingBank_Location", SqlDbType.VarChar);
        p_txtReimbursingBankLocation_754.Value = txtReimbursingBankLocation_754.Text.Trim();
        SqlParameter p_txtReimbursingBankName_754 = new SqlParameter("@ReimbursingBank_Name", SqlDbType.VarChar);
        p_txtReimbursingBankName_754.Value = txtReimbursingBankName_754.Text.Trim();
        SqlParameter p_txtReimbursingBankAddress1_754 = new SqlParameter("@ReimbursingBank_Add1", SqlDbType.VarChar);
        p_txtReimbursingBankAddress1_754.Value = txtReimbursingBankAddress1_754.Text.Trim();
        SqlParameter p_txtReimbursingBankAddress2_754 = new SqlParameter("@ReimbursingBank_Add2", SqlDbType.VarChar);
        p_txtReimbursingBankAddress2_754.Value = txtReimbursingBankAddress2_754.Text.Trim();
        SqlParameter p_txtReimbursingBankAddress3_754 = new SqlParameter("@ReimbursingBank_Add3", SqlDbType.VarChar);
        p_txtReimbursingBankAddress3_754.Value = txtReimbursingBankAddress3_754.Text.Trim();

        SqlParameter p_ddlAccountwithbank_754 = new SqlParameter("@ddlAccWithBank", SqlDbType.VarChar);
        p_ddlAccountwithbank_754.Value = ddlAccountwithbank_754.SelectedValue;
        SqlParameter p_txtAccountwithBankAccountnumber_754 = new SqlParameter("@AccWithBank_PartyIdent", SqlDbType.VarChar);
        p_txtAccountwithBankAccountnumber_754.Value = txtAccountwithBankAccountnumber_754.Text.Trim();
        SqlParameter p_txtAccountwithBankAccountnumber1_754 = new SqlParameter("@AccWithBank_PartyIdent1", SqlDbType.VarChar);
        p_txtAccountwithBankAccountnumber1_754.Value = txtAccountwithBankAccountnumber1_754.Text.Trim();
        SqlParameter p_txtAccountwithBankIdentifiercode_754 = new SqlParameter("@AccWithBank_Identcode", SqlDbType.VarChar);
        p_txtAccountwithBankIdentifiercode_754.Value = txtAccountwithBankIdentifiercode_754.Text.Trim();
        SqlParameter p_txtAccountwithBankLocation_754 = new SqlParameter("@AccWithBank_Location", SqlDbType.VarChar);
        p_txtAccountwithBankLocation_754.Value = txtAccountwithBankLocation_754.Text.Trim();
        SqlParameter p_txtAccountwithBankName_754 = new SqlParameter("@AccWithBank_Name", SqlDbType.VarChar);
        p_txtAccountwithBankName_754.Value = txtAccountwithBankName_754.Text.Trim();
        SqlParameter p_txtAccountwithBankAddress1_754 = new SqlParameter("@AccWithBank_Add1", SqlDbType.VarChar);
        p_txtAccountwithBankAddress1_754.Value = txtAccountwithBankAddress1_754.Text.Trim();
        SqlParameter p_txtAccountwithBankAddress2_754 = new SqlParameter("@AccWithBank_Add2", SqlDbType.VarChar);
        p_txtAccountwithBankAddress2_754.Value = txtAccountwithBankAddress2_754.Text.Trim();
        SqlParameter p_txtAccountwithBankAddress3_754 = new SqlParameter("@AccWithBank_Add3", SqlDbType.VarChar);
        p_txtAccountwithBankAddress3_754.Value = txtAccountwithBankAddress3_754.Text.Trim();


        SqlParameter p_ddlBeneficiarybank_754 = new SqlParameter("@ddlBeneficiaryBank", SqlDbType.VarChar);
        p_ddlBeneficiarybank_754.Value = ddlBeneficiarybank_754.SelectedValue;
        SqlParameter p_txtBeneficiaryBankAccountnumber_754 = new SqlParameter("@BeneficiaryBank_PartyIdent", SqlDbType.VarChar);
        p_txtBeneficiaryBankAccountnumber_754.Value = txtBeneficiaryBankAccountnumber_754.Text.Trim();
        SqlParameter p_txtBeneficiaryBankAccountnumber1_754 = new SqlParameter("@BeneficiaryBank_PartyIdent1", SqlDbType.VarChar);
        p_txtBeneficiaryBankAccountnumber1_754.Value = txtBeneficiaryBankAccountnumber1_754.Text.Trim();
        SqlParameter p_txtBeneficiaryBankIdentifiercode_754 = new SqlParameter("@BeneficiaryBank_Identcode", SqlDbType.VarChar);
        p_txtBeneficiaryBankIdentifiercode_754.Value = txtBeneficiaryBankIdentifiercode_754.Text.Trim();
        SqlParameter p_txtBeneficiaryBankName_754 = new SqlParameter("@BeneficiaryBank_Name", SqlDbType.VarChar);
        p_txtBeneficiaryBankName_754.Value = txtBeneficiaryBankName_754.Text.Trim();
        SqlParameter p_txtBeneficiaryBankAddress1_754 = new SqlParameter("@BeneficiaryBank_Add1", SqlDbType.VarChar);
        p_txtBeneficiaryBankAddress1_754.Value = txtBeneficiaryBankAddress1_754.Text.Trim();
        SqlParameter p_txtBeneficiaryBankAddress2_754 = new SqlParameter("@BeneficiaryBank_Add2", SqlDbType.VarChar);
        p_txtBeneficiaryBankAddress2_754.Value = txtBeneficiaryBankAddress2_754.Text.Trim();
        SqlParameter p_txtBeneficiaryBankAddress3_754 = new SqlParameter("@BeneficiaryBank_Add3", SqlDbType.VarChar);
        p_txtBeneficiaryBankAddress3_754.Value = txtBeneficiaryBankAddress3_754.Text.Trim();

        SqlParameter p_754_SenToRecinfo1 = new SqlParameter("@SenToRecinfo1", SqlDbType.VarChar);
        p_754_SenToRecinfo1.Value = txt_754_SenRecInfo1.Text.Trim();
        SqlParameter p_754_SenToRecinfo2 = new SqlParameter("@SenToRecinfo2", SqlDbType.VarChar);
        p_754_SenToRecinfo2.Value = txt_754_SenRecInfo2.Text.Trim();
        SqlParameter p_754_SenToRecinfo3 = new SqlParameter("@SenToRecinfo3", SqlDbType.VarChar);
        p_754_SenToRecinfo3.Value = txt_754_SenRecInfo3.Text.Trim();
        SqlParameter p_754_SenToRecinfo4 = new SqlParameter("@SenToRecinfo4", SqlDbType.VarChar);
        p_754_SenToRecinfo4.Value = txt_754_SenRecInfo4.Text.Trim();
        SqlParameter p_754_SenToRecinfo5 = new SqlParameter("@SenToRecinfo5", SqlDbType.VarChar);
        p_754_SenToRecinfo5.Value = txt_754_SenRecInfo5.Text.Trim();
        SqlParameter p_754_SenToRecinfo6 = new SqlParameter("@SenToRecinfo6", SqlDbType.VarChar);
        p_754_SenToRecinfo6.Value = txt_754_SenRecInfo6.Text.Trim();

        SqlParameter p_txt_754_Narr1 = new SqlParameter("@Narrative1", SqlDbType.VarChar);
        p_txt_754_Narr1.Value = txt_754_Narr1.Text.Trim();
        SqlParameter p_txt_754_Narr2 = new SqlParameter("@Narrative2", SqlDbType.VarChar);
        p_txt_754_Narr2.Value = txt_754_Narr2.Text.Trim();
        SqlParameter p_txt_754_Narr3 = new SqlParameter("@Narrative3", SqlDbType.VarChar);
        p_txt_754_Narr3.Value = txt_754_Narr3.Text.Trim();
        SqlParameter p_txt_754_Narr4 = new SqlParameter("@Narrative4", SqlDbType.VarChar);
        p_txt_754_Narr4.Value = txt_754_Narr4.Text.Trim();
        SqlParameter p_txt_754_Narr5 = new SqlParameter("@Narrative5", SqlDbType.VarChar);
        p_txt_754_Narr5.Value = txt_754_Narr5.Text.Trim();
        SqlParameter p_txt_754_Narr6 = new SqlParameter("@Narrative6", SqlDbType.VarChar);
        p_txt_754_Narr6.Value = txt_754_Narr6.Text.Trim();
        SqlParameter p_txt_754_Narr7 = new SqlParameter("@Narrative7", SqlDbType.VarChar);
        p_txt_754_Narr7.Value = txt_754_Narr7.Text.Trim();
        SqlParameter p_txt_754_Narr8 = new SqlParameter("@Narrative8", SqlDbType.VarChar);
        p_txt_754_Narr8.Value = txt_754_Narr8.Text.Trim();
        SqlParameter p_txt_754_Narr9 = new SqlParameter("@Narrative9", SqlDbType.VarChar);
        p_txt_754_Narr9.Value = txt_754_Narr9.Text.Trim();
        SqlParameter p_txt_754_Narr10 = new SqlParameter("@Narrative10", SqlDbType.VarChar);
        p_txt_754_Narr10.Value = txt_754_Narr10.Text.Trim();
        SqlParameter p_txt_754_Narr11 = new SqlParameter("@Narrative11", SqlDbType.VarChar);
        p_txt_754_Narr11.Value = txt_754_Narr11.Text.Trim();
        SqlParameter p_txt_754_Narr12 = new SqlParameter("@Narrative12", SqlDbType.VarChar);
        p_txt_754_Narr12.Value = txt_754_Narr12.Text.Trim();
        SqlParameter p_txt_754_Narr13 = new SqlParameter("@Narrative13", SqlDbType.VarChar);
        p_txt_754_Narr13.Value = txt_754_Narr13.Text.Trim();
        SqlParameter p_txt_754_Narr14 = new SqlParameter("@Narrative14", SqlDbType.VarChar);
        p_txt_754_Narr14.Value = txt_754_Narr14.Text.Trim();
        SqlParameter p_txt_754_Narr15 = new SqlParameter("@Narrative15", SqlDbType.VarChar);
        p_txt_754_Narr15.Value = txt_754_Narr15.Text.Trim();
        SqlParameter p_txt_754_Narr16 = new SqlParameter("@Narrative16", SqlDbType.VarChar);
        p_txt_754_Narr16.Value = txt_754_Narr16.Text.Trim();
        SqlParameter p_txt_754_Narr17 = new SqlParameter("@Narrative17", SqlDbType.VarChar);
        p_txt_754_Narr17.Value = txt_754_Narr17.Text.Trim();
        SqlParameter p_txt_754_Narr18 = new SqlParameter("@Narrative18", SqlDbType.VarChar);
        p_txt_754_Narr18.Value = txt_754_Narr18.Text.Trim();
        SqlParameter p_txt_754_Narr19 = new SqlParameter("@Narrative19", SqlDbType.VarChar);
        p_txt_754_Narr19.Value = txt_754_Narr19.Text.Trim();
        SqlParameter p_txt_754_Narr20 = new SqlParameter("@Narrative20", SqlDbType.VarChar);
        p_txt_754_Narr20.Value = txt_754_Narr20.Text.Trim();

        SqlParameter P_AddedBy = new SqlParameter("@AddedBy", hdnUserName);
        SqlParameter P_AddedDate = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        SqlParameter P_UpdatedBy = new SqlParameter("@UpdatedBy", hdnUserName);
        SqlParameter P_UpdatedDate = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData("TF_Exp_AddUpdateSwift754", p_Branch, p_SwiftType, p_txtReceiver754, p_754SenRefno, p_754RelRef, p_ddlPrinAmtPaidAccNegoAmt_754, p_txtPrinAmtPaidAccNegoDate_754,
        p_ddl_PrinAmtPaidAccNegoCurr_754, p_txtPrinAmtPaidAccNegoAmt_754, p_ddl_754_AddAmtClamd_Ccy, p_txt_754_AddAmtClamd_Amt, p_txt_754_ChargesDeduct1, p_txt_754_ChargesDeduct2,
        p_txt_754_ChargesDeduct3, p_txt_754_ChargesDeduct4, p_txt_754_ChargesDeduct5, p_txt_754_ChargesDeduct6, p_txt_754_ChargesAdded1, p_txt_754_ChargesAdded2, p_txt_754_ChargesAdded3,
        p_txt_754_ChargesAdded4, p_txt_754_ChargesAdded5, p_txt_754_ChargesAdded6, p_ddlTotalAmtclamd_754, p_txt_754_TotalAmtClmd_Date, p_ddl_754_TotalAmtClmd_Ccy, p_txt_754_TotalAmtClmd_Amt,
        p_ddlReimbursingbank_754, p_txtReimbursingBankAccountnumber_754, p_txtReimbursingBankAccountnumber1_754, p_txtReimbursingBankIdentifiercode_754, p_txtReimbursingBankLocation_754,
        p_txtReimbursingBankName_754, p_txtReimbursingBankAddress1_754, p_txtReimbursingBankAddress2_754, p_txtReimbursingBankAddress3_754, p_ddlAccountwithbank_754,
        p_txtAccountwithBankAccountnumber_754, p_txtAccountwithBankAccountnumber1_754, p_txtAccountwithBankIdentifiercode_754, p_txtAccountwithBankLocation_754, p_txtAccountwithBankName_754,
        p_txtAccountwithBankAddress1_754, p_txtAccountwithBankAddress2_754, p_txtAccountwithBankAddress3_754, p_ddlBeneficiarybank_754, p_txtBeneficiaryBankAccountnumber_754,
        p_txtBeneficiaryBankAccountnumber1_754, p_txtBeneficiaryBankIdentifiercode_754, p_txtBeneficiaryBankName_754, p_txtBeneficiaryBankAddress1_754, p_txtBeneficiaryBankAddress2_754,
        p_txtBeneficiaryBankAddress3_754, p_754_SenToRecinfo1, p_754_SenToRecinfo2, p_754_SenToRecinfo3, p_754_SenToRecinfo4, p_754_SenToRecinfo5, p_754_SenToRecinfo6, p_txt_754_Narr1,
        p_txt_754_Narr2, p_txt_754_Narr3, p_txt_754_Narr4, p_txt_754_Narr5, p_txt_754_Narr6, p_txt_754_Narr7, p_txt_754_Narr8, p_txt_754_Narr9, p_txt_754_Narr10, p_txt_754_Narr11,
        p_txt_754_Narr12, p_txt_754_Narr13, p_txt_754_Narr14, p_txt_754_Narr15, p_txt_754_Narr16, p_txt_754_Narr17, p_txt_754_Narr18, p_txt_754_Narr19, p_txt_754_Narr20,
        P_AddedBy, P_AddedDate, P_UpdatedBy, P_UpdatedDate);

        if (_result == "added")
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added as Draft.');", true);
        }
        else if (_result == "Updated")
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated successfully.');", true);
        }
        else
        {
            // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Error');", true);
        }
    }
    public void Sendtochecker799()
    {
        string hdnUserName = Session["userName"].ToString();
        SqlParameter p_Branch = new SqlParameter("@Branch", SqlDbType.VarChar);
        p_Branch.Value = ddlBranch.SelectedItem.Text;
        SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
        p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;

        SqlParameter p_txtReceiver799 = new SqlParameter("@Receiver", SqlDbType.VarChar);
        p_txtReceiver799.Value = txtReceiver799.Text.Trim();

        SqlParameter p_799TranRefno = new SqlParameter("@Trans_Ref_No", SqlDbType.VarChar);
        p_799TranRefno.Value = txt_799_transRefNo.Text.Trim();

        SqlParameter p_799RelatedRef = new SqlParameter("@Related_Ref", SqlDbType.VarChar);
        p_799RelatedRef.Value = txt_799_RelRef.Text.Trim();
        SqlParameter p_799Narr1 = new SqlParameter("@Narrative1", SqlDbType.VarChar);
        p_799Narr1.Value = txt_799_Narr1.Text.Trim();
        SqlParameter p_799Narr2 = new SqlParameter("@Narrative2", SqlDbType.VarChar);
        p_799Narr2.Value = txt_799_Narr2.Text.Trim();
        SqlParameter p_799Narr3 = new SqlParameter("@Narrative3", SqlDbType.VarChar);
        p_799Narr3.Value = txt_799_Narr3.Text.Trim();
        SqlParameter p_799Narr4 = new SqlParameter("@Narrative4", SqlDbType.VarChar);
        p_799Narr4.Value = txt_799_Narr4.Text.Trim();
        SqlParameter p_799Narr5 = new SqlParameter("@Narrative5", SqlDbType.VarChar);
        p_799Narr5.Value = txt_799_Narr5.Text.Trim();
        SqlParameter p_799Narr6 = new SqlParameter("@Narrative6", SqlDbType.VarChar);
        p_799Narr6.Value = txt_799_Narr6.Text.Trim();
        SqlParameter p_799Narr7 = new SqlParameter("@Narrative7", SqlDbType.VarChar);
        p_799Narr7.Value = txt_799_Narr7.Text.Trim();
        SqlParameter p_799Narr8 = new SqlParameter("@Narrative8", SqlDbType.VarChar);
        p_799Narr8.Value = txt_799_Narr8.Text.Trim();
        SqlParameter p_799Narr9 = new SqlParameter("@Narrative9", SqlDbType.VarChar);
        p_799Narr9.Value = txt_799_Narr9.Text.Trim();
        SqlParameter p_799Narr10 = new SqlParameter("@Narrative10", SqlDbType.VarChar);
        p_799Narr10.Value = txt_799_Narr10.Text.Trim();
        SqlParameter p_799Narr11 = new SqlParameter("@Narrative11", SqlDbType.VarChar);
        p_799Narr11.Value = txt_799_Narr11.Text.Trim();
        SqlParameter p_799Narr12 = new SqlParameter("@Narrative12", SqlDbType.VarChar);
        p_799Narr12.Value = txt_799_Narr12.Text.Trim();
        SqlParameter p_799Narr13 = new SqlParameter("@Narrative13", SqlDbType.VarChar);
        p_799Narr13.Value = txt_799_Narr13.Text.Trim();
        SqlParameter p_799Narr14 = new SqlParameter("@Narrative14", SqlDbType.VarChar);
        p_799Narr14.Value = txt_799_Narr14.Text.Trim();
        SqlParameter p_799Narr15 = new SqlParameter("@Narrative15", SqlDbType.VarChar);
        p_799Narr15.Value = txt_799_Narr15.Text.Trim();
        SqlParameter p_799Narr16 = new SqlParameter("@Narrative16", SqlDbType.VarChar);
        p_799Narr16.Value = txt_799_Narr16.Text.Trim();
        SqlParameter p_799Narr17 = new SqlParameter("@Narrative17", SqlDbType.VarChar);
        p_799Narr17.Value = txt_799_Narr17.Text.Trim();
        SqlParameter p_799Narr18 = new SqlParameter("@Narrative18", SqlDbType.VarChar);
        p_799Narr18.Value = txt_799_Narr18.Text.Trim();
        SqlParameter p_799Narr19 = new SqlParameter("@Narrative19", SqlDbType.VarChar);
        p_799Narr19.Value = txt_799_Narr19.Text.Trim();
        SqlParameter p_799Narr20 = new SqlParameter("@Narrative20", SqlDbType.VarChar);
        p_799Narr20.Value = txt_799_Narr20.Text.Trim();
        SqlParameter p_799Narr21 = new SqlParameter("@Narrative21", SqlDbType.VarChar);
        p_799Narr21.Value = txt_799_Narr21.Text.Trim();
        SqlParameter p_799Narr22 = new SqlParameter("@Narrative22", SqlDbType.VarChar);
        p_799Narr22.Value = txt_799_Narr22.Text.Trim();
        SqlParameter p_799Narr23 = new SqlParameter("@Narrative23", SqlDbType.VarChar);
        p_799Narr23.Value = txt_799_Narr23.Text.Trim();
        SqlParameter p_799Narr24 = new SqlParameter("@Narrative24", SqlDbType.VarChar);
        p_799Narr24.Value = txt_799_Narr24.Text.Trim();
        SqlParameter p_799Narr25 = new SqlParameter("@Narrative25", SqlDbType.VarChar);
        p_799Narr25.Value = txt_799_Narr25.Text.Trim();
        SqlParameter p_799Narr26 = new SqlParameter("@Narrative26", SqlDbType.VarChar);
        p_799Narr26.Value = txt_799_Narr26.Text.Trim();
        SqlParameter p_799Narr27 = new SqlParameter("@Narrative27", SqlDbType.VarChar);
        p_799Narr27.Value = txt_799_Narr27.Text.Trim();
        SqlParameter p_799Narr28 = new SqlParameter("@Narrative28", SqlDbType.VarChar);
        p_799Narr28.Value = txt_799_Narr28.Text.Trim();
        SqlParameter p_799Narr29 = new SqlParameter("@Narrative29", SqlDbType.VarChar);
        p_799Narr29.Value = txt_799_Narr29.Text.Trim();
        SqlParameter p_799Narr30 = new SqlParameter("@Narrative30", SqlDbType.VarChar);
        p_799Narr30.Value = txt_799_Narr30.Text.Trim();
        SqlParameter p_799Narr31 = new SqlParameter("@Narrative31", SqlDbType.VarChar);
        p_799Narr31.Value = txt_799_Narr31.Text.Trim();
        SqlParameter p_799Narr32 = new SqlParameter("@Narrative32", SqlDbType.VarChar);
        p_799Narr32.Value = txt_799_Narr32.Text.Trim();
        SqlParameter p_799Narr33 = new SqlParameter("@Narrative33", SqlDbType.VarChar);
        p_799Narr33.Value = txt_799_Narr33.Text.Trim();
        SqlParameter p_799Narr34 = new SqlParameter("@Narrative34", SqlDbType.VarChar);
        p_799Narr34.Value = txt_799_Narr34.Text.Trim();
        SqlParameter p_799Narr35 = new SqlParameter("@Narrative35", SqlDbType.VarChar);
        p_799Narr35.Value = txt_799_Narr35.Text.Trim();

        SqlParameter P_AddedBy = new SqlParameter("@AddedBy", hdnUserName);
        SqlParameter P_AddedDate = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        SqlParameter P_UpdatedBy = new SqlParameter("@UpdatedBy", hdnUserName);
        SqlParameter P_UpdatedDate = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData("TF_Exp_AddUpdateSwift799", p_Branch, p_SwiftType, p_txtReceiver799, p_799TranRefno, p_799RelatedRef, p_799Narr1, p_799Narr2, p_799Narr3, p_799Narr4, p_799Narr5,
            p_799Narr6, p_799Narr7, p_799Narr8, p_799Narr9, p_799Narr10, p_799Narr11, p_799Narr12, p_799Narr13, p_799Narr14, p_799Narr15, p_799Narr16, p_799Narr17, p_799Narr18,
            p_799Narr19, p_799Narr20, p_799Narr21, p_799Narr22, p_799Narr23, p_799Narr24, p_799Narr25, p_799Narr26, p_799Narr27, p_799Narr28, p_799Narr29, p_799Narr30,
           p_799Narr31, p_799Narr32, p_799Narr33, p_799Narr34, p_799Narr35, P_AddedBy, P_AddedDate, P_UpdatedBy, P_UpdatedDate);

        if (_result == "added")
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added as Draft.');", true);
        }
        else if (_result == "Updated")
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated successfully.');", true);
        }
        else
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Error');", true);
        }

    }
    public void Sendtochecker499()
    {
        string hdnUserName = Session["userName"].ToString();
        SqlParameter p_Branch = new SqlParameter("@Branch", SqlDbType.VarChar);
        p_Branch.Value = ddlBranch.SelectedItem.Text;
        SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
        p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;

        SqlParameter p_txtReceiver499 = new SqlParameter("@Receiver", SqlDbType.VarChar);
        p_txtReceiver499.Value = txtReceiver499.Text.Trim();

        SqlParameter p_499TranRefno = new SqlParameter("@Trans_Ref_No", SqlDbType.VarChar);
        p_499TranRefno.Value = txt_499_transRefNo.Text.Trim();

        SqlParameter p_499RelatedRef = new SqlParameter("@Related_Ref", SqlDbType.VarChar);
        p_499RelatedRef.Value = txt_499_RelRef.Text.Trim();
        SqlParameter p_499Narr1 = new SqlParameter("@Narrative1", SqlDbType.VarChar);
        p_499Narr1.Value = txt_499_Narr1.Text.Trim();
        SqlParameter p_499Narr2 = new SqlParameter("@Narrative2", SqlDbType.VarChar);
        p_499Narr2.Value = txt_499_Narr2.Text.Trim();
        SqlParameter p_499Narr3 = new SqlParameter("@Narrative3", SqlDbType.VarChar);
        p_499Narr3.Value = txt_499_Narr3.Text.Trim();
        SqlParameter p_499Narr4 = new SqlParameter("@Narrative4", SqlDbType.VarChar);
        p_499Narr4.Value = txt_499_Narr4.Text.Trim();
        SqlParameter p_499Narr5 = new SqlParameter("@Narrative5", SqlDbType.VarChar);
        p_499Narr5.Value = txt_499_Narr5.Text.Trim();
        SqlParameter p_499Narr6 = new SqlParameter("@Narrative6", SqlDbType.VarChar);
        p_499Narr6.Value = txt_499_Narr6.Text.Trim();
        SqlParameter p_499Narr7 = new SqlParameter("@Narrative7", SqlDbType.VarChar);
        p_499Narr7.Value = txt_499_Narr7.Text.Trim();
        SqlParameter p_499Narr8 = new SqlParameter("@Narrative8", SqlDbType.VarChar);
        p_499Narr8.Value = txt_499_Narr8.Text.Trim();
        SqlParameter p_499Narr9 = new SqlParameter("@Narrative9", SqlDbType.VarChar);
        p_499Narr9.Value = txt_499_Narr9.Text.Trim();
        SqlParameter p_499Narr10 = new SqlParameter("@Narrative10", SqlDbType.VarChar);
        p_499Narr10.Value = txt_499_Narr10.Text.Trim();
        SqlParameter p_499Narr11 = new SqlParameter("@Narrative11", SqlDbType.VarChar);
        p_499Narr11.Value = txt_499_Narr11.Text.Trim();
        SqlParameter p_499Narr12 = new SqlParameter("@Narrative12", SqlDbType.VarChar);
        p_499Narr12.Value = txt_499_Narr12.Text.Trim();
        SqlParameter p_499Narr13 = new SqlParameter("@Narrative13", SqlDbType.VarChar);
        p_499Narr13.Value = txt_499_Narr13.Text.Trim();
        SqlParameter p_499Narr14 = new SqlParameter("@Narrative14", SqlDbType.VarChar);
        p_499Narr14.Value = txt_499_Narr14.Text.Trim();
        SqlParameter p_499Narr15 = new SqlParameter("@Narrative15", SqlDbType.VarChar);
        p_499Narr15.Value = txt_499_Narr15.Text.Trim();
        SqlParameter p_499Narr16 = new SqlParameter("@Narrative16", SqlDbType.VarChar);
        p_499Narr16.Value = txt_499_Narr16.Text.Trim();
        SqlParameter p_499Narr17 = new SqlParameter("@Narrative17", SqlDbType.VarChar);
        p_499Narr17.Value = txt_499_Narr17.Text.Trim();
        SqlParameter p_499Narr18 = new SqlParameter("@Narrative18", SqlDbType.VarChar);
        p_499Narr18.Value = txt_499_Narr18.Text.Trim();
        SqlParameter p_499Narr19 = new SqlParameter("@Narrative19", SqlDbType.VarChar);
        p_499Narr19.Value = txt_499_Narr19.Text.Trim();
        SqlParameter p_499Narr20 = new SqlParameter("@Narrative20", SqlDbType.VarChar);
        p_499Narr20.Value = txt_499_Narr20.Text.Trim();
        SqlParameter p_499Narr21 = new SqlParameter("@Narrative21", SqlDbType.VarChar);
        p_499Narr21.Value = txt_499_Narr21.Text.Trim();
        SqlParameter p_499Narr22 = new SqlParameter("@Narrative22", SqlDbType.VarChar);
        p_499Narr22.Value = txt_499_Narr22.Text.Trim();
        SqlParameter p_499Narr23 = new SqlParameter("@Narrative23", SqlDbType.VarChar);
        p_499Narr23.Value = txt_499_Narr23.Text.Trim();
        SqlParameter p_499Narr24 = new SqlParameter("@Narrative24", SqlDbType.VarChar);
        p_499Narr24.Value = txt_499_Narr24.Text.Trim();
        SqlParameter p_499Narr25 = new SqlParameter("@Narrative25", SqlDbType.VarChar);
        p_499Narr25.Value = txt_499_Narr25.Text.Trim();
        SqlParameter p_499Narr26 = new SqlParameter("@Narrative26", SqlDbType.VarChar);
        p_499Narr26.Value = txt_499_Narr26.Text.Trim();
        SqlParameter p_499Narr27 = new SqlParameter("@Narrative27", SqlDbType.VarChar);
        p_499Narr27.Value = txt_499_Narr27.Text.Trim();
        SqlParameter p_499Narr28 = new SqlParameter("@Narrative28", SqlDbType.VarChar);
        p_499Narr28.Value = txt_499_Narr28.Text.Trim();
        SqlParameter p_499Narr29 = new SqlParameter("@Narrative29", SqlDbType.VarChar);
        p_499Narr29.Value = txt_499_Narr29.Text.Trim();
        SqlParameter p_499Narr30 = new SqlParameter("@Narrative30", SqlDbType.VarChar);
        p_499Narr30.Value = txt_499_Narr30.Text.Trim();
        SqlParameter p_499Narr31 = new SqlParameter("@Narrative31", SqlDbType.VarChar);
        p_499Narr31.Value = txt_499_Narr31.Text.Trim();
        SqlParameter p_499Narr32 = new SqlParameter("@Narrative32", SqlDbType.VarChar);
        p_499Narr32.Value = txt_499_Narr32.Text.Trim();
        SqlParameter p_499Narr33 = new SqlParameter("@Narrative33", SqlDbType.VarChar);
        p_499Narr33.Value = txt_499_Narr33.Text.Trim();
        SqlParameter p_499Narr34 = new SqlParameter("@Narrative34", SqlDbType.VarChar);
        p_499Narr34.Value = txt_499_Narr34.Text.Trim();
        SqlParameter p_499Narr35 = new SqlParameter("@Narrative35", SqlDbType.VarChar);
        p_499Narr35.Value = txt_499_Narr35.Text.Trim();

        SqlParameter P_AddedBy = new SqlParameter("@AddedBy", hdnUserName);
        SqlParameter P_AddedDate = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        SqlParameter P_UpdatedBy = new SqlParameter("@UpdatedBy", hdnUserName);
        SqlParameter P_UpdatedDate = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData("TF_Exp_AddUpdateSwift499", p_Branch, p_SwiftType, p_txtReceiver499, p_499TranRefno, p_499RelatedRef, p_499Narr1, p_499Narr2, p_499Narr3, p_499Narr4, p_499Narr5,
            p_499Narr6, p_499Narr7, p_499Narr8, p_499Narr9, p_499Narr10, p_499Narr11, p_499Narr12, p_499Narr13, p_499Narr14, p_499Narr15, p_499Narr16, p_499Narr17, p_499Narr18,
            p_499Narr19, p_499Narr20, p_499Narr21, p_499Narr22, p_499Narr23, p_499Narr24, p_499Narr25, p_499Narr26, p_499Narr27, p_499Narr28, p_499Narr29, p_499Narr30,
           p_499Narr31, p_499Narr32, p_499Narr33, p_499Narr34, p_499Narr35, P_AddedBy, P_AddedDate, P_UpdatedBy, P_UpdatedDate);

        if (_result == "added")
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added as Draft.');", true);
        }
        else if (_result == "Updated")
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated successfully.');", true);
        }
        else
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Error');", true);
        }

    }
    public void Sendtochecker420()
    {
        string hdnUserName = Session["userName"].ToString();
        SqlParameter p_Branch = new SqlParameter("@Branch", SqlDbType.VarChar);
        p_Branch.Value = ddlBranch.SelectedItem.Text;
        SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
        p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;
        SqlParameter p_txtReceiver420 = new SqlParameter("@Receiver", SqlDbType.VarChar);
        p_txtReceiver420.Value = txtReceiver420.Text.Trim();
        SqlParameter p_420TranRefno = new SqlParameter("@Trans_Ref_No", SqlDbType.VarChar);
        p_420TranRefno.Value = txt_420_SendingBankTRN.Text.Trim();
        SqlParameter p_420RelatedRef = new SqlParameter("@Related_Ref", SqlDbType.VarChar);
        p_420RelatedRef.Value = txt_420_RelRef.Text.Trim();
        SqlParameter p_420_DdlAmtTraced = new SqlParameter("@ddlAmtTraced", SqlDbType.VarChar);
        p_420_DdlAmtTraced.Value = ddlAmountTraced_420.SelectedValue;
        SqlParameter p_420_TracedAmt = new SqlParameter("@TracedAmt", SqlDbType.VarChar);
        p_420_TracedAmt.Value = txtAmountTracedAmount_420.Text.Trim();
        SqlParameter p_420_Tracedcode = new SqlParameter("@Tracedcode", SqlDbType.VarChar);
        p_420_Tracedcode.Value = ddlAmountTracedCode_420.SelectedValue;
        SqlParameter p_420_Tracedcurr = new SqlParameter("@Tracedcurr", SqlDbType.VarChar);
        p_420_Tracedcurr.Value = ddl_AmountTracedCurrency_420.SelectedItem.Text.Trim();
        SqlParameter p_420_TracedDate = new SqlParameter("@TracedDate", SqlDbType.VarChar);
        p_420_TracedDate.Value = txtAmountTracedDate_420.Text.Trim();
        SqlParameter p_420_TracedDayMonth = new SqlParameter("@TracedDayMonth", SqlDbType.VarChar);
        p_420_TracedDayMonth.Value = ddlAmountTracedDayMonth_420.SelectedValue;
        SqlParameter p_420_TracedNoofDayMonth = new SqlParameter("@TracedNoofDayMonth", SqlDbType.VarChar);
        p_420_TracedNoofDayMonth.Value = txtAmountTracedNoofDaysMonth_420.Text.Trim();
        SqlParameter p_420_DateofCollnInstruction = new SqlParameter("@DateofCollnInstruction", SqlDbType.VarChar);
        p_420_DateofCollnInstruction.Value = txt_420_DateofCollnInstruction.Text.Trim();
        SqlParameter p_420_DraweeAccount = new SqlParameter("@DraweeAccount", SqlDbType.VarChar);
        p_420_DraweeAccount.Value = txt_420_DraweeAccount.Text.Trim();
        SqlParameter p_420_DraweeName = new SqlParameter("@DraweeName", SqlDbType.VarChar);
        p_420_DraweeName.Value = txt_420_DraweeName.Text.Trim();
        SqlParameter p_420_DraweeAdd1 = new SqlParameter("@DraweeAdd1", SqlDbType.VarChar);
        p_420_DraweeAdd1.Value = txt_420_DraweeAdd1.Text.Trim();
        SqlParameter p_420_DraweeAdd2 = new SqlParameter("@DraweeAdd2", SqlDbType.VarChar);
        p_420_DraweeAdd2.Value = txt_420_DraweeAdd2.Text.Trim();
        SqlParameter p_420_DraweeAdd3 = new SqlParameter("@DraweeAdd3", SqlDbType.VarChar);
        p_420_DraweeAdd3.Value = txt_420_DraweeAdd3.Text.Trim();
        SqlParameter p_420_SenToRecinfo1 = new SqlParameter("@SenToRecinfo1", SqlDbType.VarChar);
        p_420_SenToRecinfo1.Value = txt_420_SenToRecinfo1.Text.Trim();
        SqlParameter p_420_SenToRecinfo2 = new SqlParameter("@SenToRecinfo2", SqlDbType.VarChar);
        p_420_SenToRecinfo2.Value = txt_420_SenToRecinfo2.Text.Trim();
        SqlParameter p_420_SenToRecinfo3 = new SqlParameter("@SenToRecinfo3", SqlDbType.VarChar);
        p_420_SenToRecinfo3.Value = txt_420_SenToRecinfo3.Text.Trim();
        SqlParameter p_420_SenToRecinfo4 = new SqlParameter("@SenToRecinfo4", SqlDbType.VarChar);
        p_420_SenToRecinfo4.Value = txt_420_SenToRecinfo4.Text.Trim();
        SqlParameter p_420_SenToRecinfo5 = new SqlParameter("@SenToRecinfo5", SqlDbType.VarChar);
        p_420_SenToRecinfo5.Value = txt_420_SenToRecinfo5.Text.Trim();
        SqlParameter p_420_SenToRecinfo6 = new SqlParameter("@SenToRecinfo6", SqlDbType.VarChar);
        p_420_SenToRecinfo6.Value = txt_420_SenToRecinfo6.Text.Trim();
        SqlParameter P_AddedBy = new SqlParameter("@AddedBy", hdnUserName);
        SqlParameter P_AddedDate = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        SqlParameter P_UpdatedBy = new SqlParameter("@UpdatedBy", hdnUserName);
        SqlParameter P_UpdatedDate = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData("TF_Exp_AddUpdateSwift420", p_Branch, p_SwiftType, p_txtReceiver420, p_420TranRefno, p_420RelatedRef, p_420_DdlAmtTraced, p_420_TracedAmt, p_420_Tracedcode,
          p_420_Tracedcurr, p_420_TracedDate, p_420_TracedDayMonth, p_420_TracedNoofDayMonth, p_420_DateofCollnInstruction, p_420_DraweeAccount, p_420_DraweeName, p_420_DraweeAdd1,
          p_420_DraweeAdd2, p_420_DraweeAdd3, p_420_SenToRecinfo1, p_420_SenToRecinfo2, p_420_SenToRecinfo3, p_420_SenToRecinfo4, p_420_SenToRecinfo5, p_420_SenToRecinfo6,
          P_AddedBy, P_AddedDate, P_UpdatedBy, P_UpdatedDate);

        if (_result == "added")
        {
            // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added as Draft.');", true);
        }
        else if (_result == "Updated")
        {
            //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated successfully.');", true);
        }
        else
        {
            // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Error while saving the Record');", true);
        }
    }
    public void Sendtochecker199()
    {
        string hdnUserName = Session["userName"].ToString();
        SqlParameter p_Branch = new SqlParameter("@Branch", SqlDbType.VarChar);
        p_Branch.Value = ddlBranch.SelectedItem.Text;
        SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
        p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;

        SqlParameter p_txtReceiver199 = new SqlParameter("@Receiver", SqlDbType.VarChar);
        p_txtReceiver199.Value = txtReceiver199.Text.Trim();

        SqlParameter p_199TranRefno = new SqlParameter("@Trans_Ref_No", SqlDbType.VarChar);
        p_199TranRefno.Value = txt_199_transRefNo.Text.Trim();

        SqlParameter p_199RelatedRef = new SqlParameter("@Related_Ref", SqlDbType.VarChar);
        p_199RelatedRef.Value = txt_199_RelRef.Text.Trim();
        SqlParameter p_199Narr1 = new SqlParameter("@Narrative1", SqlDbType.VarChar);
        p_199Narr1.Value = txt_199_Narr1.Text.Trim();
        SqlParameter p_199Narr2 = new SqlParameter("@Narrative2", SqlDbType.VarChar);
        p_199Narr2.Value = txt_199_Narr2.Text.Trim();
        SqlParameter p_199Narr3 = new SqlParameter("@Narrative3", SqlDbType.VarChar);
        p_199Narr3.Value = txt_199_Narr3.Text.Trim();
        SqlParameter p_199Narr4 = new SqlParameter("@Narrative4", SqlDbType.VarChar);
        p_199Narr4.Value = txt_199_Narr4.Text.Trim();
        SqlParameter p_199Narr5 = new SqlParameter("@Narrative5", SqlDbType.VarChar);
        p_199Narr5.Value = txt_199_Narr5.Text.Trim();
        SqlParameter p_199Narr6 = new SqlParameter("@Narrative6", SqlDbType.VarChar);
        p_199Narr6.Value = txt_199_Narr6.Text.Trim();
        SqlParameter p_199Narr7 = new SqlParameter("@Narrative7", SqlDbType.VarChar);
        p_199Narr7.Value = txt_199_Narr7.Text.Trim();
        SqlParameter p_199Narr8 = new SqlParameter("@Narrative8", SqlDbType.VarChar);
        p_199Narr8.Value = txt_199_Narr8.Text.Trim();
        SqlParameter p_199Narr9 = new SqlParameter("@Narrative9", SqlDbType.VarChar);
        p_199Narr9.Value = txt_199_Narr9.Text.Trim();
        SqlParameter p_199Narr10 = new SqlParameter("@Narrative10", SqlDbType.VarChar);
        p_199Narr10.Value = txt_199_Narr10.Text.Trim();
        SqlParameter p_199Narr11 = new SqlParameter("@Narrative11", SqlDbType.VarChar);
        p_199Narr11.Value = txt_199_Narr11.Text.Trim();
        SqlParameter p_199Narr12 = new SqlParameter("@Narrative12", SqlDbType.VarChar);
        p_199Narr12.Value = txt_199_Narr12.Text.Trim();
        SqlParameter p_199Narr13 = new SqlParameter("@Narrative13", SqlDbType.VarChar);
        p_199Narr13.Value = txt_199_Narr13.Text.Trim();
        SqlParameter p_199Narr14 = new SqlParameter("@Narrative14", SqlDbType.VarChar);
        p_199Narr14.Value = txt_199_Narr14.Text.Trim();
        SqlParameter p_199Narr15 = new SqlParameter("@Narrative15", SqlDbType.VarChar);
        p_199Narr15.Value = txt_199_Narr15.Text.Trim();
        SqlParameter p_199Narr16 = new SqlParameter("@Narrative16", SqlDbType.VarChar);
        p_199Narr16.Value = txt_199_Narr16.Text.Trim();
        SqlParameter p_199Narr17 = new SqlParameter("@Narrative17", SqlDbType.VarChar);
        p_199Narr17.Value = txt_199_Narr17.Text.Trim();
        SqlParameter p_199Narr18 = new SqlParameter("@Narrative18", SqlDbType.VarChar);
        p_199Narr18.Value = txt_199_Narr18.Text.Trim();
        SqlParameter p_199Narr19 = new SqlParameter("@Narrative19", SqlDbType.VarChar);
        p_199Narr19.Value = txt_199_Narr19.Text.Trim();
        SqlParameter p_199Narr20 = new SqlParameter("@Narrative20", SqlDbType.VarChar);
        p_199Narr20.Value = txt_199_Narr20.Text.Trim();
        SqlParameter p_199Narr21 = new SqlParameter("@Narrative21", SqlDbType.VarChar);
        p_199Narr21.Value = txt_199_Narr21.Text.Trim();
        SqlParameter p_199Narr22 = new SqlParameter("@Narrative22", SqlDbType.VarChar);
        p_199Narr22.Value = txt_199_Narr22.Text.Trim();
        SqlParameter p_199Narr23 = new SqlParameter("@Narrative23", SqlDbType.VarChar);
        p_199Narr23.Value = txt_199_Narr23.Text.Trim();
        SqlParameter p_199Narr24 = new SqlParameter("@Narrative24", SqlDbType.VarChar);
        p_199Narr24.Value = txt_199_Narr24.Text.Trim();
        SqlParameter p_199Narr25 = new SqlParameter("@Narrative25", SqlDbType.VarChar);
        p_199Narr25.Value = txt_199_Narr25.Text.Trim();
        SqlParameter p_199Narr26 = new SqlParameter("@Narrative26", SqlDbType.VarChar);
        p_199Narr26.Value = txt_199_Narr26.Text.Trim();
        SqlParameter p_199Narr27 = new SqlParameter("@Narrative27", SqlDbType.VarChar);
        p_199Narr27.Value = txt_199_Narr27.Text.Trim();
        SqlParameter p_199Narr28 = new SqlParameter("@Narrative28", SqlDbType.VarChar);
        p_199Narr28.Value = txt_199_Narr28.Text.Trim();
        SqlParameter p_199Narr29 = new SqlParameter("@Narrative29", SqlDbType.VarChar);
        p_199Narr29.Value = txt_199_Narr29.Text.Trim();
        SqlParameter p_199Narr30 = new SqlParameter("@Narrative30", SqlDbType.VarChar);
        p_199Narr30.Value = txt_199_Narr30.Text.Trim();
        SqlParameter p_199Narr31 = new SqlParameter("@Narrative31", SqlDbType.VarChar);
        p_199Narr31.Value = txt_199_Narr31.Text.Trim();
        SqlParameter p_199Narr32 = new SqlParameter("@Narrative32", SqlDbType.VarChar);
        p_199Narr32.Value = txt_199_Narr32.Text.Trim();
        SqlParameter p_199Narr33 = new SqlParameter("@Narrative33", SqlDbType.VarChar);
        p_199Narr33.Value = txt_199_Narr33.Text.Trim();
        SqlParameter p_199Narr34 = new SqlParameter("@Narrative34", SqlDbType.VarChar);
        p_199Narr34.Value = txt_199_Narr34.Text.Trim();
        SqlParameter p_199Narr35 = new SqlParameter("@Narrative35", SqlDbType.VarChar);
        p_199Narr35.Value = txt_199_Narr35.Text.Trim();

        SqlParameter P_AddedBy = new SqlParameter("@AddedBy", hdnUserName);
        SqlParameter P_AddedDate = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        SqlParameter P_UpdatedBy = new SqlParameter("@UpdatedBy", hdnUserName);
        SqlParameter P_UpdatedDate = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData("TF_Exp_AddUpdateSwift199", p_Branch, p_SwiftType, p_txtReceiver199, p_199TranRefno, p_199RelatedRef, p_199Narr1, p_199Narr2, p_199Narr3, p_199Narr4, p_199Narr5,
            p_199Narr6, p_199Narr7, p_199Narr8, p_199Narr9, p_199Narr10, p_199Narr11, p_199Narr12, p_199Narr13, p_199Narr14, p_199Narr15, p_199Narr16, p_199Narr17, p_199Narr18,
            p_199Narr19, p_199Narr20, p_199Narr21, p_199Narr22, p_199Narr23, p_199Narr24, p_199Narr25, p_199Narr26, p_199Narr27, p_199Narr28, p_199Narr29, p_199Narr30,
           p_199Narr31, p_199Narr32, p_199Narr33, p_199Narr34, p_199Narr35, P_AddedBy, P_AddedDate, P_UpdatedBy, P_UpdatedDate);

        if (_result == "added")
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added as Draft.');", true);
        }
        else if (_result == "Updated")
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated successfully.');", true);
        }
        else
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Error');", true);
        }

    }
    public void Sendtochecker299()
    {
        string hdnUserName = Session["userName"].ToString();
        SqlParameter p_Branch = new SqlParameter("@Branch", SqlDbType.VarChar);
        p_Branch.Value = ddlBranch.SelectedItem.Text;
        SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
        p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;

        SqlParameter p_txtReceiver299 = new SqlParameter("@Receiver", SqlDbType.VarChar);
        p_txtReceiver299.Value = txtReceiver299.Text.Trim();

        SqlParameter p_299TranRefno = new SqlParameter("@Trans_Ref_No", SqlDbType.VarChar);
        p_299TranRefno.Value = txt_299_transRefNo.Text.Trim();

        SqlParameter p_299RelatedRef = new SqlParameter("@Related_Ref", SqlDbType.VarChar);
        p_299RelatedRef.Value = txt_299_RelRef.Text.Trim();
        SqlParameter p_299Narr1 = new SqlParameter("@Narrative1", SqlDbType.VarChar);
        p_299Narr1.Value = txt_299_Narr1.Text.Trim();
        SqlParameter p_299Narr2 = new SqlParameter("@Narrative2", SqlDbType.VarChar);
        p_299Narr2.Value = txt_299_Narr2.Text.Trim();
        SqlParameter p_299Narr3 = new SqlParameter("@Narrative3", SqlDbType.VarChar);
        p_299Narr3.Value = txt_299_Narr3.Text.Trim();
        SqlParameter p_299Narr4 = new SqlParameter("@Narrative4", SqlDbType.VarChar);
        p_299Narr4.Value = txt_299_Narr4.Text.Trim();
        SqlParameter p_299Narr5 = new SqlParameter("@Narrative5", SqlDbType.VarChar);
        p_299Narr5.Value = txt_299_Narr5.Text.Trim();
        SqlParameter p_299Narr6 = new SqlParameter("@Narrative6", SqlDbType.VarChar);
        p_299Narr6.Value = txt_299_Narr6.Text.Trim();
        SqlParameter p_299Narr7 = new SqlParameter("@Narrative7", SqlDbType.VarChar);
        p_299Narr7.Value = txt_299_Narr7.Text.Trim();
        SqlParameter p_299Narr8 = new SqlParameter("@Narrative8", SqlDbType.VarChar);
        p_299Narr8.Value = txt_299_Narr8.Text.Trim();
        SqlParameter p_299Narr9 = new SqlParameter("@Narrative9", SqlDbType.VarChar);
        p_299Narr9.Value = txt_299_Narr9.Text.Trim();
        SqlParameter p_299Narr10 = new SqlParameter("@Narrative10", SqlDbType.VarChar);
        p_299Narr10.Value = txt_299_Narr10.Text.Trim();
        SqlParameter p_299Narr11 = new SqlParameter("@Narrative11", SqlDbType.VarChar);
        p_299Narr11.Value = txt_299_Narr11.Text.Trim();
        SqlParameter p_299Narr12 = new SqlParameter("@Narrative12", SqlDbType.VarChar);
        p_299Narr12.Value = txt_299_Narr12.Text.Trim();
        SqlParameter p_299Narr13 = new SqlParameter("@Narrative13", SqlDbType.VarChar);
        p_299Narr13.Value = txt_299_Narr13.Text.Trim();
        SqlParameter p_299Narr14 = new SqlParameter("@Narrative14", SqlDbType.VarChar);
        p_299Narr14.Value = txt_299_Narr14.Text.Trim();
        SqlParameter p_299Narr15 = new SqlParameter("@Narrative15", SqlDbType.VarChar);
        p_299Narr15.Value = txt_299_Narr15.Text.Trim();
        SqlParameter p_299Narr16 = new SqlParameter("@Narrative16", SqlDbType.VarChar);
        p_299Narr16.Value = txt_299_Narr16.Text.Trim();
        SqlParameter p_299Narr17 = new SqlParameter("@Narrative17", SqlDbType.VarChar);
        p_299Narr17.Value = txt_299_Narr17.Text.Trim();
        SqlParameter p_299Narr18 = new SqlParameter("@Narrative18", SqlDbType.VarChar);
        p_299Narr18.Value = txt_299_Narr18.Text.Trim();
        SqlParameter p_299Narr19 = new SqlParameter("@Narrative19", SqlDbType.VarChar);
        p_299Narr19.Value = txt_299_Narr19.Text.Trim();
        SqlParameter p_299Narr20 = new SqlParameter("@Narrative20", SqlDbType.VarChar);
        p_299Narr20.Value = txt_299_Narr20.Text.Trim();
        SqlParameter p_299Narr21 = new SqlParameter("@Narrative21", SqlDbType.VarChar);
        p_299Narr21.Value = txt_299_Narr21.Text.Trim();
        SqlParameter p_299Narr22 = new SqlParameter("@Narrative22", SqlDbType.VarChar);
        p_299Narr22.Value = txt_299_Narr22.Text.Trim();
        SqlParameter p_299Narr23 = new SqlParameter("@Narrative23", SqlDbType.VarChar);
        p_299Narr23.Value = txt_299_Narr23.Text.Trim();
        SqlParameter p_299Narr24 = new SqlParameter("@Narrative24", SqlDbType.VarChar);
        p_299Narr24.Value = txt_299_Narr24.Text.Trim();
        SqlParameter p_299Narr25 = new SqlParameter("@Narrative25", SqlDbType.VarChar);
        p_299Narr25.Value = txt_299_Narr25.Text.Trim();
        SqlParameter p_299Narr26 = new SqlParameter("@Narrative26", SqlDbType.VarChar);
        p_299Narr26.Value = txt_299_Narr26.Text.Trim();
        SqlParameter p_299Narr27 = new SqlParameter("@Narrative27", SqlDbType.VarChar);
        p_299Narr27.Value = txt_299_Narr27.Text.Trim();
        SqlParameter p_299Narr28 = new SqlParameter("@Narrative28", SqlDbType.VarChar);
        p_299Narr28.Value = txt_299_Narr28.Text.Trim();
        SqlParameter p_299Narr29 = new SqlParameter("@Narrative29", SqlDbType.VarChar);
        p_299Narr29.Value = txt_299_Narr29.Text.Trim();
        SqlParameter p_299Narr30 = new SqlParameter("@Narrative30", SqlDbType.VarChar);
        p_299Narr30.Value = txt_299_Narr30.Text.Trim();
        SqlParameter p_299Narr31 = new SqlParameter("@Narrative31", SqlDbType.VarChar);
        p_299Narr31.Value = txt_299_Narr31.Text.Trim();
        SqlParameter p_299Narr32 = new SqlParameter("@Narrative32", SqlDbType.VarChar);
        p_299Narr32.Value = txt_299_Narr32.Text.Trim();
        SqlParameter p_299Narr33 = new SqlParameter("@Narrative33", SqlDbType.VarChar);
        p_299Narr33.Value = txt_299_Narr33.Text.Trim();
        SqlParameter p_299Narr34 = new SqlParameter("@Narrative34", SqlDbType.VarChar);
        p_299Narr34.Value = txt_299_Narr34.Text.Trim();
        SqlParameter p_299Narr35 = new SqlParameter("@Narrative35", SqlDbType.VarChar);
        p_299Narr35.Value = txt_299_Narr35.Text.Trim();

        SqlParameter P_AddedBy = new SqlParameter("@AddedBy", hdnUserName);
        SqlParameter P_AddedDate = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        SqlParameter P_UpdatedBy = new SqlParameter("@UpdatedBy", hdnUserName);
        SqlParameter P_UpdatedDate = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData("TF_Exp_AddUpdateSwift299", p_Branch, p_SwiftType, p_txtReceiver299, p_299TranRefno, p_299RelatedRef, p_299Narr1, p_299Narr2, p_299Narr3, p_299Narr4, p_299Narr5,
            p_299Narr6, p_299Narr7, p_299Narr8, p_299Narr9, p_299Narr10, p_299Narr11, p_299Narr12, p_299Narr13, p_299Narr14, p_299Narr15, p_299Narr16, p_299Narr17, p_299Narr18,
            p_299Narr19, p_299Narr20, p_299Narr21, p_299Narr22, p_299Narr23, p_299Narr24, p_299Narr25, p_299Narr26, p_299Narr27, p_299Narr28, p_299Narr29, p_299Narr30,
           p_299Narr31, p_299Narr32, p_299Narr33, p_299Narr34, p_299Narr35, P_AddedBy, P_AddedDate, P_UpdatedBy, P_UpdatedDate);

        if (_result == "added")
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added as Draft.');", true);
        }
        else if (_result == "Updated")
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated successfully.');", true);
        }
        else
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Error');", true);
        }

    }
    public void Sendtochecker999()
    {
        string hdnUserName = Session["userName"].ToString();
        SqlParameter p_Branch = new SqlParameter("@Branch", SqlDbType.VarChar);
        p_Branch.Value = ddlBranch.SelectedItem.Text;
        SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
        p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;

        SqlParameter p_txtReceiver999 = new SqlParameter("@Receiver", SqlDbType.VarChar);
        p_txtReceiver999.Value = txtReceiver999.Text.Trim();

        SqlParameter p_999TranRefno = new SqlParameter("@Trans_Ref_No", SqlDbType.VarChar);
        p_999TranRefno.Value = txt_999_transRefNo.Text.Trim();

        SqlParameter p_999RelatedRef = new SqlParameter("@Related_Ref", SqlDbType.VarChar);
        p_999RelatedRef.Value = txt_999_RelRef.Text.Trim();
        SqlParameter p_999Narr1 = new SqlParameter("@Narrative1", SqlDbType.VarChar);
        p_999Narr1.Value = txt_999_Narr1.Text.Trim();
        SqlParameter p_999Narr2 = new SqlParameter("@Narrative2", SqlDbType.VarChar);
        p_999Narr2.Value = txt_999_Narr2.Text.Trim();
        SqlParameter p_999Narr3 = new SqlParameter("@Narrative3", SqlDbType.VarChar);
        p_999Narr3.Value = txt_999_Narr3.Text.Trim();
        SqlParameter p_999Narr4 = new SqlParameter("@Narrative4", SqlDbType.VarChar);
        p_999Narr4.Value = txt_999_Narr4.Text.Trim();
        SqlParameter p_999Narr5 = new SqlParameter("@Narrative5", SqlDbType.VarChar);
        p_999Narr5.Value = txt_999_Narr5.Text.Trim();
        SqlParameter p_999Narr6 = new SqlParameter("@Narrative6", SqlDbType.VarChar);
        p_999Narr6.Value = txt_999_Narr6.Text.Trim();
        SqlParameter p_999Narr7 = new SqlParameter("@Narrative7", SqlDbType.VarChar);
        p_999Narr7.Value = txt_999_Narr7.Text.Trim();
        SqlParameter p_999Narr8 = new SqlParameter("@Narrative8", SqlDbType.VarChar);
        p_999Narr8.Value = txt_999_Narr8.Text.Trim();
        SqlParameter p_999Narr9 = new SqlParameter("@Narrative9", SqlDbType.VarChar);
        p_999Narr9.Value = txt_999_Narr9.Text.Trim();
        SqlParameter p_999Narr10 = new SqlParameter("@Narrative10", SqlDbType.VarChar);
        p_999Narr10.Value = txt_999_Narr10.Text.Trim();
        SqlParameter p_999Narr11 = new SqlParameter("@Narrative11", SqlDbType.VarChar);
        p_999Narr11.Value = txt_999_Narr11.Text.Trim();
        SqlParameter p_999Narr12 = new SqlParameter("@Narrative12", SqlDbType.VarChar);
        p_999Narr12.Value = txt_999_Narr12.Text.Trim();
        SqlParameter p_999Narr13 = new SqlParameter("@Narrative13", SqlDbType.VarChar);
        p_999Narr13.Value = txt_999_Narr13.Text.Trim();
        SqlParameter p_999Narr14 = new SqlParameter("@Narrative14", SqlDbType.VarChar);
        p_999Narr14.Value = txt_999_Narr14.Text.Trim();
        SqlParameter p_999Narr15 = new SqlParameter("@Narrative15", SqlDbType.VarChar);
        p_999Narr15.Value = txt_999_Narr15.Text.Trim();
        SqlParameter p_999Narr16 = new SqlParameter("@Narrative16", SqlDbType.VarChar);
        p_999Narr16.Value = txt_999_Narr16.Text.Trim();
        SqlParameter p_999Narr17 = new SqlParameter("@Narrative17", SqlDbType.VarChar);
        p_999Narr17.Value = txt_999_Narr17.Text.Trim();
        SqlParameter p_999Narr18 = new SqlParameter("@Narrative18", SqlDbType.VarChar);
        p_999Narr18.Value = txt_999_Narr18.Text.Trim();
        SqlParameter p_999Narr19 = new SqlParameter("@Narrative19", SqlDbType.VarChar);
        p_999Narr19.Value = txt_999_Narr19.Text.Trim();
        SqlParameter p_999Narr20 = new SqlParameter("@Narrative20", SqlDbType.VarChar);
        p_999Narr20.Value = txt_999_Narr20.Text.Trim();
        SqlParameter p_999Narr21 = new SqlParameter("@Narrative21", SqlDbType.VarChar);
        p_999Narr21.Value = txt_999_Narr21.Text.Trim();
        SqlParameter p_999Narr22 = new SqlParameter("@Narrative22", SqlDbType.VarChar);
        p_999Narr22.Value = txt_999_Narr22.Text.Trim();
        SqlParameter p_999Narr23 = new SqlParameter("@Narrative23", SqlDbType.VarChar);
        p_999Narr23.Value = txt_999_Narr23.Text.Trim();
        SqlParameter p_999Narr24 = new SqlParameter("@Narrative24", SqlDbType.VarChar);
        p_999Narr24.Value = txt_999_Narr24.Text.Trim();
        SqlParameter p_999Narr25 = new SqlParameter("@Narrative25", SqlDbType.VarChar);
        p_999Narr25.Value = txt_999_Narr25.Text.Trim();
        SqlParameter p_999Narr26 = new SqlParameter("@Narrative26", SqlDbType.VarChar);
        p_999Narr26.Value = txt_999_Narr26.Text.Trim();
        SqlParameter p_999Narr27 = new SqlParameter("@Narrative27", SqlDbType.VarChar);
        p_999Narr27.Value = txt_999_Narr27.Text.Trim();
        SqlParameter p_999Narr28 = new SqlParameter("@Narrative28", SqlDbType.VarChar);
        p_999Narr28.Value = txt_999_Narr28.Text.Trim();
        SqlParameter p_999Narr29 = new SqlParameter("@Narrative29", SqlDbType.VarChar);
        p_999Narr29.Value = txt_999_Narr29.Text.Trim();
        SqlParameter p_999Narr30 = new SqlParameter("@Narrative30", SqlDbType.VarChar);
        p_999Narr30.Value = txt_999_Narr30.Text.Trim();
        SqlParameter p_999Narr31 = new SqlParameter("@Narrative31", SqlDbType.VarChar);
        p_999Narr31.Value = txt_999_Narr31.Text.Trim();
        SqlParameter p_999Narr32 = new SqlParameter("@Narrative32", SqlDbType.VarChar);
        p_999Narr32.Value = txt_999_Narr32.Text.Trim();
        SqlParameter p_999Narr33 = new SqlParameter("@Narrative33", SqlDbType.VarChar);
        p_999Narr33.Value = txt_999_Narr33.Text.Trim();
        SqlParameter p_999Narr34 = new SqlParameter("@Narrative34", SqlDbType.VarChar);
        p_999Narr34.Value = txt_999_Narr34.Text.Trim();
        SqlParameter p_999Narr35 = new SqlParameter("@Narrative35", SqlDbType.VarChar);
        p_999Narr35.Value = txt_999_Narr35.Text.Trim();

        SqlParameter P_AddedBy = new SqlParameter("@AddedBy", hdnUserName);
        SqlParameter P_AddedDate = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        SqlParameter P_UpdatedBy = new SqlParameter("@UpdatedBy", hdnUserName);
        SqlParameter P_UpdatedDate = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData("TF_Exp_AddUpdateSwift999", p_Branch, p_SwiftType, p_txtReceiver999, p_999TranRefno, p_999RelatedRef, p_999Narr1, p_999Narr2, p_999Narr3, p_999Narr4, p_999Narr5,
            p_999Narr6, p_999Narr7, p_999Narr8, p_999Narr9, p_999Narr10, p_999Narr11, p_999Narr12, p_999Narr13, p_999Narr14, p_999Narr15, p_999Narr16, p_999Narr17, p_999Narr18,
            p_999Narr19, p_999Narr20, p_999Narr21, p_999Narr22, p_999Narr23, p_999Narr24, p_999Narr25, p_999Narr26, p_999Narr27, p_999Narr28, p_999Narr29, p_999Narr30,
           p_999Narr31, p_999Narr32, p_999Narr33, p_999Narr34, p_999Narr35, P_AddedBy, P_AddedDate, P_UpdatedBy, P_UpdatedDate);

        if (_result == "added")
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added as Draft.');", true);
        }
        else if (_result == "Updated")
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated successfully.');", true);
        }
        else
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Error');", true);
        }

    }

    public void SaveView799()
    {
        string hdnUserName = Session["userName"].ToString();
        if (txtReceiver799.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter receiver');", true);
            txtReceiver799.Focus();
        }
        else if (txt_799_transRefNo.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Transaction Reference Number');", true);
            txt_799_transRefNo.Focus();
        }
        else if (txt_799_transRefNo.Text.Contains("//"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number contains two consecutive slashes //');", true);
            txt_799_transRefNo.Focus();
        }
        else if (txt_799_transRefNo.Text.StartsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number start with slash(/)');", true);
            txt_799_transRefNo.Focus();
        }
        else if (txt_799_transRefNo.Text.EndsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number end with slash(/)');", true);
            txt_799_transRefNo.Focus();
        }

        else if (txt_799_RelRef.Text.Contains("//"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference contains two consecutive slashes //');", true);
            txt_799_RelRef.Focus();
        }
        else if (txt_799_RelRef.Text.StartsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference start with slash(/)');", true);
            txt_799_RelRef.Focus();
        }
        else if (txt_799_RelRef.Text.EndsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Relateds Reference end with slash(/)');", true);
            txt_799_RelRef.Focus();
        }

        else if (txt_799_Narr1.Text == "" && txt_799_Narr2.Text == "" && txt_799_Narr3.Text == "" && txt_799_Narr4.Text == "" && txt_799_Narr5.Text == "" && txt_799_Narr6.Text == "" && txt_799_Narr7.Text == "" && txt_799_Narr8.Text == "" && txt_799_Narr9.Text == "" && txt_799_Narr10.Text == ""
        && txt_799_Narr11.Text == "" && txt_799_Narr12.Text == "" && txt_799_Narr13.Text == "" && txt_799_Narr14.Text == "" && txt_799_Narr15.Text == "" && txt_799_Narr16.Text == "" && txt_799_Narr17.Text == "" && txt_799_Narr18.Text == "" && txt_799_Narr19.Text == "" && txt_799_Narr20.Text == ""
        && txt_799_Narr21.Text == "" && txt_799_Narr22.Text == "" && txt_799_Narr23.Text == "" && txt_799_Narr24.Text == "" && txt_799_Narr25.Text == "" && txt_799_Narr26.Text == "" && txt_799_Narr27.Text == "" && txt_799_Narr28.Text == "" && txt_799_Narr29.Text == "" && txt_799_Narr30.Text == ""
        && txt_799_Narr31.Text == "" && txt_799_Narr32.Text == "" && txt_799_Narr33.Text == "" && txt_799_Narr34.Text == "" && txt_799_Narr35.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Narrative');", true);
            txt_799_Narr1.Focus();
        }
        else
        {
            SqlParameter p_Branch = new SqlParameter("@Branch", SqlDbType.VarChar);
            p_Branch.Value = ddlBranch.SelectedItem.Text;
            SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
            p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;

            SqlParameter p_txtReceiver799 = new SqlParameter("@Receiver", SqlDbType.VarChar);
            p_txtReceiver799.Value = txtReceiver799.Text.Trim();

            SqlParameter p_799TranRefno = new SqlParameter("@Trans_Ref_No", SqlDbType.VarChar);
            p_799TranRefno.Value = txt_799_transRefNo.Text.Trim();

            SqlParameter p_799RelatedRef = new SqlParameter("@Related_Ref", SqlDbType.VarChar);
            p_799RelatedRef.Value = txt_799_RelRef.Text.Trim();
            SqlParameter p_799Narr1 = new SqlParameter("@Narrative1", SqlDbType.VarChar);
            p_799Narr1.Value = txt_799_Narr1.Text.Trim();
            SqlParameter p_799Narr2 = new SqlParameter("@Narrative2", SqlDbType.VarChar);
            p_799Narr2.Value = txt_799_Narr2.Text.Trim();
            SqlParameter p_799Narr3 = new SqlParameter("@Narrative3", SqlDbType.VarChar);
            p_799Narr3.Value = txt_799_Narr3.Text.Trim();
            SqlParameter p_799Narr4 = new SqlParameter("@Narrative4", SqlDbType.VarChar);
            p_799Narr4.Value = txt_799_Narr4.Text.Trim();
            SqlParameter p_799Narr5 = new SqlParameter("@Narrative5", SqlDbType.VarChar);
            p_799Narr5.Value = txt_799_Narr5.Text.Trim();
            SqlParameter p_799Narr6 = new SqlParameter("@Narrative6", SqlDbType.VarChar);
            p_799Narr6.Value = txt_799_Narr6.Text.Trim();
            SqlParameter p_799Narr7 = new SqlParameter("@Narrative7", SqlDbType.VarChar);
            p_799Narr7.Value = txt_799_Narr7.Text.Trim();
            SqlParameter p_799Narr8 = new SqlParameter("@Narrative8", SqlDbType.VarChar);
            p_799Narr8.Value = txt_799_Narr8.Text.Trim();
            SqlParameter p_799Narr9 = new SqlParameter("@Narrative9", SqlDbType.VarChar);
            p_799Narr9.Value = txt_799_Narr9.Text.Trim();
            SqlParameter p_799Narr10 = new SqlParameter("@Narrative10", SqlDbType.VarChar);
            p_799Narr10.Value = txt_799_Narr10.Text.Trim();
            SqlParameter p_799Narr11 = new SqlParameter("@Narrative11", SqlDbType.VarChar);
            p_799Narr11.Value = txt_799_Narr11.Text.Trim();
            SqlParameter p_799Narr12 = new SqlParameter("@Narrative12", SqlDbType.VarChar);
            p_799Narr12.Value = txt_799_Narr12.Text.Trim();
            SqlParameter p_799Narr13 = new SqlParameter("@Narrative13", SqlDbType.VarChar);
            p_799Narr13.Value = txt_799_Narr13.Text.Trim();
            SqlParameter p_799Narr14 = new SqlParameter("@Narrative14", SqlDbType.VarChar);
            p_799Narr14.Value = txt_799_Narr14.Text.Trim();
            SqlParameter p_799Narr15 = new SqlParameter("@Narrative15", SqlDbType.VarChar);
            p_799Narr15.Value = txt_799_Narr15.Text.Trim();
            SqlParameter p_799Narr16 = new SqlParameter("@Narrative16", SqlDbType.VarChar);
            p_799Narr16.Value = txt_799_Narr16.Text.Trim();
            SqlParameter p_799Narr17 = new SqlParameter("@Narrative17", SqlDbType.VarChar);
            p_799Narr17.Value = txt_799_Narr17.Text.Trim();
            SqlParameter p_799Narr18 = new SqlParameter("@Narrative18", SqlDbType.VarChar);
            p_799Narr18.Value = txt_799_Narr18.Text.Trim();
            SqlParameter p_799Narr19 = new SqlParameter("@Narrative19", SqlDbType.VarChar);
            p_799Narr19.Value = txt_799_Narr19.Text.Trim();
            SqlParameter p_799Narr20 = new SqlParameter("@Narrative20", SqlDbType.VarChar);
            p_799Narr20.Value = txt_799_Narr20.Text.Trim();
            SqlParameter p_799Narr21 = new SqlParameter("@Narrative21", SqlDbType.VarChar);
            p_799Narr21.Value = txt_799_Narr21.Text.Trim();
            SqlParameter p_799Narr22 = new SqlParameter("@Narrative22", SqlDbType.VarChar);
            p_799Narr22.Value = txt_799_Narr22.Text.Trim();
            SqlParameter p_799Narr23 = new SqlParameter("@Narrative23", SqlDbType.VarChar);
            p_799Narr23.Value = txt_799_Narr23.Text.Trim();
            SqlParameter p_799Narr24 = new SqlParameter("@Narrative24", SqlDbType.VarChar);
            p_799Narr24.Value = txt_799_Narr24.Text.Trim();
            SqlParameter p_799Narr25 = new SqlParameter("@Narrative25", SqlDbType.VarChar);
            p_799Narr25.Value = txt_799_Narr25.Text.Trim();
            SqlParameter p_799Narr26 = new SqlParameter("@Narrative26", SqlDbType.VarChar);
            p_799Narr26.Value = txt_799_Narr26.Text.Trim();
            SqlParameter p_799Narr27 = new SqlParameter("@Narrative27", SqlDbType.VarChar);
            p_799Narr27.Value = txt_799_Narr27.Text.Trim();
            SqlParameter p_799Narr28 = new SqlParameter("@Narrative28", SqlDbType.VarChar);
            p_799Narr28.Value = txt_799_Narr28.Text.Trim();
            SqlParameter p_799Narr29 = new SqlParameter("@Narrative29", SqlDbType.VarChar);
            p_799Narr29.Value = txt_799_Narr29.Text.Trim();
            SqlParameter p_799Narr30 = new SqlParameter("@Narrative30", SqlDbType.VarChar);
            p_799Narr30.Value = txt_799_Narr30.Text.Trim();
            SqlParameter p_799Narr31 = new SqlParameter("@Narrative31", SqlDbType.VarChar);
            p_799Narr31.Value = txt_799_Narr31.Text.Trim();
            SqlParameter p_799Narr32 = new SqlParameter("@Narrative32", SqlDbType.VarChar);
            p_799Narr32.Value = txt_799_Narr32.Text.Trim();
            SqlParameter p_799Narr33 = new SqlParameter("@Narrative33", SqlDbType.VarChar);
            p_799Narr33.Value = txt_799_Narr33.Text.Trim();
            SqlParameter p_799Narr34 = new SqlParameter("@Narrative34", SqlDbType.VarChar);
            p_799Narr34.Value = txt_799_Narr34.Text.Trim();
            SqlParameter p_799Narr35 = new SqlParameter("@Narrative35", SqlDbType.VarChar);
            p_799Narr35.Value = txt_799_Narr35.Text.Trim();

            SqlParameter P_AddedBy = new SqlParameter("@AddedBy", hdnUserName);
            SqlParameter P_AddedDate = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            SqlParameter P_UpdatedBy = new SqlParameter("@UpdatedBy", hdnUserName);
            SqlParameter P_UpdatedDate = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

            TF_DATA objSave = new TF_DATA();
            _result = objSave.SaveDeleteData("TF_Exp_AddUpdateSwift799", p_Branch, p_SwiftType, p_txtReceiver799, p_799TranRefno, p_799RelatedRef, p_799Narr1, p_799Narr2, p_799Narr3, p_799Narr4, p_799Narr5,
                p_799Narr6, p_799Narr7, p_799Narr8, p_799Narr9, p_799Narr10, p_799Narr11, p_799Narr12, p_799Narr13, p_799Narr14, p_799Narr15, p_799Narr16, p_799Narr17, p_799Narr18,
                p_799Narr19, p_799Narr20, p_799Narr21, p_799Narr22, p_799Narr23, p_799Narr24, p_799Narr25, p_799Narr26, p_799Narr27, p_799Narr28, p_799Narr29, p_799Narr30,
               p_799Narr31, p_799Narr32, p_799Narr33, p_799Narr34, p_799Narr35, P_AddedBy, P_AddedDate, P_UpdatedBy, P_UpdatedDate);

            if (_result == "added")
            {
                audittrail("M", txt_799_transRefNo.Text.Trim());
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added as Draft.');", true);
            }
            else if (_result == "Updated")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Error');", true);
            }
        }
    }
    public void SaveView754()
    {
        string hdnUserName = Session["userName"].ToString();
        if (txtReceiver754.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Receiver');", true);
        }

        else if (txt_754_SenRef.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Sender Reference');", true);
        }
        else if (txt_754_SenRef.Text.Contains("//"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Sender Reference contains two consecutive slashes //');", true);
        }
        else if (txt_754_SenRef.Text.StartsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Sender Reference start with slash(/)');", true);
        }
        else if (txt_754_SenRef.Text.EndsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Sender Reference end with slash(/)');", true);
        }

        else if (txt_754_RelRef.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Related Reference');", true);
        }
        else if (txt_754_RelRef.Text.Contains("//"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference contains two consecutive slashes //');", true);
        }
        else if (txt_754_RelRef.Text.StartsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference start with slash(/)');", true);
        }
        else if (txt_754_RelRef.Text.EndsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference end with slash(/)');", true);
        }

        else if (txtPrinAmtPaidAccNegoDate_754.Text == "" && ddlPrinAmtPaidAccNego_754.SelectedValue == "A")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Principal Amount Date should not be blank');", true);
        }
        else if (ddl_PrinAmtPaidAccNegoCurr_754.SelectedItem.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Principal Amount Currency');", true);
        }
        else if (txtPrinAmtPaidAccNegoAmt_754.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter amount in principle amount field');", true);
            txtPrinAmtPaidAccNegoAmt_754.Focus();
        }
        else if (ddl_PrinAmtPaidAccNegoCurr_754.SelectedItem.Text == "JPY" && txtPrinAmtPaidAccNegoAmt_754.Text.Contains("."))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('If currency is JPY then Decimal is not allowed in amount.');", true);
            txtPrinAmtPaidAccNegoAmt_754.Focus();
        }
        else if (Regex.IsMatch(txtPrinAmtPaidAccNegoAmt_754.Text, @"\.\d\d\d"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal in principle amt field');", true);
            txtPrinAmtPaidAccNegoAmt_754.Focus();
        }
        else if (txtPrinAmtPaidAccNegoAmt_754.Text.Contains(','))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please do not enter (,) in principle amount field');", true);
            txtPrinAmtPaidAccNegoAmt_754.Focus();
        }
        else if (ddl_754_AddAmtClamd_Ccy.SelectedItem.Text == "JPY" && txt_754_AddAmtClamd_Amt.Text.Contains("."))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('If currency is JPY then Decimal is not allowed in amount.');", true);
            txt_754_AddAmtClamd_Amt.Focus();
        }
        else if (ddl_754_TotalAmtClmd_Ccy.SelectedItem.Text == "JPY" && txt_754_TotalAmtClmd_Amt.Text.Contains("."))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('If currency is JPY then Decimal is not allowed in amount.');", true);
            txt_754_TotalAmtClmd_Amt.Focus();
        }

        else if (Regex.IsMatch(txt_754_TotalAmtClmd_Amt.Text, @"\.\d\d\d"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal in Total amount claimed field');", true);
            txt_754_TotalAmtClmd_Amt.Focus();
        }

        else if (txt_754_TotalAmtClmd_Amt.Text.Contains(','))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please do not enter (,) in Total amount claimed field');", true);
            txt_754_TotalAmtClmd_Amt.Focus();
        }
        else if (Regex.IsMatch(txt_754_AddAmtClamd_Amt.Text, @"\.\d\d\d"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal in Total amount claimed field');", true);
            txt_754_AddAmtClamd_Amt.Focus();
        }

        else if (txtReimbursingBankAccountnumber_754.Text != "" && txtAccountwithBankAccountnumber_754.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
        }

        else if (ddl_754_TotalAmtClmd_Ccy.Text != "" && ddl_754_TotalAmtClmd_Ccy.Text != ddl_PrinAmtPaidAccNegoCurr_754.Text)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('The currency code in the fields 32a and 34a must be the same ');", true);
        }

        else if ((txt_754_SenRecInfo1.Text + txt_754_SenRecInfo2.Text + txt_754_SenRecInfo3.Text +
        txt_754_SenRecInfo4.Text + txt_754_SenRecInfo5.Text + txt_754_SenRecInfo6.Text != "") &&
        (txt_754_Narr1.Text + txt_754_Narr2.Text + txt_754_Narr3.Text + txt_754_Narr4.Text + txt_754_Narr5.Text +
        txt_754_Narr6.Text + txt_754_Narr7.Text + txt_754_Narr8.Text + txt_754_Narr9.Text + txt_754_Narr10.Text +
        txt_754_Narr11.Text + txt_754_Narr12.Text + txt_754_Narr13.Text + txt_754_Narr14.Text + txt_754_Narr15.Text +
        txt_754_Narr16.Text + txt_754_Narr17.Text + txt_754_Narr18.Text + txt_754_Narr19.Text + txt_754_Narr20.Text != ""))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 72Z or 77 may be present, but not both');", true);
        }

        else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankIdentifiercode_754.Text != "") &&
       (txtAccountwithBankAccountnumber_754.Text + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankIdentifiercode_754.Text != ""))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
        }

        else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankLocation_754.Text != "") &&
           (txtAccountwithBankAccountnumber_754.Text + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankLocation_754.Text != ""))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
        }

        else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankName_754.Text +
            txtReimbursingBankAddress1_754 + txtReimbursingBankAddress2_754 + txtReimbursingBankAddress3_754 != "") &&
          (txtAccountwithBankAccountnumber_754.Text + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankName_754.Text +
          txtAccountwithBankAddress1_754.Text + txtAccountwithBankAddress2_754.Text + txtAccountwithBankAddress3_754.Text != ""))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
        }

        else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankIdentifiercode_754.Text != "") &&
           (txtAccountwithBankAccountnumber_754.Text + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankLocation_754.Text != ""))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
        }

        else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankIdentifiercode_754.Text != "") &&
           (txtAccountwithBankAccountnumber_754.Text + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankName_754.Text +
           txtAccountwithBankAddress1_754.Text + txtAccountwithBankAddress2_754.Text + txtAccountwithBankAddress3_754.Text != ""))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
        }

        else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankLocation_754.Text != "") &&
           (txtAccountwithBankAccountnumber_754.Text + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankIdentifiercode_754.Text != ""))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
        }

        else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankLocation_754.Text != "") &&
           (txtAccountwithBankAccountnumber_754.Text + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankName_754.Text +
           txtAccountwithBankAddress1_754.Text + txtAccountwithBankAddress2_754.Text + txtAccountwithBankAddress3_754.Text != ""))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
        }

        else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankName_754.Text +
            txtReimbursingBankAddress1_754.Text + txtReimbursingBankAddress2_754.Text + txtReimbursingBankAddress3_754.Text != "") &&
           (txtAccountwithBankAccountnumber_754.Text + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankIdentifiercode_754.Text != ""))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
        }

        else if ((txtReimbursingBankAccountnumber_754.Text + txtReimbursingBankAccountnumber1_754.Text + txtReimbursingBankName_754.Text +
            txtReimbursingBankAddress1_754.Text + txtReimbursingBankAddress2_754.Text + txtReimbursingBankAddress3_754.Text != "") && (txtAccountwithBankAccountnumber_754.Text
            + txtAccountwithBankAccountnumber1_754.Text + txtAccountwithBankLocation_754.Text != ""))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Either field 53a or 57a may be present, but not both');", true);
        }
        else
        {
            SqlParameter p_Branch = new SqlParameter("@Branch", SqlDbType.VarChar);
            p_Branch.Value = ddlBranch.SelectedItem.Text;
            SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
            p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;

            SqlParameter p_txtReceiver754 = new SqlParameter("@Receiver", SqlDbType.VarChar);
            p_txtReceiver754.Value = txtReceiver754.Text.Trim();


            SqlParameter p_754SenRefno = new SqlParameter("@Send_Ref", SqlDbType.VarChar);
            p_754SenRefno.Value = txt_754_SenRef.Text.Trim();


            SqlParameter p_754RelRef = new SqlParameter("@Related_Ref", SqlDbType.VarChar);
            p_754RelRef.Value = txt_754_RelRef.Text.Trim();

            SqlParameter p_ddlPrinAmtPaidAccNegoAmt_754 = new SqlParameter("@ddlPrincipalAmtPaidAccptdNego", SqlDbType.VarChar);
            p_ddlPrinAmtPaidAccNegoAmt_754.Value = ddlPrinAmtPaidAccNego_754.SelectedValue;



            SqlParameter p_txtPrinAmtPaidAccNegoDate_754 = new SqlParameter("@PrincipalAmtPaidAccptdNego_Date", SqlDbType.VarChar);
            p_txtPrinAmtPaidAccNegoDate_754.Value = txtPrinAmtPaidAccNegoDate_754.Text.Trim();
            SqlParameter p_ddl_PrinAmtPaidAccNegoCurr_754 = new SqlParameter("@PrincipalAmtPaidAccptdNego_Curr", SqlDbType.VarChar);
            p_ddl_PrinAmtPaidAccNegoCurr_754.Value = ddl_PrinAmtPaidAccNegoCurr_754.Text.Trim();

            SqlParameter p_txtPrinAmtPaidAccNegoAmt_754 = new SqlParameter("@PrincipalAmtPaidAccptdNego_Amt", SqlDbType.VarChar);
            p_txtPrinAmtPaidAccNegoAmt_754.Value = txtPrinAmtPaidAccNegoAmt_754.Text.Trim();

            SqlParameter p_ddl_754_AddAmtClamd_Ccy = new SqlParameter("@AddnlAmt_Curr", SqlDbType.VarChar);
            p_ddl_754_AddAmtClamd_Ccy.Value = ddl_754_AddAmtClamd_Ccy.Text.Trim();


            SqlParameter p_txt_754_AddAmtClamd_Amt = new SqlParameter("@AddnlAmt_Amt", SqlDbType.VarChar);
            p_txt_754_AddAmtClamd_Amt.Value = txt_754_AddAmtClamd_Amt.Text.Trim();

            SqlParameter p_txt_754_ChargesDeduct1 = new SqlParameter("@Charges1", SqlDbType.VarChar);
            p_txt_754_ChargesDeduct1.Value = txt_754_ChargesDeduct1.Text.Trim();
            SqlParameter p_txt_754_ChargesDeduct2 = new SqlParameter("@Charges2", SqlDbType.VarChar);
            p_txt_754_ChargesDeduct2.Value = txt_754_ChargesDeduct2.Text.Trim();
            SqlParameter p_txt_754_ChargesDeduct3 = new SqlParameter("@Charges3", SqlDbType.VarChar);
            p_txt_754_ChargesDeduct3.Value = txt_754_ChargesDeduct3.Text.Trim();
            SqlParameter p_txt_754_ChargesDeduct4 = new SqlParameter("@Charges4", SqlDbType.VarChar);
            p_txt_754_ChargesDeduct4.Value = txt_754_ChargesDeduct4.Text.Trim();
            SqlParameter p_txt_754_ChargesDeduct5 = new SqlParameter("@Charges5", SqlDbType.VarChar);
            p_txt_754_ChargesDeduct5.Value = txt_754_ChargesDeduct5.Text.Trim();
            SqlParameter p_txt_754_ChargesDeduct6 = new SqlParameter("@Charges6", SqlDbType.VarChar);
            p_txt_754_ChargesDeduct6.Value = txt_754_ChargesDeduct6.Text.Trim();

            SqlParameter p_txt_754_ChargesAdded1 = new SqlParameter("@ChargesAdded1", SqlDbType.VarChar);
            p_txt_754_ChargesAdded1.Value = txt_754_ChargesAdded1.Text.Trim();
            SqlParameter p_txt_754_ChargesAdded2 = new SqlParameter("@ChargesAdded2", SqlDbType.VarChar);
            p_txt_754_ChargesAdded2.Value = txt_754_ChargesAdded2.Text.Trim();
            SqlParameter p_txt_754_ChargesAdded3 = new SqlParameter("@ChargesAdded3", SqlDbType.VarChar);
            p_txt_754_ChargesAdded3.Value = txt_754_ChargesAdded3.Text.Trim();
            SqlParameter p_txt_754_ChargesAdded4 = new SqlParameter("@ChargesAdded4", SqlDbType.VarChar);
            p_txt_754_ChargesAdded4.Value = txt_754_ChargesAdded4.Text.Trim();
            SqlParameter p_txt_754_ChargesAdded5 = new SqlParameter("@ChargesAdded5", SqlDbType.VarChar);
            p_txt_754_ChargesAdded5.Value = txt_754_ChargesAdded5.Text.Trim();
            SqlParameter p_txt_754_ChargesAdded6 = new SqlParameter("@ChargesAdded6", SqlDbType.VarChar);
            p_txt_754_ChargesAdded6.Value = txt_754_ChargesAdded6.Text.Trim();

            SqlParameter p_ddlTotalAmtclamd_754 = new SqlParameter("@ddlTotalAmtClaimed", SqlDbType.VarChar);
            p_ddlTotalAmtclamd_754.Value = ddlTotalAmtclamd_754.SelectedValue;
            SqlParameter p_txt_754_TotalAmtClmd_Date = new SqlParameter("@TotalAmtClaimed_Date", SqlDbType.VarChar);
            p_txt_754_TotalAmtClmd_Date.Value = txt_754_TotalAmtClmd_Date.Text.Trim();
            SqlParameter p_ddl_754_TotalAmtClmd_Ccy = new SqlParameter("@TotalAmtClaimed_Curr", SqlDbType.VarChar);
            p_ddl_754_TotalAmtClmd_Ccy.Value = ddl_754_TotalAmtClmd_Ccy.Text.Trim();

            SqlParameter p_txt_754_TotalAmtClmd_Amt = new SqlParameter("@TotalAmtClaimed_Amt", SqlDbType.VarChar);
            p_txt_754_TotalAmtClmd_Amt.Value = txt_754_TotalAmtClmd_Amt.Text.Trim();

            //if (ddl_754_TotalAmtClmd_Ccy.Text != "")
            //{
            //    if (ddl_754_TotalAmtClmd_Ccy.Text != ddl_PrinAmtPaidAccNegoCurr_754.Text)
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('The currency code in the fields 32a and 34a must be the same ');", true);
            //    }
            //}

            SqlParameter p_ddlReimbursingbank_754 = new SqlParameter("@ddlReimbursingBank", SqlDbType.VarChar);
            p_ddlReimbursingbank_754.Value = ddlReimbursingbank_754.SelectedValue;
            SqlParameter p_txtReimbursingBankAccountnumber_754 = new SqlParameter("@ReimbursingBank_PartyIdent", SqlDbType.VarChar);
            p_txtReimbursingBankAccountnumber_754.Value = txtReimbursingBankAccountnumber_754.Text.Trim();
            SqlParameter p_txtReimbursingBankAccountnumber1_754 = new SqlParameter("@ReimbursingBank_PartyIdent1", SqlDbType.VarChar);
            p_txtReimbursingBankAccountnumber1_754.Value = txtReimbursingBankAccountnumber1_754.Text.Trim();
            SqlParameter p_txtReimbursingBankIdentifiercode_754 = new SqlParameter("@ReimbursingBank_Identcode", SqlDbType.VarChar);
            p_txtReimbursingBankIdentifiercode_754.Value = txtReimbursingBankIdentifiercode_754.Text.Trim();
            SqlParameter p_txtReimbursingBankLocation_754 = new SqlParameter("@ReimbursingBank_Location", SqlDbType.VarChar);
            p_txtReimbursingBankLocation_754.Value = txtReimbursingBankLocation_754.Text.Trim();
            SqlParameter p_txtReimbursingBankName_754 = new SqlParameter("@ReimbursingBank_Name", SqlDbType.VarChar);
            p_txtReimbursingBankName_754.Value = txtReimbursingBankName_754.Text.Trim();
            SqlParameter p_txtReimbursingBankAddress1_754 = new SqlParameter("@ReimbursingBank_Add1", SqlDbType.VarChar);
            p_txtReimbursingBankAddress1_754.Value = txtReimbursingBankAddress1_754.Text.Trim();
            SqlParameter p_txtReimbursingBankAddress2_754 = new SqlParameter("@ReimbursingBank_Add2", SqlDbType.VarChar);
            p_txtReimbursingBankAddress2_754.Value = txtReimbursingBankAddress2_754.Text.Trim();
            SqlParameter p_txtReimbursingBankAddress3_754 = new SqlParameter("@ReimbursingBank_Add3", SqlDbType.VarChar);
            p_txtReimbursingBankAddress3_754.Value = txtReimbursingBankAddress3_754.Text.Trim();

            SqlParameter p_ddlAccountwithbank_754 = new SqlParameter("@ddlAccWithBank", SqlDbType.VarChar);
            p_ddlAccountwithbank_754.Value = ddlAccountwithbank_754.SelectedValue;
            SqlParameter p_txtAccountwithBankAccountnumber_754 = new SqlParameter("@AccWithBank_PartyIdent", SqlDbType.VarChar);
            p_txtAccountwithBankAccountnumber_754.Value = txtAccountwithBankAccountnumber_754.Text.Trim();
            SqlParameter p_txtAccountwithBankAccountnumber1_754 = new SqlParameter("@AccWithBank_PartyIdent1", SqlDbType.VarChar);
            p_txtAccountwithBankAccountnumber1_754.Value = txtAccountwithBankAccountnumber1_754.Text.Trim();
            SqlParameter p_txtAccountwithBankIdentifiercode_754 = new SqlParameter("@AccWithBank_Identcode", SqlDbType.VarChar);
            p_txtAccountwithBankIdentifiercode_754.Value = txtAccountwithBankIdentifiercode_754.Text.Trim();
            SqlParameter p_txtAccountwithBankLocation_754 = new SqlParameter("@AccWithBank_Location", SqlDbType.VarChar);
            p_txtAccountwithBankLocation_754.Value = txtAccountwithBankLocation_754.Text.Trim();
            SqlParameter p_txtAccountwithBankName_754 = new SqlParameter("@AccWithBank_Name", SqlDbType.VarChar);
            p_txtAccountwithBankName_754.Value = txtAccountwithBankName_754.Text.Trim();
            SqlParameter p_txtAccountwithBankAddress1_754 = new SqlParameter("@AccWithBank_Add1", SqlDbType.VarChar);
            p_txtAccountwithBankAddress1_754.Value = txtAccountwithBankAddress1_754.Text.Trim();
            SqlParameter p_txtAccountwithBankAddress2_754 = new SqlParameter("@AccWithBank_Add2", SqlDbType.VarChar);
            p_txtAccountwithBankAddress2_754.Value = txtAccountwithBankAddress2_754.Text.Trim();
            SqlParameter p_txtAccountwithBankAddress3_754 = new SqlParameter("@AccWithBank_Add3", SqlDbType.VarChar);
            p_txtAccountwithBankAddress3_754.Value = txtAccountwithBankAddress3_754.Text.Trim();


            SqlParameter p_ddlBeneficiarybank_754 = new SqlParameter("@ddlBeneficiaryBank", SqlDbType.VarChar);
            p_ddlBeneficiarybank_754.Value = ddlBeneficiarybank_754.SelectedValue;
            SqlParameter p_txtBeneficiaryBankAccountnumber_754 = new SqlParameter("@BeneficiaryBank_PartyIdent", SqlDbType.VarChar);
            p_txtBeneficiaryBankAccountnumber_754.Value = txtBeneficiaryBankAccountnumber_754.Text.Trim();
            SqlParameter p_txtBeneficiaryBankAccountnumber1_754 = new SqlParameter("@BeneficiaryBank_PartyIdent1", SqlDbType.VarChar);
            p_txtBeneficiaryBankAccountnumber1_754.Value = txtBeneficiaryBankAccountnumber1_754.Text.Trim();
            SqlParameter p_txtBeneficiaryBankIdentifiercode_754 = new SqlParameter("@BeneficiaryBank_Identcode", SqlDbType.VarChar);
            p_txtBeneficiaryBankIdentifiercode_754.Value = txtBeneficiaryBankIdentifiercode_754.Text.Trim();
            SqlParameter p_txtBeneficiaryBankName_754 = new SqlParameter("@BeneficiaryBank_Name", SqlDbType.VarChar);
            p_txtBeneficiaryBankName_754.Value = txtBeneficiaryBankName_754.Text.Trim();
            SqlParameter p_txtBeneficiaryBankAddress1_754 = new SqlParameter("@BeneficiaryBank_Add1", SqlDbType.VarChar);
            p_txtBeneficiaryBankAddress1_754.Value = txtBeneficiaryBankAddress1_754.Text.Trim();
            SqlParameter p_txtBeneficiaryBankAddress2_754 = new SqlParameter("@BeneficiaryBank_Add2", SqlDbType.VarChar);
            p_txtBeneficiaryBankAddress2_754.Value = txtBeneficiaryBankAddress2_754.Text.Trim();
            SqlParameter p_txtBeneficiaryBankAddress3_754 = new SqlParameter("@BeneficiaryBank_Add3", SqlDbType.VarChar);
            p_txtBeneficiaryBankAddress3_754.Value = txtBeneficiaryBankAddress3_754.Text.Trim();

            SqlParameter p_754_SenToRecinfo1 = new SqlParameter("@SenToRecinfo1", SqlDbType.VarChar);
            p_754_SenToRecinfo1.Value = txt_754_SenRecInfo1.Text.Trim();
            SqlParameter p_754_SenToRecinfo2 = new SqlParameter("@SenToRecinfo2", SqlDbType.VarChar);
            p_754_SenToRecinfo2.Value = txt_754_SenRecInfo2.Text.Trim();
            SqlParameter p_754_SenToRecinfo3 = new SqlParameter("@SenToRecinfo3", SqlDbType.VarChar);
            p_754_SenToRecinfo3.Value = txt_754_SenRecInfo3.Text.Trim();
            SqlParameter p_754_SenToRecinfo4 = new SqlParameter("@SenToRecinfo4", SqlDbType.VarChar);
            p_754_SenToRecinfo4.Value = txt_754_SenRecInfo4.Text.Trim();
            SqlParameter p_754_SenToRecinfo5 = new SqlParameter("@SenToRecinfo5", SqlDbType.VarChar);
            p_754_SenToRecinfo5.Value = txt_754_SenRecInfo5.Text.Trim();
            SqlParameter p_754_SenToRecinfo6 = new SqlParameter("@SenToRecinfo6", SqlDbType.VarChar);
            p_754_SenToRecinfo6.Value = txt_754_SenRecInfo6.Text.Trim();

            SqlParameter p_txt_754_Narr1 = new SqlParameter("@Narrative1", SqlDbType.VarChar);
            p_txt_754_Narr1.Value = txt_754_Narr1.Text.Trim();
            SqlParameter p_txt_754_Narr2 = new SqlParameter("@Narrative2", SqlDbType.VarChar);
            p_txt_754_Narr2.Value = txt_754_Narr2.Text.Trim();
            SqlParameter p_txt_754_Narr3 = new SqlParameter("@Narrative3", SqlDbType.VarChar);
            p_txt_754_Narr3.Value = txt_754_Narr3.Text.Trim();
            SqlParameter p_txt_754_Narr4 = new SqlParameter("@Narrative4", SqlDbType.VarChar);
            p_txt_754_Narr4.Value = txt_754_Narr4.Text.Trim();
            SqlParameter p_txt_754_Narr5 = new SqlParameter("@Narrative5", SqlDbType.VarChar);
            p_txt_754_Narr5.Value = txt_754_Narr5.Text.Trim();
            SqlParameter p_txt_754_Narr6 = new SqlParameter("@Narrative6", SqlDbType.VarChar);
            p_txt_754_Narr6.Value = txt_754_Narr6.Text.Trim();
            SqlParameter p_txt_754_Narr7 = new SqlParameter("@Narrative7", SqlDbType.VarChar);
            p_txt_754_Narr7.Value = txt_754_Narr7.Text.Trim();
            SqlParameter p_txt_754_Narr8 = new SqlParameter("@Narrative8", SqlDbType.VarChar);
            p_txt_754_Narr8.Value = txt_754_Narr8.Text.Trim();
            SqlParameter p_txt_754_Narr9 = new SqlParameter("@Narrative9", SqlDbType.VarChar);
            p_txt_754_Narr9.Value = txt_754_Narr9.Text.Trim();
            SqlParameter p_txt_754_Narr10 = new SqlParameter("@Narrative10", SqlDbType.VarChar);
            p_txt_754_Narr10.Value = txt_754_Narr10.Text.Trim();
            SqlParameter p_txt_754_Narr11 = new SqlParameter("@Narrative11", SqlDbType.VarChar);
            p_txt_754_Narr11.Value = txt_754_Narr11.Text.Trim();
            SqlParameter p_txt_754_Narr12 = new SqlParameter("@Narrative12", SqlDbType.VarChar);
            p_txt_754_Narr12.Value = txt_754_Narr12.Text.Trim();
            SqlParameter p_txt_754_Narr13 = new SqlParameter("@Narrative13", SqlDbType.VarChar);
            p_txt_754_Narr13.Value = txt_754_Narr13.Text.Trim();
            SqlParameter p_txt_754_Narr14 = new SqlParameter("@Narrative14", SqlDbType.VarChar);
            p_txt_754_Narr14.Value = txt_754_Narr14.Text.Trim();
            SqlParameter p_txt_754_Narr15 = new SqlParameter("@Narrative15", SqlDbType.VarChar);
            p_txt_754_Narr15.Value = txt_754_Narr15.Text.Trim();
            SqlParameter p_txt_754_Narr16 = new SqlParameter("@Narrative16", SqlDbType.VarChar);
            p_txt_754_Narr16.Value = txt_754_Narr16.Text.Trim();
            SqlParameter p_txt_754_Narr17 = new SqlParameter("@Narrative17", SqlDbType.VarChar);
            p_txt_754_Narr17.Value = txt_754_Narr17.Text.Trim();
            SqlParameter p_txt_754_Narr18 = new SqlParameter("@Narrative18", SqlDbType.VarChar);
            p_txt_754_Narr18.Value = txt_754_Narr18.Text.Trim();
            SqlParameter p_txt_754_Narr19 = new SqlParameter("@Narrative19", SqlDbType.VarChar);
            p_txt_754_Narr19.Value = txt_754_Narr19.Text.Trim();
            SqlParameter p_txt_754_Narr20 = new SqlParameter("@Narrative20", SqlDbType.VarChar);
            p_txt_754_Narr20.Value = txt_754_Narr20.Text.Trim();

            SqlParameter P_AddedBy = new SqlParameter("@AddedBy", hdnUserName);
            SqlParameter P_AddedDate = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            SqlParameter P_UpdatedBy = new SqlParameter("@UpdatedBy", hdnUserName);
            SqlParameter P_UpdatedDate = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

            TF_DATA objSave = new TF_DATA();
            _result = objSave.SaveDeleteData("TF_Exp_AddUpdateSwift754", p_Branch, p_SwiftType, p_txtReceiver754, p_754SenRefno, p_754RelRef, p_ddlPrinAmtPaidAccNegoAmt_754, p_txtPrinAmtPaidAccNegoDate_754,
            p_ddl_PrinAmtPaidAccNegoCurr_754, p_txtPrinAmtPaidAccNegoAmt_754, p_ddl_754_AddAmtClamd_Ccy, p_txt_754_AddAmtClamd_Amt, p_txt_754_ChargesDeduct1, p_txt_754_ChargesDeduct2,
            p_txt_754_ChargesDeduct3, p_txt_754_ChargesDeduct4, p_txt_754_ChargesDeduct5, p_txt_754_ChargesDeduct6, p_txt_754_ChargesAdded1, p_txt_754_ChargesAdded2, p_txt_754_ChargesAdded3,
            p_txt_754_ChargesAdded4, p_txt_754_ChargesAdded5, p_txt_754_ChargesAdded6, p_ddlTotalAmtclamd_754, p_txt_754_TotalAmtClmd_Date, p_ddl_754_TotalAmtClmd_Ccy, p_txt_754_TotalAmtClmd_Amt,
            p_ddlReimbursingbank_754, p_txtReimbursingBankAccountnumber_754, p_txtReimbursingBankAccountnumber1_754, p_txtReimbursingBankIdentifiercode_754, p_txtReimbursingBankLocation_754,
            p_txtReimbursingBankName_754, p_txtReimbursingBankAddress1_754, p_txtReimbursingBankAddress2_754, p_txtReimbursingBankAddress3_754, p_ddlAccountwithbank_754,
            p_txtAccountwithBankAccountnumber_754, p_txtAccountwithBankAccountnumber1_754, p_txtAccountwithBankIdentifiercode_754, p_txtAccountwithBankLocation_754, p_txtAccountwithBankName_754,
            p_txtAccountwithBankAddress1_754, p_txtAccountwithBankAddress2_754, p_txtAccountwithBankAddress3_754, p_ddlBeneficiarybank_754, p_txtBeneficiaryBankAccountnumber_754,
            p_txtBeneficiaryBankAccountnumber1_754, p_txtBeneficiaryBankIdentifiercode_754, p_txtBeneficiaryBankName_754, p_txtBeneficiaryBankAddress1_754, p_txtBeneficiaryBankAddress2_754,
            p_txtBeneficiaryBankAddress3_754, p_754_SenToRecinfo1, p_754_SenToRecinfo2, p_754_SenToRecinfo3, p_754_SenToRecinfo4, p_754_SenToRecinfo5, p_754_SenToRecinfo6, p_txt_754_Narr1,
            p_txt_754_Narr2, p_txt_754_Narr3, p_txt_754_Narr4, p_txt_754_Narr5, p_txt_754_Narr6, p_txt_754_Narr7, p_txt_754_Narr8, p_txt_754_Narr9, p_txt_754_Narr10, p_txt_754_Narr11,
            p_txt_754_Narr12, p_txt_754_Narr13, p_txt_754_Narr14, p_txt_754_Narr15, p_txt_754_Narr16, p_txt_754_Narr17, p_txt_754_Narr18, p_txt_754_Narr19, p_txt_754_Narr20,
            P_AddedBy, P_AddedDate, P_UpdatedBy, P_UpdatedDate);

            if (_result == "added")
            {
                audittrail("M", txt_754_SenRef.Text.Trim());
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added as Draft.');", true);
            }
            else if (_result == "Updated")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Error');", true);
            }
        }
    }
    public void SaveView742()
    {
        string hdnUserName = Session["userName"].ToString();
        if (txtReceiver742.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Receiver');", true);
        }

        else if (txt_742_ClaimBankRef.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Claiming Bank Reference');", true);
        }
        else if (txt_742_ClaimBankRef.Text.Contains("//"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Claiming bank Ref contains two consecutive slashes //');", true);
        }
        else if (txt_742_ClaimBankRef.Text.StartsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Claiming bank Ref start with slash(/)');", true);
        }
        else if (txt_742_ClaimBankRef.Text.EndsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Claiming bank Ref end with slash(/)');", true);
        }

        else if (txt_742_DocumCreditNo.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Documentary Credit Number');", true);
        }
        else if (txt_742_DocumCreditNo.Text.Contains("//"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Documentary credit No contains consecutive slashes //');", true);
        }
        else if (txt_742_DocumCreditNo.Text.StartsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Documentary credit No start with slash(/)');", true);
        }
        else if (txt_742_DocumCreditNo.Text.EndsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Documentary credit No end with slash(/)');", true);
        }

        else if (txtIssuingBankIdentifiercode_742.Text == "" && ddl_Issuingbank_742.SelectedValue == "A")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Issuing Bank identifier code');", true);
            txtIssuingBankIdentifiercode_742.Focus();
        }
        else if (txtIssuingBankName_742.Text == "" && ddl_Issuingbank_742.SelectedValue == "D")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Issuing Bank Name');", true);
            txtIssuingBankName_742.Focus();
        }

        else if (ddl_742_PrinAmtClmd_Ccy.SelectedItem.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Principle Amount Claimed Currency');", true);
            ddl_742_PrinAmtClmd_Ccy.Focus();
        }
        else if (txt_742_PrinAmtClmd_Amt.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Principle Amount Claimed Amount');", true);
            txt_742_PrinAmtClmd_Amt.Focus();
        }

        else if (ddl_742_PrinAmtClmd_Ccy.SelectedItem.Text == "JPY" && txt_742_PrinAmtClmd_Amt.Text.Contains("."))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('If currency is JPY then Decimal is not allowed in amount.');", true);
            txt_742_PrinAmtClmd_Amt.Focus();
        }
        else if (Regex.IsMatch(txt_742_PrinAmtClmd_Amt.Text, @"\.\d\d\d"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal in Principal Amount Claimed field');", true);
            txt_742_PrinAmtClmd_Amt.Focus();
        }
        else if (txt_742_PrinAmtClmd_Amt.Text.Contains(','))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please do not enter (,) in Principal amount Claimed field');", true);
            txt_742_PrinAmtClmd_Amt.Focus();
        }

        else if (ddl_742_AddAmtClamd_Ccy.SelectedItem.Text == "JPY" && txt_742_AddAmtClamd_Amt.Text.Contains("."))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('If currency is JPY then Decimal is not allowed in amount.');", true);
            txt_742_AddAmtClamd_Amt.Focus();
        }
        else if (Regex.IsMatch(txt_742_AddAmtClamd_Amt.Text, @"\.\d\d\d"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal in additional amount field');", true);
            txt_742_AddAmtClamd_Amt.Focus();
        }
        else if (txt_742_AddAmtClamd_Amt.Text.Contains(','))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please do not enter (,) in additional amount field');", true);
            txt_742_AddAmtClamd_Amt.Focus();
        }

        else if (txt_742_TotalAmtClmd_Date.Text == "" && ddlTotalAmtclamd_742.SelectedValue == "A")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Total Amount claimed Date should not be blank');", true);
            txt_742_TotalAmtClmd_Date.Focus();
        }
        else if (ddl_742_TotalAmtClmd_Ccy.SelectedItem.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Total Amount claimed Currency should not be blank');", true);
            ddl_742_TotalAmtClmd_Ccy.Focus();
        }
        else if (txt_742_TotalAmtClmd_Amt.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Total Amount claimed Amount should not be blank');", true);
            txt_742_TotalAmtClmd_Amt.Focus();
        }
        else if (ddl_742_TotalAmtClmd_Ccy.SelectedItem.Text == "JPY" && txt_742_TotalAmtClmd_Amt.Text.Contains("."))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('If currency is JPY then Decimal is not allowed in amount.');", true);
            txt_742_TotalAmtClmd_Amt.Focus();
        }
        else if (Regex.IsMatch(txt_742_TotalAmtClmd_Amt.Text, @"\.\d\d\d"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal in total amount claimed field');", true);
            txt_742_TotalAmtClmd_Amt.Focus();
        }

        else if (txt_742_TotalAmtClmd_Amt.Text.Contains(','))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please do not enter (,) in total amount claimed field');", true);
            txt_742_TotalAmtClmd_Amt.Focus();
        }

        else
        {
            SqlParameter p_Branch = new SqlParameter("@Branch", SqlDbType.VarChar);
            p_Branch.Value = ddlBranch.SelectedItem.Text;
            SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
            p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;
            SqlParameter p_txtReceiver742 = new SqlParameter("@Receiver", SqlDbType.VarChar);
            p_txtReceiver742.Value = txtReceiver742.Text.Trim();
            SqlParameter p_txt_742_ClaimBankRef = new SqlParameter("@ClaimBankRef", SqlDbType.VarChar);
            p_txt_742_ClaimBankRef.Value = txt_742_ClaimBankRef.Text.Trim();

            SqlParameter p_txt_742_DocumCreditNo = new SqlParameter("@DocumCreditNo", SqlDbType.VarChar);
            p_txt_742_DocumCreditNo.Value = txt_742_DocumCreditNo.Text.Trim();

            SqlParameter p_txt_742_Dateofissue = new SqlParameter("@Dateofissue", SqlDbType.VarChar);
            p_txt_742_Dateofissue.Value = txt_742_Dateofissue.Text.Trim();

            SqlParameter p_ddl_Issuingbank_742 = new SqlParameter("@ddlIssuingBank", SqlDbType.VarChar);
            p_ddl_Issuingbank_742.Value = ddl_Issuingbank_742.SelectedValue;

            SqlParameter p_txtIssuingBankAccountnumber_742 = new SqlParameter("@IssuingBank_PartyIdent", SqlDbType.VarChar);
            p_txtIssuingBankAccountnumber_742.Value = txtIssuingBankAccountnumber_742.Text.Trim();

            SqlParameter p_txtIssuingBankAccountnumber1_742 = new SqlParameter("@IssuingBank_PartyIdent1", SqlDbType.VarChar);
            p_txtIssuingBankAccountnumber1_742.Value = txtIssuingBankAccountnumber1_742.Text.Trim();

            SqlParameter p_txtIssuingBankIdentifiercode_742 = new SqlParameter("@IssuingBank_Identcode", SqlDbType.VarChar);
            p_txtIssuingBankIdentifiercode_742.Value = txtIssuingBankIdentifiercode_742.Text.Trim();

            SqlParameter p_txtIssuingBankName_742 = new SqlParameter("@IssuingBank_Name", SqlDbType.VarChar);
            p_txtIssuingBankName_742.Value = txtIssuingBankName_742.Text.Trim();
            SqlParameter p_txtIssuingBankAddress1_742 = new SqlParameter("@IssuingBank_Add1", SqlDbType.VarChar);
            p_txtIssuingBankAddress1_742.Value = txtIssuingBankAddress1_742.Text.Trim();
            SqlParameter p_txtIssuingBankAddress2_742 = new SqlParameter("@IssuingBank_Add2", SqlDbType.VarChar);
            p_txtIssuingBankAddress2_742.Value = txtIssuingBankAddress2_742.Text.Trim();
            SqlParameter p_txtIssuingBankAddress3_742 = new SqlParameter("@IssuingBank_Add3", SqlDbType.VarChar);
            p_txtIssuingBankAddress3_742.Value = txtIssuingBankAddress3_742.Text.Trim();
            SqlParameter p_ddl_742_PrinAmtClmd_Ccy = new SqlParameter("@PrincipalAmtClaimed_Curr", SqlDbType.VarChar);
            p_ddl_742_PrinAmtClmd_Ccy.Value = ddl_742_PrinAmtClmd_Ccy.SelectedItem.Text.Trim();

            SqlParameter p_txt_742_PrinAmtClmd_Amt = new SqlParameter("@PrincipalAmtClaimed_Amt", SqlDbType.VarChar);
            p_txt_742_PrinAmtClmd_Amt.Value = txt_742_PrinAmtClmd_Amt.Text.Trim();

            SqlParameter p_ddl_742_AddAmtClamd_Ccy = new SqlParameter("@AddnlAmt_Curr", SqlDbType.VarChar);
            p_ddl_742_AddAmtClamd_Ccy.Value = ddl_742_AddAmtClamd_Ccy.SelectedItem.Text.Trim();

            SqlParameter p_txt_742_AddAmtClamd_Amt = new SqlParameter("@AddnlAmt_Amt", SqlDbType.VarChar);
            p_txt_742_AddAmtClamd_Amt.Value = txt_742_AddAmtClamd_Amt.Text.Trim();

            SqlParameter p_txt_742_Charges1 = new SqlParameter("@Charges1", SqlDbType.VarChar);
            p_txt_742_Charges1.Value = txt_742_Charges1.Text.Trim();
            SqlParameter p_txt_742_Charges2 = new SqlParameter("@Charges2", SqlDbType.VarChar);
            p_txt_742_Charges2.Value = txt_742_Charges2.Text.Trim();
            SqlParameter p_txt_742_Charges3 = new SqlParameter("@Charges3", SqlDbType.VarChar);
            p_txt_742_Charges3.Value = txt_742_Charges3.Text.Trim();
            SqlParameter p_txt_742_Charges4 = new SqlParameter("@Charges4", SqlDbType.VarChar);
            p_txt_742_Charges4.Value = txt_742_Charges4.Text.Trim();
            SqlParameter p_txt_742_Charges5 = new SqlParameter("@Charges5", SqlDbType.VarChar);
            p_txt_742_Charges5.Value = txt_742_Charges5.Text.Trim();
            SqlParameter p_txt_742_Charges6 = new SqlParameter("@Charges6", SqlDbType.VarChar);
            p_txt_742_Charges6.Value = txt_742_Charges6.Text.Trim();

            SqlParameter p_ddlTotalAmtclamd_742 = new SqlParameter("@ddlTotalAmtClaimed", SqlDbType.VarChar);
            p_ddlTotalAmtclamd_742.Value = ddlTotalAmtclamd_742.SelectedValue;

            SqlParameter p_txt_742_TotalAmtClmd_Date = new SqlParameter("@TotalAmtClaimed_Date", SqlDbType.VarChar);
            p_txt_742_TotalAmtClmd_Date.Value = txt_742_TotalAmtClmd_Date.Text.Trim();

            SqlParameter p_ddl_742_TotalAmtClmd_Ccy = new SqlParameter("@TotalAmtClaimed_Curr", SqlDbType.VarChar);
            p_ddl_742_TotalAmtClmd_Ccy.Value = ddl_742_TotalAmtClmd_Ccy.SelectedItem.Text.Trim();

            SqlParameter p_txt_742_TotalAmtClmd_Amt = new SqlParameter("@TotalAmtClaimed_Amt", SqlDbType.VarChar);
            p_txt_742_TotalAmtClmd_Amt.Value = txt_742_TotalAmtClmd_Amt.Text.Trim();

            SqlParameter p_ddlAccountwithbank_742 = new SqlParameter("@ddlAccWithBank", SqlDbType.VarChar);
            p_ddlAccountwithbank_742.Value = ddlAccountwithbank_742.SelectedValue;
            SqlParameter p_txtAccountwithBankAccountnumber_742 = new SqlParameter("@AccWithBank_PartyIdent", SqlDbType.VarChar);
            p_txtAccountwithBankAccountnumber_742.Value = txtAccountwithBankAccountnumber_742.Text.Trim();
            SqlParameter p_txtAccountwithBankAccountnumber1_742 = new SqlParameter("@AccWithBank_PartyIdent1", SqlDbType.VarChar);
            p_txtAccountwithBankAccountnumber1_742.Value = txtAccountwithBankAccountnumber1_742.Text.Trim();
            SqlParameter p_txtAccountwithBankIdentifiercode_742 = new SqlParameter("@AccWithBank_Identcode", SqlDbType.VarChar);
            p_txtAccountwithBankIdentifiercode_742.Value = txtAccountwithBankIdentifiercode_742.Text.Trim();
            SqlParameter p_txtAccountwithBankLocation_742 = new SqlParameter("@AccWithBank_Location", SqlDbType.VarChar);
            p_txtAccountwithBankLocation_742.Value = txtAccountwithBankLocation_742.Text.Trim();
            SqlParameter p_txtAccountwithBankName_742 = new SqlParameter("@AccWithBank_Name", SqlDbType.VarChar);
            p_txtAccountwithBankName_742.Value = txtAccountwithBankName_742.Text.Trim();
            SqlParameter p_txtAccountwithBankAddress1_742 = new SqlParameter("@AccWithBank_Add1", SqlDbType.VarChar);
            p_txtAccountwithBankAddress1_742.Value = txtAccountwithBankAddress1_742.Text.Trim();
            SqlParameter p_txtAccountwithBankAddress2_742 = new SqlParameter("@AccWithBank_Add2", SqlDbType.VarChar);
            p_txtAccountwithBankAddress2_742.Value = txtAccountwithBankAddress2_742.Text.Trim();
            SqlParameter p_txtAccountwithBankAddress3_742 = new SqlParameter("@AccWithBank_Add3", SqlDbType.VarChar);
            p_txtAccountwithBankAddress3_742.Value = txtAccountwithBankAddress3_742.Text.Trim();

            SqlParameter p_ddlBeneficiarybank_742 = new SqlParameter("@ddlBeneficiaryBank", SqlDbType.VarChar);
            p_ddlBeneficiarybank_742.Value = ddlBeneficiarybank_742.SelectedValue;
            SqlParameter p_txtBeneficiaryBankAccountnumber_742 = new SqlParameter("@BeneficiaryBank_PartyIdent", SqlDbType.VarChar);
            p_txtBeneficiaryBankAccountnumber_742.Value = txtBeneficiaryBankAccountnumber_742.Text.Trim();
            SqlParameter p_txtBeneficiaryBankAccountnumber1_742 = new SqlParameter("@BeneficiaryBank_PartyIdent1", SqlDbType.VarChar);
            p_txtBeneficiaryBankAccountnumber1_742.Value = txtBeneficiaryBankAccountnumber1_742.Text.Trim();
            SqlParameter p_txtBeneficiaryBankIdentifiercode_742 = new SqlParameter("@BeneficiaryBank_Identcode", SqlDbType.VarChar);
            p_txtBeneficiaryBankIdentifiercode_742.Value = txtBeneficiaryBankIdentifiercode_742.Text.Trim();
            SqlParameter p_txtBeneficiaryBankName_742 = new SqlParameter("@BeneficiaryBank_Name", SqlDbType.VarChar);
            p_txtBeneficiaryBankName_742.Value = txtBeneficiaryBankName_742.Text.Trim();
            SqlParameter p_txtBeneficiaryBankAddress1_742 = new SqlParameter("@BeneficiaryBank_Add1", SqlDbType.VarChar);
            p_txtBeneficiaryBankAddress1_742.Value = txtBeneficiaryBankAddress1_742.Text.Trim();
            SqlParameter p_txtBeneficiaryBankAddress2_742 = new SqlParameter("@BeneficiaryBank_Add2", SqlDbType.VarChar);
            p_txtBeneficiaryBankAddress2_742.Value = txtBeneficiaryBankAddress2_742.Text.Trim();
            SqlParameter p_txtBeneficiaryBankAddress3_742 = new SqlParameter("@BeneficiaryBank_Add3", SqlDbType.VarChar);
            p_txtBeneficiaryBankAddress3_742.Value = txtBeneficiaryBankAddress3_742.Text.Trim();

            SqlParameter p_742_SenToRecinfo1 = new SqlParameter("@SenToRecinfo1", SqlDbType.VarChar);
            p_742_SenToRecinfo1.Value = txt_742_SenRecInfo1.Text.Trim();
            SqlParameter p_742_SenToRecinfo2 = new SqlParameter("@SenToRecinfo2", SqlDbType.VarChar);
            p_742_SenToRecinfo2.Value = txt_742_SenRecInfo2.Text.Trim();
            SqlParameter p_742_SenToRecinfo3 = new SqlParameter("@SenToRecinfo3", SqlDbType.VarChar);
            p_742_SenToRecinfo3.Value = txt_742_SenRecInfo3.Text.Trim();
            SqlParameter p_742_SenToRecinfo4 = new SqlParameter("@SenToRecinfo4", SqlDbType.VarChar);
            p_742_SenToRecinfo4.Value = txt_742_SenRecInfo4.Text.Trim();
            SqlParameter p_742_SenToRecinfo5 = new SqlParameter("@SenToRecinfo5", SqlDbType.VarChar);
            p_742_SenToRecinfo5.Value = txt_742_SenRecInfo5.Text.Trim();
            SqlParameter p_742_SenToRecinfo6 = new SqlParameter("@SenToRecinfo6", SqlDbType.VarChar);
            p_742_SenToRecinfo6.Value = txt_742_SenRecInfo6.Text.Trim();

            SqlParameter P_AddedBy = new SqlParameter("@AddedBy", hdnUserName);
            SqlParameter P_AddedDate = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            SqlParameter P_UpdatedBy = new SqlParameter("@UpdatedBy", hdnUserName);
            SqlParameter P_UpdatedDate = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

            TF_DATA objSave = new TF_DATA();
            _result = objSave.SaveDeleteData("TF_Exp_AddUpdateSwift742", p_Branch, p_SwiftType, p_txtReceiver742, p_txt_742_ClaimBankRef, p_txt_742_DocumCreditNo, p_txt_742_Dateofissue, p_ddl_Issuingbank_742,
                p_txtIssuingBankAccountnumber_742, p_txtIssuingBankAccountnumber1_742, p_txtIssuingBankIdentifiercode_742, p_txtIssuingBankName_742, p_txtIssuingBankAddress1_742,
                p_txtIssuingBankAddress2_742, p_txtIssuingBankAddress3_742, p_ddl_742_PrinAmtClmd_Ccy, p_txt_742_PrinAmtClmd_Amt, p_ddl_742_AddAmtClamd_Ccy, p_txt_742_AddAmtClamd_Amt,
                p_txt_742_Charges1, p_txt_742_Charges2, p_txt_742_Charges3, p_txt_742_Charges4, p_txt_742_Charges5, p_txt_742_Charges6, p_ddlTotalAmtclamd_742, p_txt_742_TotalAmtClmd_Date,
                p_ddl_742_TotalAmtClmd_Ccy, p_txt_742_TotalAmtClmd_Amt, p_ddlAccountwithbank_742, p_txtAccountwithBankAccountnumber_742, p_txtAccountwithBankAccountnumber1_742,
                p_txtAccountwithBankIdentifiercode_742, p_txtAccountwithBankLocation_742, p_txtAccountwithBankName_742, p_txtAccountwithBankAddress1_742, p_txtAccountwithBankAddress2_742,
                p_txtAccountwithBankAddress3_742, p_ddlBeneficiarybank_742, p_txtBeneficiaryBankAccountnumber_742, p_txtBeneficiaryBankAccountnumber1_742, p_txtBeneficiaryBankIdentifiercode_742,
                p_txtBeneficiaryBankName_742, p_txtBeneficiaryBankAddress1_742, p_txtBeneficiaryBankAddress2_742, p_txtBeneficiaryBankAddress3_742, p_742_SenToRecinfo1,
                p_742_SenToRecinfo2, p_742_SenToRecinfo3, p_742_SenToRecinfo4, p_742_SenToRecinfo5, p_742_SenToRecinfo6, P_AddedBy, P_AddedDate, P_UpdatedBy, P_UpdatedDate);

            if (_result == "added")
            {
                audittrail("M", txt_742_ClaimBankRef.Text.Trim());
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added as Draft .');", true);
            }
            else if (_result == "Updated")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Error while saving the Record');", true);
            }
        }
    }
    public void SaveView420()
    {
        string hdnUserName = Session["userName"].ToString();
        if (txtReceiver420.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Receiver');", true);
            txtReceiver420.Focus();
        }
        else if (txt_420_SendingBankTRN.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Sending Bank TRN');", true);
        }
        else if (txt_420_SendingBankTRN.Text.Contains("//"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Sending Bank TRN contains two consecutive slashes //');", true);
        }
        else if (txt_420_SendingBankTRN.Text.StartsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Sending Bank TRN start with slash(/)');", true);
        }
        else if (txt_420_SendingBankTRN.Text.EndsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Sending Bank TRN end with slash(/)');", true);
        }

        else if (txt_420_RelRef.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Related Reference');", true);
        }
        else if (txt_420_RelRef.Text.Contains("//"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference contains two consecutive slashes //');", true);
        }
        else if (txt_420_RelRef.Text.StartsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference start with slash(/)');", true);
        }
        else if (txt_420_RelRef.Text.EndsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference end with slash(/)');", true);
        }

        else if (Regex.IsMatch(txtAmountTracedAmount_420.Text, @"\.\d\d\d"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter maximum two values after decimal');", true);
            txtAmountTracedAmount_420.Focus();
        }
        else if (txtAmountTracedAmount_420.Text.Contains(","))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please do not enter (,) in amount field');", true);
            txtAmountTracedAmount_420.Focus();
        }

        else if (ddl_AmountTracedCurrency_420.SelectedItem.Text == "JPY" && txtAmountTracedAmount_420.Text.Contains("."))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('If currency is JPY then Decimal is not allowed in amount field.');", true);
            ddl_AmountTracedCurrency_420.Focus();
        }
        else if (ddl_AmountTracedCurrency_420.SelectedItem.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Amount traced currency');", true);
            ddl_AmountTracedCurrency_420.Focus();
        }
        else if (txtAmountTracedAmount_420.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Amount traced amount');", true);
            txtAmountTracedAmount_420.Focus();
        }

        else if (ddlAmountTraced_420.SelectedValue == "A" && txtAmountTracedDate_420.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Amount traced date');", true);
            txtAmountTracedDate_420.Focus();
        }
        else if (ddlAmountTraced_420.SelectedValue == "K" && ddlAmountTracedDayMonth_420.SelectedValue == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Amount traced day month');", true);
            ddlAmountTracedDayMonth_420.Focus();
        }
        else if (ddlAmountTraced_420.SelectedValue == "K" && txtAmountTracedNoofDaysMonth_420.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Amount traced no of days month');", true);
            txtAmountTracedNoofDaysMonth_420.Focus();
        }

        else if (ddlAmountTraced_420.SelectedValue == "K" && ddlAmountTracedCode_420.SelectedValue == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Amount traced code');", true);
            ddlAmountTracedCode_420.Focus();
        }

        else
        {
            SqlParameter p_Branch = new SqlParameter("@Branch", SqlDbType.VarChar);
            p_Branch.Value = ddlBranch.SelectedItem.Text;
            SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
            p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;
            SqlParameter p_txtReceiver420 = new SqlParameter("@Receiver", SqlDbType.VarChar);
            p_txtReceiver420.Value = txtReceiver420.Text.Trim();
            SqlParameter p_420TranRefno = new SqlParameter("@Trans_Ref_No", SqlDbType.VarChar);
            p_420TranRefno.Value = txt_420_SendingBankTRN.Text.Trim();
            SqlParameter p_420RelatedRef = new SqlParameter("@Related_Ref", SqlDbType.VarChar);
            p_420RelatedRef.Value = txt_420_RelRef.Text.Trim();
            SqlParameter p_420_DdlAmtTraced = new SqlParameter("@ddlAmtTraced", SqlDbType.VarChar);
            p_420_DdlAmtTraced.Value = ddlAmountTraced_420.SelectedValue;
            SqlParameter p_420_TracedAmt = new SqlParameter("@TracedAmt", SqlDbType.VarChar);
            p_420_TracedAmt.Value = txtAmountTracedAmount_420.Text.Trim();
            SqlParameter p_420_Tracedcode = new SqlParameter("@Tracedcode", SqlDbType.VarChar);
            p_420_Tracedcode.Value = ddlAmountTracedCode_420.SelectedValue;
            SqlParameter p_420_Tracedcurr = new SqlParameter("@Tracedcurr", SqlDbType.VarChar);
            p_420_Tracedcurr.Value = ddl_AmountTracedCurrency_420.SelectedItem.Text.Trim();
            SqlParameter p_420_TracedDate = new SqlParameter("@TracedDate", SqlDbType.VarChar);
            p_420_TracedDate.Value = txtAmountTracedDate_420.Text.Trim();
            SqlParameter p_420_TracedDayMonth = new SqlParameter("@TracedDayMonth", SqlDbType.VarChar);
            p_420_TracedDayMonth.Value = ddlAmountTracedDayMonth_420.SelectedValue;
            SqlParameter p_420_TracedNoofDayMonth = new SqlParameter("@TracedNoofDayMonth", SqlDbType.VarChar);
            p_420_TracedNoofDayMonth.Value = txtAmountTracedNoofDaysMonth_420.Text.Trim();
            SqlParameter p_420_DateofCollnInstruction = new SqlParameter("@DateofCollnInstruction", SqlDbType.VarChar);
            p_420_DateofCollnInstruction.Value = txt_420_DateofCollnInstruction.Text.Trim();
            SqlParameter p_420_DraweeAccount = new SqlParameter("@DraweeAccount", SqlDbType.VarChar);
            p_420_DraweeAccount.Value = txt_420_DraweeAccount.Text.Trim();
            SqlParameter p_420_DraweeName = new SqlParameter("@DraweeName", SqlDbType.VarChar);
            p_420_DraweeName.Value = txt_420_DraweeName.Text.Trim();
            SqlParameter p_420_DraweeAdd1 = new SqlParameter("@DraweeAdd1", SqlDbType.VarChar);
            p_420_DraweeAdd1.Value = txt_420_DraweeAdd1.Text.Trim();
            SqlParameter p_420_DraweeAdd2 = new SqlParameter("@DraweeAdd2", SqlDbType.VarChar);
            p_420_DraweeAdd2.Value = txt_420_DraweeAdd2.Text.Trim();
            SqlParameter p_420_DraweeAdd3 = new SqlParameter("@DraweeAdd3", SqlDbType.VarChar);
            p_420_DraweeAdd3.Value = txt_420_DraweeAdd3.Text.Trim();
            SqlParameter p_420_SenToRecinfo1 = new SqlParameter("@SenToRecinfo1", SqlDbType.VarChar);
            p_420_SenToRecinfo1.Value = txt_420_SenToRecinfo1.Text.Trim();
            SqlParameter p_420_SenToRecinfo2 = new SqlParameter("@SenToRecinfo2", SqlDbType.VarChar);
            p_420_SenToRecinfo2.Value = txt_420_SenToRecinfo2.Text.Trim();
            SqlParameter p_420_SenToRecinfo3 = new SqlParameter("@SenToRecinfo3", SqlDbType.VarChar);
            p_420_SenToRecinfo3.Value = txt_420_SenToRecinfo3.Text.Trim();
            SqlParameter p_420_SenToRecinfo4 = new SqlParameter("@SenToRecinfo4", SqlDbType.VarChar);
            p_420_SenToRecinfo4.Value = txt_420_SenToRecinfo4.Text.Trim();
            SqlParameter p_420_SenToRecinfo5 = new SqlParameter("@SenToRecinfo5", SqlDbType.VarChar);
            p_420_SenToRecinfo5.Value = txt_420_SenToRecinfo5.Text.Trim();
            SqlParameter p_420_SenToRecinfo6 = new SqlParameter("@SenToRecinfo6", SqlDbType.VarChar);
            p_420_SenToRecinfo6.Value = txt_420_SenToRecinfo6.Text.Trim();
            SqlParameter P_AddedBy = new SqlParameter("@AddedBy", hdnUserName);
            SqlParameter P_AddedDate = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            SqlParameter P_UpdatedBy = new SqlParameter("@UpdatedBy", hdnUserName);
            SqlParameter P_UpdatedDate = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

            TF_DATA objSave = new TF_DATA();
            _result = objSave.SaveDeleteData("TF_Exp_AddUpdateSwift420", p_Branch, p_SwiftType, p_txtReceiver420, p_420TranRefno, p_420RelatedRef, p_420_DdlAmtTraced, p_420_TracedAmt, p_420_Tracedcode,
              p_420_Tracedcurr, p_420_TracedDate, p_420_TracedDayMonth, p_420_TracedNoofDayMonth, p_420_DateofCollnInstruction, p_420_DraweeAccount, p_420_DraweeName, p_420_DraweeAdd1,
              p_420_DraweeAdd2, p_420_DraweeAdd3, p_420_SenToRecinfo1, p_420_SenToRecinfo2, p_420_SenToRecinfo3, p_420_SenToRecinfo4, p_420_SenToRecinfo5, p_420_SenToRecinfo6,
              P_AddedBy, P_AddedDate, P_UpdatedBy, P_UpdatedDate);

            if (_result == "added")
            {
                audittrail("M", txt_420_SendingBankTRN.Text.Trim());
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added as Draft.');", true);
            }
            else if (_result == "Updated")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Error while saving the Record');", true);
            }
        }
    }
    public void SaveView499()
    {
        string hdnUserName = Session["userName"].ToString();
        if (txtReceiver499.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Receiver');", true);
            txtReceiver499.Focus();
        }
        else if (txt_499_transRefNo.Text.Trim() == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter transaction reference number');", true);
            txt_499_transRefNo.Focus();
        }
        else if (txt_499_transRefNo.Text.Contains("//"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number contains two consecutive slashes //');", true);
            txt_499_transRefNo.Focus();
        }
        else if (txt_499_transRefNo.Text.StartsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number start with slash(/)');", true);
            txt_499_transRefNo.Focus();
        }
        else if (txt_499_transRefNo.Text.EndsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number end with slash(/)');", true);
            txt_499_transRefNo.Focus();
        }

        else if (txt_499_RelRef.Text.Contains("//"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference contains two consecutive slashes //');", true);
            txt_499_RelRef.Focus();
        }
        else if (txt_499_RelRef.Text.StartsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference start with slash(/)');", true);
            txt_499_RelRef.Focus();
        }
        else if (txt_499_RelRef.Text.EndsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Relateds Reference end with slash(/)');", true);
            txt_499_RelRef.Focus();
        }

        else if (txt_499_Narr1.Text == "" && txt_499_Narr2.Text == "" && txt_499_Narr3.Text == "" && txt_499_Narr4.Text == "" && txt_499_Narr5.Text == "" && txt_499_Narr6.Text == "" && txt_499_Narr7.Text == "" && txt_499_Narr8.Text == "" && txt_499_Narr9.Text == "" && txt_499_Narr10.Text == ""
        && txt_499_Narr11.Text == "" && txt_499_Narr12.Text == "" && txt_499_Narr13.Text == "" && txt_499_Narr14.Text == "" && txt_499_Narr15.Text == "" && txt_499_Narr16.Text == "" && txt_499_Narr17.Text == "" && txt_499_Narr18.Text == "" && txt_499_Narr19.Text == "" && txt_499_Narr20.Text == ""
        && txt_499_Narr21.Text == "" && txt_499_Narr22.Text == "" && txt_499_Narr23.Text == "" && txt_499_Narr24.Text == "" && txt_499_Narr25.Text == "" && txt_499_Narr26.Text == "" && txt_499_Narr27.Text == "" && txt_499_Narr28.Text == "" && txt_499_Narr29.Text == "" && txt_499_Narr30.Text == ""
        && txt_499_Narr31.Text == "" && txt_499_Narr32.Text == "" && txt_499_Narr33.Text == "" && txt_499_Narr34.Text == "" && txt_499_Narr35.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Narrative');", true);
            txt_499_Narr1.Focus();
        }

        else
        {
            SqlParameter p_Branch = new SqlParameter("@Branch", SqlDbType.VarChar);
            p_Branch.Value = ddlBranch.SelectedItem.Text;
            SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
            p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;
            SqlParameter p_txtReceiver499 = new SqlParameter("@Receiver", SqlDbType.VarChar);
            p_txtReceiver499.Value = txtReceiver499.Text.Trim();
            SqlParameter p_499TranRefno = new SqlParameter("@Trans_Ref_No", SqlDbType.VarChar);
            p_499TranRefno.Value = txt_499_transRefNo.Text.Trim();
            SqlParameter p_499RelatedRef = new SqlParameter("@Related_Ref", SqlDbType.VarChar);
            p_499RelatedRef.Value = txt_499_RelRef.Text.Trim();
            SqlParameter p_499Narr1 = new SqlParameter("@Narrative1", SqlDbType.VarChar);
            p_499Narr1.Value = txt_499_Narr1.Text.Trim();
            SqlParameter p_499Narr2 = new SqlParameter("@Narrative2", SqlDbType.VarChar);
            p_499Narr2.Value = txt_499_Narr2.Text.Trim();
            SqlParameter p_499Narr3 = new SqlParameter("@Narrative3", SqlDbType.VarChar);
            p_499Narr3.Value = txt_499_Narr3.Text.Trim();
            SqlParameter p_499Narr4 = new SqlParameter("@Narrative4", SqlDbType.VarChar);
            p_499Narr4.Value = txt_499_Narr4.Text.Trim();
            SqlParameter p_499Narr5 = new SqlParameter("@Narrative5", SqlDbType.VarChar);
            p_499Narr5.Value = txt_499_Narr5.Text.Trim();
            SqlParameter p_499Narr6 = new SqlParameter("@Narrative6", SqlDbType.VarChar);
            p_499Narr6.Value = txt_499_Narr6.Text.Trim();
            SqlParameter p_499Narr7 = new SqlParameter("@Narrative7", SqlDbType.VarChar);
            p_499Narr7.Value = txt_499_Narr7.Text.Trim();
            SqlParameter p_499Narr8 = new SqlParameter("@Narrative8", SqlDbType.VarChar);
            p_499Narr8.Value = txt_499_Narr8.Text.Trim();
            SqlParameter p_499Narr9 = new SqlParameter("@Narrative9", SqlDbType.VarChar);
            p_499Narr9.Value = txt_499_Narr9.Text.Trim();
            SqlParameter p_499Narr10 = new SqlParameter("@Narrative10", SqlDbType.VarChar);
            p_499Narr10.Value = txt_499_Narr10.Text.Trim();
            SqlParameter p_499Narr11 = new SqlParameter("@Narrative11", SqlDbType.VarChar);
            p_499Narr11.Value = txt_499_Narr11.Text.Trim();
            SqlParameter p_499Narr12 = new SqlParameter("@Narrative12", SqlDbType.VarChar);
            p_499Narr12.Value = txt_499_Narr12.Text.Trim();
            SqlParameter p_499Narr13 = new SqlParameter("@Narrative13", SqlDbType.VarChar);
            p_499Narr13.Value = txt_499_Narr13.Text.Trim();
            SqlParameter p_499Narr14 = new SqlParameter("@Narrative14", SqlDbType.VarChar);
            p_499Narr14.Value = txt_499_Narr14.Text.Trim();
            SqlParameter p_499Narr15 = new SqlParameter("@Narrative15", SqlDbType.VarChar);
            p_499Narr15.Value = txt_499_Narr15.Text.Trim();
            SqlParameter p_499Narr16 = new SqlParameter("@Narrative16", SqlDbType.VarChar);
            p_499Narr16.Value = txt_499_Narr16.Text.Trim();
            SqlParameter p_499Narr17 = new SqlParameter("@Narrative17", SqlDbType.VarChar);
            p_499Narr17.Value = txt_499_Narr17.Text.Trim();
            SqlParameter p_499Narr18 = new SqlParameter("@Narrative18", SqlDbType.VarChar);
            p_499Narr18.Value = txt_499_Narr18.Text.Trim();
            SqlParameter p_499Narr19 = new SqlParameter("@Narrative19", SqlDbType.VarChar);
            p_499Narr19.Value = txt_499_Narr19.Text.Trim();
            SqlParameter p_499Narr20 = new SqlParameter("@Narrative20", SqlDbType.VarChar);
            p_499Narr20.Value = txt_499_Narr20.Text.Trim();
            SqlParameter p_499Narr21 = new SqlParameter("@Narrative21", SqlDbType.VarChar);
            p_499Narr21.Value = txt_499_Narr21.Text.Trim();
            SqlParameter p_499Narr22 = new SqlParameter("@Narrative22", SqlDbType.VarChar);
            p_499Narr22.Value = txt_499_Narr22.Text.Trim();
            SqlParameter p_499Narr23 = new SqlParameter("@Narrative23", SqlDbType.VarChar);
            p_499Narr23.Value = txt_499_Narr23.Text.Trim();
            SqlParameter p_499Narr24 = new SqlParameter("@Narrative24", SqlDbType.VarChar);
            p_499Narr24.Value = txt_499_Narr24.Text.Trim();
            SqlParameter p_499Narr25 = new SqlParameter("@Narrative25", SqlDbType.VarChar);
            p_499Narr25.Value = txt_499_Narr25.Text.Trim();
            SqlParameter p_499Narr26 = new SqlParameter("@Narrative26", SqlDbType.VarChar);
            p_499Narr26.Value = txt_499_Narr26.Text.Trim();
            SqlParameter p_499Narr27 = new SqlParameter("@Narrative27", SqlDbType.VarChar);
            p_499Narr27.Value = txt_499_Narr27.Text.Trim();
            SqlParameter p_499Narr28 = new SqlParameter("@Narrative28", SqlDbType.VarChar);
            p_499Narr28.Value = txt_499_Narr28.Text.Trim();
            SqlParameter p_499Narr29 = new SqlParameter("@Narrative29", SqlDbType.VarChar);
            p_499Narr29.Value = txt_499_Narr29.Text.Trim();
            SqlParameter p_499Narr30 = new SqlParameter("@Narrative30", SqlDbType.VarChar);
            p_499Narr30.Value = txt_499_Narr30.Text.Trim();
            SqlParameter p_499Narr31 = new SqlParameter("@Narrative31", SqlDbType.VarChar);
            p_499Narr31.Value = txt_499_Narr31.Text.Trim();
            SqlParameter p_499Narr32 = new SqlParameter("@Narrative32", SqlDbType.VarChar);
            p_499Narr32.Value = txt_499_Narr32.Text.Trim();
            SqlParameter p_499Narr33 = new SqlParameter("@Narrative33", SqlDbType.VarChar);
            p_499Narr33.Value = txt_499_Narr33.Text.Trim();
            SqlParameter p_499Narr34 = new SqlParameter("@Narrative34", SqlDbType.VarChar);
            p_499Narr34.Value = txt_499_Narr34.Text.Trim();
            SqlParameter p_499Narr35 = new SqlParameter("@Narrative35", SqlDbType.VarChar);
            p_499Narr35.Value = txt_499_Narr35.Text.Trim();
            SqlParameter P_AddedBy = new SqlParameter("@AddedBy", hdnUserName);
            SqlParameter P_AddedDate = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            SqlParameter P_UpdatedBy = new SqlParameter("@UpdatedBy", hdnUserName);
            SqlParameter P_UpdatedDate = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

            TF_DATA objSave = new TF_DATA();
            _result = objSave.SaveDeleteData("TF_Exp_AddUpdateSwift499", p_Branch, p_SwiftType, p_txtReceiver499, p_499TranRefno, p_499RelatedRef, p_499Narr1, p_499Narr2, p_499Narr3, p_499Narr4, p_499Narr5,
              p_499Narr6, p_499Narr7, p_499Narr8, p_499Narr9, p_499Narr10, p_499Narr11, p_499Narr12, p_499Narr13, p_499Narr14, p_499Narr15, p_499Narr16, p_499Narr17, p_499Narr18,
              p_499Narr19, p_499Narr20, p_499Narr21, p_499Narr22, p_499Narr23, p_499Narr24, p_499Narr25, p_499Narr26, p_499Narr27, p_499Narr28, p_499Narr29, p_499Narr30,
              p_499Narr31, p_499Narr32, p_499Narr33, p_499Narr34, p_499Narr35, P_AddedBy, P_AddedDate, P_UpdatedBy, P_UpdatedDate);
            if (_result == "added")
            {
                audittrail("M", txt_499_transRefNo.Text.Trim());
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added as Draft.');", true);
            }
            else if (_result == "Updated")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Error while saving the Record');", true);
            }
        }
    }
    public void SaveView199()
    {
        string hdnUserName = Session["userName"].ToString();
        if (txtReceiver199.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Receiver');", true);
            txtReceiver199.Focus();
        }
        else if (txt_199_transRefNo.Text.Trim() == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter transaction reference number');", true);
            txt_199_transRefNo.Focus();
        }
        else if (txt_199_transRefNo.Text.Contains("//"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number contains two consecutive slashes //');", true);
            txt_199_transRefNo.Focus();
        }
        else if (txt_199_transRefNo.Text.StartsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number start with slash(/)');", true);
            txt_199_transRefNo.Focus();
        }
        else if (txt_199_transRefNo.Text.EndsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number end with slash(/)');", true);
            txt_199_transRefNo.Focus();
        }

        else if (txt_199_RelRef.Text.Contains("//"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference contains two consecutive slashes //');", true);
            txt_199_RelRef.Focus();
        }
        else if (txt_199_RelRef.Text.StartsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference start with slash(/)');", true);
            txt_199_RelRef.Focus();
        }
        else if (txt_199_RelRef.Text.EndsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Relateds Reference end with slash(/)');", true);
            txt_199_RelRef.Focus();
        }

        else if (txt_199_Narr1.Text == "" && txt_199_Narr2.Text == "" && txt_199_Narr3.Text == "" && txt_199_Narr4.Text == "" && txt_199_Narr5.Text == "" && txt_199_Narr6.Text == "" && txt_199_Narr7.Text == "" && txt_199_Narr8.Text == "" && txt_199_Narr9.Text == "" && txt_199_Narr10.Text == ""
        && txt_199_Narr11.Text == "" && txt_199_Narr12.Text == "" && txt_199_Narr13.Text == "" && txt_199_Narr14.Text == "" && txt_199_Narr15.Text == "" && txt_199_Narr16.Text == "" && txt_199_Narr17.Text == "" && txt_199_Narr18.Text == "" && txt_199_Narr19.Text == "" && txt_199_Narr20.Text == ""
        && txt_199_Narr21.Text == "" && txt_199_Narr22.Text == "" && txt_199_Narr23.Text == "" && txt_199_Narr24.Text == "" && txt_199_Narr25.Text == "" && txt_199_Narr26.Text == "" && txt_199_Narr27.Text == "" && txt_199_Narr28.Text == "" && txt_199_Narr29.Text == "" && txt_199_Narr30.Text == ""
        && txt_199_Narr31.Text == "" && txt_199_Narr32.Text == "" && txt_199_Narr33.Text == "" && txt_199_Narr34.Text == "" && txt_199_Narr35.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Narrative');", true);
            txt_199_Narr1.Focus();
        }

        else
        {
            SqlParameter p_Branch = new SqlParameter("@Branch", SqlDbType.VarChar);
            p_Branch.Value = ddlBranch.SelectedItem.Text;
            SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
            p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;
            SqlParameter p_txtReceiver199 = new SqlParameter("@Receiver", SqlDbType.VarChar);
            p_txtReceiver199.Value = txtReceiver199.Text.Trim();
            SqlParameter p_199TranRefno = new SqlParameter("@Trans_Ref_No", SqlDbType.VarChar);
            p_199TranRefno.Value = txt_199_transRefNo.Text.Trim();
            SqlParameter p_199RelatedRef = new SqlParameter("@Related_Ref", SqlDbType.VarChar);
            p_199RelatedRef.Value = txt_199_RelRef.Text.Trim();
            SqlParameter p_199Narr1 = new SqlParameter("@Narrative1", SqlDbType.VarChar);
            p_199Narr1.Value = txt_199_Narr1.Text.Trim();
            SqlParameter p_199Narr2 = new SqlParameter("@Narrative2", SqlDbType.VarChar);
            p_199Narr2.Value = txt_199_Narr2.Text.Trim();
            SqlParameter p_199Narr3 = new SqlParameter("@Narrative3", SqlDbType.VarChar);
            p_199Narr3.Value = txt_199_Narr3.Text.Trim();
            SqlParameter p_199Narr4 = new SqlParameter("@Narrative4", SqlDbType.VarChar);
            p_199Narr4.Value = txt_199_Narr4.Text.Trim();
            SqlParameter p_199Narr5 = new SqlParameter("@Narrative5", SqlDbType.VarChar);
            p_199Narr5.Value = txt_199_Narr5.Text.Trim();
            SqlParameter p_199Narr6 = new SqlParameter("@Narrative6", SqlDbType.VarChar);
            p_199Narr6.Value = txt_199_Narr6.Text.Trim();
            SqlParameter p_199Narr7 = new SqlParameter("@Narrative7", SqlDbType.VarChar);
            p_199Narr7.Value = txt_199_Narr7.Text.Trim();
            SqlParameter p_199Narr8 = new SqlParameter("@Narrative8", SqlDbType.VarChar);
            p_199Narr8.Value = txt_199_Narr8.Text.Trim();
            SqlParameter p_199Narr9 = new SqlParameter("@Narrative9", SqlDbType.VarChar);
            p_199Narr9.Value = txt_199_Narr9.Text.Trim();
            SqlParameter p_199Narr10 = new SqlParameter("@Narrative10", SqlDbType.VarChar);
            p_199Narr10.Value = txt_199_Narr10.Text.Trim();
            SqlParameter p_199Narr11 = new SqlParameter("@Narrative11", SqlDbType.VarChar);
            p_199Narr11.Value = txt_199_Narr11.Text.Trim();
            SqlParameter p_199Narr12 = new SqlParameter("@Narrative12", SqlDbType.VarChar);
            p_199Narr12.Value = txt_199_Narr12.Text.Trim();
            SqlParameter p_199Narr13 = new SqlParameter("@Narrative13", SqlDbType.VarChar);
            p_199Narr13.Value = txt_199_Narr13.Text.Trim();
            SqlParameter p_199Narr14 = new SqlParameter("@Narrative14", SqlDbType.VarChar);
            p_199Narr14.Value = txt_199_Narr14.Text.Trim();
            SqlParameter p_199Narr15 = new SqlParameter("@Narrative15", SqlDbType.VarChar);
            p_199Narr15.Value = txt_199_Narr15.Text.Trim();
            SqlParameter p_199Narr16 = new SqlParameter("@Narrative16", SqlDbType.VarChar);
            p_199Narr16.Value = txt_199_Narr16.Text.Trim();
            SqlParameter p_199Narr17 = new SqlParameter("@Narrative17", SqlDbType.VarChar);
            p_199Narr17.Value = txt_199_Narr17.Text.Trim();
            SqlParameter p_199Narr18 = new SqlParameter("@Narrative18", SqlDbType.VarChar);
            p_199Narr18.Value = txt_199_Narr18.Text.Trim();
            SqlParameter p_199Narr19 = new SqlParameter("@Narrative19", SqlDbType.VarChar);
            p_199Narr19.Value = txt_199_Narr19.Text.Trim();
            SqlParameter p_199Narr20 = new SqlParameter("@Narrative20", SqlDbType.VarChar);
            p_199Narr20.Value = txt_199_Narr20.Text.Trim();
            SqlParameter p_199Narr21 = new SqlParameter("@Narrative21", SqlDbType.VarChar);
            p_199Narr21.Value = txt_199_Narr21.Text.Trim();
            SqlParameter p_199Narr22 = new SqlParameter("@Narrative22", SqlDbType.VarChar);
            p_199Narr22.Value = txt_199_Narr22.Text.Trim();
            SqlParameter p_199Narr23 = new SqlParameter("@Narrative23", SqlDbType.VarChar);
            p_199Narr23.Value = txt_199_Narr23.Text.Trim();
            SqlParameter p_199Narr24 = new SqlParameter("@Narrative24", SqlDbType.VarChar);
            p_199Narr24.Value = txt_199_Narr24.Text.Trim();
            SqlParameter p_199Narr25 = new SqlParameter("@Narrative25", SqlDbType.VarChar);
            p_199Narr25.Value = txt_199_Narr25.Text.Trim();
            SqlParameter p_199Narr26 = new SqlParameter("@Narrative26", SqlDbType.VarChar);
            p_199Narr26.Value = txt_199_Narr26.Text.Trim();
            SqlParameter p_199Narr27 = new SqlParameter("@Narrative27", SqlDbType.VarChar);
            p_199Narr27.Value = txt_199_Narr27.Text.Trim();
            SqlParameter p_199Narr28 = new SqlParameter("@Narrative28", SqlDbType.VarChar);
            p_199Narr28.Value = txt_199_Narr28.Text.Trim();
            SqlParameter p_199Narr29 = new SqlParameter("@Narrative29", SqlDbType.VarChar);
            p_199Narr29.Value = txt_199_Narr29.Text.Trim();
            SqlParameter p_199Narr30 = new SqlParameter("@Narrative30", SqlDbType.VarChar);
            p_199Narr30.Value = txt_199_Narr30.Text.Trim();
            SqlParameter p_199Narr31 = new SqlParameter("@Narrative31", SqlDbType.VarChar);
            p_199Narr31.Value = txt_199_Narr31.Text.Trim();
            SqlParameter p_199Narr32 = new SqlParameter("@Narrative32", SqlDbType.VarChar);
            p_199Narr32.Value = txt_199_Narr32.Text.Trim();
            SqlParameter p_199Narr33 = new SqlParameter("@Narrative33", SqlDbType.VarChar);
            p_199Narr33.Value = txt_199_Narr33.Text.Trim();
            SqlParameter p_199Narr34 = new SqlParameter("@Narrative34", SqlDbType.VarChar);
            p_199Narr34.Value = txt_199_Narr34.Text.Trim();
            SqlParameter p_199Narr35 = new SqlParameter("@Narrative35", SqlDbType.VarChar);
            p_199Narr35.Value = txt_199_Narr35.Text.Trim();
            SqlParameter P_AddedBy = new SqlParameter("@AddedBy", hdnUserName);
            SqlParameter P_AddedDate = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            SqlParameter P_UpdatedBy = new SqlParameter("@UpdatedBy", hdnUserName);
            SqlParameter P_UpdatedDate = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

            TF_DATA objSave = new TF_DATA();
            _result = objSave.SaveDeleteData("TF_Exp_AddUpdateSwift199", p_Branch, p_SwiftType, p_txtReceiver199, p_199TranRefno, p_199RelatedRef, p_199Narr1, p_199Narr2, p_199Narr3, p_199Narr4, p_199Narr5,
              p_199Narr6, p_199Narr7, p_199Narr8, p_199Narr9, p_199Narr10, p_199Narr11, p_199Narr12, p_199Narr13, p_199Narr14, p_199Narr15, p_199Narr16, p_199Narr17, p_199Narr18,
              p_199Narr19, p_199Narr20, p_199Narr21, p_199Narr22, p_199Narr23, p_199Narr24, p_199Narr25, p_199Narr26, p_199Narr27, p_199Narr28, p_199Narr29, p_199Narr30,
              p_199Narr31, p_199Narr32, p_199Narr33, p_199Narr34, p_199Narr35, P_AddedBy, P_AddedDate, P_UpdatedBy, P_UpdatedDate);
            if (_result == "added")
            {
                audittrail("M", txt_199_transRefNo.Text.Trim());
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added as Draft.');", true);
            }
            else if (_result == "Updated")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Error while saving the Record');", true);
            }
        }
    }
    public void SaveView299()
    {
        string hdnUserName = Session["userName"].ToString();
        if (txtReceiver299.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Receiver');", true);
            txtReceiver299.Focus();
        }
        else if (txt_299_transRefNo.Text.Trim() == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter transaction reference number');", true);
            txt_299_transRefNo.Focus();
        }
        else if (txt_299_transRefNo.Text.Contains("//"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number contains two consecutive slashes //');", true);
            txt_299_transRefNo.Focus();
        }
        else if (txt_299_transRefNo.Text.StartsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number start with slash(/)');", true);
            txt_299_transRefNo.Focus();
        }
        else if (txt_299_transRefNo.Text.EndsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number end with slash(/)');", true);
            txt_299_transRefNo.Focus();
        }

        else if (txt_299_RelRef.Text.Contains("//"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference contains two consecutive slashes //');", true);
            txt_299_RelRef.Focus();
        }
        else if (txt_299_RelRef.Text.StartsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference start with slash(/)');", true);
            txt_299_RelRef.Focus();
        }
        else if (txt_299_RelRef.Text.EndsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Relateds Reference end with slash(/)');", true);
            txt_299_RelRef.Focus();
        }

        else if (txt_299_Narr1.Text == "" && txt_299_Narr2.Text == "" && txt_299_Narr3.Text == "" && txt_299_Narr4.Text == "" && txt_299_Narr5.Text == "" && txt_299_Narr6.Text == "" && txt_299_Narr7.Text == "" && txt_299_Narr8.Text == "" && txt_299_Narr9.Text == "" && txt_299_Narr10.Text == ""
        && txt_299_Narr11.Text == "" && txt_299_Narr12.Text == "" && txt_299_Narr13.Text == "" && txt_299_Narr14.Text == "" && txt_299_Narr15.Text == "" && txt_299_Narr16.Text == "" && txt_299_Narr17.Text == "" && txt_299_Narr18.Text == "" && txt_299_Narr19.Text == "" && txt_299_Narr20.Text == ""
        && txt_299_Narr21.Text == "" && txt_299_Narr22.Text == "" && txt_299_Narr23.Text == "" && txt_299_Narr24.Text == "" && txt_299_Narr25.Text == "" && txt_299_Narr26.Text == "" && txt_299_Narr27.Text == "" && txt_299_Narr28.Text == "" && txt_299_Narr29.Text == "" && txt_299_Narr30.Text == ""
        && txt_299_Narr31.Text == "" && txt_299_Narr32.Text == "" && txt_299_Narr33.Text == "" && txt_299_Narr34.Text == "" && txt_299_Narr35.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Narrative');", true);
            txt_299_Narr1.Focus();
        }

        else
        {
            SqlParameter p_Branch = new SqlParameter("@Branch", SqlDbType.VarChar);
            p_Branch.Value = ddlBranch.SelectedItem.Text;
            SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
            p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;
            SqlParameter p_txtReceiver299 = new SqlParameter("@Receiver", SqlDbType.VarChar);
            p_txtReceiver299.Value = txtReceiver299.Text.Trim();
            SqlParameter p_299TranRefno = new SqlParameter("@Trans_Ref_No", SqlDbType.VarChar);
            p_299TranRefno.Value = txt_299_transRefNo.Text.Trim();
            SqlParameter p_299RelatedRef = new SqlParameter("@Related_Ref", SqlDbType.VarChar);
            p_299RelatedRef.Value = txt_299_RelRef.Text.Trim();
            SqlParameter p_299Narr1 = new SqlParameter("@Narrative1", SqlDbType.VarChar);
            p_299Narr1.Value = txt_299_Narr1.Text.Trim();
            SqlParameter p_299Narr2 = new SqlParameter("@Narrative2", SqlDbType.VarChar);
            p_299Narr2.Value = txt_299_Narr2.Text.Trim();
            SqlParameter p_299Narr3 = new SqlParameter("@Narrative3", SqlDbType.VarChar);
            p_299Narr3.Value = txt_299_Narr3.Text.Trim();
            SqlParameter p_299Narr4 = new SqlParameter("@Narrative4", SqlDbType.VarChar);
            p_299Narr4.Value = txt_299_Narr4.Text.Trim();
            SqlParameter p_299Narr5 = new SqlParameter("@Narrative5", SqlDbType.VarChar);
            p_299Narr5.Value = txt_299_Narr5.Text.Trim();
            SqlParameter p_299Narr6 = new SqlParameter("@Narrative6", SqlDbType.VarChar);
            p_299Narr6.Value = txt_299_Narr6.Text.Trim();
            SqlParameter p_299Narr7 = new SqlParameter("@Narrative7", SqlDbType.VarChar);
            p_299Narr7.Value = txt_299_Narr7.Text.Trim();
            SqlParameter p_299Narr8 = new SqlParameter("@Narrative8", SqlDbType.VarChar);
            p_299Narr8.Value = txt_299_Narr8.Text.Trim();
            SqlParameter p_299Narr9 = new SqlParameter("@Narrative9", SqlDbType.VarChar);
            p_299Narr9.Value = txt_299_Narr9.Text.Trim();
            SqlParameter p_299Narr10 = new SqlParameter("@Narrative10", SqlDbType.VarChar);
            p_299Narr10.Value = txt_299_Narr10.Text.Trim();
            SqlParameter p_299Narr11 = new SqlParameter("@Narrative11", SqlDbType.VarChar);
            p_299Narr11.Value = txt_299_Narr11.Text.Trim();
            SqlParameter p_299Narr12 = new SqlParameter("@Narrative12", SqlDbType.VarChar);
            p_299Narr12.Value = txt_299_Narr12.Text.Trim();
            SqlParameter p_299Narr13 = new SqlParameter("@Narrative13", SqlDbType.VarChar);
            p_299Narr13.Value = txt_299_Narr13.Text.Trim();
            SqlParameter p_299Narr14 = new SqlParameter("@Narrative14", SqlDbType.VarChar);
            p_299Narr14.Value = txt_299_Narr14.Text.Trim();
            SqlParameter p_299Narr15 = new SqlParameter("@Narrative15", SqlDbType.VarChar);
            p_299Narr15.Value = txt_299_Narr15.Text.Trim();
            SqlParameter p_299Narr16 = new SqlParameter("@Narrative16", SqlDbType.VarChar);
            p_299Narr16.Value = txt_299_Narr16.Text.Trim();
            SqlParameter p_299Narr17 = new SqlParameter("@Narrative17", SqlDbType.VarChar);
            p_299Narr17.Value = txt_299_Narr17.Text.Trim();
            SqlParameter p_299Narr18 = new SqlParameter("@Narrative18", SqlDbType.VarChar);
            p_299Narr18.Value = txt_299_Narr18.Text.Trim();
            SqlParameter p_299Narr19 = new SqlParameter("@Narrative19", SqlDbType.VarChar);
            p_299Narr19.Value = txt_299_Narr19.Text.Trim();
            SqlParameter p_299Narr20 = new SqlParameter("@Narrative20", SqlDbType.VarChar);
            p_299Narr20.Value = txt_299_Narr20.Text.Trim();
            SqlParameter p_299Narr21 = new SqlParameter("@Narrative21", SqlDbType.VarChar);
            p_299Narr21.Value = txt_299_Narr21.Text.Trim();
            SqlParameter p_299Narr22 = new SqlParameter("@Narrative22", SqlDbType.VarChar);
            p_299Narr22.Value = txt_299_Narr22.Text.Trim();
            SqlParameter p_299Narr23 = new SqlParameter("@Narrative23", SqlDbType.VarChar);
            p_299Narr23.Value = txt_299_Narr23.Text.Trim();
            SqlParameter p_299Narr24 = new SqlParameter("@Narrative24", SqlDbType.VarChar);
            p_299Narr24.Value = txt_299_Narr24.Text.Trim();
            SqlParameter p_299Narr25 = new SqlParameter("@Narrative25", SqlDbType.VarChar);
            p_299Narr25.Value = txt_299_Narr25.Text.Trim();
            SqlParameter p_299Narr26 = new SqlParameter("@Narrative26", SqlDbType.VarChar);
            p_299Narr26.Value = txt_299_Narr26.Text.Trim();
            SqlParameter p_299Narr27 = new SqlParameter("@Narrative27", SqlDbType.VarChar);
            p_299Narr27.Value = txt_299_Narr27.Text.Trim();
            SqlParameter p_299Narr28 = new SqlParameter("@Narrative28", SqlDbType.VarChar);
            p_299Narr28.Value = txt_299_Narr28.Text.Trim();
            SqlParameter p_299Narr29 = new SqlParameter("@Narrative29", SqlDbType.VarChar);
            p_299Narr29.Value = txt_299_Narr29.Text.Trim();
            SqlParameter p_299Narr30 = new SqlParameter("@Narrative30", SqlDbType.VarChar);
            p_299Narr30.Value = txt_299_Narr30.Text.Trim();
            SqlParameter p_299Narr31 = new SqlParameter("@Narrative31", SqlDbType.VarChar);
            p_299Narr31.Value = txt_299_Narr31.Text.Trim();
            SqlParameter p_299Narr32 = new SqlParameter("@Narrative32", SqlDbType.VarChar);
            p_299Narr32.Value = txt_299_Narr32.Text.Trim();
            SqlParameter p_299Narr33 = new SqlParameter("@Narrative33", SqlDbType.VarChar);
            p_299Narr33.Value = txt_299_Narr33.Text.Trim();
            SqlParameter p_299Narr34 = new SqlParameter("@Narrative34", SqlDbType.VarChar);
            p_299Narr34.Value = txt_299_Narr34.Text.Trim();
            SqlParameter p_299Narr35 = new SqlParameter("@Narrative35", SqlDbType.VarChar);
            p_299Narr35.Value = txt_299_Narr35.Text.Trim();
            SqlParameter P_AddedBy = new SqlParameter("@AddedBy", hdnUserName);
            SqlParameter P_AddedDate = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            SqlParameter P_UpdatedBy = new SqlParameter("@UpdatedBy", hdnUserName);
            SqlParameter P_UpdatedDate = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

            TF_DATA objSave = new TF_DATA();
            _result = objSave.SaveDeleteData("TF_Exp_AddUpdateSwift299", p_Branch, p_SwiftType, p_txtReceiver299, p_299TranRefno, p_299RelatedRef, p_299Narr1, p_299Narr2, p_299Narr3, p_299Narr4, p_299Narr5,
              p_299Narr6, p_299Narr7, p_299Narr8, p_299Narr9, p_299Narr10, p_299Narr11, p_299Narr12, p_299Narr13, p_299Narr14, p_299Narr15, p_299Narr16, p_299Narr17, p_299Narr18,
              p_299Narr19, p_299Narr20, p_299Narr21, p_299Narr22, p_299Narr23, p_299Narr24, p_299Narr25, p_299Narr26, p_299Narr27, p_299Narr28, p_299Narr29, p_299Narr30,
              p_299Narr31, p_299Narr32, p_299Narr33, p_299Narr34, p_299Narr35, P_AddedBy, P_AddedDate, P_UpdatedBy, P_UpdatedDate);
            if (_result == "added")
            {
                audittrail("M", txt_299_transRefNo.Text.Trim());
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added as Draft.');", true);
            }
            else if (_result == "Updated")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Error while saving the Record');", true);
            }
        }
    }
    public void SaveView999()
    {
        string hdnUserName = Session["userName"].ToString();
        if (txtReceiver999.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Receiver');", true);
            txtReceiver999.Focus();
        }
        else if (txt_999_transRefNo.Text.Trim() == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter transaction reference number');", true);
            txt_999_transRefNo.Focus();
        }
        else if (txt_999_transRefNo.Text.Contains("//"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number contains two consecutive slashes //');", true);
            txt_999_transRefNo.Focus();
        }
        else if (txt_999_transRefNo.Text.StartsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number start with slash(/)');", true);
            txt_999_transRefNo.Focus();
        }
        else if (txt_999_transRefNo.Text.EndsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction Reference Number end with slash(/)');", true);
            txt_999_transRefNo.Focus();
        }

        else if (txt_999_RelRef.Text.Contains("//"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference contains two consecutive slashes //');", true);
            txt_999_RelRef.Focus();
        }
        else if (txt_999_RelRef.Text.StartsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Related Reference start with slash(/)');", true);
            txt_999_RelRef.Focus();
        }
        else if (txt_999_RelRef.Text.EndsWith("/"))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Relateds Reference end with slash(/)');", true);
            txt_999_RelRef.Focus();
        }

        else if (txt_999_Narr1.Text == "" && txt_999_Narr2.Text == "" && txt_999_Narr3.Text == "" && txt_999_Narr4.Text == "" && txt_999_Narr5.Text == "" && txt_999_Narr6.Text == "" && txt_999_Narr7.Text == "" && txt_999_Narr8.Text == "" && txt_999_Narr9.Text == "" && txt_999_Narr10.Text == ""
        && txt_999_Narr11.Text == "" && txt_999_Narr12.Text == "" && txt_999_Narr13.Text == "" && txt_999_Narr14.Text == "" && txt_999_Narr15.Text == "" && txt_999_Narr16.Text == "" && txt_999_Narr17.Text == "" && txt_999_Narr18.Text == "" && txt_999_Narr19.Text == "" && txt_999_Narr20.Text == ""
        && txt_999_Narr21.Text == "" && txt_999_Narr22.Text == "" && txt_999_Narr23.Text == "" && txt_999_Narr24.Text == "" && txt_999_Narr25.Text == "" && txt_999_Narr26.Text == "" && txt_999_Narr27.Text == "" && txt_999_Narr28.Text == "" && txt_999_Narr29.Text == "" && txt_999_Narr30.Text == ""
        && txt_999_Narr31.Text == "" && txt_999_Narr32.Text == "" && txt_999_Narr33.Text == "" && txt_999_Narr34.Text == "" && txt_999_Narr35.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please enter Narrative');", true);
            txt_999_Narr1.Focus();
        }

        else
        {
            SqlParameter p_Branch = new SqlParameter("@Branch", SqlDbType.VarChar);
            p_Branch.Value = ddlBranch.SelectedItem.Text;
            SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
            p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;
            SqlParameter p_txtReceiver999 = new SqlParameter("@Receiver", SqlDbType.VarChar);
            p_txtReceiver999.Value = txtReceiver999.Text.Trim();
            SqlParameter p_999TranRefno = new SqlParameter("@Trans_Ref_No", SqlDbType.VarChar);
            p_999TranRefno.Value = txt_999_transRefNo.Text.Trim();
            SqlParameter p_999RelatedRef = new SqlParameter("@Related_Ref", SqlDbType.VarChar);
            p_999RelatedRef.Value = txt_999_RelRef.Text.Trim();
            SqlParameter p_999Narr1 = new SqlParameter("@Narrative1", SqlDbType.VarChar);
            p_999Narr1.Value = txt_999_Narr1.Text.Trim();
            SqlParameter p_999Narr2 = new SqlParameter("@Narrative2", SqlDbType.VarChar);
            p_999Narr2.Value = txt_999_Narr2.Text.Trim();
            SqlParameter p_999Narr3 = new SqlParameter("@Narrative3", SqlDbType.VarChar);
            p_999Narr3.Value = txt_999_Narr3.Text.Trim();
            SqlParameter p_999Narr4 = new SqlParameter("@Narrative4", SqlDbType.VarChar);
            p_999Narr4.Value = txt_999_Narr4.Text.Trim();
            SqlParameter p_999Narr5 = new SqlParameter("@Narrative5", SqlDbType.VarChar);
            p_999Narr5.Value = txt_999_Narr5.Text.Trim();
            SqlParameter p_999Narr6 = new SqlParameter("@Narrative6", SqlDbType.VarChar);
            p_999Narr6.Value = txt_999_Narr6.Text.Trim();
            SqlParameter p_999Narr7 = new SqlParameter("@Narrative7", SqlDbType.VarChar);
            p_999Narr7.Value = txt_999_Narr7.Text.Trim();
            SqlParameter p_999Narr8 = new SqlParameter("@Narrative8", SqlDbType.VarChar);
            p_999Narr8.Value = txt_999_Narr8.Text.Trim();
            SqlParameter p_999Narr9 = new SqlParameter("@Narrative9", SqlDbType.VarChar);
            p_999Narr9.Value = txt_999_Narr9.Text.Trim();
            SqlParameter p_999Narr10 = new SqlParameter("@Narrative10", SqlDbType.VarChar);
            p_999Narr10.Value = txt_999_Narr10.Text.Trim();
            SqlParameter p_999Narr11 = new SqlParameter("@Narrative11", SqlDbType.VarChar);
            p_999Narr11.Value = txt_999_Narr11.Text.Trim();
            SqlParameter p_999Narr12 = new SqlParameter("@Narrative12", SqlDbType.VarChar);
            p_999Narr12.Value = txt_999_Narr12.Text.Trim();
            SqlParameter p_999Narr13 = new SqlParameter("@Narrative13", SqlDbType.VarChar);
            p_999Narr13.Value = txt_999_Narr13.Text.Trim();
            SqlParameter p_999Narr14 = new SqlParameter("@Narrative14", SqlDbType.VarChar);
            p_999Narr14.Value = txt_999_Narr14.Text.Trim();
            SqlParameter p_999Narr15 = new SqlParameter("@Narrative15", SqlDbType.VarChar);
            p_999Narr15.Value = txt_999_Narr15.Text.Trim();
            SqlParameter p_999Narr16 = new SqlParameter("@Narrative16", SqlDbType.VarChar);
            p_999Narr16.Value = txt_999_Narr16.Text.Trim();
            SqlParameter p_999Narr17 = new SqlParameter("@Narrative17", SqlDbType.VarChar);
            p_999Narr17.Value = txt_999_Narr17.Text.Trim();
            SqlParameter p_999Narr18 = new SqlParameter("@Narrative18", SqlDbType.VarChar);
            p_999Narr18.Value = txt_999_Narr18.Text.Trim();
            SqlParameter p_999Narr19 = new SqlParameter("@Narrative19", SqlDbType.VarChar);
            p_999Narr19.Value = txt_999_Narr19.Text.Trim();
            SqlParameter p_999Narr20 = new SqlParameter("@Narrative20", SqlDbType.VarChar);
            p_999Narr20.Value = txt_999_Narr20.Text.Trim();
            SqlParameter p_999Narr21 = new SqlParameter("@Narrative21", SqlDbType.VarChar);
            p_999Narr21.Value = txt_999_Narr21.Text.Trim();
            SqlParameter p_999Narr22 = new SqlParameter("@Narrative22", SqlDbType.VarChar);
            p_999Narr22.Value = txt_999_Narr22.Text.Trim();
            SqlParameter p_999Narr23 = new SqlParameter("@Narrative23", SqlDbType.VarChar);
            p_999Narr23.Value = txt_999_Narr23.Text.Trim();
            SqlParameter p_999Narr24 = new SqlParameter("@Narrative24", SqlDbType.VarChar);
            p_999Narr24.Value = txt_999_Narr24.Text.Trim();
            SqlParameter p_999Narr25 = new SqlParameter("@Narrative25", SqlDbType.VarChar);
            p_999Narr25.Value = txt_999_Narr25.Text.Trim();
            SqlParameter p_999Narr26 = new SqlParameter("@Narrative26", SqlDbType.VarChar);
            p_999Narr26.Value = txt_999_Narr26.Text.Trim();
            SqlParameter p_999Narr27 = new SqlParameter("@Narrative27", SqlDbType.VarChar);
            p_999Narr27.Value = txt_999_Narr27.Text.Trim();
            SqlParameter p_999Narr28 = new SqlParameter("@Narrative28", SqlDbType.VarChar);
            p_999Narr28.Value = txt_999_Narr28.Text.Trim();
            SqlParameter p_999Narr29 = new SqlParameter("@Narrative29", SqlDbType.VarChar);
            p_999Narr29.Value = txt_999_Narr29.Text.Trim();
            SqlParameter p_999Narr30 = new SqlParameter("@Narrative30", SqlDbType.VarChar);
            p_999Narr30.Value = txt_999_Narr30.Text.Trim();
            SqlParameter p_999Narr31 = new SqlParameter("@Narrative31", SqlDbType.VarChar);
            p_999Narr31.Value = txt_999_Narr31.Text.Trim();
            SqlParameter p_999Narr32 = new SqlParameter("@Narrative32", SqlDbType.VarChar);
            p_999Narr32.Value = txt_999_Narr32.Text.Trim();
            SqlParameter p_999Narr33 = new SqlParameter("@Narrative33", SqlDbType.VarChar);
            p_999Narr33.Value = txt_999_Narr33.Text.Trim();
            SqlParameter p_999Narr34 = new SqlParameter("@Narrative34", SqlDbType.VarChar);
            p_999Narr34.Value = txt_999_Narr34.Text.Trim();
            SqlParameter p_999Narr35 = new SqlParameter("@Narrative35", SqlDbType.VarChar);
            p_999Narr35.Value = txt_999_Narr35.Text.Trim();
            SqlParameter P_AddedBy = new SqlParameter("@AddedBy", hdnUserName);
            SqlParameter P_AddedDate = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            SqlParameter P_UpdatedBy = new SqlParameter("@UpdatedBy", hdnUserName);
            SqlParameter P_UpdatedDate = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

            TF_DATA objSave = new TF_DATA();
            _result = objSave.SaveDeleteData("TF_Exp_AddUpdateSwift999", p_Branch, p_SwiftType, p_txtReceiver999, p_999TranRefno, p_999RelatedRef, p_999Narr1, p_999Narr2, p_999Narr3, p_999Narr4, p_999Narr5,
              p_999Narr6, p_999Narr7, p_999Narr8, p_999Narr9, p_999Narr10, p_999Narr11, p_999Narr12, p_999Narr13, p_999Narr14, p_999Narr15, p_999Narr16, p_999Narr17, p_999Narr18,
              p_999Narr19, p_999Narr20, p_999Narr21, p_999Narr22, p_999Narr23, p_999Narr24, p_999Narr25, p_999Narr26, p_999Narr27, p_999Narr28, p_999Narr29, p_999Narr30,
              p_999Narr31, p_999Narr32, p_999Narr33, p_999Narr34, p_999Narr35, P_AddedBy, P_AddedDate, P_UpdatedBy, P_UpdatedDate);
            if (_result == "added")
            {
                audittrail("M", txt_999_transRefNo.Text.Trim());
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added as Draft.');", true);
            }
            else if (_result == "Updated")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Error while saving the Record');", true);
            }
        }
    }
    
    public void audittrail(string status, string DocNo)
    {
        SqlParameter p_Branch = new SqlParameter("@Branch", SqlDbType.VarChar);
        p_Branch.Value = ddlBranch.SelectedItem.Text;
        SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
        p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;
        SqlParameter p_Transrefno = new SqlParameter("@Trans_Ref_No", DocNo);
        string hdnUserName = Session["userName"].ToString();
         SqlParameter P_Status = new SqlParameter("@Status", status);
        SqlParameter P_Makerid = new SqlParameter("@Makerid", hdnUserName);
        SqlParameter P_Makerdate = new SqlParameter("@MakerDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData("TF_Exp_SaveAuditTrail", p_Branch, p_SwiftType, p_Transrefno, P_Status, P_Makerid, P_Makerdate);

        if (_result == "added")
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added as Draft.');", true);
        }
    }
    protected void fillCurrency()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            DataTable dt_PrinAmt = objData.getData("TF_IMP_Currency_List");
            ddl_742_PrinAmtClmd_Ccy.Items.Clear();
            ListItem li_PrinAmt = new ListItem();
            li_PrinAmt.Value = "";
            if (dt_PrinAmt.Rows.Count > 0)
            {
                li_PrinAmt.Text = "";
                ddl_742_PrinAmtClmd_Ccy.DataSource = dt_PrinAmt.DefaultView;
                ddl_742_PrinAmtClmd_Ccy.DataTextField = "C_Code";
                ddl_742_PrinAmtClmd_Ccy.DataValueField = "C_Code";
                ddl_742_PrinAmtClmd_Ccy.DataBind();
            }
            else
            {
                li_PrinAmt.Text = "No record(s) found";
            }
            ddl_742_PrinAmtClmd_Ccy.Items.Insert(0, li_PrinAmt);

            //////////////////////////

            DataTable dt_AddAmtClamd = objData.getData("TF_IMP_Currency_List");
            ddl_742_AddAmtClamd_Ccy.Items.Clear();
            ListItem li_AddAmtClamd = new ListItem();
            li_AddAmtClamd.Value = "";
            if (dt_AddAmtClamd.Rows.Count > 0)
            {
                li_AddAmtClamd.Text = "";
                ddl_742_AddAmtClamd_Ccy.DataSource = dt_AddAmtClamd.DefaultView;
                ddl_742_AddAmtClamd_Ccy.DataTextField = "C_Code";
                ddl_742_AddAmtClamd_Ccy.DataValueField = "C_Code";
                ddl_742_AddAmtClamd_Ccy.DataBind();
            }
            else
            {
                li_AddAmtClamd.Text = "No record(s) found";
            }
            ddl_742_AddAmtClamd_Ccy.Items.Insert(0, li_AddAmtClamd);

            ///////////////////////////

            DataTable dt_TotalAmtClmd = objData.getData("TF_IMP_Currency_List");
            ddl_742_TotalAmtClmd_Ccy.Items.Clear();
            ListItem li_TotalAmtClmd = new ListItem();
            li_TotalAmtClmd.Value = "";
            if (dt_TotalAmtClmd.Rows.Count > 0)
            {
                li_TotalAmtClmd.Text = "";
                ddl_742_TotalAmtClmd_Ccy.DataSource = dt_TotalAmtClmd.DefaultView;
                ddl_742_TotalAmtClmd_Ccy.DataTextField = "C_Code";
                ddl_742_TotalAmtClmd_Ccy.DataValueField = "C_Code";
                ddl_742_TotalAmtClmd_Ccy.DataBind();
            }
            else
            {
                li_TotalAmtClmd.Text = "No record(s) found";
            }
            ddl_742_TotalAmtClmd_Ccy.Items.Insert(0, li_TotalAmtClmd);

            ////////////////////////
            DataTable dt_AddAmt = objData.getData("TF_IMP_Currency_List");
            ddl_754_AddAmtClamd_Ccy.Items.Clear();
            ListItem li_AddAmt = new ListItem();
            li_AddAmt.Value = "";
            if (dt_AddAmt.Rows.Count > 0)
            {
                li_AddAmt.Text = "";
                ddl_754_AddAmtClamd_Ccy.DataSource = dt_AddAmt.DefaultView;
                ddl_754_AddAmtClamd_Ccy.DataTextField = "C_Code";
                ddl_754_AddAmtClamd_Ccy.DataValueField = "C_Code";
                ddl_754_AddAmtClamd_Ccy.DataBind();
            }
            else
            {
                li_AddAmt.Text = "No record(s) found";
            }
            ddl_754_AddAmtClamd_Ccy.Items.Insert(0, li_AddAmt);

            ///////////////////
            DataTable dt_PrinAmtPaidAccNego = objData.getData("TF_IMP_Currency_List");
            ddl_PrinAmtPaidAccNegoCurr_754.Items.Clear();
            ListItem li_PrinAmtPaidAccNego = new ListItem();
            li_PrinAmtPaidAccNego.Value = "";
            if (dt_PrinAmtPaidAccNego.Rows.Count > 0)
            {
                li_PrinAmtPaidAccNego.Value = "";
                ddl_PrinAmtPaidAccNegoCurr_754.DataSource = dt_PrinAmtPaidAccNego.DefaultView;
                ddl_PrinAmtPaidAccNegoCurr_754.DataTextField = "C_Code";
                ddl_PrinAmtPaidAccNegoCurr_754.DataValueField = "C_Code";
                ddl_PrinAmtPaidAccNegoCurr_754.DataBind();
            }
            else
            {
                li_PrinAmtPaidAccNego.Text = "No record(s) found";
            }
            ddl_PrinAmtPaidAccNegoCurr_754.Items.Insert(0, li_PrinAmtPaidAccNego);

            ////////////////

            DataTable dt_TotalAmt = objData.getData("TF_IMP_Currency_List");
            ddl_754_TotalAmtClmd_Ccy.Items.Clear();
            ListItem li_TotalAmt = new ListItem();
            li_TotalAmt.Value = "";
            if (dt_TotalAmt.Rows.Count > 0)
            {
                li_TotalAmt.Text = "";
                ddl_754_TotalAmtClmd_Ccy.DataSource = dt_TotalAmt.DefaultView;
                ddl_754_TotalAmtClmd_Ccy.DataTextField = "C_Code";
                ddl_754_TotalAmtClmd_Ccy.DataValueField = "C_Code";
                ddl_754_TotalAmtClmd_Ccy.DataBind();
            }
            else
            {
                li_TotalAmt.Text = "No record(s) found";
            }
            ddl_754_TotalAmtClmd_Ccy.Items.Insert(0, li_TotalAmt);

            ////////////////

            DataTable dt_AmountTraced = objData.getData("TF_IMP_Currency_List");
            ddl_AmountTracedCurrency_420.Items.Clear();
            ListItem li_AmountTraced = new ListItem();
            li_AmountTraced.Value = "";
            if (dt_AmountTraced.Rows.Count > 0)
            {
                ddl_AmountTracedCurrency_420.DataSource = dt_AmountTraced.DefaultView;
                ddl_AmountTracedCurrency_420.DataTextField = "C_Code";
                ddl_AmountTracedCurrency_420.DataValueField = "C_Code";
                ddl_AmountTracedCurrency_420.DataBind();
            }
            else
            {
                li_AmountTraced.Text = "No record(s) found";
            }
            ddl_AmountTracedCurrency_420.Items.Insert(0, li_AmountTraced);


        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void ddlSwiftTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSwiftTypes.SelectedValue == "1")
        {
            TabContainerMain.Visible = true;
            TabSwifts.Visible = true;
            Panel_103.Visible = true;
            Panel_299.Visible = false;
            Panel_199.Visible = false;
            Panel_202.Visible = false;
            Panel_420.Visible = false;
            Panel_499.Visible = false;
            Panel_730.Visible = false;
            Panel_742.Visible = false;
            Panel_754.Visible = false;
            Panel_799.Visible = false;
            Panel_999.Visible = false;
        }
        if (ddlSwiftTypes.SelectedValue == "2")
        {
            TabContainerMain.Visible = true;
            TabSwifts.Visible = true;
            Panel_103.Visible = false;
            Panel_299.Visible = true;
            Panel_199.Visible = false;
            Panel_202.Visible = false;
            Panel_420.Visible = false;
            Panel_499.Visible = false;
            Panel_730.Visible = false;
            Panel_742.Visible = false;
            Panel_754.Visible = false;
            Panel_799.Visible = false;
            Panel_999.Visible = false;
        }

        if (ddlSwiftTypes.SelectedValue == "3")
        {
            TabContainerMain.Visible = true;
            TabSwifts.Visible = true;
            Panel_103.Visible = false;
            Panel_299.Visible = false;
            Panel_199.Visible = true;
            Panel_202.Visible = false;
            Panel_420.Visible = false;
            Panel_499.Visible = false;
            Panel_730.Visible = false;
            Panel_742.Visible = false;
            Panel_754.Visible = false;
            Panel_799.Visible = false;
            Panel_999.Visible = false;
        }

        if (ddlSwiftTypes.SelectedValue == "4")
        {
            TabContainerMain.Visible = true;
            TabSwifts.Visible = true;
            Panel_103.Visible = false;
            Panel_299.Visible = false;
            Panel_199.Visible = false;
            Panel_202.Visible = true;
            Panel_420.Visible = false;
            Panel_499.Visible = false;
            Panel_730.Visible = false;
            Panel_742.Visible = false;
            Panel_754.Visible = false;
            Panel_799.Visible = false;
            Panel_999.Visible = false;
        }

        if (ddlSwiftTypes.SelectedValue == "5")
        {
            TabContainerMain.Visible = true;
            TabSwifts.Visible = true;
            Panel_103.Visible = false;
            Panel_299.Visible = false;
            Panel_199.Visible = false;
            Panel_202.Visible = false;
            Panel_420.Visible = true;
            Panel_499.Visible = false;
            Panel_730.Visible = false;
            Panel_742.Visible = false;
            Panel_754.Visible = false;
            Panel_799.Visible = false;
            Panel_999.Visible = false;
        }

        if (ddlSwiftTypes.SelectedValue == "6")
        {
            TabContainerMain.Visible = true;
            TabSwifts.Visible = true;
            Panel_103.Visible = false;
            Panel_299.Visible = false;
            Panel_199.Visible = false;
            Panel_202.Visible = false;
            Panel_420.Visible = false;
            Panel_499.Visible = true;
            Panel_730.Visible = false;
            Panel_742.Visible = false;
            Panel_754.Visible = false;
            Panel_799.Visible = false;
            Panel_999.Visible = false;
        }

        if (ddlSwiftTypes.SelectedValue == "7")
        {
            TabContainerMain.Visible = true;
            TabSwifts.Visible = true;
            Panel_103.Visible = false;
            Panel_299.Visible = false;
            Panel_199.Visible = false;
            Panel_202.Visible = false;
            Panel_420.Visible = false;
            Panel_499.Visible = false;
            Panel_730.Visible = true;
            Panel_742.Visible = false;
            Panel_754.Visible = false;
            Panel_799.Visible = false;
            Panel_999.Visible = false;
        }

        if (ddlSwiftTypes.SelectedValue == "8")
        {
            TabContainerMain.Visible = true;
            TabSwifts.Visible = true;
            Panel_103.Visible = false;
            Panel_299.Visible = false;
            Panel_199.Visible = false;
            Panel_202.Visible = false;
            Panel_420.Visible = false;
            Panel_499.Visible = false;
            Panel_730.Visible = false;
            Panel_742.Visible = true;
            Panel_754.Visible = false;
            Panel_799.Visible = false;
            Panel_999.Visible = false;
        }

        if (ddlSwiftTypes.SelectedValue == "9")
        {
            TabContainerMain.Visible = true;
            TabSwifts.Visible = true;
            Panel_103.Visible = false;
            Panel_299.Visible = false;
            Panel_199.Visible = false;
            Panel_202.Visible = false;
            Panel_420.Visible = false;
            Panel_499.Visible = false;
            Panel_730.Visible = false;
            Panel_742.Visible = false;
            Panel_754.Visible = false;
            Panel_799.Visible = true;
            Panel_999.Visible = false;
            //  ddlSwiftTypes.ClearSelection();
        }

        if (ddlSwiftTypes.SelectedValue == "10")
        {
            TabContainerMain.Visible = true;
            TabSwifts.Visible = true;
            Panel_103.Visible = false;
            Panel_299.Visible = false;
            Panel_199.Visible = false;
            Panel_202.Visible = false;
            Panel_420.Visible = false;
            Panel_499.Visible = false;
            Panel_730.Visible = false;
            Panel_742.Visible = false;
            Panel_754.Visible = false;
            Panel_799.Visible = false;
            Panel_999.Visible = true;
        }

        if (ddlSwiftTypes.SelectedValue == "11")
        {
            TabContainerMain.Visible = true;
            TabSwifts.Visible = true;
            Panel_103.Visible = false;
            Panel_299.Visible = false;
            Panel_199.Visible = false;
            Panel_202.Visible = false;
            Panel_420.Visible = false;
            Panel_499.Visible = false;
            Panel_730.Visible = false;
            Panel_742.Visible = false;
            Panel_754.Visible = true;
            Panel_799.Visible = false;
            Panel_999.Visible = false;
        }

    }
    protected void ddl_AmountTracedCurrency_420_SelectedIndexChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@CCode", SqlDbType.VarChar);
        p1.Value = ddl_AmountTracedCurrency_420.SelectedItem.Text.Trim();
        string _query = "TF_Exp_Currency_List";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            ddl_AmountTracedCurrency_420.Text = dt.Rows[0]["C_Code"].ToString().Trim();
        }
    }
    protected void ddl_742_PrinAmtClmd_Ccy_SelectedIndexChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@CCode", SqlDbType.VarChar);
        p1.Value = ddl_742_PrinAmtClmd_Ccy.SelectedItem.Text.Trim();
        string _query = "TF_Exp_Currency_List";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            ddl_742_PrinAmtClmd_Ccy.Text = dt.Rows[0]["C_Code"].ToString().Trim();
        }
    }
    protected void ddl_742_AddAmtClamd_Ccy_SelectedIndexChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@CCode", SqlDbType.VarChar);
        p1.Value = ddl_742_AddAmtClamd_Ccy.SelectedItem.Text.Trim();
        string _query = "TF_Exp_Currency_List";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            ddl_742_AddAmtClamd_Ccy.Text = dt.Rows[0]["C_Code"].ToString().Trim();
        }
    }
    protected void ddl_742_TotalAmtClmd_Ccy_SelectedIndexChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@CCode", SqlDbType.VarChar);
        p1.Value = ddl_742_TotalAmtClmd_Ccy.SelectedItem.Text.Trim();
        string _query = "TF_Exp_Currency_List";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            ddl_742_TotalAmtClmd_Ccy.Text = dt.Rows[0]["C_Code"].ToString().Trim();
        }
    }
    protected void ddl_754_AddAmtClamd_Ccy_SelectedIndexChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@CCode", SqlDbType.VarChar);
        p1.Value = ddl_754_AddAmtClamd_Ccy.SelectedItem.Text.Trim();
        string _query = "TF_Exp_Currency_List";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            ddl_754_AddAmtClamd_Ccy.Text = dt.Rows[0]["C_Code"].ToString().Trim();
        }
    }
    protected void ddl_PrinAmtPaidAccNegoCurr_754_SelectedIndexChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@CCode", SqlDbType.VarChar);
        p1.Value = ddl_PrinAmtPaidAccNegoCurr_754.SelectedItem.Text.Trim();
        string _query = "TF_Exp_Currency_List";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            ddl_PrinAmtPaidAccNegoCurr_754.Text = dt.Rows[0]["C_Code"].ToString().Trim();
        }
    }
    protected void ddl_754_TotalAmtClmd_Ccy_SelectedIndexChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@CCode", SqlDbType.VarChar);
        p1.Value = ddl_754_TotalAmtClmd_Ccy.SelectedItem.Text.Trim();
        string _query = "TF_Exp_Currency_List";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            ddl_754_TotalAmtClmd_Ccy.Text = dt.Rows[0]["C_Code"].ToString().Trim();
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = ddlBranch.Text.Trim();
        string _query = "TF_GetBranchDetails_EXP_BillEntry";
        DataTable dt = objData.getData(_query, p1);

        if (dt.Rows.Count > 0)
        {
            string txtBranchCode = "";
            txtBranchCode = dt.Rows[0]["BranchCode"].ToString().Trim();
        }
        else
        {
            //hdnBranchCode.Value = "";
            //txtDocumentNo.Text = "";
        }
        //fillGrid();
        ddlBranch.Focus();
    }
    protected void ddlintermediaryinstitution_TextChanged(object sender, EventArgs e)
    {
        if (ddlintermediaryinstitution.SelectedValue == "A")
        {
            lblintermediaryinstitutionidenfier.Text = "Identifier Code: ";
            txtintermediaryinstitutioncode.Visible = true;
            txtintermediaryinstitutionname.Visible = false;
            lblintermediaryinstitutionAddress1.Visible = false;
            txtintermediaryinstitutionAddress1.Visible = false;
            lblintermediaryinstitutionAddress2.Visible = false;
            txtintermediaryinstitutionAddress2.Visible = false;
            lblintermediaryinstitutionAddress3.Visible = false;
            txtintermediaryinstitutionAddress3.Visible = false;
        }
        else
        {
            if (ddlintermediaryinstitution.SelectedValue == "C")
            {
                lblintermediaryinstitutionidenfier.Text = "";
                txtintermediaryinstitutionidentifiercode.Visible = false;
                txtintermediaryinstitutioncode.Visible = false;
                txtintermediaryinstitutionname.Visible = false;
                lblintermediaryinstitutionAddress1.Visible = false;
                txtintermediaryinstitutionAddress1.Visible = false;
                lblintermediaryinstitutionAddress2.Visible = false;
                txtintermediaryinstitutionAddress2.Visible = false;
                lblintermediaryinstitutionAddress3.Visible = false;
                txtintermediaryinstitutionAddress3.Visible = false;
            }
            else
            {
                lblintermediaryinstitutionidenfier.Text = "Name: ";
                txtintermediaryinstitutioncode.Visible = false;
                txtintermediaryinstitutionidentifiercode.Visible = true;
                txtintermediaryinstitutionname.Visible = true;
                lblintermediaryinstitutionAddress1.Visible = true;
                txtintermediaryinstitutionAddress1.Visible = true;
                lblintermediaryinstitutionAddress2.Visible = true;
                txtintermediaryinstitutionAddress2.Visible = true;
                lblintermediaryinstitutionAddress3.Visible = true;
                txtintermediaryinstitutionAddress3.Visible = true;
            }
        }
    }
    protected void ddlAccountwithinstitution_TextChanged(object sender, EventArgs e)
    {
        if (ddlAccountwithinstitution.SelectedValue == "A")
        {
            lblAccountwithinstitutionidenfier.Text = "Identifier Code: ";
            txtAccountwithinstitutioncode.Visible = true;
            txtAccountwithinstitutionlocation.Visible = false;
            txtAccountwithinstitutionName.Visible = false;

            lblAccountwithinstitutionAddress1.Visible = false;
            lblAccountwithinstitutionAddress2.Visible = false;
            lblAccountwithinstitutionAddress3.Visible = false;
            txtAccountwithinstitutionAddress1.Visible = false;
            txtAccountwithinstitutionAddress2.Visible = false;
            txtAccountwithinstitutionAddress3.Visible = false;
        }
        else
        {
            if (ddlAccountwithinstitution.SelectedValue == "B")
            {
                lblAccountwithinstitutionidenfier.Text = "Location :";
                txtAccountwithinstitutionidentifiercode.Visible = true;
                txtAccountwithinstitutioncode.Visible = false;
                txtAccountwithinstitutionlocation.Visible = true;
                txtAccountwithinstitutionName.Visible = false;

                lblAccountwithinstitutionAddress1.Visible = false;
                lblAccountwithinstitutionAddress2.Visible = false;
                lblAccountwithinstitutionAddress3.Visible = false;
                txtAccountwithinstitutionAddress1.Visible = false;
                txtAccountwithinstitutionAddress2.Visible = false;
                txtAccountwithinstitutionAddress3.Visible = false;
            }
            else
            {
                if (ddlAccountwithinstitution.SelectedValue == "C")
                {
                    lblAccountwithinstitutionidenfier.Text = "";
                    txtAccountwithinstitutionidentifiercode.Visible = false;
                    txtAccountwithinstitutioncode.Visible = false;
                    txtAccountwithinstitutionlocation.Visible = false;
                    txtAccountwithinstitutionName.Visible = false;

                    lblAccountwithinstitutionAddress1.Visible = false;
                    lblAccountwithinstitutionAddress2.Visible = false;
                    lblAccountwithinstitutionAddress3.Visible = false;
                    txtAccountwithinstitutionAddress1.Visible = false;
                    txtAccountwithinstitutionAddress2.Visible = false;
                    txtAccountwithinstitutionAddress3.Visible = false;
                }
                else
                {
                    lblAccountwithinstitutionidenfier.Text = "Name : ";
                    txtAccountwithinstitutionidentifiercode.Visible = true;
                    txtAccountwithinstitutioncode.Visible = false;
                    txtAccountwithinstitutionlocation.Visible = false;
                    txtAccountwithinstitutionName.Visible = true;
                    lblAccountwithinstitutionAddress1.Visible = true;
                    lblAccountwithinstitutionAddress2.Visible = true;
                    lblAccountwithinstitutionAddress3.Visible = true;
                    txtAccountwithinstitutionAddress1.Visible = true;
                    txtAccountwithinstitutionAddress2.Visible = true;
                    txtAccountwithinstitutionAddress3.Visible = true;
                }
            }
        }
    }
    protected void ddlOrderingcustomer_TextChanged(object sender, EventArgs e)
    {
        if (ddlOrderingcustomer.SelectedValue == "A")
        {
            lblOrderingcustomerSwiftcode.Text = "Identifier Code ";
            TxtOrderingcustomerSwiftcode.Visible = true;
            TxtOrderingcustomerName.Visible = false;
            TxtOrderingcustomerNameK.Visible = false;
            lblOrderingcustomerAddress1.Visible = false;
            txtOrderingcustomerAddress1.Visible = false;
            lblOrderingcustomerAddress2.Visible = false;
            txtOrderingcustomerAddress2.Visible = false;
            lblOrderingcustomerAddress3.Visible = false;
            txtOrderingcustomerAddress3.Visible = false;
        }
        else
        {
            if (ddlOrderingcustomer.SelectedValue == "F")
            {
                lblOrderingcustomerSwiftcode.Text = " Name : ";
                TxtOrderingcustomerSwiftcode.Visible = false;
                TxtOrderingcustomerName.Visible = true;
                TxtOrderingcustomerNameK.Visible = false;
                lblOrderingcustomerAddress1.Visible = true;
                txtOrderingcustomerAddress1.Visible = true;
                lblOrderingcustomerAddress2.Visible = true;
                txtOrderingcustomerAddress2.Visible = true;
                lblOrderingcustomerAddress3.Visible = true;
                txtOrderingcustomerAddress3.Visible = true;
            }
            else
            {
                lblOrderingcustomerSwiftcode.Text = " Name : ";
                TxtOrderingcustomerSwiftcode.Visible = false;
                TxtOrderingcustomerName.Visible = false;
                TxtOrderingcustomerNameK.Visible = true;
                lblOrderingcustomerAddress1.Visible = true;
                txtOrderingcustomerAddress1.Visible = true;
                lblOrderingcustomerAddress2.Visible = true;
                txtOrderingcustomerAddress2.Visible = true;
                lblOrderingcustomerAddress3.Visible = true;
                txtOrderingcustomerAddress3.Visible = true;
            }
        }
    }
    protected void ddlOrderingInstitution_TextChanged(object sender, EventArgs e)
    {
        if (ddlOrderingInstitution.SelectedValue == "A")
        {
            lblOrderingInstitutionSwiftCode.Text = "Identifier Code: ";
            txtOrderingInstitutionSwiftCode.Visible = true;
            txtOrderingInstitutionName.Visible = false;
            lblOrderingInstitutionAddress1.Visible = false;
            lblOrderingInstitutionAddress2.Visible = false;
            lblOrderingInstitutionAddress3.Visible = false;
            txtOrderingInstitutionAddress1.Visible = false;
            txtOrderingInstitutionAddress2.Visible = false;
            txtOrderingInstitutionAddress3.Visible = false;
        }
        else
        {
            lblOrderingInstitutionSwiftCode.Text = "Name: ";
            txtOrderingInstitutionSwiftCode.Visible = false;
            txtOrderingInstitutionName.Visible = true;
            lblOrderingInstitutionAddress1.Visible = true;
            lblOrderingInstitutionAddress2.Visible = true;
            lblOrderingInstitutionAddress3.Visible = true;
            txtOrderingInstitutionAddress1.Visible = true;
            txtOrderingInstitutionAddress2.Visible = true;
            txtOrderingInstitutionAddress3.Visible = true;
        }
    }
    protected void ddlSendersCorrespondent_TextChanged(object sender, EventArgs e)
    {
        if (ddlSendersCorrespondent.SelectedValue == "A")
        {
            lblSendersCorrespondentSwiftCode.Text = "Identifier Code: ";
            txtSendersCorrespondentSwiftCode.Visible = true;
            txtSendersCorrespondentName.Visible = false;
            txtSendersCorrespondentLocation.Visible = false;
            lblSendersCorrespondentAddress1.Visible = false;
            lblSendersCorrespondentAddress2.Visible = false;
            lblSendersCorrespondentAddress3.Visible = false;
            txtSendersCorrespondentAddress1.Visible = false;
            txtSendersCorrespondentAddress2.Visible = false;
            txtSendersCorrespondentAddress3.Visible = false;
        }
        else
        {
            if (ddlSendersCorrespondent.SelectedValue == "B")
            {
                lblSendersCorrespondentSwiftCode.Text = "Location: ";
                txtSendersCorrespondentSwiftCode.Visible = false;
                txtSendersCorrespondentName.Visible = false;
                txtSendersCorrespondentLocation.Visible = true;
                lblSendersCorrespondentAddress1.Visible = false;
                lblSendersCorrespondentAddress2.Visible = false;
                lblSendersCorrespondentAddress3.Visible = false;
                txtSendersCorrespondentAddress1.Visible = false;
                txtSendersCorrespondentAddress2.Visible = false;
                txtSendersCorrespondentAddress3.Visible = false;
            }
            else
            {
                lblSendersCorrespondentSwiftCode.Text = "Name: ";
                txtSendersCorrespondentSwiftCode.Visible = false;
                txtSendersCorrespondentName.Visible = true;
                txtSendersCorrespondentLocation.Visible = false;
                lblSendersCorrespondentAddress1.Visible = true;
                lblSendersCorrespondentAddress2.Visible = true;
                lblSendersCorrespondentAddress3.Visible = true;
                txtSendersCorrespondentAddress1.Visible = true;
                txtSendersCorrespondentAddress2.Visible = true;
                txtSendersCorrespondentAddress3.Visible = true;
            }
        }
    }
    protected void ddlReceiversCorrespondent_TextChanged(object sender, EventArgs e)
    {
        if (ddlReceiversCorrespondent.SelectedValue == "A")
        {
            lblReceiversCorrespondentSwiftCode.Text = "Identifier Code: ";
            txtReceiversCorrespondentSwiftCode.Visible = true;
            txtReceiversCorrespondentName.Visible = false;
            txtReceiversCorrespondentLocation.Visible = false;
            lblReceiversCorrespondentAddress1.Visible = false;
            lblReceiversCorrespondentAddress2.Visible = false;
            lblReceiversCorrespondentAddress3.Visible = false;
            txtReceiversCorrespondentAddress1.Visible = false;
            txtReceiversCorrespondentAddress2.Visible = false;
            txtReceiversCorrespondentAddress3.Visible = false;
        }
        else
        {
            if (ddlReceiversCorrespondent.SelectedValue == "B")
            {
                lblReceiversCorrespondentSwiftCode.Text = "Location: ";
                txtReceiversCorrespondentSwiftCode.Visible = false;
                txtReceiversCorrespondentName.Visible = false;
                txtReceiversCorrespondentLocation.Visible = true;
                lblReceiversCorrespondentAddress1.Visible = false;
                lblReceiversCorrespondentAddress2.Visible = false;
                lblReceiversCorrespondentAddress3.Visible = false;
                txtReceiversCorrespondentAddress1.Visible = false;
                txtReceiversCorrespondentAddress2.Visible = false;
                txtReceiversCorrespondentAddress3.Visible = false;
            }
            else
            {
                lblReceiversCorrespondentSwiftCode.Text = "Name: ";
                txtReceiversCorrespondentSwiftCode.Visible = false;
                txtReceiversCorrespondentName.Visible = true;
                txtReceiversCorrespondentLocation.Visible = false;
                lblReceiversCorrespondentAddress1.Visible = true;
                lblReceiversCorrespondentAddress2.Visible = true;
                lblReceiversCorrespondentAddress3.Visible = true;
                txtReceiversCorrespondentAddress1.Visible = true;
                txtReceiversCorrespondentAddress2.Visible = true;
                txtReceiversCorrespondentAddress3.Visible = true;
            }
        }
    }
    protected void ddlThirdReimbursementInstitution_TextChanged(object sender, EventArgs e)
    {
        if (ddlThirdReimbursementInstitution.SelectedValue == "A")
        {
            lblThirdReimbursementInstitutionSwiftCode.Text = "Identifier Code: ";
            txtThirdReimbursementInstitutionSwiftCode.Visible = true;
            txtThirdReimbursementInstitutionName.Visible = false;
            txtThirdReimbursementInstitutionLocation.Visible = false;
            lblThirdReimbursementInstitutionAddress1.Visible = false;
            lblThirdReimbursementInstitutionAddress2.Visible = false;
            lblThirdReimbursementInstitutionAddress3.Visible = false;
            txtThirdReimbursementInstitutionAddress1.Visible = false;
            txtThirdReimbursementInstitutionAddress2.Visible = false;
            txtThirdReimbursementInstitutionAddress3.Visible = false;
        }
        else
        {
            if (ddlThirdReimbursementInstitution.SelectedValue == "B")
            {
                lblThirdReimbursementInstitutionSwiftCode.Text = "Location: ";
                txtThirdReimbursementInstitutionSwiftCode.Visible = false;
                txtThirdReimbursementInstitutionName.Visible = false;
                txtThirdReimbursementInstitutionLocation.Visible = true;
                lblThirdReimbursementInstitutionAddress1.Visible = false;
                lblThirdReimbursementInstitutionAddress2.Visible = false;
                lblThirdReimbursementInstitutionAddress3.Visible = false;
                txtThirdReimbursementInstitutionAddress1.Visible = false;
                txtThirdReimbursementInstitutionAddress2.Visible = false;
                txtThirdReimbursementInstitutionAddress3.Visible = false;
            }
            else
            {
                lblThirdReimbursementInstitutionSwiftCode.Text = "Name: ";
                txtThirdReimbursementInstitutionSwiftCode.Visible = false;
                txtThirdReimbursementInstitutionName.Visible = true;
                txtThirdReimbursementInstitutionLocation.Visible = false;
                lblThirdReimbursementInstitutionAddress1.Visible = true;
                lblThirdReimbursementInstitutionAddress2.Visible = true;
                lblThirdReimbursementInstitutionAddress3.Visible = true;
                txtThirdReimbursementInstitutionAddress1.Visible = true;
                txtThirdReimbursementInstitutionAddress2.Visible = true;
                txtThirdReimbursementInstitutionAddress3.Visible = true;
            }
        }
    }
    protected void ddlOrderingInstitution_191_TextChanged(object sender, EventArgs e)
    {
        if (ddlOrderingInstitution_191.SelectedValue == "A")
        {
            lblOrderingInstitutionSwiftCode_191.Text = "Identifier Code: ";
            txtOrderingInstitutionSwiftCode_191.Visible = true;
            txtOrderingInstitutionName_191.Visible = false;
            lblOrderingInstitutionAddress1_191.Visible = false;
            lblOrderingInstitutionAddress2_191.Visible = false;
            lblOrderingInstitutionAddress3_191.Visible = false;
            txtOrderingInstitutionAddress1_191.Visible = false;
            txtOrderingInstitutionAddress2_191.Visible = false;
            txtOrderingInstitutionAddress3_191.Visible = false;
        }
        else
        {
            lblOrderingInstitutionSwiftCode_191.Text = "Name: ";
            txtOrderingInstitutionSwiftCode_191.Visible = false;
            txtOrderingInstitutionName_191.Visible = true;
            lblOrderingInstitutionAddress1_191.Visible = true;
            lblOrderingInstitutionAddress2_191.Visible = true;
            lblOrderingInstitutionAddress3_191.Visible = true;
            txtOrderingInstitutionAddress1_191.Visible = true;
            txtOrderingInstitutionAddress2_191.Visible = true;
            txtOrderingInstitutionAddress3_191.Visible = true;
        }
    }
    protected void ddlAccountwithinstitution_191_TextChanged(object sender, EventArgs e)
    {
        if (ddlAccountwithinstitution_191.SelectedValue == "A")
        {
            lblAccountwithinstitutionidenfier_191.Text = "Identifier Code: ";
            txtAccountwithinstitutioncode_191.Visible = true;
            txtAccountwithinstitutionlocation_191.Visible = false;
            txtAccountwithinstitutionName_191.Visible = false;

            lblAccountwithinstitutionAddress1_191.Visible = false;
            lblAccountwithinstitutionAddress2_191.Visible = false;
            lblAccountwithinstitutionAddress3_191.Visible = false;
            txtAccountwithinstitutionAddress1_191.Visible = false;
            txtAccountwithinstitutionAddress2_191.Visible = false;
            txtAccountwithinstitutionAddress3_191.Visible = false;
        }
        else
        {
            if (ddlAccountwithinstitution_191.SelectedValue == "B")
            {
                lblAccountwithinstitutionidenfier_191.Text = "Location :";
                txtAccountwithinstitutionidentifiercode_191.Visible = true;
                txtAccountwithinstitutioncode_191.Visible = false;
                txtAccountwithinstitutionlocation_191.Visible = true;
                txtAccountwithinstitutionName_191.Visible = false;

                lblAccountwithinstitutionAddress1_191.Visible = false;
                lblAccountwithinstitutionAddress2_191.Visible = false;
                lblAccountwithinstitutionAddress3_191.Visible = false;
                txtAccountwithinstitutionAddress1_191.Visible = false;
                txtAccountwithinstitutionAddress2_191.Visible = false;
                txtAccountwithinstitutionAddress3_191.Visible = false;
            }
            else
            {
                lblAccountwithinstitutionidenfier_191.Text = "Name : ";
                txtAccountwithinstitutionidentifiercode_191.Visible = true;
                txtAccountwithinstitutioncode_191.Visible = false;
                txtAccountwithinstitutionlocation_191.Visible = false;
                txtAccountwithinstitutionName_191.Visible = true;
                lblAccountwithinstitutionAddress1_191.Visible = true;
                lblAccountwithinstitutionAddress2_191.Visible = true;
                lblAccountwithinstitutionAddress3_191.Visible = true;
                txtAccountwithinstitutionAddress1_191.Visible = true;
                txtAccountwithinstitutionAddress2_191.Visible = true;
                txtAccountwithinstitutionAddress3_191.Visible = true;
            }
        }
    }
    protected void ddlAmountofCharges_730_TextChanged(object sender, EventArgs e)
    {
        if (ddlAmountofCharges_730.SelectedValue == "B")
        {
            txtAmountofChargesDate_730.Visible = false;
            txtAmountofChargesCurrency_730.Visible = true;
            txtAmountofChargesAmount_730.Visible = true;
        }
        else
        {
            txtAmountofChargesDate_730.Visible = true;
            txtAmountofChargesCurrency_730.Visible = true;
            txtAmountofChargesAmount_730.Visible = true;
        }
    }
    protected void ddlAccountWithBank_730_TextChanged(object sender, EventArgs e)
    {
        if (ddlAccountWithBank_730.SelectedValue == "A")
        {
            lblAccountWithBankSwiftcode_730.Text = "Identifier Code: ";
            txtAccountWithBankIdentifiercode_730.Visible = true;
            txtAccountWithBankName_730.Visible = false;
            lblAccountWithBankAddress1_730.Visible = false;
            lblAccountWithBankAddress2_730.Visible = false;
            lblAccountWithBankAddress3_730.Visible = false;
            txtAccountWithBankAddress1_730.Visible = false;
            txtAccountWithBankAddress2_730.Visible = false;
            txtAccountWithBankAddress3_730.Visible = false;
        }
        else
        {
            lblAccountWithBankSwiftcode_730.Text = "Name: ";
            txtAccountWithBankIdentifiercode_730.Visible = false;
            txtAccountWithBankName_730.Visible = true;
            lblAccountWithBankAddress1_730.Visible = true;
            lblAccountWithBankAddress2_730.Visible = true;
            lblAccountWithBankAddress3_730.Visible = true;
            txtAccountWithBankAddress1_730.Visible = true;
            txtAccountWithBankAddress2_730.Visible = true;
            txtAccountWithBankAddress3_730.Visible = true;
        }
    }
    protected void ddlOrderingInstitution_202_TextChanged(object sender, EventArgs e)
    {
        if (ddlOrderingInstitution_202.SelectedValue == "A")
        {
            lblOrderingInstitutionidentier_202.Text = "Identifier Code: ";
            txtOrderingInstitutionidentifiercode_202.Visible = true;
            txtOrderingInstitutionName_202.Visible = false;
            lblOrderingInstitutionAddress1_202.Visible = false;
            lblOrderingInstitutionAddress2_202.Visible = false;
            lblOrderingInstitutionAddress3_202.Visible = false;
            txtOrderingInstitutionAddress1_202.Visible = false;
            txtOrderingInstitutionAddress2_202.Visible = false;
            txtOrderingInstitutionAddress3_202.Visible = false;
        }
        else
        {
            lblOrderingInstitutionidentier_202.Text = "Name: ";
            txtOrderingInstitutionidentifiercode_202.Visible = false;
            txtOrderingInstitutionName_202.Visible = true;
            lblOrderingInstitutionAddress1_202.Visible = true;
            lblOrderingInstitutionAddress2_202.Visible = true;
            lblOrderingInstitutionAddress3_202.Visible = true;
            txtOrderingInstitutionAddress1_202.Visible = true;
            txtOrderingInstitutionAddress2_202.Visible = true;
            txtOrderingInstitutionAddress3_202.Visible = true;
        }
    }
    protected void ddlSendersCorrespondent_202_TextChanged(object sender, EventArgs e)
    {
        if (ddlSendersCorrespondent_202.SelectedValue == "A")
        {
            lblSendersCorrespondentIdentifier_202.Text = "Identifier Code: ";
            txtSendersCorrespondentidentifiercode_202.Visible = true;
            txtSendersCorrespondentName_202.Visible = false;
            txtSendersCorrespondentLocation_202.Visible = false;
            lblSendersCorrespondentAddress1_202.Visible = false;
            lblSendersCorrespondentAddress2_202.Visible = false;
            lblSendersCorrespondentAddress3_202.Visible = false;
            txtSendersCorrespondentAddress1_202.Visible = false;
            txtSendersCorrespondentAddress2_202.Visible = false;
            txtSendersCorrespondentAddress3_202.Visible = false;
        }
        else
        {
            if (ddlSendersCorrespondent_202.SelectedValue == "B")
            {
                lblSendersCorrespondentIdentifier_202.Text = "Location: ";
                txtSendersCorrespondentidentifiercode_202.Visible = false;
                txtSendersCorrespondentName_202.Visible = false;
                txtSendersCorrespondentLocation_202.Visible = true;
                lblSendersCorrespondentAddress1_202.Visible = false;
                lblSendersCorrespondentAddress2_202.Visible = false;
                lblSendersCorrespondentAddress3_202.Visible = false;
                txtSendersCorrespondentAddress1_202.Visible = false;
                txtSendersCorrespondentAddress2_202.Visible = false;
                txtSendersCorrespondentAddress3_202.Visible = false;
            }
            else
            {
                lblSendersCorrespondentIdentifier_202.Text = "Name: ";
                txtSendersCorrespondentidentifiercode_202.Visible = false;
                txtSendersCorrespondentName_202.Visible = true;
                txtSendersCorrespondentLocation_202.Visible = false;
                lblSendersCorrespondentAddress1_202.Visible = true;
                lblSendersCorrespondentAddress2_202.Visible = true;
                lblSendersCorrespondentAddress3_202.Visible = true;
                txtSendersCorrespondentAddress1_202.Visible = true;
                txtSendersCorrespondentAddress2_202.Visible = true;
                txtSendersCorrespondentAddress3_202.Visible = true;
            }
        }
    }
    protected void ddlReceiversCorrespondent_202_TextChanged(object sender, EventArgs e)
    {
        if (ddlReceiversCorrespondent_202.SelectedValue == "A")
        {
            lblReceiversCorrespondentIdentier_202.Text = "Identifier Code: ";
            txtReceiversCorrespondentIdentifiercode_202.Visible = true;
            txtReceiversCorrespondentName_202.Visible = false;
            txtReceiversCorrespondentLocation_202.Visible = false;
            lblReceiversCorrespondentAddress1_202.Visible = false;
            lblReceiversCorrespondentAddress2_202.Visible = false;
            lblReceiversCorrespondentAddress3_202.Visible = false;
            txtReceiversCorrespondentAddress1_202.Visible = false;
            txtReceiversCorrespondentAddress2_202.Visible = false;
            txtReceiversCorrespondentAddress3_202.Visible = false;
        }
        else
        {
            if (ddlReceiversCorrespondent_202.SelectedValue == "B")
            {
                lblReceiversCorrespondentIdentier_202.Text = "Location: ";
                txtReceiversCorrespondentIdentifiercode_202.Visible = false;
                txtReceiversCorrespondentName_202.Visible = false;
                txtReceiversCorrespondentLocation_202.Visible = true;
                lblReceiversCorrespondentAddress1_202.Visible = false;
                lblReceiversCorrespondentAddress2_202.Visible = false;
                lblReceiversCorrespondentAddress3_202.Visible = false;
                txtReceiversCorrespondentAddress1_202.Visible = false;
                txtReceiversCorrespondentAddress2_202.Visible = false;
                txtReceiversCorrespondentAddress3_202.Visible = false;
            }
            else
            {
                lblReceiversCorrespondentIdentier_202.Text = "Name: ";
                txtReceiversCorrespondentIdentifiercode_202.Visible = false;
                txtReceiversCorrespondentName_202.Visible = true;
                txtReceiversCorrespondentLocation_202.Visible = false;
                lblReceiversCorrespondentAddress1_202.Visible = true;
                lblReceiversCorrespondentAddress2_202.Visible = true;
                lblReceiversCorrespondentAddress3_202.Visible = true;
                txtReceiversCorrespondentAddress1_202.Visible = true;
                txtReceiversCorrespondentAddress2_202.Visible = true;
                txtReceiversCorrespondentAddress3_202.Visible = true;
            }
        }
    }
    protected void ddlIntermediary_202_TextChanged(object sender, EventArgs e)
    {
        if (ddlIntermediary_202.SelectedValue == "A")
        {
            lblIntermediaryIdentifier.Text = "Identifier Code: ";
            txtIntermediaryIdentifiercode_202.Visible = true;
            txtIntermediaryname_202.Visible = false;
            lblIntermediaryAddress1_202.Visible = false;
            txtIntermediaryAddress1_202.Visible = false;
            lblIntermediaryAddress2_202.Visible = false;
            txtIntermediaryAddress2_202.Visible = false;
            lblIntermediaryAddress3_202.Visible = false;
            txtIntermediaryAddress3_202.Visible = false;
        }
        else
        {
            lblIntermediaryIdentifier.Text = "Name : ";
            txtIntermediaryIdentifiercode_202.Visible = false;
            txtIntermediaryname_202.Visible = true;
            lblIntermediaryAddress1_202.Visible = true;
            txtIntermediaryAddress1_202.Visible = true;
            lblIntermediaryAddress2_202.Visible = true;
            txtIntermediaryAddress2_202.Visible = true;
            lblIntermediaryAddress3_202.Visible = true;
            txtIntermediaryAddress3_202.Visible = true;
        }
    }
    protected void ddlAccountwithinstitution_202_TextChanged(object sender, EventArgs e)
    {
        if (ddlAccountwithinstitution_202.SelectedValue == "A")
        {
            lblAccountwithinstitutionidenfier_202.Text = "Identifier Code : ";
            txtAccountwithinstitutionIdentifiercode_202.Visible = true;
            txtAccountwithinstitutionlocation_202.Visible = false;
            txtAccountwithinstitutionName_191.Visible = false;
            lblAccountwithinstitutionAddress1_202.Visible = false;
            lblAccountwithinstitutionAddress2_202.Visible = false;
            lblAccountwithinstitutionAddress3_202.Visible = false;
            txtAccountwithinstitutionAddress1_202.Visible = false;
            txtAccountwithinstitutionAddress2_202.Visible = false;
            txtAccountwithinstitutionAddress3_202.Visible = false;
        }
        else
        {
            if (ddlAccountwithinstitution_202.SelectedValue == "B")
            {
                lblAccountwithinstitutionidenfier_202.Text = "Location : ";
                txtAccountwithinstitutionIdentifiercode_202.Visible = false;
                txtAccountwithinstitutionlocation_202.Visible = true;
                txtAccountwithinstitutionName_202.Visible = false;

                lblAccountwithinstitutionAddress1_202.Visible = false;
                lblAccountwithinstitutionAddress2_202.Visible = false;
                lblAccountwithinstitutionAddress3_202.Visible = false;
                txtAccountwithinstitutionAddress1_202.Visible = false;
                txtAccountwithinstitutionAddress2_202.Visible = false;
                txtAccountwithinstitutionAddress3_202.Visible = false;
            }
            else
            {
                lblAccountwithinstitutionidenfier_202.Text = "Name : ";
                txtAccountwithinstitutionIdentifiercode_202.Visible = false;
                txtAccountwithinstitutionlocation_202.Visible = false;
                txtAccountwithinstitutionName_202.Visible = true;
                lblAccountwithinstitutionAddress1_202.Visible = true;
                lblAccountwithinstitutionAddress2_202.Visible = true;
                lblAccountwithinstitutionAddress3_202.Visible = true;
                txtAccountwithinstitutionAddress1_202.Visible = true;
                txtAccountwithinstitutionAddress2_202.Visible = true;
                txtAccountwithinstitutionAddress3_202.Visible = true;
            }
        }
    }
    protected void ddlBeneficiaryInstitution_202_TextChanged(object sender, EventArgs e)
    {
        if (ddlBeneficiaryInstitution_202.SelectedValue == "A")
        {
            lblBeneficiaryInstitutionIdentifier_202.Text = "Identifier Code : ";
            txtBeneficiaryInstitutionidentifiercode_202.Visible = true;
            txtBeneficiaryInstitutionName_202.Visible = false;
            lblBeneficiaryInstitutionAddress1_202.Visible = false;
            lblBeneficiaryInstitutionAddress2_202.Visible = false;
            lblBeneficiaryInstitutionAddress3_202.Visible = false;
            txtBeneficiaryInstitutionAddress1_202.Visible = false;
            txtBeneficiaryInstitutionAddress2_202.Visible = false;
            txtBeneficiaryInstitutionAddress3_202.Visible = false;
        }
        else
        {
            lblBeneficiaryInstitutionIdentifier_202.Text = "Name : ";
            txtBeneficiaryInstitutionidentifiercode_202.Visible = false;
            txtBeneficiaryInstitutionName_202.Visible = true;
            lblBeneficiaryInstitutionAddress1_202.Visible = true;
            lblBeneficiaryInstitutionAddress2_202.Visible = true;
            lblBeneficiaryInstitutionAddress3_202.Visible = true;
            txtBeneficiaryInstitutionAddress1_202.Visible = true;
            txtBeneficiaryInstitutionAddress2_202.Visible = true;
            txtBeneficiaryInstitutionAddress3_202.Visible = true;
        }
    }
    protected void ddlAmountTraced_420_TextChanged(object sender, EventArgs e)
    {
        if (ddlAmountTraced_420.SelectedValue == "A")
        {
            lblAmountTracedDayMonth_420.Visible = false;
            ddlAmountTracedDayMonth_420.Visible = false;
            ddlAmountTracedDayMonth_420.SelectedItem.Text = "";
            lblAmountTracedNoofDaysMonth_420.Visible = false;
            txtAmountTracedNoofDaysMonth_420.Visible = false;
            txtAmountTracedNoofDaysMonth_420.Text = "";
            lblAmountTracedDate_420.Visible = true;
            txtAmountTracedDate_420.Visible = true;
            lblAmountTracedCode_420.Visible = false;
            ddlAmountTracedCode_420.Visible = false;
            ddlAmountTracedCode_420.SelectedItem.Text = "";
            ddl_AmountTracedCurrency_420.Visible = true;
            txtAmountTracedAmount_420.Visible = true;
            btnCal420_Date.Visible = true;
        }
        else
        {
            if (ddlAmountTraced_420.SelectedValue == "B")
            {
                lblAmountTracedDayMonth_420.Visible = false;
                ddlAmountTracedDayMonth_420.Visible = false;
                ddlAmountTracedDayMonth_420.SelectedItem.Text = "";
                lblAmountTracedNoofDaysMonth_420.Visible = false;
                txtAmountTracedNoofDaysMonth_420.Visible = false;
                txtAmountTracedNoofDaysMonth_420.Text = "";
                lblAmountTracedDate_420.Visible = false;
                txtAmountTracedDate_420.Visible = false;
                txtAmountTracedDate_420.Text = "";
                lblAmountTracedCode_420.Visible = false;
                ddlAmountTracedCode_420.Visible = false;
                ddlAmountTracedCode_420.SelectedItem.Text = "";
                ddl_AmountTracedCurrency_420.Visible = true;
                txtAmountTracedAmount_420.Visible = true;
                btnCal420_Date.Visible = false;
            }
            else
            {
                lblAmountTracedDayMonth_420.Visible = true;
                ddlAmountTracedDayMonth_420.Visible = true;
                lblAmountTracedNoofDaysMonth_420.Visible = true;
                txtAmountTracedNoofDaysMonth_420.Visible = true;
                lblAmountTracedDate_420.Visible = false;
                txtAmountTracedDate_420.Visible = false;
                txtAmountTracedDate_420.Text = "";
                lblAmountTracedCode_420.Visible = true;
                ddlAmountTracedCode_420.Visible = true;
                ddl_AmountTracedCurrency_420.Visible = true;
                txtAmountTracedAmount_420.Visible = true;
                btnCal420_Date.Visible = false;
            }
        }
    }
    protected void ddl_Issuingbank_742_TextChanged(object sender, EventArgs e)
    {
        if (ddl_Issuingbank_742.SelectedValue == "A")
        {
            lblIssuingBankIdentifier_742.Text = "Identifier Code: ";
            txtIssuingBankIdentifiercode_742.Visible = true;
            txtIssuingBankName_742.Visible = false;
            txtIssuingBankName_742.Text = "";
            lblIssuingBankAddress1_742.Visible = false;
            lblIssuingBankAddress2_742.Visible = false;
            lblIssuingBankAddress3_742.Visible = false;
            txtIssuingBankAddress1_742.Visible = false;
            txtIssuingBankAddress1_742.Text = "";
            txtIssuingBankAddress2_742.Visible = false;
            txtIssuingBankAddress2_742.Text = "";
            txtIssuingBankAddress3_742.Visible = false;
            txtIssuingBankAddress3_742.Text = "";
        }
        else
        {
            lblIssuingBankIdentifier_742.Text = "Name: ";
            txtIssuingBankIdentifiercode_742.Visible = false;
            txtIssuingBankIdentifiercode_742.Text = "";
            txtIssuingBankName_742.Visible = true;
            lblIssuingBankAddress1_742.Visible = true;
            lblIssuingBankAddress2_742.Visible = true;
            lblIssuingBankAddress3_742.Visible = true;
            txtIssuingBankAddress1_742.Visible = true;
            txtIssuingBankAddress2_742.Visible = true;
            txtIssuingBankAddress3_742.Visible = true;
        }
    }
    protected void ddlTotalAmtclamd_742_TextChanged(object sender, EventArgs e)
    {
        if (ddlTotalAmtclamd_742.SelectedValue == "A")
        {
            lbl_742_TotalAmtClmd_Date.Visible = true;
            txt_742_TotalAmtClmd_Date.Visible = true;
            TotalAmtClmd742_Date.Visible = true;
        }
        else
        {
            lbl_742_TotalAmtClmd_Date.Visible = false;
            txt_742_TotalAmtClmd_Date.Visible = false;
            txt_742_TotalAmtClmd_Date.Text = "";
            TotalAmtClmd742_Date.Visible = false;
        }
    }
    protected void ddlBeneficiarybank_742_TextChanged(object sender, EventArgs e)
    {
        if (ddlBeneficiarybank_742.SelectedValue == "A")
        {
            lblBeneficiaryBankIdentifier_742.Text = "Identifier Code: ";
            txtBeneficiaryBankIdentifiercode_742.Visible = true;
            txtBeneficiaryBankName_742.Visible = false;
            txtBeneficiaryBankName_742.Text = "";
            lblBeneficiaryBankAddress1_742.Visible = false;
            lblBeneficiaryBankAddress2_742.Visible = false;
            lblBeneficiaryBankAddress3_742.Visible = false;
            txtBeneficiaryBankAddress1_742.Visible = false;
            txtBeneficiaryBankAddress1_742.Text = "";
            txtBeneficiaryBankAddress2_742.Visible = false;
            txtBeneficiaryBankAddress2_742.Text = "";
            txtBeneficiaryBankAddress3_742.Visible = false;
            txtBeneficiaryBankAddress3_742.Text = "";
        }
        else
        {
            lblBeneficiaryBankIdentifier_742.Text = "Name: ";
            txtBeneficiaryBankIdentifiercode_742.Visible = false;
            txtBeneficiaryBankIdentifiercode_742.Text = "";
            txtBeneficiaryBankName_742.Visible = true;
            lblBeneficiaryBankAddress1_742.Visible = true;
            lblBeneficiaryBankAddress2_742.Visible = true;
            lblBeneficiaryBankAddress3_742.Visible = true;
            txtBeneficiaryBankAddress1_742.Visible = true;
            txtBeneficiaryBankAddress2_742.Visible = true;
            txtBeneficiaryBankAddress3_742.Visible = true;
        }
    }
    protected void ddlAccountwithbank_742_TextChanged(object sender, EventArgs e)
    {
        if (ddlAccountwithbank_742.SelectedValue == "A")
        {
            lblAccountwithBankIdentifier_742.Text = "Identifier Code: ";
            txtAccountwithBankIdentifiercode_742.Visible = true;
            txtAccountwithBankLocation_742.Visible = false;
            txtAccountwithBankLocation_742.Text = "";
            txtAccountwithBankName_742.Visible = false;
            txtAccountwithBankName_742.Text = "";
            lblAccountwithBankAddress1_742.Visible = false;
            lblAccountwithBankAddress2_742.Visible = false;
            lblAccountwithBankAddress3_742.Visible = false;
            txtAccountwithBankAddress1_742.Visible = false;
            txtAccountwithBankAddress1_742.Text = "";
            txtAccountwithBankAddress2_742.Visible = false;
            txtAccountwithBankAddress2_742.Text = "";
            txtAccountwithBankAddress3_742.Visible = false;
            txtAccountwithBankAddress3_742.Text = "";
        }
        else
        {
            if (ddlAccountwithbank_742.SelectedValue == "B")
            {
                lblAccountwithBankIdentifier_742.Text = "Location :";
                txtAccountwithBankIdentifiercode_742.Visible = false;
                txtAccountwithBankIdentifiercode_742.Text = "";
                txtAccountwithBankLocation_742.Visible = true;
                txtAccountwithBankName_742.Visible = false;
                txtAccountwithBankName_742.Text = "";
                lblAccountwithBankAddress1_742.Visible = false;
                lblAccountwithBankAddress2_742.Visible = false;
                lblAccountwithBankAddress3_742.Visible = false;
                txtAccountwithBankAddress1_742.Visible = false;
                txtAccountwithBankAddress1_742.Text = "";
                txtAccountwithBankAddress2_742.Visible = false;
                txtAccountwithBankAddress2_742.Text = "";
                txtAccountwithBankAddress3_742.Visible = false;
                txtAccountwithBankAddress3_742.Text = "";
            }
            else
            {
                lblAccountwithBankIdentifier_742.Text = "Name : ";
                txtAccountwithBankIdentifiercode_742.Visible = false;
                txtAccountwithBankIdentifiercode_742.Text = "";
                txtAccountwithBankLocation_742.Visible = false;
                txtAccountwithBankLocation_742.Text = "";
                txtAccountwithBankName_742.Visible = true;
                lblAccountwithBankAddress1_742.Visible = true;
                lblAccountwithBankAddress2_742.Visible = true;
                lblAccountwithBankAddress3_742.Visible = true;
                txtAccountwithBankAddress1_742.Visible = true;
                txtAccountwithBankAddress2_742.Visible = true;
                txtAccountwithBankAddress3_742.Visible = true;
            }
        }
    }
    protected void ddlTotalAmtclamd_754_TextChanged(object sender, EventArgs e)
    {
        if (ddlTotalAmtclamd_754.SelectedValue == "A")
        {
            lbl_754_TotalAmtClmd_Date.Visible = true;
            txt_754_TotalAmtClmd_Date.Visible = true;
            TotalAmtClmd754_Date.Visible = true;
            txt_754_TotalAmtClmd_Date.Focus();
        }
        else
        {
            lbl_754_TotalAmtClmd_Date.Visible = false;
            txt_754_TotalAmtClmd_Date.Visible = false;
            txt_754_TotalAmtClmd_Date.Text = "";
            TotalAmtClmd754_Date.Visible = false;
            ddl_754_TotalAmtClmd_Ccy.Focus();
        }
    }
    protected void ddlBeneficiarybank_754_TextChanged(object sender, EventArgs e)
    {
        if (ddlBeneficiarybank_754.SelectedValue == "A")
        {
            lblBeneficiaryBankIdentifier_754.Text = "Identifier Code: ";
            txtBeneficiaryBankIdentifiercode_754.Visible = true;
            txtBeneficiaryBankName_754.Visible = false;
            txtBeneficiaryBankName_754.Text = "";
            lblBeneficiaryBankAddress1_754.Visible = false;
            lblBeneficiaryBankAddress2_754.Visible = false;
            lblBeneficiaryBankAddress3_754.Visible = false;
            txtBeneficiaryBankAddress1_754.Visible = false;
            txtBeneficiaryBankAddress1_754.Text = "";
            txtBeneficiaryBankAddress2_754.Visible = false;
            txtBeneficiaryBankAddress2_754.Text = "";
            txtBeneficiaryBankAddress3_754.Visible = false;
            txtBeneficiaryBankAddress3_754.Text = "";
        }
        else
        {
            lblBeneficiaryBankIdentifier_754.Text = "Name: ";
            txtBeneficiaryBankIdentifiercode_754.Visible = false;
            txtBeneficiaryBankIdentifiercode_754.Text = "";
            txtBeneficiaryBankName_754.Visible = true;
            lblBeneficiaryBankAddress1_754.Visible = true;
            lblBeneficiaryBankAddress2_754.Visible = true;
            lblBeneficiaryBankAddress3_754.Visible = true;
            txtBeneficiaryBankAddress1_754.Visible = true;
            txtBeneficiaryBankAddress2_754.Visible = true;
            txtBeneficiaryBankAddress3_754.Visible = true;
        }
    }
    protected void ddlAccountwithbank_754_TextChanged(object sender, EventArgs e)
    {
        if (ddlAccountwithbank_754.SelectedValue == "A")
        {
            lblAccountwithBankIdentifier_754.Text = "Identifier Code: ";
            txtAccountwithBankIdentifiercode_754.Visible = true;
            txtAccountwithBankLocation_754.Visible = false;
            txtAccountwithBankLocation_754.Text = "";
            txtAccountwithBankName_754.Visible = false;
            txtAccountwithBankName_754.Text = "";

            lblAccountwithBankAddress1_754.Visible = false;
            lblAccountwithBankAddress2_754.Visible = false;
            lblAccountwithBankAddress3_754.Visible = false;
            txtAccountwithBankAddress1_754.Visible = false;
            txtAccountwithBankAddress1_754.Text = "";
            txtAccountwithBankAddress2_754.Visible = false;
            txtAccountwithBankAddress2_754.Text = "";
            txtAccountwithBankAddress3_754.Visible = false;
            txtAccountwithBankAddress3_754.Text = "";
        }
        else
        {
            if (ddlAccountwithbank_754.SelectedValue == "B")
            {
                lblAccountwithBankIdentifier_754.Text = "Location :";
                txtAccountwithBankIdentifiercode_754.Visible = false;
                txtAccountwithBankIdentifiercode_754.Text = "";
                txtAccountwithBankLocation_754.Visible = true;
                txtAccountwithBankName_754.Visible = false;
                txtAccountwithBankName_754.Text = "";
                lblAccountwithBankAddress1_754.Visible = false;
                lblAccountwithBankAddress2_754.Visible = false;
                lblAccountwithBankAddress3_754.Visible = false;
                txtAccountwithBankAddress1_754.Visible = false;
                txtAccountwithBankAddress1_754.Text = "";
                txtAccountwithBankAddress2_754.Visible = false;
                txtAccountwithBankAddress2_754.Text = "";
                txtAccountwithBankAddress3_754.Visible = false;
                txtAccountwithBankAddress3_754.Text = "";
            }
            else
            {
                lblAccountwithBankIdentifier_754.Text = "Name : ";
                txtAccountwithBankIdentifiercode_754.Visible = false;
                txtAccountwithBankIdentifiercode_754.Text = "";
                txtAccountwithBankLocation_754.Visible = false;
                txtAccountwithBankLocation_754.Text = "";
                txtAccountwithBankName_754.Visible = true;

                lblAccountwithBankAddress1_754.Visible = true;
                lblAccountwithBankAddress2_754.Visible = true;
                lblAccountwithBankAddress3_754.Visible = true;
                txtAccountwithBankAddress1_754.Visible = true;
                txtAccountwithBankAddress2_754.Visible = true;
                txtAccountwithBankAddress3_754.Visible = true;
            }
        }
    }
    protected void ddlPrinAmtPaidAccNego_754_TextChanged(object sender, EventArgs e)
    {
        if (ddlPrinAmtPaidAccNego_754.SelectedValue == "A")
        {
            lblPrinAmtPaidAccNegoDate_754.Visible = true;
            txtPrinAmtPaidAccNegoDate_754.Visible = true;
            BtnPrinAmtPaidAccNegoDate_754.Visible = true;
        }
        else
        {
            lblPrinAmtPaidAccNegoDate_754.Visible = false;
            txtPrinAmtPaidAccNegoDate_754.Visible = false;
            txtPrinAmtPaidAccNegoDate_754.Text = "";
            BtnPrinAmtPaidAccNegoDate_754.Visible = false;
        }
    }
    protected void ddlReimbursingbank_754_TextChanged(object sender, EventArgs e)
    {
        if (ddlReimbursingbank_754.SelectedValue == "A")
        {
            lblReimbursingBankIdentifier_754.Text = "Identifier Code: ";
            txtReimbursingBankIdentifiercode_754.Visible = true;
            txtReimbursingBankLocation_754.Visible = false;
            txtReimbursingBankLocation_754.Text = "";
            txtReimbursingBankName_754.Visible = false;
            txtReimbursingBankName_754.Text = "";

            lblReimbursingBankAddress1_754.Visible = false;
            lblReimbursingBankAddress2_754.Visible = false;
            lblReimbursingBankAddress3_754.Visible = false;
            txtReimbursingBankAddress1_754.Visible = false;
            txtReimbursingBankAddress1_754.Text = "";
            txtReimbursingBankAddress2_754.Visible = false;
            txtReimbursingBankAddress2_754.Text = "";
            txtReimbursingBankAddress3_754.Visible = false;
            txtReimbursingBankAddress3_754.Text = "";
        }
        else
        {
            if (ddlReimbursingbank_754.SelectedValue == "B")
            {
                lblReimbursingBankIdentifier_754.Text = "Location :";
                txtReimbursingBankIdentifiercode_754.Visible = false;
                txtReimbursingBankIdentifiercode_754.Text = "";
                txtReimbursingBankLocation_754.Visible = true;
                txtReimbursingBankName_754.Visible = false;
                txtReimbursingBankName_754.Text = "";
                lblReimbursingBankAddress1_754.Visible = false;
                lblReimbursingBankAddress2_754.Visible = false;
                lblReimbursingBankAddress3_754.Visible = false;
                txtReimbursingBankAddress1_754.Visible = false;
                txtReimbursingBankAddress1_754.Text = "";
                txtReimbursingBankAddress2_754.Visible = false;
                txtReimbursingBankAddress2_754.Text = "";
                txtReimbursingBankAddress3_754.Visible = false;
                txtReimbursingBankAddress3_754.Text = "";
            }
            else
            {
                lblReimbursingBankIdentifier_754.Text = "Name : ";
                txtReimbursingBankIdentifiercode_754.Visible = false;
                txtReimbursingBankIdentifiercode_754.Text = "";
                txtReimbursingBankLocation_754.Visible = false;
                txtReimbursingBankLocation_754.Text = "";
                txtReimbursingBankName_754.Visible = true;

                lblReimbursingBankAddress1_754.Visible = true;
                lblReimbursingBankAddress2_754.Visible = true;
                lblReimbursingBankAddress3_754.Visible = true;
                txtReimbursingBankAddress1_754.Visible = true;
                txtReimbursingBankAddress2_754.Visible = true;
                txtReimbursingBankAddress3_754.Visible = true;
            }
        }
    }
    //public void TotalAmountClaimed754Change()
    //{
    //    string TotalAmountClaimed754 = ddlTotalAmtclamd_754.SelectedValue;
    //    if (TotalAmountClaimed754 == "A")
    //    {
    //        txt_754_TotalAmtClmd_Date.Style.Add("display", "inline-block");
    //        lbl_754_TotalAmtClmd_Date.Style.Add("display", "inline-block");

    //    }
    //    if (TotalAmountClaimed754 == "B")
    //    {
    //        txt_754_TotalAmtClmd_Date.Style.Add("display", "none");
    //        lbl_754_TotalAmtClmd_Date.Style.Add("display", "none");

    //    }
    //}
    protected Boolean Check_X_CharSet(string CheckString)
    {
        //Regex r = new Regex(@"[^0-9a-zA-Z:/?().'+ ,-]+");
        Regex r = new Regex(@"[^0-9a-zA-Z /?:().,'+-]");
        if (r.IsMatch(CheckString.Trim()))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('special characters are not allowed !!');", true);
            return false;
        }
        else
        {
            return true;
        }
    }
    protected Boolean Check_Z_CharSet(string CheckString)
    {
        //Regex r = new Regex(@"[^0-9a-zA-Z .,()/='+:?!%&*<>;{@#_-]+");
        Regex r = new Regex(@"[^0-9a-zA-Z /?:().,'-]+");
        if (r.IsMatch(CheckString.Trim()))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('special characters are not allowed !!');", true);
            return false;
        }
        else
        {
            return true;
        }
    }
    protected Boolean Check_Alphabets(string CheckString)
    {
        Regex r = new Regex(@"[^A-Z]");
        if (r.IsMatch(CheckString.Trim()))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Only Captital characters are allowed !!');", true);
            return false;
        }
        else
        {
            return true;
        }
    }
    protected Boolean Check_AlphaNumric(string CheckString)
    {
        Regex r = new Regex(@"[^0-9A-Z]");
        if (r.IsMatch(CheckString.Trim()))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Only Number and Captital characters are allowed !!');", true);
            return false;
        }
        else
        {
            return true;
        }
    }

    protected void txtReceiver420_TextChanged(object sender, EventArgs e)
    {
        if (Check_AlphaNumric(txtReceiver420.Text) == false)
        {
            txtReceiver420.Text = "";
            txtReceiver420.Focus();
        }
        else if ((txtReceiver420.Text.Length < 8) || (txtReceiver420.Text.Length > 8 && txtReceiver420.Text.Length != 11))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Receiver should be 8 or 11 alphanumeric characters!');", true);
            txtReceiver420.Text = "";
            txtReceiver420.Focus();
        }
        else
        {
            txt_420_SendingBankTRN.Focus();
        }
    }
    protected void txt_420_SendingBankTRN_TextChanged(object sender, EventArgs e)
    {
        CheckTransRef_SwiftType();
        if (Check_X_CharSet(txt_420_SendingBankTRN.Text) == false)
        {
            txt_420_SendingBankTRN.Text = "";
            txt_420_SendingBankTRN.Focus();
        }
        else
        {
            txt_420_RelRef.Focus();
        }
    }
    protected void txt_420_RelRef_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_420_RelRef.Text) == false)
        {
            txt_420_RelRef.Text = "";
            txt_420_RelRef.Focus();
        }
        else
        {
            ddlAmountTracedDayMonth_420.Focus();
        }
    }

    //protected void txtAmountTracedDayMonth_420_TextChanged(object sender, EventArgs e)
    //{  
    //    if (Check_X_CharSet(txtAmountTracedDayMonth_420.Text) == false)
    //    {
    //        txtAmountTracedDayMonth_420.Text = "";
    //        txtAmountTracedDayMonth_420.Focus();
    //    }
    //    else
    //    {
    //        txtAmountTracedNoofDaysMonth_420.Focus();
    //    }
    //    Check420();
    //}
    protected void txtAmountTracedNoofDaysMonth_420_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtAmountTracedNoofDaysMonth_420.Text) == false)
        {
            txtAmountTracedNoofDaysMonth_420.Text = "";
            txtAmountTracedNoofDaysMonth_420.Focus();
        }
        else if (txtAmountTracedNoofDaysMonth_420.Text != "")
        {
            string DaysMonth_420 = Convert.ToInt32(txtAmountTracedNoofDaysMonth_420.Text).ToString("000");
            txtAmountTracedNoofDaysMonth_420.Text = DaysMonth_420;
            ddlAmountTracedCode_420.Focus();
        }
        else
        {
            ddlAmountTracedCode_420.Focus();
        }
       
    }
    //protected void txtAmountTracedCode_420_TextChanged(object sender, EventArgs e)
    //{
    //    if (Check_X_CharSet(txtAmountTracedCode_420.Text) == false)
    //    {
    //        txtAmountTracedCode_420.Text = "";
    //        txtAmountTracedCode_420.Focus();
    //    }
    //    else
    //    {
    //        txtAmountTracedAmount_420.Focus();
    //    }
    //}
    protected void txtAmountTracedAmount_420_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtAmountTracedAmount_420.Text) == false)
        {
            txtAmountTracedAmount_420.Text = "";
            txtAmountTracedAmount_420.Focus();
        }
        else
        {
            txt_420_DateofCollnInstruction.Focus();
        }
    }

    protected void txt_420_DraweeAccount_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_420_DraweeAccount.Text) == false)
        {
            txt_420_DraweeAccount.Text = "";
            txt_420_DraweeAccount.Focus();
        }
        else
        {
            txt_420_DraweeName.Focus();
        }
    }
    protected void txt_420_DraweeName_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_420_DraweeName.Text) == false)
        {
            txt_420_DraweeName.Text = "";
            txt_420_DraweeName.Focus();
        }
        else
        {
            txt_420_DraweeAdd1.Focus();
        }
    }
    protected void txt_420_DraweeAdd1_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_420_DraweeAdd1.Text) == false)
        {
            txt_420_DraweeAdd1.Text = "";
            txt_420_DraweeAdd1.Focus();
        }
        else
        {
            txt_420_DraweeAdd2.Focus();
        }
    }
    protected void txt_420_DraweeAdd2_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_420_DraweeAdd2.Text) == false)
        {
            txt_420_DraweeAdd2.Text = "";
            txt_420_DraweeAdd2.Focus();
        }
        else
        {
            txt_420_DraweeAdd3.Focus();
        }
    }
    protected void txt_420_DraweeAdd3_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_420_DraweeAdd3.Text) == false)
        {
            txt_420_DraweeAdd3.Text = "";
            txt_420_DraweeAdd3.Focus();
        }
        else
        {
            txt_420_SenToRecinfo1.Focus();
        }
    }

    protected void txt_420_SenToRecinfo1_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_420_SenToRecinfo1.Text) == false)
        {
            txt_420_SenToRecinfo1.Text = "";
            txt_420_SenToRecinfo1.Focus();
        }
        else
        {
            txt_420_SenToRecinfo2.Focus();
        }
    }
    protected void txt_420_SenToRecinfo2_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_420_SenToRecinfo2.Text) == false)
        {
            txt_420_SenToRecinfo2.Text = "";
            txt_420_SenToRecinfo2.Focus();
        }
        else
        {
            txt_420_SenToRecinfo3.Focus();
        }
    }
    protected void txt_420_SenToRecinfo3_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_420_SenToRecinfo3.Text) == false)
        {
            txt_420_SenToRecinfo3.Text = "";
            txt_420_SenToRecinfo3.Focus();
        }
        else
        {
            txt_420_SenToRecinfo4.Focus();
        }
    }
    protected void txt_420_SenToRecinfo4_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_420_SenToRecinfo4.Text) == false)
        {
            txt_420_SenToRecinfo4.Text = "";
            txt_420_SenToRecinfo4.Focus();
        }
        else
        {
            txt_420_SenToRecinfo5.Focus();
        }
    }
    protected void txt_420_SenToRecinfo5_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_420_SenToRecinfo5.Text) == false)
        {
            txt_420_SenToRecinfo5.Text = "";
            txt_420_SenToRecinfo5.Focus();
        }
        else
        {
            txt_420_SenToRecinfo6.Focus();
        }
    }
    protected void txt_420_SenToRecinfo6_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_420_SenToRecinfo6.Text) == false)
        {
            txt_420_SenToRecinfo6.Text = "";
            txt_420_SenToRecinfo6.Focus();
        }
        else
        {
            txt_420_SenToRecinfo6.Focus();
        }
    }

    //================================420=================================================================

    protected void txt_499_transRefNo_TextChanged(object sender, EventArgs e)
    {
        CheckTransRef_SwiftType();
        if (Check_X_CharSet(txt_499_transRefNo.Text) == false)
        {
            txt_499_transRefNo.Text = "";
            txt_499_transRefNo.Focus();
        }
        else
        {
            txt_499_RelRef.Focus();
        }
    }
    protected void txt_499_RelRef_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_RelRef.Text) == false)
        {
            txt_499_RelRef.Text = "";
            txt_499_RelRef.Focus();
        }
        else
        {
            txt_499_Narr1.Focus();
        }
    }
    protected void txtReceiver499_TextChanged(object sender, EventArgs e)
    {
        if (Check_AlphaNumric(txtReceiver499.Text) == false)
        {
            txtReceiver499.Text = "";
            txtReceiver499.Focus();
        }
        else if ((txtReceiver499.Text.Length < 8) || (txtReceiver499.Text.Length > 8 && txtReceiver499.Text.Length != 11))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Receiver should be 8 or 11 alphanumeric characters!');", true);
            txtReceiver499.Text = "";
            txtReceiver499.Focus();
        }
        else
        {
            txt_499_transRefNo.Focus();
        }
    }
    protected void txt_499_Narr1_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr1.Text) == false)
        {
            txt_499_Narr1.Text = "";
            txt_499_Narr1.Focus();
        }
        else
        {
            txt_499_Narr2.Focus();
        }
    }
    protected void txt_499_Narr2_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr2.Text) == false)
        {
            txt_499_Narr2.Text = "";
            txt_499_Narr2.Focus();
        }
        else
        {
            txt_499_Narr3.Focus();
        }
    }
    protected void txt_499_Narr3_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr3.Text) == false)
        {
            txt_499_Narr3.Text = "";
            txt_499_Narr3.Focus();
        }
        else
        {
            txt_499_Narr4.Focus();
        }
    }
    protected void txt_499_Narr4_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr4.Text) == false)
        {
            txt_499_Narr4.Text = "";
            txt_499_Narr4.Focus();
        }
        else
        {
            txt_499_Narr5.Focus();
        }
    }
    protected void txt_499_Narr5_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr5.Text) == false)
        {
            txt_499_Narr5.Text = "";
            txt_499_Narr5.Focus();
        }
        else
        {
            txt_499_Narr6.Focus();
        }
    }
    protected void txt_499_Narr6_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr6.Text) == false)
        {
            txt_499_Narr6.Text = "";
            txt_499_Narr6.Focus();
        }
        else
        {
            txt_499_Narr7.Focus();
        }
    }
    protected void txt_499_Narr7_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr7.Text) == false)
        {
            txt_499_Narr7.Text = "";
            txt_499_Narr7.Focus();
        }
        else
        {
            txt_499_Narr8.Focus();
        }
    }
    protected void txt_499_Narr8_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr8.Text) == false)
        {
            txt_499_Narr8.Text = "";
            txt_499_Narr8.Focus();
        }
        else
        {
            txt_499_Narr9.Focus();
        }
    }
    protected void txt_499_Narr9_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr9.Text) == false)
        {
            txt_499_Narr9.Text = "";
            txt_499_Narr9.Focus();
        }
        else
        {
            txt_499_Narr10.Focus();
        }
    }
    protected void txt_499_Narr10_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr10.Text) == false)
        {
            txt_499_Narr10.Text = "";
            txt_499_Narr10.Focus();
        }
        else
        {
            txt_499_Narr11.Focus();
        }
    }
    protected void txt_499_Narr11_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr11.Text) == false)
        {
            txt_499_Narr11.Text = "";
            txt_499_Narr11.Focus();
        }
        else
        {
            txt_499_Narr12.Focus();
        }
    }
    protected void txt_499_Narr12_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr12.Text) == false)
        {
            txt_499_Narr12.Text = "";
            txt_499_Narr12.Focus();
        }
        else
        {
            txt_499_Narr13.Focus();
        }
    }
    protected void txt_499_Narr13_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr13.Text) == false)
        {
            txt_499_Narr13.Text = "";
            txt_499_Narr13.Focus();
        }
        else
        {
            txt_499_Narr14.Focus();
        }
    }
    protected void txt_499_Narr14_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr14.Text) == false)
        {
            txt_499_Narr14.Text = "";
            txt_499_Narr14.Focus();
        }
        else
        {
            txt_499_Narr15.Focus();
        }
    }
    protected void txt_499_Narr15_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr15.Text) == false)
        {
            txt_499_Narr15.Text = "";
            txt_499_Narr15.Focus();
        }
        else
        {
            txt_499_Narr16.Focus();
        }
    }
    protected void txt_499_Narr16_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr16.Text) == false)
        {
            txt_499_Narr16.Text = "";
            txt_499_Narr16.Focus();
        }
        else
        {
            txt_499_Narr17.Focus();
        }
    }
    protected void txt_499_Narr17_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr17.Text) == false)
        {
            txt_499_Narr17.Text = "";
            txt_499_Narr17.Focus();
        }
        else
        {
            txt_499_Narr18.Focus();
        }
    }
    protected void txt_499_Narr18_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr18.Text) == false)
        {
            txt_499_Narr18.Text = "";
            txt_499_Narr18.Focus();
        }
        else
        {
            txt_499_Narr19.Focus();
        }
    }
    protected void txt_499_Narr19_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr19.Text) == false)
        {
            txt_499_Narr19.Text = "";
            txt_499_Narr19.Focus();
        }
        else
        {
            txt_499_Narr20.Focus();
        }
    }
    protected void txt_499_Narr20_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr20.Text) == false)
        {
            txt_499_Narr20.Text = "";
            txt_499_Narr20.Focus();
        }
        else
        {
            txt_499_Narr21.Focus();
        }
    }
    protected void txt_499_Narr21_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr21.Text) == false)
        {
            txt_499_Narr21.Text = "";
            txt_499_Narr21.Focus();
        }
        else
        {
            txt_499_Narr22.Focus();
        }
    }
    protected void txt_499_Narr22_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr22.Text) == false)
        {
            txt_499_Narr22.Text = "";
            txt_499_Narr22.Focus();
        }
        else
        {
            txt_499_Narr23.Focus();
        }
    }
    protected void txt_499_Narr23_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr23.Text) == false)
        {
            txt_499_Narr23.Text = "";
            txt_499_Narr23.Focus();
        }
        else
        {
            txt_499_Narr24.Focus();
        }
    }
    protected void txt_499_Narr24_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr24.Text) == false)
        {
            txt_499_Narr24.Text = "";
            txt_499_Narr24.Focus();
        }
        else
        {
            txt_499_Narr25.Focus();
        }
    }
    protected void txt_499_Narr25_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr25.Text) == false)
        {
            txt_499_Narr25.Text = "";
            txt_499_Narr25.Focus();
        }
        else
        {
            txt_499_Narr26.Focus();
        }
    }
    protected void txt_499_Narr26_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr26.Text) == false)
        {
            txt_499_Narr26.Text = "";
            txt_499_Narr26.Focus();
        }
        else
        {
            txt_499_Narr27.Focus();
        }
    }
    protected void txt_499_Narr27_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr27.Text) == false)
        {
            txt_499_Narr27.Text = "";
            txt_499_Narr27.Focus();
        }
        else
        {
            txt_499_Narr28.Focus();
        }
    }
    protected void txt_499_Narr28_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr28.Text) == false)
        {
            txt_499_Narr28.Text = "";
            txt_499_Narr28.Focus();
        }
        else
        {
            txt_499_Narr29.Focus();
        }
    }
    protected void txt_499_Narr29_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr29.Text) == false)
        {
            txt_499_Narr29.Text = "";
            txt_499_Narr29.Focus();
        }
        else
        {
            txt_499_Narr30.Focus();
        }
    }
    protected void txt_499_Narr30_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr30.Text) == false)
        {
            txt_499_Narr30.Text = "";
            txt_499_Narr30.Focus();
        }
        else
        {
            txt_499_Narr31.Focus();
        }
    }
    protected void txt_499_Narr31_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr31.Text) == false)
        {
            txt_499_Narr31.Text = "";
            txt_499_Narr31.Focus();
        }
        else
        {
            txt_499_Narr32.Focus();
        }
    }
    protected void txt_499_Narr32_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr32.Text) == false)
        {
            txt_499_Narr32.Text = "";
            txt_499_Narr32.Focus();
        }
        else
        {
            txt_499_Narr33.Focus();
        }
    }
    protected void txt_499_Narr33_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr33.Text) == false)
        {
            txt_499_Narr33.Text = "";
            txt_499_Narr33.Focus();
        }
        else
        {
            txt_499_Narr34.Focus();
        }
    }
    protected void txt_499_Narr34_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr34.Text) == false)
        {
            txt_499_Narr34.Text = "";
            txt_499_Narr34.Focus();
        }
        else
        {
            txt_499_Narr35.Focus();
        }
    }
    protected void txt_499_Narr35_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_499_Narr35.Text) == false)
        {
            txt_499_Narr35.Text = "";
            txt_499_Narr35.Focus();
        }
        else
        {
            txt_499_Narr35.Focus();
        }
    }

    //=============================499=====================================================//


    protected void txtReceiver742_TextChanged(object sender, EventArgs e)
    {
        if (Check_AlphaNumric(txtReceiver742.Text) == false)
        {
            txtReceiver742.Text = "";
            txtReceiver742.Focus();
        }
        else if ((txtReceiver742.Text.Length < 8) || (txtReceiver742.Text.Length > 8 && txtReceiver742.Text.Length != 11))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Receiver should be 8 or 11 alphanumeric characters!');", true);
            txtReceiver742.Text = "";
            txtReceiver742.Focus();
        }
        else
        {
            txt_742_ClaimBankRef.Focus();
        }
    }
    protected void txt_742_ClaimBankRef_TextChanged(object sender, EventArgs e)
    {
        CheckTransRef_SwiftType();
        if (Check_X_CharSet(txt_742_ClaimBankRef.Text) == false)
        {
            txt_742_ClaimBankRef.Text = "";
            txt_742_ClaimBankRef.Focus();
        }
        else
        {
            txt_742_DocumCreditNo.Focus();
        }
    }
    protected void txt_742_DocumCreditNo_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_742_DocumCreditNo.Text) == false)
        {
            txt_742_DocumCreditNo.Text = "";
            txt_742_DocumCreditNo.Focus();
        }
        else
        {
            txt_742_Dateofissue.Focus();
        }
    }
    protected void txtIssuingBankAccountnumber_742_TextChanged(object sender, EventArgs e)
    {
        if (Check_Alphabets(txtIssuingBankAccountnumber_742.Text) == false)
        {
            txtIssuingBankAccountnumber_742.Text = "";
            txtIssuingBankAccountnumber_742.Focus();
        }
        else
        {
            txtIssuingBankAccountnumber1_742.Focus();
        }
    }
    protected void txtIssuingBankAccountnumber1_742_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtIssuingBankAccountnumber1_742.Text) == false)
        {
            txtIssuingBankAccountnumber1_742.Text = "";
            txtIssuingBankAccountnumber1_742.Focus();
        }
        else
        {
            if (ddl_Issuingbank_742.SelectedValue == "A")
                txtIssuingBankIdentifiercode_742.Focus();
            else
                txtIssuingBankName_742.Focus();
        }
    }
    protected void txtIssuingBankIdentifiercode_742_TextChanged(object sender, EventArgs e)
    {
        if (Check_AlphaNumric(txtIssuingBankIdentifiercode_742.Text) == false)
        {
            txtIssuingBankIdentifiercode_742.Text = "";
            txtIssuingBankIdentifiercode_742.Focus();
        }
        else
        {
            ddl_742_PrinAmtClmd_Ccy.Focus();
        }
    }
    protected void txtIssuingBankName_742_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtIssuingBankName_742.Text) == false)
        {
            txtIssuingBankName_742.Text = "";
            txtIssuingBankName_742.Focus();
        }
        else
        {
            txtIssuingBankAddress1_742.Focus();
        }
    }
    protected void txtIssuingBankAddress1_742_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtIssuingBankAddress1_742.Text) == false)
        {
            txtIssuingBankAddress1_742.Text = "";
            txtIssuingBankAddress1_742.Focus();
        }
        else
        {
            txtIssuingBankAddress2_742.Focus();
        }
    }
    protected void txtIssuingBankAddress2_742_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtIssuingBankAddress2_742.Text) == false)
        {
            txtIssuingBankAddress2_742.Text = "";
            txtIssuingBankAddress2_742.Focus();
        }
        else
        {
            txtIssuingBankAddress3_742.Focus();
        }
    }
    protected void txtIssuingBankAddress3_742_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtIssuingBankAddress3_742.Text) == false)
        {
            txtIssuingBankAddress3_742.Text = "";
            txtIssuingBankAddress3_742.Focus();
        }
        else
        {
            ddl_742_PrinAmtClmd_Ccy.Focus();
        }
    }
    protected void txt_742_Charges1_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_742_Charges1.Text) == false)
        {
            txt_742_Charges1.Text = "";
            txt_742_Charges1.Focus();
        }
        else
        {
            txt_742_Charges2.Focus();
        }
    }
    protected void txt_742_Charges2_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_742_Charges2.Text) == false)
        {
            txt_742_Charges2.Text = "";
            txt_742_Charges2.Focus();
        }
        else
        {
            txt_742_Charges3.Focus();
        }
    }
    protected void txt_742_Charges3_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_742_Charges3.Text) == false)
        {
            txt_742_Charges3.Text = "";
            txt_742_Charges3.Focus();
        }
        else
        {
            txt_742_Charges4.Focus();
        }
    }
    protected void txt_742_Charges4_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_742_Charges4.Text) == false)
        {
            txt_742_Charges4.Text = "";
            txt_742_Charges4.Focus();
        }
        else
        {
            txt_742_Charges5.Focus();
        }
    }
    protected void txt_742_Charges5_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_742_Charges5.Text) == false)
        {
            txt_742_Charges5.Text = "";
            txt_742_Charges5.Focus();
        }
        else
        {
            txt_742_Charges6.Focus();
        }
    }
    protected void txt_742_Charges6_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_742_Charges6.Text) == false)
        {
            txt_742_Charges6.Text = "";
            txt_742_Charges6.Focus();
        }
        else
        {
            ddlTotalAmtclamd_742.Focus();
        }
    }
    protected void txtAccountwithBankAccountnumber_742_TextChanged(object sender, EventArgs e)
    {
        if (Check_Alphabets(txtAccountwithBankAccountnumber_742.Text) == false)
        {
            txtAccountwithBankAccountnumber_742.Text = "";
            txtAccountwithBankAccountnumber_742.Focus();
        }
        else
        {
            txtAccountwithBankAccountnumber1_742.Focus();
        }
    }
    protected void txtAccountwithBankAccountnumber1_742_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtAccountwithBankAccountnumber1_742.Text) == false)
        {
            txtAccountwithBankAccountnumber1_742.Text = "";
            txtAccountwithBankAccountnumber1_742.Focus();
        }
        else
        {
            if (ddlAccountwithbank_742.SelectedValue == "A")
                txtAccountwithBankIdentifiercode_742.Focus();
            else if (ddlAccountwithbank_742.SelectedValue == "B")
                txtAccountwithBankLocation_742.Focus();
            else if (ddlAccountwithbank_742.SelectedValue == "D")
                txtAccountwithBankName_742.Focus();
        }
    }
    protected void txtAccountwithBankIdentifiercode_742_TextChanged(object sender, EventArgs e)
    {
        if (Check_AlphaNumric(txtAccountwithBankIdentifiercode_742.Text) == false)
        {
            txtAccountwithBankIdentifiercode_742.Text = "";
            txtAccountwithBankIdentifiercode_742.Focus();
        }
        else
        {
            ddlBeneficiarybank_742.Focus();
        }
    }
    protected void txtAccountwithBankLocation_742_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtAccountwithBankLocation_742.Text) == false)
        {
            txtAccountwithBankLocation_742.Text = "";
            txtAccountwithBankLocation_742.Focus();
        }
        else
        {
            ddlBeneficiarybank_742.Focus();
        }
    }
    protected void txtAccountwithBankName_742_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtAccountwithBankName_742.Text) == false)
        {
            txtAccountwithBankName_742.Text = "";
            txtAccountwithBankName_742.Focus();
        }
        else
        {
            txtAccountwithBankAddress1_742.Focus();
        }
    }
    protected void txtAccountwithBankAddress1_742_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtAccountwithBankAddress1_742.Text) == false)
        {
            txtAccountwithBankAddress1_742.Text = "";
            txtAccountwithBankAddress1_742.Focus();
        }
        else
        {
            txtAccountwithBankAddress2_742.Focus();
        }
    }
    protected void txtAccountwithBankAddress2_742_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtAccountwithBankAddress2_742.Text) == false)
        {
            txtAccountwithBankAddress2_742.Text = "";
            txtAccountwithBankAddress2_742.Focus();
        }
        else
        {
            txtAccountwithBankAddress3_742.Focus();
        }
    }
    protected void txtAccountwithBankAddress3_742_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtAccountwithBankAddress3_742.Text) == false)
        {
            txtAccountwithBankAddress3_742.Text = "";
            txtAccountwithBankAddress3_742.Focus();
        }
        else
        {
            ddlBeneficiarybank_742.Focus();
        }
    }
    protected void txtBeneficiaryBankAccountnumber_742_TextChanged(object sender, EventArgs e)
    {
        if (Check_Alphabets(txtBeneficiaryBankAccountnumber_742.Text) == false)
        {
            txtBeneficiaryBankAccountnumber_742.Text = "";
            txtBeneficiaryBankAccountnumber_742.Focus();
        }
        else
        {
            txtBeneficiaryBankAccountnumber1_742.Focus();
        }
    }
    protected void txtBeneficiaryBankAccountnumber1_742_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtBeneficiaryBankAccountnumber1_742.Text) == false)
        {
            txtBeneficiaryBankAccountnumber1_742.Text = "";
            txtBeneficiaryBankAccountnumber1_742.Focus();
        }
        else
        {
            if (ddlBeneficiarybank_742.SelectedValue == "A")
                txtBeneficiaryBankIdentifiercode_742.Focus();
            else
                txtBeneficiaryBankName_742.Focus();
        }
    }
    protected void txtBeneficiaryBankIdentifiercode_742_TextChanged(object sender, EventArgs e)
    {
        if (Check_AlphaNumric(txtBeneficiaryBankIdentifiercode_742.Text) == false)
        {
            txtBeneficiaryBankIdentifiercode_742.Text = "";
            txtBeneficiaryBankIdentifiercode_742.Focus();
        }
        else
        {
            txt_742_SenRecInfo1.Focus();
        }
    }
    protected void txtBeneficiaryBankName_742_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtBeneficiaryBankName_742.Text) == false)
        {
            txtBeneficiaryBankName_742.Text = "";
            txtBeneficiaryBankName_742.Focus();
        }
        else
        {
            txtBeneficiaryBankAddress1_742.Focus();
        }
    }
    protected void txtBeneficiaryBankAddress1_742_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtBeneficiaryBankAddress1_742.Text) == false)
        {
            txtBeneficiaryBankAddress1_742.Text = "";
            txtBeneficiaryBankAddress1_742.Focus();
        }
        else
        {
            txtBeneficiaryBankAddress2_742.Focus();
        }
    }
    protected void txtBeneficiaryBankAddress2_742_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtBeneficiaryBankAddress2_742.Text) == false)
        {
            txtBeneficiaryBankAddress2_742.Text = "";
            txtBeneficiaryBankAddress2_742.Focus();
        }
        else
        {
            txtBeneficiaryBankAddress3_742.Focus();
        }
    }
    protected void txtBeneficiaryBankAddress3_742_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtBeneficiaryBankAddress3_742.Text) == false)
        {
            txtBeneficiaryBankAddress3_742.Text = "";
            txtBeneficiaryBankAddress3_742.Focus();
        }
        else
        {
            txt_742_SenRecInfo1.Focus();
        }
    }
    protected void txt_742_SenRecInfo1_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_742_SenRecInfo1.Text) == false)
        {
            txt_742_SenRecInfo1.Text = "";
            txt_742_SenRecInfo1.Focus();
        }
        else
        {
            txt_742_SenRecInfo2.Focus();
        }
    }
    protected void txt_742_SenRecInfo2_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_742_SenRecInfo2.Text) == false)
        {
            txt_742_SenRecInfo2.Text = "";
            txt_742_SenRecInfo2.Focus();
        }
        else
        {
            txt_742_SenRecInfo3.Focus();
        }
    }
    protected void txt_742_SenRecInfo3_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_742_SenRecInfo3.Text) == false)
        {
            txt_742_SenRecInfo3.Text = "";
            txt_742_SenRecInfo3.Focus();
        }
        else
        {
            txt_742_SenRecInfo4.Focus();
        }
    }
    protected void txt_742_SenRecInfo4_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_742_SenRecInfo4.Text) == false)
        {
            txt_742_SenRecInfo4.Text = "";
            txt_742_SenRecInfo4.Focus();
        }
        else
        {
            txt_742_SenRecInfo5.Focus();
        }
    }
    protected void txt_742_SenRecInfo5_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_742_SenRecInfo5.Text) == false)
        {
            txt_742_SenRecInfo5.Text = "";
            txt_742_SenRecInfo5.Focus();
        }
        else
        {
            txt_742_SenRecInfo6.Focus();
        }
    }
    protected void txt_742_SenRecInfo6_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_742_SenRecInfo6.Text) == false)
        {
            txt_742_SenRecInfo6.Text = "";
            txt_742_SenRecInfo6.Focus();
        }
        else
        {
            txt_742_SenRecInfo6.Focus();
        }
    }

    protected void txtReceiver754_TextChanged(object sender, EventArgs e)
    {
        if (Check_AlphaNumric(txtReceiver754.Text) == false)
        {
            txtReceiver754.Text = "";
            txtReceiver754.Focus();
        }
        else if ((txtReceiver754.Text.Length < 8) || (txtReceiver754.Text.Length > 8 && txtReceiver754.Text.Length != 11))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Receiver should be 8 or 11 alphanumeric characters!');", true);
            txtReceiver754.Text = "";
            txtReceiver754.Focus();
        }
        else
        {
            txt_754_SenRef.Focus();
        }
    }
    protected void txt_754_SenRef_TextChanged(object sender, EventArgs e)
    {
        CheckTransRef_SwiftType();
        if (Check_X_CharSet(txt_754_SenRef.Text) == false)
        {
            txt_754_SenRef.Text = "";
            txt_754_SenRef.Focus();
        }
        else
        {
            txt_754_RelRef.Focus();
        }
    }
    protected void txt_754_RelRef_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_754_RelRef.Text) == false)
        {
            txt_754_RelRef.Text = "";
            txt_754_RelRef.Focus();
        }
        else
        {
            ddlPrinAmtPaidAccNego_754.Focus();
        }
    }
    protected void txt_754_ChargesDeduct1_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_ChargesDeduct1.Text) == false)
        {
            txt_754_ChargesDeduct1.Text = "";
            txt_754_ChargesDeduct1.Focus();
        }
        else
        {
            txt_754_ChargesDeduct2.Focus();
        }
    }
    protected void txt_754_ChargesDeduct2_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_ChargesDeduct2.Text) == false)
        {
            txt_754_ChargesDeduct2.Text = "";
            txt_754_ChargesDeduct2.Focus();
        }
        else
        {
            txt_754_ChargesDeduct3.Focus();
        }
    }
    protected void txt_754_ChargesDeduct3_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_ChargesDeduct3.Text) == false)
        {
            txt_754_ChargesDeduct3.Text = "";
            txt_754_ChargesDeduct3.Focus();
        }
        else
        {
            txt_754_ChargesDeduct4.Focus();
        }
    }
    protected void txt_754_ChargesDeduct4_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_ChargesDeduct4.Text) == false)
        {
            txt_754_ChargesDeduct4.Text = "";
            txt_754_ChargesDeduct4.Focus();
        }
        else
        {
            txt_754_ChargesDeduct5.Focus();
        }
    }
    protected void txt_754_ChargesDeduct5_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_ChargesDeduct5.Text) == false)
        {
            txt_754_ChargesDeduct5.Text = "";
            txt_754_ChargesDeduct5.Focus();
        }
        else
        {
            txt_754_ChargesDeduct6.Focus();
        }
    }
    protected void txt_754_ChargesDeduct6_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_ChargesDeduct6.Text) == false)
        {
            txt_754_ChargesDeduct6.Text = "";
            txt_754_ChargesDeduct6.Focus();
        }
        else
        {
            txt_754_ChargesAdded1.Focus();
        }
    }
    protected void txt_754_ChargesAdded1_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_ChargesAdded1.Text) == false)
        {
            txt_754_ChargesAdded1.Text = "";
            txt_754_ChargesAdded1.Focus();
        }
        else
        {
            txt_754_ChargesAdded2.Focus();
        }
    }
    protected void txt_754_ChargesAdded2_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_ChargesAdded2.Text) == false)
        {
            txt_754_ChargesAdded2.Text = "";
            txt_754_ChargesAdded2.Focus();
        }
        else
        {
            txt_754_ChargesAdded3.Focus();
        }
    }
    protected void txt_754_ChargesAdded3_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_ChargesAdded3.Text) == false)
        {
            txt_754_ChargesAdded3.Text = "";
            txt_754_ChargesAdded3.Focus();
        }
        else
        {
            txt_754_ChargesAdded4.Focus();
        }
    }
    protected void txt_754_ChargesAdded4_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_ChargesAdded4.Text) == false)
        {
            txt_754_ChargesAdded4.Text = "";
            txt_754_ChargesAdded4.Focus();
        }
        else
        {
            txt_754_ChargesAdded5.Focus();
        }
    }
    protected void txt_754_ChargesAdded5_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_ChargesAdded5.Text) == false)
        {
            txt_754_ChargesAdded5.Text = "";
            txt_754_ChargesAdded5.Focus();
        }
        else
        {
            txt_754_ChargesAdded6.Focus();
        }
    }
    protected void txt_754_ChargesAdded6_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_ChargesAdded6.Text) == false)
        {
            txt_754_ChargesAdded6.Text = "";
            txt_754_ChargesAdded6.Focus();
        }
        else
        {
            ddlTotalAmtclamd_754.Focus();
        }
    }

    protected void txtReimbursingBankAccountnumber_754_TextChanged(object sender, EventArgs e)
    {
        if (Check_Alphabets(txtReimbursingBankAccountnumber_754.Text) == false)
        {
            txtReimbursingBankAccountnumber_754.Text = "";
            txtReimbursingBankAccountnumber_754.Focus();
        }
        else
        {
            txtReimbursingBankAccountnumber1_754.Focus();
        }
    }
    protected void txtReimbursingBankAccountnumber1_754_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtReimbursingBankAccountnumber1_754.Text) == false)
        {
            txtReimbursingBankAccountnumber1_754.Text = "";
            txtReimbursingBankAccountnumber1_754.Focus();
        }
        else
        {
            if (ddlReimbursingbank_754.SelectedValue == "A")
                txtReimbursingBankIdentifiercode_754.Focus();
            else if (ddlReimbursingbank_754.SelectedValue == "B")
                txtReimbursingBankLocation_754.Focus();
            else if (ddlReimbursingbank_754.SelectedValue == "D")
                txtReimbursingBankName_754.Focus();

        }
    }
    protected void txtReimbursingBankIdentifiercode_754_TextChanged(object sender, EventArgs e)
    {
        if (Check_AlphaNumric(txtReimbursingBankIdentifiercode_754.Text) == false)
        {
            txtReimbursingBankIdentifiercode_754.Text = "";
            txtReimbursingBankIdentifiercode_754.Focus();
        }
        else
        {
            ddlAccountwithbank_754.Focus();
        }
    }
    protected void txtReimbursingBankLocation_754_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtReimbursingBankLocation_754.Text) == false)
        {
            txtReimbursingBankLocation_754.Text = "";
            txtReimbursingBankLocation_754.Focus();
        }
        else
        {
            ddlAccountwithbank_754.Focus();
        }
    }
    protected void txtReimbursingBankName_754_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtReimbursingBankName_754.Text) == false)
        {
            txtReimbursingBankName_754.Text = "";
            txtReimbursingBankName_754.Focus();
        }
        else
        {
            txtReimbursingBankAddress1_754.Focus();
        }
    }
    protected void txtReimbursingBankAddress1_754_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtReimbursingBankAddress1_754.Text) == false)
        {
            txtReimbursingBankAddress1_754.Text = "";
            txtReimbursingBankAddress1_754.Focus();
        }
        else
        {
            txtReimbursingBankAddress2_754.Focus();
        }
    }
    protected void txtReimbursingBankAddress2_754_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtReimbursingBankAddress2_754.Text) == false)
        {
            txtReimbursingBankAddress2_754.Text = "";
            txtReimbursingBankAddress2_754.Focus();
        }
        else
        {
            txtReimbursingBankAddress3_754.Focus();
        }
    }

    protected void txtReimbursingBankAddress3_754_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtReimbursingBankAddress3_754.Text) == false)
        {
            txtReimbursingBankAddress3_754.Text = "";
            txtReimbursingBankAddress3_754.Focus();
        }
        else
        {
            ddlAccountwithbank_754.Focus();
        }
    }
    protected void txtAccountwithBankAccountnumber_754_TextChanged(object sender, EventArgs e)
    {
        if (Check_Alphabets(txtAccountwithBankAccountnumber_754.Text) == false)
        {
            txtAccountwithBankAccountnumber_754.Text = "";
            txtAccountwithBankAccountnumber_754.Focus();
        }
        else
        {
            txtAccountwithBankAccountnumber1_754.Focus();
        }
    }
    protected void txtAccountwithBankAccountnumber1_754_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtAccountwithBankAccountnumber1_754.Text) == false)
        {
            txtAccountwithBankAccountnumber1_754.Text = "";
            txtAccountwithBankAccountnumber1_754.Focus();
        }
        else
        {
            if (ddlAccountwithbank_754.SelectedValue == "A")
                txtAccountwithBankIdentifiercode_754.Focus();
            else if (ddlAccountwithbank_754.SelectedValue == "B")
                txtAccountwithBankLocation_754.Focus();
            else if (ddlAccountwithbank_754.SelectedValue == "D")
                txtAccountwithBankName_754.Focus();
        }
    }
    protected void txtAccountwithBankIdentifiercode_754_TextChanged(object sender, EventArgs e)
    {
        if (Check_AlphaNumric(txtAccountwithBankIdentifiercode_754.Text) == false)
        {
            txtAccountwithBankIdentifiercode_754.Text = "";
            txtAccountwithBankIdentifiercode_754.Focus();
        }
        else
        {
            ddlBeneficiarybank_754.Focus();
        }
    }
    protected void txtAccountwithBankLocation_754_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtAccountwithBankLocation_754.Text) == false)
        {
            txtAccountwithBankLocation_754.Text = "";
            txtAccountwithBankLocation_754.Focus();
        }
        else
        {
            ddlBeneficiarybank_754.Focus();
        }
    }
    protected void txtAccountwithBankName_754_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtAccountwithBankName_754.Text) == false)
        {
            txtAccountwithBankName_754.Text = "";
            txtAccountwithBankName_754.Focus();
        }
        else
        {
            txtAccountwithBankAddress1_754.Focus();
        }
    }
    protected void txtAccountwithBankAddress1_754_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtAccountwithBankAddress1_754.Text) == false)
        {
            txtAccountwithBankAddress1_754.Text = "";
            txtAccountwithBankAddress1_754.Focus();
        }
        else
        {
            txtAccountwithBankAddress2_754.Focus();
        }
    }
    protected void txtAccountwithBankAddress2_754_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtAccountwithBankAddress2_754.Text) == false)
        {
            txtAccountwithBankAddress2_754.Text = "";
            txtAccountwithBankAddress2_754.Focus();
        }
        else
        {
            txtAccountwithBankAddress3_754.Focus();
        }
    }
    protected void txtAccountwithBankAddress3_754_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtAccountwithBankAddress3_754.Text) == false)
        {
            txtAccountwithBankAddress3_754.Text = "";
            txtAccountwithBankAddress3_754.Focus();
        }
        else
        {
            ddlBeneficiarybank_754.Focus();
        }
    }
    protected void txtBeneficiaryBankAccountnumber_754_TextChanged(object sender, EventArgs e)
    {
        if (Check_Alphabets(txtBeneficiaryBankAccountnumber_754.Text) == false)
        {
            txtBeneficiaryBankAccountnumber_754.Text = "";
            txtBeneficiaryBankAccountnumber_754.Focus();
        }
        else
        {
            txtBeneficiaryBankAccountnumber1_754.Focus();
        }
    }
    protected void txtBeneficiaryBankAccountnumber1_754_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtBeneficiaryBankAccountnumber1_754.Text) == false)
        {
            txtBeneficiaryBankAccountnumber1_754.Text = "";
            txtBeneficiaryBankAccountnumber1_754.Focus();
        }
        else
        {
            if (ddlBeneficiarybank_754.SelectedValue == "A")
                txtBeneficiaryBankIdentifiercode_754.Focus();
            else
                txtBeneficiaryBankName_754.Focus();

        }
    }
    protected void txtBeneficiaryBankIdentifiercode_754_TextChanged(object sender, EventArgs e)
    {
        if (Check_AlphaNumric(txtBeneficiaryBankIdentifiercode_754.Text) == false)
        {
            txtBeneficiaryBankIdentifiercode_754.Text = "";
            txtBeneficiaryBankIdentifiercode_754.Focus();
        }
        else
        {
            txt_754_SenRecInfo1.Focus();
        }
    }
    protected void txtBeneficiaryBankName_754_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtBeneficiaryBankName_754.Text) == false)
        {
            txtBeneficiaryBankName_754.Text = "";
            txtBeneficiaryBankName_754.Focus();
        }
        else
        {
            txtBeneficiaryBankAddress1_754.Focus();
        }
    }
    protected void txtBeneficiaryBankAddress1_754_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtBeneficiaryBankAddress1_754.Text) == false)
        {
            txtBeneficiaryBankAddress1_754.Text = "";
            txtBeneficiaryBankAddress1_754.Focus();
        }
        else
        {
            txtBeneficiaryBankAddress2_754.Focus();
        }
    }
    protected void txtBeneficiaryBankAddress2_754_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtBeneficiaryBankAddress2_754.Text) == false)
        {
            txtBeneficiaryBankAddress2_754.Text = "";
            txtBeneficiaryBankAddress2_754.Focus();
        }
        else
        {
            txtBeneficiaryBankAddress3_754.Focus();
        }
    }
    protected void txtBeneficiaryBankAddress3_754_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txtBeneficiaryBankAddress3_754.Text) == false)
        {
            txtBeneficiaryBankAddress3_754.Text = "";
            txtBeneficiaryBankAddress3_754.Focus();
        }
        else
        {
            txt_754_SenRecInfo1.Focus();
        }
    }
    protected void txt_754_SenRecInfo1_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_SenRecInfo1.Text) == false)
        {
            txt_754_SenRecInfo1.Text = "";
            txt_754_SenRecInfo1.Focus();
        }
        else
        {
            txt_754_SenRecInfo2.Focus();
        }
    }
    protected void txt_754_SenRecInfo2_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_SenRecInfo2.Text) == false)
        {
            txt_754_SenRecInfo2.Text = "";
            txt_754_SenRecInfo2.Focus();
        }
        else
        {
            txt_754_SenRecInfo3.Focus();
        }
    }
    protected void txt_754_SenRecInfo3_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_SenRecInfo3.Text) == false)
        {
            txt_754_SenRecInfo3.Text = "";
            txt_754_SenRecInfo3.Focus();
        }
        else
        {
            txt_754_SenRecInfo4.Focus();
        }
    }
    protected void txt_754_SenRecInfo4_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_SenRecInfo4.Text) == false)
        {
            txt_754_SenRecInfo4.Text = "";
            txt_754_SenRecInfo4.Focus();
        }
        else
        {
            txt_754_SenRecInfo5.Focus();
        }
    }
    protected void txt_754_SenRecInfo5_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_SenRecInfo5.Text) == false)
        {
            txt_754_SenRecInfo5.Text = "";
            txt_754_SenRecInfo5.Focus();
        }
        else
        {
            txt_754_SenRecInfo6.Focus();
        }
    }
    protected void txt_754_SenRecInfo6_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_SenRecInfo6.Text) == false)
        {
            txt_754_SenRecInfo6.Text = "";
            txt_754_SenRecInfo6.Focus();
        }
        else
        {
            txt_754_Narr1.Focus();
        }
    }

    protected void txt_754_Narr1_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_Narr1.Text) == false)
        {
            txt_754_Narr1.Text = "";
            txt_754_Narr1.Focus();
        }
        else
        {
            txt_754_Narr2.Focus();
        }
    }
    protected void txt_754_Narr2_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_Narr2.Text) == false)
        {
            txt_754_Narr2.Text = "";
            txt_754_Narr2.Focus();
        }
        else
        {
            txt_754_Narr3.Focus();
        }
    }
    protected void txt_754_Narr3_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_Narr3.Text) == false)
        {
            txt_754_Narr3.Text = "";
            txt_754_Narr3.Focus();
        }
        else
        {
            txt_754_Narr4.Focus();
        }
    }
    protected void txt_754_Narr4_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_Narr4.Text) == false)
        {
            txt_754_Narr4.Text = "";
            txt_754_Narr4.Focus();
        }
        else
        {
            txt_754_Narr5.Focus();
        }
    }
    protected void txt_754_Narr5_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_Narr5.Text) == false)
        {
            txt_754_Narr5.Text = "";
            txt_754_Narr5.Focus();
        }
        else
        {
            txt_754_Narr6.Focus();
        }
    }
    protected void txt_754_Narr6_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_Narr6.Text) == false)
        {
            txt_754_Narr6.Text = "";
            txt_754_Narr6.Focus();
        }
        else
        {
            txt_754_Narr7.Focus();
        }
    }
    protected void txt_754_Narr7_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_Narr7.Text) == false)
        {
            txt_754_Narr7.Text = "";
            txt_754_Narr7.Focus();
        }
        else
        {
            txt_754_Narr8.Focus();
        }
    }
    protected void txt_754_Narr8_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_Narr8.Text) == false)
        {
            txt_754_Narr8.Text = "";
            txt_754_Narr8.Focus();
        }
        else
        {
            txt_754_Narr9.Focus();
        }
    }
    protected void txt_754_Narr9_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_Narr9.Text) == false)
        {
            txt_754_Narr9.Text = "";
            txt_754_Narr9.Focus();
        }
        else
        {
            txt_754_Narr10.Focus();
        }
    }
    protected void txt_754_Narr10_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_Narr10.Text) == false)
        {
            txt_754_Narr10.Text = "";
            txt_754_Narr10.Focus();
        }
        else
        {
            txt_754_Narr11.Focus();
        }
    }
    protected void txt_754_Narr11_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_Narr11.Text) == false)
        {
            txt_754_Narr11.Text = "";
            txt_754_Narr11.Focus();
        }
        else
        {
            txt_754_Narr12.Focus();
        }
    }
    protected void txt_754_Narr12_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_Narr12.Text) == false)
        {
            txt_754_Narr12.Text = "";
            txt_754_Narr12.Focus();
        }
        else
        {
            txt_754_Narr13.Focus();
        }
    }
    protected void txt_754_Narr13_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_Narr13.Text) == false)
        {
            txt_754_Narr13.Text = "";
            txt_754_Narr13.Focus();
        }
        else
        {
            txt_754_Narr14.Focus();
        }
    }
    protected void txt_754_Narr14_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_Narr14.Text) == false)
        {
            txt_754_Narr14.Text = "";
            txt_754_Narr14.Focus();
        }
        else
        {
            txt_754_Narr15.Focus();
        }
    }
    protected void txt_754_Narr15_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_Narr15.Text) == false)
        {
            txt_754_Narr15.Text = "";
            txt_754_Narr15.Focus();
        }
        else
        {
            txt_754_Narr16.Focus();
        }
    }
    protected void txt_754_Narr16_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_Narr16.Text) == false)
        {
            txt_754_Narr16.Text = "";
            txt_754_Narr16.Focus();
        }
        else
        {
            txt_754_Narr17.Focus();
        }
    }
    protected void txt_754_Narr17_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_Narr17.Text) == false)
        {
            txt_754_Narr17.Text = "";
            txt_754_Narr17.Focus();
        }
        else
        {
            txt_754_Narr17.Focus();
        }
    }
    protected void txt_754_Narr18_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_Narr18.Text) == false)
        {
            txt_754_Narr18.Text = "";
            txt_754_Narr18.Focus();
        }
        else
        {
            txt_754_Narr19.Focus();
        }
    }
    protected void txt_754_Narr19_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_Narr19.Text) == false)
        {
            txt_754_Narr19.Text = "";
            txt_754_Narr19.Focus();
        }
        else
        {
            txt_754_Narr20.Focus();
        }
    }
    protected void txt_754_Narr20_TextChanged(object sender, EventArgs e)
    {
        if (Check_Z_CharSet(txt_754_Narr20.Text) == false)
        {
            txt_754_Narr20.Text = "";
            txt_754_Narr20.Focus();
        }
        else
        {
            txt_754_Narr20.Focus();
        }
    }
    //==================================754 end==========================================//

    protected void txt_199_transRefNo_TextChanged(object sender, EventArgs e)
    {
        CheckTransRef_SwiftType();
        if (Check_X_CharSet(txt_199_transRefNo.Text) == false)
        {
            txt_199_transRefNo.Text = "";
            txt_199_transRefNo.Focus();
        }
        else
        {
            txt_199_RelRef.Focus();
        }
    }
    protected void txt_199_RelRef_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_RelRef.Text) == false)
        {
            txt_199_RelRef.Text = "";
            txt_199_RelRef.Focus();
        }
        else
        {
            txt_199_Narr1.Focus();
        }
    }
    protected void txtReceiver199_TextChanged(object sender, EventArgs e)
    {
        if (Check_AlphaNumric(txtReceiver199.Text) == false)
        {
            txtReceiver199.Text = "";
            txtReceiver199.Focus();
        }
        else if ((txtReceiver199.Text.Length < 8) || (txtReceiver199.Text.Length > 8 && txtReceiver199.Text.Length != 11))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Receiver should be 8 or 11 alphanumeric characters!');", true);
            txtReceiver199.Text = "";
            txtReceiver199.Focus();
        }
        else
        {
            txt_199_transRefNo.Focus();
        }
    }
    protected void txt_199_Narr1_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr1.Text) == false)
        {
            txt_199_Narr1.Text = "";
            txt_199_Narr1.Focus();
        }
        else
        {
            txt_199_Narr2.Focus();
        }
    }
    protected void txt_199_Narr2_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr2.Text) == false)
        {
            txt_199_Narr2.Text = "";
            txt_199_Narr2.Focus();
        }
        else
        {
            txt_199_Narr3.Focus();
        }
    }
    protected void txt_199_Narr3_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr3.Text) == false)
        {
            txt_199_Narr3.Text = "";
            txt_199_Narr3.Focus();
        }
        else
        {
            txt_199_Narr4.Focus();
        }
    }
    protected void txt_199_Narr4_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr4.Text) == false)
        {
            txt_199_Narr4.Text = "";
            txt_199_Narr4.Focus();
        }
        else
        {
            txt_199_Narr5.Focus();
        }
    }
    protected void txt_199_Narr5_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr5.Text) == false)
        {
            txt_199_Narr5.Text = "";
            txt_199_Narr5.Focus();
        }
        else
        {
            txt_199_Narr6.Focus();
        }
    }
    protected void txt_199_Narr6_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr6.Text) == false)
        {
            txt_199_Narr6.Text = "";
            txt_199_Narr6.Focus();
        }
        else
        {
            txt_199_Narr7.Focus();
        }
    }
    protected void txt_199_Narr7_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr7.Text) == false)
        {
            txt_199_Narr7.Text = "";
            txt_199_Narr7.Focus();
        }
        else
        {
            txt_199_Narr8.Focus();
        }
    }
    protected void txt_199_Narr8_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr8.Text) == false)
        {
            txt_199_Narr8.Text = "";
            txt_199_Narr8.Focus();
        }
        else
        {
            txt_199_Narr9.Focus();
        }
    }
    protected void txt_199_Narr9_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr9.Text) == false)
        {
            txt_199_Narr9.Text = "";
            txt_199_Narr9.Focus();
        }
        else
        {
            txt_199_Narr10.Focus();
        }
    }
    protected void txt_199_Narr10_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr10.Text) == false)
        {
            txt_199_Narr10.Text = "";
            txt_199_Narr10.Focus();
        }
        else
        {
            txt_199_Narr11.Focus();
        }
    }
    protected void txt_199_Narr11_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr11.Text) == false)
        {
            txt_199_Narr11.Text = "";
            txt_199_Narr11.Focus();
        }
        else
        {
            txt_199_Narr12.Focus();
        }
    }
    protected void txt_199_Narr12_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr12.Text) == false)
        {
            txt_199_Narr12.Text = "";
            txt_199_Narr12.Focus();
        }
        else
        {
            txt_199_Narr13.Focus();
        }
    }
    protected void txt_199_Narr13_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr13.Text) == false)
        {
            txt_199_Narr13.Text = "";
            txt_199_Narr13.Focus();
        }
        else
        {
            txt_199_Narr14.Focus();
        }
    }
    protected void txt_199_Narr14_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr14.Text) == false)
        {
            txt_199_Narr14.Text = "";
            txt_199_Narr14.Focus();
        }
        else
        {
            txt_199_Narr15.Focus();
        }
    }
    protected void txt_199_Narr15_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr15.Text) == false)
        {
            txt_199_Narr15.Text = "";
            txt_199_Narr15.Focus();
        }
        else
        {
            txt_199_Narr16.Focus();
        }
    }
    protected void txt_199_Narr16_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr16.Text) == false)
        {
            txt_199_Narr16.Text = "";
            txt_199_Narr16.Focus();
        }
        else
        {
            txt_199_Narr17.Focus();
        }
    }
    protected void txt_199_Narr17_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr17.Text) == false)
        {
            txt_199_Narr17.Text = "";
            txt_199_Narr17.Focus();
        }
        else
        {
            txt_199_Narr18.Focus();
        }
    }
    protected void txt_199_Narr18_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr18.Text) == false)
        {
            txt_199_Narr18.Text = "";
            txt_199_Narr18.Focus();
        }
        else
        {
            txt_199_Narr19.Focus();
        }
    }
    protected void txt_199_Narr19_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr19.Text) == false)
        {
            txt_199_Narr19.Text = "";
            txt_199_Narr19.Focus();
        }
        else
        {
            txt_199_Narr20.Focus();
        }
    }
    protected void txt_199_Narr20_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr20.Text) == false)
        {
            txt_199_Narr20.Text = "";
            txt_199_Narr20.Focus();
        }
        else
        {
            txt_199_Narr21.Focus();
        }
    }
    protected void txt_199_Narr21_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr21.Text) == false)
        {
            txt_199_Narr21.Text = "";
            txt_199_Narr21.Focus();
        }
        else
        {
            txt_199_Narr22.Focus();
        }
    }
    protected void txt_199_Narr22_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr22.Text) == false)
        {
            txt_199_Narr22.Text = "";
            txt_199_Narr22.Focus();
        }
        else
        {
            txt_199_Narr23.Focus();
        }
    }
    protected void txt_199_Narr23_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr23.Text) == false)
        {
            txt_199_Narr23.Text = "";
            txt_199_Narr23.Focus();
        }
        else
        {
            txt_199_Narr24.Focus();
        }
    }
    protected void txt_199_Narr24_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr24.Text) == false)
        {
            txt_199_Narr24.Text = "";
            txt_199_Narr24.Focus();
        }
        else
        {
            txt_199_Narr25.Focus();
        }
    }
    protected void txt_199_Narr25_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr25.Text) == false)
        {
            txt_199_Narr25.Text = "";
            txt_199_Narr25.Focus();
        }
        else
        {
            txt_199_Narr26.Focus();
        }
    }
    protected void txt_199_Narr26_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr26.Text) == false)
        {
            txt_199_Narr26.Text = "";
            txt_199_Narr26.Focus();
        }
        else
        {
            txt_199_Narr27.Focus();
        }
    }
    protected void txt_199_Narr27_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr27.Text) == false)
        {
            txt_199_Narr27.Text = "";
            txt_199_Narr27.Focus();
        }
        else
        {
            txt_199_Narr28.Focus();
        }
    }
    protected void txt_199_Narr28_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr28.Text) == false)
        {
            txt_199_Narr28.Text = "";
            txt_199_Narr28.Focus();
        }
        else
        {
            txt_199_Narr29.Focus();
        }
    }
    protected void txt_199_Narr29_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr29.Text) == false)
        {
            txt_199_Narr29.Text = "";
            txt_199_Narr29.Focus();
        }
        else
        {
            txt_199_Narr30.Focus();
        }
    }
    protected void txt_199_Narr30_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr30.Text) == false)
        {
            txt_199_Narr30.Text = "";
            txt_199_Narr30.Focus();
        }
        else
        {
            txt_199_Narr31.Focus();
        }
    }
    protected void txt_199_Narr31_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr31.Text) == false)
        {
            txt_199_Narr31.Text = "";
            txt_199_Narr31.Focus();
        }
        else
        {
            txt_199_Narr32.Focus();
        }
    }
    protected void txt_199_Narr32_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr32.Text) == false)
        {
            txt_199_Narr32.Text = "";
            txt_199_Narr32.Focus();
        }
        else
        {
            txt_199_Narr33.Focus();
        }
    }
    protected void txt_199_Narr33_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr33.Text) == false)
        {
            txt_199_Narr33.Text = "";
            txt_199_Narr33.Focus();
        }
        else
        {
            txt_199_Narr34.Focus();
        }
    }
    protected void txt_199_Narr34_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr34.Text) == false)
        {
            txt_199_Narr34.Text = "";
            txt_199_Narr34.Focus();
        }
        else
        {
            txt_199_Narr35.Focus();
        }
    }
    protected void txt_199_Narr35_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_199_Narr35.Text) == false)
        {
            txt_199_Narr35.Text = "";
            txt_199_Narr35.Focus();
        }
        else
        {
            txt_199_Narr35.Focus();
        }
    }

    protected void txt_299_transRefNo_TextChanged(object sender, EventArgs e)
    {
        CheckTransRef_SwiftType();
        if (Check_X_CharSet(txt_299_transRefNo.Text) == false)
        {
            txt_299_transRefNo.Text = "";
            txt_299_transRefNo.Focus();
        }
        else
        {
            txt_299_RelRef.Focus();
        }
    }
    protected void txt_299_RelRef_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_RelRef.Text) == false)
        {
            txt_299_RelRef.Text = "";
            txt_299_RelRef.Focus();
        }
        else
        {
            txt_299_Narr1.Focus();
        }
    }
    protected void txtReceiver299_TextChanged(object sender, EventArgs e)
    {
        if (Check_AlphaNumric(txtReceiver299.Text) == false)
        {
            txtReceiver299.Text = "";
            txtReceiver299.Focus();
        }
        else if ((txtReceiver299.Text.Length < 8) || (txtReceiver299.Text.Length > 8 && txtReceiver299.Text.Length != 11))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Receiver should be 8 or 11 alphanumeric characters!');", true);
            txtReceiver299.Text = "";
            txtReceiver299.Focus();
        }
        else
        {
            txt_299_transRefNo.Focus();
        }
    }
    protected void txt_299_Narr1_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr1.Text) == false)
        {
            txt_299_Narr1.Text = "";
            txt_299_Narr1.Focus();
        }
        else
        {
            txt_299_Narr2.Focus();
        }
    }
    protected void txt_299_Narr2_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr2.Text) == false)
        {
            txt_299_Narr2.Text = "";
            txt_299_Narr2.Focus();
        }
        else
        {
            txt_299_Narr3.Focus();
        }
    }
    protected void txt_299_Narr3_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr3.Text) == false)
        {
            txt_299_Narr3.Text = "";
            txt_299_Narr3.Focus();
        }
        else
        {
            txt_299_Narr4.Focus();
        }
    }
    protected void txt_299_Narr4_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr4.Text) == false)
        {
            txt_299_Narr4.Text = "";
            txt_299_Narr4.Focus();
        }
        else
        {
            txt_299_Narr5.Focus();
        }
    }
    protected void txt_299_Narr5_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr5.Text) == false)
        {
            txt_299_Narr5.Text = "";
            txt_299_Narr5.Focus();
        }
        else
        {
            txt_299_Narr6.Focus();
        }
    }
    protected void txt_299_Narr6_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr6.Text) == false)
        {
            txt_299_Narr6.Text = "";
            txt_299_Narr6.Focus();
        }
        else
        {
            txt_299_Narr7.Focus();
        }
    }
    protected void txt_299_Narr7_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr7.Text) == false)
        {
            txt_299_Narr7.Text = "";
            txt_299_Narr7.Focus();
        }
        else
        {
            txt_299_Narr8.Focus();
        }
    }
    protected void txt_299_Narr8_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr8.Text) == false)
        {
            txt_299_Narr8.Text = "";
            txt_299_Narr8.Focus();
        }
        else
        {
            txt_299_Narr9.Focus();
        }
    }
    protected void txt_299_Narr9_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr9.Text) == false)
        {
            txt_299_Narr9.Text = "";
            txt_299_Narr9.Focus();
        }
        else
        {
            txt_299_Narr10.Focus();
        }
    }
    protected void txt_299_Narr10_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr10.Text) == false)
        {
            txt_299_Narr10.Text = "";
            txt_299_Narr10.Focus();
        }
        else
        {
            txt_299_Narr11.Focus();
        }
    }
    protected void txt_299_Narr11_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr11.Text) == false)
        {
            txt_299_Narr11.Text = "";
            txt_299_Narr11.Focus();
        }
        else
        {
            txt_299_Narr12.Focus();
        }
    }
    protected void txt_299_Narr12_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr12.Text) == false)
        {
            txt_299_Narr12.Text = "";
            txt_299_Narr12.Focus();
        }
        else
        {
            txt_299_Narr13.Focus();
        }
    }
    protected void txt_299_Narr13_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr13.Text) == false)
        {
            txt_299_Narr13.Text = "";
            txt_299_Narr13.Focus();
        }
        else
        {
            txt_299_Narr14.Focus();
        }
    }
    protected void txt_299_Narr14_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr14.Text) == false)
        {
            txt_299_Narr14.Text = "";
            txt_299_Narr14.Focus();
        }
        else
        {
            txt_299_Narr15.Focus();
        }
    }
    protected void txt_299_Narr15_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr15.Text) == false)
        {
            txt_299_Narr15.Text = "";
            txt_299_Narr15.Focus();
        }
        else
        {
            txt_299_Narr16.Focus();
        }
    }
    protected void txt_299_Narr16_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr16.Text) == false)
        {
            txt_299_Narr16.Text = "";
            txt_299_Narr16.Focus();
        }
        else
        {
            txt_299_Narr17.Focus();
        }
    }
    protected void txt_299_Narr17_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr17.Text) == false)
        {
            txt_299_Narr17.Text = "";
            txt_299_Narr17.Focus();
        }
        else
        {
            txt_299_Narr18.Focus();
        }
    }
    protected void txt_299_Narr18_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr18.Text) == false)
        {
            txt_299_Narr18.Text = "";
            txt_299_Narr18.Focus();
        }
        else
        {
            txt_299_Narr19.Focus();
        }
    }
    protected void txt_299_Narr19_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr19.Text) == false)
        {
            txt_299_Narr19.Text = "";
            txt_299_Narr19.Focus();
        }
        else
        {
            txt_299_Narr20.Focus();
        }
    }
    protected void txt_299_Narr20_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr20.Text) == false)
        {
            txt_299_Narr20.Text = "";
            txt_299_Narr20.Focus();
        }
        else
        {
            txt_299_Narr21.Focus();
        }
    }
    protected void txt_299_Narr21_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr21.Text) == false)
        {
            txt_299_Narr21.Text = "";
            txt_299_Narr21.Focus();
        }
        else
        {
            txt_299_Narr22.Focus();
        }
    }
    protected void txt_299_Narr22_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr22.Text) == false)
        {
            txt_299_Narr22.Text = "";
            txt_299_Narr22.Focus();
        }
        else
        {
            txt_299_Narr23.Focus();
        }
    }
    protected void txt_299_Narr23_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr23.Text) == false)
        {
            txt_299_Narr23.Text = "";
            txt_299_Narr23.Focus();
        }
        else
        {
            txt_299_Narr24.Focus();
        }
    }
    protected void txt_299_Narr24_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr24.Text) == false)
        {
            txt_299_Narr24.Text = "";
            txt_299_Narr24.Focus();
        }
        else
        {
            txt_299_Narr25.Focus();
        }
    }
    protected void txt_299_Narr25_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr25.Text) == false)
        {
            txt_299_Narr25.Text = "";
            txt_299_Narr25.Focus();
        }
        else
        {
            txt_299_Narr26.Focus();
        }
    }
    protected void txt_299_Narr26_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr26.Text) == false)
        {
            txt_299_Narr26.Text = "";
            txt_299_Narr26.Focus();
        }
        else
        {
            txt_299_Narr27.Focus();
        }
    }
    protected void txt_299_Narr27_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr27.Text) == false)
        {
            txt_299_Narr27.Text = "";
            txt_299_Narr27.Focus();
        }
        else
        {
            txt_299_Narr28.Focus();
        }
    }
    protected void txt_299_Narr28_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr28.Text) == false)
        {
            txt_299_Narr28.Text = "";
            txt_299_Narr28.Focus();
        }
        else
        {
            txt_299_Narr29.Focus();
        }
    }
    protected void txt_299_Narr29_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr29.Text) == false)
        {
            txt_299_Narr29.Text = "";
            txt_299_Narr29.Focus();
        }
        else
        {
            txt_299_Narr30.Focus();
        }
    }
    protected void txt_299_Narr30_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr30.Text) == false)
        {
            txt_299_Narr30.Text = "";
            txt_299_Narr30.Focus();
        }
        else
        {
            txt_299_Narr31.Focus();
        }
    }
    protected void txt_299_Narr31_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr31.Text) == false)
        {
            txt_299_Narr31.Text = "";
            txt_299_Narr31.Focus();
        }
        else
        {
            txt_299_Narr32.Focus();
        }
    }
    protected void txt_299_Narr32_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr32.Text) == false)
        {
            txt_299_Narr32.Text = "";
            txt_299_Narr32.Focus();
        }
        else
        {
            txt_299_Narr33.Focus();
        }
    }
    protected void txt_299_Narr33_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr33.Text) == false)
        {
            txt_299_Narr33.Text = "";
            txt_299_Narr33.Focus();
        }
        else
        {
            txt_299_Narr34.Focus();
        }
    }
    protected void txt_299_Narr34_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr34.Text) == false)
        {
            txt_299_Narr34.Text = "";
            txt_299_Narr34.Focus();
        }
        else
        {
            txt_299_Narr35.Focus();
        }
    }
    protected void txt_299_Narr35_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_299_Narr35.Text) == false)
        {
            txt_299_Narr35.Text = "";
            txt_299_Narr35.Focus();
        }
        else
        {
            txt_299_Narr35.Focus();
        }
    }

    protected void txt_999_transRefNo_TextChanged(object sender, EventArgs e)
    {
        CheckTransRef_SwiftType();
        if (Check_X_CharSet(txt_999_transRefNo.Text) == false)
        {
            txt_999_transRefNo.Text = "";
            txt_999_transRefNo.Focus();
        }
        else
        {
            txt_999_RelRef.Focus();
        }
    }
    protected void txt_999_RelRef_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_RelRef.Text) == false)
        {
            txt_999_RelRef.Text = "";
            txt_999_RelRef.Focus();
        }
        else
        {
            txt_999_Narr1.Focus();
        }
    }
    protected void txtReceiver999_TextChanged(object sender, EventArgs e)
    {
        if (Check_AlphaNumric(txtReceiver999.Text) == false)
        {
            txtReceiver999.Text = "";
            txtReceiver999.Focus();
        }
        else if ((txtReceiver999.Text.Length < 8) || (txtReceiver999.Text.Length > 8 && txtReceiver999.Text.Length != 11))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Receiver should be 8 or 11 alphanumeric characters!');", true);
            txtReceiver999.Text = "";
            txtReceiver999.Focus();
        }
        else
        {
            txt_999_transRefNo.Focus();
        }
    }
    protected void txt_999_Narr1_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr1.Text) == false)
        {
            txt_999_Narr1.Text = "";
            txt_999_Narr1.Focus();
        }
        else
        {
            txt_999_Narr2.Focus();
        }
    }
    protected void txt_999_Narr2_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr2.Text) == false)
        {
            txt_999_Narr2.Text = "";
            txt_999_Narr2.Focus();
        }
        else
        {
            txt_999_Narr3.Focus();
        }
    }
    protected void txt_999_Narr3_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr3.Text) == false)
        {
            txt_999_Narr3.Text = "";
            txt_999_Narr3.Focus();
        }
        else
        {
            txt_999_Narr4.Focus();
        }
    }
    protected void txt_999_Narr4_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr4.Text) == false)
        {
            txt_999_Narr4.Text = "";
            txt_999_Narr4.Focus();
        }
        else
        {
            txt_999_Narr5.Focus();
        }
    }
    protected void txt_999_Narr5_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr5.Text) == false)
        {
            txt_999_Narr5.Text = "";
            txt_999_Narr5.Focus();
        }
        else
        {
            txt_999_Narr6.Focus();
        }
    }
    protected void txt_999_Narr6_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr6.Text) == false)
        {
            txt_999_Narr6.Text = "";
            txt_999_Narr6.Focus();
        }
        else
        {
            txt_999_Narr7.Focus();
        }
    }
    protected void txt_999_Narr7_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr7.Text) == false)
        {
            txt_999_Narr7.Text = "";
            txt_999_Narr7.Focus();
        }
        else
        {
            txt_999_Narr8.Focus();
        }
    }
    protected void txt_999_Narr8_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr8.Text) == false)
        {
            txt_999_Narr8.Text = "";
            txt_999_Narr8.Focus();
        }
        else
        {
            txt_999_Narr9.Focus();
        }
    }
    protected void txt_999_Narr9_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr9.Text) == false)
        {
            txt_999_Narr9.Text = "";
            txt_999_Narr9.Focus();
        }
        else
        {
            txt_999_Narr10.Focus();
        }
    }
    protected void txt_999_Narr10_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr10.Text) == false)
        {
            txt_999_Narr10.Text = "";
            txt_999_Narr10.Focus();
        }
        else
        {
            txt_999_Narr11.Focus();
        }
    }
    protected void txt_999_Narr11_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr11.Text) == false)
        {
            txt_999_Narr11.Text = "";
            txt_999_Narr11.Focus();
        }
        else
        {
            txt_999_Narr12.Focus();
        }
    }
    protected void txt_999_Narr12_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr12.Text) == false)
        {
            txt_999_Narr12.Text = "";
            txt_999_Narr12.Focus();
        }
        else
        {
            txt_999_Narr13.Focus();
        }
    }
    protected void txt_999_Narr13_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr13.Text) == false)
        {
            txt_999_Narr13.Text = "";
            txt_999_Narr13.Focus();
        }
        else
        {
            txt_999_Narr14.Focus();
        }
    }
    protected void txt_999_Narr14_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr14.Text) == false)
        {
            txt_999_Narr14.Text = "";
            txt_999_Narr14.Focus();
        }
        else
        {
            txt_999_Narr15.Focus();
        }
    }
    protected void txt_999_Narr15_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr15.Text) == false)
        {
            txt_999_Narr15.Text = "";
            txt_999_Narr15.Focus();
        }
        else
        {
            txt_999_Narr16.Focus();
        }
    }
    protected void txt_999_Narr16_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr16.Text) == false)
        {
            txt_999_Narr16.Text = "";
            txt_999_Narr16.Focus();
        }
        else
        {
            txt_999_Narr17.Focus();
        }
    }
    protected void txt_999_Narr17_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr17.Text) == false)
        {
            txt_999_Narr17.Text = "";
            txt_999_Narr17.Focus();
        }
        else
        {
            txt_999_Narr18.Focus();
        }
    }
    protected void txt_999_Narr18_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr18.Text) == false)
        {
            txt_999_Narr18.Text = "";
            txt_999_Narr18.Focus();
        }
        else
        {
            txt_999_Narr19.Focus();
        }
    }
    protected void txt_999_Narr19_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr19.Text) == false)
        {
            txt_999_Narr19.Text = "";
            txt_999_Narr19.Focus();
        }
        else
        {
            txt_999_Narr20.Focus();
        }
    }
    protected void txt_999_Narr20_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr20.Text) == false)
        {
            txt_999_Narr20.Text = "";
            txt_999_Narr20.Focus();
        }
        else
        {
            txt_999_Narr21.Focus();
        }
    }
    protected void txt_999_Narr21_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr21.Text) == false)
        {
            txt_999_Narr21.Text = "";
            txt_999_Narr21.Focus();
        }
        else
        {
            txt_999_Narr22.Focus();
        }
    }
    protected void txt_999_Narr22_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr22.Text) == false)
        {
            txt_999_Narr22.Text = "";
            txt_999_Narr22.Focus();
        }
        else
        {
            txt_999_Narr23.Focus();
        }
    }
    protected void txt_999_Narr23_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr23.Text) == false)
        {
            txt_999_Narr23.Text = "";
            txt_999_Narr23.Focus();
        }
        else
        {
            txt_999_Narr24.Focus();
        }
    }
    protected void txt_999_Narr24_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr24.Text) == false)
        {
            txt_999_Narr24.Text = "";
            txt_999_Narr24.Focus();
        }
        else
        {
            txt_999_Narr25.Focus();
        }
    }
    protected void txt_999_Narr25_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr25.Text) == false)
        {
            txt_999_Narr25.Text = "";
            txt_999_Narr25.Focus();
        }
        else
        {
            txt_999_Narr26.Focus();
        }
    }
    protected void txt_999_Narr26_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr26.Text) == false)
        {
            txt_999_Narr26.Text = "";
            txt_999_Narr26.Focus();
        }
        else
        {
            txt_999_Narr27.Focus();
        }
    }
    protected void txt_999_Narr27_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr27.Text) == false)
        {
            txt_999_Narr27.Text = "";
            txt_999_Narr27.Focus();
        }
        else
        {
            txt_999_Narr28.Focus();
        }
    }
    protected void txt_999_Narr28_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr28.Text) == false)
        {
            txt_999_Narr28.Text = "";
            txt_999_Narr28.Focus();
        }
        else
        {
            txt_999_Narr29.Focus();
        }
    }
    protected void txt_999_Narr29_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr29.Text) == false)
        {
            txt_999_Narr29.Text = "";
            txt_999_Narr29.Focus();
        }
        else
        {
            txt_999_Narr30.Focus();
        }
    }
    protected void txt_999_Narr30_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr30.Text) == false)
        {
            txt_999_Narr30.Text = "";
            txt_999_Narr30.Focus();
        }
        else
        {
            txt_999_Narr31.Focus();
        }
    }
    protected void txt_999_Narr31_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr31.Text) == false)
        {
            txt_999_Narr31.Text = "";
            txt_999_Narr31.Focus();
        }
        else
        {
            txt_999_Narr32.Focus();
        }
    }
    protected void txt_999_Narr32_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr32.Text) == false)
        {
            txt_999_Narr32.Text = "";
            txt_999_Narr32.Focus();
        }
        else
        {
            txt_999_Narr33.Focus();
        }
    }
    protected void txt_999_Narr33_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr33.Text) == false)
        {
            txt_999_Narr33.Text = "";
            txt_999_Narr33.Focus();
        }
        else
        {
            txt_999_Narr34.Focus();
        }
    }
    protected void txt_999_Narr34_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr34.Text) == false)
        {
            txt_999_Narr34.Text = "";
            txt_999_Narr34.Focus();
        }
        else
        {
            txt_999_Narr35.Focus();
        }
    }
    protected void txt_999_Narr35_TextChanged(object sender, EventArgs e)
    {
        if (Check_X_CharSet(txt_999_Narr35.Text) == false)
        {
            txt_999_Narr35.Text = "";
            txt_999_Narr35.Focus();
        }
        else
        {
            txt_999_Narr35.Focus();
        }
    }
}