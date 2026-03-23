<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptRRETURN_ValidationInputInvoiceFile.aspx.cs" Inherits="Reports_RRETURNReports_rptRRETURN_ValidationInputInvoiceFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />   
    
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
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
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <Triggers>
                    <%--<asp:PostBackTrigger ControlID="btnUpldCSV" />--%>
                    <asp:PostBackTrigger ControlID="btnValidate" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <input type="hidden" id="hdnCustId" runat="server" />
                            <td align="left" style="width: 50%; font-weight: bold" valign="bottom">
                                <span class="pageLabel" style="font-weight: bold">Data Validation Of Input Upload File</span>
                            </td>
                            <td align="right" style="width: 50%">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%;" valign="top" colspan="2">
                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                    <tr>
                                        <td colspan="2" nowrap align="center">
                                            <%--<asp:Label ID="labelMessage1" runat="server" CssClass="mandatoryField"></asp:Label>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Branch :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" runat="server" AutoPostBack="true"
                                                TabIndex="1" Width="100px">
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;
                                            <%--<asp:Label Text="" ID="lbl_adcode" runat="server" CssClass="elementLabel" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">From FortNight Date :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10"
                                                ValidationGroup="dtVal" Width="70" TabIndex="2" AutoPostBack="true"></asp:TextBox>
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
                                            <span class="elementLabel">To FortNight Date :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10"
                                                Width="70" TabIndex="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td align="right" style="width: 150px">
                                            <span class="elementLabel">Select File :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:FileUpload ID="fileinhouse" runat="server" ViewStateMode="Enabled" TabIndex="3"
                                                Width="500px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Input File :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:TextBox ID="txtInputFile" runat="server" CssClass="textBox" MaxLength="10"
                                                Width="413px" TabIndex="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <table border="0" style="border-color: red" cellpadding="2">
                                                <tr>
                                                    <td width="130px" nowrap>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Button ID="btnValidate" runat="server" Text="Validate" CssClass="buttonDefault"
                                                                    ToolTip="Validate" TabIndex="4" OnClick="btnValidate_Click" />                                                                
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <br />
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

