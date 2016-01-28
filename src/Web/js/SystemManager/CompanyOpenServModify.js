$(document).ready(function(){
	$.formValidator.initConfig({formid:"frmMain",onerror:function(){hidePopup();alert("页面上有错");}});
	$("#txtCompanyCD").formValidator({tipid:"CompanyCDTip",onshow:"请输入公司代码",onfocus:"数字，字母或者下划线",oncorrect:"该公司代码可用"}).inputValidator({min:1,onerror:"公司代码不能为空,请确认"}).functionValidator({
	    fun:function(val,elem){
	        var letterOrNumber = /^\w+$/.test(val);
	        if (!letterOrNumber){
	            return "请不要输入数字和字母以外的。";
	        }
	        return true;
	    }
	}).ajaxValidator({
        type : "get",
        url : "../../Handler/SystemManager/CompanyOpenServInfo.ashx",
        datatype : "CompanyCD",
        success : function(data){
            if( data == "1" )
	        {
                return true;
	        }
            else
	        {
                return false;
	        }
        },
        onerror : "该公司代码已经存在，请更换公司代码",
        onwait : "正在对公司代码是否存在进行校验，请稍候..."
	});
	
	$("#txtMaxRoles").formValidator({tipid:"MaxRolesTip",onshow:"请输入公司最大角色数",onfocus:"输入整数", oncorrect:" "}).regexValidator({regexp:"intege1",datatype:"enum",onerror:"正整数格式不正确"});
	
	$("#txtMaxUser").formValidator({tipid:"MaxUserTip",onshow:"请输入公司最大用户数",onfocus:"输入整数", oncorrect:" "}).regexValidator({regexp:"intege1",datatype:"enum",onerror:"正整数格式不正确"});
	
	$("#txtMaxDocSize").formValidator({tipid:"MaxDocSizeTip",onshow:"请输入公司文件大小上限",onfocus:"输入整数", oncorrect:" "}).regexValidator({regexp:"intege1",datatype:"enum",onerror:"正整数格式不正确"});
	
	$("#txtSingleDocSize").formValidator({tipid:"SingleDocSizeTip",onshow:"请输入单个文件大小上限",onfocus:"输入整数", oncorrect:" "}).regexValidator({regexp:"intege1",datatype:"enum",onerror:"正整数格式不正确"});
	
	$("#txtMaxDocNum").formValidator({tipid:"MaxDocNumTip",onshow:"请输入文件总数",onfocus:"输入整数", oncorrect:" "}).regexValidator({regexp:"intege1",datatype:"enum",onerror:"正整数格式不正确"});
	
	$("#txtOpenDate").formValidator({tipid:"OpenDateTip",onshow:"请输入生效日期",onfocus:"客户公司的生效日期", oncorrect:" "}).functionValidator({
	     fun:function(val,elem){
	        if (!IsRightDate(val))
	        {
                return "请输入正确的日期";
	        }
            var openDate = document.getElementById("txtOpenDate").value;
            var closeDate = document.getElementById("txtCloseDate").value;
            if (openDate != "" && closeDate != "")
            {
                if (CompareDate(openDate, closeDate) == "1")
                {
                    return "您输入的生效日期晚于失效日期。";
                }
            }
	        return true;
	    } 
	});
	$("#txtCloseDate").formValidator({tipid:"CloseDateTip",onshow:"请输入失效日期",onfocus:"客户公司的失效日期", oncorrect:" "}).functionValidator({
	     fun:function(val,elem){
	        if (!IsRightDate(val))
	        {
                return "请输入正确的日期";
	        }
            var openDate = document.getElementById("txtOpenDate").value;
            var closeDate = document.getElementById("txtCloseDate").value;
            if (openDate != "" && closeDate != "")
            {
                if (CompareDate(openDate, closeDate) == "1")
                {
                    return "您输入的生效日期晚于失效日期。";
                }
            }
	        return true;
	    } 
	});
	
	$("#txtRemark").formValidator({tipid:"RemarkTip",onshow:"请输入备注",onfocus:"关于客户公司的一些说明备注", oncorrect:" "});
	
});


submitFlag = false;

function DoBack()
{
    submitFlag = false;
    window.open("CompanyOpenServ_Query.aspx", "_mainFrame");
}

function DoCheck()
{
    showPopup();
    submitFlag = true;
}