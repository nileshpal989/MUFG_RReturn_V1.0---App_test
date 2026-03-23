<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_ViewCountryMaster.aspx.cs"
    Inherits="TF_ViewCountryMaster" %>

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
    <script type="text/javascript" language="javascript">
        function validateSearch() {
            var _txtvalue = document.getElementById('txtSearch').value;
            _txtvalue = _txtvalue.replace(/'&lt;'/, "");
            _txtvalue = _txtvalue.replace(/'&gt;'/, "");
            if (_txtvalue.indexOf('<!') != -1 || _txtvalue.indexOf('>!') != -1 || _txtvalue.indexOf('!') != -1 || _txtvalue.indexOf('<') != -1 || _txtvalue.indexOf('>') != -1 || _txtvalue.indexOf('|') != -1) {
                alert('!, |, <, and > are not allowed.');
                document.getElementById('txtSearch').value = _txtvalue;
                return false;
            }
            else
                return true;
        }

        function submitForm(event) {
            if (event.keyCode == '13') {
                if (validateSearch() == true)
                    __doPostBack('btnSearch', '');
                else
                    return false;
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />            
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnAdd" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><b>Country Master View<b></span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="buttonDefault" ToolTip="Add New Record"
                                    OnClick="btnAdd_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr align="right">
                            <td align="right" style="width: 100%;" valign="top" colspan="2">
                                <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                    CssClass="textBox" MaxLength="40" Width="180px"></asp:TextBox>
                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault"
                                    ToolTip="Search" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; height: 21px;" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr id="rowGrid" runat="server">
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <asp:GridView ID="GridViewCountry" runat="server" AutoGenerateColumns="false" Width="40%"
                                    GridLines="Both" AllowPaging="true" OnRowCommand="GridViewCountry_RowCommand"
                                    OnRowDataBound="GridViewCountry_RowDataBound">
                                    <PagerSettings Visible="false" />
                                    <RowStyle Wrap="false" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle" CssClass="gridItem" />
                                    <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Middle" CssClass="gridHeader" />
                                    <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                        CssClass="gridAlternateItem" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Country ID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="6%" ItemStyle-Width="6%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountryID" runat="server" Text='<%# Eval("CountryID") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>                                            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Country Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="30%" ItemStyle-Width="30%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountryName" runat="server" Text='<%# Eval("CountryName") %>'  CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>                                            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="4%" ItemStyle-Width="4%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("C_Status")%>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="4%" HeaderText="Delete"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="4%">
                                            <ItemTemplate>
                                                <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# Eval("CountryID") %>'
                                                    CommandName="RemoveRecord" Text="Delete" ToolTip="Delete Record" BackColor="#007DFB" Width="100%" Font-Bold="true" ForeColor="White" Font-Size="11px"/>
                                            </ItemTemplate>                                            
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr id="rowPager" runat="server">
                            <td align="center" style="width: 100%" valign="top" colspan="2" class="gridHeader">
                                <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="left" width="25%">
                                                &nbsp;Records Per Page :&nbsp;
                                                <asp:DropDownList ID="ddlrecordpage" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlrecordpage_SelectedIndexChanged">
                                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                    <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                    <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                                    <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" valign="top" width="50%">
                                                <asp:Button ID="btnnavfirst" runat="server" Text="First" ToolTip="First" OnClick="btnnavfirst_Click" />
                                                <asp:Button ID="btnnavpre" runat="server" Text="Prev" ToolTip="Previous" OnClick="btnnavpre_Click" />
                                                <asp:Button ID="btnnavnext" runat="server" Text="Next" ToolTip="Next" OnClick="btnnavnext_Click" />
                                                <asp:Button ID="btnnavlast" runat="server" Text="Last" ToolTip="Last" OnClick="btnnavlast_Click" />
                                            </td>
                                            <td align="right" style="width: 25%;">
                                                &nbsp;<asp:Label ID="lblpageno" runat="server"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblrecordno" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
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
