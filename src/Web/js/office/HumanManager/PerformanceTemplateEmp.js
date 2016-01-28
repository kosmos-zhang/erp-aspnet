/* 页面初期显示 */
$(document).ready(function(){
     DoSearchInfo();
     
     
     
     $("#imgEmployeeID").click();
       $("#imgTemplate").click();
        $("#imgSteps").click();
        SearchFlowData();
});
function DoSearchInfo(currPage)
{ 
    
    TurnToPage(1);
}

/*
* 改页显示
*/
function ChangePageCountIndex(newPageCount, newPageIndex)
{
    //判断是否是数字
    if (!IsNumber(newPageCount))
    {
        popMsgObj.ShowMsg('请输入正确的显示条数！');
        return;
    }
    if (!IsNumber(newPageIndex))
    {
        popMsgObj.ShowMsg('请输入正确的转到页数！');
        return;
    }
    //判断重置的页数是否超过最大页数
    if(newPageCount <=0 || newPageIndex <= 0 || newPageIndex > ((totalRecord - 1)/newPageCount) + 1)
    {
        popMsgObj.ShowMsg('转至页数超出查询范围！');
    }
    else
    {
        //设置每页显示记录数
        this.pageCount = parseInt(newPageCount);
        //显示页面数据
        TurnToPage(parseInt(newPageIndex));
    }
}

/* 分页相关变量定义 */  

var pageCount = 10;//每页显示记录数
var totalRecord = 0;//总记录数
var pagerStyle = "flickr";//jPagerBar样式
var currentPageIndex = 1;//当前页
var orderBy = "";//排序字段


/*
* 翻页处理
*/
function TurnToPage(pageIndex)
{
    //设置当前页
    currentPageIndex = pageIndex;
    //获取查询条件
   
    //设置动作种类
    var action="SearchInfo";
    var postParam = "PageIndex=" + pageIndex + "&PageCount=" + pageCount + "&Action=" + action + "&OrderBy=" + orderBy + "&UsedStatus=1"
    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  '../../../Handler/Office/HumanManager/PerformanceTemplateEmp_Query.ashx?' + postParam,//目标地址
        dataType:"json",//数据格式:JSON
        cache:false,
        beforeSend:function()
        {
            AddPop();
        },//发送数据之前
        success: function(msg)
        {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#tblDetailInfo tbody").find("tr.newrow").remove();
            $.each(msg.data
                ,function(i,item)
                {
                    if(item.TemplateNo != null && item.TemplateNo != "")
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"
                        + "<input id='chkSelect' class='TemplateNo'  name='chkSelect' value='" + item.TemplateNo + "'  type='checkbox'/>" + "</td>" //选择框
                        + "<td height='22' align='center'><a href='#' >" + item.Title + "</a></td>").appendTo($("#tblDetailInfo tbody")//启用状态
                    );
            });
            //页码
            ShowPageBar(
                "divPageClickInfo",//[containerId]提供装载页码栏的容器标签的客户端ID
                "<%= Request.Url.AbsolutePath %>",//[url]
                {
                    style:pagerStyle,mark:"DetailListMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageCount,
                    currentPageIndex:pageIndex,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上一页",
                    nextWord:"下一页",
                    First:"首页",
                    End:"末页",
                    onclick:"TurnToPage({pageindex});return false;"
                }
            );
            totalRecord = msg.totalCount;
            $("#txtShowPageCount").val(pageCount);
            ShowTotalPage(msg.totalCount, pageCount, pageIndex,$("#pagecount"));
            $("#txtToPage").val(pageIndex);
        },
        error: function() 
        {
            popMsgObj.ShowMsg('请求发生错误！');
        },
        complete:function(){
            hidePopup();
            $("#divPageClickInfo").show();
            SetTableRowColor("tblDetailInfo","#E7E7E7","#FFFFFF","#cfc","cfc");
        }
    });
}

