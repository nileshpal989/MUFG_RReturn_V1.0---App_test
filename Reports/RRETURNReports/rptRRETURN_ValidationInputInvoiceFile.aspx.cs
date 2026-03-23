using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using System.Net;
using Microsoft.Win32;
using System.Threading;
using System.Diagnostics;
//using Microsoft.VisualBasic.FileIO;
using System.Configuration;
using System.Globalization;

public partial class Reports_RRETURNReports_rptRRETURN_ValidationInputInvoiceFile : System.Web.UI.Page
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
                //btnUpload.Attributes.Add("onclick", "return validate();");
            }
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
    protected void btnValidate_Click(object sender, EventArgs e)
    {        //System.Threading.Thread.Sleep(10000);
        try
        {
            TF_DATA objData = new TF_DATA();
            DataTable dt = new DataTable();

            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            dateInfo.ShortDatePattern = "dd/MM/yyyy";

            DateTime documentDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
            DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);
            // new 
            TF_DATA objData_Check = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@Branch", SqlDbType.VarChar);
            p1.Value = ddlBranch.SelectedItem.ToString().Trim();

            SqlParameter p2 = new SqlParameter("@FromDate", txtFromDate.Text);
            SqlParameter p3 = new SqlParameter("@ToDate", txtToDate.Text);
            //string _qryChk = "Rreturn_Delete_CSV_File_transection";

            //DataTable dtChk = objData_Check.getData(_qryChk, p1, p2, p3);

            if (fileinhouse.HasFile)
            {
                string fname;
                fname = fileinhouse.FileName;
                //string path = Server.MapPath(fileinhouse.PostedFile.FileName);
                txtInputFile.Text = fileinhouse.PostedFile.FileName;
                if (fname.Contains(".xls") == true || fname.Contains(".xlsx") == true || fname.Contains(".XLS") == true || fname.Contains(".XLSX") == true)
                {
                    string FileName = Path.GetFileName(fileinhouse.PostedFile.FileName);
                    string Extension = Path.GetExtension(fileinhouse.PostedFile.FileName);
                    string FolderPath = Server.MapPath("../Uploaded_Files");

                    if (!Directory.Exists(FolderPath))
                    {
                        Directory.CreateDirectory(FolderPath);
                    }

                    FileName = FileName.Replace(" ", "");

                    string FilePath = FolderPath + "\\" + System.IO.Path.GetFileName(fileinhouse.PostedFile.FileName);
                    fileinhouse.SaveAs(FilePath);
                    GetExcelSheets(FilePath, Extension, "No");
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Excel File First.')", true);
            }
        }
        catch (Exception Ex)
        {
            SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
            ADCODE.Value = ddlBranch.SelectedValue.ToString();

            SqlParameter MENUNAME = new SqlParameter("@MENUNAME", SqlDbType.VarChar);
            MENUNAME.Value = "Data Validation Of Input Upload File";

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
    private void GetExcelSheets(string FilePath, string Extension, string isHDR)
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            dateInfo.ShortDatePattern = "dd/MM/yyyy";
            DateTime documentDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
            DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);
            TF_DATA objData_Check = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@Branch", SqlDbType.VarChar);
            p1.Value = ddlBranch.SelectedItem.ToString().Trim();
            SqlParameter p2 = new SqlParameter("@startdate", SqlDbType.VarChar);
            p2.Value = documentDate.ToString("MM/dd/yyyy");
            SqlParameter p3 = new SqlParameter("@enddate", SqlDbType.VarChar);
            p3.Value = documentDate1.ToString("MM/dd/yyyy");
            SqlParameter _adcode = new SqlParameter("@ADCode", SqlDbType.VarChar);
            _adcode.Value = ddlBranch.SelectedItem.Value;
            string _qryChk = "TF_RET_Input_File_Delete";
            DataTable dtChk = objData_Check.getData(_qryChk, _adcode);


            string conStr = "";
            int norecinexcel = 0;
            int cnt = 0;
            int cntTot = 0;
            int errorcount = 0;

            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
                case ".XLS": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".XLSX": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, isHDR);

            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();

            DataTable dt = new DataTable();

            cmdExcel.Connection = connExcel;
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();
            connExcel.Open();

            //cmdExcel.CommandText = "SELECT * FROM [" + SheetName + "]";
            if (dtExcelSchema.Rows.Count > 0)
            {
                // Strict whitelist validation
                if (System.Text.RegularExpressions.Regex.IsMatch(SheetName, @"^[A-Za-z0-9_]+\$$"))
                {
                    // Escape closing bracket (extra safety)
                    SheetName = SheetName.Replace("]", "]]");

                    cmdExcel.CommandText = "SELECT * FROM [" + SheetName + "]";
                }
                else
                {
                    throw new Exception("Invalid sheet name format.");
                }
            }

            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();
            int RowCount = dt.Rows.Count;
            if (dt.Columns.Count == 71)
            {
                if (dt.Rows.Count > 1)
                {
                    for (int i = 1; i < RowCount; i++)
                    {
                        if (dt.Rows[i][0].ToString().Trim() != "")
                        {
                            norecinexcel = norecinexcel + 1;

                            string Item = dt.Rows[i][0].ToString();
                            SqlParameter p24 = new SqlParameter("@Itemno", SqlDbType.VarChar);
                            p24.Value = Item;

                            string FILE_AS_OF_DT = dt.Rows[i][1].ToString();
                            SqlParameter p4 = new SqlParameter("@FILE_AS_OF_DT", SqlDbType.VarChar);
                            p4.Value = FILE_AS_OF_DT;

                            string SRNO = dt.Rows[i][2].ToString();
                            SqlParameter p5 = new SqlParameter("@SRNO", SqlDbType.VarChar);
                            p5.Value = SRNO;

                            string IMP_Booking_Office_Number = dt.Rows[i][3].ToString();
                            SqlParameter p6 = new SqlParameter("@IMP_Booking_Office_Number", SqlDbType.VarChar);
                            p6.Value = IMP_Booking_Office_Number;

                            string Bank_REF_NO = dt.Rows[i][4].ToString();
                            SqlParameter p7 = new SqlParameter("@Bank_REF_NO", SqlDbType.VarChar);
                            p7.Value = Bank_REF_NO;

                            string MOD_TYPE = dt.Rows[i][5].ToString();
                            SqlParameter p8 = new SqlParameter("@MOD_TYPE", SqlDbType.VarChar);
                            p8.Value = MOD_TYPE;

                            string CURR = dt.Rows[i][6].ToString();
                            SqlParameter p9 = new SqlParameter("@CURR", SqlDbType.VarChar);
                            p9.Value = CURR;

                            string IMP_GL_Code = dt.Rows[i][7].ToString();
                            SqlParameter p10 = new SqlParameter("@IMP_GL_Code", SqlDbType.VarChar);
                            p10.Value = IMP_GL_Code;

                            string IMP_GL_Sub_Code = dt.Rows[i][8].ToString();
                            SqlParameter p11 = new SqlParameter("@IMP_GL_Sub_Code", SqlDbType.VarChar);
                            p11.Value = IMP_GL_Sub_Code;

                            string IMP_Transaction_Number = dt.Rows[i][9].ToString();
                            SqlParameter p12 = new SqlParameter("@IMP_Transaction_Number", SqlDbType.VarChar);
                            p12.Value = IMP_Transaction_Number;

                            string IMP_Transaction_Sub_Number = dt.Rows[i][10].ToString();
                            SqlParameter p13 = new SqlParameter("@IMP_Transaction_Sub_Number", SqlDbType.VarChar);
                            p13.Value = IMP_Transaction_Sub_Number;

                            string Transaction_REF_Number = dt.Rows[i][12].ToString();
                            SqlParameter p14 = new SqlParameter("@Transaction_REF_Number", SqlDbType.VarChar);
                            p14.Value = Transaction_REF_Number;

                            string ORIGINAL_AMOUNT = dt.Rows[i][41].ToString();
                            SqlParameter p15 = new SqlParameter("@ORIGINAL_AMOUNT", SqlDbType.VarChar);
                            p15.Value = ORIGINAL_AMOUNT;

                            string OCA_INVOICE_AMOUNT = dt.Rows[i][42].ToString();
                            SqlParameter p16 = new SqlParameter("@OCA_INVOICE_AMOUNT", SqlDbType.VarChar);
                            p16.Value = OCA_INVOICE_AMOUNT;

                            string DEAL_DT = dt.Rows[i][56].ToString();
                            SqlParameter p17 = new SqlParameter("@DEAL_DT", SqlDbType.VarChar);
                            p17.Value = DEAL_DT;

                            string VALUE_DT = dt.Rows[i][57].ToString();
                            SqlParameter p18 = new SqlParameter("@VALUE_DT", SqlDbType.VarChar);
                            p18.Value = VALUE_DT;

                            string AVIDENCE_RECIEVE_DT = dt.Rows[i][58].ToString();
                            SqlParameter p19 = new SqlParameter("@AVIDENCE_RECIEVE_DT", SqlDbType.VarChar);
                            p19.Value = AVIDENCE_RECIEVE_DT;

                            string SHIPMENT_DT = dt.Rows[i][60].ToString();
                            SqlParameter p20 = new SqlParameter("@SHIPMENT_DT", SqlDbType.VarChar);
                            p20.Value = SHIPMENT_DT;

                            string SETTLEMENT_DT = dt.Rows[i][61].ToString();
                            SqlParameter p21 = new SqlParameter("@SETTLEMENT_DT", SqlDbType.VarChar);
                            p21.Value = SETTLEMENT_DT;

                            string _userName = Session["userName"].ToString().Trim();
                            SqlParameter p22 = new SqlParameter("@adduser", SqlDbType.VarChar);
                            p22.Value = _userName;

                            string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                            SqlParameter p23 = new SqlParameter("@adddate", SqlDbType.VarChar);
                            p23.Value = _uploadingDate;

                            TF_DATA objDataInput = new TF_DATA();

                            string qryInput = "TF_RET_Import_Input_File_Insert";
                            string dtInput = objDataInput.SaveDeleteData(qryInput, _adcode, p24, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21);

                        }
                    }
                    string qryInput1 = "rptRET_Validate_Input_Import_File";
                    DataTable dtInput1 = objData_Check.getData(qryInput1, _adcode);
                    if (dtInput1.Rows.Count > 0)
                    {
                        string script = "window.open('View_rptRRETURN_ValidationInputInvoiceFile.aspx?PageHeader=Validate Imput Invoice File&frm=" + txtFromDate.Text + "&to=" + txtToDate.Text + "&ADCode=" + ddlBranch.SelectedItem.Value + "','_blank','height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Error Record.')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Record In Input File.')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invalid File Format. Check Excel File.')", true);
            }
        }
        catch (Exception Ex)
        {
            SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
            ADCODE.Value = ddlBranch.SelectedValue.ToString();

            SqlParameter MENUNAME = new SqlParameter("@MENUNAME", SqlDbType.VarChar);
            MENUNAME.Value = "Data Validation Of Input Upload File";

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