<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserInfo_Query.aspx.cs" Inherits="Pages_Office_SystemManager_UserInfo_Query" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>用户管理</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/office/SystemManager/UserInfoQuery.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            border-width: 0pt;
            background-color: #ffffff;
            height: 21px;
            margin-left: 2px;
            width: 113px;
        }
        #Select1
        {
            width: 74px;
        }
        #chklockflag
        {
            width: 135px;
        }
    </style>
    </head>
<body>
    <form id="frmMain" runat="server">
      <input  type="hidden" id="IsCompanyOpen" runat="server"/>
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                <uc1:Message ID="Message1" runat="server" />
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
             
                <uc1:Message ID="Message2" runat="server" />
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
                                        用户名&nbsp;
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <input name="txtUserID" specialworkcheck="用户名" type="text" class="style1" size="13" id="txtUserID" 
                                            runat="server" />
                                        <input id="txtUserName" type="hidden" />
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        锁定标志
                                    </td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                        <select id="chklockflag"  runat="server">
                                            <option value="">--请选择--</option>
                                            <option value="0">否</option>
                                            <option value="1">是</option>
                                        </select></td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        生效日期
                                    </td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                        &nbsp;
                                        <input id="txtOpenDate" class="tdinput" name="txtOpenDate"  readonly onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtOpenDate')})"
                                            size="15" type="text" runat="server"/></td>
                                </tr>
                                <tr class="table-item">
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        失效日期
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        &nbsp;<input id="txtCloseDate" class="tdinput" readonly name="txtCloseDate" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtCloseDate')})"
                                            size="15" type="text" />
                                        &nbsp;
                                    </td>
                                      <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        员工姓名
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                    <select id="EmployeeID" runat="server" name="SetPro1" width="139px">
                                        <option></option>
                                    </select>
                                    </td>
                                           <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                       <span id="spanHardTitle" >是否启用加密狗</span> 
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF" >
                                   <span id="spanHardContent">  <select id="selIsHardValidate" runat="server"  width="139px" >
                                        <option value="">--请选择--</option>
                                         <option value="1">是</option>
                                          <option value="0">否</option>
                                    </select></span>
                                  
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                            onclick='Fun_Search_UserInfo()'  id="btnQuery" runat="server" visible="false" />&nbsp;
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
               用户信息列表
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                <img  alt="新建" src="../../../Images/Button/Bottom_btn_new.jpg"
               onclick="Show();" runat="server" visible="false" id="btnNew"/><img alt="" src="../../../Images/Button/Main_btn_delete.jpg" 
                                onclick="DelUserInfo();" visible="false" runat="server" id="btnDel"  /><asp:HiddenField ID="hidSearchCondition" runat="server" />
    <asp:HiddenField ID="hidModuleID" runat="server" />
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
                                选择<input type="checkbox" id="btnAll" name="btnAll" onclick="OptionCheckAll();" />
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('UserID','oID');return false;">
                                    用户名<span id="oID" class="orderTip"></span></div>
                            </th>
                         <%--   <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('UserName','oGroup');return false;">
                                    用户名<span id="oGroup" class="orderTip"></span></div>
                            </th>--%>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('OpenDate','oC1');return false;">
                                    生效日期<span id="oC1" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('CloseDate','oC2');return false;">
                                    失效日期<span id="oC2" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('LockFlag','oC3');return false;">
                                    是否锁定<span id="oC3" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ModifiedDate','oC4');return false;">
                                    最后更新日期<span id="oC4" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ModifiedUserID','oC5');return false;">
                                    最后更新用户ID<span id="oC5" class="orderTip"></span></div>
                            </th>
                             <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" id="thHardTitle">
                                <div class="orderClick" onclick="OrderBy('IsHardValidate','oC7');return false;">
                                    是否启用加密狗<span id="oC7" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('remark','oC6');return false;">
                                    备注<span id="oC6" class="orderTip"></span></div>
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
                <br />
            </td>
        </tr>
    </table>
        <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>

    </form>
</body>
</html>
