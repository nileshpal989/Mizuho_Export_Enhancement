using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;
using System.Configuration;
public partial class IMP_IMPReports_TF_IMP_ForAcceptance : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["INWConnectionString"].ToString());
    string com;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string frmdate =Request.QueryString["fromdate"];
            string ToDate = Request.QueryString["toDate"];
            string Document_Type = Request.QueryString["Type"];
            string branch = Request.QueryString["Branch"];
            string Status = Request.QueryString["Status"];
            string username = Session["userName"].ToString();
            reportview(frmdate, ToDate, Document_Type, branch, username, Status);
            txtfromdate.Text = frmdate;
            txttodate.Text = ToDate;
            DocumentTypeList();
            BranchList();
            StatusTypeList();
        }
    }
    protected void reportview(string frmdate, string ToDate, string employees, string branch, string username, string Status)
    {
        string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
        // Set the processing mode for the ReportViewer to Remote

        ReportViewer1.ProcessingMode = ProcessingMode.Remote;
        IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
        ReportViewer1.ServerReport.ReportServerCredentials = irsc;
        ServerReport serverReport = ReportViewer1.ServerReport;
        serverReport.ReportServerUrl = new Uri(url);

        if (Request.QueryString["LodgmentData"] == "Lodg")
        {
            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/CheckerReport_DashBoard";
        }
      
        else if (Request.QueryString["Acceptance"] == "Accp")
        {
           serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/ImportBillAcceptance_report_dashboard";
        }
       // else if (Request.QueryString["Buttontype"] == "Sett")
       // {
       //     serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/IMP_TF_Settlement_DashBoard";

       // }
        Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
        user.Name = "UserName";
        user.Values.Add(username);

        Microsoft.Reporting.WebForms.ReportParameter from = new Microsoft.Reporting.WebForms.ReportParameter();
        from.Name = "startdate";

        Microsoft.Reporting.WebForms.ReportParameter to = new Microsoft.Reporting.WebForms.ReportParameter();
        to.Name = "enddate";

        Microsoft.Reporting.WebForms.ReportParameter branchs = new Microsoft.Reporting.WebForms.ReportParameter();
        branchs.Name = "Branch";

        Microsoft.Reporting.WebForms.ReportParameter Document_Type = new Microsoft.Reporting.WebForms.ReportParameter();
        Document_Type.Name = "Document_Type";

        Microsoft.Reporting.WebForms.ReportParameter Statuss = new Microsoft.Reporting.WebForms.ReportParameter();
        Statuss.Name = "Status";


        from.Values.Add(frmdate);
        to.Values.Add(ToDate);
        Document_Type.Values.Add(employees);
        branchs.Values.Add(branch);
        Statuss.Values.Add(Status);

        ReportViewer1.ServerReport.SetParameters(
           new Microsoft.Reporting.WebForms.ReportParameter[] { from, to, Document_Type, branchs, user, Statuss });


    }
    protected void DocumentTypeList()
    {

        conn.Open();
        // string com = "select UserName from TF_Users where Active='1' and not UserName='Admin'";
        if (Request.QueryString["LodgmentData"] == "Lodg" || Request.QueryString["Buttontype"] == "Sett")
        {
            com = "select distinct Document_Type from TF_IMP_BOE";
        }
        else if (Request.QueryString["Acceptance"] == "Accp")
        {
            com = "select distinct Document_Type from TF_IMP_BOE where Document_Type in ('ACC','ICU')";
        }
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        adpt.SelectCommand.CommandType = CommandType.Text;
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        DropDownList1.DataSource = dt;
        DropDownList1.DataTextField = "Document_Type";
        DropDownList1.DataValueField = "Document_Type";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, new ListItem("All"));
        conn.Close();
    }


    protected void BranchList()
    {
        conn.Open();
        if (Request.QueryString["LodgmentData"] == "Lodg" || Request.QueryString["Buttontype"] == "Sett")
        {
            com = "select BranchName,AuthorizedDealerCode from TF_Branch";
        }
        else if (Request.QueryString["Acceptance"] == "Accp")
        {
            com = "select BranchName from TF_Branch";
        }
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        adpt.SelectCommand.CommandType = CommandType.Text;
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        DropDownList2.DataSource = dt;
        DropDownList2.DataTextField = "BranchName";
        if (Request.QueryString["LodgmentData"] == "Lodg")
        {
            DropDownList2.DataValueField = "AuthorizedDealerCode";
        }
        else if (Request.QueryString["Acceptance"] == "Accp")
        {
            DropDownList2.DataValueField = "BranchName";
        }
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, new ListItem("All"));
        conn.Close();
    }


    protected void StatusTypeList()
    {

        conn.Open();
        com = "TF_IMP_GetStatus";
        SqlDataAdapter adpt = new SqlDataAdapter(com, conn);
        adpt.SelectCommand.CommandType = CommandType.StoredProcedure;
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        DropDownList3.DataSource = dt;
        DropDownList3.DataTextField = "Checker_Status";
        DropDownList3.DataValueField = "Checker_value";
        DropDownList3.DataBind();
        DropDownList3.Items.Insert(0, new ListItem("All"));
        conn.Close();

    }
    protected void Search_Click(object sender, EventArgs e)
    {
        string frmdate = txtfromdate.Text;
        string ToDate = txttodate.Text;
        string Document_Type = DropDownList1.SelectedValue.ToString();
        string branch = DropDownList2.SelectedValue.ToString();
        string Status = DropDownList3.SelectedValue.ToString();
        string username = Session["userName"].ToString();
        reportview(frmdate, ToDate, Document_Type, branch, username, Status);
    }
}