using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_Mizho_IMP_MasterReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!(Session["username"] == null))
            {
                //Response.Write("<script language='javascript'>this.Close();</script>");
            }
            else
            {
                Response.Redirect("../../TF_Login.aspx");
            }
            if (Request.QueryString["frm"] != null)
            {
                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl = new Uri(url);
                // Set the report server URL and report path
                string Mode=Request.QueryString["Type"].ToString().Trim();
                if (Mode=="F")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/IMP_holiday_rptAuditTrail_FileUpload";
                }
                else
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_rptAuditTrail_Master";

                }
                //string frmdate = DateTime.ParseExact(Request.QueryString["frm"].ToString(), "dd/MM/yyyy", null).ToString("dd/MM/yyyy");
                //string todate = DateTime.ParseExact(Request.QueryString["to"].ToString(), "dd/MM/yyyy", null).ToString("dd/MM/yyyy");

                Microsoft.Reporting.WebForms.ReportParameter startdate = new Microsoft.Reporting.WebForms.ReportParameter();
                startdate.Name = "startdate";
               string Frmdate1 = Request.QueryString["frm"].ToString().Trim();
               startdate.Values.Add(Frmdate1);

                Microsoft.Reporting.WebForms.ReportParameter enddate = new Microsoft.Reporting.WebForms.ReportParameter();
                enddate.Name = "enddate";
               string todate1 = Request.QueryString["to"].ToString().Trim();
               enddate.Values.Add(todate1);

                Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
                Branch.Name = "Branch";
                string Branch1 = Request.QueryString["Branch"];
                Branch.Values.Add(Branch1);

                Microsoft.Reporting.WebForms.ReportParameter userlog = new Microsoft.Reporting.WebForms.ReportParameter();
                userlog.Name = "userlog";
                userlog.Values.Add(Session["userName"].ToString());

                Microsoft.Reporting.WebForms.ReportParameter ModeType1 = new Microsoft.Reporting.WebForms.ReportParameter();
                ModeType1.Name = "ModeType";
                string Mode1 = Request.QueryString["Type"];
                ModeType1.Values.Add(Mode1);
                //
                Microsoft.Reporting.WebForms.ReportParameter menuname = new Microsoft.Reporting.WebForms.ReportParameter();
                menuname.Name = "Menu";
                string menu = Request.QueryString["Menu"];
                menuname.Values.Add(menu);
                //
                Microsoft.Reporting.WebForms.ReportParameter user1 = new Microsoft.Reporting.WebForms.ReportParameter();
                user1.Name = "User";
                string User1 = Request.QueryString["User"];
                user1.Values.Add(User1);

                ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, enddate, userlog, user1, Branch, menuname, ModeType1 });
            }
        }
    }
}