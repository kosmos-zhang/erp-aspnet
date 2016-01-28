<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPerformanceDetailsByLT.aspx.cs" Inherits="Pages_Office_HumanManager_PrintPerformanceDetailsByLT" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
      <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
</head>
<body>
<form id="frmMain" runat="server">
    <input id="txtCustCode"  type="hidden"/>
   <input type="hidden" id="VoucherModuleID" runat="server" />
<a name="DetailListMark"></a>
<span id="Forms" class="Spantype" name="Forms"></span>
    <table width="98%"border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr>
            <td  valign="top" class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
            </td>
            <td align="right" valign="top">
                <div id='divSearch'>
                    <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblSearch','divSearch')"/>
                </div>&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2"  >
                <table width="99%" border="0" align="center" cellpadding="0" id="tblSearch"  cellspacing="0" bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
                                <tr>
                                    <td height="20" class="tdColTitle" width="10%">登帐时间</td>
                                    <td class="tdColInput" width="23%">
                                        <asp:TextBox ID="txtOrderCD" runat="server" class="tdinput" ></asp:TextBox>
                                        &nbsp;</td>
                                    <td class="tdColInput" width="23%">
                                        &nbsp;</td>
                                    <td class="tdColInput" width="24%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center" bgcolor="#FFFFFF">
                                        <input type="hidden" id="hidSearchCondition" name="hidSearchCondition" />
                                        &nbsp;

                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                 </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" height="5">
                <input type="hidden" id="hidModuleID" runat="server" />
                <input type="hidden" id="hidModuleIDAsse" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">

                <table width="98%" border="0" cellpadding="0" cellspacing="0" id="tblDetailList" >
                    <tr>
                        <td valign="top">
                            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                        </td>
                        <td align="center" valign="top"></td>
                    </tr>
                    <tr><td colspan="2" height="2"></td></tr>
                    <tr>
                        <td height="30" colspan="2" align="center" valign="top" class="Title">统计分析</td>
                    </tr>
                    <tr>
                        <td height="35" colspan="2" valign="top">
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="800" bgcolor="#FFFFFF">

                                         &nbsp;&nbsp;&nbsp;
                                         
                                         &nbsp;&nbsp;
                                        &nbsp;&nbsp;
                                        
                                            &nbsp;&nbsp;
                                        
                                        
                                         <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                                             AutoDataBind="true" />
                                        
                                        
                                         </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                </table>            
            </td>
        </tr>
    </table>
</form>
</body>
</html>
