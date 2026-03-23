<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_AddEditPortMaster.aspx.cs"
    Inherits="RBI_AddEditPortMaster" %>

<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script src="Scripts/Validations.js" language="javascript" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function RestrictSplChar() {
            //if (event.keyCode == 32 || event.keyCode == 33 || event.keyCode == 34 || event.keyCode == 35 || event.keyCode == 36 || event.keyCode == 37 || event.keyCode == 38) {
            //if (event.keyCode >= 32 && event.keyCode <= 47 || event.keyCode >= 91 && event.keyCode <= 96 || event.keyCode >= 58 && event.keyCode <= 64 || event.keyCode >= 123 && event.keyCode <= 126 || event.keyCode == 189) {
            if ((event.keyCode < 65 || event.keyCode > 90) && (event.keyCode < 96 || event.keyCode > 105) && event.keyCode != 189 && event.keyCode != 191 && (event.keyCode < 48 || event.keyCode > 57) && event.keyCode != 46 && event.keyCode != 8 && event.keyCode != 109) {
                event.returnValue = false;
                return false;
            }
        }

        //        function validate_Number(evnt) {
        //            var charCode = (evnt.which) ? evnt.which : event.keyCode;
        //            if ((charCode < 65 || charCode > 90) && (charCode < 96 || charCode > 105) && charCode != 189 && charCode != 191 && (charCode < 48 || charCode > 57) && charCode != 46 && charCode != 8 && charCode!=109)
        //                return false;
        //            else
        //                return true;
        //        }

        function validateSave() {
            var RegExpres = /^[a-z0-9 ]+$/i;
            var _portID = document.getElementById('txtPortID');
            var _portName = document.getElementById('txtPortName');
            if (trimAll(_portID.value) == '') {
                alert('Enter Port ID.');
                _portID.focus();
                return false;
            }
            if (RegExpres.test(trimAll(_portID.value)) == false) {
                alert('Only Alphanumeric value is allowed.');
                _portID.focus();
                return false;
            }
            if (trimAll(_portName.value) == '') {
                alert('Enter Port Name.');
                _portName.focus();
                return false;
            }
            //if(RegExpres.test(trimAll(_portName.value)) == false)
            //{
            //alert('Only Alphanumeric value is allowed.');
            //_portName.focus();
            //return false;
            //}
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnSave">
    <asp:scriptmanager id="ScriptManagerMain" runat="server">
    </asp:scriptmanager>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:updatepanel id="UpdatePanelMain" runat="server">
                <triggers>
                    <asp:PostBackTrigger ControlID="btnBack" />
                    <asp:PostBackTrigger ControlID="btnCancel" />
                </triggers>
                <contenttemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><b>Port Master Details</b></span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" OnClick="btnBack_Click"
                                    ToolTip="Back" />
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
                                <table cellspacing="0" cellpadding="0" border="0" width="400px" style="line-height: 150%">
                                    <tr>
                                        <td align="right" style="width: 110px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Port ID :</span>
                                        </td>
                                        <td align="left" style="width: 290px">
                                            &nbsp;<asp:TextBox ID="txtPortID" style="text-transform:uppercase" runat="server" CssClass="textBox" MaxLength="6"
                                                Width="60"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 110px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Name :</span>
                                        </td>
                                        <td align="left" style="width: 290px">
                                            &nbsp;<asp:TextBox ID="txtPortName" runat="server" style="text-transform:uppercase" CssClass="textBox" MaxLength="30"
                                                Width="250"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 110px">
                                        </td>
                                        <td align="left" style="width: 290px; padding-top: 10px; padding-bottom: 10px">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                OnClick="btnSave_Click" ToolTip="Save" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                OnClick="btnCancel_Click" ToolTip="Cancel" />
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
