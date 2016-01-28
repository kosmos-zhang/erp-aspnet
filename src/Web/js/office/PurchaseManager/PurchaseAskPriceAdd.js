    var pageCountHistory = 10;
    var currentPageIndexHistory = 1;
    var totalRecordHistory = 0;
    var OrderByHistory = "AskNo ASC AskOrder ASC";


    $(document).ready(function() {
    if($("#HiddenMoreUnit").val()=="False")
        { 
}
else
{
    document .getElementById ("SpProductCount").innerText="基本数量";
    document .getElementById ("spCount").style.display="none";
    document .getElementById ("spUsedUnitCount").style.display="block"; 
    
      document .getElementById ("spUnitID").innerText="基本单位";
    document .getElementById ("spUnitIDMoon").style.display="none";
    document .getElementById ("spUsedUnitID").style.display="block";
    }
        var requestobj = GetRequest(location.search);
        $("#HiddenURLParams").val(location.search);
        var SourcePage = requestobj['SourcePage'];
        if (SourcePage == "Info") {//列表页面进入
            //返回按钮可用
            $("#btn_back").css("display", "inline");
        }
        //    else if(SourcePage == "Desk")
        //    {//个人桌面进入8
        //        //返回按钮可用
        //        $("#btn_back").css("display", "inline");
        //    }
        else if (SourcePage == null) {//菜单栏进入
        }
        var intFromType = requestobj["intFromType"];
        if (intFromType != null) {//个人桌面进入
            $("#btn_back").css("display", "inline");
        }
        var ID = requestobj['ID'];
        if (ID != null) {
            FillPurchaseAskPrice(ID);
        }
        else {
            GetExtAttr('officedba.PurchaseAskPrice', null);
         }
        GetFlowButton_DisplayControl();
        //    fnGetExtAttr();
    });


function fnPrint()
{
    var ID = $("#ThisID").val();
    if(parseInt(ID) == 0)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先保存！");
        return;
    }
    window.open("../../../Pages/PrinttingModel/PurchaseManager/PurchaseAskPricePrint.aspx?ID="+ID);
}



function DoBack()
{
    var URLParams = document.getElementById("HiddenURLParams").value;
    var requestobj = GetRequest(URLParams);
    if(requestobj['SourcePage'] == "Info")
    {//来源于询价列表
        //替换ModuleID
        URLParams = URLParams.replace("ModuleID=2041601","ModuleID=2041602");
        URLParams = URLParams.replace("SourcePage=Info","SourcePage=Add");
        window.location.href = 'PurchaseAskPriceInfo.aspx?'+URLParams;
    }
    var intFromType = requestobj["intFromType"];
    if(intFromType != null)
    {//来源个人桌面
        var ModuleID = requestobj["ListModuleID"];
        URLParams = "ModuleID="+ModuleID;
        switch(intFromType)
        {
            case "2":
                //来源FlowMyApply.aspx
                window.location.href='../../../DeskTop.aspx';
                break;
            case "3":
                //来源FlowMyApply.aspx
                window.location.href='../../Personal/WorkFlow/FlowMyApply.aspx?'+URLParams;
                break;
            case "4":
                //FlowMyProcess.aspx
                window.location.href='../../Personal/WorkFlow/FlowMyProcess.aspx?'+URLParams;
                break;
            case "5":
                //FlowWaitProcess.aspx
                window.location.href='../../Personal/WorkFlow/FlowWaitProcess.aspx?'+URLParams;
                break;
            default:
                break;
        }
    }
}




