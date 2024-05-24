using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EXPORTReports_rpt_Inward_Remmitance_Outstanding_statement_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Cust_AccNo"] != null)
            {
                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl = new Uri(url);

                Microsoft.Reporting.WebForms.ReportParameter CustAcNo = new Microsoft.Reporting.WebForms.ReportParameter();
                CustAcNo.Name = "CustAcNo";
                Microsoft.Reporting.WebForms.ReportParameter AsOn_Date = new Microsoft.Reporting.WebForms.ReportParameter();
                AsOn_Date.Name = "AsOn_Date";
                Microsoft.Reporting.WebForms.ReportParameter User = new Microsoft.Reporting.WebForms.ReportParameter();
                User.Name = "User";
                Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
                Branch.Name = "Branch";
                
                //string AsOnDate = DateTime.ParseExact(Request.QueryString["AsOnDate"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");

                if (Request.QueryString["Report"] == "IRM_Outstanding_Statement")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptInwardRemittanceOutstandingStatement";
                }

                CustAcNo.Values.Add(Request.QueryString["Cust_AccNo"].ToString());
                AsOn_Date.Values.Add(Request.QueryString["AsOnDate"].ToString());
                User.Values.Add(Session["userName"].ToString());
                Branch.Values.Add(Request.QueryString["Branch"].ToString());
                ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { CustAcNo, AsOn_Date, Branch, User });
            }
        }
    }
}