using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using System.Net;

public partial class VIEW_TF_rpt_LoginLog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["frm"] != null && Request.QueryString["to"] != null)
            {
                PageHeader.Text = Request.QueryString["PageHeader"].ToString();
                try
                {
                    Encryption objEncryption = new Encryption();
                    TF_DATA objData = new TF_DATA();
                    SqlParameter p1 = new SqlParameter("@frmdate", SqlDbType.Date);
                    p1.Value = Request.QueryString["frm"];
                    SqlParameter p2 = new SqlParameter("@todate", SqlDbType.Date);
                    p2.Value = Request.QueryString["to"];
                    SqlParameter p3 = new SqlParameter("@user", SqlDbType.VarChar);
                    p3.Value = Request.QueryString["User"];
                    string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
                    ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                    IReportServerCredentials irsc = new CustomReportCredentials(objEncryption.decrypttext(WebConfigurationManager.ConnectionStrings["user"].ConnectionString), objEncryption.decrypttext(WebConfigurationManager.ConnectionStrings["password"].ConnectionString), objEncryption.decrypttext(WebConfigurationManager.ConnectionStrings["domain"].ConnectionString));
                    ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                    ServerReport serverReport = ReportViewer1.ServerReport;
                    serverReport.ReportServerUrl = new Uri(url);
                    // Set the report server URL and report path
                    Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
                    Branch.Name = "Branch";
                    serverReport.ReportPath = "/MUFG_TF_Reports/rptUserLoginActivity";
                    //string fromdate = DateTime.ParseExact(Request.QueryString["frm"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                    //string todate1 = DateTime.ParseExact(Request.QueryString["to"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");

                    string fromdate = Request.QueryString["frm"].ToString();
                    string todate1 = Request.QueryString["to"].ToString();
                    Microsoft.Reporting.WebForms.ReportParameter frmdate = new Microsoft.Reporting.WebForms.ReportParameter();
                    frmdate.Name = "frmdate";
                    frmdate.Values.Add(fromdate);
                    Microsoft.Reporting.WebForms.ReportParameter todate = new Microsoft.Reporting.WebForms.ReportParameter();
                    todate.Name = "ToDate";
                    todate.Values.Add(todate1);
                    string Branch1 = Request.QueryString["Branch"];
                    Branch.Values.Add(Branch1);
                    Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
                    user.Name = "User";
                    //User.Values.Add(Session["userName"].ToString());
                    string User1 = Request.QueryString["User"];
                    user.Values.Add(User1);



                    Microsoft.Reporting.WebForms.ReportParameter Loginuser = new Microsoft.Reporting.WebForms.ReportParameter();
                    Loginuser.Name = "LoginUser";
                    Loginuser.Values.Add(Session["userName"].ToString());
                    // ModeType.Values.Add(ModeTypevalue);
                    ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { frmdate, todate, user, Loginuser });
                }
                catch (Exception Ex)
                {
                    SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
                    ADCODE.Value = "";

                    SqlParameter MENUNAME = new SqlParameter("@MENUNAME", SqlDbType.VarChar);
                    MENUNAME.Value = "User Login Activity Details";

                    SqlParameter IPAddress = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    IPAddress.Value = GetIPAddress();

                    SqlParameter URL = new SqlParameter("@URL", SqlDbType.VarChar);
                    URL.Value = HttpContext.Current.Request.Url.AbsoluteUri;

                    SqlParameter TYPE = new SqlParameter("@TYPE", SqlDbType.VarChar);
                    TYPE.Value = Ex.GetType().Name.ToString();

                    SqlParameter Message = new SqlParameter("@Message", SqlDbType.VarChar);
                    Message.Value = Ex.Message;

                    SqlParameter StackTrace = new SqlParameter("@StackTrace", SqlDbType.VarChar);
                    StackTrace.Value = Ex.StackTrace;

                    SqlParameter Source = new SqlParameter("@Source", SqlDbType.VarChar);
                    Source.Value = Ex.Source;

                    SqlParameter TargetSite = new SqlParameter("@TargetSite", SqlDbType.VarChar);
                    TargetSite.Value = Ex.TargetSite.ToString();

                    SqlParameter DATETIME = new SqlParameter("@DATETIME", SqlDbType.VarChar);
                    DATETIME.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                    SqlParameter UserName = new SqlParameter("@UserName", SqlDbType.VarChar);
                    UserName.Value = Session["userName"].ToString().Trim(); ;

                    TF_DATA objDataInput = new TF_DATA();
                    string qryError = "TF_RET_ErrorException";
                    string dtInput1 = objDataInput.SaveDeleteData(qryError, ADCODE, MENUNAME, IPAddress, URL, Message, StackTrace, Source, TargetSite, DATETIME, TYPE, UserName);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Page contains error.')", true);
                    Response.Redirect("../../RRETURN/ErrorPage.aspx?PageHeader=Error Page");
                }
            }
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_rpt_LoginLog.aspx?PageHeader=" + PageHeader.Text, true);
    }
}