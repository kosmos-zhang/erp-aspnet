/* 页面初期显示 */
$(document).ready(function(){
      ShowDeptTree("");
});
 String.prototype.length2 = function() {
    var cArr = this.match(/[^\x00-\xff]/ig);
    return this.length + (cArr == null ? 0 : cArr.length);
}
  function textcontrol(taId,maxSize) 
  {   
                // 默认 最大字符限制数   
                var defaultMaxSize = 250;   
                var ta = document.getElementById(taId);   
                // 检验 textarea 是否存在   
               if(!ta) {   
                          return;   
                }   
                // 检验 最大字符限制数 是否合法   
                if(!maxSize) {   
                   maxSize = defaultMaxSize;   
               } else {   
                    maxSize = parseInt(maxSize);   
                    if(!maxSize || maxSize < 1) {   
                        maxSize = defaultMaxSize;   
                   }   
               }   
               　　 if (ta.value.length2() > maxSize) {   
                   ta.value=ta.value.substring(0,maxSize);   
                  return true ;
               }    
           } 
/*
* 获取组织机构树
*/
function ShowDeptTree(deptID)
{
    //判断组织机构ID输入时，判断样式
    if (deptID != "")
    {
        //获取样式
        divDeal = document.getElementById("divSuper_" + deptID);
        //获取样式表单
        css = divDeal.className;
        //样式表单为打开状态时，不进行查询数据，隐藏菜单
        if ("folder_open" == css)
        { 
            //设置表单样式为关闭状态
            divDeal.className = "folder_close";
            //隐藏子组织机构
            document.getElementById("divSub_" + deptID).style.display = "none";
            //返回不执行查询
            return;
        }
        //表单样式为关闭状态时
        else
        {
            //设置表单样式为打开状态
            divDeal.className = "folder_open";
        }
    }
    //执行查询数据
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/PerformanceElem.ashx?Action=InitTree&DeptID=" + deptID,
        dataType:'string',//
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
           // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
             popMsgObj.ShowMsg('请求发生错误  ！');
        }, 
        success:function(data) 
        {
            //隐藏提示框  
            hidePopup();
            //显示评分项目树
            //第一次初期化时
            if (deptID == null || "" == deptID)
            {
                //设置子评分项目信息
                document.getElementById("divDeptTree").innerHTML = data;
            }
            //点击节点时
            else
            {
                //设置子评分项目信息
                document.getElementById("divSub_" + deptID).innerHTML = data;
                //将子评分项目div设置成可见
                document.getElementById("divSub_" + deptID).style.display = "block";
            }
        } 
    });  
}
/**/   
function SetSelectValue(deptID, deptName, superDeptID)
{

    //上级机构ID
    document.getElementById("hidSelectValue").value = deptID + "|" + deptName + "|" + superDeptID;
    //显示选中项
    if (deptName == "") deptName = "全部指标";
    document.getElementById("divSelectName").innerHTML = deptName;
    DoModify(deptID);
    ShowDeptTree(deptID);
    
}
function SetSelectValue1(deptID, deptName, superDeptID)
{

    //上级机构ID
    document.getElementById("hidSelectValue").value = deptID + "|" + deptName + "|" + superDeptID;
    //显示选中项
    if (deptName == "") deptName = "全部指标";
    document.getElementById("divSelectName").innerHTML = deptName;
  //  DoModify(deptID);
   // ShowDeptTree(deptID);
}
var temCount=0;
function DoEditDept(flag)
{

temCount =flag ;
    //获取选择值
    selectValue = document.getElementById("hidSelectValue").value.Trim();
   //新建顶级节点
      if ("2" == flag)
   {
        //新建模式
        DoNew();
       return ;
     
   }
    //没有选择机构时
  if (selectValue == "")
   {
       // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
         popMsgObj.ShowMsg(' 请选择指标项目！');
       return;
   }
    //解析组织机构信息
    deptInfo = selectValue.split("|");
    var editDeptID = "";
    var editDeptName = "";
    
   /// deptID + "|" + deptName + "|" + superDeptID;
    //添加同级
    if ("0" == flag)
    {
        //上级机构ID
        superDeptID = deptInfo[2];
        //上级机构名称
        if (editDeptID != "")
            editDeptName = document.getElementById("divDeptName_" + superDeptID).innerHTML;
            document.getElementById("hidParentElemNo").value=superDeptID;
            
            DoNew();
            
    }
    //添加下级
    else if ("1" == flag)
    {
        //上级机构ID
        editDeptID = deptInfo[0];
        //上级机构名称
        editDeptName = deptInfo[1];
        document.getElementById("hidParentElemNo").value=editDeptID;
       // alert (editDeptID);
        DoNew();
    }
    
}



