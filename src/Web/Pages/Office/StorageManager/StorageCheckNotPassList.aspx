<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageCheckNotPassList.aspx.cs"
    Inherits="Pages_Office_StorageManager_StorageCheckNotPassList" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/StorageNoPass.ascx" TagName="StorageNoPass"
    TagPrefix="uc5" %>
<%--<%@ Register src="../../../UserControl/CheckApplay.ascx" tagname="CheckApplay" tagprefix="uc2" %>--%>
<%@ Register Src="../../../UserControl/ReportFrom.ascx" TagName="ReportFrom" TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/ReportMan.ascx" TagName="ReportMan" TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/ProductInfoControl.ascx" TagName="ProductInfoControl"
    TagPrefix="uc6" %>
<%@ Register Src="../../../UserControl/GetBillExAttrControl.ascx" TagName="GetBillExAttrControl"
    TagPrefix="uc7" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>不合格品处置单列表</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/office/StorageManager/StorageCheckNotPassList.js" type="text/javascript"></script>

    <script type="text/javascript">
     function Fun_FillParent_Content(id,prodNo,prodName,standardSell,unitID,codeName,taxRate,sellTax,discount,specification,codeTypeName,typeID,StandardCost)
     {

                   document.getElementById('ProductName').value=prodName;
                   document.getElementById('hiddenProductID').value=id;

                   document.getElementById('divStorageProduct').style.display='none';


    }
     function Fun_FillReport1(a,b,c,d,e,f,g,h,i,J)
     {
        document.getElementById('hiddenFromReportID').value=b;
        document.getElementById('FromReportNo').value=a;
        document.getElementById('divStorageNoPass').style.display='none';
        closeRotoscopingDiv(false,'divBackShadow');
     }
     function removeAll()
     {
        document.getElementById('hiddenFromReportID').value='0';
        document.getElementById('FromReportNo').value='';
        document.getElementById('divStorageNoPass').style.display='none';
        closeRotoscopingDiv(false,'divBackShadow');
     }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="hiddenOrder" type="hidden" value="0" runat="server" /><!--导出排序需要--->
    <div id="divBackShadow" style="display: none">
        <iframe src="../../../Pages/Common/MaskPage.aspx" id="BackShadowIframe" frameborder="0"
            width="100%"></iframe>
    </div>
    <uc5:StorageNoPass ID="StorageNoPass1" runat="server" />
    <uc6:ProductInfoControl ID="ProductInfoControl1" runat="server" />
    <uc4:ReportMan ID="ReportMan1" runat="server" />
    <!-- Start 消 息 提 示 -->
    <uc1:Message ID="Message1" runat="server" />
    <!-- End 消 息 提 示 -->
    <input id="isreturn" value="0" type="hidden" />
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                &nbsp;
            </td>
            <td rowspan="2" align="right" valign="top">
                <div id='searchClick'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" /></div>
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件<uc3:ReportFrom
                    ID="ReportFrom1" runat="server" />
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
                                        单据编号
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <asp:TextBox ID="txtReportNo" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                                    </td>
                                    <td bgcolor="#E7E7E7" align="right" width="10%">
                                        单据主题
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <asp:TextBox ID="txtSubject" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right" width="10%">
                                        质检报告单
                                    </td>
                                    <td bgcolor="#FFFFFF" width="24%">
                                        <input id="FromReportNo" type="text" readonly onclick="popNoPassObj.ShowList(1);"
                                            class="tdinput" />
                                        <input id="hiddenFromReportID" runat="server" value="0" type="hidden" />
                                    </td>
                                </tr>
                                <tr class="table-item">
                                    <td height="20" bgcolor="#E7E7E7" align="right" width="10%">
                                        物品名称
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <input id="ProductName" onclick="popTechObj.ShowList('','ProductName','hiddenProductID');"
                                            class="tdinput" width="95%" readonly="readonly" />
                                        <input id="hiddenProductID" runat="server" value="0" type="hidden" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        单据状态
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        &nbsp;<select id="sltBillStatus" runat="server" name="D2">
                                            <option value="00">-请选择-</option>
                                            <option value="1">制单</option>
                                            <option value="2">执行</option>
                                            <option value="4">手工结单</option>
                                            <option value="5">自动结单</option>
                                        </select>
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        审批状态
                                    </td>
                                    <td width="24%" bgcolor="#FFFFFF">
                                        <select id="ddlFlowStatus" runat="server" name="D1">
                                            <option value="00">-请选择-</option>
                                            <option value="6">待提交</option>
                                            <option value="1">待审批</option>
                                            <option value="2">审批中</option>
                                            <option value="3">审批通过</option>
                                            <option value="4">审批不通过</option>
                                            <option value="5">撤消审批</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr class="table-item">
                                    <td height="20" bgcolor="#E7E7E7" align="right" width="10%">
                                        处理日期
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <input id="BeginTime" runat="server" readonly="readonly" class="tdinput" style="width: 90px"
                                            onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('EndCheckDate')})" />至<input
                                                id="EndTime" runat="server" class="tdinput" readonly="readonly" style="width: 90px"
                                                onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('EndCheckDate')})" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        <span id="OtherConditon" style="display:none">其他条件</span>
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <uc7:GetBillExAttrControl ID="GetBillExAttrControl1" runat="server" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                    </td>
                                    <td width="24%" bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <img runat="server" visible="false" id="btnQuery" alt="检索" src="../../../images/Button/Bottom_btn_search.jpg"
                                            style='cursor: hand;' onclick='FistSearchNoPass()' />
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
                不合格品处置单列表
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <a href="StorageCheckNotPassAdd.aspx?myAction=fromlist&ModuleID=2071301">
                                <img runat="server" visible="false" id="btnAdd" src="../../../images/Button/Bottom_btn_new.jpg"
                                    alt="新建质检报告" style='cursor: hand;' border="0" /></a><img runat="server" visible="false"
                                        id="btnDel" src="../../../images/Button/Main_btn_delete.jpg" alt="删除质检报告" style='cursor: hand;'
                                        border="0" onclick="Fun_Delete_NoPass();" />
                            <asp:ImageButton ID="btnImport" runat="server" OnClientClick="return IsOut();" ImageUrl="../../../images/Button/Main_btn_out.jpg"
                                OnClick="btnImport_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" id="tdResult">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1"
                    bgcolor="#999999">
                    <tbody>
                        <tr>
                            <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                选择<input id="cbSelectAll" type="checkbox" onclick="SelectAllList();" />
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ProcessNo','oProcessNo');return false;">
                                    单据编号<span id="oProcessNo" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('Title','oTitle');return false;">
                                    单据主题<span id="oTitle" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('FromType','oFromType');return false;">
                                    源单类型<span id="oFromType" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ReportID','oReportID');return false;">
                                    质检报告单<span id="oReportID" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ProNo','oProNo');return false;">
                                    物品编号<span id="oProNo" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ProductName','oProductName');return false;">
                                    物品名称<span id="oProductName" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('EmployeeName','oEmployeeName');return false;">
                                    处理负责人<span id="oEmployeeName" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ProcessDate','oProcessDate');return false;">
                                    处理日期<span id="oProcessDate" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('BillStatusID','oBillStatusID');return false;">
                                    单据状态<span id="oBillStatusID" class="orderTip"></span></div>
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
    </form>
</body>
</html>
