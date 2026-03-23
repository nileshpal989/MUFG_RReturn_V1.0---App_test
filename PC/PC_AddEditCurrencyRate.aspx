<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PC_AddEditCurrencyRate.aspx.cs"
    Inherits="PC_PC_AddEditCurrencyRate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="../Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="../Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">

        function isValidDate(controlID, CName) {
            var obj = controlID;
            if (controlID.value != "__/__/____") {

                var day = obj.value.split("/")[0];
                var month = obj.value.split("/")[1];
                var year = obj.value.split("/")[2];
                var today = new Date();

                if (day == "__") {
                    day = today.getDay();
                }
                if (month == "__") {
                    month = today.getMonth() + 1;
                }
                if (year == "____") {
                    year = today.getFullYear();
                }
                var dt = new Date(year, month - 1, day);

                //              if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {

                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year)) {

                    alert('Invalid ' + CName);
                    controlID.focus();
                    return false;
                }
            }
        }

        function validateSave() {
            var txtDate = document.getElementById('txtDate');
            if (txtDate.value == "") {
                alert('Enter Date');
                txtDate.focus();
                return false;
            }
            return true;
        }

        function CalculateCrossRates() {
            var USDSrate = document.getElementById('txtUSD_SRate');
            var USDCrate = document.getElementById('txtUSD_CRate');

            if (USDSrate.value != '') {
             //   USDCrate.value = USDSrate.value;
                USDCrate.value = 1;
                USDSrate.value = parseFloat(USDSrate.value).toFixed(4);
                USDCrate.value = parseFloat(USDCrate.value).toFixed(4);
            }

            if (parseFloat(USDSrate.value) == 0)
                USDSrate.value = 1;


            var GBPSrate = document.getElementById('txtGBP_SRate');
            var GBPCrate = document.getElementById('txtGBP_CRate');

            if (GBPSrate.value != '') {
                GBPCrate.value = parseFloat(GBPSrate.value) / parseFloat(USDSrate.value);

                GBPSrate.value = parseFloat(GBPSrate.value).toFixed(4);
                GBPCrate.value = parseFloat(GBPCrate.value).toFixed(4);
            }

            var EURSrate = document.getElementById('txtEur_SRate');
            var EURCrate = document.getElementById('txtEur_CRate');

            if (EURSrate.value != '') {
                EURCrate.value = parseFloat(EURSrate.value) / parseFloat(USDSrate.value);

                EURSrate.value = parseFloat(EURSrate.value).toFixed(4);
                EURCrate.value = parseFloat(EURCrate.value).toFixed(4);
            }

            var JPYSrate = document.getElementById('txtJPY_SRate');
            var JPYCrate = document.getElementById('txtJPY_CRate');

            if (JPYSrate.value != '') {
                JPYCrate.value = parseFloat(JPYSrate.value) / parseFloat(USDSrate.value);

                JPYSrate.value = parseFloat(JPYSrate.value).toFixed(4);
                JPYCrate.value = parseFloat(JPYCrate.value).toFixed(4);
            }


            var CHFSrate = document.getElementById('txtCHF_SRate');
            var CHFCrate = document.getElementById('txtCHF_CRate');

            if (CHFSrate.value != '') {
                CHFCrate.value = parseFloat(CHFSrate.value) / parseFloat(USDSrate.value);

                CHFSrate.value = parseFloat(CHFSrate.value).toFixed(4);
                CHFCrate.value = parseFloat(CHFCrate.value).toFixed(4);
            }

            var AUDSrate = document.getElementById('txtAUD_SRate');
            var AUDCrate = document.getElementById('txtAUD_CRate');

            if (AUDSrate.value != '') {
                AUDCrate.value = parseFloat(AUDSrate.value) / parseFloat(USDSrate.value);

                AUDSrate.value = parseFloat(AUDSrate.value).toFixed(4);
                AUDCrate.value = parseFloat(AUDCrate.value).toFixed(4);
            }

            var CADSrate = document.getElementById('txtCAD_SRate');
            var CADCrate = document.getElementById('txtCAD_CRate');

            if (CADSrate.value != '') {
                CADCrate.value = parseFloat(CADSrate.value) / parseFloat(USDSrate.value);

                CADSrate.value = parseFloat(CADSrate.value).toFixed(4);
                CADCrate.value = parseFloat(CADCrate.value).toFixed(4);
            }

            var SGDSrate = document.getElementById('txtSGD_SRate');
            var SGDCrate = document.getElementById('txtSGD_CRate');

            if (SGDSrate.value != '') {
                SGDCrate.value = parseFloat(SGDSrate.value) / parseFloat(USDSrate.value);

                SGDSrate.value = parseFloat(SGDSrate.value).toFixed(4);
                SGDCrate.value = parseFloat(SGDCrate.value).toFixed(4);
            }

            var SEKSrate = document.getElementById('txtSEK_SRate');
            var SEKCrate = document.getElementById('txtSEK_CRate');

            if (SEKSrate.value != '') {
                SEKCrate.value = parseFloat(SEKSrate.value) / parseFloat(USDSrate.value);

                SEKSrate.value = parseFloat(SEKSrate.value).toFixed(4);
                SEKCrate.value = parseFloat(SEKCrate.value).toFixed(4);
            }

            var HKDSrate = document.getElementById('txtHKD_SRate');
            var HKDCrate = document.getElementById('txtHKD_CRate');

            if (HKDSrate.value != '') {
                HKDCrate.value = parseFloat(HKDSrate.value) / parseFloat(USDSrate.value);

                HKDSrate.value = parseFloat(HKDSrate.value).toFixed(4);
                HKDCrate.value = parseFloat(HKDCrate.value).toFixed(4);
            }

            var SARSrate = document.getElementById('txtSAR_SRate');
            var SARCrate = document.getElementById('txtSAR_CRate');

            if (SARSrate.value != '') {
                SARCrate.value = parseFloat(SARSrate.value) / parseFloat(USDSrate.value);

                SARSrate.value = parseFloat(SARSrate.value).toFixed(4);
                SARCrate.value = parseFloat(SARCrate.value).toFixed(4);
            }

            var DEMSrate = document.getElementById('txtDEM_SRate');
            var DEMCrate = document.getElementById('txtDEM_CRate');

            if (DEMSrate.value != '') {
                DEMCrate.value = parseFloat(DEMSrate.value) / parseFloat(USDSrate.value);

                DEMSrate.value = parseFloat(DEMSrate.value).toFixed(4);
                DEMCrate.value = parseFloat(DEMCrate.value).toFixed(4);
            }

            var RUBSrate = document.getElementById('txtRUB_SRate');
            var RUBCrate = document.getElementById('txtRUB_CRate');

            if (RUBSrate.value != '') {
                RUBCrate.value = parseFloat(RUBSrate.value) / parseFloat(USDSrate.value);

                RUBSrate.value = parseFloat(RUBSrate.value).toFixed(4);
                RUBCrate.value = parseFloat(RUBCrate.value).toFixed(4);
            }

            var INRSrate = document.getElementById('txtINR_SRate');
            var INRCrate = document.getElementById('txtINR_CRate');

            if (INRSrate.value != '') {
                INRCrate.value = parseFloat(INRSrate.value) / parseFloat(USDSrate.value);

                INRSrate.value = parseFloat(INRSrate.value).toFixed(4);
                INRCrate.value = parseFloat(INRCrate.value).toFixed(4);
            }

            return true;
        }


    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
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
        <uc2:Menu ID="Menu1" runat="server" />
        <br />
        <br />
        <center>
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnBack" />
                    <asp:PostBackTrigger ControlID="btnCancel" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel">Cross Currency Rate Master Details</span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                    OnClick="btnBack_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                                  <input type="hidden" id="hdnMODE_DEMcurr" runat="server" />
                                  <input type="hidden" id="hdnMODE_RUBcurr" runat="server" />
                                  <input type="hidden" id="hdnMODE_INRcurr" runat="server" />
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
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Date :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            &nbsp;<asp:TextBox ID="txtDate" runat="server" CssClass="textBox" MaxLength="10"
                                                ValidationGroup="dtVal" Width="70px" TabIndex="1" OnTextChanged="txtDate_TextChanged"
                                                AutoPostBack="true"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="mdDocdate" Mask="99/99/9999" MaskType="Date"
                                                runat="server" TargetControlID="txtDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                CultureTimePlaceholder=":" Enabled="True">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <asp:Button ID="btncalendar_DocDate" runat="server" CssClass="btncalendar_enabled"
                                                TabIndex="-1" />
                                            <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtDate" PopupButtonID="btncalendar_DocDate" Enabled="True">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdDocdate"
                                                ValidationGroup="dtVal" ControlToValidate="txtDate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="Date is invalid"></ajaxToolkit:MaskedEditValidator>
                                        </td>
                                    </tr>
                                </table>
                                <table cellspacing="0" cellpadding="1" border="0" width="800px" style="line-height: 150%">
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <span class="elementLabel">Currency </span>
                                        </td>
                                        <td align="left" style="width: 150px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <span class="elementLabel">Spot Rate </span>
                                        </td>
                                        <td align="left" >
                                            <span class="elementLabel">  Cross Rate to USD </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <asp:TextBox ID="txtCurrUSD" runat="server" CssClass="textBox" Font-Bold="true" MaxLength="3" Width="40px"
                                                Text="USD" Enabled="false" ></asp:TextBox>
                                        </td>
                                        <td align="center" style="width: 100px">
                                            <asp:TextBox ID="txtUSD_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="1" style="text-align:right"></asp:TextBox>
                                        </td>
                                      <td align="left" >
                                      
                                            <asp:TextBox ID="txtUSD_CRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                 Font-Bold="true" BackColor="LightBlue" style="text-align:right"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <asp:TextBox ID="txtCurrGBP" runat="server" CssClass="textBox" MaxLength="3" Width="40px"
                                                Text="GBP" Enabled="false" Font-Bold="true"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width: 100px">
                                            <asp:TextBox ID="txtGBP_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="2" style="text-align:right"></asp:TextBox>
                                        </td>
                                        <td align="left" >
                                            <asp:TextBox ID="txtGBP_CRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                Font-Bold="true"  BackColor="LightBlue" style="text-align:right"> </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <asp:TextBox ID="txtCurrEur" runat="server" CssClass="textBox" MaxLength="3" Width="40px"
                                                Text="EUR" Enabled="false" Font-Bold="true"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width: 100px">
                                            <asp:TextBox ID="txtEur_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="3" style="text-align:right"></asp:TextBox>
                                        </td>
                                        <td align="left" >
                                            <asp:TextBox ID="txtEur_CRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                Font-Bold="true"  BackColor="LightBlue" style="text-align:right"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <asp:TextBox ID="txtCurrJPY" runat="server" CssClass="textBox" MaxLength="3" Width="40px"
                                                Text="JPY" Enabled="false" Font-Bold="true"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width: 100px">
                                            <asp:TextBox ID="txtJPY_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="4" style="text-align:right"></asp:TextBox>
                                        </td>
                                        <td align="left" >
                                            <asp:TextBox ID="txtJPY_CRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                Font-Bold="true"  BackColor="LightBlue" style="text-align:right"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <asp:TextBox ID="txtCurrCHF" runat="server" CssClass="textBox" MaxLength="3" Width="40px"
                                                Text="CHF" Enabled="false" Font-Bold="true"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width: 100px">
                                            <asp:TextBox ID="txtCHF_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="5" style="text-align:right"></asp:TextBox>
                                        </td>
                                      <td align="left" >
                                            <asp:TextBox ID="txtCHF_CRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                Font-Bold="true"  BackColor="LightBlue" style="text-align:right"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <asp:TextBox ID="txtCurrAUD" runat="server" CssClass="textBox" MaxLength="3" Width="40px"
                                                Text="AUD" Enabled="false" Font-Bold="true"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width: 100px">
                                            <asp:TextBox ID="txtAUD_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="6" style="text-align:right"></asp:TextBox>
                                        </td>
                                       <td align="left" >
                                            <asp:TextBox ID="txtAUD_CRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                 Font-Bold="true"  BackColor="LightBlue" style="text-align:right"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <asp:TextBox ID="txtCurrCAD" runat="server" CssClass="textBox" MaxLength="3" Width="40px"
                                                Text="CAD" Enabled="false" Font-Bold="true"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width: 100px">
                                            <asp:TextBox ID="txtCAD_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="7" style="text-align:right" ></asp:TextBox>
                                        </td>
                                      <td align="left" >
                                            <asp:TextBox ID="txtCAD_CRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                Font-Bold="true"  BackColor="LightBlue" style="text-align:right"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width:50px">
                                            <asp:TextBox ID="txtCurrSGD" runat="server" CssClass="textBox" MaxLength="3" Width="40px"
                                                Text="SGD" Enabled="false" Font-Bold="true"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width: 100px">
                                            <asp:TextBox ID="txtSGD_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="8" style="text-align:right"></asp:TextBox>
                                        </td>
                                     <td align="left" >
                                            <asp:TextBox ID="txtSGD_CRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                 Font-Bold="true"  BackColor="LightBlue" style="text-align:right"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <asp:TextBox ID="txtCurrSEK" runat="server" CssClass="textBox" MaxLength="3" Width="40px"
                                                Text="SEK" Enabled="false" Font-Bold="true"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width: 100px">
                                            <asp:TextBox ID="txtSEK_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="9" style="text-align:right"></asp:TextBox>
                                        </td>
                                        <td align="left" >
                                            <asp:TextBox ID="txtSEK_CRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                 Font-Bold="true"  BackColor="LightBlue" style="text-align:right"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <asp:TextBox ID="txtCurrHKD" runat="server" CssClass="textBox" MaxLength="3" Width="40px"
                                                Text="HKD" Enabled="false" Font-Bold="true"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width: 100px">
                                            <asp:TextBox ID="txtHKD_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="10" style="text-align:right"></asp:TextBox>
                                        </td>
                                        <td align="left" >
                                            <asp:TextBox ID="txtHKD_CRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                Font-Bold="true"  BackColor="LightBlue" style="text-align:right"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <asp:TextBox ID="txtCurrSAR" runat="server" CssClass="textBox" MaxLength="3" Width="40px"
                                                Text="SAR" Enabled="false" Font-Bold="true"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width:100px">
                                            <asp:TextBox ID="txtSAR_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="11" style="text-align:right"></asp:TextBox>
                                        </td>
                                       <td align="left" >
                                            <asp:TextBox ID="txtSAR_CRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                Font-Bold="true"  BackColor="LightBlue" style="text-align:right"></asp:TextBox>
                                        </td>
                                    </tr>
                                          <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <asp:TextBox ID="txtCurrDEM" runat="server" CssClass="textBox" MaxLength="3" Width="40px"
                                                Text="DEM" Enabled="false" Font-Bold="true"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width:100px">
                                            <asp:TextBox ID="txtDEM_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="11" style="text-align:right"></asp:TextBox>
                                        </td>
                                       <td align="left" >
                                            <asp:TextBox ID="txtDEM_CRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                Font-Bold="true"  BackColor="LightBlue" style="text-align:right"></asp:TextBox>
                                        </td>
                                        </tr>
                                         <tr>
                                        <td align="right" style="width: 200px">
                                        </td>

                                        <td align="left" style="width: 50px">
                                            <asp:TextBox ID="txtCurrRUB" runat="server" CssClass="textBox" MaxLength="3" Width="40px"
                                                Text="RUR" Enabled="false" Font-Bold="true"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width:100px">
                                            <asp:TextBox ID="txtRUB_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="12" style="text-align:right"></asp:TextBox>
                                        </td>
                                       <td align="left" >
                                            <asp:TextBox ID="txtRUB_CRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                Font-Bold="true"  BackColor="LightBlue" style="text-align:right"></asp:TextBox>
                                        </td>
                                    </tr>

                                     <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <asp:TextBox ID="txtCurrINR" runat="server" CssClass="textBox" MaxLength="3" Width="40px"
                                                Text="INR" Enabled="false" Font-Bold="true"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width:100px">
                                            <asp:TextBox ID="txtINR_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="13" style="text-align:right"></asp:TextBox>
                                        </td>
                                       <td align="left" >
                                            <asp:TextBox ID="txtINR_CRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                Font-Bold="true"  BackColor="LightBlue" style="text-align:right"></asp:TextBox>
                                        </td>
                                    </tr>


                                    </tr>
                                </table>
                                <br />
                                <table cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="right" style="width: 120px">
                                        </td>
                                        <td align="left" style="width: 400px; padding-top: 10px; padding-bottom: 10px">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                ToolTip="Save" TabIndex="13" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDefault"
                                                ToolTip="Delete" TabIndex="14" onclick="btnDelete_Click"  />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                ToolTip="Cancel" TabIndex="14" OnClick="btnCancel_Click" />
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
