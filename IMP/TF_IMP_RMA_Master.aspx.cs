using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.OleDb;
using System.IO;

public partial class IMP_TF_IMP_RMA_Master : System.Web.UI.Page
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
                ddlrecordperpage.SelectedValue = "20";
                fillGrid();
            }
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(10000);
        TF_DATA objData = new TF_DATA();
        DataTable dt = new DataTable();

        if (fileinhouse.HasFile)
        {
            string fname;
            fname = fileinhouse.FileName;
            //string path = Server.MapPath(fileinhouse.PostedFile.FileName);
            txtInputFile.Text = fileinhouse.PostedFile.FileName;
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
            GetCSVSheet();
            fillGrid();
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

            cmdExcel.CommandText = "SELECT F1,F2,F3,F4,F5,F6,F7 FROM [" + SheetName + "]";

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
                        SqlParameter POwnBIC = new SqlParameter("@OwnBIC", dt.Rows[i][0]);
                        SqlParameter PCorrespondent = new SqlParameter("@Correspondent", dt.Rows[i][1]);
                        SqlParameter PRStatus = new SqlParameter("@RStatus", dt.Rows[i][2]);
                        SqlParameter PRRestr = new SqlParameter("@RRestr", dt.Rows[i][3]);
                        SqlParameter PSStatus = new SqlParameter("@SStatus", dt.Rows[i][4]);
                        SqlParameter PSRestr = new SqlParameter("@SRestr", dt.Rows[i][5]);
                        SqlParameter PCorrespondentName = new SqlParameter("@CorrespondentName", dt.Rows[i][6]);
                        SqlParameter PAddedBy = new SqlParameter("@AddedBy", Session["userName"].ToString());
                        string Result = objData.SaveDeleteData("TF_IMP_UploadRMAMaster", POwnBIC, PCorrespondent, PRStatus, PRRestr, PSStatus, PSRestr, PCorrespondentName, PAddedBy);
                        if (Result == "Inserted")
                        {
                            cnt++;
                        }
                        else
                        {

                        }
                        cntTot++;
                    }
                }
                labelMessage.Text = "<font color='red'>" + cnt + "</font>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Records Uploaded out of " + "<font color='red'>" + cntTot + "</font>" + " from file " + System.IO.Path.GetFileName(fileinhouse.PostedFile.FileName);
                string record = cnt + " records uploaded out of " + cntTot + " records from the file " + System.IO.Path.GetFileName(fileinhouse.PostedFile.FileName);

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "uploadfile", "VAlert('" + record + "')", true);
            }
        }
        catch (Exception Ex)
        {
            labelMessage.Text = Ex.Message;
        }
    }
    private void GetCSVSheet()
    {
        try
        {
            
            StreamReader sr = new StreamReader(fileinhouse.FileContent);
            string line, fname;
            int cnt = 0, cntTot = 0;
            //fname = fileinhouse.FileName;
            fname = fileinhouse.FileName;
            if (fname.Contains(".csv") == false && fname.Contains(".CSV") == false)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CSV Input Data File Upload at Branch", "alert('Please upload only CSV file.')", true);
                fileinhouse.Focus();
            }
            else
            {
                TF_DATA objDataInput = new TF_DATA();
                string _result = objDataInput.SaveDeleteData("TF_IMP_DeleteRMAMaster");

                line = sr.ReadLine().ToString();
                while (sr.EndOfStream == false)
                {
                    line = sr.ReadLine().ToString();

                    string _OWNBIC = "";
                    _OWNBIC = line.Split(',')[0].Trim();
                    SqlParameter POwnBIC = new SqlParameter("@OwnBIC", SqlDbType.VarChar);
                    POwnBIC.Value = _OWNBIC;
                    
                    string _Corre = "";
                    _Corre = line.Split(',')[1].Trim();
                    SqlParameter PCorrespondent = new SqlParameter("@Correspondent", SqlDbType.VarChar);
                    PCorrespondent.Value = _Corre;

                    string _RStatus = "";
                    _RStatus = line.Split(',')[2].Trim();
                    SqlParameter PRStatus = new SqlParameter("@RStatus", SqlDbType.VarChar);
                    PRStatus.Value = _RStatus;

                    string _RRES = "";
                    _RRES = line.Split(',')[3].Trim();
                    SqlParameter PRRestr = new SqlParameter("@RRestr", SqlDbType.VarChar);
                    PRRestr.Value = _RRES;

                    string _SStatus = "";
                    _SStatus = line.Split(',')[4].Trim();
                    SqlParameter PSStatus = new SqlParameter("@SStatus", SqlDbType.VarChar);
                    PSStatus.Value = _SStatus;

                    string _SRES = "";
                    _SRES = line.Split(',')[5].Trim();
                    SqlParameter PSRestr = new SqlParameter("@SRestr", SqlDbType.VarChar);
                    PSRestr.Value = _SRES;

                    string _CorreName = "";
                    _CorreName = line.Split(',')[6].Trim();
                    SqlParameter PCorrespondentName = new SqlParameter("@CorrespondentName", SqlDbType.VarChar);
                    PCorrespondentName.Value = _CorreName;

                    SqlParameter PAddedBy = new SqlParameter("@AddedBy", Session["userName"].ToString());
                    string Result = objDataInput.SaveDeleteData("TF_IMP_UploadRMAMaster", POwnBIC, PCorrespondent, PRStatus, PRRestr, PSStatus, PSRestr, PCorrespondentName, PAddedBy);
                    if (Result == "Inserted")
                    {
                        cnt++;
                    }
                    else
                    {

                    }
                    cntTot++;                    
                }
                labelMessage.Text = "<font color='red'>" + cnt + "</font>" + " Records Uploaded Successfully";
            }
            labelMessage.Text = "<font color='red'>" + cnt + "</font>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Records Uploaded out of " + "<font color='red'>" + cntTot + "</font>" + " from file " + System.IO.Path.GetFileName(fileinhouse.PostedFile.FileName);
            string record = cnt + " records uploaded out of " + cntTot + " records from the file " + System.IO.Path.GetFileName(fileinhouse.PostedFile.FileName);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "uploadfile", "VAlert('" + record + "')", true);
        }
        catch (Exception Ex)
        {
            labelMessage.Text = Ex.Message;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    { fillGrid(); }
    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewRMA.PageCount != GridViewRMA.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewRMA.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewRMA.PageIndex + 1) + " of " + GridViewRMA.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewRMA.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewRMA.PageIndex != (GridViewRMA.PageCount - 1))
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
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewRMA.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewRMA.PageIndex > 0)
        {
            GridViewRMA.PageIndex = GridViewRMA.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewRMA.PageIndex != GridViewRMA.PageCount - 1)
        {
            GridViewRMA.PageIndex = GridViewRMA.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewRMA.PageIndex = GridViewRMA.PageCount - 1;
        fillGrid();
    }
    protected void fillGrid()
    {
        string search = txtSearch.Text.Trim();
        TF_DATA objData = new TF_DATA();
        string _query = "TF_IMP_RMAFillGrid";

        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewRMA.PageSize = _pageSize;
            GridViewRMA.DataSource = dt.DefaultView;
            GridViewRMA.DataBind();
            GridViewRMA.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            txtSearch.Visible = true;
            btnSearch.Visible = true;
            lblSearch.Visible = true;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewRMA.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
            lblSearch.Visible = true;
            txtSearch.Visible = true;
            btnSearch.Visible = true;
        }
    }
}