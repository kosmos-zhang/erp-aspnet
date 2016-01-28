<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Main" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>进销存系统</title>

    <script src="js/common/Common.js" type="text/javascript"></script>
    <script src="js/JQuery/jquery_last.js" type="text/javascript"></script>
        <script src="js/JQuery/jquery.messager.js" type="text/javascript"></script>
    <script src="js/common/Check.js" type="text/javascript"></script>
    <script src="js/ChangePsd.js" type="text/javascript"></script>
    <script src="js/SystemAlert.js" type="text/javascript"></script>
    <script src="js/Main.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="css/default.css" />

   
    <script language="javascript" type="text/javascript">

      var  CiframLoadCount =0; 
  
        //$(document).ready(function(){
         function hideLoading()
         {  
            document.getElementById("loadingPanel").style.display = "none";
            
            var ids = "Top,Left,Main,footBar";
            var idlist = ids.split(',');
            for(var i=0;i<idlist.length;i++)
            {
                document.getElementById(idlist[i]).style.display = "";
            }
          }  
       // });
        
        var iframeCnt = 2;
        
        function checkIframeLoadedCnt()
        {
        
            iframeCnt--;
            
            if(iframeCnt==0)
            {
                hideLoading();
            }
        }
    </script>
    
    <style type="text/css">
        #noticePanel
        {
        	left:400px;
        	top:150px;
        	width:400px;
        	
        	border:solid 2px #999999;
        	background-color:#ffffff;
        	color:#000000;
        	
        	}
    
        
    
        .style1
        {
            width: 69px;
        }
        
        
      
    
        .style2
        {
            height: 18px;
        }
        
        
      
    
    </style>
</head>
<body>







<input type="hidden"   id="txtIsFresh"/>
    <div id="loadingPanel" style="display:block;z-index:10;margin:0 auto;margin-top:200px;text-align:center;width:60%;background:#f1f1f1;color:Blue;">
        <table align="center">
            <tr><td>
                正在加载中...
            </td><td>
                <img src="Images/<%=Zxl100Path %>/clock.gif" />
            </td></tr>
        </table>
        
    </div>
    
    
       <form id="form1" runat="server">
    <table width="100%" height="100%" border="0" cellspacing="0" cellpadding="0">
  
  <tr>
    <td  colspan="2" valign="top" height="90">
        <iframe src="Top.aspx" name="Top"  width="100%" style="display:none;" height="100%" scrolling="No" frameborder="0" id="Top"></iframe>
    </td>
    
  </tr>
  <tr>
    <td     width="184" id="leftTD" valign="top"> <iframe src="Left.aspx"  style="display:none;" name="Left" width="184"  scrolling="No" frameborder="0" id="Left"></iframe></td>
    
    <td id="mainTd"  valign="top" bgcolor="#666666" width="100%">
    <iframe src="DeskTop.aspx" name="Main" onload="adjustHeight(this);" style="display:none;" width="100%"    scrolling="yes" frameborder="0" id="Main"></iframe>
    </td>
  </tr>
  <tr id="footBar" style="display:none;">
    <td height="31" colspan="2" valign="bottom" background="images/Main_bottom_bg.jpg"><table  width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="20">&nbsp;</td>
        <td width="220">
        
        <img src="images/Button/Bottom_btn_backindex.jpg" style="cursor:pointer;"  onclick="javascript:document.getElementById('Main').src='DeskTop.aspx';" />
        
        <a href="LogOut.aspx"><img border="0" src="images/Button/Bottom_btn_Ckout.jpg" width="51" height="24" /></a>
        
        <font    ></font>
     <%--   <img src="images/Button/Bottom_btn_book.jpg" width="51" height="22" />&nbsp;
        <img src="images/Button/Bottom_btn_fast.jpg" width="72" height="22" />&nbsp;
        <img src="images/Button/Bottom_btn_box.jpg" width="50" height="22" />&nbsp;&nbsp;--%>
        <img src="images/Main_bottom_line.jpg" width="9" height="22" /></td>
        <td align="left">
           当前用户： <asp:Label ID="lblUserInfo" runat="server" Text="Label"></asp:Label>
        </td>
     <td align="right"    ><img src="images/light_un.gif"  style="cursor:pointer ; vertical-align:middle;  float: right   ;border :0; padding-right:20px" id="checkTask"   onclick="fnEmty()"    title ="暂无待办事项！"  />&nbsp;
        <a href="Pages/Office/CustManager/LinkMan_Info.aspx?ModuleID=2021202" target="Main"><img 

