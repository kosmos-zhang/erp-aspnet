/**********************************************
* 类作用：   通用JS
* 建立人：   wuchenghao
* 建立时间： 2009-1-3 
* Copyright (C) 2007-2009 wuchenghao
* All rights reserved
***********************************************/

/*默认新建页面明细中备注省略显示的长度*/
var dRemarkLength = 6;
/*默认列表页面主题显示长度*/
var LTitleLength = 10;



/*****获得文件大小*****/
//filePath:文件路径，string
//result:结果，string
function GetFileSize(filePath) {
    var result = "";
    var fso = new ActiveXObject("Scripting.FileSystemObject");
    result = fso.GetFile(filePath).size;
    //alert(result);
    return result;
}

/*****判断文件大小是否超出限定大小*****/
//filePath:文件路径，string
//fileSizeMax:文件大小限定最大值,int
//result: 结果,boolean.若超出限制返回false;未超出返回true
function CheckFileSize(filePath) {
    var result = false;
    var fileSizeMax = 1024 * 1024 * 10; //目前为10M
    var fileSize = GetFileSize(filePath);
    if (fileSize > fileSizeMax) {
        result = false;
    } else {
        result = true;
    }
    //alert(result);
    return result;
}

/*****判断文件格式是否符合规定*****/
//filePath:文件路径，string
//fileTypeAllow:允许的文件类型,array
//result: 结果,boolean.若超出限制返回false;未超出返回true
function CheckFileType(filePath) {
    //允许的文件类型
    var fileTypeAllow = new Array();
    var fileTypeAllowStr = ".gif||.jpg||.jpeg||.doc||.ppt||.xls||.pdf||.rar||.zip";
    fileTypeAllow = fileTypeAllowStr.split("||");
    var pos = filePath.lastIndexOf(".");
    var lastname = filePath.substring(pos, filePath.length);
    var iCount = 0;
    while (iCount < fileTypeAllow.length) {
        if (lastname.toLowerCase() == fileTypeAllow[iCount]) {
            return true;
        }
        iCount = iCount + 1;
    }
    return false;
}

/*****JS执行调用RAR.exe实现压缩何解压缩*****/
function RunRar(fName, dName) {
    var winRar = new ActiveXObject("WScript.Shell");
    fName = fName.replace(/\\/, "\\\\");
    //dName=dName.replace(/\./,"_");
    //var cmd="winrar a "+ rName + ".rar " + fName + " -ep1" ;
    var cmd = "winrar a \"" + dName + "\" \"" + fName + "\" -ep1";
    //alert(cmd);
    winRar.run(cmd, 1, true);
}

/*****读取注册表信息*****/
function GetBoardKey() {
    WSHShell = new ActiveXObject("WScript.Shell");
    var bkey = WSHShell.RegRead("HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\ShockwaveFlash\\DisplayVersion");
    alert(bkey);
}

/*
* 校验输入长度
* 参数   objCheck 校验的控件
* 参数   length   需要的长度
* 返回值 true     正确输入
*        false    错误输入
*/
function CheckInputLength(objCheck, length) {
    var inputValue = objCheck.value;
    if (inputValue == "undefined" || inputValue == null || inputValue == "") {
        return false;
    }
    else {
        if (inputValue.length < length) {
            return false;
        }
    }
    return true;
}

/*
* 校验是否是正确的日期
* 参数   val   校验的控件
* 参数   split 日期分隔符
* 返回值 true  正确的日期
*        false 错误的日期
*/
function IsRightDateWithSplit(val, split) {
    if (val == null || val == "") {
        return true;
    }
    var year, month, day, checkDate;
    if (split != "undefined" && split != null && split != "") {
        var tempDate = val.split(split);
        if (tempDate.length == 3) {
            checkDate = tempDate[0].toString() + tempDate[1].toString() + tempDate[2].toString();
        } else {
            return false;
        }
    }
    else {
        checkDate = val;
    }
    if (!/\d/.test(checkDate)) {
        return false;
    }

    year = checkDate.substring(0, 4) - 0;
    month = checkDate.substring(4, 6) - 0;
    day = checkDate.substring(6) - 0;

    var date = new Date(year, month - 1, day);

    xYear = date.getFullYear();
    xMonth = date.getMonth() + 1;
    xDay = date.getDate();

    if (day != xDay || month != xMonth || year != xYear) {
        return false;
    }
    return true;
}

/*
* 校验是否是正确的日期
* 参数   val   校验的字符
* 返回值 true  正确的日期
*        false 错误的日期
*/
function IsRightDate(val) {
    if ("undefined" == val || val == null || val == "") {
        return true;
    }
    //分隔符为“-”
    if (val.indexOf("-") >= 0) {
        return IsRightDateWithSplit(val, "-");
    }
    //分隔符为“/”
    else if (val.indexOf("/") >= 0) {
        return IsRightDateWithSplit(val, "/");
    }
    //没有分隔符
    else {
        return IsRightDateWithSplit(val, "");
    }
}

/*
* 转换日期的格式
* 参数   val       校验的字符
* 参数   formatFlg 日期格式标志位  
*                  1 YYYYMMDD 
*                  2 YYYY/MM/DD 
*                  3 YYYY-MM-DD
* 返回值 转换后的日期
*        如果val不是正确的日期，则返回null
*        如果formatFlg为1，2，3以外时，返回null
*/
function ChangeDate(val, formatFlg) {
    if (IsRightDate(val)) {
        var year, month, day;
        //分隔符为“-”
        if (val.indexOf("-") >= 0) {
            //如果即为原来格式，返回
            if ("3" == formatFlg) return val;
            //获取年月日
            var date = val.split("-");
            year = date[0].length == 1 ? "0" + date[0] : date[0];
            month = date[1].length == 1 ? "0" + date[1] : date[1];
            day = date[2].length == 1 ? "0" + date[2] : date[2];

        }
        //分隔符为“/”
        else if (val.indexOf("/") >= 0) {
            //如果即为原来格式，返回
            if ("2" == formatFlg) return val;
            //获取年月日
            var date = val.split("/");
            year = date[0];
            month = date[1];
            day = date[2];

            year = date[0].length == 1 ? "0" + date[0] : date[0];
            month = date[1].length == 1 ? "0" + date[1] : date[1];
            day = date[2].length == 1 ? "0" + date[2] : date[2];
        }
        //没有分隔符
        else {
            //如果即为原来格式，返回
            if ("1" == formatFlg) return val;
            //获取年月日
            year = parseInt(val.substring(0, 4));
            month = parseInt(val.substring(4, 6));
            day = parseInt(val.substring(6));
        }
        if ("1" == formatFlg) return year.toString() + month.toString() + day.toString();
        else if ("2" == formatFlg) return year + "/" + month + "/" + day;
        else if ("3" == formatFlg) return year + "-" + month + "-" + day;
        else return null;
    }
    else {
        return null;
    }
}

