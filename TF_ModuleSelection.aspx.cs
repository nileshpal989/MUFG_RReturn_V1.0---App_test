using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TF_ModuleSelection : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime nowDate = System.DateTime.Now;
            if (Session["LoggedUserId"] != null)
            {
                //lblUserName.Text = "Welcome, " + Session["userName"].ToString().Trim();
                //lblRole.Text = "| Role: " + Session["userRole"].ToString().Trim();
                lblUserName.Text = "Welcome, " + Server.HtmlEncode(Session["userName"].ToString());
                lblRole.Text = "| Role: " + Server.HtmlEncode(Session["userRole"].ToString());
                lblTime.Text = nowDate.ToLongDateString();

            }
        }

        switch (Request.QueryString["type"].ToString())
        {

            
        }

    }


    
    protected void signout_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TF_Log_Out.aspx?PageHeader=Logout", true);
    }    
    protected void btnRreturn_Click(object sender, EventArgs e)
    {        
        Session["ModuleID"] = "RET";
        Response.Redirect("~/RRETURN/Ret_Selction.aspx?PageHeader=Module Selection", true);
    }
}