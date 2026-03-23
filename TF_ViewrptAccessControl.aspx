<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_ViewrptAccessControl.aspx.cs"
    Inherits="TF_ViewrptAccessControl" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
</head>
<body>
    <form id="formrdata" runat="server" autocomplete="off">
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
            <br />
            <table cellspacing="0" border="0" width="100%">
                <tr>
                    <td align="left" style="width: 50%" valign="bottom">
                        <asp:Label ID="PageHeader" CssClass="pageLabel" runat="server" Style="font-weight: bold"></asp:Label>
                    </td>
                    <td align="right" style="width: 50%" valign="top">
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" OnClick="btnBack_Click"
                            ToolTip="Back" />
                    </td>
                </tr>
            </table>
            <br />
            <hr />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="500px" ShowParameterPrompts="False"
                        Width="100%" ShowPrintButton="true" BackColor="AliceBlue">
                    </rsweb:ReportViewer>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>