/*
* 比较两个日期的大小
* 参数    date1 日期1
* 参数    date2 日期2
* 返回值  null  比较的两个日期中至少有一个不是正确的日期
*         -1    date1早于date2
*         0     date1等于date2
*         1     date1晚于date2
*         2     date1和date2中有一个为空
*/
function CompareDate(date1, date2) {
    if (date1 == "" || date2 == "") {
        return 2;
    }
    //如果是正确的日期则进行比较
    if (IsRightDate(date1) && IsRightDate(date2)) {
        dateOne = ChangeDate(date1, 2);
        dateTwo = ChangeDate(date2, 2);
        tempDate1 = Date.parse(dateOne);
        tempDate2 = Date.parse(dateTwo);
        if (tempDate1 > tempDate2) return 1;
        else if (tempDate1 < tempDate2) return -1;
        else return 0;

    }
    else {
        return null;
    }
}



/*
* 获取选择的行
* 返回值说明
*           null 表格没有
*           选择多行的时候，行数之间用逗号隔开，行号从0开始
*/
function GetSelectValue() {
    //获取一览表
    var table = document.getElementById("xgoss_tb");
    if (table == "undefined" || table == null) {
        return null;
    }
    //获取表的行数
    var rowCount = table.rows.length;
    var objs = document.getElementsByTagName('input');
    var select = "";
    for (var i = 0; i < objs.length; i++) {
        //CheckBox类型控件
        if (objs[i].getAttribute("type") == "checkbox" && objs[i].checked) {
            var index = objs[i].id.indexOf("CheckBox");
            if (index > -1) {
                var row = objs[i].id.substring(index + 9);
                if (select == "") {
                    select = row.toString();
                }
                else {
                    select += "," + row.toString();
                }
            }
        }
    }
    return select;
}


/*
* 有树形的表格，点击打开关闭记录处理
* 参数    index 点击的行数
* 参数    key   分支的key值
*/
function DoTreeTableDisplay(index, key) {
    //获取表格
    table = document.getElementById("xgossTreeTable");
    //获取表格长度
    rowCount = table.rows.length;
    //获取显示图片
    img = document.getElementById("img" + key);
    //获取图片地址
    src = img.src;
    //标志位定义    
    if (src.indexOf("icon_list_open") > -1) {
        //打开分支
        isOpen = false;
    }
    else {
        //收起分支
        isOpen = true;
    }
    index = parseInt(index);
    //遍历key下的值
    for (var i = index + 2; i < rowCount; i++) {
        //获取行
        row = table.rows[i];
        //行和key值相等时，执行处理
        if (row.id == key) {
            //显示具体内容行
            if (isOpen) {
                row.style.display = "";
            }
            //不显示具体内容行
            else {
                row.style.display = "none";
            }
        }
        else {
            break;
        }
    }
    //更换显示图片
    if (isOpen) {
        img.src = "Images/Menu/icon_list_open.gif";
    }
    else {
        img.src = "Images/Menu/icon_list_close.gif";
    }

}

function ShowPreventReclickDiv() {

    /**第一步：创建DIV遮罩层。*/
    var sWidth, sHeight;
    sWidth = window.screen.availWidth - 300;
    //屏幕可用工作区高度： window.screen.availHeight;
    //屏幕可用工作区宽度： window.screen.availWidth;
    //网页正文全文宽：     document.body.scrollWidth;
    //网页正文全文高：     document.body.scrollHeight;
    if (window.screen.availHeight > document.body.scrollHeight) {  //当高度少于一屏
        sHeight = window.screen.availHeight - 200;
    } else {//当高度大于一屏
        sHeight = document.body.scrollHeight - 200;
    }

    obj = document.getElementById("divPreventReclick");
    //隐藏信息提示DIV
    if (obj != null && typeof (obj) != "undefined") obj.style.display = "block";
    else {

        //	    //创建遮罩背景
        //	    var maskObj = document.createElement("div");
        //	    maskObj.setAttribute('id','divPreventReclick');
        //	    maskObj.style.position = "absolute";
        //	    maskObj.style.top = "0";
        //	    maskObj.style.left = "0";
        //	    maskObj.style.background = "#777";
        //	    maskObj.style.filter = "Alpha(opacity=50);";
        //	    maskObj.style.opacity = "0.3";
        //	    maskObj.style.width = sWidth + "px";
        //	    maskObj.style.height = sHeight + "px";
        //	    maskObj.style.zIndex = "2";
        //	    maskObj.innerHTML = "<iframe style='position: absolute; z-index:-1; width:"+sWidth+"; height:"+sHeight+";' frameborder='0'>  </iframe>";
        //	    //document.body.appendChild(maskObj);
        //	    //return maskObj.outerHTML;
        //	    document.body.appendChild(maskObj);

        // 整个div的大小和位子
        var preventReclick = "<div id='divPreventReclick' style='position:absolute;z-index:2; background-color:#777;"
                        + "left:0;top:0;filter:Alpha(opacity=0);width:" + sWidth + ";height:" + sHeight + ";' >";
        preventReclick += "<iframe style='position: absolute; width:" + sWidth + "; height:" + sHeight + ";' frameborder='0'>  </iframe>";
        preventReclick += "</div>";

        insertHtml("afterBegin", document.body, preventReclick);
    }
}

