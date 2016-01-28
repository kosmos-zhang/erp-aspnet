    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    var ifshow="0";
    var currentPageIndex = 1;
    var orderBy = "ModifiedDate_d";//排序字段
    var reset="1";
    var action="";
    var pageCountHistory = 10;
    var currentPageIndexHistory = 1;
    var totalRecordHistory = 0;
    
$(document).ready(function()
{
  
    
    var requestobj = GetRequest(location.search);
    var SourcePage = requestobj['SourcePage'];
    if( SourcePage == "Add")
    {//编辑页面进入
        if(requestobj['No'] != null)
        {//询价编号传过去了，说明之前点击了检索，设置检索条件，然后检索
            fnSetSelectCondition(requestobj);
            fnGetSelectCondition();
            TurnToPage(currentPageIndex);
        }
        else
        {//将检索条件设为空，并且不进行检索操作
            
        }
        
        //
        
    }
    else if(SourcePage == null)
    {//左侧菜单栏进入
        
    }
      IsDiplayOther('GetBillExAttrControl1_SelExtValue','GetBillExAttrControl1_TxtExtValue');
});


//***********************************************Start 界面取值与赋值********************************************************
//将界面上的值放入隐藏域，此函数在点击检索时激发
function fnGetSelectCondition()
{
    var strParams = "";
    var AskNo = $("#txtAskNo").val();
    var AskTitle = $("#txtAskTitle").val();
    var FromType = $("#ddlFromType").val();
    var AskDeptID = $("#hidDeptID").val();
    var AskDeptName = $("#DeptName").val();
    var AskUserID = $("#hidUserID").val();
    var AskUserName = $("#UserAskUserName").val();
    
    var BillStatus = $("#ddlBillStatus").val();
    var ProviderID = $("#hidProviderID").val();
    var ProviderName = $("#txtProviderName").val();
    var FlowStatus = $("#ddlFlowStatus").val();
    var StartAskDate = $("#StartAskDate").val();
    var EndAskDate = $("#EndAskDate").val();
    
    strParams += "&No="+AskNo;
    strParams += "&Title="+AskTitle;
    strParams += "&FromType="+FromType;
    strParams += "&AskDeptID="+AskDeptID;
    strParams += "&AskDeptName="+AskDeptName;
    strParams += "&AskUserID="+AskUserID;
    strParams += "&AskUserName="+AskUserName;
    
    strParams += "&BillStatus="+BillStatus;
    strParams += "&ProviderID="+ProviderID;
    strParams += "&ProviderName="+ProviderName;
    strParams += "&FlowStatus="+FlowStatus;
    strParams += "&StartAskDate="+StartAskDate;
    strParams += "&EndAskDate="+EndAskDate;
    
     var EFIndex=document.getElementById("GetBillExAttrControl1_SelExtValue").value;
           var EFDesc=document.getElementById("GetBillExAttrControl1_TxtExtValue").value;

 strParams += "&EFIndex="+escape(EFIndex);
    strParams += "&EFDesc="+escape(EFDesc); 
    
    $("#SearchCondition").val(strParams);
}

//界面赋值
//将查询条件赋到相应位置
function fnSetSelectCondition(requestobj)
{
    $("#txtAskNo").val(requestobj['No']);
    $("#txtAskTitle").val(requestobj['Title']);
    $("#ddlFromType").val(requestobj['FromType']);
    $("#hidDeptID").val(requestobj['DeptID']);
    $("#DeptName").val(requestobj['DeptName']);
    
    $("#hidUserID").val(requestobj['AskUserID']);
    $("#UserAskUserName").val(requestobj['AskUserName']);
    $("#ddlBillStatus").val(requestobj['BillStatus']);
    $("#hidProviderID").val(requestobj['ProviderID']);
    $("#txtProviderName").val(requestobj['ProviderName']);
    
    $("#ddlFlowStatus").val(requestobj['FlowStatus']);
    $("#StartAskDate").val(requestobj['StartAskDate']);
    $("#EndAskDate").val(requestobj['EndAskDate']);
    
    pageCount = parseInt(requestobj['pageCount']);
    currentPageIndex = parseInt(requestobj['pageIndex']);
}
//***********************************************End   界面取值与赋值********************************************************



