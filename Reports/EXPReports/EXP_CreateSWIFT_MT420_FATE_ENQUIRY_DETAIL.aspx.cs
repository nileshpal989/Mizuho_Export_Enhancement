using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class Reports_EXPReports_EXP_CreateSWIFT_MT420_FATE_ENQUIRY_DETAIL : System.Web.UI.Page
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
                //txtFromDate.Focus();
                //fillProcessingDate();
                //txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

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

    //protected void fillProcessingDate()
    //{
    //    if (Session["startdate"] != null)
    //    {
    //        txtFromDate.Text = Session["startdate"].ToString();
    //        hdnFromDate.Value = Session["startdateMM"].ToString();
    //    }


    //}
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


        //System.Globalization.DateTimeFormatInfo dateInfo1 = new System.Globalization.DateTimeFormatInfo();
        //dateInfo1.ShortDatePattern = "dd/MM/yyyy";
        //DateTime documentDate1 = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo1);

        //SqlParameter o1 = new SqlParameter("@DocDate", SqlDbType.VarChar);
        //string _DocDT = documentDate1.ToString("MM/dd/yyyy");
        //o1.Value = _DocDT;
        SqlParameter o2 = new SqlParameter("@Frdocno", SqlDbType.VarChar);
        o2.Value = Frdoc;
        SqlParameter o3 = new SqlParameter("@Todocno", SqlDbType.VarChar);
        o3.Value = Todoc;

        SqlParameter o4 = new SqlParameter("@docno", SqlDbType.VarChar);
        o4.Value = doc;

        string _qry2 = "TF_Create_EXP_SWIFT_MT420_FATE_ENQUIRY";
        DataTable dt2 = objData2.getData(_qry2, o2, o3, o4);
        int number = 0;


        if (dt2.Rows.Count > 0)
        {
            for (int j = 0; j < dt2.Rows.Count; j++)
            {
                string _filePath1 = "";
                _filePath1 = _directoryPath + "/" + "MT-420-" + dt2.Rows[j]["DOC_NO"].ToString().Replace("/", "-") + ".txt";
                StreamWriter sw1;
                sw1 = File.CreateText(_filePath1);


                string Len = dt2.Rows[j]["W_SWIFT_CODE"].ToString().Trim().Replace(" ", "");
                if (Len.Length < 8 || Len.Length > 13)
                {
                    labelMessage.Text = "Invalid BIC Code, check it in Overseas Bank Master";
                    sw1.Flush();
                    sw1.Close();
                    sw1.Dispose();
                    //txtFromDate.Focus();
                    rdbAllDocNo.Focus();
                    return;

                }

                string W_COUNT_TEMP = number.ToString("0000000000");
                sw1.WriteLine((char)(1) + "{1:F01" + dt2.Rows[j]["Branch_Swift_Code"].ToString().Trim() + W_COUNT_TEMP + "}{2:I420" + dt2.Rows[j]["W_SWIFT_CODE"].ToString().Trim() + "}{4:");
                sw1.WriteLine(":20:" + dt2.Rows[j]["Document_No"].ToString().Trim());

                string _strRemark1 = "";
                _strRemark1 = txtRemark1.Text.Trim();
                if (_strRemark1 != "")
                {
                    if (_strRemark1.Length <= 16)
                    {
                        sw1.WriteLine(":21:" + _strRemark1);
                    }
                    if (_strRemark1.Length > 16)
                    {
                        sw1.WriteLine(":21:" + _strRemark1.Substring(0, 16));
                    }
                }
                else
                {
                    sw1.WriteLine(":21:" + "NONREF");
                }

                sw1.WriteLine(":32A:" + dt2.Rows[j]["Other_Currency_For_INR"].ToString().Trim() + dt2.Rows[j]["Amount"].ToString().Trim());
                sw1.WriteLine(":30:" + dt2.Rows[j]["Date_Received"].ToString().Trim());


                

                    string _str59A = "";
                    _str59A = dt2.Rows[j]["CUST_NAME"].ToString().Trim();
                    if (_str59A != "")
                    {
                        if (_str59A.Length <= 34)
                        {
                            sw1.WriteLine(":59:" + _str59A);
                        }

                        if (_str59A.Length > 34)
                        {
                            sw1.WriteLine(":59:" + _str59A.Substring(0, 34));
                            //if (_str59A.Substring(34) != "")
                            //{
                            //    sw1.WriteLine(_str59A.Substring(34));
                            //}
                        }
                    }



                    string _str59B = "";
                    //string _str59C = "";
                    _str59B = dt2.Rows[j]["CUST_ADDRESS"].ToString().Trim();
                    _str59B = _str59B.Replace("/", "-");
                    _str59B = _str59B.Replace("(", "");
                    _str59B = _str59B.Replace(")", "");
                    _str59B = _str59B.Replace("&", "-");

                    //_str59C = dt2.Rows[j]["CUST_PINCODE"].ToString().Trim();

                    if (_str59B != "")
                    {
                        if (_str59B.Length <= 34)
                        {
                            sw1.WriteLine(_str59B);
                        }

                        if (_str59B.Length > 34 && _str59B.Length <= 69)
                        {
                            sw1.WriteLine(_str59B.Substring(0, 34));

                            if (_str59B.Substring(34) != "")
                            {
                                sw1.WriteLine(_str59B.Substring(34));
                            }
                        }
                        if (_str59B.Length > 69 && _str59B.Length <= 104)
                        {
                            sw1.WriteLine(_str59B.Substring(0, 34));
                            sw1.WriteLine(_str59B.Substring(34, 34));

                            if (_str59B.Substring(69) != "")
                            {
                                sw1.WriteLine(_str59B.Substring(69));
                            }
                        }
                        if (_str59B.Length > 104 && _str59B.Length <= 139)
                        {
                            sw1.WriteLine(_str59B.Substring(0, 34));
                            sw1.WriteLine(_str59B.Substring(34, 34));
                            sw1.WriteLine(_str59B.Substring(69, 34));


                            //if (_str59B.Substring(105) != "")
                            //{
                            //    sw1.WriteLine(_str59B.Substring(105));
                            //}
                        }
                        if (_str59B.Length > 140 && _str59B.Length <= 175)
                        {
                            sw1.WriteLine(_str59B.Substring(0, 34));
                            sw1.WriteLine(_str59B.Substring(34, 34));
                            sw1.WriteLine(_str59B.Substring(69, 34));
                            //sw1.WriteLine(_str59B.Substring(105, 35));
                            //if (_str59B.Substring(140) != "")
                            //{
                            //    sw1.WriteLine(_str59B.Substring(140));
                            //}
                        }
                        if (_str59B.Length > 175)
                        {
                            sw1.WriteLine(_str59B.Substring(0, 34));
                            sw1.WriteLine(_str59B.Substring(34, 34));
                            sw1.WriteLine(_str59B.Substring(69, 34));
                            //sw1.WriteLine(_str59B.Substring(105, 35));
                            //if (_str59B.Substring(140) != "")
                            //{
                            //    sw1.WriteLine(_str59B.Substring(140));
                            //}
                        }

                        //if (_str59B.Length >= 35)
                        //{
                        //    if (_str59C != "")
                        //    {
                        //        sw1.WriteLine(_str59C);
                        //    }
                        //}
                    }

                sw1.WriteLine(".");
                string _strRemark2 = "";
                _strRemark2 = txtRemark2.Text.Trim();
                if (_strRemark2 != "")
                {
                    if (_strRemark2.Length <= 45)
                    {
                        sw1.WriteLine(":72:" + _strRemark2);
                    }

                    if (_strRemark2.Length > 45 && _strRemark2.Length <= 94)
                    {
                        sw1.WriteLine(":72:" + _strRemark2.Substring(0, 45));
                        if (_strRemark2.Substring(45) != "")
                        {
                            sw1.WriteLine(_strRemark2.Substring(45));
                        }
                    }
                    if (_strRemark2.Length > 94 && _strRemark2.Length <= 143)
                    {
                        sw1.WriteLine(":72:" + _strRemark2.Substring(0, 45));
                        sw1.WriteLine(_strRemark2.Substring(45, 49));
                        if (_strRemark2.Substring(94) != "")
                        {
                            sw1.WriteLine(_strRemark2.Substring(94));
                        }
                    }
                    if (_strRemark2.Length > 143 && _strRemark2.Length <= 192)
                    {
                        sw1.WriteLine(":72:" + _strRemark2.Substring(0, 45));
                        sw1.WriteLine(_strRemark2.Substring(45, 49));
                        sw1.WriteLine(_strRemark2.Substring(94, 49));
                        if (_strRemark2.Substring(143) != "")
                        {
                            sw1.WriteLine(_strRemark2.Substring(143));
                        }
                    }
                    if (_strRemark2.Length > 192 && _strRemark2.Length <= 241)
                    {
                        sw1.WriteLine(":72:" + _strRemark2.Substring(0, 45));
                        sw1.WriteLine(_strRemark2.Substring(45, 49));
                        sw1.WriteLine(_strRemark2.Substring(94, 49));
                        sw1.WriteLine(_strRemark2.Substring(143, 49));
                        if (_strRemark2.Substring(192) != "")
                        {
                            sw1.WriteLine(_strRemark2.Substring(192));
                        }
                    }
                    if (_strRemark2.Length > 241 && _strRemark2.Length <= 290)
                    {
                        sw1.WriteLine(":72:" + _strRemark2.Substring(0, 45));
                        sw1.WriteLine(_strRemark2.Substring(45, 49));
                        sw1.WriteLine(_strRemark2.Substring(94, 49));
                        sw1.WriteLine(_strRemark2.Substring(143, 49));
                        sw1.WriteLine(_strRemark2.Substring(192, 49));
                        if (_strRemark2.Substring(241) != "")
                        {
                            sw1.WriteLine(_strRemark2.Substring(241));
                        }
                    }
                    if (_strRemark2.Length > 290 && _strRemark2.Length <= 339)
                    {
                        sw1.WriteLine(":72:" + _strRemark2.Substring(0, 45));
                        sw1.WriteLine(_strRemark2.Substring(45, 49));
                        sw1.WriteLine(_strRemark2.Substring(94, 49));
                        sw1.WriteLine(_strRemark2.Substring(143, 49));
                        sw1.WriteLine(_strRemark2.Substring(192, 49));
                        sw1.WriteLine(_strRemark2.Substring(241, 49));
                        if (_strRemark2.Substring(290) != "")
                        {
                            sw1.WriteLine(_strRemark2.Substring(290));
                        }
                    }
                    if (_strRemark2.Length > 339 && _strRemark2.Length <= 388)
                    {
                        sw1.WriteLine(":72:" + _strRemark2.Substring(0, 45));
                        sw1.WriteLine(_strRemark2.Substring(45, 49));
                        sw1.WriteLine(_strRemark2.Substring(94, 49));
                        sw1.WriteLine(_strRemark2.Substring(143, 49));
                        sw1.WriteLine(_strRemark2.Substring(192, 49));
                        sw1.WriteLine(_strRemark2.Substring(241, 49));
                        sw1.WriteLine(_strRemark2.Substring(290, 49));
                        if (_strRemark2.Substring(339) != "")
                        {
                            sw1.WriteLine(_strRemark2.Substring(339));
                        }
                    }
                    if (_strRemark2.Length > 388 && _strRemark2.Length <= 437)
                    {
                        sw1.WriteLine(":72:" + _strRemark2.Substring(0, 45));
                        sw1.WriteLine(_strRemark2.Substring(45, 49));
                        sw1.WriteLine(_strRemark2.Substring(94, 49));
                        sw1.WriteLine(_strRemark2.Substring(143, 49));
                        sw1.WriteLine(_strRemark2.Substring(192, 49));
                        sw1.WriteLine(_strRemark2.Substring(241, 49));
                        sw1.WriteLine(_strRemark2.Substring(290, 49));
                        sw1.WriteLine(_strRemark2.Substring(339, 49));
                        if (_strRemark2.Substring(388) != "")
                        {
                            sw1.WriteLine(_strRemark2.Substring(388));
                        }
                    }
                    if (_strRemark2.Length > 437 && _strRemark2.Length <= 486)
                    {
                        sw1.WriteLine(":72:" + _strRemark2.Substring(0, 45));
                        sw1.WriteLine(_strRemark2.Substring(45, 49));
                        sw1.WriteLine(_strRemark2.Substring(94, 49));
                        sw1.WriteLine(_strRemark2.Substring(143, 49));
                        sw1.WriteLine(_strRemark2.Substring(192, 49));
                        sw1.WriteLine(_strRemark2.Substring(241, 49));
                        sw1.WriteLine(_strRemark2.Substring(290, 49));
                        sw1.WriteLine(_strRemark2.Substring(339, 49));
                        sw1.WriteLine(_strRemark2.Substring(388, 49));
                        if (_strRemark2.Substring(437) != "")
                        {
                            sw1.WriteLine(_strRemark2.Substring(437));
                        }
                    }
                    if (_strRemark2.Length > 486 && _strRemark2.Length <= 535)
                    {
                        sw1.WriteLine(":72:" + _strRemark2.Substring(0, 45));
                        sw1.WriteLine(_strRemark2.Substring(45, 49));
                        sw1.WriteLine(_strRemark2.Substring(94, 49));
                        sw1.WriteLine(_strRemark2.Substring(143, 49));
                        sw1.WriteLine(_strRemark2.Substring(192, 49));
                        sw1.WriteLine(_strRemark2.Substring(241, 49));
                        sw1.WriteLine(_strRemark2.Substring(290, 49));
                        sw1.WriteLine(_strRemark2.Substring(339, 49));
                        sw1.WriteLine(_strRemark2.Substring(388, 49));
                        sw1.WriteLine(_strRemark2.Substring(437, 49));
                        if (_strRemark2.Substring(486) != "")
                        {
                            sw1.WriteLine(_strRemark2.Substring(486));
                        }
                    }
                    if (_strRemark2.Length > 535 && _strRemark2.Length <= 584)
                    {
                        sw1.WriteLine(":72:" + _strRemark2.Substring(0, 45));
                        sw1.WriteLine(_strRemark2.Substring(45, 49));
                        sw1.WriteLine(_strRemark2.Substring(94, 49));
                        sw1.WriteLine(_strRemark2.Substring(143, 49));
                        sw1.WriteLine(_strRemark2.Substring(192, 49));
                        sw1.WriteLine(_strRemark2.Substring(241, 49));
                        sw1.WriteLine(_strRemark2.Substring(290, 49));
                        sw1.WriteLine(_strRemark2.Substring(339, 49));
                        sw1.WriteLine(_strRemark2.Substring(388, 49));
                        sw1.WriteLine(_strRemark2.Substring(437, 49));
                        sw1.WriteLine(_strRemark2.Substring(486, 49));
                        if (_strRemark2.Substring(535) != "")
                        {
                            sw1.WriteLine(_strRemark2.Substring(535));  
                        }
                    }
                    if (_strRemark2.Length > 584)
                    {
                        sw1.WriteLine(":72:" + _strRemark2.Substring(0, 45));
                        sw1.WriteLine(_strRemark2.Substring(45, 49));
                        sw1.WriteLine(_strRemark2.Substring(94, 49));
                        sw1.WriteLine(_strRemark2.Substring(143, 49));
                        sw1.WriteLine(_strRemark2.Substring(192, 49));
                        sw1.WriteLine(_strRemark2.Substring(241, 49));
                        sw1.WriteLine(_strRemark2.Substring(290, 49));
                        sw1.WriteLine(_strRemark2.Substring(339, 49));
                        sw1.WriteLine(_strRemark2.Substring(388, 49));
                        sw1.WriteLine(_strRemark2.Substring(437, 49));
                        sw1.WriteLine(_strRemark2.Substring(486, 49));
                        sw1.WriteLine(_strRemark2.Substring(535, 49));
                        //if (_strRemark2.Substring(539) != "")
                        //{
                        //    sw1.WriteLine(_strRemark2.Substring(539));
                        //}
                    }
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

            //txtFromDate.Focus();

        }

        else
        {
            labelMessage.Font.Bold = true;
            labelMessage.Text = "No Records found ";
            //txtFromDate.Focus();
        }
    }
}