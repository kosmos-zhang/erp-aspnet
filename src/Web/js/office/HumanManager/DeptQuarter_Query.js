/* 页面初期显示 */
$(document).ready(function(){
      ShowDeptTree("");
});

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
        url: "../../../Handler/Office/HumanManager/DeptQuarter_Query.ashx?Action=InitTree&DeptID=" + deptID,
        dataType:'string',//
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(data) 
        {
            //隐藏提示框  
         //   hidePopup();
            //显示组织机构树
            //第一次初期化时
             //  document.getElementById("divDeptQuarterTree").innerHTML = data;
            if (deptID == null || "" == deptID)
            {
                //设置子组织机构信息
                document.getElementById("divDeptQuarterTree").innerHTML = data;
            }
            //点击节点时
            else
            {
                //设置子组织机构信息
                document.getElementById("divSub_" + deptID).innerHTML = data;
                //将自组织机构div设置成可见
                document.getElementById("divSub_" + deptID).style.display = "block";
            }
        } 
    });  
}




///* 页面初期显示 */
//$(document).ready(function(){
//      InitPageInfo();
//});

function getColor(obj)
{
for (var i=0;i<200;i++)
{
    if (document .getElementById ("divDeptName_"+i))
    {
document .getElementById ("divDeptName_"+i).style.background  = "none";
    }
}
//document .getElementById ().stylecolor='Yellow'; 
document.getElementById(obj).style.background  = "Yellow";
  
}


/*
* 获取机构岗位树
*/
function InitPageInfo()
{
    //清空选择项信息
    document.getElementById("hidSelectControl").value = "";
    document.getElementById("hidSelectValue").value = "";
    //执行查询数据
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/DeptQuarter_Query.ashx?Action=InitPage",
        dataType:'string',//
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(data) 
        {
            //设置岗位信息
            document.getElementById("divDeptQuarterTree").innerHTML = data;
        } 
    });  
}

