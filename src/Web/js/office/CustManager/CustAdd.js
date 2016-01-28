var LinkMan_Item = new Array();//存储联系人数组
var arrlength=0;
var page = "";
var CustNam = "";
var CustNo = "";
var CustClass = "";
var CustClassName = "";
var currentPageIndex = "";
var pageCount = "";
var isnew="1";//1添加;2保存



$(document).ready(function()
{
    alerCustType();//客户细分
    fnGetExtAttr();//客户扩展属性
    
      $("#txtMaxCreditDate").hide();//设置账期天数不可输入
      $("#txtUcCustName").attr("readonly",false);
      
      requestobj = GetRequest();
      requestparam1=requestobj['custid'];
      custbig = requestobj['custbig'];
      custno = requestobj['custno'];

      page = requestobj['Pages'];
      CustNam = requestobj['CustNam'];
      CustNo = requestobj['CustNo'];
      CustClass = requestobj['CustClass'];
      CustClassName = requestobj['CustClassName'];
      currentPageIndex = requestobj['currentPageIndex'];
      pageCount = requestobj['pageCount'];
      
         
      if(typeof(requestparam1)!="undefined")
    {    
    
        document.getElementById("selCustBig").disabled = true; //客户大类不可改
    
        //显示返回按钮
        $("#btn_back").show();
        
        if(requestparam1 != -1)
        {
            GetCustInfo(requestparam1,custbig,custno);            
             
        }
        
        var para;
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
    }
});

//返回
function Back()
{ 
    window.location.href=page+'?CustNam='+CustNam+'&CustNo='+CustNo+'&CustClass='+CustClass+'&CustClassName='+CustClassName+
    '&currentPageIndex='+currentPageIndex+'&pageCount='+pageCount+'&ModuleID=2021102';
}