/*
* 编辑要素
*/
function DoNew()
{    AlertMsg();
    //编辑模式
    document.getElementById("hidEditFlag").value = "INSERT";
    //清除页面输入
    ClearElemInfo();
    //显示修改页面
    document.getElementById("divEditCheckItem").style.display = "block";
    //判断编号是否有除了“手工录入”的其他项目，如果有，进入新建模式，编号先选中其他的项目
     if ( Number ( document.getElementById("AimNum_ddlCodeRule").options.length)>Number ( '1')){
     document.getElementById("AimNum_ddlCodeRule").selectedIndex ="1";
          }
      else
        {document.getElementById("AimNum_ddlCodeRule").selectedIndex ="0";
         }
        var numberLength=Number ( document.getElementById("AimNum_ddlCodeRule").options.length);
       var   AimNum_ddlCodeRule_Select=document.getElementById("AimNum_ddlCodeRule").options[ document.getElementById("AimNum_ddlCodeRule").selectedIndex ].value;
    
     // alert ("选中的INDEX:"+numberLength)
       //alert ("选中的值为:"+AimNum_ddlCodeRule_Select);
     if (AimNum_ddlCodeRule_Select=="")
         {
           document.getElementById("AimNum_txtCode").disabled =false ;
           document.getElementById("AimNum_txtCode").value ="";
          }
          else
          {  document.getElementById("AimNum_txtCode").disabled =true  ;
             document.getElementById("AimNum_txtCode").value ="保存时自动生成";
          
          }
         document.getElementById("AimNum_ddlCodeRule").disabled =false;
          document.getElementById("AimNum_ddlCodeRule").style.display ='';
}