///////显示数据添加时的等待层
function AddPop() {
    showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/extanim64.gif", "处理数据中，请稍候……");
}
function showPopup(img, img1, retstr) {
    //    try{
    //          openRotoscopingDiv(true,"divMsgShadow","MsgShadowIframe");
    //        }
    //    catch(e)
    //    {
    ShowPreventReclickDiv();
    document.getElementById("Forms").style.display = "block";
    document.getElementById("Forms").innerHTML = Create_Div(img, img1, true);
    document.getElementById("FormContent").innerText = retstr;
    //    }
}
///////隐藏数据添加时的等待层
function hidePopup() {
    //try{closeRotoscopingDiv(true,"divMsgShadow");}
    //catch(e)
    //{
    obj = document.getElementById("divPreventReclick");
    //隐藏遮挡的DIV
    if (obj != null && typeof (obj) != "undefined") obj.style.display = "none";
    /* modified by wuzq 2009-05-21 end */
    document.getElementById("Forms").style.display = "none";

    //}


}
///////创建层（）
function Create_Div(img, img1, bool) {
    FormStr = "<table width=100% height='100' border='0' cellpadding=0 cellspacing=0 bgcolor=#FFFFFF>"
    FormStr += "<tr>"
    FormStr += "<td width=90%  bgcolor=#3F6C96>&nbsp;</td>"
    FormStr += "<td width=10% height=20 align=\"right\" bgcolor=#3F6C96>"
    if (bool) {
        FormStr += "<img src='" + img + "' style='cursor:pointer;display:block;' id='CloseImg' onClick='hidePopup()'>"
    }
    FormStr += "</td></tr><tr><td height='1' colspan='2' background='../../../Images/Pic/bg_03.gif'></td></tr><tr><td valign=top height=104 id='FormsContent' colspan=2 align=center>"
    FormStr += "<table width=100% border=0 cellspacing=0 cellpadding=0>"
    FormStr += "<tr>"
    FormStr += "<td width=25% align='center' valign=top style='padding-top:20px;'>"
    FormStr += "<img name=exit src='" + img1 + "' border=0></td>";
    FormStr += "<td width=75% height=50 id='FormContent' style='padding-left:5pt;padding-top:20px;line-height:17pt;' valign=top></td>"
    FormStr += "</tr></table>"
    FormStr += "</td></tr></table>"
    return FormStr;
}

///firefox的innertext
(function(bool) {
    function setInnerText(o, s) {
        while (o.childNodes.length != 0) {
            o.removeChild(o.childNodes[0]);
        }

        o.appendChild(document.createTextNode(s));
    }

    function getInnerText(o) {
        var sRet = "";

        for (var i = 0; i < o.childNodes.length; i++) {
            if (o.childNodes[i].childNodes.length != 0) {
                sRet += getInnerText(o.childNodes[i]);
            }

            if (o.childNodes[i].nodeValue) {
                if (o.currentStyle.display == "block") {
                    sRet += o.childNodes[i].nodeValue + "\n";
                } else {
                    sRet += o.childNodes[i].nodeValue;
                }
            }
        }

        return sRet;
    }

    if (bool) {
        HTMLElement.prototype.__defineGetter__("currentStyle", function() {
            return this.ownerDocument.defaultView.getComputedStyle(this, null);
        });

        HTMLElement.prototype.__defineGetter__("innerText", function() {
            return getInnerText(this);
        })

        HTMLElement.prototype.__defineSetter__("innerText", function(s) {
            setInnerText(this, s);
        })
    }
})(/Firefox/.test(window.navigator.userAgent));

//获取url中"?"符后的字串
function GetRequest() {
    var url = location.search;
    var theRequest = new Object();
    if (url.indexOf("?") != -1) {
        var str = url.substr(1);
        strs = str.split("&");
        for (var i = 0; i < strs.length; i++) {
            theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
        }
    }
    return theRequest;
}
//js去除前后空格
String.prototype.Trim = function() {
    return this.replace(/(^\s*)|(\s*$)/g, "");
}
String.prototype.LTrim = function() {
    return this.replace(/(^\s*)/g, "");
}
String.prototype.RTrim = function() {
    return this.replace(/(\s*$)/g, "");
}
//去除所有空格
function Trim(str, is_global) {
    var result;
    result = str.replace(/(^\s+)|(\s+$)/g, "");
    if (is_global.toLowerCase() == "g")
        result = result.replace(/\s/g, "");
    return result;
}

/***********************************************************
Function formatnumber(value,num)
Written by zergling
javascript版本的FormatNumber函数，用法与VBScript相同，第一个参数是待格式化的数值，第二个是保留小数位数
注意：返回的是字符串类型
第一个函数需要调用第二个函数，所以第二个不能去掉
***********************************************************/
function FormatAfterDotNumber(value, num) //四舍五入
{
    if (value != '' && typeof (value) != 'undefined') {
        var a_str = formatnumber(value, num);
        var a_int = parseFloat(a_str);
        if (value.toString().length > a_str.length) {
            var b_str = value.toString().substring(a_str.length, a_str.length + 1)
            var b_int = parseFloat(b_str);
            if (b_int < 5) {
                return a_str
            }
            else {
                var bonus_str, bonus_int;
                if (num == 0) {
                    bonus_int = 1;
                }
                else {
                    bonus_str = "0."
                    for (var i = 1; i < num; i++)
                        bonus_str += "0";
                    bonus_str += "1";
                    bonus_int = parseFloat(bonus_str);
                }
                a_str = formatnumber(a_int + bonus_int, num)
            }
        }
        return a_str
    }
    else {
        //此处原始为 return "0.00"; 现已改成如下形式(返回小数长度为num的0值)
        //2010-4-22 Modified by hexw
        var valuestr = "0.";
        var numLen = num;
        if (num == 0) {
            num = 1;
        }
        for (var i = 0; i < numLen; i++) {
            valuestr = valuestr + "0";
        }
        return valuestr;
    }
}

