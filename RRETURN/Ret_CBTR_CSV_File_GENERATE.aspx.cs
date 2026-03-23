using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class Ret_CBTR_CSV_File_GENERATE : System.Web.UI.Page
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
                ddlBranch.Enabled = false;
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
            Error = CBTR_CSV_GENERATE();
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

                path = "file://" + _serverName + "/TF_GeneratedFiles/RRETURN/CBTR/BR_" + Branchname.Replace(" ", "") + "_CBTRFile";
                link = "/TF_GeneratedFiles/RRETURN/CBTR/BR_" + Branchname.Replace(" ", "") + "_CBTRFile";
                
                string script = "alert('Files Created Successfully on " + _serverName + " in " + link + " ')";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
                labelMessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
            }
        }
        catch (Exception ex)
        {
            labelMessage.Text = "Exception: " + ex.Message;
        }
    }
    public string CBTR_CSV_GENERATE()
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

            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/RRETURN/CBTR/BR_" + Branchname.Replace(" ", "") + "_CBTRFile");
            
            _strAdCode = "CBTR_" + ddlBranch.SelectedItem.Value + "_" +todate.Substring(3, 2) + todate.Substring(6, 4);
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
            SqlParameter p4 = new SqlParameter("@ADCode", SqlDbType.VarChar);
            p4.Value = ddlBranch.SelectedItem.Value;
            #region CSVFile
            //REM FOR CSV FILE
            TF_DATA obj = new TF_DATA();
            string _qry = "TF_RET_CBTR_CSV_Generate";
            DataTable dt = obj.getData(_qry, p1, p2, p3,p4);
            string _filePath = _directoryPath + "/" + _strAdCode + ".CSV";
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            string _strHeader = "REF_NO,BRANCH NAME,TRANSACTION_DT,TRANS_REF_NO,TRANSACTION_TYPE,INSTRUMENT_TYPE,TRANS_INSTITUTE_NAME,TRANS_INSTITUTE_REF_NO,"
                + "TRANS_STATE_CODE,TRANS_COUNTRY_CODE,INSTRUMENT_COUNTRY_CODE,FC_AMOUNT,EXCH_RATE,INR_AMOUNT,CURR,PURPOSE_ID,RISK_RATING,MOD_TYPE,CUSTOMER_NAME,"
                + "CUSTOMER_ID,CUSTOMER_ADDRESS";
            sw.WriteLine(_strHeader);
            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string _strADCODE = dt.Rows[j]["ADCODE"].ToString().Trim();
                    sw.Write(_strADCODE + ",");
                    string _strBRANCHNAME = ddlBranch.SelectedItem.ToString().Trim();
                    sw.Write(_strBRANCHNAME + ",");
                    string _strTRANSACTION_DT = dt.Rows[j]["TRANSACTION_DT"].ToString();
                    sw.Write(_strTRANSACTION_DT + ",");
                    string _strDOCNO = dt.Rows[j]["DOCNO"].ToString().Trim();
                    sw.Write(_strDOCNO + ",");
                    string _strTRANSTYPE = dt.Rows[j]["TRANSTYPE"].ToString().Trim();
                    sw.Write(_strTRANSTYPE + ",");
                    string _strINSTRUMENT_TYPE = "E";
                    sw.Write(_strINSTRUMENT_TYPE + ",");
                    string _strTRANS_INSTITUTE_NAME = "";
                    sw.Write(_strTRANS_INSTITUTE_NAME + ",");
                    string _strTRANS_INSTITUTE_REF_NO = "";
                    sw.Write(_strTRANS_INSTITUTE_REF_NO + ",");
                    string _strTRANS_STATE_CODE = "XX";
                    sw.Write(_strTRANS_STATE_CODE + ",");
                    string _strTransCountryCode = dt.Rows[j]["TransCountryCode"].ToString();
                    sw.Write(_strTransCountryCode + ",");
                    string _strINSTRUMNETCOUNTRYCODE = dt.Rows[j]["TransCountryCode"].ToString();
                    sw.Write(_strINSTRUMNETCOUNTRYCODE + ",");
                    string _strFC_AMOUNT = dt.Rows[j]["FC_AMOUNT"].ToString();
                    sw.Write(_strFC_AMOUNT + ",");
                    string _strEXRT = dt.Rows[j]["EXRT"].ToString();
                    sw.Write(_strEXRT + ",");
                    string _strAMOUNT_IN_INR = dt.Rows[j]["AMOUNT_IN_INR"].ToString();
                    sw.Write(_strAMOUNT_IN_INR + ",");
                    string _strCURR = dt.Rows[j]["CURR"].ToString();
                    sw.Write(_strCURR + ",");
                    string _strPURPOSEID = dt.Rows[j]["PURPOSEID"].ToString();
                    sw.Write(_strPURPOSEID + ",");
                    string _strRISK_RATING = "T3";
                    sw.Write(_strRISK_RATING + ",");
                    string _strMOD_TYPE = dt.Rows[j]["MOD_TYPE"].ToString();
                    sw.Write(_strMOD_TYPE + ",");
                    string _strCUSTOMER_NAME = dt.Rows[j]["CUSTOMER_NAME"].ToString();
                    sw.Write(_strCUSTOMER_NAME + ",");
                    string _strCUSTOMER_ID = dt.Rows[j]["CUSTOMER_ID"].ToString();
                    sw.Write(_strCUSTOMER_ID + ",");
                    string _strCUSTOMER_ADDRESS = dt.Rows[j]["CUSTOMER_ADDRESS"].ToString();
                    sw.WriteLine(_strCUSTOMER_ADDRESS + ",");
                }
            }
            else
            {
                labelMessage.Text = "Records Not Found.";
                //ErrorMessage = "Records Not Found For RES Data.";
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
            //REM END
            #endregion
        }
        catch (Exception ex)
        {
            labelMessage.Text = "Exception: " + ex.Message;
            ErrorMessage = "Exception: " + ex.Message;
        }
        return ErrorMessage;
    }
}