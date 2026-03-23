<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptRRETURN_Schedule.aspx.cs"
    Inherits="Reports_RRETURNReports_rptRRETURN_Schedule" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script type="text/javascript">
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0! 
        var yyyy = today.getFullYear();
        if (dd < 10) { dd = '0' + dd }
        if (mm < 10) { mm = '0' + mm }
        function toDate() {
            if (document.getElementById('txtFromDate').value != "__/__/____") {
                var toDate;
                toDate = dd + '/' + mm + '/' + yyyy;
                document.getElementById('txtToDate').value = toDate;
            }
        } 
    </script>
    <script language="javascript" type="text/javascript">
        function OpenAuthSignCodeList(e) {
            var keycode;
            var txtAuthSign;
            var Branch = document.getElementById('ddlBranch').value;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                txtAuthSign = document.getElementById('txtAuthSign').value;
                open_popup('RRETURN_AuthSign_Help.aspx?PageHeader=Auth Sign&CustID=' + txtAuthSign + '&Branch=' + Branch, 450, 550, 'AuthSignCodeList');
                return false;
            }
        }
        function selectAuthSign(selectedID) {
            var id = selectedID;
            document.getElementById('hdnAuthSignCode').value = id;
            document.getElementById('btnAuthSignCode').click();
        }
        function OpenCurrencyList(hNo) {
            open_popup('../../TF_CurrencyLookup1.aspx?PageHeader=Auth Sign&pc=' + hNo, 450, 400, 'CurrencyList');
            return false;
        }
        function selectCurrency(selectedID, selectedName, hNo) {
            var id = selectedID;
            var Name = selectedName;
            document.getElementById('hdnCurrencyHelpNo').value = hNo;
            document.getElementById('hdnCurId').value = id;
            document.getElementById('hdnCurName').value = Name;
            document.getElementById('btnCurr').click();
        }
        function changeCurrencyDesc() {
            var txtCurrency = document.getElementById('txtCurrency');
            var lblCurDesc = document.getElementById('lblCurDesc');
            if (txtCurrency.value != "")
                lblCurDesc.innerHTML = txtCurrency.value;
            else
                lblCurDesc.innerHTML = "";
            return true;
        }
        function Custhelp() {
            var fromdate = document.getElementById('txtfromDate');
            var toDate = document.getElementById('txtToDate');
            if (fromdate.value == '') {
                alert('Select From Date.');
                fromdate.focus();
                return false;
            }
            if (toDate.value == '') {
                alert('Select To Date.');
                toDate.focus();
                return false;
            }
            popup = window.open('../../TF_CustomerLookUp1.aspx?PageHeader=Customer Lookup', 'helpCustId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpCustId"
            return false;
        }
        function CustId(event) {
            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('btCustList').click();
            }
        }
        function sss() {
            var s = popup.document.getElementById('txtcell1').value;
            if (common == "helpCustId") {
                document.getElementById('txtCustomer').value = s;
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function toogleDisplay() {
            var rdbAllCurrency = document.getElementById('rdbAllCurrency');
            var rdbSelectedCustomer = document.getElementById('rdbSelectedCustomer');
            var divCureency = document.getElementById('divCureency');
            var txtCurrency = document.getElementById('txtCurrency');
            var lblCurrencyName = document.getElementById('lblCurrencyName');
            var rdbSchedule1A = document.getElementById('rdbSchedule1A');
            var rdbSchedule1B = document.getElementById('rdbSchedule1B');
            //                var txtAuthSign = document.getElementById('txtAuthSign');
            if (rdbAllCurrency.checked == true) {
                divCureency.style.display = 'none';
                txtCurrency.value = '';
                lblCurrencyName.innerHTML = '';
            }
            else {
                divCureency.style.display = 'block';
            }
            return true;
        }
        function Generate() {
            var ddlBranch = document.getElementById('ddlBranch').value;
            var txtfromDate = document.getElementById('txtfromDate');
            var txtToDate = document.getElementById('txtToDate');
            var txtAuthSign = document.getElementById('txtAuthSign');            
            var e = document.getElementById('txtCurrency');            
            if (ddlBranch == 0) {
                alert('Select Branch Name');
                ddlBranch.focus();
                return false;
            }
            if (txtfromDate.value == '') {
                alert('Select From Date.');
                txtfromDate.focus();
                return false;
            }
            if (txtToDate.value == '') {
                alert('Select To Date.');
                txtToDate.focus();
                return false;
            }
            if (txtAuthSign.value == '') {
                alert('Select Authorised Signatory.');
                txtAuthSign.focus();
                return false;
            }
            if (document.getElementById('rdbSelectedCurrency').checked == true && e.value == '') {
                alert('Select Currency.');
                txtCurrency.focus();
                return false;
            }

        }
    </script>
</head>
<body>
    <form id="form2" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script src="../../Scripts/InitEndRequest.js" type="text/javascript"></script>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <asp:Label ID="PageHeader" CssClass="pageLabel" runat="server" Style="font-weight: bold"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 50%" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top">
                                <hr />
                                <input type="hidden" id="hdnCurrencyHelpNo" runat="server" />
                                <input type="hidden" id="hdnCurId" runat="server" />
                                <input type="hidden" id="hdnCurName" runat="server" />
                                <asp:Button ID="btnCurr" Style="display: none;" runat="server" OnClick="btnCurr_Click" />
                                <input type="hidden" id="hdnAuthSignCode" runat="server" />
                                <asp:Button ID="btnAuthSignCode" Style="display: none;" runat="server" OnClick="btnAuthSignCode_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top">
                                <asp:Label ID="lblmessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                                <table cellspacing="0" cellpadding="0" border="0" width="1300px" style="line-height: 150%">
                                    <tr>
                                        <td width="10%" align="right" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left" nowrap>
                                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" AutoPostBack="true"
                                                Width="100px" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 150px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">From Date :</span>
                                        </td>
                                        <td align="left" style="width: 800px">
                                            <asp:TextBox ID="txtfromDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtval"
                                                Width="70" TabIndex="2"></asp:TextBox>
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
                                            <%-- <asp:Button ID="btnChangeDate" runat="server" OnClick="btnChangeDate_Click" />--%>
                                            &nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To Date :</span>
                                            &nbsp;
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" Width="70"
                                                TabIndex="3"></asp:TextBox>
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
                                </table>
                                <br />
                                <table id="Table3" runat="server">
                                    <tr>
                                        <td align="right" style="font-weight: bold; color: #000000; font-size: small;" width="200px">
                                            <asp:Label ID="lblAuthSign" runat="server" Style="font-weight: bold; color: #000000;
                                                font-size: small;" Text="Authorised Signatory  "></asp:Label>
                                        </td>
                                        <td align="left" width="0px" style="font-weight: bold; color: #000000;">
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:TextBox ID="txtAuthSign" runat="server" CssClass="textBox" MaxLength="40" TabIndex="6"
                                                Width="200px" AutoPostBack="true" ToolTip="Press F2 for Help" onkeydown="OpenAuthSignCodeList(this);"
                                                OnTextChanged="txtAuthSign_TextChanged"></asp:TextBox>
                                            <asp:Button ID="btnAuthSignList" runat="server" CssClass="btnHelp_enabled" />
                                            <asp:Label ID="lblAuthSignName" runat="server" CssClass="elementLabel"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table>
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rdbAllCurrency" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data2" TabIndex="4" Text="All Currency" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rdbSelectedCurrency" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data2" TabIndex="5" Text="Selected Currency" />
                                        </td>
                                    </tr>
                                </table>
                                <div id="divCureency">
                                    <table id="Table1" runat="server">
                                        <tr>
                                            <td align="right" style="font-weight: bold; color: #000000; font-size: small;" width="200px">
                                                <asp:Label ID="lblCurrency1" runat="server" Style="font-weight: bold; color: #000000;
                                                    font-size: small;" Text="Currency "></asp:Label>
                                            </td>
                                            <td align="left" nowrap>
                                                <asp:TextBox ID="txtCurrency" runat="server" CssClass="textBox" AutoPostBack="true"
                                                    OnTextChanged="txtCurrency_TextChanged" Width="30"></asp:TextBox>
                                                <asp:Button ID="btnCurrencyList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                                <asp:Label ID="lblCurDesc" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                    </div>
                    <fieldset id="CustList" runat="server" style="width: 800px">
                        <table id="Table2" runat="server">
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rdbSchedule1A" runat="server" CssClass="elementLabel" GroupName="Data3"
                                        TabIndex="7" Width="500px" /><br />
                                    <asp:RadioButton ID="rdbSchedule1B" runat="server" CssClass="elementLabel" GroupName="Data3"
                                        TabIndex="8" Width="500px" /><br />
                                    <asp:RadioButton ID="rdbSchedule1C" runat="server" CssClass="elementLabel" GroupName="Data3"
                                        TabIndex="8" Width="500px" /><br />
                                    <asp:RadioButton ID="rdbSchedule1D" runat="server" CssClass="elementLabel" GroupName="Data3"
                                        TabIndex="8" Width="500px" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <table>
                        <tr valign="bottom">
                            <td align="right" style="width: 120px">
                            </td>
                            <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                &nbsp;
                                <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Generate"
                                    ToolTip="Genarate" TabIndex="7" OnClick="btnSave_Click" />
                                <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                    Style="visibility: hidden" Text="Button" UseSubmitBehavior="false" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
    <script type="text/javascript" language="javascript">
        window.onload = function () {
            toogleDisplay();
        }
    </script>
</body>
</html>
