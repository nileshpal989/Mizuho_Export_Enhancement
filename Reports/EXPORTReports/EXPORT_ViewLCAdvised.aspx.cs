﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EXPORTReport_EXPORT_ViewLCAdvised : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["frm"] != null && Request.QueryString["to"] != null)
            {


                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
                // Set the processing mode for the ReportViewer to Remote
                string purvalue = "";
                string Benfvalue = "";
                string IssuingBankvalue = "";
                //string remittervalue = "";
                //string Beneficaryvalue = "";

                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl =
                  new Uri(url);
                // Set the report server URL and report path

                Microsoft.Reporting.WebForms.ReportParameter pur = new Microsoft.Reporting.WebForms.ReportParameter();
                pur.Name = "pur";
                Microsoft.Reporting.WebForms.ReportParameter Benfid = new Microsoft.Reporting.WebForms.ReportParameter();
                Benfid.Name = "Benfid";
                Microsoft.Reporting.WebForms.ReportParameter IssuingBankid = new Microsoft.Reporting.WebForms.ReportParameter();
                IssuingBankid.Name = "IssuingBankid";
                Microsoft.Reporting.WebForms.ReportParameter branch = new Microsoft.Reporting.WebForms.ReportParameter();
                branch.Name = "Branch";
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
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptExportLCAdvice_DocDatewise";
                    purvalue = "DocDate";

                }
                else if (Request.QueryString["rptCode"] == "2")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptExportLCAdvice_BeneficaryWise";
                    purvalue = "0";
                    Benfvalue = "";

                }

                else if (Request.QueryString["rptCode"] == "3")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptExportLCAdvice_BeneficaryWise";
                    purvalue = "1";
                    Benfvalue = Request.QueryString["rptType"].ToString();
                }

                else if (Request.QueryString["rptCode"] == "4")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptExportLCAdvice_IssuingBankWise";
                    purvalue = "0";
                    IssuingBankvalue = "";

                }

                else if (Request.QueryString["rptCode"] == "5")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptExportLCAdvice_IssuingBankWise";
                    purvalue = "1";
                    IssuingBankvalue = Request.QueryString["rptType"].ToString();
                }

                else if (Request.QueryString["rptCode"] == "6")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptExportLCAdvice_LCNowise";
                    purvalue = "LCNo";

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

                //------------------------------------------------------


                Microsoft.Reporting.WebForms.ReportParameter sessiontrue = new Microsoft.Reporting.WebForms.ReportParameter();
                sessiontrue.Name = "sessiontrue";
                //if (Session["userName"] != null)
                //{
                sessiontrue.Values.Add("1");
                //}
                //else
                //{
                //    sessiontrue.Values.Add("0");
                //}

                // Microsoft.Reporting.WebForms.ReportParameter purposecode = new Microsoft.Reporting.WebForms.ReportParameter();
                // purposecode.Name = "purposecode";
                // string pCode = Request.QueryString["pCode"].ToString();
                // purposecode.Values.Add(pCode);
                pur.Values.Add(purvalue);
                Benfid.Values.Add(Benfvalue);
                IssuingBankid.Values.Add(IssuingBankvalue);
               
                if (Request.QueryString["rptCode"] == "1")
                {
                    ReportViewer1.ServerReport.SetParameters(
                       new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, enddate, sessiontrue,branch, pur,user });
                }
                else if (Request.QueryString["rptCode"] == "2" || Request.QueryString["rptCode"] == "3")
                {
                    ReportViewer1.ServerReport.SetParameters(
                  new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, enddate, sessiontrue, Benfid, pur, branch, user });
                }
                
                else if (Request.QueryString["rptCode"] == "4" || Request.QueryString["rptCode"] == "5")
                {
                    ReportViewer1.ServerReport.SetParameters(
                  new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, enddate, sessiontrue, IssuingBankid, pur, branch, user });
                }
                else if (Request.QueryString["rptCode"] == "6")
                {
                    ReportViewer1.ServerReport.SetParameters(
                       new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, enddate, sessiontrue, pur,branch, user });
                }
            }
        }
    }
}