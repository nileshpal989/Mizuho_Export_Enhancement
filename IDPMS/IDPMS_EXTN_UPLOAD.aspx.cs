using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;

public partial class IDPMS_IDPMS_EXTN_UPLOAD : System.Web.UI.Page
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
                lblprocess.Text = "";
                lblupload.Text = "";
                lblvalidation.Text = "";
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
    protected void btnupload_Click(object sender, EventArgs e)
    {
        string result = "", _query = "";

        TF_DATA objdata = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@AddedAdCode", SqlDbType.VarChar);
        p1.Value = Session["userADCode"].ToString().Trim()
            ;
        _query = "TF_IDPMS_Extn_Upload_Delete";
        result = objdata.SaveDeleteData(_query, p1);

        if (FileUpload1.HasFile)
        {
            string fname;
            fname = FileUpload1.FileName;
            if (fname.Contains(".xls") == false && fname.Contains(".xlsx") == false)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Please upload only Excel file.')", true);
                txtFileName.Text = "";
                lblupload.Text = "";
                lblvalidation.Text = "";
                lblprocess.Text = "";
                FileUpload1.Focus();
            }

            else
            {
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string FolderPath = Server.MapPath("~/Uploaded_Files/IDPMS/Payment_Extn");

                txtFileName.Text = FileName.ToString();
                lblupload.Text = "";
                lblvalidation.Text = "";
                lblprocess.Text = "";

                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }

                FileName = FileName.Replace(" ", "");

                string FilePath = FolderPath + "\\" + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(FilePath);
                GetExcelSheets(FilePath, Extension, "No");
            }
        }

        else
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Please upload Excel file first...')", true);
            txtFileName.Text = "";
            lblupload.Text = "";
            lblvalidation.Text = "";
            lblprocess.Text = "";
            FileUpload1.Focus();
        }
    }

    private void GetExcelSheets(string FilePath, string Extension, string isHDR)
    {
        string conStr = "";
        string portOfDischarge = "", billOfEntryNumber = "", billOfEntryDate = "", IECode = "", ADCode = "", extenstionBy = "", letterNumber = "", letterDate = "",
               Addedby = "", extensionDate = "";
        //  remarks = "", Addedby = "", Addeddate = "", Updatedby = "", Updateddate = "";


        switch (Extension)
        {
            case ".xls":
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;

            case ".xlsx":
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                break;
        }

        conStr = string.Format(conStr, FilePath, isHDR);

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

        cmdExcel.CommandText = "SELECT F1,F2,FORMAT(F3,'dd/MM/yyyy') as F3,F4,F5,F6,F7,FORMAT(F8,'dd/MM/yyyy') as F8 From [" + SheetName + "]";
        //cmdExcel.CommandText = "SELECT F1,F2,F3,F4,F5,F6,F7,F8 From [" + SheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();

        SqlParameter p1 = new SqlParameter("@portOfDischarge", SqlDbType.VarChar);
        SqlParameter p2 = new SqlParameter("@billOfEntryNumber", SqlDbType.VarChar);
        SqlParameter p3 = new SqlParameter("@billOfEntryDate", SqlDbType.VarChar);
        SqlParameter p4 = new SqlParameter("@IECode", SqlDbType.VarChar);
        SqlParameter p5 = new SqlParameter("@ADCode", SqlDbType.VarChar);
        SqlParameter p6 = new SqlParameter("@extenstionBy", SqlDbType.VarChar);
        SqlParameter p7 = new SqlParameter("@letterNumber", SqlDbType.VarChar);
        SqlParameter p8 = new SqlParameter("@letterDate", SqlDbType.VarChar);
        SqlParameter p9 = new SqlParameter("@extensionDate", SqlDbType.VarChar);
        SqlParameter p10 = new SqlParameter("@Addedby", SqlDbType.VarChar);
        string _query = "TF_IDPMS_Extn_Upload";

        if (dt.Rows.Count > 1)
        {
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString().Trim() != "")
                {
                    norecinexcel = norecinexcel + 1;

                    portOfDischarge = dt.Rows[i][0].ToString();
                    billOfEntryNumber = dt.Rows[i][1].ToString();
                    billOfEntryDate = dt.Rows[i][2].ToString();
                    IECode = dt.Rows[i][3].ToString();
                    ADCode = Session["userADCode"].ToString();
                    extenstionBy = dt.Rows[i][4].ToString();
                    letterNumber = dt.Rows[i][5].ToString();
                    letterDate = dt.Rows[i][6].ToString();
                    extensionDate = dt.Rows[i][7].ToString();
                    Addedby = Session["userName"].ToString();

                    p1.Value = portOfDischarge;
                    p2.Value = billOfEntryNumber;
                    p3.Value = billOfEntryDate;
                    p4.Value = IECode;
                    p5.Value = ADCode;
                    p6.Value = extenstionBy;
                    p7.Value = letterNumber;
                    p8.Value = letterDate;
                    p9.Value = extensionDate;
                    p10.Value = Addedby;

                    TF_DATA objSave = new TF_DATA();
                    result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);

                    if (result == "Uploaded")
                    {
                        cntrec = cntrec + 1;
                        lblupload.Text = "<b><font color='red'>" + cntrec + "</font> record(s) " + "Uploaded out of <font color='red'>" + norecinexcel + "</font> from file " + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName) + "</b>";

                    }
                }
            }
        }
        else
        {
            lblupload.Text = "No Records Found.";
            lblvalidation.Text = "";
            lblprocess.Text = "";
        }
    }

    protected void btnValidate_Click(object sender, EventArgs e)
    {
        TF_DATA objdata = new TF_DATA();
        string script = "";
        SqlParameter p1 = new SqlParameter("@ADCode", Session["userADCode"].ToString());
        DataTable dt = objdata.getData("TF_IDPMS_Extn_Upload_Validate", p1);
        if (dt.Rows.Count > 0)
        {
            script = "window.open('IDPM_rpt_DataValidation_Pay_Extension.aspx','_blank','height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
            lblvalidation.Text = "<b><font color='red'>" + dt.Rows.Count + " Error Record(s)</b>";
            lblprocess.Text = "";
        }
        else
        {
            lblvalidation.Text = "No Error Record.";
            lblprocess.Text = "";
            //script = "alert('No Error Records')";
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
        }
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        TF_DATA objdata1 = new TF_DATA();
        string script = "";
        SqlParameter p1 = new SqlParameter("@ADCode", Session["userADCode"].ToString());
        DataTable dt = objdata1.getData("TF_IDPMS_Extn_Upload_Validate", p1);
        if (dt.Rows.Count > 0)
        {
            lblprocess.Text = "<b><font color='red'>No Records Processed. Please Correct All The Error Records First And Then Reupload File..!</b>";
            //script = "alert('No Records Processed. Please Correct All the Error records first and then Reupload file..!')";
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
        }
        else
        {
            TF_DATA objdata = new TF_DATA();
            string result = objdata.SaveDeleteData("TF_IDPMS_Extn_Upload_Process", p1);
            if (result.Substring(0, 8) == "Uploaded")
            {
                lblprocess.Text = result.Substring(8) + " Records processed successfully.";
            }
            else
            {
                lblprocess.Text = "No Records Processed.";
            }
        }
    }
}