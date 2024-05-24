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

public partial class IMP_HelpForms_TF_IMP_SG_CustHelp : System.Web.UI.Page
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
        SqlParameter p1 = new SqlParameter("@search", txtSearch.Text.Trim());
        SqlParameter p2 = new SqlParameter("@CustAccNo", Request.QueryString["CustAccNo"].ToString());
        
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData("TF_IMP_GetSGCustList", p1, p2);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridviewSG_Custlist.DataSource = dt.DefaultView;
            GridviewSG_Custlist.DataBind();
            GridviewSG_Custlist.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else
        {
            GridviewSG_Custlist.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }

    }
    protected void GridviewSG_Custlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridviewSG_Custlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblAccode = new Label();
            Label lblCustAbb = new Label();
            Label lblAcno = new Label();
            Label lblDescription = new Label();
            lblAccode = (Label)e.Row.FindControl("lblAccode");
            lblCustAbb = (Label)e.Row.FindControl("lblCustAbb");
            lblAcno = (Label)e.Row.FindControl("lblAcno");
            lblDescription = (Label)e.Row.FindControl("lblDescription");

            int i = 0;
            string pageurl = "";
            foreach (TableCell cell in e.Row.Cells)
            {
                if (Request.QueryString["Debit_Credit"].Trim() == "Credit")
                {
                    switch (Request.QueryString["IMP_ACC"].Trim())
                    {
                        case "IMPACC1":
                            pageurl = "window.opener.select_CR_Code1('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "IMPACC2":
                            pageurl = "window.opener.select_CR_Code2('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "IMPACC3":
                            pageurl = "window.opener.select_CR_Code3('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "IMPACC4":
                            pageurl = "window.opener.select_CR_Code4('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "IMPACC5":
                            pageurl = "window.opener.select_CR_Code5('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "GO4":
                            pageurl = "window.opener.select_GO4_CR_Code('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;

                        // General Operations
                        case "GO1Left1":
                            pageurl = "window.opener.select_GO1Left1('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "GO1Left2":
                            pageurl = "window.opener.select_GO1Left2('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "GO1Right1":
                            pageurl = "window.opener.select_GO1Right1('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "GO1Right2":
                            pageurl = "window.opener.select_GO1Right2('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "GO2Left1":
                            pageurl = "window.opener.select_GO2Left1('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "GO2Left2":
                            pageurl = "window.opener.select_GO2Left2('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "GO2Right1":
                            pageurl = "window.opener.select_GO2Right1('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "GO2Right2":
                            pageurl = "window.opener.select_GO2Right2('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "GO3Left1":
                            pageurl = "window.opener.select_GO3Left1('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "GO3Left2":
                            pageurl = "window.opener.select_GO3Left2('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "GO3Right1":
                            pageurl = "window.opener.select_GO3Right1('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "GO3Right2":
                            pageurl = "window.opener.select_GO3Right2('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                    }
                }
                if (Request.QueryString["Debit_Credit"].Trim() == "Debit")
                {
                    switch (Request.QueryString["IMP_ACC"].Trim())
                    {
                        case "IMPACC1":
                            pageurl = "window.opener.select_DR_Code1('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "IMPACC2":
                            pageurl = "window.opener.select_DR_Code2('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "IMPACC3":
                            pageurl = "window.opener.select_DR_Code3('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "IMPACC4":
                            pageurl = "window.opener.select_DR_Code4('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "IMPACC5":
                            pageurl = "window.opener.select_DR_Code5('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "GO4":
                            pageurl = "window.opener.select_GO4_DR_Code('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                    }
                }


                if (i != 7)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void GridviewSG_Custlist_RowCreated(object sender, GridViewRowEventArgs e)
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