<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserRole_Query.aspx.cs" Inherits="Pages_Office_SystemManager_UserRole_Query" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户角色关联列表</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/page.js" type="text/javascript"></script>
    <script src="../../../js/office/SystemManager/UserRole_Query.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
        <style type="text/css">
        #pageDataList1 TD{color:#333333;}
          #userList 
        {
           border:solid 1px #111111;
             width:200px;
            z-index:11;
            display:none;
            position:absolute;
            background-color:White;
            
        }
        
        #editPanel
        {
            width:400px;
            background-color:#fefefe;
            position:absolute;
            border:solid 1px #000000;
            padding:5px;
        }
    </style>
    
    <script language="javascript" type="text/javascript">
        function getChildNodes(nodeTable)
      {
            if(nodeTable.nextSibling == null)
                return [];
            var nodes = nodeTable.nextSibling;  
           
            if(nodes.tagName == "DIV")
            {
                return nodes.childNodes;//return childnodes's nodeTables;
            }
            return [];
      }
        function OnTreeNodeClick()
        {
            var toEle = document.getElementById("Drp_UserInfo");
            var ele = GetEventSource();
            
            if(ele.tagName == "A")
            {
                var node = null;
                try
                {
                    node =ele.parentElement.parentElement.parentElement.parentElement;//get node Table
                                   
                }catch(e){return;}
                if( getChildNodes(node).length > 0)
                   return;
                    
            
                toEle.value = ele.innerText;
                hideUserList();
            }
            
        }
    
        function showUserList()
        {
            var list = document.getElementById("userList");
           
            if(list.style.display != "none")
            {
                list.style.display = "none";
                return;
            }
            
            var pos = elePos(document.getElementById("Drp_UserInfo"));
            
            list.style.left = pos.x;
            list.style.top = pos.y+20;
            
            document.getElementById("userList").style.display = "block";
        }
        function ClearList()
        {
         document.getElementById("Drp_UserInfo").value="";
        }
        
        function hideUserList()
        {
            document.getElementById("userList").style.display = "none";
        }
    </script>
</head>
<body>
<form id="formPackage" name="formPackage" runat="server">
<input type="hidden" name="hidRoleID" />
<input type="hidden" name="hidUserID" />
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
                                        角色名称</td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <asp:DropDownList ID="lstRoleID_Drp_RoleInfo" runat="server" Width="170px">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="10%" bgcolor="#E7E7E7" align="right">
                                        用户名
                                    </td>
                                    <td width="25%" bgcolor="#FFFFFF" colspan="3">
                          <input type="text" id="Drp_UserInfo" onclick="showUserList()" class="tdinput" runat="server"  readonly/></td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;' id="btnQuery" visible="false" runat="server"  onclick="Fun_Search_UserRoleInfo()"/>&nbsp;
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
                用户角色关联
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                <img  alt="新建" src="../../../Images/Button/Bottom_btn_new.jpg"
                 onclick="Show();" runat="server" visible="false" id="btnNew"/><img 
                                alt="" src="../../../Images/Button/Main_btn_delete.jpg" 
                                onclick="DelUserRoelInfo();" id="btnDel" runat="server" visible="false" /><asp:HiddenField ID="hidModuleID" runat="server" />
                            <input id="txtUserName" type="hidden" />
                            <asp:HiddenField ID="hidSearchCondition" runat="server" />
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
                         <%--   <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('UserID','oID');return false;">
                                    用户ID<span id="oID" class="orderTip"></span></div>
                            </th>--%>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('UserID','oGroup');return false;">
                                    用户名<span id="oGroup" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('RoleName','oC3');return false;">
                                    角色名称<span id="oC3" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ModifiedDate','oC4');return false;">
                                    最后更新日期<span id="oC4" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ModifiedUserID','oC4');return false;">
                                    最后更新用户ID<span id="Span1" class="orderTip"></span></div>
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
                                            <input name="text" type="text" id="Text3" style="display: none" />
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
    </table>    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
   <div id="userList" style="display:none;" class="MList">

<div style="background-color:#f1f1f1;padding:3px; height:20px; padding-left:50px; s padding-top:1px">
<a style="float:left" href="javascript:hideUserList()">关闭</a>
<a style="float:right;margin-right:10px" href="javascript:ClearList()">清除</a></div>
<div style=" padding-top:5px; height:300px; width:200px; overflow:auto; margin-top:1px">
    <asp:TreeView ID="TreeView1" runat="server" ShowLines="True">
    </asp:TreeView></div>
</div>
</form>
</body>
</html>
