$(document).ready(function(){
  DoSearch();
});

/*
* 查询操作
*/
function DoSearch()
{
    /* 获取参数 */
    //员工编号
    postParam = "EmployeeNo=" + document.getElementById("txtEmployeeNo").value.Trim();
    //员工工号
    postParam += "&EmployeeNum=" + document.getElementById("txtEmployeeNum").value.Trim();
    //员工姓名
    postParam += "&EmployeeName=" +escape ( document.getElementById("txtEmployeeName").value.Trim());
    //所在岗位
    postParam += "&QuarterID=" + document.getElementById("ddlQuarter").value.Trim();
    //岗位职等
    postParam += "&AdminLevelID=" + document.getElementById("ctAdminLevel_ddlCodeType").value.Trim();
    //年月
    postParam += "&SalaryMonth=" + document.getElementById("txtSalaryMonth").value.Trim();
    
    //设置检索条件
    document.getElementById("hidSearchCondition").value = postParam;
    
    TurnToPage(1);
    
}

/*
* 重置页面
*/
function ClearInput()
{
    //员工编号
    document.getElementById("txtEmployeeNo").value = "";
    //员工工号
    document.getElementById("txtEmployeeNum").value = "";
    //员工姓名
    document.getElementById("txtEmployeeName").value = "";
    //所在岗位
    document.getElementById("ddlQuarter").value = "";
    //岗位职等
    document.getElementById("ctAdminLevel_ddlCodeType").value = "";
    //年月
    document.getElementById("txtSalaryMonth").value = "";
}

/*
* 重置页面
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
    var action="GetInfo";
    var postParam = "PageIndex=" + pageIndex + "&PageCount=" + pageCount + "&Action=" + action + "&OrderBy=" + orderBy + "&" + searchCondition;
    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  '../../../Handler/Office/HumanManager/InputTaxInfo.ashx',
        data : postParam,//目标地址
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
                    if(item.EmployeeNo != null && item.EmployeeNo != "")
                    $("<tr class='newrow'></tr>").append(
                          "<td height='22' align='center'>" + item.EmployeeNo + "</td>" //员工编号
                        + "<td height='22' align='center'>" + item.EmployeeName + "</td>" //姓名
                        + "<td height='22' align='center'>" + item.DeptName + "</td>" //部门
                        + "<td height='22' align='center'>" + item.QuarterName + "</td>" //岗位
                        + "<td height='22' align='center'>" + item.AdminLevelName + "</td>"  //岗位职等 
                        + "<td height='22' align='center'>" + item.TotalSalary + "</td>"  //工资合计  
                        + "<td height='22' align='center'>" + item.TaxRate + "</td>" //税率
                        + "<td height='22' align='center'>" + item.TotalTax + "</td>").appendTo($("#tblDetailInfo tbody")//税额
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
    for(var i = 1; i < tableTr.length; i++)
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