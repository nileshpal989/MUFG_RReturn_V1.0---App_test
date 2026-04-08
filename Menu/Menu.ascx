<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="Menu_Menu" %>
<!DOCTYPE HTML>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=charset=UTF-8" />
    <title>LMCC Trade Finance System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script type="text/javascript" language="JavaScript">
		var _timeLeft, _countDownTimer, _popupTimer;
		function startSession() {

			_timeLeft = '<%= Session.Timeout %>';
			//          alert(_timeLeft);
			updateCountDown();
		}

		function updateCountDown() {
			if (_timeLeft > 2) {
				_timeLeft--;
				_countDownTimer = window.setTimeout(updateCountDown, 60000);

			} else {
				showPopup();
			}
		}
		function stopTimers() {
			window.clearTimeout(_popupTimer);
			window.clearTimeout(_countDownTimer);
			document.getElementById("Menu1_CountDownHolder").innerText = "";
		}
		function updateCountDownTimer() {

			var min = Math.floor(_timeLeft / 60);

			var sec = _timeLeft % 60;
			if (sec < 10)
				sec = "0" + sec;

			document.getElementById("Menu1_CountDownHolder").innerText = "This session will expire in " + min + ":" + sec + " mins";

			if (_timeLeft > 0) {
				_timeLeft--;
				_countDownTimer = window.setTimeout(updateCountDownTimer, 1000);
				this.focus();
			} else {
				var loginID = document.getElementById("Menu1_hdnloginid").value;
				// document.location = '../TF_Login.aspx?sessionout=yes&sessionid=' + loginID;
			   document.location = <%= QuotedTimeOutUrl %>;

			}
		}
		function showPopup() {

			_timeLeft = 120;

			updateCountDownTimer();

		}
		function sendKeepAlive() {
			stopTimers();
		}

		function checkenter(evnt) {
			var charCode = (evnt.which) ? evnt.which : event.keyCode;
			 //alert(charCode);
			if (charCode == 13)
			{
			return false;
			}
			else
				return true;
		}

    </script>
    <script type="text/javascript" language="JavaScript">
		function CallHouseKeeping() {
		 <%= Page.ClientScript.GetPostBackEventReference(this.btnHousekeeping, "") %>
		}
		  function checkenter(evnt) {
			var charCode = (evnt.which) ? evnt.which : event.keyCode;
			 //alert(charCode);
			if (charCode == 13)
			{
			return false;
			}
			else
				return true;
		}
    </script>
    <script type="text/javascript">

        function openFormatPopup() {

            document.getElementById("formatPopup").style.display = "block";
        }

        function closeFormatPopup() {

            document.getElementById("formatPopup").style.display = "none";
        }

        function downloadFormat() {

            if (document.getElementById("rbGiftCity").checked) {

//                alert("Gift City selected");
                window.location.href = '../File_Format/RReturnUploadFormat_GiftCity.xlsx';
            }
            else if (document.getElementById("rbOtherBranch").checked) {

                //alert("Other Branch selected");
                 window.location.href = '../File_Format/RReturnUploadFormat.xls';
            }
            else {

                alert("Please select branch");
                return;
            }

            closeFormatPopup();
        }

</script>




