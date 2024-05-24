using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EXP_EXP_AddEditAcceptedDate : System.Web.UI.Page
{
    #region Initialisation

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("EXP_ViewUpdatingAcceptedDueDate.aspx", true);
                }
                else
                {
                    switch (Request.QueryString["DocPrFx"].Trim())
                    {
                        case "N":
                            lblDocumentType.Text = "Negotiation";
                            break;
                        case "P":
                            lblDocumentType.Text = "Purchase";
                            break;
                        case "D":
                            lblDocumentType.Text = "Discount";
                            break;
                        case "E":
                            lblDocumentType.Text = "EBR";
                            break;
                        case "C":
                            lblDocumentType.Text = "Collection";
                            break;
                        case "M":
                            lblDocumentType.Text = "M-Advance";
                            break;
                        case "B":
                            lblDocumentType.Text = "B-LBD Buyers";
                            break;
                        case "S":
                            lblDocumentType.Text = "S-LBD Sellers";
                            break;
                    }

                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtDocumentNo.Text = Request.QueryString["DocNo"].Trim();
                        txtDocType.Text = Request.QueryString["DocPrFx"].Trim();
                        fillDetails(Request.QueryString["DocNo"].Trim());
                    }
                }
                txtAcceptedDueDate.Focus();
                txtAgencyCommAmt.Attributes.Add("onkeydown", "return validate_Number(event);");
                btnSave.Attributes.Add("onclick", "return ValidateSave();");
                txtAcceptedDueDate.Attributes.Add("onblur", "return checkSysDate(" + txtAcceptedDueDate.ClientID + ");");
                txtACdate.Attributes.Add("onblur", "return checkSysDate(" + txtACdate.ClientID + ");");
            }
        }
    }

    #endregion

    #region Help Codes

    private void fillCustomerCodeDescription()
    {
        lblCustomerDesc.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@custid", SqlDbType.VarChar);
        p1.Value = txtCustAcNo.Text;
        string _query = "TF_rptGetCustomerMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCustomerDesc.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
            if (lblCustomerDesc.Text.Length > 20)
            {
                lblCustomerDesc.ToolTip = lblCustomerDesc.Text;
                lblCustomerDesc.Text = lblCustomerDesc.Text.Substring(0, 20) + "...";
            }
        }
        else
        {
            txtCustAcNo.Text = "";
            lblCustomerDesc.Text = "";
        }

    }

    private void fillOverseasPartyDescription()
    {
        lblOverseasPartyDesc.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@partyID", SqlDbType.VarChar);
        p1.Value = txtOverseasPartyID.Text;
        SqlParameter p2 = new SqlParameter("@type", SqlDbType.VarChar);
        p2.Value = "EXPORT";
        string _query = "TF_GetOverseasPartyMasterDetails";
        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            lblOverseasPartyDesc.Text = dt.Rows[0]["Party_Name"].ToString().Trim();
            //if (lblOverseasPartyDesc.Text.Length > 20)
            //{
            //    lblOverseasPartyDesc.ToolTip = lblOverseasPartyDesc.Text;
            //    lblOverseasPartyDesc.Text = lblOverseasPartyDesc.Text.Substring(0, 20) + "...";
            //}
        }
        else
        {
            txtOverseasPartyID.Text = "";
            lblOverseasPartyDesc.Text = "";
        }

    }

    //private void fillOverseasBankDescription()
    //{
    //    lblOverseasBankDesc.Text = "";
    //    TF_DATA objData = new TF_DATA();

    //    SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
    //    p1.Value = txtOverseasBankID.Text;
    //    string _query = "TF_GetOverseasBankMasterDetails";
    //    DataTable dt = objData.getData(_query, p1);
    //    if (dt.Rows.Count > 0)
    //    {
    //        lblOverseasBankDesc.Text = dt.Rows[0]["BankName"].ToString().Trim();
    //        if (lblOverseasBankDesc.Text.Length > 20)
    //        {
    //            lblOverseasBankDesc.ToolTip = lblOverseasBankDesc.Text;
    //            lblOverseasBankDesc.Text = lblOverseasBankDesc.Text.Substring(0, 20) + "...";
    //        }
    //    }
    //    else
    //    {
    //        txtOverseasBankID.Text = "";
    //        lblOverseasBankDesc.Text = "";
    //    }

    //}

    #endregion

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_ViewUpdatingAcceptedDueDate.aspx", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _script = "";
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
       
        SqlParameter docNumber = new SqlParameter("@docNo", SqlDbType.VarChar);
        docNumber.Value = txtDocumentNo.Text.Trim();

        SqlParameter pAccpDueDate = new SqlParameter("@AccpDueDate", SqlDbType.VarChar);
        pAccpDueDate.Value = txtAcceptedDueDate.Text.Trim();

        SqlParameter pAgencyCommAmt = new SqlParameter("@AgencyCommAmt", SqlDbType.VarChar);
        pAgencyCommAmt.Value = txtAgencyCommAmt.Text.Trim();

        SqlParameter pAgencyCommDate = new SqlParameter("@AgencyCommDate", SqlDbType.VarChar);
        pAgencyCommDate.Value = txtACdate.Text.Trim();

        SqlParameter pAgencyCommRemarks = new SqlParameter("@AgencyCommRemarks", SqlDbType.VarChar);
        pAgencyCommRemarks.Value = txtACremarks.Text.Trim();

        SqlParameter pUser = new SqlParameter("@user", SqlDbType.VarChar);
        pUser.Value = _userName;

        SqlParameter pUploadDate = new SqlParameter("@uploadingdate", SqlDbType.VarChar);
        pUploadDate.Value = _uploadingDate;

        string _query = "TF_EXP_UpdateExportEntryDetails_AcceptedDueDate";
        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData(_query, docNumber, pAccpDueDate,pAgencyCommAmt,pAgencyCommDate,pAgencyCommRemarks, pUser, pUploadDate);

        _script = "";
        if (_result.Substring(0, 5) == "added")
        {
            _script = "window.location='EXP_ViewUpdatingAcceptedDueDate.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                _script = "window.location='EXP_ViewUpdatingAcceptedDueDate.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = _result;
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_ViewUpdatingAcceptedDueDate.aspx", true);
    }

    protected void fillDetails(string _DocNo)
    {
        SqlParameter p1 = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        p1.Value = _DocNo;
        string _query = "TF_EXP_GetExportBillEntryDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            //------------------Document Details---------------------//

            txtCustAcNo.Text = dt.Rows[0]["CustAcNo"].ToString().Trim();
            fillCustomerCodeDescription();
            txtOverseasPartyID.Text = dt.Rows[0]["Overseas_Party_Code"].ToString().Trim();
            fillOverseasPartyDescription();
            //txtOverseasBankID.Text = dt.Rows[0]["Overseas_Bank_Code"].ToString().Trim();
            //fillOverseasBankDescription();

            txtDateRcvd.Text = dt.Rows[0]["Date_Received"].ToString().Trim();
            //txtDateNegotiated.Text = dt.Rows[0]["Date_Negotiated"].ToString().Trim();

            txtBillAmount.Text = dt.Rows[0]["ActBillAmt"].ToString().Trim();
            lblCurrency.Text = dt.Rows[0]["Currency"].ToString().Trim();
            txtDueDate.Text = dt.Rows[0]["Due_Date"].ToString().Trim();

            txtAcceptedDueDate.Text = dt.Rows[0]["Accepted_Due_Date"].ToString().Trim();

            txtAgencyCommAmt.Text = dt.Rows[0]["AgencyCommissionAmt"].ToString().Trim();
            txtACdate.Text = dt.Rows[0]["AgencyCommissionDate"].ToString().Trim();
            txtACremarks.Text = dt.Rows[0]["AgencyCommissionRemarks"].ToString().Trim();
        }
    }
}