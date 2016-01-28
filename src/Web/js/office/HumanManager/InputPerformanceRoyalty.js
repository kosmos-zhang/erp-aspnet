    /*
* 查询
*/
function DoSearch(currPage)
{
   var EmployeeID=$("#txtEmployeeID").val();//选择的需要查询的人员
   var sltTaskflag=$("#sltTaskflag").val();//选择的考核类型
  
   var UrlParam="Act=search"+
   "&EmployeeID="+escape(EmployeeID)+"&Taskflag="+escape(sltTaskflag);
    
    //设置检索条件
    document.getElementById("hidSearchCondition").value = UrlParam;
    if (currPage == null || typeof(currPage) == "undefined")
    {
        TurnToPage(1);
    }
    else
    {
        TurnToPage(parseInt(currPage,10));
    }
}
 /*-----------------------------------------上面是查询-----------------------------------------------------------*/     
    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var action = "";//操作
    var orderBy = "";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)
    {
           $("#checkall").attr("checked",false);
           currentPageIndex = pageIndex;
           //获取查询条件
           var searchCondition = document.getElementById("hidSearchCondition").value;
           var UrlParam = "pageIndex=" + pageIndex + "&pageCount=" + pageCount + "&orderBy=" + orderBy + "&" + searchCondition;
             //进行查询获取数据 
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/HumanManager/InputPerformanceRoyalty.ashx?'+UrlParam,//目标地址
           cache:false,
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='ckb_"+item.ID+"' name='Checkbox1'  value="+item.ID+" onclick=IfSelectAll('Checkbox1','checkall')  type='checkbox'/>"+"</td>"+
                        "<td height='22' align='center' title=\""+item.EmployeeName+"\">"+ fnjiequ(item.EmployeeName,10) +"</td>"+
                        "<td height='22' align='center' title=\""+item.TaskNo+"\">"+fnjiequ(item.TaskNo,10)+"</td>"+
                        "<td height='22' align='center' title=\""+item.Title+"\">"+ fnjiequ(item.Title,10) +"</td>"+
                        "<td height='22' align='center' title=\""+item.TaskNum+"\">"+fnjiequ(item.TaskNum,10)+"</td>"+
                        "<td height='22' align='center' title=\""+item.TaskFlag+"\">"+ fnjiequ(item.TaskFlag,10) +"</td>"+
                        "<td height='22' align='center' title=\""+item.StartDate+"\">"+fnjiequ(item.StartDate,10)+"</td>"+
                        "<td height='22' align='center' title=\""+item.EndDate+"\">"+ fnjiequ(item.EndDate,10) +"</td>"+
                        "<td height='22' align='center' title=\""+item.BaseMoney+"\">"+fnjiequ(item.BaseMoney,10)+"</td>"+
                        "<td height='22' align='center' title=\""+item.Confficent+"\">"+ fnjiequ(item.Confficent,10) +"</td>"+
                        "<td height='22' align='center' title=\""+item.PerformanceMoney+"\">"+fnjiequ(item.PerformanceMoney,10)+"</td>").appendTo($("#pageDataList1 tbody"));
                        
                   });
                    //页码
                   ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPage({pageindex});return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                  document.getElementById("Text2").value=msg.totalCount;
                  $("#ShowPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                  $("#ToPage").val(pageIndex);
                  },
           error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
           complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow($("#Text2").val());pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    //table行颜色
function pageDataList1(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=1;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

//改变每页记录数及跳至页数
    function ChangePageCountIndex(newPageCount,newPageIndex)
    {
    
        
            //判断是否是数字
        if (!PositiveInteger(newPageCount))
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
            return;
        }
        if (!PositiveInteger(newPageIndex))
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
            this.pageCount=parseInt(newPageCount,10);
            TurnToPage(parseInt(newPageIndex,10));
        }
    }
//排序
function OrderBy(orderColum,orderTip)
{
    if (document.getElementById("hidSearchCondition").value == "" || document.getElementById("hidSearchCondition").value == null) return;
    var ordering = "d";
    var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
        ordering = "a";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↑");
    }
    else
    {
        
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderBy = orderColum+"_"+ordering;
    TurnToPage(1);
}

