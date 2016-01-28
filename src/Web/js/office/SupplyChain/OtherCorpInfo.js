
//===============================================数 据 处 理============================================
$(document).ready(function()
{
    if(intOtherCorpInfoID>0)
    {
//     document.all.txt_CustNo.readOnly=true;  
     document.getElementById('txt_CustNo').readOnly=true;  
    }
    else
    {
    }
});
///验证电话号码
//只允许输入0-9和‘-’号   
// 任何一个字符不符合返回true                       
function isvalidtel(inputs) //校验电话号码    //add by taochun
{ 
var i,temp; 
var isvalidtel = false; 
inputstr=trim(inputs); 
if(inputstr.length==null||inputstr.length==0) return true; 
for(i=0;i<inputstr.length;i++) 
{ 
temp=inputstr.substring(i,i+1); 
if(!(temp>="0" && temp<="9" || temp=="-")) 
{ 
isvalidtel=true; 
break; 
} 
} 
return isvalidtel; 
} 

function checkMobile( s ){   
var regu =/^[1]([3][0-9]{1}|59|58)[0-9]{8}$/;
var re = new RegExp(regu);
if (re.test(s)) {
   return true;
}else{
   return false;
}
}
function Fun_Save_OtherCorpInfo()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    requestobj = GetRequest();
    recordnoparam = requestobj['intOtherCorpInfoID'];
    if(typeof(recordnoparam)=="undefined")
    {
     Action = 'Add';
     if(!document.getElementById('txt_CustNo').readOnly)
     {
    if( document.getElementById('txtPlanNoHidden').value=="0")
    {
      isFlag = false;
      fieldText = fieldText +  "单位编号|";
      msgText = msgText + "往来单位编号已经存在|"; 
    }
     }
    }
    else
    {
      Action = 'Edit';
    }
    glb_BillID = document.getElementById('txtIndentityID').value;
    var BigType=$("#sel_BigType").val();              
    var CustNo=trim($("#txt_CustNo").val());                
    var CustName=trim($("#txt_CustName").val());            
    var CorpNam=trim($("#txt_CorpNam").val());              
    var PYShort=$("#txt_PYShort").val();              
    var CustNote=$("#txt_CustNote").val();            
    var AreaID=$("#sel_AreaID").val();                
    var CompanyType=trim($("#txt_CompanyType").val());      
    var StaffCount=$("#txt_StaffCount").val();        
    var SetupDate=$("#txt_SetupDate").val();          
    var ArtiPerson=$("#txt_ArtiPerson").val();        
    var SetupMoney=$("#txt_SetupMoney").val();        
    var SetupAddress=$("#txt_SetupAddress").val();    
    var CapitalScale=$("#txt_CapitalScale").val();    
    var SaleroomY=$("#txt_SaleroomY").val();          
    var ProfitY=$("#txt_ProfitY").val();              
    var TaxCD=$("#txt_TaxCD").val();                  
    var BusiNumber=$("#txt_BusiNumber").val();        
    var SellArea=$("#txt_SellArea").val();            
    var CountryID=$("#sel_CountryID").val();          
    var Province=$("#txt_Province").val();            
    var City=$("#txt_City").val();                    
    var Post=$("#txt_Post").val();                    
    var ContactName=$("#txt_ContactName").val();      
    var Tel=$("#txt_Tel").val();                      
    var Fax=$("#txt_Fax").val();                      
    var Mobile=$("#txt_Mobile").val();                
    var email=trim($("#txt_email").val());                  
    var Addr=$("#txt_Addr").val();                    
    var BillType=$("#sel_BillType").val();            
    var PayType=$("#sel_PayType").val();              
    var MoneyType=$("#sel_MoneyType").val();          
    var CurrencyType=$("#sel_CurrencyType").val();    
    var Remark=$("#txt_Remark").val();                
    var UsedStatus=$("#sel_UsedStatus").val();        
    var Creator=$("#txtPrincipal").val();              
    var CreateDate=$("#txt_CreateDate").val();        
      var isTax = document.getElementById('chk_isTax').checked ? "1" : "0"; 
    if( document.getElementById('txtIndentityID').value=="1")
    {
     Action="Edit";

    }
