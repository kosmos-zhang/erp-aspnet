<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleLicense_Query.aspx.cs" Inherits="Pages_Office_SystemManager_RoleLicense_Query" %>
<%@ Register src="../../../UserControl/RoleDrpControl.ascx" tagname="RoleDrpControl" tagprefix="uc3" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>角色权限关联</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/pagecss.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/page.js" type="text/javascript"></script>
    <script src="../../../js/office/SystemManager/RoleLicense_Query.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
  <script   language="JavaScript" type="text/javascript">  
  function dopostback()
  {   
        var   o  =   window.event.srcElement;   
          if   (o.type =="checkbox")   
          {   
               __doPostBack("","");   
          }  

  }  

        
    function GetSelMTreeNodes()
      {
            hideMList();
            
            var idlist = "";
            
            var chks = document.getElementById("TreeView2").getElementsByTagName("input");
            for(var i=1;i<chks.length;i++)
            {
                if(chks[i].type != "checkbox")
                    continue;
                if(!chks[i].checked)
                    continue;
                var node = null;
                try
                {
                    node = chks[i].parentElement.parentElement.parentElement.parentElement;//get node Table
                                   
                }catch(e){continue;}
                if( getChildNodes(node).length > 0)
                    continue;
                
                if(idlist != "")
                    idlist += ",";
                idlist += chks[i].nextSibling.innerText;   
            }
            
            document.getElementById("uds").value =  idlist;
      }
   
  
      function OnTreeNodeChecked() 
      {
            var ele = event.srcElement; 
            if(ele.type != 'checkbox')
                return;
                
            var s = null;
            var node = null;
            try
            {
                node = ele.parentElement.parentElement.parentElement.parentElement;//get node Table
                               
            }catch(e){return;}
            
            
            //处理父节点
            var pNode = getParentNode(node);     
            if(pNode != null)
            {     
                var checkBoxs = pNode.getElementsByTagName('INPUT'); 
                for(var i=0;i<checkBoxs.length;i++) 
                { 
                    if(checkBoxs[i].type=='checkbox') 
                    {                   
                        if(ele.checked)
                        {
                            if(checkBoxs[i].checked == false)
                                checkBoxs[i].checked=true; 
                        }else{
                        
                            checkBoxs[i].checked=getChildNodesChecked(pNode);
                        }                 
                                            
                        break;
                    }
                } 
            }
            
            
            //处理子节点
            var childnodes = getChildNodes(node);
            for(var i=0;i<childnodes.length;i++)
            {
                var checkBoxs = childnodes[i].getElementsByTagName('INPUT'); 
                for(var j=0;j<checkBoxs.length;j++) 
                { 
                    if(checkBoxs[j].type=='checkbox') 
                    {                   
                        checkBoxs[j].checked= ele.checked;   
                        break;
                    }
                } 
            }
        
      }
      
      
      function getChildNodesChecked(node)
      {
      
            var r = false;
            
             //处理子节点
            var childnodes = getChildNodes(node);
            for(var i=0;i<childnodes.length;i++)
            {
                var checkBoxs = childnodes[i].getElementsByTagName('INPUT'); 
                for(var j=0;j<checkBoxs.length;j++) 
                { 
                    if(checkBoxs[j].type=='checkbox') 
                    {                   
                        r =  r || checkBoxs[j].checked;                        
                        break;
                    }
                } 
            }
            
            return r;
      }
      
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
      
      function getParentNode(nodeTable)
      {
            var s = null;
            try
            {
                s = (nodeTable.parentElement) ;//get nodes DIV
                if(s.tagName != "DIV")
                    s = null;                       
                s = s.previousSibling ;//get previous Node Table
            }catch(e){s = null;}

            return s;
      }
     
        function showMList()
        {
          var list = document.getElementById("MList");
           
            if(list.style.display != "none")
            {
                list.style.display = "none";
                return;
            }
            
            var pos = elePos(document.getElementById("uds"));
            
            list.style.left = pos.x;
            list.style.top = pos.y+20;
            var pNode=document.getElementById("TreeView2");
                var checkBoxs = pNode.getElementsByTagName('INPUT'); 
                for(var i=0;i<checkBoxs.length;i++) 
                { 
                    if(checkBoxs[i].type=='checkbox') 
                    {                   
                       
                            checkBoxs[i].checked = false;
                                
                    }
            }
            document.getElementById("MList").style.display = "block";
        }
       function showBList()
        {
          var list = document.getElementById("BList");
           
            if(list.style.display != "none")
            {
                list.style.display = "none";
                return;
            }
            
            var pos = elePos(document.getElementById("ds"));
            
            list.style.left = pos.x;
            list.style.top = pos.y+20;
            
            document.getElementById("BList").style.display = "block";
        }
         function hideMList()
        {
            document.getElementById("MList").style.display = "none";
        }
         function hideBList()
        {
            document.getElementById("BList").style.display = "none";
        }
        function OnTreeNodeMChecked()
        {
          
          var ele = event.srcElement; 
        if(ele.type=='checkbox') 
        { 
        var childrenDivID = ele.id.replace('CheckBox','Nodes'); 
        var div = document.getElementById(childrenDivID); 
        if(div==null)return; 
        var checkBoxs = div.getElementsByTagName('INPUT'); 
        for(var i=0;i<checkBoxs.length;i++) 
        { 
        if(checkBoxs[i].type=='checkbox') 
        checkBoxs[i].checked=ele.checked; 
        } 
        }
        }
  </script> 
    <style type="text/css">
        .style1
        {
            width: 30%;
        }
         .MList 
        {
            border:solid 1px #111111;
             width:200px;
            z-index:11;
            display:none;
            position:absolute;
            background-color:White;
            
        }
    </style>
