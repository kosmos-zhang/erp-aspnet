<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TechnicsArchives.ascx.cs" Inherits="UserControl_TechnicsArchives" %>
 <div id="divPopTechShadow" style="display: none">
    <iframe id="PopTechShadowIframe" frameborder="0" width="100%"></iframe>
</div>
<div id="layout">
    <!--提示信息弹出详情start-->
    <div id="divTechnicsArchives" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 600px; z-index: 1000; position: absolute; display: none;
        top: 20%; left: 70%; margin: 5px 0 0 -400px;">
        <table width="99%">
            <tr>
                <td>
                    <img id="btnClose" alt="关闭" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: hand;' onclick="document.getElementById('divTechnicsArchives').style.display='none';closeRotoscopingDiv(false,'divPopTechShadow');"  />
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            工艺代码<span id="oID" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            工艺名称<span id="oC1" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            拼音缩写<span id="oC2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            工艺描述<span id="oC3" class="orderTip"></span></div>
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
                            <td height="28" background="../../../images/Main/PageList_bg.jpg" width="25%">
                                <div id="pagecount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataList1_Pager" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divpage">
                                    <input name="text" type="text" id="Text2" style="display: none" />
                                    <span id="pageDataList1_Total"></span>每页显示
                                    <input name="text" type="text" id="ShowPageCount" />
                                    条 转到第
                                    <input name="text" type="text" id="ToPage" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());" />
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
var popTechObj=new Object();
popTechObj.InputObj = null;

popTechObj.ShowList=function(objInput)
{
    popTechObj.InputObj = objInput;
    openRotoscopingDiv(false,'divPopTechShadow','PopTechShadowIframe');
    document.getElementById('divTechnicsArchives').style.display='block';
}

$(document).ready(function(){
      TurnToPage(currentPageIndex);
    });    
    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var action = "";//操作
    var orderBy = "ModifiedDate_d";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)
    {
           currentPageIndex = pageIndex;
           var TechNo= "";
           var TechName="";
           var PYShort="";
           var UsedStatus = "1";
           
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/ProductionManager/TechnicsArchivesList.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+"&TechNo="+escape(TechNo)+"&TechName="+escape(TechName)+"&PYShort="+escape(PYShort)+"&UsedStatus="+escape(UsedStatus)+"",//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        var tempUsedStatus = '';
                        if(item.UsedStatus==0) tempUsedStatus='停用';
                        if(item.UsedStatus==1) tempUsedStatus='启用';
                        
                        if(item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioTech\" id=\"radioTech_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"Fun_FillParent_Content("+item.ID+",'"+item.TechName+"');\" />"+"</td>"+
                        "<td height='22' align='center'>" + item.TechNo + "</td>"+
                        "<td height='22' align='center'>" + item.TechName +"</td>"+
                        "<td height='22' align='center'>"+item.PYShort+"</td>"+
                        "<td height='22' align='center'>"+item.Remark+"</td>"+
                        "<td height='22' align='center'>"+tempUsedStatus+"</td>").appendTo($("#pageDataList1 tbody"));
                   });
                    //页码
                   ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPage({pageindex});return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                  document.getElementById("Text2").value=msg.totalCount;
                  $("#ShowPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex);
                  $("#ToPage").val(pageIndex);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                  },
           error: function() {}, 
           complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.getElementById("Text2").value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    //table行颜色
function pageDataList1(o,a,b,c,d){
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

function Fun_Search_TechnicsArchives(aa)
{
      search="1";
      TurnToPage(1);
}
function Ifshow(count)
{
    if(count=="0")
    {
        document.getElementById("divpage").style.display = "none";
        document.getElementById("pagecount").style.display = "none";
    }
    else
    {
        document.getElementById("divpage").style.display = "block";
        document.getElementById("pagecount").style.display = "block";
    }
}
    
//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount,newPageIndex)
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
            this.pageCount=parseInt(newPageCount);
            TurnToPage(parseInt(newPageIndex));
        }
    }
}
//排序
function OrderBy(orderColum,orderTip)
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
   
function AddPop()
{
    //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/extanim64.gif","数据处理中，请稍候……");
}
 
function hidePopup()
{
    //document.all.Forms.style.display = "none";
}


</script>