/*
* 设置数据明细表的行颜色
*/
function SetTableRowColor(elem,colora,colorb, colorc, colord){
    //获取DIV中 行数据
    var tableTr = document.getElementById(elem).getElementsByTagName("tr");
    for(var i = 0; i < tableTr.length; i++)
    {
        //设置行颜色
        tableTr[i].style.backgroundColor = (tableTr[i].sectionRowIndex%2 == 0) ? colora:colorb;
        //设置鼠标落在行上时的颜色
        tableTr[i].onmouseover = function()
        {
            if(this.x != "1") this.style.backgroundColor = colorc;
        }
        //设置鼠标离开行时的颜色
        tableTr[i].onmouseout = function()
        {
            if(this.x != "1") this.style.backgroundColor = (this.sectionRowIndex%2 == 0) ? colora:colorb;
        }
    }
}

/*
* 排序处理
*/
function OrderBy(orderColum,orderTip)
{
    var ordering = "a";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
         allOrderTipDOM.empty();
         $("#"+orderTip).html("↑");
    }
    else
    {
        ordering = "d";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderBy = orderColum+"_"+ordering;
    TurnToPage(1);
}
function OrderFlowBy(orderColum,orderTip)
{
    var ordering = "a";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
         allOrderTipDOM.empty();
         $("#"+orderTip).html("↑");
    }
    else
    {
        ordering = "d";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderBy = orderColum+"_"+ordering;
    TurnToFlowPage(1);
}





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
    curLevel = 0

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
   
    if(node.SubNodes.length>0)
    {
       tr.insertCell(-1).innerHTML = "<img onclick=\"treeview_expand(this)\" src=\"/images/treeimg/WebResource6.gif\">";
    }else{
       tr.insertCell(-1).innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;";
    }    
        //tr.insertCell(-1).innerHTML = "<input id=\"treeview_checkbox"+node.index+"\" class='EmployeeList' type=checkbox  value=\""+node.value+"\"><a onmouseover=\"treeview_onmove("+node.index+",1)\" onmouseout=\"treeview_onmove("+node.index+",0)\" id=\"treeview_node"+node.index+"\" style=\"color:black;\" href=\"javascript:treeview_onselnode("+node.index+");\">"+node.text+"</a>";
    tr.insertCell(-1).innerHTML = "<input id=\"treeview_checkbox"+node.index+"\" class='EmployeeList' type=checkbox   value=\""+node.value+"\">"+node.text;
    
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
    
    
    document.getElementById("txtToList").value = userlist;
    document.getElementById("seluseridlist").value = useridlist
                
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


function ClearShow()
{
 var emList=$(".EmployeeList");
  var len=emList.length;
  for (var i=0;i<len ;i++)
   {
        if (emList[i] .checked==true )
       {
            emList[i] .checked=false ;
       }
  
   }
     var tmList=$(".TemplateNo");
  var tmListlen=tmList.length;
  for (var a=0;a<tmListlen ;a++)
   {
        if (tmList[a] .checked==true )
       {
         tmList[a] .checked=false ;
      
         }
  
   }
   //alert("startIndex"); 
    var tab=document .getElementById("tbDetail");
        var sbody=tab.tBodies [0];
       
        var rowCount=sbody .rows.length-1;
        //alert (rowCount);
        if (rowCount >0)
        {
          for (var col=1;col <rowCount+1 ;col++)
          {
          //  alert (col );
            sbody.deleteRow(1);
          }
        }

//$("#tbDetail tbody").find("tr.newrow").remove();

 // alert("endIndex"); 

}

function DoSaveInfo()
{
    /* 页面信息进行校验 */
    //基本信息校验 有错误时，返回
    if (!CheckInputInfo())
    {
        return;
    }
    //获取基本信息参数
    var EmployeeID=getAll();
    var ScoreEmployee=getkaoping();
    var TemplateNo=getAll1();
    var postParaList="&EmployeeID="+EmployeeID +"&ScoreEmployee="+ScoreEmployee+"&TemplateNo="+TemplateNo;
   
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/PerformanceTemplateEmp_Query.ashx?Action=InsertInfo&" + postParaList,
        dataType:'json',//返回json格式数据
        cache:false,
        beforeSend:function()
        {
            AddPop();
        }, 
        error: function()
        {
            //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
             popMsgObj.ShowMsg('请求发生错误 ！');
        }, 
        success:function(data) 
        {
            //隐藏提示框
            hidePopup();
            //保存成功
            if(data.sta == 1) 
            { 
                //设置编辑模式
             //   document.getElementById("hidEditFlag").value = data.info;
                //设置ID 
              //  document.getElementById("hidElemID").value = data.data;
                //设置提示信息
               // ClearShow();
               // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                 popMsgObj.ShowMsg('保存成功！');
                 showFlowEdit();
                 SearchFlowData();
            }
            //保存失败
            else 
            { 
                hidePopup();
                //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
                 popMsgObj.ShowMsg('保存失败,请确认！');
            }
        } 
    });  
}


