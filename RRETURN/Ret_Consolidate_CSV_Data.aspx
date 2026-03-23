<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ret_Consolidate_CSV_Data.aspx.cs"
    Inherits="RRETURN_Ret_Consolidate_CSV_Data" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" src="../Scripts/jquery.min.js"></script>    
    <script type="text/javascript" language="javascript">
        function CheckBranch() {
            var BranchSelect = document.getElementById('ddlBranch');
            var selectedBranch = BranchSelect.options[BranchSelect.selectedIndex].text;
            if (selectedBranch == "GIFT CITY") {
                alert('This Branch is Not Allowed For Consolidation.');
                return false;
            }
        }
        function ShowProgress() {            
                setTimeout(function () {
                    var modal = $('<div />');
                    modal.addClass("modal");
                    $('body').append(modal);
                    var loading = $(".loading");
                    loading.show();
                    var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                    var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                    loading.css({ top: top, left: left });
                }, 200);
        }
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to consolidate data?")) {
                confirm_value.value = "Yes";
                var btnConfirmval = document.getElementById('btnConfirm');
                btnConfirmval.click();
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 152px;
        }
        
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: transparent;
            z-index: 99;
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid navy;
            width: 200px;
            height: 85px;
            display: none;
            position: absolute;
            background-color: White;
            z-index: 999;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
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
    <div class="loading" align="center">
        Consolidation Data. Please wait.
        <br />
        <img src="../Images/Consolidate.gif" alt="" />
    </div>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnUpload" />
                    <asp:PostBackTrigger ControlID="btnConfirm" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <asp:HiddenField ID="hdnbranch" runat="server" />
                        <tr>
                            <input type="hidden" id="hdnCustId" runat="server" />
                            <td align="left" style="width: 50%; font-weight: bold" valign="bottom">
                                <span class="pageLabel" style="font-weight: bold">Consolidate Branch CSV File At Head
                                    OFfice</span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Label ID="lblerrormessage" runat="server"></asp:Label>
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
                                        <td align="left" colspan="2" nowrap>
                                            <%--<asp:Label ID="labelMessage1" runat="server" CssClass="mandatoryField"></asp:Label>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style1">
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
                                        <td align="right" class="style1">
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
                                        <td align="right" class="style1">
                                            <span class="elementLabel">To FortNight Date :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10"
                                                Width="70" TabIndex="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style1">
                                            <asp:Label ID="lblMumbai" runat="server" Text="Mumbai :" class="elementLabel"></asp:Label>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:Label ID="labelmumbai" runat="server" CssClass="mandatoryField"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style1">
                                            <asp:Label ID="lblDelhi" runat="server" Text="New Delhi :" class="elementLabel"></asp:Label>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:Label ID="labeldelhi" runat="server" CssClass="mandatoryField"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style1">
                                            <asp:Label ID="lblBanglore" runat="server" Text="Bangalore :" class="elementLabel"></asp:Label>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:Label ID="labelBangalore" runat="server" CssClass="mandatoryField"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style1">
                                            <asp:Label ID="lblChennai" runat="server" Text="Chennai :" class="elementLabel"></asp:Label>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:Label ID="labelchennai" runat="server" CssClass="mandatoryField"></asp:Label>
                                        </td>
                                    </tr>
                                  
                                    <tr>
                                        <td align="left" colspan="2">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <table border="1" cellpadding="2" style="border-color: red">
                                        <tr>
                                            <td align="center" nowrap width="400px">
                                                &nbsp;<asp:Label ID="labelmessage" runat="server" Font-Bold="true" Font-Size="Small"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <table border="0" cellpadding="2" style="border-color: red">
                                        <tr>
                                            <td align="center" nowrap width="400px">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnUpload" runat="server" CssClass="buttonDefault" OnClick="btnUpload_Click"
                                                            TabIndex="3" Text="Consolidate" ToolTip="Upload CSV File" />
                                                        <%--<asp:HiddenField  ID="btnConfirm" runat="server"/>--%>
                                                        <asp:Button ID="btnConfirm" runat="server" OnClick="btnConfirm_Click" TabIndex="3"
                                                            Style="visibility: hidden" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </table>
                            </td>
                        </tr>
                    </table>
                    </td> </tr> </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
        </center>
    </div>
    </form>
</body>
</html>
