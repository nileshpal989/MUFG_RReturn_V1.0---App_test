using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Drawing;
public partial class Reports_RRETURNReports_rptRRETURN_Purpose_Control_Totals : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            clearControls();
            txtFromDate.Focus();
            fillBranch();
            ddlBranch.SelectedValue = Session["userADCode"].ToString();
            //ddlBranch.Enabled = false;
            PageHeader.Text = Request.QueryString["PageHeader"].ToString();
            txtFromDate.Text = Session["FrRelDt"].ToString();
            txtToDate.Text = Session["ToRelDt"].ToString();
            btnSave.Attributes.Add("onclick", "return validateSave();");
        }
    }
    protected void clearControls()
    {
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtFromDate.Focus();
        labelMessage.Text = "";
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
    }
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        var Report = PageHeader.Text;
            var Branch = ddlBranch.SelectedValue;
            var from = txtFromDate.Text;
            var to = txtToDate.Text;
            Response.Redirect("ViewrptRRETURN_Purpose_Control_Totals.aspx?PageHeader=" + Report + "&frm=" + from + "&to=" + to + "&branch=" + Branch + "&Report=" + Report);            
    }
}