src="images/button/btn_czkhlxr.jpg" id="btn_czkhlxr" runat="server"  border="0" style="vertical-align:middle;padding-top:2px " 

/></a>
        &nbsp;<img src="images/Button/Bottom_btn_pwd.jpg"  onclick="ShowPsd();"  style="vertical-align:middle;  
padding-top:2px;border:0"  border="0" />&nbsp;
        </td>
      </tr>
    </table></td>
    
  </tr>
</table>
 <div id="divBackShadow" style="display: none">
    <iframe  id="BackShadowIframe" frameborder="0"
        width="100%"></iframe>
</div>
<div id="ChangePsd" style="border: solid 2px #898989; background: #fff;  padding: 10px; width: 400px; z-index: 100; position: absolute;top: 60%; left: 68%; margin: -200px 0 0 -400px; display:none">
               <table width="99%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#999999" style=" margin-left:6px">
                    <tr>
                        <td height="20" bgcolor="#F4F0ED" class="Blue"   >
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                         修改密码<input id="hfcommanycd" type="hidden"  runat="server"/></td>
                                    <td align="right">
                                        <div id='searchClick'>
                                            </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                
               <table width="99%" border="0" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_01" style="margin-left:6px">
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6" colspan="2">
                     <table style="width:100%"  align="center" border="0" >
                        <tr>
                             <td height="28" bgcolor="#FFFFFF" align="left">
                          <img alt="" src="Images/Button/Bottom_btn_save.jpg" 
                            onclick="EditPwd();"/>
                                 <img alt="关闭" src="Images/Button/Bottom_btn_back.jpg" 
                                     onclick="ClearText();"/><input id="hf_psd" type="hidden" runat="server"/></td>
                      </tr>
                   </table>
                        </td>
                        </tr>
                    <tr>
                        <td align="right" bgcolor="#E6E6E6">
                            用户名<span class="redbold">*</span></td>
                        <td bgcolor="#FFFFFF">
                            <asp:TextBox ID="txt_User" runat="server" CssClass="tdinput" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            原密码<span class="redbold">*</span></td>
                        <td height="20" bgcolor="#FFFFFF" >
                            <asp:TextBox ID="txtOldPassword" runat="server" CssClass="tdinput" 
                                TextMode="Password" onblur="OnlyPsd();"></asp:TextBox>
                        </td>
                        </tr>
                    <tr>
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            新密码<span class="redbold">*</span></td>
                        <td height="20" bgcolor="#FFFFFF" >
                            <asp:TextBox ID="txtNewPassword" runat="server" CssClass="tdinput" 
                                TextMode="Password"></asp:TextBox>
                        </td>
                        </tr>
                    <tr id="CloseDate">
                        <td height="20" align="right" bgcolor="#E6E6E6">
                            确认新密码<span class="redbold">*</span></td>
                        <td height="20" bgcolor="#FFFFFF" >
                            <asp:TextBox ID="txtRePassword" runat="server" CssClass="tdinput" 
                                TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                
    </div>
    <input name="inpMessageTipTimerSpan" type="hidden" id="inpMessageTipTimerSpan" value="300" runat="server" />
</form>

