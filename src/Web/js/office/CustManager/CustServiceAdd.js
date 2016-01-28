var isnew="1";//1添加;2保存

var page;
var CustName;
var CustID;
var ServeType;
var Fashion;
var ServiceDateBegin;
var ServiceDateEnd;
var Title;
var CustLinkMan;
var Executant;
var currentPageIndex;
var pageCount;

var ListType;
var custID;
var custNo;

$(document).ready(function()
{
    requestobj = GetRequest();   
    recordnoparam = requestobj['serviceid']; 
    page = requestobj['Pages'];
    CustName = requestobj['CustName'];
    CustID = requestobj['CustID'];
    ServeType = requestobj['ServeType'];
    Fashion = requestobj['Fashion'];
    ServiceDateBegin = requestobj['ServiceDateBegin'];
    ServiceDateEnd = requestobj['ServiceDateEnd'];
    Title = requestobj['Title'];
    CustLinkMan = requestobj['CustLinkMan'];
    Executant = requestobj['Executant'];
    currentPageIndex = requestobj['currentPageIndex'];
    pageCount = requestobj['pageCount'];
    
    ListType = requestobj['ListType'];
    custID = requestobj['custID'];
    custNo = requestobj['custNo'];

    DealPage(recordnoparam);
});

//处理加载页面
function DealPage(recordnoparam)
{
    if(typeof(recordnoparam)!="undefined" && recordnoparam != "-1")
    {
        isnew="2";
         $("#TitleModify").show();
        $("#TitleAdd").hide();
        $("#hfCustServiceID").attr("value",recordnoparam);
        //显示返回按钮
        $("#btn_back").show();
        GetServiceInfo(recordnoparam);
    }
    if(recordnoparam == "-1")
    {
        //显示返回按钮
        $("#btn_back").show();
    }
}

//返回
function Back()
{
    switch(page)
    {
        case"CustService_Info.aspx":
               window.location.href=page+'?CustName='+CustName+'&ServeType='+ServeType+'&Fashion='+Fashion+'&ServiceDateBegin='+ServiceDateBegin+'&ServiceDateEnd='+ServiceDateEnd+
            '&Title='+Title+'&CustLinkMan='+CustLinkMan+'&Executant='+Executant+'&currentPageIndex='+currentPageIndex+'&pageCount='+pageCount+'&CustID='+CustID+'&ModuleID=2021602';
        break;
        case"CustColligate.aspx":
            window.location.href=page+'?ListType='+ListType+'&custID='+custID+'&custNo='+custNo+'&ModuleID=2022101';
        break;
    }
}

function PagePrint()
{
     var Url = document.getElementById("hfCustServiceID").value;
     if(Url == "")
     {
        popMsgObj.Show("打印|","请保存单据再打印|");
        return;
     }
//     if(confirm("请确认您的单据已经保存？"))
//     {
        window.open("../../PrinttingModel/CustManager/ServicePrint.aspx?id=" + Url);
//     }
}

