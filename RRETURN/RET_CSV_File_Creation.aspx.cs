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

public partial class RRETURN_RET_CSV_File_Creation : System.Web.UI.Page
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
        RES_CSV_GENERATE();
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
            string Branchname = ddlBranch.SelectedItem.ToString().Trim();

            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/RRETURN/DataCheck/BR_" + Branchname.Replace(" ", "") + "DataCheck");

            _strAdCode = "DataCheck_" + ddlBranch.SelectedItem.Value + "_" + todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4);
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
            //REM FOR CSV FILE
            TF_DATA obj = new TF_DATA();
            string _qry = "TF_RET_CSV_File_Generate";
            DataTable dt = obj.getData(_qry, p1, p2, p3, p4);
            string _filePath = _directoryPath + "/" + _strAdCode + ".CSV";
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            string _strHeader = "ADCODE,BRANCH NAME,SRNO,FR_FORTNIGHT_DT,TO_FORTNIGHT_DT,TRANSACTION_DT,DOCNO,IECODE,FORMSRNO,PURPOSE_ID,PORT_CODE,SHIPPING_BILL_NO,SHIPPING_BILL_DT,CURR,AMOUNT,INR_AMOUNT,AC_COUNTRY_CODE,BN_COUNTRY_CODE,CUSTOMSRNO,LCINDICATION,BENEFICIARYNAME,REMMITERNAME,REMITTERCOUNTRY,VALUEDT,VASTR0_AC,MOD_TYPE,SCHEDULENO,REALISED_AMT,EXRT,BILLNO,Bank Code,Bank Name,GL_Code,GL_Sub_Code,Booking_Office_Number,Transaction_Number,Transaction_Indication,CMF_Number,NV_Indicator,Settlement_Date";            
            sw.WriteLine(_strHeader);
            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {                    
                    sw.Write(dt.Rows[j]["ADCODE"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["BRANCHNAME"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["SRNO"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["FR_FORTNIGHT_DT"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["TO_FORTNIGHT_DT"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["TRANSACTION_DT"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["DOCNO"].ToString().Trim() + ",");                    
                    sw.Write(dt.Rows[j]["IECODE"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["FORMSRNO"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["PURPOSE_ID"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["PORT_CODE"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["SHIPPING_BILL_NO"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["SHIPPING_BILL_DT"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["CURR"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["AMOUNT"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["INR_AMOUNT"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["AC_COUNTRY_CODE"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["BN_COUNTRY_CODE"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["CUSTOMSRNO"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["LCINDICATION"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["BENEFICIARYNAME"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["REMMITERNAME"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["RemiCountry"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["VALUEDT"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["VASTR0_AC"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["MOD_TYPE"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["SCHEDULENO"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["REALISED_AMT"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["EXRT"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["BILLNO"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["BankCode"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["BankName"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["IMP_GL_Code"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["IMP_GL_Sub_Code"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["IMP_Booking_Office_Number"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["IMP_Transaction_Number"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["IMP_Transaction_Indication"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["IMP_CMF_Number"].ToString().Trim() + ",");
                    sw.Write(dt.Rows[j]["IMP_NV_Indicator"].ToString().Trim() + ",");
                    sw.WriteLine(dt.Rows[j]["IMP_Settlement_Date"].ToString().Trim() + ",");                                    
                }
                TF_DATA objServerName = new TF_DATA();
                string _serverName = objServerName.GetServerName();                
                string path = "";
                string link = "";

                path = "file://" + _serverName + "/TF_GeneratedFiles/RRETURN/DataCheck/BR_" + Branchname.Replace(" ", "") + "_DataCheck";
                link = "/TF_GeneratedFiles/RRETURN/DataCheck/BR_" + Branchname.Replace(" ", "") + "_DataCheck";

                lblqename.Text = _strAdCode;
                lnkEDownload.Visible = true;

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
                string _menu = "CSV File Creation For Data Check";
                SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
                menu.Value = _menu;
                TF_DATA objSave = new TF_DATA();
                string at = objSave.SaveDeleteData(_query, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, FWDContractNo, DocumnetDate, Mode, user, moddate, menu);
            }
            else
            {
                lblqename.Text = "";
                lnkEDownload.Visible = false;
                labelMessage.Text = "Records Not Found.";
                //ErrorMessage = "Records Not Found For RES Data.";
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
            //REM END
            #endregion
        }
        catch (Exception Ex)
        {
            SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
            ADCODE.Value = ddlBranch.SelectedValue.ToString();

            SqlParameter MENUNAME = new SqlParameter("@MENUNAME", SqlDbType.VarChar);
            MENUNAME.Value = "CSV File Creation For Data Check";

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
    // Download
    protected void lnkEDownload_Click(object sender, EventArgs e)
    {
        string Branchname = ddlBranch.SelectedItem.ToString().Trim();
        string _todate = txtToDate.Text.ToString().Trim();
        string _strAdCode = "DataCheck_" + ddlBranch.SelectedItem.Value + "_" + _todate.Substring(0, 2) + _todate.Substring(3, 2) + _todate.Substring(6, 4);
        string filePath = "~/TF_GeneratedFiles/RRETURN/DataCheck/BR_" + Branchname.Replace(" ", "") + "DataCheck/" + _strAdCode + ".CSV";
        string FileName = _strAdCode + ".CSV";
        lblqename.Text = _strAdCode + ".CSV";
        Response.ContentType = "image/jpg";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
        Response.TransmitFile(Server.MapPath(filePath));
        Response.End();
    }
}