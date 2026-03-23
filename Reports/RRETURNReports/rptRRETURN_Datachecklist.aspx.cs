using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
public partial class Reports_RRETURNReports_rptRRETURN_Datachecklist : System.Web.UI.Page
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
        {
            txtPurposeCode.Attributes.Add("onkeydown", "return PurposeId(event)");
            BtnPurposeList.Attributes.Add("onclick", "return PurposeCodehelp()"); 
            if (!IsPostBack)
            {
                clearControls();
                rdbAllPurposeCode.Checked = true;
                txtFromDate.Focus();               
                txtFromDate.Text = Session["FrRelDt"].ToString();
                txtToDate.Text = Session["ToRelDt"].ToString();               
                btnSave.Attributes.Add("onclick", "return validateSave();");
                PageHeader.Text = Request.QueryString["PageHeader"].ToString();
                rdbTypewise.Checked = true;
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();                
                //ddlBranch.Enabled = false;
                fillCombo();
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
        rdbAllPurposeCode.Checked = true;
        rdbSelectedPurposeCode.Checked = false;
        txtPurposeCode.Visible = false;
        BtnPurposeList.Visible = false;
        lblPurposeCode.Visible = false;        
        txtPurposeCode.Text = "";
        lblPurposeCode.Text = "";
        dropDownListSelectedType.Visible = false;
        Purpose.Visible = false;
        type.Visible = false;
    }
    protected void rdbSelectedPurposeCode_CheckedChanged(object sender, EventArgs e)
    {
        rdbSelectedPurposeCode.Focus();
        rdbSelectedPurposeCode.Checked = true;
        rdbAllPurposeCode.Checked = false;
        txtPurposeCode.Visible = true;
        BtnPurposeList.Visible = true;
        lblPurposeCode.Visible = true;        
        dropDownListSelectedType.Visible = false;
        Purpose.Visible = true;
        type.Visible = false;
        txtPurposeCode.Focus();
    }
    protected void txtPurposeCode_TextChanged(object sender, EventArgs e)
    {
        fillPurposeIdDescription();
        //txtPurposeCode.Focus();
    }
    public void fillPurposeIdDescription()
    {
        lblPurposeCode.Text = "";
        string purposeid = txtPurposeCode.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@purposeid", SqlDbType.VarChar);
        p1.Value = purposeid;
        string _query = "TF_RET_GetPurposeMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {            
            lblPurposeCode.Text = dt.Rows[0]["description"].ToString().Trim();
            btnSave.Focus();
        }
        else
        {
            txtPurposeCode.Text = "";            
            lblPurposeCode.Text = "<font style=color:red;>"+"Invalid Purpose Code"+"</font>";
            txtPurposeCode.Focus();
        }
    }
    protected void rdbTypewise_CheckedChanged(object sender, EventArgs e)
    {
        {
            if (rdbTypewise.Checked == true)
            {
                rdbAllTypes.Visible = true;
                rdbSelectedType.Visible = true;
                rdbAllTypes.Checked = true;
                rdbSelectedType.Checked = false;
                type.Visible = false;
                dropDownListSelectedType.Visible = false;
                rdbAllPurposeCode.Visible = false;
                rdbSelectedPurposeCode.Visible = false;
                Purpose.Visible = false;
                txtPurposeCode.Visible = false;
                lblPurposeCode.Visible = false;
                BtnPurposeList.Visible = false;
                txtPurposeCode.Text = "";
            }
            else
            {
                rdbAllTypes.Visible = false;
                rdbSelectedType.Visible =false;
                dropDownListSelectedType.Visible = false;
            }
            rdbTypewise.Focus();
        }       
    }
    protected void rdbPurposeCodewise_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbPurposeCodewise.Checked == true)
        {
            rdbAllPurposeCode.Visible = true;
            rdbSelectedPurposeCode.Visible = true;
            rdbAllPurposeCode.Checked = true;
            rdbSelectedPurposeCode.Checked = false;
            rdbAllTypes.Visible = false;
            rdbSelectedType.Visible = false;
            type.Visible = false;
            dropDownListSelectedType.Visible = false;
        }
        else
        {
            rdbAllPurposeCode.Visible = false;
            rdbSelectedPurposeCode.Visible = false;            
        }
        rdbPurposeCodewise.Focus();
    }   
    protected void dropDownListSelectedType_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropDownListSelectedType.Focus();
    }
    protected void fillCombo()
    {
        dropDownListSelectedType.Items.Add("EXP");
        dropDownListSelectedType.Items.Add("IMP");
        dropDownListSelectedType.Items.Add("INW");
        dropDownListSelectedType.Items.Add("OTW");
        dropDownListSelectedType.Items.Add("OTH");
    }
    protected void rdbAllTypes_CheckedChanged(object sender, EventArgs e)
    {
        rdbAllTypes.Focus();
        rdbAllTypes.Checked = true;
        rdbSelectedType.Checked = false;
        dropDownListSelectedType.Visible = false;  
        rdbAllPurposeCode.Checked = false;
        rdbSelectedPurposeCode.Checked = false;
        txtPurposeCode.Visible = false;
        BtnPurposeList.Visible = false;
        lblPurposeCode.Visible = false;
        type.Visible = false;
        txtPurposeCode.Text = "";
        lblPurposeCode.Text = "";
        Purpose.Visible = false;
    }
    protected void rdbSelectedType_CheckedChanged(object sender, EventArgs e)
    {        
        rdbSelectedType.Focus();
        rdbSelectedType.Checked = true;
        rdbAllTypes.Checked = false;
        dropDownListSelectedType.Visible = true;
        type.Visible = true;
        rdbAllPurposeCode.Checked = false;
        rdbSelectedPurposeCode.Checked = false;
        txtPurposeCode.Visible = false;
        BtnPurposeList.Visible = false;
        lblPurposeCode.Visible = false;
        txtPurposeCode.Text = "";
        lblPurposeCode.Text = "";
        Purpose.Visible = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //"View_rptRRETURN_Datachecklist.aspx?PageHeader=RReturn Data Check List&frm=' + from + '&to=' + to + '&rptType=' + rptType + '&rptCode=' + rptCode + '&branch=' + Branch + '&Report=' + Report, ''";
        string rptType = "";
        string Report = PageHeader.Text;
        string Branch = ddlBranch.SelectedValue;
        int rptCode = 0;
        if (Report == "R Return Data CheckList")
        {
            if (rdbTypewise.Checked)
            {

                if (rdbAllTypes.Checked == true)
                {
                    rptCode = 1;
                    rptType = "ALL";
                }
                else if (rdbSelectedType.Checked == true)
                {
                    //string Type = dropDownListSelectedType.SelectedValue;
                    //if (Type == '0') {
                    //    alert('Select Type');
                    //    Type.focus();
                    //    return false;
                    //}
                    rptCode = 2;
                    rptType = dropDownListSelectedType.SelectedValue;
                }
            }
            else if (rdbPurposeCodewise.Checked == true)
            {

                if (rdbAllPurposeCode.Checked == true)
                {
                    rptCode = 3;
                    rptType = "ALL";
                }
                else if (rdbSelectedPurposeCode.Checked == true)
                {
                    //string txt = txtPurposeCode.Text;
                    //if (txt == "") {
                    //    alert('Enter Purpose Code');
                    //    txt.focus();
                    //    return false;
                    //}
                    rptCode = 4;
                    rptType = txtPurposeCode.Text;
                }
            }
        }

        Response.Redirect("View_rptRRETURN_Datachecklist.aspx?PageHeader=" + PageHeader.Text + "&frm=" + txtFromDate.Text + "&to=" + txtToDate.Text + "&rptType=" + rptType + "&rptCode=" + rptCode + "&branch=" + Branch + "&Report=" + Report);
    }
}