using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EXP_EXP_AddEditCurrCardRate : System.Web.UI.Page
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
                hdnMODE_DEMcurr.Value = "add";
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("EXP_ViewCurrencyCardRate.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtDate.Text = Request.QueryString["AsOnDate"].Trim();
                        txtDate.Enabled = false;
                        btncalendar_DocDate.Enabled = false;
                        filldetails();
                    }
                    else
                        txtDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                }
                
                
                txtUSD_PRate.Attributes.Add("onblur", "return FormatRates();");
                txtUSD_SRate.Attributes.Add("onblur", "return FormatRates();");

                txtGBP_PRate.Attributes.Add("onblur", "return FormatRates();");
                txtGBP_SRate.Attributes.Add("onblur", "return FormatRates();");

                txtEur_PRate.Attributes.Add("onblur", "return FormatRates();");
                txtEur_SRate.Attributes.Add("onblur", "return FormatRates();");

                txtJPY_PRate.Attributes.Add("onblur", "return FormatRates();");
                txtJPY_SRate.Attributes.Add("onblur", "return FormatRates();");

                txtCHF_PRate.Attributes.Add("onblur", "return FormatRates();");
                txtCHF_SRate.Attributes.Add("onblur", "return FormatRates();");

                txtAUD_PRate.Attributes.Add("onblur", "return FormatRates();");
                txtAUD_SRate.Attributes.Add("onblur", "return FormatRates();");

                txtCAD_PRate.Attributes.Add("onblur", "return FormatRates();");
                txtCAD_SRate.Attributes.Add("onblur", "return FormatRates();");

                txtSGD_PRate.Attributes.Add("onblur", "return FormatRates();");
                txtSGD_SRate.Attributes.Add("onblur", "return FormatRates();");

                txtSEK_PRate.Attributes.Add("onblur", "return FormatRates();");
                txtSEK_SRate.Attributes.Add("onblur", "return FormatRates();");

                txtHKD_PRate.Attributes.Add("onblur", "return FormatRates();");
                txtHKD_SRate.Attributes.Add("onblur", "return FormatRates();");

                txtSAR_PRate.Attributes.Add("onblur", "return FormatRates();");
                txtSAR_SRate.Attributes.Add("onblur", "return FormatRates();");

                txtDEM_PRate.Attributes.Add("onblur", "return FormatRates();");
                txtDEM_SRate.Attributes.Add("onblur", "return FormatRates();");

                txtDate.Attributes.Add("onblur", "return isValidDate(" + txtDate.ClientID + "," + "'Date'" + " );");
                btnSave.Attributes.Add("onclick", "return validateSave();");

            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _result = "", _Mode = Request.QueryString["mode"].ToString();

        TF_DATA objData = new TF_DATA();
        string _query = "";

        if (_Mode == "add")
        {
            _query = "TF_CurrencyCardRateExists";
            SqlParameter pCurrencyCardRateDate = new SqlParameter("@date", SqlDbType.VarChar);
            pCurrencyCardRateDate.Value = txtDate.Text.Trim();
            _result = objData.SaveDeleteData(_query, pCurrencyCardRateDate);
            if (_result == "exists")
            {
                _Mode = "edit";
                hdnMODE_DEMcurr.Value = "edit";
            }
        }
      
        _query = "TF_EXP_UpdateCurrencyCardRate";
        SqlParameter pMode = new SqlParameter("@mode", SqlDbType.VarChar);
        pMode.Value = _Mode;

        SqlParameter pDate = new SqlParameter("@date", SqlDbType.VarChar);
        pDate.Value = txtDate.Text.Trim();

        SqlParameter pUser = new SqlParameter("@user", SqlDbType.VarChar);
        pUser.Value = _userName;

        SqlParameter pUploadDate = new SqlParameter("@uploadDate", SqlDbType.VarChar);
        pUploadDate.Value = _uploadingDate;

        //----------save USD details-----------//
        SqlParameter pCurrUSD = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrUSD.Value = txtCurrUSD.Text.Trim();

        SqlParameter pUSDPrate = new SqlParameter("@PRate", SqlDbType.VarChar);
        pUSDPrate.Value = txtUSD_PRate.Text.Trim();

        SqlParameter pUSDSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pUSDSrate.Value = txtUSD_SRate.Text.Trim();

        _result = objData.SaveDeleteData(_query, pMode, pDate, pCurrUSD, pUSDPrate, pUSDSrate, pUser, pUploadDate);

        //----------save GBP details-----------//
        SqlParameter pCurrGBP = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrGBP.Value = txtCurrGBP.Text.Trim();

        SqlParameter pGBPPrate = new SqlParameter("@PRate", SqlDbType.VarChar);
        pGBPPrate.Value = txtGBP_PRate.Text.Trim();

        SqlParameter pGBPSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pGBPSrate.Value = txtGBP_SRate.Text.Trim();

        _result = objData.SaveDeleteData(_query, pMode, pDate, pCurrGBP, pGBPPrate, pGBPSrate, pUser, pUploadDate);

        //----------save EUR details-----------//
        SqlParameter pCurrEUR = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrEUR.Value = txtCurrEur.Text.Trim();

        SqlParameter pEURPrate = new SqlParameter("@PRate", SqlDbType.VarChar);
        pEURPrate.Value = txtEur_PRate.Text.Trim();

        SqlParameter pEURSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pEURSrate.Value = txtEur_SRate.Text.Trim();

        _result = objData.SaveDeleteData(_query, pMode, pDate, pCurrEUR, pEURPrate, pEURSrate, pUser, pUploadDate);

        //----------save JPY details-----------//
        SqlParameter pCurrJPY = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrJPY.Value = txtCurrJPY.Text.Trim();

        SqlParameter pJPYPrate = new SqlParameter("@PRate", SqlDbType.VarChar);
        pJPYPrate.Value = txtJPY_PRate.Text.Trim();

        SqlParameter pJPYSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pJPYSrate.Value = txtJPY_SRate.Text.Trim();

        _result = objData.SaveDeleteData(_query, pMode, pDate, pCurrJPY, pJPYPrate, pJPYSrate, pUser, pUploadDate);

        //----------save CHF details-----------//
        SqlParameter pCurrCHF = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrCHF.Value = txtCurrCHF.Text.Trim();

        SqlParameter pCHFPrate = new SqlParameter("@PRate", SqlDbType.VarChar);
        pCHFPrate.Value = txtCHF_PRate.Text.Trim();

        SqlParameter pCHFSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pCHFSrate.Value = txtCHF_SRate.Text.Trim();

        _result = objData.SaveDeleteData(_query, pMode, pDate, pCurrCHF, pCHFPrate, pCHFSrate, pUser, pUploadDate);

        //----------save AUD details-----------//
        SqlParameter pCurrAUD = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrAUD.Value = txtCurrAUD.Text.Trim();

        SqlParameter pAUDPrate = new SqlParameter("@PRate", SqlDbType.VarChar);
        pAUDPrate.Value = txtAUD_PRate.Text.Trim();

        SqlParameter pAUDSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pAUDSrate.Value = txtAUD_SRate.Text.Trim();

        _result = objData.SaveDeleteData(_query, pMode, pDate, pCurrAUD, pAUDPrate, pAUDSrate, pUser, pUploadDate);

        //----------save CAD details-----------//
        SqlParameter pCurrCAD = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrCAD.Value = txtCurrCAD.Text.Trim();

        SqlParameter pCADPrate = new SqlParameter("@PRate", SqlDbType.VarChar);
        pCADPrate.Value = txtCAD_PRate.Text.Trim();

        SqlParameter pCADSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pCADSrate.Value = txtCAD_SRate.Text.Trim();

        _result = objData.SaveDeleteData(_query, pMode, pDate, pCurrCAD, pCADPrate, pCADSrate, pUser, pUploadDate);

        //----------save SGD details-----------//
        SqlParameter pCurrSGD = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrSGD.Value = txtCurrSGD.Text.Trim();

        SqlParameter pSGDPrate = new SqlParameter("@PRate", SqlDbType.VarChar);
        pSGDPrate.Value = txtSGD_PRate.Text.Trim();

        SqlParameter pSGDSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pSGDSrate.Value = txtSGD_SRate.Text.Trim();

        _result = objData.SaveDeleteData(_query, pMode, pDate, pCurrSGD, pSGDPrate, pSGDSrate, pUser, pUploadDate);

        //----------save SEK details-----------//
        SqlParameter pCurrSEK = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrSEK.Value = txtCurrSEK.Text.Trim();

        SqlParameter pSEKPrate = new SqlParameter("@PRate", SqlDbType.VarChar);
        pSEKPrate.Value = txtSEK_PRate.Text.Trim();

        SqlParameter pSEKSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pSEKSrate.Value = txtSEK_SRate.Text.Trim();

        _result = objData.SaveDeleteData(_query, pMode, pDate, pCurrSEK, pSEKPrate, pSEKSrate, pUser, pUploadDate);

        //----------save HKD details-----------//
        SqlParameter pCurrHKD = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrHKD.Value = txtCurrHKD.Text.Trim();

        SqlParameter pHKDPrate = new SqlParameter("@PRate", SqlDbType.VarChar);
        pHKDPrate.Value = txtHKD_PRate.Text.Trim();

        SqlParameter pHKDSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pHKDSrate.Value = txtHKD_SRate.Text.Trim();

        _result = objData.SaveDeleteData(_query, pMode, pDate, pCurrHKD, pHKDPrate, pHKDSrate, pUser, pUploadDate);

        //----------save SAR details-----------//
        SqlParameter pCurrSAR = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrSAR.Value = txtCurrSAR.Text.Trim();

        SqlParameter pSARPrate = new SqlParameter("@PRate", SqlDbType.VarChar);
        pSARPrate.Value = txtSAR_PRate.Text.Trim();

        SqlParameter pSARSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pSARSrate.Value = txtSAR_SRate.Text.Trim();

        _result = objData.SaveDeleteData(_query, pMode, pDate, pCurrSAR, pSARPrate, pSARSrate, pUser, pUploadDate);

        //----------save DEM details-----------//
        SqlParameter pCurrDEM = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrDEM.Value = txtCurrDEM.Text.Trim();

        SqlParameter pDEMPrate = new SqlParameter("@PRate", SqlDbType.VarChar);
        pDEMPrate.Value = txtDEM_PRate.Text.Trim();

        SqlParameter pDEMSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pDEMSrate.Value = txtDEM_SRate.Text.Trim();
        pMode.Value = hdnMODE_DEMcurr.Value;
        _result = objData.SaveDeleteData(_query, pMode, pDate, pCurrDEM, pDEMPrate, pDEMSrate, pUser, pUploadDate);

        string _script = "";
        if (_result == "added")
        {
            _script = "window.location='EXP_ViewCurrencyCardRate.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", _script, true);
        }
        else if (_result == "updated")
        {
            _script = "window.location='EXP_ViewCurrencyCardRate.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", _script, true);
        }
        else
        {
            labelMessage.Text = _result;
        }
    }
   
    private void filldetails()
    {
        string _date = txtDate.Text.Trim();
        
        string query = "";
        SqlParameter p1 = new SqlParameter("@asOnDate", SqlDbType.VarChar);
        p1.Value = _date;

        query = "TF_EXP_GetCurrCardRateDetails";
        
        TF_DATA objdata = new TF_DATA();
        DataTable dt = objdata.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            string _Curr = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _Curr = dt.Rows[i]["Currency"].ToString();
                switch (_Curr)
                {
                    case "USD":
                        txtUSD_PRate.Text = dt.Rows[i]["PurchaseRate"].ToString();
                        txtUSD_SRate.Text = dt.Rows[i]["SaleRate"].ToString();
                        break;
                    case "GBP":
                        txtGBP_PRate.Text = dt.Rows[i]["PurchaseRate"].ToString();
                        txtGBP_SRate.Text = dt.Rows[i]["SaleRate"].ToString();
                        break;
                    case "EUR":
                        txtEur_PRate.Text = dt.Rows[i]["PurchaseRate"].ToString();
                        txtEur_SRate.Text = dt.Rows[i]["SaleRate"].ToString();
                        break;
                    case "JPY":
                        txtJPY_PRate.Text = dt.Rows[i]["PurchaseRate"].ToString();
                        txtJPY_SRate.Text = dt.Rows[i]["SaleRate"].ToString();
                        break;
                    case "CHF":
                        txtCHF_PRate.Text = dt.Rows[i]["PurchaseRate"].ToString();
                        txtCHF_SRate.Text = dt.Rows[i]["SaleRate"].ToString();
                        break;
                    case "AUD":
                        txtAUD_PRate.Text = dt.Rows[i]["PurchaseRate"].ToString();
                        txtAUD_SRate.Text = dt.Rows[i]["SaleRate"].ToString();
                        break;
                    case "CAD":
                        txtCAD_PRate.Text = dt.Rows[i]["PurchaseRate"].ToString();
                        txtCAD_SRate.Text = dt.Rows[i]["SaleRate"].ToString();
                        break;
                    case "SGD":
                        txtSGD_PRate.Text = dt.Rows[i]["PurchaseRate"].ToString();
                        txtSGD_SRate.Text = dt.Rows[i]["SaleRate"].ToString();
                        break;
                    case "SEK":
                        txtSEK_PRate.Text = dt.Rows[i]["PurchaseRate"].ToString();
                        txtSEK_SRate.Text = dt.Rows[i]["SaleRate"].ToString();
                        break;
                    case "HKD":
                        txtHKD_PRate.Text = dt.Rows[i]["PurchaseRate"].ToString();
                        txtHKD_SRate.Text = dt.Rows[i]["SaleRate"].ToString();
                        break;
                    case "SAR":
                        txtSAR_PRate.Text = dt.Rows[i]["PurchaseRate"].ToString();
                        txtSAR_SRate.Text = dt.Rows[i]["SaleRate"].ToString();
                        break;
                    case "DEM":
                        txtDEM_PRate.Text = dt.Rows[i]["PurchaseRate"].ToString();
                        txtDEM_SRate.Text = dt.Rows[i]["SaleRate"].ToString();
                        hdnMODE_DEMcurr.Value = "edit";
                        break;
                }
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_ViewCurrencyCardRate.aspx", true);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_ViewCurrencyCardRate.aspx", true);
    }
}