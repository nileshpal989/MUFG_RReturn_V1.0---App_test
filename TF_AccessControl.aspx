<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_AccessControl.aspx.cs"
    Inherits="TF_AccessControl" %>

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
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script src="Scripts/Validations.js" language="javascript" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function validateSave() {
            if (document.getElementById('hdntype').value == "Pwsd") {
                return validatePaswd();
            }
            else {
                //    var RegExpres = /^[a-z0-9 ]+$/i;
                var _userName = document.getElementById('txtUserName');
                var _password = document.getElementById('txtPassword');
                var _retypePassword = document.getElementById('txtReTypePassword');
                var _role = document.getElementById('ddlRole');
                if (_userName.value == '') {
                    alert('Enter User Name.');
                    _userName.focus();
                    return false;
                }
                //               if (RegExpres.test(trimAll(_userName.value)) == false) {
                //                   alert('Only Alphanumeric value is allowed.');
                //                   _userName.focus();
                //                   return false;
                //               }
                if (_password.value == '') {
                    alert('Enter Password.');
                    _password.focus();
                    return false;
                }
                //               if (RegExpres.test(trimAll(_password.value)) == false) {
                //                   alert('Only Alphanumeric value is allowed.');
                //                   _password.focus();
                //                   return false;
                //               }
                //               else {
                if (_password.value.length < 8) {
                    alert('Enter minimum 8 characters in Password.');
                    _password.focus();
                    return false;
                }
                //               }
                ////This is to check whether new password and previous password are not same
                //if(document.getElementById('hdnpswd').value!='')
                //{
                //    if(document.getElementById('hdnpswd').value!=_password.value)
                //    {
                //        if(document.getElementById('hdnpswd').value==_password.value)
                //        {
                //            alert('Password cannot be same as previous password.');
                //            _password.focus();
                //            return false;
                //        }
                //    }
                //}
                if (_retypePassword.value == '') {
                    alert('Re-Type Password.');
                    _retypePassword.focus();
                    return false;
                }
                //               if (RegExpres.test(trimAll(_retypePassword.value)) == false) {
                //                   alert('Only Alphanumeric value is allowed.');
                //                   _retypePassword.focus();
                //                   return false;
                //               }
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
        function validatePaswd() {
            //        var RegExpres = /^[a-z0-9 ]+$/i;
            var _password = document.getElementById('txtPassword');
            var _retypePassword = document.getElementById('txtReTypePassword');
            var _newpassword = document.getElementById('hdnpswd');

            if (_password.value == '') {
                alert('Enter Password.');
                _password.focus();
                return false;
            }
            //           if (RegExpres.test(trimAll(_password.value)) == false) {
            //               alert('Only Alphanumeric value is allowed.');
            //               _password.focus();
            //               return false;
            //           }
            //           else {
            if (_password.value.length < 8) {
                alert('Enter minimum 8 characters in Password.');
                _password.focus();
                return false;
            }
            //   }
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
            //           if (RegExpres.test(trimAll(_retypePassword.value)) == false) {
            //               alert('Only Alphanumeric value is allowed.');
            //               _retypePassword.focus();
            //               return false;
            //           }
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
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
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
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel" style="font-weight: bold">Access Control</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%;" valign="top">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="right" width="10%">
                                <span class="elementLabel">User :</span>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlUser" runat="server" AutoPostBack="True" CssClass="dropdownList"
                                    TabIndex="1" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td align="right" width="10%">
                                <span class="elementLabel">Module :</span>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlModule" runat="server" AutoPostBack="True" CssClass="dropdownList"
                                    TabIndex="2" Width="200px" 
                                    onselectedindexchanged="ddlModule_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <div style="overflow: auto; width: 100%; height: 100%">
                            <tr>
                                <td>
                                </td>
                                <td align="left" height="400px">
                                    <asp:GridView ID="GridViewMenuList" runat="server" AutoGenerateColumns="False" PageSize="5"
                                        Width="65%">
                                        <AlternatingRowStyle CssClass="gridAlternateItem" Height="18px" HorizontalAlign="Left"
                                            VerticalAlign="Middle" Wrap="False" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr No.">
                                                <ItemTemplate>
                                                    <span class="elementLabel">
                                                        <%# ((GridViewRow)Container).RowIndex + 1%></span>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Menus">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMenu" runat="server" CssClass="elementLabel" Text='<%# Eval("MenuName") %>'></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allow/Deny" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%"
                                                ItemStyle-Width="10%">
                                                <HeaderTemplate>
                                                    <asp:CheckBox runat="server" ID="HeaderChkAllow" AutoPostBack="true" ToolTip="Select All"
                                                        Text="Allow/Deny" OnCheckedChanged="HeaderChkAllow_CheckedChanged" TabIndex="2" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="RowChkAllow" AutoPostBack="true" OnCheckedChanged="RowChkAllow_CheckedChanged"
                                                        TabIndex="3" />
                                                    <asp:Label ID="lblAccess" runat="server" CssClass="elementLabel" ForeColor="RED"
                                                        Text="Access Denied"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Module">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblModule" runat="server" CssClass="elementLabel" Text='<%# Eval("Module") %>'></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" ForeColor="#1A60A6" VerticalAlign="Top" />
                                        <RowStyle CssClass="gridItem" Height="18px" HorizontalAlign="Left" VerticalAlign="Top"
                                            Wrap="False" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault" ToolTip="Save"
                                        OnClick="btnSave_Click" TabIndex="4" />&nbsp
                                </td>
                            </tr>
                        </div>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>
