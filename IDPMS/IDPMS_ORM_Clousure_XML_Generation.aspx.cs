using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class IDPMS_IDPMS_ORM_Clousure_XML_Generation : System.Web.UI.Page
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
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;

                //ddlBranch.SelectedIndex = 1;

                btnSave.Attributes.Add("onclick", "return Generate();");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtfromDate.Focus();
            }
        }
    }

    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        //ListItem li = new ListItem();
        //li.Value = "All Branches";

        if (dt.Rows.Count > 0)
        {
            //li.Text = "All Branches";

            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        {
            int noOfIrm = 0;
            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            dateInfo.ShortDatePattern = "dd/MM/yyyy";

            DateTime documentDate = Convert.ToDateTime(txtfromDate.Text.Trim(), dateInfo);
            DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);

            TF_DATA objData = new TF_DATA();

            SqlParameter p1 = new SqlParameter("@fromdate", SqlDbType.VarChar);
            p1.Value = txtfromDate.Text.ToString();

            SqlParameter p2 = new SqlParameter("@todate", SqlDbType.VarChar);
            p2.Value = txtToDate.Text.ToString();

            SqlParameter p3 = new SqlParameter("@BranchName", SqlDbType.VarChar);
            p3.Value = ddlBranch.Text;

            string _query = "XML_ORMClosure_creation";
            string a = ddlBranch.Text;
            DataTable dt = objData.getData(_query, p1, p2, p3);
            if (dt.Rows.Count > 0)
            {
                string _directoryPath = Server.MapPath("../TF_GeneratedFiles/IDPMS/" + a);
                if (!Directory.Exists(_directoryPath))
                {
                    Directory.CreateDirectory(_directoryPath);
                }
                string queryfilname = "filename_ORM_Closure";
                DataTable dtfile = objData.getData(queryfilname);
                string filename1 = dtfile.Rows[0]["FileName"].ToString();
                string Adcode = ddlBranch.SelectedValue.ToString();

                string _filePath = _directoryPath + "/" + Adcode + "_" + filename1 + ".ora.xml";
                StreamWriter sw;
                sw = File.CreateText(_filePath);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    noOfIrm = noOfIrm + 1;
                }

                sw.WriteLine(dt.Rows[0]["XMLStartTag"].ToString());
                sw.WriteLine("<bank>");
                sw.WriteLine("<checkSum>");
                sw.WriteLine("<noOfORM>" + noOfIrm + "</noOfORM>");
                sw.WriteLine("</checkSum>");
                sw.WriteLine("<outwardReferences>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    sw.WriteLine("<outwardReference>");
                    sw.WriteLine("<outwardReferenceNumber>" + dt.Rows[i]["ORMNO"].ToString() + "</outwardReferenceNumber>");
                    sw.WriteLine("<ADCode>" + dt.Rows[i]["ADCODE"].ToString() + "</ADCode>");
                    sw.WriteLine("<remittanceCurrency>" + dt.Rows[i]["remittanceCurrency"].ToString() + "</remittanceCurrency>");
                    sw.WriteLine("<adjustedAmount>" + Convert.ToDecimal(dt.Rows[i]["adjustedAmount"].ToString()).ToString("0.00") + "</adjustedAmount>");
                    sw.WriteLine("<adjustedDate>" + dt.Rows[i]["adjustedDate"].ToString() + "</adjustedDate>");
                    sw.WriteLine("<IECode>" + dt.Rows[i]["IECode"].ToString() + "</IECode>");
                    sw.WriteLine("<SWIFT>" + dt.Rows[i]["SWIFT"].ToString() + "</SWIFT>");
                    sw.WriteLine("<adjustmentIndicator>" + dt.Rows[i]["adjustmentIndicator"].ToString() + "</adjustmentIndicator>");
                    sw.WriteLine("<adjustmentSeqNumber>" + dt.Rows[i]["adjustmentSeqNumber"].ToString() + "</adjustmentSeqNumber>");
                    sw.WriteLine("<approvedBy>" + dt.Rows[i]["ApproveBy"].ToString() + "</approvedBy>");
                    sw.WriteLine("<letterNumber>" + dt.Rows[i]["letterNumber"].ToString() + "</letterNumber>");
                    sw.WriteLine("<letterDate>" + dt.Rows[i]["letterDate"].ToString() + "</letterDate>");
                    sw.WriteLine("<documentNumber>" + dt.Rows[i]["documentNumber"].ToString() + "</documentNumber>");
                    sw.WriteLine("<documentDate>" + dt.Rows[i]["documentDate"].ToString() + "</documentDate>");
                    sw.WriteLine("<portofDischarge>" + dt.Rows[i]["portofDischarge"].ToString() + "</portofDischarge>");
                    sw.WriteLine("<recordIndicator>" + dt.Rows[i]["RecordInd"].ToString() + "</recordIndicator>");
                    sw.WriteLine("<remark>" + dt.Rows[i]["Remarks"].ToString() + "</remark>");
                    sw.WriteLine("</outwardReference>");

                    string _query1 = "Insert_ORM_XML_File_Generation_data";
                    SqlParameter filename = new SqlParameter("@Filname", SqlDbType.VarChar);
                    filename.Value = filename1;
                    SqlParameter ORMNo = new SqlParameter("@OrmNo", SqlDbType.VarChar);
                    ORMNo.Value = dt.Rows[i]["ORMNO"].ToString();
                    SqlParameter ADcode = new SqlParameter("@Adcode", SqlDbType.VarChar);
                    ADcode.Value = dt.Rows[i]["ADCODE"].ToString();
                    SqlParameter remitterCur = new SqlParameter("@remittercur", SqlDbType.VarChar);
                    remitterCur.Value = dt.Rows[i]["remittanceCurrency"].ToString();
                    SqlParameter Adjstamt = new SqlParameter("@AdjusmentAmt", SqlDbType.VarChar);
                    Adjstamt.Value = dt.Rows[i]["adjustedAmount"].ToString();
                    SqlParameter Adjstdate = new SqlParameter("@adjustDate", SqlDbType.VarChar);
                    Adjstdate.Value = dt.Rows[i]["adjustedDate"].ToString();
                    SqlParameter IECODE = new SqlParameter("@IECode", SqlDbType.VarChar);
                    IECODE.Value = dt.Rows[i]["IECode"].ToString();
                    SqlParameter Swift = new SqlParameter("@swift", SqlDbType.VarChar);
                    Swift.Value = dt.Rows[i]["SWIFT"].ToString();
                    SqlParameter AdjstIND = new SqlParameter("@adjusmentIND", SqlDbType.VarChar);
                    AdjstIND.Value = dt.Rows[i]["adjustmentIndicator"].ToString();
                    SqlParameter adjstseq = new SqlParameter("@AdjustseqNo", SqlDbType.VarChar);
                    adjstseq.Value = dt.Rows[i]["adjustmentSeqNumber"].ToString();
                    SqlParameter Lettrno = new SqlParameter("@letterNo", SqlDbType.VarChar);
                    Lettrno.Value = dt.Rows[i]["letterNumber"].ToString();
                    SqlParameter lettrdate = new SqlParameter("@letterdate", SqlDbType.VarChar);
                    lettrdate.Value = dt.Rows[i]["letterDate"].ToString();
                    SqlParameter DocNo = new SqlParameter("@docno", SqlDbType.VarChar);
                    DocNo.Value = dt.Rows[i]["documentNumber"].ToString();
                    SqlParameter DocDt = new SqlParameter("@Docdate", SqlDbType.VarChar);
                    DocDt.Value = dt.Rows[i]["documentDate"].ToString();
                    SqlParameter DocPort = new SqlParameter("@Portdischge", SqlDbType.VarChar);
                    DocPort.Value = dt.Rows[i]["portofDischarge"].ToString();
                    //SqlParameter Rmark = new SqlParameter("@Remark", SqlDbType.VarChar);
                    //Rmark.Value = dt.Rows[i]["recordind"].ToString();
                    SqlParameter AddedBy = new SqlParameter("@addedby", SqlDbType.VarChar);
                    AddedBy.Value = Session["userName"].ToString();
                    SqlParameter AddedDate = new SqlParameter("@addedate", SqlDbType.VarChar);
                    AddedDate.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                    objData.SaveDeleteData(_query1, filename, ORMNo, ADcode, remitterCur, Adjstamt, Adjstdate, IECODE, Swift, AdjstIND, adjstseq,
                          Lettrno, lettrdate, DocNo, DocDt, DocPort, AddedBy, AddedDate);


                }
                sw.WriteLine("</outwardReferences>");
                sw.WriteLine("</bank>");
                sw.Flush();
                sw.Close();
                sw.Dispose();
                TF_DATA objServerName = new TF_DATA();
                string _serverName = objServerName.GetServerName();
                lblmessage.Font.Bold = true;
                string path = "file://" + _serverName + "/TF_GeneratedFiles/IDPMS";
                string link = "/TF_GeneratedFiles/IDPMS/";
                lblmessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
            }
            else
            {
                lblmessage.Text = "No Records";
            }
        }
    }
    protected void btnsavcan_Click(object sender, EventArgs e)
    {
        int noOfIrm = 0;
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";

        DateTime documentDate = Convert.ToDateTime(txtfromDate.Text.Trim(), dateInfo);
        DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);

        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@fromdate", SqlDbType.VarChar);
        p1.Value = txtfromDate.Text.ToString();

        SqlParameter p2 = new SqlParameter("@todate", SqlDbType.VarChar);
        p2.Value = txtToDate.Text.ToString();

        SqlParameter p3 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p3.Value = ddlBranch.Text;

        string _query = "XML_ORMClosureCancel_creation";
        string a = ddlBranch.Text;
        DataTable dt = objData.getData(_query, p1, p2, p3);
        if (dt.Rows.Count > 0)
        {
            string _directoryPath = Server.MapPath("../TF_GeneratedFiles/IDPMS/" + a);
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string queryfilname = "filename_ORM_Closure_Cancel";
            DataTable dtfile = objData.getData(queryfilname);
            string filename1 = dtfile.Rows[0]["FileName"].ToString();
            string Adcode = ddlBranch.SelectedValue.ToString();

            string _filePath = _directoryPath + "/" + Adcode + "_" + filename1 + ".ora.xml";
            StreamWriter sw;
            sw = File.CreateText(_filePath);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                noOfIrm = noOfIrm + 1;
            }

            sw.WriteLine(dt.Rows[0]["XMLStartTag"].ToString());
            sw.WriteLine("<bank>");
            sw.WriteLine("<checkSum>");
            sw.WriteLine("<noOfORM>" + noOfIrm + "</noOfORM>");
            sw.WriteLine("</checkSum>");
            sw.WriteLine("<outwardReferences>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                sw.WriteLine("<outwardReference>");
                sw.WriteLine("<outwardReferenceNumber>" + dt.Rows[i]["ORMNO"].ToString() + "</outwardReferenceNumber>");
                sw.WriteLine("<ADCode>" + dt.Rows[i]["ADCODE"].ToString() + "</ADCode>");
                sw.WriteLine("<remittanceCurrency>" + dt.Rows[i]["remittanceCurrency"].ToString() + "</remittanceCurrency>");
                sw.WriteLine("<adjustedAmount>" + Convert.ToDecimal(dt.Rows[i]["adjustedAmount"].ToString()).ToString("0.00") + "</adjustedAmount>");
                sw.WriteLine("<adjustedDate>" + dt.Rows[i]["adjustedDate"].ToString() + "</adjustedDate>");
                sw.WriteLine("<IECode>" + dt.Rows[i]["IECode"].ToString() + "</IECode>");
                sw.WriteLine("<SWIFT>" + dt.Rows[i]["SWIFT"].ToString() + "</SWIFT>");
                sw.WriteLine("<adjustmentIndicator>" + dt.Rows[i]["adjustmentIndicator"].ToString() + "</adjustmentIndicator>");
                sw.WriteLine("<adjustmentSeqNumber>" + dt.Rows[i]["adjustmentSeqNumber"].ToString() + "</adjustmentSeqNumber>");
                sw.WriteLine("<approvedBy>" + dt.Rows[i]["ApproveBy"].ToString() + "</approvedBy>");
                sw.WriteLine("<letterNumber>" + dt.Rows[i]["letterNumber"].ToString() + "</letterNumber>");
                sw.WriteLine("<letterDate>" + dt.Rows[i]["letterDate"].ToString() + "</letterDate>");
                sw.WriteLine("<documentNumber>" + dt.Rows[i]["documentNumber"].ToString() + "</documentNumber>");
                sw.WriteLine("<documentDate>" + dt.Rows[i]["documentDate"].ToString() + "</documentDate>");
                sw.WriteLine("<portofDischarge>" + dt.Rows[i]["portofDischarge"].ToString() + "</portofDischarge>");
                sw.WriteLine("<recordIndicator>" + dt.Rows[i]["RecordInd"].ToString() + "</recordIndicator>");
                sw.WriteLine("<remark>" + dt.Rows[i]["Remarks"].ToString() + "</remark>");
                sw.WriteLine("</outwardReference>");

                string _query1 = "Insert_ORM_Cancel_XML_File_Generation_data";
                SqlParameter filename = new SqlParameter("@Filname", SqlDbType.VarChar);
                filename.Value = filename1;
                SqlParameter ORMNo = new SqlParameter("@OrmNo", SqlDbType.VarChar);
                ORMNo.Value = dt.Rows[i]["ORMNO"].ToString();
                SqlParameter ADcode = new SqlParameter("@Adcode", SqlDbType.VarChar);
                ADcode.Value = dt.Rows[i]["ADCODE"].ToString();
                SqlParameter remitterCur = new SqlParameter("@remittercur", SqlDbType.VarChar);
                remitterCur.Value = dt.Rows[i]["remittanceCurrency"].ToString();
                SqlParameter Adjstamt = new SqlParameter("@AdjusmentAmt", SqlDbType.VarChar);
                Adjstamt.Value = dt.Rows[i]["adjustedAmount"].ToString();
                SqlParameter Adjstdate = new SqlParameter("@adjustDate", SqlDbType.VarChar);
                Adjstdate.Value = dt.Rows[i]["adjustedDate"].ToString();
                SqlParameter IECODE = new SqlParameter("@IECode", SqlDbType.VarChar);
                IECODE.Value = dt.Rows[i]["IECode"].ToString();
                SqlParameter Swift = new SqlParameter("@swift", SqlDbType.VarChar);
                Swift.Value = dt.Rows[i]["SWIFT"].ToString();
                SqlParameter AdjstIND = new SqlParameter("@adjusmentIND", SqlDbType.VarChar);
                AdjstIND.Value = dt.Rows[i]["adjustmentIndicator"].ToString();
                SqlParameter adjstseq = new SqlParameter("@AdjustseqNo", SqlDbType.VarChar);
                adjstseq.Value = dt.Rows[i]["adjustmentSeqNumber"].ToString();
                SqlParameter Lettrno = new SqlParameter("@letterNo", SqlDbType.VarChar);
                Lettrno.Value = dt.Rows[i]["letterNumber"].ToString();
                SqlParameter lettrdate = new SqlParameter("@letterdate", SqlDbType.VarChar);
                lettrdate.Value = dt.Rows[i]["letterDate"].ToString();
                SqlParameter DocNo = new SqlParameter("@docno", SqlDbType.VarChar);
                DocNo.Value = dt.Rows[i]["documentNumber"].ToString();
                SqlParameter DocDt = new SqlParameter("@Docdate", SqlDbType.VarChar);
                DocDt.Value = dt.Rows[i]["documentDate"].ToString();
                SqlParameter DocPort = new SqlParameter("@Portdischge", SqlDbType.VarChar);
                DocPort.Value = dt.Rows[i]["portofDischarge"].ToString();
                //SqlParameter Rmark = new SqlParameter("@Remark", SqlDbType.VarChar);
                //Rmark.Value = dt.Rows[i]["recordind"].ToString();
                SqlParameter AddedBy = new SqlParameter("@addedby", SqlDbType.VarChar);
                AddedBy.Value = Session["userName"].ToString();
                SqlParameter AddedDate = new SqlParameter("@addedate", SqlDbType.VarChar);
                AddedDate.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                objData.SaveDeleteData(_query1, filename, ORMNo, ADcode, remitterCur, Adjstamt, Adjstdate, IECODE, Swift, AdjstIND, adjstseq,
                      Lettrno, lettrdate, DocNo, DocDt, DocPort, AddedBy, AddedDate);


            }
            sw.WriteLine("</outwardReferences>");
            sw.WriteLine("</bank>");
            sw.Flush();
            sw.Close();
            sw.Dispose();
            TF_DATA objServerName = new TF_DATA();
            string _serverName = objServerName.GetServerName();
            lblmessage.Font.Bold = true;
            string path = "file://" + _serverName + "/TF_GeneratedFiles/IDPMS";
            string link = "/TF_GeneratedFiles/IDPMS/";
            lblmessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
        }
        else
        {
            lblmessage.Text = "No Records";
        }
    }
}

