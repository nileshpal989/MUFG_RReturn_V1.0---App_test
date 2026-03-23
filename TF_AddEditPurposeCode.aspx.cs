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

public partial class TF_AddEditPurposeCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("TF_Login.aspx?PageHeader=Login&sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                
                btnSave.Attributes.Add("onclick", "return validateSave();");
                clearControls();
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_ViewPurposeCode.aspx?PageHeader=Purpose Code View", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtPurposeCode.Text = Request.QueryString["purposecode"].Trim();
                        txtPurposeCode.Enabled = false;
                        fillDetails(Request.QueryString["purposecode"].Trim());
                        txtDescription.Focus();
                    }
                    else
                    {
                        txtPurposeCode.Enabled = true;
                        txtPurposeCode.Focus();
                    }
                }
                btnSave.Attributes.Add("onclick", "return validateSave();");

            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewPurposeCode.aspx?PageHeader=Purpose Code View", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _mode = Request.QueryString["mode"].Trim();
        string _purposeCode = txtPurposeCode.Text.Trim();
        string _description = txtDescription.Text.Trim();

        TF_DATA objSave = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@mode", SqlDbType.VarChar);
        p1.Value = _mode;
        SqlParameter p2 = new SqlParameter("@purposeid", SqlDbType.VarChar);
        p2.Value = _purposeCode;
        SqlParameter p3 = new SqlParameter("@description", SqlDbType.VarChar);
        p3.Value = _description;
        SqlParameter p4 = new SqlParameter("@user", SqlDbType.VarChar);
        p4.Value = _userName;
        SqlParameter p5 = new SqlParameter("@uploadingdate", SqlDbType.VarChar);
        p5.Value = _uploadingDate;

        string _query = "TF_UpdatePurposeCodeMaster";

        _result = objSave.SaveDeleteData(_query,p1,p2,p3,p4,p5);
        string _script = "";
        if (_result == "added")
        {
            _script = "window.location='TF_ViewPurposeCode.aspx?PageHeader=Purpose Code View&result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                _script = "window.location='TF_ViewPurposeCode.aspx?PageHeader=Purpose Code View&result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = _result;
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewPurposeCode.aspx?PageHeader=Purpose Code View", true);
    }
    protected void clearControls()
    {
        txtPurposeCode.Text = "";
        txtDescription.Text = "";
    }
    protected void fillDetails(string _purposeCode)
    {
        TF_DATA objData = new TF_DATA();

        TF_DATA objSave = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@purposecode", SqlDbType.VarChar);
        p1.Value = _purposeCode;
       
        string _query = "TF_GetPurposeCodeMasterDetails";

        DataTable dt = objData.getData(_query,p1);
        if (dt.Rows.Count > 0)
        {
            txtDescription.Text = dt.Rows[0][1].ToString().Trim();
        }
    }
}