<script language="javascript" type="text/javascript">
 var Cookie={
              set:function(name,value,min)   
              {         
                  var   exp     =   new   Date();       
                  exp.setTime(exp.getTime()   +   min*60*1000);   
                  document.cookie   =   name   +   "="+   escape(value)   +";expires="+   exp.toGMTString()+";path=/"; 
                        
              },   
              
              get:function(name)   
              {      

                  var exp = "(^|[\\s]+)"+name+"=([^;]*)(;|$)";      
                  var   arr   =   document.cookie.match(new RegExp(exp)); 
                  
                  if(arr   !=   null)
                     return   unescape(arr[2]); 
                  return   null;   
              },
                 
              del:function(name)   
              {         
                  var   exp   =   new   Date();   
                  exp.setTime(exp.getTime()   -   1);  
                
                  var   cval=this.get(name); 
                    
                  if(cval!=null)
                     document.cookie=name   +"="+cval+";expires="+exp.toGMTString();   
              }
            }
            
            
            
            
    var holeHeight = document.documentElement.clientHeight;
   
    document.getElementById("Left").height = holeHeight - 121;
    document.getElementById("Main").height = holeHeight - 121;
     
     
     
     function adjustHeight(obj)
     {
        var win=obj;
        if (document.getElementById)
        {
            if (win && !window.opera)
            {
               // win.document.body.height = win.height-10;
            }
        }
       if( CiframLoadCount == 1 ){
            try{
              MSG.hide();
            }catch(ee){
            
            }
       }else if(CiframLoadCount == 2 ){
           
       }else if(CiframLoadCount == 0){
           CiframLoadCount++;
       }
     }
    function ShowPsd()
    {
    openRotoscopingDiv(false,'divBackShadow','BackShadowIframe');
     document.getElementById("ChangePsd").style.display = 'block';
    }
    
    //))/Images/Left_Frame/Fuction_01.jpg

    var preload_images = [];
    preload_images.push("Fuction_01.jpg");
    preload_images.push("Fuction_02.jpg");
    preload_images.push("Fuction_03.jpg");
    preload_images.push("Fuction_04.jpg");
    preload_images.push("Fuction_05.jpg");
    preload_images.push("Fuction_06.jpg");
    preload_images.push("Fuction_07.jpg");
    preload_images.push("Fuction_11.jpg");
    preload_images.push("Fuction_10.jpg");
    preload_images.push("Fuction_08.jpg");
    preload_images.push("Fuction_12.jpg");
    preload_images.push("Fuction_13.jpg");
    preload_images.push("Fuction_09.jpg");

    preload_images.push("Report_09.jpg");
    preload_images.push("Report_01.jpg");
    preload_images.push("Report_02.jpg");
    preload_images.push("Report_03.jpg");
    preload_images.push("Report_04.jpg");
    preload_images.push("Report_05.jpg");
    preload_images.push("Report_06.jpg");
    preload_images.push("Report_07.jpg");
    preload_images.push("Report_08.jpg");
    preload_images.push("Report_10.jpg");
    preload_images.push("Report_11.jpg");
    preload_images.push("Report_12.jpg");
    
    for(var i=0;i<preload_images.length;i++)
    {
        var imgObj = new Image();
        imgObj.src = "/Images/Left_Frame/"+preload_images[i];
    }
    
    



</script>


