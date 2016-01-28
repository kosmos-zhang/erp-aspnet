var ListType;
var custID;
var custNo;

$(document).ready(function()
{
    requestQuaobj = GetRequest();   
     var action=requestQuaobj['myAction'];
     var from=requestQuaobj['from'];
     
     ListType = requestQuaobj['ListType'];
    custID = requestQuaobj['custID'];
    custNo = requestQuaobj['custNo'];
    
    if(action=='edit' || from=='list')
    {
        document.getElementById('btnReturn').style.display='';
        
    }
    if(CustAdviceID>0)
    { 
        LoadCustInfo(CustAdviceID);
    }
    else
    {
        
    }   

});

function LoadCustInfo(CustID)
{      
  
       var rowsCount=0;
       $.ajax({
       type: "POST",//用POST方式传输ss
       dataType:"json",//数据格式:JSON
       url:"../../../Handler/Office/CustManager/CustAdviceInfo.ashx?ID="+CustID,//目标地址
       cache:false,          
       success: function(msg){
                var rowsCount = 0;
                var countTotals = 0;
                //数据获取完毕，填充页面据显示

                    if(typeof(msg.dataCustAdvice)!='undefined')
                    {
                       
                        $.each(msg.dataCustAdvice,function(i,item)           
                        {
                            document.getElementById('hiddenCustAdviceID').value=item.ID;
                            document.getElementById('lbInfoNo').value=item.AdviceNo;
                            document.getElementById('divCodeRuleUC').style.display='none';
                            document.getElementById('divTaskNo').style.display='';
                            
                            document.getElementById('txtTitle').value=item.Title;
                            document.getElementById('txtAdvicer').value=item.Advicer;
                            document.getElementById('hfCustID').value=item.CustID;
                            document.getElementById('txtUcCustName').value=item.CustName;
                            document.getElementById('hfLinkmanID').value=item.CustLinkMan;
                            document.getElementById('txtUcLinkMan').value=item.LinkManName;
                            
                            document.getElementById("hfCustNo").value = item.CustNo;
                                                        
                            document.getElementById('hiddenDestClerk').value=item.DestClerk;
                            document.getElementById('UserDestClerk').value=item.EmployeeName;
                            
                            document.getElementById('txtLeadSay').value=item.LeadSay;
                            document.getElementById('txtDoSomething').value=item.DoSomething;
                            document.getElementById('txtAccept').value=item.Accept;
                        
                            document.getElementById('txtAdviceDate').value=item.AdviceDate.toString().substr(0,11);
                            document.getElementById('DropDownList1').value=item.AdviceDate.toString().substr(11,2);
                            document.getElementById('DropDownList2').value=item.AdviceDate.toString().substr(14,2);
                            
                            document.getElementById('txtAdviceType').value=item.AdviceType;
                            document.getElementById('txtState').value=item.State;
                            document.getElementById('txtContents').value=item.Contents;
                            document.getElementById('txtRemark').value=item.Remark; 
                            
                            document.getElementById('tbCreater').value=item.CreatorName;
                            document.getElementById('txtCreateDate').value=item.CreatedDate;
                            
                            document.getElementById('txtModifiedDate').value=item.ModifiedDate;
                            document.getElementById('txtModifiedUserID').value=item.ModifiedUserID;
                            
                            var aaa = item.CanViewUser.replace(",","");
                            var bbb = aaa.lastIndexOf(",");
                            var ccc = aaa.slice(0,bbb);                        
                            $("#txtCanUserID").val(ccc);
                            $("#txtCanUserName").val(item.CanViewUserName);                          

                        });
                    }
              },
       error: function() {alert('加载数据时发生请求异常');}, 
       complete:function(){}
       });
}

