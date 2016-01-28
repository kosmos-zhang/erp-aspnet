/*
* 添加要素
*/
function AddElem()
{    
    //查询数据
    TurnToPage(1);    
    //显示DIV
    document.getElementById("divCheckElem").style.display = "block";
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
    //设置动作种类
    var action="GetElem";
    var postParam = "PageIndex=" + pageIndex + "&PageCount=" + pageCount + "&Action=" + action + "&OrderBy=" + orderBy;
    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  '../../../Handler/Office/HumanManager/RectCheckTemplate_Edit.ashx',
        data : postParam,//目标地址
        dataType:"json",//数据格式:JSON
        cache:false,
        beforeSend:function()
        {
            AddPop();
        },//发送数据之前
        success: function(msg)
        {
            hidePopup();
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#tblDetailInfo tbody").find("tr.newrow").remove();
            $.each(msg.data
                ,function(i,item)
                {
                    if(item.ID != null && item.ID != "")
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"
                        + "<input id='chkSelect' name='chkSelect' value='" + item.ID + "' type='checkbox' onclick=\"SetCheckAll(this, 'chkSelect', 'chkCheckAll');\" /></td>"//选择框
                        + "<td height='22' align='left' id='tdElemName_" + item.ID + "'>" + item.ElemName + "</td>" //要素名称
                        + "<td height='22' align='left'>" + item.Standard + "</td>").appendTo($("#tblDetailInfo tbody")//评分标准
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

function ggg()
{
alert (document.getElementById("tblElemDetail").innerHTML);
}
/*
* 确认处理
*/
function DoConfirm()
{
//debugger;
    pageCount = 10;   
    //获取选择框
    var chkList = document.getElementsByName("chkSelect");
    var hadElem = "";
    for( var i = 0; i < chkList.length; i++ )
    {
        //判断选择框是否是选中的
        if (chkList[i].checked)
        {
            //获取要素ID
            elemID = chkList[i].value;
            //获取要素名称
            elemName = document.getElementById("tdElemName_" + elemID).innerHTML;
            //获取表格
            table = document.getElementById("tblElemDetail");
            //获取行号
            var no = table.rows.length;
            var isExist = false;
            //校验要素是否存在
            
            for (var j = 1; j < no; j++)
            {
                //获取已添加的要素ID
              if (  table.rows[j].style.display != "none")
              {
                var tblElemID = document.getElementById("hidElemID_" + j).value.Trim();
                //判断是否相等
                if (tblElemID == elemID)
                {
                    isExist = true;
                    break;
                }
              }
            }
            //如果重复，提示消息
            if (isExist)
            {
                hadElem += elemName + ",";
                break ;
            }
            else
            {
                //添加到表格中
                AddElemToTable(elemID, elemName);
            }
        }
    } 
    if (hadElem != "")
    {
        //设置提示信息
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif", "要素【" + hadElem.substring(0, hadElem.length - 1) + "】已经添加。");
    }  
    //隐藏DIV
    document.getElementById("divCheckElem").style.display = "none";
}

/*
*
*/
function DoCancel()
{   
    pageCount = 10; 
    //隐藏DIV
    document.getElementById("divCheckElem").style.display = "none";
}

/*
* 增加要素到Table中
*/
function AddElemToTable(elemID, elemName){
    //获取表格
    table = document.getElementById("tblElemDetail");
    //获取行号
    var no = table.rows.length;
    //添加新行
	var objTR = table.insertRow(-1);
	/* 添加行的单元格 */
	//选择框
	var objTD = objTR.insertCell(-1);
	objTD.className = "tdColInputCenter";
	objTD.innerHTML = "<input type='checkbox' id='chkSelect_" + no + "'>"
	    + "<input type='hidden' id='hidElemID_" + no + "' value='" + elemID + "'>";
	//要素名称
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = elemName;
	//满分
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '6'  style='width:95%'   class='tdinput' id='txtMaxScore_" + no + "' value=''>";
	//权重
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '6'  style='width:95%'  class='tdinput' id='txtRate_" + no + "' value=''>";
	//备注
	objTD = objTR.insertCell(-1);
	objTD.className = "tdColInput";
	objTD.innerHTML = "<input type='text' maxlength = '100'    style='width:95%'    class='tdinput' id='txtElemRemark_" + no + "' value=''>";
}

/*
* 删除要素
*/
function DeleteElem()
{
    //获取表格
    table = document.getElementById("tblElemDetail");
    //获取表格行数
	var count = table.rows.length;
	if (count < 2) return;
	//遍历表格中的数据，判断是否选中
	for (var row = count - 1; row > 0; row--)
	{
	    //获取选择框控件
	    chkControl = document.getElementById("chkSelect_" + row);
	    //如果控件为选中状态，删除该行，并对之后的行改变控件名称
	    if (chkControl.checked)
	    {
	       //删除行，实际是隐藏该行
	       table.rows[row].style.display = "none";
	    }
	}
}

/*
* 全选择checkbox
*/
function SelectAll()
{
    //获取表格
    table = document.getElementById("tblElemDetail");
    //获取表格行数
	var count = table.rows.length;
	if (count < 2) return;
	var isSelectAll = document.getElementById("chkAll").checked;
	//遍历表格中的数据，判断是否选中
	for (var row = count - 1; row > 0; row--)
	{
	    //获取选择框控件
	    chkControl = document.getElementById("chkSelect_" + row);
	    //全选择
	    if (isSelectAll)
	    {
	        chkControl.checked = true;
	    }
	    else
	    {
	        chkControl.checked = false;
	    }
	}
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
    
    //获取编辑模式
    editFlag = document.getElementById("hidEditFlag").value.Trim();
    //新建时，编号选择手工输入时
    if ("INSERT" == editFlag)
    {
        //获取编码规则下拉列表选中项
        codeRule = document.getElementById("codeRule_ddlCodeRule").value.Trim();
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "")
        {
            //获取输入的编号
            codeNo = document.getElementById("codeRule_txtCode").value.Trim();
            //编号必须输入
            if (codeNo == "")
            {
                isErrorFlag = true;
                fieldText += "模板编号|";
   		        msgText += "请输入模板编号|";
            }
            else
            {
                if (!CodeCheck(codeNo))
                {
                    isErrorFlag = true;
                    fieldText += "模板编号|";
                    msgText += "模板编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]|";
                }
            }
        }
    }
        
    //主题必须输入
    if (document.getElementById("txtTitle").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "主题|";
        msgText += "请输入主题|";  
    }
    //备注
    remark = document.getElementById("txtRemark").value.Trim();
    if (remark != "" && remark != null && strlen(remark) > 500)
    {
        isErrorFlag = true;
        fieldText += "备注|";
        msgText += "备注最多只允许输入500个字符！|";  
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
* 校验要素信息
*/
function CheckElemInfo()
{
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;
	
	//获取表格
    table = document.getElementById("tblElemDetail");
    //获取表格行数
    var count = table.rows.length;
    //定义变量
    var elemCount = 0;
    var totalRate = 0;
    //遍历所有行，获取技能信息
	for (var i = 1 ; i < count ; i++)
	{
	    //不是删除行时，校验输入
	    if (table.rows[i].style.display != "none")
	    {
            //增长行数
            elemCount++;
	        //满分
            maxScore = document.getElementById("txtMaxScore_" + i).value.Trim();            
            //判断是否输入
            if(maxScore == "" || maxScore == null)
            {
                isErrorFlag = true;
                fieldText += "模板要素 行：" + elemCount + " 满分|";
                msgText += "请输入满分|";  
            }
            //判断是否为数字
            else if (!IsNumeric(maxScore, 3, 2) || parseFloat(maxScore) <= 0)
            {
                isErrorFlag = true;
                fieldText += "模板要素 行：" + elemCount + " 满分|";
                msgText += "请输入正确的满分|";  
            }
            
            //备注
            msRemark= document.getElementById("txtElemRemark_" + i).value.Trim();
              if(strlen(msRemark)> 100)
              {
   		                isErrorFlag = true;
                fieldText += "要素备注 行：" + elemCount + "|";
                msgText += "要素备注最多只允许输入100个字符|";  
              }
	        //权重
            rate = document.getElementById("txtRate_" + i).value.Trim();            
            //判断是否输入
            if(rate == "" || rate == null)
            {
                isErrorFlag = true;
                fieldText += "模板要素 行：" + elemCount + " 权重|";
                msgText += "请输入权重|";  
            }
            //判断是否为数字
            else if (!IsNumeric(rate, 3, 2) || parseFloat(rate) <= 0)
            {
                isErrorFlag = true;
                fieldText += "模板要素 行：" + elemCount + " 权重|";
                msgText += "请输入正确的权重|";  
            }
            else
            {
                //权重相加
                totalRate = totalRate + parseFloat(rate);
            }
            
	    }
	}
	//判断权重总和是否是100
	if (count > 1 && totalRate != 100)
	{
        isErrorFlag = true;
        fieldText += "模板要素|";
        msgText += "您输入权重总和不等于100|";  
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
* 保存模板信息
*/
function DoSave()
{
    /* 页面信息进行校验 */
    //基本信息校验 有错误时，返回
    if (CheckBaseInfo())
    {
        return;
    //要素信息校验 有错误时，返回
    } else if(CheckElemInfo())
    {
        return;
    }
    //获取基本信息参数
    postParams = GetBaseInfoParams();
    //获取要素信息参数
    postParams += GetElemParams();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/RectCheckTemplate_Edit.ashx",
        data : postParams,
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
                /* 设置编号的显示 */ 
                //显示考核的编号 考核编号DIV可见              
                document.getElementById("divCodeNo").style.display = "block";
                //设置考核编号
                document.getElementById("divCodeNo").innerHTML = data.data;
                //编码规则DIV不可见
                document.getElementById("divCodeRule").style.display = "none";
                //设置提示信息
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
            }
            //编号已存在
            else if(data.sta == 2)
            {
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","该编号已被使用，请输入未使用的编号！");
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
function GetBaseInfoParams()
{
    //获取编辑模式
    editFlag = document.getElementById("hidEditFlag").value;
    //新建时，编号选择手工输入时
    var param = "Action=SaveTemplate&EditFlag=" + editFlag;
    var no = "";
    //更新的时候
    if ("INSERT" != editFlag)
    {
        //编号
        no = document.getElementById("divCodeNo").innerHTML;
    }
    //插入的时候
    else
    {
        //获取编码规则ID
        codeRule = document.getElementById("codeRule_ddlCodeRule").value.Trim();
        //手工输入的时候
        if ("" == codeRule)
        {
            //招聘活动编号
            no = document.getElementById("codeRule_txtCode").value.Trim();
        }
        else
        {
            //编码规则ID
            param += "&CodeRuleID=" + codeRule;
        }
    }
    //编号
    param += "&TemplateNo=" + no;
    //主题
    param += "&Title=" + escape(document.getElementById("txtTitle").value.Trim());
    //岗位
    param += "&QuarterID=" + document.getElementById("ddlQuarter").value.Trim();
    //启用状态
    param += "&UsedStatus=" + document.getElementById("seleUsedStatus").value.Trim();
    //备注
    param += "&Remark=" + escape(document.getElementById("txtRemark").value.Trim());
    
    return param;
}

/*
* 要素信息参数
*/
function GetElemParams()
{   
    var param = "";
    //获取表格
    table = document.getElementById("tblElemDetail");
    //获取表格行数
    var count = table.rows.length;
    //定义变量
    var elemCount = 0;
    //遍历所有行，获取技能信息
	for (var i = 1 ; i < count ; i++)
	{
	    //不是删除行时，校验输入
	    if (table.rows[i].style.display != "none")
	    {
	        //增长行数
            elemCount++;
	        //要素ID
	        param += "&ElemID_" + elemCount + "=" +escape ( document.getElementById("hidElemID_" + i).value.Trim());
	        //满分
            param += "&MaxScore_" + elemCount + "=" + escape (document.getElementById("txtMaxScore_" + i).value.Trim());
	        //权重
            param += "&Rate_" + elemCount + "=" + escape (document.getElementById("txtRate_" + i).value.Trim()); 
            //备注
            param += "&ElemRemark_" + elemCount + "=" + escape ( document.getElementById("txtElemRemark_" + i).value.Trim()); 
	    }
	}
    //要素记录数
    param += "&ElemCount=" + elemCount;
    
    //返回参数信息
    return param;
}

/*
* 返回面试模板列表
*/
function DoBack()
{
    //获取查询条件
    var searchCondition = document.getElementById("hidSearchCondition").value;
    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
    window.location.href = "RectCheckTemplate_Info.aspx?ModuleID=" + ModuleID + searchCondition;
}