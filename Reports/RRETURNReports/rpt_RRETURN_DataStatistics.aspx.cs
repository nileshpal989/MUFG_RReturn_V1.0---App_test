using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class Reports_RRETURNReports_rpt_RRETURN_DataStatistics : System.Web.UI.Page
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
                txtFromDate.Focus();
                txtFromDate.Text = Session["FrRelDt"].ToString();
                txtToDate.Text = Session["ToRelDt"].ToString();
                btnSave.Attributes.Add("onclick", "return validateSave();");
                PageHeader.Text = Request.QueryString["PageHeader"].ToString();
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                //ddlBranch.Enabled = false;
            }
        }
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
        var from = txtFromDate.Text;
        var to = txtToDate.Text;


        Response.Redirect("View_rpt_RRETURN_DataStatistics.aspx?PageHeader=" + Report + "&frm=" + from + "&to=" + to);
    }
}