<%@ Application Language="C#" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Net" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown
        //string ipAddressW = GetIPAddress();


    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
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

        SqlParameter PIPNEw = new SqlParameter("@IP", SqlDbType.VarChar);
        PIPNEw.Value = "";

        string _qurey = "TF_AddLoginLogoutLog";

        string _query2 = "TF_AddLoginLogoutLog_Concurrent_Session";

        TF_DATA objSave = new TF_DATA();

        string s = objSave.SaveDeleteData(_qurey, p1, p2, p3, p4, p5);
        string s1 = objSave.SaveDeleteData(_query2, p1, p2, p3, p4, par6, PIPNEw);
        Session.Abandon();
        Session.RemoveAll();
        Session.Clear();
    }

</script>
