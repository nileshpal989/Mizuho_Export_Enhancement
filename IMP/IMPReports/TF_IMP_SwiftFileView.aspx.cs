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

public partial class IMP_IMPReports_TF_IMP_SwiftFileView : System.Web.UI.Page
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
            if (Request.QueryString["Type"].ToString() == "FOREIGN")
            {
                switch (Request.QueryString["FileType"].ToString())
                {
                    case "MT199":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_MT199Report";
                        break;
                    case "MT299":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_MT299Report";
                        break;
                    case "MT499":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_MT499Report";
                        break;
                    case "Prot499":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_MTProtest499Report";
                        break;
                    case "Prot999":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_MTProtest999Report";
                        break;
                    case "MT999":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_MT999Report";
                        break;
                    case "MT734":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_MT734Report";
                        break;
                    case "MT799":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_MT799Report";
                        break;
                    case "MT999LC":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_MT999LCReport";
                        break;
                    

                    //Acceptance Reports
                    case "MT740":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_MT740Report";
                        break;
                    case "MT756":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_MT756Report";
                        break;
                    case "MT999756":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_MT999756Report";
                        break;
                    case "MT799ACC":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_MT799ACCReport";
                        break;
                    case "MT412":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_MT412Report";
                        break;
                    case "MT999C":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_MT999412Report";
                        break;
                    case "MT499C":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_MT499CReport";
                        break;
                    case "MT747":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_MT747Report";
                        break;

                    // Settlement Reports
                    case "MT103":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_MT103Report";
                        break;
                    case "MT202":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_MT202Report";
                        break;
                    case "MT200":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_MT200Report";
                        break;
                    case "R42":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_MTR42Report";
                        break;
                        //Nilesh
                    case "MT754":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_MT754Report";
                        break;
                        //END nilesh
                }//
            }
            //vinay Changes 22/02/2020
            if (Request.QueryString["Type"].ToString() == "FOREIGN_Ledger")
            {
                switch (Request.QueryString["FileType"].ToString())
                {
                    case "MT499":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_Ledger_Modification_MT499Report";
                        break;
                    case "MT799":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_Ledger_Modification_MT799Report";
                        break;
                    case "MT999":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_Ledger_Modification_MT999Report";
                        break;
                }
            
            }
            if (Request.QueryString["Type"].ToString() == "LOCAL")
            {
                switch (Request.QueryString["FileType"].ToString())
                {
                    case "MT499":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_FIN499Report";
                        break;
                    case "Prot499":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_FINProtest499Report";
                        break;
                    case "Prot999":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_FINProtest999Report";
                        break;
                    case "MT999":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_FIN999Report";
                        break;
                    case "MT734":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_FIN734Report";
                        break;
                    case "MT799":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_FIN799Report";
                        break;
                    case "MT999LC":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_FIN999LCReport";
                        break;
                    case "MT756":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_FIN756Report";
                        break;
                    case "MT799ACC":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_FIN799ACCReport";
                        break;
                    case "IBDR42":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_IBD_R42";
                        break;
                    case "MT747":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_MT747Report";
                        break;
                }
            }
            if (Request.QueryString["FileType"].ToString() == "AML")
            {
                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_AMLReport";
            }

            string _DocNo = Request.QueryString["DocNo"].ToString();
            string _userName = Session["userName"].ToString();

            Microsoft.Reporting.WebForms.ReportParameter DocNo = new Microsoft.Reporting.WebForms.ReportParameter();
            DocNo.Name = "DocNo";
            DocNo.Values.Add(_DocNo);

            Microsoft.Reporting.WebForms.ReportParameter UserName = new Microsoft.Reporting.WebForms.ReportParameter();
            UserName.Name = "username";
            UserName.Values.Add(_userName);

            Microsoft.Reporting.WebForms.ReportParameter IBDDocument_No_Extn = new Microsoft.Reporting.WebForms.ReportParameter();
            IBDDocument_No_Extn.Name = "IBDDocument_No_Extn";

            if (Request.QueryString["FileType"].ToString() == "IBDR42")
            {
                string _IBDDocument_No_Extn = Request.QueryString["IBDExtnNo"].ToString();
                IBDDocument_No_Extn.Values.Add(_IBDDocument_No_Extn);

                ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { DocNo, IBDDocument_No_Extn, UserName });
            }
            else
            {
                ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { DocNo, UserName });
            }
        }
    }
}