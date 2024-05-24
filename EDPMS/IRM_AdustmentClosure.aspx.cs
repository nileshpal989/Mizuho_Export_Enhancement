using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class EDPMS_IRM_AdustmentClosure : System.Web.UI.Page
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

                if (Session["userRole"].ToString() == "Supervisor")
                {
                   // btnSave.Enabled = false;
                    lblSupervisormsg.Visible = true;
                    txt_irmno.Focus();
                }

                else
                {
                    btnSave.Enabled = true;
                    lblSupervisormsg.Visible = false;
                }



                if (Request.QueryString["mode"].ToString() == "add")
                {
                    fillCurrency();
                    hdnyr.Value = Request.QueryString["year"].ToString();
                    

                  
                }

                else
                {
                    fillCurrency();
                    string irmno = Request.QueryString["irmnono"].ToString();
                    filldetails(irmno);
                    txt_irmno.Enabled = false;
                    btncusthelp.Enabled = false;
                }


                btncusthelp.Attributes.Add("onclick", "return OpenTTNoList();");
                btn_reasonhelp.Attributes.Add("onclick", "return adj();");
                btnSave.Attributes.Add("onclick", "return validate();");
                //txt_adjamt.Attributes.Add("onblur", "return amt();");
                
            }

        }
      
    }

    protected void filldetails(string irmno)
    {
        //string _script = "";
        string query = "fillirmclosure";
        TF_DATA objdata = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@IRMNO", irmno);
        DataTable dt = objdata.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            txt_irmno.Text = dt.Rows[0]["IRMNO"].ToString();
            txt_adcode.Text = dt.Rows[0]["AdCode"].ToString();
            txt_iecode.Text = dt.Rows[0]["IECode"].ToString();
            ddlcurr.SelectedValue = dt.Rows[0]["Currency"].ToString().Trim();
            if ( dt.Rows[0]["AmountAdjusted"].ToString()!="")
            {
                txt_adjamt.Text =Convert.ToDecimal(dt.Rows[0]["AmountAdjusted"]).ToString("0.00");
            }
            else
            {
                txt_adjamt.Text = dt.Rows[0]["AmountAdjusted"].ToString();
            }
         
            txt_letterNo.Text = dt.Rows[0]["LetterNo"].ToString();
            txt_adjdate.Text = dt.Rows[0]["AdjsutmentDate"].ToString();
            txt_adjreason.Text = dt.Rows[0]["Reason"].ToString();
            txt_docno.Text = dt.Rows[0]["DocumentNo"].ToString();
            txt_docdate.Text = dt.Rows[0]["DocDate"].ToString();
            txt_recport.Text = dt.Rows[0]["DocPort"].ToString();
            txt_closseqno.Text = dt.Rows[0]["ClosureSeqNo"].ToString();
            ddlremclose.Text = dt.Rows[0]["CloseofRemInd"].ToString();
            txt_remarks.Text = dt.Rows[0]["Remark"].ToString();
            ddlapproved.Text = dt.Rows[0]["ApprovedBy"].ToString();
           

        }
        
    
    }

    protected void fillCurrency()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlcurr.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "Select";
            ddlcurr.DataSource = dt.DefaultView;
            ddlcurr.DataTextField = "C_Code";
            ddlcurr.DataValueField = "C_Code";
            ddlcurr.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlcurr.Items.Insert(0, li);

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _script = "";
        string query = "Save_IRMClosure";
        TF_DATA objdata = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@IRMNO",txt_irmno.Text);
        SqlParameter p2 = new SqlParameter("@AdCode", txt_adcode.Text);
        SqlParameter p3 = new SqlParameter("@IECode", txt_iecode.Text);
        SqlParameter p4 = new SqlParameter("@Currency", ddlcurr.Text);
        SqlParameter p5 = new SqlParameter("@AmountAdjusted", txt_adjamt.Text);
        SqlParameter p6 = new SqlParameter("@LetterNo", txt_letterNo.Text);
        SqlParameter p7 = new SqlParameter("@AdjsutmentDate", txt_adjdate.Text);
        SqlParameter p8 = new SqlParameter("@Reason", txt_adjreason.Text);
        SqlParameter p9 = new SqlParameter("@DocumentNo", txt_docno.Text);
        SqlParameter p10 = new SqlParameter("@DocDate", txt_docdate.Text);
        SqlParameter p11 = new SqlParameter("@DocPort", txt_recport.Text);
        SqlParameter p12 = new SqlParameter("@RecordInd", "1");
        SqlParameter p13 = new SqlParameter("@ClosureSeqNo", txt_closseqno.Text);
        SqlParameter p14 = new SqlParameter("@CloseofRemInd", ddlremclose.Text);
        SqlParameter p15 = new SqlParameter("@Remark", txt_remarks.Text);
        SqlParameter p16 = new SqlParameter("@ApprovedBy", ddlapproved.Text);
        SqlParameter p17 = new SqlParameter("@AddedBy", Session["userName"].ToString());
        SqlParameter p18 = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        SqlParameter p19 = new SqlParameter("@UpdatedBy", Session["userName"].ToString());
        SqlParameter p20 = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

        string result = objdata.SaveDeleteData(query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20);

        if (result=="inserted")
        {
            _script = "window.location='View_IRM_AdustmentClosure.aspx?result=" + result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);

            // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);
        }

        else
        {
            _script = "window.location='View_IRM_AdustmentClosure.aspx?result=" + result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("View_IRM_AdustmentClosure.aspx");
    }
    protected void txt_irmno_TextChanged(object sender, EventArgs e)
    {
        string query = "Filladie";
        TF_DATA objdata = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@IRMNO", txt_irmno.Text);
        filldetails(txt_irmno.Text);
        DataTable dt = objdata.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            txt_adcode.Text = dt.Rows[0]["adcode"].ToString();
            txt_iecode.Text = dt.Rows[0]["iecode"].ToString();
            ddlcurr.Focus();
        
        }

        else
        {

            txt_adcode.Text = "";
            txt_iecode.Text = "";
            txt_irmno.Focus();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("View_IRM_AdustmentClosure.aspx");
    }
    protected void txt_closseqno_TextChanged(object sender, EventArgs e)
    {
        string query = "SeqNoCheck";

         TF_DATA objdata = new TF_DATA();
       
       
        string result = objdata.SaveDeleteData(query);

        if (result == "exist")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('This Closure Sequence No is Already Exists');", true);
            txt_closseqno.Text = "";
            txt_closseqno.Focus();
        }

        else
        {
            ddlremclose.Focus();
        }

        

    }
}