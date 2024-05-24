using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public partial class TF_OUCodeLookup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // hndsessionid.Value = Request.QueryString["strnew"].ToString();
            rbtnType.Visible = false;
            rbtnType.SelectedValue = "2";
            fillOUCode();
            txtSearch.Attributes.Add("onkeypress", "return submitForm(event);");
            btngo.Attributes.Add("onclick", "return validateSearch();");
            btnCancel.Attributes.Add("onclick", "window.close(); return false;");
            // txtSearch.Focus();

            lstOUCodeList.Focus();
            lstOUCodeList.SelectedIndex = 0;
        }

    }
    protected void fillOUCode()
    {
        //TF_DATA objData = new TF_DATA();
        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        string _query = "TF_GetOUCodeMasterList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);

        if (dt.Rows.Count > 0)
        {
            if (Request.QueryString["OU_CD"] != null)
            {
                TF_DATA objDatap = new TF_DATA();
                if (Request.QueryString["OU_CD"].ToString() == "0")
                {
                    BindOUCodes(dt);
                }
                else
                {
                    SqlParameter par1 = new SqlParameter("@oucode", SqlDbType.VarChar);
                    par1.Value = Request.QueryString["OU_CD"].ToString();

                    string _query1 = "TF_GetOUCodeMasterDetails";
                    DataTable dtp = objDatap.getData(_query1, par1);
                    if (dtp.Rows.Count > 0)
                    {
                        lstOUCodeList.DataSource = dtp.DefaultView;
                        if (rbtnType.SelectedValue == "1")
                        {
                            lstOUCodeList.DataValueField = "OUCode";
                            lstOUCodeList.DataTextField = "OUCode";
                        }
                        else
                        {
                            lstOUCodeList.DataValueField = "OUCode";
                            lstOUCodeList.DataTextField = "newdesc";
                        }
                        lstOUCodeList.DataBind();
                    }
                    else
                    {

                        ListItem li = new ListItem();
                        li.Text = "No record(s) found";
                        li.Value = "0";
                        lstOUCodeList.Items.Clear();
                        lstOUCodeList.Items.Insert(0, li);
                    }
                }
            }
            else
            {
                lstOUCodeList.DataSource = dt.DefaultView;
                if (rbtnType.SelectedValue == "1")
                {
                    lstOUCodeList.DataValueField = "OUCode";
                    lstOUCodeList.DataTextField = "OUCode";
                }
                else
                {
                    lstOUCodeList.DataValueField = "OUCode";

                    lstOUCodeList.DataTextField = "newdesc";
                }
                lstOUCodeList.DataBind();
            }
        }
        else
        {
            ListItem li = new ListItem();
            li.Text = "No record(s) found";
            li.Value = "0";
            lstOUCodeList.Items.Clear();
            lstOUCodeList.Items.Insert(0, li);
        }
    }

    public void BindOUCodes(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            lstOUCodeList.DataSource = dt.DefaultView;
            if (rbtnType.SelectedValue == "1")
            {
                lstOUCodeList.DataValueField = "OUCode";
                lstOUCodeList.DataTextField = "OUCode";
            }
            else
            {
                lstOUCodeList.DataValueField = "OUCode";
                lstOUCodeList.DataTextField = "newdesc";
            }
            lstOUCodeList.DataBind();
        }
        else
        {
            ListItem li = new ListItem();
            li.Text = "No record(s) found";
            li.Value = "0";
            lstOUCodeList.Items.Clear();
            lstOUCodeList.Items.Insert(0, li);
        }
    }

    protected void btngo_Click(object sender, EventArgs e)
    {
        //TF_DATA objData = new TF_DATA();
        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        string _query = "TF_GetOUCodeMasterList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        BindOUCodes(dt);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (lstOUCodeList.SelectedValue != "0")
        {
            string pcId = "";
            string pcdes = "";

            foreach (ListItem li in lstOUCodeList.Items)
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
                mystring.Append("window.opener.selectOUCode('" + pcId + "');");
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
                { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "showError", "window.opener.alert('Select any one Purpose Code.');", true); }
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

            string _query = "TF_GetOUCodeMasterList";
            TF_DATA objData = new TF_DATA();
            DataTable dt = objData.getData(_query, p1);
            BindOUCodes(dt);
        }
    }

    public void VisibleInvisibleOfSection()
    {
        if (rbtnType.SelectedValue == "1")
        {
            lstOUCodeList.SelectionMode = ListSelectionMode.Single;
            fillOUCode();
        }
        else if (rbtnType.SelectedValue == "2")
        {
            lstOUCodeList.SelectionMode = ListSelectionMode.Single;
            fillOUCode();
        }
    }
}
