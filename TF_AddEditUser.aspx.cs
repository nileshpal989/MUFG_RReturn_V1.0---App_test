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


public partial class TF_AddEditUser : System.Web.UI.Page
{
    string _NewValues = ""; string _OldValues = "";
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
                    ddlBranch.Focus();                    
                    fillBranch();
                    btnSave.Attributes.Add("onclick", "return validateSave();");
                    clearControls();
                    if (Request.QueryString["mode"] == null)
                    {
                        Response.Redirect("TF_ViewHouseKeeping.aspx?PageHeader=House Keeping View", true);
                    }
                    else
                    {
                        if (Request.QueryString["mode"].Trim() != "add")
                        {
                            lblHeader.Text = "User Updation";
                            //btnChangePaswd.Enabled = true; // commented by supriya 16012025
                            txtUserName.Text = Request.QueryString["username"].Trim();
                            txtUserName.Enabled = false;
                            // commented by supriya 16012025
                            //txtPassword.Enabled = false;
                            //txtReTypePassword.Enabled = false;
                            // end
                            fillDetails(Request.QueryString["username"].Trim());
                            ddlBranch.Enabled = false;
                        }
                        else
                        {
                            lblHeader.Text = "User Creation";
                            //btnChangePaswd.Enabled = false; // commented by supriya 16012025
                            txtUserName.Enabled = true;
                            ddlBranch.Enabled = true;
                        }
                        btnSave.Attributes.Add("onclick", "return validateSave();");
                    }                   
                }
            }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewHouseKeeping.aspx?PageHeader=House Keeping View", true);
    }

    protected void btnChangePaswd_Click(object sender, EventArgs e)
    {
        // commented by supriya 16012025
        //fillDetails(Request.QueryString["username"]);
        //txtUserName.Enabled = false;
        //ddlRole.Enabled = false;
        //ddlStatus.Enabled = false;

        //txtPassword.Enabled = true;
        //txtReTypePassword.Enabled = true;

        //txtPassword.Attributes.Add("value", "");
        //txtReTypePassword.Attributes.Add("value", "");

        //hdntype.Value = "Pwsd";
        //btnChangePaswd.Enabled = false;
        //txtPassword.Focus();
        // end
    }

    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _mode = Request.QueryString["mode"].Trim();
        string _userName = txtUserName.Text.Trim();
        string _role = ddlRole.SelectedValue.Trim();
        string _active = ddlStatus.SelectedValue.Trim();
        string _adcode = ddlBranch.SelectedValue.Trim();

        Encryption objEncryption = new Encryption();
        //string _password = objEncryption.encryptplaintext(txtPassword.Text.Trim()); // commented by supriya 16012025
        string _password = "";

        TF_DATA objUpdateExDt = new TF_DATA();
        string s = "";

        TF_DATA objSave = new TF_DATA();


        if (hdntype.Value == "Pwsd")
        {
            // commented by supriya 16012025
            //_result = objSave.UpdateUserPassword(_userName, _password);
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alertMessage", "alert('Password Updated.')", true);
            //labelMessage.Text = "Password " + _result;

            //s = objUpdateExDt.UpdateUserExpiryDate(_userName);

            //txtUserName.Enabled = true;
            //ddlRole.Enabled = true;
            //ddlStatus.Enabled = true;

            //txtPassword.Enabled = false;
            //txtReTypePassword.Enabled = false;
            //hdntype.Value = "";
            //btnChangePaswd.Enabled = true;
            //fillDetails(Request.QueryString["username"]);
            // end
        }
        else
        {
            _result = objSave.addUpdateUserDetails(_mode, _userName, _password, _role, _active, _adcode);
            s = objUpdateExDt.UpdateUserExpiryDate(_userName);
            string _script = "";
            if (_result == "added")
            {
                string _query = "TF_Admin_AuditTrail";
                _NewValues = "Branch:" + ddlBranch.SelectedItem.Text + ";UserName: " + txtUserName.Text + ";Role: " + ddlRole.SelectedValue.ToString() + ";Status:" + ddlStatus.SelectedItem.Text.ToString() + ";DateTime:" + System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                SqlParameter Branch = new SqlParameter("@BranchCode", SqlDbType.VarChar);
                Branch.Value = ddlBranch.SelectedValue;
                SqlParameter Mod = new SqlParameter("@ModType", SqlDbType.VarChar);
                Mod.Value = "ADMIN";
                SqlParameter oldvalues = new SqlParameter("@OldValues", SqlDbType.VarChar);
                oldvalues.Value = _OldValues;
                SqlParameter newvalues = new SqlParameter("@NewValues", SqlDbType.VarChar);
                newvalues.Value = _NewValues;
                SqlParameter Acno = new SqlParameter("@CustAcNo", SqlDbType.VarChar);
                Acno.Value = "";
                SqlParameter DocumentNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
                DocumentNo.Value = "";
                SqlParameter FWDContractNo = new SqlParameter("@FWD_Contract_No", SqlDbType.VarChar);
                FWDContractNo.Value = "";
                SqlParameter DocumnetDate = new SqlParameter("@DocumentDate", SqlDbType.VarChar);
                DocumnetDate.Value = "";
                SqlParameter Mode = new SqlParameter("@Mode", "A");
                SqlParameter user = new SqlParameter("@ModifiedBy", SqlDbType.VarChar);
                //user.Value = _userName;
                user.Value = Session["userName"].ToString();
                string _moddate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                SqlParameter moddate = new SqlParameter("@ModifiedDate", SqlDbType.VarChar);
                moddate.Value = _moddate;
                //string _type = "A";
                //SqlParameter type = new SqlParameter("@type", SqlDbType.VarChar);
                //type.Value = _type;
                string _menu = "User Creation";
                SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
                menu.Value = _menu;
                string at = objSave.SaveDeleteData(_query, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, FWDContractNo, DocumnetDate, Mode, user, moddate, menu);

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "confirm", "ConfirmRedirectAddedAccessControl();", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "confirm", "ConfirmRedirectAccessControl();", true);
                //_script = "window.location='TF_ViewHouseKeeping.aspx?result=" + _result + "'";
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
            {
                if (_result == "updated")
                {
                    int isneedtolog = 0;
                    _OldValues = "Branch:" + ddlBranch.SelectedItem.Text + ";UserName: " + txtUserName.Text + ";Role: " + hdnRole.Value.ToString() + ";Status:" + hdnStatus.Value.ToString() + ";DateTime:" + System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    //           "UserName: " + txtUserName.Text + ";Role: " + ddlRole.SelectedValue.ToString() + ";Status:" + ddlStatus.SelectedValue.ToString() + ";DateTime:" + System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    if (hdnRole.Value != ddlRole.SelectedValue.ToString())
                    {
                        isneedtolog = 1;
                        _NewValues = _NewValues + "Role : " + ddlRole.SelectedValue.ToString();
                    }
                    if (hdnStatus.Value != ddlStatus.SelectedValue.ToString())
                    {
                        isneedtolog = 1;
                        _NewValues = _NewValues + "; Status : " + ddlStatus.SelectedItem.Text.ToString();
                    }

                    string _query = "TF_Admin_AuditTrail";
                    SqlParameter Branch = new SqlParameter("@BranchCode", SqlDbType.VarChar);
                    Branch.Value = ddlBranch.SelectedValue;
                    SqlParameter Mod = new SqlParameter("@ModType", SqlDbType.VarChar);
                    Mod.Value = "ADMIN";
                    SqlParameter oldvalues = new SqlParameter("@OldValues", SqlDbType.VarChar);
                    oldvalues.Value = _OldValues;
                    SqlParameter newvalues = new SqlParameter("@NewValues", SqlDbType.VarChar);
                    newvalues.Value = _NewValues;
                    SqlParameter Acno = new SqlParameter("@CustAcNo", SqlDbType.VarChar);
                    Acno.Value = "";
                    SqlParameter DocumentNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
                    DocumentNo.Value = "";
                    SqlParameter FWDContractNo = new SqlParameter("@FWD_Contract_No", SqlDbType.VarChar);
                    FWDContractNo.Value = "";
                    SqlParameter DocumnetDate = new SqlParameter("@DocumentDate", SqlDbType.VarChar);
                    DocumnetDate.Value = "";
                    SqlParameter Mode = new SqlParameter("@Mode", "M");
                    SqlParameter user = new SqlParameter("@ModifiedBy", SqlDbType.VarChar);
                    user.Value = Session["userName"].ToString();
                    string _moddate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    SqlParameter moddate = new SqlParameter("@ModifiedDate", SqlDbType.VarChar);
                    moddate.Value = _moddate;
                    //string _type = "A";
                    //SqlParameter type = new SqlParameter("@type", SqlDbType.VarChar);
                    //type.Value = _type;
                    string _menu = "User Modification";
                    SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
                    menu.Value = _menu;
                    if (isneedtolog == 1)
                    {
                        string at = objSave.SaveDeleteData(_query, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, FWDContractNo, DocumnetDate, Mode, user, moddate, menu);
                    }

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "confirm", "ConfirmRedirectUpdatedAccessControl();", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "confirm", "ConfirmRedirectAccessControl();", true);
                    //_script = "window.location='TF_ViewHouseKeeping.aspx?result=" + _result + "'";
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                }
                else
                    labelMessage.Text = _result;
            }
        }


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewHouseKeeping.aspx?PageHeader=House Keeping View", true);
    }
    protected void clearControls()
    {
        txtUserName.Text = "";
        //txtPassword.Text = ""; // commented by supriya 16012025
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
            // commented by supriya 16012025
            //_password = objEncryption.decrypttext(dt.Rows[0][1].ToString().Trim());
            //txtPassword.Text = _password;
            //hdnpswd.Value = _password;
            //txtReTypePassword.Text = _password;
            //txtPassword.Attributes.Add("value", _password);
            //txtReTypePassword.Attributes.Add("value", _password);
            // end

          
            ddlRole.SelectedValue = dt.Rows[0][2].ToString().Trim();
            hdnRole.Value = dt.Rows[0][2].ToString().Trim();
            ddlStatus.SelectedValue = dt.Rows[0][3].ToString().Trim();
            hdnStatus.Value = dt.Rows[0][3].ToString().Trim();
            ddlBranch.SelectedValue = dt.Rows[0]["ADCode"].ToString().Trim();

        }
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_AccessControl.aspx?PageHeader=Access Control&uname=" + txtUserName.Text, true);
    }
}
