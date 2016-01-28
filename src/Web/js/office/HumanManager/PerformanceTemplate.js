$(document).ready(function(){
     DoSearchPerformanceTypeInfo();
       document .getElementById ("HDCodeFlag").value="INSERT";
//       SearchFlowData();
 document .getElementById ("hidCon1").value="1";
  
});

 String.prototype.length2 = function() {
    var cArr = this.match(/[^\x00-\xff]/ig);
    return this.length + (cArr == null ? 0 : cArr.length);
}




  function textcontrol(taId,maxSize) 
  {   
                // 默认 最大字符限制数   
                var defaultMaxSize = 250;   
                var ta = document.getElementById(taId);   
                // 检验 textarea 是否存在   
               if(!ta) {   
                          return;   
                }   
                // 检验 最大字符限制数 是否合法   
                if(!maxSize) {   
                   maxSize = defaultMaxSize;   
               } else {   
                    maxSize = parseInt(maxSize);   
                    if(!maxSize || maxSize < 1) {   
                        maxSize = defaultMaxSize;   
                   }   
               }   
               　　 if (ta.value.length2() > maxSize) {   
                   ta.value=ta.value.substring(0,maxSize);   
                 return true ;
                     
               }    
           } 
           
           
function DoSearchPerformanceTypeInfo()
{
    //编辑模式
    //document.getElementById("hidEditFlag").value = "UPDATE";
    //设置ID
   /// document.getElementById("hidElemID").value = elemID;
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/PerformanceTemplate_Add.ashx?action=GetPerformanceTypeInfo",
        dataType:'json',//
        cache:false,
        beforeSend:function()
        {              
        }, 
        error: function()
        {
           // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
             popMsgObj.ShowMsg('请求发生错误 ！');
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
//              var tab=document .getElementById("tbTemplateEdit");
//        var sbody=tab.tBodies [0];
//       
//        var rowCount=sbody .rows.length-1;
//        //alert (rowCount);
//        if (rowCount >0)
//        {
//          for (var col=1;col <rowCount+1 ;col++)
//          {
//          //  alert (col );
//            sbody.deleteRow(1);
//          }
//        }
            
//            document.getElementById ("divShowEdit").style.display="block";
             document.getElementById("selPerformanceType").options.length=0;
            //  document.getElementById("selPerformanceType").options.add(new Option("--请选择--","请选择"))
                document.getElementById("selSearchPerformanceType").options.length=0;
              document.getElementById("selSearchPerformanceType").options.add(new Option("--请选择--","请选择"))
            $.each(msg.data
                ,function(i,item)
                {
                    if(item.ID != null && item.ID != "")
                     document.getElementById("selPerformanceType").options.add(new Option(item.TypeName,item.ID))
                      document.getElementById("selSearchPerformanceType").options.add(new Option(item.TypeName,item.ID))
                        
                  
            });
        }
    });
    //显示修改页面
   // document.getElementById("divEditCheckItem").style.display = "block"; 
}

function DoSave()
{
     if (!CheckInputInfo())
     {
       return ;
      }
   
   var title=document .getElementById ("txtTitle").value.Trim();
   var performanceType=document.getElementById("selPerformanceType").value.Trim()
   var usedStatus=document.getElementById("sltSearchUsedStatus").value.Trim();
   var templateNo;
   var CodeRuleID;
    var editFlag=   document .getElementById ("HDCodeFlag").value.Trim();
    if (editFlag =="INSERT")
    {
   var   aim_codeRule = document.getElementById("AimNum_ddlCodeRule").value.Trim();
        //手工输入的时候
        if ("" == aim_codeRule)
        {
             templateNo=  document.getElementById("AimNum_txtCode").value.Trim();
            
        }
        else 
        {
          CodeRuleID=  document.getElementById("AimNum_ddlCodeRule").value.Trim();
        }
     }else
     {
     
     templateNo=document.getElementById("txtEditPerformTemp").value.Trim();
     
     }
   
             var emList=$(".RateClass");
             var len=emList.length;
             var message=new Array ();
        for (var i=0;i<len ;i++)
          {
            message .push (emList[i].title+"_"+emList[i].value);
           }
   var  remark=document.getElementById("txtRemark").value;    
 
    var postParaList="&message="+escape ( message) +"&templateNo="+escape ( templateNo)+"&usedStatus="+escape ( usedStatus)+"&title="+escape ( title)+"&performanceType="+escape ( performanceType)+"&remark="+escape ( remark)+"&editFlag="+escape ( editFlag)+"&CodeRuleID="+escape ( CodeRuleID);

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/HumanManager/PerformanceTemplate_Add.ashx?action=saveInfro&" + postParaList,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            AddPop();
        },
        error: function() {
            //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
            popMsgObj.ShowMsg('请求发生错误 ！');
        },
        success: function(data) {
            //隐藏提示框
            hidePopup();
           
            //保存成功
            if (data.sta == 1) {
                popMsgObj.ShowMsg('保存成功 ！');
                 document.getElementById("HDCodeFlag").value = data.info;
                //设置ID 
                document.getElementById("txtEditPerformTemp").value = data.data;
                document.getElementById("txtEditPerformTemp").style.display="block"; 
                 document.getElementById("txtPerformTmNo").style.display="none"; 
                
                
                
            }else if (data.sta == 2) {
                popMsgObj.ShowMsg('该编号已被使用，请选择未使用的编号！');
            }
            //保存失败
            else {
                hidePopup();
                popMsgObj.ShowMsg('保存失败,请确认！');
            }
        }
    });  
}


