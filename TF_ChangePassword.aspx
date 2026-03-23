<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_ChangePassword.aspx.cs"
    Inherits="TF_ChangePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-Tradefinance System</title>
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <script src="Scripts/Validations.js" language="javascript" type="text/javascript"></script>
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">
        function ShowAlert() {
            //            var _newpassword = document.getElementById('txtNewPassword');
            //            alert('Change your password.');
            //           _newpassword.focus();           
        }

        function ValidatePass() {
            var _password = document.getElementById('txtNewPassword');
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

        function validateSave() {
            var RegExpres = /^[a-z0-9 ]+$/i;
            var _userName = document.getElementById('txtUserName');
            var _password = document.getElementById('txtPassword');
            var _newpassword = document.getElementById('txtNewPassword');
            var _retypePassword = document.getElementById('txtReTypePassword');
            var _role = document.getElementById('ddlRole');


            if (_password.value == '') {
                alert('Enter Password.');
                _password.focus();
                return false;
            }
            //            if (RegExpres.test(trimAll(_password.value)) == false) {
            //                alert('Only Alphanumeric value is allowed.');
            //                _password.focus();
            //                return false;
            //            }

            ////This is to check whether new password and previous password are not same


            if (_password.value == _newpassword.value) {
                alert('Password cannot be same as previous password.');
                _newpassword.focus();
                return false;
            }

            if (_retypePassword.value == '') {
                alert('Re-Type Password.');
                _retypePassword.focus();
                return false;
            }

            //            if (RegExpres.test(trimAll(_retypePassword.value)) == false) {
            //                alert('Only Alphanumeric value is allowed.');
            //                _retypePassword.focus();
            //                return false;
            //            }

            if (_newpassword.value != _retypePassword.value) {
                alert('Password and Re-Typed Password are not matching.');
                _retypePassword.focus();
                return false;
            }

            return true;
        }



    </script>
</head>
<body onload="ShowAlert();">
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnSave">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <div>
        <center>
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 100%" valign="bottom" colspan="2">
                                <span class="pageLabel"><b>Change Password</b></span>
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
                                                Width="150px" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 250px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">New Password :</span>
                                        </td>
                                        <td align="left" style="width: 700px">
                                            &nbsp;<asp:TextBox ID="txtNewPassword" runat="server" CssClass="textBox" MaxLength="15"
                                                Width="150px" TextMode="Password"></asp:TextBox>&nbsp;<%--<span class="mandatoryField"><b>Password
                                                    Must Contain at least one number, one lowercase, one uppercase, one special charactor
                                                    and Minimum length 8 charactor Example:[Rajesh@1234]</b></span>--%>
                                        </td>
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
                                        </td>
                                        <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                OnClick="btnSave_Click" ToolTip="Save" />
                                            &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                OnClick="btnCancel_Click" ToolTip="Save" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="left" style="width: 60%; border: 1px solid #49A3FF" valign="top">
                            &nbsp;&nbsp;&nbsp;<span class="elementLabel" style="font-size:large;">Password Policy</span><br/><br />
                            <span class="mandatoryField" style="font-size:small;">1. Minimum lengh should be 8 Characters.</span><br />
                            <span class="mandatoryField" style="font-size:small;">2. Password should be Alphanumeric plus Special
                                            character with atlest One Upper case Character.</span><br />
                                            <span class="mandatoryField" style="font-size:small;">3. eg. Username_1234 OR Username@1234.</span><br />
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
