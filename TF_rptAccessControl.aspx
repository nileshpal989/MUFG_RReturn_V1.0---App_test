<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_rptAccessControl.aspx.cs"
    Inherits="TF_rptAccessControl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript">

        function OpenUserList() {
            open_popup('TF_UserLookUp.aspx?PageHeader=User LookUp', 400, 200, 'UserList');

            return false;
        }
        function selectUser(Uname) {

            document.getElementById('txtUser').value = Uname;
            document.getElementById('txtUser').focus();
        }
        function validateSave() {


            var txt = document.getElementById('txtUser');
            var btnSelectedUser = document.getElementById('rdbSelectedUser');
            if (btnSelectedUser.checked == true) {
                if (txt.value == '') {
                    alert('Enter User Name .');
                    txt.focus();
                    return false;
                }
            }
        }


    </script>
    <style type="text/css">
        .style1
        {
            width: 50%;
        }
        .style2
        {
            width: 148px;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="Scripts/InitEndRequest.js"></script>
    <script src="Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
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
        <uc1:Menu ID="Menu1" runat="server" />
        <br />
        <br />
        <%--<asp:ScriptManager ID="ScriptManagerMain" runat="server">
            </asp:ScriptManager>

            <script language="javascript" type="text/javascript" src="Scripts/Enable_Disable_Opener.js"></script>
            <script src="Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>--%>
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
            </Triggers>
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" valign="bottom" class="style1">
                            <%--<span class="pageLabel"><b>Accessed Menu List</b></span>--%>
                            <asp:Label ID="PageHeader" CssClass="pageLabel" runat="server" Style="font-weight: bold"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                            <table cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="rdbAllUser" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                            GroupName="Data1" Checked="true" Text="All Users" OnCheckedChanged="rdbAllUser_CheckedChanged" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdbSelectedUser" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                            GroupName="Data1" Text="Selected User" OnCheckedChanged="rdbSelectedUser_CheckedChanged" />
                                    </td>
                                </tr>
                            </table>
                            <table id="Table2" runat="server" visible="false" border="0">
                                <tr>
                                    <td align="right" style="font-weight: bold; color: #000000; font-size: medium;" class="style2">
                                        <span class="elementLabel">User Name :</span>&nbsp;
                                    </td>
                                    <td align="left" width="0px" style="font-weight: bold; color: #000000;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:TextBox ID="txtUser" runat="server" CssClass="textBox" MaxLength="40" TabIndex="6"
                                            Width="186px"  OnTextChanged="txtUser_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        &nbsp;
                                        <asp:Button ID="btnBenfList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                        <asp:Label ID="lblUserName" runat="server" CssClass="elementLabel" Width="400px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table border="0">
                                <tr valign="bottom">
                                    <td align="right" style="width: 120px">
                                    </td>
                                    <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                        &nbsp;
                                        <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Generate"
                                            ToolTip="Generate" TabIndex="7" OnClick="btnSave_Click" />
                                    </td>
                                </tr>
                            </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
