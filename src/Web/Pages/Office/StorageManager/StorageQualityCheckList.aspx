<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageQualityCheckList.aspx.cs"
    Inherits="Pages_Office_StorageManager_StorageQualityCheckList" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/FlowApply.ascx" TagName="FlowApply" TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/CustInfo.ascx" TagName="CustInfo" TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/GetBillExAttrControl.ascx" TagName="GetBillExAttrControl"
    TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/PageBar-1.1.1.js" language="javascript" type="text/javascript"></script>

    <script src="../../../js/office/StorageManager/StorageQualityCheckList.js" type="text/javascript"></script>

    <title>质检申请单列表</title>

    <script language="javascript" type="text/javascript">
    $(document).ready(function() {
        IsDiplayOther('GetBillExAttrControl1_SelExtValue');
        var IfBack=document.getElementById('hiddenBackValue').value;
        var ThePage=document.getElementById('ToPage').value;
        if(IfBack!=0)
        {
            TurnToPage(ThePage);
        }
    });
    </script>

    <style type="text/css">
        .style1
        {
            width: 10%;
        }
        .style2
        {
            width: 12%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="HiddenOrderID" runat="server" value="0" /><!--存储列表排序字段名字--->
    <div id="divBackShadow" style="display: none">
        <iframe src="../../../Pages/Common/MaskPage.aspx" id="BackShadowIframe" frameborder="0"
            width="100%"></iframe>
    </div>
    <input id="isreturn" value="0" type="hidden" />
    <input id="hiddenBackValue" value="0" type="hidden" /><!--返回需要--->
    <input id="HiddenDept" value="0" type="hidden" runat="server" />
    <input id="HiddenUser" value="0" type="hidden" runat="server" />
    <input id="hiddenCustID" runat="server" type="hidden" value="0" />
    <uc1:Message ID="Message1" runat="server" />
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
                <uc2:FlowApply ID="FlowApply1" runat="server" />
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
                                        质检申请单编号
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <input name="txtInNo" id="txtInNo" type="text" runat="server" class="tdinput" size="19" />
                                    </td>
                                    <td bgcolor="#E7E7E7" align="right" class="style2">
                                        质检申请单主题
                                    </td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                        <input name="txtTitle" runat="server" id="txtTitle" type="text" class="tdinput" size="19" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        报检人员
                                    </td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                        <input name="txtChecker" id="UserChecker" type="text" readonly onclick="alertdiv('UserChecker,HiddenUser');"
                                            class="tdinput" size="19" />
                                    </td>
                                </tr>
                                <tr class="table-item">
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        往来单位
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <input name="txtCustName" id="txtCustName" onclick="SelectCust();" type="text" class="tdinput"
                                            size="19" />
                                    </td>
                                    <td bgcolor="#E7E7E7" align="right" class="style2">
                                        报检部门
                                    </td>
                                    <td width="10%" bgcolor="#FFFFFF">
                                        <input name="DeptQuality" id="DeptQuality" readonly onclick="alertdiv('DeptQuality,HiddenDept');"
                                            title="00" type="text" class="tdinput" size="19" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        报检日期
                                    </td>
                                    <td bgcolor="#FFFFFF" class="style1">
                                        <input runat="server" name="BeginCheckDate" id="BeginCheckDate" style="width: 85px"
                                            type="text" readonly class="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('BeginCheckDate')})"
                                            size="19" />至
                                        <input runat="server" name="EndCheckDate" id="EndCheckDate" style="width: 85px" type="text"
                                            class="tdinput" readonly onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('EndCheckDate')})"
                                            size="19" />
                                    </td>
                                </tr>
                                <tr class="table-item">
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        源单类型
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <select id="FromType" runat="server">
                                            <option value="00">-请选择-</option>
                                            <option value="0">无来源</option>
                                            <option value="1">采购到货单</option>
                                            <option value="2">生产任务单</option>
                                        </select>
                                    </td>
                                    <td bgcolor="#E7E7E7" align="right" class="style2">
                                        质检类别
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <select runat="server" id="txtCheckType">
                                            <option value="00">-请选择-</option>
                                            <option value="1">进货检验</option>
                                            <option value="2">过程检验</option>
                                            <option value="3">最终检验</option>
                                        </select>
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        检验方式
                                    </td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                        <select runat="server" id="txtCheckMode">
                                            <option value="00">-请选择-</option>
                                            <option value="1">全检</option>
                                            <option value="2">抽检</option>
                                            <option value="3">临检</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr class="table-item">
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        单据状态
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <select runat="server" id="BillStatus">
                                            <option value="00">-请选择-</option>
                                            <option value="1">制单</option>
                                            <option value="2">执行</option>
                                            <option value="4">手工结单</option>
                                            <option value="5">自动结单</option>
                                        </select>
                                    </td>
                                    <td bgcolor="#E7E7E7" align="right" class="style2">
                                        审批状态
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <select runat="server" id="ddlFlowStatus">
                                            <option value="00">-请选择-</option>
                                            <option value="6">待提交</option>
                                            <option value="1">待审批</option>
                                            <option value="2">审批中</option>
                                            <option value="3">审批通过</option>
                                            <option value="4">审批不通过</option>
                                            <option value="5">撤消审批</option>
                                        </select>
                                    </td>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        <span id="OtherConditon" style="display: none">其他条件</span>
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <uc4:GetBillExAttrControl ID="GetBillExAttrControl1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <img runat="server" visible="false" alt="检索" src="../../../images/Button/Bottom_btn_search.jpg"
                                            style='cursor: hand;' id="tbn_search" onclick="Fun_Search_QualityCheck(1);" />
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
                质检申请单列表
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img runat="server" visible="false" src="../../../images/Button/Bottom_btn_new.jpg"
                                alt="新建采购入库单" style='cursor: hand;' onclick="window.location='StorageQualityCheckAdd.aspx?ModuleID=2071101&From=List';"
                                id="btn_new" />
                            <img runat="server" visible="false" id="btnDel" src="../../../images/Button/Main_btn_delete.jpg"
                                alt="删除" style='cursor: hand;' border="0" onclick="storageQualityDelete();" />
                            <asp:ImageButton ID="btnImport" OnClientClick="return IsOut();" ImageUrl="../../../images/Button/Main_btn_out.jpg"
                                runat="server" OnClick="btnImport_Click" />
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
                                选择<input id="CheckboxList" name="CheckboxList" onclick="selectAll();" type="checkbox" />
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ApplyNo','oApplyNo');return false;">
                                    质检申请单编号<span id="oApplyNo" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('Title','oTitle');return false;">
                                    质检申请单主题<span id="oTitle" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('FromType','oFromType');return false;">
                                    源单类型<span id="oFromType" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('CustName','oCustName');return false;">
                                    往来单位<span id="oCustName" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('CustBigType','oCustBigType');return false;">
                                    往来单位大类<span id="oCustBigType" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('CheckMode','oCheckMode');return false;">
                                    检验方式<span id="oCheckMode" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('EmployeeName','oEmployeeName');return false;">
                                    报检人员<span id="oEmployeeName" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('DeptName','oDeptName');return false;">
                                    报检部门<span id="oDeptName" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('CheckDate','oCheckDate');return false;">
                                    报检日期<span id="oCheckDate" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('BillStatus','oBillStatus');return false;">
                                    单据状态<span id="oBillStatus" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('FlowStatus','oFlowStatus');return false;">
                                    审批状态<span id="oFlowStatus" class="orderTip"></span></div>
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
                                            <input name="text" type="text" id="Text2" style="display: none" />
                                            <span id="pageDataList1_Total"></span>每页显示
                                            <input runat="server" name="text" type="text" id="ShowPageCount" />
                                            条 转到第
                                            <input runat="server" name="text" type="text" id="ToPage" />
                                            页
                                            <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
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
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    <input id="HiddenSearch" runat="server" value="" type="hidden" />
    </form>
</body>
</html>
