////页面初期表示显示数据
//$(document).ready(function(){
//      //TurnToPage(1);
//});
/*
* 执行查询
*/
function DoSearch()
{
 


var isFlag=true ;
    var fieldText="";
    var msgText="";
  
    var sd=document.getElementById("txtEmployeeNo").value.Trim();
 if (!CheckSpecialWord(sd ))
 {
           isFlag = false;
            fieldText +="人员编号|";
   		    msgText = msgText +"人员编号"+  "不能含有特殊字符|";
 }
  if (!CheckSpecialWord(document.getElementById("txtName").value.Trim()))
 {
           isFlag = false;
            fieldText +="姓名|";
   		    msgText = msgText +"姓名"+  "不能含有特殊字符|";
 }
   if (!CheckSpecialWord(document.getElementById("txtTotalSeniority").value.Trim()))
 {
           isFlag = false;
            fieldText +="工作年限|";
   		    msgText = msgText +"工作年限"+  "不能含有特殊字符|";
 }
  
     if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
      return false ;
            return false ;
                  return false ;
                        return false ;
    }
    
    var search = "";
    //人员编号
    search += "EmployeeNo=" + escape ( document.getElementById("txtEmployeeNo").value.Trim());
    //姓名
    search += "&EmployeeName=" + escape (document.getElementById("txtName").value.Trim());
    //应聘岗位
    search += "&QuarterID=" + escape (document.getElementById("ddlQuarter").value.Trim());
    //工作年限
    search += "&TotalSeniority=" + escape (document.getElementById("txtTotalSeniority").value.Trim());
    //学历
    search += "&CultureID=" + escape (document.getElementById("ddlCulture_ddlCodeType").value.Trim());
    //专业
    search += "&ProfessionalID=" + escape (document.getElementById("ddlProfessional_ddlCodeType").value.Trim());
    
    //设置检索条件
    document.getElementById("hidSearchCondition").value = search;
    //执行查询
    TurnToPage(1);
}

/*
* 重置页面
*/
function ClearInput()
{
    //人员编号
    document.getElementById("txtEmployeeNo").value = "";
    //姓名
    document.getElementById("txtName").value = "";
    //应聘岗位
    document.getElementById("ddlQuarter").value = "";
    //工作年限
    document.getElementById("txtTotalSeniority").value = "";
    //学历
    document.getElementById("ddlCulture_ddlCodeType").value = "";
    //专业
    document.getElementById("ddlProfessional_ddlCodeType").value = "";
}

/*
* 改页显示
*/
function ChangePageCountIndex(newPageCount, newPageIndex)
{
    //判断是否是数字
    if (!IsNumber(newPageCount))
    {
        popMsgObj.ShowMsg('请输入正确的显示条数！');
        return;
    }
    if (!IsNumber(newPageIndex))
    {
        popMsgObj.ShowMsg('请输入正确的转到页数！');
        return;
    }
    //判断重置的页数是否超过最大页数
    if(newPageCount <=0 || newPageIndex <= 0 || newPageIndex > ((totalRecord - 1)/newPageCount) + 1)
    {
        popMsgObj.ShowMsg('转至页数超出查询范围！');
    }
    else
    {
        //设置每页显示记录数
        this.pageCount = parseInt(newPageCount, 10);
        //显示页面数据
        TurnToPage(parseInt(newPageIndex, 10));
    }
}

/* 分页相关变量定义 */  

var pageCount = 10;//每页显示记录数
var totalRecord = 0;//总记录数
var pagerStyle = "flickr";//jPagerBar样式
var currentPageIndex = 1;//当前页
var orderBy = "";//排序字段

