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
public partial class RRETURN_RET_AddEditVostroOpCloBalanceDataEntry : System.Web.UI.Page
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
                clearControls();
                btnSave.Attributes.Add("onclick", "return validateSave();");
                btnDelete.Attributes.Add("onclick", "return validatedelete();");
                txtFromDate.Text = Session["FrRelDt"].ToString().Trim();
                hdnUserRole.Value = Session["userRole"].ToString().Trim();
                txtToDate.Text = Session["ToRelDt"].ToString().Trim();
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                //lblAdcodeDesc.Text = Session["userADCode"].ToString();
                if (Session["userADCode"] != null)
                {
                    lblAdcodeDesc.Text = Server.HtmlEncode(Session["userADCode"].ToString());
                }
                //ddlBranch.Enabled = false;

                
                fillCurrency();
                addAttributes();
                //  fillCountryAC();
                btncalendar_FromDate.Focus();
                txtFromDate.Attributes.Add("onblur", "return ValidDates();");
                btnSave.Attributes.Add("onclick", "return validateSave();");
                ddlBranch.Focus();
                txtCL_C.Attributes.Add("onpaste", "return false;");
                txtCL_D.Attributes.Add("onpaste", "return false;");
                txtOP_C.Attributes.Add("onpaste", "return false;");
                txtOP_D.Attributes.Add("onpaste", "return false;");
            }
            BtnBankCode.Attributes.Add("onclick", "return OpenBankCodeList('1');");
            //  ddlBankCode.Attributes.Add("onchange", "return changeBankCodeDesc();");
            // ddlBranch.Attributes.Add("onchange", "return changeBranchDesc();");
            btnCountryACHolder.Attributes.Add("onclick", "return OpenCountryListAc('1');");
            txtFromDate.Attributes.Add("onblur", "return ValidDates();");
            txtToDate.Attributes.Add("onblur", "return ValidDates1();");
            txtCountryAC.Attributes.Add("onblur", "return fillforOB();");
            txtToDate.Attributes.Add("onblur", "return fillforOB();");
            txtBankCode.Attributes.Add("onblur", "return fillforOB();");
            txtCL_C.Attributes.Add("onkeydown", "return validate_Number(event);");
            txtCL_D.Attributes.Add("onkeydown", "return validate_Number(event);");
            txtOP_C.Attributes.Add("onkeydown", "return validate_Number(event);");
            txtOP_D.Attributes.Add("onkeydown", "return validate_Number(event);");
        }

    }
    protected void fillBankCode()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        SqlParameter p2 = new SqlParameter("@countryofac", SqlDbType.VarChar);
        p2.Value = txtCountryAC.Text.ToString();
        string _query = "TF_RET_HelpGetBankCodeList";
        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            txtBankCode.Text = dt.Rows[0]["BANKCODE"].ToString();
            lblBankCode.Text = dt.Rows[0]["BANKNAME"].ToString();
        }
        else
        {
            txtBankCode.Text = "";
            lblBankCode.Text = "<font style=color:red;>"+"Invalid Bank"+"</font>";
            txtBankCode.Focus();
        }
    }
    protected void btnBankCode_Click(object sender, EventArgs e)
    {
        if (hdnBankCode.Value != "")
        {
            txtBankCode.Text =
            hdnBankCode.Value;
            txtBankCode.Text = txtBankCode.Text;
            //txtBankCode_TextChanged(null,null);
            txtBankCode.Focus();
        }
    }
    //protected void fillCountryAC()
    //{
    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
    //    p1.Value = "";
    //    string _query = "TF_RET_HelpGetCountryACList";
    //    DataTable dt = objData.getData(_query, p1);
    //    dropDownListCountryACHolder.Items.Clear();
    //    ListItem li = new ListItem();
    //    li.Value = "0";
    //    if (dt.Rows.Count > 0)
    //    {
    //        li.Text = "";
    //        dropDownListCountryACHolder.DataSource = dt.DefaultView;
    //        dropDownListCountryACHolder.DataTextField = "CountryID";
    //        dropDownListCountryACHolder.DataValueField = "CountryName";
    //        dropDownListCountryACHolder.DataBind();
    //    }
    //    else
    //    {
    //        li.Text = "No record(s) found";
    //    }
    //    dropDownListCountryACHolder.Items.Insert(0, li);
    //}
    protected void btnCountryAc_Click(object sender, EventArgs e)
    {
        if (hdnCountryAc.Value != "")
        {
            txtCountryAC.Text =
            hdnCountryAc.Value;
            //txtCountryACHolder.Text = txtCountryAC.Text;
            GetCountryDesc();
            txtCountryAC.Focus();
        }
        fillBankCode();
    }
    protected void addAttributes()
    {
        //txtOP_C.Attributes.Add("oncut", "return false;");
        //txtOP_C.Attributes.Add("onpaste", "return false;");
        ////     txtOP_C.Attributes.Add("onfocus", "blur();");
        //txtOP_C.Attributes.Add("onkeypress", "return false;");
        //txtOP_D.Attributes.Add("oncut", "return false;");
        //txtOP_D.Attributes.Add("onpaste", "return false;");
        //txtOP_D.Attributes.Add("onkeypress", "return false;");
        //txtCL_C.Attributes.Add("oncut", "return false;");
        //txtCL_C.Attributes.Add("onpaste", "return false;");
        ////   txtCL_C.Attributes.Add("onfocus", "blur();");
        //txtCL_C.Attributes.Add("onkeypress", "return false;");
        //txtCL_D.Attributes.Add("oncut", "return false;");
        //txtCL_D.Attributes.Add("onpaste", "return false;");
        //// txtCL_D.Attributes.Add("onfocus", "blur();");
        //txtCL_D.Attributes.Add("onkeypress", "return false;");
        //txtOP_D.Attributes.Add("oncontextmenu", "return false;");
        //txtOP_C.Attributes.Add("oncontextmenu", "return false;");
        //txtCL_D.Attributes.Add("oncontextmenu", "return false;");
        //txtCL_C.Attributes.Add("oncontextmenu", "return false;");
        //txtOP_D.Attributes.Add("onkeydown", "return validate_date(event);");
        //txtOP_C.Attributes.Add("onkeydown", "return validate_date(event);");
        //txtCL_D.Attributes.Add("onkeydown", "return validate_date(event);");
        //txtCL_C.Attributes.Add("onkeydown", "return validate_date(event);");
    }
    protected void fillCurrency()
    {
        dropDownListCurrency.Items.Clear();
        ListItem lio = new ListItem();
        lio.Value = "0";
        lio.Text = "INR";
        dropDownListCurrency.Items.Add(lio);
        ListItem li1 = new ListItem();
        li1.Value = "1";
        li1.Text = "ACD";
        dropDownListCurrency.Items.Add(li1);
    }
    protected void fillDetails()
    {
        TF_DATA ObjData = new TF_DATA();
        string _query = "TF_RET_GetVostroOpeningBalanceDetails";
        SqlParameter p1 = new SqlParameter("@adcode", SqlDbType.VarChar);
        p1.Value = ddlBranch.Text.ToString().Trim();
        SqlParameter p2 = new SqlParameter("@currencyid", SqlDbType.VarChar);
        p2.Value = dropDownListCurrency.SelectedItem.ToString();
        SqlParameter p3 = new SqlParameter("@fromdate", SqlDbType.VarChar);
        p3.Value = txtFromDate.Text.ToString();
        SqlParameter p4 = new SqlParameter("@todate", SqlDbType.VarChar);
        p4.Value = txtToDate.Text.ToString();
        SqlParameter p5 = new SqlParameter("@COUNTRYCODE", SqlDbType.VarChar);
        p5.Value = txtCountryAC.Text.ToString();
        SqlParameter p6 = new SqlParameter("@VOSTRO_BANK_CODE", SqlDbType.VarChar);
        p6.Value = txtBankCode.Text.ToString();
        DataTable dt = ObjData.getData(_query, p1, p2, p3, p4, p5, p6);
        if (dt.Rows.Count > 0)
        {

            txtOP_D.Text = dt.Rows[0]["OP_D"].ToString().Trim();
            txtOP_C.Text = dt.Rows[0]["OP_C"].ToString().Trim();
            txtCL_D.Text = dt.Rows[0]["CL_D"].ToString().Trim();
            txtCL_C.Text = dt.Rows[0]["CL_C"].ToString().Trim();
        }
        else
        {
            txtOP_D.Text = "";
            txtOP_C.Text = "";
            txtCL_D.Text = "";
            txtCL_C.Text = "";
        }
        //txtOP_D.Focus();
    }
    protected void btnCurr_Click(object sender, EventArgs e)
    {
    }
    protected void btnfillgrid_Click(object sender, EventArgs e)
    {
        //    clearTextboxControls();
        // fillDetails();
        txtCountryAC.Focus();
    }
    protected void btnCurrencyfillgrid_Click(object sender, EventArgs e)
    {
        clearTextboxControls();
        //   fillDetails();
        dropDownListCurrency.Focus();
    }
    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@AdCode", SqlDbType.VarChar);
        p1.Value = Session["userADCode"].ToString();
        string _query = "TF_RET_GetBranchDetials";
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
        string from_Date = hdnNextFromdate.Value;
        string to_Date = hdnNextToDate.Value;
        string _ADCODE = ddlBranch.Text.ToString();
        string _FR_FORTNIGHT_DT = txtFromDate.Text.ToString();
        string _TO_FORTNIGHT_DT = txtToDate.Text.ToString();
        string _VASTRO_BANK_CODE = txtBankCode.Text.ToString();
        string _NV = "V";
        string _CURR = dropDownListCurrency.SelectedItem.ToString();
        string _COUNTRYCODE = txtCountryAC.Text.ToString();
        string _OP_D = txtOP_D.Text.Trim();
        string _OP_C = txtOP_C.Text.Trim();
        string _CL_D = txtCL_D.Text.Trim();
        string _CL_C = txtCL_C.Text.Trim();
        if (_OP_D == "")
            _OP_D = "0";
        if (_OP_C == "")
            _OP_C = "0";
        if (_CL_D == "")
            _CL_D = "0";
        if (_CL_C == "")
            _CL_C = "0";
        SqlParameter NextFromDate = new SqlParameter("@NextFromDate", SqlDbType.VarChar);
        SqlParameter NextToDate = new SqlParameter("@NextToDate", SqlDbType.VarChar);
        SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
        SqlParameter FR_FORTNIGHT_DT = new SqlParameter("@FR_FORTNIGHT_DT", SqlDbType.VarChar);
        SqlParameter TO_FORTNIGHT_DT = new SqlParameter("@TO_FORTNIGHT_DT", SqlDbType.VarChar);
        SqlParameter VASTRO_BANK_CODE = new SqlParameter("@VOSTRO_BANK_CODE", SqlDbType.VarChar);
        SqlParameter NV = new SqlParameter("@NV", SqlDbType.VarChar);
        SqlParameter CURR = new SqlParameter("@CURR", SqlDbType.VarChar);
        SqlParameter COUNTRYCODE = new SqlParameter("@COUNTRYCODE", SqlDbType.VarChar);
        SqlParameter OP_D = new SqlParameter("@OP_D", SqlDbType.VarChar);
        SqlParameter OP_C = new SqlParameter("@OP_C", SqlDbType.VarChar);
        SqlParameter CL_D = new SqlParameter("@CL_D", SqlDbType.VarChar);
        SqlParameter CL_C = new SqlParameter("@CL_C", SqlDbType.VarChar);
        NextFromDate.Value = from_Date;
        NextToDate.Value = to_Date;
        ADCODE.Value = _ADCODE;
        FR_FORTNIGHT_DT.Value = _FR_FORTNIGHT_DT;
        TO_FORTNIGHT_DT.Value = _TO_FORTNIGHT_DT;
        VASTRO_BANK_CODE.Value = _VASTRO_BANK_CODE;
        NV.Value = _NV;
        CURR.Value = _CURR;
        COUNTRYCODE.Value = _COUNTRYCODE;
        OP_D.Value = _OP_D;
        OP_C.Value = _OP_C;
        CL_D.Value = _CL_D;
        CL_C.Value = _CL_C;
        string _result = "";
        string _query = "TF_RET_UpdateVostroOpeningClosingBalance";
        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData(_query, NextFromDate, NextToDate, ADCODE, FR_FORTNIGHT_DT, TO_FORTNIGHT_DT, VASTRO_BANK_CODE, NV, CURR, COUNTRYCODE, OP_D,
        OP_C, CL_D, CL_C);
        string _script = "";
        if (_result == "added")
        {
            _script = "alert('Record Added.')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            clearControls();
            txtCountryACHolder.Text = "";
            txtCountryACHolder.Focus();
        }
        else
        {
            labelMessage.Text = _result;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearControls();
        Response.Redirect("RET_AddEditVostroOpCloBalanceDataEntry.aspx?PageHeader=Vostro Opening Closing balance Data Entry", true);
    }
    protected void dropDownListCurrency_SelectedIndexChangeds(object sender, EventArgs e)
    {
        //  fillDetails();
        //dropDownListCurrency.Focus();
    }
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ValidateDates", "return ValidDates();", true);
        txtToDate.Focus();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Ret_Main.aspx?PageHeader=RET Main", true);
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetADCode();
        //  fillDetails();
        ddlBranch.Focus();
    }
    protected void GetADCode()
    {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_RET_GetBranchADCode";
        SqlParameter p1 = new SqlParameter("@branchname", ddlBranch.SelectedItem.Text);
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblAdcodeDesc.Text = dt.Rows[0]["AuthorizedDealerCode"].ToString().Trim();
        }
        else
        {
            lblAdcodeDesc.Text = "";
        }
    }
    protected void clearTextboxControls()
    {
        txtOP_C.Text = "";
        txtOP_D.Text = "";
        txtCL_C.Text = "";
        txtCL_D.Text = "";
    }
    protected void clearControls()
    {
        lblBankCode.Text = "";
        ddlBranch.SelectedIndex = -1;
        dropDownListCurrency.SelectedIndex = -1;
        //txtFromDate.Text = "";
        //txtToDate.Text = "";
        txtCountryAC.Text = "";
        txtBankCode.Text = "";
        txtOP_C.Text = "";
        txtOP_D.Text = "";
        txtCL_C.Text = "";
        txtCL_D.Text = "";
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        fillDetails();
        txtCountryAC.Focus();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        TF_DATA ObjData = new TF_DATA();
        string _query = "TF_RET_DeleteVostroOpeningClosingBalance";
        SqlParameter p1 = new SqlParameter("@adcode", SqlDbType.VarChar);
        p1.Value = ddlBranch.Text.ToString().Trim();
        SqlParameter p2 = new SqlParameter("@currencyid", SqlDbType.VarChar);
        p2.Value = dropDownListCurrency.SelectedItem.ToString();
        SqlParameter p3 = new SqlParameter("@fromdate", SqlDbType.VarChar);
        p3.Value = txtFromDate.Text.ToString();
        SqlParameter p4 = new SqlParameter("@todate", SqlDbType.VarChar);
        p4.Value = txtToDate.Text.ToString();
        SqlParameter p5 = new SqlParameter("@COUNTRYCODE", SqlDbType.VarChar);
        p5.Value = txtCountryAC.Text.ToString();
        SqlParameter p6 = new SqlParameter("@VOSTRO_BANK_CODE", SqlDbType.VarChar);
        p6.Value = txtBankCode.Text.ToString();
        string _result;
        _result = ObjData.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6);
        string _script = "";
        if (_result == "deleted")
        {
            _script = "alert('Record Deleted.')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            clearControls();
        }
        else
        {
            labelMessage.Text = _result;
        }
    }
    //protected void dropDownListCountryACHolder_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillBankCode();
    //    txtCountryAC.Focus();
    //}
    //protected void ddlBankCode_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlBankCode.Text != "0")
    //        lblBankCode.Text = ddlBankCode.Text;
    //    else
    //        lblBankCode.Text = "";
    //    clearTextboxControls();
    //    fillDetails();
    //    txtOP_D.Focus();
    //}
    protected void dropDownListCurrency_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (dropDownListCurrency.SelectedIndex == 0)
        {
            txtCurrencyDescription.Text = "INDIAN RUPEES";
        }
        else if (dropDownListCurrency.SelectedIndex == 1)
        {
            txtCurrencyDescription.Text = "ASIAN CURRENCY DOLLAR";
        }
        lblBankCode.Text = "";
        ddlBranch.SelectedIndex = -1;
        //dropDownListCurrency.SelectedIndex = -1;
        //txtFromDate.Text = "";
        //txtToDate.Text = "";
        txtCountryAC.Text = "";
        txtBankCode.Text = "";
        txtOP_C.Text = "";
        txtOP_D.Text = "";
        txtCL_C.Text = "";
        txtCL_D.Text = "";
        dropDownListCurrency.Focus();
    }
    protected void txtCountryAC_TextChanged(object sender, EventArgs e)
    {
        GetCountryDesc();
    }
    protected void GetCountryDesc()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = txtCountryAC.Text;
        string _query = "TF_RET_HelpGetCountryACListByID";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            txtCountryACHolder.Text = dt.Rows[0]["CountryName"].ToString();
        }
        else
        {
            txtCountryAC.Text = "";
            txtCountryACHolder.Text = "<font style=color:red;>"+"Invalid Country"+"</font>";
            txtCountryACHolder.Focus();
        }
    }
    protected void txtBankCode_TextChanged(object sender, EventArgs e)
    {
        if (txtCountryAC.Text != "")
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@CountryCode", SqlDbType.VarChar);
            p1.Value = txtCountryAC.Text;
            SqlParameter p2 = new SqlParameter("@BankCode", SqlDbType.VarChar);
            p2.Value = txtBankCode.Text;
            string _query = "TF_RET_GetVostroBankName";
            DataTable dt = objData.getData(_query, p1, p2);
            if (dt.Rows.Count > 0)
            {
                lblBankCode.Text = dt.Rows[0]["BankName"].ToString();
                fillDetails();
                txtOP_D.Focus();
            }
            else
            {
                
                txtBankCode.Text = "";
                txtBankCode.Focus();
                clearTextboxControls();
                lblBankCode.Text = "<font style=color:red;>"+"Invalid Bank Code"+"</font>";
            }
        }
        else
        {
            lblBankCode.Text = "";
            txtBankCode.Text = "";
            txtCountryAC.Focus();
            string _script = "alert('Select AC Country First.')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        
        
        
    }
    protected void fillBank()
    {
        
    }
    protected void btnfillforOB_Click(object sender, EventArgs e)
    {
        fillDetails();
    }
}