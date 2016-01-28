<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmployeeSel.ascx.cs" Inherits="UserControl_EmployeeSel" %>

<script type="text/javascript">
   var custinfo;
   var bgObj;
   var titlel;
   var msgObj;
   var txt=document.createElement("p"); 
   txt.style.margin="1em 0" ;
   txt.setAttribute("id","msgTxt");
function EmployeeAlert()
{ 
   msgw=400;//提示窗口的宽度 
   msgh=100;//提示窗口的高度 
   titleheight=25 //提示窗口标题高度 
   bordercolor="#336699";//提示窗口的边框颜色 
   titlecolor="#99CCFF";//提示窗口的标题颜色 
   
   custinfo = "<table  style='width:" + msgw + ";'><tr style='width:400;' class='newrow'><td height='22' align='center'>选择</td><td height='22' align='center'>员工姓名</td></tr>";
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
           url:  '../../../Handler/Office/CustManager/EmployeeSel.ashx',//目标地址
           cache:false,
           //beforeSend:function(){AddPop();},//发送数据之前
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $.each(msg.data,function(i,item){
                      //if(item.ID != null && item.ID != "")
                        custinfo = custinfo + "<tr style='width:" + msgw + ";' class='newrow'><td height='22' align='center'>"+
                        "<input id='Check1' name='Check1' onclick=getEmploy('"+item.id+"','"+item.EmployeeName+"') value='选择'  type='radio'/>"+"</td>"+                       
                        "<td height='22' align='center'><a href='#' onclick=SeleEmployee('"+item.id+"')>"+ item.EmployeeName +"</a></td>"+
                        "</tr>";
                   });
                   },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){ealert();}//接收数据完毕
           });
}
function ealert()
{
    custinfo = custinfo + "</table>";
    var sss = title;
    txt.innerHTML= custinfo ; 
    document.getElementById("msgDiv").appendChild(txt); 
    //禁用选择
    Employeejinzhi();    
}

//打开连接
function SeleEmployee(custid)
{
    //window.location.href='LinkMan_Edit.aspx?linkid='+custid;
}

//关闭
function closeEmploy()
{
  document.body.removeChild(bgObj); 
  document.getElementById("msgDiv").removeChild(title); 
  document.body.removeChild(msgObj); 
  Employeeyunxu();//关闭时允许选择
}

//使调用页面获得员工ID、姓名
function getEmploy(id,employeename)
{
    var txtemployeeid = document.getElementById("EmployeeSel1_hfEmployeeIDControl").value;    

    if(txtemployeeid !="")
    {
        document.getElementById(txtemployeeid).value = id;
    }
    //window.parent.document.getElementById("txtEmployeeName").value = employeename;
    document.getElementById("txtEmployeeName").value = employeename; 
    
    closeEmploy();
}

//禁用选择
function Employeejinzhi()
{
   //document.getElementById("selecust").removeAttribute("onclick");
   //window.event.returnValue = false;
   document.getElementById("txtEmployeeName").onclick = function(){ return false;}
}

//允许选择
function Employeeyunxu()
{
    //document.getElementById("selecust").onclick = function(){ return true;}    
    document.getElementById("txtEmployeeName").onclick = EmployeeAlert;
}
</script>
<input name="txtEmployeeName" onclick="EmployeeAlert();" id="txtEmployeeName"  type="text" class="tdinput"  size="15" />
<asp:HiddenField ID="hfEmployeeIDControl" runat="server" />