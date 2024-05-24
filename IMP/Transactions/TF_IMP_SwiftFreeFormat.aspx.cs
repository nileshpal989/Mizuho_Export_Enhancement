using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using System.IO;
public partial class IMP_Transactions_TF_IMP_SwiftFreeFormat : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("../../TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_IMP_SwiftFreeFormat_View.aspx?ID=" + Request.QueryString["ID"].ToString(), true);
                }
                else
                {
                    lbl_Header.Text = Request.QueryString["ID"].ToString();

                    fillBranch();
                    ddlBranch.SelectedValue = Session["userADCode"].ToString();

                    Span_ApproveReject.Visible = false;

                    if (Request.QueryString["ID"].ToString() == "Swift Free Format Message - Checker")
                    {
                        Panal_FreeFormatSwift.Enabled = false;
                        Panal_FreeFormatNarrative.Enabled = false;
                        btnSubmit.Visible = false;
                        Span_ApproveReject.Visible = true;
                    }

                    if (Request.QueryString["mode"].ToString() != "add")
                    {
                        txt_TransRefNo.Text = Request.QueryString["Document_No"].ToString();
                        ddlSwiftTypes.SelectedValue = Request.QueryString["Swift_Type"].ToString();
                        Fill_Details();
                    }
                }
            }
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
        ListItem li = new ListItem();
        li.Value = "";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);

    }
    protected void Fill_Details()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter p_SwiftType = new SqlParameter("@Swift_type", ddlSwiftTypes.SelectedItem.Text.ToUpper());
        SqlParameter p_txt_TransRefNo = new SqlParameter("@Document_No", txt_TransRefNo.Text.Trim().ToUpper());

        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_Swift_FreeFormat_Details", p_txt_TransRefNo, p_SwiftType);
        if (dt.Rows.Count > 0)
        {
            ddlBranch.Enabled = false;
            txt_TransRefNo.Enabled = false;
            ddlSwiftTypes.Enabled = false;
            txt_Receiver.Text = dt.Rows[0]["Receiver"].ToString();
            ddlSwiftTypes.SelectedValue = dt.Rows[0]["Swift_Type"].ToString();
            ddlBranch.SelectedValue = dt.Rows[0]["AuthorizedDealerCode"].ToString();
            txt_TransRefNo.Text = dt.Rows[0]["Document_No"].ToString();
            txt_RelRef.Text = dt.Rows[0]["RelatedRef"].ToString();

            Narrative1.Text = dt.Rows[0]["Narrative1"].ToString();
            Narrative2.Text = dt.Rows[0]["Narrative2"].ToString();
            Narrative3.Text = dt.Rows[0]["Narrative3"].ToString();
            Narrative4.Text = dt.Rows[0]["Narrative4"].ToString();
            Narrative5.Text = dt.Rows[0]["Narrative5"].ToString();
            Narrative6.Text = dt.Rows[0]["Narrative6"].ToString();
            Narrative7.Text = dt.Rows[0]["Narrative7"].ToString();
            Narrative8.Text = dt.Rows[0]["Narrative8"].ToString();
            Narrative9.Text = dt.Rows[0]["Narrative9"].ToString();
            Narrative10.Text = dt.Rows[0]["Narrative10"].ToString();
            Narrative11.Text = dt.Rows[0]["Narrative11"].ToString();
            Narrative12.Text = dt.Rows[0]["Narrative12"].ToString();
            Narrative13.Text = dt.Rows[0]["Narrative13"].ToString();
            Narrative14.Text = dt.Rows[0]["Narrative14"].ToString();
            Narrative15.Text = dt.Rows[0]["Narrative15"].ToString();
            Narrative16.Text = dt.Rows[0]["Narrative16"].ToString();
            Narrative17.Text = dt.Rows[0]["Narrative17"].ToString();
            Narrative18.Text = dt.Rows[0]["Narrative18"].ToString();
            Narrative19.Text = dt.Rows[0]["Narrative19"].ToString();
            Narrative20.Text = dt.Rows[0]["Narrative20"].ToString();
            Narrative21.Text = dt.Rows[0]["Narrative21"].ToString();
            Narrative22.Text = dt.Rows[0]["Narrative22"].ToString();
            Narrative23.Text = dt.Rows[0]["Narrative23"].ToString();
            Narrative24.Text = dt.Rows[0]["Narrative24"].ToString();
            Narrative25.Text = dt.Rows[0]["Narrative25"].ToString();
            Narrative26.Text = dt.Rows[0]["Narrative26"].ToString();
            Narrative27.Text = dt.Rows[0]["Narrative27"].ToString();
            Narrative28.Text = dt.Rows[0]["Narrative28"].ToString();
            Narrative29.Text = dt.Rows[0]["Narrative29"].ToString();
            Narrative30.Text = dt.Rows[0]["Narrative30"].ToString();
            Narrative31.Text = dt.Rows[0]["Narrative31"].ToString();
            Narrative32.Text = dt.Rows[0]["Narrative32"].ToString();
            Narrative33.Text = dt.Rows[0]["Narrative33"].ToString();
            Narrative34.Text = dt.Rows[0]["Narrative34"].ToString();
            Narrative35.Text = dt.Rows[0]["Narrative35"].ToString();

        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMP_SwiftFreeFormat_View.aspx?ID=" + Request.QueryString["ID"].ToString(), true);
    }
    protected void btnSave_Click()
    {
        #region SaveMassage
        SqlParameter P_BranchCode = new SqlParameter("@Branch_Code", ddlBranch.SelectedItem.Value.ToUpper());
        SqlParameter p_SwiftType = new SqlParameter("@Swift_type", ddlSwiftTypes.SelectedItem.Value.ToUpper());
        SqlParameter p_txt_Receiver = new SqlParameter("@Receiver", txt_Receiver.Text.Trim().ToUpper());
        SqlParameter p_txt_TransRefNo = new SqlParameter("@Document_No", txt_TransRefNo.Text.Trim().ToUpper());
        SqlParameter p_txt_RelRef = new SqlParameter("@RelatedRef", txt_RelRef.Text.Trim().ToUpper());

        SqlParameter P_Narrative1 = new SqlParameter("@Narrative1", Narrative1.Text.ToUpper());
        SqlParameter P_Narrative2 = new SqlParameter("@Narrative2", Narrative2.Text.ToUpper());
        SqlParameter P_Narrative3 = new SqlParameter("@Narrative3", Narrative3.Text.ToUpper());
        SqlParameter P_Narrative4 = new SqlParameter("@Narrative4", Narrative4.Text.ToUpper());
        SqlParameter P_Narrative5 = new SqlParameter("@Narrative5", Narrative5.Text.ToUpper());
        SqlParameter P_Narrative6 = new SqlParameter("@Narrative6", Narrative6.Text.ToUpper());
        SqlParameter P_Narrative7 = new SqlParameter("@Narrative7", Narrative7.Text.ToUpper());
        SqlParameter P_Narrative8 = new SqlParameter("@Narrative8", Narrative8.Text.ToUpper());
        SqlParameter P_Narrative9 = new SqlParameter("@Narrative9", Narrative9.Text.ToUpper());
        SqlParameter P_Narrative10 = new SqlParameter("@Narrative10", Narrative10.Text.ToUpper());
        SqlParameter P_Narrative11 = new SqlParameter("@Narrative11", Narrative11.Text.ToUpper());
        SqlParameter P_Narrative12 = new SqlParameter("@Narrative12", Narrative12.Text.ToUpper());
        SqlParameter P_Narrative13 = new SqlParameter("@Narrative13", Narrative13.Text.ToUpper());
        SqlParameter P_Narrative14 = new SqlParameter("@Narrative14", Narrative14.Text.ToUpper());
        SqlParameter P_Narrative15 = new SqlParameter("@Narrative15", Narrative15.Text.ToUpper());
        SqlParameter P_Narrative16 = new SqlParameter("@Narrative16", Narrative16.Text.ToUpper());
        SqlParameter P_Narrative17 = new SqlParameter("@Narrative17", Narrative17.Text.ToUpper());
        SqlParameter P_Narrative18 = new SqlParameter("@Narrative18", Narrative18.Text.ToUpper());
        SqlParameter P_Narrative19 = new SqlParameter("@Narrative19", Narrative19.Text.ToUpper());
        SqlParameter P_Narrative20 = new SqlParameter("@Narrative20", Narrative20.Text.ToUpper());
        SqlParameter P_Narrative21 = new SqlParameter("@Narrative21", Narrative21.Text.ToUpper());
        SqlParameter P_Narrative22 = new SqlParameter("@Narrative22", Narrative22.Text.ToUpper());
        SqlParameter P_Narrative23 = new SqlParameter("@Narrative23", Narrative23.Text.ToUpper());
        SqlParameter P_Narrative24 = new SqlParameter("@Narrative24", Narrative24.Text.ToUpper());
        SqlParameter P_Narrative25 = new SqlParameter("@Narrative25", Narrative25.Text.ToUpper());
        SqlParameter P_Narrative26 = new SqlParameter("@Narrative26", Narrative26.Text.ToUpper());
        SqlParameter P_Narrative27 = new SqlParameter("@Narrative27", Narrative27.Text.ToUpper());
        SqlParameter P_Narrative28 = new SqlParameter("@Narrative28", Narrative28.Text.ToUpper());
        SqlParameter P_Narrative29 = new SqlParameter("@Narrative29", Narrative29.Text.ToUpper());
        SqlParameter P_Narrative30 = new SqlParameter("@Narrative30", Narrative30.Text.ToUpper());
        SqlParameter P_Narrative31 = new SqlParameter("@Narrative31", Narrative31.Text.ToUpper());
        SqlParameter P_Narrative32 = new SqlParameter("@Narrative32", Narrative32.Text.ToUpper());
        SqlParameter P_Narrative33 = new SqlParameter("@Narrative33", Narrative33.Text.ToUpper());
        SqlParameter P_Narrative34 = new SqlParameter("@Narrative34", Narrative34.Text.ToUpper());
        SqlParameter P_Narrative35 = new SqlParameter("@Narrative35", Narrative35.Text.ToUpper());

        SqlParameter P_AddedBy = new SqlParameter("@AddedBy", Session["userName"].ToString());
        SqlParameter P_AddedDate = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        SqlParameter P_UpdatedBy = new SqlParameter("@UpdatedBy", Session["userName"].ToString());
        SqlParameter P_UpdatedDate = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        SqlParameter P_Value_Date = new SqlParameter("@Value_Date", System.DateTime.Now.ToString("dd/MM/yyyy"));

        TF_DATA objSave = new TF_DATA();
        string _Result = objSave.SaveDeleteData("TF_IMP_Swift_FreeFormat_AddUpdate",
        P_BranchCode, p_SwiftType, p_txt_Receiver, p_txt_TransRefNo, p_txt_RelRef,

        P_Narrative1, P_Narrative2, P_Narrative3, P_Narrative4, P_Narrative5,
        P_Narrative6, P_Narrative7, P_Narrative8, P_Narrative9, P_Narrative10,
        P_Narrative11, P_Narrative12, P_Narrative13, P_Narrative14, P_Narrative15,
        P_Narrative16, P_Narrative17, P_Narrative18, P_Narrative19, P_Narrative20,
        P_Narrative21, P_Narrative22, P_Narrative23, P_Narrative24, P_Narrative25,
        P_Narrative26, P_Narrative27, P_Narrative28, P_Narrative29, P_Narrative30,
        P_Narrative31, P_Narrative32, P_Narrative33, P_Narrative34, P_Narrative35,
        P_AddedBy, P_AddedDate, P_UpdatedBy, P_UpdatedDate, P_Value_Date
        );

        #endregion
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txt_TransRefNo.Text.Trim());
        string AR = "";
        if (ddlApproveReject.SelectedIndex == 1)
        {
            AR = "A";
            CreateSwiftSFMSFile();
        }
        if (ddlApproveReject.SelectedIndex == 2)
        {
            AR = "R";
        }
        SqlParameter P_Status = new SqlParameter("@Status", AR);
        SqlParameter P_RejectReason = new SqlParameter("@RejectReason", hdnRejectReason.Value.Trim());
        SqlParameter P_checkby = new SqlParameter("@checkby", Session["userName"].ToString().Trim());
        SqlParameter p_SwiftType = new SqlParameter("@Swift_type", ddlSwiftTypes.SelectedItem.Value.ToUpper());

        string Result = obj.SaveDeleteData("TF_IMP_Swift_FreeFormat_ApproveReject", P_DocNo, p_SwiftType, P_Status, P_RejectReason, P_checkby);

        Response.Redirect("TF_IMP_SwiftFreeFormat_View.aspx?ID=" + Request.QueryString["ID"].ToString() + "&result=" + AR, true);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        btnSave_Click();
        #region SubmitMassage

        SqlParameter p_SwiftType = new SqlParameter("@Swift_type", ddlSwiftTypes.SelectedItem.Value.ToUpper());
        SqlParameter p_txt_TransRefNo = new SqlParameter("@Document_No", txt_TransRefNo.Text.Trim().ToUpper());

        TF_DATA obj = new TF_DATA();
        string Result = obj.SaveDeleteData("TF_IMP_Swift_FreeFormat_SubmitChecker", p_txt_TransRefNo, p_SwiftType);
        if (Result == "Submit")
        {
            Response.Redirect("TF_IMP_SwiftFreeFormat_View.aspx?ID=" + Request.QueryString["ID"].ToString() + "&result=Submit", true);
        }

        #endregion
    }
    protected void btn_Generate_Swift_Click(object sender, EventArgs e)
    {
        btnSave_Click();

        string Docno = txt_TransRefNo.Text.Trim(), SwiftType = ddlSwiftTypes.SelectedItem.Value.Trim();

        string url = "../IMPReports/TF_IMP_SwiftFileView.aspx?DocNo=" + Docno + "&FileType=" + SwiftType + "&Type=FOREIGN";

        string script = "window.open('" + url + "','_blank','scrollbars=yes,top=200,left=200,status=0,menubar=0,width=800,height=400')";
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);

    }
    public void CreateSwiftSFMSFile()
    {
        if (ddlSwiftTypes.SelectedItem.Value == "MT199")
        {
            CreateSwift199_XML();
        }
        else if (ddlSwiftTypes.SelectedItem.Value == "MT199")
        {
            CreateSwift299_XML();
        }
    }
    public void CreateSwift199_XML()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txt_TransRefNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT199_FileCreation_XML", P_DocNo);
        string TodayDate = DateTime.Now.ToString("dd/MM/yyyy");
        string TodayDate1 = DateTime.Now.ToString("ddMMyyyyhhmmss");
        string MTodayDate = "";
        if (TodayDate.Contains("/"))
        {
            MTodayDate = TodayDate.Replace("/", "");
        }
        if (TodayDate.Contains("-"))
        {
            MTodayDate = TodayDate.Replace("-", "");
        }
        if (dt.Rows.Count > 0)
        {
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SWIFT_XML/FreeFormat/MT199/" + MTodayDate + "/");

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = _directoryPath + "/" + txt_TransRefNo.Text + "_MT199_" + TodayDate1 + ".xml";

            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                string Narrative = "";
                #region Narrative
                if (dt.Rows[0]["Narrative1"].ToString() != "")
                {
                    Narrative = dt.Rows[0]["Narrative1"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative2"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative2"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative3"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative3"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative4"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative4"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative5"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative5"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative6"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative6"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative7"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative8"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative8"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative9"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative9"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative10"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative10"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative11"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative11"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative12"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative12"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative13"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative13"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative14"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative14"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative15"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative15"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative16"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative16"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative17"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative17"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative18"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative18"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative19"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative19"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative20"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative20"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative21"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative21"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative22"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative22"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative23"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative23"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative24"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative24"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative25"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative25"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative26"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative26"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative27"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative27"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative28"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative28"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative29"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative29"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative30"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative30"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative31"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative31"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative32"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative32"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative33"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative33"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative34"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative34"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative35"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative35"].ToString().Replace("/", "");
                }
                if (Narrative.Length != 0)
                {
                    string comma = Narrative.Trim().Substring(0, 1);
                    if (comma == ",") { Narrative = Narrative.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["To"].ToString() + "</Receiver>");
                sw.WriteLine("<TransactionReferenceNumber>" + dt.Rows[0]["TransactionReferenceNumber"].ToString() + "</TransactionReferenceNumber>");
                sw.WriteLine("<RelatedReference>" + dt.Rows[0]["RelatedReference"].ToString() + "</RelatedReference>");
                sw.WriteLine("<Narrative>" + Narrative + "</Narrative>");
                sw.WriteLine("</reqMT>");
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
    }
    public void CreateSwift299_XML()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txt_TransRefNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT299_FileCreation_XML", P_DocNo);
        string TodayDate = DateTime.Now.ToString("dd/MM/yyyy");
        string TodayDate1 = DateTime.Now.ToString("ddMMyyyyhhmmss");
        string MTodayDate = "";
        if (TodayDate.Contains("/"))
        {
            MTodayDate = TodayDate.Replace("/", "");
        }
        if (TodayDate.Contains("-"))
        {
            MTodayDate = TodayDate.Replace("-", "");
        }
        if (dt.Rows.Count > 0)
        {
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SWIFT_XML/FreeFormat/MT299/" + MTodayDate + "/");

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = _directoryPath + "/" + txt_TransRefNo.Text + "_MT299_" + TodayDate1 + ".xml";

            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                string Narrative = "";
                #region Narrative
                if (dt.Rows[0]["Narrative1"].ToString() != "")
                {
                    Narrative = dt.Rows[0]["Narrative1"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative2"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative2"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative3"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative3"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative4"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative4"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative5"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative5"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative6"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative6"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative7"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative8"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative8"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative9"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative9"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative10"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative10"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative11"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative11"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative12"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative12"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative13"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative13"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative14"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative14"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative15"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative15"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative16"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative16"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative17"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative17"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative18"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative18"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative19"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative19"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative20"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative20"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative21"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative21"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative22"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative22"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative23"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative23"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative24"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative24"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative25"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative25"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative26"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative26"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative27"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative27"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative28"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative28"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative29"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative29"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative30"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative30"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative31"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative31"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative32"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative32"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative33"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative33"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative34"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative34"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative35"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative35"].ToString().Replace("/", "");
                }
                if (Narrative.Length != 0)
                {
                    string comma = Narrative.Trim().Substring(0, 1);
                    if (comma == ",") { Narrative = Narrative.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["To"].ToString() + "</Receiver>");
                sw.WriteLine("<TransactionReferenceNumber>" + dt.Rows[0]["TransactionReferenceNumber"].ToString() + "</TransactionReferenceNumber>");
                sw.WriteLine("<RelatedReference>" + dt.Rows[0]["RelatedReference"].ToString() + "</RelatedReference>");
                sw.WriteLine("<Narrative>" + Narrative + "</Narrative>");
                sw.WriteLine("</reqMT>");
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
    }
}