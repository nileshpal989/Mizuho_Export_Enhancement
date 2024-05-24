using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EXP_TF_AddEditPaymentExtn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //int _records = 0;
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            string year = ""; string BranchName = ""; string custacno = ""; string docno = ""; string shipbillno = "";
            if (!IsPostBack)
            {
                if (Session["userRole"].ToString() == "Supervisor")
                {
                    btnSave.Enabled = false;
                    lblSupervisormsg.Visible = true;
                }
                else
                {
                    btnSave.Enabled = true;
                    lblSupervisormsg.Visible = false;
                }



                if (Request.QueryString["mode"].ToString()=="add")
                {
                    fillbranch();
                     txt_year.Text = Request.QueryString["Year"].ToString();
                    
                     //txt_branch.Text = Request.QueryString["BranchName"].ToString();
                     txt_custacno.Focus();
                     //txt_custacno.Text = Request.QueryString["BranchName"].ToString();
                    // fillgrid();
                }

                else
                {

                    txt_custacno.Text = Request.QueryString["custacno"].ToString();

                     fillcust();
                     txt_custacno.Enabled = false;
                     btncusthelp.Enabled = false;
                     fillbranch();
                     txt_year.Text = Request.QueryString["DocYear"].ToString();
                     
                     //txt_branch.Text = Request.QueryString["BranchName"].ToString();
                     docno = Request.QueryString["docno"].ToString();
                     shipbillno = Request.QueryString["shipbillno"].ToString();

                     string query = "filladdeditdata";

                     SqlParameter p1 = new SqlParameter("@shipbillno",shipbillno);
                     SqlParameter p2 = new SqlParameter("@docno", docno);
                     TF_DATA objdata=new TF_DATA();
                     DataTable dt = objdata.getData(query, p1, p2);

                    if (dt.Rows.Count>0)
                    {
                        int _records = dt.Rows.Count;
                        int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());

                         GridViewpaymentextn.DataSource = dt.DefaultView;
                         GridViewpaymentextn.DataBind();
                         GridViewpaymentextn.Visible = true;
                         rowPager.Visible = true;
                         //labelMessage.Visible = false;
                         pagination(_records, _pageSize);
                    }

                    else
                    {
                        
                        GridViewpaymentextn.Visible = false;

                    }
                      
                }

               
            }

           // btncusthelp.Attributes.Add("onclick", "return opencust(event);");
            
           
        }


    }

    protected void fillbranch()
    {
        string adcode=Request.QueryString["BranchName"].ToString();
        string query = "getbranchname";
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@adcode", adcode);
         DataTable dt = objData.getData(query, p1);

         if (dt.Rows.Count > 0)
         {
             txt_branch.Text = dt.Rows[0]["BranchName"].ToString();
         
         }
         else
         {
             txt_branch.Text = "";
         }
    
    }

    protected void fillgrid()
    {
        int _pageSize = 0;
        string search = txt_branch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@branch", SqlDbType.VarChar);
        p1.Value = search;

        SqlParameter p2 = new SqlParameter("@year", SqlDbType.VarChar);
        p2.Value = txt_year.Text.Trim();

        //SqlParameter p3 = new SqlParameter("@DocType", SqlDbType.VarChar);
        //p3.Value = txtDocPrFx.Text.Trim();

        SqlParameter p4 = new SqlParameter("@custid", SqlDbType.VarChar);
        p4.Value = txt_custacno.Text.Trim();

        string query = "PaymentExtnData";
        TF_DATA objData = new TF_DATA();

        DataTable dt = objData.getData(query, p1, p2, p4);

        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
             _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewpaymentextn.PageSize = _pageSize;
            GridViewpaymentextn.DataSource = dt.DefaultView;
            GridViewpaymentextn.DataBind();
            GridViewpaymentextn.Visible = true;
            rowPager.Visible = true;
            lblmessage.Visible = false;
            pagination(_records, _pageSize);
        }

        else
        {
            GridViewpaymentextn.Visible = false;
            GridViewpaymentextn.Visible = false;
            rowPager.Visible = false;
            lblmessage.Text = "No record(s) found.";
            //labelMessage.Visible = true;
        }


        
    }

    protected void GridViewpaymentextn_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
        string _script = "";
        string _docNo = "";
        string docdate = "";
        string shipbillno ="";
        string shipbilldate = "";
        string adbank = "1";
        Button lb = (Button)e.CommandSource;
        string letterno = ((TextBox)lb.Parent.FindControl("txt_letterno")).Text;
        string letterdate = ((TextBox)lb.Parent.FindControl("txt_letterdate")).Text;
        string extndate = ((TextBox)lb.Parent.FindControl("txt_extndate")).Text;

        string[] values_p;

        string str = e.CommandArgument.ToString();
        string txtourrefno = "", Approval = "";
        if (str != "")
        {
            char[] splitchar = { ',' };
            values_p = str.Split(splitchar);
             _docNo = values_p[0].ToString();
             docdate = values_p[1].ToString();
             shipbillno = values_p[2].ToString();
             shipbilldate = values_p[3].ToString();
             //letterno = values_p[4].ToString();
             //letterdate = values_p[5].ToString();
             //extndate = values_p[6].ToString();

        }

        SqlParameter p1 = new SqlParameter("@branchcode", txt_branch.Text);
        SqlParameter p2 = new SqlParameter("@year", txt_year.Text);
        SqlParameter p3 = new SqlParameter("@custacno", txt_custacno.Text);
        SqlParameter p4 = new SqlParameter("@docno", _docNo);
        SqlParameter p5 = new SqlParameter("@docdate", docdate);
        SqlParameter p6 = new SqlParameter("@shipbillno", shipbillno);
        SqlParameter p7 = new SqlParameter("@shipbilldate", shipbilldate);
        SqlParameter p8 = new SqlParameter("@adbank", adbank);
        SqlParameter p9 = new SqlParameter("@letterno", letterno);
        SqlParameter p10 = new SqlParameter("@letterdate", letterdate);
        SqlParameter p11 = new SqlParameter("@extndate", extndate);
        SqlParameter p12 = new SqlParameter("@addedby", Session["userName"].ToString());
        SqlParameter p13 = new SqlParameter("@addeddate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        SqlParameter p14 = new SqlParameter("@updatedby", Session["userName"].ToString());
        SqlParameter p15 = new SqlParameter("@updateddate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        //SqlParameter p16 = new SqlParameter("@checkstatus", check);

        if (letterno != "" && letterdate != "" && extndate != "")
        {
            TF_DATA objdata = new TF_DATA();
            string qur = "paydatediff";
            SqlParameter pshipdate = new SqlParameter("@shipbilldate",shipbilldate);
            SqlParameter pextndate = new SqlParameter("@extndate",extndate);

            DataTable dt = objdata.getData(qur, pshipdate, pextndate);
            if (dt.Rows[0]["result"].ToString()=="1")
            {
                string _query = "Insertintopaymentextension";

                result = objdata.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15);

                if (result == "inserted")
                {
                    //_script = "window.location='EXP_ViewPaymentExtension.aspx?result=" + result + "'";
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);
                    fillgrid();
                }
                else
                {
                    _script = "window.location='EXP_ViewPaymentExtension.aspx?result=" + result + "'";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                    //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);  
                }
                
            }

            else
            {
                string date = dt.Rows[0]["result"].ToString();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Payment Extension Date Should be greater Than equal to "+date+"');", true);

                TextBox txtextndate = ((TextBox)lb.Parent.FindControl("txt_extndate"));
                txtextndate.Text = "__/__/____";

                //txtextndate.Attributes.Add("onblur", "JavaScript:Edate('" + txtextndate.ClientID + "');");    
            }

            
        }

        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Letter No,Letter Date,Extn Date cant be blank.');", true);

           

          //  gridview.Rows[e.NewEditIndex].FindControl("txtname").Focus();

        }

       

    }
   

    protected void GridViewpaymentextn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType==DataControlRowType.DataRow)
        {
            //TextBox txtlno = new TextBox();
            //TextBox txtldate = new TextBox();
            //TextBox txtextndate = new TextBox();

           TextBox txtlno = (TextBox)e.Row.FindControl("txt_letterno");
           TextBox txtldate = (TextBox)e.Row.FindControl("txt_letterdate");
           TextBox txtextndate = (TextBox)e.Row.FindControl("txt_extndate");

           Button btnDelete = (Button)e.Row.FindControl("btnadd");

           var a = txtlno.Text;
           var b = txtldate.Text;
           var c = txtextndate.Text;

           // txtlno.Attributes.Add("onblur", "JavaScript:calc('" + a + "', '" + b + "','" + c + "');");

           //      txtlno.Attributes.Add("onblur", "JavaScript:calc('" + txtlno.ClientID + "', '" + txtldate.ClientID + "','" + txtextndate.ClientID + "');");

          //            txtldate.Attributes.Add("onblur", "JavaScript:Ldate('" + txtlno.ClientID + "', '" + txtldate.ClientID + "','" + txtextndate.ClientID + "');");     

           //     txtextndate.Attributes.Add("onblur", "JavaScript:Edate('" + txtlno.ClientID + "', '" + txtldate.ClientID + "','" + txtextndate.ClientID + "');");    


        }
        
    }


    protected void RowChkAllow1_CheckedChanged(object sender, EventArgs e)
    {
        string result = "";

        for (int i = 0; i < GridViewpaymentextn.Rows.Count; i++)
        {
            CheckBox chkrow1 = (CheckBox)GridViewpaymentextn.Rows[i].FindControl("RowChkAllow1");
            if (chkrow1.Checked == true)
            {
                string check="1";

              

                Label lbldocno = new Label();
                Label lbldocdate = new Label();
                Label lblshipbillno = new Label();
                Label lblshipbilldate = new Label();

                DropDownList adbank = new DropDownList();
                TextBox letterno = new TextBox();
                TextBox letterdate = new TextBox();
                TextBox extndate = new TextBox();

                string docno; string docdate; string shibillno; string shibilldate; string adb; string letno; string letdate; string extdate;


                //(GridViewpaymentextn.DataSource as DataTable).Rows.Count();

                    lbldocno = (Label)GridViewpaymentextn.Rows[i].Cells[0].FindControl("lbldocno");
                    docno = lbldocno.Text;

                    lbldocdate = (Label)GridViewpaymentextn.Rows[i].Cells[1].FindControl("lbldocdate");
                    docdate = lbldocdate.Text;

                    lblshipbillno = (Label)GridViewpaymentextn.Rows[i].Cells[2].FindControl("lblshipbillno");
                    shibillno = lblshipbillno.Text;

                    lblshipbilldate = (Label)GridViewpaymentextn.Rows[i].Cells[3].FindControl("lblshipbilldate");
                    shibilldate = lblshipbilldate.Text;

                    adbank = (DropDownList)GridViewpaymentextn.Rows[i].Cells[4].FindControl("ddl_adbank");
                    adb = adbank.SelectedValue;

                    letterno = (TextBox)GridViewpaymentextn.Rows[i].Cells[5].FindControl("txt_letterno");
                    letno = letterno.Text;

                    letterdate = (TextBox)GridViewpaymentextn.Rows[i].Cells[6].FindControl("txt_letterdate");
                    letdate = letterdate.Text;

                    extndate = (TextBox)GridViewpaymentextn.Rows[i].Cells[7].FindControl("txt_extndate");
                    extdate = extndate.Text;

                    SqlParameter p1 = new SqlParameter("@branchcode", txt_branch.Text);
                    SqlParameter p2 = new SqlParameter("@year", txt_year.Text);
                    SqlParameter p3 = new SqlParameter("@custacno", txt_custacno.Text);
                    SqlParameter p4 = new SqlParameter("@docno", docno);
                    SqlParameter p5 = new SqlParameter("@docdate", docdate);
                    SqlParameter p6 = new SqlParameter("@shipbillno", shibillno);
                    SqlParameter p7 = new SqlParameter("@shipbilldate", shibilldate);
                    SqlParameter p8 = new SqlParameter("@adbank", adb);
                    SqlParameter p9 = new SqlParameter("@letterno", letno);
                    SqlParameter p10 = new SqlParameter("@letterdate", letdate);
                    SqlParameter p11 = new SqlParameter("@extndate", extdate);
                    SqlParameter p12 = new SqlParameter("@addedby", Session["userName"].ToString());
                    SqlParameter p13 = new SqlParameter("@addeddate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                    SqlParameter p14 = new SqlParameter("@updatedby", Session["userName"].ToString());
                    SqlParameter p15 = new SqlParameter("@updateddate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                    SqlParameter p16 = new SqlParameter("@checkstatus", check);

                    if (letno != "" && letdate != "" && extdate != "")
                    {
                        string _query = "Insertintopaymentextension";
                        TF_DATA objdata = new TF_DATA();
                        result = objdata.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15,p16);
                    }

                    else
                    {
                          ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Letter No,Letter Date,Extn Date Cant be blank.');", true);  
                    }
                
            }

            else if(chkrow1.Checked==false)
            {
                string _query = "Deletefrompayment";
                
                 Label lbldocno = new Label();
                    lbldocno = (Label)GridViewpaymentextn.Rows[i].Cells[0].FindControl("lbldocno");
                    string docno = lbldocno.Text;

                SqlParameter p4 = new SqlParameter("@docno", docno);
                TF_DATA objdata = new TF_DATA();

                 string result1 = objdata.SaveDeleteData(_query, p4);
                 if (result1=="update")
                 {
                     fillgrid();
                 }

            }

        }



        //string _script = "";

        //if (result == "inserted")
        //{
        //    _script = "window.location='EXP_ViewPaymentExtension.aspx?result=" + result + "'";
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        //    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);  
        //}
        //else
        //{
        //    _script = "window.location='EXP_ViewPaymentExtension.aspx?result=" + result + "'";
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        //    //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);  
        //}
           
        
       
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        string query = "PaymentExtnData";
        TF_DATA objData = new TF_DATA();

        string search = txt_branch.Text.Trim();
        SqlParameter pb = new SqlParameter("@branch", SqlDbType.VarChar);
        pb.Value = search;

        SqlParameter py = new SqlParameter("@year", SqlDbType.VarChar);
        py.Value = txt_year.Text.Trim();

        //SqlParameter p3 = new SqlParameter("@DocType", SqlDbType.VarChar);
        //p3.Value = txtDocPrFx.Text.Trim();

        SqlParameter pc = new SqlParameter("@custid", SqlDbType.VarChar);
        pc.Value = txt_custacno.Text.Trim();

        DataTable dt = objData.getData(query, pb, py, pc);

        if (dt.Rows.Count > 0)
        {
       
        string result = "";

            Label lbldocno = new Label();
            Label lbldocdate = new Label();
            Label lblshipbillno = new Label();
            Label lblshipbilldate = new Label();

            DropDownList adbank = new DropDownList();
            TextBox letterno = new TextBox();
            TextBox letterdate = new TextBox();
            TextBox extndate = new TextBox();
        

            string docno; string docdate; string shibillno; string shibilldate; string adb; string letno; string letdate; string extdate;


             //(GridViewpaymentextn.DataSource as DataTable).Rows.Count();

         

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lbldocno = (Label)GridViewpaymentextn.Rows[i].Cells[0].FindControl("lbldocno");
                docno = lbldocno.Text;

                lbldocdate = (Label)GridViewpaymentextn.Rows[i].Cells[1].FindControl("lbldocdate");
                docdate = lbldocdate.Text;

                lblshipbillno = (Label)GridViewpaymentextn.Rows[i].Cells[2].FindControl("lblshipbillno");
                shibillno = lblshipbillno.Text;

                lblshipbilldate = (Label)GridViewpaymentextn.Rows[i].Cells[3].FindControl("lblshipbilldate");
                shibilldate = lblshipbilldate.Text;

                adbank = (DropDownList)GridViewpaymentextn.Rows[i].Cells[4].FindControl("ddl_adbank");
                adb = adbank.SelectedValue;

                letterno = (TextBox)GridViewpaymentextn.Rows[i].Cells[5].FindControl("txt_letterno");
                letno = letterno.Text;



                letterdate = (TextBox)GridViewpaymentextn.Rows[i].Cells[6].FindControl("txt_letterdate");
                letdate = letterdate.Text;

                extndate = (TextBox)GridViewpaymentextn.Rows[i].Cells[7].FindControl("txt_extndate");
                extdate = extndate.Text;

                SqlParameter p1 = new SqlParameter("@branchcode", txt_branch.Text);
                SqlParameter p2 = new SqlParameter("@year", txt_year.Text);
                SqlParameter p3 = new SqlParameter("@custacno", txt_custacno.Text);
                SqlParameter p4 = new SqlParameter("@docno", docno);
                SqlParameter p5 = new SqlParameter("@docdate", docdate);
                SqlParameter p6 = new SqlParameter("@shipbillno", shibillno);
                SqlParameter p7 = new SqlParameter("@shipbilldate", shibilldate);
                SqlParameter p8 = new SqlParameter("@adbank", adb);
                SqlParameter p9 = new SqlParameter("@letterno", letno);
                SqlParameter p10 = new SqlParameter("@letterdate", letdate);
                SqlParameter p11 = new SqlParameter("@extndate", extdate);
                SqlParameter p12 = new SqlParameter("@addedby", Session["userName"].ToString());
                SqlParameter p13 = new SqlParameter("@addeddate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                SqlParameter p14 = new SqlParameter("@updatedby", Session["userName"].ToString());
                SqlParameter p15 = new SqlParameter("@updateddate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

                if (letno!="" && letdate!="" && extdate!="")
                    
                {
                    string _query = "Insertintopaymentextension";
                    TF_DATA objdata = new TF_DATA();
                    result = objdata.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15);
                }

               // else
               // {
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Letter Date,Letter No,Extension Date Can't be Blank');", true);
               // }
               

            }

            string _script = "";

            if (result == "inserted")
            {
                _script = "window.location='EXP_ViewPaymentExtension.aspx?result=" + result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);  
            }
            else
            {
                _script = "window.location='EXP_ViewPaymentExtension.aspx?result=" + result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);  
            }

        }

        else
        {
            if (txt_custacno.Text=="")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Customer A/C No. cant be blank.');", true);
                txt_custacno.Focus();
            }
        }
        
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_ViewPaymentExtension.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_ViewPaymentExtension.aspx");
    }
    protected void txt_custacno_TextChanged(object sender, EventArgs e)
    {

        fillcust();

        fillgrid();
       
        
    }

    protected void fillcust()
    {

        string query = "HelpCustSearchId";
        TF_DATA objdata = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@id", txt_custacno.Text);
        DataTable dt = objdata.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            lbl_custname.Text = dt.Rows[0]["CUST_NAME"].ToString();

        }

        else
        {
            lbl_custname.Text = "Invalid Customer A/C No.";
            txt_custacno.Focus();
        }
    
    }


    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewpaymentextn.PageCount != GridViewpaymentextn.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewpaymentextn.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewpaymentextn.PageIndex + 1) + " of " + GridViewpaymentextn.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewpaymentextn.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewpaymentextn.PageIndex != (GridViewpaymentextn.PageCount - 1))
        {
            pgcontrols.enablelastnav(btnnavnext, btnnavlast);
        }
        else
        {
            pgcontrols.disablelastnav(btnnavnext, btnnavlast);
        }
    }
    private void navigationVisibility(Boolean visibility)
    {
        lblpageno.Visible = visibility;
        lblrecordno.Visible = visibility;
        btnnavfirst.Visible = visibility;
        btnnavlast.Visible = visibility;
        btnnavnext.Visible = visibility;
        btnnavpre.Visible = visibility;
    }
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewpaymentextn.PageIndex = 0;
        fillgrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewpaymentextn.PageIndex > 0)
        {
            GridViewpaymentextn.PageIndex = GridViewpaymentextn.PageIndex - 1;
        }

        fillgrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewpaymentextn.PageIndex != GridViewpaymentextn.PageCount - 1)
        {
            GridViewpaymentextn.PageIndex = GridViewpaymentextn.PageIndex + 1;
        }
        fillgrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewpaymentextn.PageIndex = GridViewpaymentextn.PageCount - 1;
        fillgrid();
    }
}
