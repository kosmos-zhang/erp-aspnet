/*
* 查询
*/
function DoSearch(currPage)
{  
    var search = "";
    //编号
    search += "EmployeeNo=" + document.getElementById("txtEmployeeNo").value;
    //工号
    search += "&EmployeeNum=" + document.getElementById("txtEmployeeNum").value;
    //姓名
    search += "&EmployeeName=" + document.getElementById("txtEmployeeName").value;
    search += "&PYShort=";
    //工种
    search += "&ContractKind=";
    //行政等级
    search += "&AdminLevel=";
    //岗位
    search += "&QuarterID=" + document.getElementById("ddlQuarter").value;
    //职称
    search += "&PositionID=" + document.getElementById("ddlPosition_ddlCodeType").value;
    //入职时间
    search += "&StartDate=" + document.getElementById("txtStartDate").value;
    search += "&EnterEndDate=";
    search += "&Mobile=";
    
    //设置检索条件
    document.getElementById("hidSearchCondition").value = search;
    //执行查询
    if (currPage == null || typeof(currPage) == "undefined")
    {
        TurnToPage(1);
    }
    else
    {
        TurnToPage(parseInt(currPage, 10));
    }
}

/*
* 重置页面
*/
function ClearInput()
{
    //编号
    document.getElementById("txtEmployeeNo").value = "";
    //工号
    document.getElementById("txtEmployeeNum").value = "";
    //身份证
    document.getElementById("txtCardID").value = "";
    //姓名
    document.getElementById("txtEmployeeName").value = "";
    //工种
    //document.getElementById("ddlContractKind").value = "";
    //行政等级
    //document.getElementById("ddlAdminLevel_ddlCodeType").value = "";
    //职务
    document.getElementById("ddlQuarter").value = "";
    //职称
    document.getElementById("ddlPosition_ddlCodeType").value = "";
    //入职时间
    document.getElementById("txtStartDate").value = "";
}

/*
* 改变页面显示
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
        var fieldText = "";
        var msgText = "";
        var RetVal=CheckSpecialWords();
        if(RetVal!="")
        {
                fieldText = fieldText + RetVal+"|";
	            msgText = msgText +RetVal+  "不能含有特殊字符|";
	            popMsgObj.Show(fieldText,msgText);
	            return;
        } 
    //设置当前页
    currentPageIndex = pageIndex;
    //获取查询条件
    var searchCondition = document.getElementById("hidSearchCondition").value;
    //设置动作种类
    var action="GetInfo";
    var postParam = "PageIndex=" + pageIndex + "&PageCount=" + pageCount + "&Action=" + action + "&OrderBy=" + orderBy + "&" + searchCondition;
    var ModuleID = document.getElementById("hidModuleID").value;
    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  '../../../Handler/Office/HumanManager/EmployeeWork_Query.ashx?' + postParam,//目标地址
        dataType:"json",//数据格式:JSON
        cache:false,
        beforeSend:function()
        {
            AddPop();
            $("#divEmployeeInfo").hide();
        },//发送数据之前
        success: function(msg)
        {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#tblDetailInfo tbody").find("tr.newrow").remove();
            $.each(msg.data
                ,function(i,item)
                {
                    var WorkAge;
                    if(item.ID != null && item.ID != "")
                    {
                     var newdate = document.getElementById("hidSysteDate").value;
                     var olddate = item.TotalYear;
                     if(olddate == "")
                     {
                        WorkAge = "";
                     }
                     else
                     {
                        WorkAge = parseInt(getDateDiff(olddate,newdate)/365) + 1;
                     }  
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"
                            + "<input id='rdoSelect' name='rdoSelect' value='" + item.ID + "' type='radio'/>" + "</td>" //选择框
                            + "<td height='22' align='center'>" + item.EmployeeNo + "</td>" //编号
                            + "<td height='22' align='center'>" + item.EmployeeNum + "</td>" //工号
                            + "<td height='22' align='center'>" + item.EmployeeName + "</td>" //姓名
                            //+ "<td height='22' align='center'>" + item.ContractKind + "</td>" //工种
                            + "<td height='22' align='center'>" + item.DeptName + "</td>"  //部门           
                          //  + "<td height='22' align='center'>" + item.AdminLevelName + "</td>"//行政等级
                            + "<td height='22' align='center'>" + item.QuarterName + "</td>" //岗位
                            + "<td height='22' align='center'>" + item.PositionName + "</td>"//职称
                            + "<td height='22' align='center'>" + item.EntryDate + "</td>"// 入职时间
                            + "<td height='22' align='center'>" + WorkAge + "</td>").appendTo($("#tblDetailInfo tbody")//本公司工龄
                        );
                    }
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
            ShowTotalPage(msg.totalCount, pageCount, pageIndex, $("#pagecount"));
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
if (totalRecord == 0) 
         {
            return;
         }
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
* 快速调职
*/
function DoFastShift()
{
    //获取选中的值
    selectID = GetSelect();
    //没有选择时，提示信息
    if (selectID == "" || selectID == null)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一项！");
    }
    else
    {        
        //获取模块功能ID
        var ModuleID = document.getElementById("hidModuleID").value;
        //获取查询条件
        searchCondition = document.getElementById("hidSearchCondition").value;
        //是否点击了查询标识
        var flag = "0";//默认为未点击查询的时候
        if (searchCondition != "") flag = "1";//设置了查询条件时
        linkParam = "EmployeeShift_Edit.aspx?FromPage=0&EmployeeID=" + selectID + "&ModuleID=" + ModuleID 
                                + "&PageIndex=" + currentPageIndex + "&PageCount=" + pageCount + "&" + searchCondition + "&Flag=" + flag;
        //返回链接的字符串
        window.location.href = linkParam;
    }
}

/*
* 快速离职
*/
function DoFastLeave()
{
    //获取选中的值
    selectID = GetSelect();
    //没有选择时，提示信息
    if (selectID == "" || selectID == null)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一项！");
    }
    else
    {        
        //获取模块功能ID
        var ModuleID = document.getElementById("hidModuleID").value;
        //获取查询条件
        searchCondition = document.getElementById("hidSearchCondition").value;
        //是否点击了查询标识
        var flag = "0";//默认为未点击查询的时候
        if (searchCondition != "") flag = "1";//设置了查询条件时
        linkParam = "EmployeeLeave_Edit.aspx?FromPage=0&EmployeeID=" + selectID + "&ModuleID=" + ModuleID 
                                + "&PageIndex=" + currentPageIndex + "&PageCount=" + pageCount + "&" + searchCondition + "&Flag=" + flag;
        //返回链接的字符串
        window.location.href = linkParam;
    }
}

/*
* 获取列表中选中的值
*/
function GetSelect()
{
    //获取选择框
    var radioList = document.getElementsByName("rdoSelect");
    //变量定义
    selectID = "";
    for( var i = 0; i < radioList.length; i++ )
    {
        //判断选择框是否是选中的
        if ( radioList[i].checked )
        {
            //获取ID
            selectID = radioList[i].value;
            
            break;
        }
    }
    //返回选中值
    return selectID;
}