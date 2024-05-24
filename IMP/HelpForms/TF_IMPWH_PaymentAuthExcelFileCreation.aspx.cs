using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ImportWareHousing_FileCreation_TF_IMPWH_PaymentAuthExcelFileCreation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string FileName = Request.QueryString["FileName"];
        string filePath = "~/TF_GeneratedFiles/IMPORT/AML/" + FileName;
        Response.ContentType = "image/jpg";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
        Response.TransmitFile(Server.MapPath(filePath));
        Response.End();
        //ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
    }
}