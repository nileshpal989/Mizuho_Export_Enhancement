using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_RRETURNReports_View_rptRRETURN_Datachecklist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["frm"] != null && Request.QueryString["to"] != null)
            {


                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
                // Set the processing mode for the ReportViewer to Remote

                //string custAcvalue = "";
                //    string purvalue = "";

                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl =
                  new Uri(url);
                // Set the report server URL and report path


                Microsoft.Reporting.WebForms.ReportParameter ModeType = new Microsoft.Reporting.WebForms.ReportParameter();
                ModeType.Name = "ModeType";
                Microsoft.Reporting.WebForms.ReportParameter PurCode = new Microsoft.Reporting.WebForms.ReportParameter();
                PurCode.Name = "PurCode";
                Microsoft.Reporting.WebForms.ReportParameter branch = new Microsoft.Reporting.WebForms.ReportParameter();
                branch.Name = "Branch";
                Microsoft.Reporting.WebForms.ReportParameter pur = new Microsoft.Reporting.WebForms.ReportParameter();
                pur.Name = "pur";



                if (Request.QueryString["branch"] == "1")
                {
                    branch.Values.Add("All Branches");
                }
                else
                {
                    branch.Values.Add(Request.QueryString["branch"]);
                }

                if (Request.QueryString["rptCode"] == "1")
                {
                    pur.Values.Add("TYPE");
                    ModeType.Values.Add("ALL");
                    PurCode.Values.Add("");
                    switch (Request.QueryString["Report"])
                    {
                        case "R Return Data CheckList":
                            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptRRETURN_DatachecklistTypeWise";
                            break;
                    }
                }
                else if (Request.QueryString["rptCode"] == "2")
                {

                    pur.Values.Add("TYPE");
                    ModeType.Values.Add(Request.QueryString["rptType"].ToString());
                    PurCode.Values.Add("");
                    switch (Request.QueryString["Report"])
                    {
                        case "R Return Data CheckList":
                            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptRRETURN_DatachecklistTypeWise";
                            break;
                    }
                }
                else if (Request.QueryString["rptCode"] == "3")
                {
                    pur.Values.Add("CODE");
                    PurCode.Values.Add("ALL");
                    ModeType.Values.Add("");
                    switch (Request.QueryString["Report"])
                    {
                        case "R Return Data CheckList":
                            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptRRETURN_DatachecklistPurCodeWise";
                            break;                        
                    }
                }

                else if (Request.QueryString["rptCode"] == "4")
                {

                    pur.Values.Add("CODE");
                    PurCode.Values.Add(Request.QueryString["rptType"].ToString());
                    ModeType.Values.Add("");
                    switch (Request.QueryString["Report"])
                    {
                         case "R Return Data CheckList":
                            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptRRETURN_DatachecklistPurCodeWise";
                            break;  
                    }
                }


                string frmdate = DateTime.ParseExact(Request.QueryString["frm"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                string todate = DateTime.ParseExact(Request.QueryString["to"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");


                Microsoft.Reporting.WebForms.ReportParameter startdate = new Microsoft.Reporting.WebForms.ReportParameter();
                startdate.Name = "startdate";
                startdate.Values.Add(frmdate);

                Microsoft.Reporting.WebForms.ReportParameter enddate = new Microsoft.Reporting.WebForms.ReportParameter();
                enddate.Name = "enddate";
                enddate.Values.Add(todate);


                Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
                user.Name = "user";
                user.Values.Add(Session["userName"].ToString());

                ReportViewer1.ServerReport.SetParameters(
                new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, enddate, branch, user, ModeType, PurCode, pur });                

            }
        }
    }
}