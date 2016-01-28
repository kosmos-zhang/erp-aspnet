var requestobj= new Object();
var requestparam1 = "";

//var page;
//var CustName;
//var CustNo;
//var CustClass;
//var CustClassName;


$(document).ready(function()
{
  
    $("#txtUcCustName").attr("readonly",false);
      requestobj = GetRequest();
      requestparam1 = requestobj['custid'];
      
      var page = requestobj['Pages']; 
      var para;      
      //debugger;
      switch (page)
      {      
        case "Cust_Info.aspx":
              para = "Cust_Info.aspx?CustNam="+requestobj['CustNam']+"&CustNo="+requestobj['CustNo']+"&CustClass="+requestobj['CustClass']+
              "&CustClassName="+requestobj['CustClassName']+'&currentPageIndex='+requestobj['currentPageIndex']+"&pageCount="+requestobj['pageCount']+"&ModuleID=2021102";
              document.getElementById("hCondition").value = para;               
              break;
        case "LinkMan_Info.aspx":
             $("#btn_save").hide();
              para = "LinkMan_Info.aspx?CustName="+requestobj['CustName']+"&LinkManName="+requestobj['LinkManName']+"&Handset="+requestobj['Handset']+
              "&LinkType="+requestobj['LinkType']+"&BeginDate="+requestobj['BeginDate']+"&EndDate="+requestobj['EndDate']+"&UcCustName="+requestobj['UcCustName']+
              "&Important="+requestobj['Important']+'&currentPageIndex='+requestobj['currentPageIndex']+"&pageCount="+requestobj['pageCount']+"&ModuleID=2021202";
              document.getElementById("hCondition").value = para; 
              break;
        case "CustContact_Info.aspx":
            $("#btn_save").hide();
            para = "CustContact_Info.aspx?CustName="+requestobj['CustName']+"&CustLinkMan="+requestobj['CustLinkMan']+
            "&LinkDateBegin="+requestobj['LinkDateBegin']+"&LinkDateEnd="+requestobj['LinkDateEnd']+'&currentPageIndex='+requestobj['currentPageIndex']+
            "&pageCount="+requestobj['pageCount']+"&ModuleID=2021302"+"&CustID="+requestobj['CustID'];
           document.getElementById("hCondition").value = para;
           break;
        case "CustService_Info.aspx":
            $("#btn_save").hide();
            para = "CustService_Info.aspx?CustName="+requestobj['CustName']+"&ServeType="+requestobj['ServeType']+"&Fashion="+requestobj['Fashion']+"&ServiceDateBegin="+requestobj['ServiceDateBegin']+
            "&ServiceDateEnd="+requestobj['ServiceDateEnd']+"&Title="+requestobj['Title']+"&CustLinkMan="+requestobj['CustLinkMan']+"&Executant="+requestobj['Executant']+
            '&currentPageIndex='+requestobj['currentPageIndex']+"&pageCount="+requestobj['pageCount']+"&ModuleID=2021602"+"&CustID="+requestobj['CustID'];
            document.getElementById("hCondition").value = para;
            break;
        case "CustComplain_Info.aspx":
            $("#btn_save").hide();
            para = "CustComplain_Info.aspx?CustName="+requestobj['CustName']+"&ComplainType="+requestobj['ComplainType']+"&Critical="+requestobj['Critical']+
                "&ComplainBegin="+requestobj['ComplainBegin']+"&ComplainEnd="+requestobj['ComplainEnd']+"&Title="+requestobj['Title']+"&CustLinkMan="+requestobj['CustLinkMan']+
                "&EmplNameL="+requestobj['EmplNameL']+"&State="+requestobj['State']+'&currentPageIndex='+requestobj['currentPageIndex']+"&pageCount="+requestobj['pageCount']+
                "&ModuleID=2021702"+'&CustID='+requestobj['CustID'];
            document.getElementById("hCondition").value = para;
            break;
        case "CustLove_Info.aspx":
            $("#btn_save").hide();
            para = "CustLove_Info.aspx?CustName="+requestobj['CustName']+"&LoveType="+requestobj['LoveType']+"&CustLinkMan="+requestobj['CustLinkMan']+"&LoveBegin="+requestobj['LoveBegin']+
                "&LoveEnd="+requestobj['LoveEnd']+"&Title="+requestobj['Title']+'&currentPageIndex='+requestobj['currentPageIndex']+"&pageCount="+requestobj['pageCount']+"&ModuleID=2021502"+
                "&CustID="+requestobj['CustID'];
               document.getElementById("hCondition").value = para; 
               break;
        case "CustTalk_Info.aspx":
            $("#btn_save").hide();
            para = "CustTalk_Info.aspx?CustName="+requestobj['CustName']+"&TalkType="+requestobj['TalkType']+"&Priority="+requestobj['Priority']+"&TalkBegin="+requestobj['TalkBegin']+
                "&TalkEnd="+requestobj['TalkEnd']+"&Title="+requestobj['Title']+"&Status="+requestobj['Status']+'&currentPageIndex='+requestobj['currentPageIndex']+"&pageCount="+
                requestobj['pageCount']+"&ModuleID=2021402"+"&CustID="+requestobj['CustID'];
            document.getElementById("hCondition").value = para;
            break;
        case "ServiceSellAnnal_Info.aspx":
            $("#btn_save").hide();
            para = "ServiceSellAnnal_Info.aspx?CID="+requestobj['CID']+"&CustName="+requestobj['CustName']+"&ProductID="+requestobj['ProductID']+"&DateBegin="+requestobj['DateBegin']+
                "&ProductName="+requestobj['ProductName']+"&DateEnd="+requestobj['DateEnd']+'&currentPageIndex='+requestobj['currentPageIndex']+"&pageCount="+requestobj['pageCount']+"&ModuleID=2021603";
            document.getElementById("hCondition").value = para;
            break;
        case "ContactDefer_Info.aspx":
             $("#btn_save").hide();
             para = "ContactDefer_Info.aspx?currentPageIndex="+requestobj['currentPageIndex']+"&pageCount="+requestobj['pageCount']+"&ModuleID=2021303";
             document.getElementById("hCondition").value = para;
             break;
        case "LinkRemind.aspx":
             $("#btn_save").hide();
             para = "LinkRemind.aspx?days="+requestobj['days']+"&currentPageIndex="+requestobj['currentPageIndex']+"&pageCount="+requestobj['pageCount']+"&ModuleID=2021206";
             document.getElementById("hCondition").value = para;
             break;
         
        default:
            $("#btn_save").hide();
            para = "Cust_Info.aspx?CustNam="+requestobj['CustNam']+"&CustNo="+requestobj['CustNo']+"&CustClass="+requestobj['CustClass']+"&CustClassName="+requestobj['CustClassName']+
                '&currentPageIndex='+requestobj['currentPageIndex']+"&pageCount="+requestobj['pageCount']+"&ModuleID=2021102";
            document.getElementById("hCondition").value = para;
            break; 
      }
    
      GetEquipmentInfo(requestparam1);
      
        fnGetExtAttr();  
});

