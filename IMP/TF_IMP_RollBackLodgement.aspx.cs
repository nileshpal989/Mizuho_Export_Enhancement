using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IMP_TF_IMP_RollBackLodgement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("~/TF_Login.aspx?Sessionout=yes&Sessionid=" + lbl.Value, true);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string Rollback = "";
            if (rdb_Yes.Checked)
            {
                Rollback = "R";

                TF_DATA obj = new TF_DATA();
                SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocumentNo.Text.Trim());
                SqlParameter P_Rollback = new SqlParameter("@Checker_Status", Rollback);
                string Result = obj.SaveDeleteData("TF_IMP_BOE_Rollback", P_DocNo, P_Rollback);
                if (Result == "Updated")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Document Rollback to Logdement Maker Queue.')", true);
                    txtDocumentNo.Text = "";
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('No effect on Document.')", true);
                txtDocumentNo.Text = "";
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TF_ModuleSelection.aspx", true);
    }
}