<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RET_ViewReturnData.aspx.cs"
    Inherits="RRETURN_RET_ViewReturnData" %>

<%@ Register Src="../Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
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
        function validate() {
            var ddlBranch = document.getElementById('ddlBranch');
            if (ddlBranch.value == "0") {
                alert('Enter Branch Name');
                ddlBranch.focus();
                return false;
            }
            var txtFromDate = document.getElementById('txtFromDate');
            if (txtFromDate.value == '') {
                alert('Enter From Date');
                txtFromDate.focus();
                return false;
            }
            var txtToDate = document.getElementById('txtToDate');
            if (txtToDate.value == '') {
                alert('Enter To Date');
                txtToDate.focus();
                return false;
            }
        }
                function changeBranchDesc() {
                  
                    var ddlBranch = document.getElementById('ddlBranch');
                    var lblAdcodeDesc = document.getElementById('lblAdcodeDesc');
                    if (ddlBranch.value != "0")
                        lblAdcodeDesc.innerHTML = ddlBranch.value;
                    else
                        lblAdcodeDesc.innerHTML = "";
                    ddlBranch.focus();
                    return true;
                }
        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            // alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }
        function ValidDates() {
            var fromdate = document.getElementById('txtFromDate');
            var toDate = document.getElementById('txtToDate');
            var day = fromdate.value.split("/")[0];
            var month = fromdate.value.split("/")[1];
            var year = fromdate.value.split("/")[2];
            var fromdateyyyy = fromdate.value.split("/")[2];
            var fromdatemm = fromdate.value.split("/")[1];
            var fromdatedd = fromdate.value.split("/")[0];
            var dt = new Date(year, month - 1, day);
            var today = new Date();
            if (fromdate.value == '') {
                alert('Select From Date.');
                document.getElementById('txtFromDate').focus();
                return false;
            }
            if ((fromdate.value.substring(0, 2) != '01') && (fromdate.value.substring(0, 2) != '16')) {
                alert('Invalid From Date.');
                document.getElementById('txtFromDate').focus();
                return false;
            }
            else {
                if (dt > today) {
                    alert("Invalid From Date.");
                    document.getElementById('txtFromDate').focus();
                    return false;
                }
                else {
                    if (fromdate.value.substring(0, 2) == '01') {
                        toDate.value = '15/' + fromdatemm + '/' + fromdateyyyy;
                        document.getElementById('txtToDate').focus();
                    }
                    else if (fromdate.value.substring(0, 2) == '16') {
                        var calDt = new Date(parseFloat(fromdateyyyy), parseFloat(fromdatemm), 0);
                        toDate.value = calDt.format("dd/MM/yyyy");
                        document.getElementById('txtToDate').focus();
                    }
                }
            }
            document.getElementById('btnfillgrid').click();
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
                            <td align="left" valign="bottom" colspan="2">
                                <span class="pageLabel" style="font-weight: bold;">R Return Entry View</span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="buttonDefault" ToolTip="Add New Record"
                                    OnClick="btnAdd_Click" TabIndex="4" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="3">
                                <hr />
                                <input type="hidden" id="hdnBranchCode" runat="server" />
                                <input type="hidden" id="hdnUserRole" runat="server" />
                                <asp:Button ID="btnfillgrid" Style="display: none;" runat="server" OnClick="btnfillgrid_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td width="05%" align="right" nowrap>
                                <span class="elementLabel">Branch :</span>
                            </td>
                            <td width="05%" align="left" nowrap>
                                &nbsp;
                                <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" runat="server" TabIndex="1"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                </asp:DropDownList>
                                &nbsp; <span class="elementLabel">AD Code :</span>&nbsp;&nbsp;
                                <asp:Label ID="lblAdcodeDesc" runat="server" Style="font-size: small" Width="50px"></asp:Label>
                                &nbsp;
                                <asp:Label ID="lblBankname" runat="server" Style="font-size: small" CssClass="elementLabel"
                                    Width="100px"></asp:Label>
                                &nbsp;
                            </td>
                            <td align="right" style="width: 100%;" valign="top">
                                <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                    CssClass="textBox" MaxLength="40" Width="180px" TabIndex="5"></asp:TextBox>
                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault"
                                    ToolTip="Search" OnClick="btnSearch_Click" TabIndex="6" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="5%" nowrap>
                                <span class="mandatoryField">*</span><span class="elementLabel">From Date :</span>
                            </td>
                            <td align="left" style="width: 5%" nowrap colspan="2">
                                &nbsp;
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                    Width="80px" TabIndex="2"></asp:TextBox>
                                <asp:Button ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled"
                                    TabIndex="-1" />
                                <ajaxToolkit:MaskedEditExtender ID="mdfdate" Mask="99/99/9999" MaskType="Date" runat="server"
                                    TargetControlID="txtFromDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                    ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                </ajaxToolkit:MaskedEditExtender>
                                <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtFromDate" PopupButtonID="btncalendar_FromDate">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdfdate"
                                    ValidationGroup="dtVal" ControlToValidate="txtFromDate" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                </ajaxToolkit:MaskedEditValidator>
                                &nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To Date :</span>
                                &nbsp;
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" Width="80px"
                                    TabIndex="3" AutoPostBack="true" OnTextChanged="txtToDate_TextChanged"></asp:TextBox>
                                <asp:Button ID="btncalendar_ToDate" runat="server" CssClass="btncalendar_enabled"
                                    Visible="true" />
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtToDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                    ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                </ajaxToolkit:MaskedEditExtender>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtToDate" PopupButtonID="btncalendar_ToDate">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                    ValidationGroup="dtVal" ControlToValidate="txtToDate" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                </ajaxToolkit:MaskedEditValidator>
                            </td>
                        </tr>
                        <tr>
                            <%--<td width="05%" nowrap align="right">
<span class="elementLabel">Sr No :</span>
</td>
<td width="10%" align="left"> &nbsp;
<asp:TextBox ID="txtSrNo" runat="server" CssClass="textBox" Width="50px" 
AutoPostBack="true" TabIndex="4" MaxLength="4"></asp:TextBox>
</td>--%>
                        </tr>
                        <tr>
                            <td align="left" style="width: 60%; height: 21px;" valign="top" colspan="3">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr id="rowGrid" runat="server">
                            <td align="left" style="width: 60%" valign="top" colspan="3">
                                <asp:GridView ID="GridViewRReturnEntry" runat="server" AutoGenerateColumns="false"
                                    Width="55%" GridLines="Both" AllowPaging="true" OnRowCommand="GridViewRReturnEntry_RowCommand"
                                    OnRowDataBound="GridViewRReturnEntry_RowDataBound">
                                    <PagerSettings Visible="false" />
                                    <RowStyle Wrap="false" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle" CssClass="gridItem" />
                                    <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Middle" CssClass="gridHeader" />
                                    <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                        CssClass="gridAlternateItem" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr No" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="04%" ItemStyle-Width="04%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SRNO") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="04%" />
                                            <ItemStyle HorizontalAlign="Center" Width="04%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Document No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="09%" ItemStyle-Width="09%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocumentNo" runat="server" Text='<%# Eval("DOCNO") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="09%" />
                                            <ItemStyle HorizontalAlign="Left" Width="09%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Doc Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="04%" ItemStyle-Width="04%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocdate" runat="server" Text='<%# Eval("TRANSACTION_DT","{00:dd/MM/yyyy}") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="04%" />
                                            <ItemStyle HorizontalAlign="Left" Width="04%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purpose" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="03%" ItemStyle-Width="03%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPurpose" runat="server" Text='<%# Eval("PURPOSEID") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="03%" />
                                            <ItemStyle HorizontalAlign="Center" Width="03%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Curr" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="02%" ItemStyle-Width="02%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCurrency" runat="server" Text='<%# Eval("CURR") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="02%" />
                                            <ItemStyle HorizontalAlign="Center" Width="02%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FC Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="08%" ItemStyle-Width="08%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFCAmount" runat="server" Text='<%# Eval("AMOUNT","{0:0.00}") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" Width="08%" />
                                            <ItemStyle HorizontalAlign="Right" Width="08%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="INR Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="08%" ItemStyle-Width="08%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblINRAmount" runat="server" Text='<%# Eval("INR_AMOUNT","{0:0.00}") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" Width="08%" />
                                            <ItemStyle HorizontalAlign="Right" Width="08%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="N/V" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="02%" ItemStyle-Width="02%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNostro" runat="server" Text='<%# Eval("VASTRO_AC") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>                                            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="04%" ItemStyle-Width="04%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblType" runat="server" Text='<%# Eval("MOD_TYPE") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="04%" />
                                            <ItemStyle HorizontalAlign="Center" Width="04%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SCH" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="03%" ItemStyle-Width="03%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSCH" runat="server" Text='<%# Eval("SCHEDULENO") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="03%" />
                                            <ItemStyle HorizontalAlign="Center" Width="03%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="05%" HeaderText="Delete"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="05%">
                                            <ItemTemplate>
                                                <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# Eval("SRNO") %>' CommandName="RemoveRecord"
                                                    Text="Delete" ToolTip="Delete Record" BackColor="#007DFB" Width="100%" Font-Bold="true" ForeColor="White" Font-Size="11px"/>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="05%" />
                                            <ItemStyle HorizontalAlign="Center" Width="05%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr id="rowPager" runat="server">
                            <td align="center" style="width: 100%" valign="top" colspan="3" class="gridHeader">
                                <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="left" width="25%">
                                                &nbsp;Records Per Page :&nbsp;
                                                <asp:DropDownList ID="ddlrecordperpage" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlrecordperpage_SelectedIndexChanged">
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
