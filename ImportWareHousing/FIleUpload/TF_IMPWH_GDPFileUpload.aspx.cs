using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using OfficeOpenXml;

public partial class ImportWareHousing_FIleUpload_TF_IMPWH_GDPFileUpload : System.Web.UI.Page
{
    string CNT = "";
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
                btnUpload.Attributes.Add("onclick", "return ShowProgress();");
            }
            btnValidate.Enabled = false;
            btnProcess.Enabled = false;
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (fileinhouse.HasFile)
        {
            string fname;
            fname = fileinhouse.FileName;

            //string path = Server.MapPath(fileinhouse.PostedFile.FileName);
            txtInputFile.Text = fileinhouse.PostedFile.FileName;
            if (fname.Contains(".xls") == true || fname.Contains(".xlsx") == true || fname.Contains(".XLS") == true || fname.Contains(".XLSX") == true)
            {
                string FileName = Path.GetFileName(fileinhouse.PostedFile.FileName);
                ViewState["FileName"] = FileName;
                if (FileName.Substring(0, 7) == "Payment")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You are trying to upload Payment File In Good To Pay File Upload! You can not upload this file!.')", true);
                    return;
                }
                string Extension = Path.GetExtension(fileinhouse.PostedFile.FileName);
                string FolderPath = Server.MapPath("../Uploaded_Files");

                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }

                FileName = FileName.Replace(" ", "");

                string FilePath = FolderPath + "\\" + System.IO.Path.GetFileName(fileinhouse.PostedFile.FileName);
                if (File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                }

                fileinhouse.SaveAs(FilePath);
                TF_DATA obj = new TF_DATA();
                SqlParameter PFileName = new SqlParameter("@FileName", FileName);
                string Result = obj.SaveDeleteData("TF_IMPWG_GDPCheckFileName", PFileName);
                hdnFilePath.Value = FilePath;
                hdnFileExtension.Value = Extension;
                hdnFileName.Value = FileName;
                ViewState["Status"] = "";
                if (Result == "Exist")
                {
                    ModalPopupExtender1.Show();
                }
                else
                {
                    //GetExcelSheets(FilePath, Extension, FileName);
                    //GetExcelUsingSpireXLS(FilePath, Extension, FileName);
                    //using (MemoryStream stream = new MemoryStream(fileinhouse.FileBytes))
                    GetExcelUsingEPPlus(FilePath, Extension, FileName);
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Excel File First.')", true);
        }
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        ViewState["Status"] = "Delete";
        GetExcelUsingEPPlus(hdnFilePath.Value, hdnFileExtension.Value, hdnFileName.Value);
    }
    private void GetExcelSheets(DataTable dt, string FileName)
    {
        string IECode, InvoiceNo, InvoiceCurrency, InvoiceAmount, TenorDays, SupplierName, ThirdPartyName, MaximumDueDate, DateOfShipment, SuppliersCountry, ThirdPartyCountry, NameOfShippingCompany, VesselAirCarrierName, TransportDocNo, PortOfLoading, PortOfDischarge, RawMaterialCapitalGoods, GoodsDescription, CountryOfOrigin, BOENo, BOEDate, ADCode, GoodToPayDiscrepant, OutstandingAmountOfInvoice, AmountTobeRemitted, InvoicesToBePaid, Currency, PaymentDate, Remark, PaymentCurrency;
        try
        {
            int norecinexcel = 0;
            int cntTot = 0;
            TF_DATA obj = new TF_DATA();
            int RowCount = dt.Rows.Count;
            int ColCount = dt.Columns.Count;
            string result = obj.SaveDeleteData("TF_IMPWH_DeleteTempGDPData");
            ViewState["IECode"] = dt.Rows[3][0].ToString().Trim();
            if (dt.Columns.Count == 30)
            {
                if (dt.Rows.Count > 1)
                {
                    if (dt.Rows[0][0].ToString().Trim() == "Digital Import Payment System")
                    {
                        for (int i = 5; i < RowCount; i++)
                        {
                            if (dt.Rows[i][0].ToString().Trim() != "")
                            {
                                if (dt.Rows[i][25].ToString().Trim() != "")
                                {
                                    norecinexcel = norecinexcel + 1;

                                    IECode = ViewState["IECode"].ToString().Trim();
                                    SqlParameter PIECode = new SqlParameter("@IECode", IECode);

                                    InvoiceNo = dt.Rows[i][1].ToString().Trim();
                                    SqlParameter PInvoiceNo = new SqlParameter("@InvoiceNo", InvoiceNo);

                                    InvoiceCurrency = dt.Rows[i][2].ToString().Trim();
                                    SqlParameter PInvoiceCurrency = new SqlParameter("@InvoiceCurrency", InvoiceCurrency);

                                    InvoiceAmount = dt.Rows[i][3].ToString().Trim();
                                    SqlParameter PInvoiceAmount = new SqlParameter("@InvoiceAmount", InvoiceAmount);

                                    TenorDays = dt.Rows[i][4].ToString().Trim();
                                    SqlParameter PTenorDays = new SqlParameter("@PaymentTermasperInvoice", TenorDays);

                                    string PaymentDueDate = dt.Rows[i][5].ToString();
                                    SqlParameter PPaymentDueDate = new SqlParameter("@PaymentDueDate", PaymentDueDate);

                                    SupplierName = dt.Rows[i][6].ToString().Trim();
                                    SqlParameter PSupplierName = new SqlParameter("@SupplierName", SupplierName);

                                    ThirdPartyName = dt.Rows[i][7].ToString().Trim();
                                    SqlParameter PThirdPartyName = new SqlParameter("@ThirdPartyName", ThirdPartyName);

                                    MaximumDueDate = dt.Rows[i][8].ToString().Trim();
                                    SqlParameter PMaximumDueDate = new SqlParameter("@MaximumDueDate", MaximumDueDate);

                                    DateOfShipment = dt.Rows[i][9].ToString().Trim();
                                    SqlParameter PDateOfShipment = new SqlParameter("@DateOfShipment", DateOfShipment);

                                    SuppliersCountry = dt.Rows[i][10].ToString().Trim();
                                    SqlParameter PSuppliersCountry = new SqlParameter("@SuppliersCountry", SuppliersCountry);

                                    ThirdPartyCountry = dt.Rows[i][11].ToString().Trim();
                                    SqlParameter PThirdPartyCountry = new SqlParameter("@ThirdPartyCountry", ThirdPartyCountry);

                                    NameOfShippingCompany = dt.Rows[i][12].ToString().Trim();
                                    SqlParameter PNameOfShippingCompany = new SqlParameter("@NameOfShippingCompany", NameOfShippingCompany);

                                    VesselAirCarrierName = dt.Rows[i][13].ToString().Trim();
                                    SqlParameter PVesselAirCarrierName = new SqlParameter("@VesselAirCarrierName", VesselAirCarrierName);

                                    TransportDocNo = dt.Rows[i][14].ToString().Trim();
                                    SqlParameter PTransportDocNo = new SqlParameter("@TransportDocNo", TransportDocNo);

                                    PortOfLoading = dt.Rows[i][15].ToString().Trim();
                                    SqlParameter PPortOfLoading = new SqlParameter("@PortOfLoading", PortOfLoading);

                                    PortOfDischarge = dt.Rows[i][16].ToString().Trim();
                                    SqlParameter PPortOfDischarge = new SqlParameter("@PortOfDischarge", PortOfDischarge);

                                    RawMaterialCapitalGoods = dt.Rows[i][17].ToString().Trim();
                                    SqlParameter PRawMaterialCapitalGoods = new SqlParameter("@RawMaterialCapitalGoods", RawMaterialCapitalGoods);

                                    GoodsDescription = dt.Rows[i][18].ToString().Trim();
                                    SqlParameter PGoodsDescription = new SqlParameter("@GoodsDescription", GoodsDescription);

                                    CountryOfOrigin = dt.Rows[i][19].ToString().Trim();
                                    SqlParameter PCountryOfOrigin = new SqlParameter("@CountryOfOrigin", CountryOfOrigin);

                                    BOENo = dt.Rows[i][20].ToString().Trim();
                                    SqlParameter PBOENo = new SqlParameter("@BOENo", BOENo);

                                    BOEDate = dt.Rows[i][21].ToString().Trim();
                                    SqlParameter PBOEDate = new SqlParameter("@BOEDate", BOEDate);

                                    ADCode = dt.Rows[i][22].ToString().Trim();
                                    SqlParameter PADCode = new SqlParameter("@ADCode", ADCode);

                                    GoodToPayDiscrepant = dt.Rows[i][23].ToString().Trim();
                                    SqlParameter PGoodToPayDiscrepant = new SqlParameter("@GoodToPayDiscrepant", GoodToPayDiscrepant);

                                    OutstandingAmountOfInvoice = dt.Rows[i][24].ToString().Trim();
                                    SqlParameter POutstandingAmountOfInvoice = new SqlParameter("@OutstandingAmountOfInvoice", OutstandingAmountOfInvoice);

                                    InvoicesToBePaid = dt.Rows[i][25].ToString().Trim();
                                    SqlParameter PInvoicesToBePaid = new SqlParameter("@InvoicesToBePaid", InvoicesToBePaid);

                                    Currency = dt.Rows[i][26].ToString().Trim();
                                    SqlParameter PCurrency = new SqlParameter("@Currency", Currency);

                                    AmountTobeRemitted = dt.Rows[i][27].ToString().Trim();
                                    SqlParameter PAmountTobeRemitted = new SqlParameter("@AmountTobeRemitted", AmountTobeRemitted);

                                    Remark = dt.Rows[i][28].ToString().Trim();
                                    SqlParameter PRemark = new SqlParameter("@Remark", Remark);

                                    if (dt.Rows[i][29].ToString().Trim() == "")
                                    {
                                        PaymentCurrency = dt.Rows[i][26].ToString().Trim();
                                    }
                                    else
                                    {
                                        PaymentCurrency = dt.Rows[i][29].ToString().Trim();
                                    }
                                    SqlParameter PPaymentCurrency = new SqlParameter("@PaymentCurrency", PaymentCurrency);

                                    SqlParameter PFileName = new SqlParameter("@FileName", FileName);

                                    string UserName = Session["userName"].ToString().Trim();
                                    SqlParameter PUserName = new SqlParameter("@UserName", UserName);

                                    TF_DATA objDataInput = new TF_DATA();

                                    string qryInput = "TF_IMPWH_GDPFileUpload";
                                    string dtInput = objDataInput.SaveDeleteData(qryInput, PIECode, PInvoiceNo, PInvoiceCurrency, PInvoiceAmount, PTenorDays, PPaymentDueDate, PSupplierName, PThirdPartyName, PMaximumDueDate, PDateOfShipment, PSuppliersCountry, PThirdPartyCountry, PNameOfShippingCompany, PVesselAirCarrierName, PTransportDocNo, PPortOfLoading, PPortOfDischarge, PRawMaterialCapitalGoods, PGoodsDescription, PCountryOfOrigin, PBOENo, PBOEDate, PADCode, PGoodToPayDiscrepant, POutstandingAmountOfInvoice, PAmountTobeRemitted, PInvoicesToBePaid, PCurrency, PPaymentCurrency, PUserName, PRemark, PFileName);

                                    if (dtInput == "Uploaded")
                                    {
                                        cntTot++;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invalid File Format. Check Excel File.')", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invalid File Format. Check Excel File.')", true);
            }
            if (lblHint.Text == "")
            {
                if (cntTot > 0)
                {
                    labelMessage.Text = "<font color='red'>" + cntTot + "</font>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Records Uploaded out of " + "<font color='red'>" + norecinexcel + "</font>" + " from file " + FileName;
                    btnValidate.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            labelMessage.Text = ex.Message;
        }
    }
    protected void btnValidate_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        string _IECode = ViewState["IECode"].ToString().Trim();
        string _Query = "TF_IMPWH_GDPData_ValidateP";
        SqlParameter PIECode = new SqlParameter("IECode", _IECode);
        DataTable dt = obj.getData(_Query, PIECode);
        if (dt.Rows.Count > 0)
        {
            lblHint.Text = "<font color='red'>" + "Please Correct All Errors Then You Can Process Data.." + "</font>";
            label2.Text = "<font color='red'>" + dt.Rows.Count + "</font>" + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Error Records ";
            string _script = "window.open('../../Reports/IMPWHReports/TF_IMPWH_ViewGDPDataValidation.aspx?Type=GTP&IECode=" + _IECode + "','_blank','height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", _script, true);
            btnValidate.Enabled = true;
        }
        else
        {
            lblHint.Text = "<font color='red'>" + "There is no Error Records You Can Process Data.." + "</font>";
            btnProcess.Enabled = true;
            label2.Text = "<font color='red'>" + dt.Rows.Count + "</font>" + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Error Records ";
        }
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        if (ViewState["Status"].ToString().Trim() == "Delete")
        {
            SqlParameter PFileName = new SqlParameter("@FileName", hdnFileName.Value);
            string result = obj.SaveDeleteData("TF_IMPWH_GDPDataDeleteReUpload", PFileName);
        }
        string _result = obj.SaveDeleteData("TF_IMPWH_GDPFileProcess");
        if (_result != "0")
        {
            SqlParameter PFileName = new SqlParameter("@FileName", ViewState["FileName"].ToString().Trim());
            SqlParameter PFileType = new SqlParameter("@FileType", "GDP");
            SqlParameter PUserName = new SqlParameter("@UserName", Session["UserName"].ToString().Trim());
            SqlParameter PRows = new SqlParameter("@Rows", _result);
            string result = obj.SaveDeleteData("TF_IMPWH_InsertUploadeFile", PFileName, PFileType, PUserName, PRows);
            label3.Text = "<font color='red'>" + _result + "</font>" + " Record Processed.";
        }
        //InsertUplodedFile();
    }
    //public void InsertUplodedFile()
    //{
    //    TF_DATA obj = new TF_DATA();
    //    SqlParameter PFileName = new SqlParameter("@FileName", ViewState["FileName"].ToString().Trim());
    //    SqlParameter PFileType = new SqlParameter("@FileType", "GDP");
    //    SqlParameter PUserName = new SqlParameter("@UserName", Session["UserName"].ToString().Trim());
    //    string result = obj.SaveDeleteData("TF_IMPWH_InsertUploadeFile", PFileName, PFileType, PUserName);
    //}
    private void GetExcelUsingEPPlus(string FilePath, string Extension, string FileName)
    {
        try
        {
            DataTable dt = new DataTable();
            ExcelWorksheet worksheet;
            ExcelPackage excelPackage;
            FileInfo File = new FileInfo(FilePath);
            try
            {
                excelPackage = new ExcelPackage(File);
                worksheet = excelPackage.Workbook.Worksheets[1];

                ////check if the worksheet is completely empty
                //if (worksheet.Dimension == null)
                //{

                //}
                //create a list to hold the column names
                List<string> columnNames = new List<string>();
                //needed to keep track of empty column headers
                int currentColumn = 1;
                //loop all columns in the sheet and add them to the datatable
                foreach (var cell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
                {
                    string columnName = cell.Text.Trim();
                    //check if the previous header was empty and add it if it was
                    if (cell.Start.Column != currentColumn)
                    {
                        columnNames.Add("Header_" + currentColumn);
                        dt.Columns.Add("Header_" + currentColumn);
                        currentColumn++;
                    }
                    //add the column name to the list to count the duplicates
                    columnNames.Add(columnName);
                    //count the duplicate column names and make them unique to avoid the exception
                    //A column named 'Name' already belongs to this DataTable
                    int occurrences = columnNames.Count(x => x.Equals(columnName));
                    if (occurrences > 1)
                    {
                        columnName = columnName + "_" + occurrences;
                    }
                    //add the column to the datatable
                    dt.Columns.Add(columnName);
                    currentColumn++;
                }
                //start adding the contents of the excel file to the datatable
                for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
                {
                    var row = worksheet.Cells[i, 1, i, worksheet.Dimension.End.Column];
                    DataRow newRow = dt.NewRow();
                    //loop all cells in the row
                    foreach (var cell in row)
                    {
                        var cellval = cell.Address;
                        var FixVal = "F" + i;
                        if (cellval != FixVal)
                        {
                            newRow[cell.Start.Column - 1] = cell.Text;
                        }
                        else
                        {
                            if (cell.RichText.Text != "" && i > 5)
                            {
                                DateTime date = Convert.ToDateTime(cell.RichText.Text);
                                string _Date = date.ToString("dd/MM/yyyy");
                                newRow[cell.Start.Column - 1] = _Date;
                            }
                            else
                            {
                                newRow[cell.Start.Column - 1] = cell.Text;
                            }
                        }
                    }
                    dt.Rows.Add(newRow);
                }
            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Decrypt Excel File First.')", true);

                //string[] files = System.IO.Directory.GetFiles(FilePath);
                //// Copy the files and overwrite destination files if they already exist.
                //foreach (string s in files)
                //{
                //    // Use static Path methods to extract only the file name from the path.
                //    fileName = System.IO.Path.GetFileName(s);
                //    destFile = System.IO.Path.Combine(targetPath, fileName);
                //    System.IO.File.Copy(s, destFile, true);
                //}
                return;
            }
            GetExcelSheets(dt, FileName);
        }
        catch (Exception Ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + Ex.Message + "')", true);
            return;
        }
    }
    private void GetExcelUsingInterop(string FilePath, string Extension, string FileName)
    {
        DataTable dt = new DataTable();
        Microsoft.Office.Interop.Excel.Application objXL = null;
        Microsoft.Office.Interop.Excel.Workbook objWB = null;
        Microsoft.Office.Interop.Excel.Range range;
        try
        {
            objXL = new Microsoft.Office.Interop.Excel.Application();
            objWB = objXL.Workbooks.Open(FilePath);
            foreach (Microsoft.Office.Interop.Excel.Worksheet objSHT in objWB.Worksheets)
            {
                range = objSHT.UsedRange;
                int rows = objSHT.UsedRange.Rows.Count;
                int cols = objSHT.UsedRange.Columns.Count;
                int noofrow = 1;
                for (int c = 1; c <= cols; c++)
                {
                    string colname = (string)(range.Cells[1, c] as Microsoft.Office.Interop.Excel.Range).Value;
                    dt.Columns.Add(c.ToString());
                    noofrow = 1;
                }
                for (int r = noofrow; r <= rows; r++)
                {
                    DataRow dr = dt.NewRow();
                    for (int c = 1; c <= cols; c++)
                    {
                        if (c != 6)
                        {
                            dr[c - 1] = (range.Cells[r, c] as Microsoft.Office.Interop.Excel.Range).Value;
                        }
                        else
                        {
                            if (r != 5)
                            {
                                DateTime _DateTimeDueDate = Convert.ToDateTime((range.Cells[r, c] as Microsoft.Office.Interop.Excel.Range).Value);
                                dr[c - 1] = _DateTimeDueDate.ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                dr[c - 1] = (range.Cells[r, c] as Microsoft.Office.Interop.Excel.Range).Value;
                            }
                        }
                    }
                    dt.Rows.Add(dr);
                }
            }
            objWB.Close();
            objXL.Quit();
        }
        catch (Exception Ex)
        {
            objWB.Saved = true;
            objWB.Close();
            objXL.Quit();
        }
        GetExcelSheets(dt, FileName);
    }
}