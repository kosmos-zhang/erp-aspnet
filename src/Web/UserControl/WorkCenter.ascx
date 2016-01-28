<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WorkCenter.ascx.cs" Inherits="UserControl_WorkCenter" %>
<div id="divPopWorkShadow" style="display: none">
    <iframe id="PopWorkShadowIframe" frameborder="0" width="100%"></iframe>
</div>
<div id="layoutWorkCenter">
    <!--提示信息弹出详情start-->
    <div id="divWorkCenter" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 600px; height: 200px; overflow: scroll; z-index: 1000; position: absolute;
        display: none; top: 50%; left: 70%; margin: 5px 0 0 -400px; scrollbar-face-color: #ffffff;
        scrollbar-highlight-color: #ffffff; scrollbar-shadow-color: COLOR:#000000; scrollbar-3dlight-color: #ffffff;
        scrollbar-darkshadow-color: #ffffff;">
        <table>
            <tr>
                <td>
                    <img id="btnClose" alt="关闭" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: hand;' onclick="document.getElementById('divWorkCenter').style.display='none';closeRotoscopingDiv(false,'divPopWorkShadow');"   />
                </td>
            </tr>
        </table>
        <div style="width: 920px">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListWorkCenter"
                bgcolor="#999999">
                <tbody>
                    <tr>
                        <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            选择
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="OrderBy('WCNo','oGroup');return false;">
                                工作中心代码<span id="oGroup" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="OrderBy('WCName','oC1');return false;">
                                工作中心名称<span id="oC1" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="OrderBy('PYShrot','oC2');return false;">
                                拼音缩写<span id="oC2" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="OrderBy('IsMain','oC3');return false;">
                                是否关键工作中心<span id="oC3" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="OrderBy('DeptID','oC4');return false;">
                                所属部门<span id="oC4" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="OrderBy('UsedState','oC5');return false;">
                                使用状态<span id="oC5" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="OrderBy('Remark','oC6');return false;">
                                备注<span id="oC6" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="OrderBy('Creator','oC7');return false;">
                                建档人<span id="oC7" class="orderTip"></span></div>
                        </th>
                        <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                            <div class="orderClick" onclick="OrderBy('CreateDate','oC8');return false;">
                                建档日期<span id="oC8" class="orderTip"></span></div>
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
                                    <div id="pagecountWorkCenter">
                                    </div>
                                </td>
                                <td height="28" align="right">
                                    <div id="pageDataListWorkCenter_Pager" class="jPagerBar">
                                    </div>
                                </td>
                                <td height="28" align="right">
                                    <div id="divpageWorkCenter">
                                        <input name="text" type="text" id="TextWorkCenter" style="display: none" />
                                        <span id="pageDataListWorkCenter_Total"></span>每页显示
                                        <input name="text" type="text" id="ShowPageCountWorkCenter" size="5" />
                                        条 转到第
                                        <input name="text" type="text" id="ToPageWorkCenter" size="5" />
                                        页
                                        <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                            width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexWorkCenter($('#ShowPageCountWorkCenter').val(),$('#ToPageWorkCenter').val());" />
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
var popWorkCenterObj=new Object();
popWorkCenterObj.RowObj = null;

