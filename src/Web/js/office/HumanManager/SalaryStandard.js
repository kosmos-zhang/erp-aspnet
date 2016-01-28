$(document).ready(function(){
//  DoSearch();
});
function getChage( obj)
{

if (obj.checked==false )
{
 document .getElementById ("chkCheckAll").checked=false ;
}
}
/*
* 查询
*/
function DoSearch()
{
    var search = "";
    //岗位
    search += "QuarterID=" +escape (  document.getElementById("ddlSearchQuarter").value.Trim());
    //岗位等级
    search += "&AdminLevel=" + escape ( document.getElementById("ctSearchQuaterAdmin_ddlCodeType").value.Trim());
    //启用状态
    search += "&UsedStatus=" + escape ( document.getElementById("ddlSearchUsedStatus").value.Trim());
    
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
    //岗位
    document.getElementById("ddlSearchQuarter").value = "";
    //岗位等级
    document.getElementById("ctSearchQuaterAdmin_ddlCodeType").value = "";
    //启用状态
    document.getElementById("ddlSearchUsedStatus").value = "";
}

/*
* 改变页面显示
*/
function ChangePageCountIndex(newPageCount, newPageIndex)
{
    //判断是否是数字
    if (!IsNumber(newPageCount))
    {
       showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请输入正确的显示条数！");
      //  popMsgObj.ShowMsg('请输入正确的显示条数！');
        return;
    }
    if (!IsNumber(newPageIndex))
    {
      showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请输入正确的转到页数！");
      //  popMsgObj.ShowMsg('请输入正确的转到页数！');
        return;
    }
    //判断重置的页数是否超过最大页数
    if(newPageCount <=0 || newPageIndex <= 0 || newPageIndex > ((totalRecord - 1)/newPageCount) + 1)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转至页数超出查询范围！");
       // popMsgObj.ShowMsg('转至页数超出查询范围！');
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
    
       if (searchCondition==""||searchCondition=="undefined")
    {
    return ;
    }
    var postParam = "PageIndex=" + pageIndex + "&PageCount=" + pageCount + "&Action=Search&OrderBy=" + orderBy + "&" + searchCondition;
    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  "../../../Handler/Office/HumanManager/SalaryStandard.ashx?" + postParam,//目标地址
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
                    if(item.ID != null && item.ID != "")
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"
                      + "<input id='chkSelect' name='chkSelect' value='" + item.ID + "'  type='checkbox' onpropertychange='getChage(this)'  />"  //选择框
//                        + "<input name='rdoSelect' value='" + item.ID + "' type='radio'/>" 
                        + "<input id='txtRemark_" + item.ID + "' value='" + item.Remark + "' type='hidden'/>" //备注
                      + "<input type='hidden' id='txtQuarterID_" + item.ID + "' value='" + item.QuarterID + "' />"//岗位
                        + "<input type='hidden' id='txtAdminLevel_" + item.ID + "' value='" + item.AdminLevel + "' />"//岗位等级
                        + "<input type='hidden' id='txtItemNo_" + item.ID + "' value='" + item.ItemNo + "' />"//工资项编号
                        + "<input type='hidden' id='txtUsedStatus_" + item.ID + "' value='" + item.UsedStatus + "' />"//启用状态
                        + "</td>" //选择框
                        + "<td height='22' align='center'>"   + "<a href='#' onclick='DoModify(\"" + item.ID + "\");'>" + item.QuarterName + "</a></td>" //岗位名称
                        + "<td height='22' align='center'>" + item.AdminLevelName + "</td>" //岗位等级名称
                        + "<td height='22' align='center'>" + item.ItemName + "</td>" //工资项名称
                        + "<td height='22' align='center' id='tdUnitPrice_" + item.ID + "'>" + item.UnitPrice + "</td>" //金额
                        + "<td height='22' align='center'>" + item.UsedStatusName + "</td>").appendTo($("#tblDetailInfo tbody")//启用状态
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

/*
* 修改处理
*/
function DoModify(selectID)
{
    //获取选中的值
//    selectID = GetSelect();
    //没有选择时，提示信息
    if (selectID == "" || selectID == null)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一项！");
    }
    else
    {
        //岗位
        document.getElementById("ddlQuarter").value = document.getElementById("txtQuarterID_" + selectID).value.Trim();
        //岗位职等
        document.getElementById("ctQuaterAdmin_ddlCodeType").value = document.getElementById("txtAdminLevel_" + selectID).value.Trim();
        //工资项
        document.getElementById("ddlSalaryItem").value = document.getElementById("txtItemNo_" + selectID).value.Trim();
        //金额
        document.getElementById("txtUnitPrice").value = document.getElementById("tdUnitPrice_" + selectID).innerHTML;
        //启用状态
        document.getElementById("ddlUsedStatus").value = document.getElementById("txtUsedStatus_" + selectID).value.Trim();
        //备注 
        document.getElementById("txtRemark").value = document.getElementById("txtRemark_" + selectID).value.Trim();
        //ID 
        document.getElementById("hidID").value = selectID;
        //岗位
        document.getElementById("ddlQuarter").disabled = true;
        //岗位职等
        document.getElementById("ctQuaterAdmin_ddlCodeType").disabled = true;
        //工资项
        document.getElementById("ddlSalaryItem").disabled = true;
        
        //显示修改页面
        document.getElementById("divEditSalary").style.display = "block";
    }
}