function ClearInput()
{

 var sbody=document .getElementById("tbEdit");
 var rowCount=sbody .rows.length-1;
        
        //alert (rowCount);
        if (rowCount >0)
        {
          for (var col=1;col <rowCount+1 ;col++)
          {
        //  alert (col);
          //  alert (col );
            sbody.deleteRow(1);
          }
        }
        
      document .getElementById ("txtTitle").value=""; 

 document .getElementById ("txtRemark").value="";
 document.getElementById("HDCodeFlag").value = "INSERT";
 document.getElementById("txtEditPerformTemp").style.display = "none";
 document.getElementById("txtEditPerformTemp").value = "";

}

function CheckInputInfo()
{
var fieldText = "";
    //出错提示信息
       var msgText = "";
//  var emList=$(".treeClass");
//  var len=emList.length;
//  var selectedNoes=new Array ();
  var itemCheck=false ;
//  for (var i=0;i<len ;i++)
//   {
//        if (emList[i] .checked==true )
//       {
//       
//       itemCheck =true;
//       }
//   }

if (""==document .getElementById ("txtTitle").value.Trim())
{
       fieldText += "模板名称|";
       msgText += "请填写模板名称|";
       itemCheck =true ;

}


if ("请选择"==document.getElementById("selPerformanceType").value.Trim())
{
       fieldText += "考核类型|";
       msgText += "请选择考核类型|";
       itemCheck =true ;

}
if (""==document.getElementById("sltSearchUsedStatus").value.Trim())
{
       fieldText += "启用状态|";
       msgText += "请选择启用状态|";
       itemCheck =true ;

}
 
if (document.getElementById("HDCodeFlag").value.Trim()=="INSERT")
{
  aim_codeRule = document.getElementById("AimNum_ddlCodeRule").value.Trim();
        //手工输入的时候
        if ("" == aim_codeRule)
        {
            //人员编号
            if(""== document.getElementById("AimNum_txtCode").value.Trim())
            {
            
             fieldText += "模板编号|";
             msgText += "请选输入模板编号|";
             itemCheck =true ;
            }
            else if (isnumberorLetters( document.getElementById("AimNum_txtCode").value.Trim()))
          {
            itemCheck = true;
            fieldText += "模板编号|";
             msgText += "模板编号只能包含字母或数字！|";
          }
          
          if (checkstr(document.getElementById("AimNum_txtCode").value.Trim(),50))
          {
          
           itemCheck = true;
            fieldText += "模板编号|";
             msgText += "模板编号长度过长！|";
          
          }
            
            
            
        }
        
        else
        {
            if (isnumberorLetters( document.getElementById("AimNum_ddlCodeRule").value.Trim()))
          {
            itemCheck = true;
            fieldText += "模板编号|";
             msgText += "模板编号只能包含字母或数字！|";
          }
        }

}



      var tbPerformanceElem=document .getElementById ("tbEdit");
      var count=tbPerformanceElem.rows.length;
      if ((count ==0)||(count <0))
      {
             fieldText += "模板指标|";
             msgText += "请添加模板指标|";
             itemCheck =true ;
      
      }


    var txtRemark = document.getElementById("txtRemark").value.Trim();
    if(strlen(txtRemark)> 500)
{
             fieldText += "备注项|";
             msgText += "备注最多只允许输入500个字符|";
             itemCheck =true ;
}

 
 
    var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
            itemCheck = true ;
            fieldText = fieldText + RetVal+"|";
   		    msgText = msgText +RetVal+  "不能含有特殊字符|";
   		    
    }



  var emList=$(".RateClass");
  var len=emList.length;
  //alert (len);
 for (var i=0;i<len ;i++)
   {
        if (""==emList[i].value.Trim() )
       {
       fieldText += "权重值|";
   	   	 msgText += "请输入权重值|";
       itemCheck =true;
       break ;
       }else
       {
          if (isNaN (emList[i].value.Trim()))
         {
         
          itemCheck =true;
      
         fieldText += "权重信息|";
   	   	 msgText += "权重必须输入数字|";
            break ;
        
     
         }else
         {
           var sValue= parseInt(emList[i].value.Trim(),10);
           if (sValue >101)
           {
               itemCheck =true;
      
         fieldText += "权重信息|";
   	   	 msgText += "权重范围在0于100之间|";
            break ;
        
       
           }
         }
       }
   }
    if (itemCheck )
   {
           popMsgObj.Show(fieldText, msgText);
           return false  ;
   }
   var sum=0.00;
    for (var a=0;a<len ;a++)
   {
      sum=sum +parseFloat(emList[a].value.Trim());
     //  alert (sum);
   }
  // alert (sum);
 
   if ((sum <100.00)||(sum >100.00))
       { 
          signEmployeeID=false;
       
         fieldText += "权重信息|";
   	   	 msgText += "权重分数必须相加等于100|";
   	   	  popMsgObj.Show(fieldText, msgText);
   	   	     return false  ;
       
       }

