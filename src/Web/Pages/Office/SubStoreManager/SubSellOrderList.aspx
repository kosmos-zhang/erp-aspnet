<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubSellOrderList.aspx.cs"
    Inherits="Pages_Office_SubStoreManager_SubSellOrderList" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/GetBillExAttrControl.ascx" TagName="GetBillExAttrControl"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>销售订单列表 </title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/Flow.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/Office/SubStoreManager/SubSellOrderList.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <a name="DetailListMark"></a>
    <table width="95%" border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <div style="display: none">
            <input id="SearchCondition" type="text" />
        </div>
        <tr>
            <td valign="top" class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
            </td>
            <td align="right" valign="top">
                <div id='divSearch'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('tblSearch','divSearch')" />
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" id="tblSearch" cellspacing="0"
                    bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC">
                                <tr>
                                    <td height="20" class="tdColTitle" width="10%">
                                        订单编号
                                    </td>
                                    <td class="tdColInput" width="23%">
                                        <input name="txtOrderNo" id="txtOrderNo" class="tdinput" type="text" style="width: 99%"
                                            specialworkcheck="订单编号" runat="server" />
                                    </td>
                                    <td height="20" class="tdColTitle" width="10%">
                                        订单主题
                                    </td>
                                    <td class="tdColInput" width="23%">
                                        <input name="txtOrderTitle" id="txtOrderTitle" class="tdinput" type="text" style="width: 99%"
                                            specialworkcheck="订单主题" runat="server" />
                                    </td>
                                    <td class="tdColTitle" width="10%">
                                        发货模式
                                    </td>
                                    <td class="tdColInput" width="24%">
                                        <select id="ddlSendMode" class="tdinput" name="ddlSendMode" width="119px" runat="server">
                                            <option selected="selected" value="0">--请选择--</option>
                                            <option value="1">分店发货</option>
                                            <option value="2">总部发货</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" class="tdColTitle">
                                        客户名称
                                    </td>
                                    <td class="tdColInput">
                                        <input id="txtCustName" class="tdinput" type="text" style="width: 99%" specialworkcheck="客户名称"
                                            runat="server" />
                                    </td>
                                    <td class="tdColTitle">
                                        客户联系电话
                                    </td>
                                    <td class="tdColInput">
                                        <input id="txtCustTel" class="tdinput" type="text" style="width: 99%" specialworkcheck="客户联系电话"
                                            runat="server" />
                                    </td>
                                    <td class="tdColTitle">
                                        客户手机号
                                    </td>
                                    <td class="tdColInput">
                                        <input id="txtCustMobile" class="tdinput" type="text" style="width: 99%" specialworkcheck="客户手机号"
                                            runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdColTitle">
                                        送货地址
                                    </td>
                                    <td class="tdColInput">
                                        <input id="txtCustAddr" class="tdinput" type="text" style="width: 99%" specialworkcheck="送货地址"
                                            runat="server" />
                                    </td>
                                    <td class="tdColTitle">
                                        销售分店
                                    </td>
                                    <td class="tdColInput">
                                        <input name="DeptName" id="DeptName" class="tdinput" onclick="alertdiv('DeptName,hidDeptID');"
                                            runat="server" type="text" style="width: 99%" disabled="disabled" />
                                        <input type="hidden" id="hidDeptID" value="0" runat="server" />
                                    </td>
                                    <td class="tdColTitle">
                                        业务员
                                    </td>
                                    <td class="tdColInput">
                                        <input id="UserName" class="tdinput" onclick="alertdiv('UserName,hidUserID');" type="text"
                                            style="width: 99%" />
                                        <input type="hidden" id="hidUserID" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" class="tdColTitle">
                                        业务状态
                                    </td>
                                    <td class="tdColInput">
                                        <select name="ddlBusiStatus" class="tdinput" width="119px" id="ddlBusiStatus" runat="server">
                                            <option value="0" selected="selected">--请选择--</option>
                                            <option value="1">下单</option>
                                            <option value="2">出库</option>
                                            <option value="3">结算</option>
                                            <option value="4">完成</option>
                                        </select>
                                    </td>
                                    <td class="tdColTitle">
                                        单据状态
                                    </td>
                                    <td class="tdColInput">
                                        <select name="ddlBillStatus" class="tdinput" width="119px" id="ddlBillStatus" runat="server">
                                            <option value="0" selected="selected">--请选择--</option>
                                            <option value="1">制单</option>
                                            <option value="2">执行</option>
                                            <option value="4">手工结单</option>
                                            <option value="5">自动结单</option>
                                        </select>
                                    </td>
                                    <td class="tdColTitle">
                                        <span id="OtherConditon" style="display: none">其他条件</span>
                                    </td>
                                    <td class="tdColInput">
                                        <uc2:GetBillExAttrControl ID="GetBillExAttrControl1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <input type="hidden" id="hidModuleID" runat="server" />
                                        <input type="hidden" id="hidSearchCondition" />
                                        <img runat="server" visible="false" alt="检索" src="../../../images/Button/Bottom_btn_search.jpg"
                                            id="btnSearch" style='cursor: pointer;' onclick='fnSearch()' />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" height="10">
                <input type="hidden" id="hfModuleID" runat="server" />
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
                销售订单列表
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img runat="server" visible="false" src="../../../Images/Button/Bottom_btn_new.jpg"
                                alt="新建" id="btnNew" runat="server" style="cursor: hand" onclick="DoNew();" />
                            <img runat="server" visible="false" src="../../../images/Button/Main_btn_delete.jpg"
                                alt="删除" id="btnDelete" runat="server" onclick="DoDelete()" style='cursor: pointer;' />
                            <asp:ImageButton OnClientClick="return IsOut();" ID="btnImport" ImageUrl="../../../images/Button/Main_btn_out.jpg"
                                AlternateText="导出Excel" runat="server" OnClick="btnImport_Click" />
                            &nbsp;<uc1:Message ID="Message1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" id="tdResult">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="SubSellOrderInfo"
                    bgcolor="#999999">
                    <tbody>
                        <tr>
                            <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                选择
                                <input type="checkbox" visible="false" id="checkall" onclick="fnSelectAll()" value="checkbox" />
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('OrderNo','oC0');return false;">
                                    订单编号<span id="oC0" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('Title','oC1');return false;">
                                    订单主题<span id="oC1" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('SendMode','oC2');return false;">
                                    发货模式<span id="oC2" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('CustName','oC3');return false;">
                                    客户名称<span id="oC3" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('CustTel','oC4');return false;">
                                    客户联系电话<span id="oC4" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('CustMobile','Span3');return false;">
                                    客户手机号<span id="Span3" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('CustAddr','oC5');return false;">
                                    送货地址<span id="oC5" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('DeptName','oC6');return false;">
                                    销售分店<span id="oC6" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('SellerName','Span1');return false;">
                                    业务员<span id="Span1" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('BusiStatusName','Span2');return false;">
                                    业务状态<span id="Span2" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('BillStatusName','oC7');return false;">
                                    单据状态<span id="oC7" class="orderTip"></span>
                                </div>
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
                                        <div id="pageDataList1_Pager" class="jPagerBar">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="divpage">
                                            <input name="text2" type="text" id="Text2" style="display: none" />
                                            每页显示<input name="ShowPageCount" type="text" id="ShowPageCount" maxlength="4" size="3" />条&nbsp;&nbsp;
                                            转到第<input name="ToPage" type="text" id="ToPage" maxlength="4" size="3" />页&nbsp;&nbsp;
                                            <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: pointer;' alt="go"
                                                align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());" />
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
    </form>
    <span id="Forms" class="Spantype" name="Forms"></span>
</body>
</html>
