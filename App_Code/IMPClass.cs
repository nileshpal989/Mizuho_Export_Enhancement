using System;
using System.IO;
using System.Configuration;

/// <summary>
/// Summary description for IMPClass
/// </summary>
public class IMPClass
{
	public IMPClass()
	{
	
	}
	public void CreateUserLog(string UserName,string Message)
	{
        string DirectoryPath = ConfigurationManager.AppSettings["UserLogPath"].ToString();
        string FileName = "IMP_Log_" + UserName + ".txt";
        if (!Directory.Exists(DirectoryPath))
        {
            Directory.CreateDirectory(DirectoryPath);
        }
        string FilePath = DirectoryPath + FileName;
        using (StreamWriter sw = new StreamWriter(FilePath, true))
        {
            sw.WriteLine(Message+" "+DateTime.Now);
            sw.Close();
        }
	}
    public static void CreateUserLogA(string UserName, string Message)
    {
        string DirectoryPath = ConfigurationManager.AppSettings["UserLogPath"].ToString();
        string FileName = "IMP_Log_" + UserName + ".txt";
        if (!Directory.Exists(DirectoryPath))
        {
            Directory.CreateDirectory(DirectoryPath);
        }
        string FilePath = DirectoryPath + FileName;
        using (StreamWriter sw = new StreamWriter(FilePath, true))
        {
            sw.WriteLine(Message + " " + DateTime.Now);
            sw.Close();
        }
    }
}