return true ;

}
/* 分页相关变量定义 */  

var pageCount = 10;//每页显示记录数
var totalRecord = 0;//总记录数
var pagerStyle = "flickr";//jPagerBar样式
var currentPageIndex = 1;//当前页
var orderBy = "ModifiedDate_d";//排序字段
function SearchFlowData(currPage)
{ 
         document .getElementById ("hidCon1").value="sign";
    var isFlag=true ;
    var fieldText="";
    var msgText="";

    if( !CheckSpecialWord(document .getElementById ("inpSearchTitle").value.Trim()))
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
function getChage( obj)
{

if (obj.checked==false )
{
 document .getElementById ("chkCheckAll").checked=false ;
}
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
   
     
  //document .getElementById ("trSear").style.display="none";
    
    
    
    var postParam = "PageIndex=" + pageIndex + "&PageCount=" + pageCount + "&action=" + action + "&OrderBy=" + orderBy ;
     if ("请选择"!=document .getElementById ("selSearchPerformanceType").value.Trim())
    {
    postParam+="&TypeID="+document .getElementById ("selSearchPerformanceType").value.Trim();
    
    }
     if (""!=document .getElementById ("inpSearchTitle").value.Trim())
    {
    postParam+="&Title="+escape ( document .getElementById ("inpSearchTitle").value.Trim());
    
    }
    
    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  '../../../Handler/Office/HumanManager/PerformanceTemplate_Add.ashx?' + postParam,//目标地址
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
                        + "<input id='chkSelect' class='TemplateNo'  name='chkSelect' value='" + item.TemplateNo + "'  type='checkbox'  onpropertychange='getChage(this)' />" + "</td>" //选择框
                        + "<td height='22' align='center'><a href='#' onclick =GetPerformanceElemInfo('"+item.TemplateNo +"')  >" + item.Title + "</a></td>"
                        + "<td height='22' align='center'>" + item.TypeName  + "</td>"
                          + "<td height='22' align='center'>" + item.CreaterName  + "</td>"
                        + "<td height='22' align='center'>" + item.CreateDate.substring(0, 10) + "</td>").appendTo($("#tbFlowDetail tbody")
                        //启用状态
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
function GetPerformanceElemInf(templateNo)
{ //alert (templateNo );
    //编辑模式
    //document.getElementById("hidEditFlag").value = "UPDATE";
    //设置ID
   /// document.getElementById("hidElemID").value = elemID;
    //alert (templateNo);
    document.getElementById("txtEditPerformTemp").style.display = "block";
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/PerformanceTemplate_Add.ashx?action=GetPerformanceElemInf&templateNo=" + templateNo,
        dataType:'json',
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
            //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
             popMsgObj.ShowMsg('请求发生错误 ！');
        }, 
        success:function(msg) 
        {
            //隐藏提示框  
            hidePopup();
              var sbody=document .getElementById("tbEdit");
        var rowCount=sbody .rows.length-1;
        if (rowCount >0)
        {
          for (var col=1;col <rowCount+1 ;col++)
          {
            sbody.deleteRow(1);
          }
        }
            $.each(msg.data  ,function(i,item)
                {
                    if(item.ElemID != null && item.ElemID != "")
                    $("<tr class='newro'></tr>").append("<td height='22' align='center'><input  id='txtEditTemp" + i + "' readonly ='readonly'  type='text' value="+item.ElemName+" title="+item.ElemID+" class='tdinput'  class='tempList'/></td>"+ "<td height='22' align='center'><input  id='txtEditScore" + i + "'   type='text' value="+item.Rate+" title="+item.ElemID+"    class='RateClass'   style='border:none;border-bottom:solid   0px   black;'/></td>" + "<td height='22' align='center'><input  type='checkbox'       class='Delete' /></td>").appendTo($("#tbEdit tbody"),//启用状态 
               document.getElementById ("txtTitle").value =item.Title,
               ///alert (i),
                 document.getElementById ("txtRemark").value=item .Description,
                  document.getElementById ("sltSearchUsedStatus").value=item .UsedStatus,
                  document.getElementById ("selPerformanceType").value=item .typeID, 
                document.getElementById ("txtEditPerformTemp").value=item .TemplateNo,
                document.getElementById ("txtPerformTmNo").style.display="none" ,
                document .getElementById ("HDCodeFlag").value="UPDATE"
               
                    );
            });
        }
    });
    //显示修改页面
   // document.getElementById("divEditCheckItem").style.display = "block"; 
  
   Show();
}

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
 function Delete()
      { 
        var sbody=document .getElementById("tbEdit");
     
   
        var emList=$(".Delete");
        var checkedEm=new Array ();
      
        var len=emList.length;
           
        for (var i=len-1;i>=0 ;i--)
       {
        
              
              if (emList[i] .checked==true )
             {
                var totalCount=0;
                if (i==0)
                {
                    if (emList [i].title=="1")
                 {
                 totalCount =0;
                 }
                 else
                 {
                 totalCount =0;
                 }
                }
                else
                {
              for (var a=0;a<i ;a++)
              {
                 if (emList [a].title=="1")
                 {
                 totalCount =totalCount +2;
                 }
                 else
                 {
                 totalCount =totalCount +1;
                 }
              }
              }
       //   totalCount=    totalCount+2
              //alert (totalCount+2 );
              var ss=totalCount +1;
                 if (emList [i].title=="1")
                 {
                    sbody.deleteRow(ss);   
                     sbody.deleteRow(ss);   
                  }
                  else {
                      if (sbody.rows[ss]) {
                          sbody.deleteRow(ss);
                      } 
                    }
            
              }
        }
       }
       
