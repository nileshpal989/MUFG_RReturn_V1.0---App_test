using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class RRETURN_Ret_AddEditAuthSign : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
            Response.Redirect("~/TF_Login.aspx?PageHeader=Login&sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("Ret_ViewAuthSignatory.aspx?PageHeader=Authorised Sign View", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtBranchname.Text = Request.QueryString["type"].ToString();
                        txtauthosign.Text = Request.QueryString["name"].ToString();
                        txtsrno.Text = Request.QueryString["srno"].ToString();
                        txtEmailID.Text = Request.QueryString["emailid"].ToString();
                        txtDesignation.Text = Request.QueryString["desgn"].ToString();
                        //txtauthosign.Enabled = false;
                        //fillDetails(txtBranchname.Text,txtauthosign.Text);
                    }
                    else
                    {
                        string mde = Request.QueryString["mode"].ToString();
                        txtBranchname.Text = Request.QueryString["BranchName"].ToString();
                        TF_DATA objdata = new TF_DATA();
                        string query = "Branchcode";
                        SqlParameter branch = new SqlParameter("@branch", SqlDbType.VarChar);
                        branch.Value = txtBranchname.Text;
                        string result1 = objdata.SaveDeleteData(query, branch);
                        SqlParameter p1 = new SqlParameter("@branchcode", result1);
                        query = "srnoincrement";
                        DataTable dt = objdata.getData(query, p1);
                        if (dt.Rows.Count > 0)
                        {
                            txtsrno.Text = dt.Rows[0]["srno"].ToString();
                        }
                    }
                }
                txtauthosign.Focus();
                btnSave.Attributes.Add("onclick", "return validateSave();");
                txtEmailID.Attributes.Add("onblur", "return checkEmail(" + txtEmailID.ClientID + ");");
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Ret_ViewAuthSignatory.aspx?PageHeader=Authorised Sign View", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _script = "";
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _mode = Request.QueryString["mode"].Trim();
        TF_DATA objSave = new TF_DATA();
        TF_DATA objData = new TF_DATA();
        string query = "Branchcode";
        SqlParameter branch = new SqlParameter("@branch", SqlDbType.VarChar);
        branch.Value = txtBranchname.Text;
        string result1 = objData.SaveDeleteData(query, branch);
        string _query = "SaveAuthSign";
        //SqlParameter pMode = new SqlParameter("@mode", SqlDbType.VarChar);
        //pMode.Value = _mode;
        SqlParameter branch1 = new SqlParameter("@branchcode", SqlDbType.VarChar);
        branch1.Value = result1;
        SqlParameter authosignatory = new SqlParameter("@authosignatory", SqlDbType.VarChar);
        authosignatory.Value = txtauthosign.Text;
        SqlParameter designation = new SqlParameter("@designation", SqlDbType.VarChar);
        designation.Value = txtDesignation.Text;
        SqlParameter emailid = new SqlParameter("@emailid", SqlDbType.VarChar);
        emailid.Value = txtEmailID.Text;
        SqlParameter srno = new SqlParameter("@srno", SqlDbType.VarChar);
        srno.Value = txtsrno.Text;
        SqlParameter addeddate = new SqlParameter("@addeddate", SqlDbType.VarChar);
        addeddate.Value = _uploadingDate;
        SqlParameter addedby = new SqlParameter("@addedby", SqlDbType.VarChar);
        addedby.Value = _userName;
        SqlParameter updateddate = new SqlParameter("@updateddate", SqlDbType.VarChar);
        updateddate.Value = _uploadingDate;
        SqlParameter updatedby = new SqlParameter("@updatedby", SqlDbType.VarChar);
        updatedby.Value = _userName;
        _result = objSave.SaveDeleteData(_query, branch1, authosignatory, designation, emailid, srno, addeddate, addedby, updateddate, updatedby);
        if (_result == "added")
        {
            _script = "window.location='Ret_ViewAuthSignatory.aspx?PageHeader=Authorised Sign View&result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                _script = "window.location='Ret_ViewAuthSignatory.aspx?PageHeader=Authorised Sign View&result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = _result;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Ret_ViewAuthSignatory.aspx?PageHeader=Authorised Sign View", true);
    }
}