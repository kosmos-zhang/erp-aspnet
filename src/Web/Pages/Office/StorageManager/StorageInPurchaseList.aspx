<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageInPurchaseList.aspx.cs"
    Inherits="Pages_Office_StorageManager_StorageInPurchaseList" ValidateRequest="false" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/GetBillExAttrControl.ascx" TagName="GetBillExAttrControl"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/common.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script type="text/javascript">
        var IsDisplayPrice = '<%=GetIsDisplayPrice() %>';
        var IsBarCode = '<%=GetIsBarCode()%>';
    </script>

    <script src="../../../js/office/StorageManager/StorageInPurchaseList.js" type="text/javascript"></script>

    <title>采购入库列表</title>
</head>
<body>
    <form id="form1" runat="server">
    <input id="Hidden1" type="hidden" runat="server" />
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable"
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
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        单据编号
                                    </td>
                                    <td width="24%" bgcolor="#FFFFFF">
                                        <input name="txtInNo" id="txtInNo" specialworkcheck="单据编号" type="text" class="tdinput"
                                            size="19" runat="server" style="width: 95%" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        单据主题
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <input name="txtTitle" id="txtTitle" specialworkcheck="单据主题" type="text" class="tdinput"
                                            size="19" runat="server" style="width: 95%" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        采购到货单
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <input name="txtFromBillID" id="txtFromBillID" type="text" class="tdinput" size="19"
                                            readonly="readonly" style="width: 90%" onclick="fnPurchaseList(this.id)" runat="server" />
                                    </td>
                                </tr>
                                <tr class="table-item">
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        交货人
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <input name="UserTaker" id="UserTaker" type="text" class="tdinput" size="19" readonly="readonly"
                                            onclick="alertdiv('UserTaker,txtTakerID');" runat="server" style="width: 95%" />
                                        <input name="txtTakerID" id="txtTakerID" type="hidden" runat="server" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        验货人
                                    </td>
                                    <td width="10%" bgcolor="#FFFFFF">
                                        <input name="UserChecker" id="UserChecker" type="text" class="tdinput" size="19"
                                            readonly="readonly" onclick="alertdiv('UserChecker,txtCheckerID');" runat="server"
                                            style="width: 95%" />
                                        <input name="txtCheckerID" id="txtCheckerID" type="hidden" runat="server" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        入库人
                                    </td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                        <input name="UserExecutor" id="UserExecutor" type="text" class="tdinput" size="19"
                                            readonly="readonly" onclick="alertdiv('UserExecutor,txtExecutorID');" runat="server"
                                            style="width: 95%" />
                                        <input name="txtExecutorID" id="txtExecutorID" type="hidden" runat="server" />
                                    </td>
                                </tr>
                                <tr class="table-item">
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        入库部门
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <input name="DeptName" id="DeptName" type="text" class="tdinput" size="19" readonly="readonly"
                                            onclick="alertdiv('DeptName,txtDeptID');" runat="server" style="width: 95%" />
                                        <input name="txtDeptID" id="txtDeptID" type="hidden" runat="server" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        仓库
                                    </td>
                                    <td width="10%" bgcolor="#FFFFFF">
                                        <asp:DropDownList ID="ddlStorageID" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        单据状态
                                    </td>
                                    <td bgcolor="#FFFFFF" class="style1">
                                        <select name="sltBillStatus" class="tdinput" id="sltBillStatus" runat="server">
                                            <option value="">--请选择--</option>
                                            <option value="1">制单</option>
                                            <option value="2">执行</option>
                                            <option value="4">手工结单</option>
                                            <option value="5">自动结单</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr class="table-item">
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        入库时间段:
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <input name="txtEnterDateStart" id="txtEnterDateStart" type="text" class="tdinput"
                                            size="12" readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEnterDateStart')})"
                                            runat="server" />
                                        至
                                        <input name="txtEnterDateEnd" id="txtEnterDateEnd" type="text" class="tdinput" size="12"
                                            readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEnterDateEnd')})"
                                            runat="server" />
                                    </td>
                                    <td align="right" bgcolor="#E6E6E6">
                                         <span id="OtherConditon" style="display:none">其他条件</span>
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <uc2:GetBillExAttrControl ID="GetBillExAttrControl1" runat="server" />
                                    </td>
                                    <td align="right" bgcolor="#E6E6E6">
                                    批次</td>
                                    <td bgcolor="#FFFFFF">
                                        <input name="txtBatchNo" id="txtBatchNo" type="text" class="tdinput" runat="server"
                                            specialworkcheck="批次" style="width: 95%" /></td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <input type="hidden" id="txtorderBy" runat="server" />
                                        <input type="hidden" id="hidModuleID" runat="server" />
                                        <input type="hidden" id="hidSearchCondition" runat="server" />
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" id="btn_Search"
                                            runat="server" style='cursor: hand;' onclick='DoSearch();' visible="false" />
                                            <input type="hidden" id="hidSelPoint" runat="server" value="" />
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
                采购入库单列表
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img src="../../../images/Button/Bottom_btn_new.jpg" id="btn_Add" runat="server"
                                alt="新建采购入库单" style='cursor: hand;' onclick="DoNew();" visible="false" />
                            <img id="btnDel" runat="server" src="../../../images/Button/Main_btn_delete.jpg"
                                alt="删除" style='cursor: hand;' border="0" onclick="Fun_Delete_InPurchase();"
                                visible="false" />
                            <asp:ImageButton ID="btnImport" runat="server" ImageUrl="../../../images/Button/Main_btn_out.jpg"
                                alt="导出Excel" OnClick="btnImport_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1"
                    bgcolor="#999999">
                    <tbody>
                        <tr>
                            <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="5%">
                                <input type="checkbox" name="checkall" id="checkall" onclick="SelectAllCk()" value="checkbox" />
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="8%">
                                <div class="orderClick" onclick="OrderBy('InNo','Span12');return false;">
                                    入库单编号<span id="Span12" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="8%">
                                <div class="orderClick" onclick="OrderBy('Title','Span13');return false;">
                                    入库单主题<span id="Span13" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="10%">
                                <div class="orderClick" onclick="OrderBy('ArriveNo','Span21');return false;">
                                    采购到货单<span id="Span21" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="6%">
                                <div class="orderClick" onclick="OrderBy('Taker','Span22');return false;">
                                    交货人<span id="Span22" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="6%">
                                <div class="orderClick" onclick="OrderBy('Checker','Span23');return false;">
                                    验货人<span id="Span23" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="7%">
                                <div class="orderClick" onclick="OrderBy('DeptName','Span1');return false;">
                                    入库部门<span id="Span1" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="6%">
                                <div class="orderClick" onclick="OrderBy('Executor','Span2');return false;">
                                    入库人<span id="Span2" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="8%">
                                <div class="orderClick" onclick="OrderBy('EnterDate','Span3');return false;">
                                    入库时间<span id="Span3" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="8%">
                                <div class="orderClick" onclick="OrderBy('CountTotal','Span4');return false;">
                                    入库数量<span id="Span4" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="8%" style="display: <%=GetIsDisplayPrice()%>">
                                <div class="orderClick" onclick="OrderBy('TotalPrice','Span5');return false;">
                                    入库金额<span id="Span5" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="8%">
                                <div class="orderClick" onclick="OrderBy('Summary','Span6');return false;">
                                    摘要<span id="Span6" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="8%">
                                <div class="orderClick" onclick="OrderBy('BillStatus','Span7');return false;">
                                    单据状态<span id="Span7" class="orderTip"></span></div>
                            </th>
                        </tr>
                    </tbody>
                </table>
                <br />
                <div style="overflow-y: auto;">
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
                </div>
                <br />
            </td>
        </tr>
    </table>
    <%---------------------------------------------------销售发货通知单div-Start-----------------------------------------------%>
    <div id="divzhezhao" style="filter: Alpha(opacity=0); width: 620px; height: 500px;
        z-index: 1000; position: absolute; display: none; top: 20%; left: 70%; margin: 5px 0 0 -400px;">
        <iframe style="border: 0; width: 620px; height: 100%; position: absolute;"></iframe>
    </div>
    <div id="divPurchaseUC" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 580px; z-index: 1001; position: absolute; display: none; top: 20%; left: 70%;
        margin: 5px 0 0 -400px;">
        <table width="100%">
            <tr>
                <td>
                    <img alt="关闭" id="btn_Close1" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: hand;'
                        onclick='Closediv();' />
                    <img alt="清除" id="ClearInput" src="../../../images/Button/Bottom_btn_del.jpg" style='cursor: hand;'
                        onclick='ClearArriveNo();' />
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" id="Table1" cellspacing="0"
            bgcolor="#CCCCCC">
            <tr>
                <td bgcolor="#FFFFFF">
                    <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                        class="table">
                        <tr class="table-item">
                            <td width="10%" height="20" bgcolor="#E7E7E7" align="right" style="font-size: 13">
                                单据编号
                            </td>
                            <td width="24%" bgcolor="#FFFFFF">
                                <input name="txtArriveNo_UC" id="txtArriveNo_UC" type="text" class="tdinput" runat="server"
                                    size="13" specialworkcheck="单据编号" style="width: 95%" />
                            </td>
                            <td width="10%" bgcolor="#E7E7E7" align="right" style="font-size: 13">
                                单据主题
                            </td>
                            <td width="23%" bgcolor="#FFFFFF">
                                <input name="txtTitle_UC" id="txtTitle_UC" type="text" class="tdinput" runat="server"
                                    size="19" specialworkcheck="单据主题" style="width: 95%" />
                            </td>
                            <td width="10%" bgcolor="#E7E7E7" align="right" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center" bgcolor="#FFFFFF">
                                <img alt="检索" id="btn_Serch" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                    onclick='TurnToOffPage(1);' />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="offerDataList"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderOffBy('ArriveNo','Span8');return false;">
                            采购到货单<span id="Span8" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderOffBy('Title','Span9');return false;">
                            主题<span id="Span9" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="OrderOffBy('CreateDate','Span10');return false;">
                            创建日期<span id="Span10" class="orderTip"></span></div>
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
                            <td height="28" background="../../../images/Main/PageList_bg.jpg" width="28%">
                                <div id="pageOffcount">
                                </div>
                            </td>
                            <td height="28" align="center">
                                <div id="pageOffList_Pager" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divOffpage">
                                    <span id="pageOffList_Total"></span>每页显示
                                    <input name="text" type="text" id="ShowOffPageCount" style="width: 20px;" />
                                    条 转到第
                                    <input name="text" type="text" style="width: 20px;" id="OffToPage" />
                                    页
                                    <img src="../../../Images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangeOffPageCountIndex($('#ShowOffPageCount').val(),$('#OffToPage').val());" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
    <%---------------------------------------------------销售发货通知单div-End-----------------------------------------------%>
    <uc1:Message ID="Message1" runat="server" />
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    </form>
</body>
</html>
