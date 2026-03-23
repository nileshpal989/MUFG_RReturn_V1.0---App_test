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

public partial class HelpBranchCategory : System.Web.UI.Page
{
    private string SearchString = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGrid();
        }

    }

    protected void fillGrid()
    {

        SqlParameter p1 = new SqlParameter("@Search", SqlDbType.VarChar);
        p1.Value = txtSearch.Text.Trim();

        string _query = "HelpReportEntityMaster";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridViewUserList.DataSource = dt.DefaultView;
            GridViewUserList.DataBind();
            GridViewUserList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;
        }
        else
        {
            GridViewUserList.Visible = false;
            rowGrid.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
            lblcategoryNames.Visible = false;
            txtSearch.Visible = false;
            btngo.Visible = false;
            btnCloseMe.Visible = false;
        }

    }

    protected void GridViewUserList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
    }

    protected void GridViewUserList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblcategory = new Label();
            lblcategory = (Label)e.Row.FindControl("lblcatid");
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "";
                pageurl = "window.opener.selectcategory('" + lblcategory.Text + "');window.opener.EndRequest();window.close();";
                if (i != 2)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

    protected void GridViewUserList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TabIndex = -1;
            e.Row.Attributes["onmouseover"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onmouseout"] = string.Format("javascript:DisSelectRow(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onselectstart"] = "javascript:return false;";
            e.Row.ToolTip = "Click to select row.";
        }

    }

    protected void btngo_Click(object sender, EventArgs e)
    {

        string search = txtSearch.Text;

        SqlParameter p1 = new SqlParameter("@Search", SqlDbType.VarChar);
        p1.Value = search;
        string _query = "HelpReportEntityMaster";
        TF_DATA objData = new TF_DATA();

        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;

            GridViewUserList.DataSource = dt.DefaultView;
            GridViewUserList.DataBind();
            GridViewUserList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else
        {
            GridViewUserList.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;

        }
    }

}