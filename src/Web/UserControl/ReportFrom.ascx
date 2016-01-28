<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportFrom.ascx.cs" Inherits="UserControl_ReportFrom" %>
<a name="pageDataListReportMark"></a>
<div id="layout">
    <!--提示信息弹出详情start-->
    <div id="divReportFromType" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 900px; z-index: 1001; position: absolute; display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -600px;">
        <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
            class="table">
            <tr class="table-item">
                <td height="25" colspan="6" bgcolor="white" align="left" style="width: 50%;">
                    <img onclick="closeReportFromTypediv()" id="Button1" src="../../../Images/Button/Bottom_btn_close.jpg" />
                    <img id="clearimg" onclick="removeAll5();" src="../../../Images/Button/Bottom_btn_del.jpg" />
                </td>
              <%--  <td height="25" colspan="3" bgcolor="#E7E7E7" align="center" style="width: 50%;">
                    <input id="Button2" type="button" onclick="removeAll5();" value="清除" />
                </td>--%>
            </tr>
            <tr class="table-item">
                <td width="13%" height="20" bgcolor="#E7E7E7" align="right">
                    单据编号
                </td>
                <td width="20%" bgcolor="#FFFFFF">
                    <input id="myBillNo55" type="text" style="width: 120px;" class="tdinput" maxlength="50" />
                </td>
                <td width="13%" bgcolor="#E7E7E7" align="right">
                    单据名称
                </td>
                <td width="20%" bgcolor="#FFFFFF">
                    <input id="myBillTitle55" class="tdinput" maxlength="50" type="text" />
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
                        onclick='TurnToPageReportFromType(1)' />&nbsp;
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1ReportFromType"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>                   
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReport2('ReportNo','ReportNo2');return false;">
                            单据编号<span id="ReportNo2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReport2('Title','Title2');return false;">
                            单据标题<span id="Title2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReport2('ProductName','ProductName2');return false;">
                            产品名称<span id="ProductName2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReport2('ProNo','ProNo2');return false;">
                            产品编号<span id="ProNo2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReport2('CodeName','CodeName2');return false;">
                            单位<span id="CodeName2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReport2('Specification','Specification2');return false;">
                            规格<span id="Specification2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReport2('CheckNum','CheckNum2');return false;">
                            报检数量<span id="CheckNum2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReport2('CheckedCount','CheckedCount2');return false;">
                            已检数量<span id="CheckedCount2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReport2('CheckerName','CheckerName2');return false;">
                            报检人员<span id="CheckerName2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReport2('DeptName','DeptName2');return false;">
                            报检部门<span id="DeptName2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReport2('CheckModeName','CheckModeName2');return false;">
                            检验方式<span id="CheckModeName2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReport2('CheckTypeName','CheckTypeName2');return false;">
                            检验类别<span id="CheckTypeName2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReport2('CheckContent','CheckContent2');return false;">
                            检验方案<span id="CheckContent2" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReport2('CheckStandard','CheckStandard2');return false;">
                            检验指标<span id="CheckStandard2" class="orderTip"></span></div>
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
                                <div id="pagecountReportFrom1">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageReport_PagerFromType" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divpageReportFromType">
                                    <input name="text" type="text" id="txt_pageReportFromType" style="display: none" />
                                    <span id="pageDataList_TotalReportMan"></span>每页显示
                                    <input name="text" type="text" id="ShowPageCountReportFrom1" size="5" />
                                    条 转到第
                                    <input name="text" type="text" id="ToPageReportFrom1" size="5" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangeReportFromPageCountIndex1($('#ShowPageCountReportFrom1').val(),$('#ToPageReportFrom1').val());" />
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
var popReportFromTypeObj=new Object();
var myMethod='';
popReportFromTypeObj.ShowFromTypeList=function(method)
{
    myMethod=method;
    document.getElementById('divReportFromType').style.display='block';
      openRotoscopingDiv(false,'divBackShadow','BackShadowIframe');
    TurnToPageReportFromType(currentPageIndexReport);
}

    var pagecountReportFrom1 = 10;//每页计数
    var totalRecordReport = 0;
    var pagerStyleReport = "flickr";//jPagerBar样式
    
    var currentPageIndexReport = 1;
    var actionReport = "";//操作
    var orderByReport = "";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPageReportFromType(pageIndexReport)
    {
           currentPageIndexReport = pageIndexReport;
           var TheBillNo=document.getElementById('myBillNo55').value;
           var TheBillTitle=document.getElementById('myBillTitle55').value;
            if(TheBillTitle!='')
            {
                if(strlen(TheBillTitle)>0)
                {
                    if(!CheckSpecialWord(TheBillTitle))
                    {
                       closeReportFromTypediv();
                       popMsgObj.ShowMsg('单据名称不能含有特殊字符!');
                       return false;
                    }
                }
            }
            if(TheBillNo!='')
            {
                if(strlen(TheBillNo)>0)
                {
                    if(!CheckSpecialWord(TheBillNo))
                    {
                       closeReportFromTypediv();
                       popMsgObj.ShowMsg('单据编号不能含有特殊字符!');
                       return false;
                    }
                }
            }
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/CheckReportFrom.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndexReport+"&method="+myMethod+"&TheBillTitle="+escape(TheBillTitle)+"&TheBillNo="+escape(TheBillNo)+"&pageCount="+pagecountReportFrom1+"&action="+actionReport+"&orderby="+orderByReport,//数据
           beforeSend:function(){$("#pageReport_PagerFromType").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1ReportFromType tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioReportFrom_"+item.ID+"\" id=\"radioReportFrom_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"FillReportForom('"+item.ReportNo+"','"+item.CheckType+"','"+item.CheckMode+"','"+item.CheckerID+"','"+item.CheckerName+"','"+item.DeptID+"','"+item.DeptName+"','"+item.ProNo+"','"+item.ProductName+"','"+item.UnitID+"','"+item.CodeName+"','"+parseFloat(parseFloat(item.CheckNum)-parseFloat(item.CheckedCount)).toFixed(2)+"','"+item.ID+"','"+item.FromLineNo+"','"+item.ProductID+"','"+item.CheckContent+"','"+item.CheckStandard+"','"+item.Specification+"');\" /></td>"+
                        "<td height='22' align='center'>"+item.ReportNo+"</td>"+
                        "<td height='22' align='center'>"+item.Title+"</td>"+
                        "<td height='22' align='center'>"+item.ProductName+"</td>"+ 
                        "<td height='22' align='center'>"+item.ProNo+"</td>"+
                        "<td height='22' align='center'>"+item.CodeName+"</td>"+
                          "<td height='22' align='center'>"+item.Specification+"</td>"+
                        "<td height='22' align='center'>"+parseFloat(item.CheckNum).toFixed(2)+"</td>"+
                        "<td height='22' align='center'>"+parseFloat(item.CheckedCount).toFixed(2)+"</td>"+
                        "<td height='22' align='center'>"+item.CheckerName+"</td>"+
                        "<td height='22' align='center'>"+item.DeptName+"</td>"+
                        "<td height='22' align='center'>"+item.CheckModeName+"</td>"+
                        "<td height='22' align='center'>"+item.CheckTypeName+"</td>"+
                         "<td height='22' align='center'>"+item.CheckContent+"</td>"+
                        "<td height='22' align='center'>"+item.CheckStandard+"</td>").appendTo($("#pageDataList1ReportFromType tbody"));
                   });
                   
                   //页码 
                    ShowPageBar("pageReport_PagerFromType",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyleReport,mark:"pageDataListReportMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pagecountReportFrom1,
                    currentPageIndex:pageIndexReport,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"TurnToPageReportFromType({pageindex});return false;"}//[attr]
                    );
                  totalRecordReport = msg.totalCount;
                  $("#ShowPageCountReportFrom1").val(pagecountReportFrom1);  
                  ShowTotalPage(msg.totalCount,pagecountReportFrom1,currentPageIndexReport);               
                  $("#ToPageReportFrom1").val(pageIndexReport);
                  ShowTotalPage(msg.totalCount,pagecountReportFrom1,currentPageIndexReport,$("#pagecountReportFrom1"));
                 
                  },
           error: function() {}, 
           complete:function(){hidePopup();$("#pageReport_PagerFromType").show();IfshowReportFrom(document.getElementById("txt_pageReportFromType").value);pageDataList1ReportFromType("pageDataList1ReportFromType","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
 function removeAll5()
 {
     if(myMethod=='1')
 {
   document.getElementById('ApplyID').value='';
   document.getElementById('ddlCheckType').value='1';
   //document.getElementById('ddlCheckMode').value='1';
   document.getElementById('UserChecker').value='';
   document.getElementById('hiddentbChecker').value='0';
   document.getElementById('DeptChecker').value='';
   document.getElementById('hiddentbCheckDept').value='0';
   document.getElementById('tbProNo').value='';
   document.getElementById('tbProName').value='';
   document.getElementById('hiddentbUnit').value='0';
   document.getElementById('tbUnit').value='';
   document.getElementById('tbProductCount').value=FormatAfterDotNumber(0,selPoint);
   document.getElementById('hiddenApplyID').value='0';
   document.getElementById('hiddenLineNo').value='0';
   document.getElementById('hiddenProID').value='0';
   document.getElementById('CheckContent').value='';
        document.getElementById('CheckContent').readOnly=false;
   document.getElementById('CheckStandard').readOnly=false;
      document.getElementById('CheckStandard').value='';
         document.getElementById('mySpecification').value='';

 }
 else
 {
    document.getElementById('tbReportID').value='0';
    document.getElementById('tbReportNo').value='';
 }
   closeReportFromTypediv();
 }
function FillReportForom(a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r)
{
 if(myMethod=='1')
 {
   document.getElementById('ApplyID').value=a;
   document.getElementById('ddlCheckType').value=b;
   //document.getElementById('ddlCheckMode').value=c;
   document.getElementById('UserChecker').value=e;
   document.getElementById('hiddentbChecker').value=d;
   document.getElementById('DeptChecker').value=g;
   document.getElementById('hiddentbCheckDept').value=f;
   document.getElementById('tbProNo').value=h;
   document.getElementById('tbProName').value=i;
   document.getElementById('hiddentbUnit').value=j;
   document.getElementById('tbUnit').value=k;
   document.getElementById('tbProductCount').value=FormatAfterDotNumber(parseFloat(l),selPoint);
   document.getElementById('hiddenApplyID').value=m;
   document.getElementById('hiddenLineNo').value=n;
   document.getElementById('hiddenProID').value=o;
   document.getElementById('CheckContent').value=p;
   document.getElementById('CheckStandard').value=q;
   document.getElementById('CheckContent').readOnly=true;
   document.getElementById('CheckStandard').readOnly=true;
   document.getElementById('mySpecification').value=r;

 }
 else
 {
    document.getElementById('tbReportID').value=m;
    document.getElementById('tbReportNo').value=a;
 }
   closeReportFromTypediv();
   
}
function ChangeReportFromPageCountIndex1(newPageCount,newPageIndex)
{
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecordReport-1)/newPageCount)+1 )
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            return false;
        }
        else
        {
            this.pagecountReportFrom1=parseInt(newPageCount);
            TurnToPageReportFromType(parseInt(newPageIndex));
       }
}
    //table行颜色
