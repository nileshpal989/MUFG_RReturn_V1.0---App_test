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
            saveLog();
            Session.Abandon();
            Session.RemoveAll();
            Session.Clear();
        }
    }
    public void saveLog()
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
        string _qurey = "TF_AddLoginLogoutLog";
        TF_DATA objSave = new TF_DATA();
        string s = objSave.SaveDeleteData(_qurey, p1, p2, p3, p4, p5);
    }
}
