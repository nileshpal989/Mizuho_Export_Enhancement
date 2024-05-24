using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using ClosedXML.Excel;
using System.Web.Services;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using OfficeOpenXml.DataValidation;

public partial class ImportWareHousing_FileCreation_TF_IMPWH_GDPExcelFileCreation : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                lblPaheHeader.Text = Request.QueryString["PageHeader"].ToString();
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;
                //btnCustHelp.Attributes.Add("Onclick", "CustHelp();");
                btnCreate.Attributes.Add("onclick", "return Validate();");
                txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
    }
    protected void fillBranch()
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
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
    }
    protected void txtCustACNo_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@accno", SqlDbType.VarChar);
        p1.Value = txtCustACNo.Text;
        string _query = "Get_Customer_Name_BY_AccNo";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblcustname.Text = dt.Rows[0]["CUST_NAME"].ToString();
            btnCreate.Focus();
        }
        else
        {
            txtCustACNo.Text = "";
            lblcustname.Text = "";
            txtCustACNo.Focus();
        }
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        string Type = Request.QueryString["Type"].ToString();
        if (Type == "GDP")
        {
            CreateExcelForGDPUsingEPPlus();
        }
        else
        {
            CreteExcelForPaymentUsingEPPlus();
        }
    }
    public void CreateExcelForGDPUsingEPPlus()
    {
        try
        {
            DataTable dtCustomer = new DataTable();
            string _FromDate = txtFromDate.Text.ToString();
            string _ADCode = ddlBranch.SelectedValue.ToString();
            string Day = _FromDate.Substring(0, 2);
            string Cust = txtCustACNo.Text.Trim();
            SqlParameter P1 = new SqlParameter("@Day", Day);
            SqlParameter P2 = new SqlParameter("@Cust", Cust);
            TF_DATA obj = new TF_DATA();
            dtCustomer = obj.getData("TF_IMPWH_GetCustomerDetialsToSendMailForPayment", P1, P2);
            int _RecordCount = 0;
            if (dtCustomer.Rows.Count > 0)
            {
                for (int i = 0; i < dtCustomer.Rows.Count; i++)
                {
                    string _IECode = dtCustomer.Rows[i]["CUST_IE_CODE"].ToString();
                    string _CustName = dtCustomer.Rows[i]["CUST_NAME"].ToString();
                    string _CustAcNo = dtCustomer.Rows[i]["CUST_ACCOUNT_NO"].ToString();
                    string _CustAbrr = dtCustomer.Rows[i]["Cust_Abbr"].ToString();
                    string _EmailID = dtCustomer.Rows[i]["CUST_EMAIL_ID"].ToString();
                    System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
                    dateInfo.ShortDatePattern = "dd/MM/yyyy";
                    string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPWH/GTP/");

                    SqlParameter p1 = new SqlParameter("@IECode", SqlDbType.VarChar);
                    p1.Value = _IECode;
                    SqlParameter p2 = new SqlParameter("@AsOnDate", SqlDbType.VarChar);
                    p2.Value = _FromDate;
                    SqlParameter p3 = new SqlParameter("@branch", SqlDbType.VarChar);
                    p3.Value = _ADCode;

                    string _qry = "TF_IMPWH_GetDumpForExcel";
                    SqlParameter PIECode = new SqlParameter("@IECode", SqlDbType.VarChar);
                    PIECode.Value = _IECode;
                    SqlParameter PCustName = new SqlParameter("@CustName", SqlDbType.VarChar);
                    PCustName.Value = _CustName;
                    SqlParameter PCustAbrr = new SqlParameter("@CustAbrr", SqlDbType.VarChar);
                    PCustAbrr.Value = _CustAbrr;
                    SqlParameter PFileType = new SqlParameter("@FileType", "GTP");
                    string FileName = obj.SaveDeleteData("TF_IMPWH_GetFileName", PCustAbrr, PFileType);
                    DataTable dt = obj.getData(_qry, p1, p2, p3);
                    int RowCount = dt.Rows.Count;
                    if (dt.Rows.Count > 0)
                    {
                        if (!Directory.Exists(_directoryPath))
                        {
                            Directory.CreateDirectory(_directoryPath);
                        }
                        //string FileName = _IECode + "_" + System.DateTime.Now.ToShortDateString().Replace("/", "") + ".xlsx";
                        string _filePath = _directoryPath + FileName;
                        using (ExcelPackage pck = new ExcelPackage())
                        {
                            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Worksheet");
                            ws.Cells["A1"].Value = "Digital Import Payment System";
                            ws.Cells["A1:AD1"].Merge = true;
                            ws.Cells["A1"].Style.Font.Bold = true;
                            ws.Cells["A1"].Style.Font.Size = 15;
                            ws.Cells["A2"].Value = "Mizuho Bank, Ltd";
                            ws.Cells["A2:AD2"].Merge = true;
                            ws.Cells["A2"].Style.Font.Bold = true;
                            ws.Cells["A2"].Style.Font.Size = 14;
                            ws.Cells["A3"].Value = "Name of the Importer";
                            ws.Cells["A3"].Style.Font.Bold = true;
                            ws.Cells["A3"].Style.Font.Size = 13;
                            ws.Cells["B3"].Value = ":";
                            ws.Cells["B3"].Style.Font.Bold = true;
                            ws.Cells["C3"].Style.Font.Size = 13;
                            ws.Cells["C3"].Value = _CustName;
                            ws.Cells["C3:AD3"].Merge = true;
                            ws.Cells["C3"].Style.Font.Bold = true;
                            ws.Cells["C3"].Style.Font.Size = 13;
                            ws.Cells["A4"].Value = _IECode;
                            ws.Cells["B4"].Value = "Invoice Details";
                            ws.Cells["B4:I4"].Merge = true;
                            ws.Cells["J4"].Value = "Shipment Details";
                            ws.Cells["J4:T4"].Merge = true;
                            ws.Cells["U4"].Value = "BOE Details";
                            ws.Cells["U4:Y4"].Merge = true;
                            ws.Cells["Z4"].Value = "Details Required";
                            ws.Cells["Z4:AD4"].Merge = true;

                            ws.Cells["A4:AD4"].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                            ws.Cells["A4:AD4"].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                            ws.Cells["A4:AD4"].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                            ws.Cells["A4:AD4"].Style.Border.Left.Style = ExcelBorderStyle.Thick;

                            ws.Cells["A4:AD4"].Style.Border.Top.Color.SetColor(Color.White);
                            ws.Cells["A4:AD4"].Style.Border.Right.Color.SetColor(Color.White);
                            ws.Cells["A4:AD4"].Style.Border.Bottom.Color.SetColor(Color.White);
                            ws.Cells["A4:AD4"].Style.Border.Left.Color.SetColor(Color.White);

                            ws.Row(4).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            ws.Cells["A4:AD4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            ws.Cells["A4:AD4"].Style.Fill.BackgroundColor.SetColor(Color.CornflowerBlue);
                            ws.Cells["A4:AD4"].Style.Font.Color.SetColor(Color.White);
                            ws.Cells["A4:AD4"].Style.Font.Bold = true;
                            // Addws.Cells[ing HeaderRow.
                            ws.Cells["A" + 5].Value = dt.Columns[0].ColumnName;
                            ws.Cells["B" + 5].Value = dt.Columns[1].ColumnName;
                            ws.Cells["C" + 5].Value = dt.Columns[2].ColumnName;
                            ws.Cells["D" + 5].Value = dt.Columns[3].ColumnName;
                            ws.Cells["E" + 5].Value = dt.Columns[4].ColumnName;
                            ws.Cells["F" + 5].Value = dt.Columns[5].ColumnName;
                            ws.Cells["G" + 5].Value = dt.Columns[6].ColumnName;
                            ws.Cells["H" + 5].Value = dt.Columns[7].ColumnName;
                            ws.Cells["I" + 5].Value = dt.Columns[8].ColumnName;
                            ws.Cells["J" + 5].Value = dt.Columns[9].ColumnName;
                            ws.Cells["K" + 5].Value = dt.Columns[10].ColumnName;
                            ws.Cells["L" + 5].Value = dt.Columns[11].ColumnName;
                            ws.Cells["M" + 5].Value = dt.Columns[12].ColumnName;
                            ws.Cells["N" + 5].Value = dt.Columns[13].ColumnName;
                            ws.Cells["O" + 5].Value = dt.Columns[14].ColumnName;
                            ws.Cells["P" + 5].Value = dt.Columns[15].ColumnName;
                            ws.Cells["Q" + 5].Value = dt.Columns[16].ColumnName;
                            ws.Cells["R" + 5].Value = dt.Columns[17].ColumnName;
                            ws.Cells["S" + 5].Value = dt.Columns[18].ColumnName;
                            ws.Cells["T" + 5].Value = dt.Columns[19].ColumnName;
                            ws.Cells["U" + 5].Value = dt.Columns[20].ColumnName;
                            ws.Cells["V" + 5].Value = dt.Columns[21].ColumnName;
                            ws.Cells["W" + 5].Value = dt.Columns[22].ColumnName;
                            ws.Cells["X" + 5].Value = dt.Columns[23].ColumnName;
                            ws.Cells["Y" + 5].Value = dt.Columns[24].ColumnName;
                            ws.Cells["Z" + 5].Value = dt.Columns[25].ColumnName;
                            ws.Cells["AA" + 5].Value = dt.Columns[26].ColumnName;
                            ws.Cells["AB" + 5].Value = dt.Columns[27].ColumnName;
                            ws.Cells["AC" + 5].Value = dt.Columns[28].ColumnName;
                            ws.Cells["AD" + 5].Value = "Payment Currency (If other than bill of entry currency)";

                            ws.Cells["A5:AD5"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            ws.Cells["A5:AD5"].Style.Fill.BackgroundColor.SetColor(Color.CornflowerBlue);
                            ws.Cells["A5:AD5"].Style.Font.Color.SetColor(Color.White);
                            ws.Cells["A5:AD5"].Style.Font.Bold = true;

                            ws.Cells["A5:AD5"].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                            ws.Cells["A5:AD5"].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                            ws.Cells["A5:AD5"].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                            ws.Cells["A5:AD5"].Style.Border.Left.Style = ExcelBorderStyle.Thick;

                            ws.Cells["A5:AD5"].Style.Border.Top.Color.SetColor(Color.White);
                            ws.Cells["A5:AD5"].Style.Border.Right.Color.SetColor(Color.White);
                            ws.Cells["A5:AD5"].Style.Border.Bottom.Color.SetColor(Color.White);
                            ws.Cells["A5:AD5"].Style.Border.Left.Color.SetColor(Color.White);

                            ws.Row(5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            int srno = 1;
                            // Adding DataRows.
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                ws.Cells["A" + (j + 6)].Value = srno;
                                ws.Cells["B" + (j + 6)].Value = dt.Rows[j][1];
                                ws.Cells["C" + (j + 6)].Value = dt.Rows[j][2];
                                ws.Cells["D" + (j + 6)].Value = dt.Rows[j][3];
                                ws.Cells["E" + (j + 6)].Value = dt.Rows[j][4];
                                ws.Cells["F" + (j + 6)].Value = dt.Rows[j][5];
                                ws.Cells["G" + (j + 6)].Value = dt.Rows[j][6];
                                ws.Cells["H" + (j + 6)].Value = dt.Rows[j][7];
                                ws.Cells["I" + (j + 6)].Value = dt.Rows[j][8];
                                ws.Cells["J" + (j + 6)].Value = dt.Rows[j][9];
                                ws.Cells["K" + (j + 6)].Value = dt.Rows[j][10];
                                ws.Cells["L" + (j + 6)].Value = dt.Rows[j][11];
                                ws.Cells["M" + (j + 6)].Value = dt.Rows[j][12];
                                ws.Cells["N" + (j + 6)].Value = dt.Rows[j][13];
                                ws.Cells["O" + (j + 6)].Value = dt.Rows[j][14];
                                ws.Cells["P" + (j + 6)].Value = dt.Rows[j][15];
                                ws.Cells["Q" + (j + 6)].Value = dt.Rows[j][16];
                                ws.Cells["R" + (j + 6)].Value = dt.Rows[j][17];
                                ws.Cells["S" + (j + 6)].Value = dt.Rows[j][18];
                                ws.Cells["T" + (j + 6)].Value = dt.Rows[j][19];
                                ws.Cells["U" + (j + 6)].Value = dt.Rows[j][20];
                                ws.Cells["V" + (j + 6)].Value = dt.Rows[j][21];
                                ws.Cells["W" + (j + 6)].Value = dt.Rows[j][22];
                                ws.Cells["X" + (j + 6)].Value = dt.Rows[j][23];
                                ws.Cells["Y" + (j + 6)].Value = dt.Rows[j][24];
                                ws.Cells["Z" + (j + 6)].Value = dt.Rows[j][25];
                                ws.Cells["AA" + (j + 6)].Value = dt.Rows[j][26];
                                ws.Cells["AB" + (j + 6)].Value = dt.Rows[j][27];
                                ws.Cells["AC" + (j + 6)].Value = dt.Rows[j][28];
                                ws.Cells["AD" + (j + 6)].Value = "";

                                ws.Cells["B" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                ws.Cells["C" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells["D" + (j + 6)].Style.Numberformat.Format = "0.00";
                                ws.Cells["D" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                ws.Cells["E" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                ws.Cells["O" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                ws.Cells["P" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                ws.Cells["U" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells["W" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells["Y" + (j + 6)].Style.Numberformat.Format = "0.00";
                                ws.Cells["Y" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                ws.Cells["Z" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells["AA" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells["AB" + (j + 6)].Style.Numberformat.Format = "0.00";
                                ws.Cells["AB" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                ws.Cells["F" + (j + 6)].Style.Numberformat.Format = "dd/mm/yyyy";
                                ws.Cells["F" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells["I" + (j + 6)].Style.Numberformat.Format = "dd/mm/yyyy";
                                ws.Cells["I" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells["J" + (j + 6)].Style.Numberformat.Format = "dd/mm/yyyy";
                                ws.Cells["J" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells["V" + (j + 6)].Style.Numberformat.Format = "dd/mm/yyyy";
                                ws.Cells["V" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                if (dt.Rows[j]["Good To Pay/Discrepant"].ToString() != "Good To Pay")
                                {
                                    ws.Cells["A" + (j + 6) + ":AD" + (j + 6)].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    ws.Cells["A" + (j + 6) + ":AD" + (j + 6)].Style.Fill.BackgroundColor.SetColor(Color.Red);
                                    ws.Cells["A" + (j + 6) + ":AD" + (j + 6)].Style.Font.Color.SetColor(Color.White);
                                }

                                if (dt.Rows[j][9].ToString() != "")
                                {
                                    ws.Cells["F" + (j + 6)].Formula = "=DATE(RIGHT(J" + (j + 6) + ",4), MID(J" + (j + 6) + ",4,2), LEFT(J" + (j + 6) + ",2))+E" + (j + 6);
                                    ws.Cells["F" + (j + 6)].Style.Numberformat.Format = "dd/mm/yyyy";
                                }

                                ws.Cells["AA" + (j + 6)].Formula = "=IF(Z" + (j + 6) + "=\"Pay\"," + ("C" + (j + 6)) + ",\"\")";
                                ws.Cells["AB" + (j + 6)].Formula = "=IF(Z" + (j + 6) + "=\"Pay\"," + ("Y" + (j + 6)) + ",\"\")";

                                var CapitalGoods = ws.DataValidations.AddListValidation("R" + (j + 6));
                                CapitalGoods.ShowErrorMessage = true;
                                CapitalGoods.ErrorStyle = ExcelDataValidationWarningStyle.warning;
                                CapitalGoods.ErrorTitle = "An invalid value was entered";
                                CapitalGoods.Error = "Select a value from the list";
                                CapitalGoods.Formula.Values.Add("Capital");
                                CapitalGoods.Formula.Values.Add("Non Capital");

                                var ImpvoiceToBePad = ws.DataValidations.AddListValidation("Z" + (j + 6));
                                ImpvoiceToBePad.ShowErrorMessage = true;
                                ImpvoiceToBePad.ErrorStyle = ExcelDataValidationWarningStyle.stop;
                                ImpvoiceToBePad.ErrorTitle = "An invalid value was entered";
                                ImpvoiceToBePad.Error = "Select a value from the list";
                                ImpvoiceToBePad.Formula.Values.Add("Pay");

                                var TenorDays = ws.DataValidations.AddIntegerValidation("E" + (j + 6));
                                TenorDays.ShowErrorMessage = true;
                                TenorDays.ErrorStyle = ExcelDataValidationWarningStyle.stop;
                                TenorDays.ErrorTitle = "The value must be an integer between 1 and 999";
                                TenorDays.Error = "The value must be an integer between 1 and 999";
                                TenorDays.Formula.Value = 1;
                                TenorDays.Formula2.Value = 999;

                                //var AmountToBeRemited = ws.DataValidations.AddIntegerValidation("AB" + (j + 6));
                                //AmountToBeRemited.ShowErrorMessage = true;
                                //AmountToBeRemited.ErrorStyle = ExcelDataValidationWarningStyle.stop;
                                //AmountToBeRemited.ErrorTitle = "Amount can not be greater than Outstanding amount.";
                                //AmountToBeRemited.Error = "Amount can not be greater than Outstanding amount.";
                                //AmountToBeRemited.Formula.Value = 1;
                                //AmountToBeRemited.Formula2.Value = Convert.ToInt32(dt.Rows[j][24]) + 1;


                                //ws.Cells["AB" + (j + 6)].DataValidation.Decimal.LessThan(Convert.ToDouble(dt.Rows[j][24]) + 1);
                                //var dvAmount = ws.Cells["AB" + (j + 6)].DataValidation;
                                //dvAmount.InputTitle = "Amount can not be greater than Outstanding amount.";
                                //dvAmount.InputMessage = "Amount can not be greater than Outstanding amount.";
                                //dvAmount.ErrorMessage = "Amount can not be greater than Outstanding amount.";

                                //ws.Cells["E" + (j + 6)].DataValidation.Decimal.Between(1, 999);
                                //ws.Cells["E" + (j + 6)].DataValidation.IgnoreBlanks = false;
                                //var dvAB = ws.Cells["E" + (j + 6)].DataValidation;
                                //dvAB.InputTitle = "Tenot Days can't be blank.";
                                //dvAB.InputMessage = "Tenot Days should be 3 digit numeric.";
                                //dvAB.ErrorMessage = "Tenot Days should be 3 digit numeric.";

                                //ws.Cells["M" + (j + 6)].DataValidation.TextLength.GreaterThan(0);
                                //ws.Cells["M" + (j + 6)].DataValidation.IgnoreBlanks = false;
                                //var dvNameShipComp = ws.Cells["M" + (j + 6)].DataValidation;
                                //dvNameShipComp.InputTitle = "Name Of Shiping Company can't be blank.";
                                //dvNameShipComp.InputMessage = "Name Of Shiping Company can't be blank.";
                                //dvNameShipComp.ErrorMessage = "Name Of Shiping Company can't be blank.";

                                //ws.Cells["N" + (j + 6)].DataValidation.TextLength.GreaterThan(0);
                                //ws.Cells["N" + (j + 6)].DataValidation.IgnoreBlanks = false;
                                //var dvVessAirCarrName = ws.Cells["N" + (j + 6)].DataValidation;
                                //dvVessAirCarrName.InputTitle = "Vessel/Air Carrier Name can't be blank.";
                                //dvVessAirCarrName.InputMessage = "Vessel/Air Carrier Name can't be blank.";
                                //dvVessAirCarrName.ErrorMessage = "Vessel/Air Carrier Name can't be blank.";

                                //ws.Cells["S" + (j + 6)].DataValidation.TextLength.GreaterThan(0);
                                //ws.Cells["S" + (j + 6)].DataValidation.IgnoreBlanks = false;
                                //var dvGoodsDescription = ws.Cells["S" + (j + 6)].DataValidation;
                                //dvGoodsDescription.InputTitle = "Goods Description can't be blank.";
                                //dvGoodsDescription.InputMessage = "Goods Description Goods can't be blank.";
                                //dvGoodsDescription.ErrorMessage = "Goods Description Goods can't be blank.";

                                //ws.Cells["T" + (j + 6)].DataValidation.TextLength.GreaterThan(0);
                                //ws.Cells["T" + (j + 6)].DataValidation.IgnoreBlanks = false;
                                //var dvCountryOfOrigin = ws.Cells["T" + (j + 6)].DataValidation;
                                //dvCountryOfOrigin.InputTitle = "Country Of Origin can't be blank.";
                                //dvCountryOfOrigin.InputMessage = "Country Of Origin Goods can't be blank.";
                                //dvCountryOfOrigin.ErrorMessage = "Country Of OriginGoods can't be blank.";

                                //ws.Cells["AA" + (j + 6)].DataValidation.TextLength.GreaterThan(0);
                                //ws.Cells["AA" + (j + 6)].DataValidation.IgnoreBlanks = false;
                                //var dvOPDAproval = ws.Cells["AA" + (j + 6)].DataValidation;
                                //dvOPDAproval.InputTitle = "Currency can't be blank.";
                                //dvOPDAproval.InputMessage = "Currency can't be blank.";
                                //dvOPDAproval.ErrorMessage = "Currency can't be blank.";

                                //ws.Cells["AC" + (j + 6)].DataValidation.TextLength.GreaterThan(0);
                                //ws.Cells["AC" + (j + 6)].DataValidation.IgnoreBlanks = false;
                                //var dvRemark = ws.Cells["AC" + (j + 6)].DataValidation;
                                //dvRemark.InputTitle = "Remark can't be blank.";
                                //dvRemark.InputMessage = "Remark can't be blank.";
                                //dvRemark.ErrorMessage = "Remark can't be blank.";
                                srno++;
                            }

                            _CustAcNo = "H0000000000" + _CustAcNo;
                            string _FilePassword = _CustAcNo.Substring(_CustAcNo.Length - 6);

                            ws.Protection.SetPassword(_FilePassword);
                            ws.Column(5).Style.Locked = false;
                            ws.Column(13).Style.Locked = false;
                            ws.Column(14).Style.Locked = false;
                            ws.Column(18).Style.Locked = false;
                            ws.Column(19).Style.Locked = false;
                            ws.Column(20).Style.Locked = false;
                            ws.Column(23).Style.Locked = false;
                            ws.Column(26).Style.Locked = false;
                            //ws.Column(27).Style.Locked = false;
                            ws.Column(28).Style.Locked = false;
                            ws.Column(29).Style.Locked = false;
                            ws.Column(30).Style.Locked = false;
                            ws.Cells[ws.Dimension.Address].AutoFitColumns();
                            ws.Cells["A4"].Style.Font.Color.SetColor(Color.CornflowerBlue);

                            

                            using (MemoryStream MS = new MemoryStream())
                            {
                                pck.SaveAs(MS, _FilePassword);
                                FileStream file = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
                                MS.WriteTo(file);
                                file.Close();
                                MS.Close();
                            }
                            _RecordCount++;
                        }
                        ViewState["FileName"] = FileName;
                        ViewState["FileType"] = "GTP";
                        lblbop6name.Text = "Good To Pay File Created Successfully";
                        lnkDownload.Visible = true;
                        SqlParameter PFileName = new SqlParameter("@FileName", FileName);
                        SqlParameter PRows = new SqlParameter("@Rows", RowCount);
                        SqlParameter PUserName = new SqlParameter("@UserName", Session["userName"].ToString());
                        string Result = obj.SaveDeleteData("TF_IMPWH_InsertFileName", PFileName, PUserName, PFileType, PRows);
                        string _serverName = obj.GetServerName();
                        string path = "file://" + _serverName + "/TF_GeneratedFiles/IMPWH/GTP";
                        string link = "/TF_GeneratedFiles/IMPWH/GTP";
                        labelMessage.Text = "File Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
                        string script1 = "alert('Good To Pay File Created Successfully')";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script1, true);
                    }
                    else
                    {
                        string script1 = "alert('There is no record for this customer.')";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script1, true);
                    }
                }
            }
            else
            {
                string script = "alert('There is no customer for today')";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
            }
            //ErrorLog(_RecordCount.ToString() + " File Created");

        }
        catch (Exception Ex)
        {
            ErrorLog(Ex.Message);
        }
    }
    public void CreteExcelForPaymentUsingEPPlus()
    {
        try
        {
            DataTable dtCustomer = new DataTable();
            string _FromDate = txtFromDate.Text.ToString();
            string _ADCode = ddlBranch.SelectedValue.ToString();
            string Day = _FromDate.Substring(0, 2);
            string Cust = txtCustACNo.Text.Trim();
            SqlParameter P1 = new SqlParameter("@Day", Day);
            SqlParameter P2 = new SqlParameter("@Cust", Cust);
            TF_DATA obj = new TF_DATA();
            dtCustomer = obj.getData("TF_IMPWH_GetCustomerDetialsToSendMailForPayment", P1, P2);
            int _RecordCount = 0;
            if (dtCustomer.Rows.Count > 0)
            {
                for (int i = 0; i < dtCustomer.Rows.Count; i++)
                {
                    string _IECode = dtCustomer.Rows[i]["CUST_IE_CODE"].ToString();
                    string _CustName = dtCustomer.Rows[i]["CUST_NAME"].ToString();
                    string _CustAcNo = dtCustomer.Rows[i]["CUST_ACCOUNT_NO"].ToString();
                    string _CustAbrr = dtCustomer.Rows[i]["Cust_Abbr"].ToString();
                    string _EmailID = dtCustomer.Rows[i]["CUST_EMAIL_ID"].ToString();
                    System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
                    dateInfo.ShortDatePattern = "dd/MM/yyyy";
                    string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPWH/Payment/");

                    SqlParameter p1 = new SqlParameter("@IECode", SqlDbType.VarChar);
                    p1.Value = _IECode;
                    SqlParameter p2 = new SqlParameter("@AsOnDate", SqlDbType.VarChar);
                    p2.Value = _FromDate;
                    SqlParameter p3 = new SqlParameter("@branch", SqlDbType.VarChar);
                    p3.Value = _ADCode;

                    string _qry = "TF_IMPWH_GenerateExcelForPayment";
                    SqlParameter PIECode = new SqlParameter("@IECode", SqlDbType.VarChar);
                    PIECode.Value = _IECode;
                    SqlParameter PCustName = new SqlParameter("@CustName", SqlDbType.VarChar);
                    PCustName.Value = _CustName;
                    SqlParameter PCustAbrr = new SqlParameter("@CustAbrr", SqlDbType.VarChar);
                    PCustAbrr.Value = _CustAbrr;
                    SqlParameter PFileType = new SqlParameter("@FileType", "Payment");
                    string FileName = obj.SaveDeleteData("TF_IMPWH_GetFileName", PCustAbrr, PFileType);
                    DataTable dt = obj.getData(_qry, p1, p2, p3);
                    int RowCount = dt.Rows.Count;
                    if (dt.Rows.Count > 0)
                    {
                        if (!Directory.Exists(_directoryPath))
                        {
                            Directory.CreateDirectory(_directoryPath);
                        }
                        //string FileName = _IECode + "_" + System.DateTime.Now.ToShortDateString().Replace("/", "") + ".xlsx";
                        string _filePath = _directoryPath + FileName;
                        using (ExcelPackage pck = new ExcelPackage())
                        {
                            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Worksheet");
                            ws.Cells["A1"].Value = "Digital Import Payment System";
                            ws.Cells["A1:AD1"].Merge = true;
                            ws.Cells["A1"].Style.Font.Bold = true;
                            ws.Cells["A1"].Style.Font.Size = 15;
                            ws.Cells["A2"].Value = "Mizuho Bank, Ltd";
                            ws.Cells["A2:AD2"].Merge = true;
                            ws.Cells["A2"].Style.Font.Bold = true;
                            ws.Cells["A2"].Style.Font.Size = 14;
                            ws.Cells["A3"].Value = "Name of the Importer";
                            ws.Cells["A3"].Style.Font.Bold = true;
                            ws.Cells["A3"].Style.Font.Size = 13;
                            ws.Cells["B3"].Value = ":";
                            ws.Cells["B3"].Style.Font.Bold = true;
                            ws.Cells["C3"].Style.Font.Size = 13;
                            ws.Cells["C3"].Value = _CustName;
                            ws.Cells["C3:AD3"].Merge = true;
                            ws.Cells["C3"].Style.Font.Bold = true;
                            ws.Cells["C3"].Style.Font.Size = 13;
                            ws.Cells["A4"].Value = _IECode;
                            ws.Cells["B4"].Value = "Invoice Details";
                            ws.Cells["B4:I4"].Merge = true;
                            ws.Cells["J4"].Value = "Shipment Details";
                            ws.Cells["J4:T4"].Merge = true;
                            ws.Cells["U4"].Value = "BOE Details";
                            ws.Cells["U4:Y4"].Merge = true;
                            ws.Cells["Z4"].Value = "Details Required";
                            ws.Cells["Z4:AD4"].Merge = true;

                            ws.Cells["A4:AD4"].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                            ws.Cells["A4:AD4"].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                            ws.Cells["A4:AD4"].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                            ws.Cells["A4:AD4"].Style.Border.Left.Style = ExcelBorderStyle.Thick;

                            ws.Cells["A4:AD4"].Style.Border.Top.Color.SetColor(Color.White);
                            ws.Cells["A4:AD4"].Style.Border.Right.Color.SetColor(Color.White);
                            ws.Cells["A4:AD4"].Style.Border.Bottom.Color.SetColor(Color.White);
                            ws.Cells["A4:AD4"].Style.Border.Left.Color.SetColor(Color.White);

                            ws.Row(4).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            ws.Cells["A4:AD4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            ws.Cells["A4:AD4"].Style.Fill.BackgroundColor.SetColor(Color.CornflowerBlue);
                            ws.Cells["A4:AD4"].Style.Font.Color.SetColor(Color.White);
                            ws.Cells["A4:AD4"].Style.Font.Bold = true;
                            // Addws.Cells[ing HeaderRow.
                            ws.Cells["A" + 5].Value = dt.Columns[0].ColumnName;
                            ws.Cells["B" + 5].Value = dt.Columns[1].ColumnName;
                            ws.Cells["C" + 5].Value = dt.Columns[2].ColumnName;
                            ws.Cells["D" + 5].Value = dt.Columns[3].ColumnName;
                            ws.Cells["E" + 5].Value = dt.Columns[4].ColumnName;
                            ws.Cells["F" + 5].Value = dt.Columns[5].ColumnName;
                            ws.Cells["G" + 5].Value = dt.Columns[6].ColumnName;
                            ws.Cells["H" + 5].Value = dt.Columns[7].ColumnName;
                            ws.Cells["I" + 5].Value = dt.Columns[8].ColumnName;
                            ws.Cells["J" + 5].Value = dt.Columns[9].ColumnName;
                            ws.Cells["K" + 5].Value = dt.Columns[10].ColumnName;
                            ws.Cells["L" + 5].Value = dt.Columns[11].ColumnName;
                            ws.Cells["M" + 5].Value = dt.Columns[12].ColumnName;
                            ws.Cells["N" + 5].Value = dt.Columns[13].ColumnName;
                            ws.Cells["O" + 5].Value = dt.Columns[14].ColumnName;
                            ws.Cells["P" + 5].Value = dt.Columns[15].ColumnName;
                            ws.Cells["Q" + 5].Value = dt.Columns[16].ColumnName;
                            ws.Cells["R" + 5].Value = dt.Columns[17].ColumnName;
                            ws.Cells["S" + 5].Value = dt.Columns[18].ColumnName;
                            ws.Cells["T" + 5].Value = dt.Columns[19].ColumnName;
                            ws.Cells["U" + 5].Value = dt.Columns[20].ColumnName;
                            ws.Cells["V" + 5].Value = dt.Columns[21].ColumnName;
                            ws.Cells["W" + 5].Value = dt.Columns[22].ColumnName;
                            ws.Cells["X" + 5].Value = dt.Columns[23].ColumnName;
                            ws.Cells["Y" + 5].Value = dt.Columns[24].ColumnName;
                            ws.Cells["Z" + 5].Value = dt.Columns[25].ColumnName;
                            ws.Cells["AA" + 5].Value = dt.Columns[26].ColumnName;
                            ws.Cells["AB" + 5].Value = dt.Columns[27].ColumnName;
                            ws.Cells["AC" + 5].Value = dt.Columns[28].ColumnName;
                            ws.Cells["AD" + 5].Value = dt.Columns[29].ColumnName;                            

                            ws.Cells["A5:AD5"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            ws.Cells["A5:AD5"].Style.Fill.BackgroundColor.SetColor(Color.CornflowerBlue);
                            ws.Cells["A5:AD5"].Style.Font.Color.SetColor(Color.White);
                            ws.Cells["A5:AD5"].Style.Font.Bold = true;

                            ws.Cells["A5:AD5"].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                            ws.Cells["A5:AD5"].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                            ws.Cells["A5:AD5"].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                            ws.Cells["A5:AD5"].Style.Border.Left.Style = ExcelBorderStyle.Thick;

                            ws.Cells["A5:AD5"].Style.Border.Top.Color.SetColor(Color.White);
                            ws.Cells["A5:AD5"].Style.Border.Right.Color.SetColor(Color.White);
                            ws.Cells["A5:AD5"].Style.Border.Bottom.Color.SetColor(Color.White);
                            ws.Cells["A5:AD5"].Style.Border.Left.Color.SetColor(Color.White);

                            ws.Row(5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            int srno = 1;
                            // Adding DataRows.
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                ws.Cells["A" + (j + 6)].Value = srno;
                                ws.Cells["B" + (j + 6)].Value = dt.Rows[j][1];
                                ws.Cells["C" + (j + 6)].Value = dt.Rows[j][2];
                                ws.Cells["D" + (j + 6)].Value = dt.Rows[j][3];
                                ws.Cells["E" + (j + 6)].Value = dt.Rows[j][4];
                                ws.Cells["F" + (j + 6)].Value = dt.Rows[j][5];
                                ws.Cells["G" + (j + 6)].Value = dt.Rows[j][6];
                                ws.Cells["H" + (j + 6)].Value = dt.Rows[j][7];
                                ws.Cells["I" + (j + 6)].Value = dt.Rows[j][8];
                                ws.Cells["J" + (j + 6)].Value = dt.Rows[j][9];
                                ws.Cells["K" + (j + 6)].Value = dt.Rows[j][10];
                                ws.Cells["L" + (j + 6)].Value = dt.Rows[j][11];
                                ws.Cells["M" + (j + 6)].Value = dt.Rows[j][12];
                                ws.Cells["N" + (j + 6)].Value = dt.Rows[j][13];
                                ws.Cells["O" + (j + 6)].Value = dt.Rows[j][14];
                                ws.Cells["P" + (j + 6)].Value = dt.Rows[j][15];
                                ws.Cells["Q" + (j + 6)].Value = dt.Rows[j][16];
                                ws.Cells["R" + (j + 6)].Value = dt.Rows[j][17];
                                ws.Cells["S" + (j + 6)].Value = dt.Rows[j][18];
                                ws.Cells["T" + (j + 6)].Value = dt.Rows[j][19];
                                ws.Cells["U" + (j + 6)].Value = dt.Rows[j][20];
                                ws.Cells["V" + (j + 6)].Value = dt.Rows[j][21];
                                ws.Cells["W" + (j + 6)].Value = dt.Rows[j][22];
                                ws.Cells["X" + (j + 6)].Value = dt.Rows[j][23];
                                ws.Cells["Y" + (j + 6)].Value = dt.Rows[j][24];
                                ws.Cells["Z" + (j + 6)].Value = dt.Rows[j][25];
                                ws.Cells["AA" + (j + 6)].Value = dt.Rows[j][26];
                                ws.Cells["AB" + (j + 6)].Value = dt.Rows[j][27];
                                ws.Cells["AC" + (j + 6)].Value = dt.Rows[j][28];
                                ws.Cells["AD" + (j + 6)].Value = dt.Rows[j][29];                                

                                ws.Cells["B" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                ws.Cells["C" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells["D" + (j + 6)].Style.Numberformat.Format = "0.00";
                                ws.Cells["D" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                ws.Cells["E" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                ws.Cells["O" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                ws.Cells["P" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                ws.Cells["U" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells["W" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells["Y" + (j + 6)].Style.Numberformat.Format = "0.00";
                                ws.Cells["Y" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                ws.Cells["Z" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells["AA" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells["AB" + (j + 6)].Style.Numberformat.Format = "0.00";
                                ws.Cells["AB" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                ws.Cells["F" + (j + 6)].Style.Numberformat.Format = "dd/mm/yyyy";
                                ws.Cells["F" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells["I" + (j + 6)].Style.Numberformat.Format = "dd/mm/yyyy";
                                ws.Cells["I" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells["J" + (j + 6)].Style.Numberformat.Format = "dd/mm/yyyy";
                                ws.Cells["J" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells["V" + (j + 6)].Style.Numberformat.Format = "dd/mm/yyyy";
                                ws.Cells["V" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells["AD" + (j + 6)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                if (dt.Rows[j][26].ToString() != dt.Rows[j][29].ToString())
                                {
                                    ws.Cells["A" + (j + 6) + ":AD" + (j + 6)].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    ws.Cells["A" + (j + 6) + ":AD" + (j + 6)].Style.Fill.BackgroundColor.SetColor(Color.SeaGreen);
                                    ws.Cells["A" + (j + 6) + ":AD" + (j + 6)].Style.Font.Color.SetColor(Color.White);
                                }

                                SqlParameter PFileNameS = new SqlParameter("@FileName", FileName);
                                SqlParameter PBOENo = new SqlParameter("@BOENo", dt.Rows[j][20].ToString());
                                SqlParameter PBOEDate = new SqlParameter("@BOEDate", dt.Rows[j][21].ToString());
                                SqlParameter PInvNo = new SqlParameter("@InvNo", dt.Rows[j][1].ToString());
                                SqlParameter PInvCurr = new SqlParameter("@InvCurr", dt.Rows[j][2].ToString());
                                SqlParameter PInvAmt = new SqlParameter("@AmtToBeRemi", dt.Rows[j][27].ToString());
                                SqlParameter PAdCode = new SqlParameter("@AdCode", dt.Rows[j][22].ToString());
                                SqlParameter PPortCode = new SqlParameter("@PortCode", dt.Rows[j][16].ToString());
                                string Result11 = obj.SaveDeleteData("TF_IMPWH_GDPSaveCreatedFileRecords", PFileNameS, PBOENo, PBOEDate, PInvNo, PInvCurr, PInvAmt, PAdCode, PPortCode);

                                srno++;
                            }
                            _CustAcNo = "H0000000000" + _CustAcNo;
                            string _FilePassword = _CustAcNo.Substring(_CustAcNo.Length - 6);

                            ws.Protection.SetPassword(_FilePassword);
                            ws.Column(5).Style.Locked = false;
                            ws.Column(13).Style.Locked = false;
                            ws.Column(14).Style.Locked = false;
                            ws.Column(18).Style.Locked = false;
                            ws.Column(23).Style.Locked = false;
                            ws.Column(26).Style.Locked = false;
                            //ws.Column(27).Style.Locked = false;
                            ws.Column(28).Style.Locked = false;
                            ws.Column(29).Style.Locked = false;
                            ws.Column(30).Style.Locked = false;                            
                            ws.Cells[ws.Dimension.Address].AutoFitColumns();
                            ws.Cells["A4"].Style.Font.Color.SetColor(Color.CornflowerBlue);

                            

                            using (MemoryStream MS = new MemoryStream())
                            {
                                pck.SaveAs(MS, _FilePassword);
                                FileStream file = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
                                MS.WriteTo(file);
                                file.Close();
                                MS.Close();
                            }
                            _RecordCount++;
                        }                        

                        ViewState["FileName"] = FileName;
                        ViewState["FileType"] = "Payment";
                        CreateExcelForUniqueParty();
                        lblbop6name.Text = "Payment File Created Successfully";
                        lnkDownload.Visible = true;

                        SqlParameter PFileName = new SqlParameter("@FileName", FileName);
                        SqlParameter PRows = new SqlParameter("@Rows", RowCount);
                        SqlParameter PUserName = new SqlParameter("@UserName", Session["userName"].ToString());
                        string Result = obj.SaveDeleteData("TF_IMPWH_InsertFileName", PFileName, PUserName, PFileType, PRows);
                        string _serverName = obj.GetServerName();
                        string path = "file://" + _serverName + "/TF_GeneratedFiles/IMPWH/Payment";
                        string link = "/TF_GeneratedFiles/IMPWH/Payment";
                        labelMessage.Text = "File Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
                        string script1 = "alert('Payment File Created Successfully')";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script1, true);
                        //DownloadFile(FileName);
                    }
                    else
                    {
                        string script1 = "alert('There is no record for this customer.')";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script1, true);
                    }
                }
            }
            else
            {
                string script = "alert('There is no customer for today')";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
            }
            //ErrorLog(_RecordCount.ToString() + " File Created");
        }
        catch (Exception Ex)
        {
            ErrorLog(Ex.Message);
        }
    }
    public void CreateExcelForUniqueParty()
    {
        try
        {
            DataTable dtCustomer = new DataTable();
            string _FromDate = txtFromDate.Text.ToString();
            string _ADCode = ddlBranch.SelectedValue.ToString();
            string Day = _FromDate.Substring(0, 2);
            string Cust = txtCustACNo.Text.Trim();
            SqlParameter P1 = new SqlParameter("@Day", Day);
            SqlParameter P2 = new SqlParameter("@Cust", Cust);
            TF_DATA obj = new TF_DATA();
            dtCustomer = obj.getData("TF_IMPWH_GetCustomerDetialsToSendMailForPayment", P1, P2);
            int _RecordCount = 0;
            if (dtCustomer.Rows.Count > 0)
            {
                for (int i = 0; i < dtCustomer.Rows.Count; i++)
                {
                    string _IECode = dtCustomer.Rows[i]["CUST_IE_CODE"].ToString();
                    string _CustName = dtCustomer.Rows[i]["CUST_NAME"].ToString();
                    string _CustAcNo = dtCustomer.Rows[i]["CUST_ACCOUNT_NO"].ToString();
                    string _CustAbrr = dtCustomer.Rows[i]["Cust_Abbr"].ToString();
                    string _EmailID = dtCustomer.Rows[i]["CUST_EMAIL_ID"].ToString();
                    System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
                    dateInfo.ShortDatePattern = "dd/MM/yyyy";
                    string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPWH/UniqParty/");

                    SqlParameter p1 = new SqlParameter("@FileName", SqlDbType.VarChar);
                    p1.Value = ViewState["FileName"].ToString();
                    SqlParameter p2 = new SqlParameter("@AsOnDate", SqlDbType.VarChar);
                    p2.Value = _FromDate;
                    SqlParameter p3 = new SqlParameter("@branch", SqlDbType.VarChar);
                    p3.Value = _ADCode;

                    string _qry = "TF_IMPWH_UniquePartyFileCreation";
                    SqlParameter PCustAbrr = new SqlParameter("@CustAbrr", SqlDbType.VarChar);
                    PCustAbrr.Value = _CustAbrr;
                    SqlParameter PFileType = new SqlParameter("@FileType", "UniqParty");
                    string FileName = obj.SaveDeleteData("TF_IMPWH_GetFileName", PCustAbrr, PFileType);
                    DataTable dt = obj.getData(_qry, p1);
                    int RowCount = dt.Rows.Count;
                    if (dt.Rows.Count > 0)
                    {
                        if (!Directory.Exists(_directoryPath))
                        {
                            Directory.CreateDirectory(_directoryPath);
                        }
                        string _filePath = _directoryPath + FileName;
                        using (ExcelPackage pck = new ExcelPackage())
                        {
                            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Worksheet");
                            // Addws.Cells[ing HeaderRow.
                            ws.Cells["A" + 1].Value = dt.Columns[0].ColumnName;
                            ws.Cells["B" + 1].Value = dt.Columns[1].ColumnName;
                            ws.Cells["C" + 1].Value = dt.Columns[2].ColumnName;
                            ws.Cells["D" + 1].Value = dt.Columns[3].ColumnName;
                            ws.Cells["E" + 1].Value = dt.Columns[4].ColumnName;
                            ws.Cells["F" + 1].Value = dt.Columns[5].ColumnName;
                            ws.Cells["G" + 1].Value = dt.Columns[6].ColumnName;
                            ws.Cells["H" + 1].Value = dt.Columns[7].ColumnName;

                            // Adding DataRows.
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                ws.Cells["A" + (j + 2)].Value = dt.Rows[j][0];
                                ws.Cells["B" + (j + 2)].Value = dt.Rows[j][1];
                                ws.Cells["C" + (j + 2)].Value = dt.Rows[j][2];
                                ws.Cells["D" + (j + 2)].Value = dt.Rows[j][3];
                                ws.Cells["E" + (j + 2)].Value = dt.Rows[j][4];
                                ws.Cells["F" + (j + 2)].Value = dt.Rows[j][5];
                                ws.Cells["G" + (j + 2)].Value = dt.Rows[j][6];
                                ws.Cells["H" + (j + 2)].Value = dt.Rows[j][7];
                            }
                            using (MemoryStream MS = new MemoryStream())
                            {
                                pck.SaveAs(MS);
                                FileStream file = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
                                MS.WriteTo(file);
                                file.Close();
                                MS.Close();
                            }
                            _RecordCount++;
                        }
                        ViewState["FileNameUP"] = FileName;
                        ViewState["FileTypeUP"] = "UniqParty";
                        lblUniqueParty.Text = "Unique Party File Created Successfully";
                        lnkDownloadUP.Visible = true;
                        SqlParameter PFileName1 = new SqlParameter("@FileName", FileName);
                        SqlParameter PRows = new SqlParameter("@Rows", RowCount);
                        SqlParameter PUserName1 = new SqlParameter("@UserName", Session["userName"].ToString());
                        string Result = obj.SaveDeleteData("TF_IMPWH_InsertFileName", PFileName1, PUserName1, PFileType, PRows);
                    }
                    else
                    {
                        string script1 = "alert('There is no record for this customer.')";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script1, true);
                    }
                }
            }
            else
            {
                string script = "alert('There is no customer for today')";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
            }
            //ErrorLog(_RecordCount.ToString() + " File Created");
        }
        catch (Exception Ex)
        {
            ErrorLog(Ex.Message);
        }
    }
    public void ErrorLog(string Message)
    {
        string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPWH/Log/");
        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }
        string path = _directoryPath + "Log.txt";
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(Message);
            writer.Close();
        }
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        string FileName = ViewState["FileName"].ToString();
        string FileType = ViewState["FileType"].ToString();
        string filePath = "~/TF_GeneratedFiles/IMPWH/" + FileType + "/" + FileName;
        Response.ContentType = "image/jpg";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
        Response.TransmitFile(Server.MapPath(filePath));
        Response.End();
    }
    protected void lnkDownloadUP_Click(object sender, EventArgs e)
    {
        string FileName = ViewState["FileNameUP"].ToString();
        string FileType = ViewState["FileTypeUP"].ToString();
        string filePath = "~/TF_GeneratedFiles/IMPWH/" + FileType + "/" + FileName;
        Response.ContentType = "image/jpg";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
        Response.TransmitFile(Server.MapPath(filePath));
        Response.End();
    }
    [WebMethod]
    public static string CheckCustAcNo(string AdCode, string CustAcNo)
    {
        string Result = "";
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@customerACNo", SqlDbType.VarChar);
        p1.Value = CustAcNo;
        SqlParameter p2 = new SqlParameter("@branch", SqlDbType.VarChar);
        p2.Value = AdCode;
        string _query = "TF_GetCustomerMasterDetails1";
        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            Result = dt.Rows[0]["CUST_NAME"].ToString();
        }
        else
        {
            Result = "false";
        }
        return Result;
    }
}