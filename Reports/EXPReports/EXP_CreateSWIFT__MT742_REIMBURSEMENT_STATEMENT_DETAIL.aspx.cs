using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
public partial class Reports_EXPReports_EXP_CreateSWIFT__MT742_REIMBURSEMENT_STATEMENT_DETAIL : System.Web.UI.Page
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

        string _qry2 = "TF_Create_EXP_SWIFT__MT742_REIMBURSEMENT_STATEMENT";
        DataTable dt2 = objData2.getData(_qry2, o1, o2, o3, o4);
        int number = 0;


        if (dt2.Rows.Count > 0)
        {
            for (int j = 0; j < dt2.Rows.Count; j++)
            {
                string _filePath1 = "";
                _filePath1 = _directoryPath + "/" + "MT-742-" + dt2.Rows[j]["DOC_NO"].ToString().Replace("/", "-") + ".txt";
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
                sw1.WriteLine((char)(1) + "{1:F01" + dt2.Rows[j]["Branch_Swift_Code"].ToString().Trim() + W_COUNT_TEMP + "}{2:I742" + dt2.Rows[j]["W_SWIFT_CODE"].ToString().Trim() + "}{4:");
                sw1.WriteLine(":20:" + dt2.Rows[j]["Document_No"].ToString().Trim());
                sw1.WriteLine(":21:" + dt2.Rows[j]["LC_NO"].ToString().Trim().Replace(" ", ""));
                sw1.WriteLine(":31C:" + dt2.Rows[j]["LC_Date"].ToString().Trim());

                string BankCode = "";
                BankCode = dt2.Rows[j]["BankCode"].ToString().Trim();
                if (BankCode != "")
                {
                    string OB_SwiftCode = "";
                    OB_SwiftCode = dt2.Rows[j]["OB_SwiftCode"].ToString().Trim();
                    if (OB_SwiftCode != "")
                    {
                        if (OB_SwiftCode.Length == 8 || OB_SwiftCode.Length == 11 || OB_SwiftCode.Length == 13)
                        {
                            sw1.WriteLine(":52A:" + OB_SwiftCode);
                        }
                        else
                        {
                            string _str52DB = "";
                            _str52DB = dt2.Rows[j]["BankName"].ToString().Trim();
                            if (_str52DB != "")
                            {
                                if (_str52DB.Length <= 35)
                                {
                                    sw1.WriteLine(":52D:" + _str52DB);
                                }

                                if (_str52DB.Length > 35 && _str52DB.Length <= 70)
                                {
                                    sw1.WriteLine(":52D:" + _str52DB.Substring(0, 35));

                                }

                             }

                            string _str52DC = "";
                            _str52DC = dt2.Rows[j]["BankAddress"].ToString().Trim();
                            _str52DC = _str52DC.Replace("/", "-");
                            _str52DC = _str52DC.Replace("(", "");
                            _str52DC = _str52DC.Replace(")", "");

                            if (_str52DC != "")
                            {
                                if (_str52DC.Length <= 35)
                                {
                                    sw1.WriteLine(_str52DC);
                                }

                                if (_str52DC.Length > 35 && _str52DC.Length <= 70)
                                {
                                    sw1.WriteLine(_str52DC.Substring(0, 35));

                                    if (_str52DC.Substring(35) != "")
                                    {
                                        sw1.WriteLine(_str52DC.Substring(35));
                                    }
                                }
                                if (_str52DC.Length > 70 && _str52DC.Length <= 105)
                                {
                                    sw1.WriteLine(_str52DC.Substring(0, 35));
                                    sw1.WriteLine(_str52DC.Substring(35, 35));

                                    if (_str52DC.Substring(70) != "")
                                    {
                                        sw1.WriteLine(_str52DC.Substring(70));
                                    }
                                }
                                if (_str52DC.Length > 105)
                                {
                                    sw1.WriteLine(_str52DC.Substring(0, 35));
                                    sw1.WriteLine(_str52DC.Substring(35, 35));
                                    sw1.WriteLine(_str52DC.Substring(70, 35));
                                }
                            }
                        }


                    }
                }
                else
                {
                    string _str52D = "";
                    _str52D = dt2.Rows[j]["LC_ISSUED_BY"].ToString().Trim();
                    if (_str52D != "")
                    {
                        if (_str52D.Length <= 34)
                        {
                            sw1.WriteLine(":52D:" + _str52D);
                        }

                        if (_str52D.Length > 34 && _str52D.Length <= 70)
                        {
                            sw1.WriteLine(":52D:" + _str52D.Substring(0, 35));

                        }

                    }    
                }

                sw1.WriteLine(":32B:" + dt2.Rows[j]["Other_Currency_For_INR"].ToString().Trim() + dt2.Rows[j]["Amount"].ToString().Trim());

                string Reimbursement_ValDate = "";
                Reimbursement_ValDate = dt2.Rows[j]["Reimbursement_DT"].ToString().Trim();
                if (Reimbursement_ValDate != "")
                {
                    sw1.WriteLine(":34A:" + dt2.Rows[j]["Reimbursement_ValDate"].ToString().Trim() + dt2.Rows[j]["Other_Currency_For_INR"].ToString().Trim() + dt2.Rows[j]["Amount"].ToString().Trim());
                }
                else
                {
                    sw1.WriteLine(":34B:" + dt2.Rows[j]["Other_Currency_For_INR"].ToString().Trim() + dt2.Rows[j]["Amount"].ToString().Trim());
                }

                string _str57A = "";
                _str57A = dt2.Rows[j]["C_SWIFTCODE"].ToString().Trim();
                if (_str57A != "")
                {
                    sw1.WriteLine(":57A:" + _str57A);
                }
                
                sw1.WriteLine(":58A:" +  dt2.Rows[j]["MD_58A"].ToString().Trim());
                sw1.WriteLine(dt2.Rows[j]["MD_58B"].ToString().Trim());
                
                string _strRemark2 = "";
                _strRemark2 = txtRemark2.Text.Trim();
                if (_strRemark2 != "")
                {
                    if (_strRemark2.Length <= 35)
                    {
                        sw1.WriteLine(":72:" + _strRemark2);
                    }

                    if (_strRemark2.Length > 35 && _strRemark2.Length <= 70)
                    {
                        sw1.WriteLine(":72:" + _strRemark2.Substring(0, 35));

                        if (_strRemark2.Substring(35) != "")
                        {
                            sw1.WriteLine(_strRemark2.Substring(35));
                        }
                    }
                    if (_strRemark2.Length > 70 && _strRemark2.Length <= 105)
                    {

                        sw1.WriteLine(":72:" + _strRemark2.Substring(0, 35));
                        sw1.WriteLine(_strRemark2.Substring(35, 35));

                        if (_strRemark2.Substring(70) != "")
                        {
                            sw1.WriteLine(_strRemark2.Substring(70));
                        }
                    }
                    if (_strRemark2.Length > 105 && _strRemark2.Length <= 140)
                    {
                        sw1.WriteLine(":72:" + _strRemark2.Substring(0, 35));
                        sw1.WriteLine(_strRemark2.Substring(35, 35));
                        sw1.WriteLine(_strRemark2.Substring(70, 35));

                        if (_strRemark2.Substring(105) != "")
                        {
                            sw1.WriteLine(_strRemark2.Substring(105));
                        }
                    }


                    if (_strRemark2.Length > 140 && _strRemark2.Length <= 175)
                    {
                        sw1.WriteLine(":72:" + _strRemark2.Substring(0, 35));
                        sw1.WriteLine(_strRemark2.Substring(35, 35));
                        sw1.WriteLine(_strRemark2.Substring(70, 35));
                        sw1.WriteLine(_strRemark2.Substring(105, 35));
                        if (_strRemark2.Substring(140) != "")
                        {
                            sw1.WriteLine(_strRemark2.Substring(140));
                        }
                    }
                    if (_strRemark2.Length > 175)
                    {
                        sw1.WriteLine(":72:" + _strRemark2.Substring(0, 35));
                        sw1.WriteLine(_strRemark2.Substring(35, 35));
                        sw1.WriteLine(_strRemark2.Substring(70, 35));
                        sw1.WriteLine(_strRemark2.Substring(105, 35));
                        sw1.WriteLine(_strRemark2.Substring(140, 35));
                        if (_strRemark2.Substring(175) != "")
                        {
                            sw1.WriteLine(_strRemark2.Substring(175));
                        }
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