using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using ClosedXML.Excel;

public partial class EXP_Problem_Exp_Bill_mgnt_circulation_xlx_genarate : System.Web.UI.Page
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
                //    fillBranch();
                //    ddlBranch.SelectedValue = Session["userADCode"].ToString();
                //    ddlBranch.Enabled = false;


                ddlBranch.SelectedValue = Session["userLBCode"].ToString();
                ddlBranch.Enabled = false;
                fillBranch();

               
                //txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                ddlBranch.Focus();
                //rdbAllCustomer.Checked = true;
            }
            txtFromDate.Attributes.Add("onblur", "return isValidDate(" + txtFromDate.ClientID + "," + "'From Date.'" + ");");
            //txtToDate.Attributes.Add("onblur", "return isValidDate(" + txtToDate.ClientID + "," + "'To Date.'" + ");");
            btnCreate.Attributes.Add("onclick", "return validateSave();");
        }
    }

    public void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", "");
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
            //ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataValueField = "BranchCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";
        ddlBranch.Items.Insert(0, li);
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        //if (txtFromDate.Text == "" || txtToDate.Text == "")
        if (txtFromDate.Text == "")
        {
            return;
        }

        string _fromdate = txtFromDate.Text.Substring(0, 2) + "" + txtFromDate.Text.Substring(3, 2) + "" + txtFromDate.Text.Substring(6, 4);
        //string _todate = txtToDate.Text.Substring(0, 2) + "" + txtToDate.Text.Substring(3, 2) + "" + txtToDate.Text.Substring(6, 4);
        TF_DATA objData1 = new TF_DATA();
        SqlParameter p = new SqlParameter("@Asondate", SqlDbType.VarChar);
        p.Value = txtFromDate.Text.Trim();
        //SqlParameter P1 = new SqlParameter("@Todate", SqlDbType.VarChar);
        //P1.Value = txtToDate.Text.Trim();
        SqlParameter P1 = new SqlParameter("@Branch", ddlBranch.SelectedValue.ToString());


        DataTable dt = objData1.getData("Exp_bill_problem_getdata", p, P1);
        if (dt.Rows.Count > 0)
        {
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);

            }

            string _filePath = _directoryPath + "/" + "ProblemEXPmgnt" + _fromdate + ".xlsx";


            if (dt.Rows.Count > 0)
            {

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Sheet1");

                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);

                        FileStream file = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
                        MyMemoryStream.WriteTo(file);
                        file.Close();
                        MyMemoryStream.Close();

                    }
                }


                TF_DATA objserverName = new TF_DATA();
                string _serverName = objserverName.GetServerName();
                string path = "file://" + _serverName + "/TF_GeneratedFiles/EXPORT";
                string link = "/TF_GeneratedFiles/EXPORT";
                labelMessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";

                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('File Created Successfully on " + _serverName + " in " + path + "')", true);

            }

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "Message", "alert('There is No records Between this Dates.')", true);
            txtFromDate.Text = "";

            txtFromDate.Focus();
        }
    }


    
}