//////获取客户服务信息(修改、查看)
 function GetServiceInfo(serviceid)
{
       $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:"../../../Handler/Office/CustManager/ServiceEdit.ashx",//目标地址
       data:'id='+reescape(serviceid),
       cache:false,
       beforeSend:function(){AddPop();},//发送数据之前           
       success: function(msg){
                //数据获取完毕，填充页面据显示
                $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                        $("#hfCustID").attr("value",item.custid);//客户ID
                        $("#hfCustNo").attr("value",item.custno);//客户编号
                        $("#txtUcCustName").attr("value",item.custname);//客户简称
                        $("#hfLinkmanID").attr("value",item.CustLinkMan);//联系人ID
                        $("#txtUcLinkMan").attr("value",item.LinkManName);//联系人名 
                        $("#hfEmplIDL").attr("value",item.OurLinkMan);//我方联络人ID
                        $("#UserEmployeeID").attr("value",item.OurLinkManName);//我方联络人名
                        $("#txtTitle").attr("value",item.Title);//服务主题
                        document.getElementById("divddlServiceNo").style.display = "none";//隐藏客户联络单编号选择
                        document.getElementById("divServeNo").style.display = "block";
                        document.getElementById("divServeNo").innerHTML = item.ServeNo;//显示联络单编号
                        $("#txtCustLinkTel").attr("value",item.CustLinkTel);//客户联系电话                       
                        $("#ddlServeType").val(item.ServeType);//服务类型
                        $("#ddlFashion").attr("value",item.Fashion);//服务方式
                        $("#seleState").attr("value",item.State);//服务状态                        
                       
                        $("#txtBeginDate").val(item.BeginDate.slice(0,10));//日期
                        $("#SeleHour").val(item.BeginDate.slice(11,14));//时
                        $("#SeleMinute").val(item.BeginDate.slice(14,16));//分                        
                        //alert(item.BeginDate.slice(0,10) + "***" +item.BeginDate.slice(11,14) +"***" +item.BeginDate.slice(14,16)); 
                        
                        $("#UserEmplNameE").attr("value",item.ExecutantName);//执行人名
                        $("#hfEmplIDE").attr("value",item.Executant);//执行人ID
                        $("#txtSpendTime").attr("value",item.SpendTime);//花费时间                       
                        $("#seleDateUnit").val(item.DateUnit);//花费时间单位
                        $("#txtModifiedDate").attr("value",item.ModifiedDate);//最后更新日期
                        $("#txtModifiedUserID").attr("value",item.ModifiedUserID);//最后更新用户                        
                        $("#txtContents").attr("value",item.Contents);//服务内容
                        $("#txtFeedback").attr("value",item.Feedback);//客户反馈
                        $("#txtLinkQA").attr("value",item.LinkQA);//对应QA
                        $("#txtRemark").attr("value",item.Remark);//备注
                        
                        var aaa = item.CanViewUser.replace(",","");
                        var bbb = aaa.lastIndexOf(",");
                        var ccc = aaa.slice(0,bbb);                        
                        $("#txtCanUserID").val(ccc);
                        $("#txtCanUserName").val(item.CanViewUserName);
               });
              },
       error: function() 
       {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
       }, 
       complete:function(){hidePopup();}//接收数据完毕
       });
}

