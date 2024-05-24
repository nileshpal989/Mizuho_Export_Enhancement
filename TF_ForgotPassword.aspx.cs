using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class TF_ForgotPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            txtUserName.Focus();
        }        
    }
    protected void btnSendRequest_Click(object sender, EventArgs e)
    {
        Encryption objEncryption = new Encryption();
        string _UserName=objEncryption.encryptplaintext(txtUserName.Text.Trim());
        MailMethod(txtEmailID.Text.Trim(), _UserName);
    }
    public void MailMethod(string ToMailID, string UserName)
    {
        string todaydate = System.DateTime.Now.ToString("dd/MM/yyyy");
        string _FromMail = ConfigurationManager.AppSettings["FromMail"];
        string _Password = ConfigurationManager.AppSettings["Password"];
        string _ToMail = ToMailID;
        //string _IPAddress = GetIPAddress();
        try
        {
            string _SmtpServerHost = ConfigurationManager.AppSettings["SMTPServerHost"].ToUpper();
            int _SmtpPort = Convert.ToInt16(ConfigurationManager.AppSettings["SMTPPort"].ToUpper());
            string _link = ConfigurationManager.AppSettings["FPURL"].ToString();
            using (MailMessage mm = new MailMessage())
            {
                mm.To.Add(_ToMail);
                mm.From = new MailAddress(_FromMail);
                mm.Subject = "Resetting your password";
                string link=_link+"?userName="+UserName;
                string button = "<a href=" + link + ">Link to reset password</a>";
                mm.Body = "Hi, <br/><br/>It seems like you forgot your password for the LMCC TRADE FINANCE application. If this is true, click link mentioned below to reset your password. <br/><br/>" + button + "<br/><br/> If you did not forget your password you can safely ignore this email.<br/><br/>Regards,<br/>LMCC Team";
                SmtpClient smtp = new SmtpClient();
                smtp.Host = _SmtpServerHost;
                smtp.EnableSsl = false;
                mm.IsBodyHtml = true;
                NetworkCredential NetworkCred = new NetworkCredential(_FromMail, _Password);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = _SmtpPort;
                smtp.Send(mm);
                labelMessage.Text ="Email send successfully";
                txtUserName.Text = "";
                txtEmailID.Text = "";
            }
        }
        catch (Exception Ex)
        {
            labelMessage.Text = Ex.Message;
            //string path = "F:\\EmailSendLog.txt";
            //using (StreamWriter writer = new StreamWriter(path, true))
            //{
            //    writer.WriteLine(string.Format(Ex.Message, _FromMail, _Password, _ToMail, DateTime.Now));
            //    writer.Close();
            //}
        }
    }
    protected void txtUserName_TextChanged(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P_Username = new SqlParameter("@UserName", txtUserName.Text.Trim());
        string Result = obj.SaveDeleteData("TF_CheckUserName", P_Username);
        if(Result=="false")
        {
            lblUserName.Text = "Invalid User Name.";
            txtUserName.Text = "";
            txtUserName.Focus();
        }
        else
        {
            lblUserName.Text = "";
            txtEmailID.Focus();
        }
    }
    protected void txtEmailID_TextChanged(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P_Username = new SqlParameter("@UserName", txtUserName.Text.Trim());
        SqlParameter P_EmailID = new SqlParameter("@EmailID", txtEmailID.Text.Trim());
        string Result = obj.SaveDeleteData("TF_CheckUserEmail",P_Username, P_EmailID);
        if (Result == "false")
        {
            lblEmailID.Text = "Invalid Email ID for user.";
            txtEmailID.Text = "";
            txtEmailID.Focus();
        }
        else
        {
            lblEmailID.Text = "";
            btnSendRequest.Focus();
        }
    }
}