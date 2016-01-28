<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductInfoUC.ascx.cs"
    Inherits="UserControl_ProductInfoUC" %>
<div id="div1ProductInfoUC" style="border: solid 10px #93BCDD; background: #fff;
    padding: 10px; width: 800px; z-index: 1001; position: absolute; display: none;
    top: 50%; left: 45%; margin: 5px 0 0 -400px;">
    <table width="100%">
        <tr>
            <td>
                <%--<a onclick="closeProductInfoUCdiv()" style="text-align: right; cursor: pointer">关闭</a>--%>
                <img alt="关闭"  src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: hand;'
                        onclick='closeProductInfoUCdiv();' />
            </td>
        </tr>
    </table>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListProductInfoUC"
        bgcolor="#999999">
        <tbody>
            <tr>
                <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    选择
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick">
                        物品编号<span id="Span1" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick">
                        物品名称<span id="Span2" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick">
                        规格<span id="Span3" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick">
                        单位<span id="Span4" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick">
                        单价<span id="Span5" class="orderTip"></span></div>
                </th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">
                    <div class="orderClick">
                        采购含税价<span id="Span6" class="orderTip"></span></div>
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
                            <div id="PageCountProductInfoUC">
                            </div>
                        </td>
                        <td height="28" align="right">
                            <div id="pageDataList1_PagerProductInfoUC" class="jPagerBar">
                            </div>
                        </td>
                        <td height="28" align="right">
                            <div id="divpageProductInfoUC">
                                <input name="TotalRecordProductInfoUC" type="text" id="TotalRecordProductInfoUC"
                                    style="display: none" />
                                <span id="TotalPageProductInfoUC"></span>每页显示
                                <input name="PerPageCountProductInfoUC" type="text" id="PerPageCountProductInfoUC"
                                    style="width: 20px" />条 转到第
                                <input name="ToPageProductInfoUC" type="text" id="ToPageProductInfoUC" style="width: 22px" />页
                                <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                    width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexProduct($('#PerPageCountProductInfoUC').val(),$('#ToPageProductInfoUC').val());" />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
</div>

<script type="text/javascript">
var popProductInfoUC = new Object();
//界面上的ID
popProductInfoUC.objProductID = null;
popProductInfoUC.objProductNo = null;
popProductInfoUC.objProductName = null;
popProductInfoUC.objSpecification = null;
popProductInfoUC.objUnitID = null;
popProductInfoUC.objUnitName = null;
popProductInfoUC.objUnitPrice = null;
popProductInfoUC.objStandardBuy = null;
popProductInfoUC.objStandardBuyHide = null;

//分页等显示信息
popProductInfoUC.pageCount = 10;
popProductInfoUC.totalRecord = 0;
popProductInfoUC.pagerStyle = "flickr";
popProductInfoUC.currentPageIndex = 1;
popProductInfoUC.action = "";
popProductInfoUC.orderBy = "";

popProductInfoUC.ShowList = function(objInputProductID,objInputProductNo,objInputProductName,objInputSpecification,objInputUnitID,objInputUnitName,objInputUnitPrice,objInputStandardBuy,objInputStandardBuyHide)
{
    popProductInfoUC.objProductID = objInputProductID;
    popProductInfoUC.objProductNo = objInputProductNo;
    popProductInfoUC.objProductName = objInputProductName;
    popProductInfoUC.objSpecification = objInputSpecification;
    popProductInfoUC.objUnitID = objInputUnitID;
    popProductInfoUC.objUnitName = objInputUnitName;
    popProductInfoUC.objUnitPrice = objInputUnitPrice;
    popProductInfoUC.objStandardBuy = objInputStandardBuy;
    popProductInfoUC.objStandardBuyHide = objInputStandardBuyHide;
    document.getElementById("div1ProductInfoUC").style.display='block';
    
    popProductInfoUC.TurnToPage(1)
}

popProductInfoUC.CloseList = function()
{
    document.getElementById("div1ProductInfoUC").style.display='none';
}


popProductInfoUC.Fill = function(Productid,Productno,Productname,Specification,UnitID,UnitName,UnitPrice,StandardBuy)
{
    document.getElementById(popProductInfoUC.objProductID).value = Productid;
    document.getElementById(popProductInfoUC.objProductNo).value = Productno;
    document.getElementById(popProductInfoUC.objProductName).value = Productname;
    if(popProductInfoUC.objSpecification!=undefined)
    {
        document.getElementById(popProductInfoUC.objSpecification).value = Specification;
    }
    if(popProductInfoUC.objUnitID!=undefined)
    {
        document.getElementById(popProductInfoUC.objUnitID).value = UnitID;
    }
    if(popProductInfoUC.objUnitName!=undefined)
    {
        document.getElementById(popProductInfoUC.objUnitName).value = UnitName;
    }
    if(popProductInfoUC.objUnitPrice!=undefined)
    {
        document.getElementById(popProductInfoUC.objUnitPrice).value = FormatAfterDotNumber(UnitPrice,2);
    }
    if(popProductInfoUC.objStandardBuy !=undefined)
    {
        document.getElementById(popProductInfoUC.objStandardBuy).value = FormatAfterDotNumber(StandardBuy,2);
    }
    if(popProductInfoUC.objStandardBuyHide !=undefined)
    {
        document.getElementById(popProductInfoUC.objStandardBuyHide).value = FormatAfterDotNumber(StandardBuy,2);
    }
    popProductInfoUC.CloseList();
    fnMergeDetail();
}

