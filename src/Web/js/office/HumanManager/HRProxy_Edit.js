/*
* 保存人才代理信息
*/
function SaveHRProxyInfo()
{
    /* 页面信息进行校验 */
    //基本信息校验 有错误时，返回
    if (CheckInput())
    {
        return;
    }
    //获取人员基本信息参数
    postParams = GetPostParams();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/HRProxy_Edit.ashx",
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
            if(data.sta == 1) 
            { 
                //设置编辑模式
                document.getElementById("hidEditFlag").value = data.info;
                /* 设置编号的显示 */ 
                //显示人才代理的编号 人才代理编号DIV可见              
                document.getElementById("divProxyCompanyNo").style.display = "block";
                //设置人才代理编号
                document.getElementById("divProxyCompanyNo").innerHTML = data.data;
                //编码规则DIV不可见
                document.getElementById("divCodeRule").style.display = "none";
                //设置提示信息
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
            }
            else if(data.sta == 2)
            {
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","您输入的编号已经存在！");
            }
            else 
            { 
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
            } 
        } 
    });    
}

/*
* 输入信息校验
*/
function CheckInput()
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
        codeRule = document.getElementById("codruleProxy_ddlCodeRule").value;
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "")
        {
            //获取输入的编号
            proxyNo = document.getElementById("codruleProxy_txtCode").value;
            //编号必须输入
            if (proxyNo == "")
            {
                isErrorFlag = true;
                fieldText += "企业编号|";
   		        msgText += "请输入企业编号|";
            }
            else
            {
                if (!CodeCheck(proxyNo))
                {
                    isErrorFlag = true;
                    fieldText += "企业编号|";
                    msgText += "企业编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]|";
                }
            }
        }
    }
    //企业名称
    if (document.getElementById("txtProxyCompanyName").value == "")
    {
        isErrorFlag = true;
        fieldText += "企业名称|";
        msgText += "请输入企业名称|";
    } 
    //地址
    if (document.getElementById("txtAddress").value == "")
    {
        isErrorFlag = true;
        fieldText += "地址|";
        msgText += "请输入地址|";
    }
    //企业法人
//    if (document.getElementById("txtCorporate").value == "")
//    {
//        isErrorFlag = true;
//        fieldText += "企业法人|";
//        msgText += "请输入企业法人|";
//    }
    //邮箱
    mail = document.getElementById("txtMail").value;
    if (mail != "" && !IsEmail(mail))
    {
        isErrorFlag = true;
        fieldText += "邮箱|";
        msgText += "请输入正确的邮箱|";
    }
    //网址
//    website = document.getElementById("txtWebsite").value;
//    if (website != "" && !IsUrl(website))
//    {
//        isErrorFlag = true;
//        fieldText += "网址|";
//        msgText += "请输入正确的网址|";
//    }
    //联系人姓名
    if (document.getElementById("txtContactName").value == "")
    {
        isErrorFlag = true;
        fieldText += "姓名|";
        msgText += "请输入姓名|";
    }
    
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {
        //显示错误信息
        popMsgObj.Show(fieldText,msgText);
    }

    return isErrorFlag;
}

