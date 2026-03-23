<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptRRETURN_Datachecklist.aspx.cs"
    Inherits="Reports_RRETURNReports_rptRRETURN_Datachecklist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
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
        function PurposeCodehelp() {
            var fromDate = document.getElementById('txtFromDate');
            var toDate = document.getElementById('txtToDate');
            if (fromDate.value == '') {
                alert('Select From Date.');
                fromDate.focus();
                return false;
            }
            var from = document.getElementById('txtFromDate').value;
            var to = document.getElementById('txtToDate').value;
            var adcode = document.getElementById('ddlBranch').value;
            popup = window.open('TF_RRETURN_PurposecodeLookup.aspx?PageHeader=Purpose Code Lookup&from=' + from + '&to=' + to + '&adcode=' + adcode, 'helpPurposeId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpPurposeId"
            return false;
        }
        function PurposeId(event) {
            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('btnPurposeList').click();
            }
        }
        function sss() {
            var s = popup.document.getElementById('txtcell1').value;
            if (common == "helpPurposeId") {
                document.getElementById('txtPurposeCode').value = s;
            }
        }
        function validateSave() {

            var Report = document.getElementById('PageHeader').innerHTML;
            var Branch = document.getElementById('ddlBranch').value;            
            var fromDate = document.getElementById('txtFromDate');
            var toDate = document.getElementById('txtToDate');

            if (Branch == '0') {
                alert('Select Branch Name.');
                Branch.focus();
                return false;
            }
            if (fromDate.value == '') {
                alert('Select From Date.');
                fromDate.focus();
                return false;
            }
            if (toDate.value == '') {
                alert('Select To Date.');
                toDate.focus();
                return false;
            }
            if (document.getElementById('rdbPurposeCodewise').checked) {
                if (document.getElementById('rdbSelectedPurposeCode').checked) {
                    if (document.getElementById('txtPurposeCode').value == '') {
                        alert('Enter Purpose Code');
                        document.getElementById('txtPurposeCode').focus();
                        return false;
                    }
                }
            }
        }
    </script>
</head>
<body>
    <form id="form2" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
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
                <%--  <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                </Triggers>--%>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <%--  <span class="pageLabel">Export Bill Delinking Register</span>--%>
                                <asp:Label ID="PageHeader" CssClass="pageLabel" runat="server" Style="font-weight: bold"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                                <table cellspacing="0" cellpadding="0" border="0" width="800px" style="line-height: 150%">
                                    <tr>
                                        <td width="10%" align="right" nowrap>
                                            <span class="mandatoryField">* </span><span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left" nowrap>
                                            &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                                AutoPostBack="true" Width="100px" runat="server">
                                            </asp:DropDownList>
                                            <%--<asp:Button ID="btnPurCodeList" runat="server" CssClass="btnHelp_enabled" 
                                                    TabIndex="-1" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 100px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">From Date :</span>
                                        </td>
                                        <td align="left" style="width: 700px">
                                            &nbsp;<asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10"
                                                ValidationGroup="dtVal" Width="70" TabIndex="2" AutoPostBack="true"></asp:TextBox>
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
                                <table cellspacing="0" border="0">
                                    <tr>
                                        <td height="40px" align="left" valign="middle" width="150px">
                                            <asp:RadioButton ID="rdbTypewise" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data" OnCheckedChanged="rdbTypewise_CheckedChanged" Text="Type wise"
                                                Style="forecolor: #000000; font-weight: bold;" TabIndex="4" Checked="True" />
                                        </td>
                                        <td height="20px" valign="middle" width="150px">
                                            <asp:RadioButton ID="rdbPurposeCodewise" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                Text="Purpose Code wise" GroupName="Data" OnCheckedChanged="rdbPurposeCodewise_CheckedChanged"
                                                Style="forecolor: #000000; font-weight: bold;" TabIndex="5" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap>
                                            <asp:RadioButton ID="rdbAllTypes" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data2" OnCheckedChanged="rdbAllTypes_CheckedChanged" TabIndex="6"
                                                Text="All Types" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <%--  //<br />--%>
                                            <asp:RadioButton ID="rdbSelectedType" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data2" OnCheckedChanged="rdbSelectedType_CheckedChanged" TabIndex="6"
                                                Text="Selected Type" />
                                        </td>
                                        <td nowrap>
                                            <asp:RadioButton ID="rdbAllPurposeCode" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data2" OnCheckedChanged="rdbAllPurposeCode_CheckedChanged" TabIndex="7"
                                                Visible="false" Text="All Purpose Code" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <%--  //<br />--%>
                                            <asp:RadioButton ID="rdbSelectedPurposeCode" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data2" OnCheckedChanged="rdbSelectedPurposeCode_CheckedChanged" TabIndex="7"
                                                Visible="false" Text="Selected Purpose Code" />
                                        </td>
                                    </tr>
                                </table>
                                <%--<fieldset id="PurposeList" runat="server" style="width: 900px" visible="false">
                                    <legend>Select Purpose Code</legend>--%>
                                <%--<table id="Table1" runat="server">
                                        <tr>                                                
                                            <td align="right" style="font-weight: bold; color: #000000; font-size: medium;" width="200px">
                                                <span style="color: Red">*</span>Purpose Code :&nbsp;
                                            </td>
                                            <td align="left" width="0px" style="font-weight: bold; color: #000000;">
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="txtPurposeCode" runat="server" CssClass="textBox" MaxLength="40" TabIndex="5"
                                                    Visible="false" Width="100px" OnTextChanged="txtPurposeCode_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                &nbsp;
                                                <asp:Button ID="BtnPurposeList" runat="server" CssClass="btnHelp_enabled" Visible="false" />
                                                &nbsp;
                                                <asp:Label ID="lblPurposeCode" runat="server" CssClass="elementLabel" Visible="false"
                                                    Width="400px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>        --%>
                                <table>
                                    <tr>
                                        <td width="5%" align="left" nowrap>
                                            <span class="elementLabel" id="type" runat="server" visible="false">Type :</span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="dropDownListSelectedType" CssClass="dropdownList" runat="server"
                                                Visible="false" AutoPostBack="true" TabIndex="8" OnSelectedIndexChanged="dropDownListSelectedType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right" style="width: 100px">
                                            <span class="elementLabel" id="Purpose" runat="server" visible="false">Purpose Code
                                                :</span>
                                        </td>
                                        <td align="left" width="0px" style="font-weight: bold; color: #000000;">
                                            <asp:TextBox ID="txtPurposeCode" runat="server" CssClass="textBox" MaxLength="40"
                                                TabIndex="9" Visible="false" Width="100px" OnTextChanged="txtPurposeCode_TextChanged"
                                                AutoPostBack="True"></asp:TextBox>
                                            <asp:Button ID="BtnPurposeList" runat="server" CssClass="btnHelp_enabled" Visible="false" />
                                            &nbsp;
                                            <asp:Label ID="lblPurposeCode" runat="server" CssClass="elementLabel" Visible="false"
                                                Width="400px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr valign="bottom">
                                        <td align="right" style="width: 120px">
                                        </td>
                                        <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                            &nbsp;
                                            <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Generate"
                                                ToolTip="Generate" TabIndex="10" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                            <asp:Button ID="Button1" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                        </td>
                                    </tr>
                                </table>
                                <input type="hidden" runat="server" id="hdnFromDate" />
                                <input type="hidden" runat="server" id="hdnToDate" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>
