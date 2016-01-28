$(document).ready(function() {
    // DoGetTemplateInfo();
    //   DoGetEmployeeInfo();
    
    DoGetTemplateInfo();
    document.getElementById("hidCon1").value = "1";
    SearchFlowData();
 
    //document.getElementById("hidEditFlag").value="INSERT";

});


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
        TurnToFlowPage(parseInt(newPageIndex));
    }
}
function DoGetTemplateNo()
{

//        var emList=$(".chkSelectTemplateNo");
      var checkedEm=new Array ();
//      
//        var len=emList.length;
//        
//        for (var i=0;i<len ;i++)
//       {
//          if (emList[i] .checked==true )
//         {
//       //  alert (emList[i].title);
//          checkedEm.push (emList[i].title);
//          }
//  
//       }
//       document .getElementById ("templateNo").value=checkedEm;

var temNoList=document .getElementById ("tbTemplate");
var count=temNoList.rows.length;
	for (var row = count; row >= 0; row--)
	{
	    //获取选择框控件
	    chkControl = document.getElementById("chkSelect_" + row);
	    //如果控件为选中状态，删除该行，并对之后的行改变控件名称
	  if (chkControl )
	  {
	    if (chkControl.checked)
	    {
	       //删除行，实际是隐藏该行
	       if (document.getElementById("hidchkSelect_" + row))
	        checkedEm.push (document.getElementById("hidchkSelect_" + row).value.Trim());
	    }
	    }
	}
document .getElementById ("templateNo").value=checkedEm;

}

function DoGetTemplateInfo()
{
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/PerformanceTask.ashx?action=GetTemplateInfo",
        dataType:'json',//
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
           // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
              popMsgObj.ShowMsg(' 请求发生错误！');
        }, 
        success:function(msg) 
        {
            //隐藏提示框  
            hidePopup();
            $.each(msg.data
                ,function(i,item)
                {
                    if(item.TemplateNo != null && item.TemplateNo != "")
                       $("<tr class='newrow'></tr>").append("<td height='22' align='center' style='width:10px'>"
                        + "<input id='chkSelect_"+i+"'  class='chkSelectTemplateNo'  name='chkSelect' onclick =DoGetTemplateNo()    value='" + item.TemplateNo + "'   type='checkbox'/><input id='hidchkSelect_"+i+"' name='chkSelect' value='" + item.TemplateNo + "'   type='hidden'/>" + "</td>" //选择框
                        + "<td height='22' align='center' style='width:auto'>" + item.Title + "</td>").appendTo($("#tbTemplate tbody")
                        );
            });
        }
    });
}

      var clickTimes=1;
      function  submitMessage()
      {
       var Flag = true;
      //出错字段
       var fieldText = "";
    //出错提示信息
       var msgText = "";
    //是否有错标识
       var employyID="1";
       var employyName="23";
       var stepName=" " ;
       var rate=0 ;
       var remark=" " ;
     
        var tab=document .getElementById("tbDetail");
        var sbody=tab.tBodies [0];
        var rowID = sbody.rows.length;
        var newRow=sbody .insertRow(-1);
        newRow .style.background="#ffffff";
        newRow .Cssclass="newrow";
        newRow.align="center";
        var newCell6=newRow .insertCell(-1);
        newCell6 .innerHTML= " <input  type='checkbox' value="+employyName+"   title="+employyID+"   class='ScoreEmployeeListCheck' onpropertychange='getChageFlow(this)'  />";
        
        
        
        var newCell1=newRow .insertCell(-1);
newCell1 .innerHTML =
 "<input  id='UsertxtFlowStepActor1" + rowID + "' style='width:97%'  maxlength='50'  type='text'   class='tdinput' onclick=alertdiv('UsertxtFlowStepActor1"+rowID+",txtFlowStepActor"+rowID+"') />"+"<input  id='txtFlowStepActor"+rowID+"'   class='ScoreEmployeeList'  type='hidden'  />";//添加列内容
 
        var newCell2=newRow .insertCell(-1);
      //  newCell2 .innerHTML=stepName ;  
    newCell2 .innerHTML="  <input id='txt' type='text' value='"+stepName+"'  style='width:97%'   maxlength='50'  class='tdinput'/>"
        var newCell3=newRow .insertCell(-1);
        //newCell3 .innerHTML=rate ;
         newCell3 .innerHTML="  <input id='txt' type='text' value='"+rate+"' size='10'  maxlength='10'  class='tdinput'/>"
         var newCell4=newRow .insertCell(-1);
        newCell4 .innerHTML=clickTimes ;
         var newCell5=newRow .insertCell(-1);
       // newCell5 .innerHTML=remark  ;
        newCell5 .innerHTML="  <input id='txt' type='text' value='"+remark+"' size='10'  maxlength='10' class='tdinput'/>"
        
        clickTimes++;
 }
    function Delete()
      { 
        var tab=document .getElementById("tbDetail");
        var sbody=tab.tBodies [0];
        var emList=$(".ScoreEmployeeListCheck");
        var checkedEm=new Array ();
      //alert (tab.innerHTML);
        var len=emList.length;
//        alert (len);
        for (var i=len-1 ;i>-1;i--)
       {
          if (emList[i] .checked==true )
         {
            sbody.deleteRow(i+1);
          }
  
       }
       recreate();
      }
      function recreate()
      {
       var tab=document .getElementById("tbDetail");
        var sbody=tab.tBodies [0];
          var emList=$(".ScoreEmployeeListCheck");
      var newLen=emList.length;
        for (var i=0;i<newLen ;i++)
       {
         
            sbody.rows[i+1].cells[4].innerHTML=i+1;
       
  
       }
      clickTimes=newLen+1 ;
      
      }
      
