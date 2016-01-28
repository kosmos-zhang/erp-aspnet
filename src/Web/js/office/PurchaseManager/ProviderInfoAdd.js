//采购新建供应商档案
var DtlCount = 0;

var page = "";
var CustNo ;
var CustNam ;
var CustType ;
var Isliebiao ;

$(document).ready(function()
{
    requestobj = GetRequest();
    var intMasterProviderID = document.getElementById("txtIndentityID").value;
    
    recordnoparam = intMasterProviderID.toString();
    
    var CustNo = requestobj['CustNo'];
    var CustName = requestobj['CustName'];
    var CustNam = requestobj['CustNam'];
    var PYShort = requestobj['PYShort'];
    var CustType = requestobj['CustType'];
    var CustClass = requestobj['CustClass'];
    var CustClassName = requestobj['CustClassName'];
    var AreaID = requestobj['AreaID'];
    var CreditGrade = requestobj['CreditGrade'];
    var Manager = requestobj['Manager'];
    var ManagerName = requestobj['ManagerName'];
    var StartCreateDate = requestobj['StartCreateDate'];
    var EndCreateDate = requestobj['EndCreateDate'];
    
    var Isliebiao = requestobj['Isliebiao'];
    var PageIndex = requestobj['PageIndex'];
    var PageCount = requestobj['PageCount'];
    
    if(typeof(Isliebiao)!="undefined")
    {
        $("#btn_back").show();
        document.getElementById("hidIsliebiao").value = Isliebiao;
        document.getElementById("TableDTB").style.display="block";
        document.getElementById("TableDTN").style.display="block";
    }
    else
    {
        document.getElementById("TableDTB").style.display="none";
        document.getElementById("TableDTN").style.display="none";
    }
    
    var ModuleID = document.getElementById("hidModuleID").value;
    var URLParams = "Isliebiao="+escape(Isliebiao)+"&ModuleID="+escape(ModuleID)+"&CustNo="+escape(CustNo)+"&CustName="+escape(CustName)+"&CustNam="+escape(CustNam)+"&PYShort="+escape(PYShort)+
                     "&CustType="+escape(CustType)+"&CustClass="+escape(CustClass)+"&CustClassName="+escape(CustClassName)+"&AreaID="+escape(AreaID)+"&CreditGrade="+escape(CreditGrade)+
                     "&Manager="+escape(Manager)+"&ManagerName="+escape(ManagerName)+"&StartCreateDate="+escape(StartCreateDate)+"&EndCreateDate="+escape(EndCreateDate)+"&PageIndex="+escape(PageIndex)+"&PageCount="+escape(PageCount)+""; 
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
            
//            if(typeof(recordnoparam)!="undefined")
            if(recordnoparam !=0)
            {
//                $("#btn_back").show();
                //isnew="2";
                //$("#hfLoveID").attr("value",recordnoparam);//ID记录在隐藏域中
                document.getElementById("txtAction").value="2";
                document.getElementById("divTitle").innerText="供应商档案";
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
    var Isliebiao = document.getElementById("hidIsliebiao").value;
    if(Isliebiao==1)
    {
        var URLParams = document.getElementById("hidSearchCondition").value;
            window.location.href='ProviderInfoInfo.aspx?'+URLParams;
    }
    else
    {//为2时表示用联络警告页面链接过来查看供应商信息
        var Isliebiao = 2;
        var URLParams = "Isliebiao="+escape(Isliebiao)+""; 
        window.location.href='ProviderContactHistoryWarning.aspx?'+URLParams;
    }
}

function GetProviderInfo(ID)
{
     $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:"../../../Handler/Office/PurchaseManager/ProviderInfoEdit.ashx",//目标地址
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
                        //$("#CodingRuleControl1").attr("value",item.CustNo);//合同编号
                        $("#CodingRuleControl1_txtCode").attr("value",item.CustNo);//供应商编号
                        $("#divProviderInfoNo").attr("value",item.CustNo);//供应商编号
                        $("#drpCustType").attr("value",item.CustType);//供应商类别
                        $("#txtCustClass").attr("value",item.CustClass);//供应商分类id
                        $("#txtCustClassName").attr("value",item.CustClassName);//供应商分类名称
                        $("#txtCustName").attr("value",item.CustName);//供应商名称
                        $("#txtPYShort").attr("value",item.PYShort);//供应商拼音代码
                        $("#txtCustNam").attr("value",item.CustNam);//供应商简称
                        
                        //辅助信息
                        $("#drpCreditGrade").attr("value",item.CreditGrade);//供应商优质级别(名称)id
                        $("#HidManager").attr("value",item.Manager);//分管采购员id
                        $("#UsertxtManager").attr("value",item.ManagerName);//分管采购员名称
                        $("#drpAreaID").attr("value",item.AreaID);//所在区域(名称)id
                        $("#drpLinkCycle").attr("value",item.LinkCycle);//联络期限
                        $("#drpHotIs").attr("value",item.HotIs);//热点供应商
                        $("#drpHotHow").attr("value",item.HotHow);//热度
                        $("#drpMeritGrade").attr("value",item.MeritGrade);//价值评估
                        $("#drpCompanyType").attr("value",item.CompanyType);//单位性质
                        $("#txtStaffCount").attr("value",item.StaffCount);//员工总数
                        $("#txtSetupDate").attr("value",item.SetupDate);//成立时间
                        $("#txtArtiPerson").attr("value",item.ArtiPerson);//法人代表
                        $("#txtSetupMoney").attr("value", jqControl(item.SetupMoney)); //注册资本(万元)
                        $("#txtCapitalScale").attr("value",jqControl(item.CapitalScale));//资产规模(万元)
                        $("#txtSaleroomY").attr("value",jqControl(item.SaleroomY));//年销售额(万元)
                        $("#txtProfitY").attr("value",jqControl(item.ProfitY));//年利润额(万元)
                        $("#txtTaxCD").attr("value",item.TaxCD);//税务登记号
                        $("#txtBusiNumber").attr("value",item.BusiNumber);//营业执照号
                        $("#drpIsTax").attr("value",item.drpIsTax);//一般纳税人
                        $("#txtSetupAddress").attr("value",item.SetupAddress);//注册地址
                        $("#txtCustNote").attr("value",item.CustNote);//供应商简介
                        var ggg = item.AllowDefaultDays==0?"":item.AllowDefaultDays;
                        $("#AllowDefaultDays").val(ggg);
                        
                        //业务信息
//                        $("#drpCountryID").attr("title",item.CountryName);//国家地区名称
                        $("#drpCountryID").attr("value",item.CountryID);//国家地区id
                        $("#txtProvince").attr("value",item.Province);//省
                        $("#txtCity").attr("value",item.City);//市(县)
                        $("#txtPost").attr("value",item.Post);//邮政编码
                        $("#txtContactName").attr("value",item.ContactName);//联系人
                        $("#txtTel").attr("value",item.Tel);//电话
                        $("#txtFax").attr("value",item.Fax);//传真
                        $("#txtMobile").attr("value",item.Mobile);//手机 
                        $("#txtemail").attr("value",item.email);//邮件 
                        
                        $("#txtOnLine").attr("value",item.OnLine);//在线咨询
                        $("#txtWebSite").attr("value",item.WebSite);//公司网址
                        $("#drpTakeType").attr("value",item.TakeType);//交货方式
                        $("#drpCarryType").attr("value",item.CarryType);//运送方式
                        $("#drpPayType").attr("value",item.PayType);//结算方式
                        $("#drpCurrencyType").attr("value",item.CurrencyType);//币种
                        $("#txtOpenBank").attr("value",item.OpenBank);//开户行
                        $("#txtAccountMan").attr("value",item.AccountMan);//户名
                        $("#txtAccountNum").attr("value",item.AccountNum);//帐号
                        $("#drpUsedStatus").attr("value",item.UsedStatus);//启用状态
                        $("#HidCreator").attr("value",item.Creator);//建档人ID
                        $("#txtCreatorReal").attr("value",item.CreatorName);//建档人名称
                        $("#txtCreateDate").attr("value",item.CreateDate);//建档日期
                        $("#txtModifiedDate").attr("value",item.ModifiedDate);//最后更新时间
                        $("#txtSellArea").attr("value",item.SellArea);//经营范围
                        $("#txtSendAddress").attr("value",item.SendAddress);//发货地址
                        $("#txtRemark").attr("value",item.Remark);//备注
                        
                        //动态信息
                        $("#txtPTotalPrice").attr("value",jqControl(item.PTotalPrice));//已开票金额(元)
                        $("#txtPYAccounts").attr("value",jqControl(item.PYAccounts));//已付款总金额(元)
                        $("#txtPNAccounts").attr("value",jqControl(item.PNAccounts));//应付款总金额(元)
                        $("#txtDCountTotal").attr("value",jqControl(item.DCountTotal));//订单数量合计
//                        $("#txtTCountTotal").attr("value",item.TCountTotal);//退货数量合计
//                        $("#txtTYAccounts").attr("value",item.TYAccounts);//已退货总金额(元)
                        
                        
                        document.getElementById("divProviderInfoNo").innerHTML=item.CustNo;
                        document.getElementById("divProviderInfoNo").style.display="block";
                        document.getElementById("divInputNo").style.display="none";
                    }
                        
               });
