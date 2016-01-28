<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManProductInfo.ascx.cs"
    Inherits="UserControl_ManProductInfo" %>
<div id="layout">
    <!--提示信息弹出详情start-->
    <div id="divReportManProduct" style="top: 20%; border: solid 10px #93BCDD; background: #fff;
        padding: 10px; width: 800px; z-index: 1001; position: absolute; display: none;
        top: 50%; left: 70%; margin: 2px 0 0 -700px;">
        <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
            class="table">
            <tr class="table-item">
                <td height="25" colspan="6" bgcolor="White" align="left" style="width: 50%;">
                    <img onclick="closeReportManProdiv()" id="Button3" src="../../../Images/Button/Bottom_btn_close.jpg" />
                    <img id="Button4" onclick="removeAll4();" src="../../../Images/Button/Bottom_btn_del.jpg" />
                </td>
            </tr>
            <tr class="table-item">
                <td width="13%" height="20" bgcolor="#E7E7E7" align="right">
                    物品编号
                </td>
                <td width="20%" bgcolor="#FFFFFF">
                    <input id="myProNo44" type="text" style="width: 120px;" class="tdinput" maxlength="50" />
                </td>
                <td width="13%" bgcolor="#E7E7E7" align="right">
                    物品名称
                </td>
                <td width="20%" bgcolor="#FFFFFF">
                    <input id="myProductName44" class="tdinput" maxlength="50" type="text" />
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
                        onclick='TurnToPageReportManProu(1)' />&nbsp;
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1ReportManPro"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReportManPro('ProductName','ProductName6');return false;">
                            物品名称<span id="ProductName6" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReportManPro('ProNo','ProNo6');return false;">
                            物品编号<span id="ProNo6" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReportManPro('CodeName','CodeName6');return false;">
                            单位<span id="CodeName6" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReportManPro('ProductedCount','ProductedCount6');return false;">
                            生产数量<span id="ProductedCount6" class="orderTip"></span></div>
                    </th>
                    <%--                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick">
                            报检数量<span id="Span3" class="orderTip"></span></div>
                    </th>--%>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReportManPro('ApplyCheckCount','ApplyCheckCount6');return false;">
                            已报检数量<span id="ApplyCheckCount6" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByReportManPro('CheckedCount','CheckedCount6');return false;">
                            已检数量<span id="CheckedCount6" class="orderTip"></span></div>
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
                                <div id="pagecountReportManPro">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataList_PagerReportManPro" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divpageReportManPro">
                                    <input name="text" type="text" id="txt_pageReportPro" style="display: none" />
                                    <span id="pageDataList_TotalManPro"></span>每页显示
                                    <input name="text" type="text" id="ShowPageCountReportManPro1" size="5" />
                                    条 转到第
                                    <input name="text" type="text" id="ToPageReportManPro1" size="5" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexReportManPro($('#ShowPageCountReportManPro1').val(),$('#ToPageReportManPro1').val());" />
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
var popReportManProObj=new Object();

var pagecountReportManPro = 10;//每页计数
var totalRecordReportPro = 0;
var pagerStyleReportPro = "flickr";//jPagerBar样式
    
