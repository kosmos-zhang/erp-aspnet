var isnew="1";//1添加;2保存

var page = "";
var CustName;
var CustID;
var ComplainType;
var Critical ;
var ComplainBegin ;
var ComplainEnd;
var Title;
var CustLinkMan ;
var EmplNameL ;
var State;
var currentPageIndex;
var pageCount;

var ListType;
var custID;
var custNo;

$(document).ready(function()
{
    requestobj = GetRequest();
    recordnoparam = requestobj['Complainid'];
    page = requestobj['Pages'];
    CustName = requestobj['CustName'];
    CustID = requestobj['CustID'];
    ComplainType = requestobj['ComplainType'];
    Critical = requestobj['Critical'];
    ComplainBegin = requestobj['ComplainBegin'];
    ComplainEnd = requestobj['ComplainEnd'];
    Title = requestobj['Title'];
    CustLinkMan = requestobj['CustLinkMan'];
    EmplNameL = requestobj['EmplNameL'];
    State = requestobj['State'];
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
    if(typeof(recordnoparam)!="undefined" && recordnoparam != -1)
    {
        isnew="2";
        $("#TitleModify").show();
        $("#TitleAdd").hide();
        $("#hfComplainID").attr("value",recordnoparam);//投诉ID记录在隐藏域中
        //显示返回按钮
        $("#btn_back").show();
        GetComplainInfo(recordnoparam);
    }
    if(recordnoparam == -1)
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
        case"CustComplain_Info.aspx":
            window.location.href=page+'?CustName='+CustName+'&ComplainType='+ComplainType+'&CustID='+CustID+'&ModuleID=2021702'+
                '&Critical='+Critical+'&ComplainBegin='+ComplainBegin+'&ComplainEnd='+ComplainEnd+'&Title='+Title+'&CustLinkMan='+CustLinkMan+
                '&EmplNameL='+EmplNameL+'&State='+State+'&currentPageIndex='+currentPageIndex+'&pageCount='+pageCount;
        break;
        case"CustColligate.aspx":
            window.location.href=page+'?ListType='+ListType+'&custID='+custID+'&custNo='+custNo+'&ModuleID=2022101';
        break;
    }
    
   
}

function PagePrint()
{
     var Url = document.getElementById("hfComplainID").value;
     if(Url == "")
     {
        popMsgObj.Show("打印|","请保存单据再打印|");
        return;
     }
//     if(confirm("请确认您的单据已经保存？"))
//     {
        window.open("../../PrinttingModel/CustManager/ComplainPrint.aspx?id=" + Url);
//     }
}

