var pageCount = 10;//每页计数
 var totalRecord = 0;
 var pagerStyle = "flickr";//jPagerBar样式
 var currentPageIndex = 1;
 
/*载入*/
window.onload=function()
{
    if(document.getElementById("txtIsSearch").value!="")
    {
        pageCount=parseInt(document.getElementById("txtPageSize").value);
        orderBy=document.getElementById("txtOrderBy").value;
        GoPage(parseInt(document.getElementById("txtPageIndex").value));
    }
}

/*重写toFixed*/
Number.prototype.toFixed = function(d) {
    return FormatAfterDotNumber(this, selPoint);
}

// 查询数据
function SearchData()
{
    if(!fnCheck())
    {
        return;
    }
    GoPage(currentPageIndex);
}



function getReportImage(t)
{
        var sumType=$("#selSumType").val();
        var startDate=$("#txtStartDate").val();
        var endDate=$("#txtEndDate").val();
      
        var type="FCF_Line.swf";
        var url="../../../Handler/Office/SellReport/SalesSummaryStatistics.ashx";
        if(t==3)
        {
            type="FCF_Column2D.swf";
        }
        
         $("#divListImage").html("<br /><br/><span style=\"color:red\">Loading...</span>");
         $.ajax({
            type: "post", 
            dataType: "string",
            url: url, 
            cache: false,
            data: "action=getimage&type="+type+"&StartDate="+startDate+"&EndDate="+endDate+"&sumType="+sumType,
            beforeSend: function() { }, 
            success: function(msg) {
                if(msg=="")
                {
                
                    $("#divListImage").html("<br /><br/><span style=\"color:red\">暂无数据</span>");
                }
                else
                {
                    var chart = new FusionCharts("../../../Images/FusionCharts/"+type, "ChartId", "800", "450");
		            chart.setDataXML(msg);		
		            chart.render("divListImage");
                }
                
                
            },
            error: function(e) {},
            complete: function() {} 
        });
}



