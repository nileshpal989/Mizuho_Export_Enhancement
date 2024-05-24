using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class Reports_EXPORTReports_EXP_CreateSWIFT_MT730_DETAIL : System.Web.UI.Page
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
    }

    protected void fillProcessingDate()
    {
        if (Session["startdate"] != null)
        {
            txtFromDate.Text = Session["startdate"].ToString();
            hdnFromDate.Value = Session["startdateMM"].ToString();
        }


    }

    //public void fillDocIdDescription()
    //{
    //    lblDocName.Text = "";
    //    System.Globalization.DateTimeFormatInfo dateInfo1 = new System.Globalization.DateTimeFormatInfo();
    //    dateInfo1.ShortDatePattern = "dd/MM/yyyy";
    //    DateTime documentDate1 = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo1);
    //    // string DocDate = txtFromDate.Text.Trim();


    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@DocDate", SqlDbType.VarChar);
    //    string _DocDT = documentDate1.ToString("MM/dd/yyyy");
    //    p1.Value = _DocDT;
    //    string _query = "TF_INW_getDocNoDetails";
    //    DataTable dt = objData.getData(_query, p1);
    //    if (dt.Rows.Count > 0)
    //    {
    //        lblDocName.Text = dt.Rows[0]["BankName"].ToString().Trim();
    //    }
    //    else
    //    {
    //        txtDocumentNo.Text = "";
    //        lblDocName.Text = "";
    //    }
    //}

    protected void btnDocId_Click(object sender, EventArgs e)
    {
        if (hdnDocId.Value != "")
        {
            txtDocumentNo.Text = hdnDocId.Value;
            //fillDocIdDescription();
            txtDocumentNo.Focus();
        }
    }

    protected void btnToDocId_Click(object sender, EventArgs e)
    {
        if (hdnToDocId.Value != "")
        {
            txtToDocumentNo.Text = hdnToDocId.Value;
            //fillToDocIdDescription();
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
        //fillDocIdDescription();
        txtDocumentNo.Focus();
        //lblDocName.Visible = true;
    }

    protected void txtToDocumentNo_TextChanged(object sender, EventArgs e)
    {
        //fillDocIdDescription();
        txtToDocumentNo.Focus();
        //lblDocName.Visible = true;
    }

    protected void btnCreateFile_Click(object sender, EventArgs e)
    {
        string _directoryPath = System.DateTime.Now.ToString("yyyy-MM-dd");
        _directoryPath = Server.MapPath("~/GeneratedFiles/SWIFT/" + _directoryPath + "/EXPORT");
        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }
        //For text file creation



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

        string _qry2 = "TF_CreateSWIFT_MT730_DETAIL";
        DataTable dt2 = objData2.getData(_qry2, o1, o2, o3, o4);
        //string UTRFlag = "R41";
        //string Sender = "NOSC0000MUM";
        int number = 0;


        if (dt2.Rows.Count > 0)
        {
            for (int j = 0; j < dt2.Rows.Count; j++)
            {
                string _filePath1 = _directoryPath  + "/" + "MT-730-" + dt2.Rows[j]["Our_Ref_No"].ToString().Replace("/", "-") + ".txt";
                StreamWriter sw1;
                sw1 = File.CreateText(_filePath1);


                string Len = dt2.Rows[j]["SwiftCode"].ToString().Trim();
                if (Len.Length != 8 && Len.Length != 11)
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

                sw1.WriteLine((char)(1) + "{1:F01" + dt2.Rows[j]["Branch_Swift_Code"].ToString().Trim() + W_COUNT_TEMP + "}{2:I730" + dt2.Rows[j]["W_SWIFT_CODE"].ToString().Trim() + "}{4:");
                sw1.WriteLine(":20:" + "EXP" + dt2.Rows[j]["Our_Ref_No"].ToString().Replace("//", "") + "-" + dt2.Rows[j]["Our_Ref_Year"].ToString().Replace("//", ""));
                
                sw1.WriteLine(":21:" + dt2.Rows[j]["LC_NO"].ToString().Replace("//", ""));
                
                sw1.WriteLine(":30:" + dt2.Rows[j]["LC_DATE"].ToString().Trim());

                string _str72 = "";

                _str72 = dt2.Rows[j]["Remark730"].ToString().Trim();
                _str72 = _str72.Replace("/", "_");
                _str72 = _str72.Replace("(", "");
                _str72 = _str72.Replace(")", "");
                _str72 = _str72.Replace("&", "_");

                
                if (_str72 != "")
                {
                    if (_str72.Length <= 35)
                    {
                        sw1.WriteLine(":72:" + _str72);
                    }

                    if (_str72.Length > 35 && _str72.Length <= 70)
                    {
                        sw1.WriteLine(":72:" + _str72.Substring(0, 35));

                        if (_str72.Substring(35) != "")
                        {
                            sw1.WriteLine(_str72.Substring(35));
                        }
                    }
                    if (_str72.Length > 70 && _str72.Length <= 105)
                    {
                        sw1.WriteLine(":72:" + _str72.Substring(0, 35));
                        sw1.WriteLine(_str72.Substring(35, 35));

                        if (_str72.Substring(70) != "")
                        {
                            sw1.WriteLine(_str72.Substring(70));
                        }
                    }
                    if (_str72.Length > 105 && _str72.Length <= 140)
                    {
                        sw1.WriteLine(":72:" + _str72.Substring(0, 35));
                        sw1.WriteLine(_str72.Substring(35, 35));
                        sw1.WriteLine(_str72.Substring(70, 35));


                        if (_str72.Substring(105) != "")
                        {
                            sw1.WriteLine(_str72.Substring(105));
                        }
                    }
                    if (_str72.Length > 140 && _str72.Length <= 175)
                    {
                        sw1.WriteLine(":72:" + _str72.Substring(0, 35));
                        sw1.WriteLine(_str72.Substring(35, 35));
                        sw1.WriteLine(_str72.Substring(70, 35));
                        sw1.WriteLine(_str72.Substring(105, 35));
                        if (_str72.Substring(140) != "")
                        {
                            sw1.WriteLine(_str72.Substring(140));
                        }
                    }
                    if (_str72.Length > 175)
                    {
                        sw1.WriteLine(":72:" + _str72.Substring(0, 35));
                        sw1.WriteLine(_str72.Substring(35, 35));
                        sw1.WriteLine(_str72.Substring(70, 35));
                        sw1.WriteLine(_str72.Substring(105, 35));
                        sw1.WriteLine(_str72.Substring(140, 35));
                        if (_str72.Substring(175) != "")
                        {
                            sw1.WriteLine(_str72.Substring(175));
                        }
                    }
                }

                sw1.Write("-}" + (char)(3)) ;

             
                sw1.Flush();
                sw1.Close();
                sw1.Dispose();
                
                number = number + 1;
            }
            
            
            TF_DATA objServerName = new TF_DATA();
            string _serverName = objServerName.GetServerName();

            labelMessage.Font.Bold = true;
            labelMessage.Text = "Files Created Successfully on Server : " + _serverName + " in " + _directoryPath;
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
            