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

public partial class TF_PurposeCodeLookUp2 : System.Web.UI.Page
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
        // string search = "";
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";

        string _query = "TF_GetPurposeCodeMasterList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridViewPurCodeList.DataSource = dt.DefaultView;
            GridViewPurCodeList.DataBind();
            GridViewPurCodeList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;
        }
        else
        {
            GridViewPurCodeList.Visible = false;
            rowGrid.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void GridViewPurCodeList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //    string result = "";
    }
    protected void GridViewPurCodeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblpurposeID = new Label();
            lblpurposeID = (Label)e.Row.FindControl("lblpurposeID");
            int i = 0;
            string pageurl = "", hNo = "";
            foreach (TableCell cell in e.Row.Cells)
            {
                hNo = Request.QueryString["hNo"].ToString();
                pageurl = "window.opener.selectPurpose('" + lblpurposeID.Text + "','" + hNo + "');window.opener.EndRequest();window.close();";

                if (i != 6)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

    protected void GridViewPurCodeList_RowCreated(object sender, GridViewRowEventArgs e)
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
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;
        string _query = "TF_GetPurposeCodeMasterList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridViewPurCodeList.DataSource = dt.DefaultView;
            GridViewPurCodeList.DataBind();
            GridViewPurCodeList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;
        }
        else
        {
            GridViewPurCodeList.Visible = false;
            rowGrid.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
}