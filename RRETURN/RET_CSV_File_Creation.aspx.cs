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
using System.Text.RegularExpressions;

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

            // 🔴 OLD
            // string Branchname = ddlBranch.SelectedItem.ToString().Trim();

            string Branchname = ddlBranch.SelectedItem.ToString().Trim();
            string safeBranch = Regex.Replace(Branchname, @"[^a-zA-Z0-9]", "");
            string safeAdCode = Regex.Replace(ddlBranch.SelectedItem.Value, @"[^a-zA-Z0-9]", "");


            todate = System.Text.RegularExpressions.Regex.Replace(todate, @"[^0-9/]", ""); // allow only date chars
            DateTime parsedDate = DateTime.ParseExact(todate, "dd/MM/yyyy", null);
            string datePart = parsedDate.ToString("ddMMyyyy");
            string basePath = Server.MapPath("~/TF_GeneratedFiles/RRETURN/DataCheck/");
            string folderName = "BR_" + safeBranch + "DataCheck";
            // 🔴 OLD
            // _directoryPath = Server.MapPath("~/TF_GeneratedFiles/RRETURN/DataCheck/BR_" + Branchname.Replace(" ", "") + "DataCheck");
            _directoryPath = Path.Combine(basePath, folderName);
            _directoryPath = Path.GetFullPath(Path.Combine(basePath, folderName));
            // 🔐 PATH TRAVERSAL CHECK
            string fullBasePath = Path.GetFullPath(basePath);
            string fullTargetPath = Path.GetFullPath(_directoryPath);

            if (!fullTargetPath.StartsWith(fullBasePath, StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("Invalid path detected.");

            }

            // 🔴 OLD
            // _strAdCode = "DataCheck_" + ddlBranch.SelectedItem.Value + "_" + datePart;

            _strAdCode = "DataCheck_" + safeAdCode + "_" + datePart;
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            SqlParameter p1 = new SqlParameter("@Branch", SqlDbType.VarChar);
            p1.Value = Branchname;

            SqlParameter p2 = new SqlParameter("@startdate", SqlDbType.VarChar);
            p2.Value = documentDate.ToString("MM/dd/yyyy");

            SqlParameter p3 = new SqlParameter("@enddate", SqlDbType.VarChar);
            p3.Value = documentDate1.ToString("MM/dd/yyyy");

            SqlParameter p4 = new SqlParameter("@AdCode", SqlDbType.VarChar);
            p4.Value = ddlBranch.SelectedItem.Value;

            TF_DATA obj = new TF_DATA();
            DataTable dt = obj.getData("TF_RET_CSV_File_Generate", p1, p2, p3, p4);

            // 🔴 OLD
            // string _filePath = _directoryPath + "/" + _strAdCode + ".CSV";

            string _filePath = Path.GetFullPath(Path.Combine(_directoryPath, _strAdCode + ".CSV"));

            StreamWriter sw = File.CreateText(_filePath);

            string _strHeader = "ADCODE,BRANCH NAME,...,Settlement_Date";
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

                lblqename.Text = _strAdCode;
                lnkEDownload.Visible = true;

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", "alert('Files Created Successfully.')", true);
                labelMessage.Text = "Files Created Successfully.";
            }
            else
            {
                lblqename.Text = "";
                lnkEDownload.Visible = false;
                labelMessage.Text = "Records Not Found.";
            }

            sw.Close();
        }
        catch (Exception)
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
    // Download
    protected void lnkEDownload_Click(object sender, EventArgs e)
    {
        string Branchname = ddlBranch.SelectedItem.ToString().Trim();

        // 🔴 OLD
        // Branchname.Replace(" ", "")

        string safeBranch = Regex.Replace(Branchname, @"[^a-zA-Z0-9]", "");
        string safeAdCode = Regex.Replace(ddlBranch.SelectedItem.Value, @"[^a-zA-Z0-9]", "");

        string _todate = txtToDate.Text.Trim();
        string datePart = _todate.Substring(0, 2) + _todate.Substring(3, 2) + _todate.Substring(6, 4);

        string fileName = "DataCheck_" + safeAdCode + "_" + datePart + ".CSV";

        string basePath = Server.MapPath("~/TF_GeneratedFiles/RRETURN/DataCheck/");
        string folderPath = Path.Combine(basePath, "BR_" + safeBranch + "DataCheck");

        if (!Path.GetFullPath(folderPath).StartsWith(Path.GetFullPath(basePath)))
            throw new Exception("Invalid path detected.");

        string fullPath = Path.Combine(folderPath, fileName);

        lblqename.Text = fileName;

        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
        Response.TransmitFile(fullPath);
        Response.End();
    }
}