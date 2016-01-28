
var Year="{yyyy}";
var YearM="{yyyyMM}";
var YearMD="{yyyyMMdd}";

$(document).ready(function(){
      
      var BatchUsedStatus=0;
      if(document.getElementById('dioBatch2').checked) {
            BatchUsedStatus = 0;
      }
      else
      {
        BatchUsedStatus=1;
      }
      if(BatchUsedStatus==1)
      {
        document.getElementById("div_Add").style.display="block";
        fnInitBatchRule();
      }
      else
      {
        document.getElementById("div_Add").style.display="none";
      }
        
    }); 
//判断批次规则是手工输入还是自动编号，若自动编号则显示编号规则编辑区，若手工输入则清除编号规则并隐藏规则编辑区   
function showOrHiddenBatchRule()
{
    var BatchUsed=0;
    if(document.getElementById('dioBatch2').checked)//手工输入 
    {
      BatchUsed = 0;
    }
    else
    {
        BatchUsed=1;
    }
    if(BatchUsed==1)
    {
        document.getElementById("div_Add").style.display="block";
    }
    else
    {
        document.getElementById("div_Add").style.display="none";
    }
}
 //初始化批次规则值   
function fnInitBatchRule()
{
    $.ajax({ 
          type: "POST",
         url: "../../../Handler/Office/SystemManager/BatchNoRuleSet.ashx?",
          dataType:'json',//返回json格式数据
          cache:false,
          data: "action=getbatchruleinfo",//数据
          beforeSend:function()
          { 
          }, 
          error: function() 
          {
            popMsgObj.ShowMsg('请求发生错误');
          }, 
          success:function(data) 
          { 
                if(data.data[0] != "" && data != null)
                {
                    $("#batchRuleID").val(data.data[0].ID);
                    $("#txt_RuleName").val(data.data[0].RuleName);
                    $("#txt_RulePrefix").val(data.data[0].RulePrefix);
                    $("#txt_RuleNoLen").val(data.data[0].RuleNoLen);
                    $("#txt_RuleExample").val(data.data[0].RuleExample); 
                    $("#txt_Remark").val(data.data[0].Remark); 
                    $("#txt_ModifiedUserID").val(data.data[0].ModifiedUserID);
                    $("#txt_ModifiedDate").val(data.data[0].ModifiedDate);
                    if("{"+data.data[0].RuleDateType+"}"==Year)
                    {
                        document.getElementById("rd_year").checked=true;
                    }
                    else if("{"+data.data[0].RuleDateType+"}"==YearM)
                    {
                        document.getElementById("rd_yearm").checked=true;
                    }
                    else if("{"+data.data[0].RuleDateType+"}"==YearMD)
                    {
                        document.getElementById("rd_yearmd").checked=true;
                    }
                    //document.getElementById("chk_default").checked=data.data[0].IsDefault=="1"?true:false;
                } 
          } 
       });
}