function CustSwitch()
{
    if($("#selCustBig").val() == '2')//选择个人客户时
    {       
        document.getElementById("txtCustNum").style.display = "block";//卡号
        document.getElementById("divCustNum").style.display = "block";//卡号标签
        
        document.getElementById("divSex").style.display = "block";//性别标签
        document.getElementById("divseleSex").style.display = "block";//性别
        
        document.getElementById("divLinkType").style.display = "block";//联系人类型标签
        document.getElementById("divddlLinkType").style.display = "block";//联系人类型
        document.getElementById("txtProvince").style.display = "none";//省      隐藏
        document.getElementById("divProvince").style.display = "none";//省标签
        
        document.getElementById("divPaperNum").style.display = "block";//身份证标签
        document.getElementById("txtPaperNum").style.display = "block";//身份证
         document.getElementById("txtCity").style.display = "none";//市      隐藏
        document.getElementById("divCity").style.display = "none";//市标签
        
        document.getElementById("divBirthday").style.display = "block";//生日标签
        document.getElementById("txtBirthday").style.display = "block";//生日
         document.getElementById("txtContactName").style.display = "none";//联系人      隐藏
        document.getElementById("divContactName").style.display = "none";//联系人标签
        
        document.getElementById("divPosition").style.display = "block";//职务标签
        document.getElementById("txtPosition").style.display = "block";//职务
         document.getElementById("txtOnLine").style.display = "none";//在线咨询      隐藏
        document.getElementById("divOnLine").style.display = "none";//在线咨询标签
        
        document.getElementById("divAge").style.display = "block";//年龄标签
        document.getElementById("txtAge").style.display = "block";//年龄
          document.getElementById("txtWebSite").style.display = "none";//公司网址      隐藏
        document.getElementById("divWebSite").style.display = "none";//公司网址标签
        
        document.getElementById("divHomeTown").style.display = "block";//籍贯标签
        document.getElementById("txtHomeTown").style.display = "block";//籍贯
         document.getElementById("txtFirstBuyDate").style.display = "none";//首次交易日期      隐藏
        document.getElementById("divFirstBuyDate").style.display = "none";//首次交易日期标签
        
        document.getElementById("divNational").style.display = "block";//民族标签       
        document.getElementById("ddlNational").style.display = "block";//民族
        document.getElementById("divddlCarryType").style.display = "none";//运送方式      隐藏
        document.getElementById("divCarryType").style.display = "none";//运送方式标签
        
         document.getElementById("divCulture").style.display = "block";//所受教育标签       
        document.getElementById("ddlCulture").style.display = "block";//所受教育
        document.getElementById("divddlTakeType").style.display = "none";//交货方式      隐藏
        document.getElementById("divTakeType").style.display = "none";//交货方式标签
        
        document.getElementById("divProfessional").style.display = "block";//所学专业标签       
        document.getElementById("ddlProfessional").style.display = "block";//所学专业
        document.getElementById("tb_GH").style.display = "block";//客户关怀
        document.getElementById("trRelaGrade").style.display = "block";//关系等级行trRelaGrade 
        document.getElementById("trUsedStatus").style.display = "block";//启用状态行trUsedStatus 
        
        document.getElementById("trSellArea").style.display = "none";//经营范围
        document.getElementById("divCurrencyType").style.display = "none";//结算币种标签
        
        document.getElementById("divddlCurrencyType").style.display = "none";//结算币种
        
        document.getElementById("divBillType").style.display = "none";//发票类型标签
        document.getElementById("divseleBillType").style.display = "none";//发票类型
        document.getElementById("trMoneyType").style.display = "none";//支付方式行
        document.getElementById("trAccountNum").style.display = "none";//账号行
        document.getElementById("tb_FZ").style.display = "none";//辅助信息
        document.getElementById("tb_JY").style.display = "none";//经营信息
         document.getElementById("tb_HZ").style.display = "none";//合作策略
    }
    else
    {
        document.getElementById("txtCustNum").style.display = "none";
        document.getElementById("divCustNum").style.display = "none";
         
        document.getElementById("divSex").style.display = "none";//性别标签
        document.getElementById("divseleSex").style.display = "none";//性别
        
        document.getElementById("divLinkType").style.display = "none";//联系人类型标签
        document.getElementById("divddlLinkType").style.display = "none";//联系人类型
        document.getElementById("txtProvince").style.display = "block";//省      显示
        document.getElementById("divProvince").style.display = "block";//省标签
        
        document.getElementById("divPaperNum").style.display = "none";//身份证标签
        document.getElementById("txtPaperNum").style.display = "none";//身份证
        document.getElementById("txtCity").style.display = "block";//市      显示
        document.getElementById("divCity").style.display = "block";//市标签
        
        document.getElementById("divBirthday").style.display = "none";//生日标签
        document.getElementById("txtBirthday").style.display = "none";//生日
         document.getElementById("txtContactName").style.display = "block";//联系人      显示
        document.getElementById("divContactName").style.display = "block";//联系人标签
        
        document.getElementById("divPosition").style.display = "none";//职务标签
        document.getElementById("txtPosition").style.display = "none";//职务
         document.getElementById("txtOnLine").style.display = "block";//在线咨询      显示
        document.getElementById("divOnLine").style.display = "block";//在线咨询标签
        
        document.getElementById("divAge").style.display = "none";//年龄标签
        document.getElementById("txtAge").style.display = "none";//年龄
        document.getElementById("txtWebSite").style.display = "block";//公司网址      显示
        document.getElementById("divWebSite").style.display = "block";//公司网址标签
        
        document.getElementById("divHomeTown").style.display = "none";//籍贯标签
        document.getElementById("txtHomeTown").style.display = "none";//籍贯
          document.getElementById("txtFirstBuyDate").style.display = "block";//首次交易日期      显示
        document.getElementById("divFirstBuyDate").style.display = "block";//首次交易日期标签
        
        document.getElementById("divNational").style.display = "none";//民族标签        
        document.getElementById("ddlNational").style.display = "none";//民族
         document.getElementById("divddlCarryType").style.display = "block";//运送方式      显示
        document.getElementById("divCarryType").style.display = "block";//运送方式标签
        
        document.getElementById("divCulture").style.display = "none";//所受教育标签       
        document.getElementById("ddlCulture").style.display = "none";//所受教育
        document.getElementById("divddlTakeType").style.display = "block";//交货方式      显示
        document.getElementById("divTakeType").style.display = "block";//交货方式标签
        
        document.getElementById("divProfessional").style.display = "none";//所学专业标签       
        document.getElementById("ddlProfessional").style.display = "none";//所学专业
        document.getElementById("tb_GH").style.display = "none";//客户关怀
        document.getElementById("trRelaGrade").style.display = "none";//关系等级行trRelaGrade
        document.getElementById("trUsedStatus").style.display = "none";//启用状态行trUsedStatus 
        
        document.getElementById("trSellArea").style.display = "block";//经营范围        
        document.getElementById("divCurrencyType").style.display = "block";//结算币种标签
        document.getElementById("divddlCurrencyType").style.display = "block";//结算币种
         document.getElementById("divBillType").style.display = "block";//发票类型标签
        document.getElementById("divseleBillType").style.display = "block";//发票类型
        document.getElementById("trMoneyType").style.display = "block";//支付方式行
        document.getElementById("trAccountNum").style.display = "block";//账号行
        document.getElementById("tb_FZ").style.display = "block";//辅助信息
        document.getElementById("tb_JY").style.display = "block";//经营信息
        document.getElementById("tb_HZ").style.display = "block";//合作策略
    }
}

