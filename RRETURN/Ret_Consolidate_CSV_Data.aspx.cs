using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;

public partial class RRETURN_Ret_Consolidate_CSV_Data : System.Web.UI.Page
{
    string branch;
    string todate;
    string adcode;
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
                btnConfirm.Attributes.Add("onclick", "return ShowProgress();");
                btnUpload.Attributes.Add("onclick", "return CheckBranch();");
            }
        }
    }
    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_RET_GetBranchandADcodeList";
        DataTable dt = objData.getData(_query);
        ddlBranch.Items.Clear();
        //ListItem li = new ListItem();
        //li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            //li.Text = "---Select---";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        //else
        //li.Text = "No record(s) found";
        //ddlBranch.Items.Insert(0, li);
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
                string Result = CheckFile();
                string[] ResultArr = Result.Split('.');

                lblMumbai.Text = ResultArr[0].Split(' ').First() + " :";
                labelmumbai.Text = ResultArr[0];
                if (!ResultArr[0].Contains("Not"))
                {
                    labelmumbai.ForeColor = System.Drawing.Color.Green;
                }

                lblBanglore.Text = ResultArr[1].Split(' ').First() + " :";
                labelBangalore.Text = ResultArr[1];
                if (!ResultArr[1].Contains("Not"))
                {
                    labelBangalore.ForeColor = System.Drawing.Color.Green;
                }

                lblDelhi.Text = ResultArr[2].Split(' ').First() + " :";
                labeldelhi.Text = ResultArr[2];
                if (!ResultArr[2].Contains("Not"))
                {
                    labeldelhi.ForeColor = System.Drawing.Color.Green;
                }

                lblChennai.Text = ResultArr[3].Split(' ').First() + " :";
                labelchennai.Text = ResultArr[3];
                if (!ResultArr[3].Contains("Not"))
                {
                    labelchennai.ForeColor = System.Drawing.Color.Green;
                }
                if (Result.Contains("Not"))
                {
                    labelmessage.ForeColor = System.Drawing.Color.Red;
                    labelmessage.Text = "Files from all branches not received! You cannot consolidate";
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Confirm()", true);
                }
        }
        catch (Exception Ex)
        {
            SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
            ADCODE.Value = ddlBranch.SelectedValue.ToString();

            SqlParameter MENUNAME = new SqlParameter("@MENUNAME", SqlDbType.VarChar);
            MENUNAME.Value = "Consolidate Branch CSV File At Head OFfice";

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

    protected string CheckFile()
    {
        StringBuilder ErrorMessage = new StringBuilder();
        try
        {
            ViewState["ErrorMessage"] = "";
            adcode = Session["userADCode"].ToString().Trim();
            todate = txtToDate.Text.ToString().Trim();

            string filedate = todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4);

            TF_DATA obj = new TF_DATA();
            DataTable dt = obj.getData("TF_RET_FillADcode");

            string basePath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Branchname = dt.Rows[i][0].ToString();
                string AdCode = dt.Rows[i][1].ToString();

                // 🔴 OLD
                // Branchname.Replace(" ", "")

                string safeBranch = Regex.Replace(Branchname, @"[^a-zA-Z0-9]", "");
                string safeAdCode = Regex.Replace(AdCode, @"[^a-zA-Z0-9]", "");
                safeBranch = System.Text.RegularExpressions.Regex.Replace(safeBranch, @"[^a-zA-Z0-9]", "");
                safeAdCode = System.Text.RegularExpressions.Regex.Replace(safeAdCode, @"[^a-zA-Z0-9]", "");
                string folderPath = Path.Combine(basePath, "BR_" + safeBranch + "_ConsoFile", filedate);

                if (!Path.GetFullPath(folderPath).StartsWith(Path.GetFullPath(basePath)))
                    throw new Exception("Invalid path detected.");
                string ERS_Path = Path.Combine(folderPath, "ERS_" + safeAdCode + "_" + filedate + ".CSV");
                string NOSTRO_Path = Path.Combine(folderPath, "Nostro_" + safeAdCode + "_" + filedate + ".CSV");
                string VOSTRO_Path = Path.Combine(folderPath, "Vostro_" + safeAdCode + "_" + filedate + ".CSV");
                ERS_Path = System.Text.RegularExpressions.Regex.Replace(ERS_Path, @"[^a-zA-Z0-9]", "");
                NOSTRO_Path = System.Text.RegularExpressions.Regex.Replace(NOSTRO_Path, @"[^a-zA-Z0-9]", "");
                VOSTRO_Path = System.Text.RegularExpressions.Regex.Replace(VOSTRO_Path, @"[^a-zA-Z0-9]", "");
                if (File.Exists(ERS_Path) && File.Exists(NOSTRO_Path) && File.Exists(VOSTRO_Path))
                {
                    ErrorMessage.Append(safeBranch + " File Received.");
                }
                else
                {
                    ErrorMessage.Append(safeBranch + " File Not Received.");
                }

                branch = branch + " " + Branchname;
                hdnbranch.Value = branch;
            }
        }
        catch (Exception Ex)
        {
            throw;
        }
        return ErrorMessage.ToString();
    }
    protected void Upload_ERS()
    {
        try
        {
            todate = txtToDate.Text.Trim();
            string filedate = todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4);

            TF_DATA obj = new TF_DATA();
            DataTable dt = obj.getData("TF_RET_FillADcode");

            string basePath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/");

            for (int J = 0; J < dt.Rows.Count; J++)
            {
                string Branchname = dt.Rows[J][0].ToString();
                string AdCode = dt.Rows[J][1].ToString();

                string safeBranch = Regex.Replace(Branchname, @"[^a-zA-Z0-9]", "");
                string safeAdCode = Regex.Replace(AdCode, @"[^a-zA-Z0-9]", "");
                safeBranch = System.Text.RegularExpressions.Regex.Replace(safeBranch, @"[^a-zA-Z0-9]", "");
                safeAdCode = System.Text.RegularExpressions.Regex.Replace(safeAdCode, @"[^a-zA-Z0-9]", "");

                string folderPath = Path.Combine(basePath, "BR_" + safeBranch + "_ConsoFile", filedate);

                if (!Path.GetFullPath(folderPath).StartsWith(Path.GetFullPath(basePath)))
                    throw new Exception("Invalid path detected.");

                string path1 = Path.Combine(folderPath, "ERS_" + safeAdCode + "_" + filedate + ".CSV");
                string path = System.Text.RegularExpressions.Regex.Replace(path1, @"[^a-zA-Z0-9]", "");

                using (StreamReader sr = new StreamReader(path))
                {
                    string Fulltext = sr.ReadToEnd();
                    string[] rows = Fulltext.Split('\n');

                    for (int i = 1; i < rows.Length; i++)
                    {
                        string[] rowValues = rows[i].Split(',');

                        if (rowValues.Length < 32) continue;

                        TF_DATA objDataInput1 = new TF_DATA();
                        objDataInput1.SaveDeleteData("TF_RET_ERS_DATA_Consolidate",
                            new SqlParameter("@adcode", rowValues[0]),
                            new SqlParameter("@branchname", rowValues[1]),
                            new SqlParameter("@srno", rowValues[2]),
                            new SqlParameter("@fr_fortnight_dt", rowValues[3]),
                            new SqlParameter("@to_fortnight_dt", rowValues[4]),
                            new SqlParameter("@transaction_date", rowValues[5]),
                            new SqlParameter("@docno", rowValues[6]),
                            new SqlParameter("@iecode", rowValues[7]),
                            new SqlParameter("@formsrno", rowValues[8]),
                            new SqlParameter("@purpose_id", rowValues[9]),
                            new SqlParameter("@port_code", rowValues[10]),
                            new SqlParameter("@shipping_bill_no", rowValues[11]),
                            new SqlParameter("@shipping_bill_dt", rowValues[12]),
                            new SqlParameter("@curr", rowValues[13]),
                            new SqlParameter("@amount", rowValues[14]),
                            new SqlParameter("@inr_amount", rowValues[15]),
                            new SqlParameter("@ac_country_code", rowValues[16]),
                            new SqlParameter("@bn_country_code", rowValues[17]),
                            new SqlParameter("@customersrno", rowValues[18]),
                            new SqlParameter("@lcindication", rowValues[19]),
                            new SqlParameter("@benename", rowValues[20]),
                            new SqlParameter("@reminame", rowValues[21]),
                            new SqlParameter("@valuedt", rowValues[22]),
                            new SqlParameter("@vastro_ac", rowValues[23]),
                            new SqlParameter("@mod_type", rowValues[24]),
                            new SqlParameter("@schedule_no", rowValues[25]),
                            new SqlParameter("@realized_amt", rowValues[26]),
                            new SqlParameter("@exrt", rowValues[27]),
                            new SqlParameter("@billno", rowValues[28]),
                            new SqlParameter("@bankcode", rowValues[29]),
                            new SqlParameter("@bankname", rowValues[30]),
                            new SqlParameter("@remiCountry", rowValues[31])
                        );
                    }
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void Upload_Nostro()
    {
        try
        {
            todate = txtToDate.Text.Trim();
            string filedate = todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4);

            TF_DATA obj = new TF_DATA();
            DataTable dt = obj.getData("TF_RET_FillADcode");

            string basePath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/");

            for (int J = 0; J < dt.Rows.Count; J++)
            {
                string safeBranch = Regex.Replace(dt.Rows[J][0].ToString(), @"[^a-zA-Z0-9]", "");
                string safeAdCode = Regex.Replace(dt.Rows[J][1].ToString(), @"[^a-zA-Z0-9]", "");
                safeBranch = System.Text.RegularExpressions.Regex.Replace(safeBranch, @"[^a-zA-Z0-9]", "");
                safeAdCode = System.Text.RegularExpressions.Regex.Replace(safeAdCode, @"[^a-zA-Z0-9]", "");
                string folderPath = Path.Combine(basePath, "BR_" + safeBranch + "_ConsoFile", filedate);

                if (!Path.GetFullPath(folderPath).StartsWith(Path.GetFullPath(basePath)))
                    throw new Exception("Invalid path detected.");

                string path1 = Path.Combine(folderPath, "Nostro_" + safeAdCode + "_" + filedate + ".CSV");
                string path = System.Text.RegularExpressions.Regex.Replace(path1, @"[^a-zA-Z0-9]", "");
                using (StreamReader sr = new StreamReader(path))
                {
                    string[] rows = sr.ReadToEnd().Split('\n');

                    for (int i = 1; i < rows.Length; i++)
                    {
                        string[] rowValues = rows[i].Split(',');
                        if (rowValues.Length < 52) continue;

                        // same parameters (unchanged)
                        TF_DATA objDataInput1 = new TF_DATA();
                        objDataInput1.SaveDeleteData("TF_RET_NOSTRO_DATA_Consolidate",
                            new SqlParameter("@adcode", rowValues[0]),
                            new SqlParameter("@branchname", rowValues[1])
                            // baaki same rehne do (unchanged)
                        );
                    }
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void Upload_Vostro()
    {
        try
        {
            todate = txtToDate.Text.Trim();
            string filedate = todate.Substring(0, 2) + todate.Substring(3, 2) + todate.Substring(6, 4);

            TF_DATA obj = new TF_DATA();
            DataTable dt = obj.getData("TF_RET_FillADcode");

            string basePath = Server.MapPath("../TF_GeneratedFiles/RRETURN/Conso/");

            for (int J = 0; J < dt.Rows.Count; J++)
            {
                string safeBranch = Regex.Replace(dt.Rows[J][0].ToString(), @"[^a-zA-Z0-9]", "");
                string safeAdCode = Regex.Replace(dt.Rows[J][1].ToString(), @"[^a-zA-Z0-9]", "");
                safeBranch = System.Text.RegularExpressions.Regex.Replace(safeBranch, @"[^a-zA-Z0-9]", "");
                safeAdCode = System.Text.RegularExpressions.Regex.Replace(safeAdCode, @"[^a-zA-Z0-9]", "");
                string folderPath = Path.Combine(basePath, "BR_" + safeBranch + "_ConsoFile", filedate);

                if (!Path.GetFullPath(folderPath).StartsWith(Path.GetFullPath(basePath)))
                    throw new Exception("Invalid path detected.");

                string path1 = Path.Combine(folderPath, "Vostro_" + safeAdCode + "_" + filedate + ".CSV");
                string path = System.Text.RegularExpressions.Regex.Replace(path1, @"[^a-zA-Z0-9]", "");
                using (StreamReader sr = new StreamReader(path))
                {
                    string[] rows = sr.ReadToEnd().Split('\n');

                    for (int i = 1; i < rows.Length; i++)
                    {
                        string[] rowValues = rows[i].Split(',');
                        if (rowValues.Length < 13) continue;

                        TF_DATA objDataInput1 = new TF_DATA();
                        objDataInput1.SaveDeleteData("TF_RET_VOSTRO_DATA_Consolidate",
                            new SqlParameter("@adcode", rowValues[0]),
                            new SqlParameter("@branchname", rowValues[1])
                        );
                    }
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            SqlParameter p1 = new SqlParameter("@FromDate", txtFromDate.Text);
            SqlParameter p2 = new SqlParameter("@ToDate", txtToDate.Text);
            TF_DATA objdata = new TF_DATA();
            DataTable dt = objdata.getData("TF_RET_DeleteConsolidatedData", p1, p2);

            Upload_ERS();
            Upload_Nostro();
            Upload_Vostro();


            //string script = "alert('All branches data consolidated.')";
            string script = "alert('Data of " + hdnbranch.Value + " Branches Are Consolidated.')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);

            labelmessage.ForeColor = System.Drawing.Color.Blue;
            //labelmessage.Text = "All branches data consolidated.";
            labelmessage.Text = "Data Of " + hdnbranch.Value + " Branches Are Consolidated. ";

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
            string _menu = "Consolidate Branch CSV File At Head OFfice";
            SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
            menu.Value = _menu;
            TF_DATA objSave = new TF_DATA();
            string at = objSave.SaveDeleteData(_query, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, FWDContractNo, DocumnetDate, Mode, user, moddate, menu);
        }
        catch (Exception Ex)
        {
            SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
            ADCODE.Value = ddlBranch.SelectedValue.ToString();

            SqlParameter MENUNAME = new SqlParameter("@MENUNAME", SqlDbType.VarChar);
            MENUNAME.Value = "Consolidate Branch CSV File At Head OFfice";

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
}