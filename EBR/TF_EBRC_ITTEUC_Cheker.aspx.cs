using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public partial class EBR_TF_EBRC_ITTEUC_Cheker : System.Web.UI.Page
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
                txtUploadedDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                fillGrid();
                btnapproveall.Attributes.Add("onclick", "return ShowProgress();");                
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
       // ListItem li = new ListItem();
       // li.Value = "All Branches";
        if (dt.Rows.Count > 0)
        {
           // li.Value = dt.Rows[0]["BranchName"].ToString().Trim();
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchCode";
            ddlBranch.DataBind();
        }
        //else
           // li.Text = "No record(s) found";

       // ddlBranch.Items.Insert(0, li);

    }
    protected void fillGrid()
    {

        SqlParameter p_search = new SqlParameter("@search", SqlDbType.VarChar);
        p_search.Value = txtSearch.Text.Trim();
        SqlParameter p_BranchName = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p_BranchName.Value = ddlBranch.SelectedItem.Text.Trim();
        SqlParameter p_UploadedDate = new SqlParameter("@UploadedDate", SqlDbType.VarChar);
        p_UploadedDate.Value = txtUploadedDate.Text.Trim();

        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData("TF_EBRC_ITTEUC_GetList_ForChecker", p_search, p_BranchName, p_UploadedDate);
        if (dt.Rows.Count > 0)
        {
            chkapproveall.Enabled = true;
            btnapprove.Enabled = true;
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewEBRCEntryList.PageSize = _pageSize;
            GridViewEBRCEntryList.DataSource = dt.DefaultView;
            GridViewEBRCEntryList.DataBind();
            GridViewEBRCEntryList.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
            
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                        CheckBox chk = (CheckBox)GridViewEBRCEntryList.HeaderRow.FindControl("HeaderChkAllow");
                        CheckBox chkrow = (CheckBox)GridViewEBRCEntryList.Rows[i].FindControl("RowChkAllow");
                        chk.Enabled = false;
                        chkrow.Enabled = false;
                if (dt.Rows[i]["Checker_Status"].ToString() == "A")
                {

                    chkrow.Enabled = false;
                    chkrow.Checked = false;
                    chkrow.BackColor = System.Drawing.Color.GreenYellow;
                }              
            }
        }
        else
        {
            chkapproveall.Enabled = false;
            btnapprove.Enabled = false;
            GridViewEBRCEntryList.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void GridViewEBRCEntryList_RowCommand(object sender, GridViewCommandEventArgs e)
    {       
        fillGrid();        
    }
    protected void GridViewEBRCEntryList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblsrno = new Label();
            Label lblBankUniqueTransactionId = new Label();
            Label lblBankRefNo = new Label();
            Label lblIRMIssueDate = new Label();
            Label lblIRMNo = new Label();
            Label lblIFSCCode = new Label();
            Label lblRemittanceADCode = new Label();
            Label lblRemittancedate = new Label();
            Label lblRemittanceFCC = new Label();
            Label lblRemittanceFCCAmount = new Label();
            Label lblINRCreditAmt = new Label();
            Label lblIECCode = new Label();
            Label lblPan = new Label();
            Label lblRemitterName = new Label();
            Label lblRemittercountry = new Label();
            Label lblPurpose = new Label();
            Label Status = new Label();
            Label Pushstatus = new Label();
            Label getstatus = new Label();

            lblsrno = (Label)e.Row.FindControl("lblsrno");
            lblBankUniqueTransactionId = (Label)e.Row.FindControl("lblBankUniqueTransactionId");
            lblBankRefNo = (Label)e.Row.FindControl("lblBankRefNo");
            lblIRMIssueDate = (Label)e.Row.FindControl("lblIRMIssueDate");
            lblIRMNo = (Label)e.Row.FindControl("lblIRMNo");
            lblIFSCCode = (Label)e.Row.FindControl("lblIFSCCode");
            lblRemittanceADCode = (Label)e.Row.FindControl("lblRemittanceADCode");
            lblRemittancedate = (Label)e.Row.FindControl("lblRemittancedate");
            lblRemittanceFCC = (Label)e.Row.FindControl("lblRemittanceFCC");
            lblRemittanceFCCAmount = (Label)e.Row.FindControl("lblRemittanceFCCAmount");
            lblINRCreditAmt = (Label)e.Row.FindControl("lblINRCreditAmt");
            lblIECCode = (Label)e.Row.FindControl("lblIECCode");
            lblPan = (Label)e.Row.FindControl("lblPan");
            lblRemitterName = (Label)e.Row.FindControl("lblRemitterName");
            lblRemittercountry = (Label)e.Row.FindControl("lblRemittercountry");
            lblPurpose = (Label)e.Row.FindControl("lblPurpose");
            Status = (Label)e.Row.FindControl("lblStatus");
            Pushstatus = (Label)e.Row.FindControl("lblpushstatus");
            getstatus = (Label)e.Row.FindControl("lblgetstatus");
            if (Status.Text == "Approved")
            {
                e.Row.BackColor = System.Drawing.Color.GreenYellow;
            }
            if (Pushstatus.Text == "Failed"||getstatus.Text=="Failed")
            {
                e.Row.BackColor = System.Drawing.Color.Tomato;
            }
        }
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewEBRCEntryList.PageCount != GridViewEBRCEntryList.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewEBRCEntryList.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewEBRCEntryList.PageIndex + 1) + " of " + GridViewEBRCEntryList.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewEBRCEntryList.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewEBRCEntryList.PageIndex != (GridViewEBRCEntryList.PageCount - 1))
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
        fillGrid();
    }
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewEBRCEntryList.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewEBRCEntryList.PageIndex > 0)
        {
            GridViewEBRCEntryList.PageIndex = GridViewEBRCEntryList.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewEBRCEntryList.PageIndex != GridViewEBRCEntryList.PageCount - 1)
        {
            GridViewEBRCEntryList.PageIndex = GridViewEBRCEntryList.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewEBRCEntryList.PageIndex = GridViewEBRCEntryList.PageCount - 1;
        fillGrid();
    }
    protected void txtUploadedDate_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void chkapproveall_CheckedChanged(object sender, EventArgs e)
    {
        SqlParameter p_search = new SqlParameter("@search", SqlDbType.VarChar);
        p_search.Value = txtSearch.Text.Trim();
        SqlParameter p_BranchName = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p_BranchName.Value = ddlBranch.SelectedItem.Text.Trim();
        SqlParameter p_UploadedDate = new SqlParameter("@UploadedDate", SqlDbType.VarChar);
        p_UploadedDate.Value = txtUploadedDate.Text.Trim();

        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData("TF_EBRC_ITTEUC_GetList_ForChecker", p_search, p_BranchName, p_UploadedDate);
        CheckBox chk = (CheckBox)GridViewEBRCEntryList.HeaderRow.FindControl("HeaderChkAllow");
        if (chkapproveall.Checked == true)
        {
            chk.Enabled = false;
            chk.Checked = true;
            if (chk.Checked)
            {                
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    CheckBox chkrow = (CheckBox)GridViewEBRCEntryList.Rows[i].FindControl("RowChkAllow");
                    if (dt.Rows[i]["Checker_Status"].ToString() != "A")
                    {
                        chkrow.Checked = true;
                    }
                }
            }
        }
        else
        {
            chk.Checked = false;
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {

                Label lbldocno = (Label)GridViewEBRCEntryList.Rows[i].FindControl("lblIRMNo");
                CheckBox chkrow = (CheckBox)GridViewEBRCEntryList.Rows[i].FindControl("RowChkAllow");
                if (dt.Rows[i]["Checker_Status"].ToString() == "A")
                {
                    chkrow.Enabled = false;
                    chkrow.Checked = false;
                }
            }            
        }
        chk.Focus();
    }
    protected void RowChkAllow_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkbox = (CheckBox)sender;
        GridViewRow row = (GridViewRow)checkbox.NamingContainer;

        if (checkbox.Checked == true)
        {
            checkbox.Focus();
        }
        else
        {
            checkbox.Focus();
        }


        CheckBox chk = (CheckBox)GridViewEBRCEntryList.HeaderRow.FindControl("HeaderChkAllow");
        int isAllChecked = 0;
        for (int i = 0; i < GridViewEBRCEntryList.Rows.Count; i++)
        {
            CheckBox chkrow = (CheckBox)GridViewEBRCEntryList.Rows[i].FindControl("RowChkAllow");
            if (chkrow.Checked == true)
                isAllChecked = 1;
            else
            {
                isAllChecked = 0;
                break;
            }
        }

        if (isAllChecked == 1)
        {
            chk.Checked = true;
            chkapproveall.Checked = true;
        }
        else
        {
            chk.Checked = false;
            chkapproveall.Checked = false;
        }

    }
    protected void btnapprove_Click(object sender, EventArgs e)
    {
        HiddenField hdnuniq = new HiddenField();
        System.Threading.Thread.Sleep(5000);
        string _result = "";
        TF_DATA objData = new TF_DATA();
        string _query = "TF_EBRC_ITTEUC_Update_CheckerApprove";
        string count_ = "";
        Label lblIRMNo = new Label();
        Label lblIRMSTATUS = new Label();
        int count = 0;
        int noofrecordinexcel = 0;
        string data = "";
        Label lblormno = new Label();
        string result_;
        string uniquetxid;
        SqlParameter docno1 = new SqlParameter("@docno", SqlDbType.VarChar);
        SqlParameter uniqid = new SqlParameter("@uniqid", SqlDbType.VarChar);
        string _qry = "TF_EBRC_ITT_EUC_GenerateUniqueTxId";
        SqlParameter p0 = new SqlParameter("@branchcode", ddlBranch.SelectedValue);
        noofrecordinexcel = GridViewEBRCEntryList.Rows.Count;
        for (int i = 0; i < GridViewEBRCEntryList.Rows.Count; i++)
        {
            CheckBox chkrow = (CheckBox)GridViewEBRCEntryList.Rows[i].FindControl("RowChkAllow");
            if (chkrow.Checked == true)
            {
                DataTable dt = objData.getData(_qry, p0);
                uniquetxid = dt.Rows[0]["bktxid"].ToString().Trim();
                hdnuniq.Value = uniquetxid;
                lblIRMNo = (Label)GridViewEBRCEntryList.Rows[i].FindControl("lblIRMNo");
                docno1.Value = lblIRMNo.Text;
                uniqid.Value = hdnuniq.Value;
                string username = Session["userName"].ToString();
                SqlParameter user = new SqlParameter("@user", username.ToUpper());
                _result = objData.SaveDeleteData(_query, docno1);
                if (_result == "Approved")
                {
                    count++;
                    count_ = count.ToString();
                    SqlParameter J1 = new SqlParameter("@BranchCode", ddlBranch.SelectedValue);
                    SqlParameter J2 = new SqlParameter("@irmno", lblIRMNo.Text);
                    SqlParameter J3 = new SqlParameter("@irmstatus", "F");
                    SqlParameter J4 = new SqlParameter("@uniquetxid", uniquetxid);
                    result_ = objData.SaveDeleteData("TF_EBRC_ITT_EUC_BulkJsonInsert", J1, J2, J3, J4);
                    string result__ = objData.SaveDeleteData("TF_EBRC_ITTEUC_MAINTabUpdate", J2, J4);
                    JsonCreation(uniquetxid, count_);
                }
            }            
        }
        if (noofrecordinexcel == count)
        {
            string msg = count + " records are Aporoved";
            string aspx = "TF_EBRC_ITTEUC_Cheker.aspx";
            var page = HttpContext.Current.CurrentHandler as Page;
            ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('" + msg + "');window.location ='" + aspx + "';", true);
        }
        else
        {
            string msg = count + " records are Aporoved";
            string aspx = "TF_EBRC_ITTEUC_Cheker.aspx";
            var page = HttpContext.Current.CurrentHandler as Page;
            ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('" + msg + "');window.location ='" + aspx + "';", true);
        }
    }
    protected void JsonCreation(string uniquetxid, string count_)
    {        
        string id = uniquetxid;
        string count = count_;
        ITTCreation ITTCreation = new ITTCreation();
        irmList ITTObj = new irmList();
        ITTCreation.uniqueTxId = id;
        
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@uniqutxid", id);
        
        DataTable dt = objData.getData("TF_EBRC_ITT_EUC_JSONFileCreationGrid", p1);

        List<irmList> ITTList1 = new List<irmList>();
        ITTList1 = (from DataRow dr in dt.Rows

                    select new irmList()
                    {
                        bankRefNumber = dr["BankRefNo"].ToString().Trim(),
                        irmIssueDate = dr["IRMIssueDate"].ToString().Trim(),
                        irmNumber = dr["DOC_NO"].ToString().Trim(),
                        irmStatus = dr["IRMSTATUS"].ToString().Trim(),
                        ifscCode = dr["IFSC_Code"].ToString().Trim(),
                        remittanceAdCode = dr["RemittanceADCode"].ToString().Trim(),
                        remittanceDate = dr["PaymentDate"].ToString().Trim(),
                        remittanceFCC = dr["CURR"].ToString().Trim(),
                        remittanceFCAmount = Math.Round(System.Convert.ToDecimal(dr["FCAMOUNT"].ToString().Trim()), 2),
                        inrCreditAmount = Math.Round(System.Convert.ToDecimal(dr["INRCreditAmount"].ToString().Trim()), 2),
                        iecCode = dr["IE_CODE"].ToString().Trim(),
                        panNumber = dr["PanNumber"].ToString().Trim(),
                        remitterName = dr["REMITTER_NAME"].ToString().Trim(),
                        remitterCountry = dr["COUNTRY_OF_AC_HOLDER"].ToString().Trim(),
                        purposeOfRemittance = dr["PCODE"].ToString().Trim(),
                        bankAccountNo = dr["BANKACCOUNTNO"].ToString().Trim()
                    }).ToList();


        ITTCreation.irmList = ITTList1;

        var json = JsonConvert.SerializeObject(ITTCreation,
    new JsonSerializerSettings()
    {
        NullValueHandling = NullValueHandling.Ignore,
        Formatting = Formatting.Indented,
        Converters = { new Newtonsoft.Json.Converters.StringEnumConverter() }
    });
        string JSONresult = JsonConvert.SerializeObject(ITTCreation);

        JObject jsonFormat = JObject.Parse(JSONresult);

        string todaydt = System.DateTime.Now.ToString("ddMMyyyy");

        string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/IRMJSON/") + todaydt;
        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }
        String FileNameStatus = "F";

        string FileName = uniquetxid + "_" + FileNameStatus;
        string _filePath = _directoryPath + "/" + FileName + ".json";
        //SqlParameter j1 = new SqlParameter("@JSON_FileOutput", json);
        //SqlParameter j2 = new SqlParameter("@Date_Time", System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        //SqlParameter j3 = new SqlParameter("@JSON_Filename", FileName);
        //string query = "TF_EBRC_ITTEUC_jsonOutputDecrypted";
        //string res = objData.SaveDeleteData(query, j1, j2, j3);
        using (var tw = new StreamWriter(_filePath, true))
        {
            tw.WriteLine(json);
            tw.Close();
        }
    }
}
public class irmList
{
    public string bankRefNumber { get; set; }
    public string irmIssueDate { get; set; }
    public string irmNumber { get; set; }
    public string irmStatus { get; set; }
    public string ifscCode { get; set; }
    public string remittanceAdCode { get; set; }
    public string remittanceDate { get; set; }
    public string remittanceFCC { get; set; }
    public decimal remittanceFCAmount { get; set; }
    public decimal inrCreditAmount { get; set; }
    public string iecCode { get; set; }
    public string panNumber { get; set; }
    public string remitterName { get; set; }
    public string remitterCountry { get; set; }
    public string purposeOfRemittance { get; set; }
    //public string modeOfPayment;
    public string bankAccountNo { get; set; }
    public irmList()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}
public class ITTCreation
{
    public string uniqueTxId;
    //public ormList ormList;
    public List<irmList> irmList = new List<irmList>();
    public ITTCreation()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}