function SaveCustData()
{
    if(!CheckInput())
    {
        return;
    }    
        
    var CustType=$("#ddlCustType").val(); //客户类别
    //var CustClass=$("#CustClassDrpControl1_ddlCustClass").val(); //客户分类   
    var CustClass=$("#CustClassDrpControl1_CustClassHidden").val(); //客户分类    
    
    var CustNoType = document.getElementById("ddlCustNo_ddlCodeRule").value;
    var CustNo = "";
    //获取编码规则下拉列表选中项  如果选中的是 手工输入时，校验编号是否输入
    if (CustNoType == "")
    {
        CustNo = $("#ddlCustNo_txtCode").val(); //客户编号
    }
    if(isnew == "2")
    {
        CustNo = document.getElementById("divCustNo").innerText;
    }
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
    //var HotType = $("#seleHotType").val();
    var MeritGrade = $("#seleMeritGrade").val();
    var RelaGrade = $("#seleRelaGrade").val();
    var Relation = $("#txtRelation").val();
    var CompanyType = $("#txtCompanyType").val();
    var StaffCount = $("#txtStaffCount").val();
    var Source = $("#txtSource").val();
    var Phase = $("#txtPhase").val();//阶段
    var CustSupe = $("#txtUcCustName").val();//上级客户
 
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
    var OpenBank = $("#txtOpenBank").val();//开户行
    var AccountMan = $("#txtAccountMan").val();//户名
    var AccountNum = $("#txtAccountNum").val();//账号
    var UsedStatus = $("#seleUsedStatus").val();
    var CreatedDate = $("#txtCreatedDate").val();
    var Remark = $("#txtRemark").val();
    
    var CustTypeManage = $("#SeleCustTypeManage").val();//客户管理分类
    var CustTypeSell = $("#SeleCustTypeSell").val();//客户营销分类
    var CustTypeTime = $("#SeleCustTypeTime").val();//客户时间分类
    var FirstBuyDate = $("#txtFirstBuyDate").val();//首次交易日
    var CanUserName = document.getElementById("txtCanUserName").value;//可查看该客户档案的人员
    var CanUserID = document.getElementById("txtCanUserID").value;//可查看该客户档案的人员姓名
   
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

    var CustBig = document.getElementById("selCustBig").value;//客户大类
    var CustNum = document.getElementById("txtCustNum").value;//卡号
    var strParams = GetLinkManInfo();    
    
    //循环联系人信息给数组后传递
   // var LinkMan = new Array();
    
     $.ajax({
        type: "POST",
        url: "../../../Handler/Office/CustManager/CustAdd.ashx",
        data:'CustType='+reescape(CustType)+'&CustClass='+reescape(CustClass)+'&CustNoType='+reescape(CustNoType)+'&CustNo='+reescape(CustNo)+'&CustName='+reescape(CustName)+'&CustNam='+reescape(CustNam)+'&CustShort='+reescape(CustShort)+
            strParams+
            '&CreditGrade='+reescape(CreditGrade)+'&Seller='+reescape(Seller)+'&AreaID='+reescape(AreaID)+'&CustNote='+reescape(CustNote)+'&LinkCycle='+reescape(LinkCycle)+'&HotIs='+reescape(HotIs)+
            '&HotHow='+reescape(HotHow)+'&MeritGrade='+reescape(MeritGrade)+'&RelaGrade='+reescape(RelaGrade)+'&Relation='+reescape(Relation)+'&CompanyType='+reescape(CompanyType)+
            '&StaffCount='+reescape(StaffCount)+'&Source='+reescape(Source)+'&Phase='+reescape(Phase)+'&CustSupe='+reescape(CustSupe)+'&Trade='+reescape(Trade)+'&SetupDate='+reescape(SetupDate)+
            '&ArtiPerson='+reescape(ArtiPerson)+'&SetupMoney='+reescape(SetupMoney)+'&SetupAddress='+reescape(SetupAddress)+'&CapitalScale='+reescape(CapitalScale)+'&SaleroomY='+reescape(SaleroomY)+
            '&ProfitY='+reescape(ProfitY)+'&TaxCD='+reescape(TaxCD)+'&BusiNumber='+reescape(BusiNumber)+'&IsTax='+reescape(IsTax)+'&SellMode='+reescape(SellMode)+'&SellArea='+reescape(SellArea)+'&CountryID='+reescape(CountryID)+
            '&Province='+reescape(Province)+'&City='+reescape(City)+'&Tel='+reescape(Tel)+'&ContactName='+reescape(ContactName)+'&Mobile='+reescape(Mobile)+'&ReceiveAddress='+reescape(ReceiveAddress)+'&WebSite='+reescape(WebSite)+'&Post='+reescape(Post)+
            '&email='+reescape(email)+'&Fax='+reescape(Fax)+'&OnLine='+reescape(OnLine)+'&TakeType='+reescape(TakeType)+'&CarryType='+reescape(CarryType)+'&BusiType='+reescape(BusiType)+'&BillType='+reescape(BillType)+'&PayType='+reescape(PayType)+
            '&MoneyType='+reescape(MoneyType)+'&CurrencyType='+reescape(CurrencyType)+'&CreditManage='+reescape(CreditManage)+'&MaxCredit='+reescape(MaxCredit)+'&MaxCreditDate='+reescape(MaxCreditDate)+'&UsedStatus='+reescape(UsedStatus)+
            '&CreatedDate='+reescape(CreatedDate)+'&Remark='+reescape(Remark)+'&OpenBank='+reescape(OpenBank)+'&AccountMan='+reescape(AccountMan)+'&AccountNum='+reescape(AccountNum)+//'&LinkManList='+reescape(LinkManList)+
            '&CustTypeManage='+reescape(CustTypeManage)+'&CustTypeSell='+reescape(CustTypeSell)+'&CustTypeTime='+reescape(CustTypeTime)+'&FirstBuyDate='+reescape(FirstBuyDate)+'&action='+reescape(isnew)+
            '&CanUserName='+escape(CanUserName)+'&CanUserID='+escape(CanUserID)+'&CatchWord='+escape(CatchWord)+'&ManageValues='+escape(ManageValues)+ '&Potential='+escape(Potential)+'&Problem='+escape(Problem)+ '&Advantages='+escape(Advantages)+'&TradePosition='+escape(TradePosition)+ '&Competition='+escape(Competition)+'&Collaborator='+escape(Collaborator)+
            '&ManagePlan='+escape(ManagePlan)+'&Collaborate='+escape(Collaborate)+'&CompanyValues='+escape(CompanyValues)+fnGetExtAttrValue()+
            '&CustBig='+escape(CustBig)+'&CustNum='+escape(CustNum),
        dataType:'json',//返回json格式数据
        cache:false,
        beforeSend:function()
          { 
              AddPop();
          },
          //complete :function(){hidePopup();},
          error: function() 
          {
              showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
          },
          success:function(data) 
            { 
                switch(data.sta)
                {
                    
                     case 1:
                     hidePopup();
                     isnew="2";
                     //保存成功后 不能修改客户编号
                     //编码规则DIV不可见
                     document.getElementById("divCodeRule").style.display = "none";
                     document.getElementById("divCustNo").style.display = "block";
                     document.getElementById("divCustNo").innerHTML = data.data;
                     document.getElementById("selCustBig").disabled = true; //客户大类不可改
                     showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
                     break;
                     
                     case 2:
                     case 3:
                     showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",data.data);
                     break;                     
                     
                     default:
                     hidePopup();
                     showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
                     break;
                }
            }
     });
}