function GetComplainInfo(ComplainID)
{
     $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:"../../../Handler/Office/CustManager/ComplainEdit.ashx",//目标地址
       data:'id='+reescape(ComplainID),
       cache:false,
       beforeSend:function(){AddPop();},//发送数据之前           
       success: function(msg){
                //数据获取完毕，填充页面据显示
                $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                        $("#hfCustID").attr("value",item.CustID);//客户ID
                        $("#hfCustNo").attr("value",item.CustNo);//客户编号
                        $("#txtUcCustName").attr("value",item.CustNam);//客户简称
                        $("#hfLinkmanID").attr("value",item.CustLinkMan);//联系人ID
                        $("#txtUcLinkMan").attr("value",item.LinkManName);//联系人名
                        
                        $("#hfDestClerkID").attr("value",item.DestClerk);//接待人ID
                        $("#UserDestClerk").attr("value",item.EmployeeName);//接待人名
                        
                        $("#txtTitle").attr("value",item.Title);//投诉主题                        
                        document.getElementById("divddlComplainNo").style.display = "none";//隐藏客户投诉单编号选择
                        document.getElementById("divComplainNo").style.display = "block";
                        document.getElementById("divComplainNo").innerHTML = item.ComplainNo;//显示投诉单编号                        
                        $("#txtCustLinkTel").attr("value",item.CustLinkTel);//客户联系电话                       
                        $("#ddlComplainType").val(item.ComplainType);//投诉类型
                        $("#seleCritical").attr("value",item.Critical);//紧急程度
                        $("#seleState").attr("value",item.State);//投诉状态
                        $("#txtSpendTime").attr("value",item.SpendTime);//花费时间
                        $("#seleDateUnit").attr("value",item.DateUnit);//花费时间单位
                        
                        //$("#txtComplainDate").attr("value",item.ComplainDate);//投诉日期
                        $("#txtComplainDate").val(item.ComplainDate.slice(0,10));//投诉日期
                        $("#SeleHour").val(item.ComplainDate.slice(11,14));//时
                        $("#SeleMinute").val(item.ComplainDate.slice(14,16));//分
                        
                        $("#txtModifiedDate").attr("value",item.ModifiedDate);//最后更新日期
                        $("#txtModifiedUserID").attr("value",item.ModifiedUserID);//最后更新用户
                        $("#txtContents").attr("value",item.Contents);//投诉内容
                        $("#UserComplainedMan").attr("value",item.ComplainedManName);//被投诉人名
                        $("#hfComplainedManID").attr("value",item.ComplainedMan);//被投诉ID
                        $("#txtDisposalProcess").attr("value",item.DisposalProcess);//处理过程                        
                        $("#txtFeedback").attr("value",item.Feedback);//客户反馈
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

//添加
function AddCustComplain()
{
    if(!CheckInput())
    {
        return;
    }
    //禁用保存按钮，防止2次提交
    var CustID = document.getElementById("hfCustID").value;//客户ID
    var CustLinkMan = document.getElementById("hfLinkmanID").value;//客户联系人    
    var DestClerk = document.getElementById("hfDestClerkID").value;//我方接待人
    var Title = document.getElementById("txtTitle").value;//投诉主题
    var ComplainNoType = document.getElementById("ddlComplainNo_ddlCodeRule").value;
    var ComplainNo = "";
    //获取编码规则下拉列表选中项  如果选中的是 手工输入时，校验编号是否输入
    if (ComplainNoType == "")
    {
        ComplainNo = $("#ddlComplainNo_txtCode").val(); //投诉单编号
    }
    if(isnew == "2")
    {
        ComplainNo = document.getElementById("divComplainNo").innerText;
    }
    
    var CustLinkTel = $("#txtCustLinkTel").val();//客户联系人电话
    var ComplainType = $("#ddlComplainType").val();//投诉类型    
    var Critical = $("#seleCritical").val();//紧急程度    
    var State = $("#seleState").val();//状态
    var SpendTime = $("#txtSpendTime").val();//花费时间
    var DateUnit = $("#seleDateUnit").val();//花费时间单位    
    
    var CanViewUser = $("#txtCanUserID").val();
    var CanViewUserName = $("#txtCanUserName").val(); 
    var StartH =  $("#SeleHour").val();
    var SeleM =  $("#SeleMinute").val();    
    var ComplainDate = $("#txtComplainDate").val() + " " + StartH + SeleM;//完成期限
    
    var Contents =  $("#txtContents").val();//投诉内容
    var ComplainedMan = document.getElementById("hfComplainedManID").value;//被投诉人ID
    var DisposalProcess = $("#txtDisposalProcess").val();//处理过程
    var Feedback =  $("#txtFeedback").val();//客户反馈
    var Remark = $("#txtRemark").val();//备注
    
    var ID = document.getElementById("hfComplainID").value;//第一次保存后返回的投诉ID

    $.ajax({
        type:"POST",
        url: "../../../Handler/Office/CustManager/ComplainAdd.ashx",
        data: 'CustID='+reescape(CustID)+
            '&CustLinkMan='+reescape(CustLinkMan)+
            '&DestClerk='+reescape(DestClerk)+
            '&Title='+reescape(Title)+
            '&ComplainNoType='+reescape(ComplainNoType)+ 
            '&ComplainNo='+reescape(ComplainNo)+            
            '&CustLinkTel='+reescape(CustLinkTel)+            
            '&ComplainType='+reescape(ComplainType)+
            '&Critical='+reescape(Critical)+
            '&State='+reescape(State)+  
            '&SpendTime='+reescape(SpendTime)+  
            '&DateUnit='+reescape(DateUnit)+            
            '&ComplainDate='+reescape(ComplainDate)+ 
             '&CanViewUser='+reescape(CanViewUser)+
            '&CanViewUserName='+reescape(CanViewUserName)+         
            '&Contents='+reescape(Contents)+
            '&ComplainedMan='+reescape(ComplainedMan)+
            '&DisposalProcess='+reescape(DisposalProcess)+  
            '&Feedback='+reescape(Feedback)+ 
            '&Remark='+reescape(Remark)+
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
                     document.getElementById("hfComplainID").value = data.data;
                     document.getElementById("divddlComplainNo").style.display = "none";//隐藏客户服务单编号选择
                     document.getElementById("divComplainNo").style.display = "block";
                     document.getElementById("divComplainNo").innerHTML = data.info;
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
    var ComplainNoType = document.getElementById("ddlComplainNo_ddlCodeRule").value;
    var ComplainNo = document.getElementById("ddlComplainNo_txtCode").value;
    var txtCustLinkMan = document.getElementById('txtUcLinkMan').value;//客户联络人姓名
    var hfLinkmanID = document.getElementById('hfLinkmanID').value;//客户联络人ID
    var UserDestClerk = document.getElementById('UserDestClerk').value;//我方联络人姓名
    var hfDestClerkID = document.getElementById('hfDestClerkID').value;//我方联络人ID
    var txtTitle = document.getElementById('txtTitle').value;//投诉时主题    
    var ComplainDate = document.getElementById("txtComplainDate").value;//服务时间
    var SpendTime = document.getElementById("txtSpendTime").value;//花费时间
    
    var Contents = document.getElementById("txtContents").value;//投诉内容
    var DisposalProcess = document.getElementById("txtDisposalProcess").value;//处理过程
    var Feedback = document.getElementById("txtFeedback").value;//客户反馈
    var Remark = document.getElementById("txtRemark").value;//备注
    var RetVal=CheckSpecialWords();
    var ComplainType = document.getElementById("ddlComplainType").value;//投诉类型
    
    //获取编码规则下拉列表选中项  如果选中的是 手工输入时，校验编号是否输入
    if (isnew == "1" && ComplainNoType == "" && ComplainNo =="")
    {
       isFlag = false;
       fieldText = fieldText + "投诉单编号|";
   		msgText = msgText +  "请输入投诉单编号|";
    }
    if(isnew == "1" && ComplainNoType == "" && ComplainNo != "")
    {
        if(!CodeCheck(ComplainNo))
        {
            isFlag = false;
           fieldText = fieldText + "投诉单编号|";
   		    msgText = msgText +  "编号只能由英文字母（a-z大小写均可）、数字（0-9）、字符（_-/.()[]{}）组成|";
        }
    }
    if(txtCustNam=="" || hfCustID == "")
    {
        isFlag = false;
         fieldText = fieldText + "投诉客户|";
   		msgText = msgText +  "请选择投诉客户|";
    }
    if(txtCustLinkMan=="" || hfLinkmanID == "")
    {
        isFlag = false;
        fieldText = fieldText + "客户联络人|";
   		msgText = msgText +  "请选择客户联络人|";
    }    
    if(UserDestClerk =="" || hfDestClerkID == "")
    {
        isFlag = false;
        fieldText = fieldText + "接待人|";
   		msgText = msgText +  "请选择接待人|";
    }
    if(txtTitle.Trim() == "")
    {
        isFlag = false;
        fieldText = fieldText + "主题|";
   		msgText = msgText +  "请输入主题|";
    }
    if(ComplainDate == "")
    {
        isFlag = false;
        fieldText = fieldText + "投诉时间|";
   		msgText = msgText +  "请输入投诉时间|";
    }
    if(SpendTime.length > 0)
    {
        if(IsZint(SpendTime) && SpendTime.length> 4)
        {
            isFlag = false;
            fieldText = fieldText + "花费时间|";
   		    msgText = msgText + "花费时间最打允许输入4位整数和2位小数|"; 
        }
        if(!IsNumberOrNumeric(SpendTime,4,2))
        {
            isFlag = false;
            fieldText = fieldText + "花费时间|";
   		    msgText = msgText + "花费时间格式不正确|";  
        }
    }    
    if(strlen(Contents)>1000)
    {
        isFlag = false;
        fieldText = fieldText + "投诉内容|";
   		msgText = msgText + "投诉内容最多只允许输入1000个字符|";
    }
    if(strlen(DisposalProcess)>1000)
    {
        isFlag = false;
        fieldText = fieldText + "处理过程|";
   		msgText = msgText + "处理过程最多只允许输入1000个字符|";
    }
    if(strlen(Feedback)>1000)
    {
        isFlag = false;
        fieldText = fieldText + "客户反馈|";
   		msgText = msgText + "客户反馈最多只允许输入1000个字符|";
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
    if(ComplainType == "" || ComplainType == "0")
    {
        isFlag = false;
        fieldText = fieldText + "投诉类型|";
   		msgText = msgText +  "请首先配置投诉类型|";
    }
    
    
    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
        //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msgText);        
    }
    return isFlag;
}