//               $.each(msg.data2,function(i,item){
////                    if(item.CustNo != null && item.CustNo != "")
//                        if(item.ID != null && item.ID != "")
//                        {
//                            var index = AddSignRow();
//                            $("#txtProductID"+index).attr("value",item.ProductID);
//                            $("#txtProductNo"+index).attr("value",item.ProductNo);
//                            $("#txtProductName"+index).attr("value",item.ProductName);
//                            $("#txtUnitID"+index).attr("value",item.UnitID);
//                            $("#txtUnitName"+index).attr("value",item.UnitName);
//                            $("#txtProductCount"+index).attr("value",item.ProductCount);
//                            $("#txtBackCount"+index).attr("value",item.BackCount);
//                            $("#txtUnitPrice"+index).attr("value",item.UnitPrice);
//                            $("#txtTaxPrice"+index).attr("value",item.TaxPrice);
//                            $("#txtDiscount"+index).attr("value",item.Discount);
//                            $("#txtTaxRate"+index).attr("value",item.TaxRate);
//                            $("#txtTotalPrice"+index).attr("value",item.TotalPrice);
//                            $("#txtTotalFee"+index).attr("value",item.TotalFee);
//                            $("#txtTotalTax"+index).attr("value",item.TotalTax);
//                            $("#txtRemark"+index).attr("value",item.Remark);
//                            $("#txtFromBillID"+index).attr("value",item.FromBillID);
//                            $("#txtFromBillNo"+index).attr("value",item.FromBillNo);
//                            $("#txtFromLineNo"+index).attr("value",item.FromLineNo);
//                        }
//               });
              },
       error: function() 
       {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
       }, 
       complete:function(){hidePopup();}//接收数据完毕
       });
}







