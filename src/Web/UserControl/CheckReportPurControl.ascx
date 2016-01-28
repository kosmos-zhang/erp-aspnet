<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheckReportPurControl.ascx.cs"
    Inherits="UserControl_CheckReportPurControl" %>
<a name="pageDataListReportPurMark"></a>
<div id="layout">
    <!--提示信息弹出详情start-->
    <div id="divReportPur" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 950px; z-index: 1001; position: absolute; display: none; top: 20%; left: 60%;
        margin: 5px 0 0 -600px;">
        <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
            class="table">
            <tr class="table-item">
                <td height="25" colspan="6" bgcolor="White" align="left" style="width: 50%;">
                    <img id="Button1" onclick="closeReportPurdiv()" src="../../../Images/Button/Bottom_btn_close.jpg"  value="关闭" />
                      <img id="Button2" onclick="removeAll6();" src="../../../Images/Button/Bottom_btn_del.jpg" value="清除" />
                </td>
            </tr>
            <tr class="table-item">
                <td width="13%" height="20" bgcolor="#E7E7E7" align="right">
                    单据编号
                </td>
                <td width="20%" bgcolor="#FFFFFF">
                    <input id="myBillNo22" type="text" style="width: 120px;" class="tdinput" maxlength="50" />
                </td>
                <td width="13%" bgcolor="#E7E7E7" align="right">
                    单据名称
                </td>
                <td width="20%" bgcolor="#FFFFFF">
                    <input id="myBillTitle22" class="tdinput" maxlength="50" type="text" />
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
                        onclick='TurnToPageCheckReportPur(1)' />&nbsp;
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1ReportPur"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReportPur2('ArriveNo','ArriveNo4');return false;">
                            单据编号<span id="ArriveNo4" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReportPur2('Title','Title4');return false;">
                            单据主题<span id="Title4" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReportPur2('EmployeeName','EmployeeName4');return false;">
                            采购员<span id="EmployeeName4" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReportPur2('DeptName','DeptName4');return false;">
                            部门<span id="DeptName4" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReportPur2('CustName','CustName4');return false;">
                            往来单位名称<span id="CustName4" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReportPur2('CustNo','CustNo4');return false;">
                            往来单位编号<span id="CustNo4" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReportPur2('CustBigTypeName','CustBigTypeName4');return false;">
                            往来单位大类<span id="CustBigTypeName4" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReportPur2('ArriveDate','ArriveDate4');return false;">
                            到货日期<span id="ArriveDate4" class="orderTip"></span></div>
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
                                <div id="pagecountReportReport">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageReportFrom_Pur" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divpageReportPur">
                                    <input name="text" type="text" id="txt_pageProduct" style="display: none" />
                                    <span id="pageDataList_TotalReportPur"></span>每页显示
                                    <input name="text" type="text" id="ShowPageCountReportPur" size="5" />
                                    条 转到第
                                    <input name="text" type="text" id="ToPageReportPur" size="5" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangeReportPurPageCountIndex($('#ShowPageCountReportPur').val(),$('#ToPageReportPur').val());" />
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
var popReportPurObj=new Object();
var myMethod;
popReportPurObj.ShowList=function(method)
{
    document.getElementById('divReportPur').style.display='block';
      openRotoscopingDiv(false,'divBackShadow','BackShadowIframe');
    myMethod=method;
    TurnToPageCheckReportPur(currentPageIndexReportPur);
}

    var pagecountReportReport = 10;//每页计数
    var totalRecordReport = 0;
    var pagerStyleReport = "flickr";//jPagerBar样式    
    
    var currentPageIndexReportPur = 1;
    var actionReportpur = "";//操作
    var orderByReportPur = "";//排序字段
    
    
    //jQuery-ajax获取JSON数据
    function TurnToPageCheckReportPur(pageIndexReport)
    {
          currentPageIndexReportPur = pageIndexReport;
           var TheBillTitle=document.getElementById('myBillTitle22').value;
           var TheBillNo=document.getElementById('myBillNo22').value;
            if(TheBillTitle!='')
            {
                if(strlen(TheBillTitle)>0)
                {
                    if(!CheckSpecialWord(TheBillTitle))
                    {
                       closeReportPurdiv();
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
                       closeReportPurdiv();
                       popMsgObj.ShowMsg('单据编号不能含有特殊字符!');
                       return false;
                    }
                }
            }
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/StorageReportPur.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndexReport+"&method="+myMethod+"&TheBillTitle="+escape(TheBillTitle)+"&TheBillNo="+escape(TheBillNo)+"&pageCount="+pagecountReportReport+"&action="+actionReportpur+"&orderby="+orderByReportPur,//数据
           beforeSend:function(){$("#pageReportFrom_Pur").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1ReportPur tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                      if(item.ID != null && item.ID != "")
                       $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioTech\" id=\"radioReport_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"Fun_FillReportPur('"+item.ID+"','"+item.ArriveNo+"','"+item.CustID+"','"+item.CustName+"','"+item.CustBigTypeID+"','"+item.CustBigTypeName+"');\" />"+"</td>"+
                       "<td height='22' align='center'>" + item.ArriveNo + "</td>"+
                        "<td height='22' align='center'>" + item.Title + "</td>"+
                        "<td height='22' align='center'>"+item.EmployeeName+"</td>"+
                        "<td height='22' align='center'>"+item.DeptName+"</td>"+
                        "<td height='22' align='center'>"+item.CustName+"</td>"+
                        "<td height='22' align='center'>"+item.CustNo+"</td>"+
                        "<td height='22' align='center'>"+item.CustBigTypeName+"</td>"+
                        "<td height='22' align='center'>"+item.ArriveDate.substring(0,10)+"</td>").appendTo($("#pageDataList1ReportPur tbody"));
                   });
