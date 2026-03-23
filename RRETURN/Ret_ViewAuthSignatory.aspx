<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ret_ViewAuthSignatory.aspx.cs"
    Inherits="RRETURN_Ret_AddEditAuthSignatory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script type="text/javascript" language="javascript">
        function validate() {
            var branch = document.getElementById('ddlBranch').value;
            if (branch == '0') {
                alert('Select Branch.');
                document.getElementById('ddlBranch').focus();
                return false;
            }
        }       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc2:Menu ID="Menu1" runat="server" />
        <br />        
        <asp:ScriptManager ID="ScriptManagerMain" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAdd" />
            </Triggers>
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel"><b>Authorised Signatory Master View</b></span>
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
                        <td width="15%" align="left" nowrap>
                            <span class="elementLabel">Branch :</span><asp:DropDownList ID="ddlBranch" CssClass="dropdownList"
                                AutoPostBack="true" runat="server" TabIndex="1" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%; height: 21px;" valign="top" colspan="2">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                    <tr id="rowGrid" runat="server">
                        <td align="left" style="width: 100%" valign="top" colspan="2">
                            <asp:GridView ID="GridViewAuthorisedSignatoryList" runat="server" AutoGenerateColumns="False"
                                Width="75%" AllowPaging="True" OnRowCommand="GridViewAuthorisedSignatoryList_RowCommand"
                                OnRowDataBound="GridViewAuthorisedSignatoryList_RowDataBound">
                                <PagerSettings Visible="false" />
                                <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle" CssClass="gridItem" />
                                <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Middle" CssClass="gridHeader"/>
                                <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                    CssClass="gridAlternateItem" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Branch" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBranch" runat="server" CssClass="elementLabel"
                                                Text='<%# Eval("BranchName") %>'></asp:Label>
                                        </ItemTemplate>                                        
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sr No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsrno" runat="server" CssClass="elementLabel"
                                                Text='<%# Eval("SrNo") %>'></asp:Label>
                                        </ItemTemplate>                                        
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Authorised Signatory Name" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" CssClass="elementLabel"
                                                Text='<%# Eval("Authorised_Signatory") %>'></asp:Label>
                                        </ItemTemplate>                                        
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesignation" runat="server" CssClass="elementLabel"
                                                Text='<%# Eval("Designation") %>'></asp:Label>
                                        </ItemTemplate>                                        
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email ID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmailID" runat="server" CssClass="elementLabel"
                                                Text='<%# Eval("Emailid") %>'></asp:Label>
                                        </ItemTemplate>                                        
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="5%" HeaderText="Delete"
                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# Eval("BranchName")+";"+Eval("SrNo") %>'
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
                                        <td align="left" style="width: 25%">
                                            &nbsp;Records per page:&nbsp;
                                            <asp:DropDownList ID="ddlrecordperpage" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlrecordperpage_SelectedIndexChanged">
                                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                                <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center" style="width: 50%" valign="top">
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
    </div>
    </form>
</body>
</html>
