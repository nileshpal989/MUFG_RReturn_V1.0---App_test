using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class TF_AdminAuditTrail : System.Web.UI.Page
{
    string pur = "";
    string CustAcNo = "";
    //string Guarantee = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
            Response.Redirect("~/TF_Login.aspx?PageHeader=Login&sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
            if (Session["userRole"].ToString() != "Admin")
            {
                System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
                Response.Redirect("TF_Login.aspx?PageHeader=Login&sessionout=yes&sessionid=" + lbl.Value, true);
            }
            else
            {
                //  txtPurposeCode.Attributes.Add("onkeydown", "return PurposeId(event)");
                // BtnPurposeList.Attributes.Add("onclick", "return PurposeCodehelp()");
                if (!IsPostBack)
                {
                    clearControls();
                    //       rdbAllPurposeCode.Checked = true;
                    ddlBranch.Focus();
                    txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    btnSave.Attributes.Add("onclick", "return validateSave();");
                    PageHeader.Text = Request.QueryString["PageHeader"].ToString();
                    //      rdbTypewise.Checked = true;
                    fillBranch();
                    ddlBranch.SelectedValue = Session["userADCode"].ToString();
                    ddlBranch.Enabled = false;
                    fillCombo();
                }
            }
    }
    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_RET_GetBranchandADcodeList";
        DataTable dt = objData.getData(_query);
        ddlBranch.Items.Clear();
        if (dt.Rows.Count > 0)
        {
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
    }
    protected void clearControls()
    {
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtFromDate.Focus();
        rdbAllTypes.Focus();
        rdbAllTypes.Checked = true;
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
    }
    protected void rdbAllPurposeCode_CheckedChanged(object sender, EventArgs e)
    {
        rdbAllPurposeCode.Focus();
        //   rdbAllPurposeCode.Checked = true;
        //     rdbSelectedPurposeCode.Checked = false;
        //    txtPurposeCode.Visible = false;
        //    BtnPurposeList.Visible = false;
        //    lblPurposeCode.Visible = false;
        //    txtPurposeCode.Text = "";
        //    lblPurposeCode.Text = "";
        //     dropDownListSelectedType.Visible = false;
        //     Purpose.Visible = false;
        //     type.Visible = false;
    }
    protected void rdbSelectedPurposeCode_CheckedChanged(object sender, EventArgs e)
    {
        rdbSelectedPurposeCode.Focus();
        rdbSelectedPurposeCode.Checked = true;
        rdbAllPurposeCode.Checked = false;
        //  txtPurposeCode.Visible = true;
        //  BtnPurposeList.Visible = true;
        //   lblPurposeCode.Visible = true;
        //   dropDownListSelectedType.Visible = false;
        //   Purpose.Visible = true;
        //   type.Visible = false;
    }
    protected void txtPurposeCode_TextChanged(object sender, EventArgs e)
    {
        fillPurposeIdDescription();
        //   txtPurposeCode.Focus();
    }
    public void fillPurposeIdDescription()
    {
        //   lblPurposeCode.Text = "";
        //   string purposeid = txtPurposeCode.Text.Trim();
        //   TF_DATA objData = new TF_DATA();
        // SqlParameter p1 = new SqlParameter("@purposeid", SqlDbType.VarChar);
        //   p1.Value = purposeid;
        //  string _query = "TF_RReturn_GetPurposeMasterDetails";
        //   DataTable dt = objData.getData(_query, p1);
        //   if (dt.Rows.Count > 0)
        //    {
        //      lblPurposeCode.Text = dt.Rows[0]["description"].ToString().Trim();
        //   }
        //    else
        // {
        //     txtPurposeCode.Text = "";
        //  lblPurposeCode.Text = "";
        //  }
    }
    protected void rdbTypewise_CheckedChanged(object sender, EventArgs e)
    {
        {
            //if (rdbTypewise.Checked == true)
            //{
            //    rdbAllTypes.Visible = true;
            //    rdbSelectedType.Visible = true;
            //    rdbAllTypes.Checked = true;
            //    rdbSelectedType.Checked = false;
            //    type.Visible = false;
            //    dropDownListSelectedType.Visible = false;
            //    rdbAllPurposeCode.Visible = false;
            //    rdbSelectedPurposeCode.Visible = false;
            //    Purpose.Visible = false;
            //    txtPurposeCode.Visible = false;
            //    lblPurposeCode.Visible = false;
            //    BtnPurposeList.Visible = false;
            //    txtPurposeCode.Text = "";
            //}
            //else
            //{
            //    rdbAllTypes.Visible = false;
            //    rdbSelectedType.Visible = false;
            //  //  dropDownListSelectedType.Visible = false;
            //}
            // rdbTypewise.Focus();
        }
    }
    protected void rdbPurposeCodewise_CheckedChanged(object sender, EventArgs e)
    {
        //if (rdbPurposeCodewise.Checked == true)
        //{
        //    rdbAllPurposeCode.Visible = true;
        //    rdbSelectedPurposeCode.Visible = true;
        //    rdbAllPurposeCode.Checked = true;
        //    rdbSelectedPurposeCode.Checked = false;
        //    rdbAllTypes.Visible = false;
        //    rdbSelectedType.Visible = false;
        //    type.Visible = false;
        //    dropDownListSelectedType.Visible = false;
        //}
        //else
        //{
        //    rdbAllPurposeCode.Visible = false;
        //    rdbSelectedPurposeCode.Visible = false;
        //}
        //rdbPurposeCodewise.Focus();
    }
    protected void dropDownListSelectedType_SelectedIndexChanged(object sender, EventArgs e)
    {
        // dropDownListSelectedType.Focus();
    }
    protected void fillCombo()
    {
        //dropDownListSelectedType.Items.Add("EXP");
        //dropDownListSelectedType.Items.Add("IMP");
        //dropDownListSelectedType.Items.Add("INW");
        //dropDownListSelectedType.Items.Add("OTW");
        //dropDownListSelectedType.Items.Add("OTH");
    }
    protected void rdbAllTypes_CheckedChanged(object sender, EventArgs e)
    {
        rdbAllTypes.Focus();
        rdbAllTypes.Checked = true;
        //// rdbSelectedType.Checked = false;
        // dropDownListSelectedType.Visible = false;
        ////  rdbAllPurposeCode.Checked = false;
        ////    rdbSelectedPurposeCode.Checked = false;
        //     txtPurposeCode.Visible = false;
        //   BtnPurposeList.Visible = false;
        //   lblPurposeCode.Visible = false;
        //  type.Visible = false;
        //  txtPurposeCode.Text = "";
        //  lblPurposeCode.Text = "";
        //  Purpose.Visible = false;
    }
    protected void rdbSelectedType_CheckedChanged(object sender, EventArgs e)
    {
        rdbSelectedType.Focus();
        ////   rdbSelectedType.Checked = true;
        ////     rdbAllTypes.Checked = false;
        // dropDownListSelectedType.Visible = true;
        //  type.Visible = true;
        ////     rdbAllPurposeCode.Checked = false;
        ////     rdbSelectedPurposeCode.Checked = false;
        //   txtPurposeCode.Visible = false;
        //   BtnPurposeList.Visible = false;
        //   lblPurposeCode.Visible = false;
        //   txtPurposeCode.Text = "";
        //    lblPurposeCode.Text = "";
        //    Purpose.Visible = false;
    }
    protected void rdbUserAccess_CheckedChanged(object sender, EventArgs e)
    {
        rdbUserAccess.Focus();      
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        var Branch = ddlBranch.SelectedValue;
        var Report = PageHeader.Text;
        int rptCode=0;
        var rptType = "";
        if (rdbAllTypes.Checked == true)
        {
            rptCode = 1;
            rptType = "ALL";
        }

        if (rdbSelectedType.Checked == true)
        {
            rptCode = 2;
            rptType = "User Creation";
        }

        if (rdbAllPurposeCode.Checked == true)
        {
            rptCode = 3;
            rptType = "User Modification";
        }

        if (rdbSelectedPurposeCode.Checked == true)
        {
            rptCode = 4;
            rptType = "User Deletion";
        }
        if (rdbUserAccess.Checked == true)
        {
            rptCode = 5;
            rptType = "User Access Control";
        }



        Response.Redirect("VIEW_TF_AdminAuditTrail.aspx?PageHeader=" + Report + "&frm=" + txtFromDate.Text + "&to=" + txtToDate.Text + "&rptType=" + rptType + "&rptCode=" + rptCode + "&branch=" + Branch);

    }
}