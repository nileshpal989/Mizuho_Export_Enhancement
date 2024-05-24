using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using DocumentFormat.OpenXml.Office2010.Excel;

public partial class EDPMS_TF_EBRC_ORM_Checker : System.Web.UI.Page
{
    TF_DATA objData = new TF_DATA();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"].ToString() == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["mode"].ToString() != "add")
                {
                    txtbktxid.Text = Request.QueryString["Ormno"].ToString();
                    ddlOrmstatus.Text = Request.QueryString["Ormstatus"].ToString();
                    getdetails();
                    readonlytext();
                    TF_DATA objData = new TF_DATA();
                    SqlParameter p1 = new SqlParameter("@ormno", txtORMNo.Text);
                    SqlParameter p2 = new SqlParameter("@ormstatus", ddlOrmstatus.Text);
                    string _query = "TF_EBRC_ORM_ApproveStatusRecord";
                    DataTable dt = objData.getData(_query, p1, p2);
                    if (dt.Rows.Count > 0)
                    {
                        lblmessage1.Font.Size = 11;
                        lblmessage1.Font.Bold = true;
                        lblmessage1.Text = "Json File Created for this ORM Number";
                        ddlstatus.Enabled = false;
                        btnAdd.Enabled = false;
                    }

                    ddlstatus.Attributes.Add("onchange", "return DialogAlert();");
                    btnAdd.Attributes.Add("onclick", "return  ShowProgress();");
                }
                else
                {
                  //  GenerateTxId();
                }

            }
        }
    }
    public void readonlytext()
    {
        txtBranchCode.Enabled = false;
        txtORMNo.Enabled = false;
        txtPaymentDate.Enabled = false;
        txtPurposeCode.Enabled = false;
        txtBeneficiaryName.Enabled = false;
        txtBeneficiaryCountry.Enabled = false;
        txt_ieccode.Enabled = false;
        txadcode.Enabled = false;
        txt_ORNfc.Enabled = false;
        txORNFCCAmount.Enabled = false;
        txt_exchagerate.Enabled = false;
        txtbktxid.Enabled = false;
        txtormissueDate.Enabled = false;
        txt_inrpayableamt.Enabled = false;
        txtifsccode.Enabled = false;
        txt_panno.Enabled = false;
        txtmodeofpayment.Enabled = false;
        txtrefIRM.Enabled = false;
        ddlOrmstatus.Enabled = false;
        btnpurposecode.Enabled = false;
        btncountrylist.Enabled = false;
        btncalendar_PaymentDate.Enabled = false;
        btncalendar_ormissueDate.Enabled = false;
    }
    public void getdetails()
    {
        string irmno = Request.QueryString["Ormno"].ToString();
        string irmstatus_ = Request.QueryString["Ormstatus"].ToString();
        SqlParameter p1 = new SqlParameter("@ormno", irmno);
        SqlParameter p2 = new SqlParameter("@ormstatus", irmstatus_);
        DataTable dt = objData.getData("TF_EBRC_ORM_Grid_GetDetailes", p1, p2);
        if (dt.Rows.Count > 0)
        {
            txtBranchCode.Text = dt.Rows[0]["BranchCode"].ToString();
            txtORMNo.Text = dt.Rows[0]["ORMNo"].ToString();
            txtPaymentDate.Text = dt.Rows[0]["PaymentDate"].ToString();
            txtPurposeCode.Text = dt.Rows[0]["PurposeCode"].ToString();
            txtBeneficiaryName.Text = dt.Rows[0]["BenefName"].ToString();
            txtBeneficiaryCountry.Text = dt.Rows[0]["BenefCountry"].ToString();
            txt_ieccode.Text = dt.Rows[0]["IECCode"].ToString();
            txadcode.Text = dt.Rows[0]["ADCode"].ToString();
            txt_ORNfc.Text = dt.Rows[0]["ORNFCC"].ToString();
            txORNFCCAmount.Text = dt.Rows[0]["ORNFCCAmt"].ToString();
            txt_exchagerate.Text = dt.Rows[0]["ExchangeRate"].ToString();
            txtbktxid.Text = dt.Rows[0]["BkUniqueTxId"].ToString();
            txtormissueDate.Text = dt.Rows[0]["ORMIssueDate"].ToString();
            txt_inrpayableamt.Text = dt.Rows[0]["INRPayableAmt"].ToString();
            txtifsccode.Text = dt.Rows[0]["IFSCCode"].ToString();
            txt_panno.Text = dt.Rows[0]["PanNo"].ToString();
            txtmodeofpayment.Text = dt.Rows[0]["ModeOfPayment"].ToString();
            txtrefIRM.Text = dt.Rows[0]["RefIRM"].ToString();
            string ormtatus = dt.Rows[0]["ORMStatus"].ToString();
            if (ormtatus == "F" || ormtatus == "C" || ormtatus == "A")
            {
                ddlOrmstatus.SelectedIndex = ddlOrmstatus.Items.IndexOf(ddlOrmstatus.Items.FindByText(dt.Rows[0]["ORMStatus"].ToString().Trim()));
            }
            string status = dt.Rows[0]["Status"].ToString();
            if (status == "Approved" || status == "Rejected")
            {
                ddlstatus.SelectedIndex = ddlstatus.Items.IndexOf(ddlstatus.Items.FindByText(dt.Rows[0]["Status"].ToString().Trim()));
            }
        }
    }
    private void insertgettxid(string _result)
    {
        string result_ = "";
        string status = "";

        if (ddlstatus.SelectedIndex == 1)
        {
            status = "Approved";
        }
        else if (ddlstatus.SelectedIndex == 2)
        {
            status = "Rejected";
        }
        //--------------------------------
        if (status == "Approved")
        {
            SqlParameter J1 = new SqlParameter("@branchCode", txtBranchCode.Text.Trim());
            SqlParameter J2 = new SqlParameter("@ormissuedate", txtormissueDate.Text.Trim());
            SqlParameter J3 = new SqlParameter("@ormno", txtORMNo.Text.Trim());
            SqlParameter J4 = new SqlParameter("@ormstaus", ddlOrmstatus.SelectedItem.Text.ToString().Trim());
            SqlParameter J5 = new SqlParameter("@ifsccode", txtifsccode.Text.Trim());
            SqlParameter J6 = new SqlParameter("@adcode", txadcode.Text.Trim());
            SqlParameter J7 = new SqlParameter("@paymentdate", txtPaymentDate.Text.Trim());
            SqlParameter J8 = new SqlParameter("@ornFC", txt_ORNfc.Text.Trim());
            SqlParameter J9 = new SqlParameter("@ornFCAMT", txORNFCCAmount.Text.Trim());
            SqlParameter J10 = new SqlParameter("@inrpayableamt", txt_inrpayableamt.Text.Trim());
            SqlParameter J11 = new SqlParameter("@exchangerate", txt_exchagerate.Text.Trim());
            SqlParameter J12 = new SqlParameter("@ieccode", txt_ieccode.Text.Trim());
            SqlParameter J13 = new SqlParameter("@panno", txt_panno.Text.Trim());
            SqlParameter J14 = new SqlParameter("@benefname", txtBeneficiaryName.Text.Trim());
            SqlParameter J15 = new SqlParameter("@benefcountry", txtBeneficiaryCountry.Text.Trim());
            SqlParameter J16 = new SqlParameter("@purposecode", txtPurposeCode.Text.Trim());
            SqlParameter J17 = new SqlParameter("@modeofpayment", txtmodeofpayment.Text.Trim());
            SqlParameter J18 = new SqlParameter("@refirm", txtrefIRM.Text.Trim());
            SqlParameter J19 = new SqlParameter("@Addedby", Session["userName"].ToString());
            SqlParameter J20 = new SqlParameter("@Addeddate", System.DateTime.Now.ToString("dd/MM/yyyy"));
            SqlParameter J21 = new SqlParameter("@status", status);
            result_ = objData.SaveDeleteData("TF_EBRC_ORM_JsonCreationInsert", J1, J2, J3, J4, J5, J6, J7, J8, J9, J10, J11, J12, J13, J14, J15, J16, J17, J18, J19, J20, J21);
            getjson(result_);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string status = "";

            if (ddlstatus.SelectedIndex == 1)
            {
                status = "Approved";
            }
            else if (ddlstatus.SelectedIndex == 2)
            {
                status = "Rejected";
            }

            if (status == "Approved")
            {
                string mode = Request.QueryString["mode"].ToString();
                string _result = "", _script = "";
                string txid = txtbktxid.Text;
                SqlParameter p1 = new SqlParameter("@branchCode", txtBranchCode.Text.Trim());
                SqlParameter p2 = new SqlParameter("@bkuniquetxid", txtbktxid.Text.Trim());
                SqlParameter p3 = new SqlParameter("@ormissuedate", txtormissueDate.Text.Trim());
                SqlParameter p4 = new SqlParameter("@ormno", txtORMNo.Text.Trim());
                SqlParameter p5 = new SqlParameter("@ormstaus", ddlOrmstatus.SelectedItem.Text.ToString().Trim());
                SqlParameter p6 = new SqlParameter("@ifsccode", txtifsccode.Text.Trim());
                SqlParameter p7 = new SqlParameter("@adcode", txadcode.Text.Trim());
                SqlParameter p8 = new SqlParameter("@paymentdate", txtPaymentDate.Text.Trim());
                SqlParameter p9 = new SqlParameter("@ornFC", txt_ORNfc.Text.Trim());
                SqlParameter p10 = new SqlParameter("@ornFCAMT", txORNFCCAmount.Text.Trim());
                SqlParameter p11 = new SqlParameter("@inrpayableamt", txt_inrpayableamt.Text.Trim());
                SqlParameter p12 = new SqlParameter("@exchangerate", txt_exchagerate.Text.Trim());
                SqlParameter p13 = new SqlParameter("@ieccode", txt_ieccode.Text.Trim());
                SqlParameter p14 = new SqlParameter("@panno", txt_panno.Text.Trim());
                SqlParameter p15 = new SqlParameter("@benefname", txtBeneficiaryName.Text.Trim());
                SqlParameter p16 = new SqlParameter("@benefcountry", txtBeneficiaryCountry.Text.Trim());
                SqlParameter p17 = new SqlParameter("@purposecode", txtPurposeCode.Text.Trim());
                SqlParameter p18 = new SqlParameter("@modeofpayment", txtmodeofpayment.Text.Trim());
                SqlParameter p19 = new SqlParameter("@refirm", txtrefIRM.Text.Trim());
                SqlParameter p20 = new SqlParameter("@Addedby", Session["userName"].ToString());
                SqlParameter p21 = new SqlParameter("@Addeddate", System.DateTime.Now.ToString("dd/MM/yyyy"));
                SqlParameter p22 = new SqlParameter("@status", status);
                SqlParameter p23 = new SqlParameter("@mode", mode);
                SqlParameter p24 = new SqlParameter("@remark", hdnRejectReason.Value.Trim());
                _result = objData.SaveDeleteData("TF_EBRC_ORM_Grid_UpdateDetails", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24);

                insertgettxid(_result);

                //--------------------------------
                if (_result == "exists")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", "alert('Orm No exists')", true);
                }

                //if (_result == "Updated")
                //{
                //    _script = "window.location='TF_EBRC_ORM_CheckerView.aspx?result=" + _result + "'";
                //    ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", _script, true);
                //}
            }
            else if (status == "Rejected")
            {
                string mode = Request.QueryString["mode"].ToString();
                string _result = "", _script = "";
                string txid = txtbktxid.Text;
                SqlParameter p1 = new SqlParameter("@branchCode", txtBranchCode.Text.Trim());
                SqlParameter p2 = new SqlParameter("@bkuniquetxid", txtbktxid.Text.Trim());
                SqlParameter p3 = new SqlParameter("@ormissuedate", txtormissueDate.Text.Trim());
                SqlParameter p4 = new SqlParameter("@ormno", txtORMNo.Text.Trim());
                SqlParameter p5 = new SqlParameter("@ormstaus", ddlOrmstatus.SelectedItem.Text.ToString().Trim());
                SqlParameter p6 = new SqlParameter("@ifsccode", txtifsccode.Text.Trim());
                SqlParameter p7 = new SqlParameter("@adcode", txadcode.Text.Trim());
                SqlParameter p8 = new SqlParameter("@paymentdate", txtPaymentDate.Text.Trim());
                SqlParameter p9 = new SqlParameter("@ornFC", txt_ORNfc.Text.Trim());
                SqlParameter p10 = new SqlParameter("@ornFCAMT", txORNFCCAmount.Text.Trim());
                SqlParameter p11 = new SqlParameter("@inrpayableamt", txt_inrpayableamt.Text.Trim());
                SqlParameter p12 = new SqlParameter("@exchangerate", txt_exchagerate.Text.Trim());
                SqlParameter p13 = new SqlParameter("@ieccode", txt_ieccode.Text.Trim());
                SqlParameter p14 = new SqlParameter("@panno", txt_panno.Text.Trim());
                SqlParameter p15 = new SqlParameter("@benefname", txtBeneficiaryName.Text.Trim());
                SqlParameter p16 = new SqlParameter("@benefcountry", txtBeneficiaryCountry.Text.Trim());
                SqlParameter p17 = new SqlParameter("@purposecode", txtPurposeCode.Text.Trim());
                SqlParameter p18 = new SqlParameter("@modeofpayment", txtmodeofpayment.Text.Trim());
                SqlParameter p19 = new SqlParameter("@refirm", txtrefIRM.Text.Trim());
                SqlParameter p20 = new SqlParameter("@Addedby", Session["userName"].ToString());
                SqlParameter p21 = new SqlParameter("@Addeddate", System.DateTime.Now.ToString("dd/MM/yyyy"));
                SqlParameter p22 = new SqlParameter("@status", status);
                SqlParameter p23 = new SqlParameter("@mode", mode);
                SqlParameter p24 = new SqlParameter("@remark", hdnRejectReason.Value.Trim());
                _result = objData.SaveDeleteData("TF_EBRC_ORM_Grid_UpdateDetails", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24);
                if (_result == "exists")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", "alert('Orm No exists')", true);
                }

                if (_result == "Updated")
                {
                    _script = "window.location='TF_EBRC_ORM_CheckerView.aspx?result=" + _result + "'";
                    ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", _script, true);
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Status.')", true);
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message;
        }
    }
    private void getjson(string result_)
    {
        JsonCreation();
    }
    public void GenerateTxId()
    {
        SqlParameter p1 = new SqlParameter("@branchcode", txtBranchCode.Text);
        string _qry = "TF_EBRC_ORM_GenerateTxId";
        DataTable dt = objData.getData(_qry, p1);
        txtbktxid.Text = dt.Rows[0]["bktxid"].ToString().Trim();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/EDPMS/TF_EBRC_ORM_CheckerView.aspx", true);
    }
    protected void txtbktxid_TextChanged(object sender, EventArgs e)
    {
        //getdetails();
    }
    protected void JsonCreation()
    {
        string UNIQTXID = "", STATUSFILENAME = "";
        OrmCreation OrmCreation = new OrmCreation();
        ormDataLst ormObj = new ormDataLst();
        SqlParameter p0 = new SqlParameter("@branchcode", txtBranchCode.Text);
        SqlParameter p01 = new SqlParameter("@ormno", txtORMNo.Text);
        SqlParameter p02 = new SqlParameter("@ormstatus", ddlOrmstatus.SelectedItem.Text);
        DataTable dt_ = objData.getData("TF_EBRC_ORM_GetUniqueTxId", p0, p01, p02);
        if (dt_.Rows.Count > 0)
        {
            OrmCreation.uniqueTxId = dt_.Rows[0]["BkUniqueTxId"].ToString().Trim();
            UNIQTXID = dt_.Rows[0]["BkUniqueTxId"].ToString().Trim();
            STATUSFILENAME = ddlOrmstatus.SelectedItem.Text;
        }
        SqlParameter p03 = new SqlParameter("@uniquetxid", UNIQTXID);
        string result__ = objData.SaveDeleteData("TF_EBRC_ORM_MAINTabUpdate", p03, p01, p02);
        SqlParameter p1 = new SqlParameter("@ormno", txtORMNo.Text);
        SqlParameter p2 = new SqlParameter("@status", ddlOrmstatus.SelectedItem.Text);
        DataTable dt = objData.getData("TF_EBRC_ORM_JSONFileCreation", p1,p2);
        List<ormDataLst> ormList1 = new List<ormDataLst>(); //create array list [] ....added by suchitra
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string issuedate = dt.Rows[0]["ORMIssueDate"].ToString().Trim();
                ormObj.ormIssueDate = issuedate.Replace("/", "").Replace("/", "");
                ormObj.ormNumber = txtORMNo.Text;
                ormObj.ormStatus = dt.Rows[0]["ORMStatus"].ToString().Trim();
                ormObj.ifscCode = dt.Rows[0]["IFSCCode"].ToString().Trim();
                ormObj.ornAdCode = dt.Rows[0]["ADCode"].ToString().Trim();
                string paymentdate = dt.Rows[0]["PaymentDate"].ToString().Trim();
                ormObj.paymentDate = paymentdate.Replace("/", "").Replace("/", "");
                ormObj.ornFCC = dt.Rows[0]["ORNFCC"].ToString().Trim();
                string ornamt = dt.Rows[0]["ORNFCCAmt"].ToString().Trim();
                ormObj.ornFCAmount = Math.Round(System.Convert.ToDecimal(dt.Rows[0]["ORNFCCAmt"].ToString().Trim()), 2);
                string ornpayamt = dt.Rows[0]["INRPayableAmt"].ToString().Trim();
                ormObj.ornINRAmount = Math.Round(System.Convert.ToDecimal(dt.Rows[0]["INRPayableAmt"].ToString().Trim()), 2);
                ormObj.iecCode = dt.Rows[0]["IECCode"].ToString().Trim();
                ormObj.panNumber = dt.Rows[0]["PanNo"].ToString().Trim();
                ormObj.beneficiaryName = dt.Rows[0]["BenefName"].ToString().Trim();
                ormObj.beneficiaryCountry = dt.Rows[0]["BenefCountry"].ToString().Trim();
                ormObj.purposeOfOutward = dt.Rows[0]["PurposeCode"].ToString().Trim();
                ormObj.referenceIRM = dt.Rows[0]["RefIRM"].ToString().Trim(); ;
                ormList1.Add(ormObj);
            }
        }
        OrmCreation.ormDataLst = ormList1;
        var json = JsonConvert.SerializeObject(OrmCreation,
   new JsonSerializerSettings()
   {
       NullValueHandling = NullValueHandling.Ignore,
       Formatting = Formatting.Indented,
       Converters = { new Newtonsoft.Json.Converters.StringEnumConverter() }
   });
        string JSONresult = JsonConvert.SerializeObject(OrmCreation);
        JObject jsonFormat = JObject.Parse(JSONresult);
        string todaydt = System.DateTime.Now.ToString("ddMMyyyy");
        string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/ORMJSON/") + todaydt;
        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }
        string FileName = UNIQTXID + "_" + STATUSFILENAME;
        string _filePath = _directoryPath + "/" + FileName + ".json";
        SqlParameter j1 = new SqlParameter("@JSON_FileOutput", json);
        SqlParameter j2 = new SqlParameter("@Date_Time", System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        SqlParameter j3 = new SqlParameter("@JSON_Filename", FileName);
        string query = "TF_EBRC_ORM_jsonOutputDecrypted";
        string res = objData.SaveDeleteData(query, j1, j2, j3);
        using (var tw = new StreamWriter(_filePath, true))
        {
            tw.WriteLine(json);
            tw.Close();
        }
        Base64EncodedJsonFile(_filePath);
    }
    protected void Base64EncodedJsonFile(string _filePath)
    {
        string inputFile = _filePath;

        byte[] bytes = File.ReadAllBytes(inputFile);
        string file = Convert.ToBase64String(bytes);

        string todaydt = System.DateTime.Now.ToString("ddMMyyyy");

        string outputFile = Server.MapPath("~/TF_GeneratedFiles/EXPORT/ORMJSON/Json_Base/") + todaydt;

        if (!Directory.Exists(outputFile))
        {
            Directory.CreateDirectory(outputFile);
        }

        string FileName = "ORM_Json_Base64Encrypted" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");

        string _path = outputFile + "/" + FileName + ".json";

        File.WriteAllText(_path, file);

        //AESEncryptJsonFile(_path);
        SqlParameter j1 = new SqlParameter("@JSON_FileOutput", file);
        SqlParameter j2 = new SqlParameter("@Date_Time", System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        SqlParameter j3 = new SqlParameter("@JSON_Filename", FileName);
        string query = "TF_EBRC_ORM_jsonOutputBase64Encrypted";
        string res = objData.SaveDeleteData(query, j1, j2, j3);

        EncryptAndEncode(file, _path);


    }
    private static byte[] generateIVandSalt()
    {
        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            byte[] nonce = new byte[16];
            rng.GetBytes(nonce);
            return nonce;
        }
    }
    private static byte[] IV = generateIVandSalt();
    private static string PASSWORD = "s1AhcHD7";
    private static byte[] SALT = generateIVandSalt();
    public void EncryptAndEncode(string file, string _path)//--- encoded64data from database
    {
        try
        {
            using (var csp = new AesCryptoServiceProvider())
            {
                var spec = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(PASSWORD), SALT, 65536);
                byte[] key = spec.GetBytes(32);
                byte[] encryptedBytes;
                using (var encryptor = GetCryptoTransform(csp, true, key))
                {
                    byte[] dataBytes = Encoding.UTF8.GetBytes(file);
                    encryptedBytes = encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length);
                }
                byte[] combined = new byte[SALT.Length + IV.Length + encryptedBytes.Length];
                Array.Copy(SALT, 0, combined, 0, SALT.Length);
                Array.Copy(csp.IV, 0, combined, SALT.Length, IV.Length);
                Array.Copy(encryptedBytes, 0, combined, SALT.Length + IV.Length, encryptedBytes.Length);

                string base64String = Convert.ToBase64String(combined);
                string inputFile = _path;
                string todaydt = System.DateTime.Now.ToString("ddMMyyyy");
                string AESoutputFile = Server.MapPath("~/TF_GeneratedFiles/EXPORT/ORMJSON/Json_Eecrypted/") + todaydt;

                if (!Directory.Exists(AESoutputFile))
                {
                    Directory.CreateDirectory(AESoutputFile);
                }
                string FileName = "IRM_Json_AESEncrypted" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
                string _AESpath = AESoutputFile + "/" + FileName + ".json";

                File.WriteAllText(_AESpath, base64String);

                SqlParameter j1 = new SqlParameter("@JSON_FileOutput", base64String);
                SqlParameter j2 = new SqlParameter("@Date_Time", System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                SqlParameter j3 = new SqlParameter("@JSON_Filename", FileName);

                string query = "TF_EBRC_ORM_jsonOutputAESEncrypted";
                string res = objData.SaveDeleteData(query, j1, j2, j3);
            }
            readonlytext();
            string result = "Approved";
            Response.Redirect("~/EDPMS/TF_EBRC_ORM_CheckerView.aspx?result_=" + result + "'", true);
        }
        catch (Exception ex)
        {
        }
    }
    private static ICryptoTransform GetCryptoTransform(AesCryptoServiceProvider csp, bool encrypting, byte[] encrypted)
    {
        if (encrypting)
        {
            csp.Mode = CipherMode.CBC;
            csp.Padding = PaddingMode.PKCS7;
            var spec = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(PASSWORD), SALT, 65536);
            byte[] key = spec.GetBytes(32);
            csp.IV = IV;
            csp.Key = key;
        }
        else
        {
            csp.Mode = CipherMode.CBC;
            csp.Padding = PaddingMode.PKCS7;
            byte[] salt = new byte[16];
            Array.Copy(encrypted, 0, salt, 0, 16);
            var spec = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(PASSWORD), salt, 65536);
            byte[] key = spec.GetBytes(32);

            byte[] IV = new byte[16];
            Array.Copy(encrypted, 16, IV, 0, 16);
            csp.IV = IV;
            csp.Key = key;
        }
        if (encrypting)
        {
            return csp.CreateEncryptor();
        }
        return csp.CreateDecryptor();
    }
}