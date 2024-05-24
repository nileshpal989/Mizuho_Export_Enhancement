using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class Reports_EXPReports_EXP_CreateSWIFT__MT799_REIMBURSEMENT_DETAIL : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu.FindControl("hdnloginid");

            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            labelMessage.Text = "";
            txtDocumentNo.Attributes.Add("onkeydown", "return DocId(event)");
            txtToDocumentNo.Attributes.Add("onkeydown", "return ToDocId(event)");
            btnDocList.Attributes.Add("onclick", "return dochelp()");
            btnToDocList.Attributes.Add("onclick", "return Todochelp()");
            if (!IsPostBack)
            {
                clearControls();
                rdbAllDocNo.Checked = true;
                txtFromDate.Focus();
                fillProcessingDate();
                txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                //txtFromDate.Attributes.Add("onkeypress", "return false;");
                //txtFromDate.Attributes.Add("oncut", "return false;");
                //txtFromDate.Attributes.Add("oncopy", "return false;");
                //txtFromDate.Attributes.Add("onpaste", "return false;");
                //txtFromDate.Attributes.Add("oncontextmenu", "return false;");
                //txtFromDate.Attributes.Add("onblur", "toDate();");
                btnCreateFile.Attributes.Add("onclick", "return validateSave();");
                btnSave.Attributes.Add("onclick", "return validateGenerate();");
                txtRemark1.Text = "";
                txtRemark2.Text = "";                
            }
            else
            {
                labelMessage.Text = "";

            }
        }
    }

    protected void clearControls()
    {
        txtFromDate.Text = "";
        labelMessage.Text = "";
        rdbAllDocNo.Focus();
        rdbSelectedDocNo.Checked = false;
        txtDocumentNo.Visible = false;
        btnDocList.Visible = false;
        txtToDocumentNo.Visible = false;
        btnToDocList.Visible = false;
        Doccode.Visible = false;
        ToDoccode.Visible = false;
        txtDocumentNo.Text = "";
        txtToDocumentNo.Text = "";
        txtRemark1.Text = "";
        txtRemark2.Text = "";        
    }

    protected void fillProcessingDate()
    {
        if (Session["startdate"] != null)
        {
            txtFromDate.Text = Session["startdate"].ToString();
            hdnFromDate.Value = Session["startdateMM"].ToString();
        }


    }
    protected void btnDocId_Click(object sender, EventArgs e)
    {
        if (hdnDocId.Value != "")
        {
            txtDocumentNo.Text = hdnDocId.Value;
            txtDocumentNo.Focus();
        }
    }

    protected void btnToDocId_Click(object sender, EventArgs e)
    {
        if (hdnToDocId.Value != "")
        {
            txtToDocumentNo.Text = hdnToDocId.Value;
            txtToDocumentNo.Focus();
        }
    }

    protected void rdbAllDocNo_CheckedChanged(object sender, EventArgs e)
    {
        rdbAllDocNo.Checked = true;
        rdbAllDocNo.Focus();
        rdbSelectedDocNo.Checked = false;
        txtDocumentNo.Visible = false;
        btnDocList.Visible = false;
        Doccode.Visible = false;
        txtToDocumentNo.Visible = false;
        btnToDocList.Visible = false;
        ToDoccode.Visible = false;
        txtDocumentNo.Text = "";
        txtToDocumentNo.Text = "";
    }
    protected void rdbSelectedDocNo_CheckedChanged(object sender, EventArgs e)
    {
        rdbSelectedDocNo.Checked = true;
        rdbSelectedDocNo.Focus();
        txtDocumentNo.Visible = true;
        btnDocList.Visible = true;
        Doccode.Visible = true;
        txtToDocumentNo.Visible = true;
        btnToDocList.Visible = true;
        ToDoccode.Visible = true;
        rdbAllDocNo.Checked = false;

    }

    protected void txtDocumentNo_TextChanged(object sender, EventArgs e)
    {
        txtDocumentNo.Focus();
    }

    protected void txtToDocumentNo_TextChanged(object sender, EventArgs e)
    {
        txtToDocumentNo.Focus();
    }

    protected void btnCreateFile_Click(object sender, EventArgs e)
    {
        string _directoryPath = System.DateTime.Now.ToString("yyyy-MM-dd");
        _directoryPath = Server.MapPath("~/GeneratedFiles/SWIFT/" + _directoryPath + "/EXPORT");
        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }
        TF_DATA objData2 = new TF_DATA();
        string doc = "";
        string Frdoc = "";
        string Todoc = "";

        if (rdbAllDocNo.Checked == true)
        {
            doc = "All";
            Frdoc = "";
            Todoc = "";
        }
        else if (rdbSelectedDocNo.Checked == true)
        {
            doc = "Single";
            Frdoc = txtDocumentNo.Text.ToString().Trim();
            Todoc = txtToDocumentNo.Text.ToString().Trim();
        }


        System.Globalization.DateTimeFormatInfo dateInfo1 = new System.Globalization.DateTimeFormatInfo();
        dateInfo1.ShortDatePattern = "dd/MM/yyyy";
        DateTime documentDate1 = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo1);

        SqlParameter o1 = new SqlParameter("@DocDate", SqlDbType.VarChar);
        string _DocDT = documentDate1.ToString("MM/dd/yyyy");
        o1.Value = _DocDT;
        SqlParameter o2 = new SqlParameter("@Frdocno", SqlDbType.VarChar);
        o2.Value = Frdoc;
        SqlParameter o3 = new SqlParameter("@Todocno", SqlDbType.VarChar);
        o3.Value = Todoc;

        SqlParameter o4 = new SqlParameter("@docno", SqlDbType.VarChar);
        o4.Value = doc;

        string _qry2 = "TF_Create_EXP_SWIFT_MT799_REIMBURSEMENT";
        DataTable dt2 = objData2.getData(_qry2, o1, o2, o3, o4);
        int number = 0;


        if (dt2.Rows.Count > 0)
        {
            for (int j = 0; j < dt2.Rows.Count; j++)
            {
                string _filePath1 = "";
                _filePath1 = _directoryPath + "/" + "MT-799-" + dt2.Rows[j]["DOC_NO"].ToString().Replace("/", "-") + ".txt";
                StreamWriter sw1;
                sw1 = File.CreateText(_filePath1);


                string Len = dt2.Rows[j]["W_SWIFT_CODE"].ToString().Trim();
                if (Len.Length < 8 || Len.Length > 13)
                {
                    labelMessage.Text = "Invalid BIC Code, check it in Overseas Bank Master";
                    sw1.Flush();
                    sw1.Close();
                    sw1.Dispose();
                    txtFromDate.Focus();
                    rdbAllDocNo.Focus();
                    return;

                }

                string W_COUNT_TEMP = number.ToString("0000000000");
                sw1.WriteLine((char)(1) + "{1:F01" + dt2.Rows[j]["Branch_Swift_Code"].ToString().Trim() + W_COUNT_TEMP + "}{2:I799" + dt2.Rows[j]["W_SWIFT_CODE"].ToString().Trim() + "}{4:");
                sw1.WriteLine(":20:" + dt2.Rows[j]["Document_No"].ToString().Trim());
                sw1.WriteLine(":21:" + dt2.Rows[j]["LC_NO"].ToString().Trim().Replace(" ", ""));
                if (txtRemark1.Text != "")
                {
                    sw1.WriteLine(":79:" + "TEST NO : " + txtRemark1.Text.Trim());
                }
                sw1.WriteLine("TEST AMOUNT : " + dt2.Rows[j]["Amount"].ToString().Trim());

                string _strRemark2 = "";
                _strRemark2 = txtRemark2.Text.Trim();
                if (_strRemark2 != "")
                {
                    if (_strRemark2.Length <= 30)
                    {
                        sw1.WriteLine("TESTED BETWEEN : " + _strRemark2);
                    }

                    if (_strRemark2.Length > 30 && _strRemark2.Length <= 79)
                    {
                        sw1.WriteLine("TESTED BETWEEN : " + _strRemark2.Substring(0, 30));
                        if (_strRemark2.Substring(30) != "")
                        {
                            sw1.WriteLine(_strRemark2.Substring(30));
                        }
                    }
                    if (_strRemark2.Length > 79)
                    {
                        sw1.WriteLine("TESTED BETWEEN : " + _strRemark2.Substring(0, 30));
                        sw1.WriteLine(_strRemark2.Substring(30, 49));
                        if (_strRemark2.Substring(79) != "")
                        {
                            sw1.WriteLine(_strRemark2.Substring(79));
                        }
                    }
                }
                sw1.WriteLine(".");
                sw1.WriteLine("L.C. NO.  : " + dt2.Rows[j]["LC_NO"].ToString().Trim());
                sw1.WriteLine("L.C. DATE : " + dt2.Rows[j]["LC_Date"].ToString().Trim());
                sw1.WriteLine(".");
                sw1.WriteLine("OUR REF. : " + dt2.Rows[j]["Document_No"].ToString().Trim());
                sw1.WriteLine(".");

                string _strLC_ISSUED_BY = "";
                _strLC_ISSUED_BY = dt2.Rows[j]["LC_ISSUED_BY"].ToString().Trim();
                if (_strLC_ISSUED_BY != "")
                {
                    if (_strLC_ISSUED_BY.Length <= 35)
                    {
                        sw1.WriteLine("ISSUED BY : " + _strLC_ISSUED_BY);
                    }

                    if (_strLC_ISSUED_BY.Length > 35 && _strLC_ISSUED_BY.Length <= 70)
                    {
                        sw1.WriteLine("ISSUED BY : " + _strLC_ISSUED_BY.Substring(0, 35));
                        if (_strLC_ISSUED_BY.Substring(35) != "")
                        {
                            sw1.WriteLine(_strLC_ISSUED_BY.Substring(35));
                        }
                    }
                }

                sw1.WriteLine(".");
                sw1.WriteLine("AMOUNT CLAIMED : " + dt2.Rows[j]["Other_Currency_For_INR"].ToString().Trim() + "  " + dt2.Rows[j]["Amount"].ToString().Trim());
                sw1.WriteLine(".");
                sw1.WriteLine("VALUE DATE : " + dt2.Rows[j]["Reimbursement_ValDate"].ToString().Trim());
                sw1.WriteLine(".");


                string _strCustName = "";
                _strCustName = dt2.Rows[j]["CUST_NAME"].ToString().Trim();
                if (_strCustName != "")
                {
                    if (_strCustName.Length <= 35)
                    {
                        sw1.WriteLine("DRAWER : " + _strCustName);
                    }

                    if (_strCustName.Length > 35 && _strCustName.Length <= 70)
                    {
                        sw1.WriteLine("DRAWER : " + _strCustName.Substring(0, 35));
                        if (_strCustName.Substring(35) != "")
                        {
                            sw1.WriteLine(_strCustName.Substring(35));
                        }
                    }

                }
                else
                {
                    sw1.WriteLine("DRAWER : ");
                }

                sw1.WriteLine(".");

                string _str59A = "";
                _str59A = dt2.Rows[j]["Party_Name"].ToString().Trim();
                if (_str59A != "")
                {
                    if (_str59A.Length <= 35)
                    {
                        sw1.WriteLine("DRAWEE : " + _str59A);
                    }

                    if (_str59A.Length > 35 && _str59A.Length <= 70)
                    {
                        sw1.WriteLine("DRAWEE : " + _str59A.Substring(0, 35));
                        if (_str59A.Substring(35) != "")
                        {
                            sw1.WriteLine(_str59A.Substring(35));
                        }
                    }

                }
                else
                {
                    sw1.WriteLine("DRAWEE : ");
                }

                sw1.WriteLine(".");

                sw1.WriteLine("WE HAVE SENT DOCUMENTS UNDER SUBJECT ");
                sw1.WriteLine("LETTER OF CREDIT.");                

                sw1.WriteLine("AS PER L.C. TERMS WE HAVE BEEN AUTHORISED BY LC");
                sw1.WriteLine("ISSUING BANK TO CLAIM REIMBURSEMENT FROM");
                sw1.WriteLine("YOURSELVES.");
                sw1.WriteLine(".");
                sw1.WriteLine("PLEASE CREDIT THE AMOUNT");
                sw1.WriteLine("OF " + dt2.Rows[j]["Other_Currency_For_INR"].ToString().Trim() + "  " + dt2.Rows[j]["Amount"].ToString().Trim() + " BY TELEGRAPHIC");

                string rptCurr = "";
                rptCurr = dt2.Rows[j]["Other_Currency_For_INR"].ToString().Trim();
                string rptC_ADD1 = "";
                rptC_ADD1 = dt2.Rows[j]["C_ADD1"].ToString().Trim();

                if (rptCurr != "")
                {
                    sw1.WriteLine("TRANSFER TO OUR " + dt2.Rows[j]["C_ADD2"].ToString().Trim() + "  HELD WITH");
                    if (rptC_ADD1 != "")
                    {
                        if (rptC_ADD1.Length <= 49)
                        {
                            sw1.WriteLine(rptC_ADD1);
                        }

                        if (rptC_ADD1.Length > 49 && rptC_ADD1.Length <= 98)
                        {
                            sw1.WriteLine(rptC_ADD1.Substring(0, 49));
                            if (rptC_ADD1.Substring(49) != "")
                            {
                                sw1.WriteLine(rptC_ADD1.Substring(49));
                            }
                        }
                    }
                }
                else
                {
                    labelMessage.Text = "Address & Ac No not entered for Credited Bank (Currency) " + dt2.Rows[j]["Other_Currency_For_INR"].ToString().Trim();
                    sw1.Flush();
                    sw1.Close();
                    sw1.Dispose();
                    txtFromDate.Focus();
                    rdbAllDocNo.Focus();
                    return;
                }



                sw1.WriteLine("PLEASE QUOTE OUR REF NO.");
                sw1.WriteLine(".");
                sw1.WriteLine(".");
                sw1.WriteLine("REGARDS");
                sw1.WriteLine(".");
                sw1.WriteLine(".");

                if (dt2.Rows[j]["C_DESCRIPTION"].ToString().Trim() != "")
                {
                    sw1.WriteLine(dt2.Rows[j]["C_DESCRIPTION"].ToString().Trim());
                }
                sw1.Write("-}" + (char)(3));


                sw1.Flush();
                sw1.Close();
                sw1.Dispose();

                number = number + 1;
            }


            TF_DATA objServerName = new TF_DATA();
            string _serverName = objServerName.GetServerName();

            labelMessage.Font.Bold = true;
            //labelMessage.Text = "Files Created Successfully on Server : " + _serverName + " in " + _directoryPath;

            string path = "file://" + _serverName + "/GeneratedFiles/SWIFT/" + System.DateTime.Now.ToString("yyyy-MM-dd") + "/EXPORT";
            string link = "/GeneratedFiles/SWIFT/" + System.DateTime.Now.ToString("yyyy-MM-dd") + "/EXPORT";
            labelMessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";

            txtFromDate.Focus();

        }

        else
        {
            labelMessage.Font.Bold = true;
            labelMessage.Text = "No Records found ";
            txtFromDate.Focus();
        }
    }
}