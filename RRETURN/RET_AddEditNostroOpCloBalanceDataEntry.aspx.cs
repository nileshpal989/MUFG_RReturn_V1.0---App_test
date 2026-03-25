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
public partial class RRETURN_RET_AddEditNostroOpCloBalanceDataEntry : System.Web.UI.Page
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
                TabContainerMain.ActiveTab = tpOpeningBalance;
                hdnUserRole.Value = Session["userRole"].ToString().Trim();
                txtFromDate.Text = Session["FrRelDt"].ToString().Trim();
                txtToDate.Text = Session["ToRelDt"].ToString().Trim();
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();

                //lblAdcodeDesc.Text = Session["userADCode"].ToString();
                if (Session["userADCode"] != null)
                {
                    lblAdcodeDesc.Text = Server.HtmlEncode(Session["userADCode"].ToString());
                }
                // ddlBranch.Enabled = false;

                fillBank();
                // fillCurrency();
                addAttributes();
                btncalendar_FromDate.Focus();
                txtFromDate.Attributes.Add("onblur", "return ValidDates();");
                btnSave.Attributes.Add("onclick", "return validateSave();");
                txtCurrency.Focus();
               
            }
            TabContainerMain.ActiveTab = tpOpeningBalance;
            //ddlBranch.Focus();
            txtFromDate.Attributes.Add("onblur", "return ValidDates();");
            txtToDate.Attributes.Add("onblur", "return ValidDates1();");
            btncurrList.Attributes.Add("onclick", "return OpenCurrencyList('1');");
            txtCurrency.Attributes.Add("onchange", "return changeCurrDesc();");
        }
        // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "changeBranchDesc", "changeBranchDesc();", true);
    }
    protected void addAttributes()
    {
        txtOpeningBalanceAmount.Attributes.Add("oncut", "return false;");
        txtOpeningBalanceAmount.Attributes.Add("onpaste", "return false;");
        txtOpeningBalanceAmount.Attributes.Add("onfocus", "blur();");
        txtOpeningBalanceAmount.Attributes.Add("onkeypress", "return false;");
        txtOBDisabled.Attributes.Add("oncut", "return false;");
        txtOBDisabled.Attributes.Add("onpaste", "return false;");
        txtOBDisabled.Attributes.Add("onfocus", "blur();");
        txtOBDisabled.Attributes.Add("onkeypress", "return false;");
        txtClosingBalanceAmount.Attributes.Add("oncut", "return false;");
        txtClosingBalanceAmount.Attributes.Add("onpaste", "return false;");
        txtClosingBalanceAmount.Attributes.Add("onfocus", "blur();");
        txtClosingBalanceAmount.Attributes.Add("onkeypress", "return false;");
        txtCBDisabled.Attributes.Add("oncut", "return false;");
        txtCBDisabled.Attributes.Add("onpaste", "return false;");
        txtCBDisabled.Attributes.Add("onfocus", "blur();");
        txtCBDisabled.Attributes.Add("onkeypress", "return false;");
        OBCashBalanceDR.Attributes.Add("onblur", "calculateOpeningBalance();");
        OBSuspAccountDR.Attributes.Add("onblur", "calculateOpeningBalance();");
        OBDepositOtherDR.Attributes.Add("onblur", "calculateOpeningBalance();");
        OBDepositRBIDR.Attributes.Add("onblur", "calculateOpeningBalance();");
        OBFixedDepositDR.Attributes.Add("onblur", "calculateOpeningBalance();");
        OBTreasuryBillDR.Attributes.Add("onblur", "calculateOpeningBalance();");
        OBSecuritiesDR.Attributes.Add("onblur", "calculateOpeningBalance();");
        OBFCurrencyDR.Attributes.Add("onblur", "calculateOpeningBalance();");
        OBCashBalanceCR.Attributes.Add("onblur", "calculateOpeningBalance();");
        OBSuspAccountCR.Attributes.Add("onblur", "calculateOpeningBalance();");
        OBDepositOtherCR.Attributes.Add("onblur", "calculateOpeningBalance();");
        OBDepositRBICR.Attributes.Add("onblur", "calculateOpeningBalance();");
        OBFixedDepositCR.Attributes.Add("onblur", "calculateOpeningBalance();");
        OBTreasuryBillCR.Attributes.Add("onblur", "calculateOpeningBalance();");
        OBSecuritiesCR.Attributes.Add("onblur", "calculateOpeningBalance();");
        OBFCurrencyCR.Attributes.Add("onblur", "calculateOpeningBalance();");
        CBCashBalanceDR.Attributes.Add("onblur", "calculateClosingBalance();");
        CBSuspAccountDR.Attributes.Add("onblur", "calculateClosingBalance();");
        CBDepositOtherDR.Attributes.Add("onblur", "calculateClosingBalance();");
        CBDepositRBIDR.Attributes.Add("onblur", "calculateClosingBalance();");
        CBFixedDepositDR.Attributes.Add("onblur", "calculateClosingBalance();");
        CBTreasuryBillsDR.Attributes.Add("onblur", "calculateClosingBalance();");
        CBSecuritiesDR.Attributes.Add("onblur", "calculateClosingBalance();");
        CBForeignCurrencyDR.Attributes.Add("onblur", "calculateClosingBalance();");
        CBCashBalanceCR.Attributes.Add("onblur", "calculateClosingBalance();");
        CBSuspAccountCR.Attributes.Add("onblur", "calculateClosingBalance();");
        CBDepositOtherCR.Attributes.Add("onblur", "calculateClosingBalance();");
        CBDepositRBICR.Attributes.Add("onblur", "calculateClosingBalance();");
        CBFixedDepositCR.Attributes.Add("onblur", "calculateClosingBalance();");
        CBTreasuryBillsCR.Attributes.Add("onblur", "calculateClosingBalance();");
        CBSecuritiesCR.Attributes.Add("onblur", "calculateClosingBalance();");
        CBForeignCurrencyCR.Attributes.Add("onblur", "calculateClosingBalance();");
        //txtEEFCAccounts.Attributes.Add("onblur", "AddCoomaToTextboxes(this);");
        //txtEFCAccounts.Attributes.Add("onblur", "AddCoomaToTextboxes1(this);");
        //txtRFCAccounts.Attributes.Add("onblur", "AddCoomaToTextboxes2(this);");
        //txtEscrowFCAccounts.Attributes.Add("onblur", "AddCoomaToTextboxes3(this);");
        //txtFCNRBAccounts.Attributes.Add("onblur", "AddCoomaToTextboxes4(this);");
        //txtOtherFCAccounts.Attributes.Add("onblur", "AddCoomaToTextboxes5(this);");
        OBCashBalanceDR.Attributes.Add("oncut", "return false;");
        OBSuspAccountDR.Attributes.Add("oncut", "return false;");
        OBDepositOtherDR.Attributes.Add("oncut", "return false;");
        OBDepositRBIDR.Attributes.Add("oncut", "return false;");
        OBFixedDepositDR.Attributes.Add("oncut", "return false;");
        OBTreasuryBillDR.Attributes.Add("oncut", "return false;");
        OBSecuritiesDR.Attributes.Add("oncut", "return false;");
        OBFCurrencyDR.Attributes.Add("oncut", "return false;");
        OBCashBalanceCR.Attributes.Add("oncut", "return false;");
        OBSuspAccountCR.Attributes.Add("oncut", "return false;");
        OBDepositOtherCR.Attributes.Add("oncut", "return false;");
        OBDepositRBICR.Attributes.Add("oncut", "return false;");
        OBFixedDepositCR.Attributes.Add("oncut", "return false;");
        OBTreasuryBillCR.Attributes.Add("oncut", "return false;");
        OBSecuritiesCR.Attributes.Add("oncut", "return false;");
        OBFCurrencyCR.Attributes.Add("oncut", "return false;");
        txtEEFCAccounts.Attributes.Add("oncut", "return false;");
        txtEFCAccounts.Attributes.Add("oncut", "return false;");
        txtRFCAccounts.Attributes.Add("oncut", "return false;");
        txtEscrowFCAccounts.Attributes.Add("oncut", "return false;");
        txtFCNRBAccounts.Attributes.Add("oncut", "return false;");
        txtOtherFCAccounts.Attributes.Add("oncut", "return false;");
        CBCashBalanceDR.Attributes.Add("oncut", "return false;");
        CBSuspAccountDR.Attributes.Add("oncut", "return false;");
        CBDepositOtherDR.Attributes.Add("oncut", "return false;");
        CBDepositRBIDR.Attributes.Add("oncut", "return false;");
        CBFixedDepositDR.Attributes.Add("oncut", "return false;");
        CBTreasuryBillsDR.Attributes.Add("oncut", "return false;");
        CBSecuritiesDR.Attributes.Add("oncut", "return false;");
        CBForeignCurrencyDR.Attributes.Add("oncut", "return false;");
        CBCashBalanceCR.Attributes.Add("oncut", "return false;");
        CBSuspAccountCR.Attributes.Add("oncut", "return false;");
        CBDepositOtherCR.Attributes.Add("oncut", "return false;");
        CBDepositRBICR.Attributes.Add("oncut", "return false;");
        CBFixedDepositCR.Attributes.Add("oncut", "return false;");
        CBTreasuryBillsCR.Attributes.Add("oncut", "return false;");
        CBSecuritiesCR.Attributes.Add("oncut", "return false;");
        CBForeignCurrencyCR.Attributes.Add("oncut", "return false;");
        OBCashBalanceDR.Attributes.Add("onpaste", "return false;");
        OBSuspAccountDR.Attributes.Add("onpaste", "return false;");
        OBDepositOtherDR.Attributes.Add("onpaste", "return false;");
        OBDepositRBIDR.Attributes.Add("onpaste", "return false;");
        OBFixedDepositDR.Attributes.Add("onpaste", "return false;");
        OBTreasuryBillDR.Attributes.Add("onpaste", "return false;");
        OBSecuritiesDR.Attributes.Add("onpaste", "return false;");
        OBFCurrencyDR.Attributes.Add("onpaste", "return false;");
        OBCashBalanceCR.Attributes.Add("onpaste", "return false;");
        OBSuspAccountCR.Attributes.Add("onpaste", "return false;");
        OBDepositOtherCR.Attributes.Add("onpaste", "return false;");
        OBDepositRBICR.Attributes.Add("onpaste", "return false;");
        OBFixedDepositCR.Attributes.Add("onpaste", "return false;");
        OBTreasuryBillCR.Attributes.Add("onpaste", "return false;");
        OBSecuritiesCR.Attributes.Add("onpaste", "return false;");
        OBFCurrencyCR.Attributes.Add("onpaste", "return false;");
        txtEEFCAccounts.Attributes.Add("onpaste", "return false;");
        txtEFCAccounts.Attributes.Add("onpaste", "return false;");
        txtRFCAccounts.Attributes.Add("onpaste", "return false;");
        txtEscrowFCAccounts.Attributes.Add("onpaste", "return false;");
        txtFCNRBAccounts.Attributes.Add("onpaste", "return false;");
        txtOtherFCAccounts.Attributes.Add("onpaste", "return false;");
        CBCashBalanceDR.Attributes.Add("onpaste", "return false;");
        CBSuspAccountDR.Attributes.Add("onpaste", "return false;");
        CBDepositOtherDR.Attributes.Add("onpaste", "return false;");
        CBDepositRBIDR.Attributes.Add("onpaste", "return false;");
        CBFixedDepositDR.Attributes.Add("onpaste", "return false;");
        CBTreasuryBillsDR.Attributes.Add("onpaste", "return false;");
        CBSecuritiesDR.Attributes.Add("onpaste", "return false;");
        CBForeignCurrencyDR.Attributes.Add("onpaste", "return false;");
        CBCashBalanceCR.Attributes.Add("onpaste", "return false;");
        CBSuspAccountCR.Attributes.Add("onpaste", "return false;");
        CBDepositOtherCR.Attributes.Add("onpaste", "return false;");
        CBDepositRBICR.Attributes.Add("onpaste", "return false;");
        CBFixedDepositCR.Attributes.Add("onpaste", "return false;");
        CBTreasuryBillsCR.Attributes.Add("onpaste", "return false;");
        CBSecuritiesCR.Attributes.Add("onpaste", "return false;");
        CBForeignCurrencyCR.Attributes.Add("onpaste", "return false;");
        OBCashBalanceDR.Attributes.Add("oncontextmenu", "return false;");
        OBSuspAccountDR.Attributes.Add("oncontextmenu", "return false;");
        OBDepositOtherDR.Attributes.Add("oncontextmenu", "return false;");
        OBDepositRBIDR.Attributes.Add("oncontextmenu", "return false;");
        OBFixedDepositDR.Attributes.Add("oncontextmenu", "return false;");
        OBTreasuryBillDR.Attributes.Add("oncontextmenu", "return false;");
        OBSecuritiesDR.Attributes.Add("oncontextmenu", "return false;");
        OBFCurrencyDR.Attributes.Add("oncontextmenu", "return false;");
        OBCashBalanceCR.Attributes.Add("oncontextmenu", "return false;");
        OBSuspAccountCR.Attributes.Add("oncontextmenu", "return false;");
        OBDepositOtherCR.Attributes.Add("oncontextmenu", "return false;");
        OBDepositRBICR.Attributes.Add("oncontextmenu", "return false;");
        OBFixedDepositCR.Attributes.Add("oncontextmenu", "return false;");
        OBTreasuryBillCR.Attributes.Add("oncontextmenu", "return false;");
        OBSecuritiesCR.Attributes.Add("oncontextmenu", "return false;");
        OBFCurrencyCR.Attributes.Add("oncontextmenu", "return false;");
        txtEEFCAccounts.Attributes.Add("oncontextmenu", "return false;");
        txtEFCAccounts.Attributes.Add("oncontextmenu", "return false;");
        txtRFCAccounts.Attributes.Add("oncontextmenu", "return false;");
        txtEscrowFCAccounts.Attributes.Add("oncontextmenu", "return false;");
        txtFCNRBAccounts.Attributes.Add("oncontextmenu", "return false;");
        txtOtherFCAccounts.Attributes.Add("oncontextmenu", "return false;");
        CBCashBalanceDR.Attributes.Add("oncontextmenu", "return false;");
        CBSuspAccountDR.Attributes.Add("oncontextmenu", "return false;");
        CBDepositOtherDR.Attributes.Add("oncontextmenu", "return false;");
        CBDepositRBIDR.Attributes.Add("oncontextmenu", "return false;");
        CBFixedDepositDR.Attributes.Add("oncontextmenu", "return false;");
        CBTreasuryBillsDR.Attributes.Add("oncontextmenu", "return false;");
        CBSecuritiesDR.Attributes.Add("oncontextmenu", "return false;");
        CBForeignCurrencyDR.Attributes.Add("oncontextmenu", "return false;");
        CBCashBalanceCR.Attributes.Add("oncontextmenu", "return false;");
        CBSuspAccountCR.Attributes.Add("oncontextmenu", "return false;");
        CBDepositOtherCR.Attributes.Add("oncontextmenu", "return false;");
        CBDepositRBICR.Attributes.Add("oncontextmenu", "return false;");
        CBFixedDepositCR.Attributes.Add("oncontextmenu", "return false;");
        CBTreasuryBillsCR.Attributes.Add("oncontextmenu", "return false;");
        CBSecuritiesCR.Attributes.Add("oncontextmenu", "return false;");
        CBForeignCurrencyCR.Attributes.Add("oncontextmenu", "return false;");
        //
        OBCashBalanceDR.Attributes.Add("onkeydown", "return validate_date(event);");
        OBSuspAccountDR.Attributes.Add("onkeydown", "return validate_date(event);");
        OBDepositOtherDR.Attributes.Add("onkeydown", "return validate_date(event);");
        OBDepositRBIDR.Attributes.Add("onkeydown", "return validate_date(event);");
        OBFixedDepositDR.Attributes.Add("onkeydown", "return validate_date(event);");
        OBTreasuryBillDR.Attributes.Add("onkeydown", "return validate_date(event);");
        OBSecuritiesDR.Attributes.Add("onkeydown", "return validate_date(event);");
        OBFCurrencyDR.Attributes.Add("onkeydown", "return validate_date(event);");
        OBCashBalanceCR.Attributes.Add("onkeydown", "return validate_date(event);");
        OBSuspAccountCR.Attributes.Add("onkeydown", "return validate_date(event);");
        OBDepositOtherCR.Attributes.Add("onkeydown", "return validate_date(event);");
        OBDepositRBICR.Attributes.Add("onkeydown", "return validate_date(event);");
        OBFixedDepositCR.Attributes.Add("onkeydown", "return validate_date(event);");
        OBTreasuryBillCR.Attributes.Add("onkeydown", "return validate_date(event);");
        OBSecuritiesCR.Attributes.Add("onkeydown", "return validate_date(event);");
        OBFCurrencyCR.Attributes.Add("onkeydown", "return validate_date(event);");
        txtEEFCAccounts.Attributes.Add("onkeydown", "return validate_date(event);");
        txtEFCAccounts.Attributes.Add("onkeydown", "return validate_date(event);");
        txtRFCAccounts.Attributes.Add("onkeydown", "return validate_date(event);");
        txtEscrowFCAccounts.Attributes.Add("onkeydown", "return validate_date(event);");
        txtFCNRBAccounts.Attributes.Add("onkeydown", "return validate_date(event);");
        txtOtherFCAccounts.Attributes.Add("onkeydown", "return validate_date(event);");
        CBCashBalanceDR.Attributes.Add("onkeydown", "return validate_date(event);");
        CBSuspAccountDR.Attributes.Add("onkeydown", "return validate_date(event);");
        CBDepositOtherDR.Attributes.Add("onkeydown", "return validate_date(event);");
        CBDepositRBIDR.Attributes.Add("onkeydown", "return validate_date(event);");
        CBFixedDepositDR.Attributes.Add("onkeydown", "return validate_date(event);");
        CBTreasuryBillsDR.Attributes.Add("onkeydown", "return validate_date(event);");
        CBSecuritiesDR.Attributes.Add("onkeydown", "return validate_date(event);");
        CBForeignCurrencyDR.Attributes.Add("onkeydown", "return validate_date(event);");
        CBCashBalanceCR.Attributes.Add("onkeydown", "return validate_date(event);");
        CBSuspAccountCR.Attributes.Add("onkeydown", "return validate_date(event);");
        CBDepositOtherCR.Attributes.Add("onkeydown", "return validate_date(event);");
        CBDepositRBICR.Attributes.Add("onkeydown", "return validate_date(event);");
        CBFixedDepositCR.Attributes.Add("onkeydown", "return validate_date(event);");
        CBTreasuryBillsCR.Attributes.Add("onkeydown", "return validate_date(event);");
        CBSecuritiesCR.Attributes.Add("onkeydown", "return validate_date(event);");
        CBForeignCurrencyCR.Attributes.Add("onkeydown", "return validate_date(event);");
        //
        OBCashBalanceDR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        OBSuspAccountDR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        OBDepositOtherDR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        OBDepositRBIDR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        OBFixedDepositDR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        OBTreasuryBillDR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        OBSecuritiesDR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        OBFCurrencyDR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        OBCashBalanceCR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        OBSuspAccountCR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        OBDepositOtherCR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        OBDepositRBICR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        OBFixedDepositCR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        OBTreasuryBillCR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        OBSecuritiesCR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        OBFCurrencyCR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        txtEEFCAccounts.Attributes.Add("onkeypress", "return checkNumeric(event);");
        txtEFCAccounts.Attributes.Add("onkeypress", "return checkNumeric(event);");
        txtRFCAccounts.Attributes.Add("onkeypress", "return checkNumeric(event);");
        txtEscrowFCAccounts.Attributes.Add("onkeypress", "return checkNumeric(event);");
        txtFCNRBAccounts.Attributes.Add("onkeypress", "return checkNumeric(event);");
        txtOtherFCAccounts.Attributes.Add("onkeypress", "return checkNumeric(event);");

        CBCashBalanceDR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        CBSuspAccountDR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        CBDepositOtherDR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        CBDepositRBIDR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        CBFixedDepositDR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        CBTreasuryBillsDR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        CBSecuritiesDR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        CBForeignCurrencyDR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        CBCashBalanceCR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        CBSuspAccountCR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        CBDepositOtherCR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        CBDepositRBICR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        CBFixedDepositCR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        CBTreasuryBillsCR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        CBSecuritiesCR.Attributes.Add("onkeypress", "return checkNumeric(event);");
        CBForeignCurrencyCR.Attributes.Add("onkeypress", "return checkNumeric(event);");
    }
    //protected void fillCurrency()
    //{
    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
    //    p1.Value = "";
    //    string _query = "TF_RET_GetCurrencyList_Nostro";
    //    DataTable dt = objData.getData(_query, p1);
    //    txtCurrency.Text.Clear();
    //    ListItem li = new ListItem();
    //    li.Value = "0";
    //    if (dt.Rows.Count > 0)
    //    {
    //        li.Text = "Select";
    //        dropDownListCurrency.DataSource = dt.DefaultView;
    //        dropDownListCurrency.DataTextField = "C_Code";
    //        dropDownListCurrency.DataValueField = "C_DESCRIPTION";
    //        dropDownListCurrency.DataBind();
    //    }
    //    else
    //        li.Text = "No record(s) found";
    //    dropDownListCurrency.Items.Insert(0, li);
    //}
    protected void fillDetails()
    {
        TF_DATA ObjData = new TF_DATA();
        string _query = "TF_RET_GetNostroOpeningBalanceDetails";
        SqlParameter p1 = new SqlParameter("@adcode", SqlDbType.VarChar);
        p1.Value = ddlBranch.Text.ToString().Trim();
        SqlParameter p2 = new SqlParameter("@currencyid", SqlDbType.VarChar);
        p2.Value = txtCurrency.Text.ToString();
        SqlParameter p3 = new SqlParameter("@fromdate", SqlDbType.VarChar);
        p3.Value = txtFromDate.Text.ToString();
        SqlParameter p4 = new SqlParameter("@todate", SqlDbType.VarChar);
        p4.Value = txtToDate.Text.ToString();
        DataTable dt = ObjData.getData(_query, p1, p2, p3, p4);
        if (dt.Rows.Count > 0)
        {
            OBCashBalanceDR.Text = dt.Rows[0]["OP_CASH_D"].ToString().Trim();
            OBCashBalanceCR.Text = dt.Rows[0]["OP_CASH_C"].ToString().Trim();
            OBSuspAccountDR.Text = dt.Rows[0]["OP_SUSP_D"].ToString().Trim();
            OBSuspAccountCR.Text = dt.Rows[0]["OP_SUSP_C"].ToString().Trim();
            OBDepositOtherDR.Text = dt.Rows[0]["OP_DEP_OTH_D"].ToString().Trim();
            OBDepositOtherCR.Text = dt.Rows[0]["OP_DEP_OTH_C"].ToString().Trim();
            OBDepositRBIDR.Text = dt.Rows[0]["OP_DEP_RBI_D"].ToString().Trim();
            OBDepositRBICR.Text = dt.Rows[0]["OP_DEP_RBI_C"].ToString().Trim();
            OBFixedDepositDR.Text = dt.Rows[0]["OP_FD_D"].ToString().Trim();
            OBFixedDepositCR.Text = dt.Rows[0]["OP_FD_C"].ToString().Trim();
            OBTreasuryBillDR.Text = dt.Rows[0]["OP_TB_D"].ToString().Trim();
            OBTreasuryBillCR.Text = dt.Rows[0]["OP_TB_C"].ToString().Trim();
            OBSecuritiesDR.Text = dt.Rows[0]["OP_SS_D"].ToString().Trim();
            OBSecuritiesCR.Text = dt.Rows[0]["OP_SS_C"].ToString().Trim();
            OBFCurrencyDR.Text = dt.Rows[0]["OP_FCLO_D"].ToString().Trim();
            OBFCurrencyCR.Text = dt.Rows[0]["OP_FCLO_C"].ToString().Trim();
            txtOpeningBalanceAmount.Text = dt.Rows[0]["OP_TOT_D"].ToString().Trim();
            txtOBDisabled.Text = dt.Rows[0]["OP_TOT_C"].ToString().Trim();
            CBCashBalanceDR.Text = dt.Rows[0]["CL_CASH_D"].ToString().Trim();
            CBCashBalanceCR.Text = dt.Rows[0]["CL_CASH_C"].ToString().Trim();
            CBSuspAccountDR.Text = dt.Rows[0]["CL_SUSP_D"].ToString().Trim();
            CBSuspAccountCR.Text = dt.Rows[0]["CL_SUSP_C"].ToString().Trim();
            CBDepositOtherDR.Text = dt.Rows[0]["CL_DEP_OTH_D"].ToString().Trim();
            CBDepositOtherCR.Text = dt.Rows[0]["CL_DEP_OTH_C"].ToString().Trim();
            CBDepositRBIDR.Text = dt.Rows[0]["CL_DEP_RBI_D"].ToString().Trim();
            CBDepositRBICR.Text = dt.Rows[0]["CL_DEP_RBI_C"].ToString().Trim();
            CBFixedDepositDR.Text = dt.Rows[0]["CL_FD_D"].ToString().Trim();
            CBFixedDepositCR.Text = dt.Rows[0]["CL_FD_C"].ToString().Trim();
            CBTreasuryBillsDR.Text = dt.Rows[0]["CL_TB_D"].ToString().Trim();
            CBTreasuryBillsCR.Text = dt.Rows[0]["CL_TB_C"].ToString().Trim();
            CBSecuritiesDR.Text = dt.Rows[0]["CL_SS_D"].ToString().Trim();
            CBSecuritiesCR.Text = dt.Rows[0]["CL_SS_C"].ToString().Trim();
            CBForeignCurrencyDR.Text = dt.Rows[0]["CL_FCLO_D"].ToString().Trim();
            CBForeignCurrencyCR.Text = dt.Rows[0]["CL_FCLO_C"].ToString().Trim();
            txtClosingBalanceAmount.Text = dt.Rows[0]["CL_TOT_D"].ToString().Trim();
            txtCBDisabled.Text = dt.Rows[0]["CL_TOT_C"].ToString().Trim();
            txtEEFCAccounts.Text = dt.Rows[0]["EEFCAC"].ToString().Trim();
            txtEFCAccounts.Text = dt.Rows[0]["EFCAC"].ToString().Trim();
            txtRFCAccounts.Text = dt.Rows[0]["RFCAC"].ToString().Trim();
            txtEscrowFCAccounts.Text = dt.Rows[0]["ESCROWAC"].ToString().Trim();
            txtFCNRBAccounts.Text = dt.Rows[0]["FCNRAC"].ToString().Trim();
            txtOtherFCAccounts.Text = dt.Rows[0]["OTHERAC"].ToString().Trim();
        }
        else
        {
            clearControls();
        }
    }
    protected void btnCurr_Click(object sender, EventArgs e)
    {
        if (hdnCurId.Value != "")
        {
            txtCurrency.Text = hdnCurId.Value;
            txtCurrencyDescription.Text = hdnCurName.Value;
            txtCurrency.Focus();
        }
    }
    protected void btnfillgrid_Click(object sender, EventArgs e)
    {
        clearTextboxControls();
        fillDetails();
        OBCashBalanceDR.Focus();
    }
    protected void btnCurrencyfillgrid_Click(object sender, EventArgs e)
    {
        clearTextboxControls();
        fillDetails();
        txtCurrency.Focus();
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
        string _NV = "N";
        string _CURR = txtCurrency.Text.ToString();
        string _OP_CASH_D = OBCashBalanceDR.Text.Trim();
        string _OP_CASH_C = OBCashBalanceCR.Text.Trim();
        string _OP_SUSP_D = OBSuspAccountDR.Text.Trim();
        string _OP_SUSP_C = OBSuspAccountCR.Text.Trim();
        string _OP_DEP_OTH_D = OBDepositOtherDR.Text.Trim();
        string _OP_DEP_OTH_C = OBDepositOtherCR.Text.Trim();
        string _OP_DEP_RBI_D = OBDepositRBIDR.Text.Trim();
        string _OP_DEP_RBI_C = OBDepositRBICR.Text.Trim();
        string _OP_FD_D = OBFixedDepositDR.Text.Trim();
        string _OP_FD_C = OBFixedDepositCR.Text.Trim();
        string _OP_TB_D = OBTreasuryBillDR.Text.Trim();
        string _OP_TB_C = OBTreasuryBillCR.Text.Trim();
        string _OP_SS_D = OBSecuritiesDR.Text.Trim();
        string _OP_SS_C = OBSecuritiesCR.Text.Trim();
        string _OP_FCLO_D = OBFCurrencyDR.Text.Trim();
        string _OP_FCLO_C = OBFCurrencyCR.Text.Trim();
        string _OP_OTH_D = "0";
        string _OP_OTH_C = "0";
        string _OP_TOT_D = txtOpeningBalanceAmount.Text.Trim();
        string _OP_TOT_C = txtOBDisabled.Text.Trim();
        string _CL_CASH_D = CBCashBalanceDR.Text.Trim();
        string _CL_CASH_C = CBCashBalanceCR.Text.Trim();
        string _CL_SUSP_D = CBSuspAccountDR.Text.Trim();
        string _CL_SUSP_C = CBSuspAccountCR.Text.Trim();
        string _CL_DEP_OTH_D = CBDepositOtherDR.Text.Trim();
        string _CL_DEP_OTH_C = CBDepositOtherCR.Text.Trim();
        string _CL_DEP_RBI_D = CBDepositRBIDR.Text.Trim();
        string _CL_DEP_RBI_C = CBDepositRBICR.Text.Trim();
        string _CL_FD_D = CBFixedDepositDR.Text.Trim();
        string _CL_FD_C = CBFixedDepositCR.Text.Trim();
        string _CL_TB_D = CBTreasuryBillsDR.Text.Trim();
        string _CL_TB_C = CBTreasuryBillsCR.Text.Trim();
        string _CL_SS_D = CBSecuritiesDR.Text.Trim();
        string _CL_SS_C = CBSecuritiesCR.Text.Trim();
        string _CL_FCLO_D = CBForeignCurrencyDR.Text.Trim();
        string _CL_FCLO_C = CBForeignCurrencyCR.Text.Trim();
        string _CL_OTH_D = "0";
        string _CL_OTH_C = "0";
        string _CL_TOT_D = txtClosingBalanceAmount.Text.Trim();
        string _CL_TOT_C = txtCBDisabled.Text.Trim();
        string _EEFCAC = txtEEFCAccounts.Text.Trim();
        string _EFCAC = txtEFCAccounts.Text.Trim();
        string _RFCAC = txtRFCAccounts.Text.Trim();
        string _ESCROWAC = txtEscrowFCAccounts.Text.Trim();
        string _FCNRAC = txtFCNRBAccounts.Text.Trim();
        string _OTHERAC = txtOtherFCAccounts.Text.Trim();
        if (_OP_CASH_D == "")
            _OP_CASH_D = "0";
        if (_OP_CASH_C == "")
            _OP_CASH_C = "0";
        if (_OP_SUSP_D == "")
            _OP_SUSP_D = "0";
        if (_OP_SUSP_C == "")
            _OP_SUSP_C = "0";
        if (_OP_DEP_OTH_D == "")
            _OP_DEP_OTH_D = "0";
        if (_OP_DEP_OTH_C == "")
            _OP_DEP_OTH_C = "0";
        if (_OP_DEP_RBI_D == "")
            _OP_DEP_RBI_D = "0";
        if (_OP_DEP_RBI_C == "")
            _OP_DEP_RBI_C = "0";
        if (_OP_FD_D == "")
            _OP_FD_D = "0";
        if (_OP_FD_C == "")
            _OP_FD_C = "0";
        if (_OP_TB_D == "")
            _OP_TB_D = "0";
        if (_OP_TB_C == "")
            _OP_TB_C = "0";
        if (_OP_SS_D == "")
            _OP_SS_D = "0";
        if (_OP_SS_C == "")
            _OP_SS_C = "0";
        if (_OP_FCLO_D == "")
            _OP_FCLO_D = "0";
        if (_OP_FCLO_C == "")
            _OP_FCLO_C = "0";
        if (_OP_OTH_D == "")
            _OP_OTH_D = "0";
        if (_OP_OTH_C == "")
            _OP_OTH_C = "0";
        if (_OP_TOT_D == "")
            _OP_TOT_D = "0";
        if (_OP_TOT_C == "")
            _OP_TOT_C = "0";
        if (_CL_CASH_D == "")
            _CL_CASH_D = "0";
        if (_CL_CASH_C == "")
            _CL_CASH_C = "0";
        if (_CL_SUSP_D == "")
            _CL_SUSP_D = "0";
        if (_CL_SUSP_C == "")
            _CL_SUSP_C = "0";
        if (_CL_DEP_OTH_D == "")
            _CL_DEP_OTH_D = "0";
        if (_CL_DEP_OTH_C == "")
            _CL_DEP_OTH_C = "0";
        if (_CL_DEP_RBI_D == "")
            _CL_DEP_RBI_D = "0";
        if (_CL_DEP_RBI_C == "")
            _CL_DEP_RBI_C = "0";
        if (_CL_FD_D == "")
            _CL_FD_D = "0";
        if (_CL_FD_C == "")
            _CL_FD_C = "0";
        if (_CL_TB_D == "")
            _CL_TB_D = "0";
        if (_CL_TB_C == "")
            _CL_TB_C = "0";
        if (_CL_SS_D == "")
            _CL_SS_D = "0";
        if (_CL_SS_C == "")
            _CL_SS_C = "0";
        if (_CL_FCLO_D == "")
            _CL_FCLO_D = "0";
        if (_CL_FCLO_C == "")
            _CL_FCLO_C = "0";
        if (_CL_OTH_D == "")
            _CL_OTH_D = "0";
        if (_CL_OTH_C == "")
            _CL_OTH_C = "0";
        if (_CL_TOT_D == "")
            _CL_TOT_D = "0";
        if (_CL_TOT_C == "")
            _CL_TOT_C = "0";
        if (_EEFCAC == "")
            _EEFCAC = "0";
        if (_EFCAC == "")
            _EFCAC = "0";
        if (_RFCAC == "")
            _RFCAC = "0";
        if (_ESCROWAC == "")
            _ESCROWAC = "0";
        if (_FCNRAC == "")
            _FCNRAC = "0";
        if (_OTHERAC == "")
            _OTHERAC = "0";
        SqlParameter NextFromDate = new SqlParameter("@NextFromDate", SqlDbType.VarChar);
        SqlParameter NextToDate = new SqlParameter("@NextToDate", SqlDbType.VarChar);
        SqlParameter ADCODE = new SqlParameter("@ADCODE", SqlDbType.VarChar);
        SqlParameter FR_FORTNIGHT_DT = new SqlParameter("@FR_FORTNIGHT_DT", SqlDbType.VarChar);
        SqlParameter TO_FORTNIGHT_DT = new SqlParameter("@TO_FORTNIGHT_DT", SqlDbType.VarChar);
        SqlParameter NV = new SqlParameter("@NV", SqlDbType.VarChar);
        SqlParameter CURR = new SqlParameter("@CURR", SqlDbType.VarChar);
        SqlParameter OP_CASH_D = new SqlParameter("@OP_CASH_D", SqlDbType.VarChar);
        SqlParameter OP_CASH_C = new SqlParameter("@OP_CASH_C", SqlDbType.VarChar);
        SqlParameter OP_SUSP_D = new SqlParameter("@OP_SUSP_D", SqlDbType.VarChar);
        SqlParameter OP_SUSP_C = new SqlParameter("@OP_SUSP_C", SqlDbType.VarChar);
        SqlParameter OP_DEP_OTH_D = new SqlParameter("@OP_DEP_OTH_D", SqlDbType.VarChar);
        SqlParameter OP_DEP_OTH_C = new SqlParameter("@OP_DEP_OTH_C", SqlDbType.VarChar);
        SqlParameter OP_DEP_RBI_D = new SqlParameter("@OP_DEP_RBI_D", SqlDbType.VarChar);
        SqlParameter OP_DEP_RBI_C = new SqlParameter("@OP_DEP_RBI_C", SqlDbType.VarChar);
        SqlParameter OP_FD_D = new SqlParameter("@OP_FD_D", SqlDbType.VarChar);
        SqlParameter OP_FD_C = new SqlParameter("@OP_FD_C", SqlDbType.VarChar);
        SqlParameter OP_TB_D = new SqlParameter("@OP_TB_D", SqlDbType.VarChar);
        SqlParameter OP_TB_C = new SqlParameter("@OP_TB_C", SqlDbType.VarChar);
        SqlParameter OP_SS_D = new SqlParameter("@OP_SS_D", SqlDbType.VarChar);
        SqlParameter OP_SS_C = new SqlParameter("@OP_SS_C", SqlDbType.VarChar);
        SqlParameter OP_FCLO_D = new SqlParameter("@OP_FCLO_D", SqlDbType.VarChar);
        SqlParameter OP_FCLO_C = new SqlParameter("@OP_FCLO_C", SqlDbType.VarChar);
        SqlParameter OP_OTH_D = new SqlParameter("@OP_OTH_D", SqlDbType.VarChar);
        SqlParameter OP_OTH_C = new SqlParameter("@OP_OTH_C", SqlDbType.VarChar);
        SqlParameter OP_TOT_D = new SqlParameter("@OP_TOT_D", SqlDbType.VarChar);
        SqlParameter OP_TOT_C = new SqlParameter("@OP_TOT_C", SqlDbType.VarChar);
        SqlParameter CL_CASH_D = new SqlParameter("@CL_CASH_D", SqlDbType.VarChar);
        SqlParameter CL_CASH_C = new SqlParameter("@CL_CASH_C", SqlDbType.VarChar);
        SqlParameter CL_SUSP_D = new SqlParameter("@CL_SUSP_D", SqlDbType.VarChar);
        SqlParameter CL_SUSP_C = new SqlParameter("@CL_SUSP_C", SqlDbType.VarChar);
        SqlParameter CL_DEP_OTH_D = new SqlParameter("@CL_DEP_OTH_D", SqlDbType.VarChar);
        SqlParameter CL_DEP_OTH_C = new SqlParameter("@CL_DEP_OTH_C", SqlDbType.VarChar);
        SqlParameter CL_DEP_RBI_D = new SqlParameter("@CL_DEP_RBI_D", SqlDbType.VarChar);
        SqlParameter CL_DEP_RBI_C = new SqlParameter("@CL_DEP_RBI_C", SqlDbType.VarChar);
        SqlParameter CL_FD_D = new SqlParameter("@CL_FD_D", SqlDbType.VarChar);
        SqlParameter CL_FD_C = new SqlParameter("@CL_FD_C", SqlDbType.VarChar);
        SqlParameter CL_TB_D = new SqlParameter("@CL_TB_D", SqlDbType.VarChar);
        SqlParameter CL_TB_C = new SqlParameter("@CL_TB_C", SqlDbType.VarChar);
        SqlParameter CL_SS_D = new SqlParameter("@CL_SS_D", SqlDbType.VarChar);
        SqlParameter CL_SS_C = new SqlParameter("@CL_SS_C", SqlDbType.VarChar);
        SqlParameter CL_FCLO_D = new SqlParameter("@CL_FCLO_D", SqlDbType.VarChar);
        SqlParameter CL_FCLO_C = new SqlParameter("@CL_FCLO_C", SqlDbType.VarChar);
        SqlParameter CL_OTH_D = new SqlParameter("@CL_OTH_D", SqlDbType.VarChar);
        SqlParameter CL_OTH_C = new SqlParameter("@CL_OTH_C", SqlDbType.VarChar);
        SqlParameter CL_TOT_D = new SqlParameter("@CL_TOT_D", SqlDbType.VarChar);
        SqlParameter CL_TOT_C = new SqlParameter("@CL_TOT_C", SqlDbType.VarChar);
        SqlParameter EEFCAC = new SqlParameter("@EEFCAC", SqlDbType.VarChar);
        SqlParameter EFCAC = new SqlParameter("@EFCAC", SqlDbType.VarChar);
        SqlParameter RFCAC = new SqlParameter("@RFCAC", SqlDbType.VarChar);
        SqlParameter ESCROWAC = new SqlParameter("@ESCROWAC", SqlDbType.VarChar);
        SqlParameter FCNRAC = new SqlParameter("@FCNRAC", SqlDbType.VarChar);
        SqlParameter OTHERAC = new SqlParameter("@OTHERAC", SqlDbType.VarChar);
        NextFromDate.Value = from_Date;
        NextToDate.Value = to_Date;
        ADCODE.Value = _ADCODE;
        FR_FORTNIGHT_DT.Value = _FR_FORTNIGHT_DT;
        TO_FORTNIGHT_DT.Value = _TO_FORTNIGHT_DT;
        NV.Value = _NV;
        CURR.Value = _CURR;
        OP_CASH_D.Value = _OP_CASH_D;
        OP_CASH_C.Value = _OP_CASH_C;
        OP_SUSP_D.Value = _OP_SUSP_D;
        OP_SUSP_C.Value = _OP_SUSP_C;
        OP_DEP_OTH_D.Value = _OP_DEP_OTH_D;
        OP_DEP_OTH_C.Value = _OP_DEP_OTH_C;
        OP_DEP_RBI_D.Value = _OP_DEP_RBI_D;
        OP_DEP_RBI_C.Value = _OP_DEP_RBI_C;
        OP_FD_D.Value = _OP_FD_D;
        OP_FD_C.Value = _OP_FD_C;
        OP_TB_D.Value = _OP_TB_D;
        OP_TB_C.Value = _OP_TB_C;
        OP_SS_D.Value = _OP_SS_D;
        OP_SS_C.Value = _OP_SS_C;
        OP_FCLO_D.Value = _OP_FCLO_D;
        OP_FCLO_C.Value = _OP_FCLO_C;
        OP_OTH_D.Value = _OP_OTH_D;
        OP_OTH_C.Value = _OP_OTH_C;
        OP_TOT_D.Value = _OP_TOT_D;
        OP_TOT_C.Value = _OP_TOT_C;
        CL_CASH_D.Value = _CL_CASH_D;
        CL_CASH_C.Value = _CL_CASH_C;
        CL_SUSP_D.Value = _CL_SUSP_D;
        CL_SUSP_C.Value = _CL_SUSP_C;
        CL_DEP_OTH_D.Value = _CL_DEP_OTH_D;
        CL_DEP_OTH_C.Value = _CL_DEP_OTH_C;
        CL_DEP_RBI_D.Value = _CL_DEP_RBI_D;
        CL_DEP_RBI_C.Value = _CL_DEP_RBI_C;
        CL_FD_D.Value = _CL_FD_D;
        CL_FD_C.Value = _CL_FD_C;
        CL_TB_D.Value = _CL_TB_D;
        CL_TB_C.Value = _CL_TB_C;
        CL_SS_D.Value = _CL_SS_D;
        CL_SS_C.Value = _CL_SS_C;
        CL_FCLO_D.Value = _CL_FCLO_D;
        CL_FCLO_C.Value = _CL_FCLO_C;
        CL_OTH_D.Value = _CL_OTH_D;
        CL_OTH_C.Value = _CL_OTH_C;
        CL_TOT_D.Value = _CL_TOT_D;
        CL_TOT_C.Value = _CL_TOT_C;
        EEFCAC.Value = _EEFCAC;
        EFCAC.Value = _EFCAC;
        RFCAC.Value = _RFCAC;
        ESCROWAC.Value = _ESCROWAC;
        FCNRAC.Value = _FCNRAC;
        OTHERAC.Value = _OTHERAC;
        string _result = "";
        string _query = "TF_RET_UpdateNostroOpeningClosingBalance";
        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData(_query, NextFromDate, NextToDate, ADCODE, FR_FORTNIGHT_DT, TO_FORTNIGHT_DT, NV, CURR, OP_CASH_D, OP_CASH_C, OP_SUSP_D,
                                 OP_SUSP_C, OP_DEP_OTH_D, OP_DEP_OTH_C, OP_DEP_RBI_D, OP_DEP_RBI_C, OP_FD_D, OP_FD_C, OP_TB_D, OP_TB_C,
                                 OP_SS_D, OP_SS_C, OP_FCLO_D, OP_FCLO_C, OP_OTH_D, OP_OTH_C, OP_TOT_D, OP_TOT_C, CL_CASH_D, CL_CASH_C, CL_SUSP_D,
                                 CL_SUSP_C, CL_DEP_OTH_D, CL_DEP_OTH_C, CL_DEP_RBI_D, CL_DEP_RBI_C, CL_FD_D, CL_FD_C, CL_TB_D, CL_TB_C, CL_SS_D,
                                 CL_SS_C, CL_FCLO_D, CL_FCLO_C, CL_OTH_D, CL_OTH_C, CL_TOT_D, CL_TOT_C, EEFCAC, EFCAC, RFCAC, ESCROWAC, FCNRAC, OTHERAC);
        string _script = "";
        if (_result == "added")
        {
            _script = "alert('Record Added.')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            clearControls();
            txtCurrency.Text = "";
            txtCurrencyDescription.Text = "";
        }
        else
        {
            labelMessage.Text = _result;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearControls();
        TabContainerMain.ActiveTab = tpOpeningBalance;
        Response.Redirect("RET_AddEditNostroOpCloBalanceDataEntry.aspx?PageHeader=Nostro Opening Closing balance Data Entry", true);
    }
    protected void dropDownListCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDetails();
        OBCashBalanceDR.Focus();
    }
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        txtToDate.Focus();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Ret_Main.aspx?PageHeader=RET Main", true);
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetADCode();
        fillDetails();
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
    protected void autoFillOpeningBalance()
    {
        DateTime selectedDate = new DateTime();
        DateTime fromDate = new DateTime();
        DateTime toDate = new DateTime();
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";
        selectedDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
        if (selectedDate.Day == 16)
        {
            string month = (selectedDate.Month + 1).ToString();
            string year = selectedDate.Year.ToString();
            if (month == "13")
            {
                month = "1";
                year = (int.Parse(year) + 1).ToString();
            }
            fromDate = DateTime.Parse("1/" + month + "/" + year);
            toDate = fromDate.AddDays(15);
        }
        else
        {
            string month = (selectedDate.Month).ToString();
            string year = selectedDate.Year.ToString();
            fromDate = DateTime.Parse("16/" + month + "/" + year);
            toDate = DateTime.Parse(DateTime.DaysInMonth(fromDate.Year, fromDate.Month).ToString() + "/" + (fromDate.Month).ToString() + "/" + fromDate.Year.ToString());
        }
        string from_Date = fromDate.ToString("dd/MM/yyyy").Trim();
        string to_Date = toDate.ToString("dd/MM/yyyy").Trim();
        // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record From Date " + from_Date + " To :"+ to_Date + "');", true);
    }
    protected void clearTextboxControls()
    {
        OBCashBalanceDR.Text = "";
        OBSuspAccountDR.Text = "";
        OBDepositOtherDR.Text = "";
        OBDepositRBIDR.Text = "";
        OBFixedDepositDR.Text = "";
        OBTreasuryBillDR.Text = "";
        OBSecuritiesDR.Text = "";
        OBFCurrencyDR.Text = "";
        OBCashBalanceCR.Text = "";
        OBSuspAccountCR.Text = "";
        OBDepositOtherCR.Text = "";
        OBDepositRBICR.Text = "";
        OBFixedDepositCR.Text = "";
        OBTreasuryBillCR.Text = "";
        OBSecuritiesCR.Text = "";
        OBFCurrencyCR.Text = "";
        txtOpeningBalanceAmount.Text = "";
        txtOBDisabled.Text = "";
        CBCashBalanceDR.Text = "";
        CBSuspAccountDR.Text = "";
        CBDepositOtherDR.Text = "";
        CBDepositRBIDR.Text = "";
        CBFixedDepositDR.Text = "";
        CBTreasuryBillsDR.Text = "";
        CBSecuritiesDR.Text = "";
        CBForeignCurrencyDR.Text = "";
        CBCashBalanceCR.Text = "";
        CBSuspAccountCR.Text = "";
        CBDepositOtherCR.Text = "";
        CBDepositRBICR.Text = "";
        CBFixedDepositCR.Text = "";
        CBTreasuryBillsCR.Text = "";
        CBSecuritiesCR.Text = "";
        CBForeignCurrencyCR.Text = "";
        txtClosingBalanceAmount.Text = "";
        txtCBDisabled.Text = "";
        txtEEFCAccounts.Text = "";
        txtEFCAccounts.Text = "";
        txtRFCAccounts.Text = "";
        txtEscrowFCAccounts.Text = "";
        txtFCNRBAccounts.Text = "";
        txtOtherFCAccounts.Text = "";
    }
    protected void clearControls()
    {
        TabContainerMain.ActiveTab = tpOpeningBalance;
        //ddlBranch.SelectedIndex = -1;
        //dropDownListPurposeCode.SelectedIndex = -1;
        //rbtnCredit.Checked = false;
        //rbtnDebit.Checked = false;
        //txtCurrency.Text = "";
        //txtCurrencyDescription.Text = "";
        //txtFromDate.Text = "";
        //txtToDate.Text = "";
        OBCashBalanceDR.Text = "";
        OBSuspAccountDR.Text = "";
        OBDepositOtherDR.Text = "";
        OBDepositRBIDR.Text = "";
        OBFixedDepositDR.Text = "";
        OBTreasuryBillDR.Text = "";
        OBSecuritiesDR.Text = "";
        OBFCurrencyDR.Text = "";
        OBCashBalanceCR.Text = "";
        OBSuspAccountCR.Text = "";
        OBDepositOtherCR.Text = "";
        OBDepositRBICR.Text = "";
        OBFixedDepositCR.Text = "";
        OBTreasuryBillCR.Text = "";
        OBSecuritiesCR.Text = "";
        OBFCurrencyCR.Text = "";
        txtOpeningBalanceAmount.Text = "";
        txtOBDisabled.Text = "";
        CBCashBalanceDR.Text = "";
        CBSuspAccountDR.Text = "";
        CBDepositOtherDR.Text = "";
        CBDepositRBIDR.Text = "";
        CBFixedDepositDR.Text = "";
        CBTreasuryBillsDR.Text = "";
        CBSecuritiesDR.Text = "";
        CBForeignCurrencyDR.Text = "";
        CBCashBalanceCR.Text = "";
        CBSuspAccountCR.Text = "";
        CBDepositOtherCR.Text = "";
        CBDepositRBICR.Text = "";
        CBFixedDepositCR.Text = "";
        CBTreasuryBillsCR.Text = "";
        CBSecuritiesCR.Text = "";
        CBForeignCurrencyCR.Text = "";
        txtClosingBalanceAmount.Text = "";
        txtCBDisabled.Text = "";
        txtEEFCAccounts.Text = "";
        txtEFCAccounts.Text = "";
        txtRFCAccounts.Text = "";
        txtEscrowFCAccounts.Text = "";
        txtFCNRBAccounts.Text = "";
        txtOtherFCAccounts.Text = "";
        hdnDebitOpnBal.Value = "0";
        hdnCreditOpnBal.Value = "0";
        hdnDebitClBal.Value = "0";
        hdnCreditClBal.Value = "0";
        hdnEEFCAC.Value = "0";
        hdnEFCAC.Value = "0";
        hdnRFCAC.Value = "0";
        hdnESCROWAC.Value = "0";
        hdnFCNRAC.Value = "0";
        hdnOHTRFCAC.Value = "0";
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        fillDetails();
        OBCashBalanceDR.Focus();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        TF_DATA ObjData = new TF_DATA();
        string _query = "TF_RET_DeleteNostroOpeningClosingBalance";
        SqlParameter p1 = new SqlParameter("@adcode", SqlDbType.VarChar);
        p1.Value = ddlBranch.Text.ToString().Trim();
        SqlParameter p2 = new SqlParameter("@currencyid", SqlDbType.VarChar);
        p2.Value = txtCurrency.Text.ToString();
        SqlParameter p3 = new SqlParameter("@fromdate", SqlDbType.VarChar);
        p3.Value = txtFromDate.Text.ToString();
        SqlParameter p4 = new SqlParameter("@todate", SqlDbType.VarChar);
        p4.Value = txtToDate.Text.ToString();
        string _result;
        _result = ObjData.SaveDeleteData(_query, p1, p2, p3, p4);
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
    protected void txtCurrency_TextChanged(object sender, EventArgs e)
    {
        txtCurrency.Text = txtCurrency.Text.ToUpper();
        fillCurrencyDes();
        fillDetails();        
    }
    protected void fillBank()
    {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_RET_GetBankName";
        DataTable dt = objData.getData(_query);
        if (dt.Rows.Count > 0)
        {
            lblBankname.Text = dt.Rows[0]["BankName"].ToString();
        }
    }
    public void fillCurrencyDes()
    {
        if (txtCurrency.Text != "")
        {
            txtCurrencyDescription.Text = "";
            SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
            string _query = "TF_ValidateCurrencyList";
            p1.Value = txtCurrency.Text.Trim();
            TF_DATA objData = new TF_DATA();
            DataTable dt = objData.getData(_query, p1);
            if (dt.Rows.Count > 0)
            {
                txtCurrencyDescription.Text = dt.Rows[0][2].ToString().Trim();
                OBCashBalanceDR.Focus();
                if (txtCurrency.Text == "INR" || txtCurrency.Text == "ACD")
                {
                    txtCurrency.Text = "";
                    txtCurrencyDescription.Text = "<font style=color:red>Invalid Currency</font>";
                    txtCurrency.Focus();
                }
            }
            else
            {
                txtCurrency.Text = "";
                txtCurrencyDescription.Text = "<font style=color:red>Invalid Currency</font>";
                txtCurrency.Focus();
            }
        }
        else
        {
            txtCurrencyDescription.Text = "";
        }
    }
}