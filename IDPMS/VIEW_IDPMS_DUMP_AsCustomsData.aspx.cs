using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class IDPMS_VIEW_IDPMS_DUMP_AsCustomsData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //string username = Session["userName"].ToString();
            //GetData();

            string parameter = Request.QueryString["parameter"].ToString();
            string todate = Request.QueryString["to"].ToString();
            string fromdate = Request.QueryString["from"].ToString();
            string Brnch = Request.QueryString["branchname"].ToString();
            string type2 = Request.QueryString["type"].ToString();

            string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;

            ReportViewer1.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            ServerReport serverReport = ReportViewer1.ServerReport;
            serverReport.ReportServerUrl = new Uri(url);

            if (parameter == "All")
            {
                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/IDPMS_DUMP_ASCustoms_ALL";
            }
            else if (parameter == "Match")
            {
                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/IDPMS_DUMP_ASCustoms_MATCHED";
            }
            else
            {
                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/IDPMS_DUMP_ASCustoms_UNMATCHED";
            }

            string username = Session["userName"].ToString();
            Microsoft.Reporting.WebForms.ReportParameter user1 = new Microsoft.Reporting.WebForms.ReportParameter();
            user1.Name = "user";
            user1.Values.Add(username);


            Microsoft.Reporting.WebForms.ReportParameter FromDate = new Microsoft.Reporting.WebForms.ReportParameter();
            FromDate.Name = "fromdate";
            FromDate.Values.Add(fromdate);

            Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
            Branch.Name = "Branch";
            Branch.Values.Add(Brnch);
            //Branch.Values.Add("All Branches");


            Microsoft.Reporting.WebForms.ReportParameter ToDate = new Microsoft.Reporting.WebForms.ReportParameter();
            ToDate.Name = "todate";
            ToDate.Values.Add(todate);

            Microsoft.Reporting.WebForms.ReportParameter type1 = new Microsoft.Reporting.WebForms.ReportParameter();
            type1.Name = "type";
            type1.Values.Add(type2);


            ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { FromDate, ToDate, user1, Branch, type1 });

        }
    }
}