/*
* 新建处理
*/
function DoNew()
{ 
    //清除页面编辑内容
    ClearEditInfo();
    //显示修改页面
    document.getElementById("divEditSalary").style.display = "block";
}

/*
* 保存处理 
*/
function DoSave()
{
    /* 页面信息进行校验 */
    //基本信息校验 有错误时，返回
    if (CheckBaseInfo())
    {
        return;
    }
    //获取基本信息参数
    postParams = GetBaseInfoParams();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/SalaryStandard.ashx?" + postParams,
        dataType:'json',//返回json格式数据
        cache:false,
        beforeSend:function()
        {
            AddPop();
        }, 
        error: function()
        {
           // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
            document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("提示|", "请求发生错误！|");
        }, 
        success:function(data) 
        {
            //隐藏提示框
            hidePopup();
            //保存成功
            if(data.sta == 1) 
            { 
                //设置ID
                document.getElementById("hidID").value = data.info;
                //岗位不可编辑
                document.getElementById("ddlQuarter").disabled = true;
                //岗位职等不可编辑
                document.getElementById("ctQuaterAdmin_ddlCodeType").disabled = true;
                //工资项不可编辑
                document.getElementById("ddlSalaryItem").disabled = true;
                //设置提示信息
            //    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
                document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("提示|", "保存成功！|");
            }
            //编号已存在
            else if(data.sta == 2)
            {
             //   showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","该工资项已经被设置，请确认！");
                
                 document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("提示|", "该工资项已经被设置，请确认！|");
            }
            //保存失败
            else 
            { 
//                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
                      document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv("提示|", "保存失败,请确认！|");
            }
        } 
    }); 
}

/*
* 基本信息参数
*/
function GetBaseInfoParams()
{
    //新建时，编号选择手工输入时
    var param = "Action=Save";
    //ID 
    param += "&ID=" + escape ( document.getElementById("hidID").value.Trim());
    //岗位
    param += "&QuarterID=" + escape ( document.getElementById("ddlQuarter").value.Trim());
    //岗位职等
    param += "&AdminLevel=" + escape ( document.getElementById("ctQuaterAdmin_ddlCodeType").value.Trim());
    //工资项
    param += "&ItemNo=" + escape ( document.getElementById("ddlSalaryItem").value.Trim());
    //金额
    param += "&UnitPrice=" + escape ( document.getElementById("txtUnitPrice").value.Trim());
    //启用状态
    param += "&UsedStatus=" +escape (  document.getElementById("ddlUsedStatus").value.Trim());
    //备注 
    param += "&Remark=" + escape ( document.getElementById("txtRemark").value.Trim());
    
    return param;
}