/*
* 翻页处理
*/
function TurnToPage(pageIndex)
{
    //设置当前页
    currentPageIndex = pageIndex;
    //获取查询条件
    var searchCondition = document.getElementById("hidSearchCondition").value;
    //设置动作种类
    var postParam = "PageIndex=" + pageIndex + "&PageCount=" + pageCount + "&OrderBy=" + orderBy + "&" + searchCondition;
    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  '../../../Handler/Office/HumanManager/SelectReserve_Query.ashx?' + postParam,//目标地址
        dataType:"json",//数据格式:JSON
        cache:false,
        beforeSend:function()
        {
            AddPop();
        },//发送数据之前
        success: function(msg)
        {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#tblDetailInfo tbody").find("tr.newrow").remove();
            $.each(msg.data
                ,function(i,item)
                {
                    if(item.ID != null && item.ID != "")
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"
                        + "<input id='rdoSelect' name='rdoSelect' value='" + item.ID + "' type='radio'/>" + "</td>" //选择框
                        + "<td height='22'>" + item.EmployeeNo + "</td>" //编号
                        + "<td height='22' id='td" + item.ID + "'>" + item.EmployeeName + "</td>" //姓名
                        + "<td height='22'>" + item.SexName + "</td>" //性别
                        + "<td height='22'>" + item.QuarterName + "</td>" //应聘岗位
                        + "<td height='22'>" + item.SchoolName + "</td>" //毕业院校
                        + "<td height='22'>" + item.ProfessionalName + "</td>" //专业
                        + "<td height='22'>" + item.CultureLevelName + "</td>" //学历
                        + "<td height='22'>" + item.TotalSeniority + "</td>").appendTo($("#tblDetailInfo tbody")//工龄
                    );
            });
            //页码
            ShowPageBar(
                "divPageClickInfo",//[containerId]提供装载页码栏的容器标签的客户端ID
                "<%= Request.Url.AbsolutePath %>",//[url]
                {
                    style:pagerStyle,mark:"DetailListMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageCount,
                    currentPageIndex:pageIndex,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上一页",
                    nextWord:"下一页",
                    First:"首页",
                    End:"末页",
                    onclick:"TurnToPage({pageindex});return false;"
                }
            );
            totalRecord = msg.totalCount;
            $("#txtShowPageCount").val(pageCount);
            ShowTotalPage(msg.totalCount, pageCount, pageIndex,$("#pagecount"));
            $("#txtToPage").val(pageIndex);
        },
        error: function() 
        {
            popMsgObj.ShowMsg('请求发生错误！');
        },
        complete:function(){
            hidePopup();
            $("#divPageClickInfo").show();
            SetTableRowColor("tblDetailInfo","#E7E7E7","#FFFFFF","#cfc","cfc");
        }
    });
}

/*
* 设置数据明细表的行颜色
*/
function SetTableRowColor(elem,colora,colorb, colorc, colord){
    //获取DIV中 行数据
    var tableTr = document.getElementById(elem).getElementsByTagName("tr");
    for(var i = 0; i < tableTr.length; i++)
    {
        //设置行颜色
        tableTr[i].style.backgroundColor = (tableTr[i].sectionRowIndex%2 == 0) ? colora:colorb;
        //设置鼠标落在行上时的颜色
        tableTr[i].onmouseover = function()
        {
            if(this.x != "1") this.style.backgroundColor = colorc;
        }
        //设置鼠标离开行时的颜色
        tableTr[i].onmouseout = function()
        {
            if(this.x != "1") this.style.backgroundColor = (this.sectionRowIndex%2 == 0) ? colora:colorb;
        }
    }
}

/*
* 排序处理
*/
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

/*
* 确定处理
*/
function DoConfirm()
{
    //获取选择框
    var radioList = document.getElementsByName("rdoSelect");
    //变量定义
    selectID = "";
    selectName = "";
    for( var i = 0; i < radioList.length; i++ )
    {
        //判断选择框是否是选中的
        if ( radioList[i].checked )
        {
            //获取ID
            selectID = radioList[i].value;
            //获取名
            selectName = document.getElementById("td" + selectID).innerHTML;
            
            break;
        }
    }
    //没有选择时，提示信息
    if (selectID == "" || selectID == null)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一项！");
    }
    else
    {
        window.parent.SetReserveInfo(selectID, selectName);
    }
}

/*
* 返回处理
*/
function DoBack()
{ 
    window.parent.SetReserveInfo("", "");
}