var currentPageIndexReportPro = 1;
var actionReportPro = "";//操作
var orderByReportPro = "";//排序字段
popReportManProObj.ShowManProList=function()
{
    document.getElementById('divReportManProduct').style.display='block';
    openRotoscopingDiv(false,'divBackShadow','BackShadowIframe');
    TurnToPageReportManProu(currentPageIndexReportPro);
}

    //jQuery-ajax获取JSON数据
    function TurnToPageReportManProu(pageIndexProduct)
    {
           var TheApplyNo=document.getElementById('ApplyID').value;
           var TheProductName=document.getElementById('myProductName44').value;
           var TheProductNo=document.getElementById('myProNo44').value;
           if(TheProductName!='')
            {
                if(strlen(TheProductName)>0)
                {
                    if(!CheckSpecialWord(TheProductName))
                    {
                       closeReportManProdiv();
                       popMsgObj.ShowMsg('物品名称不能含有特殊字符!');
                       return false;
                    }
                }
            }
            if(TheProductNo!='')
            {
                if(strlen(TheProductNo)>0)
                {
                    if(!CheckSpecialWord(TheProductNo))
                    {
                       closeReportManProdiv();
                       popMsgObj.ShowMsg('物品编号不能含有特殊字符!');
                       return false;
                    }
                }
            }
           currentPageIndexReportPro = pageIndexProduct;
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/ReportManProductInfo.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndexProduct+"&pageCount="+pagecountReportManPro+"&TheProductName="+escape(TheProductName)+"&TheProductNo="+escape(TheProductNo)+"&action="+actionReportPro+"&orderby="+orderByReportPro+"&ApplyNo="+TheApplyNo+"",//数据
           beforeSend:function(){$("#pageDataList_PagerReportManPro").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1ReportManPro tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioTech\" id=\"radioTech_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"Fun_FillManPro("+  item.ProductID+",'"+item.ProductName+"','"+item.ProNo+"','"+item.UnitID+"','"+item.CodeName+"','"+item.FromLineNo+"','"+item.ID+"','"+item.CheckCount+"','"+item.Specification+"');\" />"+"</td>"+
                        "<td height='22' align='center'>"+item.ProductName+"</td>"+
                        "<td height='22' align='center'>"+item.ProNo+"</td>"+
                        "<td height='22' align='center'>"+item.CodeName+"</td>"+
                         "<td height='22' align='center'>"+parseFloat(item.ProductedCount).toFixed(2)+"</td>"+
//                         "<td height='22' align='center'>"+parseFloat(item.CheckCount).toFixed(2)+"</td>"+
                         "<td height='22' align='center'>"+parseFloat(item.ApplyCheckCount).toFixed(2)+"</td>"+
                        "<td height='22' align='center'>"+parseFloat(item.CheckedCount).toFixed(2)+"</td>").appendTo($("#pageDataList1ReportManPro tbody"));

                   });
                    //页码
                    ShowPageBar("pageDataList_PagerReportManPro",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyleReport,mark:"pageDataListProductMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pagecountReportManPro,
                    currentPageIndex:pageIndexProduct,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"TurnToPageReportManProu({pageindex});return false;"}//[attr]
                    );
                  totalRecordReportPro = msg.totalCount;
                  $("#ShowPageCountReportManPro1").val(pagecountReportManPro);  
                  ShowTotalPage(msg.totalCount,pagecountReportManPro,pageIndexProduct);               
                  $("#ToPageReportManPro1").val(pageIndexProduct);
                  ShowTotalPage(msg.totalCount,pagecountReportManPro,pageIndexProduct,$("#pagecountReportManPro"));
                   
                  },
           error: function() {}, 
           complete:function(){$("#pageDataList_PagerReportManPro").show();IfshowReportPro(document.all["txt_pageReportPro"].value);pageDataList1ReportManPro("pageDataList1ReportManPro","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    
    
    function Fun_FillManPro(a,b,c,d,e,g,h,i,j)
    {
        document.getElementById('hiddenProID').value=a;
        document.getElementById('tbProName').value=b;
        document.getElementById('tbProNo').value=c;
        document.getElementById('hiddentbUnit').value=d;
        document.getElementById('tbUnit').value=e;
        document.getElementById('hiddenLineNo').value=g;
       // document.getElementById('hiddenApplyID').value=h;
        document.getElementById('FromDetailID').value=h;
        document.getElementById('mySpecification').value=j;
        document.getElementById('tbProductCount').value=FormatAfterDotNumber(parseFloat(i),selPoint);
        closeReportManProdiv();
        
        
    }
    function removeAll4()
    {
                document.getElementById('hiddenProID').value='0';
        document.getElementById('tbProName').value='';
        document.getElementById('tbProNo').value='';
        document.getElementById('hiddentbUnit').value='0';
        document.getElementById('tbUnit').value='';
        document.getElementById('hiddenLineNo').value='0';
        document.getElementById('mySpecification').value='';
       // document.getElementById('hiddenApplyID').value=h;
       document.getElementById('FromDetailID').value='0';
        document.getElementById('tbProductCount').value=FormatAfterDotNumber(0,selPoint);
        closeReportManProdiv();
    }
    //table行颜色
function pageDataList1ReportManPro(o,a,b,c,d){
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

function Fun_Search_ReportPro(aa)
{
      search="1";
      TurnToPage(1);
}
function IfshowReportPro(count)
    {
        if(count=="0")
        {
            document.all["divpageReportManPro"].style.display = "none";
            document.all["pagecountReportManPro"].style.display = "none";
        }
        else
        {
            document.all["divpageReportManPro"].style.display = "block";
            document.all["pagecountReportManPro"].style.display = "block";
        }
    }
    
    function SelectDept(retval)
    {
        alert(retval);
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndexReportManPro(newPageCount,newPageIndex)
    {
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecordReportPro-1)/newPageCount)+1 )
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            return false;
        }
        else
        {
            this.pagecountReportManPro=parseInt(newPageCount);
            TurnToPageReportManProu(parseInt(newPageIndex));
        }
    }
    //排序
    function orderByReportManPro(orderColum,orderTip)
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
        orderByReportPro = orderColum+"_"+ordering;
        TurnToPageReportManProu(1);
    }
 
function closeReportManProdiv()
{
    document.getElementById("divReportManProduct").style.display="none";
    document.getElementById('myProductName44').value='';
    document.getElementById('myProNo44').value='';
    closeRotoscopingDiv(false,'divBackShadow');

}


</script>