//页码
                    ShowPageBar("pageReportFrom_Pur",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyleReport,mark:"pageDataListReportPurMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pagecountReportReport,
                    currentPageIndex:pageIndexReport,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"TurnToPageCheckReportPur({pageindex});return false;"}//[attr]
                    );
                  totalRecordReport = msg.totalCount;
                  $("#ShowPageCountReportPur").val(pagecountReportReport);  
                  ShowTotalPage(msg.totalCount,pagecountReportReport,currentPageIndexReportPur);               
                  $("#ToPageReportPur").val(pageIndexReport);
                  ShowTotalPage(msg.totalCount,pagecountReportReport,currentPageIndexReportPur,$("#pagecountReportReport"));

                  },
           error: function() {}, 
           complete:function(){$("#pageReportFrom_Pur").show();IfshowPur(document.getElementById("txt_pageProduct").value);pageDataList1ReportPur("pageDataList1ReportPur","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    
function Fun_FillReportPur(ID,ArriveNo,CustID,CustName,CustBigTypeID,CustBigTypeName)
{
   if(myMethod=='1')
   {
        document.getElementById('hiddenApplyID').value=ID;
        document.getElementById('ApplyID').value=ArriveNo;
        document.getElementById('hiddentbCustID').value=CustID;
        document.getElementById('tbCustID').value=CustName;
        document.getElementById('hiddentbCustBigID').value=CustBigTypeID;
        document.getElementById('tbCustBigID').value=CustBigTypeName;
        
   }
   else
   {
        document.getElementById('tbReportID').value=ID;
        document.getElementById('tbReportNo').value=ArriveNo;
   }
   closeReportPurdiv();
   
}
function removeAll6()
{
       if(myMethod=='1')
   {
        document.getElementById('hiddenApplyID').value='0';
        document.getElementById('ApplyID').value='';
        document.getElementById('hiddentbCustID').value='0';
        document.getElementById('tbCustID').value='';
        document.getElementById('hiddentbCustBigID').value='0';
        document.getElementById('tbCustBigID').value='';
        
   }
   else
   {
        document.getElementById('tbReportID').value='0';
        document.getElementById('tbReportNo').value='';
   }
   closeReportPurdiv();
}
    //table行颜色
function pageDataList1ReportPur(o,a,b,c,d){
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
            document.all["divpageReportPur"].style.display = "none";
            document.all["pagecountReportReport"].style.display = "none";
        }
        else
        {
            document.all["divpageReportPur"].style.display = "block";
            document.all["pagecountReportReport"].style.display = "block";
        }
       }
    }
    
  
    //改变每页记录数及跳至页数
    function ChangeReportPurPageCountIndex(newPageCount,newPageIndex)
    {
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecordReport-1)/newPageCount)+1 )
        {
            //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            return false;
        }
        else
        {
            this.pagecountReportReport=parseInt(newPageCount);
            TurnToPageCheckReportPur(parseInt(newPageIndex));
        }
    }
      //排序
    function orderByReportPur2(orderColum,orderTip)
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
        orderByReportPur = orderColum+"_"+ordering;
        TurnToPageCheckReportPur(1);
    }
 
function closeReportPurdiv()
{
    document.getElementById("divReportPur").style.display="none";
    document.getElementById('myBillTitle22').value='';
    document.getElementById('myBillNo22').value='';
    closeRotoscopingDiv(false,'divBackShadow');

}


</script>