function formatnumber(value, num) //直接去尾
{
    var a, b, c, i
    a = value.toString();
    b = a.indexOf('.');
    c = a.length;
    if (num == 0) {
        if (b != -1)
            a = a.substring(0, b);
    }
    else {
        if (b == -1) {
            a = a + ".";
            for (i = 1; i <= num; i++)
                a = a + "0";
        }
        else {
            a = a.substring(0, b + num + 1);
            for (i = c; i <= b + num; i++)
                a = a + "0";
        }
    }
    return a
}
/***********************************************************
Function formatnumber(value,num)
Written by zergling
javascript版本的FormatNumber函数，用法与VBScript相同，第一个参数是待格式化的数值，第二个是保留小数位数
注意：返回的是字符串类型
第一个函数需要调用第二个函数，所以第二个不能去掉
***********************************************************/


/*
* 控制输入框的输入
* 整数值 只允许输入 0-9
*/
function Number_OnKeyDown() {
    var event = arguments[0] || window.event;
    //获取按下键的值
    var keyCode = event.charCode || event.keyCode;

    //数字键
    if ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 96 && keyCode <= 105) || (keyCode >= 37 && keyCode <= 40)
        || keyCode == 46 || keyCode == 8 || keyCode == 9 || keyCode == 13) {
    }
    else {
        window.event.returnValue = false;
    }
}

/*
* 控制输入框的输入
* 整数值 只允许输入 0-9 小数点
*/
//hm edit at 0929/ keycode==229 当是全角的时候可以让他输入
function Numeric_OnKeyDown() {
    var event = arguments[0] || window.event;
    //获取按下键的值
    var keyCode = event.charCode || event.keyCode;
    //数字键
    if ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 96 && keyCode <= 105) || (keyCode >= 37 && keyCode <= 40)
        || keyCode == 46 || keyCode == 8 || keyCode == 110 || keyCode == 190 || keyCode == 9 || keyCode == 13 || keyCode == 229) {
    }

    else {
        window.event.returnValue = false;
    }
}




/*
* 控制输入框的输入
* 整数值 只允许输入 0-9 屏蔽shift键-Modify By Moshenlin 
*/
function Number_KeyDown() {
    var event = arguments[0] || window.event;
    //获取按下键的值
    var keyCode = event.charCode || event.keyCode;
    //数字键
    if (event.shiftKey && (keyCode >= 48 && keyCode <= 57)) {
        window.event.returnValue = false;
    }
    else if ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 96 && keyCode <= 105) || (keyCode >= 37 && keyCode <= 40)
        || keyCode == 46 || keyCode == 8 || keyCode == 9 || keyCode == 13) {
    }
    else {
        window.event.returnValue = false;
    }
}


/***********************************************************
字符转日期（可进行日期比较）
***********************************************************/
function StringToDate(DateStr) {
    var converted = Date.parse(DateStr);
    var myDate = new Date(converted);
    if (isNaN(myDate)) {
        var arys = DateStr.split('-');
        myDate = new Date(arys[0], --arys[1], arys[2]);
    }
    return myDate;
}
/***********************************************************
正整数判断
***********************************************************/
function PositiveInteger(retval) {
    var r = /^\+?[1-9][0-9]*$/; //正整数 
    if (r.test(retval))
        return true;
    else
        return false;
}

/*
* where：插入位置。包括beforeBegin,beforeEnd,afterBegin,afterEnd。
* el：用于参照插入位置的html元素对象
* html：要插入的html代码
*/
function insertHtml(where, el, html) {
    where = where.toLowerCase();
    //IE  
    if (el.insertAdjacentHTML) {
        switch (where) {
            case "beforebegin":
                el.insertAdjacentHTML('BeforeBegin', html);
                return el.previousSibling;
            case "afterbegin":
                el.insertAdjacentHTML('AfterBegin', html);
                return el.firstChild;
            case "beforeend":
                el.insertAdjacentHTML('BeforeEnd', html);
                return el.lastChild;
            case "afterend":
                el.insertAdjacentHTML('AfterEnd', html);
                return el.nextSibling;
        }
        throw 'Illegal insertion point -> "' + where + '"';
    }
    //FF
    var range = el.ownerDocument.createRange();
    var frag;
    switch (where) {
        case "beforebegin":
            range.setStartBefore(el);
            frag = range.createContextualFragment(html);
            el.parentNode.insertBefore(frag, el);
            return el.previousSibling;
        case "afterbegin":
            if (el.firstChild) {
                range.setStartBefore(el.firstChild);
                frag = range.createContextualFragment(html);
                el.insertBefore(frag, el.firstChild);
                return el.firstChild;
            }
            else {
                el.innerHTML = html;
                return el.firstChild;
            }
        case "beforeend":
            if (el.lastChild) {
                range.setStartAfter(el.lastChild);
                frag = range.createContextualFragment(html);
                el.appendChild(frag);
                return el.lastChild;
            }
            else {
                el.innerHTML = html;
                return el.lastChild;
            }
        case "afterend":
            range.setStartAfter(el);
            frag = range.createContextualFragment(html);
            el.parentNode.insertBefore(frag, el.nextSibling);
            return el.nextSibling;
    }
    throw 'Illegal insertion point -> "' + where + '"';
}


/****************************测字符串的长度*********************************/
/**
*obj参数：strValue，要测试的字符串；len，最大长度,
*返回值:大于最大值，返回false；负责返回true。
*/
function fnCheckStrLen(strValue, len) {
    var newvalue = strValue.replace(/[^\x00-\xff]/g, "**");
    var length = newvalue.length;
    if (length > len) {
        return false;
    }
    else {
        return true;
    }
}
/****************************Input只能输入小数后两位*********************************/
/**
*obj参数：传入的控件ID,
*/

function FractionDigits(obj) {
    try {
        obj.value = obj.value.replace(/[^\d+\.]|(^\..*)/g, '').replace(/(\d+\.\d{1,1})((?:\d+)|(?:\.+))?/g, '$1');
    }
    catch (err) {

    }
}
/****************************特殊字符校验时处理*********************************/
function CheckSpecialWords() {
    var RetvalStr = "";
    try {
        $("input:text", document.forms[0]).each(function() {

            if (this.value != "") {
                if (this.getAttribute("SpecialWorkCheck") != null) {
                    if (!CheckSpecialWord(this.value)) {
                        RetvalStr += this.getAttribute("SpecialWorkCheck") + ",";
                    }
                }
            }
        }
               );
        $("textarea", document.forms[0]).each(function() {
            if (this.value != "") {

                if (this.getAttribute("SpecialWorkCheck") != null) {
                    if (!CheckSpecialWord(this.value))
                        RetvalStr += this.getAttribute("SpecialWorkCheck") + ",";
                }
            }
        }
               );
        $("asp:TextBox", document.forms[0]).each(function() {
            if (this.value != "") {

                if (this.getAttribute("SpecialWorkCheck") != null) {
                    if (!CheckSpecialWord(this.value))
                        RetvalStr += this.getAttribute("SpecialWorkCheck") + ",";
                }
            }
        }
               );
        if (RetvalStr != "")
            RetvalStr = RetvalStr.substr(0, RetvalStr.length - 1);
        return RetvalStr;
    }
    catch (err) {
        return "";
    }
}

