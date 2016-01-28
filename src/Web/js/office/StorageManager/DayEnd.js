  var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var currentpageCount = 10;
    var action = "";//操作
    var orderBy = "ProductNo_d";//排序字段
    var Isliebiao ;
    
    var ifdel="0";//是否删除
    var issearch="";


$(document).ready(function()
{
 

    requestobj = GetRequest(location.search);
    if(requestobj["day"] != null)
    {//从编辑页面返回的
        fnSetSelectCondition(requestobj);
        SearchArriveData();
    } 
  
});


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

 
function fnLinkEdit(ProductID,BatchNo,StorageID,DailyDate)
{
    //检索条件
 
    var URLParams = $("#hidSearchCondition").val();
    URLParams += "&ProductID="+ProductID;
     URLParams += "&BatchNo="+BatchNo;
      URLParams += "&StorageID="+StorageID;
       URLParams += "&DailyDate="+DailyDate;
    URLParams += "&pageIndex="+document .getElementById ("ToPage").value.Trim();
    URLParams += "&pageCount="+currentpageCount;
    URLParams += "&action=Info";
    URLParams += "&orderby="+orderBy;
    URLParams += "&intFromType=1";//intFromType为1标志位来源于列表    
    var ModuleID = document.getElementById("hidModuleID").value;
    URLParams += "&ModuleID="+ModuleID;
   window.location.href='DayEndDetail.aspx?'+URLParams;
}

//获取检索条件放入隐藏域   
function fnGetSelectCondition()
{
    var strParams = "";
    strParams += "day="+escape($("#txtStartPlanDate").val());
    $("#hidSearchCondition").val(strParams)
    return strParams;
}


//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount,newPageIndex)
{
    if(!IsZint(newPageCount))
    {
        popMsgObj.ShowMsg('显示条数必须输入正整数！');
        return;
    }
    if(!IsZint(newPageIndex))
    {
        popMsgObj.ShowMsg('跳转页数必须输入正整数！');
        return;
    }
    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
    {
        popMsgObj.ShowMsg('转到页数超出查询范围！');
        return;
    }
    else
    {
        currentpageCount=parseInt(newPageCount);
        TurnToPage(parseInt(newPageIndex));
    }
}   

//设置从编辑页面返回时的检索条件
function fnSetSelectCondition(requestobj)
{
    $("#txtStartPlanDate").val(requestobj["day"]);
    currentPageIndex = requestobj["pageIndex"];
    currentpageCount = requestobj["pageCount"];
    orderBy = requestobj["orderby"];
}



function SearchArriveData(sign)
{
    if(!fnCheck())
        return;


   
       issearch="1";
       
    //检索条件
    fnGetSelectCondition();
    if (typeof (sign)!="undefined")
    {
    TurnToPage(sign);
    }
    else
    {
    TurnToPage(currentPageIndex);
    }
    
}

