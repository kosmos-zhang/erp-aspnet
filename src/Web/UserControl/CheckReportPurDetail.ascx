<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheckReportPurDetail.ascx.cs" Inherits="UserControl_CheckReportPurDetail" %>
 <a name="pageDataListReportPurDetailMark"></a>
<div id="layout">
    <!--提示信息弹出详情start-->
    <div id="divReportPurDetail" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 950px; z-index: 1001; position: absolute; display: none; top: 20%; left: 60%;
        margin: 5px 0 0 -600px;">      
        <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
            class="table">
            <tr class="table-item">
                <td height="25" colspan="6" bgcolor="White" align="left" style="width: 50%;">
                   <img onclick="closeReportPurDetaildiv()" id="Button3" src="../../../Images/Button/Bottom_btn_close.jpg"  />
                   <img id="Button4" type="button" onclick="removeAll7();" src="../../../Images/Button/Bottom_btn_del.jpg" />
                </td>
       
            </tr>
            <tr class="table-item">
                <td width="13%" height="20" bgcolor="#E7E7E7" align="right">
                    物品编号
                </td>
                <td width="20%" bgcolor="#FFFFFF">
                    <input id="myProNo33" type="text" style="width: 120px;" class="tdinput" maxlength="50" />
                </td>
                <td width="13%" bgcolor="#E7E7E7" align="right">
                    物品名称
                </td>
                <td width="20%" bgcolor="#FFFFFF">
                    <input id="myProductName33" class="tdinput" maxlength="50" type="text" />
                </td>
                <td width="13%" bgcolor="#E7E7E7" align="right">
                </td>
                <td width="21%" bgcolor="#FFFFFF">
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center" bgcolor="#FFFFFF">
                    <input type="hidden" id="hiddConSearchModel" value="all" />
                    <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: pointer;'
                        onclick='TurnToPageCheckReportPurDetail(1)' />&nbsp;
                </td>
            </tr>
        </table>
        
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1ReportPurDetail"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>                   
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div  class="orderClick" onclick="orderByReportPurDetail2('ProductName','ProductName5');return false;">
                           物品名称<span id="ProductName5" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div  class="orderClick" onclick="orderByReportPurDetail2('ProNo','ProNo5');return false;" >
                            物品编号<span id="ProNo5" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div  class="orderClick" onclick="orderByReportPurDetail2('CodeName','CodeName5');return false;">
                            单位<span id="CodeName5" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReportPurDetail2('ProductCount','ProductCount5');return false;">
                            到货数量<span id="ProductCount5" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div  class="orderClick" onclick="orderByReportPurDetail2('ApplyCheckCount','ApplyCheckCount5');return false;">
                            已报检数量<span id="ApplyCheckCount5" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div  class="orderClick" onclick="orderByReportPurDetail2('CheckedCount','CheckedCount5');return false;">
                            已检数量<span id="CheckedCount5" class="orderTip"></span></div>
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
                            <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                <div id="pagecountReportReportDetail">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageReportFrom_PurDetail" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divpageReportPurDetail">
                                    <input name="text" type="text" id="txt_pagePurDetail" style="display: none" />
                                    <span id="pageDataList_TotalReportPurDetail"></span>每页显示
                                    <input name="text" type="text" id="ShowPageCountReportPurDetail" size="5" />
                                    条 转到第
                                    <input name="text" type="text" id="ToPageReportPurDetail" size="5" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangeReportPurDetailPageCountIndex($('#ShowPageCountReportPurDetail').val(),$('#ToPageReportPurDetail').val());" />
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
var popReportPurDetailObj=new Object();
var myMethod;
popReportPurDetailObj.ShowList=function(method)
{
    document.getElementById('divReportPurDetail').style.display='block';
      openRotoscopingDiv(false,'divBackShadow','BackShadowIframe');
    myMethod=method;
    TurnToPageCheckReportPurDetail(currentPageIndexReportPur);
}

    var pagecountReportReportDetail = 10;//每页计数
    var totalRecordReport = 0;
    var pagerStyleReport = "flickr";//jPagerBar样式    
    
    var currentPageIndexReportPur = 1;
    var actionReportpur = "";//操作
    var orderByReportPurDetail = "";//排序字段
    
    
    //jQuery-ajax获取JSON数据
    function TurnToPageCheckReportPurDetail(pageIndexReport)
    {
           var MyProductName1=document.getElementById('myProductName33').value;
           var MyProNo1=document.getElementById('myProNo33').value;
            if(MyProductName1!='')
            {
                if(strlen(MyProductName1)>0)
                {
                    if(!CheckSpecialWord(MyProductName1))
                    {
                       closeReportPurDetaildiv();
                       popMsgObj.ShowMsg('物品名称不能含有特殊字符!');
                       return false;
                    }
                }
            }
            if(MyProNo1!='')
            {
                if(strlen(MyProNo1)>0)
                {
                    if(!CheckSpecialWord(MyProNo1))
                    {
                       closeReportPurDetaildiv();
                       popMsgObj.ShowMsg('物品编号不能含有特殊字符!');
                       return false;
                    }
                }
            }
          currentPageIndexReportPur = pageIndexReport;
           var ArriveNo=document.getElementById('ApplyID').value;
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/ReportPurDetail.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndexReport+"&pageCount="+pagecountReportReportDetail+"&MyProNo1="+escape(MyProNo1)+"&MyProductName1="+escape(MyProductName1)+"&ArriveNo="+ArriveNo+"&action="+actionReportpur+"&orderby="+orderByReportPurDetail,//数据
           beforeSend:function(){$("#pageReportFrom_PurDetail").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1ReportPurDetail tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                      if(item.ID != null && item.ID != "")
                       $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioTech\" id=\"radioReport_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"Fun_FillReportPurDetail('"+item.ID+"','"+item.ProNo+"','"+item.ProductName+"','"+item.CodeName+"','"+item.ProductID+"','"+item.ProductCount+"','"+item.CheckedCount+"','"+item.ApplyCheckCount+"','"+item.Specification+"');\" />"+"</td>"+
                       "<td height='22' align='center'>" + item.ProductName + "</td>"+
                        "<td height='22' align='center'>" + item.ProNo + "</td>"+
                        "<td height='22' align='center'>"+item.CodeName+"</td>"+
                        "<td height='22' align='center'>"+item.ProductCount+"</td>"+
                        "<td height='22' align='center'>"+item.ApplyCheckCount+"</td>"+
                        "<td height='22' align='center'>"+item.CheckedCount+"</td>").appendTo($("#pageDataList1ReportPurDetail tbody"));
                   });
