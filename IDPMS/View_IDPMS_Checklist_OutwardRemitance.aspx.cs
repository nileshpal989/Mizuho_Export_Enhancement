using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class IDPMS_View_IDPMS_Checklist_OutwardRemitance : System.Web.UI.Page
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

                //Use this to take Report Folder Name from the web.config
                //string ReportFolderName = System.Web.Configuration.WebConfigurationManager.AppSettings["ReportFolderName"].ToString();
                //serverReport.ReportPath = "/" + ReportFolderName + "/TF_IDPMS_DataChecklist_OutwardRemitance_rpt";

                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IDPMS_DataChecklist_OutwardRemitance_rpt";

                string fromdate1 = Request.QueryString["from"].ToString();
                string todate1 = Request.QueryString["to"].ToString();
                string type1 = Request.QueryString["type"].ToString();
                string ormtype = Request.QueryString["ormtype"].ToString();

                string brname = Request.QueryString["branchname"].ToString();
                Microsoft.Reporting.WebForms.ReportParameter branchname = new Microsoft.Reporting.WebForms.ReportParameter();
                branchname.Name = "Branch";
                branchname.Values.Add(brname);

                Microsoft.Reporting.WebForms.ReportParameter fromdate = new Microsoft.Reporting.WebForms.ReportParameter();
                fromdate.Name = "fromdate";
                fromdate.Values.Add(fromdate1);

                Microsoft.Reporting.WebForms.ReportParameter todate = new Microsoft.Reporting.WebForms.ReportParameter();
                todate.Name = "todate";
                todate.Values.Add(todate1);

                Microsoft.Reporting.WebForms.ReportParameter Cust = new Microsoft.Reporting.WebForms.ReportParameter();
                Cust.Name = "CustID";
                Cust.Values.Add(type1);

                string username = Session["userName"].ToString();
                Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
                user.Name = "user";
                user.Values.Add(username);

                Microsoft.Reporting.WebForms.ReportParameter ormtype1 = new Microsoft.Reporting.WebForms.ReportParameter();
                ormtype1.Name = "typeoform";
                ormtype1.Values.Add(ormtype);

                ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { branchname, fromdate, todate, Cust, user, ormtype1 });
            }
        }
    }

}