//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex)
{
 
//    currentPageIndex = pageIndex;
     if (pageIndex !="")
{
 pageIndex =parseInt (pageIndex ,10);
}
else
{
currentPageIndex = pageIndex;
}

    var URLParams = document.getElementById("hidSearchCondition").value;
    URLParams += "&pageIndex="+pageIndex;
    URLParams += "&pageCount="+currentpageCount;
    URLParams += "&ActionApply=Search";
    URLParams += "&orderby="+orderBy;
    document .getElementById ("hidOrderby").value=orderBy;
    document .getElementById ("hidPageCount").value=currentpageCount;
      document .getElementById ("hidPageIndex").value=pageIndex;
      
    $.ajax({
        type: "POST",//用POST方式传输
        dataType:"json",//数据格式:JSON
        url:  '../../../Handler/Office/StorageManager/DayEnd.ashx',//目标地址
        cache:false,
        data: URLParams,
        beforeSend:function(){$("#pageDataList1_PagerList").hide();},//发送数据之前

        success: function(msg){
                //数据获取完毕，填充页面据显示
                //数据列表
                $("#pageDataList1 tbody").find("tr.newrow").remove();
                var j =1;
                $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
             
                    var Specification = item.Specification;
                    if(Specification != null){
                    if (Specification.length > 20) {
                        Specification = Specification.substring(0, 20) + '...';
                        }
                    }

                   // "<td height='22' align='center'>" + "<input id='chk" + i + "' name='Checkbox1'  Title='" + item.DailyDate + "' Class='" + true + "'  value='" + item.ID + "' type='checkbox' onclick=IfSelectAll('Checkbox1','checkall') />" + "</td>" +
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'><a href='#' onclick=fnLinkEdit('"+item.ProductID+"','"+item.BatchNo+"','"+item.StorageID+"','"+item.DailyDate+"')>"+ item.DailyDate +"</a></td>"+
                    "<td height='22' align='center'>"+ item .ProductNo +"</td>"+
                    "<td height='22' align='center'>"+ item.ProductName +"</td>"+
                    "<td height='22' align='center'>"+ Specification +"</td>"+
                    "<td height='22' align='center'>"+ item.BatchNo +"</td>"+
                     "<td height='22' align='center'>"+ item.StorageName +"</td>"+
                    "<td height='22' align='center'>"+ item.InTotal +"</td>"+
                    "<td height='22' align='center'> "+ item.OutTotal +" </td>"+ 
                    "<td height='22' align='center'  >"+ item.TodayCount +" </td>").appendTo($("#pageDataList1 tbody"));
               });
                //页码
               ShowPageBar("pageDataList1_PagerList",//[containerId]提供装载页码栏的容器标签的客户端ID
               "<%= Request.Url.AbsolutePath %>",//[url]
                {style:pagerStyle,mark:"pageDataList1Mark",
                totalCount:msg.totalCount,showPageNumber:3,pageCount:currentpageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                onclick:"TurnToPage({pageindex});return false;"}//[attr]
                ); 
              totalRecord = msg.totalCount;
             // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
              document.getElementById("Text2").value=msg.totalCount;
              $("#ShowPageCount").val(currentpageCount);
              ShowTotalPage(msg.totalCount,currentpageCount,pageIndex,$("#pagecount"));
              $("#ToPage").val(pageIndex);
              },
        error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");},
        complete: function() { hidePopup(); $("#pageDataList1_PagerList").show(); Ifshow(document.getElementById("Text2").value); pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
    });
}

//排序
function OrderBy(orderColum,orderTip)
{
 
    if(issearch=="")
        return;
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
    TurnToPage(1);
}

 


