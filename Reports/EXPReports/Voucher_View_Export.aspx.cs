using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EXPReports_Voucher_View_Export : System.Web.UI.Page
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
            Microsoft.Reporting.WebForms.ReportParameter pur = new Microsoft.Reporting.WebForms.ReportParameter();
            pur.Name = "pur";
            Microsoft.Reporting.WebForms.ReportParameter startdate = new Microsoft.Reporting.WebForms.ReportParameter();
            startdate.Name = "startdate";
            Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
            Branch.Name = "Branch";
            string branch = Request.QueryString["branch"];
               

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
                if (Request.QueryString["rptCode"] == "1")
                {
                    //serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/VoucherContLiabLCIssues";
                    docno = "";
                    todocno = "";
                    pur.Values.Add("0");
                }
                if (Request.QueryString["rptCode"] == "2")
                {
                   // serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/VoucherContLiabLCIssues";
                    docno = Request.QueryString["rptType"].ToString();
                    todocno = Request.QueryString["rptTypeTo"].ToString();
                    pur.Values.Add("1");

                }
      
                switch(Request.QueryString["Voucher"])
                {

                    case "Realisation Voucher":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Voucher_Realisation";
                        break;

                    case "Realisation Voucher LBD-BUYERS":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Voucher_Realisation_LBD_Buyers";
                        break;

                    case "Realisation Voucher LBD-SELLERS":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Voucher_Realisation_LBD_Sellers";
                        break;
                    case "Negotiated Collection Realisation Voucher":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Voucher_Negotiated_CollectionRealisation";
                        break;
                    case "Collection Realisation Voucher":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Voucher_Collection_Realisation";
                        break;
                    case "Interest Voucher":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Voucher_Interest";
                        break;
                    case "Delinking Voucher":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Voucher_Dilinking";
                        break;
                    case "Delinking Interest Voucher":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Voucher_Dilinking_Interest";
                        break;
                    case "Other Bank Charges Voucher":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Voucher_OtherBankCharges";
                        break;
                    case "Commission Voucher":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Voucher_Commision";
                        break;
                    case "Advance Payment Voucher":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Voucher_Advance_Payment";
                        break;
                    case "Export Bill Discounted Voucher":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Voucher_Bill_Discounted";
                        break;
                    case "Export Bill Discounted Voucher LBD-BUYERS":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Voucher_Bill_Discounted_LBDbuyers";
                        break;
                    case "Export Bill Discounted Voucher LBD-SELLERS":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Voucher_Bill_Discounted_LBDSellers";
                        break;
                    case "Realisation (TT 18) Voucher":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Voucher_Realisation_TT_18";
                        break;
                    case "Bank Line Transfer Voucher":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Voucher_BankLine_Transfer";
                        break;


                }

                string frmdate = DateTime.ParseExact(Request.QueryString["frm"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                startdate.Values.Add(frmdate);
                Branch.Values.Add(branch);
                Docno.Values.Add(docno);
                ToDocno.Values.Add(todocno);
              
                ReportViewer1.ServerReport.SetParameters(
                   new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, pur, Docno, ToDocno, Branch });
            }
        }
    }
}