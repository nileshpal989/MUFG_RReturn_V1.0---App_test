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
using System.Net;


public partial class TF_Login : System.Web.UI.Page
{
    TF_DATA objSave1 = new TF_DATA();
    //private static string domain = "uat.lmccsoft.com";
    private static string domain = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        domain = hdndomain.Value;
        if (!IsPostBack)
        {
            txtUserName.Focus();
            if (Request.QueryString["sessionout"] != null)
            {

                if (Request.QueryString["sessionout"].ToString() == "yes")
                {
                    labelMessage.Text = "Your session has timed out! Login again";

                    TF_DATA objSave = new TF_DATA();

                    SqlParameter p1 = new SqlParameter("@username", SqlDbType.VarChar);
                    p1.Value = "";
                    SqlParameter p2 = new SqlParameter("@loggedin", SqlDbType.VarChar);
                    p2.Value = "";
                    SqlParameter p3 = new SqlParameter("@loggedout", SqlDbType.VarChar);
                    p3.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    SqlParameter p4 = new SqlParameter("@type", SqlDbType.VarChar);
                    p4.Value = "LOGOUT";
                    SqlParameter p5 = new SqlParameter("@id", SqlDbType.VarChar);
                    p5.Value = Request.QueryString["sessionid"].ToString();
                    string _qurey = "TF_AddLoginLogoutLog";
                    string s = objSave.SaveDeleteData(_qurey, p1, p2, p3, p4, p5);

                    txtUserName.Text = s.Substring(7);


                    Session.Abandon();
                    Session.RemoveAll();
                    Session.Clear();
                    if (txtUserName.Text != "")
                        txtPassword.Focus();
                }
            }


            hdmremaindays.Value = ""; Hidden1.Value = "";
            btnLogin.Attributes.Add("onclick", "return validateSave();");
            //btnalert.Attributes.Add("Style", "display:none;");
            btnUserList.Attributes.Add("onclick", "return OpenUserList();");
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        TF_DATA objSave = new TF_DATA();
        Encryption objEncryption = new Encryption();
        string _userName = txtUserName.Text.Trim();
        string _password = objEncryption.encryptplaintext(txtPassword.Text.Trim());
        string ipAddressW = GetIPAddress();
        //String adPath = "LDAP://uat.lmccsoft.com"; //lmcc adpath
        String adPath = hdnAdPath.Value; //lmcc adpath
        string Log_Query = "TF_Audit_ApplicationLogs";
        int invalidcount;
        if (_userName == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "messege", "alert('Please enter UserName')", true);
            return;
        }
        else if (_password == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "messege", "alert('Please enter Password')", true);
            return;
        }
        else if (_userName != "" && _password != "")
        {
            LdapAuthentication adAuth = new LdapAuthentication(adPath);
            try
            {
                if (true == adAuth.IsAuthenticated(domain, txtUserName.Text, txtPassword.Text))
                {
                    _userName = txtUserName.Text.Trim();
                    string suser = chkUser(_userName);
                    if (suser == "in-active")
                    {
                        txtUserName.Focus();
                        labelMessage.Text = "Your Account is In-Active.Contact Administrator";
                    }
                    else
                    {
                        if (authenticatedADUser(_userName) == "valid")
                        {
                            string _result = "", dtsAlert = "";

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
                            string _query1 = "TF_AddLoginLogoutLog";

                            string s = objSave.SaveDeleteData(_query1, par1, par2, par3, par4, par5);

                            ///this session variable is used to find out who was login and update in log table when he/she logsout
                            ///
                            Session["LoggedUserId"] = s;

                            //End
                            string result2 = objSave1.SaveDeleteData("TF_updateInvalidvLog", par1);
                            //Response.Redirect("TF_ModuleSelection.aspx?PageHeader=RET Module&type=0", true);
                            Response.Redirect("TF_ModuleSelection.aspx?type=0", false);


                        }
                        else
                        {
                            suser = chkUser(_userName);
                            if (suser == "in-active")
                            {
                                txtUserName.Focus();
                                labelMessage.Text = "Your account is in-active.Contact Administrator";
                            }
                            else if (suser == "in-valid")
                            {
                                txtUserName.Focus();
                                labelMessage.Text = "Invalid login details.";
                            }
                            else
                            {
                                txtUserName.Focus();
                                labelMessage.Text = "Invalid login details.";
                                labelMessage.Text = "Authentication did not succeed. Check user name and password.";
                            }
                        }
                    }
                }
                else
                {
                    labelMessage.Text = "Authentication did not succeed. Check user name and password.";
                }
            }
            catch (Exception ex)
            {
                string errorLabel = "Error authenticating. " + ex.Message;
                //Session.Abandon();
                txtUserName.Text = ""; txtPassword.Text = "";
                //lbllocked.Text = ex.Message;
                labelMessage.Text = ex.Message;

                SqlParameter para1 = new SqlParameter("@UserName", SqlDbType.VarChar);
                para1.Value = _userName;
                SqlParameter para2 = new SqlParameter("@LogDateTime", SqlDbType.VarChar);
                para2.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                string hostName = Dns.GetHostName(); //Retrive the Name of HOST
                string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                SqlParameter PIPNEw = new SqlParameter("@IP", SqlDbType.VarChar);
                PIPNEw.Value = ipAddressW;
                string query1 = "CheckAdminrole";
                DataTable checkAdmin = objSave1.getData(query1, para1);
                if (checkAdmin.Rows[0]["userRole"].ToString() != "NA")
                {
                    if (checkAdmin.Rows[0]["userRole"].ToString() != "Admin")
                    {
                        string _queryalert = "InvLog";
                        DataTable dtalert = objSave1.getData(_queryalert, para1, para2);
                        if (dtalert.Rows.Count > 0 && dtalert.Rows[0][0].ToString() == "Alert1")
                        {
                            txtUserName.Text = "";
                            txtPassword.Text = "";
                            labelMessage.Text = "";
                            invalidcount = 1;
                            SqlParameter count_invalid = new SqlParameter("@count", SqlDbType.VarChar);
                            count_invalid.Value = invalidcount;
                            string _queryAuditInvalidLoigins = "TF_AuditInvalidLoigins";
                            string s1 = objSave1.SaveDeleteData(_queryAuditInvalidLoigins, para1, para2, PIPNEw, count_invalid);
                            SqlParameter puserid = new SqlParameter("@userID", SqlDbType.VarChar);
                            puserid.Value = _userName;
                            SqlParameter pip = new SqlParameter("@IP", SqlDbType.VarChar);
                            pip.Value = ipAddressW;
                            SqlParameter ptimestamp = new SqlParameter("@timestamp", System.Data.SqlDbType.DateTime);
                            ptimestamp.Value = System.DateTime.Now;
                            SqlParameter paratype = new SqlParameter("@type", SqlDbType.VarChar);
                            paratype.Value = "Login";
                            SqlParameter parastatus = new SqlParameter("@status", SqlDbType.VarChar);
                            parastatus.Value = "Invalid Login: Attempt One";
                            string stored_logs = objSave1.SaveDeleteData(Log_Query, puserid, pip, ptimestamp, paratype, parastatus);
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('First attempt unsuccessful! Please verify your username/password and try again. Note:Your account will be locked after 3 unsuccessful attempts.')", true);
                            return;
                        }
                        else if (dtalert.Rows.Count > 0 && dtalert.Rows[0][0].ToString() == "Alert2")
                        {
                            txtUserName.Text = "";
                            txtPassword.Text = "";
                            labelMessage.Text = "";
                            invalidcount = 2;
                            SqlParameter count_invalid = new SqlParameter("@count", SqlDbType.VarChar);
                            count_invalid.Value = invalidcount;
                            string _queryAuditInvalidLoigins = "TF_AuditInvalidLoigins";
                            string s1 = objSave1.SaveDeleteData(_queryAuditInvalidLoigins, para1, para2, PIPNEw, count_invalid);
                            SqlParameter puserid = new SqlParameter("@userID", SqlDbType.VarChar);
                            puserid.Value = _userName;
                            SqlParameter pip = new SqlParameter("@IP", SqlDbType.VarChar);
                            pip.Value = ipAddressW;
                            SqlParameter ptimestamp = new SqlParameter("@timestamp", System.Data.SqlDbType.DateTime);
                            ptimestamp.Value = System.DateTime.Now;
                            SqlParameter paratype = new SqlParameter("@type", SqlDbType.VarChar);
                            paratype.Value = "Login";
                            SqlParameter parastatus = new SqlParameter("@status", SqlDbType.VarChar);
                            parastatus.Value = "Invalid Login: Attempt Two";
                            string stored_logs = objSave1.SaveDeleteData(Log_Query, puserid, pip, ptimestamp, paratype, parastatus);
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Second attempt unsuccessful! Please verify your username/password and try again. Note:Your account will be locked after 3 unsuccessful attempts.')", true);
                            return;

                        }
                        else if (dtalert.Rows.Count > 0 && dtalert.Rows[0][0].ToString() == "Locked")
                        {
                            txtUserName.Text = "";
                            txtPassword.Text = "";
                            labelMessage.Text = "";
                            invalidcount = 3;
                            SqlParameter count_invalid = new SqlParameter("@count", SqlDbType.VarChar);
                            count_invalid.Value = invalidcount;
                            string _queryAuditInvalidLoigins = "TF_AuditInvalidLoigins";
                            string s1 = objSave1.SaveDeleteData(_queryAuditInvalidLoigins, para1, para2, PIPNEw, count_invalid);
                            SqlParameter puserid = new SqlParameter("@userID", SqlDbType.VarChar);
                            puserid.Value = _userName;
                            SqlParameter pip = new SqlParameter("@IP", SqlDbType.VarChar);
                            pip.Value = ipAddressW;
                            SqlParameter ptimestamp = new SqlParameter("@timestamp", System.Data.SqlDbType.DateTime);
                            ptimestamp.Value = System.DateTime.Now;
                            SqlParameter paratype = new SqlParameter("@type", SqlDbType.VarChar);
                            paratype.Value = "Login";
                            SqlParameter parastatus = new SqlParameter("@status", SqlDbType.VarChar);
                            parastatus.Value = "Login Failed: Account Locked";
                            string stored_logs = objSave1.SaveDeleteData(Log_Query, puserid, pip, ptimestamp, paratype, parastatus);
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Your Account Is Locked ! Please Contact Application Administrator.')", true);
                            return;
                        }

                    }
                }
                else
                {
                    txtUserName.Focus();
                    labelMessage.Text = "Invalid login details.";
                    SqlParameter puserid = new SqlParameter("@userID", SqlDbType.VarChar);
                    puserid.Value = _userName;
                    SqlParameter pip = new SqlParameter("@IP", SqlDbType.VarChar);
                    pip.Value = ipAddressW;
                    SqlParameter ptimestamp = new SqlParameter("@timestamp", System.Data.SqlDbType.DateTime);
                    ptimestamp.Value = System.DateTime.Now;
                    SqlParameter paratype = new SqlParameter("@type", SqlDbType.VarChar);
                    paratype.Value = "Login";
                    SqlParameter parastatus = new SqlParameter("@status", SqlDbType.VarChar);
                    parastatus.Value = labelMessage.Text;
                    string stored_logs = objSave1.SaveDeleteData(Log_Query, puserid, pip, ptimestamp, paratype, parastatus);
                }
                //Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
                HttpCookie cookie = new HttpCookie("ASP.NET_SessionId", "");
                cookie.HttpOnly = true;   // JS se access block
                //cookie.Secure = true;     // Sirf HTTPS pe chalega
                //cookie.SameSite = SameSiteMode.Strict; // CSRF protection

                Response.Cookies.Add(cookie);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "messege", "alert('Something went wrong')", true);
            return;
        }
    }

    protected void btnalert_Click(object sender, EventArgs e)
    {
        string _userName = txtUserName.Text.Trim();
        if (Hidden1.Value == "1")
        {
            Response.Redirect("TF_ChangePassword.aspx?PageHeader=Change Password&userName=" + _userName);
        }
        else
        {
            int remday = 0;
            remday = int.Parse(hdmremaindays.Value);
            if (remday != 0)
            {
                TF_DATA objSave = new TF_DATA();
                //This is used update one more month to user when he loggs every time
                //  _result = objSave.UpdateUserExpiryDate(_userName);

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
                string _query1 = "TF_AddLoginLogoutLog";

                string s = objSave.SaveDeleteData(_query1, par1, par2, par3, par4, par5);

                ///this session variable is used to find out who was login and update in log table when he/she logsout
                ///
                Session["LoggedUserId"] = s;

                //Session["ModuleID"] = "PCFC";
                //Response.Redirect("~/PC/PC_Main.aspx", true);

                Response.Redirect("TF_ModuleSelection.aspx?PageHeader=RET Module&type=0", true);
            }
        }
    }

    protected string authenticateUser(string _userName, string _password)
    {
        string sretrn = "";
        SqlParameter p1 = new SqlParameter("@username", SqlDbType.VarChar);
        p1.Value = _userName;
        SqlParameter p2 = new SqlParameter("@password", SqlDbType.VarChar);
        p2.Value = _password;
        string _query = "TF_AuthenticateUser";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            Session["userRole"] = dt.Rows[0]["userRole"].ToString().Trim();
            Session["userName"] = dt.Rows[0]["userName"].ToString().Trim();
            Session["userADCode"] = dt.Rows[0]["ADCode"].ToString().Trim();
            Session["userLBCode"] = dt.Rows[0]["BranchCode"].ToString().Trim();

            sretrn = "valid";
        }

        return sretrn;
    }

    //NILESH 14012025
    public static string GetIPAddress()
    {
        string ipAddress = string.Empty;
        foreach (IPAddress item in Dns.GetHostAddresses(HttpContext.Current.Request.UserHostAddress))
        {
            if (item.AddressFamily.ToString().Equals("InterNetwork"))
            {
                ipAddress = item.ToString();
                break;
            }
        }
        if (!string.IsNullOrEmpty(ipAddress))
        {
            return ipAddress;
        }
        foreach (IPAddress item in Dns.GetHostAddresses(Dns.GetHostName()))
        {
            if (item.AddressFamily.ToString().Equals("InterNetwork"))
            {
                ipAddress = item.ToString();
                break;
            }
        }
        return ipAddress;
    }

    protected string authenticatedADUser(string _userName)
    {
        string sretrn = "";
        SqlParameter p1 = new SqlParameter("@username", SqlDbType.VarChar);
        p1.Value = _userName;
        string _query = "TF_Validated_ADUser";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            Session["userRole"] = dt.Rows[0]["userRole"].ToString().Trim();
            Session["userName"] = dt.Rows[0]["userName"].ToString().Trim();
            Session["userADCode"] = dt.Rows[0]["ADCode"].ToString().Trim();
            Session["userLBCode"] = dt.Rows[0]["BranchCode"].ToString().Trim();
            sretrn = "valid";
        }
        else
        {
            sretrn = "Invalid";
        }
        return sretrn;
    }
    public string chkUser(string _userName)
    {
        string sretrn = "";
        SqlParameter p1 = new SqlParameter("@username", SqlDbType.VarChar);
        p1.Value = _userName;
        //string _query = "TF_CheckUserExists";
        string _query = "TF_CheckUserExists_NEW";
        TF_DATA objData = new TF_DATA();

        DataTable dtnew = objData.getData(_query, p1);
        if (dtnew.Rows.Count > 0)
        {
            if (dtnew.Rows[0]["active"].ToString() == "2")
            {
                sretrn = "in-active";
            }
        }
        else
        {
            sretrn = "in-valid";
        }
        return sretrn;
    }
}