function changEdit(obj)
{

        var emList=$(".ScoreEmployeeListCheck");
        var len=emList.length;
        for (var i=0;i<len ;i++)
       {
         if (obj.checked==true)
          {
          emList[i] .checked=true;
           }
           else
           {
           emList[i] .checked=false ;
           }
       }
}      

function changEdit2(obj)
{

        var emList=$(".ScoreELCheck");
        var len=emList.length;
        for (var i=0;i<len ;i++)
       {
         if (obj.checked==true)
          {
          emList[i] .checked=true;
           }
           else
           {
           emList[i] .checked=false ;
           }
       }
} 


function getChageFlow2( obj)
{

if (obj.checked==false )
{
 document .getElementById ("chkCheckAll3").checked=false ;
}
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


function DoSaveInfo()
{
    /* 页面信息进行校验 */
    //基本信息校验 有错误时，返回
    if (!CheckInputInfo())
    {
   
        return;
    }
    //获取基本信息参数
    var EmployeeID=document.getElementById("seluseridlist").value.Trim();
    var ScoreEmployee=getkaoping();
    var TemplateNo=document.getElementById("templateNo").value.Trim();
    var postParaList="&EmployeeID="+ escape ( EmployeeID) +"&ScoreEmployee="+escape ( ScoreEmployee)+"&TemplateNo="+escape ( TemplateNo);
   
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
               
//有用的                
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


function CheckInputInfo()
{     var fieldText = "";
    //出错提示信息
       var msgText = "";
  var  signScoreEmployee=true;

      var selEmployeeID= document.getElementById("seluseridlist").value.Trim();
      if (selEmployeeID =='')
      {
        signScoreEmployee=false  ;
       fieldText += "被考核人|";
   	   	 msgText += "请选择被考核人|";
      }
        var  signTemplateNo=true ;
            var selTemplate= document.getElementById("templateNo").value.Trim();
      if (selTemplate =='')
      {
       signTemplateNo=false ;
        fieldText += "模板|";
   	   	 msgText += "请选择模板|";
      }
        
        
  var  signEmployeeID=false ;
   
    var tab=document .getElementById("tbDetail");
        var sbody=tab.tBodies [0];
       
        var rowCount=sbody .rows.length-1;
        if (rowCount >0)
        {
         signEmployeeID=true ;
        
        }
          if (signEmployeeID==false )
       {
         
         fieldText += "考评人|";
   	   	 msgText += "请添加考评人及权重信息|";
        }
  ///if (!signEmployeeID)
  //    {
  //     popMsgObj.Show(fieldText, msgText);
  //     return false;
  //    }
      
      
        var rowCount1=sbody .rows.length;
        var sum=0.00;
         
      
       for(var i=1;i<rowCount+1 ;i++)
       { 
         
         if (isNaN (sbody .rows[i].cells[3].childNodes[0].value.Trim()))
         {
         
          signEmployeeID=false;
      
         fieldText += "权重信息|";
   	   	 msgText += "权重必须输入数字|";
         
           popMsgObj.Show(fieldText, msgText);
         return false  ;
         }else
         {
           var sValue= parseInt(sbody .rows[i].cells[3].childNodes[0].value.Trim(),10);
           if (sValue ==0 || sValue >101)
           {
               signEmployeeID=false;
      
         fieldText += "权重信息|";
   	   	 msgText += "权重范围在0于100之间|";
         
           popMsgObj.Show(fieldText, msgText);
         return false  ;
           }
         }
      
       
     
      var sdes= document .getElementById ("txtFlowStepActor"+i).value;
         if ((sbody .rows[i].cells[1].childNodes[0].title==i)|( sdes=="")  )
         {
         
          signEmployeeID=false;
      
         fieldText += "考评人|";
   	   	 msgText += "请选择考评人|";
         
           popMsgObj.Show(fieldText, msgText);
         return false  ;
         }
         var sdfdfse=sbody .rows[i].cells[2].childNodes[0].value.Trim();
          if (""==sbody .rows[i].cells[2].childNodes[0].value.Trim())
         {
         
          signEmployeeID=false;
      
         fieldText += "流程名|";
   	   	 msgText += "请填写步骤名|";
         
           popMsgObj.Show(fieldText, msgText);
         return false  ;
         }
         
         
        
          sum =sum +parseFloat(sbody .rows[i].cells[3].childNodes[0].value.Trim());
         
       
       } 
       
       if ((sum <100.00)||(sum >100.00))
       { 
          signEmployeeID=false;
       
         fieldText += "权重信息|";
   	   	 msgText += "权重分数必须相加等于100|";
       
       }
      
      
      
      if (signEmployeeID&&signTemplateNo&&signScoreEmployee)
      {
        return true ;
      }
      else
      {
       popMsgObj.Show(fieldText, msgText);
         return false  ;
      
      }
}


   function getkaoping()
   {
   var tab=document .getElementById("tbDetail");
        var sbody=tab.tBodies [0];
        var rowCount=sbody .rows.length;
       var customer=new Array ();
        var temp=new Array ();
       
       for(var i=1;i<rowCount ;i++)
       { 
         var ss=sbody .rows[i].cells[1];
        // alert (ss.childNodes[1].value);
         temp .push (ss.childNodes[1].value,sbody .rows[i].cells[2].childNodes[0].value,sbody .rows[i].cells[3].childNodes[0].value,sbody .rows[i].cells[4].innerHTML,sbody .rows[i].cells[5].childNodes[0].value);
       
       } 
       return temp ;
   
   }
   
     function showFlowEdit()
  { 
  
 

      ClearShow();
          
          clickTimes=1;
          
               if (document.getElementById("trSearch2").style.display == "block") 
     {
      document.getElementById("trSearch2").style.display = "none"; 
     }    
     else
     {
      document.getElementById("trSearch2").style.display = "block"; 
     }
     if (document.getElementById("trSearch").style.display == "block") 
     {
      document.getElementById("trSearch").style.display = "none"; 
     }    
     else
     {
      document.getElementById("trSearch").style.display = "block"; 
     }
     if ( document.getElementById("add_1").style.display == "block")
      {   
       document.getElementById("add_1").style.display = "none";   
      }
      else
      {    
         document.getElementById("add_1").style.display = "block"
      }
      
       SearchFlowData();
   }
   function ClearShow()
{
       treeview_selall();
       treeview_selall();
     var tmList=$(".chkSelectTemplateNo");
  var tmListlen=tmList.length;
  for (var a=0;a<tmListlen ;a++)
   {
        if (tmList[a] .checked==true )
       {
         tmList[a] .checked=false ;
      
         }
  
   }
    var tab=document .getElementById("tbDetail");
        var sbody=tab.tBodies [0];
       
        var rowCount=sbody .rows.length-1;
        if (rowCount >0)
        {
          for (var col=1;col <rowCount+1 ;col++)
          {
            sbody.deleteRow(1);
          }
        }
}
function getChage( obj)
{

if (obj.checked==false )
{
 document .getElementById ("chkCheckAll").checked=false ;
}
}
function getChageFlow( obj)
{

if (obj.checked==false )
{
 document .getElementById ("chkCheckAll2").checked=false ;
}
}

function SearchFlowData(currPage)
{          document .getElementById ("hidCon1").value="sign";
       var isFlag=true ;
    var fieldText="";
    var msgText="";

    if( !CheckSpecialWord(document .getElementById ("txtSearchTemplateName").value.Trim()))
    {
            isFlag = false;
            fieldText ="考核模板主题|";
   		    msgText = "考核模板主题不能含有特殊字符|";
   		    
    }
     if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
      return;
    }
    TurnToFlowPage(1);
}
function TurnToFlowPage(pageIndex)
{
var hid=document .getElementById ("hidCon1").value;
if ( hid=="1")
{
return ;
}
    //设置当前页
    currentPageIndex = pageIndex;
    //获取查询条件
   
    //设置动作种类
    var action="SearchFlowInfo";
   
    
    
    
    
    var postParam = "PageIndex=" + pageIndex + "&PageCount=" + pageCount + "&Action=" + action + "&OrderBy=" + orderBy ;
     if (""!=document .getElementById ("txtSearchScoreEmployee").value.Trim())
    {
    postParam+="&ScoreEmployee="+escape ( document .getElementById ("txtSearchScoreEmployee").value.Trim());
    
    }
     if (""!=document .getElementById ("txtSearchEmployeeID").value.Trim())
    {
    postParam+="&EmployeeID="+escape ( document .getElementById ("txtSearchEmployeeID").value.Trim());
    
    }
     if (""!=document .getElementById ("txtSearchTemplateName").value.Trim())
    {
    postParam+="&TemplateName="+escape ( document .getElementById ("txtSearchTemplateName").value.Trim());
    
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
                        + "<input id='chkSelect' class='TemplateNo'  name='chkSelect1' value='" + item.ID + "'  type='checkbox' onpropertychange='getChage(this)' />" + "</td>" //选择框
                        + "<td height='22' align='center'><a href='#' onclick =GetEmployee('"+item.EmployeeID+"','"+item.TemplateNo+"','"+item.TemplateName+"')>" + item.EmployeeName + "</a></td>"
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

/* 分页相关变量定义 */  

var pageCount = 10;//每页显示记录数
var totalRecord = 0;//总记录数
var pagerStyle = "flickr";//jPagerBar样式
var currentPageIndex = 1;//当前页
var orderBy = "ModifiedDate_d";//排序字段
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

function GetEmployee(employeeID,templateNo,templateName)
{
    //编辑模式
    //document.getElementById("hidEditFlag").value = "UPDATE";
    //设置ID
   /// document.getElementById("hidElemID").value = elemID;
   document .getElementById ("txtEditTemp1").value=templateName ;
  document .getElementById ("txtEditTemp1").title=templateNo ;
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
        if (rowCount >=0)
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
//               "<td height='22' align='center'><input  id='txtEditTemp" + i + "' readonly ='readonly'  type='text'  title="+item.TemplateNo+" value="+item.TemplateName+"  class='tdinput' onclick=javascript:popTemplateObj.ShowList('txtEditTemp" + i + "')  class='tempList'/></td>"
             "<td height='22' align='center'><input  id='UsertxtFlowStepActor2" + i + "' value="+item.ScoreEmployeeName+"   type='text'   class='tdinput' onclick=alertdiv('UsertxtFlowStepActor2"+i+",txtEditScore"+i+"') />"+"<input  id='txtEditScore"+i+"'   class='tempList'  type='hidden'  value="+item .ScoreEmployee+" /></td>"
              
             
             
               + "<td height='22' align='center'><input id='txt' type='text' size='5' value='"+item.StepNo+"' class='tdinput'/></td>" 
                   + "<td height='22' align='center'><input id='txt' type='text' value='"+item.StepName+"' class='tdinput'/></td>"  
                      + "<td height='22' align='center'><input id='txt' type='text'size='10' value='"+item.Rate+"'  class='tdinput'/></td>"
                       + "<td height='22' align='center'><input id='txt' type='text' value='"+item.remark+"' class='tdinput'/></td>"
                         + "<td height='22' align='center'><input  type='checkbox'       class='ScoreELCheck' class='tdinput' onpropertychange='getChageFlow2(this)' /></td>"
             
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

 function Editcancel()
 {
//alert ("1");
  CloseDiv();
  //self.location="PerformanceT.aspx?ModuleID=2011801"; 
    document.getElementById ("txtScorEmp").value=" ";
             document.getElementById ("txtScorEmp").title=" ";
             $("#tbTemplateEdit tbody").find("tr.newro").remove();
                var tab=document .getElementById("tbTemplateEdit");
        var sbody=tab.tBodies [0];
       
        var rowCount=sbody .rows.length;
        var i=document .getElementById ("tbTemplateEdit").innerHTML;
          var len=rowCount;
        
        for (var i=len-1;i>-1 ;i--)
       {
      
            sbody.deleteRow(i);
      
       }
       document.getElementById ("divShowEdit").style.display="none";
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
//       templateList .push (sbody .rows[i].cells[0].childNodes[0].title);
    templateList .push (document .getElementById ("txtEditTemp1").title);
     //  alert (sbody .rows[i].cells[1].innerHTML);
       messageList.push (document .getElementById ("txtScorEmp").title,document .getElementById ("txtEditTemp1").title,sbody .rows[i].cells[0].childNodes[1].value,sbody .rows[i].cells[1].childNodes[0].value,sbody .rows[i].cells[2].childNodes[0].value,sbody .rows[i].cells[3].childNodes[0].value,sbody .rows[i].cells[4].childNodes[0].value);
       
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
                window.location.href = "PerformanceT.aspx?ModuleID=2011801";
//               SearchFlowData();
            }
            //保存失败
            else 
            {  
                hidePopup();
               // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                 popMsgObj.ShowMsg( data.data);
            }
        } 
    });  
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
         var quanZhong=sbody .rows[i].cells[3].childNodes[0].value.Trim();
         if (quanZhong =="")
         {
            signEmployeeID=false;
      
         fieldText += "权重信息|";
   	   	 msgText += "请输入权重|";
         
           popMsgObj.Show(fieldText, msgText);
         return false  ;
         }
        else
        {
         if (isNaN (quanZhong ))
         {
         
          signEmployeeID=false;
      
         fieldText += "权重信息|";
   	   	 msgText += "权重必须输入数字|";
         
           popMsgObj.Show(fieldText, msgText);
         return false  ;
         }
         }
         
      //   alert (cTrim (sbody .rows[i].cells[2].childNodes[0].value,0));
      var liuCheng=sbody .rows[i].cells[1].childNodes[0].value.Trim();
            if (''== liuCheng )
         {
         
          signEmployeeID=false;
      
         fieldText += "流程顺序|";
   	   	 msgText += "请填写流程顺序|";
         
           popMsgObj.Show(fieldText, msgText);
         return false  ;
         }
         else
         {
         
         
          if (isNaN (sbody .rows[i].cells[1].childNodes[0].value.Trim()))
         {
         
          signEmployeeID=false;
      
         fieldText += "流程顺序|";
   	   	 msgText += "流程顺序必须输入数字|";
         
           popMsgObj.Show(fieldText, msgText);
         return false  ;
         }
         }
      // alert (sbody .rows[i].cells[1].childNodes[0].title);
       
//         if (" "==sbody .rows[i].cells[0].childNodes[0].title)
//         {
//        
//          signEmployeeID=false;
//      
//         fieldText += "模板|";
//   	   	 msgText += "请选择模板|";
//         
//           popMsgObj.Show(fieldText, msgText);
//         return false  ;
//         }
  //  alert (   cTrim (sbody .rows[i].cells[1].childNodes[0].value,0));
    
    var kaoPingRen=sbody .rows[i].cells[0].childNodes[0].value.Trim();
          if (''==  kaoPingRen )
         {
      
          signEmployeeID=false;
      
         fieldText += "考评人|";
   	   	 msgText += "请选择考评人|";
         
           popMsgObj.Show(fieldText, msgText);
         return false  ;
         }
          if (" "==sbody .rows[i].cells[2].childNodes[0].value.Trim())
         {
         
          signEmployeeID=false;
      
         fieldText += "步骤名称|";
   	   	 msgText += "请填写步骤名称|";
         
           popMsgObj.Show(fieldText, msgText);
         return false  ;
         }
         
         
        
          sum =sum +parseFloat(sbody .rows[i].cells[3].childNodes[0].value.Trim());
         
       
       } 
         if ((sum%100)!=0)
       { 
          signEmployeeID=false;
       
         fieldText += "权重信息|";
   	   	 msgText += "权重项目相加须等于100|";
       
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
 
 
  function DeleteTemplate()
 {
 var tab=document .getElementById("tbTemplateEdit");
        var sbody=tab.tBodies [0];
        var emList=$(".ScoreELCheck");
        var checkedEm=new Array ();
      
        var len=emList.length;
        
        for (var i=len-1;i>-1 ;i--)
       {
          if (emList[i] .checked==true )
         {
            sbody.deleteRow(i);
          }
       }
 }
   function EditTemplate()
  {
   var tab=document .getElementById("tbTemplateEdit");
        var sbody=tab.tBodies [0];
        var rowID = sbody.rows.length;
        var newRow=sbody .insertRow(-1);
        newRow .style.background="#ffffff";
        newRow .Class="newro";
        newRow.align="center";
//   var newCell1=newRow .insertCell(-1);
//     newCell1 .innerHTML = "<input  id='txtEditTemp" + rowID + "' readonly ='readonly'  type='text' title=' '   class='tdinput' onclick=javascript:popTemplateObj.ShowList('txtEditTemp" + rowID + "')  class='tempList'/>";//添加列内容
     var newCell2=newRow .insertCell(-1);
     newCell2 .innerHTML =   "<input  id='UsertxtFlowStepActor2" + rowID + "'   type='text'   class='tdinput' onclick=alertdiv('UsertxtFlowStepActor2"+rowID+",txtEditScore"+rowID+"') />"+"<input  id='txtEditScore"+rowID+"'   class='tempList'  type='hidden'  />";//添加列内容
     var newCell3=newRow .insertCell(-1);
   newCell3 .innerHTML ="<input id='txt' type='text' value=' ' size='5' class='tdinput'/>";
     var newCell4=newRow .insertCell(-1);
   newCell4 .innerHTML ="<input id='txt' type='text' value=' ' class='tdinput'/>";
     var newCell5=newRow .insertCell(-1);
   newCell5 .innerHTML ="<input id='txt' type='text' value=' ' size='10' class='tdinput'/>";
    var newCell6=newRow .insertCell(-1);
   newCell6.innerHTML ="<input id='txt' type='text' value=' ' class='tdinput'/>";
   var newCell7=newRow .insertCell(-1);
        newCell7 .innerHTML= " <input  type='checkbox'       class='ScoreELCheck' class='tdinput'/>";
  
  }
  
   function DoDelete() {
        if(confirm("删除后不可恢复，确认删除吗！"))
	      {
	        //获取选择框
	        var chkList = document.getElementsByName("chkSelect1");
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
//	        alert (deleteNos);
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
	    }
	    
	    
	     function cTrim(sInputString,iType)   // Description: sInputString 为输入字符串，iType为类型，分别为 0 - 去除前后空格; 1 - 去前导空格; 2 - 去尾部空格
 {   
    var sTmpStr = ' ' ;
    var i = -1 ;
    if(iType == 0 || iType == 1) 
    {  
          while(sTmpStr == ' ') 
          {  
            ++i ; 
             sTmpStr = sInputString.substr(i,1) ;
           } 
         sInputString = sInputString.substring(i) ;
    } 
   if(iType == 0 || iType == 2)  
   {  
           sTmpStr = ' ' ;
            i = sInputString.length ; 
            while(sTmpStr == ' ') 
             {  
               --i;  
               sTmpStr = sInputString.substr(i,1); 
              }  
            sInputString = sInputString.substring(0,i+1);  
    }  
    return sInputString;  
     }


