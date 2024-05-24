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


public partial class EXP_HelpForms_TF_EXP_SundryaccountHelp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            sundryAc_Rdbtn.Checked = true;
            fillGrid();
        }
        txtSearch.Focus();
    }
    protected void fillGrid()
    {
        string search = txtSearch.Text.Trim();
        string AccType = "";
        if (sundryAc_Rdbtn.Checked == true)
        {
            AccType = "Sundry";
        }

        else if (InteroffAc_Rdbtn.Checked == true)
        {
            AccType = "InterOffice";
        }
        else if (rdb_NostroAc.Checked == true)
        {
            AccType = "Nostro";
        }
        else if (rdb_CustAc.Checked == true)
        {
            AccType = "CustAc";
        }
        else if (rbd_SuspenseAC.Checked == true)
        {
            AccType = "SuspAc";
        }
        string BRcode = Request.QueryString["BRCode"].Trim();
        string ACcode = Request.QueryString["ACCode"].Trim();
        string IMPACC = Request.QueryString["IMP_ACC"].Trim();
        string IMPCURR = Request.QueryString["Curr"].Trim();
        string IMPCRD  =Request.QueryString["Debit_Credit"].Trim();

        SqlParameter p1 = new SqlParameter("@search", search);
        SqlParameter p2 = new SqlParameter("@AccType", AccType);
        SqlParameter p3 = new SqlParameter("@BRcode", BRcode);
        SqlParameter p4 = new SqlParameter("@ACcode", ACcode);
        SqlParameter p5 = new SqlParameter("@IMPACC", IMPACC);
        SqlParameter p6 = new SqlParameter("@IMPCURR", IMPCURR);
        SqlParameter p7 = new SqlParameter("@IMPCRD", IMPCRD);

        //BranchName
        string _query = "TF_EXP_GetSundryHelp";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2, p3, p4, p5, p6, p7);
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
        BranchVisibility();
    }
    protected void GridviewSundryAccount_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridviewSundryAccount_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblAccode = new Label();
            Label lblCustAbb = new Label();
            Label lblAcno = new Label();
            Label lblDescription = new Label();
            Label lblCCY = new Label();
            lblAccode = (Label)e.Row.FindControl("lblAccode");
            lblCustAbb = (Label)e.Row.FindControl("lblCustAbb");
            lblAcno = (Label)e.Row.FindControl("lblAcno");
            lblDescription = (Label)e.Row.FindControl("lblDescription");
            lblCCY = (Label)e.Row.FindControl("lblCCY");

            int i = 0;
            string pageurl = "";
            foreach (TableCell cell in e.Row.Cells)
            {
                if (Request.QueryString["Debit_Credit"].Trim() == "Credit")
                {
                    switch (Request.QueryString["IMP_ACC"].Trim())
                    {
                        case "IMPACC1":
                            pageurl = "window.opener.select_CR_Code1('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "','" + lblCCY.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "IMPACC2":
                            pageurl = "window.opener.select_CR_Code2('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "','" + lblCCY.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "IMPACC3":
                            pageurl = "window.opener.select_CR_Code3('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "','" + lblCCY.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "IMPACC4":
                            pageurl = "window.opener.select_CR_Code4('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "','" + lblCCY.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "IMPACC5":
                            pageurl = "window.opener.select_CR_Code5('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "','" + lblCCY.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "GO4":
                            pageurl = "window.opener.select_GO4_CR_Code('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "','" + lblCCY.Text + "');window.opener.EndRequest();window.close();";
                            break;

                        // General Operations
                        case "GO1Left1":
                            pageurl = "window.opener.select_GO1Left1('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "GO1Left2":
                            pageurl = "window.opener.select_GO1Left2('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "GO1Right1":
                            pageurl = "window.opener.select_GO1Right1('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "','" + lblCCY.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "GO1Right2":
                            pageurl = "window.opener.select_GO1Right2('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "','" + lblCCY.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "GO2Left1":
                            pageurl = "window.opener.select_GO2Left1('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "GO2Left2":
                            pageurl = "window.opener.select_GO2Left2('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "GO2Right1":
                            pageurl = "window.opener.select_GO2Right1('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "','" + lblCCY.Text + "');window.opener.EndRequest();window.close();";
                            break;
                        case "GO2Right2":
                            pageurl = "window.opener.select_GO2Right2('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "','" + lblCCY.Text + "');window.opener.EndRequest();window.close();";
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
                            pageurl = "window.opener.select_GO4_DR_Code('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "','" + lblDescription.Text + "','" + lblCCY.Text + "');window.opener.EndRequest();window.close();";
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
    protected void sundryAc_Rdbtn_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
        txtSearch.Text = "";
    }

    protected void BranchVisibility()
    {
        string BRcode = Request.QueryString["BRCode"].Trim();
        if (Request.QueryString["Debit_Credit"].Trim() == "Credit")
        {
            if (BRcode == "Mumbai")
            {
                switch (Request.QueryString["IMP_ACC"].Trim())
                {
                    case "IMPACC1":
                        InteroffAc_Rdbtn.Enabled = false;
                        break;
                    case "IMPACC2":
                        InteroffAc_Rdbtn.Enabled = false;
                        break;
                    case "IMPACC3":
                        InteroffAc_Rdbtn.Enabled = false;
                        break;
                    case "IMPACC4":
                        InteroffAc_Rdbtn.Enabled = false;
                        break;
                    case "IMPACC5":
                        InteroffAc_Rdbtn.Enabled = false;
                        break;
                    case "GO1Right1":
                        InteroffAc_Rdbtn.Enabled = false;
                        break;
                    case "GO1Right2":
                        InteroffAc_Rdbtn.Enabled = false;
                        break;
                    case "GO2Right1":
                        InteroffAc_Rdbtn.Enabled = false;
                        break;
                    case "GO2Right2":
                        InteroffAc_Rdbtn.Enabled = false;
                        break;
                }
            }
            else
            {
                switch (Request.QueryString["IMP_ACC"].Trim())
                {
                    case "IMPACC1":
                        rdb_NostroAc.Enabled = false;
                        break;
                    case "IMPACC2":
                        rdb_NostroAc.Enabled = false;
                        break;
                    case "IMPACC3":
                        rdb_NostroAc.Enabled = false;
                        break;
                    case "IMPACC4":
                        rdb_NostroAc.Enabled = false;
                        break;
                    case "IMPACC5":
                        rdb_NostroAc.Enabled = false;
                        break;
                }
            }
        }
        if (Request.QueryString["Debit_Credit"].Trim() == "Debit")
        {
            if (BRcode == "Mumbai")
            {
                switch (Request.QueryString["IMP_ACC"].Trim())
                {
                    //case "IMPACC1":
                    //    InteroffAc_Rdbtn.Enabled = false;
                    //    break;
                    //case "IMPACC2":
                    //    InteroffAc_Rdbtn.Enabled = false;
                    //    break;
                    //case "IMPACC3":
                    //    InteroffAc_Rdbtn.Enabled = false;
                    //    break;
                    //case "IMPACC4":
                    //    InteroffAc_Rdbtn.Enabled = false;
                    //    break;
                    //case "IMPACC5":
                    //    InteroffAc_Rdbtn.Enabled = false;
                    //    break;
                    //case "GO1Right1":
                    //    InteroffAc_Rdbtn.Enabled = false;
                    //    break;
                    //case "GO1Right2":
                    //    InteroffAc_Rdbtn.Enabled = false;
                    //    break;
                    //case "GO2Right1":
                    //    InteroffAc_Rdbtn.Enabled = false;
                    //    break;
                    //case "GO2Right2":
                    //    InteroffAc_Rdbtn.Enabled = false;
                    //    break;
                }
            }
            else
            {
                switch (Request.QueryString["IMP_ACC"].Trim())
                {
                    case "IMPACC1":
                        rdb_NostroAc.Enabled = false;
                        break;
                    case "IMPACC2":
                        rdb_NostroAc.Enabled = false;
                        break;
                    case "IMPACC3":
                        rdb_NostroAc.Enabled = false;
                        break;
                    case "IMPACC4":
                        rdb_NostroAc.Enabled = false;
                        break;
                    case "IMPACC5":
                        rdb_NostroAc.Enabled = false;
                        break;
                }
            }
        }
    }

}