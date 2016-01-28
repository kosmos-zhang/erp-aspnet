  $(document).ready(function(){
        requestobj = GetRequest(); 
 
  
    var deptid = requestobj['deptid'];
    var RequestDept = requestobj['RequestDept'];
    var type = requestobj['type'];
    var year = requestobj['year'];
    var month = requestobj['month']; 
 
    
    if(typeof(deptid)!="undefined")
    { 
    document .getElementById ("hdPara").value="deptid="+escape (deptid)+"&RequestDept="+escape ( RequestDept)+"&type="+escape ( type)+"&year="+escape ( year) +"&month="+escape (month)  ;  
    
  
    
      document .getElementById ("hidDeptID").value=deptid;
      document .getElementById ("hidRequestDept").value=RequestDept;
      document .getElementById ("hidtype").value=type;
      document .getElementById ("hidyear").value=year;
      document .getElementById ("hidmonth").value=month; 
      GetPageList(type);
    
       SearchData(1 );
    } 
});
   
  
 function GetPageList(type)
 { 
     if (type==="1")//招聘人数
     {
            document .getElementById ("trZhoping").style.display="Block";                                      
     }
     else if (type=="2" )  //面试人数   
     {
     document .getElementById ("trMianShi").style.display="Block";              
     }
        else if ( type=="3") //报道人数
     {
     document .getElementById ("trBaoDao").style.display="Block";              
     }
     else if ( type=="4") //迟到人数
     {
     document .getElementById ("trChiDao").style.display="Block";              
     }
       else if ( type=="5") //早退人数
     {
     document .getElementById ("trZaoTui").style.display="Block";              
     }
          else if ( type=="6") //旷工人数 
     {
     document .getElementById ("trKuangGong").style.display="Block";              
     }
        else if ( type=="7") //请假人数 
     {
     document .getElementById ("trQingJia").style.display="Block";              
     }
       else if ( type=="8" ) //迁出人数 
     {
     document .getElementById ("trQianChu").style.display="Block";              
     }
      else if ( type=="9") //||迁入人数 
     {
     document .getElementById ("trQianRu").style.display="Block";              
     }
        else if ( type=="10") //||离职人数 
     {
     document .getElementById ("trLiZhi").style.display="Block";              
     }
     
     
     
     
  }
  
    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var action = "";//操作
    var orderBy = "";//排序字段
    var ifdel="0";//是否删除
        




function SearchData(aa)
{
    TurnToPage(aa);
}

