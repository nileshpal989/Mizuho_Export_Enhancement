using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class IDPMS_View_BOE_PenPayExt_Done : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["branch"] != null)
            {
                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;

                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl = new Uri(url);


                string branch1 = Request.QueryString["branch"];
                string fromddate1 = Request.QueryString["fromdate"];
                string todate1 = Request.QueryString["todate"];
                string cust1 = Request.QueryString["cust"];

                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_BOEPayExtDone_Rpt";

                //string brname = Request.QueryString["branch"].ToString();
                Microsoft.Reporting.WebForms.ReportParameter branchname = new Microsoft.Reporting.WebForms.ReportParameter();
                branchname.Name = "Branch";
                branchname.Values.Add(branch1);

                //string brname = Request.QueryString["branchname"].ToString();
                Microsoft.Reporting.WebForms.ReportParameter fromdate = new Microsoft.Reporting.WebForms.ReportParameter();
                fromdate.Name = "fromdate";
                fromdate.Values.Add(fromddate1);

                Microsoft.Reporting.WebForms.ReportParameter todate = new Microsoft.Reporting.WebForms.ReportParameter();
                todate.Name = "todate";
                todate.Values.Add(todate1);

                Microsoft.Reporting.WebForms.ReportParameter cust = new Microsoft.Reporting.WebForms.ReportParameter();
                cust.Name = "CustID";
                cust.Values.Add(cust1);

                string username = Session["userName"].ToString();
                Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
                user.Name = "user";
                user.Values.Add(username);

                ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { branchname, fromdate, todate, user, cust });

            }
        }
    }

}