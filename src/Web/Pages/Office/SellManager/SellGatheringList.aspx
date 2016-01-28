<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SellGatheringList.aspx.cs"
    Inherits="Pages_Office_SellManager_SellGatheringList" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/sellModuleSelectCustUC.ascx" TagName="sellModuleSelectCustUC"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/SelectSellSendUC.ascx" TagName="SelectSellSendUC"
    TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/SelectSellOrderUC.ascx" TagName="SelectSellOrderUC"
    TagPrefix="uc5" %>
<%@ Register src="../../../UserControl/GetBillExAttrControl.ascx" tagname="GetBillExAttrControl" tagprefix="uc6" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/office/SellManager/SellGatheringList.js" type="text/javascript"></script>

    <title>回款计划列表</title>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        var precisionLength=<%=SelPoint %>;//小数精度
    </script>
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
                                    <td width="11%" height="20" bgcolor="#E7E7E7" align="right">
                                        单据编号
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <input id="GatheringNo" specialworkcheck="单据编号" type="text" style="width: 120px;" class="tdinput" size="15" />
                                        <input type="hidden" id="hiddExpGatheringNo" runat="server" />
                                    </td>
                                    <td width="11%" bgcolor="#E7E7E7" align="right">
                                        主题
                                    </td>
                                    <td width="22%" bgcolor="#FFFFFF">
                                        <input id="Title" type="text" style="width: 120px;" specialworkcheck="主题" class="tdinput"
                                            maxlength="50" />
                                        <input type="hidden" id="hiddExpTitle" runat="server" />
                                    </td>
                                    <td width="11%" bgcolor="#E7E7E7" align="right">
                                        客户
                                    </td>
                                    <td width="22%" bgcolor="#FFFFFF">
                                        <input id="CustID" class="tdinput" type="text" readonly="readonly" style="width: 120px;"
                                            onclick="fnSelectCustInfo()" />
                                        <input type="hidden" id="hiddExpCustID" runat="server" />
                                    </td>
                                </tr>
                                <tr class="table-item">
                                    <td bgcolor="#E7E7E7" align="right">
                                        源单类型
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input name="DeptName" id="DeptName" class="tdinput" type="text" size="15" readonly="readonly"
                                            style="display: none;" /><select style="width: 120px;margin-top:2px;margin-left:2px;" id="FromType">
                                                <option value='' selected="selected">--请选择--</option>
                                                <option value="0">无来源</option>
                                                <option value="1">销售订单</option>
                                                <option value="2">销售发货单</option>
                                            </select>
                                        <input type="hidden" id="hiddExpFromType" runat="server" />
                                    </td>
                                    <td height="20" bgcolor="#E7E7E7" align="right">
                                        源单编号
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="FromBillID" onclick="fnSelectOfferInfo()" class="tdinput" type="text"
                                            readonly="true" style="width: 120px;" />
                                        <input type="hidden" id="hiddExpFromBillID" runat="server" />
                                    </td>
                                    <td bgcolor="#E7E7E7" align="right">
                                        业务员
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="UserSeller" class="tdinput" onclick="fnSelectSeller()" readonly="readonly"
                                            style="width: 120px;" type="text" /><input id="hiddSeller" type="hidden" />
                                        <input type="hidden" id="hiddExpSeller" runat="server" />
                                    </td>
                                </tr>
                                <tr class="table-item">
                                    <td height="20" bgcolor="#E7E7E7" align="right">
                                        总金额范围
                                    </td>
                                    <td bgcolor="#FFFFFF" style="vertical-align: bottom;">
                                        <table style="margin-bottom: 0px; vertical-align: bottom" border="0" cellpadding="0"
                                            cellspacing="0">
                                            <tr>
                                                <td>
                                                    <input id="PlanPrice" onchange="Number_round(this,<%=SelPoint %>)" class="tdinput" type="text"
                                                        style="width: 60px;" maxlength="100" />
                                                    <input type="hidden" id="hiddExpPlanPrice" runat="server" />
                                                </td>
                                                <td style="vertical-align: middle">
                                                    至
                                                </td>
                                                <td>
                                                    <input id="PlanPrice0" class="tdinput" onchange="Number_round(this,<%=SelPoint %>)" type="text"
                                                        style="width: 60px;" maxlength="100" />
                                                    <input type="hidden" id="hiddExpPlanPrice0" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td bgcolor="#E7E7E7" align="right">
                                        期次（数字）
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="GatheringTime" class="tdinput" type="text" style="width: 120px;" maxlength="100" />
                                        <input type="hidden" id="hiddExpGatheringTime" runat="server" />
                                    </td>
                                    <td align="right" bgcolor="#E6E6E6">
                                        <span id="OtherConditon" style="display:none">其他条件</span>
                                   </td>
                                   <td bgcolor="#FFFFFF">
                                        <uc6:GetBillExAttrControl ID="GetBillExAttrControl1" runat="server" />
                                   </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <uc1:Message ID="Message1" runat="server" />
                                        <img runat="server" visible="false" alt="检索" src="../../../images/Button/Bottom_btn_search.jpg"
                                            style='cursor: pointer;' id="btnSearch" onclick='TurnToPage(1)' />&nbsp;
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
                回款计划列表
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img runat="server" visible="false" src="../../../images/Button/Bottom_btn_new.jpg"
                                id="btnNew" alt="新建" style='cursor: pointer;' onclick="fnNew()" />
                            <img runat="server" visible="false" src="../../../images/Button/Main_btn_delete.jpg"
                                alt="删除" onclick="DelEquipmentInfo()" style='cursor: pointer;' id="btnDel" />
                            <asp:ImageButton ID="btnImport" ImageUrl="../../../images/Button/Main_btn_out.jpg"
                                AlternateText="导出Excel" runat="server" OnClick="btnImport_Click" />
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
                            <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                选择
                                <input type="checkbox" visible="false" id="checkall" onclick="selectall()" value="checkbox" />
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('GatheringNo','oGroup');return false;">
                                    单据编号<span id="oGroup" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('Title','oC1');return false;">
                                    主题<span id="oC1" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('CustName','oC2');return false;">
                                    客户<span id="oC2" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('stateName','oC3');return false;">
                                    回款状态<span id="oC3" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('fromTypeName','oC4');return false;">
                                    源单类型<span id="oC4" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('BillNo','oC5');return false;">
                                    源单编号<span id="oC5" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('EmployeeName','oC6');return false;">
                                    业务员<span id="oC6" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('PlanPrice','oC7');return false;">
                                    总金额<span id="oC7" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('GatheringTime','oC8');return false;">
                                    期次<span id="oC8" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('PlanGatherDate','oC9');return false;">
                                    计划日期<span id="oC9" class="orderTip"></span></div>
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
                                        <div id="pageSellOffcount">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="pageDataList1_Pager" class="jPagerBar">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="divpage">
                                            &nbsp;<input name="text" type="text" id="Text2" style="display: none" />
                                            <span id="pageDataList1_Total"></span>每页显示
                                            <input name="text" type="text" id="ShowPageCount" maxlength="4" />
                                            条 转到第
                                            <input name="text" type="text" id="ToPage" maxlength="8" />
                                            页
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
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype" name="Forms"></span>
    <uc2:sellModuleSelectCustUC ID="sellModuleSelectCustUC1" runat="server" />
    <uc4:SelectSellSendUC ID="SelectSellSendUC1" runat="server" />
    <uc5:SelectSellOrderUC ID="SelectSellOrderUC1" runat="server" />
    <input id="hiddUrl" type="hidden" value="" />
    <input type="hidden" id="hiddExpOrder" runat="server" />
    <input type="hidden" id="hiddExpTotal" runat="server" />
    </form>
</body>
</html>
