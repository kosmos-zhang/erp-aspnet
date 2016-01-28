$(document).ready(function(){

      fnGetExtAttr();//物品控件拓展属性
    });  
    /*
* 查询
*/
function DoSearch(currPage)
{
   //$("#btnPrint").css("display", "inline"); 
   var fieldText = "";
   var msgText = "";
   var isFlag = true;

           var txtProductNo=$("#txtProductNo").val();
           var txtProductName=$("#txtProductName").val();
           if(txtProductNo=="")
            {
                isFlag=false;
                fieldText=fieldText + "物品编号|";
                msgText =msgText+"请选择物品|";
            }
           if(!isFlag)
           {
                popMsgObj.Show(fieldText,msgText);
                return;
           }
           
           var UrlParam="&action="+action+
           "&txtProductNo="+escape(txtProductNo)+
           "&txtProductName="+escape(txtProductName);
    
    //设置检索条件
    document.getElementById("hidSearchCondition").value = UrlParam;
    if (currPage == null || typeof(currPage) == "undefined")
    {
        TurnToPage0(1);
    }
    else
    {
        TurnToPage0(parseInt(currPage,10));
    }
}
 /*-----------------------------------------DoSearch结束----------------------------------------------*/
 
 
    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var action = "";//操作
    var orderBy = "";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPage0(pageIndex)
    {
           currentPageIndex = pageIndex;
           //获取查询条件
           var searchCondition = document.getElementById("hidSearchCondition").value;
           
           var UrlParam =searchCondition;
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/OperatingModel/StorageManager/StockProductAnalysis.ashx',//目标地址
           data:UrlParam,
           cache:false,
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    $("#pageDataList2 tbody").find("tr.newrow").remove();
                    $("#pageDataList3 tbody").find("tr.newrow").remove();
                    $("#pageDataList4 tbody").find("tr.newrow").remove();
                    $("#pageDataList5 tbody").find("tr.newrow").remove();
                    //alert(msg.DateSellandPurchase[0].CountTotal)
                    if (msg.DateSellandPurchase.length != 0)
                    {
                        $("#btnPrint").css("display", "inline");
                        $.each(msg.DateSellandPurchase,function(i,item){
                            if(item.type != null)
                            {
                                $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+ NumRound(item.CountTotal,4)+"</a></td>"+
                                "<td height='22' align='center'>"+ NumRound(item.TaxTotalPrice,4) +"</td>"+
                                "<td height='22' align='center'>"+NumRound(item.TotalPrice,4)+"</td>"+
                                "<td height='22' align='center'>"+NumRound(item.TaxTotalPric,4)+"</td>"+
                                "<td height='22' align='center'>"+item.type+"</td>").appendTo($("#pageDataList1 tbody"));
                                
                            }
                          });
                      }
                    if (msg.DateStockandPrice.length != 0)
                    {
                        $("#btnPrint").css("display", "inline");
                        $.each(msg.DateStockandPrice,function(i,item){
                            if(item.ProductCount != null)
                            {
                                $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+ NumRound(item.ProductCount,4) +"</a></td>"+
                                "<td height='22' align='center'>"+ NumRound(item.TotalPrice,4) +"</td>"+
                                "<td height='22' align='center'>"+NumRound(item.TaxPrice,4)+"</td>"+
                                "<td height='22' align='center'>"+NumRound(item.TaxRate,4)+"</td>"+
                                "<td height='22' align='center'>"+NumRound(item.MaxStockNum,4)+"</td>"+
                                "<td height='22' align='center'>"+NumRound(item.MinStockNum,4)+"</td>").appendTo($("#pageDataList2 tbody"));
                                
                                $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+ NumRound(item.StandardBuy,4) +"</a></td>"+
                                "<td height='22' align='center'>"+ NumRound(item.TaxBuy,4) +"</td>"+
                                "<td height='22' align='center'>"+NumRound(item.StandardCost,4)+"</td>"+
                                "<td height='22' align='center'>"+NumRound(item.SellTax,4)+"</td>"+
                                "<td height='22' align='center'>"+NumRound(item.StandardSell,4)+"</td>"+
                                "<td height='22' align='center'>"+NumRound(item.SellPrice,4)+"</td>").appendTo($("#pageDataList3 tbody"));
                                
                            }
                            });
                      }
                     
                    if (msg.DateLoss.length != 0)
                    {
                        $("#btnPrint").css("display", "inline");
                        $.each(msg.DateLoss,function(i,item){
                            if(item.TotalCount != null)
                            {
                                $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+NumRound(item.TotalCount,4) +"</a></td>"+
                                "<td height='22' align='center'>"+NumRound(item.TotalPrice,4) +"</td>"+
                                "<td height='22' align='center'>"+NumRound(item.LossCount,4)+"</td>"+
                                "<td height='22' align='center'>"+NumRound(item.LossTotalPrice,4)+"</td>").appendTo($("#pageDataList4 tbody"));
                                
                            }
                            });
                      }
                      
                    if (msg.DateStockInfo.length != 0)
                    {
                        $("#btnPrint").css("display", "inline");
                        $.each(msg.DateStockInfo,function(i,item){
                            if(item.ProductCount != null)
                            {
                                $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+item.StorageName +"</a></td>"+
                                "<td height='22' align='center'>"+NumRound(item.ProductCount,4) +"</td>"+
                                "<td height='22' align='center'>"+NumRound(item.StandardCost,4)+"</td>"+
                                "<td height='22' align='center'>"+NumRound(item.TotalPrice,4)+"</td>"+
                                "<td height='22' align='center'>"+NumRound(item.MaxStockNum,4)+"</td>"+
                                "<td height='22' align='center'>"+NumRound(item.MinStockNum,4)+"</td>").appendTo($("#pageDataList5 tbody"));
                                
                            }
                            });
                      }
                      
                  },
           error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
           complete:function(){hidePopup();pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");
                              pageDataList1("pageDataList2","#E7E7E7","#FFFFFF","#cfc","cfc");
                              pageDataList1("pageDataList3","#E7E7E7","#FFFFFF","#cfc","cfc");
                              pageDataList1("pageDataList4","#E7E7E7","#FFFFFF","#cfc","cfc");
                              pageDataList1("pageDataList5","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }


function Ifshow(count)
    {
        if(count=="0")
        {
            document.getElementById("divpage").style.display = "none";
            document.getElementById("pagecount").style.display = "none";
        }
        else
        {
            document.getElementById("divpage").style.display = "block";
            document.getElementById("pagecount").style.display = "block";
        }
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndex(newPageCount,newPageIndex)
    {
    
        
            //判断是否是数字
        if (!PositiveInteger(newPageCount))
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
            return;
        }
        if (!PositiveInteger(newPageIndex))
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
            return;
        }
    
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            return false;
        }
        else
        {
            this.pageCount=parseInt(newPageCount,10);
            TurnToPage0(parseInt(newPageIndex,10));
        }
    }
    //排序
    var ordering = "a";
    function OrderBy(orderColum,orderTip)
    {
        ordering = "a";
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
        DoSearch();
    }



//打印
function fnPrint() {
    if(ordering=="a")
    {
        ordering="asc";
    }
    else
    {
        ordering="desc"
    }
    orderBy=orderBy.split('_')[0];
    var strRul = $("#hidSearchCondition").val();
    window.open("PrintStockProductAnalysis.aspx?orderby="+orderBy+"&ordering="+ordering+"&" + strRul);
}

//物品控件
function Fun_FillParent_Content(id,ProNo,ProdName)
{
   document.getElementById('txtProductNo').value=ProNo;
   document.getElementById('txtProductName').value=ProdName;
}

//转向时带上条件---To购进明细
function fnGoInDetail()
{
    var UrlParam="txtProductNo="+escape($("#txtProductNo").val())+
   "&txtProductName="+escape($("#txtProductName").val());
    window.location.href= "StockProductInDetail.aspx?"+ UrlParam;
}

//转向时带上条件---To销售明细
function fnGoSellDetail()
{
    var UrlParam="txtProductNo="+escape($("#txtProductNo").val())+
   "&txtProductName="+escape($("#txtProductName").val());
    window.location.href= "StockProductSellDetail.aspx?"+ UrlParam;
}