using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EXPORTReports_EXPORT_ViewLCAdvisedAmendment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["frm"] != null && Request.QueryString["to"] != null)
            {


                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
                // Set the processing mode for the ReportViewer to Remote
                //string purvalue = "";
                //string Benfvalue = "";
                //string IssuingBankvalue = "";
               

                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl =
                  new Uri(url);
                // Set the report server URL and report path

                //Microsoft.Reporting.WebForms.ReportParameter pur = new Microsoft.Reporting.WebForms.ReportParameter();
                //pur.Name = "pur";
                //Microsoft.Reporting.WebForms.ReportParameter Benfid = new Microsoft.Reporting.WebForms.ReportParameter();
                //Benfid.Name = "Benfid";
                //Microsoft.Reporting.WebForms.ReportParameter IssuingBankid = new Microsoft.Reporting.WebForms.ReportParameter();
                //IssuingBankid.Name = "IssuingBankid";
                Microsoft.Reporting.WebForms.ReportParameter branch = new Microsoft.Reporting.WebForms.ReportParameter();
                branch.Name = "Branch";
                if (Request.QueryString["branch"] == "1")
                {
                    branch.Values.Add("All Branches");
                }
                else
                {
                    branch.Values.Add(Request.QueryString["branch"]);
                }


                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptExportLCAmendmentAdvice_DocDatewise";
                    //purvalue = "DocDate";


                string frmdate = DateTime.ParseExact(Request.QueryString["frm"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                string todate = DateTime.ParseExact(Request.QueryString["to"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");


                Microsoft.Reporting.WebForms.ReportParameter startdate = new Microsoft.Reporting.WebForms.ReportParameter();
                startdate.Name = "startdate";
                startdate.Values.Add(frmdate);

                Microsoft.Reporting.WebForms.ReportParameter enddate = new Microsoft.Reporting.WebForms.ReportParameter();
                enddate.Name = "enddate";
                enddate.Values.Add(todate);


                Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
                user.Name = "user";
                user.Values.Add(Session["userName"].ToString());

                //------------------------------------------------------


                Microsoft.Reporting.WebForms.ReportParameter sessiontrue = new Microsoft.Reporting.WebForms.ReportParameter();
                sessiontrue.Name = "sessiontrue";
                
                sessiontrue.Values.Add("1");
               

                //pur.Values.Add(purvalue);
                

               
               
                    ReportViewer1.ServerReport.SetParameters(
                       new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, enddate, sessiontrue, branch, user });
                
               
                }
            }
        }
    }
