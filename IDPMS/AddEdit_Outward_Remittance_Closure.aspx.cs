using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class IDPMS_AddEdit_Outward_Remittance_Closure : System.Web.UI.Page
{
    //static string ormno, adcode, iecode, curr, adjamount, swiftcode, letno, adjdate, letdate, ind, docno, docdate, portdis, seqno, recind, rem, apprby;
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
                generateDocumentNo();
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;
                if (Session["userRole"].ToString() == "Supervisor")
                {
                    lblSupervisormsg.Visible = true;
                    txt_irmno.Focus();
                }

                else
                {
                    btnSave.Enabled = true;
                    lblSupervisormsg.Visible = false;
                }

                fillCurrency();
                
                btnhelp.Attributes.Add("onclick", "return Cust_Help();");
                btncusthelp.Attributes.Add("onclick", "return OTT_Help();");
                btnormCanHelp.Attributes.Add("onclick", " return HelpDocNo1();");
                btnSave.Attributes.Add("onclick", "return validate();");
                txt_adjdate.Attributes.Add("onblur", "return isValidDate(" + txt_adjdate.ClientID + "," + "'Adjustment Date'" + " );");
                txtletterdate.Attributes.Add("onblur", "return isValidDate(" + txtletterdate.ClientID + "," + "'Letter Date'" + " );");
                txt_docdate.Attributes.Add("onblur", "return isValidDate(" + txt_docdate.ClientID + "," + "'BOE Date'" + " );");
            }

        }

    }

    //protected void filldetails(string irmno)
    //{
    //    string query = "Fill_ORM";
    //    TF_DATA objdata = new TF_DATA();

    //    SqlParameter p1 = new SqlParameter("@ORMNo", irmno);
    //    DataTable dt = objdata.getData(query, p1);
    //    if (dt.Rows.Count > 0)
    //    {
    //        txt_irmno.Text = dt.Rows[0]["outwardReferenceNumber"].ToString();
    //        ormno = dt.Rows[0]["outwardReferenceNumber"].ToString();

    //        txt_adcode.Text = dt.Rows[0]["ADCode"].ToString();
    //        adcode = dt.Rows[0]["ADCode"].ToString();

    //        txt_iecode.Text = dt.Rows[0]["IECode"].ToString();
    //        iecode = dt.Rows[0]["IECode"].ToString();

    //        ddlcurr.SelectedValue = dt.Rows[0]["remittanceCurrency"].ToString().Trim();
    //        curr = dt.Rows[0]["remittanceCurrency"].ToString().Trim();

    //        if (dt.Rows[0]["adjustedAmount"].ToString() != "")
    //        {
    //            txt_adjamt.Text = Convert.ToDecimal(dt.Rows[0]["adjustedAmount"]).ToString("0.00");
    //            adjamount = Convert.ToDecimal(dt.Rows[0]["adjustedAmount"]).ToString("0.00");
    //        }
    //        else
    //        {
    //            txt_adjamt.Text = dt.Rows[0]["adjustedAmount"].ToString();
    //            adjamount = dt.Rows[0]["adjustedAmount"].ToString();
    //        }

    //        txtswift.Text = dt.Rows[0]["SWIFT"].ToString();
    //        swiftcode = dt.Rows[0]["SWIFT"].ToString();

    //        txt_letterNo.Text = dt.Rows[0]["letterNumber"].ToString();
    //        letno = dt.Rows[0]["letterNumber"].ToString();

    //        txt_adjdate.Text = dt.Rows[0]["adjustedDate"].ToString();
    //        adjdate = dt.Rows[0]["adjustedDate"].ToString();

    //        txtletterdate.Text = dt.Rows[0]["letterDate"].ToString();
    //        letdate = dt.Rows[0]["letterDate"].ToString();

    //        DdladjustInd.Text = dt.Rows[0]["adjustmentIndicator"].ToString();
    //        ind = dt.Rows[0]["adjustmentIndicator"].ToString();

    //        txt_docno.Text = dt.Rows[0]["documentNumber"].ToString();
    //        docno = dt.Rows[0]["documentNumber"].ToString();

    //        txt_docdate.Text = dt.Rows[0]["documentDate"].ToString();
    //        docdate = dt.Rows[0]["documentDate"].ToString();

    //        txt_recport.Text = dt.Rows[0]["portofDischarge"].ToString();
    //        portdis = dt.Rows[0]["portofDischarge"].ToString();

    //        txt_closseqno.Text = dt.Rows[0]["adjustmentSeqNumber"].ToString();
    //        seqno = dt.Rows[0]["adjustmentSeqNumber"].ToString();

    //        ddlremclose.Text = dt.Rows[0]["RecordInd"].ToString();
    //        recind = dt.Rows[0]["RecordInd"].ToString();

    //        txt_remarks.Text = dt.Rows[0]["Remarks"].ToString();
    //        rem = dt.Rows[0]["Remarks"].ToString();

    //        ddlapproved.Text = dt.Rows[0]["ApproveBy"].ToString();
    //        apprby = dt.Rows[0]["ApproveBy"].ToString();
    //    }
    //}

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
        if (ddlremclose.SelectedIndex == 0)
        {
            string _script = "";
            string query = "Save_ORM_CLR_Data";
            TF_DATA objdata = new TF_DATA();

            //string OldValue = "ORMNo:" + ormno + ";ADCode:" + adcode + ";IECode:" + iecode + ";Curr:" + curr + ";AdjustedAmount:" + adjamount +
            //    ";SwiftCode:" + swiftcode + ";LetterNo:" + letno + ";AdjustedDate:" + adjdate + ";LetterDate:" + letdate +
            //    ";AdjustmentIndicator:" + ind + ";DocumentNumber:" + docno + ";DocumentDate:" + docdate + ";PortOfDischarge:" + portdis +
            //    ";AdjustmentSeqNumber:" + seqno + ";RecordIND:" + recind + ";Remarks:" + rem + ";ApprovedBy:" + apprby;

            string NewValue = "DocNo:" + txtDocNo.Text + ";ORMNo:" + txt_irmno.Text + ";ADCode:" + ddlBranch.SelectedValue.ToString() + ";IECode:" + txt_iecode.Text +
                ";Curr:" + ddlcurr.SelectedValue + ";AdjustedAmount:" + txt_adjamt.Text + ";SwiftCode:" + txtswift.Text +
                ";LetterNo:" + txt_letterNo.Text + ";AdjustedDate:" + txt_adjdate.Text + ";LetterDate:" + txtletterdate.Text +
                ";AdjustmentIndicator:" + DdladjustInd.Text + ";BOE No:" + txt_docno.Text + ";BOE Date:" + txt_docdate.Text +
                ";PortOfDischarge:" + txt_recport.Text + ";AdjustmentSeqNumber:" + txtDocNo.Text + ";RecordIND:" + ddlremclose.Text +
                ";Remarks:" + txt_remarks.Text + ";ApprovedBy:" + ddlapproved.Text;

            SqlParameter p0 = new SqlParameter("@ORC_DOC_NO", txtDocNo.Text);
            SqlParameter p1 = new SqlParameter("@ORMNO", txt_irmno.Text);
            SqlParameter p2 = new SqlParameter("@AdCode", ddlBranch.SelectedValue.ToString());
            SqlParameter p3 = new SqlParameter("@IECode", txt_iecode.Text);
            SqlParameter p4 = new SqlParameter("@remittercur", ddlcurr.Text);
            SqlParameter p5 = new SqlParameter("@AmountAdjusted", txt_adjamt.Text);
            SqlParameter p6 = new SqlParameter("@letterno", txt_letterNo.Text);
            SqlParameter p7 = new SqlParameter("@lettrdate", txtletterdate.Text);
            SqlParameter p8 = new SqlParameter("@adjustdate", txt_adjdate.Text);
            SqlParameter p9 = new SqlParameter("@adjutmentInd", DdladjustInd.Text);
            SqlParameter p10 = new SqlParameter("@documentno", txt_docno.Text);
            SqlParameter p11 = new SqlParameter("@documentDate", txt_docdate.Text);
            SqlParameter p12 = new SqlParameter("@portDischarge", txt_recport.Text);
            SqlParameter p13 = new SqlParameter("@adjusmentseqno", txtDocNo.Text);
            SqlParameter p14 = new SqlParameter("@RecordInd", ddlremclose.Text);
            SqlParameter p15 = new SqlParameter("@remarks", txt_remarks.Text);
            SqlParameter p16 = new SqlParameter("@approveby", ddlapproved.Text);
            SqlParameter p17 = new SqlParameter("@Addedby", Session["userName"].ToString());
            SqlParameter p18 = new SqlParameter("@Addeddate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            SqlParameter p19 = new SqlParameter("@Updatedby", Session["userName"].ToString());
            SqlParameter p20 = new SqlParameter("@Updatedate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            SqlParameter p21 = new SqlParameter("@Swift", txtswift.Text);

            string result = objdata.SaveDeleteData(query, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21);

            if (result == "inserted")
            {
                //Audit Trail Logic
                query = "TF_IDPMS_AddEdit_AuditTrail";

                SqlParameter q1 = new SqlParameter("@ADCode", ddlBranch.SelectedValue.ToString());
                SqlParameter q2 = new SqlParameter("@IECode", txt_iecode.Text);
                SqlParameter q3 = new SqlParameter("@OldValues", "");
                SqlParameter q4 = new SqlParameter("@NewValues", NewValue);
                SqlParameter q5 = new SqlParameter("@DocumentNo", txtDocNo.Text);
                SqlParameter q6 = new SqlParameter("@Mode", "A");
                SqlParameter q7 = new SqlParameter("@ModifiedBy", Session["userName"].ToString());
                SqlParameter q8 = new SqlParameter("@ModifiedDate", "");
                SqlParameter q9 = new SqlParameter("@MenuName", "Outward Remittance Closure");
                string S = objdata.SaveDeleteData(query, q1, q2, q3, q4, q5, q6, q7, q8, q9);

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('ORM Closed.');", true);
                clear();
                generateDocumentNo();

            }

            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
            }
        }
        else
        {
            string querysave = "Save_ORM_CLR_Data_Cancel";
            TF_DATA objsave = new TF_DATA();

            SqlParameter a0 = new SqlParameter("@ORC_DOC_NO", txtDocNo.Text);
            SqlParameter a1 = new SqlParameter("@ORMNO", txt_irmno.Text);
            SqlParameter a2 = new SqlParameter("@AdCode", ddlBranch.SelectedValue.ToString());
            SqlParameter a3 = new SqlParameter("@IECode", txt_iecode.Text);
            SqlParameter a4 = new SqlParameter("@remittercur", ddlcurr.Text);
            SqlParameter a5 = new SqlParameter("@AmountAdjusted", txt_adjamt.Text);
            SqlParameter a6 = new SqlParameter("@letterno", txt_letterNo.Text);
            SqlParameter a7 = new SqlParameter("@lettrdate", txtletterdate.Text);
            SqlParameter a8 = new SqlParameter("@adjustdate", txt_adjdate.Text);
            SqlParameter a9 = new SqlParameter("@adjutmentInd", DdladjustInd.Text);
            SqlParameter a10 = new SqlParameter("@documentno", txt_docno.Text);
            SqlParameter a11 = new SqlParameter("@documentDate", txt_docdate.Text);
            SqlParameter a12 = new SqlParameter("@portDischarge", txt_recport.Text);
            SqlParameter a13 = new SqlParameter("@adjusmentseqno", txtDocNo.Text);
            SqlParameter a14 = new SqlParameter("@RecordInd", ddlremclose.Text);
            SqlParameter a15 = new SqlParameter("@remarks", txt_remarks.Text);
            SqlParameter a16 = new SqlParameter("@approveby", ddlapproved.Text);
            SqlParameter a17 = new SqlParameter("@Addedby", Session["userName"].ToString());
            SqlParameter a18 = new SqlParameter("@Addeddate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            SqlParameter a19 = new SqlParameter("@Updatedby", Session["userName"].ToString());
            SqlParameter a20 = new SqlParameter("@Updatedate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            SqlParameter a21 = new SqlParameter("@Swift", txtswift.Text);

            string result = objsave.SaveDeleteData(querysave, a0, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16, a17, a18, a19, a20, a21);
            if (result == "inserted")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('ORM Cancellation Done.');", true);
                clear();
                txt_iecode.Text = "";
                txt_irmno.Text = "";
                lblOrmAmount.Text = "";
                lblcurr.Text = "";
                lblCustName1.Text = "";
                txtDocNo.Enabled = false;
                btnormCanHelp.Enabled = false;
                btncusthelp.Enabled = true;
                btnhelp.Enabled = true;
                txt_iecode.Enabled = true;
                txt_irmno.Enabled = true;
                ddlcurr.Enabled = true;
                txt_adjamt.Enabled = true;
                txt_adjdate.Enabled = true;
                DdladjustInd.Enabled = true;
                ddlapproved.Enabled = true;
                txt_letterNo.Enabled = true;
                txtletterdate.Enabled = true;
                txtswift.Enabled = true;
                txt_docno.Enabled = true;
                txt_docdate.Enabled = true;
                txt_recport.Enabled = true;
                txt_remarks.Enabled = true;
                lblOrmAmountLBL.Visible = true;
                generateDocumentNo();
            }
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddEdit_Outward_Remittance_Closure.aspx");
        //clear();
        //generateDocumentNo();
    }
    protected void txt_irmno_TextChanged(object sender, EventArgs e)
    {
        string query = "Filldata";
        TF_DATA objdata = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@ORMNo", txt_irmno.Text);
        DataTable dt = objdata.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            txt_recport.Text = dt.Rows[0]["Port_Of_Ship"].ToString();
            ddlcurr.SelectedValue = dt.Rows[0]["Currency"].ToString();
            txtswift.Text = dt.Rows[0]["Swift_code"].ToString();
            txt_adjamt.Text = dt.Rows[0]["Amount"].ToString();
            lblOrmAmount.Text = dt.Rows[0]["Amount"].ToString();
            lblcurr.Text = dt.Rows[0]["Currency"].ToString();
        }

        else
        {
            txt_iecode.Text = "";
            txt_irmno.Focus();
        }
    }
    //protected void txt_closseqno_TextChanged(object sender, EventArgs e)
    //{
    //    string query = "adjustclrchk";

    //    TF_DATA objdata = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@ORMNO", txt_irmno.Text);

    //    string result = objdata.SaveDeleteData(query, p1);

    //    if (result == "exist")
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('This Closure Sequence No is Already Exists');", true);
    //        txt_closseqno.Text = "";
    //        txt_closseqno.Focus();
    //    }
    //    else
    //    {
    //        ddlremclose.Focus();
    //    }
    //}
    protected void txt_iecode_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@IECODE", SqlDbType.VarChar);
        p1.Value = txt_iecode.Text.ToString();


        string _query = "get_Cust_name";


        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCustName1.Text = dt.Rows[0]["CUST_NAME"].ToString();

        }
        else
        {
            lblCustName1.Text = "";
            txt_iecode.Text = "";
            txt_irmno.Text = "";
            ddlcurr.SelectedValue = "0";
            txtswift.Text = "";
            txt_recport.Text = "";
            txtletterdate.Text = "";
            txt_letterNo.Text = "";
            txt_adjamt.Text = "";
            txt_docno.Text = "";
            txt_docdate.Text = "";



        }
    }
    private void clear()
    {
        txt_iecode.Text = "";
        lblCustName1.Text = "";
        txt_adjdate.Text = "";
        txt_remarks.Text = "";
        txt_irmno.Text = "";
        ddlcurr.SelectedValue = "0";
        ddlremclose.SelectedValue = "1";
        txtswift.Text = "";
        txt_recport.Text = "";
        txtletterdate.Text = "";
        txt_letterNo.Text = "";
        txt_adjamt.Text = "";
        txt_docno.Text = "";
        txt_docdate.Text = "";
        lblOrmAmount.Text = "";
        lblcurr.Text = "";

    }

    private void generateDocumentNo()
    {
        TF_DATA objData = new TF_DATA();
        string _query = "Generate_ORM_Closure_No";
        SqlParameter p1 = new SqlParameter("@year", SqlDbType.VarChar);
        p1.Value = System.DateTime.Now.Year.ToString();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            txtDocNo.Text = dt.Rows[0]["LastdocNo"].ToString();
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("View_Outward_Remittance_Closure.aspx", true);
    }
    protected void ddlremclose_SelectedIndexChanged(object sender, EventArgs e)
    {
        string indx = ddlremclose.SelectedIndex.ToString();
        if (ddlremclose.SelectedIndex == 0)
        {
            txt_iecode.Text = "";
            txt_irmno.Text = "";
            lblOrmAmount.Text = "";
            lblcurr.Text = "";
            lblCustName1.Text = "";
            txtDocNo.Enabled = false;
            btnormCanHelp.Enabled = false;
            btncusthelp.Enabled = true;
            btnhelp.Enabled = true;
            txt_iecode.Enabled = true;
            txt_irmno.Enabled = true;
            ddlcurr.Enabled = true;
            txt_adjamt.Enabled = true;
            txt_adjdate.Enabled = true;
            DdladjustInd.Enabled = true;
            ddlapproved.Enabled = true;
            txt_letterNo.Enabled = true;
            txtletterdate.Enabled = true;
            txtswift.Enabled = true;
            txt_docno.Enabled = true;
            txt_docdate.Enabled = true;
            txt_recport.Enabled = true;
            txt_remarks.Enabled = true;
            lblOrmAmountLBL.Visible = true;
            generateDocumentNo();
        }
        else
        {
            txtDocNo.Text = "";
            txt_iecode.Text = "";
            txt_irmno.Text = "";
            lblOrmAmount.Text = "";
            lblcurr.Text = "";
            lblCustName1.Text = "";
            txtDocNo.Enabled = true;
            btnormCanHelp.Enabled = true;
            btncusthelp.Enabled = false;
            btnhelp.Enabled = false;
            txt_iecode.Enabled = false;
            txt_irmno.Enabled = false;
            ddlcurr.Enabled = false;
            txt_adjamt.Enabled = false;
            txt_adjdate.Enabled = false;
            DdladjustInd.Enabled = false;
            ddlapproved.Enabled = false;
            txt_letterNo.Enabled = false;
            txtletterdate.Enabled = false;
            txtswift.Enabled = false;
            txt_docno.Enabled = false;
            txt_docdate.Enabled = false;
            txt_recport.Enabled = false;
            txt_remarks.Enabled = false;
            lblOrmAmountLBL.Visible = false;
        }
    }
    protected void txtDocNo_TextChanged(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@docno",SqlDbType.VarChar);
        p1.Value = txtDocNo.Text;

        string _query = "FillORMClrData";
        DataTable dt = obj.getData(_query,p1);

        if (dt.Rows.Count > 0)
        {
            txtDocNo.Text = dt.Rows[0]["ORC_DOC_NO"].ToString();
            txt_iecode.Text = dt.Rows[0]["IECode"].ToString();
            lblCustName1.Text = dt.Rows[0]["CUST_NAME"].ToString();
            txt_irmno.Text = dt.Rows[0]["outwardReferenceNumber"].ToString();
            ddlcurr.SelectedValue = dt.Rows[0]["remittanceCurrency"].ToString();
            txt_adjamt.Text = dt.Rows[0]["adjustedAmount"].ToString();
            txt_adjdate.Text = dt.Rows[0]["adjustedDate"].ToString();
            DdladjustInd.SelectedValue = dt.Rows[0]["adjustmentIndicator"].ToString();
            txt_letterNo.Text = dt.Rows[0]["letterNumber"].ToString();
            txtletterdate.Text = dt.Rows[0]["letterDate"].ToString();
            ddlapproved.SelectedValue = dt.Rows[0]["ApproveBy"].ToString();
            txtswift.Text = dt.Rows[0]["SWIFT"].ToString();
            txt_docno.Text = dt.Rows[0]["documentNumber"].ToString();
            txt_docdate.Text = dt.Rows[0]["documentDate"].ToString();
            txt_recport.Text = dt.Rows[0]["portofDischarge"].ToString();
            txt_remarks.Text = dt.Rows[0]["Remarks"].ToString();
        }
    }
}