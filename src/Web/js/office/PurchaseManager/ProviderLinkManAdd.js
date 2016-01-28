//采购新建供应商档案

var DtlCount = 0;

var page = "";
var CustName;
var CustNo ;
var LinkManName ;
var Handset ;
var Important ;

$(document).ready(function()
{
    requestobj = GetRequest();
    var intMasterProviderLinkManID = document.getElementById("txtIndentityID").value;
    
    recordnoparam = intMasterProviderLinkManID.toString();
    
    var CustName = requestobj['CustName'];
    var CustNo = requestobj['CustNo'];
    var LinkManName = requestobj['LinkManName'];
    var Handset = requestobj['Handset'];
    var Important = requestobj['Important'];
    var LinkType = requestobj['LinkType'];
    var StartBirthday = requestobj['StartBirthday'];
    var EndBirthday = requestobj['EndBirthday'];
    
    var Isliebiao = requestobj['Isliebiao'];
    var PageIndex = requestobj['PageIndex'];
    var PageCount = requestobj['PageCount'];
    
    if(typeof(Isliebiao)!="undefined")
    {
        $("#btn_back").show();
        document.getElementById("hidIsliebiao").value = Isliebiao;
    }
    
    var ModuleID = document.getElementById("hidModuleID").value;
var URLParams = "Isliebiao="+escape(Isliebiao)+"&ModuleID="+escape(ModuleID)+"&CustName="+escape(CustName)+"&CustNo="+escape(CustNo)+"&LinkManName="+escape(LinkManName)+"&Handset="+escape(Handset)+"&Important="+escape(Important)+"&LinkType="+escape(LinkType)+"&StartBirthday="+escape(StartBirthday)+"&EndBirthday="+escape(EndBirthday)+"&PageIndex="+escape(PageIndex)+"&PageCount="+escape(PageCount)+""; 
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
                document.getElementById("divTitle").innerText="供应商联系人";
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
    window.location.href='ProviderLinkManInfo.aspx?'+URLParams;
}

function GetProviderInfo(ID)
{
 
     $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:"../../../Handler/Office/PurchaseManager/ProviderLinkManEdit.ashx",//目标地址
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
                        $("#txtCustName").attr("value",item.CustName);//供应商名称
                        $("#txtCustNo").attr("value",item.CustNo);//供应商编号
                        $("#txtLinkManName").attr("value",item.LinkManName);//联系人姓名
                        $("#drpSex").attr("value",item.Sex);//性别
                        $("#drpImportant").attr("value",item.Important);//重要程度
                        $("#txtCompany").attr("value",item.Company);//单位
                        $("#txtAppellation").attr("value",item.Appellation);//称谓
                        $("#txtDepartment").attr("value",item.Department);//部门
                        $("#txtPosition").attr("value",item.Position);//职务
                        $("#txtOperation").attr("value",item.Operation);//负责业务
                        //联系信息
                        $("#txtWorkTel").attr("value",item.WorkTel);//工作电话
                        $("#txtFax").attr("value",item.Fax);//传真
                        $("#txtHandset").attr("value",item.Handset);//移动电话
                        $("#txtMailAddress").attr("value",item.MailAddress);//邮件地址
                        $("#txtHomeTel").attr("value",item.HomeTel);//家庭电话
                        $("#txtMSN").attr("value",item.MSN);//MSN
                        $("#txtQQ").attr("value",item.QQ);//QQ
                        $("#txtPost").attr("value",item.Post);//邮政编码
                        $("#txtHomeAddress").attr("value",item.HomeAddress);//住址
                        $("#txtAge").attr("value",item.Age);//年龄
                        $("#txtPaperType").attr("value",item.PaperType);//证件类型
                        $("#txtPaperNum").attr("value",item.PaperNum);//证件号
                        $("#drpLinkType").attr("value",item.LinkType);//联系人类型
                        $("#txtModifiedDate").attr("value",item.ModifiedDate);//最后更新时间
                        //关怀点
                        $("#txtBirthday").attr("value",item.Birthday);//生日
                        $("#txtLikes").attr("value",item.Likes);//爱好
//                        $("#hfPageAttachment").attr("value",item.Photo);//照片
                        $("#txtRemark").attr("value",item.Remark);//备注 
                        
//                        if(item.Photo!="")
//                        {
//                            //下载删除显示
//                            document.getElementById("divDealResume").style.display = "block";
//                            //上传不显示
//                            document.getElementById("divUploadResume").style.display = "none"
//                        }
                        
                        //相片                       
                        var photoURL = item.Photo;
                        if (photoURL == "")
                        {                           
                            document.getElementById("imgPhoto").src  = "../../../Images/Pic/Pic_Nopic.jpg";
                        }
                        else
                        {                           
                            document.getElementById("imgPhoto").src  = "../../../Images/Photo/" + photoURL;
                           
                            document.getElementById("hfPhotoUrl").value = photoURL;
                            document.getElementById("hfPagePhotoUrl").value = photoURL;                            
                        }
                    
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







//新建供应商联系人
function InsertProviderLinkManInfo() 
{ 

if(!CheckBaseInfo())return;

        var action = null;

         var fieldText = "";
         var msgText = "";
         var isFlag = true;
         
         
         //基本信息
         var custNo = document.getElementById("txtCustNo").value.Trim();//供应商编号
         var linkManName=document.getElementById("txtLinkManName").value.Trim();//联系人姓名
         var sex=document.getElementById("drpSex").value.Trim();//性别
         var important=document.getElementById("drpImportant").value.Trim();//重要程度
         var company=document.getElementById("txtCompany").value.Trim();//单位
         var appellation=document.getElementById("txtAppellation").value.Trim();//称谓
         var department=document.getElementById("txtDepartment").value.Trim();//部门
         var position=document.getElementById("txtPosition").value.Trim();//职务
         var operation=document.getElementById("txtOperation").value.Trim();//负责业务
        
        //联系信息
        var workTel=document.getElementById("txtWorkTel").value.Trim();//工作电话
        var fax=document.getElementById("txtFax").value.Trim();//传真
        var handset=document.getElementById("txtHandset").value.Trim();//移动电话
        var mailAddress=document.getElementById("txtMailAddress").value.Trim();//邮件地址
        var homeTel=document.getElementById("txtHomeTel").value.Trim();//家庭电话
        var mSN=document.getElementById("txtMSN").value.Trim();//MSN
        var qQ=document.getElementById("txtQQ").value.Trim();//QQ
        var post=document.getElementById("txtPost").value.Trim();//邮政编码
        var homeAddress=document.getElementById("txtHomeAddress").value.Trim();//住址
        var age=document.getElementById("txtAge").value.Trim();//年龄
        var paperType=document.getElementById("txtPaperType").value.Trim();//证件类型
        var paperNum=document.getElementById("txtPaperNum").value.Trim();//证件号
        var linkType=document.getElementById("drpLinkType").value.Trim();//联系人类型
        var modifiedUserID=document.getElementById("txtModifiedUserID").value.Trim();//最后更新用户
              
         //关怀点
         var birthday=document.getElementById("txtBirthday").value.Trim();//生日
         var likes=document.getElementById("txtLikes").value.Trim();//爱好
//         var photo=document.getElementById("hfPageAttachment").value;//照片
//         var photo = document.getElementById("hfPageAttachment").value.replace(/\\/g,"\\\\");//照片
         var photo = document.getElementById("hfPagePhotoUrl").value.Trim();//照片
         var remark=document.getElementById("txtRemark").value.Trim();//备注
         
         if(document.getElementById("txtAction").value.Trim()=="1")
        {
            action="Add";
        }
        else
        {
             action="Update";
        }
        
     
         if(custNo.length<=0)
         {
            isFlag = false;
            fieldText = fieldText + "供应商名称|";
   		    msgText = msgText +  "供应商名称不允许为空|";
         }
         
         var txtIndentityID = $("#txtIndentityID").val();
         
         
//         var UrlParam = "custNo="+escape(custNo)+"&linkManName="+escape(linkManName)+"&sex="+escape(sex)+"&important="+escape(important)+"&company="+escape(company)+"\
//                        &appellation="+escape(appellation)+"&department="+escape(department)+"&position="+escape(position)+"&operation="+escape(operation)+"&workTel="+escape(workTel)+"\
//                        &fax="+escape(fax)+"&handset="+escape(handset)+"&mailAddress="+escape(mailAddress)+"&homeTel="+escape(homeTel)+"&mSN="+escape(mSN)+"\
//                        &qQ="+escape(qQ)+"&post="+escape(post)+"&homeAddress="+escape(homeAddress)+"&age="+escape(age)+"&paperType="+escape(paperType)+"\
//                        &paperNum="+escape(paperNum)+"&linkType="+escape(linkType)+"&modifiedUserID="+escape(modifiedUserID)+"&birthday="+escape(birthday)+"&likes="+escape(likes)+"\
//                        &photo="+escape(photo)+"&remark="+escape(remark)+"&action="+escape(action)+"&ID="+escape(txtIndentityID)+"";
         
         
         
        $.ajax({ 
                  type: "POST",
//                  url: "../../../Handler/Office/PurchaseManager/ProviderLinkManAdd.ashx?"+UrlParam,
                  url: "../../../Handler/Office/PurchaseManager/ProviderLinkManAdd.ashx",
                  dataType:'json',//返回json格式数据
                  data: "custNo="+escape(custNo)+"&linkManName="+escape(linkManName)+"&sex="+escape(sex)+"&important="+escape(important)+"&company="+escape(company)+"&appellation="+escape(appellation)+"&department="+escape(department)+"&position="+escape(position)+"&operation="+escape(operation)+"&workTel="+escape(workTel)+"&fax="+escape(fax)+"&handset="+escape(handset)+"&mailAddress="+escape(mailAddress)+"&homeTel="+escape(homeTel)+"&mSN="+escape(mSN)+"&qQ="+escape(qQ)+"&post="+escape(post)+"&homeAddress="+escape(homeAddress)+"&age="+escape(age)+"&paperType="+escape(paperType)+"&paperNum="+escape(paperNum)+"&linkType="+escape(linkType)+"&modifiedUserID="+escape(modifiedUserID)+"&birthday="+escape(birthday)+"&likes="+escape(likes)+"&photo="+escape(photo)+"&remark="+escape(remark)+"&action="+escape(action)+"&ID="+escape(txtIndentityID)+"",//数据
                
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
                        $("#CodingRuleControl1_txtCode").val(data.data);
                        $("#CodingRuleControl1_txtCode").attr("readonly","readonly");
                        $("#CodingRuleControl1_ddlCodeRule").attr("disabled","false");
                        
                        if(action=="Add")
                        {
                            popMsgObj.ShowMsg(data.info);
                            document.getElementById('txtIndentityID').value = data.sta;
                            document.getElementById("txtAction").value="2";
                        }
                        else
                        {
                            document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value.Trim();
                            document.getElementById("txtModifiedUserIDReal").value =  document.getElementById("txtModifiedUserID2").value.Trim();
                            document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value.Trim();
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


/*
* 相片处理
*/
function DealEmployeePhoto(flag)
{
    //flag未设置时，返回不处理
    if (flag == "undefined" || flag == "")
    {
        return;
    }
    //上传相片
    else if ("upload" == flag)
    {
        document.getElementById("uploadKind").value = "PHOTO";
        ShowUploadPhoto();
    }
    //清除相片
    else if ("clear" == flag)
    {
        document.getElementById("imgPhoto").src = "../../../Images/Pic/Pic_Nopic.jpg";  
    }
}

/*
* 上传后回调处理
*/
function AfterUploadFile(url)
{    
    if (url != "")
    {
    //debugger;
        document.getElementById("imgPhoto").src = "../../../Images/Photo/" + url;
        document.getElementById("hfPagePhotoUrl").value = url;
    }
}



////附件处理
//function DealResume(flag)
//{
//    //flag未设置时，返回不处理
//    if (flag == "undefined" || flag == "")
//    {
//        return;
//    }
//    //上传附件
//    else if ("upload" == flag)
//    {
//        ShowUploadFile();
//    }
//    //清除附件
//    else if ("clear" == flag)
//    {
//            //设置附件路径
//            document.getElementById("hfPageAttachment").value = "";
//            //下载删除不显示
//            document.getElementById("divDealResume").style.display = "none";
//            //上传显示 
//            document.getElementById("divUploadResume").style.display = "block";
//    }
//    //下载附件
//    else if ("download" == flag)
//    {
//     //获取简历路径
//            resumeUrl = document.getElementById("hfPageAttachment").value;
////            window.open("..\\..\\..\\" + resumeUrl, "_blank");
//            window.open("../../Common/DownloadFile.aspx?RelativeFilePath=" + resumeUrl, "_blank");
//    }
//}

//function AfterUploadFile(url)
//{
//    if (url != "")
//    {
//       // alert(url);
////        //设置简历路径
////        document.getElementById("hfPageResume").value = url;
//        //下载删除显示
//        document.getElementById("divDealResume").style.display = "block";
//        //上传不显示
//        document.getElementById("divUploadResume").style.display = "none";
//        
//        //设置简历路径
//        document.getElementById("hfPageAttachment").value = url;
//    }
//}

function ChangeCurreny()
{//选择币种带出汇率
    var IDExchangeRate = document.getElementById("drpCurrencyType").value.Trim();
    document.getElementById("CurrencyTypeID").value = IDExchangeRate.split('_')[0];
    document.getElementById("txtRate").value = IDExchangeRate.split('_')[1];
}

function DeleteAll()
{
    var providerID=document.getElementById("txtHidProviderID").value.Trim();

    if(providerID =="")
    {
        DeleteSignRow100();
        document.getElementById("txtProviderID").style.display="block";
        document.getElementById("txtHiddenProviderID1").style.display="none";
        alert("请选择供应商")
        document.getElementById("drpFromType").value=0;
        return false;
    }
    else
    {
        DeleteSignRow100();
        fnTotalInfo();
    }
}


function DeleteSignRow100()
{
    var signFrame = document.getElementById("dg_Log");  
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {
        for(var i=signFrame.rows.length-1;i>0;--i)
        {
            signFrame.deleteRow(i);
        }
    }
    findObj("txtTRLastIndex",document).value = 1;
}




//验证
/*
* 基本信息校验
*/
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
//    if (document.getElementById("txtAction").value=="1")
//    {
////        获取编码规则下拉列表选中项
//        codeRule = document.getElementById("CodingRuleControl1_ddlCodeRule").value;
//        //如果选中的是 手工输入时，校验编号是否输入
//        if (codeRule == "")
//        {
//            //获取输入的编号
//            employeeNo = document.getElementById("CodingRuleControl1_txtCode").value;
//            //编号必须输入
//            if (employeeNo == "")
//            {
//                isFlag = false;
//                fieldText += "供应商编号|";
//   		        msgText += "请输入供应商编号|";
//            }
//        }    
//     }         

    //不为空验证
    if(document.getElementById("txtCustName").value.Trim() == "")
    {
        isFlag = false;
        fieldText += "供应商名称|";
        msgText += "请输入供应商名称|";
    }
    if(document.getElementById("txtLinkManName").value.Trim() == "")
    {
        isFlag = false;
        fieldText += "联系人姓名|";
        msgText += "请输入联系人姓名|";
    }
    
    //限制字数
    var Operation=document.getElementById("txtOperation").value.Trim();//负责业务
    var HomeAddress=document.getElementById("txtHomeAddress").value.Trim();//住址
    var Remark=document.getElementById("txtRemark").value.Trim();//备注
    var Age=document.getElementById("txtAge").value.Trim();//年龄
    var QQ=document.getElementById("txtQQ").value.Trim();//QQ
    var MSN=document.getElementById("txtMSN").value.Trim();//MSN
    var HomeTel=document.getElementById("txtHomeTel").value.Trim();//家庭电话
    var Handset=document.getElementById("txtHandset").value.Trim();//移动电话
    var MailAddress=document.getElementById("txtMailAddress").value.Trim();//邮件地址
     var Fax=document.getElementById("txtFax").value.Trim();//传真
    
        if(strlen(Fax)>200)
    {
        isFlag = false;
        fieldText += "传真|";
   		msgText +=  "传真仅限于200个字符以内|";      
    }
    if(strlen(Operation)>50)
    {
        isFlag = false;
        fieldText += "负责业务|";
   		msgText +=  "负责业务仅限于50个字符以内|";      
    }
    if(strlen(HomeAddress)>50)
    {
        isFlag = false;
        fieldText += "住址|";
   		msgText +=  "住址仅限于50个字符以内|";      
    }
    if(strlen(Remark)>200)
    {
        isFlag = false;
        fieldText += "备注|";
   		msgText +=  "备注仅限于200个字符以内|";      
    }
    if(strlen(Age)>3)
    {
        isFlag = false;
        fieldText += "年龄|";
   		msgText +=  "年龄仅限于3个字符以内|";      
    }
    if(strlen(QQ)>20)
    {
        isFlag = false;
        fieldText += "QQ|";
   		msgText +=  "QQ仅限于20个字符以内|";      
    }
    if(strlen(MSN)>30)
    {
        isFlag = false;
        fieldText += "MSN|";
   		msgText +=  "MSN仅限于30个字符以内|";      
    }
    if(strlen(HomeTel)>200)
    {
        isFlag = false;
        fieldText += "家庭电话|";
   		msgText +=  "家庭电话仅限于200个字符以内|";      
    }
    if(strlen(Handset)>20)
    {
        isFlag = false;
        fieldText += "移动电话|";
   		msgText +=  "移动电话仅限于20个字符以内|";      
    }
    if(strlen(MailAddress)>30)
    {
        isFlag = false;
        fieldText += "邮件地址|";
   		msgText +=  "邮件地址仅限于30个字符以内|";      
    }
    

    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
    }
    return isFlag;
}


function FillProvider(providerid,providerno,providername,taketype,taketypename,carrytype,carrytypename,paytype,paytypename)//选择供应商带出部分信息
{  
    document.getElementById("txtCustName").value = providername;
    document.getElementById("txtCustNo").value = providerno;
    
    closeProviderdiv();
}

//判断是否有相同记录有返回true，没有返回false
function ExistFromBill(productid,frombillid,fromLineno)
{
    var signFrame = document.getElementById("dg_Log");  
    
    if((typeof(signFrame) == "undefined")||signFrame ==null)
    {
        return false;
    }
    for(var i=1;i<signFrame.rows.length;++i)
    {
        var productid1 = document.getElementById("txtProductID"+i).value.Trim();
        var frombillid1 = document.getElementById("txtFromBillID"+i).value.Trim();
        var fromlineno1 = document.getElementById("txtFromLineNo"+i).value.Trim();
        
        if((signFrame.rows[i].style.display!="none")&&(productid1 == productid)&&(frombillid1 == frombillid)&&(fromlineno1 == fromLineno))
        {
            return true;
        } 
    }
    return false;
}


 function Fun_FillParent_Content(id,no,productname,price,unitid,unit,taxrate,taxprice,discount,standard)
{
    var temp = popTechObj.InputObj;
    var index = parseInt(temp.split('o')[2]);
    var ProductID = 'txtProductID'+index;
    var ProductNo = 'txtProductNo'+index;
    var ProductName='txtProductName'+index;
    var UnitID = 'txtUnitID'+index;
    var Unit='txtUnitName'+index;
    var Price='txtUnitPrice'+index;
    var Standard='txtstandard'+index;
//    var ProductID = popTechObj.InputObj;
    var TaxPrice = 'txtTaxPrice'+index;
    var Discount = 'txtDiscount'+index;
    var TaxRate = 'txtTaxRate'+index;
    
    document.getElementById(TaxRate).value = taxrate ;
    document.getElementById(Discount).value = discount;
    document.getElementById(TaxPrice).value = taxprice;
    document.getElementById(Standard).value=standard;
    document.getElementById(UnitID).value = unitid;
    document.getElementById(Unit).value=unit;
    document.getElementById(ProductID).value = id;
    document.getElementById(ProductNo).value = no;
    document.getElementById(ProductName).value = productname;
    document.getElementById(Price).value = price;
    document.getElementById('divProviderInfo').style.display='none';
    
}


///*
//* 相片处理
//*/
//function DealEmployeePhoto(flag)
//{
//    //flag未设置时，返回不处理
//    if (flag == "undefined" || flag == "")
//    {
//        return;
//    }
//    //上传相片
//    else if ("upload" == flag)
//    {
//        document.getElementById("uploadKind").value = "PHOTO";
//        ShowUploadFile();
//    }
//    //清除相片
//    else if ("clear" == flag)
//    {
//        document.getElementById("imgPhoto").src = "../../../Images/Pic/Pic_Nopic.jpg";  
//    }
//}



/*单据打印*/
function ProviderLinkManPrint()
{
 var isPrint=document.getElementById('txtIndentityID').value;
   if(isPrint!=undefined&&parseInt(isPrint)>0)
   window.open("../../../Pages/PrinttingModel/PurchaseManager/ProviderLinkManPrint.aspx?ID="+document.getElementById("txtIndentityID").value);
   else
   popMsgObj.ShowMsg("请先保存单据后再打印");
       
}
