<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PerformanceTemplate.aspx.cs" Inherits="Pages_Office_HumanManager_PerformanceTemplate" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>考核模板设置</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/office/HumanManager/PerformanceTemplate.js" type="text/javascript"></script>
    <%--<script src="../../../js/personal/MessageBox/UserListCtrl2.js" type="text/javascript"></script>--%>
    <script src="../../../js/personal/common.js" type="text/javascript"></script>
    <style type="text/css">
        
        #selUserBox{background:#ffffff;}
        
        #userList  {border:solid 1px #3366cc;width:200px;height:300px;overflow:auto;padding-left:10px;}
        
        #typeListTab {background: #2255bb;padding:5px;margin:0px;width:202px;background:#3366cc;}
       /* #typeListTab LI{cursor:pointer;display:inline;color:White;margin-left:5px;border:solid 1px #0000ff;padding:2px;}
       */
       
       
       .tab{cursor:pointer;display:inline;color:White;background-color:inherit;margin-left:5px;border:solid 1px #0000ff;padding:2px;}
       .selTab{cursor:pointer;display:inline;color:Black;background-color:White;margin-left:5px;border:solid 1px #0000ff;padding:2px;}
       
       
       
        #editPanel
        {
            width:400px;
            background-color:#fefefe;
            position:absolute;
            border:solid 1px #000000;
            padding:5px;
        }
        
       
            #selPerformanceType
        {
            width: 85px;
        }
        
        #inpSearchTitle
        {
            width: 171px;
        }
        
        
        
        
        
        
        
 .tab_a{width:99%px; background-color:#FFFFFF ; border-collapse:collapse; border:1px #D1D1D1 solid; border-spacing:0px; margin:0 auto 8px}

.tab_a th{ padding:4px 6px; border-right:1px #D1D1D1 solid; border:1px #D1D1D1 solid;background:#F4F4F4; text-align:left}
.tab_a td{ padding:4px 6px;  border:1px #D1D1D1 solid}
        
        
        
        </style>
        
     <script type="text/javascript">
     

//var UserListItem = {text:"",value:"",groupid:""};
/*
    Tree UI
*/

var treenodes = [];
var curLevel = 0;

function BuildTree(nodes)
{
    treenodes = [];
    
    treeview_selnode = null;
    treeview_selnodeindex = -1;
      
    treeview_selNodes = [];
    curLevel = 0;

    var container = document.getElementById("userList");
    
    container.innerHTML = "";    
         
    var tb = document.createElement("TABLE");
    //tb.border="1";
    tb.cellSpacing="0";
    tb.cellPadding="0";
    
    container.appendChild(tb);
    
    for(var i=0;i<nodes.length;i++)
    {
        nodes[i].pIndex = -1;
        BuildSubNodes(nodes[i],tb);
    }
    
}

function BuildSubNodes(node,tb)
{
    if(curFlag == 0)
    {
        for(var i=0;i<treenodes.length;i++)
        {
            if(treenodes[i].value == node.value)
            {
                return;
            }
        }
    }

    node.index = treenodes.length;
    treenodes.push(node);
    
    
    

    var tr = tb.insertRow(-1);
    var sunCount=node.SubNodes.length;
   //alert (curLevel);
    if((node.SubNodes.length>0)||(curLevel==0))
    {
       tr.insertCell(-1).innerHTML = "<img onclick=\"treeview_expand(this)\" src=\"/images/treeimg/WebResource6.gif\">";
    }else{
       tr.insertCell(-1).innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;";
    }    
    tr.insertCell(-1).innerHTML = "<input id=\"treeview_checkbox"+node.index+"\" class='treeClass' title='"+node.index+"_"+node.text+"_"+node.value+"_"+sunCount+"' type=checkbox onclick=\"treeview_onselnode("+node.index+")\" value=\""+node.value+"\"><a onmouseover=\"treeview_onmove("+node.index+",1)\" onmouseout=\"treeview_onmove("+node.index+",0)\" id=\"treeview_node"+node.index+"\" style=\"color:black;\" href=\"javascript:treeview_onselnode("+node.index+");\">"+node.text+"</a>";
    
    curLevel++;
    if(node.SubNodes.length > 0)
    {
        var subTb = document.createElement("TABLE");
        //subTb.border="1";
        subTb.cellSpacing="0";
        subTb.cellPadding="0";
        subTb.style.display="";
    
        var ttr = tb.insertRow(-1);
        ttr.insertCell(-1);
        ttr.insertCell(-1).appendChild(subTb);
    
        for(var i=0;i<node.SubNodes.length;i++)
        {            
            node.SubNodes[i].pIndex = node.index;
            
            BuildSubNodes(node.SubNodes[i],subTb);
        }
        
    }
    curLevel--;
}


//----------------------
function treeview_expand(obj)
{
    var tr = obj.parentNode.parentNode;
    if (tr.nextSibling )
    {
    var ntr = tr.nextSibling.cells[1].firstChild;
    
    if(ntr.style.display == "none")
    {
        ntr.style.display = "";
        obj.src = "/images/treeimg/WebResource6.gif";
    }else{
        ntr.style.display = "none";
        obj.src = "/images/treeimg/WebResource5.gif";
    }
    }
}

var treeview_selnode = null;
var treeview_selnodeindex = -1;
var treeview_selNodes = [];
function treeview_onselnode(idx)
{   
    var obj = document.getElementById("treeview_node"+idx);
    if(treeview_selnode != null)
    {
        treeview_selnode.style.color = "#000000";
    }
        
    obj.style.color = "#ff0000";
    treeview_selnode = obj;
    treeview_selnodeindex = idx;
    
    
    var srcEle = GetEventSource();
    if( srcEle == null)//is A
    {
        obj = obj.previousSibling;           
    }
    
    obj.checked = !obj.checked ;    
    
 
    treeview_addSel(idx);
    
    treeview_allchks(idx);//checkbox 连动
    treeview_allchks2(idx);//checkbox 连动
    
    
    //get seluseridlist
    var userlist = "";
    var useridlist = "";
    for(var i in treeview_selNodes)
    {
        if(treenodes[treeview_selNodes[i]].isUser != true)
            continue;
        var tid = treenodes[treeview_selNodes[i]].value;
        
        
        if((","+useridlist+",").indexOf(","+tid+",") != -1)
        {
            continue;
        }
       
       if(userlist != "")
       {
        userlist+= ",";
        useridlist += ",";
       }         
        userlist += treenodes[treeview_selNodes[i]].text;
        useridlist += tid;
    }
    var list=new Array ();
      for(var i in treeview_selNodes)
    {
  
    
    if (treenodes[treeview_selNodes[i]].isUser!=true )
    {
    list.push (treeview_selNodes[i]+"_"+"1");
    }
    else
    {
 //   alert ("true");
  list.push (treeview_selNodes[i]+"_"+"0");
    }
    }
    
  
                //alert (treeview_selNodes);
                //alert(userlist);
                 //alert(useridlist);
                // alert (useridlist);
                 var emList=$(".treeClass");
  var len=emList.length;
  var selectedNoes=new Array ();
  for (var i=0;i<len ;i++)
   {
        if (emList[i] .checked==true )
       {
       if (emList [i].parentNode.parentNode)
       {
       var parentSilbing=emList [i].parentNode.parentNode.firstChild.innerHTML;
       var tempSign;
          if (parentSilbing =="&nbsp;&nbsp;&nbsp;&nbsp;")
         {
          //alert ("1");
         // tempSign=1;
         }
         else
         {
          //alert ("0");
          // tempSign=0;
         }
         
        
         
         var titleTemp=emList [i].title;
         var dd=titleTemp .split("_");
        // alert (dd);
        for (var s=0;s<list .length ;s++)
         {
             var ss=list[s].split("_");
             if (dd[0]==ss[0])
             {
             tempSign=ss[1];
             break ;
             }else
             {
             continue ;
             }
         }
         selectedNoes .push (dd[0]+"_"+tempSign +"_"+dd[1]+"_"+dd[2]+"_"+dd[3]);//行-是否为父节点-节点名称-节点ID--是否有子节点
         
       }
      
       }
  
   } 
   //alert (selectedNoes);
      document.getElementById("txtSender").value = userlist;
    document.getElementById("txtSenderHidden").value = selectedNoes;
                 
                 
                 
}

function getStatrMess()
{
var emList=$(".RateClass");
             var len=emList.length;
             var message=new Array ();
        for (var i=0;i<len ;i++)
          {
            message .push (emList[i].title+"_"+emList[i].value);
           }
           
   treeview_selNodes = [];
    for(var i=0;i<treenodes.length;i++)
    {
             for (var aTemp=0;aTemp <message .length ;aTemp ++)
                    {
                            var bTemp=message[aTemp ] .split ("_");
                            var cTemp=document.getElementById("treeview_checkbox"+i).value;
                            if (cTemp==bTemp [0])
                            {     document.getElementById("treeview_checkbox"+i).checked = true;
                            treeview_onselnode( i);
                               treeview_selNodes.push(i);     
                       
                            }
                    }
     
     
    }

      
}
function CreateTb()
{

//  var tr = tb.insertRow(-1);
//   
//    if(node.SubNodes.length>0)
//    {
//       tr.insertCell(-1).innerHTML = "<img onclick=\"treeview_expand(this)\" src=\"/images/treeimg/WebResource6.gif\">";
//    }else{
//       tr.insertCell(-1).innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;";
//    }    
//    tr.insertCell(-1).innerHTML = "<input id=\"treeview_checkbox"+node.index+"\" class='treeClass' title='"+node.index+"_"+node.text+"_"+node.value+"' type=checkbox onclick=\"treeview_onselnode("+node.index+")\" value=\""+node.value+"\"><a onmouseover=\"treeview_onmove("+node.index+",1)\" onmouseout=\"treeview_onmove("+node.index+",0)\" id=\"treeview_node"+node.index+"\" style=\"color:black;\" href=\"javascript:treeview_onselnode("+node.index+");\">"+node.text+"</a>";
//    
var fieldText = "";
    //出错提示信息
       var msgText = "";
  var emList=$(".treeClass");
  var len=emList.length;
  var selectedNoes=new Array ();
  var itemCheck=false ;
  for (var i=0;i<len ;i++)
   {
        if (emList[i] .checked==true )
       {
       
       itemCheck =true;
       }
   }
   if (!itemCheck )
   {
     fieldText += "指标项|";
   	   	 msgText += "请选择指标|";
         
           popMsgObj.Show(fieldText, msgText);
         return ;
   
   
   }

      var emList=$(".RateClass");
             var len=emList.length;
             var message=new Array ();
        for (var i=0;i<len ;i++)
          {
            message .push (emList[i].title+"_"+emList[i].value);
           }


//0_0_道德指标 _75_2,1_1_生活指标_123_0,2_1_方法指标_438_0,3_0_zhege_15_0

var selecList= document.getElementById("txtSenderHidden").value;

var select=selecList .split (",");
//alert (selecList);
var tb=document.getElementById("tbEdit");//行-是否为父节点-节点名称-节点ID--是否有子节点
var resultStr="";
for (var i=0;i<select .length;i++)
{
    
    var selectItem=select[i] .split ("_");
    //alert (selectItem );
    if (selectItem[1]==1)//说明是父级节点
    {
    
        if (selectItem[4]==0 )
       {
            var tem='' ;
                           for (var aTemp=0;aTemp <message .length ;aTemp ++)
                    {
                            var bTemp=message[aTemp ] .split ("_");
                            if (selectItem [3]==bTemp [0])
                            {
                          
                                    tem =bTemp [1];
                                    break ;
                            }
                    }
                   resultStr +="<tr class='newrow'><td height='22' align='center'><input  type='checkbox'       class='Delete'  title='0'  onpropertychange='getChageFlow(this)'/></td><td colspan='2' height='22' align='center'>"+selectItem [2]+"</td><td><input type  ='text' size='10' class='RateClass' title='"+selectItem [3]+"'  value='"+tem +"'  maxlength='10' style='border:none;border-bottom:solid   0px   black;'  /></td></tr>";
          
        }else
       {
          resultStr +="";
          var sign=0;
          var count=1;
          var selectItemTempdd=select[i+1] .split ("_");
         
          var temp=selectItemTempdd[4];

          if (parseInt ( temp,10) ==0)
          {      
         
          for (var sj=i+2;sj<select.length;sj++ )
                  {
                       var selectItemsdTempd=select[sj] .split ("_");
                         
                     //  alert (selectItemsdTempd);
                       if (selectItemsdTempd[1]==0)
                       {
                           if (selectItemsdTempd[4]==0  )
                           {
                           count ++;
                           }else
                           {
                           break ;
                           }
                       }else
                       {
                         break ;
                       }
                  }
          
          }
          else
          {
            count =1;
          }
        // alert (count );
           for (var a=1;a<=count ;a++)
          { 
                var selectItemTemp=select[i+a] .split ("_");
             var tem2='' ;
                           for (var aTemp=0;aTemp <message .length ;aTemp ++)
                    {
                            var bTemp=message[aTemp ] .split ("_");
                            if (selectItemTemp [3]==bTemp [0])
                            {
                            
                                    tem2 =bTemp [1];
                                    break ;
                            }
                    }
                    
      
           // alert (selectItemTemp);
            if (sign==0)
            {
            
              resultStr +="<tr class='newrow'><td height='22' align='center'><input  type='checkbox'   class='Delete'   title='1' onpropertychange='getChageFlow(this)' /></td><td rowspan="+count+"  height='22' align='center'>"+selectItem [2]+"</td><td height='22' align='center'>"+selectItemTemp[2]+"</td><td><input type='text' size='10' class='RateClass' title='"+selectItemTemp [3]+"' maxlength='10'  value='"+tem2 +"'  class='tdinput'  style='border:none;border-bottom:solid   0px   black;'/></td></tr>"
              sign=sign+1;
             }
             else
             {
             
             resultStr +="<tr class='newrow'><td></td> <td height='22' align='center'>"+selectItemTemp[2]+"</td><td><input type='text' size='10'  value='"+tem2 +"'  class='RateClass' title='"+selectItemTemp [3]+"'   maxlength='10' class='tdinput' style='border:none;border-bottom:solid   0px   black;'/></td></tr>";
             }
          
          }
          
           
           
           
           i=i+count ;
           
           
           
       }
    }
    else
    {
    
    
   // resultStr +="<table><tr><td>"+selectItem [2]+"</td><td><input type='text' size='10' class='RateClass' title='"+selectItem [3]+"' maxlength='10'/><td><td ><input  type='checkbox' class='Delete' /></td></tr></table>";
    
    
    }
    
 
    
    
    
    
//    if (selectItem [1]==0)
//   {  
//       var tr = tb.insertRow(-1);
//       tr.insertCell(-1).innerHTML=selectItem [2];
//       
//       if (selectItem [4]==0)
//       {  
//         tr.insertCell(-1).innerHTML="<input type='text' size='10' class='RateClass' title='"+selectItem [3]+"' maxlength='10'/>";
//         tr.insertCell(-1).innerHTML="<input  type='checkbox'       class='Delete' />";
//       }
//    }
//    else
//   {
//       if (tb.lastChild.lastChild.lastChild.firstChild.lastChild)
//      {
//         var newTr=tb.lastChild.lastChild.lastChild.lastChild ;
//         var newNode = document.createElement("tr");
//         var ddd=document.createElement("td");
//         ddd.innerHTML=selectItem [2];
//         var sfs=document.createElement("td");
//         sfs.innerHTML="<td><input type='text' size='10' class='RateClass' title='"+selectItem [3]+"'maxlength='10'/><td>";  
//         var sff=document.createElement("td");
//         sff.innerHTML="<td ><input  type='checkbox' class='Delete' /></td>"
//         newNode.appendChild(ddd );
//         newNode.appendChild(sfs );
//         newNode.appendChild(sff );
//         newTr.lastChild .appendChild(newNode );
//       }else
//       {
//         var newTr=tb .lastChild;
//         var newNode = document.createElement("td");
//         newNode.innerHTML ="<table><tr><td>"+selectItem [2]+"</td><td><input type='text' size='10' class='RateClass' title='"+selectItem [3]+"' maxlength='10'/><td><td ><input  type='checkbox' class='Delete' /></td></tr></table>";
//         newTr.lastChild .appendChild(newNode );
//        }
//     
//    }
}
//alert (tb.innerHTML );
//alert (resultStr );

//debugger ;
if (tb.tBodies [0])
{
//alert ("1");

 
   tb.outerHTML="<table id='tbEdit' width='100%' border='0' cellspacing='0' cellpadding='3'  class='tab_a'><tr><th>选择<input type=\"checkbox\" id=\"chkCheckAll2\" name=\"chkCheckAll2\" onclick=\"changEdit(this);\"/></th><th  colspan='2' width='100'>指标名称</th><th width='200' >权重</th></tr><tbody>"+resultStr+"</tbody></table>" ;
   }
   else
   {
  // alert ("2");
   }
var selectItem=select[0] .split ("_");
//alert (select);
//alert (select.length);
//alert (selectItem);
//alert (select);
  var box = document.getElementById("selUserBox");
        
            box.style.display = "none";
            clearSel();


}

function treeview_addSel(idx)
{
//    //只获取叶子节点
//    if( treenodes[idx].SubNodes.length != 0 )
//        return;
        
    var obj = document.getElementById("treeview_checkbox"+idx);
     if(obj.checked)
    {
        for(var m in treeview_selNodes)
        {
            if(treeview_selNodes[m] == idx)
                break;
        }
        treeview_selNodes.push(idx);
    }else{
        for(var m in treeview_selNodes)
        {
            if(treeview_selNodes[m] == idx)
            {
                var i = m;
                var arr = treeview_selNodes;
                i=parseInt(i);
                for(var j=i;j<arr.length-1;j++)
                {        
                    arr[j] = arr[j+1];
                }
                arr.length--;                       
                break;
            }
        }
    }
}

function treeview_allchks(idx)
{
    var chk = document.getElementById("treeview_checkbox"+idx);
    
    //有子节点
    if(treenodes[idx].SubNodes.length>0)
    {       
            for(var i in treenodes[idx].SubNodes)
            {
                var chk2 = document.getElementById("treeview_checkbox"+treenodes[idx].SubNodes[i].index);
                chk2.checked = chk.checked;
                treeview_addSel(treenodes[idx].SubNodes[i].index);
                
                treeview_allchks(treenodes[idx].SubNodes[i].index);
                
            }       
   } 

}

function treeview_allchks2(idx)  
{
    var chk = document.getElementById("treeview_checkbox"+idx);
    
    //有父节点
    if(treenodes[idx].pIndex != -1)
    {
        if(!chk.checked )
        {
            var selCountOfPNode = 0;
            for(var i in treenodes[treenodes[idx].pIndex].SubNodes)
            {
                var tchildIndex = treenodes[treenodes[idx].pIndex].SubNodes[i].index;
                if(document.getElementById("treeview_checkbox"+tchildIndex).checked)
                {
                    selCountOfPNode++;
                }
            }
            
            if(selCountOfPNode > 0)
                return;
        }
    
        var chk2 = document.getElementById("treeview_checkbox"+treenodes[idx].pIndex);
        chk2.checked = chk.checked;
         treeview_addSel(treenodes[idx].pIndex);
         
        treeview_allchks2(treenodes[idx].pIndex);
        return;
    }
}

function treeview_onmove(idx,flag)
{
    if(treeview_selnodeindex == idx)
        return;
        
    var obj = document.getElementById("treeview_node"+idx);
    if(flag == 1)
    {
        obj.style.color = "#ff0000";
    }else{
        obj.style.color = "#000000";
    }
}


function treeview_unsel()
{
    if(treeview_selnodeindex != -1)
    {
         var obj = document.getElementById("treeview_node"+treeview_selnodeindex);
        obj.style.color = "#000000";   
    }

   for(var i=0;i<treeview_selNodes.length;i++)
   {
 
        var chk2 = document.getElementById("treeview_checkbox"+treeview_selNodes[i]);
         
        chk2.checked = false;
   }
   treeview_selNodes.length = 0;
   
  treeview_selnode = null;
  treeview_selnodeindex = -1;

   
}

var selalled = false;
function treeview_selall()
{
    if(selalled)
    {
        treeview_unsel();
        selalled = false;
        return;
    }

    treeview_selNodes = [];
    for(var i=0;i<treenodes.length;i++)
    {
        treeview_selNodes.push(i);        
        document.getElementById("treeview_checkbox"+i).checked = true;
    }
    
    getsellist();
    
    selalled = true;
}
     
     </script>
     <script language="javascript" type="text/javascript">
        function showInfo(msg)
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msg);
        }
    
    
        $(document).ready(function(){
              swithEditPanel(1);
            LoadUserList('LoadUserListWithDepartment',BuildTree);      
                     
            
        });
        
        var curFlag = 0;
        function swithEditPanel(flag)
        {
            var lastEle = document.getElementById("tab_"+curFlag);
            lastEle.className = "tab";
        
            curFlag = flag;
            
            
        document.getElementById("tab_"+flag).className = "selTab";
           
            
        }
        
        
        
        function LoadUserList(action,callback)
        {
            $.ajax({ 
                    type: "POST",
                    url: "../../../Handler/Office/HumanManager/PerformanceTemplate_Edit.ashx?action=" + action,
                    dataType:'string',
                    data: '',
                    cache:false,
                    success:function(data) 
                    {                          
                        var result = null;
                        eval("result = "+data);
                        
                        if(result.result)                    
                        {
                            callback(result.data);
                                             
                        }else{                  
                               showInfo(result.data);               
                        }                   
                    },
                    error:function(data)
                    {
                         showInfo(data.responseText);
                    }
                });
        }
        
        
        function clearSel()
        {
 
               document.getElementById("txtSender").value = "";
               document.getElementById("txtSenderHidden").value = "";
               treeview_unsel();
               
        }
        
        function hideSelPanel()
        {
             var box = document.getElementById("selUserBox");
        
            box.style.display = "none";
          
        }
        
        
        
         function showSelPanel()
        {
            var box = document.getElementById("selUserBox");
            box.style.left = "200px";
            box.style.top = "100px";
            box.style.display = "";
            getStatrMess();
         
            
        }
        
        
        
        
        
     
        function dispData(data)
        {
            var ttID = document.getElementById("ttID");
            var ttTitle = document.getElementById("ttTitle");
            var ttSendUser = document.getElementById("ttSendUser");
            var ttSendDate = document.getElementById("ttSendDate");
            var ttContent = document.getElementById("ttContent");
            ttTitle.innerHTML = data.ID;
            ttSendUser.innerHTML = data.SendUserID;
            ttTitle.innerHTML = data.Title;
            ttSendDate.innerHTML = data.CreateDate;
            ttContent.innerHTML = data.Content;
                      
            
            document.getElementById("editPanel").style.left = "200px";
            document.getElementById("editPanel").style.top ="200px";
            document.getElementById("editPanel").style.display = "";
            
            TurnToPage(currentPageIndex);
        }
        function hideMsg()
        {
            document.getElementById("editPanel").style.display = "none";
        }

</script>
</head>
<body>
<form id="frmMain" runat="server">


<div id="popupContent"></div>
<span id="Forms" class="Spantype"></span>
<a name="DetailListMark"></a>
<uc1:Message ID="msgError" runat="server" />
<table width="98%"border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
<tr>
    <td align="right" colspan="2" style="height:20px" bgcolor="#f0f0f0"><a href="PerformanceType.aspx?ModuleID=2011801">考核类型设置</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="PerformanceElem.aspx?ModuleID=2011801">考核指标设置</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="PerformanceTemplate.aspx?ModuleID=2011801">考核模板设置</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<a href="PerformanceT.aspx?ModuleID=2011801">人员考核流程设置</a></td>
    </tr>
    <tr id="trSear" style="display:block; ">
        <td valign="top" colspan="2" bgcolor="#f0f0f0">
            <img src="../../../images/Main/Line.jpg"   />
        </td>
    </tr>
                    <tr id="trSearc" style="display:block; ">
                        <td height="20"  bgcolor="#f0f0f0" >
                           <span class="Blue"><img src="../../../images/Main/Arrow.jpg"  height="18" align="absmiddle" /></span>检索条件
                        </td>
                        <td  bgcolor="#f0f0f0"   align="right" >                              <div id='searchClick6'>
                                          <img src="../../../images/Main/Close.jpg" style="cursor: pointer" id="img3" onclick="oprItem('tbSS','searchClick6')" /></div><input id="hidCon1" type="hidden"   value="2" /></td>
                    </tr>
  
    
  
    <tr id="trSearch" style="display:block; ">
       <td colspan="2" bgcolor="#FFFFFF">
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#CCCCCC" >
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" >
                            <table width="100%" border="0" cellspacing="1" cellpadding="2" bgcolor="#CCCCCC" id="tbSS">
                                <tr>
                                    <td height="20" class="tdColTitle" width="10%" >
                                     <span style="color:Black; font-weight:normal ">  考核类型</span></td>
                                    <td class="tdColInput" width="23%">
                                                                       
                                <select id="selSearchPerformanceType" style="width:auto" runat="server">
                                </select>
                                 </td>
                                    <td class="tdColTitle" width="10%">
                                    <span style="color:Black; font-weight:200 ">  考核模板主题</span></td>
                                    <td class="tdColInput" width="23%">
                                                                       
                                <input id="inpSearchTitle" type="text"  size="50"  maxlength="50" class="tdinput" style="width:100%" runat="server"/>
                                            
                                             </td>
                                 <td class="tdColTitle" width="10%"></td> <td class="tdColInput" width="23%"></td>
                                </tr>    
                                <tr>
                              
                                               <td  colspan="7" align="center" class="tdColInput">
              <img title="检索"  id="btnSearch" src="../../../images/Button/Bottom_btn_search.jpg"  style='cursor:pointer;' 
                                                       onclick='SearchFlowData()'     runat="server" 
                                                       visible="false"/>
           <%--   <img alt="重置" src="../../../images/Button/Bottom_btn_re.jpg" id="btnReset" runat="server" visible="false" style='cursor:pointer;' onclick="ClearSearchCondition()" width="52" height="23" />--%>
              </td>
                                
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" >
                 <tr>
                        <td valign="top" bgcolor="#f0f0f0">
                            <img src="../../../images/Main/Line.jpg" width="122"  />
                        </td>
                        
                    </tr>
                    <tr>
                        <td height="20" colspan="2" align="center" valign="top" class="Title" bgcolor="#f0f0f0">  考核模板设置</td>
                    </tr>
                          <tr>
                        <td height="35" colspan="2" valign="top">
                            <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF">
                                   <img src="../../../Images/Button/Bottom_btn_new.jpg" alt="新建" visible=" false" id="btnNew" runat="server" style="cursor:hand"   onclick="ShowEdit();"/><img 
                                        src="../../../images/Button/Main_btn_delete.jpg" alt="删除" visible="false" 
                                        id="btnDelete" runat="server" onclick="DoDelete(0)" style='cursor:pointer;' 
                                         />
                                                                               <asp:ImageButton ID="btnImport" ImageUrl="../../../images/Button/Main_btn_out.jpg" AlternateText="导出Excel" runat="server" onclick="btnImport_Click" />
                                        
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
               
             </table>

         <table  border="0"  align="center" cellpadding="2" cellspacing="1" id="tbSearch" width="99%"  >
                    <tr>
                        <td colspan="2">
   
                            <table width="99%" border="0" align="center" cellpadding="0"  cellspacing="1" id="tbFlowDetail"  bgcolor="#999999">
                                <tbody>
                                    <tr>
                                        <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">选择<input type="checkbox" id="chkCheckAll" name="chkCheckAll" onclick="AllSelect('chkCheckAll', 'chkSelect')"></th>
                                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderFlowBy('TemplateNo','oC0');return false;">
                                                模板主题<span id="oC0" class="orderTip"></span>
                                            </div>
                                        </th>
                                         <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderFlowBy('TypeName','oC1');return false;">
                                                考核类型<span id="oC1" class="orderTip"></span>
                                            </div>
                                        </th>
                                         <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderFlowBy('CreaterName','oC2');return false;">
                                                创建人<span id="oC2" class="orderTip"></span>
                                            </div>
                                        </th>
                                         <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                            <div class="orderClick" onclick="OrderFlowBy('CreateDate','oC3');return false;">
                                                创建日期<span id="oC3" class="orderTip"></span>
                                            </div>
                                        </th>
                                    </tr>
                                   
                                    
                                </tbody>
                               
                            </table>
                     
                            <br/>
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList" >
                                <tr>
                                    <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
                                        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                                            <tr>
                                                <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  >
                                                    <div id="pagecount"></div>
                                                </td>
                                                <td height="28"  align="right">
                                                    <div id="divPageInfo" class="jPagerBar"></div>
                                                </td>
                                                <td height="28" align="right">
                                                    <div id="div4">
                                                        每页显示<input name="txtShowPageCount" type="text" id="txtPageCount" size="3"  maxlength="5"/>条&nbsp;&nbsp;
                                                        转到第<input name="txtToPage2" type="text" id="txtToPage2" size="3"/>页&nbsp;&nbsp;
                                                        <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:pointer;' alt="go" width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#txtPageCount').val(),$('#txtToPage2').val());" />
                                                    </div>
                                                 </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br/>
                        </td>
                    </tr>
                </table>
       
       </td>
       
       </tr>
    <tr id="add1" style="display:none;">
        <td height="40" valign="top" colspan="2">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                <tr>
                    <td height="30" class="tdColInput">
                        <table width="100%">
                           <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122"  />
            </td>
        </tr>
             <tr>
                        <td  colspan="2" align="center" valign="top" class="Title">考核模板设置</td>
                    </tr>
                          <tr>
                        <td height="35" colspan="2" valign="top">
                            <table width="100%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF">
                                         <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand"   onclick="DoSave();"/>
                                    <img src="../../../Images/Button/Bottom_btn_back.jpg" runat="server" visible="true" alt="返回" id="btnBack" onclick="DoBack();" style="cursor:hand"   />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                          
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="add2" style="display:none;">
    <td colspan="2">
        <!-- <div style="height:500px;overflow-y:scroll;"> -->
        <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblmain">
            <tr>
                <td  colspan="2">
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="20" bgcolor="#F4F0ED" class="Blue">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr>
                                        <td>基本信息</td>
                                        <td align="right">
                                            <div id='divBaseInfo'>
                                                <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblBaseInfo','divBaseInfo')"/>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="2" height="0">
                                <input type="hidden" id="hidEditFlag" runat="server" />
                                <input type="hidden" id="hidModuleID" runat="server" />
                                <input type="hidden" id="hidSearchCondition" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" id="tblBaseInfo" style="display:block">
                        <tr>
                            <td height="20" align="right" class="tdColTitle" width="10%"   valign="top">模板编号<span class="redbold">*</span></td>
                            <td  height="20" class="tdColInput" width="24%">
                        
                                <input id="txtEditPerformTemp" type="text" maxlength="20" readonly="readonly"  disabled   class="tdinput" />
                                    <div id="txtPerformTmNo">  <uc2:CodingRule ID="AimNum" runat="server" class="tdinput" /></div></td>
                                
                                
                            <td height="20" align="right" class="tdColTitle" width="10%"   valign="top">主题<span class="redbold">*</span></td>
                            <td   class="tdColInput" width="23%" >
                               
                                <input id="txtTitle" type="text"   maxlength="50" class="tdinput"     style="width:98%" SpecialWorkCheck="主题" /></td>
                          
                        </tr>
                        <tr>
                          <td height="20" align="right" class="tdColTitle" width="10%">考核类型<span class="redbold">*</span></td>
                            <td height="20" class="tdColInput" width="24%">
                               
                                <select id="selPerformanceType" style="width:auto">
                                </select>
                                
                                
                            </td>
                             <td class="tdColTitle" width="10%">启用状态<span class="redbold">*</span></td>
                                    <td class="tdColInput" width="23%">
                                        <select id="sltSearchUsedStatus" runat="server">
                                            <option value="1">启用</option>
                                            <option value="0">停用</option>
                                           
                                        </select>
                                    </td>
                            
                            
                            
                        
                        </tr>
                            
                        <tr>
                        
                            <td height="20" align="right" class="tdColTitle" >备注</td>
                            <td height="20" class="tdColInput" colspan="3">
                             
              <input id="txtRemark" maxlength ="500" onkeyup="textcontrol(this.id,250)"   style="width:100%; " class="tdinput" SpecialWorkCheck="备注"   />
                                    <input id="HDCodeFlag" type="hidden" />
                            </td>
                            
                        </tr>
                        <tr>
                        
                            <td height="20" align="right" class="tdColTitle" >创建日期</td>
                            <td height="20" class="tdColInput" colspan="3">
                                <div id ="dvCreateDate"></div>      </td>
                            
                        </tr>
                    </table>
                </td>
            </tr>
            <tr><td colspan="2" height="10"></td></tr>
            <tr>
                <td valign="top" ><img src="../../../images/Main/Line.jpg" width="122" height="7" /></td>
                <td align="right" valign="top"><img src="../../../images/Main/LineR.jpg" width="122" height="7" /></td>
            </tr>
            <tr><td colspan="2" height="10"></td></tr>
            <tr>
                <td colspan="2">
                    <table width="99%" cellpadding="0" cellspacing="1" border="0" align="center">
                        <tr>
                            <td height="25" valign="top" >
                                <span class="Blue">添加指标</span>
                            </td>
                            <td align="right" valign="top">
                                <div id='divElem'>
                                    <img src="../../../images/Main/close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblElem','divElem')"/>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="99%"  id="tblElem" width="100%" border="0" cellspacing="0" cellpadding="3"  class="tab_a" >
                        <tr>
                            <td height="28">
                                <img src="../../../images/Button/Main_btn_add.jpg "    id="btnAdd" runat="server" alt="添加" style="cursor:hand" onclick="showSelPanel();" visible="false" />
                                <img src="../../../images/Button/Main_btn_delete.jpg"     id="btnDeleteFlow" runat="server" alt="删除" style="cursor:hand" onclick="Delete();" visible="false"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="divElemDetail" runat="server"></div>
                                
                               
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr><td colspan="2" height="10"></td></tr>
        </table>
        
     
           
      
        <table id="tbEdit" width="100%" border="0" cellspacing="0" cellpadding="3"  class="tab_a">
        <tr><th>选择<input type="checkbox" id="chkCheckAll2" name="chkCheckAll2" onclick="changEdit(this);"/></th><th  colspan='2' width="200">指标名称</th><th width="200" >权重</th></tr>
        <tbody>
        </tbody>
       </table>
  
       
       
       
       
       
        <div id="selUserBox" style="position:absolute;display:none;">
    <ul id="typeListTab">
         <li id="tab_0" class="tab" onclick="swithEditPanel(0);LoadUserList('LoadUserList',BuildTree)" style="display:none ">全部</li>
                            <li  id="tab_1" class="selTab" onclick="swithEditPanel(1);LoadUserList('LoadUserListWithDepartment',BuildTree)">考核指标</li>
                            <li  id="tab_2" class="tab" onclick="swithEditPanel(2);LoadUserList('LoadUserListWithGroup',BuildTree)" style="display:none ">分组</li>                            
                            
                          <li style="display:inline; color:White "  onclick="hideSelPanel()"><img style="margin-left:94px;cursor:pointer;" align="absbottom" src="../../../Images/Pic/Close.gif"/>关闭</li>
    </ul>
    <div id="userList"></div>
    <input name="txtSender" id="txtSender" onclick="showSelPanel()" readonly="readonly"   type="text" class="tdinput" style="width:180px; display:none ;"  />
              <input type="hidden" value="" id="txtSenderHidden"/>
                    
                       
                          <a href="#" onclick="clearSel(this)" style="display :none">清空</a>
    <div style="border:solid 1px #3366cc;padding:5px;text-align:center;width:200px;" >
        <a href="#" onclick="clearSel()">清空</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" onclick="CreateTb()" >确定</a> 
    </div>
    
</div>
        <!-- </div> -->
        <%--要素选择页面 开始--%>
        
        <%--要素选择页面 结束--%>
    </td></tr>
</table>
</form>
</body>
</html>
