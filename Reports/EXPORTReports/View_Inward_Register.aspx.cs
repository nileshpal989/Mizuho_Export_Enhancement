﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EXPORTReports_View_Inward_Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["frm"] != null && Request.QueryString["to"] != null)
            {

                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString; 
                string Idvalue = "";
                string custvalue = "";
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl =
                  new Uri(url);
                
                Microsoft.Reporting.WebForms.ReportParameter custacc = new Microsoft.Reporting.WebForms.ReportParameter();
                custacc.Name = "custacc";
                Microsoft.Reporting.WebForms.ReportParameter adCode = new Microsoft.Reporting.WebForms.ReportParameter();
                adCode.Name = "adCode";
                Microsoft.Reporting.WebForms.ReportParameter id = new Microsoft.Reporting.WebForms.ReportParameter();
                id.Name = "id";
                Microsoft.Reporting.WebForms.ReportParameter frmDate = new Microsoft.Reporting.WebForms.ReportParameter();
                frmDate.Name = "fromdate";
                Microsoft.Reporting.WebForms.ReportParameter toDate = new Microsoft.Reporting.WebForms.ReportParameter();
                toDate.Name = "todate";


                string frmdate = DateTime.ParseExact(Request.QueryString["frm"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                string todate = DateTime.ParseExact(Request.QueryString["to"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                string AdCode = Request.QueryString["Branch"].ToString();
                
                if (Request.QueryString["rptCode"] == "1")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptInwardRemittanceRegisterDocwise";
                    custvalue = Request.QueryString["rptType"].ToString();
                    Idvalue = "1";
                }
                else if (Request.QueryString["rptCode"] == "2")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptInwardRemittancesRegisterCustomerwise";
                     Idvalue = "2";
                    custvalue = Request.QueryString["rptType"].ToString();
                }
                else if (Request.QueryString["rptCode"] == "3")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptInwardRemittancesRegisterCustomerwise";
                    Idvalue = "2";
                    custvalue = Request.QueryString["rptType"].ToString();
                }
                frmDate.Values.Add(frmdate);
                toDate.Values.Add(todate);
                adCode.Values.Add(AdCode);
                custacc.Values.Add(custvalue);
                id.Values.Add(Idvalue);
                Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
                user.Name = "user";
                user.Values.Add(Session["userName"].ToString());

                //------------------------------------------------------//
                if (Request.QueryString["rptCode"] == "1")
                {
                    ReportViewer1.ServerReport.SetParameters(
                       new Microsoft.Reporting.WebForms.ReportParameter[] { frmDate, toDate, adCode, custacc, id, user });
                }
                else if (Request.QueryString["rptCode"] == "2" || Request.QueryString["rptCode"] == "3")
                {
                    ReportViewer1.ServerReport.SetParameters(
                  new Microsoft.Reporting.WebForms.ReportParameter[] { frmDate, toDate, adCode, custacc, id, user });
                }
                
            }
        }
    }
}