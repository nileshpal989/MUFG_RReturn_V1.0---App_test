using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class TF_rptAccessControl : System.Web.UI.Page
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
                    //PageHeader.Text = Request.QueryString["PageHeader"].ToString();
                    string header = Request.QueryString["PageHeader"];
                    if (!string.IsNullOrEmpty(header))
                    {
                        PageHeader.Text = Server.HtmlEncode(header);
                    }
                    btnBenfList.Attributes.Add("onclick", "return OpenUserList();");
                    btnSave.Attributes.Add("onclick", "return validateSave();");
                }
            }
    }

    protected void rdbSelectedUser_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbSelectedUser.Checked)
        {
            Table2.Visible = true;
        }
        else if (rdbAllUser.Checked)
        {
            Table2.Visible = false;
        }
    }
    protected void rdbAllUser_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbSelectedUser.Checked)
        {
            Table2.Visible = true;
        }
        else if (rdbAllUser.Checked)
        {
            Table2.Visible = false;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        var Report = PageHeader.Text;
        var User = "";
        int rptCode;
        if (rdbAllUser.Checked == true)
        {
            rptCode = 1;
            User = "All";
        }

        rptCode = 2;
        User = txtUser.Text;



        Response.Redirect("TF_ViewrptAccessControl.aspx?PageHeader="+Report+"&rptType=" + User + "&rptCode=" + rptCode);

    }
    protected void txtUser_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@userName", SqlDbType.VarChar);
        p1.Value = txtUser.Text;
        string _query = "TF_GetUserDetailsNew";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblUserName.Text = "";
        }
        else
        {
            txtUser.Text = "";
            lblUserName.Text = "<font style=color:red;>" + "User does not exists." + "</font>";
            txtUser.Focus();
        }
    }
}