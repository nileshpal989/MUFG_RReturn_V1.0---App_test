<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/TF_ModuleSelection.aspx.cs"
    Inherits="TF_ModuleSelection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>LMCC Trade Finance System</title>
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
</head>
<body>
    <form id="form2" runat="server" autocomplete="off">
    <div id="body">
        <div id="container-signout">
            <div id="container2-signout">
                <asp:Button ID="signout" runat="server" CssClass="signout_bt" Text="Logout" OnClick="signout_Click" />
            </div>
        </div>
        <div id="container-header">
            <div id="container2-header">
                <div id="logo">
                </div>
                <div id="header-info">
                    <asp:Label ID="lblUserName" runat="server"></asp:Label><asp:Label ID="lblRole" runat="server"></asp:Label></div>
                <div id="header-Date">
                    <asp:Label ID="lblTime" runat="server" CssClass="elementLabel"></asp:Label>
                </div>
            </div>
            <div id="buttonrow">
                <div id="container1-bt">
                    <asp:Button ID="Button1" runat="server" CssClass="ret_bt" TabIndex="6" OnClick="btnRreturn_Click" />
                </div>
                <div id="container1-bt">
                    
                </div>
                <div id="container2-bt">
                    
                </div>
            </div>
        </div>        
        <div id="buttonrow3">
            <div id="container1-bt">
            </div>
            <div id="container1-bt">
            </div>
            <div id="container2-bt">
            </div>
        </div>
        <div id="buttonrow4">
            <div id="container1-bt">
            </div>
            <div id="container1-bt">
            </div>
            <div id="container2-bt">
            </div>
        </div>
        <div class="footer">
            <span class="h2">&copy;&nbsp;2013 Lateral Management Computer Consultants</span></div>
    </div>
    <%-- <div class="footer">
            <span class="h2">&copy;&nbsp;2013 Lateral Management Computer Consultants</span></div>
    </div>--%>
    </form>
</body>
</html>
