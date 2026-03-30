using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class TF_rpt_LoginLog : System.Web.UI.Page
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
                    fillBranch();
                    ddlBranch.SelectedValue = Session["userADCode"].ToString();
                    ddlBranch.Enabled = false;
                    fillUserList();

                    //PageHeader.Text = Request.QueryString["PageHeader"].ToString();
                    string header = Request.QueryString["PageHeader"];
                    if (!string.IsNullOrEmpty(header))
                    {
                        PageHeader.Text = Server.HtmlEncode(header);
                    }
                    btnUserList.Attributes.Add("onclick", "return OpenUserList1();");
                    //txtUserName.Attributes.Add("onblur", "return checkUser();");
                    rdbAllCustomer.Attributes.Add("onclick", "return toogleDisplay();");
                    rdbSelectedCustomer.Attributes.Add("onclick", "return toogleDisplay();");
                    txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                }
                
                ddlBranch.Focus();
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "toogleDisplay();", true);
                btnSave.Attributes.Add("onclick", "return Generate();");
            }
    }
    protected void fillUserList()
    {
        //SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        //p1.Value = "";
        //string _query = "TF_UserList";
        //TF_DATA objData = new TF_DATA();
        //DataTable dt = objData.getData(_query, p1);
        //if (dt.Rows.Count > 0)
        //{
        //    ddlUserList.DataSource = dt.DefaultView;
        //    ddlUserList.DataTextField = "sAMAccountName";
        //    ddlUserList.DataValueField = "sAMAccountName";
        //    ddlUserList.DataBind();
        //}
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
            ddlBranch.DataBind();
        }
    }
    //protected void btnSave_Click(object sender, EventArgs e)
    //{

    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@frmdate", SqlDbType.Date);
    //    p1.Value = txtFromDate.Text;
    //    SqlParameter p2 = new SqlParameter("@todate", SqlDbType.Date);
    //    p2.Value = txtToDate.Text;
    //    SqlParameter p3 = new SqlParameter("@user", SqlDbType.VarChar);
    //    if (rdbAllCustomer.Checked)
    //    {
    //        p3.Value = "ALL";
    //    }
    //    else
    //    {
    //        p3.Value = txtUserName.Text;
    //    }
    //    string _query = "RRETURN_PasswordHis";
    //    DataTable dt = objData.getData(_query, p1, p2, p3);
    //    if (dt.Rows.Count > 0)
    //    {
    //        TF_DATA objData2 = new TF_DATA();
    //        string __query = "deletepasshis";
    //        string dlt = objData2.SaveDeleteData(__query);
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            Encryption objEncryption = new Encryption();
    //            string username = dt.Rows[i]["userName"].ToString().Trim();
    //            string password = objEncryption.decrypttext(dt.Rows[i]["newpassword"].ToString().Trim());
    //            string updateDate = dt.Rows[i]["change_date"].ToString().Trim();
    //            int lenght = password.Length;
    //            string first2char = password.Substring(0, 2);
    //            int m = lenght - 2;
    //            string last2char = password.Substring(m, 2);
    //            TF_DATA objData1 = new TF_DATA();
    //            SqlParameter p6 = new SqlParameter("@user", SqlDbType.VarChar);
    //            p6.Value = username;
    //            SqlParameter p7 = new SqlParameter("@first2", SqlDbType.VarChar);
    //            p7.Value = first2char;
    //            SqlParameter p8 = new SqlParameter("@last2", SqlDbType.VarChar);
    //            p8.Value = last2char;
    //            SqlParameter p9 = new SqlParameter("@lenght", SqlDbType.Int);
    //            p9.Value = lenght;
    //            SqlParameter p10 = new SqlParameter("@updatedate", SqlDbType.DateTime);
    //            p10.Value = updateDate;
    //            string _query1 = "spintopass";
    //            string dtpass = objData1.SaveDeleteData(_query1, p6, p7, p8, p9, p10);
    //        }
    //      ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Generate", "Generate();", true);
    //        //btnSave.Attributes.Add("onclick", "return Generate();");
    //    }
    //}
    protected void btnSave_Click(object sender, EventArgs e)
    {
        var Report = PageHeader.Text;
        var Branch = ddlBranch.SelectedValue;
        var frmdate = txtFromDate.Text;
        var todate = txtToDate.Text;
        var User = "";
        if (rdbAllCustomer.Checked == true)
            User = "All";
        else
            User = txtUserName.Text;
        Response.Redirect("VIEW_TF_rpt_LoginLog.aspx?PageHeader=" + Report + "&frm=" + frmdate + "&to=" + todate + "&Branch=" + Branch + "&User=" + User);

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