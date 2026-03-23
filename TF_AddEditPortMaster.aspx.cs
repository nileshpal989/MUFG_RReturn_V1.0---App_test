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

public partial class RBI_AddEditPortMaster : System.Web.UI.Page
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
                    Response.Redirect("TF_ViewPortMaster.aspx?PageHeader=Port Code Master View", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtPortID.Text = Request.QueryString["portcode"].Trim();
                        txtPortID.Enabled = false;
                        fillDetails(Request.QueryString["portcode"].Trim());
                        txtPortName.Focus();
                    }
                    else
                    {
                        txtPortID.Enabled = true;
                        txtPortID.Focus();
                    }
                }
                btnSave.Attributes.Add("onclick", "return validateSave();");

            }
        }
        //txtPortName.Attributes.Add("onkeydown", "return RestrictSplChar();");
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewPortMaster.aspx?PageHeader=Port Code Master View", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _mode = Request.QueryString["mode"].Trim();
        string _portCode = txtPortID.Text.Trim();
        string _portName = txtPortName.Text.Trim();

        TF_DATA objSave = new TF_DATA();
        string _query = "TF_UpdatePortCodeMaster";

        SqlParameter p1 = new SqlParameter("@mode", SqlDbType.VarChar);
        p1.Value = _mode;

        SqlParameter p2 = new SqlParameter("@portcode", SqlDbType.VarChar);
        p2.Value = _portCode;

        SqlParameter p3 = new SqlParameter("@portname", SqlDbType.VarChar);
        p3.Value = _portName;

        SqlParameter p4 = new SqlParameter("@user", SqlDbType.VarChar);
        p4.Value = _userName;

        SqlParameter p5 = new SqlParameter("@uploadingdate", SqlDbType.VarChar);
        p5.Value = _uploadingDate;

        _result = objSave.SaveDeleteData(_query,p1,p2,p3,p4,p5);
        string _script = "";
        if (_result == "added")
        {
            _script = "window.location='TF_ViewPortMaster.aspx?PageHeader=Port Code Master View&result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                _script = "window.location='TF_ViewPortMaster.aspx?PageHeader=Port Code Master View&result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = _result;
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewPortMaster.aspx?PageHeader=Port Code Master View", true);
    }
    protected void clearControls()
    {
        txtPortID.Text = "";
        txtPortName.Text = "";
    }
    protected void fillDetails(string _portCode)
    {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_GetPortCodeMasterDetails";

        SqlParameter p1 = new SqlParameter("@portcode", SqlDbType.VarChar);
        p1.Value = _portCode;

        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            txtPortName.Text = dt.Rows[0][1].ToString().Trim();
        }
    }
}