function Show()
{
document .getElementById ("add1").style.display="block";
document .getElementById ("add2").style.display="block";  
document .getElementById ("trSearch").style.display="none";
document.getElementById("txtPerformTmNo").style.display = "block";





}

function ShowEdit() {
     var myDate = new Date();
       document .getElementById ("dvCreateDate").innerHTML=myDate.toLocaleDateString().substring (0,10);  
    Show();
    ClearInput(); 
    document .getElementById ("trSearc").style.display="none";
    document .getElementById ("trSear").style.display="none";
}

function DoBack()
{
document .getElementById ("trSear").style.display="block";
  document .getElementById ("trSearc").style.display="block";
document .getElementById ("add1").style.display="none";
document .getElementById ("add2").style.display="none";  
document .getElementById ("trSearch").style.display="block";
document .getElementById ("HDCodeFlag").value="INSERT";
 document.getElementById("AimNum_ddlCodeRule").value="";
  document.getElementById("AimNum_txtCode").value="";
         SearchFlowData();


}    
	//判断字符串是否超过指定的digit长度
	function checkstr(str,digit)
	{ 
	
	     //定义checkstr函数实现对用户名长度的限制
	        var n=0;         //定义变量n，初始值为0
	        for(i=0;i<str.length;i++){     //应用for循环语句，获取表单提交用户名字符串的长度
	        var leg=str.charCodeAt(i);     //获取字符的ASCII码值
	        if(leg>255)
	        {       //判断如果长度大于255 
	          n+=2;       //则表示是汉字为两个字节
	        }
	        else 
	        {
	         n+=1;       //否则表示是英文字符，为一个字节
	        }
	        }
	        
	      //  alert (n);
	        
	        if (n>digit)
	        {        //判断用户名的总长度如果超过指定长度，则返回true
	        return true;
	        }
	        else 
	        {return false;       //如果用户名的总长度不超过指定长度，则返回false
	        }
	    }
	    function DoDelete(sign) {
	      if(confirm("删除后不可恢复，确认删除吗！"))
	      {
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
	            var postParam = "Action=DeleteInfo&sign="+sign+"&DeleteNO=" + escape(deleteNos);
	            //删除
	            $.ajax(
        {
            type: "POST",
            url: "../../../Handler/Office/HumanManager/PerformanceTemplate_Add.ashx?" + postParam,
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
                    TurnToFlowPage(1);
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
	    
	    
	   
function GetPerformanceElemInfo(templateNo)
{
       document.getElementById("txtEditPerformTemp").style.display = "block";
         document.getElementById("trSear").style.display = "none";
         document.getElementById("trSearc").style.display = "none";
    //编辑模式
    //document.getElementById("hidEditFlag").value = "UPDATE";
    //设置ID
   /// document.getElementById("hidElemID").value = elemID;
   var outerString='';
   var outerStringTemp='';
   $.ajax({
       type: "POST",
       url: "../../../Handler/Office/HumanManager/PerformanceTemplate_Add.ashx?action=GetPerformanceElemInf&templateNo=" + templateNo,
       dataType: 'json', //
       cache: false,
       beforeSend: function() {
       },
       error: function() {
           popMsgObj.ShowMsg(' 请求发生错误！');
       },
       success: function(msg) {
           //隐藏提示框  
           var employee = new Array();
           var templateNo = new Array();
           var list = new Array();
           hidePopup();
           //        var sbody=document .getElementById("tbEdit");
           //        var rowCount=sbody .rows.length-1;
           //        if (rowCount >0)
           //        {
           //          for (var col=1;col <rowCount+1 ;col++)
           //          {
           //            sbody.deleteRow(1);
           //          }
           //        }

           $("#tbEdit tbody").find("tr.newrow").remove();
           /* 设置考核类型信息 */
           $.each(msg.data, function(i, item) {
               //要素名称

               document.getElementById("txtTitle").value = item.Title,
                  document.getElementById("txtRemark").value = item.Description;
               document.getElementById("sltSearchUsedStatus").value = item.UsedStatus;
               document.getElementById("selPerformanceType").value = item.typeID;
               document.getElementById("txtEditPerformTemp").value = item.TemplateNo;
               document.getElementById("txtPerformTmNo").style.display = "none";
               document.getElementById("HDCodeFlag").value = "UPDATE";
               document.getElementById("dvCreateDate").innerHTML = item.CreateDate;
               list.push(i, item.ParentID, item.ParentName, item.ElemID, item.ElemName, item.Rate);
               //   a                a+1                        a+2                      a+3                a+4                  a+5
               //                  if (item .ParentName!=null &&item .ParentName!='')
               //                  {
               //                    outerString +="<tr class='newrow'> <td height='22' align='center'><input  type='checkbox' class='Delete'  onpropertychange='getChageFlow(this)'/></td><td height='22' align='center' >"+item.ParentName +"</td><td height='22' align='center' ><input  id='txtEditTemp" + i + "' readonly ='readonly'  type='text' value="+item.ElemName+" title="+item.ElemID+" class='tdinput'  class='tempList'/></td>"+ "<td height='22' align='center'><input  id='txtEditScore" + i + "'   type='text' value="+item.Rate+" title="+item.ElemID+"    class='RateClass'   style='border:none;border-bottom:solid   0px   black;'/></td></tr>";
               //                  }
               //                  else
               //                  {
               //                    outerString +="<tr class='newrow'><td height='22' align='center'><input  type='checkbox' class='Delete'  onpropertychange='getChageFlow(this)'/></td><td height='22' align='center' colspan='2'><input  id='txtEditTemp" + i + "' readonly ='readonly'  type='text' value="+item.ElemName+" title="+item.ElemID+" class='tdinput'  class='tempList'/></td>"+ "<td height='22' align='center'><input  id='txtEditScore" + i + "'   type='text' value="+item.Rate+" title="+item.ElemID+"    class='RateClass'   style='border:none;border-bottom:solid   0px   black;'/></td>" + "</tr>";
               //                    }



           });
         
           for (var a = 0; a < list.length - 4; a++) {
               var ss = list[a + 1];
               if (list[a + 1] == null || list[a + 1] == '') {
                   outerStringTemp += "<tr class='newrow'><td height='22' align='center'><input  type='checkbox' class='Delete'  title='0'  onpropertychange='getChageFlow(this)'/></td><td height='22' align='center'  colspan='2' ><input  id='txtEditTemp" + list[a] + "' readonly ='readonly'  type='text' value=" + list[a + 4] + " title=" + list[a + 3] + " class='tdinput'  class='tempList'/></td>" + " <td height='22'  ><input  id='txtEditScore" + list[a] + "'   type='text' value=" + list[a + 5] + " title=" + list[a + 3] + "    class='RateClass'   style='border:none;border-bottom:solid   0px   black;'/></td>" + "</tr>";
                   a = a + 5;
               }
               else {
                   //                      list .push (i ,  item .ParentID,item .ParentName,item .ElemID,item.ElemName,item.Rate);
                   //                                / a          a+1            a+2            a+3         a+4          a+5
                   var count = 1;
                   var sign = 0;
                   for (var b = a; b < list.length - 4; b++) {
                       var sd = list[b + 7];
                       var dd = list[a + 1];
                       if (list[b + 7] == list[a + 1]) {
                           count++;
                       }
                       b = b + 5;
                   }
                   var sdsdd = a + count * 6;
                   for (var c = a; c < sdsdd; c++) {
                       if (sign == 0) {
                           outerStringTemp += "<tr class='newrow'> <td height='22' align='center'><input  type='checkbox' class='Delete'  onpropertychange='getChageFlow(this)' title='1'/></td><td height='22' align='center'  rowspan=" + count + "  >" + list[c + 2] + "</td><td height='22' align='center' ><input  id='txtEditTemp" + list[c] + "' readonly ='readonly'  type='text' value=" + list[c + 4] + " title=" + list[c + 3] + " class='tdinput'  class='tempList'/></td>" + "<td height='22'  ><input  id='txtEditScore" + list[c] + "'   type='text' value=" + list[c + 5] + " title=" + list[c + 3] + "    class='RateClass'   style='border:none;border-bottom:solid   0px   black;'/></td></tr>";
                           sign++;
                       }
                       else {
                           outerStringTemp += "<tr class='newrow'>     <td height='22' align='center'></td><td height='22' align='center' ><input  id='txtEditTemp" + list[c] + "' readonly ='readonly'  type='text' value=" + list[c + 4] + " title=" + list[c + 3] + " class='tdinput'  class='tempList'/></td>" + "<td height='22'  ><input  id='txtEditScore" + list[c] + "'   type='text' value=" + list[c + 5] + " title=" + list[c + 3] + "    class='RateClass'   style='border:none;border-bottom:solid   0px   black;'/></td></tr>";
                       }
                       c = c + 5;

                   }
                   a = a + count * 6 - 1;


               }






           }
           // debugger ;
           var tb = document.getElementById("tbEdit");

           if (tb.tBodies[0]) {
               //alert ("1");


               tb.outerHTML = "<table id='tbEdit' width='100%' border='0' cellspacing='0' cellpadding='3'  class='tab_a'><tr><th>选择<input type=\"checkbox\" id=\"chkCheckAll2\" name=\"chkCheckAll2\" onclick=\"changEdit(this);\"/></th><th  colspan='2' width='100'>指标名称</th><th width='200' >权重</th></tr><tbody>" + outerStringTemp + "</tbody></table>";
           }
           //  alert (tb.outerHTML );
           //alert (list );
       }

   }); 

       Show();
       
       
    }


             
      
function changEdit(obj)
{

        var emList=$(".Delete");
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

function getChageFlow( obj)
{

if (obj.checked==false )
{
 document .getElementById ("chkCheckAll2").checked=false ;
}
}