</head>
<body onload="startSession();" onclick="stopTimers();startSession();" onkeyup="stopTimers();startSession();"
    onkeypress="return checkenter(event);">
    <div id="body">
        <div id="container3-header">
            <div id="container2-header">
                <div id="logo">
                </div>
                <div id="container-header-module">
                    <div id="header-info">
                        <asp:Label ID="lblUserName" runat="server" />
                        <asp:Label ID="lblRole" runat="server" />
                    </div>
                    <div id="header-Date">
                        <asp:Label ID="lblTime" runat="server" CssClass="elementLabel" />
                        <input type="hidden" id="hdnloginid" runat="server" />
                        <input type="hidden" id="hdnModuleID" runat="server" />
                    </div>
                    <div id="module-info">
                        <asp:Label ID="lblModuleName" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <asp:Label ID="CountDownHolder" runat="server" Font-Bold="true" Font-Size="Large"
            ForeColor="#ff0000" ToolTip="Click to Renew your Session" CssClass="mandatoryField" />
        <nav>
			<ul class="menu">
				<li>
					<a href="~/TF_ModuleSelection.aspx?type=0" id="hHome" runat="server">Home</a>
				</li>
				<li>
					<a href="#">Masters</a>
					<ul>                    						
						<li><a href="../TF_ViewCurrencyMaster.aspx?PageHeader=Currency Master" id="mnuCurrencyMaster" runat="server" disabled="disabled" visible="false">Currency Master</a></li>
						<li><a href="../TF_ViewPurposeCode.aspx?PageHeader=Purpose Code Master" id="mnuPurposeCodeMaster" runat="server" disabled="disabled" visible="false">Purpose Code Master</a></li>						
						<li><a href="../TF_ViewPortMaster.aspx?PageHeader=Port Code Master" id="mnuPortCodeMaster" runat="server" disabled="disabled" visible="false">Port Code Master</a></li>
						<li><a href="~/TF_ViewCountryMaster.aspx?PageHeader=Country Master" id="mnuCountryMaster" disabled="disabled" runat="server" visible="false">Country Master</a></li>
						<li><a href="~/RRETURN/Ret_ViewAuthSignatory.aspx?PageHeader=Authorised Signatory" id="mnuRet_AuthorisedSignatory"  runat="server" disabled="disabled" visible="false">Authorised Signatory</a></li>						
						<li><a href="~/RRETURN/TF_ViewVastroBankMaster.aspx?PageHeader=Vostro Bank Master"  id="mnuVastroBankMaster" runat="server" disabled="disabled" visible="false">Vostro Bank Master</a></li>												
					</ul>
				</li>
				<li>
					<a href="#">Transactions</a>
					<ul>
						<li><a href="~/RRETURN/RET_ViewReturnData.aspx?PageHeader=R Return Data Entry" id="mnu_RReturn" runat="server"  disabled="disabled" visible="false">R Return Data Entry</a></li>						
						<li><a href="~/RRETURN/RET_AddEditNostroOpCloBalanceDataEntry.aspx?PageHeader=Nostro Opening/Closing balance Data Entry" id="mnu_Nostro" runat="server"  disabled="disabled" visible="false">Nostro Opening/Closing balance Data Entry</a></li>						
						<li><a href="~/RRETURN/RET_AddEditVostroOpCloBalanceDataEntry.aspx?PageHeader=Vostro Opening/Closing balance Data Entry" id="mnu_Vostro" runat="server"  disabled="disabled" visible="false">Vostro Opening/Closing balance Data Entry</a></li>						
					</ul>
				</li>
				<li>
					<a href="#">File Creation</a>
					<ul>
						<li><a href="~/RRETURN/Ret_TxtFileCReation.aspx?PageHeader=RBI Text File [QE,BOP6]" id="mnu_ret_txtFileCreation" runat="server" disabled="disabled" visible="false">RBI Text File [QE,BOP6]</a></li>						
						<li><a href="~/RRETURN/Ret_DataCSV.aspx?PageHeader=Data File [CSV] For Checking" id="mnu_ret_DataCsvforCheck" runat="server" disabled="disabled" visible="false">File Extraction For HO</a></li>						
						<li><a href="~/RRETURN/RET_RBITextFileAtHeadOffice.aspx?PageHeader=RBI Text File Creation At Head Office" id="mnuRET_RBITextFileAtHeadOffice" runat="server" disabled="disabled" visible="false">RBI Text File Creation At Head Office</a></li>						
						<li><a href="~/RRETURN/RET_ExcelFileForImport.aspx?PageHeader=Excel File Creation For Import" id="mnuRET_ExcelFileForImport" runat="server" disabled="disabled" visible="false">Excel File Creation For Import</a></li>
						<li><a href="~/RRETURN/RET_CSV_File_Creation.aspx?PageHeader=CSV File Creation For Data Check" id="mnu_RRETURN_RET_CSV_File_Creation" runat="server"  visible="false" disabled="disabled">CSV File Creation For Data Check</a></li>						
					</ul>
				</li>
				<li>
					<a href="#">Reports</a>
					<ul>
						<li><a href="~/Reports/RRETURNReports/rptRRETURN_Datachecklist.aspx?PageHeader=R Return Data CheckList" id="mnu_RReturnDataCheckList" runat="server" disabled="disabled" visible="false">Data CheckList</a></li>						
						<li><a href="~/Reports/RRETURNReports/rptRRETURN_Data_CheckList_For_Import.aspx?PageHeader=R Return Data CheckList For Import" id="mnu_RRETURN_Data_CheckList_For_Import" runat="server" disabled="disabled" visible="false">Data CheckList For Import</a></li>						
						<li><a href="~/Reports/RRETURNReports/rptRRETURN_Purpose_Control_Totals.aspx?PageHeader=Data Validation" id="mnu_RReturnDataValidation" runat="server" disabled="disabled" visible="false">Data Validation</a></li>						
						<li><a href="~/Reports/RRETURNReports/rpt_RRETURN_DataStatistics.aspx?PageHeader=Data Statistics" id="mnu_RRETURN_DataStatistics" runat="server" disabled="disabled" visible="false">Data Statistics</a></li>						
						<li><a href="~/Reports/RRETURNReports/rptRRETURN_Purpose_Control_Totals.aspx?PageHeader=R RETURN Cover Page Total" id="mnu_RReturnCoverPage" runat="server" disabled="disabled" visible="false">R RETURN Cover Page Total</a></li>						
						<li><a href="~/Reports/RRETURNReports/rptRRETURN_NostroReport.aspx?PageHeader=RRETURN Report to RBI(Nostro)" id="mnu_NostroReport" runat="server" disabled="disabled" visible="false">R RETURN Report to RBI(Nostro)</a></li>						       
						<li><a href="~/Reports/RRETURNReports/rptRRETURN_VostroReport.aspx?PageHeader=R RETURN Report to RBI(Vostro)" id="mnu_RReturnVostroReport" runat="server" disabled="disabled" visible="false">R RETURN Report to RBI(Vostro)</a></li>						
						<li><a href="~/Reports/RRETURNReports/rptRRETURN_Purpose_Control_Totals.aspx?PageHeader=Purpose Code Wise Control Totals" id="mnu_RReturnPurposeTotals" runat="server" disabled="disabled" visible="false">Purpose Code Wise Control Totals</a></li>						      
						<li><a href="~/Reports/RRETURNReports/rptRRETURN_Schedule.aspx?PageHeader=Schedule 1" id="mnu_Schedule1" runat="server" disabled="disabled" visible="false">Schedule 1</a></li>
					    <li><a href="~/Reports/RRETURNReports/rptRRETURN_Schedule.aspx?PageHeader=Schedule 2" id="mnu_Schedule2" runat="server" disabled="disabled" visible="false">Schedule 2</a></li>
					    <li><a href="~/Reports/RRETURNReports/rptRRETURN_Schedule.aspx?PageHeader=Schedule 3/4/5/6" id="mnu_Schedule3" runat="server" disabled="disabled" visible="false">Schedule 3/4/5/6</a></li>
						<%--<li><a href="~/Reports/RRETURNReports/rptRRETURN_Schedule.aspx?PageHeader=Supplementary Statement of Purchases" id="mnu_NonExport" runat="server" disabled="disabled" visible="false">Supplementary Statement of Purchases</a></li>--%>						
						<li><a href="~/Reports/RRETURNReports/rptRRETURN_NostroReport.aspx?PageHeader=Consolidated R RETURN Report to RBI(Nostro)" id="mnu_ConsolRReturnNostroReport" runat="server" disabled="disabled" visible="false">Consolidated R RETURN Report to RBI(Nostro)</a></li>						       
						<li><a href="~/Reports/RRETURNReports/rptRRETURN_VostroReport.aspx?PageHeader=Consolidated R RETURN Report to RBI(Vostro)" id="mnu_ConsolRreturnVostroReport" runat="server" disabled="disabled" visible="false">Consolidated R RETURN Report to RBI(Vostro)</a></li>						
						<li><a href="~/Reports/RRETURNReports/rptRRETURN_ValidationInputInvoiceFile.aspx?PageHeader=Data Validation Of Input Upload File" id="mnu_DataValidationOfInputUploadFile" runat="server" disabled="disabled" visible="false">Validation Of Invoice Input File</a></li>						                 
						<li><a href="~/Reports/ExcelReport/TF_RET_COverPAGETOTAL.aspx" id="mnu_coverpagetotal_Excel" runat="server" disabled="disabled" visible="false">Cover Page Total - Excel</a></li>						                 
					</ul>
				</li>
				<li>
					<a href="#">File Upload</a>
					<ul>
						<li><a href="~/RRETURN/Transaction_FileUpload_New.aspx?PageHeader=Excel Input Data File Upload at Branch" id="mFU_RET_CSV" runat="server" disabled="disabled" visible="false">Excel Input Data File Upload at Branch</a></li>						
						<li><a href="~/RRETURN/Ret_Consolidate_CSV_Data.aspx?PageHeader=Consolidate Branch CSV File At Head Office" id="mnu_ConsolidateCSV" runat="server"  visible="false" disabled="disabled">Consolidate Branch CSV File At Head Office</a></li>												
					</ul>
				</li>
				<li>
					<a href="#">File Format</a>
					<ul>
                    <li><a href="javascript:void(0);" id="mnu_uploadformt" runat="server"  onclick="openFormatPopup(); return false;">
                         Excel Input Data File Upload Format </a>
                    </li>
						<%--<li><a href="~/File_Format/RReturnUploadFormat.xls" id="mnu_uploadformt" runat="server"  visible="false" disabled="disabled">Excel Input Data File Upload Format</a></li>						--%>
					</ul>
				</li>
				<li >
					<a href="#">Audit Trail</a>
					<ul>
						<li><a href="~/Reports/TF_AuditTrail.aspx?PageHeader=Audit Trail - Transactions&ModuleType=RET" id="mnuRETAuditTrail" runat="server" disabled="disabled" visible="true">Audit Trail - Transactions</a></li>						
						<li><a href="~/TF_AuditTrail_LoginLogOut.aspx?PageHeader=Audit Trail - Login Details&ModuleType=RET" id="mnuRETAuditTrail_Login" runat="server" disabled="disabled" visible="false">Audit Trail - Login Details</a></li>												
					</ul>
				</li>
				<li>
					<a href="#">Admin</a>
					<ul>
						<li><a id="mHk_HouseKeeping" runat="server" disabled="disabled" visible="false" onclick="CallHouseKeeping()" >User Management</a></li>	
                        <li><a href="~/TF_AccessControl.aspx?PageHeader=Access Control&uname=---Select---" id="mHK_AccessControl" runat="server" disabled="disabled" visible="false">Access Control</a></li>	
                        <li><a href="~/TF_rptAccessControl.aspx?PageHeader=Accessed Menu List" id="mHK_AccessedMenuList" runat="server" disabled="disabled" visible="false">Accessed Menu List</a></li>				
						<li><a href="~/TF_rpt_LoginLog.aspx?PageHeader=User Login Activity Details" id="tf_rpt_loginlog" runat="server" disabled="disabled" visible="false">User Login Activity Details</a></li>
                         <li><a href="~/TF_AdminAuditTrail.aspx?PageHeader=Admin AuditTrail" id="AdminAudit" runat="server" disabled="disabled" visible="false">Admin AuditTrail</a></li>
                         <li><a href="~/RRETURN/TF_RET_ErrorLog_TxtFileCreation.aspx?PageHeader=Error Log" id="mnu_ErrorLog" runat="server" disabled="disabled" visible="false">Error Log</a></li>
                         <li><a href="~/Reports/RRETURNReports/TF_RReturn_FU_History.aspx?PageHeader=File Upload History" id="mnu_FUHistory" runat="server" disabled="disabled" visible="false">File Upload History</a></li>
					</ul>
				</li>
				<li>
					<a href="~/TF_Log_Out.aspx?PageHeader=Logout" id="LogOut" runat="server">Logout</a>
				</li>
			</ul>
		</nav>
       <div id="formatPopup"
     style="display:none;
     position:fixed;
     top:30%;
     left:40%;
     background-color:white;
     padding:20px;
     border:2px solid black;
     box-shadow:0px 0px 15px gray;
     z-index:9999;">

    <h3>Select Branch</h3>

    <input type="radio" name="branchType" id="rbGiftCity" />
    Gift City

    <br /><br />

    <input type="radio" name="branchType" id="rbOtherBranch" />
    Other Branch

    <br /><br />

    <button type="button" onclick="downloadFormat()">Download</button>

    <button type="button" onclick="closeFormatPopup()">Cancel</button>

</div>

    </div>
    <asp:Button ID="btnHousekeeping" Style="display: none;" runat="server" OnClick="btnHousekeeping_Click" />
</body>
</html>