//-------------------------保存Start
function Fun_Save_Cust()
{
  if(CheckInput())
  {
    var UrlParam='';
    var myAction='add';
    var myMethod=document.getElementById('hiddenCustAdviceID').value;   //该单据ID
    var AdviceNo='';
    var bmgz='';
    if(myMethod=='0')  // 新建
    {
          if($("#checkNo_ddlCodeRule").val()=="")//手工输入
           {
                AdviceNo=$("#checkNo_txtCode").val();
                bmgz="sd";
           }
         else
           {
                AdviceNo=$("#checkNo_ddlCodeRule").val();
                bmgz="zd";
           }
    }
    else   //编辑
    {
            AdviceNo=document.getElementById('lbInfoNo').value;
            myAction='edit';
    }
    var Title=document.getElementById('txtTitle').value;
    var txtAdvicer=document.getElementById('txtAdvicer').value;
    var CustID=document.getElementById('hfCustID').value;
    var CustLinkMan=document.getElementById('hfLinkmanID').value;
    var DestClerk=document.getElementById('hiddenDestClerk').value;
    var LeadSay=$("#txtLeadSay").val();
    var DoSomething=$("#txtDoSomething").val();
    var txtAccept=document.getElementById('txtAccept').value;
    var txtAdviceDate=document.getElementById('txtAdviceDate').value+" "+document.getElementById('DropDownList1').value+":"+document.getElementById('DropDownList2').value;
    var txtAdviceType=document.getElementById('txtAdviceType').value;
    var txtState=document.getElementById('txtState').value;
    var txtContents=$("#txtContents").val();
    var txtRemark=$("#txtRemark").val();
    var CanViewUser = $("#txtCanUserID").val();
    var CanViewUserName = $("#txtCanUserName").val();     
                                    
UrlParam="Title="+escape(Title)+   
         "&AdviceNo="+escape(AdviceNo)+                                       
        "&txtAdvicer="+escape(txtAdvicer) + 
        "&CustID="+escape(CustID)+   
        "&CustLinkMan="+escape(CustLinkMan)+         
        "&DestClerk="+escape(DestClerk) + 
        "&LeadSay="+escape(LeadSay)+   
        "&DoSomething="+escape(DoSomething)+     
        "&txtAccept="+escape(txtAccept) + 
        "&txtAdviceDate="+escape(txtAdviceDate)+   
        "&txtAdviceType="+escape(txtAdviceType)+   
        "&txtState="+escape(txtState) + 
        "&txtContents="+escape(txtContents)+   
        "&txtRemark="+escape(txtRemark)+  
        "&myAction="+escape(myAction)+
        "&bmgz="+escape(bmgz)+
         '&CanViewUser='+reescape(CanViewUser)+
        '&CanViewUserName='+reescape(CanViewUserName)+
        "&ID="+myMethod;                                            

        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/CustManager/CustAdviceAdd.ashx",
                  dataType:'json',//返回json格式数据
                  cache:false,
                  data:UrlParam,
                  beforeSend:function()
                  { 
                  }, 
                  error: function() 
                  {
                    popMsgObj.ShowMsg('保存分店客户时请求发生错误');
                  }, 
                  success:function(data) 
                  { 
                     var reInfo=data.data.split('|');

                     if(reInfo.length > 1)
                       {   
                            popMsgObj.ShowMsg(data.info);
                            document.getElementById('lbInfoNo').value = reInfo[1];     
                            document.getElementById('divCodeRuleUC').style.display='none';                       
                            document.getElementById('divTaskNo').style.display=''; 
                            document.getElementById('hiddenCustAdviceID').value=reInfo[0];
             
                      }
                    else
                    {
                        popMsgObj.ShowMsg(data.info);
                    }
                                     } 
              }); 
    }
}
function CheckInput()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    
    var Title=document.getElementById('txtTitle').value;
    var txtAdvicer=document.getElementById('txtAdvicer').value;//提出建议人
    var CustID=document.getElementById('hfCustID').value;//提出建议客户
    var CustLinkMan=document.getElementById('hfLinkmanID').value;//客户联系人
    var DestClerk=document.getElementById('hiddenDestClerk').value;//接待人

    var txtAccept=document.getElementById('txtAccept').value;//采纳程度
    var txtAdviceDate=document.getElementById('txtAdviceDate').value;//建议时间
    var txtAdviceType=document.getElementById('txtAdviceType').value;//建议类型
    var txtState=document.getElementById('txtState').value;//状态
    var txtContents=document.getElementById('txtContents').value;//建议内容
    var txtLeadSay=document.getElementById('txtLeadSay').value;//领导意见 
    var txtDoSomething=document.getElementById('txtDoSomething').value;//对应措施
    var txtRemark=document.getElementById('txtRemark').value;
        //获取编码规则下拉列表选中项
    codeRule = document.getElementById("checkNo_ddlCodeRule").value; 
    //如果选中的是 手工输入时，校验编号是否输入
    if (codeRule == "")
    {
        //获取输入的编号
        txtAdviceNo = document.getElementById("checkNo_txtCode").value;
        //编号必须输入     
        if(parseFloat(document.getElementById('hiddenCustAdviceID').value)<1)
        {
            if (txtAdviceNo == "")
            {
                isFlag = false;
                fieldText += "建议编号|";
	            msgText += "请输入建议编号|";
            }
                    else
            {
                if (isnumberorLetters(txtAdviceNo))
                {
                    isFlag = false;
                    fieldText += "建议编号|";
                    msgText += "建议编号只能包含字母或数字！|";
                }
            }
        }
    }
        if(strlen(Title)<=0)
        {
            isFlag = false;
            fieldText = fieldText + "建议主题|";
   		    msgText = msgText +  "请输入建议主题 |";
        }
        if(strlen(Title)>0)
        {
            if(trim(Title)=='')
            {
                isFlag = false;
                fieldText = fieldText + "建议主题|";
   		        msgText = msgText +  "不能输入空建议主题 |";
            }
        }
     if(strlen(Title)>100)
        {
         isFlag = false;
         fieldText = fieldText + "建议主题|";
   		 msgText = msgText + "建议主题最多只允许输入100个字符符|";
        }
