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
public partial class TF_AddEditVastroBankMaster : System.Web.UI.Page
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
                    Response.Redirect("TF_ViewVastroBankMaster.aspx?PageHeader=Vostro Bank Master View", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtCountrycode.Text = Request.QueryString["Countrycode"].Trim();
                        txtCountrycode.Enabled = false;
                        txtBankCode.Enabled = false;
                        btnCountryList.Enabled = false;
                        btnBankList.Enabled = false;
                        //fillDetails(Request.QueryString["Countrycode"].Trim());
                        fillDetails(Request.QueryString["Countrycode"].Trim(), Request.QueryString["bankcode"].Trim());
                    }
                    else
                    {
                        txtCountrycode.Enabled = true;
                        txtBankCode.Enabled = true;
                    }
                }
                txtCountrycode.Focus();
                btnSave.Attributes.Add("onclick", "return validateSave();");
                txtBankCode.Attributes.Add("onkeydown", "return validate_Number(event);");
                btnCountryList.Attributes.Add("onclick", "return OpenCountryList('1');");
                btnBankList.Attributes.Add("onclick", "return OpenVastroList('mouseClick');");
                txtCountrycode.Attributes.Add("onblur", "toUpper_Case();");
                //btnVosterBankList.Attributes.Add("onclick", "return OpenOverseasBankList('mouseClick');");
            }
        }
    }
    protected void btnCountry_Click(object sender, EventArgs e)
    {
        if (hdnCountry.Value != "")
        {
            txtCountrycode.Text = hdnCountry.Value;
            fillCountryDescription();
            txtCountrycode.Focus();
        }
    }
    protected void btnBankCode_Click(object sender, EventArgs e)
    {
        if (hdnBankCode.Value != "")
        {
            txtBankCode.Text = hdnBankCode.Value;
            fillDetails(txtCountrycode.Text, txtBankCode.Text);
            txtBankCode.Focus();
        }
    }
    protected void btnOverseasBank_Click(object sender, EventArgs e)
    {
        if (hdnOverseasId.Value != "")
        {
            //txtVosterBankID.Text = hdnOverseasId.Value;
            //fillRemittingBankDescription();
            //txtVosterBankID.Focus();
        }
    }
    private void fillCountryDescription()
    {
        lblCountryDesc.Text = "";
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@cid", SqlDbType.VarChar);
        p1.Value = txtCountrycode.Text;
        string _query = "TF_GetCountryDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCountryDesc.Text = dt.Rows[0]["CountryName"].ToString().Trim();
        }
        else
        {
            txtCountrycode.Text = "";
            lblCountryDesc.Text = "<font style=color:red;>"+"Invalid Country Code"+"</font>";
        }
    }
    protected void txtVosterBankID_TextChanged(object sender, EventArgs e)
    {
        //fillRemittingBankDescription();
    }
    //public void fillRemittingBankDescription()
    //{
    //    txtBankName.Text = "";
    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
    //    p1.Value = txtVosterBankID.Text.Trim();
    //    string _query = "TF_GetOverseasBankMasterDetails";
    //    DataTable dt = objData.getData(_query, p1);
    //    if (dt.Rows.Count > 0)
    //    {
    //        txtBankName.Text = dt.Rows[0]["BankName"].ToString().Trim();
    //    }
    //    else
    //    {
    //        txtVosterBankID.Text = "";
    //        txtBankName.Text = "";
    //    }
    //}
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewVastroBankMaster.aspx?PageHeader=Vostro Bank Master View", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _mode = Request.QueryString["mode"].Trim();
        string _CountryCode = txtCountrycode.Text.Trim();
        string _BankCode = txtBankCode.Text.Trim();
        string _BankName = txtBankName.Text.Trim();
        TF_DATA objSave = new TF_DATA();
        string _query = "TF_UpdateVastroBankMaster";
        SqlParameter p1 = new SqlParameter("@mode", SqlDbType.VarChar);
        p1.Value = _mode;
        SqlParameter p2 = new SqlParameter("@countrycode", SqlDbType.VarChar);
        p2.Value = _CountryCode;
        SqlParameter p3 = new SqlParameter("@bankcode", SqlDbType.VarChar);
        p3.Value = _BankCode;
        SqlParameter p4 = new SqlParameter("@bankname", SqlDbType.VarChar);
        p4.Value = _BankName;
        SqlParameter p5 = new SqlParameter("@user", SqlDbType.VarChar);
        p5.Value = _userName;
        SqlParameter p6 = new SqlParameter("@uploadingdate", SqlDbType.VarChar);
        p6.Value = _uploadingDate;
        //SqlParameter p7 = new SqlParameter("@Vostro_Bank_ID", txtVosterBankID.Text);
        _result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6);
        string _script = "";
        if (_result == "added")
        {
            _script = "window.location='TF_ViewVastroBankMaster.aspx?PageHeader=Vostro Bank Master View&result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                _script = "window.location='TF_ViewVastroBankMaster.aspx?PageHeader=Vostro Bank Master View&result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = _result;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewVastroBankMaster.aspx?PageHeader=Vostro Bank Master View&", true);
    }
    protected void clearControls()
    {
        txtCountrycode.Text = "";
        txtBankCode.Text = "";
        txtBankName.Text = "";
        //txtVosterBankID.Text = "";
    }
    protected void fillDetails(string _CountryCode, string _BankCode)
    {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_GetVastroBankMasterDetails";
        SqlParameter p1 = new SqlParameter("@countrycode", SqlDbType.VarChar);
        p1.Value = _CountryCode;
        SqlParameter p2 = new SqlParameter("@bankcode", SqlDbType.VarChar);
        p2.Value = _BankCode;
        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            fillCountryDescription();
            txtBankCode.Text = dt.Rows[0]["BANKCODE"].ToString().Trim();
            //txtVosterBankID.Text = dt.Rows[0]["Vostro_Bank_ID"].ToString();
            txtBankName.Text = dt.Rows[0]["BANKNAME"].ToString().Trim();
        }
        else
        {
            txtBankName.Text = "";
            txtBankName.Focus();
        }       
    }
    protected void txtBankCode_TextChanged(object sender, EventArgs e)
    {
        fillDetails(txtCountrycode.Text, txtBankCode.Text);        
    }
    protected void txtCountrycode_TextChanged(object sender, EventArgs e)
    {
        fillCountryDescription();
        txtCountrycode.Focus();
    }
}