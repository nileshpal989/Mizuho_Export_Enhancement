using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_View_rptAuditTrail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["frm"] != null && Request.QueryString["to"] != null)
            {
                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;

                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl = new Uri(url);
                // Set the report server URL and report path

                Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
                Branch.Name = "Branch";
                Microsoft.Reporting.WebForms.ReportParameter Type = new Microsoft.Reporting.WebForms.ReportParameter();
                Type.Name = "ModeType";

                switch (Request.QueryString["Report"])
                {
                    case "Audit Trail":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_rptAuditTrail";
                        break;
                }

                //string frmdate = DateTime.ParseExact(Request.QueryString["frm"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                //string todate = DateTime.ParseExact(Request.QueryString["to"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");

                string frmdate = Request.QueryString["frm"].ToString().Trim();
                string todate = Request.QueryString["to"].ToString().Trim();

                Microsoft.Reporting.WebForms.ReportParameter startdate = new Microsoft.Reporting.WebForms.ReportParameter();
                startdate.Name = "startdate";
                startdate.Values.Add(frmdate);

                Microsoft.Reporting.WebForms.ReportParameter enddate = new Microsoft.Reporting.WebForms.ReportParameter();
                enddate.Name = "enddate";
                enddate.Values.Add(todate);

                Microsoft.Reporting.WebForms.ReportParameter Module = new Microsoft.Reporting.WebForms.ReportParameter();
                Module.Name = "ModuleType";

                string _Module = Request.QueryString["Module"];
                Module.Values.Add(_Module);

                string Branch1 = Request.QueryString["Branch"];
                Branch.Values.Add(Branch1);

                Microsoft.Reporting.WebForms.ReportParameter User = new Microsoft.Reporting.WebForms.ReportParameter();
                User.Name = "User";
                //User.Values.Add(Session["userName"].ToString());

                string Type1 = Request.QueryString["Type"];
                Type.Values.Add(Type1);

                string User1 = Request.QueryString["User"];
                User.Values.Add(User1);

                // ModeType.Values.Add(ModeTypevalue);

                ReportViewer1.ServerReport.SetParameters(
                    new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, enddate, Branch, User, Type, Module });

            }
        }

    }
}