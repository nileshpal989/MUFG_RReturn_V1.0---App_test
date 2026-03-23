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

public partial class TF_ViewHouseKeeping : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("TF_Login.aspx?PageHeader=Login&sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
            if (Session["userRole"].ToString() != "Admin")
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

    protected void btnSearch_Click(object sender, EventArgs e)
    { fillGrid(); }


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
            if (GridViewUsers.PageCount != GridViewUsers.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewUsers.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewUsers.PageIndex + 1) + " of " + GridViewUsers.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewUsers.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewUsers.PageIndex != (GridViewUsers.PageCount - 1))
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
        GridViewUsers.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewUsers.PageIndex > 0)
        {
            GridViewUsers.PageIndex = GridViewUsers.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewUsers.PageIndex != GridViewUsers.PageCount - 1)
        {
            GridViewUsers.PageIndex = GridViewUsers.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewUsers.PageIndex = GridViewUsers.PageCount - 1;
        fillGrid();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_AddEditUser.aspx?PageHeader=Add Edit User&mode=add", true);
    }
    protected void fillGrid()
    {
        TF_DATA objData = new TF_DATA();
        string search = txtSearch.Text.Trim();
        DataTable dt = objData.userList(search, Session["userName"].ToString());
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewUsers.PageSize = _pageSize;
            GridViewUsers.DataSource = dt.DefaultView;
            GridViewUsers.DataBind();
            GridViewUsers.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewUsers.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void GridViewUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
        string _userName = e.CommandArgument.ToString();
        SqlParameter p1 = new SqlParameter("@Username", SqlDbType.VarChar);
        p1.Value = _userName;
        SqlParameter p2 = new SqlParameter("@DeletedBy", SqlDbType.VarChar);
        p2.Value = Session["userName"].ToString();
        string _query = "TF_Admin_DeleteAuditLog";
        TF_DATA objData = new TF_DATA();
        SqlParameter p3 = new SqlParameter("@userName", SqlDbType.VarChar);
        p3.Value = _userName;
        DataTable dt = objData.getData("TF_GetAdCode", p3);
        result = objData.deleteUserDetails(_userName);
        SqlParameter p4 = new SqlParameter("@AdCode", SqlDbType.VarChar);
        p4.Value = dt.Rows[0][0].ToString();
        string result1 = objData.SaveDeleteData(_query, p1, p2, p4);
        fillGrid();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
    }
    protected void GridViewUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblUserName = new Label();
            Button btnDelete = new Button();
            lblUserName = (Label)e.Row.FindControl("lblUserName");
            btnDelete = (Button)e.Row.FindControl("btnDelete");

            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";
            btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");

            if (lblUserName.Text == Session["userName"].ToString())
            {
                btnDelete.Enabled = false;
            }

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='TF_AddEditUser.aspx?PageHeader=Add Edit User&mode=edit&username=" + lblUserName.Text.Trim() + "'";
                if (i != 3)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }


}
