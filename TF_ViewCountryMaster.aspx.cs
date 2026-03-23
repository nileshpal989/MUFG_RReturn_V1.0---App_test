using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class TF_ViewCountryMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("TF_Login.aspx?PageHeader=Login&sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                ddlrecordpage.SelectedValue = "20";
                fillGrid();
                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].Trim() == "added")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Record Added.');", true);
                    }
                    else if (Request.QueryString["result"].Trim() == "updated")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Record Updated.');", true);
                    }
                }
                txtSearch.Attributes.Add("onkeypress", "return submitForm(event);");
                btnSearch.Attributes.Add("onclick", "return validateSearch();");
            }
        }
    }
    protected void GridViewCountry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
        string _countryid = e.CommandArgument.ToString();
        SqlParameter p1 = new SqlParameter("@countryID", SqlDbType.VarChar);
        p1.Value = _countryid;
        string query = "TF_DeleteCountryMaster";

        TF_DATA objdata = new TF_DATA();
        result = objdata.SaveDeleteData(query, p1);
        fillGrid();
        if (result == "deleted")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "deletemessage", "alert('Record Deleted.')'", true);
        }
    }
    protected void GridViewCountry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCountryID = new Label();
            Button btnDelete = new Button();
            Label lblStatus = new Label();

            lblCountryID = (Label)e.Row.FindControl("lblCountryID");
            btnDelete = (Button)e.Row.FindControl("btnDelete");

            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";
            btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");

            lblStatus = (Label)e.Row.FindControl("lblStatus");
            if (lblStatus.Text == "In-Active")
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string url = "window.location='TF_AddEditCountryMaster.aspx?PageHeader=Country Master Detials&mode=edit&countryid=" + lblCountryID.Text.Trim() + "'";
                if (i != 3)
                    cell.Attributes.Add("onclick", url);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_AddEditCountryMaster.aspx?PageHeader=Country Master Detials&mode=add", true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewCountry.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewCountry.PageIndex > 0)
        {
            GridViewCountry.PageIndex = GridViewCountry.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewCountry.PageIndex != GridViewCountry.PageCount - 1)
        {
            GridViewCountry.PageIndex = GridViewCountry.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewCountry.PageIndex = GridViewCountry.PageCount - 1;
        fillGrid();
    }
    protected void ddlrecordpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void fillGrid()
    {
        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        string query = "TF_GetCountryListMSTR";
        TF_DATA objdata = new TF_DATA();
        DataTable dt = objdata.getData(query, p1);

        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordpage.SelectedValue.Trim());
            GridViewCountry.PageSize = _pageSize;
            GridViewCountry.DataSource = dt.DefaultView;
            GridViewCountry.DataBind();
            GridViewCountry.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewCountry.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No Record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewCountry.PageCount != GridViewCountry.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewCountry.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewCountry.PageIndex + 1) + " of " + GridViewCountry.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewCountry.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewCountry.PageIndex != (GridViewCountry.PageCount - 1))
        {
            pgcontrols.enablelastnav(btnnavnext, btnnavlast);
        }
        else
        {
            pgcontrols.disablelastnav(btnnavnext, btnnavlast);
        }
    }
    private void navigationVisibility(Boolean visibility)
    {
        lblpageno.Visible = visibility;
        lblrecordno.Visible = visibility;
        btnnavfirst.Visible = visibility;
        btnnavlast.Visible = visibility;
        btnnavnext.Visible = visibility;
        btnnavpre.Visible = visibility;
    }
}