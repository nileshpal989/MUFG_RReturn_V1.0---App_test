<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_RRETURN_PurposecodeLookup.aspx.cs"
    Inherits="Reports_RRETURNReports_TF_RRETURN_PurposecodeLookup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Sector Help</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/HelpCss.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen/">
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
            <ProgressTemplate>
                <div id="progressBackgroundMain" class="progressBackground">
                    <div id="processMessage" class="progressimageposition">
                        <img src="../../Images/ajax-loader.gif" style="border: 0px" alt="" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table align="left" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCustName" runat="server">Enter Purpose Code :</asp:Label>&nbsp;
                            <asp:TextBox ID="txtsearch" runat="server" AutoPostBack="True" OnTextChanged="txtsearch_TextChanged"
                                MaxLength="20"></asp:TextBox>&nbsp;
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../../Images/search.gif"
                                OnClick="ImageButton1_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:GridView ID="grdsearch" runat="server" AutoGenerateColumns="False" ondblclick="GetValues()"
                                OnRowCreated="grdsearch_RowCreated" Width="100%" CellPadding="4" PageSize="15"
                                Height="124px">
                                <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="top" CssClass="gridItem" />
                                <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                    CssClass="gridAlternateItem" />
                                <Columns>
                                    <asp:BoundField DataField="PURPOSEID" HeaderText="Purpose Code" ItemStyle-Width="10%"
                                        ItemStyle-HorizontalAlign="left">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="elementLabel" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="description" HeaderText="Description" ItemStyle-Width="20%">
                                        <ItemStyle Width="20%" />
                                    </asp:BoundField>
                                </Columns>
                                <SelectedRowStyle CssClass="grdselectback" BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
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
