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
                fieldText += "机构编号|";
   		        msgText += "请输入机构编号|";
            }
            else
            {
                if (!CodeCheck(codeNo))
                {
                    isErrorFlag = true;
                    fieldText += "机构编号|";
                    msgText += "机构编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]|";
                }
            }
        }
    }
        
    //机构名称必须输入
    if (document.getElementById("txtDeptName").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "机构名称|";
        msgText += "请输入机构名称|";  
    }
    //拼音代码
    if (document.getElementById("txtPYShort").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "拼音代码|";
        msgText += "请输入拼音代码|";  
    }
    //邮编
    post = document.getElementById("txtPost").value.Trim();
    if (post != "" && !IsNumber(post))
    {
        isErrorFlag = true;
        fieldText += "邮编|";
        msgText += "请输入正确的邮编|";  
    }
    //Email
    mail = document.getElementById("txtEmail").value.Trim();
    if (mail != "" && !IsEmail(mail))
    {
        isErrorFlag = true;
        fieldText += "邮编|";
        msgText += "请输入正确的邮编|";  
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
        url: "../../../Handler/Office/HumanManager/DeptInfo_Edit.ashx?" + postParams,
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
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","您输入的编号已经存在！");
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
        //人员编号
        param += "&DeptNo=" +escape ( document.getElementById("divCodeNo").innerHTML);
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
            param += "&DeptNo=" + document.getElementById("codeRule_txtCode").value.Trim();
        }
        else
        {
            //编码规则ID
            param += "&CodeRuleID=" + codeRule;
        }
    }
    //上级机构
    param += "&SuperDeptID=" + document.getElementById("txtSuperDeptID").value.Trim();
    //独立核算
    param += "&AccountFlag=" + document.getElementById("ddlAccountFlag").value.Trim();
    //机构名称
    param += "&DeptName=" + escape(document.getElementById("txtDeptName").value.Trim());
    //拼音代码
    param += "&PYShort=" + document.getElementById("txtPYShort").value.Trim();
    //机构职责
    param += "&Duty=" + escape(document.getElementById("txtDuty").value.Trim());
    //是否分公司
    param += "&SubFlag=" + document.getElementById("ddlSubFlag").value.Trim();
    //是否零售
    param += "&SaleFlag=" + document.getElementById("ddlSaleFlag").value.Trim();
    //描述信息
    param += "&Description=" + escape(document.getElementById("txtDescription").value.Trim());
    //地址
    param += "&Address=" + escape(document.getElementById("txtAddress").value.Trim());
    //邮编
    param += "&Post=" + escape(document.getElementById("txtPost").value.Trim());
    //联系人
    param += "&LinkMan=" + escape(document.getElementById("txtLinkMan").value.Trim());
    //电话
    param += "&Tel=" + escape(document.getElementById("txtTel").value.Trim());
    //传真
    param += "&Fax=" + escape(document.getElementById("txtFax").value.Trim());
    //Email
    param += "&Email=" + escape(document.getElementById("txtEmail").value.Trim());
    //启用状态
    param += "&UsedStatus=" + document.getElementById("ddlUsedStatus").value.Trim();
    
    return param;
}

/*
* 获取拼音缩写
*/
function GetPYShort()
{
    //获取机构名称
    deptName = document.getElementById("txtDeptName").value.Trim();
    //未输入时，返回不获取拼音缩写
    if (deptName == "")
    {
        return;
    }
    //输入时，获取拼音缩写
    else
    {
        postParams = "Text=" + deptName;
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
* 返回列表
*/
function DoBack()
{
    //获取查询条件
    var searchCondition = document.getElementById("hidSearchCondition").value;
    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
    window.location.href = "DeptInfo_Info.aspx?ModuleID=" + ModuleID + searchCondition;
}