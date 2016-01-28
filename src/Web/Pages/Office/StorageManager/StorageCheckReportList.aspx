<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageCheckReportList.aspx.cs"
    Inherits="Pages_Office_StorageManager_StorageCheckReportList" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/CheckApplay.ascx" TagName="CheckApplay" TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/ReportFrom.ascx" TagName="ReportFrom" TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/ReportMan.ascx" TagName="ReportMan" TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/CheckReportPurControl.ascx" TagName="CheckReportPurControl"
    TagPrefix="uc5" %>
<%@ Register Src="../../../UserControl/GetBillExAttrControl.ascx" TagName="GetBillExAttrControl"
    TagPrefix="uc6" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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

    <script src="../../../js/office/StorageManager/StorageReportList.js" type="text/javascript"></script>

    <title>质检报告单列表</title>

    <script type="text/javascript">
    function selectReport()
    {
      var TheFromType=document.getElementById('sltFromType').value;
        if(TheFromType=="1")
        {
           popReportObj.ShowList(2);
        }
        if(TheFromType=="2")
        {
            popReportFromTypeObj.ShowFromTypeList(2);
        }
        if(TheFromType=="3")
        {
            popReportManObj.ShowList(2);
        }
        if(TheFromType=="4")
        {
            popReportPurObj.ShowList(0);
        }
    }
    function FillFromArrive(a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r)
    {
       document.getElementById('tbReportNo').value=q;
       document.getElementById('divPurchaseArrive').style.display='none';
    }
    function SelectAllList()
    {
        var cb=document.getElementsByName('cbboxall');
        for(var i=0;i<cb.length;i++)
        {
            if(cb[i].checked)
            {
                cb[i].checked=false;
            }
            else
            {
                cb[i].checked=true;
            }
        }
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
    <uc2:CheckApplay ID="CheckApplay1" runat="server" />
    <uc3:ReportFrom ID="ReportFrom1" runat="server" />
    <input id="isreturn" value="0" type="hidden" />
    <uc5:CheckReportPurControl ID="CheckReportPurControl1" runat="server" />
    <!-- Start 消 息 提 示 -->
    <uc1:Message ID="Message1" runat="server" />
    <!-- End 消 息 提 示 -->
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                <uc4:ReportMan ID="ReportMan1" runat="server" />
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
                &nbsp;
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
                                        源单类型
                                    </td>
                                    <td bgcolor="#FFFFFF" width="24%">
                                        <select runat="server" id="sltFromType">
                                            <option value="00">--请选择--</option>
                                            <option value="0">无来源</option>
                                            <option value="1">检验申请单</option>
                                            <option value="2">质检报告单</option>
                                            <option value="3">生产任务单</option>
                                            <option value="4">采购到货单</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr class="table-item">
                                    <td height="20" bgcolor="#E7E7E7" align="right" width="10%">
                                        对应源单编号
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <input id="tbReportID" type="hidden" class="tdinput" width="95%" />
                                        <input id="tbReportNo" runat="server" onclick="selectReport();" type="text" class="tdinput"
                                            width="99%" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        质检类别
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <select runat="server" id="sltCheckType">
                                            <option selected value="00">--请选择--</option>
                                            <option value="1">进货检验</option>
                                            <option value="2">过程检验</option>
                                            <option value="3">最终检验</option>
                                        </select>
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        检验方式
                                    </td>
                                    <td width="24%" bgcolor="#FFFFFF">
                                        <select runat="server" id="sltCheckMode" name="sltCheckMode">
                                            <option value="00">--请选择--</option>
                                            <option value="1">全检</option>
                                            <option value="2">抽检</option>
                                            <option value="3">临检</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr class="table-item">
                                    <td height="20" bgcolor="#E7E7E7" align="right" width="10%">
                                        报检人员
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <asp:TextBox ID="UserPrincipal" runat="server" onclick="alertdiv('UserPrincipal,hiddenChecker');"
                                            ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                                        <input type="hidden" id="hiddenChecker" value="0" runat="server" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        报检部门
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <asp:TextBox ID="DeptName" runat="server" onclick="alertdiv('DeptName,hiddenCheckDept');"
                                            ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                                        <input type="hidden" id="hiddenCheckDept" runat="server" value="0" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        审批状态
                                    </td>
                                    <td width="24%" bgcolor="#FFFFFF">
                                        <select runat="server" id="ddlFlowStatus">
                                            <option value="00">--请选择--</option>
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
                                        检验日期
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <input id="BeginTime" runat="server" readonly="readonly" class="tdinput" style="width: 90px"
                                            onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('EndCheckDate')})" />
                                        至<input id="EndTime" class="tdinput" runat="server" readonly="readonly" style="width: 90px"
                                            onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('EndCheckDate')})" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        单据状态
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <select id="sltBillStatus" runat="server">
                                            <option value="00">--请选择--</option>
                                            <option value="1">制单</option>
                                            <option value="2">执行</option>
                                            <option value="4">手工结单</option>
                                            <option value="5">自动结单</option>
                                        </select>
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        <span id="OtherConditon" style="display: none">其他条件</span>
                                    </td>
                                    <td width="24%" bgcolor="#FFFFFF">
                                        <uc6:GetBillExAttrControl ID="GetBillExAttrControl1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <img id="btnQuery" runat="server" visible="false" alt="检索" src="../../../images/Button/Bottom_btn_search.jpg"
                                            style='cursor: hand;' onclick='FistSearchReport()' /><input id="myPageIndex" value="00"
                                                type="hidden" />
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
                质检报告单列表
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <a href="StorageCheckReportAdd.aspx?myAction=fromlist&ModuleID=2071201">
                                <img id="btnAdd" runat="server" visible="false" src="../../../images/Button/Bottom_btn_new.jpg"
                                    alt="新建质检报告" style='cursor: hand;' border="0" /></a><img id="btnDel" runat="server"
                                        visible="false" src="../../../images/Button/Main_btn_delete.jpg" alt="删除质检报告"
                                        style='cursor: hand;' border="0" onclick="Fun_Delete_Report();" />
                            <asp:ImageButton ID="btnImport" OnClientClick="return IsOut();" ImageUrl="../../../images/Button/Main_btn_out.jpg"
                                runat="server" OnClick="btnImport_Click" />
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
                                <div class="orderClick" onclick="OrderBy11('ReportNo','oReportNo');return false;">
                                    单据编号<span id="oReportNo" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy11('Title','oTitle1');return false;">
                                    单据主题<span id="oTitle1" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy11('FromTypeName','oFromTypeName');return false;">
                                    源单类型<span id="oFromTypeName" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy11('OtherCorpName','oOtherCorpName');return false;">
                                    往来单位<span id="oOtherCorpName" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy11('BigTypeName','oBigTypeName');return false;">
                                    往来单位类别<span id="oBigTypeName" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy11('CheckTypeName','oCheckTypeName2');return false;">
                                    质检类别<span id="oCheckTypeName2" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy11('CheckModeName','oCheckModeName22222');return false;">
                                    检验方式<span id="oCheckModeName22222" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy11('EmployeeName','oEmployeeName2');return false;">
                                    报检人员<span id="oEmployeeName2" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy11('DeptName','oDeptName2');return false;">
                                    报检部门<span id="oDeptName2" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy11('CheckDate','oCheckDate');return false;">
                                    报检日期<span id="oCheckDate" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy11('BillStatus','oBillStatus');return false;">
                                    单据状态<span id="oBillStatus" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy11('FlowStatus','oFlowStatus');return false;">
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
