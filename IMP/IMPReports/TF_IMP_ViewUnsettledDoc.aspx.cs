using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Globalization;

public partial class IMP_IMPReports_TF_IMP_ViewUnsettledDoc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string Branchcode = Request.QueryString["Branchcode"].ToString();
            string DocumentType = Request.QueryString["Documentype"].ToString();
            string Type = Request.QueryString["Type"].ToString();
            string Date = Request.QueryString["Date"].ToString();
            string userName = Session["userName"].ToString();
            string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;

            ReportViewer1.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            ServerReport serverReport = ReportViewer1.ServerReport;
            serverReport.ReportServerUrl = new Uri(url);

            if (Request.QueryString["Type"].ToString()=="A")  //UNACCEPTED Report
            {
                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_Unaccepted_LodgementReports";
            }
             if (Request.QueryString["Type"].ToString()=="S")  //UNSETTELD REPORT
            {
                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_Unsettled_LodgementReports";
            
            }
            // string CustAC = Request.QueryString["CustAC"].ToString();

            Microsoft.Reporting.WebForms.ReportParameter Asondate = new Microsoft.Reporting.WebForms.ReportParameter();
            Asondate.Name = "date";
            Asondate.Values.Add(Date);

            Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
            Branch.Name = "branchCode";
            Branch.Values.Add(Branchcode);


            Microsoft.Reporting.WebForms.ReportParameter Document_Type = new Microsoft.Reporting.WebForms.ReportParameter();
            Document_Type.Name = "DocType";
            Document_Type.Values.Add(DocumentType);

            //Microsoft.Reporting.WebForms.ReportParameter R_Type = new Microsoft.Reporting.WebForms.ReportParameter();
            //R_Type.Name = "Type";
            //R_Type.Values.Add(Type);

            //Microsoft.Reporting.WebForms.ReportParameter CustomerAcNO = new Microsoft.Reporting.WebForms.ReportParameter();
            //CustomerAcNO.Name = "custAcNo";
            //CustomerAcNO.Values.Add(CustAC);

            Microsoft.Reporting.WebForms.ReportParameter UserName = new Microsoft.Reporting.WebForms.ReportParameter();
            UserName.Name = "user";
            UserName.Values.Add(userName);

            ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { Document_Type, UserName, Branch, Asondate});
        }
    }
}