<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_AuditTrail_LoginLogOut.aspx.cs"
    Inherits="TF_AuditTrail_LoginLogOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">

        function OpenUserList() {
            open_popup('TF_UserLookUp.aspx?PageHeader=User LookUp', 450, 450, 'UserList');
            return false;
        }
        function selectUser(selectedID) {
            var id = selectedID;
            document.getElementById('txtUserName').value = id;
        }
        //function checkUser() {

        //  var txtUserName = document.getElementById('txtUserName');
        //  var ddlUserList = document.getElementById('ddlUserList').options;
        //  var userExists = "False";
        // if (txtUserName.value != "") {
        //    for (var i = 0; i < ddlUserList.length; i++) {
        //        if (ddlUserList[i].value.toLowerCase() == txtUserName.value.toLowerCase()) {
        //            userExists = "True";
        //            break;
        //       }
        //    }
        //    if (userExists == "False") {
        //       alert('User : ' + txtUserName.value + ' not exists.');
        //        txtUserName.value = "";
        //        txtUserName.focus();
        //         return false;
        //     }
        //  }
        //   return true;
        // }

        //        function toogleDisplay() {
        //            var rdbAllCustomer = document.getElementById('rdbAllCustomer');
        //            var rdbSelectedCustomer = document.getElementById('rdbSelectedCustomer');
        //            var divUser = document.getElementById('divUser');

        //            if (rdbAllCustomer.checked == true) {
        //                divUser.style.display = 'none';
        //            }
        //            else {
        //                divUser.style.display = 'block';
        //            }
        //            return true;

        //        }
    </script>
    <script language="javascript" type="text/javascript">
        function Generate() {
            var ddlBranch = document.getElementById('ddlBranch');
            var txtFromDate = document.getElementById('txtFromDate');
            var txtToDate = document.getElementById('txtToDate');

            var rdbAllCustomer = document.getElementById('rdbAllCustomer');
            var rdbSelectedCustomer = document.getElementById('rdbSelectedCustomer');

            var Branch = document.getElementById('ddlBranch').value;
            if (Branch == 0) {
                alert('Select Branch Name.');
                Branch.focus();
                return false;
            }

            var fromDate = document.getElementById('txtFromDate');
            if (fromDate.value == '') {
                alert('Select From Date.');
                fromDate.focus();
                return false;
            }
            var toDate = document.getElementById('txtToDate');
            if (toDate.value == '') {
                alert('Select To Date.');
                toDate.focus();
                return false;
            }

            if (toDate.value == '') {
                alert('Select To Date.');
                toDate.focus();
                return false;
            }
            var txtUserName = document.getElementById('txtUserName');
            if (document.getElementById('rdbSelectedCustomer').checked == true && txtUserName.value == '') {
                alert('Select User Name.');
                txtUserName.focus();
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form2" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script src="Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script src="Scripts/InitEndRequest.js" type="text/javascript"></script>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="Images/ajax-loader.gif" style="border: 0px" alt="" />
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
                                <strong>
                                    <asp:Label ID="PageHeader" CssClass="pageLabel" Font-Bold="true" runat="server"></asp:Label></strong>
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
                                <input type="hidden" id="hdnModule" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                                <table cellspacing="0" cellpadding="0" border="0" width="800px" style="line-height: 150%">
                                    <tr>
                                        <td width="15%" align="right" nowrap>
                                            <span class="mandatoryField">* </span><span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left" nowrap>
                                            &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" Width="120px"
                                                runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="mandatoryField">*</span><span class="elementLabel">From Date :</span>
                                        </td>
                                        <td align="left" style="width: 700px">
                                            &nbsp;<asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10"
                                                ValidationGroup="dtVal" Width="70" TabIndex="2"></asp:TextBox>
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
                                    <tr>
                                        <td align="right">
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rdbAllCustomer" runat="server" CssClass="elementLabel" GroupName="Data1"
                                                TabIndex="4" Text="All User" Checked="true" AutoPostBack="true" OnCheckedChanged="rdbAllCustomer_CheckedChanged" />
                                            <asp:RadioButton ID="rdbSelectedCustomer" runat="server" CssClass="elementLabel"
                                                GroupName="Data1" TabIndex="4" AutoPostBack="true" Text="Selected User" OnCheckedChanged="rdbSelectedCustomer_CheckedChanged" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <div id="divUser" runat="server" visible="false">
                                                <span class="elementLabel">User :</span><asp:TextBox ID="txtUserName" CssClass="textBox"
                                                    runat="server" TabIndex="5" OnTextChanged="txtUserName_TextChanged" AutoPostBack="true"></asp:TextBox><asp:Button
                                                        ID="btnUserList" CssClass="btnHelp_enabled" runat="server" TabIndex="-1" />
                                                <asp:Label ID="lblUserName" runat="server" CssClass="elementLabel" Width="400px"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr valign="bottom">
                                        <td align="right" style="width: 120px">
                                        </td>
                                        <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                            <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Generate"
                                                ToolTip="Generate" TabIndex="7" OnClick="btnSave_Click" />
                                            <asp:DropDownList ID="ddlUserList" runat="server" Style="display: none;" CssClass="dropdownList"
                                                TabIndex="-1" Width="50px">
                                            </asp:DropDownList>
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
    <%--<script type="text/javascript" language="javascript">
        window.onload = function () {
            toogleDisplay();
        }
    </script>--%>
</body>
</html>
