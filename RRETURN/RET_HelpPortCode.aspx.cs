using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class RRETURN_RET_HelpPortCode : System.Web.UI.Page
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
        SqlParameter p1 = new SqlParameter("@Search", SqlDbType.VarChar);
        p1.Value = search;
        string _query = "TF_RET_HelpPortCode";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridViewPortCodeList.DataSource = dt.DefaultView;
            GridViewPortCodeList.DataBind();
            GridViewPortCodeList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;
        }
        else
        {
            GridViewPortCodeList.Visible = false;
            rowGrid.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void GridViewPortCodeList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //string result = "";
    }
    protected void GridViewPortCodeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCommodityID = new Label();
            lblCommodityID = (Label)e.Row.FindControl("lblCommodityID");
            Label lblCommodityDesc = new Label();
            lblCommodityDesc = (Label)e.Row.FindControl("lblCommodityDesc");
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "";
                pageurl = "window.opener.selectPort('" + lblCommodityID.Text + "','" + lblCommodityDesc.Text + "');window.opener.EndRequest();window.close();";
                if (i != 2)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void GridViewPortCodeList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TabIndex = -1;
            //e.Row.Attributes["onmouseover"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            //e.Row.Attributes["onmouseout"] = string.Format("javascript:DisSelectRow(this, {0});", e.Row.RowIndex);
            e.Row.ToolTip = "Click to select row.";
        }
    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@Search", SqlDbType.VarChar);
        p1.Value = search;
        string _query = "TF_RET_HelpPortCode";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridViewPortCodeList.DataSource = dt.DefaultView;
            GridViewPortCodeList.DataBind();
            GridViewPortCodeList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;
        }
        else
        {
            GridViewPortCodeList.Visible = false;
            rowGrid.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
}