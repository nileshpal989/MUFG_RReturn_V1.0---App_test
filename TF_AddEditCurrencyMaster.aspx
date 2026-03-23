<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_AddEditCurrencyMaster.aspx.cs"
    Inherits="RBI_AddEditCurrencyMaster" %>

<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Validations.js" language="javascript" type="text/javascript"></script>
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">
        //        function validateSave() {
        //            var RegExpres = /^[a-z0-9 ]+$/i;
        //            var _cRecId = document.getElementById('txtCRecID');
        //            var _currencyID = document.getElementById('txtCurrencyID');
        //            var _description = document.getElementById('txtDescription');

        //            if (trimAll(_cRecId.value) == '') {
        //                alert('Enter Currency Rec. ID.');
        //                _cRecId.focus();
        //                return false;
        //            }
        //            if (RegExpres.test(trimAll(_cRecId.value)) == false) {
        //                alert('Only Alphanumeric value is allowed.');
        //                _cRecId.focus();
        //                return false;
        //            }
        //            if (trimAll(_currencyID.value) == '') {
        //                alert('Enter Currency ID.');
        //                _currencyID.focus();
        //                return false;
        //            }
        //            if (RegExpres.test(trimAll(_currencyID.value)) == false) {
        //                alert('Only Alphanumeric value is allowed.');
        //                _currencyID.focus();
        //                return false;
        //            }
        //            if (trimAll(_description.value) == '') {
        //                alert('Enter Description.');
        //                _description.focus();
        //                return false;
        //            }
        //            //if(RegExpres.test(trimAll(_description.value)) == false)
        //            //{
        //            //alert('Only Alphanumeric value is allowed.');
        //            //_description.focus();
        //            //return false;
        //            //}
        //            return true;
        //        }

        function validateSave() {

            var _currencyID = document.getElementById('txtCurrencyID');
            var _description = document.getElementById('txtDescription');

            if (_currencyID.value == '') {
                alert('Enter Currency ID');
                document.getElementById('txtCurrencyID').focus();
                return false;
            }

            if (_currencyID.value.length !== 3) {
                alert('Currency ID should be 3 Digit Alpha');
                document.getElementById('txtCurrencyID').focus();
                return false;
            }


            if (_description.value == '') {
                alert('Enter Currency Description');
                document.getElementById('txtDescription').focus();
                return false;
            }
        }

        function onlyChars(e) {
            var key = e.keyCode;
            var keychar = String.fromCharCode(key);
            var reg = new RegExp("[a-zA-Z ]");
            if (key != 13) {
                return reg.test(keychar);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnSave">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnBack" />
                    <asp:PostBackTrigger ControlID="btnCancel" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><b>Currency Master Details<b></span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" OnClick="btnBack_Click"
                                    ToolTip="Back" TabIndex="7" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="2">
                                <table cellspacing="0" cellpadding="0" border="0" width="600px" style="line-height: 150%">
                                    <%-- <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Currency Rec. ID :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            &nbsp;<asp:TextBox ID="txtCRecID" runat="server" CssClass="textBox" Text="CUR" TabIndex="-1"
                                                MaxLength="5" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Currency ID :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            &nbsp;<asp:TextBox ID="txtCurrencyID" style="text-transform:uppercase" runat="server" CssClass="textBox" TabIndex="1"
                                                MaxLength="3" Width="40px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Description :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            &nbsp;<asp:TextBox ID="txtDescription" runat="server" TabIndex="2" CssClass="textBox"
                                                MaxLength="50" Width="230px" style="text-transform:uppercase"></asp:TextBox>
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Status :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            &nbsp;<asp:RadioButton ID="rdbActive" runat="server" TabIndex="4" Text="Active" GroupName="rdbgrpStatus"
                                                Checked="true" class="elementLabel" />
                                            &nbsp;<asp:RadioButton ID="rdbInActive" runat="server" TabIndex="4" Text="In-Active"
                                                GroupName="rdbgrpStatus" class="mandatoryField" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 400px; padding-top: 10px; padding-bottom: 10px">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                OnClick="btnSave_Click" ToolTip="Save" TabIndex="5" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                OnClick="btnCancel_Click" ToolTip="Cancel" TabIndex="6" />
                                        </td>
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
