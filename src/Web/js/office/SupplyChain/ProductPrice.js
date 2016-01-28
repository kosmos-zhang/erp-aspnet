 
$(document).ready(function()
{
    requestobj = GetRequest();
    recordnoparam = requestobj['intOtherCorpInfoID'];
    if(typeof(recordnoparam)!="undefined")
    { 
     document.getElementById('txt_ChangeNo').disabled=true;  
     document.getElementById('txtIndentityID').value=recordnoparam;
//     document.getElementById("hidSearchCondition").value=requestobj;
    }
    else
    {
    }
});
 function Fun_FillParent_Content(id,no,productname,price,unit,codyt,taxrate,selltax,discount,d,TypeCode,name)
{
    document.getElementById("txt_ProductName").value=productname;
    document.getElementById("hf_ProductID").value=id;
    document.getElementById('divStorageProduct').style.display='none';
    document.getElementById("txt_TaxRate").value=taxrate;
    document.getElementById("txt_Discount").value=discount;
    document.getElementById("txt_StandardSell").value=price;
    document.getElementById("txt_SellTax").value=selltax;
    document.getElementById("txt_TaxRateNew").value=taxrate;
    document.getElementById("txt_DiscountNew").value=discount;
    document.getElementById("txt_StandardSellNew").value=price;
    document.getElementById("txt_SellTaxNew").value=selltax;
}
function Fun_Save_ProductPriceInfo()
{ 
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    requestobj = GetRequest();
    recordnoparam = requestobj['intOtherCorpInfoID'];
    
    if(typeof(recordnoparam)=="undefined")
    {
        Action = 'Add';
        if( document.getElementById('txtPlanNoHidden').value=="0")
        {
          isFlag = false;
          fieldText = fieldText +  "单位编号|";
          msgText = msgText + "变更单编号已经存在|"; 
        }
    }
    else
    {
      Action = 'Edit';
//       document.getElementById("divConfirmor").style.display="block";
    }
    if(document.getElementById('txtIndentityID').value!="0")
    {
     Action = 'Edit';
//       document.getElementById("divConfirmor").style.display="block";
    }
    var ChangeNo=trim($("#txt_ChangeNo").val());                 
    var Title=trim($("#txt_Title").val());                       
    var ProductID=trim($("#hf_ProductID").val());               
    var StandardSell=trim($("#txt_StandardSell").val());         
    var SellTax=trim($("#txt_SellTax").val());                   
    var StandardSellNew=trim($("#txt_StandardSellNew").val());   
    var SellTaxNew=trim($("#txt_SellTaxNew").val());             
    var ChangeDate=trim($("#txt_ChangeDate").val());             
    var Chenger=trim($("#txtChenger").val());                   
    var Remark=trim($("#txt_Remark").val());                     
    var BillStatus=trim($("#sel_BillStatus").val());             
    var Creator=$("#hf_Creator").val();                   
    var CreateDate=$("#txt_CreateDate").val();             
    var Confirmor="";               
    var ConfirmDate=$("#txt_ConfirmDate").val();   
    var ID= $("#txtIndentityID").val();      
    var DiscountNew= trim($("#txt_DiscountNew").val());
    var Discount= trim($("#txt_Discount").val());
    var TaxRateNew= trim($("#txt_TaxRateNew").val());
    var TaxRate= trim($("#txt_TaxRate").val());
     if(ChangeNo=="")
     {
        isFlag = false;
        fieldText += "变更单编号|";
        msgText += "请输入变更单编号|";
     }
    else
    {
      if(!CodeCheck(ChangeNo))
        {
            isFlag = false;
           fieldText = fieldText + "变更单编号|";
   		    msgText = msgText +  "变更单编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]|";
       }
    }
      if(strlen(ChangeNo)>50)
     {
        isFlag = false;
        fieldText += "变更单编号|";
        msgText += "变更单仅限于50个字符以内|";
     }
     if(Title=="")
     {
        isFlag = false;
        fieldText += "变更单主题|";
        msgText += "请输入变更单主题|";
     }
      if(strlen(Title)>100)
     {
        isFlag = false;
        fieldText += "变更单主题|";
        msgText += "变更单主题仅限于100个字符以内|";
     }
       if(ProductID=="")
     {
        isFlag = false;
        fieldText += "物品名称|";
        msgText += "请选择物品|";
     }
     if(Remark!="")
     {
        if(strlen(Remark)>1024)
     {
        isFlag = false;
        fieldText += "调整原因|";
        msgText += "调整原因仅限于1024个字符以内|";
     }
     }
     if(StandardSellNew=="")
     {
        isFlag = false;
        fieldText += "调整后去税售价|";
        msgText += "请输入调整后去税售价|";
     }
      if(StandardSellNew!="")
      {
          if(!IsNumeric(StandardSellNew))
       {
          isFlag = false;
        fieldText += "调整后去税售价|";
        msgText += "调整后去税售价格式不对|";
       }
      else{
       if(StandardSellNew.indexOf('.')>-1)
     {
      if(!IsNumeric(StandardSellNew,8,glb_SelPoint))
     {
       isFlag = false;
        fieldText += "调整后去税售价|";
        msgText += "调整后去税售价格式不对|";
     }
     }
     else if(StandardSellNew.indexOf('.')==-1)
     {
       if(strlen(StandardSellNew)>8)
     {
      isFlag = false;
        fieldText += "调整后去税售价|";
        msgText += "调整后去税售价整数部分不能超过8位|";
     }
     }
      }
     }
    
     if(SellTaxNew=="")
     {
        isFlag = false;
        fieldText += "调整后含税售价|";
        msgText += "请输入调整后含税售价|";
     }
      if(SellTaxNew!="")
      {
          if(!IsNumeric(SellTaxNew))
       {
          isFlag = false;
        fieldText += "调整后含税售价|";
        msgText += "调整后含税售价格式不对|";
       }
       else
       {
        if(SellTaxNew.indexOf('.')>-1)
     {
         if (!IsNumeric(SellTaxNew, 8, glb_SelPoint))
     {
       isFlag = false;
        fieldText += "调整后含税售价|";
        msgText += "调整后含税售价格式不对|";
     }
     }
     else if(SellTaxNew.indexOf('.')==-1)
     {
       if(strlen(SellTaxNew)>8)
     {
      isFlag = false;
        fieldText += "调整后含税售价|";
        msgText += "调整后含税售价整数部分不能超过8位|";
     }
     }
       }
      }
      if(DiscountNew!="")
      {
          if(!IsNumeric(DiscountNew))
       {
          isFlag = false;
        fieldText += "调整后折扣|";
        msgText += "调整后折扣格式不对|";
       }
       else{
       if(parseFloat(DiscountNew)>100)
       {
          isFlag = false;
        fieldText += "调整后折扣|";
        msgText += "调整后折扣不能超过100|";
       }
      else if(DiscountNew.indexOf('.')>-1)
     {
         if (!IsNumeric(DiscountNew, 6, glb_SelPoint))
     {
       isFlag = false;
        fieldText += "调整后折扣|";
        msgText += "调整后折扣格式不对|";
     }
     }
     else if(DiscountNew.indexOf('.')==-1)
     {
       if(strlen(DiscountNew)>6)
     {
      isFlag = false;
        fieldText += "调整后折扣|";
        msgText += "调整后折扣整数部分不能超过6位|";
     }
     }}
      }
     else 
     {
        isFlag = false;
        fieldText += "调整后折扣|";
        msgText += "请输入调整后折扣|";
     }
     
       if(TaxRateNew!="")
      {
          if(!IsNumeric(TaxRateNew))
       {
          isFlag = false;
        fieldText += "调整后销项税率|";
        msgText += "调整后销项税率格式不对|";
       }
       else
       {
            if(parseFloat(TaxRateNew)>100)
       {
          isFlag = false;
        fieldText += "调整后销项税率|";
        msgText += "调整后销项税率不能超过100|";
       }
       if(TaxRateNew.indexOf('.')>-1)
     {
         if (!IsNumeric(TaxRateNew, 6, glb_SelPoint))
     {
       isFlag = false;
        fieldText += "调整后销项税率|";
        msgText += "调整后销项税率格式不对|";
     }
     }
     else if(TaxRateNew.indexOf('.')==-1)
     {
       if(strlen(TaxRateNew)>6)
     {
      isFlag = false;
        fieldText += "调整后销项税率|";
        msgText += "调整后销项税率整数部分不能超过6位|";
     }
     }
       }
   
      }
     else 
     {
        isFlag = false;
        fieldText += "调整后销项税率|";
        msgText += "请输入调整后销项税率|";
     }
     
     if(strlen(ChangeDate)>0)
    {
        if(!isDate(ChangeDate))
        {
            isFlag = false;
             fieldText = fieldText +  "申请日期|";
   		     msgText = msgText + "申请日期式不正确|";   
        }
    }
    else
    {isFlag = false;
             fieldText = fieldText +  "申请日期|";
   		     msgText = msgText + "请选择申请日期|";
     }
       if(strlen(CreateDate)>0)
    {
        if(!isDate(CreateDate))
        {
            isFlag = false;
             fieldText = fieldText +  "制单日期|";
   		     msgText = msgText + "制单日期格式不正确|";   
        }
    }
    else{isFlag = false;
             fieldText = fieldText +  "制单日期|";
   		     msgText = msgText + "请选择制单日期|";
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
    if((parseFloat(SellTax)!=parseFloat(SellTaxNew))||(parseFloat(Discount)!=parseFloat(DiscountNew)))
    { 
   
    }
    else{  popMsgObj.ShowMsg("未做价格或折扣调整，请先调整价格或折扣，再确认！");
    return;}
       var UrlParam = "ChangeNo="+ChangeNo+"\
                     &Title	="+	Title	+"\
         &ProductID	="+	ProductID	+"\
         &StandardSell	="+	StandardSell	+"\
         &SellTax	="+	SellTax	+"\
         &StandardSellNew	="+	StandardSellNew	+"\
         &SellTaxNew	="+	SellTaxNew	+"\
         &ChangeDate	="+	ChangeDate	+"\
         &Chenger	="+	Chenger	+"\
         &Remark	="+	Remark	+"\
         &BillStatus	="+	BillStatus	+"\
         &Creator	="+	Creator	+"\
         &TaxRateNew	="+	TaxRateNew	+"\
         &DiscountNew	="+	DiscountNew	+"\
         &Discount	="+	Discount	+"\
         &TaxRate	="+	TaxRate	+"\
         &CreateDate	="+	CreateDate	+"\
         &Confirmor	="+	Confirmor	+"\
         &ConfirmDate	="+	ConfirmDate	+"\
         &ID	="+	ID	+"\
         &Action="+Action             
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/SupplyChain/ProductPrice.ashx?"+UrlParam,
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
                        document.getElementById('txt_ChangeNo').readOnly=true; 
//                        document.all.txt_ChangeNo.readOnly=true; 
                        document.getElementById("txt_ChangeNo").disabled=true;
                        document.getElementById("btn_AD").style.display="block";
                        document.getElementById("btnsure").style.display="none";
                    }
                    else
                    {
                        popMsgObj.ShowMsg(data.info);
                    }
                  } 
               }); 
}
function checkonly()
{
    if(!document.getElementById('txt_ProdNo').readOnly)
    {
        var ChangeNo=document.getElementById('txt_ChangeNo').value; 
    var TableName="officedba.ProductInfo";
    var ColumName="ChangeNo";
    $.ajax({ 
              type: "POST",
              url: "../../../Handler/CheckOnlyOne.ashx?strcode='"+ProdNo+"'&colname="+ColumName+"&tablename="+TableName+"",
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
                popMsgObj.ShowMsg("价格调整单编号已经存在");
                    document.getElementById('txtPlanNoHidden').value = data.sta;
                }
                else
                {
                    document.getElementById('txtPlanNoHidden').value = "";
                } 
            } 
           });
    }
}
function ChangeStatus()
{ 
 var SellTaxNew=document.getElementById("txt_SellTaxNew").value;//含税售价
 var StandardSellNew=document.getElementById("txt_StandardSellNew").value;//去税售价
 var TaxRateNew=document.getElementById("txt_TaxRateNew").value;//销项税率
  var sum=parseFloat(StandardSellNew)*(parseFloat(TaxRateNew)/100+1);
  if (parseFloat(document.getElementById("txt_SellTaxNew").value).toFixed(glb_SelPoint) != sum.toFixed(glb_SelPoint))
  {
    var msg=window.confirm("调整后的含税售价不等于调整后的去税售价*（1+销项税率），是否继续确认？")
    if(msg==true)
    {
    stauts();
    }
  }
  else
  {
  stauts();
  }
}


