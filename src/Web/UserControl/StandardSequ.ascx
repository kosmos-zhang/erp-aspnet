<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StandardSequ.ascx.cs"
    Inherits="UserControl_StandardSequ" %>
<div id="divPopSequShadow" style="display: none">
    <iframe id="PopSequShadowIframe" frameborder="0" width="100%"></iframe>
</div>
<div id="layout">

    <!--提示信息弹出详情start-->
    <div id="divStandardSequ" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 600px; height: 200px;overflow: scroll; z-index: 1000; position: absolute; display: none; top: 50%; left: 70%;
        margin: 5px 0 0 -400px;	scrollbar-face-color: #ffffff; scrollbar-highlight-color: #ffffff;scrollbar-shadow-color: COLOR:#000000; scrollbar-3dlight-color: #ffffff;scrollbar-darkshadow-color: #ffffff;  ">
        <table>
        <tr><td>
        <img id="btnClose" alt="关闭" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: hand;' onclick="document.getElementById('divStandardSequ').style.display='none';closeRotoscopingDiv(false,'divPopSequShadow');"  />
        </td></tr>
        </table>
        <div style="width: 920px">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListSequ"
                bgcolor="#999999">
                <tbody>
                    <tr>
                        <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            选择
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick">
                                工序代码<span id="oID" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick">
                                工序名称<span id="oC1" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick">
                                所属工作中心<span id="oC3" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick">
                                准备时间<span id="Span1" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick">
                                运行时间<span id="Span2" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick">
                                是否计费<span id="Span3" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick">
                                是否外协<span id="Span4" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick">
                                检验方式<span id="Span5" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick">
                                单位计时工资<span id="Span6" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick">
                                单位计件工资<span id="Span7" class="orderTip"></span></div>
                        </th>

                    </tr>
                </tbody>
            </table>
            <br />
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#FFFFFF"
                class="PageList">
                <tr>
                    <td height="28" background="../../../images/Main/PageList_bg.jpg">
                        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                            <tr>
                                <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                    <div id="pagecountSequ">
                                    </div>
                                </td>
                                <td height="28" align="right">
                                    <div id="pageDataListSequ_Pager" class="jPagerBar">
                                    </div>
                                </td>
                                <td height="28" align="right">
                                    <div id="divpageSequ">
                                        <input name="text" type="text" id="TextSequ" style="display: none" />
                                        <span id="pageDataListSequ_Total"></span>每页显示
                                        <input name="text" type="text" id="ShowPageCountSequ" size="5" />
                                        条 转到第
                                        <input name="text" type="text" id="ToPageSequ" size="5" />
                                        页
                                        <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                            width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexSequ($('#ShowPageCountSequ').val(),$('#ToPageSequ').val());" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </div>
    <!--提示信息弹出详情end-->
</div>

<script language="javascript">
var popSequObj=new Object();
popSequObj.RowObj = null;

popSequObj.ShowList=function(objRow)
{
    popSequObj.RowObj = objRow;
    openRotoscopingDiv(false,'divPopSequShadow','PopSequShadowIframe');
    document.getElementById('divStandardSequ').style.display='block';
    document.getElementById('divStandardSequ').style.position='absolute';
    TurnToPageSequ(1);
}
  
var pageCountSequ = 10;//每页计数
var totalRecordSequ = 0;
var pagerStyleSequ = "flickr";//jPagerBar样式

