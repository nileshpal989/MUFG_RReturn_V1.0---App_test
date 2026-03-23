using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class INW_HelpPrincipal_Officer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGrid();
        }
    }

    protected void fillGrid()
    {
        string search = "";
        string Branch = "";
        //if (Branch == "All Branches")
        //    Branch = "Mumbai";

        string _query = "Help_Principal_Officer";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@Search", SqlDbType.VarChar);
        p1.Value = search;

        SqlParameter p2 = new SqlParameter("@Branch", SqlDbType.VarChar);
        p2.Value = Branch ;

        DataTable dt = objData.getData(_query, p1,p2 );
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;

            GridViewUserList.DataSource = dt.DefaultView;
            GridViewUserList.DataBind();
            GridViewUserList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else
        {
            GridViewUserList.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }

    }
    protected void GridViewUserList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
    }
    protected void GridViewUserList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocNo = new Label();
            lblDocNo = (Label)e.Row.FindControl("lblDocNo");

            Label lblDesc = new Label();
            lblDesc = (Label)e.Row.FindControl("lblCustName");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "";

                pageurl = "window.opener.selectPO_Off('" + lblDocNo.Text + "','" + lblDesc.Text + "' );window.opener.EndRequest();window.close();";
                //string pageurl1 = "window.opener.selectUser1('" + lblDocNo.Text + "');window.opener.EndRequest();window.close();";


                if (i != 2)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;

                //return false;
            }
        }
    }

    protected void GridViewUserList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TabIndex = -1;
            //e.Row.Attributes["onmouseover"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            //e.Row.Attributes["onmouseout"] = string.Format("javascript:DisSelectRow(this, {0});", e.Row.RowIndex);

            e.Row.ToolTip = "Click to select row.";
        }

    }

    protected void btngo_Click(object sender, EventArgs e)
    {

        string search = txtSearch.Text;

        string Branch = Request.QueryString["Branch"];

        string _query = "Help_Principal_Officer";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@Search", SqlDbType.VarChar);
        p1.Value = search;

        SqlParameter p2 = new SqlParameter("@Branch", SqlDbType.VarChar);
        p2.Value = Branch;

        DataTable dt = objData.getData(_query, p1, p2);


        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;

            GridViewUserList.DataSource = dt.DefaultView;
            GridViewUserList.DataBind();
            GridViewUserList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else
        {
            GridViewUserList.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
}


