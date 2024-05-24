using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
public partial class IMP_TF_IMP_Opn_Cl_BalanceMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
            Response.Redirect("~/TF_Login.aspx?Sessionout=yes&Sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_IMP_Opn_Cl_BalanceMaster_View.aspx", true);
                }
                else
                {
                    fillCurrency();
                }
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            TF_DATA objSave = new TF_DATA();
            SqlParameter PDocument_Curr = new SqlParameter("@Document_Curr", ddlCurr.SelectedValue.ToString());
            SqlParameter PCl_Bal_Qty = new SqlParameter("@Cl_Bal_Qty", txtBalanceQty.Text.ToString());
            SqlParameter PCl_Bal_Amt = new SqlParameter("@Cl_Bal_Amt", txtCloseBalance.Text.ToString());
            SqlParameter PDate = new SqlParameter("@Document_Date", DateTime.ParseExact(txt_AcceptanceDate.Text.ToString(), "dd/MM/yyyy", null));
            SqlParameter PUserName = new SqlParameter("@AddedBy", Session["userName"].ToString().Trim());
            string result = objSave.SaveDeleteData("TF_IMP_Voucher_Balance_Add", PDocument_Curr, PCl_Bal_Qty, PCl_Bal_Amt, PDate, PUserName);
            string _script = "";
            if (result == "added")
            {
                _script = "window.location='TF_IMP_Opn_Cl_BalanceMaster_View.aspx?result=" + result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
            {
                labelMessage.Text = result;
            }
        }
        catch( Exception ex){
            labelMessage.Text = ex.Message.ToString();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMP_Opn_Cl_BalanceMaster_View.aspx", true);
    }
    protected void ddlCurr_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter PDocument_Curr = new SqlParameter("@Document_Curr",ddlCurr.SelectedValue.ToString());
        string result = objData.SaveDeleteData("TF_IMP_Voucher_Balance_CurrCheck", PDocument_Curr);
        if (result == "exists")
        {
            labelMessage.Text = "Record already exists";
            //ddlCurr.ClearSelection();
        }
        else { labelMessage.Text = ""; }
    }
    protected void fillCurrency()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            DataTable dt = objData.getData("TF_IMP_Currency_List");
            ddlCurr.Items.Clear();
            ListItem li = new ListItem();
            li.Value = "";
            if (dt.Rows.Count > 0)
            {
                li.Text = "Select";
                ddlCurr.DataSource = dt.DefaultView;
                ddlCurr.DataTextField = "C_Code";
                ddlCurr.DataValueField = "C_Code";
                ddlCurr.DataBind();
            }
            else
            {
                li.Text = "No record(s) found";
            }
            ddlCurr.Items.Insert(0, li);
        }
        catch (Exception ex)
        {
            labelMessage.Text = ex.Message.ToString();
        }
    }
}