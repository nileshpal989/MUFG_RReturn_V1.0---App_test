using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class PC_PC_AddEditCurrencyRate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {

            if (!IsPostBack)
            {
                btnDelete.Enabled = false;
                hdnMODE_DEMcurr.Value = "add";
                hdnMODE_RUBcurr.Value = "add";
                hdnMODE_INRcurr.Value = "add";
                Clear();
                txtDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtDate_TextChanged(null, null);
                txtUSD_SRate.Attributes.Add("onblur", "return CalculateCrossRates();");
                txtGBP_SRate.Attributes.Add("onblur", "return CalculateCrossRates();");
                txtEur_SRate.Attributes.Add("onblur", "return CalculateCrossRates();");
                txtJPY_SRate.Attributes.Add("onblur", "return CalculateCrossRates();");
                txtCHF_SRate.Attributes.Add("onblur", "return CalculateCrossRates();");
                txtAUD_SRate.Attributes.Add("onblur", "return CalculateCrossRates();");
                txtCAD_SRate.Attributes.Add("onblur", "return CalculateCrossRates();");
                txtSGD_SRate.Attributes.Add("onblur", "return CalculateCrossRates();");
                txtSEK_SRate.Attributes.Add("onblur", "return CalculateCrossRates();");
                txtHKD_SRate.Attributes.Add("onblur", "return CalculateCrossRates();");
                txtSAR_SRate.Attributes.Add("onblur", "return CalculateCrossRates();");
                txtDEM_SRate.Attributes.Add("onblur", "return CalculateCrossRates();");
                txtRUB_SRate.Attributes.Add("onblur", "return CalculateCrossRates();");
                txtINR_SRate.Attributes.Add("onblur", "return CalculateCrossRates();");

                txtUSD_CRate.Attributes.Add("onkeypress", "return false;");
                txtGBP_CRate.Attributes.Add("onkeypress", "return false;");
                txtEur_CRate.Attributes.Add("onkeypress", "return false;");
                txtJPY_CRate.Attributes.Add("onkeypress", "return false;");
                txtCHF_CRate.Attributes.Add("onkeypress", "return false;");
                txtAUD_CRate.Attributes.Add("onkeypress", "return false;");
                txtCAD_CRate.Attributes.Add("onkeypress", "return false;");
                txtSGD_CRate.Attributes.Add("onkeypress", "return false;");
                txtSEK_CRate.Attributes.Add("onkeypress", "return false;");
                txtHKD_CRate.Attributes.Add("onkeypress", "return false;");
                txtSAR_CRate.Attributes.Add("onkeypress", "return false;");
                txtDEM_CRate.Attributes.Add("onkeypress", "return false;");
                txtRUB_CRate.Attributes.Add("onkeypress", "return false;");
                txtINR_CRate.Attributes.Add("onkeypress", "return false;");


                txtUSD_CRate.Attributes.Add("onkeydown", "return false;");
                txtGBP_CRate.Attributes.Add("onkeydown", "return false;");
                txtEur_CRate.Attributes.Add("onkeydown", "return false;");
                txtJPY_CRate.Attributes.Add("onkeydown", "return false;");
                txtCHF_CRate.Attributes.Add("onkeydown", "return false;");
                txtAUD_CRate.Attributes.Add("onkeydown", "return false;");
                txtCAD_CRate.Attributes.Add("onkeydown", "return false;");
                txtSGD_CRate.Attributes.Add("onkeydown", "return false;");
                txtSEK_CRate.Attributes.Add("onkeydown", "return false;");
                txtHKD_CRate.Attributes.Add("onkeydown", "return false;");
                txtSAR_CRate.Attributes.Add("onkeydown", "return false;");
                txtDEM_CRate.Attributes.Add("onkeydown", "return false;");
                txtRUB_CRate.Attributes.Add("onkeydown", "return false;");
                txtINR_CRate.Attributes.Add("onkeydown", "return false;");

                btnSave.Attributes.Add("onclick", "return validateSave();");
                txtDate.Attributes.Add("onblur", "return isValidDate(" + txtDate.ClientID + "," + "'Date'" + " );");
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _result="";

        TF_DATA objData = new TF_DATA();
        string _query = "TF_PC_UpdateCurrencyRate";

        SqlParameter pMode = new SqlParameter("@mode", SqlDbType.VarChar);
        if (btnSave.Text=="Save")
        pMode.Value = "add";
        else
        pMode.Value = "edit";

        SqlParameter pDate = new SqlParameter("@date", SqlDbType.VarChar);
        pDate.Value = txtDate.Text.Trim();

        SqlParameter pUser = new SqlParameter("@user", SqlDbType.VarChar);
        pUser.Value = _userName;

        SqlParameter pUploadDate = new SqlParameter("@uploadDate", SqlDbType.VarChar);
        pUploadDate.Value = _uploadingDate;

        //----------save USD details-----------//
        SqlParameter pCurrUSD = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrUSD.Value = txtCurrUSD.Text.Trim();

        SqlParameter pUSDSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pUSDSrate.Value = txtUSD_SRate.Text.Trim();

        SqlParameter pUSDCrate = new SqlParameter("@CRate", SqlDbType.VarChar);
        pUSDCrate.Value = txtUSD_CRate.Text.Trim();

        _result = objData.SaveDeleteData(_query,pMode ,pDate,pCurrUSD,pUSDSrate,pUSDCrate,pUser,pUploadDate);

        //----------save GBP details-----------//
        SqlParameter pCurrGBP = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrGBP.Value = txtCurrGBP.Text.Trim();

        SqlParameter pGBPSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pGBPSrate.Value = txtGBP_SRate.Text.Trim();

        SqlParameter pGBPCrate = new SqlParameter("@CRate", SqlDbType.VarChar);
        pGBPCrate.Value = txtGBP_CRate.Text.Trim();

        _result = objData.SaveDeleteData(_query, pMode, pDate, pCurrGBP, pGBPSrate, pGBPCrate, pUser, pUploadDate);

        //----------save EUR details-----------//
        SqlParameter pCurrEUR = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrEUR.Value = txtCurrEur.Text.Trim();

        SqlParameter pEURSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pEURSrate.Value = txtEur_SRate.Text.Trim();

        SqlParameter pEURCrate = new SqlParameter("@CRate", SqlDbType.VarChar);
        pEURCrate.Value = txtEur_CRate.Text.Trim();

        _result = objData.SaveDeleteData(_query, pMode, pDate, pCurrEUR, pEURSrate, pEURCrate, pUser, pUploadDate);

        //----------save JPY details-----------//
        SqlParameter pCurrJPY = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrJPY.Value = txtCurrJPY.Text.Trim();

        SqlParameter pJPYSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pJPYSrate.Value = txtJPY_SRate.Text.Trim();

        SqlParameter pJPYCrate = new SqlParameter("@CRate", SqlDbType.VarChar);
        pJPYCrate.Value = txtJPY_CRate.Text.Trim();

        _result = objData.SaveDeleteData(_query, pMode, pDate, pCurrJPY, pJPYSrate, pJPYCrate, pUser, pUploadDate);

        //----------save CHF details-----------//
        SqlParameter pCurrCHF = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrCHF.Value = txtCurrCHF.Text.Trim();

        SqlParameter pCHFSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pCHFSrate.Value = txtCHF_SRate.Text.Trim();

        SqlParameter pCHFCrate = new SqlParameter("@CRate", SqlDbType.VarChar);
        pCHFCrate.Value = txtCHF_CRate.Text.Trim();

        _result = objData.SaveDeleteData(_query, pMode, pDate, pCurrCHF, pCHFSrate, pCHFCrate, pUser, pUploadDate);

        //----------save AUD details-----------//
        SqlParameter pCurrAUD = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrAUD.Value = txtCurrAUD.Text.Trim();

        SqlParameter pAUDSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pAUDSrate.Value = txtAUD_SRate.Text.Trim();

        SqlParameter pAUDCrate = new SqlParameter("@CRate", SqlDbType.VarChar);
        pAUDCrate.Value = txtAUD_CRate.Text.Trim();

        _result = objData.SaveDeleteData(_query, pMode, pDate, pCurrAUD, pAUDSrate, pAUDCrate, pUser, pUploadDate);

        //----------save CAD details-----------//
        SqlParameter pCurrCAD = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrCAD.Value = txtCurrCAD.Text.Trim();

        SqlParameter pCADSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pCADSrate.Value = txtCAD_SRate.Text.Trim();

        SqlParameter pCADCrate = new SqlParameter("@CRate", SqlDbType.VarChar);
        pCADCrate.Value = txtCAD_CRate.Text.Trim();

        _result = objData.SaveDeleteData(_query, pMode, pDate, pCurrCAD, pCADSrate, pCADCrate, pUser, pUploadDate);

        //----------save SGD details-----------//
        SqlParameter pCurrSGD = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrSGD.Value = txtCurrSGD.Text.Trim();

        SqlParameter pSGDSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pSGDSrate.Value = txtSGD_SRate.Text.Trim();

        SqlParameter pSGDCrate = new SqlParameter("@CRate", SqlDbType.VarChar);
        pSGDCrate.Value = txtSGD_CRate.Text.Trim();

        _result = objData.SaveDeleteData(_query, pMode, pDate, pCurrSGD, pSGDSrate, pSGDCrate, pUser, pUploadDate);

        //----------save SEK details-----------//
        SqlParameter pCurrSEK = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrSEK.Value = txtCurrSEK.Text.Trim();

        SqlParameter pSEKSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pSEKSrate.Value = txtSEK_SRate.Text.Trim();

        SqlParameter pSEKCrate = new SqlParameter("@CRate", SqlDbType.VarChar);
        pSEKCrate.Value = txtSEK_CRate.Text.Trim();

        _result = objData.SaveDeleteData(_query, pMode, pDate, pCurrSEK, pSEKSrate, pSEKCrate, pUser, pUploadDate);

        //----------save HKD details-----------//
        SqlParameter pCurrHKD = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrHKD.Value = txtCurrHKD.Text.Trim();

        SqlParameter pHKDSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pHKDSrate.Value = txtHKD_SRate.Text.Trim();

        SqlParameter pHKDCrate = new SqlParameter("@CRate", SqlDbType.VarChar);
        pHKDCrate.Value = txtHKD_CRate.Text.Trim();

        _result = objData.SaveDeleteData(_query, pMode, pDate, pCurrHKD, pHKDSrate, pHKDCrate, pUser, pUploadDate);

        //----------save SAR details-----------//
        SqlParameter pCurrSAR = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrSAR.Value = txtCurrSAR.Text.Trim();

        SqlParameter pSARSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pSARSrate.Value = txtSAR_SRate.Text.Trim();

        SqlParameter pSARCrate = new SqlParameter("@CRate", SqlDbType.VarChar);
        pSARCrate.Value = txtSAR_CRate.Text.Trim();

        _result = objData.SaveDeleteData(_query, pMode, pDate, pCurrSAR, pSARSrate, pSARCrate, pUser, pUploadDate);

        //----------save DEM details-----------//
        SqlParameter pCurrDEM = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrDEM.Value = txtCurrDEM.Text.Trim();

        SqlParameter pDEMSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pDEMSrate.Value = txtDEM_SRate.Text.Trim();

        SqlParameter pDEMCrate = new SqlParameter("@CRate", SqlDbType.VarChar);
        pDEMCrate.Value = txtDEM_CRate.Text.Trim();

        pMode.Value = hdnMODE_DEMcurr.Value;
        _result = objData.SaveDeleteData(_query, pMode, pDate, pCurrDEM, pDEMSrate, pDEMCrate, pUser, pUploadDate);


        //----------save RUB details-----------//
        SqlParameter pCurrRUB = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrRUB.Value = txtCurrRUB.Text.Trim();

        SqlParameter pRUBSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pRUBSrate.Value = txtRUB_SRate.Text.Trim();

        SqlParameter pRUBCrate = new SqlParameter("@CRate", SqlDbType.VarChar);
        pRUBCrate.Value = txtRUB_CRate.Text.Trim();

        pMode.Value = hdnMODE_RUBcurr.Value;
        _result = objData.SaveDeleteData(_query, pMode, pDate, pCurrRUB, pRUBSrate, pRUBCrate, pUser, pUploadDate);



        //----------save INR details-----------//
        SqlParameter pCurrINR = new SqlParameter("@curr", SqlDbType.VarChar);
        pCurrINR.Value = txtCurrINR.Text.Trim();

        SqlParameter pINRSrate = new SqlParameter("@SRate", SqlDbType.VarChar);
        pINRSrate.Value = txtINR_SRate.Text.Trim();

        SqlParameter pINRCrate = new SqlParameter("@CRate", SqlDbType.VarChar);
        pINRCrate.Value = txtINR_CRate.Text.Trim();

         pMode.Value = hdnMODE_INRcurr.Value;
        _result = objData.SaveDeleteData(_query, pMode, pDate, pCurrINR, pINRSrate, pINRCrate, pUser, pUploadDate);



        string _script = "";
        if (_result == "added")
        {
            _script = "alert('Record Added.')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            txtDate.Text = "";
            Clear();
        }
        else
        {
            if (_result == "updated")
            {
                _script = "alert('Record Updated.')";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                txtDate.Text = "";
                Clear();
            }
            else
                labelMessage.Text = _result;
        }
    }
    private void Clear()
    {
     //   txtDate.Text = "";

        txtUSD_SRate.Text = "";
        txtUSD_CRate.Text = "";

        txtGBP_SRate.Text = "";
        txtGBP_CRate.Text = "";

        txtEur_SRate.Text = "";
        txtEur_CRate.Text = "";

        txtJPY_SRate.Text = "";
        txtJPY_CRate.Text = "";

        txtCHF_SRate.Text = "";
        txtCHF_CRate.Text = "";

        txtAUD_SRate.Text = "";
        txtAUD_CRate.Text = "";

        txtCAD_SRate.Text = "";
        txtCAD_CRate.Text = "";

        txtSGD_SRate.Text = "";
        txtSGD_CRate.Text = "";

        txtSEK_SRate.Text = "";
        txtSEK_CRate.Text = "";

        txtHKD_SRate.Text = "";
        txtHKD_CRate.Text = "";

        txtSAR_SRate.Text = "";
        txtSAR_CRate.Text = "";

        txtDEM_SRate.Text = "";
        txtDEM_CRate.Text = "";

        txtINR_SRate.Text = "";
        txtINR_CRate.Text = "";


        txtRUB_SRate.Text = "";
        txtRUB_CRate.Text = "";

        hdnMODE_DEMcurr.Value = "add";
        hdnMODE_RUBcurr.Value = "add";
        hdnMODE_INRcurr.Value = "add";

        btnSave.Text = "Save";
        btnDelete.Enabled = false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PC/PC_Main.aspx",true);
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        labelMessage.Text = "";
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";

        //DateTime _date = Convert.ToDateTime(txtDate.Text.Trim(), dateInfo);
        //DateTime _now = Convert.ToDateTime(System.DateTime.Now, dateInfo);

        //if (_date <= _now)
        //{
            TF_DATA objData = new TF_DATA();
            string _query = "TF_PC_GetCurrRatedetails";

            SqlParameter pDate = new SqlParameter("@date", SqlDbType.VarChar);
            pDate.Value = txtDate.Text.Trim();

            DataTable dt = objData.getData(_query, pDate);
            if (dt.Rows.Count > 0)
            {
                txtCurrUSD.Text = dt.Rows[0]["Currency"].ToString();
                txtUSD_SRate.Text = dt.Rows[0]["SpotRate"].ToString();
                txtUSD_CRate.Text = dt.Rows[0]["CrossRatetoUSD"].ToString();

                txtCurrGBP.Text = dt.Rows[1]["Currency"].ToString();
                txtGBP_SRate.Text = dt.Rows[1]["SpotRate"].ToString();
                txtGBP_CRate.Text = dt.Rows[1]["CrossRatetoUSD"].ToString();

                txtCurrEur.Text = dt.Rows[2]["Currency"].ToString();
                txtEur_SRate.Text = dt.Rows[2]["SpotRate"].ToString();
                txtEur_CRate.Text = dt.Rows[2]["CrossRatetoUSD"].ToString();

                txtCurrJPY.Text = dt.Rows[3]["Currency"].ToString();
                txtJPY_SRate.Text = dt.Rows[3]["SpotRate"].ToString();
                txtJPY_CRate.Text = dt.Rows[3]["CrossRatetoUSD"].ToString();

                txtCurrCHF.Text = dt.Rows[4]["Currency"].ToString();
                txtCHF_SRate.Text = dt.Rows[4]["SpotRate"].ToString();
                txtCHF_CRate.Text = dt.Rows[4]["CrossRatetoUSD"].ToString();

                txtCurrAUD.Text = dt.Rows[5]["Currency"].ToString();
                txtAUD_SRate.Text = dt.Rows[5]["SpotRate"].ToString();
                txtAUD_CRate.Text = dt.Rows[5]["CrossRatetoUSD"].ToString();

                txtCurrCAD.Text = dt.Rows[6]["Currency"].ToString();
                txtCAD_SRate.Text = dt.Rows[6]["SpotRate"].ToString();
                txtCAD_CRate.Text = dt.Rows[6]["CrossRatetoUSD"].ToString();

                txtCurrSGD.Text = dt.Rows[7]["Currency"].ToString();
                txtSGD_SRate.Text = dt.Rows[7]["SpotRate"].ToString();
                txtSGD_CRate.Text = dt.Rows[7]["CrossRatetoUSD"].ToString();

                txtCurrSEK.Text = dt.Rows[8]["Currency"].ToString();
                txtSEK_SRate.Text = dt.Rows[8]["SpotRate"].ToString();
                txtSEK_CRate.Text = dt.Rows[8]["CrossRatetoUSD"].ToString();

                txtCurrHKD.Text = dt.Rows[9]["Currency"].ToString();
                txtHKD_SRate.Text = dt.Rows[9]["SpotRate"].ToString();
                txtHKD_CRate.Text = dt.Rows[9]["CrossRatetoUSD"].ToString();

                txtCurrSAR.Text = dt.Rows[10]["Currency"].ToString();
                txtSAR_SRate.Text = dt.Rows[10]["SpotRate"].ToString();
                txtSAR_CRate.Text = dt.Rows[10]["CrossRatetoUSD"].ToString();

                //------------added by Vinay on 13/12/2013-------------------//
                if (dt.Rows.Count > 11)   // ------ for old records.
                {
                    txtCurrDEM.Text = dt.Rows[11]["Currency"].ToString();
                    txtDEM_SRate.Text = dt.Rows[11]["SpotRate"].ToString();
                    txtDEM_CRate.Text = dt.Rows[11]["CrossRatetoUSD"].ToString();
                    hdnMODE_DEMcurr.Value = "edit";
                }
                else if (dt.Rows.Count < 11)
                {
                    hdnMODE_DEMcurr.Value = "add";
                }
                //-----------------------------------------------------------//


                //------------added by Upendra on 09/01/2013-------------------//
                if (dt.Rows.Count > 12)   // ------ for old records.
                {
                    txtCurrRUB.Text = dt.Rows[12]["Currency"].ToString();
                    txtRUB_SRate.Text = dt.Rows[12]["SpotRate"].ToString();
                    txtRUB_CRate.Text = dt.Rows[12]["CrossRatetoUSD"].ToString();
                    hdnMODE_RUBcurr.Value = "edit";

                    txtCurrINR.Text = dt.Rows[13]["Currency"].ToString();
                    txtINR_SRate.Text = dt.Rows[13]["SpotRate"].ToString();
                    txtINR_CRate.Text = dt.Rows[13]["CrossRatetoUSD"].ToString();
                    hdnMODE_INRcurr.Value = "edit";
                }
                else
                {
                    hdnMODE_RUBcurr.Value = "add";
                    hdnMODE_INRcurr.Value = "add";
   
                }
                //-----
                btnSave.Text = "Update";
                btnDelete.Enabled = true;
                txtUSD_SRate.Focus();
            }
            else
            {
                txtUSD_SRate.Focus();
                Clear();
               // txtDate.Text = _date.ToString();
            }
        //}
        //else
        //{
        //    txtDate.Text = "";
        //    Clear();
        //    labelMessage.Font.Bold = true;
        //    labelMessage.Text = "Date cannot be greater than today's date.";
        //}
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PC/PC_Main.aspx", true);
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string _result = "";
        TF_DATA objData = new TF_DATA();
        string _query = "TF_DeleteCrossCurrencyRateMaster";

        SqlParameter pDate = new SqlParameter("@Date", SqlDbType.VarChar);
        pDate.Value = txtDate.Text.Trim();

        _result = objData.SaveDeleteData(_query ,pDate);

        string _script = "";
        if (_result == "deleted")
        {
            _script = "alert('Record Deleted.')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            txtDate.Text = "";
            Clear();
        }
        else
        {     labelMessage.Text = _result;
        }
    }
}