function GetInfoByDeptID(DeptID, deptName)
{
   //判断组织机构ID输入时，判断样式
//    if (quarterID != "")
//    {
//        //获取样式
//        divDeal = document.getElementById("divQSuper_" + quarterID);
//        //获取样式表单
//        css = divDeal.className;
//        //样式表单为打开状态时，不进行查询数据，隐藏菜单
//        if ("folder_open" == css)
//        {
//            //设置表单样式为关闭状态
//            divDeal.className = "folder_close";
//            //隐藏子组织机构
//            document.getElementById("divQSub_" + quarterID).style.display = "none";
//            //返回不执行查询
//            return;
//        }
//        //表单样式为关闭状态时
//        else
//        {
//            //设置表单样式为打开状态
//            divDeal.className = "folder_open";
//        }
//    }
    //执行查询数据
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/HumanManager/DeptQuarter_Query.ashx?Action=InitQuarterByDeptID&DeptID=" + DeptID,
        dataType: 'string', //
        cache: false,
        beforeSend: function() {
        },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        success: function(data) {
            //隐藏提示框
        //hidePopup();
            if (deptName != null)
                document.getElementById("selectedDeptName").innerHTML = deptName;
            document.getElementById("divDeptTree").innerHTML = data;
            //            //设置子组织机构信息
            //            document.getElementById("divQSub_" + quarterID).innerHTML = data;
            //            //将自组织机构div设置成可见
            //            document.getElementById("divQSub_" + quarterID).style.display = "block";

            document.getElementById("hidDeptInfo").value = DeptID;
        }
    });  
}
/*
* 获取机构岗位树
*/
function ShowDeptQuarterTree(quarterID)
{
    //判断组织机构ID输入时，判断样式
    if (quarterID != "")
    {
        //获取样式
        divDeal = document.getElementById("divQSuper_" + quarterID);
        //获取样式表单
        css = divDeal.className;
        //样式表单为打开状态时，不进行查询数据，隐藏菜单
        if ("folder_open" == css)
        {
            //设置表单样式为关闭状态
            divDeal.className = "folder_close";
            //隐藏子组织机构
            document.getElementById("divQSub_" + quarterID).style.display = "none";
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
        url: "../../../Handler/Office/HumanManager/DeptQuarter_Query.ashx?Action=InitQuarter&QuarterID=" + quarterID,
        dataType:'string',//
        cache:false,
        beforeSend:function()
        {
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(data) 
        {
            //隐藏提示框  
            hidePopup();
        //设置子组织机构信息
            document.getElementById("divQSub_" + quarterID).innerHTML = data;
            //将自组织机构div设置成可见
            document.getElementById("divQSub_" + quarterID).style.display = "block";
        } 
    });  
}
/*
* 设置选中的值
*/
function SetSelectValue(quarterID, quarterName, superQuarterID, deptID)
{
    //上级岗位信息
    document.getElementById("hidSelectValue").value = quarterID 
                                    + "|" + quarterName + "|" + superQuarterID + "|" + deptID;
    var selectControlIDValue = "";
    if (quarterID != null && quarterID != "")
    {
        //改变选中项背景色
    //    document.getElementById("divQuarterName_" + quarterID).style.background  = "gray";
        selectControlIDValue = "divQuarterName_" + quarterID;
    }
    else
    {
        //改变选中项背景色
    //    document.getElementById("divDQuarterName_" + deptID).style.background  = "gray";
        selectControlIDValue = "divDQuarterName_" + deptID;
    }
    //获取前一选择项
    selectControlID = document.getElementById("hidSelectControl").value;
    if (selectControlID != "")
    {
        //改变前选择项的背景色
        if (document.getElementById(selectControlID))
        {
             document.getElementById(selectControlID).style.background  = "";
        }
   
    }
    //设置新的选择项
    document.getElementById("hidSelectControl").value = selectControlIDValue;
    document .getElementById ("hidDeptInfo").value=deptID ;
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
    //获取编辑模式
    editFlag = document.getElementById("hidEditFlag").value;
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
                fieldText += "岗位编号|";
   		        msgText += "请输入岗位编号|";
            }
            else
            {
                if (!CodeCheck(codeNo))
                {
                    isErrorFlag = true;
                    fieldText += "岗位编号|";
                    msgText += "岗位编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]|";
                }
            }
        }
    }
        
    //岗位名称必须输入
    if (document.getElementById("txtQuarterName").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "岗位名称|";
        msgText += "请输入岗位名称|";  
    }
    //拼音代码
    if (document.getElementById("txtPYShort").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "拼音代码|";
        msgText += "请输入拼音代码|";  
    }
    else
    {
     if (!CodeCheck(document.getElementById("txtPYShort").value.Trim()))
                {
                    isErrorFlag = true;
                    fieldText += "拼音代码|";
                    msgText += "拼音代码只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]|";
                }
    }
    //岗位职责
    duty = document.getElementById("txtDuty").value.Trim();
    if (strlen(duty) > 1024)
    {
        isErrorFlag = true;
        fieldText += "岗位职责|";
        msgText += "岗位职责最多只允许输入1024个字符！|";  
    }
    //任职资格
    dutyRequire = document.getElementById("txtDutyRequire").value.Trim();
    if (strlen(dutyRequire) > 1024)
    {
        isErrorFlag = true;
        fieldText += "任职资格|";
        msgText += "任职资格最多只允许输入1024个字符！|";  
    }
    
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {
        //显示错误信息
        popMsgObj.Show(fieldText, msgText);
    }
     
    return isErrorFlag;
}

