using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for TF_EDPMS_Data
/// </summary>
public class TF_EDPMS_Data
{
	public TF_EDPMS_Data()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string SaveDeleteData(string query, params SqlParameter[] queryParameters)
    {
        string _result = "";
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString);
        SqlCommand com = new SqlCommand(query, con);
        try
        {
            con.Open();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandTimeout = 0;
            foreach (SqlParameter param in queryParameters)
                com.Parameters.Add(param);
            _result = com.ExecuteScalar().ToString();
        }
        catch (Exception ex)
        {
            _result = ex.Message.Trim();
        }
        finally
        {
            com.Parameters.Clear();
            com.Dispose();
            con.Close();
            con.Dispose();
        }
        return _result;
    }

}