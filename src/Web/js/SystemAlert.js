// JScript 文件
    <!--
    /**//*
    **    ==================================================================================================
    **    类名：CLASS_MSN_MESSAGE
    **    功能：提供类似MSN消息框
    **    示例：
    ---------------------------------------------------------------------------------------------------
    var MSG = new CLASS_MSN_MESSAGE("aa",300,200,"短消息提示：","您有1封消息","我回合肥了,有事联系我");
    MSG.show();
    ---------------------------------------------------------------------------------------------------
    **    作者：王玉贞
    **    邮件：azhen925456@hotmail.com
    **    日期：2009-01-18
    **    ==================================================================================================
    **/
    /**//*
    *    消息构造
    */ 
    function CLASS_MSN_MESSAGE(id,width,height,caption,title,message,target,action){
    this.id     = id;
    this.title  = title;
    this.caption= caption;
    this.message= message;
    this.target = target;
    this.action = action;
    this.width    = width?width:300;
    this.height = height?height:200;
    this.timeout= 6000;
    this.speed    = 10;
    this.step    = 1;
    this.right    = screen.width -1;
    this.bottom = screen.height;
    this.left    = this.right - this.width;
    this.top    = this.bottom - this.height;
    this.timer    = 0;
    this.pause    = false;
    this.close    = false;
    this.autoHide    = true;
    }
    /**//*
    *    隐藏消息方法
    */
    CLASS_MSN_MESSAGE.prototype.hide = function(){
    if(this.onunload()){
    var offset  = this.height>this.bottom-this.top?this.height:this.bottom-this.top;
    var me  = this;
    if(this.timer>0){
    window.clearInterval(me.timer);
    }
    var fun = function(){
    if(me.pause==false||me.close){
    var x  = me.left;
    var y  = 0;
    var width = me.width;
    var height = 0;
    if(me.offset>0){
    height = me.offset;
    }
    y  = me.bottom - height;
    if(y>=me.bottom){
    window.clearInterval(me.timer);
    if( typeof(me.Pop)+"" != "undefined" )
    { 
      me.Pop.hide();
    }else{
      return;
    }   
    } else {
    me.offset = me.offset - me.step;
    }
    me.Pop.show(x,y,width,height);
    }
    }
    this.timer = window.setInterval(fun,this.speed)
    }
    }
    /**//*
    *    消息卸载事件，可以重写
    */
    CLASS_MSN_MESSAGE.prototype.onunload = function() {
    return true;
    }
    /**//*
    *    消息命令事件，要实现自己的连接，请重写它
    *
    */
    CLASS_MSN_MESSAGE.prototype.oncommand = function(){
    this.close = true;
    this.hide();
    window.frames.Main.location='http://www.baidu.com';
    }
    /**//*
    *    消息显示方法
    */
    CLASS_MSN_MESSAGE.prototype.show = function(){
    var oPopup = window.createPopup(); //IE5.5+
    this.Pop = oPopup;
    var w = this.width;
    var h = this.height;
    var str = "<DIV style='WIDTH: " + w + "px; BORDER-BOTTOM: #455690 1px solid; POSITION: absolute; TOP: 0px; HEIGHT: " + h + "px; BACKGROUND-COLOR: #E7E7E7;border-style:double;border-color:#999999'>"
    "<TABLE  height=\"31\" border=0 cellPadding=0 cellSpacing=0 background=\"images/Main/messege_top.gif\" >"
    str += "<TR>"
    str += "<TD ><SPAN title=关闭 style='FONT-WEIGHT: bold; FONT-SIZE: 12px; CURSOR: hand; COLOR: red; MARGIN-RIGHT: 4px' id='btSysClose' ><img src='images/Main/messege_top.gif' usemap=\"#Map\"   style=\"border:0px;\" > </SPAN><map   name=\"Map\">"   
     str +=" <area style=\"outline:none;\"  shape=\"rect\"   coords=\"0,0,250,29\"   href=\"#\"   alt=\"关闭\">"    
     str +="</map> </TD>"
    str += "<TD style='PADDING-RIGHT: 2px; PADDING-TOP: 2px' vAlign=center align=right width=19>"
    str += "</TD>"
    str +="</TR>"
    str += "</TABLE>"
    str += "<TABLE  height=\"84\" border=0 cellPadding=0 cellSpacing=0 background=\"images/Main/messege_main.jpg\" >"
    str += "<TR>"
    str += "<TD style='FONT-SIZE: 12px;COLOR: #0f2c8c' width=30></TD>"
    str += "<TD style='PADDING-LEFT: 4px; FONT-SIZE: 14px; font-weight:bolder; PADDING-TOP: 4px' vAlign=center width='100%'></TD>"
    str += "</TR>"
    str += "<TR>"
    str += "<TD style='PADDING-RIGHT: 1px;PADDING-BOTTOM: 1px' colSpan=3 height=" + (h-28) + ">"
    str += "<DIV style='break-all;' >" + this.message 
    str += "</DIV>"
    str += "</TD>"
    str += "</TR>"
    str += "</TABLE>"
    str += "</DIV>"
    oPopup.document.body.innerHTML = str;
    this.offset  = 0;
    var me  = this;
    oPopup.document.body.onmouseover = function(){me.pause=true;}
    oPopup.document.body.onmouseout = function(){me.pause=false;}
    var fun = function(){
    var x  = me.left;
    var y  = 0;
    var width    = me.width;
    var height    = me.height;
    if(me.offset>me.height){
    height = me.height;
    } else {
    height = me.offset;
    }
    y  = me.bottom - me.offset;
    if(y<=me.top){
    me.timeout--;
    if(me.timeout==0){
    window.clearInterval(me.timer);
    if(me.autoHide){
    me.hide();
    }
    }
    } else {
    me.offset = me.offset + me.step;
    }
    me.Pop.show(x,y,width,height);
    }
    this.timer = window.setInterval(fun,this.speed)
    var btClose = oPopup.document.getElementById("btSysClose");
    btClose.onclick = function(){
    me.close = true;
    me.hide();
    }
    var btCommand = oPopup.document.getElementById("btCommand");
//    btCommand.onclick = function(){
//    me.oncommand();
 //   }
    var ommand = oPopup.document.getElementById("ommand");
//    ommand.onclick = function(){
//    //this.close = true;
//    me.hide();
//    window.open(ommand.href);
//    }
    }
    /**//*
    ** 设置速度方法
    **/
    CLASS_MSN_MESSAGE.prototype.speed = function(s){
    var t = 20;
    try {
    t = praseInt(s);
    } catch(e){}
    this.speed = t;
    }
    /**//*
    ** 设置步长方法
    **/
    CLASS_MSN_MESSAGE.prototype.step = function(s){
    var t = 1;
    try {
    t = praseInt(s);
    } catch(e){}
    this.step = t;
    }
    CLASS_MSN_MESSAGE.prototype.rect = function(left,right,top,bottom){
    try {
    this.left        = left    !=null?left:this.right-this.width;
    this.right        = right    !=null?right:this.left +this.width;
    this.bottom        = bottom!=null?(bottom>screen.height?screen.height:bottom):screen.height;
    this.top        = top    !=null?top:this.bottom - this.height;
    } catch(e){}
    }
    
    
    
//    var MSG1 = new CLASS_MSN_MESSAGE("aa",170,106,"","","您的收件箱中有<b>1</b>封未读邮件");
//    MSG1.rect(null,null,null,screen.height-50);
//    MSG1.speed    = 10;
//    MSG1.step    = 5;
    //alert(MSG1.top);
    
       
    //同时两个有闪烁，只能用层代替了，不过层不跨框架
    //var MSG2 = new CLASS_MSN_MESSAGE("aa",300,200,"短消息提示：","您有2封消息","好的啊");
    //   MSG2.rect(100,null,null,screen.height);
    //    MSG2.show();
    //-->
