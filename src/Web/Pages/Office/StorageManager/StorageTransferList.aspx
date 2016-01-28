<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageTransferList.aspx.cs" Inherits="Pages_Office_StorageManager_StorageTransferList" %>



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

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>


       <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/office/StorageManager/StorageTransferList.js" type="text/javascript"></script>
    
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


    <title>库存调拨单列表</title>

</head>
<body>
    <form id="form1" runat="server">
    <input id="txtPara" value=""  type="hidden" runat="server"/>
       <input type="hidden"  id="txtPageIndex" />
    <input type="hidden" id="txtPageSize"  />
    <input type="hidden" id="txtOrderBy" runat="server" />
    <input id="txtIsSearch" type="hidden" />
       <input id="MoudleID" type="hidden"  runat="server"/>
       <input id="ModuleInOutID" type="hidden"  runat="server"/>
           <input type="hidden" id="hidSelPoint" runat="server" />
    <input  type="hidden" id="hidIsBatchNo" runat="server"/>
    <uc1:Message ID="Message1" runat="server" />
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
                <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                                id="searchtable" >
                                <tr>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        调拨单编号
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                       <input type="text" id="txtTransferNo" class="tdinput tboxsize"  runat="server" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        调拨单主题
                                    </td>
                                    <td height="20" class="tdColInput" width="23%">
                                        <input id="txtTitle" type="text" runat="server" class="tdinput tboxsize" maxlength="50" />
                                    </td>
                                    <td height="20" align="right" class="tdColTitle" width="10%">
                                        调拨申请人
                                    </td>
                                    <td height="20" class="tdColInput" width="24%">
                                        <input type="text" id="UserApplyUser" onclick="alertdiv('UserApplyUser,txtApplyUserID')"
                                            readonly class="tdinput tboxsize" />
                                        <input type="hidden" id="txtApplyUserID" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                        要货部门
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input type="text" id="DeptApplyDept" class="tdinput tboxsize" readonly onclick="alertdiv('DeptApplyDept,txtApplyDeptID')" />
                                        <input type="hidden" id="txtApplyDeptID"  runat="server"/>
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        调入仓库
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:DropDownList ID="ddlInStorageID" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                        要求到货日期
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <input type="text" id="txtRequireInDate" class="tdinput tboxsize" readonly onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtRequireInDate')})"  runat="server"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" align="right" class="tdColTitle">
                                       调货部门</td>
                                    <td height="20" class="tdColInput">
                                          <input type="text" id="DeptOutDeptID" class="tdinput tboxsize" onclick="alertdiv('DeptOutDeptID,txtOutDeptID')"
                                            readonly />
                                        <input type="hidden" id="txtOutDeptID"  runat="server"/>
                                         
                                         </td>
                                    <td height="20" align="right" class="tdColTitle">
                                          调出仓库
                                    </td>
                                    <td height="20" class="tdColInput">
                                        <asp:DropDownList ID="ddlOutStorageID" runat="server" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                      审批状态
                                    </td>
                                    <td height="20" class="tdColInput">
                                         <asp:DropDownList ID="ddlConfirmStatus" runat="server">
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
                                        业务状态 
                                    </td>
                                    <td height="20" class="tdColInput">
                                       <asp:DropDownList ID="ddlBusiStatus" runat="server" >
                                        <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                                            <asp:ListItem Value="1">调拨申请</asp:ListItem>
                                            <asp:ListItem Value="2">调拨出库</asp:ListItem>
                                            <asp:ListItem Value="3">调拨入库</asp:ListItem>
                                            <asp:ListItem Value="4">调拨完成</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                       单据状态
                                    </td>
                                    <td height="20" class="tdColInput">
                                       <asp:DropDownList ID="ddlBillStatus" runat="server" >
                                     <asp:ListItem Value="-1" Text="--请选择--"></asp:ListItem>
                                            <asp:ListItem Text="制单" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="执行" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="手工结单" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="自动结单" Value="5"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td height="20" align="right" class="tdColTitle">
                                    
                                        <span id="OtherConditon" style="display:none">其他条件</span></td>
                                    <td height="20" class="tdColInput">
                                     
                                        <uc2:GetBillExAttrControl ID="GetBillExAttrControl1" runat="server" />
                                     
                                    </td>
                                </tr>
                                      <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style="cursor:pointer;" onclick="TurnToPage(1);"   id="img_btn_search" runat="server" visible="false"/>
                                       <%-- <img alt="重置" src="../../../images/Button/Bottom_btn_re.jpg" style='cursor: hand;'
                                            onclick="Fun_ClearInput()" width="52" height="23" id="imgReset"/>--%>
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
               库存调拨单列表
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img src="../../../images/Button/Bottom_btn_new.jpg" alt="新建" style='cursor:pointer; float:left'
                                onclick="createNew();"  id="img_btn_new" runat="server"  visible="false"/>
                            <img id="img_btn_del" runat="server" visible="false" src="../../../images/Button/Main_btn_delete.jpg" alt="删除" style='cursor:pointer; float:left'
                                border="0" onclick="storageDelete();"   />
                                                                   <asp:ImageButton ID="imgOutput" 
                                runat="server" AlternateText="导出"  
                                ImageUrl="../../../images/Button/Main_btn_out.jpg" onclick="imgOutput_Click" 
                                 />
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
                                <div class="orderClick" onclick="OrderBy('TransferNo','TransferNo');return false;">
                                   调拨单编号<span id="TransferNo" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('Title','Title');return false;">
                                    调拨单主题<span id="Title" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ApplyUserID','ApplyUserID');return false;">
                                    调拨申请人<span id="ApplyUserID" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ApplyDeptID','ApplyDeptID');return false;">
                                    要货部门<span id="ApplyDeptID" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('InStorageID','InStorageID');return false;">
                                    调入仓库<span id="InStorageID" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('RequireInDate','RequireInDate');return false;">
                                   要求到货日期<span id="RequireInDate" class="orderTip"></span></div>
                            </th>
                            
                            
   <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('OutDeptID','OutDeptID');return false;">
                                   调货部门<span id="OutDeptID" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('OutStorageID','OutStorageID');return false;">
                                   调出仓库<span id="OutStorageID" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('TransferCount','TransferCount');return false;">
                                    调拨数量<span id="TransferCount" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('TransferPrice','TransferPrice');return false;">
                                    调拨金额<span id="TransferPrice" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('BillStatus','BillStatus');return false;">
                                    单据状态<span id="BillStatus" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('FlowStatus','FlowStatus');return false;">
                                   审批状态<span id="FlowStatus" class="orderTip"></span></div>
                            </th>
                                   <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('BusiStatus','BusiStatus');return false;">
                                   业务状态<span id="BusiStatus" class="orderTip"></span></div>
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
                                                <input name="text" type="text" id="ShowPageCount" onblur="checkPage('ShowPageCount',1)" />
                                                条 转到第
                                                <input name="text" type="text" id="ToPage" onblur="checkPage('ToPage',2)"/>
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