//***********************************************Start 检    索********************************************************
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

   
   if(CompareDate($("#StartAskDate").val(), $("#EndAskDate").val())==1)
   {
        isFlag=false;
        fieldText=fieldText + "询价时间|";
        msgText = msgText +  "起始时间不能大于终止时间|";
   }
   if(!isFlag)
   {
        popMsgObj.Show(fieldText,msgText);
   } 
   return isFlag;   
}

//检索
function DoSearch()
{
    if(!fnCheck())
    return;
    //将界面上的检索条件放入隐藏域中
    fnGetSelectCondition();
    
    TurnToPage(1);
}

//获取页码和排序信息
function fnGetPageInfo()
{
    var Info = "&pageCount="+pageCount;
    Info += "&pageIndex="+currentPageIndex;
    Info += "&orderBy="+orderBy;
    return Info;
}


//排序
    function OrderByAsk(orderColum,orderTip)
    {
    if($("#SearchCondition").val() == "")
        return;
        var ooo = "ASC";
        ifshow="0";
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
            ooo = "DESC";
        }
        orderBy = orderColum+"_"+ordering;
        ooo = orderColum +" " +ooo;
        $("#hidOrderBy").val(ooo);
        TurnToPage(1);
    }
function TurnToPage(pageIndex)
{
    
    currentPageIndex = pageIndex;
    
    document.getElementById("checkall").checked = false;
    //获取检索条件
    var URLParams = $("#SearchCondition").val();
    
    //页码
    URLParams += fnGetPageInfo();
    
    //操作码
    URLParams += "&ActionAsk=Select"; 
    
    $.ajax(
    {
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchaseAskPriceInfo.ashx?'+URLParams,//目标地址
           cache:false,
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg)
           {
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#PurAskPriceInfo tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item)
                    {
//                        var history;
//                        if(item.AskOrder == 1)
//                        {//第一次询价，询价历史不连接
//                            history = "<td height='22' align='center'>询价历史</td>"
//                        }
//                        else
//                        {
//                            history = "<td height='22' align='center'><a href='#' onclick=ShowPurAskPriceHistory('"+item.ID+"','"+item.AskNo.Trim()+"','"+item.AskOrder.Trim()+"',1)>询价历史</a></td>"
//                        }
                        if(item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox"+(i + 1)+"' name='Checkbox' onclick=\"IfSelectAll('Checkbox','checkall')\" value="+item.ID+"   type='checkbox'/>"+"</td>"+
//                        history +
                        "<td height='22' align='center' title=\""+item.AskNo+"\"><a href='#' onclick=fnSelect('"+item.ID+"')>"+ GetStandardString(item.AskNo,25) +"</a></td>"+
                        "<td height='22' align='center' title=\""+item.AskTitle+"\">"+ GetStandardString(item.AskTitle,15) +"</td>"+
                        "<td height='22' align='center'>"+ item.ProviderName +"</td>"+
                        "<td height='22' align='center'>"+ item.AskDate +"</td>"+
                        "<td height='22' align='center'>"+ item.AskUserName +"</td>"+
                        "<td height='22' align='center'>"+ item.AskOrder +"</td>"+ 
                        "<td height='22' align='center'>"+  (parseFloat(item.TotalPrice)).toFixed($("#HiddenPoint").val())+"</td>"+
                        "<td height='22' align='center'>"+       (parseFloat(item.TotalTax)).toFixed($("#HiddenPoint").val())+"</td>"+
                        "<td height='22' align='center'>"+  (parseFloat(item.TotalFee)).toFixed($("#HiddenPoint").val())+"</td>"+
                        "<td height='22' align='center' id=\"BillStatusName"+(i+1)+"\">"+ item.BillStatusName +"</td>"+
                        "<td height='22' align='center' id=\"FlowStatusName"+(i+1)+"\">"+ item.FlowStatusName +"</td>"+
                        "<td height='22' align='center' style=\"display:none\" id=\"AskNo"+(i+1)+"\">"+ item.AskNo +"</td>"
                        ).appendTo($("#PurAskPriceInfo tbody"));
                   });
                    //页码
                   ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPage({pageindex});return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text2").value=msg.totalCount;
                  $("#ShowPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                  $("#ToPage").val(pageIndex);
           },
           error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
           complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.getElementById("Text2").value);pageDataList1("PurAskPriceInfo","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
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

function IfshowHistory(count)
{
    if(count=="0")
    {
        document.getElementById("divPageHistory").style.display = "none";
        document.getElementById("pageHistorycount").style.display = "none";
    }
    else
    {
        document.getElementById("divPageHistory").style.display = "block";
        document.getElementById("pageHistorycount").style.display = "block";
    }
}
//***********************************************End   检    索********************************************************





//***********************************************Start 跳    转********************************************************
//跳转至某条单据
function fnSelect(ID)
{
    //检索条件
    var URLParams = $("#SearchCondition").val();
    
    //分页信息
    URLParams += fnGetPageInfo();
    
    //来源单据标志
    URLParams += "&SourcePage=Info";
    
    URLParams += "&ID="+ID;
    
    //跳转时传ModuleID
    URLParams += "&ModuleID=2041601";
    window.location.href = 'PurchaseAskPriceAdd.aspx?'+URLParams;
}


//跳转至新增页面
function DoNew()
{
    //检索条件
    var URLParams = $("#SearchCondition").val();
    //分页信息
    URLParams += fnGetPageInfo();
    
    //来源单据标志
    URLParams += "&SourcePage=Info";
    
    //跳转时传ModuleID
    URLParams += "&ModuleID=2041601";
    window.location.href = 'PurchaseAskPriceAdd.aspx?'+URLParams;
}


//***********************************************End   跳    转********************************************************











//获取url中"?"符后的字串
function GetRequest(url)
{
   var theRequest = new Object();
   if (url.indexOf("?") != -1) 
   {
      var str = url.substr(1);
      strs = str.split("&");
      for(var i = 0; i < strs.length; i ++) 
      {
         theRequest[strs[i].split("=")[0]]=unescape(strs[i].split("=")[1]);
      }
   }
   return theRequest;
}









function ShowPurAskPriceHistory(ID,AskNo,AskOrder,pageIndex)
{
currentPageIndexHistory = pageIndex;
    var ActionAsk = "History";
    var URLParams = "ActionAsk="+ActionAsk;
    URLParams += "&ID="+ID;
    URLParams += "&AskNo="+AskNo;
    URLParams += "&AskOrder="+AskOrder;
    
    URLParams += "&pageCount="+"10";
    URLParams += "&pagerStyle="+pagerStyle;
    URLParams += "&pageIndex="+"1";
    URLParams += "&orderBy="+"";
    $.ajax(
    {
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchaseAskPriceInfo.ashx?'+URLParams,//目标地址
           cache:false,
           beforeSend:function(){AddPop();$("#pageDataList1_PagerHistory").hide();},//发送数据之前
           
           success: function(msg)
           {
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#PurAskPriceHistory tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item)
                    {
                        if(item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Chkbox"+(i + 1)+"' name='Chkbox'  value="+item.ID+"   type='checkbox'/>"+"</td>"+
//                        "<td height='22' align='center'><a href='#' onclick=SelectDept('"+item.ApplyIndex+"')>"+ item.ApplyNo +"</a></td>"+
                        "<td height='22' align='center'>"+item.ProductNo+"</td>"+
                        "<td height='22' align='center'>"+ item.ProductName +"</td>"+
                        "<td height='22' align='center'>"+ item.Specification +"</td>"+
                        "<td height='22' align='center'>"+ FormatAfterDotNumber(item.ProductCount,2) +"</td>"+
                        "<td height='22' align='center'>"+ item.RequireDate +"</td>"+
                        "<td height='22' align='center'>"+ item.UnitName +"</td>"+
                        "<td height='22' align='center'>"+ FormatAfterDotNumber(item.UnitPrice,2) +"</td>"+
                        "<td height='22' align='center'>"+ FormatAfterDotNumber(item.TaxPrice,2) +"</td>"+
                        "<td height='22' align='center'>"+ FormatAfterDotNumber(item.DiscountDetail,2) +"</td>"+
                        "<td height='22' align='center'>"+ FormatAfterDotNumber(item.TaxRate,2) +"</td>"+
                        "<td height='22' align='center'>"+ FormatAfterDotNumber(item.TotalPriceDetail,2) +"</td>"+
                        "<td height='22' align='center'>"+ FormatAfterDotNumber(item.TotalFeeDetail,2) +"</td>"+
                        "<td height='22' align='center'>"+ FormatAfterDotNumber(item.TotalTaxDetail,2) +"</td>"+
                        
                        "<td height='22' align='center'>"+ FormatAfterDotNumber(item.CountTotal,2) +"</td>"+
                        "<td height='22' align='center'>"+ FormatAfterDotNumber(item.TotalPrice,2) +"</td>"+
                        "<td height='22' align='center'>"+ FormatAfterDotNumber(item.TotalTax,2) +"</td>"+
                        "<td height='22' align='center'>"+ FormatAfterDotNumber(item.TotalFee,2) +"</td>"+
                        "<td height='22' align='center'>"+ FormatAfterDotNumber(item.Discount,2) +"</td>"+
                        "<td height='22' align='center'>"+ FormatAfterDotNumber(item.DiscountTotal,2) +"</td>"+
                        "<td height='22' align='center'>"+ FormatAfterDotNumber(item.RealTotal,2) +"</td>"
                        ).appendTo($("#PurAskPriceHistory tbody")
                        
                        );
                   });
                    //页码
                   ShowPageBar("pageDataList1_PagerHistory",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountHistory,currentPageIndex:currentPageIndexHistory,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"return false;"}//[attr]
                    );
                  totalRecordHistory = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text2History").value=msg.totalCount;
                  $("#ShowPageCountHistory").val(pageCountHistory);
                  ShowTotalPage(msg.totalCount,pageCountHistory,currentPageIndexHistory,$("#pageHistorycount"));
                  $("#ToPageHistory").val(currentPageIndexHistory);
           },
           error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
           complete:function(){hidePopup();$("#pageDataList1_PagerHistory").show();IfshowHistory(document.getElementById("Text2History").value);pageDataList1("PurAskPriceHistory","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
      });
    document.getElementById("History").style.display = "block";
    document.getElementById("frmHistory").style.display="block";
}

function closeHistory()
{
    document.getElementById("History").style.display = "none";
    document.getElementById("frmHistory").style.display="none";
}



//全选
function SelectAll() {
    $.each($("#PurAskPriceInfo :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
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
        this.pageCount=parseInt(newPageCount);
        TurnToPage(parseInt(newPageIndex));
    }
}


function ChangePageCountIndexHistory(newPageCount,newPageIndex)
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
    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecordHistory-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        pageCountHistory=parseInt(newPageCount);
        TurnToPage(parseInt(newPageIndex));
    }
}


function DeletePurAsk()
{
if(!confirm("是否确认进行删除操作！"))
return;
    var signFrame = document.getElementById("PurAskPriceInfo");
    var IDs = "(";
    var AskNos = "(";
    
    var isErrorFlag = false;
    var msgText = "";
    
    var Count = 0;
    for(var i=1;i<signFrame.rows.length;++i)
    {
        if(document.getElementById("Checkbox"+i).checked == true)
        {
            var aaa =document.getElementById("BillStatusName"+i+"").innerHTML;
            var bbb = document.getElementById("FlowStatusName"+i+"").innerHTML;
            if(( aaa== "制单")&&( bbb== ""))
            {//可以删
                IDs += document.getElementById("Checkbox"+i).value;
                IDs += ",";
                AskNos +="'";
                AskNos +=document.getElementById("AskNo"+i).innerHTML;
                AskNos +="'";
                AskNos +=",";
                Count++;
            } 
            else
            {//不可以删
                isErrorFlag = true;
                msgText += ""+i+",";
            }           
        }
    }
    IDs=IDs.slice(0,IDs.length-1);
    AskNos=AskNos.slice(0,AskNos.length-1);
    IDs +=")";
    AskNos +=")";
    if(isErrorFlag)
    {   
//        msgText = "已提交审批或已确认后的单据不允许删除!"+msgText.substring(0,msgText.length-1)+"条";
    msgText = "已提交审批或已确认后的单据不允许删除!";
        popMsgObj.ShowMsg(msgText);
        return;
    }
    if(Count == 0)
    {
        msgText +="请选择要删除的采购询价！";
        popMsgObj.ShowMsg(msgText);
        return;
    }
    var ActionAsk = "Delete";
    var URLParams = "";
    URLParams += "&ActionAsk="+ActionAsk;
    URLParams += "&IDs="+IDs;
    URLParams += "&AskNos="+AskNos;
    $.ajax(
    {
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchaseAskPriceInfo.ashx?'+URLParams,//目标地址
           cache:false,
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg)
           {
//                ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());
DoSearch();
                msgText +="删除成功！";
                popMsgObj.ShowMsg(msgText);
           },
           error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
           complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.all["Text2"].value);pageDataList1("PurAskPriceInfo","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
      });
}