var isnew="1";//1添加;2保存

var page = "";
var CustName = "";
var UcCustName = "";
var LinkManName = "";
var Handset = "";
var Important = "";
var LinkType;
var BeginDate;
var EndDate;
var currentPageIndex = "";
var pageCount = "";

$(document).ready(function()
{
      requestobj = GetRequest();
      requestparam1=requestobj['linkid'];

      page = requestobj['Pages'];
      CustName = requestobj['CustName'];
      UcCustName = requestobj['UcCustName'];
      LinkManName = requestobj['LinkManName'];
      Handset = requestobj['Handset'];
      Important = requestobj['Important'];
      LinkType = requestobj['LinkType'];
      BeginDate = requestobj['BeginDate'];
      EndDate = requestobj['EndDate'];
      currentPageIndex = requestobj['currentPageIndex'];
      pageCount = requestobj['pageCount'];
      
      if(typeof(requestparam1)!="undefined")
      {    
        //显示返回按钮
        $("#btn_back").show();
      }
});

//返回
function Back()
{ 
    window.location.href=page+'?CustName='+CustName+'&LinkManName='+LinkManName+'&Handset='+Handset+'&Important='+Important+'&UcCustName='+UcCustName+
    '&LinkType='+LinkType+'&BeginDate='+BeginDate+'&EndDate='+EndDate+'&currentPageIndex='+currentPageIndex+'&pageCount='+pageCount+'&ModuleID=2021202';
}

function PagePrint()
{
     var Url = document.getElementById("hfLinkID").value;
     if(Url == "")
     {
     popMsgObj.Show("打印|","请保存单据再打印|");
        return;
     }
    window.open("../../PrinttingModel/CustManager/LinkAddPrint.aspx?id=" + Url);
}

