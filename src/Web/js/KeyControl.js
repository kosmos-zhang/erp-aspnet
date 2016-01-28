<script language ="javascript" type="text/javascript" >
 <!-- 
  
  function JudgeIsNumTwo(obj)
	      {
	    
              var keycode = event.keyCode;
              
             if((keycode >8) && (keycode <48) ||(keycode >57) &&(keycode <96)|| (keycode >105) &&(keycode <110) || (keycode >110) &&(keycode <190) || (keycode >190))
              {
                  alert("请输入大于0的数");
                  obj.value = "";
                  return;
              }
             
	      }
  
  function Enter(obj)
  {  //不允许按回车
      var KeyCode = event.keyCode;
      if (KeyCode == 13 )
      {
        event.returnValue=false;
        //alert("请切换到英文状态下,输入整数!");
      }
  }
  


			



function window.onload()   //方向键控制
 {
     SetFocusToFirstControl();
 }
var curCtlIndex = 0;
var arrCtl = new Array();
var oldEvent = new Array();
function SetFocusToFirstControl()
{
    var i = 0,j = -1;
    for(i=0;i<document.forms[0].elements.length;i++)
    {
        if(document.forms[0].elements[i].tagName.toUpperCase() == 'INPUT' || document.forms[0].elements[i].tagName.toUpperCase() == 'SELECT' || document.forms[0].elements[i].tagName.toUpperCase() == 'TEXTAREA')
        {
            if(document.forms[0].elements[i].type.toUpperCase() != 'SUBMIT' && document.forms[0].elements[i].type.toUpperCase() != 'RESET' && document.forms[0].elements[i].type.toUpperCase() != 'HIDDEN' && document.forms[0].elements[i].type.toUpperCase() != 'BUTTON')
            {
                if(document.forms[0].elements[i].disabled == 'disabled' || document.forms[0].elements[i].disabled == true) 
                    continue;
                if(document.forms[0].elements[i].readOnly) 
                    continue;
                if(document.forms[0].elements[i].style.display =="none") 
                    continue;
                if(document.forms[0].elements[i].style.width == "0px")
                    continue;
                try
                {
                    j++;
                    arrCtl[j] = document.forms[0].elements[i];
                    arrCtl[j].blur();
                }
                catch(el)
                { }
            }
            document.forms[0].elements[i].onblur = onblur_handler;
            document.forms[0].elements[i].onfocus = onfocus_handler;
        }
        //alert(document.forms[0].elements[i].type + document.forms[0].elements[i].tagName);
    }
    //alert(arrCtl.length);
    for(i=0;i<arrCtl.length;i++)
    {
        try
        {
            arrCtl[i].focus();
            break;
        }
        catch(el)
        {}
    }
}
/**//*

*/
function document.onkeydown()
{
    var i = 0;
    if(event.keyCode == 13  || event.keyCode == 39 )
    {
        for(i=curCtlIndex+1;i<arrCtl.length;i++)
        {
            if(curCtlIndex < arrCtl.length - 1)
            {
                try
                {
                    curCtlIndex++;
                    arrCtl[curCtlIndex].focus();
                    return false;
                    //break;
                }
                catch(el)
                {}
            }
            else
            {
                //break;
            }
        }   
//        if(objSubmit != undefined && objSubmit != '' )
//            document.getElementById(objSubmit).click();
        return false;
    }
    else if(event.keyCode == 37)    // || event.keyCode == 38
    {
        for(i=curCtlIndex-1;i>=0;i--)
        {
            try
            {
                curCtlIndex--;
                arrCtl[curCtlIndex].focus();
                break;
            }
            catch(el)
            {}
        }
        return false;
    }
}

function onblur_handler()
{
    this.style.backgroundColor="White";
}

function onfocus_handler()
{
    for(i=0;i<arrCtl.length;i++)
    {
        if(this.id == '')
        {
            if(this.name == arrCtl[i].name)
            {
                curCtlIndex = i;
                break;
            }
        }
        else
        {
            if(this.id == arrCtl[i].id)
            {
                curCtlIndex = i;
                break;
            }
        }
    }
    this.style.backgroundColor="#DEDFDE";
    return true;
}
//禁止输入字符，只能输入整型数字
	    function IsDigit()
        {
          return ((event.keyCode >= 48) && (event.keyCode <= 57));
        }
        //去除字符中的空格
        function TrimSpace(str)
        {
	        var strNo=str.length;
	        var i;
	        for(i=1;i<=strNo;i++)
	        {
		        str=str.replace(" ","");
	        }
	        return str;
        }   
 -->
</script> 