function CheckClient() {
  
 
        if (document.getElementById("lblErrorMes")) {
            document.getElementById("lblErrorMes").innerHTML = "";
        }

        var SDSD = document.getElementById("txtDisplayCode").value;
        document.getElementById("hfdNo").value = SDSD;
        
        var sig = CheckBaseInfo();
        
if (sig) {
            return false;
        }
        else {
            return true;
        }
     

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
        url: "../../../Handler/Office/HumanManager/DeptQuarter_Query.ashx?Action=EditQuarter&" + postParams,
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
        //    hidePopup();
            //保存成功
            if(data.sta == 1) 
            { 
                          showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
                //设置编辑模式
                document.getElementById("hidEditFlag").value = data.info;
                /* 设置编号的显示 */ 
                //显示考核的编号 考核编号DIV可见              
                document.getElementById("divCodeNo").style.display = "block";
                //设置考核编号
                document.getElementById("txtDisplayCode").value = data.data;
                document.getElementById("txtDisplayCode").disabled = true;
                //编码规则DIV不可见
                document.getElementById("divCodeRule").style.display = "none";
                if (document .getElementById ("hidDeptInfo").value.Trim()!=null |document .getElementById ("hidDeptInfo").value.Trim()!='')
                {
                GetInfoByDeptID(document .getElementById ("hidDeptInfo").value.Trim());
                }
                //设置提示信息
  
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
    var param = "EditFlag=" + editFlag;
    //更新的时候
    if ("INSERT" != editFlag)
    {
        //编号
        param += "&QuarterNo=" + document.getElementById("txtDisplayCode").value.Trim();
    }
    //插入的时候
    else
    {
        //获取编码规则ID
        codeRule = document.getElementById("codeRule_ddlCodeRule").value.Trim();
        //手工输入的时候
        if ("" == codeRule)
        {
            //编号
            param += "&QuarterNo=" + document.getElementById("codeRule_txtCode").value.Trim();
        }
        else
        {
            //编码规则ID
            param += "&CodeRuleID=" + codeRule;
        }
    }
    //所属机构
    param += "&DeptID=" + document.getElementById("txtDeptID").value.Trim();
    //上级岗位
    param += "&SuperQuarter=" + document.getElementById("hidSuperQuarter").value.Trim();
    //岗位名称
    param += "&QuarterName=" + escape(document.getElementById("txtQuarterName").value.Trim());
    //拼音代码
    param += "&PYShort=" + escape(document.getElementById("txtPYShort").value.Trim());
    //是否关键岗位
    param += "&KeyFlag=" + document.getElementById("ddlKeyFlag").value.Trim();
    //岗位分类
    param += "&TypeID=" + document.getElementById("ddlQuarterType_ddlCodeType").value.Trim();
    //岗位级别
    param += "&LevelID=" + document.getElementById("ddlQuarterLevel_ddlCodeType").value.Trim();
    //描述信息
    param += "&Description=" + escape(document.getElementById("txtDescription").value.Trim());
    //启用状态
    param += "&UsedStatus=" + document.getElementById("ddlUsedStatus").value.Trim();
    //附件
    param += "&Attachment=" + escape(document.getElementById("hfAttachment").value.Trim());
    param += "&PageAttachment=" + escape(document.getElementById("hfPageAttachment").value.Trim());
    //岗位职责
    param += "&Duty=" + escape(document.getElementById("txtDuty").value.Trim());
    //任职资格
    param += "&DutyRequire=" + escape(document.getElementById("txtDutyRequire").value.Trim());
    
    return param;
}

/*
* 获取拼音缩写
*/
function GetPYShort() {

    
    if (!CheckSpecialWord(document.getElementById("txtQuarterName").value.Trim())) {
        return;
    }
    //获取岗位名称
    quarterName = escape(document.getElementById("txtQuarterName").value.Trim());
    //未输入时，返回不获取拼音缩写
    if (quarterName == "")
    {
        return;
    }
    //输入时，获取拼音缩写
    else
    {
        postParams = "Text=" + quarterName;
        $.ajax({ 
            type: "POST",
            url: "../../../Handler/Common/PYShort.ashx?" + postParams,
            dataType:'json',//返回json格式数据
            cache:false,
            error: function()
            {
            }, 
            success:function(data) 
            {
                //获取成功时，设置拼音缩写
                if(data.sta == 1)
                {
                    //设置拼音缩写
                    document.getElementById("txtPYShort").value = data.info;
                }
            } 
        });
    }
}

/*
* 返回处理
*/
function DoBack()
{
    //隐藏页面
  //  CloseReclickDeptDiv();
    CloseDiv();
    document.getElementById("txtDisplayCode").value = "";
    document.getElementById("chMMubiao").checked = false;
    document.getElementById("chMRi").checked = false;
    document.getElementById("chMZhou").checked = false;
    document.getElementById("chMYue").checked = false;
    document.getElementById("chMJi").checked = false;
    document.getElementById("chMNian").checked = false;


    document.getElementById("chRRenWu").checked = false;
    document.getElementById("chRGEren").checked = false;
    document.getElementById("chRZhipai").checked = false;


    document.getElementById("chGgongzuo").checked = false;
    document.getElementById("chCricheng").checked = false;

    try {
        var oEditor = FCKeditorAPI.GetInstance('FCKeditor1');
        oEditor.SetHTML("", false);
    } catch (e) { }

    document.getElementById("QuterModelSelect").value = "";



  
    
    
    
    
    
    
    document.getElementById("divEditDeptQuarter").style.display = "none";
    if (document.getElementById("lblErrorMes")) {
        document.getElementById("lblErrorMes").innerHTML = "";
    }
    //设置页面显示数据
  //ShowDeptTree("");
//    InitPageInfo();
}

/*
* 编辑机构岗位
*/
function DoEditDeptQuarter(flag)
{ 
    
    //获取选择值
    selectValue = document.getElementById("hidSelectValue").value;
    //没有选择机构时
    if (selectValue == "")
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择岗位！");
        return false ;
    }
    //解析组织机构信息
    quarterInfo = selectValue.split("|");
    var editQuarterID = "";
    var editQuarterName = "";
    var deptID = "";
    //添加同级
    if ("0" == flag)
    {
        //上级岗位ID
        superQuarterID = quarterInfo[2];
        editQuarterID = superQuarterID;
        //上级岗位名称
        if (editQuarterID != "")
//           deptName=deptName.substring( deptName .lastIndexOf(">")+1,deptName .length);
//         document.getElementById("txtDeptName").value = deptName;

           if (document.getElementById("divQuarterName_" + superQuarterID))
           {
            editQuarterName = document.getElementById("divQuarterName_" + superQuarterID).innerHTML;
            }
        //所属机构
        deptID = quarterInfo[3];
    }
    //添加下级
    else if ("1" == flag)
    {
        //上级岗位ID
        editQuarterID = quarterInfo[0];
        //上级岗位名称
        editQuarterName = quarterInfo[1];
        //所属机构
        deptID = quarterInfo[3];
    }
    //修改
    if ("2" == flag) {
 
        //编辑模式
//        document.getElementById("hidEditFlag").value = "UPDATE";
        /* 设置编号的显示 */ 
        //显示考核的编号 考核编号DIV可见              
        document.getElementById("divCodeNo").style.display = "block";
        //编码规则DIV不可见
        document.getElementById("divCodeRule").style.display = "none";

        document.getElementById("hidquarterID").value = quarterInfo[0];
        //设置组织机构信息
        //InitDeptQuarterInfo(quarterInfo[0]);
     
    }
    //添加时
    else 
    {
        //清除添加页面的信息
        ClearEditInfo();
        //所属机构ID
        document.getElementById("txtDeptID").value = deptID;
        //所属机构名称
        if (document.getElementById("divDeptName_" + deptID))
        { }
        else {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请选择部门");
            return;
        }
        deptName = document.getElementById("divDeptName_" + deptID).innerHTML;
        
        
                deptName=deptName.substring( deptName .lastIndexOf(">")+1,deptName .length);
        document.getElementById("txtDeptName").value = deptName.replace(/&nbsp;/g, "");
        //上级岗位名称
        document.getElementById("txtSuperQuarterName").value = editQuarterName;
        //上级岗位ID
        document.getElementById("hidSuperQuarter").value = editQuarterID;
        //编辑模式
        document.getElementById("hidEditFlag").value = "INSERT";
        /* 设置编号的显示 */ 
        //显示编号 编号DIV可见              
        document.getElementById("divCodeNo").style.display = "none";
        //编码规则DIV不可见
        document.getElementById("divCodeRule").style.display = "block";
    }
  //  ShowReclickDeptDiv();
    // ShowPreventReclickDiv();
    // openRotoscopingDiv(true, 'divRotoscoping', 'divIframe');
    AlertProductMsg();
    //显示修改页面
    document.getElementById("divEditDeptQuarter").style.display = "block";
    return true;
}

