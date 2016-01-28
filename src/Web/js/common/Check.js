
/**********************************************
 * 类作用：   验证JS
 * 建立人：   王玉贞
 * 建立时间： 2005-9-25 
 * Copyright (C) 2005-2009 wangyz
 * All rights reserved
 ***********************************************/ 


 

 //判断是否为数字  added by jiangym
function  IsNumber(strIn) 
{ 
	return strIn.match(/^[0-9_]+$/); 
} 

 //判断是否为数字和字母 added by jiangym
 function isNumorZm(str)
 {
    var   r   =  /^[A-Za-z0-9]+$/　
     return  r.test(str); 　 
 }
  
function IsZint(str)
{
  var   r   =  /^[0-9]*[1-9][0-9]*$/　　//正整数     

    return  r.test(str);   
} 
//判断是否为整数    added by jiangym
function Isint(str)
{
  var   r   =  /^[0-9]*[0-9][0-9]*$/　　//整数     

    return  r.test(str);   
}

//判断是否为实数
function Isfloat(str) 
{
    if (/^\-?([1-9]\d*|0)(\.\d*)?$/.test(str)) 
    {
        return true;
    }
    else 
    {
        return false;
    }
} 


//判断数字或Numeric型
function IsNumberOrNumeric(NUM,countInt,countFloat)
{
    if((IsNumber(NUM))||(IsNumeric(NUM,countInt,countFloat)))
    return true;
    return false;
}

//判断是否为Numeric型
function IsNumeric(NUM,countInt,countFloat)
{
	var i,j,strTemp,tempDotArray,dotCount;
	strTemp="0123456789.";
	dotCount = 0;
	if ( NUM.length== 0)
		return false;
	for (i=0;i<NUM.length;i++) {
		j=strTemp.indexOf(NUM.charAt(i));
		if (j==-1) {
			//说明有字符不是数字
			return false;
		}
		if(NUM.charAt(i)=='.'){
		    dotCount = dotCount + 1;
		}
		
	}
	tempDotArray = NUM.split('.');
	if(tempDotArray.length>1){
	    if(NUM.length>(countInt+countFloat+1))
	    {
	        return false;
	    }
	    if(tempDotArray[1].toString().length>countFloat){
	        return false;
	    }
	    if(tempDotArray[0].toString().length>countInt){
	        return false;
	    }
	}
	else
	{   
	    if(NUM.length>countInt)
	    {
	        return false;
	    }
	}
	if(dotCount>1){
	    return false;
	}
	//说明是正确的Numeric型
	return true;
}


//判断是否为数字
function fucCheckNUM(NUM)
{
	var i,j,strTemp;
	strTemp="0123456789";
	if ( NUM.length== 0)
		return 0;
	for (i=0;i<NUM.length;i++) {
		j=strTemp.indexOf(NUM.charAt(i));
		if (j==-1) {
			//说明有字符不是数字
			return 0;
		}
	}
	//说明是数字
	return 1;
}

//判断是否为Numeric型(可带负号)
function IsNumericFH(NUM,countInt,countFloat)
{
	var i = NUM.toString();
	var i1 = i.substring(0,1);
	if(i1 == "-")
	{
	    var i2 = i.substring(1,i.length);
	    return IsNumberOrNumeric(i2,countInt,countFloat);
	}
	return IsNumberOrNumeric(NUM,countInt,countFloat)
}

//判断是否为日期型开始
function isDate (theStr)
{
	var the1st = theStr.indexOf('-');
	var the2nd = theStr.lastIndexOf('-');

	if (the1st == the2nd) {
		return(false);
	} else {
		var y = theStr.substring(0,the1st);
		var m = theStr.substring(the1st+1,the2nd);
		var d = theStr.substring(the2nd+1,theStr.length);
		var maxDays = 31;

		if (fucCheckNUM(m)==false || fucCheckNUM(d)==false || fucCheckNUM(y)==false) {
			return(false);
		} else if (y.length < 4) {
			return(false);
		} else if ((m<1) || (m>12)) {
			return(false);
		} else if (m==4 || m==6 || m==9 || m==11)
			maxDays = 30;
		else if (m==2) {
			if (y % 4 > 0)
				maxDays = 28;
			else if (y % 100 == 0 && y % 400 > 0)
				maxDays = 28;
			else
				maxDays = 29;
		}
		if  ((d<1) || (d>maxDays)) {
			return(false);
		} else {
			return(true);
		}
	}
}

//是否为英文字符
function  IsLetterStr(strIn) 
{ 
	return strIn.match(/^[A-Za-z]+$/); 

}

//是否为英文或者数字字符
function  IsValidStr(strIn) 
{ 
	return strIn.match(/^[A-Za-z0-9_]+$/); 

} 
//是否为中文字符
function IsValidChinese(strIn) 
{ 
	return strIn.match(/^[\u4e00-\u9fa5]+$/); 
}

//是否为英文字符+点号+逗号+横杠+括号+空格
function  IsValidSpecialStr(strIn) 
{ 
	return strIn.match(/^[0-9a-zA-Z]+$/); 

} 
//是否为电子邮件
function IsEmail(strIn) 
{ 
	return strIn.match(/^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/); 
}

