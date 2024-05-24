using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class EXP_HelpForms_TF_EXP_SundryaccountHelp1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGrid();
        }
        txtSearch.Focus();
    }
    protected void fillGrid()
    {
        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData("TF_EXP_GLCodeMaster_List", p1);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridviewSundryAccount.DataSource = dt.DefaultView;
            GridviewSundryAccount.DataBind();
            GridviewSundryAccount.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else
        {
            GridviewSundryAccount.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }

    }
    protected void GridviewSundryAccount_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridviewSundryAccount_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblGLcode = new Label();
            Label lblDescription = new Label();
            Label lblAcno = new Label();
            lblGLcode = (Label)e.Row.FindControl("lblGLcode");
            lblDescription = (Label)e.Row.FindControl("lblDescription");
            lblAcno = (Label)e.Row.FindControl("lblAcno");

            int i = 0;
            string pageurl = "";
            foreach (TableCell cell in e.Row.Cells)
            {
                if (Request.QueryString["IMP_ACC"].Trim() == "GO1")
                {
                    switch (Request.QueryString["Debit_Credit"].Trim())
                    {
                        case "Debit1":
                            pageurl = "window.opener.select_GO1_Debit1('" + lblGLcode.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "Debit2":
                            pageurl = "window.opener.select_GO1_Debit2('" + lblGLcode.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "Debit3":
                            pageurl = "window.opener.select_GO1_Debit3('" + lblGLcode.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "Debit4":
                            pageurl = "window.opener.select_GO1_Debit4('" + lblGLcode.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                    }
                }
                if (Request.QueryString["IMP_ACC"].Trim() == "GO2")
                {
                    switch (Request.QueryString["Debit_Credit"].Trim())
                    {
                        case "Debit1":
                            pageurl = "window.opener.select_GO2_Debit1('" + lblGLcode.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "Debit2":
                            pageurl = "window.opener.select_GO2_Debit2('" + lblGLcode.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "Debit3":
                            pageurl = "window.opener.select_GO2_Debit3('" + lblGLcode.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "Debit4":
                            pageurl = "window.opener.select_GO2_Debit4('" + lblGLcode.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                    }
                }
                if (Request.QueryString["IMP_ACC"].Trim() == "GO3")
                {
                    switch (Request.QueryString["Debit_Credit"].Trim())
                    {
                        case "Debit1":
                            pageurl = "window.opener.select_GO3_Debit1('" + lblGLcode.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "Debit2":
                            pageurl = "window.opener.select_GO3_Debit2('" + lblGLcode.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "Debit3":
                            pageurl = "window.opener.select_GO3_Debit3('" + lblGLcode.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "Debit4":
                            pageurl = "window.opener.select_GO3_Debit4('" + lblGLcode.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                    }
                }
                if (Request.QueryString["IMP_ACC"].Trim() == "GO4")
                {
                    switch (Request.QueryString["Debit_Credit"].Trim())
                    {
                        case "Debit1":
                            pageurl = "window.opener.select_GO4_Debit1('" + lblGLcode.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "Debit2":
                            pageurl = "window.opener.select_GO4_Debit2('" + lblGLcode.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                    }
                }
                if (i != 3)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void GridviewSundryAccount_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TabIndex = -1;
            e.Row.Attributes["onmouseover"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onmouseout"] = string.Format("javascript:DisSelectRow(this, {0});", e.Row.RowIndex);

            e.Row.ToolTip = "Click to select row.";
        }
    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
}