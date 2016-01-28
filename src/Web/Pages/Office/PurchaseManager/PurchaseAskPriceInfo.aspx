<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchaseAskPriceInfo.aspx.cs"
    Inherits="Pages_Office_PurchaseManager_PurchaseAskPriceInfo" %>

<%@ Register Src="../../../UserControl/ProviderInfo.ascx" TagName="ProviderInfo"
    TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<%@ Register src="../../../UserControl/GetBillExAttrControl.ascx" tagname="GetBillExAttrControl" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>采购订单列表</title>
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

    <script src="../../../js/Office/PurchaseManager/PurchaseAskPriceInfo.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <style type="text/css">
        .DivSpanCss
        {
            filter: progid:DXImageTransform.Microsoft.DropShadow(color=#cccccc,offX=4,offY=4,positives=true);
            position: absolute;
            top: 160px;
            left: 190px;
            border-width: 1pt;
            border-color: #EEEEEE;
            border-style: solid;
            width: 380px;
            display: none;
            height: 180px;
            z-index: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
         <input id="HiddenPoint" type="hidden" runat="server" />
    <iframe id="frmHistory" style="filter: Alpha(opacity=0);border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 1000px; height:1000px; z-index: 1001; position: absolute; display: none; top: 20%; left: 40%;
        margin: 5px 0 0 -400px;">
    </iframe>
    <div id="History" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 1000px; z-index: 1001; position: absolute; display: none; top: 20%; left: 40%;
        margin: 5px 0 0 -400px;">
        <table width="100%">
            <tr>
                <td>
                    <a onclick="closeHistory()" style="text-align: right; cursor: pointer">关闭</a>
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="PurAskPriceHistory"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" align="center">
                        序号
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                        <div class="orderClick" onclick="OrderBy('ProductNo','oGroup');return false;">
                            物品编号<span id="oGroup" class="orderTip"></span></div>
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                        <div class="orderClick" onclick="OrderBy('ProductName','Span6');return false;">
                            物品名称<span id="Span6" class="orderTip"></span></div>
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                        <div class="orderClick" onclick="OrderBy('Specification','Span7');return false;">
                            规格<span id="Span7" class="orderTip"></span></div>
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                        <div class="orderClick" onclick="OrderBy('ProductCount','Span8');return false;">
                            计划数量<span id="Span8" class="orderTip"></span></div>
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                        <div class="orderClick" onclick="OrderBy('RequireDate','Span9');return false;">
                            交货日期<span id="Span9" class="orderTip"></span></div>
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                        <div class="orderClick" onclick="OrderBy('UnitName','Span10');return false;">
                            单位<span id="Span10" class="orderTip"></span></div>
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                        <div class="orderClick" onclick="OrderBy('UnitPrice','Span11');return false;">
                            单价<span id="Span11" class="orderTip"></span></div>
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle" valign="middle">
                        <div class="orderClick" onclick="OrderBy('TaxPrice','Span12');return false;">
                            含税价<span id="Span12" class="orderTip"></span></div>
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                        <div class="orderClick" onclick="OrderBy('DiscountDetail','Span13');return false;">
                            折扣<span id="Span13" class="orderTip"></span></div>
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                        <div class="orderClick" onclick="OrderBy('TaxRate','Span14');return false;">
                            税率<span id="Span14" class="orderTip"></span></div>
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                        <div class="orderClick" onclick="OrderBy('TotalPriceDetail','Span15');return false;">
                            金额<span id="Span15" class="orderTip"></span></div>
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                        <div class="orderClick" onclick="OrderBy('TotalFeeDetail','Span16');return false;">
                            含税金额<span id="Span16" class="orderTip"></span></div>
                    </td>
                    <td align="center" bgcolor="#E6E6E6" class="ListTitle">
                        <div class="orderClick" onclick="OrderBy('TotalTaxDetail','Span17');return false;">
                            税额<span id="Span17" class="orderTip"></span></div>
                    </td>
                    
                    
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
                                <div id="pageHistorycount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataList1_PagerHistory" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divPageHistory">
                                    <input name="text" type="text" id="Text2History" style="display: none" />
                                    <span id="pageDataList1_TotalProvider"></span>每页显示
                                    <input name="text" type="text" id="ShowPageCountHistory" />条 转到第
                                    <input name="text" type="text" id="ToPageHistory" />页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexHistory($('#ShowPageCountHistory').val(),$('#ToPageHistory').val());" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
    <uc2:Message ID="Message1" runat="server" />
    <a name="DetailListMark"></a>
    <table width="98%" border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
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
                                        询价单编号
                                        <td class="tdColInput" width="23%">
                                            <input name="txtAsNo" id="txtAskNo" runat="server" class="tdinput" type="text" style="width: 99%"
                                                specialworkcheck="询价单编号" />
                                        </td>
                                        <td height="20" class="tdColTitle" width="10%">
                                            询价单主题
                                        </td>
                                        <td class="tdColInput" width="23%">
                                            <input name="txtAskTitle" id="txtAskTitle" runat="server"  class="tdinput" type="text" style="width: 99%"
                                                specialworkcheck="询价单主题" />
                                        </td>
                                        <td class="tdColTitle">
                                            源单类型
                                        </td>
                                        <td class="tdColInput">
                                            <select name="ddlFromType" class="tdinput" width="119px" id="ddlFromType" runat="server" >
                                                <option value="a">--请选择--</option>
                                                <option value="0">无来源</option>
                                                <option value="1">采购申请</option>
                                                <option value="2">采购计划</option>
                                            </select>
                                        </td>
                                </tr>
                                <tr>
                                    <td height="20" class="tdColTitle">
                                        部门
                                    </td>
                                    <td class="tdColInput">
                                        <input name="DeptName" id="DeptName" class="tdinput" onclick="alertdiv('DeptName,hidDeptID');"
                                            type="text" style="width: 99%" />
                                        <input type="hidden" id="hidDeptID" runat="server" />
                                    </td>
                                    <td class="tdColTitle">
                                        询价员
                                    </td>
                                    <td class="tdColInput">
                                        <input name="UserAskUserName" id="UserAskUserName" class="tdinput" onclick="alertdiv('UserAskUserName,hidUserID');"
                                            type="text" style="width: 99%" />
                                        <input type="hidden" id="hidUserID" runat="server" />
                                    </td>
                                    <td class="tdColTitle">
                                        单据状态
                                    </td>
                                    <td class="tdColInput">
                                        <select name="ddlBillStatus" class="tdinput" width="119px" id="ddlBillStatus" runat="server" >
                                            <option value="0" selected="selected">--请选择--</option>
                                            <option value="1">制单</option>
                                            <option value="2">执行</option>
                                            <%--<option value="3">变更</option>--%>
                                            <option value="4">手工结单</option>
                                            <option value="5">自动结单</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" class="tdColTitle">
                                        供应商
                                    </td>
                                    <td class="tdColInput">
                                        <uc1:ProviderInfo ID="ProviderInfo1" runat="server" />
                                        <input name="txtProviderName" id="txtProviderName" onclick="popProviderObj.ShowProviderList('hidProviderID','txtProviderName')"
                                            class="tdinput" type="text" style="width: 99%" readonly />
                                        <input type="hidden" id="hidProviderID" runat="server" />
                                    </td>
                                    <td class="tdColTitle">
                                        审批状态
                                    </td>
                                    <td class="tdColInput">
                                        <select name="ddlFlowStatus" class="tdinput" width="119px" id="ddlFlowStatus" runat="server" >
                                            <option value="" selected="selected">--请选择--</option>
                                            <option value="0">待提交</option>
                                            <option value="1">待审批</option>
                                            <option value="2">审批中</option>
                                            <option value="3">审批通过</option>
                                            <option value="4">审批不通过</option>
                                            <option value="5">撤销审批</option>
                                        </select>
                                    </td>
                                    <td class="tdColTitle">
                                    <span id="OtherConditon" style=" display:none">其他条件</span></td>
                                    <td class="tdColInput">
                                        <uc3:GetBillExAttrControl ID="GetBillExAttrControl1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        起始询价日期
                                    </td>
                                    <td height="20" class="tdColInput" width="24%">
                                        <input name="StartAskDate" id="StartAskDate" class="tdinput" type="text" style="width: 99%"
                                            onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('StartAskDate')})"  runat="server"   readonly="readonly"/>
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        终止询价日期
                                    </td>
                                    <td height="20" class="tdColInput" width="24%">
                                        <input name="EndAskDate" id="EndAskDate" class="tdinput" type="text" style="width: 99%"
                                            onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('EndAskDate')})"  runat="server"  readonly ="readonly"/>
                                        <input type="hidden" id="hidOrderBy" runat="server" value="AskNo ASC " />
                                    </td>
                                    <td class="tdColTitle">
                                    </td>
                                    <td class="tdColInput">
                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <input type="hidden" id="hidModuleID" runat="server" />
                                        <input type="hidden" id="HiddenURLParams" />
                                        <img runat="server" visible="false" alt="检索" src="../../../images/Button/Bottom_btn_search.jpg"
                                            id="btnSearch" runat="server" style='cursor: pointer;' onclick='DoSearch()' />
                                        <%--<img alt="重置" src="../../../images/Button/Bottom_btn_re.jpg" id="btnReset" runat="server" visible="true" style='cursor:pointer;' onclick="ClearInputProxy()" width="52" height="23" />--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
        <table width="98%" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td colspan="2" valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" /> <input type="hidden" id="hfModuleID" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" align="center" border="0" cellpadding="0" cellspacing="0" id="tblDetailList">
                    <tr>
                        <td colspan="2" height="2">
                        </td>
                    </tr>
                    <tr>
                        <td height="30" colspan="2" align="center" valign="top" class="Title">
                            采购询价单列表
                        </td>
                    </tr>
                    <tr>
                        <td height="35" colspan="2" valign="top"><input type="hidden" id="SearchCondition" />
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF">
                                        <img runat="server" visible="false" src="../../../Images/Button/Bottom_btn_new.jpg"
                                            alt="新建" id="btnNew" runat="server" style="cursor: hand" onclick="DoNew();" />
                                        <img runat="server" visible="false" src="../../../images/Button/Main_btn_delete.jpg"
                                            alt="删除" id="btnDelete" runat="server" onclick="DeletePurAsk()" style='cursor: pointer;' />
                                        <asp:ImageButton ID="btnExport" runat="server" ImageUrl="../../../images/Button/Main_btn_out.jpg"
                                alt="导出Excel" OnClick="btnImport_Click"/>
                                        
                        
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                 </table>
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" id="PurAskPriceInfo"
                    bgcolor="#999999">
                    <tbody>
                        <tr>
                            <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                选择<input type="checkbox" visible="false" id="checkall" onclick="SelectAll()" value="checkbox" />
                            </th>
                            <%--<th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="return false;">
                                    询价历史<span id="Span5" class="orderTip"></span>
                                </div>
                            </th>--%>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderByAsk('AskNo','oC0');return false;">
                                    询价单编号<span id="oC0" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderByAsk('AskTitle','oC1');return false;">
                                    询价单主题<span id="oC1" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderByAsk('ProviderName','oC2');return false;">
                                    供应商<span id="oC2" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderByAsk('AskDate','oC4');return false;">
                                    询价日期<span id="oC4" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderByAsk('AskUserName','oC5');return false;">
                                    询价员<span id="oC5" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderByAsk('AskOrder','oC6');return false;">
                                    当前询价次数<span id="oC6" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderByAsk('TotalPrice','Span3');return false;">
                                    金额合计<span id="Span3" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderByAsk('TotalTax','Span4');return false;">
                                    税额合计<span id="Span4" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderByAsk('TotalFee','Span1');return false;">
                                    含税金额合计<span id="Span1" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderByAsk('BillStatusName','Span2');return false;">
                                    单据状态<span id="Span2" class="orderTip"></span>
                                </div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderByAsk('FlowStatusName','oC7');return false;">
                                    审批状态<span id="oC7" class="orderTip"></span>
                                </div>
                            </th>
                        </tr>
                    </tbody>
                </table>
                <br />
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
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
    </form>
    <span id="Forms" class="Spantype" name="Forms"></span>
</body>
</html>
