using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Globalization;
using System.Net;

public partial class EDPMS_EDPMS_GenerateXMLFile_IRMAdjstmntClsr : System.Web.UI.Page
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

            string _query = "TF_IRMAdjstMntClsr";
            string a = ddlBranch.Text;
            DataTable dt = objData.getData(_query, p1, p2, p3);
            if (dt.Rows.Count > 0)
            {
                string _directoryPath = Server.MapPath("../TF_GeneratedFiles/EDPMS/" + a);
                if (!Directory.Exists(_directoryPath))
                {
                    Directory.CreateDirectory(_directoryPath);
                }
                string queryfilname = "EDPMS_IRM_FileCreation_SrNo";
                DataTable dtfile = objData.getData(queryfilname);
                string filename1 = dtfile.Rows[0]["FileName"].ToString();
                string _filePath = _directoryPath + "/" + filename1 + ".ircls.xml";
                StreamWriter sw;
                sw = File.CreateText(_filePath);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    noOfIrm = noOfIrm + 1;
                }

                sw.WriteLine(@"<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>");
                sw.WriteLine("<bank>");
                sw.WriteLine("<checkSum>");
                sw.WriteLine("<noOfIrm>" + noOfIrm + "</noOfIrm>");
                sw.WriteLine("</checkSum>");
                sw.WriteLine("<remittances>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // string FIRC_No = "", FIRC_Date = "", fircAmount = "", fundTransferFlag = "", remarks = "", remitterAddress = "", recordIndicator = "";

                    sw.WriteLine("<remittance>");
                    sw.WriteLine("<IRMNumber>" + dt.Rows[i]["IRMNO"].ToString() + "</IRMNumber>");
                    sw.WriteLine("<adCode>" + dt.Rows[i]["AdCode"].ToString() + "</adCode>");
                    sw.WriteLine("<ieCode>" + dt.Rows[i]["IECode"].ToString() + "</ieCode>");
                    sw.WriteLine("<currency>" + dt.Rows[i]["Currency"].ToString() + "</currency>");
                    sw.WriteLine("<adjustedAmount>" + Convert.ToDecimal(dt.Rows[i]["AmountAdjusted"].ToString()).ToString("0.00") + "</adjustedAmount>");
                    sw.WriteLine("<approvalBy>" + dt.Rows[i]["ApprovedBy"].ToString() + "</approvalBy>");
                    sw.WriteLine("<letterNo>" + dt.Rows[i]["LetterNo"].ToString() + "</letterNo>");
                    sw.WriteLine("<adjustmentDate>" + dt.Rows[i]["AdjsutmentDate"].ToString() + "</adjustmentDate>");
                    sw.WriteLine("<reasonForAdjustment>" + dt.Rows[i]["Reason"].ToString() + "</reasonForAdjustment>");
                    sw.WriteLine("<recordIndicator>" + dt.Rows[i]["RecordInd"].ToString() + "</recordIndicator>");
                    sw.WriteLine("<closureSequenceNo>" + dt.Rows[i]["ClosureSeqNo"].ToString() + "</closureSequenceNo>");
                    sw.WriteLine("<docNumber>" + dt.Rows[i]["DocumentNo"].ToString() + "</docNumber>");
                    sw.WriteLine("<docDate>" + dt.Rows[i]["DocDate"].ToString() + "</docDate>");
                    sw.WriteLine("<docPort>" + dt.Rows[i]["DocPort"].ToString() + "</docPort>");
                    sw.WriteLine("<remark>" + dt.Rows[i]["Remark"].ToString() + "</remark>");
                    sw.WriteLine("</remittance>");

                    string _query1 = "EDPMS_Insert_IRMAdjstClsr_FileCreation_XML";
                    SqlParameter filename = new SqlParameter("@FileName", SqlDbType.VarChar);
                    filename.Value = filename1;
                    SqlParameter IRMNumber = new SqlParameter("@IRMNO", SqlDbType.VarChar);
                    IRMNumber.Value = dt.Rows[i]["IRMNO"].ToString();
                    SqlParameter ADcode = new SqlParameter("@AdCode", SqlDbType.VarChar);
                    ADcode.Value = dt.Rows[i]["AdCode"].ToString();
                    SqlParameter IECode = new SqlParameter("@IECode", SqlDbType.VarChar);
                    IECode.Value = dt.Rows[i]["IECode"].ToString();
                    SqlParameter Curr = new SqlParameter("@Currency", SqlDbType.VarChar);
                    Curr.Value = dt.Rows[i]["Currency"].ToString();
                    SqlParameter AmtAdjst = new SqlParameter("@AmountAdjusted", SqlDbType.VarChar);
                    AmtAdjst.Value = dt.Rows[i]["AmountAdjusted"].ToString();
                    SqlParameter ApprvBy = new SqlParameter("@ApprovedBy", SqlDbType.VarChar);
                    ApprvBy.Value = dt.Rows[i]["ApprovedBy"].ToString();
                    SqlParameter LettrNo = new SqlParameter("@LetterNo", SqlDbType.VarChar);
                    LettrNo.Value = dt.Rows[i]["LetterNo"].ToString();
                    SqlParameter AdjstDt = new SqlParameter("@AdjsutmentDate", SqlDbType.VarChar);
                    AdjstDt.Value = dt.Rows[i]["AdjsutmentDate"].ToString();
                    SqlParameter Reason = new SqlParameter("@Reason", SqlDbType.VarChar);
                    Reason.Value = dt.Rows[i]["Reason"].ToString();
                    SqlParameter RecrdInd = new SqlParameter("@RecordInd", SqlDbType.VarChar);
                    RecrdInd.Value = dt.Rows[i]["RecordInd"].ToString();
                    SqlParameter ClsrSqNo = new SqlParameter("@ClosureSeqNo", SqlDbType.VarChar);
                    ClsrSqNo.Value = dt.Rows[i]["ClosureSeqNo"].ToString();
                    SqlParameter DocNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
                    DocNo.Value = dt.Rows[i]["DocumentNo"].ToString();
                    SqlParameter DocDt = new SqlParameter("@DocDate", SqlDbType.VarChar);
                    DocDt.Value = dt.Rows[i]["DocDate"].ToString();
                    SqlParameter DocPort = new SqlParameter("@DocPort", SqlDbType.VarChar);
                    DocPort.Value = dt.Rows[i]["DocPort"].ToString();
                    SqlParameter Rmark = new SqlParameter("@Remark", SqlDbType.VarChar);
                    Rmark.Value = dt.Rows[i]["recordind"].ToString();
                    SqlParameter AddedBy = new SqlParameter("@AddedBy", SqlDbType.VarChar);
                    AddedBy.Value = Session["userName"].ToString();
                    SqlParameter AddedDate = new SqlParameter("@AddedDate", SqlDbType.VarChar);
                    AddedDate.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                    objData.SaveDeleteData(_query1, filename, IRMNumber, ADcode, IECode, Curr, AmtAdjst, ApprvBy, LettrNo, AdjstDt,
                                           Reason, RecrdInd, ClsrSqNo, DocNo, DocDt, DocPort, Rmark, AddedBy, AddedDate);

                }
                sw.WriteLine("</remittances>");
                sw.WriteLine("</bank>");
                sw.Flush();
                sw.Close();
                sw.Dispose();
                TF_DATA objServerName = new TF_DATA();
                string _serverName = objServerName.GetServerName();
                lblmessage.Font.Bold = true;
                string path = "file://" + _serverName + "/TF_GeneratedFiles/EDPMS";
                string link = "/TF_GeneratedFiles/EDPMS/";
                lblmessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
            }
            else
            {
                lblmessage.Text = "No Records";
            }
        }
    }
}