/*只构造遮罩层等  
*需在页面上添加
<div id="divRotoscoping" style="display:none">
<iframe id="divIframe" frameborder="0" width="100%" ></iframe>
</div>
*isLoading 为是否需要loading
*divRotoscoping 页面上自己定义的div的ID(例：divMask)
*divIframe 页面上自己定义的iframe的ID(例：MaskIframe)
* by pdd*/
/*********打开遮罩层*********/
function openRotoscopingDiv(isLoading, divRotoscoping, divIframe) {
    var sWidth, sHeight;
    sWidth = window.screen.availWidth;
    if (window.screen.availHeight > document.body.scrollHeight) {  //当高度少于一屏
        sHeight = window.screen.availHeight;
    }
    else {
        sHeight = document.body.scrollHeight;
    }

    /**构造遮罩DIV属性**/
    var maskObj = document.getElementById(divRotoscoping)
    maskObj.style.position = "absolute";
    maskObj.style.top = "0";
    maskObj.style.left = "0";
    maskObj.style.background = "#777";
    maskObj.style.filter = "Alpha(opacity=20);";
    maskObj.style.opacity = "0.3";
    maskObj.style.offsetWidth = sWidth + "px";
    maskObj.style.offsetHeight = sHeight + "px";
    maskObj.style.zIndex = "20";
    maskObj.style.display = "block";
    /*设置Iframe高度*/
    //alert(document.documentElement.clientHeight+","+document.body.scrollHeight)
    /*判断是否包含垂直滚动条*/
    if (document.documentElement.clientHeight > document.body.scrollHeight)
        document.getElementById(divIframe).height = document.documentElement.clientHeight - 3; //有垂直滚动条
    else
        document.getElementById(divIframe).height = document.documentElement.scrollHeight - 3; //无垂直滚动条

    document.getElementById(divIframe).width = document.documentElement.clientWidth - 3;
    if (isLoading) {
        document.getElementById("Forms").style.display = "block";
        document.getElementById("Forms").innerHTML = Create_Div("../../../Images/Pic/Close.gif", "../../../Images/Pic/extanim64.gif", true);
        document.getElementById("FormContent").innerText = "处理数据中，请稍候……";
    }
}
/**********关闭遮罩层*********/
function closeRotoscopingDiv(isLoading, divRotoscoping) {
    document.getElementById(divRotoscoping).style.display = "none";
    if (isLoading) {
        document.getElementById("Forms").style.display = "none";
    }
}




//获取当前日期
function GetNow() {
    var today = new Date();
    var todaynow = today.getYear() + "-" + (today.getMonth() + 1) + "-" + today.getDate(); //当前日期
    return todaynow;
}

/*
*设置浮点数百分比格式
*使用前需要的num 做验证
*num 需要格式的数字 默认 2位
* PDD
*/
function FormatPercent(num) {
    var tmp = parseFloat(num * 100);
    return tmp.toFixed(2) + "%";
}

/*将百分数转化成浮点 4位有效数字 需自己对参数进行验证 PDD*/
function PercentToFloat(str) {
    if (str == "")
        return "";
    var tmp = str.replace("%", "");
    return parseFloat(parseFloat(tmp) / 100).toFixed(4);
}

//将过长的名称截取
function GetStandardString(value, length) {
    if (strlen(value.toString()) > length) {
        return value.substring(0, length - 3) + "...";
    }
    return value;
}

/***--------------------------截取字符串-------------------------------------***/
function fnjiequ(str, strlen) {
    return str.length > strlen + 3 ? str.slice(0, strlen) + "…" : str;
}


//判断是否为Numeric型
function IsNumeric(NUM, countInt, countFloat) {
    var i, j, strTemp, tempDotArray, dotCount;
    strTemp = "0123456789.";
    dotCount = 0;
    if (NUM.length == 0)
        return false;
    for (i = 0; i < NUM.length; i++) {
        j = strTemp.indexOf(NUM.charAt(i));
        if (j == -1) {
            //说明有字符不是数字
            return false;
        }
        if (NUM.charAt(i) == '.') {
            dotCount = dotCount + 1;
        }

    }
    tempDotArray = NUM.split('.');
    if (tempDotArray.length > 1) {
        if (NUM.length > (countInt + countFloat + 1)) {
            return false;
        }
        if (tempDotArray[1].toString().length > countFloat) {
            return false;
        }
        if (tempDotArray[0].toString().length > countInt) {
            return false;
        }
    }
    else {
        if (NUM.length > countInt) {
            return false;
        }
    }
    if (dotCount > 1) {
        return false;
    }
    //说明是正确的Numeric型
    return true;
}
//判断是否为Numeric型(可带负号)
function IsNumericFH(NUM, countInt, countFloat) {
    var i = NUM.toString();
    var i1 = i.substring(0, 1);
    if (i1 == "-") {
        var i2 = i.substring(1, i.length);
        return IsNumberOrNumeric(i2, countInt, countFloat);
    }
    return IsNumberOrNumeric(NUM, countInt, countFloat)
}


//判断数字或Numeric型
function IsNumberOrNumeric(NUM, countInt, countFloat) {
    if (IsNumeric(NUM, countInt, countFloat))
        return true;
    return false;
}



//对输入的数字自动保留两位小数，四舍五入
/*
obj 输入数字的控件
fractionDigits 保留的小数位数
flag 判断是否为负数 pg：Number_round(this,2,1)负数验证并四舍五入
Number_round(this,2)正数验证并四舍五入
整数自动保留两位小数 pg: N.00
调用：如：onchange="Number_round(this,2)";
*/

