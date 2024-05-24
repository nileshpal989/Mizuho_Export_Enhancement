using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EXPReports_View_rptExportBillRegister : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.QueryString["frm"] != null && Request.QueryString["to"] != null)
            {


                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
                // Set the processing mode for the ReportViewer to Remote
                string doctypevalue = "";
                string custAcvalue = "";
                string OVPvalue = "";
                string OVBvalue = "";
                string Countryvalue = "";
                string Loanvalue = "";
                string CSvalue = "";
                string LCvalue = "";

                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl =
                  new Uri(url);
                // Set the report server URL and report path

                Microsoft.Reporting.WebForms.ReportParameter doctype = new Microsoft.Reporting.WebForms.ReportParameter();
                doctype.Name = "doctype";
                Microsoft.Reporting.WebForms.ReportParameter CustAcNo = new Microsoft.Reporting.WebForms.ReportParameter();
                CustAcNo.Name = "CustAcNo";
                Microsoft.Reporting.WebForms.ReportParameter branch = new Microsoft.Reporting.WebForms.ReportParameter();
                branch.Name = "Branch";
                Microsoft.Reporting.WebForms.ReportParameter OVP = new Microsoft.Reporting.WebForms.ReportParameter();
                OVP.Name = "OVP";
                Microsoft.Reporting.WebForms.ReportParameter OVB = new Microsoft.Reporting.WebForms.ReportParameter();
                OVB.Name = "OVB";
                Microsoft.Reporting.WebForms.ReportParameter Country = new Microsoft.Reporting.WebForms.ReportParameter();
                Country.Name = "Country";
                Microsoft.Reporting.WebForms.ReportParameter Loan = new Microsoft.Reporting.WebForms.ReportParameter();
                Loan.Name = "Loan";
                Microsoft.Reporting.WebForms.ReportParameter CS = new Microsoft.Reporting.WebForms.ReportParameter();
                CS.Name = "CS";
                Microsoft.Reporting.WebForms.ReportParameter LC = new Microsoft.Reporting.WebForms.ReportParameter();
                LC.Name = "LC";


                if (Request.QueryString["branch"] == "1")
                {
                    branch.Values.Add("All Branches");
                }
                else
                {
                    branch.Values.Add(Request.QueryString["branch"]);
                }


                if (Request.QueryString["rptCode"] == "1")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptEXPBillRegister_DocNo";
                    doctypevalue = Request.QueryString["rptDocType"].ToString();
                    Loanvalue = Request.QueryString["rptLoan"].ToString();
                    CSvalue = Request.QueryString["rptCS"].ToString();
                    LCvalue = Request.QueryString["rptLC"].ToString();


                }

                else if (Request.QueryString["rptCode"] == "2")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptEXPBillRegister_Customerwise";
                    doctypevalue = Request.QueryString["rptDocType"].ToString();
                    Loanvalue = Request.QueryString["rptLoan"].ToString();
                    CSvalue = Request.QueryString["rptCS"].ToString();
                    custAcvalue = Request.QueryString["rptType"].ToString();
                    LCvalue = Request.QueryString["rptLC"].ToString();


                }

                else if (Request.QueryString["rptCode"] == "3")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptEXPBillRegister_OverseasPartywise";
                    doctypevalue = Request.QueryString["rptDocType"].ToString();
                    Loanvalue = Request.QueryString["rptLoan"].ToString();
                    CSvalue = Request.QueryString["rptCS"].ToString();
                    OVPvalue = Request.QueryString["rptType"].ToString();
                    LCvalue = Request.QueryString["rptLC"].ToString();

                }
                else if (Request.QueryString["rptCode"] == "4")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptEXPBillRegister_OverseasBankwise";
                    doctypevalue = Request.QueryString["rptDocType"].ToString();
                    Loanvalue = Request.QueryString["rptLoan"].ToString();
                    CSvalue = Request.QueryString["rptCS"].ToString();
                    OVBvalue = Request.QueryString["rptType"].ToString();
                    LCvalue = Request.QueryString["rptLC"].ToString();

                }
                else if (Request.QueryString["rptCode"] == "5")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptEXPBillRegister_Countrywise";
                    doctypevalue = Request.QueryString["rptDocType"].ToString();
                    Loanvalue = Request.QueryString["rptLoan"].ToString();
                    CSvalue = Request.QueryString["rptCS"].ToString();
                    Countryvalue = Request.QueryString["rptType"].ToString();
                    LCvalue = Request.QueryString["rptLC"].ToString();

                }


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


               
                CustAcNo.Values.Add(custAcvalue);
                doctype.Values.Add(doctypevalue);
                OVP.Values.Add(OVPvalue);
                OVB.Values.Add(OVBvalue);
                Country.Values.Add(Countryvalue);
                Loan.Values.Add(Loanvalue);
                CS.Values.Add(CSvalue);
                LC.Values.Add(LCvalue);

                if (Request.QueryString["rptCode"] == "1")
                {
                    ReportViewer1.ServerReport.SetParameters(
                       new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, enddate, branch, user, doctype, Loan,CS,LC });
                }
                else if (Request.QueryString["rptCode"] == "2")
                {
                    ReportViewer1.ServerReport.SetParameters(
                  new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, enddate, branch, user, CustAcNo, doctype, Loan,CS,LC });
                }

                else if (Request.QueryString["rptCode"] == "3")
                {
                    ReportViewer1.ServerReport.SetParameters(
                  new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, enddate, branch, user, OVP, doctype, Loan,CS,LC });
                }
                else if (Request.QueryString["rptCode"] == "4")
                {
                    ReportViewer1.ServerReport.SetParameters(
                  new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, enddate, branch, user, OVB, doctype, Loan,CS,LC });
                }
                else if (Request.QueryString["rptCode"] == "5")
                {
                    ReportViewer1.ServerReport.SetParameters(
                  new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, enddate, branch, user, Country, doctype,Loan,CS,LC });
                }

            }
        }
    }
}