function DoNew()
{

   //清除添加页面的信息     
         ClearEditInfo();
         AlertProductMsg();
  
   if (document .getElementById ("hidDeptInfo").value.Trim()=="") {
       showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请选择部门");
       CloseDiv();
                   return;}
                   var deptName="";
     if (document .getElementById ("hidDeptInfo").value.Trim()!=null ||document .getElementById ("hidDeptInfo").value.Trim()!='')
                {
                    var i=document .getElementById ("hidDeptInfo").value.Trim();
                     if (document .getElementById ("divDeptName_"+i))
                       { var temp=document .getElementById ("divDeptName_"+i).innerHTML;
                       
                      while(temp.indexOf("&nbsp;")   >=0)   
                          {   
                              temp= temp .replace("&nbsp;","")
                          }
                               
                        deptName=temp;
                       }
                }
    document .getElementById ("txtSuperQuarterName").value="";   
        document .getElementById ("hidSuperQuarter").value="";  
        document.getElementById("divEditDeptQuarter").style.display = "block";
       document.getElementById("hidEditFlag").value = "INSERT";
        document.getElementById("txtDeptID").value =document .getElementById ("hidDeptInfo").value.Trim();
  
        deptName=deptName.substring( deptName .lastIndexOf(">")+1,deptName .length);
         document.getElementById("txtDeptName").value = deptName;
    document.getElementById("divCodeNo").style.display = "none";
        //编码规则DIV不可见
        document.getElementById("divCodeRule").style.display = "block";
       
}

