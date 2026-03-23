using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Reports_TF_AuditTrail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            string URL = HttpContext.Current.Request.Url.AbsoluteUri;
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("~/TF_Login.aspx?PageHeader=Login&sessionout=yes&sessionid=" + lbl.Value + "&PageUrl=" + URL, true);
        }
        else
        {
            if (!IsPostBack)
            {
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                //ddlBranch.Enabled = false;
                btnSave.Attributes.Add("onclick", "return Generate();");
                btnUserList.Attributes.Add("onclick", "return OpenUserList();");
                PageHeader.Text = Request.QueryString["PageHeader"].ToString();
                txtFromDate.Focus();
            }
            txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
    protected void fillUserList()
    {
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_UserList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            ddlUserList.DataSource = dt.DefaultView;
            ddlUserList.DataTextField = "userName";
            ddlUserList.DataValueField = "userName";
            ddlUserList.DataBind();
        }
    }
    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        if (dt.Rows.Count > 0)
        {
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
    }
    protected void rdbAllCustomer_CheckedChanged(object sender, EventArgs e)
    {
        rdbSelectedCustomer.Checked = false;
        rdbAllCustomer.Checked = true;
        divUser.Visible = false;
        txtUserName.Text = "";

    }
    protected void rdbSelectedCustomer_CheckedChanged(object sender, EventArgs e)
    {
        rdbSelectedCustomer.Checked = true;
        rdbAllCustomer.Checked = false;
        divUser.Visible = true;
        txtUserName.Focus();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        var Branch = ddlBranch.SelectedValue;
        var Report = PageHeader.Text;
        //var Module = hdnModule.Value;
        var Type = "";
        var UserName = "";
        if (rdbAllCustomer.Checked == true)
            UserName = "All";
        else
            UserName = txtUserName.Text;

        if (rdbAllTypes.Checked == true)
            Type = "All";
        else if (rdbAdd.Checked == true)
            Type = "A";
        else if (rdbDelete.Checked == true)
            Type = "D";
        else if (rdbModify.Checked == true)
            Type = "M";
        else if (rdbFileUpload.Checked == true)
            Type = "F";
        else if (rdbFileCreation.Checked == true)
            Type = "C";
        Response.Redirect("View_rptAuditTrail.aspx?PageHeader=" + Report + "&frm=" + txtFromDate.Text + "&to=" + txtToDate.Text + "&Branch=" + Branch + "&UserName=" + UserName + "&Type=" + Type + "&Report=" + Report + "&Module=RET");
    }
    protected void txtUserName_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@userName", SqlDbType.VarChar);
        p1.Value = txtUserName.Text;
        string _query = "TF_GetUserDetailsNew";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblUserName.Text = "";
        }
        else
        {
            txtUserName.Text = "";
            lblUserName.Text = "<font style=color:red;>" + "User does not exists." + "</font>";
            txtUserName.Focus();
        }
    }
}