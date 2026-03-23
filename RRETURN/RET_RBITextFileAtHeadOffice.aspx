<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RET_RBITextFileAtHeadOffice.aspx.cs" Inherits="RRETURN_RET_RBITextFileAtHeadOffice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="../Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="../Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript">
        function validateControl() {
            var BranchSelect = document.getElementById('ddlBranch');
            var selectedBranch = BranchSelect.options[BranchSelect.selectedIndex].text;
            var fromdate = document.getElementById('txtFromDate');
            var toDate = document.getElementById('txtToDate');
            var fromdateyyyy = fromdate.value.split("/")[2];
            var fromdatemm = fromdate.value.split("/")[1];
            var fromdatedd = fromdate.value.split("/")[0];
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
            if (selectedBranch == "GIFT CITY") {
                alert('This Branch is Not Allowed For This Operation.');
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
    </script>
    <style type="text/css">
        .style1
        {
            width: 249px;
        }
        .style2
        {
            width: 281px;
        }
        .style6
        {
            width: 90px;
        }
        .style7
        {
            width: 174px;
        }
        .style8
        {
            width: 120px;
        }
        
        
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
        
        .GridviewDiv
        {
            font-size: 100%;
            font-family: 'Lucida Grande' , 'Lucida Sans Unicode' , Verdana, Arial, Helevetica, sans-serif;
            color: #303933;
        }
        Table.Gridview
        {
            border: solid 1px #df5015;
        }
        .Gridview th
        {
            color: #FFFFFF;
            border-right-color: #abb079;
            border-bottom-color: #abb079;
            padding: 0.5em 0.5em 0.5em 0.5em;
            text-align: center;
        }
        .Gridview td
        {
            border-bottom-color: #f0f2da;
            border-right-color: #f0f2da;
            padding: 0.5em 0.5em 0.5em 0.5em;
        }
        .Gridview tr
        {
            color: Black;
            background-color: White;
            text-align: left;
        }
        :link, :visited
        {
            color: #DF4F13;
            text-decoration: none;
        }
    </style>
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
    <br />
    <div align="left">
        <span class="pageLabel" style="font-weight: bold">RBI Text File Creation At Head Office</span>
        <hr />
        <table>
            <tr>
                <td align="right">
                    <span class="mandatoryField">*</span> <span class="elementLabel">Branch :</span>
                </td>
                <td align="left" class="style1">
                    &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" Width="100px"
                        runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    QE File:
                </td>
                <td>
                    <asp:Label ID="lblqename" runat="server"></asp:Label>
                </td>
                <td>
                    <div>
                        <asp:LinkButton ID="lnkQEDownload" runat="server" Visible="false" Text="Download File" CssClass="buttonDefault"
                            OnClick="lnkQEDownload_Click"></asp:LinkButton>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <span class="elementLabel">From FortNight Date :<span>
                </td>
                <td align="left" class="style1">
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
                <td>
                    BOP6 File:
                </td>
                <td>
                    <asp:Label ID="lblbop6name" runat="server"></asp:Label>
                </td>
                <td>
                    <div>
                        <asp:LinkButton ID="lnkBOP6Download" runat="server" Visible="false" Text="Download File" CssClass="buttonDefault"
                            OnClick="lnkBOP6Download_Click"></asp:LinkButton>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <span class="elementLabel">To FortNight Date :</span>
                </td>
                <td align="left" class="style1" colspan="4">
                    &nbsp;<asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10"
                        Width="70" TabIndex="2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left" colspan="4">
                    &nbsp;<asp:Button ID="btnCreate" runat="server" Text="Generate Files" CssClass="buttonDefault"
                        ToolTip="Generate Files For ERS" TabIndex="3" OnClick="btnCreate_Click" />
                    <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                </td>
            </tr>
        </table>
        <hr />
    </div>
    </form>
</body>
</html>