function SwapRow()
{ 
  var t= 0;
     var tab=document .getElementById("tbDetail");
        var table=tab.tBodies [0];
 // table = document.getElementById("dg_Log");
  var count = table.rows.length ;
 
	  var emList=$(".ScoreEmployeeListCheck");
        var checkedEm=new Array ();
      
        var len=emList.length;
         
        for (var i=0;i<len ;i++)
       {
          if (emList[i] .checked==true )
         {
             t=t+1;
          }
  
       }
	
   if(t==0)
	{
	    popMsgObj.ShowMsg('请选择需要操作选项！');
	   return;
	}
	if(t>1)
	{
	   popMsgObj.ShowMsg('只能选择一项！');
	   return;
	}
	var startIndex=0;
	for (var i=0;i<len ;i++)
       {
          if (emList[i] .checked==true )
         {
           startIndex=i;
             break;
          }
  
       }
       
      
       if (startIndex ==0)
       {
        popMsgObj.ShowMsg('数据已经不允许再上移！');
     
	  return;
       
       
       
       
       }
  // var select = document.getElementById('chkSelect_1');
  // if(select.checked)
  // {
  //    popMsgObj.ShowMsg('数据已经不允许再上移！');
   //   select.checked=false;
	//   return;
  // }
//   for (var i = count - 1; i > 0; i--)
//	{
//        var select = document.getElementById("chkSelect_" + i);
//        if (select.checked)
//        {
//          SwapRowValue(i,i-1);
//          break;
//        }
//	} 
	   for (var i=0;i<len ;i++)
       {
          if (emList[i] .checked==true )
         {
            SwapRowValue(i+1,i);
             break;
          }
  
       }
}




///交换行数据
function SwapRowValue(x,y)
{  var tab=document .getElementById("tbDetail");
        var table=tab.tBodies [0];




	var objX,objY;
	objX = table .rows[x].cells[1];
	objY = table .rows[y].cells[1];
    var str = objX.innerHTML;
	objX.innerHTML = objY.innerHTML;
	objY.innerHTML = str;


	objX = table .rows[x].cells[0];
	objY = table .rows[y].cells[0];
	 var str = objX.innerHTML;
	objX.innerHTML = objY.innerHTML;
	objY.innerHTML = str;
	
	
    objX = table .rows[x].cells[2];
	objY = table .rows[y].cells[2];
	var str = objX.innerHTML;
	objX.innerHTML = objY.innerHTML;
	objY.innerHTML = str;
	
	  objX = table .rows[x].cells[3];
	objY = table .rows[y].cells[3];
	var str = objX.innerHTML;
	objX.innerHTML = objY.innerHTML;
	objY.innerHTML = str;
	
	  objX = table .rows[x].cells[5];
	objY = table .rows[y].cells[5];
	var str = objX.innerHTML;
	objX.innerHTML = objY.innerHTML;
	objY.innerHTML = str;
}



