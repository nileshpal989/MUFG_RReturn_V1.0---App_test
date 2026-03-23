<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_PurposecodeLookup1.aspx.cs" Inherits="TF_PurposecodeLookup1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Sector Help</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/HelpCss.css" rel="stylesheet" type="text/css" />
    
    
        <script type="text/javascript">
            var SelectedRow = null;
            var SelectedRowIndex = null;
            var UpperBound = null;
            var LowerBound = null;



            window.onload = function () {

                UpperBound = parseInt('<%= this.grdsearch.Rows.Count %>') - 1;
                LowerBound = 0;
                SelectedRowIndex = -1;

                return false;
                //  var s = window.opener.document.getElementById('txtBrand').value;
                //  alert(s);
            }

            function SelectRow(CurrentRow, RowIndex) {
                if (SelectedRow == CurrentRow || RowIndex > UpperBound || RowIndex < LowerBound)
                    return;

                if (SelectedRow != null) {
                    SelectedRow.style.backgroundColor = SelectedRow.originalBackgroundColor;
                    SelectedRow.style.color = SelectedRow.originalForeColor;
                }

                if (CurrentRow != null) {
                    CurrentRow.originalBackgroundColor = CurrentRow.style.backgroundColor;
                    CurrentRow.originalForeColor = CurrentRow.style.color;
                    CurrentRow.style.backgroundColor = '#DCFC5C';
                    CurrentRow.style.color = 'Black';
                }

                SelectedRow = CurrentRow;
                SelectedRowIndex = RowIndex;
                setTimeout("SelectedRow.focus();", 0);
            }

            function RefreshParentPage() {
                //  var s = document.getElementById('TextBox2').value;
                //   alert(s);
                //  window.opener.document.getElementById('TextBox1').value=s;
                //  window.opener.sss();
                window.opener.document.getElementById('btnSearchCriteria').click();
                window.close();
                return false;
            }

            function GetValues() {
                //  alert('ssss');
                var table = document.getElementById('<%=grdsearch.ClientID %>');

                var Row = table.rows[SelectedRowIndex + 1];
                if (SelectedRowIndex >= 0) {
                    var CellValue1 = Row.cells[0].innerText;
                    var CellValue2 = Row.cells[1].innerText;
                    document.getElementById('txtcell1').value = CellValue1;
                    window.opener.document.getElementById('btnSearchCriteria').click();
                    window.close();
                }

                //    document.getElementById('txtsearch').value = CellValue1;
                // alert(CellValue1);
                // alert(CellValue2);
                // var s = window.opener.document.getElementById('txtBrand').value;
                //    alert('s');
                // window.opener.close();

                //        document.getElementById('txtcell1').value = CellValue1;
                //        document.getElementById('txtcell2').value = CellValue2;
                //  myWindow.opener.document.write("<p>This is the source window!</p>");
                //    window.opener.document.getElementById('btnSearchCriteria').click();
                //  window.parent.document.getElementById('btnExit').click();

                //window.opener.document.getElementById('btnExit').click();
                // alert('r');
                return false;
            }

            function SelectSibling(e) {
                var e = e ? e : window.event;
                var keyCode = e.which ? e.which : e.keyCode;

                if (keyCode == 40)
                    SelectRow(SelectedRow.nextSibling, SelectedRowIndex + 1);

                else if (keyCode == 38)
                    SelectRow(SelectedRow.previousSibling, SelectedRowIndex - 1);
                if (keyCode == 13) {
                    GetValues();

                }
                return false;

            }
            function sss() {
                // alert('sss');
                window.opener.sss();
                window.opener.document.getElementById('btnSearchCriteria').click();
                return false;
            }
            //    function SelectSibling(e) {
            //        var e = e ? e : window.event;
            //        var KeyCode = e.which ? e.which : e.keyCode;

            //        if (KeyCode == 40)
            //            SelectRow(SelectedRow.nextSibling, SelectedRowIndex + 1);
            //        else if (KeyCode == 38)
            //            SelectRow(SelectedRow.previousSibling, SelectedRowIndex - 1);
            //        if (keyCode == 13) {
            //        alert('enter');
            //            GetValues();

            //        }
            //        return false;
            //    }
            function stopRKey(evt) {
                var evt = (evt) ? evt : ((event) ? event : null);
                var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
                if ((evt.keyCode == 13) && (node.type == "text")) { document.getElementById('ImageButton1').click(); return false; }
            }

            document.onkeydown = stopRKey; 
    </script>
    <style type="text/css">
        .style1
        {
            width: 60px;
        }
        .style3
        {
            width: 210px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 155px;">
                            <asp:Label ID="lblCustName" runat="server">Enter Purpose Code : &nbsp;</asp:Label>
                        </td>
                        <td style="width: 200px">
                            <asp:TextBox ID="txtsearch" runat="server" Width="196px" AutoPostBack="True" OnTextChanged="txtsearch_TextChanged"
                                MaxLength="20"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="Images/search.gif" OnClick="ImageButton1_Click" />
                        </td>
                    </tr>
                    <tr>
                        
                        <td class="style3" colspan="3">
                            &nbsp;
                            <asp:GridView ID="grdsearch" runat="server" AutoGenerateColumns="False" ondblclick="GetValues()"
                                OnRowCreated="grdsearch_RowCreated" Width="470px" Height="70px" 
                                CellPadding="4" ForeColor="#333333" GridLines="None" Font-Size="9pt">
                                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" 
                                    Font-Size="Smaller" />
                                <RowStyle HorizontalAlign="Left" Height="18px" VerticalAlign="top" 
                                    CssClass="gridItem" BackColor="#FFFBD6" ForeColor="#333333" />
                                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle ForeColor="White" VerticalAlign="Top" CssClass="gridHeader" 
                                    BackColor="#990000" Font-Bold="True" />
                                <AlternatingRowStyle HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                    CssClass="gridAlternateItem" BackColor="White" />
                                <Columns>
                                    <%--<asp:BoundField DataField="CUST_ACCOUNT_NO" HeaderText="A/C NO." ItemStyle-Width="100px"
                                        ItemStyle-HorizontalAlign="Left" >
                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                    </asp:BoundField>--%>
                                    <asp:BoundField DataField="Purpose_Code" HeaderText="Purpose Code" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="left">
                                        <ItemStyle HorizontalAlign="left" Width="70px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="description" HeaderText="Description" 
                                        ItemStyle-Width="300px" >
                                    <ItemStyle Width="300px" />
                                    </asp:BoundField>
                                </Columns>
                                <SelectedRowStyle CssClass="grdselectback" BackColor="#FFCC66" Font-Bold="True" 
                                    ForeColor="Navy" />
                                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                <SortedDescendingHeaderStyle BackColor="#820000" />
                            </asp:GridView>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            &nbsp;
                        </td>
                        <td class="style3">
                            &nbsp;
                        </td>
                        <td>
                            <asp:HiddenField ID="txtcell1" runat="server" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
