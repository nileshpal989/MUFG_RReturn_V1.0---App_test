<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RET_AddEditVostroOpCloBalanceDataEntry.aspx.cs"
    Inherits="RRETURN_RET_AddEditVostroOpCloBalanceDataEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    </script>
    <script language="javascript" type="text/javascript">
        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            // alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }

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
            var countryID = document.getElementById('txtCountryAC');
            if (countryID.value == '') {
                try {
                    alert('Select Country of A/C Holder .');
                    countryID.focus();
                    return false;
                }
                catch (err) {
                    alert('Select Country of A/C Holder .');
                    return false;
                }
            }
            var bankID = document.getElementById('txtBankCode');
            if (bankID.value == '') {
                try {
                    alert('Select Bank Code.');
                    bankID.focus();
                    return false;
                }
                catch (err) {
                    alert('Select Bank Code.');
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
            var txtOP_D = document.getElementById('txtOP_D');
            var txtOP_C = document.getElementById('txtOP_C');
            var txtCL_D = document.getElementById('txtCL_D');
            var txtCL_C = document.getElementById('txtCL_C');

            if (txtOP_D.value == '' && txtOP_C.value == '') {
                var r = confirm("Opening Balance Is Blank Do You Want To Save?");
                if (r == true) {

                } else {
                    return false;
                }
            }
            if (txtCL_D.value == '' && txtCL_C.value == '') {
                var r = confirm("Closing Balance Is Blank Do You Want To Save?");
                if (r == true) {

                } else {
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
                        fromdateyyyy = parseFloat(fromdateyyyy) + parseInt('01')
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
        function validate_date(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            //alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46 && charCode != 111 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
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
        function OpenCountryListAc(hNo) {
            open_popup('TF_RET_ACCountryLookUp.aspx?PageHeader=Ac Country Lookup&hNo=' + hNo, 450, 400, 'CountryList');
            //open_popup('Test.aspx?hNo=' + hNo, 400, 450, 'CountryList');
            return false;
        }
        function selectCountryAc(selectedID, hNo) {
            var id = selectedID;
            document.getElementById('hdnCountryHelpNoAc').value = hNo;
            document.getElementById('hdnCountryAc').value = id;
            document.getElementById('btnCountryAc').click();
        }
        function changeCountryDescAc() {
            var txtCountryAC = document.getElementById('txtCountryAC');
            var txtCountryACHolder = document.getElementById('txtCountryACHolder');
            if (txtCountryAC.value != '') {
                txtCountryACHolder.innerHTML = txtCountryAC.value;
            }
            else {
                txtCountryACHolder.innerHTML = '';
            }
            return true;
        }
        function OpenBankCodeList(hNo) {
            var bankcode = document.getElementById('txtCountryAC');
            var strUser = bankcode.value;
            open_popup('RET_helpBankCode.aspx?PageHeader=Bank Code Help&hNo=' + hNo + '&bankcode=' + strUser, 450, 400, 'CountryList');
            //open_popup('Test.aspx?hNo=' + hNo + '&bankcode=' + strUser, 450, 450, 'CountryList');
            return false;
        }
        function selectBankCode(selectedID, hNo) {
            var id = selectedID;
            document.getElementById('hdnBankCodeHelpNo').value = hNo;
            document.getElementById('txtBankCode').value = id;
            __doPostBack('txtBankCode');
            //document.getElementById('hdnBankCode').value = id;
            //document.getElementById('btnBankCode1').click();
        }
        function changeBankCodeDesc() {
            var txtBankCode = document.getElementById('txtBankCode');
            var lblBankCode = document.getElementById('lblBankCode');
            if (txtBankCode.value != "0")
                lblBankCode.innerHTML = txtBankCode.value;
            else
                lblBankCode.innerHTML = "";
            return true;
        }

        function fillforOB() {
            document.getElementById('btnfillforOB').click();
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
        </center>
        <center>
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnBack" />
                    <asp:PostBackTrigger ControlID="btnCancel" />
                    <asp:PostBackTrigger ControlID="btnDelete" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel" style="font-weight: bold;">Vostro Opening/Closing Balance Data
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
                                <input type="hidden" id="hdnBankCodeHelpNo" runat="server" />
                                <input type="hidden" id="hdnBankCode" runat="server" />
                                <asp:Button ID="btnBankCode1" Style="display: none;" runat="server" OnClick="btnBankCode_Click" /><input
                                    type="hidden" id="hdnCountryofDestination" runat="server" />
                                <input type="hidden" id="hdnCountryHelpNoAc" runat="server" />
                                <input type="hidden" id="hdnCountryAc" runat="server" />
                                <asp:Button ID="btnCountryAc" Style="display: none;" runat="server" OnClick="btnCountryAc_Click" />
                                <asp:Button ID="btnfillforOB" Style="display: none;" runat="server" OnClick="btnfillforOB_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" colspan="2">
                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                    <tr>
                                        <td align="right" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" runat="server" TabIndex="1"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            &nbsp; <span class="elementLabel">AD Code :</span>&nbsp;&nbsp;
                                            <asp:Label ID="lblAdcodeDesc" runat="server" Style="font-size: small" Width="50px"></asp:Label>
                                            &nbsp;                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Currency :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                            <asp:DropDownList ID="dropDownListCurrency" CssClass="dropdownList" runat="server"
                                                TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="dropDownListCurrency_SelectedIndexChanged1">
                                            </asp:DropDownList>
                                            <asp:Button ID="btncurrList" runat="server" Visible="false" CssClass="btnHelp_enabled" />
                                            &nbsp;
                                            <asp:Label ID="txtCurrencyDescription" runat="server" CssClass="elementLabel" Width="250px">INDIAN RUPEES</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">From Date :</span>
                                        </td>
                                        <td align="left">
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
                                        <td align="right">
                                            <span class="mandatoryField">*</span> <span class="elementLabel">Country of Vostro A/C
                                                Holder :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                            <asp:TextBox ID="txtCountryAC" runat="server" CssClass="textBox" MaxLength="2" AutoPostBack="true"
                                                Width="30" OnTextChanged="txtCountryAC_TextChanged" style="text-transform:uppercase"></asp:TextBox>
                                            <asp:Button ID="btnCountryACHolder" runat="server" CssClass="btnHelp_enabled" />
                                            <asp:Label ID="txtCountryACHolder" runat="server" CssClass="elementLabel" Width="250"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="mandatoryField">*</span> <span class="elementLabel">Bank Code :</span>
                                        </td>
                                        <td align="left" nowrap>
                                            &nbsp;
                                            <asp:TextBox ID="txtBankCode" CssClass="textBox" runat="server" AutoPostBack="true"
                                                Width="30px" OnTextChanged="txtBankCode_TextChanged"></asp:TextBox>
                                            <asp:Button ID="BtnBankCode" runat="server" CssClass="btnHelp_enabled" />
                                            <asp:Label ID="lblBankCode" runat="server" CssClass="elementLabel" Width="250"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td align="Left">
                                            <table cellspacing="0" border="0" width="220px">
                                                <tr>
                                                    <td align="right" width="110px">
                                                        <span class="elementLabel">&nbsp;Debit Amount</span>
                                                    </td>
                                                    <td align="right" width="110px">
                                                        <span class="elementLabel">Credit Amount</span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Opening Balance :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                            <asp:TextBox ID="txtOP_D" runat="server" CssClass="textBox" MaxLength="15" Width="100px"
                                                Style="text-align: right" TabIndex="7"></asp:TextBox>
                                            &nbsp;
                                            <asp:TextBox ID="txtOP_C" runat="server" CssClass="textBox" MaxLength="15" Width="100px"
                                                Style="text-align: right" TabIndex="8"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Closing Balance :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                            <asp:TextBox ID="txtCL_D" runat="server" CssClass="textBox" MaxLength="15" Width="100px"
                                                Style="text-align: right" TabIndex="9"></asp:TextBox>
                                            &nbsp;
                                            <asp:TextBox ID="txtCL_C" runat="server" CssClass="textBox" MaxLength="15" Width="100px"
                                                Style="text-align: right" TabIndex="10"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2" style="width: 100%; border: 1px  #49A3FF" valign="bottom">
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                        </td>
                                        <td align="left" style="width: 640px; padding-top: 10px; padding-bottom: 10px">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" OnClick="btnSave_Click"
                                                TabIndex="11" Text="Save" ToolTip="Save" />
                                            <asp:Button ID="btnDelete" runat="server" CssClass="buttonDefault" OnClick="btnDelete_Click"
                                                TabIndex="12" Text="Delete" ToolTip="Delete" />
                                            <asp:Button ID="btnCancel" runat="server" CssClass="buttonDefault" OnClick="btnCancel_Click"
                                                TabIndex="13" Text="Cancel" ToolTip="Cancel" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>
