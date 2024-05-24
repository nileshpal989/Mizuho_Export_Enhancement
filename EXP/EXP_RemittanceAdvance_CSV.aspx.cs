using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Data;

public partial class EXP_EXP_RemittanceAdvance_CSV : System.Web.UI.Page
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

                ddlBranch.SelectedValue = Session["userLBCode"].ToString();
                ddlBranch.Enabled = false;
                fillBranch();
                txtDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                rbtAllDocNo.Attributes.Add("onclick", "return toggledisplay();");
                rbtSelectedDocNo.Attributes.Add("onclick", "return toggledisplay();");
            }
            btnGenerate.Attributes.Add("onclick", "return validate_save();");
            //ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "return toggledisplay();", true);
        }
    }

    public void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", "");
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
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

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        string _ColumnHeader = "", _RowData = "";
        string a = Session["userADCode"].ToString();
        string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/" + a);
        string Doc = "";

        if (rbtAllDocNo.Checked == true)
        {
            Doc = "ALL";
        }
        else
            Doc = txtDocno.Text;

        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }

        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@Docno", Doc);
        SqlParameter p2 = new SqlParameter("@date", txtDate.Text.Trim());
        SqlParameter p3 = new SqlParameter("@Branch", ddlBranch.SelectedValue.ToString());

        string query = "TF_EXP_RemittanceAdvance_CSV";
        DataTable dt = objData.getData(query, p1, p2,p3);

        if (dt.Rows.Count > 0)
        {
            string date = txtDate.Text.Substring(0, 2) + "" + txtDate.Text.Substring(3, 2) + "" + txtDate.Text.Substring(6, 4);

            string _filePath = _directoryPath + "/Export Document against Advance Remittance" + "-" + date + ".csv";
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            for (int c = 0; c < dt.Columns.Count; c++)
            {
                _ColumnHeader = _ColumnHeader + dt.Columns[c].ColumnName.ToString() + ",";
            }

            sw.WriteLine(_ColumnHeader);
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int r = 0; r < dt.Columns.Count; r++)
                {
                    _RowData = _RowData + dt.Rows[j][r].ToString().Replace(",", "").ToString() + ",";
                }

                sw.WriteLine(_RowData.ToString());
                _RowData = "";
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();

            TF_DATA objServerName = new TF_DATA();
            string _serverName = objServerName.GetServerName();
            string path = "file://" + _serverName + "/TF_GeneratedFiles/EXPORT";
            string link = "/TF_GeneratedFiles/EXPORT";
            labelMessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Files Created Successfully');", true);
        }
        else
        {
            //txtDate.Text = "";

            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('No Record Found');", true);
        }
        if (rbtAllDocNo.Checked == true)
            DC.Attributes.Add("style", "display:none");
        else
            DC.Attributes.Add("style", "display:block");
    }
    protected void txtDocno_TextChanged(object sender, EventArgs e)
    {
        DC.Attributes.Add("style", "display:block");
    }
}