popProductInfoUC.Ifshow=function (count)
{
    if(count=="0")
    {
        document.getElementById("divpageProductInfoUC").style.display = "none";
        document.getElementById("TotalPageProductInfoUC").style.display = "none";
    }
    else
    {
        document.getElementById("divpageProductInfoUC").style.display = "block";
        document.getElementById("TotalPageProductInfoUC").style.display = "block";
    }
}

function ChangePageCountIndexProduct(newPageCountProvider,newPageIndexProvider)
{
    if(newPageCountProvider <=0 || newPageIndexProvider <= 0 ||  newPageIndexProvider  > ((popProductInfoUC.totalRecord-1)/newPageCountProvider)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
    ifshow="0";
        popProductInfoUC.pageCount=parseInt(newPageCountProvider);
        TurnToPageProvider(parseInt(newPageIndexProvider));
    }
}

function closeProductInfoUCdiv()
{
    popProductInfoUC.CloseList();
}

function pageProductInfoUCDataList1(o,a,b,c,d)
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

popProductInfoUC.TurnToPage = function(pageIndex)
{
    popProductInfoUC.currentPageIndex = pageIndex;
    
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/UC/ProductInfoUC.ashx',//目标地址
           cache:false,
           data: "pageIndex="+popProductInfoUC.currentPageIndex+"&pageCount="+popProductInfoUC.pageCount+"&action="+popProductInfoUC.action+
                 "&orderby="+popProductInfoUC.orderBy+"&ProductID="+escape(popProductInfoUC.ProductID)+"&ProductNo="+escape(popProductInfoUC.ProductNo)+
                 "&ProductName="+escape(popProductInfoUC.ProductName)+"",//数据
           beforeSend:function(){$("#pageDataList1_PagerProductInfoUC").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListProductInfoUC tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item)
                    {
                        if(item.ProductID != null && item.ProductID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioTech\" id=\"radioTech_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"popProductInfoUC.Fill("+  item.ProductID+",'"+item.ProductNo+"','"+item.ProductName+"','"+item.Specification+"','"+item.UnitID+"','"+item.UnitName+"','"+item.UnitPrice+"','"+item.StandardBuy+"');\" />"+"</td>"+
                        "<td height='22' align='center'>" + item.ProductNo + "</td>"+
                        "<td height='22' align='center'>" + item.ProductName + "</td>"+
                        "<td height='22' align='center'>" + item.Specification + "</td>"+
                        "<td height='22' align='center'>" + item.UnitName + "</td>"+
                        "<td height='22' align='center'>" + FormatAfterDotNumber(item.UnitPrice,2) + "</td>"+
                        "<td height='22' align='center'>"+FormatAfterDotNumber(item.StandardBuy,2)+"</td>").appendTo($("#pageDataListProductInfoUC tbody"));
                   });
                    //页码
                   ShowPageBar("pageDataList1_PagerProductInfoUC",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:popProductInfoUC.pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount
                    ,showPageNumber:3
                    ,pageCount:popProductInfoUC.pageCount
                    ,currentPageIndex:popProductInfoUC.currentPageIndex
                    ,noRecordTip:"没有符合条件的记录"
                    ,preWord:"上页"
                    ,nextWord:"下页",First:"首页",End:"末页",
                    onclick:"popProductInfoUC.TurnToPage({pageindex});return false;"}//[attr]
                    );
                 popProductInfoUC.totalRecord = msg.totalCount;
                  document.getElementById("TotalRecordProductInfoUC").value=msg.totalCount;
                  $("#PerPageCountProductInfoUC").val(popProductInfoUC.pageCount);                  
                 ShowTotalPage(msg.totalCount,popProductInfoUC.pageCount,popProductInfoUC.pageIndex,$("#TotalPageProductInfoUC"));
                $("#ToPageProductInfoUC").val(pageIndex);
                  },
           error: function() {
},
           complete:function(){$("#pageDataList1_PagerProductInfoUC").show();popProductInfoUC.Ifshow(document.getElementById("TotalRecordProductInfoUC").value);pageProductInfoUCDataList1("pageDataListProductInfoUC","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
}
</script>

