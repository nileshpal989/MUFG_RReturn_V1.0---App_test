<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ret_Selction.aspx.cs" Inherits="RRETURN_Ret_Selction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
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
            if ((fromdate.value.substring(0, 2) != '01') && (fromdate.value.substring(0, 2) != '16') && fromdate.value!='__/__/____') {
                alert('Invalid From Date1.');
                document.getElementById('txtFromDate').focus();
                return false;
            }
            else {
                if (dt > today) {
                    alert("Invalid From Date2.");
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
    </script>
</head>
<body>
    <form id="form2" runat="server" autocomplete="off" defaultbutton="btnSave">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <div id="Div1">
        <div id="container-signout">
            <div id="container2-signout">
                <asp:Button ID="signout" runat="server" CssClass="signout_bt" Text="Logout" OnClick="signout_Click" />
            </div>
        </div>
        <div id="container-header">
            <div id="container2-header">
                <div id="logo">
                </div>
                <div id="header-info">
                    <asp:Label ID="lblUserName" runat="server"></asp:Label><asp:Label ID="lblRole" runat="server"></asp:Label>
                </div>
                <div id="header-Date">
                    <asp:Label ID="lblTime" runat="server" CssClass="elementLabel"></asp:Label>
                </div>
                <div id="module-info">
                    <asp:Label ID="lblModuleName" runat="server" Text="R-Return Module"></asp:Label>
                </div>
            </div>
            <div id="buttonrow">
                <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSave" />
                    </Triggers>
                    <ContentTemplate>
                        <table cellspacing="0" border="0" width="100%">
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
                                <td align="center" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                                    <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                        <tr>
                                            <td align="right" style="width: 40%">
                                                <span class="mandatoryField">*</span><span class="elementLabel">From Date :</span>
                                            </td>
                                            <td align="left" style="width: 60%">
                                                &nbsp;<asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10"
                                                    ValidationGroup="dtVal" Width="80px" TabIndex="2" AutoPostBack="false"></asp:TextBox>
                                                <asp:Button ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled"
                                                    TabIndex="1" onfocus="this.select()" />
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
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font color="red">[From Date has to be '01' or '16']</font>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                &nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To Date :</span>
                                            </td>
                                            <td align="left">
                                                &nbsp;<asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10"
                                                    Width="80px" TabIndex="3" Enabled="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr valign="bottom">
                                            <td align="right" style="width: 40%">
                                            </td>
                                            <td align="left" style="width: 60%; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                                &nbsp;<asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Ok"
                                                    ToolTip="Ok" TabIndex="4" OnClick="btnSave_Click" ViewStateMode="Enabled" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <input type="hidden" runat="server" id="hdnFromDate" />
                        <input type="hidden" runat="server" id="hdnToDate" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="buttonrow2">
        </div>
        <div id="buttonrow3">
        </div>
        <div id="buttonrow4">
        </div>
        <div class="footer">
            <span class="h2">&copy;&nbsp;2015 Lateral Management Computer Consultants</span>
        </div>
    </div>
    </form>
</body>
</html>
