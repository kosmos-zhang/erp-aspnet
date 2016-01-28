<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Left.aspx.cs" Inherits="Left" %>

<%@ Register src="UserControl/LeftMenu.ascx" tagname="LeftMenu" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link rel="stylesheet" type="text/css" href="css/default.css" />
    <script src="js/JQuery/jquery_last.js" type="text/javascript"></script>
    <style type="text/css">
       
        
        
        /*标准链接样式*/
        a:link {color: #000000;
	        text-decoration: none;
        }
        a:hover {color: #000000;
                 text-decoration: underline;
        }
        a:visited {color: #000000;
                 text-decoration: none;
        }

    </style>
<script language="javascript" type="text/javascript">
    
function GetCustTel()
{
    var Tel = "13865901030";
    var CompanyCD = "T0004";//document.getElementById("hidCompanCD").value;
    var EmployeeID = "173";//document.getElementById("hidEmployeeID").value;
    var CustID = "";

    //先判断响铃次数
    if(1==2)
    {       
         //再判断是否有客户信息
        $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:"Handler/Office/CustManager/CustTelShow.ashx",//目标地址
       data:'CustTel='+escape(Tel)+'&action=HaveCust'+'&CompanyCD='+escape(CompanyCD),
       cache:false,
       //beforeSend:function(){AddPop();},//发送数据之前           
       success: function(msg){
                //数据获取完毕，填充页面据显示
                $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                        CustID = item.ID;
               });
              },
       error: function() 
       {
       }, 
       complete:function()
                {
                    //若有对应客户信息再弹出
                    if(CustID != "")
                    {
                        var sessionSection = "";
                        var url = document.location.href.toLowerCase();                        
                        if (url.indexOf("/(s(") != -1) {
                            var sidx = url.indexOf("/(s(") + 1;
                            var eidx = url.indexOf("))") + 2;
                            url = document.location.href;
                            sessionSection = url.substring(sidx, eidx);
                            sessionSection += "/";
                        }                        
                        var url2 = sessionSection + "../Pages/Office/CustManager/CustTelShow.aspx?CustTel="+escape(Tel)+"&SessionID="+escape(sessionSection)+
                        "&CompanyCD="+escape(CompanyCD)+"&EmployeeID="+escape(EmployeeID)+"&CustID="+escape(CustID);
                        //window.showModalDialog(url, window, "dialogWidth=850px;dialogHeight=390px;scroll:no;");
                        window.showModalDialog(url2, window, "dialogWidth=980px;dialogHeight=500px;scroll:no;");
                    }
                }//接收数据完毕hidePopup();
       });    
    }
}

    //ff && ie Event start here
function SearchEvent()
{    
    if(document.all)
        return event;

    func=SearchEvent.caller;
    while(func!=null)
    {
        var arg0=func.arguments[0];             
        if(arg0)
        {
            if(arg0.constructor==MouseEvent) // 如果就是event 对象
                return arg0;
            if(arg0.constructor==KeyboardEvent) // 如果就是event 对象
                return arg0;
            if(arg0.constructor==Event) // 如果就是event 对象
                return arg0;
        }
        func=func.caller;
    }
    return null;
}
/**/
   document.onkeydown = function(e){  
        var IsFresh=false;
        if(document.all)
        {
            if(event.keyCode == 116)
            {
              IsFresh=true;
            }
        }
        
        var evt = SearchEvent();
        if(evt.charCode == 116)
        {        
           IsFresh=true;
        }
        
       window.parent.document.getElementById("txtIsFresh").value=IsFresh;
   };
   
   
   
          
    function scrollMenuUp(val)
    {                   
        document.getElementById("menuPanel").scrollTop -= 60;  
       
    }
    
    function scrollMenuDown(val)
    {                   
        document.getElementById("menuPanel").scrollTop += 60;  
       
    }
    
    
    var timer1=null;
    function startScrollUp()
    {
        timer1 = setInterval(scrollMenuUp,300);
        
    }
    function startScrollDown()
    {
        timer1 = setInterval(scrollMenuDown,300);
        
    }
    
    
    function stopScroll()
    {
        if(timer1 != null)
        {
            clearInterval(timer1);
        }
    }
    
    
     var expandedItems = new Array();
                
		function expandSubMenu(event,url)
		{
		    
		
		    var ele = null;
		    if(document.all)
		    {
		        ele = event.srcElement
		    }else{
		        ele = arguments[0].target;
		    }
		    
		    if(ele.tagName == "IMG")
		        ele = ele.parentElement||ele.parentNode;
		    if(ele.tagName == "A")
		    {
		        ele = ele.parentElement||ele.parentNode; 
		        //ele = ele.parentElement||ele.parentNode; 
		    }
		    
		    var nodeLevel = ele.getAttribute("level");
		    var ele2 = (ele.parentElement||ele.parentNode);
		    ele2 = ele2.nextSibling.firstChild;
		    //ele2 is current TD element,当前子菜单的TD容器;
		    
		    var imgOpenClose = null;
		    if(nodeLevel ==  "0")
		    {
		        imgOpenClose = ele.childNodes[ele.childNodes.length-1];
		    }else if(nodeLevel == "1")
		    {
		        imgOpenClose = ele.firstChild;
		    }
		    
		    ele2.imgObj = imgOpenClose;
		    ele2.level = nodeLevel;
		    
		    if(ele2.style.display != "none")
		    {		    
		        if(imgOpenClose != null)
		        {    
		            if(nodeLevel == 0)
		                imgOpenClose.src = "images/left_frame/Arrow_open.jpg";
		            else
		                imgOpenClose.src = "images/left_frame/Main_left_file.jpg";
		        }
		        ele2.style.display = "none";
		        
		        
		    }else{
		        if(typeof url != "undefined")
	            {
	                if( url +"" != "")
	                {
	                    //if( url != parent.document.getElementById("Main").src)
	                    {
	                        parent.document.getElementById("Main").src = url;
	                    }
	                }
	            }
		    
		        if(imgOpenClose != null)
		        {
		            if(nodeLevel == 0)
		                imgOpenClose.src = "images/left_frame/Arrow_close.jpg";
		            else
		                imgOpenClose.src = "images/left_frame/Main_left_file_open.jpg";
		        }
		        ele2.style.display = "";
		        
		        if(ele2 == expandedItems[parseInt(nodeLevel)])
		            return;
		        
		        var tEle = expandedItems[parseInt(nodeLevel)];
		        if(tEle != null)
		        {		            
		            tEle.style.display = "none";
		            if(tEle.level == "0")
		            {
		                tEle.imgObj.src = "images/left_frame/Arrow_open.jpg";
		            }else if(tEle.level == "1"){
		                tEle.imgObj.src = "images/left_frame/Main_left_file.jpg";
		            }
		            
		        }		        
		        
		        //ele2.imgOpenClose = imgOpenClose;
		        expandedItems[parseInt(nodeLevel)] = ele2; 
		        
		    }
		}
		
		
		
		
		
		
		var lastColor = null;
		function highLight(e,flag)
		{			  
		    var ele = null;
		    if(document.all)
		    {
		        ele = event.srcElement;
		    }else{
		        ele = e.target;;
		    }
		    
		    if(ele.tagName == "IMG")
		        ele = ele.parentElement||ele.parentNode;
		        
		   if(flag == 0)
		   {
		        lastColor = ele.style.color;
		        ele.style.color = "#ff0000";
		   }else{
		        ele.style.color = lastColor;
		   }
		   
		}
