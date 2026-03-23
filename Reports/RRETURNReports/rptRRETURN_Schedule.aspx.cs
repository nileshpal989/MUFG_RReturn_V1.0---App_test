using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
public partial class Reports_RRETURNReports_rptRRETURN_Schedule : System.Web.UI.Page
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
                // fillCurrency();
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                //ddlBranch.Enabled = false;
                PageHeader.Text = Request.QueryString["PageHeader"].ToString();
                txtfromDate.Text = Session["FrRelDt"].ToString();
                txtToDate.Text = Session["ToRelDt"].ToString();
                // txtCurrency.Attributes.Add("onkeydown", "return CustId(event);");
                //  btCurList.Attributes.Add("onclick", "return Custhelp();");
                PageHeader.Text = Request.QueryString["PageHeader"].ToString();
                rdbAllCurrency.Attributes.Add("onclick", "return toogleDisplay();");
                rdbSelectedCurrency.Attributes.Add("onclick", "return toogleDisplay();");
                rdbSchedule1A.Attributes.Add("onclick", "return toogleDisplay();");
                rdbSchedule1B.Attributes.Add("onclick", "return toogleDisplay();");
                btnSave.Attributes.Add("onclick", "return Generate();");
                btnAuthSignList.Attributes.Add("onclick", "return OpenAuthSignCodeList('mouseClick');");
                btnCurrencyList.Attributes.Add("onclick", "return OpenCurrencyList('6');");
                //  txtCurrency.Attributes.Add("onchange", "return changeCurrencyDesc();");
                rdbAllCurrency.Visible = true;
                rdbAllCurrency.Checked = true;
                rdbSelectedCurrency.Visible = true;
                rdbSchedule1A.Visible = true;
                rdbSchedule1A.Checked = true;
                rdbSchedule1B.Visible = true;
                if (PageHeader.Text == "Schedule 1")
                {
                    rdbSchedule1A.Text = "Schedule 1 A [Imports Below Rs 5,00,000]";
                    rdbSchedule1B.Text = "Schedule 1 B [Imports Above Rs 5,00,000]";
                    rdbSchedule1A.Visible = true;
                    rdbSchedule1A.Checked = true;
                    rdbSchedule1B.Visible = true;
                    rdbSchedule1C.Visible = false;
                    rdbSchedule1D.Visible = false;
                }
                if (PageHeader.Text == "Schedule 2")
                {
                    rdbSchedule1A.Text = "Schedule 2 A [Other than Imports Below Rs 5,00,000]";
                    rdbSchedule1B.Text = "Schedule 2 B [Other than Imports Above Rs 5,00,000]";
                    rdbSchedule1A.Visible = true;
                    rdbSchedule1A.Checked = true;
                    rdbSchedule1B.Visible = true;
                    rdbSchedule1C.Visible = false;
                    rdbSchedule1D.Visible = false;
                }
                if (PageHeader.Text == "Schedule 3/4/5/6")
                {
                    rdbSchedule1A.Text = "Schedule 3 [Full Realisation]";
                    rdbSchedule1B.Text = "Schedule 4 [Part Realisation]";
                    rdbSchedule1C.Text = "Schedule 5 [Receipt of Full Advance]";
                    rdbSchedule1D.Text = "Schedule 6 [Receipt of Partial Advance]";
                    rdbSchedule1A.Visible = true;
                    rdbSchedule1A.Checked = true;
                    rdbSchedule1B.Visible = true;
                    rdbSchedule1C.Visible = true;
                    rdbSchedule1D.Visible = true;
                }
                if (PageHeader.Text == "Supplementary Statement of Purchases")
                {
                    rdbSchedule1A.Text = "Other than Exports Below Rs 5,00,000";
                    rdbSchedule1B.Text = "Other than Exports Above Rs 5,00,000";
                    rdbSchedule1A.Visible = true;
                    rdbSchedule1A.Checked = true;
                    rdbSchedule1B.Visible = true;
                    rdbSchedule1C.Visible = false;
                    rdbSchedule1D.Visible = false;
                }
            }
            // txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            ddlBranch.Focus();
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
            }
            else
            {
                txtAuthSign.Text = "";
                lblAuthSignName.Text = "Invalid Id";
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
    //    txtCurrency.Items.Clear();
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
        txtAuthSign.Focus();
    }
    protected void txtCurrency_TextChanged(object sender, EventArgs e)
    {
        if (txtCurrency.Text != "")
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
            p1.Value = txtCurrency.Text;
            string _query = "HelpCurMstr";
            DataTable dt = objData.getData(_query, p1);
            txtCurrency.Text = "";
            if (dt.Rows.Count > 0)
            {
                txtCurrency.Text = dt.Rows[0]["C_Code"].ToString();
                lblCurDesc.Text = dt.Rows[0]["C_DESCRIPTION"].ToString();
            }
            else
            {
                lblCurDesc.Text = "Invalid Currency";
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
        var Report=PageHeader.Text;
        var Currency="";
        var Branch = ddlBranch.SelectedValue;

        if (rdbAllCurrency.Checked == true)
                Currency = "All";
            else
                Currency = txtCurrency.Text;
            var rptPurType="";            
            if (Report == "Schedule 1") {
                if (rdbSchedule1A.Checked == true) {
                    rptPurType = "Schedule1A";
                }
                else if (rdbSchedule1B.Checked == true) {
                    rptPurType = "Schedule1B";
                }
            }
            else if (Report == "Schedule 2") {
                if (rdbSchedule1A.Checked == true) {
                    rptPurType = "Schedule2A";
                }
                else if (rdbSchedule1B.Checked == true) {
                    rptPurType = "Schedule2B";
                }
            }
            else if (Report == "Schedule 3/4/5/6") {
                if (rdbSchedule1A.Checked == true) {
                    rptPurType = "Schedule3";
                }
                else if (rdbSchedule1B.Checked == true) {
                    rptPurType = "Schedule4";
                }
                else if (rdbSchedule1C.Checked == true) {
                    rptPurType = "Schedule5";
                }
                else if (rdbSchedule1D.Checked == true) {
                    rptPurType = "Schedule6";
                }
            }
            else if (Report == "Supplementary Statement of Purchases") {
                if (rdbSchedule1A.Checked == true) {
                    rptPurType = "SuppST_Below";
                }
                else if (rdbSchedule1B.Checked == true) {
                    rptPurType = "SuppST_Above";
                }
            }
            Response.Redirect("View_rptRRETURN_Schedule.aspx?PageHeader=" + Report + "&frm=" + txtfromDate.Text + "&to=" + txtToDate.Text + "&Branch=" + Branch + "&Currency=" + Currency + "&Report=" + Report + "&txtAuthSign=" + txtAuthSign.Text + "&rptPurType=" + rptPurType);
            
    }
}