//是否网址
function IsUrl(strIn)
{
    return strIn.match(/^http:\/\/[A-Za-z0-9]+\.[A-Za-z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/);
}

//验证中文
function isChinese(str)
{
   var lst = /[u00-uFF]/;       
   return !lst.test(str);      
}
//验证中文、英文、.
function isName(Name)
{
    var re = /^[\u4e00-\u9fa5a-zA-Z\.\·]*$/g;
    return re.test(Name);
}


//验证中文、英文、中英文混合字符串长度
function strlen(str) 
{

   var strlength=0;
   for (i=0;i<str.length;i++)
  {
     if (isChinese(str.charAt(i))==true)
        strlength=strlength + 2;
     else
        strlength=strlength + 1;
  }
    return strlength;
}


//中文+英文+点号+逗号+横杠+括号+空格
function ValidText(strIn)
{
    if(IsValidChinese(strIn)==null && IsValidSpecialStr(strIn)==null)
    {
        for(var i=0;i<strIn.length;i++)
        {
            if(IsValidChinese(strIn.charAt(i))==null && IsValidSpecialStr(strIn.charAt(i))==null)
            {
                return false;
            }
        }
    }
    return true;
}
//百分比验证
function ValidPercent(strIn){  
    var reg = /^\d+(\.\d+)?%$/; 
    return reg.test(strIn);
} 

//显示隐藏栏目
function oprItem(itemId,clickId)
{
	if(document.getElementById(itemId).style.display== 'none')
	{
		document.getElementById(itemId).style.display="";
		document.getElementById(clickId).innerHTML = '<img src="../../../images/Main/Close.jpg" onmouseover=document.getElementById("' + clickId +'").style.cursor = "pointer" onclick=oprItem("'+ itemId +'","'+ clickId +'")></img>';
	}
	else
	{
		document.getElementById(itemId).style.display= 'none';
	    document.getElementById(clickId).innerHTML = '<img src="../../../images/Main/Open.jpg" style="CURSOR: pointer" onmouseover=document.getElementById("' + clickId +'").style.cursor = "pointer" onclick=oprItem("'+ itemId +'","'+ clickId +'")></img>';
	}
}
//显示隐藏栏目
function oprItem1(itemId,clickId,control1,control2)
{
	if(document.getElementById(itemId).style.display== 'none')
	{
		document.getElementById(itemId).style.display="";
		document.getElementById(clickId).innerHTML = '<img src="../../../images/Main/Close.jpg" onmouseover=document.getElementById("' + clickId +'").style.cursor = "pointer" onclick=oprItem1("'+ itemId +'","'+ clickId +'","'+control1+'","'+control2+'")></img>';
		
		$("#"+control1).show();
	    $("#"+control2).hide();
	}
	else
	{
		document.getElementById(itemId).style.display= 'none';
	    document.getElementById(clickId).innerHTML = '<img src="../../../images/Main/Open.jpg" style="CURSOR: pointer" onmouseover=document.getElementById("' + clickId +'").style.cursor = "pointer" onclick=oprItem1("'+ itemId +'","'+ clickId +'","'+control1+'","'+control2+'")></img>';
	    $("#"+control1).hide();
	    $("#"+control2).show();
	}
}
function isnumberorLetters(str) 
{
   var lst = /^[0-9a-zA-Z]+$/;       
   return !lst.test(str);      
}

 //去左空格;
function ltrim(s){
return s.replace( /^\s*/, "");
}
//去右空格;
function rtrim(s){
return s.replace( /\s*$/, "");
}
   //左右空格;
function trim(s){
return rtrim(ltrim(s));
} 
 /************************************
 *删除字符串空格
 *iType: 0 - 去除前后空格; 1 - 去前导空格; 2 - 去尾部空格
 ************************************/
function cTrim(sInputString,iType)  
{   
     var sTmpStr = ' ';
     var i = -1;
     if(iType == 0 || iType == 1) 
     { 
            while(sTmpStr == ' ')  
               {
                    ++i;  
                    sTmpStr = sInputString.substr(i,1);
              }  
         sInputString = sInputString.substring(i); 
     }  
     if(iType == 0 || iType == 2)  
     { 
        sTmpStr = ' ';  
        i = sInputString.length;
        while(sTmpStr == ' ') 
        {  
            --i ;
            sTmpStr = sInputString.substr(i,1); 
       }  
       sInputString = sInputString.substring(0,i+1);  
     }  
 return sInputString; 
 }

//验证开始日期不能大于结束日期，验证条件成立返回真，否则为假   
//任何一个输入如果为空，则返回为真   
//本比较函数不对填写日期的合法性进行校验 
function compareDate( checkStartDate,checkEndDate)   
{
    var arys= new Array();  
    var startdate=new Date(arys[0],parseInt(arys[1]-1),arys[2]);   
    if(checkStartDate != "" && checkEndDate != "") 
    {  
        arys = checkStartDate.split('-');  
        var startdate=new Date(arys[0],parseInt(arys[1]-1),arys[2]);   
        arys=checkEndDate.split('-');  
        var checkEndDate=new Date(arys[0],parseInt(arys[1]-1),arys[2]);   
        if(startdate > checkEndDate) 
        {  
          //alert("日期开始时间大于结束时间！");  
          return false;  
        }
        else
        {
         return true;
        }
    }
    else
    {
        return true;
    }
}



//屏蔽编码规则中输入的特殊字符 add by songfei
//arr[0]=/[\`\~\!\@\#\$\%\^\&\*\(\)\+\\\]\[\}\{\'\;\:\"\/\.\,\>\<\]\s\|\=\-\?]/g; 
var arr=new Array();   
arr[0]=/[\`\~\!\@\#\$\%\^\&\*\+\'\;\:\"\,\>\<\s\|\=\?]/g;    
arr[1]=/[^\d]/g;   
function filtecharacter(obj, index) {   
    obj.value = obj.value.replace(arr[index], "");   
} 
/***********************************************
 *编号验证：只允许输入英文字母、数字、下划线_、减号-、括号：（、）、[、]、{、}、斜线\、/、小数点.
 **********************************************/
function CodeCheck(CodeValue)
{
    //var re = /^[a-zA-Z0-9_\-\(\)\[\]\{\}\.\/\\]+$/g;
    var re = /^[a-zA-Z0-9_\-\(\)\[\]\{\}\.]+$/g;
    return re.test(CodeValue);
} 

/***********************************************
 *编号验证：只允许输入 数字、减号-、加号、乘号、除号括号：（、）、[、]、{、}、斜线\、/、小数点.
 **********************************************/
function CaculatorCheck(CodeValue)
{
    //var re = /^[a-zA-Z0-9_\-\(\)\[\]\{\}\.\/\\]+$/g;
   
    var re =  /^[0-9\-\+\@\/\*\(\)\[\]\{\}\.]+$/g;
    return re.test(CodeValue);
} 

/***********************************************
 *验证是否包含特殊字符 pdd
 **********************************************/
 function CheckSpecialWord(str)
 {
    var SpWord=new Array("'","\\","<",">","%","?","/","+");//可以继续添加特殊字符 此 /  字符也不可输入 输出时会破坏JSON格式
    for(var i=0;i<SpWord.length;i++)
    {
        for(var j=0;j<str.length;j++)
        {
            if(SpWord[i]==str.charAt(j))
            {
                return false;
                break;
            }
        }
    }
    return true;
}

/***********************************************
*验证是否包含特殊字符，允许包含问号
**********************************************/
function CheckSpecialWord2(str) {
    var SpWord = new Array("'", "\\", "<", ">", "%",  "/", "+"); //可以继续添加特殊字符 此 /  字符也不可输入 输出时会破坏JSON格式
    for (var i = 0; i < SpWord.length; i++) {
        for (var j = 0; j < str.length; j++) {
            if (SpWord[i] == str.charAt(j)) {
                return false;
                break;
            }
        }
    }
    return true;
}



/*验证规格只允许+和*特殊字符可以输入
这两个控件是函数写在控件上(ProductInfoControl.ascx,ProductInfoBatchControl.ascx)，因不确定所有调用这两个控件的地方是否调用Check.js文件
修改时需要注意
*/
function CheckSpecification(str) {
    var SpWord = new Array("'", "\\", "<", ">", "%", "?", "/"); //可以继续添加特殊字符 此 /  字符也不可输入 输出时会破坏JSON格式
    for (var i = 0; i < SpWord.length; i++) {
        for (var j = 0; j < str.length; j++) {
            if (SpWord[i] == str.charAt(j)) {
                return false;
                break;
            }
        }
    }
    return true;
}

  /*********************************************
 *验证数字（正浮点数与正整数） IsInt为true为验证正整数 pdd
 *********************************************/
   function IsNumOrFloat(s,IsInt)
   {
        var strTemp="0123456789.";
	    if ( s.length== 0)
		    return false;
	    for (i=0;i<s.length;i++) {
		    j=strTemp.indexOf(s.charAt(i));
		    if (j==-1) {
			return false;
		}}
        var resFloat=/^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$/;//正浮点正则
        var resNum= /^[0-9]*[1-9][0-9]*$/;//正整数正则 
        if(resNum.test(s))
           return true;
        else
            if(IsInt)
                return false;
        if(resFloat.test(s))
            return true;
       return false;
       
   }

/*验证百分数*/
function checkPercent(str)
{
    if(str.charAt(str.length-1)!="%")
        return false;
    
    var tmp=str.replace("%","");
    if(!IsNumOrFloat(tmp,false))
        return false;

    return trim;
}

/*只含有汉字、数字、字母、下划线，下划线位置不限：*/
function CheckValue(ret)
{
  var res=/^(?!-)[a-zA-Z0-9-\u4e00-\u9fa5]+$/;
  if(res.test(ret))
  return true;
  else
  return false;

}

function CheckPhone(str)
{
    var p=/^0?\d{2,3}\-\d{7,8}$/;
    if(p.test(str))
        return true;
    return false;
}