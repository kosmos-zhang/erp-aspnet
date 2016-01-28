$(document).ready(function(){
//  DoSearchInfo();
});





/*
* 编辑要素
*/
function DoNew()
{
    //编辑模式
    document.getElementById("hidEditFlag").value = "INSERT";
    //清除页面输入
    ClearElemInfo();
    //显示修改页面
    document.getElementById("divEditCheckItem").style.display = "block";
}

/*
* 清空输入
*/
function ClearElemInfo()
{
    //要素名称
    document.getElementById("txtEditElemName").value = "";
    //启用状态
    document.getElementById("sltEditUsedStatus").value = "1";
    //评分标准
    document.getElementById("txtStandard").value = "";
    //备注
    document.getElementById("txtRemark").value = "";
}

/*
* 修改要素信息
*/
function DoModify(elemID)
{
    //编辑模式
    document.getElementById("hidEditFlag").value = "UPDATE";
    //设置ID
    document.getElementById("hidElemID").value = elemID;
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/RectCheckElem_Edit.ashx?Action=GetInfo&ElemID=" + elemID,
        dataType:'json',//
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(msg) 
        {
            //隐藏提示框  
            hidePopup();
            /* 设置组织机构信息 */
            $.each(msg.data, function(i,item){
                //要素名称
                document.getElementById("txtEditElemName").value = item.ElemName;
                //启用状态
                document.getElementById("sltEditUsedStatus").value = item.UsedStatus;
                document.getElementById("hidBeforeUpdateUsedStatus").value = item.UsedStatus;
                //评分标准
                document.getElementById("txtStandard").value = item.Standard;
                //备注
                document.getElementById("txtRemark").value = item.Remark;
            });
        }
    });
    //显示修改页面
    document.getElementById("divEditCheckItem").style.display = "block"; 
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
        
    //要素名称必须输入
    if (document.getElementById("txtEditElemName").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "要素名称|";
        msgText += "请输入要素名称|";  
    }
    //评分标准
    standard = document.getElementById("txtStandard").value.Trim()
    if (standard == "" || standard == null)
    {
        isErrorFlag = true;
        fieldText += "评分标准|";
        msgText += "请输入评分标准|";  
    }
    else if (strlen(standard) > 1000)
    {
        isErrorFlag = true;
        fieldText += "评分标准|";
        msgText += "评分标准最多只允许输入1000个字符|";  
    }
    //备注
    remark = document.getElementById("txtRemark").value.Trim();
    if (remark != "" && remark != null && strlen(remark) > 200)
    {
        isErrorFlag = true;
        fieldText += "备注|";
        msgText += "备注最多只允许输入200个字符|";  
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
* 保存信息
*/
function DoSaveInfo()
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
        url: "../../../Handler/Office/HumanManager/RectCheckElem_Edit.ashx",
        data : "Action=EditInfo&" + postParams,
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
                //设置编辑模式
                document.getElementById("hidEditFlag").value = data.info;
                //设置ID 
                document.getElementById("hidElemID").value = data.data;
                //设置提示信息
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
            }
            //保存成功
            else if(data.sta == 2) 
            { 
                //设置提示信息
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","该要素已经被使用，您不能将其停用！");
            }
            //保存失败
            else 
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
            }
        } 
    });  
}

/*
* 基本信息参数
*/
function GetBaseInfoParams()
{
    //获取编辑模式
    editFlag = document.getElementById("hidEditFlag").value;
    //新建时，编号选择手工输入时
    var param = "EditFlag=" + editFlag;
    //要素名称
    param += "&ElemID=" + document.getElementById("hidElemID").value.Trim();
    //要素名称
    param += "&ElemName=" + escape(document.getElementById("txtEditElemName").value.Trim());
    //启用状态
    param += "&UsedStatus=" + document.getElementById("sltEditUsedStatus").value.Trim();
    param += "&BeforeUpdateUsedStatus=" + document.getElementById("hidBeforeUpdateUsedStatus").value.Trim();
    //评分标准
    param += "&Standard=" + escape(document.getElementById("txtStandard").value.Trim());
    //备注
    param += "&Remark=" + escape(document.getElementById("txtRemark").value.Trim());
    
    return param;
}

/*
* 返回操作
*/
function DoBack()
{
    //清除页面输入
    ClearElemInfo();
    //隐藏修改页面
    document.getElementById("divEditCheckItem").style.display = "none"; 
    //执行查询
    TurnToPage(1);
}


function fnCheckInfo() {
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;

    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isErrorFlag = true;
        var tmpKeys = RetVal.split(',');
        for (var i = 0; i < tmpKeys.length; i++) {
            isErrorFlag = true;
            fieldText = fieldText + tmpKeys[i].toString() + "|";
            msgText = msgText + "不能含有特殊字符|";
        }
    }
    if (isErrorFlag) {
        popMsgObj.Show(fieldText, msgText);
    }
    return isErrorFlag;
    
    
    
}
/*
* 执行查询
*/
function DoSearchInfo(currPage) {


    if (fnCheckInfo())
        return;
    var search = "";
    //要素名称
    search += "ElemName=" +escape (  document.getElementById("txtSearchElemName").value.Trim());
    //启用状态
    search += "&UsedStatus=" + escape ( document.getElementById("sltSearchUsedStatus").value.Trim());
    
    //设置检索条件
    document.getElementById("hidSearchCondition").value = search;
    
    TurnToPage(1);
}

/*
* 重置页面
*/
function ClearInput()
{
    //要素名称
    document.getElementById("txtSearchElemName").value = "";
    //启用状态
    document.getElementById("sltSearchUsedStatus").value = "";
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
var orderBy = "ModifiedDate_d";//排序字段
var deleFlag = "0";//删除标识


/*
* 翻页处理
*/
function TurnToPage(pageIndex)
{
    //设置当前页
    currentPageIndex = pageIndex;
    //获取查询条件
//    var searchCondition = document.getElementById("hidSearchCondition").value;
       var searchCondition = document.getElementById("hidSearchCondition").value;
          if (searchCondition==""||searchCondition=="undefined")
    {
    return ;
    }
    //设置动作种类
    var action="SearchInfo";
    var postParam = "PageIndex=" + pageIndex + "&PageCount=" + pageCount + "&Action=" + action + "&OrderBy=" + orderBy + "&" + searchCondition;
    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  '../../../Handler/Office/HumanManager/RectCheckElem_Edit.ashx?' + postParam,//目标地址
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
                        + "<input id='chkSelect' name='chkSelect' value='" + item.ID + "'  type='checkbox'  onclick=\"SetCheckAll(this, 'chkSelect', 'chkCheckAll');\"   />" + "</td>" //选择框
                        + "<td height='22' align='center'><a href='#' onclick='DoModify(\"" + item.ID + "\");'>" + item.ElemName + "</a></td>" //要素名称
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
* 删除面试要素信息
*/
function DoDelete()
{
    //获取选择框
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
        if(!confirm("删除后将不可恢复，确认删除吗！"))
        {
            return;
        }
        var postParam = "Action=DeleteInfo&DeleteNO=" + escape(deleteNos);
        //删除
        $.ajax(
        { 
            type: "POST",
            url: "../../../Handler/Office/HumanManager/RectCheckElem_Edit.ashx?" + postParam,
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
                if(data.sta == 1) 
                { 
                    //
                    TurnToPage(1);
                    //
                    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除成功！");
                } 
                else if(data.sta == 2) 
                {
                    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","要素已经被使用！");
                }
                else
                {
                    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除失败！");
                } 
            } 
        });
    }
}
