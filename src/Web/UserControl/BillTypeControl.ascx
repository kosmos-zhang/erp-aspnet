<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BillTypeControl.ascx.cs" Inherits="UserControl_BillTypeControl" %>


<script src="../js/JQuery/jquery_last.js" type="text/javascript"></script>

<script src="../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>
<div id="layout">
    <!--提示信息弹出详情start-->
    <div id="divStorageProduct" style="border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 600px; z-index: 1001; position: absolute; display: none;
        top: 50%; left: 70%; margin: 5px 0 0 -400px;">
        <table width="100%">
            <tr>
                <td>
                    <a onclick="closediv()" style="text-align: right; cursor: pointer">关闭</a>
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
                    <%-- <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            查看<span id="oID" class="orderTip"></span></div>
                    </th>--%>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            单据分类编码<span id="Span1" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            单据分类名称<span id="oC2" class="orderTip"></span></div>
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
                                <div id="pageproductcount">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataList1_Pager" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divproductpage">
                                    <input name="text" type="text" id="Text2" style="display: none" />
                                    <span id="pageDataList1_Total"></span>每页显示
                                    <input name="text" type="text" id="ShowProductPageCount" />
                                    条 转到第
                                    <input name="text" type="text" id="ToProductPage" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#ShowProductPageCount').val(),$('#ToProductPage').val());" />
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


popTechObj.ShowList=function()
{
    document.getElementById('divStorageProduct').style.display='block';
    TurnToPage(currentPageIndex);
}

//$(document).ready(function(){
//      TurnToPage(currentPageIndex);
//    });    
    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var action = "";//操作
    var orderBy = "";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)
    {
           currentPageIndex = pageIndex;
           var BillTypeName=document.getElementById("Ddp_BillType").value;
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/SystemManager/ApprovalFlowSet.ashx?BillTypeName='+BillTypeName+'&orderBy='+orderBy+'',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&BillTypeName="+escape(BillTypeName)+"",//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioTech\" id=\"radioTech_"+item.ID+"\" value=\""+item.TypeCode+","+item.TypeName+"\" onclick=\"document.getElementById('txt_BillTypeName').value= this.value.split(',')[1];document.getElementById('hd_typecode').value= this.value.split(',')[0]\" />"+"</td>"+
//                        "<td height='22' align='center'><a href=\"StorageProducntAdd.aspx?intProductID="+item.ID+"\">"+ item.ProdNo +"</a></td>"+
                        "<td height='22' align='center'>" + item.TypeCode + "</td>"+
                        "<td height='22' align='center'>"+item.TypeName+"</td>").appendTo($("#pageDataList1 tbody"));
                   });
                    //页码
                   ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上页",nextWord:"下页",First:"首页",End:"末页",
                    onclick:"TurnToPage({pageindex});return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.all["Text2"].value=msg.totalCount;
                  $("#ShowProductPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                  $("#ToProductPage").val(pageIndex);
                  },
           error: function() {}, 
           complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.all["Text2"].value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
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

function Fun_Search_StorageProduct(aa)
{
      search="1";
      TurnToPage(1);
}
function Ifshow(count)
    {
        if(count=="0")
        {
            document.all["divproductpage"].style.display = "none";
            document.all["pageproductcount"].style.display = "none";
        }
        else
        {
            document.all["divproductpage"].style.display = "block";
            document.all["pageproductcount"].style.display = "block";
        }
    }
    
    function SelectDept(retval)
    {
        alert(retval);
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndex(newPageCount,newPageIndex)
    {
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
        {
            //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            return false;
        }
        else
        {
            this.pageCount=parseInt(newPageCount);
            TurnToPage(parseInt(newPageIndex));
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
function Create_Div(img,img1,bool)
{
	FormStr = "<table width=100% height='104' border='0' cellpadding=0 cellspacing=0 bgcolor=#FFFFFF>"
	FormStr += "<tr>"
	FormStr += "<td width=90%  bgcolor=#3F6C96>&nbsp;</td>"
	FormStr += "<td width=10% height=20 bgcolor=#3F6C96>"
	if(bool)
	{
		FormStr += "<img src='" + img +"' style='cursor:hand;display:block;' id='CloseImg' onClick=document.all['Forms'].style.display='none';>"
	}
	FormStr += "</td></tr><tr><td height='1' colspan='2' background='../../../Images/Pic/bg_03.gif'></td></tr><tr><td valign=top height=104 id='FormsContent' colspan=2 align=center>"
	FormStr += "<table width=100% border=0 cellspacing=0 cellpadding=0>"
	FormStr += "<tr>"
	FormStr += "<td width=25% align='center' valign=top style='padding-top:20px;'>"
	FormStr += "<img name=exit src='" + img1 + "' border=0></td>";
	FormStr += "<td width=75% height=50 id='FormContent' style='padding-left:5pt;padding-top:20px;line-height:17pt;' valign=top></td>"
	FormStr += "</tr></table>"
	FormStr += "</td></tr></table>"
	return FormStr;
} 
function closediv()
{
    document.getElementById("divStorageProduct").style.display="none";
}


</script>