function Number_round(obj, fractionDigits, flag) {
    var numberStr = CtoH(obj);

    if (flag) {
        if (!IsNumericFH(numberStr, 1000, 1000)) {
            obj.value = "";
            return;
        }
    }
    else {
        if (!IsNumeric(numberStr, 1000, 1000)) {
            obj.value = "";
            return;
        }
    }

    var RetValue = "";
    var num = numberStr.toString().split('.');
    if (num[1] != "" && num[1] != null && parseInt(num[1]) == 0) {
        RetValue = numberStr;
    }
    else {
        if (numberStr != parseInt(numberStr)) {
            with (Math) {
                RetValue = round(numberStr * pow(10, fractionDigits)) / pow(10, fractionDigits);
            }
            var Rev = RetValue.toString().split('.');
            if (Rev[1] == undefined || Rev[1] == null || Rev[1] == "") {
                RetValue = RetValue + ".00";
            }
            else if (Rev[1].toString().length == 1) {
                RetValue = RetValue + "0";
            }
        }
        else {
            RetValue = numberStr + ".00";
        }
    }
    obj.value = parseFloat(RetValue).toFixed(fractionDigits);

}

/*
Ctoh函数用于辅助Number_round函数过滤输入法上的全角问题
从而直接将全角输入变成半角
*/

function CtoH(obj) {
    var str = obj.value;
    var result = "";
    for (var i = 0; i < str.length; i++) {
        if (str.charCodeAt(i) == 12288) {
            result += String.fromCharCode(str.charCodeAt(i) - 12256);
            continue;
        }
        if (str.charCodeAt(i) > 65280 && str.charCodeAt(i) < 65375)
            result += String.fromCharCode(str.charCodeAt(i) - 65248);
        else result += String.fromCharCode(str.charCodeAt(i));
    }
    return result;

}

function reescape(val) {
    return " " + escape(val).Trim();
}

function fracDigits(num) {
    var ReturnValue;
    for (var i = 0; i < num; i++) {
        ReturnValue += "0";
    }
    return ReturnValue;
}


//对数字保留几位小数，在页面上四舍五入--hm添加
//a_Num---需要处理的数字
//a_Bit---需要保留的几位小数
function NumRound(a_Num, a_Bit) {
    return ((Math.round(a_Num * Math.pow(10, a_Bit)) / Math.pow(10, a_Bit)).toFixed(a_Bit));
}



//销售报表用的对数字保留几位小数，在页面上四舍五入--hm添加
//a_Num---需要处理的数字
//a_Bit---需要保留的几位小数
function NumRoundOfSellModule(a_Num, a_Bit) {
    if (a_Num != '') {
        return ((Math.round(a_Num * Math.pow(10, a_Bit)) / Math.pow(10, a_Bit)).toFixed(a_Bit));
    } else {
        return '';
    }
}

//两日期天数差
function getDateDiff(date1, date2) {

    var re = /^(\d{4})\S(\d{1,2})\S(\d{1,2})$/;
    var dt1, dt2;
    if (re.test(date1)) {
        dt1 = new Date(RegExp.$1, RegExp.$2 - 1, RegExp.$3);
    }

    if (re.test(date2)) {
        dt2 = new Date(RegExp.$1, RegExp.$2 - 1, RegExp.$3);
    }

    return Math.floor((dt2 - dt1) / (1000 * 60 * 60 * 24))

}

//截取某长度内容的方法
function SubValue(num, value) {
    if (strlen(value) > num) {
        return value.substr(0, num) + "…";
    }
    else {
        return value;
    }
}

/*控件居中
*objID 需要居中的控件 ID名称 
*div中的样式 top: 30%; left: 40%; margin: 5px 0 0 -400px;" 需删除
*isPercent 表示 该控件 定义的高度是百分比（true） 还是数值（fasle）
*必须在控件显示之后调用该方法
*by pdd
*/
function CenterToDocument(objID, isPercent) {
    var obj = document.getElementById(objID);

    /*定义位置*/
    var _top = 0;
    var _left = 0;
    var objWidth = 0, objHeight = 0;

    //获取滚动条滚动的长度
    var scrollLength = getScrollTop();
    /*获取控件尺寸*/
    if (isPercent) {
        objWidth = obj.style.width.replace("px", "") == "" ? 0 : PercentToFloat(obj.style.width);
        //  objHeight=obj.style.height.replace("px","")==""?0:PercentToFloat(obj.style.height); 
    }
    else {
        objWidth = obj.style.width.replace("px", "") == "" ? 0 : obj.style.width.replace("px", "");
    }
    objHeight = obj.offsetHeight == "" ? 0 : obj.offsetHeight;

    /*定义文档尺寸*/
    var docWidth = 0, docHight = 0;

    /*获取当前文档尺寸*/
    if (document.documentElement.clientHeight > document.body.scrollHeight) {
        //有垂直滚动条
        docWidth = parseInt(document.documentElement.scrollWidth);
        //docHight=parseInt(document.documentElement.clientHeight);
    }
    else {
        //无垂直滚动条
        docWidth = parseInt(document.documentElement.scrollWidth);
        //docHight=parseInt( document.documentElement.scrollHeight);
    }
    docHight = parseInt(document.documentElement.clientHeight);
    /*设置位置*/
    if (isPercent) {
        /*定义的控件尺寸为百分比*/
        //_top=docHight*(1-(objHeight==0?0.3:objHeight))/2;
        _left = docWidth * (1 - (objWidth == 0 ? 0.3 : objWidth)) / 2;
    }
    else {
        /*定义的控件尺寸为数值*/

        _left = (docWidth - (objWidth == 0 ? 100 : objWidth)) / 2;
    }
    _top = (docHight - (objHeight == 0 ? 200 : objHeight)) / 3;
    //alert(_top+","+_left+","+scrollLength+","+objHeight);
    obj.style.top = parseInt(_top + scrollLength) + "px";
    obj.style.left = parseInt(_left) + "px";


}

