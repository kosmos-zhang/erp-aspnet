<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StorageEmployeeInfo.ascx.cs"
    Inherits="UserControl_StorageEmployeeInfo" %>
<div id="layoutEmployee">
    <!--提示信息弹出详情start-->
    <a name="pageEmployeeDataListMark"></a>
    <div id="divQuaEmployeeInfo" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 700px; z-index: 1001; position: absolute; display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px;">
        <table width="100%">
            <tr>
                <td>
                    <img alt="关闭" id="btn_Close_a10" src="../../../images/Button/Bottom_btn_close.jpg"
                        style='cursor: pointer;' onclick='closeEmployeediv();' />
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListEmployee"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="EmployeeBy('EmployeeNo','OrderEmployee');return false;">
                            报检人员<span id="OrderEmployee" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="EmployeeBy('EmployeeName','oSEmp1');return false;">
                            性别<span id="OrderTipEmployee" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="EmployeeBy('DeptName','OrderEmployee2');return false;">
                            所在部门<span id="OrderEmployee2" class="orderTip"></span></div>
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
                                <div id="pageEmployeCount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataList_PagerEmployee" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divQualityCheckEmployeePage">
                                    <span id="pageDataList_TotalEmployee"></span>每页显示
                                    <input name="text" type="text" style="width: 30px;" id="ShowPageCountEmployee" />
                                    条 转到第
                                    <input name="text" type="text" style="width: 30px;" id="ToPageEmployee" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangeEmployeePageCountIndex($('#ShowPageCountEmployee').val(),$('#ToPageEmployee').val());" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
    <!--提示信息弹出详情end-->
</div>

<script type="text/javascript">


var popStorageQuaEmployeeObj = new Object();

popStorageQuaEmployeeObj.ShowQueEmployeeList=function(objEmpID1,objEmpID2)
{  
   
    popStorageQuaEmployeeObj.TurnToQuaEmployee(currentEmployeePageIndex,objEmpID1,objEmpID2);
    
}

    
    var pageEmployeeCount = 10;//每页计数
    var totaEmployeeRecord = 0;
    var pagerEmployeeStyle = "flickr";//jPagerBar样式
    
    var currentEmployeePageIndex = 1;
    var actionEmployee = "";//操作
    var orderEmployeeBy = "";//排序字段
    //jQuery-ajax获取JSON数据
    popStorageQuaEmployeeObj.TurnToQuaEmployee =function(pageIndex,objEmpID1,objEmpID2)
    {
            
           currentPageIndexEmployee = pageIndex;
           
            $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:'../../../Handler/Office/StorageManager/StorageEmployeeInfo.ashx',//目标地址
           cache:false,
           data: "Method="+0+"&currentPageIndexEmployee="+pageIndex+"&pageCountEmployee="+pageEmployeeCount+"&orderByEmployee="+orderEmployeeBy+"",//数据
           beforeSend:function(){$("#pageDataList_PagerEmployee").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListEmployee tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.EmployeeID != null && item.EmployeeID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioTech\" id=\"radioTech_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"FillStorageEmployee('"+item.EmployeeID+"','"+item.EmployeeName+"','"+item.DeptID+"','"+item.DeptName+"','"+objEmpID1+"','"+objEmpID2+"');\" />"+"</td>"+
                        "<td height='22' align='center'>" + item.EmployeeName + "</td>"+
                        "<td height='22' align='center'>" + item.Sex + "</td>"+
                        "<td height='22' align='center'>"+item.DeptName+"</td>").appendTo($("#pageDataListEmployee tbody"));
                   });
                 
                    //页码
                    ShowPageBar("pageDataList_PagerEmployee",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerEmployeeStyle,mark:"pageEmployeeDataListMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageEmployeCount:pageEmployeCount,
                    currentPageIndex:pageIndex,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"ProTurnToPage({pageindex});return false;"}//[attr]
                    );
                  totalSellEmpRecord = msg.totalCount;
                  $("#ShowPageCountEmployee").val(pageEmployeCount);
                  ShowTotalPage(msg.totalCount,pageEmployeCount,pageIndex,$("#pageEmployeCount"));
                  $("#ToPageEmployee").val(pageIndex);
                  },
           error: function() {}, 
           complete:function(){$("#pageDataList_PagerEmployee").show();pageEmployeeDataList("pageDataListEmployee","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
        
         document.getElementById('divQuaEmployeeInfo').style.display='block'; 
    }
  
     //table行颜色
function pageEmployeeDataList(o,a,b,c,d)
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
function ChangeEmployeePageCountIndex(newPageCount,newPageIndex)
{
    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalSellEmpRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        this.pageEmployeCount=parseInt(newPageCount);
        TurnToQuaEmployee(1);
    }
}
//排序
function EmployeeBy(orderColum,orderTip)
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
    orderSellEmpBy = orderColum+"_"+ordering;
    TurnToQuaEmployee(1);
}
   
function closeEmployeediv()
{
    document.getElementById("divQuaEmployeeInfo").style.display="none";
}

function FillStorageEmployee(a,b,c,d,e,f)
{

    $("#"+e).val(b);
    $("#"+e).attr("title",a);    
    $("#"+f).val(d);
    $("#"+f).attr("title",c);
     
    closeEmployeediv();
}




</script>

