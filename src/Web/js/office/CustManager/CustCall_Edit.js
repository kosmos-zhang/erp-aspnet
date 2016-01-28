var ListType;
var custID;
var custNo;

$(document).ready(function()
{
    requestobj = GetRequest();
    var CallID = requestobj['ID'];
    document.getElementById("hfCallID").value = CallID;
    
    ListType = requestobj['ListType'];
    custID = requestobj['custID'];
    custNo = requestobj['custNo'];
    
    GetLoveInfo(CallID);
});

function GetLoveInfo(CallID)
{
    $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:"../../../Handler/Office/CustManager/CustTelShow.ashx",//目标地址
       data:'id='+reescape(CallID)+'&action=CallLoad',
       cache:false,
       beforeSend:function(){AddPop();},//发送数据之前           
       success: function(msg){
                //数据获取完毕，填充页面据显示
                $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                    {
                        $("#txtCustName").val(item.CustName);//客户
                        $("#hidCustID").val(item.CustID);//客户ID                        
                        $("#txtCallTime").val(item.CallTime);
                        $("#txtCallor").val(item.Callor);
                        $("#txtCreator").val(item.EmployeeName);
                        $("#txtCallContents").val(item.CallContents);
                        $("#txtModifiedUserID").val(item.ModifiedUserName);
                        $("#txtModifiedDate").val(item.ModifiedDate);
                        $("#txtTitle").val(item.Title);
                    }
                       
               });
              },
       error: function() 
       {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
       }, 
       complete:function(){hidePopup();}//接收数据完毕
       });
}

//修改客户来电信息
function EditCustCall()
{
    var CallID =  document.getElementById("hfCallID").value;
    var Callor =  document.getElementById("txtCallor").value;//来电人
    var CallContents =  document.getElementById("txtCallContents").value;//通话内容
    var Title =  document.getElementById("txtTitle").value;//通话biaoti
    
   
    $.ajax({
        type:"POST",
        url: "../../../Handler/Office/CustManager/CustTelShow.ashx",
        data: 'action=CallEdit'+
            '&CallID='+escape(CallID)+'&Title='+escape(Title)+
            '&Callor='+escape(Callor)+
            '&CallContents='+escape(CallContents),
            dataType:'json',
        cache:false,
        beforeSend:function()
          { 
              AddPop();
          },
          error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");},
          success:function(data) 
            { 
                if(data.sta==1) 
                { 
                     showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
                     return;
                } 
                else 
                { 
                  hidePopup();
                  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
                } 
            }
      });
}

function Back()
{
     if(typeof(ListType)!="undefined")
    {
        window.location.href='CustColligate.aspx?ListType='+ListType+'&custID='+custID+'&custNo='+custNo+'&ModuleID=2022101';
    }
    else
    {
        history.back(); 
    }
}