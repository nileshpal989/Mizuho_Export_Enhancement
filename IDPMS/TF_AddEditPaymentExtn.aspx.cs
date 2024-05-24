using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class IDPMS_TF_AddEditPaymentExtn : System.Web.UI.Page
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

            string year = ""; string BranchName = ""; string custacno = ""; string prtcd = ""; string billno = "";
            if (!IsPostBack)
            {
                if (Session["userRole"].ToString() == "Supervisor")
                {
                    lblSupervisormsg.Visible = true;
                }
                else
                {
                    lblSupervisormsg.Visible = false;
                }

                if (Request.QueryString["CustACNo"] != null)
                {
                    txt_custacno.Text = Request.QueryString["CustACNo"].ToString();
                    if (Request.QueryString["CustACNo"] != null)
                    {
                        txtSearch.Text = Request.QueryString["BOENo"].ToString();
                    }
                    txt_custacno_TextChanged(null, null);
                }
            }

            fillbranch();
            ddlBranch.SelectedValue = Session["userADCode"].ToString();
            ddlBranch.Enabled = false;
        }
    }

    protected void fillbranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        if (dt.Rows.Count > 0)
        {
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
    }

    protected void fillcust()
    {
        string query = "HelpCustSearchId1";
        TF_DATA objdata = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@id", txt_custacno.Text);
        DataTable dt = objdata.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            txt_custacno.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString();
            lbl_custname.Text = dt.Rows[0]["CUST_NAME"].ToString();
        }
        else
        {
            lbl_custname.Text = "Invalid Customer A/C No.";
            txt_custacno.Focus();
        }
    }

    protected void fillgrid()
    {
        int _pageSize = 0;
        string search = ddlBranch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@branch", SqlDbType.VarChar);
        p1.Value = search;

        SqlParameter p4 = new SqlParameter("@custid", SqlDbType.VarChar);
        p4.Value = txt_custacno.Text.Trim();

        SqlParameter p5 = new SqlParameter("@search", SqlDbType.VarChar);
        p5.Value = txtSearch.Text;

        string query = "PaymentExtnData1";
        TF_DATA objData = new TF_DATA();

        DataTable dt = objData.getData(query, p1, p4, p5);

        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewpaymentextn.PageSize = _pageSize;
            GridViewpaymentextn.DataSource = dt.DefaultView;
            GridViewpaymentextn.DataBind();
            GridViewpaymentextn.Visible = true;
            rowPager.Visible = true;
            lblmessage.Visible = false;
            pagination(_records, _pageSize);
        }

        else
        {

            GridViewpaymentextn.Visible = false;
            rowPager.Visible = false;
            lblmessage.Text = "No record(s) found.";
            //labelMessage.Visible = true;
        }



    }

    protected void GridViewpaymentextn_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
        string _prtcd = "";
        string billno = "";
        string billdate = "";
        string CustAccNo = txt_custacno.Text;

        Button lb = (Button)e.CommandSource;
        string extby = ((DropDownList)lb.Parent.FindControl("ddl_extby")).Text;
        string letterno = ((TextBox)lb.Parent.FindControl("txt_letterno")).Text;
        string letterdate = ((TextBox)lb.Parent.FindControl("txt_letterdate")).Text;
        string extndate = ((TextBox)lb.Parent.FindControl("txt_extndate")).Text;

        string[] values_p;

        string str = e.CommandArgument.ToString();
        if (str != "")
        {
            char[] splitchar = { ',' };
            values_p = str.Split(splitchar);
            _prtcd = values_p[0].ToString();
            billno = values_p[1].ToString();
            billdate = values_p[2].ToString();

        }

        SqlParameter p1 = new SqlParameter("@branchcode", ddlBranch.Text);
        SqlParameter p3 = new SqlParameter("@custacno", txt_custacno.Text);
        SqlParameter p4 = new SqlParameter("@portcode", _prtcd);
        SqlParameter p6 = new SqlParameter("@billno", billno);
        SqlParameter p7 = new SqlParameter("@billdate", billdate);
        SqlParameter p8 = new SqlParameter("@extby", extby);
        SqlParameter p9 = new SqlParameter("@letterno", letterno);
        SqlParameter p10 = new SqlParameter("@letterdate", letterdate);
        SqlParameter p11 = new SqlParameter("@extndate", extndate);
        SqlParameter p12 = new SqlParameter("@addedby", Session["userName"].ToString());
        SqlParameter p13 = new SqlParameter("@addeddate", System.DateTime.Now);
        SqlParameter p14 = new SqlParameter("@updatedby", Session["userName"].ToString());
        SqlParameter p15 = new SqlParameter("@updateddate", System.DateTime.Now);

        if (extby == "1")
        {
            if (letterno == "" && letterdate == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Letter No,Letter Date cant be blank When Extension By is RBI.');", true);
                return;
            }
        }
        else
        {

            TF_DATA objdata = new TF_DATA();
            string qur = "paydatediff";
            SqlParameter pshipdate = new SqlParameter("@shipbilldate", billdate);
            SqlParameter pextndate = new SqlParameter("@extndate", extndate);

            DataTable dt = objdata.getData(qur, pshipdate, pextndate);
            if (dt.Rows[0]["result"].ToString() == "1")
            {
                string _query = "Insertintopaymentextension1";

                result = objdata.SaveDeleteData(_query, p1, p3, p4, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15);
                string newvalue = "Adcode:" + ddlBranch.Text + ";CustomerAc:" + txt_custacno.Text + ";PortCode:" + _prtcd + ";BillNo:" + billno + ";BillDate:" + billdate + ";ExtensionBy:" + extby +
                                ";LetterNo:" + letterno + ";LetterDate:" + letterdate + ";ExtensionDate:" + extndate;
                if (result == "inserted" || result == "updated")
                {
                    #region AUDIT TRAIL LOGIC
                    SqlParameter Q1 = new SqlParameter("@Adcode", ddlBranch.SelectedValue.ToString());
                    SqlParameter Q2 = new SqlParameter("@OldValues", "");
                    SqlParameter Q3 = new SqlParameter("@NewValues", newvalue);
                    SqlParameter Q4 = new SqlParameter("@CustAcNo", txt_custacno.Text);
                    SqlParameter Q5 = new SqlParameter("@DocumentNo", "");
                    SqlParameter Q6 = new SqlParameter("@DocumentDate", "");
                    SqlParameter Q7 = new SqlParameter("@Mode", "A");
                    SqlParameter Q8 = new SqlParameter("@ModifiedBy", Session["userName"].ToString());
                    SqlParameter Q9 = new SqlParameter("@MenuName", "Bill Of Entry - Payment Extension Data Entry");

                    string s = objdata.SaveDeleteData("TF_IDPMS_Audit_Trail_Add", Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9);
                    #endregion

                    if (result == "inserted")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                    }

                    txt_custacno.Text = CustAccNo;
                    txt_custacno_TextChanged(null, null);
                }

            }

            else
            {
                string date = dt.Rows[0]["result"].ToString();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Payment Extension Date Should be greater Than equal to " + date + "');", true);

                TextBox txtextndate = ((TextBox)lb.Parent.FindControl("txt_extndate"));
                txtextndate.Text = "__/__/____";
            }
        }
    }

    protected void GridViewpaymentextn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtlno = (TextBox)e.Row.FindControl("txt_letterno");
            TextBox txtldate = (TextBox)e.Row.FindControl("txt_letterdate");
            TextBox txtextndate = (TextBox)e.Row.FindControl("txt_extndate");

            Button btnDelete = (Button)e.Row.FindControl("btnadd");

            var a = txtlno.Text;
            var b = txtldate.Text;
            var c = txtextndate.Text;
        }
    }

    protected void txt_custacno_TextChanged(object sender, EventArgs e)
    {
        fillcust();
        fillgrid();
    }

    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewpaymentextn.PageCount != GridViewpaymentextn.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewpaymentextn.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewpaymentextn.PageIndex + 1) + " of " + GridViewpaymentextn.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewpaymentextn.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewpaymentextn.PageIndex != (GridViewpaymentextn.PageCount - 1))
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
        GridViewpaymentextn.PageIndex = 0;
        fillgrid();
    }

    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewpaymentextn.PageIndex > 0)
        {
            GridViewpaymentextn.PageIndex = GridViewpaymentextn.PageIndex - 1;
        }

        fillgrid();
    }

    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewpaymentextn.PageIndex != GridViewpaymentextn.PageCount - 1)
        {
            GridViewpaymentextn.PageIndex = GridViewpaymentextn.PageIndex + 1;
        }
        fillgrid();
    }

    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewpaymentextn.PageIndex = GridViewpaymentextn.PageCount - 1;
        fillgrid();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
}
