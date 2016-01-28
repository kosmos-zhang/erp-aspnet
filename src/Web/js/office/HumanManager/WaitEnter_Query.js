/*
* 查询
*/
function DoSearch(currPage)
{
var RetVal=CheckSpecialWords();
           var fieldText = "";
           var msgText = "";
           if(RetVal!="")
           {
                fieldText = fieldText + RetVal+"|";
   		        msgText = msgText +RetVal+  "不能含有特殊字符|";
   		        popMsgObj.Show(fieldText,msgText); 
   		        return;
           }
    var search = "";
    
    search += "EmployeeNo=" + document.getElementById("txtEmployeeNo").value;//编号
    search += "&EmployeeName=" + document.getElementById("txtEmployeeName").value;//姓名
    search += "&Sex=" + document.getElementById("ddlSex").value;//性别
    search += "&Culture=" + document.getElementById("ddlCulture_ddlCodeType").value;//学历
    search += "&QuarterID=" + document.getElementById("ddlQuarter").value;//应聘岗位
    search += "&SchoolName=" + document.getElementById("txtSchoolName").value;//毕业院校
    search += "&Flag=" + document.getElementById("DDLFlag").value;//人员类型
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
    //姓名
    document.getElementById("txtEmployeeName").value = "";
    //性别
    document.getElementById("ddlSex").value = "";
    //学历
    document.getElementById("ddlCulture_ddlCodeType").value = "";
    //应聘岗位
    document.getElementById("ddlQuarter").value = "";
    //毕业院校
    document.getElementById("txtSchoolName").value = "";
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
    //设置当前页
    currentPageIndex = pageIndex;
    //获取查询条件
    var searchCondition = document.getElementById("hidSearchCondition").value;
    var postParam = "PageIndex=" + pageIndex + "&PageCount=" + pageCount + "&Action=GetInfo&OrderBy=" + orderBy + "&" + searchCondition;
    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  '../../../Handler/Office/HumanManager/WaitEnter_Query.ashx?' + postParam,//目标地址
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
            var i=0;
            $.each(msg.data
                ,function(i,item)
                {
                    if(item.ID != null && item.ID != "")
                    {
                    var j=i++;
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"
                        + "<input id='txtCardID_" + j + "'value='" + item.CardID + "' type='hidden'/>"
                        + "<input id='txtQuarterID_" + j + "'value='" + item.QuarterID + "' type='hidden'/>"
                        + "<input id='rdoSelect' name='rdoSelect' value='" + item.ID + "' title='"+j+"' type='radio'/>" + "</td>" //选择框
                        + "<td height='22' align='center' id='tdNo_" + j + "'>" + item.EmployeeNo + "</td>" //编号
                        + "<td height='22' align='center' id='tdName_" + j + "'>" + item.EmployeeName + "</td>" //姓名
                        + "<td height='22' align='center'>" + item.SexName + "</td>" //性别
                        + "<td height='22' align='center'>" + item.QuarterName + "</td>" //应聘岗位
//                        + "<td height='22' align='center'>" + item.Birth + "</td>" //出身年月
//                        + "<td height='22' align='center'>" + item.Age + "</td>" //年龄
//                        + "<td height='22' align='center'>" + item.HomeAddress + "</td>"  //住址          
                        + "<td height='22' align='center'>" + item.Contact + "</td>"//联系方式
                        + "<td height='22' align='center'>" + item.CultureLevelName + "</td>" //学历
                        + "<td height='22' align='center'>" + item.SchoolName + "</td>"//毕业院校
                        + "<td height='22' align='center'>" + item.ProfessionalName + "</td>"// 专业
                        + "<td height='22' align='center'>" + item.Flag + "</td>"// 人员类型
                        + "<td height='22' align='center'><input id='Final_" + j + "' title='"+item.ID+"' value='" + item.FinalResult + "' type='hidden'/>" + item.FinalResult + "</td>").appendTo($("#tblDetailInfo tbody")//复试结果
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

/*
* 合同录入
*/
function DoInputContract()
{
    //获取选中的值
     var selectID = GetSelect();
    var i=GetSelecti();

    //没有选择时，提示信息
    if (selectID == "" || selectID == null)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一项！");
    }
    else
    {        
        //获取模块功能ID
        var ModuleID = document.getElementById("hidContractModuleID").value;
        //获取查询条件
        searchCondition = document.getElementById("hidSearchCondition").value;
        //获取员工名
        employeeName = document.getElementById("tdName_" + i).innerHTML;
        //是否点击了查询标识
        var flag = "0";//默认为未点击查询的时候
        if (searchCondition != "") flag = "1";//设置了查询条件时
        linkParam = "EmployeeContract_Edit.aspx?FromPage=0&UserID=" + selectID + "&UserName=" + employeeName + "&ModuleID=" + ModuleID 
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
    var selectID = "";
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

/*
* 获取列表中选中的值
*/
function GetSelecti()
{
    //获取选择框
    var radioList = document.getElementsByName("rdoSelect");
    //变量定义
    var selectID = "";
    for( var i = 0; i < radioList.length; i++ )
    {
        //判断选择框是否是选中的
        if ( radioList[i].checked )
        {
            //获取ID
            selectID = radioList[i].title;
            
            break;
        }
    }
    //返回选中值
    return selectID;
}

/*
* 入职处理
*/
function DoEnter(param)
{
    //获取选中的值
   var selectID = GetSelect();
    var i=GetSelecti();
    //没有选择时，提示信息
    if (selectID == "" || selectID == null)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一项！");
    }
    else
    {
        

        if(param=="1")
        {
            //人员ID
            document.getElementById("txtEnterEmployeeID").value = selectID;
            //人员编号
            document.getElementById("txtEnterEmployeeNo").value = document.getElementById("tdNo_" + i).innerHTML;
            //人员姓名
            document.getElementById("txtEnterEmployeeName").value = document.getElementById("tdName_" + i).innerHTML;
            //身份证号
            document.getElementById("txtEnterEmployeeCardID").value = document.getElementById("txtCardID_" + i).value;
            //岗位
            document.getElementById("ddlEnterQuarter").value = document.getElementById("txtQuarterID_" + i).value;
            //入职时间
            document.getElementById("txtEnterDate").value = document.getElementById("txtSystemDate").value;
            
            document.getElementById("divEnterInput").style.display = "block";

        }
        else
        {
            
            var FinalResultTag="Final_"+i;
            var FinalResultValue=document.getElementById(FinalResultTag).value;
            if(FinalResultValue!="拟予试用")
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","只有复试结果是拟予试用的才能入职！");
                return;
            }
            else
            {
                            //人员ID
            document.getElementById("txtEnterEmployeeID").value = selectID;
            //人员编号
            document.getElementById("txtEnterEmployeeNo").value = document.getElementById("tdNo_" + i).innerHTML;
            //人员姓名
            document.getElementById("txtEnterEmployeeName").value = document.getElementById("tdName_" + i).innerHTML;
            //身份证号
            document.getElementById("txtEnterEmployeeCardID").value = document.getElementById("txtCardID_" + i).value;
            //岗位
            document.getElementById("ddlEnterQuarter").value = document.getElementById("txtQuarterID_" + i).value;
            //入职时间
            document.getElementById("txtEnterDate").value = document.getElementById("txtSystemDate").value;
            
            document.getElementById("divEnterInput").style.display = "block";

            }
        }
    }
}

/*
* 保存信息
*/
function DoSave()
{
    /* 页面信息进行校验 */
    //基本信息校验 有错误时，返回
    if (CheckEnterInfo())
    {
        return;
    }
    //获取基本信息参数
    postParams = GetSaveParams();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/WaitEnter_Query.ashx?" + postParams,
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
            //隐藏提示框
            hidePopup();
            //保存成功
            if(data.sta == 1) 
            { 
                //设置提示信息
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
            }
            //保存失败
            else 
            { 
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
            }
        } 
    });  
}

