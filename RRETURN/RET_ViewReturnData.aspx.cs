using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class RRETURN_RET_ViewReturnData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
            Response.Redirect("~/TF_Login.aspx?PageHeader=Login&sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                hdnUserRole.Value = Session["userRole"].ToString().Trim();
                txtFromDate.Text = Session["FrRelDt"].ToString().Trim();
                txtToDate.Text = Session["ToRelDt"].ToString().Trim();
                ddlrecordperpage.SelectedValue = "20";
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                lblAdcodeDesc.Text = Session["userADCode"].ToString();
                //ddlBranch.Enabled = false;
                fillBank();
                fillGrid();
                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].ToString().Substring(0, 5) == "added")
                    {
                        string _docNo = Request.QueryString["result"].ToString().Substring(5);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Add " + _docNo + "');", true);
                    }
                    else
                        if (Request.QueryString["result"].Trim() == "updated")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                        }
                }
                ddlBranch.Focus();
                //ddlBranch.Attributes.Add("onchange", "return changeBranchDesc();");
                btnAdd.Attributes.Add("onclick", "return validate();");
                btnSearch.Attributes.Add("onclick", "return validateSearch();");
            }
            ddlBranch.Focus();
            txtFromDate.Attributes.Add("onblur", "return ValidDates();");
        }
        //// ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "changeBranchDesc", "changeBranchDesc();", true);
    }
    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_RET_GetBranchandADcodeList";
        DataTable dt = objData.getData(_query);
        ddlBranch.Items.Clear();
        if (dt.Rows.Count > 0)
        {
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            lblAdcodeDesc.Text = dt.Rows[0]["AuthorizedDealerCode"].ToString();
            ddlBranch.DataBind();
        }
    }
    protected void fillBank()
    {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_RET_GetBankName";
        DataTable dt = objData.getData(_query);
        if (dt.Rows.Count > 0)
        {
           lblBankname.Text = dt.Rows[0]["BankName"].ToString();  
        }
    }
    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewRReturnEntry.PageCount != GridViewRReturnEntry.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewRReturnEntry.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewRReturnEntry.PageIndex + 1) + " of " + GridViewRReturnEntry.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }
        if (GridViewRReturnEntry.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewRReturnEntry.PageIndex != (GridViewRReturnEntry.PageCount - 1))
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
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewRReturnEntry.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewRReturnEntry.PageIndex > 0)
        {
            GridViewRReturnEntry.PageIndex = GridViewRReturnEntry.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewRReturnEntry.PageIndex != GridViewRReturnEntry.PageCount - 1)
        {
            GridViewRReturnEntry.PageIndex = GridViewRReturnEntry.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewRReturnEntry.PageIndex = GridViewRReturnEntry.PageCount - 1;
        fillGrid();
    }
    protected void GridViewRReturnEntry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result;
        SqlParameter p1 = new SqlParameter("@ADCode", SqlDbType.VarChar);
        p1.Value = ddlBranch.Text.Trim();
        SqlParameter p2 = new SqlParameter("@fromdate", SqlDbType.VarChar);
        p2.Value = txtFromDate.Text.ToString().Trim();
        SqlParameter p3 = new SqlParameter("@todate", SqlDbType.VarChar);
        p3.Value = txtToDate.Text.ToString().Trim();
        SqlParameter p4 = new SqlParameter("@srno", SqlDbType.VarChar);
        p4.Value = e.CommandArgument.ToString();
        SqlParameter p5 = new SqlParameter("@user", Session["userName"].ToString().Trim());
        SqlParameter p6 = new SqlParameter("@uploadeddate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        string _query = "TF_RET_DeleteRReturnEntryDetails";
        TF_DATA objData = new TF_DATA();
        result = objData.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6);
        fillGrid();
        if (result == "deleted")
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
    }
    protected void GridViewRReturnEntry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSrNo = new Label();
            Button btnDelete = new Button();
            lblSrNo = (Label)e.Row.FindControl("lblSrNo");
            btnDelete = (Button)e.Row.FindControl("btnDelete");
            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";
            //if (hdnUserRole.Value == "OIC")
            btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
            //else
            //btnDelete.Attributes.Add("onclick", "alert('Only OIC can delete all records.');return false;");
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='RET_AddEditReturnData.aspx?PageHeader=RReturn Data Entry&mode=edit&&Adcode=" + ddlBranch.Text.Trim()
                + "&fromdate=" + txtFromDate.Text + "&BranchName=" + ddlBranch.SelectedItem.Text
                + "&todate=" + txtToDate.Text.Trim() + "&srno=" + lblSrNo.Text.Trim() + "'";
                if (i < 10)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    { 
        fillGrid();
    }
    protected void fillGrid()
    {
        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;
        SqlParameter p2 = new SqlParameter("@ADCode", SqlDbType.VarChar);
        p2.Value = ddlBranch.Text.Trim();
        SqlParameter p3 = new SqlParameter("@fromdate", SqlDbType.VarChar);
        p3.Value = txtFromDate.Text.ToString().Trim();
        SqlParameter p4 = new SqlParameter("@todate", SqlDbType.VarChar);
        p4.Value = txtToDate.Text.ToString().Trim();
        string _query = "TF_RET_GetRReturnEntryList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2, p3, p4);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewRReturnEntry.PageSize = _pageSize;
            GridViewRReturnEntry.DataSource = dt.DefaultView;
            GridViewRReturnEntry.DataBind();
            GridViewRReturnEntry.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewRReturnEntry.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("RET_AddEditReturnData.aspx?PageHeader=RReturn Data Entry&mode=add&&BranchName=" + ddlBranch.SelectedItem.ToString() + "&Adcode=" + ddlBranch.Text.Trim() + "&fromdate=" + txtFromDate.Text.ToString().Trim()
        + "&todate=" + txtToDate.Text.ToString().Trim(), true);
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
        txtToDate.Focus();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "changeBranchDesc", "changeBranchDesc();", true);
    }
    protected void btnfillgrid_Click(object sender, EventArgs e)
    {
        fillGrid();
        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "changeBranchDesc", "changeBranchDesc();", true);
        btnAdd.Focus();
    }
}