/*
* 修改要素信息
*/
function DoModify(elemID)
{
    //编辑模式
    AlertMsg();
    document.getElementById("hidEditFlag").value = "UPDATE";
    //设置ID
    document.getElementById("hidElemID").value = elemID;
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/PerformanceElem.ashx?Action=GetInfo&ElemID=" + elemID,
        dataType:'json',//
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
           // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
             popMsgObj.ShowMsg('请求发生错误 ！');
        }, 
        success:function(msg) 
        {
            //隐藏提示框  
            hidePopup();
            /* 设置考核类型信息 */
            $.each(msg.data, function(i,item){
                //要素名称
                document.getElementById("txtEditElemName").value = item.ElemName;
                //启用状态
                document.getElementById("sltEditUsedStatus").value = item.UsedStatus;
             document.getElementById("txtAsseFrom").value = item.AsseForm;
    //评分标准分
    document.getElementById("txtStandardScore").value = item.StandardScore;
    //评分最小值
    document.getElementById("txtMinScore").value = item.MinScore;
    //评分最大值
    document.getElementById("txtMaxScore").value =  item.MaxScore;
    //评分标准
    document.getElementById("txtAsseStandard").value = item.AsseStandard;
    //评分来源
   
    
    //备注
    document.getElementById("txtRemark").value = item.Remark;
    //评分细则
    document.getElementById("txtScoreRules").value =item.ScoreRules;
    document.getElementById("AimNum_ddlCodeRule").value="";
    document.getElementById("AimNum_txtCode").value =item.ElemNo;
     document.getElementById("AimNum_txtCode").disabled =true ;
      document.getElementById("AimNum_ddlCodeRule").style.display ="none"; 
                           
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
                    var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
            isErrorFlag = true;
            fieldText = fieldText + RetVal+"|";
   		    msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
          aim_codeRule = document.getElementById("AimNum_ddlCodeRule").value.Trim();
       // alert (aim_codeRule);
        //手工输入的时候
        if ("" == aim_codeRule)
        {
            //人员编号
           if (""== document.getElementById("AimNum_txtCode").value.Trim())
           {
            isErrorFlag = true;
        fieldText += "考核编号|";
        msgText += "请输入考核编号|";
           }else
           {
               if ( isnumberorLetters( document.getElementById("AimNum_txtCode").value.Trim()))
               {
                isErrorFlag = true;
                fieldText += "考核编号|";
                 msgText += "考核编号只能包含字母或数字！|";
               }
           }
           
           
           if( checkstr(document.getElementById("AimNum_txtCode").value.Trim(),50))
           {
           
            isErrorFlag = true;
            fieldText += "考核编号|";
             msgText += "考核编号长度过长！|";
           
           }
           
        }
        else
        {
            if (isnumberorLetters( document.getElementById("AimNum_ddlCodeRule").value.Trim()))
          {
            isErrorFlag = true;
            fieldText += "考核编号|";
             msgText += "考核编号只能包含字母或数字！|";
          }
        }
        
    //考核类型名称必须输入
    if (document.getElementById("txtEditElemName").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "考核指标名称|";
        msgText += "请输入考核指标名称|";  
    }
    else
    {
     if (  !CheckSpecialWord(document.getElementById("txtEditElemName").value.Trim() ))
      {
           isErrorFlag = true;
            fieldText += "考核指标名称|";
            msgText += "考核指标名称不能含有特殊字符|";
      }
    }
    
    
    //评分标准必须输入
    if (document.getElementById("txtStandardScore").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "评分标准|";
        msgText += "请输入评分标准分|";  
    }
    else
    {
      //判断是否输入数字
     if (!Isint(document.getElementById("txtStandardScore").value.Trim()))
    {
         isErrorFlag = true;
        fieldText += "评分标准分|";
        msgText += "评分标准分必须是整数|";
    }
    }
    //评分最小值必须输入
    if (document.getElementById("txtMinScore").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "评分最小值|";
        msgText += "请输入评分最小值|";  
    }
    else
    {
       if (!Isint(document.getElementById("txtMinScore").value.Trim()))
    {
         isErrorFlag = true;
        fieldText += "评分最小值|";
        msgText += "评分最小值必须是整数|";
    }
    }
    //评分最大值必须输入
    if (document.getElementById("txtMaxScore").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "评分最大值|";
        msgText += "请输入评分最大值|";  
    }else
    {
        if (!Isint(document.getElementById("txtMaxScore").value.Trim()))
    {
         isErrorFlag = true;
        fieldText += "评分最大值|";
        msgText += "评分最大值必须是整数|";
    }
    }
  
 
 
    //判断条件，必须最小值<标准值<最大值
    if((Number(document.getElementById("txtMinScore").value.Trim()))>(Number(document.getElementById("txtMaxScore").value.Trim())))
    {
       isErrorFlag = true;
        fieldText += "错误|";
        msgText += "评分最小值应该小于评分最大值|";
    }
     if((Number(document.getElementById("txtMinScore").value.Trim()))>(Number(document.getElementById("txtStandardScore").value.Trim())))
    {
       isErrorFlag = true;
        fieldText += "错误|";
        msgText += "评分最小值应该小于评分标准分|";
    }
    if((Number(document.getElementById("txtMaxScore").value.Trim()))<(Number(document.getElementById("txtStandardScore").value.Trim())))
    {
       isErrorFlag = true;
        fieldText += "错误|";
        msgText += "评分最大值应该大于评分标准分|";
    } 
     var txtAsseStandard = document.getElementById("txtAsseStandard").value.Trim();
    if(strlen(txtAsseStandard)> 1024)
    {
    
      isErrorFlag = true;
        fieldText += "评分标准项|";
        msgText += "评分标准最多只允许输入1024个字符|";
    }
      var txtAsseFrom = document.getElementById("txtAsseFrom").value.Trim();
    if(strlen(txtAsseFrom)> 1024)
    {
    
      isErrorFlag = true;
        fieldText += "评分来源项|";
        msgText += "评分来源最多只允许输入1024个字符|";
    }
         var txtScoreRules = document.getElementById("txtScoreRules").value.Trim();
    if(strlen(txtScoreRules)> 1024)
    {
    
      isErrorFlag = true;
        fieldText += "评分细则项|";
        msgText += "评分细则最多只允许输入1024个字符|";
    }
    
         var txtRemark = document.getElementById("txtRemark").value.Trim();
    if(strlen(txtRemark)> 1024)
    {
    
      isErrorFlag = true;
        fieldText += "备注项|";
        msgText += "备注最多只允许输入1024个字符|";
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
        url: "../../../Handler/Office/HumanManager/PerformanceElem.ashx?Action=EditInfo&" + postParams,
        dataType:'json',//返回json格式数据
        cache:false,
        beforeSend:function()
        {
            AddPop();
        }, 
        error: function()
        {
            //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
             popMsgObj.ShowMsg(' 请求发生错误！');
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
                
               // alert (data.data);
                //设置ID 
              
                  sMessage = data.data.split("|"); 
                 // alert (sMessage);
                    document.getElementById("hidElemID").value =sMessage[0];
                //设置提示信息
              //  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                //设置ID 
              document.getElementById("AimNum_ddlCodeRule").value="";
    document.getElementById("AimNum_txtCode").value =sMessage[3];
     document.getElementById("AimNum_txtCode").disabled =true ;
      document.getElementById("AimNum_ddlCodeRule").style.display ="none"; 
               
           var getMessage=document.getElementById("hidSelectValue").value;
               var sd=getMessage .split("|");
               
      if (getMessage =='')
      {
          ShowDeptTree("");
      }
      else
      {
          
          
             if (sd[0]!="null" || sd[0]!='')
             {
             if (temCount =="0")//添加同级
             {
                 ShowDeptTree(sd[2]);
                      ShowDeptTree(sd[2]);
             }
             else if (temCount =="1")//添加下级
             {
               ShowDeptTree(sd[0]);
                      ShowDeptTree(sd[0]);
             }
              else if (temCount =="2")//修改
             {
               ShowDeptTree(sd[2]);
                      ShowDeptTree(sd[2]);
             }
             }
             else
             {
              ShowDeptTree("");
             }
             }
             
             
                   
               // SetSelectValue('', '', '')
              //  ShowDeptTree("");
               CloseDiv();
                   popMsgObj.ShowMsg(' 保存成功！');
               
            }
            else if (data.sta == 2)
            {
            
          //  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
             popMsgObj.ShowMsg(' 很抱歉，指标仅添加两级目录！');
         }
         else if (data.sta == 3) {

             //  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
             popMsgObj.ShowMsg(' 该编号已被使用，请录入未使用的编号!');
         }
            //保存失败
            else 
            { 
                hidePopup();
                //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                 popMsgObj.ShowMsg(' 保存失败,请确认！');
            }
        } 
    });  
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
    //评分标准分
    document.getElementById("txtStandardScore").value = "";
    //评分最小值
    document.getElementById("txtMinScore").value = "";
    //评分最大值
    document.getElementById("txtMaxScore").value = "";
    //评分标准
    document.getElementById("txtAsseStandard").value = "";
    //评分来源
    document.getElementById("txtAsseFrom").value = "";
    //备注
    document.getElementById("txtRemark").value = "";
    //评分细则
    document.getElementById("txtScoreRules").value = "";
    
     
    
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
    param += "&ElemID=" + document.getElementById("hidElemID").value.Trim();
    //考核类型名称
    param += "&ElemName=" +escape (  document.getElementById("txtEditElemName").value.Trim());
    //启用状态
    param += "&UsedStatus=" + document.getElementById("sltEditUsedStatus").value.Trim();
    //评分标准分
   param += "&StandardScore=" +document.getElementById("txtStandardScore").value.Trim();
    //评分最小值
   param += "&MinScore=" +document.getElementById("txtMinScore").value.Trim();
    //评分最大值
    param +="&MaxScore=" +document.getElementById("txtMaxScore").value.Trim(); 
    //评分标准
    param +="&AsseStandard=" +escape ( document.getElementById("txtAsseStandard").value.Trim());
    //评分来源
   param += "&AsseFrom=" +escape ( document.getElementById("txtAsseFrom").value.Trim()); 
    //备注
   param += "&Remark=" +escape ( document.getElementById("txtRemark").value.Trim()); 
    //评分细则
   param += "&ScoreRules=" +escape ( document.getElementById("txtScoreRules").value.Trim()); 
   //父指标编号
   param += "&ParentElemNo="+document.getElementById("hidParentElemNo").value.Trim();
    //获取编码规则ID  
        aim_codeRule = document.getElementById("AimNum_ddlCodeRule").value.Trim();
       // alert (aim_codeRule);
        //手工输入的时候
        if ("" == aim_codeRule)
        {
            //人员编号
            param += "&ElemNo=" + document.getElementById("AimNum_txtCode").value.Trim();
        }
        else
        {
            //编码规则ID
            param += "&CodeRuleID=" + document.getElementById("AimNum_ddlCodeRule").value.Trim();
        }
    
    // alert ( document.getElementById("AimNum_txtCode").value);
     //  alert ( document.getElementById("AimNum_ddlCodeRule").value);
    //alert (document.getElementById("hidParentElemNo").value);
    
    
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
    CloseDiv();
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
    var action="SearchInfo";
    var postParam = "PageIndex=" + pageIndex + "&PageCount=" + pageCount + "&Action=" + action + "&OrderBy=" + orderBy + "&" + searchCondition;
    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  '../../../Handler/Office/HumanManager/PerformanceElem.ashx?' + postParam,//目标地址
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
                        + "<input id='chkSelect' name='chkSelect' value='" + item.ID + "'  type='checkbox'/>" + "</td>" //选择框
                        + "<td height='22' align='center'><a href='#' >" + item.ElemName + "</a></td>" //要素名称onclick='DoModify(\"" + item.ID + "\");'
                        + "<td height='22' align='center'>" + item.StandardScore+ "</td>"
                         + "<td height='22' align='center'>" + item.ScoreArrange + "</td>"
                          + "<td height='22' align='center'>" + item.AsseStandard+ "</td>"
                           + "<td height='22' align='center'>" + item.AsseFrom  + "</td>"
                            + "<td height='22' align='center'>" + item.Remark + "</td>"
                          + "<td height='22' align='center'>" + item.UsedStatusName+ "</td> ").appendTo($("#tblDetailInfo tbody")//启用状态
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
* 删除指标项目
*/
function DoDelete()
{  if(confirm("删除后不可恢复，确认删除吗！"))
{
    //获取选择值
    selectValue = document.getElementById("hidSelectValue").value;
    //没有选择指标项目时
    if (selectValue == "")
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择一项标准！");
        return;
    }
    //解析指标项目信息
    deptInfo = selectValue.split("|");
    //执行删除
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/PerformanceElem.ashx?Action=DeleteDeptInfo&DeptID=" + deptInfo[0],
        dataType:'json',
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
           // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
             popMsgObj.ShowMsg('请求发生错误！');
        }, 
        success:function(msg) 
        {
            //存在子指标项目时
            if(msg.sta == 2) 
            {
                //显示提示信息
             //   showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                 popMsgObj.ShowMsg(' 该指标存在子指标项目,请删除子指标后再删除！');
            }
            else if(msg.sta == 3) 
            {
              // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                popMsgObj.ShowMsg(' 该指标在模板中已经使用，无法完成删除，请确认！');
            }
            //删除成功
            else if(msg.sta == 1) 
            {
                //设置页面显示数据
             
                    ShowDeptTree(deptInfo[2]);
                  ShowDeptTree(deptInfo[2]);
                 document.getElementById("hidParentElemNo").value='';
               /// SetSelectValue('', '', '');
                //显示提示信息
               // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                 popMsgObj.ShowMsg(' 删除成功！');
            }
            //删除失败
            else
            {
                //显示提示信息
                //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
                 popMsgObj.ShowMsg(' 删除失败！');
            } 
        }
    }); 
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
	
	
	
	
	function DoEditDot()
	{
	//获取选择值
    selectValue = document.getElementById("hidSelectValue").value.Trim();
    //没有选择指标项目时
    if (selectValue == "")
    {
       // showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","！");
         popMsgObj.ShowMsg(' 请选择一项标准！');
        return;
    }
    //解析指标项目信息
   // alert (selectValue);
    deptInfo = selectValue.split("|");
    SetSelectValue(deptInfo[0], deptInfo[1], deptInfo[2])
  //  ShowDeptTree(deptInfo[1]);
	}

	//判断字符串是否超过指定的digit长度
	function checkstr(str,digit)
	{ 
	
	     //定义checkstr函数实现对用户名长度的限制
	        var n=0;         //定义变量n，初始值为0
	        for(i=0;i<str.length;i++){     //应用for循环语句，获取表单提交用户名字符串的长度
	        var leg=str.charCodeAt(i);     //获取字符的ASCII码值
	        if(leg>255)
	        {       //判断如果长度大于255 
	          n+=2;       //则表示是汉字为两个字节
	        }
	        else 
	        {
	         n+=1;       //否则表示是英文字符，为一个字节
	        }
	        }
	        
	      //  alert (n);
	        
	        if (n>digit)
	        {        //判断用户名的总长度如果超过指定长度，则返回true
	        return true;
	        }
	        else 
	        {return false;       //如果用户名的总长度不超过指定长度，则返回false
	        }  
    }
