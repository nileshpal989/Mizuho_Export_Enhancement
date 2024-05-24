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
using System.Net;
using System.Web.Services;


public partial class TF_Log_Out : System.Web.UI.Page
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
            Session.Abandon();
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
    public void saveLog()
    {
        string ipAddressW = GetIPAddress();

        SqlParameter p1 = new SqlParameter("@username", SqlDbType.VarChar);
        p1.Value = Session["userName"].ToString();
        SqlParameter p2 = new SqlParameter("@loggedin", DBNull.Value);
        p2.Value = "";
        SqlParameter p3 = new SqlParameter("@loggedout", SqlDbType.DateTime);
        p3.Value = System.DateTime.Now;
        SqlParameter p4 = new SqlParameter("@type", SqlDbType.VarChar);
        p4.Value = "LOGOUT";
        SqlParameter p5 = new SqlParameter("@id", SqlDbType.VarChar);
        p5.Value = Session["LoggedUserId"].ToString();

        SqlParameter par6 = new SqlParameter("@id_1", SqlDbType.VarChar);
        par6.Value = Session["LoggedUserId_concurrent"].ToString();

        string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
        //Console.WriteLine(hostName);
        // Get the IP  
        string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

        // SqlParameter pIP = new SqlParameter("@IP", SqlDbType.VarChar);
        // pIP.Value = myIP;

        SqlParameter PIPNEw = new SqlParameter("@IP", SqlDbType.VarChar);
        PIPNEw.Value = ipAddressW;

        string _qurey = "TF_AddLoginLogoutLog";

        string _query2 = "TF_AddLoginLogoutLog_Concurrent_Session";

        TF_DATA objSave = new TF_DATA();

        string s = objSave.SaveDeleteData(_qurey, p1, p2, p3, p4, p5);
        string s1 = objSave.SaveDeleteData(_query2, p1, p2, p3, p4, par6, PIPNEw);

        //TF_DATA objSave = new TF_DATA();
        //string s = objSave.AddLoginLogoutAuditTrail(Session["userName"].ToString(), "", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), "LOGOUT", Session["LoggedUserId"].ToString());
    }
    [WebMethod]
    public static string CheckUserSession(string userName)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@UserName",userName.Trim());
        string Status = obj.SaveDeleteData("TF_CheckUserLoginStatus",p1);
        return Status;
    }
}
