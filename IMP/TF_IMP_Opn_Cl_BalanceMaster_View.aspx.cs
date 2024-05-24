using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Web.Security;

public partial class IMP_TF_IMP_Opn_Cl_BalanceMaster_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
            Response.Redirect("~/TF_Login.aspx?Sessionout=yes&Sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                fillGrid();

                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].Trim() == "added")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);
                    }
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMP_Opn_Cl_BalanceMaster.aspx?mode=add", true);
    }
    protected void fillGrid()
    {
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData("TF_IMP_Voucher_Balance_CurrList");
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            //int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            //GridViewGLCode.PageSize = _pageSize;
            GridViewGLCode.DataSource = dt.DefaultView;
            GridViewGLCode.DataBind();
            GridViewGLCode.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;
        }
        else
        {
            GridViewGLCode.Visible = false;
            rowGrid.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
}