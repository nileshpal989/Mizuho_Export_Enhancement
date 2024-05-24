﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;

public partial class RRETURN_Ret_Consolidate_CSV_Data : System.Web.UI.Page
{
    string todate;
    string adcode;
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
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;
                txtFromDate.Text = Session["FrRelDt"].ToString().Trim();
                txtToDate.Text = Session["ToRelDt"].ToString().Trim();
                btnUpload.Attributes.Add("onclick", "return validate();");
            }
        }
    }
    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_RET_GetBranchandADcodeList";
        DataTable dt = objData.getData(_query);
        ddlBranch.Items.Clear();
        //ListItem li = new ListItem();
        //li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            //li.Text = "---Select---";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        //else
        //li.Text = "No record(s) found";
        //ddlBranch.Items.Insert(0, li);
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            CheckFile();
            string errormumbai = ViewState["ErrorMessageMumbai"].ToString();
            string errordelhi = ViewState["ErrorMessageDelhi"].ToString();
            string errorBangalore = ViewState["ErrorMessageBangalore"].ToString();
            string errorchennai = ViewState["ErrorMessageChennai"].ToString();

            if (errormumbai != "Mumbai File Not Received.")
            {
                labelmumbai.ForeColor = System.Drawing.Color.Green;
                labelmumbai.Text = ViewState["ErrorMessageMumbai"].ToString();
            }
            else
            {
                labelmumbai.Text = ViewState["ErrorMessageMumbai"].ToString();
            }
            if (errordelhi != "Delhi File Not Received.")
            {
                labeldelhi.ForeColor = System.Drawing.Color.Green;
                labeldelhi.Text = ViewState["ErrorMessageDelhi"].ToString();
            }
            else
            {
                labeldelhi.Text = ViewState["ErrorMessageDelhi"].ToString();
            }
            if (errorBangalore != "Bangalore File Not Received.")
            {
                labelBangalore.ForeColor = System.Drawing.Color.Green;
                labelBangalore.Text = ViewState["ErrorMessageBangalore"].ToString();
            }
            else
            {
                labelBangalore.Text = ViewState["ErrorMessageBangalore"].ToString();
            }
            if (errorchennai != "Chennai File Not Received.")
            {
                labelchennai.ForeColor = System.Drawing.Color.Green;
                labelchennai.Text = ViewState["ErrorMessageChennai"].ToString();
            }
            else
            {
                labelchennai.Text = ViewState["ErrorMessageChennai"].ToString();
            }
            if (errormumbai != "Mumbai File Not Received." && errordelhi != "Delhi File Not Received." && errorBangalore != "Bangalore File Not Received." && errorchennai != "Chennai File Not Received.")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Confirm()", true);
            }
            else
            {
                labelmessage.ForeColor = System.Drawing.Color.Red;
                labelmessage.Text = "Files from all branches not received! You cannot consolidate";
            }
        }
        catch (Exception Ex)
        {
            SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
            ADCODE.Value = ddlBranch.SelectedValue.ToString();

            SqlParameter MENUNAME = new SqlParameter("@MENUNAME", SqlDbType.VarChar);
            MENUNAME.Value = "Consolidate Branch CSV File At Head OFfice";

            SqlParameter IPAddress = new SqlParameter("@IPAddress", SqlDbType.VarChar);
            IPAddress.Value = GetIPAddress();

            SqlParameter URL = new SqlParameter("@URL", SqlDbType.VarChar);
            URL.Value = HttpContext.Current.Request.Url.AbsoluteUri;

            SqlParameter TYPE = new SqlParameter("@TYPE", SqlDbType.VarChar);
            TYPE.Value = Ex.GetType().Name.ToString();

            SqlParameter Message = new SqlParameter("@Message", SqlDbType.VarChar);
            Message.Value = Ex.Message;

            SqlParameter StackTrace = new SqlParameter("@StackTrace", SqlDbType.VarChar);
            StackTrace.Value = Ex.StackTrace;

            SqlParameter Source = new SqlParameter("@Source", SqlDbType.VarChar);
            Source.Value = Ex.Source;

            SqlParameter TargetSite = new SqlParameter("@TargetSite", SqlDbType.VarChar);
            TargetSite.Value = Ex.TargetSite.ToString();

            SqlParameter DATETIME = new SqlParameter("@DATETIME", SqlDbType.VarChar);
            DATETIME.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

            SqlParameter UserName = new SqlParameter("@UserName", SqlDbType.VarChar);
            UserName.Value = Session["userName"].ToString().Trim(); ;

            TF_DATA objDataInput = new TF_DATA();
            string qryError = "TF_RET_ErrorException";
            string dtInput1 = objDataInput.SaveDeleteData(qryError, ADCODE, MENUNAME,IPAddress, URL, Message, StackTrace, Source, TargetSite, DATETIME, TYPE, UserName);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Page contains error.')", true);
            Response.Redirect("ErrorPage.aspx?PageHeader=Error Page");
        }
    }
    //protected void CheckFile()
    //{
    //    ViewState["ErrorMessage"] = "";
    //    adcode = Session["userADCode"].ToString().Trim();
    //    todate = Session["ToRelDt"].ToString().Trim();
    //    string filedate = todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4);

    //    // Mumbai File Check
    //    string mumbaierspath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/HO_ConsoFile/ERS_6770001_" + filedate + ".CSV");
    //    string a = System.IO.Path.GetFileName(mumbaierspath);

    //    if (!System.IO.File.Exists(mumbaierspath))
    //    {
    //        ViewState["ErrorMessageMumbai"] = "Mumbai File Not Received.";
    //        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Mumbai ERS File does not exist');", true);
    //    }
    //    else
    //    {
    //        ViewState["ErrorMessageMumbai"] = "Mumbai File Received.";
    //    }
    //    string mumbainostropath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/HO_ConsoFile/Nostro_6770001_" + filedate + ".CSV");
    //    if (!System.IO.File.Exists(mumbainostropath))
    //    {
    //        ViewState["ErrorMessageMumbai"] = "Mumbai File Not Received.";
    //        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Mumbai ERS File does not exist');", true);
    //    }
    //    else
    //    {
    //        ViewState["ErrorMessageMumbai"] = "Mumbai File Received.";
    //    }
    //    string mumbaivostropath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/HO_ConsoFile/Vostro_6770001_" + filedate + ".CSV");
    //    if (!System.IO.File.Exists(mumbaivostropath))
    //    {
    //        ViewState["ErrorMessageMumbai"] = "Mumbai File Not Received.";
    //        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Mumbai ERS File does not exist');", true);
    //    }
    //    else
    //    {
    //        ViewState["ErrorMessageMumbai"] = "Mumbai File Received.";
    //    }

    //    // Delhi File Check
    //    string delhierspath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/HO_ConsoFile/ERS_6770002_" + filedate + ".CSV");
    //    if (!System.IO.File.Exists(delhierspath))
    //    {
    //        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('New Delhi ERS File does not exist');", true);
    //        ViewState["ErrorMessageDelhi"] = "New Delhi File Not Received.";
    //    }
    //    else
    //    {
    //        ViewState["ErrorMessageDelhi"] = "New Delhi File Received.";
    //    }
    //    string delhinostropath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/HO_ConsoFile/Nostro_6770002_" + filedate + ".CSV");
    //    if (!System.IO.File.Exists(delhinostropath))
    //    {
    //        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('New Delhi ERS File does not exist');", true);
    //        ViewState["ErrorMessageDelhi"] = "New Delhi File Not Received.";
    //    }
    //    else
    //    {
    //        ViewState["ErrorMessageDelhi"] = "New Delhi File Received.";
    //    }
    //    string delhivostropath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/HO_ConsoFile/Vostro_6770002_" + filedate + ".CSV");
    //    if (!System.IO.File.Exists(delhivostropath))
    //    {
    //        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('New Delhi ERS File does not exist');", true);
    //        ViewState["ErrorMessageDelhi"] = "New Delhi File Not Received.";
    //    }
    //    else
    //    {
    //        ViewState["ErrorMessageDelhi"] = "New Delhi File Received.";
    //    }

    //    // Bangalore File Check
    //    string Bangaloreerspath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/HO_ConsoFile/ERS_6770003_" + filedate + ".CSV");
    //    if (!System.IO.File.Exists(Bangaloreerspath))
    //    {
    //        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Bangalore ERS File does not exist');", true);
    //        ViewState["ErrorMessageBangalore"] = "Bangalore File Not Received.";
    //    }
    //    else
    //    {
    //        ViewState["ErrorMessageBangalore"] = "Bangalore File Received.";
    //    }
    //    string Bangalorenostropath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/HO_ConsoFile/Nostro_6770003_" + filedate + ".CSV");
    //    if (!System.IO.File.Exists(Bangalorenostropath))
    //    {
    //        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Bangalore ERS File does not exist');", true);
    //        ViewState["ErrorMessageBangalore"] = "Bangalore File Not Received.";
    //    }
    //    else
    //    {
    //        ViewState["ErrorMessageBangalore"] = "Bangalore File Received.";
    //    }
    //    string Bangalorevostropath = Server.MapPath("../TF_GeneratedFiles/RRETURN/HO_ConsoFile/Vostro_6770003_" + filedate + ".CSV");
    //    if (!System.IO.File.Exists(Bangalorevostropath))
    //    {
    //        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Bangalore ERS File does not exist');", true);
    //        ViewState["ErrorMessageBangalore"] = "Bangalore File Not Received.";
    //    }
    //    else
    //    {
    //        ViewState["ErrorMessageBangalore"] = "Bangalore File Received.";
    //    }

    //    // Chennai File Check
    //    string chennaierspath = Server.MapPath("../TF_GeneratedFiles/RRETURN/HO_ConsoFile/ERS_6770004_" + filedate + ".CSV");
    //    if (!System.IO.File.Exists(chennaierspath))
    //    {
    //        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Chennai ERS File does not exist');", true);
    //        ViewState["ErrorMessageChennai"] = "Chennai File Not Received.";
    //    }
    //    else
    //    {
    //        ViewState["ErrorMessageChennai"] = "Chennai File Received.";
    //    }
    //    string chennainostropath = Server.MapPath("../TF_GeneratedFiles/RRETURN/HO_ConsoFile/Nostro_6770004_" + filedate + ".CSV");
    //    if (!System.IO.File.Exists(chennainostropath))
    //    {
    //        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Chennai ERS File does not exist');", true);
    //        ViewState["ErrorMessageChennai"] = "Chennai File Not Received.";
    //    }
    //    else
    //    {
    //        ViewState["ErrorMessageChennai"] = "Chennai File Received.";
    //    }
    //    string chennaivostropath = Server.MapPath("../TF_GeneratedFiles/RRETURN/HO_ConsoFile/Vostro_6770004_" + filedate + ".CSV");
    //    if (!System.IO.File.Exists(chennaivostropath))
    //    {
    //        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Chennai ERS File does not exist');", true);
    //        ViewState["ErrorMessageChennai"] = "Chennai File Not Received.";
    //    }
    //    else
    //    {
    //        ViewState["ErrorMessageChennai"] = "Chennai File Received.";
    //    }
    //}
    protected void CheckFile()
    {
        //StringBuilder ErrorMessage = new StringBuilder();
        try
        {
            ViewState["ErrorMessage"] = "";
            adcode = Session["userADCode"].ToString().Trim();
            todate = Session["ToRelDt"].ToString().Trim();
            string filedate = todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4);

            string mumbaierspath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/BR_Mumbai_ConsoFile/ERS_6770001_" + filedate + ".CSV");
            string mumbainostropath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/BR_Mumbai_ConsoFile/Nostro_6770001_" + filedate + ".CSV");
            string mumbaivostropath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/BR_Mumbai_ConsoFile/Vostro_6770001_" + filedate + ".CSV");
            if (System.IO.File.Exists(mumbaierspath))
            {
                if (System.IO.File.Exists(mumbainostropath))
                {
                    if (System.IO.File.Exists(mumbaivostropath))
                    {
                        ViewState["ErrorMessageMumbai"] = "Mumbai File Received.";
                    }
                    else
                    {
                        ViewState["ErrorMessageMumbai"] = "Mumbai File Not Received.";
                    }
                }
                else
                {
                    ViewState["ErrorMessageMumbai"] = "Mumbai File Not Received.";
                }
            }
            else
            {
                ViewState["ErrorMessageMumbai"] = "Mumbai File Not Received.";
            }

            //New Delhi File Check
            string delhierspath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/BR_NewDelhi_ConsoFile/ERS_6770002_" + filedate + ".CSV");
            string delhinostropath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/BR_NewDelhi_ConsoFile/Nostro_6770002_" + filedate + ".CSV");
            string delhivostropath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/BR_NewDelhi_ConsoFile/Vostro_6770002_" + filedate + ".CSV");
            if (System.IO.File.Exists(delhierspath))
            {
                if (System.IO.File.Exists(delhinostropath))
                {
                    if (System.IO.File.Exists(delhivostropath))
                    {
                        ViewState["ErrorMessageDelhi"] = "Delhi File Received.";
                    }
                    else
                    {
                        ViewState["ErrorMessageDelhi"] = "Delhi File Not Received.";
                    }
                }
                else
                {
                    ViewState["ErrorMessageDelhi"] = "Delhi File Not Received.";
                }
            }
            else
            {
                ViewState["ErrorMessageDelhi"] = "Delhi File Not Received.";
            }

            // Bangalore File Check
            string Bangaloreerspath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/BR_Bangalore_ConsoFile/ERS_6770003_" + filedate + ".CSV");
            string Bangalorenostropath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/BR_Bangalore_ConsoFile/Nostro_6770003_" + filedate + ".CSV");
            string Bangalorevostropath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/BR_Bangalore_ConsoFile/Vostro_6770003_" + filedate + ".CSV");
            if (System.IO.File.Exists(Bangaloreerspath))
            {
                if (System.IO.File.Exists(Bangalorenostropath))
                {
                    if (System.IO.File.Exists(Bangalorevostropath))
                    {
                        ViewState["ErrorMessageBangalore"] = "Bangalore File Received.";
                    }
                    else
                    {
                        ViewState["ErrorMessageBangalore"] = "Bangalore File Not Received.";
                    }
                }
                else
                {
                    ViewState["ErrorMessageBangalore"] = "Bangalore File Not Received.";
                }
            }
            else
            {
                ViewState["ErrorMessageBangalore"] = "Bangalore File Not Received.";
            }

            // Chennai File Check
            string chennaierspath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/BR_Chennai_ConsoFile/ERS_6770004_" + filedate + ".CSV");
            string chennainostropath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/BR_Chennai_ConsoFile/Nostro_6770004_" + filedate + ".CSV");
            string chennaivostropath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/BR_Chennai_ConsoFile/Vostro_6770004_" + filedate + ".CSV");
            if (System.IO.File.Exists(chennaierspath))
            {
                if (System.IO.File.Exists(chennainostropath))
                {
                    if (System.IO.File.Exists(chennaivostropath))
                    {
                        ViewState["ErrorMessageChennai"] = "Chennai File Received.";
                    }
                    else
                    {
                        ViewState["ErrorMessageChennai"] = "Chennai File Not Received.";
                    }
                }
                else
                {
                    ViewState["ErrorMessageChennai"] = "Chennai File Not Received.";
                }
            }
            else
            {
                ViewState["ErrorMessageChennai"] = "Chennai File Not Received.";
            }
        }
        catch (Exception Ex)
        {
            SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
            ADCODE.Value = ddlBranch.SelectedValue.ToString();

            SqlParameter MENUNAME = new SqlParameter("@MENUNAME", SqlDbType.VarChar);
            MENUNAME.Value = "Consolidate Branch CSV File At Head OFfice";

            SqlParameter IPAddress = new SqlParameter("@IPAddress", SqlDbType.VarChar);
            IPAddress.Value = GetIPAddress();

            SqlParameter URL = new SqlParameter("@URL", SqlDbType.VarChar);
            URL.Value = HttpContext.Current.Request.Url.AbsoluteUri;

            SqlParameter TYPE = new SqlParameter("@TYPE", SqlDbType.VarChar);
            TYPE.Value = Ex.GetType().Name.ToString();

            SqlParameter Message = new SqlParameter("@Message", SqlDbType.VarChar);
            Message.Value = Ex.Message;

            SqlParameter StackTrace = new SqlParameter("@StackTrace", SqlDbType.VarChar);
            StackTrace.Value = Ex.StackTrace;

            SqlParameter Source = new SqlParameter("@Source", SqlDbType.VarChar);
            Source.Value = Ex.Source;

            SqlParameter TargetSite = new SqlParameter("@TargetSite", SqlDbType.VarChar);
            TargetSite.Value = Ex.TargetSite.ToString();

            SqlParameter DATETIME = new SqlParameter("@DATETIME", SqlDbType.VarChar);
            DATETIME.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

            SqlParameter UserName = new SqlParameter("@UserName", SqlDbType.VarChar);
            UserName.Value = Session["userName"].ToString().Trim(); ;

            TF_DATA objDataInput = new TF_DATA();
            string qryError = "TF_RET_ErrorException";
            string dtInput1 = objDataInput.SaveDeleteData(qryError, ADCODE, MENUNAME, IPAddress, URL, Message, StackTrace, Source, TargetSite, DATETIME, TYPE, UserName);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Page contains error.')", true);
            Response.Redirect("ErrorPage.aspx?PageHeader=Error Page");
        }
       // return ErrorMessage.ToString();
    }
    public void Upload_ERS()
    {
        #region ERS Data Consolidation
        try
        {
            todate = Session["ToRelDt"].ToString().Trim();
            string filedate = todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4);
            // Mumbai File Check
            DataTable dtCsv = new DataTable();
            string Fulltext;
            string mumbaiERSpath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/BR_Mumbai_ConsoFile/ERS_6770001_" + filedate + ".CSV");
            string newdelhiERSpath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/BR_NewDelhi_ConsoFile/ERS_6770002_" + filedate + ".CSV");
            string BangaloreERSpath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/BR_Bangalore_ConsoFile/ERS_6770003_" + filedate + ".CSV");
            string chennaiERSpath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/BR_Chennai_ConsoFile/ERS_6770004_" + filedate + ".CSV");
            string[] pathArr = { mumbaiERSpath, newdelhiERSpath, BangaloreERSpath, chennaiERSpath };
            for (int j = 0; j < 4; j++)
            {
                string path = pathArr[j];
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        Fulltext = sr.ReadToEnd().ToString(); //read full file text  
                        string[] rows = Fulltext.Split('\n'); //split full file text into rows  
                        for (int i = 0; i < rows.Count() - 1; i++)
                        {
                            string[] rowValues = rows[i].Split(','); //split each row with comma to get individual values  
                            {
                                if (i != 0)
                                {

                                    SqlParameter adcode = new SqlParameter("@adcode", SqlDbType.VarChar);
                                    adcode.Value = rowValues[0].ToString();

                                    SqlParameter branchname = new SqlParameter("@branchname", SqlDbType.VarChar);
                                    branchname.Value = rowValues[1].ToString();

                                    SqlParameter srno = new SqlParameter("@srno", SqlDbType.VarChar);
                                    srno.Value = rowValues[2].ToString();

                                    SqlParameter fr_fortnight_dt = new SqlParameter("@fr_fortnight_dt", SqlDbType.VarChar);
                                    fr_fortnight_dt.Value = rowValues[3].ToString();

                                    SqlParameter to_fortnight_dt = new SqlParameter("@to_fortnight_dt", SqlDbType.VarChar);
                                    to_fortnight_dt.Value = rowValues[4].ToString();

                                    SqlParameter transaction_date = new SqlParameter("@transaction_date", SqlDbType.VarChar);
                                    transaction_date.Value = rowValues[5].ToString();

                                    SqlParameter docno = new SqlParameter("@docno", SqlDbType.VarChar);
                                    docno.Value = rowValues[6].ToString();

                                    SqlParameter iecode = new SqlParameter("@iecode", SqlDbType.VarChar);
                                    iecode.Value = rowValues[7].ToString();

                                    SqlParameter formsrno = new SqlParameter("@formsrno", SqlDbType.VarChar);
                                    formsrno.Value = rowValues[8].ToString();

                                    SqlParameter purpose_id = new SqlParameter("@purpose_id", SqlDbType.VarChar);
                                    purpose_id.Value = rowValues[9].ToString();

                                    SqlParameter port_code = new SqlParameter("@port_code", SqlDbType.VarChar);
                                    port_code.Value = rowValues[10].ToString();

                                    SqlParameter shipping_bill_no = new SqlParameter("@shipping_bill_no", SqlDbType.VarChar);
                                    shipping_bill_no.Value = rowValues[11].ToString();

                                    SqlParameter shipping_bill_dt = new SqlParameter("@shipping_bill_dt", SqlDbType.VarChar);
                                    shipping_bill_dt.Value = rowValues[12].ToString();

                                    SqlParameter curr = new SqlParameter("@curr", SqlDbType.VarChar);
                                    curr.Value = rowValues[13].ToString();

                                    SqlParameter amount = new SqlParameter("@amount", SqlDbType.VarChar);
                                    amount.Value = rowValues[14].ToString();

                                    SqlParameter inr_amount = new SqlParameter("@inr_amount", SqlDbType.VarChar);
                                    inr_amount.Value = rowValues[15].ToString();

                                    SqlParameter ac_country_code = new SqlParameter("@ac_country_code", SqlDbType.VarChar);
                                    ac_country_code.Value = rowValues[16].ToString();

                                    SqlParameter bn_country_code = new SqlParameter("@bn_country_code", SqlDbType.VarChar);
                                    bn_country_code.Value = rowValues[17].ToString();

                                    SqlParameter remiCountry = new SqlParameter("@remicountry", SqlDbType.VarChar);
                                    remiCountry.Value = rowValues[18].ToString();

                                    SqlParameter customersrno = new SqlParameter("@customersrno", SqlDbType.VarChar);
                                    customersrno.Value = rowValues[19].ToString();

                                    SqlParameter lcindication = new SqlParameter("@lcindication", SqlDbType.VarChar);
                                    lcindication.Value = rowValues[20].ToString();

                                    SqlParameter benename = new SqlParameter("@benename", SqlDbType.VarChar);
                                    benename.Value = rowValues[21].ToString();

                                    SqlParameter reminame = new SqlParameter("@reminame", SqlDbType.VarChar);
                                    reminame.Value = rowValues[22].ToString();
                                    
                                    SqlParameter valuedt = new SqlParameter("@valuedt", SqlDbType.VarChar);
                                    valuedt.Value = rowValues[23].ToString();

                                    SqlParameter vastro_ac = new SqlParameter("@vastro_ac", SqlDbType.VarChar);
                                    vastro_ac.Value = rowValues[24].ToString();

                                    SqlParameter mod_type = new SqlParameter("@mod_type", SqlDbType.VarChar);
                                    mod_type.Value = rowValues[25].ToString();

                                    SqlParameter schedule_no = new SqlParameter("@schedule_no", SqlDbType.VarChar);
                                    schedule_no.Value = rowValues[26].ToString();

                                    SqlParameter realized_amt = new SqlParameter("@realized_amt", SqlDbType.VarChar);
                                    realized_amt.Value = rowValues[27].ToString();

                                    SqlParameter exrt = new SqlParameter("@exrt", SqlDbType.VarChar);
                                    exrt.Value = rowValues[28].ToString();

                                    SqlParameter billno = new SqlParameter("@billno", SqlDbType.VarChar);
                                    billno.Value = rowValues[29].ToString();

                                    SqlParameter bankcode = new SqlParameter("@bankcode", SqlDbType.VarChar);
                                    bankcode.Value = rowValues[30].ToString();

                                    SqlParameter bankname = new SqlParameter("@bankname", SqlDbType.VarChar);
                                    bankname.Value = rowValues[31].ToString();

                                    TF_DATA objDataInput1 = new TF_DATA();
                                    string qryInput1 = "TF_RET_ERS_DATA_Consolidate";
                                    string dtInput1 = objDataInput1.SaveDeleteData(qryInput1, adcode, branchname, srno, fr_fortnight_dt, to_fortnight_dt, transaction_date, docno, iecode, formsrno, purpose_id, port_code, shipping_bill_no, shipping_bill_dt, curr, amount, inr_amount, ac_country_code, bn_country_code, customersrno, lcindication, benename, reminame, valuedt, vastro_ac, mod_type, schedule_no, realized_amt, exrt, billno, bankcode, bankname, remiCountry);
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
            ADCODE.Value = ddlBranch.SelectedValue.ToString();

            SqlParameter MENUNAME = new SqlParameter("@MENUNAME", SqlDbType.VarChar);
            MENUNAME.Value = "Consolidate Branch CSV File At Head OFfice";

            SqlParameter IPAddress = new SqlParameter("@IPAddress", SqlDbType.VarChar);
            IPAddress.Value = GetIPAddress();

            SqlParameter URL = new SqlParameter("@URL", SqlDbType.VarChar);
            URL.Value = HttpContext.Current.Request.Url.AbsoluteUri;

            SqlParameter TYPE = new SqlParameter("@TYPE", SqlDbType.VarChar);
            TYPE.Value = Ex.GetType().Name.ToString();

            SqlParameter Message = new SqlParameter("@Message", SqlDbType.VarChar);
            Message.Value = Ex.Message;

            SqlParameter StackTrace = new SqlParameter("@StackTrace", SqlDbType.VarChar);
            StackTrace.Value = Ex.StackTrace;

            SqlParameter Source = new SqlParameter("@Source", SqlDbType.VarChar);
            Source.Value = Ex.Source;

            SqlParameter TargetSite = new SqlParameter("@TargetSite", SqlDbType.VarChar);
            TargetSite.Value = Ex.TargetSite.ToString();

            SqlParameter DATETIME = new SqlParameter("@DATETIME", SqlDbType.VarChar);
            DATETIME.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

            SqlParameter UserName = new SqlParameter("@UserName", SqlDbType.VarChar);
            UserName.Value = Session["userName"].ToString().Trim(); ;

            TF_DATA objDataInput = new TF_DATA();
            string qryError = "TF_RET_ErrorException";
            string dtInput1 = objDataInput.SaveDeleteData(qryError, ADCODE, MENUNAME, IPAddress, URL, Message, StackTrace, Source, TargetSite, DATETIME, TYPE, UserName);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Page contains error.')", true);
            Response.Redirect("ErrorPage.aspx?PageHeader=Error Page");
        }
        #endregion
    }
    protected void Upload_Nostro()
    {
        try
        {
            todate = Session["ToRelDt"].ToString().Trim();
            string filedate = todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4);
            // Mumbai File Check
            DataTable dtCsv = new DataTable();
            string Fulltext;
            string mumbaiNostropath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/BR_Mumbai_ConsoFile/Nostro_6770001_" + filedate + ".CSV");
            string newdelhiNostropath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/BR_NewDelhi_ConsoFile/Nostro_6770002_" + filedate + ".CSV");
            string BangaloreNostropath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/BR_Bangalore_ConsoFile/Nostro_6770003_" + filedate + ".CSV");
            string chennaiNostropath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/BR_Chennai_ConsoFile/Nostro_6770004_" + filedate + ".CSV");
            string[] pathArr = { mumbaiNostropath, newdelhiNostropath, BangaloreNostropath, chennaiNostropath };
            for (int j = 0; j < 4; j++)
            {
                string path = pathArr[j];
                using (StreamReader sr = new StreamReader(path))
                {

                    while (!sr.EndOfStream)
                    {
                        Fulltext = sr.ReadToEnd().ToString(); //read full file text  
                        string[] rows = Fulltext.Split('\n'); //split full file text into rows  
                        for (int i = 0; i < rows.Count() - 1; i++)
                        {
                            string[] rowValues = rows[i].Split(','); //split each row with comma to get individual values  
                            {
                                if (i != 0)
                                {
                                    SqlParameter adcode = new SqlParameter("@adcode", SqlDbType.VarChar);
                                    adcode.Value = rowValues[0].ToString();

                                    SqlParameter branchname = new SqlParameter("@branchname", SqlDbType.VarChar);
                                    branchname.Value = rowValues[1].ToString();

                                    SqlParameter fr_fortnight_dt = new SqlParameter("@fr_fortnight_dt", SqlDbType.VarChar);
                                    fr_fortnight_dt.Value = rowValues[2].ToString();

                                    SqlParameter to_fortnight_dt = new SqlParameter("@to_fortnight_dt", SqlDbType.VarChar);
                                    to_fortnight_dt.Value = rowValues[3].ToString();

                                    SqlParameter nv = new SqlParameter("@nv", SqlDbType.VarChar);
                                    nv.Value = rowValues[4].ToString();

                                    SqlParameter curr = new SqlParameter("@curr", SqlDbType.VarChar);
                                    curr.Value = rowValues[5].ToString();

                                    SqlParameter op_cash_d = new SqlParameter("@op_cash_d", SqlDbType.VarChar);
                                    op_cash_d.Value = rowValues[6].ToString();

                                    SqlParameter op_cash_c = new SqlParameter("@op_cash_c", SqlDbType.VarChar);
                                    op_cash_c.Value = rowValues[7].ToString();

                                    SqlParameter op_susp_d = new SqlParameter("@op_susp_d", SqlDbType.VarChar);
                                    op_susp_d.Value = rowValues[8].ToString();

                                    SqlParameter op_susp_c = new SqlParameter("@op_susp_c", SqlDbType.VarChar);
                                    op_susp_c.Value = rowValues[9].ToString();

                                    SqlParameter op_dep_oth_d = new SqlParameter("@op_dep_oth_d", SqlDbType.VarChar);
                                    op_dep_oth_d.Value = rowValues[10].ToString();

                                    SqlParameter op_dep_oth_c = new SqlParameter("@op_dep_oth_c", SqlDbType.VarChar);
                                    op_dep_oth_c.Value = rowValues[11].ToString();

                                    SqlParameter op_dep_rbi_d = new SqlParameter("@op_dep_rbi_d", SqlDbType.VarChar);
                                    op_dep_rbi_d.Value = rowValues[12].ToString();

                                    SqlParameter op_dep_rbi_c = new SqlParameter("@op_dep_rbi_c", SqlDbType.VarChar);
                                    op_dep_rbi_c.Value = rowValues[13].ToString();

                                    SqlParameter op_fd_d = new SqlParameter("@op_fd_d", SqlDbType.VarChar);
                                    op_fd_d.Value = rowValues[14].ToString();

                                    SqlParameter op_fd_c = new SqlParameter("@op_fd_c", SqlDbType.VarChar);
                                    op_fd_c.Value = rowValues[15].ToString();

                                    SqlParameter op_tb_d = new SqlParameter("@op_tb_d", SqlDbType.VarChar);
                                    op_tb_d.Value = rowValues[16].ToString();

                                    SqlParameter op_tb_c = new SqlParameter("@op_tb_c", SqlDbType.VarChar);
                                    op_tb_c.Value = rowValues[17].ToString();

                                    SqlParameter op_ss_d = new SqlParameter("@op_ss_d", SqlDbType.VarChar);
                                    op_ss_d.Value = rowValues[18].ToString();

                                    SqlParameter op_ss_c = new SqlParameter("@op_ss_c", SqlDbType.VarChar);
                                    op_ss_c.Value = rowValues[19].ToString();

                                    SqlParameter op_fclo_d = new SqlParameter("@op_fclo_d", SqlDbType.VarChar);
                                    op_fclo_d.Value = rowValues[20].ToString();

                                    SqlParameter op_fclo_c = new SqlParameter("@op_fclo_c", SqlDbType.VarChar);
                                    op_fclo_c.Value = rowValues[21].ToString();

                                    SqlParameter op_oth_d = new SqlParameter("@op_oth_d", SqlDbType.VarChar);
                                    op_oth_d.Value = rowValues[22].ToString();

                                    SqlParameter op_oth_c = new SqlParameter("@op_oth_c", SqlDbType.VarChar);
                                    op_oth_c.Value = rowValues[23].ToString();

                                    SqlParameter op_tot_d = new SqlParameter("@op_tot_d", SqlDbType.VarChar);
                                    op_tot_d.Value = rowValues[24].ToString();

                                    SqlParameter op_tot_c = new SqlParameter("@op_tot_c", SqlDbType.VarChar);
                                    op_tot_c.Value = rowValues[25].ToString();

                                    SqlParameter cl_cash_d = new SqlParameter("@cl_cash_d", SqlDbType.VarChar);
                                    cl_cash_d.Value = rowValues[26].ToString();

                                    SqlParameter cl_cash_c = new SqlParameter("@cl_cash_c", SqlDbType.VarChar);
                                    cl_cash_c.Value = rowValues[27].ToString();

                                    SqlParameter cl_susp_d = new SqlParameter("@cl_susp_d", SqlDbType.VarChar);
                                    cl_susp_d.Value = rowValues[28].ToString();

                                    SqlParameter cl_susp_c = new SqlParameter("@cl_susp_c", SqlDbType.VarChar);
                                    cl_susp_c.Value = rowValues[29].ToString();

                                    SqlParameter cl_dep_oth_d = new SqlParameter("@cl_dep_oth_d", SqlDbType.VarChar);
                                    cl_dep_oth_d.Value = rowValues[30].ToString();

                                    SqlParameter cl_dep_oth_c = new SqlParameter("@cl_dep_oth_c", SqlDbType.VarChar);
                                    cl_dep_oth_c.Value = rowValues[31].ToString();

                                    SqlParameter cl_dep_rbi_d = new SqlParameter("@cl_dep_rbi_d", SqlDbType.VarChar);
                                    cl_dep_rbi_d.Value = rowValues[32].ToString();

                                    SqlParameter cl_dep_rbi_c = new SqlParameter("@cl_dep_rbi_c", SqlDbType.VarChar);
                                    cl_dep_rbi_c.Value = rowValues[33].ToString();

                                    SqlParameter cl_fd_d = new SqlParameter("@cl_fd_d", SqlDbType.VarChar);
                                    cl_fd_d.Value = rowValues[34].ToString();

                                    SqlParameter cl_fd_c = new SqlParameter("@cl_fd_c", SqlDbType.VarChar);
                                    cl_fd_c.Value = rowValues[35].ToString();

                                    SqlParameter cl_tb_d = new SqlParameter("@cl_tb_d", SqlDbType.VarChar);
                                    cl_tb_d.Value = rowValues[36].ToString();

                                    SqlParameter cl_tb_c = new SqlParameter("@cl_tb_c", SqlDbType.VarChar);
                                    cl_tb_c.Value = rowValues[37].ToString();

                                    SqlParameter cl_ss_d = new SqlParameter("@cl_ss_d", SqlDbType.VarChar);
                                    cl_ss_d.Value = rowValues[38].ToString();

                                    SqlParameter cl_ss_c = new SqlParameter("@cl_ss_c", SqlDbType.VarChar);
                                    cl_ss_c.Value = rowValues[39].ToString();

                                    SqlParameter cl_fclo_d = new SqlParameter("@cl_fclo_d", SqlDbType.VarChar);
                                    cl_fclo_d.Value = rowValues[40].ToString();

                                    SqlParameter cl_fclo_c = new SqlParameter("@cl_fclo_c", SqlDbType.VarChar);
                                    cl_fclo_c.Value = rowValues[41].ToString();

                                    SqlParameter cl_oth_d = new SqlParameter("@cl_oth_d", SqlDbType.VarChar);
                                    cl_oth_d.Value = rowValues[42].ToString();

                                    SqlParameter cl_oth_c = new SqlParameter("@cl_oth_c", SqlDbType.VarChar);
                                    cl_oth_c.Value = rowValues[43].ToString();

                                    SqlParameter cl_tot_d = new SqlParameter("@cl_tot_d", SqlDbType.VarChar);
                                    cl_tot_d.Value = rowValues[44].ToString();

                                    SqlParameter cl_tot_c = new SqlParameter("@cl_tot_c", SqlDbType.VarChar);
                                    cl_tot_c.Value = rowValues[45].ToString();

                                    SqlParameter eefcac = new SqlParameter("@eefcac", SqlDbType.VarChar);
                                    eefcac.Value = rowValues[46].ToString();

                                    SqlParameter efcac = new SqlParameter("@efcac", SqlDbType.VarChar);
                                    efcac.Value = rowValues[47].ToString();

                                    SqlParameter rfcac = new SqlParameter("@rfcac", SqlDbType.VarChar);
                                    rfcac.Value = rowValues[48].ToString();

                                    SqlParameter escrowac = new SqlParameter("@escrowac", SqlDbType.VarChar);
                                    escrowac.Value = rowValues[49].ToString();

                                    SqlParameter fcnrac = new SqlParameter("@fcnrac", SqlDbType.VarChar);
                                    fcnrac.Value = rowValues[50].ToString();

                                    SqlParameter otherac = new SqlParameter("@otherac", SqlDbType.VarChar);
                                    otherac.Value = rowValues[51].ToString();

                                    TF_DATA objDataInput1 = new TF_DATA();
                                    string query = "TF_RET_NOSTRO_DATA_Consolidate";
                                    string dtInput1 = objDataInput1.SaveDeleteData(query, adcode, branchname, fr_fortnight_dt, to_fortnight_dt, nv, curr, op_cash_d, op_cash_c, op_susp_d, op_susp_c, op_dep_oth_d, op_dep_oth_c, op_dep_rbi_d, op_dep_rbi_c, op_fd_d, op_fd_c, op_tb_d, op_tb_c, op_ss_d, op_ss_c, op_fclo_d, op_fclo_c, op_oth_d, op_oth_c, op_tot_d, op_tot_c, cl_cash_d, cl_cash_c, cl_susp_d, cl_susp_c, cl_dep_oth_d, cl_dep_oth_c, cl_dep_rbi_d, cl_dep_rbi_c, cl_fd_d, cl_fd_c, cl_tb_d, cl_tb_c, cl_ss_d, cl_ss_c, cl_fclo_d, cl_fclo_c, cl_oth_d, cl_oth_c, cl_tot_d, cl_tot_c, eefcac, efcac, rfcac, escrowac, fcnrac, otherac);
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
            ADCODE.Value = ddlBranch.SelectedValue.ToString();

            SqlParameter MENUNAME = new SqlParameter("@MENUNAME", SqlDbType.VarChar);
            MENUNAME.Value = "Consolidate Branch CSV File At Head OFfice";

            SqlParameter IPAddress = new SqlParameter("@IPAddress", SqlDbType.VarChar);
            IPAddress.Value = GetIPAddress();

            SqlParameter URL = new SqlParameter("@URL", SqlDbType.VarChar);
            URL.Value = HttpContext.Current.Request.Url.AbsoluteUri;

            SqlParameter TYPE = new SqlParameter("@TYPE", SqlDbType.VarChar);
            TYPE.Value = Ex.GetType().Name.ToString();

            SqlParameter Message = new SqlParameter("@Message", SqlDbType.VarChar);
            Message.Value = Ex.Message;

            SqlParameter StackTrace = new SqlParameter("@StackTrace", SqlDbType.VarChar);
            StackTrace.Value = Ex.StackTrace;

            SqlParameter Source = new SqlParameter("@Source", SqlDbType.VarChar);
            Source.Value = Ex.Source;

            SqlParameter TargetSite = new SqlParameter("@TargetSite", SqlDbType.VarChar);
            TargetSite.Value = Ex.TargetSite.ToString();

            SqlParameter DATETIME = new SqlParameter("@DATETIME", SqlDbType.VarChar);
            DATETIME.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

            SqlParameter UserName = new SqlParameter("@UserName", SqlDbType.VarChar);
            UserName.Value = Session["userName"].ToString().Trim(); ;

            TF_DATA objDataInput = new TF_DATA();
            string qryError = "TF_RET_ErrorException";
            string dtInput1 = objDataInput.SaveDeleteData(qryError, ADCODE, MENUNAME, IPAddress, URL, Message, StackTrace, Source, TargetSite, DATETIME, TYPE, UserName);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Page contains error.')", true);
            Response.Redirect("ErrorPage.aspx?PageHeader=Error Page");
        }
    }
    protected void Upload_Vostro()
    {
        #region Vostro Data Consolidation
        try
        {
            todate = Session["ToRelDt"].ToString().Trim();
            string filedate = todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4);
            // Mumbai File Check
            DataTable dtCsv = new DataTable();
            string Fulltext;
            string mumbaiVostropath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/BR_Mumbai_ConsoFile/Vostro_6770001_" + filedate + ".CSV");
            string newdelhiVostropath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/BR_NewDelhi_ConsoFile/Vostro_6770002_" + filedate + ".CSV");
            string BangaloreVostropath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/BR_Bangalore_ConsoFile/Vostro_6770003_" + filedate + ".CSV");
            string chennaiVostropath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/BR_Chennai_ConsoFile/Vostro_6770004_" + filedate + ".CSV");
            string[] pathArr = { mumbaiVostropath, newdelhiVostropath, BangaloreVostropath, chennaiVostropath };
            for (int j = 0; j < 4; j++)
            {
                string path = pathArr[j];
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        Fulltext = sr.ReadToEnd().ToString(); //read full file text  
                        string[] rows = Fulltext.Split('\n'); //split full file text into rows  
                        for (int i = 0; i < rows.Count() - 1; i++)
                        {
                            string[] rowValues = rows[i].Split(','); //split each row with comma to get individual values  
                            {
                                if (i != 0)
                                {
                                    SqlParameter adcode = new SqlParameter("@adcode", SqlDbType.VarChar);
                                    adcode.Value = rowValues[0].ToString();

                                    SqlParameter branchname = new SqlParameter("@branchname", SqlDbType.VarChar);
                                    branchname.Value = rowValues[1].ToString();

                                    SqlParameter fr_fortnight_dt = new SqlParameter("@fr_fortnight_dt", SqlDbType.VarChar);
                                    fr_fortnight_dt.Value = rowValues[2].ToString();

                                    SqlParameter to_fortnight_dt = new SqlParameter("@to_fortnight_dt", SqlDbType.VarChar);
                                    to_fortnight_dt.Value = rowValues[3].ToString();

                                    SqlParameter nv = new SqlParameter("@nv", SqlDbType.VarChar);
                                    nv.Value = rowValues[4].ToString();

                                    SqlParameter curr = new SqlParameter("@curr", SqlDbType.VarChar);
                                    curr.Value = rowValues[5].ToString();

                                    SqlParameter countrycode = new SqlParameter("@countrycode", SqlDbType.VarChar);
                                    countrycode.Value = rowValues[6].ToString();

                                    SqlParameter bankname = new SqlParameter("@bankname", SqlDbType.VarChar);
                                    bankname.Value = rowValues[7].ToString();

                                    SqlParameter op_d = new SqlParameter("@op_d", SqlDbType.VarChar);
                                    op_d.Value = rowValues[8].ToString();

                                    SqlParameter op_c = new SqlParameter("@op_c", SqlDbType.VarChar);
                                    op_c.Value = rowValues[9].ToString();

                                    SqlParameter cl_d = new SqlParameter("@cl_d", SqlDbType.VarChar);
                                    cl_d.Value = rowValues[10].ToString();

                                    SqlParameter cl_c = new SqlParameter("@cl_c", SqlDbType.VarChar);
                                    cl_c.Value = rowValues[11].ToString();

                                    SqlParameter bankcode = new SqlParameter("@bankcode", SqlDbType.VarChar);
                                    bankcode.Value = rowValues[12].ToString();

                                    TF_DATA objDataInput1 = new TF_DATA();
                                    string query = "TF_RET_VOSTRO_DATA_Consolidate";
                                    string dtInput1 = objDataInput1.SaveDeleteData(query, adcode, branchname, fr_fortnight_dt, to_fortnight_dt, nv, curr, countrycode, bankname,
                                        op_d, op_c, cl_d, cl_c, bankcode);
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
            ADCODE.Value = ddlBranch.SelectedValue.ToString();

            SqlParameter MENUNAME = new SqlParameter("@MENUNAME", SqlDbType.VarChar);
            MENUNAME.Value = "Consolidate Branch CSV File At Head OFfice";

            SqlParameter IPAddress = new SqlParameter("@IPAddress", SqlDbType.VarChar);
            IPAddress.Value = GetIPAddress();

            SqlParameter URL = new SqlParameter("@URL", SqlDbType.VarChar);
            URL.Value = HttpContext.Current.Request.Url.AbsoluteUri;

            SqlParameter TYPE = new SqlParameter("@TYPE", SqlDbType.VarChar);
            TYPE.Value = Ex.GetType().Name.ToString();

            SqlParameter Message = new SqlParameter("@Message", SqlDbType.VarChar);
            Message.Value = Ex.Message;

            SqlParameter StackTrace = new SqlParameter("@StackTrace", SqlDbType.VarChar);
            StackTrace.Value = Ex.StackTrace;

            SqlParameter Source = new SqlParameter("@Source", SqlDbType.VarChar);
            Source.Value = Ex.Source;

            SqlParameter TargetSite = new SqlParameter("@TargetSite", SqlDbType.VarChar);
            TargetSite.Value = Ex.TargetSite.ToString();

            SqlParameter DATETIME = new SqlParameter("@DATETIME", SqlDbType.VarChar);
            DATETIME.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

            SqlParameter UserName = new SqlParameter("@UserName", SqlDbType.VarChar);
            UserName.Value = Session["userName"].ToString().Trim(); ;

            TF_DATA objDataInput = new TF_DATA();
            string qryError = "TF_RET_ErrorException";
            string dtInput1 = objDataInput.SaveDeleteData(qryError, ADCODE, MENUNAME, IPAddress, URL, Message, StackTrace, Source, TargetSite, DATETIME, TYPE, UserName);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Page contains error.')", true);
            Response.Redirect("ErrorPage.aspx?PageHeader=Error Page");
        }
        #endregion
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
        SqlParameter p1 = new SqlParameter("@FromDate", txtFromDate.Text);
        SqlParameter p2 = new SqlParameter("@ToDate", txtToDate.Text);
        TF_DATA objdata = new TF_DATA();
        DataTable dt = objdata.getData("TF_RET_DeleteConsolidatedData", p1, p2);

        Upload_ERS();
        Upload_Nostro();
        Upload_Vostro();


        string script = "alert('All branches data consolidated.')";
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);

        labelmessage.ForeColor = System.Drawing.Color.Blue;
        labelmessage.Text = "All branches data consolidated.";
        }
        catch (Exception Ex)
        {
            SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
            ADCODE.Value = ddlBranch.SelectedValue.ToString();

            SqlParameter MENUNAME = new SqlParameter("@MENUNAME", SqlDbType.VarChar);
            MENUNAME.Value = "Consolidate Branch CSV File At Head OFfice";

            SqlParameter IPAddress = new SqlParameter("@IPAddress", SqlDbType.VarChar);
            IPAddress.Value = GetIPAddress();

            SqlParameter URL = new SqlParameter("@URL", SqlDbType.VarChar);
            URL.Value = HttpContext.Current.Request.Url.AbsoluteUri;

            SqlParameter TYPE = new SqlParameter("@TYPE", SqlDbType.VarChar);
            TYPE.Value = Ex.GetType().Name.ToString();

            SqlParameter Message = new SqlParameter("@Message", SqlDbType.VarChar);
            Message.Value = Ex.Message;

            SqlParameter StackTrace = new SqlParameter("@StackTrace", SqlDbType.VarChar);
            StackTrace.Value = Ex.StackTrace;

            SqlParameter Source = new SqlParameter("@Source", SqlDbType.VarChar);
            Source.Value = Ex.Source;

            SqlParameter TargetSite = new SqlParameter("@TargetSite", SqlDbType.VarChar);
            TargetSite.Value = Ex.TargetSite.ToString();

            SqlParameter DATETIME = new SqlParameter("@DATETIME", SqlDbType.VarChar);
            DATETIME.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

            SqlParameter UserName = new SqlParameter("@UserName", SqlDbType.VarChar);
            UserName.Value = Session["userName"].ToString().Trim(); ;

            TF_DATA objDataInput = new TF_DATA();
            string qryError = "TF_RET_ErrorException";
            string dtInput1 = objDataInput.SaveDeleteData(qryError, ADCODE, MENUNAME, IPAddress, URL, Message, StackTrace, Source, TargetSite, DATETIME, TYPE, UserName);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Page contains error.')", true);
            Response.Redirect("ErrorPage.aspx?PageHeader=Error Page");
        }
    }
    public static string GetIPAddress()
    {
        string ipAddress = string.Empty;
        foreach (IPAddress item in Dns.GetHostAddresses(HttpContext.Current.Request.UserHostAddress))
        {
            if (item.AddressFamily.ToString().Equals("InterNetwork"))
            {
                ipAddress = item.ToString();
                break;
            }
        }
        if (!string.IsNullOrEmpty(ipAddress))
        {
            return ipAddress;
        }
        foreach (IPAddress item in Dns.GetHostAddresses(Dns.GetHostName()))
        {
            if (item.AddressFamily.ToString().Equals("InterNetwork"))
            {
                ipAddress = item.ToString();
                break;
            }
        }
        return ipAddress;
    }
}