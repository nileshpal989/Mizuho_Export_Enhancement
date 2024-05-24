using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;


public partial class Reports_EXPORTReports_ViewCountryWise : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Microsoft.Reporting.WebForms.ReportParameter custid = new Microsoft.Reporting.WebForms.ReportParameter();
            custid.Name = "cur";

            if (Request.QueryString["frm"] != null)
            {

                
                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
                // Set the processing mode for the ReportViewer to Remote

                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl =
                  new Uri(url);
                // Set the report server URL and report path
                Microsoft.Reporting.WebForms.ReportParameter pur = new Microsoft.Reporting.WebForms.ReportParameter();
                pur.Name = "pur";


                if (Request.QueryString["rptCode"] == "1")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptExportContryWiseTF";
                    custid.Values.Add("");
                    pur.Values.Add("0");
                    ReportViewer1.ServerReport.SetParameters(
                   new Microsoft.Reporting.WebForms.ReportParameter[] { custid });
                    pur.Values.Add("1");
                }
                else if (Request.QueryString["rptCode"] == "2")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptExportContryWiseTF";
                    //Microsoft.Reporting.WebForms.ReportParameter custid = new Microsoft.Reporting.WebForms.ReportParameter();
                    //custid.Name = "custid";
                    custid.Values.Add(Request.QueryString["rptType"].ToString());
                    ReportViewer1.ServerReport.SetParameters(
                    new Microsoft.Reporting.WebForms.ReportParameter[] { custid });
                    pur.Values.Add("1");
                }


                string frmdate = DateTime.ParseExact(Request.QueryString["frm"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                Microsoft.Reporting.WebForms.ReportParameter frmDate = new Microsoft.Reporting.WebForms.ReportParameter();
                frmDate.Name = "startdate";
                frmDate.Values.Add(frmdate);
                string todate = DateTime.ParseExact(Request.QueryString["to"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                Microsoft.Reporting.WebForms.ReportParameter enddate = new Microsoft.Reporting.WebForms.ReportParameter();
                enddate.Name = "enddate";
                enddate.Values.Add(todate);




                Microsoft.Reporting.WebForms.ReportParameter sessiontrue = new Microsoft.Reporting.WebForms.ReportParameter();
                sessiontrue.Name = "sessiontrue";
                //if (Session["userName"] != null)
                //{
                sessiontrue.Values.Add("1");
                //}
                //else
                //{
                //    sessiontrue.Values.Add("0");
                //}
                Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
                user.Name = "user";
                user.Values.Add(Session["userName"].ToString());


                Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
                Branch.Name = "Branch";
                string branch = Request.QueryString["branch"];
                Branch.Values.Add(branch);

                ReportViewer1.ServerReport.SetParameters(
                   new Microsoft.Reporting.WebForms.ReportParameter[] { frmDate,enddate, sessiontrue, Branch, pur,user });
            }
        }

    }
}