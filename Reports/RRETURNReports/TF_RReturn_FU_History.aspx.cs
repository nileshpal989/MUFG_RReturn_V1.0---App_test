using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Net;

public partial class Reports_RRETURNReports_TF_RReturn_FU_History : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            clearControls();
            txtFromDate.Focus();
            fillBranch();
            ddlBranch.SelectedValue = Session["userADCode"].ToString();
            string header = Request.QueryString["PageHeader"];
            if (!string.IsNullOrEmpty(header))
            {
                PageHeader.Text = Server.HtmlEncode(header);
            }
            txtFromDate.Text = Session["FrRelDt"].ToString();
            txtToDate.Text = Session["ToRelDt"].ToString();
            btnSave.Attributes.Add("onclick", "return validateSave();");
        }
    }
    protected void clearControls()
    {
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtFromDate.Focus();
        labelMessage.Text = "";
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
    }
    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        if (dt.Rows.Count > 0)
        {
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            TF_DATA objgetdata = new TF_DATA();
            String Query = "RET_FU_History";
            SqlParameter P1 = new SqlParameter("@FR_FORTNIGHT_DT", txtFromDate.Text.Trim());
            SqlParameter P2 = new SqlParameter("@TO_FORTNIGHT_DT", txtToDate.Text.Trim());
            SqlParameter P3 = new SqlParameter("@Adcode", ddlBranch.SelectedValue);
            SqlParameter P4 = new SqlParameter("@docNo", "");
            DataTable dt = objgetdata.getData(Query, P1, P2, P3, P4);
            if (dt.Rows.Count > 0)
            {
                ExportToExcel(dt);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Data Found.')", true);
            }
        }
        catch (Exception Ex)
        {
            SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
            ADCODE.Value = ddlBranch.SelectedValue.ToString();

            SqlParameter MENUNAME = new SqlParameter("@MENUNAME", SqlDbType.VarChar);
            MENUNAME.Value = "File Upload History";

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

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Page contains error, Kindly Check The Error Log')", true);
        }
    }
    //protected void ExportToExcel(DataTable dt)
    //{
    //    try
    //    {
    //        IWorkbook workbook = new XSSFWorkbook();
    //        ISheet sheet = workbook.CreateSheet("Report");
    //        // Header row
    //        IRow headerRow = sheet.CreateRow(0);
    //        for (int i = 0; i < dt.Columns.Count; i++)
    //        {
    //            headerRow.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);

    //        }
    //        // Data rows
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            IRow row = sheet.CreateRow(i + 1);

    //            for (int j = 0; j < dt.Columns.Count; j++)
    //            {
    //                row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
    //            }
    //        }
    //        // Auto size columns
    //        for (int i = 0; i < dt.Columns.Count; i++)
    //        {
    //            sheet.AutoSizeColumn(i);
    //        }
    //        // Download file
    //        using (MemoryStream ms = new MemoryStream())
    //        {
    //            //workbook.Write(ms);
    //            //byte[] bytes = ms.ToArray();

    //            //Response.Clear();
    //            //Response.Buffer = true;
    //            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //            //Response.AddHeader("content-disposition", "attachment;filename=File Upload History.xlsx");
    //            //Response.BinaryWrite(bytes);
    //            //Response.Flush();
    //            //HttpContext.Current.ApplicationInstance.CompleteRequest();
    //            workbook.Write(ms);

    //            Response.Clear();
    //            Response.Buffer = true;
    //            Response.AddHeader("content-disposition", "attachment;filename=Report.xlsx");
    //            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    //            Response.BinaryWrite(ms.ToArray());
    //            Response.End();
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
    //        ADCODE.Value = ddlBranch.SelectedValue.ToString();

    //        SqlParameter MENUNAME = new SqlParameter("@MENUNAME", SqlDbType.VarChar);
    //        MENUNAME.Value = "File Upload History";

    //        SqlParameter IPAddress = new SqlParameter("@IPAddress", SqlDbType.VarChar);
    //        IPAddress.Value = GetIPAddress();

    //        SqlParameter URL = new SqlParameter("@URL", SqlDbType.VarChar);
    //        URL.Value = HttpContext.Current.Request.Url.AbsoluteUri;

    //        SqlParameter TYPE = new SqlParameter("@TYPE", SqlDbType.VarChar);
    //        TYPE.Value = Ex.GetType().Name.ToString();

    //        SqlParameter Message = new SqlParameter("@Message", SqlDbType.VarChar);
    //        Message.Value = Ex.Message;

    //        SqlParameter StackTrace = new SqlParameter("@StackTrace", SqlDbType.VarChar);
    //        StackTrace.Value = Ex.StackTrace;

    //        SqlParameter Source = new SqlParameter("@Source", SqlDbType.VarChar);
    //        Source.Value = Ex.Source;

    //        SqlParameter TargetSite = new SqlParameter("@TargetSite", SqlDbType.VarChar);
    //        TargetSite.Value = Ex.TargetSite.ToString();

    //        SqlParameter DATETIME = new SqlParameter("@DATETIME", SqlDbType.VarChar);
    //        DATETIME.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

    //        SqlParameter UserName = new SqlParameter("@UserName", SqlDbType.VarChar);
    //        UserName.Value = Session["userName"].ToString().Trim();

    //        TF_DATA objDataInput = new TF_DATA();
    //        string qryError = "TF_RET_ErrorException";
    //        string dtInput1 = objDataInput.SaveDeleteData(qryError, ADCODE, MENUNAME, IPAddress, URL, Message, StackTrace, Source, TargetSite, DATETIME, TYPE, UserName);

    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Page contains error, Kindly Check The Error Log')", true);
    //    }
    //}

    protected void ExportToExcel(DataTable dt)
    {
        try
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Report");

            // ===== STYLE (ADD KIYA HAI) =====
            ICellStyle headerStyle = workbook.CreateCellStyle();
            IFont headerFont = workbook.CreateFont();
            headerFont.IsBold = true;
            headerStyle.SetFont(headerFont);

            headerStyle.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            headerStyle.FillPattern = FillPattern.SolidForeground;

            ICellStyle dataStyle = workbook.CreateCellStyle();
            dataStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            dataStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            dataStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            dataStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

            // Header row
            IRow headerRow = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = headerRow.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
                cell.CellStyle = headerStyle; // APPLY STYLE
            }

            // Data rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i + 1);

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                    cell.CellStyle = dataStyle; // APPLY STYLE
                }
            }
            // Auto size columns
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sheet.AutoSizeColumn(i);
            }
            // Freeze header (ADD KIYA)
            sheet.CreateFreezePane(0, 1);
            // Download file
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=File Upload History.xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(ms.ToArray());
                Response.End();
            }
        }
        catch (Exception Ex)
        {
            SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
            ADCODE.Value = ddlBranch.SelectedValue.ToString();

            SqlParameter MENUNAME = new SqlParameter("@MENUNAME", SqlDbType.VarChar);
            MENUNAME.Value = "File Upload History";

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

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Page contains error, Kindly Check The Error Log')", true);
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