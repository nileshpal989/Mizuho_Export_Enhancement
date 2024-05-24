using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Math;
using System.Runtime.InteropServices.ComTypes;
using System.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Database;
using System.Runtime.InteropServices;


public partial class EXP_rptGenerateGBaseData : System.Web.UI.Page
{
    string s = "";
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
                ddlBranch.SelectedValue = Session["userLBCode"].ToString();
                ddlBranch.Enabled = false;
                ddlbranchsettlement.SelectedValue = Session["userLBCode"].ToString();
                ddlbranchsettlement.Enabled = true;
                fillBranch();
                fillBranchsettlement();
                getbranchdetails();
                txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                ddlBranch.Focus();
                txtfromdatesettlement.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txttodatesettlement.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                ddlbranchsettlement.Focus();
            }
            txtFromDate.Attributes.Add("onblur", "return isValidDate(" + txtFromDate.ClientID + "," + "'From Date.'" + ");");
            txtToDate.Attributes.Add("onblur", "return isValidDate(" + txtToDate.ClientID + "," + "'To Date.'" + ");");
            btnCreate.Attributes.Add("onclick", "return validateSave();");
            txtfromdatesettlement.Attributes.Add("onblur", "return isValidDate(" + txtFromDate.ClientID + "," + "'From Date.'" + ");");
            txttodatesettlement.Attributes.Add("onblur", "return isValidDate(" + txtToDate.ClientID + "," + "'To Date.'" + ");");
            btngenretesettlement.Attributes.Add("onclick", "return validateSave();");
        }
    }

    public void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", "");
        string _query = "TF_GetBranchDetails";
        System.Data.DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchCode";

            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";
        ddlBranch.Items.Insert(0, li);
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        TF_DATA objData1 = new TF_DATA();

        if (txtFromDate.Text == "" || txtToDate.Text == "")
        {
            return;
        }

        string _fromdate = txtFromDate.Text.Substring(0, 2) + "" + txtFromDate.Text.Substring(3, 2) + "" + txtFromDate.Text.Substring(6, 4);
        string _todate = txtToDate.Text.Substring(0, 2) + "" + txtToDate.Text.Substring(3, 2) + "" + txtToDate.Text.Substring(6, 4);

        SqlParameter P1 = new SqlParameter("@FROMDATE", SqlDbType.VarChar);
        P1.Value = txtFromDate.Text.Trim();

        SqlParameter P2 = new SqlParameter("@TODATE", SqlDbType.VarChar);
        P2.Value = txtToDate.Text.Trim();

        SqlParameter P3 = new SqlParameter("@BRANCHCODE", SqlDbType.VarChar);
        P3.Value = ddlBranch.SelectedValue.ToString();

        System.Data.DataTable dt = objData1.getData("TF_EXP_GBASE_FILE_GENERATION", P1, P2, P3);
        if (dt.Rows.Count > 0)
        {
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/GBASE/" + ddlBranch.SelectedItem.Text.Trim() + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string branchname = ddlBranch.SelectedItem.ToString().Trim();
            string brpref = "";
            if (branchname == "Mumbai")
            {
                brpref = "MB";
            }
            else if (branchname == "New Delhi")
            {
                brpref = "ND";
            }
            else if (branchname == "Chennai")
            {
                brpref = "CH";
            }
            else if (branchname == "Bangalore")
            {
                brpref = "BG";
            }

            string _filePath = _directoryPath + "/" + brpref + _fromdate + "TO" + _todate + ".xlsx";
            //string _filePath = _directoryPath + "/" + "GBaseDataFile" + _fromdate + "TO" + _todate + ".xlsx";


            if (dt.Rows.Count > 0)
            {

                using (XLWorkbook wb = new XLWorkbook())
                {
                    dt.Columns.Remove("Addeddate");

                    var sheet1 = wb.Worksheets.Add(dt, "Sheet1");
                    sheet1.Table("Table1").ShowAutoFilter = false;
                    sheet1.Table("Table1").Theme = XLTableTheme.None;

                    //wb.Worksheets.Add(dt, "Sheet1");

                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);

                        FileStream file = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
                        MyMemoryStream.WriteTo(file);
                        file.Close();
                        MyMemoryStream.Close();
                    }
                }

                TF_DATA objserverName = new TF_DATA();
                string _serverName = objserverName.GetServerName();
                string path = "file://" + _serverName + "/TF_GeneratedFiles/";
                string link = "/TF_GeneratedFiles/EXPORT/GBASE/";
                labelMessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";

                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('File Created Successfully on " + _serverName + " in " + path + "')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "Message", "alert('There is No records Between this Dates.')", true);
            txtFromDate.Text = "";

            txtFromDate.Focus();
        }
    }
    public void fillBranchsettlement()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", "");
        string _query = "TF_GetBranchDetails";
        System.Data.DataTable dt = objData.getData(_query, p1);
        ddlbranchsettlement.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlbranchsettlement.DataSource = dt.DefaultView;
            ddlbranchsettlement.DataTextField = "BranchName";
            ddlbranchsettlement.DataValueField = "BranchCode";

            ddlbranchsettlement.DataBind();
        }
        else
            li.Text = "No record(s) found";
        ddlbranchsettlement.Items.Insert(0, li);
    }
    protected void btnCreatesettlement_Click(object sender, EventArgs e)
    {
        getbranchdetails();
        GBaseFileCreation();
        GenraloperationGbaseFileCreation();
        InterOfficeGbaseFileCreation();
        string Script = hdnGBASE.Value + "\\n" + hdnGOGABSE.Value + "\\n" + hdnIOGBASE.Value;
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('"+ Script + "');", true);
    }

    //-------------------------------ADDED BY NILESH 11-09-2023------------------------------------    
    public void getbranchdetails()
    {
        TF_DATA objdata = new TF_DATA();
        string branchcode = ddlbranchsettlement.Text.Trim();
        string _qury = "getbranchdetails";
        SqlParameter p1 = new SqlParameter("@branchcode", branchcode);
        System.Data.DataTable dt = objdata.getData(_qury, p1);
        hdnauthorisedcode.Value = dt.Rows[0]["AuthorizedDealerCode"].ToString();
    }
    public void GBaseFileCreation()
    {
        hdnGBASE.Value = "";
        TF_DATA objData1 = new TF_DATA();
        SqlParameter Fromdate = new SqlParameter("@FROMDATE", txtfromdatesettlement.Text.Trim());
        SqlParameter TOdate = new SqlParameter("@TODATE", txttodatesettlement.Text.Trim());
        SqlParameter Branch = new SqlParameter("@Branch", ddlbranchsettlement.Text.Trim());
        System.Data.DataTable dt = objData1.getData("TF_EXP_RealisedGBaseFile", Fromdate, TOdate, Branch);
        string TodayDate = DateTime.Now.ToString("dd/MM/yyyy");
        string date = DateTime.Now.ToString("ddMMyyyy_hhmmss");
        string fromdate = txtfromdatesettlement.Text.Trim().Replace("/", "").Replace("/", "");
        string todate = txttodatesettlement.Text.Trim().Replace("/", "").Replace("/", "");
        string MTodayDate = "";
        if (TodayDate.Contains("/"))
        {
            MTodayDate = TodayDate.Replace("/", "");
        }
        if (TodayDate.Contains("-"))
        {
            MTodayDate = TodayDate.Replace("-", "");
        }
        if (dt.Rows.Count > 0)
        {
            string branchname = ddlbranchsettlement.SelectedItem.ToString().Trim();
            string brpref = "";
            if (branchname == "Mumbai")
            {
                brpref = "MB";
            }
            else if (branchname == "New Delhi")
            {
                brpref = "ND";
            }
            else if (branchname == "Chennai")
            {
                brpref = "CH";
            }
            else if (branchname == "Bangalore")
            {
                brpref = "BG";
            }
            string ADCODE = hdnauthorisedcode.Value;
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/" + ADCODE + "/Settlement/" + MTodayDate + "/GBASE/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string Filename = brpref + "_" + date + "_GBase" + ".xlsx";
            string _filePath = _directoryPath + "/" + brpref + "_" + date + "_GBase" + ".xlsx";
            if (dt.Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var sheet1 = wb.Worksheets.Add(dt, "GBase");
                    sheet1.Table("Table1").ShowAutoFilter = false;
                    sheet1.Table("Table1").Theme = XLTableTheme.None;
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        FileStream file = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
                        MyMemoryStream.WriteTo(file);
                        file.Close();
                        MyMemoryStream.Close();
                    }
                }
                TF_DATA objserverName = new TF_DATA();
                string _serverName = objserverName.GetServerName();
                string path = "file://" + _serverName + "/Mizuho_UAT/TF_GeneratedFiles/EXPORT/";
                string link = "/Mizuho_UAT/TF_GeneratedFiles/EXPORT/";
                labelmsgsettlement.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
                hdnGBASE.Value = "Files Created Successfully For GBASE";
                #region LOGCREATED
                

                foreach (DataRow dt1 in dt.Rows)
                {
                    SqlParameter SRNO = new SqlParameter("@SRNO", dt1["Sr No."].ToString().Trim());
                    SqlParameter FUNCTION = new SqlParameter("@FUNCTION",dt1["FUNCTION"].ToString().Trim());
                    SqlParameter OPERATION_KIND = new SqlParameter("@OPERATION_KIND",dt1["OPERATION KIND"].ToString().Trim());
                    SqlParameter DOC_NO = new SqlParameter("@DOC_NO",dt1["DOC NO"].ToString().Trim());
                    SqlParameter CURRENCY_ABBR = new SqlParameter("@CURRENCY_ABBR",dt1["CURRENCY ABBR"].ToString().Trim());
                    SqlParameter AMOUNT1 = new SqlParameter("@AMOUNT1",dt1["AMOUNT1"].ToString().Trim());
                    SqlParameter CUSTOMER = new SqlParameter("@CUSTOMER",dt1["CUSTOMER"].ToString().Trim());
                    SqlParameter COMMENT_CODE = new SqlParameter("@COMMENT_CODE",dt1["COMMENT CODE"].ToString().Trim());
                    SqlParameter MATURITY = new SqlParameter("@MATURITY",dt1["MATURITY"].ToString().Trim());
                    SqlParameter CURRENCY_ABBR2 = new SqlParameter("@CURRENCY_ABBR2",dt1["CURRENCY ABBR2"].ToString().Trim());
                    SqlParameter AMOUNT2 = new SqlParameter("@AMOUNT2",dt1["AMOUNT2"].ToString().Trim());
                    SqlParameter CREDITED_ON = new SqlParameter("@CREDITED_ON",dt1["CREDITED ON"].ToString().Trim());
                    SqlParameter AMOUNT3 = new SqlParameter("@AMOUNT3",dt1["AMOUNT3"].ToString().Trim());
                    SqlParameter INTEREST_PAYER = new SqlParameter("@INTEREST_PAYER",dt1["INTEREST PAYER"].ToString().Trim());
                    SqlParameter INTEREST_FROM = new SqlParameter("@INTEREST_FROM",dt1["INTEREST FROM"].ToString().Trim());
                    SqlParameter INTEREST_TO = new SqlParameter("@INTEREST_TO",dt1["INTEREST TO"].ToString().Trim());
                    SqlParameter NOS_OF_DAYS = new SqlParameter("@NOS_OF_DAYS",dt1["NOS. OF DAYS"].ToString().Trim());
                    SqlParameter RATE = new SqlParameter("@RATE",dt1["RATE"].ToString().Trim());
                    SqlParameter AMOUNT4 = new SqlParameter("@AMOUNT4",dt1["AMOUNT4"].ToString().Trim());
                    SqlParameter OVERDUE_INTEREST_RATE = new SqlParameter("@OVERDUE_INTEREST_RATE",dt1["OVERDUE INTEREST RATE"].ToString().Trim());
                    SqlParameter NOS_OF_DAYS1 = new SqlParameter("@NOS_OF_DAYS1",dt1["NOS. OF DAYS1"].ToString().Trim());
                    SqlParameter AMOUNT5 = new SqlParameter("@AMOUNT5",dt1["AMOUNT5"].ToString().Trim());
                    SqlParameter RECEIVED_INTEREST_AMOUNT_6 = new SqlParameter("@RECEIVED_INTEREST_AMOUNT_6",dt1["RECEIVED INTEREST AMOUNT 6"].ToString().Trim());
                    SqlParameter REMARKS = new SqlParameter("@REMARKS",dt1["REMARKS"].ToString().Trim());
                    SqlParameter ATTN = new SqlParameter("@ATTN",dt1["ATTN."].ToString().Trim());
                    // EXPORT ACCOUNTING 1
                    SqlParameter DOC_NO1 = new SqlParameter("@DOC_NO1",dt1["DOC NO1"].ToString().Trim());
                    SqlParameter AMOUNT7 = new SqlParameter("@AMOUNT7",dt1["AMOUNT7"].ToString().Trim());
                    SqlParameter DISCOUNT_MATURITY_Principal = new SqlParameter("@DISCOUNT_MATURITY_Principal",dt1["DISCOUNT/MATURITY Principal"].ToString().Trim());
                    SqlParameter LUMP_SUM_Principal = new SqlParameter("@LUMP_SUM_Principal",dt1["LUMP SUM Principal"].ToString().Trim());
                    SqlParameter CONTRACT_NO_Principal = new SqlParameter("@CONTRACT_NO_Principal",dt1["CONTRACT NO Principal"].ToString().Trim());
                    SqlParameter EXCH_CCY_Principal = new SqlParameter("@EXCH_CCY_Principal",dt1["EXCH. CCY Principal"].ToString().Trim());
                    SqlParameter EXCH_RATE_Principal = new SqlParameter("@EXCH_RATE_Principal",dt1["EXCH. RATE Principal"].ToString().Trim());
                    SqlParameter INTNL_EX_RATE_Principal = new SqlParameter("@INTNL_EX_RATE_Principal",dt1["INTNL EX.RATE Principal"].ToString().Trim());
                    SqlParameter DISCOUNT_MATURITY_Interest = new SqlParameter("@DISCOUNT_MATURITY_Interest",dt1["DISCOUNT/MATURITY Interest"].ToString().Trim());
                    SqlParameter LUMP_SUM_Interest = new SqlParameter("@LUMP_SUM_Interest",dt1["LUMP SUM Interest"].ToString().Trim());
                    SqlParameter CONTRACT_NO_Interest = new SqlParameter("@CONTRACT_NO_Interest",dt1["CONTRACT NO Interest"].ToString().Trim());
                    SqlParameter EXCH_CCY_Interest = new SqlParameter("@EXCH_CCY_Interest",dt1["EXCH. CCY Interest"].ToString().Trim());
                    SqlParameter EXCH_RATE_Interest = new SqlParameter("@EXCH_RATE_Interest",dt1["EXCH. RATE Interest"].ToString().Trim());
                    SqlParameter INTNL_EX_RATE_Interest = new SqlParameter("@INTNL_EX_RATE_Interest",dt1["INTNL EX.RATE Interest"].ToString().Trim());
                    SqlParameter DISCOUNT_MATURITY_Commission = new SqlParameter("@DISCOUNT_MATURITY_Commission",dt1["DISCOUNT/MATURITY Commission"].ToString().Trim());
                    SqlParameter LUMP_SUM_Commission = new SqlParameter("@LUMP_SUM_Commission",dt1["LUMP SUM Commission"].ToString().Trim());
                    SqlParameter CONTRACT_NO_Commission = new SqlParameter("@CONTRACT_NO_Commission",dt1["CONTRACT NO Commission"].ToString().Trim());
                    SqlParameter EXCH_CCY_Commission = new SqlParameter("@EXCH_CCY_Commission",dt1["EXCH. CCY Commission"].ToString().Trim());
                    SqlParameter EXCH_RATE_Commission = new SqlParameter("@EXCH_RATE_Commission",dt1["EXCH. RATE Commission"].ToString().Trim());
                    SqlParameter INTNL_EX_RATE_Commission = new SqlParameter("@INTNL_EX_RATE_Commission",dt1["INTNL EX.RATE Commission"].ToString().Trim());
                    SqlParameter DISCOUNT_MATURITY_Their_Commission = new SqlParameter("@DISCOUNT_MATURITY_Their_Commission",dt1["DISCOUNT/MATURITY Their_Commission"].ToString().Trim());
                    SqlParameter LUMP_SUM_Their_Commission = new SqlParameter("@LUMP_SUM_Their_Commission",dt1["LUMP SUM Their_Commission"].ToString().Trim());
                    SqlParameter CONTRACT_NO_Their_Commission = new SqlParameter("@CONTRACT_NO_Their_Commission",dt1["CONTRACT NO Their_Commission"].ToString().Trim());
                    SqlParameter EXCH_CCY_Their_Commission = new SqlParameter("@EXCH_CCY_Their_Commission",dt1["EXCH. CCY Their_Commission"].ToString().Trim());
                    SqlParameter EXCH_RATE_Their_Commission = new SqlParameter("@EXCH_RATE_Their_Commission",dt1["EXCH. RATE Their_Commission"].ToString().Trim());
                    SqlParameter INTNL_EX_RATE_Their_Commission = new SqlParameter("@INTNL_EX_RATE_Their_Commission",dt1["INTNL EX.RATE Their_Commission"].ToString().Trim());
                    SqlParameter CR_CODE = new SqlParameter("@CR_CODE",dt1["CR CODE"].ToString().Trim());
                    SqlParameter CR_CUST_ABBR = new SqlParameter("@CR_CUST_ABBR",dt1["CR CUST. ABBR."].ToString().Trim());
                    SqlParameter CR_AC_NUMBER = new SqlParameter("@CR_AC_NUMBER",dt1["CR A/C NUMBER"].ToString().Trim());
                    SqlParameter CR_Settlement_Curr = new SqlParameter("@CR_Settlement_Curr",dt1["CR Settlement Curr"].ToString().Trim());
                    SqlParameter CR_Settlement_AMOUNT = new SqlParameter("@CR_Settlement_AMOUNT",dt1["CR Settlement AMOUNT"].ToString().Trim());
                    SqlParameter CR_Settlement_PAYER = new SqlParameter("@CR_Settlement_PAYER",dt1["CR Settlement PAYER"].ToString().Trim());
                    SqlParameter CR_Interest_Curr = new SqlParameter("@CR_Interest_Curr",dt1["CR Interest Curr"].ToString().Trim());
                    SqlParameter CR_Interest_AMOUNT = new SqlParameter("@CR_Interest_AMOUNT",dt1["CR Interest AMOUNT"].ToString().Trim());
                    SqlParameter CR_Interest_PAYER = new SqlParameter("@CR_Interest_PAYER",dt1["CR Interest PAYER"].ToString().Trim());
                    SqlParameter CR_Acceptance_Comm_Curr = new SqlParameter("@CR_Acceptance_Comm_Curr",dt1["CR Acceptance Comm Curr"].ToString().Trim());
                    SqlParameter CR_Acceptance_Comm_AMOUNT = new SqlParameter("@CR_Acceptance_Comm_AMOUNT",dt1["CR Acceptance Comm AMOUNT"].ToString().Trim());
                    SqlParameter CR_Acceptance_Comm_PAYER = new SqlParameter("@CR_Acceptance_Comm_PAYER",dt1["CR Acceptance Comm PAYER"].ToString().Trim());
                    SqlParameter CR_Pay_Handle_Comm_Curr = new SqlParameter("@CR_Pay_Handle_Comm_Curr",dt1["CR Pay Handle Comm Curr"].ToString().Trim());
                    SqlParameter CR_Pay_Handle_Comm_AMOUNT = new SqlParameter("@CR_Pay_Handle_Comm_AMOUNT",dt1["CR Pay Handle Comm AMOUNT"].ToString().Trim());
                    SqlParameter CR_Pay_Handle_Comm_PAYER = new SqlParameter("@CR_Pay_Handle_Comm_PAYER",dt1["CR Pay Handle Comm PAYER"].ToString().Trim());
                    SqlParameter DR_CODE_1 = new SqlParameter("@DR_CODE_1",dt1["DR CODE 1"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_1 = new SqlParameter("@DR_CUST_ABBR_1",dt1["DR CUST. ABBR. 1"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_1 = new SqlParameter("@DR_AC_NUMBER_1",dt1["DR A/C NUMBER 1"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_1 = new SqlParameter("@DR_Current_Acc_Curr_1",dt1["DR_Current_Acc_Curr 1"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_1 = new SqlParameter("@DR_Current_Acc_Amount_1",dt1["DR_Current_Acc_Amount 1"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_1 = new SqlParameter("@DR_Current_Acc_PAYER_1",dt1["DR_Current_Acc PAYER 1"].ToString().Trim());
                    SqlParameter DR_CODE_2 = new SqlParameter("@DR_CODE_2",dt1["DR CODE 2"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_2 = new SqlParameter("@DR_CUST_ABBR_2",dt1["DR CUST. ABBR. 2"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_2 = new SqlParameter("@DR_AC_NUMBER_2",dt1["DR A/C NUMBER 2"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_2 = new SqlParameter("@DR_Current_Acc_Curr_2",dt1["DR_Current_Acc_Curr 2"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_2 = new SqlParameter("@DR_Current_Acc_Amount_2",dt1["DR_Current_Acc_Amount 2"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_2 = new SqlParameter("@DR_Current_Acc_PAYER_2",dt1["DR_Current_Acc PAYER 2"].ToString().Trim());
                    SqlParameter DR_CODE_3 = new SqlParameter("@DR_CODE_3",dt1["DR CODE 3"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_3 = new SqlParameter("@DR_CUST_ABBR_3",dt1["DR CUST. ABBR. 3"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_3 = new SqlParameter("@DR_AC_NUMBER_3",dt1["DR A/C NUMBER 3"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_3 = new SqlParameter("@DR_Current_Acc_Curr_3",dt1["DR_Current_Acc_Curr 3"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_3 = new SqlParameter("@DR_Current_Acc_Amount_3",dt1["DR_Current_Acc_Amount 3"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_3 = new SqlParameter("@DR_Current_Acc_PAYER_3",dt1["DR_Current_Acc PAYER 3"].ToString().Trim());
                    SqlParameter DR_CODE_4 = new SqlParameter("@DR_CODE_4",dt1["DR CODE 4"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_4 = new SqlParameter("@DR_CUST_ABBR_4",dt1["DR CUST. ABBR. 4"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_4 = new SqlParameter("@DR_AC_NUMBER_4",dt1["DR A/C NUMBER 4"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_4 = new SqlParameter("@DR_Current_Acc_Curr_4",dt1["DR_Current_Acc_Curr 4"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_4 = new SqlParameter("@DR_Current_Acc_Amount_4",dt1["DR_Current_Acc_Amount 4"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_4 = new SqlParameter("@DR_Current_Acc_PAYER_4",dt1["DR_Current_Acc PAYER 4"].ToString().Trim());
                    SqlParameter DR_CODE_5 = new SqlParameter("@DR_CODE_5",dt1["DR CODE 5"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_5 = new SqlParameter("@DR_CUST_ABBR_5",dt1["DR CUST. ABBR. 5"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_5 = new SqlParameter("@DR_AC_NUMBER_5",dt1["DR A/C NUMBER 5"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_5 = new SqlParameter("@DR_Current_Acc_Curr_5",dt1["DR_Current_Acc_Curr 5"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_5 = new SqlParameter("@DR_Current_Acc_Amount_5",dt1["DR_Current_Acc_Amount 5"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_5 = new SqlParameter("@DR_Current_Acc_PAYER_5",dt1["DR_Current_Acc PAYER 5"].ToString().Trim());
                    SqlParameter DR_CODE_6 = new SqlParameter("@DR_CODE_6",dt1["DR CODE 6"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_6 = new SqlParameter("@DR_CUST_ABBR_6",dt1["DR CUST. ABBR. 6"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_6 = new SqlParameter("@DR_AC_NUMBER_6",dt1["DR A/C NUMBER 6"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_6 = new SqlParameter("@DR_Current_Acc_Curr_6",dt1["DR_Current_Acc_Curr 6"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_6 = new SqlParameter("@DR_Current_Acc_Amount_6",dt1["DR_Current_Acc_Amount 6"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_6 = new SqlParameter("@DR_Current_Acc_PAYER_6",dt1["DR_Current_Acc PAYER 6"].ToString().Trim());
                    // EXPORT ACCOUNTING 2
                    SqlParameter DOC_NO2 = new SqlParameter("@DOC_NO2",dt1["DOC NO2"].ToString().Trim());
                    SqlParameter AMOUNT8 = new SqlParameter("@AMOUNT8",dt1["AMOUNT8"].ToString().Trim());
                    SqlParameter DISCOUNT_MATURITY_Principal1 = new SqlParameter("@DISCOUNT_MATURITY_Principal1",dt1["DISCOUNT/MATURITY Principal1"].ToString().Trim());
                    SqlParameter LUMP_SUM_Principal1 = new SqlParameter("@LUMP_SUM_Principal1",dt1["LUMP SUM Principal1"].ToString().Trim());
                    SqlParameter CONTRACT_NO_Principal1 = new SqlParameter("@CONTRACT_NO_Principal1",dt1["CONTRACT NO Principal1"].ToString().Trim());
                    SqlParameter EXCH_CCY_Principal1 = new SqlParameter("@EXCH_CCY_Principal1",dt1["EXCH. CCY Principal1"].ToString().Trim());
                    SqlParameter EXCH_RATE_Principal1 = new SqlParameter("@EXCH_RATE_Principal1",dt1["EXCH. RATE Principal1"].ToString().Trim());
                    SqlParameter INTNL_EX_RATE_Principal1 = new SqlParameter("@INTNL_EX_RATE_Principal1",dt1["INTNL EX.RATE Principal1"].ToString().Trim());
                    SqlParameter DISCOUNT_MATURITY_Interest1 = new SqlParameter("@DISCOUNT_MATURITY_Interest1",dt1["DISCOUNT/MATURITY Interest1"].ToString().Trim());
                    SqlParameter LUMP_SUM_Interest1 = new SqlParameter("@LUMP_SUM_Interest1",dt1["LUMP SUM Interest1"].ToString().Trim());
                    SqlParameter CONTRACT_NO_Interest1 = new SqlParameter("@CONTRACT_NO_Interest1",dt1["CONTRACT NO Interest1"].ToString().Trim());
                    SqlParameter EXCH_CCY_Interest1 = new SqlParameter("@EXCH_CCY_Interest1",dt1["EXCH. CCY Interest11"].ToString().Trim());
                    SqlParameter EXCH_RATE_Interest1 = new SqlParameter("@EXCH_RATE_Interest1",dt1["EXCH. RATE Interest11"].ToString().Trim());
                    SqlParameter INTNL_EX_RATE_Interest1 = new SqlParameter("@INTNL_EX_RATE_Interest1",dt1["INTNL EX.RATE Interest1"].ToString().Trim());
                    SqlParameter DISCOUNT_MATURITY_Commission1 = new SqlParameter("@DISCOUNT_MATURITY_Commission1",dt1["DISCOUNT/MATURITY Commission1"].ToString().Trim());
                    SqlParameter LUMP_SUM_Commission1 = new SqlParameter("@LUMP_SUM_Commission1",dt1["LUMP SUM Commission1"].ToString().Trim());
                    SqlParameter CONTRACT_NO_Commission1 = new SqlParameter("@CONTRACT_NO_Commission1",dt1["CONTRACT NO Commission1"].ToString().Trim());
                    SqlParameter EXCH_CCY_Commission1 = new SqlParameter("@EXCH_CCY_Commission1",dt1["EXCH. CCY Commission1"].ToString().Trim());
                    SqlParameter EXCH_RATE_Commission1 = new SqlParameter("@EXCH_RATE_Commission1",dt1["EXCH. RATE Commission1"].ToString().Trim());
                    SqlParameter INTNL_EX_RATE_Commission1 = new SqlParameter("@INTNL_EX_RATE_Commission1",dt1["INTNL EX.RATE Commission1"].ToString().Trim());
                    SqlParameter DISCOUNT_MATURITY_Their_Commission1 = new SqlParameter("@DISCOUNT_MATURITY_Their_Commission1",dt1["DISCOUNT/MATURITY Their_Commission1"].ToString().Trim());
                    SqlParameter LUMP_SUM_Their_Commission1 = new SqlParameter("@LUMP_SUM_Their_Commission1",dt1["LUMP SUM Their_Commission1"].ToString().Trim());
                    SqlParameter CONTRACT_NO_Their_Commission1 = new SqlParameter("@CONTRACT_NO_Their_Commission1",dt1["CONTRACT NO Their_Commission1"].ToString().Trim());
                    SqlParameter EXCH_CCY_Their_Commission1 = new SqlParameter("@EXCH_CCY_Their_Commission1",dt1["EXCH. CCY Their_Commission1"].ToString().Trim());
                    SqlParameter EXCH_RATE_Their_Commission1 = new SqlParameter("@EXCH_RATE_Their_Commission1",dt1["EXCH. RATE Their_Commission1"].ToString().Trim());
                    SqlParameter INTNL_EX_RATE_Their_Commission1 = new SqlParameter("@INTNL_EX_RATE_Their_Commission1",dt1["INTNL EX.RATE Their_Commission1"].ToString().Trim());
                    SqlParameter CR_CODE1 = new SqlParameter("@CR_CODE1",dt1["CR CODE1"].ToString().Trim());
                    SqlParameter CR_CUST_ABBR1 = new SqlParameter("@CR_CUST_ABBR1",dt1["CR CUST. ABBR.1"].ToString().Trim());
                    SqlParameter CR_AC_NUMBER1 = new SqlParameter("@CR_AC_NUMBER1",dt1["CR A/C NUMBER1"].ToString().Trim());
                    SqlParameter CR_Settlement_Curr1 = new SqlParameter("@CR_Settlement_Curr1",dt1["CR Settlement Curr1"].ToString().Trim());
                    SqlParameter CR_Settlement_AMOUNT1 = new SqlParameter("@CR_Settlement_AMOUNT1",dt1["CR Settlement AMOUNT1"].ToString().Trim());
                    SqlParameter CR_Settlement_PAYER1 = new SqlParameter("@CR_Settlement_PAYER1",dt1["CR Settlement PAYER1"].ToString().Trim());
                    SqlParameter CR_Interest_Curr1 = new SqlParameter("@CR_Interest_Curr1",dt1["CR Interest Curr1"].ToString().Trim());
                    SqlParameter CR_Interest_AMOUNT1 = new SqlParameter("@CR_Interest_AMOUNT1",dt1["CR Interest AMOUNT1"].ToString().Trim());
                    SqlParameter CR_Interest_PAYER1 = new SqlParameter("@CR_Interest_PAYER1",dt1["CR Interest PAYER1"].ToString().Trim());
                    SqlParameter CR_Acceptance_Comm_Curr1 = new SqlParameter("@CR_Acceptance_Comm_Curr1",dt1["CR Acceptance Comm Curr1"].ToString().Trim());
                    SqlParameter CR_Acceptance_Comm_AMOUNT1 = new SqlParameter("@CR_Acceptance_Comm_AMOUNT1",dt1["CR Acceptance Comm AMOUNT1"].ToString().Trim());
                    SqlParameter CR_Acceptance_Comm_PAYER1 = new SqlParameter("@CR_Acceptance_Comm_PAYER1",dt1["CR Acceptance Comm PAYER1"].ToString().Trim());
                    SqlParameter CR_Pay_Handle_Comm_Curr1 = new SqlParameter("@CR_Pay_Handle_Comm_Curr1",dt1["CR Pay Handle Comm Curr1"].ToString().Trim());
                    SqlParameter CR_Pay_Handle_Comm_AMOUNT1 = new SqlParameter("@CR_Pay_Handle_Comm_AMOUNT1",dt1["CR Pay Handle Comm AMOUNT1"].ToString().Trim());
                    SqlParameter CR_Pay_Handle_Comm_PAYER1 = new SqlParameter("@CR_Pay_Handle_Comm_PAYER1",dt1["CR Pay Handle Comm PAYER1"].ToString().Trim());
                    SqlParameter DR_CODE_11 = new SqlParameter("@DR_CODE_11",dt1["DR CODE 11"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_11 = new SqlParameter("@DR_CUST_ABBR_11",dt1["DR CUST. ABBR. 11"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_11 = new SqlParameter("@DR_AC_NUMBER_11",dt1["DR A/C NUMBER 11"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_11 = new SqlParameter("@DR_Current_Acc_Curr_11",dt1["DR_Current_Acc_Curr 11"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_11 = new SqlParameter("@DR_Current_Acc_Amount_11",dt1["DR_Current_Acc_Amount 11"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_11 = new SqlParameter("@DR_Current_Acc_PAYER_11",dt1["DR_Current_Acc PAYER 11"].ToString().Trim());
                    SqlParameter DR_CODE_21 = new SqlParameter("@DR_CODE_21",dt1["DR CODE 21"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_21 = new SqlParameter("@DR_CUST_ABBR_21",dt1["DR CUST. ABBR. 21"].ToString().Trim());
                    // SqlParameter DR_AC_NUMBER_21 = new SqlParameter("@DR_AC_NUMBER_21",dt1["DR A/C NUMBER 21"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_21 = new SqlParameter("@DR_Current_Acc_Curr_21",dt1["DR_Current_Acc_Curr 21"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_21 = new SqlParameter("@DR_Current_Acc_Amount_21",dt1["DR_Current_Acc_Amount 21"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_21 = new SqlParameter("@DR_Current_Acc_PAYER_21",dt1["DR_Current_Acc PAYER 21"].ToString().Trim());
                    SqlParameter DR_CODE_31 = new SqlParameter("@DR_CODE_31",dt1["DR CODE 31"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_31 = new SqlParameter("@DR_CUST_ABBR_31",dt1["DR CUST. ABBR. 31"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_31 = new SqlParameter("@DR_AC_NUMBER_31",dt1["DR A/C NUMBER 31"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_31 = new SqlParameter("@DR_Current_Acc_Curr_31",dt1["DR_Current_Acc_Curr 31"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_31 = new SqlParameter("@DR_Current_Acc_Amount_31",dt1["DR_Current_Acc_Amount 31"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_31 = new SqlParameter("@DR_Current_Acc_PAYER_31",dt1["DR_Current_Acc PAYER 31"].ToString().Trim());
                    SqlParameter DR_CODE_41 = new SqlParameter("@DR_CODE_41",dt1["DR CODE 41"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_41 = new SqlParameter("@DR_CUST_ABBR_41",dt1["DR CUST. ABBR. 41"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_41 = new SqlParameter("@DR_AC_NUMBER_41",dt1["DR A/C NUMBER 41"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_41 = new SqlParameter("@DR_Current_Acc_Curr_41",dt1["DR_Current_Acc_Curr 41"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_41 = new SqlParameter("@DR_Current_Acc_Amount_41",dt1["DR_Current_Acc_Amount 41"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_41 = new SqlParameter("@DR_Current_Acc_PAYER_41",dt1["DR_Current_Acc PAYER 41"].ToString().Trim());
                    SqlParameter DR_CODE_51 = new SqlParameter("@DR_CODE_51",dt1["DR CODE 51"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_51 = new SqlParameter("@DR_CUST_ABBR_51",dt1["DR CUST. ABBR. 51"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_51 = new SqlParameter("@DR_AC_NUMBER_51",dt1["DR A/C NUMBER 51"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_51 = new SqlParameter("@DR_Current_Acc_Curr_51",dt1["DR_Current_Acc_Curr 51"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_51 = new SqlParameter("@DR_Current_Acc_Amount_51",dt1["DR_Current_Acc_Amount 51"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_51 = new SqlParameter("@DR_Current_Acc_PAYER_51",dt1["DR_Current_Acc PAYER 51"].ToString().Trim());
                    SqlParameter DR_CODE_61 = new SqlParameter("@DR_CODE_61",dt1["DR CODE 61"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_61 = new SqlParameter("@DR_CUST_ABBR_61",dt1["DR CUST. ABBR. 61"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_61 = new SqlParameter("@DR_AC_NUMBER_61",dt1["DR A/C NUMBER 61"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_61 = new SqlParameter("@DR_Current_Acc_Curr_61",dt1["DR_Current_Acc_Curr 61"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_61 = new SqlParameter("@DR_Current_Acc_Amount_61",dt1["DR_Current_Acc_Amount 61"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_61 = new SqlParameter("@DR_Current_Acc_PAYER_61",dt1["DR_Current_Acc PAYER 61"].ToString().Trim());

                    // EXPORT ACCOUNTING 3
                    SqlParameter DOC_NO3 = new SqlParameter("@DOC_NO3",dt1["DOC NO3"].ToString().Trim());
                    SqlParameter AMOUNT9 = new SqlParameter("@AMOUNT9",dt1["AMOUNT9"].ToString().Trim());
                    SqlParameter DISCOUNT_MATURITY_Principal2 = new SqlParameter("@DISCOUNT_MATURITY_Principal2",dt1["DISCOUNT/MATURITY Principal2"].ToString().Trim());
                    SqlParameter LUMP_SUM_Principal2 = new SqlParameter("@LUMP_SUM_Principal2",dt1["LUMP SUM Principal2"].ToString().Trim());
                    SqlParameter CONTRACT_NO_Principal2 = new SqlParameter("@CONTRACT_NO_Principal2",dt1["CONTRACT NO Principal2"].ToString().Trim());
                    SqlParameter EXCH_CCY_Principal2 = new SqlParameter("@EXCH_CCY_Principal2",dt1["EXCH. CCY Principal2"].ToString().Trim());
                    SqlParameter EXCH_RATE_Principal2 = new SqlParameter("@EXCH_RATE_Principal2",dt1["EXCH. RATE Principal2"].ToString().Trim());
                    SqlParameter INTNL_EX_RATE_Principal2 = new SqlParameter("@INTNL_EX_RATE_Principal2",dt1["INTNL EX.RATE Principal2"].ToString().Trim());
                    SqlParameter DISCOUNT_MATURITY_Interest2 = new SqlParameter("@DISCOUNT_MATURITY_Interest2",dt1["DISCOUNT/MATURITY Interest2"].ToString().Trim());
                    SqlParameter LUMP_SUM_Interest2 = new SqlParameter("@LUMP_SUM_Interest2",dt1["LUMP SUM Interest2"].ToString().Trim());
                    SqlParameter CONTRACT_NO_Interest2 = new SqlParameter("@CONTRACT_NO_Interest2",dt1["CONTRACT NO Interest2"].ToString().Trim());
                    SqlParameter EXCH_CCY_Interest2 = new SqlParameter("@EXCH_CCY_Interest2",dt1["EXCH. CCY Interest2"].ToString().Trim());
                    SqlParameter EXCH_RATE_Interest2 = new SqlParameter("@EXCH_RATE_Interest2",dt1["EXCH. RATE Interest2"].ToString().Trim());
                    SqlParameter INTNL_EX_RATE_Interest2 = new SqlParameter("@INTNL_EX_RATE_Interest2",dt1["INTNL EX.RATE Interest2"].ToString().Trim());
                    SqlParameter DISCOUNT_MATURITY_Commission2 = new SqlParameter("@DISCOUNT_MATURITY_Commission2",dt1["DISCOUNT/MATURITY Commission2"].ToString().Trim());
                    SqlParameter LUMP_SUM_Commission2 = new SqlParameter("@LUMP_SUM_Commission2",dt1["LUMP SUM Commission2"].ToString().Trim());
                    SqlParameter CONTRACT_NO_Commission2 = new SqlParameter("@CONTRACT_NO_Commission2",dt1["CONTRACT NO Commission2"].ToString().Trim());
                    SqlParameter EXCH_CCY_Commission2 = new SqlParameter("@EXCH_CCY_Commission2",dt1["EXCH. CCY Commission2"].ToString().Trim());
                    SqlParameter EXCH_RATE_Commission2 = new SqlParameter("@EXCH_RATE_Commission2",dt1["EXCH. RATE Commission2"].ToString().Trim());
                    SqlParameter INTNL_EX_RATE_Commission2 = new SqlParameter("@INTNL_EX_RATE_Commission2",dt1["INTNL EX.RATE Commission2"].ToString().Trim());
                    SqlParameter DISCOUNT_MATURITY_Their_Commission2 = new SqlParameter("@DISCOUNT_MATURITY_Their_Commission2",dt1["DISCOUNT/MATURITY Their_Commission2"].ToString().Trim());
                    SqlParameter LUMP_SUM_Their_Commission2 = new SqlParameter("@LUMP_SUM_Their_Commission2",dt1["LUMP SUM Their_Commission2"].ToString().Trim());
                    SqlParameter CONTRACT_NO_Their_Commission2 = new SqlParameter("@CONTRACT_NO_Their_Commission2",dt1["CONTRACT NO Their_Commission2"].ToString().Trim());
                    SqlParameter EXCH_CCY_Their_Commission2 = new SqlParameter("@EXCH_CCY_Their_Commission2",dt1["EXCH. CCY Their_Commission2"].ToString().Trim());
                    SqlParameter EXCH_RATE_Their_Commission2 = new SqlParameter("@EXCH_RATE_Their_Commission2",dt1["EXCH. RATE Their_Commission2"].ToString().Trim());
                    SqlParameter INTNL_EX_RATE_Their_Commission2 = new SqlParameter("@INTNL_EX_RATE_Their_Commission2",dt1["INTNL EX.RATE Their_Commission2"].ToString().Trim());
                    SqlParameter CR_CODE11 = new SqlParameter("@CR_CODE11",dt1["CR CODE11"].ToString().Trim());
                    SqlParameter CR_CUST_ABBR11 = new SqlParameter("@CR_CUST_ABBR11",dt1["CR CUST. ABBR.11"].ToString().Trim());
                    SqlParameter CR_AC_NUMBER11 = new SqlParameter("@CR_AC_NUMBER11",dt1["CR A/C NUMBER11"].ToString().Trim());
                    SqlParameter CR_Settlement_Curr11 = new SqlParameter("@CR_Settlement_Curr11",dt1["CR Settlement Curr11"].ToString().Trim());
                    SqlParameter CR_Settlement_AMOUNT11 = new SqlParameter("@CR_Settlement_AMOUNT11",dt1["CR Settlement AMOUNT11"].ToString().Trim());
                    SqlParameter CR_Settlement_PAYER11 = new SqlParameter("@CR_Settlement_PAYER11",dt1["CR Settlement PAYER11"].ToString().Trim());
                    SqlParameter CR_Interest_Curr11 = new SqlParameter("@CR_Interest_Curr11",dt1["CR Interest Curr11"].ToString().Trim());
                    SqlParameter CR_Interest_AMOUNT11 = new SqlParameter("@CR_Interest_AMOUNT11",dt1["CR Interest AMOUNT11"].ToString().Trim());
                    SqlParameter CR_Interest_PAYER11 = new SqlParameter("@CR_Interest_PAYER11",dt1["CR Interest PAYER11"].ToString().Trim());
                    SqlParameter CR_Acceptance_Comm_Curr11 = new SqlParameter("@CR_Acceptance_Comm_Curr11",dt1["CR Acceptance Comm Curr11"].ToString().Trim());
                    SqlParameter CR_Acceptance_Comm_AMOUNT11 = new SqlParameter("@CR_Acceptance_Comm_AMOUNT11",dt1["CR Acceptance Comm AMOUNT11"].ToString().Trim());
                    SqlParameter CR_Acceptance_Comm_PAYER11 = new SqlParameter("@CR_Acceptance_Comm_PAYER11",dt1["CR Acceptance Comm PAYER11"].ToString().Trim());
                    SqlParameter CR_Pay_Handle_Comm_Curr11 = new SqlParameter("@CR_Pay_Handle_Comm_Curr11",dt1["CR Pay Handle Comm Curr11"].ToString().Trim());
                    SqlParameter CR_Pay_Handle_Comm_AMOUNT11 = new SqlParameter("@CR_Pay_Handle_Comm_AMOUNT11",dt1["CR Pay Handle Comm AMOUNT11"].ToString().Trim());
                    SqlParameter CR_Pay_Handle_Comm_PAYER11 = new SqlParameter("@CR_Pay_Handle_Comm_PAYER11",dt1["CR Pay Handle Comm PAYER11"].ToString().Trim());
                    SqlParameter DR_CODE_111 = new SqlParameter("@DR_CODE_111",dt1["DR CODE 111"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_111 = new SqlParameter("@DR_CUST_ABBR_111",dt1["DR CUST. ABBR. 111"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_111 = new SqlParameter("@DR_AC_NUMBER_111",dt1["DR A/C NUMBER 111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_111 = new SqlParameter("@DR_Current_Acc_Curr_111",dt1["DR_Current_Acc_Curr 111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_111 = new SqlParameter("@DR_Current_Acc_Amount_111",dt1["DR_Current_Acc_Amount 111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_111 = new SqlParameter("@DR_Current_Acc_PAYER_111",dt1["DR_Current_Acc PAYER 111"].ToString().Trim());
                    SqlParameter DR_CODE_211 = new SqlParameter("@DR_CODE_211",dt1["DR CODE 211"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_211 = new SqlParameter("@DR_CUST_ABBR_211",dt1["DR CUST. ABBR. 211"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_211 = new SqlParameter("@DR_AC_NUMBER_211",dt1["DR A/C NUMBER 211"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_211 = new SqlParameter("@DR_Current_Acc_Curr_211",dt1["DR_Current_Acc_Curr 211"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_211 = new SqlParameter("@DR_Current_Acc_Amount_211",dt1["DR_Current_Acc_Amount 211"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_211 = new SqlParameter("@DR_Current_Acc_PAYER_211",dt1["DR_Current_Acc PAYER 211"].ToString().Trim());
                    SqlParameter DR_CODE_311 = new SqlParameter("@DR_CODE_311",dt1["DR CODE 311"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_311 = new SqlParameter("@DR_CUST_ABBR_311",dt1["DR CUST. ABBR. 311"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_311 = new SqlParameter("@DR_AC_NUMBER_311",dt1["DR A/C NUMBER 311"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_311 = new SqlParameter("@DR_Current_Acc_Curr_311",dt1["DR_Current_Acc_Curr 311"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_311 = new SqlParameter("@DR_Current_Acc_Amount_311",dt1["DR_Current_Acc_Amount 311"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_311 = new SqlParameter("@DR_Current_Acc_PAYER_311",dt1["DR_Current_Acc PAYER 311"].ToString().Trim());
                    SqlParameter DR_CODE_411 = new SqlParameter("@DR_CODE_411",dt1["DR CODE 411"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_411 = new SqlParameter("@DR_CUST_ABBR_411",dt1["DR CUST. ABBR. 411"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_411 = new SqlParameter("@DR_AC_NUMBER_411",dt1["DR A/C NUMBER 411"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_411 = new SqlParameter("@DR_Current_Acc_Curr_411",dt1["DR_Current_Acc_Curr 411"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_411 = new SqlParameter("@DR_Current_Acc_Amount_411",dt1["DR_Current_Acc_Amount 411"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_411 = new SqlParameter("@DR_Current_Acc_PAYER_411",dt1["DR_Current_Acc PAYER 411"].ToString().Trim());
                    SqlParameter DR_CODE_511 = new SqlParameter("@DR_CODE_511",dt1["DR CODE 511"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_511 = new SqlParameter("@DR_CUST_ABBR_511",dt1["DR CUST. ABBR. 511"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_511 = new SqlParameter("@DR_AC_NUMBER_511",dt1["DR A/C NUMBER 511"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_511 = new SqlParameter("@DR_Current_Acc_Curr_511",dt1["DR_Current_Acc_Curr 511"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_511 = new SqlParameter("@DR_Current_Acc_Amount_511",dt1["DR_Current_Acc_Amount 511"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_511 = new SqlParameter("@DR_Current_Acc_PAYER_511",dt1["DR_Current_Acc PAYER 511"].ToString().Trim());
                    SqlParameter DR_CODE_611 = new SqlParameter("@DR_CODE_611",dt1["DR CODE 611"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_611 = new SqlParameter("@DR_CUST_ABBR_611",dt1["DR CUST. ABBR. 611"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_611 = new SqlParameter("@DR_AC_NUMBER_611",dt1["DR A/C NUMBER 611"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_611 = new SqlParameter("@DR_Current_Acc_Curr_611",dt1["DR_Current_Acc_Curr 611"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_611 = new SqlParameter("@DR_Current_Acc_Amount_611",dt1["DR_Current_Acc_Amount 611"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_611 = new SqlParameter("@DR_Current_Acc_PAYER_611",dt1["DR_Current_Acc PAYER 611"].ToString().Trim());

                    // EXPORT ACCOUNTING 4
                    SqlParameter DOC_NO4 = new SqlParameter("@DOC_NO4",dt1["DOC NO4"].ToString().Trim());
                    SqlParameter AMOUNT10 = new SqlParameter("@AMOUNT10",dt1["AMOUNT10"].ToString().Trim());
                    SqlParameter DISCOUNT_MATURITY_Principal3 = new SqlParameter("@DISCOUNT_MATURITY_Principal3",dt1["DISCOUNT/MATURITY Principal3"].ToString().Trim());
                    SqlParameter LUMP_SUM_Principal3 = new SqlParameter("@LUMP_SUM_Principal3",dt1["LUMP SUM Principal3"].ToString().Trim());
                    SqlParameter CONTRACT_NO_Principal3 = new SqlParameter("@CONTRACT_NO_Principal3",dt1["CONTRACT NO Principal3"].ToString().Trim());
                    SqlParameter EXCH_CCY_Principal3 = new SqlParameter("@EXCH_CCY_Principal3",dt1["EXCH. CCY Principal3"].ToString().Trim());
                    SqlParameter EXCH_RATE_Principal3 = new SqlParameter("@EXCH_RATE_Principal3",dt1["EXCH. RATE Principal3"].ToString().Trim());
                    SqlParameter INTNL_EX_RATE_Principal3 = new SqlParameter("@INTNL_EX_RATE_Principal3",dt1["INTNL EX.RATE Principal3"].ToString().Trim());
                    SqlParameter DISCOUNT_MATURITY_Interest3 = new SqlParameter("@DISCOUNT_MATURITY_Interest3",dt1["DISCOUNT/MATURITY Interest3"].ToString().Trim());
                    SqlParameter LUMP_SUM_Interest3 = new SqlParameter("@LUMP_SUM_Interest3",dt1["LUMP SUM Interest3"].ToString().Trim());
                    SqlParameter CONTRACT_NO_Interest3 = new SqlParameter("@CONTRACT_NO_Interest3",dt1["CONTRACT NO Interest3"].ToString().Trim());
                    SqlParameter EXCH_CCY_Interest3 = new SqlParameter("@EXCH_CCY_Interest3",dt1["EXCH. CCY Interest3"].ToString().Trim());
                    SqlParameter EXCH_RATE_Interest3 = new SqlParameter("@EXCH_RATE_Interest3",dt1["EXCH. RATE Interest3"].ToString().Trim());
                    SqlParameter INTNL_EX_RATE_Interest3 = new SqlParameter("@INTNL_EX_RATE_Interest3",dt1["INTNL EX.RATE Interest3"].ToString().Trim());
                    SqlParameter DISCOUNT_MATURITY_Commission3 = new SqlParameter("@DISCOUNT_MATURITY_Commission3",dt1["DISCOUNT/MATURITY Commission3"].ToString().Trim());
                    SqlParameter LUMP_SUM_Commission3 = new SqlParameter("@LUMP_SUM_Commission3",dt1["LUMP SUM Commission3"].ToString().Trim());
                    SqlParameter CONTRACT_NO_Commission3 = new SqlParameter("@CONTRACT_NO_Commission3",dt1["CONTRACT NO Commission3"].ToString().Trim());
                    SqlParameter EXCH_CCY_Commission3 = new SqlParameter("@EXCH_CCY_Commission3",dt1["EXCH. CCY Commission3"].ToString().Trim());
                    SqlParameter EXCH_RATE_Commission3 = new SqlParameter("@EXCH_RATE_Commission3",dt1["EXCH. RATE Commission3"].ToString().Trim());
                    SqlParameter INTNL_EX_RATE_Commission3 = new SqlParameter("@INTNL_EX_RATE_Commission3",dt1["INTNL EX.RATE Commission3"].ToString().Trim());
                    SqlParameter DISCOUNT_MATURITY_Their_Commission3 = new SqlParameter("@DISCOUNT_MATURITY_Their_Commission3",dt1["DISCOUNT/MATURITY Their_Commission3"].ToString().Trim());
                    SqlParameter LUMP_SUM_Their_Commission3 = new SqlParameter("@LUMP_SUM_Their_Commission3",dt1["LUMP SUM Their_Commission3"].ToString().Trim());
                    SqlParameter CONTRACT_NO_Their_Commission3 = new SqlParameter("@CONTRACT_NO_Their_Commission3",dt1["CONTRACT NO Their_Commission3"].ToString().Trim());
                    SqlParameter EXCH_CCY_Their_Commission3 = new SqlParameter("@EXCH_CCY_Their_Commission3",dt1["EXCH. CCY Their_Commission3"].ToString().Trim());
                    SqlParameter EXCH_RATE_Their_Commission3 = new SqlParameter("@EXCH_RATE_Their_Commission3",dt1["EXCH. RATE Their_Commission3"].ToString().Trim());
                    SqlParameter INTNL_EX_RATE_Their_Commission3 = new SqlParameter("@INTNL_EX_RATE_Their_Commission3",dt1["INTNL EX.RATE Their_Commission3"].ToString().Trim());
                    SqlParameter CR_CODE111 = new SqlParameter("@CR_CODE111",dt1["CR CODE111"].ToString().Trim());
                    SqlParameter CR_CUST_ABBR111 = new SqlParameter("@CR_CUST_ABBR111",dt1["CR CUST. ABBR.111"].ToString().Trim());
                    SqlParameter CR_AC_NUMBER111 = new SqlParameter("@CR_AC_NUMBER111",dt1["CR A/C NUMBER111"].ToString().Trim());
                    SqlParameter CR_Settlement_Curr111 = new SqlParameter("@CR_Settlement_Curr111",dt1["CR Settlement Curr111"].ToString().Trim());
                    SqlParameter CR_Settlement_AMOUNT111 = new SqlParameter("@CR_Settlement_AMOUNT111",dt1["CR Settlement AMOUNT111"].ToString().Trim());
                    SqlParameter CR_Settlement_PAYER111 = new SqlParameter("@CR_Settlement_PAYER111",dt1["CR Settlement PAYER111"].ToString().Trim());
                    SqlParameter CR_Interest_Curr111 = new SqlParameter("@CR_Interest_Curr111",dt1["CR Interest Curr111"].ToString().Trim());
                    SqlParameter CR_Interest_AMOUNT111 = new SqlParameter("@CR_Interest_AMOUNT111",dt1["CR Interest AMOUNT111"].ToString().Trim());
                    SqlParameter CR_Interest_PAYER111 = new SqlParameter("@CR_Interest_PAYER111",dt1["CR Interest PAYER111"].ToString().Trim());
                    SqlParameter CR_Acceptance_Comm_Curr111 = new SqlParameter("@CR_Acceptance_Comm_Curr111",dt1["CR Acceptance Comm Curr111"].ToString().Trim());
                    SqlParameter CR_Acceptance_Comm_AMOUNT111 = new SqlParameter("@CR_Acceptance_Comm_AMOUNT111",dt1["CR Acceptance Comm AMOUNT111"].ToString().Trim());
                    SqlParameter CR_Acceptance_Comm_PAYER111 = new SqlParameter("@CR_Acceptance_Comm_PAYER111",dt1["CR Acceptance Comm PAYER111"].ToString().Trim());
                    SqlParameter CR_Pay_Handle_Comm_Curr111 = new SqlParameter("@CR_Pay_Handle_Comm_Curr111",dt1["CR Pay Handle Comm Curr111"].ToString().Trim());
                    SqlParameter CR_Pay_Handle_Comm_AMOUNT111 = new SqlParameter("@CR_Pay_Handle_Comm_AMOUNT111",dt1["CR Pay Handle Comm AMOUNT111"].ToString().Trim());
                    SqlParameter CR_Pay_Handle_Comm_PAYER111 = new SqlParameter("@CR_Pay_Handle_Comm_PAYER111",dt1["CR Pay Handle Comm PAYER111"].ToString().Trim());
                    SqlParameter DR_CODE_1111 = new SqlParameter("@DR_CODE_1111",dt1["DR CODE 1111"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_1111 = new SqlParameter("@DR_CUST_ABBR_1111",dt1["DR CUST. ABBR. 1111"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_1111 = new SqlParameter("@DR_AC_NUMBER_1111",dt1["DR A/C NUMBER 1111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_1111 = new SqlParameter("@DR_Current_Acc_Curr_1111",dt1["DR_Current_Acc_Curr 1111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_1111 = new SqlParameter("@DR_Current_Acc_Amount_1111",dt1["DR_Current_Acc_Amount 1111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_1111 = new SqlParameter("@DR_Current_Acc_PAYER_1111",dt1["DR_Current_Acc PAYER 1111"].ToString().Trim());
                    SqlParameter DR_CODE_2111 = new SqlParameter("@DR_CODE_2111",dt1["DR CODE 2111"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_2111 = new SqlParameter("@DR_CUST_ABBR_2111",dt1["DR CUST. ABBR. 2111"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_2111 = new SqlParameter("@DR_AC_NUMBER_2111",dt1["DR A/C NUMBER 2111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_2111 = new SqlParameter("@DR_Current_Acc_Curr_2111",dt1["DR_Current_Acc_Curr 2111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_2111 = new SqlParameter("@DR_Current_Acc_Amount_2111",dt1["DR_Current_Acc_Amount 2111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_2111 = new SqlParameter("@DR_Current_Acc_PAYER_2111",dt1["DR_Current_Acc PAYER 2111"].ToString().Trim());
                    SqlParameter DR_CODE_3111 = new SqlParameter("@DR_CODE_3111",dt1["DR CODE 3111"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_3111 = new SqlParameter("@DR_CUST_ABBR_3111",dt1["DR CUST. ABBR. 3111"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_3111 = new SqlParameter("@DR_AC_NUMBER_3111",dt1["DR A/C NUMBER 3111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_3111 = new SqlParameter("@DR_Current_Acc_Curr_3111",dt1["DR_Current_Acc_Curr 3111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_3111 = new SqlParameter("@DR_Current_Acc_Amount_3111",dt1["DR_Current_Acc_Amount 3111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_3111 = new SqlParameter("@DR_Current_Acc_PAYER_3111",dt1["DR_Current_Acc PAYER 3111"].ToString().Trim());
                    SqlParameter DR_CODE_4111 = new SqlParameter("@DR_CODE_4111",dt1["DR CODE 4111"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_4111 = new SqlParameter("@DR_CUST_ABBR_4111",dt1["DR CUST. ABBR. 4111"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_4111 = new SqlParameter("@DR_AC_NUMBER_4111",dt1["DR A/C NUMBER 4111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_4111 = new SqlParameter("@DR_Current_Acc_Curr_4111",dt1["DR_Current_Acc_Curr 4111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_4111 = new SqlParameter("@DR_Current_Acc_Amount_4111",dt1["DR_Current_Acc_Amount 4111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_4111 = new SqlParameter("@DR_Current_Acc_PAYER_4111",dt1["DR_Current_Acc PAYER 4111"].ToString().Trim());
                    SqlParameter DR_CODE_5111 = new SqlParameter("@DR_CODE_5111",dt1["DR CODE 5111"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_5111 = new SqlParameter("@DR_CUST_ABBR_5111",dt1["DR CUST. ABBR. 5111"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_5111 = new SqlParameter("@DR_AC_NUMBER_5111",dt1["DR A/C NUMBER 5111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_5111 = new SqlParameter("@DR_Current_Acc_Curr_5111",dt1["DR_Current_Acc_Curr 5111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_5111 = new SqlParameter("@DR_Current_Acc_Amount_5111",dt1["DR_Current_Acc_Amount 5111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_5111 = new SqlParameter("@DR_Current_Acc_PAYER_5111",dt1["DR_Current_Acc PAYER 5111"].ToString().Trim());
                    SqlParameter DR_CODE_6111 = new SqlParameter("@DR_CODE_6111",dt1["DR CODE 6111"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_6111 = new SqlParameter("@DR_CUST_ABBR_6111",dt1["DR CUST. ABBR. 6111"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_6111 = new SqlParameter("@DR_AC_NUMBER_6111",dt1["DR A/C NUMBER 6111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_6111 = new SqlParameter("@DR_Current_Acc_Curr_6111",dt1["DR_Current_Acc_Curr 6111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_6111 = new SqlParameter("@DR_Current_Acc_Amount_6111",dt1["DR_Current_Acc_Amount 6111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_6111 = new SqlParameter("@DR_Current_Acc_PAYER_6111",dt1["DR_Current_Acc PAYER 6111"].ToString().Trim());

                    // EXPORT ACCOUNTING 5
                    SqlParameter DOC_NO5 = new SqlParameter("@DOC_NO5",dt1["DOC NO1"].ToString().Trim());
                    SqlParameter AMOUNT11 = new SqlParameter("@AMOUNT11",dt1["AMOUNT7"].ToString().Trim());
                    SqlParameter DISCOUNT_MATURITY_Principal4 = new SqlParameter("@DISCOUNT_MATURITY_Principal4",dt1["DISCOUNT/MATURITY Principal4"].ToString().Trim());
                    SqlParameter LUMP_SUM_Principal4 = new SqlParameter("@LUMP_SUM_Principal4",dt1["LUMP SUM Principal4"].ToString().Trim());
                    SqlParameter CONTRACT_NO_Principal4 = new SqlParameter("@CONTRACT_NO_Principal4",dt1["CONTRACT NO Principal4"].ToString().Trim());
                    SqlParameter EXCH_CCY_Principal4 = new SqlParameter("@EXCH_CCY_Principal4",dt1["EXCH. CCY Principal4"].ToString().Trim());
                    SqlParameter EXCH_RATE_Principal4 = new SqlParameter("@EXCH_RATE_Principal4",dt1["EXCH. RATE Principal4"].ToString().Trim());
                    SqlParameter INTNL_EX_RATE_Principal4 = new SqlParameter("@INTNL_EX_RATE_Principal4",dt1["INTNL EX.RATE Principal4"].ToString().Trim());
                    SqlParameter DISCOUNT_MATURITY_Interest4 = new SqlParameter("@DISCOUNT_MATURITY_Interest4",dt1["DISCOUNT/MATURITY Interest4"].ToString().Trim());
                    SqlParameter LUMP_SUM_Interest4 = new SqlParameter("@LUMP_SUM_Interest4",dt1["LUMP SUM Interest4"].ToString().Trim());
                    SqlParameter CONTRACT_NO_Interest4 = new SqlParameter("@CONTRACT_NO_Interest4",dt1["CONTRACT NO Interest4"].ToString().Trim());
                    SqlParameter EXCH_CCY_Interest4 = new SqlParameter("@EXCH_CCY_Interest4",dt1["EXCH. CCY Interest4"].ToString().Trim());
                    SqlParameter EXCH_RATE_Interest4 = new SqlParameter("@EXCH_RATE_Interest4",dt1["EXCH. RATE Interest4"].ToString().Trim());
                    SqlParameter INTNL_EX_RATE_Interest4 = new SqlParameter("@INTNL_EX_RATE_Interest4",dt1["INTNL EX.RATE Interest4"].ToString().Trim());
                    SqlParameter DISCOUNT_MATURITY_Commission4 = new SqlParameter("@DISCOUNT_MATURITY_Commission4",dt1["DISCOUNT/MATURITY Commission4"].ToString().Trim());
                    SqlParameter LUMP_SUM_Commission4 = new SqlParameter("@LUMP_SUM_Commission4",dt1["LUMP SUM Commission4"].ToString().Trim());
                    SqlParameter CONTRACT_NO_Commission4 = new SqlParameter("@CONTRACT_NO_Commission4",dt1["CONTRACT NO Commission4"].ToString().Trim());
                    SqlParameter EXCH_CCY_Commission4 = new SqlParameter("@EXCH_CCY_Commission4",dt1["EXCH. CCY Commission4"].ToString().Trim());
                    SqlParameter EXCH_RATE_Commission4 = new SqlParameter("@EXCH_RATE_Commission4",dt1["EXCH. RATE Commission4"].ToString().Trim());
                    SqlParameter INTNL_EX_RATE_Commission4 = new SqlParameter("@INTNL_EX_RATE_Commission4",dt1["INTNL EX.RATE Commission4"].ToString().Trim());
                    SqlParameter DISCOUNT_MATURITY_Their_Commission4 = new SqlParameter("@DISCOUNT_MATURITY_Their_Commission4",dt1["DISCOUNT/MATURITY Their_Commission4"].ToString().Trim());
                    SqlParameter LUMP_SUM_Their_Commission4 = new SqlParameter("@LUMP_SUM_Their_Commission4",dt1["LUMP SUM Their_Commission4"].ToString().Trim());
                    SqlParameter CONTRACT_NO_Their_Commission4 = new SqlParameter("@CONTRACT_NO_Their_Commission4",dt1["CONTRACT NO Their_Commission4"].ToString().Trim());
                    SqlParameter EXCH_CCY_Their_Commission4 = new SqlParameter("@EXCH_CCY_Their_Commission4",dt1["EXCH. CCY Their_Commission4"].ToString().Trim());
                    SqlParameter EXCH_RATE_Their_Commission4 = new SqlParameter("@EXCH_RATE_Their_Commission4",dt1["EXCH. RATE Their_Commission4"].ToString().Trim());
                    SqlParameter INTNL_EX_RATE_Their_Commission4 = new SqlParameter("@INTNL_EX_RATE_Their_Commission4",dt1["INTNL EX.RATE Their_Commission4"].ToString().Trim());
                    SqlParameter CR_CODE1111 = new SqlParameter("@CR_CODE1111",dt1["CR CODE1111"].ToString().Trim());
                    SqlParameter CR_CUST_ABBR1111 = new SqlParameter("@CR_CUST_ABBR1111",dt1["CR CUST. ABBR.1111"].ToString().Trim());
                    SqlParameter CR_AC_NUMBER1111 = new SqlParameter("@CR_AC_NUMBER1111",dt1["CR A/C NUMBER1111"].ToString().Trim());
                    SqlParameter CR_Settlement_Curr1111 = new SqlParameter("@CR_Settlement_Curr1111",dt1["CR Settlement Curr1111"].ToString().Trim());
                    SqlParameter CR_Settlement_AMOUNT1111 = new SqlParameter("@CR_Settlement_AMOUNT1111",dt1["CR Settlement AMOUNT1111"].ToString().Trim());
                    SqlParameter CR_Settlement_PAYER1111 = new SqlParameter("@CR_Settlement_PAYER1111",dt1["CR Settlement PAYER1111"].ToString().Trim());
                    SqlParameter CR_Interest_Curr1111 = new SqlParameter("@CR_Interest_Curr1111",dt1["CR Interest Curr1111"].ToString().Trim());
                    SqlParameter CR_Interest_AMOUNT1111 = new SqlParameter("@CR_Interest_AMOUNT1111",dt1["CR Interest AMOUNT1111"].ToString().Trim());
                    SqlParameter CR_Interest_PAYER1111 = new SqlParameter("@CR_Interest_PAYER1111",dt1["CR Interest PAYER1111"].ToString().Trim());
                    SqlParameter CR_Acceptance_Comm_Curr1111 = new SqlParameter("@CR_Acceptance_Comm_Curr1111",dt1["CR Acceptance Comm Curr1111"].ToString().Trim());
                    SqlParameter CR_Acceptance_Comm_AMOUNT1111 = new SqlParameter("@CR_Acceptance_Comm_AMOUNT1111",dt1["CR Acceptance Comm AMOUNT1111"].ToString().Trim());
                    SqlParameter CR_Acceptance_Comm_PAYER1111 = new SqlParameter("@CR_Acceptance_Comm_PAYER1111",dt1["CR Acceptance Comm PAYER1111"].ToString().Trim());
                    SqlParameter CR_Pay_Handle_Comm_Curr1111 = new SqlParameter("@CR_Pay_Handle_Comm_Curr1111",dt1["CR Pay Handle Comm Curr1111"].ToString().Trim());
                    SqlParameter CR_Pay_Handle_Comm_AMOUNT1111 = new SqlParameter("@CR_Pay_Handle_Comm_AMOUNT1111",dt1["CR Pay Handle Comm AMOUNT1111"].ToString().Trim());
                    SqlParameter CR_Pay_Handle_Comm_PAYER1111 = new SqlParameter("@CR_Pay_Handle_Comm_PAYER1111",dt1["CR Pay Handle Comm PAYER1111"].ToString().Trim());
                    SqlParameter DR_CODE_11111 = new SqlParameter("@DR_CODE_11111",dt1["DR CODE 11111"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_11111 = new SqlParameter("@DR_CUST_ABBR_11111",dt1["DR CUST. ABBR. 11111"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_11111 = new SqlParameter("@DR_AC_NUMBER_11111",dt1["DR A/C NUMBER 11111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_11111 = new SqlParameter("@DR_Current_Acc_Curr_11111",dt1["DR_Current_Acc_Curr 11111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_11111 = new SqlParameter("@DR_Current_Acc_Amount_11111",dt1["DR_Current_Acc_Amount 11111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_11111 = new SqlParameter("@DR_Current_Acc_PAYER_11111",dt1["DR_Current_Acc PAYER 11111"].ToString().Trim());
                    SqlParameter DR_CODE_21111 = new SqlParameter("@DR_CODE_21111",dt1["DR CODE 21111"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_21111 = new SqlParameter("@DR_CUST_ABBR_21111",dt1["DR CUST. ABBR. 21111"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_21111 = new SqlParameter("@DR_AC_NUMBER_21111",dt1["DR A/C NUMBER 21111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_21111 = new SqlParameter("@DR_Current_Acc_Curr_21111",dt1["DR_Current_Acc_Curr 21111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_21111 = new SqlParameter("@DR_Current_Acc_Amount_21111",dt1["DR_Current_Acc_Amount 21111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_21111 = new SqlParameter("@DR_Current_Acc_PAYER_21111",dt1["DR_Current_Acc PAYER 21111"].ToString().Trim());
                    SqlParameter DR_CODE_31111 = new SqlParameter("@DR_CODE_31111",dt1["DR CODE 31111"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_31111 = new SqlParameter("@DR_CUST_ABBR_31111",dt1["DR CUST. ABBR. 31111"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_31111 = new SqlParameter("@DR_AC_NUMBER_31111",dt1["DR A/C NUMBER 31111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_31111 = new SqlParameter("@DR_Current_Acc_Curr_31111",dt1["DR_Current_Acc_Curr 31111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_31111 = new SqlParameter("@DR_Current_Acc_Amount_31111",dt1["DR_Current_Acc_Amount 31111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_31111 = new SqlParameter("@DR_Current_Acc_PAYER_31111",dt1["DR_Current_Acc PAYER 31111"].ToString().Trim());
                    SqlParameter DR_CODE_41111 = new SqlParameter("@DR_CODE_41111",dt1["DR CODE 41111"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_41111 = new SqlParameter("@DR_CUST_ABBR_41111",dt1["DR CUST. ABBR. 41111"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_41111 = new SqlParameter("@DR_AC_NUMBER_41111",dt1["DR A/C NUMBER 41111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_41111 = new SqlParameter("@DR_Current_Acc_Curr_41111",dt1["DR_Current_Acc_Curr 41111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_41111 = new SqlParameter("@DR_Current_Acc_Amount_41111",dt1["DR_Current_Acc_Amount 41111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_41111 = new SqlParameter("@DR_Current_Acc_PAYER_41111",dt1["DR_Current_Acc PAYER 41111"].ToString().Trim());
                    SqlParameter DR_CODE_51111 = new SqlParameter("@DR_CODE_51111",dt1["DR CODE 51111"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_51111 = new SqlParameter("@DR_CUST_ABBR_51111",dt1["DR CUST. ABBR. 51111"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_51111 = new SqlParameter("@DR_AC_NUMBER_51111",dt1["DR A/C NUMBER 51111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_51111 = new SqlParameter("@DR_Current_Acc_Curr_51111",dt1["DR_Current_Acc_Curr 51111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_51111 = new SqlParameter("@DR_Current_Acc_Amount_51111",dt1["DR_Current_Acc_Amount 51111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_51111 = new SqlParameter("@DR_Current_Acc_PAYER_51111",dt1["DR_Current_Acc PAYER 51111"].ToString().Trim());
                    SqlParameter DR_CODE_61111 = new SqlParameter("@DR_CODE_61111",dt1["DR CODE 61111"].ToString().Trim());
                    SqlParameter DR_CUST_ABBR_61111 = new SqlParameter("@DR_CUST_ABBR_61111",dt1["DR CUST. ABBR. 61111"].ToString().Trim());
                    SqlParameter DR_AC_NUMBER_61111 = new SqlParameter("@DR_AC_NUMBER_61111",dt1["DR A/C NUMBER 61111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Curr_61111 = new SqlParameter("@DR_Current_Acc_Curr_61111",dt1["DR_Current_Acc_Curr 61111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_Amount_61111 = new SqlParameter("@DR_Current_Acc_Amount_61111",dt1["DR_Current_Acc_Amount 61111"].ToString().Trim());
                    SqlParameter DR_Current_Acc_PAYER_61111 = new SqlParameter("@DR_Current_Acc_PAYER_61111",dt1["DR_Current_Acc PAYER 61111"].ToString().Trim());

                    SqlParameter FileName = new SqlParameter("@FileName", Filename);
                    SqlParameter FilePath = new SqlParameter("@FilePath", _filePath);
                    SqlParameter Generatedby = new SqlParameter("@Generatedby", Session["userName"].ToString().Trim());
                    String LOGDATE1 = DateTime.Now.ToString();
                    SqlParameter GenratedDate = new SqlParameter("@GenratedDate", LOGDATE1);
                    string LOG = objData1.SaveDeleteData("TF_EXP_RealisedGBase_LOGFILE", SRNO, FUNCTION, OPERATION_KIND, DOC_NO, CURRENCY_ABBR, AMOUNT1, CUSTOMER, COMMENT_CODE, MATURITY, CURRENCY_ABBR2, AMOUNT2, CREDITED_ON, AMOUNT3,
                INTEREST_PAYER, INTEREST_FROM, INTEREST_TO, NOS_OF_DAYS, RATE, AMOUNT4, OVERDUE_INTEREST_RATE, NOS_OF_DAYS1, AMOUNT5, RECEIVED_INTEREST_AMOUNT_6, REMARKS, ATTN,

                DOC_NO1, AMOUNT7, DISCOUNT_MATURITY_Principal, LUMP_SUM_Principal, CONTRACT_NO_Principal, EXCH_CCY_Principal, EXCH_RATE_Principal, INTNL_EX_RATE_Principal,
                DISCOUNT_MATURITY_Interest, LUMP_SUM_Interest, CONTRACT_NO_Interest, EXCH_CCY_Interest, EXCH_RATE_Interest, INTNL_EX_RATE_Interest,
                DISCOUNT_MATURITY_Commission, LUMP_SUM_Commission, CONTRACT_NO_Commission, EXCH_CCY_Commission, EXCH_RATE_Commission, INTNL_EX_RATE_Commission,
                DISCOUNT_MATURITY_Their_Commission, LUMP_SUM_Their_Commission, CONTRACT_NO_Their_Commission, EXCH_CCY_Their_Commission, EXCH_RATE_Their_Commission, INTNL_EX_RATE_Their_Commission,
                CR_CODE, CR_CUST_ABBR, CR_AC_NUMBER,
                CR_Settlement_Curr, CR_Settlement_AMOUNT, CR_Settlement_PAYER,
                CR_Interest_Curr, CR_Interest_AMOUNT, CR_Interest_PAYER,
                CR_Acceptance_Comm_Curr, CR_Acceptance_Comm_AMOUNT, CR_Acceptance_Comm_PAYER,
                CR_Pay_Handle_Comm_Curr, CR_Pay_Handle_Comm_AMOUNT, CR_Pay_Handle_Comm_PAYER,
                DR_CODE_1, DR_CUST_ABBR_1, DR_AC_NUMBER_1, DR_Current_Acc_Curr_1, DR_Current_Acc_Amount_1, DR_Current_Acc_PAYER_1,
                DR_CODE_2, DR_CUST_ABBR_2, DR_AC_NUMBER_2, DR_Current_Acc_Curr_2, DR_Current_Acc_Amount_2, DR_Current_Acc_PAYER_2,
                DR_CODE_3, DR_CUST_ABBR_3, DR_AC_NUMBER_3, DR_Current_Acc_Curr_3, DR_Current_Acc_Amount_3, DR_Current_Acc_PAYER_3,
                DR_CODE_4, DR_CUST_ABBR_4, DR_AC_NUMBER_4, DR_Current_Acc_Curr_4,
                DR_Current_Acc_Amount_4,
                DR_Current_Acc_PAYER_4,
                DR_CODE_5,
                DR_CUST_ABBR_5,
                DR_AC_NUMBER_5,
                DR_Current_Acc_Curr_5,
                DR_Current_Acc_Amount_5,
                DR_Current_Acc_PAYER_5,
                DR_CODE_6,
                DR_CUST_ABBR_6,
                DR_AC_NUMBER_6,
                DR_Current_Acc_Curr_6,
                DR_Current_Acc_Amount_6,
                DR_Current_Acc_PAYER_6,

                DOC_NO2,
                AMOUNT8,
                DISCOUNT_MATURITY_Principal1,
                LUMP_SUM_Principal1,
                CONTRACT_NO_Principal1,
                EXCH_CCY_Principal1,
                EXCH_RATE_Principal1,
                INTNL_EX_RATE_Principal1,
                DISCOUNT_MATURITY_Interest1,
                LUMP_SUM_Interest1,
                CONTRACT_NO_Interest1,
                EXCH_CCY_Interest1,
                EXCH_RATE_Interest1,
                INTNL_EX_RATE_Interest1,
                DISCOUNT_MATURITY_Commission1,
                LUMP_SUM_Commission1,
                CONTRACT_NO_Commission1,
                EXCH_CCY_Commission1,
                EXCH_RATE_Commission1,
                INTNL_EX_RATE_Commission1,
                DISCOUNT_MATURITY_Their_Commission1,
                LUMP_SUM_Their_Commission1,
                CONTRACT_NO_Their_Commission1,
                EXCH_CCY_Their_Commission1,
                EXCH_RATE_Their_Commission1,
                INTNL_EX_RATE_Their_Commission1,
                CR_CODE1,
                CR_CUST_ABBR1,
                CR_AC_NUMBER1,
                CR_Settlement_Curr1,
                CR_Settlement_AMOUNT1,
                CR_Settlement_PAYER1,
                CR_Interest_Curr1,
                CR_Interest_AMOUNT1,
                CR_Interest_PAYER1,
                CR_Acceptance_Comm_Curr1,
                CR_Acceptance_Comm_AMOUNT1,
                CR_Acceptance_Comm_PAYER1,
                CR_Pay_Handle_Comm_Curr1,
                CR_Pay_Handle_Comm_AMOUNT1,
                CR_Pay_Handle_Comm_PAYER1,
                DR_CODE_11,
                DR_CUST_ABBR_11,
                DR_AC_NUMBER_11,
                DR_Current_Acc_Curr_11,
                DR_Current_Acc_Amount_11,
                DR_Current_Acc_PAYER_11,
                DR_CODE_21,
                DR_CUST_ABBR_21,

                DR_Current_Acc_Curr_21,
                DR_Current_Acc_Amount_21,
                DR_Current_Acc_PAYER_21,
                DR_CODE_31,
                DR_CUST_ABBR_31,
                DR_AC_NUMBER_31,
                DR_Current_Acc_Curr_31,
                DR_Current_Acc_Amount_31,
                DR_Current_Acc_PAYER_31,
                DR_CODE_41,
                DR_CUST_ABBR_41,
                DR_AC_NUMBER_41,
                DR_Current_Acc_Curr_41,
                DR_Current_Acc_Amount_41,
                DR_Current_Acc_PAYER_41,
                DR_CODE_51,
                DR_CUST_ABBR_51,
                DR_AC_NUMBER_51,
                DR_Current_Acc_Curr_51,
                DR_Current_Acc_Amount_51,
                DR_Current_Acc_PAYER_51,
                DR_CODE_61,
                DR_CUST_ABBR_61,
                DR_AC_NUMBER_61,
                DR_Current_Acc_Curr_61,
                DR_Current_Acc_Amount_61,
                DR_Current_Acc_PAYER_61,

                DOC_NO3,
                AMOUNT9,
                DISCOUNT_MATURITY_Principal2,
                LUMP_SUM_Principal2,
                CONTRACT_NO_Principal2,
                EXCH_CCY_Principal2,
                EXCH_RATE_Principal2,
                INTNL_EX_RATE_Principal2,
                DISCOUNT_MATURITY_Interest2,
                LUMP_SUM_Interest2,
                CONTRACT_NO_Interest2,
                EXCH_CCY_Interest2,
                EXCH_RATE_Interest2,
                INTNL_EX_RATE_Interest2,
                DISCOUNT_MATURITY_Commission2,
                LUMP_SUM_Commission2,
                CONTRACT_NO_Commission2,
                EXCH_CCY_Commission2,
                EXCH_RATE_Commission2,
                INTNL_EX_RATE_Commission2,
                DISCOUNT_MATURITY_Their_Commission2,
                LUMP_SUM_Their_Commission2,
                CONTRACT_NO_Their_Commission2,
                EXCH_CCY_Their_Commission2,
                EXCH_RATE_Their_Commission2,
                INTNL_EX_RATE_Their_Commission2,
                CR_CODE11,
                CR_CUST_ABBR11,
                CR_AC_NUMBER11,
                CR_Settlement_Curr11,
                CR_Settlement_AMOUNT11,
                CR_Settlement_PAYER11,
                CR_Interest_Curr11,
                CR_Interest_AMOUNT11,
                CR_Interest_PAYER11,
                CR_Acceptance_Comm_Curr11,
                CR_Acceptance_Comm_AMOUNT11,
                CR_Acceptance_Comm_PAYER11,
                CR_Pay_Handle_Comm_Curr11,
                CR_Pay_Handle_Comm_AMOUNT11,
                CR_Pay_Handle_Comm_PAYER11,
                DR_CODE_111,
                DR_CUST_ABBR_111,
                DR_AC_NUMBER_111,
                DR_Current_Acc_Curr_111,
                DR_Current_Acc_Amount_111,
                DR_Current_Acc_PAYER_111,
                DR_CODE_211,
                DR_CUST_ABBR_211,
                DR_AC_NUMBER_211,
                DR_Current_Acc_Curr_211,
                DR_Current_Acc_Amount_211,
                DR_Current_Acc_PAYER_211,
                DR_CODE_311,
                DR_CUST_ABBR_311,
                DR_AC_NUMBER_311,
                DR_Current_Acc_Curr_311,
                DR_Current_Acc_Amount_311,
                DR_Current_Acc_PAYER_311,
                DR_CODE_411,
                DR_CUST_ABBR_411,
                DR_AC_NUMBER_411,
                DR_Current_Acc_Curr_411,
                DR_Current_Acc_Amount_411,
                DR_Current_Acc_PAYER_411,
                DR_CODE_511,
                DR_CUST_ABBR_511,
                DR_AC_NUMBER_511,
                DR_Current_Acc_Curr_511,
                DR_Current_Acc_Amount_511,
                DR_Current_Acc_PAYER_511,
                DR_CODE_611,
                DR_CUST_ABBR_611,
                DR_AC_NUMBER_611,
                DR_Current_Acc_Curr_611,
                DR_Current_Acc_Amount_611,
                DR_Current_Acc_PAYER_611,

                DOC_NO4,
                AMOUNT10,
                DISCOUNT_MATURITY_Principal3,
                LUMP_SUM_Principal3,
                CONTRACT_NO_Principal3,
                EXCH_CCY_Principal3,
                EXCH_RATE_Principal3,
                INTNL_EX_RATE_Principal3,
                DISCOUNT_MATURITY_Interest3,
                LUMP_SUM_Interest3,
                CONTRACT_NO_Interest3,
                EXCH_CCY_Interest3,
                EXCH_RATE_Interest3,
                INTNL_EX_RATE_Interest3,
                DISCOUNT_MATURITY_Commission3,
                LUMP_SUM_Commission3,
                CONTRACT_NO_Commission3,
                EXCH_CCY_Commission3,
                EXCH_RATE_Commission3,
                INTNL_EX_RATE_Commission3,
                DISCOUNT_MATURITY_Their_Commission3,
                LUMP_SUM_Their_Commission3,
                CONTRACT_NO_Their_Commission3,
                EXCH_CCY_Their_Commission3,
                EXCH_RATE_Their_Commission3,
                INTNL_EX_RATE_Their_Commission3,
                CR_CODE111,
                CR_CUST_ABBR111,
                CR_AC_NUMBER111,
                CR_Settlement_Curr111,
                CR_Settlement_AMOUNT111,
                CR_Settlement_PAYER111,
                CR_Interest_Curr111,
                CR_Interest_AMOUNT111,
                CR_Interest_PAYER111,
                CR_Acceptance_Comm_Curr111,
                CR_Acceptance_Comm_AMOUNT111,
                CR_Acceptance_Comm_PAYER111,
                CR_Pay_Handle_Comm_Curr111,
                CR_Pay_Handle_Comm_AMOUNT111,
                CR_Pay_Handle_Comm_PAYER111,
                DR_CODE_1111,
                DR_CUST_ABBR_1111,
                DR_AC_NUMBER_1111,
                DR_Current_Acc_Curr_1111,
                DR_Current_Acc_Amount_1111,
                DR_Current_Acc_PAYER_1111,
                DR_CODE_2111,
                DR_CUST_ABBR_2111,
                DR_AC_NUMBER_2111,
                DR_Current_Acc_Curr_2111,
                DR_Current_Acc_Amount_2111,
                DR_Current_Acc_PAYER_2111,
                DR_CODE_3111,
                DR_CUST_ABBR_3111,
                DR_AC_NUMBER_3111,
                DR_Current_Acc_Curr_3111,
                DR_Current_Acc_Amount_3111,
                DR_Current_Acc_PAYER_3111,
                DR_CODE_4111,
                DR_CUST_ABBR_4111,
                DR_AC_NUMBER_4111,
                DR_Current_Acc_Curr_4111,
                DR_Current_Acc_Amount_4111,
                DR_Current_Acc_PAYER_4111,
                DR_CODE_5111,
                DR_CUST_ABBR_5111,
                DR_AC_NUMBER_5111,
                DR_Current_Acc_Curr_5111,
                DR_Current_Acc_Amount_5111,
                DR_Current_Acc_PAYER_5111,
                DR_CODE_6111,
                DR_CUST_ABBR_6111,
                DR_AC_NUMBER_6111,
                DR_Current_Acc_Curr_6111,
                DR_Current_Acc_Amount_6111,
                DR_Current_Acc_PAYER_6111,


                DOC_NO5,
                AMOUNT11,
                DISCOUNT_MATURITY_Principal4,
                LUMP_SUM_Principal4,
                CONTRACT_NO_Principal4,
                EXCH_CCY_Principal4,
                EXCH_RATE_Principal4,
                INTNL_EX_RATE_Principal4,
                DISCOUNT_MATURITY_Interest4,
                LUMP_SUM_Interest4,
                CONTRACT_NO_Interest4,
                EXCH_CCY_Interest4,
                EXCH_RATE_Interest4,
                INTNL_EX_RATE_Interest4,
                DISCOUNT_MATURITY_Commission4,
                LUMP_SUM_Commission4,
                CONTRACT_NO_Commission4,
                EXCH_CCY_Commission4,
                EXCH_RATE_Commission4,
                INTNL_EX_RATE_Commission4,
                DISCOUNT_MATURITY_Their_Commission4,
                LUMP_SUM_Their_Commission4,
                CONTRACT_NO_Their_Commission4,
                EXCH_CCY_Their_Commission4,
                EXCH_RATE_Their_Commission4,
                INTNL_EX_RATE_Their_Commission4,
                CR_CODE1111,
                CR_CUST_ABBR1111,
                CR_AC_NUMBER1111,
                CR_Settlement_Curr1111,
                CR_Settlement_AMOUNT1111,
                CR_Settlement_PAYER1111,
                CR_Interest_Curr1111,
                CR_Interest_AMOUNT1111,
                CR_Interest_PAYER1111,
                CR_Acceptance_Comm_Curr1111,
                CR_Acceptance_Comm_AMOUNT1111,
                CR_Acceptance_Comm_PAYER1111,
                CR_Pay_Handle_Comm_Curr1111,
                CR_Pay_Handle_Comm_AMOUNT1111,
                CR_Pay_Handle_Comm_PAYER1111,
                DR_CODE_11111,
                DR_CUST_ABBR_11111,
                DR_AC_NUMBER_11111,
                DR_Current_Acc_Curr_11111,
                DR_Current_Acc_Amount_11111,
                DR_Current_Acc_PAYER_11111,
                DR_CODE_21111,
                DR_CUST_ABBR_21111,
                DR_AC_NUMBER_21111,
                DR_Current_Acc_Curr_21111,
                DR_Current_Acc_Amount_21111,
                DR_Current_Acc_PAYER_21111,
                DR_CODE_31111,
                DR_CUST_ABBR_31111,
                DR_AC_NUMBER_31111,
                DR_Current_Acc_Curr_31111,
                DR_Current_Acc_Amount_31111,
                DR_Current_Acc_PAYER_31111,
                DR_CODE_41111,
                DR_CUST_ABBR_41111,
                DR_AC_NUMBER_41111,
                DR_Current_Acc_Curr_41111,
                DR_Current_Acc_Amount_41111,
                DR_Current_Acc_PAYER_41111,
                DR_CODE_51111,
                DR_CUST_ABBR_51111,
                DR_AC_NUMBER_51111,
                DR_Current_Acc_Curr_51111,
                DR_Current_Acc_Amount_51111,
                DR_Current_Acc_PAYER_51111,
                DR_CODE_61111,
                DR_AC_NUMBER_61111,
                DR_Current_Acc_Curr_61111,
                DR_Current_Acc_Amount_61111,
                DR_Current_Acc_PAYER_61111,
                FileName,FilePath,
                Generatedby,
                GenratedDate);
                }
#endregion
            }
        }

        
        else
        {
            hdnGBASE.Value = "No Record In GBASE...";
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('No Record In GBASE...');", true);
        }
    }
    public void GenraloperationGbaseFileCreation()
    {
        hdnGOGABSE.Value = "";
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PRefNo = new SqlParameter("@FROMDATE", SqlDbType.VarChar);
        PRefNo.Value = txtfromdatesettlement.Text.Trim();
        SqlParameter SRNo = new SqlParameter("@TODATE", txttodatesettlement.Text.Trim());
        SqlParameter Branch = new SqlParameter("@Branch", ddlbranchsettlement.Text.Trim());
        System.Data.DataTable dt = objData1.getData("TF_EXP_RealisedGenralOprationGBaseFile", PRefNo, SRNo, Branch);
        string TodayDate = DateTime.Now.ToString("dd/MM/yyyy");
        string date = DateTime.Now.ToString("ddMMyyyy_hhmmss");
        string fromdate = txtfromdatesettlement.Text.Trim().Replace("/", "").Replace("/", "");
        string todate = txttodatesettlement.Text.Trim().Replace("/", "").Replace("/", "");
        string MTodayDate = "";
        if (TodayDate.Contains("/"))
        {
            MTodayDate = TodayDate.Replace("/", "");
        }
        if (TodayDate.Contains("-"))
        {
            MTodayDate = TodayDate.Replace("-", "");
        }
        if (dt.Rows.Count > 0)
        {
            string branchname = ddlbranchsettlement.SelectedItem.ToString().Trim();
            string brpref = "";
            if (branchname == "Mumbai")
            {
                brpref = "MB";
            }
            else if (branchname == "New Delhi")
            {
                brpref = "ND";
            }
            else if (branchname == "Chennai")
            {
                brpref = "CH";
            }
            else if (branchname == "Bangalore")
            {
                brpref = "BG";
            }
            string ADCODE = hdnauthorisedcode.Value;
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/" + ADCODE + "/Settlement/" + MTodayDate + "/GO-GBASE/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + brpref + "_" + date + "_GO-GBase" + ".xlsx";
            string _fileName= brpref + "_" + date + "_GO-GBase" + ".xlsx";
            if (dt.Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var sheet1 = wb.Worksheets.Add(dt, "GO-GBase");
                    sheet1.Table("Table1").ShowAutoFilter = false;
                    sheet1.Table("Table1").Theme = XLTableTheme.None;
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        FileStream file = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
                        MyMemoryStream.WriteTo(file);
                        file.Close();
                        MyMemoryStream.Close();
                    }
                }
                TF_DATA objserverName = new TF_DATA();
                string _serverName = objserverName.GetServerName();
                string path = "file://" + _serverName + "/Mizuho_UAT/TF_GeneratedFiles/EXPORT/";
                string link = "/Mizuho_UAT/TF_GeneratedFiles/EXPORT/";
                labelmsgsettlement.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
                hdnGOGABSE.Value = "Files Created Successfully For GO-GBASE";
                
                #region LOGCREATED
                
                //foreach (DataRow dt1 in dt.Rows)
                //{
                //    SqlParameter FUNCTION = new SqlParameter("@FUNCTION", dt1["FUNCTION"].ToString().Trim());
                //    SqlParameter OPERATION_KIND = new SqlParameter("@OPERATION_KIND", dt1["OPERATION KIND"].ToString().Trim());
                //    SqlParameter VALUE_DATE = new SqlParameter("@VALUE_DATE", dt1["VALUE DATE"].ToString().Trim());
                //    SqlParameter REF_NO = new SqlParameter("@REF_NO", dt1["REF-NO"].ToString().Trim());
                //    SqlParameter COMMENT = new SqlParameter("@COMMENT", dt1["COMMENT"].ToString().Trim());
                //    SqlParameter SECTION_CODE = new SqlParameter("@SECTION_CODE", dt1["SECTION CODE"].ToString().Trim());
                //    SqlParameter REMARKS = new SqlParameter("@REMARKS", dt1["REMARKS"].ToString().Trim());
                //    SqlParameter MEMO = new SqlParameter("@MEMO", dt1["MEMO"].ToString().Trim());
                //    SqlParameter SCHEME_NO = new SqlParameter("@SCHEME_NO", dt1["SCHEME NO"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT = new SqlParameter("@DEBIT_CREDIT", dt1["DEBIT/CREDIT"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_CCY = new SqlParameter("@DEBIT_CREDIT_CCY", dt1["DEBIT/CREDIT CCY"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_AMOUNT = new SqlParameter("@DEBIT_CREDIT_AMOUNT", dt1["DEBIT/CREDIT AMOUNT"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_CUSTOMER = new SqlParameter("@DEBIT_CREDIT_CUSTOMER", dt1["DEBIT/CREDIT CUSTOMER"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_ACCOUNT_CODE = new SqlParameter("@DEBIT_CREDIT_ACCOUNT_CODE", dt1["DEBIT/CREDIT ACCOUNT CODE"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_ACCOUNT_NO = new SqlParameter("@DEBIT_CREDIT_ACCOUNT_NO", dt1["DEBIT/CREDIT ACCOUNT NO."].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_EXCH_RATE = new SqlParameter("@DEBIT_CREDIT_EXCH_RATE", dt1["DEBIT/CREDIT EXCH RATE"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_ExchCCY = new SqlParameter("@DEBIT_CREDIT_ExchCCY", dt1["DEBIT/CREDIT ExchCCY"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_FUND = new SqlParameter("@DEBIT_CREDIT_FUND", dt1["DEBIT/CREDIT FUND"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_CHECK_NO = new SqlParameter("@DEBIT_CREDIT_CHECK_NO", dt1["DEBIT/CREDIT CHECK NO."].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_AVAILABLE = new SqlParameter("@DEBIT_CREDIT_AVAILABLE", dt1["DEBIT/CREDIT AVAILABLE"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_ADVICE_PRINT = new SqlParameter("@DEBIT_CREDIT_ADVICE_PRINT", dt1["DEBIT/CREDIT ADVICE PRINT"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_DETAILS = new SqlParameter("@DEBIT_CREDIT_DETAILS", dt1["DEBIT/CREDIT DETAILS"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_ENTITY = new SqlParameter("@DEBIT_CREDIT_ENTITY", dt1["DEBIT/CREDIT ENTITY"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_DIVISION = new SqlParameter("@DEBIT_CREDIT_DIVISION", dt1["DEBIT/CREDIT DIVISION"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_INTER_AMOUNT = new SqlParameter("@DEBIT_CREDIT_INTER_AMOUNT", dt1["DEBIT/CREDIT INTER-AMOUNT"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_INTER_RATE = new SqlParameter("@DEBIT_CREDIT_INTER_RATE", dt1["DEBIT/CREDIT INTER-RATE"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT1 = new SqlParameter("@DEBIT_CREDIT1", dt1["DEBIT/CREDIT1"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_CCY1 = new SqlParameter("@DEBIT_CREDIT_CCY1", dt1["DEBIT/CREDIT CCY1"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_AMOUNT1 = new SqlParameter("@DEBIT_CREDIT_AMOUNT1", dt1["DEBIT/CREDIT AMOUNT1"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_CUSTOMER1 = new SqlParameter("@DEBIT_CREDIT_CUSTOMER1", dt1["DEBIT/CREDIT CUSTOMER1"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_ACCOUNT_CODE1 = new SqlParameter("@DEBIT_CREDIT_ACCOUNT_CODE1", dt1["DEBIT/CREDIT ACCOUNT CODE1"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_ACCOUNT_NO1 = new SqlParameter("@DEBIT_CREDIT_ACCOUNT_NO1", dt1["DEBIT/CREDIT ACCOUNT NO.1"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_EXCH_RATE1 = new SqlParameter("@DEBIT_CREDIT_EXCH_RATE1", dt1["DEBIT/CREDIT EXCH RATE1"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_ExchCCY1 = new SqlParameter("@DEBIT_CREDIT_ExchCCY1", dt1["DEBIT/CREDIT ExchCCY1"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_FUND1 = new SqlParameter("@DEBIT_CREDIT_FUND1", dt1["DEBIT/CREDIT FUND1"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_CHECK_NO1 = new SqlParameter("@DEBIT_CREDIT_CHECK_NO1", dt1["DEBIT/CREDIT CHECK NO.1"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_AVAILABLE1 = new SqlParameter("@DEBIT_CREDIT_AVAILABLE1", dt1["DEBIT/CREDIT AVAILABLE1"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_ADVICE_PRINT1 = new SqlParameter("@DEBIT_CREDIT_ADVICE_PRINT1", dt1["DEBIT/CREDIT ADVICE PRINT1"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_DETAILS1 = new SqlParameter("@DEBIT_CREDIT_DETAILS1", dt1["DEBIT/CREDIT DETAILS1"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_ENTITY1 = new SqlParameter("@DEBIT_CREDIT_ENTITY1", dt1["DEBIT/CREDIT ENTITY1"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_DIVISION1 = new SqlParameter("@DEBIT_CREDIT_DIVISION1", dt1["DEBIT/CREDIT DIVISION1"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_INTER_AMOUNT1 = new SqlParameter("@DEBIT_CREDIT_INTER_AMOUNT1", dt1["DEBIT/CREDIT INTER-AMOUNT1"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_INTER_RATE1 = new SqlParameter("@DEBIT_CREDIT_INTER_RATE1", dt1["DEBIT/CREDIT INTER-RATE1"].ToString().Trim());
                //    SqlParameter COMMENT1 = new SqlParameter("@COMMENT1", dt1["COMMENT1"].ToString().Trim());
                //    SqlParameter SECTION_CODE1 = new SqlParameter("@SECTION_CODE1", dt1["SECTION CODE1"].ToString().Trim());
                //    SqlParameter REMARKS1 = new SqlParameter("@REMARKS1", dt1["REMARKS1"].ToString().Trim());
                //    SqlParameter MEMO1 = new SqlParameter("@MEMO1", dt1["MEMO1"].ToString().Trim());
                //    SqlParameter SCHEME_NO1 = new SqlParameter("@SCHEME_NO1", dt1["SCHEME NO1"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT2 = new SqlParameter("@DEBIT_CREDIT2", dt1["DEBIT/CREDIT2"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_CCY2 = new SqlParameter("@DEBIT_CREDIT_CCY2", dt1["DEBIT/CREDIT CCY2"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_AMOUNT2 = new SqlParameter("@DEBIT_CREDIT_AMOUNT2", dt1["DEBIT/CREDIT AMOUNT2"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_CUSTOMER2 = new SqlParameter("@DEBIT_CREDIT_CUSTOMER2", dt1["DEBIT/CREDIT CUSTOMER2"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_ACCOUNT_CODE2 = new SqlParameter("@DEBIT_CREDIT_ACCOUNT_CODE2", dt1["DEBIT/CREDIT ACCOUNT CODE2"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_ACCOUNT_NO2 = new SqlParameter("@DEBIT_CREDIT_ACCOUNT_NO2", dt1["DEBIT/CREDIT ACCOUNT NO.2"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_EXCH_RATE2 = new SqlParameter("@DEBIT_CREDIT_EXCH_RATE2", dt1["DEBIT/CREDIT EXCH RATE2"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_ExchCCY2 = new SqlParameter("@DEBIT_CREDIT_ExchCCY2", dt1["DEBIT/CREDIT ExchCCY2"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_FUND2 = new SqlParameter("@DEBIT_CREDIT_FUND2", dt1["DEBIT/CREDIT FUND2"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_CHECK_NO2 = new SqlParameter("@DEBIT_CREDIT_CHECK_NO2", dt1["DEBIT/CREDIT CHECK NO.2"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_AVAILABLE2 = new SqlParameter("@DEBIT_CREDIT_AVAILABLE2", dt1["DEBIT/CREDIT AVAILABLE2"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_ADVICE_PRINT2 = new SqlParameter("@DEBIT_CREDIT_ADVICE_PRINT2", dt1["DEBIT/CREDIT ADVICE PRINT2"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_DETAILS2 = new SqlParameter("@DEBIT_CREDIT_DETAILS2", dt1["DEBIT/CREDIT DETAILS2"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_ENTITY2 = new SqlParameter("@DEBIT_CREDIT_ENTITY2", dt1["DEBIT/CREDIT ENTITY2"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_DIVISION2 = new SqlParameter("@DEBIT_CREDIT_DIVISION2", dt1["DEBIT/CREDIT DIVISION2"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_INTER_AMOUNT2 = new SqlParameter("@DEBIT_CREDIT_INTER_AMOUNT2", dt1["DEBIT/CREDIT INTER-AMOUNT2"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_INTER_RATE2 = new SqlParameter("@DEBIT_CREDIT_INTER_RATE2", dt1["DEBIT/CREDIT INTER-RATE2"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT3 = new SqlParameter("@DEBIT_CREDIT3", dt1["DEBIT/CREDIT3"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_CCY3 = new SqlParameter("@DEBIT_CREDIT_CCY3", dt1["DEBIT/CREDIT CCY3"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_AMOUNT3 = new SqlParameter("@DEBIT_CREDIT_AMOUNT3", dt1["DEBIT/CREDIT AMOUNT3"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_CUSTOMER3 = new SqlParameter("@DEBIT_CREDIT_CUSTOMER3", dt1["DEBIT/CREDIT CUSTOMER3"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_ACCOUNT_CODE3 = new SqlParameter("@DEBIT_CREDIT_ACCOUNT_CODE3", dt1["DEBIT/CREDIT ACCOUNT CODE3"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_ACCOUNT_NO3 = new SqlParameter("@DEBIT_CREDIT_ACCOUNT_NO3", dt1["DEBIT/CREDIT ACCOUNT NO.3"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_EXCH_RATE3 = new SqlParameter("@DEBIT_CREDIT_EXCH_RATE3", dt1["DEBIT/CREDIT EXCH RATE3"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_ExchCCY3 = new SqlParameter("@DEBIT_CREDIT_ExchCCY3", dt1["DEBIT/CREDIT ExchCCY3"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_FUND3 = new SqlParameter("@DEBIT_CREDIT_FUND3", dt1["DEBIT/CREDIT FUND3"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_CHECK_NO3 = new SqlParameter("@DEBIT_CREDIT_CHECK_NO3", dt1["DEBIT/CREDIT CHECK NO.3"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_AVAILABLE3 = new SqlParameter("@DEBIT_CREDIT_AVAILABLE3", dt1["DEBIT/CREDIT AVAILABLE3"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_ADVICE_PRINT3 = new SqlParameter("@DEBIT_CREDIT_ADVICE_PRINT3", dt1["DEBIT/CREDIT ADVICE PRINT3"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_DETAILS3 = new SqlParameter("@DEBIT_CREDIT_DETAILS3", dt1["DEBIT/CREDIT DETAILS3"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_ENTITY3 = new SqlParameter("@DEBIT_CREDIT_ENTITY3", dt1["DEBIT/CREDIT ENTITY3"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_DIVISION3 = new SqlParameter("@DEBIT_CREDIT_DIVISION3", dt1["DEBIT/CREDIT DIVISION3"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_INTER_AMOUNT3 = new SqlParameter("@DEBIT_CREDIT_INTER_AMOUNT3", dt1["DEBIT/CREDIT INTER-AMOUNT3"].ToString().Trim());
                //    SqlParameter DEBIT_CREDIT_INTER_RATE3 = new SqlParameter("@DEBIT_CREDIT_INTER_RATE3", dt1["DEBIT/CREDIT INTER-RATE3"].ToString().Trim());
                //    SqlParameter FileName = new SqlParameter("@FileName", _fileName);
                //    SqlParameter FilePath = new SqlParameter("@FilePath", _filePath);
                //    SqlParameter Generatedby = new SqlParameter("@Generatedby", Session["userName"].ToString().Trim());
                //    String LOGDATE1 = DateTime.Now.ToString();
                //    SqlParameter GenratedDate = new SqlParameter("@GenratedDate", LOGDATE1);
                //    string LOG = objData1.SaveDeleteData("TF_EXP_RealisedGO_LOGFILE", FUNCTION, OPERATION_KIND, VALUE_DATE, REF_NO, COMMENT, SECTION_CODE, REMARKS, MEMO, SCHEME_NO, DEBIT_CREDIT, DEBIT_CREDIT_CCY, DEBIT_CREDIT_AMOUNT,
                //    DEBIT_CREDIT_CUSTOMER, DEBIT_CREDIT_ACCOUNT_CODE, DEBIT_CREDIT_ACCOUNT_NO, DEBIT_CREDIT_EXCH_RATE, DEBIT_CREDIT_ExchCCY, DEBIT_CREDIT_FUND,
                //    DEBIT_CREDIT_CHECK_NO, DEBIT_CREDIT_AVAILABLE, DEBIT_CREDIT_ADVICE_PRINT, DEBIT_CREDIT_DETAILS, DEBIT_CREDIT_ENTITY, DEBIT_CREDIT_DIVISION,
                //    DEBIT_CREDIT_INTER_AMOUNT, DEBIT_CREDIT_INTER_RATE, DEBIT_CREDIT1, DEBIT_CREDIT_CCY1, DEBIT_CREDIT_AMOUNT1, DEBIT_CREDIT_CUSTOMER1,
                //    DEBIT_CREDIT_ACCOUNT_CODE1, DEBIT_CREDIT_ACCOUNT_NO1, DEBIT_CREDIT_EXCH_RATE1, DEBIT_CREDIT_ExchCCY1, DEBIT_CREDIT_FUND1, DEBIT_CREDIT_CHECK_NO1,
                //    DEBIT_CREDIT_AVAILABLE1, DEBIT_CREDIT_ADVICE_PRINT1, DEBIT_CREDIT_DETAILS1, DEBIT_CREDIT_ENTITY1, DEBIT_CREDIT_DIVISION1, DEBIT_CREDIT_INTER_AMOUNT1,
                //    DEBIT_CREDIT_INTER_RATE1, COMMENT1, SECTION_CODE1, REMARKS1, MEMO1, SCHEME_NO1, DEBIT_CREDIT2, DEBIT_CREDIT_CCY2, DEBIT_CREDIT_AMOUNT2,
                //    DEBIT_CREDIT_CUSTOMER2, DEBIT_CREDIT_ACCOUNT_CODE2, DEBIT_CREDIT_ACCOUNT_NO2, DEBIT_CREDIT_EXCH_RATE2, DEBIT_CREDIT_ExchCCY2, DEBIT_CREDIT_FUND2,
                //    DEBIT_CREDIT_CHECK_NO2, DEBIT_CREDIT_AVAILABLE2, DEBIT_CREDIT_ADVICE_PRINT2, DEBIT_CREDIT_DETAILS2, DEBIT_CREDIT_ENTITY2, DEBIT_CREDIT_DIVISION2,
                //    DEBIT_CREDIT_INTER_AMOUNT2, DEBIT_CREDIT_INTER_RATE2, DEBIT_CREDIT3, DEBIT_CREDIT_CCY3, DEBIT_CREDIT_AMOUNT3, DEBIT_CREDIT_CUSTOMER3, DEBIT_CREDIT_ACCOUNT_CODE3,
                //    DEBIT_CREDIT_ACCOUNT_NO3, DEBIT_CREDIT_EXCH_RATE3, DEBIT_CREDIT_ExchCCY3, DEBIT_CREDIT_FUND3, DEBIT_CREDIT_CHECK_NO3, DEBIT_CREDIT_AVAILABLE3,
                //    DEBIT_CREDIT_ADVICE_PRINT3, DEBIT_CREDIT_DETAILS3, DEBIT_CREDIT_ENTITY3, DEBIT_CREDIT_DIVISION3, DEBIT_CREDIT_INTER_AMOUNT3, DEBIT_CREDIT_INTER_RATE3,
                //    FileName, FilePath,Generatedby,GenratedDate);
                //}
                #endregion
            }

        }
        else
        {
            hdnGOGABSE.Value = "No Record In GO-GBASE...";
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('No Record In General Operation...');", true);
            //lblgogabsenorecord.Text = "No Rocords In General Operation..";
        }
    }
    public void InterOfficeGbaseFileCreation()
    {
        hdnIOGBASE.Value = "";
        if (ddlbranchsettlement.Text.Trim() != "792")
        {
            TF_DATA objData1 = new TF_DATA();
            SqlParameter PRefNo = new SqlParameter("@FROMDATE", SqlDbType.VarChar);
            PRefNo.Value = txtfromdatesettlement.Text.Trim();
            SqlParameter SRNo = new SqlParameter("@TODATE", txttodatesettlement.Text.Trim());
            SqlParameter Branch = new SqlParameter("@Branch", ddlbranchsettlement.Text.Trim());
            System.Data.DataTable dt = objData1.getData("TF_EXP_RealisedInterofficeGBaseFile", PRefNo, SRNo, Branch);
            string TodayDate = DateTime.Now.ToString("dd/MM/yyyy");
            string date = DateTime.Now.ToString("ddMMyyyy_hhmmss");
            string fromdate = txtfromdatesettlement.Text.Trim().Replace("/", "").Replace("/", "");
            string todate = txttodatesettlement.Text.Trim().Replace("/", "").Replace("/", "");
            string MTodayDate = "";
            if (TodayDate.Contains("/"))
            {
                MTodayDate = TodayDate.Replace("/", "");
            }
            if (TodayDate.Contains("-"))
            {
                MTodayDate = TodayDate.Replace("-", "");
            }
            if (dt.Rows.Count > 0)
            {
                string branchname = ddlbranchsettlement.SelectedItem.ToString().Trim();
                string brpref = "";
                if (branchname == "Mumbai")
                {
                    brpref = "MB";
                }
                else if (branchname == "New Delhi")
                {
                    brpref = "ND";
                }
                else if (branchname == "Chennai")
                {
                    brpref = "CH";
                }
                else if (branchname == "Bangalore")
                {
                    brpref = "BG";
                }
                string ADCODE = hdnauthorisedcode.Value;
                string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/" + ADCODE + "/Settlement/" + MTodayDate + "/IO/");
                if (!Directory.Exists(_directoryPath))
                {
                    Directory.CreateDirectory(_directoryPath);
                }
                string _fileName = brpref + "_" + date + "_IOGBase" + ".xlsx";
                string _filePath = _directoryPath + "/" + brpref + "_" + date + "_IOGBase" + ".xlsx";
                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var sheet1 = wb.Worksheets.Add(dt, "IO");
                        sheet1.Table("Table1").ShowAutoFilter = false;
                        sheet1.Table("Table1").Theme = XLTableTheme.None;
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            FileStream file = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
                            MyMemoryStream.WriteTo(file);
                            file.Close();
                            MyMemoryStream.Close();
                        }
                    }
                    TF_DATA objserverName = new TF_DATA();
                    string _serverName = objserverName.GetServerName();
                    string path = "file://" + _serverName + "/Mizuho_UAT/TF_GeneratedFiles/EXPORT/";
                    string link = "/Mizuho_UAT/TF_GeneratedFiles/EXPORT/";
                    labelmsgsettlement.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
                    hdnIOGBASE.Value = "Files Created Successfully For IO-GBASE...";

                    #region LOGCREATED

                    //foreach (DataRow dt1 in dt.Rows)
                    //{
                    //    SqlParameter FUNCTION = new SqlParameter("@FUNCTION", dt1["FUNCTION"].ToString().Trim());
                    //    SqlParameter OPERATION_KIND = new SqlParameter("@OPERATION_KIND", dt1["OPERATION KIND"].ToString().Trim());
                    //    SqlParameter VALUE_DATE = new SqlParameter("@VALUE_DATE", dt1["VALUE DATE"].ToString().Trim());
                    //    SqlParameter REF_NO = new SqlParameter("@REF_NO", dt1["REF-NO"].ToString().Trim());
                    //    SqlParameter COMMENT = new SqlParameter("@COMMENT", dt1["COMMENT"].ToString().Trim());
                    //    SqlParameter SECTION_CODE = new SqlParameter("@SECTION_CODE", dt1["SECTION CODE"].ToString().Trim());
                    //    SqlParameter REMARKS = new SqlParameter("@REMARKS", dt1["REMARKS"].ToString().Trim());
                    //    SqlParameter MEMO = new SqlParameter("@MEMO", dt1["MEMO"].ToString().Trim());
                    //    SqlParameter SCHEME_NO = new SqlParameter("@SCHEME_NO", dt1["SCHEME NO"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT = new SqlParameter("@DEBIT_CREDIT", dt1["DEBIT/CREDIT"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_CCY = new SqlParameter("@DEBIT_CREDIT_CCY", dt1["DEBIT/CREDIT CCY"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_AMOUNT = new SqlParameter("@DEBIT_CREDIT_AMOUNT", dt1["DEBIT/CREDIT AMOUNT"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_CUSTOMER = new SqlParameter("@DEBIT_CREDIT_CUSTOMER", dt1["DEBIT/CREDIT CUSTOMER"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_ACCOUNT_CODE = new SqlParameter("@DEBIT_CREDIT_ACCOUNT_CODE", dt1["DEBIT/CREDIT ACCOUNT CODE"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_ACCOUNT_NO = new SqlParameter("@DEBIT_CREDIT_ACCOUNT_NO", dt1["DEBIT/CREDIT ACCOUNT NO."].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_EXCH_RATE = new SqlParameter("@DEBIT_CREDIT_EXCH_RATE", dt1["DEBIT/CREDIT EXCH RATE"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_ExchCCY = new SqlParameter("@DEBIT_CREDIT_ExchCCY", dt1["DEBIT/CREDIT ExchCCY"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_FUND = new SqlParameter("@DEBIT_CREDIT_FUND", dt1["DEBIT/CREDIT FUND"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_CHECK_NO = new SqlParameter("@DEBIT_CREDIT_CHECK_NO", dt1["DEBIT/CREDIT CHECK NO."].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_AVAILABLE = new SqlParameter("@DEBIT_CREDIT_AVAILABLE", dt1["DEBIT/CREDIT AVAILABLE"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_ADVICE_PRINT = new SqlParameter("@DEBIT_CREDIT_ADVICE_PRINT", dt1["DEBIT/CREDIT ADVICE PRINT"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_DETAILS = new SqlParameter("@DEBIT_CREDIT_DETAILS", dt1["DEBIT/CREDIT DETAILS"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_ENTITY = new SqlParameter("@DEBIT_CREDIT_ENTITY", dt1["DEBIT/CREDIT ENTITY"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_DIVISION = new SqlParameter("@DEBIT_CREDIT_DIVISION", dt1["DEBIT/CREDIT DIVISION"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_INTER_AMOUNT = new SqlParameter("@DEBIT_CREDIT_INTER_AMOUNT", dt1["DEBIT/CREDIT INTER-AMOUNT"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_INTER_RATE = new SqlParameter("@DEBIT_CREDIT_INTER_RATE", dt1["DEBIT/CREDIT INTER-RATE"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT1 = new SqlParameter("@DEBIT_CREDIT1", dt1["DEBIT/CREDIT1"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_CCY1 = new SqlParameter("@DEBIT_CREDIT_CCY1", dt1["DEBIT/CREDIT CCY1"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_AMOUNT1 = new SqlParameter("@DEBIT_CREDIT_AMOUNT1", dt1["DEBIT/CREDIT AMOUNT1"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_CUSTOMER1 = new SqlParameter("@DEBIT_CREDIT_CUSTOMER1", dt1["DEBIT/CREDIT CUSTOMER1"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_ACCOUNT_CODE1 = new SqlParameter("@DEBIT_CREDIT_ACCOUNT_CODE1", dt1["DEBIT/CREDIT ACCOUNT CODE1"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_ACCOUNT_NO1 = new SqlParameter("@DEBIT_CREDIT_ACCOUNT_NO1", dt1["DEBIT/CREDIT ACCOUNT NO.1"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_EXCH_RATE1 = new SqlParameter("@DEBIT_CREDIT_EXCH_RATE1", dt1["DEBIT/CREDIT EXCH RATE1"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_ExchCCY1 = new SqlParameter("@DEBIT_CREDIT_ExchCCY1", dt1["DEBIT/CREDIT ExchCCY1"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_FUND1 = new SqlParameter("@DEBIT_CREDIT_FUND1", dt1["DEBIT/CREDIT FUND1"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_CHECK_NO1 = new SqlParameter("@DEBIT_CREDIT_CHECK_NO1", dt1["DEBIT/CREDIT CHECK NO.1"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_AVAILABLE1 = new SqlParameter("@DEBIT_CREDIT_AVAILABLE1", dt1["DEBIT/CREDIT AVAILABLE1"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_ADVICE_PRINT1 = new SqlParameter("@DEBIT_CREDIT_ADVICE_PRINT1", dt1["DEBIT/CREDIT ADVICE PRINT1"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_DETAILS1 = new SqlParameter("@DEBIT_CREDIT_DETAILS1", dt1["DEBIT/CREDIT DETAILS1"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_ENTITY1 = new SqlParameter("@DEBIT_CREDIT_ENTITY1", dt1["DEBIT/CREDIT ENTITY1"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_DIVISION1 = new SqlParameter("@DEBIT_CREDIT_DIVISION1", dt1["DEBIT/CREDIT DIVISION1"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_INTER_AMOUNT1 = new SqlParameter("@DEBIT_CREDIT_INTER_AMOUNT1", dt1["DEBIT/CREDIT INTER-AMOUNT1"].ToString().Trim());
                    //    SqlParameter DEBIT_CREDIT_INTER_RATE1 = new SqlParameter("@DEBIT_CREDIT_INTER_RATE1", dt1["DEBIT/CREDIT INTER-RATE1"].ToString().Trim());                        
                    //    SqlParameter FileName = new SqlParameter("@FileName", _fileName);
                    //    SqlParameter FilePath = new SqlParameter("@FilePath", _filePath);
                    //    SqlParameter Generatedby = new SqlParameter("@Generatedby", Session["userName"].ToString().Trim());
                    //    String LOGDATE1 = DateTime.Now.ToString();
                    //    SqlParameter GenratedDate = new SqlParameter("@GenratedDate", LOGDATE1);
                    //    string LOG = objData1.SaveDeleteData("TF_EXP_RealisedIO_LOGFILE", FUNCTION, OPERATION_KIND, VALUE_DATE, REF_NO, COMMENT, SECTION_CODE, REMARKS, MEMO, SCHEME_NO, DEBIT_CREDIT, DEBIT_CREDIT_CCY, DEBIT_CREDIT_AMOUNT,
                    //    DEBIT_CREDIT_CUSTOMER, DEBIT_CREDIT_ACCOUNT_CODE, DEBIT_CREDIT_ACCOUNT_NO, DEBIT_CREDIT_EXCH_RATE, DEBIT_CREDIT_ExchCCY, DEBIT_CREDIT_FUND,
                    //    DEBIT_CREDIT_CHECK_NO, DEBIT_CREDIT_AVAILABLE, DEBIT_CREDIT_ADVICE_PRINT, DEBIT_CREDIT_DETAILS, DEBIT_CREDIT_ENTITY, DEBIT_CREDIT_DIVISION,
                    //    DEBIT_CREDIT_INTER_AMOUNT, DEBIT_CREDIT_INTER_RATE, DEBIT_CREDIT1, DEBIT_CREDIT_CCY1, DEBIT_CREDIT_AMOUNT1, DEBIT_CREDIT_CUSTOMER1,
                    //    DEBIT_CREDIT_ACCOUNT_CODE1, DEBIT_CREDIT_ACCOUNT_NO1, DEBIT_CREDIT_EXCH_RATE1, DEBIT_CREDIT_ExchCCY1, DEBIT_CREDIT_FUND1, DEBIT_CREDIT_CHECK_NO1,
                    //    DEBIT_CREDIT_AVAILABLE1, DEBIT_CREDIT_ADVICE_PRINT1, DEBIT_CREDIT_DETAILS1, DEBIT_CREDIT_ENTITY1, DEBIT_CREDIT_DIVISION1, DEBIT_CREDIT_INTER_AMOUNT1,
                    //    DEBIT_CREDIT_INTER_RATE1,FileName, FilePath, Generatedby, GenratedDate);
                    //}
                    #endregion
                }
            }

            else
            {
                hdnIOGBASE.Value = "No Record In IO-GBASE...";
                // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('No Record In Inter Office...');", true);
                // lblIOgabsenorecord.Text = "No Rocords In Inter Office..";
            }
        }
    }
    //-------------------------------END BY NILESH 11-09-2023------------------------------------
}
