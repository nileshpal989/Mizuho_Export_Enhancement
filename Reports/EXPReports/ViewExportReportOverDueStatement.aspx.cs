using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EXPReports_ViewExportReportOverDueStatement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Microsoft.Reporting.WebForms.ReportParameter DocType = new Microsoft.Reporting.WebForms.ReportParameter();
            DocType.Name = "DocType";

            Microsoft.Reporting.WebForms.ReportParameter FromDate = new Microsoft.Reporting.WebForms.ReportParameter();
            FromDate.Name = "Date";

            Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
            Branch.Name = "Branch";

            Microsoft.Reporting.WebForms.ReportParameter BillAmt = new Microsoft.Reporting.WebForms.ReportParameter();
            BillAmt.Name = "Bill_Amount";

            Microsoft.Reporting.WebForms.ReportParameter Days = new Microsoft.Reporting.WebForms.ReportParameter();
            Days.Name = "No_Of_Days";

            Microsoft.Reporting.WebForms.ReportParameter OverSeasBank = new Microsoft.Reporting.WebForms.ReportParameter();
            OverSeasBank.Name = "OverSeasBank";

            Microsoft.Reporting.WebForms.ReportParameter CustType = new Microsoft.Reporting.WebForms.ReportParameter();
            CustType.Name = "CustType";

            Microsoft.Reporting.WebForms.ReportParameter FCust = new Microsoft.Reporting.WebForms.ReportParameter();
            FCust.Name = "FCust";

            Microsoft.Reporting.WebForms.ReportParameter User = new Microsoft.Reporting.WebForms.ReportParameter();
            User.Name = "User";

            Microsoft.Reporting.WebForms.ReportParameter Loan = new Microsoft.Reporting.WebForms.ReportParameter();
            Loan.Name = "Loan";

            Microsoft.Reporting.WebForms.ReportParameter LC = new Microsoft.Reporting.WebForms.ReportParameter();
            LC.Name = "LC";

            if (Request.QueryString["FromDate"] != null)
            {
                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
                // Set the processing mode for the ReportViewer to Remote

                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl = new Uri(url);

                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptExportBillOverdueStatement";

                string frmdate = DateTime.ParseExact(Request.QueryString["FromDate"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                FromDate.Values.Add(frmdate);

                string Branch1 = Request.QueryString["Branch"];
                Branch.Values.Add(Branch1);

                string DocType1 = Request.QueryString["DocType"];
                DocType.Values.Add(DocType1);

                string CustType1 = Request.QueryString["CustType"];
                CustType.Values.Add(CustType1);

                string FCust1 = Request.QueryString["FCust"];
                FCust.Values.Add(FCust1);

                string BillAmt1 = Request.QueryString["BillAmt"];
                BillAmt.Values.Add(BillAmt1);

                string Days1 = Request.QueryString["Days"];
                Days.Values.Add(Days1);

                string LC1 = Request.QueryString["rptLC"];
                LC.Values.Add(LC1);

                string OverSeasBank1 = Request.QueryString["OverSeasBank"];
                OverSeasBank.Values.Add(OverSeasBank1);

                Loan.Values.Add(Request.QueryString["rptLoan"].ToString());

                User.Values.Add(Session["userName"].ToString());

                ReportViewer1.ServerReport.SetParameters(
                   new Microsoft.Reporting.WebForms.ReportParameter[] { FromDate, DocType, BillAmt, Days, OverSeasBank , Branch, FCust, CustType, User,Loan , LC });



            }
        }
    }
}