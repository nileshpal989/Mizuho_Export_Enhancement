using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.IO;

public partial class IMPW_IMPW_UPLOADSUPPLIERMASTER : System.Web.UI.Page
{
    int norecinexcel, cntrec;
    string result;
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
                ddlBranch.SelectedIndex = 1;
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;

                lblUploadMsg.Text = "";
                lblValMsg.Text = "";
                lblProcessMsg.Text = "";
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
        {
            li.Text = "No record(s) found";
        }
        ddlBranch.Items.Insert(0, li);
    }

    protected void btnupload_Click(object sender, EventArgs e)
    {
        string result = "";
        lblUploadMsg.Text = "";
        lblValMsg.Text = "";
        lblProcessMsg.Text = "";

        TF_DATA objdata = new TF_DATA();
        string query = "TF_IMPW_SupplierMaster_FileUploadDelete";
        SqlParameter ADCODE = new SqlParameter("@ADCODE", Session["userADCode"].ToString());
        result = objdata.SaveDeleteData(query, ADCODE);

        string Ext = System.IO.Path.GetExtension(FileUpload1.FileName);

        string c = FileUpload1.FileName;

        if (FileUpload1.HasFile)
        {
            string fname;
            fname = FileUpload1.FileName;
            //if (fname.Contains(".xls") == false && fname.Contains(".xlsx") == false)
            if (Ext != ".xls" && Ext != ".xlsx")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Please Upload Excel File Only !!')", true);
                FileUpload1.Focus();
            }
            else
            {
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string FolderPath = Server.MapPath("~/Uploaded_Files");

                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }

                FileName = FileName.Replace(" ", "");

                string FilePath = FolderPath + "\\" + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(FilePath);
                GetExcelSheets(FilePath, Extension, "No");

                txtInputFile.Text = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                //query = "TF_IDPMS_GET_DOCNO_SETTLEMENT_TEMP";
                //result = objdata.SaveDeleteData(query, ADCODE);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Please Select Excel File First !!')", true);
            FileUpload1.Focus();
        }

        lblValMsg.Text = "";
        lblProcessMsg.Text = "";
    }

    private void GetExcelSheets(string FilePath, string Extension, string isHDR)
    {
        string conStr = "";

        string Ref_no = "", AD_Code = "", Amount = "", Currency = "", payment_date = "", IE_Code = "", IE_Name = "", IE_Address = "", IE_Pan_Number = "", CG = "", Ben_name = "", Ben_accno = "", Ben_country = "", Swift_code = "", Purpose_code = "", Record_indicator = "", Remarks = "", Payment_Terms = "",
            Seq_No = "",
             UploadedBy = "", UploadedDate = "";

        switch (Extension)
        {
            case ".xls": //Excel 97-03
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                         .ConnectionString;
                break;
            case ".xlsx": //Excel 07
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                         .ConnectionString;
                break;
        }

        //    //Get the Sheets in Excel WorkBook
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

        cmdExcel.CommandText = "SELECT F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13 From [" + SheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();

        SqlParameter Adcode = new SqlParameter("@Adcode", SqlDbType.VarChar);
        SqlParameter Cust_ACNo = new SqlParameter("@Cust_ACNo", SqlDbType.VarChar);
        SqlParameter Supplier_ID = new SqlParameter("@Supplier_ID", SqlDbType.VarChar);
        SqlParameter Supplier_Name = new SqlParameter("@Supplier_Name", SqlDbType.VarChar);
        SqlParameter Supplier_Address = new SqlParameter("@Supplier_Address", SqlDbType.VarChar);
        SqlParameter Supplier_Country = new SqlParameter("@Supplier_Country", SqlDbType.VarChar);
        SqlParameter Bank_SwiftCode = new SqlParameter("@Bank_SwiftCode", SqlDbType.VarChar);
        SqlParameter Bank_Name = new SqlParameter("@Bank_Name", SqlDbType.VarChar);
        SqlParameter Bank_Address = new SqlParameter("@Bank_Address", SqlDbType.VarChar);
        SqlParameter Bank_Country = new SqlParameter("@Bank_Country", SqlDbType.VarChar);
        SqlParameter Supplier_ContactNo = new SqlParameter("@Supplier_ContactNo", SqlDbType.VarChar);
        SqlParameter Supplier_EmailID1 = new SqlParameter("@Supplier_EmailID1", SqlDbType.VarChar);
        SqlParameter Supplier_EmailID2 = new SqlParameter("@Supplier_EmailID2", SqlDbType.VarChar);
        SqlParameter Supplier_EmailID3 = new SqlParameter("@Supplier_EmailID3", SqlDbType.VarChar);
        SqlParameter AddedBy = new SqlParameter("@AddedBy", SqlDbType.VarChar);

        string _query = "TF_IMPW_SupplierMaster_FileUpload";

        if (dt.Rows.Count > 1)
        {
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString().Trim() != "" || dt.Rows[i][1].ToString().Trim() != "")
                {
                    norecinexcel = norecinexcel + 1;

                    Adcode.Value = ddlBranch.SelectedValue.ToString().Trim();
                    Cust_ACNo.Value = dt.Rows[i][0].ToString();
                    Supplier_ID.Value = dt.Rows[i][1].ToString();
                    Supplier_Name.Value = dt.Rows[i][2].ToString();
                    Supplier_Address.Value = dt.Rows[i][3].ToString();
                    Supplier_Country.Value = dt.Rows[i][4].ToString().Trim();
                    Bank_SwiftCode.Value = dt.Rows[i][5].ToString();
                    Bank_Name.Value = dt.Rows[i][6].ToString();
                    Bank_Address.Value = dt.Rows[i][7].ToString();
                    Bank_Country.Value = dt.Rows[i][8].ToString();
                    Supplier_ContactNo.Value = dt.Rows[i][9].ToString().ToUpper().Trim();
                    Supplier_EmailID1.Value = dt.Rows[i][10].ToString().Trim();
                    Supplier_EmailID2.Value = dt.Rows[i][11].ToString().Trim();
                    Supplier_EmailID3.Value = dt.Rows[i][12].ToString().Trim();
                    AddedBy.Value = Session["userName"].ToString().Trim();

                    TF_DATA objSave = new TF_DATA();

                    result = objSave.SaveDeleteData(_query, Adcode, Cust_ACNo, Supplier_ID, Supplier_Name, Supplier_Address, Supplier_Country, Bank_SwiftCode,
                                                    Bank_Name, Bank_Address, Bank_Country, Supplier_ContactNo, Supplier_EmailID1, Supplier_EmailID2, Supplier_EmailID3, AddedBy);

                    if (result == "Uploaded")
                    {
                        cntrec = cntrec + 1;

                        lblUploadMsg.Text = "<b><font color='red'>" + cntrec + "</font> record(s) " + "Uploaded out of <font color='red'>" + norecinexcel + "</font> from file " + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName) + "</b>";
                    }
                }
            }
        }
    }
    protected void btnValidate_Click(object sender, EventArgs e)
    {
        TF_DATA objdata = new TF_DATA();
        string script = "";
        string _adcode = Session["userADCode"].ToString();
        SqlParameter AdCode = new SqlParameter("@AdCode", Session["userADCode"].ToString().Trim());

        string Query = "TF_IMPW_SupplierMaster_FileUpload_Validate";

        DataTable dt = objdata.getData(Query, AdCode);
        if (dt.Rows.Count > 0)
        {
            lblValMsg.Text = "<b><font color='red'>" + dt.Rows.Count.ToString() + "</font> Error Records. </b>";
            script = "window.open('Impw_UploadSupplierMaster_rpt.aspx','_blank','height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);

        }
        else
        {
            script = "alert('No Error Records.')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
            lblValMsg.Text = "<b> No Error Records </b>";
        }
        lblProcessMsg.Text = "";
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        TF_DATA objdata = new TF_DATA();

        SqlParameter AdCode = new SqlParameter("@AdCode", Session["userADCode"].ToString().Trim());

        DataTable dt = objdata.getData("TF_IMPW_SupplierMaster_FileUpload_Validate", AdCode);
        if (dt.Rows.Count > 0)
        {
            lblProcessMsg.Text = "<b> No Records Processed. Please Correct All the Error records first and then Reupload file. </b>";
        }
        else
        {
            string result = objdata.SaveDeleteData("TF_IMPW_SupplierMaster_FileUpload_Process", AdCode);
            if (result.Substring(0, 8) == "Uploaded")
            {
                lblProcessMsg.Text = "<b><font color='red'>" + result.Substring(8) + "</font> Records processed successfully. </b>";
            }
            else
            {
                lblProcessMsg.Text = "<b> No Records Processed. </b>";
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Impw_ViewSupplierMaster.aspx", true);
    }
}