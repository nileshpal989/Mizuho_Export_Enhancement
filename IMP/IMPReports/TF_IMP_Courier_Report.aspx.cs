using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class IMP_IMPReports_TF_IMP_Courier_Report : System.Web.UI.Page
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
                //fillddlBranch();
                //ddlBranch.SelectedIndex = 1;
                //ddlBranch.SelectedValue = Session["userADCode"].ToString();
                //ddlBranch.Enabled = false;
                //txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                //btnCustHelp.Attributes.Add("Onclick", "CustHelp();");
                //txtCust.Attributes.Add("onkeydown", "return CustHelp_F2();");



            }
            Generate.Attributes.Add("onclick", "generateReport();");
        }
    }
}