/********************
* 取窗口滚动条高度 
******************/
function getScrollTop() {
    var scrollTop = 0;
    if (document.documentElement && document.documentElement.scrollTop) {
        scrollTop = document.documentElement.scrollTop;
    }
    else if (document.body) {
        scrollTop = document.body.scrollTop;
    }
    return scrollTop;
}


/********************
* 取窗口可视范围的高度 
*******************/
function getClientHeight() {
    var clientHeight = 0;
    if (document.body.clientHeight && document.documentElement.clientHeight) {
        var clientHeight = (document.body.clientHeight < document.documentElement.clientHeight) ? document.body.clientHeight : document.documentElement.clientHeight;
    }
    else {
        var clientHeight = (document.body.clientHeight > document.documentElement.clientHeight) ? document.body.clientHeight : document.documentElement.clientHeight;
    }
    return clientHeight;
}

/********************
* 取文档内容实际高度 
*******************/
function getScrollHeight() {
    return Math.max(document.body.scrollHeight, document.documentElement.scrollHeight);
}


/*
*拖动层
*objID div的ID
*evt 直接传event对象
* 调用示例：onmousedown="MoveDiv('divGetProductInfo',event)"
*切不可整个DIV所有区域点击都可以拖动，这样将导致input失效
**By pdd
*/
function MoveDiv(objID, evt) {
    /*读取控件*/
    var o = document.getElementById(objID);

    /*验证控件*/
    if (!o) return;

    /*设置鼠标样式*/
    o.style.cursor = "move";

    /*左边距*/
    var relLeft = evt.clientX - o.offsetLeft;
    /*上边距*/
    var relTop = evt.clientY - o.offsetTop;

    /*抓取事件*/
    if (!window.captureEvents)
        o.setCapture();
    else
        window.captureEvents(Event.MOUSEMOVE | Event.MOUSEUP);

    /*鼠标移动*/
    document.onmousemove = function(e) {
        if (!o) return;
        e = e ? e : window.event;
        if (e.clientX - relLeft <= 0)
            o.style.left = 0 + "px";
        else if (e.clientX - relLeft >= document.documentElement.clientWidth - o.offsetWidth - 2)
            o.style.left = (document.documentElement.clientWidth - o.offsetWidth - 2) + "px";
        else
            o.style.left = e.clientX - relLeft + "px";
        if (e.clientY - relTop <= 1)
            o.style.top = 1 + "px";
        else if (e.clientY - relTop >= document.documentElement.clientHeight - o.offsetHeight - 2)
            o.style.top = (document.documentElement.clientHeight - o.offsetHeight - 2) + "px";
        else
            o.style.top = e.clientY - relTop + "px";

    }

    /*鼠标释放*/
    document.onmouseup = function() {
        if (!o) return;
        if (!window.captureEvents)
            o.releaseCapture();
        else
            window.releaseEvents(Event.MOUSEMOVE | Event.MOUSEUP);

        document.onmousemove = null;
        document.onmouseup = null;

        /*撤销鼠标移动样式*/
        o.style.cursor = "default";
    }

}


/*
*为onkeydown追加事件
*/


//ff && ie Event start here
function SearchEvent() {
    if (document.all)
        return event;

    func = SearchEvent.caller;
    while (func != null) {
        var arg0 = func.arguments[0];
        if (arg0) {
            if (arg0.constructor == MouseEvent) // 如果就是event 对象
                return arg0;
            if (arg0.constructor == KeyboardEvent) // 如果就是event 对象
                return arg0;
            if (arg0.constructor == Event) // 如果就是event 对象
                return arg0;
        }
        func = func.caller;
    }
    return null;
}
//eventName,without 'on' for: click
function AddEvent(obj, eventName, handler) {
    if (document.all) {
        obj.attachEvent("on" + eventName, handler);
    } else {
        obj.addEventListener(eventName, handler, false);
    }
}

function diabledF5(e) {
    var obj;
    if (obj == null) {
        return;
    }
    if (window.parent.document.getElementById("txtIsFresh") != null)
        obj = window.parent.document.getElementById("txtIsFresh");
    else
        obj = document.getElementById("txtIsFresh");
    if (document.all) {
        if (event.keyCode == 116) {
            obj.value = true;
        }
    }

    var evt = SearchEvent();
    if (evt.charCode == 116) {
        obj.value = true;
    }

};


AddEvent(document, "keydown", diabledF5);

/*追加事件结束*/


/*根据ProductID和仓库ID,绑定批次列表*/
/*ProductID:物品ID，StorageControlID仓库控件ID，ListControlID：下拉列表控件ID，IsBatchNo：是否启用批次，BatchNo：批次(新建时不用传值，查看时传入批次)*/
function GetBatchList(ProductID, StorageControlID, ListControlID, IsBatchNo, BatchNo) {
    if (IsBatchNo == "" || IsBatchNo == "0") {
        document.getElementById(ListControlID).options[0] = new Option("-未启用-", "");
    }
    else {
        var StorageID = $("#" + StorageControlID).val();
        var paUrl = "Act=GetBatchList&ProductID=" + ProductID + "&StorageID=" + StorageID;
        $.ajax({
            type: "POST",
            url: '../../../Handler/Office/StorageManager/StorageOutSellInfo.ashx?' + paUrl, //目标地址
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
                //AddPop();
            },
            complete: function() {// hidePopup();
            },
            error: function() {
                //popMsgObj.ShowMsg('请求发生错误');                        
            },
            success: function(item) {
                ClearBatchList(ListControlID); //填充批次前清除上一次列表
                if (item.data.length > 0)//多条数据填充批次
                {
                    if (item.data.length == 1 && item.data[0].BatchNo == "") {
                        document.getElementById(ListControlID).options[0] = new Option("-请选择-", "");
                    }
                    else {
                        for (var i = 0; i < item.data.length; i++) {
                            //填充批次列表
                            document.getElementById(ListControlID).options[i] = new Option(item.data[i].BatchNo, item.data[i].BatchNo);
                            if (typeof (BatchNo) != "undefined") { $("#" + ListControlID).val(BatchNo); }
                        }
                    }
                }
                else {
                    document.getElementById(ListControlID).options[0] = new Option("-请选择-", "");
                }
            }
        });
    }

}
//填充批次前清除上一次列表
function ClearBatchList(ListControlID) {
    var length = document.getElementById(ListControlID).options.length;
    if (length > 1) {
        for (i = length - 1; i >= 0; i--) {
            document.getElementById(ListControlID).remove(i);
            length--;
        }
    }
}

