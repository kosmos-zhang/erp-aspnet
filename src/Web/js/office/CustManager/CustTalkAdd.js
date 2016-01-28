var isnew="1";//1添加;2保存
    
var page = "";
var CustName;
var CustID;
var TalkType;
var Priority ;
var TalkBegin ;
var TalkEnd;
var Title;
var Status;
var currentPageIndex;
var pageCount;

var ListType;
var custID;
var custNo;

$(document).ready(function()
{
    requestobj = GetRequest();
    recordnoparam = requestobj['Talkid'];
    page = requestobj['Pages'];
    CustName = requestobj['CustName'];
    CustID = requestobj['CustID'];
    TalkType = requestobj['TalkType'];
    Priority = requestobj['Priority'];
    TalkBegin = requestobj['TalkBegin'];
    TalkEnd = requestobj['TalkEnd'];
    Title = requestobj['Title'];
    Status = requestobj['Status'];
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

        $("#hfCustTalkID").attr("value",recordnoparam);//ID记录在隐藏域中
        //显示返回按钮`
        $("#btn_back").show();
        GetTalkInfo(recordnoparam);
    }
    if(recordnoparam == "-1")
    {
        isnew="1";        
        //显示返回按钮`
        $("#btn_back").show();       
    }
}

//返回
function Back()
{ 
    switch(page)
    {
        case"CustTalk_Info.aspx":
            window.location.href=page+'?CustName='+CustName+'&TalkType='+TalkType+'&Priority='+Priority+'&TalkBegin='+TalkBegin+'&TalkEnd='+TalkEnd+
                '&Title='+Title+'&Status='+Status+'&currentPageIndex='+currentPageIndex+
                '&pageCount='+pageCount+'&CustID='+CustID+'&ModuleID=2021402';
        break;
        case"CustColligate.aspx":
            window.location.href=page+'?ListType='+ListType+'&custID='+custID+'&custNo='+custNo+'&ModuleID=2022101';
        break;
    }
}

function PagePrint()
{
     var Url = document.getElementById("hfCustTalkID").value;
     if(Url == "")
     {
        popMsgObj.Show("打印|","请保存单据再打印|");
        return;
     }
//     if(confirm("请确认您的单据已经保存？"))
//     {
        window.open("../../PrinttingModel/CustManager/TalkPrint.aspx?id=" + Url);
//     }
}

