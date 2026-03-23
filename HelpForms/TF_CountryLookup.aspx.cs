using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class TF_CountryLookup : System.Web.UI.Page
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

        query = "HelpCountryMstr";
        // da=new SqlDataAdapter(query,
        DataSet ds = erp.databind(query);
        try
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdsearch.DataSource = ds.Tables[0];
                grdsearch.DataBind();
            }
        }
        catch (Exception)
        {

            //throw;
        }


    }
    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {
        if (txtsearch.Text != "")
        {
            query = "HelpCountrySearchId";
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
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Enter Sector  Name');", true);
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
        if (e.Row.RowType == DataControlRowType.DataRow &&
   (e.Row.RowState == DataControlRowState.Normal ||
    e.Row.RowState == DataControlRowState.Alternate))
        {
            e.Row.TabIndex = -1;
            e.Row.Attributes["onclick"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onkeydown"] = "javascript:return SelectSibling(event);";
            e.Row.Attributes["onselectstart"] = "javascript:return false;";
            e.Row.ToolTip = "Click to select row and then navigate";
        }
    }
}
