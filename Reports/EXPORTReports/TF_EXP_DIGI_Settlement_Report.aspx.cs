using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_EXPORTReports_TF_EXP_DIGI_Settlement_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblpath.Text = ""; labelMessage.Text = ""; lblmsg.Text = "";
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                ddlBranch.Focus();
                btnSave.Attributes.Add("onclick", "return validateGenerate();");
                getbranchdetails();
            }
            btnSave.Attributes.Add("onclick", "return validateGenerate();");
            labelMessage.Text = "";

        }

    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
        labelMessage.Text = "";
        txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //System.Threading.Thread.Sleep(5000);
            TF_DATA objdata = new TF_DATA();
            string script = "TF_Export_Report_Settlement_Digi", ID = "";


            SqlParameter p1 = new SqlParameter("@Branch", ddlBranch.SelectedItem.Text);
            SqlParameter p2 = new SqlParameter("@startdate", txtFromDate.Text.Trim());
            SqlParameter p3 = new SqlParameter("@enddate", txtToDate.Text.Trim());
            SqlParameter p4 = new SqlParameter("@ID", ID);
            DataTable dt = objdata.getData(script, p1, p2, p3, p4);


            string dateInfo = DateTime.Now.ToString("ddMMyyyy").Trim();
            string date = DateTime.Now.ToString("_ddMMyyyy_HHmmss").Trim();
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/Export/ExcelReports/Export Bill Realisation/" + dateInfo + "/");
            string FileName = "TF_Export_Bill_Realisation" + date + ".xlsx";
            ViewState["_directoryPath"] = ("~/TF_GeneratedFiles/Export/ExcelReports/Export Bill Realisation/" + dateInfo + "/");
            ViewState["FileName"] = FileName;

            if (dt.Rows.Count > 0)
            {
                if (!Directory.Exists(_directoryPath))
                {
                    Directory.CreateDirectory(_directoryPath);
                }
                ViewState["MyDataTable"] = dt;
                string _filePath = _directoryPath + FileName;
                using (XLWorkbook wb = new XLWorkbook())
                {
                    int red = 31;
                    int green = 112;
                    int blue = 166;
                    //var ws = wb.Worksheets.Add(dt, "Export Bill Realisation");
                    var ws = wb.Worksheets.Add("Export Bill Realisation");

                    var firstRow = ws.Row(1);
                    List<string> columnsToAlign = new List<string> { "Bill_Amount", "OSBillAmount", "InstructedAmount", "RealizedAmount", "FbankChanges",
                        "PCFC_Amt", "EEFC_Amt", "Cross Currency Amt", "ShippinBillAmount", "ShippingBillSettlement_Amt",
                        "IMP_ACC1_CR_Sundry_Deposit_Amt","IMP_ACC1_DR_Current_Acc_Amount","IMP_ACC2_CR_Sundry_Deposit_Amt","IMP_ACC2_DR_Current_Acc_Amount",
                        "IMP_ACC3_CR_Sundry_Deposit_Amt","IMP_ACC3_DR_Current_Acc_Amount","IMP_ACC4_CR_Sundry_Deposit_Amt","IMP_ACC4_DR_Current_Acc_Amount",
                        "IMP_ACC5_CR_Sundry_Deposit_Amt","IMP_ACC5_DR_Current_Acc_Amount"};
                    List<string> exchrate = new List<string> { "ExchangeRate", "InternalExchRate", "Cross Currency Rate","IMP_ACC2_Principal_Exch_Rate",
                        "IMP_ACC2_Principal_Intnl_Exch_Rate","IMP_ACC3_Principal_Exch_Rate","IMP_ACC3_Principal_Intnl_Exch_Rate","IMP_ACC4_Principal_Exch_Rate",
                        "IMP_ACC4_Principal_Intnl_Exch_Rate","IMP_ACC5_Principal_Exch_Rate","IMP_ACC5_Principal_Intnl_Exch_Rate"};
                    int startRow = 1;
                    int startColumn = 1;

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        // Fetch header name from DataTable and write to Excel
                        ws.Cell(startRow, startColumn + j).Value = dt.Columns[j].ColumnName;

                        // Check if the current column name exists in columnsToAlign or columnsToAlign1
                        string columnName = dt.Columns[j].ColumnName;
                        if ((!columnsToAlign.Contains(columnName) && !exchrate.Contains(columnName)) || columnsToAlign.Contains(columnName) || exchrate.Contains(columnName))
                        {
                            // Set background color and bold property for non-blank fields
                            ws.Cell(startRow, startColumn + j).Style.Fill.BackgroundColor = XLColor.FromArgb(red, green, blue);
                            ws.Cell(startRow, startColumn + j).Style.Font.FontColor = XLColor.White;
                            ws.Cell(startRow, startColumn + j).Style.Font.Bold = true;
                        }
                    }
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        // Fetch header name from DataTable and write to Excel
                        ws.Cell(startRow, startColumn + j).Value = dt.Columns[j].ColumnName;
                    }
                    // Write data from DataTable starting from the second row
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            // Write each cell value to the worksheet
                            ws.Cell(startRow + i + 1, startColumn + j).Value = dt.Rows[i][j];
                        }
                    }
                    foreach (string columnName in columnsToAlign)
                    {
                        int columnIndex = dt.Columns.IndexOf(columnName) + 1; // Adding 1 because ClosedXML is 1-based index                                                
                        ws.Column(columnIndex).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        ws.Column(columnIndex).Style.NumberFormat.Format = "#,##0.00"; // Set the number format as desired                        
                    }
                    foreach (string columnName1 in exchrate)
                    {
                        int columnIndex1 = dt.Columns.IndexOf(columnName1) + 1; // Adding 1 because ClosedXML is 1-based index                     
                        ws.Column(columnIndex1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        ws.Column(columnIndex1).Style.NumberFormat.Format = "#,##0.00000"; // Set the number format as desired
                    }
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        FileStream file = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
                        MyMemoryStream.WriteTo(file);
                        file.Close();
                        MyMemoryStream.Close();
                    }
                    txtFromDate.Focus();
                    lblpath.Text = "<b>" + ViewState["_directoryPath"].ToString().Trim() + "</b>";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyJQueryFunction", "alert('Report Generated Successfully..'); ClickAnotherButton();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyJQueryFunction", "setTimeout(function() { alert('Report Generated Successfully..');ClickAnotherButton();  setTimeout(ClickAnotherButton, 10); }, 10);", true);

                    lblmsg.Text = "<b> Report Generated Successfully.. </b>";
                }
            }
            else
            {
                txtFromDate.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "alert('No Record Found.');", true);
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = "There is some issue to generate the report.";
            string _Addedby = Session["UserName"].ToString();
            TF_DATA objSave = new TF_DATA();
            SqlParameter Report_Name = new SqlParameter("@Report_Name", "TF_Export_Bill_Realisation-Excel");
            SqlParameter Module = new SqlParameter("@Module", "Export".ToUpper());
            SqlParameter Menu = new SqlParameter("@Menu", "Report");
            SqlParameter Error_msg = new SqlParameter("@Error_msg", ex.Message);
            SqlParameter Error_code = new SqlParameter("@Error_code", ex.StackTrace);
            SqlParameter Error_time = new SqlParameter("@Error_time", System.DateTime.Now);
            SqlParameter Error_Part = new SqlParameter("@Error_Part", "During generating the report (Button Generate Click)");
            SqlParameter Addedby = new SqlParameter("@Addedby", _Addedby.ToUpper());
            string _query = "TF_EXP_Report_Log";
            string result = objSave.SaveDeleteData(_query, Report_Name, Module, Menu, Error_msg, Error_code, Error_time, Addedby);
        }
    }
    protected void btnexcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = (DataTable)ViewState["MyDataTable"];
            if (dt != null)
            {                
                string _directoryPath = ViewState["_directoryPath"].ToString();
                string FileName = ViewState["FileName"].ToString();
                Response.ContentType = "image/jpg";
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
                Response.TransmitFile(Server.MapPath(_directoryPath + "/" + FileName));                                
                Response.Flush();
                Response.End();
                lblmsg.Text = "Report Generated Successfully..";
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "alert('No Record Found For Download.');", true);
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = "There is some issue to download the report.";
            string _Addedby = Session["UserName"].ToString();
            TF_DATA objSave = new TF_DATA();
            SqlParameter Report_Name = new SqlParameter("@Report_Name", "TF_Export_Bill_Realisation-Excel");
            SqlParameter Module = new SqlParameter("@Module", "Export".ToUpper());
            SqlParameter Menu = new SqlParameter("@Menu", "Report");
            SqlParameter Error_msg = new SqlParameter("@Error_msg", ex.Message);
            SqlParameter Error_Place = new SqlParameter("@Error_Place", ex.StackTrace);
            SqlParameter Error_time = new SqlParameter("@Error_time", System.DateTime.Now);
            SqlParameter Error_Part = new SqlParameter("@Error_Part", "During Downloading the report");
            SqlParameter Addedby = new SqlParameter("@Addedby", _Addedby.ToUpper());
            string _query = "TF_EXP_Report_Log";
            string result = objSave.SaveDeleteData(_query, Report_Name, Module, Menu, Error_msg, Error_Place, Error_time, Error_Part, Addedby);
        }
    }
    private void LogError(Exception ex)
    {
        string method = ViewState["Method"].ToString();
        string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
        message += Environment.NewLine;
        message += "-----------------------------------------------------------";
        message += Environment.NewLine;
        message += string.Format("Message: {0}", ex.Message);
        message += Environment.NewLine;
        message += string.Format("StackTrace: {0}", ex.StackTrace);
        message += Environment.NewLine;
        message += string.Format("Source: {0}", ex.Source);
        message += Environment.NewLine;
        message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
        message += Environment.NewLine;
        message += "Method " + method;
        message += Environment.NewLine;
        message += "-----------------------------------------------------------";
        message += Environment.NewLine;

        string FileName = "ErrorLog.txt";
        string dateInfo = DateTime.Now.ToString("ddMMyyyy").Trim();
        string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/Export/ExcelReports/Export Bill Outstanding More Than 270/" + dateInfo + "/");
        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }
        //string path = Server.MapPath("~/TF_GeneratedFiles/Export/ExcelReports/Export Bill Outstanding/ErrorLog/" + dateInfo);
        string path = _directoryPath + "/" + FileName;
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(message);
            writer.Close();
        }
    }

    private void getbranchdetails()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchName";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
    }
}