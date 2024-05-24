using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class EDPMS_EDPMS_File_Creation : System.Web.UI.Page
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
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;
                txtFromDate.Focus();
                txtFromDate.Attributes.Add("onblur", "return isValidDate(" + txtFromDate.ClientID + "," + "'From Date.'" + ");");
                txtToDate.Attributes.Add("onblur", "return isValidDate(" + txtToDate.ClientID + "," + "'To Date.'" + ");");
                btnCreate.Attributes.Add("onclick", "return validateSave();");
            }
        }
    }
    public void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", "");
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li01 = new ListItem();
        li01.Value = "All Branches";
        if (dt.Rows.Count > 0)
        {
            li01.Text = "All Branches";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li01.Text = "No record(s) found";
        ddlBranch.Items.Insert(0, li01);
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        TF_EDPMS_Data objTrans = new TF_EDPMS_Data();
        SqlParameter v1 = new SqlParameter("@branch", SqlDbType.VarChar);
        v1.Value = ddlBranch.Text.ToString().Trim();

        SqlParameter v2 = new SqlParameter("@frmDate", SqlDbType.VarChar);
        v2.Value = txtFromDate.Text.ToString();

        SqlParameter v3 = new SqlParameter("@toDate", SqlDbType.VarChar);
        v3.Value = txtToDate.Text.ToString();

        string _qryValid = "";
        if (rdbDocBill.Checked == true)
        {
            rdbRealised.Checked = false;
            _qryValid = "TF_EDPMS_DATA_TRANSFER_DocBill";
        }
        else
        {
            rdbDocBill.Checked = false;
            _qryValid = "TF_EDPMS_DATA_TRANSFER";
        }
        string dtv = objTrans.SaveDeleteData(_qryValid, v1, v2, v3);
        labelMessage.Text = dtv;

        string _qryValidate = "TF_EDPMS_GenerateData_Validation";
        TF_DATA objValid = new TF_DATA();
        DataTable dt = objValid.getData(_qryValidate, v1, v2, v3);
        if (dt.Rows.Count > 0)
        {
            string script = "window.open('EDPMS_Validation_Report.aspx?Branch=" + ddlBranch.SelectedItem.Text + "&frm=" + txtFromDate.Text + "&To=" + txtToDate.Text + "','_blank','height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
        }
    }
}
