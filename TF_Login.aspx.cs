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
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.IO;


public partial class TF_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["UserName"] != null)
            {
                txtUserName.Text = Request.QueryString["UserName"].ToString();
                txtPassword.Focus();
            }
            else
            {
                txtUserName.Focus();
            }
            if (Request.QueryString["sessionout"] != null)
            {
                if (Request.QueryString["sessionout"].ToString() == "yes")
                {
                    labelMessage.Text = "Your session has timed out! Login again";
                    TF_DATA objSave = new TF_DATA();
                    SqlParameter p1 = new SqlParameter("@username", SqlDbType.VarChar);
                    p1.Value = "";
                    SqlParameter p2 = new SqlParameter("@loggedin", SqlDbType.VarChar);
                    p2.Value = "";
                    SqlParameter p3 = new SqlParameter("@loggedout", SqlDbType.VarChar);
                    p3.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    SqlParameter p4 = new SqlParameter("@type", SqlDbType.VarChar);
                    p4.Value = "LOGOUT";
                    SqlParameter p5 = new SqlParameter("@id", SqlDbType.VarChar);
                    p5.Value = Request.QueryString["sessionid"].ToString();
                    SqlParameter par6 = new SqlParameter("@id_1", SqlDbType.VarChar);
                    par6.Value = "";
                    string _qurey = "TF_AddLoginLogoutLog";
                    string _query2 = "TF_AddLoginLogoutLog_Concurrent_Session";
                    string s = objSave.SaveDeleteData(_qurey, p1, p2, p3, p4, p5);
                    string s1 = objSave.SaveDeleteData(_query2, p1, p2, p3, p4, par6);
                    txtUserName.Text = s.Substring(7);
                    //Session.Abandon();
                    //Session.RemoveAll();
                    //Session.Clear();

                    if (txtUserName.Text != "")
                        txtPassword.Focus();
                }
            }
            hdmremaindays.Value = ""; Hidden1.Value = "";
            btnLogin.Attributes.Add("onclick", "return validateSave();");
            btnalert.Attributes.Add("Style", "display:none;");
            btnUserList.Attributes.Add("onclick", "return OpenUserList();");
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Encryption objEncryption = new Encryption();
        string _userName = txtUserName.Text.Trim();
        string _password = objEncryption.encryptplaintext(txtPassword.Text.Trim());
        if (authenticateUser(_userName, _password) == "valid")
        {
            string ipAddressW = GetIPAddress();
            string _result = "", dtsAlert = "";
            SqlParameter p1 = new SqlParameter("@username", SqlDbType.VarChar);
            p1.Value = _userName;
            SqlParameter p2 = new SqlParameter("@password", SqlDbType.VarChar);
            p2.Value = _password;
            string _query = "TF_AlertUser";
            TF_DATA objData = new TF_DATA();
            DataTable dtnew = objData.getData(_query, p1, p2);
            if (dtnew.Rows.Count > 0)
            {
                hdmremaindays.Value = dtnew.Rows[0]["noofdaysleft"].ToString().Trim('-');
                dtsAlert = "true";
            }
            else
            {
                hdmremaindays.Value = "";
                dtsAlert = "false";
            }
            if (dtsAlert == "false")
            {
                TF_DATA objSave = new TF_DATA();
                if (Session["userRole"] != null)
                {
                    if (Session["userRole"].ToString() == "Admin")
                    {
                        _query = "TF_UpdateUserExpiry";
                        SqlParameter pUserName = new SqlParameter("@username", SqlDbType.VarChar);
                        pUserName.Value = _userName;
                        string _PassExpDate = DateTime.Now.AddMonths(2).ToString("MM/dd/yyyy");
                        SqlParameter pPassExpDate = new SqlParameter("@passwordexpirydate", SqlDbType.VarChar);
                        pPassExpDate.Value = _PassExpDate;
                        _result = objSave.SaveDeleteData(_query, pUserName, pPassExpDate);
                    }
                }
                if (Session["userRole"].ToString() != "Admin")
                {
                    string CheckUserLogoutStatus = objData.SaveDeleteData("TF_CheckUserLogoutStatus", p1);
                    if (CheckUserLogoutStatus != "0")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "messege", "alert('Your Session is active now you can not login. Please contact administrator!')", true);
                        return;
                    }
                }
                SqlParameter par1 = new SqlParameter("@username", SqlDbType.VarChar);
                par1.Value = _userName;
                SqlParameter par2 = new SqlParameter("@loggedin", System.Data.SqlDbType.DateTime);
                par2.Value = System.DateTime.Now;
                SqlParameter par3 = new SqlParameter("@loggedout", DBNull.Value);
                par3.Value = "";
                SqlParameter par4 = new SqlParameter("@type", SqlDbType.VarChar);
                par4.Value = "LOGIN";
                SqlParameter par5 = new SqlParameter("@id", SqlDbType.VarChar);
                par5.Value = "";
                SqlParameter par6 = new SqlParameter("@id_1", SqlDbType.VarChar);
                par6.Value = "";
                string hostName = Dns.GetHostName(); // Retrive the Name of HOST
                //Console.WriteLine(hostName);
                // Get the IP
                string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

                //  SqlParameter pIP = new SqlParameter("@IP", SqlDbType.VarChar);
                //   pIP.Value = myIP;

                SqlParameter PIPNEw = new SqlParameter("@IP", SqlDbType.VarChar);
                PIPNEw.Value = ipAddressW;
                string _query1 = "TF_AddLoginLogoutLog";
                string _query2 = "TF_AddLoginLogoutLog_Concurrent_Session";
                string s = objSave.SaveDeleteData(_query1, par1, par2, par3, par4, par5);
                string s1 = objSave.SaveDeleteData(_query2, par1, par2, par3, par4, par6, PIPNEw);
                Session["LoggedUserId"] = s;
                Session["LoggedUserId_concurrent"] = s1;

                if (Session["userEmailID"].ToString() != "" && Session["userRole"].ToString() == "Admin")
                {
                    //MailMethod(Session["userEmailID"].ToString(), "");
                }
                Response.Redirect("TF_ModuleSelection.aspx?type=0", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "confirm", "ConfirmCahngePaswd();", true);
            }
        }
        else
        {
            string suser = chkUser(_userName, _password);
            if (suser == "in-active")
            {
                txtUserName.Focus();
                labelMessage.Text = "Your account is in-active.Contact Administrator";
            }
            else if (suser == "pswd-exp")
            {
                txtUserName.Focus();
                labelMessage.Text = "Your Password has been expired.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "confirm", "ConfirmCahngePaswdExpired();", true);
            }
            else if (suser == "in-valid")
            {
                txtUserName.Focus();
                labelMessage.Text = "Invalid login details.";
            }
            else
            {
                txtUserName.Focus();
                labelMessage.Text = "Invalid login details.";
            }
        }
    }
    protected void btnalert_Click(object sender, EventArgs e)
    {
        Encryption objEncryption = new Encryption();
        string ipAddressW = GetIPAddress();
        string _userName = txtUserName.Text.Trim();
        if (Hidden1.Value == "1")
        {
            Response.Redirect("TF_ChangePassword.aspx?userName=" + _userName);
        }
        else
        {
            int remday = 0;
            remday = int.Parse(hdmremaindays.Value);
            if (remday != 0)
            {
                TF_DATA objSave = new TF_DATA();
                SqlParameter par1 = new SqlParameter("@username", SqlDbType.VarChar);
                par1.Value = _userName;
                SqlParameter par2 = new SqlParameter("@loggedin", System.Data.SqlDbType.DateTime);
                par2.Value = System.DateTime.Now;
                SqlParameter par3 = new SqlParameter("@loggedout", DBNull.Value);
                par3.Value = "";
                SqlParameter par4 = new SqlParameter("@type", SqlDbType.VarChar);
                par4.Value = "LOGIN";
                SqlParameter par5 = new SqlParameter("@id", SqlDbType.VarChar);
                par5.Value = "";
                SqlParameter par6 = new SqlParameter("@id_1", SqlDbType.VarChar);
                par6.Value = "";
                string hostName = Dns.GetHostName(); // Retrive the Name of HOST
                //Console.WriteLine(hostName);
                // Get the IP
                string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

                //  SqlParameter pIP = new SqlParameter("@IP", SqlDbType.VarChar);
                //   pIP.Value = myIP;

                SqlParameter PIPNEw = new SqlParameter("@IP", SqlDbType.VarChar);
                PIPNEw.Value = ipAddressW;
                string _query1 = "TF_AddLoginLogoutLog";
                string _query2 = "TF_AddLoginLogoutLog_Concurrent_Session";
                string s = objSave.SaveDeleteData(_query1, par1, par2, par3, par4, par5);
                string s1 = objSave.SaveDeleteData(_query2, par1, par2, par3, par4, par6, PIPNEw);
                Session["LoggedUserId"] = s;
                Session["LoggedUserId_concurrent"] = s1;
                if (Session["userEmailID"].ToString() != "" && Session["userRole"].ToString() == "Admin")
                {
                    //MailMethod(Session["userEmailID"].ToString(), "");
                }
                Response.Redirect("TF_ModuleSelection.aspx?type=0", true);
            }
        }
    }
    protected string authenticateUser(string _userName, string _password)
    {
        string sretrn = "";
        SqlParameter p1 = new SqlParameter("@username", SqlDbType.VarChar);
        p1.Value = _userName;
        SqlParameter p2 = new SqlParameter("@password", SqlDbType.VarChar);
        p2.Value = _password;
        string _query = "TF_AuthenticateUser";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            Session["userRole"] = dt.Rows[0]["userRole"].ToString().Trim();
            Session["userName"] = dt.Rows[0]["userName"].ToString().Trim();
            Session["userADCode"] = dt.Rows[0]["ADCode"].ToString().Trim();
            Session["userLBCode"] = dt.Rows[0]["BranchCode"].ToString().Trim();
            Session["userEmailID"] = dt.Rows[0]["EmailID"].ToString().Trim();
            sretrn = "valid";
        }
        return sretrn;
    }
    public string chkUser(string _userName, string _password)
    {
        string sretrn = "";
        SqlParameter p1 = new SqlParameter("@username", SqlDbType.VarChar);
        p1.Value = _userName;
        SqlParameter p2 = new SqlParameter("@password", SqlDbType.VarChar);
        p2.Value = _password;
        string _query = "TF_CheckUserExists";
        TF_DATA objData = new TF_DATA();
        DataTable dtnew = objData.getData(_query, p1, p2);
        if (dtnew.Rows.Count > 0)
        {
            int dtleft = 0;
            if (dtnew.Rows[0]["active"].ToString() == "2")
            {
                dtleft = Convert.ToInt32(dtnew.Rows[0]["noofdaysleft"].ToString());
                if (dtleft <= 0)
                {
                    sretrn = "in-active";
                }
                else
                {
                    sretrn = "pswd-exp";
                }
            }
            else
            {
                dtleft = Convert.ToInt32(dtnew.Rows[0]["noofdaysleft"].ToString());
                if (dtleft >= 0)
                {
                    sretrn = "pswd-exp";
                }
                else
                    sretrn = "active";
            }
        }
        else
        {
            sretrn = "in-valid";
        }
        return sretrn;
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Encryption objEncryption = new Encryption();
        string _userName = txtUserName.Text.Trim();
        string _password = objEncryption.encryptplaintext(txtPassword.Text.Trim());
        if (authenticateUser(_userName, _password) == "valid")
        {
            Response.Redirect("~/TF_ChangePassword.aspx?userName=" + _userName, true);
        }
        else
        {
            string suser = chkUser(_userName, _password);
            if (suser == "in-active")
            {
                txtUserName.Focus();
                labelMessage.Text = "Your account is in-active.Contact Administrator";
            }
            else if (suser == "pswd-exp")
            {
                txtUserName.Focus();
                labelMessage.Text = "Your Password has been expired.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "confirm", "ConfirmCahngePaswdExpired();", true);
            }
            else if (suser == "in-valid")
            {
                txtUserName.Focus();
                labelMessage.Text = "Invalid login details.";
            }
            else
            {
                txtUserName.Focus();
                labelMessage.Text = "Invalid login details.";
            }
        }
    }
    protected void btnForgotPassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TF_ForgotPassword.aspx");
    }
    public void MailMethod(string ToMailID, string UserName)
    {
        string todaydate = System.DateTime.Now.ToString("dd/MM/yyyy");
        string _FromMail = ConfigurationManager.AppSettings["FromMail"];
        string _Password = ConfigurationManager.AppSettings["Password"];
        string _ToMail = ToMailID;
        string _IPAddress = GetIPAddress();
        try
        {
            string _SmtpServerHost = ConfigurationManager.AppSettings["SMTPServerHost"].ToUpper();
            int _SmtpPort = Convert.ToInt16(ConfigurationManager.AppSettings["SMTPPort"].ToUpper());

            using (MailMessage mm = new MailMessage())
            {
                mm.To.Add(_ToMail);
                mm.From = new MailAddress(_FromMail);
                mm.Subject = "Your LMCC admin account has been accessed.";
                mm.Body = "Hi, <br/><br/> Your security is very important to us. This is to notify you that your admin account was used to access LMCC Applications. <br/><br/> email: " + _ToMail + "<br/> time: " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "<br/> IP address: " + _IPAddress + "<br/><br/>If this was you, you can ignore this alert. If you suspect any suspicious activity on your account, don't hesitate to get in touch: support@lmccsoft.com. <br/><br/> Regards, <br/> LMCC Team";
                SmtpClient smtp = new SmtpClient();
                smtp.Host = _SmtpServerHost;
                smtp.EnableSsl = false;
                mm.IsBodyHtml = true;
                NetworkCredential NetworkCred = new NetworkCredential(_FromMail, _Password);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = _SmtpPort;
                smtp.Send(mm);
                MailLog("Email send successfully");
            }
        }
        catch (Exception Ex)
        {
            string path = "F:\\EmailSendLog.txt";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(string.Format(Ex.Message, _FromMail, _Password, _ToMail, DateTime.Now));
                writer.Close();
            }
        }
    }
    public void MailLog(string Message)
    {
        string path = "C:\\EmailSendLog.txt";
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(Message);
            writer.Close();
        }
    }
    public static string GetIPAddress()
    {
        string ipAddress = string.Empty;
        foreach (IPAddress item in Dns.GetHostAddresses(HttpContext.Current.Request.UserHostAddress))
        {
            if (item.AddressFamily.ToString().Equals("InterNetwork"))
            {
                ipAddress = item.ToString();
                break;
            }
        }
        if (!string.IsNullOrEmpty(ipAddress))
        {
            return ipAddress;
        }
        foreach (IPAddress item in Dns.GetHostAddresses(Dns.GetHostName()))
        {
            if (item.AddressFamily.ToString().Equals("InterNetwork"))
            {
                ipAddress = item.ToString();
                break;
            }
        }
        return ipAddress;
    }
}