//        if(strlen(txtAdvicer)<=0)
//        {
//            isFlag = false;
//            fieldText = fieldText + "提出建议人|";
//   		    msgText = msgText +  "请输入提出建议人 |";
//        }
        if(strlen(document.getElementById('txtUcCustName').value)<=0)        
        {
             isFlag = false;
            fieldText = fieldText + "提出建议客户|";
   		    msgText = msgText +  "请输入提出建议客户 |";
        }
//                if(strlen(document.getElementById('txtUcLinkMan').value)<=0)        
//        {
//             isFlag = false;
//            fieldText = fieldText + "客户联系人|";
//   		    msgText = msgText +  "请输入客户联系人 |"; 
//        }
//        if(document.getElementById('hiddenDestClerk').value=='0')        
//        {
//             isFlag = false;
//            fieldText = fieldText + "接待人|";
//   		    msgText = msgText +  "请输入接待人 |";
//        }

   if(strlen(txtAdvicer)>20)
    {
        isFlag = false;
        fieldText = fieldText + "提出建议人|";
   		msgText = msgText + "提出建议人最多只允许输入20个字符符|";
    }
    if(strlen(txtDoSomething)>500)
    {
        isFlag = false;
        fieldText = fieldText + "对应措施|";
   		msgText = msgText + "对应措施最多只允许输入500个字符符|";
    }
        if(strlen(txtLeadSay)>500)
    {
        isFlag = false;
        fieldText = fieldText + "领导意见|";
   		msgText = msgText + "领导意见最多只允许输入500个字符符|";  
    }
            if(strlen(txtRemark)>1000)
    {
        isFlag = false;
        fieldText = fieldText + "备注|";
   		msgText = msgText + "备注最多只允许输入1000个字符符|";  
    }
        if(strlen(txtAdviceDate)<=0)
        {
            isFlag = false;
            fieldText = fieldText + "建议时间|";
   		    msgText = msgText +  "请输入建议时间 |";
        }
        if(strlen(txtContents)<=0)
        {
            isFlag = false;
            fieldText = fieldText + "建议内容|";
   		    msgText = msgText +  "请输入建议内容 |";
        }
                if(strlen(txtContents)>1000)
    {
        isFlag = false;
        fieldText = fieldText + "建议内容|";
   		msgText = msgText + "建议内容最多只允许输入1000个字符符|";
    }
            if(!CheckSpecialWord(Title))
        {
            isFlag = false;
            fieldText = fieldText + "主题|";
   		    msgText = msgText +  "主题不能含有特殊字符 |";
        }
        
        if(!CheckSpecialWord(txtContents))
        {
            isFlag = false;
            fieldText = fieldText + "建议内容|";
   		    msgText = msgText +  "建议内容不能含有特殊字符 |";
        }
        if(!CheckSpecialWord(txtAdvicer))
        {
            isFlag = false;
            fieldText = fieldText + "提出建议人|";
   		    msgText = msgText +  "提出建议人不能含有特殊字符 |";
        }
               if(!CheckSpecialWord(txtLeadSay))
        {
            isFlag = false;
            fieldText = fieldText + "领导意见|";
   		    msgText = msgText +  "领导意见不能含有特殊字符 |";
        }
        
        
        
                if(!CheckSpecialWord(txtDoSomething))
        {
            isFlag = false;
            fieldText = fieldText + "对应措施|";
   		    msgText = msgText +  "对应措施不能含有特殊字符 |";
        }
        
                        if(!CheckSpecialWord(txtRemark))
        {
            isFlag = false;
            fieldText = fieldText + "备注|";
   		    msgText = msgText +  "备注不能含有特殊字符 |";
        }
   
    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
    }
    return isFlag;
}

