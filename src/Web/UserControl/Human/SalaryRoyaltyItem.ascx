<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SalaryRoyaltyItem.ascx.cs" Inherits="UserControl_Human_SalaryRoyaltyItem" %>
<div id="sellModuleEmployee">
    <!--提示信息弹出详情start-->
    <a name="pageRoyaltyDataList1Mark"></a>
    <div id="divSellRoyaltySelect" class="checktable"  style="border: solid 10px  #93BCDD; background: #ffffff;
        padding: 10px; width: 700px; z-index: 1001; position: absolute; display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px;">
        <table width="100%">
            <tr>
                <td>
                    <a onclick="closeRoyaltydiv()" style="text-align: right; cursor: pointer">关闭</a>
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="RoyaltyEmpList"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" style="display:none ">
                        <div class="orderClick" onclick="RoyaltyEmpOrderBy('TemplateNo','otemplateEmp');return false;">
                            员工编号<span id="otemplateEmp" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="RoyaltyEmpOrderBy('Title','otemplateEmp1');return false;">
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
                                <div id="pageRoyaltyEmpcount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="RoyaltyEmpList_Pager" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divSellEmpPage">
                                    <span id="pagetemplateEmpList_Total"></span>每页显示
                                    <input name="text" type="text"  style=" width:30px;" id="ShowRoyaltyEmpPageCount" />
                                    条 转到第
                                    <input name="text" type="text" style=" width:30px;" id="ToSellRoyaltyPage" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangeRoyaltyEmpPageCountIndex($('#ShowRoyaltyEmpPageCount').val(),$('#ToSellRoyaltyPage').val());" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        
    </div>
    <input id="hidRoyaltyObject1" type="hidden" />
    <input id="hidRoyaltyObject2" type="hidden" />
    <!--提示信息弹出详情end-->
</div>

<script type="text/javascript">
var popRoyaltyObj=new Object();
popRoyaltyObj.InputObj1 = null;
popRoyaltyObj.InputObj2 = null;

popRoyaltyObj.ShowList=function(objInput1,objInput2)
{
    popRoyaltyObj.InputObj1 = objInput1;
    popRoyaltyObj.InputObj2 = objInput2;
document .getElementById ("hidRoyaltyObject1").value=objInput1;
      document .getElementById ("hidRoyaltyObject2").value=objInput2;
    document.getElementById('divSellRoyaltySelect').style.display='block';
    SellRoyaltyTurnToPage(currentRoyaltyEmpPageIndex,objInput1,objInput2);
}
  
    var pageRoyaltyEmpcount = 10;//每页计数
    var totalRoyaltyEmpRecord = 0;
    var pagerRoyaltyEmpStyle = "flickr";//jPagerBar样式
    
    var currentRoyaltyEmpPageIndex = 1;
    var actionRoyaltyEmp = "";//操作
    var orderRoyaltyEmpBy ="";//排序字段
    //jQuery-ajax获取JSON数据
    function SellRoyaltyTurnToPage(pageIndex,objInput1,objInput2)
    {
           currentRoyaltyEmpPageIndex = pageIndex;   
            var postParam ="pageIndex="+pageIndex+"&PageCount="+pageRoyaltyEmpcount+"&Action=SearchRoyaltyItemInfo&OrderBy="+orderRoyaltyEmpBy ; 
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/HumanManager/InputFloatSalary.ashx?'+postParam,//目标地址
           cache:false,
           beforeSend:function(){$("#RoyaltyEmpList_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#RoyaltyEmpList tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.TemplateNo != null && item.TemplateNo != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioEmp_\" id=\"radioEmp_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"fnSelectRoyaltyEmp('"+item.TemplateNo+"','"+item.Title+"','"+objInput1+"','"+objInput2+"');\" />"+"</td>"+
                         "<td height='22' align='center' style='display:none'>"+ item.TemplateNo +"</td>"+
                         "<td height='22' align='center'>"+ item.Title +"</td>").appendTo($("#RoyaltyEmpList tbody"));
                   });
                    //页码
                    ShowPageBar("RoyaltyEmpList_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerRoyaltyEmpStyle,mark:"pageRoyaltyDataList1Mark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageRoyaltyEmpcount,
                    currentPageIndex:pageIndex,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"SellRoyaltyTurnToPage({pageindex},'"+objInput1+"','"+objInput2+"') ;return false;"}//[attr]
                    );
                  totalRoyaltyEmpRecord = msg.totalCount;
                  $("#ShowRoyaltyEmpPageCount").val(pageRoyaltyEmpcount);
                  ShowTotalPage(msg.totalCount,pageRoyaltyEmpcount,pageIndex,$("#pageRoyaltyEmpcount"));
                  $("#ToSellRoyaltyPage").val(pageIndex);
                  },
           error: function() {}, 
           complete:function(){$("#RoyaltyEmpList_Pager").show();pageRoyaltyCustDataList1("RoyaltyEmpList","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    //table行颜色
function pageRoyaltyCustDataList1(o,a,b,c,d)
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
function ChangeRoyaltyEmpPageCountIndex(newPageCount,newPageIndex)
{
    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRoyaltyEmpRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        this.pageRoyaltyEmpcount=parseInt(newPageCount);
          SellRoyaltyTurnToPage(parseInt(newPageIndex),  document .getElementById ("hidRoyaltyObject1").value.Trim()  ,document .getElementById ("hidRoyaltyObject2").value.Trim() );
    }
}
//排序
function RoyaltyEmpOrderBy(orderColum,orderTip)
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
    orderRoyaltyEmpBy = orderColum+"_"+ordering;

      SellRoyaltyTurnToPage(1,  document .getElementById ("hidRoyaltyObject1").value.Trim()  ,document .getElementById ("hidRoyaltyObject2").value.Trim() );
}
   
function closeRoyaltydiv()
{
    document.getElementById("divSellRoyaltySelect").style.display="none";
}

function fnSelectRoyaltyEmp(TemplateNo,TemplateName,objInput1,objInput2)
{
    $("#"+objInput1).val(TemplateName);
    $("#"+objInput1).attr("title",TemplateNo);
    $("#"+objInput2).val(TemplateName);
    $("#"+objInput2).attr("title",TemplateNo);
    closeRoyaltydiv();
}
</script>