//新建供应商档案
function InsertProviderInfo() 
{ 

if(!CheckBaseInfo())return;

        var action = null;

         var fieldText = "";
         var msgText = "";
         var isFlag = true;
         var custNo = "";
         var CodeType=$("#CodingRuleControl1_ddlCodeRule").val();
         
         //基本信息
   
         if (CodeType == "")
         {
            custNo=$("#CodingRuleControl1_txtCode").val();
         }
         var custType=document.getElementById("drpCustType").value;//供应商类别
         var custClassName=document.getElementById("txtCustClassName").value.Trim();//供应商分类
         var custClass=document.getElementById("txtCustClass").value.Trim();//供应商分类
         var custName=document.getElementById("txtCustName").value.Trim();//供应商名称
         var custNam=document.getElementById("txtCustNam").value.Trim();//供应商简称
         var pYShort=document.getElementById("txtPYShort").value.Trim();//供应商拼音代码
        
        //辅助信息
        var creditGrade=document.getElementById("drpCreditGrade").value.Trim();//供应商优质级别
        var manager=document.getElementById("HidManager").value.Trim();//分管采购员id
        var managerName=document.getElementById("UsertxtManager").value.Trim();//分管采购员名称
        var areaID=document.getElementById("drpAreaID").value.Trim();//所在区域
        var linkCycle=document.getElementById("drpLinkCycle").value.Trim();//联络期限
        var hotIs=document.getElementById("drpHotIs").value.Trim();//热点供应商
        var hotHow=document.getElementById("drpHotHow").value.Trim();//热度
        var meritGrade=document.getElementById("drpMeritGrade").value.Trim();//价值评估
        var companyType=document.getElementById("drpCompanyType").value.Trim();//单位性质
        var staffCount=document.getElementById("txtStaffCount").value.Trim();//员工总数
        var setupDate=document.getElementById("txtSetupDate").value.Trim();//成立时间
        var artiPerson=document.getElementById("txtArtiPerson").value.Trim();//法人代表
        var setupMoney=document.getElementById("txtSetupMoney").value.Trim();//注册资本(万元)
        var capitalScale=document.getElementById("txtCapitalScale").value.Trim();//资产规模(万元)
        var saleroomY=document.getElementById("txtSaleroomY").value.Trim();//年销售额(万元)
        var profitY=document.getElementById("txtProfitY").value.Trim();//年利润额(万元)
        var taxCD=document.getElementById("txtTaxCD").value.Trim();//税务登记号
        var busiNumber=document.getElementById("txtBusiNumber").value.Trim();//营业执照号
        var isTax=document.getElementById("drpIsTax").value.Trim();//一般纳税人
        var setupAddress=document.getElementById("txtSetupAddress").value.Trim();//注册地址
        var custNote=document.getElementById("txtCustNote").value.Trim();//供应商简介 
        var AllowDefaultDays = $("#AllowDefaultDays").val();
        if(AllowDefaultDays == "")
        {
            AllowDefaultDays = 0;
        } 
              
         //业务信息
         var countryID=document.getElementById("drpCountryID").value;//国家地区
         var province=document.getElementById("txtProvince").value.Trim();//省
         var city=document.getElementById("txtCity").value.Trim();//市(县)
         var post=document.getElementById("txtPost").value.Trim();//邮编
         var contactName=document.getElementById("txtContactName").value.Trim();//联系人
         var tel=document.getElementById("txtTel").value.Trim();//电话
         var fax=document.getElementById("txtFax").value.Trim();//传真
         var mobile=document.getElementById("txtMobile").value.Trim();//手机
         var email=document.getElementById("txtemail").value.Trim();//邮件
         var onLine=document.getElementById("txtOnLine").value.Trim();//在线咨询
         var webSite=document.getElementById("txtWebSite").value.Trim();//公司网址
         var takeType=document.getElementById("drpTakeType").value.Trim();//交货方式
         var carryType=document.getElementById("drpCarryType").value.Trim();//运送方式
         var payType=document.getElementById("drpPayType").value.Trim();//结算方式
         var currencyType=document.getElementById("drpCurrencyType").value.Trim();//币种
         var openBank=document.getElementById("txtOpenBank").value.Trim();//开户行
         var accountMan=document.getElementById("txtAccountMan").value.Trim();//户名
         var accountNum=document.getElementById("txtAccountNum").value.Trim();//帐号
         var usedStatus=document.getElementById("drpUsedStatus").value.Trim();//启用状态
         var creator=document.getElementById("HidCreator").value.Trim();//建档人id
         var creatorName=document.getElementById("txtCreatorReal").value.Trim();//建档人名称
         var createDate=document.getElementById("txtCreateDate").value.Trim();//建档日期
         var modifiedUserID=document.getElementById("txtModifiedUserID").value.Trim();//最后更新用户ID
         var modifiedUserName=document.getElementById("txtModifiedUserIDReal").value.Trim();//最后更新用户名称
         var sellArea=document.getElementById("txtSellArea").value.Trim();//经营范围
         var sendAddress=document.getElementById("txtSendAddress").value.Trim();//发货地址
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
            fieldText = fieldText + "供应商档案编号|";
   		    msgText = msgText +  "供应商档案编号不允许为空|";
         }
         

         var no= document.getElementById("divProviderInfoNo").innerHTML;
         var txtIndentityID = $("#txtIndentityID").val();
         
