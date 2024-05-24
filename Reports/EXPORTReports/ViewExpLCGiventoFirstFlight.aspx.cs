using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EXPORTReports_ViewExpLCGiventoFirstFlight : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            //if (Request.QueryString["frm"] != null)
            //{
            try
            {
                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl =
                  new Uri(url);

                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptExpLCGiventoFirstFlight";


                string frmdate = DateTime.ParseExact(Request.QueryString["frm"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                Microsoft.Reporting.WebForms.ReportParameter frmDate = new Microsoft.Reporting.WebForms.ReportParameter();
                frmDate.Name = "frmDate";
                frmDate.Values.Add(frmdate);
                //frmDate.Values.Add("2013/04/30");
                string todate = DateTime.ParseExact(Request.QueryString["to"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                Microsoft.Reporting.WebForms.ReportParameter toDate = new Microsoft.Reporting.WebForms.ReportParameter();
                toDate.Name = "toDate";
                toDate.Values.Add(todate);

                Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
                Branch.Name = "branch";
                string branch = Request.QueryString["branch"].ToString();
                Branch.Values.Add(branch);

                Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
                user.Name = "user";
                string u = Session["userName"].ToString();
                user.Values.Add(u);
                //user.Values.Add("a");

                ReportViewer1.ServerReport.SetParameters(
                    new Microsoft.Reporting.WebForms.ReportParameter[] { frmDate, toDate, Branch, user });
            }
            catch (Exception ex)
            { }

            //serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptPCFC_ListofPCFCProposals";

            //ReportViewer1.ServerReport.SetParameters(
            //  new Microsoft.Reporting.WebForms.ReportParameter[] { frmDate,sessiontrue, Branch, pur })
        }
    }

}
