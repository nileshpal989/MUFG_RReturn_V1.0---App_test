using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
public partial class RRETURN_Ret_Selction : System.Web.UI.Page
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
            GetFromToDate();
            btncalendar_FromDate.Focus();
        }
        btncalendar_FromDate.Focus();
        txtFromDate.Attributes.Add("onblur", "return ValidDates();");
        btnSave.Attributes.Add("onclick", "return ValidDates();");
    }
    protected void signout_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TF_Log_Out.aspx?PageHeader=Logout", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Session["FrRelDt"] = txtFromDate.Text;
        Session["ToRelDt"] = txtToDate.Text;
        //Session["ModuleID"] = "RET";
        Session["ModuleID"] = "R-Return";
        Response.Redirect("~/RReturn/Ret_Main.aspx?PageHeader=RET Main", true);
    }
    protected void GetFromToDate()
    {
        int TodayDay = int.Parse(System.DateTime.Today.Date.ToString("dd"));
        DateTime Date = DateTime.Today.AddDays(-15);

        int PrevMonth = DateTime.Now.AddMonths(-1).Month;
        int DayInMonth = System.DateTime.DaysInMonth(System.DateTime.Now.Year, PrevMonth);
        if (TodayDay < 16)
        {
            txtFromDate.Text = Date.ToString("16/MM/yyyy");
            txtToDate.Text = Date.ToString(DayInMonth + "/MM/yyyy");
        }
        else
        {
            txtFromDate.Text = System.DateTime.Today.ToString("01/MM/yyyy");
            txtToDate.Text = System.DateTime.Today.ToString("15/MM/yyyy");
        }
    }
}