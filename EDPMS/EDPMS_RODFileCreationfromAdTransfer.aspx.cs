using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EDPMS_EDPMS_RODFileCreationfromAdTransfer : System.Web.UI.Page
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
                //txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;
                txtFromDate.Focus();
                //ddlBranch.Focus();
            }
        }
    }
    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        //ListItem li = new ListItem();
        //li.Value = "All Branches";

        if (dt.Rows.Count > 0)
        {
            //li.Text = "All Branches";

            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        //else
        //    li.Text = "No record(s) found";

        //ddlBranch.Items.Insert(0, li);

    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        TF_DATA objValid = new TF_DATA();
        SqlParameter v1 = new SqlParameter("@branch", SqlDbType.VarChar);
        v1.Value = ddlBranch.Text.ToString().Trim();

        SqlParameter v2 = new SqlParameter("@frmDate", SqlDbType.VarChar);
        v2.Value = txtFromDate.Text.ToString();

        SqlParameter v3 = new SqlParameter("@toDate", SqlDbType.VarChar);
        v3.Value = txtToDate.Text.ToString();

        string _qryValid = "TF_EDPMS_OtherADData";
        string dtv = objValid.SaveDeleteData(_qryValid, v1, v2, v3);
        string _qryValidate = "TF_EDPMS_GenerateData_Validation";


        SqlParameter v4 = new SqlParameter("@type", SqlDbType.VarChar);
        v4.Value = "Docbill";

        DataTable dt = objValid.getData(_qryValidate, v1, v2, v3, v4);

        if (dt.Rows.Count > 0)
        {
            string script = "window.open('EDPMS_Validation_Report.aspx?Branch=" + ddlBranch.SelectedItem.Text + "&frm=" + txtFromDate.Text + "&To=" + txtToDate.Text + "&type=" + "DocBill" + "','_blank','height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
        }
        labelMessage.Text = dtv;
    }
}