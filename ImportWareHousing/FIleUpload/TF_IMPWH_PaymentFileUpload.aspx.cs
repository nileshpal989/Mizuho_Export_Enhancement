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
using System.Data.OleDb;
using System.Net;
using System.Runtime.InteropServices;
using OfficeOpenXml;

public partial class ImportWareHousing_FIleUpload_TF_IMPWH_PaymentFileUpload : System.Web.UI.Page
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
                btnUpload.Attributes.Add("onclick", "return ShowProgress();");
            }
            fillBranch();
            ddlBranch.SelectedValue = Session["userADCode"].ToString();
            ddlBranch.Enabled = false;

            GridViewInvoice.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            //txtOTTRefNo.ReadOnly = true;
            //txtTotalOttAmt.ReadOnly = true;
            btnProcess.Attributes.Add("onclick", "return ValidateProcess();");
            //txtOTTDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            btnPartyID.Attributes.Add("onclick", "return Cust_Help();");
            btnHelp_DocNo.Attributes.Add("onclick", "return OTT_Help()");
            
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
                if (FileName.Substring(0, 3) == "GTP")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You are trying to upload Good To Pay File In Payment File Upload! You can not upload this file!.')", true);
                    return;
                }
                ViewState["FileName"] = FileName;
                string Extension = Path.GetExtension(fileinhouse.PostedFile.FileName);
                string FolderPath = Server.MapPath("../Uploaded_Files");

                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }
                FileName = FileName.Replace(" ", "");
                string FilePath = FolderPath + "\\" + System.IO.Path.GetFileName(fileinhouse.PostedFile.FileName);
                fileinhouse.SaveAs(FilePath);
                TF_DATA obj = new TF_DATA();
                SqlParameter PFileName = new SqlParameter("@FileName", FileName);
                string Result = obj.SaveDeleteData("TF_IMPWG_PaymentCheckFileName", PFileName);
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
                    GetExcelUsingEPPlus(FilePath, FileName);
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
        GetExcelUsingEPPlus(hdnFilePath.Value, hdnFileName.Value);
    }
    public void GetExcelSheets(DataTable dt, string FileName)
    {
        string IECode, InvoiceNo, InvoiceCurrency, InvoiceAmount, TenorDays, SupplierName, ThirdPartyName, MaximumDueDate, DateOfShipment, SuppliersCountry, ThirdPartyCountry, NameOfShippingCompany, VesselAirCarrierName, TransportDocNo, PortOfLoading, PortOfDischarge, RawMaterialCapitalGoods, GoodsDescription, CountryOfOrigin, BOENo, BOEDate, ADCode, GoodToPayDiscrepant, OutstandingAmountOfInvoice, AmountTobeRemitted, InvoicesToBePaid, Currency, PaymentCurrency, Remark, OttNo;
        decimal ExchRate;
        int norecinexcel = 0;
        int cntTot = 0;
        TF_DATA obj = new TF_DATA();
        int RowCount = dt.Rows.Count;
        int ColCount = dt.Columns.Count;
        string result = obj.SaveDeleteData("TF_IMPWH_DeleteTempPaymentData");
        ViewState["IECode"] = dt.Rows[3][0].ToString().Trim();
        ViewState["IEName"] = dt.Rows[2][2].ToString().Trim();
        ViewState["BeneName"] = dt.Rows[5][6].ToString().Trim();
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

                                PaymentCurrency = dt.Rows[i][29].ToString().Trim();
                                SqlParameter PPaymentCurrency = new SqlParameter("@PaymentCurrency", PaymentCurrency);

                                OttNo = txtORMNo.Text.Trim();
                                SqlParameter POttNo = new SqlParameter("@OttNo", OttNo);
                                if (txtExchangeRate.Text.Trim() != "")
                                {
                                    ExchRate = Convert.ToDecimal(txtExchangeRate.Text.Trim());
                                }
                                else
                                {
                                    ExchRate = 1;
                                }
                                SqlParameter PExchRate = new SqlParameter("@ExchRate", SqlDbType.Decimal);
                                PExchRate.Value = ExchRate;

                                SqlParameter PFileName = new SqlParameter("@FileName", FileName);

                                string UserName = Session["userName"].ToString().Trim();
                                SqlParameter PUserName = new SqlParameter("@UserName", UserName);

                                TF_DATA objDataInput = new TF_DATA();

                                string qryInput = "TF_IMPWH_PaymentFileUpload";
                                string dtInput = objDataInput.SaveDeleteData(qryInput, PIECode, PInvoiceNo, PInvoiceCurrency, PInvoiceAmount, PTenorDays, PPaymentDueDate, PSupplierName, PThirdPartyName, PMaximumDueDate, PDateOfShipment, PSuppliersCountry, PThirdPartyCountry, PNameOfShippingCompany, PVesselAirCarrierName, PTransportDocNo, PPortOfLoading, PPortOfDischarge, PRawMaterialCapitalGoods, PGoodsDescription, PCountryOfOrigin, PBOENo, PBOEDate, PADCode, PGoodToPayDiscrepant, POutstandingAmountOfInvoice, PAmountTobeRemitted, PInvoicesToBePaid, PCurrency, PUserName, PRemark, PFileName, PPaymentCurrency, POttNo, PExchRate);

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
        if (cntTot > 0)
        {
            labelMessage.Text = "<font color='red'>" + cntTot + "</font>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Records Uploaded out of " + "<font color='red'>" + norecinexcel + "</font>" + " from file " + FileName;
            fillgrid();
        }
    }
    private void GetExcelUsingEPPlus(string FilePath, string FileName)
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
                    newRow[cell.Start.Column - 1] = cell.Text;
                }

                dt.Rows.Add(newRow);
            }
        }
        catch (Exception Ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Decrypt Excel File First.')", true);
            return;
        }
        GetExcelSheets(dt, FileName);
    }
    protected void fillgrid()
    {
        string sign = ddlExchangeRateSign.SelectedValue;
        SqlParameter PSign = new SqlParameter("@Sign", sign);
        TF_DATA objget = new TF_DATA();
        DataTable dt = objget.getData("TF_IMPWH_FillPaymentGridTemp", PSign);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _PageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewInvoice.PageSize = _PageSize;
            GridViewInvoice.DataSource = dt.DefaultView;
            GridViewInvoice.DataBind();
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _PageSize);
            GridViewInvoice.Enabled = true;
            GridViewInvoice.Visible = true;
        }
        else
        {
            GridViewInvoice.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No Record(s) Found...";
            labelMessage.Visible = true;
        }
        DataTable dtORM = objget.getData("TF_IMPWH_GetAmountTotalFileUpload");
        lblPayAmtTotal.Visible = true;
        lblTotPayAmt.Text = dtORM.Rows[0][0].ToString();
        btnProcess.Visible = true;
        lblSearch.Visible = true;
        txtSearch.Visible = true;

    }
    protected void GridViewInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    DataRowView drv = e.Row.DataItem as DataRowView;
        //    hdnInvCurr.Value = drv["Invoice_Curr"].ToString();
        //    hdnPayCurr.Value = drv["PaymentCurrency"].ToString();
        //    if (drv["Status"].ToString() == "N")
        //    {
        //        e.Row.BackColor = System.Drawing.Color.Red;
        //        e.Row.ForeColor = System.Drawing.Color.White;
        //    }
        //}
    }
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewInvoice.PageIndex = 0;
        fillgrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewInvoice.PageIndex > 0)
        {
            GridViewInvoice.PageIndex = GridViewInvoice.PageIndex - 1;
        }
        fillgrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewInvoice.PageIndex != GridViewInvoice.PageCount - 1)
        {
            GridViewInvoice.PageIndex = GridViewInvoice.PageIndex + 1;
        }
        fillgrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewInvoice.PageIndex = GridViewInvoice.PageCount - 1;
        fillgrid();
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();

        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewInvoice.PageCount != GridViewInvoice.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewInvoice.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) :" + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewInvoice.PageIndex + 1) + " of " + GridViewInvoice.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewInvoice.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }

        if (GridViewInvoice.PageIndex != (GridViewInvoice.PageCount - 1))
        {
            pgcontrols.enablelastnav(btnnavnext, btnnavlast);
        }
        else
        {
            pgcontrols.disablelastnav(btnnavnext, btnnavlast);
        }

    }
    private void navigationVisibility(Boolean visibility)
    {
        lblpageno.Visible = visibility;
        lblrecordno.Visible = visibility;
        btnnavfirst.Visible = visibility;
        btnnavlast.Visible = visibility;
        btnnavnext.Visible = visibility;
        btnnavpre.Visible = visibility;
    }
    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void btnValidate_Click(object sender, EventArgs e)
    {
        //TF_DATA obj = new TF_DATA();
        //string _IECode = ViewState["IECode"].ToString().Trim();
        //string _Query = "TF_IMPWH_PaymentData_ValidateP";
        //SqlParameter PIECode = new SqlParameter("IECode", _IECode);
        //DataTable dt = obj.getData(_Query, PIECode);
        //if (dt.Rows.Count > 0)
        //{
        //    lblHint.Text = "<font color='red'>" + "Please Correct All Errors Then You Can Process Data.." + "</font>";
        //    label2.Text = "<font color='red'>" + dt.Rows.Count + "</font>" + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Error Records ";
        //    string _script = "window.open('../../Reports/IMPWHReports/TF_IMPWH_ViewGDPDataValidation.aspx?Type=Payment&IECode=" + _IECode + "','_blank','height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", _script, true);
        //    btnValidate.Enabled = true;
        //}
        //else
        //{
        //    lblHint.Text = "<font color='red'>" + "There is no Error Records You Can Process Data.." + "</font>";
        //    btnProcess.Enabled = true;
        //    label2.Text = "<font color='red'>" + dt.Rows.Count + "</font>" + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Error Records ";
        //}
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        int count = 0;
        if (Convert.ToDouble(lblTotPayAmt.Text) > Convert.ToDouble(lblOrmAmount.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Total Payment Amount is greater than Balance ORM Amount..')", true);
            fillgrid();
            return;
        }
        TF_DATA objdata = new TF_DATA();
        if (ViewState["Status"].ToString().Trim() == "Delete")
        {
            SqlParameter PFileName = new SqlParameter("@FileName", hdnFileName.Value);
            string result = objdata.SaveDeleteData("TF_IMPWH_DeletePaymentData", PFileName);
        }
        count = GridViewInvoice.Rows.Count;
        if (GridViewInvoice.Rows.Count > 0)
        {
            foreach (GridViewRow row in GridViewInvoice.Rows)
            {
                Label IECode = row.FindControl("lblIECode") as Label;
                Label SuppierName = row.FindControl("lblSupplierName") as Label;
                Label InvNo = row.FindControl("lblInvoiceNo") as Label;
                Label InvCurr = row.FindControl("lblInvoiceCurr") as Label;
                Label PaymentTerm = row.FindControl("LabelPayment_Term") as Label;
                Label PaymentDueDate = row.FindControl("LabelPayment_Due_Date") as Label;
                Label ThirdPartyName = row.FindControl("LabelThird_Party_Name") as Label;
                Label MaximumDueDate = row.FindControl("LabelMaximum_Due_Date") as Label;
                Label DateOfShipping = row.FindControl("LabelDate_Of_Shipping") as Label;
                Label SupplierCountry = row.FindControl("LabelSupplier_Country") as Label;
                Label ThirdPartyCountry = row.FindControl("LabelThird_Party_Country") as Label;
                Label NameShippingCompany = row.FindControl("LabelName_Of_Shipping_Comp") as Label;
                Label VesselAir = row.FindControl("LabelVessel_Air_Carrier_Name") as Label;
                Label TransportDocNo = row.FindControl("LabelTransport_Doc_No") as Label;
                Label PortOfLoading = row.FindControl("LabelPort_Of_Loading") as Label;
                Label PortOfDischarge = row.FindControl("LabelPort_Of_Discharge") as Label;
                Label RowMaterial = row.FindControl("LabelRawMaterial_CapitalGoods") as Label;
                Label GoodsDiscription = row.FindControl("LabelGoods_Description") as Label;
                Label CountryOfOrigin = row.FindControl("LabelCountry_Of_Origin") as Label;
                Label BOENo = row.FindControl("lblBOENo") as Label;
                Label BOEDate = row.FindControl("lblBOEDate") as Label;
                Label OutstandingAmt = row.FindControl("LabelOut_Standing_Amount") as Label;
                Label ExchRate = row.FindControl("LabelExchRate") as Label;
                string OttNo = txtORMNo.Text.Trim();
                string InvoiceToBePaid = "Pay";
                Label PaymentCurr = row.FindControl("lblPaymentCurr") as Label;
                Label Remark = row.FindControl("lblRemark") as Label;
                Label FileName = row.FindControl("LabelFileName") as Label;
                Label ADCode = row.FindControl("LabelAD_Code") as Label;
                TextBox InvAmt = row.FindControl("lblinvcamt") as TextBox;
                TextBox PayAmt = row.FindControl("txtPayAmt") as TextBox;

                SqlParameter PIECode = new SqlParameter("@IECode", IECode.Text);
                SqlParameter PSuppierName = new SqlParameter("@SuppierName", SuppierName.Text);
                SqlParameter PInvNo = new SqlParameter("@InvNo", InvNo.Text);
                SqlParameter PInvCurr = new SqlParameter("@InvCurr", InvCurr.Text);
                SqlParameter PPaymentTerm = new SqlParameter("@PaymentTerm", PaymentTerm.Text);
                SqlParameter PPaymentDueDate = new SqlParameter("@PaymentDueDate", PaymentDueDate.Text);
                SqlParameter PThirdPartyName = new SqlParameter("@ThirdPartyName", ThirdPartyName.Text);
                SqlParameter PMaximumDueDate = new SqlParameter("@MaximumDueDate", MaximumDueDate.Text);
                SqlParameter PDateOfShipping = new SqlParameter("@DateOfShipping", DateOfShipping.Text);
                SqlParameter PSupplierCountry = new SqlParameter("@SupplierCountry", SupplierCountry.Text);
                SqlParameter PThirdPartyCountry = new SqlParameter("@ThirdPartyCountry", ThirdPartyCountry.Text);
                SqlParameter PNameShippingCompany = new SqlParameter("@NameShippingCompany", NameShippingCompany.Text);
                SqlParameter PVesselAir = new SqlParameter("@VesselAir", VesselAir.Text);
                SqlParameter PTransportDocNo = new SqlParameter("@TransportDocNo", TransportDocNo.Text);
                SqlParameter PPortOfLoading = new SqlParameter("@PortOfLoading", PortOfLoading.Text);
                SqlParameter PPortOfDischarge = new SqlParameter("@PortOfDischarge", PortOfDischarge.Text);
                SqlParameter PRowMaterial = new SqlParameter("@RowMaterial", RowMaterial.Text);
                SqlParameter PGoodsDiscription = new SqlParameter("@GoodsDiscription", GoodsDiscription.Text);
                SqlParameter PCountryOfOrigin = new SqlParameter("@CountryOfOrigin", CountryOfOrigin.Text);
                SqlParameter PBOENo = new SqlParameter("@BOENo", BOENo.Text);
                SqlParameter PBOEDate = new SqlParameter("@BOEDate", BOEDate.Text);
                SqlParameter POutstandingAmt = new SqlParameter("@OutstandingAmt", OutstandingAmt.Text);
                SqlParameter PAmountToBeRamited = new SqlParameter("@AmountToBeRamited", InvAmt.Text);
                SqlParameter PExchRate = new SqlParameter("@ExchRate", ExchRate.Text);
                SqlParameter POttNo = new SqlParameter("@OttNo", OttNo);
                SqlParameter PInvoiceToBePaid = new SqlParameter("@InvoiceToBePaid", InvoiceToBePaid);
                SqlParameter PPaymentCurr = new SqlParameter("@PaymentCurr", PaymentCurr.Text);
                SqlParameter PRemark = new SqlParameter("@Remark", Remark.Text);
                SqlParameter PFileName = new SqlParameter("@FileName", FileName.Text);
                SqlParameter PADCode = new SqlParameter("@ADCode", ADCode.Text);
                SqlParameter PInvAmt = new SqlParameter("@InvAmt", InvAmt.Text);
                SqlParameter PPayAmt = new SqlParameter("@PayAmt", PayAmt.Text);
                string _AddedBy = Session["userName"].ToString();
                SqlParameter AddedBy = new SqlParameter("@Addedby", _AddedBy);
                string result3 = objdata.SaveDeleteData("TF_IMPWH_PaymentDataProcess", PIECode, PSuppierName, PInvNo, PInvCurr, PPaymentTerm, PPaymentDueDate, PThirdPartyName, PMaximumDueDate, PDateOfShipping, PSupplierCountry, PThirdPartyCountry, PNameShippingCompany, PVesselAir, PTransportDocNo, PPortOfLoading, PPortOfDischarge, PRowMaterial, PGoodsDiscription, PCountryOfOrigin, PBOENo, PBOEDate, POutstandingAmt, PAmountToBeRamited, PExchRate, POttNo, PInvoiceToBePaid, PPaymentCurr, PRemark, PFileName, PADCode, PPayAmt, AddedBy);
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('"+count+" Record Processed.')", true);
        }
        txtInputFile.Text = "";
        txtPartyID.Text = "";
        txtORMNo.Text = "";
        txtExchangeRate.Text = "";
        lblPayAmtTotal.Text = "";
        lblTotPayAmt.Text = "";
        lblOrmAmount.Text = "";
        lblcurren.Text = "";
        lblCustName.Text = "";
        btnProcess.Visible = false;
        lblPayAmtTotal.Visible = false;
        lblPayAmtTotal.Visible = false;
        lblSearch.Visible = false;
        txtSearch.Visible = false;
    }
    //public void DirectSettlement()
    //{
    //    string ADCode = Session["userADCode"].ToString();
    //    string IECode = ViewState["IECode"].ToString();
    //    string IEName = ViewState["IEName"].ToString();
    //    string BeneName = ViewState["BeneName"].ToString();
    //    TF_DATA objdata = new TF_DATA();
    //    SqlParameter ADCODE = new SqlParameter("@ADCODE", ADCode);

    //    string _AddedBy = Session["userName"].ToString();
    //    SqlParameter AddedBy = new SqlParameter("@Addedby", _AddedBy);

    //    SqlParameter p1 = new SqlParameter("@Ref_no", txtOTTRefNo.Text);
    //    SqlParameter p2 = new SqlParameter("@AD_Code", ADCode);
    //    SqlParameter p3 = new SqlParameter("@Amount", txtTotalOttAmt.Text);
    //    SqlParameter p4 = new SqlParameter("@Curr", lblORMCurrency.Text);
    //    SqlParameter p5 = new SqlParameter("@Payment_Date", txtOTTDate.Text);
    //    SqlParameter p6 = new SqlParameter("@IE_Code", IECode);
    //    SqlParameter p7 = new SqlParameter("@IE_Name", IEName);
    //    SqlParameter p8 = new SqlParameter("@Ben_Name", BeneName);
    //    decimal ExRate = 1;
    //    if (txtExchangeRate.Text == "")
    //    {
    //        ExRate = 1;
    //    }
    //    else
    //    {
    //        ExRate = Convert.ToDecimal(txtExchangeRate.Text);
    //    }
    //    SqlParameter p9 = new SqlParameter("@ExchangeRate", ExRate);
    //    string result1 = objdata.SaveDeleteData("TF_IMPWH_GET_DOCNO_Payment_TEMP", ADCODE, p9, p1);
    //    string result0 = objdata.SaveDeleteData("TF_IMPWH_ADDORM", p1, p2, p3, p4, p5, p6, p7, p8, p9);
    //    //string result2 = objdata.SaveDeleteData("TF_IDPMS_SET_PayRefNo_SETTLEMENT_TEMP", ADCODE, AddedBy);
    //    string result3 = objdata.SaveDeleteData("TF_IMPWH_BOE_Settlement_FileUpload_Process", ADCODE, AddedBy);
    //    txtTotalOttAmt.Text = "";
    //    lblORMCurrency.Text = "";
    //    txtOTTRefNo.Text = "";
    //    txtExchangeRate.Text = "";
    //    txtOTTDate.Text = "";
    //    txtInputFile.Text = "";
    //    btnProcess.Visible = false;
    //}
    public void InsertUplodedFile()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PFileName = new SqlParameter("@FileName", ViewState["FileName"].ToString().Trim());
        SqlParameter PFileType = new SqlParameter("@FileType", "GDP");
        SqlParameter PUserName = new SqlParameter("@UserName", Session["UserName"].ToString().Trim());
        string result = obj.SaveDeleteData("TF_IMPWH_InsertUploadeFile", PFileName, PFileType, PUserName);
    }
    //========================//
    protected void txtORMNo_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@OTTNo", SqlDbType.VarChar);
        p1.Value = txtORMNo.Text.Trim();

        SqlParameter p2 = new SqlParameter("@iecode", SqlDbType.VarChar);
        p2.Value = txtPartyID.Text.Trim();

        SqlParameter p3 = new SqlParameter("@branch", SqlDbType.VarChar);
        p3.Value = ddlBranch.Text.Trim();

        string _query = "TF_GetOTTDetails";


        DataTable dt = objData.getData(_query, p1, p2, p3);
        if (dt.Rows.Count > 0)
        {
            lblOrmAmount.Text = dt.Rows[0]["Amount"].ToString();
            lblcurren.Text = dt.Rows[0]["Currency"].ToString();
        }
        else
        {
            lblOrmAmount.Text = "";
            txtORMNo.Text = "";
            lblcurren.Text = "";
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
        //ListItem li = new ListItem();
        //li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            //li.Text = "---Select---";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else { }
        //    li.Text = "No record(s) found";

        //ddlBranch.Items.Insert(0, li);
    }
    protected void txtPartyID_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@customerACNo", SqlDbType.VarChar);
        p1.Value = txtPartyID.Text.Trim();

        SqlParameter p2 = new SqlParameter("@branch", SqlDbType.VarChar);
        p2.Value = ddlBranch.SelectedValue.ToString();
        string _query = "TF_GetCustomerMasterDetails1";


        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            lblCustName.Text = dt.Rows[0]["CUST_NAME"].ToString();
        }
        else
        {
            lblCustName.Text = "";
            txtPartyID.Text = "";

        }


    }
    protected void txtPayAmt_textchange(object sender, EventArgs e)
    {
        double sumofPayBill = 0;
        for (int i = 0; i < GridViewInvoice.Rows.Count; i++)
        {
            if (GridViewInvoice.Rows.Count > 0)
            {
                TextBox txtPamt = new TextBox();
                txtPamt = (TextBox)GridViewInvoice.Rows[i].Cells[6].FindControl("txtPayAmt");
                double txtPayAmt = Convert.ToDouble(txtPamt.Text);
                sumofPayBill = sumofPayBill + Convert.ToDouble(txtPamt.Text);
            }
        }
        lblTotPayAmt.Text = sumofPayBill.ToString("F");
    }
}