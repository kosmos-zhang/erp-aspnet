<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SellModuleSelectEmployeeUC.ascx.cs" Inherits="UserControl_SellModuleSelectEmployeeUC" %>
<div id="sellModuleEmployee">
    <!--提示信息弹出详情start-->
    <a name="pageEmpDataList1Mark"></a>
    <div id="divSellModuleEmpSelect" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 700px; z-index: 1001; position: absolute; display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px;">
        <table width="100%">
            <tr>
                <td>
                    <a onclick="closeSellModuEmpdiv()" style="text-align: right; cursor: pointer">关闭</a>
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="sellEmpList"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="SellEmpOrderBy('EmployeeNo','oSellEmp');return false;">
                            员工编号<span id="oSellEmp" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="SellEmpOrderBy('EmployeeName','oSEmp1');return false;">
                            员工名称<span id="oSEmp1" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="SellEmpOrderBy('DeptName','oSEmp2');return false;">
                            部门名称<span id="oSEmp2" class="orderTip"></span></div>
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
                                <div id="pageSellEmpcount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="sellEmpList_Pager" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divSellEmpPage">
                                    <span id="pageSellEmpList_Total"></span>每页显示
                                    <input name="text" type="text"  style=" width:30px;" id="ShowSellEmpPageCount" />
                                    条 转到第
                                    <input name="text" type="text" style=" width:30px;" id="ToSellEmpPage" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangeSellEmpPageCountIndex($('#ShowSellEmpPageCount').val(),$('#ToSellEmpPage').val());" />
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
var popSellEmpObj=new Object();
popSellEmpObj.InputObj1 = null;
popSellEmpObj.InputObj2 = null;

popSellEmpObj.ShowList=function(objInput1,objInput2)
{
    popSellEmpObj.InputObj1 = objInput1;
    popSellEmpObj.InputObj2 = objInput2;
    document.getElementById('divSellModuleEmpSelect').style.display='block';
    SellEmpTurnToPage(currentSellEmpPageIndex,objInput1,objInput2);
}
  
    var pageSellEmpCount = 10;//每页计数
    var totalSellEmpRecord = 0;
    var pagerSellEmpStyle = "flickr";//jPagerBar样式
    
    var currentSellEmpPageIndex = 1;
    var actionSellEmp = "";//操作
    var orderSellEmpBy = "";//排序字段
    //jQuery-ajax获取JSON数据
    function SellEmpTurnToPage(pageIndex,objInput1,objInput2)
    {
           currentSellEmpPageIndex = pageIndex;        
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/SellManager/SellModuleSelectEmployee.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageSellEmpCount="+pageSellEmpCount+"&orderby="+orderSellEmpBy,//数据
           beforeSend:function(){$("#sellEmpList_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#sellEmpList tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioEmp_\" id=\"radioEmp_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"fnSelectSellEmp("+item.ID+",'"+item.EmployeeName+"','"+item.DeptName+"','"+item.NowDeptID+"','"+objInput1+"','"+objInput2+"');\" />"+"</td>"+
                         "<td height='22' align='center'>"+ item.EmployeeNo +"</td>"+
                         "<td height='22' align='center'>"+ item.EmployeeName +"</td>"+
                         "<td height='22' align='center'>"+item.DeptName+"</td>").appendTo($("#sellEmpList tbody"));
                   });
                    //页码
                    ShowPageBar("sellEmpList_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerSellEmpStyle,mark:"pageEmpDataList1Mark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageSellEmpCount,
                    currentPageIndex:pageIndex,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"SellEmpTurnToPage({pageindex});return false;"}//[attr]
                    );
                  totalSellEmpRecord = msg.totalCount;
                  $("#ShowSellEmpPageCount").val(pageSellEmpCount);
                  ShowTotalPage(msg.totalCount,pageSellEmpCount,pageIndex,$("#pageSellEmpcount"));
                  $("#ToSellEmpPage").val(pageIndex);
                  },
           error: function() {}, 
           complete:function(){$("#sellEmpList_Pager").show();pageSellCustDataList1("sellEmpList","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    //table行颜色
function pageSellCustDataList1(o,a,b,c,d)
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
function ChangeSellEmpPageCountIndex(newPageCount,newPageIndex)
{
    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalSellEmpRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        this.pageSellEmpCount=parseInt(newPageCount);
        SellEmpTurnToPage(parseInt(newPageIndex));
    }
}
//排序
function SellEmpOrderBy(orderColum,orderTip)
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
    SellEmpTurnToPage(1);
}
   
function closeSellModuEmpdiv()
{
    document.getElementById("divSellModuleEmpSelect").style.display="none";
}

function fnSelectSellEmp(ID,EmployeeName,DeptName,NowDeptID,objInput1,objInput2)
{
    $("#"+objInput1).val(EmployeeName);
    $("#"+objInput1).attr("title",ID);
    $("#"+objInput2).val(DeptName);
    $("#"+objInput2).attr("title",NowDeptID);
    closeSellModuEmpdiv();
}
</script>
