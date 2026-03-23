<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_AddEditCountryMaster.aspx.cs"
    Inherits="TF_AddEditCountryMaster" %>

<%@ Register Src="Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">
        function validateSave() {

            var cid = document.getElementById('txtCountryID').value;
            var cname = document.getElementById('txtCountryName').value;

            if (cid == '') {
                alert('Enter Country ID');
                document.getElementById('txtCountryID').focus();
                return false;
            }

            if (cname == '') {
                alert('Enter Country Name');
                document.getElementById('txtCountryName').focus();
                return false;
            }
        }

        function onlyChars(e) {
            var key = e.keyCode;
            var keychar = String.fromCharCode(key);
            var reg = new RegExp("[a-zA-Z ]");
            if (key != 13) {
                return reg.test(keychar);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnSave">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnBack" />
                    <asp:PostBackTrigger ControlID="btnCancel" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><strong>Country Master Details</strong></span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
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
                                <table cellspacing="0" cellpadding="0" border="0" width="600px" style="line-height: 150%">
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Country ID :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            &nbsp;<asp:TextBox ID="txtCountryID" runat="server" style="text-transform:uppercase" CssClass="textBox" MaxLength="2"
                                                Width="30px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Country Name :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            &nbsp;<asp:TextBox ID="txtCountryName" style="text-transform:uppercase" runat="server" CssClass="textBox" MaxLength="50"
                                                Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Status :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            &nbsp;<asp:RadioButton ID="rdbActive" runat="server" TabIndex="3" Text="Active" GroupName="rdbgrpStatus" Checked="true" class="elementLabel"/>
                                            &nbsp;<asp:RadioButton ID="rdbInActive" runat="server" TabIndex="3" Text="In-Active" GroupName="rdbgrpStatus" class="mandatoryField" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 400px; padding-top: 10px; padding-bottom: 10px">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                ToolTip="Save" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                ToolTip="Cancel" OnClick="btnCancel_Click" />
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
