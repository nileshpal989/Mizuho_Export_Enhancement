using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class TF_AddEditRelationshipMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                clearall();
                string ACId = Session["BranchS"].ToString();
                txtBusinessSourceID1.Text = ACId.Substring(0, 1);
                txtBusinessSourceID2.Text = ACId.Substring(0, 1);
                txtBusinessSourceID3.Text = ACId.Substring(0, 1);
                txtBusinessSourceID4.Text = ACId.Substring(0, 1);
                txtBusinessSourceID5.Text = ACId.Substring(0, 1);

                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_ViewRelationshipMaster.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtBusinessSourceID1.Text = Request.QueryString["L1"].Trim();
                        txtBusinessSourceID1.Enabled = true;
                        fillDetails(Request.QueryString["L1"].Trim());
                    }
                    else
                    {
                        txtBusinessSourceID1.Enabled = true;
                    }
                }
                if (Session["BranchS"].ToString() != null)
                {

                    Button2.Text = Session["BranchS"].ToString();
                }
                btnSave.Attributes.Add("onclick", "return validateSave();");
                txtBusinessSourceID1.Attributes.Add("onkeydown", "return ACofficerId1(event);");
                btnBusSourceList1.Attributes.Add("onclick", "return ACofficerhelp1();");

                txtBusinessSourceID2.Attributes.Add("onkeydown", "return ACofficerId2(event);");
                btnBusSourceList2.Attributes.Add("onclick", "return ACofficerhelp2();");

                txtBusinessSourceID3.Attributes.Add("onkeydown", "return ACofficerId3(event);");
                btnBusSourceList3.Attributes.Add("onclick", "return ACofficerhelp3();");

                txtBusinessSourceID4.Attributes.Add("onkeydown", "return ACofficerId4(event);");
                btnBusSourceList4.Attributes.Add("onclick", "return ACofficerhelp4();");

                txtBusinessSourceID5.Attributes.Add("onkeydown", "return ACofficerId5(event);");
                btnBusSourceList5.Attributes.Add("onclick", "return ACofficerhelp5();");

                txtBusinessSourceID1.Attributes.Add("onblur", "return Myfunction2();");
                txtBusinessSourceID2.Attributes.Add("onblur", "return Myfunction2();");
                txtBusinessSourceID3.Attributes.Add("onblur", "return Myfunction2();");
                txtBusinessSourceID4.Attributes.Add("onblur", "return Myfunction2();");
                txtBusinessSourceID5.Attributes.Add("onblur", "return Myfunction2();");
            }
        }

    }
    public void clearall()
    {
        txtBusinessSourceID1.Text = "";
        txtBusinessSourceID2.Text = "";
        txtBusinessSourceID3.Text = "";
        txtBusinessSourceID4.Text = "";
        txtBusinessSourceID5.Text = "";
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtBusinessSourceID1.Text.ToString() == "")
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Enter Business Source Id(Level1)')", true);

        }
        else if (txtBusinessSourceID2.Text.ToString() == "")
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Enter Business Source Id(Level2)')", true);

        }
                 else if (txtBusinessSourceID3.Text.ToString() == "")
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Enter Business Source Id(Level3)')", true);

        }
        else if (txtBusinessSourceID4.Text.ToString() == "")
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Enter Business Source Id(Level4)')", true);

        }
        else if (txtBusinessSourceID5.Text.ToString() == "")
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Enter Business Source Id(Level5)')", true);

        }



        else
        {
            string _result = "";
            string _userName = Session["userName"].ToString();
            string _mode = Request.QueryString["mode"].Trim();
            string _BranchCode = Request.QueryString["BranchCode"].ToString().Trim();
            string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");


           

            //string _L1 = txtID.Text.Trim() + txtBusinessSourceID1.Text.Trim();
            //string _L2 = txtID2.Text.Trim() + txtBusinessSourceID2.Text.Trim();
            //string _L3 = txtID3.Text.Trim() + txtBusinessSourceID3.Text.Trim();
            //string _L4 = txtID4.Text.Trim() + txtBusinessSourceID4.Text.Trim();
            //string _L5 = txtID5.Text.Trim() + txtBusinessSourceID5.Text.Trim();


            string _L1 =  txtBusinessSourceID1.Text.Trim();
            string _L2 = txtBusinessSourceID2.Text.Trim();
            string _L3 = txtBusinessSourceID3.Text.Trim();
            string _L4 =  txtBusinessSourceID4.Text.Trim();
            string _L5 =  txtBusinessSourceID5.Text.Trim();


            //TF_DATA objSave = new TF_DATA();
            //_result = objSave.addUpdateBusinessSourceMaster(_mode, _BranchCode, _BusinessSourceID, _Name, _Designation, _userName, _uploadingDate);

            SqlParameter p1 = new SqlParameter("@mode", SqlDbType.VarChar);
            p1.Value = _mode;
            SqlParameter p2 = new SqlParameter("@branchcode", SqlDbType.VarChar);
            p2.Value = _BranchCode;
            SqlParameter p3 = new SqlParameter("@L1", SqlDbType.VarChar);
            p3.Value = _L1;
            SqlParameter p4 = new SqlParameter("@L2", SqlDbType.VarChar);
            p4.Value = _L2;
            SqlParameter p5 = new SqlParameter("@L3", SqlDbType.VarChar);
            p5.Value = _L3;
            SqlParameter p6 = new SqlParameter("@L4", SqlDbType.VarChar);
            p6.Value = _L4;
            SqlParameter p7 = new SqlParameter("@L5", SqlDbType.VarChar);
            p7.Value = _L5;
            SqlParameter p8 = new SqlParameter("@user", SqlDbType.VarChar);
            p8.Value = _userName;
            SqlParameter p9 = new SqlParameter("@uploadeddate", SqlDbType.VarChar);
            p9.Value = _uploadingDate;

            string _query = "TF_UpdateRelationshipMaster";
            TF_DATA objSave = new TF_DATA();
            _result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8, p9);



            string _script = "";
            if (_result == "added")
            {
                _script = "window.location='TF_ViewRelationshipMaster.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
            {
                if (_result == "updated")
                {
                    _script = "window.location='TF_ViewRelationshipMaster.aspx?result=" + _result + "'";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                }
                //else
                //    //labelMessage.Text = _result;
            }


        }
        ////clearall();
    }
   

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewRelationshipMaster.aspx", true);
    }
    protected void fillDetails(string _businessSourceID)
    {
        SqlParameter p1 = new SqlParameter("@L1", SqlDbType.VarChar);
        p1.Value = _businessSourceID;
        string _query = "TF_GetRelationshipMasterDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {

            //txtID.Text = dt.Rows[0]["L1"].ToString().Trim().Substring(0, 1);
            //txtBusinessSourceID1.Text = dt.Rows[0]["L1"].ToString().Trim().Substring(1);
            //txtID2.Text = dt.Rows[0]["L2"].ToString().Trim().Substring(0, 1);
            //txtBusinessSourceID2.Text = dt.Rows[0]["L2"].ToString().Trim().Substring(1);
            //txtID3.Text = dt.Rows[0]["L3"].ToString().Trim().Substring(0, 1);
            //txtBusinessSourceID3.Text = dt.Rows[0]["L3"].ToString().Trim().Substring(1);
            //txtID4.Text = dt.Rows[0]["L4"].ToString().Trim().Substring(0, 1);
            //txtBusinessSourceID4.Text = dt.Rows[0]["L4"].ToString().Trim().Substring(1);
            //txtID5.Text = dt.Rows[0]["L5"].ToString().Trim().Substring(0, 1);
            //txtBusinessSourceID5.Text = dt.Rows[0]["L5"].ToString().Trim().Substring(1);


            txtBusinessSourceID1.Text = dt.Rows[0]["L1"].ToString().Trim();
            txtBusinessSourceID2.Text = dt.Rows[0]["L2"].ToString().Trim();
            txtBusinessSourceID3.Text = dt.Rows[0]["L3"].ToString().Trim();
            txtBusinessSourceID4.Text = dt.Rows[0]["L4"].ToString().Trim();
            txtBusinessSourceID5.Text = dt.Rows[0]["L5"].ToString().Trim();
            fillBusSourceIdDescription1();
            fillBusSourceIdDescription2();
            fillBusSourceIdDescription3();
            fillBusSourceIdDescription4();
            fillBusSourceIdDescription5();
           
        }
    }
    public void fillBusSourceIdDescription1()
    {
        lblName1.Text = "";

        //string BusSourceid = txtID.Text.Trim() +txtBusinessSourceID1.Text.Trim();
        string BusSourceid =  txtBusinessSourceID1.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@businessSourceID", SqlDbType.VarChar);
        p1.Value = BusSourceid;

        string _query = "TF_GetBusinessSourceMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblName1.Text = dt.Rows[0]["NAME"].ToString().Trim();
        }
        else
        {
            txtBusinessSourceID1.Text = "";
            //lblName1.Text = "";
            lblName1.Text = "Invalid Id";
        }
    }

    public void fillBusSourceIdDescription2()
    {
        lblName2.Text = "";

        string BusSourceid = txtBusinessSourceID2.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@businessSourceID", SqlDbType.VarChar);
        p1.Value = BusSourceid;

        string _query = "TF_GetBusinessSourceMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblName2.Text = dt.Rows[0]["NAME"].ToString().Trim();
        }
        else
        {
            txtBusinessSourceID2.Text = "";
            lblName2.Text = "Invalid Id";
        }
    }

    public void fillBusSourceIdDescription3()
    {
        lblName3.Text = "";

        string BusSourceid = txtBusinessSourceID3.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@businessSourceID", SqlDbType.VarChar);
        p1.Value = BusSourceid;

        string _query = "TF_GetBusinessSourceMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblName3.Text = dt.Rows[0]["NAME"].ToString().Trim();
        }
        else
        {
            txtBusinessSourceID3.Text = "";
            lblName3.Text = "Invalid Id";
        }
    }

    public void fillBusSourceIdDescription4()
    {
        lblName4.Text = "";

        string BusSourceid =  txtBusinessSourceID4.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@businessSourceID", SqlDbType.VarChar);
        p1.Value = BusSourceid;

        string _query = "TF_GetBusinessSourceMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblName4.Text = dt.Rows[0]["NAME"].ToString().Trim();
        }
        else
        {
            txtBusinessSourceID4.Text = "";
            lblName4.Text = "Invalid Id";
        }
    }

    public void fillBusSourceIdDescription5()
    {
        lblName5.Text = "";

        string BusSourceid = txtBusinessSourceID5.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@businessSourceID", SqlDbType.VarChar);
        p1.Value = BusSourceid;

        string _query = "TF_GetBusinessSourceMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblName5.Text = dt.Rows[0]["NAME"].ToString().Trim();
        }
        else
        {
            txtBusinessSourceID5.Text = "";
            lblName5.Text = "Invalid Id";
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewRelationshipMaster.aspx", true);
    }

    protected void txtBusinessSourceID1_TextChanged(object sender, EventArgs e)
    {
        if (txtBusinessSourceID1.Text.ToString() != "")
        {
            fillBusSourceIdDescription1();
            txtBusinessSourceID1.Focus();

        }
        else
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Enter Business Source Id')", true);
        }



        //if (txtBusinessSourceID1.Text.Length < 3)
        //{

        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Invalid Business Source Id')", true);
        //}



    }
    protected void txtBusinessSourceID2_TextChanged(object sender, EventArgs e)
    {
        if (txtBusinessSourceID2.Text.ToString() != "")
        {
            fillBusSourceIdDescription2();
            txtBusinessSourceID2.Focus();

        }
        else
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Enter Business Source Id')", true);
        }

        //if (txtBusinessSourceID1.Text.Length < 3)
        //{

        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Invalid Business Source Id')", true);
        //}
    }
    protected void txtBusinessSourceID3_TextChanged(object sender, EventArgs e)
    {
        if (txtBusinessSourceID3.Text.ToString() != "")
        {
            fillBusSourceIdDescription3();
            txtBusinessSourceID3.Focus();

        }
        else
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Enter Business Source Id')", true);
        }
    }
    protected void txtBusinessSourceID4_TextChanged(object sender, EventArgs e)
    {
        if (txtBusinessSourceID4.Text.ToString() != "")
        {
            fillBusSourceIdDescription4();
            txtBusinessSourceID4.Focus();

        }
        else
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Enter Business Source Id')", true);
        }
    }
    protected void txtBusinessSourceID5_TextChanged(object sender, EventArgs e)
    {
        if (txtBusinessSourceID5.Text.ToString() != "")
        {
            fillBusSourceIdDescription5();
            txtBusinessSourceID5.Focus();

        }
        else
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Enter Business Source Id')", true);
        }
    }
}
