<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_DeleteErrorRecords.aspx.cs"
    Inherits="TF_DeleteErrorRecords" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="Style/style_new.css" rel="stylesheet" type="text/css" />
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
                   if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {

                       if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year)) {

                           alert('Invalid ' + CName);
                           controlID.focus();
                           return false;
                       }
                   }
               }
           }

           function validateDelete(btnNo) {

               var hdnRole = document.getElementById('hdnRole');
               var ans = '';
               if (hdnRole.value != 'Supervisor') {
                   alert('Only Supervisor can Delete the Error Records.');
                   return false;
               }
               if (btnNo == '1') {
                   ans = confirm('Are you sure you want to delete Doc Bill Error Records?');
                   if (ans) {
                       return true;
                   }
                   else
                       return false;
               }
               else if (btnNo == '2') {
                   ans = confirm('Are you sure you want to delete Export Realisation Error Records?');
                   if (ans) {
                       return true;
                   }
                   else
                       return false;

               }
               return true;
           }

        </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
     <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script src="../../Scripts/InitEndRequest.js" type="text/javascript"></script>
    <%--<asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <div>
        <uc1:Menu ID="Menu1" runat="server" />
        <br />
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <ContentTemplate>
                <div style="text-align: left;">
                    <span class="pageLabel"><strong>Deletion of Error Records</strong> </span>
                    <input id="hdnRole" type="hidden" runat="server"/>
                    <br />
                    <hr />
                    <asp:Label ID="lblmessage" runat="server" CssClass="mandatoryField"></asp:Label>
                    <br/>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="elementLabel">Branch :</span>              
                    <asp:DropDownList ID="ddlRefNo" runat="server" CssClass="textBox">
                    </asp:DropDownList>
                    <br />
                    <span class="mandatoryField">*</span><span class="elementLabel">From Date :</span> 
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10" 
                        TabIndex="2"  Width="70px" AutoPostBack="true" 
                        ontextchanged="txtFromDate_TextChanged"></asp:TextBox>
                    <asp:Button ID="btncalendar_FromDate" runat="server" 
                        CssClass="btncalendar_enabled" TabIndex="-1" />
                    <ajaxToolkit:MaskedEditExtender ID="mdfdate" runat="server" 
                        AcceptNegative="Left" CultureName="en-GB" DisplayMoney="Left" 
                        ErrorTooltipEnabled="true" InputDirection="RightToLeft" Mask="99/99/9999" 
                        MaskType="Date" PromptCharacter="_" TargetControlID="txtFromDate">
                    </ajaxToolkit:MaskedEditExtender>
                    <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" 
                        Format="dd/MM/yyyy" PopupButtonID="btncalendar_FromDate" 
                        TargetControlID="txtFromDate">
                    </ajaxToolkit:CalendarExtender>
                    &nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To Date :</span> 
                   
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" 
                        TabIndex="3" Width="70px" AutoPostBack="true" 
                        ontextchanged="txtToDate_TextChanged"></asp:TextBox>
                    <asp:Button ID="btncalendar_ToDate" runat="server" 
                        CssClass="btncalendar_enabled" />
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                        AcceptNegative="Left" CultureName="en-GB" DisplayMoney="Left" 
                        ErrorTooltipEnabled="true" InputDirection="RightToLeft" Mask="99/99/9999" 
                        MaskType="Date" PromptCharacter="_" TargetControlID="txtToDate">
                    </ajaxToolkit:MaskedEditExtender>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                        Format="dd/MM/yyyy" PopupButtonID="btncalendar_ToDate" 
                        TargetControlID="txtToDate">
                    </ajaxToolkit:CalendarExtender>
                    <br />
                    <table align="left" border="1" cellspacing="0" width="50%">
                        <caption>
                            <br />
                            <tr>
                                <td>
                                    <table>
                                        <tr height="40px">
                                            <td align="right" nowrap width="10%">
                                            </td>
                                            <td align="center" nowrap width="20%">
                                                <span class="pageLabel"><strong>Receipt Of Document</strong> </span>
                                            </td>
                                            <td align="center" nowrap width="20%">
                                                <span class="pageLabel"><strong>Export Realisation</strong> </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span class="elementLabel">XML Files Pending to be Generated :</span>
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="txtdoc_pending" runat="server" CssClass="textBox" 
                                                    ReadOnly="true" Style="text-align: center" Width="70px"></asp:TextBox>
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="txtrealized_pending" runat="server" CssClass="textBox" 
                                                    ReadOnly="true" Style="text-align: center" Width="70px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span class="elementLabel">XML Files Already Generated :</span>
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="txtdoc_generated" runat="server" CssClass="textBox" 
                                                    ReadOnly="true" Style="text-align: center" Width="70px"></asp:TextBox>
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="txtrealized_generated" runat="server" CssClass="textBox" 
                                                    ReadOnly="true" Style="text-align: center" Width="70px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span class="elementLabel">XML Files Successfully uploaded to RBI :</span>
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="txtdoc_succeeded" runat="server" CssClass="textBox" 
                                                    ReadOnly="true" Style="text-align: center" Width="70px"></asp:TextBox>
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="txtrealized_succeeded" runat="server" CssClass="textBox" 
                                                    ReadOnly="true" Style="text-align: center" Width="70px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" nowrap>
                                                <span class="elementLabel">XML Files Generated with Error Records :</span>
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="txtDoc_Failed" runat="server" CssClass="textBox" 
                                                    ReadOnly="true" Style="text-align: center" Width="70px"></asp:TextBox>
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="txtRealized_Failed" runat="server" CssClass="textBox" 
                                                    ReadOnly="true" Style="text-align: center" Width="70px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr height="50px">
                                            <td align="right" nowrap>
                                            </td>
                                            <td align="center">
                                                <asp:Button ID="btnDelete_Doc" runat="server" CssClass="buttonDefault" 
                                                    OnClick="btnDelete_Doc_Click" Text="Delete" />
                                            </td>
                                            <td align="center">
                                                <asp:Button ID="btnDelete_Realized" runat="server" CssClass="buttonDefault" 
                                                    onclick="btnDelete_Realized_Click" Text="Delete" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </caption>
                    </table>
                    <br>
                    <br></br>
                    <br></br>
                    </br>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
