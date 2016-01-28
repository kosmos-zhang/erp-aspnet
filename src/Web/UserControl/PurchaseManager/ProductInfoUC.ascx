<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductInfoUC.ascx.cs" Inherits="UserControl_PurchaseManager_ProductInfoUC" %>
<div id="divProductInfoUC" style="border: solid 10px #93BCDD; background: #fff; padding: 10px; width: 600px; z-index: 1001; position: absolute; display: none;top: 50%; left: 70%; margin: 5px 0 0 -400px;">
    <table width="100%"><tr><td><a onclick="popProductInfoObj.CloseList()" style="text-align:right; cursor:pointer">关闭</a></td></tr></table>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1Provider"bgcolor="#999999">
        <tbody>
            <tr>
                <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">选择</th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick">物品编号<span id="Span2" class="orderTip"></span></div></th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick">物品名称<span id="oC2" class="orderTip"></span></div></th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick">规格<span id="Span1" class="orderTip"></span></div></th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick">单位<span id="Span3" class="orderTip"></span></div></th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick">计划采购单价<span id="Span4" class="orderTip"></span></div></th>
               
            </tr>
        </tbody>
    </table>
    <br />
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"class="PageList">
        <tr>
            <td height="28" background="../../../images/Main/PageList_bg.jpg">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                    <tr>
                        <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                            <%--总页数，当前第几页--%>
                            <div id="PageCountProductInfoUC"></div>
                        </td>
                        <td height="28" align="right">
                        <%--前一页后一页--%>
                            <div id="PreBackPage" class="jPagerBar"></div>
                        </td>
                        <td height="28" align="right">
                            <div id="divPerPageProductInfoUC">
                                <input name="text" type="text" id="Text2Provider" style="display: none" />
                                <span id="TotalRecordProductInfoUC"></span>每页显示
                                <input name="PerPageCountProductInfoUC" type="text" id="PerPageCountProductInfoUC" />条 转到第
                                <input name="ToPageProductInfoUC" type="text" id="ToPageProductInfoUC" />页
                                <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCountProvider').val(),$('#ToPageProvider').val());" />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>

<script type="text/javascript">
var popProductInfoObj = new Object();

popProductInfoObj.ProductID = null;
popProductInfoObj.ProductNo = null;
popProductInfoObj.ProductName = null;
popProductInfoObj.Specification = null;
popProductInfoObj.UnitID = null;
popProductInfoObj.UnitName = null;
popProductInfoObj.UnitPrice = null;


//分页等显示信息
popProductInfoObj.pageCount = 10;
popProductInfoObj.totalRecord = 0;
popProductInfoObj.pagerStyle = "flickr";
popProductInfoObj.pageIndex = 1;
popProductInfoObj.action = "";
popProductInfoObj.orderBy = "";

//popProductInfoObj.ShowList = function()
//{
//    alert(123)
//}


popProductInfoObj.ShowList = function(objProductID,objProducrNo,objProductName,objSpecification,objUnitID,objUnitName,objUnitPrice)
{
    popProductInfoObj.ProductID = objProductID;
    popProductInfoObj.ProductNo = objProducrNo;
    popProductInfoObj.ProductName = objProductName;
    popProductInfoObj.Specification = objSpecification;
    popProductInfoObj.UnitID = objUnitID;
    popProductInfoObj.UnitName = objUnitName;
    popProductInfoObj.UnitPrice = objUnitPrice;
    popProductInfoObj.TurnToPage(1);
    document.getElementById('divProductInfoUC').style.display='block';
    
}

popProductInfoObj.CloseList = function()
{
    document.getElementById('divProductInfoUC').style.display='none';
}

popProductInfoObj.Fill = function(productid,productno,productname,specification,unitid,unitname,unitprice)
{
    document.getElementById(popProductInfoObj.ProductID).value = productid;
    document.getElementById(popProductInfoObj.ProductNo).value = productno;
    document.getElementById(popProductInfoObj.ProductName).value = productname;
    document.getElementById(popProductInfoObj.Specification).value = specification;
    document.getElementById(popProductInfoObj.UnitID).value = unitid;
    document.getElementById(popProductInfoObj.UnitName).value = unitname;
    document.getElementById(popProductInfoObj.UnitPrice).value = unitprice;
document.getElementById('divProductInfoUC').style.display='none';
}

popProductInfoObj.TurnToPage = function(pageIndex)
{
    popProductInfoObj.pageIndex = pageIndex;
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/UC/ProductInfoUC.ashx',//目标地址
           cache:false,
           data: "pageIndex="+popProductInfoObj.pageIndex+"&pageCount="+popProductInfoObj.pageCount+"&action="+popProductInfoObj.action+
                 "&orderby="+popProductInfoObj.orderBy+"&ProviderID="+escape(popProductInfoObj.ProviderID)+"&ProviderNo="+escape(popProductInfoObj.ProviderNo)+
                 "&ProviderName="+escape(popProductInfoObj.ProviderName)+"",//数据
           beforeSend:function(){$("#pageDataList1_PagerProductInfoUC").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1Provider tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item)
                    {
                        if(item.ProductID != null && item.ProductID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioTech\" id=\"radioTech_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"popProductInfoObj.Fill('"+item.ProductID+"','"+item.ProductNo+"','"+item.ProductName+"','"+item.Specification+"','"+item.UnitID+"','"+item.UnitName+"','"+item.UnitPrice+"');\" />"+"</td>"+
                        "<td height='22' align='center'>" + item.ProductNo + "</td>"+
                        "<td height='22' align='center'>" + item.ProductName + "</td>"+
                        "<td height='22' align='center'>" + item.Specification + "</td>"+
                        "<td height='22' align='center'>" + item.UnitName + "</td>"+
                        "<td height='22' align='center'>" + item.UnitPrice + "</td>").appendTo($("#pageDataList1Provider tbody"));
                   });
                    //页码
                   ShowPageBar(
                    "divPageClickInfo",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {
                        style:popProductInfoObj.pagerStyle,
                        mark:"DetailListMark",
                        totalCount:msg.totalCount,
                        showPageNumber:3,
                        pageCount:popProductInfoObj.pageCount,
                        pageIndex:popProductInfoObj.pageIndex,
                        noRecordTip:"没有符合条件的记录",
                        preWord:"上一页",
                        nextWord:"下一页",
                        First:"首页",
                        End:"末页",
                        onclick:"popProductInfoObj.TurnToPage({pageindex});return false;"
                    });
                    popProductInfoObj.totalRecord = msg.totalCount;
                    $("#divPerPageProductInfoUC").val(popProductInfoObj.pageCount);
                    ShowTotalPage(msg.totalCount, popProductInfoObj.pageCount, popProductInfoObj.pageIndex, $("#divPerPageProductInfoUC"));
                    $("#ToPageProductInfoUC").val(pageIndex);
                  },
           error: function() {}
           //complete:function(){$("#pageDataList1_PagerProductInfoUC").show();Ifshow(document.all["TotalRecordProductInfoUC"].value);pageDataList1("pageDataList1_PagerProductInfoUC","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
}
</script>