</head>
<body>
    <form id="frmMain" runat="server">
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    <table width="99%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
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
                                    <td width="20%" bgcolor="#FFFFFF" class="style1">
                                        &nbsp;<uc3:RoleDrpControl ID="RoleDrpControl1" runat="server" />&nbsp;
                                    </td>
                                    <td width="10%" height="20" bgcolor="#E7E7E7" align="right">
                                        操作模块
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                         <input onclick="showMList();" type="text" class="tdinput" readonly id="uds" onkeydown="return false;"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center" bgcolor="#FFFFFF">
                                        <uc4:Message ID="Message2" runat="server" />
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                            onclick='Fun_Search_RoleFunctionInfo()' id="btnQuery" visible="true" runat="server" />&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                
                
                
                <table width="99%" height="57" border="0" cellpadding="0" cellspacing="0" >
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr><td><table><tr><td height="1">
            <asp:HiddenField ID="hf_back" runat="server"  Value="0" />
            </td></tr></table></td></tr>
        <tr>
              <td height="30" colspan="2" align="center" valign="top" class="Title">
               角色权限列表
            </td>
        </tr>
        <tr>
          <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <asp:ImageButton  runat="server" 
                                ImageUrl="~/Images/Button/Button_Authorization .jpg" onclick="btnAdd_Click"  visible="true" id="btnNew"/>
                            <img onclick="DelRoleFunction();" 
                                src="../../../images/Button/Main_btn_delete.jpg" id="btnDel" runat="server" visible="false" />
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
                                选择<input id="btnAll" name="btnAll" onclick="OptionCheckAll();" type="checkbox" /></th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('RoleName','oID');return false;">
                                     角色名称<span id="oID" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ModuleName','Span1');return false;">
                                   模块名称<span id="Span1" class="orderTip"></span></div>
                            </th>                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('FunctionCD','oGroup');return false;">
                                    功能编码<span id="oGroup" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('FunctionName','oC1');return false;">
                                    功能名称<span id="oC1" class="orderTip"></span></div>
                            </th>
                           <%-- <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('CloseDate','oC2');return false;">
                                  企业编码 <span id="oC2" class="orderTip"></span></div>
                            </th>--%>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ModifiedUserID','oC3');return false;">
                                    最后更新用户<span id="oC3" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('ModifiedDate','oC3');return false;">
                                    最后更新日期<span id="Span3" class="orderTip"></span></div>
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
                
                
                
                
                
                
            </td>
        </tr>
    </table>
    
    <div id="MList" style="display:none;" class="MList">
<div style="background-color:#f1f1f1;padding:3px;padding-left:50px;">
<a href="javascript:hideMList()">关闭</a>
<a style="margin-left:20px;" href="javascript:GetSelMTreeNodes()">确定</a>
</div>

  <div style=" padding-top:5px; height:300px; width:200px; overflow:auto; margin-top:1px">
    <asp:TreeView ID="TreeView2" runat="server" ShowLines="True">
    </asp:TreeView></div>
</div> 
    </form>
</body>
</html>
