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
            string Branchname = ddlBranch.SelectedItem.ToString().Trim();
            //_directoryPath = Server.MapPath("~/TF_GeneratedFiles/RRETURN/Conso/BR_" + Branchname.Replace(" ", "") + "_ConsoFile/"+ todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4));
            //_strAdCode = "ERS_" + ddlBranch.SelectedItem.Value + "_" + todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4);
            
            string datePart = todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4);
            string basePath = Server.MapPath("~/TF_GeneratedFiles/RRETURN/Conso/");
            
            _directoryPath = basePath + "BR_" + Branchname.Replace(" ", "") + "_ConsoFile/" + datePart;
            _strAdCode = "ERS_" + ddlBranch.SelectedItem.Value + "_" + datePart;

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
            string _qry = "TF_RET_DATACSV";
            DataTable dt = obj.getData(_qry, p1, p2, p3, p4);
            string _filePath = _directoryPath + "/" + _strAdCode + ".CSV";
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
                    string _strADCODE = dt.Rows[j]["ADCODE"].ToString().Trim();
                    sw.Write(_strADCODE + ",");
                    string _strBRANCHNAME = dt.Rows[j]["BRANCHNAME"].ToString().Trim();
                    sw.Write(_strBRANCHNAME + ",");
                    string _strSRNO = dt.Rows[j]["SR"].ToString();
                    sw.Write(_strSRNO + ",");
                    string _strFR_FORTNIGHT_DT = dt.Rows[j]["FR_FORTNIGHT_DT"].ToString().Trim();
                    sw.Write(_strFR_FORTNIGHT_DT + ",");
                    string _strTO_FORTNIGHT_DT = dt.Rows[j]["TO_FORTNIGHT_DT"].ToString().Trim();
                    sw.Write(_strTO_FORTNIGHT_DT + ",");
                    string _strTRANSACTION_DT = dt.Rows[j]["TRANSACTION_DT"].ToString();
                    sw.Write(_strTRANSACTION_DT + ",");
                    string _strDOCNO = dt.Rows[j]["DOCNO"].ToString();
                    sw.Write(_strDOCNO + ",");
                    string _strIECODE = dt.Rows[j]["IECODE"].ToString();
                    sw.Write(_strIECODE + ",");
                    string _strFORMSRNO = dt.Rows[j]["FORMSRNO"].ToString();
                    sw.Write(_strFORMSRNO + ",");
                    string _strPURPOSEID = dt.Rows[j]["PURPOSEID"].ToString().Trim();
                    sw.Write(_strPURPOSEID + ",");
                    string _strPORT_CODE = dt.Rows[j]["PORT_CODE"].ToString().Trim();
                    sw.Write(_strPORT_CODE + ",");
                    string _strSHIPPING_BILL_NO = dt.Rows[j]["SHIPPING_BILL_NO"].ToString();
                    sw.Write(_strSHIPPING_BILL_NO + ",");
                    string _strSHIPPING_BILL_DT = dt.Rows[j]["SHIPPING_BILL_DT"].ToString();
                    sw.Write(_strSHIPPING_BILL_DT + ",");
                    string _strCURR = dt.Rows[j]["CURR"].ToString();
                    sw.Write(_strCURR + ",");
                    string _strAMOUNT = dt.Rows[j]["AMOUNT"].ToString();
                    sw.Write(_strAMOUNT + ",");
                    string _strINR_AMOUNT = dt.Rows[j]["INR_AMOUNT"].ToString();
                    sw.Write(_strINR_AMOUNT + ",");
                    string _strAC_COUNTRY_CODE = dt.Rows[j]["AC_COUNTRY_CODE"].ToString();
                    sw.Write(_strAC_COUNTRY_CODE + ",");
                    string _strBN_COUNTRY_CODE = dt.Rows[j]["BN_COUNTRY_CODE"].ToString();
                    sw.Write(_strBN_COUNTRY_CODE + ",");
                    
                    string _strCUSTOMSRNO = dt.Rows[j]["CUSTOMSRNO"].ToString().Trim();
                    sw.Write(_strCUSTOMSRNO + ",");
                    string _strLCINDICATION = dt.Rows[j]["LCINDICATION"].ToString().Trim();
                    sw.Write(_strLCINDICATION + ",");
                    string _strBENEFICIARY = dt.Rows[j]["BENEFICIARY"].ToString();
                    sw.Write(_strBENEFICIARY + ",");
                    string _strREMMITER = dt.Rows[j]["REMMITER"].ToString();
                    sw.Write(_strREMMITER + ",");                    
                    string _strVALUEDT = dt.Rows[j]["VALUEDT"].ToString();
                    sw.Write(_strVALUEDT + ",");
                    string _strVASTRO_AC = dt.Rows[j]["VASTRO_AC"].ToString().Trim();
                    sw.Write(_strVASTRO_AC + ",");
                    string _strMOD_TYPE = dt.Rows[j]["MOD_TYPE"].ToString();
                    sw.Write(_strMOD_TYPE + ",");
                    string _strSCHEDULE = dt.Rows[j]["SCHEDULE"].ToString();
                    sw.Write(_strSCHEDULE + ",");
                    string _strREAL_AMT = dt.Rows[j]["REAL_AMT"].ToString();
                    sw.Write(_strREAL_AMT + ",");
                    string _strEXRT = dt.Rows[j]["EXRT"].ToString();
                    sw.Write(_strEXRT + ",");
                    string _strBILLNO = dt.Rows[j]["BILLNO"].ToString();
                    sw.Write(_strBILLNO + ",");
                    string _strBANK_CODE = dt.Rows[j]["BANK_CODE"].ToString();
                    sw.Write(_strBANK_CODE + ",");
                    string _strBANKNAME = dt.Rows[j]["BANKNAME"].ToString();
                    sw.Write(_strBANKNAME + ",");
                    string _strREMMITERCountry = dt.Rows[j]["RemiCountry"].ToString();
                    sw.WriteLine(_strREMMITERCountry + ",");
                }
                string downloadpath = "~/TF_GeneratedFiles/RRETURN/Conso/BR_" + Branchname.Replace(" ", "") + "_ConsoFile/"+ todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4)+"/ERS_" + ddlBranch.SelectedItem.Value + "_" + todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4) + ".CSV";
                txters.Text = _strAdCode + ".CSV";
                HyperLink new_Link1 = new HyperLink();
                new_Link1.Text = "Download File";
                new_Link1.NavigateUrl = downloadpath;
                new_Link1.CssClass = "buttonDefault";
                ersfile.Controls.Add(new_Link1);
            }
            else
            {
                string downloadpath = "~/TF_GeneratedFiles/RRETURN/Conso/BR_" + Branchname.Replace(" ", "") + "_ConsoFile/" + todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4) + "/ERS_" + ddlBranch.SelectedItem.Value + "_" + todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4) + ".CSV";
                txters.Text = _strAdCode + ".CSV";
                HyperLink new_Link1 = new HyperLink();
                new_Link1.Text = "Download File";
                new_Link1.NavigateUrl = downloadpath;
                new_Link1.CssClass = "buttonDefault";
                ersfile.Controls.Add(new_Link1);
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
            string Branchname = ddlBranch.SelectedItem.ToString().Trim();

            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/RRETURN/Conso/BR_" + Branchname.Replace(" ", "") + "_ConsoFile/"+ todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4));
           
            _strAdCode = "Nostro_" + ddlBranch.SelectedItem.Value + "_" + todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4);
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
            //VOSTRO FOR CSV FILE
            TF_DATA obj = new TF_DATA();
            string _qry = "TF_RET_Nostro_DATACSV";
            DataTable dt = obj.getData(_qry, p1, p2, p3, p4);
            string _filePath = _directoryPath + "/" + _strAdCode + ".CSV";
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            string _strHeader = "ADCODE,BRANCHNAME,FR_FORTNIGHT_DT,TO_FORTNIGHT_DT,NV,CURR,OP_CASH_D,OP_CASH_C,OP_SUSP_D,OP_SUSP_C,OP_DEP_OTH_D,OP_DEP_OTH_C,OP_DEP_RBI_D,OP_DEP_RBI_C,OP_FD_D,OP_FD_C,"
+ "OP_TB_D,OP_TB_C,OP_SS_D,OP_SS_C,OP_FCLO_D,OP_FCLO_C,OP_OTH_D,OP_OTH_C,OP_TOT_D,OP_TOT_C,CL_CASH_D,CL_CASH_C,CL_SUSP_D,CL_SUSP_C,CL_DEP_OTH_D,CL_DEP_OTH_C,CL_DEP_RBI_D,"
+ "CL_DEP_RBI_C,CL_FD_D,CL_FD_C,CL_TB_D,CL_TB_C,CL_SS_D,CL_SS_C,CL_FCLO_D,CL_FCLO_C,CL_OTH_D,CL_OTH_C,CL_TOT_D,CL_TOT_C,EEFCAC,EFCAC,RFCAC,ESCROWAC,FCNRAC,OTHERAC";
            sw.WriteLine(_strHeader);
            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string _strADCODE = dt.Rows[j]["ADCODE"].ToString().Trim();
                    sw.Write(_strADCODE + ",");
                    string _strBRANCHNAME = Branchname;
                    sw.Write(_strBRANCHNAME + ",");
                    //string _strBRANCHNAME = dt.Rows[j]["BRANCHNAME"].ToString().Trim();
                    //sw.Write(_strBRANCHNAME + ",");                    
                    string _strFR_FORTNIGHT_DT = dt.Rows[j]["FR_FORTNIGHT_DT"].ToString().Trim();
                    sw.Write(_strFR_FORTNIGHT_DT + ",");
                    string _strTO_FORTNIGHT_DT = dt.Rows[j]["TO_FORTNIGHT_DT"].ToString().Trim();
                    sw.Write(_strTO_FORTNIGHT_DT + ",");
                    string _strNV = dt.Rows[j]["NV"].ToString();
                    sw.Write(_strNV + ",");
                    string _strCURR = dt.Rows[j]["CURR"].ToString();
                    sw.Write(_strCURR + ",");
                    string _strOP_CASH_D = dt.Rows[j]["OP_CASH_D"].ToString();
                    sw.Write(_strOP_CASH_D + ",");
                    string _strOP_CASH_C = dt.Rows[j]["OP_CASH_C"].ToString();
                    sw.Write(_strOP_CASH_C + ",");
                    string _strOP_SUSP_D = dt.Rows[j]["OP_SUSP_D"].ToString().Trim();
                    sw.Write(_strOP_SUSP_D + ",");
                    string _strOP_SUSP_C = dt.Rows[j]["OP_SUSP_C"].ToString().Trim();
                    sw.Write(_strOP_SUSP_C + ",");
                    string _strOP_DEP_OTH_D = dt.Rows[j]["OP_DEP_OTH_D"].ToString();
                    sw.Write(_strOP_DEP_OTH_D + ",");
                    string _strOP_DEP_OTH_C = dt.Rows[j]["OP_DEP_OTH_C"].ToString();
                    sw.Write(_strOP_DEP_OTH_C + ",");
                    string _strOP_DEP_RBI_D = dt.Rows[j]["OP_DEP_RBI_D"].ToString();
                    sw.Write(_strOP_DEP_RBI_D + ",");
                    string _strOP_DEP_RBI_C = dt.Rows[j]["OP_DEP_RBI_C"].ToString();
                    sw.Write(_strOP_DEP_RBI_C + ",");
                    string _strOP_FD_D = dt.Rows[j]["OP_FD_D"].ToString();
                    sw.Write(_strOP_FD_D + ",");
                    string _strOP_FD_C = dt.Rows[j]["OP_FD_C"].ToString();
                    sw.Write(_strOP_FD_C + ",");
                    string _strOP_TB_D = dt.Rows[j]["OP_TB_D"].ToString();
                    sw.Write(_strOP_TB_D + ",");
                    string _strOP_TB_C = dt.Rows[j]["OP_TB_C"].ToString().Trim();
                    sw.Write(_strOP_TB_C + ",");
                    string _strOP_SS_D = dt.Rows[j]["OP_SS_D"].ToString().Trim();
                    sw.Write(_strOP_SS_D + ",");
                    string _strOP_SS_C = dt.Rows[j]["OP_SS_C"].ToString();
                    sw.Write(_strOP_SS_C + ",");
                    string _strOP_FCLO_D = dt.Rows[j]["OP_FCLO_D"].ToString();
                    sw.Write(_strOP_FCLO_D + ",");
                    string _strOP_FCLO_C = dt.Rows[j]["OP_FCLO_C"].ToString();
                    sw.Write(_strOP_FCLO_C + ",");
                    string _strOP_OTH_D = dt.Rows[j]["OP_OTH_D"].ToString().Trim();
                    sw.Write(_strOP_OTH_D + ",");
                    string _strOP_OTH_C = dt.Rows[j]["OP_OTH_C"].ToString();
                    sw.Write(_strOP_OTH_C + ",");
                    string _strOP_TOT_D = dt.Rows[j]["OP_TOT_D"].ToString();
                    sw.Write(_strOP_TOT_D + ",");
                    string _strOP_TOT_C = dt.Rows[j]["OP_TOT_C"].ToString();
                    sw.Write(_strOP_TOT_C + ",");
                    string _strCL_CASH_D = dt.Rows[j]["CL_CASH_D"].ToString();
                    sw.Write(_strCL_CASH_D + ",");
                    string _strCL_CASH_C = dt.Rows[j]["CL_CASH_C"].ToString();
                    sw.Write(_strCL_CASH_C + ",");
                    string _strCL_SUSP_D = dt.Rows[j]["CL_SUSP_D"].ToString();
                    sw.Write(_strCL_SUSP_D + ",");
                    string _strCL_SUSP_C = dt.Rows[j]["CL_SUSP_C"].ToString();
                    sw.Write(_strCL_SUSP_C + ",");
                    string _strCL_DEP_OTH_D = dt.Rows[j]["CL_DEP_OTH_D"].ToString();
                    sw.Write(_strCL_DEP_OTH_D + ",");
                    string _strCL_DEP_OTH_C = dt.Rows[j]["CL_DEP_OTH_C"].ToString();
                    sw.Write(_strCL_DEP_OTH_C + ",");
                    string _strCL_DEP_RBI_D = dt.Rows[j]["CL_DEP_RBI_D"].ToString().Trim();
                    sw.Write(_strCL_DEP_RBI_D + ",");
                    string _strCL_DEP_RBI_C = dt.Rows[j]["CL_DEP_RBI_C"].ToString().Trim();
                    sw.Write(_strCL_DEP_RBI_C + ",");
                    string _strCL_FD_D = dt.Rows[j]["CL_FD_D"].ToString();
                    sw.Write(_strCL_FD_D + ",");
                    string _strCL_FD_C = dt.Rows[j]["CL_FD_C"].ToString();
                    sw.Write(_strCL_FD_C + ",");
                    string _strCL_TB_D = dt.Rows[j]["CL_TB_D"].ToString();
                    sw.Write(_strCL_TB_D + ",");
                    string _strCL_TB_C = dt.Rows[j]["CL_TB_C"].ToString().Trim();
                    sw.Write(_strCL_TB_C + ",");
                    string _strCL_SS_D = dt.Rows[j]["CL_SS_D"].ToString();
                    sw.Write(_strCL_SS_D + ",");
                    string _strCL_SS_C = dt.Rows[j]["CL_SS_C"].ToString();
                    sw.Write(_strCL_SS_C + ",");
                    string _strCL_FCLO_D = dt.Rows[j]["CL_FCLO_D"].ToString();
                    sw.Write(_strCL_FCLO_D + ",");
                    string _strCL_FCLO_C = dt.Rows[j]["CL_FCLO_C"].ToString();
                    sw.Write(_strCL_FCLO_C + ",");
                    string _strCL_OTH_D = dt.Rows[j]["CL_OTH_D"].ToString();
                    sw.Write(_strCL_OTH_D + ",");
                    string _strCL_OTH_C = dt.Rows[j]["CL_OTH_C"].ToString();
                    sw.Write(_strCL_OTH_C + ",");
                    string _strCL_TOT_D = dt.Rows[j]["CL_TOT_D"].ToString();
                    sw.Write(_strCL_TOT_D + ",");
                    string _strCL_TOT_C = dt.Rows[j]["CL_TOT_C"].ToString();
                    sw.Write(_strCL_TOT_C + ",");
                    string _strEEFCAC = dt.Rows[j]["EEFCAC"].ToString();
                    sw.Write(_strEEFCAC + ",");
                    string _strEFCAC = dt.Rows[j]["EFCAC"].ToString().Trim();
                    sw.Write(_strEFCAC + ",");
                    string _strRFCAC = dt.Rows[j]["RFCAC"].ToString().Trim();
                    sw.Write(_strRFCAC + ",");
                    string _strESCROWAC = dt.Rows[j]["ESCROWAC"].ToString();
                    sw.Write(_strESCROWAC + ",");
                    string _strFCNRAC = dt.Rows[j]["FCNRAC"].ToString();
                    sw.Write(_strFCNRAC + ",");
                    string _strOTHERAC = dt.Rows[j]["OTHERAC"].ToString();
                    sw.WriteLine(_strOTHERAC + ",");
                }
                string downloadpath = "~/TF_GeneratedFiles/RRETURN/Conso/BR_" + Branchname.Replace(" ", "") + "_ConsoFile/" + todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4) + "/Nostro_" + ddlBranch.SelectedItem.Value + "_" + todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4) + ".CSV";
                txtnostro.Text = _strAdCode + ".CSV";
                HyperLink new_Link1 = new HyperLink();
                new_Link1.Text = "Download File";
                new_Link1.NavigateUrl = downloadpath;
                new_Link1.CssClass = "buttonDefault";
                nostrofile.Controls.Add(new_Link1);
            }
            else
            {
                string downloadpath = "~/TF_GeneratedFiles/RRETURN/Conso/BR_" + Branchname.Replace(" ", "") + "_ConsoFile/" + todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4) + "/Nostro_" + ddlBranch.SelectedItem.Value + "_" + todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4) + ".CSV";
                txtnostro.Text = _strAdCode + ".CSV";
                HyperLink new_Link1 = new HyperLink();
                new_Link1.Text = "Download File";
                new_Link1.NavigateUrl = downloadpath;
                new_Link1.CssClass = "buttonDefault";
                nostrofile.Controls.Add(new_Link1);
                labelMessage.Text = "Records Not Found.";
                //ErrorMessage = "Records Not Found For Nostro O/C Balance.";
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
            string Branchname = ddlBranch.SelectedItem.ToString().Trim();

            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/RRETURN/Conso/BR_" + Branchname.Replace(" ", "") + "_ConsoFile/" + todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4));
            
            _strAdCode = "Vostro_" + ddlBranch.SelectedItem.Value + "_" + todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4);
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
            string _qry = "TF_Vostro_DATACSV";
            DataTable dt = obj.getData(_qry, p1, p2, p3, p4);
            string _filePath = _directoryPath + "/" + _strAdCode + ".CSV";
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            string _strHeader = "ADCODE,BRANCH NAME,FR_FORTNIGHT_DT,TO_FORTNIGHT_DT,NV,CURR,COUNTRY CODE,BANK NAME,OP_D,OP_C,CL_D,CL_C,BANKCODE";
            sw.WriteLine(_strHeader);
            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string _strADCODE = dt.Rows[j]["ADCODE"].ToString().Trim();
                    sw.Write(_strADCODE + ",");
                    string _strBRANCHNAME = Branchname;
                    sw.Write(_strBRANCHNAME + ",");
                    string _strFR_FORTNIGHT_DT = dt.Rows[j]["FR_FORTNIGHT_DT"].ToString().Trim();
                    sw.Write(_strFR_FORTNIGHT_DT + ",");
                    string _strTO_FORTNIGHT_DT = dt.Rows[j]["TO_FORTNIGHT_DT"].ToString().Trim();
                    sw.Write(_strTO_FORTNIGHT_DT + ",");
                    string _strNV = dt.Rows[j]["NV"].ToString();
                    sw.Write(_strNV + ",");
                    string _strCURR = dt.Rows[j]["CURR"].ToString();
                    sw.Write(_strCURR + ",");
                    string _strCOUNTRYCODE = dt.Rows[j]["COUNTRYCODE"].ToString();
                    sw.Write(_strCOUNTRYCODE + ",");
                    string _strBANKNAME = dt.Rows[j]["BANKNAME"].ToString();
                    sw.Write(_strBANKNAME + ",");
                    string _strOP_D = dt.Rows[j]["OP_D"].ToString();
                    sw.Write(_strOP_D + ",");
                    string _strOP_C = dt.Rows[j]["OP_C"].ToString();
                    sw.Write(_strOP_C + ",");
                    string _strCL_D = dt.Rows[j]["CL_D"].ToString().Trim();
                    sw.Write(_strCL_D + ",");
                    string _strCL_C = dt.Rows[j]["CL_C"].ToString().Trim();
                    sw.Write(_strCL_C + ",");
                    string _strBANKCODE = dt.Rows[j]["BANKCODE"].ToString();
                    sw.WriteLine(_strBANKCODE + ",");
                }
                string downloadpath = "~/TF_GeneratedFiles/RRETURN/Conso/BR_" + Branchname.Replace(" ", "") + "_ConsoFile/" + todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4) + "/Vostro_" + ddlBranch.SelectedItem.Value + "_" + todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4) + ".CSV";
                txtvostro.Text = _strAdCode + ".CSV";
                HyperLink new_Link1 = new HyperLink();
                new_Link1.Text = "Download File";
                new_Link1.NavigateUrl = downloadpath;                
                new_Link1.CssClass = "buttonDefault";
                vostrofile.Controls.Add(new_Link1);
            }
            else
            {
                string downloadpath = "~/TF_GeneratedFiles/RRETURN/Conso/BR_" + Branchname.Replace(" ", "") + "_ConsoFile/" + todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4) + "/Vostro_" + ddlBranch.SelectedItem.Value + "_" + todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4) + ".CSV";
                txtvostro.Text = _strAdCode + ".CSV";
                HyperLink new_Link1 = new HyperLink();
                new_Link1.Text = "Download File";
                new_Link1.NavigateUrl = downloadpath;
                new_Link1.CssClass = "buttonDefault";
                vostrofile.Controls.Add(new_Link1);
                labelMessage.Text = "Records Not Found.";
                //ErrorMessage = "Records Not Found For Vostro O/C Balance.";
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