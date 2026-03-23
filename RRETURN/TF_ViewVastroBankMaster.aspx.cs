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
public partial class TF_ViewVastroBankMaster : System.Web.UI.Page
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
                ddlrecordperpage.SelectedValue = "20";
                fillGrid();
                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].Trim() == "added")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);
                    }
                    else
                        if (Request.QueryString["result"].Trim() == "updated")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                        }
                }
                txtSearch.Attributes.Add("onkeypress", "return submitForm(event);");
                btnSearch.Attributes.Add("onclick", "return validateSearch();");
            }
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
            if (GridViewVastroBank.PageCount != GridViewVastroBank.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewVastroBank.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewVastroBank.PageIndex + 1) + " of " + GridViewVastroBank.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }
        if (GridViewVastroBank.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewVastroBank.PageIndex != (GridViewVastroBank.PageCount - 1))
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
        GridViewVastroBank.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewVastroBank.PageIndex > 0)
        {
            GridViewVastroBank.PageIndex = GridViewVastroBank.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewVastroBank.PageIndex != GridViewVastroBank.PageCount - 1)
        {
            GridViewVastroBank.PageIndex = GridViewVastroBank.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewVastroBank.PageIndex = GridViewVastroBank.PageCount - 1;
        fillGrid();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_AddEditVastroBankMaster.aspx?PageHeader=Vostro Bank Master Details&mode=add", true);
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
        TF_DATA objData = new TF_DATA();
        string _query = "TF_GetVastroBankMasterList";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewVastroBank.PageSize = _pageSize;
            GridViewVastroBank.DataSource = dt.DefaultView;
            GridViewVastroBank.DataBind();
            GridViewVastroBank.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewVastroBank.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void GridViewPortCodes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
        string _CountryCode = "";
        string _BankCode = "";
        string[] values_p;
        string str = e.CommandArgument.ToString();
        if (str != "")
        {
            char[] splitchar = { ';' };
            values_p = str.Split(splitchar);
            _CountryCode = values_p[0].ToString();
            _BankCode = values_p[1].ToString();
        }
        //string _bankCode = e.CommandArgument.ToString();
        SqlParameter p1 = new SqlParameter("@countrycode", SqlDbType.VarChar);
        p1.Value = _CountryCode;
        SqlParameter p2 = new SqlParameter("@bankcode", SqlDbType.VarChar);
        p2.Value = _BankCode;
        string _query = "TF_DeleteVastroBankMasterDetails";
        TF_DATA objData = new TF_DATA();
        result = objData.SaveDeleteData(_query, p1, p2);
        fillGrid();
        if (result == "deleted")
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
        else
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record can not be deleted as it is associated with another record.');", true);
    }
    protected void GridViewPortCodes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCountryCode = new Label();
            Label lblBankCode = new Label();
            Label lblLinked = new Label();
            Button btnDelete = new Button();
            lblCountryCode = (Label)e.Row.FindControl("lblCountryCode");
            lblBankCode = (Label)e.Row.FindControl("lblBankCode");
            lblLinked = (Label)e.Row.FindControl("lblLinked");
            btnDelete = (Button)e.Row.FindControl("btnDelete");
            if (lblLinked.Text.Trim() == "0")
            {
                btnDelete.Enabled = true;
                btnDelete.CssClass = "deleteButton";
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
            }
            else
            {
                btnDelete.Enabled = false;
                btnDelete.CssClass = "deleteButtonDisabled";
            }
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                //string pageurl = "window.location='TF_AddEditVastroBankMaster.aspx?mode=edit&Countrycode=" + lblCountryCode.Text.Trim() + "'";
                string pageurl = "window.location='TF_AddEditVastroBankMaster.aspx?PageHeader=Vostro Bank Master Details&mode=edit&Countrycode=" + lblCountryCode.Text.Trim() + "&bankcode=" + lblBankCode.Text.Trim() + "'";
                if (i != 3)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
}