//保存修改批次规则
function InsertBatchNoRule()
{
   var fieldText = "";
   var msgText = "";
   var isFlag = true;
   var UseStatus=null;
   var DefalutStatus=null;
   var RuleDateType=null;
   var ActionFlag="";
   var batchRuleID=$("#batchRuleID").val();
   
   if(batchRuleID != '')
   {
        ActionFlag="updaterule";
   }
   else
   {
        ActionFlag="saverule";
   }
    var RuleName=trim(document.getElementById("txt_RuleName").value);//规则名称
    var RulePrefix=trim(document.getElementById("txt_RulePrefix").value);//前缀
    var RuleNoLen=trim(document.getElementById("txt_RuleNoLen").value);//流水号长度
    var RuleExample=trim(document.getElementById("txt_RuleExample").value);//编号示例
    var Remark=trim(document.getElementById("txt_Remark").value);//备注 
    var UseStatus=0;
    if(document.getElementById('dioBatch2').checked) {
        UseStatus = 0;
    }
    else
    {
        UseStatus=1;
    }
    //若手工输入时则不进行信息验证
   if(UseStatus==1)
   {
         /**验证Start**/

          if(RuleName.length<=0)
          {
              isFlag = false;
                fieldText = fieldText + "批次规则名称|";
   		        msgText = msgText +  "批次规则名称不能为空|";   
          }
          if(strlen(RuleName)>50)
          {
              isFlag = false;
              fieldText = fieldText + "批次规则名称|";
   		      msgText = msgText +  "批次规则名称仅限于50个字符以内|";      
          }
          if(RulePrefix.length<=0)
          {
              isFlag = false;
              fieldText = fieldText + "批次前缀|";
   		      msgText = msgText +  "批次前缀不能为空|";   
          }
          else
          {
              if(!IsLetterStr(RulePrefix))
              {
                 isFlag = false;
                    fieldText = fieldText + "批次前缀|";
   		            msgText = msgText +  "批次前缀只能输入字母|";   
              }
               if(strlen(RulePrefix)>10)
                {
                    isFlag = false;
                    fieldText = fieldText + "批次前缀|";
   		            msgText = msgText +  "批次前缀仅限于10个字符以内|";      
                }            
          }

           if(RuleNoLen.length<=0)
          {
              isFlag = false;
                fieldText = fieldText + "流水号长度|";
   		        msgText = msgText +  "流水号长度不能为空|";   
          }
          else
          {
            var rulelength=parseInt(RuleNoLen);
            if(rulelength>8)
            {
                isFlag = false;
                fieldText = fieldText + "流水号长度|";
   		        msgText = msgText +  "流水号长度不能超过8|";   
            }
             if(!IsNumber(RuleNoLen))
               {
                isFlag = false;
                fieldText = fieldText + "流水号长度|";
   		        msgText = msgText +  "流水号长度必须是数字|";  
               }
          }
           
           if(Remark!="")
            {   if(strlen(Remark)>100)
               {
                isFlag = false;
                fieldText = fieldText + "备注|";
	            msgText = msgText +  "备注仅限于100个字符以内|";  
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
                return;
            }
            /**验证End**/
   }
    
  //if(document.getElementById("chk_default").checked)
  //{
    DefalutStatus="1";
  //}
  //else
  //{
  //   DefalutStatus="0";
  //}
  
    if(document.getElementById("rd_year").checked)
    {
        RuleDateType=Year;
    }
    else if(document.getElementById("rd_yearm").checked)
    {
        RuleDateType=YearM;
    }
    else if(document.getElementById("rd_yearmd").checked)
    {
        RuleDateType=YearMD;
    }
    RuleDateType= RuleDateType.substring(1,RuleDateType.length-1)

    var ID= document.getElementById("batchRuleID").value;
    if(ID=="" || ID=="undefined")
    {
        ID="";
    }
    $.ajax({ 
            type: "POST",
            url: "../../../Handler/Office/SystemManager/BatchNoRuleSet.ashx?",
            dataType:'string',
            cache:false,
            data: "action="+escape(ActionFlag)+"&ID="+ID+"&UsedStatus="+escape(UseStatus)+"&RuleName="+escape(RuleName)+"&RulePrefix="+escape(RulePrefix)+"&RuleDateType="+escape(RuleDateType)+"&RuleNoLen="+escape(RuleNoLen)+"&RuleExample="+escape(RuleExample)+"&Remark="+escape(Remark)+"&IsDefault="+escape(DefalutStatus),//数据
            beforeSend:function()
            { 
            }, 
            error: function() 
            {
                popMsgObj.ShowMsg('请求发生错误!');
            }, 
            success:function(data) 
            { 
                var strarr = data.split("|");
                if(data != "" && data != "undefined")
                {
                    $("#batchRuleID").val(strarr[1]);
                }
                popMsgObj.ShowMsg(strarr[0]);
                
            } 
       });
}

function fillRuleExample()
{
     var RuleDateType=null;
     var RulePrefix=document.getElementById("txt_RulePrefix").value;
     var RuleNoLen=document.getElementById("txt_RuleNoLen").value;
     var rulelength=parseInt(RuleNoLen);
     
     var x="";
     for(var i=1;i<=rulelength;i++)
     {
        x=  x+"N";
     }
     var n="{"+x+"}";
     
    if(document.getElementById("rd_year").checked)
    {
        RuleDateType=Year;
    }
    else if(document.getElementById("rd_yearm").checked)
    {
        RuleDateType=YearM;
    }
    else if(document.getElementById("rd_yearmd").checked)
    {
        RuleDateType=YearMD;
    }
    document.getElementById("txt_RuleExample").value=RulePrefix+RuleDateType+n;
   
}

