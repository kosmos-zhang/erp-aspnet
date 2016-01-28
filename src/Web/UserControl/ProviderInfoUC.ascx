<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProviderInfoUC.ascx.cs" Inherits="UserControl_ProviderInfoUC" %>

<div id="divProviderInfoUC" style="border: solid 10px #93BCDD; background: #fff;
    padding: 10px; width: 600px; z-index: 1001; position: absolute; display: none;
    top: 50%; left: 70%; margin: 5px 0 0 -400px;">
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListProviderInfoUC"bgcolor="#999999">
        <tbody>
            <tr>
                <th height="20" align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF">选择</th>
                <th align="center" background="../../../images/Main/Table_bg.jpg" bgcolor="#FFFFFF"><div class="orderClick">物品编号<span id="Span1" class="orderTip"></span></div></th>
                
            </tr>
        </tbody>
    </table>
    <br />
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"class="PageList">
        <tr>
            <td height="28" background="../../../images/Main/PageList_bg.jpg">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                    <tr>
                        <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%"><div id="PageCountProviderInfoUC"></div></td>
                            <td height="28" align="right"><div id="pageDataList1_PagerProviderInfoUC" class="jPagerBar"></div></td>
                        <td height="28" align="right">
                            <div id="divpageProviderInfoUC">
                                <input name="TotalRecordProviderInfoUC" type="text" id="TotalRecordProviderInfoUC" style="display: none" />
                                <span id="TotalPageProviderInfoUC"></span>每页显示
                                <input name="PerPageCountProviderInfoUC" type="text" id="PerPageCountProviderInfoUC" />条 转到第
                                <input name="ToPageProviderInfoUC" type="text" id="ToPageProviderInfoUC" />页
                                <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#PerPageCountProviderInfoUC').val(),$('#ToPageProviderInfoUC').val());" />
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
var popProviderInfoUC = new Object();
//界面上的ID
popProviderInfoUC.objProviderID = null;
popProviderInfoUC.objProviderNo = null;
popProviderInfoUC.objProviderName = null;

//给界面的赋值
popProviderInfoUC.ProviderID = null;
popProviderInfoUC.ProviderNo = null;
popProviderInfoUC.ProviderName = null;

//分页等显示信息
popProviderInfoUC.pageCount = 10;
popProviderInfoUC.totalRecord = 0;
popProviderInfoUC.pagerStyle = "flickr";
popProviderInfoUC.currentPageIndex = 1;
popProviderInfoUC.action = "";
popProviderInfoUC.orderBy = "";

popProviderInfoUC.ShowList = function(objInputProviderID,objInputProviderNo,objInputProviderName)
{
    popProviderInfoUC.objProviderID = objInputProviderID;
    popProviderInfoUC.objProviderNo = objInputProviderNo;
    popProviderInfoUC.objProviderName = objInputProviderName;
    
    document.getElementById("divProviderInfoUC").style.display='block';
    
    popProviderInfoUC.TurnToPage(1)
}

popProviderInfoUC.CloseList = function()
{
    document.getElementById("divProviderInfoUC").style.display='none';
}


popProviderInfoUC.Fill = function(providerid,providerno,providername)
{
    document.getElementById(popProviderInfoUC.objProviderID).value = providerid;
    document.getElementById(popProviderInfoUC.objProviderNo).value = providerno;
    document.getElementById(popProviderInfoUC.objProviderName).value = providername;
    
    popProviderInfoUC.CloseList();
}

function ShowTotalPage212(totalcount,pagecount,pageindex,obj)
{
    var tempresult=Math.floor(totalcount/pagecount);
    var tempresult1=totalcount/pagecount;
    if(typeof(obj)!="undefined")
    {
        if(tempresult==tempresult1)
           obj.html("总记录数：<font color=red>"+totalcount+"</font>&nbsp;&nbsp;共分为：<font color=red>"+tempresult+"</font>&nbsp;页");
        else
        {
           var result=tempresult+1;
           obj.html("总记录数：<font color=red>"+totalcount+"</font>&nbsp;&nbsp;共分为：<font color=red>"+result+"</font>&nbsp;页");
        }
    }
    else
    {
        if(tempresult==tempresult1)
           $("#PageCount").html("总记录数：<font color=red>"+totalcount+"</font>&nbsp;&nbsp;共分为：<font color=red>"+tempresult+"</font>&nbsp;页");
        else
        {
           var result=tempresult+1;
           $("#PageCount").html("总记录数：<font color=red>"+totalcount+"</font>&nbsp;&nbsp;共分为：<font color=red>"+result+"</font>&nbsp;页");
        }
    }
}

popProviderInfoUC.TurnToPage = function(pageIndex)
{
    popProviderInfoUC.currentPageIndex = pageIndex;
    
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/UC/ProviderInfoUC.ashx',//目标地址
           cache:false,
           data: "pageIndex="+popProviderInfoUC.currentPageIndex+"&pageCount="+popProviderInfoUC.pageCount+"&action="+popProviderInfoUC.action+
                 "&orderby="+popProviderInfoUC.orderBy+"&ProviderID="+escape(popProviderInfoUC.ProviderID)+"&ProviderNo="+escape(popProviderInfoUC.ProviderNo)+
                 "&ProviderName="+escape(popProviderInfoUC.ProviderName)+"",//数据
           beforeSend:function(){$("#pageDataList1_PagerProviderInfoUC").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListProviderInfoUC tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item)
                    {
                        if(item.ProductID != null && item.ProductID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+" <input type=\"radio\" name=\"radioTech\" id=\"radioTech_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"popProviderInfoUC.Fill("+  item.ProductID+",'"+item.ProductNo+"','"+item.ProductName+"');\" />"+"</td>"+
                        "<td height='22' align='center'>" + item.ProductNo + "</td>"+
                        "<td height='22' align='center'>"+item.ProductName+"</td>").appendTo($("#pageDataListProviderInfoUC tbody"));
                   });
                    //页码
                   ShowPageBar("pageDataList1_PagerProviderInfoUC",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:popProviderInfoUC.pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:popProviderInfoUC.pageCount,pageIndex:popProviderInfoUC.currentPageIndex,noRecordTip:"没有符合条件的记录",preWord:"上页",nextWord:"下页",First:"首页",End:"末页",
                    onclick:"popProviderInfoUC.TurnToPage({pageindex});return false;"}//[attr]
                    );
                 // totalRecord = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                 // document.all["TotalRecordProviderInfoUC"].value=msg.totalCount;
                 // $("#TotalPageProviderInfoUC").val(popProviderInfoUC.pageCount);
                 // ShowTotalPage212(msg.totalCount,popProviderInfoUC.pageCount,popProviderInfoUC.currentPageIndex,$("#TotalRecordProviderInfoUC"));
                  //ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                 // $("#ToPageProviderInfoUC").val(pageIndex);
                  },
           error: function() {
}
           //complete:function(){$("#pageDataList1_PagerProviderInfoUC").show();Ifshow(document.all["TotalRecordProviderInfoUC"].value);pageDataList1("pageDataList1_PagerProviderInfoUC","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
}
</script>