//返回
function Back()
{ 
    window.location.href=document.getElementById("hCondition").value;
}

function PagePrint()
{
     var Url = document.getElementById("txtCustNo").value;
     if(Url == "")
     {
        popMsgObj.Show("打印|","请保存单据再打印|");
        return;
     }
       var keyList=fnGetExtAttrValue();
//    if(confirm("请确认您的单据已经保存？"))
//    {    
     window.open("../../PrinttingModel/CustManager/CustAddPrint.aspx?id=" + Url+"&keyList="+keyList);
//    }
}


  //////获取客户信息(修改、查看)
 function GetEquipmentInfo(requestparam1)
{
       var retval = requestparam1;
       $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:"../../../Handler/Office/CustManager/CustEdit.ashx",//目标地址
       data:'id='+reescape(retval),
       cache:false,
       beforeSend:function(){AddPop();},//发送数据之前
       success: function(msg){
                //数据获取完毕，填充页面据显示
                $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                        $("#ddlCustType").val(item.CustType);
                                                
                        $("#CustClassDrpControl1_CustClassHidden").val(item.CustClass);//客户分类 
                        $("#CustClassDrpControl1_CustClass").val(item.CustClassName);//客户分类名                       
    
                        $("#txtCustNo").val(item.CustNo);//客户编号                        
                        $("#txtCustName").val(item.CustName);//客户名称
                        $("#txtCustNam").val(item.CustNam);
                        $("#txtCustShort").val(item.CustShort);                       
                        $("#ddlCreditGrade").val(item.CreditGrade);//客户优质级别
                        $("#txtJoinUser").val(item.Manager); //销售人员ID
                        $("#UserLinker").val(item.ManagerName); //销售人员Name
                        $("#ddlArea").val(item.AreaID); //区域
                        $("#txtCustNote").val(item.CustNote);//客户简介                       
                        $("#ddlLinkCycle").val(item.LinkCycle);//联络期限 
                        $("#seleHotIs").val(item.HotIs);//热点客户
                        $("#seleHotHow").val(item.HotHow);                        
                        $("#SeleCustTypeManage").val(item.CustTypeManage);
                        $("#SeleCustTypeSell").val(item.CustTypeSell);
                        $("#SeleCustTypeTime").val(item.CustTypeTime);
                        $("#txtFirstBuyDate").val(item.FirstBuyDate);
                        
                        $("#seleMeritGrade").val(item.MeritGrade);
                        $("#seleRelaGrade").val(item.RelaGrade);
                        $("#txtRelation").val(item.Relation);                         
                        $("#txtCompanyType").val(item.CompanyType);//单位性质
                        if(item.StaffCount != 0)
                        {
                            $("#txtStaffCount").val(item.StaffCount);
                        }
                        $("#txtSource").val(item.Source);
                        $("#txtPhase").val(item.Phase);//阶段
                        $("#txtUcCustName").val(item.CustSupe);
                        $("#txtTrade").val(item.Trade);//行业
                        $("#txtSetupDate").val(item.SetupDate);//成立时间
                        $("#txtArtiPerson").val(item.ArtiPerson);//法人代表
                        $("#txtSetupMoney").val(item.SetupMoney);//注册资本
                        $("#txtSetupAddress").val(item.SetupAddress);
                        $("#txtCapitalScale").val(item.CapitalScale);//资产规模
                        $("#txtSaleroomY").val(item.SaleroomY);
                        $("#txtProfitY").val(item.ProfitY);
                        $("#txtTaxCD").val(item.TaxCD);
                        $("#txtBusiNumber").val(item.BusiNumber);//营业执照号
                        $("#seleIsTax").val(item.IsTax);
                        $("#txtSellMode").val(item.SellMode);
                        $("#txtSellArea").val(item.SellArea);                         
                        $("#ddlCountry").val(item.CountryID);//国家地区
                        $("#txtProvince").val(item.Province);
                        $("#txtCity").val(item.City);
                        $("#txtTel").val(item.Tel);
                        $("#txtContactName").val(item.ContactName);
                        $("#txtMobile").val(item.Mobile);//手机
                        $("#txtReceiveAddress").val(item.ReceiveAddress);
                        $("#txtWebSite").val(item.WebSite);
                        $("#txtPost").val(item.Post);
                        $("#txtemail").val(item.email);
                        $("#txtFax").val(item.Fax);
                        $("#txtOnLine").val(item.OnLine);                        
                        $("#ddlTakeType").val(item.TakeType);//交货方式                        
                        $("#ddlCarryType").val(item.CarryType);//运送方式
                        $("#seleBusiType").val(item.BusiType);
                        $("#seleBillType").val(item.BillType);                        
                        $("#ddlPayType").val(item.PayType);//结算方式                       
                        $("#ddlMoneyType").val(item.MoneyType);                        
                        $("#ddlCurrencyType").val(item.CurrencyType);
                        $("#seleCreditManage").val(item.CreditManage);
                        $("#txtMaxCredit").val(item.MaxCredit);
                        
                        $("#txtMaxCreditDate").val(item.MaxCreditDate);//帐期天数
                        if(item.CreditManage == "1")
                        {
                            $("#txtMaxCreditDate").hide();
                        }
                        else
                        {
                            $("#txtMaxCreditDate").show();
                        }
                        
                        $("#seleUsedStatus").val(item.UsedStatus);
                        $("#txtModifiedUser").val(item.ModifiedUserID);
                        $("#txtModifiedDate").val(item.ModifiedDate);

                        $("#txtCreator").val(item.CreatorName);
                    
                        $("#txtCreatedDate").val(item.CreatedDate);
                        $("#txtOpenBank").val(item.OpenBank);
                        $("#txtAccountMan").val(item.AccountMan);
                        $("#txtAccountNum").val(item.AccountNum);
                        $("#txtRemark").val(item.Remark);
                
                
                        $("#CatchWord").val(item.CatchWord);
                        $("#ManageValues").val(item.ManageValues);
                        $("#Potential").val(item.Potential);
                        $("#Problem").val(item.Problem);
                        $("#Advantages").val(item.Advantages);
                        $("#TradePosition").val(item.TradePosition);
                        $("#Competition").val(item.Competition);
                        $("#Collaborator").val(item.Collaborator);
                        $("#ManagePlan").val(item.ManagePlan);
                        $("#Collaborate").val(item.Collaborate);
                        $("#CompanyValues").val(item.CompanyValues);
                        
                        var aaa = item.CanViewUser.replace(",","");
                        var bbb = aaa.lastIndexOf(",");
                        var ccc = aaa.slice(0,bbb);                        
                        $("#txtCanUserID").val(ccc);
                        $("#txtCanUserName").val(item.CanViewUserName);
                        //if(item.EquipmentID!="")//生成明细
                            //AddSignRow(item.FittingNO,item.FittingName,item.FittingDescription);
               });
              },
       error: function() 
       {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
       }, 
       complete:function(){hidePopup();}//接收数据完毕
       });
}

