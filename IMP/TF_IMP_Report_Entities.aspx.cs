using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class IMP_TF_IMP_Report_Entities : System.Web.UI.Page
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
                if (Request.QueryString["mode"] == null)
                {
                    TF_DATA objData = new TF_DATA();
                    DataTable dt = new DataTable();
                    dt = objData.getData("TF_IMP_ReportEntity_Details");
                    if (dt.Rows.Count > 0)
                    {
                        txt_Ctrl_Dept.Text = dt.Rows[0]["Ctrl_Dept"].ToString();
                        txt_Ctrl_Person.Text = dt.Rows[0]["Ctrl_Person"].ToString();
                        txt_BalChecker.Text = dt.Rows[0]["Bal_Checker"].ToString();
                    }
                    else
                    {
                        clear();
                    }
                }
            }
        }
    }
    private void clear()
    {
        txt_Ctrl_Dept.Text = "";
        txt_Ctrl_Person.Text = "";
        txt_BalChecker.Text = "";
        txt_Ctrl_Dept.Focus();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IMP/IMP_Main.aspx", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string result = "";
        TF_DATA objSave = new TF_DATA();

        SqlParameter Ctrl_Dept = new SqlParameter("@Ctrl_Dept", txt_Ctrl_Dept.Text.Trim());
        SqlParameter Ctrl_Person = new SqlParameter("@Ctrl_Person", txt_Ctrl_Person.Text.Trim());
        SqlParameter Bal_Checker = new SqlParameter("@Bal_Checker", txt_BalChecker.Text.Trim());
        SqlParameter Username = new SqlParameter("@Username", Session["userName"].ToString().Trim());
        SqlParameter AddedDate = new SqlParameter("@AddedDate", SqlDbType.VarChar);
        AddedDate.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        result = objSave.SaveDeleteData("TF_IMP_ReportEntity_AddUpdate", Ctrl_Dept, Ctrl_Person, Bal_Checker, Username, AddedDate);

        labelMessage.Text = "Record "+result;
    }
}
