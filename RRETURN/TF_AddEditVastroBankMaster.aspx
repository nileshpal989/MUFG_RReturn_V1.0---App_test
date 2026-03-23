<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_AddEditVastroBankMaster.aspx.cs"
    Inherits="TF_AddEditVastroBankMaster" %>

<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript">
        function toUpper_Case() {
            document.getElementById('txtCountrycode').value = (document.getElementById('txtCountrycode').value).toUpperCase();
        }
        function OpenCountryList(hNo) {
            open_popup('../TF_CountryLookUp1.aspx?PageHeader=Country Lookup&hNo=' + hNo, 450, 400, 'CountryList');
            return false;
        }
        function selectCountry(selectedID, hNo) {
            var id = selectedID;
            document.getElementById('hdnCountry').value = id;
            document.getElementById('btnCountry').click();
        }
        function OpenVastroList(e) {
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                txtCountrycode = document.getElementById('txtCountrycode').value;
                open_popup('TF_VastroBankCodeLookUp.aspx?PageHeader=Vostro Bank Code Lookup&CountryID=' + txtCountrycode, 450, 400, 'VastroBankCodeList');
                return false;
            }
        }
        function selectVastroBankCode(selectedID) {
            var id = selectedID;
            //    document.getElementById('hdnCountryHelpNo').value = hNo;
            document.getElementById('hdnBankCode').value = id;
            document.getElementById('btnBankCode').click();
        }
        function OpenOverseasBankList(e) {
            var keycode;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                var txtVosterBankID = document.getElementById('txtVosterBankID');
                open_popup('INW/INW_OverseasBankLookup.aspx?PageHeader=Overseas Bank LookUp&bankID=' + txtVosterBankID.value, 450, 650, 'OverseasBankList');
                return false;
            }
        }
        function selectOverseasBank(selectedID) {
            var id = selectedID;
            document.getElementById('hdnOverseasId').value = id;
            document.getElementById('btnOverseasBank').click();
        }
        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            //  alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }
        function validateSave() {
            var RegExpres = /^[a-z0-9 ]+$/i;
            var _CountryCode = document.getElementById('txtCountrycode');
            var _BankName = document.getElementById('txtBankName');
            var _BankCode = document.getElementById('txtBankCode');
            var _txtVostro = document.getElementById('txtVosterBankID');
            if (_CountryCode.value == '') {
                alert('Enter Country Code');
                _CountryCode.focus();
                return false;
            }
            if (_BankCode.value == '') {
                alert('Enter Bank Code.');
                _BankCode.focus();
                return false;
            }
            if (_BankName.value == '') {
                alert('Enter Bank Name.');
                _BankName.focus();
                return false;
            }
            return true;
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 106%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnSave">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnBack" />
                    <asp:PostBackTrigger ControlID="btnCancel" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" valign="bottom" class="style1">
                                <span class="pageLabel"><b>Vostro Bank Master Details<b></span>
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
                            <td align="right" valign="top" class="style1" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                                <%-------------------------  hidden fields  --------------------------------%>
                                <input type="hidden" id="hdnCountry" runat="server" />
                                <asp:Button ID="btnCountry" Style="display: none;" runat="server" OnClick="btnCountry_Click" />
                                <input type="hidden" id="hdnBankCode" runat="server" />
                                <asp:Button ID="btnBankCode" Style="display: none;" runat="server" OnClick="btnBankCode_Click" />
                                <input type="hidden" id="hdnOverseasId" runat="server" />
                                <asp:Button ID="btnOverseasBank" Style="display: none;" runat="server" OnClick="btnOverseasBank_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="border: 1px solid #49A3FF" valign="top" class="style1" colspan="2">
                                <table cellspacing="0" cellpadding="0" border="0" width="600px" style="line-height: 150%">
                                    <tr>
                                        <td align="right" style="width: 200px" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">Country Of Vostro A/C
                                                Holder :</span>
                                        </td>
                                        <td align="left" style="width: 400px" nowrap>
                                            &nbsp;<asp:TextBox ID="txtCountrycode" runat="server" CssClass="textBox" MaxLength="2"
                                                Width="20" TabIndex="1" Style="text-transform: uppercase" OnTextChanged="txtCountrycode_TextChanged"
                                                AutoPostBack="true"></asp:TextBox>
                                            <asp:Button ID="btnCountryList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" /><asp:Label
                                                ID="lblCountryDesc" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">Bank Code :</span>
                                        </td>
                                        <td align="left" style="width: 400px" nowrap>
                                            &nbsp;<asp:TextBox ID="txtBankCode" runat="server" CssClass="textBox" MaxLength="2"
                                                Width="20" TabIndex="2" OnTextChanged="txtBankCode_TextChanged" AutoPostBack="true"></asp:TextBox>                                            
                                            <asp:Button ID="btnBankList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <tr>
                                            <td align="right" nowrap style="width: 200px">
                                                <span class="mandatoryField">*</span><span class="elementLabel">Vosrto Bank Name :</span>
                                            </td>
                                            <td align="left" nowrap style="width: 400px">
                                                &nbsp;<asp:TextBox ID="txtBankName" Style="text-transform: capitalize" runat="server"
                                                    CssClass="textBox" MaxLength="50" TabIndex="3" Width="350px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 110px">
                                            </td>
                                            <td align="left" style="width: 290px; padding-top: 10px; padding-bottom: 10px">
                                                &nbsp;<asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" OnClick="btnSave_Click"
                                                    TabIndex="4" Text="Save" ToolTip="Save" />
                                                <asp:Button ID="btnCancel" runat="server" CssClass="buttonDefault" OnClick="btnCancel_Click"
                                                    TabIndex="5" Text="Cancel" ToolTip="Cancel" />
                                            </td>
                                        </tr>
                                    </tr>
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