function SwapRowDown()
{ 
  var t= 0;
     var tab=document .getElementById("tbDetail");
        var table=tab.tBodies [0];
 // table = document.getElementById("dg_Log");
  var count = table.rows.length ;
 
	  var emList=$(".ScoreEmployeeListCheck");
        var checkedEm=new Array ();
      
        var len=emList.length;
         
        for (var i=0;i<len ;i++)
       {
          if (emList[i] .checked==true )
         {
             t=t+1;
          }
  
       }
	if(t>1)
	{
	   popMsgObj.ShowMsg('只能选择一项！');
	   return;
	}
	 if(t==0)
	{
	    popMsgObj.ShowMsg('请选择需要操作选项！');
	   return;
	}
	var lastIndex=0;
	for (var i=0;i<len ;i++)
       {
          if (emList[i] .checked==true )
         {
           lastIndex=i;
             break;
          }
  
       }
       
     // alert (lastIndex);
    //  alert (count );
       if ((lastIndex+2) ==count )
       {
        popMsgObj.ShowMsg('数据已经不允许再下移！');
     
	  return;
       
       
       
       
       }
  for (var i=0;i<len ;i++)
       {
          if (emList[i] .checked==true )
         {
            SwapRowValue(i+1,i+2);
             break;
          }
  
       }
}



function SearchFlowData(currPage)
{ 
    
    TurnToFlowPage(1);
}

  
  function showFlowEdit()
  { 
      ClearShow();
           
          
     if (document.getElementById("trSearch").style.display == "block") 
     {
      document.getElementById("trSearch").style.display = "none"; 
     }    
     else
     {
      document.getElementById("trSearch").style.display = "block"; 
     }
     if ( document.getElementById("add_1").style.display == "block")
      {    document.getElementById("add_1").style.display = "none"; 
           document.getElementById("add_2").style.display = "none"; 
           document.getElementById("add_3").style.display = "none"; 
           document.getElementById("add_4").style.display = "none"; 
        
           
      }
      else
      {     document.getElementById("add_1").style.display = "block"
          document.getElementById("add_2").style.display = "block";
          document.getElementById("add_3").style.display = "block";
          document.getElementById("add_4").style.display = "block";
      
      
      }
   }
   function hideFlowEdit()
   {
   document.getElementById("add_1").style.display = "none"; 
    document.getElementById("add_2").style.display = "none"; 
     document.getElementById("add_3").style.display = "none"; 
      document.getElementById("add_4").style.display = "none"; 
      
   ClearShow();
   }
  




