using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
public partial class EXP_EXP_SubAccountNo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            fillGrid();
        }

    }


    protected void fillGrid()
    {
        //string search = "";
        SqlParameter p1 = new SqlParameter("@custAcNo", SqlDbType.VarChar);
        if (Request.QueryString["custId"] != null)
        {
            p1.Value = Request.QueryString["custId"].ToString();
        }
        else
            p1.Value = "";

        SqlParameter p2 = new SqlParameter("@branch", SqlDbType.VarChar);
        p2.Value = Request.QueryString["branch"].ToString();

        string _query = "TF_GetCustomerSub_ACNo_new";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1,p2);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;

            GridViewSubAcList.DataSource = dt.DefaultView;
            GridViewSubAcList.DataBind();
            GridViewSubAcList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else
        {
            GridViewSubAcList.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }

    }
    protected void GridViewSubAcList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }
    protected void GridViewSubAcList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblAcNo = new Label();
            Label lblSubAcNo = new Label();
            Label lblBalAmt = new Label();
            lblAcNo = (Label)e.Row.FindControl("lblAcNo");
            lblSubAcNo = (Label)e.Row.FindControl("lblSubAcNo");
            lblBalAmt = (Label)e.Row.FindControl("lblBalAmt");
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "";
                if (Request.QueryString["hNo"].ToString() == "1")
                    pageurl = "window.opener.selectSubAc1('" + lblSubAcNo.Text + "','" + lblAcNo.Text + "','" + lblBalAmt.Text + "');window.opener.EndRequest();window.close();";
                if (Request.QueryString["hNo"].ToString() == "2")
                    pageurl = "window.opener.selectSubAc2('" + lblSubAcNo.Text + "','" + lblAcNo.Text + "','" + lblBalAmt.Text + "');window.opener.EndRequest();window.close();";
                if (Request.QueryString["hNo"].ToString() == "3")
                    pageurl = "window.opener.selectSubAc3('" + lblSubAcNo.Text + "','" + lblAcNo.Text + "','" + lblBalAmt.Text + "');window.opener.EndRequest();window.close();";
                if (Request.QueryString["hNo"].ToString() == "4")
                    pageurl = "window.opener.selectSubAc4('" + lblSubAcNo.Text + "','" + lblAcNo.Text + "','" + lblBalAmt.Text + "');window.opener.EndRequest();window.close();";
                if (Request.QueryString["hNo"].ToString() == "5")
                    pageurl = "window.opener.selectSubAc5('" + lblSubAcNo.Text + "','" + lblAcNo.Text + "','" + lblBalAmt.Text + "');window.opener.EndRequest();window.close();";
                if (Request.QueryString["hNo"].ToString() == "6")
                    pageurl = "window.opener.selectSubAc6('" + lblSubAcNo.Text + "','" + lblAcNo.Text + "','" + lblBalAmt.Text + "');window.opener.EndRequest();window.close();";
                if (i != 6)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

    protected void GridViewSubAcList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TabIndex = -1;
            e.Row.Attributes["onmouseover"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            e.Row.ToolTip = "Click to select row.";
        }

    }
}
