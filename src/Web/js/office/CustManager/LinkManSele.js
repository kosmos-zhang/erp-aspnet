var linkmanHtml ="";  
var bgObj;
var titlel;
var msgObj;
var txtlinkname = "";
var hflinkmanID = "";
var txt=document.createElement("p"); 
txt.style.margin="1em 0" ;
txt.setAttribute("id","msgTxt");

   
function ShowLinkManList(custid,LinkmanID,CustLinkMan)
{
    if(custid == "")
    {
        return;
    }
    
   msgw=300;//提示窗口的宽度 
   msgh=100;//提示窗口的高度 
   titleheight=25 //提示窗口标题高度 
   bordercolor="#336699";//提示窗口的边框颜色 
   titlecolor="#99CCFF";//提示窗口的标题颜色 
   txtlinkname = CustLinkMan;//接受传过来的存储姓名控件名
   hflinkmanID = LinkmanID;
       
   linkmanHtml = "<table  style='width:" + msgw + ";'><tr style='width:300;' class='newrow'><td height='22' align='center'>选择</td><td height='22' align='center'>联系人姓名</td></tr>";
   var msgw,msgh,bordercolor; 
      
   var sWidth,sHeight; 
   sWidth=document.body.offsetWidth; 
   sHeight=screen.height; 
   bgObj=document.createElement("div"); 
   bgObj.setAttribute('id','bgDiv'); 
   bgObj.style.position="absolute"; 
   bgObj.style.top="0"; 
   bgObj.style.background="#777"; 
   bgObj.style.filter="progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75"; 
   bgObj.style.opacity="0.6"; 
   bgObj.style.left="0"; 
   bgObj.style.width=sWidth + "px"; 
   //bgObj.style.height=sHeight + "px";
    
   bgObj.style.zIndex = "10000"; 
   document.body.appendChild(bgObj); 

   msgObj=document.createElement("div") 
   msgObj.setAttribute("id","msgDiv"); 
   msgObj.setAttribute("align","center"); 
   msgObj.style.background="white"; 
   msgObj.style.border="1px solid " + bordercolor; 
   msgObj.style.position = "absolute"; 
   msgObj.style.left = "50%"; 
   msgObj.style.top = "50%"; 
   msgObj.style.font="12px/1.6em Verdana, Geneva, Arial, Helvetica, sans-serif"; 
   msgObj.style.marginLeft = "-225px" ; 
   msgObj.style.marginTop = -75+document.documentElement.scrollTop+"px"; 
   msgObj.style.width = msgw + "px"; 
   //msgObj.style.height =msgh + "px"; 
   msgObj.style.height= "300px";
   msgObj.style.overflowY = "scroll";
   msgObj.style.overflowX = "hidden";
   
   msgObj.style.textAlign = "center"; 
   msgObj.style.lineHeight ="25px"; 
   msgObj.style.zIndex = "10001"; 
    
   title=document.createElement("div"); 
   title.setAttribute("id","msgTitle"); 
   title.setAttribute("align","right"); 
   title.style.margin="0"; 
   title.style.padding="3px"; 
   title.style.background=bordercolor; 
   title.style.filter="progid:DXImageTransform.Microsoft.Alpha(startX=20, startY=20, finishX=100, finishY=100,style=1,opacity=75,finishOpacity=100);"; 
   title.style.opacity="0.75"; 
   title.style.border="1px solid " + bordercolor; 
   title.style.height="18px"; 
   title.style.font="12px Verdana, Geneva, Arial, Helvetica, sans-serif"; 
   title.style.color="white"; 
   title.style.cursor="pointer"; 
   var closed = "<img src='../../../Images/Pic/Close.gif' style='cursor:pointer;display:block;' id='CloseImg' onClick='closeEmploy()'>";
   title.innerHTML=closed; 
   
   document.body.appendChild(msgObj); 
   document.getElementById("msgDiv").appendChild(title); 
   
   
   $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/LinkManSele.ashx',//目标地址
           data:'custid='+reescape(custid),
           cache:false,
           //beforeSend:function(){AddPop();},//发送数据之前
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $.each(msg.data,function(i,item){
                      //if(item.ID != null && item.ID != "")
                        linkmanHtml = linkmanHtml + "<tr style='width:" + msgw + ";' class='newrow'><td height='22' align='center'>"+
                        "<input id='Check1' name='Check1' onclick=getLinkman('"+item.id+"','"+item.LinkManName+"') value='选择'  type='radio'/>"+"</td>"+                       
                        "<td height='22' align='center'><a href='#' onclick=SeleLinkman('"+item.id+"')>"+ item.LinkManName +"</a></td>"+
                        "</tr>";
                   });
                   },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){linkmanalert();}//接收数据完毕
           });   
}

function linkmanalert()
{
    linkmanHtml = linkmanHtml + "</table>";
    var sss = title;
    txt.innerHTML= linkmanHtml ; 
    document.getElementById("msgDiv").appendChild(txt); 
    //禁用选择
    linkmanjinzhi();
}

//打开连接
function SeleLinkman(linkid)
{
    window.location.href='LinkMan_Edit.aspx?linkid='+linkid;
}

//关闭
function closelinkman()
{
  document.body.removeChild(bgObj); 
  document.getElementById("msgDiv").removeChild(title); 
  document.body.removeChild(msgObj); 
  linkmanyunxu();//关闭时允许选择
}

//使调用页面获得ID、姓名
function getLinkman(id,employeename)
{
    //$("#txtlinkname").attr("value",employeename);
    //$("#hfLinkmanID").attr("value",id);    
    document.getElementById(hflinkmanID).value = id;
    document.getElementById(txtlinkname).value = employeename;
   
    closelinkman();
}

//禁用选择
function linkmanjinzhi()
{
   //document.getElementById("selecust").removeAttribute("onclick");
   //window.event.returnValue = false;
   document.getElementById(txtlinkname).onclick = function(){ return false;}
}

//允许选择
function linkmanyunxu()
{
    document.getElementById(txtlinkname).onclick = ShowLinkManList;
}