using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TF_ModuleSelection : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("~/TF_Login.aspx", true);
        }
        else
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
            }


            switch (Request.QueryString["type"].ToString())
            {
                case "1":
                    btnEXP_Click(null, null);
                    break;
                case "2":
                    btnEBR_Click(null, null);
                    break;
                case "3":
                    btnXOS_Click(null, null);
                    break;
                case "4":
                    btnEDPMS_Click(null, null);
                    break;
                case "5":
                    btnIDPMS_Click(null, null);
                    break;
                case "6":
                    btnRreturn_Click(null, null);
                    break;
                case "7":
                    btnImportWarehousing_Click(null, null);
                    break;
                case "8":
                    btnImportAutomation_Click(null, null);
                    break;
            }

        }
    }
    protected void btnEXP_Click(object sender, EventArgs e)
    {
        Session["ModuleID"] = "EXP";
        Response.Redirect("~/EXP/EXP_Main.aspx", true);
    }
    protected void btnXOS_Click(object sender, EventArgs e)
    {
        Session["ModuleID"] = "XOS";
        Response.Redirect("~/XOS/XOS_Main.aspx", true);
    }
    protected void btnEBR_Click(object sender, EventArgs e)
    {
        Session["ModuleID"] = "EBR";
        Response.Redirect("~/EBR/EBR_Main.aspx", true);
    }
    protected void signout_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TF_Log_Out.aspx", true);
    }
    protected void btnEDPMS_Click(object sender, EventArgs e)
    {
        Session["ModuleID"] = "EDPMS";
        Response.Redirect("~/EDPMS/EDPMS_Main.aspx", true);
    }
    protected void btnIDPMS_Click(object sender, EventArgs e)
    {
        Session["ModuleID"] = "IDPMS";
        Response.Redirect("~/IDPMS/IDPMS_Main.aspx", true);
    }
    protected void btnRreturn_Click(object sender, EventArgs e)
    {
        Session["ModuleID"] = "RET";
        Response.Redirect("~/RRETURN/Ret_Selction.aspx", true);
    }
    protected void btnImportWarehousing_Click(object sender, EventArgs e)
    {
        Session["ModuleID"] = "ImportWareHousing";
        Response.Redirect("~/ImportWareHousing/Import_Warehousing_Main.aspx", true);
    }
    protected void btnImportAutomation_Click(object sender, EventArgs e)
    {
        Session["ModuleID"] = "IMP";
        Response.Redirect("IMP/IMP_Main.aspx", true);
    }
    protected void btnSwiftSTP_Click(object sender, EventArgs e)
    {
        Session["ModuleID"] = "STP";
        Response.Redirect("IMP/TF_Swift_STP_Main.aspx", true);
    }
}