/*
* 清除页面输入内容
*/
function ClearEditInfo() {


    document.getElementById("txtDisplayCode").value = "";
    //岗位名称
    document.getElementById("txtQuarterName").value = "";
    //机构编号
    document.getElementById("codeRule_txtCode").value = "";
    //拼音代码
    document.getElementById("txtPYShort").value = "";
    //是否关键岗位
    document.getElementById("ddlKeyFlag").value = "";
    //岗位分类
    document.getElementById("ddlQuarterType_ddlCodeType").value = "";
    //岗位级别
    document.getElementById("ddlQuarterLevel_ddlCodeType").value = "";
    //描述信息
    document.getElementById("txtDescription").value = "";
    //启用状态
    document.getElementById("ddlUsedStatus").value = "1";
    //附件
    document.getElementById("hfAttachment").value = "";
    document.getElementById("hfPageAttachment").value = "";
    //下载删除不显示
    document.getElementById("divDealAttachment").style.display = "none";
    //上传显示 
    document.getElementById("divUploadAttachment").style.display = "block";
    //岗位职责
    document.getElementById("txtDuty").value = "";
    //任职资格
    document.getElementById("txtDutyRequire").value = "";
}

/*
* 设置组织机构信息
*/
function InitDeptQuarterInfo(quarterID)
{
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/DeptQuarter_Query.ashx?Action=GetQuarterInfo&QuarterID=" + quarterID,
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
            //岗位编号


            document.getElementById("txtDeptID").value = item.DeptID;
                document.getElementById("txtDisplayCode").value = item.QuarterNo;
                document.getElementById("txtDisplayCode").disabled = true;
                //所属机构
                document.getElementById("txtDeptName").value = item.DeptName;
                //上级岗位
                document.getElementById("txtSuperQuarterName").value = item.SuperQuarterName;
                //岗位名称
                document.getElementById("txtQuarterName").value = item.QuarterName;
                //拼音代码
                document.getElementById("txtPYShort").value = item.PYShort;
                //是否关键岗位
                document.getElementById("ddlKeyFlag").value = item.KeyFlag;
                //岗位分类
                document.getElementById("ddlQuarterType_ddlCodeType").value = item.TypeID;
                //岗位级别
                document.getElementById("ddlQuarterLevel_ddlCodeType").value = item.LevelID;
                //描述信息
                document.getElementById("txtDescription").value = item.Description;
                //启用状态
                document.getElementById("ddlUsedStatus").value = item.UsedStatus;
                //附件
                var attachment = item.Attachment;
                //附件存在时
                if (attachment == null || attachment == "")
                {
                    //下载删除不显示
                    document.getElementById("divDealAttachment").style.display = "none";
                    //上传显示 
                    document.getElementById("divUploadAttachment").style.display = "block";
                }
                else
                { 
                    //下载删除显示
                    document.getElementById("divDealAttachment").style.display = "block";
                    //上传不显示 
                    document.getElementById("divUploadAttachment").style.display = "none";
                }
                //附件
                document.getElementById("hfAttachment").value = attachment;
                document.getElementById("hfPageAttachment").value = attachment;
                //岗位职责
                document.getElementById("txtDuty").value = item.Duty;
                //任职资格
                document.getElementById("txtDutyRequire").value = item.DutyRequire;
                
            });
        }
    }); 
}

