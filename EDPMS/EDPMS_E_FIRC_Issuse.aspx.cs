using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EDPMS_EDPMS_E_FIRC_Issuse : System.Web.UI.Page
{
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
                clear();
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("EDPMS_View_E_FIRCIssue.aspx", true);
                }
                else
                {


                    if (Session["userRole"].ToString() == "Supervisor")
                    {
                        btnSave.Enabled = false;
                        lblSupervisormsg.Visible = true;
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        lblSupervisormsg.Visible = false;
                    }
                   
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        btnDocNo.Visible = false;
                        fillDetails(Request.QueryString["FircNo"].Trim());
                    }
                    else
                    {
                        txtYear.Text = Request.QueryString["Year"].ToString();
                        string s= Request.QueryString["Branch"].ToString();
                        ddlBranch.Text =s;
                        ddlBranch.Enabled = false;
                        fillBranch();
                        
                    }

                }







               //fillBranch();
               
                btnDocNo.Attributes.Add("onclick", "return OpenDocNoList('mouseClick');");
                btnSave.Attributes.Add("onclick", "return validation();");
              //  txtfircamt.Attributes.Add("onblur", "return balance(); ");
                txtfirc_issuedate.Attributes.Add("onblur", "return isValidDate(" + txtfirc_issuedate.ClientID + "," + "'FIRC Date'" + " );");
            }
        }
    }

    public void fillDetails(string fircNo)
    {
        SqlParameter firc_No = new SqlParameter("@FIRCNo",SqlDbType.VarChar);
        firc_No.Value = Request.QueryString["FircNo"].Trim();
        DataTable dt = new DataTable();
        TF_DATA objgetData = new TF_DATA();
        dt = objgetData.getData("tf_EDPMS_E_FIRC_GetDetails", firc_No);
        if (dt.Rows.Count > 0)
        { 
            ddlBranch.Text=dt.Rows[0]["BranchCode"].ToString();
            ddlBranch.Enabled = false;
            fillBranch();
            txtYear.Text=dt.Rows[0]["Year"].ToString();
            txtDocumentNo.Text=dt.Rows[0]["DocumentNo"].ToString();
            txtiecode.Text=dt.Rows[0]["IECode"].ToString();
            txtiename.Text=dt.Rows[0]["IEName"].ToString();
            txtSrNo.Text=dt.Rows[0]["Sr_No"].ToString();
            txtfircno.Text=dt.Rows[0]["Firc_No"].ToString();
            txtfirc_issuedate.Text=dt.Rows[0]["Firc_Issuse_Date"].ToString();
            
            txtfircamt.Text=dt.Rows[0]["Firc_Amount"].ToString();
            hdnFircAmt.Value = txtfircamt.Text;
            txtbalance.Text=dt.Rows[0]["Balance_Amount"].ToString();
            hdnBalanceAmount.Value = txtbalance.Text;
            txttotalamt.Text = dt.Rows[0]["Total_Amount"].ToString();
        
        }



    }


    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
       ListItem li = new ListItem();
        //li.Value = "1";
        if (dt.Rows.Count > 0)
        {
           // li.Text = "---Select---";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

       // ddlBranch.Items.Insert(0, li);
        //ddlBranch.SelectedIndex = 1;
        //ddlBranch_SelectedIndexChanged(null, null);
        //fillGrid();
    }
    protected void btnDocNumber_Click(object sender, EventArgs e)
    {
        if (hdnDocNo.Value != "")
        {
            txtDocumentNo.Text = hdnDocNo.Value;
            txtfirc_issuedate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtDocumentNo.Focus();
            CustomerDeatils();
            FIRC_SR_NO();
            TotalAmt();
            fircBalance();
            
           
        }
    }
    public void fircBalance()
    {
        SqlParameter TTRefNo = new SqlParameter("@TTRefNo", SqlDbType.VarChar);
        TTRefNo.Value = txtDocumentNo.Text;
        DataTable dt = new DataTable();
        TF_DATA objData = new TF_DATA();
        dt = objData.getData("TF_GET_Balance_E_FIRC", TTRefNo);
        if (dt.Rows.Count > 0)
        {
            txtbalance.Text = dt.Rows[0]["Balance_Amount"].ToString();
            hdnBalanceAmount.Value = dt.Rows[0]["Balance_Amount"].ToString();
           
            hdnFircAmt.Value =dt.Rows[0]["Firc_Amount"].ToString();
            if (Convert.ToDouble(hdnBalanceAmount.Value)== 0.00)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert(' Balance Amount is Zero You Cant Issuse Any More FIRC');", true);
                btnSave.Enabled = false;
            }

        }
        
    }

    public void FIRC_SR_NO()
    { 
        SqlParameter TTRefNo= new SqlParameter("@TTFRefNo",SqlDbType.VarChar);
        TTRefNo.Value = txtDocumentNo.Text;
        DataTable dt = new DataTable();
        TF_DATA objData = new TF_DATA();
        dt = objData.getData("TF_GET_TTF_RefNo_Sr",TTRefNo);
        if (dt.Rows.Count > 0)
        {
            txtSrNo.Text = dt.Rows[0]["SRNO"].ToString();
            txtfircno.Text = dt.Rows[0]["FIRCNO"].ToString();
        }
    }
    public void TotalAmt()
    {
        SqlParameter TTRefNo = new SqlParameter("@TTFRefNo", SqlDbType.VarChar);
        TTRefNo.Value = txtDocumentNo.Text;
        DataTable dt = new DataTable();
        TF_DATA objData = new TF_DATA();
        dt = objData.getData("TTRefNo_GetTotalAmt", TTRefNo);
        if (dt.Rows.Count > 0)
        {
            txttotalamt.Text = dt.Rows[0]["Amount"].ToString();
            if (txtSrNo.Text == "01")
            {
                txtbalance.Text = dt.Rows[0]["Amount"].ToString();
                hdnBalanceAmount.Value = dt.Rows[0]["Amount"].ToString();
            }
        }
    
    }
    public void clear()
    {
        txtDocumentNo.Text = "";
        txtiecode.Text = "";
        txtiename.Text = "";
        txtSrNo.Text = "";
        txtfircno.Text = "";
        txtfirc_issuedate.Text = "";
        txtfircamt.Text = "";
        txtbalance.Text = "";
        txttotalamt.Text = "";
    }
    public void CustomerDeatils()
    {
        SqlParameter TTRefNo = new SqlParameter("@TTRefNo",SqlDbType.VarChar);
        TTRefNo.Value = txtDocumentNo.Text;
        DataTable dt = new DataTable();
        TF_DATA objData = new TF_DATA();
        dt = objData.getData("TF_GetCustomerDetails", TTRefNo);
        if (dt.Rows.Count > 0)
        {
            txtiecode.Text = dt.Rows[0]["CUST_IE_CODE"].ToString();
            txtiename.Text = dt.Rows[0]["CUST_NAME"].ToString();

        }
        else
        {
            txtiecode.Focus();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EDPMS_View_E_FIRCIssue.aspx", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
       
        SqlParameter p = new SqlParameter("@BranchCode", SqlDbType.VarChar);
        p.Value = ddlBranch.Text;

        SqlParameter p2 = new SqlParameter("@Year", SqlDbType.VarChar);
        p2.Value = txtYear.Text;

        SqlParameter p3 = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        p3.Value = txtDocumentNo.Text;

        SqlParameter p4 = new SqlParameter("@Sr_No", SqlDbType.VarChar);
        p4.Value = txtSrNo.Text;

        SqlParameter p5 = new SqlParameter("@Firc_No",SqlDbType.VarChar);
        p5.Value = txtfircno.Text;

        SqlParameter p6 = new SqlParameter("@Firc_Issuse_Date", SqlDbType.VarChar);
        p6.Value = txtfirc_issuedate.Text;

        SqlParameter p7 = new SqlParameter("@Firc_Amount",SqlDbType.VarChar);
        p7.Value = txtfircamt.Text;

        SqlParameter p8 = new SqlParameter("@Balance_Amount", SqlDbType.VarChar);
        p8.Value = txtbalance.Text;

        SqlParameter p9 = new SqlParameter("@Utlization_Amount", SqlDbType.VarChar);
        p9.Value = 0;

        SqlParameter p10 = new SqlParameter("@Total_Amount", SqlDbType.VarChar);
        p10.Value = txttotalamt.Text;

        SqlParameter p11 = new SqlParameter("@IECode", SqlDbType.VarChar);
        p11.Value = txtiecode.Text;

        SqlParameter p12 = new SqlParameter("@IEName", SqlDbType.VarChar);
        p12.Value = txtiename.Text;

        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        string _result = "";
        string query="TF_EDPMS_E_FIRC_ADD_EDIT";
        TF_DATA objsave = new TF_DATA();
        _result = objsave.SaveDeleteData(query, p, p2, p3,p4,p5,p6,p7,p8,p9,p10,p11,p12);

        string _script = "";
        if (_result == "Added")
        {
            _script = "window.location='EDPMS_View_E_FIRCIssue.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "Updated")
            {
                _script = "window.location='EDPMS_View_E_FIRCIssue.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = _result;
        }

        
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EDPMS_View_E_FIRCIssue.aspx", true);
    }
    protected void txtfircamt_TextChanged(object sender, EventArgs e)
    {
        if(txtfircamt.Text!="")
        {
            if (Request.QueryString["mode"].Trim() != "add")
            {
                SqlParameter p = new SqlParameter("@EFIRC_No", SqlDbType.VarChar);
                p.Value = txtfircno.Text;

                SqlParameter p1 = new SqlParameter("@EFIRC_Amt", SqlDbType.VarChar);
                p1.Value = txtfircamt.Text;

                TF_DATA obgetdate = new TF_DATA();
                DataTable dt = new DataTable();
                btnSave.Enabled = true;
                dt = obgetdate.getData("TF_GetEFIRC_Balance", p, p1);
                if (dt.Rows.Count > 0)
                {
                    txtbalance.Text = dt.Rows[0]["balanceAmt"].ToString();
                    decimal balance_Amt = Convert.ToDecimal(txtbalance.Text);
                    if (balance_Amt < 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert(' Balance Amount is less Than Zero.');", true);
                        btnSave.Enabled = false;
                    
                    }


                }

            }
            else
            {
                SqlParameter p = new SqlParameter("@DoumentNo", SqlDbType.VarChar);
                p.Value = txtDocumentNo.Text;

                SqlParameter p1 = new SqlParameter("@FircAmt", SqlDbType.VarChar);
                p1.Value = txtfircamt.Text;

                TF_DATA obgetdate = new TF_DATA();
                DataTable dt = new DataTable();
                btnSave.Enabled = true;
                dt = obgetdate.getData("TF_GetEFIRC_Balance1", p, p1);
                if (dt.Rows.Count > 0)
                {
                    txtbalance.Text = dt.Rows[0]["BalanceAmt"].ToString();
                    decimal balance_Amt = Convert.ToDecimal(txtbalance.Text);
                    if (balance_Amt < 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert(' Balance Amount is less Than Zero.');", true);
                        btnSave.Enabled = false;

                    }
                }
            }
        }

    }
}