function AddCustService()
{
   if(!CheckInput())
    {
        return;
    }
    var CustID = document.getElementById("hfCustID").value;//客户ID
    var CustLinkMan = document.getElementById("hfLinkmanID").value;//客户联系人    
    var OurLinkMan = document.getElementById("hfEmplIDL").value;//我方联系人
    var Title = document.getElementById("txtTitle").value;//服务主题
    var ServeNoType = document.getElementById("ddlServeNO_ddlCodeRule").value;
    var ServeNo = "";
    //获取编码规则下拉列表选中项  如果选中的是 手工输入时，校验编号是否输入
    if (ServeNoType == "")
    {
        ServeNo = $("#ddlServeNO_txtCode").val(); //服务单编号
    }
    if(isnew == "2")
    {
        ServeNo = document.getElementById("divServeNo").innerText;
    }
    
    var CustLinkTel = $("#txtCustLinkTel").val();
    var ServeType = $("#ddlServeType").val();//服务类型
    var Fashion = $("#ddlFashion").val();//服务方式
    var State = $("#seleState").val();////服务状态
    
    //var BeginDate = $("#txtBeginDate").val();
    var StartH =  $("#SeleHour").val();
    var SeleM =  $("#SeleMinute").val();    
    var BeginDate = $("#txtBeginDate").val() + " " + StartH + SeleM;
    
    var Executant = $("#hfEmplIDE").val();//执行人
    var SpendTime = $("#txtSpendTime").val();//花费时间
    var DateUnit = $("#seleDateUnit").val();//花费时间单位
    var Contents =  $("#txtContents").val();//服务内容
    var Feedback =  $("#txtFeedback").val();//客户反馈
    var LinkQA =  $("#txtLinkQA").val();//对应QA
    var Remark = $("#txtRemark").val();//备注
      var CanViewUser = $("#txtCanUserID").val();
    var CanViewUserName = $("#txtCanUserName").val(); 
    var ID = document.getElementById("hfCustServiceID").value;//第一次保存后返回的服务ID

    $.ajax({
        type:"POST",
        url: "../../../Handler/Office/CustManager/ServiceAdd.ashx",
        data: 'CustID='+reescape(CustID)+
            '&CustLinkMan='+reescape(CustLinkMan)+
            '&OurLinkMan='+reescape(OurLinkMan)+
            '&Title='+reescape(Title)+
            '&ServeNoType='+reescape(ServeNoType)+
            '&ServeNo='+reescape(ServeNo)+
            '&CustLinkTel='+reescape(CustLinkTel)+
            '&ServeType='+reescape(ServeType)+  
            '&Fashion='+reescape(Fashion)+  
            '&State='+reescape(State)+  
            '&BeginDate='+reescape(BeginDate)+  
            '&Executant='+reescape(Executant)+  
            '&SpendTime='+reescape(SpendTime)+  
            '&DateUnit='+reescape(DateUnit)+
            '&Contents='+reescape(Contents)+  
            '&Feedback='+reescape(Feedback)+  
            '&LinkQA='+reescape(LinkQA)+  
            '&Remark='+reescape(Remark)+
            '&CanViewUser='+reescape(CanViewUser)+
            '&CanViewUserName='+reescape(CanViewUserName)+
            '&ID='+reescape(ID)+ 
            '&action='+reescape(isnew),
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
                     isnew="2";
                     document.getElementById("hfCustServiceID").value = data.data;
                     document.getElementById("divddlServiceNo").style.display = "none";//隐藏客户服务单编号选择
                     document.getElementById("divServeNo").style.display = "block";
                     document.getElementById("divServeNo").innerHTML = data.info;                     
                     showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
                     return;
                } 
                if(data.sta==2) 
                { 
                     showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",data.data);
                   
                } 
                else 
                { 
                  hidePopup();
                  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
                } 
            }
      });
}

function ShowLinkMan(LinkmanID,CustLinkMan)
{
    ShowLinkManList(document.getElementById("hfCustNo").value,LinkmanID,CustLinkMan);
}