function GetTalkInfo(ComplainID)
{
     $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:"../../../Handler/Office/CustManager/TalkEdit.ashx",//目标地址
       data:'id='+reescape(ComplainID),
       cache:false,
       beforeSend:function(){AddPop();},//发送数据之前           
       success: function(msg){
                //数据获取完毕，填充页面据显示
                $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                        $("#hfCustID").attr("value",item.CustID);//客户ID
                        $("#hfCustNo").attr("value",item.custno);//客户编号                        
                        $("#txtUcCustName").attr("value",item.custnam);//客户简称                         
                        $("#hfLinkmanID").attr("value",item.CustLinkMan);//联系人ID
                        $("#txtUcLinkMan").attr("value",item.linkmanname);//联系人名                        
                        $("#txtJoinUser").attr("value",item.LinkerID);//执行人ID
                        $("#UserLinker").attr("value",item.LinkerName);//执行人Name
                        $("#txtTitle").attr("value",item.Title);//投诉主题
                        document.getElementById("divddlTalkNo").style.display = "none";//隐藏编号选择
                        document.getElementById("divTalkNo").style.display = "block";
                        document.getElementById("divTalkNo").innerHTML = item.TalkNo;//显示编号
                        $("#selePriority").attr("value",item.Priority);
                        $("#ddlTalkType").attr("value",item.TalkType);
                        $("#seleStatus").attr("value",item.Status);
                        
                        $("#txtCompleteDate").val(item.CompleteDate.slice(0,10));//完成期限
                        $("#SeleHour").val(item.CompleteDate.slice(11,14));//时
                        $("#SeleMinute").val(item.CompleteDate.slice(14,16));//分
//alert(item.CompleteDate.slice(0,10) + "***" +item.CompleteDate.slice(11,14) +"***" +item.CompleteDate.slice(14,16)); 

                        $("#txtCreator").attr("value",item.EmployeeName);
                        $("#hfCreator").attr("value",item.Creator);
                        $("#txtCreatedDate").attr("value",item.CreatedDate);
                        $("#txtModifiedUserID").attr("value",item.ModifiedUserID);//最后更新用户
                        $("#txtModifiedDate").attr("value",item.ModifiedDate);//最后更新日期
                        $("#txtContents").attr("value",item.Contents);//内容
                        $("#txtResult").attr("value",item.Result);//处理过程
                        $("#txtFeedback").attr("value",item.Feedback);//客户反馈
                        $("#txtRemark").attr("value",item.remark);//备注
                        
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

//添加
function AddCustTalk()
{
    if(!CheckInput())
    {
        return;
    }
    
    var CustID = document.getElementById("hfCustID").value;//客户
            
    var CustLinkMan = document.getElementById("hfLinkmanID").value;//客户联系人    
    var Linker = document.getElementById("txtJoinUser").value;//执行人
    var Title = document.getElementById("txtTitle").value;//主题 
    var TalkNoType = document.getElementById("ddlTalkNo_ddlCodeRule").value;
    var TalkNo = "";
    //获取编码规则下拉列表选中项  如果选中的是 手工输入时，校验编号是否输入
    if (TalkNoType == "")
    {
        TalkNo = $("#ddlTalkNo_txtCode").val(); //编号
    }
    if(isnew == "2")
    {
        TalkNo = document.getElementById("divTalkNo").innerText;
    }
    var Priority = $("#selePriority").val();//优先级
    var TalkType = $("#ddlTalkType").val();//洽谈类型    
    var Status = $("#seleStatus").val();//状态
    
    var StartH =  $("#SeleHour").val();
    var SeleM =  $("#SeleMinute").val();    
    var CompleteDate = $("#txtCompleteDate").val() + " " + StartH + SeleM;//完成期限
       
    var Creator =  $("#hfCreator").val();//创建者

    var CreatedDate =  $("#txtCreatedDate").val();//创建日期       
    var Contents =  $("#txtContents").val();//行动描述
    var Feedback =  $("#txtFeedback").val();//客户反馈
    var Result =  $("#txtResult").val();//效果评估
    var Remark = $("#txtRemark").val();//备注
    var CanViewUser = $("#txtCanUserID").val();
    var CanViewUserName = $("#txtCanUserName").val(); 

    var ID = document.getElementById("hfCustTalkID").value;//第一次保存后返回的洽谈ID

    $.ajax({
        type:"POST",
        url: "../../../Handler/Office/CustManager/TalkAdd.ashx",
        data: 'CustID='+reescape(CustID)+
            '&CustLinkMan='+reescape(CustLinkMan)+
            '&Linker='+reescape(Linker)+
            '&Title='+reescape(Title)+
            '&TalkNoType='+reescape(TalkNoType)+ 
            '&TalkNo='+reescape(TalkNo)+
            '&Priority='+reescape(Priority)+
            '&TalkType='+reescape(TalkType)+
            '&Status='+reescape(Status)+
            '&CompleteDate='+reescape(CompleteDate)+
            '&Creator='+reescape(Creator)+
            '&CreatedDate='+reescape(CreatedDate)+           
            '&Contents='+reescape(Contents)+
            '&Feedback='+reescape(Feedback)+ 
            '&Result='+reescape(Result)+
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
                     document.getElementById("hfCustTalkID").value = data.data;
                     document.getElementById("divddlTalkNo").style.display = "none";//隐藏客户关怀单编号选择
                     document.getElementById("divTalkNo").style.display = "block";
                     document.getElementById("divTalkNo").innerHTML = data.info;
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

var CustNo = document.getElementById("hfCustNo").value;//客户ID
var CustID = document.getElementById("hfCustID").value;//客户ID
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
    var txtUcLinkMan = document.getElementById('txtUcLinkMan').value;//客户联系人姓名
    var hfLinkmanID = document.getElementById('hfLinkmanID').value;//客户联系人ID    
    var UserLinker = document.getElementById('UserLinker').value;//执行人姓名
    var txtJoinUser = document.getElementById('txtJoinUser').value;//执行人ID    
    var txtTitle = document.getElementById('txtTitle').value;//关怀时主题    
    var TalkNoType = document.getElementById("ddlTalkNo_ddlCodeRule").value;
    var TalkNo = document.getElementById("ddlTalkNo_txtCode").value;
    var txtCompleteDate = document.getElementById("txtCompleteDate").value;//执行日期 
    var CreatedDate = document.getElementById("txtCreatedDate").value;//创建日期 
    
    var Contents = document.getElementById("txtContents").value;//行动描述
    var Feedback = document.getElementById("txtFeedback").value;//客户反馈
    var Result = document.getElementById("txtResult").value;//效果评估
    var Remark = document.getElementById("txtRemark").value;//备注
    var RetVal=CheckSpecialWords();
    
    //获取编码规则下拉列表选中项  如果选中的是 手工输入时，校验编号是否输入
    if (isnew == "1" && TalkNoType == "" && TalkNo =="")
    {
       isFlag = false;
       fieldText = fieldText + "洽谈编号|";  
   		msgText = msgText +  "请输入洽谈编号|";
    }
    if(isnew == "1" && TalkNoType == "" && TalkNo != "")
    {
        if(!CodeCheck(TalkNo))
        {
            isFlag = false;
           fieldText = fieldText + "洽谈编号|";
   		    msgText = msgText +  "编号只能由英文字母（a-z大小写均可）、数字（0-9）、字符（_-/.()[]{}）组成|";
        }
    }
//    if (isnew == "1" && TalkNoType == "" && TalkNo !="" && TalkNo.match(/^[A-Za-z0-9_]+$/) == null)//如果选中的是手工输入时，编号只能包含字母和数字
//    {
//        isFlag = false;  
//        fieldText = fieldText + "洽谈编号|";      
//   		msgText = msgText +  "洽谈编号只能包含字母或数字|";
//    }
    if(txtCustNam=="" || hfCustID == "")
    {
        isFlag = false;
        fieldText = fieldText + "对应客户|";
   		msgText = msgText +  "请选择对应客户|";
    }
    if(txtUcLinkMan=="" || hfLinkmanID == "")
    {
        isFlag = false;
        fieldText = fieldText + "客户联系人|";
   		msgText = msgText +  "请选择客户联系人|";
    }    
    if(UserLinker =="" || txtJoinUser == "")
    {
        isFlag = false;
        fieldText = fieldText + "执行人|";
   		msgText = msgText +  "请选择执行人|";
    }    
    if(txtTitle.Trim() == "")
    {
        isFlag = false;
        fieldText = fieldText + "洽谈主题|";
   		msgText = msgText +  "请输入洽谈主题|";
    }
    if(txtCompleteDate == "")
    {
        isFlag = false;
        fieldText = fieldText + "完成期限|";
   		msgText = msgText +  "请输入完成期限|";
    }

    if(strlen(Contents)>1000)
    {
        isFlag = false;
        fieldText = fieldText + "行动描述|";
   		msgText = msgText + "行动描述最多只允许输入1000个字符|";
    }
    if(strlen(Feedback)>1000)
    {
        isFlag = false;
        fieldText = fieldText + "客户反馈|";
   		msgText = msgText + "客户反馈最多只允许输入1000个字符|";
    }
    if(strlen(Result)>1000)
    {
        isFlag = false;        
        fieldText = fieldText + "效果评估|";
   		msgText = msgText + "效果评估最多只允许输入1000个字符|";
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
   
    if(!isFlag)
    {
        //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msgText);        
        popMsgObj.Show(fieldText,msgText);
    }
    return isFlag;
}