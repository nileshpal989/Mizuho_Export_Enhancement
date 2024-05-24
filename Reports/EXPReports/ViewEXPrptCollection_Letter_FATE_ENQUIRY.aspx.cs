using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EXPReports_ViewEXPrptCollection_Letter_FATE_ENQUIRY : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Microsoft.Reporting.WebForms.ReportParameter Frdocno = new Microsoft.Reporting.WebForms.ReportParameter();
            Frdocno.Name = "Frdocno";
            Microsoft.Reporting.WebForms.ReportParameter Todocno = new Microsoft.Reporting.WebForms.ReportParameter();
            Todocno.Name = "Todocno";
            string frdocno = "";
            string todocno = "";
            //if (Request.QueryString["frm"] != null)
            //{
            string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
            // Set the processing mode for the ReportViewer to Remote

            ReportViewer1.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            ServerReport serverReport = ReportViewer1.ServerReport;
            serverReport.ReportServerUrl = new Uri(url);
            Microsoft.Reporting.WebForms.ReportParameter docno = new Microsoft.Reporting.WebForms.ReportParameter();
            docno.Name = "docno";
            Microsoft.Reporting.WebForms.ReportParameter Remark1 = new Microsoft.Reporting.WebForms.ReportParameter();
            Remark1.Name = "Remark1";
            Microsoft.Reporting.WebForms.ReportParameter Remark3 = new Microsoft.Reporting.WebForms.ReportParameter();
            Remark3.Name = "Remark3";
            Microsoft.Reporting.WebForms.ReportParameter Remark2 = new Microsoft.Reporting.WebForms.ReportParameter();
            Remark2.Name = "Remark2";
            Microsoft.Reporting.WebForms.ReportParameter Remark4 = new Microsoft.Reporting.WebForms.ReportParameter();
            Remark4.Name = "Remark4";
            Microsoft.Reporting.WebForms.ReportParameter Remark5 = new Microsoft.Reporting.WebForms.ReportParameter();
            Remark5.Name = "Remark5";


            string remark1 = "";
            string remark2 = "";
            string remark3 = "";
            string remark4 = "";
            string remark5 = "";

            // Set the report server URL and report path
            if (Request.QueryString["rptCode"] == "1")
            {
                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptEXP_Collection_FATE_ENQUIRY_LETTER";
                frdocno = "";
                todocno = "";
                docno.Values.Add("All");
            }
            if (Request.QueryString["rptCode"] == "2")
            {
                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptEXP_Collection_FATE_ENQUIRY_LETTER";


                frdocno = Request.QueryString["rptFrdocno"].ToString();
                todocno = Request.QueryString["rptTodocno"].ToString();
                docno.Values.Add("1");

            }


            remark1 = Request.QueryString["Remark1"].ToString();
            remark2 = Request.QueryString["Remark2"].ToString();
            remark3 = Request.QueryString["Remark3"].ToString();
            remark4 = Request.QueryString["Remark4"].ToString();
            remark5 = Request.QueryString["Remark5"].ToString();

            //string frmdate = DateTime.ParseExact(Request.QueryString["frm"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");

            //Microsoft.Reporting.WebForms.ReportParameter DocDate = new Microsoft.Reporting.WebForms.ReportParameter();
            //DocDate.Name = "DocDate";
            //DocDate.Values.Add(frmdate);

            Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
            Branch.Name = "Branch";
            string branch = Request.QueryString["Branch"];
            Branch.Values.Add(branch);

            Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
            user.Name = "user";
            user.Values.Add(Session["userName"].ToString());


            Frdocno.Values.Add(frdocno);
            Todocno.Values.Add(todocno);
            Remark1.Values.Add(remark1);
            Remark2.Values.Add(remark2);
            Remark3.Values.Add(remark3);
            Remark4.Values.Add(remark4);
            Remark5.Values.Add(remark5);

            ReportViewer1.ServerReport.SetParameters(
               new Microsoft.Reporting.WebForms.ReportParameter[] { docno, Frdocno, Todocno, Remark1, Remark2, Remark3, Remark4, Remark5, user, Branch });
            //}
        }
    }
}