using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Threading;
using System.Xml;


public partial class EDPMS_PaymentExtnAck : System.Web.UI.Page
{
    int cntrec, norecinxml;
    string result;
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
                fillBranch();
                ddlRefNo.SelectedValue = Session["userADCode"].ToString();
                ddlRefNo.Enabled = false;
            }
        }
    }


    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlRefNo.Items.Clear();
        //ListItem li = new ListItem();
        //li.Value = "All Branches";

        if (dt.Rows.Count > 0)
        {
            //li.Text = "All Branches";

            ddlRefNo.DataSource = dt.DefaultView;
            ddlRefNo.DataTextField = "BranchName";
            ddlRefNo.DataValueField = "AuthorizedDealerCode";
            ddlRefNo.DataBind();

        }
        //else
        //    li.Text = "No record(s) found";

        //ddlBranch.Items.Insert(0, li);

    }
    private string upload_file(string _filepath)
    {
        string uploadresult = "", _FileNo = "";
        string fileName = "";
        string portcode = "", exportType = "", export_Agency = "", ShipingBillNo = "", shppingdate = "", formNo = "",adcode="",ExtRelDate="",
            IECode = "", existingAdCode = "", newAdCode = "", errorcode = "", UploadedBy = "", UploadedDate = "";


        string path = Server.MapPath("~/GeneratedFiles/UploadedFiles");
        string _rowData = System.DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
        uploadresult = "fileuploaded";
        TF_DATA objSave = new TF_DATA();
        try
        {
            System.IO.File.SetAttributes(_filepath, FileAttributes.Normal);
            if (System.IO.File.Exists(_filepath))
            {
                try
                {
                    File.Delete(_filepath);
                }
                catch (Exception ex)  //or maybe in finally
                {
                    GC.Collect(); //kill object that keep the file. I think dispose will do the trick as well.
                    Thread.Sleep(10000); //Wait for object to be killed. 

                    File.Delete(_filepath); //File can be now deleted
                }
            }
        }
        catch { }
        try
        {
            FileUpload1.PostedFile.SaveAs(_filepath);
            uploadresult = "fileuploaded";
        }
        catch
        {
            uploadresult = "ioerror";
        }

        if (uploadresult == "fileuploaded")
        {
            var sr = _filepath;
            _FileNo = sr.Split('\\').Last();

            SqlParameter P22 = new SqlParameter("@FileName", _FileNo);
            string QueryCheckFile = "TF_Checkpmtextnack_FileName";
            DataTable dt = objSave.getData(QueryCheckFile, P22);
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('File  already Upload.');", true);

            }
            else
            {
                string[] values;
                values = _FileNo.Split('.');
                string Vales1 = values[1].ToString();
                fileName = Vales1.Substring(4, 3);
              

                if (fileName == "pen")
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(sr);

                    XmlNode Bank =
                        doc.SelectSingleNode("/bank/checkSum");
                    string noOfShippingBills =
                        Bank.SelectSingleNode("noOfShippingBills").InnerText;
                    XmlNode ShippingBills =
                        doc.SelectSingleNode("/bank/shippingBills");
                    XmlNodeList shippingBill =
                       ShippingBills.SelectNodes("shippingBill");

                    string[] values_p;
                    string _errorcode;
                    foreach (XmlNode node in shippingBill)
                    {

                        portcode = node["portCode"].InnerText;
                        norecinxml = norecinxml + 1;
                        exportType = node["exportType"].InnerText;
                        ShipingBillNo = node["shippingBillNo"].InnerText;
                        shppingdate = node["shippingBillDate"].InnerText;
                        adcode = node["adCode"].InnerText;
                        IECode = node["IECode"].InnerText;
                        ExtRelDate = node["extendedRealizationDate"].InnerText;
                        errorcode = node["errorCodes"].InnerText;
                        char[] splitchar = { ',' };
                        values_p = errorcode.Split(splitchar);
                        _errorcode = values_p[0].ToString();

                        UploadedBy = Session["UserName"].ToString();
                        UploadedDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                        SqlParameter p1 = new SqlParameter("@FileName", SqlDbType.VarChar);
                        p1.Value = _FileNo;

                        SqlParameter p6 = new SqlParameter("@shipbillno", SqlDbType.VarChar);
                        p6.Value = ShipingBillNo;

                        SqlParameter p7 = new SqlParameter("@shipbilldate", SqlDbType.VarChar);
                        p7.Value = shppingdate;

                        SqlParameter p3 = new SqlParameter("@portcode", SqlDbType.VarChar);
                        p3.Value = portcode;

                        SqlParameter p5 = new SqlParameter("@adcode", SqlDbType.VarChar);
                        p5.Value = adcode;

                        SqlParameter p9 = new SqlParameter("@IEcode", SqlDbType.VarChar);
                        p9.Value = IECode;

                        SqlParameter p10 = new SqlParameter("@extreldate", SqlDbType.VarChar);
                        p10.Value = ExtRelDate;

                        SqlParameter p12 = new SqlParameter("@Error_Code", SqlDbType.VarChar);
                        p12.Value = _errorcode;

                        SqlParameter p13 = new SqlParameter("@AddedBy", SqlDbType.VarChar);
                        p13.Value = UploadedBy;

                        SqlParameter p14 = new SqlParameter("@AddedDate", SqlDbType.VarChar);
                        p14.Value = UploadedDate;

                        string _query = "TF_EDPMS_FileUpload_Acknowledgment_PaymentExtn";

                        result = objSave.SaveDeleteData(_query, p1, p3, p5, p6, p7, p9, p10, p12, p13, p14);

                        if (result == "Uploaded")
                        {
                            cntrec = cntrec + 1;
                        }

                        labelMessage.Text = "<b><font color='red'>" + cntrec + "</font> record(s) " + "Uploaded out of <font color='red'>" + norecinxml + "</font> from file " + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName) + "</b>";
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Select  Vaild File');", true);

                }
            }
            return uploadresult;

        }
        else
            return uploadresult;

    }
    protected void btnupload_Click(object sender, EventArgs e)
    {
        string result = "";

        string path = Server.MapPath("~/GeneratedFiles/UploadedFiles");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        result = upload_file(path + "\\" + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName));
    }
}