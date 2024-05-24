using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class IDPMS_TF_OtherBOE_Data_Entry : System.Web.UI.Page
{
    static string _mode;
    static string branch, Document_no, Document_date, bill_no, bill_date, port_code;
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
                _mode = Request.QueryString["mode"].ToString();
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;
                txtBOENo.Focus();

                if (Session["userRole"].ToString() == "Supervisor")
                {

                    lblSupervisormsg.Visible = true;
                    //v//  txtBOENo.Focus();
                }

                else
                {
                    btnSave.Enabled = true;
                    lblSupervisormsg.Visible = false;
                }



                if (Request.QueryString["mode"].ToString() == "add")
                {

                    //hdnyr.Value = Request.QueryString["year"].ToString();



                }

                else
                {
                    //v//
                    string DocNo = Request.QueryString["DocNo"].ToString();
                    filldetails(DocNo);



                }

                //v//
                txtBOEDate.Attributes.Add("onblur", "return isValidDate(" + txtBOEDate.ClientID + ", 'BOE Date');");
                btnSave.Attributes.Add("onclick", "return validate();");
                btnporthelp.Attributes.Add("onclick", "return PortHelp();");

            }

        }

    }

    protected void filldetails(string DocNo)
    {
        //string _script = "";
        string query = "Fill_Other_Ad";
        TF_DATA objdata = new TF_DATA();

        //SqlParameter p1 = new SqlParameter("@BOENo", BOENO);
        //DataTable dt = objdata.getData(query, p1);
        //if (dt.Rows.Count > 0)
        //{

        //    txtBOENo.Text = dt.Rows[0]["BOENo"].ToString();
        //    txtBOEDate.Text = dt.Rows[0]["BOEDate"].ToString();
        //    txtPortCode.Text = dt.Rows[0]["BOEPortCode"].ToString();
        //    ddlBranch.SelectedValue = dt.Rows[0]["BranchCode"].ToString();

        //}
        SqlParameter p1 = new SqlParameter("@DocNo", DocNo);
        DataTable dt = objdata.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            ddlBranch.SelectedValue = dt.Rows[0]["BranchCode"].ToString();
            branch = dt.Rows[0]["BranchCode"].ToString();

            txtBOENo.Text = dt.Rows[0]["BOENo"].ToString();
            bill_no = dt.Rows[0]["BOENo"].ToString();

            txtBOEDate.Text = dt.Rows[0]["BOEDate"].ToString();
            bill_date = dt.Rows[0]["BOEDate"].ToString();

            txtPortCode.Text = dt.Rows[0]["BOEPortCode"].ToString();
            port_code = dt.Rows[0]["BOEPortCode"].ToString();
            txtPortCode_TextChanged(null,null);

            txtdocno.Text = dt.Rows[0]["Doc_No"].ToString();
            Document_no = dt.Rows[0]["Doc_No"].ToString();

            txtdocdate.Text = dt.Rows[0]["Doc_Date"].ToString();
            Document_date = dt.Rows[0]["Doc_Date"].ToString();
        }
    }

    //chnges S
    protected void generateDocNo()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@year", System.DateTime.Now.Year.ToString());
        SqlParameter p2 = new SqlParameter("@branch", ddlBranch.SelectedValue);

        string query = "Generate_OtherBOE_DocNo";
        DataTable dt = obj.getData(query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            txtdocno.Text = dt.Rows[0]["LastdocNo"].ToString();
            txtdocdate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        }
    }

    //chngs E


    protected void btnSave_Click(object sender, EventArgs e)
    {


        string _script = "";
        string query = "Add_Edit_Other_Ad";
        TF_DATA objdata = new TF_DATA();

        string OldValue = "Branch:" + branch + ";Document No:" + Document_no + ";Document Date:" + Document_date +
            ";Bill of Entry No:" + bill_no + ";Bill of Entry Date:" + bill_date + ";Port Code:" + port_code;

        string NewValue = "Branch:" + ddlBranch.SelectedValue.ToString().Trim() + ";Document No:" + txtdocno.Text.Trim() +
            ";Document Date:" + txtdocdate.Text.Trim();



        SqlParameter P1 = new SqlParameter("@branchCode", SqlDbType.VarChar);
        P1.Value = ddlBranch.SelectedValue.ToString();

        SqlParameter P2 = new SqlParameter("@BOENo", SqlDbType.VarChar);
        P2.Value = txtBOENo.Text.ToString().Trim();

        SqlParameter P3 = new SqlParameter("@BOEDate", SqlDbType.VarChar);
        P3.Value = txtBOEDate.Text.ToString().Trim();


        SqlParameter P4 = new SqlParameter("@BOEPortCode", SqlDbType.VarChar);
        P4.Value = txtPortCode.Text.ToString().Trim();

        SqlParameter P5 = new SqlParameter("@addedBy", SqlDbType.VarChar);
        P5.Value = Session["userName"].ToString();

        SqlParameter P6 = new SqlParameter("@AddedDate", SqlDbType.DateTime);
        P6.Value = System.DateTime.Now.ToString();

        SqlParameter P7 = new SqlParameter("@updatedBy", SqlDbType.VarChar);
        P7.Value = Session["userName"].ToString();

        SqlParameter P8 = new SqlParameter("@updateddate", SqlDbType.DateTime);
        P8.Value = System.DateTime.Now.ToString();
        //v// S

        SqlParameter p9 = new SqlParameter("@DocNo", SqlDbType.VarChar);
        p9.Value = txtdocno.Text;

        SqlParameter p10 = new SqlParameter("@DocDate", SqlDbType.VarChar);
        p10.Value = txtdocdate.Text;



        string result = objdata.SaveDeleteData(query, P1, P2, P3, P4, P5, P6, P7, P8, p9, p10);

        if (result == "inserted")
        {

            #region AUDIT TRAIL LOGIC

            NewValue = NewValue + ";Bill of Entry No:" + txtBOENo.Text.Trim() + ";Bill of Entry Date:" + txtBOEDate.Text.Trim()
                + ";Port Code:" + txtPortCode.Text.Trim();

            SqlParameter Q1 = new SqlParameter("@Adcode", ddlBranch.SelectedValue.ToString());
            SqlParameter Q2 = new SqlParameter("@OldValues", "");
            SqlParameter Q3 = new SqlParameter("@NewValues", NewValue);
            SqlParameter Q4 = new SqlParameter("@CustAcNo", "");
            SqlParameter Q5 = new SqlParameter("@DocumentNo", txtdocno.Text);
            SqlParameter Q6 = new SqlParameter("@DocumentDate", txtdocdate.Text);
            SqlParameter Q7 = new SqlParameter("@Mode", "A");
            SqlParameter Q8 = new SqlParameter("@ModifiedBy", Session["userName"].ToString());
            SqlParameter Q9 = new SqlParameter("@MenuName", "Bill Of Entry - Other AD Data Entry");

            result = objdata.SaveDeleteData("TF_IDPMS_Audit_Trail_Add", Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9);
            #endregion

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);
            clear();
            //_script = "window.location='View_TF_OtherBOE_Data_Entry.aspx?result=" + result + "'";
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);

        }

        else
        {

            #region AUDIT TRAIL LOGIC

            if (bill_no != txtBOENo.Text.Trim())
            {
                NewValue = NewValue + ";Bill of Entry No:" + txtBOENo.Text.Trim();
            }
            if (bill_date != txtBOEDate.Text.Trim())
            {
                NewValue = NewValue + ";Bill of Entry Date:" + txtBOEDate.Text.Trim();
            }
            if (port_code != txtPortCode.Text.Trim())
            {
                NewValue = NewValue + ";Port Code:" + txtPortCode.Text.Trim();
            }


            SqlParameter Q1 = new SqlParameter("@Adcode", ddlBranch.SelectedValue.ToString());
            SqlParameter Q2 = new SqlParameter("@OldValues", OldValue);
            SqlParameter Q3 = new SqlParameter("@NewValues", NewValue);
            SqlParameter Q4 = new SqlParameter("@CustAcNo", "");
            SqlParameter Q5 = new SqlParameter("@DocumentNo", txtdocno.Text);
            SqlParameter Q6 = new SqlParameter("@DocumentDate", txtdocdate.Text);
            SqlParameter Q7 = new SqlParameter("@Mode", "M");
            SqlParameter Q8 = new SqlParameter("@ModifiedBy", Session["userName"].ToString());
            SqlParameter Q9 = new SqlParameter("@MenuName", "Bill Of Entry - Other AD Data Entry");

            result = objdata.SaveDeleteData("TF_IDPMS_Audit_Trail_Add", Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9);
            #endregion

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
            clear();
        }


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("View_TF_OtherBOE_Data_Entry.aspx");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("View_TF_OtherBOE_Data_Entry.aspx");
    }

    private void clear()
    {
        txtBOENo.Text = "";
        txtBOEDate.Text = "";
        txtPortCode.Text = "";
        lblPrtcde.Text = "";
        generateDocNo();
    }
    //protected void txtBOENo_TextChanged(object sender, EventArgs e)
    //{
    //    string query = "FillBOEDetais";
    //    TF_DATA objdata = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@BOENo", txtBOENo.Text);
    //    filldetails(txtBOENo.Text);
    //    DataTable dt = objdata.getData(query, p1);
    //    if (dt.Rows.Count > 0)
    //    {
    //        txtBOEDate.Text = dt.Rows[0]["BOEDate"].ToString();
    //        txtPortCode.Text = dt.Rows[0]["BOEPortCode"].ToString();
    //        ddlBranch.SelectedValue = dt.Rows[0]["BranchCode"].ToString();
    //    }

    //    else
    //    {

    //        txtBOEDate.Text = "";
    //        txtPortCode.Text = "";
    //        txtBOENo.Focus();
    //    }
    //}
    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        //ListItem li = new ListItem();
        //li.Value = "All Branches";

        if (dt.Rows.Count > 0)
        {
            //li.Text = "All Branches";

            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();

        }
        generateDocNo();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        generateDocNo();
    }
    protected void txtdocno_TextChanged(object sender, EventArgs e)
    {
        string query = "FillBOEDetais";
        TF_DATA objdata = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@DocNo", txtdocno.Text);
        filldetails(txtdocno.Text);
        DataTable dt = objdata.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            txtBOENo.Text = dt.Rows[0]["BOENo"].ToString();
            txtBOEDate.Text = dt.Rows[0]["BOEDate"].ToString();
            txtPortCode.Text = dt.Rows[0]["BOEPortCode"].ToString();
            ddlBranch.SelectedValue = dt.Rows[0]["BranchCode"].ToString();
        }

        else
        {

            txtBOEDate.Text = "";
            txtPortCode.Text = "";
            //txtBOENo.Focus();
        }
    }
    protected void txtPortCode_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objdata = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@portcode", txtPortCode.Text);
        DataTable dt = objdata.getData("TF_GetPortCodeMasterDetails",p1);
        if (dt.Rows.Count > 0)
        {
            lblPrtcde.Text = dt.Rows[0]["portName"].ToString();
        }
    }
}
