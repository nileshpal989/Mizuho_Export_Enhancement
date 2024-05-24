using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using System.Net;
using Microsoft.Win32;
using System.Threading;
using System.Diagnostics;
//using Microsoft.VisualBasic.FileIO;
using System.Configuration;
using System.Globalization;

public partial class RRETURN_Transaction_FileUpload_New : System.Web.UI.Page
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
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;
                txtFromDate.Text = Session["FrRelDt"].ToString().Trim();
                txtToDate.Text = Session["ToRelDt"].ToString().Trim();
                //btnUpload.Attributes.Add("onclick", "return validate();");
            }
        }
    }
    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_RET_GetBranchandADcodeList";
        DataTable dt = objData.getData(_query);
        ddlBranch.Items.Clear();
        if (dt.Rows.Count > 0)
        {
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(10000);
        label2.Text = "";
        lblHint.Text = "";
        TF_DATA objData = new TF_DATA();
        DataTable dt = new DataTable();

        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";

        DateTime documentDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
        DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);
        // new 
        TF_DATA objData_Check = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@Branch", SqlDbType.VarChar);
        p1.Value = ddlBranch.SelectedItem.ToString().Trim();

        SqlParameter p2 = new SqlParameter("@FromDate", txtFromDate.Text);
        SqlParameter p3 = new SqlParameter("@ToDate", txtToDate.Text);
        //string _qryChk = "Rreturn_Delete_CSV_File_transection";

        //DataTable dtChk = objData_Check.getData(_qryChk, p1, p2, p3);

        if (fileinhouse.HasFile)
        {
            string fname;
            fname = fileinhouse.FileName;
            //string path = Server.MapPath(fileinhouse.PostedFile.FileName);
            txtInputFile.Text = fileinhouse.PostedFile.FileName;
            if (fname.Contains(".xls") == true || fname.Contains(".xlsx") == true || fname.Contains(".XLS") == true || fname.Contains(".XLSX") == true)
            {
                string FileName = Path.GetFileName(fileinhouse.PostedFile.FileName);
                string Extension = Path.GetExtension(fileinhouse.PostedFile.FileName);
                string FolderPath = Server.MapPath("../Uploaded_Files");

                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }

                FileName = FileName.Replace(" ", "");

                string FilePath = FolderPath + "\\" + System.IO.Path.GetFileName(fileinhouse.PostedFile.FileName);
                fileinhouse.SaveAs(FilePath);
                GetExcelSheets(FilePath, Extension, "No");
            }
        }
        else
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Excel File First.')", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "Alert('Please Select Excel File First.')", true);
        }
    }
    private void GetExcelSheets(string FilePath, string Extension, string isHDR)
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            dateInfo.ShortDatePattern = "dd/MM/yyyy";
            DateTime documentDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
            DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);
            TF_DATA objData_Check = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@Branch", SqlDbType.VarChar);
            p1.Value = ddlBranch.SelectedItem.ToString().Trim();
            SqlParameter p2 = new SqlParameter("@startdate", SqlDbType.VarChar);
            p2.Value = documentDate.ToString("MM/dd/yyyy");
            SqlParameter p3 = new SqlParameter("@enddate", SqlDbType.VarChar);
            p3.Value = documentDate1.ToString("MM/dd/yyyy");
            SqlParameter _adcode = new SqlParameter("@adcode", SqlDbType.VarChar);
            _adcode.Value = ddlBranch.SelectedItem.Value;
            string _qryChk = "TF_RReturn_CSV_Delete";
            DataTable dtChk = objData_Check.getData(_qryChk, _adcode);


            string conStr = "";
            int norecinexcel = 0;
            int cnt = 0;
            int cntTot = 0;
            int errorcount = 0;

            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
                case ".XLS": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".XLSX": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, isHDR);

            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();

            DataTable dt = new DataTable();

            cmdExcel.Connection = connExcel;
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();
            connExcel.Open();

            cmdExcel.CommandText = "SELECT F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23,F24,F25,F26,F27,F28,F29 FROM [" + SheetName + "]";

            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();
            int RowCount = dt.Rows.Count;
            if (dt.Rows.Count > 1)
            {
                for (int i = 1; i < RowCount; i++)
                {
                    if (dt.Rows[i][0].ToString().Trim() != "")
                    {
                        norecinexcel = norecinexcel + 1;

                        string strADCode = "";
                        strADCode = ddlBranch.SelectedValue.ToString();
                        SqlParameter adcode = new SqlParameter("@adcode", SqlDbType.VarChar);
                        adcode.Value = strADCode;
                        string strModType = "";
                        strModType = dt.Rows[i][0].ToString();
                        SqlParameter p4 = new SqlParameter("@modtype", SqlDbType.VarChar);
                        p4.Value = strModType;
                        string strTransDt = "";
                        strTransDt = dt.Rows[i][1].ToString();
                        SqlParameter p5 = new SqlParameter("@transdate", SqlDbType.VarChar);
                        p5.Value = strTransDt;
                        string strDocNo = "";
                        strDocNo = dt.Rows[i][2].ToString();
                        SqlParameter p6 = new SqlParameter("@docno", SqlDbType.VarChar);
                        p6.Value = strDocNo;
                        string strPURPOSE_ID = "";
                        strPURPOSE_ID = dt.Rows[i][3].ToString();
                        SqlParameter p7 = new SqlParameter("@purposeid", SqlDbType.VarChar);
                        p7.Value = strPURPOSE_ID;
                        string strCURR = "";
                        string strVOSTROAC = "";
                        strCURR = dt.Rows[i][4].ToString();
                        SqlParameter p8 = new SqlParameter("@currency", SqlDbType.VarChar);
                        p8.Value = strCURR;
                        if ((strCURR == "INR") || (strCURR == "ACD"))
                            strVOSTROAC = "V";
                        else
                            strVOSTROAC = "N";
                        SqlParameter p9 = new SqlParameter("@vostroac", SqlDbType.VarChar);
                        p9.Value = strVOSTROAC;
                        string strAMOUNT = "";
                        strAMOUNT = dt.Rows[i][5].ToString().Replace(",", "");
                        SqlParameter p10 = new SqlParameter("@amount", SqlDbType.VarChar);
                        p10.Value = strAMOUNT;
                        string strEXRT = "";
                        strEXRT = dt.Rows[i][6].ToString().Replace(",", "");
                        if (strCURR == "INR")
                            strEXRT = "1";
                        SqlParameter p11 = new SqlParameter("@exrt", SqlDbType.VarChar);
                        p11.Value = strEXRT;
                        string strBN_COUNTRY_CODE = "";
                        strBN_COUNTRY_CODE = dt.Rows[i][7].ToString();
                        SqlParameter p12 = new SqlParameter("@bincountrycode", SqlDbType.VarChar);
                        p12.Value = strBN_COUNTRY_CODE;
                        string strREMMITERCountry = "";
                        strREMMITERCountry = dt.Rows[i][8].ToString();
                        SqlParameter PstrREMMITERCountry = new SqlParameter("@REMMITERCountry", SqlDbType.VarChar);
                        PstrREMMITERCountry.Value = strREMMITERCountry;
                        //if (strREMMITERCountry != "")
                        //{
                        //    PstrREMMITERCountry.Value = strREMMITERCountry;
                        //}
                        //else
                        //{
                        //    PstrREMMITERCountry.Value = strBN_COUNTRY_CODE;
                        //}
                        string strAC_COUNTRY_CODE = "";
                        strAC_COUNTRY_CODE = dt.Rows[i][9].ToString();
                        SqlParameter p13 = new SqlParameter("@accountrycode", SqlDbType.VarChar);
                        p13.Value = strAC_COUNTRY_CODE;
                        string strBankCode = "";
                        strBankCode = dt.Rows[i][10].ToString();
                        SqlParameter p14 = new SqlParameter("@vostrobankcode", SqlDbType.VarChar);
                        if (strVOSTROAC == "N")
                        {
                            strBankCode = "";
                        }
                        p14.Value = strBankCode;
                        string strBENEFICIARYNAME = "";
                        strBENEFICIARYNAME = dt.Rows[i][11].ToString();
                        SqlParameter p15 = new SqlParameter("@benname", SqlDbType.VarChar);
                        p15.Value = strBENEFICIARYNAME;
                        if (strBENEFICIARYNAME.Contains("\n"))
                        {
                            strBENEFICIARYNAME = strBENEFICIARYNAME.Replace("\n", "");
                        }
                        string strREMMITERNAME = "";
                        strREMMITERNAME = dt.Rows[i][12].ToString();
                        SqlParameter p16 = new SqlParameter("@remitername", SqlDbType.VarChar);
                        p16.Value = strREMMITERNAME;
                        if (strREMMITERNAME.Contains("\n"))
                        {
                            strREMMITERNAME = strREMMITERNAME.Replace("\n", "");
                        }
                        string strBILLNO = "";
                        strBILLNO = dt.Rows[i][13].ToString();
                        SqlParameter p17 = new SqlParameter("@billno", SqlDbType.VarChar);
                        p17.Value = strBILLNO;
                        string strIECODE = "";
                        strIECODE = dt.Rows[i][14].ToString();
                        SqlParameter p18 = new SqlParameter("@iecode", SqlDbType.VarChar);
                        p18.Value = strIECODE;
                        string strFORMSRNO = "";
                        strFORMSRNO = dt.Rows[i][15].ToString();
                        SqlParameter p19 = new SqlParameter("@formsrno", SqlDbType.VarChar);
                        p19.Value = strFORMSRNO;
                        string strPORT_CODE = "";
                        strPORT_CODE = dt.Rows[i][16].ToString();
                        SqlParameter p20 = new SqlParameter("@portcode", SqlDbType.VarChar);
                        p20.Value = strPORT_CODE;
                        string strSHIPPING_BILL_NO = "";
                        strSHIPPING_BILL_NO = dt.Rows[i][17].ToString();
                        SqlParameter p21 = new SqlParameter("@shippingbillno", SqlDbType.VarChar);
                        p21.Value = strSHIPPING_BILL_NO;
                        string strSHIPPING_BILL_DT = "";
                        strSHIPPING_BILL_DT = dt.Rows[i][18].ToString();
                        SqlParameter p22 = new SqlParameter("@shippingbilldate", SqlDbType.VarChar);
                        p22.Value = strSHIPPING_BILL_DT;
                        string strCUSTOMSRNO = "";
                        strCUSTOMSRNO = dt.Rows[i][19].ToString();
                        SqlParameter p23 = new SqlParameter("@custno", SqlDbType.VarChar);
                        p23.Value = strCUSTOMSRNO;
                        string strREALISED_AMT = "";
                        strREALISED_AMT = dt.Rows[i][20].ToString().Replace(",", "");
                        SqlParameter p24 = new SqlParameter("@relamt", SqlDbType.VarChar);
                        p24.Value = strREALISED_AMT;
                        string strSCHEDULENO = "";
                        strSCHEDULENO = dt.Rows[i][21].ToString();
                        SqlParameter p25 = new SqlParameter("@schedno", SqlDbType.VarChar);
                        p25.Value = strSCHEDULENO;
                        string strLCINDICATION = "";
                        strLCINDICATION = dt.Rows[i][22].ToString();
                        SqlParameter p26 = new SqlParameter("@lindication", SqlDbType.VarChar);
                        if (strLCINDICATION == "0" || strLCINDICATION == "1")
                        {
                            strLCINDICATION = strLCINDICATION;
                        }
                        else
                        {
                            strLCINDICATION = "0";
                        }
                        p26.Value = strLCINDICATION;
                        string strFR_FORTNIGHT_DT = "";
                        strFR_FORTNIGHT_DT = dt.Rows[i][23].ToString();
                        SqlParameter p27 = new SqlParameter("@fromdate", SqlDbType.VarChar);
                        p27.Value = strFR_FORTNIGHT_DT;
                        string strTO_FORTNIGHT_DT = "";
                        strTO_FORTNIGHT_DT = dt.Rows[i][24].ToString();
                        SqlParameter p28 = new SqlParameter("@todate", SqlDbType.VarChar);
                        p28.Value = strTO_FORTNIGHT_DT;

                        string RemAcNo = "";
                        RemAcNo = dt.Rows[i][25].ToString();
                        SqlParameter p29 = new SqlParameter("@RemAcNo", SqlDbType.VarChar);
                        p29.Value = RemAcNo;

                        string RemiAdd = "";
                        RemiAdd = dt.Rows[i][26].ToString();
                        SqlParameter p30 = new SqlParameter("@RemiAdd", SqlDbType.VarChar);
                        p30.Value = RemiAdd;
                        if (RemiAdd.Contains("\n"))
                        {
                            RemiAdd = RemiAdd.Replace("\n", "");
                        }
                        string BeneAcNo = "";
                        BeneAcNo = dt.Rows[i][27].ToString();
                        SqlParameter p31 = new SqlParameter("@BeneAcNo", SqlDbType.VarChar);
                        p31.Value = BeneAcNo;

                        string BeneAdd = "";
                        BeneAdd = dt.Rows[i][28].ToString();
                        SqlParameter p32 = new SqlParameter("@BeneAdd", SqlDbType.VarChar);
                        p32.Value = BeneAdd;
                        if (BeneAdd.Contains("\n"))
                        {
                            BeneAdd = BeneAdd.Replace("\n", "");
                        }

                        string _userName = Session["userName"].ToString().Trim();
                        SqlParameter p33 = new SqlParameter("@adduser", SqlDbType.VarChar);
                        p33.Value = _userName;
                        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                        SqlParameter p34 = new SqlParameter("@adddate", SqlDbType.VarChar);
                        p34.Value = _uploadingDate;

                        TF_DATA objDataInput = new TF_DATA();

                        string qryInput = "Transaction_FileUpload_New";
                        string dtInput = objDataInput.SaveDeleteData(qryInput, p1, p2, p3, adcode, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20,
                            p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, PstrREMMITERCountry);
                        if (dtInput == "inserted")
                        {
                            cnt++;
                        }
                        else
                        {
                            string _Remarks = dtInput;
                            SqlParameter p35 = new SqlParameter("@Remarks", SqlDbType.VarChar);
                            p35.Value = _Remarks;
                            SqlParameter p36 = new SqlParameter("@SRNO", SqlDbType.VarChar);
                            p36.Value = i;
                            errorcount++;
                            labelMessage.Text = dtInput;
                            string qryInput1 = "Transaction_FileUpload_New_Not_Uploaded";
                            string dtInput1 = objDataInput.SaveDeleteData(qryInput1, p1, p2, p3, adcode, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20,
                                p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36);
                        }
                        cntTot++;
                    }
                }
            }
            if (lblHint.Text == "")
            {
                labelMessage.Text = "<font color='red'>" + cnt + "</font>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Records Uploaded out of " + "<font color='red'>" + cntTot + "</font>" + " from file " + System.IO.Path.GetFileName(fileinhouse.PostedFile.FileName);
                string record = cnt + " records uploaded out of " + cntTot + " records from the file " + System.IO.Path.GetFileName(fileinhouse.PostedFile.FileName);

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "uploadfile", "VAlert('" + record + "')", true);
                if (errorcount > 0)
                {
                    string script = "window.open('RET_CSV_Not_Uploaded.aspx?frm=" + txtFromDate.Text + "&to=" + txtToDate.Text + "&ADCode=" + ddlBranch.SelectedItem.Value + "','_blank','height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
                }
            }
        }
        catch (Exception Ex)
        {
            SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
            ADCODE.Value = ddlBranch.SelectedValue.ToString();

            SqlParameter MENUNAME = new SqlParameter("@MENUNAME", SqlDbType.VarChar);
            MENUNAME.Value = "Excel Input Data File Upload at Branch";

            SqlParameter IPAddress = new SqlParameter("@IPAddress", SqlDbType.VarChar);
            IPAddress.Value = GetIPAddress();

            SqlParameter URL = new SqlParameter("@URL", SqlDbType.VarChar);
            URL.Value = HttpContext.Current.Request.Url.AbsoluteUri;

            SqlParameter TYPE = new SqlParameter("@TYPE", SqlDbType.VarChar);
            TYPE.Value = Ex.GetType().Name.ToString();

            SqlParameter Message = new SqlParameter("@Message", SqlDbType.VarChar);
            Message.Value = Ex.Message;

            SqlParameter StackTrace = new SqlParameter("@StackTrace", SqlDbType.VarChar);
            StackTrace.Value = Ex.StackTrace;

            SqlParameter Source = new SqlParameter("@Source", SqlDbType.VarChar);
            Source.Value = Ex.Source;

            SqlParameter TargetSite = new SqlParameter("@TargetSite", SqlDbType.VarChar);
            TargetSite.Value = Ex.TargetSite.ToString();

            SqlParameter DATETIME = new SqlParameter("@DATETIME", SqlDbType.VarChar);
            DATETIME.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

            SqlParameter UserName = new SqlParameter("@UserName", SqlDbType.VarChar);
            UserName.Value = Session["userName"].ToString().Trim();

            TF_DATA objDataInput = new TF_DATA();
            string qryError = "TF_RET_ErrorException";
            string dtInput1 = objDataInput.SaveDeleteData(qryError, ADCODE, MENUNAME, IPAddress, URL, Message, StackTrace, Source, TargetSite, DATETIME, TYPE, UserName);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Page contains error.')", true);
            Response.Redirect("ErrorPage.aspx?PageHeader=Error Page");
            //labelMessage.Text = Ex.Message;
        }
    }
    protected void btnValidate_Click(object sender, EventArgs e)
    {
        //if (lblHint.Text == "")
        //{
        try
        {
            if (txtInputFile.Text != "")
            {
                TF_DATA objData = new TF_DATA();
                string script = "";


                System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
                dateInfo.ShortDatePattern = "dd/MM/yyyy";

                DateTime documentDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
                DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);
                // new 

                SqlParameter p1 = new SqlParameter("@Branch", SqlDbType.VarChar);
                p1.Value = ddlBranch.SelectedItem.ToString().Trim();

                SqlParameter p2 = new SqlParameter("@FromDate", txtFromDate.Text);
                SqlParameter p3 = new SqlParameter("@ToDate", txtToDate.Text);
                SqlParameter _adcode = new SqlParameter("@_adcode", SqlDbType.VarChar);
                _adcode.Value = ddlBranch.SelectedItem.Value;
                string _qryChk = "RRETURN_CSV_Validate";

                DataTable dt = objData.getData(_qryChk, p1, p2, p3, _adcode);
                if (dt.Rows.Count > 0)
                {
                    //btnProcess.Enabled = false;
                    lblHint.Text = "<font color='red'>" + "Please Correct All Errors Then You Can Process Data.." + "</font>";
                    label2.Text = "<font color='red'>" + dt.Rows.Count + "</font>" + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Error Records ";
                    // script = "window.open('RET_CSV_Validation.aspx','_blank','height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
                    string record = +dt.Rows.Count + " error records found.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "uploadfile", "Valiadat('" + record + "'+' Please click on OK to view the error report.')", true);

                    script = "window.open('RET_CSV_Validation.aspx','_blank','height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
                }
                else
                {
                    label2.Text = "<font color='red'>" + dt.Rows.Count + "</font>" + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Error Records ";
                    //lblHint.Text = "";
                    string record = " No Errors Records ";

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "uploadfile", "VAlert('" + record + "')", true);
                }
            }
            else
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload Excel File First.')", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "Alert('Please Select Excel File First.')", true);
            }
            //}
            //else
            //{
            //    lblHint.Text = lblHint.Text;
            //}
        }
        
        catch (Exception Ex)
        {
            SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
            ADCODE.Value = ddlBranch.SelectedValue.ToString();

            SqlParameter MENUNAME = new SqlParameter("@MENUNAME", SqlDbType.VarChar);
            MENUNAME.Value = "Excel Input Data File Upload at Branch";

            SqlParameter IPAddress = new SqlParameter("@IPAddress", SqlDbType.VarChar);
            IPAddress.Value = GetIPAddress();

            SqlParameter URL = new SqlParameter("@URL", SqlDbType.VarChar);
            URL.Value = HttpContext.Current.Request.Url.AbsoluteUri;

            SqlParameter TYPE = new SqlParameter("@TYPE", SqlDbType.VarChar);
            TYPE.Value = Ex.GetType().Name.ToString();

            SqlParameter Message = new SqlParameter("@Message", SqlDbType.VarChar);
            Message.Value = Ex.Message;

            SqlParameter StackTrace = new SqlParameter("@StackTrace", SqlDbType.VarChar);
            StackTrace.Value = Ex.StackTrace;

            SqlParameter Source = new SqlParameter("@Source", SqlDbType.VarChar);
            Source.Value = Ex.Source;

            SqlParameter TargetSite = new SqlParameter("@TargetSite", SqlDbType.VarChar);
            TargetSite.Value = Ex.TargetSite.ToString();

            SqlParameter DATETIME = new SqlParameter("@DATETIME", SqlDbType.VarChar);
            DATETIME.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

            SqlParameter UserName = new SqlParameter("@UserName", SqlDbType.VarChar);
            UserName.Value = Session["userName"].ToString().Trim();

            TF_DATA objDataInput = new TF_DATA();
            string qryError = "TF_RET_ErrorException";
            string dtInput1 = objDataInput.SaveDeleteData(qryError, ADCODE, MENUNAME, IPAddress, URL, Message, StackTrace, Source, TargetSite, DATETIME, TYPE, UserName);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Page contains error.')", true);
            Response.Redirect("ErrorPage.aspx?PageHeader=Error Page");
            //labelMessage.Text = Ex.Message;
        }
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        if (txtInputFile.Text != "")
        {
            if (lblHint.Text == "")
            {
                SqlParameter p1 = new SqlParameter("@Branch", SqlDbType.VarChar);
                p1.Value = ddlBranch.SelectedItem.ToString().Trim();

                SqlParameter p2 = new SqlParameter("@FromDate", txtFromDate.Text);
                SqlParameter p3 = new SqlParameter("@ToDate", txtToDate.Text);
                SqlParameter _adcode = new SqlParameter("@_adcode", SqlDbType.VarChar);
                _adcode.Value = ddlBranch.SelectedItem.Value;
                TF_DATA objdata = new TF_DATA();
                DataTable dt = objdata.getData("Rreturn_Delete_CSV_File_transection", p1, p2, p3);
                //if (dt.Rows.Count > 0)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('There are still Error Records in CSV File.Please Correct and Reupload and Revalidate till there are no error records.')", true);

                //}
                //else
                //{
                string result = objdata.SaveDeleteData("Rreturn_CSV_Upload_Process_transection", _adcode);
                if (result.Substring(0, 8) == "Uploaded")
                {
                    label3.Text = "<font color='red'>" + result.Substring(8) + "</font>" + "&nbsp;&nbsp;&nbsp;&nbsp;Valid Records Processed ";
                    string record = result.Substring(8) + " Valid Records Processed .";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "uploadfile", "Alert('" + record + "')", true);
                }
                else
                {

                    label3.Text = " <font color='red'>" + "0 " + "</font>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Records processed ";
                }
                //}
            }
            else
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Correct All Errors Then You Can Process Data..')", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "Alert('Please Correct All Errors Then You Can Process Data.')", true);
            }
        }
        else
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload Excel File.')", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "Alert('Please Select Excel File First.')", true);
        }
    }
    public static string GetIPAddress()
    {
        string ipAddress = string.Empty;
        foreach (IPAddress item in Dns.GetHostAddresses(HttpContext.Current.Request.UserHostAddress))
        {
            if (item.AddressFamily.ToString().Equals("InterNetwork"))
            {
                ipAddress = item.ToString();
                break;
            }
        }
        if (!string.IsNullOrEmpty(ipAddress))
        {
            return ipAddress;
        }
        foreach (IPAddress item in Dns.GetHostAddresses(Dns.GetHostName()))
        {
            if (item.AddressFamily.ToString().Equals("InterNetwork"))
            {
                ipAddress = item.ToString();
                break;
            }
        }
        return ipAddress;
    }
}