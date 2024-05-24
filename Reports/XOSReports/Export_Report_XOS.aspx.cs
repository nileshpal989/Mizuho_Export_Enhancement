using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Drawing;


public partial class Reports_EXPReports_Export_Report_XOS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {



            if (!IsPostBack)
            {
                clearControls();               
                fillddlBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch_SelectedIndexChanged(null, null);
                ddlBranch.Enabled = false;
                txtFromDate.Focus();
            }
            txtFromDate.Attributes.Add("onblur", "return isValidDate(" + txtFromDate.ClientID + "," + "'As On Date'" + " );");

            btnCreate.Attributes.Add("onclick", "return validateSave();");
        }


    }

    public void fillddlBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
    }

    protected void clearControls()
    {
        txtFromDate.Text = "";

        txtFromDate.Focus();
    }


    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
        labelMessage.Text = "";
      //      txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
    }


    protected void btnCreate_Click(object sender, EventArgs e)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter p = new SqlParameter("@startdate", SqlDbType.VarChar);
        p.Value = txtFromDate.Text.ToString().Trim();
        string _qry1 = "tf_EXPORT_Report_XOS_getcurrencyCardMaster";
        DataTable dt1 = objData1.getData(_qry1, p);
        if (dt1.Rows.Count > 0)
        {

            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            dateInfo.ShortDatePattern = "dd/MM/yyyy";
            bool flag = true;
            if (ddlBranch.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Enter Branch Name.');", true);
                flag = false;
            }
            if (txtFromDate.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Enter As on Date');", true);
                flag = false;
            }
            if (flag == true)
            {
                DateTime documentDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
                string _directoryPath = "XOS/" + ddlBranch.Text.ToString();
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/" + _directoryPath);


                TF_DATA objData = new TF_DATA();
                SqlParameter p1 = new SqlParameter("@startdate", SqlDbType.VarChar);
                p1.Value = documentDate.ToString("MM/dd/yyyy");
                SqlParameter p2 = new SqlParameter("@Branch", SqlDbType.VarChar);
                p2.Value = ddlBranch.Text.ToString().Trim();

                int cnt = 0;
                string _qry = "TF_Export_Report_XOS_16122013";
                DataTable dt = objData.getData(_qry, p1, p2);
                if (dt.Rows.Count > 0)
                {
                    if (!Directory.Exists(_directoryPath))
                    {
                        Directory.CreateDirectory(_directoryPath);
                    }
                    string _filePath = _directoryPath + "/" + ddlBranch.Text.ToString() + documentDate.ToString("dd-MM-yyyy") + ".csv";
                    StreamWriter sw;
                    sw = File.CreateText(_filePath);
                    //string s = "AD_CODE,XOS_PERIOD,IEC_CODE, EXPORT,EXPORT_BILL_NO, EXPORT_BILL_DATE,STATUS_HOLDER,EXPORTER_NAME," +
                    //           "EXPORTER_ADDERESS_LINE1,EXPORTER_ADDERESS_LINE2, CITY,PIN_CODE,PORT_OF_SHIPMENT, PORT_CODE,SHIPPING_BILL_NO," +
                    //           "SHIPPING_BILL_DATE,GR_PP_SDF_NO ,DATE_OF_EXPORT,DUE_DATE_OF_REALIZATION, EXTENSION_GRANTED, EXTN_GRANTING_AUTHORITY," +
                    //           "EXTN_GRANTED_UP_TO, COUNTRY_OF_EXPORT, COMMODITY_CODE, INVOICE_CURRENCY, INVOICE_AMOUNT_FC,INVOICE_AMOUNT_INR,REAILZED_CURRENCY," +
                    //           "REALIZED_AMOUNT_INR, OUTSTANDING_AMOUNT_INR, OVERSEAS_BUYER_NAME,OVERSEAS_BYUER_ADDRESS1, OVERSEAS_BYUER_ADDRESS2,OVERSEAS_BYUER_COUNTRY," +
                    //           "OVERSEAS_BYUER_PINCODE, BILL_REALIZED,REALIZATION_DATE , REMARK";
                //    sw.WriteLine(s);


                    for (int j = 0; j < dt.Rows.Count; j++)
                    {

                        string _strBranchName = dt.Rows[j]["BranchName"].ToString().Trim();
                        string _strBranchName1 = _strBranchName.Replace(",", "-");
                        string _strAD_CODE = dt.Rows[j]["ADcode"].ToString().Trim();
                        string _strAD_CODE1 = _strAD_CODE.Replace(",", "-");
                        string _strXOS_PERIOD = dt.Rows[j]["XOS_PERIOD"].ToString().Trim();
                        string _strXOS_PERIOD1 = _strXOS_PERIOD.Replace(",", "-");
                        string _strIEC_CODE = dt.Rows[j]["IEC_CODE"].ToString().Trim();
                        string _strIEC_CODE1 = _strIEC_CODE.Replace(",", "-");
                        string _strEXPORT = dt.Rows[j]["EXPORT"].ToString().Trim();
                        string _strEXPORT1 = _strEXPORT.Replace(",", "-");
                        string _strEXPORT_BILL_NO = dt.Rows[j]["EXPORT_BILL_NO"].ToString().Trim();
                        string _strEXPORT_BILL_NO1 = _strEXPORT_BILL_NO.Replace(",", "-");
                        string _strEXPORT_BILL_DATE = dt.Rows[j]["EXPORT_BILL_DATE"].ToString().Trim();
                        string _strEXPORT_BILL_DATE1 = _strEXPORT_BILL_DATE.Replace(",", "-");
                        string _strSTATUS_HOLDER = dt.Rows[j]["STATUS_HOLDER"].ToString().Trim();
                        string _strSTATUS_HOLDER1 = _strSTATUS_HOLDER.Replace(",", "-");
                        string _strEXPORTER_NAME = dt.Rows[j]["EXPORTER_NAME"].ToString().Trim();
                        string _strEXPORTER_NAME1 = _strEXPORTER_NAME.Replace(",", "-");
                        string _strEXPORTER_ADDERESS_LINE1 = dt.Rows[j]["EXPORTER_ADDERESS_LINE1"].ToString().Trim();
                        string _strEXPORTER_ADDERESS_LINE11 = _strEXPORTER_ADDERESS_LINE1.Replace(",", "-");
                        string _strEXPORTER_ADDERESS_LINE2 = dt.Rows[j]["EXPORTER_ADDERESS_LINE2"].ToString().Trim();
                        string _strEXPORTER_ADDERESS_LINE21 = _strEXPORTER_ADDERESS_LINE2.Replace(",", "-");
                        string _strCITY = dt.Rows[j]["CITY"].ToString().Trim();
                        string _strCITY1 = _strCITY.Replace(",", "-");
                        string _strPIN_CODE = dt.Rows[j]["PIN_CODE"].ToString().Trim();
                        string _strPIN_CODE1 = _strPIN_CODE.Replace(",", "-");
                        string _strPORT_OF_SHIPMENT = dt.Rows[j]["PORT_OF_SHIPMENT"].ToString().Trim();
                        string _strPORT_OF_SHIPMENT1 = _strPORT_OF_SHIPMENT.Replace(",", "-");
                        string _strPORT_CODE = dt.Rows[j]["PORT_CODE"].ToString().Trim();
                        string _strPORT_CODE1 = _strPORT_CODE.Replace(",", "-");
                        string _strSHIPPING_BILL_NO = dt.Rows[j]["SHIPPING_BILL_NO"].ToString().Trim();
                        string _strSHIPPING_BILL_NO1 = _strSHIPPING_BILL_NO.Replace(",", "-");
                        string _strSHIPPING_BILL_DATE = dt.Rows[j]["SHIPPING_BILL_DATE"].ToString().Trim();
                        string _strSHIPPING_BILL_DATE1 = _strSHIPPING_BILL_DATE.Replace(",", "-");
                        string _strGR_PP_SDF_NO = dt.Rows[j]["GR_PP_SDF_NO"].ToString().Trim();
                        string _strGR_PP_SDF_NO1 = _strGR_PP_SDF_NO.Replace(",", "-");
                        string _strDATE_OF_EXPORT = dt.Rows[j]["DATE_OF_EXPORT"].ToString().Trim();
                        string _strDATE_OF_EXPORT1 = _strDATE_OF_EXPORT.Replace(",", "-");
                        string _strDUE_DATE_OF_REALIZATION = dt.Rows[j]["DUE_DATE_OF_REALIZATION"].ToString().Trim();
                        string _strDUE_DATE_OF_REALIZATION1 = _strDUE_DATE_OF_REALIZATION.Replace(",", "-");
                        string _strEXTENSION_GRANTED = dt.Rows[j]["EXTENSION_GRANTED"].ToString().Trim();
                        string _strEXTENSION_GRANTED1 = _strEXTENSION_GRANTED.Replace(",", "-");
                        string _strEXTN_GRANTING_AUTHORITY = dt.Rows[j]["EXTN_GRANTING_AUTHORITY"].ToString().Trim();
                        string _strEXTN_GRANTING_AUTHORITY1 = _strEXTN_GRANTING_AUTHORITY.Replace(",", "-");
                        string _strEXTN_GRANTED_UP_TO = dt.Rows[j]["EXTN_GRANTED_UP_TO"].ToString().Trim();
                        string _strEXTN_GRANTED_UP_TO1 = _strEXTN_GRANTED_UP_TO.Replace(",", "-");
                        string _strCOUNTRY_OF_EXPORT = dt.Rows[j]["COUNTRY_OF_EXPORT"].ToString().Trim();
                        string _strCOUNTRY_OF_EXPORT1 = _strCOUNTRY_OF_EXPORT.Replace(",", "-");
                        string _strCOMMODITY_CODE = dt.Rows[j]["COMMODITY_CODE"].ToString().Trim();
                        string _strCOMMODITY_CODE1 = _strCOMMODITY_CODE.Replace(",", "-");
                        string _strINVOICE_CURRENCY = dt.Rows[j]["INVOICE_CURRENCY"].ToString().Trim();
                        string _strINVOICE_CURRENCY1 = _strINVOICE_CURRENCY.Replace(",", "-");
                        string _strINVOICE_AMOUNT_FC = dt.Rows[j]["INVOICE_AMOUNT_FC"].ToString().Trim();
                        string _strINVOICE_AMOUNT_FC1 = _strINVOICE_AMOUNT_FC.Replace(",", "-");
                        string _strINVOICE_AMOUNT_INR = dt.Rows[j]["INVOICE_AMOUNT_INR"].ToString().Trim();
                        string _strINVOICE_AMOUNT_INR1 = _strINVOICE_AMOUNT_INR.Replace(",", "-");
                        string _strREAILZED_CURRENCY = dt.Rows[j]["REAILZED_CURRENCY"].ToString().Trim();
                        string _strREAILZED_CURRENCY1 = _strREAILZED_CURRENCY.Replace(",", "-");
                        string _strREALIZED_AMOUNT_INR = dt.Rows[j]["REALIZED_AMOUNT"].ToString().Trim();
                        string _strREALIZED_AMOUNT_INR1 = _strREALIZED_AMOUNT_INR.Replace(",", "-");
                        string _strOUTSTANDING_AMOUNT_INR = dt.Rows[j]["OUTSTANDING_AMOUNT"].ToString().Trim();
                        string _strOUTSTANDING_AMOUNT_INR1 = _strOUTSTANDING_AMOUNT_INR.Replace(",", "-");
                        string _strOVERSEAS_BUYER_NAME = dt.Rows[j]["OVERSEAS_BUYER_NAME"].ToString().Trim();
                        string _strOVERSEAS_BUYER_NAME1 = _strOVERSEAS_BUYER_NAME.Replace(",", "-");
                        string _strOVERSEAS_BYUER_ADDRESS1 = dt.Rows[j]["OVERSEAS_BYUER_ADDRESS1"].ToString().Trim();
                        string _strOVERSEAS_BYUER_ADDRESS11 = _strOVERSEAS_BYUER_ADDRESS1.Replace(",", "-");
                        string _strOVERSEAS_BYUER_ADDRESS2 = dt.Rows[j]["OVERSEAS_BYUER_ADDRESS2"].ToString().Trim();
                        string _strOVERSEAS_BYUER_ADDRESS21 = _strOVERSEAS_BYUER_ADDRESS2.Replace(",", "-");
                        string _strOVERSEAS_BYUER_COUNTRY = dt.Rows[j]["OVERSEAS_BYUER_COUNTRY"].ToString().Trim();
                        string _strOVERSEAS_BYUER_COUNTRY1 = _strOVERSEAS_BYUER_COUNTRY.Replace(",", "-");
                        string _strOVERSEAS_BYUER_PINCODE = dt.Rows[j]["OVERSEAS_BYUER_PINCODE"].ToString().Trim();
                        string _strOVERSEAS_BYUER_PINCODE1 = _strOVERSEAS_BYUER_PINCODE.Replace(",", "-");
                        string _strBILL_REALIZED = dt.Rows[j]["BILL_REALIZED"].ToString().Trim();
                        string _strBILL_REALIZED1 = _strBILL_REALIZED.Replace(",", "-");
                        string _strREALIZATION_DATE = dt.Rows[j]["REALIZATION_DATE"].ToString().Trim();
                        string _strREALIZATION_DATE1 = _strREALIZATION_DATE.Replace(",", "-");
                        string _strREMARK = dt.Rows[j]["REMARK"].ToString().Trim();
                        string _strREMARK1 = _strREMARK.Replace(",", "-");


                      
                            sw.Write(_strAD_CODE1 + ",");
                            sw.Write(_strXOS_PERIOD1 + ",");
                            sw.Write(_strIEC_CODE1 + ",");
                            sw.Write(_strEXPORT1 + ",");
                            sw.Write(_strEXPORT_BILL_NO1 + ",");
                            sw.Write(_strEXPORT_BILL_DATE1 + ",");
                            sw.Write(_strSTATUS_HOLDER1 + ",");
                            sw.Write(_strEXPORTER_NAME1 + ",");
                            sw.Write(_strEXPORTER_ADDERESS_LINE11 + ",");
                            sw.Write(_strEXPORTER_ADDERESS_LINE21 + ",");

                            sw.Write(_strCITY1 + ",");
                            sw.Write(_strPIN_CODE1 + ",");
                            sw.Write(_strPORT_OF_SHIPMENT1 + ",");

                            sw.Write(_strPORT_CODE1 + ",");
                            sw.Write(_strSHIPPING_BILL_NO1 + ",");
                            sw.Write(_strSHIPPING_BILL_DATE1 + ",");

                            sw.Write(_strGR_PP_SDF_NO1 + ",");
                            sw.Write(_strDATE_OF_EXPORT1 + ",");
                            sw.Write(_strDUE_DATE_OF_REALIZATION1 + ",");

                            sw.Write(_strEXTENSION_GRANTED1 + ",");
                            sw.Write(_strEXTN_GRANTING_AUTHORITY1 + ",");
                            sw.Write(_strEXTN_GRANTED_UP_TO1 + ",");
                            sw.Write(_strCOUNTRY_OF_EXPORT1 + ",");
                            sw.Write(_strCOMMODITY_CODE1 + ",");
                            sw.Write(_strINVOICE_CURRENCY1 + ",");
                            sw.Write(Convert.ToDecimal(_strINVOICE_AMOUNT_FC1) + ",");
                            sw.Write(Convert.ToDecimal(_strINVOICE_AMOUNT_INR1) + ",");
                            sw.Write(_strREAILZED_CURRENCY1 + ",");
                            sw.Write(Convert.ToDecimal(_strREALIZED_AMOUNT_INR1) + ",");
                            sw.Write(Convert.ToDecimal(_strOUTSTANDING_AMOUNT_INR1) + ",");
                            sw.Write(_strOVERSEAS_BUYER_NAME1 + ",");

                            sw.Write(_strOVERSEAS_BYUER_ADDRESS11 + ",");
                            sw.Write(_strOVERSEAS_BYUER_ADDRESS21 + ",");
                            sw.Write(_strOVERSEAS_BYUER_COUNTRY1 + ",");

                            sw.Write(_strOVERSEAS_BYUER_PINCODE1 + ",");
                            sw.Write(_strBILL_REALIZED1 + ",");
                            sw.Write(_strREALIZATION_DATE1 + ",");

                            sw.WriteLine(_strREMARK1);


                            cnt++;
                      
                    }
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();

                    TF_DATA objServerName = new TF_DATA();
                    string _serverName = objServerName.GetServerName();

                    // labelMessage.Text = "Files Created Successfully on Server : " + _serverName + " in " + _directoryPath;
                    // ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Files Created Successfully');", true);

                    if (cnt == 0)
                    {
                        labelMessage.Text = "There is No Record for This Dates " + documentDate.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        string path = "file://" + _serverName + "/TF_GeneratedFiles";
                        string link = _directoryPath;
                        labelMessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
                    }


                    ddlBranch.Focus();

                }
                else
                {
                    //labelMessage.Text = "No Reocrds ";
                    labelMessage.Text = "There is No Record for This Dates " + documentDate.ToString("dd/MM/yyyy");
                    //ddlBranch.Focus();

                    txtFromDate.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('There is No Record for This Dates');", true);
                }
            }

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please Add Cross Currency Rate Master for a "+ txtFromDate.Text.ToString() + "' );", true);
        }
    }
}