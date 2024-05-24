using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class EXP_TF_EXP_SwiftFileView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
            ReportViewer1.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            ServerReport serverReport = ReportViewer1.ServerReport;
            serverReport.ReportServerUrl = new Uri(url);
            string swifttype = Request.QueryString["SwiftType"].ToString();
            
               if (swifttype == "MT 499")
               {
                   serverReport.ReportPath = "/Mizuho_TF_Reports/TF_EXP_MT499Report";
               }
               if (swifttype == "MT 199")
               {
                   serverReport.ReportPath = "/Mizuho_TF_Reports/TF_EXP_MT199Report";
               }
               if (swifttype == "MT 299")
               {
                   serverReport.ReportPath = "/Mizuho_TF_Reports/TF_EXP_MT299Report";
               }
               if (swifttype == "MT 999")
               {
                   serverReport.ReportPath = "/Mizuho_TF_Reports/TF_EXP_MT999Report";
               }
               if (swifttype == "MT 799")
               {
                   serverReport.ReportPath = "/Mizuho_TF_Reports/TF_EXP_MT799Report";
               }
               if (swifttype == "MT 420")
               {
                   serverReport.ReportPath = "/Mizuho_TF_Reports/TF_EXP_MT420Report";
               }
               if (swifttype == "MT 742")
               {
                   serverReport.ReportPath = "/Mizuho_TF_Reports/TF_EXP_MT742Report";
               }
               if (swifttype == "MT 754")
               {
                   serverReport.ReportPath = "/Mizuho_TF_Reports/TF_EXP_MT754Report";
               }

            string _DocNo = Request.QueryString["DocNo"].ToString();
            string _userName = Session["userName"].ToString();

            Microsoft.Reporting.WebForms.ReportParameter DocNo = new Microsoft.Reporting.WebForms.ReportParameter();
            DocNo.Name = "DocNo";
            DocNo.Values.Add(_DocNo);

            Microsoft.Reporting.WebForms.ReportParameter UserName = new Microsoft.Reporting.WebForms.ReportParameter();
            UserName.Name = "username";
            UserName.Values.Add(_userName);
            ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { DocNo, UserName });

        }
    }
}