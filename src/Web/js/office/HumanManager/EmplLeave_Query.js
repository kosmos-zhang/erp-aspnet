$(document).ready(function()
{
      requestobj = GetRequest();
      var flag = requestobj['Flag'];
    if (flag=="1")
    {
        //编号
        $("#txtNotifyNo").val(requestobj['NotifyNo']);
        //申请人
        $("#txtTitle").val(requestobj['Title']);
        $("#ddlApply").val(requestobj['MoveApplyNo']);
        //申请日期
        $("#hidEmployeeID").val(requestobj['EmployeeID']);
         $("#UserEmployeeName").val(requestobj['EmployeeName']);
        //审批状态
        $("#txtOutDate").val(requestobj['OutDate']);
        $("#txtOutToDate").val(requestobj['txtOutToDate']);

        //获取当前页
       var pageIndex = requestobj['PageIndex'];
        //获取每页显示记录数 
        pageCount = requestobj['PageCount'];
        //执行查询
        DoSearch(pageIndex);
    }
 });
/*
* 查询
*/
function DoSearch(currPage)
{
if(!CheckInput())
    {
        return;
    }
    var search = "";
    //调职单编号
    search += "NotifyNo=" + document.getElementById("txtNotifyNo").value;
    //调职单主题
    search += "&Title=" + document.getElementById("txtTitle").value;
    //对应申请单
    search += "&EmplApplyNo=" + document.getElementById("ddlApply").value;
    //员工
    search += "&EmployeeID=" + document.getElementById("hidEmployeeID").value;
    search += "&EmployeeName=" + document.getElementById("UserEmployeeName").value;
    //离职日期
    search += "&OutDate=" + document.getElementById("txtOutDate").value;
    search += "&OutToDate=" + document.getElementById("txtOutToDate").value;
    
    //设置检索条件
    document.getElementById("hidSearchCondition").value = search;
    if (currPage == null || typeof(currPage) == "undefined")
    {
        TurnToPage(1);
    }
    else
    {
        TurnToPage(parseInt(currPage, 10));
    }
}
function CheckInput()
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

