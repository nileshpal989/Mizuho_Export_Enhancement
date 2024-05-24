using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Reports_EXPORTReports_rpt_Export_BillSettlementAdvice : System.Web.UI.Page
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
            //txtDocumentNo.Attributes.Add("onkeydown", "return DocId(event)");

            if (!IsPostBack)
            {
                ddlBranch.Focus();
                //btnSave.Attributes.Add("onclick", "return validateGenerate();");
                fillddlBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;
                txtDocDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtDocDate_TextChanged(null, null);
                //Doccode.Visible = false;
            }
        }
    }

    public void fillddlBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        //li.Value = "---Select---";
        ListItem li01 = new ListItem();
        li01.Value = "All Branches";
        if (dt.Rows.Count > 0)
        {
            //li.Text = "---Select---";
            li.Text = "All Branches";

            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);

    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
    }
    protected void txtDocDate_TextChanged(object sender, EventArgs e)
    {
        fillddlDocument();
    }

    protected void txtSrNo_TextChanged(object sender, EventArgs e)
    {
        fillddlDocument();
    }

    public void fillddlDocument()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = ddlBranch.SelectedValue;
        SqlParameter p2 = new SqlParameter("@DocumentDate", SqlDbType.VarChar);
        p2.Value = txtDocDate.Text;
        SqlParameter p3 = new SqlParameter("@SrNo", SqlDbType.VarChar);
        p3.Value = txtSrNo.Text;
        string BillType = "";
        if (rbAll.Checked == true)
        {
            BillType = "All";
        }
        else if (rbBLA.Checked == true)
        {
            BillType = "BLA";
        }
        else if (rbBLU.Checked == true)
        {
            BillType = "BLU";
        }
        else if (rbBBA.Checked == true)
        {
            BillType = "BBA";
        }
        else if (rbBBU.Checked == true)
        {
            BillType = "BBU";
        }
        else if (rbBCA.Checked == true)
        {
            BillType = "BCA";
        }
        else if (rbBCU.Checked == true)
        {
            BillType = "BCU";
        }
        SqlParameter p4 = new SqlParameter("@BillType", SqlDbType.VarChar);
        p4.Value = BillType;
        string _query = "TF_EXP_GetSettlementDocumentno";
        DataTable dt = objData.getData(_query, p1, p2, p3,p4);
        ddlDocumentno.Items.Clear();
        ListItem li = new ListItem();
        //li.Value = "---Select---";
        ListItem li01 = new ListItem();
        li01.Value = "All Branches";
        if (dt.Rows.Count > 0)
        {
            //li.Text = "---Select---";
            //li.Text = "All Branches";

            ddlDocumentno.DataSource = dt.DefaultView;
            ddlDocumentno.DataTextField = "Document_No";
            ddlDocumentno.DataValueField = "Document_No";
            ddlDocumentno.DataBind();
        }
        else
            li.Text = "No record(s) found";

        //ddlDocumentno.Items.Insert(0, li);

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtDocDate.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select Documnet Date.');", true);
            txtDocDate.Focus();
        }
        else if (ddlDocumentno.SelectedValue == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select Documnet No.');", true);
            ddlDocumentno.Focus();
        }
        else if (txtSrNo.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select SRNo.');", true);
            ddlDocumentno.Focus();
        }
        else
        {
            string branchcode = "";
            if (ddlBranch.SelectedValue == "6770001")
            {
                branchcode = "792";
            }
            else if (ddlBranch.SelectedValue == "6770002")
            {
                branchcode = "793";
            }
            else if (ddlBranch.SelectedValue == "6770003")
            {
                branchcode = "716";
            }
            else
            {
                branchcode = "717";
            }
            string docno = ddlDocumentno.Text.Trim().Replace("/", "-").Replace("/", "-"); // Assuming you want to replace all slashes with dashes.
            string url = "~/Reports/EXPORTReports/ViewExportBillSettlementAdvice.aspx?DocSrNo=" + docno + "&BranchName=" + branchcode +"&SrNo=" +txtSrNo.Text;
            string script = "window.open('" + ResolveUrl(url) + "','_blank','height=600,width=800,status=no,resizable=no,scrollbars=yes,toolbar=no,location=center,menubar=no,top=20,left=100')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
        }
    }

    protected void All_CheckedChanged(object sender, EventArgs e)
    {
        fillddlDocument();
    }
    protected void rbBLA_CheckedChanged(object sender, EventArgs e)
    {
        fillddlDocument();
    }
    protected void rbBLU_CheckedChanged(object sender, EventArgs e)
    {
        fillddlDocument();
    }
    protected void rbBBA_CheckedChanged(object sender, EventArgs e)
    {
        fillddlDocument();
    }
    protected void rbBBU_CheckedChanged(object sender, EventArgs e)
    {
        fillddlDocument();
    }
    protected void rbBCA_CheckedChanged(object sender, EventArgs e)
    {
        fillddlDocument();
    }
    protected void rbBCU_CheckedChanged(object sender, EventArgs e)
    {
        fillddlDocument();
    }
}