function Ifshow(count)
{
    if(count=="0")
    {
        document.all["divpage"].style.display = "none";
        document.all["pagecount"].style.display = "none";
    }
    else
    {
        document.all["divpage"].style.display = "block";
        document.all["pagecount"].style.display = "block";
    }
}

/*------------------------------------------删除 Start-------------------------------------------*/
function DoDel()
{

    if(confirm('删除后不可恢复，你确定要删除吗？'))
     {
      var ck = document.getElementsByName("Checkbox1");
            var ck2 = "";    
            for( var i = 0; i < ck.length; i++ )
            {
                if ( ck[i].checked )
                {
                   ck2 += ck[i].value+',';
                }
            }
            var IDArray = ck2.substring(0,ck2.length-1);
            x = ck2.split(',');
            if(x.length-1<=0)
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一项删除！");
                return;
            }
            else
            {
                var Act='Del';
                var UrlParam = "Act="+Act+"&strID="+IDArray;
                    $.ajax({ 
                          type: "POST",
                          url: "../../../Handler/Office/HumanManager/InputPerformanceRoyalty.ashx?"+UrlParam,
                          dataType:'json',//返回json格式数据
                          cache:false,
                          beforeSend:function()
                          { 
                             AddPop();
                          }, 
                          //complete :function(){ //hidePopup();},
                          error: function() 
                          {
                            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
                            //popMsgObj.ShowMsg('请求发生错误');
                            
                          }, 
                          success:function(data) 
                          { 
                            
                            //popMsgObj.ShowMsg(data.info);
                           
                            if(data.sta>0)
                            {
                                TurnToPage(1);
                            }
                            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",data.info);
                          } 
                       });
             }
     }
   else
   {
      return false;
   }
}


/*---------------------------保存------------------------------------*/
function DoCopy()
{
    
      $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/HumanManager/InputPerformanceRoyalty.ashx?Act=Copy",
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                     AddPop();
                  }, 
                  complete :function(){ hidePopup();},
                  error: function() 
                  {
                    //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误");
                    popMsgObj.ShowMsg('请求发生错误');
                    
                  }, 
                  success:function(msg) 
                  { 
                    if(msg.sta==1) 
                    {
                        DoSearch();//刷新
                    }
                    popMsgObj.ShowMsg(msg.info);
                  } 
               });              
}


