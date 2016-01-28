<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageInitailList.aspx.cs"
    Inherits="Pages_Office_StorageManager_StorageInitail" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register src="../../../UserControl/GetBillExAttrControl.ascx" tagname="GetBillExAttrControl" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/office/StorageManager/StorageInitailList.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/common.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript">function ToPage_onclick() {

}

    </script>

    <title>期初库存录入列表</title>
</head>
<body>
    <form id="form1" runat="server">
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
                                        <input name="txtInNo" id="txtInNo" type="text" class="tdinput" runat="server" size="13"
                                            specialworkcheck="单据编号" style="width: 95%" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        单据主题
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <input name="txtTitle" id="txtTitle" type="text" class="tdinput" runat="server" size="19"
                                            specialworkcheck="单据主题" style="width: 95%" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        仓库
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <asp:DropDownList ID="sltStorageID" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr class="table-item">
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        入库部门
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <input name="DeptName" id="DeptName" type="text" class="tdinput" runat="server" size="19"
                                            readonly="readonly" onclick="alertdiv('DeptName,txtDeptID');" style="width: 95%" />
                                        <input name="txtDeptID" runat="server" id="txtDeptID" type="hidden" />
                                    </td>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        入库人
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <input name="UsertxtExecutor" id="UsertxtExecutor" type="text" class="tdinput" runat="server"
                                            size="19" readonly="readonly" onclick="alertdiv('UsertxtExecutor,txtExecutorID');"
                                            style="width: 95%" />
                                        <input name="txtExecutorID" id="txtExecutorID" runat="server" type="hidden" />
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
                                <tr>
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
                                        <input type="hidden" id="hidSearchCondition" />
                                        <img alt="检索" id="btn_Search" runat="server" src="../../../images/Button/Bottom_btn_search.jpg"
                                            style='cursor: hand;' onclick='DoSearch();' visible="false" />
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
                期初库存列表
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img src="../../../images/Button/Bottom_btn_new.jpg" id="btn_Add" runat="server"
                                alt="新建" style='cursor: hand;' onclick="DoNew();" visible="false" />
                            <img id="btnDel" runat="server" src="../../../images/Button/Main_btn_delete.jpg"
                                alt="删除" style='cursor: hand;' border="0" onclick="Fun_Delete_StorageInitail();"
                                visible="false" />
                            <%--<img id="btnImport" runat="server" src="../../../images/Button/Main_btn_out.jpg"
                                alt="导出Excel" />--%>
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
                                width="15%">
                                <div class="orderClick" onclick="OrderBy('InNo','oGroup');return false;">
                                    单据编号<span id="oGroup" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="10%">
                                <div class="orderClick" onclick="OrderBy('Title','oC1');return false;">
                                    单据主题<span id="oC1" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="10%">
                                <div class="orderClick" onclick="OrderBy('StorageName','oC2');return false;">
                                    仓库<span id="oC2" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="10%">
                                <div class="orderClick" onclick="OrderBy('DeptName','Span11');return false;">
                                    入库部门<span id="Span11" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="10%">
                                <div class="orderClick" onclick="OrderBy('Executor','oC3');return false;">
                                    入库人<span id="oC3" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="10%">
                                <div class="orderClick" onclick="OrderBy('EnterDate','oC4');return false;">
                                    入库日期<span id="oC4" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="10%">
                                <div class="orderClick" onclick="OrderBy('CountTotal','Span2');return false;">
                                    入库数量<span id="Span2" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="10%">
                                <div class="orderClick" onclick="OrderBy('TotalPrice','oC7');return false;">
                                    入库金额<span id="oC7" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="10%">
                                <div class="orderClick" onclick="OrderBy('BillStatus','oC14');return false;">
                                    单据状态<span id="oC14" class="orderTip"></span></div>
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
                                                <input name="text" type="text" id="ToPage" onclick="return ToPage_onclick()" />
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
    <span id="Span1" class="Spantype"></span>
    <uc1:Message ID="Message1" runat="server" />
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    </form>
</body>
</html>
