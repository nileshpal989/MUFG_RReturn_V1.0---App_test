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
using System.Net;
using System.Data.OleDb;
using System.Data.SqlClient;
public partial class Reports_RRETURNReports_ViewrptRRETURN_Purpose_Control_Totals : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["frm"] != null && Request.QueryString["to"] != null)
            {
                //PageHeader.Text = Request.QueryString["PageHeader"].ToString();
                string header = Request.QueryString["PageHeader"];
                if (!string.IsNullOrEmpty(header))
                {
                    PageHeader.Text = Server.HtmlEncode(header);
                }
                try
                {
                    Encryption objEncryption = new Encryption();
                    string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
                    //ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                    ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                    IReportServerCredentials irsc = new CustomReportCredentials(objEncryption.decrypttext(WebConfigurationManager.ConnectionStrings["user"].ConnectionString), objEncryption.decrypttext(WebConfigurationManager.ConnectionStrings["password"].ConnectionString), objEncryption.decrypttext(WebConfigurationManager.ConnectionStrings["domain"].ConnectionString));
                    ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                    ServerReport serverReport = ReportViewer1.ServerReport;
                    serverReport.ReportServerUrl = new Uri(url);
                    switch (Request.QueryString["Report"])
                    {
                        case "Purpose Code Wise Control Totals":
                            serverReport.ReportPath = "/MUFG_TF_Reports/rptRRETURN_PURPOSE_CONTROLS_TOTAL";
                            break;
                        case "Data Statistics":
                            serverReport.ReportPath = "/MUFG_TF_Reports/rptRRETURN_Data_Statistics";
                            break;
                        case "Data Validation":
                            serverReport.ReportPath = "/MUFG_TF_Reports/rptRRETURN_Data_Validation";
                            break;
                        case "R RETURN Cover Page Total":
                            serverReport.ReportPath = "/MUFG_TF_Reports/rptRRETURN_CoverPageTotal";
                            break;
                    }
                    string frmdate = DateTime.ParseExact(Request.QueryString["frm"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                    string todate = DateTime.ParseExact(Request.QueryString["to"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");

                    //string frmdate = Request.QueryString["frm"].ToString();
                    //string todate = Request.QueryString["to"].ToString();

                    Microsoft.Reporting.WebForms.ReportParameter startdate = new Microsoft.Reporting.WebForms.ReportParameter();
                    startdate.Name = "startdate";
                    startdate.Values.Add(frmdate);
                    Microsoft.Reporting.WebForms.ReportParameter enddate = new Microsoft.Reporting.WebForms.ReportParameter();
                    enddate.Name = "enddate";
                    enddate.Values.Add(todate);
                    Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
                    user.Name = "user";
                    user.Values.Add(Session["userName"].ToString());
                    Microsoft.Reporting.WebForms.ReportParameter branch = new Microsoft.Reporting.WebForms.ReportParameter();
                    branch.Name = "Branch";
                    branch.Values.Add(Request.QueryString["branch"]);
                    ReportViewer1.ServerReport.SetParameters( new ReportParameter[] { startdate, enddate, branch, user });
                }
                catch (Exception Ex)
                {
                    SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
                    ADCODE.Value = Request.QueryString["branch"];

                    SqlParameter MENUNAME = new SqlParameter("@MENUNAME", SqlDbType.VarChar);
                    MENUNAME.Value = Request.QueryString["Report"];

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
        Response.Redirect("rptRRETURN_Purpose_Control_Totals.aspx?PageHeader=" + PageHeader.Text, true);
    }
}