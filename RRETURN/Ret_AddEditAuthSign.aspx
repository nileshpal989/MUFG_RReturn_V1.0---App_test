<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ret_AddEditAuthSign.aspx.cs"
    Inherits="RRETURN_Ret_AddEditAuthSign" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">
        function validateSave() {
            var type = document.getElementById('txtauthosign');
            if (type.value == '') {
                alert('Enter Authorised Signatory.');
                type.focus();
                return false;
            }
            // return true;
        }
        function checkEmail(textbox) {
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (textbox.value.trim() == "") {
                return false;
            }
            if (!filter.test(textbox.value)) {
                alert('Please provide a valid email address');
                textbox.focus();
                return false;
            }
        }
        //        function ConfirmDelete() {
        //            var ans = confirm('Do You want to delete this Record?');
        //            if (ans) {
        //                return true;
        //            }
        //            else
        //                return false;
        //        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <uc2:Menu ID="Menu1" runat="server" />
        <br />
        <br />
        <center>
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><b>Authorised Signatory Details<b></span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" TabIndex="6" CssClass="buttonDefault" ToolTip="Back"
                                    OnClick="btnBack_Click" />
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
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="2">
                                <table border="0" cellspacing="0" width="30%">
                                    <tr>
                                        <td align="right" width="10%" nowrap>
                                            <span class="elementLabel">Branch Name :</span>
                                        </td>
                                        <td align="left" nowrap width="10%">
                                            <asp:TextBox ID="txtBranchname" runat="server" Height="14px" Width="75px" Enabled="false"
                                                CssClass="textBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Sr No :</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtsrno" runat="server" Height="14px" Width="30px" Enabled="false"
                                                CssClass="textBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap>
                                            <span class="elementLabel">Authorised Signatory :</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtauthosign" runat="server" Height="14px" Width="250px" CssClass="textBox"
                                                TabIndex="1" onfocus="this.select()" MaxLength="40"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap>
                                            <span class="elementLabel">Designation :</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDesignation" runat="server" Height="14px" Width="250px" CssClass="textBox"
                                                TabIndex="2" onfocus="this.select()" MaxLength="40"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap>
                                            <span class="elementLabel">Email ID :</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEmailID" runat="server" Height="14px" Width="250px" CssClass="textBox"
                                                TabIndex="3" onfocus="this.select()" MaxLength="40"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="right" style="width: 30px">
                                        </td>
                                        <td align="left" style="width: 400px; padding-top: 10px; padding-bottom: 10px">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                ToolTip="Save" TabIndex="4" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                ToolTip="Cancel" TabIndex="5" OnClick="btnCancel_Click" />
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
</body>
</html>
