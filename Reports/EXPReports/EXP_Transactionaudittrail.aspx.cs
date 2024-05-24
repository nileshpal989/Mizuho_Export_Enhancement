using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.IO;

public partial class Reports_EXPReports_EXP_Transactionaudittrail : System.Web.UI.Page
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
                clearControls();

                fillddlBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                // ddlBranch.Enabled = false;
                txtFromDate.Focus();
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

            }
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "toogleDisplay();", true);
            btnSave.Attributes.Add("onclick", "return Custhelp();");
        }
    }
    public void fillddlBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "---Select---";
        if (dt.Rows.Count > 0)
        {
            li.Value = "---Select---";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);

    }
    protected void clearControls()
    {
        txtFromDate.Text = "";

        txtFromDate.Focus();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string CurrentDate = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        if (ddlBranch.SelectedValue == "---Select---")
        {
            Response.Write("<script>alert('Please Select Branch')</script>");
        }
        else if (txtFromDate.Text == "")
        {
            Response.Write("<script>alert('Please Select From Date')</script>");
        }
        else if (ddlCheckList.SelectedValue == "")
        {
            Response.Write("<script>alert('Please Select Transaction')</script>");
        }
        else if (ddlCheckList.SelectedValue == "Lodgement")
        {
            TF_DATA objdata = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@Branch", ddlBranch.SelectedItem.Text);
            SqlParameter p2 = new SqlParameter("@startdate", txtFromDate.Text);
            SqlParameter p3 = new SqlParameter("@enddate", txtToDate.Text);
            string script = "TF_EXP_LodgementTransaction";
            DataTable dt = objdata.getData(script, p1, p2, p3);
            if (dt.Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var worksheet =  wb.Worksheets.Add(dt, "Data_CheckLsit");
                    worksheet.CellsUsed().Style.Alignment.WrapText = true;
                    worksheet.Column(2).Width = 60; // Column B
                    worksheet.Column(3).Width = 60; // Column C
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=EXP_Lodgement_Audit" + CurrentDate + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }

            else
            {
                txtFromDate.Text = "";
                txtFromDate.Focus();
                Response.Write("<script>alert('No Records')</script>");

            }

        }

        else if (ddlCheckList.SelectedValue == "Inward")
        {
            TF_DATA objdata = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@Branch", ddlBranch.SelectedItem.Text);
            SqlParameter p2 = new SqlParameter("@startdate", txtFromDate.Text);
            SqlParameter p3 = new SqlParameter("@enddate", txtToDate.Text);
            string script = "TF_EXP_InwardTransaction";
            DataTable dt = objdata.getData(script, p1, p2, p3);
            if (dt.Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                   var worksheet= wb.Worksheets.Add(dt, "Data_CheckLsit");
                    worksheet.CellsUsed().Style.Alignment.WrapText = true;
                    worksheet.Column(2).Width = 80; // Column B
                    worksheet.Column(3).Width = 80; // Column C
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=EXP_Inward_Audit" + CurrentDate + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }

            else
            {
                txtFromDate.Text = "";
                txtFromDate.Focus();
                Response.Write("<script>alert('No Records')</script>");

            }

        }

    }
}