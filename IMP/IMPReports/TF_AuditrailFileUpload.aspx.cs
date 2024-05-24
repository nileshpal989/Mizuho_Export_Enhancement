using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class IMP_IMPReports_TF_AuditrailFileUpload : System.Web.UI.Page
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
                //btnUserList.Attributes.Add("onclick", "return OpenUserList();");
                btnSave.Attributes.Add("onclick", "return Generate();");
               
                rdbAlluser.Checked = true;
                Table1.Visible = false;

                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
              
            }

        }
    }
    protected void rdbAlluser_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbAlluser.Checked == true)
        {
            Table1.Visible = false;
            txtusername.Text = "";
            //txtUserName.Text = "";
        }
    }
    protected void rdbSelecteduser_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbSelecteduser.Checked == true)
        {
            Table1.Visible = true;
            rdbAlluser.Checked = false;
        }
    }
}