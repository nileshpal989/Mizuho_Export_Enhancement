using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public partial class EDPMS_TF_EBRC_ORM_CheckerView : System.Web.UI.Page
{
    TF_DATA objData = new TF_DATA();
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
                ddlrecordperpage.SelectedValue = "20";
                fillBranch();
                ddlbranch.SelectedValue = Session["userLBCode"].ToString();
                txtfromDate.Focus();
                txtfromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                ddlOrmstatus.SelectedValue = "1";
                fillGrid();
                btnapprove.Attributes.Add("onclick", "return Confirm();");
                btnAdd.Visible = false;

                if (Request.QueryString["result"] != null)
                {

                    if (Request.QueryString["result"].Trim() == "added")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);
                    }
                    else
                        if (Request.QueryString["result"].Trim() == "updated")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                        }
                }
                if (Request.QueryString["result_"] != null)
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Transaction is Approved')", true);
                }
                if (Request.QueryString["result"] != null)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record has been sent to maker.');", true);
                }
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
        ddlbranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlbranch.DataSource = dt.DefaultView;
            ddlbranch.DataTextField = "BranchName";
            ddlbranch.DataValueField = "BranchCode";
            ddlbranch.DataBind();
        }
        else
            li.Text = "No record(s) found";
        ddlbranch.Items.Insert(0, li);
    }

    protected void fillGrid()
    {
        //getLastDocNo();
        string search = txtSearch.Text.Trim();
        string irmstatus;
        SqlParameter p1 = new SqlParameter("@branchCode", ddlbranch.SelectedValue);
        SqlParameter p2 = new SqlParameter("@Ormno", "");
        SqlParameter p3 = new SqlParameter("@mode", "");
        SqlParameter p4 = new SqlParameter("@search", search);
        string status;
        if (rdbAll.Checked == true)
        {
            status = "All";
        }
        else if (rdbApproved.Checked == true)
        {
            status = "Approved";
        }
        else if (rdbPending.Checked == true)
        {
            status = "Pending";
        }
        else
        {
            status = "Rejected";
        }
        SqlParameter p5 = new SqlParameter("@status", status);
        if (ddlOrmstatus.SelectedValue == "1")
        {
            irmstatus = "F";
        }
        else if (ddlOrmstatus.SelectedValue == "2")
        {
            irmstatus = "A";
        }
        else if (ddlOrmstatus.SelectedValue == "3")
        {
            irmstatus = "C";
        }
        else
        {
            irmstatus = "";
        }

        if (irmstatus == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select irm status')", true);
            ddlOrmstatus.Focus();
        }
        else
        {
            SqlParameter p6 = new SqlParameter("@fromdate", txtfromDate.Text);
            SqlParameter p7 = new SqlParameter("@todate", txtToDate.Text);
            SqlParameter p8 = new SqlParameter("@Ormstatus", irmstatus);
            string _query = "TF_EBRC_ORM_Grid";
            TF_DATA objData = new TF_DATA();
            DataTable dt = objData.getData(_query, p1, p2, p3, p4, p5, p6, p7, p8);
            if (dt.Rows.Count > 0)
            {
                int _records = dt.Rows.Count;
                int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
                GridViewInwData.PageSize = _pageSize;
                GridViewInwData.DataSource = dt.DefaultView;
                GridViewInwData.DataBind();
                GridViewInwData.Visible = true;
                rowGrid.Visible = true;
                rowPager.Visible = true;
                lblMessage.Visible = false;
                pagination(_records, _pageSize);
            }
            else
            {
                GridViewInwData.Visible = false;
                rowGrid.Visible = false;
                rowPager.Visible = false;
                lblMessage.Text = "No record(s) found.";
                lblMessage.Visible = true;
            }
        }
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }

    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewInwData.PageCount != GridViewInwData.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewInwData.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewInwData.PageIndex + 1) + " of " + GridViewInwData.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewInwData.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewInwData.PageIndex != (GridViewInwData.PageCount - 1))
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

    protected void ddlOrmstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrmstatus.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Orm status')", true);
            ddlOrmstatus.Focus();
        }
        if (ddlOrmstatus.SelectedValue == "1")
        {
            fillGrid();
        }
        else if (ddlOrmstatus.SelectedValue == "2")
        {
            fillGrid();
        }
        else if (ddlOrmstatus.SelectedValue == "3")
        {
            fillGrid();
        }
    }
    protected void GridViewInwData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //string result = "";
        //string _ormno = e.CommandArgument.ToString();
        //SqlParameter p1 = new SqlParameter("@branchCode", ddlbranch.SelectedValue.Trim());
        //SqlParameter p2 = new SqlParameter("@Ormno", _ormno);

        //string _query = "TF_EBRC_ORM_Grid_Delete";
        //TF_DATA objData = new TF_DATA();
        //result = objData.SaveDeleteData(_query, p1, p2);
        //fillGrid();
        //if (result == "deleted")
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
        //}
    }
    protected void GridViewInwData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblormno = new Label();
            Label lblDocDate = new Label();
            Button btnDelete = new Button();
            Label Status = new Label();
            Label getstatus = new Label();
            Label pushstatus = new Label();
            Label ormstatus = new Label();
            Status = (Label)e.Row.FindControl("lblstatus");
            pushstatus = (Label)e.Row.FindControl("lblAPIstatus");
            getstatus = (Label)e.Row.FindControl("lblDGFTstatus");
            if (Status.Text == "Approved")
            {
                e.Row.BackColor = System.Drawing.Color.GreenYellow;
            }
            if (Status.Text == "Rejected")
            {
                e.Row.BackColor = System.Drawing.Color.Tomato;
            }
            if (getstatus.Text == "Processed")
            {
                e.Row.BackColor = System.Drawing.Color.GreenYellow;
            }
            if (((getstatus.Text == "Failed" ) || (pushstatus.Text == "Failed")))
            {
                e.Row.BackColor = System.Drawing.Color.Tomato;
            }
            lblormno = (Label)e.Row.FindControl("lblORMNo");
            ormstatus = (Label)e.Row.FindControl("lblormstatus");
            //btnDelete = (Button)e.Row.FindControl("btnDelete");
            //btnDelete.Enabled = true;
            //btnDelete.CssClass = "deleteButton";
            //btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
            CheckBox chk = (CheckBox)e.Row.FindControl("chkselect");
            string result;
            SqlParameter p1 = new SqlParameter("@ormno", lblormno.Text.Trim());
            result = objData.SaveDeleteData("TF_EBRC_ORM_EnableDisableChkGrid", p1);
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='TF_EBRC_ORM_Checker.aspx?mode=edit&Ormno=" + lblormno.Text.Trim() + "&Ormstatus=" + ormstatus.Text + "'";
                if (i != 12)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    if (result == "Exists")
                    {
                        chk.Enabled = false;
                        chk.Checked = false;

                    }
                    else
                    {
                        cell.Style.Add("cursor", "default");
                    }
                i++;
            }
        }
    }
    protected void ddlrecordperpage_SelectedIndexChanged1(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewInwData.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewInwData.PageIndex > 0)
        {
            GridViewInwData.PageIndex = GridViewInwData.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewInwData.PageIndex != GridViewInwData.PageCount - 1)
        {
            GridViewInwData.PageIndex = GridViewInwData.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewInwData.PageIndex = GridViewInwData.PageCount - 1;
        fillGrid();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void btnsearchrecord_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void rdbAll_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void rdbApproved_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void rdbRejected_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void rdbPending_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlbranch.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Select Branch.');", true);
            ddlbranch.Focus();
        }
        else
        {
            string branch = ddlbranch.SelectedValue.Trim();
            Response.Redirect("TF_EBRC_ORM_Checker.aspx?mode=add", true);
        }
    }

    protected void btnapprove_Click(object sender, EventArgs e)
    {
        int count = 0;
        string count_ = "";
        foreach (GridViewRow gvrow_ in GridViewInwData.Rows)
        {
            
            if (gvrow_.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)gvrow_.FindControl("chkSelect");
                if (chk != null & chk.Checked)
                {
                    count++;
                }
                count_ = count.ToString();
            }
        }
        if (count != 0)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                string data = "";
                Label lblormno = new Label();
                Label ormstatus = new Label();
                string _result;
                string result_;
                string uniquetxid;
                SqlParameter p0 = new SqlParameter("@branchcode", ddlbranch.SelectedValue);
                string _qry = "TF_EBRC_ORM_GenerateUniqueTxId";
                DataTable dt = objData.getData(_qry, p0);
                uniquetxid = dt.Rows[0]["bktxid"].ToString().Trim();
                foreach (GridViewRow gvrow in GridViewInwData.Rows)
                {
                    
                    if (gvrow.RowType == DataControlRowType.DataRow)
                    {
                        lblormno = (gvrow.Cells[0].FindControl("lblORMNo") as Label);
                        ormstatus = (gvrow.Cells[0].FindControl("lblormstatus") as Label);
                        CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");

                        if (chk != null & chk.Checked)
                        {
                            string storid = lblormno.Text;
                            string storname = ormstatus.Text;
                            data = data + storid + " ,  " + storname + " , " + "<br>";

                            SqlParameter p1 = new SqlParameter("@Addedby", Session["userName"].ToString());
                            SqlParameter p2 = new SqlParameter("@Addeddate", System.DateTime.Now.ToString("dd/MM/yyyy"));
                            SqlParameter p3 = new SqlParameter("@status", "Approved");
                            SqlParameter p4 = new SqlParameter("@ormno", lblormno.Text);
                            SqlParameter p5 = new SqlParameter("@ormstatus", ormstatus.Text);
                            SqlParameter p6 = new SqlParameter("@bulkflag", "B");
                            SqlParameter p7 = new SqlParameter("@uniquetxid", uniquetxid);
                            _result = objData.SaveDeleteData("TF_EBRC_ORM_UpdateStatusGrid", p1, p2, p3, p4, p5, p6,p7);

                            if (_result == "Updated")
                            {
                                SqlParameter J1 = new SqlParameter("@BranchCode", ddlbranch.SelectedValue);
                                SqlParameter J2 = new SqlParameter("@ormno", lblormno.Text);
                                SqlParameter J3 = new SqlParameter("@ormstatus", ormstatus.Text);
                                SqlParameter J4 = new SqlParameter("@uniquetxid", uniquetxid);
                                result_ = objData.SaveDeleteData("TF_EBRC_ORM_BulkJsonInsert", J1, J2, J3, J4);
                            }

                        }
                    }
                }
                lbl.Text = data;
                JsonCreation(uniquetxid, count_);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Row')", true);
        }
    }

    protected void JsonCreation(string uniquetxid, string count_)
    {
        string id = uniquetxid;
        string count = count_;
        OrmCreation OrmCreation = new OrmCreation();
        ormDataLst ormObj = new ormDataLst();
        OrmCreation.uniqueTxId = id;

        SqlParameter p1 = new SqlParameter("@uniqutxid", id);
        DataTable dt = objData.getData("TF_EBRC_ORM_JSONFileCreationGrid", p1);

        List<ormDataLst> ormList1 = new List<ormDataLst>();
        ormList1 = (from DataRow dr in dt.Rows

                    select new ormDataLst()
                    {
                        ormIssueDate = dr["ORMIssueDate"].ToString().Trim(),
                        ormNumber = dr["ORMNo"].ToString().Trim(),
                        ormStatus = dr["ORMStatus"].ToString().Trim(),
                        ifscCode = dr["IFSCCode"].ToString().Trim(),
                        ornAdCode = dr["ADCode"].ToString().Trim(),
                        paymentDate = dr["PaymentDate"].ToString().Trim(),
                        ornFCC = dr["ORNFCC"].ToString().Trim(),
                        ornFCAmount = Math.Round(System.Convert.ToDecimal(dr["ORNFCCAmt"].ToString().Trim()),2),
                        ornINRAmount = Math.Round(System.Convert.ToDecimal(dr["INRPayableAmt"].ToString().Trim()),2),
                        iecCode = dr["IECCode"].ToString().Trim(),
                        panNumber = dr["PanNo"].ToString().Trim(),
                        beneficiaryName = dr["BenefName"].ToString().Trim(),
                        beneficiaryCountry = dr["BenefCountry"].ToString().Trim(),
                        purposeOfOutward = dr["PurposeCode"].ToString().Trim(),
                        referenceIRM = dr["RefIRM"].ToString().Trim()
                    }).ToList();      


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
        String FileNameStatus="F";
        if (ddlOrmstatus.SelectedItem.Text == "Fresh")
        {
            FileNameStatus = "F";
        }
        else if (ddlOrmstatus.SelectedItem.Text == "Amended")
        {
            FileNameStatus = "A";
        }
        else 
        {
            FileNameStatus = "C";
        }
        //string FileName = "ORM_Json_Decrypted" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
        string FileName = uniquetxid  +"_"+ FileNameStatus;

        string _filePath = _directoryPath + "/" + FileName + ".json";

        //if (File.Exists(_filePath))
        //{
        //    File.Delete(_filePath);

        //    using (var tw = new StreamWriter(_filePath, true))
        //    {
        //        tw.WriteLine(jsonFormat.ToString());
        //        tw.Close();
        //    }
        //}
        //else if (!File.Exists(_filePath))
        //{
        //    using (var tw = new StreamWriter(_filePath, true))
        //    {
        //        tw.WriteLine(jsonFormat.ToString());
        //        tw.Close();
        //    }
        //}
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
        Base64EncodedJsonFile(_filePath, count);
    }

    protected void Base64EncodedJsonFile(string _filePath, string count)
    {
        string inputFile = _filePath;
        string count_ = count;

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

        SqlParameter j1 = new SqlParameter("@JSON_FileOutput", file);
        SqlParameter j2 = new SqlParameter("@Date_Time", System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        SqlParameter j3 = new SqlParameter("@JSON_Filename", FileName);
        string query = "TF_EBRC_ORM_jsonOutputBase64Encrypted";
        string res = objData.SaveDeleteData(query, j1, j2, j3);
        //AESEncryptJsonFile(_path, count_);
        EncryptAndEncode(file, count_, _path);
    }

    //protected void AESEncryptJsonFile(string _path, string count_)
    //{
    //    string inputFile = _path;
    //    string count = count_;
    //    string todaydt = System.DateTime.Now.ToString("ddMMyyyy");

    //    string AESoutputFile = Server.MapPath("~/TF_GeneratedFiles/EBRC/ORM/Json_Eecrypted/") + todaydt;

    //    if (!Directory.Exists(AESoutputFile))
    //    {
    //        Directory.CreateDirectory(AESoutputFile);
    //    }
    //    string FileName = "ORM_Json_AESEncrypted" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");

    //    string _AESpath = AESoutputFile + "/" + FileName + ".json";

    //    byte[] key = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
    //    string cryptFile = _AESpath;

    //    using (FileStream fileCrypt = new FileStream(cryptFile, FileMode.Create))
    //    {
    //        using (AesManaged encrypt = new AesManaged())
    //        {
    //            using (CryptoStream cs = new CryptoStream(fileCrypt, encrypt.CreateEncryptor(key, key), CryptoStreamMode.Write))
    //            {
    //                using (FileStream fileInput = new FileStream(inputFile, FileMode.Open))
    //                {
    //                    encrypt.KeySize = 256;
    //                    encrypt.BlockSize = 128;
    //                    int data;
    //                    while ((data = fileInput.ReadByte()) != -1)
    //                    {
    //                        cs.WriteByte((byte)data);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    //string record = count + " records are Aporoved";
    //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "alert('" + record + "');", true);
    //    //Response.Redirect("~/EBR/TF_EBRC_IRM_CheckerView.aspx", true);
    //    string msg = count + " records are Aporoved";
    //    string aspx = "TF_EBRC_ORM_CheckerView.aspx";
    //    var page = HttpContext.Current.CurrentHandler as Page;
    //    ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('" + msg + "');window.location ='" + aspx + "';", true);

    //    //readonlytext();
    //}

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
    public void EncryptAndEncode(string file, string count_, string _path)//--- encoded64data from database
    {
        try
        {
            using (var csp = new AesCryptoServiceProvider())
            {
                //string PASSWORD = "";
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
                //lblEncryptedValue.Text = "Encoded and Encrypted value : " + base64String;
                //HiddenField1.Value = base64String;
                string inputFile = _path;
                string count = count_;
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

                string msg = count + " records are Aporoved";
                string aspx = "TF_EBRC_ORM_CheckerView.aspx";
                var page = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('" + msg + "');window.location ='" + aspx + "';", true);
            }
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


    protected void chkall_CheckedChanged(object sender, EventArgs e)
    {
        Label lblormno = new Label();
        foreach (GridViewRow gvrow in GridViewInwData.Rows)
        {
            if (gvrow.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk_ = (CheckBox)gvrow.FindControl("chkall");
                CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                if (chk.Checked)
                {
                    lblormno = (gvrow.Cells[0].FindControl("lblORMNo") as Label);
                    string result;
                    SqlParameter p1 = new SqlParameter("@ormno", lblormno.Text.Trim());
                    SqlParameter p2 = new SqlParameter("@status", ddlOrmstatus.SelectedItem.Text.Trim());
                    result = objData.SaveDeleteData("TF_EBRC_ORM_EnableDisableChkGrid", p1,p2);
                    if (result == "Exists")
                    {
                        chk.Enabled = false;
                        chk.Checked = false;
                    }
                    else
                    {
                        btnapprove.Enabled = true;
                    }
                }
                else
                {
                    //    CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                    lblormno = (gvrow.Cells[0].FindControl("lblORMNo") as Label);
                    string result;
                    SqlParameter p1 = new SqlParameter("@ormno", lblormno.Text.Trim());
                    result = objData.SaveDeleteData("TF_EBRC_ORM_EnableDisableChkGrid", p1);
                    if (result == "Exists")
                    {
                        chk.Enabled = false;
                        chk.Checked = false;
                    }
                    else
                    {
                        btnapprove.Enabled = true;
                    }
                }
            }
        }
    }
    
}