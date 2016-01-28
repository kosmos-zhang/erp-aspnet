var isnew="1";//1添加;2保存

var page = "";
var CustName;
var CustID;
var LoveType;
var CustLinkMan ;
var LoveBegin ;
var LoveEnd;
var Title;
var currentPageIndex;
var pageCount;

var ListType;
var custID;
var custNo;

$(document).ready(function()
{
    requestobj = GetRequest();
    recordnoparam = requestobj['Loveid'];
    page = requestobj['Pages'];
    CustName = requestobj['CustName'];
    CustID = requestobj['CustID'];
    LoveType = requestobj['LoveType'];
    CustLinkMan = requestobj['CustLinkMan'];
    LoveBegin = requestobj['LoveBegin'];
    LoveEnd = requestobj['LoveEnd'];
    Title = requestobj['Title'];
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
        $("#hfLoveID").attr("value",recordnoparam);//ID记录在隐藏域中
        //显示返回按钮
        $("#btn_back").show();
        GetLoveInfo(recordnoparam);
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
        case"CustTalk_Info.aspx":
            window.location.href=page+'?CustName='+CustName+'&LoveType='+LoveType+'&LoveBegin='+LoveBegin+'&LoveEnd='+LoveEnd+'&Title='+Title+'&CustID='+CustID+
                '&CustLinkMan='+CustLinkMan+'&currentPageIndex='+currentPageIndex+'&pageCount='+pageCount+'&ModuleID=2021502';
        break;
        case"CustColligate.aspx":
            window.location.href=page+'?ListType='+ListType+'&custID='+custID+'&custNo='+custNo+'&ModuleID=2022101';
        break;
    }
   
}

function PagePrint()
{
     var Url = document.getElementById("hfLoveID").value;
     if(Url == "")
     {
        popMsgObj.Show("打印|","请保存单据再打印|");
        return;
     }
//     if(confirm("请确认您的单据已经保存？"))
//     {
        window.open("../../PrinttingModel/CustManager/LovePrint.aspx?id=" + Url);
//     }
}

