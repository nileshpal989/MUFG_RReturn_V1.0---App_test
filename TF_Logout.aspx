<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_Logout.aspx.cs" Inherits="TF_Logout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-Trade Finance System</title>
     <link id="Link1" runat="server" rel="shortcut icon" href="Images/favicon.ico" type="image/x-icon"/>
    <link id="Link2" runat="server" rel="icon" href="Images/favicon.ico" type="image/ico"/>
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="Style/style_new.css"  rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
        <div>
            <center>
                <br />
                <br />
                <br />
                <br />
                <br />
                <table cellspacing="0" cellpadding="0" width="800px" border="1px solid #666666">
                    <tr>
                        <td align="center" style="width: 100%; padding-top: 10px; padding-bottom: 20px">
                            <h2>
                                Thanks for using LMCC-Trade Finance System</h2>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td align="center" style="padding-top: 10px;" colspan="2">
                                        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="login_bt" ToolTip="Login"
                                            OnClick="btnLogin_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </center>
        </div>
        <div class="footer">
            <span class="h2">&copy;&nbsp;2013 Lateral Management Computer Consultants</span></div>
    </form>
</body>
</html>
