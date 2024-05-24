using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class IDPMS_IDPMS_BOE_Setlement_Cancel : System.Web.UI.Page
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
                if (Request.QueryString["mode"] != "Add")
                {
                    fillBranch();
                    ddlBranch.SelectedValue = Session["userADCode"].ToString();
                    ddlBranch.Enabled = false;
                    txtPayrefNo.Text = Request.QueryString["PayRefNo"].ToString();
                    fillDetails(Request.QueryString["PayRefNo"]);
                    fillgrid();

                }

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

        if (dt.Rows.Count > 0)
        {

            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }

    }
    private void fillDetails(string DocNo)
    {
        TF_DATA obj = new TF_DATA();
        string query = "TF_IDPMS_Cancel_GetSettleDetails";
        SqlParameter p1 = new SqlParameter("@PayRefNo", SqlDbType.VarChar);
        p1.Value = txtPayrefNo.Text;
        DataTable dt = obj.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            txtDocumentNo.Text = dt.Rows[0]["Doc_No"].ToString();
            txtPartyID.Text = dt.Rows[0]["CUST_AC_NO"].ToString();
            txtPartyID_TextChanged(null, null);
            txtDocDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtORMNo.Text = dt.Rows[0]["outwardReferenceNumber"].ToString();
            if (dt.Rows[0]["Party_Code"].ToString() != "")
            {
                ThirdPartyCB.Checked = true;
                ThirdPartyTR.Visible = true;
                txtThirdPartyID.Text = dt.Rows[0]["Party_Code"].ToString();
                txtThirdPartyID_TextChanged(null, null);
            }
            lblcurren.Text = dt.Rows[0]["remittanceCurrency"].ToString();
            txtbillno.Text = dt.Rows[0]["Bill_No"].ToString();
            txtbilldate.Text = dt.Rows[0]["Bill_Date"].ToString();
            txtprtcd.Text = dt.Rows[0]["PortCode"].ToString();

            fillgrid();

        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("IDPMS_BOE_Setlement_Cancel_view.aspx", true);
    }


    protected void txtPartyID_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@customerACNo", SqlDbType.VarChar);
        p1.Value = txtPartyID.Text.Trim();

        SqlParameter p2 = new SqlParameter("@branch", SqlDbType.VarChar);
        p2.Value = ddlBranch.SelectedValue.ToString();
        string _query = "TF_GetCustomerMasterDetails1";


        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            lblCustName.Text = dt.Rows[0]["CUST_NAME"].ToString();
        }
        else
        {
            lblCustName.Text = "";
            txtPartyID.Text = "";

        }


    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("IDPMS_BOE_Setlement_Cancel_view.aspx", true);
    }
    protected void GridViewEDPMS_Bill_Details_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerialNumber = new Label();
            Button btnDelete = new Button();
            lblSerialNumber = (Label)e.Row.FindControl("lblinvoiesrno");

            int i = 0;

            //if (Request.QueryString["mode"] != "Add")
            //{
            //foreach (TableCell cell in e.Row.Cells)
            //{
            //    if (i != 4)
            //        //cell.Attributes.Add("onclick", "return gridClicked(" + lblSerialNumber.Text + ");");
            //    else
            //        cell.Style.Add("cursor", "default");
            //    i++;
            //}
            //}
        }
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();

        string query = "TF_ADD_BOE_Set_Can";
        string result = "";

        SqlParameter p1 = new SqlParameter("@payrefno", txtPayrefNo.Text);

        result = obj.SaveDeleteData(query, p1);

        string NewValue = "Document No:" + txtDocumentNo.Text.Trim() + ";Document Date:" + txtDocDate.Text + ";Customer ID:" + txtPartyID.Text +
                            ";ORM No:" + txtORMNo.Text + ";Bill No:" + txtbillno.Text.Trim() + ";Bill Date:" + txtbilldate.Text.Trim() + ";Port Code:" + txtprtcd.Text.Trim();

        if (result == "Save")
        {
            #region AUDIT TRAIL LOGIC

            query = "TF_IDPMS_Audit_Trail_Add";
            SqlParameter Q1 = new SqlParameter("@Adcode", ddlBranch.SelectedValue.ToString());
            SqlParameter Q2 = new SqlParameter("@OldValues", "");
            SqlParameter Q3 = new SqlParameter("@NewValues", NewValue);
            SqlParameter Q4 = new SqlParameter("@CustAcNo", txtPartyID.Text.Trim());
            SqlParameter Q5 = new SqlParameter("@DocumentNo", txtDocumentNo.Text.Trim());
            SqlParameter Q6 = new SqlParameter("@DocumentDate", txtDocDate.Text.Trim());
            SqlParameter Q7 = new SqlParameter("@Mode", "A");
            SqlParameter Q8 = new SqlParameter("@ModifiedBy", Session["userName"].ToString());
            SqlParameter Q9 = new SqlParameter("@MenuName", "Bill Of Entry - Payment Settlement Cancellation Data Entry");

            result = obj.SaveDeleteData(query, Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9);

            #endregion

            string _script = "window.location='IDPMS_BOE_Setlement_Cancel_view.aspx?result=added'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);

        }


    }

    protected void fillgrid()
    {

        TF_DATA obj = new TF_DATA();
        string query = "TF_IDPMS_Get_BillDetails_Cancel";
        SqlParameter P1 = new SqlParameter("@billno", txtbillno.Text);
        SqlParameter P2 = new SqlParameter("@billdate", txtbilldate.Text);
        SqlParameter P3 = new SqlParameter("@portcode", txtprtcd.Text);
        SqlParameter P4 = new SqlParameter("@PayRefNo", txtPayrefNo.Text);

        DataTable dt = obj.getData(query, P1, P2, P3, P4);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridViewEDPMS_Bill_Details.DataSource = dt.DefaultView;
            ViewState["CurrentTable"] = dt;
            GridViewEDPMS_Bill_Details.DataBind();
            GridViewEDPMS_Bill_Details.Visible = true;
            txtPayAmt_textchange(null, null);
        }
        else
        {
            GridViewEDPMS_Bill_Details.Visible = false;
        }


    }

    protected void txtPayAmt_textchange(object sender, EventArgs e)
    {
        double sumofPayBill = 0;


        for (int i = 0; i < GridViewEDPMS_Bill_Details.Rows.Count; i++)
        {
            if (GridViewEDPMS_Bill_Details.Rows.Count > 0)
            {
                Label txtPamt = new Label();
                txtPamt = (Label)GridViewEDPMS_Bill_Details.Rows[i].Cells[4].FindControl("lblinvcamt");
                double txtPayAmt = Convert.ToDouble(txtPamt.Text);

                sumofPayBill = sumofPayBill + Convert.ToDouble(txtPamt.Text);

            }
        }

        lblTot.Text = "BOE Settled Amont";
        lblTotPayAmt.Text = sumofPayBill.ToString("F");

    }


    protected void ThirdPartyCB_CheckedChanged(object sender, EventArgs e)
    {
        if (ThirdPartyCB.Checked == true)
        {
            ThirdPartyTR.Visible = true;
        }
        else
        {
            ThirdPartyTR.Visible = false;
            txtThirdPartyID.Text = "";
            lblThirdPartyName.Text = "";
        }
    }
    protected void txtThirdPartyID_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@customerACNo", SqlDbType.VarChar);
        p1.Value = txtThirdPartyID.Text.Trim();

        SqlParameter p2 = new SqlParameter("@branch", SqlDbType.VarChar);
        p2.Value = ddlBranch.SelectedValue.ToString();
        string _query = "TF_GetCustomerMasterDetails1";


        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            lblThirdPartyName.Text = dt.Rows[0]["CUST_NAME"].ToString();
        }
        else
        {
            lblThirdPartyName.Text = "";
            txtThirdPartyID.Text = "";

        }
    }
}