//新建时
function ClearInput()
{
    window.location.reload();
}

//若是中文则自动填充拼音缩写
function LoadPYShort()
{
    var txtCustNam = $("#txtCustName").val().Trim();
    //debugger;
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

/*
* 获取会员客户时个人信息
*/
function GetLinkManInfo()
{
     var strParams = "";
     
     strParams += "&Sex=" + escape(document.getElementById("seleSex").value);//性别
     strParams += "&LinkType=" + escape(document.getElementById("ddlLinkType").value);//联系人类型
     strParams += "&PaperNum=" + escape(document.getElementById("txtPaperNum").value);//身份证号
     strParams += "&Birthday=" + escape(document.getElementById("txtBirthday").value);//生日
     strParams += "&Position=" + escape(document.getElementById("txtPosition").value);//职务
     strParams += "&Age=" + escape(document.getElementById("txtAge").value);//年龄
     strParams += "&HomeTown=" + escape(document.getElementById("txtHomeTown").value);//籍贯
     strParams += "&NationalID=" + escape(document.getElementById("CodeTypeDrpControl1_ddlCodeType").value);//民族
     strParams += "&CultureLevel=" + escape(document.getElementById("CodeTypeDrpControl2_ddlCodeType").value);//所受教育
     strParams += "&Professional=" + escape(document.getElementById("CodeTypeDrpControl3_ddlCodeType").value);//所学专业
     strParams += "&RelaGrade0=" + escape(document.getElementById("seleRelaGrade0").value);//关系等级
     strParams += "&UsedStatus0=" + escape(document.getElementById("seleUsedStatus0").value);//启用状态     
     strParams += "&IncomeYear=" + escape(document.getElementById("txtIncomeYear").value);//年收入情况
     strParams += "&FuoodDrink=" + escape(document.getElementById("txtFuoodDrink").value);//饮食偏好
     strParams += "&LoveMusic=" + escape(document.getElementById("txtLoveMusic").value);//喜欢的音乐
     strParams += "&LoveColor=" + escape(document.getElementById("txtLoveColor").value);//喜欢的颜色
     strParams += "&LoveSmoke=" + escape(document.getElementById("txtLoveSmoke").value);//喜欢的香烟
     strParams += "&LoveDrink=" + escape(document.getElementById("txtLoveDrink").value);//爱喝的酒
     strParams += "&LoveTea=" + escape(document.getElementById("txtLoveTea").value);//爱喝的茶
     strParams += "&LoveBook=" + escape(document.getElementById("txtLoveBook").value);//喜欢的书籍
     strParams += "&LoveSport=" + escape(document.getElementById("txtLoveSport").value);//喜欢的运动
     strParams += "&LoveClothes=" + escape(document.getElementById("txtLoveClothes").value);//喜欢的品牌服饰
     strParams += "&Cosmetic=" + escape(document.getElementById("txtCosmetic").value);//喜欢的牌化妆品
     strParams += "&Car=" + escape(document.getElementById("txtCar").value);//开什么车
     strParams += "&Nature=" + escape(document.getElementById("txtNature").value);//性格描述
     strParams += "&AboutFamily=" + escape(document.getElementById("txtAboutFamily").value);//家人情况
     strParams += "&Appearance=" + escape(document.getElementById("txtAppearance").value);//外表描述
     strParams += "&AdoutBody=" + escape(document.getElementById("txtAdoutBody").value);//健康状况
     
     return strParams;
}

//允许延期付款选“是”时
function DivMaxcShow()
{
    if(document.getElementById("seleCreditManage").value == "2")
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

function PagePrint()
{
     var Url = document.getElementById("divCustNo").innerHTML;
     if(Url == "")
     {
      popMsgObj.Show("打印|","请保存单据再打印|");
        return;
     }
    //window.open("../../PrinttingModel/CustManager/CustAddPrint.aspx?id=" + Url);
    
    var custBig = document.getElementById("selCustBig").value;
     //var keyList=fnGetExtAttrValue();
     if(custBig == "2")
     {
        window.open("../../PrinttingModel/CustManager/CustLinkPrint.aspx?id=" + Url+"&CustBig="+custBig+"&CustNo="+Url);
     }
     else
     {
        window.open("../../PrinttingModel/CustManager/CustAddPrint.aspx?id=" + Url+"&CustBig="+custBig+"&CustNo="+Url);
     }
    
     
}

//主表单验证
function CheckInput()
{    
    var fieldText = "";
    var msgText = "";
    var isFlag = true;       
    //var CustClassName = document.getElementById('CustClass').value;//客户分类名
    var ddlCustNoType = document.getElementById("ddlCustNo_ddlCodeRule").value;//客户编号类型
    var txtCustNo = document.getElementById("ddlCustNo_txtCode").value;// 客户编号
    var divCustNo = document.getElementById("divCustNo").innerHTML;//编辑时客户编号
    var txtStaffCount = document.getElementById("txtStaffCount").value;// 人员规模   
    var txtCustName = document.getElementById("txtCustName").value; //客户名称    
    var LinkerID = document.getElementById("txtJoinUser").value; //分管业务员ID
    var JoinUser = document.getElementById("UserLinker").value; //分管业务员名
    
    
    var txtCreator = document.getElementById("txtCreator").value; //建单人
    var txtCreatedDate = document.getElementById("txtCreatedDate").value; //建单日期
    var txtSetupMoney = document.getElementById("txtSetupMoney").value; //注册资本
    var txtCapitalScale = document.getElementById("txtCapitalScale").value; //资产规模
    var txtSaleroomY = document.getElementById("txtSaleroomY").value; //年销售额
    var txtProfitY =  document.getElementById("txtProfitY").value; //年利润额
    var txtMaxCredit = document.getElementById("txtMaxCredit").value; //信用额度
    var txtCustNote = document.getElementById("txtCustNote").value;//客户简介
    var txtSetupDate = document.getElementById("txtSetupDate").value;//成立日期
    var txtRelation = document.getElementById("txtRelation").value; //关系描述
    var txtSellArea = document.getElementById("txtSellArea").value; //经营范围  
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
                
    var txtAge = document.getElementById("txtAge").value;//客户联系人年龄
    var txtBirthday = document.getElementById("txtBirthday").value;//客户联系人出生日期
    var CustBig = document.getElementById("selCustBig").value;
    if(txtAge.length > 0 && txtBirthday.length > 0 && CustBig == "2")
    {
        if(txtAge.length < 3 && IsNumber(txtAge) != null)
        {
            if(StringToDate(txtBirthday) < StringToDate(GetNow()))
            {
                var BeginYear = txtBirthday.slice(0,4);
                var EndYear = GetNow().slice(0,4);
                var DiffAge = (EndYear - BeginYear + 1).toString();
                if(DiffAge != txtAge)
                {                
                    isFlag = false;
                    fieldText = fieldText + "生日与年龄|";
   		            msgText = msgText +  "生日与年龄不符合实际情况|";
                }                
            }
        }
    }

	var txtCustType = document.getElementById('ddlCustType').value;//客户类别   
    if(txtCustType =="")
    {
        isFlag = false;
        fieldText = fieldText +  "客户类别|";
   		msgText = msgText +  "请首先配置客户类别|";
    }
    
    var Handset = document.getElementById("txtMobile").value;//手机号
    if(Handset.length > 0)
    {
         if(IsNumber(Handset) == null || Handset.length != 11)
        {
            isFlag = false;
            fieldText = fieldText + "手机号|";
   		    msgText = msgText +  "请输入正确的手机号|";
        }
    }
    
    var Email = document.getElementById("txtemail").value;//Email
    if(Email != "" && !IsEmail(Email))
    {
         isFlag = false;
         fieldText = fieldText + "电子邮件|";
   		 msgText = msgText +  "请输入正确的电子邮件格式|";
    }
    
    var ddlArea = document.getElementById('ddlArea').value;//区域   
    if(ddlArea =="")
    {
        isFlag = false;
        fieldText = fieldText +  "区域|";
   		msgText = msgText +  "请首先配置区域|";
    }
    
     var ddlCreditGrade = document.getElementById('ddlCreditGrade').value;//客户优质级别   
    if(ddlCreditGrade =="")
    {
        isFlag = false;
        fieldText = fieldText +  "客户优质级别|";
   		msgText = msgText +  "请首先配置客户优质级别|";
    }
    
     var ddlLinkCycle = document.getElementById('ddlLinkCycle').value;//客户联络期限   
    if(ddlLinkCycle =="")
    {
        isFlag = false;
        fieldText = fieldText +  "客户联络期限|";
   		msgText = msgText +  "请首先配置客户联络期限|";
    }
    
    var ddlCountry = document.getElementById('ddlCountry').value;//国家地区   
    if(ddlCountry =="")
    {
        isFlag = false;
        fieldText = fieldText +  "国家地区|";
   		msgText = msgText +  "请首先配置国家地区|";
    }
    
    var ddlTakeType = document.getElementById('ddlTakeType').value;//交货方式   
    if(ddlTakeType =="")
    {
        isFlag = false;
        fieldText = fieldText +  "交货方式|";
   		msgText = msgText +  "请首先配置交货方式|";
    }
    
    var ddlCarryType = document.getElementById('ddlCarryType').value;//运货方式   
    if(ddlCarryType =="")
    {
        isFlag = false;
        fieldText = fieldText +  "运货方式|";
   		msgText = msgText +  "请首先配置运货方式|";
    }
    
     var ddlPayType = document.getElementById('ddlPayType').value;//结算方式   
    if(ddlPayType =="")
    {
        isFlag = false;
        fieldText = fieldText +  "结算方式|";
   		msgText = msgText +  "请首先配置结算方式|";
    }
    
     var ddlMoneyType = document.getElementById('ddlMoneyType').value;//支付方式   
    if(ddlMoneyType =="")
    {
        isFlag = false;
        fieldText = fieldText +  "支付方式|";
   		msgText = msgText +  "请首先配置支付方式|";
    }
    
    var ddlCurrencyType = document.getElementById('ddlCurrencyType').value;//币种   
    if(ddlCurrencyType =="")
    {
        isFlag = false;
        fieldText = fieldText +  "币种|";
   		msgText = msgText +  "请首先配置币种|";
    }
    
    
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
	 if(txtCustShort != "" && txtCustShort.match(/^[A-Za-z0-9]+$/) == null)
	 {
	    isFlag = false;
	    fieldText = fieldText +  "拼音缩写|";
   		msgText = msgText +  "拼音缩写必须为英文字母|";
	 }
	  
    if (ddlCustNoType == "" && txtCustNo == "" && divCustNo == "")//如果选中的是手工输入时，编号必须输入
    {
        isFlag = false;
        fieldText = fieldText +  "客户编号|";
   		msgText = msgText +  "请输入客户编号|";
    }
    if(txtCustNo != "" && ddlCustNoType == "" && divCustNo != "")
    {
        if(!CodeCheck(txtCustNo))
        {
            isFlag = false;
           fieldText = fieldText + "客户编号|";
   		    msgText = msgText +  "编号不能含有特殊字符|";
        }
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
    if(txtStaffCount.length>0)
    {
        if(!PositiveInteger(txtStaffCount))
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
    if(LinkerID == "" || JoinUser == "")
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
    if(txtSetupMoney.length>0)
    {
        if(!IsNumeric(txtSetupMoney,12,2))
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
    if(txtCapitalScale.length>0)
    {
        if(!IsNumeric(txtCapitalScale,12,2))
        {
            isFlag = false;
            fieldText = fieldText +  "资产规模|";
   		    msgText = msgText + "资产规模精度输入的格式不正确|";  
        }
        if(parseInt(txtCapitalScale).toString().length > 10)
        {
            isFlag = false;
            fieldText = fieldText +  "资产规模|";
   		    msgText = msgText + "资产规模整数部分过长|";
        }
    }
    if(txtSaleroomY.length>0)
    {
        if(!IsNumeric(txtSaleroomY,12,2))
        {
            isFlag = false;
            fieldText = fieldText +  "年销售额|";
   		    msgText = msgText + "年销售额精度输入的格式不正确|";  
        }
        if(parseInt(txtSaleroomY).toString().length > 10)
        {
            isFlag = false;
            fieldText = fieldText +  "年销售额|";
   		    msgText = msgText + "年销售额整数部分过长|";
        }
    }
    if(txtProfitY.length>0)
    {
        if(!IsNumeric(txtProfitY,12,2))
        {
            isFlag = false;
            fieldText = fieldText +  "年利润额|";
   		    msgText = msgText + "年利润额精度输入的格式不正确|";  
        }
        if(parseInt(txtProfitY).toString().length > 10)
        {
            isFlag = false;
            fieldText = fieldText + "年利润额|";
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
            fieldText = fieldText + "信用额度|";
   		    msgText = msgText + "信用额度整数部分过长|";
        }
    }
    if(strlen(txtCustNote) > 1024)
    {
        isFlag = false;
        fieldText = fieldText + "客户简介|";
   		msgText = msgText + "客户简介最多只允许输入1024个字符|";
    }
    if(txtSetupDate.length>0)
    {
        if(!isDate(txtSetupDate))
        {
            isFlag = false;
             fieldText = fieldText + "成立日期|";
   		     msgText = msgText + "成立日期格式不正确|";    
        }
    }
    if(txtCreatedDate.length>0)
    {
        if(!isDate(txtCreatedDate))
        {
            isFlag = false;
             fieldText = fieldText + "建单日期|";
   		     msgText = msgText + "建单日期格式不正确|";    
        }
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
                        Coutrol = "<input id='" + ControlID + "' specialworkcheck='"+item.EFDesc+"' class='tdinput' type='text' style='width: 99%; height: 20px' maxlength='128' />";
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
                            "<td  height='20' colspan='5' class='tdColInput' width='90%'>" + Coutrol + "</td>";
                        $("<tr></tr>").append("" + Cell + "").appendTo($("#ExtTab tbody"));
                        Cell = "";
                     
                
                });
        
            }
            $("#hiddKey").val(strKey);
            //fnSetExtAttrValue();
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
//--------------------扩展属性操作----------------------------------------------------------------------------//

 //////获取客户信息(修改、查看)
 function GetCustInfo(requestparam1,custbig,custno)
{
        isnew = "2";
       var retval = requestparam1;
       $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:"../../../Handler/Office/CustManager/CustEdit.ashx",//目标地址
       data:'id='+reescape(retval)+'&custbig='+reescape(custbig)+'&custno='+reescape(custno),
       cache:false,
       beforeSend:function(){AddPop();},//发送数据之前
       success: function(msg){
                //数据获取完毕，填充页面据显示
                $.each(msg.data,function(i,item){
                    if(item.ID != null && item.ID != "")
                        $("#ddlCustType").val(item.CustType);
                                                
                        $("#CustClassDrpControl1_CustClassHidden").val(item.CustClass);//客户分类 
                        $("#CustClassDrpControl1_CustClass").val(item.CustClassName);//客户分类名                       
                        
                        //$("#txtCustNo").val(item.CustNo);//客户编号
                        document.getElementById("divCodeRule").style.display = "none";
                        document.getElementById("divCustNo").style.display = "block";
                        document.getElementById("divCustNo").innerHTML = item.CustNo;
                        
                        $("#selCustBig").val(item.CustBig);//客户大类
                        
                        if(custbig == "2")
                        {
                            $("#txtCustNum").val(item.CustNum);//卡号
                            $("#seleSex").val(item.Sex);
                            $("#ddlLinkType").val(item.LinkType);
                            $("#txtPaperNum").val(item.PaperNum);
                            $("#txtBirthday").val(item.Birthday);
                            $("#txtPosition").val(item.Position);
                            $("#txtAge").val(item.Age);
                            $("#txtHomeTown").val(item.HomeTown);                           
                            $("#CodeTypeDrpControl1_ddlCodeType").val(item.NationalID);
                            $("#CodeTypeDrpControl2_ddlCodeType").val(item.CultureLevel);
                            $("#CodeTypeDrpControl3_ddlCodeType").val(item.Professional);
                            $("#seleRelaGrade0").val(item.RelaGrade);
                            $("#txtModifiedUser0").val(item.ModifiedUser);
                            $("#txtModifiedDate0").val(item.ModifiedDate);
                            
                            $("#txtIncomeYear").val(item.IncomeYear);
                            $("#txtFuoodDrink").val(item.FuoodDrink);
                            $("#txtLoveMusic").val(item.LoveMusic);
                            $("#txtLoveColor").val(item.LoveColor);
                            $("#txtLoveSmoke").val(item.LoveSmoke);
                            $("#txtLoveDrink").val(item.LoveDrink);
                            $("#txtLoveTea").val(item.LoveTea);
                            $("#txtLoveBook").val(item.LoveBook);
                            $("#txtLoveSport").val(item.LoveSport);
                            $("#txtLoveClothes").val(item.LoveClothes);
                            $("#txtCosmetic").val(item.Cosmetic);
                            $("#txtCar").val(item.Car);
                            $("#txtNature").val(item.Nature);
                            $("#txtAboutFamily").val(item.AboutFamily);
                            $("#txtAppearance").val(item.Appearance);
                            $("#txtAdoutBody").val(item.AdoutBody);                            
                        }
                        
                        CustSwitch();                        

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
                        if(custbig == "2")
                        {
                            $("#txtTel").val(item.WorkTel);
                            
                        }
                        else
                        {
                            $("#txtTel").val(item.Tel);
                        }
                        
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
               });
               fnSetExtAttrValue();
              },
       error: function() 
       {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
       }, 
       complete:function(){hidePopup();}//接收数据完毕
       });
}

//修改页面初始化扩展属性值
function fnSetExtAttrValue() {
    var strKey = $.trim($("#hiddKey").val()); //扩展属性字段值

    var strCustNo = document.getElementById("divCustNo").innerHTML;//$.trim($("#txtCustNo").val());
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