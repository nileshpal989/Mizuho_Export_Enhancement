using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using ClosedXML.Excel;
using System.IO;

public partial class Reports_EXPORTReports_Export_Pending_Authorization_Transaction_Report : System.Web.UI.Page
{
    TF_DATA objdata = new TF_DATA();
    string fileName = "";
    string filePath = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {

            if (!IsPostBack)
            {
                fillddlBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Focus();
                btnGenerate.Attributes.Add("onclick", "return validateSave();");
            }
        }
    }
    public void fillddlBranch()
    {

        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objdata.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "0";
        ListItem li01 = new ListItem();
        li.Value = "1";

        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";
        ddlBranch.Items.Insert(0, li);
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
        labelMessage.Text = "";
        txtFromDate.Focus();
    }
    protected void btnGenerate_Click(object sender, System.EventArgs e)
    {
        if (ddlBranch.SelectedItem.Text == "---Select---")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select branch.')", true);
        }
        else if (txtFromDate.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select from doc date.')", true);
        }
        else if (txtToDate.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select To doc date.')", true);
        }
        else
        {
            int[] columnsToCenter = null;
            int[] columnsToRight = null;


            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            dateInfo.ShortDatePattern = "dd/MM/yyyy";
            DateTime From_Date = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
            DateTime To_Date = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);
            string selectedType = "";
            string todaydt = System.DateTime.Now.ToString("ddMMyyyy");

            string directoryPath = "", folderReport = "", excelsheetname = "", script = "", adcode = "";
            adcode = ddlBranch.SelectedValue.ToString();
            if (rbdlodgement.Checked == true)
            {
                selectedType = "Lodge";
                directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/ExcelReports/Pending_Auth_Transaction/Lodgement/") + adcode + "/" + todaydt;
                folderReport = "Lodgement";
                fileName = "TF_Export_Pending_Auth_Transaction_Lodgement_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xlsx";
                excelsheetname = "Lodgement";
                script = "TF_ExportPendingAuthTansaction_lodgement";
            }

            else if (rdbsettlement.Checked == true)
            {
                script = "TF_Export_Report_SETTLEMENT_PNDNGTRNSCN";
                selectedType = "settle";
                directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/ExcelReports/Pending_Auth_Transaction/Settlement/") + adcode + "/" + todaydt;
                folderReport = "Settlement";
                excelsheetname = "Settlement";
                fileName = "TF_Export_Pending_Auth_Transaction_Settlement_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xlsx";

            }

            else if (rdbIRM.Checked == true)
            {
                script = "TF_ExportPendingAuthTansaction_IRM";
                selectedType = "IRM";
                directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/ExcelReports/Pending_Auth_Transaction/IRM/") + adcode + "/" + todaydt;
                folderReport = "IRM";
                excelsheetname = "IRM";
                fileName = "TF_Export_Pending_Auth_Transaction_IRM_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xlsx";

            }

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            filePath = Path.Combine(directoryPath, fileName);
            SqlParameter p1 = new SqlParameter("@Branch", ddlBranch.SelectedItem.Text);
            SqlParameter p2 = new SqlParameter("@startdate", txtFromDate.Text);
            SqlParameter p3 = new SqlParameter("@enddate", txtToDate.Text);

            DataTable dt = objdata.getData(script, p1, p2, p3);
            string _directoryPath = "~/TF_GeneratedFiles/EXPORT/ExcelReports/Pending_Auth_Transaction/" + folderReport + "/" + adcode + "/" + todaydt;
            ViewState["_directoryPath"] = _directoryPath;
            ViewState["fileName"] = fileName;

            if (dt.Rows.Count > 0)
            {
                if (rbdlodgement.Checked == true)
                {
                    columnsToCenter = new int[] { 2, 17, 23, 25, 27, 29, 32, 40 };
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    var sheet = wb.Worksheets.Add(excelsheetname);
                    // Add headers and adjust column width

                    if (rbdlodgement.Checked == true)
                    {
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
                        foreach (var colLetter in new[] { "G", "J", "K", "AK", "BA", "BF" })
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
                    }

                    if (rdbsettlement.Checked == true)
                    {
                        int red = 31;
                        int green = 112;
                        int blue = 166;
                        var firstRow = sheet.Row(1);
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
                            sheet.Cell(startRow, startColumn + j).Value = dt.Columns[j].ColumnName;
                            // Calculate the width based on the length of the column header text
                            int headerLength = dt.Columns[j].ColumnName.Length;
                            // Set the column width to accommodate the header text
                            sheet.Column(j + 1).Width = headerLength * 1.2; // Adjust multiplier as needed
                            // Check if the current column name exists in columnsToAlign or columnsToAlign1
                            string columnName = dt.Columns[j].ColumnName;
                            if ((!columnsToAlign.Contains(columnName) && !exchrate.Contains(columnName)) || columnsToAlign.Contains(columnName) || exchrate.Contains(columnName))
                            {
                                // Set background color and bold property for non-blank fields
                                sheet.Cell(startRow, startColumn + j).Style.Fill.BackgroundColor = XLColor.FromArgb(red, green, blue);
                                sheet.Cell(startRow, startColumn + j).Style.Font.FontColor = XLColor.White;
                                sheet.Cell(startRow, startColumn + j).Style.Font.Bold = true;
                            }
                        }
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            // Fetch header name from DataTable and write to Excel
                            sheet.Cell(startRow, startColumn + j).Value = dt.Columns[j].ColumnName;
                        }
                        // Write data from DataTable starting from the second row
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                // Write each cell value to the worksheet
                                sheet.Cell(startRow + i + 1, startColumn + j).Value = dt.Rows[i][j];
                                string cellData = dt.Rows[i][j].ToString();

                                if (j == 48) // Zero-based index for column 43
                                {
                                    cellData = "'" + cellData; // Format as text
                                }
                                sheet.Cell(startRow + i + 1, startColumn + j).Value = cellData;

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
                        foreach (string columnName in columnsToAlign)
                        {
                            int columnIndex = dt.Columns.IndexOf(columnName) + 1; // Adding 1 because ClosedXML is 1-based index                                                
                            sheet.Column(columnIndex).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                            sheet.Column(columnIndex).Style.NumberFormat.Format = "#,##0.00"; // Set the number format as desired                        
                        }
                        foreach (string columnName1 in exchrate)
                        {
                            int columnIndex1 = dt.Columns.IndexOf(columnName1) + 1; // Adding 1 because ClosedXML is 1-based index                     
                            sheet.Column(columnIndex1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                            sheet.Column(columnIndex1).Style.NumberFormat.Format = "#,##0.00000"; // Set the number format as desired
                        }
                    }

                    if (rdbIRM.Checked == true)
                    {
                        int startRow = 1;
                        int startColumn = 1;
                        // Write headers from DataTable to the first row
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            // Fetch header name from DataTable and write to Excel
                            sheet.Cell(startRow, startColumn + j).Value = dt.Columns[j].ColumnName;
                            // Apply bold font style to the header cell
                            sheet.Cell(1, j + 1).Style.Font.Bold = true;
                            // Calculate the width based on the length of the column header text
                            int headerLength = dt.Columns[j].ColumnName.Length;
                            // Set the column width to accommodate the header text
                            sheet.Column(j + 1).Width = headerLength * 1.2; // Adjust multiplier as needed
                        }
                        // Write data from DataTable starting from the second row
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                // Write each cell value to the worksheet
                                sheet.Cell(startRow + i + 1, startColumn + j).Value = dt.Rows[i][j];
                                string cellData = dt.Rows[i][j].ToString();

                                if (j == 19)
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
                        List<string> columnsToAlign = new List<string> { "IRM_Amt", "ExchangeRate" };

                        foreach (string columnName in columnsToAlign)
                        {
                            int columnIndex = dt.Columns.IndexOf(columnName) + 1; // Adding 1 because ClosedXML is 1-based index
                            sheet.Column(columnIndex).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                            sheet.Column(columnIndex).Style.NumberFormat.Format = "#,##0.00"; // Set the number format as desired
                            //ws.Column(columnIndex).Style.NumberFormat.Format = "0.00";
                        }
                        List<string> columnsToAlign1 = new List<string> { "ExchangeRate" };
                        foreach (string columnName in columnsToAlign1)
                        {
                            int columnIndex = dt.Columns.IndexOf(columnName) + 1; // Adding 1 because ClosedXML is 1-based index
                            sheet.Column(columnIndex).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                            sheet.Column(columnIndex).Style.NumberFormat.Format = "#,##0.00000"; // Set the number format as desired
                            //ws.Column(columnIndex).Style.NumberFormat.Format = "0.00000";
                        }
                        List<string> columnsToAlign2 = new List<string> { "IRM_Creation_Date", "LEI_Expiry_Date" };
                        foreach (string columnName in columnsToAlign2)
                        {
                            int columnIndex = dt.Columns.IndexOf(columnName) + 1; // Adding 1 because ClosedXML is 1-based index
                            sheet.Column(columnIndex).Style.NumberFormat.Format = "dd/mm/yyyy"; // Set the number format as desired
                            //ws.Column(columnIndex).Style.NumberFormat.Format = "0.00000";
                        }
                        sheet.Tables.ForEach(t => t.Dispose());
                    }
                    TF_DATA objServerName = new TF_DATA();
                    string _serverName = objServerName.GetServerName();
                    labelMessage.Font.Bold = true;
                    labelMessage.Visible = true;

                    string path = "file://" + _serverName + "/TF_GeneratedFiles/EXPORT/ExcelReports/Pending_Auth_Transaction/" + folderReport + "/" + adcode + "/" + todaydt;
                    labelMessage.Text = "Files Saved Successfully on " + path;
                    wb.SaveAs(filePath);
                    ViewState["SuccessMessage"] = "Files Saved Successfully on " + path;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "downloadAlert", "setTimeout(function() { alert('Report Generated Successfully.'); __doPostBack('" + btnHidden.UniqueID + "', ''); }, 100);", true);
                }
            }
            else
            {
                txtFromDate.Text = "";
                txtFromDate.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('No records found for this date range.')", true);
            }
        }
    }
    protected void btnHidden_Click(object sender, EventArgs e)
    {
        if (ViewState["SuccessMessage"] != null)
        {
            string _dire = ViewState["_directoryPath"].ToString();
            string _direFile = ViewState["fileName"].ToString();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=" + _direFile);
            Response.TransmitFile(Server.MapPath(_dire + "/" + _direFile));
            Response.End();
            labelMessage.Font.Bold = true;
            labelMessage.Visible = true;
            string mess = ViewState["SuccessMessage"].ToString();
            labelMessage.Text = mess;
        }
    }
    protected void rbdlodgement_CheckedChanged(object sender, System.EventArgs e)
    {
        labelMessage.Text = "";
    }
    protected void rdbsettlement_CheckedChanged(object sender, System.EventArgs e)
    {
        labelMessage.Text = "";
    }
    protected void rdbIRM_CheckedChanged(object sender, System.EventArgs e)
    {
        labelMessage.Text = "";
    }
}