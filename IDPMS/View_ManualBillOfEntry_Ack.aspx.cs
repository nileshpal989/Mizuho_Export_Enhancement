using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class IDPMS_View_ManualBillOfEntry_Ack : System.Web.UI.Page
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

                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Mizhuo_AckManual_BOE";

                string brname = Request.QueryString["branchname"].ToString();
                string errorcode = Request.QueryString["ErrorCode"].ToString();
                string todate = Request.QueryString["to"].ToString();
                string fromdate = Request.QueryString["from"].ToString();

                Microsoft.Reporting.WebForms.ReportParameter branchname = new Microsoft.Reporting.WebForms.ReportParameter();
                branchname.Name = "AdCode";
                branchname.Values.Add(brname);


                Microsoft.Reporting.WebForms.ReportParameter Error_code = new Microsoft.Reporting.WebForms.ReportParameter();
                Error_code.Name = "ErrorCode";
                Error_code.Values.Add(errorcode);

                Microsoft.Reporting.WebForms.ReportParameter FromDate = new Microsoft.Reporting.WebForms.ReportParameter();
                FromDate.Name = "fromDate";
                FromDate.Values.Add(fromdate);

                Microsoft.Reporting.WebForms.ReportParameter ToDate = new Microsoft.Reporting.WebForms.ReportParameter();
                ToDate.Name = "toDate";
                ToDate.Values.Add(todate);


                string username = Session["userName"].ToString();
                Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
                user.Name = "user";
                user.Values.Add(username);


                ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { branchname, Error_code, FromDate, ToDate, user });


            }
        }
    }
}