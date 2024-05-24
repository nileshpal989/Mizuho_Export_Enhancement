using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EXPReports_Export_Report_View_BillOustanding : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.QueryString["frm"] != null )
            {


                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
                // Set the processing mode for the ReportViewer to Remote
                string doctypevalue = "";
                string OVPvalue = "";
                string OVBvalue = "";
                string Loanvalue = "";
                string LCvalue = "";
                string BillType = "";
                string type = "";
                string unaccepted = "";
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
                Microsoft.Reporting.WebForms.ReportParameter Loan = new Microsoft.Reporting.WebForms.ReportParameter();
                Loan.Name = "Loan";
                Microsoft.Reporting.WebForms.ReportParameter LC = new Microsoft.Reporting.WebForms.ReportParameter();
                LC.Name = "LC";
                Microsoft.Reporting.WebForms.ReportParameter billtype = new Microsoft.Reporting.WebForms.ReportParameter();
                billtype.Name = "billtype";
                Microsoft.Reporting.WebForms.ReportParameter Type = new Microsoft.Reporting.WebForms.ReportParameter();
                Type.Name = "Type";
                Microsoft.Reporting.WebForms.ReportParameter Unaccepted = new Microsoft.Reporting.WebForms.ReportParameter();
                Unaccepted.Name = "unaccepted";

                if (Request.QueryString["branch"] == "All Branches")
                {
                    branch.Values.Add("All");
                }
                else
                {
                    branch.Values.Add(Request.QueryString["branch"]);
                }


                if (Request.QueryString["rptCode"] == "1")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Export_Report_BillOutStanding";
                    doctypevalue = Request.QueryString["rptDocType"].ToString();
                    Loanvalue = Request.QueryString["rptLoan"].ToString();
                    LCvalue = Request.QueryString["rptLC"].ToString();
                    BillType = Request.QueryString["BillType"].ToString();
                    unaccepted = Request.QueryString["Unaccepted"].ToString();
                }

                else if (Request.QueryString["rptCode"] == "2")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Export_Report_BillOutStanding_Custwise";
                    doctypevalue = Request.QueryString["rptDocType"].ToString();
                    Loanvalue = Request.QueryString["rptLoan"].ToString();
                    LCvalue = Request.QueryString["rptLC"].ToString();
                    BillType = Request.QueryString["BillType"].ToString();
                    type = Request.QueryString["rptType"].ToString();
                    unaccepted = Request.QueryString["Unaccepted"].ToString();


                }

                else if (Request.QueryString["rptCode"] == "3")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Export_Report_BillOutStanding_OverseasPartyWise";
                    doctypevalue = Request.QueryString["rptDocType"].ToString();
                    Loanvalue = Request.QueryString["rptLoan"].ToString();
                    LCvalue = Request.QueryString["rptLC"].ToString();
                    BillType = Request.QueryString["BillType"].ToString();
                    type = Request.QueryString["rptType"].ToString();
                    unaccepted = Request.QueryString["Unaccepted"].ToString();
                }
                else if (Request.QueryString["rptCode"] == "4")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Export_Report_BillOutStanding_OverseasBankWise";
                    doctypevalue = Request.QueryString["rptDocType"].ToString();
                    Loanvalue = Request.QueryString["rptLoan"].ToString();
                    LCvalue = Request.QueryString["rptLC"].ToString();
                    BillType = Request.QueryString["BillType"].ToString();
                    type = Request.QueryString["rptType"].ToString();
                    unaccepted = Request.QueryString["Unaccepted"].ToString();

                }
                else if (Request.QueryString["rptCode"] == "5")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Export_Report_BillOutStanding_Currency";
                    doctypevalue = Request.QueryString["rptDocType"].ToString();
                    Loanvalue = Request.QueryString["rptLoan"].ToString();
                    LCvalue = Request.QueryString["rptLC"].ToString();
                    BillType = Request.QueryString["BillType"].ToString();
                    type = Request.QueryString["rptType"].ToString();
                    unaccepted = Request.QueryString["Unaccepted"].ToString();


                }


                string frmdate = DateTime.ParseExact(Request.QueryString["frm"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
             

                Microsoft.Reporting.WebForms.ReportParameter startdate = new Microsoft.Reporting.WebForms.ReportParameter();
                startdate.Name = "startdate";
                startdate.Values.Add(frmdate);

              


                Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
                user.Name = "user";
                user.Values.Add(Session["userName"].ToString());

                //------------------------------------------------------


                Unaccepted.Values.Add(unaccepted);
                Type.Values.Add(type);
                doctype.Values.Add(doctypevalue);
                OVP.Values.Add(OVPvalue);
                OVB.Values.Add(OVBvalue);
                Loan.Values.Add(Loanvalue);
                LC.Values.Add(LCvalue);
                billtype.Values.Add(BillType);

                if (Request.QueryString["rptCode"] == "1")
                {
                    ReportViewer1.ServerReport.SetParameters(
                       new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, branch, user, doctype, Loan, LC, billtype, Unaccepted });
                }
                else if (Request.QueryString["rptCode"] == "2")
                {
                    ReportViewer1.ServerReport.SetParameters(
                  new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, branch, user, Type, doctype, Loan, LC, billtype, Unaccepted });
                }

                else if (Request.QueryString["rptCode"] == "3")
                {
                    ReportViewer1.ServerReport.SetParameters(
                  new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, branch, user, Type, doctype, Loan, LC, billtype, Unaccepted });
                }
                else if (Request.QueryString["rptCode"] == "4")
                {
                    ReportViewer1.ServerReport.SetParameters(
                  new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, branch, user, Type, doctype, Loan, LC, billtype, Unaccepted});
                }
                else if (Request.QueryString["rptCode"] == "5")
                {
                    ReportViewer1.ServerReport.SetParameters(
                  new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, branch, user, Type, doctype, Loan, LC, billtype, Unaccepted });
                }

            }
        }
    }
}