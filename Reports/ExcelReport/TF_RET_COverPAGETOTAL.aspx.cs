using OboutInc.Calendar2;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_ExcelReport_TF_RET_COverPAGETOTAL : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            clearControls();
            txtFromDate.Focus();
            fillBranch();
            ddlBranch.SelectedValue = Session["userADCode"].ToString();
            //ddlBranch.Enabled = false;
            txtFromDate.Text = Session["FrRelDt"].ToString();
            txtToDate.Text = Session["ToRelDt"].ToString();
            //txtFromDate.Text = "01/10/2022";
            //txtToDate.Text = "15/10/2022";
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
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            string _query = "TF_rptRRETURN_CoverPageTotal_Excel";
            TF_DATA objget = new TF_DATA();
            DataTable dt = new DataTable();

            string frmdate = "";
            string todate = "";
            try
            {
                DateTime parsedto = DateTime.ParseExact(txtToDate.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime parsedfrom = DateTime.ParseExact(txtFromDate.Text.Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                todate = parsedto.ToString("yyyy-MM-dd");
                frmdate = parsedfrom.ToString("yyyy-MM-dd");
            }
            catch (FormatException ex)
            {
                labelMessage.Text = "Date format is not valid: " + txtToDate.Text;
                return;
            }


            //string todate = Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd");


            SqlParameter[] param = new SqlParameter[]
            {
            new SqlParameter("@startdate", SqlDbType.Date) { Value =frmdate},
            new SqlParameter("@enddate", SqlDbType.Date) { Value = todate},
            new SqlParameter("@Branch", SqlDbType.VarChar, 20) { Value = ddlBranch.SelectedItem.Text}
            };


            dt = objget.getData(_query, param);
            if (dt.Rows.Count > 0)
            {
                using (ExcelPackage excel = new ExcelPackage())
                {
                    ExcelWorksheet ws = excel.Workbook.Worksheets.Add("CoverPage Report");
                    int row = 1;
                    //var currencyGroups = dt.AsEnumerable()
                    //                       .GroupBy(r => r.Field<string>("CURR"));
                    var currencyGroups = dt.AsEnumerable()
                       .OrderBy(r => r.Field<string>("CURR")) // sort by CURR ascending
                       .GroupBy(r => r.Field<string>("CURR"));
                    
                    ws.Cells[row, 1].Value = "MUFG R-Return";
                    ws.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    ws.Cells[row, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    ws.Cells[row, 1].Style.Font.Bold = true;
                    ws.Cells[row, 2, row, 5].Merge = true;
                    ws.Cells[row, 2].Value = "From " + txtFromDate.Text + " To " + txtToDate.Text;
                    ws.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    ws.Cells[row, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    ws.Cells[row, 2].Style.Font.Bold = true;
                    row++;
                    ws.Cells[row, 1].Value = "Final";
                    ws.Cells[row, 2].Value = "Sales Purpose Code/Description";
                    ws.Cells[row, 3].Value = "Sales Amount";
                    ws.Cells[row, 4].Value = "Purchase Purpose Code/Description";
                    ws.Cells[row, 5].Value = "Purchase Amount";
                    ws.Row(row).Style.Font.Bold = true;
                    //ws.Cells[row, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //ws.Cells[row, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //ws.Cells[row, 3].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);
                    //ws.Cells[row, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);

                    row++;
                    foreach (var currGroup in currencyGroups)
                    {
                        // Header row
                        row++;
                        ws.Cells[row, 1].Value = currGroup.Key;
                        ws.Cells[row, 1].Style.Font.Bold = true;
                        //ws.Cells[row, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //ws.Cells[row, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);

                        //row++;
                        decimal totalSales = 0;
                        decimal totalPurchase = 0;
                        string[] excludedPurposeCodes = new[] { "P2088", "P9999", "S2088", "S9999", "P2199", "S2199" };
                        foreach (var dr in currGroup)
                        {
                            string purpose = dr["PURPOSE_CODE"].ToString();
                            string description = dr["DESCRIPTION"].ToString();
                            decimal amount = Convert.ToDecimal(dr["FC_AMOUNT"]);
                            //ws.Cells[row, 1].Value = purpose;
                            //ws.Cells[row, 2].Value = description;
                            // Skip P9999 and S9999
                            if (excludedPurposeCodes.Contains(purpose, StringComparer.OrdinalIgnoreCase))
                                continue;
                            if (purpose.StartsWith("S"))
                                ws.Cells[row, 2].Value = purpose + " (" + description + ")";
                            else if (purpose.StartsWith("P"))
                                ws.Cells[row, 4].Value = purpose + " (" + description + ")";

                            if (purpose.StartsWith("S"))
                            {
                                ws.Cells[row, 3].Value = amount;
                                totalSales += amount;
                            }
                            else if (purpose.StartsWith("P"))
                            {
                                ws.Cells[row, 5].Value = amount;
                                totalPurchase += amount;
                            }
                            //ws.Cells[row, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            //ws.Cells[row, 3].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.MediumPurple);
                            //ws.Cells[row, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            //ws.Cells[row, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.MediumPurple);
                            row++;
                        }
                        // Total Row
                        ws.Cells[row, 2].Value = "Total";
                        ws.Cells[row, 4].Value = "Total";
                        ws.Cells[row, 3].Value = totalSales;
                        ws.Cells[row, 5].Value = totalPurchase;
                        ws.Row(row).Style.Font.Bold = true;
                        //ws.Row(row).Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //ws.Row(row).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);
                        row++;

                        decimal openAmount = currGroup
                        .Where(r =>
                            r.Field<string>("PURPOSE_CODE").Equals("P2088", StringComparison.OrdinalIgnoreCase) ||
                            r.Field<string>("PURPOSE_CODE").Equals("S2088", StringComparison.OrdinalIgnoreCase))
                        .Sum(r => Math.Abs(Convert.ToDecimal(r["FC_AMOUNT"])));
                
                        decimal closingBalanceAmount = currGroup
                        .Where(r =>
                            r.Field<string>("PURPOSE_CODE").Equals("P2199", StringComparison.OrdinalIgnoreCase) ||
                            r.Field<string>("PURPOSE_CODE").Equals("S2199", StringComparison.OrdinalIgnoreCase))
                        .Sum(r => Math.Abs(Convert.ToDecimal(r["FC_AMOUNT"])));

                        // Add rows: OPEN, Purch, Sales, Closing Balance
                        ws.Cells[row, 4].Value = "Opening Balance";
                        ws.Cells[row, 5].Value = openAmount;
                        //ws.Cells[row, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //ws.Cells[row, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.MediumPurple);
                        row++;
                        ws.Cells[row, 4].Value = "Purchase";
                        ws.Cells[row, 5].Value = totalPurchase;
                        row++;
                        ws.Cells[row, 4].Value = "Sales";
                        ws.Cells[row, 5].Value = totalSales;
                        row++;
                        ws.Cells[row, 4].Value = "Closing Balance";
                        ws.Cells[row, 5].Value = closingBalanceAmount;
                        ws.Row(row).Style.Font.Bold = true;
                        row++;

                        row += 1; // Leave 2 blank rows between currencies
                    }

                    // AutoFit columns
                    ws.Cells[ws.Dimension.Address].AutoFitColumns();

                    // Output Excel file
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;  filename=CoverPageReport.xlsx");
                    Response.BinaryWrite(excel.GetAsByteArray());
                    Response.End();
                }
            }
            else
            {
                labelMessage.Text = "Data Not Found";
            }
        }
        catch (Exception ex)
        {
            labelMessage.Text = "Exception : " + ex.Message;
        }
    }
}