/*
* 删除组织机构
*/
function DoDelete()
{
    //获取选择值
    selectValue = document.getElementById("hidSelectValue").value;
    //没有选择机构时
    if (selectValue == "")
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择岗位");
        return;
    }
    
    if(!confirm("确认删除吗！"))
    {
        return;
    }
    //解析岗位信息
    quarterInfo = selectValue.split("|");
    //执行删除
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/DeptQuarter_Query.ashx?Action=DeleteQuarter&QuarterID=" + quarterInfo[0],
        dataType:'json',
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
            //存在子组织机构时
            if(msg.sta == 2) 
            {
                //显示提示信息
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","该岗位存在子岗位,请删除子岗位后再删除！");
            }
            //删除成功
            else if(msg.sta == 1) 
            {
                //设置页面显示数据
              //  InitPageInfo();
                //显示提示信息
                      if (document .getElementById ("hidDeptInfo").value.Trim()!=null |document .getElementById ("hidDeptInfo").value.Trim()!='')
                {
                GetInfoByDeptID(document .getElementById ("hidDeptInfo").value.Trim());
                }
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除成功！");
            }
            //删除失败
            else
            {
                //显示提示信息
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除失败！");
            } 
        }
    }); 
}

/*
* 附件处理
*/
function DealAttachment(flag)
{
    //flag未设置时，返回不处理
    if (flag == "undefined" || flag == "")
    {
        return;
    }
    //上传附件
    else if ("upload" == flag)
    {
        ShowUploadFile();
    }
    //清除附件
    else if ("clear" == flag)
    {
            //设置附件路径
            document.getElementById("hfPageAttachment").value = "";
            //下载删除不显示
            document.getElementById("divDealAttachment").style.display = "none";
            //上传显示 
            document.getElementById("divUploadAttachment").style.display = "block";
    }
    //下载附件
    else if ("download" == flag)
    {
            //获取附件路径
            attachUrl = document.getElementById("hfPageAttachment").value;
            //下载文件
            window.open("../../Common/DownloadFile.aspx?RelativeFilePath=" + attachUrl, "_blank");
    
    }
}

/*
* 上传文件后回调处理
*/
function AfterUploadFile(url, docName)
{
    if (url != "")
    {
        //设置附件路径
        document.getElementById("hfPageAttachment").value = url;
           document.getElementById("spanAttachmentName").innerHTML = docName;
        //下载删除显示
        document.getElementById("divDealAttachment").style.display = "block";
        //上传不显示
        document.getElementById("divUploadAttachment").style.display = "none";
    }
}

function ShowReclickDeptDiv(){
    debugger;
   /**第一步：创建DIV遮罩层。*/
	var sWidth,sHeight;
	sWidth = window.screen.availWidth-300;
	if(window.screen.availHeight > document.body.scrollHeight){  //当高度少于一屏
		sHeight = window.screen.availHeight-200;  
	}else{//当高度大于一屏
		sHeight = document.body.scrollHeight-200;   
	}
	
	 obj = document.getElementById("divPreventReclickDept");
    //隐藏信息提示DIV
	 if (obj != null && typeof (obj) != "undefined") {
	     obj.style.display = "block";
	 }
	 else {
	     // 整个div的大小和位子
	     var preventReclick = "<div id='divPreventReclickDept' style='position:absolute;z-index:2000; background-color:#777;"
                        + "left:0;top:0;filter:Alpha(opacity=0);width:" + sWidth + ";height:" + sHeight + ";' >";
	     preventReclick += "<iframe style='position: absolute; z-index:-1; width:" + sWidth + "; height:" + sHeight + ";' frameborder='0'>  </iframe>";
	     preventReclick += "</div>";

	     insertHtml("afterBegin", document.body, preventReclick);
	 }
}

function CloseReclickDeptDiv(){
	 obj = document.getElementById("divPreventReclickDept");
    //隐藏信息提示DIV
    if ( obj != null && typeof(obj) != "undefined") obj.style.display = "none";
}

