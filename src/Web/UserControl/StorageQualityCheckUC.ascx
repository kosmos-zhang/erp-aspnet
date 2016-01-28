<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StorageQualityCheckUC.ascx.cs" Inherits="UserControl_StorageQualityCheckUC" %>
<div id="layout">
    <!--提示信息弹出详情start-->
    <a name="pageEmpDataList1Mark"></a>
    <div id="divProInfo" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 700px; z-index: 1001; position: absolute; display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px;">
        <table width="100%">
            <tr>
                <td>
                    <a onclick="closeProdiv()" style="text-align: right; cursor: pointer">关闭</a>
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1Pro"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" >
                            物品编号<span id="oSellEmp" class="orderTip"></span></div>
                    </th>
                    
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" >
                            物品名称<span id="oSEmp2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" >
                            单位<span id="Span2" class="orderTip"></span></div>
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
                                <div id="pagecount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataList1_PagerPro" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divQualityCheckPage">
                                    <span id="pageDataList1_TotalProvider"></span>每页显示
                                    <input name="text" type="text"  style=" width:30px;" id="ShowPageCountCust" />
                                    条 转到第
                                    <input name="text" type="text" style=" width:30px;" id="ToPageCust" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangeProPageCountIndex($('#ShowPageCountCust').val(),$('#ToPageCust').val());" />
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


var popQualityPro1Obj = new Object();

popQualityPro1Obj.ShowProList=function(objProviderID1,objProviderID2,objProviderID3)
{  
    popQualityPro1Obj.TurnToPagePro(currentProPageIndex,objProviderID1,objProviderID2,objProviderID3);
    openRotoscopingDiv(false,'divBackShadow','BackShadowIframe');
}

    
    var pageProCount = 10;//每页计数
    var totaProRecord = 0;
    var pagerProStyle = "flickr";//jPagerBar样式
    
    var currentProPageIndex = 1;
    var actionPro = "";//操作
    var orderProBy = "";//排序字段
    //jQuery-ajax获取JSON数据
    popQualityPro1Obj.TurnToPagePro =function(pageIndex,objProviderID1,objProviderID2,objProviderID3)
    {
            
           currentPageIndexPro = pageIndex;
           
           var ProviderID= "";
           var ProviderNo="";
           var ProviderName="";
            $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:'../../../Handler/Office/StorageManager/StorageQualityCheck.ashx',//目标地址
           cache:false,
           data: "Method="+0+"&currentPageIndexPro="+pageIndex+"&pageCountPro="+pageProCount+"&orderByPro="+orderProBy+"",//数据
           beforeSend:function(){$("#pageDataList1_PagerPro").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1Pro tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ProID != null && item.ProID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioTech\" id=\"radioTech_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"FillProQua('"+item.ProID+"','"+item.ProName+"','"+item.UnitID+"','"+item.UnitName+"','"+item.ProNo+"','"+objProviderID1+"','"+objProviderID2+"','"+objProviderID3+"');\" />"+"</td>"+
                        "<td height='22' align='center'>" + item.ProNo + "</td>"+
                        "<td height='22' align='center'>" + item.ProName + "</td>"+
                        "<td height='22' align='center'>"+item.UnitName+"</td>").appendTo($("#pageDataList1Pro tbody"));
                   });
                 
                    //页码
                    ShowPageBar("pageDataList1_PagerPro",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerProStyle,mark:"pageEmpDataList1Mark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pagecount,
                    currentPageIndex:pageIndex,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"ProTurnToPage({pageindex});return false;"}//[attr]
                    );
                  totalSellEmpRecord = msg.totalCount;
                  $("#ShowPageCountCust").val(pagecount);
                  ShowTotalPage(msg.totalCount,pagecount,pageIndex,$("#pagecount"));
                  $("#ToPageCust").val(pageIndex);
                  },
           error: function() {}, 
           complete:function(){$("#pageDataList1_PagerPro").show();pageSellCustDataList1("pageDataList1Pro","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
        
         document.getElementById('divProInfo').style.display='block'; 
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
function ChangeProPageCountIndex(newPageCount,newPageIndex)
{
    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalSellEmpRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        this.pagecount=parseInt(newPageCount);
        TurnToPagePro(1);
    }
}
//排序
function ProOrderBy(orderColum,orderTip)
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
    TurnToPagePro(1);
}
   
function closeProdiv()
{
    document.getElementById("divProInfo").style.display="none";
    closeRotoscopingDiv(false,'divBackShadow');
}

function FillProQua(ProID,ProName,UnitID,UnitName,ProDNO,objProviderID1,objProviderID2,objProviderID3)
{

    $("#"+objProviderID1).val(ProName);
    $("#"+objProviderID1).attr("title",ProID);    
    $("#"+objProviderID2).val(ProDNO);
    $("#"+objProviderID3).val(UnitName);
    $("#"+objProviderID3).attr("title",UnitID);
    document.getElementById(objProviderID1).readOnly=true;   
    document.getElementById(objProviderID2).readOnly=true;   
    document.getElementById(objProviderID3).readOnly=true;       
    closeProdiv();
}




</script>