function SaveCustData()
{
    if(!CheckInput())
    {
        return;
    }

    var CustType=$("#ddlCustType").val(); //客户类别
    
    var CustClass=$("#CustClassDrpControl1_CustClassHidden").val();//客户分类
    var CustNoType = "";
    var CustNo = document.getElementById("txtCustNo").value; //获取编号   
    var CustName = $("#txtCustName").val();//客户名称
    var CustNam = $("#txtCustNam").val();
    var CustShort = $("#txtCustShort").val();
    var CreditGrade = $("#ddlCreditGrade").val();  //客户优质级别    
    var Seller = $("#txtJoinUser").val();//销售人员 
    var AreaID = $("#ddlArea").val();//区域
    var CustNote = $("#txtCustNote").val();//客户简介
    var LinkCycle = $("#ddlLinkCycle").val();//联络期限
    var HotIs = $("#seleHotIs").val();//热点客户
    var HotHow = $("#seleHotHow").val();
    var HotType = $("#seleHotType").val();
    var MeritGrade = $("#seleMeritGrade").val();
    var RelaGrade = $("#seleRelaGrade").val();
    var Relation = $("#txtRelation").val();
    var CompanyType = $("#txtCompanyType").val();
    var StaffCount = $("#txtStaffCount").val();
    var Source = $("#txtSource").val();
    var Phase = $("#txtPhase").val();//阶段
    var CustSupe = $("#txtUcCustName").val();
    var Trade = $("#txtTrade").val();//行业
    var SetupDate = $("#txtSetupDate").val();//成立时间
    var ArtiPerson = $("#txtArtiPerson").val();//法人代表
    var SetupMoney = $("#txtSetupMoney").val();//注册资本
    var SetupAddress = $("#txtSetupAddress").val();
    var CapitalScale = $("#txtCapitalScale").val();//资产规模
    var SaleroomY = $("#txtSaleroomY").val();
    var ProfitY = $("#txtProfitY").val();
    var TaxCD = $("#txtTaxCD").val();
    var BusiNumber = $("#txtBusiNumber").val();//营业执照号
    var IsTax = $("#seleIsTax").val();
    var SellMode = $("#txtSellMode").val();
    var SellArea = $("#txtSellArea").val();
    var CountryID = $("#ddlCountry").val(); //国家地区
    var Province = $("#txtProvince").val();
    var City = $("#txtCity").val();
    var Tel = $("#txtTel").val();
    var ContactName = $("#txtContactName").val();
    var Mobile = $("#txtMobile").val();//手机
    var ReceiveAddress = $("#txtReceiveAddress").val();
    var WebSite = $("#txtWebSite").val();//公司网址
    var Post = $("#txtPost").val();//邮编
    var email = $("#txtemail").val();
    var Fax = $("#txtFax").val();
    var OnLine = $("#txtOnLine").val();
    var TakeType = $("#ddlTakeType").val();//交货方式
    var CarryType =  $("#ddlCarryType").val();//运送方式
    var BusiType = $("#seleBusiType").val();
    var BillType = $("#seleBillType").val();
    var PayType = $("#ddlPayType").val();//结算方式
    var MoneyType = $("#ddlMoneyType").val();//支付方式
    var CurrencyType = $("#ddlCurrencyType").val();//结算币种
    var CreditManage = $("#seleCreditManage").val();
    var MaxCredit = $("#txtMaxCredit").val();
    var MaxCreditDate = $("#txtMaxCreditDate").val();    
    var UsedStatus = $("#seleUsedStatus").val();
    //var Creator = $("#ddlCreator").val();
    var CreatedDate = $("#txtCreatedDate").val();
    var OpenBank = $("#txtOpenBank").val();
    var AccountMan = $("#txtAccountMan").val();
    var AccountNum = $("#txtAccountNum").val();
    var Remark = $("#txtRemark").val();

    var CustTypeManage = $("#SeleCustTypeManage").val();//客户管理分类
    var CustTypeSell = $("#SeleCustTypeSell").val();//客户营销分类
    var CustTypeTime = $("#SeleCustTypeTime").val();//客户时间分类
    var FirstBuyDate = $("#txtFirstBuyDate").val();//首次交易日

    var CanUserID = $("#txtCanUserID").val();//可查看该客户档案的人员ID
    var CanUserName = $("#txtCanUserName").val();//可查看该客户档案的人员姓名   
            var CatchWord = $("#CatchWord").val();//账号
        var ManageValues = $("#ManageValues").val();
        var Potential = $("#Potential").val();
        var Problem = $("#Problem").val();
        var Advantages = $("#Advantages").val();//账号
        var TradePosition = $("#TradePosition").val();
        var Competition = $("#Competition").val();
        var Collaborator = $("#Collaborator").val();
        var ManagePlan = $("#ManagePlan").val();//账号
        var Collaborate = $("#Collaborate").val();
                var CompanyValues = $("#CompanyValues").val();
    
     $.ajax({
        type: "POST",
        url: "../../../Handler/Office/CustManager/CustAdd.ashx",
        data:'CustType='+reescape(CustType)+'&CustClass='+reescape(CustClass)+'&CustNoType='+reescape(CustNoType)+'&CustNo='+reescape(CustNo)+'&CustName='+reescape(CustName)+'&CustNam='+reescape(CustNam)+'&CustShort='+reescape(CustShort)+
            '&CreditGrade='+reescape(CreditGrade)+'&Seller='+reescape(Seller)+'&AreaID='+reescape(AreaID)+'&CustNote='+reescape(CustNote)+'&LinkCycle='+reescape(LinkCycle)+'&HotIs='+reescape(HotIs)+
            '&HotHow='+reescape(HotHow)+'&HotType='+reescape(HotType)+'&MeritGrade='+reescape(MeritGrade)+'&RelaGrade='+reescape(RelaGrade)+'&Relation='+reescape(Relation)+'&CompanyType='+reescape(CompanyType)+
            '&StaffCount='+reescape(StaffCount)+'&Source='+reescape(Source)+'&Phase='+reescape(Phase)+'&CustSupe='+reescape(CustSupe)+'&Trade='+reescape(Trade)+'&SetupDate='+reescape(SetupDate)+
            '&ArtiPerson='+reescape(ArtiPerson)+'&SetupMoney='+reescape(SetupMoney)+'&SetupAddress='+reescape(SetupAddress)+'&CapitalScale='+reescape(CapitalScale)+'&SaleroomY='+reescape(SaleroomY)+
            '&ProfitY='+reescape(ProfitY)+'&TaxCD='+reescape(TaxCD)+'&BusiNumber='+reescape(BusiNumber)+'&IsTax='+reescape(IsTax)+'&SellMode='+reescape(SellMode)+'&SellArea='+reescape(SellArea)+'&CountryID='+reescape(CountryID)+
            '&Province='+reescape(Province)+'&City='+reescape(City)+'&Tel='+reescape(Tel)+'&ContactName='+reescape(ContactName)+'&Mobile='+reescape(Mobile)+'&ReceiveAddress='+reescape(ReceiveAddress)+'&WebSite='+reescape(WebSite)+'&Post='+reescape(Post)+
            '&email='+reescape(email)+'&Fax='+reescape(Fax)+'&OnLine='+reescape(OnLine)+'&TakeType='+reescape(TakeType)+'&CarryType='+reescape(CarryType)+'&BusiType='+reescape(BusiType)+'&BillType='+reescape(BillType)+'&PayType='+reescape(PayType)+
            '&MoneyType='+reescape(MoneyType)+'&CurrencyType='+reescape(CurrencyType)+'&CreditManage='+reescape(CreditManage)+'&MaxCredit='+reescape(MaxCredit)+'&MaxCreditDate='+reescape(MaxCreditDate)+'&UsedStatus='+reescape(UsedStatus)+
            '&CreatedDate='+reescape(CreatedDate)+'&Remark='+reescape(Remark)+'&OpenBank='+reescape(OpenBank)+'&AccountMan='+reescape(AccountMan)+'&AccountNum='+reescape(AccountNum)+
            '&CustTypeManage='+reescape(CustTypeManage)+'&CustTypeSell='+reescape(CustTypeSell)+'&CustTypeTime='+reescape(CustTypeTime)+'&FirstBuyDate='+reescape(FirstBuyDate)+'&action='+reescape(2)+
            '&CanUserID='+escape(CanUserID)+'&CanUserName='+escape(CanUserName)+'&CatchWord='+escape(CatchWord)+'&ManageValues='+escape(ManageValues)+ '&Potential='+escape(Potential)+'&Problem='+escape(Problem)+ '&Advantages='+escape(Advantages)+'&TradePosition='+escape(TradePosition)+ '&Competition='+escape(Competition)+'&Collaborator='+escape(Collaborator)+
             '&ManagePlan='+escape(ManagePlan)+'&Collaborate='+escape(Collaborate)+'&CompanyValues='+escape(CompanyValues)+fnGetExtAttrValue(),             
        dataType:'json',//返回json格式数据
        cache:false,
        beforeSend:function()
          { 
              AddPop();
          },
          error: function() 
          {
                //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
                popMsgObj.Show("请求","请求发生错误！");
          },
          success:function(data) 
            { 
                if(data.sta==1) 
                { 
                     //保存成功后 不能修改客户编号
                     //编码规则DIV不可见
                     hidePopup();
                     showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
                } 
                else 
                { 
                  hidePopup();                  
                  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
                } 
            }
     });
}