function stauts()
{
try{
     var Action="ChangeStatus";
     var fieldText = "";
     var msgText = "";
     var isFlag = true;
     var Confirmor=$("#hf_Confirmor").val();               
     var ConfirmDate=$("#txt_ConfirmDate").val();  
   if(strlen(ConfirmDate)>0)
    {
        if(!isDate(ConfirmDate))
        {
            isFlag = false;
             fieldText = fieldText +  "确认日期|";
   		     msgText = msgText + "确认日期式不正确|";   
        }
    }
    else
    {       
             isFlag = false;
             fieldText = fieldText +  "确认日期|";
   		     msgText = msgText + "请选择确认日期|";
     }
    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
        return;
    }
      var ID= $("#txtIndentityID").val(); 
      var ProductID=$("#hf_ProductID").val(); 
      var StandardSellNew= document.getElementById("txt_StandardSellNew").value;
      var SellTaxNew= document.getElementById("txt_SellTaxNew").value;
      
      var TaxRateNew= document.getElementById("txt_TaxRateNew").value;
      var DiscountNew= document.getElementById("txt_DiscountNew").value;
       
           var UrlParam = "Action="+Action+"&ChangeID="+ID+"&ProductID="+ProductID+"&StandardSellNew="+StandardSellNew+"&SellTaxNew="+SellTaxNew+"&Confirmor="+Confirmor+"&ConfirmDate="+ConfirmDate+"&TaxRateNew="+TaxRateNew+"&DiscountNew="+DiscountNew
    $.ajax({ 
               type: "POST",
                  url: "../../../Handler/Office/SupplyChain/ProductPrice.ashx?"+UrlParam,
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
                        document.getElementById('sel_BillStatus').value = data.sta;
                        document.getElementById("btnAdd").style.display="none";
                        document.getElementById("btn_AD").style.display="none";
                        document.getElementById("btnsave").style.display="block";
                        document.getElementById("btnsure").style.display="block";
                        if(typeof(recordnoparam)!="undefined")
                         { 
                          document.getElementById("btnback").style.display="block";
                         }
                        document.getElementById("divConfirmor").style.display="block";

                    }
                    else
                    {
                        popMsgObj.ShowMsg(data.info);
                    }
                  } 
           });
           }
           catch(e)
           {}
}