/*
* 基本信息校验
*/
function CheckBaseInfo()
{
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;
        
    //岗位
    inputInfo = document.getElementById("ddlQuarter").value.Trim();
    if (inputInfo == "" || inputInfo == null)
    {
        isErrorFlag = true;
        fieldText += "岗位|";
        msgText += "请输入岗位|";  
    }
    //岗位职等
    inputInfo = document.getElementById("ctQuaterAdmin_ddlCodeType").value.Trim();
    if (inputInfo == "" || inputInfo == null)
    {
        isErrorFlag = true;
        fieldText += "岗位职等|";
        msgText += "请输入岗位职等|";  
    }
    //工资项
    inputInfo = document.getElementById("ddlSalaryItem").value.Trim();
    if (inputInfo == "" || inputInfo == null)
    {
        isErrorFlag = true;
        fieldText += "工资项|";
        msgText += "请输入工资项|";  
    }
    //金额
    inputInfo = document.getElementById("txtUnitPrice").value.Trim();
    if (inputInfo == "" || inputInfo == null)
    {
        isErrorFlag = true;
        fieldText += "金额|";
        msgText += "请输入金额|";  
    }
    else
    {
        if (!IsNumeric(inputInfo, 10, 2))
        {  isErrorFlag = true;
        fieldText += "金额|";
        msgText += "请输入正确的金额|";  
        }
    }
	
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {        
       // popMsgObj.Show(fieldText, msgText);
    document.getElementById("spanMsg").style.display = "block";
	    document.getElementById("spanMsg").innerHTML = CreateErrorMsgDiv(fieldText, msgText);
    }
    
    return isErrorFlag;
}

///////创建层（）
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
* 删除处理 
*/
function DoDelete()
{
    //获取选择框
      if(confirm("删除后不可恢复，确认删除吗！"))
      {
    var chkList = document.getElementsByName("chkSelect");
    var chkValue = "";    
    for( var i = 0; i < chkList.length; i++ )
    {
        //判断选择框是否是选中的
        if ( chkList[i].checked )
        {
           chkValue += "'" + chkList[i].value + "',";
        }
    }
    var deleteNos = chkValue.substring(0, chkValue.length - 1);
    selectLength = chkValue.split("',");
    if(chkValue == "" || chkValue == null || selectLength.length < 1)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一项删除！");
        return;
    }
    else
    {
        var postParam = "Action=DeleteInfo&DeleteNO=" + escape(deleteNos);
        //删除
        $.ajax(
        { 
            type: "POST",
            url: "../../../Handler/Office/HumanManager/SalaryStandard.ashx?" + postParam,
            dataType:'json',//返回json格式数据
            cache:false,
            beforeSend:function()
            { 

            },
            error: function() 
            {
              ///  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                 popMsgObj.ShowMsg('请求发生错误！');
            }, 
            success:function(data) 
            { 
                if(data.sta == 1) 
                { 
                    // 
                    // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                       showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除成功！");
                    TurnToPage(1);
                    //
                  
                } 
                else if(data.sta == 2) 
                {
                    //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                     popMsgObj.ShowMsg('类型已被使用 ,请确认！');
                }
                else
                {
                   // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                     popMsgObj.ShowMsg('删除失败 ,请确认！');
                } 
            } 
        });
    }
    }
}

/*
* 导出处理 
*/
function DoExport()
{
    alert("导出处理,暂时不做");
}

/*
* 返回处理
*/
function DoBack()
{ 
    //清除页面编辑内容
    ClearEditInfo();
    //显示修改页面
    document.getElementById("divEditSalary").style.display = "none";
    if (document.getElementById("hidSearchCondition").value.Trim() != null 
            && document.getElementById("hidSearchCondition").value.Trim() != "")
    {
        //跳转页面
        TurnToPage(currentPageIndex);
    }
}

/*
* 清除页面编辑内容
*/
function ClearEditInfo()
{
    //岗位
     document.getElementById("ddlQuarter").value = '';
    //岗位职等
    document.getElementById("ctQuaterAdmin_ddlCodeType").value = '';
    //工资项
    document.getElementById("ddlSalaryItem").value = '';
    //金额
    document.getElementById("txtUnitPrice").value = '';
    //启用状态
    document.getElementById("ddlUsedStatus").value = "1";
    //备注 
    document.getElementById("txtRemark").value = '';
    //ID 
    document.getElementById("hidID").value = '';
    //岗位
    document.getElementById("ddlQuarter").disabled = false;
    //岗位职等
    document.getElementById("ctQuaterAdmin_ddlCodeType").disabled = false;
    //工资项
    document.getElementById("ddlSalaryItem").disabled = false;
}