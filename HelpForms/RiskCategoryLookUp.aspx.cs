using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class HelpForms_RiskCategoryLookUp : System.Web.UI.Page
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
        SqlParameter p1 = new SqlParameter("@EnumerationType", "Risk Rating");

        string _query = "TF_CBWT_GetEnumerationList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
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

    protected void GridViewBankList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblRiskCode = new Label();
            lblRiskCode = (Label)e.Row.FindControl("lblRiskCode");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "";
                pageurl = "window.opener.selectRiskCategory('" + lblRiskCode.Text + "');window.opener.EndRequest();window.close();";

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
        fillGrid();
    }

}
