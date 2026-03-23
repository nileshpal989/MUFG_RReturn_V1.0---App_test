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

public partial class RRETURN_RET_RBITextFileAtHeadOffice : System.Web.UI.Page
{
    string QE_File_Path, BOP6_File_Path;
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
            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            dateInfo.ShortDatePattern = "dd/MM/yyyy";
            DateTime documentDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
            DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);
            string todate = txtToDate.Text.Trim();
            string formattedDate = Convert.ToDateTime(todate).ToString("ddMMyyyy");
            string Branchname = ddlBranch.SelectedItem.ToString().Trim();
            SqlParameter BranchName = new SqlParameter("@Branch", SqlDbType.VarChar);
            BranchName.Value = ddlBranch.SelectedItem.ToString().Trim();
            SqlParameter startDate = new SqlParameter("@startdate", SqlDbType.VarChar);
            startDate.Value = DateTime.ParseExact(txtFromDate.Text.Trim(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
            SqlParameter endDate = new SqlParameter("@enddate", SqlDbType.VarChar);
            endDate.Value = DateTime.ParseExact(txtToDate.Text.Trim(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
            TF_DATA DataVal = new TF_DATA();
            string _directoryPath = "";
            string _strAdCode = "";

            //----------------------30032026 NIlesh ------------------------------------------------------
            //_directoryPath = Server.MapPath("~/TF_GeneratedFiles/RRETURN/HO-RBI-ERS-CON/" + todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4));
            string basePath = Server.MapPath("~/TF_GeneratedFiles/RRETURN/HO-RBI-ERS-CON/");
            _directoryPath = Path.Combine(basePath,formattedDate);
            _strAdCode = ddlBranch.Text.ToString().Trim().Substring(0, 3);

                if (!Directory.Exists(_directoryPath))
                {
                    Directory.CreateDirectory(_directoryPath);
                }
                SqlParameter p1 = new SqlParameter("@Branch", SqlDbType.VarChar);
                p1.Value = ddlBranch.SelectedItem.ToString().Trim();
                SqlParameter p2 = new SqlParameter("@startdate", SqlDbType.VarChar);
                p2.Value = documentDate.ToString("dd/MM/yyyy");
                SqlParameter p3 = new SqlParameter("@enddate", SqlDbType.VarChar);
                p3.Value = documentDate1.ToString("dd/MM/yyyy");
                TF_DATA objQE = new TF_DATA();
                string _qryQEValidate = "TF_RET_RBI_Text_File_Validation";
                DataTable dtQEValidate = objQE.getData(_qryQEValidate, p1, p2, p3);
                if (dtQEValidate.Rows[0][0].ToString() != "0")
                {
                    #region QEFile
                    //REM FOR QE FILE
                    string _qryQE = "TF_RET_RBI_ConverPage";
                    DataTable dtQE = objQE.getData(_qryQE, p1, p2, p3);
                    //----------------------30032026 NIlesh ------------------------------------------------------
                    //string _filePath = _directoryPath + "/" + _strAdCode + "_QE.TXT";
                    //string NameQE="/" + _strAdCode + "_QE.TXT";
                    //string _filePath = _directoryPath + NameQE;
                    string safeAdCode = Path.GetFileName(_strAdCode); // remove path chars
                    string NameQE = safeAdCode + "_QE.TXT";
                    string _filePath = Path.Combine(_directoryPath, NameQE);
                    StreamWriter sw;
                    sw = File.CreateText(_filePath);
                    if (dtQE.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtQE.Rows.Count; j++)
                        {
                            string _strADCODE = dtQE.Rows[j]["ADCODE"].ToString().Trim();
                            sw.Write(_strADCODE + "|");
                            string _strTO_FORTNIGHT_DT = dtQE.Rows[j]["TO_FORTNIGHT_DT"].ToString().Trim();
                            sw.Write(_strTO_FORTNIGHT_DT + "|");
                            int _strSr = j + 1;
                            string _strSrNo = _strSr.ToString("0000");
                            sw.Write(_strSrNo + "|");
                            string _strPURPOSE_CODE = dtQE.Rows[j]["PURPOSE_CODE"].ToString().Trim();
                            sw.Write(_strPURPOSE_CODE + "|");
                            string _strAC_COUNTRY_CODE = dtQE.Rows[j]["AC_COUNTRY_CODE"].ToString();
                            sw.Write(_strAC_COUNTRY_CODE + "|");
                            string _strCURR = dtQE.Rows[j]["CURR"].ToString().Trim();
                            sw.Write(_strCURR + "|");
                            string _strFC_AMOUNT = dtQE.Rows[j]["FC_AMOUNT"].ToString().Trim();
                            double _strAMT = Convert.ToDouble(_strFC_AMOUNT);
                            _strFC_AMOUNT = _strAMT.ToString("000000000000000");
                            sw.Write(_strFC_AMOUNT + "|");
                            string _strVOSTRO_AC = dtQE.Rows[j]["VASTRO_AC"].ToString().Trim();
                            sw.WriteLine(_strVOSTRO_AC + "|");
                        }
                        lblqename.Text = "644_QE";
                        lnkQEDownload.Visible = true;
                    }
                    else
                    {
                        lblqename.Text = "644_QE";
                        lnkQEDownload.Visible = true;
                        sw.WriteLine("|");
                    }
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                    //REM END
                    #endregion

                    #region BOP6File For RBI At Head Office
                    TF_DATA objBOP = new TF_DATA();
                    string _qryBOP6 = "TF_RET_RESRBI_BOP6";
                    DataTable dtBOP6 = objBOP.getData(_qryBOP6, p1, p2, p3);
                    //----------------------30032026 NIlesh ------------------------------------------------------
                    //string NameBOP6 = "/" + _strAdCode + "_BOP6.TXT";
                    //_filePath = _directoryPath + NameBOP6;
                    //_filePath = _directoryPath + "/" + _strAdCode + "_BOP6.TXT";
                    string NameBOP6 = safeAdCode + "_BOP6.TXT";
                    _filePath = Path.Combine(_directoryPath, NameBOP6);
                    StreamWriter swBOP6;
                    swBOP6 = File.CreateText(_filePath);
                    if (dtBOP6.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtBOP6.Rows.Count; j++)
                        {
                            string _strADCODEBOP6 = dtBOP6.Rows[j]["ADCODE"].ToString().Trim();
                            swBOP6.Write(_strADCODEBOP6 + "|");
                            string _strTO_FORTNIGHT_DT_ENC = dtBOP6.Rows[j]["TO_FORTNIGHT_DT"].ToString().Trim();
                            swBOP6.Write(_strTO_FORTNIGHT_DT_ENC + "|");
                            string _strTRANS_DT = dtBOP6.Rows[j]["TRANS_DT"].ToString().Trim();
                            swBOP6.Write(_strTRANS_DT + "|");
                            string _strSR = dtBOP6.Rows[j]["SRNO"].ToString();
                            swBOP6.Write(_strSR + "|");
                            string _strPURPOSEID = dtBOP6.Rows[j]["PURPOSEID"].ToString().Trim();
                            swBOP6.Write(_strPURPOSEID + "|");
                            string _strBN_COUNTRY_CODE = dtBOP6.Rows[j]["BN_COUNTRY_CODE"].ToString();
                            swBOP6.Write(_strBN_COUNTRY_CODE + "|");
                            string _strCURRBOP6 = dtBOP6.Rows[j]["CURR"].ToString();
                            swBOP6.Write(_strCURRBOP6 + "|");
                            string _strFC_AMOUNT = dtBOP6.Rows[j]["FC_AMOUNT"].ToString().Trim();
                            double _strAMT = Convert.ToDouble(_strFC_AMOUNT);
                            _strFC_AMOUNT = _strAMT.ToString("000000000000000");
                            swBOP6.Write(_strFC_AMOUNT + "|");
                            string _strSHIP_DT = dtBOP6.Rows[j]["SHIP_DT"].ToString();
                            swBOP6.Write(_strSHIP_DT + "|");
                            string _strLCINDICATION = dtBOP6.Rows[j]["LCINDICATION"].ToString();
                            swBOP6.Write(_strLCINDICATION + "|");
                            string _strAC_COUNTRY_CODE = dtBOP6.Rows[j]["AC_COUNTRY_CODE"].ToString();
                            swBOP6.Write(_strAC_COUNTRY_CODE + "|");
                            string _strRemi_COUNTRY_CODE = dtBOP6.Rows[j]["RemiCountry"].ToString();
                            swBOP6.WriteLine(_strRemi_COUNTRY_CODE + "|");
                        }
                        lblbop6name.Text = "644_BOP6";
                        lnkBOP6Download.Visible = true;
                    }
                    else
                    {
                        lblbop6name.Text = "644_BOP6";
                        lnkBOP6Download.Visible = true;
                        swBOP6.WriteLine("|");
                    }
                    swBOP6.Flush();
                    swBOP6.Close();
                    swBOP6.Dispose();
                    ////Rem End
                    #endregion
                    #region ENCFile
                    ////REM FOR ENC FILE
                    //TF_DATA objENC = new TF_DATA();
                    //string _qryENC = "TF_RET_RBIENC";
                    //DataTable dtENC = objENC.getData(_qryENC, p1, p2, p3);
                    //_filePath = _directoryPath + "/" + _strAdCode + "_ENC.TXT";
                    //StreamWriter swENC;
                    //swENC = File.CreateText(_filePath);
                    //if (dtENC.Rows.Count > 0)
                    //{
                    //    for (int j = 0; j < dtENC.Rows.Count; j++)
                    //    {
                    //        string _strADCODEENC = dtENC.Rows[j]["ADCODE"].ToString().Trim();
                    //        swENC.Write(_strADCODEENC + "|");
                    //        string _strTO_FORTNIGHT_DT_ENC = dtENC.Rows[j]["TO_FORTNIGHT_DT"].ToString().Trim();
                    //        swENC.Write(_strTO_FORTNIGHT_DT_ENC + "|");
                    //        string _strTRANS_DT = dtENC.Rows[j]["TRANS_DT"].ToString().Trim();
                    //        swENC.Write(_strTRANS_DT + "|");
                    //        string _strBILLNO = dtENC.Rows[j]["BILLNO"].ToString().Trim();
                    //        swENC.Write(_strBILLNO + "|");
                    //        string _strIECODE = dtENC.Rows[j]["IECODE"].ToString();
                    //        swENC.Write(_strIECODE + "|");
                    //        string _strFORMSRNO = dtENC.Rows[j]["FORMSRNO"].ToString();
                    //        swENC.Write(_strFORMSRNO + "|");
                    //        string _strSHIPP_NO = dtENC.Rows[j]["SHIPP_NO"].ToString();
                    //        swENC.Write(_strSHIPP_NO + "|");
                    //        string _strSHIP_DT = dtENC.Rows[j]["SHIP_DT"].ToString();
                    //        swENC.Write(_strSHIP_DT + "|");
                    //        string _strCUSTOMSR = dtENC.Rows[j]["CUSTOMSR"].ToString();
                    //        swENC.Write(_strCUSTOMSR + "|");
                    //        string _strCURRENC = dtENC.Rows[j]["CURR"].ToString();
                    //        swENC.Write(_strCURRENC + "|");
                    //        string _strFC_AMOUNTENC = dtENC.Rows[j]["AMOUNT"].ToString().Trim();
                    //        double _strAMTENC = Convert.ToDouble(_strFC_AMOUNTENC);
                    //        _strFC_AMOUNTENC = _strAMTENC.ToString("00000000000000");
                    //        swENC.Write(_strFC_AMOUNTENC + "|");
                    //        string _strBN_COUNTRY_CODE = dtENC.Rows[j]["BN_COUNTRY_CODE"].ToString();
                    //        swENC.WriteLine(_strBN_COUNTRY_CODE + "|");
                    //    }
                    //}
                    //else
                    //{
                    //    swENC.WriteLine("|");
                    //}
                    //swENC.Flush();
                    //swENC.Close();
                    //swENC.Dispose();
                    ////Rem End
                    #endregion
                    #region SCH3TO6File
                    //TF_DATA objSCH3TO6 = new TF_DATA();
                    //string _qrySCH3TO6 = "TF_RET_RBISCH3TO6";
                    //DataTable dtSCH3TO6 = objSCH3TO6.getData(_qrySCH3TO6, p1, p2, p3);
                    //_filePath = _directoryPath + "/" + _strAdCode + "_SCH3TO6.TXT";
                    //StreamWriter swSCH3TO6;
                    //swSCH3TO6 = File.CreateText(_filePath);
                    //if (dtSCH3TO6.Rows.Count > 0)
                    //{
                    //    for (int j = 0; j < dtSCH3TO6.Rows.Count; j++)
                    //    {
                    //        string _strADCODEBOP6 = dtSCH3TO6.Rows[j]["ADCODE"].ToString().Trim();
                    //        swSCH3TO6.Write(_strADCODEBOP6 + "|");
                    //        string _strTO_FORTNIGHT_DT_ENC = dtSCH3TO6.Rows[j]["TO_FORTNIGHT_DT"].ToString().Trim();
                    //        swSCH3TO6.Write(_strTO_FORTNIGHT_DT_ENC + "|");
                    //        string _strTRANS_DT = dtSCH3TO6.Rows[j]["TRANS_DT"].ToString().Trim();
                    //        swSCH3TO6.Write(_strTRANS_DT + "|");
                    //        string _strBILLNO = dtSCH3TO6.Rows[j]["BILLNO"].ToString();
                    //        swSCH3TO6.Write(_strBILLNO + "|");
                    //        string _strFORMSRNO = dtSCH3TO6.Rows[j]["FORMSRNO"].ToString().Trim();
                    //        swSCH3TO6.Write(_strFORMSRNO + "|");
                    //        string _strSHIPP_NO = dtSCH3TO6.Rows[j]["SHIPP_NO"].ToString();
                    //        swSCH3TO6.Write(_strSHIPP_NO + "|");
                    //        string _strSHIP_DT = dtSCH3TO6.Rows[j]["SHIP_DT"].ToString();
                    //        swSCH3TO6.Write(_strSHIP_DT + "|");
                    //        string _strCURRBOP6 = dtSCH3TO6.Rows[j]["CURR"].ToString();
                    //        swSCH3TO6.Write(_strCURRBOP6 + "|");
                    //        string _strFC_AMOUNT = dtSCH3TO6.Rows[j]["AMOUNT"].ToString().Trim();
                    //        double _strAMT = Convert.ToDouble(_strFC_AMOUNT);
                    //        _strFC_AMOUNT = _strAMT.ToString("000000000000000");
                    //        swSCH3TO6.Write(_strFC_AMOUNT + "|");
                    //        string _strFC_AMOUNTREAL = dtSCH3TO6.Rows[j]["REALISED_AMT"].ToString().Trim();
                    //        double _strAMTREAL = Convert.ToDouble(_strFC_AMOUNTREAL);
                    //        _strFC_AMOUNTREAL = _strAMTREAL.ToString("000000000000000");
                    //        swSCH3TO6.Write(_strFC_AMOUNTREAL + "|");
                    //        string _strBN_COUNTRY_CODE = dtSCH3TO6.Rows[j]["BN_COUNTRY_CODE"].ToString();
                    //        swSCH3TO6.Write(_strBN_COUNTRY_CODE + "|");
                    //        string _strSCHEDULENO = dtSCH3TO6.Rows[j]["SCHEDULENO"].ToString();
                    //        swSCH3TO6.WriteLine(_strSCHEDULENO + "|");
                    //    }
                    //}
                    //else
                    //{
                    //    swSCH3TO6.WriteLine("|");
                    //}
                    //swSCH3TO6.Flush();
                    //swSCH3TO6.Close();
                    //swSCH3TO6.Dispose();
                    ////Rem End
                    #endregion
                    TF_DATA objServerName = new TF_DATA();
                    string _serverName = objServerName.GetServerName();
                    string path = "";
                    string link = "";
                    path = "file://" + _serverName + "/GeneratedFiles/RRETURN/HO-RBI-ERS-CON";
                    link = "/GeneratedFiles/RRETURN/HO-RBI-ERS-CON";
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
                    string _menu = "RBI Text File Creation At Head Office";
                    SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
                    menu.Value = _menu;
                    TF_DATA objSave = new TF_DATA();
                    string at = objSave.SaveDeleteData(_query, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, FWDContractNo, DocumnetDate, Mode, user, moddate, menu);
                }
                else
                {
                    string script = "alert('Consolidate data is not available. You can not create text file.')";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
                    labelMessage.Text = "Consolidate data is not available. You can not create text file.";
                }
        }
        catch (Exception Ex)
        {
            SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
            ADCODE.Value = ddlBranch.SelectedValue.ToString();

            SqlParameter MENUNAME = new SqlParameter("@MENUNAME", SqlDbType.VarChar);
            MENUNAME.Value = "RBI Text File Creation At Head Office";

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

    // QE Download
    protected void lnkQEDownload_Click(object sender, EventArgs e)
    {
        string todate = txtToDate.Text.Trim();
        string filePath = "~/TF_GeneratedFiles/RRETURN/HO-RBI-ERS-CON/" + todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4) + "/644_QE.txt";
        string FileName = "644_QE.txt";
        lblqename.Text = "644_QE.txt";
        Response.ContentType = "image/jpg";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
        Response.TransmitFile(Server.MapPath(filePath));
        Response.End();
    }
    // BOP6 Download
    protected void lnkBOP6Download_Click(object sender, EventArgs e)
    {
        string todate = txtToDate.Text.Trim();
        string filePath = "~/TF_GeneratedFiles/RRETURN/HO-RBI-ERS-CON/" + todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4) + "/644_BOP6.txt";
        string FileName = "644_BOP6.txt";
        lblbop6name.Text = "644_BOP6.txt";
        Response.ContentType = "image/jpg";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
        Response.TransmitFile(Server.MapPath(filePath));
        Response.End();
    }
}