//         var UrlParam = "custNo="+escape(custNo)+"&custType="+escape(custType)+"&custClass="+escape(custClass)+"&custName="+escape(custName)+"&custNam="+escape(custNam)+"\
//                        &pYShort="+escape(pYShort)+"&creditGrade="+escape(creditGrade)+"&manager="+escape(manager)+"&managerName="+escape(managerName)+"&areaID="+escape(areaID)+"\
//                        &linkCycle="+escape(linkCycle)+"&hotIs="+escape(hotIs)+"&hotHow="+escape(hotHow)+"&meritGrade="+escape(meritGrade)+"&companyType="+escape(companyType)+"\
//                        &staffCount="+escape(staffCount)+"&setupDate="+escape(setupDate)+"&artiPerson="+escape(artiPerson)+"&setupMoney="+escape(setupMoney)+"&capitalScale="+escape(capitalScale)+"\
//                        &saleroomY="+escape(saleroomY)+"&profitY="+escape(profitY)+"&taxCD="+escape(taxCD)+"&busiNumber="+escape(busiNumber)+"&isTax="+escape(isTax)+"\
//                        &setupAddress="+escape(setupAddress)+"&custNote="+escape(custNote)+"&countryID="+escape(countryID)+"&province="+escape(province)+"&city="+escape(city)+"&post="+escape(post)+"\
//                        &contactName="+escape(contactName)+"&tel="+escape(tel)+"&fax="+escape(fax)+"&mobile="+escape(mobile)+"\
//                        &email="+escape(email)+"&onLine="+escape(onLine)+"&webSite="+escape(webSite)+"&takeType="+escape(takeType)+"&carryType="+escape(carryType)+"\
//                        &payType="+escape(payType)+"&currencyType="+escape(currencyType)+"&openBank="+escape(openBank)+"&accountMan="+escape(accountMan)+"&accountNum="+escape(accountNum)+"&usedStatus="+escape(usedStatus)+"\
//                        &creator="+escape(creator)+"&creatorName="+escape(creatorName)+"&createDate="+escape(createDate)+"&modifiedUserID="+escape(modifiedUserID)+"&modifiedUserName="+escape(modifiedUserName)+"\
//                        &sellArea="+escape(sellArea)+"&sendAddress="+escape(sendAddress)+"&remark="+escape(remark)+"&action="+escape(action)+"\
//                        &CodeType="+escape(CodeType)+"&cno="+escape(no)+"&ID="+escape(txtIndentityID)+"";
         
         
         
        $.ajax({ 
                  type: "POST",
//                  url: "../../../Handler/Office/PurchaseManager/ProviderInfoAdd.ashx?"+UrlParam,
                  url: "../../../Handler/Office/PurchaseManager/ProviderInfoAdd.ashx",
                  dataType:'json',//返回json格式数据
                  data: "custNo="+escape(custNo)+"&custType="+escape(custType)+"&custClass="+escape(custClass)+"&custName="+escape(custName)+"&custNam="+escape(custNam)+"\
                        &pYShort="+escape(pYShort)+"&creditGrade="+escape(creditGrade)+"&manager="+escape(manager)+"&managerName="+escape(managerName)+"&areaID="+escape(areaID)+"\
                        &linkCycle="+escape(linkCycle)+"&hotIs="+escape(hotIs)+"&hotHow="+escape(hotHow)+"&meritGrade="+escape(meritGrade)+"&companyType="+escape(companyType)+"\
                        &staffCount="+escape(staffCount)+"&setupDate="+escape(setupDate)+"&artiPerson="+escape(artiPerson)+"&setupMoney="+escape(setupMoney)+"&capitalScale="+escape(capitalScale)+"\
                        &saleroomY="+escape(saleroomY)+"&profitY="+escape(profitY)+"&taxCD="+escape(taxCD)+"&busiNumber="+escape(busiNumber)+"&isTax="+escape(isTax)+"\
                        &setupAddress="+escape(setupAddress)+"&custNote="+escape(custNote)+"&AllowDefaultDays="+escape(AllowDefaultDays)+"&countryID="+escape(countryID)+"&province="+escape(province)+"&city="+escape(city)+"&post="+escape(post)+"\
                        &contactName="+escape(contactName)+"&tel="+escape(tel)+"&fax="+escape(fax)+"&mobile="+escape(mobile)+"\
                        &email="+escape(email)+"&onLine="+escape(onLine)+"&webSite="+escape(webSite)+"&takeType="+escape(takeType)+"&carryType="+escape(carryType)+"\
                        &payType="+escape(payType)+"&currencyType="+escape(currencyType)+"&openBank="+escape(openBank)+"&accountMan="+escape(accountMan)+"&accountNum="+escape(accountNum)+"&usedStatus="+escape(usedStatus)+"\
                        &creator="+escape(creator)+"&creatorName="+escape(creatorName)+"&createDate="+escape(createDate)+"&modifiedUserID="+escape(modifiedUserID)+"&modifiedUserName="+escape(modifiedUserName)+"\
                        &sellArea="+escape(sellArea)+"&sendAddress="+escape(sendAddress)+"&remark="+escape(remark)+"&action="+escape(action)+"\
                        &CodeType="+escape(CodeType)+"&cno="+escape(no)+"&ID="+escape(txtIndentityID)+"",//数据
                
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
                            document.getElementById('txtIndentityID').value= data.sta;
                            document.getElementById("txtAction").value="2";
                           if(CodeType!="")
                           {
                                isnew="edit";
                                document.getElementById("divProviderInfoNo").innerHTML=data.data; 
                                document.getElementById("divProviderInfoNo").style.display="block";
                                document.getElementById("divInputNo").style.display="none";
                           }
                           else
                           {
                               document.getElementById("divProviderInfoNo").innerHTML=data.data;
                               document.getElementById("divProviderInfoNo").style.display="block";
                               document.getElementById("divInputNo").style.display="none";
                                
                           }
                         
                        }
                        else
                        {
                              document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value;
                              document.getElementById("txtModifiedUserIDReal").value =  document.getElementById("txtModifiedUserID2").value;
                              document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value;
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

//附件处理
function DealResume(flag)
{
    //flag未设置时，返回不处理
    if (flag == "undefined" || flag == "")
    {
        return;
    }
    //上传附件
    else if ("upload" == flag)
    {
        ShowUploadFile();
    }
    //清除附件
    else if ("clear" == flag)
    {
            //设置附件路径
            document.getElementById("hfPageAttachment").value = "";
            //下载删除不显示
            document.getElementById("divDealResume").style.display = "none";
            //上传显示 
            document.getElementById("divUploadResume").style.display = "block";
    }
    //下载附件
    else if ("download" == flag)
    {
     //获取简历路径
            resumeUrl = document.getElementById("hfPageAttachment").value.Trim();
            window.open("..\\..\\..\\" + resumeUrl, "_blank");
    }
}

function AfterUploadFile(url)
{
    if (url != "")
    {
       // alert(url);
//        //设置简历路径
//        document.getElementById("hfPageResume").value = url;
        //下载删除显示
        document.getElementById("divDealResume").style.display = "block";
        //上传不显示
        document.getElementById("divUploadResume").style.display = "none";
        
        //设置简历路径
        document.getElementById("hfPageAttachment").value = url;
    }
}

function ChangeCurreny()
{//选择币种带出汇率
    var IDExchangeRate = document.getElementById("drpCurrencyType").value;
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
var regu =/^[1]([3|5|8][0-9]{1}|59|58)[0-9]{8}$/;
var re = new RegExp(regu);
if (re.test(s)) {
   return true;
}else{
   return false;
}
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
    if (document.getElementById("txtAction").value.Trim()=="1")
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
                fieldText += "供应商编号|";
   		        msgText += "请输入供应商编号|";
            }
            else
            {
                if(!CodeCheck($.trim($("#CodingRuleControl1_txtCode").val())))
                {
                    isFlag = false;
                    fieldText = fieldText + "供应商编号|";
   		            msgText = msgText +  "供应商编号只能由英文字母（a-z大小写均可）、数字（0-9）、字符（_-/.()[]{}）组成|";
                }
                else if(strlen($.trim($("#CodingRuleControl1_txtCode").val())) > 50)
                {
                    isErrorFlag = true;
                    fieldText += "供应商编号|";
   		            msgText += "供应商编号长度不大于50|";
   		        }
   		        
            }
        }    
     }       
     
     //判断手机号和电话号码
    var Tel=document.getElementById("txtTel").value.Trim();//电话
    var Mobile=document.getElementById("txtMobile").value.Trim();//手机
    var Fax = document.getElementById("txtFax").value.Trim();//传真
    if(Tel!="")
    {
        if(isvalidtel(Tel))
        {
            isFlag = false;
            fieldText +=  "电话|";
            msgText +=   "电话格式不正确|";      
        }
    }
    if(Mobile!="")
    {
        if(!checkMobile(Mobile))
        {
          isFlag = false;
          fieldText = fieldText + "手机|";
   	      msgText = msgText +  "手机格式不正确|";
        }
    }
    if(Fax!="")
   {
        if(isvalidtel(Fax))
        {
          isFlag = false;
            fieldText = fieldText + "传真|";
            msgText = msgText +  "传真格式不正确|";
        }
   }
     
    //不为空验证
    if(document.getElementById("txtCustName").value.Trim() == "")
    {
        isFlag = false;
        fieldText += "供应商名称|";
        msgText += "请输入供应商名称|";
    }

    //整单折扣验证是否为数字
    if(document.getElementById("txtStaffCount").value.Trim() !="")
    {
        if(fucCheckNUM(document.getElementById("txtStaffCount").value.Trim()) == false)
        {
            isFlag = false;
            fieldText += "员工总数|";
            msgText += "请输入正确的员工总数|";
        }
    }
    if(document.getElementById("txtSetupMoney").value.Trim() != "")
    {
        if(IsNumberOrNumeric(document.getElementById("txtSetupMoney").value.Trim(),12,4) == false)
        {
            isFlag = false;
            fieldText += "注册资本|";
            msgText += "请输入正确的注册资本|";
        }
    }
    if(document.getElementById("txtCapitalScale").value.Trim() != "")
    {
        if(IsNumberOrNumeric(document.getElementById("txtCapitalScale").value.Trim(),12,4) == false)
        {
            isFlag = false;
            fieldText += "资产规模|";
            msgText += "请输入正确的资产规模|";
        }
    }
    if(document.getElementById("txtSaleroomY").value.Trim() != "")
    {
        if(IsNumberOrNumeric(document.getElementById("txtSaleroomY").value.Trim(),12,4) == false)
        {
            isFlag = false;
            fieldText += "年销售额|";
            msgText += "请输入正确的年销售额|";
        }
    }
    if(document.getElementById("txtProfitY").value.Trim() !="")
    {
        if(IsNumberOrNumeric(document.getElementById("txtProfitY").value.Trim(),12,4) == false)
        {
            isFlag = false;
            fieldText += "年利润额|";
            msgText += "请输入正确的年利润额|";
        }
    }
    
    //限制字数
    var CustNote=document.getElementById("txtCustNote").value.Trim();//供应商简介
    var SellArea=document.getElementById("txtSellArea").value.Trim();//经营范围
    var Remark=document.getElementById("txtRemark").value.Trim();//备注
    var Post=document.getElementById("txtPost").value.Trim();//邮政编码
    if(strlen(CustNote)>1024)
    {
        isFlag = false;
        fieldText += "供应商简介|";
   		msgText +=  "供应商简介仅限于1024个字符以内|";      
    }
    if(strlen(SellArea)>200)
    {
        isFlag = false;
        fieldText += "经营范围|";
   		msgText +=  "经营范围仅限于200个字符以内|";      
    }
    if(strlen(Remark)>200)
    {
        isFlag = false;
        fieldText += "备注|";
   		msgText +=  "备注仅限于200个字符以内|";      
    }
    if(strlen(Post)>20)
    {
        isFlag = false;
        fieldText += "邮编|";
   		msgText +=  "邮编仅限于20个字符以内|";      
    }
    
    
    
    
