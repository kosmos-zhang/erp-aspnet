//采购新建供应商联络

var DtlCount = 0;

var page = "";
var CustName;
var CustID ;
var Linker;
var LinkerName ;
var StartLinkDate ;
var EndLinkDate ;

$(document).ready(function()
{
    requestobj = GetRequest();
    var intMasterProviderLinkManID = document.getElementById("txtIndentityID").value;
    
    recordnoparam = intMasterProviderLinkManID.toString();
    
    var CustName = requestobj['CustName'];
    var CustID = requestobj['CustID'];
    var LinkerName = requestobj['LinkerName'];
    var Linker = requestobj['Linker'];
    var StartLinkDate = requestobj['StartLinkDate'];
    var EndLinkDate = requestobj['EndLinkDate'];
    
    var Isliebiao = requestobj['Isliebiao'];
    var PageIndex = requestobj['PageIndex'];
    var PageCount = requestobj['PageCount'];
    
    if(typeof(Isliebiao)!="undefined")
    {
        $("#btn_back").show();
        document.getElementById("hidIsliebiao").value = Isliebiao;
    }
    
    var ModuleID = document.getElementById("hidModuleID").value;
    var URLParams = "Isliebiao="+escape(Isliebiao)+"&ModuleID="+escape(ModuleID)+"&CustName="+escape(CustName)+"&CustID="+escape(CustID)+"&Linker="+escape(Linker)+
                    "&LinkerName="+escape(LinkerName)+"&StartLinkDate="+escape(StartLinkDate)+"&EndLinkDate="+escape(EndLinkDate)+"&PageIndex="+escape(PageIndex)+"&PageCount="+escape(PageCount)+"";
    document.getElementById("hidSearchCondition").value = URLParams;
    
    
    DealPage(recordnoparam);

});


//获取url中"?"符后的字串
function GetRequest()
 {
   var url = location.search; 
   var theRequest = new Object();
   if (url.indexOf("?") != -1) 
   {
      var str = url.substr(1);
      strs = str.split("&");
      for(var i = 0; i < strs.length; i ++) 
      {
         theRequest[strs[i].split("=")[0]]=unescape(strs[i].split("=")[1]);
      }
   }

   return theRequest;
  }

//处理加载页面
function DealPage(recordnoparam)
{
//显示返回按钮
        
        if(typeof(document.getElementById("hidIsliebiao").value)!="undefined")
        {
            if(recordnoparam !=0)
            {
                document.getElementById("txtAction").value="2";
                document.getElementById("divTitle").innerText="供应商联络";
//                GetFlowButton_DisplayControl();
        //        //显示返回按钮
        //        $("#btn_back").show();
                GetProviderInfo(recordnoparam);
            }
    }
}

//返回
function Back()
{ 
var URLParams = document.getElementById("hidSearchCondition").value;
    window.location.href='ProviderContactHistoryInfo.aspx?'+URLParams;
}

