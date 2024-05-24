using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EDPMS_Reports_View_Reciept_Doc_Ack : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["branchname"] != null)
            {
                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;

                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl = new Uri(url);

                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_EDPMS_Reciecpt_Doc_Ack";

                string brname = Request.QueryString["branchname"].ToString();
                string errorcode = Request.QueryString["ErrorCode"].ToString();

                Microsoft.Reporting.WebForms.ReportParameter branchname = new Microsoft.Reporting.WebForms.ReportParameter();
                branchname.Name = "AdCode";
                branchname.Values.Add(brname);

                
                Microsoft.Reporting.WebForms.ReportParameter Error_code = new Microsoft.Reporting.WebForms.ReportParameter();
                Error_code.Name = "ErrorCode";
                Error_code.Values.Add(errorcode);

             
                string username = Session["userName"].ToString();
                Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
                user.Name = "user";
                user.Values.Add(username);

                ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { branchname, Error_code, user });

            }
        }
    }
}