//主表单验证
function CheckInput()
{
var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var txtCustNam = document.getElementById('txtUcCustName').value;//客户简称
    var hfCustID = document.getElementById('hfCustID').value;//客户ID
    var txtCustLinkMan = document.getElementById('txtUcLinkMan').value;//客户联络人姓名
    var hfLinkmanID = document.getElementById('hfLinkmanID').value;//客户联络人ID    
    var UserEmployeeID = document.getElementById('UserEmployeeID').value;//我方联络人姓名
    var hfEmplIDL = document.getElementById('hfEmplIDL').value;//我方联络人ID
    var UserEmplNameE = document.getElementById('UserEmplNameE').value;//我方执行人姓名
    var hfEmplIDE = document.getElementById('hfEmplIDE').value;//我方执行人ID
    var txtTitle = document.getElementById('txtTitle').value;//服务主题
    var ServeNoType = document.getElementById("ddlServeNO_ddlCodeRule").value;
    var ServeNo = document.getElementById("ddlServeNO_txtCode").value;
    var BeginDate = document.getElementById("txtBeginDate").value;//服务时间
    var SpendTime = document.getElementById("txtSpendTime").value;//花费时间
    var BeginDate = document.getElementById("txtBeginDate").value;//
    var Contents = document.getElementById("txtContents").value;//服务内容
    var Feedback = document.getElementById("txtFeedback").value;//客户反馈
    var LinkQA = document.getElementById("txtLinkQA").value;//对应需求
    var Remark = document.getElementById("txtRemark").value;//备注
    var RetVal=CheckSpecialWords();
    var ServeType = document.getElementById("ddlServeType").value;//服务类型
    var Fashion = document.getElementById("ddlFashion").value;//服务类型
    
    //获取编码规则下拉列表选中项  如果选中的是 手工输入时，校验编号是否输入
    if (isnew == "1" && ServeNoType == "" && ServeNo =="")
    {
       isFlag = false;
       fieldText = fieldText + "服务单编号|";
   		msgText = msgText +  "请输入服务单编号|";
    }
    if(isnew == "1" && ServeNoType == "" && ServeNo != "")
    {
        if(!CodeCheck(ServeNo))
        {
            isFlag = false;
           fieldText = fieldText + "服务单编号|";
   		    msgText = msgText + "编号只能由英文字母（a-z大小写均可）、数字（0-9）、字符（_-/.()[]{}）组成|";
        }
    }
    
    if(txtCustNam=="" || hfCustID == "")
    {
        isFlag = false;
        fieldText = fieldText + "对应客户|";
   		msgText = msgText +  "请选择对应客户|";
    } 
    if(txtCustLinkMan=="" || hfLinkmanID == "")
    {
        isFlag = false;
        fieldText = fieldText + "客户联络人|";
   		msgText = msgText +  "请选择客户联络人|";
    }    
    if(UserEmployeeID=="" || hfEmplIDL == "")
    {
        isFlag = false;
        fieldText = fieldText + "我方联络人|";
   		msgText = msgText +  "请选择我方联络人|";
    }
    if(UserEmplNameE=="" || hfEmplIDE == "")
    {
        isFlag = false;
        fieldText = fieldText + "我方执行人|";
   		msgText = msgText +  "请输入执行人|";
    }
    if(txtTitle.Trim() == "")
    {
        isFlag = false;
        fieldText = fieldText + "服务主题|";
   		msgText = msgText +  "请输入服务主题|";
    }
    if(BeginDate == "")
    {
        isFlag = false;
        fieldText = fieldText + "服务时间|";
   		msgText = msgText +  "请输入服务时间|";
    }
    if(SpendTime.length > 0)
    {
        if(!IsNumeric(SpendTime,4,2))
        {
            isFlag = false;
            fieldText = fieldText + "花费时间|";
   		    msgText = msgText + "花费时间格式不正确|";  
        }
    }   
    if(SpendTime == "")
    {
        document.getElementById("txtSpendTime").value = "0.00";
    }    
    if(strlen(Contents)>1000)
    {
        isFlag = false;        
        fieldText = fieldText + "服务内容|";
   		msgText = msgText + "服务内容最多只允许输入1000个字符|";
    }
    if(strlen(Feedback)>200)
    {
        isFlag = false;
        fieldText = fieldText + "客户反馈|";
   		msgText = msgText + "客户反馈最多只允许输入200个字符|";
    }
    if(strlen(LinkQA)>200)
    {
        isFlag = false;
        fieldText = fieldText + "对应需求|";
   		msgText = msgText + "对应需求最多只允许输入200个字符|";
    }
    if(strlen(Remark)>1000)
    {
        isFlag = false;
        fieldText = fieldText + "备注|";
   		msgText = msgText + "备注最多只允许输入1000个字符|";
    }
    if(RetVal!="")
    {
            isFlag = false;
            fieldText = fieldText + RetVal+"|";
   		    msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
    if(ServeType == "" || ServeType == "0")
    {
        isFlag = false;
        fieldText = fieldText + "服务类型|";
   		msgText = msgText +  "请首先配置服务类型|";
    }
    if(Fashion == "")
    {
        isFlag = false;
        fieldText = fieldText + "服务方式|";
   		msgText = msgText +  "请选择服务方式|";
    }
    
    if(!isFlag)
    {
       //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msgText);        
       popMsgObj.Show(fieldText,msgText);
    }
    return isFlag;
}