var currentPageIndex = 1;
var actionSequ = "";//操作
var orderBySequ  = "ModifiedDate_d";//排序字段
//jQuery-ajax获取JSON数据
function TurnToPageSequ(pageIndexSequ)
{
       currentPageIndex = pageIndexSequ;
       var SequNo= "";
       var SequName= "";
       var PYShort= "";
       var WCID = "0";
       var UsedStatus = "1";
       
       $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:  '../../../Handler/Office/ProductionManager/StandardSequList.ashx',//目标地址
       cache:false,
       data: "pageIndex="+pageIndexSequ+"&pageCount="+pageCountSequ+"&action="+actionSequ+"&orderby="+orderBySequ+"&SequNo="+escape(SequNo)+"&SequName="+escape(SequName)+"&PYShort="+escape(PYShort)+"&WCID="+escape(WCID)+"&UsedStatus="+UsedStatus+"",//数据
       beforeSend:function(){AddPop();$("#pageDataListSequ_Pager").hide();},//发送数据之前
       
       success: function(msg){
                //数据获取完毕，填充页面据显示
                //数据列表
                $("#pageDataListSequ tbody").find("tr.newrow").remove();
                $.each(msg.data,function(i,item){
                
                    var tempUsedStatus = null;
                    var tempCheckWay = null;
                    var tempIsCharge = null;
                    var tempIsoutsource = null;
                    if(item.UsedStatus==1){tempUsedStatus='启用';}else{tempMain='停用';}
                    
                    if(item.CheckWay==0)
                        tempCheckWay ='免检';
                    if(item.CheckWay==1)
                        tempCheckWay ='全检';
                    if(item.CheckWay==2)
                        tempCheckWay ='抽检';
                    if(item.CheckWay==3)
                        tempCheckWay ='不定期检验';
                        
                    if(item.IsCharge==1){tempIsCharge='是';}else{tempIsCharge='否';}
                    
                     if(item.Isoutsource==1){tempIsoutsource='是';}else{tempIsoutsource='否';}
                       

                    if(item.ID != null && item.ID != "")
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='CheckboxSequ' name='CheckboxSequ'  value="+item.ID+"  type='radio'  onclick=\"Fun_FillParent_SequContent('"+item.ID+"','"+item.SequName+"','"+item.WCName+"','"+item.WCID+"','"+item.TimeUnit+"','"+item.ReadyTime+"','"+item.RunTime+"','"+item.IsCharge+"','"+item.Isoutsource+"','"+item.CheckWay+"','"+item.TimeWage+"','"+item.PieceWage+"');\" />"+"</td>"+
                    "<td height='22' align='center'>"+ item.SequNo +"</td>"+
                    "<td height='22' align='center'>"+ item.SequName +"</td>"+
                    "<td height='22' align='center'>"+item.WCName+"</td>"+
                    "<td height='22' align='center'>"+item.ReadyTime+"</td>"+
                    "<td height='22' align='center'>"+item.RunTime+"</td>"+
                    "<td height='22' align='center'>"+tempIsCharge+"</td>"+
                    "<td height='22' align='center'>"+tempIsoutsource+"</td>"+
                     "<td height='22' align='center'>"+tempCheckWay+"</td>"+
                    "<td height='22' align='center'>"+item.TimeWage+"</td>"+
                    "<td height='22' align='center'>"+item.PieceWage+"</td>").appendTo($("#pageDataListSequ tbody"));
               });
                     //页码
                      ShowPageBar("pageDataListSequ_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyleSequ,mark:"pageSequDataListMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageCountSequ,
                    currentPageIndex:pageIndexSequ,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"TurnToPageSequ({pageindex});return false;"}//[attr]
                    );
                      totalRecord = msg.totalCount;
                     // $("#pageDataList1_TotalProduct").html(msg.totalCount);//记录总条数
                      document.getElementById("TextSequ").value=msg.totalCount;
                      $("#ShowPageCountSequ").val(pageCountSequ);
                      ShowTotalPage(msg.totalCount,pageCountSequ,pageIndexSequ);
                      $("#ToPageSequ").val(pageIndexSequ);
                          ShowTotalPage(msg.totalCount,pageCountSequ,currentPageIndex,$("#pagecountSequ"));
                      //document.getElementById('tdResult').style.display='block';
                      },
           error: function() {}, 
           complete:function(){hidePopup();$("#pageDataListSequ_Pager").show();IfshowSequ(document.getElementById('TextSequ').value);pageDataListSequ("pageDataListSequ","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
}
    //table行颜色
function pageDataListSequ(o,a,b,c,d){
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

function IfshowSequ(count)
    {
        if(count=="0")
        {
            document.getElementById('divpageSequ').style.display = "none";
            document.getElementById('divpageSequ').style.display = "none";
        }
        else
        {
            document.getElementById('divpageSequ').style.display = "block";
            document.getElementById('divpageSequ').style.display = "block";
        }
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndexSequ(newPageCount,newPageIndex)
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
                this.pageCountSequ=parseInt(newPageCount);
                TurnToPageSequ(parseInt(newPageIndex));
            }
        }

    }
    //排序
    function OrderBySequ(orderColum,orderTip)
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

</script>

