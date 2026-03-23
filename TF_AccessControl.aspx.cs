using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class TF_AccessControl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
            Response.Redirect("~/TF_Login.aspx?PageHeader=Login&sessionout=yes&sessionid=" + lbl.Value, true);
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
                    string u = Request.QueryString["uname"].ToString();
                    fillUser();
                    ddlUser.SelectedValue = u;
                    ddlUser.Focus();
                    fillModule();
                }
            }
    }
    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillModule();
        ddlModule.Focus();
    }
    private void fillUser()
    {
        string _query = "TF_UserList";
        TF_DATA objData = new TF_DATA();
        SqlParameter pSearch = new SqlParameter("@search", SqlDbType.VarChar);
        pSearch.Value = "";
        DataTable dt = objData.getData(_query, pSearch);
        ddlUser.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlUser.DataSource = dt.DefaultView;
            ddlUser.DataTextField = "userName";
            ddlUser.DataValueField = "userName";
            ddlUser.DataBind();
        }
        else
            li.Text = "No record(s) found";
        ddlUser.Items.Insert(0, li);
    }
    private void fillgrid()
    {
        string _query = "TF_GetMenuList";
        TF_DATA objData = new TF_DATA();
        SqlParameter Module = new SqlParameter("@ModuleName", SqlDbType.VarChar);
        Module.Value = ddlModule.SelectedItem.ToString();
        DataTable dt = objData.getData(_query, Module);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridViewMenuList.DataSource = dt.DefaultView;
            GridViewMenuList.DataBind();
            GridViewMenuList.Visible = true;

            SqlParameter pUserName = new SqlParameter("@userName", SqlDbType.VarChar);
            pUserName.Value = ddlUser.Text;

            //
            SqlParameter pModule = new SqlParameter("@moduleID", SqlDbType.VarChar);
            pModule.Value = ddlModule.SelectedItem.ToString();

            //
            string _query1 = "TF_GetAccessedMenuList";
            DataTable dt1 = objData.getData(_query1, pUserName, pModule);
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i <= dt1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= GridViewMenuList.Rows.Count - 1; j++)
                    {
                        CheckBox chkrow = (CheckBox)GridViewMenuList.Rows[j].FindControl("RowChkAllow");
                        Label lblAccess = (Label)GridViewMenuList.Rows[j].FindControl("lblAccess");
                        if (dt1.Rows[i]["MenuName"].ToString() == dt.Rows[j]["MenuName"].ToString())
                        {
                            chkrow.Checked = true;
                            lblAccess.Text = "Access Allowed";
                            lblAccess.ForeColor = System.Drawing.Color.Blue;
                        }
                    }
                }
            }
            //else
            //    GridViewMenuList.Visible = false;
        }
        else
        {
            GridViewMenuList.Visible = false;
        }
    }
    protected void HeaderChkAllow_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)GridViewMenuList.HeaderRow.FindControl("HeaderChkAllow");
        if (chk.Checked)
        {
            for (int i = 0; i < GridViewMenuList.Rows.Count; i++)
            {
                CheckBox chkrow = (CheckBox)GridViewMenuList.Rows[i].FindControl("RowChkAllow");
                Label lblAccess = (Label)GridViewMenuList.Rows[i].FindControl("lblAccess");
                chkrow.Checked = true;
                lblAccess.Text = "Access Allowed";
                lblAccess.ForeColor = System.Drawing.Color.Blue;
            }
        }
        else
        {
            for (int i = 0; i < GridViewMenuList.Rows.Count; i++)
            {
                CheckBox chkrow = (CheckBox)GridViewMenuList.Rows[i].FindControl("RowChkAllow");
                Label lblAccess = (Label)GridViewMenuList.Rows[i].FindControl("lblAccess");
                chkrow.Checked = false;
                lblAccess.Text = "Access Denied";
                lblAccess.ForeColor = System.Drawing.Color.Red;
            }
        }
        chk.Focus();
    }
    protected void RowChkAllow_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkbox = (CheckBox)sender;
        GridViewRow row = (GridViewRow)checkbox.NamingContainer;
        Label lblAccess = (Label)row.FindControl("lblAccess");
        if (checkbox.Checked == true)
        {
            lblAccess.Text = "Access Allowed";
            lblAccess.ForeColor = System.Drawing.Color.Blue;
            checkbox.Focus();
        }
        else
        {
            lblAccess.Text = "Access Denied";
            lblAccess.ForeColor = System.Drawing.Color.Red;
            checkbox.Focus();
        }
        CheckBox chk = (CheckBox)GridViewMenuList.HeaderRow.FindControl("HeaderChkAllow");
        int isAllChecked = 0;
        for (int i = 0; i < GridViewMenuList.Rows.Count; i++)
        {
            CheckBox chkrow = (CheckBox)GridViewMenuList.Rows[i].FindControl("RowChkAllow");
            if (chkrow.Checked == true)
                isAllChecked = 1;
            else
            {
                isAllChecked = 0;
                break;
            }
        }
        if (isAllChecked == 1)
            chk.Checked = true;
        else
            chk.Checked = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";

        if (ddlUser.SelectedIndex < 1)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Select User');", true);
            ddlUser.Focus();
            return;
        }

        TF_DATA objDel = new TF_DATA();
        string _queryDel = "TF_DeleteUserAccess";

        SqlParameter pUserName = new SqlParameter("@userName", SqlDbType.VarChar);
        pUserName.Value = ddlUser.Text;

        SqlParameter pModule = new SqlParameter("@module", SqlDbType.VarChar);
        pModule.Value = ddlModule.SelectedItem.ToString();       
        TF_DATA objData = new TF_DATA();
        string _queryUpdate = "TF_UpdateUserAccess";
        string _OldValues = "";
        string _NewValues = "";

        foreach (GridViewRow row in GridViewMenuList.Rows)
        {
            CheckBox chkRow = (CheckBox)row.FindControl("RowChkAllow");
            Label lblMenu = (Label)row.FindControl("lblMenu");
            string menuName = lblMenu.Text;

            SqlParameter pMenuName = new SqlParameter("@MenuName", SqlDbType.VarChar);
            pMenuName.Value = menuName;

            if (chkRow.Checked)
            {
                _result = objData.SaveDeleteData(_queryUpdate, pUserName, pMenuName);

                if (_result == "added")
                {
                    _OldValues = "Access Denied";
                    _NewValues = "Access Allowed";
                    LogAuditTrail(pUserName, pModule, menuName, _OldValues, _NewValues);
                }
            }
            else
            {
                bool wasAccessAllowed = CheckPreviousAccess(ddlUser.Text, menuName);
                if (wasAccessAllowed)
                {
                    _OldValues = "Access Allowed";
                    _NewValues = "Access Denied";
                    LogAuditTrail(pUserName, pModule, menuName, _OldValues, _NewValues);

                    string _queryDel1 = "TF_DeleteUserAccess1";

                    string _resultdel = objDel.SaveDeleteData(_queryDel1, pUserName, pMenuName);
                }
            }
        }

        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Saved.');window.location.reload();", true);
        
    }

    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    string _result = "";
    //    if (ddlUser.SelectedIndex < 1)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Select User');", true);
    //        ddlUser.Focus();
    //        return;
    //    }
    //    TF_DATA objDel = new TF_DATA();
    //    string _queryDel = "TF_DeleteUserAccess";

    //    SqlParameter pUserName = new SqlParameter("@userName", SqlDbType.VarChar);
    //    pUserName.Value = ddlUser.Text;

    //    SqlParameter pModule = new SqlParameter("@module", SqlDbType.VarChar);
    //    pModule.Value = ddlModule.SelectedItem.ToString();


    //    _result = objDel.SaveDeleteData(_queryDel, pUserName, pModule);
    //    if (_result == "deleted" || _result == "new")
    //    {
    //        TF_DATA objData = new TF_DATA();
    //        string _query = "TF_UpdateUserAccess";
    //        Label lblMenu = new Label();
    //        //SqlParameter pUserName = new SqlParameter("@userName", SqlDbType.VarChar);
    //        // pUserName.Value = ddlUser.Text;
    //        SqlParameter pMenuName = new SqlParameter("@MenuName", SqlDbType.VarChar);
    //        for (int i = 0; i < GridViewMenuList.Rows.Count; i++)
    //        {
    //            CheckBox chkrow = (CheckBox)GridViewMenuList.Rows[i].FindControl("RowChkAllow");
    //            if (chkrow.Checked == true)
    //            {
    //                lblMenu = (Label)GridViewMenuList.Rows[i].FindControl("lblMenu");
    //                pMenuName.Value = lblMenu.Text;
    //                _result = objData.SaveDeleteData(_query, pUserName, pMenuName);
    //            }
    //        }
    //        if (_result == "added")
    //        {
    //            ddlUser.SelectedIndex = 0;
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Saved.');window.location.reload();", true);
    //        }
    //        if (_result == "deleted")
    //        {
    //            ddlUser.SelectedIndex = 0;
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');window.location.reload();", true);
    //        }
    //    }
    //    else
    //    {
    //        labelMessage.Text = _result;
    //    }
    //}

    private void fillModule()
    {
        string _query = "TF_FillModule";
        TF_DATA objData = new TF_DATA();

        DataTable dt = objData.getData(_query);
        ddlModule.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlModule.DataSource = dt.DefaultView;
            ddlModule.DataTextField = "Mod_Name";
            ddlModule.DataValueField = "Mod_Name";
            ddlModule.DataBind();
        }
        else
            li.Text = "No record(s) found";
        ddlModule.Items.Insert(0, li);
    }
    private void LogAuditTrail(SqlParameter pUserName, SqlParameter pModule, string menuName, string oldValues, string newValues)
    {
        TF_DATA objData = new TF_DATA();
        string _queryAuditTrail = "TF_Admin_Access_AuditTrail";

        SqlParameter Branch = new SqlParameter("@BranchCode", SqlDbType.VarChar) { Value = "2140001" };
        SqlParameter Mod = new SqlParameter("@ModType", SqlDbType.VarChar) { Value = "ADMIN" };
        SqlParameter oldvaluesParam = new SqlParameter("@OldValues", SqlDbType.VarChar) { Value = oldValues };
        SqlParameter newvaluesParam = new SqlParameter("@NewValues", SqlDbType.VarChar) { Value = newValues };
        SqlParameter Acno = new SqlParameter("@CustAcNo", SqlDbType.VarChar) { Value = "" };
        SqlParameter DocumentNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar) { Value = "" };
        SqlParameter FWDContractNo = new SqlParameter("@FWD_Contract_No", SqlDbType.VarChar) { Value = "" };
        SqlParameter DocumnetDate = new SqlParameter("@DocumentDate", SqlDbType.VarChar) { Value = "" };
        SqlParameter Mode = new SqlParameter("@Mode", SqlDbType.VarChar) { Value = (newValues == "Access Allowed") ? "A" : "D" };
        SqlParameter user = new SqlParameter("@ModifiedBy", SqlDbType.VarChar) { Value = Session["userName"].ToString() };
        SqlParameter moddate = new SqlParameter("@ModifiedDate", SqlDbType.DateTime) { Value = DateTime.Now };
        SqlParameter menuNameParam = new SqlParameter("@MenuName", SqlDbType.VarChar) { Value = "User Access Control" };
        SqlParameter usernameParam = new SqlParameter("@username", SqlDbType.VarChar) { Value = pUserName.Value };

        string result = objData.SaveDeleteData(_queryAuditTrail, Branch, Mod, oldvaluesParam, newvaluesParam, Acno, DocumentNo, FWDContractNo, DocumnetDate, Mode, user, moddate, menuNameParam, usernameParam);
    }

    private bool CheckPreviousAccess(string userName, string menuName)
    {
        TF_DATA objData = new TF_DATA();
        string query = "TF_CheckUserMenuAccess";
        SqlParameter pUserName = new SqlParameter("@userName", SqlDbType.VarChar) { Value = userName };
        SqlParameter pMenuName = new SqlParameter("@MenuName", SqlDbType.VarChar) { Value = menuName };

        DataTable dt = objData.getData(query, pUserName, pMenuName);
        return dt.Rows.Count > 0;
    }
    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
        ddlModule.Focus();
    }
}