function GetProviderInfo(ID)
{
 
     $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:"../../../Handler/Office/PurchaseManager/ProviderContactHistoryEdit.ashx",//目标地址
//       data:'ID='+escape(ID),
        data:"ID="+ID+"",
//        data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+"&RoleID="+escape(RoleID)+"",//数据
       cache:false,
       beforeSend:function(){AddPop();},//发送数据之前           
       success: function(msg){
                //数据获取完毕，填充页面据显示
                $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                    {
                        //基本信息
                        $("#CodingRuleControl1_txtCode").attr("value",item.ContactNo);//联络单编号
                        $("#divProviderContactHistoryNo").attr("value",item.ContactNo);//合同编号
                        $("#txtCustID").attr("value",item.CustID);//供应商id
                        $("#txtCustNo").attr("value",item.CustNo);//供应商编号
                        $("#txtCustName").attr("value",item.CustName);//供应商名称
                        $("#txtTitle").attr("value",item.Title);//主题
                        $("#HidLinkManID").attr("value",item.LinkManID);//供应商被联络人id
                        $("#UsertxtLinkManName").attr("value",item.LinkManName);//供应商被联络人
                        $("#drpLinkReasonID").attr("value",item.LinkReason);//联络事由
                        $("#drpLinkMode").attr("value",item.LinkMode);//联络方式
                        $("#txtLinkDate").attr("value",item.LinkDate);//联络时间
                        $("#HidLinker").attr("value",item.Linker);//联络人id
                        $("#UsertxtLinkerName").attr("value",item.LinkerName);//联络人名称
                        $("#txtContents").attr("value",item.Contents);//联络内容
                        document.getElementById("divProviderContactHistoryNo").innerHTML=item.ContactNo;
                        document.getElementById("divProviderContactHistoryNo").style.display="block";
                        document.getElementById("divInputNo").style.display="none";
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







//采购新建供应商联络
function InsertProviderContactHistory() 
{ 

if(!CheckBaseInfo())return;

        var action = null;

         var fieldText = "";
         var msgText = "";
         var isFlag = true;
         
         
         //供应商联络
         var contactNo = "";
         var CodeType=$("#CodingRuleControl1_ddlCodeRule").val();
         
         //基本信息
   
         if (CodeType == "")
         {
            contactNo=$("#CodingRuleControl1_txtCode").val();
         }
         var custID = document.getElementById("txtCustID").value.Trim();//供应商id
         var title=document.getElementById("txtTitle").value.Trim();//主题
         var linkManID=document.getElementById("HidLinkManID").value.Trim();//被联络人ID
         var linkManName=document.getElementById("UsertxtLinkManName").value.Trim();//被联络人名称
         var linkReasonID=document.getElementById("drpLinkReasonID").value.Trim();//联络事由
         var linkMode=document.getElementById("drpLinkMode").value.Trim();//联络方式
         var linkDate=document.getElementById("txtLinkDate").value.Trim();//联络时间
         var linker=document.getElementById("HidLinker").value.Trim();//联络人ID
         var contents=document.getElementById("txtContents").value.Trim();//联络内容
        
         
         if(document.getElementById("txtAction").value=="1")
        {
            action="Add";
        }
        else
        {
             action="Update";
        }
        
     
         if(custID.length<=0)
         {
            isFlag = false;
            fieldText = fieldText + "供应商名称|";
   		    msgText = msgText +  "供应商名称不允许为空|";
         }
         
         var no=  document.getElementById("divProviderContactHistoryNo").innerHTML;
         var txtIndentityID = $("#txtIndentityID").val();
         
         
//         var UrlParam = "custID="+escape(custID)+"&title="+escape(title)+"&linkManID="+escape(linkManID)+"&linkManName="+escape(linkManName)+"\
//                        &linkReasonID="+escape(linkReasonID)+"&linkMode="+escape(linkMode)+"&linkDate="+escape(linkDate)+"&linker="+escape(linker)+"\
//                        &contents="+escape(contents)+"&action="+escape(action)+"&ID="+escape(txtIndentityID)+"&contactNo="+escape(contactNo)+"\
//                        &CodeType="+escape(CodeType)+"&cno="+escape(no)+"";
         
         
         
        $.ajax({ 
                  type: "POST",
//                  url: "../../../Handler/Office/PurchaseManager/ProviderContactHistoryAdd.ashx?"+UrlParam,
                  url: "../../../Handler/Office/PurchaseManager/ProviderContactHistoryAdd.ashx",
                  dataType:'json',//返回json格式数据
                  data: "custID="+escape(custID)+"&title="+escape(title)+"&linkManID="+escape(linkManID)+"&linkManName="+escape(linkManName)+"\
                        &linkReasonID="+escape(linkReasonID)+"&linkMode="+escape(linkMode)+"&linkDate="+escape(linkDate)+"&linker="+escape(linker)+"\
                        &contents="+escape(contents)+"&action="+escape(action)+"&ID="+escape(txtIndentityID)+"&contactNo="+escape(contactNo)+"\
                        &CodeType="+escape(CodeType)+"&cno="+escape(no)+"",//数据
                
                  cache:false,
                  beforeSend:function()
                  { 
                      //AddPop();
                  }, 
                //complete :function(){hidePopup();},
                error: function() {
                
                  
                }, 
                success:function(data) 
                {
                    if(data.sta>0) 
                    { 
                      
                        if(action=="Add")
                        {
                            popMsgObj.ShowMsg(data.info);
                            document.getElementById('txtIndentityID').value = data.sta;
                            document.getElementById("txtAction").value="2";
                            if(CodeType!="")
                           {
                                isnew="edit";
                                document.getElementById("divProviderContactHistoryNo").innerHTML=data.data; 
                                document.getElementById("divProviderContactHistoryNo").style.display="block";
                                document.getElementById("divInputNo").style.display="none";
                           }
                           else
                           {
                               document.getElementById("divProviderContactHistoryNo").innerHTML=data.data;
                               document.getElementById("divProviderContactHistoryNo").style.display="block";
                               document.getElementById("divInputNo").style.display="none";
                                
                           }
                        }
                        else
                        {
                              popMsgObj.ShowMsg(data.info);
                        }
                             
                    } 
                    else 
                    { 
                        hidePopup();
                        popMsgObj.ShowMsg(data.info);
                    } 
                } 
               });  

} 


//验证
//基本信息校验
function CheckBaseInfo()
{

     var fieldText = "";
      var msgText = "";
      var isFlag = true;   
      
      //先检验页面上的特殊字符
      var RetVal=CheckSpecialWords();
      if(RetVal!="")
      {
          isFlag = false;
          fieldText = fieldText + RetVal+"|";
          msgText = msgText +RetVal+  "不能含有特殊字符|";
      }
      
      
    //新建时，编号选择手工输入时
    if (document.getElementById("txtAction").value=="1")
    {
//        获取编码规则下拉列表选中项
        codeRule = document.getElementById("CodingRuleControl1_ddlCodeRule").value.Trim();
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "")
        {
            //获取输入的编号
            employeeNo = document.getElementById("CodingRuleControl1_txtCode").value.Trim();
            //编号必须输入
            if (employeeNo == "")
            {
                isFlag = false;
                fieldText += "联络单编号|";
   		        msgText += "请输入联络单编号|";
            }
            else
            {
                if(!CodeCheck($.trim($("#CodingRuleControl1_txtCode").val())))
                {
                    isFlag = false;
                    fieldText = fieldText + "联络单编号|";
   		            msgText = msgText +  "联络单编号只能由英文字母（a-z大小写均可）、数字（0-9）、字符（_-/.()[]{}）组成|";
                }
                else if(strlen($.trim($("#CodingRuleControl1_txtCode").val())) > 50)
                {
                    isErrorFlag = true;
                    fieldText += "联络单编号|";
   		            msgText += "联络单编号长度不大于50|";
   		        }
   		        
            }
        }    
     }       
   

    //不为空验证
    if(document.getElementById("txtCustName").value.Trim() == "")
    {
        isFlag = false;
        fieldText += "供应商名称|";
        msgText += "请输入供应商名称|";
    }
//    if(document.getElementById("txtTitle").value.Trim() == "")
//    {
//        isFlag = false;
//        fieldText += "主题|";
//        msgText += "请输入主题|";
//    }
//    else
//    {
//        if(strlen(document.getElementById("txtTitle").value.Trim())>50)
//        {
//            isFlag = false;
//            fieldText +=  "主题|";
//   		    msgText +=  "主题仅限于50个字符以内|";      
//        }
//    }


    if(strlen(document.getElementById("txtTitle").value.Trim())>50)
    {
        isFlag = false;
        fieldText +=  "主题|";
	    msgText +=  "主题仅限于50个字符以内|";      
    }

    if(document.getElementById("UsertxtLinkerName").value.Trim() == "")
    {
        isFlag = false;
        fieldText += "联络人|";
        msgText += "请输入联络人|";
    }
    

    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
    }
    return isFlag;
}

//选择分店销售订单带出相关信息
function FillFromSubSellOrder(id,orderno,sendmode,outdate,outuserid,outusername,custname,custtel,custmobile,custaddr)
{  
if(document.getElementById("txtOrderID").value.Trim() == "")
    {
        document.getElementById("HidOrderID").value = id;
        document.getElementById("txtOrderID").value = orderno;
        document.getElementById("ddlSendMode").value = sendmode; 
        if(outdate != "")
        {
            var outdate1 = outdate.split(' ')[0];
            var outdatehour = outdate.split(' ')[1].split(':')[0];
            var outdatemin = outdate.split(' ')[1].split(':')[1];
            document.getElementById("txtOutDate").value = outdate1;
            document.getElementById("ddlOutDateHour").value = outdatehour;
            document.getElementById("ddlOutDateMin").value = outdatemin;
        }
        document.getElementById("HidOutUserID").value = outuserid;
        document.getElementById("UserOutUserID").value = outusername;
        document.getElementById("txtCustName").value = custname;
        document.getElementById("txtCustTel").value = custtel;
        document.getElementById("txtCustMobile").value = custmobile;
        document.getElementById("txtCustAddr").value = custaddr;
        
        closeSubSellOrderdiv();
    }
    else
    {
        if(document.getElementById("txtOrderID").value.Trim() == orderno)
        {
            closeSubSellOrderdiv();
        }
        else
        {
            DeleteSignRow100();
            fnTotalInfo();
            document.getElementById("HidOrderID").value = id;
            document.getElementById("txtOrderID").value = orderno;
            document.getElementById("ddlSendMode").value = sendmode; 
            if(outdate != "")
            {
                var outdate1 = outdate.split(' ')[0];
                var outdatehour = outdate.split(' ')[1].split(':')[0];
                var outdatemin = outdate.split(' ')[1].split(':')[1];
                document.getElementById("txtOutDate").value = outdate1;
                document.getElementById("ddlOutDateHour").value = outdatehour;
                document.getElementById("ddlOutDateMin").value = outdatemin;
            }
            document.getElementById("HidOutUserID").value = outuserid;
            document.getElementById("UserOutUserID").value = outusername;
            document.getElementById("txtCustName").value = custname;
            document.getElementById("txtCustTel").value = custtel;
            document.getElementById("txtCustMobile").value = custmobile;
            document.getElementById("txtCustAddr").value = custaddr;
            
            closeSubSellOrderdiv();
        }
    }
}

function FillProvider(providerid,providerno,providername,taketype,taketypename,carrytype,carrytypename,paytype,paytypename)
{ 
    if(document.getElementById("txtCustNo").value.Trim() == "")
    {
        document.getElementById("txtCustID").value = providerid;
        document.getElementById("txtCustNo").value = providerno;
        document.getElementById("txtCustName").value = providername;
        closeProviderdiv();
    }
    else
    {
        if(document.getElementById("txtCustNo").value.Trim() == providerno)
        {
            closeProviderdiv();
        }
        else
        {
            document.getElementById("txtCustID").value = providerid;
            document.getElementById("txtCustNo").value = providerno;
            document.getElementById("txtCustName").value = providername;
            
            document.getElementById("UsertxtLinkManName").value = "";
            document.getElementById("HidLinkManID").value = "";
            
            closeProviderdiv();
        }
    }
}




function ProviderLinkMan()
{
    var CustNo= document.getElementById("txtCustNo").value.Trim();
    if(CustNo =="")
    {
        alert("请先选择供应商！");
        return;
    }
    else
    {
        popProviderLinkManSelectObj.ShowList('',CustNo);
    }
    
}



function FillFromProviderLink(id,linkmanname)
{
    document.getElementById("HidLinkManID").value = id;
    document.getElementById("UsertxtLinkManName").value = linkmanname;
     
    closeProviderLinkMandiv();
}




/*单据打印*/
function ProviderContactHistoryPrint()
{
   var isPrint=document.getElementById('txtIndentityID').value;
   if(isPrint!=undefined&&parseInt(isPrint)>0)
   window.open("../../../Pages/PrinttingModel/PurchaseManager/ProviderContactHistoryPrint.aspx?ID="+document.getElementById("txtIndentityID").value);
   else
   popMsgObj.ShowMsg("请先保存单据后再打印");
    
       
}
