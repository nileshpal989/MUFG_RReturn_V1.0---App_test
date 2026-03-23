<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_Log_Out.aspx.cs" Inherits="TF_Log_Out" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC - Trade Finance System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="Images/favicon.ico" type="image/ico" />
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript">
        function redirect() {
            window.location = 'TF_Logout.aspx';
        }
    </script>
</head>
<body onload="redirect();">
    <form id="form1" runat="server" autocomplete="off">
    <div>
        <uc1:Menu ID="Menu1" runat="server" Visible="false" />
    </div>
    </form>
</body>
</html>