//    //明细来源的校验
//    var signFrame = document.getElementById("dg_Log");
//    if((typeof(signFrame) == "undefined")||signFrame==null)
//    {
//        isFlag = false;
//        fieldText +="明细来源|";
//        msgText +="明细来源不能为空|";
//    }
//    else
//    {
//       
//         for(var i=1;i<signFrame.rows.length;++i)
//        {
//            if(signFrame.rows[i].style.display!="none")
//            {
//                var BackCount = "txtBackCount"+i;
//                var no = document.getElementById(BackCount).value;
//                if(IsNumeric(no,12,4) == false)
//                {
//                    isFlag = false;
//                    fieldText +="退货数量|";
//                    msgText +="请输入正确的退货数量|";
//                }
//                var UnitPrice = "txtUnitPrice" + i;
//                var no1 = document.getElementById(UnitPrice).value;
//                if(IsNumeric(no1,12,4) == false)
//                {
//                    isFlag = false;
//                    fieldText +="单价|";
//                    msgText +="请输入正确的单价数量";
//                }
//                var TotalPrice = "txtTotalPrice" + i;
//                var no2 = document.getElementById(TotalPrice).value;
//                if(IsNumeric(no2,12,4) == false)
//                {
//                    isFlag = false;
//                    fieldText +="金额小计|";
//                    msgText +="请输入正确的金额小计数量";
//                }
//            }
//        }
//    }
    

    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
    }
    return isFlag;
}