<script language="javascript" type="text/javascript">
    var s_UserSessionMinLife = UserSessionMinLife;

    function userLife()
    {
        $.ajax({ 
            type: "POST",
            url: "Handler/UserLifeHandler.ashx",
            dataType:'string',//返回json格式数据
            data: '',
            cache:false,
            success:function(data) 
            {
//                var rjson;
//                eval("rjson="+data);
//                
//                if(rjson.result)
//                {
//                    if(rjson.data.length > 0)
//                    {
//                        doNotice(rjson.data[0]);
//                    }
//                }
                    
                setTimeout(userLife,1000*s_UserSessionMinLife);
                
            } ,
            error:function(r){
                //alert(r.responseText);
                
            }
        });
    
    }    
    
   setTimeout(userLife,1000*5);
   
   
   var showedNoticeIDList;
   function doNotice(notice)
   {
      showedNoticeIDList = Cookie.get("showedNoticeIDList");  
      if(showedNoticeIDList == null)
      {
        showedNoticeIDList = ",";
      }
      
        if(showedNoticeIDList.indexOf(","+notice.ID+",") != -1)
            return;
            
       
       //document.getElementById("panelContent").innerHTML = notice.Title+"<br>"+notice.Content+"<br>"+notice.PubDate;
       document.getElementById("ntitle").innerHTML = notice.Title;
       document.getElementById("ncontent").innerHTML = notice.Content;
       document.getElementById("ndate").innerHTML = notice.PubDate;
       
       document.getElementById("noticePanel").style.display = "";
       
            
       showedNoticeIDList += notice.ID+",";      
       Cookie.set("showedNoticeIDList",showedNoticeIDList,60*24*7);  
        
   }
   function hidePanel()
   {
        document.getElementById("noticePanel").style.display = "none";
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

function GetEventSource(evt)
{        
    if(typeof evt == "undefined" || evt == null)
    { 
        evt = SearchEvent();        
        if(typeof evt == "undefined" || evt == null)
        { 
            return null;
        }
    }
           
    return evt.target||evt.srcElement;
}

function GetEventCode(evt)
{
    if(typeof evt == "undefined" || evt == null)
    { 
        evt = SearchEvent();        
        if(typeof evt == "undefined" || evt == null)
        { 
            return null;
        }
    }
    
    return evt.keyCode || evt.charCode;
}


/*保留F5建 删除此方法 2009/9/24 pdd*/
//   document.onkeydown = function(e){  
//        
//        if(document.all)
//        {
//            if(event.keyCode == 116)
//            {
//                event.keyCode = 0;
//                return false;
//            }
//            
//            return true;
//        }
//        
//        var evt = SearchEvent();
//        if(evt.charCode == 116)
//        {        
//            evt.charCode = 0;            
//            return false;
//        }
//        return true;
//   };

    document.oncontextmenu = function(){
        return false;
    };

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

     document.getElementById("txtIsFresh").value=IsFresh;
    // alert(document.getElementById("txtIsFresh").value);
   };


   window.onbeforeunload =function(){
            var IsFresh=document.getElementById("txtIsFresh").value;
       // var sel = confirm("确认退出吗？");
            var urlPara="Handler/UserLifeHandler.ashx?act=remove&IsFresh="+IsFresh.toString();
        //    alert(urlPara);
            //将F5事件重置
            document.getElementById("txtIsFresh").value="false";
       // if(sel)
       // {
            $.ajax({
             url:urlPara ,
             async: false   
            }); 
            
       // }else{
          //  return "";
       // }
        
        
   };
   
  
   
</script>
<div id="noticePanel" style="z-index:10;position:absolute;display:none; ">
    <div style="margin-bottom:10px;padding:5px;text-align:right;width:90%;">最新公告&nbsp;&nbsp;<a href="javascript:hidePanel()">关闭</a></div>
    <div id="panelContent">
        <table align="left" cellpadding="3" border="1" style="border-collapse:collapse;" width="100%">
            <tr><td class="style1" align="right">标题：</td><th align="left">
                <span id="ntitle"></span>
            </th></tr>
            <tr><td class="style1" align="right">发布时间：</td><td>
                <span id="ndate"></span>
            </td></tr>
            <tr><td class="style1" valign="top" align="right">内容：</td><td>
                <div id="ncontent" style="width:100%;height:300px; overflow:auto;"></div>
            </td></tr>
          
        </table>
    </div>
</div>    

</body>

<script type="text/javascript">
    window.onload = function() {
        GetAllDestTopList();
        var time = document.getElementById("inpMessageTipTimerSpan").value;
        setInterval(GetAllDestTopList, 1000 * parseInt(time));
    }
  
//  function MSGshow(){
//     try{
//         MSG.show();
//     }catch(eee){}
//  }
</script>
</html>
