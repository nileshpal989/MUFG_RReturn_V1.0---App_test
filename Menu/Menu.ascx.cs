using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Menu_Menu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime nowDate = System.DateTime.Now;
            if (Session["LoggedUserId"] != null)
            {
                lblUserName.Text = "Welcome, " + Session["userName"].ToString().Trim();
                lblRole.Text = "| Role: " + Session["userRole"].ToString().Trim();
                lblTime.Text = nowDate.ToLongDateString();
                hdnloginid.Value = Session["LoggedUserId"].ToString();
                if (Session["ModuleID"] != null)
                {
                    hdnModuleID.Value = Session["ModuleID"].ToString();
                }
                
            }
        }

        //---------------------------------- ##### Done By Nilesh #####------------------------------------------------------------------
        //#region RReturn Module
        //if (hdnModuleID.Value == "R-Return")
        //{
        //    lblModuleName.Text = "R Return Module";

        //    //---------------- Master ---------------------- 

        //    //mnuPortCodeMaster.Disabled = false;
        //    mnuPortCodeMaster.Visible = true;

        //    //mnuCurrencyMaster.Disabled = false;
        //    mnuCurrencyMaster.Visible = true;

        //    //mnuPurposeCodeMaster.Disabled = false;
        //    mnuPurposeCodeMaster.Visible = true;

        //    //mnuCountryMaster.Disabled = false;
        //    mnuCountryMaster.Visible = true;

        //    //mnuVastroBankMaster.Disabled = false;
        //    mnuVastroBankMaster.Visible = true;

        //    //mnuRet_AuthorisedSignatory.Disabled = false;
        //    mnuRet_AuthorisedSignatory.Visible = true;
        //    //---------------- Transaction ----------------------     

        //    //mnu_RReturn.Disabled = false;
        //    mnu_RReturn.Visible = true;

        //    //mnu_Nostro.Disabled = false;
        //    mnu_Nostro.Visible = true;

        //    //mnu_Vostro.Disabled = false;
        //    mnu_Vostro.Visible = true;

        //    //---------------- File Upload ----------------------

        //    //mFU_RET_CSV.Disabled = false;
        //    mFU_RET_CSV.Visible = true;


        //    //mnu_ConsolidateCSV.Disabled = false;
        //    mnu_ConsolidateCSV.Visible = true;




        //    //mnu_uploadformt.Disabled = false;
        //    mnu_uploadformt.Visible = true;

        //    //mnu_Ret_CBTR_CSV_File_GENERATE.Disabled = false;
        //    //mnu_Ret_CBTR_CSV_File_GENERATE.Visible = true;

        //    //mnu_RRETURN_RET_CSV_File_Creation.Disabled = false;
        //    //mnu_RRETURN_RET_CSV_File_Creation.Visible = true;

        //    //---------------- File Creation ----------------------

        //    //mnu_ret_txtFileCreation.Disabled = false;
        //    mnu_ret_txtFileCreation.Visible = true;

        //    //mnu_ret_DataCsvforCheck.Disabled = false;
        //    mnu_ret_DataCsvforCheck.Visible = true;


        //    //mnuRET_RBITextFileAtHeadOffice.Disabled = false;
        //    mnuRET_RBITextFileAtHeadOffice.Visible = true;


        //    //mnuRET_ExcelFileForImport.Disabled = false;
        //    mnuRET_ExcelFileForImport.Visible = true;

        //    //mnu_RRETURN_RET_CSV_File_Creation.Disabled = false;
        //    mnu_RRETURN_RET_CSV_File_Creation.Visible = true;


        //    //---------------- Reports ----------------------

        //    //mnu_RReturnDataCheckList.Disabled = false;
        //    mnu_RReturnDataCheckList.Visible = true;

        //    //mnu_RReturnDataValidation.Disabled = false;
        //    mnu_RReturnDataValidation.Visible = true;

        //    //mnu_RReturnCoverPage.Disabled = false;
        //    mnu_RReturnCoverPage.Visible = true;

        //    //mnu_NostroReport.Disabled = false;
        //    mnu_NostroReport.Visible = true;

        //    //mnu_RRETURN_DataStatistics.Disabled = false;
        //    mnu_RRETURN_DataStatistics.Visible = true;

        //    //mnu_RReturnVostroReport.Disabled = false;
        //    mnu_RReturnVostroReport.Visible = true;

        //    //mnu_RReturnPurposeTotals.Disabled = false;
        //    mnu_RReturnPurposeTotals.Visible = true;

        //    //mnu_RRETURN_Data_CheckList_For_Import.Disabled = false;
        //    mnu_RRETURN_Data_CheckList_For_Import.Visible = true;


        //    //mnu_ConsolRReturnNostroReport.Disabled = false;
        //    mnu_ConsolRReturnNostroReport.Visible = true;

        //    //mnu_ConsolRreturnVostroReport.Disabled = false;
        //    mnu_ConsolRreturnVostroReport.Visible = true;


        //    //mnu_DataValidationOfInputUploadFile.Disabled = false;
        //    mnu_DataValidationOfInputUploadFile.Visible = true;


        //    //mnu_Schedule1.Disabled = false;
        //    mnu_Schedule1.Visible = true;

        //    //mnu_Schedule2.Disabled = false;
        //    mnu_Schedule2.Visible = true;

        //    //mnu_Schedule3.Disabled = false;
        //    mnu_Schedule3.Visible = true;

        //    //mnu_NonExport.Disabled = false;
        //    //mnu_NonExport.Visible = true;

        //    // ------------------------ Options -----------------------------------------
        //}
        //#endregion
        //---------------------------------- ##### END CHANGES #####------------------------------------------------------------------

        #region Supervisor Role

        if (Session["userRole"] != null && Session["userRole"].ToString().Trim() == "Supervisor")
        {
            if (hdnModuleID.Value == "R-Return")
            {
                mnuRETAuditTrail.Visible = true;
                mnuRETAuditTrail.Disabled = false;

                mnuRETAuditTrail_Login.Visible = true;
                mnuRETAuditTrail_Login.Disabled = false;
            }            
        }
        #endregion

        #region Admin Role

        if (Session["userRole"] != null && Session["userRole"].ToString().Trim() == "Admin")
        {
            mHK_AccessControl.Visible = true;
            mHK_AccessControl.Disabled = false;

            mHK_AccessedMenuList.Visible = true;
            mHK_AccessedMenuList.Disabled = false;
            //disableAllMenu();

            tf_rpt_loginlog.Visible = true;
            tf_rpt_loginlog.Disabled = false;

            AdminAudit.Visible = true;
            AdminAudit.Disabled = false;

            mnu_ErrorLog.Visible = true;
            mnu_ErrorLog.Disabled = false;

            mHk_HouseKeeping.Visible = true;
            mHk_HouseKeeping.Disabled = false;
        }
        
        #endregion
    

    //Access Control List

        string _query = "TF_GetAccessedMenuList";
        TF_DATA objData = new TF_DATA();

        string _user = "";
        if (Session["userName"] != null)
            _user = Session["userName"].ToString().Trim();
        // string ModuleID= hdnModuleID.Value = Session["ModuleID"].ToString();

        SqlParameter pUserName = new SqlParameter("@userName", SqlDbType.VarChar);
        pUserName.Value = _user;

        SqlParameter pModuleID = new SqlParameter("@moduleID", SqlDbType.VarChar);
        if (Session["ModuleID"] == null)
        {
            pModuleID.Value = "CTR";
        }
        else
        {
            pModuleID.Value = hdnModuleID.Value = Session["ModuleID"].ToString();
        }


        DataTable dt = objData.getData(_query, pUserName, pModuleID);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //---------------------------------- ##### Master #####------------------------------------------------------------------
                if (mnuCurrencyMaster.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnuCurrencyMaster.Visible = true;
                    mnuCurrencyMaster.Disabled = false;
                }
                if (mnuPurposeCodeMaster.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnuPurposeCodeMaster.Visible = true;
                    mnuPurposeCodeMaster.Disabled = false;
                }
                if (mnuPortCodeMaster.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnuPortCodeMaster.Visible = true;
                    mnuPortCodeMaster.Disabled = false;
                }
                if (mnuCountryMaster.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnuCountryMaster.Visible = true;
                    mnuCountryMaster.Disabled = false;
                }
                if (mnuRet_AuthorisedSignatory.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnuRet_AuthorisedSignatory.Visible = true;
                    mnuRet_AuthorisedSignatory.Disabled = false;
                }
                if (mnuVastroBankMaster.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnuVastroBankMaster.Visible = true;
                    mnuVastroBankMaster.Disabled = false;
                }
                //---------------------------------- ##### Transactions #####------------------------------------------------------------------
                // -------------------------------R-Return ------------------------------------------//
                if (mnu_RReturn.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnu_RReturn.Visible = true;
                    mnu_RReturn.Disabled = false;
                }
                if (mnu_Vostro.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnu_Vostro.Visible = true;
                    mnu_Vostro.Disabled = false;
                }
                if (mnu_Nostro.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnu_Nostro.Visible = true;
                    mnu_Nostro.Disabled = false;
                }

                //---------------------------------- ##### File Creation #####------------------------------------------------------------------
                // -------------------------------R-Return ------------------------------------------//
                if (mnu_ret_txtFileCreation.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnu_ret_txtFileCreation.Visible = true;
                    mnu_ret_txtFileCreation.Disabled = false;
                }
                if (mnu_ret_DataCsvforCheck.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnu_ret_DataCsvforCheck.Visible = true;
                    mnu_ret_DataCsvforCheck.Disabled = false;
                }
                if (mnuRET_RBITextFileAtHeadOffice.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnuRET_RBITextFileAtHeadOffice.Visible = true;
                    mnuRET_RBITextFileAtHeadOffice.Disabled = false;
                }
                if (mnuRET_ExcelFileForImport.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnuRET_ExcelFileForImport.Visible = true;
                    mnuRET_ExcelFileForImport.Disabled = false;
                }
                if (mnu_RRETURN_RET_CSV_File_Creation.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnu_RRETURN_RET_CSV_File_Creation.Visible = true;
                    mnu_RRETURN_RET_CSV_File_Creation.Disabled = false;
                }

                //---------------------------------- ##### Reports #####------------------------------------------------------------------
                // -------------------------------R-Return ------------------------------------------//
                if (mnu_RReturnDataCheckList.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnu_RReturnDataCheckList.Visible = true;
                    mnu_RReturnDataCheckList.Disabled = false;
                }
                if (mnu_RRETURN_Data_CheckList_For_Import.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnu_RRETURN_Data_CheckList_For_Import.Visible = true;
                    mnu_RRETURN_Data_CheckList_For_Import.Disabled = false;
                }
                if (mnu_RReturnDataValidation.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnu_RReturnDataValidation.Visible = true;
                    mnu_RReturnDataValidation.Disabled = false;
                }
                if (mnu_RRETURN_DataStatistics.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnu_RRETURN_DataStatistics.Visible = true;
                    mnu_RRETURN_DataStatistics.Disabled = false;
                }
                if (mnu_RReturnCoverPage.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnu_RReturnCoverPage.Visible = true;
                    mnu_RReturnCoverPage.Disabled = false;
                }
                if (mnu_NostroReport.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnu_NostroReport.Visible = true;
                    mnu_NostroReport.Disabled = false;
                }
                if (mnu_RReturnVostroReport.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnu_RReturnVostroReport.Visible = true;
                    mnu_RReturnVostroReport.Disabled = false;
                }
                if (mnu_RReturnPurposeTotals.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnu_RReturnPurposeTotals.Visible = true;
                    mnu_RReturnPurposeTotals.Disabled = false;
                }                
                if (mnu_Schedule1.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnu_Schedule1.Visible = true;
                    mnu_Schedule1.Disabled = false;
                }
                if (mnu_Schedule2.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnu_Schedule2.Visible = true;
                    mnu_Schedule2.Disabled = false;
                }
                if (mnu_Schedule3.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnu_Schedule3.Visible = true;
                    mnu_Schedule3.Disabled = false;
                }
                //if (mnu_NonExport.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                //{
                //    mnu_NonExport.Visible = true;
                //    mnu_NonExport.Disabled = false;
                //}
                if (mnu_ConsolRReturnNostroReport.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnu_ConsolRReturnNostroReport.Visible = true;
                    mnu_ConsolRReturnNostroReport.Disabled = false;
                }
                if (mnu_ConsolRreturnVostroReport.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnu_ConsolRreturnVostroReport.Visible = true;
                    mnu_ConsolRreturnVostroReport.Disabled = false;
                }
                if (mnu_DataValidationOfInputUploadFile.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnu_DataValidationOfInputUploadFile.Visible = true;
                    mnu_DataValidationOfInputUploadFile.Disabled = false;
                }
                //------------------------------ADDED BY NILESH--------------------------------------
                if (mnu_coverpagetotal_Excel.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnu_coverpagetotal_Excel.Visible = true;
                    mnu_coverpagetotal_Excel.Disabled = false;
                }
                //---------------------------------- ##### File Upload #####------------------------------------------------------------------
                // -------------------------------R-Return ------------------------------------------//
                if (mFU_RET_CSV.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mFU_RET_CSV.Visible = true;
                    mFU_RET_CSV.Disabled = false;
                }
                if (mnu_ConsolidateCSV.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnu_ConsolidateCSV.Visible = true;
                    mnu_ConsolidateCSV.Disabled = false;
                }
                //---------------------------------- ##### File Upload Format #####------------------------------------------------------------------
                // -------------------------------R-Return ------------------------------------------//
                if (mnu_uploadformt.InnerHtml == dt.Rows[i]["MenuName"].ToString())
                {
                    mnu_uploadformt.Visible = true;
                    mnu_uploadformt.Disabled = false;
                }                
            }
        }

    }

    protected void btnHome_Click(object sender, EventArgs e)
    {
        if (hdnModuleID.Value != null)
        {
            string Module = hdnModuleID.Value;

            if (Module == "RET")
            {
                Response.Redirect("~/RRETURN/RET_Main.aspx", true);

            }
        }
    }

    protected void btnHousekeeping_Click(object sender, EventArgs e)
    {
        if (Session["userRole"] == null || Session["userRole"].ToString().Trim() != "Admin")
            Response.Redirect("~/TF_HouseKeeping.aspx?PageHeader=User Management", true);
        else
            Response.Redirect("~/TF_ViewHouseKeeping.aspx?PageHeader=User Management", true);
    }

    protected string QuotedTimeOutUrl
    {
        get { return '"' + ResolveClientUrl("~/TF_Login.aspx?sessionout=yes&sessionid=" + hdnloginid.Value) + '"'; }
    }
}