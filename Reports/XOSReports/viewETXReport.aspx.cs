using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_XOSReports_viewETXReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         if (!IsPostBack)
        {
            Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
            Branch.Name = "Branch";

            Microsoft.Reporting.WebForms.ReportParameter FromDate = new Microsoft.Reporting.WebForms.ReportParameter();
            FromDate.Name = "FDate";

            Microsoft.Reporting.WebForms.ReportParameter ToDate = new Microsoft.Reporting.WebForms.ReportParameter();
           ToDate.Name = "TDate";

            Microsoft.Reporting.WebForms.ReportParameter Cust = new Microsoft.Reporting.WebForms.ReportParameter();
            Cust.Name = "Cust";

         
            if (Request.QueryString["fromDate"] != null)
            {
                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
             
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl = new Uri(url);

                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptETXRegister";

                string frmdate = Request.QueryString["fromDate"].ToString();

                FromDate.Values.Add(frmdate);

                string todate = Request.QueryString["todate"].ToString();

                ToDate.Values.Add(todate);


                //string frmdate = DateTime.ParseExact(Request.QueryString["fromDate"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");

                //FromDate.Values.Add(frmdate);

                //string todate = DateTime.ParseExact(Request.QueryString["todate"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");

                //ToDate.Values.Add(todate);




                string Branch1 = Request.QueryString["Branch"].ToString();
                Branch.Values.Add(Branch1);

             

                string CustType1 = Request.QueryString["Cust"].ToString();
                Cust.Values.Add(CustType1);

              
                Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
                user.Name = "user";
                user.Values.Add(Session["userName"].ToString());

                ReportViewer1.ServerReport.SetParameters(
                   new Microsoft.Reporting.WebForms.ReportParameter[] { user,FromDate,ToDate, Branch, Cust});



            }
        }
    }
}

    
