 <%@ Control Language="C#" AutoEventWireup="true" CodeFile="SalaryPieceItem.ascx.cs" Inherits="UserControl_Human_SalaryPieceItem" %>
<div id="sellModuleEmployee">
    <!--提示信息弹出详情start-->
    <a name="pageEmpDataList1Mark"></a>
    <div id="divSellTemplateSelect" class="checktable"  style="border: solid 10px  #93BCDD; background: #ffffff;
        padding: 10px; width: 700px; z-index: 1001; position: absolute; display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px;">
        <table width="100%">
            <tr>
                <td>
                    <a onclick="closeTemplatediv()" style="text-align: right; cursor: pointer">关闭</a>
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="templateEmpList"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF" style="display:none ">
                        <div class="orderClick" onclick="templateEmpOrderBy('TemplateNo','otemplateEmp');return false;">
                            员工编号<span id="otemplateEmp" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="templateEmpOrderBy('Title','otemplateEmp1');return false;">
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
                                <div id="pageTemplateEmpcount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="templateEmpList_Pager" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divSellEmpPage">
                                    <span id="pagetemplateEmpList_Total"></span>每页显示
                                    <input name="text" type="text"  style=" width:30px;" id="ShowTemplateEmpPageCount" />
                                    条 转到第
                                    <input name="text" type="text" style=" width:30px;" id="ToSellEmpPage" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangeTemplateEmpPageCountIndex($('#ShowTemplateEmpPageCount').val(),$('#ToSellEmpPage').val());" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        
    </div>
    <input id="hidObject1" type="hidden" />
    <input id="hidObject2" type="hidden" />
    <!--提示信息弹出详情end-->
</div>

<script type="text/javascript">
var popTemplateObj=new Object();
popTemplateObj.InputObj1 = null;
popTemplateObj.InputObj2 = null;

popTemplateObj.ShowList=function(objInput1,objInput2)
{
    popTemplateObj.InputObj1 = objInput1;
    popTemplateObj.InputObj2 = objInput2;
document .getElementById ("hidObject1").value=objInput1;
      document .getElementById ("hidObject2").value=objInput2;
    document.getElementById('divSellTemplateSelect').style.display='block';
    SellTemplateTurnToPage(currentTemplateEmpPageIndex,objInput1,objInput2);
}
  
    var pageTemplateEmpcount = 10;//每页计数
    var totalTemplateEmpRecord = 0;
    var pagerTemplateEmpStyle = "flickr";//jPagerBar样式
    
    var currentTemplateEmpPageIndex = 1;
    var actiontemplateEmp = "";//操作
    var orderTemplateEmpBy ="";//排序字段
    //jQuery-ajax获取JSON数据
    function SellTemplateTurnToPage(pageIndex,objInput1,objInput2)
    {
           currentTemplateEmpPageIndex = pageIndex;   
            var postParam ="pageIndex="+pageIndex+"&PageCount="+pageTemplateEmpcount+"&Action=SearchPieceItemInfo&OrderBy="+orderTemplateEmpBy ; 
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/HumanManager/InputFloatSalary.ashx?'+postParam,//目标地址
           cache:false,
           beforeSend:function(){$("#templateEmpList_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#templateEmpList tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.TemplateNo != null && item.TemplateNo != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioEmp_\" id=\"radioEmp_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"fnSelectTemplateEmp('"+item.TemplateNo+"','"+item.Title+"','"+objInput1+"','"+objInput2+"');\" />"+"</td>"+
                         "<td height='22' align='center' style='display:none'>"+ item.TemplateNo +"</td>"+
                         "<td height='22' align='center'>"+ item.Title +"</td>").appendTo($("#templateEmpList tbody"));
                   });
                    //页码
                    ShowPageBar("templateEmpList_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerTemplateEmpStyle,mark:"pageEmpDataList1Mark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageTemplateEmpcount,
                    currentPageIndex:pageIndex,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"SellTemplateTurnToPage({pageindex},'"+objInput1+"','"+objInput2+"') ;return false;"}//[attr]
                    );
                  totalTemplateEmpRecord = msg.totalCount;
                  $("#ShowTemplateEmpPageCount").val(pageTemplateEmpcount);
                  ShowTotalPage(msg.totalCount,pageTemplateEmpcount,pageIndex,$("#pageTemplateEmpcount"));
                  $("#ToSellEmpPage").val(pageIndex);
                  },
           error: function() {}, 
           complete:function(){$("#templateEmpList_Pager").show();pageTemplateCustDataList1("templateEmpList","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    //table行颜色
function pageTemplateCustDataList1(o,a,b,c,d)
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
function ChangeTemplateEmpPageCountIndex(newPageCount,newPageIndex)
{
    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalTemplateEmpRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        this.pageTemplateEmpcount=parseInt(newPageCount);
          SellTemplateTurnToPage(parseInt(newPageIndex),  document .getElementById ("hidObject1").value.Trim()  ,document .getElementById ("hidObject2").value.Trim() );
    }
}
//排序
function templateEmpOrderBy(orderColum,orderTip)
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
    orderTemplateEmpBy = orderColum+"_"+ordering;

      SellTemplateTurnToPage(1,  document .getElementById ("hidObject1").value.Trim()  ,document .getElementById ("hidObject2").value.Trim() );
}
   
function closeTemplatediv()
{
    document.getElementById("divSellTemplateSelect").style.display="none";
}

function fnSelectTemplateEmp(TemplateNo,TemplateName,objInput1,objInput2)
{

    $("#"+objInput1).val(TemplateName);
    $("#"+objInput1).attr("title",TemplateNo);
    $("#"+objInput2).val(TemplateName);
    $("#"+objInput2).attr("title",TemplateNo);
    closeTemplatediv();
}
</script>