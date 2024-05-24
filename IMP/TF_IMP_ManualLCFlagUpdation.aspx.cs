using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IMP_TF_IMP_Miscellaneous : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void txtDocumentNo_TextChanged(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocumentNo.Text.Trim());
        DataTable dt = obj.getData("TF_IMP_FillMiscellaneousDetails", P_DocNo);
        if (dt.Rows.Count > 0)
        {
            OWNLCValue.Value = dt.Rows[0]["OwnLCDiscount_Type"].ToString();
            if (dt.Rows[0]["OwnLCDiscount_Type"].ToString() == "Y")
            {
                rdb_ownLCDiscount_No.Checked = false;
                rdb_ownLCDiscount_Yes.Checked = true;
            }
            else
            {
                rdb_ownLCDiscount_Yes.Checked = false;
                rdb_ownLCDiscount_No.Checked = true;
            }
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string OWNLC = "";
            if (rdb_ownLCDiscount_Yes.Checked)
            {
                OWNLC = "Y";
            }
            if (rdb_ownLCDiscount_No.Checked)
            {
                OWNLC = "N";
            }
            TF_DATA obj = new TF_DATA();
            SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocumentNo.Text.Trim());
            SqlParameter P_OWNLC = new SqlParameter("@OWNLC", OWNLC);
            string Result = obj.SaveDeleteData("TF_IMP_UpdateMiscellaneous", P_DocNo, P_OWNLC);
            if (Result == "Updated")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Transaction successfully updated.')", true);
                txtDocumentNo.Text = "";
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
}