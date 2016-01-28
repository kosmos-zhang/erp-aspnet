<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportTableList.aspx.cs" Inherits="ReportTableList" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>业务表列表</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../../../css/default.css" type="text/css" rel="stylesheet" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    
    <script src="../../../js/JQuery/PrintArea.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="hidOrder" type="hidden" value="0" runat="server" />
    <div id="divBackShadow" style="display: none">
        <iframe src="../../../Pages/Common/MaskPage.aspx" id="BackShadowIframe" frameborder="0"
            width="100%"></iframe>
    </div>
    <uc1:Message ID="Message1" runat="server" />
    <table width="95%" border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                &nbsp;
            </td>
            <td rowspan="2" align="right" valign="top">
                <div id='searchClick'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('basicInfo','searchClick')" /></div>
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" id="basicInfo" cellspacing="0"
                    bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                class="table">
                                <tr class="table-item">
                                    <td height="20" class="tdColTitle" width="15%">
                                        起始时间
                                    </td>
                                    <td class="tdColInput" width="15%">
                                        <input id="txtBeginDate" runat="server" class="tdinput" name="txtOpenDate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtBeginDate')})" size="15" type="text" runat="server" />
                                    </td>
                                    <td height="20" class="tdColTitle" width="5%">
                                        截止时间
                                    </td>
                                    <td class="tdColInput">
                                        <input id="txtEndDate" runat="server" class="tdinput" name="txtOpenDate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEndDate')})" size="15" type="text" runat="server" />
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td colspan="4" align="center" bgcolor="#FFFFFF">
                                        <asp:ImageButton ID="btn_query" 
                                            ImageUrl="../../../images/Button/Bottom_btn_search.jpg" runat="server" 
                                            onclick="btn_query_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td align="center" valign="top">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" align="center" valign="top" class="Title">
                <asp:Literal ID="report_title"  runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <asp:ImageButton ID="btn_excel" 
                                ImageUrl="../../../images/Button/Main_btn_out.jpg" runat="server" 
                                onclick="btn_excel_Click" />
                            <img alt="" src="../../../Images/Button/btn_pageprint.jpg" onclick=" $('div#myPrintArea').printArea();" style="cursor:pointer;" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" id="tdResult">
                <div id="myPrintArea">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1" bgcolor="#999999">
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="Reportlist" Width="100%" runat="server" BorderWidth="0px" 
                            CellPadding="0" CellSpacing="1">
                            <RowStyle BackColor="White" Height="25px" Width="95%" />
                            <HeaderStyle Height="25px" BackColor="#ECECEC" />
                        </asp:GridView>
                    </td>
                </tr>
                </table>
                </div> 
            </td>
        </tr>
        <tr>
            <td align="center">
                <table width="100%" height="57" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="height:30px; background-color:#FFFFFF; padding-left:5px" align="left">
                        制表人：<asp:Literal ID="lbl_person" runat="server"></asp:Literal>
                    </td>
                    <td align="right" style="background-color:#FFFFFF; padding-right:5px">
                        制表时间：<asp:Literal ID="lbl_time" runat="server"></asp:Literal>
                    </td>
                </tr>
                </table>
            </td>
        </tr>
    </table>
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    </form>
</body>
</html>