function TurnToFlowPage(pageIndex)
{
    //设置当前页
    currentPageIndex = pageIndex;
    //获取查询条件
   
    //设置动作种类
    var action="SearchFlowInfo";
   
    
    
    
    
    var postParam = "PageIndex=" + pageIndex + "&PageCount=" + pageCount + "&Action=" + action + "&OrderBy=" + orderBy ;
     if (""!=document .getElementById ("txtSearchScoreEmployee").value)
    {
    postParam+="&ScoreEmployee="+document .getElementById ("txtSearchScoreEmployee").value;
    
    }
     if (""!=document .getElementById ("txtSearchEmployeeID").value)
    {
    postParam+="&EmployeeID="+document .getElementById ("txtSearchEmployeeID").value;
    
    }
     if (""!=document .getElementById ("txtSearchTemplateName").value)
    {
    postParam+="&TemplateName="+document .getElementById ("txtSearchTemplateName").value;
    
    }
    
    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  '../../../Handler/Office/HumanManager/PerformanceTemplateEmp_Query.ashx?' + postParam,//目标地址
        dataType:"json",//数据格式:JSON
        cache:false,
        beforeSend:function()
        {
            AddPop();
        },//发送数据之前
        success: function(msg)
        {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#tbFlowDetail tbody").find("tr.newrow").remove();
            $.each(msg.data
                ,function(i,item)
                {
                    if(item.ID != null && item.ID != "")
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"
                        + "<input id='chkSelect' class='TemplateNo'  name='chkSelect' value='" + item.ID + "'  type='checkbox'/>" + "</td>" //选择框
                        + "<td height='22' align='center'><a href='#' onclick =GetEmployee('"+item.EmployeeID+"','"+item.TemplateNo+"')>" + item.EmployeeName + "</a></td>"
                        + "<td height='22' align='center'>" + item.TemplateName + "</td>"
                        + "<td height='22' align='center'>" + item.StepName + "</td>"
                        + "<td height='22' align='center'>" + item.ScoreEmployeeName + "</td>").appendTo($("#tbFlowDetail tbody")//启用状态
                    );
            });
            //页码
            ShowPageBar(
                "divPageInfo",//[containerId]提供装载页码栏的容器标签的客户端ID
                "<%= Request.Url.AbsolutePath %>",//[url]
                {
                    style:pagerStyle,mark:"DetailListMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageCount,
                    currentPageIndex:pageIndex,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上一页",
                    nextWord:"下一页",
                    First:"首页",
                    End:"末页",
                    onclick:"TurnToFlowPage({pageindex});return false;"
                }
            );
            totalRecord = msg.totalCount;
            $("#txtPageCount").val(pageCount);
            ShowTotalPage(msg.totalCount, pageCount, pageIndex,$("#pagecount"));
            $("#txtToPage2").val(pageIndex);
        },
        error: function() 
        {
            popMsgObj.ShowMsg('请求发生错误！');
        },
        complete:function(){
            hidePopup();
            $("#divPageInfo").show();
            SetTableRowColor("tbFlowDetail","#E7E7E7","#FFFFFF","#cfc","cfc");
        }
    });
}
function GetEmployee(employeeID,templateNo)
{
    //编辑模式
    //document.getElementById("hidEditFlag").value = "UPDATE";
    //设置ID
   /// document.getElementById("hidElemID").value = elemID;
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/PerformanceTemplateEmp_Query.ashx?Action=GetEmployeeInfo&EmployeeID=" + employeeID+"&templateNo="+templateNo,
        dataType:'json',//
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
            //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
             popMsgObj.ShowMsg('请求发生错误！');
        }, 
        success:function(msg) 
        {
            //隐藏提示框  
            hidePopup();
            /* 设置考核类型信息 */
          //  $.each(msg.data, function(i,item){
                //要素名称
           //     document.getElementById("txtEditElemName").value = item.TypeName;
                //启用状态
           //     document.getElementById("sltEditUsedStatus").value = item.UsedStatus;
           // });
            
            
          //  $("#tbTemplateEdit tbody").find("tr.newro").remove();
          AlertMsg();
              var tab=document .getElementById("tbTemplateEdit");
        var sbody=tab.tBodies [0];
       
        var rowCount=sbody .rows.length-1;
        //alert (rowCount);
        if (rowCount >0)
        {
          for (var col=1;col <rowCount+1 ;col++)
          {
          //  alert (col );
            sbody.deleteRow(1);
          }
        }
            
            document.getElementById ("divShowEdit").style.display="block";
            
            $.each(msg.data
                ,function(i,item)
                {
                
                    if(item.ID != null && item.ID != "")
                    $("<tr class='newro'></tr>").append(
               "<td height='22' align='center'><input  id='txtEditTemp" + i + "' readonly ='readonly'  type='text' value="+item.TemplateName+" title="+item.TemplateNo+" class='tdinput' onclick=javascript:popTemplateObj.ShowList('txtEditTemp" + i + "')  class='tempList'/></td>"
             + "<td height='22' align='center'><input  id='UsertxtFlowStepActor2" + i + "' value="+item.ScoreEmployeeName+"   type='text'   class='tdinput' onclick=alertdiv('UsertxtFlowStepActor2"+i+",txtEditScore"+i+"') />"+"<input  id='txtEditScore"+i+"'   class='tempList'  type='hidden'  value="+item .ScoreEmployee+" /></td>"
              
             
             
               + "<td height='22' align='center'><input id='txt' type='text' size='5' value='"+item.StepNo+"' class='tdinput'/></td>" 
                   + "<td height='22' align='center'><input id='txt' type='text' value='"+item.StepName+"' class='tdinput'/></td>"  
                      + "<td height='22' align='center'><input id='txt' type='text'size='10' value='"+item.Rate+"'  class='tdinput'/></td>"
                       + "<td height='22' align='center'><input id='txt' type='text' value='"+item.remark+"' class='tdinput'/></td>"
                         + "<td height='22' align='center'><input  type='checkbox'       class='ScoreELCheck' class='tdinput'/></td>"
             
              ).appendTo($("#tbTemplateEdit tbody"),//启用状态
               document.getElementById ("txtScorEmp").title =item .EmployeeID,
                 document.getElementById ("txtScorEmp").value=item .EmployeeName
                
              
                    );
            });
        }
    });
    //显示修改页面
   // document.getElementById("divEditCheckItem").style.display = "block"; 
}



