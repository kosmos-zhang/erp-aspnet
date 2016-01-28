<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ApplyReason.ascx.cs" Inherits="UserControl_ApplyReason" %>
<div id="layoutApplyReason">
    <!--提示信息弹出详情start-->
    <div id="divApplyReason" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 600px; z-index: 1001; position: absolute; display: none;
        top: 50%; left: 70%; margin: 5px 0 0 -400px;">
        <table width="100%"><tr><td><a onclick="popApplyReasonObj.closeApplyReasondiv()" style="text-align:left; cursor:pointer">关闭</a></td></tr></table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListApplyReason"bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">选择</th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick">原因ID<span id="Span1" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick">原因标题<span id="Span2" class="orderTip"></span></div></th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick">原因<span id="Span3" class="orderTip"></span></div></th>
                </tr>
            </tbody>
        </table>
        <br />
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"class="PageList">
            <tr>
                <td height="28" background="../../../images/Main/PageList_bg.jpg">
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                        <tr>
                            <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%"><div id="pageApplyReasoncount"></div></td>
                            <td height="28" align="right"><div id="pageDataList1_PagerApplyReason" class="jPagerBar"></div></td>
                            <td height="28" align="right">
                                <div id="divApplypage">
                                    <input name="TextApplyReason" type="text" id="TextApplyReason" style="display: none" />
                                    <span id="pageDataList1_Total"></span>每页显示
                                    <input name="ShowPageCountApplyReason" type="text" id="ShowPageCountApplyReason" />条 转到第
                                    <input name="ToPageApplyReason" type="text" id="ToPageApplyReason" />页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCountApplyReason').val(),$('#ToPageApplyReason').val());" />
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
var popApplyReasonObj=new Object();
popApplyReasonObj.ReasonID = null;
popApplyReasonObj.ReasonTitle = null;
popApplyReasonObj.ReasonName = null;

    var pageCountApplyReason = 10;//每页计数
    var totalRecordApplyReason= 0;
    var pagerStyleApplyReason = "flickr";//jPagerBar样式
    
    var currentPageIndexApplyReason = 1;
    var actionApplyReason = "";//操作
    var orderByApplyReason = "";//排序字段
    
popApplyReasonObj.ShowList = function(objInputReasonID,objInputReasonTitle,objInputReasonName)
{
    popApplyReasonObj.ReasonID = objInputReasonID;
    popApplyReasonObj.ReasonTitle = objInputReasonTitle;
    popApplyReasonObj.ReasonName = objInputReasonName;
    document.getElementById('divApplyReason').style.display='block';
    popApplyReasonObj.TurnToPage(1);
}

popApplyReasonObj.Fill = function (ReasonID,ReasonTitle,ReasonName)
{
    document.getElementById(popApplyReasonObj.ReasonID).value = ReasonID;
    document.getElementById(popApplyReasonObj.ReasonTitle).value = ReasonTitle;
    if((popApplyReasonObj.ReasonName !="" )&&(popApplyReasonObj.ReasonName!=null))
    {
        document.getElementById(popApplyReasonObj.ReasonName).value = ReasonName;
    }
    popApplyReasonObj.closeApplyReasondiv();
    
}
popApplyReasonObj.closeApplyReasondiv = function()
{
    document.getElementById("divApplyReason").style.display="none";
}


popApplyReasonObj.TurnToPage = function(pageIndexApplyReason)
{
       currentPageIndexApplyReason = pageIndexApplyReason;

       $.ajax({
       type: "POST",
       dataType:"json",
       url:  '../../../Handler/Office/PurchaseManager/ApplyReason.ashx',
       cache:false,
       data: "pageIndexApplyReason="+pageIndexApplyReason+"&pageCountApplyReason="+pageCountApplyReason+"&actionApplyReason="+actionApplyReason+"&orderbyApplyReason="+orderByApplyReason+"",
       beforeSend:function(){},
       
       success: function(msg){
                $("#pageDataListApplyReason tbody").find("tr.newrow").remove();
                $.each(msg.data,function(i,item){
                    if(item.ApplyReasonID != null && item.ApplyReasonID != "")
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioTech\" id=\"radioTech_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"popApplyReasonObj.Fill('"+item.ApplyReasonID+"','"+item.ApplyReasonTitle+"','"+item.ApplyReason+"');\" />"+"</td>"+
                    "<td height='22' align='center'>" + item.ApplyReasonID + "</td>"+
                    "<td height='22' align='center'>" + item.ApplyReasonTitle + "</td>"+
                    "<td height='22' align='center'>"+item.ApplyReason+"</td>").appendTo($("#pageDataListApplyReason tbody"));
               });
               ShowPageBar("pageDataList1_PagerApplyReason",
               "<%= Request.Url.AbsolutePath %>",
                {style:pagerStyleApplyReason,mark:"pageDataList1MarkApplyReason",
                totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountApplyReason,currentPageIndex:pageIndexApplyReason,noRecordTip:"没有符合条件的记录",preWord:"上页",nextWord:"下页",First:"首页",End:"末页",
                onclick:"popApplyReasonObj.TurnToPage({pageindexApplyReason});return false;"}
                );
              totalRecord = msg.totalCountApplyReason;
              document.all["TextApplyReason"].value=msg.totalCountApplyReason;
              $("#ShowPageCountApplyReason").val(pageCountApplyReason);
              ShowTotalPage(msg.totalCountApplyReason,pageCountApplyReason,pageIndexApplyReason);
              $("#ToPageApplyReason").val(pageIndexApplyReason);
              },
       error: function() {}, 
       complete:function(){}
       });
}
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

function Ifshow(count)
{
    if(count=="0")
    {
        document.all["divApplyReason"].style.display = "none";
        document.all["pagecountApplyReason"].style.display = "none";
    }
    else
    {
        document.all["divApplyReason"].style.display = "block";
        document.all["pagecountApplyReason"].style.display = "block";
    }
}

function SelectApplyReason(retval)
{
    alert(retval);
}

//改变每页记录数及跳至页数
function ChangePageCountIndexApplyReason(newPageCountApplyReason,newPageIndexApplyReason)
{
    if(newPageCountApplyReason <=0 || newPageIndexApplyReason <= 0 ||  newPageIndexApplyReason  > ((totalRecordApplyReason-1)/newPageCountApplyReason)+1 )
    {
        return false;
    }
    else
    {
        this.pageCountApplyReason=parseInt(newPageCountApplyReason);
        popApplyReasonObj.TurnToPage(parseInt(newPageIndexApplyReason));
    }
}
//排序
function OrderBy(orderColum,orderTip)
{
    var ordering = "a";
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
    popApplyReasonObj.TurnToPage(1);
}
   
function AddPop()
{
    
}
 
function hidePopup()
{
}
</script>