<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SalaryTimeItem.ascx.cs" Inherits="UserControl_Human_SalaryTimeItem" %>
<div id="sellModuleEmployee">
    <!--提示信息弹出详情start-->
    <a name="pageTimeDataList1Mark"></a>
    <div id="divSellTimeSelect" class="checktable"  style="border: solid 10px  #93BCDD; background: #ffffff;
        padding: 10px; width: 700px; z-index: 1001; position: absolute; display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px;">
        <table width="100%">
            <tr>
                <td>
                    <a onclick="closeTimediv()" style="text-align: right; cursor: pointer">关闭</a>
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="timeEmpList"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" style="display:none ">
                        <div class="orderClick" onclick="timeEmpOrderBy('TemplateNo','otemplateEmp');return false;">
                            员工编号<span id="otemplateEmp" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="timeEmpOrderBy('Title','otemplateEmp1');return false;">
                            模板名称<span id="otemplateEmp1" class="orderTip"></span></div>
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
                            <td height="28" background="../../../images/Main/PageList_bg.jpg" width="28%">
                                <div id="pageTimeEmpcount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="timeEmpList_Pager" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divSellEmpPage">
                                    <span id="pagetemplateEmpList_Total"></span>每页显示
                                    <input name="text" type="text"  style=" width:30px;" id="ShowTimeEmpPageCount" />
                                    条 转到第
                                    <input name="text" type="text" style=" width:30px;" id="ToSellTimePage" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangeTimeEmpPageCountIndex($('#ShowTimeEmpPageCount').val(),$('#ToSellTimePage').val());" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        
    </div>
    <input id="hidTimeObject1" type="hidden" />
    <input id="hidTimeObject2" type="hidden" />
    <!--提示信息弹出详情end-->
</div>

<script type="text/javascript">
var popTimeObj=new Object();
popTimeObj.InputObj1 = null;
popTimeObj.InputObj2 = null;

popTimeObj.ShowList=function(objInput1,objInput2)
{
    popTimeObj.InputObj1 = objInput1;
    popTimeObj.InputObj2 = objInput2;
document .getElementById ("hidTimeObject1").value=objInput1;
      document .getElementById ("hidTimeObject2").value=objInput2;
    document.getElementById('divSellTimeSelect').style.display='block';
    SellTimeTurnToPage(currentTimeEmpPageIndex,objInput1,objInput2);
}
  
    var pageTimeEmpcount = 10;//每页计数
    var totalTimeEmpRecord = 0;
    var pagerTimeEmpStyle = "flickr";//jPagerBar样式
    
    var currentTimeEmpPageIndex = 1;
    var actiontimeEmp = "";//操作
    var orderTimeEmpBy ="";//排序字段
    //jQuery-ajax获取JSON数据
    function SellTimeTurnToPage(pageIndex,objInput1,objInput2)
    {
           currentTimeEmpPageIndex = pageIndex;   
            var postParam ="pageIndex="+pageIndex+"&PageCount="+pageTimeEmpcount+"&Action=SearchTimeItemInfo&OrderBy="+orderTimeEmpBy ; 
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/HumanManager/InputFloatSalary.ashx?'+postParam,//目标地址
           cache:false,
           beforeSend:function(){$("#timeEmpList_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#timeEmpList tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.TemplateNo != null && item.TemplateNo != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioEmp_\" id=\"radioEmp_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"fnSelectTimeEmp('"+item.TemplateNo+"','"+item.Title+"','"+objInput1+"','"+objInput2+"');\" />"+"</td>"+
                         "<td height='22' align='center' style='display:none'>"+ item.TemplateNo +"</td>"+
                         "<td height='22' align='center'>"+ item.Title +"</td>").appendTo($("#timeEmpList tbody"));
                   });
                    //页码
                    ShowPageBar("timeEmpList_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerTimeEmpStyle,mark:"pageTimeDataList1Mark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageTimeEmpcount,
                    currentPageIndex:pageIndex,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"SellTimeTurnToPage({pageindex},'"+objInput1+"','"+objInput2+"') ;return false;"}//[attr]
                    );
                  totalTimeEmpRecord = msg.totalCount;
                  $("#ShowTimeEmpPageCount").val(pageTimeEmpcount);
                  ShowTotalPage(msg.totalCount,pageTimeEmpcount,pageIndex,$("#pageTimeEmpcount"));
                  $("#ToSellTimePage").val(pageIndex);
                  },
           error: function() {}, 
           complete:function(){$("#timeEmpList_Pager").show();pageTimeCustDataList1("timeEmpList","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    //table行颜色
function pageTimeCustDataList1(o,a,b,c,d)
{
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


//改变每页记录数及跳至页数
function ChangeTimeEmpPageCountIndex(newPageCount,newPageIndex)
{
    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalTimeEmpRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        this.pageTimeEmpcount=parseInt(newPageCount);
          SellTimeTurnToPage(parseInt(newPageIndex),  document .getElementById ("hidTimeObject1").value.Trim()  ,document .getElementById ("hidTimeObject2").value.Trim() );
    }
}
//排序
function timeEmpOrderBy(orderColum,orderTip)
{
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
    orderTimeEmpBy = orderColum+"_"+ordering;

      SellTimeTurnToPage(1,  document .getElementById ("hidTimeObject1").value.Trim()  ,document .getElementById ("hidTimeObject2").value.Trim() );
}
   
function closeTimediv()
{
    document.getElementById("divSellTimeSelect").style.display="none";
}

function fnSelectTimeEmp(TemplateNo,TemplateName,objInput1,objInput2)
{
    $("#"+objInput1).val(TemplateName);
    $("#"+objInput1).attr("title",TemplateNo);
    $("#"+objInput2).val(TemplateName);
    $("#"+objInput2).attr("title",TemplateNo);
    closeTimediv();
}
</script>