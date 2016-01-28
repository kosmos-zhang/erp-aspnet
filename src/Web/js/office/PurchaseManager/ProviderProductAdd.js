//采购新建供应商物品推荐

var DtlCount = 0;

var page = "";
var CustName;
var CustNo ;
var ProductName ;
var ProductID ;
var Grade ;
var Joiner ;
var JoinerName ;
var StartJoinDate ;
var EndJoinDate ;

$(document).ready(function()
{
    requestobj = GetRequest();
    var intMasterProviderProductID = document.getElementById("txtIndentityID").value;
    
    recordnoparam = intMasterProviderProductID.toString();
    
    var CustName = requestobj['CustName'];
    var CustNo = requestobj['CustNo'];
    var ProductName = requestobj['ProductName'];
    var ProductID = requestobj['ProductID'];
    var Grade = requestobj['Grade'];
    var Joiner = requestobj['Joiner'];
    var JoinerName = requestobj['JoinerName'];
    var StartJoinDate = requestobj['StartJoinDate'];
    var EndJoinDate = requestobj['EndJoinDate'];
    
    var Isliebiao = requestobj['Isliebiao'];
    var PageIndex = requestobj['PageIndex'];
    var PageCount = requestobj['PageCount'];
    
    if(typeof(Isliebiao)!="undefined")
    {
        $("#btn_back").show();
        document.getElementById("hidIsliebiao").value = Isliebiao;
    }
    
    var ModuleID = document.getElementById("hidModuleID").value;
    var URLParams = "Isliebiao="+escape(Isliebiao)+"&ModuleID="+escape(ModuleID)+"&CustName="+escape(CustName)+"&CustNo="+escape(CustNo)+"&ProductName="+escape(ProductName)+
                    "&ProductID="+escape(ProductID)+"&Grade="+escape(Grade)+"&Joiner="+escape(Joiner)+"&JoinerName="+escape(JoinerName)+
                    "&StartJoinDate="+escape(StartJoinDate)+"&EndJoinDate="+escape(EndJoinDate)+"&PageIndex="+escape(PageIndex)+"&PageCount="+escape(PageCount)+""; 
                     
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
                document.getElementById("divTitle").innerText="供应商物品推荐";
//                GetFlowButton_DisplayControl();
        //        //显示返回按钮
        //        $("#btn_back").show();
                GetProviderProduct(recordnoparam);
            }
    }
}

//返回
function Back()
{ 
var URLParams = document.getElementById("hidSearchCondition").value;
    window.location.href='ProviderProductInfo.aspx?'+URLParams;
}

function GetProviderProduct(ID)
{
 
     $.ajax({
       type: "POST",//用POST方式传输
       dataType:"json",//数据格式:JSON
       url:"../../../Handler/Office/PurchaseManager/ProviderProductEdit.ashx",//目标地址
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
                        //供应商物品推荐
                        $("#txtCustName").attr("value",item.CustName);//供应商名称
                        $("#txtCustNo").attr("value",item.CustNo);//供应商编号
                        $("#HidProductID").attr("value",item.ProductID);//物品id
                        $("#txtProductName").attr("value",item.ProductName);//物品名称
                        $("#drpGrade").attr("value",item.Grade);//推荐程度
                        $("#HidJoiner").attr("value",item.Joiner);//推荐人id
                        $("#UsertxtJoiner").attr("value",item.JoinerName);//推荐人名称
                        $("#txtJoinDate").attr("value",item.JoinDate);//推荐时间
                        $("#txtRemark").attr("value",item.Remark);//推荐理由
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







//供应商物品推荐
function InsertProviderProductInfo() 
{ 

if(!CheckBaseInfo())return;

        var action = null;

         var fieldText = "";
         var msgText = "";
         var isFlag = true;
         
         
         var custNo = document.getElementById("txtCustNo").value.Trim();//供应商编号
         var productID=document.getElementById("HidProductID").value.Trim();//物品id
         var grade=document.getElementById("drpGrade").value.Trim();//推荐程度
         var joiner=document.getElementById("HidJoiner").value.Trim();//推荐人
         var joinDate=document.getElementById("txtJoinDate").value.Trim();//推荐时间
         var remark=document.getElementById("txtRemark").value.Trim();//推荐理由
         
        
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
         
         
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/PurchaseManager/ProviderProductAdd.ashx",
                  dataType:'json',//返回json格式数据
                  data: "custNo="+escape(custNo)+"&productID="+escape(productID)+"&grade="+escape(grade)+"&joiner="+escape(joiner)+"\
                        &joinDate="+escape(joinDate)+"&remark="+escape(remark)+"&action="+escape(action)+"&ID="+escape(txtIndentityID)+"",//数据
                
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
/*
* 基本信息校验
*/
function CheckBaseInfo()
{

     var fieldText = "";
      var msgText = "";
      var isFlag = true;        

    //不为空验证
    if(document.getElementById("txtCustName").value.Trim() == "")
    {
        isFlag = false;
        fieldText += "供应商名称|";
        msgText += "请输入供应商名称|";
    }
    if(document.getElementById("txtProductName").value.Trim() == "")
    {
        isFlag = false;
        fieldText += "物品名称|";
        msgText += "请输入物品名称|";
    }
    
    //限制字数
    var Remark=document.getElementById("txtRemark").value.Trim();//推荐理由
    if(strlen(Remark)>1024)
    {
        isFlag = false;
        fieldText += "推荐理由|";
   		msgText +=  "推荐理由仅限于1024个字符以内|";      
    }

    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
    }
    return isFlag;
}




//选择物品
function Fun_FillParent_Content(id,no,productname,price,unitid,unit,taxrate,taxprice,discount,standard)
{
    document.getElementById("HidProductID").value = id;
    document.getElementById("txtProductName").value = productname;
    document.getElementById('divProviderInfo').style.display='none';
}