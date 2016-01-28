<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebSiteOrderList.aspx.cs" Inherits="Pages_Office_CustomWebSiteManager_WebSiteOrderList" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/office/CustomWebSiteManager/WebSiteOrderList.js" type="text/javascript"></script>
    <title>采购到货单列表</title>
</head>
<body>
    <form id="form1" runat="server">
     <input type="hidden" id="txtAction" value="ADD" />
    <input type="hidden" id="txtSendPriceID" value="-1" />
    <span id="Span2" class="Spantype"></span>
    <input type="hidden" id="txtUserID" runat="server" />
    <input type="hidden" id="txtUserName" runat="server" />
    <input type="hidden" id="txtDate" runat="server" />
    <input id="txtOrderBy" type="hidden" value="ID DESC" runat="server" />
    <input id="txtIsSearch" type="hidden" />
    <input type="hidden" id="txtPageSize" />
    <input type="hidden" id="txtPageIndex" />
    <input id="ModuleID" type="hidden" runat="server" />
    <input id="OutModuleID" type="hidden" runat="server" />
    <uc1:Message ID="Message1" runat="server" />
    <!-- 销售订单-->
    <!-- 销售订单-->
    <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td rowspan="2" align="right" valign="top">
                <div id='searchClick'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" /></div>
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
                <table width="99%" border="0" align="center" cellpadding="0" id="searchtable" cellspacing="0"
                    bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                class="table">
                                <tr class="table-item">
                                    <td height="20" bgcolor="#E7E7E7" align="right" width="10%">
                                        订单编号
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <input type="text" id="txtOrderNo" runat="server" class="tdinput"   style="width:95%" specialworkcheck="订单编号"/>
                                    </td>
                                    <td bgcolor="#E7E7E7" align="right" width="10%">
                                        下单日期
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%" style="vertical-align: bottom">
                                        <input type="text" id="txtOrderDateStart" runat="server" class="tdinput"  
                                            style="vertical-align: bottom; width:70px" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtOrderStartDate')})"
                                            readonly />
                                        至
                                        <input type="text" id="txtOrderDateEnd" runat="server" class="tdinput"  style=" width:70px"
                                            onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtOrderEndDate')})" readonly />
                                    </td>
                        </td>
                        <td width="10%" bgcolor="#E7E7E7" align="right" width="10%">
                            最迟发货日期
                        </td>
                        <td bgcolor="#FFFFFF" width="24%">
                            <input type="text" id="txtConsignmentDateStart" runat="server" class="tdinput" style=" width:70px" 
                                onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtConsignmentDateStart')})"
                                readonly />
                            至
                            <input type="text" id="txtConsignmentDateEnd" runat="server" class="tdinput"  style=" width:70px"
                                onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtConsignmentDateEnd')})"
                                readonly />
                        </td>
                    </tr>
                    <tr class="table-item">
                        <td height="20" bgcolor="#E7E7E7" align="right" width="10%">
                            订单状态
                        </td>
                        <td width="23%" bgcolor="#FFFFFF">
                            <select runat="server" id="selOrderStatus">
                                <option value="">--请选择--</option>
                                <option value="1">待处理</option>
                                <option value="2">处理中</option>
                                <option value="3">发货中</option>
                                <option value="4">已结单</option>
                            </select>
                        </td>
                        <td width="10%" bgcolor="#E7E7E7" align="right">
                            &nbsp;
                        </td>
                        <td width="23%" bgcolor="#FFFFFF">
                            &nbsp;
                        </td>
                        <td width="10%" bgcolor="#E7E7E7" align="right">
                            &nbsp;
                        </td>
                        <td width="24%" bgcolor="#FFFFFF">
                            <input type="hidden" id="txtHidProviderID" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center" bgcolor="#FFFFFF">
                            <img id="btnQuery" alt="检索" src="../../../images/Button/Bottom_btn_search.jpg"
                                style='cursor: hand;' onclick='TurnToPage(1)' runat="server" visible="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </td> </tr> </table>
    <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
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
                订单列表
            </td>
        </tr>

        <tr>
            <td colspan="2" id="tdResult">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1"
                    bgcolor="#999999">
                    <tbody>
                        <tr>
                    
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="CreateSort('OrderNo');return false;">
                                    订单编号<span id="OrderNo" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="CreateSort('CustName');return false;">
                                                                        客户名称<span id="CustName" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="CreateSort('OrderDate');return false;">
                                    下单日期<span id="OrderDate" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="CreateSort('ConsignmentDate');return false;">
                                    最迟发货日期<span id="ConsignmentDate" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="CreateSort('Status');return false;">
                                    订单状态<span id="Status" class="orderTip"></span></div>
                            </th>
                        </tr>
                    </tbody>
                </table>
                <br />
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                    class="PageList">
                    <tr>
                        <td height="28" background="../../../images/Main/PageList_bg.jpg">
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                                <tr>
                                    <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                        <div id="pagecount">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="pageDataList1_PagerList" class="jPagerBar">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="divpage">
                                            <input name="text" type="text" id="Text2" style="display: none" />
                                            <span id="pageDataList1_Total"></span>每页显示
                                            <input name="text" type="text" id="ShowPageCount" />
                                            条 转到第
                                            <input name="text" type="text" id="ToPage" />
                                            页
                                            <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                                width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
    </table>
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    </form>
</body>
</html>
