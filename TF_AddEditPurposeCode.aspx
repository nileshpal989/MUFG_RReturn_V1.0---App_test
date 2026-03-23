<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_AddEditPurposeCode.aspx.cs"
    Inherits="TF_AddEditPurposeCode" %>

<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script src="Scripts/Validations.js" language="javascript" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function validateSave() {
            var RegExpres = /^[a-z0-9 ]+$/i;
            var _purposeCode = document.getElementById('txtPurposeCode');
            var _description = document.getElementById('txtDescription');
            if (trimAll(_purposeCode.value) == '') {
                alert('Enter Purpose Code.');
                _purposeCode.focus();
                return false;
            }
            var a = document.getElementById('txtPurposeCode').value;
            if (a.charAt(0) != 'p' && a.charAt(0) != 'P' && a.charAt(0) != 's' && a.charAt(0) != 'S') {
                alert("Purpose Code must start with 'P' or 'S'.");
                _purposeCode.focus();
                return false;
            }
            if (trimAll(_description.value) == '') {
                alert('Enter Description.');
                _description.focus();
                return false;
            }
            //if(RegExpres.test(trimAll(_description.value)) == false)
            //{
            //alert('Only Alphanumeric value is allowed.');
            //_description.focus();
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
            <asp:updatepanel id="UpdatePanel1" runat="server">
                <triggers>
                        <asp:PostBackTrigger ControlID="btnBack" />
                        <asp:PostBackTrigger ControlID="btnCancel" />
                    </triggers>
                <contenttemplate>
                        <table cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="left" style="width: 50%" valign="bottom">
                                    <span class="pageLabel"><b>Purpose Code Master Details</b></span>
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
                                    <table cellspacing="0" cellpadding="0" border="0" width="600px" style="line-height: 150%">
                                        <tr style="height:30px">
                                            <td align="right" style="white-space:nowrap">
                                                <span class="mandatoryField">*</span><span class="elementLabel">Purpose Code :</span>
                                            </td>
                                            <td align="left" >
                                                &nbsp;<asp:TextBox ID="txtPurposeCode" style="text-transform:uppercase" runat="server" CssClass="textBox" MaxLength="5"
                                                    Width="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr style="height:auto">
                                            <td align="right" style="vertical-align:top">
                                                <span class="mandatoryField">*</span><span class="elementLabel">Description : </span>
                                            </td>
                                            <td align="left" style="white-space:nowrap">
                                            &nbsp;<asp:TextBox ID="txtDescription"  runat="server" CssClass="textBox" MaxLength="50" 
                                                    Width="500px" TextMode="MultiLine"  Height="50px" Columns="5" Rows="5" ></asp:TextBox>

                                                    
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 110px">
                                            </td>
                                            <td align="left" style="width: 300px; padding-top: 10px; padding-bottom: 10px">
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
