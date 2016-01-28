<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageBorrowList.aspx.cs"
    Inherits="Pages_Office_StorageManager_StorageBorrowList" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register src="../../../UserControl/GetBillExAttrControl.ascx" tagname="GetBillExAttrControl" tagprefix="uc6" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/office/StorageManager/StorageBorrowList.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <style type="text/css">
        .fontBlod
        {
            font-weight: bold;
        }
    </style>

    <title>借货单列表</title>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="txtSearchPara" />
    <uc1:Message ID="Message1" runat="server" />
    <input type="hidden" id="txtPageIndex" runat="server" />
    <input type="hidden" id="txtPageSize" runat="server" />
    <input type="hidden" id="txtOrderBy" runat="server" />
    <input type="hidden" id="MoudleID" runat="server" />
          <input type="hidden" id="hidSelPoint" runat="server" />
    <input  type="hidden" id="hidIsBatchNo" runat="server"/>
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td rowspan="2" align="right" valign="top">
                <div id='searchClick'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" />
                </div>
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
                            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                                id="tblInterviewInfo">
                                <tr>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        借货单编号
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <input id="tboxBorrowNo" type="text" runat="server" class="tdinput tboxsize" maxlength="25" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        借货单主题
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <input id="tboxTitle" type="text" runat="server" class="tdinput tboxsize" maxlength="50" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        借货人
                                    </td>
                                    <td height="20" class="tdColInput" width="24%">
                                        <input type="hidden" id="txtBorrower" runat="server" />
                                        <input type="text" id="UserBorrower" onclick="alertdiv('UserBorrower,txtBorrower')"
                                            readonly class="tdinput tboxsize" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                        借货部门
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input type="text" id="DeptBorrowDeptName" readonly onclick="alertdiv('DeptBorrowDeptName,txtBorrowDeptID')"
                                            class="tdinput tboxsize" />
                                        <input type="hidden" id="txtBorrowDeptID" runat="server" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        &nbsp;借货日期
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:TextBox ID="tboxBorrowStartTime" runat="server" MaxLength="10" CssClass="tdinput"
                                            onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('tboxBorrowStartTime')})"
                                            ReadOnly="true" Width="30%"></asp:TextBox>至<asp:TextBox ID="tboxBorrowEndTime" runat="server"
                                                MaxLength="10" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('tboxBorrowEndTime')})"
                                                ReadOnly="true" Width="30%"></asp:TextBox>
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        &nbsp;被借仓库
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:DropDownList ID="ddlDepot" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                        &nbsp;被借部门
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input type="hidden" id="txtOutDept"  runat="server"/>
                                        <input type="text" id="DeptOutDept" onclick="alertdiv('DeptOutDept,txtOutDept')"
                                            readonly class="tdinput tboxsize" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        出库人
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input type="hidden" id="txtTransactor"  runat="server"/>
                                        <input type="text" id="UserTransactor" class="tdinput tboxsize" onclick="alertdiv('UserTransactor,txtTransactor')" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        审批状态
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:DropDownList ID="ddlSubmitStatus" runat="server">
                                            <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                                            <asp:ListItem Value="0">待提交</asp:ListItem>
                                            <asp:ListItem Value="1">待审批</asp:ListItem>
                                            <asp:ListItem Value="2">审批中</asp:ListItem>
                                            <asp:ListItem Value="3">审批通过</asp:ListItem>
                                            <asp:ListItem Value="4">审批不通过</asp:ListItem>
                                            <asp:ListItem Value="5">撤销审批</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                        &nbsp;单据状态
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:DropDownList ID="ddlBillStatus" runat="server" >
                                            <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                                            <asp:ListItem Text="制单" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="执行" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="手工结单" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="自动结单" Value="5"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                     <span id="OtherConditon" style="display:none">其他条件</span>
                                    </td>
                                    <td height="20" class="tdColInput"> <uc6:GetBillExAttrControl ID="GetBillExAttrControl1" runat="server" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                    </td>
                                    <td height="20" class="tdColInput">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: pointer;'
                                            onclick='TurnToPage(1);' id="img_btn_search" runat="server" visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="98%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
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
                借货申请单列表
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img src="../../../images/Button/Bottom_btn_new.jpg" alt="新建" style='cursor: pointer;'
                                onclick="createNew();" id="img_btn_new" runat="server" visible="false" />
                            <img id="img_btn_del" src="../../../images/Button/Main_btn_delete.jpg" alt="删除" style='cursor: pointer;'
                                border="0" onclick="storageDelete();" visible="false" runat="server" />
                            <asp:ImageButton ID="imgOutput" runat="server" AlternateText="导出"  
                                ImageUrl="../../../images/Button/Main_btn_out.jpg" onclick="imgOutput_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" id="tblStoragelist"
                    bgcolor="#999999">
                    <tbody>
                        <tr>
                            <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                选择<input type="checkbox" id="chk_StorageList" onclick="selectAll();" />
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('BorrowNo','BorrowNo');return false;">
                                    借货单编号<span id="BorrowNo" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('title','title');return false;">
                                    借货单主题<span id="title" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('DeptID','Dept');return false;">
                                    借货部门<span id="Dept" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('Borrower','Borrower');return false;">
                                    借货人<span id="Borrower" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('OutDeptID','OutDept');return false;">
                                    被借部门<span id="OutDept" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('StorageID','Storage');return false;">
                                    被借仓库<span id="Storage" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('CountTotal','CountTotal');return false;">
                                    借货数量<span id="CountTotal" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('TotalPrice','BorrowPrice');return false;">
                                    借货金额<span id="BorrowPrice" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('Transactor','Outer');return false;">
                                    出库人<span id="Outer" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('OutDate','OutDate');return false;">
                                    出库时间<span id="OutDate" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('BillStatus','BillStatus');return false;">
                                    单据状态<span id="BillStatus" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('FlowStatus','FlowStatus');return false;">
                                    审批状态<span id="FlowStatus" class="orderTip"></span></div>
                            </th>
                        </tr>
                    </tbody>
                </table>
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
                                            <input name="text" type="text" id="ShowPageCount" onblur="checkPage('ShowPageCount',1)" />
                                            条 转到第
                                            <input name="text" type="text" id="ToPage" onblur="checkPage('ToPage',2)" />
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
    </form>
</body>
</html>
