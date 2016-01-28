<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportProductInfo.ascx.cs"
    Inherits="UserControl_ReportProductInfo" %>
<div id="layout">
    <!--提示信息弹出详情start-->
    <div id="divReportProduct" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
        width: 800px; z-index: 1001; position: absolute; display: none; top: 50%; left: 70%;
        margin: -115px 0 0 -700px;">
        <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
            class="table">
            <tr class="table-item">
                <td height="25" colspan="6" bgcolor="White" align="left" style="width: 50%;">
                    <img onclick="closeReportProdiv()" id="Button1" src="../../../Images/Button/Bottom_btn_close.jpg" />
                     <img id="Button2" onclick="removeAll2();" src="../../../Images/Button/Bottom_btn_del.jpg" />
                </td>
  
            </tr>
            <tr class="table-item">
                <td width="13%" height="20" bgcolor="#E7E7E7" align="right">
                    物品编号
                </td>
                <td width="20%" bgcolor="#FFFFFF">
                    <input id="myProNo77" type="text" style="width: 120px;" class="tdinput" maxlength="50" />
                </td>
                <td width="13%" bgcolor="#E7E7E7" align="right">
                    物品名称
                </td>
                <td width="20%" bgcolor="#FFFFFF">
                    <input id="myProductName77" class="tdinput" maxlength="50" type="text" />
                </td>
                <td width="13%" bgcolor="#E7E7E7" align="right">
                </td>
                <td width="21%" bgcolor="#FFFFFF">
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center" bgcolor="#FFFFFF">
                    <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: pointer;'
                        onclick='TurnToPageReportCheckPro(1)' />&nbsp;
                </td>
            </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1ReportPro"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        选择
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByAppReportPro('ProductName','ProductName7');return false;">
                            物品名称<span id="ProductName7" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByAppReportPro('ProNo','ProNo7');return false;">
                            物品编号<span id="ProNo7" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByAppReportPro('CodeName','CodeName7');return false;">
                            单位<span id="CodeName7" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByAppReportPro('CheckMode','CheckMode7');return false;">
                            检验方式<span id="CheckMode7" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByAppReportPro('ProductCount','ProductCount7');return false;">
                            报检数量<span id="ProductCount7" class="orderTip"></span></div>
                    </th>
                    <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                        <div class="orderClick" onclick="orderByAppReportPro('CheckedCount','CheckedCount7');return false;">
                            已检数量<span id="CheckedCount7" class="orderTip"></span></div>
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
                                <div id="pagecountReportPro">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataList_PagerReportPro" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divpageReportPro">
                                    <input name="text" type="text" id="txt_pageReportPro" style="display: none" />
                                    <span id="pageDataList_TotalProduct"></span>每页显示
                                    <input name="text" type="text" id="ShowPageCountReportPro" size="5" />
                                    条 转到第
                                    <input name="text" type="text" id="ToPageReportPro" size="5" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexPro($('#ShowPageCountReportPro').val(),$('#ToPageReportPro').val());" />
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
var popReportProObj=new Object();
popReportProObj.ShowProList=function()
{

   // var myApplyNo=document.getElementById('hiddenApplyNo').value;
 
//    var theMyFromType=document.getElementById('sltFromType').value;
//    var myUrl='';
//    if(theMyFromType=='1')     //质检申请单
//    {
//        myUrl='ReportProduct.ashx';
//    }
//    if(theMyFromType=='3')   //生产任务单
//    {
//        myUrl='ReportManProductInfo.ashx';
//    }
    document.getElementById('divReportProduct').style.display='block';
        openRotoscopingDiv(false,'divBackShadow','BackShadowIframe');
    TurnToPageReportCheckPro(currentPageIndexReportPro);
}

    var pagecountReportPro = 10;//每页计数
    var totalRecordReportPro = 0;
    var pagerStyleReportPro = "flickr";//jPagerBar样式
    
    var currentPageIndexReportPro = 1;
    var actionReportPro = "";//操作
    var orderByReportPro = "";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPageReportCheckPro(pageIndexProduct)
    {

           var TheApplyNo=document.getElementById('ApplyID').value;
           var TheProNo=document.getElementById('myProNo77').value;
           var TheProductName=document.getElementById('myProductName77').value;
           if(TheProductName!='')           
            {
                if(strlen(TheProductName)>0)
                {
                    if(!CheckSpecialWord(TheProductName))
                    {
                       closeReportProdiv();
                       popMsgObj.ShowMsg('物品名称不能含有特殊字符!');
                       return false;
                    }
                }
            }
            if(TheProNo!='')
            {
                if(strlen(TheProNo)>0)
                {
                    if(!CheckSpecialWord(TheProNo))
                    {
                       closeReportProdiv();
                       popMsgObj.ShowMsg('物品编号不能含有特殊字符!');
                       return false;
                    }
                }
            }
           currentPageIndexReportPro = pageIndexProduct;
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/ReportProduct.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndexProduct+"&pageCount="+pagecountReportPro+"&TheProNo="+escape(TheProNo)+"&TheProductName="+escape(TheProductName)+"&action="+actionReportPro+"&orderby="+orderByReportPro+"&ApplyNo="+TheApplyNo+"",//数据
           beforeSend:function(){$("#pageDataList_PagerReportPro").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1ReportPro tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioTech\" id=\"radioTech_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"Fun_FillPro("+  item.ProductID+",'"+item.ProductName+"','"+item.ProNo+"','"+item.UnitID+"','"+item.CodeName+"','"+item.ProductCount+"','"+item.ID+"','"+item.FromLineNo+"','"+item.CheckModeID+"','"+item.CheckedCount+"','"+item.Specification+"');\" />"+"</td>"+
                        "<td height='22' align='center'>"+item.ProductName+"</td>"+
                        "<td height='22' align='center'>"+item.ProNo+"</td>"+
                        "<td height='22' align='center'>"+item.CodeName+"</td>"+
                        "<td height='22' align='center'>"+item.CheckMode+"</td>"+
                         "<td height='22' align='center'>"+parseFloat(item.ProductCount).toFixed(2)+"</td>"+
                        "<td height='22' align='center'>"+parseFloat(item.CheckedCount).toFixed(2)+"</td>").appendTo($("#pageDataList1ReportPro tbody"));

                   });                
                                      //页码
                    ShowPageBar("pageDataList_PagerReportPro",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyleReport,mark:"pageDataListProductMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pagecountReportPro,
                    currentPageIndex:pageIndexProduct,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"TurnToPageReportCheckPro({pageindex});return false;"}//[attr]
                    );
                  totalRecordReportPro = msg.totalCount;
                  $("#ShowPageCountReportPro").val(pagecountReportPro);  
                  ShowTotalPage(msg.totalCount,pagecountReportPro,currentPageIndexReportPro);               
                  $("#ToPageReportPro").val(pageIndexProduct);
                  ShowTotalPage(msg.totalCount,pagecountReportPro,currentPageIndexReportPro,$("#pagecountReportPro"));                 
                  
                  },
           error: function() {}, 
           complete:function(){$("#pageDataList_PagerReportPro").show();IfshowReportPro(document.all["txt_pageReportPro"].value);pageDataList1ReportPro("pageDataList1ReportPro","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    
    function ChangePageCountIndexPro(newPageCount,newPageIndex)
    {
       if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecordReportPro -1)/newPageCount)+1 )
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            return false;
        }
        else
        {
            this.pagecountReportPro=parseInt(newPageCount);
            TurnToPageReportCheckPro(parseInt(newPageIndex));
       }
    }
    function removeAll2()
    {
        document.getElementById('hiddenProID').value='0';
        document.getElementById('tbProName').value='';
        document.getElementById('tbProNo').value='';
        document.getElementById('hiddentbUnit').value='0';
        document.getElementById('tbUnit').value='';
        document.getElementById('tbProductCount').value=FormatAfterDotNumber(0,selPoint);
        document.getElementById('FromDetailID').value='0';
        document.getElementById('hiddenLineNo').value='0';
        //document.getElementById('ddlCheckMode').value='1';
        document.getElementById('mySpecification').value='';
        closeReportProdiv();
    }
    function Fun_FillPro(a,b,c,d,e,f,g,h,i,j,k)
    {
        document.getElementById('hiddenProID').value=a;
        document.getElementById('tbProName').value=b;
        document.getElementById('tbProNo').value=c;
        document.getElementById('hiddentbUnit').value=d;
        document.getElementById('tbUnit').value=e;
        document.getElementById('tbProductCount').value=FormatAfterDotNumber(parseFloat(f-j),selPoint);
        document.getElementById('FromDetailID').value=g;
        document.getElementById('hiddenLineNo').value=h;
        //document.getElementById('ddlCheckMode').value=i;
        document.getElementById('mySpecification').value=k;
        closeReportProdiv();
        
        
    }
    //table行颜色
function pageDataList1ReportPro(o,a,b,c,d){
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
            document.all["divpageReportPro"].style.display = "none";
            document.all["pagecountReportPro"].style.display = "none";
        }
        else
        {
            document.all["divpageReportPro"].style.display = "block";
            document.all["pagecountReportPro"].style.display = "block";
        }
    }
    
    function SelectDept(retval)
    {
        alert(retval);
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndexReportPro(newPageCount,newPageIndex)
    {
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
        {
            //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            return false;
        }
        else
        {
            this.pagecountReportPro=parseInt(newPageCount);
            TurnToPageReportCheckPro(parseInt(newPageIndex));
        }
    }
    //排序
    function orderByAppReportPro(orderColum,orderTip)
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
        TurnToPageReportCheckPro(1);
    }
 
function closeReportProdiv()
{
    document.getElementById("divReportProduct").style.display="none";
    document.getElementById('myProNo77').value='';
    document.getElementById('myProductName77').value='';
    closeRotoscopingDiv(false,'divBackShadow');

}


</script>

