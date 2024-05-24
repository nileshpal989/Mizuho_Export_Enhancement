using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading;
using DocumentFormat.OpenXml;

public partial class Reports_EXPORTReports_TF_EXP_ExportDigiLodgementReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            if (!IsPostBack)
            {
                clearControls();
                fillddlBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                txtFromDate.Focus();
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");                
                btnSave.Attributes.Add("onclick", "return validateSave();");
            }
        }
    }
    public void fillddlBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        //li.Value = "---Select---";
        ListItem li01 = new ListItem();
        li01.Value = "All Branches";
        if (dt.Rows.Count > 0)
        {
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";      

    }
    protected void clearControls()
    {
        txtFromDate.Text = "";

        txtFromDate.Focus();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
           // System.Threading.Thread.Sleep(5000);
            string folder = "", fileName = "", script = "";
            string todaydt = System.DateTime.Now.ToString("ddMMyyyy");
            if (rdbLodgment.Checked == true)
            {
                folder = "Lodgement";
                fileName = "Export_Digi_Lodgement_Data" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xlsx";
                script = "TF_IRMExportDigilodgement";
            }
            if (rdbSettlement.Checked == true)
            {
                folder = "Settlement";
                fileName = "Export_Digi_Settlement_Data" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xlsx";
                script = "TF_Export_Report_Settlement_Digi";
            }
            string directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/ExcelReports/Digi Export/" + folder + "/") + ddlBranch.SelectedValue.ToString() + "/" + todaydt;
            string _directoryPath = "~/TF_GeneratedFiles/EXPORT/ExcelReports/Digi Export/" + folder + "/" + ddlBranch.SelectedValue.ToString() + "/" + todaydt;
            string filePath = Path.Combine(directoryPath, fileName);
            ViewState["_directoryPath"] = _directoryPath;
            ViewState["fileName"] = fileName;

            TF_DATA objdata = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@Branch", ddlBranch.SelectedItem.Text);
            SqlParameter p2 = new SqlParameter("@startdate", txtFromDate.Text.ToString());
            SqlParameter p3 = new SqlParameter("@enddate", txtToDate.Text.ToString()); ;

            DataTable dt = objdata.getData(script, p1, p2, p3);

            if (dt.Rows.Count > 0)
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                if (rdbLodgment.Checked == true)
                {
                    int[] columnsToCenter = { 2, 17, 23, 25, 27, 29, 32, 40 };
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var sheet = wb.Worksheets.Add("Export_DigiLodgement");

                        // Add headers and adjust column width
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            sheet.Cell(1, j + 1).Value = dt.Columns[j].ColumnName;
                            // Apply bold font style to the header cell
                            sheet.Cell(1, j + 1).Style.Font.Bold = true;
                            // Calculate the width based on the length of the column header text
                            int headerLength = dt.Columns[j].ColumnName.Length;
                            // Set the column width to accommodate the header text
                            sheet.Column(j + 1).Width = headerLength * 1.2; // Adjust multiplier as needed
                        }
                        // Add data without creating a table and adjust column width based on data length
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                string cellData = dt.Rows[i][j].ToString();

                                if ((j == 22 || j == 33 || j == 38))
                                {
                                    cellData = "'" + cellData; // Text format
                                }

                                sheet.Cell(i + 2, j + 1).Value = cellData;

                                // Calculate the width based on the length of the cell data
                                int dataLength = cellData.Length;
                                // Get the current column width
                                double currentWidth = sheet.Column(j + 1).Width;
                                // Set the column width to accommodate the longest cell data
                                if (dataLength * 1.2 > currentWidth)
                                {
                                    sheet.Column(j + 1).Width = dataLength * 1.2; // Adjust multiplier as needed
                                }
                            }
                        }
                        // Format columns as numeric
                        foreach (var colLetter in new[] { "G", "J", "K", "AL", "BB", "BG" })
                        {
                            var column = sheet.Column(colLetter);
                            column.Style.NumberFormat.Format = "#,##0.00";  // Numeric format
                        }

                        foreach (var colLetter in new[] { "I" })
                        {
                            var column = sheet.Column(colLetter);
                            column.Style.NumberFormat.Format = "#,##0.00000";  // Numeric format
                        }


                        foreach (var columnIndex in columnsToCenter)
                        {
                            //foreach (var row in sheet.RowsUsed().Skip(1))
                            foreach (var row in sheet.RowsUsed())
                            {
                                row.Cell(columnIndex + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            }

                            var column = sheet.Column(columnIndex + 1);
                            column.Style.NumberFormat.Format = "dd/mm/yyyy"; // Date format
                        }



                        TF_DATA objServerName = new TF_DATA();
                        string _serverName = objServerName.GetServerName();
                        labelMessage.Font.Bold = true;
                        labelMessage.Visible = true;

                        string path = "file://" + _serverName + "/TF_GeneratedFiles/EXPORT/ExcelReports/DigiLodgement/Digi Export/" + folder + "/" + ddlBranch.SelectedValue.ToString() + "/" + todaydt;
                        labelMessage.Text = "Files Saved Successfully on " + path;
                        wb.SaveAs(filePath);
                        ViewState["SuccessMessage"] = "Files Saved Successfully on " + path;

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "downloadAlert", "setTimeout(function() { alert('Report Generated Successfully.'); __doPostBack('" + btnHidden.UniqueID + "', ''); }, 100);", true);

                    }
                }
                if (rdbSettlement.Checked == true)
                {
                    ViewState["MyDataTable"] = dt;
                    string _filePath = directoryPath + "/" + fileName;
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        int red = 31;
                        int green = 112;
                        int blue = 166;
                        //var ws = wb.Worksheets.Add(dt, "Export Bill Realisation");
                        var ws = wb.Worksheets.Add("Export_DigiSettlement");

                        var firstRow = ws.Row(1);
                        List<string> columnsToAlign = new List<string> { "Bill_Amount", "OSBillAmount", "InstructedAmount", "RealizedAmount",
                        "PCFC_Amt", "EEFC_Amt", "Cross Currency Amt", "ShippinBillAmount", "ShippingBillSettlement_Amt",
                        "IMP_ACC1_CR_Sundry_Deposit_Amt","IMP_ACC1_DR_Current_Acc_Amount","IMP_ACC2_CR_Sundry_Deposit_Amt","IMP_ACC2_DR_Current_Acc_Amount",
                        "IMP_ACC3_CR_Sundry_Deposit_Amt","IMP_ACC3_DR_Current_Acc_Amount","IMP_ACC4_CR_Sundry_Deposit_Amt","IMP_ACC4_DR_Current_Acc_Amount",
                        "IMP_ACC5_CR_Sundry_Deposit_Amt","IMP_ACC5_DR_Current_Acc_Amount"};
                        List<string> exchrate = new List<string> { "ExchangeRate", "InternalExchRate","FbankChanges", "Cross Currency Rate","IMP_ACC2_Principal_Exch_Rate",
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
                                string cellData = dt.Rows[i][j].ToString();

                                if (j == 48) // Zero-based index for column 43
                                {
                                    cellData = "'" + cellData; // Format as text
                                }

                                ws.Cell(startRow + i + 1, startColumn + j).Value = cellData;
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
                        labelMessage.Text = "<b>" + ViewState["_directoryPath"].ToString().Trim() + "</b>";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "downloadAlert", "setTimeout(function() { alert('Report Generated Successfully.'); __doPostBack('" + btnHidden.UniqueID + "', ''); }, 100);", true);

                        lblmsg.Text = "<b> Report Generated Successfully.. </b>";
                    }
                }
            }
            else
            {
                txtFromDate.Text = "";
                txtFromDate.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('No records found for this date range.')", true);
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
    protected void btnHidden_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["SuccessMessage"] != null)
            {
                string fileName = ViewState["fileName"].ToString();
                string _dire = ViewState["_directoryPath"].ToString();
                string _direFile = ViewState["fileName"].ToString();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                Response.TransmitFile(Server.MapPath(_dire + "/" + _direFile));
                Response.End();
                labelMessage.Font.Bold = true;
                labelMessage.Visible = true;
                string mess = ViewState["SuccessMessage"].ToString();
                labelMessage.Text = mess;

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
}