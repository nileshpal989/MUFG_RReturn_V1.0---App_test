<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RET_AddEditNostroOpCloBalanceDataEntry.aspx.cs"
    Inherits="RRETURN_RET_AddEditNostroOpCloBalanceDataEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <title>LMCC-Tradefinance System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="../Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="../Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/TAB.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript" src="../Scripts/Validations.js"></script>
    <script language="javascript" type="text/javascript">
        function checkNumeric(e) {
            return AllowDecimalValues(e);
        }
        function calculateOpeningBalance() {
            var txtOBCashBalanceDR = document.getElementById('TabContainerMain_tpOpeningBalance_OBCashBalanceDR');
            if (txtOBCashBalanceDR.value == '')
                txtOBCashBalanceDR.value = 0;
            var txtOBSuspAccountDR = document.getElementById('TabContainerMain_tpOpeningBalance_OBSuspAccountDR');
            if (txtOBSuspAccountDR.value == '')
                txtOBSuspAccountDR.value = 0;
            var txtOBDepositOtherDR = document.getElementById('TabContainerMain_tpOpeningBalance_OBDepositOtherDR');
            if (txtOBDepositOtherDR.value == '')
                txtOBDepositOtherDR.value = 0;
            var txtOBDepositRBIDR = document.getElementById('TabContainerMain_tpOpeningBalance_OBDepositRBIDR');
            if (txtOBDepositRBIDR.value == '')
                txtOBDepositRBIDR.value = 0;
            var txtOBFixedDepositDR = document.getElementById('TabContainerMain_tpOpeningBalance_OBFixedDepositDR');
            if (txtOBFixedDepositDR.value == '')
                txtOBFixedDepositDR.value = 0;
            var txtOBTreasuryBillDR = document.getElementById('TabContainerMain_tpOpeningBalance_OBTreasuryBillDR');
            if (txtOBTreasuryBillDR.value == '')
                txtOBTreasuryBillDR.value = 0;
            var txtOBSecuritiesDR = document.getElementById('TabContainerMain_tpOpeningBalance_OBSecuritiesDR');
            if (txtOBSecuritiesDR.value == '')
                txtOBSecuritiesDR.value = 0;
            var txtOBFCurrencyDR = document.getElementById('TabContainerMain_tpOpeningBalance_OBFCurrencyDR');
            if (txtOBFCurrencyDR.value == '')
                txtOBFCurrencyDR.value = 0;
            var txtOBCashBalanceCR = document.getElementById('TabContainerMain_tpOpeningBalance_OBCashBalanceCR');
            if (txtOBCashBalanceCR.value == '')
                txtOBCashBalanceCR.value = 0;
            var txtOBSuspAccountCR = document.getElementById('TabContainerMain_tpOpeningBalance_OBSuspAccountCR');
            if (txtOBSuspAccountCR.value == '')
                txtOBSuspAccountCR.value = 0;
            var txtOBDepositOtherCR = document.getElementById('TabContainerMain_tpOpeningBalance_OBDepositOtherCR');
            if (txtOBDepositOtherCR.value == '')
                txtOBDepositOtherCR.value = 0;
            var txtOBDepositRBICR = document.getElementById('TabContainerMain_tpOpeningBalance_OBDepositRBICR');
            if (txtOBDepositRBICR.value == '')
                txtOBDepositRBICR.value = 0;
            var txtOBFixedDepositCR = document.getElementById('TabContainerMain_tpOpeningBalance_OBFixedDepositCR');
            if (txtOBFixedDepositCR.value == '')
                txtOBFixedDepositCR.value = 0;
            var txtOBTreasuryBillCR = document.getElementById('TabContainerMain_tpOpeningBalance_OBTreasuryBillCR');
            if (txtOBTreasuryBillCR.value == '')
                txtOBTreasuryBillCR.value = 0;
            var txtOBSecuritiesCR = document.getElementById('TabContainerMain_tpOpeningBalance_OBSecuritiesCR');
            if (txtOBSecuritiesCR.value == '')
                txtOBSecuritiesCR.value = 0;
            var txtOBFCurrencyCR = document.getElementById('TabContainerMain_tpOpeningBalance_OBFCurrencyCR');
            if (txtOBFCurrencyCR.value == '')
                txtOBFCurrencyCR.value = 0;
            var totalDebit = (parseFloat(txtOBCashBalanceDR.value) +
                     parseFloat(txtOBSuspAccountDR.value) +
                     parseFloat(txtOBDepositOtherDR.value) +
                     parseFloat(txtOBDepositRBIDR.value) +
                     parseFloat(txtOBFixedDepositDR.value) +
                     parseFloat(txtOBTreasuryBillDR.value) +
                     parseFloat(txtOBSecuritiesDR.value) +
                     parseFloat(txtOBFCurrencyDR.value));
            var totalCredit = parseFloat(txtOBCashBalanceCR.value) +
                      parseFloat(txtOBSuspAccountCR.value) +
                      parseFloat(txtOBDepositOtherCR.value) +
                      parseFloat(txtOBDepositRBICR.value) +
                      parseFloat(txtOBFixedDepositCR.value) +
                      parseFloat(txtOBTreasuryBillCR.value) +
                      parseFloat(txtOBSecuritiesCR.value) +
                      parseFloat(txtOBFCurrencyCR.value);
            var OpeningBalanceAmount = parseFloat(totalDebit).toFixed(2) - parseFloat(totalCredit).toFixed(2);
            document.getElementById('TabContainerMain_tpOpeningBalance_txtOpeningBalanceAmount').value = OpeningBalanceAmount.toFixed(2); ;
            if (document.getElementById('TabContainerMain_tpOpeningBalance_txtOpeningBalanceAmount').value.indexOf('-') != -1) {
                document.getElementById('TabContainerMain_tpOpeningBalance_txtOBDisabled').value = Math.abs(document.getElementById('TabContainerMain_tpOpeningBalance_txtOpeningBalanceAmount').value).toFixed(2);
                document.getElementById('TabContainerMain_tpOpeningBalance_txtOpeningBalanceAmount').value = 0;
            }
            else
                document.getElementById('TabContainerMain_tpOpeningBalance_txtOBDisabled').value = 0;
            //document.getElementById('TabContainerMain_tpOpeningBalance_txtOpeningBalanceAmount').value = CurrencyFormatted(OpeningBalanceAmount); ;
        }
        function calculateClosingBalance() {
            var txtCBCashBalanceDR = document.getElementById('TabContainerMain_tpClosingBalance_CBCashBalanceDR');
            if (txtCBCashBalanceDR.value == '')
                txtCBCashBalanceDR.value = 0;
            var txtCBSuspAccountDR = document.getElementById('TabContainerMain_tpClosingBalance_CBSuspAccountDR');
            if (txtCBSuspAccountDR.value == '')
                txtCBSuspAccountDR.value = 0;
            var txtCBDepositOtherDR = document.getElementById('TabContainerMain_tpClosingBalance_CBDepositOtherDR');
            if (txtCBDepositOtherDR.value == '')
                txtCBDepositOtherDR.value = 0;
            var txtCBDepositRBIDR = document.getElementById('TabContainerMain_tpClosingBalance_CBDepositRBIDR');
            if (txtCBDepositRBIDR.value == '')
                txtCBDepositRBIDR.value = 0;
            var txtCBFixedDepositDR = document.getElementById('TabContainerMain_tpClosingBalance_CBFixedDepositDR');
            if (txtCBFixedDepositDR.value == '')
                txtCBFixedDepositDR.value = 0;
            var txtCBTreasuryBillDR = document.getElementById('TabContainerMain_tpClosingBalance_CBTreasuryBillsDR');
            if (txtCBTreasuryBillDR.value == '')
                txtCBTreasuryBillDR.value = 0;
            var txtCBSecuritiesDR = document.getElementById('TabContainerMain_tpClosingBalance_CBSecuritiesDR');
            if (txtCBSecuritiesDR.value == '')
                txtCBSecuritiesDR.value = 0;
            var txtCBFCurrencyDR = document.getElementById('TabContainerMain_tpClosingBalance_CBForeignCurrencyDR');
            if (txtCBFCurrencyDR.value == '')
                txtCBFCurrencyDR.value = 0;
            var txtCBCashBalanceCR = document.getElementById('TabContainerMain_tpClosingBalance_CBCashBalanceCR');
            if (txtCBCashBalanceCR.value == '')
                txtCBCashBalanceCR.value = 0;
            var txtCBSuspAccountCR = document.getElementById('TabContainerMain_tpClosingBalance_CBSuspAccountCR');
            if (txtCBSuspAccountCR.value == '')
                txtCBSuspAccountCR.value = 0;
            var txtCBDepositOtherCR = document.getElementById('TabContainerMain_tpClosingBalance_CBDepositOtherCR');
            if (txtCBDepositOtherCR.value == '')
                txtCBDepositOtherCR.value = 0;
            var txtCBDepositRBICR = document.getElementById('TabContainerMain_tpClosingBalance_CBDepositRBICR');
            if (txtCBDepositRBICR.value == '')
                txtCBDepositRBICR.value = 0;
            var txtCBFixedDepositCR = document.getElementById('TabContainerMain_tpClosingBalance_CBFixedDepositCR');
            if (txtCBFixedDepositCR.value == '')
                txtCBFixedDepositCR.value = 0;
            var txtCBTreasuryBillCR = document.getElementById('TabContainerMain_tpClosingBalance_CBTreasuryBillsCR');
            if (txtCBTreasuryBillCR.value == '')
                txtCBTreasuryBillCR.value = 0;
            var txtCBSecuritiesCR = document.getElementById('TabContainerMain_tpClosingBalance_CBSecuritiesCR');
            if (txtCBSecuritiesCR.value == '')
                txtCBSecuritiesCR.value = 0;
            var txtCBFCurrencyCR = document.getElementById('TabContainerMain_tpClosingBalance_CBForeignCurrencyCR');
            if (txtCBFCurrencyCR.value == '')
                txtCBFCurrencyCR.value = 0;
            var totalDebit = parseFloat(txtCBCashBalanceDR.value) +
                     parseFloat(txtCBSuspAccountDR.value) +
                     parseFloat(txtCBDepositOtherDR.value) +
                      parseFloat(txtCBDepositRBIDR.value) +
                      parseFloat(txtCBFixedDepositDR.value) +
                      parseFloat(txtCBTreasuryBillDR.value) +
                      parseFloat(txtCBSecuritiesDR.value) +
                      parseFloat(txtCBFCurrencyDR.value);
            var totalCredit = parseFloat(txtCBCashBalanceCR.value) +
                      parseFloat(txtCBSuspAccountCR.value) +
                      parseFloat(txtCBDepositOtherCR.value) +
                      parseFloat(txtCBDepositRBICR.value) +
                       parseFloat(txtCBFixedDepositCR.value) +
                       parseFloat(txtCBTreasuryBillCR.value) +
                       parseFloat(txtCBSecuritiesCR.value) +
                       parseFloat(txtCBFCurrencyCR.value);
            var ClosingBalanceAmount = parseFloat(totalDebit).toFixed(2) - parseFloat(totalCredit).toFixed(2);
            document.getElementById('TabContainerMain_tpClosingBalance_txtClosingBalanceAmount').value = ClosingBalanceAmount.toFixed(2);
            if (document.getElementById('TabContainerMain_tpClosingBalance_txtClosingBalanceAmount').value.indexOf('-') != -1) {
                document.getElementById('TabContainerMain_tpClosingBalance_txtCBDisabled').value = Math.abs(document.getElementById('TabContainerMain_tpClosingBalance_txtClosingBalanceAmount').value).toFixed(2);
                document.getElementById('TabContainerMain_tpClosingBalance_txtClosingBalanceAmount').value = 0;
            }
            else
                document.getElementById('TabContainerMain_tpClosingBalance_txtCBDisabled').value = 0;
        }
    </script>
    <script language="javascript" type="text/javascript">
        function validate_date(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            //alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46 && charCode != 111 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }
        function validatedelete() {
            if (document.getElementById('hdnUserRole').value == "OIC") {
                var ans = confirm('Record will be Deleted . Do you want to proceed.');
                if (ans) {
                    return true;
                }
                else {
                    document.getElementById('btnDelete').focus();
                    return false;
                }
            }
            else {
                alert('Only OIC can delete all records.');
                document.getElementById('btnDelete').focus();
                return false;
            }
        }
        function validateSave() {
            var ddlBranch = document.getElementById('ddlBranch');
            if (ddlBranch.value == "0") {
                try {
                    alert('Select Branch.');
                    ddlBranch.focus();
                    return false;
                }
                catch (err) {
                    alert('Select Branch.');
                    return false;
                }
            }
            var currencyID = document.getElementById('txtCurrency');
            if (currencyID.value == "") {
                try {
                    alert('Select Currency.');
                    currencyID.focus();
                    return false;
                }
                catch (err) {
                    alert('Select Currency.');
                    return false;
                }
            }
            var fromDate = document.getElementById('txtFromDate');
            if (trimAll(fromDate.value) == '') {
                try {
                    alert('Select From Date.');
                    fromDate.focus();
                    return false;
                }
                catch (err) {
                    alert('Select From Date.');
                    return false;
                }
            }
            var toDate = document.getElementById('txtToDate');
            if (toDate.value == '') {
                try {
                    alert('Select To Date.');
                    toDate.focus();
                    return false;
                }
                catch (err) {
                    alert('Select To Date.');
                    return false;
                }
            }
            return true;
        }
    </script>
    <script language="javascript" type="text/javascript">
        function NextValidDates() {
            var fromdate = document.getElementById('txtFromDate');
            var toDate = document.getElementById('txtToDate');
            var nextfromdate;
            var nexttodate;
            var fromdateyyyy = fromdate.value.split("/")[2];
            var fromdatemm = fromdate.value.split("/")[1];
            var fromdatedd = fromdate.value.split("/")[0];
            if (fromdate.value.substring(0, 2) == '01') {
                nextfromdate = '16/' + fromdatemm + '/' + fromdateyyyy;
                var calDt = new Date(parseFloat(fromdateyyyy), parseFloat(fromdatemm), 0);
                nexttodate = calDt.format("dd/MM/yyyy");
            }
            else {
                if (fromdate.value.substring(0, 2) == '16') {
                    if (fromdatemm == '12') {
                        fromdatemm = '01';
                        fromdateyyyy = parseFloat(fromdateyyyy) + parseFloat('01')
                    }
                    else {
                        fromdatemm = parseFloat(fromdatemm) + parseFloat('01');
                    }
                    nextfromdate = '01/' + fromdatemm + '/' + fromdateyyyy;
                    nexttodate = '15/' + fromdatemm + '/' + fromdateyyyy;
                    var next = new Date(nextfromdate);
                    var mDay = next.getDate();
                    var mMonth = next.getMonth();
                    var mYear = next.getFullYear();
                    if (mDay < 10) { mDay = '0' + mDay }
                    if (mMonth < 10) { mMonth = '0' + mMonth }
                    nextfromdate = '01/' + mDay + '/' + fromdateyyyy;
                    nexttodate = '15/' + mDay + '/' + fromdateyyyy;
                }
            }
            document.getElementById('hdnNextFromdate').value = nextfromdate;
            document.getElementById('hdnNextToDate').value = nexttodate;
            return true;
        }
    </script>
    <script language="javascript" type="text/javascript">
        //        function changeBranchDesc() {
        //            var ddlBranch = document.getElementById('ddlBranch');
        //            var lblAdcodeDesc = document.getElementById('lblAdcodeDesc');
        //            //  alert(ddlBranch.value);
        //            if (ddlBranch.value != "0")
        //                lblAdcodeDesc.innerHTML = ddlBranch.value;
        //            else
        //                lblAdcodeDesc.innerHTML = "";
        //            ddlBranch.focus();
        //            return true;
        //        }
        function ValidDates() {
            var fromdate = document.getElementById('txtFromDate');
            var toDate = document.getElementById('txtToDate');
            var day = fromdate.value.split("/")[0];
            var month = fromdate.value.split("/")[1];
            var year = fromdate.value.split("/")[2];
            var fromdateyyyy = fromdate.value.split("/")[2];
            var fromdatemm = fromdate.value.split("/")[1];
            var fromdatedd = fromdate.value.split("/")[0];
            var dt = new Date(year, month - 1, day);
            var today = new Date();
            if (fromdate.value == '') {
                alert('Select From Date.');
                document.getElementById('txtFromDate').focus();
                return false;
            }
            if ((fromdate.value.substring(0, 2) != '01') && (fromdate.value.substring(0, 2) != '16')) {
                alert('Invalid From Date.');
                document.getElementById('txtFromDate').focus();
                return false;
            }
            else {
                if (dt > today) {
                    alert("Invalid From Date.");
                    document.getElementById('txtFromDate').focus();
                    return false;
                }
                else {
                    if (fromdate.value.substring(0, 2) == '01') {
                        toDate.value = '15/' + fromdatemm + '/' + fromdateyyyy;
                        document.getElementById('txtToDate').focus();
                    }
                    else if (fromdate.value.substring(0, 2) == '16') {
                        var calDt = new Date(parseFloat(fromdateyyyy), parseFloat(fromdatemm), 0);
                        toDate.value = calDt.format("dd/MM/yyyy");
                        document.getElementById('txtToDate').focus();
                    }
                }
            }
            document.getElementById('txtToDate').focus();
            MaintainFocus();
            return true;
        }
        function MaintainFocus() {
            document.getElementById('txtToDate').focus();
        }
        function ValidDates1() {
            document.getElementById('btnfillgrid').click();
            return true;
        }
        function OpenCurrencyList(hNo) {
            open_popup('TF_RRETURN_CurrencyHelp_Nostro.aspx?PageHeader=Currency Help&pc=' + hNo, 450, 400, 'CurrencyList');
            return false;
        }
        function selectCurrency(selectedID, SelectedName, hNo) {
            var id = selectedID;
            var Name = SelectedName;

            document.getElementById('txtCurrency').value = selectedID
            document.getElementById('hdnCurrencyHelpNo').value = hNo;
            document.getElementById('hdnCurId').value = id;
            document.getElementById('hdnCurName').value = Name;
            document.getElementById('btnCurr').click();
            //__doPostBack('txtCurrency', '');
        }
        function changeCurrDesc() {
            var txtCurrency = document.getElementById('txtCurrency');
            var txtCurrencyDescription = document.getElementById('txtCurrencyDescription');
            if (txtCurrency.value != "")
                txtCurrencyDescription.innerHTML = txtCurrency.value;
            else
                txtCurrencyDescription.innerHTML = "";
            document.getElementById('btnCurrfillgrid').click();
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:scriptmanager id="ScriptManagerMain" runat="server">
    </asp:scriptmanager>
    <script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
    <asp:updateprogress id="updateProgress" runat="server" dynamiclayout="true">
        <progresstemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </progresstemplate>
    </asp:updateprogress>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
        </center>
        <center>
            <asp:updatepanel id="UpdatePanelMain" runat="server">
                <triggers>
                    <asp:PostBackTrigger ControlID="btnBack" />
                    <asp:PostBackTrigger ControlID="btnCancel" />
                </triggers>
                <contenttemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%; font-weight: bold;" valign="bottom">
                                <span class="pageLabel " style="font-weight: bold;">Nostro Opening/Closing Balance Data
                                    Entry</span>
                            </td>
                            <td align="right" style="width: 50%" valign="top">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" OnClick="btnBack_Click"
                                    ToolTip="Back" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                                <input type="hidden" id="hdnBranchCode" runat="server" />
                                <input type="hidden" id="hdnUserRole" runat="server" />
                                <asp:Button ID="btnfillgrid" Style="display: none;" runat="server" OnClick="btnfillgrid_Click" />
                                <input type="hidden" id="hdnCurrencyHelpNo" runat="server" />
                                <asp:Button ID="btnCurrfillgrid" Style="display: none;" runat="server" OnClick="btnCurrencyfillgrid_Click" />
                                <%--<input type="hidden" id="hdnCurId" runat="server" />--%>
                                <%-- <asp:Button ID="btnCurr" Style="display: none;" runat="server" OnClick="btnCurr_Click" />--%>
                                <input type="hidden" id="hdnNextFromdate" runat="server" />
                                <input type="hidden" id="hdnNextToDate" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="2">
                                <table cellspacing="0" cellpadding="0" border="0" width="650px" style="line-height: 150%">
                                    <tr>
                                        <td align="right">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            &nbsp;
                                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" runat="server" TabIndex="1"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            &nbsp; <span class="elementLabel">AD Code :</span>&nbsp;&nbsp;
                                            <asp:Label ID="lblAdcodeDesc" runat="server" Style="font-size: small" Width="50px"></asp:Label>
                                            &nbsp;
                                            <asp:Label ID="lblBankname" runat="server" Style="font-size: small" CssClass="elementLabel"
                                                Width="200px"></asp:Label>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Currency :</span>
                                        </td>
                                        <td align="left" style="width: 300px">
                                            &nbsp;
                                            <asp:TextBox ID="txtCurrency" CssClass="textBox" runat="server" MaxLength="3" TabIndex="2" Width="30"
                                                AutoPostBack="true" OnTextChanged="txtCurrency_TextChanged" style="text-transform:uppercase"></asp:TextBox>
                                            <asp:Button ID="btncurrList" runat="server" CssClass="btnHelp_enabled" />
                                            <asp:Label ID="txtCurrencyDescription" runat="server" CssClass="elementLabel" Width="250"></asp:Label>
                                            <asp:Label ID="lblCurrency" runat="server" CssClass="elementLabel"></asp:Label>
                                            <input type="hidden" id="hdnCurId" runat="server" />
                                            <input type="hidden" id="hdnCurName" runat="server" />
                                            <asp:Button ID="btnCurr" Style="display: none;" runat="server" OnClick="btnCurr_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="mandatoryField">*</span><span class="elementLabel">From Date :</span>
                                        </td>
                                        <td align="left" style="width: 300px">
                                            &nbsp;
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                                Width="70" TabIndex="3" OnTextChanged="txtFromDate_TextChanged"></asp:TextBox>
                                            <asp:Button ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled"
                                                TabIndex="-1" />
                                            <ajaxToolkit:MaskedEditExtender ID="mdfdate" Mask="99/99/9999" MaskType="Date" runat="server"
                                                TargetControlID="txtFromDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                                ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="btncalendar_FromDate">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdfdate"
                                                ValidationGroup="dtVal" ControlToValidate="txtFromDate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                            </ajaxToolkit:MaskedEditValidator>
                                            &nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To Date :</span>
                                            &nbsp;
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" Width="70"
                                                TabIndex="4" AutoPostBack="true" OnTextChanged="txtToDate_TextChanged"></asp:TextBox>
                                            <asp:Button ID="btncalendar_ToDate" runat="server" CssClass="btncalendar_enabled" />
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                                runat="server" TargetControlID="txtToDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                                ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtToDate" PopupButtonID="btncalendar_ToDate">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                ValidationGroup="dtVal" ControlToValidate="txtToDate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                            </ajaxToolkit:MaskedEditValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 98%; border: 1px solid #49A3FF" valign="top" colspan="2">
                                            <ajaxToolkit:TabContainer ID="TabContainerMain" runat="server" ActiveTabIndex="0"
                                                CssClass="ajax__tab_xp-theme" TabIndex="-1" AutoPostBack="true">
                                                <ajaxToolkit:TabPanel ID="tpOpeningBalance" runat="server" HeaderText="OPENING BALANCE"
                                                    Font-Bold="true" ForeColor="White">
                                                    <ContentTemplate>
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                                <td align="left" style="width: 300px">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left" style="width: 100px">
                                                                    <span class="elementLabel">Debit Amount</span>
                                                                </td>
                                                                <td align="left" style="width: 200px">
                                                                    <span class="elementLabel">Credit Amount</span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp; A. Cash Balance</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="OBCashBalanceDR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        TabIndex="5" Width="80px" Style="text-align: right; background-color: #99FFCC"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="OBCashBalanceCR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="6"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp; B. Suspence Account Balance</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="OBSuspAccountDR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        TabIndex="7" Width="80px" Style="text-align: right; background-color: #99FFCC"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="OBSuspAccountCR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="8"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp; C. Deposit with other AD's in India</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="OBDepositOtherDR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="9"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="OBDepositOtherCR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="10"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp; D. Deposit with RBI</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="OBDepositRBIDR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="11"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="OBDepositRBICR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="12"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp; E. Fixed Deposit</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="OBFixedDepositDR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="13"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="OBFixedDepositCR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="14"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp; F. Treasury Bills/Treasury Deposit</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="OBTreasuryBillDR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="15"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="OBTreasuryBillCR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="16"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp; G. Securities and Shares</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="OBSecuritiesDR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="17"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="OBSecuritiesCR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="18"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp; H. Foreign Currency Loan Outstanding</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="OBFCurrencyDR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="19"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="OBFCurrencyCR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="20"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel" style="font-weight: bold">Total A to H</span>&nbsp;&nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtOpeningBalanceAmount" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; font-weight: bold; background-color: #99FFCC"
                                                                        TabIndex="21"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtOBDisabled" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; font-weight: bold; background-color: #99FFCC"
                                                                        TabIndex="22"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel ID="tpClosingBalance" runat="server" HeaderText="CLOSING BALANCE"
                                                    Font-Bold="true" ForeColor="Lime">
                                                    <ContentTemplate>
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                                <td align="left" style="width: 300px">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left" style="width: 100px">
                                                                    <span class="elementLabel">Debit Amount</span>
                                                                </td>
                                                                <td align="left" style="width: 200px">
                                                                    <span class="elementLabel">Credit Amount</span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp; A. Cash Balance</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="CBCashBalanceDR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="23"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="CBCashBalanceCR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="24"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp; B. Suspence Account Balance</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="CBSuspAccountDR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="25"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="CBSuspAccountCR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="26"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp; C. Deposit with other AD's in India</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="CBDepositOtherDR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="27"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="CBDepositOtherCR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="28"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp; D. Deposit with RBI</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="CBDepositRBIDR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="29"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="CBDepositRBICR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="30"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp; E. Fixed Deposit</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="CBFixedDepositDR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="31"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="CBFixedDepositCR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="32"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp; F. Treasury Bills/Treasury Deposit</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="CBTreasuryBillsDR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="33"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="CBTreasuryBillsCR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="34"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp; G. Securities and Shares</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="CBSecuritiesDR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="35"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="CBSecuritiesCR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="36"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp; H. Foreign Currency Loan Outstanding</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="CBForeignCurrencyDR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="37"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="CBForeignCurrencyCR" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="38"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel" style="font-weight: bold">Total A to H</span>&nbsp;&nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtClosingBalanceAmount" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; font-weight: bold; background-color: #99FFCC"
                                                                        TabIndex="39"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtCBDisabled" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; font-weight: bold; background-color: #99FFCC"
                                                                        TabIndex="40"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel ID="tpOthers" runat="server" HeaderText="OTHERS" Font-Bold="true"
                                                    ForeColor="Lime">
                                                    <ContentTemplate>
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                            <tr>
                                                                <td align="left" style="width: 300px">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left" style="width: 100px">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left" style="width: 200px">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp; A. EEFC Accounts</span>&nbsp;&nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtEEFCAccounts" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="41"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp; B. EFC Accounts</span>&nbsp;&nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtEFCAccounts" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="42"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp; C. RFC Accounts</span>&nbsp;&nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtRFCAccounts" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="43"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp; D. ESCROW F.C Accounts</span>&nbsp;&nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtEscrowFCAccounts" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="44"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp; E. FCNR (B) Accounts</span>&nbsp;&nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtFCNRBAccounts" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="45"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp; F. Other F.C Accounts</span>&nbsp;&nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtOtherFCAccounts" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="80px" Style="text-align: right; background-color: #99FFCC" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                            </ajaxToolkit:TabContainer>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 160px">
                                        </td>
                                        <td align="left" style="width: 640px; padding-top: 10px; padding-bottom: 10px">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                OnClick="btnSave_Click" ToolTip="Save" TabIndex="47" />
                                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDefault"
                                                OnClick="btnDelete_Click" ToolTip="Delete" TabIndex="48" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                OnClick="btnCancel_Click" ToolTip="Cancel" TabIndex="49" />
                                            <%-- These section to handle log  --%>
                                            <input type="hidden" runat="server" id="hdnDebitOpnBal" />
                                            <input type="hidden" runat="server" id="hdnCreditOpnBal" />
                                            <input type="hidden" runat="server" id="hdnDebitClBal" />
                                            <input type="hidden" runat="server" id="hdnCreditClBal" />
                                            <input type="hidden" runat="server" id="hdnEEFCAC" />
                                            <input type="hidden" runat="server" id="hdnEFCAC" />
                                            <input type="hidden" runat="server" id="hdnRFCAC" />
                                            <input type="hidden" runat="server" id="hdnESCROWAC" />
                                            <input type="hidden" runat="server" id="hdnFCNRAC" />
                                            <input type="hidden" runat="server" id="hdnOHTRFCAC" />
                                            <%-- End of These section to handle log  --%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </contenttemplate>
            </asp:updatepanel>
        </center>
    </div>
    </form>
</body>
</html>
