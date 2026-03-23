<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_HouseKeeping.aspx.cs"
    Inherits="TF_HouseKeeping" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-Tradefinance System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="Images/favicon.ico" type="image/ico" />
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Validations.js" language="javascript" type="text/javascript"></script>
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript">

        function Blank() {
            document.getElementById('txtPassword').value = '';
        }
        function ChangePass() {
            var C = confirm('Do you want to change password?');


            if (C == true) {
                document.getElementById('txtPassword').value = '';
                document.getElementById('txtReTypePassword').value = '';

            } else {
                return false;
            }


        }



        function validateSave() {
            if (document.getElementById('hdntype').value == "Pwsd") {
                return validatePaswd();
            }
            else {
                var _userName = document.getElementById('txtUserName');
                var _password = document.getElementById('txtPassword');
                var _retypePassword = document.getElementById('txtReTypePassword');
                var _role = document.getElementById('ddlRole');

                if (_userName.value == '') {
                    alert('Enter User Name.');
                    _userName.focus();
                    return false;
                }
                if (_password.value == '') {
                    alert('Enter Password.');
                    _password.focus();
                    return false;
                }
                if (_password.value.length < 8) {
                    alert('Enter minimum 8 characters in Password.');
                    _password.focus();
                    return false;
                }
                if (_retypePassword.value == '') {
                    alert('Re-Type Password.');
                    _retypePassword.focus();
                    return false;
                }
                if (_password.value != _retypePassword.value) {
                    alert('Password and Re-Typed Password are not matching.');
                    _retypePassword.focus();
                    return false;
                }
                if (_role.selectedIndex == -1 || _role.selectedIndex == 0) {
                    alert('Select Role.');
                    _role.focus();
                    return false;
                }
            }
            return true;
        }

        function ValidatePass() {
            var _password = document.getElementById('txtPassword');
            var strongRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})/
            if (_password.value != '') {
                if (_password.value.length >= 8) {
                    if (strongRegex.test(_password.value) == false) {
                        alert('Password Does Not Match Password Policy.');
                        _password.focus();
                        return false;
                    }
                }
                else {
                    alert('Password Length Should Be Min 8 Charactor.');
                    _password.focus();
                    return false;
                }
            }
        }

        function validatePaswd() {
            var _password = document.getElementById('txtPassword');
            var _retypePassword = document.getElementById('txtReTypePassword');
            var _newpassword = document.getElementById('hdnpswd');
            if (_password.value == '') {
                alert('Enter Password.');
                _password.focus();
                return false;
            }
            if (_password.value.length < 8) {
                alert('Enter minimum 8 characters in Password.');
                _password.focus();
                return false;
            }
            if (_password.value == _newpassword.value) {
                alert('Password cannot be same as previous password.');
                _password.focus();
                return false;
            }
            if (_retypePassword.value == '') {
                alert('Enter Re-Type Password.');
                _retypePassword.focus();
                return false;
            }
            if (_password.value != _retypePassword.value) {
                alert('Password and Re-Typed Password are not matching.');
                _retypePassword.focus();
                return false;
            }
            return true;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnSave">
    <asp:scriptmanager id="ScriptManagerMain" runat="server">
    </asp:scriptmanager>
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
            <asp:updatepanel id="UpdatePanelMain" runat="server" updatemode="conditional">
                <triggers>
                <asp:PostBackTrigger ControlID="btnChangePaswd" />
            </triggers>
                <contenttemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 100%" valign="bottom" colspan="2">
                                <span class="pageLabel"><b>User Details</b></span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 40%; border: 1px solid #49A3FF" valign="top">
                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                    <tr>
                                        <td align="right" style="width: 250px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">User Name :</span>
                                        </td>
                                        <td align="left" style="width: 700px">
                                            &nbsp;<asp:TextBox ID="txtUserName" runat="server" CssClass="textBox" MaxLength="20"
                                                Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 250px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Password :</span>
                                        </td>
                                        <td align="left" style="width: 700px">
                                            &nbsp;<asp:TextBox ID="txtPassword" runat="server" CssClass="textBox" MaxLength="15"
                                                Width="150px" TextMode="Password" tooltip="Password Must Contain at least one number, one lowercase, one uppercase, one special charactor and Minimum length 8 charactor Example:[Rajesh@1243]"></asp:TextBox>&nbsp;<%--<span class="mandatoryField"><b>Password
                                                    Must Contain at least one number, one lowercase, one uppercase, one special charactor
                                                    and Minimum length 8 charactor Example:[Rajesh@1243]</b></span>--%></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 250px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Re-Type Password :</span>
                                        </td>
                                        <td align="left" style="width: 700px">
                                            &nbsp;<asp:TextBox ID="txtReTypePassword" runat="server" CssClass="textBox" MaxLength="15"
                                                Width="150px" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 250px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Role :</span>
                                        </td>
                                        <td align="left" style="width: 700px">
                                            &nbsp;<asp:DropDownList ID="ddlRole" runat="server" CssClass="dropdownList">
                                                <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                                <asp:ListItem Value="Supervisor" Text="Supervisor"></asp:ListItem>
                                                <asp:ListItem Value="User" Text="User"></asp:ListItem>
                                                <asp:ListItem Value="OIC" Text="OIC"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 250px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Status :</span>
                                        </td>
                                        <td align="left" style="width: 700px">
                                            &nbsp;<asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropdownList">
                                                <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="In-Active"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 250px">
                                        </td>
                                        <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                OnClick="btnSave_Click" ToolTip="Save" />
                                            <asp:Button ID="btnChangePaswd" runat="server" Text="Change Password" Width="120"
                                                CssClass="buttonDefault" OnClick="btnChangePaswd_Click" ToolTip="Change Password" />
                                            <input type="hidden" id="hdntype" runat="server" />
                                            <input type="hidden" id="hdnpswd" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="left" style="width: 60%; border: 1px solid #49A3FF" valign="top">
                            &nbsp;&nbsp;&nbsp;<span class="elementLabel" style="font-size:large;"><b>Password Policy</b></span><br/><br />
                            <span class="mandatoryField" style="font-size:small;">1. Minimum lengh should be 8 Characters.</span><br />
                            <span class="mandatoryField" style="font-size:small;">2. Password should be Alphanumeric plus Special
                                            character with atlest One Upper case Character.</span><br />
                                            <span class="mandatoryField" style="font-size:small;">3. eg. Username_1234 OR Username@1234.</span><br />
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