function  DelArrive()
{ 
    var fieldText = "";
    var isFlag = true;
    var msgText = "";
    var DetailID = '';//id
    var DetailNo = '';//No
    var pageDataList1 = findObj("pageDataList1", document);
    for (i = 0; i < pageDataList1.rows.length; i++) {
        if ($("#chk" + i).attr("checked")) {
            
            DetailNo += $("#chk" + i).attr("title") + ',';
            DetailID += $("#chk" + i).val() + ',';
            var FromBillText = $.trim($("#chk" + i).attr("class"));//是否引用，返回True和False
            var BillStatusText = pageDataList1.rows[i + 1].cells[9].innerText;
            var FlowInstanceText = pageDataList1.rows[i + 1].cells[11].innerText;
            if ($.trim(BillStatusText) != '制单' || $.trim(FlowInstanceText) != '' || FromBillText == 'True') {
                isFlag = false;
                fieldText = fieldText + "采购合同：" + $("#chk" + i).attr("title") + "|";
                msgText = msgText + "已提交审批或已确认后的单据不允许删除|";
            }
        }
    }
    if (DetailID == '') {
        popMsgObj.ShowMsg('请至少选择一条数据！');
    }
    else {
        if (isFlag) {
            if (confirm("确认执行删除操作吗？")) {
                //删除
                $.ajax({
                    type: "POST",
                    url: "../../../Handler/Office/PurchaseManager/PurchaseArriveInfo.ashx",
                    data: "action=Delete&DetailNo=" + escape(DetailNo),
                    dataType: 'json', //返回json格式数据
                    cache: false,
                    beforeSend: function() {

                    },

                    error: function() {

                        popMsgObj.ShowMsg('请求发生错误！');
                    },
                    success: function(data) {
                        if (data.sta == 1) {
                            popMsgObj.ShowMsg('删除成功！');
                            SearchArriveData();
                        }
                        else {
                            popMsgObj.ShowMsg('删除失败！');
                        }
                    }
                });
            }
        }
        else {
            popMsgObj.Show(fieldText, msgText);
        }
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



function fnnCheck(date)
{
 
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;
    
   if($("#txtStartPlanDate").val() !='')
   {
        if(  !compareDate  (date,$("#txtStartPlanDate").val() ))
        {
            isErrorFlag = true;
            msgText += "日期必须选择"+date+"之后！";
        }
    }
    else
    {
     isErrorFlag = true;
            msgText += "请选择日期！";
    }
    
    
    if(isErrorFlag)
    {
        popMsgObj.ShowMsg(msgText);
    }
    return isErrorFlag;
}


function DayEnd()
{
  
 
var day=document .getElementById ("txtStartPlanDate").value.Trim();


    
    
    
    
    
    var URLParams = "ActionApply=DayEnd";
    URLParams += "&day="+day;  
    $.ajax({ 
        type: "POST", 
        url: "../../../Handler/Office/StorageManager/DayEnd.ashx",
        dataType:'json',//返回json格式数据
        cache:false,
        data:URLParams,
        beforeSend:function()
        {
           // AddPop();
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(msg) 
        {
            if(msg.sta == 1)
            {
             
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","对"+day+" 日结成功！");
          SearchArriveData(1);
      } else if (msg.sta == 2) {

      showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请对前一日进行日结才可对" + day + "日结");

  }
  else if (msg.sta == 4) {

  //showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "由于系统当前日期已经做过出入库相关业务，因此不能进行首次日结处理!(首次日结处理的日期必须是当前日期的前1天，且系统当前日期没有做过出入库相关业务.)");
  alert("由于系统当前日期已经做过出入库相关业务，因此不能进行首次日结处理!(首次日结处理的日期必须是当前日期的前1天，且系统当前日期没有做过出入库相关业务.)");
  }
            
            else 
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","日结失败！");
            }
        } 
    });



}



function SearchRecord() {
   
    var day = document.getElementById("txtStartPlanDate").value.Trim();
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;

    if ($("#txtStartPlanDate").val() != '') {

    }
    else {
        isErrorFlag = true;
        msgText += "请选择日期！";
    }


    if (isErrorFlag) {
        popMsgObj.ShowMsg(msgText);
        return;
    }
  
    var URLParams = "ActionApply=SearchRecord";
    URLParams += "&day=" + day;

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/StorageManager/DayEnd.ashx",
        dataType: 'json', //返回json格式数据
        cache: false,
        data: URLParams,
        beforeSend: function() {
            //AddPop();
        },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        success: function(msg) {
           
            if (msg.sta == 1) {
                SearchArriveData();
            } else if (msg.sta == 2) {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", msg.info);
                return false;
            }
            else {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "日期检查失败！");
                return false;
            }
        }
    });



}



function OperateRecord() {

    var day = document.getElementById("txtStartPlanDate").value.Trim();
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;

    if ($("#txtStartPlanDate").val() != '') {

    }
    else {
        isErrorFlag = true;
        msgText += "请选择日期！";
    }


    if (isErrorFlag) {
        popMsgObj.ShowMsg(msgText);
        return;
    }
 
    var URLParams = "ActionApply=OperateRecord";
    URLParams += "&day=" + day;

    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/StorageManager/DayEnd.ashx",
        dataType: 'json', //返回json格式数据
        cache: false,
        data: URLParams,
        beforeSend: function() {
            //AddPop();
        },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        success: function(msg) {
            if (msg.sta == 1) {

                DayEnd();
            } else if (msg.sta == 2) {

                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", msg.info);
                return false;

            }

            else {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "日期检查失败！");
                return false;
            }
        }
    });



}