function LoadSellTaxNew()
{
 var SellTaxNew=document.getElementById("txt_SellTaxNew").value;//含税售价
 var StandardSellNew=document.getElementById("txt_StandardSellNew").value;//去税售价
 var TaxRateNew=document.getElementById("txt_TaxRateNew").value;//销项税率
 var sub= parseFloat(StandardSellNew)*(parseFloat(TaxRateNew)/100+1);
 if(isNaN(sub))
 {
  document.getElementById("txt_SellTaxNew").value="";
 }
 else
 {
     document.getElementById("txt_SellTaxNew").value = sub.toFixed(glb_SelPoint);
 }
 //含税售价=去税售价*（1+销项税率）
}
/*
* 返回按钮
*/
function DoBack()
{
    //获取查询条件
       var searchCondition = document.getElementById("hidSearchCondition").value;
        //获取模块功能ID
         var ModuleID = document.getElementById("hidModuleID").value;
        window.location.href = "ProductPriceList.aspx?ModuleID=" + ModuleID + searchCondition;
}
///打印单据
function PrintProductPrice()
{
 var ChangeNo=document.getElementById('txtIndentityID').value; 
 if(ChangeNo=="0"){alert("请先保存或确认数据后打印!");return;}
  window.open("PrintProductPrice.aspx?ChangeNo="+ChangeNo);
}