function pageDataList1ReportFromType(o,a,b,c,d){
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

function Fun_Search_StorageProduct(aa)
{
      search="1";
      TurnToPage(1);
}
function IfshowReportFrom(count)
    {
      if(myMethod=='1')
      {
        if(count=="0")
        {
            document.getElementById("divpageReportFromType").style.display = "none";
            document.getElementById("pagecountReportFrom1").style.display = "none";
        }
        else
        {
            document.getElementById("divpageReportFromType").style.display = "block";
            document.getElementById("pagecountReportFrom1").style.display = "block";
        }
      }
    }
    
    function SelectDept(retval)
    {
        alert(retval);
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndexProduct(newPageCount,newPageIndex)
    {
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
        {
            //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            return false;
        }
        else
        {
            this.pagecountReportFrom1=parseInt(newPageCount);
            TurnToPageReportFromType(parseInt(newPageIndex));
        }
    }
    //排序
    function orderByReport2(orderColum,orderTip)
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
        orderByReport = orderColum+"_"+ordering;
        TurnToPageReportFromType(1);
    }
 
function closeReportFromTypediv()
{
    document.getElementById("divReportFromType").style.display="none";
    document.getElementById('myBillNo55').value='';
    document.getElementById('myBillTitle55').value='';
    closeRotoscopingDiv(false,'divBackShadow');

}
</script>

