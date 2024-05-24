using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TF_rptAccessControl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnBenfList.Attributes.Add("onclick", "return OpenUserList();");
            btnSave.Attributes.Add("onclick", "return validateSave();");
        }
       
    }

    protected void rdbSelectedUser_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbSelectedUser.Checked)
        {
            Table2.Visible = true;
        }
        else if (rdbAllUser.Checked)
        {
            Table2.Visible = false;
        }
    }
    protected void rdbAllUser_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbSelectedUser.Checked)
        {
            Table2.Visible = true;
        }
        else if (rdbAllUser.Checked)
        {
            Table2.Visible = false;
        }
    }
}