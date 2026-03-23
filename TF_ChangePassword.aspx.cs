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


public partial class TF_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["userName"] == null)
        //{
        //    //    System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)TF_Menu1.FindControl("hdnloginid");

        //    //    Response.Redirect("TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        //}
        //else
        //{
            if (!IsPostBack)
            {
                txtNewPassword.Focus();
                btnSave.Attributes.Add("onclick", "return validateSave();");
                clearControls();
                txtUserName.Text = Request.QueryString["userName"].ToString().Trim();
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
                fillDetails(Request.QueryString["userName"].ToString().Trim());
            }
            txtNewPassword.Attributes.Add("onblur", "ValidatePass();");
        //}
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _mode = "edit";
        string _userName = txtUserName.Text.Trim();

        Encryption objEncryption = new Encryption();
        string _password = objEncryption.encryptplaintext(txtNewPassword.Text.Trim());

        TF_DATA objSave = new TF_DATA();
        _result = objSave.UpdateUserPassword(_userName, _password);
        if (_result == "updated")
        {
            _result = objSave.UpdateUserExpiryDate(_userName);
            string _query1 = "TF_AddLoginLogoutLog";
            SqlParameter par1 = new SqlParameter("@username", SqlDbType.VarChar);
            par1.Value = _userName;
            SqlParameter par2 = new SqlParameter("@loggedin", System.Data.SqlDbType.DateTime);
            par2.Value = System.DateTime.Now;
            SqlParameter par3 = new SqlParameter("@loggedout", DBNull.Value);
            par3.Value = "";
            SqlParameter par4 = new SqlParameter("@type", SqlDbType.VarChar);
            par4.Value = "LOGIN";
            SqlParameter par5 = new SqlParameter("@id", SqlDbType.VarChar);
            par5.Value = "";
            string s = objSave.SaveDeleteData(_query1, par1, par2, par3, par4, par5);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Message", "alert('Password updated.');window.navigate('TF_Login.aspx');", true);
            //Response.Redirect("TF_Login.aspx", true);
        }
        else
        {
            Response.Redirect("TF_Login.aspx", true);
        }
    }
    protected void clearControls()
    {
        txtUserName.Text = "";
        txtPassword.Text = "";
        txtNewPassword.Text = "";
    }
    protected void fillDetails(string _userName)
    {
        TF_DATA objData = new TF_DATA();
        Encryption objEncryption = new Encryption();
        string _password = "";
        DataTable dt = objData.getUserDetails(_userName);
        if (dt.Rows.Count > 0)
        {
            _password = objEncryption.decrypttext(dt.Rows[0][1].ToString().Trim());
            txtPassword.Text = _password;
            txtReTypePassword.Text = "";
            // txtReTypePassword.Text = _password;          
            txtPassword.Attributes.Add("value", _password);
            txtReTypePassword.Attributes.Add("value", "");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_Logout.aspx");
    }
}
