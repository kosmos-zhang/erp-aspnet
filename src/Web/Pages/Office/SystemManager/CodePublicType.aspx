<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CodePublicType.aspx.cs" Inherits="Pages_Office_SystemManager_CodePublicType" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>公共分类</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
     <script src="../../../js/KnowledgeCenter/common.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
     <script language="javascript" type="text/javascript">
      function GetSelTreeNodes()
      {
            hideUserList();
            var idlist = "";
            var chks = document.getElementById("TreeView1").getElementsByTagName("input");
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
            document.getElementById("txt_typeflagname").value =  idlist;
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
        function showUserList()
        {
            var list = document.getElementById("userList");
           
            if(list.style.display != "none")
            {
                list.style.display = "none";
                return;
            }
           
//                 var s = null;
//            var node = null;
//            try
//            {
//                node = ele.parentElement.parentElement.parentElement.parentElement;//get node Table
//                               
//            }catch(e){return;}
//            
//            
//            //处理父节点
//            var pNode = getParentNode(node);     
//             var ele = event.srcElement; 
////            if(ele.type != 'checkbox')
////                return;
//                
//            var s = null;
//            var node = null;            
//            //处理父节点
var pNode=document.getElementById("TreeView1");
                var checkBoxs = pNode.getElementsByTagName('INPUT'); 
                for(var i=0;i<checkBoxs.length;i++) 
                { 
                    if(checkBoxs[i].type=='checkbox') 
                    {                   
                       
                            checkBoxs[i].checked = false;
                                
                    }
            }
               
               
               
               
               
            document.getElementById("txt_typeflagname").value =  "";
            document.getElementById("userList").style.display = "block";
        }
        function hideUserList()
        {
            document.getElementById("userList").style.display = "none";
        }
      
    </script>
      <style type="text/css">
        #userList 
        {
            border:solid 1px #111111;
            width:180px;
            height:300px;
            overflow:auto;    
            z-index:1;
            display:none;
            position: absolute;
            background-color:White;
            margin-left:200px;
            top: 2%;
        }
    </style>
</head>
<body>
    <form id="frmMain" runat="server">
  
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
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件<input 
                    id="hf_typeflag" type="hidden" runat="server" />
             
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
                                    <td width="15%" height="20" bgcolor="#E7E7E7" align="right">
                                    分类类别<span id="oC2" class="orderTip"></span></td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                        <input id="txt_typeflagname" onclick="showUserList()" class="tdinput" type="text" readonly /></td>
                                    <td width="15%" bgcolor="#E7E7E7" align="right">
                                        启用状态
                                    </td>
                                    <td width="45%" bgcolor="#FFFFFF">
                                       <select id="UsedStatus" runat="server" name="SetPro2" width="139px">
                                       <option value="">--请选择--</option>
                           <option value="1">启用</option>
                          <option value="0">停用</option>
                                </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center" bgcolor="#FFFFFF">
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                            onclick='Fun_Search_UserInfo()' id="btnQuery" visible="false" runat="server" /> </td>
                                    
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
                 <input id="hf_ID" type="hidden" />
                 <input id="hidSearchCondition" type="hidden" />
               分类信息列表
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
            <img alt="新建" src="../../../Images/Button/Bottom_btn_new.jpg" 
                onclick="Show();" runat="server" visible="false" id="btnNew"/><img alt="" src="../../../Images/Button/Main_btn_delete.jpg" 
                                onclick="DelCodePubInfo();" id="btnDel" runat="server" visible="false" /></td>
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
                                <div class="orderClick" onclick="OrderBy('TypeCode','oGroup');return false;">
                                    分类类别<span id="oGroup" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('TypeName','oC1');return false;">
                                    分类名称<span id="oC1" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" onclick="OrderBy('UsedStatus','oC3');return false;">
                                    启用状态<span id="Span3" class="orderTip"></span></div>
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
    </table>  <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
     <div id="userList" style="display:none;  margin-top:45px;">
<div style="background-color:#f1f1f1;padding:3px;padding-left:50px;">
<a href="javascript:hideUserList()">关闭</a>
<a style="margin-left:20px;" href="javascript:GetSelTreeNodes()">确定</a>
</div>
    <asp:TreeView ID="TreeView1" runat="server"></asp:TreeView>
</div><div id="divBackShadow" style="display: none;" >
    <iframe id="BackShadowIframe" frameborder="0"
        width="100%"></iframe></div>
    <div id="div_Add"   style="border: solid 10px #898989; background: #fff;  padding: 10px; width: 400px; height:150px; z-index: 21; position: absolute;top: 50%; left: 70%; margin: -200px 0 0 -400px; display:none ">
    <table width="92%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999" style=" margin-left:34px">
      <tr>
        <td height="28" bgcolor="#FFFFFF" align="left">
          
            <img alt="保存"  src="../../../Images/Button/Bottom_btn_save.jpg" onclick="InsertCodePublicType();" id="btnSave" runat="server" visible="false"/>
            <img alt="返回"  src="../../../Images/Button/Bottom_btn_back.jpg" onclick="Hide();" /></td>
          </tr>
      </table>
        
        <table width="100%"  border="0" align="center" cellpadding="0" cellspacing="1">
         <tr height="30px">
           <td   align="right">
               分类类别：<span class="redbold">*</span></td>
           <td  >
              <asp:DropDownList ID="drp_typecode" runat="server" Width="206px" style="z-index:1">
                    </asp:DropDownList>
                    </td>
       </tr>
        <tr height="30px">
                <td    align="right">
                    分类名称：<span class="redbold">*</span></td>
                <td >
                    <asp:TextBox ID="txt_TypeName" specialworkcheck="分类名称" Width="200px" runat="server"></asp:TextBox>
                </td>
                </tr>
        <tr height="30px">
                <td   align="right">
                    描述信息：</td>
                <td  >
                    <asp:TextBox ID="txt_Description"  pecialworkcheck="描述信息" Width="200px" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>
                </tr>
          <tr height="30px">
          <td    align="right">
                    启用状态：<span class="redbold">*</span></td>
                <td   >
                    启用<input id="rd_use" type="radio" value="1" name="RadUsedStatus"  checked=checked/>
                    停用<input id="rd_notuse" type="radio" value="0" name="RadUsedStatus" /></td>
          </tr>
        </table>

</div>
    </form>
</body>
</html>
<script src="../../../js/office/SystemManager/CodePublicType.js" type="text/javascript"></script>

