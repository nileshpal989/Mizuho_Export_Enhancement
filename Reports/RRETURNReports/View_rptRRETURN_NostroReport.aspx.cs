using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class Reports_RRETURNReports_View_rptRRETURN_NostroReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            if (Request.QueryString["frm"] != null && Request.QueryString["to"] != null)
            {
                try
                {
                    string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;

                    ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                    IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                    ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                    ServerReport serverReport = ReportViewer1.ServerReport;
                    serverReport.ReportServerUrl = new Uri(url);

                    // Set the report server URL and report path

                    switch (Request.QueryString["Report"])
                    {
                        case "RRETURN Report to RBI(Nostro)":

                            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptRRETURN_Nostro";
                            break;

                        case "Consolidated R RETURN Report to RBI(Nostro)":
                            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptConsolRRETURN_Nostro";
                            break;
                    }

                    string frmdate = DateTime.ParseExact(Request.QueryString["frm"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                    string todate = DateTime.ParseExact(Request.QueryString["to"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");

                    Microsoft.Reporting.WebForms.ReportParameter startdate = new Microsoft.Reporting.WebForms.ReportParameter();
                    startdate.Name = "startdate";
                    startdate.Values.Add(frmdate);

                    Microsoft.Reporting.WebForms.ReportParameter enddate = new Microsoft.Reporting.WebForms.ReportParameter();
                    enddate.Name = "enddate";
                    enddate.Values.Add(todate);

                    Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
                    Branch.Name = "Branch";
                    string Branch1 = Request.QueryString["Branch"];
                    Branch.Values.Add(Branch1);


                    Microsoft.Reporting.WebForms.ReportParameter Currency = new Microsoft.Reporting.WebForms.ReportParameter();
                    Currency.Name = "cur";
                    string Currency1 = Request.QueryString["Currency"];
                    Currency.Values.Add(Currency1);

                    Microsoft.Reporting.WebForms.ReportParameter txtserialNo = new Microsoft.Reporting.WebForms.ReportParameter();
                    txtserialNo.Name = "txtserialNo";
                    string txtserialNo1 = Request.QueryString["txtserialNo"];
                    txtserialNo.Values.Add(txtserialNo1);



                    TF_DATA objData = new TF_DATA();

                    string Name = "";
                    string Designation = "";
                    string Emailid = "";
                    //string txtAuthSign = 

                    SqlParameter p1 = new SqlParameter("@AuthName", SqlDbType.VarChar);
                    p1.Value = Request.QueryString["txtAuthSign"].ToString();

                    string _query = "TF_rptGetAuthSignDetails";
                    DataTable dt = objData.getData(_query, p1);

                    if (dt.Rows.Count > 0)
                    {
                        Name = dt.Rows[0]["Authorised_Signatory"].ToString().Trim();
                        Designation = dt.Rows[0]["Designation"].ToString().Trim();
                        Emailid = dt.Rows[0]["Emailid"].ToString().Trim();
                    }

                    Microsoft.Reporting.WebForms.ReportParameter name = new Microsoft.Reporting.WebForms.ReportParameter();
                    name.Name = "name";
                    name.Values.Add(Name);

                    Microsoft.Reporting.WebForms.ReportParameter designation = new Microsoft.Reporting.WebForms.ReportParameter();
                    designation.Name = "designation";
                    designation.Values.Add(Designation);

                    Microsoft.Reporting.WebForms.ReportParameter emailid = new Microsoft.Reporting.WebForms.ReportParameter();
                    emailid.Name = "emailid";
                    emailid.Values.Add(Emailid);




                    switch (Request.QueryString["Report"])
                    {
                        case "RRETURN Report to RBI(Nostro)":

                            ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, enddate, Branch, Currency, name, designation, emailid, txtserialNo });
                            break;

                        case "Consolidated R RETURN Report to RBI(Nostro)":
                            ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, enddate, Currency, name, designation, emailid, txtserialNo });
                            break;
                    }

                    //ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, enddate, Branch, Currency, name, designation, emailid, txtserialNo });

                   
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}