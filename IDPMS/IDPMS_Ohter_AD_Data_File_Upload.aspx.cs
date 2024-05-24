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


public partial class IDPMS_IDPMS_Ohter_AD_Data_File_Upload : System.Web.UI.Page
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
        else if (!IsPostBack)
        {
            fillBranch();
            ddlBranch.SelectedIndex = 1;
            ddlBranch.SelectedValue = Session["userADCode"].ToString();
            ddlBranch.Enabled = false;
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
        string _query;
        TF_DATA objdata = new TF_DATA();
        _query = "Other_AD_BOE_FileUploadDelete";
        result = objdata.SaveDeleteData(_query);

        if (FileUpload1.HasFile)
        {
            string Filename;
            Filename = FileUpload1.FileName;

            if (Filename.Contains(".xls") == false && Filename.Contains("xlsx") == false)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Please upload Only Excel file')", true);
                FileUpload1.Focus();
                txtFileName.Text = "";
                lblupload.Text = "";
                lblvalidation.Text = "";
                lblprocess.Text = "";

            }
            else
            {
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string FolderPath = Server.MapPath("~/Uploaded_Files/IDPMS/Other_AD");

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

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('please upload Excel File First')", true);
            txtFileName.Text = "";
            lblupload.Text = "";
            lblvalidation.Text = "";
            lblprocess.Text = "";
            FileUpload1.Focus();
        }
    }


    private void GetExcelSheets(string FilePath, string Extension, string isHDR)
    {
        string conStr = "",
            BOE_No = "", BOE_Date = "", Port_Code = "", BranchCode = "", Addedby = "", Doc_Date = "";
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

        cmdExcel.CommandText = "SELECT F1,F2,F3 From [" + SheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();

        SqlParameter p1 = new SqlParameter("@ADCode", SqlDbType.VarChar);
        SqlParameter p2 = new SqlParameter("@BOENo", SqlDbType.VarChar);
        SqlParameter p3 = new SqlParameter("@BOEDate", SqlDbType.VarChar);
        SqlParameter p4 = new SqlParameter("@BOEPortCode", SqlDbType.VarChar);
        SqlParameter p5 = new SqlParameter("@Addedby", SqlDbType.VarChar);
        //SqlParameter p6 = new SqlParameter("@Doc_Date", SqlDbType.VarChar);

        string _query = "Other_AD_BOE_FileUpload";

        if (dt.Rows.Count > 1)
        {
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString().Trim() != "")
                {
                    norecinexcel = norecinexcel + 1;

                    BranchCode = Session["userADCode"].ToString();
                    BOE_No = dt.Rows[i][0].ToString();
                    BOE_Date = dt.Rows[i][1].ToString();
                    Port_Code = dt.Rows[i][2].ToString();
                    Addedby = Session["userName"].ToString();

                    p1.Value = BranchCode;
                    p2.Value = BOE_No;
                    p3.Value = BOE_Date;
                    p4.Value = Port_Code;
                    p5.Value = Addedby;
                    //p6.Value = Doc_Date;


                    TF_DATA objSave = new TF_DATA();
                    result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5);

                    if (result == "Uploaded")
                    {
                        cntrec = cntrec + 1;
                        lblupload.Text = "<b><font color='red'>" + cntrec + "</font> Record(s) " + "Uploaded Out Of <font color='red'>" + norecinexcel + "</font> From File " + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName) + "</b>";
                        lblvalidation.Text = "";
                        lblprocess.Text = "";
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
        DataTable dt = objdata.getData("Other_AD_BOE_FileUpload_Validate", p1);
        if (dt.Rows.Count > 0)
        {
            script = "window.open('IDPM_rpt_DataValidation_Other_AD.aspx','_blank','height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
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
        DataTable dt = objdata1.getData("Other_AD_BOE_FileUpload_Validate", p1);
        if (dt.Rows.Count > 0)
        {
            lblprocess.Text = "<b><font color='red'>No Records Processed. Please Correct All The Error Records First And Then Reupload File..!</b>";
            //script = "alert('No Records Processed. Please Correct All the Error records first and then Reupload file..!')";
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
        }
        else
        {
            TF_DATA objdata = new TF_DATA();
            string result = objdata.SaveDeleteData("Other_AD_BOE_FileUpload_Process",p1);
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