//    //获取编码规则下拉列表选中项
//    codeRule = document.getElementById("codruleMasterProductSchedule_ddlCodeRule").value;
//    //如果选中的是 手工输入时，校验编号是否输入
//    if (codeRule == "")
//    {
//        //获取输入的编号
//        txtPlanNo = document.getElementById("codruleMasterProductSchedule_txtCode").value;
//        //编号必须输入
//        if (txtPlanNo == "")
//        {
//            isFlag = false;
//            fieldText += "单据编号|";
//	        msgText += "请输入单据编号|";
//        }
//        else
//        {
//            if (isnumberorLetters(txtPlanNo))
//            {
//                isFlag = false;
//                fieldText += "单据编号|";
//                msgText += "单据编号只能包含字母或数字！|";
//            }
//        }
//    }

     if(CustNo=="")
     {
        isFlag = false;
        fieldText += "往来单位编号|";
        msgText += "请输入往来单位编号|";
     }
     else
    {
      if(!CodeCheck(CustNo))
        {
            isFlag = false;
           fieldText = fieldText + "往来单位编号|";
   		    msgText = msgText +  "往来单位编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]|";
       }
    }
      if(strlen(CustNo)>50)
     {
        isFlag = false;
        fieldText += "往来单位编号|";
        msgText += "往来单位编号仅限于50个字符以内|";
     }
    if(CustName=="")
     {
        isFlag = false;
        fieldText += "往来单位名称|";
        msgText += "请输入单位名称|";
     }
      if(strlen(CustName)>100)
     {
        isFlag = false;
        fieldText += "往来单位名称|";
        msgText += "往来单位名称仅限于100个字符以内|";
     }
       if(CorpNam=="")
     {
        isFlag = false;
        fieldText += "往来单位简称|";
        msgText += "请输入单位简称|";
     }
      if(strlen(CorpNam)>50)
     {
        isFlag = false;
        fieldText += "往来单位简称|";
        msgText += "往来单位简称仅限于50个字符以内|";
     }
        if(CompanyType!="")
      if(strlen(CompanyType)>50)
     {
        isFlag = false;
        fieldText += "单位性质|";
        msgText += "单位性质仅限于50个字符以内|";
     }
     if(Creator=="")
     {
        isFlag = false;
        fieldText += "建档人|";
        msgText += "请选择建档人|";
     }
     if(StaffCount!="")
     if(!IsNumber(StaffCount))
     {
       isFlag = false;
        fieldText += "人员规模|";
        msgText += "人员规模必须为非负整数|";
     }
       if(SetupMoney!="")
       {
         if(!IsNumeric(SetupMoney))
         {
           isFlag = false;
            fieldText += "注册资本|";
            msgText += "注册资本格式不对|";
         }
         else
         {
           if(SetupMoney.indexOf('.')>-1)
             {
                  if(!IsNumeric(SetupMoney,10,2))
                 {
                   isFlag = false;
                    fieldText += "注册资本|";
                    msgText += "注册资本格式不对|";
                 }
             }
             else if(SetupMoney.indexOf('.')==-1)
             {
                 if(strlen(SetupMoney)>10)
                 {
                   isFlag = false;
                    fieldText += "注册资本|";
                    msgText += "注册资本整数部分不能超过10位|";
                 }
             }
         }
       }
   
      if(CapitalScale!="")
      {
         if(!IsNumeric(CapitalScale))
         {
           isFlag = false;
            fieldText += "资产规模|";
            msgText += "资产规模格式不对|";
         }
        else
         {
           if(CapitalScale.indexOf('.')>-1)
             {
                  if(!IsNumeric(CapitalScale,10,2))
                 {
                   isFlag = false;
                    fieldText += "资产规模|";
                    msgText += "资产规模格式不对|";
                 }
             }
             else if(CapitalScale.indexOf('.')==-1)
             {
                 if(strlen(CapitalScale)>10)
                 {
                   isFlag = false;
                    fieldText += "资产规模|";
                    msgText += "资产规模整数部分不能超过10位|";
                 }
             }
         }
      }
   
      if(SaleroomY!="")
      {
          if(!IsNumeric(SaleroomY))
         {
           isFlag = false;
            fieldText += "年销售额|";
            msgText += "年销售额格式不对|";
         }
         else
         {
           if(SaleroomY.indexOf('.')>-1)
             {
                  if(!IsNumeric(SaleroomY,10,2))
                 {
                   isFlag = false;
                    fieldText += "年销售额|";
                    msgText += "年销售额格式不对|";
                 }
             }
             else if(SaleroomY.indexOf('.')==-1)
             {
                 if(strlen(SaleroomY)>10)
                 {
                   isFlag = false;
                    fieldText += "年销售额|";
                    msgText += "年销售额整数部分不能超过10位|";
                 }
             }
         }
      }

      if(ProfitY!="")
      {
         if(!IsNumeric(ProfitY))
         {
           isFlag = false;
            fieldText += "年利润额|";
            msgText += "年利润额格式不对|";
         }
         else
         {
           if(ProfitY.indexOf('.')>-1)
             {
                  if(!IsNumeric(ProfitY,10,2))
                 {
                   isFlag = false;
                    fieldText += "年利润额|";
                    msgText += "年利润额格式不对|";
                 }
             }
             else if(ProfitY.indexOf('.')==-1)
             {
                 if(strlen(ProfitY)>10)
                 {
                   isFlag = false;
                    fieldText += "年利润额|";
                    msgText += "年利润额整数部分不能超过10位|";
                 }
             }
         }
      }
      if(Tel!="")
    {
    if(isvalidtel(Tel))
    {
        isFlag = false;
        fieldText = fieldText + "电话|";
        msgText = msgText +  "电话格式不对|";      
    }
    }
   if(Post!="")
   {
    if(!IsNumeric(Post))
    {
      isFlag = false;
        fieldText = fieldText + "邮编 |";
        msgText = msgText +  "邮编格式不对|";
    }
   }
   if(CountryID==null)
   {
        isFlag = false;
        fieldText = fieldText + "国家地区 |";
        msgText = msgText +  "请选择国家地区|";
   }
    if(Mobile!="")
    if(!checkMobile(Mobile))
    {
      isFlag = false;
      fieldText = fieldText + "手机|";
   	  msgText = msgText +  "手机号码有错误|";
    }
    if(Fax!="")
   {
    if(isvalidtel(Fax))
    {
        isFlag = false;
        fieldText = fieldText + "传真|";
        msgText = msgText +  "传真格式不对|";
    }
   }
   if(email!="")
   {
    if(!IsEmail(email))
    {
     isFlag = false;
        fieldText = fieldText + "邮件|";
        msgText = msgText +  "邮件格式不对|";
    }
   }
    if(strlen(CreateDate)>0)
    {
        if(!isDate(CreateDate))
        {
            isFlag = false;
             fieldText = fieldText +  "建档日期|";
   		     msgText = msgText + "建档日期格式不正确|";   
        }
    }
        var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
            isFlag = false;
            fieldText = fieldText + RetVal+"|";
   		    msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
        return ;
    }
  
        var UrlParam ='';
       var UrlParam = "CustNo="+CustNo+"\
                     &BigType="+BigType+"\
                     &CustName="+CustName+"\
                     &CorpNam="+CorpNam+"\
                     &PYShort="+PYShort+"\
                     &CustNote="+CustNote+"\
                     &AreaID="+AreaID+"\
                     &CompanyType="+CompanyType+"\
                     &StaffCount="+StaffCount+"\
                     &SetupDate="+SetupDate+"\
                     &ArtiPerson="+ArtiPerson+"\
                     &SetupMoney="+SetupMoney+"\
                     &SetupAddress="+SetupAddress+"\
                     &CapitalScale="+CapitalScale+"\
                     &SaleroomY="+SaleroomY+"\
                     &ProfitY="+ProfitY+"\
                     &TaxCD="+TaxCD+"\
                     &BusiNumber="+BusiNumber+"\
                     &isTax="+isTax+"&SellArea="+SellArea+"&CountryID="+CountryID+"&Province="+Province+"\
                     &City="+City+"&Post="+Post+"&ContactName="+ContactName+"&Tel="+Tel+"&Fax="+Fax+"\
                     &Mobile="+Mobile+"&email="+email+"&Addr="+Addr+"&PayType="+PayType+"&MoneyType="+MoneyType+"&CurrencyType="+CurrencyType+"\
                     &Remark="+Remark+"&UsedStatus="+UsedStatus+"&Creator="+Creator+"&BillType="+BillType+"&CreateDate="+CreateDate+"+&Action="+Action               
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/SupplyChain/OtherCorpInfo.ashx?"+UrlParam,
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                  }, 
                  error: function() 
                  {
                    popMsgObj.ShowMsg('请求发生错误');
                  }, 
                  success:function(data) 
                  { 
                    if(data.sta>0) 
                    {
                        popMsgObj.ShowMsg(data.info);
                        document.getElementById('txtIndentityID').value = data.sta;
                         document.getElementById('txt_CustNo').readOnly=true; 
//                         document.all.txt_CustNo.readOnly=true;  
                    }
                    else
                    {
                        popMsgObj.ShowMsg(data.info);
                    }
                  } 
               });
