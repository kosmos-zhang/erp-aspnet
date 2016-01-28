var isnew="1";//1添加;2保存

var page ;
var CustName; //检索条件 客户简称
var CustID;
var CustLinkMan ;//bei联络人
var LinkDateBegin ;//联络开始时间
var LinkDateEnd;//联络结束时间 
var pageCount;
var currentPageIndex;

var ListType;
var custID;
var custNo;

$(document).ready(function()
{   
    requestobj = GetRequest();
    recordnoparam = requestobj['contactid'];    
    page = requestobj['Pages'];    
    
    CustName = requestobj['CustName'];
    CustID = requestobj['CustID'];
    CustLinkMan = requestobj['CustLinkMan'];
    LinkDateBegin = requestobj['LinkDateBegin'];
    LinkDateEnd = requestobj['LinkDateEnd'];
    pageCount = requestobj['pageCount'];
    currentPageIndex = requestobj['currentPageIndex'];
    
     ListType = requestobj['ListType'];
     custID = requestobj['custID'];
     custNo = requestobj['custNo'];
    
    
    DealPage(recordnoparam);
});

//处理加载页面
function DealPage(recordnoparam)
{
    if(typeof(recordnoparam)!="undefined" && recordnoparam!= "-1")
    {    
        isnew="2";
        IsModify();
        $("#hfCustContactID").attr("value",recordnoparam);

        //显示返回按钮
        $("#btn_back").show();
        GetContactInfo(recordnoparam);
    }
    if(recordnoparam == "-1")
    {
        //显示返回按钮
        $("#btn_back").show();
    }
}

//当修改时，表头显示
function IsModify()
{
     $("#TitleModify").show();
     $("#TitleAdd").hide();
}

//返回
function Back()
{
    switch(page)
    {
        case "CustContact_Info.aspx":
         window.location.href=page+'?CustName='+CustName+'&CustLinkMan='+CustLinkMan+'&LinkDateBegin='+LinkDateBegin+
                '&LinkDateEnd='+LinkDateEnd+'&currentPageIndex='+currentPageIndex+
                '&pageCount='+pageCount+'&CustID='+CustID+'&ModuleID=2021302';
        break;
        
        case "CustColligate.aspx":
         window.location.href=page+'?ListType='+ListType+'&custID='+custID+'&custNo='+custNo+'&ModuleID=2022101';
        break;
    }
}

function PagePrint()
{
     var Url = document.getElementById("hfCustContactID").value;
     if(Url == "")
     {
        popMsgObj.Show("打印|","请保存单据再打印|");
        return;
     }
//    if(confirm("请确认您的单据已经保存？"))
//    {
        window.open("../../PrinttingModel/CustManager/ContactPrint.aspx?id=" + Url);
//    }
}

  //////获取客户联络信息(修改、查看)
 function GetContactInfo(contactid)
{
       var retval = contactid;
       $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:"../../../Handler/Office/CustManager/ContactEdit.ashx",//目标地址
       data:'id='+reescape(retval),
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
                        $("#hfEmployeeID").attr("value",item.Linker);//我方联络人ID
                        $("#UserEmplNameL").attr("value",item.LinkerName);//我方联络人名                         
                        $("#txtTitle").attr("value",item.Title);//联络主题
                        document.getElementById("divddlContactNo").style.display = "none";//隐藏客户联络单编号选择
                        document.getElementById("divContacytNo").style.display = "block";
                        document.getElementById("divContacytNo").innerHTML = item.ContactNO;//显示联络单编号                        
                        $("#ddlLinkReasonID").val(item.LinkReasonID);//联络缘由
                        $("#ddlLinkMode").attr("value",item.LinkMode);//联络方式                       
                        $("#txtLinkDate").attr("value",item.LinkDate);//联络日期
                        $("#txtContents").attr("value",item.Contents);//联络内容
                        
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

function ShowLinkMan(LinkmanID,CustLinkMan)
{
    ShowLinkManList(document.getElementById("hfCustNo").value,LinkmanID,CustLinkMan);
}

function AddContactHistoryData()
{
    if(!CheckInput())
    {
        return;
    }
    var CustID = $("#hfCustID").val();//客户ID
    var CustLinkMan = $("#hfLinkmanID").val(); //客户联系人ID
    var Title = $("#txtTitle").val();
    
    var ContactNoType = document.getElementById("ddlContactNo_ddlCodeRule").value;
    var ContactNo = "";
    //获取编码规则下拉列表选中项  如果选中的是 手工输入时，校验编号是否输入
    if (ContactNoType == "")
    {
        ContactNo = $("#ddlContactNo_txtCode").val(); //客户联络单编号
    }
    
    var LinkReasonID = $("#ddlLinkReasonID").val();
    var LinkMode = $("#ddlLinkMode").val();    
    var LinkDate = $("#txtLinkDate").val();    
    var Linker = $("#hfEmployeeID").val(); //我方联系人ID
    var Contents = $("#txtContents").val();
    var CanViewUser = $("#txtCanUserID").val();
    var CanViewUserName = $("#txtCanUserName").val(); 
    var ID = document.getElementById("hfCustContactID").value;//第一次保存后返回的联络ID
    
    $.ajax({
        type:"POST",
        url: "../../../Handler/Office/CustManager/ContactHistory.ashx",
        data:'CustID='+reescape(CustID)+
            '&CustLinkMan='+reescape(CustLinkMan)+
            '&Title='+reescape(Title)+
            '&ContactNoType='+reescape(ContactNoType)+
            '&ContactNo='+reescape(ContactNo)+
            '&LinkReasonID='+reescape(LinkReasonID)+
            '&LinkMode='+reescape(LinkMode)+   
            '&LinkDate='+reescape(LinkDate)+   
            '&Linker='+reescape(Linker)+
            '&Contents='+reescape(Contents)+
            '&CanViewUser='+reescape(CanViewUser)+
            '&CanViewUserName='+reescape(CanViewUserName)+
            '&id='+reescape(ID)+
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
                     
                     document.getElementById("hfCustContactID").value = data.data;
                     document.getElementById("divddlContactNo").style.display = "none";//隐藏客户联络单编号选择
                     document.getElementById("divContacytNo").style.display = "block";
                     if(isnew == "1")
                     {
                        document.getElementById("divContacytNo").innerHTML = data.info;
                     }                     
                     isnew="2";
                     
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
                  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","添加失败,请确认！");
                } 
            }
      });
}
//})