/*
* 获取提交的基本信息
*/
function GetPostParams()
{    
    editFlag = document.getElementById("hidEditFlag").value;
    var strParams = "EditFlag=" + editFlag;//编辑标识
    var no = "";
    //更新的时候
    if ("UPDATE" == editFlag)
    {
        //人员编号
        no = document.getElementById("divProxyCompanyNo").innerHTML;
    }
    //插入的时候
    else
    {
        //获取编码规则ID
        codeRule = document.getElementById("codruleProxy_ddlCodeRule").value;
        //手工输入的时候
        if ("" == codeRule)
        {
            //人员编号
            no = document.getElementById("codruleProxy_txtCode").value;
        }
        else
        {
            //编码规则ID
            strParams += "&CodeRuleID=" + document.getElementById("codruleProxy_ddlCodeRule").value;
        }
    }
    //编号
    strParams += "&ProxyCompanyCD=" + no;
    
    /* 获取代理公司信息 */
    //企业名称
    strParams += "&ProxyCompanyName=" + reescape(document.getElementById("txtProxyCompanyName").value);
    //企业性质
    strParams += "&Nature=" + reescape(document.getElementById("txtNature").value);
    //地址
    strParams += "&Address=" + reescape(document.getElementById("txtAddress").value);
    //企业法人
    strParams += "&Corporate=" + reescape(document.getElementById("txtCorporate").value);
    //电话
    strParams += "&CompanyTel=" + reescape(document.getElementById("txtTel").value);
    //传真
    strParams += "&CompanyFax=" + reescape(document.getElementById("txtFax").value);
    //邮箱
    strParams += "&CompanyMail=" + reescape(document.getElementById("txtMail").value);
    //网址
    strParams += "&Website=" + reescape(document.getElementById("txtWebsite").value);
    //合作关系
    strParams += "&Cooperation=" + reescape(document.getElementById("ddlCooperation").value);
    //重要程度
    strParams += "&Important=" + reescape(document.getElementById("ddlImportant").value);
    //收费标准
    strParams += "&Standard=" + reescape(document.getElementById("txtStandard").value);
    //启用状态
    strParams += "&UsedStatus=" + reescape(document.getElementById("ddlUsedStatus").value);
    //chkUsedStatus = document.getElementById("chkUsedStatus");
    
//    if (chkUsedStatus.checked)
//    {
//        strParams += "&UsedStatus=1";
//    }
//    else
//    {
//        strParams += "&UsedStatus=0";
//    }
    
    /* 获取代理公司联系人信息 */
    //姓名
    strParams += "&ContactName=" + reescape(document.getElementById("txtContactName").value);
    //固定电话
    strParams += "&ContactTel=" + reescape(document.getElementById("txtContactTel").value);
    //移动电话
    strParams += "&ContactMobile=" + reescape(document.getElementById("txtContactMobile").value);
    //网络通讯
    strParams += "&ContactWeb=" + reescape(document.getElementById("txtContactWeb").value);
    //工号
    strParams += "&ContactCardNo=" + reescape(document.getElementById("txtContactCardNo").value);
    //公司职务
    strParams += "&ContactPosition=" + reescape(document.getElementById("txtContactPosition").value);
    //备注
    strParams += "&ContactRemark=" + reescape(document.getElementById("txtContactRemark").value);
    
    /* 获取代理公司联系人信息 */
    //附加信息
    strParams += "&Additional=" + reescape(document.getElementById("txtAdditional").value);
    //返回参数字符串
    return strParams;
}

/*
* 新建人才代理信息
*/
function NewHRProxyInfo()
{
    window.location.reload();
}
/*
* 返回按钮
*/
function DoBack()
{
    //获取查询条件
    var searchCondition = document.getElementById("hidSearchCondition").value;
    //获取模块功能ID
    moduleID = document.getElementById("hidModuleID").value;
    window.location.href = "HRProxy_Info.aspx?ModuleID=" + moduleID + searchCondition;
}

//打印功能
function fnPrintOrder() {
    
        var OrderNo = $.trim($("#codruleProxy_txtCode").val());
        if (OrderNo == '保存时自动生成' || OrderNo == '') {

            OrderNo = $.trim($("#divProxyCompanyNo").text());
            if (OrderNo == '保存时自动生成' || OrderNo == '') {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请保存您所填的信息！");
            }
            else {

                window.open('../../../Pages/PrinttingModel/HumanManager/HRProxyPrint.aspx?no=' + OrderNo);
            }
        }
        else {

            window.open('../../../Pages/PrinttingModel/HumanManager/HRProxyPrint.aspx?no=' + OrderNo);
        }
}