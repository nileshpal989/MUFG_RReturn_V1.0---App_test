using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Net;
using System.Threading;

public partial class RRETURN_TF_RET_ErrorLog_TxtFileCreation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
            Response.Redirect("~/TF_Login.aspx?PageHeader=Login&sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
            if (Session["userRole"].ToString() != "Admin")
            {
                System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
                Response.Redirect("TF_Login.aspx?PageHeader=Login&sessionout=yes&sessionid=" + lbl.Value, true);
            }
            else
            {
                if (!IsPostBack)
                {
                    fillBranch();
                    ddlBranch.SelectedValue = Session["userADCode"].ToString();
                    ddlBranch.Enabled = false;
                    txtFromDate.Text = Session["FrRelDt"].ToString().Trim();
                    txtToDate.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
                    btnCreate.Attributes.Add("onclick", "return validateControl();");
                }
                //txtFromDate.Attributes.Add("onblur", "return validateControl();");
                btnCreate.Attributes.Add("onclick", "return validateControl();");
                txtFromDate.Focus();
            }
    }
    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_RET_GetBranchandADcodeList";
        DataTable dt = objData.getData(_query);
        ddlBranch.Items.Clear();
        if (dt.Rows.Count > 0)
        {
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            dateInfo.ShortDatePattern = "dd/MM/yyyy";
            DateTime documentDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
            DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);
            TF_DATA DataVal = new TF_DATA();

            string _directoryPath = "";
            string _strAdCode = "";
            string Branchname = ddlBranch.SelectedItem.ToString().Trim();
            _strAdCode = ddlBranch.Text.ToString().Trim().Substring(0, 3);

            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/RRETURN/ERROR");


            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            SqlParameter p1 = new SqlParameter("@startdate", SqlDbType.VarChar);
            p1.Value = documentDate.ToString("yyyy/MM/dd");
            SqlParameter p2 = new SqlParameter("@enddate", SqlDbType.VarChar);
            p2.Value = documentDate1.ToString("yyyy/MM/dd");

            TF_DATA objQE = new TF_DATA();
            string _qryQE = "TF_RET_ErrorLog_TxtFileCreation";
            DataTable dtQE = objQE.getData(_qryQE, p1, p2);
            string _filePath = _directoryPath + "/ErrorLog" + documentDate.ToString("dd-MM-yyyy") + "To" + documentDate1.ToString("dd-MM-yyyy") + ".TXT";
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            ViewState["_directoryPath"] = ("~/TF_GeneratedFiles/RRETURN/ERROR");
            ViewState["FileName"] = "/ErrorLog" + documentDate.ToString("dd-MM-yyyy") + "To" + documentDate1.ToString("dd-MM-yyyy") + ".TXT";
            if (dtQE.Rows.Count > 0)
            {
                for (int j = 0; j < dtQE.Rows.Count; j++)
                {
                    sw.WriteLine("Sr No.: " + (j + 1));
                    sw.WriteLine("User: " + dtQE.Rows[j]["USERNAME"].ToString().Trim());
                    sw.WriteLine("Date: " + dtQE.Rows[j]["DATETIME"].ToString().Trim());
                    sw.WriteLine("Branch Name: " + dtQE.Rows[j]["BranchName"].ToString().Trim());
                    sw.WriteLine("IP Address: " + dtQE.Rows[j]["IPADDRESS"].ToString().Trim());
                    sw.WriteLine("Page URL: " + dtQE.Rows[j]["PAGEURL"].ToString().Trim());
                    sw.WriteLine("Exception Type: " + dtQE.Rows[j]["TYPE"].ToString().Trim());
                    sw.WriteLine("Message: " + dtQE.Rows[j]["Message"].ToString().Trim());
                    sw.WriteLine("StackTrace: " + dtQE.Rows[j]["STACKTRACE"].ToString().Trim());
                    sw.WriteLine("Source: " + dtQE.Rows[j]["SOURCE"].ToString().Trim());
                    sw.WriteLine("Target Site: " + dtQE.Rows[j]["TARGETSITE"].ToString().Trim());
                    sw.WriteLine("===================================================================");
                    sw.WriteLine("");
                }
            }
            else
            {
                sw.WriteLine("No Error Record");
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();

            TF_DATA objServerName = new TF_DATA();
            string _serverName = objServerName.GetServerName();
            string path = "";
            string link = "";

            path = "file://" + _serverName + "/TF_GeneratedFiles/RRETURN/ERROR";
            link = "/TF_GeneratedFiles/RRETURN/ERROR";

            //string script = "alert('Files Created Successfully on " + _serverName + " in " + link + " ')";
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
            labelMessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyJQueryFunction", "setTimeout(function() { alert('Report Created Successfully..');ClickAnotherButton();  setTimeout(ClickAnotherButton, 10); }, 10);", true);

        }
        catch (Exception Ex)
        {
            SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
            ADCODE.Value = ddlBranch.SelectedValue.ToString();

            SqlParameter MENUNAME = new SqlParameter("@MENUNAME", SqlDbType.VarChar);
            MENUNAME.Value = "Error Log File Creation";

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
            Response.Redirect("ErrorPage.aspx?PageHeader=Error Page");
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
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {
            Thread.Sleep(100);
            string _directoryPath = ViewState["_directoryPath"].ToString();
            string FileName = ViewState["FileName"].ToString();
            //string filePath = ViewState["filePath"].ToString();

            Response.ContentType = "image/jpg";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
            Response.TransmitFile(Server.MapPath(_directoryPath + "/" + FileName));
            Response.End();

        }
        catch (Exception ex)
        {
            ViewState["Method"] = "lnk download Click";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "Alert('" + ex.Message + "');", true);
        }

    }
}