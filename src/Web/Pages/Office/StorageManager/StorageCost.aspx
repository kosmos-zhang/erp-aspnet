<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageCost.aspx.cs" Inherits="Pages_Office_StorageManager_StorageCost" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>

<%@ Register Src="../../../UserControl/ProductInfoControl.ascx" TagName="ProductInfoControl"
    TagPrefix="uc5" %>
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

    <script src="../../../js/office/StorageManager/StorageCost.js" type="text/javascript"></script>

    <title>存货成本</title>
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
    <input id="DigitalLength" type="hidden" runat="server" />
    <uc1:Message ID="Message1" runat="server" />
 <uc5:ProductInfoControl ID="ProductInfoControl1" runat="server" />

<div id="divAdd" style="border: solid 10px #898989; background: #fff; padding: 10px;
        width: 360px; z-index: 21; position: absolute; top: 53%; left: 60%; margin: -200px 0 0 -400px;
        display:none ">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="" bgcolor="#999999"
            style="margin-left: 4px">
            <tr>
                <td height="28" bgcolor="#FFFFFF">
                    <img alt="保存" src="../../../Images/Button/Bottom_btn_save.jpg" onclick="CalculationStorageCost();"
                        id="btnSave" runat="server" visible="true" style='cursor:pointer;' />
                    <img alt="返回" src="../../../Images/Button/Bottom_btn_back.jpg" onclick="hideCalculationLayer();"
                        style='cursor::pointer;' />
                </td>
            </tr>
        </table>
        <table width="100%" border="0" align="center" cellspacing="1" bgcolor="#999999">
            <tr> 
                <td align="right" class="tdColTitle"   style=" width:35%">
                    月份<span class="redbold">*</span>：
                </td>
                <td class="tdColInput">
                  <select runat="server" id="selYearMonthC" onchange="getPreYearMonth();"></select>
                </td>
            </tr>
            <tr>
                <td align="right" class="tdColTitle">
                    开始日期<span class="redbold">*</span>：
                </td>
                <td class="tdColInput">
                <input  id="txtStartDateC" type="text" runat="server" readonly="true"  onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStartDateC')})"/>
                </td>
            </tr>
            <tr>
                <td align="right" class="tdColTitle">
                    结束日期<span class="redbold">*</span>：
                </td>
                <td class="tdColInput">
                <input id="txtEndDateC"  type="text" runat="server" readonly="true"   onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEndDateC')})" />
               
                </td>
            </tr>
    
        </table>
    </div>
 
 
 <div id="divEdit" style="border: solid 10px #898989; background: #fff; padding: 10px;
        width: 360px; z-index: 21; position: absolute; top: 53%; left: 60%; margin: -200px 0 0 -400px;
        display:none ">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="" bgcolor="#999999"
            style="margin-left: 4px">
            <tr>
                <td height="28" bgcolor="#FFFFFF">
                    <img alt="保存" src="../../../Images/Button/Bottom_btn_save.jpg" onclick="editEndCost();"
                        id="Img1" runat="server" visible="true" style='cursor:pointer;' />
                    <img alt="返回" src="../../../Images/Button/Bottom_btn_back.jpg" onclick="hideEditCost();"
                        style='cursor::pointer;' />
                </td>
            </tr>
        </table>
        <table width="100%" border="0" align="center" cellspacing="1" bgcolor="#999999">
            <tr> 
                <td align="right" class="tdColTitle"   style=" width:35%">
                    期末成本<span class="redbold">*</span>：
                </td>
                <td class="tdColInput">
                 <input type="text" id="txtPeriodCost"  />
                </td>
            </tr>
    
        </table>
    </div>
 
 


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
                                        物品名称
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <input type="hidden" id="txtProductID" />
                                        <input type="text" id="txtProductName" runat="server" class="tdinput" style="width: 95%"
                                            readonly="true" onclick="getProductInfo();" />
                                    </td>
                                    <td bgcolor="#E7E7E7" align="right" width="10%">
                                        年月
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%" style="vertical-align: bottom">
                                       
                                            <select runat="server" id="txtStartYearMonth">
                                            
                                            </select>
                                            
                                        至
                                    <select runat="server" id="txtEndYearMonth"></select>
                                    </td>
                        </td>
                        <td width="10%" bgcolor="#E7E7E7" align="right" width="10%">
                         
                        </td>
                        <td bgcolor="#FFFFFF" width="24%">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center" bgcolor="#FFFFFF">
                            <img id="btnQuery" alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                onclick='TurnToPage(1)' runat="server" visible="true" />
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
                存货成本列表
            </td>
        </tr>
        <tr>
            <td colspan="2" id="tdResult">
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img src="../../../images/Button/btn_jschcb.jpg" style="cursor: hand" onclick="showCalculationLayer();"
                                alt="计算存货成本" runat="server" id="btnCalculation" visible="true" />
                         
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1"
                    bgcolor="#999999">
                    <tbody>
                        <tr>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="CreateSort('YearMonth');return false;">
                                   年月<span id="YearMonth" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="CreateSort('ProdNo');return false;">
                                    物品编号<span id="ProdNo" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="CreateSort('ProductName');return false;">
                                    品名<span id="ProductName" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="CreateSort('Specification');return false;">
                                    规格<span id="Specification" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="CreateSort('UnitName');return false;">
                                    单位<span id="UnitName" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="CreateSort('ColorName');return false;">
                                    颜色<span id="ColorName" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="CreateSort('PeriodBeginCost');return false;">
                                    期初成本<span id="PeriodBeginCost" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="CreateSort('PeriodBeginCount');return false;">
                                    期初数量<span id="PeriodBeginCount" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="CreateSort('LastTotalPrice');return false;">
                                    期初金额<span id="LastTotalPrice" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="CreateSort('PeriodEndCost');return false;">
                                    期末成本<span id="PeriodEndCost" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="CreateSort('PeriodEndCount');return false;">
                                    期末数量<span id="PeriodEndCount" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="CreateSort('CurrentTotalPrice');return false;">
                                    期末金额<span id="CurrentTotalPrice" class="orderTip"></span></div>
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
