    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.IO;
    using System.Data.SqlClient;
    using System.Data;
    using System.Globalization;
    using System.Drawing;
    public partial class Reports_EXPReports_TF_EXPORT_MBILL_ENTRY_EXCELFILE_CREATION: System.Web.UI.Page

    {
        string cust;
    protected void Page_Load(object sender, EventArgs e)
    {
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
                    clearControls();
                    fillddlBranch();
                    ddlBranch.SelectedValue = Session["userADCode"].ToString();
                    ddlBranch.Enabled = false;
                    txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    //btnCreate.Attributes.Add("onclick", "return validateSave();");
                    //txtFromDate.Attributes.Add("onblur", "return LastDay();");
                    ddlBranch.Focus();

                }
                btnCreate.Attributes.Add("onclick", "return validateSave();");
                BtnCustList.Attributes.Add("onclick", "return Custhelp();");
                txtCustomer.Attributes.Add("onkeydown", "return CustId(event);");
            }

        }
    }

    protected void clearControls()
    {
        txtFromDate.Text = "";
        labelMessage.Text = "";
        txtToDate.Text = "";
        ddlBranch.Focus();
    }

    //protected void fillBranch()
    //{
    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
    //    p1.Value = "";
    //    string _query = "TF_GetBranchDetails";
    //    DataTable dt = objData.getData(_query, p1);
    //    ddlBranch.Items.Clear();
    //    ListItem li = new ListItem();
    //    li.Value = "0";
    //    if (dt.Rows.Count > 0)
    //    {
    //        li.Text = "---Select---";
    //        ddlBranch.DataSource = dt.DefaultView;
    //        ddlBranch.DataTextField = "BranchName";
    //        ddlBranch.DataValueField = "BranchName";
    //        ddlBranch.DataBind();
    //    }
    //    else
    //        li.Text = "No record(s) found";
    //    ddlBranch.Items.Insert(0, "All Branches");
    //    ddlBranch.Items.Insert(0, li);
    //}


    public void fillddlBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "";
        ListItem li01 = new ListItem();
        li01.Value = "All Branches";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            li01.Text = "All Branches";

            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
        ddlBranch.Items.Insert(1, li01);
    }

         protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";
        DateTime today = Convert.ToDateTime(txtFromDate.Text, dateInfo);

        today = today.AddMonths(1);

        DateTime lastday = today.AddDays(-(today.Day));

        txtToDate.Text = lastday.ToString("dd/MM/yyyy");
        txtToDate.Focus();  
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
    }


    public void fillCustomerIdDescription()
    {
        lblCustomerName.Text = "";
        string custid = txtCustomer.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@custid", SqlDbType.VarChar);
        p1.Value = custid;
        string _query = "TF_rptGGGetCustomerMasterDetails1";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCustomerName.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
        }
        else
        {
            txtCustomer.Text = "";
            lblCustomerName.Text = "";
        }
    }


    protected void rdbAllCustomer_CheckedChanged(object sender, EventArgs e)
    {
        rdbAllCustomer.Checked = true;
        CustList.Visible = false;
        rdbAllCustomer.Focus();
        
        
    }
    protected void rdbSelectedCustomer_CheckedChanged(object sender, EventArgs e)
    {
        CustList.Visible = true;
        txtCustomer.Text = "";
        lblCustomerName.Text = "Customer Name";
        txtCustomer.Visible = true;
        BtnCustList.Visible = true;
        lblCustomerName.Visible = true;
        rdbSelectedCustomer.Focus(); 
      

    }

    protected void txtCustomer_TextChanged(object sender, EventArgs e)
    {
        fillCustomerIdDescription();
        txtCustomer.Focus();

    }

    protected void btnCustClick_Click(object sender, EventArgs e)
    {
        fillCustomerIdDescription();
    }


   protected void btnCreate_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";

        if (rdbAllCustomer.Checked == true)
        {
            cust = "All";
        }
        if (rdbSelectedCustomer.Checked == true)
        {
            cust = txtCustomer.Text.Trim();
        }

         DateTime documentDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
        DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);

       string _directoryPath = ddlBranch.Text.ToString() + "-" + documentDate.ToString("ddMMyyyy") + "-" + documentDate1.ToString("ddMMyyyy");

       _directoryPath = Server.MapPath("~/GeneratedFiles/EXPORT/MBILL/"+ _directoryPath );



        //if (!Directory.Exists(_directoryPath))
        //{
        //    Directory.CreateDirectory(_directoryPath);
        //}
        //For csv file creation
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@Branch", SqlDbType.VarChar);
        p1.Value = ddlBranch.Text.ToString().Trim();

        SqlParameter p2 = new SqlParameter("@startdate", SqlDbType.VarChar);
        p2.Value = documentDate.ToString("MM/dd/yyyy");

        SqlParameter p3 = new SqlParameter("@enddate", SqlDbType.VarChar);
        p3.Value = documentDate1.ToString("MM/dd/yyyy");


        SqlParameter p4 = new SqlParameter("@cust", SqlDbType.VarChar);
        p4.Value = cust;

        string _qry = "TF_CRS_MBILL";
        DataTable dt = objData.getData(_qry, p1, p2, p3,p4);
        if (dt.Rows.Count > 0)
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + ddlBranch.Text.ToString() + documentDate.ToString("dd-MM-yyyy") + "-" + documentDate1.ToString("dd-MM-yyyy") + ".csv";
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            sw.WriteLine("BranchName,DocumentNo ,Date_Received,DocumentType,CustAcno,CustomerName,InvoiceNo,Currency,Bill Amount,TT/FIRCNo,Commission,Bank_Cert_Charges,Courier_Charges,STax_FxDls,ServiceTaxAmt,NetAmt");
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                /*1*/
                string BranchName = dt.Rows[j]["BranchName"].ToString().Trim();
                string BranchName1 = BranchName.Replace(",", " -");
                sw.Write(BranchName1 + ",");

                /*2*/
                string Document_No = dt.Rows[j]["Document_No"].ToString().Trim();
                string Document_No1 = Document_No.Replace(",", " -");
                sw.Write(Document_No1 + ",");

                /*3*/
                string Date_Received = dt.Rows[j]["Date_Received"].ToString().Trim();
                string Date_Received1 = Date_Received.Replace(",", " -");
                sw.Write(Date_Received1 + ",");

                /*4*/
                string Document_Type = dt.Rows[j]["Document_Type"].ToString().Trim();
                string Document_Type1 = Document_Type.Replace(",", " -");
                sw.Write(Document_Type1 + ",");


                /*5*/
                string CustAcNo = "'" + dt.Rows[j]["CustAcNo"].ToString().Trim();
                string CustAcNo1 = CustAcNo.Replace(",", " -");

                sw.Write(CustAcNo1 + ",");



                /*6*/
                string CUST_NAME = dt.Rows[j]["CUST_NAME"].ToString().Trim();
                string CUST_NAME1 = CUST_NAME.Replace(",", " -");
                sw.Write(CUST_NAME1 + ",");

                /*7*/
                string Invoice_No = dt.Rows[j]["Invoice_No"].ToString().Trim();
                string Invoice_No1 = Invoice_No.Replace(",", " -");
                sw.Write(Invoice_No1 + ",");


                /*8*/
                string currency = dt.Rows[j]["currency"].ToString().Trim();
                string currency1 = currency.Replace(",", " -");
                sw.Write(currency1 + ",");


                /*9*/
                string Bill_Amount = dt.Rows[j]["Bill_Amount"].ToString().Trim();
                string Bill_Amount1 = Bill_Amount.Replace(",", " -");
                sw.Write(Bill_Amount1 + ",");

                /*10*/
                string TTREFNO = dt.Rows[j]["TTREFNO"].ToString().Trim();
                string TTREFNO1 = TTREFNO.Replace(",", " -");
                sw.Write(TTREFNO1 + ",");

                /*11*/
                string Commission_Amount = dt.Rows[j]["Commission_Amount"].ToString().Trim();
                string Commission_Amount1 = Commission_Amount.Replace(",", " -");
                sw.Write(Commission_Amount1 + ",");


                /*12*/
                string Bank_Cert = dt.Rows[j]["Bank_Cert"].ToString().Trim();
                string Bank_Cert1 = Bank_Cert.Replace(",", " -");
                sw.Write(Bank_Cert1 + ",");


                /*13*/
                string Courier_Charges = dt.Rows[j]["Courier_Charges"].ToString().Trim();
                string Courier_Charges1 = Courier_Charges.Replace(",", " -");
                sw.Write(Courier_Charges1 + ",");


                /*14*/
                string STax_FxDls = dt.Rows[j]["STax_FxDls"].ToString().Trim();
                string STax_FxDls1 = STax_FxDls.Replace(",", " -");
                sw.Write(STax_FxDls1 + ",");


                /*15*/
                string STaxAmt = dt.Rows[j]["STaxAmt"].ToString().Trim();
                string STaxAmt1 = STaxAmt.Replace(",", " -");
                sw.Write(STaxAmt1 + ",");


                /*16*/
                string Net_Amount = dt.Rows[j]["Net_Amount"].ToString().Trim();
                string Net_Amount1 = Net_Amount.Replace(",", " -");
                sw.WriteLine(Net_Amount1);


                /*59*/
                //string _strUpdatedDate = dt.Rows[j]["UpdatedDate"].ToString().Trim();
                //string _strUpdatedDate1 = _strUpdatedDate.Replace(",", " -");
                //sw.WriteLine(_strUpdatedDate1);

            }
            sw.Flush();
            sw.Close();
            sw.Dispose();

            TF_DATA objServerName = new TF_DATA();
            string _serverName = objServerName.GetServerName();



            string path = "file://" + _serverName + "/GeneratedFiles/EXPORT/MBILL";
            string link = "/GeneratedFiles/EXPORT/MBILL";
            labelMessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";

            ////labelMessage.Text = "Files Created Successfully on Server : " + _serverName + " in " + _directoryPath;
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Files Created Successfully');", true);
            ddlBranch.Focus();
            //txtToDate.Text = "";
            //txtFromDate.Text = "";

        }
        else
        {
            //labelMessage.Text = "No Reocrds ";
            labelMessage.Text = "There is No Record Between This Dates " + documentDate.ToString("dd/MM/yyyy") + "-" + documentDate1.ToString("dd/MM/yyyy");
            //ddlBranch.Focus();

            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtFromDate.Focus();
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('There is No Record Between This Dates');", true);

        }
    }

  
  
    protected void ddlBranch_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        
        

    }

}