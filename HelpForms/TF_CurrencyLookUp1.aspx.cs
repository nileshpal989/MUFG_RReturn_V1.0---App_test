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


public partial class TF_CurrencyLookUp1 : System.Web.UI.Page
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
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";

        string _query = "TF_GetCurrencyList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;

            GridViewCurrList.DataSource = dt.DefaultView;
            GridViewCurrList.DataBind();
            GridViewCurrList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else
        {
            GridViewCurrList.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }

    }
    protected void GridViewCurrList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       // string result = "";
    }
    protected void GridViewCurrList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCurrID = new Label();
            lblCurrID = (Label)e.Row.FindControl("lblCurrID");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "";
                
              
                    pageurl = "window.opener.selectCurrency('" + lblCurrID.Text + "');window.opener.EndRequest();window.close();";
                
                
                if (i != 2)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

    protected void GridViewCurrList_RowCreated(object sender, GridViewRowEventArgs e)
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

        string _query = "TF_GetCurrencyList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;

            GridViewCurrList.DataSource = dt.DefaultView;
            GridViewCurrList.DataBind();
            GridViewCurrList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else
        {
            GridViewCurrList.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
}
