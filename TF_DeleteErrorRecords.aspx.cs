using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class TF_DeleteErrorRecords : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
            Response.Redirect("TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                hdnRole.Value = Session["userRole"].ToString();
                fillBranch();
                ddlRefNo.SelectedValue = Session["userADCode"].ToString();
                ddlRefNo.Enabled = false;
                txtFromDate.Text = "01/03/2014";
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                fillCounts();
                btnDelete_Doc.Attributes.Add("onclick", "return validateDelete(1);");
                btnDelete_Realized.Attributes.Add("onclick", "return validateDelete(2);");
                txtFromDate.Attributes.Add("onblur", "isValidDate(" + txtFromDate.ClientID + "," + "' From Ship. Date'" + ");");
                txtToDate.Attributes.Add("onblur", "isValidDate(" + txtToDate.ClientID + "," + "' To Ship. Date'" + ");");

            }
        }
    }
    protected void fillCounts()
    {
        TF_DATA objdata = new TF_DATA();
        string _query = "TF_Get_XML_File_Counts";
        SqlParameter p1 = new SqlParameter("@fromDate",txtFromDate.Text);
        SqlParameter p2 = new SqlParameter("@toDate",txtToDate.Text);
        SqlParameter p3 = new SqlParameter("@adCode",ddlRefNo.SelectedValue);

        DataTable dt = objdata.getData(_query,p1,p2,p3);
        if (dt.Rows.Count > 0)
        {
            txtdoc_pending.Text = dt.Rows[0]["Doc_Pending"].ToString();
            txtdoc_generated.Text = dt.Rows[0]["Doc_Generated"].ToString();
            txtdoc_succeeded.Text = dt.Rows[0]["Doc_Succeed"].ToString();
            txtDoc_Failed.Text = dt.Rows[0]["Doc_Failed"].ToString();
            txtrealized_pending.Text = dt.Rows[0]["Realized_Pending"].ToString();
            txtrealized_generated.Text = dt.Rows[0]["Realized_Generated"].ToString();
            txtrealized_succeeded.Text = dt.Rows[0]["Realized_Succeed"].ToString();
            txtRealized_Failed.Text = dt.Rows[0]["Realized_Failed"].ToString();
        }
       
    }
    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlRefNo.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlRefNo.DataSource = dt.DefaultView;
            ddlRefNo.DataTextField = "BranchName";
            ddlRefNo.DataValueField = "AuthorizedDealerCode";
            ddlRefNo.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlRefNo.Items.Insert(0, li);
    }
    protected void btnDelete_Doc_Click(object sender, EventArgs e)
    {
        TF_DATA objdata = new TF_DATA();
        string _query = "TF_Delete_ACK_ErrorRecords";
        SqlParameter p1 = new SqlParameter("@type", "DocBill");
        SqlParameter p2 = new SqlParameter("@adCode", ddlRefNo.SelectedValue);
        SqlParameter p3 = new SqlParameter("@fromDate", txtFromDate.Text.Trim());
        SqlParameter p4 = new SqlParameter("@toDate", txtToDate.Text.Trim()); 
        
        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "confirm", "confirm('Are you sure you want to delete Error Records?')", true);
        string _result = objdata.SaveDeleteData(_query, p1,p2,p3,p4);
        if (_result == "DELETED")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Deleted')", true);
            fillCounts();
        }
        else
            lblmessage.Text = _result;
    }
    protected void btnDelete_Realized_Click(object sender, EventArgs e)
    {
        TF_DATA objdata = new TF_DATA();
        string _query = "TF_Delete_ACK_ErrorRecords";
        SqlParameter p1 = new SqlParameter("@type", "Realized");
        SqlParameter p2 = new SqlParameter("@adCode",ddlRefNo.SelectedValue);
        SqlParameter p3 = new SqlParameter("@fromDate", txtFromDate.Text.Trim());
        SqlParameter p4 = new SqlParameter("@toDate", txtToDate.Text.Trim()); 
        
        string _result = objdata.SaveDeleteData(_query, p1,p2,p3,p4);
        if (_result == "DELETED")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Deleted')", true);
            fillCounts();
        }
        else
            lblmessage.Text = _result;
    }
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        fillCounts();
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        fillCounts();
    }
}