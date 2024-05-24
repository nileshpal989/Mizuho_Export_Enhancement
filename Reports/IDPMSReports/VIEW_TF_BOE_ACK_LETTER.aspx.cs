using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_IDPMSReports_VIEW_TF_BOE_ACK_LETTER : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["branchname"] != null)
            {
                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;

                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl = new Uri(url);

                //serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_BOE_ACK_LETTER";
                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IDPMS_BOE_ACK_LETTER_BYORM";

                string branch = Request.QueryString["branchname"].ToString();
                string doc = Request.QueryString["doc"].ToString();
                string type = Request.QueryString["type"].ToString();
                string fromdate = Request.QueryString["fromdate"].ToString();
                string todate = Request.QueryString["todate"].ToString();

                string ormtype = Request.QueryString["ormtype"].ToString();



                Microsoft.Reporting.WebForms.ReportParameter type1 = new Microsoft.Reporting.WebForms.ReportParameter();
                type1.Name = "type";
                type1.Values.Add(type);

                Microsoft.Reporting.WebForms.ReportParameter doc1 = new Microsoft.Reporting.WebForms.ReportParameter();
                doc1.Name = "doc";
                doc1.Values.Add(doc);

                Microsoft.Reporting.WebForms.ReportParameter fromdate1 = new Microsoft.Reporting.WebForms.ReportParameter();
                fromdate1.Name = "fromdate";
                fromdate1.Values.Add(fromdate);

                Microsoft.Reporting.WebForms.ReportParameter todate1 = new Microsoft.Reporting.WebForms.ReportParameter();
                todate1.Name = "todate";
                todate1.Values.Add(todate);

                Microsoft.Reporting.WebForms.ReportParameter branch1 = new Microsoft.Reporting.WebForms.ReportParameter();
                branch1.Name = "adcode";
                branch1.Values.Add(branch);

                Microsoft.Reporting.WebForms.ReportParameter ormtype1 = new Microsoft.Reporting.WebForms.ReportParameter();
                ormtype1.Name = "typeoform";
                ormtype1.Values.Add(ormtype);

                ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { type1, doc1, fromdate1, todate1, branch1, ormtype1 });

            }
        }
    }
}