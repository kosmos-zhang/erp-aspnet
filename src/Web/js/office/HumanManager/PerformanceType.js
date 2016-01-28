$(document).ready(function(){
//     DoSearchInfo();
});


/*
* 编辑要素
*/
function DoNew()
{
 AlertMsg();
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
    //类型名称
    document.getElementById("txtEditElemName").value = "";
    //启用状态
    document.getElementById("sltEditUsedStatus").value = "1";
}

/*
* 修改要素信息
*/
function DoModify(elemID)
{AlertMsg();
    //编辑模式
    document.getElementById("hidEditFlag").value = "UPDATE";
    //设置ID
    document.getElementById("hidElemID").value = elemID;
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/PerformanceType.ashx?Action=GetInfo&ElemID=" + elemID,
        dataType:'json',//
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","");
             popMsgObj.ShowMsg('请求发生错误！');
        }, 
        success:function(msg) 
        {
            //隐藏提示框  
            hidePopup();
            /* 设置考核类型信息 */
            $.each(msg.data, function(i,item){
                //要素名称
                document.getElementById("txtEditElemName").value = item.TypeName;
                //启用状态
                document.getElementById("sltEditUsedStatus").value = item.UsedStatus;
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
        
    //考核类型名称必须输入
    if (document.getElementById("txtEditElemName").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "考核类型名称|";
        msgText += "请输入考核类型名称|";  
    }
    else
    {
      var txtRemark = document.getElementById("txtEditElemName").value.Trim();
    if(strlen(txtRemark)> 100)
    {
    
      isErrorFlag = true;
        fieldText += "考核类型名称|";
        msgText += "考核类型名称最多只允许输入100个字符|";
    }
      if (  !CheckSpecialWord(txtRemark))
      {
           isErrorFlag = true;
            fieldText += "考核类型名称|";
            msgText += "考核类型名称不能含有特殊字符|";
      }
    
    }
    
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {
        //显示错误信息  fieldText += "考核类型名称|"; msgText += "请输入考核类型名称|"; 
        
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
        url: "../../../Handler/Office/HumanManager/PerformanceType.ashx?Action=EditInfo&" + postParams,
        dataType:'json',//返回json格式数据
        cache:false,
        beforeSend:function()
        {
            AddPop();
        }, 
        error: function()
        {
           // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","");
             popMsgObj.ShowMsg('请求发生错误！');
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
                  //  popMsgObj.ShowMsg("保存成功");
                     popMsgObj.ShowMsg('保存成功！');
             //   popMsgObj.Show("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
                 //显示错误信息  fieldText += "考核类型名称|"; msgText += "请输入考核类型名称|"; 
        
       // popMsgObj(fieldText, msgText);
              //  if ( document.getElementById("layout"))
              //  {
              //  alert (document.getElementById("layout").innerHTML);
               // }
            }
            //保存失败
            else 
            { 
                hidePopup();
              //  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
                popMsgObj.ShowMsg('保存失败,请确认！');
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
    //类型ID
    param += "&ElemID=" + document.getElementById("hidElemID").value;
    //考核类型名称
    param += "&ElemName=" +escape (  document.getElementById("txtEditElemName").value);
    //启用状态
    param += "&UsedStatus=" + document.getElementById("sltEditUsedStatus").value;
   
    
    return param;
}

/*
* 返回操作
*/
function DoBack()
{CloseDiv();
    //清除页面输入
    ClearElemInfo();
    //隐藏修改页面
    document.getElementById("divEditCheckItem").style.display = "none"; 
    DoSearchInfo();
}

/*
* 执行查询
*/
function DoSearchInfo(currPage)
{ 
    var search = "";
    //要素名称
    search += "ElemName=" + escape ( document.getElementById("txtSearchElemName").value.Trim());
    //启用状态
    search += "&UsedStatus=" + document.getElementById("sltSearchUsedStatus").value.Trim();
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
        popMsgObj.Show(fieldText,msgText);
      return;
    }
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
        this.pageCount = parseInt(newPageCount);
        //显示页面数据
        TurnToPage(parseInt(newPageIndex));
    }
}

/* 分页相关变量定义 */  

var pageCount = 10;//每页显示记录数
var totalRecord = 0;//总记录数
var pagerStyle = "flickr";//jPagerBar样式
var currentPageIndex = 1;//当前页
var orderBy = "ModifiedDate_d";//排序字段

function getChage( obj)
{

if (obj.checked==false )
{
 document .getElementById ("chkCheckAll").checked=false ;
}
}

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
    //设置动作种类
    var action="SearchInfo";
    var postParam = "PageIndex=" + pageIndex + "&PageCount=" + pageCount + "&Action=" + action + "&OrderBy=" + orderBy + "&" + searchCondition;
    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  '../../../Handler/Office/HumanManager/PerformanceType.ashx?' + postParam,//目标地址
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
                        + "<input id='chkSelect' name='chkSelect' value='" + item.ID + "'  type='checkbox' onpropertychange='getChage(this)'  />" + "</td>" //选择框
                        + "<td height='22' align='center'><a href='#' onclick='DoModify(\"" + item.ID + "\");'>" + item.TypeName + "</a></td>" //要素名称
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
* 删除面试要素信息
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
            url: "../../../Handler/Office/HumanManager/PerformanceType.ashx?" + postParam,
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
                      popMsgObj.ShowMsg('删除成功！');
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

	function AlertMsg(){

	   /**第一步：创建DIV遮罩层。*/
		var sWidth,sHeight;
		sWidth = window.screen.availWidth;
		//屏幕可用工作区高度： window.screen.availHeight;
		//屏幕可用工作区宽度： window.screen.availWidth;
		//网页正文全文宽：     document.body.scrollWidth;
		//网页正文全文高：     document.body.scrollHeight;
		if(window.screen.availHeight > document.body.scrollHeight){  //当高度少于一屏
			sHeight = window.screen.availHeight;  
		}else{//当高度大于一屏
			sHeight = document.body.scrollHeight;   
		}
		//创建遮罩背景
		var maskObj = document.createElement("div");
		maskObj.setAttribute('id','BigDiv');
		maskObj.style.position = "absolute";
		maskObj.style.top = "0";
		maskObj.style.left = "0";
		maskObj.style.background = "#777";
		maskObj.style.filter = "Alpha(opacity=30);";
		maskObj.style.opacity = "0.3";
		maskObj.style.width = sWidth + "px";
		maskObj.style.height = sHeight + "px";
		maskObj.style.zIndex = "200";
		document.body.appendChild(maskObj);
		
	}

		function CloseDiv(){
		var Bigdiv = document.getElementById("BigDiv");
		//var Mydiv = document.getElementById("div_Add");
		if (Bigdiv)
		{
		document.body.removeChild(Bigdiv);
		} 
//         Bigdiv.style.display = "none";
		///Mydiv.style.display="none";
	}