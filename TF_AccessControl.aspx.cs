using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class TF_AccessControl : System.Web.UI.Page
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
                
                hdnModuleID.Value = Session["ModuleID"].ToString();
                fillUser();
                ddlUser.Focus();
                fillgrid();
            }

        }
    }

    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        string access = ddlAccess.SelectedItem.Text;
        if (access != "" && (ddlUser.SelectedItem.Text != "---Select---"))
        {
            fillgrid();
            ddlAccess.Focus();
        }
        else
        {
            GridViewMenuList.Visible = false;
        }
        ddlUser.Focus();
    }

    private void fillUser()
    {
        string _query = "TF_UserList";
        TF_DATA objData = new TF_DATA();

        SqlParameter pSearch = new SqlParameter("@search", SqlDbType.VarChar);
        pSearch.Value = "";

        DataTable dt = objData.getData(_query, pSearch);
        ddlUser.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlUser.DataSource = dt.DefaultView;
            ddlUser.DataTextField = "userName";
            ddlUser.DataValueField = "userName";
            ddlUser.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlUser.Items.Insert(0, li);
    }

    private void fillgrid()
    {
        SqlParameter Accessmenu = new SqlParameter("@Accessmenu", SqlDbType.VarChar);
        Accessmenu.Value = ddlAccess.SelectedItem.Text;
        SqlParameter Module = new SqlParameter("@Module", SqlDbType.VarChar);
        if (hdnModuleID.Value == "IMP")
        {
            Module.Value = "IMP";
        }
        else if (hdnModuleID.Value == "EXP")
        {
            Module.Value = "EXP";
        }
        else if (hdnModuleID.Value == "EBR")
        {
            Module.Value = "EBR";
        }
        else if (hdnModuleID.Value == "EDPMS")
        {
            Module.Value = "EDPMS";
        }

        else if (hdnModuleID.Value == "IDPMS")
        {
            Module.Value = "IDPMS";
        }
        else if (hdnModuleID.Value == "IMPWH")
        {
            Module.Value = "IMPWH";
        }
        else if (hdnModuleID.Value == "R-Return")
        {
            Module.Value = "R-Return";
        }
        string _query = "TF_GetMenuList_new1";
        TF_DATA objData = new TF_DATA();

        DataTable dt = objData.getData(_query, Accessmenu, Module);
        if (dt.Rows.Count > 0)
        {
            GridViewMenuList.DataSource = dt.DefaultView;
            GridViewMenuList.DataBind();
            GridViewMenuList.Visible = true;
            labelMessage.Visible = false;
            SqlParameter pUserName = new SqlParameter("@userName", SqlDbType.VarChar);
            pUserName.Value = ddlUser.Text;
            string _query1 = "TF_GetAccessedMenuList";
            DataTable dt1 = objData.getData(_query1, pUserName);
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i <= dt1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= GridViewMenuList.Rows.Count - 1; j++)
                    {
                        CheckBox chkrow = (CheckBox)GridViewMenuList.Rows[j].FindControl("RowChkAllow");
                        //Label lblAccess = (Label)GridViewMenuList.Rows[j].FindControl("lblAccess");

                        if (dt1.Rows[i]["MenuName"].ToString() == dt.Rows[j]["MenuName"].ToString())
                        {
                            chkrow.Checked = true;
                            //lblAccess.Text = "Access Allowed";
                            //lblAccess.ForeColor = System.Drawing.Color.Blue;
                        }
                    }
                }

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
        else
        {
            GridViewMenuList.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;

        }
    }
    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void HeaderChkAllow_CheckedChanged(object sender, EventArgs e)
    {

        CheckBox chk = (CheckBox)GridViewMenuList.HeaderRow.FindControl("HeaderChkAllow");
        if (chk.Checked)
        {
            for (int i = 0; i < GridViewMenuList.Rows.Count; i++)
            {

                CheckBox chkrow = (CheckBox)GridViewMenuList.Rows[i].FindControl("RowChkAllow");
                //Label lblAccess = (Label)GridViewMenuList.Rows[i].FindControl("lblAccess");
                chkrow.Checked = true;
                //lblAccess.Text = "Access Allowed";
                //lblAccess.ForeColor = System.Drawing.Color.Blue;
            }

        }
        else
        {
            for (int i = 0; i < GridViewMenuList.Rows.Count; i++)
            {

                CheckBox chkrow = (CheckBox)GridViewMenuList.Rows[i].FindControl("RowChkAllow");
                //Label lblAccess = (Label)GridViewMenuList.Rows[i].FindControl("lblAccess");
                chkrow.Checked = false;
                //lblAccess.Text = "Access Denied";
                //lblAccess.ForeColor = System.Drawing.Color.Red;
            }
        }
        chk.Focus();

    }
    protected void RowChkAllow_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkbox = (CheckBox)sender;
        GridViewRow row = (GridViewRow)checkbox.NamingContainer;

        //Label lblAccess = (Label)row.FindControl("lblAccess");

        if (checkbox.Checked == true)
        {
            //lblAccess.Text = "Access Allowed";
            //lblAccess.ForeColor = System.Drawing.Color.Blue;
            checkbox.Focus();
        }
        else
        {
            //lblAccess.Text = "Access Denied";
            //lblAccess.ForeColor = System.Drawing.Color.Red;
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

        if (ddlUser.SelectedIndex < 1)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Select User');", true);
            ddlUser.Focus();
            return;
        }

        TF_DATA objDel = new TF_DATA();
        string _queryDel = "TF_DeleteUserAccess_new1";

        SqlParameter pUserName = new SqlParameter("@userName", SqlDbType.VarChar);
        pUserName.Value = ddlUser.Text;
        SqlParameter Accessmenu = new SqlParameter("@Accessmenu", SqlDbType.VarChar);
        Accessmenu.Value = ddlAccess.SelectedValue.ToString();

        SqlParameter Module = new SqlParameter("@Module", SqlDbType.VarChar);
        if (hdnModuleID.Value == "IMP")
        {
            Module.Value = "IMP";
        }
        else if (hdnModuleID.Value == "EXP")
        {
            Module.Value = "EXP";
        }
        else if (hdnModuleID.Value == "EBR")
        {
            Module.Value = "EBR";
        }
        else if (hdnModuleID.Value == "EDPMS")
        {
            Module.Value = "EDPMS";
        }

        else if (hdnModuleID.Value == "IDPMS")
        {
            Module.Value = "IDPMS";
        }
        else if (hdnModuleID.Value == "IMPWH")
        {
            Module.Value = "IMPWH";
        }
        else if (hdnModuleID.Value == "R-Return")
        {
            Module.Value = "R-Return";
        }
        _result = objDel.SaveDeleteData(_queryDel, pUserName, Accessmenu,Module);

        if (_result == "deleted" || _result == "new")
        {
            TF_DATA objData = new TF_DATA();
            string _query = "TF_UpdateUserAccess_new1";

            Label lblMenu = new Label();
            SqlParameter pMenuName = new SqlParameter("@MenuName", SqlDbType.VarChar);

            for (
                int i = 0; i < GridViewMenuList.Rows.Count; i++)
            {

                CheckBox chkrow = (CheckBox)GridViewMenuList.Rows[i].FindControl("RowChkAllow");
                if (chkrow.Checked == true)
                {

                    lblMenu = (Label)GridViewMenuList.Rows[i].FindControl("lblMenu");

                    pMenuName.Value = lblMenu.Text;

                    _result = objData.SaveDeleteData(_query, pUserName, pMenuName, Accessmenu);
                }

            }
            if (_result == "added")
            {
                //ddlUser.SelectedIndex = 0;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Access Rights Updated.');", true);
            }
            if (_result == "deleted")
            {
                //ddlUser.SelectedIndex = 0;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Access Rights Updated.');", true);
            }

        }
        else
        {
            labelMessage.Text = _result;
        }

    }
    protected void ddlAccess_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUser.SelectedIndex == -1 || ddlUser.SelectedIndex == 0)
        {

            ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "messege", "alert('Select user.')", true);
            GridViewMenuList.Visible = false;
            ddlUser.Focus();
        }
        else
        {
            fillgrid();
            ddlAccess.Focus();
        }
    }

}