</script>
</head>
<body  onload="GetCustTel()">
    
<table id="leftMenu" width="180"  border="0" cellpadding="0" cellspacing="0">
  <form id="form1" runat="server">
<tr>
    <td width="180" valign="top" bgcolor="#DDDDDD" ><img onmouseover="startScrollUp()" onmouseout="stopScroll()" style="cursor:pointer;"   src="images/<%=Zxl100Path %>Main_left_up.jpg" width="180" height="17" /></td>
    <td width="180" rowspan="3" valign="top" background="images/Main_left_rightbg.jpg" >&nbsp;</td>
  </tr>
    
  <tr>
    <td valign="top" id="menuTd" bgcolor="#DDDDDD">
        <div id="menuPanel" style="width:180px;overflow:hidden;margin:0;">

            
            <uc1:LeftMenu ID="LeftMenu1" runat="server" />

      
     </div></td>
  </tr>
  
  
  <tr>
    <td valign="top" bgcolor="#DDDDDD"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td ><img src="images/<%=Zxl100Path %>Main_left_down.jpg" onmouseover="startScrollDown()" onmouseout="stopScroll()" style="cursor:pointer;"   width="180" height="17" /></td>
          <td>
              <input id="hidSessionID" type="hidden" runat="server" />
          </td>
      </tr>
    </table></td>
  </tr> </form>
</table>

<script language="javascript" type="text/javascript">
       
		
		
    var holeHeight = parent.document.documentElement.clientHeight;
    var  ModelNameDefault='<%=ModelName %>'

    //34 = 17*2,上下滚动的图片按钮
    document.getElementById("menuPanel").style.height = (holeHeight - 121-34)+"px" ;
    
   // alert(document.getElementById("menuPanel").style.height);
  
    var idx = document.location.href.lastIndexOf("?");
    var moduleId = "?moduleid=1";
    if(idx > 0)
    {
        moduleId = document.location.href.substring(idx);      
    };


    moduleId = parseInt(moduleId.substring(moduleId.length - 1));
    var workModelInfoEle = parent.Top.document.getElementById("workModelInfo");   
   
    var modelArr=ModelNameDefault.split('|');
    
    
    if(workModelInfoEle != null)
    {
        switch (moduleId)
        {
            case 1:
                
                workModelInfoEle.innerHTML = modelArr[0];
                break;
            case 2:
                
                 workModelInfoEle.innerHTML = modelArr[1];
                break;
            case 3:
                
                workModelInfoEle.innerHTML = modelArr[2];
                break;
            case 4:
                
                workModelInfoEle.innerHTML =modelArr[3];
                break;
            case 5:
                
                workModelInfoEle.innerHTML = modelArr[4];
                break;
            case 6:
                
                workModelInfoEle.innerHTML = modelArr[5];
                break;
            default:
                break;
        }
    }


    if(typeof defaultPage != "undefined")
    {
        if(defaultPage + "" != "")
        {
            if(document.location.href.toLowerCase().indexOf("nofreshdefault") == -1)
            {
                
                parent.document.getElementById("Main").src = defaultPage;
            }
        }
    }
    
    
    parent.checkIframeLoadedCnt();
    
    
    
//    
//    var imgs = document.images;
//    
//    var imgurls = "";
//    for(var i=0;i<imgs.length;i++)
//    {
//        var url = imgs[i].src;
//        url = url.substring(url.lastIndexOf("/")+1);
//        
//        if(url.indexOf("left") != -1)
//        {
//            continue;
//        }
//         if(url.indexOf("_open") != -1)
//        {
//            continue;
//        }
//        
//        if(url.indexOf("_") != -1)
//        {
//            imgurls += "\npreload_images.push(\""+url+"\");";
//        }
//    }
//    
//    alert(imgurls);
    
</script>
  
   
</body>
</html>
