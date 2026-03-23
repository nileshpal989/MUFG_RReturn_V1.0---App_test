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

public partial class Reports_View_rptAuditTrail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PageHeader.Text = Request.QueryString["PageHeader"].ToString();
            try
            {
                Encryption objEncryption = new Encryption();
                if (Request.QueryString["frm"] != null && Request.QueryString["to"] != null)
                {
                    string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
                    ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                    IReportServerCredentials irsc = new CustomReportCredentials(objEncryption.decrypttext(WebConfigurationManager.ConnectionStrings["user"].ConnectionString), objEncryption.decrypttext(WebConfigurationManager.ConnectionStrings["password"].ConnectionString), objEncryption.decrypttext(WebConfigurationManager.ConnectionStrings["domain"].ConnectionString));
                    ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                    ServerReport serverReport = ReportViewer1.ServerReport;
                    serverReport.ReportServerUrl =
                      new Uri(url);
                    // Set the report server URL and report path
                    Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
                    Branch.Name = "Branch";
                    Microsoft.Reporting.WebForms.ReportParameter Type = new Microsoft.Reporting.WebForms.ReportParameter();
                    Type.Name = "ModeType";

                    switch (Request.QueryString["Report"])
                    {
                        case "Audit Trail - Transactions":
                            serverReport.ReportPath = "/MUFG_TF_Reports/TF_rptAuditTrail";
                            break;
                        case "Audit Trail - Login Details":
                            serverReport.ReportPath = "/MUFG_TF_Reports/rptRRETURN_Login_AuditTrail";
                            break;
                    }
                    string frmdate = DateTime.ParseExact(Request.QueryString["frm"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                    string todate = DateTime.ParseExact(Request.QueryString["to"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");

                    Microsoft.Reporting.WebForms.ReportParameter startdate = new Microsoft.Reporting.WebForms.ReportParameter();
                    startdate.Name = "startdate";
                    startdate.Values.Add(frmdate);

                    Microsoft.Reporting.WebForms.ReportParameter enddate = new Microsoft.Reporting.WebForms.ReportParameter();
                    enddate.Name = "enddate";
                    enddate.Values.Add(todate);

                    Microsoft.Reporting.WebForms.ReportParameter Module = new Microsoft.Reporting.WebForms.ReportParameter();
                    Module.Name = "ModuleType";

                    string _Module = Request.QueryString["Module"];
                    Module.Values.Add(_Module);

                    string Branch1 = Request.QueryString["Branch"];
                    Branch.Values.Add(Branch1);

                    Microsoft.Reporting.WebForms.ReportParameter User = new Microsoft.Reporting.WebForms.ReportParameter();
                    User.Name = "User";

                    Microsoft.Reporting.WebForms.ReportParameter UserName = new Microsoft.Reporting.WebForms.ReportParameter();
                    UserName.Name = "UserName";
                    //User.Values.Add(Session["userName"].ToString());

                    string Type1 = Request.QueryString["Type"];
                    Type.Values.Add(Type1);

                    string User1 = Session["userName"].ToString();
                    User.Values.Add(User1);

                    string UserName1 = Request.QueryString["UserName"];
                    UserName.Values.Add(UserName1);

                    // ModeType.Values.Add(ModeTypevalue);

                    switch (Request.QueryString["Report"])
                    {

                        case "Audit Trail - Transactions":
                            ReportViewer1.ServerReport.SetParameters(
                        new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, enddate, Branch, User, Type, Module, UserName });
                            break;

                        case "Audit Trail - Login Details":
                            ReportViewer1.ServerReport.SetParameters(
                        new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, enddate, User, UserName });
                            break;
                    }
                }
            }
            catch (Exception Ex)
            {
                SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
                ADCODE.Value = "";

                SqlParameter MENUNAME = new SqlParameter("@MENUNAME", SqlDbType.VarChar);
                MENUNAME.Value = "Audit Trail - Transactions";

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
        switch (Request.QueryString["Report"])
        {
            case "Audit Trail - Transactions":
                Response.Redirect("TF_AuditTrail.aspx?PageHeader=" + PageHeader.Text, true);
                break;
            case "Audit Trail - Login Details":
                Response.Redirect("../TF_AuditTrail_LoginLogOut.aspx?PageHeader=" + PageHeader.Text, true);
                break;

        }


    }
}