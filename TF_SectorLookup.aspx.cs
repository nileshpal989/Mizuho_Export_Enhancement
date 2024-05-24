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

public partial class TF_SectorLookup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
            if (!IsPostBack)
            {
               // hndsessionid.Value = Request.QueryString["strnew"].ToString();
                rbtnType.Visible = false;
                rbtnType.SelectedValue = "2";
                fillSector();
                txtSearch.Attributes.Add("onkeypress", "return submitForm(event);");
                btngo.Attributes.Add("onclick", "return validateSearch();");
                btnCancel.Attributes.Add("onclick", "window.close(); return false;");
                // txtSearch.Focus();

                lstSectList.Focus();
                lstSectList.SelectedIndex = 0;
            }
        
    }
    protected void fillSector()
    {
        //TF_DATA objData = new TF_DATA();
        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        string _query = "TF_GetSectorMasterList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);

        if (dt.Rows.Count > 0)
        {
            if (Request.QueryString["sec_ID"] != null)
            {
                TF_DATA objDatap = new TF_DATA();
                if (Request.QueryString["sec_ID"].ToString() == "0")
                {
                    BindSectCodes(dt);
                }
                else
                {
                    SqlParameter par1 = new SqlParameter("@sectorid", SqlDbType.VarChar);
                    par1.Value = Request.QueryString["sec_ID"].ToString();

                    string _query1 = "TF_GetSectorMasterDetails";
                    DataTable dtp = objDatap.getData(_query1,par1);
                    if (dtp.Rows.Count > 0)
                    {
                        lstSectList.DataSource = dtp.DefaultView;
                        if (rbtnType.SelectedValue == "1")
                        {
                            lstSectList.DataValueField = "SECTOR_ID";
                            lstSectList.DataTextField = "SECTOR_ID";
                        }
                        else
                        {
                            lstSectList.DataValueField = "SECTOR_ID";
                            lstSectList.DataTextField = "newdesc";
                        }
                        lstSectList.DataBind();
                    }
                    else
                    {

                        ListItem li = new ListItem();
                        li.Text = "No record(s) found";
                        li.Value = "0";
                        lstSectList.Items.Clear();
                        lstSectList.Items.Insert(0, li);
                    }
                }
            }
            else
            {
                lstSectList.DataSource = dt.DefaultView;
                if (rbtnType.SelectedValue == "1")
                {
                    lstSectList.DataValueField = "SECTOR_ID";
                    lstSectList.DataTextField = "SECTOR_ID";
                }
                else
                {
                    lstSectList.DataValueField = "SECTOR_ID";

                    lstSectList.DataTextField = "newdesc";
                }
                lstSectList.DataBind();
            }
        }
        else
        {
            ListItem li = new ListItem();
            li.Text = "No record(s) found";
            li.Value = "0";
            lstSectList.Items.Clear();
            lstSectList.Items.Insert(0, li);
        }
    }

    public void BindSectCodes(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            lstSectList.DataSource = dt.DefaultView;
            if (rbtnType.SelectedValue == "1")
            {
                lstSectList.DataValueField = "SECTOR_ID";
                lstSectList.DataTextField = "SECTOR_ID";
            }
            else
            {
                lstSectList.DataValueField = "SECTOR_ID";
                lstSectList.DataTextField = "newdesc";
            }
            lstSectList.DataBind();
        }
        else
        {
            ListItem li = new ListItem();
            li.Text = "No record(s) found";
            li.Value = "0";
            lstSectList.Items.Clear();
            lstSectList.Items.Insert(0, li);
        }
    }

    protected void btngo_Click(object sender, EventArgs e)
    {
        //TF_DATA objData = new TF_DATA();
        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        string _query = "TF_GetSectorMasterList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        BindSectCodes(dt);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (lstSectList.SelectedValue != "0")
        {
            string pcId = "";
            string pcdes = "";

            foreach (ListItem li in lstSectList.Items)
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
                mystring.Append("window.opener.selectSectorID('" + pcId + "');");
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
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Close", "window.close();",true);
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

            string _query = "TF_GetSectorMasterList";
            TF_DATA objData = new TF_DATA();
            DataTable dt = objData.getData(_query, p1);
            BindSectCodes(dt);
        }
    }

    public void VisibleInvisibleOfSection()
    {
        if (rbtnType.SelectedValue == "1")
        {
            lstSectList.SelectionMode = ListSelectionMode.Single;
            fillSector();
        }
        else if (rbtnType.SelectedValue == "2")
        {
            lstSectList.SelectionMode = ListSelectionMode.Single;
            fillSector();
        }
    }
}