//页码
                    ShowPageBar("pageReportFrom_PurDetail",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyleReport,mark:"pageDataListReportPurDetailMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pagecountReportReportDetail,
                    currentPageIndex:pageIndexReport,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"TurnToPageCheckReportPurDetail({pageindex});return false;"}//[attr]
                    );
                  totalRecordReport = msg.totalCount;
                  $("#ShowPageCountReportPurDetail").val(pagecountReportReportDetail);  
                  ShowTotalPage(msg.totalCount,pagecountReportReportDetail,currentPageIndexReportPur);               
                  $("#ToPageReportPurDetail").val(pageIndexReport);
                  ShowTotalPage(msg.totalCount,pagecountReportReportDetail,currentPageIndexReportPur,$("#pagecountReportReportDetail"));

                  },
           error: function() {}, 
           complete:function(){$("#pageReportFrom_PurDetail").show();IfshowPur(document.getElementById("txt_pagePurDetail").value);pageDataList1ReportPurDetail("pageDataList1ReportPurDetail","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    
function Fun_FillReportPurDetail(ID,ProNo,ProductName,CodeName,ProductID,ProductCount,CheckedCount,ApplyCheckCount,Specification)
{
        document.getElementById('tbProNo').value=ProNo;
        document.getElementById('tbProName').value=ProductName;
        document.getElementById('hiddenProID').value=ProductID;
        document.getElementById('tbUnit').value=CodeName;
        document.getElementById('FromDetailID').value=ID;  
        document.getElementById('mySpecification').value=Specification;
        document.getElementById('tbProductCount').value=FormatAfterDotNumber(parseFloat(parseFloat(ProductCount)-parseFloat(ApplyCheckCount)),selPoint);     
        closeReportPurDetaildiv();
   
}
function removeAll7()
{
            document.getElementById('tbProNo').value='';
        document.getElementById('tbProName').value='';
        document.getElementById('hiddenProID').value='0';
        document.getElementById('tbUnit').value='';
        document.getElementById('FromDetailID').value='0';
        document.getElementById('mySpecification').value='';       
        document.getElementById('tbProductCount').value=FormatAfterDotNumber(0,selPoint);
        closeReportPurDetaildiv();
}
    //table行颜色
function pageDataList1ReportPurDetail(o,a,b,c,d){
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

function IfshowPur(count)
    {
       if(myMethod=='1')
       {
        if(count=="0")
        {
            document.all["divpageReportPurDetail"].style.display = "none";
            document.all["pagecountReportReportDetail"].style.display = "none";
        }
        else
        {
            document.all["divpageReportPurDetail"].style.display = "block";
            document.all["pagecountReportReportDetail"].style.display = "block";
        }
       }
    }
    
  
    //改变每页记录数及跳至页数
    function ChangeReportPurDetailPageCountIndex(newPageCount,newPageIndex)
    {
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecordReport-1)/newPageCount)+1 )
        {
            //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            return false;
        }
        else
        {
            this.pagecountReportReportDetail=parseInt(newPageCount);
            TurnToPageCheckReportPurDetail(parseInt(newPageIndex));
        }
    }
      //排序
    function orderByReportPurDetail2(orderColum,orderTip)
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
        orderByReportPurDetail = orderColum+"_"+ordering;
        
        TurnToPageCheckReportPurDetail(1);
    }
 
function closeReportPurDetaildiv()
{
    document.getElementById("divReportPurDetail").style.display="none";
    document.getElementById('myProductName33').value='';
    document.getElementById('myProNo33').value='';
    closeRotoscopingDiv(false,'divBackShadow');

}


</script>