//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex)
{
       currentPageIndex = pageIndex;
         
     
     var ps=  document .getElementById ("hdPara").value;
              
//       document.getElementById("hdPara").value = "orderby="+orderBy+"&quterID="+escape(quterID)+"&QuaterAdmin="+escape (QuaterAdmin)  ;
 
      action ="EmployeeConditionByMonthPrint_Select";
       $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:  '../../../Handler/OperatingModel/HumanManager/HumanReport.ashx',//目标地址
       cache:false,
       data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&Action="+action+"&orderby="+orderBy+"&"+ps  ,//数据
       beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
       
       success: function(msg){  
                //数据获取完毕，填充页面据显示
                //数据列表
                $("#pageDataList1 tbody").find("tr.newrow").remove();
             var type=document .getElementById ("hidtype").value;
             if (type ==="1")//招聘人数
             {
                       $.each(msg.data,function(i,item){
                      if(item.DeptID != null && item.DeptID != ""&& item.DeptID != "0")
                     {
             
                        $("<tr class='newrow'  style='width:100%'></tr>").append("<td height='22' align='center'>" + item.DeptName + "</td>"+
                        "<td height='22' align='center'>" + item.PersonCount + "</td>"+   
                        "<td height='22' align='center'>"+ item.StartDate +"</td>"+ 
                            "<td height='22' align='center'>" + item.EndDate + "</td>"+  
                        "<td height='22' align='center'>"+item.EmployeeName+"</td>").appendTo($("#pageDataList1 tbody"));}
                   });
               }
               else    if (type ==="2")//面试人数 
             {
                       $.each(msg.data,function(i,item){
                      if(item.DeptID != null && item.DeptID != ""&& item.DeptID != "0")
                     {
             
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + item.DeptName + "</td>"+
                        "<td height='22' align='center'>" + item.QuarterName + "</td>"+   
                        "<td height='22' align='center'>"+ item.EmployeeName +"</td>"+  
                            "<td height='22' align='center'>" + item.InterviewDate + "</td>"+  
                                     "<td height='22' align='center'>" + item.TestScore + "</td>"+  
                                              "<td height='22' align='center'>" + item.InterviewResultContent + "</td>"+  
                                                       "<td height='22' align='center'>" + item.CheckDate + "</td>"+  
                        "<td height='22' align='center'>"+item.FinalResultContent+"</td>").appendTo($("#pageDataList1 tbody"));}
                   });
               }
                    else    if (type ==="3")// 报道人数
             {
                       $.each(msg.data,function(i,item){
                      if(item.DeptID != null && item.DeptID != ""&& item.DeptID != "0")
                     {
             
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + item.DeptName + "</td>"+
                        "<td height='22' align='center'>" + item.EmployeeName + "</td>"+    
                        "<td height='22' align='center'>"+item.EnterDate+"</td>").appendTo($("#pageDataList1 tbody"));}
                   });
               }
               else    if (type ==="4"||type ==="5")// 迟到人数   || 早退时间
             {
                       $.each(msg.data,function(i,item){
                      if(item.DeptID != null && item.DeptID != ""&& item.DeptID != "0")
                     {
             
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + item.DeptName + "</td>"+
                         "<td height='22' align='center'>" + item.EmployeeName + "</td>"+    
                          "<td height='22' align='center'>" + item.Date + "</td>"+    
                          "<td height='22' align='center'>" + item.StartTime + "</td>"+      
                         "<td height='22' align='center'>"+item.DelayTimeLong+"</td>").appendTo($("#pageDataList1 tbody"));}
                   });
               }
                   else    if (type ==="6")// 旷工人数   
             {
                       $.each(msg.data,function(i,item){
                      if(item.DeptID != null && item.DeptID != ""&& item.DeptID != "0")
                     {
             
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + item.DeptName + "</td>"+
                         "<td height='22' align='center'>" + item.EmployeeName + "</td>"+   
                         "<td height='22' align='center'>"+item.EveryDay+"</td>").appendTo($("#pageDataList1 tbody"));}
                   });
               } 
                      else    if (type ==="7")// 请假人数   
             {
                       $.each(msg.data,function(i,item){
                      if(item.DeptID != null && item.DeptID != ""&& item.DeptID != "0")
                     {
             
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + item.DeptName + "</td>"+
                         "<td height='22' align='center'>" + item.EmployeeName + "</td>"+   
                         "<td height='22' align='center'>" + item.LeveTypeName + "</td>"+   
                         "<td height='22' align='center'>" + item.ApplyDate + "</td>"+   
                         "<td height='22' align='center'>" + item.StartDate + "</td>"+   
                         "<td height='22' align='center'>"+item.EndDate+"</td>").appendTo($("#pageDataList1 tbody"));}
                   });
               }
                                     else    if (type ==="8"||type ==="9")// 迁出人数  ||迁入人数  
             {
                       $.each(msg.data,function(i,item){
                      if(item.DeptID != null && item.DeptID != ""&& item.DeptID != "0")
                     {
             
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + item.OldDeptName + "</td>"+
                          "<td height='22' align='center'>" + item.OldQuarterName + "</td>"+   
                         "<td height='22' align='center'>" + item.EmployeeName + "</td>"+   
                         "<td height='22' align='center'>" + item.NewDeptName + "</td>"+    
                           "<td height='22' align='center'>" + item.NewQuarterName + "</td>"+   
                         "<td height='22' align='center'>"+item.OutDate+"</td>").appendTo($("#pageDataList1 tbody"));}
                   });
               }
                                                else    if (type ==="10")// 离职人数
             {
                       $.each(msg.data,function(i,item){
                      if(item.DeptID != null && item.DeptID != ""&& item.DeptID != "0")
                     {
             
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + item.DeptName + "</td>"+
                          "<td height='22' align='center'>" + item.EmployeeName + "</td>"+  
                         "<td height='22' align='center'>"+item.OutDate+"</td>").appendTo($("#pageDataList1 tbody"));}
                   });
               }
               
              
 
               
                //页码
               ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
               "<%= Request.Url.AbsolutePath %>",//[url]
                {style:pagerStyle,mark:"pageDataList1Mark",
                totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                onclick:"TurnToPage({pageindex});return false;"}//[attr]
                );
              totalRecord = msg.totalCount;
             // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
              document.getElementById("Text2").value=msg.totalCount;
              $("#ShowPageCount").val(pageCount);
              ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
              $("#ToPage").val(pageIndex);
              },
       error: function() 
       {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
       }, 
       complete:function(){if(ifdel=="0"){hidePopup();}$("#pageDataList1_Pager").show();Ifshow(document.getElementById("Text2").value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
       });
}

//table行颜色
function pageDataList1(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

function Ifshow(count)
{
    if(count=="0")
    {
        document.getElementById("divpage").style.display = "none";
        document.getElementById("pagecount").style.display = "none";
    }
    else
    {
        document.getElementById("divpage").style.display = "block";
        document.getElementById("pagecount").style.display = "block";
    }
}

//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount,newPageIndex)
{
    if(!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    } 
    if(!PositiveInteger(newPageIndex))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
        return;
    } 

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        ifdel = "0";
        this.pageCount=parseInt(newPageCount);
        TurnToPage(parseInt(newPageIndex));
    }
}
    //排序
    function OrderBy(orderColum,orderTip)
    {
        if (totalRecord == 0) 
     {
        return;
     }
        ifdel = "0";
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
document .getElementById ("OrderT").value=orderBy;
        TurnToPage(1);
    }
    
//打印 
function pageSetup()
{   
    var Url = $("#hdPara").val();
     if(Url == "")
     {
        popMsgObj.Show("打印|","请检索数据后再预览|");
        return;
     }
    window.open("EmployeeSexPrint.aspx?" + Url);
}