function GoPage(pageIndex)
{

var type=$("#showType").val();
    switch(type)
    {  
        case "1":
           document.getElementById("divCenterData").style.display="none";
            $("#divList").attr("style","display:");
            //$("#divListImage").attr("style","display:none");
            break;
        case "2":
        case "3":
        $("#divList").attr("style","display:none");
        $("#divCenterData").attr("style","display:");
        getReportImage(type);
        break;
    }

    document.getElementById("txtPageIndex").value=pageIndex;
    document.getElementById("txtPageSize").value=pageCount;
    currentPageIndex = pageIndex;
    
    var sumType=$("#selSumType").val();
    var startDate=$("#txtStartDate").val();
    var endDate=$("#txtEndDate").val();
//    
//    if(startDate=="" )
//    {
//        popMsgObj.Show("开始日期|","开始日期不能为空");
//    } 
//   if(endDate=="")
//   {
//        
//   }
//    
    
    
    /*保存搜索标志位*/
    document.getElementById("txtIsSearch").value="0";
    /*构造参数*/
    var para="action=get&"+
                    "&StartDate="+startDate+
                    "&EndDate="+endDate+
                    "&sumType="+sumType+
                    "&pageIndex="+pageIndex+
                    "&PageSize="+pageCount+
                    "&OrderBy="+document.getElementById("txtOrderBy").value;
      
    /*异步页面*/
    var url="../../../Handler/Office/SellReport/SalesSummaryStatistics.ashx";
    
   switch(sumType)
   {
        case"byDept":
        $("#thByDept").attr("style","display:");
        $("#thBySeller").attr("style","display:none");
         $("#thByProduct").attr("style","display:none");
        break;
        case "bySeller":
        $("#thByDept").attr("style","display:none");
         $("#thBySeller").attr("style","display:");
         $("#thByProduct").attr("style","display:none");
         break;
        case "byProduct":
         $("#thByDept").attr("style","display:none");
         $("#thBySeller").attr("style","display:none");
         $("#thByProduct").attr("style","display:");
        break;
   }
    
    $.ajax(
    {
        type: "POST",
        dataType:"json",
        url: url,
        cache:false,
        data: para,
        beforeSend:function(){ openRotoscopingDiv(true,"divPageMask","PageMaskIframe");},
        success: function(msg)
        {    
                
                    var totalNum=0;
                    var totalPrice=0;
            
                   $("#tblSubProductSendPrice tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                            
                            totalNum=parseFloat( parseFloat(totalNum)+parseFloat( item.SellNumTotal))  //parseFloat(totalNum).toFixed(2) +parseFloat(item.SellNumTotal).toFixed(2);
                            totalPrice=parseFloat( parseFloat(totalPrice)+ parseFloat(item.SellPriceTotal));
                        
                           var tdStr="";
                           switch(sumType)
                           {
                                case"byDept":
                                    tdStr="<td align=\"center\" title=\""+item.DeptName+"\" >"+item.DeptName+"</td><td style=\"display:none\"></td><td style=\"display:none\"></td>";
                                    break;
                                case "bySeller":
                                    tdStr="<td style=\"display:none\"></td><td align=\"center\" title=\""+item.EmployeeName+"\" >"+item.EmployeeName+"</td><td style=\"display:none\"></td>";                        break;
                               case "byProduct":
                                    tdStr="<td style=\"display:none\"></td><td style=\"display:none\"></td><td align=\"center\" title=\""+item.ProductName+"\" >"+item.ProductName+"</td>";                        break;
                           }
                        
                    
                        if(item!=null && item!="")
                        {
                        $("<tr class='newrow'></tr>").append(tdStr+
                        createTd(parseFloat(item.SellNumTotal).toFixed(2), parseFloat( item.SellNumTotal).toFixed(2))+
                        createTd(parseFloat(item.SellPriceTotal).toFixed(2),parseFloat(item.SellPriceTotal).toFixed(2))).appendTo($("#tblSubProductSendPrice tbody"));             
                   }});
                   
                   var appendStr="";
                     switch(sumType)
                           {
                                case"byDept":
                                    appendStr="<td align=\"center\">合计：</td><td style=\"display:none\"></td><td style=\"display:none\"></td>";
                                    break;
                                case "bySeller":
                                    appendStr="<td style=\"display:none\"></td><td align=\"center\">合计：</td><td style=\"display:none\"></td>";                       
                                     break;
                               case "byProduct":
                                    appendStr="<td style=\"display:none\"></td><td style=\"display:none\"></td><td align=\"center\">合计：</td>";                       
                                     break;
                           }
                            $("<tr class='newrow'></tr>").append(appendStr
                                +createTd(parseFloat( totalNum).toFixed(2),"")+createTd(parseFloat( totalPrice).toFixed(2))).appendTo($("#tblSubProductSendPrice tbody"));       
                   
                   
                     //页码
                   ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"GoPage({pageindex});return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                  $("#ShowPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                  $("#ToPage").val(pageIndex);      
        },
        error: function(msg){(true,"divPageMask");popMsgObj.Show("载入信息|","载入信息失败|");},
        complete:function(){closeRotoscopingDiv(true,"divPageMask");$("#pageDataList1_Pager").show();pageDataList1("tblSubProductSendPrice","#E7E7E7","#FFFFFF","#cfc","cfc");}
        
        }
    );
}

/*构造td*/
function createTd(value,title)
{
    return "<td align=\"center\" title=\""+title+"\" >"+value+"</td>";
}






/*设置排序字段*/
function CreateSort(control) {
    if (document.getElementById("txtIsSearch").value == "") {
        return;
    }
        var ordering=document.getElementById("txtOrderBy");
        var obj=document.getElementById(control);
        var allOrderTipDOM  = $(".orderTip");
        allOrderTipDOM.empty();
        if(ordering.value==(control+ " ASC"))
        {
            ordering.value=control+ " DESC";
            obj.innerHTML="↓";
        }
        else
        {
            ordering.value=control+ " ASC";
            obj.innerHTML="↑";
        }
       GoPage(1);
       
       
}





 /*翻页验证*/
 function ChangePageCountIndex(newPageCount,newPageIndex) {
     if (document.getElementById("txtIsSearch").value == "") {
         return;
     }
        if(!IsNumOrFloat(newPageCount,true))
        {
            popMsgObj.Show("每页显示|","必须为正整数格式|");
            document.getElementById("ShowPageCount").value="10";
            return;
        }
        if(!IsNumOrFloat(newPageIndex,true))
        {
            popMsgObj.Show("跳转页数|","必须为正整数格式|");
            document.getElementById("ToPage").value="1";
            return;
        }
 
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
        {
            popMsgObj.Show("跳转页数|","超出查询范围|");
            return false;
        }
        else
        {
            this.pageCount=parseInt(newPageCount);
             GoPage(parseInt(newPageIndex));
             document.getElementById("chk_SendPriceList").checked=false;
        }
  }


function fnCheck()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;

    var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
        isFlag = false;
        fieldText = fieldText + RetVal+"|";
        msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
    
    if(!isFlag)
    {
    popMsgObj.Show(fieldText,msgText);
    } 
    return isFlag;   
}