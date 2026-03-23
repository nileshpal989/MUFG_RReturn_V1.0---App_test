using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;

public partial class VIEW_TF_AdminAuditTrail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PageHeader.Text = Request.QueryString["PageHeader"].ToString();
            if (Request.QueryString["frm"] != null && Request.QueryString["to"] != null)
            {
                try
                {
                    Encryption objEncryption = new Encryption();
                    string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;

                    ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                    IReportServerCredentials irsc = new CustomReportCredentials(objEncryption.decrypttext(WebConfigurationManager.ConnectionStrings["user"].ConnectionString), objEncryption.decrypttext(WebConfigurationManager.ConnectionStrings["password"].ConnectionString), objEncryption.decrypttext(WebConfigurationManager.ConnectionStrings["domain"].ConnectionString));
                    ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                    ServerReport serverReport = ReportViewer1.ServerReport;
                    serverReport.ReportServerUrl =
                      new Uri(url);

                    Microsoft.Reporting.WebForms.ReportParameter ModeType = new Microsoft.Reporting.WebForms.ReportParameter();
                    ModeType.Name = "ModeType";
                    Microsoft.Reporting.WebForms.ReportParameter PurCode = new Microsoft.Reporting.WebForms.ReportParameter();
                    PurCode.Name = "PurCode";
                    Microsoft.Reporting.WebForms.ReportParameter branch = new Microsoft.Reporting.WebForms.ReportParameter();
                    branch.Name = "Branch";
                    Microsoft.Reporting.WebForms.ReportParameter pur = new Microsoft.Reporting.WebForms.ReportParameter();
                    pur.Name = "pur";
                    string RPTTYPE = Request.QueryString["rptType"].ToString();

                    string RPTCODE = Request.QueryString["rptCode"].ToString();
                    if (RPTCODE == "5")
                    {
                        serverReport.ReportPath = "/MUFG_TF_Reports/AdminAccessAuditTrail";
                    }
                    else
                    {
                        serverReport.ReportPath = "/MUFG_TF_Reports/AdminAudittrail";
                    }
                    string frmdate = Request.QueryString["frm"].ToString();
                    //DateTime.ParseExact(Request.QueryString["frm"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                    string todate = Request.QueryString["to"].ToString();
                    DateTime.ParseExact(Request.QueryString["to"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");



                    Microsoft.Reporting.WebForms.ReportParameter startdate = new Microsoft.Reporting.WebForms.ReportParameter();
                    startdate.Name = "fromdate";
                    startdate.Values.Add(frmdate);

                    Microsoft.Reporting.WebForms.ReportParameter enddate = new Microsoft.Reporting.WebForms.ReportParameter();
                    enddate.Name = "todate";
                    enddate.Values.Add(todate);

                    Microsoft.Reporting.WebForms.ReportParameter rpt_type = new Microsoft.Reporting.WebForms.ReportParameter();
                    rpt_type.Name = "rpttype";
                    rpt_type.Values.Add(RPTTYPE);

                    Microsoft.Reporting.WebForms.ReportParameter rpt_code = new Microsoft.Reporting.WebForms.ReportParameter();
                    rpt_code.Name = "rptcode";
                    rpt_code.Values.Add(RPTCODE);


                    Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
                    user.Name = "user";
                    user.Values.Add(Session["userName"].ToString());

                    ReportViewer1.ServerReport.SetParameters(
                    new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, enddate, user, rpt_type, rpt_code });
                }
                catch (Exception Ex)
                {
                    SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
                    ADCODE.Value = Request.QueryString["branch"];

                    SqlParameter MENUNAME = new SqlParameter("@MENUNAME", SqlDbType.VarChar);
                    MENUNAME.Value = "Admin Audit Trail";

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
        Response.Redirect("TF_AdminAuditTrail.aspx?PageHeader=" + PageHeader.Text, true);
    }
}