//若是中文则自动填充拼音缩写
function LoadPYShort()
{
    var txtCustNam = $("#txtCustNam").val();
    if(txtCustNam.length>0 && isChinese(txtCustNam))
    {
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Common/PYShort.ashx?Text="+escape(txtCustNam),
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
                    document.getElementById('txtPYShort').value = data.info;
                  } 
               });
     }
     else
     {
        document.getElementById('txtPYShort').value = "";
     }
}

function TotalMoney()
{
    var signFrame = findObj("dg_Log",document); 
    var txtTRLastIndex = findObj("txtTRLastIndex",document);
    var total = "";
    if(parseInt(txtTRLastIndex.value)>0)
    {
        for(var i=1;i<signFrame.rows.length;++i)
        {
          parseInt(total)+=document.getElementById("txtTotalPrice"+i).value.Trim();
        }
        document.getElementById("txtTotalMoney").value=total;
        
    }
    else
    {
        document.getElementById("txtTotalMoney").value=total;
    }
}

function FillProvider(providerid,providerno,providername,taketype,taketypename,carrytype,carrytypename,paytype,paytypename)//选择供应商带出部分信息
{  
    document.getElementById("txtProviderID").value = providername;
    document.getElementById("txtHidProviderID").value = providerid;
    document.getElementById("txtHiddenProviderID1").value = providername;//当选择源单类型时，用于显示供应商并且不允许再修改
    if (taketype=="")
    {
    taketype="0";
    }
       if (carrytype=="")
    {
    carrytype="0";
    }
       if (paytype=="")
    {
    paytype="0";
    }
    document.getElementById("drpTakeType").value = taketype;
    document.getElementById("drpCarryType").value = carrytype;
    document.getElementById("drpPayType").value= paytype;
    
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


function FillUnit(unitid,unitname) //回填单位
{
    var i = popUnitObj.InputObj;
    var UnitID = "txtUnitID"+i;
    var UnitName = "txtUnitName"+i;
    document.getElementById(UnitID).value = unitid;
    document.getElementById(UnitName).value = unitname;
}



/*单据打印*/
function ProviderInfoPrint()
{
    var isPrint=document.getElementById('txtIndentityID').value;
   if(isPrint!=undefined&&parseInt(isPrint)>0)
   window.open("../../../Pages/PrinttingModel/PurchaseManager/ProviderInfoPrint.aspx?ID="+document.getElementById("txtIndentityID").value);
   else
   popMsgObj.ShowMsg("请先保存单据后再打印");

}



function jqControl(value) {
    var ret = "";
    var point = document.getElementById("HiddenPoint").value;
    if (value != null && value != "" && value != undefined) {
        ret = parseFloat(value).toFixed(point);
    }
    return ret;
}
