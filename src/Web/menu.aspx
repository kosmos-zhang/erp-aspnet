<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menu.aspx.cs" Inherits="system_pages_menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
<script src="js/JQuery/jquery_last.js" type="text/javascript"></script>
    <title>无标题页</title>
<link href="../../../css/default.css" rel="stylesheet" type="text/css" />
<STYLE type=text/css>
body{font:75%/150% arial;background-color:#FFFFFF; font-size:14px;}
#mycolumn{width:206px;
margin:0px auto;
    }
ul,li{list-style:none;}
a{text-decoration:none;}
.win.winB{background: left top no-repeat;}
.win.winB h3{padding-left:40px;padding-top:5px;margin:0;font-size:14px;line-height:15px;height:33px;
background:#DDDDDD right top no-repeat;color:#000000;
        width: 163px;
    }

.win.winB .box{margin:0;padding:0;border:1px solid #dddddd;border-top:1px solid #5babde;}
.win.winB ul{margin:0;padding:0;}
.win.winB ul li{margin:0;padding:0;}
.win.winB ul li p{margin:0;padding:0;display:block;padding:1px 0;background: left bottom no-repeat;}

.win.winB ul li span{display:block;height:30px;background: left top repeat-y;line-height:30px;}
.win.winB ul li span a{display:block;padding-left:32px;cursor:pointer;zoom:1; height:30px; text-align:left;}

.win.winB ul li ul li a:link{color:#666666;}
.win.winB ul li ul li a:visited{color:#666666;}
.win.winB ul li ul li a:hover{color:#ff6600;}
.win.winB ul li ul li a:active{color:#ff6600;}
li.menuoff ul{display:none;}
li.menuoff span a{background: 16px 10px no-repeat;font-weight:normal;}
li.menuon span a{font-weight:700;background: 16px 10px no-repeat;}
li.menuon ul{display:block;}
.win.winB ul li ul li.selected{background:#fffceb url(images/Menu/dot_3_red.gif) 22px center no-repeat;border-bottom:1px solid #ffc08d;color:#ff6600;}
li.menu2off span a{background: 16px 10px no-repeat;font-weight:normal;}
li.menu2on span a{font-weight:700;background: 16px 10px no-repeat;}
/*二级菜单*/
.win.winB ul li ul li p{margin:0;padding:0;display:block;padding:1px 0;background:none;}
.win.winB ul li ul li span{padding-left:10px;display:block;height:20px;background:none;line-height:20px;overflow:hidden; vertical-align:middle;}/*ie下无效*/

.win.winB ul li ul li ul { display:none}
.win.winB ul li ul li ul li{ list-style:none; }
.win.winB ul li ul .menu2off{padding-left:0px;line-height:31px;height:1%;}
.win.winB ul li ul .menu2on{padding-left:0px;line-height:31px;height:1%;}
.win.winB ul li ul li ul li{ list-style:none;list-style-position: outside}
/*二级菜单*/
#mqMenu li ul li{padding-left:42px;line-height:31px;height:1%;background:#DDDDDD url(images/Menu/dot_3.gif) 32px center no-repeat;}
#mqMenu li ul .menu2off{ background:#DDDDDD url(images/Menu/dot_3.gif) -22px center no-repeat; padding-left:0;}
#mqMenu li ul .menu2on{ background:#DDDDDD url(images/Menu/dot_3.gif) -22px center no-repeat; padding-left:0;}
#mqMenu li ul .menu2on ul ,#mqMenu li ul .menu2on ul li{ display:block;}
/*三级菜单*/
#mqMenu li ul li ul li{padding-left:84px; line-height:20px;height:1%;background:#DDDDDD url(images/Menu/dot_3.gif) 74px center no-repeat; border-bottom:none; overflow:hidden; font-size:12px;
}
    .style4
    {
        width: 118px; font-size:12px;
    }
    <style>

</style>
</STYLE>
<script language="javascript" src="js/common/Ajax.js"></script>
</head>
<body leftmargin="0" rightmargin="0" topmargin="0" style=" background-color:#DDDDDD">
<DIV id=mycolumn>
<DIV class="win winB">

<div>
    <table 
        style="width:100%;">
        <tr>
            <td align="center" class="style4"  >
                <asp:Label ID="ParentNameShow" runat="server" Text="Label"></asp:Label> </td>
            <td align="right" >
                </td>
        </tr>
    </table>
    </div>
     <!--<INPUT onclick=dv.scrollTop-=30 type=button value=up> <INPUT onclick=dv.scrollTop+=30 type=button value=Down>id="dv" style="height:590px;width:190px;overflow:hidden;"-->
<DIV class=box ><!--菜单开始，menuoff关闭菜单，menuon打开菜单，下面的代码表示同时只能展开一个菜单-->
<UL id=mqMenu>  
  <asp:Repeater ID="rpMenu" runat="server">
    <ItemTemplate>
      <LI class="menuoff" id="LI<%#DataBinder.Eval(Container.DataItem,"ModuleID")%>" >
        <P>
        <SPAN style="background-image: url('Images/Menu/navC_right.gif');text-align:center;">
            <a onclick="MenuList('<%#DataBinder.Eval(Container.DataItem,"ModuleID")%>');">
                <%#DataBinder.Eval(Container.DataItem, "ModuleName")%>
            </a>
        </SPAN>
        </P>
        <UL id="UL<%#DataBinder.Eval(Container.DataItem,"ModuleID")%>" ></UL>
      </LI>	
    </ItemTemplate>
  </asp:Repeater>
</UL>
</DIV>
</DIV>
</DIV>
</body>
<script>
function MenuList(ObjectID){

   var menuFlag = document.getElementById("LI"+ObjectID).className;
   if(menuFlag=="menuoff")
   {
      var url = "Handler/MenuList.ashx?ParentID="+ObjectID;
     // var requestResult = getmenu(url);
      //var requestResult = SendRequest("POST",url,null,false);
       $.ajax({ 
              type: "POST",
              datatype:'html',
              url: url,
              cache:false,
            success:function(data) 
            { 
                  document.getElementById("UL"+ObjectID).innerHTML=data;

            } 
         });
      document.getElementById("LI"+ObjectID).className = "menuon";
      //alert(requestResult);
   }else if(menuFlag=="menuon"){
      document.getElementById("LI"+ObjectID).className = "menuoff";
   }else if(menuFlag=="menu2off"){
      var url = "Handler/MenuList.ashx?ParentID="+ObjectID;
     // var requestResult = getmenu(url);
             $.ajax({ 
              type: "POST",
              datatype:'html',
              url: url,
              cache:false,
            success:function(data) 
            { 
              document.getElementById("UL"+ObjectID).innerHTML=data;

            } 
         });
      document.getElementById("LI"+ObjectID).className = "menu2on";
   }else if(menuFlag=="menu2on"){
      document.getElementById("LI"+ObjectID).className = "menu2off";
   }   
}
</script>
</html>
