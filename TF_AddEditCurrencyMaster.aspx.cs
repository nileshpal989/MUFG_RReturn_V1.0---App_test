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

public partial class RBI_AddEditCurrencyMaster : System.Web.UI.Page
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
                txtCurrencyID.Attributes.Add("onkeypress", "javascript:return onlyChars(event)");
                clearControls();
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_ViewCurrencyMaster.aspx?PageHeader=Currency Master View", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtCurrencyID.Text = Request.QueryString["currencyid"].Trim();
                        txtCurrencyID.Enabled = false;
                        fillDetails(Request.QueryString["currencyid"].Trim());
                        txtDescription.Focus();
                    }
                    else
                    {
                        txtCurrencyID.Enabled = true;
                        txtCurrencyID.Focus();
                    }
                }
                btnSave.Attributes.Add("onclick", "return validateSave();");
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewCurrencyMaster.aspx?PageHeader=Currency Master View", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _mode = Request.QueryString["mode"].Trim();
        //string _cReCID = txtCRecID.Text.Trim();
        string _currencyID = txtCurrencyID.Text.Trim();
        string _currencyDescription = txtDescription.Text.Trim();
        string _Status = "";
        if (rdbActive.Checked)
        {
            _Status = "Active";
        }
        if (rdbInActive.Checked)
        {
            _Status = "In-Active";
        }

        TF_DATA objSave = new TF_DATA();
        string _query = "TF_UpdateCurrencyMaster";

        SqlParameter p1 = new SqlParameter("@mode", SqlDbType.VarChar);
        p1.Value = _mode;
        // SqlParameter p2 = new SqlParameter("@crecid", SqlDbType.VarChar);
        // p2.Value = _cReCID;
        SqlParameter p3 = new SqlParameter("@currencyid", SqlDbType.VarChar);
        p3.Value = _currencyID;
        SqlParameter p4 = new SqlParameter("@description", SqlDbType.VarChar);
        p4.Value = _currencyDescription;
        SqlParameter p5 = new SqlParameter("@status", SqlDbType.VarChar);
        p5.Value = _Status;        
        SqlParameter p6 = new SqlParameter("@user", SqlDbType.VarChar);
        p6.Value = Session["userName"].ToString().Trim();

        _result = objSave.SaveDeleteData(_query, p1, p3, p4, p5, p6);
        string _script = "";
        if (_result == "added")
        {
            _script = "window.location='TF_ViewCurrencyMaster.aspx?PageHeader=Currency Master View&result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                _script = "window.location='TF_ViewCurrencyMaster.aspx?PageHeader=Currency Master View&result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = _result;
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewCurrencyMaster.aspx?PageHeader=Currency Master View", true);
    }
    protected void clearControls()
    {
        //  txtCRecID.Text = "";
        txtCurrencyID.Text = "";
        txtDescription.Text = "";
    }
    protected void fillDetails(string _currencyID)
    {
        SqlParameter p1 = new SqlParameter("@currencyid", SqlDbType.VarChar);
        p1.Value = _currencyID;
        string _query = "TF_GetCurrencyMasterDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            //txtCRecID.Text = dt.Rows[0]["C_Rec_ID"].ToString().Trim();
            txtCurrencyID.Text = dt.Rows[0]["C_Code"].ToString().Trim();
            txtDescription.Text = dt.Rows[0]["C_Description"].ToString().Trim();            
            if (dt.Rows[0]["C_Status"].ToString().Trim() == "In-Active")
            {
                rdbInActive.Checked = true;
            }
            if (dt.Rows[0]["C_Status"].ToString().Trim() == "Active")
            {
                rdbActive.Checked = true;
            }
        }
    }
}