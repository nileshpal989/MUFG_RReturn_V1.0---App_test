using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class TF_AddEditCountryMaster : System.Web.UI.Page
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
                //clearControls();
                
                //txtCountryID.Text = Request.QueryString["countryid"].ToString();
                
                btnSave.Attributes.Add("onclick", "return validateSave();");
                txtCountryID.Attributes.Add("onkeypress", "javascript:return onlyChars(event)");
                txtCountryName.Attributes.Add("onkeypress", "javascript:return onlyChars(event)");
                
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_ViewCountryMaster.aspx?PageHeader=Country Master View", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtCountryID.Text = Request.QueryString["countryid"].Trim();
                        txtCountryID.Enabled = false;
                        txtCountryName.Focus();
                    }
                    else
                    {
                        txtCountryID.Focus();
                    }
                }
                filldetails();
            }
        }
    }

    protected void clearControls()
    {
        txtCountryID.Text = "";
        txtCountryName.Text = "";
    }

    protected void filldetails()
    {
        string _countryID = txtCountryID.Text.Trim().ToUpper();
        SqlParameter p1 = new SqlParameter("@cid",SqlDbType.VarChar);
        p1.Value = _countryID;
        string query = "TF_GetCountryDetails";
        TF_DATA objdata = new TF_DATA();
        DataTable dt = objdata.getData(query,p1);
        if (dt.Rows.Count > 0)
        {
            txtCountryName.Text = dt.Rows[0]["CountryName"].ToString();
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewCountryMaster.aspx?PageHeader=Country Master View", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string query = "TF_UpdateCountryMaster";
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy");
        string _mode = Request.QueryString["mode"].Trim();
        string _cid = txtCountryID.Text.Trim().ToUpper();
        string _cname = txtCountryName.Text.Trim().ToUpper();
        string _Status = "";
        if (rdbActive.Checked)
        {
            _Status = "Active";
        }
        if (rdbInActive.Checked)
        {
            _Status = "In-Active";
        }

        SqlParameter cid = new SqlParameter("@cid", SqlDbType.VarChar);
        SqlParameter cname = new SqlParameter("@cname", SqlDbType.VarChar);
        SqlParameter mode = new SqlParameter("@cmode", SqlDbType.VarChar);
        SqlParameter user = new SqlParameter("@user", SqlDbType.VarChar);
        SqlParameter date = new SqlParameter("@date", SqlDbType.VarChar);
        SqlParameter Status = new SqlParameter("@status", SqlDbType.VarChar);
        Status.Value = _Status;

        cid.Value = _cid;
        cname.Value = _cname;
        mode.Value = _mode;
        user.Value = _userName;
        date.Value = _uploadingDate;

        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData(query, cid, cname, mode, user, date, Status);
        string _script = "";
        if (_result == "added")
        {
            _script = "window.location='TF_ViewCountryMaster.aspx?PageHeader=Country Master View&result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", _script, true);
        }
        else if (_result == "updated")
        {
            _script = "window.location='TF_ViewCountryMaster.aspx?PageHeader=Country Master View&result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", _script, true);
        }
        else
        {
            labelMessage.Text = _result;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewCountryMaster.aspx?PageHeader=Country Master View", true);
    }
}