<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileFormat.aspx.cs" Inherits="FileFormat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC-TradeFinance</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css"/>
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
   
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <uc1:Menu ID="Menu1" runat="server"/>
        <p align="left" class="pageLabel">
            <strong>Inward File Format </strong>
        </p>
        <hr />
        <br />      
        <table width="80%" border="1" class="elementTable" cellpadding="1" cellspacing="1" align="left">
            <tr>
                <td width="7%" align="center">
                    <span class="pageLabel"><b>Sr.No</b></span>
                </td>
                <td align="left" width="5%">
                    <span class="pageLabel"><b>Fields</b></span>
                </td>
                <td width="20%" align="left">
                    <span class="pageLabel"><b>Format</b></span>
                </td>
                <td align="left">
                    <span class="pageLabel"><b>Remarks</b></span>
                </td>
            </tr>
            <tr>
                <td align="center" nowrap>
                    <span class="elementLabel">1</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Branch Code </span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Char (3)</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Branch Code of Bank</span>
                </td>
            </tr>
            <tr>
                <td align="center" nowrap>
                    <span class="elementLabel">2</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Document_No </span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Char(14)</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Document Reference Number Of Transaction</span>
                </td>
            </tr>
            <tr>
                <td align="center" nowrap>
                    <span class="elementLabel">3</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Document_Date</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">(DD/MM/YYYY)</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Document Date Of Transaction</span>
                </td>
            </tr>
            <tr>
                <td align="center" nowrap>
                    <span class="elementLabel">4</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">CustAcNo </span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Char (12)</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Customer Account Number</span>
                </td>
            </tr>
            <tr>
                <td align="center" nowrap>
                    <span class="elementLabel">5</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Purpose_Code</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Char (5)</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Purpose Code Of Transaction</span>
                </td>
            </tr>
            <tr>
                <td align="center" nowrap>
                    <span class="elementLabel">6</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Currency </span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Char (3)</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Currency Code</span>
                </td>
            </tr>
            <tr>
                <td align="center" nowrap>
                    <span class="elementLabel">7</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Amount</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel"></span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Amount in Foreign Currency</span>
                </td>
            </tr>
            <tr>
                <td align="center" nowrap>
                    <span class="elementLabel">8</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Exchange_Rate </span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel"></span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Exchange Rate</span>
                </td>
            </tr>
            <tr>
                <td align="center" nowrap >
                    <span class="elementLabel">9</span>
                </td>
                <td align="left" nowrap >
                    <span class="elementLabel">Amount_In_INR </span>
                </td>
                <td align="left" nowrap >
                    <span class="elementLabel"></span>
                </td>
                <td align="left" nowrap >
                    <span class="elementLabel">Amount In Indian Rupees</span>
                </td>
            </tr>
            <tr>
                <td align="center" nowrap>
                    <span class="elementLabel">10</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">FIRC_No </span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Char (15)</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Foreign Inward Remittance Certificate Number</span>
                </td>
            </tr>
            <tr>
                <td align="center" nowrap>
                    <span class="elementLabel">11</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">FIRC_Date</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">(DD/MM/YYYY) </span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Foreign Inward Remittance Certificate Date</span>
                </td>
            </tr>
            <tr>
                <td align="center" nowrap >
                    <span class="elementLabel">12</span>
                </td>
                <td align="left" nowrap >
                    <span class="elementLabel">FIRC_AD_Code</span>
                </td>
                <td align="left" nowrap >
                    <span class="elementLabel">Char (7)</span>
                </td>
                <td align="left" nowrap >
                    <span class="elementLabel"></span>
                </td>
            </tr>
            <tr>
                <td align="center" nowrap>
                    <span class="elementLabel">13</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Value_Date </span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">(DD/MM/YYYY) </span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel"></span>
                </td>
            </tr>
            <tr>
                <td align="center" nowrap>
                    <span class="elementLabel">14</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Remitter_Name</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Char (50)</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Name Of Remitter</span>
                </td>
            </tr>
            <tr>
                <td align="center" nowrap>
                    <span class="elementLabel">15</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Remitter_CountryID </span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Char (2)</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Country Code Of Remitter</span>
                </td>
            </tr>

             <tr>
                <td align="center" nowrap>
                    <span class="elementLabel">16</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Remitter Address </span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Char (100)</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Address Of Remitter</span>
                </td>
            </tr>

             <tr>
                <td align="center" nowrap>
                    <span class="elementLabel">17</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Remitting_Bank </span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Char (100)</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Name Of Bank</span>
                </td>
            </tr>
            <tr>
                <td align="center" nowrap>
                    <span class="elementLabel">18</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Remitter_Bank_CountryID </span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Char (2)</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Country Code Of Remitter Bank</span>
                </td>
            </tr>
             <tr>
                <td align="center" nowrap>
                    <span class="elementLabel">19</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Remitting_Bank_Address </span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Char (100)</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Address of Bank</span>
                </td>
            </tr>

             <tr>
                <td align="center" nowrap>
                    <span class="elementLabel">20</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Purpose_Of_Remittance </span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Char (100)</span>
                </td>
                <td align="left" nowrap>
                    <span class="elementLabel">Purpose Of Remittance</span>
                </td>
            </tr>


        </table>
    </div>
    </form>
</body>
</html>
