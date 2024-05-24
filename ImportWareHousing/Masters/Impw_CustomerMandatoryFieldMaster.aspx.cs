using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class ImportWareHousing_Masters_Impw_CustomerMandatoryFieldMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["LoggedUserId"] == null)
        {
            Response.Redirect("~/TF_Log_out.aspx?sessionout=yes&sessionid=" + "", true);
        }



        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                fillddlBranch();
                ddlBranch.SelectedIndex = 1;
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;
                btnCustHelp.Attributes.Add("Onclick", "CustHelp();");
                impcust.Text = "[ Only Import Wearhouse Customer ]";
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
    private void fillgrid()
    {
        string _query = "TF_IMPWH_GetMendatoryMenuList";
        TF_DATA objData = new TF_DATA();
      
        DataTable dt = objData.getData(_query);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridViewMenuList.DataSource = dt.DefaultView;
            GridViewMenuList.DataBind();
            GridViewMenuList.Visible = true;

            SqlParameter CUSTAcc = new SqlParameter("@CustAccNo", SqlDbType.VarChar);
            CUSTAcc.Value = txtcustname.Text;

            //
            //SqlParameter pModule = new SqlParameter("@moduleID", SqlDbType.VarChar);
            //pModule.Value = ddlModule.SelectedItem.ToString();

            //
            string _query1 = "TF_IMPWH_GetAccessedMenuList";
            DataTable dt1 = objData.getData(_query1, CUSTAcc);
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i <= dt1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= GridViewMenuList.Rows.Count - 1; j++)
                    {
                        CheckBox chkrow = (CheckBox)GridViewMenuList.Rows[j].FindControl("RowChkAllow");
                        Label lblAccess = (Label)GridViewMenuList.Rows[j].FindControl("lblAccess");
                        if (dt1.Rows[i]["MenuName"].ToString() == dt.Rows[j]["MenuName"].ToString())
                        {
                            chkrow.Checked = true;
                            lblAccess.Text = "Mandatory";
                            lblAccess.ForeColor = System.Drawing.Color.Blue;
                        }
                    }
                }
            }
            //else
            //    GridViewMenuList.Visible = false;
        }
        else
        {
            GridViewMenuList.Visible = false;
          
        }
    }
    protected void HeaderChkAllow_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)GridViewMenuList.HeaderRow.FindControl("HeaderChkAllow");
        if (chk.Checked)
        {
            for (int i = 0; i < GridViewMenuList.Rows.Count; i++)
            {
                CheckBox chkrow = (CheckBox)GridViewMenuList.Rows[i].FindControl("RowChkAllow");
                Label lblAccess = (Label)GridViewMenuList.Rows[i].FindControl("lblAccess");
                chkrow.Checked = true;
                lblAccess.Text = "Mandatory";
                lblAccess.ForeColor = System.Drawing.Color.Blue;
            }
        }
        else
        {
            for (int i = 0; i < GridViewMenuList.Rows.Count; i++)
            {
                CheckBox chkrow = (CheckBox)GridViewMenuList.Rows[i].FindControl("RowChkAllow");
                Label lblAccess = (Label)GridViewMenuList.Rows[i].FindControl("lblAccess");
                chkrow.Checked = false;
                lblAccess.Text = "No";
                lblAccess.ForeColor = System.Drawing.Color.Red;
            }
        }
        chk.Focus();
    }
    protected void RowChkAllow_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkbox = (CheckBox)sender;
        GridViewRow row = (GridViewRow)checkbox.NamingContainer;
        Label lblAccess = (Label)row.FindControl("lblAccess");
        if (checkbox.Checked == true)
        {
            lblAccess.Text = "Mandatory";
            lblAccess.ForeColor = System.Drawing.Color.Blue;
            checkbox.Focus();
        }
        else
        {
            lblAccess.Text = "No";
            lblAccess.ForeColor = System.Drawing.Color.Red;
            checkbox.Focus();
        }
        CheckBox chk = (CheckBox)GridViewMenuList.HeaderRow.FindControl("HeaderChkAllow");
        int isAllChecked = 0;
        for (int i = 0; i < GridViewMenuList.Rows.Count; i++)
        {
            CheckBox chkrow = (CheckBox)GridViewMenuList.Rows[i].FindControl("RowChkAllow");
            if (chkrow.Checked == true)
                isAllChecked = 1;
            else
            {
                isAllChecked = 0;
                break;
            }
        }
        if (isAllChecked == 1)
            chk.Checked = true;
        else
            chk.Checked = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        if (txtcustname.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Select User');", true);
            txtcustname.Focus();
            return;
        }
        TF_DATA objDel = new TF_DATA();
        string _queryDel = "TF_IMPWH_DeleteCustomerUserAccess";

        SqlParameter pUserName = new SqlParameter("@CustAccNo", SqlDbType.VarChar);
        pUserName.Value = txtcustname.Text;

        


        _result = objDel.SaveDeleteData(_queryDel, pUserName);
        if (_result == "deleted" || _result == "new")
        {
            TF_DATA objData = new TF_DATA();
            string _query = "TF_IMPWH_UpdateCustAccess";
            Label lblMenu = new Label();
            SqlParameter custacc = new SqlParameter("@CustAccNo", SqlDbType.VarChar);
            custacc.Value = txtcustname.Text;
            SqlParameter menuid = new SqlParameter("@MenuName", SqlDbType.VarChar);
            for (int i = 0; i < GridViewMenuList.Rows.Count; i++)
            {
                CheckBox chkrow = (CheckBox)GridViewMenuList.Rows[i].FindControl("RowChkAllow");
                if (chkrow.Checked == true)
                {
                    lblMenu = (Label)GridViewMenuList.Rows[i].FindControl("lblMenu");
                    menuid.Value = lblMenu.Text;
                    _result = objData.SaveDeleteData(_query, custacc, menuid);
                }
            }
            if (_result == "added")
            {
                //ddlUser.SelectedIndex = 0;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Saved.');window.location.reload();", true);
            }
            if (_result == "deleted")
            {
                //ddlUser.SelectedIndex = 0;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');window.location.reload();", true);
            }
        }
        else
        {
            labelMessage.Text = _result;
        }
    }
    protected void txtcustname_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@customerACNo", SqlDbType.VarChar);
        p1.Value = txtcustname.Text.Trim();

        SqlParameter p2 = new SqlParameter("@branch", SqlDbType.VarChar);
        p2.Value = ddlBranch.SelectedValue;
        string _query = "TF_GetCustomerMasterDetails1";


        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            lblcustname.Text = dt.Rows[0]["CUST_NAME"].ToString();
            //impcust.Text = "[ Only Import Wearhouse Customer ]";
            //txtSupplierID.Focus();
            fillgrid();
            impcust.Text = "";
        }
        else
        {
            
            txtcustname.Text = "";
            lblcustname.Text = "";
           // impcust.Text = "";
            txtcustname.Focus();
        }

        //if (txtSupplierID.Text.Trim() != "")
        //{
        //    txtSupplierID_TextChanged(null, null);
        //}
       
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        txtcustname.Text = "";
        lblcustname.Text = "";
        impcust.Text = "[ Only Import Wearhouse Customer ]";
        GridViewMenuList.Visible = false;
    }
}