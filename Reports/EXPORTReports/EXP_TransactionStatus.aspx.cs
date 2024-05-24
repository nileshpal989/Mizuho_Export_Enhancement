using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_EXPORTReports_EXP_TransactionStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            clearControls();

            txtFromDate.Attributes.Add("onblur", "toDate();");

            btnSave.Attributes.Add("onclick", "return validateSave();");
        }
    }
    protected void clearControls()
    {
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtFromDate.Focus();
    }
}