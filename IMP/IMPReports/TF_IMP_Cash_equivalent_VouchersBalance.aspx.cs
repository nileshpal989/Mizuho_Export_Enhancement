using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class IMP_IMPReports_TF_IMP_Cash_equivalent_VouchersBalance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillCurrency();
            Generate.Attributes.Add("onclick", " validateGenerate();");
        }
    }
    protected void fillCurrency()
    {

        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData("TF_IMP_Currency_List");
        ddlcurrency.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "";
        if (dt.Rows.Count > 0)
        {
            li.Value = "All";
            li.Text = "All";
            ddlcurrency.DataSource = dt.DefaultView;
            ddlcurrency.DataTextField = "C_Code";
            ddlcurrency.DataValueField = "C_Code";
            ddlcurrency.DataBind();


        }
        else
        {
            li.Text = "No record(s) found";
        }
        ddlcurrency.Items.Insert(0, li);
    }
}