//分店
//根据ProductID绑定批次列表
//ProductID:物品ID，ListControlID：下拉列表控件ID，IsBatchNo：是否启用批次，BatchNo：批次(新建时不用传值，查看时传入批次)
function GetSubBatchList(ProductID, ListControlID, IsBatchNo, BatchNo, deptID, companyCD) {
    $("#" + ListControlID).html("");
    if (IsBatchNo == "" || IsBatchNo == "0") {
        document.getElementById(ListControlID).options[0] = new Option("-未启用-", "");
    }
    else {
        var paUrl = "action=GETBATCHNO&productID=" + ProductID + "&deptID=" + deptID + "&companyCD=" + companyCD;
        $.ajax({
            type: "POST",
            url: '../../../Handler/Office/LogisticsDistributionManager/StorageProductQuery.ashx', //目标地址
            dataType: 'json', //返回json格式数据
            data: paUrl,
            cache: false,
            beforeSend: function() {
            },
            complete: function() {// hidePopup();
            },
            error: function() {
            },
            success: function(item) {
                if (item.data.length > 0)//多条数据填充批次
                {
                    if (item.data.length == 1 && item.data[0].BatchNo == "") {
                        document.getElementById(ListControlID).options[0] = new Option("-请选择-", "");
                    }
                    else {
                        for (var i = 0; i < item.data.length; i++) {
                            //填充批次列表
                            document.getElementById(ListControlID).options[i] = new Option(item.data[i].BatchNo, item.data[i].BatchNo);
                        }
                    }
                    if (typeof (BatchNo) != "undefined") { $("#" + ListControlID).val(BatchNo); }

                }
                else {
                    document.getElementById(ListControlID).options[0] = new Option("-请选择-", "");
                }
            }
        });
    }

}


/*Start 明细焦点处理相关*/
/*

1.参数说明
objSelf:当前对象名称,敲上下键时会进行上下移动
objNext:下一个需要获得焦点的输入框
rows:当前行数,当第一行的数量输入完毕敲回车后，若明细中无第二行明细，则新加一行，若有第二行，则直接使第二行的物品编号获得焦点
isAddRow:是否添加新行(true:是，false:否),正常在物品编号调用时传false,在后面的数量调用时传true,目前所列模块只在物品编号和数量两个地方做处理

2.备注
在明细物品编号和数量中，加上onkeydown属性调用此方法,目前只控制回车(平行移动)、上下键(指定列上下移动)
页面声明eventObj.Table(明细tableID，大部分页面明细用的是dg_Log,个别页面用的非dg_Log)
*/
var eventObj = new Object();
eventObj.Table = ''; //'dg_Log';

function SetEventFocus(objSelf, objNext, rows, isAddRow) {
    /*  onkeydown时触发该方法
    Enter:13
    Backspace:8
    Left Arrow:37
    Up Arrow:38
    Right Arror:39
    Down Arror:40
    */
    if (eventObj.Table != '' && eventObj.RowLine != '') {

        var haveRows = 0; /*明细中可见行数*/
        var signFrame = findObj(eventObj.Table, document);
        for (var i = 0; i < signFrame.rows.length - 1; i++) {
            if (signFrame.rows[i + 1].style.display != "none") {
                haveRows++;
            }
        }
        var keyS = GetCommonEventCode();
        if (keyS == 13) {
            if (isAddRow) {

                if (((rows + 1) > haveRows)) {
                    AddSignRow();
                }
                /*获取下一行:若添加了三行，中间第二行被删除时，在第一行的数据后敲回车，行数往下顺移 */
                rows++;
                SetEventObjFocus(EventNextRow(rows), objNext);

            }
            else {
                SetEventObjFocus(rows, objNext);
            }
        }
        /*向上箭头*/
        if (keyS == 38) {
            rows--;
            SetEventObjFocus(EventPreviousRow(rows), objSelf);

        }
        /*向下键头*/
        if (keyS == 40) {
            rows++;
            SetEventObjFocus(EventNextRow(rows), objSelf);

        }
    }

}
/*设置对象获取焦点*/
function SetEventObjFocus(rows, objEvent) {
    var signFrame = findObj(eventObj.Table, document);
    if (signFrame.rows[rows].style.display != "none") {
        if (!document.getElementById(objEvent + rows).disabled) {
            document.getElementById(objEvent + rows).focus();
        }

    }
}
/*获取下一行*/
function EventNextRow(rows) {
    var signFrame = findObj(eventObj.Table, document);
    if (rows > signFrame.rows.length - 1) {
        return --rows;
    }

    if (signFrame.rows[rows].style.display != "none") {
        return rows;
    }
    else {
        rows++;
        return EventNextRow(rows);
    }
}

/*获取上一行*/
function EventPreviousRow(rows) {

    if (rows < 1) {
        return ++rows;
    }
    var signFrame = findObj(eventObj.Table, document);
    if (signFrame.rows[rows].style.display != "none") {
        return rows;
    }
    else {
        rows--;
        return EventPreviousRow(rows);
    }
}





function GetCommonEventCode(evt) {

    if (typeof evt == "undefined" || evt == null) {
        evt = SearchCommonEvent();
        if (typeof evt == "undefined" || evt == null) {
            return null;
        }
    }

    return evt.keyCode || evt.charCode;
}
function SearchCommonEvent() {
    if (document.all)
        return event;

    func = SearchCommonEvent.caller;
    while (func != null) {
        var arg0 = func.arguments[0];
        if (arg0) {
            if (arg0.constructor == MouseEvent) // 如果就是event 对象
                return arg0;
            if (arg0.constructor == KeyboardEvent) // 如果就是event 对象
                return arg0;
            if (arg0.constructor == Event) // 如果就是event 对象
                return arg0;
        }
        func = func.caller;
    }
    return null;
}
/*End 明细焦点处理相关*/
