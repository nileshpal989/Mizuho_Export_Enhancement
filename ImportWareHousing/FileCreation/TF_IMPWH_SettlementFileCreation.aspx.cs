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

public partial class ImportWareHousing_FileCreation_TF_IMPWH_SettlementFileCreation : System.Web.UI.Page
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
    //protected void rdballcust_CheckedChanged(object sender, EventArgs e)
    //{
    //    rdbselected.Checked = false;
    //    rdballcust.Checked = true;
    //    selectedcust.Visible = false;
    //    txtCust.Text = "";
    //    lblcustname.Text = "";
    //}
    //protected void rdbselected_CheckedChanged(object sender, EventArgs e)
    //{
    //    rdbselected.Checked = true;
    //    rdballcust.Checked = false;
    //    selectedcust.Visible = true;
    //}
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        CreteExcelForSettlement();
    }
    public void CreteExcelForSettlement()
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
                    string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPWH/Settlement/");

                    SqlParameter p1 = new SqlParameter("@IECode", SqlDbType.VarChar);
                    p1.Value = _IECode;
                    SqlParameter p2 = new SqlParameter("@AsOnDate", SqlDbType.VarChar);
                    p2.Value = _FromDate;
                    SqlParameter p3 = new SqlParameter("@branch", SqlDbType.VarChar);
                    p3.Value = _ADCode;

                    string _qry = "TF_IMPWH_SettlementFileCreation";
                    SqlParameter PCustAbrr = new SqlParameter("@CustAbrr", SqlDbType.VarChar);
                    PCustAbrr.Value = _CustAbrr;
                    SqlParameter PFileType = new SqlParameter("@FileType", "Settlement");
                    string FileName = obj.SaveDeleteData("TF_IMPWH_GetFileName", PCustAbrr, PFileType);
                    DataTable dt = obj.getData(_qry, p1, p2, p3);
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
                            ws.Cells["A" +1].Value = dt.Columns[0].ColumnName;
                            ws.Cells["B" +1].Value = dt.Columns[1].ColumnName;
                            ws.Cells["C" +1].Value = dt.Columns[2].ColumnName;
                            ws.Cells["D" +1].Value = dt.Columns[3].ColumnName;
                            ws.Cells["E" +1].Value = dt.Columns[4].ColumnName;
                            ws.Cells["F" +1].Value = dt.Columns[5].ColumnName;
                            ws.Cells["G" +1].Value = dt.Columns[6].ColumnName;
                            ws.Cells["H" +1].Value = dt.Columns[7].ColumnName;
                            ws.Cells["I" +1].Value = dt.Columns[8].ColumnName;
                            ws.Cells["J" +1].Value = dt.Columns[9].ColumnName;
                            
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
                                ws.Cells["I" + (j + 2)].Value = dt.Rows[j][8];
                                ws.Cells["J" + (j + 2)].Value = dt.Rows[j][9];

                                ws.Cells["G" + (j + 2)].Style.Numberformat.Format = "0.00";
                                ws.Cells["H" + (j + 2)].Style.Numberformat.Format = "0.00";

                                SqlParameter PPort_Of_Discharge = new SqlParameter("@Port_Of_Discharge", dt.Rows[j][0].ToString());
                                SqlParameter PBOE_No = new SqlParameter("@BOE_No", dt.Rows[j][1].ToString());
                                SqlParameter PBOE_Date = new SqlParameter("@BOE_Date", dt.Rows[j][2].ToString());
                                SqlParameter PIECode = new SqlParameter("@IECode", dt.Rows[j][3].ToString());
                                SqlParameter POTT_No = new SqlParameter("@OTT_No", dt.Rows[j][4].ToString());
                                SqlParameter PInvoice_No = new SqlParameter("@Invoice_No", dt.Rows[j][5].ToString());
                                SqlParameter POut_Standing_Amount = new SqlParameter("@Out_Standing_Amount", dt.Rows[j][6].ToString());
                                SqlParameter PPayment_Amt = new SqlParameter("@Payment_Amt", Convert.ToDecimal(dt.Rows[j][7]));
                                SqlParameter PFileName = new SqlParameter("@FileName", FileName);
                                SqlParameter PUserName = new SqlParameter("@UserName", Session["userName"].ToString());
                                string SaveResult = obj.SaveDeleteData("TF_IMPWH_UpdatePaymentTableStatus", PPort_Of_Discharge, PBOE_No, PBOE_Date, PIECode, POTT_No, PInvoice_No, POut_Standing_Amount, PPayment_Amt, PFileName, PUserName);

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
                        ViewState["FileName"] = FileName;
                        ViewState["FileType"] = "Settlement";
                        lblbop6name.Text = "Settlement File Created Successfully";
                        lnkDownload.Visible = true;
                        SqlParameter PFileName1 = new SqlParameter("@FileName", FileName);
                        SqlParameter PRows = new SqlParameter("@Rows", RowCount);
                        SqlParameter PUserName1 = new SqlParameter("@UserName", Session["userName"].ToString());
                        string Result = obj.SaveDeleteData("TF_IMPWH_InsertFileName", PFileName1, PUserName1, PFileType, PRows);
                        string _serverName = obj.GetServerName();
                        string path = "file://" + _serverName + "/TF_GeneratedFiles/IMPWH/Settlement";
                        string link = "/TF_GeneratedFiles/IMPWH/Settlement";
                        labelMessage.Text = "File Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
                        string script1 = "alert('Settlement File Created Successfully')";
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