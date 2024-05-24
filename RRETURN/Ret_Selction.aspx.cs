using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
public partial class RRETURN_Ret_Selction : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime nowDate = System.DateTime.Now;
            if (Session["LoggedUserId"] != null)
            {
                lblUserName.Text = "Welcome, " + Session["userName"].ToString().Trim();
                lblRole.Text = "| Role: " + Session["userRole"].ToString().Trim();
                lblTime.Text = nowDate.ToLongDateString();
            }
            btncalendar_FromDate.Focus();
        }
        btncalendar_FromDate.Focus();
        txtFromDate.Attributes.Add("onblur", "return ValidDates();");
        btnSave.Attributes.Add("onclick", "return ValidDates();");
    }
    protected void signout_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TF_Log_Out.aspx", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Session["FrRelDt"] = txtFromDate.Text;
        Session["ToRelDt"] = txtToDate.Text;
        //Session["ModuleID"] = "RET";
        Session["ModuleID"] = "R-Return";
        Response.Redirect("~/RReturn/Ret_Main.aspx", true);
    }
}