//若是中文则自动填充拼音缩写
function LoadPYShort()
{
    var txtCustNam = $("#txtCustName").val().Trim();   
    if(txtCustNam.length>0 && isChinese(txtCustNam))
    {
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Common/PYShort.ashx?Text="+reescape(txtCustNam),
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
                    document.getElementById('txtCustShort').value = data.info;
                  } 
               });
     }
}

function DivMaxcShow()
{
    if(document.getElementById("seleCreditManage").value == "2")//允许延期付款选“是”时
    {
        document.getElementById("divMaxC").style.display = "block";
         $("#txtMaxCreditDate").show();
    }
    else
    {
        document.getElementById("divMaxC").style.display = "none";
        $("#txtMaxCreditDate").hide();
    }
}

//主表单验证
function CheckInput()
{    
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var txtCustType = document.getElementById('ddlCustType').value;//客户类别    
    //var txtCustClass = $("#CustClassHidden").val();//客户分类
    var CustClassName = $("#CustClassDrpControl1_CustClass").val();//客户分类
    var txtStaffCount = document.getElementById("txtStaffCount").value;// 人员规模    
    var txtCustName = document.getElementById("txtCustName").value; //客户名称
    var txtSeller = document.getElementById("txtJoinUser").value; //销售人员
    var JoinUser = document.getElementById("UserLinker").value; //分管业务员名
 
    var ddlCreator = document.getElementById("txtCreator").value; //建单人
    var txtCreatedDate = document.getElementById("txtCreatedDate").value; //建单日期
    var txtSetupMoney = document.getElementById("txtSetupMoney").value; //注册资本
    var txtCapitalScale = document.getElementById("txtCapitalScale").value; //资产规模
    var txtSaleroomY = document.getElementById("txtSaleroomY").value; //年销售额
    var txtProfitY =  document.getElementById("txtProfitY").value; //年利润额
    var txtMaxCredit = document.getElementById("txtMaxCredit").value; //信用额度
    //var txtUsedCredit = document.getElementById("txtUsedCredit").value; //已使用信用额度    
    var txtCustNote = document.getElementById("txtCustNote").value;//客户简介
    var txtSetupDate = document.getElementById("txtSetupDate").value;//成立日期
    var txtRelation = document.getElementById("txtRelation").value; //关系描述
    var txtSellArea = document.getElementById("txtSellArea").value; //经营范围
    //var txtUpdateCredit = document.getElementById("txtUpdateCredit").value; //信用额度最后更新日期
    var txtCreatedDate = document.getElementById("txtCreatedDate").value; //创建日期
    var txtRemark = document.getElementById("txtRemark").value; //备注
    var txtCustShort = document.getElementById("txtCustShort").value.Trim(); //拼音缩写
    
    var CreditManage = document.getElementById("seleCreditManage").value; //是否允许延期付款
    var MaxCreditDate = document.getElementById("txtMaxCreditDate").value; //帐期天数
    var RetVal=CheckSpecialWords();
            var CatchWord = $("#CatchWord").val();//账号
        var ManageValues = $("#ManageValues").val();
        var Potential = $("#Potential").val();
        var Problem = $("#Problem").val();
        var Advantages = $("#Advantages").val();//账号
        var TradePosition = $("#TradePosition").val();
        var Competition = $("#Competition").val();
        var Collaborator = $("#Collaborator").val();
        var ManagePlan = $("#ManagePlan").val();//账号
        var Collaborate = $("#Collaborate").val();
                var CompanyValues = $("#CompanyValues").val();
    if(CreditManage == "2" && MaxCreditDate.length <= 0)
    {
        isFlag = false;
        fieldText = fieldText +  "帐期天数|"; 
   		msgText = msgText +  "允许延期付款时,请填入帐期天数|";
    }
    if(MaxCreditDate.length > 0 && !IsNumber(MaxCreditDate))
    {
        isFlag = false;
        fieldText = fieldText +  "帐期天数|"; 
   		msgText = msgText +  "帐期天数必须为整数|";
    }
    
    //是否为英文字符     
	if(txtCustShort != "" && txtCustShort.match(/^[A-Za-z]+$/) == null)
	 {
	    isFlag = false;
	    fieldText = fieldText +  "拼音缩写|";
   		msgText = msgText +  "拼音缩写必须为英文字母|";
	 }
//    if(txtStaffCount =="")
//    {
//        isFlag = false;
//        fieldText = fieldText +  "员工总数|";
//	    msgText = msgText + "请输入员工总数|";
//    }
    if(txtStaffCount.length>0)
    {
        if(PositiveInteger(txtStaffCount) == null)
        {
            isFlag = false;
            fieldText = fieldText +  "员工总数|";
   		    msgText = msgText + "员工总数必须为正整数|";  
        }
    }   
    if(txtCustName.Trim() == "")
    {
        isFlag = false;
        fieldText = fieldText + "客户名称|";
   		msgText = msgText +  "请输入客户名称|";
    }
    
    if(txtSeller == "" || JoinUser == "")
    {
        isFlag = false;
         fieldText = fieldText + "分管业务员|";
   		msgText = msgText +  "请选择分管业务员|";
    }
    
    if(txtCreatedDate=="")
    {
        isFlag = false;
        fieldText = fieldText + "建单日期|";
   		msgText = msgText +  "请输入建单日期|";
    }
//     if(txtSetupMoney=="")
//    {
//        isFlag = false;
//        fieldText = fieldText +  "注册资本|";
//	    msgText = msgText + "请输入注册资本|";
//    }
    if(txtSetupMoney.length>0)
    {
        if(!IsNumeric(txtSetupMoney,12,4))
        {
            isFlag = false;
            fieldText = fieldText +  "注册资本|";
   		    msgText = msgText + "注册资本精度输入的格式不正确|";  
        }
        if(parseInt(txtSetupMoney).toString().length > 10)
        {
            isFlag = false;
            fieldText = fieldText +  "注册资本|";
   		    msgText = msgText + "注册资本整数部分过长|";
        }
    }
//     if(txtCapitalScale=="")
//    {       
//   		isFlag = false;
//        fieldText = fieldText +  "资产规模|";
//	    msgText = msgText + "请输入资产规模|";  
//    }
    if(txtCapitalScale.length>0)
    {
        if(!IsNumeric(txtCapitalScale,12,4))
        {
            isFlag = false;
            fieldText = fieldText +  "资产规模|";
   		    msgText = msgText + "资产规模精度输入的格式不正确|";  
        }
        if(parseInt(txtCapitalScale).toString().length > 10)
        {
            isFlag = false;
   		    msgText = msgText + "资产规模整数部分过长|";
        }
    }
//    if(txtSaleroomY=="")
//    {
//        isFlag = false;
//        fieldText = fieldText +  "年销售额|";
//	    msgText = msgText + "请输入年销售额|";
//    }
    if(txtSaleroomY.length>0)
    {
        if(!IsNumeric(txtSaleroomY,12,4))
        {
            isFlag = false;
            fieldText = fieldText +  "年销售额|";
   		    msgText = msgText + "年销售额精度输入的格式不正确|";  
        }
        if(parseInt(txtSaleroomY).toString().length > 10)
        {
            isFlag = false;
   		    msgText = msgText + "年销售额整数部分过长|";
        }
    }
    if(txtProfitY.length>0)
    {
        if(!IsNumeric(txtProfitY,12,4))
        {
            isFlag = false;
            fieldText = fieldText +  "年利润额|";
   		    msgText = msgText + "年利润额精度输入的格式不正确|";  
        }
        if(parseInt(txtProfitY).toString().length > 10)
        {
            isFlag = false;
            fieldText = fieldText +  "年利润额|";
   		    msgText = msgText + "年利润额整数部分过长|";
        }
    }

    if(txtMaxCredit.length>0)
    {
        if(!IsNumeric(txtMaxCredit,12,2))
        {
            isFlag = false;
            fieldText = fieldText +  "信用额度|";
   		    msgText = msgText + "信用额度精度输入的格式不正确|";  
        }
        if(parseInt(txtMaxCredit).toString().length > 10)
        {
            isFlag = false;
            fieldText = fieldText +  "信用额度|";
   		    msgText = msgText + "信用额度整数部分过长|";
        }
    }   
    if(strlen(txtCustNote)>1024)
    {
        isFlag = false;
        fieldText = fieldText + "客户简介|";
   		msgText = msgText + "客户简介最多只允许输入1024个字符|";
    }
//    if(txtSetupDate=="")
//    {       
//        isFlag = false;
//         fieldText = fieldText + "成立日期|";
//	     msgText = msgText + "请选择成立日期|";       
//    }
    if(txtSetupDate.length>0)
    {
        if(!isDate(txtSetupDate))
        {
            isFlag = false;
             fieldText = fieldText +  "成立日期|";
   		     msgText = msgText + "成立日期格式不正确|";    
        }
    }
    if(txtCreatedDate.length>0)
    {
        if(!isDate(txtCreatedDate))
        {
            isFlag = false;
             fieldText = fieldText +  "建单日期|";
   		     msgText = msgText + "建单日期格式不正确|";    
        }
    }
    if(CatchWord != "")
    {
        if(!CheckSpecialWord(CatchWord))
        {
            isFlag = false;
           fieldText = fieldText + "企业口号|";
   		    msgText = msgText +  "企业口号不能含有特殊字符|";
        }
    }
    if(strlen(CatchWord) > 500)
    {
        isFlag = false;
        fieldText = fieldText + "企业口号|";
   		msgText = msgText + "企业口号最多只允许输入500个字符|";
    }
    if(ManageValues != "")
    {
        if(!CheckSpecialWord(ManageValues))
        {
            isFlag = false;
           fieldText = fieldText + "企业文化概述|";
   		    msgText = msgText +  "企业文化概述不能含有特殊字符|";
        }
    }
    if(strlen(ManageValues) > 500)
    {
        isFlag = false;
        fieldText = fieldText + "企业文化概述|";
   		msgText = msgText + "企业文化概述最多只允许输入500个字符|";
    }
    if(Potential != "")
    {
        if(!CheckSpecialWord(Potential))
        {
            isFlag = false;
           fieldText = fieldText + "发展潜力|";
   		    msgText = msgText +  "发展潜力不能含有特殊字符|";
        }
    }
        if(strlen(Potential) > 500)
    {
        isFlag = false;
        fieldText = fieldText + "发展潜力|";
   		msgText = msgText + "发展潜力最多只允许输入500个字符|";
    }
        if(Problem != "")
    {
        if(!CheckSpecialWord(Problem))
        {
            isFlag = false;
           fieldText = fieldText + "存在问题|";
   		    msgText = msgText +  "存在问题不能含有特殊字符|";
        }
    }
    if(strlen(Problem) > 500)
    {
        isFlag = false;
        fieldText = fieldText + "存在问题|";
   		msgText = msgText + "存在问题最多只允许输入500个字符|";
    }
        if(Advantages != "")
    {
        if(!CheckSpecialWord(Advantages))
        {
            isFlag = false;
           fieldText = fieldText + "市场优劣势|";
   		    msgText = msgText +  "市场优劣势不能含有特殊字符|";
        }
    }
       if(strlen(Advantages) > 500)
    {
        isFlag = false;
        fieldText = fieldText + "市场优劣势|";
   		msgText = msgText + "市场优劣势最多只允许输入500个字符|";
    }
        if(TradePosition != "")
    {
        if(!CheckSpecialWord(TradePosition))
        {
            isFlag = false;
           fieldText = fieldText + "行业地位|";
   		    msgText = msgText +  "行业地位不能含有特殊字符|";
        }
    }
           if(strlen(TradePosition) > 500)
    {
        isFlag = false;
        fieldText = fieldText + "行业地位|";
   		msgText = msgText + "行业地位最多只允许输入500个字符|";
    }
        if(Competition != "")
    {
        if(!CheckSpecialWord(Competition))
        {
            isFlag = false;
           fieldText = fieldText + "竞争对手|";
   		    msgText = msgText +  "竞争对手不能含有特殊字符|";
        }
    }
   if(strlen(Competition) > 500)
    {
        isFlag = false;
        fieldText = fieldText + "竞争对手|";
   		msgText = msgText + "竞争对手最多只允许输入500个字符|";
    }
        if(Collaborator != "")
    {
        if(!CheckSpecialWord(Collaborator))
        {
            isFlag = false;
           fieldText = fieldText + "合作伙伴|";
   		    msgText = msgText +  "合作伙伴不能含有特殊字符|";
        }
    }
       if(strlen(Collaborator) > 500)
    {
        isFlag = false;
        fieldText = fieldText + "合作伙伴|";
   		msgText = msgText + "合作伙伴最多只允许输入500个字符|";
    }
        if(ManagePlan != "")
    {
        if(!CheckSpecialWord(ManagePlan))
        {
            isFlag = false;
           fieldText = fieldText + "发展计划|";
   		    msgText = msgText +  "发展计划不能含有特殊字符|";
        }
    }
           if(strlen(ManagePlan) > 500)
    {
        isFlag = false;
        fieldText = fieldText + "发展计划|";
   		msgText = msgText + "发展计划最多只允许输入500个字符|";
    }
        if(Collaborate != "")
    {
        if(!CheckSpecialWord(Collaborate))
        {
            isFlag = false;
           fieldText = fieldText + "合作方法|";
   		    msgText = msgText +  "合作方法不能含有特殊字符|";
        }
    }
               if(strlen(Collaborate) > 500)
    {
        isFlag = false;
        fieldText = fieldText + "合作方法|";
   		msgText = msgText + "合作方法最多只允许输入500个字符|";
    }
        if(CompanyValues != "")
    {
        if(!CheckSpecialWord(CompanyValues))
        {
            isFlag = false;
           fieldText = fieldText + "经营理念|";
   		    msgText = msgText +  "经营理念不能含有特殊字符|";
        }
    } 
                   if(strlen(CompanyValues) > 500)
    {
        isFlag = false;
        fieldText = fieldText + "经营理念|";
   		msgText = msgText + "经营理念最多只允许输入500个字符|";
    } 
    if(strlen(txtRelation)> 200)
    {
        isFlag = false;
        fieldText = fieldText + "关系描述|";
   		msgText = msgText + "关系描述最多只允许输入200个字符|";
    }
    if(strlen(txtSellArea) > 200)
    {
        isFlag = false;
         fieldText = fieldText + "经营范围|";
   		msgText = msgText + "经营范围最多只允许输入200个字符|";
    }    
    if(strlen(txtRemark)> 200)
    {
        isFlag = false;
        fieldText = fieldText + "备注|";
   		msgText = msgText + "备注最多只允许输入200个字符|";
    }
    
    if(RetVal!="")
    {
        isFlag = false;
        fieldText = fieldText + RetVal+"|";
	    msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
    
     if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
        
        //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msgText);
    }

    return isFlag;    
}

