<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_AddEditUser.aspx.cs" Inherits="TF_AddEditUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-Tradefinance System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="Images/favicon.ico" type="image/ico" />
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Validations.js" language="javascript" type="text/javascript"></script>
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">

        function ConfirmRedirectAddedAccessControl() {

            var Ok = confirm('User Created. Do you want to Provide Access Control?');
            var btrue;

            if (Ok) {
                document.getElementById('btnConfirm').click();
                btrue = true;
            }
            else {
                document.getElementById('btnCancel').click();
                btrue = true;
            }


            return btrue;
        }

        function ConfirmRedirectUpdatedAccessControl() {

            var Ok = confirm('User Updated. Do you want to Update Access Control?');
            var btrue;

            if (Ok) {
                document.getElementById('btnConfirm').click();
                btrue = true;
            }
            else {
                document.getElementById('btnCancel').click();
                btrue = true;
            }


            return btrue;
        }

        function ChangePass() {
            // commented by supriya 16012025
//            var C = confirm('Do you want to change password?');


//            if (C == true) {
//                document.getElementById('txtPassword').value = '';
//                document.getElementById('txtReTypePassword').value = '';

//            } else {
//                return false;
//            }
            // end

        }

        function checkPasswordMatch() {
            var password = document.getElementById('txtPassword').value;
            var confirmPassword = document.getElementById('txtReTypePassword').value;
            var array = password.split('');



            if (password != confirmPassword) {
                document.getElementById('lblPassMatch').value = "Passwords do not match!";
            }
            else
                document.getElementById('lblPassMatch').value = "Passwords match.";
        }

        function validateSave() {

            if (document.getElementById('hdntype').value == "Pwsd") {
                return validatePaswd();
            }
            else {

                //      var RegExpres = /^[a-z0-9 ]+$/i;
                var _ddlBranch = document.getElementById('ddlBranch');
                var _userName = document.getElementById('txtUserName');
                // commented by supriya 16012025
//                var _password = document.getElementById('txtPassword');
                //                var _retypePassword = document.getElementById('txtReTypePassword');
                // end
                var _role = document.getElementById('ddlRole');
                var ddlBranch = document.getElementById('ddlBranch');

                if (_ddlBranch.value == '0') {
                    alert('Select Branch.');
                    _ddlBranch.focus();
                    return false;
                }
                if (_userName.value == '') {
                    alert('Enter User Name.');
                    _userName.focus();
                    return false;
                }
                //                if (RegExpres.test(trimAll(_userName.value)) == false) {
                //                    alert('Only Alphanumeric value is allowed.');
                //                    _userName.focus();
                //                    return false;
                //                }
                // commented by supriya 16012025
//                if (_password.value == '') {
//                    alert('Enter Password.');
//                    _password.focus();
//                    return false;
                //                }
                // end
                //                if (RegExpres.test(trimAll(_password.value)) == false) {
                //                    alert('Only Alphanumeric value is allowed.');
                //                    _password.focus();
                //                    return false;
                //                }
                //                else {
                // commented by supriya 16012025
//                if (_password.value.length < 8) {
//                    alert('Enter minimum 8 characters in Password.');
//                    _password.focus();
//                    return false;
//                }
//                //                }
//                if (_retypePassword.value == '') {
//                    alert('Enter Re-Type Password.');
//                    _retypePassword.focus();
//                    return false;
                //                }
                // end
                //                if (RegExpres.test(trimAll(_retypePassword.value)) == false) {
                //                    alert('Only Alphanumeric value is allowed.');
                //                    _retypePassword.focus();
                //                    return false;
                //                }
                // commented by supriya 16012025
//                if (_password.value != _retypePassword.value) {
//                    alert('Password and Re-Typed Password are not matching.');
//                    _retypePassword.focus();
//                    return false;
                //                }
                // end
                if (_role.selectedIndex == -1 || _role.selectedIndex == 0) {
                    alert('Select Role.');
                    _role.focus();
                    return false;
                }
                if (ddlBranch.value == '0') {
                    alert('Select Branch.');
                    ddlBranch.focus();
                    return false;
                }
            }
            return true;
        }
        function PasswordChanged(field) {

            var span = document.getElementById("PasswordStrength");
            span.innerHTML = CheckPassword(field.value);

        }


        function CheckPassword(password) {

            var strength = new Array();

            strength[0] = "Blank";

            strength[1] = "Very Weak";

            strength[2] = "Weak";

            strength[3] = "Medium";

            strength[4] = "Strong";

            strength[5] = "Very Strong";



            var score = 1;

            if ((password.length == 0))
                return strength[0];

            if (password.length < 2)

                return strength[1];

            if (password.length < 4)

                return strength[2];

            if (password.length >= 4)

                score++;

            if (password.length >= 8)

                score++;

            if (password.length >= 12)

                score++;

            if (password.match(/\d+/))

                score++;

            if (password.match(/[a-z]/) &&

                password.match(/[A-Z]/))

                score++;

            if (password.match(/.[@,#,$,%,^,&,*,?,_,~,-,£,(,)]/))

                score++;



            return strength[score];

        }

        function validatePaswd() {
        // commented by supriya 16012025
//            //            var RegExpres = /^[a-z0-9 ]+$/i;
//            var _password = document.getElementById('txtPassword');
//            var _retypePassword = document.getElementById('txtReTypePassword');
//            var _newpassword = document.getElementById('hdnpswd');

//            if (_password.value == '') {
//                alert('Enter Password.');
//                _password.focus();
//                return false;
//            }
//            //            if (RegExpres.test(trimAll(_password.value)) == false) {
//            //                alert('Only Alphanumeric value is allowed.');
//            //                _password.focus();
//            //                return false;
//            //            }
//            //            else {
//            if (_password.value.length < 8) {
//                alert('Enter minimum 8 characters in Password.');
//                _password.focus();
//                return false;
//            }
//            //            }

//            if (_password.value == _newpassword.value) {
//                alert('Password cannot be same as previous password.');
//                _password.focus();
//                return false;
//            }
//            if (_retypePassword.value == '') {
//                alert('Enter Re-Type Password.');
//                _retypePassword.focus();
//                return false;
//            }

//            //            if (RegExpres.test(trimAll(_retypePassword.value)) == false) {
//            //                alert('Only Alphanumeric value is allowed.');
//            //                _retypePassword.focus();
//            //                return false;
//            //            }

//            if (_password.value != _retypePassword.value) {
//                alert('Password and Re-Typed Password are not matching.');
//                _retypePassword.focus();
//                return false;
//            }

            //            return true;
            // end
        }

    </script>
    <style type="text/css">
        .style1
        {
            width: 50%;
        }
        .style2
        {
            width: 39%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnSave">
    <asp:scriptmanager id="ScriptManagerMain" runat="server">
    </asp:scriptmanager>
    <%--<asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:updatepanel id="UpdatePanelMain" runat="server" updatemode="conditional">
                <triggers>
                    <asp:PostBackTrigger ControlID="btnBack" />
                    <asp:PostBackTrigger ControlID="btnCancel" />
                </triggers>
                <contenttemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" valign="bottom" class="style2">
                                <asp:Label ID="lblHeader" class="pageLabel" Text="User Creation" 
                                        runat="server" Font-Bold="True"></asp:Label>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" OnClick="btnBack_Click"
                                    ToolTip="Back" TabIndex="10"/>
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
                            <td align="left" style="border: 1px solid #49A3FF; " valign="top" 
                                class="style2">
                                <table cellspacing="0" cellpadding="0" border="0" width="450px" style="line-height: 150%">
                                    <tr>
                                        <td align="right" nowrap="nowrap">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left" nowrap="nowrap">
                                            &nbsp;<asp:DropDownList runat="server" ID="ddlBranch" CssClass="dropdownList" TabIndex="1">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap="nowrap">
                                            <span class="mandatoryField">*</span><span class="elementLabel">User Name :</span>
                                        </td>
                                        <td align="left" nowrap="nowrap">
                                            &nbsp;<asp:TextBox ID="txtUserName" runat="server" CssClass="textBox" MaxLength="20"
                                                Width="200px" TabIndex="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <%--====== commented by Supriya 16/01/2025 (for ad login) ===================--%>
                                    <%--<tr>
                                        <td align="right" nowrap="nowrap">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Password :</span>
                                        </td>
                                        <td align="left" nowrap="nowrap">
                                            &nbsp;<asp:TextBox ID="txtPassword" runat="server" CssClass="textBox" MaxLength="15"
                                                Width="150px" TextMode="Password" TabIndex="3"></asp:TextBox>
                                            <span id="PasswordStrength"></span>
                                        </td>
                                    </tr>--%>
                                    <%--<tr>
                                        <td align="right" nowrap="nowrap">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Re-Type Password :</span>
                                        </td>
                                        <td align="left" nowrap="nowrap">
                                            &nbsp;<asp:TextBox ID="txtReTypePassword" runat="server" CssClass="textBox" MaxLength="15"
                                                Width="150px" TextMode="Password" TabIndex="4"></asp:TextBox>
                                                <asp:Label ID="lblPassMatch" runat="server" CssClass="mandatoryField"></asp:Label>
                                        </td>
                                    </tr>--%>
                                    <%--====== end ===================--%>
                                    <tr>
                                        <td align="right" nowrap="nowrap">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Role :</span>
                                        </td>
                                        <td align="left" nowrap="nowrap">
                                            &nbsp;<asp:DropDownList ID="ddlRole" runat="server" CssClass="dropdownList" TabIndex="5">
                                                <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                                <asp:ListItem Value="Supervisor" Text="Supervisor"></asp:ListItem>
                                                <asp:ListItem Value="User" Text="User"></asp:ListItem>
                                                <asp:ListItem Value="OIC" Text="OIC"></asp:ListItem>
                                                <asp:ListItem Value="Admin" Text="Admin"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap="nowrap">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Status :</span>
                                        </td>
                                        <td align="left" nowrap="nowrap">
                                            &nbsp;<asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropdownList" TabIndex="6">
                                                <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="In-Active"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap="nowrap">
                                        </td>
                                        <td align="left" style="padding-top: 10px; padding-bottom: 10px" nowrap>
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                OnClick="btnSave_Click" ToolTip="Save" TabIndex="7" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                OnClick="btnCancel_Click" ToolTip="Cancel" TabIndex="8" />
                                            <asp:Button ID="btnChangePaswd" runat="server" Text="Change Password" Width="120"
                                                CssClass="buttonDefault" OnClick="btnChangePaswd_Click" ToolTip="Change Password"
                                                TabIndex="9" Visible="false" /><%--added Visible="false" by supriya 16012025--%>
                                                <asp:Button ID="btnConfirm" runat="server" OnClick="btnConfirm_Click"
                                                            Style="visibility: hidden" />
                                            <input type="hidden" id="hdntype" runat="server" />
                                            <input type="hidden" id="hdnpswd" runat="server" />                                            
                                            <%--Audit Trail--%>
                                            <input type="hidden" id="hdnRole" runat="server" />
                                            <input type="hidden" id="hdnStatus" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="left" style="border: 1px solid #49A3FF;" valign="top" class="style1">
                            <table width="100%">
                            <tr>
                            <td align="left">
                            &nbsp;&nbsp;&nbsp;<span class="mandatoryField" ></span><span class="elementLabel" style="font-size:larger;"><b>User Creation Steps</b></span>
                            </td>
                            </tr>
                            <tr>
                            <td align="left">
                            
                            </td>
                            </tr>
                            <tr>
                            <td align="left">
                            <span class="mandatoryField" style="font-size:small;">1.</span><span class="mandatoryField" style="font-size:large;"> Create New User.</span>
                            </td>
                            </tr>
                            <tr>
                            <td align="left">
                            <span class="mandatoryField" style="font-size:small;">2.</span><span class="mandatoryField" style="font-size:large;"> Assign Role.</span>
                            </td>
                            </tr>
                            <tr>
                            <td align="left">
                            <span class="mandatoryField" style="font-size:small;">3.</span><span class="mandatoryField" style="font-size:large;"> Provide Access Control.</span>
                            </td>
                            </tr>
                            </table>
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
