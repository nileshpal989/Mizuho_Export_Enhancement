using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;
 
public partial class Reports_EXPORTReports_ViewVoucherLCExpoerAmendent : System.Web.UI.Page
{
     protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Microsoft.Reporting.WebForms.ReportParameter Docno = new Microsoft.Reporting.WebForms.ReportParameter();
            Docno.Name = "docno";
            string docno = "";
             Microsoft.Reporting.WebForms.ReportParameter ToDocno = new Microsoft.Reporting.WebForms.ReportParameter();
            ToDocno.Name = "todocno";
            string todocno = "";
           
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
                Microsoft.Reporting.WebForms.ReportParameter pur = new Microsoft.Reporting.WebForms.ReportParameter();
                pur.Name = "pur";

                // Set the report server URL and report path
                if (Request.QueryString["rptCode"] == "1")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/VoucherExportLCustAmendment";
                    docno = "";
                    todocno = "";
                    pur.Values.Add("0");
                }
                if (Request.QueryString["rptCode"] == "2")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/VoucherExportLCustAmendment";

                      
                    docno = Request.QueryString["rptType"].ToString();
                    todocno = Request.QueryString["rptTypeTo"].ToString();
                    pur.Values.Add("1");

                }


                string frmdate = DateTime.ParseExact(Request.QueryString["frm"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");

                Microsoft.Reporting.WebForms.ReportParameter startdate = new Microsoft.Reporting.WebForms.ReportParameter();
                startdate.Name = "startdate";
                startdate.Values.Add(frmdate);


                Microsoft.Reporting.WebForms.ReportParameter sessiontrue = new Microsoft.Reporting.WebForms.ReportParameter();
                sessiontrue.Name = "sessiontrue";
                sessiontrue.Values.Add("1");

                Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
                Branch.Name = "Branch";
                string branch = Request.QueryString["branch"];
                Branch.Values.Add(branch);
                //if (Session["userName"] != null)
                //{

                //}
                //else
                //{
                //    sessiontrue.Values.Add("0");
                //}

                // Microsoft.Reporting.WebForms.ReportParameter purposecode = new Microsoft.Reporting.WebForms.ReportParameter();
                // purposecode.Name = "purposecode";
                // string pCode = Request.QueryString["pCode"].ToString();
                // purposecode.Values.Add(pCode);
                //Microsoft.Reporting.WebForms.ReportParameter DocNo = new Microsoft.Reporting.WebForms.ReportParameter();
                //DocNo.Name = "DocNo";
                //DocNo.Values.Add(Request.QueryString["rptType"].ToString());

                Docno.Values.Add(docno);
                ToDocno.Values.Add(todocno);
                ReportViewer1.ServerReport.SetParameters(
                   new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, pur, Docno,ToDocno, sessiontrue, Branch });
            }
        }
    }
}