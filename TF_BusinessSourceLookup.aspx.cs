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
using System.Text;
using System.Data.SqlClient;

public partial class TF_BusinessSourceLookup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            // hndsessionid.Value = Request.QueryString["strnew"].ToString();
            rbtnType.Visible = false;
            rbtnType.SelectedValue = "2";
            fillBusSource();
            txtSearch.Attributes.Add("onkeypress", "return submitForm(event);");
            btngo.Attributes.Add("onclick", "return validateSearch();");
            btnCancel.Attributes.Add("onclick", "window.close(); return false;");
            // txtSearch.Focus();

            lstBusSourceList.Focus();
            lstBusSourceList.SelectedIndex = 0;
        }

    }
    protected void fillBusSource()
    {
        //TF_DATA objData = new TF_DATA();
        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        string _query = "TF_GetBusinessSourceMasterList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);

        if (dt.Rows.Count > 0)
        {
            if (Request.QueryString["ACofficer_ID"] != null)
            {
                TF_DATA objDatap = new TF_DATA();
                if (Request.QueryString["ACofficer_ID"].ToString() == "0")
                {
                    BindBusSourceCodes(dt);
                }
                else
                {
                    SqlParameter par1 = new SqlParameter("@businessSourceID", SqlDbType.VarChar);
                    par1.Value = Request.QueryString["ACofficer_ID"].ToString();

                    string _query1 = "TF_GetBusinessSourceMasterDetails";
                    DataTable dtp = objDatap.getData(_query1, par1);
                    
                    if (dtp.Rows.Count > 0)
                    {
                        lstBusSourceList.DataSource = dtp.DefaultView;
                        if (rbtnType.SelectedValue == "1")
                        {
                            lstBusSourceList.DataValueField = "BUSINESS_SOURCE_ID";
                            lstBusSourceList.DataTextField = "BUSINESS_SOURCE_ID";
                        }
                        else
                        {
                            lstBusSourceList.DataValueField = "BUSINESS_SOURCE_ID";
                            lstBusSourceList.DataTextField = "newdesc";
                        }
                        lstBusSourceList.DataBind();
                    }
                    else
                    {

                        ListItem li = new ListItem();
                        li.Text = "No record(s) found";
                        li.Value = "0";
                        lstBusSourceList.Items.Clear();
                        lstBusSourceList.Items.Insert(0, li);
                    }
                }
            }
            else
            {
                lstBusSourceList.DataSource = dt.DefaultView;
                if (rbtnType.SelectedValue == "1")
                {
                    lstBusSourceList.DataValueField = "BUSINESS_SOURCE_ID";
                    lstBusSourceList.DataTextField = "BUSINESS_SOURCE_ID";
                }
                else
                {
                    lstBusSourceList.DataValueField = "BUSINESS_SOURCE_ID";

                    lstBusSourceList.DataTextField = "newdesc";
                }
                lstBusSourceList.DataBind();
            }
        }
        else
        {
            ListItem li = new ListItem();
            li.Text = "No record(s) found";
            li.Value = "0";
            lstBusSourceList.Items.Clear();
            lstBusSourceList.Items.Insert(0, li);
        }
    }

    public void BindBusSourceCodes(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            lstBusSourceList.DataSource = dt.DefaultView;
            if (rbtnType.SelectedValue == "1")
            {
                lstBusSourceList.DataValueField = "BUSINESS_SOURCE_ID";
                lstBusSourceList.DataTextField = "BUSINESS_SOURCE_ID";
            }
            else
            {
                lstBusSourceList.DataValueField = "BUSINESS_SOURCE_ID";
                lstBusSourceList.DataTextField = "newdesc";
            }
            lstBusSourceList.DataBind();
        }
        else
        {
            ListItem li = new ListItem();
            li.Text = "No record(s) found";
            li.Value = "0";
            lstBusSourceList.Items.Clear();
            lstBusSourceList.Items.Insert(0, li);
        }
    }

    protected void btngo_Click(object sender, EventArgs e)
    {
        
        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        string _query = "TF_GetBusinessSourceMasterList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        BindBusSourceCodes(dt);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (lstBusSourceList.SelectedValue != "0")
        {
            string pcId = "";
            string pcdes = "";

            foreach (ListItem li in lstBusSourceList.Items)
            {
                if (li.Selected)
                {
                    pcId = pcId + li.Value.ToString().Trim();
                    pcdes = pcdes + li.Text.ToString().Trim();
                }
            }
            if (pcId != "")
            {
                StringBuilder mystring = new StringBuilder();
                mystring.Append("window.opener.selectBusinessSourceID('" + pcId + "');");
                mystring.Append("window.opener.EndRequest();");
                mystring.Append("window.close();");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SelectEMail", mystring.ToString()
                    , true);
            }
            else
            {
                if (pcId == "No record(s) found")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "showError", "window.opener.alert('No record(s) to select.');", true);
                }
                else
                { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "showError", "window.opener.alert('Select any one Business Source ID.');", true); }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "showError", "window.opener.alert('Record can not be selected.');", true);
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Close", "window.close();", true);
    }

    protected void rbtnType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtSearch.Text == "")
        {
            if (rbtnType.SelectedValue == "1")
            {
                VisibleInvisibleOfSection();
            }
            else if (rbtnType.SelectedValue == "2")
            {
                VisibleInvisibleOfSection();
            }
        }
        else
        {
            string search = txtSearch.Text.Trim();
            SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
            p1.Value = search;

            string _query = "TF_GetBusinessSourceMasterList";
            TF_DATA objData = new TF_DATA();
            DataTable dt = objData.getData(_query, p1);
            BindBusSourceCodes(dt);
        }
    }

    public void VisibleInvisibleOfSection()
    {
        if (rbtnType.SelectedValue == "1")
        {
            lstBusSourceList.SelectionMode = ListSelectionMode.Single;
            fillBusSource();
        }
        else if (rbtnType.SelectedValue == "2")
        {
            lstBusSourceList.SelectionMode = ListSelectionMode.Single;
            fillBusSource();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewBusinessSourceMaster.aspx", true);
    }
}
