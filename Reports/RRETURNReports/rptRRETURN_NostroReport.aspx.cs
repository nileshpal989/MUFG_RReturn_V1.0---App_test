using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
public partial class Reports_RRETURNReports_rptRRETURN_NostroReport : System.Web.UI.Page
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
                //  fillCurrency();
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                hdnBranchName.Value = ddlBranch.SelectedItem.Text;
                //ddlBranch.Enabled = false;
                //PageHeader.Text = Request.QueryString["PageHeader"].ToString();
                txtfromDate.Text = Session["FrRelDt"].ToString();
                txtToDate.Text = Session["ToRelDt"].ToString();
                //PageHeader.Text = Request.QueryString["PageHeader"].ToString();
                string header = Request.QueryString["PageHeader"];
                if (!string.IsNullOrEmpty(header))
                {
                    PageHeader.Text = Server.HtmlEncode(header);
                }
                btnSave.Attributes.Add("onclick", "return Generate();");
                btnAuthSignList.Attributes.Add("onclick", "return OpenAuthSignCodeList('mouseClick');");
                btnCurrencyList.Attributes.Add("onclick", "return OpenCurrencyList('6');");
                //  txtCurrency.Attributes.Add("onchange", "return changeCurrencyDesc();");
            }
            txtfromDate.Focus();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "toogleDisplay();", true);
        }
    }
    protected void btnAuthSignCode_Click(object sender, EventArgs e)
    {
        if (hdnAuthSignCode.Value != "")
        {
            txtAuthSign.Text = hdnAuthSignCode.Value;
            fillAuthCodeDescription();
            txtAuthSign.Focus();
        }
    }
    private void fillAuthCodeDescription()
    {
        if (txtAuthSign.Text != "")
        {
            lblAuthSignName.Text = "";
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@AuthName", SqlDbType.VarChar);
            p1.Value = txtAuthSign.Text;
            string _query = "TF_rptGetAuthSignDetails";
            DataTable dt = objData.getData(_query, p1);
            if (dt.Rows.Count > 0)
            {
                lblAuthSignName.Text = dt.Rows[0]["Authorised_Signatory"].ToString().Trim();
                txtCurrency.Focus();
            }
            else
            {
                txtAuthSign.Text = "";
                lblAuthSignName.Text = "<font style='color:red'>" + "Invalid Id" + "</font>";
                txtAuthSign.Focus();
            }
        }
        else
        {
            lblAuthSignName.Text = "";
        }
    }
    //protected void fillCurrency()
    //{
    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
    //    p1.Value = "";
    //    string _query = "TF_GetCurrencyList";
    //    DataTable dt = objData.getData(_query, p1);
    //    ddlCurrency.Items.Clear();
    //    ListItem li = new ListItem();
    //    li.Value = "0";
    //    if (dt.Rows.Count > 0)
    //    {
    //        li.Text = "Select";
    //        ddlCurrency.DataSource = dt.DefaultView;
    //        ddlCurrency.DataTextField = "C_Code";
    //        ddlCurrency.DataValueField = "C_DESCRIPTION";
    //        ddlCurrency.DataBind();
    //    }
    //    else
    //        li.Text = "No record(s) found";
    //    ddlCurrency.Items.Insert(0, li);
    //}
    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        if (dt.Rows.Count > 0)
        {
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
    }
    protected void btnCurr_Click(object sender, EventArgs e)
    {
        if (hdnCurId.Value != "")
        {
            switch (hdnCurrencyHelpNo.Value)
            {
                case "6":
                    txtCurrency.Text = hdnCurId.Value;
                    lblCurDesc.Text = hdnCurName.Value;
                    txtCurrency.Focus();
                    break;
            }
        }
    }
    protected void txtAuthSign_TextChanged(object sender, EventArgs e)
    {
        fillAuthCodeDescription();
        //txtAuthSign.Focus();
    }
    protected void txtCurrency_TextChanged(object sender, EventArgs e)
    {
        if (txtCurrency.Text != "")
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
            p1.Value = txtCurrency.Text;
            string _query = "TF_GetCurrencyList";
            DataTable dt = objData.getData(_query, p1);
            txtCurrency.Text = "";
            if (dt.Rows.Count > 0)
            {
                txtCurrency.Text = dt.Rows[0]["C_Code"].ToString();
                lblCurDesc.Text = dt.Rows[0]["C_DESCRIPTION"].ToString();
                txtserialNo.Focus();
            }
            else
            {
                lblCurDesc.Text = "<font style=color:red>" + "Invalid Currency" + "</font>";
                txtCurrency.Text = "";
                txtCurrency.Focus();
            }
        }
        else
        {
            lblCurDesc.Text = "";
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        var Branch = ddlBranch.SelectedValue;
        var Report = PageHeader.Text;
        Response.Redirect("View_rptRRETURN_NostroReport.aspx?PageHeader=" + Report + "&frm=" + txtfromDate.Text + "&to=" + txtToDate.Text + "&Branch=" + Branch + "&Currency=" + txtCurrency.Text + "&Report=" + Report + "&txtAuthSign=" + txtAuthSign.Text + "&txtserialNo=" + txtserialNo.Text);
    }
}