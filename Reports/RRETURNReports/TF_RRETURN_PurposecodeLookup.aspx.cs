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

public partial class Reports_RRETURNReports_TF_RRETURN_PurposecodeLookup : System.Web.UI.Page
{
    TF_DATA erp = new TF_DATA();
    string query;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack != true)
        {
            grdDataBind();
            txtsearch.Focus();
        }
        txtsearch.Focus();
    }
    public void grdDataBind()
    {
        string fromdate = Request.QueryString["from"].ToString();
        string todate = Request.QueryString["to"];
        string adcode = Request.QueryString["adcode"];
        SqlParameter p1 = new SqlParameter("@fromdate", SqlDbType.VarChar);
        p1.Value = fromdate;
        SqlParameter p2 = new SqlParameter("@todate", SqlDbType.VarChar);
        p2.Value = todate;
        SqlParameter p3 = new SqlParameter("@adcode", SqlDbType.VarChar);
        p3.Value = adcode;
        SqlParameter p4 = new SqlParameter("@search", SqlDbType.VarChar);
        p4.Value = txtsearch.Text;
        string _query = "TF_RET_HelpPurposeCodeMstr";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2,p3,p4);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            grdsearch.DataSource = dt.DefaultView;
            grdsearch.DataBind();
            grdsearch.Visible = true;
            //rowGrid.Visible = true;
            labelMessage.Visible = false;
        }
        else
        {
            grdsearch.Visible = false;
            //rowGrid.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }

        //query = "TF_RET_HelpPurposeCodeMstr";
        // da=new SqlDataAdapter(query,
        //DataSet ds = erp.databind(query);
        //try
        //{
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        grdsearch.DataSource = ds.Tables[0];
        //        grdsearch.DataBind();
        //    }
        //}
        //catch (Exception)
        //{
        //    throw;
        //}
    }
    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {
        if (txtsearch.Text != "")
        {
            query = "TF_RET_HelpPurCodeSearchId";
            DataSet ds = erp.databind1para(query, txtsearch.Text.Trim());
            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdsearch.DataSource = ds.Tables[0];
                    grdsearch.DataBind();
                    txtsearch.Focus();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }
        else if (txtsearch.Text == "")
        {
            // MessageBox.Show("Please Enter Customer Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Enter Purpose Code');", true);
            grdDataBind();
            txtsearch.Focus();
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        txtsearch_TextChanged(this, null);
    }
    protected void grdsearch_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
        {
            e.Row.TabIndex = -1;
            e.Row.Attributes["onclick"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onkeydown"] = "javascript:return SelectSibling(event);";
            e.Row.Attributes["onselectstart"] = "javascript:return false;";
            e.Row.ToolTip = "Click to select row and then navigate";
        }
    }
}