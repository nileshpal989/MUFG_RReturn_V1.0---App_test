using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;

public partial class TF_PurposeCodeLookUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // hndsessionid.Value = Request.QueryString["strnew"].ToString();
            rbtnType.Visible = false;
            rbtnType.SelectedValue = "2";
            fillCustomer();
            txtSearch.Attributes.Add("onkeypress", "return submitForm(event);");
            btngo.Attributes.Add("onclick", "return validateSearch();");
            btnCancel.Attributes.Add("onclick", "window.close(); return false;");
            // txtSearch.Focus();

            lstcustList.Focus();
            lstcustList.SelectedIndex = 0;
        }

    }
    protected void fillCustomer()
    {
        //TF_DATA objData = new TF_DATA();
        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        string _query = "TF_GetPurposeCodeMasterList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);

        if (dt.Rows.Count > 0)
        {
            if (Request.QueryString["c_ID"] != null)
            {
                TF_DATA objDatap = new TF_DATA();
                if (Request.QueryString["c_ID"].ToString() == "0")
                {
                    BindCustCodes(dt);
                }
                else
                {
                    SqlParameter par1 = new SqlParameter("@purposecode", SqlDbType.VarChar);
                    par1.Value = Request.QueryString["c_ID"].ToString();

                    string _query1 = "TF_GetPurposeCodeMasterDetails";
                    DataTable dtp = objDatap.getData(_query1, par1);
                    if (dtp.Rows.Count > 0)
                    {
                        lstcustList.DataSource = dtp.DefaultView;
                        if (rbtnType.SelectedValue == "1")
                        {
                            lstcustList.DataValueField = "purposeID";
                            lstcustList.DataTextField = "purposeID";
                        }
                        else
                        {
                            lstcustList.DataValueField = "purposeID";
                            lstcustList.DataTextField = "newdesc";
                        }
                        lstcustList.DataBind();
                    }
                    else
                    {

                        ListItem li = new ListItem();
                        li.Text = "No record(s) found";
                        li.Value = "0";
                        lstcustList.Items.Clear();
                        lstcustList.Items.Insert(0, li);
                    }
                }
            }
            else
            {
                lstcustList.DataSource = dt.DefaultView;
                if (rbtnType.SelectedValue == "1")
                {
                    lstcustList.DataValueField = "purposeID";
                    lstcustList.DataTextField = "purposeID";
                }
                else
                {
                    lstcustList.DataValueField = "purposeID";

                    lstcustList.DataTextField = "newdesc";
                }
                lstcustList.DataBind();
            }
        }
        else
        {
            ListItem li = new ListItem();
            li.Text = "No record(s) found";
            li.Value = "0";
            lstcustList.Items.Clear();
            lstcustList.Items.Insert(0, li);
        }
    }

    public void BindCustCodes(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            lstcustList.DataSource = dt.DefaultView;
            if (rbtnType.SelectedValue == "1")
            {
                lstcustList.DataValueField = "purposeID";
                lstcustList.DataTextField = "purposeID";
            }
            else
            {
                lstcustList.DataValueField = "purposeID";
                lstcustList.DataTextField = "newdesc";
            }
            lstcustList.DataBind();
        }
        else
        {
            ListItem li = new ListItem();
            li.Text = "No record(s) found";
            li.Value = "0";
            lstcustList.Items.Clear();
            lstcustList.Items.Insert(0, li);
        }
    }

    protected void btngo_Click(object sender, EventArgs e)
    {
        //TF_DATA objData = new TF_DATA();
        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        string _query = "TF_GetPurposeCodeMasterList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        BindCustCodes(dt);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (lstcustList.SelectedValue != "0")
        {
            string pcId = "";
            string pcdes = "";

            foreach (ListItem li in lstcustList.Items)
            {
                if (li.Selected)
                {
                    pcId = pcId + li.Value.ToString().Trim();
                    pcdes = pcdes + li.Text.ToString().Trim();
                }
            }
            if (pcId != "")
            {
                StringBuilder mystring = new StringBuilder();
                mystring.Append("window.opener.selectPurpose('" + pcId + "');");
                mystring.Append("window.opener.EndRequest();");
                mystring.Append("window.close();");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SelectEMail", mystring.ToString()
                    , true);
            }
            else
            {
                if (pcId == "No record(s) found")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "showError", "window.opener.alert('No record(s) to select.');", true);
                }
                else
                { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "showError", "window.opener.alert('Select any one Purpose Code.');", true); }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "showError", "window.opener.alert('Record can not be selected.');", true);
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Close", "window.close();", true);
    }

    protected void rbtnType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtSearch.Text == "")
        {
            if (rbtnType.SelectedValue == "1")
            {
                VisibleInvisibleOfSection();
            }
            else if (rbtnType.SelectedValue == "2")
            {
                VisibleInvisibleOfSection();
            }
        }
        else
        {
            string search = txtSearch.Text.Trim();
            SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
            p1.Value = search;

            string _query = "TF_GetPurposeCodeMasterList";
            TF_DATA objData = new TF_DATA();
            DataTable dt = objData.getData(_query, p1);
            BindCustCodes(dt);
        }
    }

    public void VisibleInvisibleOfSection()
    {
        if (rbtnType.SelectedValue == "1")
        {
            lstcustList.SelectionMode = ListSelectionMode.Single;
            fillCustomer();
        }
        else if (rbtnType.SelectedValue == "2")
        {
            lstcustList.SelectionMode = ListSelectionMode.Single;
            fillCustomer();
        }
    }
}