function GetLoveInfo(LoveID)
{
     $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:"../../../Handler/Office/CustManager/LoveEdit.ashx",//目标地址
       data:'id='+reescape(LoveID),
       cache:false,
       beforeSend:function(){AddPop();},//发送数据之前           
       success: function(msg){
                //数据获取完毕，填充页面据显示
                $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                        $("#hfCustID").attr("value",item.CustID);//客户ID
                        $("#hfCustNo").attr("value",item.CustNo);//客户编号                       
                        $("#txtUcCustName").attr("value",item.custnam);//客户简称
                        $("#hfLinkmanID").attr("value",item.CustLinkMan);//联系人ID
                        $("#txtUcLinkMan").attr("value",item.linkmanname);//联系人名
                        $("#hfLinker").attr("value",item.Linker);//执行人ID                        
                        $("#UserLinker").attr("value",item.employeename);//执行人名
                        $("#txtTitle").attr("value",item.Title);//关怀主题
                        document.getElementById("divddlLoveNo").style.display = "none";//隐藏客户关怀单编号选择
                        document.getElementById("divLoveNo").style.display = "block";
                        document.getElementById("divLoveNo").innerHTML = item.LoveNo;//显示关怀单编号                        
                        
                        //$("#txtLoveDate").attr("value",item.LoveDate);//日期                        
                        $("#txtLoveDate").val(item.LoveDate.slice(0,10));//日期
                        $("#SeleHour").val(item.LoveDate.slice(11,14));//时
                        $("#SeleMinute").val(item.LoveDate.slice(14,16));//分
                        
                        $("#ddlLoveType").val(item.LoveType);//关怀类型
                        $("#txtModifiedDate").attr("value",item.ModifiedDate);//最后更新日期
                        $("#txtModifiedUserID").attr("value",item.ModifiedUserID);//最后更新用户
                        $("#txtContents").attr("value",item.Contents);//关怀内容
                        $("#txtFeedback").attr("value",item.Feedback);//客户反馈
                        $("#txtRemark").attr("value",item.remarks);//备注
                        
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
function AddCustLove()
{
    if(!CheckInput())
    {
        return;
    }
    //禁用保存按钮，防止2次提交
    var CustID = document.getElementById("hfCustID").value;//客户ID
    var CustLinkMan = document.getElementById("hfLinkmanID").value;//客户联系人    
    var Linker = document.getElementById("hfLinker").value;//我方接待人
    var Title = document.getElementById("txtTitle").value;//关怀主题 
    var LoveNoType = document.getElementById("ddlLoveNo_ddlCodeRule").value;
    var LoveNo = "";
    //获取编码规则下拉列表选中项  如果选中的是 手工输入时，校验编号是否输入
    if (LoveNoType == "")
    {
        LoveNo = $("#ddlLoveNo_txtCode").val(); //投诉单编号
    }
    if(isnew == "2")
    {
        LoveNo = document.getElementById("divLoveNo").innerText;
    }
    
    var StartH =  $("#SeleHour").val();
    var SeleM =  $("#SeleMinute").val();    
    var LoveDate = $("#txtLoveDate").val() + " " + StartH + SeleM;//关怀时间
    
    var LoveType = $("#ddlLoveType").val();//关怀类型
    //var ModifiedDate = $("#txtModifiedDate").val();//最后更新日期
    //var ModifiedUserID = $("#txtModifiedUserID").val();//最后更新用户
    var Contents =  $("#txtContents").val();//关怀内容
    var Feedback =  $("#txtFeedback").val();//客户反馈
    var Remark = $("#txtRemark").val();//备注
     var CanViewUser = $("#txtCanUserID").val();
    var CanViewUserName = $("#txtCanUserName").val(); 

    var ID = document.getElementById("hfLoveID").value;//第一次保存后返回的关怀ID

    $.ajax({
        type:"POST",
        url: "../../../Handler/Office/CustManager/LoveAdd.ashx",
        data: 'CustID='+reescape(CustID)+
            '&CustLinkMan='+reescape(CustLinkMan)+
            '&Linker='+reescape(Linker)+
            '&Title='+reescape(Title)+
            '&LoveNoType='+reescape(LoveNoType)+ 
            '&LoveNo='+reescape(LoveNo)+
            '&LoveType='+reescape(LoveType)+
            '&LoveDate='+reescape(LoveDate)+
            '&CanViewUser='+reescape(CanViewUser)+
            '&CanViewUserName='+reescape(CanViewUserName)+
            '&Contents='+reescape(Contents)+
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
                     document.getElementById("hfLoveID").value = data.data;
                     document.getElementById("divddlLoveNo").style.display = "none";//隐藏客户关怀单编号选择
                     document.getElementById("divLoveNo").style.display = "block";
                     document.getElementById("divLoveNo").innerHTML = data.info;
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
    var txtUcLinkMan = document.getElementById('txtUcLinkMan').value;//客户联络人姓名
    var hfLinkmanID = document.getElementById('hfLinkmanID').value;//客户联络人ID
    var UserLinker = document.getElementById('UserLinker').value;//执行人姓名
    var hfLinkerID = document.getElementById('hfLinker').value;//执行人ID
    var txtTitle = document.getElementById('txtTitle').value;//关怀时主题    
    var LoveNoType = document.getElementById("ddlLoveNo_ddlCodeRule").value;
    var LoveNo = document.getElementById("ddlLoveNo_txtCode").value;
    var txtLoveDate = document.getElementById("txtLoveDate").value;//执行日期
    
    var Contents = document.getElementById("txtContents").value;//关怀内容
    var Feedback = document.getElementById("txtFeedback").value;//客户反馈
    var Remark = document.getElementById("txtRemark").value;//备注    
    var RetVal=CheckSpecialWords();
    var LoveType = document.getElementById("ddlLoveType").value;//关怀类型
    
    //获取编码规则下拉列表选中项  如果选中的是 手工输入时，校验编号是否输入
    if (isnew == "1" && LoveNoType == "" && LoveNo =="")
    {
       isFlag = false;
       fieldText = fieldText + "关怀单编号|";
   		msgText = msgText +  "请输入关怀单编号|";
    }    
     if(isnew == "1" && LoveNoType == "" && LoveNo != "")
    {
        if(!CodeCheck(LoveNo))
        {
            isFlag = false;
           fieldText = fieldText + "关怀单编号|";
   		    msgText = msgText +  "编号只能由英文字母（a-z大小写均可）、数字（0-9）、字符（_-/.()[]{}）组成|";
        }
    }
    if(txtCustNam=="" || hfCustID == "")
    {
        isFlag = false;
        fieldText = fieldText + "关怀客户|";
   		msgText = msgText +  "请选择关怀客户|";
    }
    if(txtUcLinkMan=="" || hfLinkmanID == "")
    {
        isFlag = false;
         fieldText = fieldText + "客户联系人|";
   		msgText = msgText +  "请选择客户联系人|";
    }
    
    if(UserLinker =="" || hfLinkerID == "")
    {
        isFlag = false;
        fieldText = fieldText + "执行人|";
   		msgText = msgText +  "请选择执行人|";
    }
    if(txtTitle.Trim() == "")
    {
        isFlag = false;
        fieldText = fieldText + "关怀主题|";
   		msgText = msgText +  "请输入关怀主题|";
    }
    if(txtLoveDate == "")
    {
        isFlag = false;
        fieldText = fieldText + "执行日期|";
   		msgText = msgText +  "请输入执行日期|";
    }
    
    if(strlen(Contents) > 1000)
    {
        isFlag = false;
        fieldText = fieldText + "关怀内容|";
   		msgText = msgText + "关怀内容最多只允许输入1000个字符|";
    }
    if(strlen(Feedback) > 1000)
    {
        isFlag = false;
         fieldText = fieldText + "客户反馈|";
   		msgText = msgText + "客户反馈最多只允许输入1000个字符|";
    }
    if(strlen(Remark) > 1000)
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
    if(LoveType=="" || LoveType=="0")
    {
        isFlag = false;
        fieldText = fieldText + "关怀类型|";
	    //msgText = msgText +RetVal+  "关怀类型不能为空，请先在分类属性设置中进行相关设置|";
	    msgText = msgText +RetVal+  "请首先配置关怀类型|";
    }
   
    if(!isFlag)
    {
        //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msgText);        
        popMsgObj.Show(fieldText,msgText);
    }
    return isFlag;
}