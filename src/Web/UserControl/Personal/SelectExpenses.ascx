<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectExpenses.ascx.cs" Inherits="UserControl_Personal_SelectExpenses" %>
<div id="ExpensesType">
    <!--提示信息弹出详情start-->
    <a name="pageExpTypeMark"></a>
    <div id="divExpensesTypeSelect" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 700px; z-index: 21; position: absolute; display: none;
        top:50%; left:45%; margin: 5px 0 0 -400px;">
        <table width="100%">
            <tr>
                <td align="left">
                 <img src="../../../Images/Button/Bottom_btn_close.jpg" alt="关闭" onclick="closeExpensesTypediv()" />
            
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="ExpensesTypeList"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="ExpUCOrderBy('CodeName','oSellFee');return false;">
                            费用名称<span id="oSellFee" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="ExpUCOrderBy('TypeName','oSFeeUC1');return false;">
                            费用类别<span id="oSFeeUC1" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="ExpUCOrderBy('Description','oSFeeUC2');return false;">
                            费用描述<span id="oSFeeUC2" class="orderTip"></span></div>
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
                                <div id="pageExpUCCount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="ExpensesUCList_Pager" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divExpensesUCPage">
                                    <span id="pageExpensesUCList_Total"></span>每页显示
                                    <input name="text" type="text" style="width: 30px;" id="ShowExpensesSelPageCount"  maxlength="3" />
                                    条 转到第
                                    <input name="text" type="text" style="width: 30px;" id="ToExpensesUCPage" maxlength="2" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangeExpensesSelPageCountIndex($('#ShowExpensesSelPageCount').val(),$('#ToExpensesUCPage').val());" />
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
var popExpensesTypeObj=new Object();
popExpensesTypeObj.InputObj1 = null;

var objid = "";//费用ID
var objcdname = "";//费用科目名
var objcd = "";//费用科目ID

popExpensesTypeObj.ShowList=function(objInput1,id,cdname,cd)
{
    popExpensesTypeObj.InputObj1 = objInput1;
    objid = id;
    objcdname = cdname;
    objcd = cd;
    
    ShowPreventReclickDiv();
    document.getElementById('divExpensesTypeSelect').style.display='block';
    ExpensesSelTurnToPage(currentExpUCPageIndex,objInput1);
}
  
    var pageExpUCCount = 10;//每页计数
    var totalExpRecord = 0;
    var pagerExpStyle = "flickr";//jPagerBar样式
    
    var currentExpUCPageIndex = 1;
    var orderBy = "";//排序字段
    //jQuery-ajax获取JSON数据
    function ExpensesSelTurnToPage(pageIndex,objInput1,obj2)
    {
           currentExpUCPageIndex = pageIndex;  
           var ExpBigType=document.getElementById("ExpType").value;   
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Personal/Expenses/SelectExpenses.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageExpUCCount="+pageExpUCCount+"&orderby="+orderBy+'&ExpBigType='+escape(ExpBigType)+'&action=getExpTypeList',//数据
           beforeSend:function(){$("#ExpensesUCList_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#ExpensesTypeList tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioFee_\" id=\"radioFee_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"fnSelectExpensesUC("+item.ID+",'"+item.CodeName+"','"+objInput1+"','"+item.FeeSubjectsNo+"','"+item.SubjectsName+"');\" />"+"</td>"+
                         "<td height='22' align='center'>"+ item.CodeName +"</td>"+
                         "<td height='22' align='center'>"+ item.TypeName +"</td>"+
                         "<td height='22' align='center'>"+item.Description+"</td>").appendTo($("#ExpensesTypeList tbody"));
                   });
                    //页码
                    ShowPageBar("ExpensesUCList_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerExpStyle,mark:"pageExpTypeMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageExpUCCount,
                    currentPageIndex:pageIndex,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"ExpensesSelTurnToPage({pageindex});return false;"}//[attr]
                    );
                  totalExpRecord = msg.totalCount;
                  $("#ShowExpensesSelPageCount").val(pageExpUCCount);
                  ShowTotalPage(msg.totalCount,pageExpUCCount,pageIndex,$("#pageExpUCCount"));
                  $("#ToExpensesUCPage").val(pageIndex);
                  },
           error: function() {}, 
           complete:function(){$("#ExpensesUCList_Pager").show();pageSellFeeDataList1("ExpensesTypeList","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    //table行颜色
function pageSellFeeDataList1(o,a,b,c,d)
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
function ChangeExpensesSelPageCountIndex(newPageCount,newPageIndex)
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    
    if (!IsNumber(newPageIndex) || newPageIndex == 0) {
        isFlag = false;
        fieldText = fieldText + "跳转页面|";
        msgText = msgText + "必须为正整数格式|";
    }
    if (!IsNumber(newPageCount) || newPageCount == 0) {
        isFlag = false;
        fieldText = fieldText + "每页显示|";
        msgText = msgText + "必须为正整数格式|";
    }
    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
    }
    else {
        if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalExpRecord - 1) / newPageCount) + 1) {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
            return false;
        }
        else {
            this.pageExpUCCount = parseInt(newPageCount);
            ExpensesSelTurnToPage(parseInt(newPageIndex));
        }
    }
}
//排序
function ExpUCOrderBy(orderColum,orderTip)
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
    orderBy = orderColum+"_"+ordering;
    ExpensesSelTurnToPage(1);
}
   
function closeExpensesTypediv() {
    obj = document.getElementById("divPreventReclick");
    //隐藏遮挡的DIV
    if (obj != null && typeof (obj) != "undefined") obj.style.display = "none";
    document.getElementById("divExpensesTypeSelect").style.display="none";
}

function fnSelectExpensesUC(ID,CodeName,objInput1,FeeSubjectsNo,SubjectsName)
{
    //$("#"+objInput1).val(CodeName);
    //$("#"+objInput1).attr("title",ID);
    $("#"+popExpensesTypeObj.InputObj1 ).val(CodeName);
    $("#"+popExpensesTypeObj.InputObj1 ).attr("title",ID);
    
    $("#"+objid).val(ID);
    $("#"+objcdname).val(SubjectsName);
    $("#"+objcd).val(FeeSubjectsNo);

    closeExpensesTypediv();
}
</script>