function CheckEditInfo()
{

  var fieldText = "";
    //出错提示信息
       var msgText = "";
         var tab=document .getElementById("tbTemplateEdit");
        var sbody=tab.tBodies [0];
       var signEmployeeID=false ;
        var rowCount=sbody .rows.length;
        if (rowCount >0)
        {
         signEmployeeID=true ;
        
        } else
        {
          signEmployeeID=false;
      
         fieldText += "系统信息|";
   	   	 msgText += "被考评人没有添加信息|";
         
           popMsgObj.Show(fieldText, msgText);
         return false  ;
        
        }
        
        
        var rowCount1=sbody .rows.length;
        var sum=0.00;
         
      
       for(var i=0;i<rowCount ;i++)
       { 
         
         if (isNaN (sbody .rows[i].cells[4].childNodes[0].value))
         {
         
          signEmployeeID=false;
      
         fieldText += "权重信息|";
   	   	 msgText += "权重必须输入数字|";
         
           popMsgObj.Show(fieldText, msgText);
         return false  ;
         }
          if (isNaN (sbody .rows[i].cells[2].childNodes[0].value))
         {
         
          signEmployeeID=false;
      
         fieldText += "流程步骤|";
   	   	 msgText += "流程必须输入数字|";
         
           popMsgObj.Show(fieldText, msgText);
         return false  ;
         }
      // alert (sbody .rows[i].cells[1].childNodes[0].title);
       
         if (" "==sbody .rows[i].cells[0].childNodes[0].title)
         {
        
          signEmployeeID=false;
      
         fieldText += "模板|";
   	   	 msgText += "请选择模板|";
         
           popMsgObj.Show(fieldText, msgText);
         return false  ;
         }
          if (" "==sbody .rows[i].cells[1].childNodes[0].title)
         {
        
          signEmployeeID=false;
      
         fieldText += "考评人|";
   	   	 msgText += "请选择考评人|";
         
           popMsgObj.Show(fieldText, msgText);
         return false  ;
         }
          if (" "==sbody .rows[i].cells[3].childNodes[0].value)
         {
         
          signEmployeeID=false;
      
         fieldText += "流程名|";
   	   	 msgText += "请填写流程名|";
         
           popMsgObj.Show(fieldText, msgText);
         return false  ;
         }
         
         
        
          sum =sum +parseFloat(sbody .rows[i].cells[4].childNodes[0].value);
         
       
       } 
         if ((sum%100)!=0)
       { 
          signEmployeeID=false;
       
         fieldText += "权重信息|";
   	   	 msgText += "一项模板的权重分数必须相加等于100|";
       
       }
       
        if (signEmployeeID)
      {
        return true ;
      }
      else
      {
       popMsgObj.Show(fieldText, msgText);
         return false  ;
      
      }
       
       


}
function DoSaveMessage()
{
    /* 页面信息进行校验 */
    //基本信息校验 有错误时，返回
    if (!CheckEditInfo())
    {
        return;
    }
    //获取基本信息参数
       var tab=document .getElementById("tbTemplateEdit");
        var sbody=tab.tBodies [0];
        var rowCount=sbody .rows.length;
        
        var rowCount1=sbody .rows.length;
        var templateList=new Array ();
         var messageList=new Array ();
         
      
       for(var i=0;i<rowCount ;i++)
       {
       //alert (sbody .rows[i].cells[1].childNodes[1].value);
       templateList .push (sbody .rows[i].cells[0].childNodes[0].title);
       alert (sbody .rows[i].cells[1].innerHTML);
       messageList.push (document .getElementById ("txtScorEmp").title,sbody .rows[i].cells[0].childNodes[0].title,sbody .rows[i].cells[1].childNodes[1].value,sbody .rows[i].cells[2].childNodes[0].value,sbody .rows[i].cells[3].childNodes[0].value,sbody .rows[i].cells[4].childNodes[0].value,sbody .rows[i].cells[5].childNodes[0].value);
       
       }
    
    
    var postParaList="&messageList="+messageList +"&TemplateNo="+templateList;
   
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/PerformanceTemplateEmp_Query.ashx?Action=InsertUpdateInfo&" + postParaList,
        dataType:'json',//返回json格式数据
        cache:false,
        beforeSend:function()
        {
            AddPop();
        }, 
        error: function()
        {
           // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
             popMsgObj.ShowMsg('请求发生错误！');
        }, 
        success:function(data) 
        {
            //隐藏提示框
            hidePopup();
            //保存成功
            if(data.sta == 1) 
            { 
                //设置编辑模式
             //   document.getElementById("hidEditFlag").value = data.info;
                //设置ID 
              //  document.getElementById("hidElemID").value = data.data;
                //设置提示信息
              //  alert ("1");
             popMsgObj.ShowMsg('保存成功！');
             Editcancel();
             document.getElementById("trSearch").style.display = "block"; 
             CloseDiv();
                //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                 
              //   SearchFlowData();
            }
            //保存失败
            else 
            {  
                hidePopup();
               // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                 popMsgObj.ShowMsg('保存失败,请确认！');
            }
        } 
    });  
}



	function AlertMsg(){

	   /**第一步：创建DIV遮罩层。*/
		var sWidth,sHeight;
		sWidth = window.screen.availWidth;
		//屏幕可用工作区高度： window.screen.availHeight;
		//屏幕可用工作区宽度： window.screen.availWidth;
		//网页正文全文宽：     document.body.scrollWidth;
		//网页正文全文高：     document.body.scrollHeight;
		if(window.screen.availHeight > document.body.scrollHeight){  //当高度少于一屏
			sHeight = window.screen.availHeight;  
		}else{//当高度大于一屏
			sHeight = document.body.scrollHeight;   
		}
		//创建遮罩背景
		var maskObj = document.createElement("div");
		maskObj.setAttribute('id','BigDiv');
		maskObj.style.position = "absolute";
		maskObj.style.top = "0";
		maskObj.style.left = "0";
		maskObj.style.background = "#777";
		maskObj.style.filter = "Alpha(opacity=30);";
		maskObj.style.opacity = "0.3";
		maskObj.style.width = sWidth + "px";
		maskObj.style.height = sHeight + "px";
		maskObj.style.zIndex = "200";
		document.body.appendChild(maskObj);
		
	}

		function CloseDiv(){
		var Bigdiv = document.getElementById("BigDiv");
		//var Mydiv = document.getElementById("div_Add");
		if (Bigdiv)
		{
		document.body.removeChild(Bigdiv);
		} 
//         Bigdiv.style.display = "none";
		///Mydiv.style.display="none";
	}


 function DoDelete() {
	        //获取选择框
	        var chkList = document.getElementsByName("chkSelect");
	        var chkValue = "";
	        for (var i = 0; i < chkList.length; i++) {
	            //判断选择框是否是选中的
	            if (chkList[i].checked) {
	                chkValue += "'" + chkList[i].value + "',";
	            }
	        }
	        var deleteNos = chkValue.substring(0, chkValue.length - 1);
	        selectLength = chkValue.split("',");
	        if (chkValue == "" || chkValue == null || selectLength.length < 1) {
	           // showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请至少选择一项删除！");
	            popMsgObj.ShowMsg('请至少选择一项删除！');
	            return;
	        }
	        else {
	            var postParam = "Action=DeleteInfo&DeleteNO=" + escape(deleteNos);
	            //删除
	            $.ajax(
        {
            type: "POST",
            url: "../../../Handler/Office/HumanManager/PerformanceTemplateEmp_Query.ashx?" + postParam,
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {

            },
            error: function() {
                ///  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                popMsgObj.ShowMsg('请求发生错误！');
            },
            success: function(data) {
                if (data.sta == 1) {
                    // 
                    // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                    popMsgObj.ShowMsg('删除成功！');
                   SearchFlowData();
                    //

                }
                else if (data.sta == 2) {
                    //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                    popMsgObj.ShowMsg('人员流程设置引用了该模版，不能执行删除 ,请确认！');
                }
                else {
                    // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                    popMsgObj.ShowMsg('删除失败 ,请确认！');
                }
            }
        });
	        }
	    }