//    }   
}

//===============================================单 证 唯 一 性  处 理=======================================
function checkonly()
{
    var PlanNo=document.getElementById('txt_CustNo').value; 
    var TableName="officedba.OtherCorpInfo";
    var ColumName="CustNo";
    $.ajax({ 
              type: "POST",
              url: "../../../Handler/CheckOnlyOne.ashx?strcode='"+PlanNo+"'&colname="+ColumName+"&tablename="+TableName+"",
              dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              {  
              }, 
            error: function() 
            {
            }, 
            success:function(data) 
            { 
                if(data.sta!=1) 
                { 
//                popMsgObj.ShowMsg("往来单位编号已经存在");
                    document.getElementById('txtPlanNoHidden').value = data.sta;
                }
                else
                {
                    document.getElementById('txtPlanNoHidden').value = "";
                } 
            } 
           });
}


 //若是中文则自动填充拼音缩写
function LoadPYShort()
{
    var txt_CustName = trim($("#txt_CustName").val());
    if(txt_CustName.length>0 && isChinese(txt_CustName))
    {
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Common/PYShort.ashx?Text="+escape(txt_CustName),
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                     //AddPop();
                  }, 
                  //complete :function(){ //hidePopup();},
                  error: function(){}, 
                  success:function(data) 
                  { 
                    document.getElementById('txt_PYShort').value = data.info;
                  } 
               });
     }
 
}
function DoBack()
{
    //获取查询条件
       var searchCondition = document.getElementById("hidSearchCondition").value;
        //获取模块功能ID
         var ModuleID = document.getElementById("hidModuleID").value;
        window.location.href = "OtherCorpInfo.aspx?ModuleID=" + ModuleID + searchCondition;
}
