<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchasePlanInfo.aspx.cs"
    Inherits="Pages_Office_PurchaseManager_PurchasePlanInfo" %>

<%@ Register Src="../../../UserControl/Department.ascx" TagName="Department" TagPrefix="uc1" %>

<%@ Register Src="../../../UserControl/FlowApply.ascx" TagName="FlowApply" TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc4" %>
<%@ Register src="../../../UserControl/GetBillExAttrControl.ascx" tagname="GetBillExAttrControl" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>采购计划单列表</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/Flow.js" type="text/javascript"></script>

    <script src="../../../js/Office/PurchaseManager/PurchasePlanInfo.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <style type="text/css">
        </style>
</head>
<body>
    <form id="form1" runat="server">
         <input id="HiddenPoint" type="hidden" runat="server" />
    <a name="pageDataList1Mark"></a>
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
        
        <tr>
            <td valign="top" class="style1">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td rowspan="2" align="right" valign="top">
                <div id='searchClick'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('tblSearch','searchClick')" /></div>
            </td>
        </tr>
        <tr>
            <td valign="top" class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
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
                                        计划单编号
                                    </td>
                                    <td class="tdColInput" width="23%">
                                        <input name="txtPlanNo" id="txtPlanNo" runat="server" class="tdinput" type="text" style="width: 99%" SpecialWorkCheck="计划单编号" />
                                    </td>
                                    <td height="20" class="tdColTitle" width="10%">
                                        主题
                                    </td>
                                    <td class="tdColInput" width="23%">
                                        <input name="txtPlanTitle" id="txtPlanTitle" runat="server" class="tdinput" type="text" style="width: 99%" SpecialWorkCheck="计划单主题"/>
                                    </td>
                                    <td class="tdColTitle" width="10%">
                                        计划员
                                    </td>
                                    <td class="tdColInput" width="24%">
                                        <input name="UserPlanUser" id="UserPlanUser" class="tdinput" style="width: 99%" onclick="alertdiv('UserPlanUser,PlanUserID');"
                                            type="text" />
                                            <input id="PlanUserID" type="hidden"  runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" class="tdColTitle">
                                        预控金额下限
                                    </td>
                                    <td class="tdColInput">
                                        <input name="txtTotalMoneyMin" id="txtTotalMoneyMin" class="tdinput" type="text"  runat="server"
                                            style="width: 99%" SpecialWorkCheck="预控金额下限" />
                                    </td>
                                    <td class="tdColTitle">
                                        预控金额上限
                                    </td>
                                    <td class="tdColInput">
                                        <input name="txtTotalMoneyMax" id="txtTotalMoneyMax" class="tdinput" type="text"  runat="server"
                                            style="width: 99%" SpecialWorkCheck="预控金额上限" />
                                    </td>
                                    <td class="tdColTitle">
                                        采购部门
                                    </td>
                                    <td class="tdColInput">
                                        <input name="DeptName" id="DeptName" class="tdinput" type="text" style="width: 99%"
                                           onclick="alertdiv('DeptName,txtDeptID');" />
                                        <input name="txtDeptID" id="txtDeptID" class="tdinput" type="text" style="display: none"   runat="server"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" class="tdColTitle">
                                        起始计划时间
                                    </td>
                                    <td class="tdColInput">
                                        <input name="txtStartPlanDate" id="txtStartPlanDate" class="tdinput" type="text" readonly ="readonly"
                                            style="width: 99%" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtbuydate')})"   runat="server"/> 
                                    </td>
                                    <td class="tdColTitle">
                                        终止计划时间
                                    </td>
                                    <td class="tdColInput">
                                        <input name="txtEndPlanDate" id="txtEndPlanDate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtbuydate')})"
                                            class="tdinput" type="text"  style="width: 99%" runat="server"  readonly="readonly" /><uc4:Message ID="Message1" runat="server"  />
                                    </td>
                                    <td class="tdColTitle">
                                        审批状态
                                    </td>
                                    <td class="tdColInput">
                                        <select name="ddlFlowStatus" class="tdinput" width="119px" id="ddlFlowStatus"  runat="server">
                                            <option value="0" selected="selected">--请选择--</option>
                                            <option value="">待提交</option>
                                            <option value="1">待审批</option>
                                            <option value="2">审批中</option>
                                            <option value="3">审批通过</option>
                                            <option value="4">审批不通过</option>
                                            <option value="5">撤销审批</option>
                                            
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdColTitle">
                                        单据状态
                                    </td>
                                    <td class="tdColInput">
                                        <select name="ddlBillStatus" class="tdinput" width="119px" id="ddlBillStatus"  runat="server">
                                            <option value="0" selected="selected">--请选择--</option>
                                            <option value="1">制单</option>
                                            <option value="2">执行</option>
                                            <%--<option value="3">变更</option>--%>
                                            <option value="4">手工结单</option>
                                            <option value="5">自动结单</option>
                                        </select>
                                        <input type="hidden" id="hidOrderBy" value="PlanNo ASC" runat="server" />
                                    </td>
                                    <td class="tdColTitle">
                                    <span id="OtherConditon" style=" display:none">其他条件</span></td>
                                    <td class="tdColInput">
                                        <uc2:GetBillExAttrControl ID="GetBillExAttrControl1" runat="server" />
                                    </td>
                                    <td class="tdColTitle">
                                    </td>
                                    <td class="tdColInput">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <input type="hidden" id="hidModuleID" runat="server" />
                                        <input type="hidden" id="SearchCondition" />
                                        <img runat="server" visible="false" alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                            id="btnSearch" onclick='DoSearch()' />
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
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td align="center" valign="top">
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" align="center" valign="top" class="Title">
                <uc3:FlowApply ID="FlowApply1" runat="server" />
                采购计划单列表
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            &nbsp;
                            <img runat="server" visible="false" alt="新建" src="../../../Images/Button/Bottom_btn_new.jpg"  id="btnNew" onclick="DoNew();" />
                            <img runat="server" visible="false" alt="删除" src="../../../Images/Button/Main_btn_delete.jpg" id="btnDelete" onclick="DeletePurPlan();" />
                            <asp:ImageButton ID="btnExport" runat="server" ImageUrl="../../../images/Button/Main_btn_out.jpg"
                                alt="导出Excel" OnClick="btnImport_Click"/>
                                        
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="PurPlanBill"
                    bgcolor="#999999">
                    <tbody>
                        <tr>
                            <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                选择<input type="checkbox"  id="checkall" onclick="SelectAll()" value="checkbox" />
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('PlanNo','oGroup');return false;">
                                    计划单编号<span id="oGroup" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('PlanTitle','PlanTitle');return false;">
                                    计划单主题<span id="PlanTitle" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('PlanUserName','oC2');return false;">
                                    计划员<span id="oC2" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('PlanMoney','oC3');return false;">
                                    预控金额<span id="oC3" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('PlanDate','oC4');return false;">
                                    计划时间<span id="oC4" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('BillStatusName','oC4');return false;">
                                    单据状态<span id="Span1" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('FlowStatusName','oC4');return false;">
                                    审批状态<span id="Span2" class="orderTip"></span></div>
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
                                        <div id="pagePurPlancount">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="PurPlanPage1" class="jPagerBar">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="divpage">
                                            <input name="text2" type="text" id="Text2" style="display: none" />
                                            <span id="pageDataList1_Total"></span>每页显示
                                            <input name="ShowPageCount" type="text" id="ShowPageCount" />条 转到第
                                            <input name="ToPage" type="text" id="ToPage" />页
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
    </form>
    <span id="Forms" class="Spantype"></span>
</body>
</html>
