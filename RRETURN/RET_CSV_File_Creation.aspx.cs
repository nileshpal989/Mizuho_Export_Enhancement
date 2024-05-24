using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class RRETURN_RET_CSV_File_Creation : System.Web.UI.Page
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
                btnCreate.Attributes.Add("onclick", "return validateControl();");
            }
            txtFromDate.Attributes.Add("onblur", "return validateControl();");
            btnCreate.Attributes.Add("onclick", "return validateControl();");
            txtFromDate.Focus();
        }
    }
    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_RET_GetBranchandADcodeList";
        DataTable dt = objData.getData(_query);
        ddlBranch.Items.Clear();
        //ListItem li = new ListItem();
        //li.Value = "All Branches";
        if (dt.Rows.Count > 0)
        {
            //li.Text = "All Branches";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        //else
        //    li.Text = "No record(s) found";
        //ddlBranch.Items.Insert(0, li);
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        RES_CSV_GENERATE();
    }
    public string RES_CSV_GENERATE()
    {
        string ErrorMessage = "";
        try
        {
            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            dateInfo.ShortDatePattern = "dd/MM/yyyy";
            DateTime documentDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
            DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);
            string todate = txtToDate.Text.Trim();
            string _directoryPath = "";
            string _strAdCode = "";
            string Branchname = ddlBranch.SelectedItem.ToString().Trim();
            if (Branchname == "Mumbai")
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/RRETURN/DataCheck/BR_Mumbai_ConsoFile");
            }
            if (Branchname == "New Delhi")
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/RRETURN/DataCheck/BR_NewDelhi_ConsoFile");
            }
            if (Branchname == "Bangalore")
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/RRETURN/DataCheck/BR_Bangalore_ConsoFile");
            }
            if (Branchname == "Chennai")
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/RRETURN/DataCheck/BR_Chennai_ConsoFile");
            }
            _strAdCode = "DataCheck_" + ddlBranch.SelectedItem.Value + "_" + todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4);
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            SqlParameter p1 = new SqlParameter("@Branch", SqlDbType.VarChar);
            p1.Value = ddlBranch.SelectedItem.ToString().Trim();
            SqlParameter p2 = new SqlParameter("@startdate", SqlDbType.VarChar);
            p2.Value = documentDate.ToString("MM/dd/yyyy");
            SqlParameter p3 = new SqlParameter("@enddate", SqlDbType.VarChar);
            p3.Value = documentDate1.ToString("MM/dd/yyyy");
            SqlParameter p4 = new SqlParameter("@AdCode", SqlDbType.VarChar);
            p4.Value = ddlBranch.SelectedItem.Value;
            #region CSVFile
            //REM FOR CSV FILE
            TF_DATA obj = new TF_DATA();
            string _qry = "TF_RET_CSV_File_Generate";
            DataTable dt = obj.getData(_qry, p1, p2, p3, p4);
            string _filePath = _directoryPath + "/" + _strAdCode + ".CSV";
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            string _strHeader = "TYPE,DOC DATE,DOC NO,P CODE,CURR,FC AMOUNT,EXCH. RATE,COUNTRY OF DEST,COUNTRY OF AC HOLDER,VOSTRO BANK CODE,BENEFICIARY NAME,REMITTER NAME,BILL NO,I  E CODE,FORM NO,PORT CODE,SHIPPING BILL NO,SHIPPING BILL DT,CUSTOM SR NO,REALISED AMT,SCHEDULE NO,LCINDICATION,FR. DATE,TO DATE,REMITTER_AC_NO,RemitterAddress,Bene_AcNO,BeneAddress";
            sw.WriteLine(_strHeader);
            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string MOD_TYPE = dt.Rows[j]["MOD_TYPE"].ToString().Trim();
                    sw.Write(MOD_TYPE + ",");
                    string TRANSACTION_DT = dt.Rows[j]["TRANSACTION_DT"].ToString().Trim();
                    sw.Write(TRANSACTION_DT + ",");
                    string DOCNO = dt.Rows[j]["DOCNO"].ToString();
                    sw.Write(DOCNO + ",");
                    string PURPOSEID = dt.Rows[j]["PURPOSEID"].ToString().Trim();
                    sw.Write(PURPOSEID + ",");
                    string CURR = dt.Rows[j]["CURR"].ToString().Trim();
                    sw.Write(CURR + ",");
                    string AMOUNT = dt.Rows[j]["AMOUNT"].ToString();
                    sw.Write(AMOUNT + ",");
                    string EXRT = dt.Rows[j]["EXRT"].ToString();
                    sw.Write(EXRT + ",");
                    string BN_COUNTRY_CODE = dt.Rows[j]["BN_COUNTRY_CODE"].ToString();
                    sw.Write(BN_COUNTRY_CODE + ",");
                    string AC_COUNTRY_CODE = dt.Rows[j]["AC_COUNTRY_CODE"].ToString();
                    sw.Write(AC_COUNTRY_CODE + ",");
                    string VOSTRO_BANK_CODE = dt.Rows[j]["VOSTRO_BANK_CODE"].ToString().Trim();
                    sw.Write(VOSTRO_BANK_CODE + ",");
                    string BENEFICIARYNAME = dt.Rows[j]["BENEFICIARYNAME"].ToString().Trim();
                    sw.Write(BENEFICIARYNAME + ",");
                    string REMMITERNAME = dt.Rows[j]["REMMITERNAME"].ToString();
                    sw.Write(REMMITERNAME + ",");
                    string BILLNO = dt.Rows[j]["BILLNO"].ToString();
                    sw.Write(BILLNO + ",");
                    string IECODE = dt.Rows[j]["IECODE"].ToString();
                    sw.Write(IECODE + ",");
                    string FORMSRNO = dt.Rows[j]["FORMSRNO"].ToString();
                    sw.Write(FORMSRNO + ",");
                    string PORT_CODE = dt.Rows[j]["PORT_CODE"].ToString();
                    sw.Write(PORT_CODE + ",");
                    string SHIPPING_BILL_NO = dt.Rows[j]["SHIPPING_BILL_NO"].ToString();
                    sw.Write(SHIPPING_BILL_NO + ",");
                    string SHIPPING_BILL_DT = dt.Rows[j]["SHIPPING_BILL_DT"].ToString();
                    sw.Write(SHIPPING_BILL_DT + ",");
                    string CUSTOMSRNO = dt.Rows[j]["CUSTOMSRNO"].ToString().Trim();
                    sw.Write(CUSTOMSRNO + ",");
                    string REALISED_AMT = dt.Rows[j]["REALISED_AMT"].ToString().Trim();
                    sw.Write(REALISED_AMT + ",");
                    string SCHEDULENO = dt.Rows[j]["SCHEDULENO"].ToString();
                    sw.Write(SCHEDULENO + ",");
                    string LCINDICATION = dt.Rows[j]["LCINDICATION"].ToString();
                    sw.Write(LCINDICATION + ",");
                    string FR_FORTNIGHT_DT = dt.Rows[j]["FR_FORTNIGHT_DT"].ToString();
                    sw.Write(FR_FORTNIGHT_DT + ",");
                    string TO_FORTNIGHT_DT = dt.Rows[j]["TO_FORTNIGHT_DT"].ToString().Trim();
                    sw.Write(TO_FORTNIGHT_DT + ",");
                    string REMITTER_ID = dt.Rows[j]["REMITTER_ID"].ToString();
                    sw.Write(REMITTER_ID + ",");
                    string REMITTER_ADDRESS = dt.Rows[j]["REMITTER_ADDRESS"].ToString();
                    sw.Write(REMITTER_ADDRESS + ",");
                    string BENEFICIARY_ID = dt.Rows[j]["BENEFICIARY_ID"].ToString();
                    sw.Write(BENEFICIARY_ID + ",");
                    string BENEFICIARY_ADDRESS = dt.Rows[j]["BENEFICIARY_ADDRESS"].ToString();
                    sw.WriteLine(BENEFICIARY_ADDRESS + ",");
                }
                TF_DATA objServerName = new TF_DATA();
                string _serverName = objServerName.GetServerName();                
                string path = "";
                string link = "";
                if (Branchname == "Mumbai")
                {
                    path = "file://" + _serverName + "/TF_GeneratedFiles/RRETURN/DataCheck/BR_Mumbai_ConsoFile";
                    link = "/TF_GeneratedFiles/RRETURN/DataCheck/BR_Mumbai_ConsoFile";
                }
                if (Branchname == "New Delhi")
                {
                    path = "file://" + _serverName + "/TF_GeneratedFiles/RRETURN/DataCheck/BR_NewDelhi_ConsoFile";
                    link = "/TF_GeneratedFiles/RRETURN/DataCheck/BR_NewDelhi_ConsoFile";
                }
                if (Branchname == "Bangalore")
                {
                    path = "file://" + _serverName + "/TF_GeneratedFiles/RRETURN/DataCheck/BR_Bangalore_ConsoFile";
                    link = "/TF_GeneratedFiles/RRETURN/DataCheck/BR_Bangalore_ConsoFile";
                }
                if (Branchname == "Chennai")
                {
                    path = "file://" + _serverName + "/TF_GeneratedFiles/RRETURN/DataCheck/BR_Chennai_ConsoFile";
                    link = "/TF_GeneratedFiles/RRETURN/DataCheck/BR_Chennai_ConsoFile";
                }
                string script = "alert('Files Created Successfully on " + _serverName + " in " + link + " ')";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
                labelMessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
            }
            else
            {
                labelMessage.Text = "Records Not Found.";
                //ErrorMessage = "Records Not Found For RES Data.";
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
            //REM END
            #endregion
        }
        catch (Exception ex)
        {
            labelMessage.Text = "Exception: " + ex.Message;
            ErrorMessage = "Exception: " + ex.Message;
        }
        return ErrorMessage;
    }
}