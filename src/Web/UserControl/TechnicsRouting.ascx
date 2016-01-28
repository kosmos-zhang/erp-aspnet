<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TechnicsRouting.ascx.cs"
    Inherits="UserControl_TechnicsRouting" %>
<div id="divPopRouteShadow" style="display: none">
    <iframe id="PopRouteShadowIframe" frameborder="0" width="100%"></iframe>
</div>
<div id="layout">
    <!--提示信息弹出详情start-->
    <div id="divTechnicsRouting" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 600px; z-index: 1000; position: absolute; display: none;
        top: 20%; left: 70%; margin: 5px 0 0 -400px;">
        <table width="99%">
            <tr>
                <td>
                <img id="btnClear" alt="清空" src="../../../images/Button/Bottom_btn_del.jpg" style='cursor: hand;' onclick="ClearRouteControl();"  />
                 <img id="btnClose" alt="关闭" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: hand;' onclick="document.getElementById('divTechnicsRouting').style.display='none';closeRotoscopingDiv(false,'divPopRouteShadow');"  />
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListRoute"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            工艺路线代码<span id="oID" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            工艺路线名称<span id="oC1" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            拼音缩写<span id="oC2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            是否主打工艺<span id="oC3" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            使用状态<span id="oC4" class="orderTip"></span></div>
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
                            <td height="28" background="../../../images/Main/PageList_bg.jpg" width="30%">
                                <div id="pagecountRoute">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataListRoute_Pager" class="jPagerBarRoute">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divpageRoute">
                                    <input name="text" type="text" id="TextRoute" style="display: none" />
                                    <span id="pageDataListRoute_Total"></span>每页显示
                                    <input name="text" type="text" id="ShowPageCountRoute" style="width: 25px" />
                                    条 转到第
                                    <input name="text" type="text" id="ToPageRoute" style="width: 25px" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCountRoute').val(),$('#ToPageRoute').val());" />
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

<script language="javascript">
var popRouteObj=new Object();
popRouteObj.InputObj = null;

popRouteObj.ShowList=function(objInput)
{
    popRouteObj.InputObj = objInput;
    openRotoscopingDiv(false,'divPopRouteShadow','PopRouteShadowIframe');
    document.getElementById('divTechnicsRouting').style.display='block';
    TurnToPageRoute(1);
}

   
    var pageCountRoute = 10;//每页计数
    var totalRecordRoute = 0;
    var pagerStyleRoute = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var actionRoute = "";//操作
    var orderByRoute = "";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPageRoute(pageIndexRoute)
    {
           currentPageIndex = pageIndexRoute;
           var RouteNo= "";
           var RouteName="";
           var PYShort="";
           var IsMainTech = "";
           var UsedStatus = "1";
           
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/ProductionManager/TechnicsRoutingList.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndexRoute+"&pageCount="+pageCountRoute+"&action="+actionRoute+"&orderby="+orderByRoute+"&RouteNo="+escape(RouteNo)+"&RouteName="+escape(RouteName)+"&PYShort="+escape(PYShort)+"&IsMainTech="+escape(IsMainTech)+"&UsedStatus="+UsedStatus+"",//数据
           beforeSend:function(){AddPopRoute();$("#pageDataListRoute_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListRoute tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){                        
                        if(item.ID != null && item.ID != "")
                        {
                            var tempMain = null;
                            var tempUsedStatus = null;
                            if(item.IsMainTech==1){tempMain='是';}else{tempMain='否';}
                            if(item.UsedStatus==1){tempUsedStatus='启用';}else{tempUsedStatus='停用';}
                            
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioTech\" id=\"radioTech_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"Fun_FillParent_RouteContent("+item.ID+",'"+item.RouteName+"');\" />"+"</td>"+
                            "<td height='22' align='center'>" + item.RouteNo + "</td>"+
                            "<td height='22' align='center'>" + item.RouteName +"</td>"+
                            "<td height='22' align='center'>"+item.PYShort+"</td>"+
                            "<td height='22' align='center'>"+tempMain+"</td>"+
                            "<td height='22' align='center'>"+tempUsedStatus+"</td>").appendTo($("#pageDataListRoute tbody"));
                        }
                   });

                     //页码
                      ShowPageBar("pageDataListRoute_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyleRoute,mark:"pageprodDataListMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageCountRoute,
                    currentPageIndex:pageIndexRoute,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"TurnToPageRoute({pageindex});return false;"}//[attr]
                    );
                      totalRecord = msg.totalCount;
                     // $("#pageDataList1_TotalProduct").html(msg.totalCount);//记录总条数
                      document.getElementById("TextRoute").value=msg.totalCount;
                      $("#ShowPageCountRoute").val(pageCountRoute);
                      ShowTotalPage(msg.totalCount,pageCountRoute,pageIndexRoute);
                      $("#ToPageRoute").val(pageIndexRoute);
                      ShowTotalPage(msg.totalCount,pageCountRoute,currentPageIndex,$("#pagecountRoute"));
                      },
           error: function() {}, 
           complete:function(){hidePopupRoute();$("#pageDataListRoute_Pager").show();IfshowRoute(document.getElementById("TextRoute").value);pageDataListRoute("pageDataListRoute","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    //table行颜色
function pageDataListRoute(o,a,b,c,d){
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

function IfshowRoute(count)
    {
        if(count=="0")
        {
            document.getElementById("divpageRoute").style.display = "none";
            document.getElementById("pagecountRoute").style.display = "none";
        }
        else
        {
            document.getElementById("divpageRoute").style.display = "block";
            document.getElementById("pagecountRoute").style.display = "block";
        }
    }
    
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndexRoute(newPageCount,newPageIndex)
    {
        var fieldText = "";
        var msgText = "";
        var isFlag = true;
        
        if(!IsNumber(newPageIndex) || newPageIndex==0)
        {
            isFlag = false;
            fieldText = fieldText + "跳转页面|";
	        msgText = msgText +  "必须为正整数格式|";
        }
        if(!IsNumber(newPageCount) || newPageCount==0)
        {
            isFlag = false;
            fieldText = fieldText + "每页显示|";
	        msgText = msgText +  "必须为正整数格式|";
        }
        if(!isFlag)
        {
            popMsgObj.Show(fieldText,msgText);
        }
        else
        {
            if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
            {
                popMsgObj.Show("转到页数|","超出查询范围|");
                return false;
            }
            else
            {
                this.pageCountRoute=parseInt(newPageCount);
                TurnToPageRoute(parseInt(newPageIndex));
            }
        }

    }
    //排序
    function OrderByRoute(orderColum,orderTip)
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
   
function AddPopRoute()
{
    //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/extanim64.gif","数据处理中，请稍候……");
}
 
function hidePopupRoute()
{
    //document.all.Forms.style.display = "none";
}

</script>

