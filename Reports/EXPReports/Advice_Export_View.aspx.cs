using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EXPReports_Advice_Export_View : System.Web.UI.Page
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
            Microsoft.Reporting.WebForms.ReportParameter custACNo = new Microsoft.Reporting.WebForms.ReportParameter();
            custACNo.Name = "custACNo";

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



                if (Request.QueryString["custACNo"] == "All")
                {
                    // Set the report server URL and report path
                    if (Request.QueryString["rptCode"] == "1")
                    {
                      
                        docno = "";
                        todocno = "";
                        pur.Values.Add("0");
                        custACNo.Values.Add("All");
                    }

                    else if (Request.QueryString["rptCode"] == "2")
                    {
                       


                        docno = Request.QueryString["rptType"].ToString();
                        todocno = Request.QueryString["rptTypeTo"].ToString();
                        pur.Values.Add("1");
                        custACNo.Values.Add("All");
                    }
                }
                else
                {
                    if (Request.QueryString["rptCode"] == "3")
                    {
                        docno = "";
                        todocno = "";
                        pur.Values.Add("2");
                        custACNo.Values.Add(Request.QueryString["custACNo"]);
                    }

                    else if (Request.QueryString["rptCode"] == "4")
                    {
                        docno = Request.QueryString["rptType"].ToString();
                        todocno = Request.QueryString["rptTypeTo"].ToString();
                        pur.Values.Add("1");
                        custACNo.Values.Add(Request.QueryString["custACNo"]);

                    }


                }

                switch (Request.QueryString["Voucher"])
                {
                    case "Interest Advice":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Voucher_Interest_Advice";
                        break;
                    case "Other Bank Charges Advice":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Voucher_OtherBankCharges_Advice";
                        break;
                    case "Commission Advice":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Voucher_Commision_Advice";
                        break;
                    case "Advance Payment Advice":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Voucher_Advance_Payment_Advice";
                        break;
                    case "Delinking Advice":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Voucher_Dilinking_Advice";
                        break;
                    case "Delinking Interest Advice":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Voucher_Dilinking_Interest_Advice";
                        break;  

                }

                string frmdate = DateTime.ParseExact(Request.QueryString["frm"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");

                Microsoft.Reporting.WebForms.ReportParameter startdate = new Microsoft.Reporting.WebForms.ReportParameter();
                startdate.Name = "startdate";
                startdate.Values.Add(frmdate);



                Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
                Branch.Name = "Branch";
                string branch = Request.QueryString["branch"];
                Branch.Values.Add(branch);

                Docno.Values.Add(docno);
                ToDocno.Values.Add(todocno);
                ReportViewer1.ServerReport.SetParameters(
                   new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, pur, Docno, ToDocno, Branch, custACNo });
            }
        }
    }
}