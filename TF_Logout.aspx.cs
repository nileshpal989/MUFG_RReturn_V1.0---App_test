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
public partial class TF_Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cookies["userName"].Expires = DateTime.Now.AddDays(-1);
        Response.Cookies["hgoribshxo1ia2jqyjmi54et"].Expires = DateTime.Now.AddDays(-1);
        Session.Abandon();
        //Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
        HttpCookie cookie = new HttpCookie("ASP.NET_SessionId", "");
        cookie.HttpOnly = true;   // JS se access block
        cookie.Secure = Request.IsSecureConnection;
        Response.Cookies.Add(cookie);
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_Login.aspx", true);
    }
}