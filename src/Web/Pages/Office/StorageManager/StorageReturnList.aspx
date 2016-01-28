<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageReturnList.aspx.cs" Inherits="Pages_Office_StorageManager_StorageReturnList" %>


<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>

<%@ Register src="../../../UserControl/StorageBorrowList.ascx" tagname="StorageBorrowList" tagprefix="uc5" %>
<%@ Register src="../../../UserControl/GetBillExAttrControl.ascx" tagname="GetBillExAttrControl" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>

    <title>借货返还单列表</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>


       <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/office/StorageManager/StorageReturnList.js" type="text/javascript"></script>
<style type="text/css">
.fontBlod
{ font-weight:bold;}
</style>
    <style type="text/css">
        .tboxsize
        {
            width: 90%;
            height: 99%;
        }
        .textAlign  
        { text-align:center;
        	}
    </style> 


</head>
<body>
    <form id="form1" runat="server">
    <input id="txtPara" value=""  type="hidden" runat="server"/>
        <input type="hidden"  id="txtPageIndex" />
    <input type="hidden" id="txtPageSize"  />
    <input type="hidden" id="txtOrderBy" runat="server"  />
    <input type="hidden" id="txtIsSearch" />
 <input type="hidden" id="MoudleID" runat="server" />
  <div id="divPageMask" style="display:none">
        <iframe  id="PageMaskIframe" frameborder="0" width="100%" ></iframe>
    </div>
    <uc1:Message ID="Message1" runat="server" />
        <uc5:StorageBorrowList ID="StorageBorrowList1" runat="server" />
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td rowspan="2" align="right" valign="top">
                <div id='searchClick1'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable1','searchClick1')" />
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
                <table width="99%" border="0" align="center" cellpadding="0" id="searchtable1" cellspacing="0"
                    bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                                id="tblInterviewInfo">
                                <tr>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        借货返还单编号
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <input type="text" id="txtReturnNo"  class="tdinput tboxsize" maxlength="25"   runat="server"/>
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        借货返还单主题
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <input id="txtReturnTitle" type="text" runat="server" class="tdinput tboxsize" maxlength="50" runat="server" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        借货部门</td>
                                    <td height="20" class="tdColInput" width="24%">
                                        <input type="text" id="DeptBorrowDept"  class="tdinput tboxsize" readonly  onclick="alertdiv('DeptBorrowDept,txtBorrowDeptID')"/>
                                        <input type="hidden" id="txtBorrowDeptID" runat="server" />
                                      </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                        被借部门
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input  type="text" id="DeptOutDeptText" class="tdinput tboxsize" readonly  onclick="alertdiv('DeptOutDeptText,txtOutDeptID')" />
                                        <input type="hidden" id="txtOutDeptID" runat="server" />
                                        </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        返还仓库
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:DropDownList ID="ddlStorage" runat="server" >
                                        </asp:DropDownList>
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        返还人
                                    </td>
                                    <td height="20" class="tdColInput">
                                            <input  type="text" id="UserReturner" class="tdinput tboxsize" readonly onclick="alertdiv('UserReturner,txtReturnerID')" />
                                            <input type="hidden" id="txtReturnerID" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                          返还时间
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input type="text" id="txtStartDate"  class="tdinput "  runat="server"      readonly  style="width:100px"  onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStartDate')})"/>至<input type="text" id="txtEndDate" class="tdinput " readonly  style="width:100px" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEndDate')})" runat="server" /></td>
                                    <td height="20" align="right" class="tdColTitle">
                                        单据状态</td>
                                    <td height="20" class="tdColInput">
                                              <asp:DropDownList ID="ddlBillStatus" runat="server"  >
                                      <asp:ListItem Value="">--请选择--</asp:ListItem>
                                        <asp:ListItem Text="制单" Value="1"></asp:ListItem>
                                       <asp:ListItem Text="执行" Value="2"></asp:ListItem>
                                      <asp:ListItem Text="手工结单" Value="4"></asp:ListItem>
                                       <asp:ListItem Text="自动结单" Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                         </td>
                                    <td height="20" align="right" class="tdColTitle">
                                       对应借货单
                                       </td>
                                    <td height="20" class="tdColInput">
                                     <input type="text" id="txtStorageReturn"  class="tdinput tboxsize"  readonly  onclick="getStorageBorrowList();"/>
                                        <input type="hidden" id="txtFromBillID" value="-1" runat="server" />
                                    </td>
                                </tr>
                                  <tr class="table-item">
                                    <td height="20" bgcolor="#E7E7E7" align="right" width="10%">
                                        <span id="OtherConditon" style="display:none">其他条件</span>
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                        <uc2:GetBillExAttrControl ID="GetBillExAttrControl1" runat="server" />
                                    </td>
                                      <td height="20" bgcolor="#E7E7E7" align="right" width="10%">
                                      
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                       
                                    </td>
                                      <td height="20" bgcolor="#E7E7E7" align="right" width="10%">
                                      
                                    </td>
                                    <td width="23%" bgcolor="#FFFFFF">
                                      
                                    </td>
                                </tr>
                                                                 <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor:pointer;'
                                            onclick='TurnToPage(1);'  runat="server"  visible="false" id="img_btn_search" />
                                    
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
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
                借货返还单列表
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img src="../../../images/Button/Bottom_btn_new.jpg" alt="新建" style='cursor:pointer;'
                                onclick="createNew();"  id="img_btn_new" runat="server" visible="false"/>
                            <img id="img_btn_del" src="../../../images/Button/Main_btn_delete.jpg" alt="删除" style='cursor:pointer;'
                                border="0" onclick="storageDelete();"  visible="false" runat="server" />
                                   <asp:ImageButton ID="imgOutput" runat="server" AlternateText="导出"  
                                ImageUrl="../../../images/Button/Main_btn_out.jpg" 
                                onclick="imgOutput_Click" />
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
                                选择<input type="checkbox" id="chk_StorageList"  onclick="selectAll();"/>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ReturnNo','ReturnNo');return false;">
                                   借货返还单编号<span id="ReturnNo" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('Title','Title');return false;">
                                    借货返还单主题<span id="Title" class="orderTip"></span></div>
                            </th>
                              <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('BorrowDept','BorrowDept');return false;">
                                    借货部门<span id="BorrowDept" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('StorageName','StorageName');return false;">
                                    返还仓库<span id="StorageName" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ReturnName','ReturnName');return false;">
                                    返还人<span id="ReturnName" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ReturnDate','ReturnDate');return false;">
                                    返还时间<span id="ReturnDate" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('BillStatus','BillStatus');return false;">
                                   单据状态<span id="BillStatus" class="orderTip"></span></div>
                            </th>
                              <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('BorrowNo','BorrowNo');return false;">
                                   对应借货单编号<span id="BorrowNo" class="orderTip"></span></div>
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
                                                <input name="text" type="text" id="ShowPageCount" onblur="checkPage('ShowPageCount','每页显示数量');"/>
                                                条 转到第
                                                <input name="text" type="text" id="ToPage"  onblur="checkPage('ToPage','页码');" />
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