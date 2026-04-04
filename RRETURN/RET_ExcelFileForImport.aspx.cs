using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using ClosedXML.Excel;
using System.Net;

public partial class RRETURN_RET_ExcelFileForImport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
            Response.Redirect("~/TF_Login.aspx?PageHeader=Login&sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                //ddlBranch.Enabled = false;
                txtFromDate.Text = Session["FrRelDt"].ToString().Trim();
                txtToDate.Text = Session["ToRelDt"].ToString().Trim();
                btnCreate.Attributes.Add("onclick", "return validateControl();");
            }
            txtFromDate.Attributes.Add("onblur", "return validateControl();");
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
        //ListItem li = new ListItem();
        //li.Value = "All Branches";
        if (dt.Rows.Count > 0)
        {
            //li.Text = "All Branches";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        //else
        //    li.Text = "No record(s) found";
        //ddlBranch.Items.Insert(0, li);
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtFromDate.Text == "")
            {
                return;
            }

            

            //string _fromdate = txtFromDate.Text.Substring(6, 4) + "" + txtFromDate.Text.Substring(3, 2) + "" + txtFromDate.Text.Substring(0, 2);
            //string _todate = txtToDate.Text.Substring(6, 4) + "" + txtToDate.Text.Substring(3, 2) + "" + txtToDate.Text.Substring(0, 2);
            string fromdate = txtFromDate.Text;
            string todate = txtToDate.Text;
            fromdate = System.Text.RegularExpressions.Regex.Replace(fromdate, @"[^0-9/]", "");
            DateTime parsedDate = DateTime.ParseExact(fromdate, "dd/MM/yyyy", null);
            string _fromdate = parsedDate.ToString("yyyyMMdd");
            todate = System.Text.RegularExpressions.Regex.Replace(todate, @"[^0-9/]", "");
            DateTime ToparsedDate = DateTime.ParseExact(todate, "dd/MM/yyyy", null);
            string _todate = ToparsedDate.ToString("yyyyMMdd");

            TF_DATA objData1 = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@AdCode", SqlDbType.VarChar);
            p1.Value = ddlBranch.SelectedItem.Value;
            SqlParameter p2 = new SqlParameter("@startdate", SqlDbType.VarChar);
            p2.Value = txtFromDate.Text;
            SqlParameter p3 = new SqlParameter("@enddate", SqlDbType.VarChar);
            p3.Value = txtToDate.Text;
            string Branchname = ddlBranch.SelectedItem.ToString().Trim();

            DataTable dt = objData1.getData("TF_RET_ExcelFileForImport",p1,p2, p3);
            if (dt.Rows.Count > 0)
            {
                string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/RRETURN/Import_Files/BR_" + Branchname.Replace(" ", "") + "_Invoice");
                if (!Directory.Exists(_directoryPath))
                {
                    Directory.CreateDirectory(_directoryPath);
                }
                string _filePath = _directoryPath + "/Invoice_Upload_From_" + _fromdate +"_To_"+_todate+ ".xlsx";


                if (dt.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, "Sheet1");
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);  
                            FileStream file = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
                            MyMemoryStream.WriteTo(file);
                            file.Close();
                            MyMemoryStream.Close();
                        }
                    }
                    lblqename.Text = "Invoice_Upload_From_" + _fromdate + "_To_" + _todate + ".xlsx";
                    lnkEDownload.Visible = true;


                    TF_DATA objserverName = new TF_DATA();
                    string _serverName = objserverName.GetServerName();
                    string path = "file://" + _serverName + "/TF_GeneratedFiles/RRETURN/Import_Files";
                    string link = "/TF_GeneratedFiles/RRETURN/Import_Files";
                    labelMessage.Text = "Files Created Successfully.";

                    ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('File Created Successfully.')", true);

                    //*************/**********/***********////// Audit Trail************************************************************//
                    string _query = "TF_RET_AuditTrail";
                    SqlParameter Branch = new SqlParameter("@BranchCode", SqlDbType.VarChar);
                    Branch.Value = ddlBranch.SelectedValue.ToString();
                    SqlParameter Mod = new SqlParameter("@ModType", SqlDbType.VarChar);
                    Mod.Value = "RET";
                    SqlParameter oldvalues = new SqlParameter("@OldValues", SqlDbType.VarChar);
                    oldvalues.Value = "";
                    SqlParameter newvalues = new SqlParameter("@NewValues", SqlDbType.VarChar);
                    newvalues.Value = "";
                    SqlParameter Acno = new SqlParameter("@CustAcNo", SqlDbType.VarChar);
                    Acno.Value = "";
                    SqlParameter DocumentNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
                    DocumentNo.Value = "";
                    SqlParameter FWDContractNo = new SqlParameter("@FWD_Contract_No", SqlDbType.VarChar);
                    FWDContractNo.Value = "";
                    SqlParameter DocumnetDate = new SqlParameter("@DocumentDate", SqlDbType.VarChar);
                    DocumnetDate.Value = "";
                    SqlParameter Mode = new SqlParameter("@Mode", "C");
                    SqlParameter user = new SqlParameter("@ModifiedBy", SqlDbType.VarChar);
                    user.Value = Session["userName"].ToString();
                    string _moddate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    SqlParameter moddate = new SqlParameter("@ModifiedDate", SqlDbType.VarChar);
                    moddate.Value = _moddate;
                    string _menu = "Excel File Creation For Import";
                    SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
                    menu.Value = _menu;
                    TF_DATA objSave = new TF_DATA();
                    string at = objSave.SaveDeleteData(_query, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, FWDContractNo, DocumnetDate, Mode, user, moddate, menu);
                }

            }
            else
            {
                lblqename.Text = "";
                lnkEDownload.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "Message", "alert('There is No records Between this Dates.')", true);                
            }
        }
        catch (Exception Ex)
        {
            SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
            ADCODE.Value = ddlBranch.SelectedValue.ToString();

            SqlParameter MENUNAME = new SqlParameter("@MENUNAME", SqlDbType.VarChar);
            MENUNAME.Value = "Excel File For Import";

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
    // Download
    protected void lnkEDownload_Click(object sender, EventArgs e)
    {
        string _fromdate = txtFromDate.Text.Substring(6, 4) + "" + txtFromDate.Text.Substring(3, 2) + "" + txtFromDate.Text.Substring(0, 2);
        string _todate = txtToDate.Text.Substring(6, 4) + "" + txtToDate.Text.Substring(3, 2) + "" + txtToDate.Text.Substring(0, 2);
        string todate = txtToDate.Text.Trim();
        string Branchname = ddlBranch.SelectedItem.ToString().Trim();
        string filePath = "~/TF_GeneratedFiles/RRETURN/Import_Files/BR_" + Branchname.Replace(" ", "") + "_Invoice/Invoice_Upload_From_" + _fromdate + "_To_" + _todate + ".xlsx";
        string FileName = "Invoice_Upload_From_" + _fromdate + "_To_" + _todate + ".xlsx";
        lblqename.Text = "Invoice_Upload_From_" + _fromdate + "_To_" + _todate + ".xlsx";
        Response.ContentType = "image/jpg";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
        Response.TransmitFile(Server.MapPath(filePath));
        Response.End();
    }
}