function AddLinkManData2()
{
    if(!CheckInput())
    {
        return;
    }
    var CustNo     = $("#hfCustNo").val();//客户编号
    var LinkManName= $("#txtLinkManName").val();
    var Sex        = $("#seleSex").val();
    var Important  = $("#seleImportant").val();
    var Company    = $("#txtCompany").val();
    var Appellation= $("#txtAppellation").val();
    var Department = $("#txtDepartment").val();
    var Position   = $("#txtPosition").val();
    var Operation  = $("#txtOperation").val();
    var WorkTel    = $("#txtWorkTel").val();
    var Fax        = $("#txtFax").val();
    var Handset    = $("#txtHandset").val();
    var MailAddress= $("#txtMailAddress").val();
    var HomeTel    = $("#txtHomeTel").val();
    var MSN        = $("#txtMSN").val();
    var QQ         = $("#txtQQ").val();
    var Post       = $("#txtPost").val();
    var HomeAddress= $("#txtHomeAddress").val();
    var Remark     = $("#txtRemark").val();
    var Age        = $("#txtAge").val();
    var Likes      = $("#txtLikes").val();
    var LinkType   = $("#ddlLinkType").val();
    var Birthday   = $("#txtBirthday").val();
    var PaperType  = $("#txtPaperType").val();
    var PaperNum   = $("#txtPaperNum").val();
    var ProfessionalDes = $("#Professional").val();
     
    var Photo      = document.getElementById("hfPagePhotoUrl").value;//$("#txtPhoto").val();
    var LinkID = document.getElementById("hfLinkID").value;//第一次保存后返回的联系人ID
    var CanUserName = document.getElementById("txtCanUserName").value;//可查看该客户档案的人员
    var CanUserID = document.getElementById("txtCanUserID").value;//可查看该客户档案的人员姓名
    
    var HomeTown=$("#HomeTown").val();
    var NationalID=$("#CodeTypeDrpControl1_ddlCodeType").val();
    var Birthcity=$("#Birthcity").val();
    var CultureLevel=$("#CodeTypeDrpControl2_ddlCodeType").val();
    var Professional=$("#CodeTypeDrpControl3_ddlCodeType").val();
    var GraduateSchool=$("#GraduateSchool").val();
    var IncomeYear=$("#IncomeYear").val();
    var FuoodDrink=$("#FuoodDrink").val();
    var LoveMusic=$("#LoveMusic").val();
    var LoveColor=$("#LoveColor").val();
    var LoveSmoke=$("#LoveSmoke").val();
    var LoveDrink=$("#LoveDrink").val();
    var LoveTea=$("#LoveTea").val();
    var LoveBook=$("#LoveBook").val();
    var LoveSport=$("#LoveSport").val();
    var LoveClothes=$("#LoveClothes").val();
    var Cosmetic=$("#Cosmetic").val();
    var Nature=$("#Nature").val();
    var Appearance=$("#Appearance").val();
    var AdoutBody=$("#AdoutBody").val();
    var AboutFamily=$("#AboutFamily").val();
    var Car=$("#Car").val();
    var LiveHouse=$("#LiveHouse").val();
    
    
    $.ajax({
        type:"POST",
        url: "../../../Handler/Office/CustManager/LinkManAdd.ashx",
        data:'CustNo='+reescape(CustNo)+    
            '&HomeTown='+escape(HomeTown)+
            '&NationalID='+escape(NationalID)+
            '&Birthcity='+escape(Birthcity)+
            '&CultureLevel='+escape(CultureLevel)+
            '&Professional='+escape(Professional)+
            '&GraduateSchool='+escape(GraduateSchool)+
            '&IncomeYear='+escape(IncomeYear)+
            '&FuoodDrink='+escape(FuoodDrink)+
            '&LoveMusic='+escape(LoveMusic)+
            '&LoveColor='+escape(LoveColor)+
            '&LoveSmoke='+escape(LoveSmoke)+
            '&LoveDrink='+escape(LoveDrink)+
            '&LoveTea='+escape(LoveTea)+
            '&LoveBook='+escape(LoveBook)+
            '&LoveSport='+escape(LoveSport)+
            '&LoveClothes='+escape(LoveClothes)+
            '&Cosmetic='+escape(Cosmetic)+
            '&Nature='+escape(Nature)+
            '&Appearance='+escape(Appearance)+
            '&AdoutBody='+escape(AdoutBody)+
            '&AboutFamily='+escape(AboutFamily)+
            '&Car='+escape(Car)+
            '&LiveHouse='+escape(LiveHouse)+
             '&LinkManName='+reescape(LinkManName)+
             '&Sex='+reescape(Sex)+
             '&Important='+reescape(Important)  +
             '&Company='+reescape(Company)    +
             '&Appellation='+reescape(Appellation)+
             '&Department='+reescape(Department) +
             '&Position='+reescape(Position)   +
             '&Operation='+reescape(Operation)  +
             '&WorkTel='+reescape(WorkTel)    +
             '&Fax='+reescape(Fax)        +
             '&Handset='+reescape(Handset)    +
             '&MailAddress='+reescape(MailAddress)+
             '&HomeTel='+reescape(HomeTel)    +
             '&MSN='+reescape(MSN)        +
             '&QQ='+reescape(QQ)         +
             '&Post='+reescape(Post)       +
             '&HomeAddress='+reescape(HomeAddress)+
             '&Remark='+reescape(Remark)     +
             '&Age='+reescape(Age)        +
             '&Likes='+reescape(Likes)      +
             '&LinkType='+reescape(LinkType)   +
             '&Birthday='+reescape(Birthday)   +
             '&PaperType='+reescape(PaperType)  +
             '&PaperNum='+reescape(PaperNum)   +
             '&Photo='+reescape(Photo)+
             '&LinkID='+reescape(LinkID)+
             '&CanUserName='+reescape(CanUserName)+
             '&CanUserID='+reescape(CanUserID)+'&ProfessionalDes='+reescape(ProfessionalDes)+
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
                     isnew="2";  
                     document.getElementById("hfLinkID").value = data.data;
                     document.getElementById("txtUcCustName").onclick = function(){ return false;}
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

function GetSomeLinkInfoByID()
{
    var LinkManName = document.getElementById("txtLinkManName").value;
    var CustNo = document.getElementById("hfCustNo").value;
      
    if(LinkManName == "" || CustNo == "")
    {
        return;
    }
    
    $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:"../../../Handler/Office/CustManager/CustLink.ashx",//目标地址
       data:'LinkName='+reescape(LinkManName)+'&CustNo='+reescape(CustNo),
       cache:false,
       beforeSend:function(){},//发送数据之前           
       success: function(msg){
                //数据获取完毕，填充页面据显示
                $.each(msg.data,function(i,item){
                    if(item.id != null && item.id != "")                       
                        $("#txtLinkManName").attr("value",item.ContactName);//联系人名
                        $("#txtWorkTel").attr("value",item.Tel);//电话
                        //document.getElementById("txtCustNam").value = item.CustNam
                        $("#txtHandset").attr("value",item.Mobile);//手机
                        $("#txtFax").attr("value",item.Fax);//传真
                        $("#txtPost").attr("value",item.Post);//邮编
                        $("#txtMailAddress").attr("value",item.email);                    
               });
              },
       error: function() 
       {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
       }, 
       complete:function(){}//接收数据完毕
       });
}

//主表单验证
function CheckInput()
{
var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var txtCustNam = document.getElementById('txtUcCustName').value;//客户简称
    var hfCustNo = document.getElementById('hfCustNo').value;//客户编码
    var txtLinkManName = document.getElementById("txtLinkManName").value;//客户联系人姓名
    var txtAge = document.getElementById("txtAge").value;//客户联系人年龄
    var txtBirthday = document.getElementById("txtBirthday").value;//客户联系人出生日期
    
    var Remark = document.getElementById("txtRemark").value;//
    var Likes = document.getElementById("txtLikes").value;//
    var Handset = document.getElementById("txtHandset").value;//手机号
    var Operation = document.getElementById("txtOperation").value;//负责业务    
    var RetVal=CheckSpecialWords();    
        
    var ddlLinkType = document.getElementById('ddlLinkType').value;//联系人类型 
    var TypeNum = document.getElementById("ddlLinkType").options.length;
    
    var Email = document.getElementById("txtMailAddress").value;// Email
    var MSN = document.getElementById("txtMSN").value;// MSN
    var QQ = document.getElementById("txtQQ").value;// QQ
        var HomeTel = document.getElementById("txtHomeTel").value;// 家庭电话
           var Fax = document.getElementById("txtFax").value;// 传真 
           
            if(HomeTel != "")
    {
        if(!CheckSpecialWord(HomeTel))
        {
            isFlag = false;
           fieldText = fieldText + "家庭电话|";
   		    msgText = msgText +  "家庭电话不能含有特殊字符|";
        }
    }
     if(strlen(HomeTel) > 200)
    {
        isFlag = false;
        fieldText = fieldText + "家庭电话|";
   		msgText = msgText + "家庭电话最多只允许输入200个字符|";
    }
    
     if(Fax != "")
    {
        if(!CheckSpecialWord(Fax))
        {
            isFlag = false;
           fieldText = fieldText + "传真|";
   		    msgText = msgText +  "传真不能含有特殊字符|";
        }
    }
     if(strlen(Fax) > 200)
    {
        isFlag = false;
        fieldText = fieldText + "传真|";
   		msgText = msgText + "传真最多只允许输入200个字符|";
    }
    if(Email != "" && !IsEmail(Email))
    {
         isFlag = false;
         fieldText = fieldText + "Email|";
   		 msgText = msgText +  "请输入正确的Email格式|";
    }
     if(MSN != "" && !IsEmail(MSN))
    {
         isFlag = false;
         fieldText = fieldText + "MSN|";
   		 msgText = msgText +  "请输入正确的MSN格式|";
    }
    
    if(QQ != "" && !IsNumber(QQ))
    {
         isFlag = false;
         fieldText = fieldText + "QQ|";
   		 msgText = msgText +  "请输入正确的QQ格式|";
    }

    var HomeTown=$("#HomeTown").val();
    var Birthcity=$("#Birthcity").val();
    var GraduateSchool=$("#GraduateSchool").val();
    var IncomeYear=$("#IncomeYear").val();
    var FuoodDrink=$("#FuoodDrink").val();
    var LoveMusic=$("#LoveMusic").val();
    var LoveColor=$("#LoveColor").val();
    var LoveSmoke=$("#LoveSmoke").val();
    var LoveDrink=$("#LoveDrink").val();
    var LoveTea=$("#LoveTea").val();
    var LoveBook=$("#LoveBook").val();
    var LoveSport=$("#LoveSport").val();
    var LoveClothes=$("#LoveClothes").val();
    var Cosmetic=$("#Cosmetic").val();
    var Nature=$("#Nature").val();
    var Appearance=$("#Appearance").val();
    var AdoutBody=$("#AdoutBody").val();
    var AboutFamily=$("#AboutFamily").val();
    var Car=$("#Car").val();
    var LiveHouse=$("#LiveHouse").val();
    var ProfessionalDes=$("#Professional").val();

    if(ProfessionalDes != "")
    {
        if(!CheckSpecialWord(ProfessionalDes))
        {
            isFlag = false;
           fieldText = fieldText + "专业描述|";
   		    msgText = msgText +  "专业描述不能含有特殊字符|";
        }
    }
    if(strlen(ProfessionalDes) > 100)
    {
        isFlag = false;
        fieldText = fieldText + "专业描述|";
   		msgText = msgText + "专业描述最多只允许输入50个字符|";
    }

    if(LiveHouse != "")
    {
        if(!CheckSpecialWord(LiveHouse))
        {
            isFlag = false;
           fieldText = fieldText + "住房情况|";
   		    msgText = msgText +  "住房情况不能含有特殊字符|";
        }
    }
     if(strlen(LiveHouse) > 50)
    {
        isFlag = false;
        fieldText = fieldText + "住房情况|";
   		msgText = msgText + "住房情况最多只允许输入50个字符|";
    }
  if(Car != "")
    {
        if(!CheckSpecialWord(Car))
        {
            isFlag = false;
           fieldText = fieldText + "开什么车|";
   		    msgText = msgText +  "开什么车不能含有特殊字符|";
        }
    }
                   if(strlen(AboutFamily) > 50)
    {
        isFlag = false;
        fieldText = fieldText + "开什么车|";
   		msgText = msgText + "开什么车最多只允许输入50个字符|";
    }
  if(AboutFamily != "")
    {
        if(!CheckSpecialWord(AboutFamily))
        {
            isFlag = false;
           fieldText = fieldText + "家人情况|";
   		    msgText = msgText +  "家人情况不能含有特殊字符|";
        }
    }
               if(strlen(AboutFamily) > 100)
    {
        isFlag = false;
        fieldText = fieldText + "家人情况|";
   		msgText = msgText + "家人情况最多只允许输入100个字符|";
    }
  if(AdoutBody != "")
    {
        if(!CheckSpecialWord(AdoutBody))
        {
            isFlag = false;
           fieldText = fieldText + "健康状况|";
   		    msgText = msgText +  "健康状况不能含有特殊字符|";
        }
    }
           if(strlen(AdoutBody) > 100)
    {
        isFlag = false;
        fieldText = fieldText + "健康状况|";
   		msgText = msgText + "健康状况最多只允许输入100个字符|";
    }
   if(Appearance != "")
    {
        if(!CheckSpecialWord(Appearance))
        {
            isFlag = false;
           fieldText = fieldText + "外表描述|";
   		    msgText = msgText +  "外表描述不能含有特殊字符|";
        }
    }
       if(strlen(Appearance) > 100)
    {
        isFlag = false;
        fieldText = fieldText + "外表描述|";
   		msgText = msgText + "外表描述最多只允许输入100个字符|";
    }
   if(Nature != "")
    {
        if(!CheckSpecialWord(Nature))
        {
            isFlag = false;
           fieldText = fieldText + "性格描述|";
   		    msgText = msgText +  "性格描述不能含有特殊字符|";
        }
    }
   if(strlen(Nature) > 100)
    {
        isFlag = false;
        fieldText = fieldText + "性格描述|";
   		msgText = msgText + "性格描述最多只允许输入100个字符|";
    }
    if(Cosmetic != "")
    {
        if(!CheckSpecialWord(Cosmetic))
        {
            isFlag = false;
           fieldText = fieldText + "喜欢哪些品牌的化妆品 |";
   		    msgText = msgText +  "喜欢哪些品牌的化妆品不能含有特殊字符|";
        }
    }
               if(strlen(Cosmetic) > 50)
    {
        isFlag = false;
        fieldText = fieldText + "喜欢哪些品牌的化妆品|";
   		msgText = msgText + "喜欢哪些品牌的化妆品最多只允许输入50个字符|";
    }
     if(LoveClothes != "")
    {
        if(!CheckSpecialWord(LoveClothes))
        {
            isFlag = false;
           fieldText = fieldText + "喜欢哪些品牌的服饰|";
   		    msgText = msgText +  "喜欢哪些品牌的服饰不能含有特殊字符|";
        }
    }
           if(strlen(LoveClothes) > 50)
    {
        isFlag = false;
        fieldText = fieldText + "喜欢哪些品牌的服饰|";
   		msgText = msgText + "喜欢哪些品牌的服饰最多只允许输入50个字符|";
    }
     if(LoveSport != "")
    {
        if(!CheckSpecialWord(LoveSport))
        {
            isFlag = false;
           fieldText = fieldText + "喜欢什么样的运动|";
   		    msgText = msgText +  "喜欢什么样的运动不能含有特殊字符|";
        }
    }
        if(strlen(LoveSport) > 50)
    {
        isFlag = false;
        fieldText = fieldText + "喜欢什么样的运动|";
   		msgText = msgText + "喜欢什么样的运动最多只允许输入50个字符|";
    }
      if(LoveBook != "")
    {
        if(!CheckSpecialWord(LoveBook))
        {
            isFlag = false;
           fieldText = fieldText + "喜欢哪类书籍 |";
   		    msgText = msgText +  "喜欢哪类书籍不能含有特殊字符|";
        }
    }
    if(strlen(LoveBook) > 50)
    {
        isFlag = false;
        fieldText = fieldText + "喜欢哪类书籍|";
   		msgText = msgText + "喜欢哪类书籍最多只允许输入50个字符|";
    }
       if(LoveTea != "")
    {
        if(!CheckSpecialWord(LoveTea))
        {
            isFlag = false;
           fieldText = fieldText + "爱喝什么茶|";
   		    msgText = msgText +  "爱喝什么茶不能含有特殊字符|";
        }
    }
             if(strlen(LoveTea) > 50)
    {
        isFlag = false;
        fieldText = fieldText + "爱喝什么茶|";
   		msgText = msgText + "爱喝什么茶最多只允许输入50个字符|";
    }
       if(LoveDrink != "")
    {
        if(!CheckSpecialWord(LoveDrink))
        {
            isFlag = false;
           fieldText = fieldText + "爱喝什么酒 |";
   		    msgText = msgText +  "爱喝什么酒不能含有特殊字符|";
        }
    }
         if(strlen(LoveDrink) > 50)
    {
        isFlag = false;
        fieldText = fieldText + "爱喝什么酒|";
   		msgText = msgText + "爱喝什么酒最多只允许输入50个字符|";
    }
       if(LoveSmoke != "")
    {
        if(!CheckSpecialWord(LoveSmoke))
        {
            isFlag = false;
           fieldText = fieldText + "喜欢什么品牌的香烟|";
   		    msgText = msgText +  "喜欢什么品牌的香烟不能含有特殊字符|";
        }
    }
        if(strlen(LoveSmoke) > 50)
    {
        isFlag = false;
        fieldText = fieldText + "喜欢什么品牌的香烟|";
   		msgText = msgText + "喜欢哪些颜色最多只允许输入50个字符|";
    }
        if(LoveColor != "")
    {
        if(!CheckSpecialWord(LoveColor))
        {
            isFlag = false;
           fieldText = fieldText + "喜欢哪些颜色 |";
   		    msgText = msgText +  "喜欢哪些颜色不能含有特殊字符|";
        }
    }
    if(strlen(LoveColor) > 50)
    {
        isFlag = false;
        fieldText = fieldText + "喜欢哪些颜色|";
   		msgText = msgText + "喜欢哪些颜色最多只允许输入50个字符|";
    }
        if(LoveMusic != "")
    {
        if(!CheckSpecialWord(LoveMusic))
        {
            isFlag = false;
           fieldText = fieldText + "喜欢什么样的音乐 |";
   		    msgText = msgText +  "喜欢什么样的音乐不能含有特殊字符|";
        }
    }
            if(strlen(LoveMusic) > 50)
    {
        isFlag = false;
        fieldText = fieldText + "喜欢什么样的音乐|";
   		msgText = msgText + "喜欢什么样的音乐最多只允许输入50个字符|";
    }
    if(FuoodDrink != "")
    {
        if(!CheckSpecialWord(FuoodDrink))
        {
            isFlag = false;
           fieldText = fieldText + "饮食偏好 |";
   		    msgText = msgText +  "饮食偏好不能含有特殊字符|";
        }
    }
        if(strlen(FuoodDrink) > 50)
    {
        isFlag = false;
        fieldText = fieldText + "饮食偏好|";
   		msgText = msgText + "饮食偏好最多只允许输入50个字符|";
    }
    if(HomeTown != "")
    {
        if(!CheckSpecialWord(HomeTown))
        {
            isFlag = false;
           fieldText = fieldText + "籍贯|";
   		    msgText = msgText +  "籍贯不能含有特殊字符|";
        }
    }
    if(strlen(HomeTown)> 100)
    {
        isFlag = false;
        fieldText = fieldText + "籍贯|";
   		msgText = msgText + "籍贯最多只允许输入100个字符|";
    }
    if(Birthcity != "")
    {
        if(!CheckSpecialWord(Birthcity))
        {
            isFlag = false;
           fieldText = fieldText + "出生地|";
   		    msgText = msgText +  "出生地不能含有特殊字符|";
        }
    }
    if(strlen(Birthcity) > 50)
    {
        isFlag = false;
        fieldText = fieldText + "出生地|";
   		msgText = msgText + "出生地最多只允许输入50个字符|";
    }
    if(GraduateSchool != "")
    {
        if(!CheckSpecialWord(GraduateSchool))
        {
            isFlag = false;
           fieldText = fieldText + "毕业学校 |";
   		    msgText = msgText +  "毕业学校不能含有特殊字符|";
        }
    }
        if(strlen(GraduateSchool) > 50)
    {
        isFlag = false;
        fieldText = fieldText + "毕业学校|";
   		msgText = msgText + "毕业学校最多只允许输入50个字符|";
    }
        if(IncomeYear != "")
    {
        if(!CheckSpecialWord(IncomeYear))
        {
            isFlag = false;
           fieldText = fieldText + "年收入情况 |";
   		    msgText = msgText +  "年收入情况不能含有特殊字符|";
        }
    }
    if(strlen(IncomeYear) > 50)
    {
        isFlag = false;
        fieldText = fieldText + "年收入情况|";
   		msgText = msgText + "年收入情况最多只允许输入50个字符|";
    }
    if(ddlLinkType =="" || TypeNum <= "1")
    {
        isFlag = false;
        fieldText = fieldText +  "联系人类型|";
   		msgText = msgText +  "请首先配置联系人类型|";
    }
    
    if(txtCustNam=="" || hfCustNo == "")
    {
        isFlag = false;
        fieldText = fieldText + "对应客户|";
   		msgText = msgText +  "请选择对应客户|";
    }
    if(txtLinkManName=="")
    {
        isFlag = false;
        fieldText = fieldText + "联系人姓名|";
   		msgText = msgText +  "请输入联系人姓名|";
    }
    if(txtLinkManName.Trim() != "")
    {
        if(!isName(txtLinkManName.Trim()))
        {
             isFlag = false;
            fieldText = fieldText + "联系人姓名|";
   		    msgText = msgText +  "请输入正确的联系人姓名|";
        }
       
    }    
    if(Handset.length > 0)
    {
         if(IsNumber(Handset) == null || Handset.length != 11)
        {
            isFlag = false;
            fieldText = fieldText + "手机号|";
   		    msgText = msgText +  "请输入正确的手机号|";
        }
    }
    if(txtAge.length > 0)
    {
         if(txtAge.length >= 3 || IsNumber(txtAge) == null)
        {
            isFlag = false;
            fieldText = fieldText + "年龄|";
   		    msgText = msgText +  "请输入正确的年龄|";
        }
    }
    if(txtBirthday.length > 0)
    {
        if(!isDate(txtBirthday))
        {
            isFlag = false;
            fieldText = fieldText + "生日|";
   		    msgText = msgText + "生日格式不正确|";    
        }
         if(StringToDate(txtBirthday)>StringToDate(GetNow()))
        {
            isFlag = false;
            fieldText = fieldText + "生日|";
   		    msgText = msgText +  "生日需早于当前日期|";
        }
    }
    
    if(txtAge.length > 0 && txtBirthday.length > 0 )
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
    if(strlen(Operation)>50)
    {
        isFlag = false;
        fieldText = fieldText + "负责业务|";
   		msgText = msgText + "负责业务最多只允许输入50个字符符|";
    }
    if(strlen(Remark)>1024)
    {
        isFlag = false;
        fieldText = fieldText + "备注|";
   		msgText = msgText + "备注最多只允许输入1024个字符符|";
    }
    if(strlen(Likes)>200)
    {
        isFlag = false;
        fieldText = fieldText + "爱好内容|";
   		msgText = msgText + "爱好内容最多只允许输入200个字符符|";
    }    
     if(RetVal!="")
    {
            isFlag = false;
            fieldText = fieldText + RetVal+"|";
   		    msgText = msgText +RetVal+  "不能含有特殊字符|";
    }

    if(!isFlag)
    {
        //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msgText);
        popMsgObj.Show(fieldText,msgText);
    }

    return isFlag;
    
}

//新建时
function ClearInput()
{
    window.location.reload();//页面重新加载，但有刷新    
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
    else if ("clear" == flag && document.getElementById("hfPagePhotoUrl").value != "")
    {
        if(confirm("确认删除相片吗！"))
        {
            document.getElementById("imgPhoto").src = "../../../Images/Pic/Pic_Nopic.jpg";  
            document.getElementById("hfPagePhotoUrl").value = "";
        }
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