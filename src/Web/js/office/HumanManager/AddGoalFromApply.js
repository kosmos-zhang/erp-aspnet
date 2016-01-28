/* 分页相关变量定义 */  

var pageCount = 10;//每页显示记录数
var totalRecord = 0;//总记录数
var pagerStyle = "flickr";//jPagerBar样式
var currentPageIndex = 1;//当前页
var orderBy = "";//排序字段

//页面初期表示显示数据
$(document).ready(function(){
      TurnToPage(currentPageIndex);
});
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

/*
* 翻页处理
*/
function TurnToPage(pageIndex)
{
    //设置当前页
    currentPageIndex = pageIndex;
    var postParam = "PageIndex=" + pageIndex + "&PageCount=" + pageCount + "&OrderBy=" + orderBy;
    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  '../../../Handler/Office/HumanManager/AddGoalFromApply.ashx?' + postParam,//目标地址
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
                    var JobName=item.JobName;
                        if(item.JobName.length>10)
                            JobName=item.JobName.substr(0,10)+"…";
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"
                            + "<input id='chkSelect' name='chkSelect' value='" + i + "' type='checkbox'/>" + "</td>" //选择框
                            + "<td height='22' align='center' id='tdDeptName_" + i + "'>" + item.DeptName + "</td>" //部门名称
                            + "<td height='22' align='center' id='tdJobName_" + i + "' title='"+item.JobName+"'>" + JobName + "</td>" //岗位名称
                            + "<td height='22' align='center' id='tdRectCount_" + i + "'>" + item.RectCount + "</td>" //人员数量
                            + "<td height='22' align='center' id='tdSexName_" + i + "'>" + item.SexName + "</td>"  //性别名称
                            + "<td height='22' align='center' id='tdAge_" + i + "'>" + item.Age + "</td>" //年龄  
                            + "<td height='22' align='center' id='tdCultureLevelName_" + i + "'>" + item.CultureLevelName + "</td>" //学历名称
                            + "<td height='22' align='center' id='tdProfessionalName_" + i + "'>" + item.ProfessionalName + "</td>"  //专业名称  
                            + "<td height='22' align='center' id='tdWorkNeeds_" + i + "'>" + item.WorkNeeds + "</td>" //工作要求
                            + "<td height='22' align='center' id='tdWorkAgeName_" + i + "'>" + item.WorkAgeName + "</td>" //工作要求
                            + "<td height='22' align='center' id='tdCompleteDate_" + i + "'>" + item.CompleteDate + "</td>"//完成时间
                            + "<td height='22' align='center' style='display:none' id='tdDeptID_" + i + "'>" + item.DeptID + "</td>" //部门ID
                            + "<td height='22' align='center' style='display:none' id='tdSexID_" + i + "'>" + item.SexID + "</td>"  //性别ID  
                            + "<td height='22' align='center' style='display:none' id='tdWorkAge_" + i + "'>" + item.WorkAge + "</td>"  //性别ID   
                            + "<td height='22' align='center' style='display:none' id='tdCultureLevelID_" + i + "'>" + item.CultureLevelID + "</td>" //学历ID
                                  + "<td height='22' align='center' style='display:none' id='tdPositionID_" + i + "'>" + item.PositionID + "</td>" //岗位ID
                            + "<td height='22' align='center' style='display:none' id='tdProfessionalID_" + i + "'>" + item.ProfessionalID + "</td>").appendTo($("#tblDetailInfo tbody")//专业ID 
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
* 添加处理
*/
function DoAdd()
{
    //获取选择框
    var chkList = document.getElementsByName("chkSelect");
    var hasSelect = false;
    var goalInfo = new Array();
    for( var i = 0; i < chkList.length; i++ )
    {
        //判断选择框是否是选中的
        if ( chkList[i].checked )
        {
            //获取行号
            chkValue = chkList[i].value;
               deptName = document.getElementById("tdDeptName_" + chkValue).innerHTML;
                WorkAge = document.getElementById("tdWorkAge_" + chkValue).innerHTML;
              
            //获取部门ID
            deptID = document.getElementById("tdDeptID_" + chkValue).innerHTML;
            //职务名称
            jobName = document.getElementById("tdJobName_" + chkValue).title;
                   PositionID = document.getElementById("tdPositionID_" + chkValue).innerHTML;
            
            //人员数量
            rectCount = document.getElementById("tdRectCount_" + chkValue).innerHTML;
            //性别
            sexID = document.getElementById("tdSexID_" + chkValue).innerHTML;
            //年龄
            age = document.getElementById("tdAge_" + chkValue).innerHTML;
            //学历
            cultureLevelID = document.getElementById("tdCultureLevelID_" + chkValue).innerHTML;
            //专业
            professionalID = document.getElementById("tdProfessionalID_" + chkValue).innerHTML;
            //要求
            workNeeds = document.getElementById("tdWorkNeeds_" + chkValue).innerHTML;
            //计划完成时间
            completeDate = document.getElementById("tdCompleteDate_" + chkValue).innerHTML;
            //定义数据
            var goal = new TempInfo(PositionID,deptName ,WorkAge ,deptID, jobName, rectCount, sexID, age, cultureLevelID, professionalID, workNeeds, completeDate);
            //添加数据
            goalInfo.push(goal);
            //设置选中标识
            hasSelect = true;
            
        }
    }
    //没有选择时
    if (!hasSelect)
    {
        popMsgObj.ShowMsg('请至少选择一条记录添加！');
        return;
    }
    else
    {
        window.parent.SetGoalFromApply(goalInfo);
    }
}

/*
* 选中记录的数据模板
* deptID            部门ID
* jobName           岗位名称
* rectCount         人员数量
* sexID             性别ID
* age               年龄
* cultureLevelID    学历ID
* professionalID    专业ID
* workNeeds         工作要求
* completeDate      完成时间
*/
function TempInfo(positionID,deptName ,WorkAge,deptID, jobName, rectCount, sexID, age, cultureLevelID
                                            , professionalID, workNeeds, completeDate)
{
this.PositionID=positionID;
this .DeptName=deptName ;
this.workAge=WorkAge ;
    this.DeptID = deptID; //部门ID
    this.JobName = jobName;//职务名称
    this.RectCount = rectCount;//人员数量
    this.SexID = sexID;//性别ID
    this.Age = age;//年龄
    this.CultureLevelID = cultureLevelID;//学历ID
    this.ProfessionalID = professionalID;//专业
    this.WorkNeeds = workNeeds;//工作要求
    this.CompleteDate = completeDate;//完成时间
}

/*
* 返回处理
*/
function DoBack()
{
    window.parent.SetGoalFromApply(null);
}