function SaveAfter() {
    
    document.getElementById("divCodeNo").style.display = "block";


    document.getElementById("divCodeRule").style.display = "none";
    var sdsd = document.getElementById("hidDeptInfo").value.Trim();
    if (document.getElementById("hidDeptInfo").value.Trim() != null | document.getElementById("hidDeptInfo").value.Trim() != '') {
   
        GetInfoByDeptID(document.getElementById("hidDeptInfo").value.Trim());
    }
    if (document.getElementById("lblErrorMes")) {
        document.getElementById("lblErrorMes").innerHTML = "";
    } 
    if (document.getElementById("lblErrorMes")) {
        document.getElementById("lblErrorMes").innerHTML = "";
    }
   
}

function readAfter() {
//    debugger;
    var attachment = document.getElementById("hidaddd").value;
    //附件存在时
    if (attachment == null || attachment == "") {

        //下载删除不显示
        document.getElementById("divDealAttachment").style.display = "none";
        //上传显示 
        document.getElementById("divUploadAttachment").style.display = "block";
    }
    else {
        //下载删除显示
        document.getElementById("divDealAttachment").style.display = "block";
        //上传不显示 
        document.getElementById("divUploadAttachment").style.display = "none";
    }
    document.getElementById("divEditDeptQuarter").style.display = "block";
    if (document.getElementById("lblErrorMes")) {
        document.getElementById("lblErrorMes").innerHTML = "";
    }
    if (document.getElementById("txtDeptID").value.Trim() != null | document.getElementById("txtDeptID").value.Trim() != '') {

        GetInfoByDeptID(document.getElementById("txtDeptID").value.Trim());
    }
    document.getElementById("divCodeNo").style.display = "block";
    document.getElementById("divCodeRule").style.display = "none";
    AlertProductMsg();
 
 
}



function readssAfter( ) {
   
    document.getElementById("divEditDeptQuarter").style.display = "block";
    if (document.getElementById("lblErrorMes")) {
        document.getElementById("lblErrorMes").innerHTML = "";
    }
    if (document.getElementById("txtDeptID").value.Trim() != null | document.getElementById("txtDeptID").value.Trim() != '') {

        GetInfoByDeptID(document.getElementById("txtDeptID").value.Trim());
    }
    AlertProductMsg();
    if (document.getElementById("txtDisplayCode").value == "") {

    } else {
    document.getElementById("divCodeNo").style.display = "block";
    document.getElementById("divCodeRule").style.display = "none";
}

//if (sign != "") {


//    document.getElementById("txtSuperQuarterName").value = sign.split('_')[0];
//    document.getElementById("hidSuperQuarter").value = sign.split('_')[1];

//}
  
}


function CloseDiv() {
    var ProductBigDiv = document.getElementById("ProductBigDiv");
    if (ProductBigDiv) {
        document.body.removeChild(ProductBigDiv);
    }
   
  
}

function AlertProductMsg() {
    
    /**第一步：创建DIV遮罩层。*/
    var sWidth, sHeight;
    sWidth = window.screen.availWidth;
    //屏幕可用工作区高度： window.screen.availHeight;
    //屏幕可用工作区宽度： window.screen.availWidth;
    //网页正文全文宽：     document.body.scrollWidth;
    //网页正文全文高：     document.body.scrollHeight;
    if (window.screen.availHeight > document.body.scrollHeight) {  //当高度少于一屏
        sHeight = window.screen.availHeight;
    } else {//当高度大于一屏
        sHeight = document.body.scrollHeight;
    }
    //创建遮罩背景
    var maskObj = document.createElement("div");
    maskObj.setAttribute('id', 'ProductBigDiv');
    maskObj.style.position = "absolute";
    maskObj.style.top = "0";
    maskObj.style.left = "0";
    maskObj.style.background = "#777";
    maskObj.style.filter = "Alpha(opacity=10);";
    maskObj.style.opacity = "0.3";
    maskObj.style.width = sWidth + "px";
    maskObj.style.height = sHeight + "px";
    maskObj.style.zIndex = "2";
    document.body.appendChild(maskObj);
}

 