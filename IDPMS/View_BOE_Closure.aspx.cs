using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class IDPMS_View_BOE_Closure : System.Web.UI.Page
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
                hdnUserRole.Value = Session["userRole"].ToString().Trim();
                txtYear.Text = DateTime.Now.Year.ToString();
                ddlrecordperpage.SelectedValue = "20";
                fillGrid();
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;


                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].ToString() == "inserted")
                    {
                        string _docNo = Request.QueryString["result"].ToString().Substring(5);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);
                        //ddlBranch.SelectedValue = "Mumbai";
                    }
                    else
                        if (Request.QueryString["result"].Trim() == "updated")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                            //ddlBranch.SelectedValue = "Mumbai";
                        }
                }

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

        //ddlBranch.Items.Insert(0, li);

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("IDPMS_BOEClosure.aspx?mode=add&year=" + txtYear.Text, true);
    }
    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }

    protected void fillGrid()
    {
        //getLastDocNo();
        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        SqlParameter p2 = new SqlParameter("@year", SqlDbType.VarChar);
        p2.Value = txtYear.Text.Trim();

        string _query = "FillBOEClosure1";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewBOEclosure.PageSize = _pageSize;
            GridViewBOEclosure.DataSource = dt.DefaultView;
            GridViewBOEclosure.DataBind();
            GridViewBOEclosure.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewBOEclosure.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }

    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewBOEclosure.PageCount != GridViewBOEclosure.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewBOEclosure.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewBOEclosure.PageIndex + 1) + " of " + GridViewBOEclosure.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewBOEclosure.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewBOEclosure.PageIndex != (GridViewBOEclosure.PageCount - 1))
        {
            pgcontrols.enablelastnav(btnnavnext, btnnavlast);
        }
        else
        {
            pgcontrols.disablelastnav(btnnavnext, btnnavlast);
        }
    }
    private void navigationVisibility(Boolean visibility)
    {
        lblpageno.Visible = visibility;
        lblrecordno.Visible = visibility;
        btnnavfirst.Visible = visibility;
        btnnavlast.Visible = visibility;
        btnnavnext.Visible = visibility;
        btnnavpre.Visible = visibility;
    }

    protected void GridViewBOEclosure_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblirmno = new Label();
            Label lbladjref = new Label();
            Button btnDelete = new Button();
            //Label lbldocno = new Label();
            //Label lblshipbillno = new Label();

            lblirmno = (Label)e.Row.FindControl("lblirmno");
            lbladjref = (Label)e.Row.FindControl("lbladjref");
            btnDelete = (Button)e.Row.FindControl("btnDelete");

            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";
            if (hdnUserRole.Value == "OIC")
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
            else
                btnDelete.Attributes.Add("onclick", "alert('Only OIC can delete all records.');return false;");


            //lbldocno = (Label)e.Row.FindControl("lbldocno");
            //lblshipbillno = (Label)e.Row.FindControl("lblshipbillno");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                //string pageurl = "window.location='IDPMS_BOEClosure.aspx?mode=edit&irmnono=" + lblirmno.Text.Trim() +"&adjref="+lbladjref.Text.Trim()+ "'";
                if (i != 7)
                    //cell.Attributes.Add("onclick", pageurl);
               // else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

    protected void GridViewBOEclosure_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "",billno="",adref="";
        string[] values_p;

        //string _userName = Session["userName"].ToString().Trim();
        //string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string str = e.CommandArgument.ToString();
         if (str != "")
        {
            char[] splitchar = { ',' };
            values_p = str.Split(splitchar);
            billno = values_p[0].ToString();
            adref = values_p[1].ToString();
        }
        

        SqlParameter p1 = new SqlParameter("@boeno", SqlDbType.VarChar);
        p1.Value = billno;
        SqlParameter p2 = new SqlParameter("@adjref",SqlDbType.VarChar);
        p2.Value = adref;
        string _query = "DeleteBOENo";
        TF_DATA objData = new TF_DATA();
        result = objData.SaveDeleteData(_query, p1,p2);
        
        if (result == "deleted")
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
        else
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record cannot be deleted.');", true);

        fillGrid();
    }

    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewBOEclosure.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewBOEclosure.PageIndex > 0)
        {
            GridViewBOEclosure.PageIndex = GridViewBOEclosure.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewBOEclosure.PageIndex != GridViewBOEclosure.PageCount - 1)
        {
            GridViewBOEclosure.PageIndex = GridViewBOEclosure.PageIndex + 1;
        }

        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewBOEclosure.PageIndex = GridViewBOEclosure.PageCount - 1;
        fillGrid();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
}