popWorkCenterObj.ShowList=function(objRow)
{
    popWorkCenterObj.RowObj = objRow;
    openRotoscopingDiv(false,'divPopWorkShadow','PopWorkShadowIframe');
    document.getElementById('divWorkCenter').style.display='block';
    document.getElementById('divWorkCenter').style.position='absolute';
    TurnToPageWorkCenter(1);
}

 
    var pageCountWorkCenter = 10;//每页计数
    var totalRecordWorkCenter= 0;
    var pagerStyleWorkCenter = "flickr";//jPagerBar样式
    
    var currentPageIndexWorkCenter = 1;
    var actionWorkCenter = "";//操作
    var orderByWorkCenter = "ModifiedDate_d";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPageWorkCenter(pageIndexWorkCenter)
    {
           currentPageIndex = pageIndexWorkCenter;
           var WCNo= "";
           var WCName="";
           var PYShort="";
           var IsMain = "";
           var DeptID = "0";
           var UsedStatus = "1";//启用状态
           
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/ProductionManager/WorkCenterList.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndexWorkCenter+"&pageCount="+pageCountWorkCenter+"&action="+actionWorkCenter+"&orderby="+orderByWorkCenter+"&WCNo="+escape(WCNo)+"&WCName="+escape(WCName)+"&PYShort="+escape(PYShort)+"&IsMain="+escape(IsMain)+"&DeptID="+escape(DeptID)+"&UsedStatus="+UsedStatus+"",//数据
           beforeSend:function(){AddPop();$("#pageDataListWorkCenter_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListWorkCenter tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        var tempMain = null;
                        var tempUsedStatus = null;
                        if(item.IsMain==1){tempMain='是';}else{tempMain='否';}
                        if(item.UsedStatus==1){tempUsedStatus='启用';}else{tempUsedStatus='停用';}
                        
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='OptionCheck_"+item.ID+"' name='OptionCheck_"+item.ID+"'  value="+item.ID+" onclick=\"Fun_FillParent_WorkCenter('"+item.ID+"','"+item.WCName+"');\"  type='checkbox'/>"+"</td>"+
                        "<td height='22' align='center'>"+ item.WCNo +"</td>"+
                        "<td height='22' align='center'>"+ item.WCName +"</td>"+
                        "<td height='22' align='center'>"+item.PYShort+"</td>"+
                        "<td height='22' align='center'>"+tempMain+"</td>"+
                        "<td height='22' align='center'>"+item.DeptName+"</td>"+
                        "<td height='22' align='center'>"+tempUsedStatus+"</td>"+
                        "<td height='22' align='center'>"+item.Remark+"</td>"+
                        "<td height='22' align='center'>"+item.EmployeeName+"</td>"+
                        "<td height='22' align='center'>"+item.CreateDate.substring(0,10)+"</td>").appendTo($("#pageDataListWorkCenter tbody"));
                   });
                     //页码
                      ShowPageBar("pageDataListWorkCenter_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyleWorkCenter,mark:"pageWorkCenterDataListMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageCountWorkCenter,
                    currentPageIndex:pageIndexWorkCenter,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"TurnToPageWorkCenter({pageindex});return false;"}//[attr]
                    );
                      totalRecord = msg.totalCount;
                     // $("#pageDataList1_TotalProduct").html(msg.totalCount);//记录总条数
                      document.getElementById("TextWorkCenter").value=msg.totalCount;
                      $("#ShowPageCountWorkCenter").val(pageCountWorkCenter);
                      ShowTotalPage(msg.totalCount,pageCountWorkCenter,pageIndexWorkCenter);
                      $("#ToPageWorkCenter").val(pageIndexWorkCenter);
                          ShowTotalPage(msg.totalCount,pageCountWorkCenter,currentPageIndex,$("#pagecountWorkCenter"));
                      //document.getElementById('tdResult').style.display='block';
                      },
           error: function() {}, 
           complete:function(){hidePopup();$("#pageDataListWorkCenter_Pager").show();IfshowWorkCenter(document.getElementById('TextWorkCenter').value);pageDataListWorkCenter("pageDataListWorkCenter","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
}
    //table行颜色
function pageDataListWorkCenter(o,a,b,c,d){
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

function IfshowWorkCenter(count)
    {
        if(count=="0")
        {
            document.getElementById('divpageWorkCenter').style.display = "none";
            document.getElementById('divpageWorkCenter').style.display = "none";
        }
        else
        {
            document.getElementById('divpageWorkCenter').style.display = "block";
            document.getElementById('divpageWorkCenter').style.display = "block";
        }
    }
    
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndexWorkCenter(newPageCount,newPageIndex)
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
                this.pageCountWorkCenter=parseInt(newPageCount);
                TurnToPageWorkCenter(parseInt(newPageIndex));
            }
        }

    }
    //排序
    function OrderByWorkCenter(orderColum,orderTip)
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