/*--------------------------------Start 全选---------------------------------------------*/
//全选
function SelectAllCk() {
    $.each($("#pageDataList1 :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}
/*--------------------------------End 全选---------------------------------------------*/

//验证业务量为有效数值
function CheckNum(EleID)
{
    if(!IsNumeric(document.getElementById(EleID).value,10,2))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","业务量输入有误，请输入有效的数值！");
        document.getElementById(EleID).value="0";
        document.getElementById(EleID).focus();
        return;
    }
    else
    {
       $("#"+EleID).val(NumRound(document.getElementById(EleID).value,2));
    }
}

/*--------------------------弹出人员选择层，把公用拖出来，跟frame框架名有冲突---------------------*/
function alertdiv1(ControlID)
{
      var Array = ControlID.split(",");
      if(Array[0].indexOf("Dept") >= 0)
      {
          if(Array.length==2)
          {
             var url="../../../Pages/Common/SelectUserOrDept.aspx?ShowType=1&OprtType=1";
             var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
             if(returnValue!="" &&  returnValue!=null && returnValue!="ClearInfo")
             {
             var splitInfo=returnValue.split("|");
              window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value =splitInfo[0].toString();
              window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value =splitInfo[1].toString();
             }
          else if(returnValue=="ClearInfo")
          {
             window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value="";
             window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value=""; 
          } 
           }
          else
          {
             var url="../../../Pages/Common/SelectUserOrDept.aspx?ShowType=1&OprtType=2";
             var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
             if(returnValue!="" &&  returnValue!=null &&  returnValue!="ClearInfo")
             {
                 var ID="";
                 var Name="";
                 var getinfo = returnValue.split(",");
                  for(var i=0; i < getinfo.length; i++)
                  {
                      var c = getinfo[i].toString();
                      ID+=c.substring(0,c.indexOf("|"))+",";
                      Name+=c.substring(c.indexOf("|")+1,c.length)+",";
                  }
              ID = ID.substring(0, ID.length-1);
              Name=Name.substring(0,Name.length-1);
           //  window.parent.document.getElementById(Array[1]).value =ID;
             if(window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value!="")
             {
                var Oldvalue=window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value;
                var Newvalue=Name;
                var Tempvalue="";
                if(Newvalue.indexOf(Oldvalue)>=0)
                {
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+",";
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
   
                       window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value+=  window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value =","+Tempvalue;
                    }
                }
                else 
                {
                    Oldvalue=Oldvalue.replace(/,/g,"");
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+","; 
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                   if(Tempvalue.length>0)
                    {
                      window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value+=  window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value =","+Tempvalue;
                    }
                }
               // window.parent.document.getElementById(Array[0]).value+=window.parent.document.getElementById(Array[0]).value =","+Name;
             }
             else
             {
                   window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value =Name;   
             }
             if(window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value!="")
             {
                  // window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value+=  window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value =","+ID;
                var Oldvalue=window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value;
                var Newvalue=ID;
                var Tempvalue="";
                if(Newvalue.indexOf(Oldvalue)>=0)
                {
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+",";
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                       window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value+=  window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value =","+Tempvalue;
                    }
                }
                else
                {
                    Oldvalue=Oldvalue.replace(/,/g,"");
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+","; 
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                       window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value+=  window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value =","+Tempvalue;
                    }
                }
             }
             else
             {
                 window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value =ID;  
             } 
          }
          else if(returnValue=="ClearInfo")
          {
              window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value="";
              window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value=""; 
          } 
        }   
      }
        if(Array[0].indexOf("User") >= 0) 
         {
          
                if(Array.length==2)
                {
                    var url="../../../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=1";
                    var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
                    if(returnValue!="" &&  returnValue!=null  &&  returnValue!="ClearInfo")
                    {
                        var splitInfo=returnValue.split("|");
                        
     
                         window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value=splitInfo[0].toString();
                         window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value=splitInfo[1].toString();
                       // window.parent.document.getElementById(Array[1]).value =
                        //window.parent.document.getElementById(Array[0]).value =splitInfo[1].toString();
                    }
                   else  if(returnValue=="ClearInfo")
                   {
                         window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value="";
                         window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value="";
                       //window.parent.document.getElementById(Array[0]).value="";
                       //window.parent.document.getElementById(Array[1]).value=""; 
                   } 
                }
          else
          {
             var url="../../../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=2";
             var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
             if(returnValue!="" &&  returnValue!=null  &&  returnValue!="ClearInfo")
             {
                 var ID="";
                 var Name="";
                 var getinfo = returnValue.split(",");
                  for(var i=0; i < getinfo.length; i++)
                  {
                      var c = getinfo[i].toString();
                      ID+=c.substring(0,c.indexOf("|"))+",";
                      Name+=c.substring(c.indexOf("|")+1,c.length)+",";
                  }
              ID = ID.substring(0, ID.length-1);
              Name=Name.substring(0,Name.length-1);
             //window.parent.document.getElementById(Array[1]).value =ID; 
             if(window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value!="")
             {
                var Oldvalue=window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value;
                var Newvalue=Name;
                var Tempvalue="";
                if(Newvalue.indexOf(Oldvalue)>=0)
                {                  
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+",";
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                       window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value+=  window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value =","+Tempvalue;
                    }
                }
                else
                {
                     
                    Oldvalue=Oldvalue.replace(/,/g,"");
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+","; 
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                      window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value+=  window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value =","+Tempvalue;
                    }
                }
             }
             else
             {
                 window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value =Name;   
             }
             if(window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value!="")
             {
                var Oldvalue=window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value;
                var Newvalue=ID;
                var Tempvalue="";
                if(Newvalue.indexOf(Oldvalue)>=0)
                {
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+",";
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                       window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value+=  window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value =","+Tempvalue;
                    }
                }
                else
                {
                    Oldvalue=Oldvalue.replace(/,/g,"");
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+","; 
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                      window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value+=  window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value =","+Tempvalue;
                    }
                }
             }
             else
             {
                 window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value =ID;   
             }
           }
           else if(returnValue=="ClearInfo")
           {
               window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value="";
               window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value=""; 
           } 
         }        
       }
         
}