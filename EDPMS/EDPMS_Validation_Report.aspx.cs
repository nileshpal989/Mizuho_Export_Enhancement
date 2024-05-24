using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class EDPMS_EDPMS_Validation_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["frm"] != null)
            {
                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl = new Uri(url);
                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/EDPMS_Bill_Validation";

                Microsoft.Reporting.WebForms.ReportParameter branch = new Microsoft.Reporting.WebForms.ReportParameter();
                branch.Name = "branch";
                if (Request.QueryString["branch"] == "All Branches")
                {
                    branch.Values.Add("All Branches");
                }
                else
                {
                    branch.Values.Add(Request.QueryString["branch"]);
                }

                string frmdate = Request.QueryString["frm"].ToString();
                Microsoft.Reporting.WebForms.ReportParameter fromdate = new Microsoft.Reporting.WebForms.ReportParameter();
                fromdate.Name = "frmDate";
                fromdate.Values.Add(frmdate);

                string tdate = Request.QueryString["to"].ToString();
                Microsoft.Reporting.WebForms.ReportParameter todate = new Microsoft.Reporting.WebForms.ReportParameter();
                todate.Name = "toDate";
                todate.Values.Add(tdate);

                Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
                user.Name = "user";
                user.Values.Add(Session["userName"].ToString());

                ReportViewer1.ServerReport.SetParameters(
                       new Microsoft.Reporting.WebForms.ReportParameter[] { fromdate, todate, branch, user });


            }
        }
    }
}