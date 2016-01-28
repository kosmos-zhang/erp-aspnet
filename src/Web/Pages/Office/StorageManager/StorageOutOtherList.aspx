<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageOutOtherList.aspx.cs"
    Inherits="Pages_Office_StorageManager_StorageOutOtherList" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register src="../../../UserControl/GetBillExAttrControl.ascx" tagname="GetBillExAttrControl" tagprefix="uc2" %>
<%@ Register src="../../../UserControl/Common/ProjectSelectControl.ascx" tagname="ProjectSelectControl" tagprefix="uc8" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/office/StorageManager/StorageOutOtherList.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/common.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <title>其他出库列表</title>
</head>
<body>
    <form id="form1" runat="server">    
    <uc8:ProjectSelectControl ID="ProjectSelectControl1" runat="server" />
    <input id="HiddenPoint" type="hidden" runat="server" />
    <input type="hidden" id="IsDisplayPrice" runat="server" value="" />
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
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC">
                                <tr>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        出库单编号
                                    </td>
                                    <td width="24%" bgcolor="#FFFFFF">
                                        <input name="txtOutNo" id="txtOutNo" specialworkcheck="出库单编号" type="text" class="tdinput"
                                            size="19" runat="server" style="width: 95%" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        出库单主题
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <input name="txtTitle" id="txtTitle" specialworkcheck="出库单主题" type="text" class="tdinput"
                                            size="19" runat="server" style="width: 95%" />
                                    </td>
                                    <td height="20" align="right" bgcolor="#E6E6E6" width="10%">
                                        源单类型
                                    </td>
                                    <td height="20" bgcolor="#FFFFFF" width="24%">
                                        <select id="sltFromType" runat="server">
                                            <option value="">--请选择--</option>
                                            <option value="0">无来源</option>
                                            <option value="1">采购退货单</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        出库人
                                    </td>
                                    <td width="10%" bgcolor="#FFFFFF">
                                        <input id="UserOuter" class="tdinput" name="UserOuter" onclick="alertdiv('UserOuter,txtOuterID');"
                                            readonly="readonly" size="19" type="text" runat="server" style="width: 95%" />
                                        <input id="txtOuterID" name="txtOuterID" type="hidden" runat="server" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        出库原因
                                    </td>
                                    <td width="10%" bgcolor="#FFFFFF">
                                        <asp:DropDownList ID="ddlReason" runat="server">
                                        </asp:DropDownList>
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
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        出库时间段:
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <input name="txtOutDateStart" id="txtOutDateStart" type="text" class="tdinput" size="12"
                                            onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtOutDateStart')})" readonly="readonly"
                                            runat="server" />
                                        至
                                        <input name="txtOutDateEnd" id="txtOutDateEnd" type="text" class="tdinput" size="12"
                                            onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtOutDateEnd')})" readonly="readonly"
                                            runat="server" />
                                    </td>
                                    <td align="right" bgcolor="#E6E6E6">
                                    批次
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="txtBatchNo" class="tdinput" type="text" runat="server" size="15" specialworkcheck="批次" />
                                    </td>
                                    <td align="right" bgcolor="#E6E6E6">
                                    <span id="OtherConditon" style="display:none">其他条件</span>
                                    </td>
                                    <td bgcolor="#FFFFFF">                                       
                                        <uc2:GetBillExAttrControl ID="GetBillExAttrControl1"  runat="server" />                                       
                                    </td>
                                    
                                </tr>
                                <tr>
                                <td align="right" bgcolor="#E6E6E6">
                                所属项目</td>
                                <td bgcolor="#FFFFFF">
                                    <input id="SelectProject" runat="server" class="tdinput"  onclick="ShowProjectInfo('SelectProject','HiddenProjectID');"
                                        readonly="readonly" size="19" type="text" style="width: 95%" />
                                    <input type="hidden" runat="server" id="HiddenProjectID" />
                                 </td>
                                  <td align="right" bgcolor="#E6E6E6">
                                </td>
                                <td bgcolor="#FFFFFF">
                                    
                                 </td>
                                  <td align="right" bgcolor="#E6E6E6">
                                </td>
                                <td bgcolor="#FFFFFF">
                                    
                                 </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <input type="hidden" id="txtorderBy" runat="server" />
                                        <input type="hidden" id="hidModuleID" runat="server" />
                                        <input type="hidden" id="hidSearchCondition" runat="server" />
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                            id="btn_Search" runat="server" onclick='DoSearch();' visible="false" />
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
                其他出库单列表
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img src="../../../images/Button/Bottom_btn_new.jpg" id="btn_Add" runat="server"
                                alt="新建销售出库单" style='cursor: hand;' onclick="DoNew();" visible="false" />
                            <img id="btnDel" runat="server" src="../../../images/Button/Main_btn_delete.jpg"
                                alt="删除" style='cursor: hand;' border="0" onclick="Fun_Delete_StorageOutOther();"
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
                                width="10%">
                                <div class="orderClick" onclick="OrderBy('OutNo','oc1');return false;">
                                    出库单编号<span id="oc1" class="orderTip"></span></div>
                            </th>
                             <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="8%">
                                <div class="orderClick" onclick="OrderBy('Title','oc2');return false;">
                                    出库单主题<span id="oc2" class="orderTip"></span></div>
                            </th>
                            <%--<th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="8%">
                                <div class="orderClick" onclick="OrderBy('BatchNo','oc2');return false;">
                                    批次<span id="oc2" class="orderTip"></span></div>
                            </th>--%>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="10%">
                                <div class="orderClick" onclick="OrderBy('FromTypeName','Span1');return false;">
                                    源单类型<span id="Span1" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="10%">
                                <div class="orderClick" onclick="OrderBy('Transactor','oc4');return false;">
                                    出库人<span id="oc4" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="10%">
                                <div class="orderClick" onclick="OrderBy('OutDate','oc5');return false;">
                                    出库时间<span id="oc5" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="15%">
                                <div class="orderClick" onclick="OrderBy('ReasonTypeName','oc5');return false;">
                                    出库原因<span id="Span2" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="10%">
                                <div class="orderClick" onclick="OrderBy('CountTotal','oc6');return false;">
                                    出库数量<span id="oc6" class="orderTip"></span></div>
                            </th>
                            <th align="center" id="tr_Money" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                width="10%">
                                <div class="orderClick" onclick="OrderBy('TotalPrice','oc7');return false;">
                                    出库金额<span id="oc7" class="orderTip"></span></div>
                            </th>
                            
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"
                                >
                                <div class="orderClick" onclick="OrderBy('BillStatusName','oc9');return false;">
                                    单据状态<span id="oc9" class="orderTip"></span></div>
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
