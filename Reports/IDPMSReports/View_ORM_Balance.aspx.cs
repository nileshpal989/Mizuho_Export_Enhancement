using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EDPMS_Reports_View_ORM_Balance : System.Web.UI.Page
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

                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IDPMS_ORM_BALANCE";

                string branch = Request.QueryString["branchname"].ToString();
                string fromdate = Request.QueryString["from"].ToString();
                string todate = Request.QueryString["to"].ToString();
                string mode = Request.QueryString["mode"].ToString();
                string type = Request.QueryString["type"].ToString();
                string ormtype = Request.QueryString["ormtype"].ToString();

                Microsoft.Reporting.WebForms.ReportParameter branch1 = new Microsoft.Reporting.WebForms.ReportParameter();
                branch1.Name = "branch";
                branch1.Values.Add(branch);

                Microsoft.Reporting.WebForms.ReportParameter fromdate1 = new Microsoft.Reporting.WebForms.ReportParameter();
                fromdate1.Name = "fromdate";
                fromdate1.Values.Add(fromdate);


                Microsoft.Reporting.WebForms.ReportParameter todate1 = new Microsoft.Reporting.WebForms.ReportParameter();
                todate1.Name = "todate";
                todate1.Values.Add(todate);

                Microsoft.Reporting.WebForms.ReportParameter mode1 = new Microsoft.Reporting.WebForms.ReportParameter();
                mode1.Name = "mode";
                mode1.Values.Add(mode);

                Microsoft.Reporting.WebForms.ReportParameter type1 = new Microsoft.Reporting.WebForms.ReportParameter();
                type1.Name = "type";
                type1.Values.Add(type);


                string username = Session["userName"].ToString();
                Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
                user.Name = "user";
                user.Values.Add(username);

                Microsoft.Reporting.WebForms.ReportParameter ormtype1 = new Microsoft.Reporting.WebForms.ReportParameter();
                ormtype1.Name = "typeoform";
                ormtype1.Values.Add(ormtype);

                ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { fromdate1, todate1, mode1, type1, user, branch1, ormtype1 });

            }
        }
    }
}