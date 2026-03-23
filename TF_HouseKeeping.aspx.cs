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


public partial class TF_HouseKeeping : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Redirect("TF_Login.aspx?PageHeader=Login&sessionout=yes", true);
        }
        else
        {
            if (!IsPostBack)
            {
                txtUserName.Focus();

                btnSave.Attributes.Add("onclick", "return validateSave();");
                clearControls();
                txtUserName.Text = Session["userName"].ToString().Trim();

                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
                txtReTypePassword.Enabled = false;

                fillDetails(Session["userName"].ToString().Trim());
                ddlRole.Enabled = false;
                ddlStatus.Enabled = false;
            }
            txtPassword.Attributes.Add("onblur", "return ValidatePass();");
            btnChangePaswd.Attributes.Add("onclick", "return ChangePass();");
            //txtPassword.Attributes.Add("onfocus", "return Blank();");
        }
    }
    protected void btnChangePaswd_Click(object sender, EventArgs e)
    {
        //fillDetails(Session["userName"].ToString().Trim());

        txtPassword.Enabled = true;
        txtReTypePassword.Enabled = true;

        txtPassword.Attributes.Add("value","");
        txtReTypePassword.Attributes.Add("value", "");

        hdntype.Value = "Pwsd";
        btnChangePaswd.Enabled = false;
        txtPassword.Focus();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _mode = "edit";
        string _userName = txtUserName.Text.Trim();
        string _role = ddlRole.SelectedValue.Trim();
        string _active = ddlStatus.SelectedValue.Trim();

        Encryption objEncryption = new Encryption();
        string _password = objEncryption.encryptplaintext(txtPassword.Text.Trim());

        TF_DATA objSave = new TF_DATA();
        if (hdntype.Value == "Pwsd")
        {
            _result = objSave.UpdateUserPassword(_userName, _password);
            if (_result=="updated")
                objSave.UpdateUserExpiryDate(_userName);
            labelMessage.Text = "Password " + _result;
            labelMessage.Font.Bold = true;
            ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(),"message","alert('Password Updated')",true);
            //txtUserName.Enabled = true;
            //ddlRole.Enabled = true;
            //ddlStatus.Enabled = true;

            //txtPassword.Enabled = false;
            //txtReTypePassword.Enabled = false;
            hdntype.Value = "";

            txtUserName.Enabled = false;
            btnChangePaswd.Enabled = true;
            ddlRole.Enabled = false;
            ddlStatus.Enabled = false;
            txtPassword.Enabled = false;
            txtReTypePassword.Enabled = false;

            txtUserName.Text = Session["userName"].ToString().Trim();
            fillDetails(Session["userName"].ToString().Trim());
        }
        else
        {

            //_result = objSave.addUpdateUserDetails(_mode, _userName, _password, _role, _active);
            //if (_result == "updated")
            //    labelMessage.Text = "Record Updated.";
            //else
            //    labelMessage.Text = _result;
            clearControls();
            txtUserName.Text = Session["userName"].ToString().Trim();
            fillDetails(Session["userName"].ToString().Trim());
        }
    }
    protected void clearControls()
    {
        txtUserName.Text = "";
        txtPassword.Text = ""; hdnpswd.Value = "";
        ddlRole.SelectedIndex = -1;
        ddlStatus.SelectedIndex = -1;
        hdntype.Value = "";
        hdnpswd.Value = "";
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
            txtReTypePassword.Text = _password;
            hdnpswd.Value = _password;
            txtPassword.Attributes.Add("value", _password);
            txtReTypePassword.Attributes.Add("value", _password);
            ddlRole.SelectedValue = dt.Rows[0][2].ToString().Trim();
            ddlStatus.SelectedValue = dt.Rows[0][3].ToString().Trim();
        }
    }
}