function fnSelectAll()
{//全选
    $.each($("#DetailTable :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}

function fnAskPriceAgain()
{
    try{
        //保存按钮可用
        $("#imgSave").css("display", "inline");
        $("#imgUnSave").css("display", "none");
        $("#imgZcxj").css("display", "none");
        $("#imgUnZcxj").css("display", "inline");
      
        //询价次数+1
        $("#txtAskOrder").val(parseInt($("#txtAskOrder").val())+1);
        if (parseInt($("#txtAskOrder").val())>=2)
        {
             $("#imgHis").css("display", "inline");
             }
        //再次询价标志位设为1
        $("#ZCXJFlag").val(1);
    }catch(e){
    }
}

//提交审批成功和审批成功后调用一个函数0:提交审批成功  1:审批成功
function Fun_OperateFlowApply_Succeed(values) {
    try{
        $("#imgSave").css("display", "none");
        $("#imgUnSave").css("display", "inline");
        $("#btn_detailadd").css("display", "none");
        $("#imgUnAdd").css("display", "inline");
        $("#btn_detaildelete").css("display", "none");
        $("#imgUnDel").css("display", "inline");
        $("#imgZcxj").css("display", "none");
        $("#imgUnZcxj").css("display", "inline");
         
    }catch(e){
    }
}

function ChangeCurreny()
{//选择币种

    var IDExchangeRate = document.getElementById("ddlCurrencyType").value.Trim();
    document.getElementById("CurrencyTypeID").value = IDExchangeRate.split('_')[0];
    
    document.getElementById("txtExchangeRate").value =  IDExchangeRate.split('_')[1];
    
    var Rate = document.getElementById("txtExchangeRate").value.Trim();
    var signFrame = findObj("DetailTable",document);
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {
        var RealCount = 0;
        for(var i=1;i<parseInt(signFrame.rows.length);++i)
        {
            if(signFrame.rows[i].style.display!="none")
            {
             if (Rate==0)
        {
        
              $("#UnitPrice"+i).val((parseFloat(0)).toFixed($("#HiddenPoint").val()));
                $("#TaxPrice"+i).val((parseFloat(0)).toFixed($("#HiddenPoint").val()));
            }
            else
            {
               var temp1=parseFloat($("#UnitPriceHide"+i).val());
               var rate=parseFloat($("#TaxPriceHide"+i).val()); 
               
                $("#UnitPrice"+i).val( (parseFloat($("#UnitPriceHide"+i).val())/parseFloat(Rate)).toFixed($("#HiddenPoint").val()));
                $("#TaxPrice"+i).val((parseFloat($("#TaxPriceHide"+i).val())/parseFloat(Rate)).toFixed($("#HiddenPoint").val()));
            }
            }
       
        }
    }
    
    fnMergeDetail();
}
//修改页面初始化扩展属性值
function ExtAttControl_FillValue(data) {
    var strKey = $.trim($("#hiddKey").val()); //扩展属性字段值

    var arrKey = strKey.split('|');
    if (typeof (data) != 'undefined') {
        $.each(data, function(i, item) {
            for (var t = 0; t < arrKey.length; t++) {
                //不为空的字段名才取值
                if ($.trim(arrKey[t]) != '') {
                    $("#" + $.trim(arrKey[t])).val(item[$.trim(arrKey[t])]);

                }
            }

        });
    }
}
//--------------------扩展属性操作----------------------------------------------------------------------------//

function FillPurchaseAskPrice(ID)
{
    var ActionAskPrice = "Fill";
    var URLParams = "&ID="+ID;
    URLParams += "&ActionAskPrice="+ActionAskPrice;
    $.ajax(
    { 
        type: "POST",
        url: "../../../Handler/Office/PurchaseManager/PurchaseAskPriceAdd.ashx",
        dataType:'json',//返回json格式数据
        cache:false,
        data:URLParams,
        beforeSend:function()
        {
            AddPop();
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        complete:function(){hidePopup();},//接收数据完毕
        success:function(msg) 
        {
            //
            document.getElementById("HiddenAction").value = "Update";
            $.each(msg.PurAskPricePri,function(i,item)
            {
                if(item.ID!= null && item.ID != "")
                {
                    $("#AddTitle").css("display","none");
                    $("#UpdateTitle").css("display","inline");
                    fnFillPurchaseAskPrice(item);
                    fnStatus($("#txtBillStatusID").val(),$("#IsCite").val());
                }
                var TableName = "officedba.PurchaseAskPrice";
                GetExtAttr(TableName, msg.PurAskPricePri);
            });
            $.each(msg.PurAskPriceDetail,function(i,item)
            {
                if(item.ID!= null && item.ID != "")
                {
                    fnFillPurchaseAskPriceDetail(item);
                }
            });
            GetFlowButton_DisplayControl();
        } 
    }); 
}



function fnFillPurchaseAskPrice(item)
{
    document.getElementById("PurAskPriceNo_txtCode").value = item.AskNo;
    document.getElementById('PurAskPriceNo_txtCode').className='tdinput';
    document.getElementById('PurAskPriceNo_txtCode').style.width='90%';
    document.getElementById('PurAskPriceNo_ddlCodeRule').style.display='none';
    
     
    
    $("#txtTitle").attr("value",item.AskTitle);
    $("#ddlFromType").attr("value",item.FromType);
    fnChangeSource(item.FromType);
    $("#txtProviderID").attr("value",item.ProviderID);
    $("#txtProviderName").attr("value",item.ProviderName);
    $("#txtAskUserID").attr("value",item.AskUserID);
    
    $("#UserAskUserName").attr("value",item.AskUserName);
    if(item.TypeID != 0)
    $("#ddlTypeID_ddlCodeType").attr("value",item.TypeID);
    $("#txtDeptID").attr("value",item.DeptID);
    $("#DeptName").attr("value",item.DeptName);
    $("#txtAskOrder").attr("value",item.AskOrder);
      if (parseInt($("#txtAskOrder").val())>=2)
        {
             $("#imgHis").css("display", "inline");
             }
    $("#txtAskDate").attr("value",item.AskDate);
    
//    var ssss = item.CurrencyType +"_"+item.Rate;
//    $("#ddlCurrencyType").attr("value",ssss);
    
    for(var i=0;i<document.getElementById("ddlCurrencyType").options.length;++i)
    {
        if(document.getElementById("ddlCurrencyType").options[i].value.split('_')[0]== item.CurrencyType)
        {
            $("#ddlCurrencyType").attr("selectedIndex",i);
            break;
        }
    }
//    $("#CurrencyTypeID").attr("value",item.CurrencyType);
    
    $("#txtExchangeRate").attr("value", item.Rate);
    
    $("#chkIsAddTax").attr("checked",(item.isAddTax == "1")); 
    $("#txtCountTotal").attr("value",(parseFloat(item.CountTotal)).toFixed($("#HiddenPoint").val()));
    $("#txtTotalPrice").attr("value",(parseFloat(item.TotalPrice)).toFixed($("#HiddenPoint").val()));
    $("#txtTotalTax").attr("value",(parseFloat(item.TotalTax)).toFixed($("#HiddenPoint").val()));
    $("#txtTotalFee").attr("value",(parseFloat(item.TotalFee)).toFixed($("#HiddenPoint").val()));
    
    $("#txtDiscount").attr("value",(parseFloat(item.Discount)).toFixed($("#HiddenPoint").val()));
    $("#txtDiscountTotal").attr("value", (parseFloat(item.DiscountTotal)).toFixed($("#HiddenPoint").val()));
    $("#txtRealTotal").attr("value",(parseFloat(item.RealTotal)).toFixed($("#HiddenPoint").val()));
    $("#txtCreatorID").attr("value",item.Creator);
    
    $("#txtCreatorName").attr("value",item.CreatorName);
    $("#txtCreateDate").attr("value",item.CreateDate);
    $("#txtBillStatusID").attr("value",item.BillStatus);
    $("#txtBillStatusName").attr("value",item.BillStatusName);
    $("#txtConfirmorID").attr("value",item.Confirmor);
    
    $("#txtConfirmorName").attr("value",item.ConfirmorName);
    $("#txtConfirmorDate").attr("value",item.ConfirmDate);
    $("#txtModifiedUserID").attr("value",item.ModifiedUserID);
    $("#txtModifiedUserName").attr("value",item.ModifiedUserName);
    $("#txtCloserID").attr("value",item.Closer);
    
    $("#txtCloserName").attr("value",item.CloserName);
    $("#txtCloseDate").attr("value",item.CloseDate);
    $("#txtModifiedDate").attr("value",item.ModifiedDate);
    $("#txtRemark").attr("value",item.remark);
    $("#ThisID").attr("value",item.ID);
    $("#FlowStatus").val(item.FlowStatus);
    
    if(item.IsCite == "False")
    {//未被引用,且制单或执行状态
        if(($("#txtBillStatusID").val() == "1")||($("#txtBillStatusID").val() == "2"))
        {
            try{
                $("#imgSave").css("display", "inline");
                $("#imgUnSave").css("display", "none");
            }catch(e){
            }
        }
        else
        {
            try{
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");
            }catch(e){
            }
        }
    }
    else
    {
        try{
            $("#imgSave").css("display", "none");
            $("#imgUnSave").css("display", "inline");
        }catch(e){
        }
    }
    
            
}

function fnFillPurchaseAskPriceDetail(item) {
     
    var rowID = AddAskPriceSignRow();
    $("#ProductID"+rowID).attr("value",item.ProductID);
    $("#ProductNo"+rowID).attr("value",item.ProductNo);
    $("#ProductName"+rowID).attr("value",item.ProductName);
    $("#Specification"+rowID).attr("value",item.Specification);

    $("#ProductCount"+rowID).attr("value",     (parseFloat(item.ProductCount)).toFixed($("#HiddenPoint").val()));
    var producount= (parseFloat(item.ProductCount)).toFixed($("#HiddenPoint").val());
    $("#RequireDate"+rowID).attr("value",item.RequireDate);
    $("#UnitID"+rowID).attr("value",item.UnitID);
    $("#UnitName"+rowID).attr("value",item.UnitName);
    $("#UnitPrice"+rowID).attr("value",   (parseFloat(item.UnitPrice)).toFixed($("#HiddenPoint").val()));
    var unitPrice =(parseFloat(item.UnitPrice)).toFixed($("#HiddenPoint").val());
    var Rate = $("#txtExchangeRate").val()
      
     
    $("#UnitPriceHide"+rowID).attr("value", (parseFloat(item.UnitPrice)*parseFloat(Rate)).toFixed($("#HiddenPoint").val()));
    $("#DtlSColor" + rowID).attr("value", item.ColorName)
    ;
    $("#TaxPrice"+rowID).attr("value",  (parseFloat(item.TaxPrice)).toFixed($("#HiddenPoint").val()));
    var taxPrice= (parseFloat(item.TaxPrice)).toFixed($("#HiddenPoint").val());
    $("#TaxPriceHide"+rowID).attr("value",  (parseFloat(item.TaxPrice)*parseFloat(Rate)).toFixed($("#HiddenPoint").val()));
 
    $("#TaxRate"+rowID).attr("value",(parseFloat(item.TaxRate)).toFixed($("#HiddenPoint").val()));
    $("#TaxRateHide"+rowID).attr("value", (parseFloat(item.TaxRate)).toFixed($("#HiddenPoint").val()));
    $("#TotalPrice"+rowID).attr("value",  (parseFloat(item.TotalPrice)).toFixed($("#HiddenPoint").val()));
    
    $("#TotalFee"+rowID).attr("value", (parseFloat(item.TotalFee)).toFixed($("#HiddenPoint").val()));
    $("#TotalTax"+rowID).attr("value",  (parseFloat(item.TotalTax)).toFixed($("#HiddenPoint").val()));
    $("#ApplyReason"+rowID).attr("value",item.ApplyReason);
    $("#Remark"+rowID).attr("value",item.Remark);
    $("#FromBillID"+rowID).attr("value",item.FromBillID);
    
    $("#FromBillNo"+rowID).attr("value",item.FromBillNo);
    $("#FromLineNo"+rowID).attr("value",item.FromLineNo);

   
                          
      if($("#HiddenMoreUnit").val()=="True")
                          {
                           $("#UsedUnitCount"+rowID).attr("value",item.UsedUnitCount);
    $("#UsedPrice"+rowID).attr("value",item.UsedPrice); 
    var issign="";
                        if (item.FromBillNo!="")
                        {
                        issign="Bill";
                        }
        GetUnitGroupSelectEx(item.ProductID,"InUnit","SignItem_TD_UnitID_Select" + rowID,"ChangeUnit(this.id,"+rowID+","+unitPrice+","+taxPrice+")","unitdiv" + rowID,"","FillSelect('"+rowID +"','"+item .UsedUnitID+"','"+issign+"')");//绑定单位组 
                         }    
    
}
 function FillSelect(rowID,UsedUnitID,Fillsign)
 {
 if (Fillsign=="Bill")
 {
 document .getElementById ("SignItem_TD_UnitID_Select"+rowID ).disabled=true ;
 }
      jsSelectItemFromSelect(document .getElementById ("SignItem_TD_UnitID_Select"+rowID ),UsedUnitID);
 }
function jsSelectItemFromSelect(objSelect, objItemValue) {        
    //判断是否存在        
 
        for (var i = 0; i < objSelect.options.length; i++) 
        {        
        var sx=objSelect.options[i].value.split('|')[0].toString() ;
            if (sx== objItemValue)
             {        
                objSelect.options[i].selected =true ;        
                break;        
            }        
        }        
         
       
}  

//获取url中"?"符后的字串
function GetRequest(url)
{
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
function SavePurchaseAskPrice()
{
CountBaseNum();
    if(fnCheckAllInfo())
    {
        return;
    }
 
    var  URLParams = fnGetPriValue();
    URLParams += fnGetDetailValue() + GetExtAttrValue();
    URLParams += "&ZCXJFlag=" + $("#ZCXJFlag").val() ;
     
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/PurchaseManager/PurchaseAskPriceAdd.ashx",
        dataType:'json',//返回json格式数据
        cache:false,
        data:URLParams,
        beforeSend:function()
        {
            AddPop();
        }, 
        error: function()
        {
            hidePopup();
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(data) 
        {
            if(data.sta == 1) 
            { 
                if(document.getElementById("HiddenAction").value == "Add")
                {
                    document.getElementById("PurAskPriceNo_txtCode").value = data.data.split('#$@')[0];
                    document.getElementById('PurAskPriceNo_txtCode').className='tdinput';
                    document.getElementById('PurAskPriceNo_txtCode').style.width='90%';
                    document.getElementById('PurAskPriceNo_ddlCodeRule').style.display='none';
                    $("#PurAskPriceNo_txtCode").attr("disabled","disabled");
                    //设置编辑模式
                    document.getElementById("HiddenAction").value = "Update";
                    
                    document.getElementById("ThisID").value = data.data.split('#$@')[1];
                    
                    //设置源单类型不可改
                    $("#ddlFromType").attr("disabled","disabled");
                    
                }
                else if(document.getElementById("HiddenAction").value == "Update")
                {
                    //更新最后更新人
//                    $("#txtModifiedUserID").val(data.data.split('#')[0]);
                    $("#txtModifiedUserName").val(data.data.split('#')[3]);
                    $("#txtModifiedDate").val(data.data.split('#')[2]);
                }
                try{
                $("#imgZcxj").css("display", "inline");
      
                    $("#imgUnZcxj").css("display", "none");
                }catch(e){
                }
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
                GetFlowButton_DisplayControl();
            }
            else if(data.sta == 2)
            {
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","该编号已被使用，请输入未使用的编号！");
            }
              else    if (data.sta==0)
            {
              showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",data.info);
            }
            else 
            { 
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
            } 
            //再次询价标志为重置为0
            $("#ZCXJFlag").val(0);
        } 
        
    }); 
}

function AskPriceExistFromApply(orderno,sortno)
{
    var signFrame = document.getElementById("DetailTable");  
    
    if((typeof(signFrame) == "undefined")||signFrame ==null)
    {
        return false;
    }
    for(var i=1;i<signFrame.rows.length;++i)
    {
        var frombillNo = document.getElementById("FromBillNo"+i).value.Trim();
        var fromlineno = document.getElementById("FromLineNo"+i).value.Trim();
        
        if((signFrame.rows[i].style.display!="none")&&(frombillNo==orderno)&&(fromlineno == sortno))
        {
            return true;
        } 
    }
    return false;
}
function ShowProdInfo()
{ 
 popTechObj.ShowListCheckSpecial('SubStorageIn"+rowID+"','Check');
}


//多选明细方法

function GetValue()
{
 
   var ck = document.getElementsByName("CheckboxProdID");
        var strarr=''; 
        var str = "";
        for( var i = 0; i < ck.length; i++ )
        {
            if ( ck[i].checked )
            {
               strarr += ck[i].value+',';
            }
        }
         str = strarr.substring(0,strarr.length-1);
         if(str =="")
         {
            popMsgObj.ShowMsg('请先选择数据！');
            return;
         }
          $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/SupplyChain/ProductCheck.ashx?str='+str,//目标地址
           cache:false,
               data:'',//数据
               beforeSend:function(){},//发送数据之前
         
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $.each(msg.data,function(i,item)
                    {
                    //填充物品ID，物品编号，物品名称,单位ID，单位名称，规格，
                        if(!IsExistCheck(item.ProdNo))
                        {
  
                          var Index = AddAskPriceSignRow();  
                        $("#ProductID"+Index).attr("value",item.ID);
                        $("#ProductNo"+Index).attr("value",item.ProdNo);
                        $("#ProductName"+Index).attr("value",item.ProductName);
                        $("#UnitID"+Index).attr("value",item.UnitID);
                        $("#UnitName"+Index).attr("value",item.CodeName); 
                        $("#Specification"+Index).attr("value",item.Specification);
                        $("#UnitPrice"+Index).attr("value",item.TaxBuy);//去税进价
                         $("#TaxPrice"+Index).attr("value",item.StandardBuy);//含税进价
                         $("#TaxRate" + Index).attr("value", item.InTaxRate); //进项税率

                         $("#DtlSColor" + Index).attr("value", item.ColorName); //颜色
                              var Rate = $("#txtExchangeRate").val()  
                         $("#UnitPriceHide"+Index).attr("value", (parseFloat(item.TaxBuy)*parseFloat(Rate)).toFixed($("#HiddenPoint").val()));
                         $("#TaxPriceHide"+Index).attr("value",  (parseFloat(item.StandardBuy)*parseFloat(Rate)).toFixed($("#HiddenPoint").val()));
                         $("#TaxRateHide"+Index).attr("value",(parseFloat(item.InTaxRate)).toFixed($("#HiddenPoint").val()));
                          if($("#HiddenMoreUnit").val()=="True")
                          {
        GetUnitGroupSelectEx(item.ID,"InUnit","SignItem_TD_UnitID_Select" + Index,"ChangeUnit(this.id,"+Index+","+item.TaxBuy+","+item.StandardBuy+")","unitdiv" + Index,'',"FillContent("+Index+","+item.TaxBuy+","+item.StandardBuy+")");//绑定单位组
        
                         }       
                          
                        }
                   });
                     
                      },
             
               complete:function(){}//接收数据完毕
           });
      closeProductdiv();
}  

function FillContent(rowID,UnitPrice,hanshuiPrice)
{ 
 var EXRate = $("#SignItem_TD_UnitID_Select"+rowID).val().split('|')[1].toString(); /*比率*/
           $("#UsedPrice"+rowID).attr("value", (parseFloat(UnitPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
           $("#UsedPricHid"+rowID).attr("value", (parseFloat(UnitPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
             $("#TaxPrice"+rowID).attr("value", (parseFloat(hanshuiPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
              $("#TaxPriceHide"+rowID).attr("value", (parseFloat(hanshuiPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
              var planCount = $("#UsedUnitCount"+rowID).val();/*计划数量*/

           if (planCount !='')
           {
           var tempcount=parseFloat(planCount*EXRate).toFixed($("#HiddenPoint").val());
           var totalPrice=parseFloat(planCount*UnitPrice*EXRate).toFixed($("#HiddenPoint").val());
           var taxRate=$("#TaxRate"+rowID).val();
           var TotalFee=parseFloat(planCount*hanshuiPrice*EXRate).toFixed($("#HiddenPoint").val());
             var TotalTax=parseFloat(totalPrice*taxRate /100).toFixed($("#HiddenPoint").val());
            $("#TotalTax"+rowID).attr("value", (parseFloat(TotalTax)).toFixed($("#HiddenPoint").val()));
            $("#TotalFee"+rowID).attr("value", (parseFloat(TotalFee)).toFixed($("#HiddenPoint").val()));
            $("#TotalPrice"+rowID).attr("value", (parseFloat(totalPrice)).toFixed($("#HiddenPoint").val()));
           
          $("#ProductCount"+rowID).attr("value", (parseFloat(tempcount)).toFixed($("#HiddenPoint").val()));
          
           }
           fnMergeDetail();
}

function FillApplyContent(rowID,UnitPrice,hanshuiPrice,ProductCount,UsedUnitCount,UsedUnitID,UsedPrice,sssign)
{

if (sssign!='')
{
 FillSelect(rowID,UsedUnitID,"Bill");
 }
 var EXRate = $("#SignItem_TD_UnitID_Select"+rowID).val().split('|')[1].toString(); /*比率*/
 if (UsedPrice!='')
 {
     $("#UsedPrice"+rowID).attr("value", (parseFloat(UsedPrice)).toFixed($("#HiddenPoint").val()));
           $("#UsedPricHid"+rowID).attr("value", (parseFloat(UsedPrice)).toFixed($("#HiddenPoint").val()));
 }
 else
 {
     $("#UsedPrice"+rowID).attr("value", (parseFloat(UnitPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
           $("#UsedPricHid"+rowID).attr("value", (parseFloat(UnitPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
 }
       
             $("#TaxPrice"+rowID).attr("value", (parseFloat(hanshuiPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
              $("#TaxPriceHide"+rowID).attr("value", (parseFloat(hanshuiPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
              
                  $("#UsedUnitCount"+rowID).attr("value", ( parseFloat(UsedUnitCount)).toFixed($("#HiddenPoint").val()));
              var planCount = $("#UsedUnitCount"+rowID).val();/*计划数量*/

           if (planCount !='')
           {
           
           var totalPrice=parseFloat(planCount*UnitPrice*EXRate).toFixed($("#HiddenPoint").val());
           var taxRate=$("#TaxRate"+rowID).val();
           var TotalFee=parseFloat(planCount*hanshuiPrice*EXRate).toFixed($("#HiddenPoint").val());
             var TotalTax=parseFloat(totalPrice*taxRate /100).toFixed($("#HiddenPoint").val());
            $("#TotalTax"+rowID).attr("value", (parseFloat(TotalTax)).toFixed($("#HiddenPoint").val()));
            $("#TotalFee"+rowID).attr("value", (parseFloat(TotalFee)).toFixed($("#HiddenPoint").val()));
            $("#TotalPrice"+rowID).attr("value", (parseFloat(totalPrice)).toFixed($("#HiddenPoint").val()));
           
    
          
           }
           fnMergeDetail();
}
//保存时重新计算
function CountBaseNum()
{

    var List_TB=findObj("DetailTable",document);
    var rowlength=List_TB.rows.length;
    var countnum = 0;
    var countprice = 0;
    for(var i=1;i<rowlength;i++)
    {
        if(List_TB.rows[i].style.display!="none")
        { 
            
             if($("#HiddenMoreUnit").val()=="True")//启用时(重新计算)
             {
                var EXRate = $("#SignItem_TD_UnitID_Select"+i).val().split('|')[1].toString(); /*比率*/
                var OutCount = $("#UsedUnitCount"+i).val();/*计划数量*/
                 
                if (OutCount != '')
                {
                    var tempcount=parseFloat(OutCount*EXRate).toFixed($("#HiddenPoint").val()); 
                    $("#ProductCount"+i).val(tempcount);/*基本数量根据出库数量和比率算出*/ 
                }
            }
        }
    }
    
    
    
}

//选择单位时计算
 function ChangeUnit(ObjID/*下拉列表控件（单位）*/,RowID/*行号*/,UsedPrice/*基本单价*/,hanshuiPrice)
 {
 
 
    var EXRate = $("#SignItem_TD_UnitID_Select"+RowID).val().split('|')[1].toString(); /*比率*/
  
           $("#UsedPrice"+RowID).attr("value", (parseFloat(UsedPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
           $("#UsedPricHid"+RowID).attr("value", (parseFloat(UsedPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
             $("#TaxPrice"+RowID).attr("value", (parseFloat(hanshuiPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
              $("#TaxPriceHide"+RowID).attr("value", (parseFloat(hanshuiPrice)*parseFloat(EXRate)).toFixed($("#HiddenPoint").val()));
                
    //var UsedUnitCount = $("#BaseCount_SignItem_TD_Text_"+RowID).val();/*基本数量*/
    var OutCount = $("#UsedUnitCount"+RowID).val();/*计划数量*/
    
    if (OutCount != '')
    {
        var tempcount=parseFloat(OutCount*EXRate).toFixed($("#HiddenPoint").val());
        var tempprice=parseFloat(UsedPrice*EXRate).toFixed($("#HiddenPoint").val());
           var totalPrice=parseFloat(OutCount*tempprice).toFixed($("#HiddenPoint").val());
           var TaxPrice =parseFloat(hanshuiPrice*EXRate).toFixed($("#HiddenPoint").val());
           $("#TaxPrice"+RowID).val(TaxPrice);/*含税价根据原始含税价和比率算出*/
           $("#TaxPriceHide"+RowID).val(TaxPrice);
            
        $("#ProductCount"+RowID).val(tempcount);/*基本数量根据计划数量和比率算出*/
        $("#UsedPrice"+RowID).val(tempprice);/*实际单价根据基本单价和比率算出*/
             $("#TotalPrice"+RowID).val(tempprice);/*实际总价根据实际单价和实际数量算出*/
            
    
    }
    fnTotalInfo();
 }

function IsExistCheck(prodNo)
{
    var sign=false ;
    var signFrame = findObj("DetailTable",document);
    var DetailLength = 0;//明细长度
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {  
        for (i = 1; i < signFrame.rows.length; i++) 
        {
          var prodNo1 = document.getElementById("ProductNo" + i).value.Trim();
            if ((signFrame.rows[i].style.display != "none" )&&(prodNo1 == prodNo)) 
            {
                 sign= true;
                 break ;
            }
        }
    }
 
    return sign ;
}

function AddAskPriceSignRow(Flag)
{//新增明细行
    var signFrame = findObj("DetailTable",document);
    var rowID = signFrame.rows.length;
    
    var newTR = signFrame.insertRow(signFrame.rows.length);//添加行
    newTR.id = "DetailSignItem" + rowID; 
    var i = 0;    
    
    var newNameXH=newTR.insertCell(i++);//添加列:选择    
    newNameXH.className="cell";
    newNameXH.innerHTML="<input name='Dtlchk' id='Dtlchk" + rowID + "'   onclick=\"IfSelectAll('Dtlchk','checkall')\"  value="+rowID+" type='checkbox' size='20'  />";   

    var newNameTD=newTR.insertCell(i++);//添加列:序号
    newNameTD.className="cell";
    newNameTD.id="SortNo" + rowID; 
    
    var newProductID=newTR.insertCell(i++);//添加列:物品ID
    newProductID.style.display = "none";    
    newProductID.innerHTML = "<input id='ProductID" + rowID + "' type='text' class=\"tdinput\" style='width:90%;' />";
    
    var newProductNo=newTR.insertCell(i++);//添加列:物品编号
    newProductNo.className="cell";  
    if(Flag == "From")
    {   
                newProductNo.innerHTML = "<input id='ProductNo" + rowID + "' type='text' class=\"tdinput\" style='width:90%;'  readonly   />";  
    }
    else if(Flag != "From")
    {
               newProductNo.innerHTML = "<input id='ProductNo" + rowID + "' type='text' class=\"tdinput\" style='width:90%;' readonly onclick=\"GetProductInfo('"+rowID+"')\" />"; 
    }  
    
    var newProductName=newTR.insertCell(i++);//添加列:物品名称
    newProductName.className="cell";
    newProductName.innerHTML = "<input id='ProductName" + rowID + "' type='text' class=\"tdinput\"  style='width:90%;' disabled/>";
    
    var newSpecification=newTR.insertCell(i++);//添加列:规格
    newSpecification.className="cell";
    newSpecification.innerHTML = "<input id='Specification" + rowID + "' type='text' class=\"tdinput\"  style='width:90%;'disabled/>";

    var newSColor = newTR.insertCell(i++); //添加列:颜色
    newSColor.className = "cell";
    newSColor.innerHTML = "<input id='DtlSColor" + rowID + "' type='text' class=\"tdinput\"  style='width:90%;'disabled/>";
    
    
    if($("#HiddenMoreUnit").val()=="False")
        {
        
     var newProductCount=newTR.insertCell(i++);//添加列:采购数量
    newProductCount.className="cell";
    newProductCount.innerHTML = "<input id='ProductCount" + rowID + "' type='text' class=\"tdinput\" onblur=\"Number_round(this," + $("#HiddenPoint").val() + "); fnTotalInfo();\"   style='width:90%;'/>"; 
    }
    else
    {
    document .getElementById ("SpProductCount").innerText="基本数量";
    document .getElementById ("spCount").style.display="none";
    document .getElementById ("spUsedUnitCount").style.display="block";
        var newProductCount=newTR.insertCell(i++);//添加列:基本数量
    newProductCount.className="cell";
    newProductCount.innerHTML = "<input id='ProductCount"+rowID+"' type='text' class=\"tdinput\"    style='width:90%;' readonly='readonly' />"; 
    
          var newUsedUnitCount=newTR.insertCell(i++);//添加列:计划数量
    newUsedUnitCount.className="cell";
    newUsedUnitCount.innerHTML = "<input id='UsedUnitCount"+rowID+"' type='text' class=\"tdinput\"  value=''  style='width:90%;' onblur=\"Number_round(this," + $("#HiddenPoint").val() + "); fnTotalInfo();\"  />"; 
    }
    
    
    var newRequireDate=newTR.insertCell(i++);//添加列:交货日期
    newRequireDate.className="cell";
    newRequireDate.innerHTML = "<input id='RequireDate" + rowID + "'type='text' class=\"tdinput\"  style='width:90%;' readonly=\"readonly\" onclick=\"J.calendar.get();\"  />";
    
      if($("#HiddenMoreUnit").val()=="False")
        {
       var newUnitID=newTR.insertCell(i++);//添加列:单位ID
    newUnitID.style.display = "none";
    newUnitID.innerHTML = "<input id='UnitID" + rowID + "' type='hidden' style='width:90%;'/>";
    
    var newUnitName=newTR.insertCell(i++);//添加列:单位
    newUnitName.className="cell";
    newUnitName.innerHTML = "<input  id='UnitName" + rowID + "'type='text' class=\"tdinput\"  style='width:80%;'  disabled  />";
        }
        else
        {
        document .getElementById ("spUnitID").innerText="基本单位";
    document .getElementById ("spUnitIDMoon").style.display="none";
    document .getElementById ("spUsedUnitID").style.display="block";
    
    var newUnitID=newTR.insertCell(i++);//添加列:基本单位ID
    newUnitID.style.display = "none";
    newUnitID.innerHTML = "<input id='UnitID" + rowID + "' type='hidden' style='width:90%;'/>";
    
    var newUnitName=newTR.insertCell(i++);//添加列:基本单位
    newUnitName.className="cell";
    newUnitName.innerHTML = "<input  id='UnitName" + rowID + "'type='text' class=\"tdinput\"   style='width:80%;'  disabled  />";
    
         var newFitNametd=newTR.insertCell(i++);//添加列:实际单位
            newFitNametd.className="cell";
            newFitNametd.id = 'SignItem_TD_UnitID_'+rowID;
            newFitNametd.innerHTML = "<div id=\"unitdiv"+rowID+"\"></div>";//添加列内容
    
        }
        
         if($("#HiddenMoreUnit").val()=="False")
        {
          var newUnitPrice=newTR.insertCell(i++);//添加列:单价
    newUnitPrice.className="cell";
    newUnitPrice.innerHTML = "<input id='UnitPrice" + rowID + "'type='text' class=\"tdinput\" onblur=\"Number_round(this," + $("#HiddenPoint").val() + ");fnTotalInfo();\"  style='width:80%;'   />";
    
    var newUnitPriceHid=newTR.insertCell(i++);//添加列:单价
    newUnitPriceHid.style.display = "none";
    newUnitPriceHid.innerHTML = "<input id='UnitPriceHide" + rowID + "'type='text' class=\"tdinput\" onblur=\"Number_round(this," + $("#HiddenPoint").val() + ");fnTotalInfo();\"  style='width:80%;'   />";
}
else
{
document .getElementById ("spUnitPrice").style.display="none";
document .getElementById ("spUsedPrice").style.display="block";

 var newUnitPrice=newTR.insertCell(i++);//添加列:基本单价
      newUnitPrice.style.display = "none";
    newUnitPrice.innerHTML = "<input id='UnitPrice" + rowID + "'type='text' class=\"tdinput\"    style='width:80%;'   />";
    
    var newUnitPriceHid=newTR.insertCell(i++);//添加列:基本单价
    newUnitPriceHid.style.display = "none";
    newUnitPriceHid.innerHTML = "<input id='UnitPriceHide" + rowID + "'type='text' class=\"tdinput\"  style='width:80%;'   />";
    
     var newUsedPrice=newTR.insertCell(i++);//添加列:实际单价
    newUsedPrice.className="cell"; 
    newUsedPrice.innerHTML = "<input id='UsedPrice" + rowID + "'type='text' class=\"tdinput\" onblur=\"Number_round(this," + $("#HiddenPoint").val() + ");fnTotalInfo();\"   style='width:80%;'   />";
    
    var newUsedPricHid=newTR.insertCell(i++);//添加列:实际单价
    newUsedPricHid.style.display = "none";
    newUsedPricHid.innerHTML = "<input id='UsedPricHid" + rowID + "'type='text' class=\"tdinput\"   style='width:80%;'   />";
}

    var newTaxPric=newTR.insertCell(i++);//添加列:含税价
    newTaxPric.className="cell";
    newTaxPric.innerHTML = "<input id='TaxPrice" + rowID + "'type='text' class=\"tdinput\"  onblur=\"Number_round(this," + $("#HiddenPoint").val() + ");fnTotalInfo();\"  style='width:90%;'   />";
    
    var newTaxPricHide=newTR.insertCell(i++);//添加列:含税价隐藏
    newTaxPricHide.style.display = "none";
    newTaxPricHide.innerHTML = "<input id='TaxPriceHide" + rowID + "'type='text' class=\"tdinput\"  style='width:90%;'   />";
    
    var newTaxRate=newTR.insertCell(i++);//添加列:税率
    newTaxRate.className="cell";
    newTaxRate.innerHTML = "<input id='TaxRate" + rowID + "'type='text' class=\"tdinput\" onblur=\"Number_round(this," + $("#HiddenPoint").val() + ");fnTotalInfo();\"  style='width:70%;'   />";
    
    var newTaxRateHide=newTR.insertCell(i++);//添加列:税率隐藏
    newTaxRateHide.style.display = "none";
    newTaxRateHide.innerHTML = "<input id='TaxRateHide" + rowID + "'type='text' class=\"tdinput\"  style='width:90%;' disabled=\"disabled\"  />";
    
    var newTotalPrice=newTR.insertCell(i++);//添加列:金额
    newTotalPrice.className="cell";
    newTotalPrice.innerHTML = "<input id='TotalPrice" + rowID + "'type='text' class=\"tdinput\"  style='width:90%;'  disabled=\"disabled\"  />";
    
    var newTotalFee=newTR.insertCell(i++);//添加列:含税金额
    newTotalFee.className="cell";
    newTotalFee.innerHTML = "<input id='TotalFee" + rowID + "'type='text' class=\"tdinput\"  style='width:90%;' disabled=\"disabled\"   />";
    
    var newTotalTax=newTR.insertCell(i++);//添加列:税额
    newTotalTax.className="cell";
    newTotalTax.innerHTML = "<input id='TotalTax" + rowID + "'type='text' class=\"tdinput\"  style='width:90%;'  disabled=\"disabled\"  />";

    var newApplyReason=newTR.insertCell(i++);//添加列:原因
    newApplyReason.className="cell";
    newApplyReason.innerHTML = "<select class='tdinput' id='ApplyReason" + rowID + "'>" + document.getElementById("HidApplyReason").innerHTML + "</select>";
    
    var newRemark=newTR.insertCell(i++);//添加列:备注
    newRemark.style.display = "none";
    newRemark.innerHTML = "<input id='Remark" + rowID + "'type='text' class=\"tdinput\"  style='width:80%;'   />";
    
    var newFromBillID=newTR.insertCell(i++);//添加列:源单ID
    newFromBillID.style.display = "none";
    newFromBillID.innerHTML = "<input id='FromBillID" + rowID + "'type='text' class=\"tdinput\"  style='width:90%;'  disabled />";
    
    var newFromBillNo=newTR.insertCell(i++);//添加列:源单编号
    newFromBillNo.className="cell";
    newFromBillNo.innerHTML = "<input id='FromBillNo" + rowID + "'type='text' class=\"tdinput\"  style='width:90%;' disabled />";
    
    var newFromLineNo=newTR.insertCell(i++);//添加列:源单序号
    newFromLineNo.className="cell";
    newFromLineNo.innerHTML = "<input id='FromLineNo" + rowID + "'type='text' class=\"tdinput\"  style='width:90%;'  disabled />";
    GenerateNo(signFrame.rows.length);
    return rowID;
}



function GenerateNo(Edge)
{//生成序号
    var signFrame = findObj("DetailTable",document);
    var SortNo = 1;//起始行号
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {
        for(var i=1;i<Edge;++i)
        {
            if(signFrame.rows[i].style.display!="none")
            {
                document.getElementById("SortNo"+i).innerHTML = SortNo++;
            }
        }        
    }
}

function DeleteAskPriceSignRow()
{//删除明细行，需要将序号重新生成
    var signFrame = findObj("DetailTable",document);        
    var ck = document.getElementsByName("Dtlchk");
    for( var i = 0; i<ck.length;i++ )
    {
        var rowID = i+1;
        if ( ck[i].checked )
        {
           signFrame.rows[rowID].style.display="none";
        }
    }
    GenerateNo(signFrame.rows.length);
    fnTotalInfo();
}

function fnCheckAllInfo()
{
    var fieldText = "";
    var msgText = "";
    var isErrorFlag = false;
    var ssw = CheckSpecialWords();
    if (ssw != "") {
   isErrorFlag = true;
        var tmpKeys = ssw.split(',');
        for (var i = 0; i < tmpKeys.length; i++) {
            isErrorFlag = true;
            fieldText = fieldText + tmpKeys[i].toString() + "|";
            msgText = msgText + "不能含有特殊字符|";
        }
    }
    //新建时，编号选择手工输入时
    if (document.getElementById("HiddenAction").value=="Add")
    {
//        获取编码规则下拉列表选中项
        var codeRule = document.getElementById("PurAskPriceNo_ddlCodeRule").value.Trim();
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "")
        {
            //获取输入的编号
            var PurOrderNo = document.getElementById("PurAskPriceNo_txtCode").value.Trim();
            //编号必须输入
            if (PurOrderNo == "")
            {
                isErrorFlag = true;
                fieldText += "询价单编号|";
   		        msgText += "请输入询价单编号|";
            }
            else
            {
                if(!CodeCheck($.trim($("#PurAskPriceNo_txtCode").val())))
                {
                    isFlag = false;
                    fieldText = fieldText + "询价单编号|";
   		            msgText = msgText +  "编号只能由英文字母（a-z大小写均可）、数字（0-9）、字符（_-/.()[]{}）组成|";
                }
                else if(strlen($.trim($("#PurAskPriceNo_txtCode").val())) > 50)
                {
                    isErrorFlag = true;
                    fieldText += "询价单编号|";
   		            msgText += "询价单编号长度不大于50|";
   		        }
   		        
            }
        }    
    } 
    //主题不空
//    if (document.getElementById("txtTitle").value.Trim() == "")
//    {
//        isErrorFlag = true;
//        fieldText += "主题|";
//        msgText += "请输入主题|";
//    }
//    else 
    if(strlen(document.getElementById("txtTitle").value.Trim())>100)
    {
        isErrorFlag = true;
        fieldText += "主题|";
        msgText += "主题不能长于100|";
    }
    //采购类别不空
    if(document.getElementById("ddlTypeID_ddlCodeType").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "采购类别|";
        msgText += "请选择采购类别|";
    }
//    //交货方式不为空
//    if(document.getElementById("ddlTakeType_ddlCodeType").value == "")
//    {
//        isErrorFlag = true;
//        fieldText += "交货方式|";
//        msgText += "请选择交货方式|";
//    }
//    //运送方式不为空
//    if(document.getElementById("ddlCarryType_ddlCodeType").value == "")
//    {
//        isErrorFlag = true;
//        fieldText += "运送方式|";
//        msgText += "请选择运送方式|";
//    }
//    //结算方式不为空
//    if(document.getElementById("ddlPayType_ddlCodeType").value == "")
//    {
//        isErrorFlag = true;
//        fieldText += "结算方式|";
//        msgText += "请选择结算方式|";
//    }
//    //支付方式不为空
//    if(document.getElementById("ddlMoneyType_ddlCodeType").value == "")
//    {
//        isErrorFlag = true;
//        fieldText += "支付方式|";
//        msgText += "请选择支付方式|";
//    }
    //供应商不为空
    if (document.getElementById("txtProviderName").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "供应商|";
        msgText += "请选择供应商|";
    }
    //采购员不为空
    if (document.getElementById("UserAskUserName").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "询价员|";
        msgText += "请选择询价员|";
    }
    //部门不为空
    if (document.getElementById("DeptName").value.Trim() == "")
    {
        isErrorFlag = true;
        fieldText += "部门|";
        msgText += "请选择部门|";
    }
    //询价日期符合格式
    if (document.getElementById("txtAskDate").value.Trim() == ""||!isDate(document.getElementById("txtAskDate").value.Trim()))
    {
        isErrorFlag = true;
        fieldText += "询价日期|";
        msgText += "请输入正确的询价日期|";
    }
    if(strlen($("#txtRemark").val()) > 200)
    {
        isErrorFlag = true;
        fieldText += "备注|";
        msgText += "备注长度不能大于200|";
    }
    
    var signFrame = findObj("DetailTable",document);
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {
        var RealCount = 0;
        for(var i=1;i<parseInt(signFrame.rows.length);++i)
        {
            if(signFrame.rows[i].style.display!="none")
            {
                RealCount++;
                //明细中物品不能为空
                if (document.getElementById("ProductNo"+i).value.Trim() == "")
                {
                    isErrorFlag = true;
                    fieldText += "物品|";
                    msgText += "请输入物品|";
                }
                //明细中数量符合格式
                var ProductCount = document.getElementById("ProductCount"+i).value.Trim();
                if (document.getElementById("ProductCount"+i).value.Trim() == ""||!IsNumberOrNumeric(document.getElementById("ProductCount"+i).value.Trim(),14,  $("#HiddenPoint").val()))
                {
             
                    isErrorFlag = true;
                    fieldText += "计划数量|";
                    msgText += "请输入正确的计划数量|";
                }
                else
                {
                if (ProductCount>0)
                {}
                else
                {
                    isErrorFlag = true;
                    fieldText += "计划数量|";
                    msgText += " 计划数量需大于零|";
                
                }
                }
                //单价符合格式
                var UnitPrice = document.getElementById("UnitPrice"+i).value.Trim();
                if (document.getElementById("UnitPrice"+i).value.Trim() == ""||!IsNumberOrNumeric(document.getElementById("UnitPrice"+i).value.Trim(),14,   $("#HiddenPoint").val()))
                {
                    isErrorFlag = true;
                    fieldText += "单价|";
                    msgText += "请输入正确的单价|";
                }
                //含税价符合格式
                var TaxPrice = document.getElementById("TaxPrice"+i).value.Trim();
                if (document.getElementById("TaxPrice"+i).value.Trim() == ""||!IsNumberOrNumeric(document.getElementById("TaxPrice"+i).value.Trim(),22,   $("#HiddenPoint").val()))
                {
                    isErrorFlag = true;
                    fieldText += "含税价|";
                    msgText += "请输入正确的含税价|";
                }
                //折扣  
//                var DetailDisCount = document.getElementById("Discount"+i).value.Trim();
//                if (document.getElementById("Discount"+i).value.Trim() == ""||!IsNumberOrNumeric(document.getElementById("Discount"+i).value.Trim(),12,4))
//                {
//                    isErrorFlag = true;
//                    fieldText += "折扣|";
//                    msgText += "请输入正确的折扣|";
//                }  
                //税率
                var TaxRate = $("#TaxRate"+i).val()
                if(!IsNumberOrNumeric(TaxRate,12,   $("#HiddenPoint").val()))  
                {
                    isErrorFlag = true;
                    fieldText += "税率|";
                    msgText += "请输入正确的税率|";
                }
                //交货日期
                if (document.getElementById("RequireDate"+i).value.Trim() == ""||!isDate(document.getElementById("RequireDate"+i).value.Trim()))
                {
                    isErrorFlag = true;
                    fieldText += "交货日期|";
                    msgText += "请输入正确的交货日期|";
                }
//                //供应商
//                if (document.getElementById("ProviderID"+i).value == null)
//                {
//                    isErrorFlag = true;
//                    fieldText += "供应商|";
//                    msgText += "请选择供应商|";
//                }
                //原因
                if (document.getElementById("ApplyReason"+i).value.Trim() == null)
                {
                    isErrorFlag = true;
                    fieldText += "原因|";
                    msgText += "请输入正确的原因|";
                }
                if(strlen($("#Remark"+i).val()) > 200)
                {
                    isErrorFlag = true;
                    fieldText += "明细备注|";
                    msgText += "备注长度不能大于200|";
                }
            }
        }
        if(RealCount == 0)
        {
            isErrorFlag = true;
            fieldText += "采购询价单明细|";
            msgText += "请输入采购询价单明细|";
        }
    }
    else
    {
        isErrorFlag = true;
        fieldText += "采购询价单明细|";
        msgText += "请输入采购询价单明细|";
    }
    if(isErrorFlag == true)
    {
        popMsgObj.Show(fieldText,msgText);
    }
    return isErrorFlag;
}



function fnGetPriValue()
{
    var strParams = "";
    var ActionAskPrice = document.getElementById("HiddenAction").value;
    strParams += "&ActionAskPrice="+ActionAskPrice;
    var PurAskPriceNo = "";
    //新建时，编号选择手工输入时
    if (ActionAskPrice=="Add")
    {
        //获取编码规则下拉列表选中项
        var CodeRule = document.getElementById("PurAskPriceNo_ddlCodeRule").value.Trim();
        //如果选中的是 手工输入时，校验编号是否输入
        if (CodeRule == "")
        {
            //获取输入的编号
            PurAskPriceNo = document.getElementById("PurAskPriceNo_txtCode").value.Trim();
        }
        strParams += "&CodeRuleID="+CodeRule;        
    }
    else
    {
        PurAskPriceNo = document.getElementById("PurAskPriceNo_txtCode").value.Trim();
    } 
    strParams += "&ID="+escape($("#ThisID").val());
    strParams += "&PurAskPriceNo="+escape(PurAskPriceNo);
    strParams += "&Title="+escape($("#txtTitle").val());
    strParams += "&FromType="+escape($("#ddlFromType").val());
    strParams += "&ProviderID="+escape($("#txtProviderID").val());    
    
    strParams += "&AskUserID="+escape($("#txtAskUserID").val());
    strParams += "&TypeID="+escape($("#ddlTypeID_ddlCodeType").val());
    strParams += "&DeptID="+escape($("#txtDeptID").val());
    strParams += "&AskOrder="+escape($("#txtAskOrder").val());
    strParams += "&AskDate="+escape($("#txtAskDate").val());
    
    strParams += "&CurrencyType="+escape($("#CurrencyTypeID").val());
    strParams += "&ExchangeRate="+escape($("#txtExchangeRate").val());
    strParams += "&IsAddTax="+(escape($("#chkIsAddTax").attr("checked")?1:0));
    
    strParams += "&CountTotal="+escape($("#txtCountTotal").val());
    strParams += "&TotalPrice="+escape($("#txtTotalPrice").val());
    strParams += "&TotalTax="+escape($("#txtTotalTax").val());
    strParams += "&TotalFee="+escape($("#txtTotalFee").val());
    strParams += "&Discount="+escape($("#txtDiscount").val());
    
    strParams += "&DiscountTotal="+escape($("#txtDiscountTotal").val());
    strParams += "&RealTotal="+escape($("#txtRealTotal").val());
    
    strParams += "&CreatorID="+escape($("#txtCreatorID").val());
    strParams += "&CreateDate="+escape($("#txtCreateDate").val());
    
    if($("#txtBillStatusID").val()=="2")
    {
        $("#txtBillStatusID").attr("value","3");
        $("#txtBillStatusName").attr("value","变更");
        $("#txtConfirmorID").attr("value","");
        $("#txtConfirmorName").attr("value","");
        $("#txtConfirmorDate").attr("value","");
    }
    strParams += "&BillStatusID="+escape($("#txtBillStatusID").val());
    strParams += "&ConfirmorID="+escape($("#txtConfirmorID").val());
    strParams += "&ConfirmorDate="+escape($("#txtConfirmorDate").val());
    
    strParams += "&ModifiedUserID="+escape($("#txtModifiedUserID").val());
    strParams += "&ModifiedDate="+escape($("#txtModifiedDate").val());
    strParams += "&CloserID="+escape($("#txtCloserID").val());
    strParams += "&CloseDate="+escape($("#txtCloseDate").val());
    strParams += "&Remark="+escape($("#txtRemark").val());
    
    return strParams;
}

function fnGetDetailValue()
{
    var strParams = "";
    var signFrame = findObj("DetailTable",document);
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {
        var RealCount = 0;
        for(var i=1;i<parseInt(signFrame.rows.length);++i)
        {
            if(signFrame.rows[i].style.display!="none")
            {
                strParams += "&SortNo"+RealCount+"="+escape(document.getElementById("SortNo"+i).innerHTML);
                strParams += "&ProductID"+RealCount+"="+escape($("#ProductID"+i+"").val());
                strParams += "&ProductNo"+RealCount+"="+escape($("#ProductNo"+i+"").val());
                strParams += "&ProductName"+RealCount+"="+escape($("#ProductName"+i+"").val());
                strParams += "&ProductCount"+RealCount+"="+escape($("#ProductCount"+i+"").val());
                 if($("#HiddenMoreUnit").val()=="True")
                 {
                   strParams += "&UsedPrice"+RealCount+"="+escape($("#UsedPrice"+i+"").val());
                   strParams += "&UsedUnitCount"+RealCount+"="+escape($("#UsedUnitCount"+i+"").val());
                  var ExRate= $("#SignItem_TD_UnitID_Select"+i).val().split('|')[1].toString();
                   var UsedUnitID= $("#SignItem_TD_UnitID_Select"+i).val().split('|')[0].toString();
                strParams += "&ExRate"+RealCount+"="+escape( ExRate);
                strParams += "&UsedUnitID"+RealCount+"="+escape(UsedUnitID);
                 }
                strParams += "&RequireDate"+RealCount+"="+escape($("#RequireDate"+i+"").val());
                strParams += "&UnitID"+RealCount+"="+escape($("#UnitID"+i+"").val());
                strParams += "&UnitPrice"+RealCount+"="+escape($("#UnitPrice"+i+"").val());
                strParams += "&TaxPrice"+RealCount+"="+escape($("#TaxPrice"+i+"").val());  
                strParams += "&TaxRate"+RealCount+"="+escape($("#TaxRate"+i+"").val());
                strParams += "&TotalPrice"+RealCount+"="+escape($("#TotalPrice"+i+"").val());
                strParams += "&TotalFee"+RealCount+"="+escape($("#TotalFee"+i+"").val());
                strParams += "&TotalTax"+RealCount+"="+escape($("#TotalTax"+i+"").val());    
                strParams += "&ApplyReason"+RealCount+"="+escape($("#ApplyReason"+i+"").val());           
                strParams += "&Remark"+RealCount+"="+escape($("#Remark"+i+"").val());       
                strParams += "&FromBillID"+RealCount+"="+escape(document.getElementById("FromBillID"+i).value.Trim());
                strParams += "&FromLineNo"+RealCount+"="+escape(document.getElementById("FromLineNo"+i).value.Trim());
                if($("#FromBillID"+i+"").attr("value") != null)
                {
                    strParams += "&FromType"+RealCount+"="+escape($("ddlFromType").val());
                }
                else
                {
                    strParams += "&FromType"+RealCount+"="+"0";
                }
                RealCount++;
            }
        }
        strParams += "&Length="+RealCount;
    }
    return strParams;
}

function fnChangeSource(FromType)
{//切换源单类型
    var signFrame = document.getElementById("DetailTable");  
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {
        for(var i=signFrame.rows.length-1;i>0;--i)
        {
            signFrame.deleteRow(i);
        }
    }
    if(FromType != "0")
    {
        $("#imgGetDtl").css("display", "inline");
        $("#imgUnGetDtl").css("display", "none");
    }
    else
    {
        $("#imgGetDtl").css("display", "none");
        $("#imgUnGetDtl").css("display", "inline");
    }
    fnTotalInfo();
}

function GetProductInfo(rowID)
{
    popTechObj.ShowList(rowID);
//    popProductInfoUC.ShowList("ProductID"+rowID+"","ProductNo"+rowID+"","ProductName"+rowID+"","Specification"+rowID+""
//    ,"UnitID"+rowID+"","UnitName"+rowID+"","UnitPrice"+rowID+"","TaxPrice"+rowID+"","TaxPriceHide"+rowID+"");
}

function Fun_FillParent_Content(id,no,productname,dddf,unitid,unit,df,sdfge,discount,standard,fg,fgf,taxprice,price,taxrate)
{

    var Rate = $("#txtExchangeRate").val();
    var RowID = popTechObj.InputObj;
    $("#ProductID"+RowID).val(id);
    $("#ProductNo"+RowID).val(no);
    $("#ProductName"+RowID).val(productname);
  
    var unitPrice=(parseFloat(price)/parseFloat(Rate)).toFixed($("#HiddenPoint").val());
    $("#UnitPrice"+RowID).val((parseFloat(price)/parseFloat(Rate)).toFixed($("#HiddenPoint").val()));
    $("#UnitPriceHide"+RowID).val(  (parseFloat(price)).toFixed($("#HiddenPoint").val()));
    $("#UnitID"+RowID).val(unitid);
    $("#UnitName"+RowID).val(unit);
    $("#TaxRate"+RowID).val(taxrate);
      
      var taxPrice=(parseFloat(taxprice)/parseFloat(Rate)).toFixed($("#HiddenPoint").val());
    $("#TaxPrice"+RowID).val((parseFloat(taxprice)/parseFloat(Rate)).toFixed($("#HiddenPoint").val()));
    $("#TaxPriceHide"+RowID).val( (parseFloat(taxprice)).toFixed($("#HiddenPoint").val()));
//    $("#Discount"+RowID).val(discount);
    $("#Specification"+RowID).val(standard);
    
            if($("#HiddenMoreUnit").val()=="False")
            {}
            else
            {
             GetUnitGroupSelectEx(id,"InUnit","SignItem_TD_UnitID_Select" + RowID,"ChangeUnit(this.id,"+RowID+","+unitPrice+","+taxPrice+")","unitdiv" + RowID,'',"FillContent("+RowID+","+unitPrice+","+taxPrice+")");//绑定单位组
            }
    
    
    
    
}

function fnSelectSource()
{
    var SourceType = $("#ddlFromType").attr("value");
    switch(SourceType)
    {
        case '0':
        {//无来源
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择源单类型！");            
            break;
        }
        case '1':
        {//来源采购申请
            popPurchaseApplyUC.ShowList('AskPrice');
            break;
        }
        case '2':
        {//来源采购计划 
            var ProviderID = $("#txtProviderID").val();
//            if(ProviderID=="")
//            {
//                popMsgObj.ShowMsg("请选择供应商！");
//                return;
//            }
            popPurchasePlanUC.ShowList("AskPrice",ProviderID);
            break;
        }
    }
}

function fnMergeDetail()
{
    fnTotalInfo();
}



function fnTotalInfo(Flag) 
{//Flag为什么事件激发的该函数
 try
 {
 

    var CountTotal = 0; //数量合计
    var TotalPrice = 0; //金额合计
    var Tax = 0; //税额合计
    var TotalFee = 0; //含税金额合计
    if(!IsNumberOrNumeric($("#txtDiscount").val(), 12,    $("#HiddenPoint").val())||$("#txtDiscount").val()>100||$("#txtDiscount").val()<0)
    {
        $("#txtDiscount").val(100);
    }
    var Discount = $("#txtDiscount").val(); //整单折扣
    var DiscountTotal = 0; //折扣金额
    var RealTotal = 0; //折后含税金额
    
    var signFrame = findObj("DetailTable", document);
    for (i = 1; i < signFrame.rows.length; i++) 
    {
        if (signFrame.rows[i].style.display != "none") 
        {
            var rowid = i;
            if(IsNumberOrNumeric($("#ProductCount" + rowid).val(), 14,    $("#HiddenPoint").val()))
            {
            
                $("#ProductCount" + rowid).val((parseFloat($("#ProductCount" + rowid).val())).toFixed($("#HiddenPoint").val()));
                if($("#ProductCount" + rowid).val()<0)
                {
                    $("#ProductCount" + rowid).attr("value","");                   
                }
            }  
            else
            {
                $("#ProductCount" + rowid).attr("value","");     
            }           
            var pCountDetail=0;
             if($("#HiddenMoreUnit").val()=="True")
             {
             pCountDetail=($("#UsedUnitCount" + rowid).val()=="")?0:$("#UsedUnitCount" + rowid).val(); //实际数量
               var EXRate = $("#SignItem_TD_UnitID_Select"+rowid).val().split('|')[1].toString(); /*比率*/
                   $("#ProductCount" + rowid).attr("value",(parseFloat (pCountDetail *EXRate).toFixed($("#HiddenPoint").val())));     
             }
             else
             {
             pCountDetail=($("#ProductCount" + rowid).val()=="")?0:$("#ProductCount" + rowid).val(); //数量
             }
             
            if(IsNumberOrNumeric($("#UnitPrice" + rowid).val(), 14,    $("#HiddenPoint").val()))
            {
            
                $("#UnitPrice" + rowid).val( (parseFloat($("#UnitPrice" + rowid).val())).toFixed($("#HiddenPoint").val()));
                if($("#UnitPrice" + rowid).val()<0)
                {
                    $("#UnitPrice" + rowid).val("");                  
                }
            }  
            else
            {
                $("#UnitPrice" + rowid).attr("value","");     
            }
            
            
            var UnitPriceDetail =0;
               if($("#HiddenMoreUnit").val()=="True")
               {
                   UnitPriceDetail=    ($("#UsedPrice" + rowid).val()=="")?0:$("#UsedPrice" + rowid).val(); //单价
               }
               else
               {
       UnitPriceDetail=    ($("#UnitPrice" + rowid).val()=="")?0:$("#UnitPrice" + rowid).val(); //单价
           }
            if(IsNumberOrNumeric($("#TaxPrice" + rowid).val(), 22,    $("#HiddenPoint").val()))
            {
           
            
                $("#TaxPrice" + rowid).val(  (parseFloat($("#TaxPrice" + rowid).val())).toFixed($("#HiddenPoint").val()));
                if($("#TaxPrice" + rowid).val()<0)
                {
                    $("#TaxPrice" + rowid).attr("value","");
                   
                }
            }  
            else
            {
                $("#TaxPrice" + rowid).attr("value","");     
            }
            var TaxPriceDetail = ($("#TaxPrice" + rowid).val()=="")?0:$("#TaxPrice" + rowid).val(); //含税价
            
            if(IsNumberOrNumeric($("#TaxRate" + rowid).val(), 12,    $("#HiddenPoint").val()))
            {
            
             
                $("#TaxRate" + rowid).val( (parseFloat($("#TaxRate" + rowid).val())).toFixed($("#HiddenPoint").val()));
                if($("#TaxRate" + rowid).val()<0)
                {
                    $("#TaxRate" + rowid).val("");
                   
                }
            }  
            var TaxRateDetail =($("#TaxRate" + rowid).val()=="")?0:$("#TaxRate" + rowid).val();//税率
             (parseFloat($("#TaxRate" + rowid).val())).toFixed($("#HiddenPoint").val())
             
             
            var TotalFeeDetail = (parseFloat(TaxPriceDetail * pCountDetail)).toFixed($("#HiddenPoint").val()); //含税金额
            
            var TotalPriceDetail = (parseFloat(UnitPriceDetail * pCountDetail)).toFixed($("#HiddenPoint").val()); //金额
            var TotalTaxDetail =(parseFloat(TotalPriceDetail * TaxRateDetail/100)).toFixed($("#HiddenPoint").val()); //税额
            $("#TotalFee" + rowid).val(  ( parseFloat(TotalFeeDetail)).toFixed($("#HiddenPoint").val())); //含税金额
            $("#TotalPrice" + rowid).val(  ( parseFloat(TotalPriceDetail)).toFixed($("#HiddenPoint").val())); //金额
            $("#TotalTax" + rowid).val(( parseFloat(TotalTaxDetail)).toFixed($("#HiddenPoint").val())); //税额
            TotalPrice += parseFloat(TotalPriceDetail);
            Tax += parseFloat(TotalTaxDetail);
            TotalFee += parseFloat(TotalFeeDetail);
            CountTotal += parseFloat(pCountDetail);
         DiscountTotal += (100 - parseFloat(100))*parseFloat(pCountDetail)*parseFloat(TaxPriceDetail)/100;


        }
    }
      
       
    $("#txtTotalPrice").val(( parseFloat(TotalPrice)).toFixed($("#HiddenPoint").val()));//金额合计
    $("#txtTotalTax").val( ( parseFloat(Tax)).toFixed($("#HiddenPoint").val()));//税额合计
    $("#txtTotalFee").val( ( parseFloat(TotalFee)).toFixed($("#HiddenPoint").val()));//含税金额合计
    $("#txtCountTotal").val( ( parseFloat(CountTotal)).toFixed($("#HiddenPoint").val())); //数量合计
    DiscountTotal += TotalFee * (100 - Discount) / 100
    $("#txtDiscountTotal").val( ( parseFloat(DiscountTotal)).toFixed($("#HiddenPoint").val())); //折扣金额
    $("#txtRealTotal").val(  ( parseFloat(TotalFee * Discount / 100)).toFixed($("#HiddenPoint").val())); //折后含税金额
    }
    catch (Error )
    {}
}


function fnChangeAddTax()
{//改变是否为增值税
    var isAddTax = document.getElementById("chkIsAddTax").checked;
    var Rate = $("#txtExchangeRate").val();
    if(isAddTax == true)
    {//是增值税则
        document.getElementById("AddTax").innerHTML="是增值税";
        var signFrame = findObj("DetailTable",document);
        if((typeof(signFrame) != "undefined")&&(signFrame !=null))
        {
            var ck = document.getElementsByName("Dtlchk");
            for( var i = 0; i<ck.length;i++ )
            {
                var rowID = i+1;
                if(signFrame.rows[rowID].style.display!="none")
                {
                
                
                    document.getElementById("TaxPrice"+rowID).value=
                       ( parseFloat(document.getElementById("TaxPriceHide"+rowID).value.Trim())/parseFloat(Rate)).toFixed($("#HiddenPoint").val());//含税价等于隐藏域的含税价
                    document.getElementById("TaxRate"+rowID).value=document.getElementById("TaxRateHide"+rowID).value.Trim();//税率等于隐藏域的税率
                }
            }
        }
    }
    else
    {
        document.getElementById("AddTax").innerHTML="非增值税";
        var signFrame = findObj("DetailTable",document);
        if((typeof(signFrame) != "undefined")&&(signFrame !=null))
        {
            var ck = document.getElementsByName("Dtlchk");
            for( var i = 0; i<ck.length;i++ )
            {
                var rowID = i+1;
                if(signFrame.rows[rowID].style.display!="none")
                {
                    document.getElementById("TaxPrice"+rowID).value=document.getElementById("UnitPrice"+rowID).value.Trim() //含税价等于单价
                    document.getElementById("TaxRate"+rowID).value=0;//税率等于0
                }
            }
        }
    }
    fnTotalInfo();
}

function Fun_ConfirmOperate()
{//确认
    if(!confirm("是否执行确认操作？"))
    return;
    
    
//    $("#HiddenAction").attr("value","Confirm");
    var ActionAskPrice ="Confirm";
    var ID = $("#ThisID").val();
        
    var URLParams = "ActionAskPrice="+ActionAskPrice;
    URLParams += "&ID="+ID;
    URLParams += "&No="+$("#PurAskPriceNo_txtCode").val();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/PurchaseManager/PurchaseAskPriceAdd.ashx",
        dataType:'json',//返回json格式数据
        cache:false,
        data:URLParams,
        beforeSend:function()
        {
            AddPop();
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(msg) 
        {
            var aaa = msg.data.split('#');
            $("#txtConfirmorID").attr("value",aaa[0]);
            $("#txtConfirmorName").attr("value",aaa[1]);
            $("#txtConfirmorDate").attr("value",aaa[2]);
            $("#txtModifiedUserName").val(aaa[3]);
            $("#txtModifiedDate").val(aaa[2]);
            //更改状态
            $("#txtBillStatusID").attr("value","2");
            $("#txtBillStatusName").attr("value","执行");
            fnStatus($("#txtBillStatusID").val(),$("#IsCite").val());
            
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","确认成功！");
            GetFlowButton_DisplayControl();
        } 
    }); 
}

function Fun_UnConfirmOperate()
{
    if(!confirm("是否执行取消确认操作？"))
        return;
    var URLParams = "ActionAskPrice=CancelConfirm";
    URLParams += "&ID="+$("#ThisID").val();
    URLParams += "&No="+$("#PurAskPriceNo_txtCode").val();
    URLParams += "&FromType="+document.getElementById("ddlFromType").value.Trim();
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/PurchaseManager/PurchaseAskPriceAdd.ashx",
        dataType:'json',//返回json格式数据
        cache:false,
        data:URLParams,
        beforeSend:function()
        {
            AddPop();
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(msg) 
        {
            if(msg.sta == 1)
            {
            fnAfterConcelConfirm(msg.sta,msg.data.split('#'));
            fnStatus($("#txtBillStatusID").val(),$("#IsCite").val());
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","取消确认成功！");
            GetFlowButton_DisplayControl();
            }
            else if(msg.sta == 2)
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","该单据已被其它单据调用了，不允许取消确认！");
            }
        } 
    });
}

//取消确认后
function fnAfterConcelConfirm(flag,BackValue)
{
    try{
        switch(flag)
        {
            case 1:
                //单据状态，确认人，确认时间
                $("#txtConfirmorID").val("");
                $("#txtConfirmorName").val("");
                $("#txtConfirmorDate").val("");
                $("#txtModifiedUserID").val(BackValue[1]);
                $("#txtModifiedUserName").val(BackValue[1]);
                $("#txtModifiedDate").val(BackValue[0]);
                $("#txtBillStatusID").attr("value","1");
                $("#txtBillStatusName").attr("value","制单");
                break;
            case 2:
                break;
        }
        $("#FlowStatus").val("5");
    }catch(e){
    }
}


function Fun_CompleteOperate(isComplete)
{
    try{
    if(isComplete)
    {
        if(!confirm("确认执行结单操作么？"))
            return;
        $("#txtBillStatusID").attr("value","4");
        $("#txtBillStatusName").attr("value","手工结单");
        
//        $("#HiddenAction").attr("value","Complete");
        var ActionAskPrice = "Complete";
        var ID = $("#ThisID").attr("value");
            
        var URLParams = "ActionAskPrice="+ActionAskPrice;
        URLParams += "&ID="+ID;
        $.ajax({ 
            type: "POST",
            url: "../../../Handler/Office/PurchaseManager/PurchaseAskPriceAdd.ashx",
            dataType:'json',//返回json格式数据
            cache:false,
            data:URLParams,
            beforeSend:function()
            {
                AddPop();
            }, 
            error: function()
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
            }, 
            success:function(msg) 
            {
                var aaa = msg.data.split('#');
                $("#txtCloserID").attr("value",aaa[0]);
                $("#txtCloserName").attr("value",aaa[1]);
                $("#txtCloseDate").attr("value",aaa[2]);
                
                $("#txtModifiedUserName").val(aaa[3]);
                $("#txtModifiedDate").val(aaa[2]);
                
                fnStatus($("#txtBillStatusID").val(),$("#IsCite").val());
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","结单成功！");
                GetFlowButton_DisplayControl();
            } 
        }); 
    }
    else
    {
        if(!confirm("确认执行取消结单操作么？"))
            return;
        $("#txtBillStatusID").attr("value","2");
        $("#txtBillStatusName").attr("value","执行");
        
//        $("#HiddenAction").attr("value","ConcelComplete");
        var ActionAskPrice = "ConcelComplete";
        var ID = $("#ThisID").attr("value");
            
        var URLParams = "ActionAskPrice="+ActionAskPrice;
        URLParams += "&ID="+ID;
        $.ajax({ 
            type: "POST",
            url: "../../../Handler/Office/PurchaseManager/PurchaseAskPriceAdd.ashx",
            dataType:'json',//返回json格式数据
            cache:false,
            data:URLParams,
            beforeSend:function()
            {
                AddPop();
            }, 
            error: function()
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
            }, 
            success:function(msg) 
            {
                $("#txtCloserID").attr("value","");
                $("#txtCloserName").attr("value","");
                $("#txtCloseDate").attr("value","");
//                $("#imgUnSave").css("display", "none");
//                $("#imgSave").css("display", "inline");
var aaa = msg.data.split('#');
                $("#txtModifiedUserName").val(aaa[0]);
                $("#txtModifiedDate").val(aaa[1]);
                
                fnStatus($("#txtBillStatusID").val(),$("#IsCite").val());
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","取消结单成功！");
                GetFlowButton_DisplayControl();
            } 
        }); 
    }
    }catch(e){
    }    
}

function Fun_FlowApply_Operate_Succeed(operateType)
{
    try{
    if(operateType == "0")
    {//提交审批成功后,不可改
        $("#imgUnSave").css("display", "inline");
        $("#imgSave").css("display", "none");
        
        $("#imgZcxj").css("display", "none");
        $("#imgUnZcxj").css("display", "inline");
   
        $("#imgAdd").css("display", "none");
        $("#imgUnAdd").css("display", "inline");
        $("#imgDel").css("display", "none");
        $("#imgUnDel").css("display", "inline");
        $("#imgGetDtl").css("display", "none");
        $("#imgUnGetDtl").css("display", "inline"); 
        $("#btnGetGoods").css("display", "none");//条码扫描按钮
    }
    else if(operateType == "1")
    {//审批成功后，不可改
        $("#imgUnSave").css("display", "inline");
        $("#imgSave").css("display", "none");
        
        $("#imgZcxj").css("display", "none");
        $("#imgUnZcxj").css("display", "inline");
        
        $("#imgAdd").css("display", "none");
        $("#imgUnAdd").css("display", "inline");
        $("#imgDel").css("display", "none");
        $("#imgUnDel").css("display", "inline");
        $("#imgGetDtl").css("display", "none");
        $("#imgUnGetDtl").css("display", "inline"); 
        $("#btnGetGoods").css("display", "none");//条码扫描按钮
    }
    else if(operateType == "2")
    {//审批不通过
        $("#imgSave").css("display", "inline");
        $("#imgUnSave").css("display", "none");
        
        $("#imgUnZcxj").css("display", "none");
        $("#imgZcxj").css("display", "inline");
       
        $("#imgAdd").css("display", "inline");
        $("#imgUnAdd").css("display", "none");
        $("#imgDel").css("display", "inline");
        $("#imgUnDel").css("display", "none");
        $("#imgGetDtl").css("display", "inline");
        $("#imgUnGetDtl").css("display", "none"); 
        $("#btnGetGoods").css("display", "inline");//条码扫描按钮
    }
    else if(operateType == "3")
    {//撤销审批
        $("#imgSave").css("display", "inline");
        $("#imgUnSave").css("display", "none");
        $("#imgAdd").css("display", "inline");
        $("#imgUnAdd").css("display", "none");
        $("#imgDel").css("display", "inline");
        $("#imgUnDel").css("display", "none");
        $("#imgGetDtl").css("display", "inline");
        $("#imgUnGetDtl").css("display", "none");
        $("#imgZcxj").css("display","inline");
        
        $("#imgUnZcxj").css("display","none"); 
        $("#btnGetGoods").css("display", "inline");//条码扫描按钮 
    }
    }catch(e){
    }
}

//根据单据状态决定页面按钮操作
function fnStatus(BillStatus,IsCite) {
    try{
    switch (BillStatus) { //单据状态（1制单，2执行，3变更，4手工结单，5自动结单）
        case '1': //制单
//            $("#HiddenAction").val('Add');
            fnFlowStatus($("#FlowStatus").val());
            break;
        case '2': //执行
            if(IsCite == "True")
            {//被引用不可编辑
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");
            }
            else
            {
                $("#HiddenAction").val('Update');
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");
                $("#imgAdd").css("display", "inline");
                $("#imgUnAdd").css("display", "none");
                $("#imgDel").css("display", "inline");
                $("#imgUnDel").css("display", "none");
                $("#imgGetDtl").css("display", "inline");
                $("#imgUnGetDtl").css("display", "none"); 
                $("#btnGetGoods").css("display", "inline");//条码扫描按钮 
            }
            
            break;
        case '3': //变更
            $("#HiddenAction").val('Update');
            $("#FromType").attr("disabled", "disabled");
            $("#imgSave").css("display", "inline");
            $("#imgUnSave").css("display", "none");
            break;
        case '4': //手工结单
            $("#imgSave").css("display", "none");
            $("#imgUnSave").css("display", "inline");
            $("#imgAdd").css("display", "none");
            $("#imgUnAdd").css("display", "inline");
            $("#imgDel").css("display", "none");
            $("#imgUnDel").css("display", "inline");
            $("#imgGetDtl").css("display", "none");
            $("#imgUnGetDtl").css("display", "inline");
            $("#imgZcxj").css("display", "none");
              
            $("#imgUnZcxj").css("display", "inline");
            $("#btnGetGoods").css("display", "none");//条码扫描按钮
            break;

        case '5':
            $("#imgSave").css("display", "none");
            $("#imgUnSave").css("display", "inline");
            $("#imgZcxj").css("display", "none");
             
            $("#imgUnZcxj").css("display", "inline");
            break;
    }
    }catch(e)
    {
    }
}

function fnFlowStatus(FlowStatus)
{
try{
    switch (FlowStatus) {
                case "0": //待提交审批 
                    if($("#ThisID").val() > 0)
                    {
                    $("#imgZcxj").css("display", "inline");
                    $("#imgUnZcxj").css("display", "none");      
                    }                    
                    break;
                case " ": //待提交审批 
                    if($("#ThisID").val() > 0)
                    {
                    $("#imgZcxj").css("display", "inline");
                    $("#imgUnZcxj").css("display", "none");  
                    }                        
                    break;
                case "1": //当前单据正在待审批
                    $("#imgSave").css("display", "none");
                    $("#imgUnSave").css("display", "inline");
                    $("#imgAdd").css("display", "none");
                    $("#imgUnAdd").css("display", "inline");
                    $("#imgDel").css("display", "none");
                    $("#imgUnDel").css("display", "inline");
                    $("#imgGetDtl").css("display", "none");
                    $("#imgUnGetDtl").css("display", "inline");
                    $("#imgZcxj").css("display", "none");
                    $("#imgUnZcxj").css("display", "inline"); 
                    $("#btnGetGoods").css("display", "none");//条码扫描按钮                   
                    break;
                case "2": //当前单据正在审批中
                    $("#imgSave").css("display", "none");
                    $("#imgUnSave").css("display", "inline");
                    $("#imgAdd").css("display", "none");
                    $("#imgUnAdd").css("display", "inline");
                    $("#imgDel").css("display", "none");
                    $("#imgUnDel").css("display", "inline");
                    $("#imgGetDtl").css("display", "none");
                    $("#imgUnGetDtl").css("display", "inline");
                    $("#imgZcxj").css("display", "none");
                    $("#imgUnZcxj").css("display", "inline");
                    $("#btnGetGoods").css("display", "none");//条码扫描按钮
                    break;
                case "3": //当前单据已经通过审核
                    //制单状态的审批通过单据,不可修改
                    if ($("#txtBillStatusID").val() == "1") {
                        $("#imgSave").css("display", "none");
                        $("#imgUnSave").css("display", "inline");
                        $("#imgAdd").css("display", "none");
                        $("#imgUnAdd").css("display", "inline");
                        $("#imgDel").css("display", "none");
                        $("#imgUnDel").css("display","inline");
                        $("#imgGetDtl").css("display","none");
                        $("#imgUnGetDtl").css("display", "inline");
                        $("#btnGetGoods").css("display", "none");//条码扫描按钮
                    }

                    break;
                case "4": //当前单据审批未通过
                    $("#imgGetDtl").css("display", "inline");
                    $("#imgUnGetDtl").css("display", "none"); 
                    $("#imgZcxj").css("display", "inline");
                    $("#imgUnZcxj").css("display", "none");
                    break;
                case "5":
                    $("#imgSave").css("display", "inline");
                    $("#imgUnSave").css("display", "none");
                    $("#imgAdd").css("display", "inline");
                    $("#imgUnAdd").css("display", "none");
                    $("#imgDel").css("display", "inline");
                    $("#imgUnDel").css("display", "none");
                    $("#imgGetDtl").css("display", "inline");
                    $("#imgUnGetDtl").css("display", "none"); 
                    $("#imgZcxj").css("display", "inline");
                    $("#imgUnZcxj").css("display", "none");
                    $("#btnGetGoods").css("display", "inline");//条码扫描按钮
                    break;   
    }
}catch(e)
{
}
}


//排序
function OrderBy(orderColum,orderTip)
{
    ifshow="0";
    var ordering = "asc";
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
         allOrderTipDOM.empty();
         $("#"+orderTip).html("↑");
    }
    else
    {
        ordering = "desc";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    orderBy = orderColum+" "+ordering;
    TurnToPage(1);
}

function ShowHisory(pageIndex)
{
    //总询价次数为一的时候不操作
    if($("#txtAskOrder").val()==1)
        return;
    document.getElementById("divHistory").style.display = "block";
    var strParams = "ActionAskPrice=History";    
    strParams += "&No="+$("#PurAskPriceNo_txtCode").val();
    strParams += "&pageIndex="+pageIndex;
    strParams += "&pageCount="+pageCountHistory;
    strParams += "&OrderBy="+OrderByHistory;
//    alert(strParams);
//    return;
    $.ajax(
    {
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/PurchaseManager/PurchaseAskPriceAdd.ashx',//目标地址
           data:strParams,
           cache:false,
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(msg)
           {
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#History tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item)
                    {
                 
                        if(item.AskOrder != null && item.AskOrder != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+ item.AskOrder +"</td>"+
                        "<td height='22' align='center'>"+ item.CountTotal +"</td>"+
                        "<td height='22' align='center'>"+ (parseFloat(item.TotalPrice)).toFixed($("#HiddenPoint").val()) +"</td>"+
                        "<td height='22' align='center'>"+  (parseFloat(item.TotalTax)).toFixed($("#HiddenPoint").val())+"</td>"+
                        "<td height='22' align='center'>"+  (parseFloat(item.TotalFee)).toFixed($("#HiddenPoint").val()) +"</td>"+
                        "<td height='22' align='center'>"+ (parseFloat(item.Discount)).toFixed($("#HiddenPoint").val())+"</td>"+
                        "<td height='22' align='center'>"+ (parseFloat(item.DiscountTotal)).toFixed($("#HiddenPoint").val()) +"</td>"+
                        "<td height='22' align='center'>"+  (parseFloat(item.RealTotal)).toFixed($("#HiddenPoint").val()) +"</td>"
                        ).appendTo($("#History tbody"));
                   });
                   //页码
                   ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountHistory,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"ShowHisory({pageindex});return false;"}//[attr]
                    );
                  totalRecordHistory = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text2").value=msg.totalCount;
                  $("#ShowPageCount").val(pageCountHistory);
                  ShowTotalPage(msg.totalCount,pageCountHistory,pageIndex,$("#pagecount"));
                  $("#ToPage").val(pageIndex);
           },
           error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
           complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.getElementById("Text2").value);pageDataList1("History","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
      });
}


//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount,newPageIndex)
{
    //判断是否是数字
    if (!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    }
    if (!PositiveInteger(newPageIndex))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
        return;
    }
    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecordHistory-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        pageCountHistory=parseInt(newPageCount);
        ShowHisory(parseInt(newPageIndex));
    }
}


function closeHistory()
{
    document.getElementById("divHistory").style.display = "none";
}

function Ifshow(count)
{
    if(count=="0")
    {
        document.getElementById("divpage").style.display = "none";
        document.getElementById("pagecount").style.display = "none";
    }
    else
    {
        document.getElementById("divpage").style.display = "block";
        document.getElementById("pagecount").style.display = "block";
    }
}

//---------------------------------------------------条码扫描Start------------------------------------------------------------------------------------
/*
 参数：商品ID，商品编号，商品名称，去税售价，单位ID，单位名称，销项税率（%），含税售价，销售折扣，规格，类型名称，类型ID，含税进价，去税进价，进项税率(%)，标准成本
*/
//根据条码获取的商品信息填充数据
function GetGoodsDataByBarCode(ID, ProdNo, ProductName, StandardSell, UnitID, UnitName, TaxRate, SellTax, Discount, Specification, CodeTypeName, TypeID, StandardBuy, TaxBuy, InTaxRate, StandardCost, IsBatchNo, BatchNo, ProductCount, CurrentStore, Source, ColorName)
{
    if(!IsExist(ID))//如果重复记录，就不增加
    {
       var rowID=AddAskPriceSignRow();//插入行
       //填充数据
       //物品ID 物品编号，物品名称，去税售价，单位ID，单位名称，销项税率，含税售价，折扣，规格型号，物品分类，物品分类ID，含税进价，去税进价，进项税率,行号
       BarCode_FillParent_Content(ColorName,ID, ProdNo, ProductName, StandardSell, UnitID, UnitName, TaxRate, SellTax, Discount, Specification, CodeTypeName, TypeID, StandardBuy, TaxBuy, InTaxRate, rowID);
 
  
    }
    else
    {
     if($("#HiddenMoreUnit").val()=="False")
      {
      document.getElementById("ProductCount"+rerowID).value=    parseFloat (parseFloat(document.getElementById("ProductCount"+rerowID).value)+1).toFixed($("#HiddenPoint").val());
   
        }
        else
        {
            var count1=document.getElementById("UsedUnitCount"+rerowID).value;
 
        document.getElementById("UsedUnitCount"+rerowID).value=     parseFloat (parseFloat(count1)+1).toFixed($("#HiddenPoint").val());
        }
        
    
        
        
        
        fnTotalInfo();//更改数量后重新计算
    }
    
}

var rerowID="";
//判断是否有相同记录有返回true，没有返回false
function IsExist(ID)
{
     var signFrame = findObj("DetailTable", document);
     if((typeof(signFrame)=="undefined")|| signFrame==null)
     {
        return false;
     }
     for (i = 1; i < signFrame.rows.length; i++) {//判断商品是否在明细列表中
         if (signFrame.rows[i].style.display != "none") {
              var ProductID = $("#ProductID" + i).val(); //商品ID（对应商品表ID）
              if(ProductID==ID)
               {
                   rerowID=i;
                   return true;
               }
            }
        }
     return false;
}
//物品ID 物品编号，物品名称，去税售价，单位ID，单位名称，销项税率，含税售价，折扣，规格型号，物品分类，物品分类ID，含税进价，去税进价，进项税率,行号
function BarCode_FillParent_Content(ColorName,id, no, productname, dddf, unitid, unit, df, sdfge, discount, standard, fg, fgf, taxprice, price, taxrate, RowID)
{
    var Rate = $("#txtExchangeRate").val();
    //var RowID = popTechObj.InputObj;



    $("#DtlSColor" + RowID).val(ColorName);
    $("#ProductID"+RowID).val(id);
    $("#ProductNo"+RowID).val(no);
    $("#ProductName"+RowID).val(productname);
   
    
    $("#UnitPrice"+RowID).val( (parseFloat(price)/parseFloat(Rate)).toFixed($("#HiddenPoint").val()));
    var unitPrice= (parseFloat(price)/parseFloat(Rate)).toFixed($("#HiddenPoint").val());
    $("#UnitPriceHide"+RowID).val(  (parseFloat(price)).toFixed($("#HiddenPoint").val()));
    $("#UnitID"+RowID).val(unitid);
    $("#UnitName"+RowID).val(unit);
    $("#TaxRate"+RowID).val(taxrate);
    var taxPrice= (parseFloat(taxprice)/parseFloat(Rate)).toFixed($("#HiddenPoint").val());
    $("#TaxPrice"+RowID).val(  (parseFloat(taxprice)/parseFloat(Rate)).toFixed($("#HiddenPoint").val()));
    $("#TaxPriceHide"+RowID).val(   (parseFloat(taxprice)).toFixed($("#HiddenPoint").val()));
//    $("#Discount"+RowID).val(discount);
    $("#Specification"+RowID).val(standard);
    $("#ProductCount"+RowID).val(1);
 
    
            if($("#HiddenMoreUnit").val()=="False")
            {
                 fnMergeDetail();
            }
            else
            {
            
             GetUnitGroupSelectEx(id,"InUnit","SignItem_TD_UnitID_Select" + RowID,"ChangeUnit(this.id,"+RowID+","+unitPrice+","+taxPrice+")","unitdiv" + RowID,'',"FillApplyContent("+RowID+","+unitPrice+","+taxPrice+","+1+",'1','','','')");//绑定单位组
            }
}

 
//---------------------------------------------------条码扫描END------------------------------------------------------------------------------------

function ShowSnapshot() {
    var intProductID = 0;
    var detailRows = 0;
    var snapProductName = '';
    var snapProductNo = '';
    var ck = document.getElementsByName("Dtlchk");
    var signFrame = findObj("DetailTable", document);
    for (var i = 0; i < ck.length; i++) {

        var rowID = i + 1;
        if (signFrame.rows[rowID].style.display != "none") {
            if (ck[i].checked) {
                detailRows++;
                intProductID = document.getElementById('ProductID' + rowID).value;
                snapProductName = document.getElementById('ProductName' + rowID).value;
                snapProductNo = document.getElementById('ProductNo' + rowID).value;
            }
        }

    }

    if (detailRows == 1) {
        if (intProductID <= 0 || strlen(cTrim(intProductID, 0)) <= 0) {
            popMsgObj.ShowMsg('选中的明细行中没有添加物品');
            return false;
        }
        ShowStorageSnapshot(intProductID, snapProductName, snapProductNo);

    }
    else {
        popMsgObj.ShowMsg('请选择单个物品查看库存快照');
        return false;
    }
}