/*
* 重置页面
*/
function ClearInput()
{
    //调职单编号
    document.getElementById("txtNotifyNo").value = "";
    //调职单主题
    document.getElementById("txtTitle").value = "";
    //对应申请单
    document.getElementById("ddlApply").value = "";
    //申请人
    document.getElementById("hidEmployeeID").value = "";
    document.getElementById("UserEmployeeName").value = "";
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
var deleFlag = "0";//删除标识

/*
* 翻页处理
*/
function TurnToPage(pageIndex)
{
    //设置全选按钮为非选中状态
    document.getElementById("chkCheckAll").checked = false;
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
        url:  '../../../Handler/Office/HumanManager/EmplLeave_Query.ashx?' + postParam,//目标地址
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
                    {
                        var StrBillStatus="";
                        if(item.BillStatus=="1")StrBillStatus="未确认";else StrBillStatus="已确认";
                        
                        var No=item.NotifyNo;
                        var StrTitle=item.Title;
                        if(item.Title.length>10)
                            StrTitle=item.Title.substr(0,10)+"…";
                        if(item.NotifyNo.length>10)
                            No=item.NotifyNo.substr(0,10)+"…";
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'><input type='hidden' id='txtStatus_" + item.NotifyNo 
                            + "' value='" + item.BillStatus + "'>"
                            + "<input id='chkSelect' name='chkSelect' value='" + item.NotifyNo + "' type='checkbox' onclick=\"SetCheckAll(this, 'chkSelect', 'chkCheckAll');\" />" + "</td>" //选择框
                            + "<td height='22' align='center'><a href='" + GetLinkParam() + "&ID=" + item.ID + "') title='"+item.NotifyNo+"'>" + No + "</a></td>" //调职单编号
                            + "<td height='22' align='center'><font title='"+item.Title+"'>" + StrTitle + "</font></td>" //调职单主题
                            + "<td height='22' align='center'>" + item.MoveApplyNo + "</td>" //对应调职申请单
                            + "<td height='22' align='center'>" + item.EmployeeNo + "</td>" //员工编号
                            + "<td height='22' align='center'>" + item.EmployeeName + "</td>" //员工姓名
                            + "<td height='22' align='center'>" + item.DeptName + "</td>" //所属部门
                            + "<td height='22' align='center'>" + item.OutDate + "</td>" //离职时间
                            + "<td height='22' align='center'>" + StrBillStatus + "</td>").appendTo($("#tblDetailInfo tbody")//单据状态
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
            ShowTotalPage(msg.totalCount, pageCount, pageIndex,$("#pagecount"));
            $("#txtToPage").val(pageIndex);
        },
        error: function() 
        {
            popMsgObj.ShowMsg('请求发生错误！');
        },
        complete:function(){
            if (deleFlag == "0")
            {
                hidePopup();
            }
            else
            {
                deleFlag = "0";
            }
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
    //获取查询条件
    var searchCondition = document.getElementById("hidSearchCondition").value;
    if (searchCondition == null || searchCondition == "") return;
    TurnToPage(1);
}

/*
* 删除信息
*/
function DoDelete()
{
    //定义删除动作变量
    var action="del";
    //获取选择框
    var chkList = document.getElementsByName("chkSelect");
    var chkValue = ""; 
    var delConfirm = "";   
    for( var i = 0; i < chkList.length; i++ )
    {
        //判断选择框是否是选中的
        if ( chkList[i].checked )
        {
            //获取编号
            selectedNo = chkList[i].value;
            //获取单据状态
            billStatus = document.getElementById("txtStatus_" + selectedNo).value;
            if ("2" == billStatus)
            {
                delConfirm += selectedNo + ","; 
            }
            chkValue += "'" + selectedNo + "',";
        }
    }
    //没有选择时
    if(chkValue == "" || chkValue == null)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一项删除！");
    }
    //选择删除的记录中包含确认的记录时
    else if(delConfirm != "")
    {
        confirmNo = delConfirm.substring(0, delConfirm.length - 1);
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","已经确认的记录不能删除！\n" + confirmNo);
    }
    //执行删除
    else
    {
        if(!confirm("删除后将不可恢复，确认删除吗！"))
        {
            return;
        }
        var deleteNos = chkValue.substring(0, chkValue.length - 1);
        var postParam = "Action=" + action + "&DeleteNOs=" + escape(deleteNos);
        //删除
        $.ajax(
        { 
            type: "POST",
            url: "../../../Handler/Office/HumanManager/EmplLeave_Query.ashx?" + postParam,
            dataType:'json',//返回json格式数据
            cache:false,
            beforeSend:function()
            {  
                AddPop();
            },
            error: function() 
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
            }, 
            success:function(data) 
            {  
                deleFlag = "1";
                if(data.sta==1) 
                { 
                    TurnToPage(1);
                    hidePopup();
                    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除成功！");
                }
                else
                {
                    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除失败！");
                } 
            } 
        });
    }
}
/*
* 新建
*/
function DoNew()
{
    window.location.href = GetLinkParam();
}

/*
* 获取链接的参数
*/
function GetLinkParam()
{
    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    //是否点击了查询标识
    var flag = "0";//默认为未点击查询的时候
    if (searchCondition != "") flag = "1";//设置了查询条件时
    linkParam = "EmployeeLeave_Edit.aspx?FromPage=1&ModuleID=" + ModuleID 
                            + "&PageIndex=" + currentPageIndex + "&OrderBy=" + orderBy + "&PageCount=" + pageCount + "&" + searchCondition + "&Flag=" + flag;
    //返回链接的字符串
    return linkParam;
}
/*
* 导出
*/
function DoExport()
{
    alert("该功能还未完成！");
}