/*
* 基本信息参数
*/
function GetSaveParams()
{
    //设置参数
    var param = "Action=Save";
    //人员ID
    param += "&EmployeeID=" + document.getElementById("txtEnterEmployeeID").value;
    //人员编号
    param += "&EmployeeNo=" + document.getElementById("txtEnterEmployeeNo").value;
    //部门ID
    param += "&Dept=" + document.getElementById("txtDept").value;
    //岗位ID
    param += "&QuarterID=" + document.getElementById("ddlEnterQuarter").value;
    //行政等级
    param += "&AdminLevelID=";
//     + document.getElementById("ddlEnterAdminLevel_ddlCodeType").value
    //岗位职等
    param += "&QuarterLevelID=" + document.getElementById("ddlEnterQuarterLevel_ddlCodeType").value;
    //职称
    param += "&PositionID=" + document.getElementById("ddlEnterPosition_ddlCodeType").value;
    //职务
    param += "&PositionTitle=" + document.getElementById("txtPositionTitle").value; 
    //入职时间
    param += "&EnterDate=" + document.getElementById("txtEnterDate").value;
    
    return param;
}

/*
* 基本信息校验
*/
function CheckEnterInfo()
{
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;
    
    //部门ID
    dept = document.getElementById("txtDept").value;
    if (dept == "" || dept == null)
    {
        isErrorFlag = true;
        fieldText += "部门|";
        msgText += "请输入部门|";  
    }
    //岗位职等
    quarterLevel = document.getElementById("ddlEnterQuarterLevel_ddlCodeType").value;
    if (quarterLevel == "" || quarterLevel == null)
    {
        isErrorFlag = true;
        fieldText += "岗位职等|";
        msgText += "请输入岗位职等|";  
    }
    //行政等级
//    adminLevel = document.getElementById("ddlEnterAdminLevel_ddlCodeType").value;
//    if (adminLevel == "" || adminLevel == null)
//    {
//        isErrorFlag = true;
//        fieldText += "行政等级|";
//        msgText += "请输入行政等级|";  
//    }
    //职称
//    position = document.getElementById("ddlEnterPosition_ddlCodeType").value;
//    if (position == "" || position == null)
//    {
//        isErrorFlag = true;
//        fieldText += "职称|";
//        msgText += "请输入职称|";  
//    }
    //职务
//    positionTitle = document.getElementById("txtPositionTitle").value;
//    if (positionTitle == "" || positionTitle == null)
//    {
//        isErrorFlag = true;
//        fieldText += "职务|";
//        msgText += "请输入职务|";  
//    }
    //入职时间
    enterDate = document.getElementById("txtEnterDate").value;
    if (enterDate == "" || enterDate == null)
    {
        isErrorFlag = true;
        fieldText += "入职时间|";
        msgText += "请输入入职时间|";  
    }
    
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {
        //显示错误信息
        popMsgObj.Show(fieldText, msgText);
    }

    return isErrorFlag;
}

