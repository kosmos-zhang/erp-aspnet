<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Top.aspx.cs" Inherits="XBase.WebSite.Top" %>

<%@ Register src="UserControl/TopMenuCell.ascx" tagname="TopMenuCell" tagprefix="uc1" %>

<%@ Register src="UserControl/Message.ascx" tagname="Message" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link rel="stylesheet" type="text/css" href="css/default.css" />
    <script type="text/javascript" src="js/swfobject.js"></script>    

    <script src="js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
    var Zxl100Path='<%=Zxl100Path %>';

    function GetCustTel()
{
     
    var CustID = "";
    
    var CompanyCD = document.getElementById("hidCompanCD").value;
    var EmployeeID = document.getElementById("hidEmployeeID").value;
//    var Tel = document.getElementById("TelShow").CustTel;
//    var f = document.getElementById("TelShow").LinCount;

    //先判断响铃次数
    if(2==1)
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
       {}, 
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
                        var url2= sessionSection + "../Pages/Office/CustManager/CustTelShow.aspx?CustTel="+escape(Tel)+
                        "&CompanyCD="+escape(CompanyCD)+"&EmployeeID="+escape(EmployeeID)+"&CustID="+escape(CustID);
                        window.showModalDialog(url2, window, "dialogWidth=850px;dialogHeight=390px;scroll:no;");
                    }
                }//接收数据完毕hidePopup();
       });
    }
    
    //半秒循环执行一次GetCustTel
    setTimeout(GetCustTel,500);

}

    var initMenuStatus = 0;
    function switchMenu()
    {  
        if(initMenuStatus == 0)
        {
            var html = '展开<img src="images/'+Zxl100Path+'Main_left_close.jpg"   ALIGN=ABSBOTTOM  border="0" />';
            document.getElementById("link_menuSwitch").innerHTML = html;
            
            parent.document.getElementById("leftTD").style.width = "5px";
            parent.document.getElementById("Left").width = "0px";
            
            initMenuStatus = 1;
        }else{
             var html = '收起<img src="images/'+Zxl100Path+'Main_left_open.jpg"   ALIGN=ABSBOTTOM  border="0" />';
             document.getElementById("link_menuSwitch").innerHTML = html;
                          
            
            parent.document.getElementById("leftTD").style.width = "188px";
            parent.document.getElementById("Left").width = "188px";
            
            initMenuStatus=0;
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
    </script>
</head>
<body onload="GetCustTel()">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <form id="form1" runat="server">
  <tr>
    <td width="230" align="center" valign="top" background="images/<%=Zxl100Path %>Main_bar_bg.jpg">
        
        <table border="0" cellpadding="0" cellspacing="0" align="left" valign="top">
            <tr>
                <td height="54" valign="top" background="images/<%=Zxl100Path %>Main_top_bg.jpg"><img src="images/<%=Zxl100Path %>LOGO.jpg" runat="server" id="img_logo" width="215" height="45"/></td>
            </tr>
            <tr>
                <td align="left"  >
                
                 <table width="180" height="36"  border="0"  align="left" cellpadding="0" cellspacing="0">
                  <tr>
                    <td style="padding-left:5px;color:#ffffff;"  ><span id="spanCurrentType" runat="server">当前模式：</span> 
                    <span  id="workModelInfo"    runat="server" ></span></td>
                    <td align="right">
                    <a style="display:inherit;ling-height:18pt;color:#ffffff;" title="点击收起或展开左侧" id="link_menuSwitch" href="javascript:switchMenu();">
                    收起<img src="images/<%=Zxl100Path %>Main_left_open.jpg"    ALIGN=ABSBOTTOM  border="0" /></a>
                    </td>
                  </tr>
                </table>
                </td>
            </tr>
        </table>
    </td>
    <td background="images/<%=Zxl100Path %>Main_bar_bg.jpg" valign="top" cellspacing=0 cellpadding=0 align="center">
    
    <%--<object id="TelShow" codebase="Setup/CTS.ocx#version=1,0,0,0" classid="CLSID:A078DCAF-9D48-4756-895F-04903E5C0F99" width="0px" height="0px"></object>--%>
       <object id="TelShow" classid="clsid:A078DCAF-9D48-4756-895F-04903E5C0F99"
        width="0" height="0" style="display: none;"></object>
       
       <uc1:TopMenuCell ID="TopMenuCell1" Visible="false" FlashPath="Images/flash/top/01.swf" runat="server" />
        <uc1:TopMenuCell ID="TopMenuCell2" Visible="false" FlashPath="Images/flash/top/02.swf" runat="server" />
        <uc1:TopMenuCell ID="TopMenuCell3"  Visible="false" FlashPath="Images/flash/top/03.swf" runat="server" />
        <uc1:TopMenuCell ID="TopMenuCell4" Visible="false"  FlashPath="Images/flash/top/04.swf" runat="server" />
        <uc1:TopMenuCell ID="TopMenuCell5"  Visible="false" FlashPath="Images/flash/top/05.swf" runat="server" />
        <uc1:TopMenuCell ID="TopMenuCell6"  Visible="false" FlashPath="Images/flash/top/06.swf" runat="server" />
        <input id="hidCompanCD" type="hidden" runat="server" />
        <input id="hidEmployeeID" type="hidden" runat="server" />
        <uc2:Message ID="Message1" runat="server" />
      </td>
  </tr>    </form>
</table>


    
    
   
<script language="javascript" type="text/javascript">
    parent.checkIframeLoadedCnt();
</script>
</body>
</html>
