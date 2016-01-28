<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageInOtherList.aspx.cs"
    Inherits="Pages_Office_StorageManager_StorageInOtherList" ValidateRequest="false" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/GetBillExAttrControl.ascx" TagName="GetBillExAttrControl"
    TagPrefix="uc2" %>
<%@ Register src="../../../UserControl/Common/ProjectSelectControl.ascx" tagname="ProjectSelectControl" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script type="text/javascript">
        var IsDisplayPrice = '<%=GetIsDisplayPrice() %>';
        var IsBarCode = '<%=GetIsBarCode()%>';
    </script>

    <script src="../../../js/office/StorageManager/StorageInOtherList.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/common.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <title>其他入库列表</title>
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
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <input name="txtInNo" id="txtInNo" specialworkcheck="单据编号" type="text" class="tdinput"
                                            size="19" runat="server" style="width: 95%" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        单据主题
                                    </td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                        <input name="txtTitle" id="txtTitle" specialworkcheck="单据主题" type="text" class="tdinput"
                                            size="19" runat="server" style="width: 95%" />
                                    </td>
                                    <td height="20" align="right" bgcolor="#E6E6E6">
                                        交货人
                                    </td>
                                    <td height="20" bgcolor="#FFFFFF">
                                        <input name="UserTaker" id="UserTaker" type="text" class="tdinput" size="19" readonly="readonly"
                                            onclick="alertdiv('UserTaker,txtTakerID');" runat="server" style="width: 95%" />
                                        <input name="txtTakerID" id="txtTakerID" type="hidden" runat="server" />
                                    </td>
                                </tr>
                                <tr class="table-item">
                                    <td height="20" align="right" bgcolor="#E6E6E6">
                                        验收人
                                    </td>
                                    <td height="20" bgcolor="#FFFFFF">
                                        <input name="UserChecker" id="UserChecker" type="text" class="tdinput" size="19"
                                            readonly="readonly" onclick="alertdiv('UserChecker,txtCheckerID');" runat="server"
                                            style="width: 95%" />
                                        <input name="txtCheckerID" id="txtCheckerID" type="hidden" runat="server" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        入库部门
                                    </td>
                                    <td width="10%" bgcolor="#FFFFFF">
                                        <input name="DeptName" id="DeptName" type="text" class="tdinput" size="19" readonly="readonly"
                                            onclick="alertdiv('DeptName,txtDeptID');" runat="server" style="width: 95%" />
                                        <input name="txtDeptID" id="txtDeptID" type="hidden" runat="server" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        单据状态
                                    </td>
                                    <td bgcolor="#FFFFFF">
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
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        入库人
                                    </td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                        <input name="UserExecutor" id="UserExecutor" type="text" class="tdinput" size="19"
                                            readonly="readonly" onclick="alertdiv('UserExecutor,txtExecutorID');" runat="server"
                                            style="width: 95%" />
                                        <input name="txtExecutorID" id="txtExecutorID" type="hidden" runat="server" />
                                    </td>
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
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        仓库
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <asp:DropDownList class="tdinput" ID="sltStorageID" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr class="table-item">
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                         <span id="OtherConditon" style="display:none">其他条件</span>
                                    </td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                        <uc2:GetBillExAttrControl ID="GetBillExAttrControl1" runat="server" />
                                    </td>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        所属项目</td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <uc3:ProjectSelectControl ID="ProjectSelectControl1" runat="server" />
                                         <input id="txtSelProject" class="tdinput"  onclick="ShowProjectInfo('txtSelProject','HidProjectID');"
                                    readonly="readonly" size="19" type="text" style="width: 95%" runat="server" />
                                <input type="hidden" id="HidProjectID" runat="server" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
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
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                            id="btn_Search" runat="server" onclick='DoSearch();' visible="false" />
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
                其他入库单列表
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img src="../../../images/Button/Bottom_btn_new.jpg" id="btn_Add" runat="server"
                                alt="新建其他入库单" style='cursor: hand;' onclick="DoNew();" visible="false" />
                            <img id="btnDel" runat="server" src="../../../images/Button/Main_btn_delete.jpg"
                                alt="删除" style='cursor: hand;' border="0" onclick="Fun_Delete_StorageInOther();"
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
                                <input type="checkbox" name="checkall" id="checkall" onclick="SelectAllCk()" value="checkbox"
                                    width="10%" />
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="10%">
                                <div class="orderClick" onclick="OrderBy('InNo','oc1');return false;">
                                    单据编号<span id="oc1" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="10%">
                                <div class="orderClick" onclick="OrderBy('Title','oc2');return false;">
                                    单据主题<span id="oc2" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="5%">
                                <div class="orderClick" onclick="OrderBy('TakerName','oc3');return false;">
                                    交货人<span id="oc3" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="5%">
                                <div class="orderClick" onclick="OrderBy('CheckerName','oc4');return false;">
                                    验收人<span id="oc4" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="5%">
                                <div class="orderClick" onclick="OrderBy('InPutDept','oc5');return false;">
                                    入库部门<span id="oc5" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="5%">
                                <div class="orderClick" onclick="OrderBy('ExecutorName','oc56');return false;">
                                    入库人<span id="oc56" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="10%">
                                <div class="orderClick" onclick="OrderBy('EnterDate','oc6');return false;">
                                    入库时间<span id="oc6" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="10%">
                                <div class="orderClick" onclick="OrderBy('ReasonTypeName','oc7');return false;">
                                    入库原因<span id="oc7" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="10%">
                                <div class="orderClick" onclick="OrderBy('CountTotal','oc8');return false;">
                                    入库数量<span id="oc8" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="10%" style="display: <%=GetIsDisplayPrice()%>">
                                <div class="orderClick" onclick="OrderBy('TotalPrice','oc9');return false;">
                                    入库金额<span id="oc9" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="10%">
                                <div class="orderClick" onclick="OrderBy('Summary','oc10');return false;">
                                    摘要<span id="oc10" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="5%">
                                <div class="orderClick" onclick="OrderBy('BillStatusName','oc11');return false;">
                                    单据状态<span id="oc11" class="orderTip"></span></div>
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
    <uc1:Message ID="Message1" runat="server" />
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    </form>
</body>
</html>
