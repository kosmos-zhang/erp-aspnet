//页面初期表示显示数据
$(document).ready(function(){
//      TurnToPage(1);
});
/*
* 执行查询
*/
function DoSearch()
{

    var search = "";
    //人员编号
    search += "EmployeeNo=" + document.getElementById("txtEmployeeNo").value;
    //姓名
    search += "&EmployeeName=" + escape ( document .getElementById("txtName").value);
    //所在部门
   // search += "&DeptID=" + document.getElementById("txtDeptID").value;
        search += "&DeptID=" +escape ( document.getElementById("DeptEmployee").value);
    //岗位
    search += "&QuarterID=" + document.getElementById("ddlQuarter").value;
    //岗位职等
    search += "&AdminLevelID=" + document.getElementById("ddlAdminLevel_ddlCodeType").value;
    
    //设置检索条件
    document.getElementById("hidSearchCondition").value = search;
    
    var isFlag=true ;
    var fieldText="";
    var msgText="";
    var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
            isFlag = false;
            fieldText = fieldText + RetVal+"|";
   		    msgText = msgText +RetVal+  "不能含有特殊字符|";
   		    
    }
     if(!isFlag)
    {
        //显示错误信息
        //popMsgObj.Show(fieldText, msgText);
        
	    document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv(fieldText, msgText);
	    return ;
    }
    
    //执行查询
    TurnToPage(1);
}


function CreateErrorMsgDiv(fieldText, msgText)
{
    errorMsg = "";
    if(fieldText != null && fieldText != "" && msgText != null && msgText != "")
    {
        var fieldArray = fieldText.split("|");
        var alertArray = msgText.split("|");
        for(var i = 0; i < fieldArray.length - 1; i++)
        {
            errorMsg += "<strong>[</strong><font color=\"red\">" + fieldArray[i].toString()
                        + "</font><strong>]</strong>：" + alertArray[i].toString() + "<br />";
        }
    }
    table = "<table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#FFFFFF'>"
            + "<tr><td align='center' height='1'>&nbsp;</td></tr>"
            + "<tr><td align='center'>" + errorMsg + "</td></tr>"
            + "<tr><td align='right'>"
            + "<img src='../../../Images/Button/closelabel.gif' onclick=\"document.getElementById('spanMsg').style.display='none';\" />"
            + "&nbsp;&nbsp;</td></tr></table>";
	
	return table;
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
    //所在部门
    document.getElementById("txtDeptID").value = "";
    document.getElementById("DeptEmployee").value = "";
    //岗位
    document.getElementById("ddlQuarter").value = "";
    //岗位职等
    document.getElementById("ddlAdminLevel_ddlCodeType").value = "";
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
        url:  '../../../Handler/Office/HumanManager/SelectEmployee_Query.ashx?' + postParam,//目标地址
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
                        + "<input id='rdoSelect' name='rdoSelect' value='" + item.ID + "' type='radio'/>"
                        + "<input type='hidden' id='txtDeptID_" + item.ID + "' value='" + item.Dept +"' />"
                        + "<input type='hidden' id='txtQuarterID_" + item.ID + "' value='" + item.QuarterID +"' />"
                        + "<input type='hidden' id='txtLevelID_" + item.ID + "' value='" + item.AdminLevelID +"' />"
                        + "</td>" //选择框
                        + "<td height='22' align='center' id='tdNo_" + item.ID + "'>" + item.EmployeeNo + "</td>" //编号
                        + "<td height='22' align='center' id='tdName_" + item.ID + "'>" + item.EmployeeName + "</td>" //姓名
                        + "<td height='22' align='center' id='tdNum_" + item.ID + "'>" + item.EmployeeNum + "</td>" //工号
                        + "<td height='22' align='center' id='tdDept_" + item.ID + "'>" + item.DeptName + "</td>" //所在部门
                        + "<td height='22' align='center' id='tdQuarter_" + item.ID + "'>" + item.QuarterName + "</td>" //岗位
                        + "<td height='22' align='center' id='tdLevel_" + item.ID + "'>" + item.AdminLevelName + "</td>" //岗位职等
                        + "<td height='22' align='center' id='tdDate_" + item.ID + "'>" + item.EnterDate + "</td>").appendTo($("#tblDetailInfo tbody")//入职时间
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
    emplID = "";
    for( var i = 0; i < radioList.length; i++ )
    {
        //判断选择框是否是选中的
        if ( radioList[i].checked )
        {
            //获取ID
            emplID = radioList[i].value;
            
            break;
        }
    }
    //没有选择时，提示信息
    if (emplID == "" || emplID == null)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一项！");
    }
    else
    {
        window.parent.SetEmployeeInfo(new TemplateEmployee(emplID));
    }
}

/*
*/
function TemplateEmployee(emplID)
{
    this.EmployeeID = emplID;//员工ID
    this.EmployeeNo = document.getElementById("tdNo_" + emplID).innerHTML;//员工编号
    this.EmployeeName = document.getElementById("tdName_" + emplID).innerHTML;//员工姓名
    this.EmployeeNum = document.getElementById("tdNum_" + emplID).innerHTML;//员工工号
    this.EnterDate = document.getElementById("tdDate_" + emplID).innerHTML;//入职时间
    this.DeptName = document.getElementById("tdDept_" + emplID).innerHTML;//所属部门名称
    this.DeptID = document.getElementById("txtDeptID_" + emplID).value;//所属部门ID
    this.QuarterName = document.getElementById("tdQuarter_" + emplID).innerHTML;//所在岗位名称
    this.QuarterID = document.getElementById("txtQuarterID_" + emplID).value;//所在岗位ID
    this.AdminLevelName = document.getElementById("tdLevel_" + emplID).innerHTML;//岗位职等名称
    this.AdminLevelID = document.getElementById("txtLevelID_" + emplID).value;//岗位职等ID
}

/*
* 返回处理
*/
function DoBack()
{ 
    window.parent.SetEmployeeInfo(null);
}