using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class IMP_IMPReports_TF_IMP_View_Shippingbillcontrol : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.QueryString["frm"] != null)//&& Request.QueryString["to"] != null)
            {
                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl = new Uri(url);

                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_Shipping_Bill_Controls";


                //string frmdate = DateTime.ParseExact(Request.QueryString["frm"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                //string todate = DateTime.ParseExact(Request.QueryString["to"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                string userName = Session["userName"].ToString();

                Microsoft.Reporting.WebForms.ReportParameter UserName = new Microsoft.Reporting.WebForms.ReportParameter();
                UserName.Name = "UserName";
                // string UserName1 = Request.QueryString["userName"].ToString();
                UserName.Values.Add(userName);

                Microsoft.Reporting.WebForms.ReportParameter startdate = new Microsoft.Reporting.WebForms.ReportParameter();
                startdate.Name = "FromDate";
                string Frmdate1 = Request.QueryString["frm"].ToString();
                startdate.Values.Add(Frmdate1);

                Microsoft.Reporting.WebForms.ReportParameter enddate = new Microsoft.Reporting.WebForms.ReportParameter();
                enddate.Name = "ToDate";
                string todate1 = Request.QueryString["to"].ToString();
                enddate.Values.Add(todate1);

                ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { UserName, startdate, enddate });
            }
        }
    }
}