//--------------------扩展属性操作----------------------------------------------------------------------------//
/*
*林飞
*获取扩展属性，初始化页面
*2009-08-13
*/
function fnGetExtAttr() {

    var strKey = ''; //使用字段集合

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: "../../../Handler/Office/CustManager/TableExtFieldsInfo.ashx", //目标地址
        cache: false,
        data: 'action=all',

        beforeSend: function() { AddPop(); }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //存在扩展属性显示页面扩展属性表格
            if (parseInt(msg.totalCount) > 0) {
                $("#ExtDiv").css("display", "");
                var ControlID = ''; //控件id
                var Coutrol = ""; //控件的html代码
                var Cell = ""; //列
            
                $.each(msg.data, function(i, item) {


                    strKey += "|" + "ExtField" + item.EFIndex; //使用字段集合
                    ControlID = "ExtField" + item.EFIndex; //控件id
                    //控件的类型，文本框
                    if (item.EFType == '1') {
                        Coutrol = "<input id='" + ControlID + "' specialworkcheck='"+item.EFDesc+"' class='tdinput' type='text' style='width:99%;height: 20px' maxlength='128' />";
                    }
                    //控件的类型，列表
                    else if (item.EFType == '2') {
                        Coutrol = "<select id='" + ControlID + "' style='height: 20px'>"
                        Coutrol += "<option selected='selected' value=''>--请选择--</option>";
                        var arr = ("|" + item.EFValueList).split('|');
                        //添加列表的值
                        for (var y = 0; y < arr.length; y++) {
                            //不为空的列表值才添加
                            if ($.trim(arr[y]) != '') {
                                Coutrol += "<option value='" + $.trim(arr[y]) + "'>" + $.trim(arr[y]) + "</option>";
                            }
                        }
                        Coutrol += "</select>";
                    }
             
                        Cell += "<td align='right' height='20' class='tdColTitle' width='10%'>" + item.EFDesc + "</td>" +
                            "<td  colspan='5'  class='tdColInput'>" + Coutrol + "</td>";
                        $("<tr></tr>").append("" + Cell + "").appendTo($("#ExtTab tbody"));
                        Cell = "";
                     
                  
                });     
                 
            
            
            }
            $("#hiddKey").val(strKey);
            fnSetExtAttrValue();
        },
        error: function() { popMsgObj.ShowMsg('请求发生错误！'); },
        complete: function() { hidePopup(); } //接收数据完毕
    });
}
/*
*林飞
*保存时获取扩展属性
*2009-08-13
*/
function fnGetExtAttrValue() {
    var strKey = $.trim($("#hiddKey").val()); //扩展属性字段值
    var strValue = "";
    //有扩展属性才取值
    if (strKey != '') {
        var arrKey = strKey.split('|');
        //取得扩展属性值
        for (var y = 0; y < arrKey.length; y++) {
            //不为空的字段名才取值
            if ($.trim(arrKey[y]) != '') {
                strValue += "&" + $.trim(arrKey[y]) + "=" + escape($("#" + $.trim(arrKey[y])).val());
            }
        }
        strValue += "&keyList=" + escape(strKey);
    }
    return strValue;
}

