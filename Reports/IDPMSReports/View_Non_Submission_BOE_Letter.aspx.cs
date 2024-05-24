using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_IDPMSReports_View_Non_Submission_BOE_Letter : System.Web.UI.Page
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


                string branch = Request.QueryString["branchname"].ToString();
                string doc = Request.QueryString["doc"].ToString();
                string type = Request.QueryString["type"].ToString();
                string fromdate = Request.QueryString["fromdate"].ToString();
                string todate = Request.QueryString["todate"].ToString();
                string ormtype = Request.QueryString["ormtype"].ToString();
                string purpose = Request.QueryString["purpose"].ToString();

                //serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Non_Submission_BOE_Letter";
                if (purpose.ToString() == "advanced")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IDPMS_TRACER_LETTER_ADVANCED";
                }
                else
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IDPMS_TRACER_LETTER";
                }

                Microsoft.Reporting.WebForms.ReportParameter CustACNo = new Microsoft.Reporting.WebForms.ReportParameter();
                CustACNo.Name = "CustACNo";
                CustACNo.Values.Add(type);

                Microsoft.Reporting.WebForms.ReportParameter OrmNo = new Microsoft.Reporting.WebForms.ReportParameter();
                OrmNo.Name = "OrmNo";
                OrmNo.Values.Add(doc);

                Microsoft.Reporting.WebForms.ReportParameter FromDate = new Microsoft.Reporting.WebForms.ReportParameter();
                FromDate.Name = "FromDate";
                FromDate.Values.Add(fromdate);

                Microsoft.Reporting.WebForms.ReportParameter ToDate = new Microsoft.Reporting.WebForms.ReportParameter();
                ToDate.Name = "ToDate";
                ToDate.Values.Add(todate);

                Microsoft.Reporting.WebForms.ReportParameter AdCode = new Microsoft.Reporting.WebForms.ReportParameter();
                AdCode.Name = "AdCode";
                AdCode.Values.Add(branch);


                Microsoft.Reporting.WebForms.ReportParameter Orm_Type = new Microsoft.Reporting.WebForms.ReportParameter();
                Orm_Type.Name = "Orm_Type";
                Orm_Type.Values.Add(ormtype);

                ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { CustACNo, OrmNo, FromDate, ToDate, AdCode, Orm_Type });

            }
        }
    }
}