//主表单验证
function CheckInput()
{
var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var txtCustNam = document.getElementById('txtUcCustName').value;//客户简称
    var hfCustID = document.getElementById('hfCustID').value;//客户ID
    var txtEmplNameL = document.getElementById('UserEmplNameL').value;//我方联络人姓名
    var hfEmployeeID = document.getElementById('hfEmployeeID').value;//我方联络人ID
    var txtTitle = document.getElementById('txtTitle').value;//联络人标题
    var txtLinkDate = document.getElementById('txtLinkDate').value;//联络人主题
    var NoType = document.getElementById("ddlContactNo_ddlCodeRule").value;
    var No = document.getElementById("ddlContactNo_txtCode").value; 
    var RetVal=CheckSpecialWords();
    var LinkReasonID = document.getElementById("ddlLinkReasonID").value;//联络事由
    var LinkMode = document.getElementById("ddlLinkMode").value;//联络方式
    
    if (isnew == "1" && NoType == "" && No =="")
    {
       isFlag = false;
       fieldText = fieldText + "联络单编号|";
   		msgText = msgText +  "请输入联络单编号|";
    } 
    if(isnew == "1" && NoType == "" && No != "")
    {
        if(!CodeCheck(No))
        {
            isFlag = false;
           fieldText = fieldText + "联络单编号|";
   		    msgText = msgText +  "编号只能由英文字母（a-z大小写均可）、数字（0-9）、字符（_-/.()[]{}）组成|";
        }
    }
    if(txtCustNam=="" || hfCustID == "")
    {
        isFlag = false;
        fieldText = fieldText + "对应客户|";
   		msgText = msgText +  "请选择对应客户|";
    } 

    if(txtEmplNameL=="" || hfEmployeeID == "")
    {
        isFlag = false;
         fieldText = fieldText + "我方联络人|";
   		msgText = msgText +  "请选择我方联络人|";
    }
    if(txtTitle.Trim() == "")
    {
        isFlag = false;
        fieldText = fieldText + "联络主题|";
   		msgText = msgText +  "请输入联络主题|";
    }
    if(txtLinkDate == "")
    {
        isFlag = false;
        fieldText = fieldText + "联络时间|";
   		msgText = msgText +  "请输入联络时间|";
    }
    if(txtLinkDate.length>0)
    {
        if(!isDate(txtLinkDate))
        {
            isFlag = false;
            fieldText = fieldText + "联络时间|";
   		     msgText = msgText + "联络时间格式不正确|";    
        }
    }
    if(RetVal!="")
    {
            isFlag = false;
            fieldText = fieldText + RetVal+"|";
   		    msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
    if(LinkReasonID == "")
    {
        isFlag = false;
        fieldText = fieldText + "联络事由|";
   		msgText = msgText +  "请选择联络事由|";
    }
    if(LinkMode == "")
    {
        isFlag = false;
        fieldText = fieldText + "联络方式|";
   		msgText = msgText +  "请选择联络方式|";
    }
    
    var Contents = document.getElementById("txtContents").value;//联络内容    
    if(strlen(Contents) > 1000)
    {
        isFlag = false;
        fieldText = fieldText + "联络内容|";
   		msgText = msgText + "联络内容最多只允许输入1000个字符|";
    }
    
    if(!isFlag)
    {
        //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msgText);        
        popMsgObj.Show(fieldText,msgText);
    }
    return isFlag;
}
