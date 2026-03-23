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
        string _query = "HelpCurMstr";
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
            Label lblCurrName = new Label();
            lblCurrName = (Label)e.Row.FindControl("lblCurrDesc");
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "", hNo = Request.QueryString["pc"].ToString();
                if (Request.QueryString["pc"].ToString() == "1")
                {
                    pageurl = "window.opener.selectCurrency('" + lblCurrID.Text + "','" + lblCurrName.Text + "');window.opener.EndRequest();window.close();";
                }
                if (Request.QueryString["pc"].ToString() == "2")
                {
                    pageurl = "window.opener.selectPCCurrency('" + lblCurrID.Text + "','" + lblCurrName.Text + "');window.opener.EndRequest();window.close();";
                }
                if (Request.QueryString["pc"].ToString() == "3")
                {
                    pageurl = "window.opener.selectInstCurrency('" + lblCurrID.Text + "','" + lblCurrName.Text + "');window.opener.EndRequest();window.close();";
                }
                if (Request.QueryString["pc"].ToString() == "4")
                {
                    pageurl = "window.opener.select71F1Currency('" + lblCurrID.Text + "','" + lblCurrName.Text + "');window.opener.EndRequest();window.close();";
                }
                if (Request.QueryString["pc"].ToString() == "5")
                {
                    pageurl = "window.opener.select71F2Currency('" + lblCurrID.Text + "','" + lblCurrName.Text + "');window.opener.EndRequest();window.close();";
                }
                if (Request.QueryString["pc"].ToString() == "6")
                {
                    pageurl = "window.opener.selectCurrency('" + lblCurrID.Text + "','" + lblCurrName.Text + "','" + hNo + "');window.opener.EndRequest();window.close();";
                }
                if (Request.QueryString["pc"].ToString() == "7")
                {
                    pageurl = "window.opener.selectCurrency('" + lblCurrID.Text + "','" + lblCurrName.Text + "','" + hNo + "');window.opener.EndRequest();window.close();";
                }
                if (Request.QueryString["pc"].ToString() == "8")
                {
                    pageurl = "window.opener.selectCurrency('" + lblCurrID.Text + "','" + lblCurrName.Text + "','" + hNo + "');window.opener.EndRequest();window.close();";
                }
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
        string _query = "HelpCurMstr";
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
