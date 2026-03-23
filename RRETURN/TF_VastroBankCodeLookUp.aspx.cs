using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
public partial class TF_VastroBankCodeLookUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGrid();
        }
    }
    protected void fillGrid()
    {
        string search = "";
        string Country = Request.QueryString["CountryID"].ToString();
        //string Branch = Request.QueryString["Branch"];
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;
        SqlParameter p2 = new SqlParameter("@countrycode", SqlDbType.VarChar);
        p2.Value = Country;
        txtSearch.Text = search;
        string _query = "TF_GetVastroBankCodeList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridViewBankList.DataSource = dt.DefaultView;
            GridViewBankList.DataBind();
            GridViewBankList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;
        }
        else
        {
            GridViewBankList.Visible = false;
            rowGrid.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void GridViewBankList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    protected void GridViewBankList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCountryID = new Label();
            lblCountryID = (Label)e.Row.FindControl("lblBankCode");
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                //string pageurl = "", hNo = Request.QueryString["hNo"].ToString();
                //pageurl = "window.opener.selectCountry('" + lblCountryID.Text + "','" + hNo + "');window.opener.EndRequest();window.close();";
                string pageurl = "window.opener.selectVastroBankCode('" + lblCountryID.Text + "');window.opener.EndRequest();window.close();";
                if (i != 6)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void GridViewBankList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TabIndex = -1;
            e.Row.Attributes["onmouseover"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onmouseout"] = string.Format("javascript:DisSelectRow(this, {0});", e.Row.RowIndex);
            e.Row.ToolTip = "Click to select row.";
        }
    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        string search = txtSearch.Text.Trim();
        string Country = Request.QueryString["CountryID"].ToString();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;
        SqlParameter p2 = new SqlParameter("@countrycode", SqlDbType.VarChar);
        p2.Value = Country;
        string _query = "TF_GetVastroBankCodeList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridViewBankList.DataSource = dt.DefaultView;
            GridViewBankList.DataBind();
            GridViewBankList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;
        }
        else
        {
            GridViewBankList.Visible = false;
            rowGrid.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
}