//修改页面初始化扩展属性值
function fnSetExtAttrValue() {
    var strKey = $.trim($("#hiddKey").val()); //扩展属性字段值

    var strCustNo = $.trim($("#txtCustNo").val());


    if (strCustNo != '') {
        //有扩展属性才取值
        if (strKey != '') {
            $.ajax({
                type: "POST", //用POST方式传输
                dataType: "json", //数据格式:JSON
                url: "../../../Handler/Office/CustManager/CustEdit.ashx", //目标地址
                cache: false,
                data: 'action=extValue&keyList=' + escape(strKey) + "&strCustNo=" + escape(strCustNo),

                beforeSend: function() { AddPop(); }, //发送数据之前

                success: function(msg) {
                    //数据获取完毕，填充页面据显示
                    //存在扩展属性显示页面扩展属性表格
                    if (parseInt(msg.totalCount) > 0) {


                        $.each(msg.data, function(i, item) {

                            var arrKey = strKey.split('|');
                            for (var t = 0; t < arrKey.length; t++) {
                                //不为空的字段名才取值
                                if ($.trim(arrKey[t]) != '') {
                                    $("#" + $.trim(arrKey[t])).val(item[$.trim(arrKey[t])]);

                                }
                            }

                        });

                    }

                },
                error: function() { popMsgObj.ShowMsg('请求发生错误！'); },
                complete: function() { hidePopup(); } //接收数据完毕
            });
        }
    }
}
//--------------------扩展属性操作----------------------------------------------------------------------------//
