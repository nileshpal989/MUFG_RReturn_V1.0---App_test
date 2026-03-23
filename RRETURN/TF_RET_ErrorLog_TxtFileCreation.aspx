<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_RET_ErrorLog_TxtFileCreation.aspx.cs"
    Inherits="RRETURN_TF_RET_ErrorLog_TxtFileCreation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="../Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="../Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">
        function ClickAnotherButton() {
            document.getElementById('btndwnldreport').click();
        }
        function validateControl() {
            var fromdate = document.getElementById('txtFromDate');
            var toDate = document.getElementById('txtToDate');
            if (fromdate.value == '') {
                alert('Select From Date.');
                document.getElementById('txtFromDate').focus();
                return false;
            }
            if (toDate.value == '') {
                alert('Select To Date.');
                document.getElementById('txtToDate').focus();
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" unselectable="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <uc1:Menu ID="Menu1" runat="server" />
    <br />
    <div align="left">
        <span class="pageLabel" style="font-weight: bold">Error Log</span>
        <hr />
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnCreate" />
                <asp:PostBackTrigger ControlID="btndwnldreport" />
            </Triggers>
            <ContentTemplate>
                <table>
                    <tr>
                        <%--<td align="right">
                            <span class="mandatoryField">*</span> <span class="elementLabel">Branch</span>
                        </td>--%>
                        <td align="left">
                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" Width="100px"
                                runat="server" Style="visibility: hidden">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="elementLabel">From Date</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                Width="70" TabIndex="2" AutoPostBack="true"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="mdDocdate" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtFromDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtFromDate" PopupButtonID="btncalendar_FromDate" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdDocdate"
                                ValidationGroup="dtVal" ControlToValidate="txtFromDate" EmptyValueMessage="Enter Date Value"
                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="Invalid"></ajaxToolkit:MaskedEditValidator>                           
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="elementLabel">To Date</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" Width="70"
                                TabIndex="2"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtToDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btncalender_todate" runat="server" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtToDate" PopupButtonID="btncalender_todate" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="mdDocdate"
                                ValidationGroup="dtVal" ControlToValidate="txtToDate" EmptyValueMessage="Enter Date Value"
                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="Invalid"></ajaxToolkit:MaskedEditValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnCreate" runat="server" Text="Create File" CssClass="buttonDefault"
                                ToolTip="Create File" TabIndex="3" OnClick="btnCreate_Click" />
                            <td>
                                <asp:Button ID="btndwnldreport" runat="server" Style="visibility: hidden" OnClick="lnkDownload_Click" />                                
                            </td>
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <hr />
    </form>
</body>
</html>
