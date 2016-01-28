<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TakeMaterial.ascx.cs" Inherits="UserControl_TakeMaterial" %>
<div id="divPopTakeShadow" style="display: none">
    <iframe id="PopTakeShadowIframe" frameborder="0"
        width="100%"></iframe>
</div>
<div id="layout">
    <!--提示信息弹出详情start-->
    <div id="divTakeMaterial" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 600px; z-index: 1000; position: absolute; display: none; top: 50%; left: 70%;
        margin: 5px 0 0 -400px;">

        <table width="99%">
            <tr>
                <td width="15">

<img id="btnClose" alt="关闭" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: hand;' onclick="document.getElementById('divTakeMaterial').style.display='none';closeRotoscopingDiv(false,'divPopTakeShadow');"  />
                </td>

            </tr>
        </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListTake"
                    bgcolor="#999999">
                    <tbody>
                        <tr>
                            <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                选择
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick">
                                    单据编号<span id="oID" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" >
                                    主题<span id="oC1" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" >
                                    生产部门<span id="oC2" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" >
                                    领料人<span id="oC3" class="orderTip"></span></div>
                            </th>
                            <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                                <div class="orderClick" >
                                    领料日期<span id="oC4" class="orderTip"></span></div>
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
                                        <div id="pagecountTake">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="pageDataListTake_Pager" class="jPagerBar">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="divpageTake">
                                            <input name="text" type="text" id="TextTake" style="display: none" />
                                            <span id="pageDataListTake_Total"></span>每页显示
                                            <input name="text" type="text" id="ShowPageCountTake" style="width:25px"/>
                                            条 转到第
                                            <input name="text" type="text" id="ToPageTake" style="width:25px"/>
                                            页
                                            <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                                width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexTake($('#ShowPageCountTake').val(),$('#ToPageTake').val());" />
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
var popTakeObj=new Object();
popTakeObj.InputObj = null;

popTakeObj.ShowList=function(objInput)
{
    popTakeObj.InputObj = objInput;
    TurnToPageTake(currentPageIndex);
    openRotoscopingDiv(false,'divPopTakeShadow','PopTakeShadowIframe');
    document.getElementById('divTakeMaterial').style.display='block';
}

  
    var pageCountTake = 10;//每页计数
    var totalRecordTake = 0;
    var pagerStyleTake = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var actionTake = "";//操作
    var orderByTake = "ModifiedDate_d";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPageTake(pageIndexTake)
    {
           currentPageIndex = pageIndexTake;
           var TakeNo = '';
           var Subject='';
           var FromType='-1';
           var TaskID ='';
           var ProcessDeptID = '';
           var ManufactureType='';
           var Taker = '';
           var Handout = '-1';
           var TakeDate = '';
           var BillStatus = '2';
           var FlowStatus = '-1';
           var TakeDateStart = '';
           var TakeDateEnd = '';
           var ProjectID = '';
           var EFIndex = "";
           var EFDesc = "";
           
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/ProductionManager/TakeMaterialList.ashx',//目标地址
           cache:false,
           data: "pageIndex=" + pageIndexTake + "&pageCount=" + pageCountTake + "&action=" + actionTake + "&orderby=" + orderByTake + "&TakeNo=" + escape(TakeNo) + "&Subject=" + escape(Subject) + "&FromType=" + escape(FromType) + "&TaskID=" + escape(TaskID) + "&ProcessDeptID=" + escape(ProcessDeptID) + "&ManufactureType=" + escape(ManufactureType) + "&Taker=" + escape(Taker) + "&Handout=" + escape(Handout) + "&TakeDate=" + escape(TakeDate) + "&TakeDateStart=" + escape(TakeDateStart) + "&TakeDateEnd=" + escape(TakeDateEnd) + "&BillStatus=" + escape(BillStatus) + "&FlowStatus=" + escape(FlowStatus) + "&EFIndex=" + escape(EFIndex) + "&EFDesc=" + escape(EFDesc) + "&ProjectID=" + escape(ProjectID) + "", //数据
           beforeSend:function(){AddPopTake();$("#pageDataListTake_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListTake tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){   
                                             
                        if(item.ID != null && item.ID != "")
                        {
                            var tempMain = null;
                            var tempUsedStatus = null;
                            if(item.IsMainTech==1){tempMain='是';}else{tempMain='否';}
                            if(item.UsedStatus==1){tempUsedStatus='启用';}else{tempUsedStatus='停用';}
                            
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioTech\" id=\"radioTech_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"Fun_FillParent_TakeContent("+item.ID+",'"+item.TakeNo+"');\" />"+"</td>"+
                            "<td height='22' align='center'>" + item.TakeNo + "</td>"+
                            "<td height='22' align='center'>" + item.Subject +"</td>"+
                            "<td height='22' align='center'>"+item.ProcessDeptName+"</td>"+
                            "<td height='22' align='center'>"+item.TakerReal+"</td>"+
                            "<td height='22' align='center'>"+item.TakeDate+"</td>").appendTo($("#pageDataListTake tbody"));
                        }
                   });

                      ShowPageBar("pageDataListTake_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyleTake,mark:"pageDataListTakeMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageCountTake,
                    currentPageIndex:pageIndexTake,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"TurnToPageTake({pageindex});return false;"}//[attr]
                    );
                      totalRecord = msg.totalCount;
                     // $("#pageDataList1_TotalProduct").html(msg.totalCount);//记录总条数
                      document.getElementById("TextTake").value=msg.totalCount;
                      $("#ShowPageCountTake").val(pageCountTake);
                      ShowTotalPage(msg.totalCount,pageCountTake,pageIndexTake);
                      $("#ToPageTake").val(pageIndexTake);
                      ShowTotalPage(msg.totalCount,pageCountTake,currentPageIndex,$("#pagecountTake"));
                      },
           error: function() {}, 
           complete:function(){hidePopupTake();$("#pageDataListTake_Pager").show();IfshowTake(document.getElementById("TextTake").value);pageDataListTake("pageDataListTake","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    //table行颜色
function pageDataListTake(o,a,b,c,d){
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

function IfshowTake(count)
    {
        if(count=="0")
        {
            document.getElementById("divpageTake").style.display = "none";
            document.getElementById("pagecountTake").style.display = "none";
        }
        else
        {
            document.getElementById("divpageTake").style.display = "block";
            document.getElementById("pagecountTake").style.display = "block";
        }
    }
    
    function SelectDept(retval)
    {
        alert(retval);
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndexTake(newPageCount,newPageIndex)
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
                this.pageCountTake=parseInt(newPageCount);
                TurnToPageTake(parseInt(newPageIndex));
            }
        }

    }
    //排序
    function OrderByTake(orderColum,orderTip)
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
   
function AddPopTake()
{
    //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/extanim64.gif","数据处理中，请稍候……");
}
 
function hidePopupTake()
{
    //document.all.Forms.style.display = "none";
}

</script>