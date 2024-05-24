using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class EDPMS_TF_EDPMS_FIRC_Created : System.Web.UI.Page
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
                //PageHeader.Text = Request.QueryString["PageHeader"].ToString();


              
                btnSave.Attributes.Add("onclick", "return Generate();");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                ddlBranch.Focus();

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
        //else
        //    li.Text = "No record(s) found";

       // ddlBranch.Items.Insert(0, li);


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string NoofshippingBill = "";
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";

        DateTime documentDate = Convert.ToDateTime(txtfromDate.Text.Trim(), dateInfo);
        DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);

        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchCode", SqlDbType.VarChar);
        p1.Value = ddlBranch.Text.Trim();

        SqlParameter p2 = new SqlParameter("@fromdate", SqlDbType.VarChar);
        p2.Value = txtfromDate.Text.ToString();

        SqlParameter p3 = new SqlParameter("@todate", SqlDbType.VarChar);
        p3.Value = txtToDate.Text.ToString();

        
           
            string Date = System.DateTime.Now.ToString("ddMMyyyy");
            string a = ddlBranch.Text;
            string _qurey = "TF_EDPMS_Generate_XML_File_EFIRC_Document";
            DataTable dt = objData.getData(_qurey, p1, p2, p3);
            if (dt.Rows.Count > 0)
            {

                string _directoryPath = Server.MapPath("../TF_GeneratedFiles/EDPMS/" + a);
                if (!Directory.Exists(_directoryPath))
                {
                    Directory.CreateDirectory(_directoryPath);
                }

                string query = "TF_EDPMS_GenerateFileName_EFIRC";
                DataTable dtfile = objData.getData(query);
                string filename1 = dtfile.Rows[0]["FileName"].ToString();

                string _filePath = _directoryPath + "/" + filename1 + ".irfirc.xml";
                StreamWriter sw;
                sw = File.CreateText(_filePath);

                sw.WriteLine(dt.Rows[0]["XMLStartTag"].ToString());
                sw.WriteLine("<bank>");
                sw.WriteLine("<checkSum>");
                sw.WriteLine("<noOfIrm>" + dt.Rows[0]["NO_OF_FIRC"].ToString() + "</noOfIrm>");
                sw.WriteLine("</checkSum>");
                sw.WriteLine("<remittances>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sw.WriteLine("<remittance>");
                    sw.WriteLine("<IRMNumber>" + dt.Rows[i]["IRMNo"].ToString() + "</IRMNumber>");
                    sw.WriteLine("<adCode>" + dt.Rows[i]["ADCode"].ToString() + "</adCode>");
                    sw.WriteLine("<fircFlag>" + dt.Rows[i]["FIRCFLAG"].ToString() + "</fircFlag>");
                    sw.WriteLine("<fircNumber>" + dt.Rows[i]["Firc_No"].ToString() + "</fircNumber>");
                    sw.WriteLine("<fircIssueDate>" + dt.Rows[i]["Firc_Issuse_Date"].ToString() + "</fircIssueDate>");
                    sw.WriteLine("<fircAmount>" + dt.Rows[i]["Firc_Amount"].ToString() + "</fircAmount>");
                    sw.WriteLine("<recordIndicator>" + dt.Rows[i]["RecordIn"].ToString() + "</recordIndicator>");
                    sw.WriteLine("</remittance>");

                    string query5 = "TF_EDPMS_Insert_EFRIC_Data_Created";

                    SqlParameter IRMNo = new SqlParameter("@IRMNO", SqlDbType.VarChar);
                    IRMNo.Value = dt.Rows[i]["IRMNo"].ToString();

                    SqlParameter adcode = new SqlParameter("@adcode", SqlDbType.VarChar);
                    adcode.Value = dt.Rows[i]["ADCode"].ToString();

                    SqlParameter fircflag = new SqlParameter("@FIRCFLAG",SqlDbType.VarChar);
                    fircflag.Value = dt.Rows[i]["FIRCFLAG"].ToString();

                    SqlParameter fircno = new SqlParameter("@FIRCNO",SqlDbType.VarChar);
                    fircno.Value = dt.Rows[i]["Firc_No"].ToString();

                    SqlParameter FIRCISSUEDATE = new SqlParameter("@FIRCISSUEDATE", SqlDbType.VarChar);
                    FIRCISSUEDATE.Value = dt.Rows[i]["Firc_Issuse_Date"].ToString();

                    SqlParameter Firc_Amt = new SqlParameter("@FIRCAMOUNT", SqlDbType.VarChar);
                    Firc_Amt.Value = dt.Rows[i]["Firc_Amount"].ToString();

                    SqlParameter filename = new SqlParameter("@filename", SqlDbType.VarChar);
                    filename.Value = filename1;
                    
                    SqlParameter addedby = new SqlParameter("@addedby", SqlDbType.VarChar);
                    addedby.Value = Session["userName"].ToString();
                    
                    SqlParameter addedtime = new SqlParameter("@addedtime", SqlDbType.VarChar);
                    addedtime.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                    objData.SaveDeleteData(query5, IRMNo, adcode, fircflag, fircno, FIRCISSUEDATE, Firc_Amt, filename, addedby, addedtime);

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