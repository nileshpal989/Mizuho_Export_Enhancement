using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Math;

public partial class EXP_TF_EXPGBASE_LOG_Reports: System.Web.UI.Page
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
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                ddlBranch.Focus();
            }
            
           
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "toogleDisplay();", true);
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
        li.Value = "0";
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
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            TF_DATA objdata = new TF_DATA();
            SqlParameter Fromdate = new SqlParameter("@FROMDATE", txtfromDate.Text.Trim());
            SqlParameter TOdate = new SqlParameter("@TODATE", txtToDate.Text.Trim());
            SqlParameter Branch = new SqlParameter("@Branch", ddlBranch.Text.Trim());
            System.Data.DataTable dt = objdata.getData("TF_EXP_Realised_GETLOGDATA", Fromdate, TOdate, Branch);
            if (dt.Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Log File");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Gbase_Log_File.xlsx");
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
                string Script = "No record Found";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('" + Script + "');", true);
                lblmsg.Text = "No record Found";
            }
        }
        catch (Exception ex)

        {
            lblmsg.Text = ex.Message;
        }
    }
}