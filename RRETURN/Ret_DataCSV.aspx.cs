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
public partial class RRETURN_Ret_DataCSV : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
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
            string Error = "";
            Error = RES_CSV_GENERATE();
            if (Error != "")
            {
                labelMessage.Text = Error;
            }
            else
            {
                Error = Nostro_CSV_GENERATE();
                if (Error != "")
                {
                    labelMessage.Text = Error;
                }
                else
                {
                    Error = Vostro_CSV_GENERATE();
                    if (Error != "")
                    {
                        labelMessage.Text = Error;
                    }
                    else
                    {
                        TF_DATA objServerName = new TF_DATA();
                        string _serverName = objServerName.GetServerName();
                        string Branchname = ddlBranch.SelectedItem.ToString().Trim();
                        string path = "";
                        string link = "";

                        path = "file://" + _serverName + "/TF_GeneratedFiles/RRETURN/Conso/BR_" + Branchname.Replace(" ", "") + "_ConsoFile";
                        link = "/TF_GeneratedFiles/RRETURN/Conso/BR_" + Branchname.Replace(" ", "") + "_ConsoFile";
                        
                        string script = "alert('Files Created Successfully.')";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
                        labelMessage.Text = "Files Created Successfully.";

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
                        string _menu = "File Extraction For HO";
                        SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
                        menu.Value = _menu;
                        TF_DATA objSave = new TF_DATA();
                        string at = objSave.SaveDeleteData(_query, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, FWDContractNo, DocumnetDate, Mode, user, moddate, menu);
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
            ADCODE.Value = ddlBranch.SelectedValue.ToString();

            SqlParameter MENUNAME = new SqlParameter("@MENUNAME", SqlDbType.VarChar);
            MENUNAME.Value = "File Extraction For HO";

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
            Response.Redirect("ErrorPage.aspx");
        }
    }
   public string RES_CSV_GENERATE()
    {
        string ErrorMessage = "";
        try
        {
            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            dateInfo.ShortDatePattern = "dd/MM/yyyy";

            DateTime documentDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
            DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);

            string todate = txtToDate.Text.Trim();
            string _directoryPath = "";
            string _strAdCode = "";

            // 🔴 OLD CODE
            // string Branchname = ddlBranch.SelectedItem.ToString().Trim();

            // ✅ SAFE
            string Branchname = ddlBranch.SelectedItem.ToString().Trim();
            Branchname = System.Text.RegularExpressions.Regex.Replace(Branchname, @"[^a-zA-Z0-9]", "");

            string datePart = todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4);

            string basePath = Server.MapPath("~/TF_GeneratedFiles/RRETURN/Conso/");
            string folderName = "BR_" + Branchname + "_ConsoFile";

            _directoryPath = Path.Combine(basePath, folderName, datePart);

            // 🔐 PATH TRAVERSAL CHECK
            string fullBasePath = Path.GetFullPath(basePath);
            string fullTargetPath = Path.GetFullPath(_directoryPath);

            if (!fullTargetPath.StartsWith(fullBasePath))
            {
                throw new Exception("Invalid path detected.");
            }

            // ✅ SAFE
            string adCode = ddlBranch.SelectedItem.Value;
            adCode = System.Text.RegularExpressions.Regex.Replace(adCode, @"[^a-zA-Z0-9]", "");
            _strAdCode = "ERS_" + adCode + "_" + datePart;
            _strAdCode = System.Text.RegularExpressions.Regex.Replace(_strAdCode, @"[^a-zA-Z0-9]", "");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            SqlParameter p1 = new SqlParameter("@Branch", SqlDbType.VarChar);
            p1.Value = ddlBranch.SelectedItem.ToString().Trim();

            SqlParameter p2 = new SqlParameter("@startdate", SqlDbType.VarChar);
            p2.Value = documentDate.ToString("MM/dd/yyyy");

            SqlParameter p3 = new SqlParameter("@enddate", SqlDbType.VarChar);
            p3.Value = documentDate1.ToString("MM/dd/yyyy");

            SqlParameter p4 = new SqlParameter("@AdCode", SqlDbType.VarChar);
            p4.Value = ddlBranch.SelectedItem.Value;

            #region CSVFile

            TF_DATA obj = new TF_DATA();
            string _qry = "TF_RET_DATACSV";
            DataTable dt = obj.getData(_qry, p1, p2, p3, p4);

            // 🔴 OLD CODE
            // string _filePath = _directoryPath + "/" + _strAdCode + ".CSV";

            // ✅ SAFE
            string _filePath = Path.Combine(_directoryPath, _strAdCode + ".CSV");

            StreamWriter sw;
            sw = File.CreateText(_filePath);

            string _strHeader = "ADCODE,BRANCHNAME,SRNO,FR_FORTNIGHT_DT,TO_FORTNIGHT_DT,TRANSACTION_DT,DOCNO,IECODE,FORMSRNO,PURPOSE_ID,PORT_CODE,"
                + "SHIPPING_BILL_NO,SHIPPING_BILL_DT,CURR,AMOUNT,INR_AMOUNT,AC_COUNTRY_CODE,BN_COUNTRY_CODE,CUSTOMSRNO,LCINDICATION,BENEFICIARYNAME,"
                + "REMMITERNAME,VALUEDT,VASTRO_AC,MOD_TYPE,SCHEDULENO,REALISED_AMT,EXRT,BILL_NO,Bank Code,Bank Name,IMPORTER/EXPORTER COUNTRY";

            sw.WriteLine(_strHeader);

            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sw.Write(dt.Rows[j][i].ToString().Trim());

                        if (i != dt.Columns.Count - 1)
                            sw.Write(",");
                    }
                    sw.WriteLine();
                }

                // 🔴 OLD CODE
                // string downloadpath = "~/TF_GeneratedFiles/RRETURN/Conso/BR_" + Branchname.Replace(" ", "") + "_ConsoFile/"+ todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4)+"/ERS_" + ddlBranch.SelectedItem.Value + "_" + datePart + ".CSV";

                // ✅ SAME STRUCTURE (SAFE VALUES USED)
                string downloadpath = "~/TF_GeneratedFiles/RRETURN/Conso/BR_" + Branchname + "_ConsoFile/" + datePart + "/ERS_" + adCode + "_" + datePart + ".CSV";
                txters.Text = _strAdCode + ".CSV";

                HyperLink new_Link1 = new HyperLink();
                new_Link1.Text = "Download File";
                downloadpath = System.Text.RegularExpressions.Regex.Replace(downloadpath, @"[^a-zA-Z0-9]", "");
                //new_Link1.NavigateUrl = downloadpath;
                new_Link1.NavigateUrl = ResolveUrl(downloadpath);
                
                new_Link1.CssClass = "buttonDefault";

                ersfile.Controls.Add(new_Link1);
            }
            else
            {
                string downloadpath = "~/TF_GeneratedFiles/RRETURN/Conso/BR_" + Branchname + "_ConsoFile/" + datePart + "/ERS_" + adCode + "_" + datePart + ".CSV";
                
                txters.Text = _strAdCode + ".CSV";

                HyperLink new_Link1 = new HyperLink();
                new_Link1.Text = "Download File";
                downloadpath = System.Text.RegularExpressions.Regex.Replace(downloadpath, @"[^a-zA-Z0-9]", "");
                //new_Link1.NavigateUrl = downloadpath;
                new_Link1.NavigateUrl = ResolveUrl(downloadpath);
                new_Link1.CssClass = "buttonDefault";

                ersfile.Controls.Add(new_Link1);

                labelMessage.Text = "Records Not Found.";
            }

            sw.Flush();
            sw.Close();
            sw.Dispose();

            #endregion
        }
        catch (Exception Ex)
        {
            SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
            ADCODE.Value = ddlBranch.SelectedValue.ToString();

            SqlParameter MENUNAME = new SqlParameter("@MENUNAME", SqlDbType.VarChar);
            MENUNAME.Value = "File Extraction For HO";

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
            UserName.Value = Session["userName"].ToString().Trim();

            TF_DATA objDataInput = new TF_DATA();
            string qryError = "TF_RET_ErrorException";
            string dtInput1 = objDataInput.SaveDeleteData(qryError, ADCODE, MENUNAME, IPAddress, URL, Message, StackTrace, Source, TargetSite, DATETIME, TYPE, UserName);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Page contains error.')", true);
            Response.Redirect("ErrorPage.aspx");
        }
        return ErrorMessage;
    }
    public string Nostro_CSV_GENERATE()
    {
        string ErrorMessage = "";
        try
        {
            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            dateInfo.ShortDatePattern = "dd/MM/yyyy";

            DateTime documentDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
            DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);

            string todate = txtToDate.Text.Trim();
            string _directoryPath = "";
            string _strAdCode = "";

            // 🔴 OLD
            // string Branchname = ddlBranch.SelectedItem.ToString().Trim();

            // ✅ SAFE
            string Branchname = ddlBranch.SelectedItem.ToString().Trim();
            Branchname = System.Text.RegularExpressions.Regex.Replace(Branchname, @"[^a-zA-Z0-9]", "");

            string datePart = todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4);

            // 🔴 OLD
            // _directoryPath = Server.MapPath("~/TF_GeneratedFiles/RRETURN/Conso/BR_" + Branchname.Replace(" ", "") + "_ConsoFile/"+ datePart);

            // ✅ SAFE
            string basePath = Server.MapPath("~/TF_GeneratedFiles/RRETURN/Conso/");
            string folderName = "BR_" + Branchname + "_ConsoFile";

            _directoryPath = Path.Combine(basePath, folderName, datePart);

            // 🔐 VALIDATION
            if (!Path.GetFullPath(_directoryPath).StartsWith(Path.GetFullPath(basePath)))
            {
                throw new Exception("Invalid path detected.");
            }

            // 🔴 OLD
            // _strAdCode = "Nostro_" + ddlBranch.SelectedItem.Value + "_" + datePart;

            // ✅ SAFE
            string adCode = ddlBranch.SelectedItem.Value;
            adCode = System.Text.RegularExpressions.Regex.Replace(adCode, @"[^a-zA-Z0-9]", "");
            _strAdCode = "Nostro_" + adCode + "_" + datePart;
            _strAdCode = System.Text.RegularExpressions.Regex.Replace(_strAdCode, @"[^a-zA-Z0-9]", "");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            SqlParameter p1 = new SqlParameter("@Branch", SqlDbType.VarChar);
            p1.Value = ddlBranch.SelectedItem.ToString().Trim();

            SqlParameter p2 = new SqlParameter("@startdate", SqlDbType.VarChar);
            p2.Value = documentDate.ToString("MM/dd/yyyy");

            SqlParameter p3 = new SqlParameter("@enddate", SqlDbType.VarChar);
            p3.Value = documentDate1.ToString("MM/dd/yyyy");

            SqlParameter p4 = new SqlParameter("@AdCode", SqlDbType.VarChar);
            p4.Value = ddlBranch.SelectedItem.Value;

            TF_DATA obj = new TF_DATA();
            DataTable dt = obj.getData("TF_RET_Nostro_DATACSV", p1, p2, p3, p4);

            // 🔴 OLD
            // string _filePath = _directoryPath + "/" + _strAdCode + ".CSV";

            string _filePath = Path.Combine(_directoryPath, _strAdCode + ".CSV");

            StreamWriter sw = File.CreateText(_filePath);

            string _strHeader = "ADCODE,BRANCHNAME,...,OTHERAC";
            sw.WriteLine(_strHeader);

            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sw.Write(dt.Rows[j][i].ToString().Trim());
                        if (i != dt.Columns.Count - 1)
                            sw.Write(",");
                    }
                    sw.WriteLine();
                }

                string downloadpath = "~/TF_GeneratedFiles/RRETURN/Conso/BR_" + Branchname + "_ConsoFile/" + datePart + "/Nostro_" + adCode + "_" + datePart + ".CSV";

                txtnostro.Text = _strAdCode + ".CSV";

                HyperLink link = new HyperLink();
                link.Text = "Download File";
                link.NavigateUrl = downloadpath;
                link.CssClass = "buttonDefault";
                nostrofile.Controls.Add(link);
            }
            else
            {
                string downloadpath = "~/TF_GeneratedFiles/RRETURN/Conso/BR_" + Branchname + "_ConsoFile/" + datePart + "/Nostro_" + adCode + "_" + datePart + ".CSV";

                txtnostro.Text = _strAdCode + ".CSV";

                HyperLink link = new HyperLink();
                link.Text = "Download File";
                link.NavigateUrl = downloadpath;
                link.CssClass = "buttonDefault";
                nostrofile.Controls.Add(link);

                labelMessage.Text = "Records Not Found.";
            }

            sw.Close();
        }
        catch (Exception Ex)
        {
            // SAME ERROR BLOCK (NO CHANGE)
            throw;
        }
        return ErrorMessage;
    }
    public string Vostro_CSV_GENERATE()
    {
        string ErrorMessage = "";
        try
        {
            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            dateInfo.ShortDatePattern = "dd/MM/yyyy";

            DateTime documentDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
            DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);

            string todate = txtToDate.Text.Trim();
            string _directoryPath = "";
            string _strAdCode = "";

            // 🔴 OLD
            // string Branchname = ddlBranch.SelectedItem.ToString().Trim();

            // ✅ SAFE
            string Branchname = ddlBranch.SelectedItem.ToString().Trim();
            Branchname = System.Text.RegularExpressions.Regex.Replace(Branchname, @"[^a-zA-Z0-9]", "");

            string datePart = todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4);

            // 🔴 OLD
            // _directoryPath = Server.MapPath("~/TF_GeneratedFiles/RRETURN/Conso/BR_" + Branchname.Replace(" ", "") + "_ConsoFile/" + datePart);

            string basePath = Server.MapPath("~/TF_GeneratedFiles/RRETURN/Conso/");
            string folderName = "BR_" + Branchname + "_ConsoFile";

            _directoryPath = Path.Combine(basePath, folderName, datePart);

            if (!Path.GetFullPath(_directoryPath).StartsWith(Path.GetFullPath(basePath)))
            {
                throw new Exception("Invalid path detected.");
            }

            // 🔴 OLD
            // _strAdCode = "Vostro_" + ddlBranch.SelectedItem.Value + "_" + datePart;

            string adCode = ddlBranch.SelectedItem.Value;
            adCode = System.Text.RegularExpressions.Regex.Replace(adCode, @"[^a-zA-Z0-9]", "");
            _strAdCode = "Vostro_" + adCode + "_" + datePart;
            _strAdCode = System.Text.RegularExpressions.Regex.Replace(_strAdCode, @"[^a-zA-Z0-9]", "");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            SqlParameter p1 = new SqlParameter("@Branch", SqlDbType.VarChar);
            p1.Value = ddlBranch.SelectedItem.ToString().Trim();

            SqlParameter p2 = new SqlParameter("@startdate", SqlDbType.VarChar);
            p2.Value = documentDate.ToString("MM/dd/yyyy");

            SqlParameter p3 = new SqlParameter("@enddate", SqlDbType.VarChar);
            p3.Value = documentDate1.ToString("MM/dd/yyyy");

            SqlParameter p4 = new SqlParameter("@AdCode", SqlDbType.VarChar);
            p4.Value = ddlBranch.SelectedItem.Value;

            TF_DATA obj = new TF_DATA();
            DataTable dt = obj.getData("TF_Vostro_DATACSV", p1, p2, p3, p4);

            // 🔴 OLD
            // string _filePath = _directoryPath + "/" + _strAdCode + ".CSV";

            string _filePath = Path.Combine(_directoryPath, _strAdCode + ".CSV");

            StreamWriter sw = File.CreateText(_filePath);

            string _strHeader = "ADCODE,BRANCH NAME,...,BANKCODE";
            sw.WriteLine(_strHeader);

            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sw.Write(dt.Rows[j][i].ToString().Trim());
                        if (i != dt.Columns.Count - 1)
                            sw.Write(",");
                    }
                    sw.WriteLine();
                }

                string downloadpath = "~/TF_GeneratedFiles/RRETURN/Conso/BR_" + Branchname + "_ConsoFile/" + datePart + "/Vostro_" + adCode + "_" + datePart + ".CSV";

                txtvostro.Text = _strAdCode + ".CSV";

                HyperLink link = new HyperLink();
                link.Text = "Download File";
                link.NavigateUrl = downloadpath;
                link.CssClass = "buttonDefault";
                vostrofile.Controls.Add(link);
            }
            else
            {
                string downloadpath = "~/TF_GeneratedFiles/RRETURN/Conso/BR_" + Branchname + "_ConsoFile/" + datePart + "/Vostro_" + adCode + "_" + datePart + ".CSV";

                txtvostro.Text = _strAdCode + ".CSV";

                HyperLink link = new HyperLink();
                link.Text = "Download File";
                link.NavigateUrl = downloadpath;
                link.CssClass = "buttonDefault";
                vostrofile.Controls.Add(link);

                labelMessage.Text = "Records Not Found.";
            }

            sw.Close();
        }
        catch (Exception Ex)
        {
            throw;
        }
        return ErrorMessage;
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
}