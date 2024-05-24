using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EXPReports_ViewCovSchLetter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Microsoft.Reporting.WebForms.ReportParameter from = new Microsoft.Reporting.WebForms.ReportParameter();
            from.Name = "FDate";

            Microsoft.Reporting.WebForms.ReportParameter Doc_No_Type = new Microsoft.Reporting.WebForms.ReportParameter();
            Doc_No_Type.Name = "Doc_No_Type";

            Microsoft.Reporting.WebForms.ReportParameter FDoc = new Microsoft.Reporting.WebForms.ReportParameter();
            FDoc.Name = "FDoc";

            Microsoft.Reporting.WebForms.ReportParameter TDoc = new Microsoft.Reporting.WebForms.ReportParameter();
            TDoc.Name = "TDoc";

            Microsoft.Reporting.WebForms.ReportParameter User = new Microsoft.Reporting.WebForms.ReportParameter();
            User.Name = "User";

            Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
            Branch.Name = "Branch";

            Microsoft.Reporting.WebForms.ReportParameter NewLine = new Microsoft.Reporting.WebForms.ReportParameter();
            NewLine.Name = "NewLine";

            Microsoft.Reporting.WebForms.ReportParameter Cust = new Microsoft.Reporting.WebForms.ReportParameter();
            Cust.Name = "Cust";

            if (Request.QueryString["from"] != null)
            {
                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
                // Set the processing mode for the ReportViewer to Remote

                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl = new Uri(url);

                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptExportCovSchLetter";

                string frmdate = DateTime.ParseExact(Request.QueryString["from"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                from.Values.Add(frmdate);

                string Doc_No_Type1 = Request.QueryString["Doc_No_Type"];
                Doc_No_Type.Values.Add(Doc_No_Type1);

                string Branch1 = Request.QueryString["Branch"];
                Branch.Values.Add(Branch1);

                string FDoc1 = Request.QueryString["FDoc"];
                FDoc.Values.Add(FDoc1);

                string TDoc1 = Request.QueryString["TDoc"];
                TDoc.Values.Add(TDoc1);

                string NewLine1 = Request.QueryString["NewLine"];
                NewLine.Values.Add(NewLine1);

                string Cust1 = Request.QueryString["Cust"];
                Cust.Values.Add(Cust1);

                User.Values.Add(Session["userName"].ToString());

                ReportViewer1.ServerReport.SetParameters(
                   new Microsoft.Reporting.WebForms.ReportParameter[] { from, Doc_No_Type, Branch, FDoc, TDoc, NewLine, User , Cust });
            }
        }
    }
}