/*
* 返回处理
*/
function DoBack()
{
    //部门ID
    document.getElementById("txtDept").value = "";
    //部门名称
    document.getElementById("DeptEnter").value = "";
    //岗位职等
    document.getElementById("ddlEnterQuarterLevel_ddlCodeType").selectedIndex = 0;
    //行政等级
//    document.getElementById("ddlEnterAdminLevel_ddlCodeType").selectedIndex = 0;
    //职称
    document.getElementById("ddlEnterPosition_ddlCodeType").selectedIndex = 0;
    //职务
    document.getElementById("txtPositionTitle").value = ""; 
    //入职时间
    document.getElementById("txtEnterDate").value = "";
    //隐藏入职页面
    document.getElementById("divEnterInput").style.display = "none";
    //进行查询操作
    TurnToPage(currentPageIndex);
}

/*
* 完全档案
*/
function DoInputAllInfo()
{
    //获取选中的值
   var selectID = GetSelect();
    //没有选择时，提示信息
    if (selectID == "" || selectID == null)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一项！");
    }
    else
    {        
        //获取模块功能ID
        var ModuleID = document.getElementById("hidEmplyModuleID").value;
        //获取查询条件
        searchCondition = document.getElementById("hidSearchCondition").value;
        //是否点击了查询标识
        var flag = "0";//默认为未点击查询的时候
        if (searchCondition != "") flag = "1";//设置了查询条件时
        linkParam = "EmployeeInfo_Add.aspx?FromPage=5&ID=" + selectID + "&ModuleID=" + ModuleID 
                                + "&PageIndex=" + currentPageIndex + "&PageCount=" + pageCount + "&" + searchCondition + "&Flag=" + flag;
        //返回链接的字符串
        window.location.href = linkParam;
    }
}